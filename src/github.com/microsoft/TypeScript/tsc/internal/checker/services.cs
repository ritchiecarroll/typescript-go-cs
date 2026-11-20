namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using astnav = go.github.com.microsoft.TypeScript.tsc.@internal.astnav_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using static checker_package;

partial class checker_package {

} partial class checkerᴛChecker {

public static slice<ж<ast.Symbol>> GetSymbolsInScope(this ж<Checker> Ꮡc, ж<ast.Node> location, ast.SymbolFlags meaning) {
    return Ꮡc.getSymbolsInScope(location, meaning);
}

internal static slice<ж<ast.Symbol>> getSymbolsInScope(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡlocation, ast.SymbolFlags meaning) {
    ref var location = ref Ꮡlocation.DerefOrNull();

    if ((ast.NodeFlags)(location.Flags & ast.NodeFlagsInWithStatement) != 0) {
        return default;
    }

    var symbols = new ast.SymbolTable(0);
    var isStaticSymbol = false;

    var symbolsʗ1 = symbols;
    void copySymbol(ж<ast.Symbol> symbol, ast.SymbolFlags meaningΔ1) {
        if ((ast.SymbolFlags)(symbol.CombinedLocalAndExportSymbolFlags() & meaningΔ1) != 0) {
            @string id = symbol.Name;
            var (_, ok) = symbolsʗ1[id, ꟷ]; if (!ok) {
                symbolsʗ1[id] = symbol;
            }
        }
    }

    var copySymbolʗ1 = copySymbol;
    void copySymbols(ast.SymbolTable source, ast.SymbolFlags meaningΔ2) {
        if (meaningΔ2 != 0) {
            foreach (var (_, symbol) in source) {
                copySymbolʗ1(symbol, meaningΔ2);
            }
        }
    }

    var copySymbolʗ2 = copySymbol;
    void copyLocallyVisibleExportSymbols(ast.SymbolTable source, ast.SymbolFlags meaningΔ3) {
        if (meaningΔ3 != 0) {
            foreach (var (_, symbol) in source) {
                if (ast.GetDeclarationOfKind(symbol, ast.KindExportSpecifier) == nil && ast.GetDeclarationOfKind(symbol, ast.KindNamespaceExport) == nil && symbol.Name != ast.InternalSymbolNameDefault) {
                    copySymbolʗ2(symbol, meaningΔ3);
                }
            }
        }
    }

    var copyLocallyVisibleExportSymbolsʗ1 = copyLocallyVisibleExportSymbols;
    var copySymbolʗ3 = copySymbol;
    var copySymbolsʗ1 = copySymbols;
    void populateSymbols() {
        ж<ast.Node> lastLocation = default;
        while (Ꮡlocation != nil) {
            if (ast.IsModuleDeclaration(Ꮡlocation) && (~Ꮡlocation.Value.AsModuleDeclaration()).Attributes != nil && lastLocation == (~Ꮡlocation.Value.AsModuleDeclaration()).Attributes) {
                lastLocation = Ꮡlocation;
                Ꮡlocation = Ꮡlocation.Value.Parent;
                continue;
            }

            if (canHaveLocals(ref (Ꮡlocation).DerefOrNull()) && Ꮡlocation.Value.Locals() != nil && !ast.IsGlobalSourceFile(Ꮡlocation)) {
                copySymbolsʗ1(Ꮡlocation.Value.Locals(), meaning);
            }

            var exprᴛ1 = Ꮡlocation.Value.Kind;
            var matchᴛ1 = false;
            if (exprᴛ1 == ast.KindSourceFile) { matchᴛ1 = true;
                do {
                    if (!ast.IsExternalModule(Ꮡlocation.Value.AsSourceFile())) {
                        break;
                    }
                    fallthrough = true;
                } while (false);
            }
            if (fallthrough || !matchᴛ1 && exprᴛ1 == ast.KindModuleDeclaration) { matchᴛ1 = true;
                copyLocallyVisibleExportSymbolsʗ1(Ꮡc.getSymbolOfDeclaration(Ꮡlocation).Exports, (ast.SymbolFlags)(meaning & ast.SymbolFlagsModuleMember));
            }
            else if (exprᴛ1 == ast.KindEnumDeclaration) { matchᴛ1 = true;
                copySymbolsʗ1(Ꮡc.getSymbolOfDeclaration(Ꮡlocation).Exports, (ast.SymbolFlags)(meaning & ast.SymbolFlagsEnumMember));
            }
            else if (exprᴛ1 == ast.KindClassExpression) { matchᴛ1 = true;
                var className = Ꮡlocation.Value.AsClassExpression().Name();
                if (className != nil) {
                    copySymbolʗ3(Ꮡlocation.Value.Symbol(), meaning);
                }
                fallthrough = true;
            }
            if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindClassDeclaration || exprᴛ1 == ast.KindInterfaceDeclaration)) {
                if (!isStaticSymbol) {
                    copySymbolsʗ1(Ꮡc.getMembersOfSymbol(Ꮡc.getSymbolOfDeclaration(Ꮡlocation)), (ast.SymbolFlags)(meaning & ast.SymbolFlagsType));
                }
            }
            else if (exprᴛ1 == ast.KindFunctionExpression) { matchᴛ1 = true;
                var funcName = Ꮡlocation.Value.Name();
                if (funcName != nil) {
                    copySymbolʗ3(Ꮡlocation.Value.Symbol(), meaning);
                }
            }

            if (introducesArgumentsExoticObject(ref (Ꮡlocation).DerefOrNull())) {
                copySymbolʗ3(Ꮡc.Value.argumentsSymbol, meaning);
            }

            isStaticSymbol = ast.IsStatic(Ꮡlocation);
            lastLocation = Ꮡlocation;
            Ꮡlocation = Ꮡlocation.Value.Parent;
        }

        copySymbolsʗ1(Ꮡc.Value.globals, meaning);
    }

    populateSymbols();

    delete(symbols, ast.InternalSymbolNameThis);
    return symbolsToArray(symbols);
}

public static slice<ж<ast.Symbol>> GetExportsOfModule(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return symbolsToArray(Ꮡc.getExportsOfModule(symbol));
}

