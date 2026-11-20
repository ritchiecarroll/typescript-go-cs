namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using fmt = fmt_package;
using iter = iter_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using modulespecifiers = go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸжastꓸNode = Span<ж<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node>>;
using static declarations_package;

partial class declarations_package {

partial struct ReferencedFilePair {
    internal ж<ast.SourceFile> @file;
    internal ж<ast.FileReference> @ref;
}

partial interface OutputPaths {
    @string DeclarationFilePath();
    @string JsFilePath();
}

partial interface DeclarationEmitHost :
    modulespecifiers.ModuleSpecifierGenerationHost
{
    @string GetCurrentDirectory();
    bool UseCaseSensitiveFileNames();
    ж<ast.SourceFile> GetSourceFileFromReference(ж<ast.SourceFile> origin, ж<ast.FileReference> @ref);
    OutputPaths GetOutputPathsFor(ж<ast.SourceFile> @file, bool forceDtsPaths);
    bool SourceFileMayBeEmitted(ж<ast.SourceFile> @file, bool forceDtsEmit);
    ast.ModifierFlags GetEffectiveDeclarationFlags(ж<ast.Node> node, ast.ModifierFlags flags);
    printer.EmitResolver GetEmitResolver();
}

partial struct thisPropertyAssignmentKey {
    internal @string name;
    internal ж<ast.Node> node;
    internal bool isStatic;
    internal bool isPrivate;
}

internal static thisPropertyAssignmentKey getThisPropertyAssignmentKey(ж<ast.Node> name, ж<ast.Node> node, bool isStatic) {
    var isPrivate = ast.IsPrivateIdentifier(name);
    if (name != nil && !ast.IsDynamicName(name)) {
        var (nameText, ok) = ast.TryGetTextOfPropertyName(name); if (ok) {
            return new thisPropertyAssignmentKey(name: nameText, isStatic: isStatic, isPrivate: isPrivate);
        }
    }
    return new thisPropertyAssignmentKey(node: node, isStatic: isStatic, isPrivate: isPrivate);
}

partial struct DeclarationTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal DeclarationEmitHost host;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ж<SymbolTrackerImpl> tracker;
    internal ж<SymbolTrackerSharedState> state;
    internal printer.EmitResolver resolver;
    internal @string declarationFilePath;
    internal @string declarationMapPath;
    internal bool needsDeclare;
    internal bool needsScopeFixMarker;
    internal bool resultHasScopeMarker;
    internal ж<ast.Node> enclosingDeclaration;
    internal bool resultHasExternalModuleIndicator;
    internal bool suppressNewDiagnosticContexts;
    internal collections.Set<@string> witnessedCjsExports;
    internal map<ast.NodeId, ж<ast.Node>> lateStatementReplacementMap;
    internal map<ast.NodeId, ж<ast.Node>> expandoHosts;
    internal map<ast.NodeId, slice<ж<ast.Node>>> expandoMembers;
    internal map<ast.NodeId, slice<ж<ast.BinaryExpression>>> deferredExpandoAssignments;
    internal collections.Set<thisPropertyAssignmentKey> seenProperties;
    internal slice<ж<ast.Node>> thisPropertyAssignmentsCollected;
    internal slice<ReferencedFilePair> rawReferencedFiles;
    internal slice<ж<ast.FileReference>> rawTypeReferenceDirectives;
    internal slice<ж<ast.FileReference>> rawLibReferenceDirectives;
    internal ж<ast.NodeVisitor> bindingNameVisitor;
    internal ж<ast.NodeVisitor> expressionVisitor;
    internal ж<ast.NodeVisitor> cjsExportAssignmentVisitor;
    internal ж<ast.NodeVisitor> exportStrippingVisitor;
    internal ж<ast.NodeVisitor> thisPropertyVisitor;
    internal ж<ast.Node> cjsExportAssignment;
    internal slice<ж<ast.Node>> cjsExportMembers;
    internal ж<ast.Node> cjsExportAssignmentName;
    internal ж<ast.NodeVisitor> declareStrippingVisitor;
    internal bool inClassExpressionDeclaration;
}

public static ж<DeclarationTransformer> NewDeclarationTransformer(DeclarationEmitHost host, ж<printer.EmitContext> context, ж<core.CompilerOptions> ᏑcompilerOptions, @string declarationFilePath, @string declarationMapPath) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    var resolver = host.GetEmitResolver();
    var state = Ꮡ(new SymbolTrackerSharedState(isolatedDeclarations: compilerOptions.IsolatedDeclarations.IsTrue(), stripInternal: compilerOptions.StripInternal.IsTrue(), resolver: resolver));
    var tracker = NewSymbolTracker(host, resolver, state);
    var tx = Ꮡ(new DeclarationTransformer(
        host: host,
        compilerOptions: ᏑcompilerOptions,
        tracker: tracker,
        state: state,
        resolver: resolver,
        declarationFilePath: declarationFilePath,
        declarationMapPath: declarationMapPath
    ));
    var resolverʗ1 = resolver;
    var txʗ1 = tx;
    tx.state.reportExpandoFunctionErrors = (ж<ast.Node> node) => {
        if (!txʗ1.state.isolatedDeclarations) {
            return;
        }
        var props = resolverʗ1.GetPropertiesOfContainerFunction(node);
        foreach (var (_, p) in range(props)) {
            if (ast.IsExpandoPropertyDeclaration(p.ValueDeclaration)) {
                var errorTarget = p.ValueDeclaration;
                if (ast.IsBinaryExpression(errorTarget)) {
                    errorTarget = errorTarget.AsBinaryExpression().Left;
                }
                txʗ1.state.addDiagnostic(createDiagnosticForNode(errorTarget, diagnostics.Assigning_properties_to_functions_without_declaring_them_is_not_supported_with_isolatedDeclarations_Add_an_explicit_declaration_for_the_properties_assigned_to_this_function));
            }
        }
    };
    tx.of(DeclarationTransformer.ᏑTransformer).NewTransformer(tx.visit, context);
    tx.bindingNameVisitor = tx.of(DeclarationTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitBindingName);
    tx.expressionVisitor = tx.of(DeclarationTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitNestedExpression);
    tx.exportStrippingVisitor = tx.of(DeclarationTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.stripExportModifiers);
    tx.thisPropertyVisitor = tx.of(DeclarationTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitThisPropertyAssignments);
    tx.cjsExportAssignmentVisitor = tx.of(DeclarationTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitCJSExportAssignments);
    tx.declareStrippingVisitor = tx.of(DeclarationTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.stripDeclareModifiers);
    return tx;
}

} partial class declarationsᴛDeclarationTransformer {

public static slice<ж<ast.Diagnostic>> GetDiagnostics(this ref DeclarationTransformer tx) {
    return tx.state.diagnostics;
}

internal static bool shouldStripInternal(this ref DeclarationTransformer tx, ж<ast.Node> node) {
    return tx.state.stripInternal && node != nil && tx.isInternalDeclaration(node, tx.state.currentSourceFile);
}

internal static bool isInternalDeclaration(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode, ж<ast.SourceFile> ᏑsourceFile) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (Ꮡnode == nil) {
        return false;
    }
    var parseTreeNode = tx.Transformer.EmitContext().MostOriginal(Ꮡnode);
    if (!ast.IsParseTreeNode(parseTreeNode)) {
        return false;
    }
    if (parseTreeNode.Kind == ast.KindParameter) {
        var @params = parseTreeNode.Parent.Parameters();
        var parseTreeNodeʗ1 = parseTreeNode;
        nint paramIdx = slices.IndexFunc(@params, (ж<astꓸParameterDeclarationNode> p) => p.AsNode() == parseTreeNodeʗ1);
        ж<ast.Node> previousSibling = default;
        if (paramIdx > 0) {
            previousSibling = @params[paramIdx - 1].AsNode();
        }

        @string text = sourceFile.Text();
        slice<ast.CommentRange> commentRanges = default;

        if (previousSibling != nil) {
            nint trailingPos = scanner.SkipTriviaEx(text, previousSibling.End() + 1, Ꮡ(new scanner.SkipTriviaOptions(StopAtComments: true)));
            foreach (var comment in range<ast.CommentRange>(scanner.GetTrailingCommentRanges(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), text, trailingPos).Invoke)) {
                commentRanges = append(commentRanges, comment);
            }
            foreach (var comment in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), text, node.Pos()).Invoke)) {
                commentRanges = append(commentRanges, comment);
            }
        } else {
            nint trailingPos = scanner.SkipTriviaEx(text, node.Pos(), Ꮡ(new scanner.SkipTriviaOptions(StopAtComments: true)));
            foreach (var comment in range<ast.CommentRange>(scanner.GetTrailingCommentRanges(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), text, trailingPos).Invoke)) {
                commentRanges = append(commentRanges, comment);
            }
        }

        if (len(commentRanges) > 0) {
            return hasInternalAnnotation(commentRanges[len(commentRanges) - 1], ᏑsourceFile);
        }
        return false;
    }

    foreach (var commentRange in range<ast.CommentRange>(tx.getLeadingCommentRangesOfNode(parseTreeNode, ᏑsourceFile).Invoke)) {
        if (hasInternalAnnotation(commentRange, ᏑsourceFile)) {
            return true;
        }
    }
    return false;
}

internal static iter.Seq<ast.CommentRange> getLeadingCommentRangesOfNode(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode, ж<ast.SourceFile> ᏑsourceFile) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (Ꮡnode == nil || node.Kind == ast.KindJsxText) {
        return default;
    }
    return scanner.GetLeadingCommentRanges(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), sourceFile.Text(), node.Pos());
}

} partial class declarations_package {

internal static bool hasInternalAnnotation(ast.CommentRange commentRange, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    @string comment = sourceFile.Text().slice(commentRange.TextRange.Pos(), commentRange.TextRange.End());
    return strings.Contains(comment, "@internal");
}

internal static nodebuilder.Flags declarationEmitNodeBuilderFlags => /* nodebuilder.FlagsMultilineObjectLiterals |
	nodebuilder.FlagsWriteClassExpressionAsTypeLiteral |
	nodebuilder.FlagsUseTypeOfFunction |
	nodebuilder.FlagsUseStructuralFallback |
	nodebuilder.FlagsAllowEmptyTuple |
	nodebuilder.FlagsGenerateNamesForShadowedTypeParams |
	nodebuilder.FlagsNoTruncation */ 531469;

internal static nodebuilder.InternalFlags declarationEmitInternalNodeBuilderFlags => /* nodebuilder.InternalFlagsAllowUnresolvedNames */ 8;

} partial class declarationsᴛDeclarationTransformer {

internal static ж<ast.Node> visit(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return default;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        return Ꮡtx.visitSourceFile(node.AsSourceFile());
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindEnumDeclaration || ᴛ == ast.KindVariableStatement || ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindJSImportDeclaration || ᴛ == ast.KindExportDeclaration || ᴛ == ast.KindExportAssignment: {
        return Ꮡtx.visitDeclarationStatements(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindBreakStatement || ᴛ == ast.KindContinueStatement || ᴛ == ast.KindDebuggerStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindEmptyStatement || ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement || ᴛ == ast.KindForStatement || ᴛ == ast.KindIfStatement || ᴛ == ast.KindLabeledStatement || ᴛ == ast.KindReturnStatement || ᴛ == ast.KindSwitchStatement || ᴛ == ast.KindThrowStatement || ᴛ == ast.KindTryStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindWithStatement || ᴛ == ast.KindNotEmittedStatement || ᴛ == ast.KindBlock || ᴛ == ast.KindMissingDeclaration || ᴛ == ast.KindExpressionStatement: {
        return default;
    }
    default: {
        return Ꮡtx.visitDeclarationSubtree(Ꮡnode);
    }}

}

} partial class declarations_package {

internal static ж<SymbolAccessibilityDiagnostic> throwDiagnostic(printer.SymbolAccessibilityResult result) {
    throw panic("Diagnostic emitted without context");
}

} partial class declarationsᴛDeclarationTransformer {

internal static ж<ast.Node> visitSourceFile(this ж<DeclarationTransformer> Ꮡtx, ж<ast.SourceFile> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    tx.cjsExportAssignmentName = default;
    if (node.IsDeclarationFile) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    tx.needsDeclare = true;
    tx.needsScopeFixMarker = false;
    tx.resultHasScopeMarker = false;
    tx.enclosingDeclaration = Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    tx.state.getSymbolAccessibilityDiagnostic = throwDiagnostic;
    tx.resultHasExternalModuleIndicator = false;
    tx.suppressNewDiagnosticContexts = false;
    tx.state.lateMarkedStatements = new slice<ж<ast.Node>>(0);
    tx.lateStatementReplacementMap = new map<ast.NodeId, ж<ast.Node>>();
    tx.expandoHosts = new map<ast.NodeId, ж<ast.Node>>();
    tx.expandoMembers = new map<ast.NodeId, slice<ж<ast.Node>>>();
    tx.deferredExpandoAssignments = new map<ast.NodeId, slice<ж<ast.BinaryExpression>>>();
    tx.rawReferencedFiles = new slice<ReferencedFilePair>(0);
    tx.rawTypeReferenceDirectives = new slice<ж<ast.FileReference>>(0);
    tx.rawLibReferenceDirectives = new slice<ж<ast.FileReference>>(0);
    Ꮡtx.ᏑwitnessedCjsExports.Clear();
    tx.state.currentSourceFile = Ꮡnode;
    tx.collectFileReferences(Ꮡnode);
    tx.resolver.PrecalculateDeclarationEmitVisibility(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).AsSourceFile());
    var updated = Ꮡtx.transformSourceFile(Ꮡnode);
    tx.state.currentSourceFile = default;
    return updated;
}

internal static void collectFileReferences(this ref DeclarationTransformer tx, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    tx.rawReferencedFiles = appendꓸꓸꓸ(tx.rawReferencedFiles, core.Map(sourceFile.ReferencedFiles, ReferencedFilePair (ж<ast.FileReference> @ref) => new ReferencedFilePair(@file: ᏑsourceFile, @ref: @ref)));
    tx.rawTypeReferenceDirectives = appendꓸꓸꓸ(tx.rawTypeReferenceDirectives, sourceFile.TypeReferenceDirectives);
    tx.rawLibReferenceDirectives = appendꓸꓸꓸ(tx.rawLibReferenceDirectives, sourceFile.LibReferenceDirectives);
}

} partial class declarations_package {

internal static slice<ж<ast.Node>> nodeOrSyntaxListChildren(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsSyntaxList(Ꮡnode)) {
        return (~node.AsSyntaxList()).Children;
    }
    return [Ꮡnode];
}

internal static slice<ж<ast.Node>> flattenSyntaxLists(slice<ж<ast.Node>> nodes) {
    return core.FlatMap<ж<ast.Node>, ж<ast.Node>>(nodes, nodeOrSyntaxListChildren);
}

} partial class declarationsᴛDeclarationTransformer {

internal static ж<astꓸStatementList> appendCjsExports(this ref DeclarationTransformer tx, ж<astꓸStatementList> ᏑcombinedStatements) {
    ref var combinedStatements = ref ᏑcombinedStatements.DerefOrNull();
    var resultᴷ = new stackAppend<ж<ast.Node>>();

    var result = slice<ж<ast.Node>>([]);
    if (tx.cjsExportAssignment != nil) {
        result = append(resultᴷ, result, tx.cjsExportAssignment);
    }
    result = appendꓸꓸꓸ(result, tx.cjsExportMembers);
    result = appendꓸꓸꓸ(result, combinedStatements.Nodes);
    var statementNodes = flattenSyntaxLists(result);
    if (len(statementNodes) != len(combinedStatements.Nodes)) {
        ᏑcombinedStatements = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statementNodes); combinedStatements = ref ᏑcombinedStatements.DerefOrNull();
    }
    return ᏑcombinedStatements;
}

