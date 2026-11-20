namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tstransforms_package;

partial class tstransforms_package {

partial struct ImportElisionTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ж<ast.SourceFile> currentSourceFile;
    internal printer.EmitResolver emitResolver;
}

public static ж<transformers.Transformer> NewImportElisionTransformer(ж<transformers.TransformOptions> Ꮡopt) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    var compilerOptions = opt.CompilerOptions;
    var emitContext = opt.Context;
    if (compilerOptions.VerbatimModuleSyntax.IsTrue()) {
        throw panic("ImportElisionTransformer should not be used with VerbatimModuleSyntax");
    }
    var tx = Ꮡ(new ImportElisionTransformer(compilerOptions: compilerOptions, emitResolver: opt.EmitResolver));
    return tx.of(ImportElisionTransformer.ᏑTransformer).NewTransformer(tx.visit, emitContext);
}

} partial class tstransformsᴛImportElisionTransformer {

internal static ж<ast.Node> visit(this ref ImportElisionTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsSourceFile(Ꮡnode) && tx.emitResolver != nil) {
        tx.emitResolver.MarkLinkedReferencesRecursively(tx.Transformer.EmitContext().MostOriginal(Ꮡnode).AsSourceFile());
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
        if (ast.IsExternalModuleImportEqualsDeclaration(Ꮡnode)) {
            if (!tx.shouldEmitAliasDeclaration(Ꮡnode)) {
                return default;
            }
        } else {
            if (!tx.shouldEmitImportEqualsDeclaration(node.AsImportEqualsDeclaration())) {
                return default;
            }
        }
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindImportDeclaration: {
        var n = node.AsImportDeclaration();
        if ((~n).ImportClause != nil) {
            var importClause = tx.Transformer.Visitor().VisitNode((~n).ImportClause);
            if (importClause == nil) {
                return default;
            }
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(n, n.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(), importClause, (~n).ModuleSpecifier, tx.Transformer.Visitor().VisitNode((~n).Attributes));
        }
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindImportClause: {
        var n = node.AsImportClause();
        var name = core.IfElse(tx.shouldEmitAliasDeclaration(Ꮡnode), n.Name(), (ж<astꓸDeclarationName>)(nil));
        var namedBindings = tx.Transformer.Visitor().VisitNode(n.NamedBindings);
        if (name == nil && namedBindings == nil) {
            return default;
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportClause(n, n.PhaseModifier, name, namedBindings);
    }
    case var ᴛ when ᴛ == ast.KindNamespaceImport: {
        if (!tx.shouldEmitAliasDeclaration(Ꮡnode)) {
            return default;
        }
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindNamedImports: {
        var n = node.AsNamedImports();
        var elements = tx.Transformer.Visitor().VisitNodes((~n).Elements);
        if (len(elements.Nodes) == 0) {
            return default;
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateNamedImports(n, elements);
    }
    case var ᴛ when ᴛ == ast.KindImportSpecifier: {
        if (!tx.shouldEmitAliasDeclaration(Ꮡnode)) {
            return default;
        }
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        if (!tx.compilerOptions.VerbatimModuleSyntax.IsTrue() && !tx.isValueAliasDeclaration(Ꮡnode)) {
            return default;
        }
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindExportDeclaration: {
        var n = node.AsExportDeclaration();
        ж<ast.Node> exportClause = default;
        if (n.ExportClause != nil) {
            exportClause = tx.Transformer.Visitor().VisitNode(n.ExportClause);
            if (exportClause == nil) {
                return default;
            }
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExportDeclaration(n, nil, false, exportClause, tx.Transformer.Visitor().VisitNode((~n).ModuleSpecifier), tx.Transformer.Visitor().VisitNode((~n).Attributes));
    }
    case var ᴛ when ᴛ == ast.KindNamedExports: {
        var n = node.AsNamedExports();
        var elements = tx.Transformer.Visitor().VisitNodes((~n).Elements);
        if (len(elements.Nodes) == 0) {
            return default;
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateNamedExports(n, elements);
    }
    case var ᴛ when ᴛ == ast.KindExportSpecifier: {
        if (!tx.isValueAliasDeclaration(Ꮡnode)) {
            return default;
        }
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        var savedCurrentSourceFile = tx.currentSourceFile;
        tx.currentSourceFile = node.AsSourceFile();
        Ꮡnode = tx.Transformer.Visitor().VisitEachChild(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        tx.currentSourceFile = savedCurrentSourceFile;
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindModuleBlock: {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }
    default: {
        return Ꮡnode;
    }}

}

internal static bool shouldEmitAliasDeclaration(this ref ImportElisionTransformer tx, ж<ast.Node> node) {
    return ast.IsInJSFile(node) || tx.isReferencedAliasDeclaration(node);
}

internal static bool shouldEmitImportEqualsDeclaration(this ref ImportElisionTransformer tx, ж<ast.ImportEqualsDeclaration> node) {
    return tx.shouldEmitAliasDeclaration(node.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) || (!ast.IsExternalModule(tx.currentSourceFile) && tx.isTopLevelValueImportEqualsWithEntityName(node.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
}

internal static bool isReferencedAliasDeclaration(this ref ImportElisionTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = tx.Transformer.EmitContext().ParseNode(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    return Ꮡnode == nil || tx.emitResolver.IsReferencedAliasDeclaration(Ꮡnode);
}

internal static bool isValueAliasDeclaration(this ref ImportElisionTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = tx.Transformer.EmitContext().ParseNode(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    return Ꮡnode == nil || tx.emitResolver.IsValueAliasDeclaration(Ꮡnode);
}

internal static bool isTopLevelValueImportEqualsWithEntityName(this ref ImportElisionTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = tx.Transformer.EmitContext().ParseNode(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    return Ꮡnode != nil && tx.emitResolver.IsTopLevelValueImportEqualsWithEntityName(Ꮡnode);
}

} // end tstransformsᴛImportElisionTransformer