public static void ForEachExportAndPropertyOfModule(this ж<Checker> Ꮡc, ж<ast.Symbol> moduleSymbol, Action<ж<ast.Symbol>, @string> cb) {
    ref var c = ref Ꮡc.DerefOrNull();

    foreach (var (key, exportedSymbol) in Ꮡc.getExportsOfModule(moduleSymbol)) {
        if (!isReservedMemberName(key)) {
            cb(exportedSymbol, key);
        }
    }

    var exportEquals = Ꮡc.resolveExternalModuleSymbol(moduleSymbol, false);
    if (exportEquals == moduleSymbol) {
        return;
    }

    var typeOfSymbol = Ꮡc.getTypeOfSymbol(exportEquals);
    if (!c.shouldTreatPropertiesOfExternalModuleAsExports(typeOfSymbol)) {
        return;
    }

    var reducedType = Ꮡc.getReducedApparentType(typeOfSymbol);
    if ((TypeFlags)(reducedType.flags & TypeFlagsStructuredType) == 0) {
        return;
    }
    foreach (var (name, symbol) in Ꮡc.resolveStructuredTypeMembers(reducedType).members) {
        if (Ꮡc.isNamedMember(symbol, name)) {
            cb(symbol, name);
        }
    }
}

public static bool IsValidPropertyAccess(this ж<Checker> Ꮡc, ж<ast.Node> node, @string propertyName) {
    return Ꮡc.isValidPropertyAccess(node, propertyName);
}

internal static bool isValidPropertyAccess(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, @string propertyName) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        return Ꮡc.isValidPropertyAccessWithType(Ꮡnode, node.Expression().Kind == ast.KindSuperKeyword, propertyName, Ꮡc.getWidenedType(Ꮡc.checkExpression(node.Expression())));
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        return Ꮡc.isValidPropertyAccessWithType(Ꮡnode, false, propertyName, Ꮡc.getWidenedType(Ꮡc.checkExpression(node.AsQualifiedName().Left)));
    }
    case var ᴛ when ᴛ == ast.KindImportType: {
        return Ꮡc.isValidPropertyAccessWithType(Ꮡnode, false, propertyName, Ꮡc.getTypeFromTypeNode(Ꮡnode));
    }}

    throw panic("Unexpected node kind in isValidPropertyAccess: " + node.Kind.String());
}

internal static bool isValidPropertyAccessWithType(this ж<Checker> Ꮡc, ж<ast.Node> node, bool isSuper, @string propertyName, ж<Type> t) {
    if (IsTypeAny(t)) {
        return true;
    }

    var prop = Ꮡc.getPropertyOfType(t, propertyName);
    return prop != nil && Ꮡc.isPropertyAccessible(node, isSuper, false, t, prop);
}

public static bool IsValidPropertyAccessForCompletions(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> t, ж<ast.Symbol> property) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡc.isPropertyAccessible(
        Ꮡnode,
        node.Kind == ast.KindPropertyAccessExpression && node.Expression().Kind == ast.KindSuperKeyword,
        false,
        t,
        property);
}

public static slice<ж<ast.Symbol>> GetAllPossiblePropertiesOfTypes(this ж<Checker> Ꮡc, slice<ж<Type>> types) {
    var unionType = Ꮡc.getUnionType(types);
    if ((TypeFlags)(unionType.flags & TypeFlagsUnion) == 0) {
        return Ꮡc.getAugmentedPropertiesOfType(unionType);
    }

    var props = new ast.SymbolTable(0);
    foreach (var (_, memberType) in range(types)) {
        var augmentedProps = Ꮡc.getAugmentedPropertiesOfType(memberType);
        foreach (var (_, p) in range(augmentedProps)) {
            var (_, ok) = props[p.Name, ꟷ]; if (!ok) {
                var prop = Ꮡc.createUnionOrIntersectionProperty(unionType, p.Name, false);
                if (prop != nil) {
                    props[p.Name] = prop;
                }
            }
        }
    }
    return slices.Collect(maps.Values<ast.SymbolTable, @string, ж<ast.Symbol>>(props));
}

public static bool IsUnknownSymbol(this ref Checker c, ж<ast.Symbol> symbol) {
    return symbol == c.unknownSymbol;
}

public static bool IsUndefinedSymbol(this ref Checker c, ж<ast.Symbol> symbol) {
    return symbol == c.undefinedSymbol;
}

public static bool IsArgumentsSymbol(this ref Checker c, ж<ast.Symbol> symbol) {
    return symbol == c.argumentsSymbol;
}

public static ж<Type> GetNonOptionalType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.removeOptionalTypeMarker(t);
}

public static ж<Type> GetStringIndexType(this ж<Checker> Ꮡc, ж<Type> t) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.getIndexTypeOfType(t, c.stringType);
}

public static ж<Type> GetNumberIndexType(this ж<Checker> Ꮡc, ж<Type> t) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.getIndexTypeOfType(t, c.numberType);
}

public static ж<Type> GetElementTypeOfArrayType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getElementTypeOfArrayType(t);
}

public static slice<ж<Signature>> GetCallSignatures(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getSignaturesOfType(t, SignatureKindCall);
}

public static slice<ж<Signature>> GetConstructSignatures(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getSignaturesOfType(t, SignatureKindConstruct);
}

public static slice<ж<ast.Symbol>> GetApparentProperties(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getAugmentedPropertiesOfType(t);
}

internal static slice<ж<ast.Symbol>> getAugmentedPropertiesOfType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    Ꮡt = Ꮡc.getApparentType(Ꮡt); t = ref Ꮡt.DerefOrNull();
    var propsByName = createSymbolTable(Ꮡc.getPropertiesOfType(Ꮡt));
    ж<Type> functionType = default;
    if (len(Ꮡc.getSignaturesOfType(Ꮡt, SignatureKindCall)) > 0) {
        functionType = c.globalCallableFunctionType;
    } else if (len(Ꮡc.getSignaturesOfType(Ꮡt, SignatureKindConstruct)) > 0) {
        functionType = c.globalNewableFunctionType;
    }

    if (propsByName == nil) {
        propsByName = new ast.SymbolTable(0);
    }
    if (functionType != nil) {
        foreach (var (_, p) in range(Ꮡc.getPropertiesOfType(functionType))) {
            var (_, ok) = propsByName[p.Name, ꟷ]; if (!ok) {
                propsByName[p.Name] = p;
            }
        }
    }
    return Ꮡc.getNamedMembers(propsByName, nil);
}

