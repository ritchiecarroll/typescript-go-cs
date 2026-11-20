namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static binder_package;

partial class binder_package {

partial interface ReferenceResolver {
    ж<ast.Node> GetReferencedExportContainer(ж<astꓸIdentifierNode> node, bool prefixLocals);
    ж<astꓸDeclaration> GetReferencedImportDeclaration(ж<astꓸIdentifierNode> node);
    ж<astꓸDeclaration> GetReferencedValueDeclaration(ж<astꓸIdentifierNode> node);
    slice<ж<astꓸDeclaration>> GetReferencedValueDeclarations(ж<astꓸIdentifierNode> node);
    @string GetElementAccessExpressionName(ж<ast.ElementAccessExpression> expression);
    ж<astꓸDeclaration> GetReferencedMemberValueDeclaration(ж<ast.Node> node);
}

partial struct ReferenceResolverHooks {
    public Func<ж<ast.Node>, @string, ast.SymbolFlags, ж<diagnostics.Message>, bool, bool, ж<ast.Symbol>> ResolveName;
    public Func<ж<ast.Node>, ж<ast.Symbol>> GetResolvedSymbol;
    public Func<ж<ast.Symbol>, ж<ast.Symbol>> GetMergedSymbol;
    public Func<ж<ast.Symbol>, ж<ast.Symbol>> GetParentOfSymbol;
    public Func<ж<astꓸDeclaration>, ж<ast.Symbol>> GetSymbolOfDeclaration;
    public Func<ж<ast.Symbol>, ast.SymbolFlags, ж<astꓸDeclaration>> GetTypeOnlyAliasDeclaration;
    public Func<ж<ast.Symbol>, ж<ast.Symbol>> GetExportSymbolOfValueSymbolIfExported;
    public Func<ж<ast.ElementAccessExpression>, (@string, bool)> GetElementAccessExpressionName;
}

internal static ReferenceResolver _ᴛ1ʗ = new referenceResolverжReferenceResolver(Ꮡ(new referenceResolver()));

partial struct referenceResolver {
    internal ж<NameResolver> resolver;
    internal ж<core.CompilerOptions> options;
    internal ReferenceResolverHooks hooks;
}

public static ReferenceResolver NewReferenceResolver(ж<core.CompilerOptions> options, ReferenceResolverHooks hooks) {
    return new referenceResolverжReferenceResolver(Ꮡ(new referenceResolver(
        options: options,
        hooks: hooks
    )));
}

} partial class binderᴛreferenceResolver {

internal static ж<ast.Symbol> getResolvedSymbol(this ref referenceResolver r, ж<ast.Node> node) {
    if (node != nil) {
        if (r.hooks.GetResolvedSymbol != nil) {
            return r.hooks.GetResolvedSymbol(node);
        }
    }
    return default;
}

internal static ж<ast.Symbol> getMergedSymbol(this ref referenceResolver r, ж<ast.Symbol> symbol) {
    if (symbol != nil) {
        if (r.hooks.GetMergedSymbol != nil) {
            return r.hooks.GetMergedSymbol(symbol);
        }
        return symbol;
    }
    return default;
}

internal static ж<ast.Symbol> getParentOfSymbol(this ref referenceResolver r, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡsymbol != nil) {
        if (r.hooks.GetParentOfSymbol != nil) {
            return r.hooks.GetParentOfSymbol(Ꮡsymbol);
        }
        return symbol.Parent;
    }
    return default;
}

internal static ж<ast.Symbol> getSymbolOfDeclaration(this ref referenceResolver r, ж<astꓸDeclaration> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    if (Ꮡdeclaration != nil) {
        if (r.hooks.GetSymbolOfDeclaration != nil) {
            return r.hooks.GetSymbolOfDeclaration(Ꮡdeclaration);
        }
        return declaration.Symbol();
    }
    return default;
}

