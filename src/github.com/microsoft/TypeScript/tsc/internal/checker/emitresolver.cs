namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using slices = slices_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using static checker_package;

partial class checker_package {

internal static printer.EmitResolver _ᴛ1ʗ = new EmitResolverжEmitResolver(((ж<EmitResolver>)nil));

partial struct JSXLinks {
    internal ж<ast.Node> importRef;
}

partial struct DeclarationLinks {
    internal core.Tristate isVisible;
}

partial struct DeclarationFileLinks {
    internal bool aliasesMarked;
}

partial struct EmitResolver {
    internal ж<Checker> checker;
    internal ж<sync.Mutex> checkerMu;
    internal Func<ж<ast.Node>, bool> isValueAliasDeclaration;
    internal Func<ж<ast.Node>, bool> aliasMarkingVisitor;
    internal binder.ReferenceResolver referenceResolver;
    internal core.LinkStore<ж<ast.Node>, JSXLinks> jsxLinks;
    internal core.LinkStore<ж<ast.Node>, DeclarationLinks> declarationLinks;
    internal core.LinkStore<ж<ast.Node>, DeclarationFileLinks> declarationFileLinks;
}

internal static ж<EmitResolver> newEmitResolver(ж<Checker> checker) {
    var e = Ꮡ(new EmitResolver(checker: checker));
    var eʗ1 = e;
        e.isValueAliasDeclaration = (ж<ast.Node> p1) => eʗ1.isValueAliasDeclarationWorker(p1);
    var eʗ2 = e;
        e.aliasMarkingVisitor = (ж<ast.Node> p1) => eʗ2.aliasMarkingVisitorWorker(p1);
    e.checkerMu = checker.Ꮡmu;
    return e;
}

} partial class checkerᴛEmitResolver {

public static ж<ast.Node> GetJsxFactoryEntity(this ж<EmitResolver> Ꮡr, ж<ast.Node> location) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.checker.getJsxFactoryEntity(location);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<ast.Node> GetJsxFragmentFactoryEntity(this ж<EmitResolver> Ꮡr, ж<ast.Node> location) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.checker.getJsxFragmentFactoryEntity(location);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsOptionalParameter(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.isOptionalParameter(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsLateBound(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (node == nil) {
            return false;
        }
        if (!ast.IsParseTreeNode(node)) {
            return false;
        }
        r.checkerMu.Lock();
        ᒐd1 = true;
        var symbol = r.checker.getSymbolOfDeclaration(node);
        if (symbol == nil) {
            return false;
        }
        return (ast.CheckFlags)(symbol.CheckFlags & ast.CheckFlagsLate) != 0;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static evaluator.Result GetEnumMemberValue(this ж<EmitResolver> Ꮡr, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if (!ast.IsParseTreeNode(Ꮡnode)) {
            return evaluator.NewResult(default, false, false, false);
        }
        r.checkerMu.Lock();
        ᒐd1 = true;

        r.checker.computeEnumMemberValues(node.Parent);
        if (!r.checker.ᏑenumMemberLinks.Has(Ꮡnode)) {
            return evaluator.NewResult(default, false, false, false);
        }
        return r.checker.ᏑenumMemberLinks.Get(Ꮡnode).value;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsDeclarationVisible(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.isDeclarationVisible(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

internal static bool isDeclarationVisible(this ref EmitResolver r, ж<ast.Node> node) {
    if (!ast.IsParseTreeNode(node)) {
        return false;
    }
    if (node == nil) {
        return false;
    }

    var links = r.declarationLinks.Get(node);
    if (links.isVisible == core.TSUnknown) {
        if (r.determineIfDeclarationIsVisible(node)) {
            links.isVisible = core.TSTrue;
        } else {
            links.isVisible = core.TSFalse;
        }
    }
    return links.isVisible == core.TSTrue;
}

internal static bool determineIfDeclarationIsVisible(this ref EmitResolver r, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindJSDocCallbackTag || ᴛ == ast.KindJSDocTypedefTag: {
        return node.Parent != nil && node.Parent.Parent != nil && node.Parent.Parent.Parent != nil && ast.IsSourceFile(node.Parent.Parent.Parent);
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        return r.isDeclarationVisible(node.Parent.Parent);
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindEnumDeclaration || ᴛ == ast.KindImportEqualsDeclaration: {
        if (ast.IsVariableDeclaration(Ꮡnode)) {
            if (ast.IsBindingPattern(node.Name()) && len(node.Name().Elements()) == 0) {
                return false;
            }
        }
        if (ast.IsExternalModuleAugmentation(Ꮡnode) || ast.IsImplicitlyExportedJSDocDeclaration(Ꮡnode)) {
            return true;
        }
        var parent = ast.GetDeclarationContainer(Ꮡnode);
        if ((ast.ModifierFlags)(r.checker.getCombinedModifierFlagsCached(Ꮡnode) & ast.ModifierFlagsExport) == 0 && !(node.Kind != ast.KindImportEqualsDeclaration && parent.Kind != ast.KindSourceFile && (ast.NodeFlags)(parent.Flags & ast.NodeFlagsAmbient) != 0)) {
            return ast.IsGlobalSourceFile(parent);
        }
        return r.isDeclarationVisible(parent);
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertySignature || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature: {
        if (r.checker.GetEffectiveDeclarationFlags(Ꮡnode, (ast.ModifierFlags)(ast.ModifierFlagsPrivate | ast.ModifierFlagsProtected)) != 0) {
            return false;
        }
        return r.isDeclarationVisible(node.Parent);
    }
    case var ᴛ when ᴛ == ast.KindConstructor || ᴛ == ast.KindConstructSignature || ᴛ == ast.KindCallSignature || ᴛ == ast.KindIndexSignature || ᴛ == ast.KindParameter || ᴛ == ast.KindModuleBlock || ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType || ᴛ == ast.KindTypeLiteral || ᴛ == ast.KindTypeReference || ᴛ == ast.KindArrayType || ᴛ == ast.KindTupleType || ᴛ == ast.KindUnionType || ᴛ == ast.KindIntersectionType || ᴛ == ast.KindParenthesizedType || ᴛ == ast.KindNamedTupleMember: {
        return r.isDeclarationVisible(node.Parent);
    }
    case var ᴛ when ᴛ == ast.KindImportClause || ᴛ == ast.KindNamespaceImport || ᴛ == ast.KindImportSpecifier: {
        return false;
    }
    case var ᴛ when ᴛ == ast.KindTypeParameter: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindSourceFile || ᴛ == ast.KindNamespaceExportDeclaration: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return false;
    }
    case var ᴛ when ᴛ == ast.KindExportSpecifier: {
        var exportDecl = node.Parent.Parent;
        if (ast.IsExportDeclaration(exportDecl) && (~exportDecl.AsExportDeclaration()).ModuleSpecifier == nil) {
            return r.isDeclarationVisible(exportDecl.Parent);
        }
        return false;
    }
    default: {
        return false;
    }}

}

public static void PrecalculateDeclarationEmitVisibility(this ж<EmitResolver> Ꮡr, ж<ast.SourceFile> @file) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        if (r.declarationFileLinks.Get(@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).aliasesMarked) {
            return;
        }
        r.declarationFileLinks.Get(@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).aliasesMarked = true;
        @file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode().ForEachChild(r.aliasMarkingVisitor);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

} partial class checker_package {

internal static bool isCommonJSModuleExports(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBinaryExpression(Ꮡnode) && ast.IsExpressionStatement(node.Parent) && ast.IsSourceFile(node.Parent.Parent) && node.Parent.Parent.AsSourceFile().CommonJSModuleIndicator != nil) {
        switch (ast.GetAssignmentDeclarationKind(Ꮡnode)) {
        case var ᴛ when ᴛ == ast.JSDeclarationKindModuleExports || ᴛ == ast.JSDeclarationKindExportsProperty: {
            return true;
        }}

    }
    return false;
}

} partial class checkerᴛEmitResolver {

internal static bool aliasMarkingVisitorWorker(this ref EmitResolver r, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        if (isCommonJSModuleExports(Ꮡnode) && ast.IsIdentifier(node.AsBinaryExpression().Right)) {
            r.markLinkedAliases(node.AsBinaryExpression().Right);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        if (node.Expression().Kind == ast.KindIdentifier) {
            r.markLinkedAliases(node.Expression());
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportSpecifier: {
        r.markLinkedAliases(node.PropertyNameOrName());
        break;
    }}

    return node.ForEachChild(r.aliasMarkingVisitor);
}

internal static void markLinkedAliases(this ref EmitResolver r, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Symbol> exportSymbol = default;
    if (node.Kind != ast.KindStringLiteral && node.Parent != nil && (ast.IsExportAssignment(node.Parent) || isCommonJSModuleExports(node.Parent))) {
        exportSymbol = r.checker.resolveName(Ꮡnode, node.Text(), (ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsType) | ast.SymbolFlagsNamespace) | ast.SymbolFlagsAlias), nil, false, false);
    } else if (node.Parent.Kind == ast.KindExportSpecifier) {
        exportSymbol = r.checker.getTargetOfExportSpecifier(node.Parent, (ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsType) | ast.SymbolFlagsNamespace) | ast.SymbolFlagsAlias), false);
    }

    var visited = new map<ast.SymbolId, EmptyStruct>(2);
    while (exportSymbol != nil) {
        var (_, seen) = visited[ast.GetSymbolId(exportSymbol), ꟷ];
        if (seen) {
            break;
        }
        visited[ast.GetSymbolId(exportSymbol)] = new EmptyStruct();

        ж<ast.Symbol> nextSymbol = default;
        foreach (var (_, declaration) in range(exportSymbol.Declarations)) {
            r.declarationLinks.Get(declaration).isVisible = core.TSTrue;

            if (ast.IsInternalModuleImportEqualsDeclaration(declaration)) {
                var internalModuleReference = declaration.AsImportEqualsDeclaration().ModuleReference;
                var firstIdentifier = ast.GetFirstIdentifier(internalModuleReference);
                var importSymbol = r.checker.resolveName(declaration, firstIdentifier.Text(), (ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsType) | ast.SymbolFlagsNamespace) | ast.SymbolFlagsAlias), nil, false, false);
                nextSymbol = importSymbol;
            }
        }

        exportSymbol = nextSymbol;
    }
}

} partial class checker_package {

internal static ast.SymbolFlags getMeaningOfEntityNameReference(ж<ast.Node> ᏑentityName) {
    ref var entityName = ref ᏑentityName.DerefOrNull();

    if (entityName.Parent.Kind == ast.KindTypeQuery || entityName.Parent.Kind == ast.KindExpressionWithTypeArguments && !ast.IsPartOfTypeNode(entityName.Parent) || entityName.Parent.Kind == ast.KindComputedPropertyName || entityName.Parent.Kind == ast.KindTypePredicate && entityName.Parent.AsTypePredicateNode().ParameterName == ᏑentityName || entityName.Parent.Kind == ast.KindBinaryExpression) {
        return (ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsExportValue);
    }
    if (entityName.Kind == ast.KindQualifiedName || entityName.Kind == ast.KindPropertyAccessExpression || entityName.Parent.Kind == ast.KindImportEqualsDeclaration || (entityName.Parent.Kind == ast.KindQualifiedName && entityName.Parent.AsQualifiedName().Left == ᏑentityName) || (entityName.Parent.Kind == ast.KindPropertyAccessExpression && entityName.Parent.Expression() == ᏑentityName) || (entityName.Parent.Kind == ast.KindElementAccessExpression && entityName.Parent.Expression() == ᏑentityName)) {
        return ast.SymbolFlagsNamespace;
    }
    return ast.SymbolFlagsType;
}

} partial class checkerᴛEmitResolver {

public static printer.SymbolAccessibilityResult IsEntityNameVisible(this ж<EmitResolver> Ꮡr, ж<ast.Node> entityName, ж<ast.Node> enclosingDeclaration) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return Ꮡr.isEntityNameVisible(entityName, enclosingDeclaration, true);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

internal static printer.SymbolAccessibilityResult isEntityNameVisible(this ж<EmitResolver> Ꮡr, ж<ast.Node> entityName, ж<ast.Node> enclosingDeclaration, bool shouldComputeAliasToMakeVisible) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (!ast.IsParseTreeNode(entityName)) {
        return new printer.SymbolAccessibilityResult(Accessibility: printer.SymbolAccessibilityNotAccessible);
    }

    var meaning = getMeaningOfEntityNameReference(entityName);
    var firstIdentifier = ast.GetFirstIdentifier(entityName);

    var symbol = r.checker.resolveName(enclosingDeclaration, firstIdentifier.Text(), meaning, nil, false, false);

    if (symbol != nil && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTypeParameter) != 0 && (ast.SymbolFlags)(meaning & ast.SymbolFlagsType) != 0) {
        return new printer.SymbolAccessibilityResult(Accessibility: printer.SymbolAccessibilityAccessible);
    }

    if (symbol == nil && ast.IsThisIdentifier(firstIdentifier)) {
        var sym = r.checker.getSymbolOfDeclaration(r.checker.getThisContainer(firstIdentifier, false, false));
        if (r.isSymbolAccessible(sym, enclosingDeclaration, meaning, false).Accessibility == printer.SymbolAccessibilityAccessible) {
            return new printer.SymbolAccessibilityResult(Accessibility: printer.SymbolAccessibilityAccessible);
        }
    }

    if (symbol == nil) {
        return new printer.SymbolAccessibilityResult(
            Accessibility: printer.SymbolAccessibilityNotResolved,
            ErrorSymbolName: firstIdentifier.Text(),
            ErrorNode: firstIdentifier
        );
    }

    var visible = Ꮡr.hasVisibleDeclarations(symbol, shouldComputeAliasToMakeVisible);
    if (visible != nil) {
        return visible.Value;
    }

    return new printer.SymbolAccessibilityResult(
        Accessibility: printer.SymbolAccessibilityNotAccessible,
        ErrorSymbolName: firstIdentifier.Text(),
        ErrorNode: firstIdentifier
    );
}

} partial class checker_package {

internal static void noopAddVisibleAlias(ж<ast.Node> declaration, ж<ast.Node> aliasingStatement) { }

} partial class checkerᴛEmitResolver {

internal static ж<printer.SymbolAccessibilityResult> hasVisibleDeclarations(this ж<EmitResolver> Ꮡr, ж<ast.Symbol> Ꮡsymbol, bool shouldComputeAliasToMakeVisible) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    ref var aliasesToMakeVisibleSet = ref heap<map<ast.NodeId, ж<ast.Node>>>(out var ᏑaliasesToMakeVisibleSet);

    Action<ж<ast.Node>, ж<ast.Node>> addVisibleAlias = default;
    if (shouldComputeAliasToMakeVisible) {
        addVisibleAlias = (ж<ast.Node> declaration, ж<ast.Node> aliasingStatement) => {
            Ꮡr.Value.declarationLinks.Get(declaration).isVisible = core.TSTrue;
            if (ᏑaliasesToMakeVisibleSet.ValueSlot == nil) {
                ᏑaliasesToMakeVisibleSet.ValueSlot = new map<ast.NodeId, ж<ast.Node>>();
            }
            ᏑaliasesToMakeVisibleSet.ValueSlot[ast.GetNodeId(declaration)] = aliasingStatement;
        };
    } else {
        addVisibleAlias = noopAddVisibleAlias;
    }

    foreach (var (_, declaration) in range(symbol.Declarations)) {
        if (ast.IsIdentifier(declaration)) {
            continue;
        }
        if (!r.isDeclarationVisible(declaration)) {
            var anyImportSyntax = getAnyImportSyntax(declaration);
            if (anyImportSyntax != nil && !ast.HasSyntacticModifier(anyImportSyntax, ast.ModifierFlagsExport) && r.isDeclarationVisible(anyImportSyntax.Parent)) {
                addVisibleAlias(declaration, anyImportSyntax);
                continue;
            }
            if (ast.IsVariableDeclaration(declaration) && ast.IsVariableStatement(declaration.Parent.Parent) && !ast.HasSyntacticModifier(declaration.Parent.Parent, ast.ModifierFlagsExport) && r.isDeclarationVisible(declaration.Parent.Parent.Parent)) {
                addVisibleAlias(declaration, declaration.Parent.Parent);
                continue;
            }
            if (ast.IsLateVisibilityPaintedStatement(declaration) && !ast.HasSyntacticModifier(declaration, ast.ModifierFlagsExport) && r.isDeclarationVisible(declaration.Parent)) {
                addVisibleAlias(declaration, declaration);
                continue;
            }
            if (ast.IsBindingElement(declaration)) {
                if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAlias) != 0 && ast.IsInJSFile(declaration) && declaration.Parent != nil && declaration.Parent.Parent != nil && ast.IsVariableDeclaration(declaration.Parent.Parent) && declaration.Parent.Parent.Parent.Parent != nil && ast.IsVariableStatement(declaration.Parent.Parent.Parent.Parent) && !ast.HasSyntacticModifier(declaration.Parent.Parent.Parent.Parent, ast.ModifierFlagsExport) && declaration.Parent.Parent.Parent.Parent.Parent != nil && r.isDeclarationVisible(declaration.Parent.Parent.Parent.Parent.Parent)) {
                    addVisibleAlias(declaration, declaration.Parent.Parent.Parent.Parent);
                    continue;
                }
                if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsBlockScopedVariable) != 0) {
                    var rootDeclaration = ast.WalkUpBindingElementsAndPatterns(declaration);
                    if (ast.IsParameterDeclaration(rootDeclaration)) {
                        return default;
                    }
                    var variableStatement = rootDeclaration.Parent.Parent;
                    if (!ast.IsVariableStatement(variableStatement)) {
                        return default;
                    }
                    if (ast.HasSyntacticModifier(variableStatement, ast.ModifierFlagsExport)) {
                        continue;
                    }
                    if (!r.isDeclarationVisible(variableStatement.Parent)) {
                        return default;
                    }
                    addVisibleAlias(declaration, variableStatement);
                    continue;
                }
            }

            return default;
        }
    }

    return Ꮡ(new printer.SymbolAccessibilityResult(
        Accessibility: printer.SymbolAccessibilityAccessible,
        AliasesToMakeVisible: slices.Collect(maps.Values<map<ast.NodeId, ж<ast.Node>>, ast.NodeId, ж<ast.Node>>(aliasesToMakeVisibleSet))
    ));
}