public static ж<ast.Symbol> TryGetMemberInModuleExportsAndProperties(this ж<Checker> Ꮡc, @string memberName, ж<ast.Symbol> moduleSymbol) {
    ref var c = ref Ꮡc.DerefOrNull();

    var symbol = Ꮡc.TryGetMemberInModuleExports(memberName, moduleSymbol);
    if (symbol != nil) {
        return symbol;
    }

    var exportEquals = Ꮡc.resolveExternalModuleSymbol(moduleSymbol, false);
    if (exportEquals == moduleSymbol) {
        return default;
    }

    var t = Ꮡc.getTypeOfSymbol(exportEquals);
    if (c.shouldTreatPropertiesOfExternalModuleAsExports(t)) {
        return Ꮡc.getPropertyOfType(t, memberName);
    }
    return default;
}

public static ж<ast.Symbol> TryGetMemberInModuleExports(this ж<Checker> Ꮡc, @string memberName, ж<ast.Symbol> moduleSymbol) {
    var symbolTable = Ꮡc.getExportsOfModule(moduleSymbol);
    return symbolTable[memberName];
}

internal static bool shouldTreatPropertiesOfExternalModuleAsExports(this ref Checker c, ж<Type> ᏑresolvedExternalModuleType) {
    ref var resolvedExternalModuleType = ref ᏑresolvedExternalModuleType.DerefOrNull();

    return (TypeFlags)(resolvedExternalModuleType.flags & TypeFlagsPrimitive) == 0 || (ObjectFlags)(resolvedExternalModuleType.objectFlags & ObjectFlagsClass) != 0 || c.isArrayType(ᏑresolvedExternalModuleType) || isTupleType(ᏑresolvedExternalModuleType);
}

public static ж<Type> GetContextualType(this ж<Checker> Ꮡc, ж<astꓸExpression> node, ContextFlags contextFlags) {
    if ((ContextFlags)(contextFlags & ContextFlagsIgnoreNodeInferences) != 0) {
        return runWithInferenceBlockedFromSourceNode(Ꮡc, node, ж<Type> () => Ꮡc.getContextualType(node, contextFlags));
    }
    return Ꮡc.getContextualType(node, contextFlags);
}

} partial class checker_package {

internal static T runWithInferenceBlockedFromSourceNode<T>(ж<Checker> Ꮡc, ж<ast.Node> node, Func<T> fn) {
    ref var c = ref Ꮡc.DerefOrNull();

    var containingCall = ast.FindAncestor(node, ast.IsCallLikeExpression);
    if (containingCall != nil) {
        var toMarkSkip = node;
        for (;;) {
            c.skipDirectInferenceNodes.Add(toMarkSkip);
            toMarkSkip = toMarkSkip.Parent;
            if (toMarkSkip == nil || toMarkSkip == containingCall) {
                break;
            }
        }
    }

    c.isInferencePartiallyBlocked = true;
    var result = runWithoutResolvedSignatureCaching(Ꮡc, node, fn);
    c.isInferencePartiallyBlocked = false;

    Ꮡc.ᏑskipDirectInferenceNodes.Clear();
    return result;
}

internal partial struct GetResolvedSignatureForSignatureHelp_result /*dyn*/ {
    internal ж<Signature> signature;
    internal slice<ж<Signature>> candidates;
}

public static (ж<Signature>, slice<ж<Signature>>) GetResolvedSignatureForSignatureHelp(ж<ast.Node> node, nint argumentCount, ж<Checker> c) {
    var res = runWithoutResolvedSignatureCaching(c, node, GetResolvedSignatureForSignatureHelp_result () => {
        var (signature, candidates) = c.getResolvedSignatureWorker(node, CheckModeIsForSignatureHelp, argumentCount);
        return new GetResolvedSignatureForSignatureHelp_result(signature, candidates);
    });
    return (res.signature, res.candidates);
}

internal static T runWithoutResolvedSignatureCaching<T>(ж<Checker> Ꮡc, ж<ast.Node> node, Func<T> fn) {
    ref var c = ref Ꮡc.DerefOrNull();

    var ancestorNode = ast.FindAncestor(node, ast.IsCallLikeOrFunctionLikeExpression);
    if (ancestorNode != nil) {
        var cachedResolvedSignatures = new map<ж<SignatureLinks>, ж<Signature>>();
        var cachedTypes = new map<ж<ValueSymbolLinks>, ж<Type>>();
        while (ancestorNode != nil) {
            var signatureLinks = c.signatureLinks.Get(ancestorNode);
            cachedResolvedSignatures[signatureLinks] = signatureLinks.resolvedSignature;
            signatureLinks.resolvedSignature = default;
            if (ast.IsFunctionExpressionOrArrowFunction(ancestorNode)) {
                var symbolLinks = c.valueSymbolLinks.Get(Ꮡc.getSymbolOfDeclaration(ancestorNode));
                var resolvedType = symbolLinks.resolvedType;
                cachedTypes[symbolLinks] = resolvedType;
                symbolLinks.resolvedType = default;
            }
            ancestorNode = ast.FindAncestor(ancestorNode.Parent, ast.IsCallLikeOrFunctionLikeExpression);
        }
        var result = fn();
        foreach (var (signatureLinks, resolvedSignature) in cachedResolvedSignatures) {
            signatureLinks.resolvedSignature = resolvedSignature;
        }
        foreach (var (symbolLinks, resolvedType) in cachedTypes) {
            symbolLinks.resolvedType = resolvedType;
        }
        return result;
    }
    return fn();
}

} partial class checkerᴛChecker {

public static ж<ast.Symbol> SkipAlias(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAlias) != 0) {
        return Ꮡc.GetAliasedSymbol(Ꮡsymbol);
    }
    return Ꮡsymbol;
}

public static slice<ж<ast.Symbol>> GetRootSymbols(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    var roots = Ꮡc.getImmediateRootSymbols(symbol);
    if (len(roots) == 0) {
        return [symbol];
    }
    slice<ж<ast.Symbol>> result = default;
    foreach (var (_, root) in range(roots)) {
        result = appendꓸꓸꓸ(result, Ꮡc.GetRootSymbols(root));
    }
    return result;
}

