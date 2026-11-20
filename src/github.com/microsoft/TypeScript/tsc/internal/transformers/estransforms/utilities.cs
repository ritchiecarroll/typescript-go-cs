namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using static estransforms_package;

partial class estransforms_package {

internal static ж<astꓸExpression> convertClassDeclarationToClassExpression(ж<printer.EmitContext> ᏑemitContext, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var updated = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassExpression(
        transformers.ExtractModifiers(ᏑemitContext, Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), ~((ast.ModifierFlags)ast.ModifierFlagsExportDefault)),
        node.Name(),
        node.TypeParameters,
        node.HeritageClauses,
        node.Members);
    emitContext.SetOriginal(updated, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    updated.Loc = node.Loc;
    return updated;
}

internal static ж<ast.Node> createNotNullCondition(ref printer.EmitContext emitContext, ж<ast.Node> left, ж<ast.Node> right, bool invert) {
    var token = ast.KindExclamationEqualsEqualsToken;
    var op = ast.KindAmpersandAmpersandToken;
    if (invert) {
        token = ast.KindEqualsEqualsEqualsToken;
        op = ast.KindBarBarToken;
    }

    return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
        nil,
        emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
            nil,
            left,
            nil,
            emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(token),
            emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindNullKeyword)),
        nil,
        emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(op),
        emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
            nil,
            right,
            nil,
            emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(token),
            emitContext.Factory.NewVoidZeroExpression()));
}

partial struct superAccessState {
    internal ж<printer.NodeFactory> factory;
    internal ж<collections.OrderedSet<@string>> capturedSuperProperties;
    internal bool hasSuperElementAccess;
    internal bool hasSuperPropertyAssignment;
    internal ж<astꓸIdentifierNode> superBinding;
    internal ж<astꓸIdentifierNode> superIndexBinding;
    internal ж<ast.NodeVisitor> superAccessVisitor;
}

} partial class estransformsᴛsuperAccessState {

internal static void initSuperAccessVisitor(this ж<superAccessState> Ꮡs, ж<printer.EmitContext> emitContext, ж<printer.NodeFactory> Ꮡfactory) {
    ref var s = ref Ꮡs.DerefOrNull();
    ref var factory = ref Ꮡfactory.DerefOrNull();

    s.factory = Ꮡfactory;
    s.superAccessVisitor = emitContext.NewNodeVisitor(Ꮡs.visitSuperAccessNode);
}

internal static ж<ast.Node> visitSuperAccessNode(this ref superAccessState s, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        var call = node.AsCallExpression();
        if (ast.IsSuperProperty((~call).Expression)) {
            return s.substituteCallExpressionWithSuperAccess(call, s.superAccessVisitor);
        }
        return s.superAccessVisitor.VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        if (node.Expression().Kind == ast.KindSuperKeyword) {
            return s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                s.superBinding, nil, node.Name(), ast.NodeFlagsNone);
        }
        return s.superAccessVisitor.VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
        if (node.Expression().Kind == ast.KindSuperKeyword) {
            return s.createSuperElementAccessInAsyncMethod(
                node.AsElementAccessExpression().ArgumentExpression);
        }
        return s.superAccessVisitor.VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindConstructor || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression: {
        return Ꮡnode;
    }
    default: {
        return s.superAccessVisitor.VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> substituteSuperAccessesInBody(this ref superAccessState s, ж<ast.Node> body) {
    return s.superAccessVisitor.VisitNode(body);
}

internal static ж<ast.Node> substituteCallExpressionWithSuperAccess(this ref superAccessState s, ж<ast.CallExpression> Ꮡcall, ж<ast.NodeVisitor> Ꮡvisitor) {
    ref var call = ref Ꮡcall.DerefOrNull();
    ref var visitor = ref Ꮡvisitor.DerefOrNull();

    var expression = call.Expression;
    ж<ast.Node> target = default;

    if (ast.IsPropertyAccessExpression(expression)) {
        target = s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            s.superBinding, nil,
            expression.AsPropertyAccessExpression().Name(), ast.NodeFlagsNone);
    } else if (ast.IsElementAccessExpression(expression)) {
        target = s.createSuperElementAccessInAsyncMethod(
            expression.AsElementAccessExpression().ArgumentExpression);
    } else {
        return Ꮡvisitor.VisitEachChild(Ꮡcall.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var callTarget = s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
        target, nil,
        s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("call"), ast.NodeFlagsNone);

    slice<ж<ast.Node>> allArgs = default;
    allArgs = append(allArgs, s.factory.NewThisExpression());
    if (call.Arguments != nil) {
        var visitedArgs = visitor.VisitNodes(call.Arguments);
        if (visitedArgs != nil) {
            allArgs = appendꓸꓸꓸ(allArgs, visitedArgs.Nodes);
        }
    }

    var result = s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        callTarget, nil, nil,
        s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(allArgs), ast.NodeFlagsNone);
    result.Loc = call.Loc;
    return result;
}

internal static ж<ast.Node> createSuperElementAccessInAsyncMethod(this ref superAccessState s, ж<ast.Node> argumentExpression) {
    var superIndexCall = s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        s.superIndexBinding, nil, nil,
        s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([argumentExpression]),
        ast.NodeFlagsNone);
    if (s.hasSuperPropertyAssignment) {
        return s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            superIndexCall, nil,
            s.factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), ast.NodeFlagsNone);
    }
    return superIndexCall;
}

