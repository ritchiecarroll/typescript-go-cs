namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using slices = slices_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.sync;
using iter = iter_package;
using ꓸꓸꓸжEmitHelper = Span<ж<printer_package.EmitHelper>>;
using static printer_package;

partial class printer_package {

partial struct EmitContext {
    public ж<NodeFactory> Factory;
    internal map<ж<astꓸMemberName>, ж<AutoGenerateInfo>> autoGenerate;
    internal map<ж<astꓸStringLiteralNode>, ж<ast.Node>> textSource;
    internal map<ж<ast.Node>, ж<ast.Node>> original;
    internal core.LinkStore<ж<ast.Node>, emitNode> emitNodes;
    internal map<ж<ast.Node>, ж<astꓸExpression>> assignedName;
    internal map<ж<ast.Node>, ж<astꓸIdentifierNode>> classThis;
    internal core.Stack<ж<varScope>> varScopeStack;
    internal core.Stack<ж<varScope>> letScopeStack;
    internal collections.OrderedSet<ж<EmitHelper>> emitHelpers;
}

partial struct environmentFlags /*num:nint*/;

internal static environmentFlags environmentFlagsNone => 0;
internal static environmentFlags environmentFlagsInParameters => /* 1 << 0 */ 1;
internal static environmentFlags environmentFlagsVariablesHoistedInParameters => /* 1 << 1 */ 2;

partial struct varScope {
    internal slice<ж<astꓸVariableDeclarationNode>> variables;
    internal slice<ж<astꓸFunctionDeclarationNode>> functions;
    internal environmentFlags flags;
    internal slice<ж<ast.Node>> initializationStatements;
}

public static ж<EmitContext> NewEmitContext() {
    var c = Ꮡ(new EmitContext());
    c.Factory = NewNodeFactory(c);
    return c;
}

internal static readonly ж<sync.Pool> ᏑemitContextPool = new StaticBox<sync.Pool>(new sync.Pool(
    New: () => NewEmitContext().OrTypedNil()
));
internal static ref sync.Pool emitContextPool => ref ((StaticBox<sync.Pool>)ᏑemitContextPool).ValueSlot;

public static (ж<EmitContext>, Action) GetEmitContext() {
    var c = ᏑemitContextPool.Get()._<ж<EmitContext>>();
    var cʗ1 = c;
    return (c, () => {
        cʗ1.Reset();
        ᏑemitContextPool.Put(cʗ1.OrTypedNil());
    });
}

} partial class printerᴛEmitContext {

public static void Reset(this ref EmitContext c) {
    c = new EmitContext(
        Factory: c.Factory
    );
}

internal static void onCreate(this ref EmitContext c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    node.Flags |= (ast.NodeFlags)(ast.NodeFlagsSynthesized);
}

internal static void onUpdate(this ref EmitContext c, ж<ast.Node> updated, ж<ast.Node> original) {
    c.SetOriginal(updated, original);
}

internal static void onClone(this ref EmitContext c, ж<ast.Node> updated, ж<ast.Node> original) {
    c.SetOriginal(updated, original);
    if (ast.IsIdentifier(updated) || ast.IsPrivateIdentifier(updated)) {
        var autoGenerate = c.autoGenerate[original]; if (autoGenerate != nil) {
            ref var autoGenerateCopy = ref heap<AutoGenerateInfo>(autoGenerate.Value, out var ᏑautoGenerateCopy);
            c.autoGenerate[updated] = ᏑautoGenerateCopy;
        }
    }
}

public static ж<ast.NodeVisitor> NewNodeVisitor(this ж<EmitContext> Ꮡc, Func<ж<ast.Node>, ж<ast.Node>> visit) {
    ref var c = ref Ꮡc.DerefOrNull();

    return ast.NewNodeVisitor(visit, c.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), new ast.NodeVisitorHooks(
        VisitParameters: Ꮡc.VisitParameters,
        VisitFunctionBody: Ꮡc.VisitFunctionBody,
        VisitIterationBody: Ꮡc.VisitIterationBody,
        VisitTopLevelStatements: Ꮡc.VisitVariableEnvironment,
        VisitEmbeddedStatement: Ꮡc.VisitEmbeddedStatement
    ));
}

public static void StartVariableEnvironment(this ref EmitContext c) {
    c.varScopeStack.Push(Ꮡ(new varScope()));
    c.StartLexicalEnvironment();
}

public static slice<ж<astꓸStatement>> EndVariableEnvironment(this ref EmitContext c) {
    var scope = c.varScopeStack.Pop();
    slice<ж<astꓸStatement>> statements = default;
    if (len(scope.functions) > 0) {
        statements = slices.Clone<slice<ж<astꓸFunctionDeclarationNode>>, ж<astꓸFunctionDeclarationNode>>(scope.functions);
    }
    if (len(scope.variables) > 0) {
        var varDeclList = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(scope.variables), ast.NodeFlagsNone);
        var varStatement = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, varDeclList);
        c.SetEmitFlags(varStatement, EFCustomPrologue);
        statements = append(statements, varStatement);
    }
    if (len(scope.initializationStatements) > 0) {
        statements = appendꓸꓸꓸ(statements, scope.initializationStatements);
    }
    return appendꓸꓸꓸ(statements, c.EndLexicalEnvironment());
}

