namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static estransforms_package;

partial class estransforms_package {

partial struct optionalChainTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
}

} partial class estransformsᴛoptionalChainTransformer {

internal static ж<ast.Node> visit(this ref optionalChainTransformer ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsOptionalChaining) == 0) {
        return Ꮡnode;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        return ch.visitCallExpression(node.AsCallExpression(), false);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0) {
            return ch.visitOptionalExpression(Ꮡnode, false, false);
        }
        return ch.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindDeleteExpression: {
        return ch.visitDeleteExpression(node.AsDeleteExpression());
    }
    default: {
        return ch.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitCallExpression(this ref optionalChainTransformer ch, ж<ast.CallExpression> Ꮡnode, bool captureThisArg) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0) {
        return ch.visitOptionalExpression(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), captureThisArg, false);
    }
    if (ast.IsParenthesizedExpression(node.Expression)) {
        var unwrapped = ast.SkipParentheses(node.Expression);
        if ((ast.NodeFlags)(unwrapped.Flags & ast.NodeFlagsOptionalChain) != 0) {
            var expression = ch.visitParenthesizedExpression(node.Expression.AsParenthesizedExpression(), true, false);
            var args = ch.Transformer.Visitor().VisitNodes(node.Arguments);
            if (ast.IsSyntheticReferenceExpression(expression)) {
                var res = ch.Transformer.Factory().NewFunctionCallCall((~expression.AsSyntheticReferenceExpression()).Expression, expression.AsSyntheticReferenceExpression().ThisArg, args.Nodes);
                res.Loc = node.Loc;
                ch.Transformer.EmitContext().SetOriginal(res, Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                return res;
            }
            return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(Ꮡnode, expression, nil, nil, args, node.Flags);
        }
    }
    return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitParenthesizedExpression(this ref optionalChainTransformer ch, ж<ast.ParenthesizedExpression> Ꮡnode, bool captureThisArg, bool isDelete) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = ch.visitNonOptionalExpression(node.Expression, captureThisArg, isDelete);
    if (ast.IsSyntheticReferenceExpression(expr)) {
        var synth = expr.AsSyntheticReferenceExpression();
        var res = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntheticReferenceExpression(ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(Ꮡnode, (~synth).Expression), synth.ThisArg);
        ch.Transformer.EmitContext().SetOriginal(res, Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return res;
    }
    return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(Ꮡnode, expr);
}

internal static ж<astꓸExpression> visitPropertyOrElementAccessExpression(this ref optionalChainTransformer ch, ж<astꓸExpression> Ꮡnode, bool captureThisArg, bool isDelete) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0) {
        return ch.visitOptionalExpression(Ꮡnode.AsNode(), captureThisArg, isDelete);
    }
    var expression = ch.Transformer.Visitor().VisitNode(node.Expression());
    debug.Assert(expression == nil || !ast.IsSyntheticReferenceExpression(expression));

    ж<astꓸExpression> thisArg = default;
    if (captureThisArg) {
        if (!transformers.IsSimpleCopiableExpression(expression)) {
            thisArg = ch.Transformer.Factory().NewTempVariable();
            ch.Transformer.EmitContext().AddVariableDeclaration(thisArg);
            expression = ch.Transformer.Factory().NewAssignmentExpression(thisArg, expression);
        } else {
            thisArg = expression;
        }
    }

    if (node.Kind == ast.KindPropertyAccessExpression) {
        var p = node.AsPropertyAccessExpression();
        expression = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAccessExpression(p, expression, nil, ch.Transformer.Visitor().VisitNode(p.Name()), p.Flags);
    } else {
        var p = node.AsElementAccessExpression();
        expression = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateElementAccessExpression(p, expression, nil, ch.Transformer.Visitor().VisitNode(p.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).AsElementAccessExpression().ArgumentExpression), p.Flags);
    }

    if (thisArg != nil) {
        var res = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntheticReferenceExpression(expression, thisArg);
        ch.Transformer.EmitContext().SetOriginal(res, Ꮡnode.AsNode());
        return res;
    }
    return expression;
}