internal static ж<ast.Symbol> getReferencedValueSymbol(this ref referenceResolver r, ж<astꓸIdentifierNode> Ꮡreference, bool startInDeclarationContainer) {
    ref var reference = ref Ꮡreference.DerefOrNull();

    var resolvedSymbol = r.getResolvedSymbol(Ꮡreference);
    if (resolvedSymbol != nil) {
        return resolvedSymbol;
    }

    var location = Ꮡreference;
    if (startInDeclarationContainer && reference.Parent != nil && ast.IsDeclaration(reference.Parent) && reference.Parent.Name() == Ꮡreference) {
        location = ast.GetDeclarationContainer(reference.Parent);
    }

    if (r.hooks.ResolveName != nil) {
        return r.hooks.ResolveName(location, reference.Text(), (ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsExportValue | ast.SymbolFlagsValue) | ast.SymbolFlagsAlias), nil, false, false);
    }

    if (r.resolver == nil) {
        r.resolver = Ꮡ(new NameResolver(
            CompilerOptions: r.options
        ));
    }

    return r.resolver.Resolve(location, reference.Text(), (ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsExportValue | ast.SymbolFlagsValue) | ast.SymbolFlagsAlias), nil, false, false);
}

internal static bool isTypeOnlyAliasDeclaration(this ref referenceResolver r, ж<ast.Symbol> symbol) {
    if (symbol != nil) {
        if (r.hooks.GetTypeOnlyAliasDeclaration != nil) {
            return r.hooks.GetTypeOnlyAliasDeclaration(symbol, ast.SymbolFlagsValue) != nil;
        }

        var node = r.getDeclarationOfAliasSymbol(symbol);
        while (node != nil) {
            switch (node.Kind) {
            case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindExportDeclaration: {
                return node.IsTypeOnly();
            }
            case var ᴛ when ᴛ == ast.KindImportClause || ᴛ == ast.KindImportSpecifier || ᴛ == ast.KindExportSpecifier: {
                if (node.IsTypeOnly()) {
                    return true;
                }
                node = node.Parent;
                continue;
                break;
            }
            case var ᴛ when ᴛ == ast.KindNamedImports || ᴛ == ast.KindNamedExports: {
                node = node.Parent;
                continue;
                break;
            }}

            break;
        }
    }
    return false;
}

internal static ж<astꓸDeclaration> getDeclarationOfAliasSymbol(this ref referenceResolver r, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return core.FindLast<ж<ast.Node>>(symbol.Declarations, ast.IsAliasSymbolDeclaration);
}

internal static ж<ast.Symbol> getExportSymbolOfValueSymbolIfExported(this ref referenceResolver r, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡsymbol != nil) {
        if (r.hooks.GetExportSymbolOfValueSymbolIfExported != nil) {
            return r.hooks.GetExportSymbolOfValueSymbolIfExported(Ꮡsymbol);
        }
        if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsExportValue) != 0 && symbol.ExportSymbol != nil) {
            Ꮡsymbol = symbol.ExportSymbol; symbol = ref Ꮡsymbol.DerefOrNull();
        }
        return r.getMergedSymbol(Ꮡsymbol);
    }
    return default;
}

