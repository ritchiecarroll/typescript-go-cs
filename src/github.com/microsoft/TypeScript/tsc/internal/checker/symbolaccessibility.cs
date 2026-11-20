namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

} partial class checkerᴛChecker {

public static bool IsTypeSymbolAccessible(this ж<Checker> Ꮡc, ж<ast.Symbol> typeSymbol, ж<ast.Node> enclosingDeclaration) {
    var access = Ꮡc.isSymbolAccessibleWorker(typeSymbol, enclosingDeclaration, ast.SymbolFlagsType, false, true);
    return access.Accessibility == printer.SymbolAccessibilityAccessible;
}

public static bool IsValueSymbolAccessible(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration) {
    var access = Ꮡc.isSymbolAccessibleWorker(symbol, enclosingDeclaration, ast.SymbolFlagsValue, false, true);
    return access.Accessibility == printer.SymbolAccessibilityAccessible;
}

public static bool IsSymbolAccessibleByFlags(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags flags) {
    var access = Ꮡc.isSymbolAccessibleWorker(symbol, enclosingDeclaration, flags, false, false);
    return access.Accessibility == printer.SymbolAccessibilityAccessible;
}

public static ж<printer.SymbolAccessibilityResult> IsAnySymbolAccessible(this ж<Checker> Ꮡc, slice<ж<ast.Symbol>> symbols, ж<ast.Node> enclosingDeclaration, ж<ast.Symbol> initialSymbol, ast.SymbolFlags meaning, bool shouldComputeAliasesToMakeVisible, bool allowModules) {
    if (len(symbols) == 0) {
        return default;
    }

    ж<ast.Symbol> hadAccessibleChain = default;
    var earlyModuleBail = false;
    foreach (var (_, symbol) in range(symbols)) {
        var accessibleSymbolChain = Ꮡc.getAccessibleSymbolChain(symbol, enclosingDeclaration, meaning, false);
        if (len(accessibleSymbolChain) > 0) {
            hadAccessibleChain = symbol;
            var hasAccessibleDeclarations = Ꮡc.GetEmitResolver().hasVisibleDeclarations(accessibleSymbolChain[0], shouldComputeAliasesToMakeVisible);
            if (hasAccessibleDeclarations != nil) {
                return hasAccessibleDeclarations;
            }
        }
        if (allowModules) {
            if (core.Some<ж<ast.Node>>(symbol.Declarations, hasNonGlobalAugmentationExternalModuleSymbol)) {
                if (shouldComputeAliasesToMakeVisible) {
                    earlyModuleBail = true;
                    continue;
                }
                return Ꮡ(new printer.SymbolAccessibilityResult(
                    Accessibility: printer.SymbolAccessibilityAccessible
                ));
            }
        }

        var containers = Ꮡc.getContainersOfSymbol(symbol, enclosingDeclaration, meaning);
        var nextMeaning = meaning;
        if (initialSymbol == symbol) {
            nextMeaning = getQualifiedLeftMeaning(meaning);
        }
        var parentResult = Ꮡc.IsAnySymbolAccessible(containers, enclosingDeclaration, initialSymbol, nextMeaning, shouldComputeAliasesToMakeVisible, allowModules);
        if (parentResult != nil) {
            return parentResult;
        }
    }

    if (earlyModuleBail) {
        return Ꮡ(new printer.SymbolAccessibilityResult(
            Accessibility: printer.SymbolAccessibilityAccessible
        ));
    }

    if (hadAccessibleChain != nil) {
        ref var moduleName = ref heap(new @string(), out var ᏑmoduleName);
        if (hadAccessibleChain != initialSymbol) {
            moduleName = Ꮡc.symbolToStringEx(hadAccessibleChain, enclosingDeclaration, ast.SymbolFlagsNamespace, SymbolFormatFlagsAllowAnyNodeKind);
        }
        return Ꮡ(new printer.SymbolAccessibilityResult(
            Accessibility: printer.SymbolAccessibilityNotAccessible,
            ErrorSymbolName: Ꮡc.symbolToStringEx(initialSymbol, enclosingDeclaration, meaning, SymbolFormatFlagsAllowAnyNodeKind),
            ErrorModuleName: moduleName
        ));
    }
    return default;
}

} partial class checker_package {

internal static bool hasNonGlobalAugmentationExternalModuleSymbol(ж<ast.Node> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    return ast.IsModuleWithStringLiteralName(Ꮡdeclaration) || (declaration.Kind == ast.KindSourceFile && ast.IsExternalOrCommonJSModule(declaration.AsSourceFile()));
}

internal static ast.SymbolFlags getQualifiedLeftMeaning(ast.SymbolFlags rightMeaning) {
    if (rightMeaning == ast.SymbolFlagsValue) {
        return ast.SymbolFlagsValue;
    }
    return ast.SymbolFlagsNamespace;
}

} partial class checkerᴛChecker {

internal static slice<ж<ast.Symbol>> getWithAlternativeContainers(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡcontainer, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var container = ref Ꮡcontainer.DerefOrNull();

    var additionalContainers = core.MapNonNil(container.Declarations, ж<ast.Symbol> (ж<ast.Node> d) => Ꮡc.getFileSymbolIfFileSymbolExportEqualsContainer(d, Ꮡcontainer));
    slice<ж<ast.Symbol>> reexportContainers = default;
    if (enclosingDeclaration != nil) {
        reexportContainers = Ꮡc.getAlternativeContainingModules(symbol, enclosingDeclaration);
    }
    var objectLiteralContainer = Ꮡc.getVariableDeclarationOfObjectLiteral(Ꮡcontainer, meaning);
    var leftMeaning = getQualifiedLeftMeaning(meaning);
    if (enclosingDeclaration != nil && (ast.SymbolFlags)(container.Flags & leftMeaning) != 0 && len(Ꮡc.getAccessibleSymbolChain(Ꮡcontainer, enclosingDeclaration, ast.SymbolFlagsNamespace, false)) > 0) {
        var resΔ1 = appendꓸꓸꓸ(appendꓸꓸꓸ(slice<ж<ast.Symbol>>([Ꮡcontainer]), additionalContainers), reexportContainers);
        if (objectLiteralContainer != nil) {
            resΔ1 = append(resΔ1, objectLiteralContainer);
        }
        return resΔ1;
    }
    ref var variableMatches = ref heap<slice<ж<ast.Symbol>>>(out var ᏑvariableMatches);
    if ((meaning == ast.SymbolFlagsValue && (ast.SymbolFlags)(container.Flags & leftMeaning) == 0) && (ast.SymbolFlags)(container.Flags & ast.SymbolFlagsType) != 0 && (TypeFlags)(Ꮡc.getDeclaredTypeOfSymbol(Ꮡcontainer).flags & TypeFlagsObject) != 0) {
        Ꮡc.someSymbolTableInScope(enclosingDeclaration, (ast.SymbolTable t, symbolTableID _Δp1, bool _Δp2, bool _Δp3, ж<ast.Node> _Δp4) => {
            var found = false;
            foreach (var (_, s) in t) {
                if ((ast.SymbolFlags)(s.Flags & leftMeaning) != 0 && Ꮡc.getTypeOfSymbol(s) == Ꮡc.getDeclaredTypeOfSymbol(Ꮡcontainer)) {
                    ᏑvariableMatches.ValueSlot = append(ᏑvariableMatches.ValueSlot, s);
                    found = true;
                }
            }
            return found;
        });
        c.sortSymbols(variableMatches);
    }

    slice<ж<ast.Symbol>> res = default;
    res = appendꓸꓸꓸ(res, variableMatches);
    res = appendꓸꓸꓸ(res, additionalContainers);
    res = append(res, Ꮡcontainer);
    if (objectLiteralContainer != nil) {
        res = append(res, objectLiteralContainer);
    }
    res = appendꓸꓸꓸ(res, reexportContainers);
    return res;
}

internal static slice<ж<ast.Symbol>> getAlternativeContainingModules(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (enclosingDeclaration == nil) {
        return default;
    }
    var containingFile = ast.GetSourceFileOfNode(enclosingDeclaration);
    var id = ast.GetNodeId(containingFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var links = c.symbolContainerLinks.Get(symbol);
    if (links.extendedContainersByFile == nil) {
        links.extendedContainersByFile = new map<ast.NodeId, slice<ж<ast.Symbol>>>();
    }
    var (existing, ok) = links.extendedContainersByFile[id, ꟷ];
    if (ok && existing != nil) {
        return existing;
    }
    ref var results = ref heap<slice<ж<ast.Symbol>>>(out var Ꮡresults);
    if (len(containingFile.Imports()) > 0) {
        foreach (var (_, importRef) in range(containingFile.Imports())) {
            if (ast.NodeIsSynthesized(importRef)) {
                continue;
            }
            var resolvedModule = Ꮡc.resolveExternalModuleName(enclosingDeclaration, importRef, true, Ꮡc.getImportAttributesTypeForModuleSpecifier(importRef));
            if (resolvedModule == nil) {
                continue;
            }
            var @ref = Ꮡc.getAliasForSymbolInContainer(resolvedModule, symbol);
            if (@ref == nil) {
                continue;
            }
            results = append(results, resolvedModule);
        }
        if (len(results) > 0) {
            links.extendedContainersByFile[id] = results;
            return results;
        }
    }

    if (links.extendedContainers != nil) {
        return links.extendedContainers.ValueSlot;
    }
    var otherFiles = c.program.SourceFiles();
    foreach (var (_, @file) in range(otherFiles)) {
        if (!ast.IsExternalModule(@file)) {
            continue;
        }
        var sym = Ꮡc.getSymbolOfDeclaration(@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var @ref = Ꮡc.getAliasForSymbolInContainer(sym, symbol);
        if (@ref == nil) {
            continue;
        }
        results = append(results, sym);
    }
    links.extendedContainers = Ꮡresults;
    return results;
}

internal static ж<ast.Symbol> getVariableDeclarationOfObjectLiteral(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, ast.SymbolFlags meaning) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if ((ast.SymbolFlags)(meaning & ast.SymbolFlagsValue) == 0) {
        return default;
    }
    if (len(symbol.Declarations) == 0) {
        return default;
    }
    var firstDecl = symbol.Declarations[0];
    if (firstDecl.Parent == nil) {
        return default;
    }
    if (!ast.IsVariableDeclaration(firstDecl.Parent)) {
        return default;
    }
    if (ast.IsObjectLiteralExpression(firstDecl) && firstDecl == firstDecl.Parent.Initializer() || ast.IsTypeLiteralNode(firstDecl) && firstDecl == firstDecl.Parent.Type()) {
        return Ꮡc.getSymbolOfDeclaration(firstDecl.Parent);
    }
    return default;
}

} partial class checker_package {

internal static bool hasExternalModuleSymbol(ж<ast.Node> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    return ast.IsAmbientModule(Ꮡdeclaration) || (declaration.Kind == ast.KindSourceFile && ast.IsExternalOrCommonJSModule(declaration.AsSourceFile()));
}

} partial class checkerᴛChecker {

internal static ж<ast.Symbol> getExternalModuleContainer(this ж<Checker> Ꮡc, ж<ast.Node> declaration) {
    var node = ast.FindAncestor(declaration, hasExternalModuleSymbol);
    if (node == nil) {
        return default;
    }
    return Ꮡc.getSymbolOfDeclaration(node);
}

internal static ж<ast.Symbol> getFileSymbolIfFileSymbolExportEqualsContainer(this ж<Checker> Ꮡc, ж<ast.Node> d, ж<ast.Symbol> container) {
    var fileSymbol = Ꮡc.getExternalModuleContainer(d);
    if (fileSymbol == nil || fileSymbol.Exports == nil) {
        return default;
    }
    var (exported, ok) = fileSymbol.Exports[ast.InternalSymbolNameExportEquals, ꟷ];
    if (!ok || exported == nil) {
        return default;
    }
    if (Ꮡc.getSymbolIfSameReference(exported, container) != nil) {
        return fileSymbol;
    }
    return default;
}

internal static slice<ж<ast.Symbol>> getContainersOfSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    var candidatesᴷ = new stackAppend<ж<ast.Symbol>>();

    var container = Ꮡc.getParentOfSymbol(Ꮡsymbol);
    if (container != nil && ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTypeParameter) == 0)) {
        return Ꮡc.getWithAlternativeContainers(container, Ꮡsymbol, enclosingDeclaration, meaning);
    }
    slice<ж<ast.Symbol>> candidates = default;
    foreach (var (_, d) in range(symbol.Declarations)) {
        if (!ast.IsAmbientModule(d) && d.Parent != nil) {
            if (hasNonGlobalAugmentationExternalModuleSymbol(d.Parent)) {
                var sym = Ꮡc.getSymbolOfDeclaration(d.Parent);
                if (sym != nil && !slices.Contains(candidates, sym)) {
                    candidates = append(candidatesᴷ, candidates, sym);
                }
                continue;
            }
            if (ast.IsModuleBlock(d.Parent) && d.Parent.Parent != nil && Ꮡc.resolveExternalModuleSymbol(Ꮡc.getSymbolOfDeclaration(d.Parent.Parent), false) == Ꮡsymbol) {
                var sym = Ꮡc.getSymbolOfDeclaration(d.Parent.Parent);
                if (sym != nil && !slices.Contains(candidates, sym)) {
                    candidates = append(candidates, sym);
                }
                continue;
            }
        }
        if (ast.IsClassExpression(d) && ast.IsBinaryExpression(d.Parent) && d.Parent.AsBinaryExpression().OperatorToken.Kind == ast.KindEqualsToken && ast.IsAccessExpression(d.Parent.AsBinaryExpression().Left) && ast.IsEntityNameExpression(d.Parent.AsBinaryExpression().Left.Expression())) {
            if (ast.IsModuleExportsAccessExpression(d.Parent.AsBinaryExpression().Left) || ast.IsExportsIdentifier(d.Parent.AsBinaryExpression().Left.Expression())) {
                var symΔ1 = Ꮡc.getSymbolOfDeclaration(ast.GetSourceFileOfNode(d).of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                if (symΔ1 != nil && !slices.Contains(candidates, symΔ1)) {
                    candidates = append(candidates, symΔ1);
                }
                continue;
            }
            Ꮡc.checkExpressionCached(d.Parent.AsBinaryExpression().Left.Expression());
            var sym = c.symbolNodeLinks.Get(d.Parent.AsBinaryExpression().Left.Expression()).resolvedSymbol;
            if (sym != nil && !slices.Contains(candidates, sym)) {
                candidates = append(candidates, sym);
            }
            continue;
        }
    }
    if (len(candidates) == 0) {
        return default;
    }

    slice<ж<ast.Symbol>> bestContainers = default;
    slice<ж<ast.Symbol>> alternativeContainers = default;
    foreach (var (_, containerΔ1) in range(candidates)) {
        if (Ꮡc.getAliasForSymbolInContainer(containerΔ1, Ꮡsymbol) == nil) {
            continue;
        }
        var allAlts = Ꮡc.getWithAlternativeContainers(containerΔ1, Ꮡsymbol, enclosingDeclaration, meaning);
        if (len(allAlts) == 0) {
            continue;
        }
        bestContainers = append(bestContainers, allAlts[0]);
        alternativeContainers = appendꓸꓸꓸ(alternativeContainers, allAlts[1..]);
    }
    return appendꓸꓸꓸ(bestContainers, alternativeContainers);
}

