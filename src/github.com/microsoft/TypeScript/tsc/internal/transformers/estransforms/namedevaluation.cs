global using anonymousFunctionDefinition = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;

namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class estransforms_package {

internal static bool isClassNamedEvaluationHelperBlock(ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> Ꮡnode) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsClassStaticBlockDeclaration(Ꮡnode) || len((~node.AsClassStaticBlockDeclaration()).Body.Statements()) != 1) {
        return false;
    }

    var statement = (~node.AsClassStaticBlockDeclaration()).Body.Statements()[0];
    if (ast.IsExpressionStatement(statement)) {
        var expression = statement.Expression();
        if (emitContext.IsCallToHelper(expression, "__setFunctionName")) {
            var arguments = expression.AsCallExpression().Value.Arguments;
            return len(arguments.Nodes) >= 2 && arguments.Nodes[1] == emitContext.AssignedName(Ꮡnode.AsNode());
        }
    }
    return false;
}

internal static bool classHasExplicitlyAssignedName(ж<printer.EmitContext> ᏑemitContext, ж<astꓸClassLikeDeclaration> Ꮡnode) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var assignedName = emitContext.AssignedName(Ꮡnode); if (assignedName != nil) {
        foreach (var (_, member) in range(node.Members())) {
            if (isClassNamedEvaluationHelperBlock(ᏑemitContext, member)) {
                return true;
            }
        }
    }
    return false;
}

internal static bool classHasDeclaredOrExplicitlyAssignedName(ж<printer.EmitContext> emitContext, ж<astꓸClassLikeDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Name() != nil || classHasExplicitlyAssignedName(emitContext, Ꮡnode);
}

internal static bool isAnonymousFunctionDefinition(ж<printer.EmitContext> emitContext, ж<astꓸExpression> Ꮡnode, Func<ж<anonymousFunctionDefinition>, bool> cb) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = ast.SkipOuterExpressions(Ꮡnode, ast.OEKAll); node = ref Ꮡnode.DerefOrNull();
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindClassExpression: {
        if (classHasDeclaredOrExplicitlyAssignedName(emitContext, Ꮡnode)) {
            return false;
        }
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionExpression: {
        if (node.AsFunctionExpression().Name() != nil) {
            return false;
        }
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrowFunction: {
        break;
        break;
    }
    default: {
        return false;
    }}

    if (cb != nil) {
        return cb(Ꮡnode);
    }
    return true;
}

internal static bool isNamedEvaluation(ж<printer.EmitContext> emitContext, ж<ast.Node> node) {
    return isNamedEvaluationAnd(emitContext, node, default);
}

internal static bool isNamedEvaluationAnd(ж<printer.EmitContext> emitContext, ж<ast.Node> Ꮡnode, Func<ж<anonymousFunctionDefinition>, bool> cb) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsNamedEvaluationSource(Ꮡnode)) {
        return false;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        return isAnonymousFunctionDefinition(emitContext, node.AsShorthandPropertyAssignment().ObjectAssignmentInitializer, cb);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindParameter || ᴛ == ast.KindBindingElement || ᴛ == ast.KindPropertyDeclaration: {
        return isAnonymousFunctionDefinition(emitContext, node.Initializer(), cb);
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return isAnonymousFunctionDefinition(emitContext, node.AsBinaryExpression().Right, cb);
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return isAnonymousFunctionDefinition(emitContext, node.Expression(), cb);
    }
    default: {
        debug.Fail("Unhandled case in isNamedEvaluation");
        return false;
    }}

}

internal static ж<astꓸStringLiteralNode> getAssignedNameOfIdentifier(ж<printer.EmitContext> ᏑemitContext, ж<astꓸIdentifierNode> name, ж<ast.Node> expression) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    var original = emitContext.MostOriginal(ast.SkipOuterExpressions(expression, ast.OEKAll));
    if ((ast.IsClassDeclaration(original) || ast.IsFunctionDeclaration(original)) && original.Name() == nil && ast.HasSyntacticModifier(original, ast.ModifierFlagsDefault)) {
        return emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("default", ast.TokenFlagsNone);
    }
    return emitContext.Factory.NewStringLiteralFromNode(name);
}

