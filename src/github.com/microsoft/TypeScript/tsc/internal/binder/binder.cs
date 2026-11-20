namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strconv = strconv_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸany = Span<any>;
using static binder_package;

partial class binder_package {

partial struct ContainerFlags /*num:int32*/;

public static ContainerFlags ContainerFlagsNone => 0;
public static ContainerFlags ContainerFlagsIsContainer => /* 1 << 0 */ 1;
public static ContainerFlags ContainerFlagsIsBlockScopedContainer => /* 1 << 1 */ 2;
public static ContainerFlags ContainerFlagsIsControlFlowContainer => /* 1 << 2 */ 4;
public static ContainerFlags ContainerFlagsIsFunctionLike => /* 1 << 3 */ 8;
public static ContainerFlags ContainerFlagsIsFunctionExpression => /* 1 << 4 */ 16;
public static ContainerFlags ContainerFlagsHasLocals => /* 1 << 5 */ 32;
public static ContainerFlags ContainerFlagsIsInterface => /* 1 << 6 */ 64;
public static ContainerFlags ContainerFlagsIsObjectLiteralOrClassExpressionMethodOrAccessor => /* 1 << 7 */ 128;
public static ContainerFlags ContainerFlagsIsThisContainer => /* 1 << 8 */ 256;
public static ContainerFlags ContainerFlagsPropagatesThisKeyword => /* 1 << 9 */ 512;

partial struct ExpandoAssignmentInfo {
    internal ж<ast.Node> node;
    internal ж<ast.Node> container;
    internal ж<ast.Node> blockScopeContainer;
}

partial struct Binder {
    internal ж<ast.SourceFile> @file;
    internal Func<ж<ast.Node>, bool> bindFunc;
    internal ж<ast.FlowNode> unreachableFlow;
    internal ж<ast.Node> container;
    internal ж<ast.Node> thisContainer;
    internal ж<ast.Node> blockScopeContainer;
    internal ж<ast.Node> lastContainer;
    internal ж<ast.FlowNode> currentFlow;
    internal ж<astꓸFlowLabel> currentBreakTarget;
    internal ж<astꓸFlowLabel> currentContinueTarget;
    internal ж<astꓸFlowLabel> currentReturnTarget;
    internal ж<astꓸFlowLabel> currentTrueTarget;
    internal ж<astꓸFlowLabel> currentFalseTarget;
    internal ж<astꓸFlowLabel> currentExceptionTarget;
    internal ж<ast.FlowNode> preSwitchCaseFlow;
    internal ж<ActiveLabel> activeLabelList;
    internal ast.NodeFlags emitFlags;
    internal bool seenThisKeyword;
    internal bool hasExplicitReturn;
    internal bool hasFlowEffects;
    internal bool inAssignmentPattern;
    internal bool seenParseError;
    internal nint symbolCount;
    internal collections.Set<ж<ast.Symbol>> notConstEnumOnlyModules;
    internal core.Arena<ast.Symbol> symbolArena;
    internal core.Arena<ast.FlowNode> flowNodeArena;
    internal core.Arena<ast.FlowList> flowListArena;
    internal core.Arena<ж<ast.Node>> singleDeclarationsArena;
    internal slice<ExpandoAssignmentInfo> expandoAssignments;
}

partial struct ActiveLabel {
    internal ж<ActiveLabel> next;
    internal ж<astꓸFlowLabel> breakTarget;
    internal ж<astꓸFlowLabel> continueTarget;
    internal @string name;
    internal bool referenced;
}

} partial class binderᴛActiveLabel {

public static ж<ast.FlowNode> BreakTarget(this ref ActiveLabel label) { return label.breakTarget; }
public static ж<ast.FlowNode> ContinueTarget(this ref ActiveLabel label) { return label.continueTarget; }

} partial class binder_package {

public static void BindSourceFile(ж<ast.SourceFile> @file) {
    if (!@file.IsBound()) {
        bindSourceFile(@file);
    }
}

[GoLazy] partial class lazyᴛbinderPool { internal static readonly ж<sync.Pool> ᏑbinderPool = new StaticBox<sync.Pool>(new sync.Pool(
    New: () => {
        var b = Ꮡ(new Binder());
        var bʗ1 = b;
                b.bindFunc = (ж<ast.Node> p1) => bʗ1.bind(p1);
        return b.OrTypedNil();
    }
)); }
internal static ref sync.Pool binderPool => ref ((StaticBox<sync.Pool>)ᏑbinderPool).ValueSlot;

internal static ж<Binder> getBinder() {
    return ᏑbinderPool.Get()._<ж<Binder>>();
}

internal static void putBinder(ж<Binder> Ꮡb) {
    ref var b = ref Ꮡb.DerefOrNull();

    b = new Binder(bindFunc: b.bindFunc);
    ᏑbinderPool.Put(Ꮡb.OrTypedNil());
}

internal static void bindSourceFile(ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    Ꮡfile.BindOnce(() => {
        GoFrame ᒐ = default;
        try {
            var b = getBinder();
            defer(putBinder, b, ref ᒐ);
            b.@file = Ꮡfile;
            b.unreachableFlow = b.newFlowNode(ast.FlowFlagsUnreachable);
            b.bind(Ꮡfile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            b.bindDeferredExpandoAssignments();
            Ꮡfile.Value.SymbolCount = b.symbolCount;
        }
        catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
        finally { ᒐ.Run(); }
    });
}

} partial class binderᴛBinder {

internal static ж<ast.Symbol> newSymbol(this ref Binder b, ast.SymbolFlags flags, @string name) {
    b.symbolCount++;
    var result = b.symbolArena.New();
    result.Flags = flags;
    result.Name = name;
    return result;
}

internal static ж<ast.Symbol> declareSymbol(this ref Binder b, ast.SymbolTable symbolTable, ж<ast.Symbol> parent, ж<ast.Node> node, ast.SymbolFlags includes, ast.SymbolFlags excludes) {
    return b.declareSymbolEx(symbolTable, parent, node, includes, excludes, false, false);
}

internal static ж<ast.Symbol> declareSymbolEx(this ref Binder b, ast.SymbolTable symbolTable, ж<ast.Symbol> parent, ж<ast.Node> Ꮡnode, ast.SymbolFlags includes, ast.SymbolFlags excludes, bool isReplaceableByMethod, bool isComputedName) {
    ref var node = ref Ꮡnode.DerefOrNull();

    debug.Assert(isComputedName || !ast.HasDynamicName(Ꮡnode));
    var isDefaultExport = ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsDefault) || ast.IsExportSpecifier(Ꮡnode) && ast.ModuleExportNameIsDefault(node.AsExportSpecifier().Name());
    @string name = default;
    switch (ᐧ) {
    case {} when isComputedName: {
        name = ast.InternalSymbolNameComputed;
        break;
    }
    case {} when isDefaultExport && parent != nil: {
        name = ast.InternalSymbolNameDefault;
        break;
    }
    default: {
        name = b.getDeclarationName(Ꮡnode);
        break;
    }}

    ж<ast.Symbol> symbol = default;
    if (name == ast.InternalSymbolNameMissing) {
        symbol = b.newSymbol(ast.SymbolFlagsNone, ast.InternalSymbolNameMissing);
    } else {
        symbol = symbolTable[name];
        if (symbol == nil) {
            symbol = b.newSymbol(ast.SymbolFlagsNone, name);
            symbolTable[name] = symbol;
            if (isReplaceableByMethod) {
                symbol.Flags |= (ast.SymbolFlags)(ast.SymbolFlagsReplaceableByMethod);
            }
        } else if (isReplaceableByMethod && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsReplaceableByMethod) == 0) {
            return symbol;
        } else if ((ast.SymbolFlags)(symbol.Flags & excludes) != 0) {
            if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsReplaceableByMethod) != 0) {
                symbol = b.newSymbol(ast.SymbolFlagsNone, name);
                symbolTable[name] = symbol;
            } else if (!((ast.SymbolFlags)(includes & ast.SymbolFlagsVariable) != 0 && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAssignment) != 0 || (ast.SymbolFlags)(includes & ast.SymbolFlagsAssignment) != 0 && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsVariable) != 0)) {
                ж<diagnostics.Message> message = default;
                if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsBlockScopedVariable) != 0) {
                    message = diagnostics.Cannot_redeclare_block_scoped_variable_0;
                } else {
                    message = diagnostics.Duplicate_identifier_0;
                }
                var messageNeedsName = true;
                if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsEnum) != 0 || (ast.SymbolFlags)(includes & ast.SymbolFlagsEnum) != 0) {
                    message = diagnostics.Enum_declarations_can_only_merge_with_namespace_or_other_enum_declarations;
                    messageNeedsName = false;
                }
                var multipleDefaultExports = false;
                if (len(symbol.Declarations) != 0) {
                    if (isDefaultExport) {
                        message = diagnostics.A_module_cannot_have_multiple_default_exports;
                        messageNeedsName = false;
                        multipleDefaultExports = true;
                    } else {
                        if (len(symbol.Declarations) != 0 && ast.IsExportAssignment(Ꮡnode) && !node.AsExportAssignment().IsExportEquals) {
                            message = diagnostics.A_module_cannot_have_multiple_default_exports;
                            messageNeedsName = false;
                            multipleDefaultExports = true;
                        }
                    }
                }
                ж<ast.Node> declarationName = ast.GetNameOfDeclaration(Ꮡnode);
                if (declarationName == nil) {
                    declarationName = Ꮡnode;
                }
                ж<ast.Diagnostic> diag = default;
                if (messageNeedsName) {
                    diag = b.createDiagnosticForNode(declarationName, message, b.getDisplayName(Ꮡnode));
                } else {
                    diag = b.createDiagnosticForNode(declarationName, message);
                }
                if (ast.IsTypeAliasDeclaration(Ꮡnode) && ast.NodeIsMissing(node.Type()) && ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsExport) && (ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsAlias | ast.SymbolFlagsType) | ast.SymbolFlagsNamespace))) != 0) {
                    diag.AddRelatedInfo(b.createDiagnosticForNode(Ꮡnode, diagnostics.Did_you_mean_0, "export type { " + node.AsTypeAliasDeclaration().Name().Text() + " }"));
                }
                foreach (var (index, declaration) in range(symbol.Declarations)) {
                    ж<ast.Node> decl = ast.GetNameOfDeclaration(declaration);
                    if (decl == nil) {
                        decl = declaration;
                    }
                    ж<ast.Diagnostic> d = default;
                    if (messageNeedsName) {
                        d = b.createDiagnosticForNode(decl, message, b.getDisplayName(declaration));
                    } else {
                        d = b.createDiagnosticForNode(decl, message);
                    }
                    if (multipleDefaultExports) {
                        d.AddRelatedInfo(b.createDiagnosticForNode(declarationName, core.IfElse(index == 0, diagnostics.Another_export_default_is_here, diagnostics.X_and_here)));
                    }
                    b.addDiagnostic(d);
                    if (multipleDefaultExports) {
                        diag.AddRelatedInfo(b.createDiagnosticForNode(decl, diagnostics.The_first_export_default_is_here));
                    }
                }
                b.addDiagnostic(diag);
                if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAccessor) != 0 && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAccessor) != (ast.SymbolFlags)(includes & ast.SymbolFlagsAccessor)) {
                    symbol.Flags |= (ast.SymbolFlags)(ast.SymbolFlagsAccessor);
                }
                symbol = b.newSymbol(ast.SymbolFlagsNone, name);
            }
        }
    }
    b.addDeclarationToSymbol(symbol, Ꮡnode, includes);
    if (symbol.Parent == nil) {
        symbol.Parent = parent;
    } else if (symbol.Parent != parent) {
        throw panic("Existing symbol parent should match new one");
    }
    return symbol;
}

internal static @string getDeclarationName(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsExportAssignment(Ꮡnode)) {
        return core.IfElse(node.AsExportAssignment().IsExportEquals, ast.InternalSymbolNameExportEquals, ast.InternalSymbolNameDefault);
    }
    var name = ast.GetNameOfDeclaration(Ꮡnode);
    if (name != nil) {
        if (ast.IsAmbientModule(Ꮡnode)) {
            @string moduleName = name.Text();
            if (ast.IsGlobalScopeAugmentation(Ꮡnode)) {
                return ast.InternalSymbolNameGlobal;
            }
            var pattern = core.TryParsePattern(moduleName); if (pattern.IsValid() && pattern.StarIndex >= 0) {
                var attributes = node.AsModuleDeclaration().Value.Attributes; if (attributes != nil) {
                    return ast.InternalSymbolNamePrefix + "\"" + moduleName + "\"pattern@" + strconv.FormatUint((uint64)ast.GetNodeId(attributes), 10);
                }
            }
            return "\""u8 + moduleName + "\""u8;
        }
        if (ast.IsPrivateIdentifier(name)) {
            var containingClass = ast.GetContainingClass(Ꮡnode);
            if (containingClass == nil) {
                return ast.InternalSymbolNameMissing;
            }
            return GetSymbolNameForPrivateIdentifier(containingClass.Symbol(), name.Text());
        }
        if (ast.IsPropertyNameLiteral(name) || ast.IsJsxNamespacedName(name)) {
            return name.Text();
        }
        if (ast.IsComputedPropertyName(name)) {
            var nameExpression = name.Expression();
            if (ast.IsStringOrNumericLiteralLike(nameExpression)) {
                return nameExpression.Text();
            }
            if (ast.IsSignedNumericLiteral(nameExpression)) {
                var unaryExpression = nameExpression.AsPrefixUnaryExpression();
                return scanner.TokenToString(unaryExpression.Operator) + unaryExpression.Operand.Text();
            }
            throw panic("Only computed properties with literal names have declaration names");
        }
        return ast.InternalSymbolNameMissing;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindConstructor: {
        return ast.InternalSymbolNameConstructor;
    }
    case var ᴛ when ᴛ == ast.KindFunctionType || ᴛ == ast.KindCallSignature: {
        return ast.InternalSymbolNameCall;
    }
    case var ᴛ when ᴛ == ast.KindConstructorType || ᴛ == ast.KindConstructSignature: {
        return ast.InternalSymbolNameNew;
    }
    case var ᴛ when ᴛ == ast.KindIndexSignature: {
        return ast.InternalSymbolNameIndex;
    }
    case var ᴛ when ᴛ == ast.KindExportDeclaration: {
        return ast.InternalSymbolNameExportStar;
    }
    case var ᴛ when ᴛ == ast.KindSourceFile || ᴛ == ast.KindBinaryExpression: {
        return ast.InternalSymbolNameExportEquals;
    }}

    return ast.InternalSymbolNameMissing;
}