public static ж<ast.Symbol> GetMappedTypeSymbolOfProperty(this ref Checker c, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var valueLinks = c.valueSymbolLinks.TryGet(Ꮡsymbol); if (valueLinks != nil) {
        return valueLinks.containingType.symbol;
    }
    return default;
}

internal static slice<ж<ast.Symbol>> getImmediateRootSymbols(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if ((ast.CheckFlags)(symbol.CheckFlags & ast.CheckFlagsSynthetic) != 0) {
        return core.MapNonNil(
            c.valueSymbolLinks.Get(Ꮡsymbol).containingType.Types(),
            ж<ast.Symbol> (ж<Type> t) => Ꮡc.getPropertyOfType(t, Ꮡsymbol.Value.Name));
    }
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTransient) != 0) {
        if (c.spreadLinks.Has(Ꮡsymbol)) {
            var leftSpread = c.spreadLinks.Get(Ꮡsymbol).leftSpread;
            var rightSpread = c.spreadLinks.Get(Ꮡsymbol).rightSpread;
            if (leftSpread != nil) {
                return [leftSpread, rightSpread];
            }
        }
        if (c.mappedSymbolLinks.Has(Ꮡsymbol)) {
            var syntheticOrigin = c.mappedSymbolLinks.Get(Ꮡsymbol).syntheticOrigin;
            if (syntheticOrigin != nil) {
                return [syntheticOrigin];
            }
        }
        var target = c.tryGetTarget(Ꮡsymbol);
        if (target != nil) {
            return [target];
        }
    }
    return default;
}

internal static ж<ast.Symbol> tryGetTarget(this ref Checker c, ж<ast.Symbol> symbol) {
    ж<ast.Symbol> target = default;
    var next = symbol;
    for (;;) {
        if (c.valueSymbolLinks.Has(next)) {
            next = c.valueSymbolLinks.Get(next).target;
        } else if (c.exportTypeLinks.Has(next)) {
            next = c.exportTypeLinks.Get(next).target;
        } else {
            next = default;
        }
        if (next == nil) {
            break;
        }
        target = next;
    }
    return target;
}

public static ж<ast.Symbol> GetExportSymbolOfSymbol(this ref Checker c, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return c.getMergedSymbol(core.IfElse(symbol.ExportSymbol != nil, symbol.ExportSymbol, Ꮡsymbol));
}

public static ж<ast.Symbol> GetExportSpecifierLocalTargetSymbol(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindExportSpecifier: {
        if (node.Parent.Parent.ModuleSpecifier() != nil) {
            return Ꮡc.getExternalModuleMember(node.Parent.Parent, Ꮡnode, false);
        }
        var name = node.PropertyNameOrName();
        if (name.Kind == ast.KindStringLiteral) {
            return default;
        }
        return Ꮡc.resolveEntityName(name, (ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsType) | ast.SymbolFlagsNamespace) | ast.SymbolFlagsAlias), true, false, nil);
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        return Ꮡc.resolveEntityName(Ꮡnode, (ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsType) | ast.SymbolFlagsNamespace) | ast.SymbolFlagsAlias), true, false, nil);
    }}

    throw panic("Unhandled case in getExportSpecifierLocalTargetSymbol, node should be ExportSpecifier | Identifier");
}

public static ж<ast.Symbol> GetShorthandAssignmentValueSymbol(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡlocation) {
    ref var location = ref Ꮡlocation.DerefOrNull();

    if (Ꮡlocation != nil && location.Kind == ast.KindShorthandPropertyAssignment) {
        return Ꮡc.resolveEntityName(location.Name(), (ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsAlias), true, false, nil);
    }
    return default;
}

public static (ж<ast.Symbol>, ж<ast.Symbol>) GetSymbolsOfParameterPropertyDeclaration(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡparameter, @string parameterName) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();

    var constructorDeclaration = parameter.Parent;
    var classDeclaration = parameter.Parent.Parent;

    var parameterSymbol = Ꮡc.getSymbol(constructorDeclaration.Locals(), parameterName, ast.SymbolFlagsValue);
    var propertySymbol = Ꮡc.getSymbol(Ꮡc.getMembersOfSymbol(classDeclaration.Symbol()), parameterName, ast.SymbolFlagsValue);

    if (parameterSymbol != nil && propertySymbol != nil) {
        return (parameterSymbol, propertySymbol);
    }

    throw panic("There should exist two symbols, one as property declaration and one as parameter declaration");
}