internal static (ж<astꓸExpression> assignedName, ж<astꓸPropertyName> updatedName) getAssignedNameOfPropertyName(ж<printer.EmitContext> ᏑemitContext, ж<astꓸPropertyName> Ꮡname, @string assignedNameText) {
    ж<astꓸExpression> assignedName = default;
    ж<astꓸPropertyName> updatedName = default;
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    var factory = emitContext.Factory;
    if (len(assignedNameText) > 0) {
        var assignedNameΔ1 = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(assignedNameText, ast.TokenFlagsNone);
        return (assignedNameΔ1, Ꮡname);
    }

    if (ast.IsPropertyNameLiteral(Ꮡname) || ast.IsPrivateIdentifier(Ꮡname)) {
        var assignedNameΔ2 = factory.NewStringLiteralFromNode(Ꮡname);
        return (assignedNameΔ2, Ꮡname);
    }

    var expression = name.Expression();
    if (ast.IsPropertyNameLiteral(expression) && !ast.IsIdentifier(expression)) {
        var assignedNameΔ3 = factory.NewStringLiteralFromNode(expression);
        return (assignedNameΔ3, Ꮡname);
    }

    debug.Assert(ast.IsComputedPropertyName(Ꮡname), "Expected computed property name");

    assignedName = factory.NewGeneratedNameForNode(Ꮡname);
    emitContext.AddVariableDeclaration(assignedName);

    var key = factory.NewPropKeyHelper(expression);
    var assignment = factory.NewAssignmentExpression(assignedName, key);
    updatedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(name.AsComputedPropertyName(), assignment);
    return (assignedName, updatedName);
}

internal static ж<ast.Node> createClassNamedEvaluationHelperBlock(ж<printer.EmitContext> ᏑemitContext, ж<astꓸExpression> assignedName, ж<astꓸExpression> ᏑthisExpression) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var thisExpression = ref ᏑthisExpression.DerefOrNull();

    if (ᏑthisExpression == nil) {
        ᏑthisExpression = emitContext.Factory.NewThisExpression(); thisExpression = ref ᏑthisExpression.DerefOrNull();
    }

    var factory = emitContext.Factory;
    var expression = factory.NewSetFunctionNameHelper(ᏑthisExpression, assignedName, "");
    var statement = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
    var body = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([statement]), false);
    var block = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(nil, body);

    emitContext.SetAssignedName(block, assignedName);
    return block.AsNode();
}

internal static ж<astꓸClassLikeDeclaration> injectClassNamedEvaluationHelperBlockIfMissing(ж<printer.EmitContext> ᏑemitContext, ж<astꓸClassLikeDeclaration> Ꮡnode, ж<astꓸExpression> assignedName, ж<astꓸExpression> thisExpression) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (classHasExplicitlyAssignedName(ᏑemitContext, Ꮡnode)) {
        return Ꮡnode;
    }

    var factory = emitContext.Factory;
    var namedEvaluationBlock = createClassNamedEvaluationHelperBlock(ᏑemitContext, assignedName, thisExpression);
    if (node.Name() != nil) {
        emitContext.SetSourceMapRange(namedEvaluationBlock.Body().Statements()[0], node.Name().Loc);
    }

    nint insertionIndex = slices.IndexFunc(node.Members(), (ж<ast.Node> n) => isClassThisAssignmentBlock(ᏑemitContext, n)) + 1;
    var leading = slices.Clone<slice<ж<ast.Node>>, ж<ast.Node>>(node.Members().slice(0, insertionIndex));
    var trailing = slices.Clone<slice<ж<ast.Node>>, ж<ast.Node>>(node.Members().slice(insertionIndex));

    slice<ж<astꓸClassElement>> members = default;
    members = appendꓸꓸꓸ(members, leading);
    members = append(members, namedEvaluationBlock);
    members = appendꓸꓸꓸ(members, trailing);
    var membersList = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(members);
    membersList.Loc = node.MemberList().Loc;

    var oldNode = Ꮡnode;
    if (ast.IsClassDeclaration(Ꮡnode)) {
        Ꮡnode = factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
            node.AsClassDeclaration(),
            node.Modifiers(),
            node.Name(),
            node.TypeParameterList(),
            node.AsClassDeclaration().HeritageClauses,
            membersList); node = ref Ꮡnode.DerefOrNull();
    } else {
        Ꮡnode = factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(
            node.AsClassExpression(),
            node.Modifiers(),
            node.Name(),
            node.TypeParameterList(),
            node.AsClassExpression().HeritageClauses,
            membersList); node = ref Ꮡnode.DerefOrNull();
    }

    emitContext.SetAssignedName(Ꮡnode, assignedName);

    var ct = emitContext.ClassThis(oldNode); if (ct != nil) {
        emitContext.SetClassThis(Ꮡnode, ct);
    }

    return Ꮡnode;
}

