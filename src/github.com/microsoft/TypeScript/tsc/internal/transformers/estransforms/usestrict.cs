namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static estransforms_package;

partial class estransforms_package {

public static ж<transformers.Transformer> NewUseStrictTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new useStrictTransformer(
        compilerOptions: opts.CompilerOptions,
        getEmitModuleFormatOfFile: opts.GetEmitModuleFormatOfFile
    ));
    return tx.of(useStrictTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

partial struct useStrictTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal Func<ast.HasFileName, core.ModuleKind> getEmitModuleFormatOfFile;
}

} partial class estransformsᴛuseStrictTransformer {

internal static ж<ast.Node> visit(this ref useStrictTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind != ast.KindSourceFile) {
        return Ꮡnode;
    }
    return tx.visitSourceFile(node.AsSourceFile());
}

internal static ж<ast.Node> visitSourceFile(this ref useStrictTransformer tx, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.ScriptKind == core.ScriptKindJSON) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    var isExternalModule = ast.IsExternalModule(Ꮡnode);
    var moduleKind = tx.compilerOptions.GetEmitModuleKind();
    var format = tx.getEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(Ꮡnode));

    if (isExternalModule && moduleKind >= core.ModuleKindES2015 && (moduleKind == core.ModuleKindPreserve || format >= core.ModuleKindES2015)) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    var statements = tx.Transformer.Factory().EnsureUseStrict(node.Statements.Nodes);
    var statementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
    statementList.Loc = node.Statements.Loc;
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(Ꮡnode, statementList, node.EndOfFileToken).AsSourceFile().of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

} // end estransformsᴛuseStrictTransformer