public static bool IsDeclarationUsed(this ж<Checker> Ꮡc, ж<ast.SourceFile> sourceFile, ж<ast.Identifier> Ꮡidentifier, bool jsxElementsPresent, bool jsxModeNeedsExplicitImport) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var identifier = ref Ꮡidentifier.DerefOrNull();

    if (jsxElementsPresent && jsxModeNeedsExplicitImport) {
        @string jsxNamespace = c.getJsxNamespace(sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        @string jsxFragmentFactory = c.GetJsxFragmentFactory(sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        @string identifierText = identifier.Text;
        if (identifierText == jsxNamespace) {
            return true;
        }
        if (jsxFragmentFactory != ""u8 && identifierText == jsxFragmentFactory) {
            return true;
        }
    }

    var symbol = Ꮡc.GetSymbolAtLocation(Ꮡidentifier.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (symbol == nil) {
        return true;
    }

    return Ꮡc.IsSymbolReferencedInFile(sourceFile, Ꮡidentifier, symbol);
}

public static bool IsSymbolReferencedInFile(this ж<Checker> Ꮡc, ж<ast.SourceFile> sourceFile, ж<ast.Identifier> Ꮡdefinition, ж<ast.Symbol> symbol) {
    ref var definition = ref Ꮡdefinition.DerefOrNull();

    @string identifierText = definition.Text;
    foreach (var (_, token) in range(getPossibleSymbolReferenceNodes(sourceFile, identifierText, sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))) {
        if (!ast.IsIdentifier(token)) {
            continue;
        }
        var id = token.AsIdentifier();
        if (id == Ꮡdefinition || (~id).Text != identifierText) {
            continue;
        }
        var refSymbol = Ꮡc.GetSymbolAtLocation(token);
        if (refSymbol == symbol) {
            return true;
        }
        if (token.Parent != nil && token.Parent.Kind == ast.KindShorthandPropertyAssignment) {
            var shorthandSymbol = Ꮡc.GetShorthandAssignmentValueSymbol(token.Parent);
            if (shorthandSymbol == symbol) {
                return true;
            }
        }
        if (token.Parent != nil && ast.IsExportSpecifier(token.Parent)) {
            var localSymbol = Ꮡc.getLocalSymbolForExportSpecifier(token.AsIdentifier(), refSymbol, token.Parent.AsExportSpecifier());
            if (localSymbol == symbol) {
                return true;
            }
        }
    }
    return false;
}

public static slice<ж<ast.Node>> GetReferencesToSymbolInFile(this ж<Checker> Ꮡc, ж<ast.SourceFile> sourceFile, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    @string identifierText = symbol.Name;
    slice<ж<ast.Node>> result = default;
    foreach (var (_, token) in range(getPossibleSymbolReferenceNodes(sourceFile, identifierText, sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))) {
        if (!ast.IsIdentifier(token)) {
            continue;
        }
        var id = token.AsIdentifier();
        if ((~id).Text != identifierText) {
            continue;
        }
        var refSymbol = Ꮡc.GetSymbolAtLocation(token);
        if (refSymbol == Ꮡsymbol) {
            result = append(result, token);
            continue;
        }
        if (token.Parent != nil && token.Parent.Kind == ast.KindShorthandPropertyAssignment) {
            var shorthandSymbol = Ꮡc.GetShorthandAssignmentValueSymbol(token.Parent);
            if (shorthandSymbol == Ꮡsymbol) {
                result = append(result, token);
                continue;
            }
        }
        if (token.Parent != nil && ast.IsExportSpecifier(token.Parent)) {
            var localSymbol = Ꮡc.getLocalSymbolForExportSpecifier(token.AsIdentifier(), refSymbol, token.Parent.AsExportSpecifier());
            if (localSymbol == Ꮡsymbol) {
                result = append(result, token);
                continue;
            }
        }
    }
    return result;
}

internal static ж<ast.Symbol> getLocalSymbolForExportSpecifier(this ж<Checker> Ꮡc, ж<ast.Identifier> referenceLocation, ж<ast.Symbol> referenceSymbol, ж<ast.ExportSpecifier> exportSpecifier) {
    if (isExportSpecifierAlias(referenceLocation, exportSpecifier)) {
        var symbol = Ꮡc.GetExportSpecifierLocalTargetSymbol(exportSpecifier.of(ast.ExportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); if (symbol != nil) {
            return symbol;
        }
    }
    return referenceSymbol;
}

} partial class checker_package {

internal static bool isExportSpecifierAlias(ж<ast.Identifier> referenceLocation, ж<ast.ExportSpecifier> ᏑexportSpecifier) {
    ref var exportSpecifier = ref ᏑexportSpecifier.DerefOrNull();

    debug.Assert(exportSpecifier.PropertyName == referenceLocation.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode() || exportSpecifier.Name() == referenceLocation.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), "referenceLocation is not export specifier name or property name");
    var propertyName = exportSpecifier.PropertyName;
    if (propertyName != nil) {
        return propertyName == referenceLocation.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    } else {
        return exportSpecifier.Parent.Parent.ModuleSpecifier() == nil;
    }
}

internal static slice<ж<ast.Node>> getPossibleSymbolReferenceNodes(ж<ast.SourceFile> sourceFile, @string symbolName, ж<ast.Node> container) {
    return core.MapNonNil(getPossibleSymbolReferencePositions(sourceFile, symbolName, container), ж<ast.Node> (nint pos) => {
        var referenceLocation = astnav.GetTouchingPropertyName(sourceFile, pos); if (referenceLocation != sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) {
            return referenceLocation;
        }
        return default;
    });
}

internal static slice<nint> getPossibleSymbolReferencePositions(ж<ast.SourceFile> ᏑsourceFile, @string symbolName, ж<ast.Node> Ꮡcontainer) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var container = ref Ꮡcontainer.DerefOrNull();

    var positions = slice<nint>([]);

    if (symbolName == ""u8) {
        return positions;
    }

    @string text = sourceFile.Text();
    nint sourceLength = len(text);
    nint symbolNameLength = len(symbolName);

    if (Ꮡcontainer == nil) {
        Ꮡcontainer = ᏑsourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(); container = ref Ꮡcontainer.DerefOrNull();
    }

    nint position = strings.Index(text.slice(container.Pos()), symbolName);
    nint endPos = container.End();
    while (position >= 0 && position < endPos) {
        nint endPosition = position + symbolNameLength;

        if ((position == 0 || !scanner.IsIdentifierPart((rune)text[position - 1])) && (endPosition == sourceLength || !scanner.IsIdentifierPart((rune)text[endPosition]))) {
            positions = append(positions, position);
        }
        nint startIndex = position + symbolNameLength + 1;
        if (startIndex > len(text)) {
            break;
        }
        nint foundIndex = strings.Index(text.slice(startIndex), symbolName); if (foundIndex != -1) {
            position = startIndex + foundIndex;
        } else {
            break;
        }
    }

    return positions;
}

} partial class checkerᴛChecker {

public static ж<Type> GetTypeArgumentConstraint(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    if (!ast.IsTypeNode(node)) {
        return default;
    }
    return Ꮡc.getTypeArgumentConstraint(node);
}

internal static slice<ж<Signature>> getUninstantiatedSignatures(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindCallExpression || ᴛ == ast.KindDecorator: {
        return Ꮡc.getSignaturesOfType(Ꮡc.getTypeOfExpression(node.Expression()), SignatureKindCall);
    }
    case var ᴛ when ᴛ == ast.KindNewExpression: {
        return Ꮡc.getSignaturesOfType(Ꮡc.getTypeOfExpression(node.Expression()), SignatureKindConstruct);
    }
    case var ᴛ when ᴛ == ast.KindJsxSelfClosingElement || ᴛ == ast.KindJsxOpeningElement: {
        if (isJsxIntrinsicTagName(node.TagName())) {
            return default;
        }
        return Ꮡc.getSignaturesOfType(Ꮡc.getTypeOfExpression(node.TagName()), SignatureKindCall);
    }
    case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
        return Ꮡc.getSignaturesOfType(Ꮡc.getTypeOfExpression(node.AsTaggedTemplateExpression().Tag), SignatureKindCall);
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression || ᴛ == ast.KindJsxOpeningFragment: {
        return default;
    }}

    return default;
}

