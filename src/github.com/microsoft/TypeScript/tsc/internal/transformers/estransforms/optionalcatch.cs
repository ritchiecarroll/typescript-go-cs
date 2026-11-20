namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static estransforms_package;

partial class estransforms_package {

partial struct optionalCatchTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
}

} partial class estransformsᴛoptionalCatchTransformer {

internal static ж<ast.Node> visit(this ref optionalCatchTransformer ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsMissingCatchClauseVariable) == 0) {
        return Ꮡnode;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindCatchClause: {
        return ch.visitCatchClause(node.AsCatchClause());
    }
    default: {
        return ch.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitCatchClause(this ref optionalCatchTransformer ch, ж<ast.CatchClause> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.VariableDeclaration == nil) {
        return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCatchClause(
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(ch.Transformer.Factory().NewTempVariable(), nil, nil, nil),
            ch.Transformer.Visitor().Visit(node.Block));
    }
    return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

} partial class estransforms_package {

internal static ж<transformers.Transformer> newOptionalCatchTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new optionalCatchTransformer());
    return tx.of(optionalCatchTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} // end estransforms_package
