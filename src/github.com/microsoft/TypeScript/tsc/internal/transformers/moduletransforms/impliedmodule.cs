namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static moduletransforms_package;

partial class moduletransforms_package {

partial struct ImpliedModuleTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<transformers.TransformOptions> opts;
    internal binder.ReferenceResolver resolver;
    internal Func<ast.HasFileName, core.ModuleKind> getEmitModuleFormatOfFile;
    internal ж<transformers.Transformer> cjsTransformer;
    internal ж<transformers.Transformer> esmTransformer;
}

public static ж<transformers.Transformer> NewImpliedModuleTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new ImpliedModuleTransformer(opts: Ꮡopts, resolver: opts.Resolver, getEmitModuleFormatOfFile: opts.GetEmitModuleFormatOfFile));
    return tx.of(ImpliedModuleTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} partial class moduletransformsᴛImpliedModuleTransformer {

internal static ж<ast.Node> visit(this ref ImpliedModuleTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        Ꮡnode = tx.visitSourceFile(node.AsSourceFile()); node = ref Ꮡnode.DerefOrNull();
        break;
    }}

    return Ꮡnode;
}

internal static ж<ast.Node> visitSourceFile(this ref ImpliedModuleTransformer tx, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsDeclarationFile) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    var format = tx.getEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(Ꮡnode));

    ж<transformers.Transformer> transformer = default;
    if (format >= core.ModuleKindES2015) {
        if (tx.esmTransformer == nil) {
            tx.esmTransformer = NewESModuleTransformer(tx.opts);
        }
        transformer = tx.esmTransformer;
    } else {
        if (tx.cjsTransformer == nil) {
            tx.cjsTransformer = NewCommonJSModuleTransformer(tx.opts);
        }
        transformer = tx.cjsTransformer;
    }

    return transformer.TransformSourceFile(Ꮡnode).of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

} // end moduletransformsᴛImpliedModuleTransformer