internal static ж<ast.Symbol> getAliasForSymbolInContainer(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡcontainer, ж<ast.Symbol> Ꮡsymbol) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var container = ref Ꮡcontainer.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    var candidatesᴷ = new stackAppend<ж<ast.Symbol>>();

    if (Ꮡcontainer == Ꮡc.getParentOfSymbol(Ꮡsymbol)) {
        return Ꮡsymbol;
    }
    if (container.Exports != nil) {
        var (exportEquals, okΔ1) = container.Exports[ast.InternalSymbolNameExportEquals, ꟷ];
        if (okΔ1 && exportEquals != nil && Ꮡc.getSymbolIfSameReference(exportEquals, Ꮡsymbol) != nil) {
            return Ꮡcontainer;
        }
    }
    var exports = Ꮡc.getExportsOfSymbol(Ꮡcontainer);
    var (quick, ok) = exports[symbol.Name, ꟷ];
    if (ok && quick != nil && Ꮡc.getSymbolIfSameReference(quick, Ꮡsymbol) != nil) {
        return quick;
    }
    slice<ж<ast.Symbol>> candidates = default;
    foreach (var (_, exported) in exports) {
        if (Ꮡc.getSymbolIfSameReference(exported, Ꮡsymbol) != nil) {
            candidates = append(candidatesᴷ, candidates, exported);
        }
    }
    if (len(candidates) > 0) {
        c.sortSymbols(candidates);
        return candidates[0];
    }
    return default;
}

