namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static estransforms_package;

partial class estransforms_package {

partial struct logicalAssignmentTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
}

} partial class estransformsᴛlogicalAssignmentTransformer {

internal static ж<ast.Node> visit(this ref logicalAssignmentTransformer ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsLogicalAssignments) == 0) {
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

internal static ж<ast.Node> visitBinaryExpression(this ref logicalAssignmentTransformer ch, ж<ast.BinaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ast.Kind nonAssignmentOperator = default;
    switch (node.OperatorToken.Kind) {
    case var ᴛ when ᴛ == ast.KindBarBarEqualsToken: {
        nonAssignmentOperator = ast.KindBarBarToken;
        break;
    }
    case var ᴛ when ᴛ == ast.KindAmpersandAmpersandEqualsToken: {
        nonAssignmentOperator = ast.KindAmpersandAmpersandToken;
        break;
    }
    case var ᴛ when ᴛ == ast.KindQuestionQuestionEqualsToken: {
        nonAssignmentOperator = ast.KindQuestionQuestionToken;
        break;
    }
    default: {
        return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }}

    var left = ast.SkipParentheses(ch.Transformer.Visitor().VisitNode(node.Left));
    var assignmentTarget = left;
    var right = ast.SkipParentheses(ch.Transformer.Visitor().VisitNode(node.Right));

    if (ast.IsAccessExpression(left)) {
        var propertyAccessTargetSimpleCopiable = transformers.IsSimpleCopiableExpression(left.Expression());
        var propertyAccessTarget = left.Expression();
        var propertyAccessTargetAssignment = left.Expression();
        if (!propertyAccessTargetSimpleCopiable) {
            propertyAccessTarget = ch.Transformer.Factory().NewTempVariable();
            ch.Transformer.EmitContext().AddVariableDeclaration(propertyAccessTarget);
            propertyAccessTargetAssignment = ch.Transformer.Factory().NewAssignmentExpression(
                propertyAccessTarget,
                left.Expression());
        }

        if (ast.IsPropertyAccessExpression(left)) {
            assignmentTarget = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                propertyAccessTarget,
                nil,
                left.Name(),
                ast.NodeFlagsNone);
            left = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                propertyAccessTargetAssignment,
                nil,
                left.Name(),
                ast.NodeFlagsNone);
        } else {
            var elementAccessArgumentSimpleCopiable = transformers.IsSimpleCopiableExpression(left.AsElementAccessExpression().ArgumentExpression);
            var elementAccessArgument = left.AsElementAccessExpression().ArgumentExpression;
            var argumentExpr = elementAccessArgument;
            if (!elementAccessArgumentSimpleCopiable) {
                elementAccessArgument = ch.Transformer.Factory().NewTempVariable();
                ch.Transformer.EmitContext().AddVariableDeclaration(elementAccessArgument);
                argumentExpr = ch.Transformer.Factory().NewAssignmentExpression(
                    elementAccessArgument,
                    left.AsElementAccessExpression().ArgumentExpression);
            }

            assignmentTarget = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(
                propertyAccessTarget,
                nil,
                elementAccessArgument,
                ast.NodeFlagsNone);
            left = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(
                propertyAccessTargetAssignment,
                nil,
                argumentExpr,
                ast.NodeFlagsNone);
        }
    }

    return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
        nil,
        left,
        nil,
        ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(nonAssignmentOperator),
        ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression(
            ch.Transformer.Factory().NewAssignmentExpression(
                assignmentTarget,
                right)));
}

} partial class estransforms_package {

internal static ж<transformers.Transformer> newLogicalAssignmentTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new logicalAssignmentTransformer());
    return tx.of(logicalAssignmentTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} // end estransforms_package