internal static ж<ast.Node> transformSourceFile(this ж<DeclarationTransformer> Ꮡtx, ж<ast.SourceFile> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        tx.cjsExportAssignment = default;
        tx.cjsExportAssignmentName = default;
        tx.cjsExportMembers = default;
        defer(() => {
            Ꮡtx.Value.cjsExportAssignment = default;
            Ꮡtx.Value.cjsExportAssignmentName = default;
            Ꮡtx.Value.cjsExportMembers = default;
        }, ref ᒐ);
        tx.cjsExportAssignmentVisitor.VisitNode(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.expressionVisitor.VisitNode(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        ж<astꓸStatementList> combinedStatements = default;
        var statements = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitNodes(node.Statements);
        combinedStatements = Ꮡtx.transformAndReplaceLatePaintedStatements(statements);
        combinedStatements = tx.appendCjsExports(combinedStatements);
        combinedStatements.Loc = statements.Loc;
        if (ast.IsExternalOrCommonJSModule(Ꮡnode)) {
            if (ast.IsInJSFile(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
                var exportEquals = node.Symbol.Exports[ast.InternalSymbolNameExportEquals]; if (exportEquals != nil && len(exportEquals.Declarations) > 1) {
                    foreach (var (_, nodeΔ1) in range(exportEquals.Declarations)) {
                        tx.state.addDiagnostic(createDiagnosticForNode(nodeΔ1, diagnostics.Multiple_module_exports_assignments_cannot_be_serialized_for_declaration_emit));
                    }
                }
            }
            if (!tx.resultHasExternalModuleIndicator || (tx.needsScopeFixMarker && !tx.resultHasScopeMarker)) {
                var marker = createEmptyExports(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory());
                var newList = append(combinedStatements.Nodes, marker);
                var withMarker = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newList);
                withMarker.Loc = combinedStatements.Loc;
                combinedStatements = withMarker;
            }
        }
        @string outputFilePath = tspath.GetDirectoryPath(tspath.NormalizeSlashes(tx.declarationFilePath));
        var result = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(Ꮡnode, combinedStatements, node.EndOfFileToken);
        result.AsSourceFile().LibReferenceDirectives = tx.getLibReferences();
        result.AsSourceFile().TypeReferenceDirectives = tx.getTypeReferences();
        result.AsSourceFile().IsDeclarationFile = true;
        result.AsSourceFile().ReferencedFiles = tx.getReferencedFiles(outputFilePath);
        return result.AsNode();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class declarations_package {

internal static ж<ast.Node> createEmptyExports(ж<ast.NodeFactory> Ꮡfactory) {
    ref var factory = ref Ꮡfactory.DerefOrNull();

    return factory.NewExportDeclaration(nil, false, factory.NewNamedExports(factory.NewNodeList([])), nil, nil);
}

} partial class declarationsᴛDeclarationTransformer {

internal static ж<astꓸStatementList> transformAndReplaceLatePaintedStatements(this ж<DeclarationTransformer> Ꮡtx, ж<astꓸStatementList> Ꮡstatements) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var statements = ref Ꮡstatements.DerefOrNull();

    while (true) {
        if (len(tx.state.lateMarkedStatements) == 0) {
            break;
        }

        var next = tx.state.lateMarkedStatements[0];
        tx.state.lateMarkedStatements = tx.state.lateMarkedStatements[1..];

        var saveNeedsDeclare = tx.needsDeclare;
        tx.needsDeclare = next.Parent != nil && ast.IsSourceFile(next.Parent);

        var result = Ꮡtx.transformTopLevelDeclaration(next);

        tx.needsDeclare = saveNeedsDeclare;
        var original = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().MostOriginal(next);
        var id = ast.GetNodeId(original);
        tx.lateStatementReplacementMap[id] = result;
    }

    var results = new slice<ж<ast.Node>>(0, len(statements.Nodes));
    foreach (var (_, statement) in range(statements.Nodes)) {
        if (!ast.IsLateVisibilityPaintedStatement(statement)) {
            results = append(results, statement);
            continue;
        }
        var original = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().MostOriginal(statement);
        var id = ast.GetNodeId(original);
        var (replacement, ok) = tx.lateStatementReplacementMap[id, ꟷ];
        if (!ok) {
            results = append(results, statement);
            continue;
        }
        if (replacement == nil) {
            continue;
        }
        if (replacement.Kind == ast.KindSyntaxList) {
            if (!tx.needsScopeFixMarker || !tx.resultHasExternalModuleIndicator) {
                foreach (var (_, elem) in range((~replacement.AsSyntaxList()).Children)) {
                    if (needsScopeMarker(elem)) {
                        tx.needsScopeFixMarker = true;
                    }
                    if (ast.IsSourceFile(statement.Parent) && ast.IsExternalModuleIndicator(elem)) {
                        tx.resultHasExternalModuleIndicator = true;
                    }
                }
            }
            results = appendꓸꓸꓸ(results, (~replacement.AsSyntaxList()).Children);
        } else {
            if (needsScopeMarker(replacement)) {
                tx.needsScopeFixMarker = true;
            }
            if (ast.IsSourceFile(statement.Parent) && ast.IsExternalModuleIndicator(replacement)) {
                tx.resultHasExternalModuleIndicator = true;
            }
            results = append(results, replacement);
        }
    }

    return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(results);
}

internal static slice<ж<ast.FileReference>> /*results*/ getReferencedFiles(this ref DeclarationTransformer tx, @string outputFilePath) {
    slice<ж<ast.FileReference>> results = default;
    var resultsᴷ = new stackAppend<ж<ast.FileReference>>(moves: true);

    foreach (var (_, pair) in range(tx.rawReferencedFiles)) {
        var sourceFile = pair.@file;
        var @ref = pair.@ref;

        if (!@ref.Preserve) {
            continue;
        }

        var @file = tx.host.GetSourceFileFromReference(sourceFile, @ref);
        if (@file == nil) {
            continue;
        }

        @string declFileName = default;
        if (@file.IsDeclarationFile) {
            declFileName = @file.FileName();
        } else {
            var paths = tx.host.GetOutputPathsFor(@file, true);
            declFileName = paths.DeclarationFilePath();
            if (len(declFileName) == 0) {
                declFileName = paths.JsFilePath();
            }
            if (len(declFileName) == 0) {
                declFileName = @file.FileName();
            }
        }
        if (len(declFileName) == 0) {
            continue;
        }

        ref var fileName = ref heap<@string>(tspath.GetRelativePathToDirectoryOrUrl(
            outputFilePath,
            declFileName,
            false,
            new tspath.ComparePathsOptions(
                CurrentDirectory: tx.host.GetCurrentDirectory(),
                UseCaseSensitiveFileNames: tx.host.UseCaseSensitiveFileNames()
            )), out var ᏑfileName);

        results = append(resultsᴷ, results, Ꮡ(new ast.FileReference(
            TextRange: core.NewTextRange(-1, -1),
            FileName: fileName,
            ResolutionMode: @ref.ResolutionMode,
            Preserve: @ref.Preserve
        )));
    }
    results = move2heap(resultsᴷ, results);
    return results;
}

internal static slice<ж<ast.FileReference>> /*result*/ getLibReferences(this ref DeclarationTransformer tx) {
    slice<ж<ast.FileReference>> result = default;

    foreach (var (_, @ref) in range(tx.rawLibReferenceDirectives)) {
        if (!@ref.Preserve) {
            continue;
        }
        result = append(result, Ꮡ(new ast.FileReference(
            TextRange: core.NewTextRange(-1, -1),
            FileName: @ref.FileName,
            ResolutionMode: @ref.ResolutionMode,
            Preserve: @ref.Preserve
        )));
    }
    return result;
}

internal static slice<ж<ast.FileReference>> /*result*/ getTypeReferences(this ref DeclarationTransformer tx) {
    slice<ж<ast.FileReference>> result = default;

    foreach (var (_, @ref) in range(tx.rawTypeReferenceDirectives)) {
        if (!@ref.Preserve) {
            continue;
        }
        result = append(result, Ꮡ(new ast.FileReference(
            TextRange: core.NewTextRange(-1, -1),
            FileName: @ref.FileName,
            ResolutionMode: @ref.ResolutionMode,
            Preserve: @ref.Preserve
        )));
    }
    return result;
}

internal static (bool, Action) setupDiagnosticContext(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡinput) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var input = ref Ꮡinput.DerefOrNull();

    var canProduceDiagnostic = canProduceDiagnostics(Ꮡinput);
    var oldWithinObjectLiteralType = tx.suppressNewDiagnosticContexts;
    var shouldEnterSuppressNewDiagnosticsContextContext = (input.Kind == ast.KindTypeLiteral || input.Kind == ast.KindMappedType) && !(input.Parent.Kind == ast.KindTypeAliasDeclaration || input.Parent.Kind == ast.KindJSTypeAliasDeclaration);

    var oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
    if (canProduceDiagnostic && !tx.suppressNewDiagnosticContexts) {
        tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNode(Ꮡinput);
    }
    var oldName = tx.state.errorNameNode;

    if (shouldEnterSuppressNewDiagnosticsContextContext) {
        tx.suppressNewDiagnosticContexts = true;
    }

    var oldDiagʗ1 = oldDiag;
    var oldNameʗ1 = oldName;
    return (canProduceDiagnostic, () => {
        Ꮡtx.Value.state.getSymbolAccessibilityDiagnostic = oldDiagʗ1;
        Ꮡtx.Value.state.errorNameNode = oldNameʗ1;
        Ꮡtx.Value.suppressNewDiagnosticContexts = oldWithinObjectLiteralType;
    });
}

internal static ж<ast.Node> visitDeclarationSubtree(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡinput) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var input = ref Ꮡinput.DerefOrNull();

        if (tx.shouldStripInternal(Ꮡinput)) {
            return default;
        }
        if (ast.IsDeclaration(Ꮡinput)) {
            if (isDeclarationAndNotVisible(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext(), tx.resolver, Ꮡinput)) {
                return default;
            }
            if (ast.HasDynamicName(Ꮡinput)) {
                if (tx.state.isolatedDeclarations) {
                    if (!tx.resolver.IsDefinitelyReferenceToGlobalSymbolObject(input.Name().Expression())) {
                        if (ast.IsClassDeclaration(input.Parent) || ast.IsObjectLiteralExpression(input.Parent)) {
                            tx.state.addDiagnostic(createDiagnosticForNode(Ꮡinput, diagnostics.Computed_property_names_on_class_or_object_literals_cannot_be_inferred_with_isolatedDeclarations));
                            return default;
                        } else if ((ast.IsInterfaceDeclaration(input.Parent) || ast.IsTypeLiteralNode(input.Parent)) && !ast.IsEntityNameExpression(input.Name().Expression())) {
                            tx.state.addDiagnostic(createDiagnosticForNode(Ꮡinput, diagnostics.Computed_properties_must_be_number_or_string_literals_variables_or_dotted_expressions_with_isolatedDeclarations));
                            return default;
                        }
                    }
                } else if (!tx.resolver.IsLateBound(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().ParseNode(Ꮡinput)) || !ast.IsEntityNameExpression(input.Name().Expression())) {
                    return default;
                }
            }
        }

        if (ast.IsFunctionLike(Ꮡinput) && tx.resolver.IsImplementationOfOverload(Ꮡinput)) {
            return default;
        }

        if (input.Kind == ast.KindSemicolonClassElement) {
            return default;
        }

        if (ast.IsHeritageClause(Ꮡinput) && (len(input.AsHeritageClause().Types.Nodes) == 0 || (len(input.AsHeritageClause().Types.Nodes) == 1 && ast.NodeIsMissing(input.AsHeritageClause().Types.Nodes[0])))) {
            return default;
        }

        var previousEnclosingDeclaration = tx.enclosingDeclaration;
        if (isEnclosingDeclaration(Ꮡinput)) {
            tx.enclosingDeclaration = Ꮡinput;
        }

        var (canProduceDiagnostic, cleanupDiagnosticContext) = Ꮡtx.setupDiagnosticContext(Ꮡinput);
        var cleanupDiagnosticContextʗ1 = cleanupDiagnosticContext;
        defer(cleanupDiagnosticContextʗ1, ref ᒐ);

        ж<ast.Node> result = default;

        switch (input.Kind) {
        case var ᴛ when ᴛ == ast.KindMappedType: {
            result = tx.transformMappedTypeNode(input.AsMappedTypeNode());
            break;
        }
        case var ᴛ when ᴛ == ast.KindHeritageClause: {
            result = tx.transformHeritageClause(input.AsHeritageClause());
            break;
        }
        case var ᴛ when ᴛ == ast.KindMethodSignature: {
            result = tx.transformMethodSignatureDeclaration(input.AsMethodSignatureDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
            result = tx.transformMethodDeclaration(input.AsMethodDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindConstructSignature: {
            result = tx.transformConstructSignatureDeclaration(input.AsConstructSignatureDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindConstructor: {
            result = tx.transformConstructorDeclaration(input.AsConstructorDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindGetAccessor: {
            result = tx.transformGetAccesorDeclaration(input.AsGetAccessorDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindSetAccessor: {
            result = tx.transformSetAccessorDeclaration(input.AsSetAccessorDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
            result = tx.transformPropertyDeclaration(input.AsPropertyDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindPropertySignature: {
            result = tx.transformPropertySignatureDeclaration(input.AsPropertySignatureDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindCallSignature: {
            result = tx.transformCallSignatureDeclaration(input.AsCallSignatureDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindIndexSignature: {
            result = tx.transformIndexSignatureDeclaration(input.AsIndexSignatureDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
            result = tx.transformVariableDeclaration(input.AsVariableDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindTypeParameter: {
            result = tx.transformTypeParameterDeclaration(input.AsTypeParameterDeclaration());
            break;
        }
        case var ᴛ when ᴛ == ast.KindExpressionWithTypeArguments: {
            result = tx.transformExpressionWithTypeArguments(input.AsExpressionWithTypeArguments());
            break;
        }
        case var ᴛ when ᴛ == ast.KindTypeReference: {
            result = tx.transformTypeReference(input.AsTypeReferenceNode());
            break;
        }
        case var ᴛ when ᴛ == ast.KindConditionalType: {
            result = tx.transformConditionalTypeNode(input.AsConditionalTypeNode());
            break;
        }
        case var ᴛ when ᴛ == ast.KindFunctionType: {
            result = tx.transformFunctionTypeNode(input.AsFunctionTypeNode());
            break;
        }
        case var ᴛ when ᴛ == ast.KindConstructorType: {
            result = tx.transformConstructorTypeNode(input.AsConstructorTypeNode());
            break;
        }
        case var ᴛ when ᴛ == ast.KindImportType: {
            result = tx.transformImportTypeNode(input.AsImportTypeNode());
            break;
        }
        case var ᴛ when ᴛ == ast.KindTypeQuery: {
            tx.checkEntityNameVisibility(input.AsTypeQueryNode().ExprName, tx.enclosingDeclaration);
            result = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡinput);
            break;
        }
        case var ᴛ when ᴛ == ast.KindQualifiedName: {
            if (input.AsQualifiedName().Right.Kind == ast.KindPrivateIdentifier) {
                tx.state.addDiagnostic(createDiagnosticForNode(Ꮡinput, diagnostics.Declaration_emit_elides_private_members_but_0_refers_to_a_private_member_Write_an_explicit_type_here, input.AsQualifiedName().Right.Text()));
            }
            result = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡinput);
            break;
        }
        case var ᴛ when ᴛ == ast.KindTupleType: {
            result = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡinput);
            if (result != nil) {
                if (transformers.IsOriginalNodeSingleLine(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext(), Ꮡinput)) {
                    Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().AddEmitFlags(result, printer.EFSingleLine);
                }
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocTypeExpression: {
            result = tx.transformJSDocTypeExpression(input.AsJSDocTypeExpression());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocTypeLiteral: {
            result = tx.transformJSDocTypeLiteral(input.AsJSDocTypeLiteral());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocPropertyTag: {
            result = tx.transformJSDocPropertyTag(input.AsJSDocParameterOrPropertyTag());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocAllType: {
            result = tx.transformJSDocAllType(input.AsJSDocAllType());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocNullableType: {
            result = tx.transformJSDocNullableType(input.AsJSDocNullableType());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocNonNullableType: {
            result = tx.transformJSDocNonNullableType(input.AsJSDocNonNullableType());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocOptionalType: {
            result = tx.transformJSDocOptionalType(input.AsJSDocOptionalType());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocVariadicType: {
            result = tx.transformJSDocVariadicType(input.AsJSDocVariadicType());
            break;
        }
        default: {
            result = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡinput);
            break;
        }}

        if (result != nil && canProduceDiagnostic && ast.HasDynamicName(Ꮡinput)) {
            tx.checkName(Ꮡinput);
        }

        tx.enclosingDeclaration = previousEnclosingDeclaration;
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static void checkName(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
    if (!tx.suppressNewDiagnosticContexts) {
        tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNodeName(Ꮡnode);
    }
    tx.state.errorNameNode = node.Name();
    debug.Assert(ast.HasDynamicName(Ꮡnode));
    var entityName = node.Name().Expression();
    tx.checkEntityNameVisibility(entityName, tx.enclosingDeclaration);
    if (!tx.suppressNewDiagnosticContexts) {
        tx.state.getSymbolAccessibilityDiagnostic = oldDiag;
    }
    tx.state.errorNameNode = default;
}

internal static ж<ast.Node> transformMappedTypeNode(this ref DeclarationTransformer tx, ж<ast.MappedTypeNode> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    ж<ast.Node> typeNode = default;
    if (input.Type == nil) {
        typeNode = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
    } else {
        typeNode = tx.Transformer.Visitor().Visit(input.Type);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMappedTypeNode(
        Ꮡinput,
        input.ReadonlyToken,
        tx.Transformer.Visitor().Visit(input.TypeParameter),
        tx.Transformer.Visitor().Visit(input.NameType),
        input.QuestionToken,
        typeNode,
        nil);
}

internal static ж<ast.Node> transformHeritageClause(this ref DeclarationTransformer tx, ж<ast.HeritageClause> Ꮡclause) {
    ref var clause = ref Ꮡclause.DerefOrNull();

    var retainedClauses = core.Filter(clause.Types.Nodes, (ж<astꓸHeritageClauseElement> t) => {
        var name = ast.GetHeritageClauseElementName(t);
        return ast.IsEntityName(name) || ast.IsEntityNameExpression(name) || (Ꮡclause.Value.Token == ast.KindExtendsKeyword && ast.IsExpressionWithTypeArguments(t) && t.Expression().Kind == ast.KindNullKeyword);
    });
    if (len(retainedClauses) == 0) {
        return default;
    }
    if (len(retainedClauses) == len(clause.Types.Nodes)) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡclause.of(ast.HeritageClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateHeritageClause(
        Ꮡclause,
        clause.Token,
        tx.Transformer.Visitor().VisitNodes(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(retainedClauses)));
}

internal static ж<ast.Node> transformImportTypeNode(this ref DeclarationTransformer tx, ж<ast.ImportTypeNode> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (!ast.IsLiteralImportTypeNode(Ꮡinput.of(ast.ImportTypeNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return Ꮡinput.of(ast.ImportTypeNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportTypeNode(
        Ꮡinput,
        input.IsTypeOf,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateLiteralTypeNode(
            input.Argument.AsLiteralTypeNode(),
            tx.rewriteModuleSpecifier(Ꮡinput.of(ast.ImportTypeNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Argument.AsLiteralTypeNode().Literal)),
        input.Attributes,
        input.Qualifier,
        tx.Transformer.Visitor().VisitNodes(input.TypeArguments));
}

internal static ж<ast.Node> transformConstructorTypeNode(this ref DeclarationTransformer tx, ж<ast.ConstructorTypeNode> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorTypeNode(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        tx.Transformer.Visitor().VisitNodes(input.TypeParameters),
        tx.updateParamList(Ꮡinput.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
        tx.Transformer.Visitor().Visit(input.Type));
}

internal static ж<ast.Node> transformFunctionTypeNode(this ref DeclarationTransformer tx, ж<ast.FunctionTypeNode> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionTypeNode(
        Ꮡinput,
        tx.Transformer.Visitor().VisitNodes(input.TypeParameters),
        tx.updateParamList(Ꮡinput.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
        tx.Transformer.Visitor().Visit(input.Type));
}

internal static ж<ast.Node> transformConditionalTypeNode(this ref DeclarationTransformer tx, ж<ast.ConditionalTypeNode> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var checkType = tx.Transformer.Visitor().Visit(input.CheckType);
    var extendsType = tx.Transformer.Visitor().Visit(input.ExtendsType);
    var oldEnclosingDecl = tx.enclosingDeclaration;
    tx.enclosingDeclaration = input.TrueType;
    var trueType = tx.Transformer.Visitor().Visit(input.TrueType);
    tx.enclosingDeclaration = oldEnclosingDecl;
    var falseType = tx.Transformer.Visitor().Visit(input.FalseType);

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConditionalTypeNode(
        Ꮡinput,
        checkType,
        extendsType,
        trueType,
        falseType);
}

internal static ж<ast.Node> transformTypeReference(this ref DeclarationTransformer tx, ж<ast.TypeReferenceNode> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    tx.checkEntityNameVisibility(input.TypeName, tx.enclosingDeclaration);
    return tx.Transformer.Visitor().VisitEachChild(Ꮡinput.of(ast.TypeReferenceNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> transformExpressionWithTypeArguments(this ref DeclarationTransformer tx, ж<ast.ExpressionWithTypeArguments> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (ast.IsEntityName(input.Expression) || ast.IsEntityNameExpression(input.Expression)) {
        tx.checkEntityNameVisibility(input.Expression, tx.enclosingDeclaration);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡinput.of(ast.ExpressionWithTypeArguments.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> transformTypeParameterDeclaration(this ref DeclarationTransformer tx, ж<ast.TypeParameterDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (isPrivateMethodTypeParameter(tx.host, Ꮡinput) && (input.DefaultType != nil || input.Constraint != nil)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTypeParameterDeclaration(
            Ꮡinput,
            Ꮡinput.of(ast.TypeParameterDeclaration.ᏑModifiersBase).Modifiers(),
            input.Name(),
            nil,
            input.Expression,
            nil);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡinput.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> transformVariableDeclaration(this ref DeclarationTransformer tx, ж<ast.VariableDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (tx.state.currentSourceFile.CommonJSModuleIndicator != nil && ast.IsVariableDeclarationInitializedToRequire(Ꮡinput.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return tx.transformCjsRequireVariableDeclaration(Ꮡinput);
    }
    if (ast.IsBindingPattern(input.Name()) && hasAnyBindingInitializers(ref (input.Name().AsBindingPattern()).DerefOrNull())) {
        return tx.recreateBindingPattern(input.Name().AsBindingPattern());
    }
    tx.suppressNewDiagnosticContexts = true;
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(
        Ꮡinput,
        tx.bindingNameVisitor.VisitNode(input.Name()),
        nil,
        tx.ensureType(Ꮡinput.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false),
        tx.ensureNoInitializer(Ꮡinput.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
}

} partial class declarations_package {

internal static bool hasAnyBindingInitializers(ref ast.BindingPattern bindingPattern) {
    foreach (var (_, elem) in range(bindingPattern.Elements.Nodes)) {
        if (!ast.IsBindingElement(elem)) {
            continue;
        }
        var e = elem.AsBindingElement();
        if ((~e).Initializer != nil) {
            return true;
        }
        if (e.Name() != nil && ast.IsBindingPattern(e.Name()) && hasAnyBindingInitializers(ref (e.Name().AsBindingPattern()).DerefOrNull())) {
            return true;
        }
    }
    return false;
}

} partial class declarationsᴛDeclarationTransformer {

internal static ж<ast.Node> transformCjsRequireVariableDeclaration(this ref DeclarationTransformer tx, ж<ast.VariableDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var specifier = tx.rewriteModuleSpecifier(Ꮡinput.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), (~input.Initializer.AsCallExpression()).Arguments.Nodes[0]);
    if (ast.IsIdentifier(input.Name())) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportEqualsDeclaration(nil, false, input.Name(), tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExternalModuleReference(specifier));
    } else if (ast.IsArrayBindingPattern(input.Name())) {
        return default;
    } else {
        var b = input.Name().AsBindingPattern();
        slice<ж<ast.Node>> importSpecifiers = default;
        foreach (var (_, elem) in range((~b).Elements.Nodes)) {
            if (!ast.IsIdentifier(elem.Name())) {
                continue;
            }
            importSpecifiers = append(importSpecifiers, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportSpecifier(false, elem.PropertyName(), elem.Name()));
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportDeclaration(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportClause(
                ast.KindUnknown,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedImports(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(importSpecifiers))),
            specifier,
            nil);
    }
}

internal static ж<ast.Node> recreateBindingPattern(this ref DeclarationTransformer tx, ж<ast.BindingPattern> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    slice<ж<ast.Node>> results = default;
    foreach (var (_, elem) in range(input.Elements.Nodes)) {
        var result = tx.recreateBindingElement(elem.AsBindingElement());
        if (result == nil) {
            continue;
        }
        if (result.Kind == ast.KindSyntaxList) {
            results = appendꓸꓸꓸ(results, (~result.AsSyntaxList()).Children);
        } else {
            results = append(results, result);
        }
    }
    if (len(results) == 0) {
        return default;
    }
    if (len(results) == 1) {
        return results[0];
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(results);
}

internal static ж<ast.Node> recreateBindingElement(this ref DeclarationTransformer tx, ж<ast.BindingElement> Ꮡe) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (e.Name() == nil) {
        return default;
    }
    if (!getBindingNameVisible(tx.resolver, Ꮡe.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return default;
    }
    if (ast.IsBindingPattern(e.Name())) {
        return tx.recreateBindingPattern(e.Name().AsBindingPattern());
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
        e.Name(),
        nil,
        tx.ensureType(Ꮡe.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false),
        nil);
}

internal static ж<ast.Node> transformIndexSignatureDeclaration(this ref DeclarationTransformer tx, ж<ast.IndexSignatureDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var t = tx.Transformer.Visitor().Visit(input.Type);
    if (t == nil) {
        t = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateIndexSignatureDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        tx.updateParamList(Ꮡinput.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
        t);
}

internal static ж<ast.Node> transformCallSignatureDeclaration(this ref DeclarationTransformer tx, ж<ast.CallSignatureDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallSignatureDeclaration(
        Ꮡinput,
        tx.ensureTypeParams(Ꮡinput.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.TypeParameters),
        tx.updateParamList(Ꮡinput.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
        tx.ensureType(Ꮡinput.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false));
}

internal static ж<ast.Node> transformPropertySignatureDeclaration(this ref DeclarationTransformer tx, ж<ast.PropertySignatureDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (ast.IsPrivateIdentifier(input.Name())) {
        return default;
    }
    var result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertySignatureDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        input.Name(),
        input.PostfixToken,
        tx.ensureType(Ꮡinput.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false),
        tx.ensureNoInitializer(Ꮡinput.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    tx.preservePartialJsDoc(result, Ꮡinput.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return result;
}

internal static ж<ast.Node> transformPropertyDeclaration(this ref DeclarationTransformer tx, ж<ast.PropertyDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (ast.IsPrivateIdentifier(input.Name())) {
        return default;
    }
    var postfixToken = input.PostfixToken;
    if (postfixToken != nil && postfixToken.Kind == ast.KindExclamationToken) {
        postfixToken = default;
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        input.Name(),
        postfixToken,
        tx.ensureType(Ꮡinput.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false),
        tx.ensureNoInitializer(Ꮡinput.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
}

internal static ж<ast.Node> transformSetAccessorDeclaration(this ref DeclarationTransformer tx, ж<ast.SetAccessorDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (ast.IsPrivateIdentifier(input.Name())) {
        return default;
    }

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        input.Name(),
        nil,
        tx.updateAccessorParamList(Ꮡinput.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.host.GetEffectiveDeclarationFlags(tx.Transformer.EmitContext().ParseNode(Ꮡinput.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), ast.ModifierFlagsPrivate) != 0),
        nil,
        nil,
        nil);
}

internal static ж<ast.Node> transformGetAccesorDeclaration(this ref DeclarationTransformer tx, ж<ast.GetAccessorDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (ast.IsPrivateIdentifier(input.Name())) {
        return default;
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        input.Name(),
        nil,
        tx.updateAccessorParamList(Ꮡinput.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.host.GetEffectiveDeclarationFlags(tx.Transformer.EmitContext().ParseNode(Ꮡinput.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), ast.ModifierFlagsPrivate) != 0),
        tx.ensureType(Ꮡinput.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false),
        nil,
        nil);
}

internal static ж<astꓸParameterList> updateAccessorParamList(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡinput, bool isPrivate) {
    ref var input = ref Ꮡinput.DerefOrNull();

    slice<ж<ast.Node>> newParams = default;
    if (!isPrivate) {
        var thisParam = ast.GetThisParameter(Ꮡinput);
        if (thisParam != nil) {
            newParams = append(newParams, tx.ensureParameter(thisParam.AsParameterDeclaration()));
        }
    }
    if (ast.IsSetAccessorDeclaration(Ꮡinput)) {
        ж<ast.Node> valueParam = default;
        if (!isPrivate) {
            if (len(newParams) == 1 && len((~input.AsSetAccessorDeclaration()).Parameters.Nodes) >= 2) {
                valueParam = tx.ensureParameter((~input.AsSetAccessorDeclaration()).Parameters.Nodes[1].AsParameterDeclaration());
            } else if (len(newParams) == 0 && len((~input.AsSetAccessorDeclaration()).Parameters.Nodes) >= 1) {
                valueParam = tx.ensureParameter((~input.AsSetAccessorDeclaration()).Parameters.Nodes[0].AsParameterDeclaration());
            }
        }
        if (valueParam == nil) {
            ж<ast.Node> t = default;
            if (!isPrivate) {
                t = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
            }
            valueParam = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"),
                nil,
                t,
                nil);
        }
        newParams = append(newParams, valueParam);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newParams);
}

internal static ж<ast.Node> transformConstructorDeclaration(this ref DeclarationTransformer tx, ж<ast.ConstructorDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        nil,
        tx.updateParamList(Ꮡinput.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
        nil,
        nil,
        nil);
}

internal static ж<ast.Node> transformConstructSignatureDeclaration(this ref DeclarationTransformer tx, ж<ast.ConstructSignatureDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructSignatureDeclaration(
        Ꮡinput,
        tx.ensureTypeParams(Ꮡinput.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.TypeParameters),
        tx.updateParamList(Ꮡinput.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
        tx.ensureType(Ꮡinput.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false));
}

internal static ж<ast.Node> omitPrivateMethodType(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (input.Symbol() != nil && len(input.Symbol().Declarations) > 0 && input.Symbol().Declarations[0] != Ꮡinput) {
        return default;
    }
    ж<ast.Node> result = default;
    if (ast.IsMethodSignatureDeclaration(Ꮡinput)) {
        result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertySignatureDeclaration(
            tx.ensureModifiers(Ꮡinput),
            input.Name(),
            nil,
            nil,
            nil);
    } else {
        result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(
            tx.ensureModifiers(Ꮡinput),
            input.Name(),
            nil,
            nil,
            nil);
    }
    tx.preserveJsDoc(result, Ꮡinput);
    return result;
}

internal static ж<ast.Node> transformMethodSignatureDeclaration(this ref DeclarationTransformer tx, ж<ast.MethodSignatureDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (tx.host.GetEffectiveDeclarationFlags(tx.Transformer.EmitContext().ParseNode(Ꮡinput.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), ast.ModifierFlagsPrivate) != 0) {
        return tx.omitPrivateMethodType(Ꮡinput.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    } else if (ast.IsPrivateIdentifier(input.Name())) {
        return default;
    } else {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodSignatureDeclaration(
            Ꮡinput,
            tx.ensureModifiers(Ꮡinput.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            input.Name(),
            input.PostfixToken,
            tx.ensureTypeParams(Ꮡinput.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.TypeParameters),
            tx.updateParamList(Ꮡinput.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
            tx.ensureType(Ꮡinput.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false));
    }
}

internal static ж<ast.Node> transformMethodDeclaration(this ref DeclarationTransformer tx, ж<ast.MethodDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (tx.host.GetEffectiveDeclarationFlags(tx.Transformer.EmitContext().ParseNode(Ꮡinput.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), ast.ModifierFlagsPrivate) != 0) {
        return tx.omitPrivateMethodType(Ꮡinput.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    } else if (ast.IsPrivateIdentifier(input.Name())) {
        return default;
    } else {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(
            Ꮡinput,
            tx.ensureModifiers(Ꮡinput.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            input.Name(),
            input.PostfixToken,
            tx.ensureTypeParams(Ꮡinput.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.TypeParameters),
            tx.updateParamList(Ꮡinput.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
            tx.ensureType(Ꮡinput.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false),
            nil,
            nil);
    }
}

internal static ж<ast.Node> visitDeclarationStatements(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡinput) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var input = ref Ꮡinput.DerefOrNull();

    if (tx.shouldStripInternal(Ꮡinput)) {
        return default;
    }
    switch (input.Kind) {
    case var ᴛ when ᴛ == ast.KindExportDeclaration: {
        if (ast.IsSourceFile(input.Parent)) {
            tx.resultHasExternalModuleIndicator = true;
        }
        tx.resultHasScopeMarker = true;
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExportDeclaration(
            input.AsExportDeclaration(),
            input.Modifiers(),
            input.IsTypeOnly(),
            input.AsExportDeclaration().ExportClause,
            tx.rewriteModuleSpecifier(Ꮡinput, input.ModuleSpecifier()),
            (~input.AsExportDeclaration()).Attributes);
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return Ꮡtx.transformExportAssignment(Ꮡinput, Ꮡinput, input.Expression(), input.AsExportAssignment().IsExportEquals);
    }
    default: {
        var id = ast.GetNodeId(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡinput));
        if (tx.lateStatementReplacementMap[id] == nil) {
            tx.lateStatementReplacementMap[id] = Ꮡtx.transformTopLevelDeclaration(Ꮡinput);
        }
        return Ꮡinput;
    }}

}

internal static ж<ast.Node> tryGetNameOfAssignedExpression(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡunwrapped) {
    ref var unwrapped = ref Ꮡunwrapped.DerefOrNull();

    ж<ast.Node> nameNode = default;
    @string nameText = default;
    if (!ast.IsPropertyAccessExpression(Ꮡunwrapped) && unwrapped.Name() != nil) {
        nameText = unwrapped.Name().Text();
    } else if (ast.IsIdentifier(Ꮡunwrapped)) {
        nameText = unwrapped.Text();
    }
    if (nameText != ""u8 && nameText != "default"u8) {
        if (tx.resolver.IsNameResolvable(tx.enclosingDeclaration, nameText)) {
            nameNode = tx.Transformer.Factory().NewUniqueNameEx(nameText, new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
        } else {
            nameNode = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(nameText);
        }
    }
    return nameNode;
}

internal static ж<ast.Node> getNameOfExportedAssignedExpression(this ref DeclarationTransformer tx, ж<ast.Node> unwrapped, bool isExportEquals) {
    var nameNode = tx.tryGetNameOfAssignedExpression(unwrapped);
    if (nameNode == nil) {
        if (isExportEquals && ast.IsSourceFileJS(tx.state.currentSourceFile)) {
            nameNode = tx.Transformer.Factory().NewUniqueNameEx("_exports", new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
        } else {
            nameNode = tx.Transformer.Factory().NewUniqueNameEx("_default", new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
        }
    }
    tx.cjsExportAssignmentName = nameNode;
    return nameNode;
}

internal static ж<ast.Node> transformExportAssignment(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡinput, ж<ast.Node> Ꮡassignment, ж<ast.Node> expression, bool isExportEquals) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var input = ref Ꮡinput.DerefOrNull();
    ref var assignment = ref Ꮡassignment.DerefOrNull();

    if (ast.IsSourceFile(input.Parent)) {
        tx.resultHasExternalModuleIndicator = true;
    }
    tx.resultHasScopeMarker = true;
    if (ast.IsIdentifier(expression) && (ast.IsSourceFile(input.Parent) || ast.IsModuleBlock(input.Parent))) {
        var exportAssignmentΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(nil, isExportEquals, nil, expression);
        tx.preserveJsDoc(exportAssignmentΔ1, Ꮡinput);
        return exportAssignmentΔ1;
    }

    var unwrapped = ast.SkipOuterExpressions(expression, ast.OEKExpressionTypePassthrough);
    var newId = tx.getNameOfExportedAssignedExpression(unwrapped, isExportEquals);
    if (ast.IsClassExpression(unwrapped)) {
        slice<ж<ast.Node>> mods = default;
        if (tx.needsDeclare) {
            mods = append(mods, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword));
        }
        var classDecl = Ꮡtx.transformClassExpressionToDeclaration(unwrapped, newId, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(mods));
        tx.preserveJsDoc(classDecl, Ꮡinput);
        var exportAssignmentΔ2 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(nil, isExportEquals, nil, newId);
        tx.removeAllComments(exportAssignmentΔ2);
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([exportAssignmentΔ2, classDecl]);
    } else if (ast.IsFunctionLike(unwrapped)) {
        slice<ж<ast.Node>> mods = default;
        if (tx.needsDeclare) {
            mods = append(mods, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword));
        }
        var fullSignatureType = assignment.Type();
        var funcDecl = tx.transformFunctionLikeToDeclaration(unwrapped, newId, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(mods), fullSignatureType);
        tx.preserveJsDoc(funcDecl, Ꮡinput);
        var exportAssignmentΔ3 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(nil, isExportEquals, nil, newId);
        tx.removeAllComments(exportAssignmentΔ3);
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([exportAssignmentΔ3, funcDecl]);
    }

    tx.state.getSymbolAccessibilityDiagnostic = (printer.SymbolAccessibilityResult _) => Ꮡ(new SymbolAccessibilityDiagnostic(
            diagnosticMessage: diagnostics.Default_export_of_the_module_has_or_is_using_private_name_0,
            errorNode: Ꮡinput
        ));
    tx.cjsExportAssignmentName = newId;
    tx.tracker.PushErrorFallbackNode(Ꮡassignment);
    ж<ast.Node> type_ = default;
    ж<ast.Node> initializer = default;
    if (ast.IsPrimitiveLiteralValue(unwrapParenthesizedExpression(expression), true)) {
        initializer = tx.resolver.CreateLiteralConstValue(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext(), Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().ParseNode(Ꮡassignment), new SymbolTrackerImplжSymbolTracker(tx.tracker));
    }
    if (initializer == nil) {
        type_ = tx.ensureType(Ꮡassignment, false);
    }
    var varDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(newId, nil, type_, initializer);
    tx.tracker.PopErrorFallbackNode();
    ж<ast.ModifierList> modList = default;
    if (tx.needsDeclare) {
        modList = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword)]));
    } else {
        modList = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList([]);
    }
    var statement = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(modList, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsConst));
    var exportAssignment = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(nil, isExportEquals, nil, newId);
    tx.preserveJsDoc(statement, Ꮡinput);
    return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([statement, exportAssignment]);
}

internal static ж<ast.Node> transformFunctionLikeToDeclaration(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡunwrapped, ж<ast.Node> funcName, ж<ast.ModifierList> mods, ж<ast.Node> fullSignatureType) {
    ref var unwrapped = ref Ꮡunwrapped.DerefOrNull();

    var d = unwrapped.FunctionLikeData();
    var sig = d.FullSignature;
    if (sig == nil) {
        sig = fullSignatureType;
    }
    if (sig == nil) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionDeclaration(
            mods,
            nil,
            funcName,
            tx.ensureTypeParams(Ꮡunwrapped, d.TypeParameters),
            tx.updateParamList(Ꮡunwrapped, d.Parameters),
            tx.ensureType(Ꮡunwrapped, false),
            tx.Transformer.Visitor().VisitNode(sig),
            nil);
    } else {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
            mods,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(funcName, nil, tx.Transformer.Visitor().VisitNode(sig), nil)])), ast.NodeFlagsConst));
    }
}

internal static ж<ast.Node> transformBinaryExpressionToExportDeclaration(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡinput, ж<ast.Node> Ꮡname) {
    ref var input = ref Ꮡinput.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    var propertyName = input.AsBinaryExpression().Right;

    tx.tracker.handleSymbolAccessibilityError(tx.resolver.IsEntityNameVisible(propertyName, tx.enclosingDeclaration));

    if (ast.IsIdentifier(Ꮡname) && propertyName.Text() == name.Text()) {
        propertyName = default;
    }

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(
        nil,
        false,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(false, propertyName, Ꮡname)]))),
        nil,
        nil);
}

internal static ж<ast.Node> transformCommonJSExport(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> input, ж<ast.Node> name) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    var res = Ꮡtx.transformCommonJSExportWorker(input, name);
    if (res == nil) {
        return res;
    }
    return tx.wrapInCJSExportNamespace(res);
}

internal static ж<ast.Node> transformCommonJSExportWorker(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡinput, ж<ast.Node> Ꮡname) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var input = ref Ꮡinput.DerefOrNull();
        ref var name = ref Ꮡname.DerefOrNull();

        @string nameText = default;
        if (ast.IsIdentifier(Ꮡname) || ast.IsStringLiteral(Ꮡname)) {
            nameText = name.Text();
        }
        if (Ꮡtx.ᏑwitnessedCjsExports.Has(nameText) && nameText != ""u8) {
            return default;
        }
        tx.witnessedCjsExports.Add(nameText);
        tx.resultHasExternalModuleIndicator = true;
        tx.resultHasScopeMarker = true;
        if (isCommonJSAliasExport(Ꮡinput) && ast.IsExpressionStatement(input.Parent) && ast.IsSourceFile(input.Parent.Parent)) {
            return tx.transformBinaryExpressionToExportDeclaration(Ꮡinput, Ꮡname);
        }

        if (ast.IsBinaryExpression(Ꮡinput)) {
            {
                var rhs = unwrapParenthesizedExpression(input.AsBinaryExpression().Right); if (ast.IsClassExpression(rhs)) {
                    var ce = rhs.AsClassExpression();
                    var classExprName = ce.Name();
                    var hasExprName = classExprName != nil && len(classExprName.Text()) > 0;

                    if (hasExprName) {
                        tx.tracker.watchedClassSymbol = rhs.Symbol();
                        tx.tracker.classSymbolTracked = false;
                        defer(() => {
                            Ꮡtx.Value.tracker.watchedClassSymbol = default;
                            Ꮡtx.Value.tracker.classSymbolTracked = false;
                        }, ref ᒐ);

                        var classNameΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(classExprName.Text());
                        var classMods = slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindExportKeyword)]);
                        var classDeclΔ1 = Ꮡtx.transformClassExpressionToDeclaration(rhs, classNameΔ1, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(classMods));
                        tx.preserveJsDoc(classDeclΔ1, Ꮡinput);

                        var namesDiffer = !ast.IsIdentifier(Ꮡname) || classExprName.Text() != name.Text();
                        var needsIsolation = namesDiffer || tx.tracker.classSymbolTracked;

                        if (needsIsolation) {
                            var nsName = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewUniqueNameEx("_ns", new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
                            slice<ж<ast.Node>> nsMods = default;
                            if (tx.needsDeclare) {
                                nsMods = append(nsMods, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword));
                            }
                            var nsDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleDeclaration(
                                Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(nsMods),
                                ast.KindNamespaceKeyword,
                                nsName,
                                nil,
                                Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleBlock(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([classDeclΔ1])));

                            @string aliasBase = "_exported";
                            @string nameTextΔ1 = name.Text(); if (ast.IsIdentifier(Ꮡname) && scanner.IsIdentifierText("_"u8 + nameTextΔ1, core.LanguageVariantStandard)) {
                                aliasBase = "_"u8 + nameTextΔ1;
                            }
                            var importAlias = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewUniqueNameEx(aliasBase, new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
                            var qualifiedName = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewQualifiedName(nsName, classNameΔ1);
                            var importDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportEqualsDeclaration(nil, false, importAlias, qualifiedName);

                            var exportSpecifier = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(false, importAlias, Ꮡname);
                            var exportDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(nil, false, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([exportSpecifier])), nil, nil);
                            tx.removeAllComments(exportDecl);

                            return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(append(slice<ж<ast.Node>>([nsDecl, importDecl]), exportDecl));
                        }

                        slice<ж<ast.Node>> modsΔ1 = default;
                        modsΔ1 = append(modsΔ1, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindExportKeyword));
                        if (tx.needsDeclare) {
                            modsΔ1 = append(modsΔ1, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword));
                        }
                        classDeclΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
                            classDeclΔ1.AsClassDeclaration(),
                            Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(modsΔ1),
                            classDeclΔ1.AsClassDeclaration().Name(),
                            (~classDeclΔ1.AsClassDeclaration()).TypeParameters,
                            classDeclΔ1.AsClassDeclaration().HeritageClauses,
                            (~classDeclΔ1.AsClassDeclaration()).Members);
                        return classDeclΔ1;
                    }
                    slice<ж<ast.Node>> mods = default;
                    mods = append(mods, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindExportKeyword));
                    if (tx.needsDeclare) {
                        mods = append(mods, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword));
                    }
                    var className = Ꮡname;
                    if (!ast.IsIdentifier(className)) {
                        className = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewUniqueNameEx("_class", new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
                    }
                    var classDecl = Ꮡtx.transformClassExpressionToDeclaration(rhs, className, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(mods));
                    tx.preserveJsDoc(classDecl, Ꮡinput);
                    if (!ast.IsIdentifier(Ꮡname)) {
                        var exportDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(nil, false, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(false, className, Ꮡname)]))), nil, nil);
                        tx.removeAllComments(exportDecl);
                        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([classDecl, exportDecl]);
                    }
                    return classDecl;
                }
            }
        }

        if (ast.IsIdentifier(Ꮡname)) {
            if (name.Text() == "default"u8) {
                var newIdΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewUniqueNameEx("_default", new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
                tx.state.getSymbolAccessibilityDiagnostic = (printer.SymbolAccessibilityResult _) => Ꮡ(new SymbolAccessibilityDiagnostic(
                        diagnosticMessage: diagnostics.Default_export_of_the_module_has_or_is_using_private_name_0,
                        errorNode: Ꮡinput
                    ));
                tx.tracker.PushErrorFallbackNode(Ꮡinput);
                var type_Δ1 = tx.ensureType(Ꮡinput, false);
                var varDeclΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(newIdΔ1, nil, type_Δ1, nil);
                tx.tracker.PopErrorFallbackNode();
                ж<ast.ModifierList> modListΔ1 = default;
                if (tx.needsDeclare) {
                    modListΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword)]));
                } else {
                    modListΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList([]);
                }
                var statementΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(modListΔ1, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDeclΔ1]), ast.NodeFlagsConst));

                var assignmentΔ1 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(input.Modifiers(), false, nil, newIdΔ1);
                tx.preserveJsDoc(statementΔ1, Ꮡinput);
                tx.removeAllComments(assignmentΔ1);
                return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([statementΔ1, assignmentΔ1]);
            } else if (tx.host.GetEmitResolver().GetReferencedValueDeclaration(Ꮡname) == Ꮡinput || tx.host.GetEmitResolver().GetReferencedValueDeclaration(Ꮡname) == nil) {
                tx.tracker.PushErrorFallbackNode(Ꮡinput);
                var type_Δ2 = tx.ensureType(Ꮡinput, false);
                var varDeclΔ2 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(Ꮡname, nil, type_Δ2, nil);
                tx.tracker.PopErrorFallbackNode();
                ж<ast.ModifierList> modListΔ2 = default;
                if (tx.needsDeclare) {
                    modListΔ2 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindExportKeyword), Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword)]));
                } else {
                    modListΔ2 = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindExportKeyword)]));
                }
                return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(modListΔ2, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDeclΔ2]), ast.NodeFlagsNone));
            }
        }
        var newId = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewUniqueNameEx("_exported", new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
        tx.state.getSymbolAccessibilityDiagnostic = (printer.SymbolAccessibilityResult _) => Ꮡ(new SymbolAccessibilityDiagnostic(
                diagnosticMessage: diagnostics.Default_export_of_the_module_has_or_is_using_private_name_0,
                errorNode: Ꮡinput
            ));
        tx.tracker.PushErrorFallbackNode(Ꮡinput);
        var type_ = tx.ensureType(Ꮡinput, false);
        var varDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(newId, nil, type_, nil);
        tx.tracker.PopErrorFallbackNode();
        ж<ast.ModifierList> modList = default;
        if (tx.needsDeclare) {
            modList = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword)]));
        } else {
            modList = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList([]);
        }
        var statement = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(modList, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsConst));

        var assignment = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(nil, false, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(false, newId, Ꮡname)]))), nil, nil);
        tx.preserveJsDoc(statement, Ꮡinput);
        tx.removeAllComments(assignment);
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([statement, assignment]);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> wrapInCJSExportNamespace(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡcontent) {
    ref var content = ref Ꮡcontent.DerefOrNull();

    if (tx.cjsExportAssignmentName == nil) {
        return Ꮡcontent;
    }
    var nsName = tx.cjsExportAssignmentName;
    slice<ж<ast.Node>> members = default;
    if (content.Kind == ast.KindSyntaxList) {
        members = content.AsSyntaxList().Value.Children;
    } else {
        members = [Ꮡcontent];
    }
    slice<ж<ast.Node>> nsMods = default;
    if (tx.needsDeclare) {
        nsMods = append(nsMods, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword));
    }
    (members, _) = tx.declareStrippingVisitor.VisitSlice(members);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleDeclaration(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(nsMods),
        ast.KindNamespaceKeyword,
        nsName,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(members)));
}

} partial class declarations_package {

internal static bool isCommonJSAliasExport(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBinaryExpression(Ꮡnode) && ast.IsIdentifier(node.AsBinaryExpression().Right)) {
        var symbol = node.Symbol(); if (symbol != nil && len(symbol.Declarations) == 1) {
            return true;
        }
    }
    return false;
}

} partial class declarationsᴛDeclarationTransformer {

internal static ж<ast.Node> transformClassExpressionToDeclaration(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> ᏑclassExpr, ж<ast.Node> className, ж<ast.ModifierList> modifiers) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var classExpr = ref ᏑclassExpr.DerefOrNull();

        var previousEnclosingDeclaration = tx.enclosingDeclaration;
        tx.enclosingDeclaration = ᏑclassExpr;
        var previousInClassExpressionDeclaration = tx.inClassExpressionDeclaration;
        tx.inClassExpressionDeclaration = true;
        var previousEnclosingDeclarationʗ1 = previousEnclosingDeclaration;
        defer(() => {
            Ꮡtx.Value.enclosingDeclaration = previousEnclosingDeclarationʗ1;
            Ꮡtx.Value.inClassExpressionDeclaration = previousInClassExpressionDeclaration;
        }, ref ᒐ);

        slice<ж<ast.Node>> extraMembers = default;
        if (ast.IsInJSFile(ᏑclassExpr)) {
            extraMembers = Ꮡtx.collectThisPropertyAssignments(ᏑclassExpr);
        }
        var members = tx.buildClassMembers(ᏑclassExpr, extraMembers.ꓸꓸꓸ);
        var typeParameters = tx.ensureTypeParams(ᏑclassExpr, (~classExpr.AsClassExpression()).TypeParameters);
        var heritageClauses = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitNodes(classExpr.AsClassExpression().HeritageClauses);

        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewClassDeclaration(
            modifiers,
            className,
            typeParameters,
            heritageClauses,
            members);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> rewriteModuleSpecifier(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡparent, ж<ast.Node> input) {
    ref var parent = ref Ꮡparent.DerefOrNull();

    if (input == nil) {
        return default;
    }
    tx.resultHasExternalModuleIndicator = tx.resultHasExternalModuleIndicator || (parent.Kind != ast.KindModuleDeclaration && parent.Kind != ast.KindImportType);
    return input;
}

internal static void preserveJsDoc(this ref DeclarationTransformer tx, ж<ast.Node> updated, ж<ast.Node> original) {
    tx.Transformer.EmitContext().AssignCommentRange(updated, original);
}

internal static void preservePartialJsDoc(this ref DeclarationTransformer tx, ж<ast.Node> updated, ж<ast.Node> Ꮡoriginal) {
    ref var original = ref Ꮡoriginal.DerefOrNull();

    if ((ast.NodeFlags)(original.Flags & ast.NodeFlagsReparsed) == 0) {
        return;
    }
    var jsdoc = core.FirstOrNil(Ꮡoriginal.EagerJSDoc(ast.GetSourceFileOfNode(Ꮡoriginal)));
    if (jsdoc == nil) {
        return;
    }
    @string description = scanner.GetTextOfJSDocComment(jsdoc.AsJSDoc().Comment);
    if (description == ""u8) {
        return;
    }
    @string comment = "*\n * "u8 + strings.ReplaceAll(description, "\n", "\n * ") + "\n "u8;
    tx.Transformer.EmitContext().AddSyntheticLeadingComment(updated, ast.KindMultiLineCommentTrivia, comment, true);
}

internal static void removeAllComments(this ref DeclarationTransformer tx, ж<ast.Node> node) {
    tx.Transformer.EmitContext().AddEmitFlags(node, printer.EFNoComments);
}

internal static ж<ast.Node> ensureType(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode, bool ignorePrivate) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ignorePrivate && tx.host.GetEffectiveDeclarationFlags(tx.Transformer.EmitContext().ParseNode(Ꮡnode), ast.ModifierFlagsPrivate) != 0) {
        return default;
    }

    if (tx.shouldPrintWithInitializer(Ꮡnode)) {
        return default;
    }

    if (!ast.IsExportAssignment(Ꮡnode) && !ast.IsBindingElement(Ꮡnode) && node.Type() != nil && (!ast.IsParameterDeclaration(Ꮡnode) || !tx.resolver.RequiresAddingImplicitUndefined(Ꮡnode, nil, tx.enclosingDeclaration))) {
        if (tx.state.currentSourceFile.IsJS()) {
            var jsFlags = declarationEmitNodeBuilderFlags;
            if (tx.inClassExpressionDeclaration) {
                jsFlags &= unchecked((nodebuilder.Flags)~(nodebuilder.Flags)(nodebuilder.FlagsWriteClassExpressionAsTypeLiteral));
            }
            var res = tx.resolver.TryJSTypeNodeToTypeNode(tx.Transformer.EmitContext(), node.Type(), tx.enclosingDeclaration, jsFlags, declarationEmitInternalNodeBuilderFlags, new SymbolTrackerImplжSymbolTracker(tx.tracker));
            if (res != nil) {
                return res;
            }
        } else {
            return tx.Transformer.Visitor().Visit(node.Type());
        }
    }

    var oldErrorNameNode = tx.state.errorNameNode;
    tx.state.errorNameNode = node.Name();
    GetSymbolAccessibilityDiagnostic oldDiag = default;
    if (!tx.suppressNewDiagnosticContexts) {
        oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
        if (canProduceDiagnostics(Ꮡnode)) {
            tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNode(Ꮡnode);
        }
    }
    ж<ast.Node> typeNode = default;

    var flags = declarationEmitNodeBuilderFlags;
    if (tx.inClassExpressionDeclaration) {
        flags &= unchecked((nodebuilder.Flags)~(nodebuilder.Flags)(nodebuilder.FlagsWriteClassExpressionAsTypeLiteral));
    }
    if (ast.HasInferredType(Ꮡnode)) {
        typeNode = tx.resolver.CreateTypeOfDeclaration(tx.Transformer.EmitContext(), Ꮡnode, tx.enclosingDeclaration, flags, declarationEmitInternalNodeBuilderFlags, new SymbolTrackerImplжSymbolTracker(tx.tracker));
    } else if (ast.IsFunctionLike(Ꮡnode)) {
        typeNode = tx.resolver.CreateReturnTypeOfSignatureDeclaration(tx.Transformer.EmitContext(), Ꮡnode, tx.enclosingDeclaration, flags, declarationEmitInternalNodeBuilderFlags, new SymbolTrackerImplжSymbolTracker(tx.tracker));
    } else {
        debug.AssertNever(Ꮡnode.OrTypedNil());
    }

    tx.state.errorNameNode = oldErrorNameNode;
    if (!tx.suppressNewDiagnosticContexts) {
        tx.state.getSymbolAccessibilityDiagnostic = oldDiag;
    }
    if (typeNode == nil) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
    }
    return typeNode;
}