internal static slice<ж<ast.Symbol>> getAccessibleSymbolChain(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, bool useOnlyExternalAliasing) {
    return Ꮡc.getAccessibleSymbolChainEx(new accessibleSymbolChainContext(symbol, enclosingDeclaration, meaning, useOnlyExternalAliasing, new map<ast.SymbolId, map<symbolTableID, EmptyStruct>>()));
}

public static slice<ж<ast.Symbol>> GetAccessibleSymbolChain(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, bool useOnlyExternalAliasing) {
    return Ꮡc.getAccessibleSymbolChain(symbol, enclosingDeclaration, meaning, useOnlyExternalAliasing);
}

} partial class checker_package {

partial struct accessibleSymbolChainContext {
    internal ж<ast.Symbol> symbol;
    internal ж<ast.Node> enclosingDeclaration;
    internal ast.SymbolFlags meaning;
    internal bool useOnlyExternalAliasing;
    internal map<ast.SymbolId, map<symbolTableID, EmptyStruct>> visitedSymbolTablesMap;
}

partial struct symbolTableID /*num:uint64*/;

internal static UntypedInt stKindShift => 61;

internal static symbolTableID stKindLocals => /* iota << stKindShift */ 0;
internal static symbolTableID stKindExports => unchecked((symbolTableID)2305843009213693952);
internal static symbolTableID stKindMembers => unchecked((symbolTableID)4611686018427387904);
internal static symbolTableID stKindGlobals => unchecked((symbolTableID)6917529027641081856);
internal static symbolTableID stKindResolvedExports => unchecked((symbolTableID)9223372036854775808);
internal static symbolTableID stKindMask => /* (iota - 1) << stKindShift */ unchecked((symbolTableID)9223372036854775808);

internal static symbolTableID symbolTableIDFromLocals(ж<ast.Node> node) {
    return (symbolTableID)(stKindLocals | ((symbolTableID)(uint64)ast.GetNodeId(node)));
}

internal static symbolTableID symbolTableIDFromExports(ж<ast.Symbol> sym) {
    return (symbolTableID)(stKindExports | ((symbolTableID)(uint64)ast.GetSymbolId(sym)));
}

internal static symbolTableID symbolTableIDFromResolvedExports(ж<ast.Symbol> sym) {
    return (symbolTableID)(stKindResolvedExports | ((symbolTableID)(uint64)ast.GetSymbolId(sym)));
}

internal static symbolTableID symbolTableIDFromMembers(ж<ast.Symbol> sym) {
    return (symbolTableID)(stKindMembers | ((symbolTableID)(uint64)ast.GetSymbolId(sym)));
}

internal static symbolTableID symbolTableIDFromGlobals() {
    return stKindGlobals;
}

} partial class checkerᴛChecker {

internal static slice<ж<ast.Symbol>> getAccessibleSymbolChainEx(this ж<Checker> Ꮡc, accessibleSymbolChainContext ctx) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (ctx.symbol == nil) {
        return default;
    }
    if (isPropertyOrMethodDeclarationSymbol(ref (ctx.symbol).DerefOrNull())) {
        return default;
    }
    ref var firstRelevantLocation = ref heap<ж<ast.Node>>(out var ᏑfirstRelevantLocation);
    Ꮡc.someSymbolTableInScope(ctx.enclosingDeclaration, (ast.SymbolTable _Δp0, symbolTableID _Δp1, bool _Δp2, bool _Δp3, ж<ast.Node> node) => {
        ᏑfirstRelevantLocation.ValueSlot = node;
        return true;
    });
    var links = c.symbolContainerLinks.Get(ctx.symbol);
    var linkKey = new accessibleChainCacheKey(ctx.useOnlyExternalAliasing, firstRelevantLocation, ctx.meaning);
    if (links.accessibleChainCache == nil) {
        links.accessibleChainCache = new map<accessibleChainCacheKey, slice<ж<ast.Symbol>>>();
    }
    var (existing, ok) = links.accessibleChainCache[linkKey, ꟷ];
    if (ok) {
        return existing;
    }

    ref var result = ref heap<slice<ж<ast.Symbol>>>(out var Ꮡresult);

    var ctxʗ1 = ctx;
    Ꮡc.someSymbolTableInScope(ctx.enclosingDeclaration, (ast.SymbolTable t, symbolTableID tableId, bool ignoreQualification, bool isLocalNameLookup, ж<ast.Node> _) => {
        var res = Ꮡc.getAccessibleSymbolChainFromSymbolTable(ctxʗ1, t, tableId, ignoreQualification, isLocalNameLookup);
        if (len(res) > 0) {
            Ꮡresult.ValueSlot = res;
            return true;
        }
        return false;
    });
    links.accessibleChainCache[linkKey] = result;
    return result;
}