public static ж<astꓸStatementList> EndAndMergeVariableEnvironmentList(this ж<EmitContext> Ꮡc, ж<astꓸStatementList> Ꮡstatements) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var statements = ref Ꮡstatements.DerefOrNull();

    slice<ж<astꓸStatement>> nodes = default;
    if (Ꮡstatements != nil) {
        nodes = statements.Nodes;
    }

    var (result, changed) = Ꮡc.endAndMergeVariableEnvironment(nodes); if (changed) {
        var list = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(result);
        list.Loc = statements.Loc;
        return list;
    }

    return Ꮡstatements;
}

public static slice<ж<astꓸStatement>> EndAndMergeVariableEnvironment(this ж<EmitContext> Ꮡc, slice<ж<astꓸStatement>> statements) {
    var (result, _) = Ꮡc.endAndMergeVariableEnvironment(statements);
    return result;
}

internal static (slice<ж<astꓸStatement>>, bool) endAndMergeVariableEnvironment(this ж<EmitContext> Ꮡc, slice<ж<astꓸStatement>> statements) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.mergeEnvironment(statements, c.EndVariableEnvironment());
}

public static void AddVariableDeclaration(this ref EmitContext c, ж<astꓸIdentifierNode> name) {
    var varDecl = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(name, nil, nil, nil);
    c.SetEmitFlags(varDecl, EFNoNestedSourceMaps);
    var scope = c.varScopeStack.Peek();
    scope.variables = append(scope.variables, varDecl);
    if ((environmentFlags)(scope.flags & environmentFlagsInParameters) != 0) {
        scope.flags |= (environmentFlags)(environmentFlagsVariablesHoistedInParameters);
    }
}

public static void AddHoistedFunctionDeclaration(this ref EmitContext c, ж<astꓸFunctionDeclarationNode> node) {
    c.SetEmitFlags(node, EFCustomPrologue);
    var scope = c.varScopeStack.Peek();
    scope.functions = append(scope.functions, node);
}

public static void StartLexicalEnvironment(this ref EmitContext c) {
    c.letScopeStack.Push(Ꮡ(new varScope()));
}

public static slice<ж<astꓸStatement>> EndLexicalEnvironment(this ref EmitContext c) {
    var scope = c.letScopeStack.Pop();
    slice<ж<astꓸStatement>> statements = default;
    if (len(scope.variables) > 0) {
        var varDeclList = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(scope.variables), ast.NodeFlagsLet);
        var varStatement = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, varDeclList);
        c.SetEmitFlags(varStatement, EFCustomPrologue);
        statements = append(statements, varStatement);
    }
    return statements;
}

public static ж<astꓸStatementList> EndAndMergeLexicalEnvironmentList(this ж<EmitContext> Ꮡc, ж<astꓸStatementList> Ꮡstatements) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var statements = ref Ꮡstatements.DerefOrNull();

    slice<ж<astꓸStatement>> nodes = default;
    if (Ꮡstatements != nil) {
        nodes = statements.Nodes;
    }

    var (result, changed) = Ꮡc.endAndMergeLexicalEnvironment(nodes); if (changed) {
        var list = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(result);
        list.Loc = statements.Loc;
        return list;
    }

    return Ꮡstatements;
}

public static slice<ж<astꓸStatement>> EndAndMergeLexicalEnvironment(this ж<EmitContext> Ꮡc, slice<ж<astꓸStatement>> statements) {
    var (result, _) = Ꮡc.endAndMergeLexicalEnvironment(statements);
    return result;
}

internal static (slice<ж<astꓸStatement>>, bool) endAndMergeLexicalEnvironment(this ж<EmitContext> Ꮡc, slice<ж<astꓸStatement>> statements) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.mergeEnvironment(statements, c.EndLexicalEnvironment());
}

public static void AddLexicalDeclaration(this ref EmitContext c, ж<astꓸIdentifierNode> name) {
    var varDecl = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(name, nil, nil, nil);
    c.SetEmitFlags(varDecl, EFNoNestedSourceMaps);
    var scope = c.letScopeStack.Peek();
    scope.variables = append(scope.variables, varDecl);
}

public static ж<astꓸStatementList> MergeEnvironmentList(this ж<EmitContext> Ꮡc, ж<astꓸStatementList> Ꮡstatements, slice<ж<astꓸStatement>> declarations) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var statements = ref Ꮡstatements.DerefOrNull();

    var (result, changed) = Ꮡc.mergeEnvironment(statements.Nodes, declarations); if (changed) {
        var list = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(result);
        list.Loc = statements.Loc;
        return list;
    }
    return Ꮡstatements;
}

public static slice<ж<astꓸStatement>> MergeEnvironment(this ж<EmitContext> Ꮡc, slice<ж<astꓸStatement>> statements, slice<ж<astꓸStatement>> declarations) {
    var (result, _) = Ꮡc.mergeEnvironment(statements, declarations);
    return result;
}