internal static bool shouldPrintWithInitializer(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return canHaveLiteralInitializer(tx.host, Ꮡnode) && node.Initializer() != nil && tx.resolver.IsLiteralConstDeclaration(tx.Transformer.EmitContext().MostOriginal(Ꮡnode));
}

internal static void checkEntityNameVisibility(this ref DeclarationTransformer tx, ж<ast.Node> entityName, ж<ast.Node> enclosingDeclaration) {
    var visibilityResult = tx.resolver.IsEntityNameVisible(entityName, enclosingDeclaration);
    tx.tracker.handleSymbolAccessibilityError(visibilityResult);
}

internal static ж<ast.Node> transformTopLevelDeclaration(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡinput) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var input = ref Ꮡinput.DerefOrNull();

    if (len(tx.state.lateMarkedStatements) > 0) {
        tx.state.lateMarkedStatements = core.Filter(tx.state.lateMarkedStatements, (ж<ast.Node> node) => node != Ꮡinput);
    }
    if (tx.shouldStripInternal(Ꮡinput)) {
        return default;
    }
    if (input.Kind == ast.KindImportEqualsDeclaration) {
        return tx.transformImportEqualsDeclaration(input.AsImportEqualsDeclaration());
    }
    if (input.Kind == ast.KindImportDeclaration || input.Kind == ast.KindJSImportDeclaration) {
        var res = Ꮡtx.transformImportDeclaration(input.AsImportDeclaration());
        if (res != nil && res.Kind != ast.KindImportDeclaration) {
            var resΔ1 = res.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory()));
            resΔ1.Kind = ast.KindImportDeclaration;
            return resΔ1;
        }
        return res;
    }
    if (ast.IsDeclaration(Ꮡinput) && isDeclarationAndNotVisible(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext(), tx.resolver, Ꮡinput)) {
        return default;
    }

    if (ast.IsFunctionLike(Ꮡinput) && tx.resolver.IsImplementationOfOverload(Ꮡinput)) {
        return default;
    }
    var original = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡinput);
    var id = ast.GetNodeId(original);
    var (_, isExpandoHost) = tx.expandoHosts[id, ꟷ];
    var (_, hasDeferredExpandoAssignments) = tx.deferredExpandoAssignments[id, ꟷ];
    if (isExpandoHost || hasDeferredExpandoAssignments) {
        return Ꮡtx.createFullExpandoBlock(id);
    }

    var previousEnclosingDeclaration = tx.enclosingDeclaration;
    if (isEnclosingDeclaration(Ꮡinput)) {
        tx.enclosingDeclaration = Ꮡinput;
    }

    var canProduceDiagnostic = canProduceDiagnostics(Ꮡinput);
    var oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
    var oldName = tx.state.errorNameNode;
    if (canProduceDiagnostic) {
        tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNode(Ꮡinput);
    }
    var saveNeedsDeclare = tx.needsDeclare;

    ж<ast.Node> result = default;
    switch (input.Kind) {
    case var ᴛ when ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration: {
        result = tx.transformTypeAliasDeclaration(input.AsTypeAliasDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindInterfaceDeclaration: {
        result = tx.transformInterfaceDeclaration(input.AsInterfaceDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        result = tx.transformFunctionDeclaration(input.AsFunctionDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
        result = Ꮡtx.transformModuleDeclaration(input.AsModuleDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration: {
        result = Ꮡtx.transformClassDeclaration(input.AsClassDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableStatement: {
        result = tx.transformVariableStatement(input.AsVariableStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
        result = Ꮡtx.transformEnumDeclaration(input.AsEnumDeclaration());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("Unhandled top-level node in declaration emit: %q"u8, input.Kind));
        break;
    }}

    tx.enclosingDeclaration = previousEnclosingDeclaration;
    tx.state.getSymbolAccessibilityDiagnostic = oldDiag;
    tx.needsDeclare = saveNeedsDeclare;
    tx.state.errorNameNode = oldName;
    return result;
}

internal static ж<ast.Node> transformTypeAliasDeclaration(this ref DeclarationTransformer tx, ж<ast.TypeAliasDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    tx.needsDeclare = false;
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTypeAliasDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.TypeAliasDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        input.Name(),
        tx.Transformer.Visitor().VisitNodes(input.TypeParameters),
        tx.Transformer.Visitor().Visit(input.Type));
}

internal static ж<ast.Node> transformInterfaceDeclaration(this ref DeclarationTransformer tx, ж<ast.InterfaceDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateInterfaceDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        input.Name(),
        tx.Transformer.Visitor().VisitNodes(input.TypeParameters),
        tx.Transformer.Visitor().VisitNodes(input.HeritageClauses),
        tx.Transformer.Visitor().VisitNodes(input.Members));
}

internal static ж<ast.Node> transformFunctionDeclaration(this ref DeclarationTransformer tx, ж<ast.FunctionDeclaration> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    if (tx.resolver.IsExpandoFunctionDeclaration(Ꮡinput.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        tx.state.reportExpandoFunctionErrors(Ꮡinput.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        nil,
        input.Name(),
        tx.ensureTypeParams(Ꮡinput.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.TypeParameters),
        tx.updateParamList(Ꮡinput.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.Parameters),
        tx.ensureType(Ꮡinput.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false),
        nil,
        nil);
}

internal static ж<ast.Node> transformModuleDeclaration(this ж<DeclarationTransformer> Ꮡtx, ж<ast.ModuleDeclaration> Ꮡinput) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var input = ref Ꮡinput.DerefOrNull();

    var mods = tx.ensureModifiers(Ꮡinput.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var saveNeedsDeclare = tx.needsDeclare;
    tx.needsDeclare = false;
    var inner = input.Body;
    var keyword = input.Keyword;
    if (keyword != ast.KindGlobalKeyword && (input.Name() == nil || !ast.IsStringLiteral(input.Name()))) {
        keyword = ast.KindNamespaceKeyword;
    }
    var attributes = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().Visit(input.Attributes);

    if (inner != nil && inner.Kind == ast.KindModuleBlock) {
        var oldNeedsScopeFix = tx.needsScopeFixMarker;
        var oldHasScopeFix = tx.resultHasScopeMarker;
        tx.resultHasScopeMarker = false;
        tx.needsScopeFixMarker = false;
        var statements = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitNodes(inner.StatementList());
        var lateStatements = Ꮡtx.transformAndReplaceLatePaintedStatements(statements);
        if ((ast.NodeFlags)(input.Flags & ast.NodeFlagsAmbient) != 0) {
            tx.needsScopeFixMarker = false;
        }
        if (!ast.IsGlobalScopeAugmentation(Ꮡinput.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && !tx.resultHasScopeMarker && !hasScopeMarker(lateStatements)) {
            if (tx.needsScopeFixMarker) {
                lateStatements = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(append(lateStatements.Nodes, createEmptyExports(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory())));
            } else {
                lateStatements = tx.exportStrippingVisitor.VisitNodes(lateStatements);
            }
        }

        var body = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateModuleBlock(inner.AsModuleBlock(), lateStatements);
        tx.needsDeclare = saveNeedsDeclare;
        tx.needsScopeFixMarker = oldNeedsScopeFix;
        tx.resultHasScopeMarker = oldHasScopeFix;

        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateModuleDeclaration(
            Ꮡinput,
            mods,
            keyword,
            input.Name(),
            attributes,
            body);
    }
    if (inner != nil) {
        Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().Visit(inner);
        var original = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext().MostOriginal(inner);
        var id = ast.GetNodeId(original);
        var (body, _) = tx.lateStatementReplacementMap[id, ꟷ];
        delete(tx.lateStatementReplacementMap, id);
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateModuleDeclaration(
            Ꮡinput,
            mods,
            keyword,
            input.Name(),
            attributes,
            body);
    }
    return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateModuleDeclaration(
        Ꮡinput,
        mods,
        keyword,
        input.Name(),
        attributes,
        nil);
}

internal static ж<ast.Node> stripExportModifiers(this ref DeclarationTransformer tx, ж<ast.Node> statement) {
    if (statement == nil) {
        return default;
    }
    var parseNode = tx.Transformer.EmitContext().ParseNode(statement);
    if (ast.IsImportEqualsDeclaration(statement) || (parseNode != nil && tx.host.GetEffectiveDeclarationFlags(parseNode, ast.ModifierFlagsDefault) != 0) || !ast.CanHaveModifiers(statement)) {
        return statement;
    }

    var oldFlags = ast.GetCombinedModifierFlags(statement);
    if ((ast.ModifierFlags)(oldFlags & ast.ModifierFlagsExport) == 0) {
        return statement;
    }
    var newFlags = (ast.ModifierFlags)(oldFlags & ((ast.ModifierFlags)(ast.ModifierFlagsAll ^ ast.ModifierFlagsExport)));
    var modifiers = ast.CreateModifiersFromModifierFlags(newFlags, tx.Transformer.Factory().NewModifier);
    return ast.ReplaceModifiers(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), statement, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(modifiers));
}

internal static ж<ast.NodeList> buildClassMembers(this ref DeclarationTransformer tx, ж<ast.Node> ᏑclassNode, params ꓸꓸꓸжastꓸNode extraMembersʗp) {
    var extraMembers = extraMembersʗp.sslice();
    ref var classNode = ref ᏑclassNode.DerefOrNull();
    var parameterPropertiesᴷ = new stackAppend<ж<ast.Node>>();

    var ctor = ast.GetFirstConstructorWithBody(ᏑclassNode);
    slice<ж<ast.Node>> parameterProperties = default;
    if (ctor != nil) {
        var oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
        foreach (var (_, param) in range((~ctor.AsConstructorDeclaration()).Parameters.Nodes)) {
            if (!ast.HasSyntacticModifier(param, ast.ModifierFlagsParameterPropertyModifier) || tx.shouldStripInternal(param)) {
                continue;
            }
            tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNode(param);
            if (param.Name().Kind == ast.KindIdentifier) {
                var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(
                    tx.ensureModifiers(param),
                    param.Name(),
                    param.QuestionToken(),
                    tx.ensureType(param, false),
                    tx.ensureNoInitializer(param));
                tx.preserveJsDoc(updated, param);
                parameterProperties = append(parameterPropertiesᴷ, parameterProperties, updated);
            } else {
                parameterProperties = appendꓸꓸꓸ(parameterProperties, tx.walkBindingPattern(param.Name().AsBindingPattern(), param));
            }
        }
        tx.state.getSymbolAccessibilityDiagnostic = oldDiag;
    }

    ж<ast.Node> privateIdentifier = default;
    if (core.Some(classNode.ClassLikeData().Members.Nodes, (ж<ast.Node> member) => member.Name() != nil && ast.IsPrivateIdentifier(member.Name()))) {
        privateIdentifier = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPrivateIdentifier("#private"), nil, nil, nil);
    }

    var lateIndexes = tx.resolver.CreateLateBoundIndexSignatures(
        tx.Transformer.EmitContext(),
        ᏑclassNode,
        tx.enclosingDeclaration,
        declarationEmitNodeBuilderFlags,
        declarationEmitInternalNodeBuilderFlags,
        new SymbolTrackerImplжSymbolTracker(tx.tracker));

    var memberNodes = new slice<ж<ast.Node>>(0, len(classNode.ClassLikeData().Members.Nodes));
    if (privateIdentifier != nil) {
        memberNodes = append(memberNodes, privateIdentifier);
    }
    memberNodes = appendꓸꓸꓸ(memberNodes, lateIndexes);
    memberNodes = appendꓸꓸꓸ(memberNodes, parameterProperties);
    memberNodes = appendꓸꓸꓸ(memberNodes, extraMembers);
    var visitResult = tx.Transformer.Visitor().VisitNodes(classNode.ClassLikeData().Members);
    if (visitResult != nil && len(visitResult.Nodes) > 0) {
        memberNodes = appendꓸꓸꓸ(memberNodes, visitResult.Nodes);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(memberNodes);
}

internal static ж<ast.Node> transformClassDeclaration(this ж<DeclarationTransformer> Ꮡtx, ж<ast.ClassDeclaration> Ꮡinput) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var input = ref Ꮡinput.DerefOrNull();

        var previousEnclosingDeclaration = tx.enclosingDeclaration;
        tx.enclosingDeclaration = Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        var previousEnclosingDeclarationʗ1 = previousEnclosingDeclaration;
        defer(() => {
            Ꮡtx.Value.enclosingDeclaration = previousEnclosingDeclarationʗ1;
        }, ref ᒐ);

        tx.state.errorNameNode = input.Name();
        tx.tracker.PushErrorFallbackNode(Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡtx.Value.tracker.PopErrorFallbackNode, ref ᒐ);

        var modifiers = tx.ensureModifiers(Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var typeParameters = tx.ensureTypeParams(Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), input.TypeParameters);

        slice<ж<ast.Node>> extraMembers = default;
        if (ast.IsInJSFile(Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            extraMembers = Ꮡtx.collectThisPropertyAssignments(Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }

        var members = tx.buildClassMembers(Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), extraMembers.ꓸꓸꓸ);

        var extendsClause = getEffectiveBaseTypeNode(Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

        if (extendsClause != nil && !ast.IsEntityNameExpression((~extendsClause.AsExpressionWithTypeArguments()).Expression) && (~extendsClause.AsExpressionWithTypeArguments()).Expression.Kind != ast.KindNullKeyword) {
            tx.tracker.ReportInferenceFallback((~extendsClause.AsExpressionWithTypeArguments()).Expression);
            @string oldId = "default";
            if (ast.NodeIsPresent(input.Name()) && ast.IsIdentifier(input.Name()) && len(input.Name().Text()) > 0) {
                oldId = input.Name().Text();
            }
            var newId = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewUniqueNameEx(oldId + "_base"u8, new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsOptimistic));
            var extendsClauseʗ1 = extendsClause;
            tx.state.getSymbolAccessibilityDiagnostic = (printer.SymbolAccessibilityResult _) => Ꮡ(new SymbolAccessibilityDiagnostic(
                    diagnosticMessage: diagnostics.X_extends_clause_of_exported_class_0_has_or_is_using_private_name_1,
                    errorNode: extendsClauseʗ1,
                    typeName: Ꮡinput.Value.Name()
                ));

            var varDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                newId,
                nil,
                tx.resolver.CreateTypeOfExpression(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext(), extendsClause.Expression(), Ꮡinput.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), declarationEmitNodeBuilderFlags, declarationEmitInternalNodeBuilderFlags, new SymbolTrackerImplжSymbolTracker(tx.tracker)),
                nil);
            ж<ast.ModifierList> mods = default;
            if (tx.needsDeclare) {
                mods = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindDeclareKeyword)]));
            }
            var statement = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
                mods,
                Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsConst));
            var newHeritageClause = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateHeritageClause(
                extendsClause.Parent.AsHeritageClause(),
                extendsClause.Parent.AsHeritageClause().Token,
                Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExpressionWithTypeArguments(
                        extendsClause.AsExpressionWithTypeArguments(),
                        newId,
                        Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitNodes((~extendsClause.AsExpressionWithTypeArguments()).TypeArguments))
                ])));
            var retainedHeritageClauses = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitNodes(input.HeritageClauses);
            var heritageList = slice<ж<ast.Node>>([
                newHeritageClause
            ]);
            if (retainedHeritageClauses != nil && len(retainedHeritageClauses.Nodes) > 0) {
                heritageList = appendꓸꓸꓸ(heritageList, retainedHeritageClauses.Nodes);
            }
            var heritageClauses = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(heritageList);

            return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(slice<ж<ast.Node>>([
                statement,
                Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
                    Ꮡinput,
                    modifiers,
                    input.Name(),
                    typeParameters,
                    heritageClauses,
                    members)
            ]));
        }

        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
            Ꮡinput,
            modifiers,
            input.Name(),
            typeParameters,
            Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Visitor().VisitNodes(input.HeritageClauses),
            members);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitThisPropertyAssignments(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> thisTarget = default;
    var isStatic = false;
    var thisContainer = ast.GetThisContainer(Ꮡnode, false, false);
    thisTarget = thisContainer.Parent;
    if (thisTarget == nil) {
        return default;
    }
    if (ast.HasStaticModifier(thisContainer) || ast.IsClassStaticBlockDeclaration(thisContainer)) {
        isStatic = true;
    }
    if (thisTarget != tx.enclosingDeclaration) {
        return default;
    }
caseBlock:
    switch (ast.GetAssignmentDeclarationKind(Ꮡnode)) {
    case var ᴛ when ᴛ == ast.JSDeclarationKindThisProperty: {
        var name = ast.GetNameOfDeclaration(Ꮡnode);
        var @base = tx.resolver.GetReferencedMemberValueDeclaration(Ꮡnode);
        var key = getThisPropertyAssignmentKey(name, Ꮡnode, isStatic);
        if (@base == nil || Ꮡtx.ᏑseenProperties.Has(key)) {
            break;
        }
        tx.seenProperties.Add(key);

        if (thisTarget.ClassLikeData().HeritageClauses != nil && len(thisTarget.ClassLikeData().HeritageClauses.Nodes) > 0 && !isClassExtendingNull(thisTarget)) {
            tx.tracker.ReportInferenceFallback(thisTarget);
            if (tx.resolver.IsThisPropertyAssignmentDeclarationRedundant(Ꮡnode)) {
                goto break_caseBlock;
            }
        }

        ж<ast.ModifierList> mods = default;
        if (isStatic) {
            mods = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindStaticKeyword)]));
        }
        if (ast.HasDynamicName(Ꮡnode)) {
            if (!transformers.IsSimpleInlineableExpression(name)) {
                break;
            }
            tx.checkName(Ꮡnode);
            name = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewComputedPropertyName(name);
        }
        if (ast.GetTextOfPropertyName(name) == "constructor"u8) {
            break;
        }
        if (ast.IsIdentifier(name) && !scanner.IsIdentifierText(name.Text(), core.LanguageVariantStandard)) {
            name = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewStringLiteralFromNode(name);
        }
        var prop = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(
            mods,
            name,
            nil,
            tx.ensureType(Ꮡnode, false),
            nil);
        if (ast.IsExpressionStatement(node.Parent)) {
            tx.preserveJsDoc(prop, node.Parent);
        }
        tx.thisPropertyAssignmentsCollected = append(tx.thisPropertyAssignmentsCollected, prop);
        break;
    }}

    break_caseBlock:;
    return tx.thisPropertyVisitor.VisitEachChild(Ꮡnode);
}

} partial class declarations_package {

internal static bool isClassExtendingNull(ж<ast.Node> node) {
    if (node == nil) {
        return false;
    }
    var extendsClause = ast.GetHeritageClause(node, ast.KindExtendsKeyword);
    if (extendsClause == nil) {
        return false;
    }
    var types = extendsClause.AsHeritageClause().Types;
    if (types == nil || len(types.Nodes) != 1) {
        return false;
    }
    var expr = types.Nodes[0].AsExpressionWithTypeArguments().Value.Expression;
    return expr != nil && expr.Kind == ast.KindNullKeyword;
}

} partial class declarationsᴛDeclarationTransformer {

internal static slice<ж<ast.Node>> collectThisPropertyAssignments(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> ᏑclassNode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var classNode = ref ᏑclassNode.DerefOrNull();

        var members = classNode.ClassLikeData().Members;
        var seen = new collections.Set<thisPropertyAssignmentKey>();
        foreach (var (_, member) in range(members.Nodes)) {
            if (member.Name() != nil) {
                var isStatic = ast.IsStatic(member);
                seen.Add(getThisPropertyAssignmentKey(member.Name(), member, isStatic));
            }
        }
        tx.seenProperties = seen;
        defer(Ꮡtx.ᏑseenProperties.Clear, ref ᒐ);
        tx.thisPropertyAssignmentsCollected = [];
        defer(() => {
            Ꮡtx.Value.thisPropertyAssignmentsCollected = default;
        }, ref ᒐ);

        foreach (var (_, n) in range(members.Nodes)) {
            tx.thisPropertyVisitor.VisitEachChild(n);
        }
        return tx.thisPropertyAssignmentsCollected;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static slice<ж<ast.Node>> walkBindingPattern(this ref DeclarationTransformer tx, ж<ast.BindingPattern> Ꮡpattern, ж<ast.Node> param) {
    ref var pattern = ref Ꮡpattern.DerefOrNull();
    var elemsᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    slice<ж<ast.Node>> elems = default;
    foreach (var (_, elem) in range(pattern.Elements.Nodes)) {
        if (ast.IsOmittedExpression(elem)) {
            continue;
        }
        if (ast.IsBindingPattern(elem.Name())) {
            elems = appendꓸꓸꓸ(elems, tx.walkBindingPattern(elem.Name().AsBindingPattern(), param));
            continue;
        }
        elems = append(elemsᴷ, elems, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(
            tx.ensureModifiers(param),
            elem.Name(),
            nil,
            tx.ensureType(elem, false),
            nil));
    }
    elems = move2heap(elemsᴷ, elems);
    return elems;
}

internal static ж<ast.Node> transformVariableStatement(this ref DeclarationTransformer tx, ж<ast.VariableStatement> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();
    var normalDeclarationsᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    var visible = false;
    foreach (var (_, decl) in range(input.DeclarationList.AsVariableDeclarationList().Declarations.Nodes)) {
        visible = getBindingNameVisible(tx.resolver, decl);
        if (visible) {
            break;
        }
    }
    if (!visible) {
        return default;
    }

    var inputNodes = input.DeclarationList.AsVariableDeclarationList().Declarations.Nodes;
    slice<ж<ast.Node>> extraImports = default;
    if (tx.state.currentSourceFile.CommonJSModuleIndicator != nil) {
        slice<ж<ast.Node>> normalDeclarations = default;
        slice<ж<ast.Node>> imports = default;
        foreach (var (_, n) in range(inputNodes)) {
            if (ast.IsVariableDeclarationInitializedToRequire(n)) {
                imports = append(imports, n);
            } else {
                normalDeclarations = append(normalDeclarationsᴷ, normalDeclarations, n);
            }
        }
        normalDeclarations = move2heap(normalDeclarationsᴷ, normalDeclarations);
        inputNodes = normalDeclarations;
        (extraImports, _) = tx.Transformer.Visitor().VisitSlice(imports);
    }

    var (nodes, _) = tx.Transformer.Visitor().VisitSlice(inputNodes);
    if (len(nodes) == 0) {
        if (len(extraImports) > 0) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(extraImports);
        }
        return default;
    }
    var nodeList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(nodes);

    var modifiers = tx.ensureModifiers(Ꮡinput.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    ж<ast.Node> declList = default;
    if (ast.IsVarUsing(input.DeclarationList) || ast.IsVarAwaitUsing(input.DeclarationList)) {
        declList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(nodeList, ast.NodeFlagsConst);
        tx.Transformer.EmitContext().SetOriginal(declList, input.DeclarationList);
        tx.Transformer.EmitContext().SetCommentRange(declList, input.DeclarationList.Loc);
        declList.Loc = input.DeclarationList.Loc;
    } else {
        declList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclarationList(input.DeclarationList.AsVariableDeclarationList(), nodeList, input.DeclarationList.Flags);
    }
    var res = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableStatement(Ꮡinput, modifiers, declList);
    if (len(extraImports) > 0) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(append(extraImports, res));
    }
    return res;
}

internal static ж<ast.Node> transformEnumDeclaration(this ж<DeclarationTransformer> Ꮡtx, ж<ast.EnumDeclaration> Ꮡinput) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var input = ref Ꮡinput.DerefOrNull();

    return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateEnumDeclaration(
        Ꮡinput,
        tx.ensureModifiers(Ꮡinput.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        input.Name(),
        Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(core.MapNonNil(input.Members.Nodes, ж<ast.Node> (ж<ast.Node> m) => {
            if (Ꮡtx.Value.shouldStripInternal(m)) {
                return default;
            }

            var enumValue = Ꮡtx.Value.resolver.GetEnumMemberValue(m);

            if (Ꮡtx.Value.state.isolatedDeclarations && m.Initializer() != nil && enumValue.HasExternalReferences && !ast.IsComputedPropertyName(m.Name())) {
                Ꮡtx.Value.state.addDiagnostic(createDiagnosticForNode(m, diagnostics.Enum_member_initializers_must_be_computable_without_references_to_external_symbols_with_isolatedDeclarations));
            }

            ж<ast.Node> newInitializer = default;
            switch (enumValue.Value.type()) {
            case jsnum.Number value: {
                if (value.IsInf()) {
                    if (value > 0D) {
                        newInitializer = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity");
                    } else {
                        newInitializer = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindMinusToken, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity"));
                    }
                } else if (value.IsNaN()) {
                    newInitializer = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("NaN");
                } else if (value >= 0D) {
                    newInitializer = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(value.String(), ast.TokenFlagsNone);
                } else {
                    newInitializer = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(
                        ast.KindMinusToken,
                        Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral((-value).String(), ast.TokenFlagsNone));
                }
                break;
            }
            case @string value: {
                newInitializer = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(value, ast.TokenFlagsNone);
                break;
            }
            default: {
                var value = enumValue.Value;
                newInitializer = default;
                break;
            }}
            var result = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateEnumMember(m.AsEnumMember(), m.Name(), newInitializer);
            Ꮡtx.Value.preserveJsDoc(result, m);
            return result;
        })));
}

internal static ж<ast.ModifierList> ensureModifiers(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var currentFlags = (ast.ModifierFlags)(ast.GetCombinedModifierFlags(tx.Transformer.EmitContext().ParseNode(Ꮡnode)) & ast.ModifierFlagsAll);
    var newFlags = tx.ensureModifierFlags(Ꮡnode);
    if (currentFlags == newFlags) {
        var mods = node.Modifiers();
        if (mods == nil) {
            return mods;
        }
        if (canReuseModifierNodes(mods.Nodes)) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(core.Filter<ж<ast.Node>>(mods.Nodes, ast.IsModifier));
        }
    }
    var result = ast.CreateModifiersFromModifierFlags(newFlags, tx.Transformer.Factory().NewModifier);
    if (len(result) == 0) {
        return default;
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(result);
}

internal static ast.ModifierFlags ensureModifierFlags(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var mask = (ast.ModifierFlags)(ast.ModifierFlagsAll ^ ((ast.ModifierFlags)((ast.ModifierFlags)(ast.ModifierFlagsPublic | ast.ModifierFlagsAsync) | ast.ModifierFlagsOverride)));
    var additions = ast.ModifierFlagsNone;
    if (tx.needsDeclare && !isAlwaysType(ref (Ꮡnode).DerefOrNull())) {
        additions = ast.ModifierFlagsAmbient;
    }
    var parentIsFile = node.Parent.Kind == ast.KindSourceFile;
    if (!parentIsFile) {
        mask ^= (ast.ModifierFlags)(ast.ModifierFlagsAmbient);
        additions = ast.ModifierFlagsNone;
    }
    if (ast.IsImplicitlyExportedJSDocDeclaration(Ꮡnode)) {
        additions |= (ast.ModifierFlags)(ast.ModifierFlagsExport);
    }
    return maskModifierFlags(Ꮡnode, mask, additions);
}

internal static ж<astꓸTypeParameterList> ensureTypeParams(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode, ж<astꓸTypeParameterList> @params) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.host.GetEffectiveDeclarationFlags(tx.Transformer.EmitContext().ParseNode(Ꮡnode), ast.ModifierFlagsPrivate) != 0) {
        return default;
    }
    ж<astꓸTypeParameterList> typeParameters = default;
    typeParameters = tx.Transformer.Visitor().VisitNodes(@params); if (typeParameters != nil) {
        return typeParameters;
    }
    var oldErrorNameNode = tx.state.errorNameNode;
    tx.state.errorNameNode = node.Name();
    GetSymbolAccessibilityDiagnostic oldDiag = default;
    if (!tx.suppressNewDiagnosticContexts) {
        oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
        if (canProduceDiagnostics(Ꮡnode)) {
            tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNode(Ꮡnode);
        }
    }

    var data = node.FunctionLikeData(); if (data != nil && data.FullSignature != nil) {
        var nodes = tx.resolver.CreateTypeParametersOfSignatureDeclaration(tx.Transformer.EmitContext(), Ꮡnode, tx.enclosingDeclaration, declarationEmitNodeBuilderFlags, declarationEmitInternalNodeBuilderFlags, new SymbolTrackerImplжSymbolTracker(tx.tracker)); if (nodes != nil) {
            typeParameters = Ꮡ(new astꓸTypeParameterList(
                Loc: node.Loc,
                Nodes: nodes
            ));
        }
    }

    tx.state.errorNameNode = oldErrorNameNode;
    if (!tx.suppressNewDiagnosticContexts) {
        tx.state.getSymbolAccessibilityDiagnostic = oldDiag;
    }
    return typeParameters;
}