internal static slice<ж<ast.Symbol>> getAccessibleSymbolChainFromSymbolTable(this ж<Checker> Ꮡc, accessibleSymbolChainContext ctx, ast.SymbolTable t, symbolTableID tableId, bool ignoreQualification, bool isLocalNameLookup) {
    var symId = ast.GetSymbolId(ctx.symbol);
    var (visitedSymbolTables, ok) = ctx.visitedSymbolTablesMap[symId, ꟷ];
    if (!ok) {
        visitedSymbolTables = new map<symbolTableID, EmptyStruct>();
        ctx.visitedSymbolTablesMap[symId] = visitedSymbolTables;
    }

    var (_, present) = visitedSymbolTables[tableId, ꟷ];
    if (present) {
        return default;
    }
    visitedSymbolTables[tableId] = new EmptyStruct();

    var res = Ꮡc.trySymbolTable(ctx, t, tableId, ignoreQualification, isLocalNameLookup);

    delete(visitedSymbolTables, tableId);
    return res;
}

internal static slice<ж<ast.Symbol>> getSymbolTableAliases(this ref Checker c, ast.SymbolTable symbols, symbolTableID tableId) {
    var kind = (symbolTableID)(tableId & stKindMask);
    if (kind == stKindMembers) {
        return default;
    }
    if (kind == stKindGlobals || kind == stKindExports || kind == stKindResolvedExports) {
        if (c.symbolTableAliasCache != nil) {
            var (aliasesΔ1, ok) = c.symbolTableAliasCache[tableId, ꟷ]; if (ok) {
                return aliasesΔ1;
            }
        }
    }
    slice<ж<ast.Symbol>> aliases = default;
    foreach (var (_, sym) in symbols) {
        if ((ast.SymbolFlags)(sym.Flags & ast.SymbolFlagsAlias) != 0) {
            aliases = append(aliases, sym);
        }
    }
    if (kind == stKindGlobals || kind == stKindExports || kind == stKindResolvedExports) {
        if (c.symbolTableAliasCache == nil) {
            c.symbolTableAliasCache = new map<symbolTableID, slice<ж<ast.Symbol>>>();
        }
        c.symbolTableAliasCache[tableId] = aliases;
    }
    return aliases;
}