internal static ж<ast.Node> GetReferencedExportContainer(this ж<referenceResolver> Ꮡr, ж<astꓸIdentifierNode> Ꮡnode, bool prefixLocals) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var startInDeclarationContainer = node.Parent != nil && (node.Parent.Kind == ast.KindModuleDeclaration || node.Parent.Kind == ast.KindEnumDeclaration) && Ꮡnode == node.Parent.Name();
    {
        var symbol = r.getReferencedValueSymbol(Ꮡnode, startInDeclarationContainer); if (symbol != nil) {
            if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsExportValue) != 0) {
                var exportSymbol = r.getMergedSymbol(symbol.ExportSymbol);
                if (!prefixLocals && (ast.SymbolFlags)(exportSymbol.Flags & ast.SymbolFlagsExportHasLocal) != 0 && (ast.SymbolFlags)(exportSymbol.Flags & ast.SymbolFlagsVariable) == 0) {
                    return default;
                }
                symbol = exportSymbol;
            }
            var parentSymbol = r.getParentOfSymbol(symbol);
            if (parentSymbol != nil) {
                if ((ast.SymbolFlags)(parentSymbol.Flags & ast.SymbolFlagsValueModule) != 0 && parentSymbol.ValueDeclaration != nil && parentSymbol.ValueDeclaration.Kind == ast.KindSourceFile) {
                    var symbolFile = parentSymbol.ValueDeclaration.AsSourceFile();
                    var referenceFile = ast.GetSourceFileOfNode(Ꮡnode);
                    var symbolIsUmdExport = symbolFile != referenceFile;
                    if (symbolIsUmdExport) {
                        return default;
                    }
                    return symbolFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
                }
                var parentSymbolʗ1 = parentSymbol;
                var isMatchingContainer = (ж<ast.Node> n) => (n.Kind == ast.KindModuleDeclaration || n.Kind == ast.KindEnumDeclaration) && Ꮡr.Value.getSymbolOfDeclaration(n) == parentSymbolʗ1;
                return ast.FindAncestor(node.Parent, isMatchingContainer);
            }
        }
    }

    return default;
}

internal static ж<astꓸDeclaration> GetReferencedImportDeclaration(this ref referenceResolver r, ж<astꓸIdentifierNode> node) {
    var symbol = r.getReferencedValueSymbol(node, false); if (symbol != nil) {
        if (ast.IsNonLocalAlias(symbol, ast.SymbolFlagsValue) && !r.isTypeOnlyAliasDeclaration(symbol)) {
            return r.getDeclarationOfAliasSymbol(symbol);
        }
    }

    return default;
}

internal static ж<astꓸDeclaration> GetReferencedValueDeclaration(this ref referenceResolver r, ж<astꓸIdentifierNode> node) {
    var symbol = r.getReferencedValueSymbol(node, false); if (symbol != nil) {
        return r.getExportSymbolOfValueSymbolIfExported(symbol).ValueDeclaration;
    }
    return default;
}

internal static slice<ж<astꓸDeclaration>> GetReferencedValueDeclarations(this ref referenceResolver r, ж<astꓸIdentifierNode> node) {
    var declarationsᴷ = new stackAppend<ж<astꓸDeclaration>>(moves: true);

    slice<ж<astꓸDeclaration>> declarations = default;
    var symbol = r.getReferencedValueSymbol(node, false); if (symbol != nil) {
        symbol = r.getExportSymbolOfValueSymbolIfExported(symbol);
        foreach (var (_, declaration) in range(symbol.Declarations)) {
            switch (declaration.Kind) {
            case var ᴛ when ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindParameter || ᴛ == ast.KindBindingElement || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindShorthandPropertyAssignment || ᴛ == ast.KindEnumMember || ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression || ᴛ == ast.KindEnumDeclaration || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindModuleDeclaration: {
                declarations = append(declarationsᴷ, declarations, declaration);
                break;
            }}

        }
    }
    declarations = move2heap(declarationsᴷ, declarations);
    return declarations;
}

internal static @string GetElementAccessExpressionName(this ref referenceResolver r, ж<ast.ElementAccessExpression> expression) {
    if (expression != nil) {
        if (r.hooks.GetElementAccessExpressionName != nil) {
            var (name, ok) = r.hooks.GetElementAccessExpressionName(expression); if (ok) {
                return name;
            }
        }
    }
    return "";
}

internal static ж<astꓸDeclaration> GetReferencedMemberValueDeclaration(this ref referenceResolver r, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var s = r.getResolvedSymbol(Ꮡnode);
    if (s == nil && node.Symbol() != nil) {
        s = r.getMergedSymbol(node.Symbol());
    }
    if (s == nil) {
        return default;
    }
    return r.getExportSymbolOfValueSymbolIfExported(s).ValueDeclaration;
}

} // end binderᴛreferenceResolver