internal static ж<astꓸParameterList> updateParamList(this ref DeclarationTransformer tx, ж<ast.Node> node, ж<astꓸParameterList> Ꮡparams) {
    ref var @params = ref Ꮡparams.DerefOrNull();

    if (tx.host.GetEffectiveDeclarationFlags(tx.Transformer.EmitContext().ParseNode(node), ast.ModifierFlagsPrivate) != 0 || len(@params.Nodes) == 0) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]);
    }
    var results = new slice<ж<ast.Node>>(len(@params.Nodes));
    foreach (var (i, p) in range(@params.Nodes)) {
        results[i] = tx.ensureParameter(p.AsParameterDeclaration());
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(results);
}

internal static ж<ast.Node> ensureParameter(this ref DeclarationTransformer tx, ж<ast.ParameterDeclaration> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
    if (!tx.suppressNewDiagnosticContexts) {
        tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNode(Ꮡp.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    ж<astꓸTokenNode> questionToken = default;
    if (tx.resolver.IsOptionalParameter(Ꮡp.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        if (p.QuestionToken != nil) {
            questionToken = p.QuestionToken;
        } else {
            questionToken = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken);
        }
    }
    var result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
        Ꮡp,
        nil,
        p.DotDotDotToken,
        tx.bindingNameVisitor.VisitNode(p.Name()),
        questionToken,
        tx.ensureType(Ꮡp.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), true),
        tx.ensureNoInitializer(Ꮡp.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    tx.state.getSymbolAccessibilityDiagnostic = oldDiag;
    return result;
}

internal static ж<ast.Node> ensureNoInitializer(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.shouldPrintWithInitializer(Ꮡnode)) {
        var unwrappedInitializer = unwrapParenthesizedExpression(node.Initializer());
        if (!ast.IsPrimitiveLiteralValue(unwrappedInitializer, true)) {
            tx.tracker.ReportInferenceFallback(Ꮡnode);
        }
        return tx.resolver.CreateLiteralConstValue(tx.Transformer.EmitContext(), tx.Transformer.EmitContext().ParseNode(Ꮡnode), new SymbolTrackerImplжSymbolTracker(tx.tracker));
    }
    return default;
}

internal static ж<ast.Node> visitBindingName(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindOmittedExpression: {
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindArrayBindingPattern || ᴛ == ast.KindObjectBindingPattern: {
        return node.VisitEachChild(tx.bindingNameVisitor);
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        if (node.PropertyName() != nil && ast.IsComputedPropertyName(node.PropertyName()) && ast.IsEntityNameExpression(node.PropertyName().Expression())) {
            tx.checkEntityNameVisibility(node.PropertyName().Expression(), tx.enclosingDeclaration);
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBindingElement(node.AsBindingElement(), node.AsBindingElement().DotDotDotToken, node.PropertyName(), tx.bindingNameVisitor.VisitNode(node.Name()), nil);
    }
    default: {
        return Ꮡnode;
    }}

}

internal static ж<ast.Node> transformImportEqualsDeclaration(this ref DeclarationTransformer tx, ж<ast.ImportEqualsDeclaration> Ꮡdecl) {
    ref var decl = ref Ꮡdecl.DerefOrNull();

    if (!tx.resolver.IsDeclarationVisible(Ꮡdecl.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return default;
    }
    if (decl.ModuleReference.Kind == ast.KindExternalModuleReference) {
        var specifier = ast.GetExternalModuleImportEqualsDeclarationExpression(Ꮡdecl.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportEqualsDeclaration(
            Ꮡdecl,
            Ꮡdecl.of(ast.ImportEqualsDeclaration.ᏑModifiersBase).Modifiers(),
            decl.IsTypeOnly,
            decl.Name(),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExternalModuleReference(decl.ModuleReference.AsExternalModuleReference(), tx.rewriteModuleSpecifier(Ꮡdecl.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), specifier)));
    } else {
        var oldDiag = tx.state.getSymbolAccessibilityDiagnostic;
        tx.state.getSymbolAccessibilityDiagnostic = createGetSymbolAccessibilityDiagnosticForNode(Ꮡdecl.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.checkEntityNameVisibility(decl.ModuleReference, tx.enclosingDeclaration);
        tx.state.getSymbolAccessibilityDiagnostic = oldDiag;
        return Ꮡdecl.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
}

internal static ж<ast.Node> transformImportDeclaration(this ж<DeclarationTransformer> Ꮡtx, ж<ast.ImportDeclaration> Ꮡdecl) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var decl = ref Ꮡdecl.DerefOrNull();

    if (decl.ImportClause == nil) {
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(
            Ꮡdecl,
            Ꮡdecl.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(),
            decl.ImportClause,
            tx.rewriteModuleSpecifier(Ꮡdecl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), decl.ModuleSpecifier),
            decl.Attributes);
    }
    var phaseModifier = decl.ImportClause.AsImportClause().PhaseModifier;
    if (phaseModifier == ast.KindDeferKeyword) {
        phaseModifier = ast.KindUnknown;
    }
    ж<ast.Node> visibleDefaultBinding = default;
    if (decl.ImportClause != nil && decl.ImportClause.Name() != nil && tx.resolver.IsDeclarationVisible(decl.ImportClause)) {
        visibleDefaultBinding = decl.ImportClause.Name();
    }
    if (decl.ImportClause.AsImportClause().NamedBindings == nil) {
        if (visibleDefaultBinding == nil) {
            return default;
        }
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(
            Ꮡdecl,
            Ꮡdecl.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(),
            Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportClause(
                decl.ImportClause.AsImportClause(),
                phaseModifier,
                visibleDefaultBinding,
                nil),
            tx.rewriteModuleSpecifier(Ꮡdecl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), decl.ModuleSpecifier),
            decl.Attributes);
    }
    if (decl.ImportClause.AsImportClause().NamedBindings.Kind == ast.KindNamespaceImport) {
        ж<ast.Node> namedBindings = default;
        if (tx.resolver.IsDeclarationVisible(decl.ImportClause.AsImportClause().NamedBindings)) {
            namedBindings = decl.ImportClause.AsImportClause().NamedBindings;
        }
        if (visibleDefaultBinding == nil && namedBindings == nil) {
            return default;
        }
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(
            Ꮡdecl,
            Ꮡdecl.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(),
            Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportClause(
                decl.ImportClause.AsImportClause(),
                phaseModifier,
                visibleDefaultBinding,
                namedBindings),
            tx.rewriteModuleSpecifier(Ꮡdecl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), decl.ModuleSpecifier),
            decl.Attributes);
    }
    var bindingList = core.Filter(
        decl.ImportClause.AsImportClause().NamedBindings.Elements(),
        (ж<ast.Node> b) => Ꮡtx.Value.resolver.IsDeclarationVisible(b));
    if (len(bindingList) > 0 || visibleDefaultBinding != nil) {
        ж<ast.Node> namedImports = default;
        if (len(bindingList) > 0) {
            namedImports = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateNamedImports(
                decl.ImportClause.AsImportClause().NamedBindings.AsNamedImports(),
                Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(bindingList));
        }
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(
            Ꮡdecl,
            Ꮡdecl.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(),
            Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportClause(
                decl.ImportClause.AsImportClause(),
                phaseModifier,
                visibleDefaultBinding,
                namedImports),
            tx.rewriteModuleSpecifier(Ꮡdecl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), decl.ModuleSpecifier),
            decl.Attributes);
    }
    if (tx.resolver.IsImportRequiredByAugmentation(Ꮡdecl)) {
        if (tx.state.isolatedDeclarations) {
            tx.state.addDiagnostic(createDiagnosticForNode(Ꮡdecl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.Declaration_emit_for_this_file_requires_preserving_this_import_for_augmentations_This_is_not_supported_with_isolatedDeclarations));
        }
        return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(
            Ꮡdecl,
            Ꮡdecl.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(),
            nil,
            tx.rewriteModuleSpecifier(Ꮡdecl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), decl.ModuleSpecifier),
            decl.Attributes);
    }
    return default;
}