internal static (slice<ж<astꓸStatement>>, bool) mergeEnvironment(this ж<EmitContext> Ꮡc, slice<ж<astꓸStatement>> statements, slice<ж<astꓸStatement>> declarations) {
    if (len(declarations) == 0) {
        return (statements, false);
    }

    var changed = false;

    nint leftStandardPrologueEnd = findSpanEnd<ж<astꓸStatement>>(statements, ast.IsPrologueDirective, 0);
    nint leftHoistedFunctionsEnd = findSpanEndWithEmitContext<ж<astꓸStatement>>(Ꮡc, statements, ((Func<ж<EmitContext>, ж<astꓸStatement>, bool>)(printerᴛEmitContext.isHoistedFunction)), leftStandardPrologueEnd);
    nint leftHoistedVariablesEnd = findSpanEndWithEmitContext<ж<astꓸStatement>>(Ꮡc, statements, ((Func<ж<EmitContext>, ж<astꓸStatement>, bool>)(printerᴛEmitContext.isHoistedVariableStatement)), leftHoistedFunctionsEnd);

    nint rightStandardPrologueEnd = findSpanEnd<ж<astꓸStatement>>(declarations, ast.IsPrologueDirective, 0);
    nint rightHoistedFunctionsEnd = findSpanEndWithEmitContext<ж<astꓸStatement>>(Ꮡc, declarations, ((Func<ж<EmitContext>, ж<astꓸStatement>, bool>)(printerᴛEmitContext.isHoistedFunction)), rightStandardPrologueEnd);
    nint rightHoistedVariablesEnd = findSpanEndWithEmitContext<ж<astꓸStatement>>(Ꮡc, declarations, ((Func<ж<EmitContext>, ж<astꓸStatement>, bool>)(printerᴛEmitContext.isHoistedVariableStatement)), rightHoistedFunctionsEnd);
    nint rightCustomPrologueEnd = findSpanEndWithEmitContext<ж<astꓸStatement>>(Ꮡc, declarations, ((Func<ж<EmitContext>, ж<astꓸStatement>, bool>)(printerᴛEmitContext.isCustomPrologue)), rightHoistedVariablesEnd);
    if (rightCustomPrologueEnd != len(declarations)) {
        throw panic("Expected declarations to be valid standard or custom prologues");
    }

    var left = statements;

    if (rightCustomPrologueEnd > rightHoistedVariablesEnd) {
        left = core.Splice(left, leftHoistedVariablesEnd, 0, declarations.slice(rightHoistedVariablesEnd, rightCustomPrologueEnd).ꓸꓸꓸ);
        changed = true;
    }

    if (rightHoistedVariablesEnd > rightHoistedFunctionsEnd) {
        left = core.Splice(left, leftHoistedFunctionsEnd, 0, declarations.slice(rightHoistedFunctionsEnd, rightHoistedVariablesEnd).ꓸꓸꓸ);
        changed = true;
    }

    if (rightHoistedFunctionsEnd > rightStandardPrologueEnd) {
        left = core.Splice(left, leftStandardPrologueEnd, 0, declarations.slice(rightStandardPrologueEnd, rightHoistedFunctionsEnd).ꓸꓸꓸ);
        changed = true;
    }

    if (rightStandardPrologueEnd > 0) {
        if (leftStandardPrologueEnd == 0) {
            left = core.Splice(left, 0, 0, declarations.slice(0, rightStandardPrologueEnd).ꓸꓸꓸ);
            changed = true;
        } else {
            ref var leftPrologues = ref heap(new collections.Set<@string>(), out var ᏑleftPrologues);
            foreach (var i in range(leftStandardPrologueEnd)) {
                var leftPrologue = statements[i];
                leftPrologues.Add(leftPrologue.Expression().Text());
            }
            for (nint i = rightStandardPrologueEnd - 1; i >= 0; i--) {
                var rightPrologue = declarations[i];
                if (!ᏑleftPrologues.Has(rightPrologue.Expression().Text())) {
                    left = core.Concatenate(slice<ж<astꓸStatement>>([rightPrologue]), left);
                    changed = true;
                }
            }
        }
    }

    return (left, changed);
}

internal static bool isCustomPrologue(this ref EmitContext c, ж<astꓸStatement> node) {
    return (EmitFlags)(c.EmitFlags(node) & EFCustomPrologue) != 0;
}