internal static slice<ж<ast.Symbol>> trySymbolTable(this ж<Checker> Ꮡc, accessibleSymbolChainContext ctx, ast.SymbolTable symbols, symbolTableID tableId, bool ignoreQualification, bool isLocalNameLookup) {
    ref var c = ref Ꮡc.DerefOrNull();
    var candidateChainsᴷ = new stackAppend<slice<ж<ast.Symbol>>>();

    var isGlobals = tableId == stKindGlobals;
    var (res, ok) = symbols[ctx.symbol.Name, ꟷ];
    if (ok && res != nil && Ꮡc.isAccessible(ctx, res, nil, ignoreQualification)) {
        return [ctx.symbol];
    }

    slice<slice<ж<ast.Symbol>>> candidateChains = default;

    if (ok && res != nil && res.ExportSymbol != nil) {
        if (Ꮡc.isAccessible(ctx, c.getMergedSymbol(res.ExportSymbol), nil, ignoreQualification)) {
            candidateChains = append(candidateChainsᴷ, candidateChains, slice<ж<ast.Symbol>>([ctx.symbol]));
        }
    }

    foreach (var (_, symbolFromSymbolTable) in range(c.getSymbolTableAliases(symbols, tableId))) {
        if (symbolFromSymbolTable.Name != ast.InternalSymbolNameExportEquals && symbolFromSymbolTable.Name != ast.InternalSymbolNameDefault && !(isUMDExportSymbol(symbolFromSymbolTable) && ctx.enclosingDeclaration != nil && ast.IsExternalModule(ast.GetSourceFileOfNode(ctx.enclosingDeclaration))) && (!ctx.useOnlyExternalAliasing || core.Some<ж<ast.Node>>(symbolFromSymbolTable.Declarations, ast.IsExternalModuleImportEqualsDeclaration)) && (isLocalNameLookup && !core.Some<ж<ast.Node>>(symbolFromSymbolTable.Declarations, isNamespaceReexportDeclaration) || !isLocalNameLookup) && (ignoreQualification || len(getDeclarationsOfKind(ref (symbolFromSymbolTable).DerefOrNull(), ast.KindExportSpecifier)) == 0)) {
            var resolvedImportedSymbol = Ꮡc.resolveAlias(symbolFromSymbolTable);
            var candidate = Ꮡc.getCandidateListForSymbol(ctx, symbolFromSymbolTable, resolvedImportedSymbol, ignoreQualification);
            if (len(candidate) > 0) {
                candidateChains = append(candidateChains, candidate);
            }
        }
    }

    if (len(candidateChains) > 0) {
        slices.SortStableFunc(candidateChains, c.compareSymbolChains);
        return candidateChains[0];
    }

    if (isGlobals) {
        return Ꮡc.getCandidateListForSymbol(ctx, c.globalThisSymbol, c.globalThisSymbol, ignoreQualification);
    }
    return default;
}