internal static ж<ast.Node> transformJSDocTypeExpression(this ref DeclarationTransformer tx, ж<ast.JSDocTypeExpression> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Visitor().Visit(input.Type);
}

internal static ж<ast.Node> transformJSDocTypeLiteral(this ref DeclarationTransformer tx, ж<ast.JSDocTypeLiteral> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var (members, _) = tx.Transformer.Visitor().VisitSlice(input.JSDocPropertyTags);
    var replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewTypeLiteralNode(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(members));
    tx.Transformer.EmitContext().SetOriginal(replacement, Ꮡinput.of(ast.JSDocTypeLiteral.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return replacement;
}

internal static ж<ast.Node> transformJSDocPropertyTag(this ref DeclarationTransformer tx, ж<ast.JSDocParameterOrPropertyTag> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertySignatureDeclaration(
        nil,
        tx.Transformer.Visitor().Visit(input.TagName),
        nil,
        tx.Transformer.Visitor().Visit(input.TypeExpression),
        nil);
    tx.Transformer.EmitContext().SetOriginal(replacement, Ꮡinput.of(ast.JSDocParameterOrPropertyTag.ᏑJSDocTagBase).of(ast.JSDocTagBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return replacement;
}

internal static ж<ast.Node> transformJSDocAllType(this ref DeclarationTransformer tx, ж<ast.JSDocAllType> input) {
    var replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
    tx.Transformer.EmitContext().SetOriginal(replacement, input.of(ast.JSDocAllType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return replacement;
}

internal static ж<ast.Node> transformJSDocNullableType(this ref DeclarationTransformer tx, ж<ast.JSDocNullableType> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewUnionTypeNode(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
        tx.Transformer.Visitor().Visit(input.Type),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewLiteralTypeNode(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindNullKeyword))
    ])));
    tx.Transformer.EmitContext().SetOriginal(replacement, Ꮡinput.of(ast.JSDocNullableType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return replacement;
}

internal static ж<ast.Node> transformJSDocNonNullableType(this ref DeclarationTransformer tx, ж<ast.JSDocNonNullableType> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    return tx.Transformer.Visitor().Visit(input.Type);
}

internal static ж<ast.Node> transformJSDocVariadicType(this ref DeclarationTransformer tx, ж<ast.JSDocVariadicType> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayTypeNode(tx.Transformer.Visitor().Visit(input.Type));
    tx.Transformer.EmitContext().SetOriginal(replacement, Ꮡinput.of(ast.JSDocVariadicType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return replacement;
}

internal static ж<ast.Node> transformJSDocOptionalType(this ref DeclarationTransformer tx, ж<ast.JSDocOptionalType> Ꮡinput) {
    ref var input = ref Ꮡinput.DerefOrNull();

    var replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewUnionTypeNode(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
        tx.Transformer.Visitor().Visit(input.Type),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindUndefinedKeyword)
    ])));
    tx.Transformer.EmitContext().SetOriginal(replacement, Ꮡinput.of(ast.JSDocOptionalType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return replacement;
}

internal static ж<ast.Node> getNameExpressionPreferringIdentifier(this ref DeclarationTransformer tx, ж<ast.Node> ᏑnameExpr) {
    ref var nameExpr = ref ᏑnameExpr.DerefOrNull();

    if (ast.IsNumericLiteral(ᏑnameExpr)) {
        ᏑnameExpr = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(nameExpr.Text(), ast.TokenFlagsNone); nameExpr = ref ᏑnameExpr.DerefOrNull();
    }
    if (ast.IsStringLiteralLike(ᏑnameExpr) && scanner.IsIdentifierText(nameExpr.Text(), core.LanguageVariantStandard)) {
        var result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(nameExpr.Text());
        var kwKind = scanner.IdentifierToKeywordKind(result.AsIdentifier());
        if (kwKind == ast.KindUnknown || kwKind == ast.KindDefaultKeyword) {
            result.Parent = nameExpr.Parent;
            result.Flags &= unchecked((ast.NodeFlags)~(ast.NodeFlags)(ast.NodeFlagsSynthesized));
            return result;
        }
    }
    return ᏑnameExpr;
}

} partial class declarations_package {

internal static bool isNotDeclareModifier(ж<astꓸModifier> Ꮡmod) {
    ref var mod = ref Ꮡmod.DerefOrNull();

    return mod.Kind != ast.KindDeclareKeyword;
}

} partial class declarationsᴛDeclarationTransformer {

internal static ж<ast.Node> stripDeclareModifiers(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return default;
    }
    var mods = node.Modifiers();
    if (mods != nil) {
        var flags = node.ModifierFlags();
        if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0) {
            var filtered = core.Filter<ж<ast.Node>>(mods.Nodes, isNotDeclareModifier);
            Ꮡnode.AsMutable().SetModifiers(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(filtered));
        }
    }
    return Ꮡnode;
}

internal static ж<ast.Node> visitCJSExportAssignments(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡexpression) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var expression = ref Ꮡexpression.DerefOrNull();

        if (Ꮡexpression != nil) {
            var (_, cleanupDiagnosticContext) = Ꮡtx.setupDiagnosticContext(Ꮡexpression);
            var cleanupDiagnosticContextʗ1 = cleanupDiagnosticContext;
            defer(cleanupDiagnosticContextʗ1, ref ᒐ);
            switch (ast.GetAssignmentDeclarationKind(Ꮡexpression)) {
            case var ᴛ when ᴛ == ast.JSDeclarationKindModuleExports: {
                if (tx.state.currentSourceFile.CommonJSModuleIndicator != nil) {
                    var result = Ꮡtx.transformExportAssignment(expression.Parent, Ꮡexpression, expression.AsBinaryExpression().Right, true);
                    if (result != nil) {
                        tx.cjsExportAssignment = result;
                        tx.resultHasScopeMarker = true;
                        tx.resultHasExternalModuleIndicator = true;
                    }
                }
                break;
            }}

            return tx.cjsExportAssignmentVisitor.VisitEachChild(Ꮡexpression);
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitNestedExpression(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡexpression) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var expression = ref Ꮡexpression.DerefOrNull();

        if (Ꮡexpression != nil) {
            var (_, cleanupDiagnosticContext) = Ꮡtx.setupDiagnosticContext(Ꮡexpression);
            var cleanupDiagnosticContextʗ1 = cleanupDiagnosticContext;
            defer(cleanupDiagnosticContextʗ1, ref ᒐ);
            switch (ast.GetAssignmentDeclarationKind(Ꮡexpression)) {
            case var ᴛ when ᴛ == ast.JSDeclarationKindProperty: {
                Ꮡtx.transformExpandoAssignment(expression.AsBinaryExpression());
                break;
            }
            case var ᴛ when ᴛ == ast.JSDeclarationKindExportsProperty: {
                if (tx.state.currentSourceFile.CommonJSModuleIndicator != nil) {
                    var result = Ꮡtx.transformCommonJSExport(Ꮡexpression, tx.getNameExpressionPreferringIdentifier(ast.GetElementOrPropertyAccessName(expression.AsBinaryExpression().Left)));
                    if (result != nil) {
                        tx.cjsExportMembers = append(tx.cjsExportMembers, result);
                    }
                }
                break;
            }
            case var ᴛ when ᴛ == ast.JSDeclarationKindObjectDefinePropertyExports: {
                if (tx.state.currentSourceFile.CommonJSModuleIndicator != nil) {
                    var result = Ꮡtx.transformCommonJSExport(Ꮡexpression, tx.getNameExpressionPreferringIdentifier(expression.Arguments()[1]));
                    if (result != nil) {
                        tx.cjsExportMembers = append(tx.cjsExportMembers, result);
                    }
                }
                break;
            }}

            return tx.expressionVisitor.VisitEachChild(Ꮡexpression);
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static void transformExpandoAssignment(this ж<DeclarationTransformer> Ꮡtx, ж<ast.BinaryExpression> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();
        var statementsᴷ = new stackAppend<ж<astꓸStatement>>();

        var left = node.Left;

        var symbol = node.Symbol;
        if (symbol == nil || (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAssignment) == 0) {
            return;
        }

        var ns = ast.GetLeftmostAccessExpression(left);
        if (ns == nil || ns.Kind != ast.KindIdentifier) {
            return;
        }

        var declaration = tx.resolver.GetReferencedValueDeclaration(ns);
        if (declaration == nil) {
            return;
        }

        if (tx.shouldStripInternal(declaration)) {
            return;
        }

        if (ast.IsVariableDeclaration(declaration) && declaration.Type() != nil) {
            return;
        }

        if (ast.IsFunctionDeclaration(declaration) && declaration.FunctionLikeData().FullSignature != nil) {
            return;
        }

        if (ast.IsVariableDeclaration(declaration) && !ast.IsFunctionLike(declaration.Initializer())) {
            return;
        }

        var host = declaration.Symbol();
        if (host == nil) {
            return;
        }

        var name = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(ns.Text());
        @string property = tx.tryGetPropertyName(left);
        if (property == ""u8 || !scanner.IsIdentifierText(property, core.LanguageVariantStandard)) {
            return;
        }

        var hostId = tx.getExpandoHostId(declaration);

        if (ast.IsDeclaration(declaration) && isDeclarationAndNotVisible(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).EmitContext(), tx.resolver, declaration)) {
            tx.deferredExpandoAssignments[hostId] = append(tx.deferredExpandoAssignments[hostId], Ꮡnode);
            return;
        }

        if (ast.IsFunctionDeclaration(declaration) && !shouldEmitFunctionProperties(ref (declaration.AsFunctionDeclaration()).DerefOrNull())) {
            return;
        }

        Ꮡtx.transformExpandoHost(name, declaration);

        var exportName = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(property);
        var localName = tx.tryGetNameOfAssignedExpression(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (localName == nil && !tx.resolver.IsNameResolvable(tx.enclosingDeclaration, property) && !ast.IsNonContextualKeyword(scanner.StringToToken(exportName.Text()))) {
            localName = exportName;
        }
        if (localName == nil || ast.IsNonContextualKeyword(scanner.StringToToken(localName.Text()))) {
            localName = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }

        var (_, cleanupDiagnosticContext) = Ꮡtx.setupDiagnosticContext(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var cleanupDiagnosticContextʗ1 = cleanupDiagnosticContext;
        defer(cleanupDiagnosticContextʗ1, ref ᒐ);

        if (ast.IsIdentifier(node.Right)) {
            var result = tx.transformBinaryExpressionToExportDeclaration(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), exportName);
            tx.expandoMembers[hostId] = append(tx.expandoMembers[hostId], result);
            return;
        }

        var preexistingExpandoHasExport = core.Some<ж<ast.Node>>(tx.expandoMembers[hostId], ast.IsExportDeclaration);
        ж<ast.ModifierList> varModifiers = default;

        if (preexistingExpandoHasExport) {
            varModifiers = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(ast.CreateModifiersFromModifierFlags(ast.ModifierFlagsExport, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewModifier));
        }

        var synthesizedNamespace = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleDeclaration(nil, ast.KindNamespaceKeyword, name, nil, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleBlock(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([])));
        synthesizedNamespace.Parent = tx.enclosingDeclaration;
        var declarationData = synthesizedNamespace.DeclarationData();
        declarationData.Symbol = host;
        var containerData = synthesizedNamespace.LocalsContainerData();
        containerData.Locals = new ast.SymbolTable(0);
        containerData.Locals[localName.Text()] = symbol;

        var oldEnclosing = tx.enclosingDeclaration;
        tx.enclosingDeclaration = synthesizedNamespace;
        var oldEnclosingʗ1 = oldEnclosing;
        defer(() => {
            Ꮡtx.Value.enclosingDeclaration = oldEnclosingʗ1;
        }, ref ᒐ);

        var statements = slice<ж<astꓸStatement>>([
            Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
                varModifiers,
                Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                    Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                        Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(localName, nil, tx.ensureType(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false), nil)
                    ])),
                    ast.NodeFlagsNone))
        ]);

        if (localName.Text() != exportName.Text()) {
            var namedExports = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(
                slice<ж<ast.Node>>([
                    Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(false, localName, exportName)
                ])));
            statements = append(statementsᴷ, statements, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(nil, false, namedExports, nil, nil));
        }

        if (len(statements) > 1 && !preexistingExpandoHasExport) {
            foreach (var (_, decl) in range(tx.expandoMembers[hostId])) {
                var modifierFlags = (ast.ModifierFlags)(ast.ModifierFlagsExport | ast.GetCombinedModifierFlags(decl));
                decl.AsMutable().SetModifiers(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(ast.CreateModifiersFromModifierFlags(modifierFlags, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewModifier)));
            }
        }
        tx.expandoMembers[hostId] = appendꓸꓸꓸ(tx.expandoMembers[hostId], statements);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static ast.NodeId getExpandoHostId(this ref DeclarationTransformer tx, ж<astꓸDeclaration> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    var root = core.IfElse(ast.IsVariableDeclaration(Ꮡdeclaration), declaration.Parent.Parent, Ꮡdeclaration);
    var id = ast.GetNodeId(tx.Transformer.EmitContext().MostOriginal(root));
    return id;
}