internal static bool isHoistedFunction(this ref EmitContext c, ж<astꓸStatement> node) {
    return c.isCustomPrologue(node) && ast.IsFunctionDeclaration(node);
}

} partial class printer_package {

internal static bool isHoistedVariable(ж<astꓸVariableDeclarationNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsIdentifier(node.Name()) && node.Initializer() == nil;
}

} partial class printerᴛEmitContext {

internal static bool isHoistedVariableStatement(this ref EmitContext c, ж<astꓸStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return c.isCustomPrologue(Ꮡnode) && ast.IsVariableStatement(Ꮡnode) && core.Every<ж<ast.Node>>(node.AsVariableStatement().DeclarationList.AsVariableDeclarationList().Declarations.Nodes, isHoistedVariable);
}

public static bool HasAutoGenerateInfo(this ref EmitContext c, ж<astꓸMemberName> node) {
    if (node != nil) {
        var (_, ok) = c.autoGenerate[node, ꟷ];
        return ok;
    }
    return false;
}

public static ж<AutoGenerateInfo> GetAutoGenerateInfo(this ref EmitContext c, ж<astꓸMemberName> name) {
    if (name == nil) {
        return default;
    }
    return c.autoGenerate[name];
}

public static ж<ast.Node> GetNodeForGeneratedName(this ref EmitContext c, ж<astꓸMemberName> name) {
    var autoGenerate = c.autoGenerate[name]; if (autoGenerate != nil && autoGenerate.Flags.IsNode()) {
        return c.getNodeForGeneratedNameWorker(autoGenerate.Node, autoGenerate.Id);
    }
    return name;
}

internal static ж<ast.Node> getNodeForGeneratedNameWorker(this ref EmitContext c, ж<ast.Node> Ꮡnode, AutoGenerateId autoGenerateId) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var original = c.Original(Ꮡnode);
    while (original != nil) {
        Ꮡnode = original; node = ref Ꮡnode.DerefOrNull();
        if (ast.IsMemberName(Ꮡnode)) {
            var autoGenerate = c.autoGenerate[Ꮡnode];
            if (autoGenerate == nil || autoGenerate.Flags.IsNode() && autoGenerate.Id != autoGenerateId) {
                break;
            }
            if (autoGenerate.Flags.IsNode()) {
                original = autoGenerate.Node;
                continue;
            }
        }
        original = c.Original(Ꮡnode);
    }
    return Ꮡnode;
}

} partial class printer_package {

partial struct AutoGenerateOptions {
    public GeneratedIdentifierFlags Flags;
    public @string Prefix;
    public @string Suffix;
}

internal static readonly ж<atomic.Uint32> ᏑnextAutoGenerateId = new StaticBox<atomic.Uint32>(default(atomic.Uint32));
internal static ref atomic.Uint32 nextAutoGenerateId => ref ((StaticBox<atomic.Uint32>)ᏑnextAutoGenerateId).ValueSlot;

partial struct AutoGenerateId /*num:uint32*/;

partial struct AutoGenerateInfo {
    public GeneratedIdentifierFlags Flags;
    public AutoGenerateId Id;
    public @string Prefix;
    public @string Suffix;
    public ж<ast.Node> Node;
}

} partial class printerᴛEmitContext {

public static void SetOriginal(this ref EmitContext c, ж<ast.Node> node, ж<ast.Node> original) {
    c.SetOriginalEx(node, original, false);
}

public static void UnsetOriginal(this ref EmitContext c, ж<ast.Node> node) {
    delete(c.original, node);
}

public static void SetOriginalEx(this ref EmitContext c, ж<ast.Node> node, ж<ast.Node> Ꮡoriginal, bool allowOverwrite) {
    ref var original = ref Ꮡoriginal.DerefOrNull();

    if (Ꮡoriginal == nil) {
        throw panic("Original cannot be nil.");
    }

    if (c.original == nil) {
        c.original = new map<ж<ast.Node>, ж<ast.Node>>();
    }

    var (existing, ok) = c.original[node, ꟷ];
    if (!ok) {
        c.original[node] = Ꮡoriginal;
        var emitNode = c.emitNodes.TryGet(Ꮡoriginal); if (emitNode != nil) {
            c.emitNodes.Get(node).copyFrom(emitNode);
        }
    } else if (!allowOverwrite && existing != Ꮡoriginal) {
        throw panic("Original node already set.");
    } else if (allowOverwrite) {
        c.original[node] = Ꮡoriginal;
    }
}

public static ж<ast.Node> Original(this ref EmitContext c, ж<ast.Node> node) {
    return c.original[node];
}

public static ж<ast.Node> MostOriginal(this ref EmitContext c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode != nil) {
        var original = c.Original(Ꮡnode);
        while (original != nil) {
            Ꮡnode = original; node = ref Ꮡnode.DerefOrNull();
            original = c.Original(Ꮡnode);
        }
    }
    return Ꮡnode;
}