internal static @string getDisplayName(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var nameNode = node.Name();
    if (nameNode != nil) {
        return scanner.DeclarationNameToString(nameNode);
    }
    @string name = b.getDeclarationName(Ꮡnode);
    if (name != ast.InternalSymbolNameMissing) {
        return name;
    }
    return "(Missing)";
}

} partial class binder_package {

public static @string GetSymbolNameForPrivateIdentifier(ж<ast.Symbol> containingClassSymbol, @string description) {
    return ast.InternalSymbolNamePrefix + "#" + strconv.Itoa((nint)ast.GetSymbolId(containingClassSymbol)) + "@" + description;
}

} partial class binderᴛBinder {

internal static ж<ast.Symbol> declareModuleMember(this ref Binder b, ж<ast.Node> Ꮡnode, ast.SymbolFlags symbolFlags, ast.SymbolFlags symbolExcludes) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var container = b.container;
    var hasExportModifier = (ast.ModifierFlags)(ast.GetCombinedModifierFlags(Ꮡnode) & ast.ModifierFlagsExport) != 0 || ast.IsImplicitlyExportedJSDocDeclaration(Ꮡnode);
    if ((ast.SymbolFlags)(symbolFlags & ast.SymbolFlagsAlias) != 0) {
        if (node.Kind == ast.KindExportSpecifier || (node.Kind == ast.KindImportEqualsDeclaration && hasExportModifier)) {
            return b.declareSymbol(ast.GetExports(container.Symbol()), container.Symbol(), Ꮡnode, symbolFlags, symbolExcludes);
        }
        return b.declareSymbol(ast.GetLocals(container), nil, Ꮡnode, symbolFlags, symbolExcludes);
    }
    if (!ast.IsAmbientModule(Ꮡnode) && (hasExportModifier || (ast.NodeFlags)(container.Flags & ast.NodeFlagsExportContext) != 0)) {
        if (!ast.IsLocalsContainer(container) || (ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsDefault) && b.getDeclarationName(Ꮡnode) == ast.InternalSymbolNameMissing)) {
            return b.declareSymbol(ast.GetExports(container.Symbol()), container.Symbol(), Ꮡnode, symbolFlags, symbolExcludes);
        }
        var exportKind = ast.SymbolFlagsNone;
        if ((ast.SymbolFlags)(symbolFlags & ast.SymbolFlagsValue) != 0) {
            exportKind = ast.SymbolFlagsExportValue;
        }
        var local = b.declareSymbol(ast.GetLocals(container), nil, Ꮡnode, exportKind, symbolExcludes);
        local.ExportSymbol = b.declareSymbol(ast.GetExports(container.Symbol()), container.Symbol(), Ꮡnode, symbolFlags, symbolExcludes);
        node.ExportableData().LocalSymbol = local;
        return local;
    }
    return b.declareSymbol(ast.GetLocals(container), nil, Ꮡnode, symbolFlags, symbolExcludes);
}

internal static ж<ast.Symbol> declareClassMember(this ref Binder b, ж<ast.Node> node, ast.SymbolFlags symbolFlags, ast.SymbolFlags symbolExcludes) {
    if (ast.IsStatic(node)) {
        return b.declareSymbol(ast.GetExports(b.container.Symbol()), b.container.Symbol(), node, symbolFlags, symbolExcludes);
    }
    return b.declareSymbol(ast.GetMembers(b.container.Symbol()), b.container.Symbol(), node, symbolFlags, symbolExcludes);
}

internal static ж<ast.Symbol> declareSourceFileMember(this ref Binder b, ж<ast.Node> node, ast.SymbolFlags symbolFlags, ast.SymbolFlags symbolExcludes) {
    if (ast.IsExternalModule(b.@file)) {
        return b.declareModuleMember(node, symbolFlags, symbolExcludes);
    }
    return b.declareSymbol(ast.GetLocals(b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), nil, node, symbolFlags, symbolExcludes);
}

internal static ж<ast.Symbol> declareSymbolAndAddToSymbolTable(this ref Binder b, ж<ast.Node> node, ast.SymbolFlags symbolFlags, ast.SymbolFlags symbolExcludes) {
    switch (b.container.Kind) {
    case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
        return b.declareModuleMember(node, symbolFlags, symbolExcludes);
    }
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        return b.declareSourceFileMember(node, symbolFlags, symbolExcludes);
    }
    case var ᴛ when ᴛ == ast.KindClassExpression || ᴛ == ast.KindClassDeclaration: {
        return b.declareClassMember(node, symbolFlags, symbolExcludes);
    }
    case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
        return b.declareSymbol(ast.GetExports(b.container.Symbol()), b.container.Symbol(), node, symbolFlags, symbolExcludes);
    }
    case var ᴛ when ᴛ == ast.KindTypeLiteral || ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindJsxAttributes: {
        return b.declareSymbol(ast.GetMembers(b.container.Symbol()), b.container.Symbol(), node, symbolFlags, symbolExcludes);
    }
    case var ᴛ when ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType || ᴛ == ast.KindCallSignature || ᴛ == ast.KindConstructSignature || ᴛ == ast.KindIndexSignature || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature || ᴛ == ast.KindConstructor || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindClassStaticBlockDeclaration || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindMappedType: {
        return b.declareSymbol(ast.GetLocals(b.container), nil, node, symbolFlags, symbolExcludes);
    }}

    throw panic("Unhandled case in declareSymbolAndAddToSymbolTable");
}

internal static ж<ast.FlowNode> newFlowNode(this ref Binder b, ast.FlowFlags flags) {
    var result = b.flowNodeArena.New();
    result.Flags = flags;
    return result;
}

internal static ж<ast.FlowNode> newFlowNodeEx(this ref Binder b, ast.FlowFlags flags, ж<ast.Node> node, ж<ast.FlowNode> antecedent) {
    var result = b.newFlowNode(flags);
    result.Node = node;
    result.Antecedent = antecedent;
    return result;
}

internal static ж<astꓸFlowLabel> createLoopLabel(this ref Binder b) {
    return b.newFlowNode(ast.FlowFlagsLoopLabel);
}

internal static ж<astꓸFlowLabel> createBranchLabel(this ref Binder b) {
    return b.newFlowNode(ast.FlowFlagsBranchLabel);
}

internal static ж<ast.FlowNode> createReduceLabel(this ref Binder b, ж<astꓸFlowLabel> target, ж<ast.FlowList> antecedents, ж<ast.FlowNode> antecedent) {
    return b.newFlowNodeEx(ast.FlowFlagsReduceLabel, ast.NewFlowReduceLabelData(target, antecedents), antecedent);
}

internal static ж<ast.FlowNode> createFlowCondition(this ref Binder b, ast.FlowFlags flags, ж<ast.FlowNode> Ꮡantecedent, ж<ast.Node> Ꮡexpression) {
    ref var antecedent = ref Ꮡantecedent.DerefOrNull();
    ref var expression = ref Ꮡexpression.DerefOrNull();

    if ((ast.FlowFlags)(antecedent.Flags & ast.FlowFlagsUnreachable) != 0) {
        return Ꮡantecedent;
    }
    if (Ꮡexpression == nil) {
        if ((ast.FlowFlags)(flags & ast.FlowFlagsTrueCondition) != 0) {
            return Ꮡantecedent;
        }
        return b.unreachableFlow;
    }
    if ((expression.Kind == ast.KindTrueKeyword && (ast.FlowFlags)(flags & ast.FlowFlagsFalseCondition) != 0 || expression.Kind == ast.KindFalseKeyword && (ast.FlowFlags)(flags & ast.FlowFlagsTrueCondition) != 0) && !ast.IsExpressionOfOptionalChainRoot(Ꮡexpression) && !ast.IsNullishCoalesce(expression.Parent)) {
        return b.unreachableFlow;
    }
    if (!isNarrowingExpression(Ꮡexpression)) {
        return Ꮡantecedent;
    }
    setFlowNodeReferenced(ref (Ꮡantecedent).DerefOrNull());
    return b.newFlowNodeEx(flags, Ꮡexpression, Ꮡantecedent);
}

internal static ж<ast.FlowNode> createFlowMutation(this ref Binder b, ast.FlowFlags flags, ж<ast.FlowNode> antecedent, ж<ast.Node> node) {
    setFlowNodeReferenced(ref (antecedent).DerefOrNull());
    b.hasFlowEffects = true;
    var result = b.newFlowNodeEx(flags, node, antecedent);
    if (b.currentExceptionTarget != nil) {
        b.addAntecedent(b.currentExceptionTarget, result);
    }
    return result;
}

internal static ж<ast.FlowNode> createFlowSwitchClause(this ref Binder b, ж<ast.FlowNode> antecedent, ж<ast.Node> switchStatement, nint clauseStart, nint clauseEnd) {
    setFlowNodeReferenced(ref (antecedent).DerefOrNull());
    return b.newFlowNodeEx(ast.FlowFlagsSwitchClause, ast.NewFlowSwitchClauseData(switchStatement, clauseStart, clauseEnd), antecedent);
}

internal static ж<ast.FlowNode> createFlowCall(this ref Binder b, ж<ast.FlowNode> antecedent, ж<ast.Node> node) {
    setFlowNodeReferenced(ref (antecedent).DerefOrNull());
    b.hasFlowEffects = true;
    return b.newFlowNodeEx(ast.FlowFlagsCall, node, antecedent);
}

internal static ж<ast.FlowList> newFlowList(this ref Binder b, ж<ast.FlowNode> head, ж<ast.FlowList> tail) {
    var result = b.flowListArena.New();
    result.Flow = head;
    result.Next = tail;
    return result;
}

internal static ж<ast.FlowList> combineFlowLists(this ref Binder b, ж<ast.FlowList> Ꮡhead, ж<ast.FlowList> tail) {
    ref var head = ref Ꮡhead.DerefOrNull();

    if (Ꮡhead == nil) {
        return tail;
    }
    return b.newFlowList(head.Flow, b.combineFlowLists(head.Next, tail));
}