internal static ж<astꓸExpression> finishTransformNamedEvaluation(ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> expression, ж<astꓸExpression> ᏑassignedName, bool ignoreEmptyStringLiteral) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var assignedName = ref ᏑassignedName.DerefOrNull();

    if (ignoreEmptyStringLiteral && ast.IsStringLiteral(ᏑassignedName) && len(assignedName.Text()) == 0) {
        return expression;
    }

    var factory = emitContext.Factory;
    var innerExpression = ast.SkipOuterExpressions(expression, ast.OEKAll);

    ж<astꓸExpression> updatedExpression = default;
    if (ast.IsClassExpression(innerExpression)) {
        updatedExpression = injectClassNamedEvaluationHelperBlockIfMissing(ᏑemitContext, innerExpression, ᏑassignedName, nil);
    } else {
        updatedExpression = factory.NewSetFunctionNameHelper(innerExpression, ᏑassignedName, "");
    }

    return factory.RestoreOuterExpressions(expression, updatedExpression, ast.OEKAll);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfPropertyAssignment(ж<printer.EmitContext> Ꮡcontext, ж<ast.PropertyAssignment> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var context = ref Ꮡcontext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = context.Factory;
    var (assignedName, name) = getAssignedNameOfPropertyName(Ꮡcontext, node.Name(), assignedNameText);
    var initializer = finishTransformNamedEvaluation(Ꮡcontext, node.Initializer, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAssignment(Ꮡnode, nil, name, nil, nil, initializer);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfShorthandAssignmentProperty(ж<printer.EmitContext> ᏑemitContext, ж<ast.ShorthandPropertyAssignment> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = emitContext.Factory;
    ж<astꓸExpression> assignedName = default;
    if (len(assignedNameText) > 0) {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(assignedNameText, ast.TokenFlagsNone);
    } else {
        assignedName = getAssignedNameOfIdentifier(ᏑemitContext, node.Name(), node.ObjectAssignmentInitializer);
    }
    var objectAssignmentInitializer = finishTransformNamedEvaluation(ᏑemitContext, node.ObjectAssignmentInitializer, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateShorthandPropertyAssignment(
        Ꮡnode,
        nil,
        node.Name(),
        nil,
        nil,
        node.EqualsToken,
        objectAssignmentInitializer);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfVariableDeclaration(ж<printer.EmitContext> ᏑemitContext, ж<ast.VariableDeclaration> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = emitContext.Factory;
    ж<astꓸExpression> assignedName = default;
    if (len(assignedNameText) > 0) {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(assignedNameText, ast.TokenFlagsNone);
    } else {
        assignedName = getAssignedNameOfIdentifier(ᏑemitContext, node.Name(), node.Initializer);
    }
    var initializer = finishTransformNamedEvaluation(ᏑemitContext, node.Initializer, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(
        Ꮡnode,
        node.Name(),
        nil,
        nil,
        initializer);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfParameterDeclaration(ж<printer.EmitContext> ᏑemitContext, ж<ast.ParameterDeclaration> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = emitContext.Factory;
    ж<astꓸExpression> assignedName = default;
    if (len(assignedNameText) > 0) {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(assignedNameText, ast.TokenFlagsNone);
    } else {
        assignedName = getAssignedNameOfIdentifier(ᏑemitContext, node.Name(), node.Initializer);
    }
    var initializer = finishTransformNamedEvaluation(ᏑemitContext, node.Initializer, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
        Ꮡnode,
        nil,
        node.DotDotDotToken,
        node.Name(),
        nil,
        nil,
        initializer);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfBindingElement(ж<printer.EmitContext> ᏑemitContext, ж<ast.BindingElement> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = emitContext.Factory;
    ж<astꓸExpression> assignedName = default;
    if (len(assignedNameText) > 0) {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(assignedNameText, ast.TokenFlagsNone);
    } else {
        assignedName = getAssignedNameOfIdentifier(ᏑemitContext, node.Name(), node.Initializer);
    }
    var initializer = finishTransformNamedEvaluation(ᏑemitContext, node.Initializer, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBindingElement(
        Ꮡnode,
        node.DotDotDotToken,
        node.PropertyName,
        node.Name(),
        initializer);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfPropertyDeclaration(ж<printer.EmitContext> ᏑemitContext, ж<ast.PropertyDeclaration> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = emitContext.Factory;
    var (assignedName, name) = getAssignedNameOfPropertyName(ᏑemitContext, node.Name(), assignedNameText);
    var initializer = finishTransformNamedEvaluation(ᏑemitContext, node.Initializer, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
        Ꮡnode,
        Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers(),
        name,
        nil,
        nil,
        initializer);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfAssignmentExpression(ж<printer.EmitContext> ᏑemitContext, ж<ast.BinaryExpression> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = emitContext.Factory;
    ж<astꓸExpression> assignedName = default;
    if (len(assignedNameText) > 0) {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(assignedNameText, ast.TokenFlagsNone);
    } else {
        assignedName = getAssignedNameOfIdentifier(ᏑemitContext, node.Left, node.Right);
    }
    var right = finishTransformNamedEvaluation(ᏑemitContext, node.Right, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(
        Ꮡnode,
        nil,
        node.Left,
        nil,
        node.OperatorToken,
        right);
}

internal static ж<astꓸExpression> transformNamedEvaluationOfExportAssignment(ж<printer.EmitContext> ᏑemitContext, ж<ast.ExportAssignment> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedNameText) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var factory = emitContext.Factory;
    ж<astꓸExpression> assignedName = default;
    if (len(assignedNameText) > 0) {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(assignedNameText, ast.TokenFlagsNone);
    } else if (node.IsExportEquals) {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("", ast.TokenFlagsNone);
    } else {
        assignedName = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("default", ast.TokenFlagsNone);
    }
    var expression = finishTransformNamedEvaluation(ᏑemitContext, node.Expression, assignedName, ignoreEmptyStringLiteral);
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExportAssignment(
        Ꮡnode,
        nil,
        node.IsExportEquals,
        nil,
        expression);
}

internal static ж<astꓸExpression> transformNamedEvaluation(ж<printer.EmitContext> context, ж<ast.Node> Ꮡnode, bool ignoreEmptyStringLiteral, @string assignedName) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
        return transformNamedEvaluationOfPropertyAssignment(context, node.AsPropertyAssignment(), ignoreEmptyStringLiteral, assignedName);
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        return transformNamedEvaluationOfShorthandAssignmentProperty(context, node.AsShorthandPropertyAssignment(), ignoreEmptyStringLiteral, assignedName);
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        return transformNamedEvaluationOfVariableDeclaration(context, node.AsVariableDeclaration(), ignoreEmptyStringLiteral, assignedName);
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        return transformNamedEvaluationOfParameterDeclaration(context, node.AsParameterDeclaration(), ignoreEmptyStringLiteral, assignedName);
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        return transformNamedEvaluationOfBindingElement(context, node.AsBindingElement(), ignoreEmptyStringLiteral, assignedName);
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return transformNamedEvaluationOfPropertyDeclaration(context, node.AsPropertyDeclaration(), ignoreEmptyStringLiteral, assignedName);
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return transformNamedEvaluationOfAssignmentExpression(context, node.AsBinaryExpression(), ignoreEmptyStringLiteral, assignedName);
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return transformNamedEvaluationOfExportAssignment(context, node.AsExportAssignment(), ignoreEmptyStringLiteral, assignedName);
    }
    default: {
        debug.Fail("Unhandled case in transformNamedEvaluation");
        return Ꮡnode;
    }}

}

} // end estransforms_package