internal static ж<Type> getTypeParameterConstraintForPositionAcrossSignatures(this ж<Checker> Ꮡc, slice<ж<Signature>> signatures, nint position) {
    var relevantConstraintsᴷ = new stackAppend<ж<Type>>();

    slice<ж<Type>> relevantConstraints = default;
    foreach (var (_, signature) in range(signatures)) {
        if (position >= len(signature.typeParameters)) {
            continue;
        }
        var relevantTypeParameter = signature.typeParameters[position];
        var relevantConstraint = Ꮡc.getConstraintOfTypeParameter(relevantTypeParameter);
        if (relevantConstraint != nil) {
            relevantConstraints = append(relevantConstraintsᴷ, relevantConstraints, relevantConstraint);
        }
    }
    return Ꮡc.getUnionType(relevantConstraints);
}

internal static ж<Type> getTypeArgumentConstraint(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    nint typeArgumentPosition = -1;
    if (ast.HasTypeArguments(node.Parent)) {
        var typeArgs = node.Parent.TypeArguments();
        foreach (var (i, arg) in range(typeArgs)) {
            if (arg == Ꮡnode) {
                typeArgumentPosition = i;
                break;
            }
        }
    }

    if (typeArgumentPosition >= 0) {
        if (ast.IsCallLikeExpression(node.Parent)) {
            return Ꮡc.getTypeParameterConstraintForPositionAcrossSignatures(
                Ꮡc.getUninstantiatedSignatures(node.Parent),
                typeArgumentPosition);
        }

        if (ast.IsDecorator(node.Parent.Parent)) {
            return Ꮡc.getTypeParameterConstraintForPositionAcrossSignatures(
                Ꮡc.getUninstantiatedSignatures(node.Parent.Parent),
                typeArgumentPosition);
        }

        if (ast.IsExpressionWithTypeArguments(node.Parent) && ast.IsExpressionStatement(node.Parent.Parent)) {
            var uninstantiatedType = Ꮡc.checkExpression(node.Parent.Expression());

            var callConstraint = Ꮡc.getTypeParameterConstraintForPositionAcrossSignatures(
                Ꮡc.getSignaturesOfType(uninstantiatedType, SignatureKindCall),
                typeArgumentPosition);
            var constructConstraint = Ꮡc.getTypeParameterConstraintForPositionAcrossSignatures(
                Ꮡc.getSignaturesOfType(uninstantiatedType, SignatureKindConstruct),
                typeArgumentPosition);

            if ((TypeFlags)(constructConstraint.flags & TypeFlagsNever) != 0) {
                return callConstraint;
            }
            if ((TypeFlags)(callConstraint.flags & TypeFlagsNever) != 0) {
                return constructConstraint;
            }
            return Ꮡc.getIntersectionType([callConstraint, constructConstraint]);
        }

        if (ast.IsTypeReferenceType(node.Parent)) {
            var typeParameters = Ꮡc.getTypeParametersForTypeReferenceOrImport(node.Parent);
            if (len(typeParameters) == 0) {
                return default;
            }
            if (typeArgumentPosition >= len(typeParameters)) {
                return default;
            }
            var relevantTypeParameter = typeParameters[typeArgumentPosition];
            var constraint = Ꮡc.getConstraintOfTypeParameter(relevantTypeParameter);
            if (constraint != nil) {
                return Ꮡc.instantiateType(
                    constraint,
                    newTypeMapper(typeParameters, Ꮡc.getEffectiveTypeArguments(node.Parent, typeParameters)));
            }
        }
    }
    return default;
}

public static bool IsTypeInvalidDueToUnionDiscriminant(this ж<Checker> Ꮡc, ж<Type> contextualType, ж<ast.Node> Ꮡobj) {
    ref var obj = ref Ꮡobj.DerefOrNull();

    var properties = obj.Properties();
    return core.Some(properties, (ж<ast.Node> property) => {
        ж<Type> nameType = default;
        var propertyName = property.Name();
        if (propertyName != nil) {
            if (ast.IsJsxNamespacedName(propertyName)) {
                nameType = Ꮡc.getStringLiteralType(propertyName.Text());
            } else {
                nameType = Ꮡc.getLiteralTypeFromPropertyName(propertyName);
            }
        }
        @string name = default;
        if (nameType != nil && isTypeUsableAsPropertyName(ref (nameType).DerefOrNull())) {
            name = getPropertyNameFromType(nameType);
        }
        ж<Type> expected = default;
        if (name != ""u8) {
            expected = Ꮡc.getTypeOfPropertyOfType(contextualType, name);
        }
        return expected != nil && isLiteralType(expected) && !Ꮡc.isTypeAssignableTo(Ꮡc.getTypeOfNode(property), expected);
    });
}

public static slice<ж<ast.Symbol>> GetExportsAndPropertiesOfModule(this ж<Checker> Ꮡc, ж<ast.Symbol> moduleSymbol) {
    ref var c = ref Ꮡc.DerefOrNull();

    var exports = Ꮡc.getExportsOfModuleAsArray(moduleSymbol);
    var exportEquals = Ꮡc.resolveExternalModuleSymbol(moduleSymbol, false);
    if (exportEquals != moduleSymbol) {
        var t = Ꮡc.getTypeOfSymbol(exportEquals);
        if (c.shouldTreatPropertiesOfExternalModuleAsExports(t)) {
            exports = appendꓸꓸꓸ(exports, Ꮡc.getPropertiesOfType(t));
        }
    }
    return exports;
}

internal static slice<ж<ast.Symbol>> getExportsOfModuleAsArray(this ж<Checker> Ꮡc, ж<ast.Symbol> moduleSymbol) {
    return symbolsToArray(Ꮡc.getExportsOfModule(moduleSymbol));
}