public static ж<ast.Node> ParseNode(this ref EmitContext c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = c.MostOriginal(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    if (Ꮡnode != nil && ast.IsParseTreeNode(Ꮡnode)) {
        return Ꮡnode;
    }
    return default;
}

public static bool IsFileLevelUniqueName(this ref EmitContext c, ж<ast.SourceFile> ᏑsourceFile, @string name, Func<@string, bool> hasGlobalName) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (hasGlobalName != nil && hasGlobalName(name)) {
        return false;
    }
    ᏑsourceFile = c.MostOriginal(ᏑsourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).AsSourceFile(); sourceFile = ref ᏑsourceFile.DerefOrNull();
    return !ᏑsourceFile.HasIdentifier(name);
}

} partial class printer_package {

partial struct emitNodeFlags /*num:uint32*/;

internal static emitNodeFlags hasCommentRange => /* 1 << iota */ 1;
internal static emitNodeFlags hasSourceMapRange => 2;

partial struct SnippetKind /*num:nint*/;

public static SnippetKind SnippetKindTabStop => /* iota */ 0;

partial struct SnippetElement {
    public SnippetKind Kind;
    public nint Order;
}

partial struct SynthesizedComment {
    public ast.Kind Kind;
    public core.TextRange Loc;
    public bool HasLeadingNewLine;
    public bool HasTrailingNewLine;
    public @string Text;
}

partial struct emitNode {
    internal emitNodeFlags flags;
    internal EmitFlags emitFlags;
    internal core.TextRange commentRange;
    internal core.TextRange sourceMapRange;
    internal map<ast.Kind, core.TextRange> tokenSourceMapRanges;
    internal slice<ж<EmitHelper>> helpers;
    internal ж<astꓸIdentifierNode> externalHelpersModuleName;
    internal slice<SynthesizedComment> leadingComments;
    internal slice<SynthesizedComment> trailingComments;
    internal ж<astꓸTypeNode> typeNode;
    internal ж<SnippetElement> snippetElement;
}

} partial class printerᴛemitNode {

internal static void copyFrom(this ref emitNode e, ж<emitNode> Ꮡsource) {
    ref var source = ref Ꮡsource.DerefOrNull();

    e.flags = source.flags;
    e.emitFlags = source.emitFlags;
    e.commentRange = source.commentRange;
    e.sourceMapRange = source.sourceMapRange;
    e.tokenSourceMapRanges = maps.Clone<map<ast.Kind, core.TextRange>, ast.Kind, core.TextRange>(source.tokenSourceMapRanges);
    e.helpers = slices.Clone<slice<ж<EmitHelper>>, ж<EmitHelper>>(source.helpers);
    e.externalHelpersModuleName = source.externalHelpersModuleName;
    if (source.snippetElement != nil) {
        ref var snippetElement = ref heap<SnippetElement>(source.snippetElement.Value, out var ᏑsnippetElement);
        e.snippetElement = ᏑsnippetElement;
    }
}

} partial class printerᴛEmitContext {

public static EmitFlags EmitFlags(this ref EmitContext c, ж<ast.Node> node) {
    var emitNode = c.emitNodes.TryGet(node); if (emitNode != nil) {
        return emitNode.emitFlags;
    }
    return EFNone;
}

public static void SetEmitFlags(this ref EmitContext c, ж<ast.Node> node, EmitFlags flags) {
    c.emitNodes.Get(node).emitFlags = flags;
}

public static void AddEmitFlags(this ref EmitContext c, ж<ast.Node> node, EmitFlags flags) {
    c.emitNodes.Get(node).emitFlags |= (EmitFlags)(flags);
}

public static ж<SnippetElement> SnippetElement(this ref EmitContext c, ж<ast.Node> node) {
    var emitNode = c.emitNodes.TryGet(node); if (emitNode != nil) {
        return emitNode.snippetElement;
    }
    return default;
}

public static void SetSnippetElement(this ref EmitContext c, ж<ast.Node> node, SnippetElement snippetElementʗp) {
    ref var snippetElement = ref heap(snippetElementʗp, out var ᏑsnippetElement);

    c.emitNodes.Get(node).snippetElement = ᏑsnippetElement;
}

public static core.TextRange CommentRange(this ref EmitContext c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var emitNode = c.emitNodes.TryGet(Ꮡnode); if (emitNode != nil && (emitNodeFlags)(emitNode.flags & hasCommentRange) != 0) {
        return emitNode.commentRange;
    }
    return node.Loc;
}

public static void SetCommentRange(this ref EmitContext c, ж<ast.Node> node, core.TextRange loc) {
    var emitNode = c.emitNodes.Get(node);
    emitNode.commentRange = loc;
    emitNode.flags |= (emitNodeFlags)(hasCommentRange);
}

public static void AssignCommentRange(this ref EmitContext c, ж<ast.Node> to, ж<ast.Node> from) {
    c.SetCommentRange(to, c.CommentRange(from));
}

public static core.TextRange SourceMapRange(this ref EmitContext c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var emitNode = c.emitNodes.TryGet(Ꮡnode); if (emitNode != nil && (emitNodeFlags)(emitNode.flags & hasSourceMapRange) != 0) {
        return emitNode.sourceMapRange;
    }
    return node.Loc;
}

public static void SetSourceMapRange(this ref EmitContext c, ж<ast.Node> node, core.TextRange loc) {
    var emitNode = c.emitNodes.Get(node);
    emitNode.sourceMapRange = loc;
    emitNode.flags |= (emitNodeFlags)(hasSourceMapRange);
}

public static void AssignSourceMapRange(this ref EmitContext c, ж<ast.Node> to, ж<ast.Node> from) {
    c.SetSourceMapRange(to, c.SourceMapRange(from));
}

public static void AssignCommentAndSourceMapRanges(this ref EmitContext c, ж<ast.Node> to, ж<ast.Node> from) {
    var emitNode = c.emitNodes.Get(to);
    var commentRange = c.CommentRange(from);
    var sourceMapRange = c.SourceMapRange(from);
    emitNode.commentRange = commentRange;
    emitNode.sourceMapRange = sourceMapRange;
    emitNode.flags |= (emitNodeFlags)((emitNodeFlags)(hasCommentRange | hasSourceMapRange));
}

public static (core.TextRange, bool) TokenSourceMapRange(this ref EmitContext c, ж<ast.Node> node, ast.Kind kind) {
    var emitNode = c.emitNodes.TryGet(node); if (emitNode != nil && emitNode.tokenSourceMapRanges != nil) {
        var (loc, ok) = emitNode.tokenSourceMapRanges[kind, ꟷ]; if (ok) {
            return (loc, true);
        }
    }
    return (new core.TextRange(), false);
}

public static void SetTokenSourceMapRange(this ref EmitContext c, ж<ast.Node> node, ast.Kind kind, core.TextRange loc) {
    var emitNode = c.emitNodes.Get(node);
    if (emitNode.tokenSourceMapRanges == nil) {
        emitNode.tokenSourceMapRanges = new map<ast.Kind, core.TextRange>();
    }
    emitNode.tokenSourceMapRanges[kind] = loc;
}

public static ж<astꓸExpression> AssignedName(this ref EmitContext c, ж<ast.Node> node) {
    return c.assignedName[node];
}

public static ж<ast.Node> TextSource(this ref EmitContext c, ж<astꓸStringLiteralNode> node) {
    return c.textSource[node];
}

public static void SetAssignedName(this ref EmitContext c, ж<ast.Node> node, ж<astꓸExpression> name) {
    if (c.assignedName == nil) {
        c.assignedName = new map<ж<ast.Node>, ж<astꓸExpression>>();
    }
    c.assignedName[node] = name;
}

public static ж<astꓸExpression> ClassThis(this ref EmitContext c, ж<ast.Node> node) {
    return c.classThis[node];
}

public static void SetClassThis(this ref EmitContext c, ж<ast.Node> node, ж<astꓸIdentifierNode> ᏑclassThis) {
    ref var classThis = ref ᏑclassThis.DerefOrNull();

    if (c.classThis == nil) {
        c.classThis = new map<ж<ast.Node>, ж<astꓸExpression>>();
    }
    c.classThis[node] = ᏑclassThis;
}

public static void RequestEmitHelper(this ref EmitContext c, ж<EmitHelper> Ꮡhelper) {
    ref var helper = ref Ꮡhelper.DerefOrNull();

    if (helper.Scoped) {
        throw panic("Cannot request a scoped emit helper");
    }
    foreach (var (_, h) in range(helper.Dependencies)) {
        c.RequestEmitHelper(h);
    }
    c.emitHelpers.Add(Ꮡhelper);
}

public static slice<ж<EmitHelper>> ReadEmitHelpers(this ж<EmitContext> Ꮡc) {
    ref var c = ref Ꮡc.DerefOrNull();

    var helpers = slices.Collect(Ꮡc.ᏑemitHelpers.Values());
    c.emitHelpers.Clear();
    return helpers;
}

public static void AddEmitHelper(this ref EmitContext c, ж<ast.Node> node, params ꓸꓸꓸжEmitHelper helperʗp) {
    var helper = helperʗp.sslice();

    var emitNode = c.emitNodes.Get(node);
    foreach (var (_, h) in range(helper)) {
        emitNode.helpers = core.AppendIfUnique(emitNode.helpers, h);
    }
}

public static void MoveEmitHelpers(this ref EmitContext c, ж<ast.Node> source, ж<ast.Node> target, Func<ж<EmitHelper>, bool> predicate) {
    var sourceEmitNode = c.emitNodes.TryGet(source);
    if (sourceEmitNode == nil) {
        return;
    }
    var sourceEmitHelpers = sourceEmitNode.helpers;
    if (len(sourceEmitHelpers) == 0) {
        return;
    }

    var targetEmitNode = c.emitNodes.Get(target);
    nint helpersRemoved = 0;
    foreach (var i in range(len(sourceEmitHelpers))) {
        var helper = sourceEmitHelpers[i];
        if (predicate(helper)) {
            helpersRemoved++;
            targetEmitNode.helpers = core.AppendIfUnique(targetEmitNode.helpers, helper);
        } else if (helpersRemoved > 0) {
            sourceEmitHelpers[i - helpersRemoved] = helper;
        }
    }

    if (helpersRemoved > 0) {
        sourceEmitHelpers = sourceEmitHelpers.slice(0, len(sourceEmitHelpers) - helpersRemoved);
        sourceEmitNode.helpers = sourceEmitHelpers;
    }
}

public static slice<ж<EmitHelper>> GetEmitHelpers(this ref EmitContext c, ж<ast.Node> node) {
    var emitNode = c.emitNodes.TryGet(node);
    if (emitNode != nil) {
        return emitNode.helpers;
    }
    return default;
}

public static ж<astꓸIdentifierNode> GetExternalHelpersModuleName(this ref EmitContext c, ж<ast.SourceFile> node) {
    var parseNode = c.ParseNode(node.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); if (parseNode != nil) {
        var emitNode = c.emitNodes.TryGet(parseNode); if (emitNode != nil) {
            return emitNode.externalHelpersModuleName;
        }
    }
    return default;
}

public static void SetExternalHelpersModuleName(this ref EmitContext c, ж<ast.SourceFile> node, ж<astꓸIdentifierNode> name) {
    var parseNode = c.ParseNode(node.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (parseNode == nil) {
        throw panic("Node must be a parse tree node or have an Original pointer to a parse tree node.");
    }

    var emitNode = c.emitNodes.Get(parseNode);
    emitNode.externalHelpersModuleName = name;
}

public static bool HasRecordedExternalHelpers(this ref EmitContext c, ж<ast.SourceFile> node) {
    var parseNode = c.ParseNode(node.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); if (parseNode != nil) {
        var emitNode = c.emitNodes.TryGet(parseNode);
        return emitNode != nil && (emitNode.externalHelpersModuleName != nil || (EmitFlags)(emitNode.emitFlags & EFExternalHelpers) != 0);
    }
    return false;
}

public static bool IsCallToHelper(this ref EmitContext c, ж<astꓸExpression> ᏑfirstSegment, @string helperName) {
    ref var firstSegment = ref ᏑfirstSegment.DerefOrNull();

    return ast.IsCallExpression(ᏑfirstSegment) && ast.IsIdentifier(firstSegment.Expression()) && ((EmitFlags)(c.EmitFlags(firstSegment.Expression()) & EFHelperName)) != 0 && firstSegment.Expression().Text() == helperName;
}

public static ж<astꓸStatementList> VisitVariableEnvironment(this ж<EmitContext> Ꮡc, ж<astꓸStatementList> nodes, ж<ast.NodeVisitor> Ꮡvisitor) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var visitor = ref Ꮡvisitor.DerefOrNull();

    c.StartVariableEnvironment();
    return Ꮡc.EndAndMergeVariableEnvironmentList(visitor.VisitNodes(nodes));
}

public static ж<astꓸParameterList> VisitParameters(this ref EmitContext c, ж<astꓸParameterList> Ꮡnodes, ж<ast.NodeVisitor> Ꮡvisitor) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();
    ref var visitor = ref Ꮡvisitor.DerefOrNull();

    c.StartVariableEnvironment();
    var scope = c.varScopeStack.Peek();
    environmentFlags oldFlags = scope.flags;
    scope.flags |= (environmentFlags)(environmentFlagsInParameters);
    Ꮡnodes = visitor.VisitNodes(Ꮡnodes); nodes = ref Ꮡnodes.DerefOrNull();

    if ((environmentFlags)(scope.flags & environmentFlagsVariablesHoistedInParameters) != 0) {
        Ꮡnodes = c.addDefaultValueAssignmentsIfNeeded(Ꮡnodes); nodes = ref Ꮡnodes.DerefOrNull();
    }
    scope.flags = oldFlags;
    return Ꮡnodes;
}

