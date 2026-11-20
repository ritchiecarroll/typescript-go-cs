namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;

partial class ast_package {

partial struct OperatorPrecedence /*num:nint*/;

public static OperatorPrecedence OperatorPrecedenceComma => /* iota */ 0;
public static OperatorPrecedence OperatorPrecedenceSpread => 1;
public static OperatorPrecedence OperatorPrecedenceYield => 2;
public static OperatorPrecedence OperatorPrecedenceAssignment => 3;
public static OperatorPrecedence OperatorPrecedenceConditional => 4;
public static OperatorPrecedence OperatorPrecedenceLogicalOR => 5;
public static OperatorPrecedence OperatorPrecedenceLogicalAND => 6;
public static OperatorPrecedence OperatorPrecedenceBitwiseOR => 7;
public static OperatorPrecedence OperatorPrecedenceBitwiseXOR => 8;
public static OperatorPrecedence OperatorPrecedenceBitwiseAND => 9;
public static OperatorPrecedence OperatorPrecedenceEquality => 10;
public static OperatorPrecedence OperatorPrecedenceRelational => 11;
public static OperatorPrecedence OperatorPrecedenceShift => 12;
public static OperatorPrecedence OperatorPrecedenceAdditive => 13;
public static OperatorPrecedence OperatorPrecedenceMultiplicative => 14;
public static OperatorPrecedence OperatorPrecedenceExponentiation => 15;
public static OperatorPrecedence OperatorPrecedenceUnary => 16;
public static OperatorPrecedence OperatorPrecedenceUpdate => 17;
public static OperatorPrecedence OperatorPrecedenceLeftHandSide => 18;
public static OperatorPrecedence OperatorPrecedenceOptionalChain => 19;
public static OperatorPrecedence OperatorPrecedenceMember => 20;
public static OperatorPrecedence OperatorPrecedencePrimary => 21;
public static OperatorPrecedence OperatorPrecedenceParentheses => 22;
public static OperatorPrecedence OperatorPrecedenceLowest => /* OperatorPrecedenceComma */ 0;
public static OperatorPrecedence OperatorPrecedenceHighest => /* OperatorPrecedenceParentheses */ 22;
public static OperatorPrecedence OperatorPrecedenceDisallowComma => /* OperatorPrecedenceYield */ 2;
public static OperatorPrecedence OperatorPrecedenceCoalesce => /* OperatorPrecedenceLogicalOR */ 5;
public static OperatorPrecedence OperatorPrecedenceInvalid => -1;

internal static Kind getOperator(ж<Expression> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    switch (expression.Kind) {
    case var ᴛ when ᴛ == KindBinaryExpression: {
        return expression.AsBinaryExpression().OperatorToken.Kind;
    }
    case var ᴛ when ᴛ == KindPrefixUnaryExpression: {
        return expression.AsPrefixUnaryExpression().Operator;
    }
    case var ᴛ when ᴛ == KindPostfixUnaryExpression: {
        return expression.AsPostfixUnaryExpression().Operator;
    }
    default: {
        return expression.Kind;
    }}

}

public static OperatorPrecedence GetExpressionPrecedence(ж<Expression> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    var @operator = getOperator(Ꮡexpression);
    OperatorPrecedenceFlags flags = default;
    if (expression.Kind == KindNewExpression && expression.ArgumentList() == nil) {
        flags = OperatorPrecedenceFlagsNewWithoutArguments;
    } else if (IsOptionalChain(Ꮡexpression)) {
        flags = OperatorPrecedenceFlagsOptionalChain;
    }
    return GetOperatorPrecedence(expression.Kind, @operator, flags);
}

partial struct OperatorPrecedenceFlags /*num:nint*/;

public static OperatorPrecedenceFlags OperatorPrecedenceFlagsNone => 0;
public static OperatorPrecedenceFlags OperatorPrecedenceFlagsNewWithoutArguments => /* 1 << 0 */ 1;
public static OperatorPrecedenceFlags OperatorPrecedenceFlagsOptionalChain => /* 1 << 1 */ 2;

public static OperatorPrecedence GetOperatorPrecedence(Kind nodeKind, Kind operatorKind, OperatorPrecedenceFlags flags) {
    switch (nodeKind) {
    case var ᴛ when ᴛ == KindSpreadElement: {
        return OperatorPrecedenceSpread;
    }
    case var ᴛ when ᴛ == KindYieldExpression: {
        return OperatorPrecedenceYield;
    }
    case var ᴛ when ᴛ == KindArrowFunction: {
        return OperatorPrecedenceAssignment;
    }
    case var ᴛ when ᴛ == KindConditionalExpression: {
        return OperatorPrecedenceConditional;
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        switch (operatorKind) {
        case var ᴛ2 when ᴛ2 == KindCommaToken: {
            return OperatorPrecedenceComma;
        }
        case var ᴛ2 when ᴛ2 == KindEqualsToken || ᴛ2 == KindPlusEqualsToken || ᴛ2 == KindMinusEqualsToken || ᴛ2 == KindAsteriskAsteriskEqualsToken || ᴛ2 == KindAsteriskEqualsToken || ᴛ2 == KindSlashEqualsToken || ᴛ2 == KindPercentEqualsToken || ᴛ2 == KindLessThanLessThanEqualsToken || ᴛ2 == KindGreaterThanGreaterThanEqualsToken || ᴛ2 == KindGreaterThanGreaterThanGreaterThanEqualsToken || ᴛ2 == KindAmpersandEqualsToken || ᴛ2 == KindCaretEqualsToken || ᴛ2 == KindBarEqualsToken || ᴛ2 == KindBarBarEqualsToken || ᴛ2 == KindAmpersandAmpersandEqualsToken || ᴛ2 == KindQuestionQuestionEqualsToken: {
            return OperatorPrecedenceAssignment;
        }
        default: {
            return GetBinaryOperatorPrecedence(operatorKind);
        }}

        break;
    }
    case var ᴛ when ᴛ == KindTypeAssertionExpression || ᴛ == KindNonNullExpression || ᴛ == KindPrefixUnaryExpression || ᴛ == KindTypeOfExpression || ᴛ == KindVoidExpression || ᴛ == KindDeleteExpression || ᴛ == KindAwaitExpression: {
        return OperatorPrecedenceUnary;
    }
    case var ᴛ when ᴛ == KindPostfixUnaryExpression: {
        return OperatorPrecedenceUpdate;
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression || ᴛ == KindElementAccessExpression: {
        if ((OperatorPrecedenceFlags)(flags & OperatorPrecedenceFlagsOptionalChain) != 0) {
            return OperatorPrecedenceOptionalChain;
        }
        return OperatorPrecedenceMember;
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        if ((OperatorPrecedenceFlags)(flags & OperatorPrecedenceFlagsOptionalChain) != 0) {
            return OperatorPrecedenceOptionalChain;
        }
        return OperatorPrecedenceMember;
    }
    case var ᴛ when ᴛ == KindNewExpression: {
        if ((OperatorPrecedenceFlags)(flags & OperatorPrecedenceFlagsNewWithoutArguments) != 0) {
            return OperatorPrecedenceLeftHandSide;
        }
        return OperatorPrecedenceMember;
    }
    case var ᴛ when ᴛ == KindTaggedTemplateExpression || ᴛ == KindMetaProperty || ᴛ == KindExpressionWithTypeArguments: {
        return OperatorPrecedenceMember;
    }
    case var ᴛ when ᴛ == KindAsExpression || ᴛ == KindSatisfiesExpression: {
        return OperatorPrecedenceRelational;
    }
    case var ᴛ when ᴛ == KindThisKeyword || ᴛ == KindSuperKeyword || ᴛ == KindImportKeyword || ᴛ == KindIdentifier || ᴛ == KindPrivateIdentifier || ᴛ == KindNullKeyword || ᴛ == KindTrueKeyword || ᴛ == KindFalseKeyword || ᴛ == KindNumericLiteral || ᴛ == KindBigIntLiteral || ᴛ == KindStringLiteral || ᴛ == KindArrayLiteralExpression || ᴛ == KindObjectLiteralExpression || ᴛ == KindFunctionExpression || ᴛ == KindClassExpression || ᴛ == KindRegularExpressionLiteral || ᴛ == KindNoSubstitutionTemplateLiteral || ᴛ == KindTemplateExpression || ᴛ == KindOmittedExpression || ᴛ == KindJsxElement || ᴛ == KindJsxSelfClosingElement || ᴛ == KindJsxFragment || ᴛ == KindMissingDeclaration: {
        return OperatorPrecedencePrimary;
    }
    case var ᴛ when ᴛ == KindParenthesizedExpression: {
        return OperatorPrecedenceParentheses;
    }
    default: {
        return OperatorPrecedenceInvalid;
    }}

}

public static OperatorPrecedence GetBinaryOperatorPrecedence(Kind operatorKind) {
    switch (operatorKind) {
    case var ᴛ when ᴛ == KindQuestionQuestionToken: {
        return OperatorPrecedenceCoalesce;
    }
    case var ᴛ when ᴛ == KindBarBarToken: {
        return OperatorPrecedenceLogicalOR;
    }
    case var ᴛ when ᴛ == KindAmpersandAmpersandToken: {
        return OperatorPrecedenceLogicalAND;
    }
    case var ᴛ when ᴛ == KindBarToken: {
        return OperatorPrecedenceBitwiseOR;
    }
    case var ᴛ when ᴛ == KindCaretToken: {
        return OperatorPrecedenceBitwiseXOR;
    }
    case var ᴛ when ᴛ == KindAmpersandToken: {
        return OperatorPrecedenceBitwiseAND;
    }
    case var ᴛ when ᴛ == KindEqualsEqualsToken || ᴛ == KindExclamationEqualsToken || ᴛ == KindEqualsEqualsEqualsToken || ᴛ == KindExclamationEqualsEqualsToken: {
        return OperatorPrecedenceEquality;
    }
    case var ᴛ when ᴛ == KindLessThanToken || ᴛ == KindGreaterThanToken || ᴛ == KindLessThanEqualsToken || ᴛ == KindGreaterThanEqualsToken || ᴛ == KindInstanceOfKeyword || ᴛ == KindInKeyword || ᴛ == KindAsKeyword || ᴛ == KindSatisfiesKeyword: {
        return OperatorPrecedenceRelational;
    }
    case var ᴛ when ᴛ == KindLessThanLessThanToken || ᴛ == KindGreaterThanGreaterThanToken || ᴛ == KindGreaterThanGreaterThanGreaterThanToken: {
        return OperatorPrecedenceShift;
    }
    case var ᴛ when ᴛ == KindPlusToken || ᴛ == KindMinusToken: {
        return OperatorPrecedenceAdditive;
    }
    case var ᴛ when ᴛ == KindAsteriskToken || ᴛ == KindSlashToken || ᴛ == KindPercentToken: {
        return OperatorPrecedenceMultiplicative;
    }
    case var ᴛ when ᴛ == KindAsteriskAsteriskToken: {
        return OperatorPrecedenceExponentiation;
    }}

    return OperatorPrecedenceInvalid;
}

public static ж<Expression> GetLeftmostExpression(ж<Expression> Ꮡnode, bool stopAtCallExpressions) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (;;) {
        var exprᴛ1 = node.Kind;
        var matchᴛ1 = false;
        if (exprᴛ1 == KindPostfixUnaryExpression) { matchᴛ1 = true;
            Ꮡnode = node.AsPostfixUnaryExpression().Operand; node = ref Ꮡnode.DerefOrNull();
            continue;
        }
        else if (exprᴛ1 == KindBinaryExpression) { matchᴛ1 = true;
            Ꮡnode = node.AsBinaryExpression().Left; node = ref Ꮡnode.DerefOrNull();
            continue;
        }
        else if (exprᴛ1 == KindConditionalExpression) { matchᴛ1 = true;
            Ꮡnode = node.AsConditionalExpression().Condition; node = ref Ꮡnode.DerefOrNull();
            continue;
        }
        else if (exprᴛ1 == KindTaggedTemplateExpression) { matchᴛ1 = true;
            Ꮡnode = node.AsTaggedTemplateExpression().Tag; node = ref Ꮡnode.DerefOrNull();
            continue;
        }
        else if (exprᴛ1 == KindCallExpression) {
            if (stopAtCallExpressions) {
                return Ꮡnode;
            }
            fallthrough = true;
        }
        if (fallthrough || !matchᴛ1 && (exprᴛ1 == KindAsExpression || exprᴛ1 == KindElementAccessExpression || exprᴛ1 == KindPropertyAccessExpression || exprᴛ1 == KindNonNullExpression || exprᴛ1 == KindPartiallyEmittedExpression || exprᴛ1 == KindSatisfiesExpression)) { matchᴛ1 = true;
            Ꮡnode = node.Expression(); node = ref Ꮡnode.DerefOrNull();
            continue;
        }

        return Ꮡnode;
    }
}

partial struct TypePrecedence /*num:int32*/;

public static TypePrecedence TypePrecedenceConditional => /* iota */ 0;
public static TypePrecedence TypePrecedenceJSDoc => 1;
public static TypePrecedence TypePrecedenceFunction => 2;
public static TypePrecedence TypePrecedenceUnion => 3;
public static TypePrecedence TypePrecedenceIntersection => 4;
public static TypePrecedence TypePrecedenceTypeOperator => 5;
public static TypePrecedence TypePrecedencePostfix => 6;
public static TypePrecedence TypePrecedenceNonArray => 7;
public static TypePrecedence TypePrecedenceLowest => /* TypePrecedenceConditional */ 0;
public static TypePrecedence TypePrecedenceHighest => /* TypePrecedenceNonArray */ 7;

public static TypePrecedence GetTypeNodePrecedence(ж<TypeNode> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    switch (n.Kind) {
    case var ᴛ when ᴛ == KindConditionalType: {
        return TypePrecedenceConditional;
    }
    case var ᴛ when ᴛ == KindJSDocOptionalType || ᴛ == KindJSDocVariadicType: {
        return TypePrecedenceJSDoc;
    }
    case var ᴛ when ᴛ == KindFunctionType || ᴛ == KindConstructorType: {
        return TypePrecedenceFunction;
    }
    case var ᴛ when ᴛ == KindUnionType: {
        return TypePrecedenceUnion;
    }
    case var ᴛ when ᴛ == KindIntersectionType: {
        return TypePrecedenceIntersection;
    }
    case var ᴛ when ᴛ == KindTypeOperator: {
        return TypePrecedenceTypeOperator;
    }
    case var ᴛ when ᴛ == KindInferType: {
        if (n.AsInferTypeNode().TypeParameter.AsTypeParameterDeclaration().Constraint != nil) {
            return TypePrecedenceFunction;
        }
        return TypePrecedenceTypeOperator;
    }
    case var ᴛ when ᴛ == KindIndexedAccessType || ᴛ == KindArrayType || ᴛ == KindOptionalType: {
        return TypePrecedencePostfix;
    }
    case var ᴛ when ᴛ == KindTypeQuery: {
        return TypePrecedenceTypeOperator;
    }
    case var ᴛ when ᴛ == KindAnyKeyword || ᴛ == KindUnknownKeyword || ᴛ == KindStringKeyword || ᴛ == KindNumberKeyword || ᴛ == KindBigIntKeyword || ᴛ == KindSymbolKeyword || ᴛ == KindBooleanKeyword || ᴛ == KindUndefinedKeyword || ᴛ == KindNeverKeyword || ᴛ == KindObjectKeyword || ᴛ == KindIntrinsicKeyword || ᴛ == KindVoidKeyword || ᴛ == KindJSDocAllType || ᴛ == KindJSDocNullableType || ᴛ == KindJSDocNonNullableType || ᴛ == KindLiteralType || ᴛ == KindTypePredicate || ᴛ == KindTypeReference || ᴛ == KindTypeLiteral || ᴛ == KindTupleType || ᴛ == KindRestType || ᴛ == KindParenthesizedType || ᴛ == KindThisType || ᴛ == KindMappedType || ᴛ == KindNamedTupleMember || ᴛ == KindTemplateLiteralType || ᴛ == KindImportType || ᴛ == KindPropertyAccessExpression || ᴛ == KindExpressionWithTypeArguments: {
        return TypePrecedenceNonArray;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled TypeNode: %v"u8, n.Kind));
        break;
    }}

}

} // end ast_package
