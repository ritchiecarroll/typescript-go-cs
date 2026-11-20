namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static estransforms_package;

partial class estransforms_package {

partial struct nullishCoalescingTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
}

} partial class estransformsᴛnullishCoalescingTransformer {

internal static ж<ast.Node> visit(this ref nullishCoalescingTransformer ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsNullishCoalescing) == 0) {
        return Ꮡnode;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return ch.visitBinaryExpression(node.AsBinaryExpression());
    }
    default: {
        return ch.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitBinaryExpression(this ref nullishCoalescingTransformer ch, ж<ast.BinaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.OperatorToken.Kind) {
    case var ᴛ when ᴛ == ast.KindQuestionQuestionToken: {
        var left = ch.Transformer.Visitor().VisitNode(node.Left);
        var right = left;
        if (!transformers.IsSimpleCopiableExpression(left)) {
            right = ch.Transformer.Factory().NewTempVariable();
            ch.Transformer.EmitContext().AddVariableDeclaration(right);
            left = ch.Transformer.Factory().NewAssignmentExpression(right, left);
        }
        return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
            createNotNullCondition(ref (ch.Transformer.EmitContext()).DerefOrNull(), left, right, false),
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
            right,
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
            ch.Transformer.Visitor().VisitNode(node.Right));
    }
    default: {
        return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }}

}

} partial class estransforms_package {

internal static ж<transformers.Transformer> newNullishCoalescingTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new nullishCoalescingTransformer());
    return tx.of(nullishCoalescingTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} // end estransforms_package