internal static ж<astꓸParameterList> addDefaultValueAssignmentsIfNeeded(this ref EmitContext c, ж<astꓸParameterList> ᏑnodeList) {
    ref var nodeList = ref ᏑnodeList.DerefOrNull();

    if (ᏑnodeList == nil) {
        return ᏑnodeList;
    }
    slice<ж<ast.Node>> result = default;
    var nodes = nodeList.Nodes;
    foreach (var (i, parameter) in range(nodes)) {
        var updated = c.addDefaultValueAssignmentIfNeeded(parameter.AsParameterDeclaration());
        if (updated != parameter) {
            if (result == nil) {
                result = slices.Clone<slice<ж<ast.Node>>, ж<ast.Node>>(nodes);
            }
            result[i] = updated;
        }
    }
    if (result != nil) {
        var res = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(result);
        res.Loc = nodeList.Loc;
        return res;
    }
    return ᏑnodeList;
}

internal static ж<ast.Node> addDefaultValueAssignmentIfNeeded(this ref EmitContext c, ж<ast.ParameterDeclaration> Ꮡparameter) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();

    if (parameter.DotDotDotToken != nil) {
        return Ꮡparameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    } else if (ast.IsBindingPattern(parameter.Name())) {
        return c.addDefaultValueAssignmentForBindingPattern(Ꮡparameter);
    } else if (parameter.Initializer != nil) {
        return c.addDefaultValueAssignmentForInitializer(Ꮡparameter, parameter.Name(), parameter.Initializer);
    }
    return Ꮡparameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