internal static void transformExpandoHost(this ж<DeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡname, ж<astꓸDeclaration> Ꮡdeclaration) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var name = ref Ꮡname.DerefOrNull();
        ref var declaration = ref Ꮡdeclaration.DerefOrNull();

        var root = core.IfElse(ast.IsVariableDeclaration(Ꮡdeclaration), declaration.Parent.Parent, Ꮡdeclaration);
        var id = tx.getExpandoHostId(Ꮡdeclaration);

        if (tx.expandoHosts[id, ꟷ] is (_, true)) {
            return;
        }

        var saveNeedsDeclare = tx.needsDeclare;
        tx.needsDeclare = true;

        var modifierFlags = tx.ensureModifierFlags(root);
        var defaultExport = (ast.ModifierFlags)(modifierFlags & ast.ModifierFlagsExport) != 0 && (ast.ModifierFlags)(modifierFlags & ast.ModifierFlagsDefault) != 0;

        tx.needsDeclare = saveNeedsDeclare;

        if (defaultExport) {
            modifierFlags |= (ast.ModifierFlags)(ast.ModifierFlagsAmbient);
            modifierFlags ^= (ast.ModifierFlags)(ast.ModifierFlagsDefault);
            modifierFlags ^= (ast.ModifierFlags)(ast.ModifierFlagsExport);
        }

        var (_, cleanupDiagnosticContext) = Ꮡtx.setupDiagnosticContext(Ꮡdeclaration);
        var cleanupDiagnosticContextʗ1 = cleanupDiagnosticContext;
        defer(cleanupDiagnosticContextʗ1, ref ᒐ);

        var modifiers = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(ast.CreateModifiersFromModifierFlags(modifierFlags, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().NewModifier));
        var replacement = new slice<ж<ast.Node>>(0);

        if (ast.IsFunctionDeclaration(Ꮡdeclaration)) {
            var (typeParameters, parameters, asteriskToken) = extractExpandoHostParams(Ꮡdeclaration);
            replacement = append(replacement, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(declaration.AsFunctionDeclaration(), modifiers, asteriskToken, declaration.Name(), tx.ensureTypeParams(Ꮡdeclaration, typeParameters), tx.updateParamList(Ꮡdeclaration, parameters), tx.ensureType(Ꮡdeclaration, false), nil, nil));
        } else if (ast.IsVariableDeclaration(Ꮡdeclaration) && ast.IsFunctionExpressionOrArrowFunction(declaration.Initializer())) {
            var fn = declaration.Initializer();
            var (typeParameters, parameters, asteriskToken) = extractExpandoHostParams(fn);
            replacement = append(replacement, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionDeclaration(modifiers, asteriskToken, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(name.Text()), tx.ensureTypeParams(fn, typeParameters), tx.updateParamList(fn, parameters), tx.ensureType(fn, false), nil, nil));
        } else {
            tx.expandoHosts[id] = Ꮡtx.transformTopLevelDeclaration(Ꮡdeclaration);
            return;
        }

        tx.state.reportExpandoFunctionErrors(Ꮡdeclaration);

        if (defaultExport) {
            if (ast.IsSourceFile(declaration.Parent)) {
                tx.resultHasExternalModuleIndicator = true;
            }
            tx.resultHasScopeMarker = true;
            replacement = append(replacement, Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(nil, false, nil, Ꮡname));
        }

        tx.expandoHosts[id] = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(replacement);
        if (tx.lateStatementReplacementMap[id, ꟷ] is (_, true)) {
            tx.lateStatementReplacementMap[id] = Ꮡtx.createFullExpandoBlock(id);
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> createFullExpandoBlock(this ж<DeclarationTransformer> Ꮡtx, ast.NodeId id) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (tx.deferredExpandoAssignments[id, ꟷ] is (var deferred, true)) {
        delete(tx.deferredExpandoAssignments, id);
        foreach (var (_, assignment) in range(deferred)) {
            Ꮡtx.transformExpandoAssignment(assignment);
        }
    }
    var n = tx.expandoHosts[id];
    {
        var (addOns, ok) = tx.expandoMembers[id, ꟷ]; if (ok) {
            ж<ast.ModifierList> modifiers = default;
            ж<ast.Node> name = default;
            slice<ж<ast.Node>> host = default;
            if (n != nil && n.Kind == ast.KindSyntaxList) {
                foreach (var c in range<ж<ast.Node>>(n.AsSyntaxList().of(ast.SyntaxList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).IterChildren().Invoke)) {
                    if (c.Name() != nil) {
                        name = c.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory()));
                        if (c.Modifiers() != nil) {
                            modifiers = c.Modifiers().Clone(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory());
                        }
                        break;
                    }
                }
                host = n.AsSyntaxList().Value.Children;
            } else if (n != nil) {
                name = n.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory()));
                if (n.Modifiers() != nil) {
                    modifiers = n.Modifiers().Clone(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory());
                }
                host = [n];
            }
            if (name != nil) {
                var moduleDecl = Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleDeclaration(
                    modifiers,
                    ast.KindNamespaceKeyword,
                    name,
                    nil,
                    Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModuleBlock(Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(addOns)));
                var members = append(host, moduleDecl);
                return Ꮡtx.of(DeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(members);
            }
        }
    }
    return n;
}

} partial class declarations_package {

internal static (ж<astꓸTypeParameterList> typeParameters, ж<astꓸParameterList> parameters, ж<astꓸTokenNode> asteriskToken) extractExpandoHostParams(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindFunctionExpression: {
        var fn = node.AsFunctionExpression();
        return ((~fn).TypeParameters, (~fn).Parameters, fn.AsteriskToken);
    }
    case var ᴛ when ᴛ == ast.KindArrowFunction: {
        var fn = node.AsArrowFunction();
        return ((~fn).TypeParameters, (~fn).Parameters, fn.AsteriskToken);
    }
    default: {
        var fn = node.AsFunctionDeclaration();
        return ((~fn).TypeParameters, (~fn).Parameters, fn.AsteriskToken);
    }}

}

} partial class declarationsᴛDeclarationTransformer {

internal static @string tryGetPropertyName(this ref DeclarationTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsElementAccessExpression(Ꮡnode)) {
        return tx.resolver.GetElementAccessExpressionName(node.AsElementAccessExpression());
    }
    if (ast.IsPropertyAccessExpression(Ꮡnode)) {
        return node.Name().Text();
    }
    return "";
}

} // end declarationsᴛDeclarationTransformer