internal static ж<ast.Node> visitDeleteExpression(this ref optionalChainTransformer ch, ж<ast.DeleteExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var unwrapped = ast.SkipParentheses(node.Expression);
    if ((ast.NodeFlags)(unwrapped.Flags & ast.NodeFlagsOptionalChain) != 0) {
        return ch.visitNonOptionalExpression(node.Expression, false, true);
    }
    return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.DeleteExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<astꓸExpression> visitNonOptionalExpression(this ref optionalChainTransformer ch, ж<astꓸExpression> Ꮡnode, bool captureThisArg, bool isDelete) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return ch.visitParenthesizedExpression(node.AsParenthesizedExpression(), captureThisArg, isDelete);
    }
    case var ᴛ when ᴛ == ast.KindElementAccessExpression || ᴛ == ast.KindPropertyAccessExpression: {
        return ch.visitPropertyOrElementAccessExpression(Ꮡnode, captureThisArg, isDelete);
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        return ch.visitCallExpression(node.AsCallExpression(), captureThisArg);
    }
    default: {
        return ch.Transformer.Visitor().VisitNode(Ꮡnode.AsNode());
    }}

}

} partial class estransforms_package {

partial struct flattenResult {
    internal ж<astꓸExpression> expression;
    internal slice<ж<ast.Node>> chain;
}

internal static bool isNonNullChain(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsNonNullExpression(Ꮡnode) && (ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0;
}

internal static flattenResult flattenChain(ж<ast.Node> Ꮡchain) {
    ref var chain = ref Ꮡchain.DerefOrNull();

    debug.Assert(!isNonNullChain(Ꮡchain));
    var links = slice<ж<ast.Node>>([Ꮡchain]);
    while (!ast.IsTaggedTemplateExpression(Ꮡchain) && chain.QuestionDotToken() == nil) {
        Ꮡchain = ast.SkipPartiallyEmittedExpressions(chain.Expression()); chain = ref Ꮡchain.DerefOrNull();
        debug.Assert(!isNonNullChain(Ꮡchain));
        links = appendꓸꓸꓸ(slice<ж<ast.Node>>([Ꮡchain]), links);
    }
    return new flattenResult(chain.Expression(), links);
}

internal static bool isCallChain(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsCallExpression(Ꮡnode) && (ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0;
}

} partial class estransformsᴛoptionalChainTransformer {

internal static ж<ast.Node> visitOptionalExpression(this ref optionalChainTransformer ch, ж<ast.Node> Ꮡnode, bool captureThisArg, bool isDelete) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var r = flattenChain(Ꮡnode);
    var expression = r.expression;
    var chain = r.chain;
    var left = ch.visitNonOptionalExpression(ast.SkipPartiallyEmittedExpressions(expression), isCallChain(chain[0]), false);
    ж<astꓸExpression> leftThisArg = default;
    var capturedLeft = left;
    if (ast.IsSyntheticReferenceExpression(left)) {
        leftThisArg = left.AsSyntheticReferenceExpression().ThisArg;
        capturedLeft = left.AsSyntheticReferenceExpression().Value.Expression;
    }
    var leftExpression = ch.Transformer.Factory().RestoreOuterExpressions(expression, capturedLeft, ast.OEKPartiallyEmittedExpressions);
    if (!transformers.IsSimpleCopiableExpression(capturedLeft)) {
        capturedLeft = ch.Transformer.Factory().NewTempVariable();
        ch.Transformer.EmitContext().AddVariableDeclaration(capturedLeft);
        leftExpression = ch.Transformer.Factory().NewAssignmentExpression(capturedLeft, leftExpression);
    }
    var rightExpression = capturedLeft;
    ж<astꓸExpression> thisArg = default;

    foreach (var (i, segment) in range(chain)) {
        switch (segment.Kind) {
        case var ᴛ when ᴛ == ast.KindElementAccessExpression || ᴛ == ast.KindPropertyAccessExpression: {
            if (i == len(chain) - 1 && captureThisArg) {
                if (!transformers.IsSimpleCopiableExpression(rightExpression)) {
                    thisArg = ch.Transformer.Factory().NewTempVariable();
                    ch.Transformer.EmitContext().AddVariableDeclaration(thisArg);
                    rightExpression = ch.Transformer.Factory().NewAssignmentExpression(thisArg, rightExpression);
                } else {
                    thisArg = rightExpression;
                }
            }
            if (segment.Kind == ast.KindElementAccessExpression) {
                rightExpression = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(rightExpression, nil, ch.Transformer.Visitor().VisitNode(segment.AsElementAccessExpression().ArgumentExpression), ast.NodeFlagsNone);
            } else {
                rightExpression = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(rightExpression, nil, ch.Transformer.Visitor().VisitNode(segment.AsPropertyAccessExpression().Name()), ast.NodeFlagsNone);
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindCallExpression: {
            if (i == 0 && leftThisArg != nil) {
                if (!ch.Transformer.EmitContext().HasAutoGenerateInfo(leftThisArg)) {
                    leftThisArg = leftThisArg.Clone(new printer.NodeFactoryжNodeFactoryCoercible(ch.Transformer.Factory()));
                    ch.Transformer.EmitContext().AddEmitFlags(leftThisArg, printer.EFNoComments);
                }
                var callThisArg = leftThisArg;
                if (leftThisArg.Kind == ast.KindSuperKeyword) {
                    callThisArg = ch.Transformer.Factory().NewThisExpression();
                }
                rightExpression = ch.Transformer.Factory().NewFunctionCallCall(rightExpression, callThisArg, ch.Transformer.Visitor().VisitNodes(segment.ArgumentList()).Nodes);
            } else {
                rightExpression = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
                    rightExpression,
                    nil,
                    nil,
                    ch.Transformer.Visitor().VisitNodes(segment.ArgumentList()),
                    ast.NodeFlagsNone);
            }
            break;
        }}

        ch.Transformer.EmitContext().SetOriginal(rightExpression, segment);
    }

    ж<ast.Node> target = default;
    if (isDelete) {
        target = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
            createNotNullCondition(ref (ch.Transformer.EmitContext()).DerefOrNull(), leftExpression, capturedLeft, true),
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
            ch.Transformer.Factory().NewTrueExpression(),
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewDeleteExpression(rightExpression));
    } else {
        target = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
            createNotNullCondition(ref (ch.Transformer.EmitContext()).DerefOrNull(), leftExpression, capturedLeft, true),
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
            ch.Transformer.Factory().NewVoidZeroExpression(),
            ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
            rightExpression);
    }
    target.Loc = node.Loc;
    if (thisArg != nil) {
        target = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntheticReferenceExpression(target, thisArg);
    }
    ch.Transformer.EmitContext().SetOriginal(target, Ꮡnode.AsNode());
    return target;
}

} partial class estransforms_package {

internal static ж<transformers.Transformer> newOptionalChainTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new optionalChainTransformer());
    return tx.of(optionalChainTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} // end estransforms_package