internal static ж<ast.Node> addDefaultValueAssignmentForBindingPattern(this ref EmitContext c, ж<ast.ParameterDeclaration> Ꮡparameter) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();

    ж<ast.Node> initNode = default;
    if (parameter.Initializer != nil) {
        initNode = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
            c.Factory.NewStrictEqualityExpression(
                c.Factory.NewGeneratedNameForNode(Ꮡparameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
                c.Factory.NewVoidZeroExpression()),
            c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
            parameter.Initializer,
            c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
            c.Factory.NewGeneratedNameForNode(Ꮡparameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    } else {
        initNode = c.Factory.NewGeneratedNameForNode(Ꮡparameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    c.AddInitializationStatement(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
        nil,
        c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
            parameter.Name(),
            nil,
            parameter.Type,
            initNode)
        ])), ast.NodeFlagsNone)));
    return c.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
        Ꮡparameter,
        Ꮡparameter.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers(),
        parameter.DotDotDotToken,
        c.Factory.NewGeneratedNameForNode(Ꮡparameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        parameter.QuestionToken,
        parameter.Type,
        nil);
}

internal static ж<ast.Node> addDefaultValueAssignmentForInitializer(this ref EmitContext c, ж<ast.ParameterDeclaration> Ꮡparameter, ж<ast.Node> Ꮡname, ж<ast.Node> initializer) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    c.AddEmitFlags(initializer, (EmitFlags)(EFNoSourceMap | EFNoComments));
    var nameClone = name.Clone(new NodeFactoryжNodeFactoryCoercible(c.Factory));
    c.AddEmitFlags(nameClone, EFNoSourceMap);
    var initAssignment = c.Factory.NewAssignmentExpression(
        nameClone,
        initializer);
    initAssignment.Loc = parameter.Loc;
    c.AddEmitFlags(initAssignment, EFNoComments);
    var initBlock = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewBlock(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(initAssignment)])), false);
    initBlock.Loc = parameter.Loc;
    c.AddEmitFlags(initBlock, (EmitFlags)((EmitFlags)((EmitFlags)(EFSingleLine | EFNoTrailingSourceMap) | EFNoTokenSourceMaps) | EFNoComments));
    c.AddInitializationStatement(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewIfStatement(
        c.Factory.NewTypeCheck(name.Clone(new NodeFactoryжNodeFactoryCoercible(c.Factory)), "undefined"),
        initBlock,
        nil));
    return c.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
        Ꮡparameter,
        Ꮡparameter.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers(),
        parameter.DotDotDotToken,
        parameter.Name(),
        parameter.QuestionToken,
        parameter.Type,
        nil);
}

public static void AddInitializationStatement(this ref EmitContext c, ж<ast.Node> node) {
    var scope = c.varScopeStack.Peek();
    if (scope == nil) {
        throw panic("Tried to add an initialization statement without a surrounding variable scope");
    }
    c.AddEmitFlags(node, EFCustomPrologue);
    scope.initializationStatements = append(scope.initializationStatements, node);
}

public static ж<ast.Node> ConvertToFunctionBlock(this ref EmitContext c, ж<ast.Node> Ꮡnode, bool multiLine) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBlock(Ꮡnode)) {
        return Ꮡnode;
    }
    var returnStatement = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewReturnStatement(Ꮡnode);
    returnStatement.Loc = node.Loc;
    var statements = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList([returnStatement]);
    statements.Loc = node.Loc;
    var block = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewBlock(statements, multiLine);
    block.Loc = node.Loc;
    return block;
}