public static slice<ж<ast.Symbol>> GetJsxIntrinsicTagNamesAt(this ж<Checker> Ꮡc, ж<ast.Node> location) {
    var intrinsics = Ꮡc.getJsxType(JsxNames.IntrinsicElements, location);
    if (intrinsics == nil) {
        return default;
    }
    return Ꮡc.GetPropertiesOfType(intrinsics);
}

public static ж<Type> GetContextualTypeForJsxAttribute(this ж<Checker> Ꮡc, ж<astꓸJsxAttributeLike> attribute) {
    return Ꮡc.getContextualTypeForJsxAttribute(attribute, ContextFlagsNone);
}

public static any GetConstantValue(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindEnumMember) {
        return Ꮡc.getEnumMemberValue(Ꮡnode).Value;
    }

    if (c.symbolNodeLinks.Get(Ꮡnode).resolvedSymbol == nil) {
        Ꮡc.checkExpressionCached(Ꮡnode);
    }
    var symbol = c.symbolNodeLinks.Get(Ꮡnode).resolvedSymbol;
    if (symbol == nil && ast.IsEntityNameExpression(Ꮡnode)) {
        symbol = Ꮡc.resolveEntityName(
            Ꮡnode,
            ast.SymbolFlagsValue,
            true,
            false,
            nil);
    }
    if (symbol != nil && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsEnumMember) != 0) {
        var member = symbol.ValueDeclaration;
        if (ast.IsEnumConst(member.Parent)) {
            return Ꮡc.getEnumMemberValue(member).Value;
        }
    }

    return default;
}

internal static (ж<Signature>, slice<ж<Signature>>) getResolvedSignatureWorker(this ж<Checker> Ꮡc, ж<ast.Node> node, CheckMode checkMode, nint argumentCountʗp) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var argumentCount = ref heap(argumentCountʗp, out var ᏑargumentCount);

    var parsedNode = printer.NewEmitContext().ParseNode(node);
    c.apparentArgumentCount = ᏑargumentCount;
    var candidatesOutArray = Ꮡ(slice<ж<Signature>>([]));
    ж<Signature> res = default;
    if (parsedNode != nil) {
        res = Ꮡc.getResolvedSignature(parsedNode, candidatesOutArray, checkMode);
    }
    c.apparentArgumentCount = default;
    return (res, candidatesOutArray.ValueSlot);
}

public static slice<ж<Signature>> GetCandidateSignaturesForStringLiteralCompletions(this ж<Checker> Ꮡc, ж<astꓸCallLikeExpression> call, ж<ast.Node> editingArgument) {
    var candidates = runWithInferenceBlockedFromSourceNode(Ꮡc, editingArgument, slice<ж<Signature>> () => {
        var (_, blockedInferenceCandidates) = Ꮡc.getResolvedSignatureWorker(call, CheckModeNormal, 0);
        return blockedInferenceCandidates;
    });
    var candidatesSet = collections.NewSetFromItems(candidates.ꓸꓸꓸ);

    var otherCandidates = runWithoutResolvedSignatureCaching(Ꮡc, editingArgument, slice<ж<Signature>> () => {
        var (_, inferenceCandidates) = Ꮡc.getResolvedSignatureWorker(call, CheckModeNormal, 0);
        return inferenceCandidates;
    });

    foreach (var (_, candidate) in range(otherCandidates)) {
        if (candidatesSet.Has(candidate)) {
            continue;
        }
        candidates = append(candidates, candidate);
    }

    return candidates;
}

public static ж<Type> GetTypeAtPosition(this ж<Checker> Ꮡc, ж<Signature> s, nint pos) {
    return Ꮡc.getTypeAtPosition(s, pos);
}

public static ж<Type> GetTypeParameterAtPosition(this ж<Checker> Ꮡc, ж<Signature> s, nint pos) {
    var t = Ꮡc.getTypeAtPosition(s, pos);
    if (t.IsIndex() && isThisTypeParameter(t.AsIndexType().target)) {
        var constraint = Ꮡc.getBaseConstraintOfType(t.AsIndexType().target);
        if (constraint != nil) {
            return Ꮡc.getIndexType(constraint);
        }
    }
    return t;
}

public static ж<Type> GetContextualTypeForArrayLiteralAtPosition(this ж<Checker> Ꮡc, ж<Type> contextualArrayType, ж<ast.Node> ᏑarrayLiteral, nint position) {
    ref var arrayLiteral = ref ᏑarrayLiteral.DerefOrNull();

    if (contextualArrayType == nil) {
        return default;
    }
    nint firstSpreadIndex = -1;
    nint lastSpreadIndex = -1;
    nint elementIndex = 0;
    var elements = arrayLiteral.Elements();
    foreach (var (i, elem) in range(elements)) {
        if (elem.Pos() < position) {
            elementIndex++;
        }
        if (ast.IsSpreadElement(elem)) {
            if (firstSpreadIndex == -1) {
                firstSpreadIndex = i;
            }
            lastSpreadIndex = i;
        }
    }
    return Ꮡc.getContextualTypeForElementExpression(
        contextualArrayType,
        elementIndex,
        -1,
        firstSpreadIndex,
        lastSpreadIndex);
}

} partial class checker_package {

[GoLazy] partial class lazyᴛknownGenericTypeNames { internal static map<@string, EmptyStruct> knownGenericTypeNames = new map<@string, EmptyStruct>{
    ["Array"] = new(),
    ["ArrayLike"] = new(),
    ["ReadonlyArray"] = new(),
    ["Promise"] = new(),
    ["PromiseLike"] = new(),
    ["Iterable"] = new(),
    ["IterableIterator"] = new(),
    ["AsyncIterable"] = new(),
    ["Set"] = new(),
    ["WeakSet"] = new(),
    ["ReadonlySet"] = new(),
    ["Map"] = new(),
    ["WeakMap"] = new(),
    ["ReadonlyMap"] = new(),
    ["Partial"] = new(),
    ["Required"] = new(),
    ["Readonly"] = new(),
    ["Pick"] = new(),
    ["Omit"] = new(),
    ["NonNullable"] = new()
}; }

internal static bool isKnownGenericTypeName(@string name) {
    var (_, exists) = knownGenericTypeNames[name, ꟷ];
    return exists;
}

} partial class checkerᴛChecker {

public static ж<Type> GetFirstTypeArgumentFromKnownType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if ((ObjectFlags)(t.objectFlags & ObjectFlagsReference) != 0 && t.symbol != nil && isKnownGenericTypeName(t.symbol.Name)) {
        var symbol = c.getGlobalSymbol(t.symbol.Name, ast.SymbolFlagsType, nil);
        if (symbol != nil && symbol == t.Target().symbol) {
            return core.FirstOrNil(Ꮡc.getTypeArguments(Ꮡt));
        }
    }
    if (t.alias != nil && isKnownGenericTypeName(t.alias.symbol.Name)) {
        var symbol = c.getGlobalSymbol(t.alias.symbol.Name, ast.SymbolFlagsType, nil);
        if (symbol != nil && symbol == t.alias.symbol) {
            return core.FirstOrNil(t.alias.typeArguments);
        }
    }
    return default;
}