internal static nint compareSymbolChainsWorker(this ref Checker c, slice<ж<ast.Symbol>> a, slice<ж<ast.Symbol>> b) {
    nint chainLen = len(a) - len(b);
    if (chainLen != 0) {
        return chainLen;
    }

    nint idx = 0;
    while (idx < len(a)) {
        nint comparison = c.compareSymbols(a[idx], b[idx]);
        if (comparison != 0) {
            return comparison;
        }
        idx++;
    }
    return 0;
}

} partial class checker_package {

internal static bool isUMDExportSymbol(ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return Ꮡsymbol != nil && len(symbol.Declarations) > 0 && symbol.Declarations[0] != nil && ast.IsNamespaceExportDeclaration(symbol.Declarations[0]);
}

internal static bool isNamespaceReexportDeclaration(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsNamespaceExport(Ꮡnode) && node.Parent.ModuleSpecifier() != nil;
}

} partial class checkerᴛChecker {

internal static slice<ж<ast.Symbol>> getCandidateListForSymbol(this ж<Checker> Ꮡc, accessibleSymbolChainContext ctx, ж<ast.Symbol> symbolFromSymbolTable, ж<ast.Symbol> resolvedImportedSymbol, bool ignoreQualification) {
    if (Ꮡc.isAccessible(ctx, symbolFromSymbolTable, resolvedImportedSymbol, ignoreQualification)) {
        return [symbolFromSymbolTable];
    }

    var candidateTable = Ꮡc.getExportsOfSymbol(resolvedImportedSymbol);
    if (candidateTable == nil) {
        return default;
    }
    var candidateTableId = symbolTableIDFromResolvedExports(resolvedImportedSymbol);
    var accessibleSymbolsFromExports = Ꮡc.getAccessibleSymbolChainFromSymbolTable(ctx, candidateTable, candidateTableId, true, false);
    if (len(accessibleSymbolsFromExports) == 0) {
        return default;
    }
    if (!Ꮡc.canQualifySymbol(ctx, symbolFromSymbolTable, getQualifiedLeftMeaning(ctx.meaning))) {
        return default;
    }
    return appendꓸꓸꓸ(slice<ж<ast.Symbol>>([symbolFromSymbolTable]), accessibleSymbolsFromExports);
}

internal static bool isAccessible(this ж<Checker> Ꮡc, accessibleSymbolChainContext ctx, ж<ast.Symbol> ᏑsymbolFromSymbolTable, ж<ast.Symbol> resolvedAliasSymbol, bool ignoreQualification) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var symbolFromSymbolTable = ref ᏑsymbolFromSymbolTable.DerefOrNull();

    var likeSymbols = false;
    if (ctx.symbol == resolvedAliasSymbol) {
        likeSymbols = true;
    }
    if (ctx.symbol == ᏑsymbolFromSymbolTable) {
        likeSymbols = true;
    }
    var symbol = c.getMergedSymbol(ctx.symbol);
    if (symbol == c.getMergedSymbol(resolvedAliasSymbol)) {
        likeSymbols = true;
    }
    if (symbol == c.getMergedSymbol(ᏑsymbolFromSymbolTable)) {
        likeSymbols = true;
    }
    if (!likeSymbols) {
        return false;
    }
    return !core.Some<ж<ast.Node>>(symbolFromSymbolTable.Declarations, hasNonGlobalAugmentationExternalModuleSymbol) && (ignoreQualification || Ꮡc.canQualifySymbol(ctx, c.getMergedSymbol(ᏑsymbolFromSymbolTable), ctx.meaning));
}