internal static ж<ast.Node> createSuperAccessVariableStatement(this ref superAccessState s) {
    var f = s.factory;
    slice<ж<ast.Node>> accessors = default;

    foreach (var name in range<@string>(s.capturedSuperProperties.Values().Invoke)) {
        slice<ж<ast.Node>> descriptorProperties = default;

        var getterBody = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindSuperKeyword), nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(name), ast.NodeFlagsNone);
        var getterArrow = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrowFunction(
            nil, nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
            nil, nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsGreaterThanToken),
            getterBody);
        var getter = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("get"), nil, nil, getterArrow);
        descriptorProperties = append(descriptorProperties, getter);

        if (s.hasSuperPropertyAssignment) {
            var vParam = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(nil, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("v"), nil, nil, nil);
            var superProp = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindSuperKeyword), nil,
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(name), ast.NodeFlagsNone);
            var assignExpr = f.NewAssignmentExpression(superProp, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("v"));
            var setterArrow = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrowFunction(
                nil, nil,
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([vParam]),
                nil, nil,
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsGreaterThanToken),
                assignExpr);
            var setter = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("set"), nil, nil, setterArrow);
            descriptorProperties = append(descriptorProperties, setter);
        }

        var descriptor = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(descriptorProperties), false);
        var accessor = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(name), nil, nil, descriptor);
        accessors = append(accessors, accessor);
    }

    var descriptorsObject = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(accessors), true);

    var objectCreateCall = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object"), nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("create"), ast.NodeFlagsNone), nil, nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindNullKeyword),
            descriptorsObject
        ])),
        ast.NodeFlagsNone);

    var decl = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(s.superBinding, nil, nil, objectCreateCall);
    var declList = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([decl]), ast.NodeFlagsConst);
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, declList);
}

internal static void trackSuperAccess(this ref superAccessState s, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (s.capturedSuperProperties == nil) {
        return;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        if (node.Expression().Kind == ast.KindSuperKeyword) {
            s.capturedSuperProperties.Add(node.Name().Text());
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
        if (node.Expression().Kind == ast.KindSuperKeyword) {
            s.hasSuperElementAccess = true;
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        if (ast.IsAssignmentOperator(node.AsBinaryExpression().OperatorToken.Kind) && assignmentTargetContainsSuperProperty(node.AsBinaryExpression().Left)) {
            s.hasSuperPropertyAssignment = true;
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        if (isUpdateExpression(Ꮡnode) && assignmentTargetContainsSuperProperty(node.AsPrefixUnaryExpression().Operand)) {
            s.hasSuperPropertyAssignment = true;
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindPostfixUnaryExpression: {
        if (isUpdateExpression(Ꮡnode) && assignmentTargetContainsSuperProperty(node.AsPostfixUnaryExpression().Operand)) {
            s.hasSuperPropertyAssignment = true;
        }
        break;
    }}

}

} partial class estransforms_package {

internal static ж<ast.Node> createAccessorPropertyBackingField(ж<printer.NodeFactory> Ꮡf, ж<ast.PropertyDeclaration> Ꮡnode, ж<ast.ModifierList> modifiers, ж<astꓸExpression> initializer) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
        Ꮡnode,
        modifiers,
        f.NewGeneratedPrivateNameForNodeEx(node.Name(), new printer.AutoGenerateOptions(Suffix: "_accessor_storage")),
        nil,
        nil,
        initializer);
}

} // end estransforms_package
