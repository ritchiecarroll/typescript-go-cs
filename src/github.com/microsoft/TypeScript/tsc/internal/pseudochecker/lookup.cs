namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static pseudochecker_package;

partial class pseudochecker_package {

} partial class pseudocheckerᴛPseudoChecker {

public static ж<PseudoType> GetReturnTypeOfSignature(this ref PseudoChecker ch, ж<ast.Node> ᏑsignatureNode) {
    ref var signatureNode = ref ᏑsignatureNode.DerefOrNull();

    switch (signatureNode.Kind) {
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        return ch.GetTypeOfAccessor(ᏑsignatureNode);
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindConstructor || ᴛ == ast.KindMethodSignature || ᴛ == ast.KindCallSignature || ᴛ == ast.KindConstructSignature || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindIndexSignature || ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindJSDocSignature: {
        return ch.createReturnFromSignature(ᏑsignatureNode);
    }
    default: {
        debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(ᏑsignatureNode), "Node needs to be an inferrable node");
        return default;
    }}

}

public static ж<PseudoType> GetTypeOfAccessor(this ref PseudoChecker ch, ж<ast.Node> accessor) {
    return ch.typeFromAccessor(accessor);
}

public static ж<PseudoType> GetTypeOfExpression(this ref PseudoChecker ch, ж<ast.Node> node) {
    return ch.typeFromExpression(node);
}

