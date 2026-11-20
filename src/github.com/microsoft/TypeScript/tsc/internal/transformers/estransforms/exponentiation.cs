namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static estransforms_package;

partial class estransforms_package {

partial struct exponentiationTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
}

} partial class estransformsᴛexponentiationTransformer {

internal static ж<ast.Node> visit(this ref exponentiationTransformer ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsExponentiationOperator) == 0) {
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

internal static ж<ast.Node> visitBinaryExpression(this ref exponentiationTransformer ch, ж<ast.BinaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.OperatorToken.Kind) {
    case var ᴛ when ᴛ == ast.KindAsteriskAsteriskEqualsToken: {
        return ch.visitExponentiationAssignmentExpression(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindAsteriskAsteriskToken: {
        return ch.visitExponentiationExpression(Ꮡnode);
    }}

    return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitExponentiationAssignmentExpression(this ref exponentiationTransformer ch, ж<ast.BinaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> target = default;
    ж<ast.Node> value = default;
    var left = ch.Transformer.Visitor().VisitNode(node.Left);
    var right = ch.Transformer.Visitor().VisitNode(node.Right);
    if (ast.IsElementAccessExpression(left)) {
        var expressionTemp = ch.Transformer.Factory().NewTempVariable();
        ch.Transformer.EmitContext().AddVariableDeclaration(expressionTemp);
        var argumentExpressionTemp = ch.Transformer.Factory().NewTempVariable();
        ch.Transformer.EmitContext().AddVariableDeclaration(argumentExpressionTemp);

        var objExpr = ch.Transformer.Factory().NewAssignmentExpression(expressionTemp, left.Expression());
        objExpr.Loc = left.Expression().Loc;
        var accessExpr = ch.Transformer.Factory().NewAssignmentExpression(argumentExpressionTemp, left.AsElementAccessExpression().ArgumentExpression);
        accessExpr.Loc = left.AsElementAccessExpression().ArgumentExpression.Loc;

        target = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(objExpr, nil, accessExpr, ast.NodeFlagsNone);

        value = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(expressionTemp, nil, argumentExpressionTemp, ast.NodeFlagsNone);
        value.Loc = left.Loc;
    } else if (ast.IsPropertyAccessExpression(left)) {
        var expressionTemp = ch.Transformer.Factory().NewTempVariable();
        ch.Transformer.EmitContext().AddVariableDeclaration(expressionTemp);
        var assignment = ch.Transformer.Factory().NewAssignmentExpression(expressionTemp, left.Expression());
        assignment.Loc = left.Expression().Loc;
        target = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(assignment, nil, left.Name(), ast.NodeFlagsNone);
        target.Loc = left.Loc;

        value = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(expressionTemp, nil, left.Name(), ast.NodeFlagsNone);
        value.Loc = left.Loc;
    } else {
        target = left;
        value = left;
    }

    var rhs = ch.Transformer.Factory().NewGlobalMethodCall("Math", "pow", [value, right]);
    rhs.Loc = node.Loc;
    var result = ch.Transformer.Factory().NewAssignmentExpression(target, rhs);
    result.Loc = node.Loc;
    return result;
}

internal static ж<ast.Node> visitExponentiationExpression(this ref exponentiationTransformer ch, ж<ast.BinaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var left = ch.Transformer.Visitor().VisitNode(node.Left);
    var right = ch.Transformer.Visitor().VisitNode(node.Right);
    var result = ch.Transformer.Factory().NewGlobalMethodCall("Math", "pow", [left, right]);
    result.Loc = node.Loc;
    return result;
}

} partial class estransforms_package {

internal static ж<transformers.Transformer> newExponentiationTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new exponentiationTransformer());
    return tx.of(exponentiationTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} // end estransforms_package