public static slice<ж<ast.Symbol>> GetPropertySymbolsFromContextualType(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> ᏑcontextualType, bool unionSymbolOk) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var contextualType = ref ᏑcontextualType.DerefOrNull();

    @string name = ast.GetTextOfPropertyName(node.Name());
    if (name == ""u8) {
        return default;
    }
    if ((TypeFlags)(contextualType.flags & TypeFlagsUnion) == 0) {
        var symbol = Ꮡc.getPropertyOfType(ᏑcontextualType, name); if (symbol != nil) {
            return [symbol];
        }
        return default;
    }
    var filteredTypes = contextualType.Types();
    if (ast.IsObjectLiteralExpression(node.Parent) || ast.IsJsxAttributes(node.Parent)) {
        filteredTypes = core.Filter(filteredTypes, (ж<Type> t) => !Ꮡc.IsTypeInvalidDueToUnionDiscriminant(t, Ꮡnode.Value.Parent));
    }
    var discriminatedPropertySymbols = core.MapNonNil(filteredTypes, ж<ast.Symbol> (ж<Type> t) => Ꮡc.getPropertyOfType(t, name));
    if (unionSymbolOk && (len(discriminatedPropertySymbols) == 0 || len(discriminatedPropertySymbols) == len(contextualType.Types()))) {
        var symbol = Ꮡc.getPropertyOfType(ᏑcontextualType, name); if (symbol != nil) {
            return [symbol];
        }
    }
    if (len(filteredTypes) == 0 && len(discriminatedPropertySymbols) == 0) {
        return core.MapNonNil(contextualType.Types(), ж<ast.Symbol> (ж<Type> t) => Ꮡc.getPropertyOfType(t, name));
    }
    return core.Deduplicate(discriminatedPropertySymbols);
}

public static ж<ast.Symbol> GetPropertySymbolOfDestructuringAssignment(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡlocation) {
    ref var location = ref Ꮡlocation.DerefOrNull();

    if (ast.IsArrayLiteralOrObjectLiteralDestructuringPattern(location.Parent.Parent)) {
        var typeOfObjectLiteral = Ꮡc.getTypeOfAssignmentPattern(location.Parent.Parent); if (typeOfObjectLiteral != nil) {
            return Ꮡc.getPropertyOfType(typeOfObjectLiteral, location.Text());
        }
    }
    return default;
}

internal static ж<Type> getTypeOfAssignmentPattern(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡexpr) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var expr = ref Ꮡexpr.DerefOrNull();

    if (ast.IsForOfStatement(expr.Parent)) {
        var iteratedType = Ꮡc.checkRightHandSideOfForOf(expr.Parent);
        return Ꮡc.checkDestructuringAssignment(Ꮡexpr, core.OrElse(iteratedType, c.errorType), CheckModeNormal, false);
    }
    if (ast.IsBinaryExpression(expr.Parent)) {
        var iteratedType = Ꮡc.getTypeOfExpression(expr.Parent.AsBinaryExpression().Right);
        return Ꮡc.checkDestructuringAssignment(Ꮡexpr, core.OrElse(iteratedType, c.errorType), CheckModeNormal, false);
    }
    if (ast.IsPropertyAssignment(expr.Parent)) {
        var nodeΔ1 = expr.Parent.Parent;
        var typeOfParentObjectLiteral = core.OrElse(Ꮡc.getTypeOfAssignmentPattern(nodeΔ1), c.errorType);
        nint propertyIndex = slices.Index(nodeΔ1.Properties(), expr.Parent);
        return Ꮡc.checkObjectLiteralDestructuringPropertyAssignment(nodeΔ1, typeOfParentObjectLiteral, propertyIndex, nil, false);
    }
    var node = expr.Parent;
    var typeOfArrayLiteral = core.OrElse(Ꮡc.getTypeOfAssignmentPattern(node), c.errorType);
    var elementType = core.OrElse(Ꮡc.checkIteratedTypeOrElementType(IterationUseDestructuring, typeOfArrayLiteral, c.undefinedType, expr.Parent), c.errorType);
    return Ꮡc.checkArrayLiteralDestructuringElementAssignment(node, typeOfArrayLiteral, slices.Index(node.Elements(), Ꮡexpr), elementType, CheckModeNormal);
}

public static ж<Signature> GetSignatureFromDeclaration(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    return Ꮡc.getSignatureFromDeclaration(node);
}

public static bool IsLibSymbolForHoverVerbosity(this ref Checker c, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡsymbol == nil) {
        return false;
    }
    foreach (var (_, decl) in range(symbol.Declarations)) {
        var sf = ast.GetSourceFileOfNode(decl);
        if (sf != nil && c.program.IsSourceFileDefaultLibrary(sf.Path())) {
            return true;
        }
    }
    return false;
}

public static bool IsLibTypeForHoverVerbosity(this ref Checker c, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    ж<ast.Symbol> symbol = default;
    if ((ObjectFlags)(t.objectFlags & ObjectFlagsReference) != 0) {
        symbol = t.Target().Symbol();
    } else {
        symbol = t.Symbol();
    }
    if (c.IsLibSymbolForHoverVerbosity(symbol)) {
        return true;
    }
    return isTupleType(Ꮡt);
}

} // end checkerᴛChecker