public static ж<PseudoType> GetTypeOfDeclaration(this ref PseudoChecker ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindParameter: {
        return ch.typeFromParameter(node.AsParameterDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        return ch.typeFromVariable(node.AsVariableDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindPropertySignature || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindJSDocPropertyTag: {
        return ch.typeFromProperty(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        return NewPseudoTypeNoResult(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return ch.typeFromExpression((~node.AsExportAssignment()).Expression);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression || ᴛ == ast.KindBinaryExpression: {
        return ch.typeFromExpandoProperty(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindShorthandPropertyAssignment: {
        return ch.typeFromPropertyAssignment(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        switch (ast.GetAssignmentDeclarationKind(Ꮡnode)) {
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindObjectDefinePropertyValue: {
            {
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.JSDeclarationKindObjectDefinePropertyExports: {
            {
            }
            break;
        }}

        return NewPseudoTypeNoResult(Ꮡnode);
    }
    default: {
        debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(Ꮡnode), "node needs to be an inferrable node");
        return default;
    }}

}

internal static ж<PseudoType> typeFromPropertyAssignment(this ref PseudoChecker ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var annotation = node.Type();
    if (annotation != nil) {
        return NewPseudoTypeDirect(annotation);
    }
    if (node.Kind == ast.KindPropertyAssignment) {
        var init = node.Initializer();
        if (init != nil) {
            var expr = ch.typeFromExpression(init);
            if (expr != nil && (expr.Kind != PseudoTypeKindInferred || len(expr.AsPseudoTypeInferred().ErrorNodes) > 0)) {
                return expr;
            }
        }
    }
    return NewPseudoTypeNoResult(Ꮡnode);
}

internal static ж<PseudoType> typeFromExpandoProperty(this ref PseudoChecker ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var declaredType = node.Type();
    if (declaredType != nil) {
        return NewPseudoTypeDirect(declaredType);
    }
    return NewPseudoTypeNoResult(Ꮡnode);
}

internal static ж<PseudoType> typeFromProperty(this ref PseudoChecker ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var t = node.Type();
    if (t != nil) {
        return NewPseudoTypeDirect(t);
    }
    if (ast.IsPropertyDeclaration(Ꮡnode)) {
        var init = node.Initializer();
        if (init != nil && !isContextuallyTyped(ref (Ꮡnode).DerefOrNull())) {
            if (ast.HasModifier(Ꮡnode, ast.ModifierFlagsReadonly) && ast.IsTemplateExpression(init)) {
                return NewPseudoTypeNoResult(Ꮡnode);
            }
            var expr = ch.typeFromExpression(init);
            if (expr != nil && (expr.Kind != PseudoTypeKindInferred || len(expr.AsPseudoTypeInferred().ErrorNodes) > 0)) {
                if (expr.Kind != PseudoTypeKindDirect && (~node.AsPropertyDeclaration()).PostfixToken != nil && (~node.AsPropertyDeclaration()).PostfixToken.Kind == ast.KindQuestionToken) {
                    return addUndefinedIfDefinitelyRequired(expr);
                }
                return expr;
            }
        }
    }
    return NewPseudoTypeNoResult(Ꮡnode);
}

internal static ж<PseudoType> typeFromVariable(this ref PseudoChecker ch, ж<ast.VariableDeclaration> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    var t = declaration.Type;
    if (t != nil) {
        return NewPseudoTypeDirect(t);
    }
    var init = declaration.Initializer;
    if (init != nil && declaration.Symbol != nil && (len(declaration.Symbol.Declarations) == 1 || core.CountWhere<ж<ast.Node>>(declaration.Symbol.Declarations, ast.IsVariableDeclaration) == 1)) {
        if (!isContextuallyTyped(ref (Ꮡdeclaration.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).DerefOrNull())) {
            if (ast.IsVarConst(Ꮡdeclaration.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && ast.IsTemplateExpression(init)) {
                return NewPseudoTypeNoResult(Ꮡdeclaration.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            }
            var expr = ch.typeFromExpression(init);
            if (expr != nil && (expr.Kind != PseudoTypeKindInferred || len(expr.AsPseudoTypeInferred().ErrorNodes) > 0)) {
                return expr;
            }
        }
    }
    return NewPseudoTypeNoResult(Ꮡdeclaration.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<PseudoType> typeFromAccessor(this ref PseudoChecker ch, ж<ast.Node> Ꮡaccessor) {
    ref var accessor = ref Ꮡaccessor.DerefOrNull();

    var accessorDeclarations = ast.GetAllAccessorDeclarationsForDeclaration(Ꮡaccessor, accessor.DeclarationData().Symbol.Declarations);
    var accessorType = ch.getTypeAnnotationFromAllAccessorDeclarations(Ꮡaccessor, accessorDeclarations);
    if (accessorType != nil && !ast.IsTypePredicateNode(accessorType)) {
        return NewPseudoTypeDirect(accessorType);
    }
    if (accessorDeclarations.GetAccessor != nil) {
        var res = ch.createReturnFromSignature(accessorDeclarations.GetAccessor.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (res.Kind == PseudoTypeKindInferred && len(res.AsPseudoTypeInferred().ErrorNodes) == 0) {
            var errorNodes = slice<ж<ast.Node>>([accessorDeclarations.GetAccessor.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()]);
            if (accessorDeclarations.SetAccessor != nil) {
                errorNodes = append(errorNodes, accessorDeclarations.SetAccessor.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            }
            res = NewPseudoTypeInferredWithErrors(res.AsPseudoTypeInferred().Expression, res.AsPseudoTypeInferred().IsSignatureReturn, errorNodes);
        }
        return res;
    }
    return NewPseudoTypeNoResult(Ꮡaccessor);
}

internal static ж<ast.Node> getTypeAnnotationFromAllAccessorDeclarations(this ref PseudoChecker ch, ж<ast.Node> node, ast.AllAccessorDeclarations accessors) {
    var accessorType = ch.getTypeAnnotationFromAccessor(node);
    if (accessorType == nil && node != accessors.FirstAccessor) {
        accessorType = ch.getTypeAnnotationFromAccessor(accessors.FirstAccessor);
    }
    if (accessorType == nil && accessors.SecondAccessor != nil && node != accessors.SecondAccessor) {
        accessorType = ch.getTypeAnnotationFromAccessor(accessors.SecondAccessor);
    }
    return accessorType;
}

internal static ж<ast.Node> getTypeAnnotationFromAccessor(this ref PseudoChecker ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return default;
    }
    if (node.Kind == ast.KindGetAccessor) {
        return (~node.AsGetAccessorDeclaration()).Type;
    }
    var set = node.AsSetAccessorDeclaration();
    if ((~set).Parameters == nil || len((~set).Parameters.Nodes) < 1) {
        return default;
    }
    var p = (~set).Parameters.Nodes[0];
    if (!ast.IsParameterDeclaration(p)) {
        return default;
    }
    return (~p.AsParameterDeclaration()).Type;
}

} partial class pseudochecker_package {

internal static bool isValueSignatureDeclaration(ж<ast.Node> node) {
    return ast.IsFunctionExpression(node) || ast.IsArrowFunction(node) || ast.IsMethodDeclaration(node) || ast.IsAccessor(node) || ast.IsFunctionDeclaration(node) || ast.IsConstructorDeclaration(node);
}

} partial class pseudocheckerᴛPseudoChecker {

internal static ж<PseudoType> createReturnFromSignature(this ref PseudoChecker ch, ж<ast.Node> Ꮡfn) {
    ref var fn = ref Ꮡfn.DerefOrNull();

    if (ast.IsFunctionLike(Ꮡfn)) {
        var d = fn.FunctionLikeData();
        var r = d.Type;
        if (r != nil) {
            return NewPseudoTypeDirect(r);
        }
    }
    if (isValueSignatureDeclaration(Ꮡfn)) {
        return ch.typeFromSingleReturnExpression(Ꮡfn);
    }
    return NewPseudoTypeNoResult(Ꮡfn);
}

internal static ж<PseudoType> typeFromSingleReturnExpression(this ref PseudoChecker ch, ж<ast.Node> Ꮡfn) {
    ref var fn = ref Ꮡfn.DerefOrNull();

    ref var candidateExpr = ref heap<ж<ast.Node>>(out var ᏑcandidateExpr);
    if (Ꮡfn != nil && !ast.NodeIsMissing(fn.Body())) {
        var flags = ast.GetFunctionFlags(Ꮡfn);
        if ((ast.FunctionFlags)(flags & ast.FunctionFlagsAsyncGenerator) != 0) {
            return NewPseudoTypeInferred(Ꮡfn, true);
        }

        var body = fn.Body();
        if (ast.IsBlock(body)) {
            var bodyʗ1 = body;
            ast.ForEachReturnStatement(body, (ж<ast.Node> stmt) => {
                if (stmt.Parent != bodyʗ1) {
                    ᏑcandidateExpr.ValueSlot = default;
                    return true;
                }
                if (ᏑcandidateExpr.ValueSlot == nil) {
                    ᏑcandidateExpr.ValueSlot = stmt.AsReturnStatement().Value.Expression;
                } else {
                    ᏑcandidateExpr.ValueSlot = default;
                    return true;
                }
                return false;
            });
        } else {
            candidateExpr = body;
        }
    }
    if (candidateExpr != nil) {
        if (isContextuallyTyped(ref (candidateExpr).DerefOrNull())) {
            ж<ast.Node> t = default;
            if (candidateExpr.Kind == ast.KindTypeAssertionExpression) {
                t = candidateExpr.AsTypeAssertion().Value.Type;
            } else if (candidateExpr.Kind == ast.KindAsExpression) {
                t = candidateExpr.AsAsExpression().Value.Type;
            }
            if (t != nil && !ast.IsConstTypeReference(t)) {
                return NewPseudoTypeDirect(t);
            }
        } else {
            return ch.typeFromExpression(candidateExpr);
        }
    }
    return NewPseudoTypeInferred(Ꮡfn, true);
}

internal static ж<PseudoType> typeFromExpression(this ref PseudoChecker ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindOmittedExpression: {
        return PseudoTypeUndefined;
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return ch.typeFromExpression((~node.AsParenthesizedExpression()).Expression);
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        if ((~node.AsIdentifier()).Text == "undefined"u8) {
            return PseudoTypeUndefined;
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindNullKeyword: {
        return PseudoTypeNull;
    }
    case var ᴛ when ᴛ == ast.KindArrowFunction || ᴛ == ast.KindFunctionExpression: {
        return ch.typeFromFunctionLikeExpression(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindTypeAssertionExpression: {
        return ch.typeFromTypeAssertion((~node.AsTypeAssertion()).Expression, (~node.AsTypeAssertion()).Type);
    }
    case var ᴛ when ᴛ == ast.KindAsExpression: {
        return ch.typeFromTypeAssertion((~node.AsAsExpression()).Expression, (~node.AsAsExpression()).Type);
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        if (ast.IsPrimitiveLiteralValue(Ꮡnode, true)) {
            return ch.typeFromPrimitiveLiteralPrefix(node.AsPrefixUnaryExpression());
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        return ch.typeFromArrayLiteral(node.AsArrayLiteralExpression());
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        return ch.typeFromObjectLiteral(node.AsObjectLiteralExpression());
    }
    case var ᴛ when ᴛ == ast.KindClassExpression: {
        return NewPseudoTypeInferredWithErrors(Ꮡnode, false, [Ꮡnode]);
    }
    case var ᴛ when ᴛ == ast.KindTemplateExpression: {
        if (IsInConstContext(Ꮡnode)) {
            return NewPseudoTypeInferred(Ꮡnode, false);
        }
        return NewPseudoTypeMaybeConstLocation(Ꮡnode, NewPseudoTypeInferred(Ꮡnode, false), PseudoTypeString);
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral: {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode, NewPseudoTypeNumericLiteral(Ꮡnode), PseudoTypeNumber);
    }
    case var ᴛ when ᴛ == ast.KindNoSubstitutionTemplateLiteral: {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode, NewPseudoTypeStringLiteral(Ꮡnode), PseudoTypeString);
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode, NewPseudoTypeStringLiteral(Ꮡnode), PseudoTypeString);
    }
    case var ᴛ when ᴛ == ast.KindBigIntLiteral: {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode, NewPseudoTypeBigIntLiteral(Ꮡnode), PseudoTypeBigInt);
    }
    case var ᴛ when ᴛ == ast.KindTrueKeyword: {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode, PseudoTypeTrue, PseudoTypeBoolean);
    }
    case var ᴛ when ᴛ == ast.KindFalseKeyword: {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode, PseudoTypeFalse, PseudoTypeBoolean);
    }}

    return NewPseudoTypeInferred(Ꮡnode, false);
}

internal static ж<PseudoType> typeFromObjectLiteral(this ref PseudoChecker ch, ж<ast.ObjectLiteralExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var errorNodes = ch.canGetTypeFromObjectLiteral(Ꮡnode); if (errorNodes != nil) {
        return NewPseudoTypeInferredWithErrors(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, errorNodes);
    }
    if (node.Properties == nil || len(node.Properties.Nodes) == 0) {
        return NewPseudoTypeObjectLiteral(default);
    }
    var results = new slice<ж<PseudoObjectElement>>(0, len(node.Properties.Nodes));
    foreach (var (_, e) in range(node.Properties.Nodes)) {
        switch (e.Kind) {
        case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
            var optional = (~e.AsMethodDeclaration()).PostfixToken != nil && (~e.AsMethodDeclaration()).PostfixToken.Kind == ast.KindQuestionToken;
            if (e.FunctionLikeData().FullSignature != nil) {
                results = append(results, NewPseudoPropertyAssignment(
                    false,
                    e.Name(),
                    optional,
                    NewPseudoTypeDirect(e.FunctionLikeData().FullSignature)));
            } else {
                results = append(results, NewPseudoObjectMethod(
                    e,
                    e.Name(),
                    optional,
                    ch.cloneTypeParameters((~e.AsMethodDeclaration()).TypeParameters),
                    ch.cloneParameters(e.ParameterList()),
                    ch.createReturnFromSignature(e)));
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
            results = append(results, NewPseudoPropertyAssignment(
                false,
                e.Name(),
                (~e.AsPropertyAssignment()).PostfixToken != nil && (~e.AsPropertyAssignment()).PostfixToken.Kind == ast.KindQuestionToken,
                ch.typeFromExpression(e.Initializer())));
            break;
        }
        case var ᴛ when ᴛ == ast.KindSetAccessor || ᴛ == ast.KindGetAccessor: {
            var member = ch.getAccessorMember(e, e.Name());
            if (member != nil) {
                results = append(results, member);
            }
            break;
        }}

    }
    return NewPseudoTypeObjectLiteral(results);
}

internal static ж<PseudoObjectElement> getAccessorMember(this ref PseudoChecker ch, ж<ast.Node> Ꮡaccessor, ж<ast.Node> name) {
    ref var accessor = ref Ꮡaccessor.DerefOrNull();

    var allAccessors = ast.GetAllAccessorDeclarationsForDeclaration(Ꮡaccessor, accessor.Symbol().Declarations);

    if (allAccessors.GetAccessor != nil && (~allAccessors.GetAccessor).Type != nil && allAccessors.SetAccessor != nil && len((~allAccessors.SetAccessor).Parameters.Nodes) > 0 && (~(~allAccessors.SetAccessor).Parameters.Nodes[0].AsParameterDeclaration()).Type != nil) {
        if (ast.IsGetAccessorDeclaration(Ꮡaccessor)) {
            return NewPseudoGetAccessor(
                Ꮡaccessor,
                name,
                false,
                ch.typeFromAccessor(Ꮡaccessor));
        } else {
            return NewPseudoSetAccessor(
                Ꮡaccessor,
                name,
                false,
                ch.cloneParameters((~accessor.AsSetAccessorDeclaration()).Parameters)[0]);
        }
    }

    if (Ꮡaccessor == allAccessors.FirstAccessor) {
        var accessorType = ch.typeFromAccessor(Ꮡaccessor);
        var @readonly = ast.IsGetAccessorDeclaration(Ꮡaccessor) && allAccessors.SecondAccessor == nil;
        return NewPseudoPropertyAssignment(
            @readonly,
            name,
            false,
            accessorType);
    }
    return default;
}

internal static slice<ж<ast.Node>> canGetTypeFromObjectLiteral(this ref PseudoChecker ch, ж<ast.ObjectLiteralExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Properties == nil || len(node.Properties.Nodes) == 0) {
        return default;
    }
    slice<ж<ast.Node>> errorNodes = default;
    foreach (var (_, e) in range(node.Properties.Nodes)) {
        if ((ast.NodeFlags)(e.Flags & ast.NodeFlagsThisNodeHasError) != 0) {
            errorNodes = append(errorNodes, e);
            continue;
        }
        if (e.Kind == ast.KindShorthandPropertyAssignment || e.Kind == ast.KindSpreadAssignment) {
            errorNodes = append(errorNodes, e);
            continue;
        }
        if ((ast.NodeFlags)(e.Name().Flags & ast.NodeFlagsThisNodeHasError) != 0) {
            errorNodes = append(errorNodes, e.Name());
            continue;
        }
        if (e.Name().Kind == ast.KindPrivateIdentifier) {
            errorNodes = append(errorNodes, e);
            continue;
        }
        if (e.Name().Kind == ast.KindComputedPropertyName) {
            var expression = e.Name().Expression();
            if (!ast.IsPrimitiveLiteralValue(expression, false)) {
                errorNodes = append(errorNodes, e.Name());
            }
        }
    }
    return errorNodes;
}

internal static ж<PseudoType> typeFromArrayLiteral(this ref PseudoChecker ch, ж<ast.ArrayLiteralExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var errorNodes = ch.canGetTypeFromArrayLiteral(Ꮡnode); if (errorNodes != nil) {
        return NewPseudoTypeInferredWithErrors(Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, errorNodes);
    }
    if (IsInConstContext(Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && isContextuallyTyped(ref (Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).DerefOrNull())) {
        return NewPseudoTypeInferred(Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false);
    }
    var results = new slice<ж<PseudoType>>(0, len(node.Elements.Nodes));
    foreach (var (_, e) in range(node.Elements.Nodes)) {
        results = append(results, ch.typeFromExpression(e));
    }
    return NewPseudoTypeTuple(results);
}

internal static slice<ж<ast.Node>> canGetTypeFromArrayLiteral(this ref PseudoChecker ch, ж<ast.ArrayLiteralExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsInConstContext(Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return slice<ж<ast.Node>>([Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()]);
    }
    foreach (var (_, e) in range(node.Elements.Nodes)) {
        if (e.Kind == ast.KindSpreadElement) {
            return [e];
        }
    }
    return default;
}

} partial class pseudochecker_package {

internal static bool isConstContextPropagatingKind(ast.Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression || ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindParenthesizedExpression || ᴛ == ast.KindSpreadElement || ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindShorthandPropertyAssignment || ᴛ == ast.KindTemplateSpan || ᴛ == ast.KindPrefixUnaryExpression: {
        return true;
    }}

    return false;
}

public static bool IsInConstContext(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var maybeAssertion = ast.FindAncestor(
        node.Parent,
        (ж<ast.Node> n) => ast.IsAssertionExpression(n) || !isConstContextPropagatingKind(n.Kind));
    return ast.IsConstAssertion(maybeAssertion);
}

} partial class pseudocheckerᴛPseudoChecker {

internal static ж<PseudoType> typeFromPrimitiveLiteralPrefix(this ref PseudoChecker ch, ж<ast.PrefixUnaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expr = Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    if (node.Operator == ast.KindPlusToken) {
        expr = node.Operand;
    }
    var inner = node.Operand;
    if (inner.Kind == ast.KindBigIntLiteral) {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), NewPseudoTypeBigIntLiteral(expr.AsNode()), PseudoTypeBigInt);
    }
    if (inner.Kind == ast.KindNumericLiteral) {
        return NewPseudoTypeMaybeConstLocation(Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), NewPseudoTypeNumericLiteral(expr.AsNode()), PseudoTypeNumber);
    }
    debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(inner));
    return default;
}

internal static ж<PseudoType> typeFromTypeAssertion(this ref PseudoChecker ch, ж<ast.Node> expression, ж<ast.Node> typeNode) {
    if (ast.IsConstTypeReference(typeNode)) {
        return ch.typeFromExpression(expression);
    }
    return NewPseudoTypeDirect(typeNode);
}

internal static ж<PseudoType> typeFromFunctionLikeExpression(this ref PseudoChecker ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.FunctionLikeData().FullSignature != nil) {
        return NewPseudoTypeDirect(node.FunctionLikeData().FullSignature);
    }
    var returnType = ch.createReturnFromSignature(Ꮡnode);
    var typeParameters = ch.cloneTypeParameters(node.FunctionLikeData().TypeParameters);
    var parameters = ch.cloneParameters(node.FunctionLikeData().Parameters);
    return NewPseudoTypeSingleCallSignature(
        Ꮡnode,
        parameters,
        typeParameters,
        returnType);
}

internal static slice<ж<ast.TypeParameterDeclaration>> cloneTypeParameters(this ref PseudoChecker ch, ж<ast.NodeList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil) {
        return default;
    }
    if (len(nodes.Nodes) == 0) {
        return default;
    }
    var result = new slice<ж<ast.TypeParameterDeclaration>>(0, len(nodes.Nodes));
    foreach (var (_, e) in range(nodes.Nodes)) {
        result = append(result, e.AsTypeParameterDeclaration());
    }
    return result;
}

} partial class pseudochecker_package {

internal static bool isUndefinedPseudoType(ж<PseudoType> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return t.Kind == PseudoTypeKindUndefined || (t.Kind == PseudoTypeKindMaybeConstLocation && isUndefinedPseudoType(t.AsPseudoTypeMaybeConstLocation().ConstType));
}

internal static bool typeNodeCouldReferToUndefined(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (node.Kind == ast.KindParenthesizedType) {
        Ꮡnode = node.AsParenthesizedTypeNode().Value.Type; node = ref Ꮡnode.DerefOrNull();
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindTypeReference || ᴛ == ast.KindIndexedAccessType || ᴛ == ast.KindTypeQuery || ᴛ == ast.KindOptionalType || ᴛ == ast.KindRestType || ᴛ == ast.KindImportType: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindIntersectionType: {
        return core.Some<ж<ast.Node>>(node.AsIntersectionTypeNode().Types.Nodes, typeNodeCouldReferToUndefined);
    }
    case var ᴛ when ᴛ == ast.KindUnionType: {
        return core.Some<ж<ast.Node>>(node.AsUnionTypeNode().Types.Nodes, typeNodeCouldReferToUndefined);
    }
    case var ᴛ when ᴛ == ast.KindConditionalType: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindTypeOperator: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindTypePredicate: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindUndefinedKeyword: {
        return true;
    }
    default: {
        return false;
    }}

}

public static bool CouldAlreadyReferToUndefinedType(ж<PseudoType> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (t.Kind == PseudoTypeKindNoResult || t.Kind == PseudoTypeKindInferred || isUndefinedPseudoType(Ꮡt)) {
        return true;
    }
    if (t.Kind == PseudoTypeKindMaybeConstLocation) {
        var mc = t.AsPseudoTypeMaybeConstLocation();
        return CouldAlreadyReferToUndefinedType(mc.RegularType);
    }
    if (t.Kind == PseudoTypeKindDirect) {
        var node = t.AsPseudoTypeDirect().TypeNode;
        return typeNodeCouldReferToUndefined(node);
    }
    if (t.Kind == PseudoTypeKindUnion) {
        return core.Some<ж<PseudoType>>(t.AsPseudoTypeUnion().Types, CouldAlreadyReferToUndefinedType);
    }
    return false;
}

internal static bool isOptionalInitializedOrRestParameter(ж<astꓸParameterDeclarationNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var p = node.AsParameterDeclaration();
    if (p.DotDotDotToken != nil || (~p).Initializer != nil || (~p).QuestionToken != nil) {
        return true;
    }
    return false;
}

internal static nint lastRequiredParamIndex(slice<ж<ast.Node>> @params) {
    for (nint i = len(@params) - 1; i >= 0; i--) {
        if (!isOptionalInitializedOrRestParameter(@params[i])) {
            return i + 1;
        }
    }
    return 0;
}

internal static ж<PseudoType> addUndefinedIfDefinitelyRequired(ж<PseudoType> expr) {
    if (CouldAlreadyReferToUndefinedType(expr)) {
        return expr;
    }
    return NewPseudoTypeUnion([expr, PseudoTypeUndefined]);
}

} partial class pseudocheckerᴛPseudoChecker {

internal static ж<PseudoType> typeFromParameter(this ref PseudoChecker ch, ж<ast.ParameterDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (parent.Kind == ast.KindSetAccessor) {
        return ch.GetTypeOfAccessor(parent);
    }
    if (node.Initializer == nil) {
        if (node.Type != nil) {
            return NewPseudoTypeDirect(node.Type);
        }
        return NewPseudoTypeNoResult(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    var p = parent.Parameters();
    nint selfIdx = slices.Index(p, Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint lastRequired = lastRequiredParamIndex(p);
    return ch.typeFromParameterWorker(Ꮡnode, selfIdx, lastRequired);
}

internal static ж<PseudoType> typeFromParameterWorker(this ref PseudoChecker ch, ж<ast.ParameterDeclaration> Ꮡnode, nint selfIdx, nint lastRequired) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (parent.Kind == ast.KindSetAccessor) {
        return ch.GetTypeOfAccessor(parent);
    }
    var hasRequiredAfter = selfIdx < lastRequired - 1;
    var declaredType = node.Type;
    if (declaredType != nil) {
        var result = NewPseudoTypeDirect(declaredType);
        if (ch.strictNullChecks && node.Initializer != nil && hasRequiredAfter) {
            return addUndefinedIfDefinitelyRequired(result);
        }
        return result;
    }
    if (node.Initializer != nil && ast.IsIdentifier(node.Name()) && !isContextuallyTyped(ref (Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).DerefOrNull())) {
        var expr = ch.typeFromExpression(node.Initializer);
        if (expr != nil && (expr.Kind == PseudoTypeKindInferred && len(expr.AsPseudoTypeInferred().ErrorNodes) == 0)) {
            expr = NewPseudoTypeInferredWithErrors(expr.AsPseudoTypeInferred().Expression, false, slice<ж<ast.Node>>([Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()]));
        }
        if (!ch.strictNullChecks) {
            return expr;
        }
        if (!hasRequiredAfter) {
            return expr;
        }
        return addUndefinedIfDefinitelyRequired(expr);
    }
    return NewPseudoTypeNoResult(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static slice<ж<PseudoParameter>> cloneParameters(this ref PseudoChecker ch, ж<ast.NodeList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil) {
        return default;
    }
    if (len(nodes.Nodes) == 0) {
        return default;
    }
    nint lastRequired = lastRequiredParamIndex(nodes.Nodes);
    var result = new slice<ж<PseudoParameter>>(0, len(nodes.Nodes));
    foreach (var (i, e) in range(nodes.Nodes)) {
        var p = e.AsParameterDeclaration();
        var optional = (~p).QuestionToken != nil;
        if (!optional && (~p).Initializer != nil) {
            optional = i >= lastRequired - 1;
        }
        result = append(result, NewPseudoParameter(
            p.DotDotDotToken != nil,
            e.Name(),
            optional,
            ch.typeFromParameterWorker(p, i, lastRequired)));
    }
    return result;
}

} partial class pseudochecker_package {

internal static bool isContextuallyTyped(ref ast.Node node) {
    return ast.FindAncestor(node.Parent, (ж<ast.Node> n) => {
        if (ast.IsCallExpression(n)) {
            return true;
        }
        if (ast.IsSatisfiesExpression(n)) {
            return true;
        }
        if ((ast.IsVariableParameterOrProperty(n) || ast.IsAssertionExpression(n)) && n.Type() != nil && !ast.IsConstAssertion(n)) {
            return true;
        }
        return ast.IsJsxElement(n) || ast.IsJsxExpression(n);
    }) != nil;
}

} // end pseudochecker_package