internal static bool canQualifySymbol(this ж<Checker> Ꮡc, accessibleSymbolChainContext ctx, ж<ast.Symbol> ᏑsymbolFromSymbolTable, ast.SymbolFlags meaning) {
    ref var symbolFromSymbolTable = ref ᏑsymbolFromSymbolTable.DerefOrNull();

    return !Ꮡc.needsQualification(ᏑsymbolFromSymbolTable, ctx.enclosingDeclaration, meaning) || len(Ꮡc.getAccessibleSymbolChainEx(new accessibleSymbolChainContext(symbolFromSymbolTable.Parent, ctx.enclosingDeclaration, getQualifiedLeftMeaning(meaning), ctx.useOnlyExternalAliasing, ctx.visitedSymbolTablesMap))) > 0;
}

internal static bool needsQualification(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning) {
    var qualify = false;
    Ꮡc.someSymbolTableInScope(enclosingDeclaration, (ast.SymbolTable symbolTable, symbolTableID _Δp1, bool _Δp2, bool _Δp3, ж<ast.Node> _Δp4) => {
        var (res, ok) = symbolTable[symbol.Value.Name, ꟷ];
        if (!ok || res == nil) {
            return false;
        }
        var symbolFromSymbolTable = Ꮡc.Value.getMergedSymbol(res);
        if (symbolFromSymbolTable == nil) {
            return false;
        }
        if (symbolFromSymbolTable == symbol) {
            return true;
        }

        var shouldResolveAlias = (ast.SymbolFlags)(symbolFromSymbolTable.Flags & ast.SymbolFlagsAlias) != 0 && ast.GetDeclarationOfKind(symbolFromSymbolTable, ast.KindExportSpecifier) == nil;
        if (shouldResolveAlias) {
            symbolFromSymbolTable = Ꮡc.resolveAlias(symbolFromSymbolTable);
        }
        var flags = symbolFromSymbolTable.Flags;
        if (shouldResolveAlias) {
            flags = Ꮡc.getSymbolFlags(symbolFromSymbolTable);
        }
        if ((ast.SymbolFlags)(flags & meaning) != 0) {
            qualify = true;
            return true;
        }

        return false;
    });

    return qualify;
}

} partial class checker_package {

internal static bool isPropertyOrMethodDeclarationSymbol(ref ast.Symbol symbol) {
    if (len(symbol.Declarations) > 0) {
        foreach (var (_, declaration) in range(symbol.Declarations)) {
            switch (declaration.Kind) {
            case var ᴛ when ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
                continue;
                break;
            }
            default: {
                return false;
            }}

        }
        return true;
    }
    return false;
}

} partial class checkerᴛChecker {

internal static bool someSymbolTableInScope(this ж<Checker> Ꮡc, ж<ast.Node> enclosingDeclaration, Func<ast.SymbolTable, symbolTableID, bool, bool, ж<ast.Node>, bool> callback) {
    ref var c = ref Ꮡc.DerefOrNull();

    for (var location = enclosingDeclaration; location != nil; location = location.Parent) {
        if (canHaveLocals(ref (location).DerefOrNull()) && location.Locals() != nil && !ast.IsGlobalSourceFile(location)) {
            if (callback(location.Locals(), symbolTableIDFromLocals(location.AsNode()), false, true, location)) {
                return true;
            }
        }
        switch (location.Kind) {
        case var ᴛ when ᴛ == ast.KindSourceFile || ᴛ == ast.KindModuleDeclaration: {
            if (ast.IsSourceFile(location) && !ast.IsExternalOrCommonJSModule(location.AsSourceFile())) {
                break;
            }
            var sym = Ꮡc.getSymbolOfDeclaration(ast.GetReparsedNodeForNode(location));
            if (callback(sym.Exports, symbolTableIDFromExports(sym), false, true, location)) {
                return true;
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression || ᴛ == ast.KindInterfaceDeclaration: {
            ast.SymbolTable table = default;
            var sym = Ꮡc.getSymbolOfDeclaration(location);
            foreach (var (key, memberSymbol) in sym.Members) {
                if ((ast.SymbolFlags)(memberSymbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsType & ~((ast.SymbolFlags)ast.SymbolFlagsAssignment)))) != 0) {
                    if (table == nil) {
                        table = new ast.SymbolTable(0);
                    }
                    table[key] = memberSymbol;
                }
            }
            if (table != nil && callback(table, symbolTableIDFromMembers(sym), false, false, location)) {
                return true;
            }
            if (ast.IsClassExpression(location) && location.AsClassExpression().Name() != nil) {
                var nameTable = Ꮡc.getClassExpressionNameTable(location);
                if (nameTable != nil && callback(nameTable, symbolTableIDFromLocals(location.AsNode()), false, true, location)) {
                    return true;
                }
            }
            break;
        }}

    }

    return callback(c.globals, symbolTableIDFromGlobals(), false, true, nil);
}

internal static ast.SymbolTable getClassExpressionNameTable(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡlocation) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var location = ref Ꮡlocation.DerefOrNull();

    var nodeId = ast.GetNodeId(Ꮡlocation);
    if (c.classExpressionNameTables != nil) {
        var (tableΔ1, ok) = c.classExpressionNameTables[nodeId, ꟷ]; if (ok) {
            return tableΔ1;
        }
    }
    var classSymbol = Ꮡc.getSymbolOfDeclaration(Ꮡlocation);
    @string nameText = location.AsClassExpression().Name().Text();
    if (len(nameText) == 0 || classSymbol == nil) {
        return default;
    }
    var table = new ast.SymbolTable(new map<@string, ж<ast.Symbol>>{[nameText] = classSymbol});
    if (c.classExpressionNameTables == nil) {
        c.classExpressionNameTables = new map<ast.NodeId, ast.SymbolTable>();
    }
    c.classExpressionNameTables[nodeId] = table;
    return table;
}

