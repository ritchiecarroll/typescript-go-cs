global using TransformerFactory = System.Func<go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.TransformOptions>, go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer>>;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸTransformerFactory = Span<TransformerFactory>;
using static transformers_package;

partial class transformers_package {

partial struct chainedTransformer {
    /*embed*/ public Transformer Transformer;
    internal slice<ж<Transformer>> components;
}

} partial class transformersᴛchainedTransformer {

internal static ж<ast.Node> visit(this ref chainedTransformer ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind != ast.KindSourceFile) {
        throw panic("Chained transform passed non-sourcefile initial node");
    }
    var result = node.AsSourceFile();
    foreach (var (_, t) in range(ch.components)) {
        result = t.TransformSourceFile(result);
    }
    return result.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

} partial class transformers_package {

partial struct TransformOptions {
    public ж<printer.EmitContext> Context;
    public ж<core.CompilerOptions> CompilerOptions;
    public binder.ReferenceResolver Resolver;
    public printer.EmitResolver EmitResolver;
    public Func<ast.HasFileName, core.ModuleKind> GetEmitModuleFormatOfFile;
}

public static TransformerFactory Chain(params ꓸꓸꓸTransformerFactory transformsʗp) {
    var transforms = transformsʗp.slice();

    if (len(transforms) < 2) {
        if (len(transforms) == 0) {
            throw panic("Expected some number of transforms to chain, but got none");
        }
        return transforms[0];
    }
    var transformsʗ1 = transforms;
    return (ж<TransformOptions> opt) => {
        var constructed = new slice<ж<Transformer>>(0, len(transformsʗ1));
        foreach (var (_, t) in range(transformsʗ1)) {
            var result = t(opt); if (result != nil) {
                constructed = append(constructed, result);
            }
        }
        switch (len(constructed)) {
        case 0: {
            return default;
        }
        case 1: {
            return constructed[0];
        }}

        var ch = Ꮡ(new chainedTransformer(components: constructed));
        return ch.of(chainedTransformer.ᏑTransformer).NewTransformer(ch.visit, opt.Context);
    };
}

} // end transformers_package