public static bool IsImplementationOfOverload(this ж<EmitResolver> Ꮡr, ж<astꓸSignatureDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if (!ast.IsParseTreeNode(Ꮡnode)) {
            return false;
        }
        if (ast.NodeIsPresent(node.Body())) {
            if (ast.IsGetAccessorDeclaration(Ꮡnode) || ast.IsSetAccessorDeclaration(Ꮡnode)) {
                return false;
            }
            r.checkerMu.Lock();
            ᒐd1 = true;
            var symbol = r.checker.getSymbolOfDeclaration(Ꮡnode);
            var signaturesOfSymbol = r.checker.getSignaturesOfSymbol(symbol);
            if (len(signaturesOfSymbol) > 1) {
                return true;
            }
            if (len(signaturesOfSymbol) == 1) {
                var signature = signaturesOfSymbol[0];
                if (signature == r.checker.getSignatureOfFullSignatureType(Ꮡnode)) {
                    return false;
                }
                var declaration = signature.declaration;
                if (declaration != Ꮡnode && (ast.NodeFlags)(declaration.Flags & ast.NodeFlagsJSDoc) == 0) {
                    return true;
                }
            }
        }
        return false;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsImportRequiredByAugmentation(this ж<EmitResolver> Ꮡr, ж<ast.ImportDeclaration> decl) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(decl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            return false;
        }
        var @file = ast.GetSourceFileOfNode(decl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if ((~@file).Symbol == nil) {
            return false;
        }
        var importTarget = Ꮡr.GetExternalModuleFileFromDeclaration(decl.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (importTarget == nil) {
            return false;
        }
        if (importTarget == @file) {
            return false;
        }
        r.checkerMu.Lock();
        ᒐd1 = true;
        var exports = r.checker.getExportsOfModule((~@file).Symbol);
        foreach (var s in range<ж<ast.Symbol>>(maps.Values<ast.SymbolTable, @string, ж<ast.Symbol>>(exports).Invoke)) {
            var merged = r.checker.getMergedSymbol(s);
            if (merged != s) {
                if (len(merged.Declarations) > 0) {
                    foreach (var (_, d) in range(merged.Declarations)) {
                        var declFile = ast.GetSourceFileOfNode(d);
                        if (declFile == importTarget) {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsDefinitelyReferenceToGlobalSymbolObject(this ж<EmitResolver> Ꮡr, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    bool ᒐd2 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if (!ast.IsPropertyAccessExpression(Ꮡnode) || !ast.IsIdentifier(node.Name()) || !ast.IsPropertyAccessExpression(node.Expression()) && !ast.IsIdentifier(node.Expression())) {
            return false;
        }
        if (node.Expression().Kind == ast.KindIdentifier) {
            if (node.Expression().Text() != "Symbol"u8) {
                return false;
            }
            r.checkerMu.Lock();
            ᒐd1 = true;
            return r.checker.getResolvedSymbol(node.Expression()) == r.checker.getGlobalSymbol("Symbol", (ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsExportValue), nil);
        }
        if (node.Expression().Expression().Kind != ast.KindIdentifier || node.Expression().Expression().Text() != "globalThis"u8 || node.Expression().Name().Text() != "Symbol"u8) {
            return false;
        }
        r.checkerMu.Lock();
        ᒐd2 = true;
        return r.checker.getResolvedSymbol(node.Expression().Expression()) == r.checker.globalThisSymbol;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd2) Ꮡr.DerefOrNull().checkerMu.Unlock(); if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool RequiresAddingImplicitUndefined(this ж<EmitResolver> Ꮡr, ж<ast.Node> declaration, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(declaration)) {
            return false;
        }
        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.requiresAddingImplicitUndefined(declaration, symbol, enclosingDeclaration);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool RequiresAddingImplicitUndefinedUnsafe(this ref EmitResolver r, ж<ast.Node> declaration, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration) {
    if (!ast.IsParseTreeNode(declaration)) {
        return false;
    }
    return r.requiresAddingImplicitUndefined(declaration, symbol, enclosingDeclaration);
}

internal static bool requiresAddingImplicitUndefined(this ref EmitResolver r, ж<ast.Node> Ꮡdeclaration, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> enclosingDeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (!ast.IsParseTreeNode(Ꮡdeclaration)) {
        return false;
    }
    switch (declaration.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertySignature || ᴛ == ast.KindJSDocPropertyTag: {
        if (Ꮡsymbol == nil) {
            Ꮡsymbol = r.checker.getSymbolOfDeclaration(Ꮡdeclaration); symbol = ref Ꮡsymbol.DerefOrNull();
        }
        var t = r.checker.getTypeOfSymbol(Ꮡsymbol);
        r.checker.ᏑmappedSymbolLinks.Has(Ꮡsymbol);
        return ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsProperty) != 0) && ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsOptional) != 0) && isOptionalDeclaration(Ꮡdeclaration) && r.checker.ᏑReverseMappedSymbolLinks.Has(Ꮡsymbol) && r.checker.ᏑReverseMappedSymbolLinks.Get(Ꮡsymbol).mappedType != nil && containsNonMissingUndefinedType(ref (r.checker).DerefOrNull(), t);
    }
    case var ᴛ when ᴛ == ast.KindParameter || ᴛ == ast.KindJSDocParameterTag: {
        return r.requiresAddingImplicitUndefinedWorker(Ꮡdeclaration, enclosingDeclaration);
    }
    default: {
        throw panic("Node cannot possibly require adding undefined");
        break;
    }}

}

internal static bool requiresAddingImplicitUndefinedWorker(this ref EmitResolver r, ж<ast.Node> parameter, ж<ast.Node> enclosingDeclaration) {
    return (r.isRequiredInitializedParameter(parameter, enclosingDeclaration) || r.isOptionalUninitializedParameterProperty(parameter)) && !r.declaredParameterTypeContainsUndefined(parameter);
}

internal static bool declaredParameterTypeContainsUndefined(this ref EmitResolver r, ж<ast.Node> Ꮡparameter) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();

    var typeNode = parameter.Type();
    if (typeNode == nil) {
        return false;
    }
    var t = r.checker.getTypeFromTypeNode(typeNode);
    return r.checker.isErrorType(t) || r.checker.containsUndefinedType(t);
}

internal static bool isOptionalUninitializedParameterProperty(this ref EmitResolver r, ж<ast.Node> Ꮡparameter) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();

    return r.checker.strictNullChecks && r.isOptionalParameter(Ꮡparameter) && (parameter.Initializer() == nil) && ast.HasSyntacticModifier(Ꮡparameter, ast.ModifierFlagsParameterPropertyModifier);
}

internal static bool isRequiredInitializedParameter(this ref EmitResolver r, ж<ast.Node> Ꮡparameter, ж<ast.Node> enclosingDeclaration) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();

    if (!r.checker.strictNullChecks || r.isOptionalParameter(Ꮡparameter) || parameter.Initializer() == nil) {
        return false;
    }
    if (ast.HasSyntacticModifier(Ꮡparameter, ast.ModifierFlagsParameterPropertyModifier)) {
        return enclosingDeclaration != nil && ast.IsFunctionLikeDeclaration(enclosingDeclaration);
    }
    return true;
}

internal static bool isOptionalParameter(this ref EmitResolver r, ж<ast.Node> node) {
    return r.checker.isOptionalParameter(node);
}

public static bool IsLiteralConstDeclaration(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(node)) {
            return false;
        }
        if (isDeclarationReadonly(node) || ast.IsVariableDeclaration(node) && ast.IsVarConst(node)) {
            r.checkerMu.Lock();
            ᒐd1 = true;
            var s = r.checker.getSymbolOfDeclaration(node);
            if (s == nil) {
                return false;
            }
            return isFreshLiteralType(r.checker.getTypeOfSymbol(s));
        }
        return false;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsExpandoFunctionDeclarationUnsafe(this ref EmitResolver r, ж<ast.Node> node) {
    if (!ast.IsParseTreeNode(node)) {
        return false;
    }
    var props = r.GetPropertiesOfContainerFunction(node);
    foreach (var (_, p) in range(props)) {
        if (ast.IsExpandoPropertyDeclaration(p.ValueDeclaration)) {
            return true;
        }
    }
    return false;
}

public static bool IsExpandoFunctionDeclaration(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.IsExpandoFunctionDeclarationUnsafe(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

internal static printer.SymbolAccessibilityResult isSymbolAccessible(this ref EmitResolver r, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, bool shouldComputeAliasToMarkVisible) {
    return r.checker.IsSymbolAccessible(symbol, enclosingDeclaration, meaning, shouldComputeAliasToMarkVisible);
}

public static printer.SymbolAccessibilityResult IsSymbolAccessible(this ref EmitResolver r, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, bool shouldComputeAliasToMarkVisible) {
    return r.isSymbolAccessible(symbol, enclosingDeclaration, meaning, shouldComputeAliasToMarkVisible);
}

} partial class checker_package {

internal static bool isConstEnumOrConstEnumOnlyModule(ref ast.Symbol s) {
    return isConstEnumSymbol(ref s) || (ast.SymbolFlags)(s.Flags & ast.SymbolFlagsConstEnumOnlyModule) != 0;
}

} partial class checkerᴛEmitResolver {

public static bool IsReferencedAliasDeclaration(this ж<EmitResolver> Ꮡr, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var c = r.checker;
        if (!c.canCollectSymbolAliasAccessibilityData || !ast.IsParseTreeNode(Ꮡnode)) {
            return true;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        if (ast.IsAliasSymbolDeclaration(Ꮡnode)) {
            var symbol = c.getSymbolOfDeclaration(Ꮡnode); if (symbol != nil) {
                var aliasLinks = c.ᏑaliasSymbolLinks.Get(symbol);
                if (aliasLinks.referenced) {
                    return true;
                }
                var target = aliasLinks.aliasTarget;
                if (target != nil && (ast.ModifierFlags)(node.ModifierFlags() & ast.ModifierFlagsExport) != 0 && (ast.SymbolFlags)(c.getSymbolFlags(target) & ast.SymbolFlagsValue) != 0 && (c.compilerOptions.ShouldPreserveConstEnums() || !isConstEnumOrConstEnumOnlyModule(ref (target).DerefOrNull()))) {
                    return true;
                }
            }
        }
        return false;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsValueAliasDeclaration(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        var c = r.checker;
        if (!c.canCollectSymbolAliasAccessibilityData || !ast.IsParseTreeNode(node)) {
            return true;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        return r.isValueAliasDeclarationWorker(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

internal static bool isValueAliasDeclarationWorker(this ref EmitResolver r, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var c = r.checker;

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
        return r.isAliasResolvedToValue(c.getSymbolOfDeclaration(Ꮡnode), false);
    }
    case var ᴛ when ᴛ == ast.KindImportClause || ᴛ == ast.KindNamespaceImport || ᴛ == ast.KindImportSpecifier || ᴛ == ast.KindExportSpecifier: {
        var symbol = c.getSymbolOfDeclaration(Ꮡnode);
        return symbol != nil && r.isAliasResolvedToValue(symbol, true);
    }
    case var ᴛ when ᴛ == ast.KindExportDeclaration: {
        var exportClause = node.AsExportDeclaration().ExportClause;
        return exportClause != nil && (ast.IsNamespaceExport(exportClause) || core.Some(exportClause.Elements(), r.isValueAliasDeclaration));
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        if (node.Expression() != nil && node.Expression().Kind == ast.KindIdentifier) {
            return r.isAliasResolvedToValue(c.getSymbolOfDeclaration(Ꮡnode), true);
        }
        return true;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        if (isCommonJSModuleExports(Ꮡnode) && ast.IsIdentifier(node.AsBinaryExpression().Right)) {
            return r.isAliasResolvedToValue(c.getSymbolOfDeclaration(Ꮡnode), true);
        }
        break;
    }}

    return false;
}

internal static bool isAliasResolvedToValue(this ref EmitResolver r, ж<ast.Symbol> Ꮡsymbol, bool excludeTypeOnlyValues) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var c = r.checker;
    if (Ꮡsymbol == nil) {
        return false;
    }
    if (symbol.ValueDeclaration != nil) {
        var container = ast.GetSourceFileOfNode(symbol.ValueDeclaration); if (container != nil) {
            var fileSymbol = c.getSymbolOfDeclaration(container.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            c.resolveExternalModuleSymbol(fileSymbol, false);
        }
    }
    var target = c.getExportSymbolOfValueSymbolIfExported(c.resolveAlias(Ꮡsymbol));
    if (target == c.unknownSymbol) {
        return !excludeTypeOnlyValues || c.getTypeOnlyAliasDeclaration(Ꮡsymbol) == nil;
    }
    return (ast.SymbolFlags)(c.getSymbolFlagsEx(Ꮡsymbol, excludeTypeOnlyValues, true) & ast.SymbolFlagsValue) != 0 && (c.compilerOptions.ShouldPreserveConstEnums() || !isConstEnumOrConstEnumOnlyModule(ref (target).DerefOrNull()));
}

public static bool IsTopLevelValueImportEqualsWithEntityName(this ж<EmitResolver> Ꮡr, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var c = r.checker;
        if (!c.canCollectSymbolAliasAccessibilityData) {
            return true;
        }
        if (!ast.IsParseTreeNode(Ꮡnode) || node.Kind != ast.KindImportEqualsDeclaration || node.Parent.Kind != ast.KindSourceFile) {
            return false;
        }
        if (ast.IsImportEqualsDeclaration(Ꮡnode) && (ast.NodeIsMissing(node.AsImportEqualsDeclaration().ModuleReference) || node.AsImportEqualsDeclaration().ModuleReference.Kind == ast.KindExternalModuleReference)) {
            return false;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        return r.isAliasResolvedToValue(c.getSymbolOfDeclaration(Ꮡnode), false);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static void MarkLinkedReferencesRecursively(this ж<EmitResolver> Ꮡr, ж<ast.SourceFile> Ꮡfile) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var @file = ref Ꮡfile.DerefOrNull();

        if (!ast.IsParseTreeNode(Ꮡfile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            return;
        }
        r.checkerMu.Lock();
        ᒐd1 = true;

        if (Ꮡfile != nil) {
            ref var visit = ref heap<Func<ж<ast.Node>, bool>>(out var Ꮡvisit);
            visit = (ж<ast.Node> n) => {
                if (ast.IsImportEqualsDeclaration(n) && (ast.ModifierFlags)(n.ModifierFlags() & ast.ModifierFlagsExport) == 0) {
                    return false;
                }
                if (ast.IsImportDeclaration(n)) {
                    return false;
                }
                Ꮡr.Value.checker.markLinkedReferences(n, ReferenceHintUnspecified, nil, nil);
                n.ForEachChild(Ꮡvisit.ValueSlot);
                return false;
            };
            @file.ForEachChild(visit);
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<ast.SourceFile> GetExternalModuleFileFromDeclaration(this ж<EmitResolver> Ꮡr, ж<ast.Node> declaration) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(declaration)) {
            return default;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.checker.getExternalModuleFileFromDeclaration(declaration);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

internal static binder.ReferenceResolver getReferenceResolver(this ref EmitResolver r) {
    if (r.referenceResolver == nil) {
        r.referenceResolver = binder.NewReferenceResolver(r.checker.compilerOptions, new binder.ReferenceResolverHooks(
            ResolveName: r.checker.resolveName,
            GetResolvedSymbol: r.checker.getResolvedSymbolOrNil,
            GetMergedSymbol: r.checker.getMergedSymbol,
            GetParentOfSymbol: r.checker.getParentOfSymbol,
            GetSymbolOfDeclaration: r.checker.getSymbolOfDeclaration,
            GetTypeOnlyAliasDeclaration: r.checker.getTypeOnlyAliasDeclarationEx,
            GetExportSymbolOfValueSymbolIfExported: r.checker.getExportSymbolOfValueSymbolIfExported,
            GetElementAccessExpressionName: r.checker.tryGetElementAccessExpressionName
        ));
    }
    return r.referenceResolver;
}

public static ж<ast.Node> GetReferencedExportContainer(this ж<EmitResolver> Ꮡr, ж<astꓸIdentifierNode> node, bool prefixLocals) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(node)) {
            return default;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        return r.getReferenceResolver().GetReferencedExportContainer(node, prefixLocals);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static void SetReferencedImportDeclaration(this ж<EmitResolver> Ꮡr, ж<astꓸIdentifierNode> node, ж<astꓸDeclaration> @ref) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        r.jsxLinks.Get(node).importRef = @ref;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<astꓸDeclaration> GetReferencedImportDeclaration(this ж<EmitResolver> Ꮡr, ж<astꓸIdentifierNode> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        if (!ast.IsParseTreeNode(node)) {
            return r.jsxLinks.Get(node).importRef;
        }

        var symbol = r.checker.getReferencedValueOrAliasSymbol(node);
        if (ast.IsNonLocalAlias(symbol, ast.SymbolFlagsValue) && r.checker.getTypeOnlyAliasDeclarationEx(symbol, ast.SymbolFlagsValue) == nil) {
            return r.checker.getDeclarationOfAliasSymbol(symbol);
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<astꓸDeclaration> GetReferencedValueDeclaration(this ж<EmitResolver> Ꮡr, ж<astꓸIdentifierNode> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(node)) {
            return default;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        return r.getReferenceResolver().GetReferencedValueDeclaration(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<astꓸDeclaration> GetReferencedValueDeclarationUnsafe(this ref EmitResolver r, ж<astꓸIdentifierNode> node) {
    return r.getReferenceResolver().GetReferencedValueDeclaration(node);
}

public static slice<ж<astꓸDeclaration>> GetReferencedValueDeclarations(this ж<EmitResolver> Ꮡr, ж<astꓸIdentifierNode> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(node)) {
            return default;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        return r.getReferenceResolver().GetReferencedValueDeclarations(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsNameResolvable(this ж<EmitResolver> Ꮡr, ж<ast.Node> location, @string name) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;

        var symbol = r.checker.resolveName(location, name, (ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsType) | ast.SymbolFlagsNamespace), nil, false, false);
        return symbol != nil;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static @string GetElementAccessExpressionName(this ж<EmitResolver> Ꮡr, ж<ast.ElementAccessExpression> expression) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(expression.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            return "";
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        return r.getReferenceResolver().GetElementAccessExpressionName(expression);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<astꓸDeclaration> GetReferencedMemberValueDeclaration(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (!ast.IsParseTreeNode(node)) {
            return default;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        return r.getReferenceResolver().GetReferencedMemberValueDeclaration(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<ast.Node> CreateReturnTypeOfSignatureDeclaration(this ж<EmitResolver> Ꮡr, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> signatureDeclaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var emitContext = ref ᏑemitContext.DerefOrNull();

        var original = emitContext.ParseNode(signatureDeclaration);
        if (original == nil) {
            return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
        }

        r.checkerMu.Lock();
        ᒐd1 = true;
        var requestNodeBuilder = NewNodeBuilder(r.checker, ᏑemitContext);
        return requestNodeBuilder.SerializeReturnTypeForSignature(original, enclosingDeclaration, flags, internalFlags, tracker);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static slice<ж<ast.Node>> CreateTypeParametersOfSignatureDeclaration(this ж<EmitResolver> Ꮡr, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> signatureDeclaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var emitContext = ref ᏑemitContext.DerefOrNull();

        var original = emitContext.ParseNode(signatureDeclaration);
        if (original == nil) {
            return default;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;
        var requestNodeBuilder = NewNodeBuilder(r.checker, ᏑemitContext);
        return requestNodeBuilder.SerializeTypeParametersForSignature(original, enclosingDeclaration, flags, internalFlags, tracker);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<ast.Node> CreateTypeOfDeclaration(this ж<EmitResolver> Ꮡr, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> declaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var emitContext = ref ᏑemitContext.DerefOrNull();

        var original = emitContext.ParseNode(declaration);
        if (original == nil) {
            return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
        }

        r.checkerMu.Lock();
        ᒐd1 = true;
        var requestNodeBuilder = NewNodeBuilder(r.checker, ᏑemitContext);
        var symbol = r.checker.getSymbolOfDeclaration(declaration);
        return requestNodeBuilder.SerializeTypeForDeclaration(declaration, symbol, enclosingDeclaration, (nodebuilder.Flags)(flags | nodebuilder.FlagsMultilineObjectLiterals), internalFlags, tracker);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<ast.Node> CreateLiteralConstValue(this ж<EmitResolver> Ꮡr, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> Ꮡnode, nodebuilder.SymbolTracker tracker) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var emitContext = ref ᏑemitContext.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        Ꮡnode = emitContext.ParseNode(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        r.checkerMu.Lock();
        var t = r.checker.getTypeOfSymbol(r.checker.getSymbolOfDeclaration(Ꮡnode));
        r.checkerMu.Unlock();
        if (t == nil) {
            return default;
        }

        ж<ast.Node> enumResult = default;
        if ((TypeFlags)(t.flags & TypeFlagsEnumLike) != 0) {
            r.checkerMu.Lock();
            ᒐd1 = true;
            var requestNodeBuilder = NewNodeBuilder(r.checker, ᏑemitContext);
            enumResult = requestNodeBuilder.SymbolToExpression(t.symbol, ast.SymbolFlagsValue, Ꮡnode, nodebuilder.FlagsNone, nodebuilder.InternalFlagsNone, tracker);
        } else if (t == r.checker.trueType) {
            enumResult = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindTrueKeyword);
        } else if (t == r.checker.falseType) {
            enumResult = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindFalseKeyword);
        }
        if (enumResult != nil) {
            return enumResult;
        }
        if ((TypeFlags)(t.flags & TypeFlagsLiteral) == 0) {
            return default;
        }
        switch (t.AsLiteralType().value.type()) {
        case @string value: {
            return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(value, ast.TokenFlagsNone);
        }
        case jsnum.Number value: {
            if (value.IsInf()) {
                if (value > 0D) {
                    return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity");
                }
                return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindMinusToken, emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity"));
            }
            if (value.IsNaN()) {
                return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("NaN");
            }
            if (value.Abs() != value) {
                return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(
                    ast.KindMinusToken,
                    emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(value.String()[1..], ast.TokenFlagsNone));
            }
            return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(value.String(), ast.TokenFlagsNone);
        }
        case jsnum.PseudoBigInt value: {
            return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewBigIntLiteral(pseudoBigIntToString(value) + "n"u8, ast.TokenFlagsNone);
        }
        case bool value: {
            var kind = ast.KindFalseKeyword;
            if (value) {
                kind = ast.KindTrueKeyword;
            }
            return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(kind);
        }}
        throw panic("unhandled literal const value kind");
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ж<ast.Node> CreateTypeOfExpression(this ж<EmitResolver> Ꮡr, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> Ꮡexpression, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var emitContext = ref ᏑemitContext.DerefOrNull();
        ref var expression = ref Ꮡexpression.DerefOrNull();

        Ꮡexpression = emitContext.ParseNode(Ꮡexpression); expression = ref Ꮡexpression.DerefOrNull();
        if (Ꮡexpression == nil) {
            return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordTypeNode(ast.KindAnyKeyword);
        }

        r.checkerMu.Lock();
        ᒐd1 = true;
        var requestNodeBuilder = NewNodeBuilder(r.checker, ᏑemitContext);
        return requestNodeBuilder.SerializeTypeForExpression(Ꮡexpression, enclosingDeclaration, (nodebuilder.Flags)(flags | nodebuilder.FlagsMultilineObjectLiterals), internalFlags, tracker);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static slice<ж<ast.Node>> CreateLateBoundIndexSignatures(this ж<EmitResolver> Ꮡr, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> Ꮡcontainer, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var emitContext = ref ᏑemitContext.DerefOrNull();
        ref var container = ref Ꮡcontainer.DerefOrNull();

        Ꮡcontainer = emitContext.ParseNode(Ꮡcontainer); container = ref Ꮡcontainer.DerefOrNull();
        r.checkerMu.Lock();
        ᒐd1 = true;

        var sym = container.Symbol();
        var staticInfos = r.checker.getIndexInfosOfType(r.checker.getTypeOfSymbol(sym));
        var instanceIndexSymbol = r.checker.getIndexSymbol(sym);
        slice<ж<IndexInfo>> instanceInfos = default;
        if (instanceIndexSymbol != nil) {
            var siblingSymbols = slices.Collect(maps.Values<ast.SymbolTable, @string, ж<ast.Symbol>>(r.checker.getMembersOfSymbol(sym)));
            instanceInfos = r.checker.getIndexInfosOfIndexSymbol(instanceIndexSymbol, siblingSymbols);
        }

        var requestNodeBuilder = NewNodeBuilder(r.checker, ᏑemitContext);

        slice<ж<ast.Node>> result = default;
        foreach (var (i, infoList) in range(slice<slice<ж<IndexInfo>>>([staticInfos, instanceInfos]))) {
            var isStatic = true;
            if (i > 0) {
                isStatic = false;
            }
            if (len(infoList) == 0) {
                continue;
            }
            foreach (var (_, info) in range(infoList)) {
                if (info.declaration != nil) {
                    continue;
                }
                if (info == r.checker.anyBaseTypeIndexInfo) {
                    continue;
                }
                if (len(info.components) != 0) {
                    var allComponentComputedNamesSerializable = enclosingDeclaration != nil && core.Every(info.components, (ж<ast.Node> c) => c.Name() != nil && ast.IsComputedPropertyName(c.Name()) && ast.IsEntityNameExpression(c.Name().Expression()) && Ꮡr.isEntityNameVisible(c.Name().Expression(), enclosingDeclaration, false).Accessibility == printer.SymbolAccessibilityAccessible);
                    if (allComponentComputedNamesSerializable) {
                        foreach (var (_, c) in range(info.components)) {
                            if (r.checker.hasLateBindableName(c)) {
                                continue;
                            }

                            var firstIdentifier = ast.GetFirstIdentifier(c.Name().Expression());
                            var name = r.checker.resolveName(firstIdentifier, firstIdentifier.Text(), (ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsExportValue), nil, true, false);
                            if (name != nil) {
                                tracker.TrackSymbol(name, enclosingDeclaration, ast.SymbolFlagsValue);
                            }

                            var mods = core.IfElse(isStatic, slice<ж<ast.Node>>([emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindStaticKeyword)]), (slice<ж<ast.Node>>)(default));
                            if (info.isReadonly) {
                                mods = append(mods, emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindReadonlyKeyword));
                            }

                            var decl = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(
                                core.IfElse(mods != nil, emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(mods), (ж<ast.ModifierList>)(nil)),
                                c.Name(),
                                c.QuestionToken(),
                                requestNodeBuilder.TypeToTypeNode(r.checker.getTypeOfSymbol(c.Symbol()), enclosingDeclaration, flags, internalFlags, tracker),
                                nil);
                            result = append(result, decl);
                        }
                        continue;
                    }
                }
                var node = requestNodeBuilder.IndexInfoToIndexSignatureDeclaration(info, enclosingDeclaration, flags, internalFlags, tracker);
                if (node != nil && isStatic) {
                    var modNodes = slice<ж<ast.Node>>([emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindStaticKeyword)]);
                    modNodes = appendꓸꓸꓸ(modNodes, node.ModifierNodes());
                    var mods = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(modNodes);
                    node = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateIndexSignatureDeclaration(
                        node.AsIndexSignatureDeclaration(),
                        mods,
                        node.ParameterList(),
                        node.Type());
                }
                if (node != nil) {
                    result = append(result, node);
                }
            }
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static ast.ModifierFlags GetEffectiveDeclarationFlags(this ж<EmitResolver> Ꮡr, ж<ast.Node> node, ast.ModifierFlags flags) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.checker.GetEffectiveDeclarationFlags(node, flags);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static any GetConstantValue(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;
        return r.checker.GetConstantValue(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static printer.TypeReferenceSerializationKind GetTypeReferenceSerializationKind(this ж<EmitResolver> Ꮡr, ж<ast.Node> typeName, ж<ast.Node> location) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        r.checkerMu.Lock();
        ᒐd1 = true;

        if (typeName == nil || location == nil) {
            return printer.TypeReferenceSerializationKindUnknown;
        }

        var isTypeOnly = false;
        if (ast.IsQualifiedName(typeName)) {
            var rootValueSymbol = r.checker.resolveEntityName(ast.GetFirstIdentifier(typeName), ast.SymbolFlagsValue, true, true, location);

            if (rootValueSymbol != nil && len(rootValueSymbol.Declarations) > 0) {
                isTypeOnly = core.Every<ж<ast.Node>>(rootValueSymbol.Declarations, ast.IsTypeOnlyImportOrExportDeclaration);
            }
        }
        var valueSymbol = r.checker.resolveEntityName(typeName, ast.SymbolFlagsValue, true, true, location);
        var resolvedValueSymbol = valueSymbol;
        if (valueSymbol != nil && (ast.SymbolFlags)(valueSymbol.Flags & ast.SymbolFlagsAlias) != 0) {
            resolvedValueSymbol = r.checker.resolveAlias(valueSymbol);
        }

        isTypeOnly = isTypeOnly || (valueSymbol != nil && r.checker.getTypeOnlyAliasDeclarationEx(valueSymbol, ast.SymbolFlagsValue) != nil);

        var typeSymbol = r.checker.resolveEntityName(typeName, ast.SymbolFlagsType, true, true, location);
        var resolvedTypeSymbol = typeSymbol;
        if (typeSymbol != nil && (ast.SymbolFlags)(typeSymbol.Flags & ast.SymbolFlagsAlias) != 0) {
            resolvedTypeSymbol = r.checker.resolveAlias(typeSymbol);
        }
        isTypeOnly = isTypeOnly || (typeSymbol != nil && r.checker.getTypeOnlyAliasDeclarationEx(typeSymbol, ast.SymbolFlagsType) != nil);

        if (resolvedValueSymbol != nil && resolvedValueSymbol == resolvedTypeSymbol) {
            var globalPromiseSymbol = r.checker.getGlobalPromiseConstructorSymbol();
            if (globalPromiseSymbol != nil && resolvedValueSymbol == globalPromiseSymbol) {
                return printer.TypeReferenceSerializationKindPromise;
            }

            var constructorType = r.checker.getTypeOfSymbol(resolvedValueSymbol);
            if (constructorType != nil && r.checker.isConstructorType(constructorType)) {
                if (isTypeOnly) {
                    return printer.TypeReferenceSerializationKindTypeWithCallSignature;
                }
                return printer.TypeReferenceSerializationKindTypeWithConstructSignatureAndValue;
            }
        }

        if (resolvedTypeSymbol == nil) {
            if (isTypeOnly) {
                return printer.TypeReferenceSerializationKindObjectType;
            }
            return printer.TypeReferenceSerializationKindUnknown;
        }

        var type_ = r.checker.getDeclaredTypeOfSymbol(resolvedTypeSymbol);
        if (r.checker.isErrorType(type_)) {
            if (isTypeOnly) {
                return printer.TypeReferenceSerializationKindObjectType;
            }
            return printer.TypeReferenceSerializationKindUnknown;
        }

        if ((TypeFlags)(type_.flags & TypeFlagsAnyOrUnknown) != 0) {
            return printer.TypeReferenceSerializationKindObjectType;
        } else if (r.checker.isTypeAssignableToKind(type_, (TypeFlags)((TypeFlags)(TypeFlagsVoid | TypeFlagsNullable) | TypeFlagsNever))) {
            return printer.TypeReferenceSerializationKindVoidNullableOrNeverType;
        } else if (r.checker.isTypeAssignableToKind(type_, TypeFlagsBooleanLike)) {
            return printer.TypeReferenceSerializationKindBooleanType;
        } else if (r.checker.isTypeAssignableToKind(type_, TypeFlagsNumberLike)) {
            return printer.TypeReferenceSerializationKindNumberLikeType;
        } else if (r.checker.isTypeAssignableToKind(type_, TypeFlagsBigIntLike)) {
            return printer.TypeReferenceSerializationKindBigIntLikeType;
        } else if (r.checker.isTypeAssignableToKind(type_, TypeFlagsStringLike)) {
            return printer.TypeReferenceSerializationKindStringLikeType;
        } else if (isTupleType(type_)) {
            return printer.TypeReferenceSerializationKindArrayLikeType;
        } else if (r.checker.isTypeAssignableToKind(type_, TypeFlagsESSymbolLike)) {
            return printer.TypeReferenceSerializationKindESSymbolType;
        } else if (r.checker.isFunctionType(type_)) {
            return printer.TypeReferenceSerializationKindTypeWithCallSignature;
        } else if (r.checker.isArrayType(type_)) {
            return printer.TypeReferenceSerializationKindArrayLikeType;
        } else {
            return printer.TypeReferenceSerializationKindObjectType;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static slice<ж<ast.Symbol>> GetPropertiesOfContainerFunction(this ref EmitResolver r, ж<ast.Node> node) {
    if (node == nil) {
        return [];
    }
    var s = r.checker.getSymbolOfDeclaration(node);
    if (s == nil) {
        return [];
    }
    return r.checker.getPropertiesOfType(r.checker.getTypeOfSymbol(s));
}

public static ж<ast.Node> TryJSTypeNodeToTypeNode(this ж<EmitResolver> Ꮡr, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> ᏑtypeNode, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var emitContext = ref ᏑemitContext.DerefOrNull();
        ref var typeNode = ref ᏑtypeNode.DerefOrNull();

        ᏑtypeNode = emitContext.ParseNode(ᏑtypeNode); typeNode = ref ᏑtypeNode.DerefOrNull();
        r.checkerMu.Lock();
        ᒐd1 = true;

        var requestNodeBuilder = NewNodeBuilder(r.checker, ᏑemitContext);
        return requestNodeBuilder.TryJSTypeNodeToTypeNode(ᏑtypeNode, enclosingDeclaration, flags, internalFlags, tracker);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

public static bool IsThisPropertyAssignmentDeclarationRedundant(this ж<EmitResolver> Ꮡr, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var r = ref Ꮡr.DerefOrNull();

        if (node == nil) {
            return false;
        }

        r.checkerMu.Lock();
        ᒐd1 = true;

        var s = r.checker.getSymbolOfDeclaration(node);
        if (s == nil || s.Parent == nil) {
            return false;
        }
        var parentType = r.checker.getDeclaredTypeOfSymbol(s.Parent);
        if (parentType == nil) {
            return false;
        }
        foreach (var (_, @base) in range(r.checker.getBaseTypes(parentType))) {
            var baseProp = r.checker.getPropertyOfType(@base, s.Name);
            if (baseProp == nil) {
                continue;
            }
            if ((ast.SymbolFlags)(baseProp.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsAccessor | ast.SymbolFlagsMethod) | ast.SymbolFlagsFunction))) != 0) {
                return true;
            }
            if (r.checker.isReadonlySymbol(baseProp) == r.checker.isReadonlySymbol(s) && ((ast.SymbolFlags)(s.Flags & ast.SymbolFlagsOptional)) == ((ast.SymbolFlags)(baseProp.Flags & ast.SymbolFlagsOptional)) && r.checker.isTypeIdenticalTo(r.checker.getTypeOfSymbol(s), r.checker.getTypeOfSymbol(baseProp))) {
                return true;
            }
        }
        return false;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡr.DerefOrNull().checkerMu.Unlock(); ᒐ.Run(); }
}

} // end checkerᴛEmitResolver