public static printer.SymbolAccessibilityResult IsSymbolAccessible(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, bool shouldComputeAliasesToMakeVisible) {
    return Ꮡc.isSymbolAccessibleWorker(symbol, enclosingDeclaration, meaning, shouldComputeAliasesToMakeVisible, true);
}

internal static printer.SymbolAccessibilityResult isSymbolAccessibleWorker(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, bool shouldComputeAliasesToMakeVisible, bool allowModules) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡsymbol != nil && enclosingDeclaration != nil) {
        var result = Ꮡc.IsAnySymbolAccessible([Ꮡsymbol], enclosingDeclaration, Ꮡsymbol, meaning, shouldComputeAliasesToMakeVisible, allowModules);
        if (result != nil) {
            return result.Value;
        }

        var symbolExternalModule = core.FirstNonNil<ж<ast.Node>, ж<ast.Symbol>>(symbol.Declarations, Ꮡc.getExternalModuleContainer);
        if (symbolExternalModule != nil) {
            var enclosingExternalModule = Ꮡc.getExternalModuleContainer(enclosingDeclaration);
            if (symbolExternalModule != enclosingExternalModule) {
                return new printer.SymbolAccessibilityResult(
                    Accessibility: printer.SymbolAccessibilityCannotBeNamed,
                    ErrorSymbolName: Ꮡc.symbolToStringEx(Ꮡsymbol, enclosingDeclaration, meaning, SymbolFormatFlagsAllowAnyNodeKind),
                    ErrorModuleName: Ꮡc.symbolToString(symbolExternalModule),
                    ErrorNode: core.IfElse(ast.IsInJSFile(enclosingDeclaration), enclosingDeclaration, (ж<ast.Node>)(nil))
                );
            }
        }

        return new printer.SymbolAccessibilityResult(
            Accessibility: printer.SymbolAccessibilityNotAccessible,
            ErrorSymbolName: Ꮡc.symbolToStringEx(Ꮡsymbol, enclosingDeclaration, meaning, SymbolFormatFlagsAllowAnyNodeKind)
        );
    }

    return new printer.SymbolAccessibilityResult(
        Accessibility: printer.SymbolAccessibilityAccessible
    );
}

} // end checkerᴛChecker