public static ж<astꓸBlockOrExpression> VisitFunctionBody(this ж<EmitContext> Ꮡc, ж<astꓸBlockOrExpression> node, ж<ast.NodeVisitor> Ꮡvisitor) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var visitor = ref Ꮡvisitor.DerefOrNull();

    var updated = visitor.VisitNode(node);
    var declarations = c.EndVariableEnvironment();
    if (len(declarations) == 0) {
        return updated;
    }

    if (updated == nil) {
        return c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewBlock(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(declarations), true);
    }

    if (!ast.IsBlock(updated)) {
        c.AddEmitFlags(updated, EFNoComments);
        var block = c.ConvertToFunctionBlock(updated, false);
        return c.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateBlock(
            block.AsBlock(),
            Ꮡc.MergeEnvironmentList(block.StatementList(), declarations),
            block.AsBlock().MultiLine);
    }

    return c.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateBlock(
        updated.AsBlock(),
        Ꮡc.MergeEnvironmentList(updated.StatementList(), declarations),
        updated.AsBlock().MultiLine);
}

public static ж<astꓸStatement> VisitIterationBody(this ref EmitContext c, ж<astꓸStatement> body, ж<ast.NodeVisitor> visitor) {
    if (body == nil) {
        return default;
    }

    c.StartLexicalEnvironment();
    var updated = c.VisitEmbeddedStatement(body, visitor);
    if (updated == nil) {
        throw panic("Expected visitor to return a statement.");
    }

    var statements = c.EndLexicalEnvironment();
    if (len(statements) > 0) {
        if (ast.IsBlock(updated)) {
            statements = appendꓸꓸꓸ(statements, updated.Statements());
            var statementsList = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
            statementsList.Loc = updated.StatementList().Loc;
            return c.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateBlock(updated.AsBlock(), statementsList, updated.AsBlock().MultiLine);
        }
        statements = append(statements, updated);
        return c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewBlock(c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNodeList(statements), true);
    }

    return updated;
}

public static ж<astꓸStatement> VisitEmbeddedStatement(this ref EmitContext c, ж<astꓸStatement> Ꮡnode, ж<ast.NodeVisitor> Ꮡvisitor) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var visitor = ref Ꮡvisitor.DerefOrNull();

    if (Ꮡnode == nil) {
        return default;
    }
    var embeddedStatement = visitor.VisitEmbeddedStatement(Ꮡnode);
    if (embeddedStatement == nil || ast.IsNotEmittedStatement(embeddedStatement)) {
        var emptyStatement = visitor.Factory.NewEmptyStatement();
        emptyStatement.Loc = node.Loc;
        c.SetOriginal(emptyStatement, Ꮡnode);
        c.AssignCommentRange(emptyStatement, Ꮡnode);
        return emptyStatement;
    }
    return embeddedStatement;
}

public static ж<ast.Node> SetSyntheticLeadingComments(this ref EmitContext c, ж<ast.Node> node, slice<SynthesizedComment> comments) {
    c.emitNodes.Get(node).leadingComments = comments;
    return node;
}

public static ж<ast.Node> AddSyntheticLeadingComment(this ref EmitContext c, ж<ast.Node> node, ast.Kind kind, @string text, bool hasTrailingNewLine) {
    c.emitNodes.Get(node).leadingComments = append(c.emitNodes.Get(node).leadingComments, new SynthesizedComment(Kind: kind, Loc: core.NewTextRange(-1, -1), HasTrailingNewLine: hasTrailingNewLine, Text: text));
    return node;
}

public static slice<SynthesizedComment> GetSyntheticLeadingComments(this ref EmitContext c, ж<ast.Node> node) {
    if (c.emitNodes.Has(node)) {
        return c.emitNodes.Get(node).leadingComments;
    }
    return default;
}

public static ж<ast.Node> SetSyntheticTrailingComments(this ref EmitContext c, ж<ast.Node> node, slice<SynthesizedComment> comments) {
    c.emitNodes.Get(node).trailingComments = comments;
    return node;
}

public static ж<ast.Node> AddSyntheticTrailingComment(this ref EmitContext c, ж<ast.Node> node, ast.Kind kind, @string text, bool hasTrailingNewLine) {
    c.emitNodes.Get(node).trailingComments = append(c.emitNodes.Get(node).trailingComments, new SynthesizedComment(Kind: kind, Loc: core.NewTextRange(-1, -1), HasTrailingNewLine: hasTrailingNewLine, Text: text));
    return node;
}

public static slice<SynthesizedComment> GetSyntheticTrailingComments(this ref EmitContext c, ж<ast.Node> node) {
    if (c.emitNodes.Has(node)) {
        return c.emitNodes.Get(node).trailingComments;
    }
    return default;
}

public static void SetTypeNode(this ref EmitContext c, ж<ast.Node> node, ж<astꓸTypeNode> ᏑtypeNode) {
    ref var typeNode = ref ᏑtypeNode.DerefOrNull();

    c.emitNodes.Get(node).typeNode = ᏑtypeNode;
}

public static ж<astꓸTypeNode> GetTypeNode(this ref EmitContext c, ж<ast.Node> node) {
    var emitNode = c.emitNodes.TryGet(node); if (emitNode != nil) {
        return emitNode.typeNode;
    }
    return default;
}

public static ж<astꓸStatement> NewNotEmittedStatement(this ref EmitContext c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var statement = c.Factory.of(NodeFactory.ᏑΔNodeFactory).NewNotEmittedStatement();
    statement.Loc = node.Loc;
    c.SetOriginal(statement, Ꮡnode);
    c.AssignCommentRange(statement, Ꮡnode);
    return statement;
}

} // end printerᴛEmitContext