internal static slice<ж<ast.Node>> newSingleDeclaration(this ref Binder b, ж<ast.Node> declaration) {
    return b.singleDeclarationsArena.NewSlice1(declaration);
}

} partial class binder_package {

internal static void setFlowNodeReferenced(ref ast.FlowNode flow) {
    if ((ast.FlowFlags)(flow.Flags & ast.FlowFlagsReferenced) == 0) {
        flow.Flags |= (ast.FlowFlags)(ast.FlowFlagsReferenced);
    } else {
        flow.Flags |= (ast.FlowFlags)(ast.FlowFlagsShared);
    }
}

} partial class binderᴛBinder {

internal static void addAntecedent(this ref Binder b, ж<astꓸFlowLabel> Ꮡlabel, ж<ast.FlowNode> Ꮡantecedent) {
    ref var label = ref Ꮡlabel.DerefOrNull();
    ref var antecedent = ref Ꮡantecedent.DerefOrNull();

    if ((ast.FlowFlags)(antecedent.Flags & ast.FlowFlagsUnreachable) != 0) {
        return;
    }
    ж<ast.FlowList> last = default;
    for (var list = label.Antecedents; list != nil; list = list.Next) {
        if (list.Flow == Ꮡantecedent) {
            return;
        }
        last = list;
    }
    if (last == nil) {
        label.Antecedents = b.newFlowList(Ꮡantecedent, nil);
    } else {
        last.Next = b.newFlowList(Ꮡantecedent, nil);
    }
    setFlowNodeReferenced(ref (Ꮡantecedent).DerefOrNull());
}

internal static ж<ast.FlowNode> finishFlowLabel(this ref Binder b, ж<astꓸFlowLabel> Ꮡlabel) {
    ref var label = ref Ꮡlabel.DerefOrNull();

    if (label.Antecedents == nil) {
        return b.unreachableFlow;
    }
    if (label.Antecedents.Next == nil) {
        return label.Antecedents.Flow;
    }
    return Ꮡlabel;
}

internal static bool bind(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return false;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        node.AsIdentifier().FlowNode = b.currentFlow;
        b.checkContextualIdentifier(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindThisKeyword || ᴛ == ast.KindSuperKeyword: {
        if (node.Kind == ast.KindThisKeyword) {
            b.seenThisKeyword = true;
        }
        node.AsKeywordExpression().FlowNode = b.currentFlow;
        break;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        if (b.currentFlow != nil && ast.IsPartOfTypeQuery(Ꮡnode)) {
            node.AsQualifiedName().FlowNode = b.currentFlow;
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindMetaProperty: {
        node.AsMetaProperty().FlowNode = b.currentFlow;
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrivateIdentifier: {
        b.checkPrivateIdentifier(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        if (b.currentFlow != nil && isNarrowableReference(Ꮡnode)) {
            setFlowNode(Ꮡnode, b.currentFlow);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        switch (ast.GetAssignmentDeclarationKind(Ꮡnode)) {
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindModuleExports: {
            b.bindModuleExportsAssignment(Ꮡnode);
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindExportsProperty: {
            b.bindExportsOrObjectDefineProperty(Ꮡnode);
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindProperty: {
            b.bindExpandoPropertyAssignment(Ꮡnode);
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindThisProperty: {
            b.bindThisPropertyAssignment(Ꮡnode);
            break;
        }}

        b.checkStrictModeBinaryExpression(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCatchClause: {
        b.checkStrictModeCatchClause(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindDeleteExpression: {
        b.checkStrictModeDeleteExpression(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPostfixUnaryExpression: {
        b.checkStrictModePostfixUnaryExpression(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        b.checkStrictModePrefixUnaryExpression(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindWithStatement: {
        b.checkStrictModeWithStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindLabeledStatement: {
        b.checkStrictModeLabeledStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindThisType: {
        b.seenThisKeyword = true;
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeParameter: {
        b.bindTypeParameter(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        b.bindParameter(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        b.bindVariableDeclarationOrBindingElement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        node.AsBindingElement().FlowNode = b.currentFlow;
        b.bindVariableDeclarationOrBindingElement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertySignature: {
        b.bindPropertyWorker(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindShorthandPropertyAssignment: {
        b.bindPropertyOrMethodOrAccessor(Ꮡnode, ast.SymbolFlagsProperty, ast.SymbolFlagsPropertyExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindEnumMember: {
        b.bindPropertyOrMethodOrAccessor(Ꮡnode, ast.SymbolFlagsEnumMember, ast.SymbolFlagsEnumMemberExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallSignature || ᴛ == ast.KindConstructSignature || ᴛ == ast.KindIndexSignature: {
        b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsSignature, ast.SymbolFlagsNone);
        break;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature: {
        b.bindPropertyOrMethodOrAccessor(Ꮡnode, (ast.SymbolFlags)(ast.SymbolFlagsMethod | getOptionalSymbolFlagForNode(Ꮡnode)), core.IfElse(ast.IsObjectLiteralMethod(Ꮡnode), ast.SymbolFlagsValue, ast.SymbolFlagsMethodExcludes));
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        b.bindFunctionDeclaration(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructor: {
        b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsConstructor, ast.SymbolFlagsNone);
        break;
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        b.bindPropertyOrMethodOrAccessor(Ꮡnode, ast.SymbolFlagsGetAccessor, ast.SymbolFlagsGetAccessorExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        b.bindPropertyOrMethodOrAccessor(Ꮡnode, ast.SymbolFlagsSetAccessor, ast.SymbolFlagsSetAccessorExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType: {
        b.bindFunctionOrConstructorType(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeLiteral || ᴛ == ast.KindMappedType: {
        b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsTypeLiteral, ast.InternalSymbolNameType);
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsObjectLiteral, ast.InternalSymbolNameObject);
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction: {
        b.bindFunctionExpression(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassExpression || ᴛ == ast.KindClassDeclaration: {
        b.bindClassLikeDeclaration(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindInterfaceDeclaration: {
        b.bindBlockScopedDeclaration(Ꮡnode, ast.SymbolFlagsInterface, ast.SymbolFlagsInterfaceExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        switch (ast.GetAssignmentDeclarationKind(Ꮡnode)) {
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindObjectDefinePropertyValue: {
            b.bindExpandoPropertyAssignment(Ꮡnode);
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindObjectDefinePropertyExports: {
            b.bindExportsOrObjectDefineProperty(Ꮡnode);
            break;
        }}

        if (ast.IsInJSFile(Ꮡnode)) {
            b.bindCallExpression(Ꮡnode);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeAliasDeclaration: {
        b.bindBlockScopedDeclaration(Ꮡnode, ast.SymbolFlagsTypeAlias, ast.SymbolFlagsTypeAliasExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSTypeAliasDeclaration: {
        if (!ast.IsSourceFile(b.blockScopeContainer)) {
            b.bindBlockScopedDeclaration(Ꮡnode, ast.SymbolFlagsTypeAlias, ast.SymbolFlagsTypeAliasExcludes);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
        b.bindEnumDeclaration(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
        Ꮡb.bindModuleDeclaration(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindNamespaceImport || ᴛ == ast.KindImportSpecifier || ᴛ == ast.KindExportSpecifier: {
        b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsAlias, ast.SymbolFlagsAliasExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamespaceExportDeclaration: {
        b.bindNamespaceExportDeclaration(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportClause: {
        b.bindImportClause(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportDeclaration: {
        b.bindExportDeclaration(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        b.bindExportAssignment(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        b.bindSourceFileIfExternalModule();
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxAttributes: {
        b.bindJsxAttributes(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxAttribute: {
        b.bindJsxAttribute(Ꮡnode, ast.SymbolFlagsProperty, ast.SymbolFlagsPropertyExcludes);
        break;
    }}

    var thisNodeOrAnySubnodesHasError = (ast.NodeFlags)(node.Flags & ast.NodeFlagsThisNodeHasError) != 0;
    if (node.Kind > ast.KindLastToken) {
        var saveSeenParseError = b.seenParseError;
        b.seenParseError = false;
        var containerFlags = GetContainerFlags(Ꮡnode);
        if (containerFlags == ContainerFlagsNone) {
            Ꮡb.bindChildren(Ꮡnode);
        } else {
            Ꮡb.bindContainer(Ꮡnode, containerFlags);
        }
        if (b.seenParseError) {
            thisNodeOrAnySubnodesHasError = true;
        }
        b.seenParseError = saveSeenParseError;
    }
    if (thisNodeOrAnySubnodesHasError) {
        node.Flags |= (ast.NodeFlags)(ast.NodeFlagsThisNodeOrAnySubNodesHasError);
        b.seenParseError = true;
    }
    return false;
}

internal static void bindPropertyWorker(this ref Binder b, ж<ast.Node> node) {
    var isAutoAccessor = ast.IsAutoAccessorPropertyDeclaration(node);
    var includes = core.IfElse(isAutoAccessor, ast.SymbolFlagsAccessor, ast.SymbolFlagsProperty);
    var excludes = core.IfElse(isAutoAccessor, ast.SymbolFlagsAccessorExcludes, ast.SymbolFlagsPropertyExcludes);
    b.bindPropertyOrMethodOrAccessor(node, (ast.SymbolFlags)(includes | getOptionalSymbolFlagForNode(node)), excludes);
}

internal static void bindSourceFileIfExternalModule(this ref Binder b) {
    b.setExportContextFlag(b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (ast.IsExternalOrCommonJSModule(b.@file)) {
        b.bindSourceFileAsExternalModule();
    } else if (ast.IsJsonSourceFile(b.@file)) {
        b.bindSourceFileAsExternalModule();
        var originalSymbol = b.@file.Value.Symbol;
        b.declareSymbol(ast.GetSymbolTable((~b.@file).Symbol.ᏑExports), (~b.@file).Symbol, b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.SymbolFlagsProperty, ast.SymbolFlagsAll);
        b.@file.Value.Symbol = originalSymbol;
    }
}

internal static void bindSourceFileAsExternalModule(this ref Binder b) {
    b.bindAnonymousDeclaration(b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.SymbolFlagsValueModule, "\""u8 + tspath.RemoveFileExtension(b.@file.FileName()) + "\""u8);
}

internal static void bindModuleDeclaration(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    b.setExportContextFlag(Ꮡnode);
    if (ast.IsAmbientModule(Ꮡnode)) {
        if (ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsExport)) {
            b.errorOnFirstToken(Ꮡnode, diagnostics.X_export_modifier_cannot_be_applied_to_ambient_modules_and_module_augmentations_since_they_are_always_visible);
        }
        if (ast.IsModuleAugmentationExternal(Ꮡnode)) {
            b.declareModuleSymbol(Ꮡnode);
        } else {
            var name = node.AsModuleDeclaration().Name();
            var symbol = b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsValueModule, ast.SymbolFlagsValueModuleExcludes);

            if (ast.IsStringLiteral(name)) {
                var attributes = node.AsModuleDeclaration().Value.Attributes;
                var pattern = core.TryParsePattern(name.Text());
                if (!pattern.IsValid()) {
                    b.errorOnFirstToken(name, diagnostics.Pattern_0_can_have_at_most_one_Asterisk_character, name.Text());
                } else if (pattern.StarIndex >= 0) {
                    b.@file.PatternAmbientModules = append(b.@file.PatternAmbientModules, Ꮡ(new ast.PatternAmbientModule(Pattern: pattern, Symbol: symbol)));
                } else if (attributes != nil) {
                    b.errorOnNode(name, diagnostics.An_ambient_module_declaration_with_import_attributes_must_use_a_pattern_name_with_an_Asterisk_character);
                }
            }
        }
    } else {
        var state = b.declareModuleSymbol(Ꮡnode);
        if (state != ast.ModuleInstanceStateNonInstantiated) {
            var symbol = node.Symbol();
            var constEnumOnlyModule = ((ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsFunction | ast.SymbolFlagsClass) | ast.SymbolFlagsRegularEnum))) == 0) && state == ast.ModuleInstanceStateConstEnumOnly && !Ꮡb.ᏑnotConstEnumOnlyModules.Has(symbol);
            if (constEnumOnlyModule) {
                symbol.Flags |= (ast.SymbolFlags)(ast.SymbolFlagsConstEnumOnlyModule);
            } else {
                symbol.Flags &= unchecked((ast.SymbolFlags)~(ast.SymbolFlags)(ast.SymbolFlagsConstEnumOnlyModule));
                b.notConstEnumOnlyModules.Add(symbol);
            }
        }
    }
}

internal static ast.ModuleInstanceState declareModuleSymbol(this ref Binder b, ж<ast.Node> node) {
    var state = ast.GetModuleInstanceState(node);
    var instantiated = state != ast.ModuleInstanceStateNonInstantiated;
    b.declareSymbolAndAddToSymbolTable(node, core.IfElse(instantiated, ast.SymbolFlagsValueModule, ast.SymbolFlagsNamespaceModule), core.IfElse(instantiated, ast.SymbolFlagsValueModuleExcludes, ast.SymbolFlagsNamespaceModuleExcludes));
    return state;
}

internal static void bindNamespaceExportDeclaration(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Modifiers() != nil) {
        b.errorOnNode(Ꮡnode, diagnostics.Modifiers_cannot_appear_here);
    }
    switch (ᐧ) {
    case {} when !ast.IsSourceFile(node.Parent): {
        b.errorOnNode(Ꮡnode, diagnostics.Global_module_exports_may_only_appear_at_top_level);
        break;
    }
    case {} when !ast.IsExternalModule(node.Parent.AsSourceFile()): {
        b.errorOnNode(Ꮡnode, diagnostics.Global_module_exports_may_only_appear_in_module_files);
        break;
    }
    case {} when !node.Parent.AsSourceFile().IsDeclarationFile: {
        b.errorOnNode(Ꮡnode, diagnostics.Global_module_exports_may_only_appear_in_declaration_files);
        break;
    }
    default: {
        b.declareSymbol(ast.GetSymbolTable(b.@file.ᏑGlobalExports), (~b.@file).Symbol, Ꮡnode, ast.SymbolFlagsAlias, ast.SymbolFlagsAliasExcludes);
        break;
    }}

}

internal static void bindImportClause(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.AsImportClause().Name() != nil) {
        b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsAlias, ast.SymbolFlagsAliasExcludes);
    }
}

internal static void bindExportDeclaration(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsExportDeclaration();
    if (b.container.Symbol() == nil) {
        b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsExportStar, b.getDeclarationName(Ꮡnode));
    } else if (decl.ExportClause == nil) {
        b.declareSymbol(ast.GetExports(b.container.Symbol()), b.container.Symbol(), Ꮡnode, ast.SymbolFlagsExportStar, ast.SymbolFlagsNone);
    } else if (ast.IsNamespaceExport(decl.ExportClause)) {
        b.declareSymbol(ast.GetExports(b.container.Symbol()), b.container.Symbol(), decl.ExportClause, ast.SymbolFlagsAlias, ast.SymbolFlagsAliasExcludes);
    }
}

internal static void bindExportAssignment(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var container = b.container;
    if (container.Symbol() == nil && ast.IsExportAssignment(Ꮡnode)) {
        b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsValue, b.getDeclarationName(Ꮡnode));
    } else {
        var flags = core.IfElse(ast.ExpressionIsAlias(node.Expression()), ast.SymbolFlagsAlias, ast.SymbolFlagsProperty);
        var symbol = b.declareSymbol(ast.GetExports(container.Symbol()), container.Symbol(), Ꮡnode, flags, ast.SymbolFlagsAll);
        if (node.AsExportAssignment().IsExportEquals) {
            SetValueDeclaration(symbol, Ꮡnode);
        }
    }
}

internal static void bindJsxAttributes(this ref Binder b, ж<ast.Node> node) {
    b.bindAnonymousDeclaration(node, ast.SymbolFlagsObjectLiteral, ast.InternalSymbolNameJSXAttributes);
}

internal static void bindJsxAttribute(this ref Binder b, ж<ast.Node> node, ast.SymbolFlags symbolFlags, ast.SymbolFlags symbolExcludes) {
    b.declareSymbolAndAddToSymbolTable(node, symbolFlags, symbolExcludes);
}

internal static void setExportContextFlag(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0 && !b.hasExportDeclarations(Ꮡnode)) {
        node.Flags |= (ast.NodeFlags)(ast.NodeFlagsExportContext);
    } else {
        node.Flags &= unchecked((ast.NodeFlags)~(ast.NodeFlags)(ast.NodeFlagsExportContext));
    }
}

internal static bool hasExportDeclarations(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    slice<ж<ast.Node>> statements = default;
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        statements = node.Statements();
        break;
    }
    case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
        var body = node.Body();
        if (body != nil && ast.IsModuleBlock(body)) {
            statements = body.Statements();
        }
        break;
    }}

    return core.Some(statements, (ж<ast.Node> s) => ast.IsExportDeclaration(s) || ast.IsExportAssignment(s));
}

internal static void bindFunctionExpression(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!b.@file.IsDeclarationFile && (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0 && ast.IsAsyncFunction(Ꮡnode)) {
        b.emitFlags |= (ast.NodeFlags)(ast.NodeFlagsHasAsyncFunctions);
    }
    setFlowNode(Ꮡnode, b.currentFlow);
    @string bindingName = ast.InternalSymbolNameFunction;
    if (ast.IsFunctionExpression(Ꮡnode) && node.AsFunctionExpression().Name() != nil) {
        b.checkStrictModeFunctionName(Ꮡnode);
        bindingName = node.AsFunctionExpression().Name().Text();
    }
    b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsFunction, bindingName);
}

internal static void bindCallExpression(this ref Binder b, ж<ast.Node> node) {
    if (b.@file.CommonJSModuleIndicator == nil && ast.IsRequireCall(node, false)) {
        b.setCommonJSModuleIndicator(node);
    }
}

internal static bool setCommonJSModuleIndicator(this ref Binder b, ж<ast.Node> node) {
    if (b.@file.ExternalModuleIndicator != nil && b.@file.ExternalModuleIndicator != b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) {
        return false;
    }
    if (b.@file.CommonJSModuleIndicator == nil) {
        b.@file.CommonJSModuleIndicator = node;
        if (b.@file.ExternalModuleIndicator == nil) {
            b.bindSourceFileAsExternalModule();
        }
    }
    return true;
}

internal static void bindClassLikeDeclaration(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindClassDeclaration: {
        b.bindBlockScopedDeclaration(Ꮡnode, ast.SymbolFlagsClass, ast.SymbolFlagsClassExcludes);
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassExpression: {
        @string nameText = ast.InternalSymbolNameClass;
        if (name != nil) {
            nameText = name.Text();
        }
        b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsClass, nameText);
        break;
    }}

    var symbol = node.Symbol();
    var prototypeSymbol = b.newSymbol((ast.SymbolFlags)(ast.SymbolFlagsProperty | ast.SymbolFlagsPrototype), "prototype");
    var symbolExport = ast.GetExports(symbol)[prototypeSymbol.Name];
    if (symbolExport != nil) {
        b.errorOnNode(symbolExport.Declarations[0], diagnostics.Duplicate_identifier_0, ast.SymbolName(prototypeSymbol));
    }
    ast.GetExports(symbol)[prototypeSymbol.Name] = prototypeSymbol;
    prototypeSymbol.Parent = symbol;
}

internal static void bindPropertyOrMethodOrAccessor(this ref Binder b, ж<ast.Node> Ꮡnode, ast.SymbolFlags symbolFlags, ast.SymbolFlags symbolExcludes) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!b.@file.IsDeclarationFile && (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0 && ast.IsAsyncFunction(Ꮡnode)) {
        b.emitFlags |= (ast.NodeFlags)(ast.NodeFlagsHasAsyncFunctions);
    }
    if (b.currentFlow != nil && ast.IsObjectLiteralOrClassExpressionMethodOrAccessor(Ꮡnode)) {
        setFlowNode(Ꮡnode, b.currentFlow);
    }
    if (ast.HasDynamicName(Ꮡnode)) {
        b.bindAnonymousDeclaration(Ꮡnode, symbolFlags, ast.InternalSymbolNameComputed);
    } else {
        b.declareSymbolAndAddToSymbolTable(Ꮡnode, symbolFlags, symbolExcludes);
    }
}

internal static void bindFunctionOrConstructorType(this ref Binder b, ж<ast.Node> node) {
    var symbol = b.newSymbol(ast.SymbolFlagsSignature, b.getDeclarationName(node));
    b.addDeclarationToSymbol(symbol, node, ast.SymbolFlagsSignature);
    var typeLiteralSymbol = b.newSymbol(ast.SymbolFlagsTypeLiteral, ast.InternalSymbolNameType);
    b.addDeclarationToSymbol(typeLiteralSymbol, node, ast.SymbolFlagsTypeLiteral);
    typeLiteralSymbol.Members = new ast.SymbolTable(0);
    typeLiteralSymbol.Members[symbol.Name] = symbol;
}

internal static void addLateBoundAssignmentDeclarationToSymbol(this ref Binder b, ж<ast.Node> node, ж<ast.Symbol> symbol) {
    var exports = ast.GetExports(symbol);
    var assignmentSymbol = exports[ast.InternalSymbolNameAssignmentDeclaration];
    if (assignmentSymbol == nil) {
        assignmentSymbol = b.newSymbol(ast.SymbolFlagsNone, ast.InternalSymbolNameAssignmentDeclaration);
        exports[ast.InternalSymbolNameAssignmentDeclaration] = assignmentSymbol;
    }
    assignmentSymbol.Declarations = append(assignmentSymbol.Declarations, node);
}

internal static void bindModuleExportsAssignment(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (b.setCommonJSModuleIndicator(Ꮡnode)) {
        var container = b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        var flags = core.IfElse(ast.ExpressionIsAlias(node.AsBinaryExpression().Right), ast.SymbolFlagsAlias, ast.SymbolFlagsProperty);
        var symbol = b.declareSymbol(ast.GetExports(container.Symbol()), container.Symbol(), Ꮡnode, flags, 0);
        SetValueDeclaration(symbol, Ꮡnode);
    }
}

internal static void bindExpandoPropertyAssignment(this ref Binder b, ж<ast.Node> node) {
    b.expandoAssignments = append(b.expandoAssignments, new ExpandoAssignmentInfo(
        node: node,
        container: b.container,
        blockScopeContainer: b.blockScopeContainer
    ));
}

internal static void bindDeferredExpandoAssignments(this ref Binder b) {
    foreach (var (_, info) in range(b.expandoAssignments)) {
        b.container = info.container;
        b.blockScopeContainer = info.blockScopeContainer;
        b.bindDeferredExpandoAssignment(info.node);
    }
}

internal static void bindCommonJSTypeExports(this ref Binder b, ж<ast.Symbol> ᏑmoduleSymbol) {
    ref var moduleSymbol = ref ᏑmoduleSymbol.DerefOrNull();

    var moduleExports = moduleSymbol.Exports;
    var exportEquals = moduleExports[ast.InternalSymbolNameExportEquals]; if (exportEquals != nil) {
        foreach (var (_, symbol) in moduleExports) {
            if (symbol.Name != ast.InternalSymbolNameExportEquals && (ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsType | ast.SymbolFlagsNamespace))) != 0) {
                ast.GetExports(exportEquals)[symbol.Name] = symbol;
                exportEquals.Flags |= (ast.SymbolFlags)(ast.SymbolFlagsNamespaceModule);
            }
        }
    }
}

internal static void bindDeferredExpandoAssignment(this ref Binder b, ж<ast.Node> node) {
    var parent = getParentOfPropertyAssignment(node);
    var symbol = b.lookupEntity(parent, b.blockScopeContainer);
    if (symbol == nil) {
        symbol = b.lookupEntity(parent, b.container);
    }
    symbol = getInitializerSymbol(symbol); if (symbol != nil) {
        if (ast.HasDynamicName(node)) {
            b.bindAnonymousDeclaration(node, (ast.SymbolFlags)(ast.SymbolFlagsProperty | ast.SymbolFlagsAssignment), ast.InternalSymbolNameComputed);
            b.addLateBoundAssignmentDeclarationToSymbol(node, symbol);
        } else {
            var exports = ast.GetExports(symbol);
            var existing = exports[b.getDeclarationName(node)]; if (existing == nil || (ast.SymbolFlags)(existing.Flags & ast.SymbolFlagsAssignment) != 0) {
                b.declareSymbol(exports, symbol, node, (ast.SymbolFlags)(ast.SymbolFlagsProperty | ast.SymbolFlagsAssignment), ast.SymbolFlagsPropertyExcludes);
            }
        }
    }
}

} partial class binder_package {

internal static ж<ast.Node> getParentOfPropertyAssignment(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return node.AsBinaryExpression().Left.Expression();
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        return node.Arguments()[0];
    }}

    throw panic("Unhandled case in getParentOfPropertyAssignment");
}

} partial class binderᴛBinder {

internal static void bindExportsOrObjectDefineProperty(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (b.setCommonJSModuleIndicator(Ꮡnode)) {
        var container = b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        var flags = core.IfElse(ast.IsBinaryExpression(Ꮡnode) && ast.ExpressionIsAlias(node.AsBinaryExpression().Right), ast.SymbolFlagsAlias, ast.SymbolFlagsFunctionScopedVariable);
        b.declareSymbol(ast.GetExports(container.Symbol()), container.Symbol(), Ꮡnode, flags, ast.SymbolFlagsFunctionScopedVariableExcludes);
    }
}

} partial class binder_package {

internal static ж<ast.Symbol> getInitializerSymbol(ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡsymbol == nil || symbol.ValueDeclaration == nil) {
        return default;
    }
    var declaration = symbol.ValueDeclaration;
    switch (ᐧ) {
    case {} when ast.IsFunctionDeclaration(declaration) || ast.IsInJSFile(declaration) && ast.IsClassDeclaration(declaration): {
        return Ꮡsymbol;
    }
    case {} when ast.IsVariableDeclaration(declaration) && ((ast.NodeFlags)(declaration.Parent.Flags & ast.NodeFlagsConst) != 0 || ast.IsInJSFile(declaration)): {
        var initializer = declaration.Initializer();
        if (ast.IsExpandoInitializer(declaration, initializer)) {
            return initializer.Symbol();
        }
        break;
    }
    case {} when ast.IsBinaryExpression(declaration) && ast.IsInJSFile(declaration): {
        var initializer = declaration.AsBinaryExpression().Right;
        if (ast.IsExpandoInitializer(declaration, initializer)) {
            return initializer.Symbol();
        }
        break;
    }}

    return default;
}

} partial class binderᴛBinder {

internal static void bindThisPropertyAssignment(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsInJSFile(Ꮡnode)) {
        return;
    }
    var bin = node.AsBinaryExpression();
    if (ast.IsPropertyAccessExpression(bin.Left) && ast.IsPrivateIdentifier(bin.Left.AsPropertyAccessExpression().Name()) || b.thisContainer == nil) {
        return;
    }
    var (classSymbol, symbolTable) = b.getThisClassAndSymbolTable(); if (symbolTable != nil) {
        if (ast.HasDynamicName(Ꮡnode)) {
            b.declareSymbolEx(symbolTable, classSymbol, Ꮡnode, ast.SymbolFlagsProperty, ast.SymbolFlagsNone, true, true);
            b.addLateBoundAssignmentDeclarationToSymbol(Ꮡnode, classSymbol);
        } else {
            b.declareSymbolEx(symbolTable, classSymbol, Ꮡnode, (ast.SymbolFlags)(ast.SymbolFlagsProperty | ast.SymbolFlagsAssignment), ast.SymbolFlagsNone, true, false);
        }
    } else if (b.thisContainer.Kind != ast.KindFunctionDeclaration && b.thisContainer.Kind != ast.KindFunctionExpression) {
        throw panic("Unhandled case in bindThisPropertyAssignment: " + b.thisContainer.Kind.String());
    }
}

internal static (ж<ast.Symbol> classSymbol, ast.SymbolTable symbolTable) getThisClassAndSymbolTable(this ref Binder b) {
    ж<ast.Symbol> classSymbol = default;
    ast.SymbolTable symbolTable = default;

    if (b.thisContainer == nil) {
        return (default, default);
    }
    switch (b.thisContainer.Kind) {
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression: {
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructor || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindClassStaticBlockDeclaration: {
        classSymbol = b.thisContainer.Parent.Symbol();
        if (ast.IsStatic(b.thisContainer)) {
            symbolTable = ast.GetExports(classSymbol);
        } else {
            symbolTable = ast.GetMembers(classSymbol);
        }
        break;
    }}

    return (classSymbol, symbolTable);
}

internal static void bindEnumDeclaration(this ref Binder b, ж<ast.Node> node) {
    if (ast.IsEnumConst(node)) {
        b.bindBlockScopedDeclaration(node, ast.SymbolFlagsConstEnum, ast.SymbolFlagsConstEnumExcludes);
    } else {
        b.bindBlockScopedDeclaration(node, ast.SymbolFlagsRegularEnum, ast.SymbolFlagsRegularEnumExcludes);
    }
}

internal static void bindVariableDeclarationOrBindingElement(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    b.checkStrictModeEvalOrArguments(Ꮡnode, node.Name());
    var name = node.Name(); if (name != nil && !ast.IsBindingPattern(name)) {
        switch (ᐧ) {
        case {} when ast.IsVariableDeclarationInitializedToRequire(Ꮡnode): {
            b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsAlias, ast.SymbolFlagsAliasExcludes);
            break;
        }
        case {} when ast.IsBlockOrCatchScoped(Ꮡnode): {
            b.bindBlockScopedDeclaration(Ꮡnode, ast.SymbolFlagsBlockScopedVariable, ast.SymbolFlagsBlockScopedVariableExcludes);
            break;
        }
        case {} when ast.IsPartOfParameterDeclaration(Ꮡnode): {
            b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsFunctionScopedVariable, ast.SymbolFlagsParameterExcludes);
            break;
        }
        default: {
            b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsFunctionScopedVariable, ast.SymbolFlagsFunctionScopedVariableExcludes);
            break;
        }}

    }
}

internal static void bindParameter(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsParameterDeclaration();
    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0) {
        b.checkStrictModeEvalOrArguments(Ꮡnode, decl.Name());
    }
    if (ast.IsBindingPattern(decl.Name())) {
        nint index = slices.Index(node.Parent.Parameters(), Ꮡnode);
        b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsFunctionScopedVariable, "__"u8 + strconv.Itoa(index));
    } else {
        b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsFunctionScopedVariable, ast.SymbolFlagsParameterExcludes);
    }
    if (ast.IsParameterPropertyDeclaration(Ꮡnode, node.Parent)) {
        var classDeclaration = node.Parent.Parent;
        var flags = (ast.SymbolFlags)(ast.SymbolFlagsProperty | core.IfElse((~decl).QuestionToken != nil, ast.SymbolFlagsOptional, ast.SymbolFlagsNone));
        b.declareSymbol(ast.GetMembers(classDeclaration.Symbol()), classDeclaration.Symbol(), Ꮡnode, flags, ast.SymbolFlagsPropertyExcludes);
    }
}

internal static void bindFunctionDeclaration(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!b.@file.IsDeclarationFile && (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0 && ast.IsAsyncFunction(Ꮡnode)) {
        b.emitFlags |= (ast.NodeFlags)(ast.NodeFlagsHasAsyncFunctions);
    }
    b.checkStrictModeFunctionName(Ꮡnode);
    b.bindBlockScopedDeclaration(Ꮡnode, ast.SymbolFlagsFunction, ast.SymbolFlagsFunctionExcludes);
}

internal static ж<ast.Node> getInferTypeContainer(this ref Binder b, ж<ast.Node> node) {
    var extendsType = ast.FindAncestor(node, (ж<ast.Node> n) => {
        var parent = n.Parent;
        return parent != nil && ast.IsConditionalTypeNode(parent) && parent.AsConditionalTypeNode().ExtendsType == n;
    });
    if (extendsType != nil) {
        return extendsType.Parent;
    }
    return default;
}

internal static void bindAnonymousDeclaration(this ref Binder b, ж<ast.Node> node, ast.SymbolFlags symbolFlags, @string name) {
    var symbol = b.newSymbol(symbolFlags, name);
    if ((ast.SymbolFlags)(symbolFlags & ((ast.SymbolFlags)(ast.SymbolFlagsEnumMember | ast.SymbolFlagsClassMember))) != 0) {
        symbol.Parent = b.container.Symbol();
    }
    b.addDeclarationToSymbol(symbol, node, symbolFlags);
}

internal static void bindBlockScopedDeclaration(this ref Binder b, ж<ast.Node> node, ast.SymbolFlags symbolFlags, ast.SymbolFlags symbolExcludes) {
    var exprᴛ1 = b.blockScopeContainer.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindModuleDeclaration) { matchᴛ1 = true;
        b.declareModuleMember(node, symbolFlags, symbolExcludes);
    }
    else if (exprᴛ1 == ast.KindSourceFile) { matchᴛ1 = true;
        do {
            if (ast.IsExternalOrCommonJSModule(b.container.AsSourceFile())) {
                b.declareModuleMember(node, symbolFlags, symbolExcludes);
                break;
            }
            fallthrough = true;
        } while (false);
    }
    if (fallthrough || !matchᴛ1) { /* default: */
        b.declareSymbol(ast.GetLocals(b.blockScopeContainer), nil, node, symbolFlags, symbolExcludes);
    }

}

internal static void bindTypeParameter(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Parent.Kind == ast.KindInferType) {
        var container = b.getInferTypeContainer(node.Parent);
        if (container != nil) {
            b.declareSymbol(ast.GetLocals(container), nil, Ꮡnode, ast.SymbolFlagsTypeParameter, ast.SymbolFlagsTypeParameterExcludes);
        } else {
            b.bindAnonymousDeclaration(Ꮡnode, ast.SymbolFlagsTypeParameter, b.getDeclarationName(Ꮡnode));
        }
    } else {
        b.declareSymbolAndAddToSymbolTable(Ꮡnode, ast.SymbolFlagsTypeParameter, ast.SymbolFlagsTypeParameterExcludes);
    }
}

internal static ж<ast.Symbol> lookupEntity(this ref Binder b, ж<ast.Node> Ꮡnode, ж<ast.Node> container) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsIdentifier(Ꮡnode)) {
        return b.lookupName(node.Text(), container);
    }
    if (node.Expression().Kind == ast.KindThisKeyword) {
        var (_, symbolTable) = b.getThisClassAndSymbolTable(); if (symbolTable != nil) {
            var name = ast.GetElementOrPropertyAccessName(Ꮡnode); if (name != nil) {
                return symbolTable[name.Text()];
            }
        }
        return default;
    }
    var symbol = getInitializerSymbol(b.lookupEntity(node.Expression(), container)); if (symbol != nil && symbol.Exports != nil) {
        var name = ast.GetElementOrPropertyAccessName(Ꮡnode); if (name != nil) {
            return symbol.Exports[name.Text()];
        }
    }
    return default;
}

internal static ж<ast.Symbol> lookupName(this ref Binder b, @string name, ж<ast.Node> Ꮡcontainer) {
    ref var container = ref Ꮡcontainer.DerefOrNull();

    var localsContainer = container.LocalsContainerData(); if (localsContainer != nil) {
        var local = localsContainer.Locals[name]; if (local != nil) {
            return core.OrElse(local.ExportSymbol, local);
        }
    }
    var declaration = container.DeclarationData(); if (declaration != nil && declaration.Symbol != nil) {
        return declaration.Symbol.Exports[name];
    }
    return default;
}

internal static void checkContextualIdentifier(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (len(b.@file.Diagnostics()) == 0 && (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0 && (ast.NodeFlags)(node.Flags & ast.NodeFlagsJSDoc) == 0 && !ast.IsIdentifierName(Ꮡnode)) {
        var originalKeywordKind = scanner.GetIdentifierToken(node.Text());
        if (originalKeywordKind == ast.KindIdentifier) {
            return;
        }
        if (originalKeywordKind >= ast.KindFirstFutureReservedWord && originalKeywordKind <= ast.KindLastFutureReservedWord) {
            b.errorOnNode(Ꮡnode, b.getStrictModeIdentifierMessage(Ꮡnode), scanner.DeclarationNameToString(Ꮡnode));
        } else if (originalKeywordKind == ast.KindAwaitKeyword) {
            if (ast.IsExternalModule(b.@file) && ast.IsInTopLevelContext(Ꮡnode)) {
                b.errorOnNode(Ꮡnode, diagnostics.Identifier_expected_0_is_a_reserved_word_at_the_top_level_of_a_module, scanner.DeclarationNameToString(Ꮡnode));
            } else if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAwaitContext) != 0) {
                b.errorOnNode(Ꮡnode, diagnostics.Identifier_expected_0_is_a_reserved_word_that_cannot_be_used_here, scanner.DeclarationNameToString(Ꮡnode));
            }
        } else if (originalKeywordKind == ast.KindYieldKeyword && (ast.NodeFlags)(node.Flags & ast.NodeFlagsYieldContext) != 0) {
            b.errorOnNode(Ꮡnode, diagnostics.Identifier_expected_0_is_a_reserved_word_that_cannot_be_used_here, scanner.DeclarationNameToString(Ꮡnode));
        }
    }
}

internal static void checkPrivateIdentifier(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Text() == "#constructor"u8) {
        if (len(b.@file.Diagnostics()) == 0) {
            b.errorOnNode(Ꮡnode, diagnostics.X_constructor_is_a_reserved_word, scanner.DeclarationNameToString(Ꮡnode));
        }
    }
}

internal static ж<diagnostics.Message> getStrictModeIdentifierMessage(this ref Binder b, ж<ast.Node> node) {
    if (ast.GetContainingClass(node) != nil) {
        return diagnostics.Identifier_expected_0_is_a_reserved_word_in_strict_mode_Class_definitions_are_automatically_in_strict_mode;
    }
    if (b.@file.ExternalModuleIndicator != nil) {
        return diagnostics.Identifier_expected_0_is_a_reserved_word_in_strict_mode_Modules_are_automatically_in_strict_mode;
    }
    return diagnostics.Identifier_expected_0_is_a_reserved_word_in_strict_mode;
}

} partial class binder_package {

internal static bool isUseStrictPrologueDirective(ж<ast.SourceFile> sourceFile, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    @string nodeText = scanner.GetSourceTextOfNodeFromSourceFile(sourceFile, node.Expression(), false);
    return nodeText == "\"use strict\""u8 || nodeText == "'use strict'"u8;
}

public static ж<ast.Node> FindUseStrictPrologue(ж<ast.SourceFile> sourceFile, slice<ж<ast.Node>> statements) {
    foreach (var (_, statement) in range(statements)) {
        if (ast.IsPrologueDirective(statement)) {
            if (isUseStrictPrologueDirective(sourceFile, statement)) {
                return statement;
            }
        } else {
            return default;
        }
    }

    return default;
}

} partial class binderᴛBinder {

internal static void checkStrictModeFunctionName(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0) {
        b.checkStrictModeEvalOrArguments(Ꮡnode, node.Name());
    }
}

internal static ж<diagnostics.Message> getStrictModeBlockScopeFunctionDeclarationMessage(this ref Binder b, ж<ast.Node> node) {
    if (ast.GetContainingClass(node) != nil) {
        return diagnostics.Function_declarations_are_not_allowed_inside_blocks_in_strict_mode_when_targeting_ES5_Class_definitions_are_automatically_in_strict_mode;
    }
    if (b.@file.ExternalModuleIndicator != nil) {
        return diagnostics.Function_declarations_are_not_allowed_inside_blocks_in_strict_mode_when_targeting_ES5_Modules_are_automatically_in_strict_mode;
    }
    return diagnostics.Function_declarations_are_not_allowed_inside_blocks_in_strict_mode_when_targeting_ES5;
}

internal static void checkStrictModeBinaryExpression(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsBinaryExpression();
    if (ast.IsLeftHandSideExpression(expr.Left) && ast.IsAssignmentOperator(expr.OperatorToken.Kind)) {
        b.checkStrictModeEvalOrArguments(Ꮡnode, expr.Left);
    }
}

internal static void checkStrictModeCatchClause(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var clause = node.AsCatchClause();
    if (clause.VariableDeclaration != nil) {
        b.checkStrictModeEvalOrArguments(Ꮡnode, clause.VariableDeclaration.AsVariableDeclaration().Name());
    }
}

internal static void checkStrictModeDeleteExpression(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsDeleteExpression();
    if ((~expr).Expression.Kind == ast.KindIdentifier) {
        b.errorOnNode((~expr).Expression, diagnostics.X_delete_cannot_be_called_on_an_identifier_in_strict_mode);
    }
}

internal static void checkStrictModePostfixUnaryExpression(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    b.checkStrictModeEvalOrArguments(Ꮡnode, node.AsPostfixUnaryExpression().Operand);
}

internal static void checkStrictModePrefixUnaryExpression(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsPrefixUnaryExpression();
    if (expr.Operator == ast.KindPlusPlusToken || expr.Operator == ast.KindMinusMinusToken) {
        b.checkStrictModeEvalOrArguments(Ꮡnode, expr.Operand);
    }
}

internal static void checkStrictModeWithStatement(this ref Binder b, ж<ast.Node> node) {
    b.errorOnFirstToken(node, diagnostics.X_with_statements_are_not_allowed_in_strict_mode);
}

internal static void checkStrictModeLabeledStatement(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var data = node.AsLabeledStatement();
    if (ast.IsDeclarationStatement((~data).Statement) || ast.IsVariableStatement((~data).Statement)) {
        b.errorOnFirstToken((~data).Label, diagnostics.A_label_is_not_allowed_here);
    }
}

} partial class binder_package {

internal static bool isEvalOrArgumentsIdentifier(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsIdentifier(Ꮡnode)) {
        @string text = node.Text();
        return text == "eval"u8 || text == "arguments"u8;
    }
    return false;
}

} partial class binderᴛBinder {

internal static void checkStrictModeEvalOrArguments(this ref Binder b, ж<ast.Node> contextNode, ж<ast.Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (Ꮡname != nil && isEvalOrArgumentsIdentifier(Ꮡname)) {
        b.errorOnNode(Ꮡname, b.getStrictModeEvalOrArgumentsMessage(contextNode), name.Text());
    }
}

internal static ж<diagnostics.Message> getStrictModeEvalOrArgumentsMessage(this ref Binder b, ж<ast.Node> node) {
    if (ast.GetContainingClass(node) != nil) {
        return diagnostics.Code_contained_in_a_class_is_evaluated_in_JavaScript_s_strict_mode_which_does_not_allow_this_use_of_0_For_more_information_see_https_Colon_Slash_Slashdeveloper_mozilla_org_Slashen_US_Slashdocs_SlashWeb_SlashJavaScript_SlashReference_SlashStrict_mode;
    }
    if (b.@file.ExternalModuleIndicator != nil) {
        return diagnostics.Invalid_use_of_0_Modules_are_automatically_in_strict_mode;
    }
    return diagnostics.Invalid_use_of_0_in_strict_mode;
}

internal static void bindContainer(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode, ContainerFlags containerFlags) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var saveContainer = b.container;
    var saveThisContainer = b.thisContainer;
    var savedBlockScopeContainer = b.blockScopeContainer;
    if ((ContainerFlags)(containerFlags & ContainerFlagsIsContainer) != 0) {
        b.container = Ꮡnode;
        b.blockScopeContainer = Ꮡnode;
        if ((ContainerFlags)(containerFlags & ContainerFlagsHasLocals) != 0) {
            b.addToContainerChain(Ꮡnode);
        }
    } else if ((ContainerFlags)(containerFlags & ContainerFlagsIsBlockScopedContainer) != 0) {
        b.blockScopeContainer = Ꮡnode;
        b.addToContainerChain(Ꮡnode);
    }
    if ((ContainerFlags)(containerFlags & ContainerFlagsIsThisContainer) != 0) {
        b.thisContainer = Ꮡnode;
    }
    if ((ContainerFlags)(containerFlags & ContainerFlagsIsControlFlowContainer) != 0) {
        var saveCurrentFlow = b.currentFlow;
        var saveBreakTarget = b.currentBreakTarget;
        var saveContinueTarget = b.currentContinueTarget;
        var saveReturnTarget = b.currentReturnTarget;
        var saveExceptionTarget = b.currentExceptionTarget;
        var saveActiveLabelList = b.activeLabelList;
        var saveHasExplicitReturn = b.hasExplicitReturn;
        var saveSeenThisKeyword = b.seenThisKeyword;
        var isImmediatelyInvoked = ((ContainerFlags)(containerFlags & ContainerFlagsIsFunctionExpression) != 0 && !ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsAsync) && !isGeneratorFunctionExpression(Ꮡnode) && ast.GetImmediatelyInvokedFunctionExpression(Ꮡnode) != nil) || node.Kind == ast.KindClassStaticBlockDeclaration;
        if (!isImmediatelyInvoked) {
            var flowStart = b.newFlowNode(ast.FlowFlagsStart);
            b.currentFlow = flowStart;
            if ((ContainerFlags)(containerFlags & ((ContainerFlags)(ContainerFlagsIsFunctionExpression | ContainerFlagsIsObjectLiteralOrClassExpressionMethodOrAccessor))) != 0) {
                flowStart.Node = Ꮡnode;
            }
        }
        if (isImmediatelyInvoked || node.Kind == ast.KindConstructor) {
            b.currentReturnTarget = b.newFlowNode(ast.FlowFlagsBranchLabel);
        } else {
            b.currentReturnTarget = default;
        }
        b.currentExceptionTarget = default;
        b.currentBreakTarget = default;
        b.currentContinueTarget = default;
        b.activeLabelList = default;
        b.hasExplicitReturn = false;
        b.seenThisKeyword = false;
        Ꮡb.bindChildren(Ꮡnode);
        node.Flags &= unchecked((ast.NodeFlags)~(ast.NodeFlags)((ast.NodeFlags)(ast.NodeFlagsReachabilityAndEmitFlags | ast.NodeFlagsContainsThis)));
        if ((ast.FlowFlags)(b.currentFlow.Flags & ast.FlowFlagsUnreachable) == 0 && (ContainerFlags)(containerFlags & ContainerFlagsIsFunctionLike) != 0) {
            var bodyData = node.BodyData();
            if (bodyData != nil && ast.NodeIsPresent(bodyData.Body)) {
                node.Flags |= (ast.NodeFlags)(ast.NodeFlagsHasImplicitReturn);
                if (b.hasExplicitReturn) {
                    node.Flags |= (ast.NodeFlags)(ast.NodeFlagsHasExplicitReturn);
                }
                bodyData.EndFlowNode = b.currentFlow;
            }
        }
        if (b.seenThisKeyword) {
            node.Flags |= (ast.NodeFlags)(ast.NodeFlagsContainsThis);
        }
        if (node.Kind == ast.KindSourceFile) {
            node.Flags |= (ast.NodeFlags)(b.emitFlags);
        }
        if (b.currentReturnTarget != nil) {
            b.addAntecedent(b.currentReturnTarget, b.currentFlow);
            b.currentFlow = b.finishFlowLabel(b.currentReturnTarget);
            if (node.Kind == ast.KindConstructor || node.Kind == ast.KindClassStaticBlockDeclaration) {
                setReturnFlowNode(Ꮡnode, b.currentFlow);
            }
        }
        if (!isImmediatelyInvoked) {
            b.currentFlow = saveCurrentFlow;
        }
        b.currentBreakTarget = saveBreakTarget;
        b.currentContinueTarget = saveContinueTarget;
        b.currentReturnTarget = saveReturnTarget;
        b.currentExceptionTarget = saveExceptionTarget;
        b.activeLabelList = saveActiveLabelList;
        b.hasExplicitReturn = saveHasExplicitReturn;
        if ((ContainerFlags)(containerFlags & ContainerFlagsPropagatesThisKeyword) != 0) {
            b.seenThisKeyword = saveSeenThisKeyword || b.seenThisKeyword;
        } else {
            b.seenThisKeyword = saveSeenThisKeyword;
        }
    } else if ((ContainerFlags)(containerFlags & ContainerFlagsIsInterface) != 0) {
        var saveSeenThisKeyword = b.seenThisKeyword;
        b.seenThisKeyword = false;
        Ꮡb.bindChildren(Ꮡnode);
        if (b.seenThisKeyword) {
            node.Flags |= (ast.NodeFlags)(ast.NodeFlagsContainsThis);
        } else {
            node.Flags &= unchecked((ast.NodeFlags)~(ast.NodeFlags)(ast.NodeFlagsContainsThis));
        }
        b.seenThisKeyword = saveSeenThisKeyword;
    } else {
        Ꮡb.bindChildren(Ꮡnode);
    }
    if (ast.IsSourceFile(Ꮡnode) && ast.IsInJSFile(Ꮡnode)) {
        foreach (var (_, statement) in range(node.Statements())) {
            if (ast.IsJSTypeAliasDeclaration(statement)) {
                b.bindBlockScopedDeclaration(statement, ast.SymbolFlagsTypeAlias, ast.SymbolFlagsTypeAliasExcludes);
            }
        }
        if (b.@file.CommonJSModuleIndicator != nil) {
            b.declareCommonJSVariable("module");
            b.declareCommonJSVariable("exports");
        }
    }
    if (ast.IsSourceFile(Ꮡnode) && ast.IsExternalOrCommonJSModule(node.AsSourceFile()) || ast.IsAmbientModule(Ꮡnode)) {
        b.bindCommonJSTypeExports(node.Symbol());
    }
    b.container = saveContainer;
    b.thisContainer = saveThisContainer;
    b.blockScopeContainer = savedBlockScopeContainer;
}

internal static void declareCommonJSVariable(this ref Binder b, @string name) {
    var locals = ast.GetLocals(b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (locals[name] == nil) {
        var symbol = b.newSymbol((ast.SymbolFlags)(ast.SymbolFlagsFunctionScopedVariable | ast.SymbolFlagsModuleExports), name);
        symbol.Declarations = b.newSingleDeclaration(b.@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        symbol.ValueDeclaration = symbol.Declarations[0];
        if (name == "module"u8) {
            var exportsProperty = b.newSymbol((ast.SymbolFlags)(ast.SymbolFlagsModuleExports | ast.SymbolFlagsProperty), "exports");
            exportsProperty.Declarations = symbol.Declarations;
            exportsProperty.ValueDeclaration = symbol.ValueDeclaration;
            exportsProperty.Parent = symbol;
            symbol.Members = new ast.SymbolTable(1);
            symbol.Members["exports"] = exportsProperty;
        }
        locals[name] = symbol;
    }
}

internal static void bindChildren(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var saveInAssignmentPattern = b.inAssignmentPattern;
    b.inAssignmentPattern = false;

    if (b.currentFlow == b.unreachableFlow) {
        var flowNodeData = node.FlowNodeData(); if (flowNodeData != nil) {
            flowNodeData.FlowNode = default;
        }
        if (ast.IsPotentiallyExecutableNode(Ꮡnode)) {
            node.Flags |= (ast.NodeFlags)(ast.NodeFlagsUnreachable);
        }
        b.bindEachChild(Ꮡnode);
        b.inAssignmentPattern = saveInAssignmentPattern;
        return;
    }

    if (ast.KindFirstStatement <= node.Kind && node.Kind <= ast.KindLastStatement) {
        var flowNodeData = node.FlowNodeData(); if (flowNodeData != nil) {
            flowNodeData.FlowNode = b.currentFlow;
        }
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindWhileStatement: {
        Ꮡb.bindWhileStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindDoStatement: {
        Ꮡb.bindDoStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        Ꮡb.bindForStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement: {
        Ꮡb.bindForInOrForOfStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindIfStatement: {
        Ꮡb.bindIfStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindReturnStatement: {
        Ꮡb.bindReturnStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindThrowStatement: {
        Ꮡb.bindThrowStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindBreakStatement: {
        Ꮡb.bindBreakStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindContinueStatement: {
        Ꮡb.bindContinueStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindTryStatement: {
        Ꮡb.bindTryStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindSwitchStatement: {
        Ꮡb.bindSwitchStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseBlock: {
        Ꮡb.bindCaseBlock(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseClause || ᴛ == ast.KindDefaultClause: {
        Ꮡb.bindCaseOrDefaultClause(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindExpressionStatement: {
        Ꮡb.bindExpressionStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindLabeledStatement: {
        Ꮡb.bindLabeledStatement(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        b.bindPrefixUnaryExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPostfixUnaryExpression: {
        b.bindPostfixUnaryExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        if (ast.IsDestructuringAssignment(Ꮡnode)) {
            b.inAssignmentPattern = saveInAssignmentPattern;
            Ꮡb.bindDestructuringAssignmentFlow(Ꮡnode);
            return;
        }
        Ꮡb.bindBinaryExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindDeleteExpression: {
        b.bindDeleteExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindConditionalExpression: {
        Ꮡb.bindConditionalExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        b.bindVariableDeclarationFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        Ꮡb.bindAccessExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        Ꮡb.bindCallExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindNonNullExpression: {
        Ꮡb.bindNonNullExpressionFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        var sourceFile = node.AsSourceFile();
        Ꮡb.bindEachStatementFunctionsFirst((~sourceFile).Statements);
        Ꮡb.bind(sourceFile.EndOfFileToken);
        break;
    }
    case var ᴛ when ᴛ == ast.KindBlock || ᴛ == ast.KindModuleBlock: {
        Ꮡb.bindEachStatementFunctionsFirst(node.StatementList());
        break;
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        Ꮡb.bindBindingElementFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        Ꮡb.bindParameterFlow(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindArrayLiteralExpression || ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindSpreadElement: {
        b.inAssignmentPattern = saveInAssignmentPattern;
        b.bindEachChild(Ꮡnode);
        break;
    }
    default: {
        b.bindEachChild(Ꮡnode);
        break;
    }}

    b.inAssignmentPattern = saveInAssignmentPattern;
}

internal static void bindEachChild(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    node.ForEachChild(b.bindFunc);
}

internal static void bindEach(this ж<Binder> Ꮡb, slice<ж<ast.Node>> nodes) {
    foreach (var (_, node) in range(nodes)) {
        Ꮡb.bind(node);
    }
}

internal static void bindNodeList(this ж<Binder> Ꮡb, ж<ast.NodeList> ᏑnodeList) {
    ref var nodeList = ref ᏑnodeList.DerefOrNull();

    if (ᏑnodeList != nil) {
        Ꮡb.bindEach(nodeList.Nodes);
    }
}

internal static void bindModifiers(this ж<Binder> Ꮡb, ж<ast.ModifierList> Ꮡmodifiers) {
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    if (Ꮡmodifiers != nil) {
        Ꮡb.bindEach(modifiers.Nodes);
    }
}

internal static void bindEachStatementFunctionsFirst(this ж<Binder> Ꮡb, ж<ast.NodeList> Ꮡstatements) {
    ref var statements = ref Ꮡstatements.DerefOrNull();

    foreach (var (_, node) in range(statements.Nodes)) {
        if (node.Kind == ast.KindFunctionDeclaration) {
            Ꮡb.bind(node);
        }
    }
    foreach (var (_, node) in range(statements.Nodes)) {
        if (node.Kind != ast.KindFunctionDeclaration) {
            Ꮡb.bind(node);
        }
    }
}

internal static ж<astꓸFlowLabel> setContinueTarget(this ref Binder b, ж<ast.Node> Ꮡnode, ж<astꓸFlowLabel> target) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var label = b.activeLabelList;
    while (label != nil && node.Parent.Kind == ast.KindLabeledStatement) {
        label.continueTarget = target;
        label = label.next;
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return target;
}

internal static void doWithConditionalBranches(this ж<Binder> Ꮡb, Func<ж<Binder>, ж<ast.Node>, bool> action, ж<ast.Node> value, ж<astꓸFlowLabel> trueTarget, ж<astꓸFlowLabel> falseTarget) {
    ref var b = ref Ꮡb.DerefOrNull();

    var savedTrueTarget = b.currentTrueTarget;
    var savedFalseTarget = b.currentFalseTarget;
    b.currentTrueTarget = trueTarget;
    b.currentFalseTarget = falseTarget;
    action(Ꮡb, value);
    b.currentTrueTarget = savedTrueTarget;
    b.currentFalseTarget = savedFalseTarget;
}

internal static void bindCondition(this ж<Binder> Ꮡb, ж<ast.Node> node, ж<astꓸFlowLabel> trueTarget, ж<astꓸFlowLabel> falseTarget) {
    ref var b = ref Ꮡb.DerefOrNull();

    Ꮡb.doWithConditionalBranches(((Func<ж<Binder>, ж<ast.Node>, bool>)(binderᴛBinder.bind)), node, trueTarget, falseTarget);
    if (node == nil || !isLogicalAssignmentExpression(node) && !ast.IsLogicalExpression(node) && !(ast.IsOptionalChain(node) && ast.IsOutermostOptionalChain(node))) {
        b.addAntecedent(trueTarget, b.createFlowCondition(ast.FlowFlagsTrueCondition, b.currentFlow, node));
        b.addAntecedent(falseTarget, b.createFlowCondition(ast.FlowFlagsFalseCondition, b.currentFlow, node));
    }
}

internal static void bindIterativeStatement(this ж<Binder> Ꮡb, ж<ast.Node> node, ж<astꓸFlowLabel> breakTarget, ж<astꓸFlowLabel> continueTarget) {
    ref var b = ref Ꮡb.DerefOrNull();

    var saveBreakTarget = b.currentBreakTarget;
    var saveContinueTarget = b.currentContinueTarget;
    b.currentBreakTarget = breakTarget;
    b.currentContinueTarget = continueTarget;
    Ꮡb.bind(node);
    b.currentBreakTarget = saveBreakTarget;
    b.currentContinueTarget = saveContinueTarget;
}

} partial class binder_package {

internal static bool isLogicalAssignmentExpression(ж<ast.Node> node) {
    return ast.IsLogicalOrCoalescingAssignmentExpression(ast.SkipParentheses(node));
}

} partial class binderᴛBinder {

internal static void bindAssignmentTargetFlow(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        foreach (var (_, e) in range(node.Elements())) {
            if (e.Kind == ast.KindSpreadElement) {
                b.bindAssignmentTargetFlow(e.Expression());
            } else {
                b.bindDestructuringTargetFlow(e);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        foreach (var (_, p) in range(node.Properties())) {
            switch (p.Kind) {
            case var ᴛ2 when ᴛ2 == ast.KindPropertyAssignment: {
                b.bindDestructuringTargetFlow(p.Initializer());
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindShorthandPropertyAssignment: {
                b.bindAssignmentTargetFlow(p.AsShorthandPropertyAssignment().Name());
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindSpreadAssignment: {
                b.bindAssignmentTargetFlow(p.Expression());
                break;
            }}

        }
        break;
    }
    default: {
        if (isNarrowableReference(Ꮡnode)) {
            b.currentFlow = b.createFlowMutation(ast.FlowFlagsAssignment, b.currentFlow, Ꮡnode);
        }
        break;
    }}

}

internal static void bindDestructuringTargetFlow(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBinaryExpression(Ꮡnode) && node.AsBinaryExpression().OperatorToken.Kind == ast.KindEqualsToken) {
        b.bindAssignmentTargetFlow(node.AsBinaryExpression().Left);
    } else {
        b.bindAssignmentTargetFlow(Ꮡnode);
    }
}

internal static void bindWhileStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsWhileStatement();
    var preWhileLabel = b.setContinueTarget(Ꮡnode, b.createLoopLabel());
    var preBodyLabel = b.createBranchLabel();
    var postWhileLabel = b.createBranchLabel();
    b.addAntecedent(preWhileLabel, b.currentFlow);
    b.currentFlow = preWhileLabel;
    Ꮡb.bindCondition((~stmt).Expression, preBodyLabel, postWhileLabel);
    b.currentFlow = b.finishFlowLabel(preBodyLabel);
    Ꮡb.bindIterativeStatement((~stmt).Statement, postWhileLabel, preWhileLabel);
    b.addAntecedent(preWhileLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(postWhileLabel);
}

internal static void bindDoStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsDoStatement();
    var preDoLabel = b.createLoopLabel();
    var preConditionLabel = b.setContinueTarget(Ꮡnode, b.createBranchLabel());
    var postDoLabel = b.createBranchLabel();
    b.addAntecedent(preDoLabel, b.currentFlow);
    b.currentFlow = preDoLabel;
    Ꮡb.bindIterativeStatement((~stmt).Statement, postDoLabel, preConditionLabel);
    b.addAntecedent(preConditionLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(preConditionLabel);
    Ꮡb.bindCondition((~stmt).Expression, preDoLabel, postDoLabel);
    b.currentFlow = b.finishFlowLabel(postDoLabel);
}

internal static void bindForStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsForStatement();
    Ꮡb.bind((~stmt).Initializer);
    if (b.currentFlow == b.unreachableFlow) {
        Ꮡb.bind(stmt.Condition);
        Ꮡb.bind((~stmt).Statement);
        Ꮡb.bind(stmt.Incrementor);
        return;
    }
    var preLoopLabel = b.setContinueTarget(Ꮡnode, b.createLoopLabel());
    var preBodyLabel = b.createBranchLabel();
    var preIncrementorLabel = b.createBranchLabel();
    var postLoopLabel = b.createBranchLabel();
    b.addAntecedent(preLoopLabel, b.currentFlow);
    b.currentFlow = preLoopLabel;
    Ꮡb.bindCondition(stmt.Condition, preBodyLabel, postLoopLabel);
    b.currentFlow = b.finishFlowLabel(preBodyLabel);
    Ꮡb.bindIterativeStatement((~stmt).Statement, postLoopLabel, preIncrementorLabel);
    b.addAntecedent(preIncrementorLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(preIncrementorLabel);
    Ꮡb.bind(stmt.Incrementor);
    b.addAntecedent(preLoopLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(postLoopLabel);
}

internal static void bindForInOrForOfStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsForInOrOfStatement();
    Ꮡb.bind((~stmt).Expression);
    if (b.currentFlow == b.unreachableFlow) {
        Ꮡb.bind((~stmt).Initializer);
        Ꮡb.bind((~stmt).Statement);
        return;
    }
    var preLoopLabel = b.setContinueTarget(Ꮡnode, b.createLoopLabel());
    var postLoopLabel = b.createBranchLabel();
    b.addAntecedent(preLoopLabel, b.currentFlow);
    b.currentFlow = preLoopLabel;
    if (node.Kind == ast.KindForOfStatement) {
        Ꮡb.bind(stmt.AwaitModifier);
    }
    b.addAntecedent(postLoopLabel, b.currentFlow);
    Ꮡb.bind((~stmt).Initializer);
    if ((~stmt).Initializer.Kind != ast.KindVariableDeclarationList) {
        b.bindAssignmentTargetFlow((~stmt).Initializer);
    }
    Ꮡb.bindIterativeStatement((~stmt).Statement, postLoopLabel, preLoopLabel);
    b.addAntecedent(preLoopLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(postLoopLabel);
}

internal static void bindIfStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsIfStatement();
    var thenLabel = b.createBranchLabel();
    var elseLabel = b.createBranchLabel();
    var postIfLabel = b.createBranchLabel();
    Ꮡb.bindCondition((~stmt).Expression, thenLabel, elseLabel);
    b.currentFlow = b.finishFlowLabel(thenLabel);
    Ꮡb.bind(stmt.ThenStatement);
    b.addAntecedent(postIfLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(elseLabel);
    Ꮡb.bind(stmt.ElseStatement);
    b.addAntecedent(postIfLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(postIfLabel);
}

internal static void bindReturnStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡb.bind(node.Expression());
    if (b.currentReturnTarget != nil) {
        b.addAntecedent(b.currentReturnTarget, b.currentFlow);
    }
    b.currentFlow = b.unreachableFlow;
    b.hasExplicitReturn = true;
    b.hasFlowEffects = true;
}

internal static void bindThrowStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡb.bind(node.Expression());
    b.currentFlow = b.unreachableFlow;
    b.hasFlowEffects = true;
}

internal static void bindBreakStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡb.bindBreakOrContinueStatement(node.Label(), b.currentBreakTarget, ((Func<ж<ActiveLabel>, ж<ast.FlowNode>>)(binderᴛActiveLabel.BreakTarget)));
}

internal static void bindContinueStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡb.bindBreakOrContinueStatement(node.Label(), b.currentContinueTarget, ((Func<ж<ActiveLabel>, ж<ast.FlowNode>>)(binderᴛActiveLabel.ContinueTarget)));
}

internal static void bindBreakOrContinueStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡlabel, ж<ast.FlowNode> currentTarget, Func<ж<ActiveLabel>, ж<ast.FlowNode>> getTarget) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var label = ref Ꮡlabel.DerefOrNull();

    Ꮡb.bind(Ꮡlabel);
    if (Ꮡlabel != nil) {
        var activeLabel = b.findActiveLabel(label.Text());
        if (activeLabel != nil) {
            activeLabel.referenced = true;
            b.bindBreakOrContinueFlow(getTarget(activeLabel));
        }
    } else {
        b.bindBreakOrContinueFlow(currentTarget);
    }
}

internal static ж<ActiveLabel> findActiveLabel(this ref Binder b, @string name) {
    for (var label = b.activeLabelList; label != nil; label = label.next) {
        if (label.name == name) {
            return label;
        }
    }
    return default;
}

internal static void bindBreakOrContinueFlow(this ref Binder b, ж<astꓸFlowLabel> flowLabel) {
    if (flowLabel != nil) {
        b.addAntecedent(flowLabel, b.currentFlow);
        b.currentFlow = b.unreachableFlow;
        b.hasFlowEffects = true;
    }
}

internal static void bindTryStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsTryStatement();
    var saveReturnTarget = b.currentReturnTarget;
    var saveExceptionTarget = b.currentExceptionTarget;
    var normalExitLabel = b.createBranchLabel();
    var returnLabel = b.createBranchLabel();
    var exceptionLabel = b.createBranchLabel();
    if (stmt.FinallyBlock != nil) {
        b.currentReturnTarget = returnLabel;
    }
    b.addAntecedent(exceptionLabel, b.currentFlow);
    b.currentExceptionTarget = exceptionLabel;
    Ꮡb.bind(stmt.TryBlock);
    b.addAntecedent(normalExitLabel, b.currentFlow);
    if (stmt.CatchClause != nil) {
        b.currentFlow = b.finishFlowLabel(exceptionLabel);
        exceptionLabel = b.createBranchLabel();
        b.addAntecedent(exceptionLabel, b.currentFlow);
        b.currentExceptionTarget = exceptionLabel;
        Ꮡb.bind(stmt.CatchClause);
        b.addAntecedent(normalExitLabel, b.currentFlow);
    }
    b.currentReturnTarget = saveReturnTarget;
    b.currentExceptionTarget = saveExceptionTarget;
    if (stmt.FinallyBlock != nil) {
        var finallyLabel = b.createBranchLabel();
        finallyLabel.Antecedents = b.combineFlowLists(normalExitLabel.Antecedents, b.combineFlowLists(exceptionLabel.Antecedents, returnLabel.Antecedents));
        b.currentFlow = finallyLabel;
        Ꮡb.bind(stmt.FinallyBlock);
        if ((ast.FlowFlags)(b.currentFlow.Flags & ast.FlowFlagsUnreachable) != 0) {
            b.currentFlow = b.unreachableFlow;
        } else {
            if (b.currentReturnTarget != nil && returnLabel.Antecedents != nil) {
                b.addAntecedent(b.currentReturnTarget, b.createReduceLabel(finallyLabel, returnLabel.Antecedents, b.currentFlow));
            }
            if (b.currentExceptionTarget != nil && exceptionLabel.Antecedents != nil) {
                b.addAntecedent(b.currentExceptionTarget, b.createReduceLabel(finallyLabel, exceptionLabel.Antecedents, b.currentFlow));
            }
            if (normalExitLabel.Antecedents != nil) {
                b.currentFlow = b.createReduceLabel(finallyLabel, normalExitLabel.Antecedents, b.currentFlow);
            } else {
                b.currentFlow = b.unreachableFlow;
            }
        }
    } else {
        b.currentFlow = b.finishFlowLabel(normalExitLabel);
    }
}

internal static void bindSwitchStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsSwitchStatement();
    var postSwitchLabel = b.createBranchLabel();
    Ꮡb.bind((~stmt).Expression);
    var saveBreakTarget = b.currentBreakTarget;
    var savePreSwitchCaseFlow = b.preSwitchCaseFlow;
    b.currentBreakTarget = postSwitchLabel;
    b.preSwitchCaseFlow = b.currentFlow;
    Ꮡb.bind(stmt.CaseBlock);
    b.addAntecedent(postSwitchLabel, b.currentFlow);
    var hasDefault = core.Some(stmt.CaseBlock.AsCaseBlock().Clauses.Nodes, (ж<ast.Node> c) => c.Kind == ast.KindDefaultClause);
    if (!hasDefault) {
        b.addAntecedent(postSwitchLabel, b.createFlowSwitchClause(b.preSwitchCaseFlow, Ꮡnode, 0, 0));
    }
    b.currentBreakTarget = saveBreakTarget;
    b.preSwitchCaseFlow = savePreSwitchCaseFlow;
    b.currentFlow = b.finishFlowLabel(postSwitchLabel);
}

internal static void bindCaseBlock(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var switchStatement = node.Parent;
    var clauses = node.AsCaseBlock().Clauses.Nodes;
    var isNarrowingSwitch = switchStatement.Expression().Kind == ast.KindTrueKeyword || isNarrowingExpression(switchStatement.Expression());
    ж<ast.FlowNode> fallthroughFlow = b.unreachableFlow;
    for (nint i = 0; i < len(clauses); i++) {
        nint clauseStart = i;
        while (len(clauses[i].Statements()) == 0 && i + 1 < len(clauses)) {
            if (fallthroughFlow == b.unreachableFlow) {
                b.currentFlow = b.preSwitchCaseFlow;
            }
            Ꮡb.bind(clauses[i]);
            i++;
        }
        var preCaseLabel = b.createBranchLabel();
        var preCaseFlow = b.preSwitchCaseFlow;
        if (isNarrowingSwitch) {
            preCaseFlow = b.createFlowSwitchClause(b.preSwitchCaseFlow, switchStatement, clauseStart, i + 1);
        }
        b.addAntecedent(preCaseLabel, preCaseFlow);
        b.addAntecedent(preCaseLabel, fallthroughFlow);
        b.currentFlow = b.finishFlowLabel(preCaseLabel);
        var clause = clauses[i];
        Ꮡb.bind(clause);
        fallthroughFlow = b.currentFlow;
        if ((ast.FlowFlags)(b.currentFlow.Flags & ast.FlowFlagsUnreachable) == 0 && i != len(clauses) - 1) {
            clause.AsCaseOrDefaultClause().FallthroughFlowNode = b.currentFlow;
        }
    }
}

internal static void bindCaseOrDefaultClause(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var clause = node.AsCaseOrDefaultClause();
    if ((~clause).Expression != nil) {
        var saveCurrentFlow = b.currentFlow;
        b.currentFlow = b.preSwitchCaseFlow;
        Ꮡb.bind((~clause).Expression);
        b.currentFlow = saveCurrentFlow;
    }
    Ꮡb.bindEach((~clause).Statements.Nodes);
}

internal static void bindExpressionStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsExpressionStatement();
    Ꮡb.bind((~stmt).Expression);
    b.maybeBindExpressionFlowIfCall((~stmt).Expression);
}

internal static void maybeBindExpressionFlowIfCall(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsCallExpression(Ꮡnode)) {
        if (node.Expression().Kind != ast.KindSuperKeyword && ast.IsDottedName(node.Expression())) {
            b.currentFlow = b.createFlowCall(b.currentFlow, Ꮡnode);
        }
    }
}

internal static void bindLabeledStatement(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var stmt = node.AsLabeledStatement();
    var postStatementLabel = b.createBranchLabel();
    b.activeLabelList = Ꮡ(new ActiveLabel(
        next: b.activeLabelList,
        name: (~stmt).Label.Text(),
        breakTarget: postStatementLabel,
        continueTarget: nil,
        referenced: false
    ));
    Ꮡb.bind((~stmt).Label);
    Ꮡb.bind((~stmt).Statement);
    if (!b.activeLabelList.referenced) {
        stmt.Value.Label.Flags |= (ast.NodeFlags)(ast.NodeFlagsUnreachable);
    }
    b.activeLabelList = b.activeLabelList.next;
    b.addAntecedent(postStatementLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(postStatementLabel);
}

internal static void bindPrefixUnaryExpressionFlow(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsPrefixUnaryExpression();
    if (expr.Operator == ast.KindExclamationToken) {
        var saveTrueTarget = b.currentTrueTarget;
        b.currentTrueTarget = b.currentFalseTarget;
        b.currentFalseTarget = saveTrueTarget;
        b.bindEachChild(Ꮡnode);
        b.currentFalseTarget = b.currentTrueTarget;
        b.currentTrueTarget = saveTrueTarget;
    } else {
        b.bindEachChild(Ꮡnode);
        if (expr.Operator == ast.KindPlusPlusToken || expr.Operator == ast.KindMinusMinusToken) {
            b.bindAssignmentTargetFlow(expr.Operand);
        }
    }
}

internal static void bindPostfixUnaryExpressionFlow(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsPostfixUnaryExpression();
    b.bindEachChild(Ꮡnode);
    if (expr.Operator == ast.KindPlusPlusToken || expr.Operator == ast.KindMinusMinusToken) {
        b.bindAssignmentTargetFlow(expr.Operand);
    }
}

internal static void bindDestructuringAssignmentFlow(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsBinaryExpression();
    if (b.inAssignmentPattern) {
        b.inAssignmentPattern = false;
        Ꮡb.bind(expr.OperatorToken);
        Ꮡb.bind(expr.Right);
        b.inAssignmentPattern = true;
        Ꮡb.bind(expr.Left);
        Ꮡb.bind((~expr).Type);
    } else {
        b.inAssignmentPattern = true;
        Ꮡb.bind(expr.Left);
        Ꮡb.bind((~expr).Type);
        b.inAssignmentPattern = false;
        Ꮡb.bind(expr.OperatorToken);
        Ꮡb.bind(expr.Right);
    }
    b.bindAssignmentTargetFlow(expr.Left);
}

internal static void bindBinaryExpressionFlow(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsBinaryExpression();
    var @operator = expr.OperatorToken.Kind;
    if (ast.IsLogicalOrCoalescingBinaryOperator(@operator) || ast.IsLogicalOrCoalescingAssignmentOperator(@operator)) {
        if (isTopLevelLogicalExpression(Ꮡnode)) {
            var postExpressionLabel = b.createBranchLabel();
            var saveCurrentFlow = b.currentFlow;
            var saveHasFlowEffects = b.hasFlowEffects;
            b.hasFlowEffects = false;
            Ꮡb.bindLogicalLikeExpression(Ꮡnode, postExpressionLabel, postExpressionLabel);
            if (b.hasFlowEffects) {
                b.currentFlow = b.finishFlowLabel(postExpressionLabel);
            } else {
                b.currentFlow = saveCurrentFlow;
            }
            b.hasFlowEffects = b.hasFlowEffects || saveHasFlowEffects;
        } else {
            Ꮡb.bindLogicalLikeExpression(Ꮡnode, b.currentTrueTarget, b.currentFalseTarget);
        }
    } else {
        Ꮡb.bind(expr.Left);
        Ꮡb.bind((~expr).Type);
        if (@operator == ast.KindCommaToken) {
            b.maybeBindExpressionFlowIfCall(expr.Left);
        }
        Ꮡb.bind(expr.OperatorToken);
        Ꮡb.bind(expr.Right);
        if (@operator == ast.KindCommaToken) {
            b.maybeBindExpressionFlowIfCall(expr.Right);
        }
        if (ast.IsAssignmentOperator(@operator) && !ast.IsAssignmentTarget(Ꮡnode)) {
            b.bindAssignmentTargetFlow(expr.Left);
            if (@operator == ast.KindEqualsToken && expr.Left.Kind == ast.KindElementAccessExpression) {
                var elementAccess = expr.Left.AsElementAccessExpression();
                if (isNarrowableOperand((~elementAccess).Expression)) {
                    b.currentFlow = b.createFlowMutation(ast.FlowFlagsArrayMutation, b.currentFlow, Ꮡnode);
                }
            }
        }
    }
}

internal static void bindLogicalLikeExpression(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode, ж<astꓸFlowLabel> trueTarget, ж<astꓸFlowLabel> falseTarget) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsBinaryExpression();
    var preRightLabel = b.createBranchLabel();
    if (expr.OperatorToken.Kind == ast.KindAmpersandAmpersandToken || expr.OperatorToken.Kind == ast.KindAmpersandAmpersandEqualsToken) {
        Ꮡb.bindCondition(expr.Left, preRightLabel, falseTarget);
    } else {
        Ꮡb.bindCondition(expr.Left, trueTarget, preRightLabel);
    }
    b.currentFlow = b.finishFlowLabel(preRightLabel);
    Ꮡb.bind(expr.OperatorToken);
    if (ast.IsLogicalOrCoalescingAssignmentOperator(expr.OperatorToken.Kind)) {
        Ꮡb.doWithConditionalBranches(((Func<ж<Binder>, ж<ast.Node>, bool>)(binderᴛBinder.bind)), expr.Right, trueTarget, falseTarget);
        b.bindAssignmentTargetFlow(expr.Left);
        b.addAntecedent(trueTarget, b.createFlowCondition(ast.FlowFlagsTrueCondition, b.currentFlow, Ꮡnode));
        b.addAntecedent(falseTarget, b.createFlowCondition(ast.FlowFlagsFalseCondition, b.currentFlow, Ꮡnode));
    } else {
        Ꮡb.bindCondition(expr.Right, trueTarget, falseTarget);
    }
}

internal static void bindDeleteExpressionFlow(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsDeleteExpression();
    b.bindEachChild(Ꮡnode);
    if ((~expr).Expression.Kind == ast.KindPropertyAccessExpression) {
        b.bindAssignmentTargetFlow((~expr).Expression);
    }
}

internal static void bindConditionalExpressionFlow(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = node.AsConditionalExpression();
    var trueLabel = b.createBranchLabel();
    var falseLabel = b.createBranchLabel();
    var postExpressionLabel = b.createBranchLabel();
    var saveCurrentFlow = b.currentFlow;
    var saveHasFlowEffects = b.hasFlowEffects;
    b.hasFlowEffects = false;
    Ꮡb.bindCondition(expr.Condition, trueLabel, falseLabel);
    b.currentFlow = b.finishFlowLabel(trueLabel);
    Ꮡb.bind((~expr).QuestionToken);
    Ꮡb.bind(expr.WhenTrue);
    b.addAntecedent(postExpressionLabel, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(falseLabel);
    Ꮡb.bind(expr.ColonToken);
    Ꮡb.bind(expr.WhenFalse);
    b.addAntecedent(postExpressionLabel, b.currentFlow);
    if (b.hasFlowEffects) {
        b.currentFlow = b.finishFlowLabel(postExpressionLabel);
    } else {
        b.currentFlow = saveCurrentFlow;
    }
    b.hasFlowEffects = b.hasFlowEffects || saveHasFlowEffects;
}

internal static void bindVariableDeclarationFlow(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    b.bindEachChild(Ꮡnode);
    if (node.Initializer() != nil || ast.IsForInOrOfStatement(node.Parent.Parent)) {
        b.bindInitializedVariableFlow(Ꮡnode);
    }
}

internal static void bindInitializedVariableFlow(this ref Binder b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> name = default;
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        name = node.AsVariableDeclaration().Name();
        break;
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        name = node.AsBindingElement().Name();
        break;
    }}

    if (name != nil && ast.IsBindingPattern(name)) {
        foreach (var (_, child) in range(name.Elements())) {
            b.bindInitializedVariableFlow(child);
        }
    } else {
        b.currentFlow = b.createFlowMutation(ast.FlowFlagsAssignment, b.currentFlow, Ꮡnode);
    }
}

internal static void bindAccessExpressionFlow(this ж<Binder> Ꮡb, ж<ast.Node> node) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (ast.IsOptionalChain(node)) {
        Ꮡb.bindOptionalChainFlow(node);
    } else {
        b.bindEachChild(node);
    }
}

internal static void bindOptionalChainFlow(this ж<Binder> Ꮡb, ж<ast.Node> node) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (isTopLevelLogicalExpression(node)) {
        var postExpressionLabel = b.createBranchLabel();
        var saveCurrentFlow = b.currentFlow;
        var saveHasFlowEffects = b.hasFlowEffects;
        Ꮡb.bindOptionalChain(node, postExpressionLabel, postExpressionLabel);
        if (b.hasFlowEffects) {
            b.currentFlow = b.finishFlowLabel(postExpressionLabel);
        } else {
            b.currentFlow = saveCurrentFlow;
        }
        b.hasFlowEffects = b.hasFlowEffects || saveHasFlowEffects;
    } else {
        Ꮡb.bindOptionalChain(node, b.currentTrueTarget, b.currentFalseTarget);
    }
}

internal static void bindOptionalChain(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode, ж<astꓸFlowLabel> trueTarget, ж<astꓸFlowLabel> falseTarget) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<astꓸFlowLabel> preChainLabel = default;
    if (ast.IsOptionalChainRoot(Ꮡnode)) {
        preChainLabel = b.createBranchLabel();
    }
    Ꮡb.bindOptionalExpression(node.Expression(), core.IfElse(preChainLabel != nil, preChainLabel, trueTarget), falseTarget);
    if (preChainLabel != nil) {
        b.currentFlow = b.finishFlowLabel(preChainLabel);
    }
    Ꮡb.doWithConditionalBranches(((Func<ж<Binder>, ж<ast.Node>, bool>)(binderᴛBinder.bindOptionalChainRest)), Ꮡnode, trueTarget, falseTarget);
    if (ast.IsOutermostOptionalChain(Ꮡnode)) {
        b.addAntecedent(trueTarget, b.createFlowCondition(ast.FlowFlagsTrueCondition, b.currentFlow, Ꮡnode));
        b.addAntecedent(falseTarget, b.createFlowCondition(ast.FlowFlagsFalseCondition, b.currentFlow, Ꮡnode));
    }
}

internal static void bindOptionalExpression(this ж<Binder> Ꮡb, ж<ast.Node> node, ж<astꓸFlowLabel> trueTarget, ж<astꓸFlowLabel> falseTarget) {
    ref var b = ref Ꮡb.DerefOrNull();

    Ꮡb.doWithConditionalBranches(((Func<ж<Binder>, ж<ast.Node>, bool>)(binderᴛBinder.bind)), node, trueTarget, falseTarget);
    if (!ast.IsOptionalChain(node) || ast.IsOutermostOptionalChain(node)) {
        b.addAntecedent(trueTarget, b.createFlowCondition(ast.FlowFlagsTrueCondition, b.currentFlow, node));
        b.addAntecedent(falseTarget, b.createFlowCondition(ast.FlowFlagsFalseCondition, b.currentFlow, node));
    }
}

internal static bool bindOptionalChainRest(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        Ꮡb.bind(node.QuestionDotToken());
        Ꮡb.bind(node.Name());
        break;
    }
    case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
        Ꮡb.bind(node.QuestionDotToken());
        Ꮡb.bind(node.AsElementAccessExpression().ArgumentExpression);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        Ꮡb.bind(node.QuestionDotToken());
        Ꮡb.bindNodeList(node.TypeArgumentList());
        Ꮡb.bindEach(node.Arguments());
        break;
    }}

    return false;
}

internal static void bindCallExpressionFlow(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var call = node.AsCallExpression();
    if (ast.IsOptionalChain(Ꮡnode)) {
        Ꮡb.bindOptionalChainFlow(Ꮡnode);
    } else {
        var expr = ast.SkipParentheses((~call).Expression);
        if (expr.Kind == ast.KindFunctionExpression || expr.Kind == ast.KindArrowFunction) {
            Ꮡb.bindNodeList((~call).TypeArguments);
            Ꮡb.bindEach((~call).Arguments.Nodes);
            Ꮡb.bind((~call).Expression);
        } else {
            b.bindEachChild(Ꮡnode);
            if ((~call).Expression.Kind == ast.KindSuperKeyword) {
                b.currentFlow = b.createFlowCall(b.currentFlow, Ꮡnode);
            }
        }
    }
    if (ast.IsPropertyAccessExpression((~call).Expression)) {
        var access = (~call).Expression.AsPropertyAccessExpression();
        if (ast.IsIdentifier(access.Name()) && isNarrowableOperand((~access).Expression) && ast.IsPushOrUnshiftIdentifier(access.Name())) {
            b.currentFlow = b.createFlowMutation(ast.FlowFlagsArrayMutation, b.currentFlow, Ꮡnode);
        }
    }
}

internal static void bindNonNullExpressionFlow(this ж<Binder> Ꮡb, ж<ast.Node> node) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (ast.IsOptionalChain(node)) {
        Ꮡb.bindOptionalChainFlow(node);
    } else {
        b.bindEachChild(node);
    }
}

internal static void bindBindingElementFlow(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var elem = node.AsBindingElement();
    Ꮡb.bind(elem.DotDotDotToken);
    Ꮡb.bind((~elem).PropertyName);
    Ꮡb.bindInitializer((~elem).Initializer);
    Ꮡb.bind(elem.Name());
}

internal static void bindParameterFlow(this ж<Binder> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var param = node.AsParameterDeclaration();
    Ꮡb.bindModifiers(param.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers());
    Ꮡb.bind(param.DotDotDotToken);
    Ꮡb.bind((~param).QuestionToken);
    Ꮡb.bind((~param).Type);
    Ꮡb.bindInitializer((~param).Initializer);
    Ꮡb.bind(param.Name());
}

internal static void bindInitializer(this ж<Binder> Ꮡb, ж<ast.Node> node) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (node == nil) {
        return;
    }
    var entryFlow = b.currentFlow;
    Ꮡb.bind(node);
    if (entryFlow == b.unreachableFlow || entryFlow == b.currentFlow) {
        return;
    }
    var exitFlow = b.createBranchLabel();
    b.addAntecedent(exitFlow, entryFlow);
    b.addAntecedent(exitFlow, b.currentFlow);
    b.currentFlow = b.finishFlowLabel(exitFlow);
}

} partial class binder_package {

internal static void setFlowNode(ж<ast.Node> Ꮡnode, ж<ast.FlowNode> flowNode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var data = node.FlowNodeData();
    if (data != nil) {
        data.FlowNode = flowNode;
    }
}

internal static void setReturnFlowNode(ж<ast.Node> Ꮡnode, ж<ast.FlowNode> returnFlowNode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindConstructor: {
        node.AsConstructorDeclaration().ReturnFlowNode = returnFlowNode;
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        node.AsFunctionDeclaration().ReturnFlowNode = returnFlowNode;
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionExpression: {
        node.AsFunctionExpression().ReturnFlowNode = returnFlowNode;
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration: {
        node.AsClassStaticBlockDeclaration().ReturnFlowNode = returnFlowNode;
        break;
    }}

}

internal static bool isGeneratorFunctionExpression(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsFunctionExpression(Ꮡnode) && node.AsFunctionExpression().AsteriskToken != nil;
}

} partial class binderᴛBinder {

internal static void addToContainerChain(this ref Binder b, ж<ast.Node> next) {
    if (b.lastContainer != nil) {
        b.lastContainer.LocalsContainerData().NextContainer = next;
    }
    b.lastContainer = next;
}

internal static void addDeclarationToSymbol(this ref Binder b, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> Ꮡnode, ast.SymbolFlags symbolFlags) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    symbol.Flags |= (ast.SymbolFlags)(symbolFlags);
    node.DeclarationData().Symbol = Ꮡsymbol;
    if (symbol.Declarations == nil) {
        symbol.Declarations = b.newSingleDeclaration(Ꮡnode);
    } else {
        symbol.Declarations = core.AppendIfUnique(symbol.Declarations, Ꮡnode);
    }
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsConstEnumOnlyModule) != 0 && (ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsFunction | ast.SymbolFlagsClass) | ast.SymbolFlagsRegularEnum))) != 0) {
        symbol.Flags &= unchecked((ast.SymbolFlags)~(ast.SymbolFlags)(ast.SymbolFlagsConstEnumOnlyModule));
        b.notConstEnumOnlyModules.Add(Ꮡsymbol);
    }
    if ((ast.SymbolFlags)(symbolFlags & ast.SymbolFlagsValue) != 0) {
        SetValueDeclaration(Ꮡsymbol, Ꮡnode);
    }
}

} partial class binder_package {

public static void SetValueDeclaration(ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> Ꮡnode) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var valueDeclaration = symbol.ValueDeclaration;
    if (valueDeclaration == nil || isAssignmentDeclaration(valueDeclaration) && !isAssignmentDeclaration(Ꮡnode) || valueDeclaration.Kind != node.Kind && isEffectiveModuleDeclaration(valueDeclaration)) {
        symbol.ValueDeclaration = Ꮡnode;
    }
}

public static ContainerFlags GetContainerFlags(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var exprᴛ1 = node.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindClassExpression || exprᴛ1 == ast.KindClassDeclaration || exprᴛ1 == ast.KindEnumDeclaration || exprᴛ1 == ast.KindObjectLiteralExpression || exprᴛ1 == ast.KindTypeLiteral || exprᴛ1 == ast.KindJsxAttributes) { matchᴛ1 = true;
        return ContainerFlagsIsContainer;
    }
    if (exprᴛ1 == ast.KindInterfaceDeclaration) { matchᴛ1 = true;
        return (ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsIsInterface);
    }
    if (exprᴛ1 == ast.KindModuleDeclaration || exprᴛ1 == ast.KindTypeAliasDeclaration || exprᴛ1 == ast.KindJSTypeAliasDeclaration || exprᴛ1 == ast.KindMappedType || exprᴛ1 == ast.KindIndexSignature) { matchᴛ1 = true;
        return (ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsHasLocals);
    }
    if (exprᴛ1 == ast.KindSourceFile) { matchᴛ1 = true;
        return (ContainerFlags)((ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsIsControlFlowContainer) | ContainerFlagsHasLocals);
    }
    if (exprᴛ1 == ast.KindGetAccessor || exprᴛ1 == ast.KindSetAccessor || exprᴛ1 == ast.KindMethodDeclaration) { matchᴛ1 = true;
        if (ast.IsObjectLiteralOrClassExpressionMethodOrAccessor(Ꮡnode)) {
            return (ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsIsControlFlowContainer) | ContainerFlagsHasLocals) | ContainerFlagsIsFunctionLike) | ContainerFlagsIsObjectLiteralOrClassExpressionMethodOrAccessor) | ContainerFlagsIsThisContainer);
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindConstructor || exprᴛ1 == ast.KindFunctionDeclaration || exprᴛ1 == ast.KindClassStaticBlockDeclaration)) { matchᴛ1 = true;
        return (ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsIsControlFlowContainer) | ContainerFlagsHasLocals) | ContainerFlagsIsFunctionLike) | ContainerFlagsIsThisContainer);
    }
    if (exprᴛ1 == ast.KindMethodSignature || exprᴛ1 == ast.KindCallSignature || exprᴛ1 == ast.KindFunctionType || exprᴛ1 == ast.KindConstructSignature || exprᴛ1 == ast.KindConstructorType) { matchᴛ1 = true;
        return (ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsIsControlFlowContainer) | ContainerFlagsHasLocals) | ContainerFlagsIsFunctionLike) | ContainerFlagsPropagatesThisKeyword);
    }
    if (exprᴛ1 == ast.KindFunctionExpression) { matchᴛ1 = true;
        return (ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsIsControlFlowContainer) | ContainerFlagsHasLocals) | ContainerFlagsIsFunctionLike) | ContainerFlagsIsFunctionExpression) | ContainerFlagsIsThisContainer);
    }
    if (exprᴛ1 == ast.KindArrowFunction) { matchᴛ1 = true;
        return (ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)((ContainerFlags)(ContainerFlagsIsContainer | ContainerFlagsIsControlFlowContainer) | ContainerFlagsHasLocals) | ContainerFlagsIsFunctionLike) | ContainerFlagsIsFunctionExpression) | ContainerFlagsPropagatesThisKeyword);
    }
    if (exprᴛ1 == ast.KindModuleBlock) { matchᴛ1 = true;
        return ContainerFlagsIsControlFlowContainer;
    }
    if (exprᴛ1 == ast.KindPropertyDeclaration) { matchᴛ1 = true;
        if (node.Initializer() != nil) {
            return (ContainerFlags)(ContainerFlagsIsControlFlowContainer | ContainerFlagsIsThisContainer);
        } else {
            return ContainerFlagsNone;
        }
    }
    if (exprᴛ1 == ast.KindCatchClause || exprᴛ1 == ast.KindForStatement || exprᴛ1 == ast.KindForInStatement || exprᴛ1 == ast.KindForOfStatement || exprᴛ1 == ast.KindCaseBlock) {
        return (ContainerFlags)(ContainerFlagsIsBlockScopedContainer | ContainerFlagsHasLocals);
    }
    if (exprᴛ1 == ast.KindBlock) { matchᴛ1 = true;
        if (ast.IsFunctionLike(node.Parent) || ast.IsClassStaticBlockDeclaration(node.Parent)) {
            return ContainerFlagsNone;
        } else {
            return (ContainerFlags)(ContainerFlagsIsBlockScopedContainer | ContainerFlagsHasLocals);
        }
    }

    return ContainerFlagsNone;
}

internal static bool isNarrowingExpression(ж<ast.Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    switch (expr.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindThisKeyword: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        return containsNarrowableReference(Ꮡexpr);
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        return hasNarrowableArgument(Ꮡexpr);
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression || ᴛ == ast.KindNonNullExpression || ᴛ == ast.KindTypeOfExpression: {
        return isNarrowingExpression(expr.Expression());
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return isNarrowingBinaryExpression(ref (expr.AsBinaryExpression()).DerefOrNull());
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        return expr.AsPrefixUnaryExpression().Operator == ast.KindExclamationToken && isNarrowingExpression(expr.AsPrefixUnaryExpression().Operand);
    }}

    return false;
}

internal static bool containsNarrowableReference(ж<ast.Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    if (isNarrowableReference(Ꮡexpr)) {
        return true;
    }
    if ((ast.NodeFlags)(expr.Flags & ast.NodeFlagsOptionalChain) != 0) {
        switch (expr.Kind) {
        case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression || ᴛ == ast.KindCallExpression || ᴛ == ast.KindNonNullExpression: {
            return containsNarrowableReference(expr.Expression());
        }}

    }
    return false;
}

internal static bool isNarrowableReference(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindThisKeyword || ᴛ == ast.KindSuperKeyword || ᴛ == ast.KindMetaProperty: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindParenthesizedExpression || ᴛ == ast.KindNonNullExpression: {
        return isNarrowableReference(node.Expression());
    }
    case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
        var expr = node.AsElementAccessExpression();
        return ast.IsStringOrNumericLiteralLike(expr.ArgumentExpression) || ast.IsEntityNameExpression(expr.ArgumentExpression) && isNarrowableReference((~expr).Expression);
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        var expr = node.AsBinaryExpression();
        return expr.OperatorToken.Kind == ast.KindCommaToken && isNarrowableReference(expr.Right) || ast.IsAssignmentOperator(expr.OperatorToken.Kind) && ast.IsLeftHandSideExpression(expr.Left);
    }}

    return false;
}

internal static bool hasNarrowableArgument(ж<ast.Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    var call = expr.AsCallExpression();
    foreach (var (_, argument) in range((~call).Arguments.Nodes)) {
        if (containsNarrowableReference(argument)) {
            return true;
        }
    }
    if (ast.IsPropertyAccessExpression((~call).Expression)) {
        if (containsNarrowableReference((~call).Expression.Expression())) {
            return true;
        }
    }
    return false;
}

internal static bool isNarrowingBinaryExpression(ref ast.BinaryExpression expr) {
    switch (expr.OperatorToken.Kind) {
    case var ᴛ when ᴛ == ast.KindEqualsToken || ᴛ == ast.KindBarBarEqualsToken || ᴛ == ast.KindAmpersandAmpersandEqualsToken || ᴛ == ast.KindQuestionQuestionEqualsToken: {
        return containsNarrowableReference(expr.Left);
    }
    case var ᴛ when ᴛ == ast.KindEqualsEqualsToken || ᴛ == ast.KindExclamationEqualsToken || ᴛ == ast.KindEqualsEqualsEqualsToken || ᴛ == ast.KindExclamationEqualsEqualsToken: {
        var left = ast.SkipParentheses(expr.Left);
        var right = ast.SkipParentheses(expr.Right);
        return isNarrowableOperand(left) || isNarrowableOperand(right) || isNarrowingTypeOfOperands(right, left) || isNarrowingTypeOfOperands(left, right) || (ast.IsBooleanLiteral(right) && isNarrowingExpression(left) || ast.IsBooleanLiteral(left) && isNarrowingExpression(right));
    }
    case var ᴛ when ᴛ == ast.KindInstanceOfKeyword: {
        return isNarrowableOperand(expr.Left);
    }
    case var ᴛ when ᴛ == ast.KindInKeyword: {
        return isNarrowingExpression(expr.Right);
    }
    case var ᴛ when ᴛ == ast.KindCommaToken: {
        return isNarrowingExpression(expr.Right);
    }}

    return false;
}

internal static bool isNarrowableOperand(ж<ast.Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    switch (expr.Kind) {
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return isNarrowableOperand(expr.Expression());
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        var binary = expr.AsBinaryExpression();
        switch (binary.OperatorToken.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindEqualsToken: {
            return isNarrowableOperand(binary.Left);
        }
        case var ᴛ2 when ᴛ2 == ast.KindCommaToken: {
            return isNarrowableOperand(binary.Right);
        }}

        break;
    }}

    return containsNarrowableReference(Ꮡexpr);
}

internal static bool isNarrowingTypeOfOperands(ж<ast.Node> Ꮡexpr1, ж<ast.Node> expr2) {
    ref var expr1 = ref Ꮡexpr1.DerefOrNull();

    return ast.IsTypeOfExpression(Ꮡexpr1) && isNarrowableOperand(expr1.Expression()) && ast.IsStringLiteralLike(expr2);
}

} partial class binderᴛBinder {

internal static void errorOnNode(this ref Binder b, ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    b.addDiagnostic(b.createDiagnosticForNode(node, message, args.ꓸꓸꓸ));
}

internal static void errorOnFirstToken(this ref Binder b, ж<ast.Node> Ꮡnode, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var node = ref Ꮡnode.DerefOrNull();

    var span = scanner.GetRangeOfTokenAtPosition(b.@file, node.Pos());
    b.addDiagnostic(ast.NewDiagnostic(b.@file, span, message, args.ꓸꓸꓸ));
}

internal static ж<ast.Diagnostic> createDiagnosticForNode(this ref Binder b, ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    return ast.NewDiagnostic(b.@file, scanner.GetErrorRangeForNode(b.@file, node), message, args.ꓸꓸꓸ);
}

internal static void addDiagnostic(this ref Binder b, ж<ast.Diagnostic> diagnostic) {
    b.@file.SetBindDiagnostics(append(b.@file.BindDiagnostics(), diagnostic));
}

} partial class binder_package {

internal static bool isSignedNumericLiteral(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindPrefixUnaryExpression) {
        var nodeΔ1 = node.AsPrefixUnaryExpression();
        return (nodeΔ1.Operator == ast.KindPlusToken || nodeΔ1.Operator == ast.KindMinusToken) && ast.IsNumericLiteral(nodeΔ1.Operand);
    }
    return false;
}

internal static ast.SymbolFlags getOptionalSymbolFlagForNode(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var postfixToken = node.PostfixToken();
    return core.IfElse(postfixToken != nil && postfixToken.Kind == ast.KindQuestionToken, ast.SymbolFlagsOptional, ast.SymbolFlagsNone);
}

internal static bool isFunctionSymbol(ref ast.Symbol symbol) {
    var d = symbol.ValueDeclaration;
    if (d != nil) {
        if (ast.IsFunctionDeclaration(d)) {
            return true;
        }
        if (ast.IsVariableDeclaration(d)) {
            var varDecl = d.AsVariableDeclaration();
            if ((~varDecl).Initializer != nil) {
                return ast.IsFunctionLike((~varDecl).Initializer);
            }
        }
    }
    return false;
}

internal static bool isStatementCondition(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Parent.Kind) {
    case var ᴛ when ᴛ == ast.KindIfStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindDoStatement: {
        return node.Parent.Expression() == Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        return node.Parent.AsForStatement().Condition == Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindConditionalExpression: {
        return node.Parent.AsConditionalExpression().Condition == Ꮡnode;
    }}

    return false;
}

internal static bool isTopLevelLogicalExpression(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (ast.IsParenthesizedExpression(node.Parent) || ast.IsPrefixUnaryExpression(node.Parent) && node.Parent.AsPrefixUnaryExpression().Operator == ast.KindExclamationToken) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return !isStatementCondition(Ꮡnode) && !ast.IsLogicalExpression(node.Parent) && !(ast.IsOptionalChain(node.Parent) && node.Parent.Expression() == Ꮡnode);
}

internal static bool isAssignmentDeclaration(ж<ast.Node> decl) {
    return ast.IsBinaryExpression(decl) || ast.IsAccessExpression(decl) || ast.IsIdentifier(decl) || ast.IsCallExpression(decl);
}

internal static bool isEffectiveModuleDeclaration(ж<ast.Node> node) {
    return ast.IsModuleDeclaration(node) || ast.IsIdentifier(node);
}

} // end binder_package
