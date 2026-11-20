namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.sync;
using ꓸꓸꓸKind = Span<ast_package.Kind>;
using static ast_package;

partial class ast_package {

internal static readonly ж<atomic.Uint64> ᏑnextNodeId = new StaticBox<atomic.Uint64>(default(atomic.Uint64));
internal static ref atomic.Uint64 nextNodeId => ref ((StaticBox<atomic.Uint64>)ᏑnextNodeId).ValueSlot;
internal static readonly ж<atomic.Uint64> ᏑnextSymbolId = new StaticBox<atomic.Uint64>(default(atomic.Uint64));
internal static ref atomic.Uint64 nextSymbolId => ref ((StaticBox<atomic.Uint64>)ᏑnextSymbolId).ValueSlot;

public static NodeId GetNodeId(ж<Node> node) {
    var id = node.Ꮡid.Load();
    if (id == 0) {
        id = ᏑnextNodeId.Add(1);
        if (!node.Ꮡid.CompareAndSwap(0, id)) {
            id = node.Ꮡid.Load();
        }
    }
    return (NodeId)id;
}

public static SymbolId GetSymbolId(ж<Symbol> symbol) {
    var id = symbol.Ꮡid.Load();
    if (id == 0) {
        id = ᏑnextSymbolId.Add(1);
        if (!symbol.Ꮡid.CompareAndSwap(0, id)) {
            id = symbol.Ꮡid.Load();
        }
    }
    return (SymbolId)id;
}

public static SymbolTable GetSymbolTable(ж<SymbolTable> Ꮡdata) {
    ref var data = ref Ꮡdata.DerefOrNull();

    if (data == nil) {
        data = new SymbolTable(0);
    }
    return data;
}

public static SymbolTable GetMembers(ж<Symbol> symbol) {
    return GetSymbolTable(symbol.ᏑMembers);
}

public static SymbolTable GetExports(ж<Symbol> symbol) {
    return GetSymbolTable(symbol.ᏑExports);
}

public static SymbolTable GetLocals(ж<Node> Ꮡcontainer) {
    ref var container = ref Ꮡcontainer.DerefOrNull();

    return GetSymbolTable(container.LocalsContainerData().ᏑLocals);
}

public static bool NodeIsMissing(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode == nil || node.Loc.Pos() == node.Loc.End() && node.Loc.Pos() >= 0 && node.Kind != KindEndOfFile;
}

public static bool NodeIsPresent(ж<Node> node) {
    return !NodeIsMissing(node);
}

public static bool NodeIsSynthesized(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return PositionIsSynthesized(node.Loc.Pos()) || PositionIsSynthesized(node.Loc.End());
}

public static bool RangeIsSynthesized(core.TextRange loc) {
    return PositionIsSynthesized(loc.Pos()) || PositionIsSynthesized(loc.End());
}

public static bool PositionIsSynthesized(nint pos) {
    return pos < 0;
}

public static ж<Node> FindLastVisibleNode(slice<ж<Node>> nodes) {
    nint fromEnd = 1;
    while (fromEnd <= len(nodes) && (NodeFlags)(nodes[len(nodes) - fromEnd].Flags & NodeFlagsReparsed) != 0) {
        fromEnd++;
    }
    if (fromEnd <= len(nodes)) {
        return nodes[len(nodes) - fromEnd];
    }
    return default;
}

public static bool NodeKindIs(ж<Node> Ꮡnode, params ꓸꓸꓸKind kindsʗp) {
    var kinds = kindsʗp.slice();
    ref var node = ref Ꮡnode.DerefOrNull();

    return slices.Contains(kinds, node.Kind);
}

public static bool IsModifier(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsModifierKind(node.Kind);
}

public static bool IsModifierLike(ж<Node> node) {
    return IsModifier(node) || IsDecorator(node);
}

public static bool IsCompoundAssignment(Kind token) {
    return token >= KindFirstCompoundAssignment && token <= KindLastCompoundAssignment;
}

public static bool IsAssignmentExpression(ж<Node> Ꮡnode, bool excludeCompoundAssignment) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == KindBinaryExpression) {
        var expr = node.AsBinaryExpression();
        return (expr.OperatorToken.Kind == KindEqualsToken || !excludeCompoundAssignment && IsAssignmentOperator(expr.OperatorToken.Kind)) && IsLeftHandSideExpression(expr.Left);
    }
    return false;
}

public static ж<Node> GetRightMostAssignedExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (IsAssignmentExpression(Ꮡnode, false)) {
        Ꮡnode = node.AsBinaryExpression().Right; node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

public static bool IsDestructuringAssignment(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsAssignmentExpression(Ꮡnode, true)) {
        var kind = node.AsBinaryExpression().Left.Kind;
        return kind == KindObjectLiteralExpression || kind == KindArrayLiteralExpression;
    }
    return false;
}

public static bool IsObjectBindingOrAssignmentElement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindBindingElement || ᴛ == KindPropertyAssignment || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindSpreadAssignment: {
        return true;
    }}

    return false;
}

public static bool IsArrayBindingOrAssignmentElement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindBindingElement || ᴛ == KindOmittedExpression || ᴛ == KindSpreadElement || ᴛ == KindArrayLiteralExpression || ᴛ == KindObjectLiteralExpression || ᴛ == KindIdentifier || ᴛ == KindPropertyAccessExpression || ᴛ == KindElementAccessExpression: {
        return true;
    }}

    return IsAssignmentExpression(Ꮡnode, true);
}

public static bool IsBindingPattern(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindObjectBindingPattern || node.Kind == KindArrayBindingPattern;
}

public static bool IsForInOrOfStatement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && (node.Kind == KindForInStatement || node.Kind == KindForOfStatement);
}

public static bool IsAssignmentTarget(ж<Node> node) {
    return GetAssignmentTarget(node) != nil;
}

public static ж<Node> GetAssignmentTarget(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (;;) {
        var parent = node.Parent;
        switch (parent.Kind) {
        case var ᴛ when ᴛ == KindBinaryExpression: {
            if (IsAssignmentOperator(parent.AsBinaryExpression().OperatorToken.Kind) && parent.AsBinaryExpression().Left == Ꮡnode) {
                return parent;
            }
            return default;
        }
        case var ᴛ when ᴛ == KindPrefixUnaryExpression: {
            if (parent.AsPrefixUnaryExpression().Operator == KindPlusPlusToken || parent.AsPrefixUnaryExpression().Operator == KindMinusMinusToken) {
                return parent;
            }
            return default;
        }
        case var ᴛ when ᴛ == KindPostfixUnaryExpression: {
            if (parent.AsPostfixUnaryExpression().Operator == KindPlusPlusToken || parent.AsPostfixUnaryExpression().Operator == KindMinusMinusToken) {
                return parent;
            }
            return default;
        }
        case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
            if (parent.Initializer() == Ꮡnode) {
                return parent;
            }
            return default;
        }
        case var ᴛ when ᴛ == KindParenthesizedExpression || ᴛ == KindArrayLiteralExpression || ᴛ == KindSpreadElement || ᴛ == KindNonNullExpression: {
            Ꮡnode = parent; node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == KindSpreadAssignment: {
            Ꮡnode = parent.Parent; node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
            if (parent.AsShorthandPropertyAssignment().Name() != Ꮡnode) {
                return default;
            }
            Ꮡnode = parent.Parent; node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == KindPropertyAssignment: {
            if (parent.AsPropertyAssignment().Name() == Ꮡnode) {
                return default;
            }
            Ꮡnode = parent.Parent; node = ref Ꮡnode.DerefOrNull();
            break;
        }
        default: {
            return default;
        }}

    }
}

public static bool IsLogicalBinaryOperator(Kind token) {
    return token == KindBarBarToken || token == KindAmpersandAmpersandToken;
}

public static bool IsLogicalOrCoalescingBinaryOperator(Kind token) {
    return IsLogicalBinaryOperator(token) || token == KindQuestionQuestionToken;
}

public static bool IsLogicalOrCoalescingBinaryExpression(ж<Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    return IsBinaryExpression(Ꮡexpr) && IsLogicalOrCoalescingBinaryOperator(expr.AsBinaryExpression().OperatorToken.Kind);
}

public static bool IsLogicalOrCoalescingAssignmentExpression(ж<Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    return IsBinaryExpression(Ꮡexpr) && IsLogicalOrCoalescingAssignmentOperator(expr.AsBinaryExpression().OperatorToken.Kind);
}

public static bool IsLogicalExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (;;) {
        if (node.Kind == KindParenthesizedExpression) {
            Ꮡnode = node.Expression(); node = ref Ꮡnode.DerefOrNull();
        } else if (node.Kind == KindPrefixUnaryExpression && node.AsPrefixUnaryExpression().Operator == KindExclamationToken) {
            Ꮡnode = node.AsPrefixUnaryExpression().Operand; node = ref Ꮡnode.DerefOrNull();
        } else {
            return IsLogicalOrCoalescingBinaryExpression(Ꮡnode);
        }
    }
}

public static bool IsAccessor(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindGetAccessor || node.Kind == KindSetAccessor;
}

public static bool IsPropertyNameLiteral(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindIdentifier || ᴛ == KindStringLiteral || ᴛ == KindNoSubstitutionTemplateLiteral || ᴛ == KindNumericLiteral: {
        return true;
    }}

    return false;
}

public static bool IsMemberName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindIdentifier || node.Kind == KindPrivateIdentifier;
}

public static bool IsEntityName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindIdentifier || node.Kind == KindQualifiedName;
}

public static bool IsPropertyName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindIdentifier || ᴛ == KindPrivateIdentifier || ᴛ == KindStringLiteral || ᴛ == KindNumericLiteral || ᴛ == KindComputedPropertyName: {
        return true;
    }}

    return false;
}

public static bool IsIdentifierName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    switch (parent.Kind) {
    case var ᴛ when ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindMethodDeclaration || ᴛ == KindMethodSignature || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindEnumMember || ᴛ == KindPropertyAssignment || ᴛ == KindPropertyAccessExpression: {
        return parent.Name() == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindQualifiedName: {
        return parent.AsQualifiedName().Right == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindBindingElement: {
        return parent.PropertyName() == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindImportSpecifier: {
        return parent.PropertyName() == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindExportSpecifier || ᴛ == KindJsxAttribute || ᴛ == KindJsxSelfClosingElement || ᴛ == KindJsxOpeningElement || ᴛ == KindJsxClosingElement: {
        return true;
    }}

    return false;
}

public static bool IsPushOrUnshiftIdentifier(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    @string text = node.Text();
    return text == "push"u8 || text == "unshift"u8;
}

public static bool IsBooleanLiteral(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindTrueKeyword || node.Kind == KindFalseKeyword;
}

public static bool IsLiteralExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsLiteralKind(node.Kind);
}

public static bool IsStringLiteralLike(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindStringLiteral || ᴛ == KindNoSubstitutionTemplateLiteral: {
        return true;
    }}

    return false;
}

public static bool IsStringOrNumericLiteralLike(ж<Node> node) {
    return IsStringLiteralLike(node) || IsNumericLiteral(node);
}

public static bool IsSignedNumericLiteral(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == KindPrefixUnaryExpression) {
        var nodeΔ1 = node.AsPrefixUnaryExpression();
        return (nodeΔ1.Operator == KindPlusToken || nodeΔ1.Operator == KindMinusToken) && IsNumericLiteral(nodeΔ1.Operand);
    }
    return false;
}

public static bool IsOptionalChain(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((NodeFlags)(node.Flags & NodeFlagsOptionalChain) != 0) {
        switch (node.Kind) {
        case var ᴛ when ᴛ == KindPropertyAccessExpression || ᴛ == KindElementAccessExpression || ᴛ == KindCallExpression || ᴛ == KindNonNullExpression: {
            return true;
        }}

    }
    return false;
}

internal static ж<TokenNode> getQuestionDotToken(ж<Expression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.QuestionDotToken();
}

public static bool IsOptionalChainRoot(ж<Expression> node) {
    return IsOptionalChain(node) && !IsNonNullExpression(node) && getQuestionDotToken(node) != nil;
}

public static bool IsOutermostOptionalChain(ж<Expression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    return !IsOptionalChain(parent) || IsOptionalChainRoot(parent) || Ꮡnode != parent.Expression();
}

public static bool IsExpressionOfOptionalChainRoot(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsOptionalChainRoot(node.Parent) && node.Parent.Expression() == Ꮡnode;
}

public static bool IsNullishCoalesce(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindBinaryExpression && node.AsBinaryExpression().OperatorToken.Kind == KindQuestionQuestionToken;
}

public static bool IsAssertionExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var kind = node.Kind;
    return kind == KindTypeAssertionExpression || kind == KindAsExpression;
}

internal static bool isLeftHandSideExpressionKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindPropertyAccessExpression || ᴛ == KindElementAccessExpression || ᴛ == KindNewExpression || ᴛ == KindCallExpression || ᴛ == KindJsxElement || ᴛ == KindJsxSelfClosingElement || ᴛ == KindJsxFragment || ᴛ == KindTaggedTemplateExpression || ᴛ == KindArrayLiteralExpression || ᴛ == KindParenthesizedExpression || ᴛ == KindObjectLiteralExpression || ᴛ == KindClassExpression || ᴛ == KindFunctionExpression || ᴛ == KindIdentifier || ᴛ == KindPrivateIdentifier || ᴛ == KindRegularExpressionLiteral || ᴛ == KindNumericLiteral || ᴛ == KindBigIntLiteral || ᴛ == KindStringLiteral || ᴛ == KindNoSubstitutionTemplateLiteral || ᴛ == KindTemplateExpression || ᴛ == KindFalseKeyword || ᴛ == KindNullKeyword || ᴛ == KindThisKeyword || ᴛ == KindTrueKeyword || ᴛ == KindSuperKeyword || ᴛ == KindNonNullExpression || ᴛ == KindExpressionWithTypeArguments || ᴛ == KindMetaProperty || ᴛ == KindImportKeyword || ᴛ == KindMissingDeclaration: {
        return true;
    }}

    return false;
}

public static bool IsLeftHandSideExpression(ж<Node> node) {
    return isLeftHandSideExpressionKind(SkipPartiallyEmittedExpressions(node).Kind);
}

internal static bool isUnaryExpressionKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindPrefixUnaryExpression || ᴛ == KindPostfixUnaryExpression || ᴛ == KindDeleteExpression || ᴛ == KindTypeOfExpression || ᴛ == KindVoidExpression || ᴛ == KindAwaitExpression || ᴛ == KindTypeAssertionExpression: {
        return true;
    }}

    return isLeftHandSideExpressionKind(kind);
}

public static bool IsUnaryExpression(ж<Node> node) {
    return isUnaryExpressionKind(SkipPartiallyEmittedExpressions(node).Kind);
}

internal static bool isExpressionKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindConditionalExpression || ᴛ == KindYieldExpression || ᴛ == KindArrowFunction || ᴛ == KindBinaryExpression || ᴛ == KindSpreadElement || ᴛ == KindAsExpression || ᴛ == KindOmittedExpression || ᴛ == KindPartiallyEmittedExpression || ᴛ == KindSatisfiesExpression: {
        return true;
    }}

    return isUnaryExpressionKind(kind);
}

public static bool IsExpression(ж<Node> node) {
    return isExpressionKind(SkipPartiallyEmittedExpressions(node).Kind);
}

public static bool IsCommaExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindBinaryExpression && node.AsBinaryExpression().OperatorToken.Kind == KindCommaToken;
}

public static bool IsCommaSequence(ж<Node> node) {
    return IsCommaExpression(node);
}

public static bool IsIterationStatement(ж<Node> Ꮡnode, bool lookInLabeledStatements) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindForStatement || ᴛ == KindForInStatement || ᴛ == KindForOfStatement || ᴛ == KindDoStatement || ᴛ == KindWhileStatement: {
        return true;
    }
    case var ᴛ when ᴛ == KindLabeledStatement: {
        return lookInLabeledStatements && IsIterationStatement(node.Statement(), lookInLabeledStatements);
    }}

    return false;
}

public static bool IsAccessExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindPropertyAccessExpression || node.Kind == KindElementAccessExpression;
}

internal static bool isFunctionLikeDeclarationKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindFunctionDeclaration || ᴛ == KindMethodDeclaration || ᴛ == KindConstructor || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindFunctionExpression || ᴛ == KindArrowFunction: {
        return true;
    }}

    return false;
}

public static bool IsFunctionLikeDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && isFunctionLikeDeclarationKind(node.Kind);
}

public static bool IsFunctionLikeKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindMethodSignature || ᴛ == KindCallSignature || ᴛ == KindJSDocSignature || ᴛ == KindConstructSignature || ᴛ == KindIndexSignature || ᴛ == KindFunctionType || ᴛ == KindConstructorType: {
        return true;
    }}

    return isFunctionLikeDeclarationKind(kind);
}

public static bool IsFunctionLike(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && IsFunctionLikeKind(node.Kind);
}

public static bool IsFunctionLikeOrClassStaticBlockDeclaration(ж<Node> node) {
    return node != nil && (IsFunctionLike(node) || IsClassStaticBlockDeclaration(node));
}

public static bool IsFunctionOrSourceFile(ж<Node> node) {
    return IsFunctionLike(node) || IsSourceFile(node);
}

public static bool IsClassLike(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindClassDeclaration || node.Kind == KindClassExpression;
}

public static bool IsClassOrInterfaceLike(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindClassDeclaration || node.Kind == KindClassExpression || node.Kind == KindInterfaceDeclaration;
}

public static bool IsClassElement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindConstructor || ᴛ == KindPropertyDeclaration || ᴛ == KindMethodDeclaration || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindIndexSignature || ᴛ == KindClassStaticBlockDeclaration || ᴛ == KindSemicolonClassElement: {
        return true;
    }}

    return false;
}

public static bool IsMethodOrAccessor(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindMethodDeclaration || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor: {
        return true;
    }}

    return false;
}

public static bool IsPrivateIdentifierClassElementDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (IsPropertyDeclaration(Ꮡnode) || IsMethodOrAccessor(Ꮡnode)) && IsPrivateIdentifier(node.Name());
}

public static bool IsObjectLiteralOrClassExpressionMethodOrAccessor(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var kind = node.Kind;
    return (kind == KindMethodDeclaration || kind == KindGetAccessor || kind == KindSetAccessor) && (node.Parent.Kind == KindObjectLiteralExpression || node.Parent.Kind == KindClassExpression);
}

public static bool IsTypeElement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindConstructSignature || ᴛ == KindCallSignature || ᴛ == KindPropertySignature || ᴛ == KindMethodSignature || ᴛ == KindIndexSignature || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindNotEmittedTypeElement: {
        return true;
    }}

    return false;
}

public static bool IsObjectLiteralElement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindPropertyAssignment || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindSpreadAssignment || ᴛ == KindMethodDeclaration || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor: {
        return true;
    }}

    return false;
}

public static bool IsObjectLiteralMethod(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && node.Kind == KindMethodDeclaration && node.Parent.Kind == KindObjectLiteralExpression;
}

public static bool IsAutoAccessorPropertyDeclaration(ж<Node> node) {
    return IsPropertyDeclaration(node) && HasAccessorModifier(node);
}

public static bool IsParameterPropertyDeclaration(ж<Node> node, ж<Node> Ꮡparent) {
    ref var parent = ref Ꮡparent.DerefOrNull();

    return IsParameterDeclaration(node) && HasSyntacticModifier(node, ModifierFlagsParameterPropertyModifier) && parent.Kind == KindConstructor;
}

public static bool IsJsxChild(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindJsxElement || ᴛ == KindJsxExpression || ᴛ == KindJsxSelfClosingElement || ᴛ == KindJsxText || ᴛ == KindJsxFragment: {
        return true;
    }}

    return false;
}

public static bool IsJsxAttributeLike(ж<Node> node) {
    return IsJsxAttribute(node) || IsJsxSpreadAttribute(node);
}

internal static bool isDeclarationStatementKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindFunctionDeclaration || ᴛ == KindMissingDeclaration || ᴛ == KindClassDeclaration || ᴛ == KindInterfaceDeclaration || ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration || ᴛ == KindEnumDeclaration || ᴛ == KindModuleDeclaration || ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindImportEqualsDeclaration || ᴛ == KindExportDeclaration || ᴛ == KindExportAssignment || ᴛ == KindNamespaceExportDeclaration: {
        return true;
    }}

    return false;
}

public static bool IsDeclarationStatement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return isDeclarationStatementKind(node.Kind);
}

internal static bool isStatementKindButNotDeclarationKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindBreakStatement || ᴛ == KindContinueStatement || ᴛ == KindDebuggerStatement || ᴛ == KindDoStatement || ᴛ == KindExpressionStatement || ᴛ == KindEmptyStatement || ᴛ == KindForInStatement || ᴛ == KindForOfStatement || ᴛ == KindForStatement || ᴛ == KindIfStatement || ᴛ == KindLabeledStatement || ᴛ == KindReturnStatement || ᴛ == KindSwitchStatement || ᴛ == KindThrowStatement || ᴛ == KindTryStatement || ᴛ == KindVariableStatement || ᴛ == KindWhileStatement || ᴛ == KindWithStatement || ᴛ == KindNotEmittedStatement: {
        return true;
    }}

    return false;
}

public static bool IsStatementButNotDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return isStatementKindButNotDeclarationKind(node.Kind);
}

public static bool IsStatement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var kind = node.Kind;
    return isStatementKindButNotDeclarationKind(kind) || isDeclarationStatementKind(kind) || isBlockStatement(Ꮡnode);
}

internal static bool isBlockStatement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind != KindBlock) {
        return false;
    }
    if (node.Parent != nil && (node.Parent.Kind == KindTryStatement || node.Parent.Kind == KindCatchClause)) {
        return false;
    }
    return !IsFunctionBlock(Ꮡnode);
}

public static bool IsFunctionBlock(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && node.Kind == KindBlock && node.Parent != nil && IsFunctionLike(node.Parent);
}

public static bool IsBlockOrCatchScoped(ж<Node> declaration) {
    return (NodeFlags)(GetCombinedNodeFlags(declaration) & NodeFlagsBlockScoped) != 0 || IsCatchClauseVariableDeclarationOrBindingElement(declaration);
}

public static bool IsCatchClauseVariableDeclarationOrBindingElement(ж<Node> declaration) {
    var node = GetRootDeclaration(declaration);
    return node.Kind == KindVariableDeclaration && node.Parent.Kind == KindCatchClause;
}

public static bool IsTypeNodeKind(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindAnyKeyword || ᴛ == KindUnknownKeyword || ᴛ == KindNumberKeyword || ᴛ == KindBigIntKeyword || ᴛ == KindObjectKeyword || ᴛ == KindBooleanKeyword || ᴛ == KindStringKeyword || ᴛ == KindSymbolKeyword || ᴛ == KindVoidKeyword || ᴛ == KindUndefinedKeyword || ᴛ == KindNeverKeyword || ᴛ == KindIntrinsicKeyword || ᴛ == KindExpressionWithTypeArguments || ᴛ == KindJSDocAllType || ᴛ == KindJSDocNullableType || ᴛ == KindJSDocNonNullableType || ᴛ == KindJSDocOptionalType || ᴛ == KindJSDocVariadicType: {
        return true;
    }}

    return kind >= KindFirstTypeNode && kind <= KindLastTypeNode;
}

public static bool IsTypeNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsTypeNodeKind(node.Kind);
}

public static bool IsJSDocKind(Kind kind) {
    return KindFirstJSDocNode <= kind && kind <= KindLastJSDocNode;
}

public static bool IsJSDocTypeAssertion(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil || !IsParenthesizedExpression(Ꮡnode) || !IsInJSFile(Ꮡnode)) {
        return false;
    }
    var expr = node.Expression();
    return IsAsExpression(expr) && expr.Type() != nil && (NodeFlags)(expr.Type().Flags & NodeFlagsReparsed) != 0;
}

public static bool IsPrologueDirective(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindExpressionStatement && node.Expression().Kind == KindStringLiteral;
}

partial struct OuterExpressionKinds /*num:uint16*/;

public static OuterExpressionKinds OEKParentheses => /* 1 << 0 */ 1;
public static OuterExpressionKinds OEKTypeAssertions => /* 1 << 1 */ 2;
public static OuterExpressionKinds OEKNonNullAssertions => /* 1 << 2 */ 4;
public static OuterExpressionKinds OEKPartiallyEmittedExpressions => /* 1 << 3 */ 8;
public static OuterExpressionKinds OEKExpressionsWithTypeArguments => /* 1 << 4 */ 16;
public static OuterExpressionKinds OEKSatisfies => /* 1 << 5 */ 32;
public static OuterExpressionKinds OEKExcludeJSDocTypeAssertion => /* 1 << 6 */ 64;
public static OuterExpressionKinds OEKAssignments => /* 1 << 7 */ 128;
public static OuterExpressionKinds OEKComma => /* 1 << 8 */ 256;
public static OuterExpressionKinds OEKAssertions => /* OEKTypeAssertions | OEKNonNullAssertions | OEKSatisfies */ 38;
public static OuterExpressionKinds OEKAll => /* OEKParentheses | OEKAssertions | OEKPartiallyEmittedExpressions | OEKExpressionsWithTypeArguments */ 63;
public static OuterExpressionKinds OEKAllExceptAssertionsOrExpressionsWithTypeArguments => /* OEKAll &^ OEKAssertions &^ OEKExpressionsWithTypeArguments */ 9;
public static OuterExpressionKinds OEKExpressionTypePassthrough => /* OEKParentheses | OEKAssignments | OEKComma */ 385;

public static bool IsOuterExpression(ж<Expression> Ꮡnode, OuterExpressionKinds kinds) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindParenthesizedExpression: {
        return (OuterExpressionKinds)(kinds & OEKParentheses) != 0 && !((OuterExpressionKinds)(kinds & OEKExcludeJSDocTypeAssertion) != 0 && IsJSDocTypeAssertion(Ꮡnode));
    }
    case var ᴛ when ᴛ == KindTypeAssertionExpression || ᴛ == KindAsExpression: {
        return (OuterExpressionKinds)(kinds & OEKTypeAssertions) != 0;
    }
    case var ᴛ when ᴛ == KindSatisfiesExpression: {
        return (OuterExpressionKinds)(kinds & ((OuterExpressionKinds)(OEKExpressionsWithTypeArguments | OEKSatisfies))) != 0;
    }
    case var ᴛ when ᴛ == KindExpressionWithTypeArguments: {
        return (OuterExpressionKinds)(kinds & OEKExpressionsWithTypeArguments) != 0;
    }
    case var ᴛ when ᴛ == KindNonNullExpression: {
        return (OuterExpressionKinds)(kinds & OEKNonNullAssertions) != 0;
    }
    case var ᴛ when ᴛ == KindPartiallyEmittedExpression: {
        return (OuterExpressionKinds)(kinds & OEKPartiallyEmittedExpressions) != 0;
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        switch (node.AsBinaryExpression().OperatorToken.Kind) {
        case var ᴛ2 when ᴛ2 == KindEqualsToken: {
            return (OuterExpressionKinds)(kinds & OEKAssignments) != 0;
        }
        case var ᴛ2 when ᴛ2 == KindCommaToken: {
            return (OuterExpressionKinds)(kinds & OEKComma) != 0;
        }}

        break;
    }}

    return false;
}

public static ж<Expression> SkipOuterExpressions(ж<Expression> Ꮡnode, OuterExpressionKinds kinds) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (IsOuterExpression(Ꮡnode, kinds)) {
        if (IsBinaryExpression(Ꮡnode)) {
            Ꮡnode = node.AsBinaryExpression().Right; node = ref Ꮡnode.DerefOrNull();
        } else {
            Ꮡnode = node.Expression(); node = ref Ꮡnode.DerefOrNull();
        }
    }
    return Ꮡnode;
}

public static ж<Expression> SkipParentheses(ж<Expression> node) {
    return SkipOuterExpressions(node, OEKParentheses);
}

public static ж<Node> SkipTypeParentheses(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (IsParenthesizedTypeNode(Ꮡnode)) {
        Ꮡnode = node.Type(); node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

public static ж<Expression> SkipPartiallyEmittedExpressions(ж<Expression> node) {
    return SkipOuterExpressions(node, OEKPartiallyEmittedExpressions);
}

public static ж<Node> WalkUpParenthesizedExpressions(ж<Expression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil && node.Kind == KindParenthesizedExpression) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

public static ж<Node> WalkUpParenthesizedTypes(ж<TypeNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil && node.Kind == KindParenthesizedType) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

public static ж<SourceFile> GetSourceFileOfNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil) {
        if (node.Kind == KindSourceFile) {
            return node.AsSourceFile();
        }
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return default;
}

internal static readonly ж<sync.Pool> ᏑsetParentInChildrenPool = new StaticBox<sync.Pool>(new sync.Pool(
    New: () => (newParentInChildrenSetter()).OrTypedNilFunc()
));
internal static ref sync.Pool setParentInChildrenPool => ref ((StaticBox<sync.Pool>)ᏑsetParentInChildrenPool).ValueSlot;

internal partial struct newParentInChildrenSetter_state /*dyn*/ {
    internal ж<Node> parent;
    internal Func<ж<Node>, bool> visit;
}

internal static Func<ж<Node>, bool> newParentInChildrenSetter() {
    ref var state = ref heap(new newParentInChildrenSetter_state(), out var Ꮡstate);

    state.visit = (ж<Node> node) => {
        if (Ꮡstate.Value.parent != nil) {
            node.Parent = Ꮡstate.Value.parent;
        }
        var saveParent = Ꮡstate.Value.parent;
        Ꮡstate.Value.parent = node;
        node.ForEachChild(Ꮡstate.Value.visit);
        Ꮡstate.Value.parent = saveParent;
        return false;
    };

    return state.visit;
}

public static void SetParentInChildren(ж<Node> node) {
    GoFrame ᒐ = default;
    try {
        var fn = ᏑsetParentInChildrenPool.Get()._<Func<ж<Node>, bool>>();
        defer(ᏑsetParentInChildrenPool.Put, (fn).OrTypedNilFunc(), ref ᒐ);
        fn(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

public static void SetImportsOfSourceFile(ж<SourceFile> Ꮡnode, slice<ж<LiteralLikeNode>> imports) {
    ref var node = ref Ꮡnode.DerefOrNull();

    node.imports = imports;
}

public static ж<Node> FindAncestor(ж<Node> Ꮡnode, Func<ж<Node>, bool> callback) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil) {
        if (callback(Ꮡnode)) {
            return Ꮡnode;
        }
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return default;
}

public static slice<ж<Node>> FindManyAncestors(ж<Node> Ꮡnode, params Span<Func<ж<Node>, bool>> callbacksʗp) {
    var callbacks = callbacksʗp.sslice();
    ref var node = ref Ꮡnode.DerefOrNull();

    var ancestors = new slice<ж<Node>>(len(callbacks));
    nint found = 0;
    while (Ꮡnode != nil) {
        foreach (var (i, callback) in range(callbacks)) {
            if (ancestors[i] == nil && callback(Ꮡnode)) {
                ancestors[i] = Ꮡnode;
                found++;
                if (found == len(callbacks)) {
                    return ancestors;
                }
                break;
            }
        }
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return ancestors;
}

public static ж<Node> FindAncestorKind(ж<Node> Ꮡnode, Kind kind) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil) {
        if (node.Kind == kind) {
            return Ꮡnode;
        }
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return default;
}

partial struct FindAncestorResult /*num:int32*/;

public static FindAncestorResult FindAncestorFalse => /* iota */ 0;
public static FindAncestorResult FindAncestorTrue => 1;
public static FindAncestorResult FindAncestorQuit => 2;

public static FindAncestorResult ToFindAncestorResult(bool b) {
    if (b) {
        return FindAncestorTrue;
    }
    return FindAncestorFalse;
}

public static ж<Node> FindAncestorOrQuit(ж<Node> Ꮡnode, Func<ж<Node>, FindAncestorResult> callback) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil) {
        switch (callback(Ꮡnode)) {
        case var ᴛ when ᴛ == FindAncestorQuit: {
            return default;
        }
        case var ᴛ when ᴛ == FindAncestorTrue: {
            return Ꮡnode;
        }}

        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return default;
}

public static bool IsNodeDescendantOf(ж<Node> Ꮡnode, ж<Node> ancestor) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil) {
        if (Ꮡnode == ancestor) {
            return true;
        }
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return false;
}

public static ModifierFlags ModifierToFlag(Kind token) {
    switch (token) {
    case var ᴛ when ᴛ == KindStaticKeyword: {
        return ModifierFlagsStatic;
    }
    case var ᴛ when ᴛ == KindPublicKeyword: {
        return ModifierFlagsPublic;
    }
    case var ᴛ when ᴛ == KindProtectedKeyword: {
        return ModifierFlagsProtected;
    }
    case var ᴛ when ᴛ == KindPrivateKeyword: {
        return ModifierFlagsPrivate;
    }
    case var ᴛ when ᴛ == KindAbstractKeyword: {
        return ModifierFlagsAbstract;
    }
    case var ᴛ when ᴛ == KindAccessorKeyword: {
        return ModifierFlagsAccessor;
    }
    case var ᴛ when ᴛ == KindExportKeyword: {
        return ModifierFlagsExport;
    }
    case var ᴛ when ᴛ == KindDeclareKeyword: {
        return ModifierFlagsAmbient;
    }
    case var ᴛ when ᴛ == KindConstKeyword: {
        return ModifierFlagsConst;
    }
    case var ᴛ when ᴛ == KindDefaultKeyword: {
        return ModifierFlagsDefault;
    }
    case var ᴛ when ᴛ == KindAsyncKeyword: {
        return ModifierFlagsAsync;
    }
    case var ᴛ when ᴛ == KindReadonlyKeyword: {
        return ModifierFlagsReadonly;
    }
    case var ᴛ when ᴛ == KindOverrideKeyword: {
        return ModifierFlagsOverride;
    }
    case var ᴛ when ᴛ == KindInKeyword: {
        return ModifierFlagsIn;
    }
    case var ᴛ when ᴛ == KindOutKeyword: {
        return ModifierFlagsOut;
    }
    case var ᴛ when ᴛ == KindDecorator: {
        return ModifierFlagsDecorator;
    }}

    return ModifierFlagsNone;
}

public static ModifierFlags ModifiersToFlags(slice<ж<Node>> modifiers) {
    ModifierFlags flags = default;
    foreach (var (_, modifier) in range(modifiers)) {
        flags |= (ModifierFlags)(ModifierToFlag(modifier.Kind));
    }
    return flags;
}

public static bool HasSyntacticModifier(ж<Node> Ꮡnode, ModifierFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ModifierFlags)(node.ModifierFlags() & flags) != 0;
}

public static bool HasAccessorModifier(ж<Node> node) {
    return HasSyntacticModifier(node, ModifierFlagsAccessor);
}

public static bool HasStaticModifier(ж<Node> node) {
    return HasSyntacticModifier(node, ModifierFlagsStatic);
}

public static bool IsStatic(ж<Node> node) {
    return IsClassElement(node) && HasStaticModifier(node) || IsClassStaticBlockDeclaration(node);
}

public static bool CanHaveSymbol(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindArrowFunction || ᴛ == KindBinaryExpression || ᴛ == KindBindingElement || ᴛ == KindCallExpression || ᴛ == KindCallSignature || ᴛ == KindClassDeclaration || ᴛ == KindClassExpression || ᴛ == KindClassStaticBlockDeclaration || ᴛ == KindConstructor || ᴛ == KindConstructorType || ᴛ == KindConstructSignature || ᴛ == KindElementAccessExpression || ᴛ == KindEnumDeclaration || ᴛ == KindEnumMember || ᴛ == KindExportAssignment || ᴛ == KindExportDeclaration || ᴛ == KindExportSpecifier || ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindFunctionType || ᴛ == KindGetAccessor || ᴛ == KindImportClause || ᴛ == KindImportEqualsDeclaration || ᴛ == KindImportSpecifier || ᴛ == KindIndexSignature || ᴛ == KindInterfaceDeclaration || ᴛ == KindJSTypeAliasDeclaration || ᴛ == KindJsxAttribute || ᴛ == KindJsxAttributes || ᴛ == KindJsxSpreadAttribute || ᴛ == KindMappedType || ᴛ == KindMethodDeclaration || ᴛ == KindMethodSignature || ᴛ == KindModuleDeclaration || ᴛ == KindNamedTupleMember || ᴛ == KindNamespaceExport || ᴛ == KindNamespaceExportDeclaration || ᴛ == KindNamespaceImport || ᴛ == KindNewExpression || ᴛ == KindNoSubstitutionTemplateLiteral || ᴛ == KindNumericLiteral || ᴛ == KindObjectLiteralExpression || ᴛ == KindParameter || ᴛ == KindPropertyAccessExpression || ᴛ == KindPropertyAssignment || ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindSetAccessor || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindSourceFile || ᴛ == KindSpreadAssignment || ᴛ == KindStringLiteral || ᴛ == KindTypeAliasDeclaration || ᴛ == KindTypeLiteral || ᴛ == KindTypeParameter || ᴛ == KindVariableDeclaration: {
        return true;
    }}

    return false;
}

public static bool CanHaveIllegalDecorators(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindPropertyAssignment || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindFunctionDeclaration || ᴛ == KindConstructor || ᴛ == KindIndexSignature || ᴛ == KindClassStaticBlockDeclaration || ᴛ == KindMissingDeclaration || ᴛ == KindVariableStatement || ᴛ == KindInterfaceDeclaration || ᴛ == KindTypeAliasDeclaration || ᴛ == KindEnumDeclaration || ᴛ == KindModuleDeclaration || ᴛ == KindImportEqualsDeclaration || ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindNamespaceExportDeclaration || ᴛ == KindExportDeclaration || ᴛ == KindExportAssignment: {
        return true;
    }}

    return false;
}

public static bool CanHaveIllegalModifiers(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindClassStaticBlockDeclaration || ᴛ == KindPropertyAssignment || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindMissingDeclaration || ᴛ == KindNamespaceExportDeclaration: {
        return true;
    }}

    return false;
}

public static bool CanHaveModifiers(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindTypeParameter || ᴛ == KindParameter || ᴛ == KindPropertySignature || ᴛ == KindPropertyDeclaration || ᴛ == KindMethodSignature || ᴛ == KindMethodDeclaration || ᴛ == KindConstructor || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindIndexSignature || ᴛ == KindConstructorType || ᴛ == KindFunctionExpression || ᴛ == KindArrowFunction || ᴛ == KindClassExpression || ᴛ == KindVariableStatement || ᴛ == KindFunctionDeclaration || ᴛ == KindClassDeclaration || ᴛ == KindInterfaceDeclaration || ᴛ == KindTypeAliasDeclaration || ᴛ == KindEnumDeclaration || ᴛ == KindModuleDeclaration || ᴛ == KindImportEqualsDeclaration || ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindExportAssignment || ᴛ == KindExportDeclaration: {
        return true;
    }}

    return false;
}

public static bool CanHaveDecorators(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindParameter || ᴛ == KindPropertyDeclaration || ᴛ == KindMethodDeclaration || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindClassExpression || ᴛ == KindClassDeclaration: {
        return true;
    }}

    return false;
}

public static bool IsFunctionOrModuleBlock(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsSourceFile(Ꮡnode) || IsModuleBlock(Ꮡnode) || IsBlock(Ꮡnode) && IsFunctionLike(node.Parent);
}

public static bool IsFunctionExpressionOrArrowFunction(ж<Node> node) {
    return IsFunctionExpression(node) || IsArrowFunction(node);
}

public static bool ForEachReturnStatement(ж<Node> body, Func<ж<Node>, bool> visitor) {
    ref var traverse = ref heap<Func<ж<Node>, bool>>(out var Ꮡtraverse);
    traverse = (ж<Node> node) => {
        switch (node.Kind) {
        case var ᴛ when ᴛ == KindReturnStatement: {
            return visitor(node);
        }
        case var ᴛ when ᴛ == KindCaseBlock || ᴛ == KindBlock || ᴛ == KindIfStatement || ᴛ == KindDoStatement || ᴛ == KindWhileStatement || ᴛ == KindForStatement || ᴛ == KindForInStatement || ᴛ == KindForOfStatement || ᴛ == KindWithStatement || ᴛ == KindSwitchStatement || ᴛ == KindCaseClause || ᴛ == KindDefaultClause || ᴛ == KindLabeledStatement || ᴛ == KindTryStatement || ᴛ == KindCatchClause: {
            return node.ForEachChild(Ꮡtraverse.ValueSlot);
        }}

        return false;
    };
    return traverse(body);
}

public static ж<Node> GetRootDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (node.Kind == KindBindingElement) {
        Ꮡnode = node.Parent.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

public static ModifierFlags GetCombinedModifierFlags(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = GetRootDeclaration(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    var flags = node.ModifierFlags();
    if (node.Kind == KindVariableDeclaration) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    if (Ꮡnode != nil && node.Kind == KindVariableDeclarationList) {
        flags |= (ModifierFlags)(node.ModifierFlags());
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    if (Ꮡnode != nil && node.Kind == KindVariableStatement) {
        flags |= (ModifierFlags)(node.ModifierFlags());
    }
    return flags;
}

public static NodeFlags GetCombinedNodeFlags(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = GetRootDeclaration(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    var flags = node.Flags;
    if (node.Kind == KindVariableDeclaration) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    if (Ꮡnode != nil && node.Kind == KindVariableDeclarationList) {
        flags |= (NodeFlags)(node.Flags);
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    if (Ꮡnode != nil && node.Kind == KindVariableStatement) {
        flags |= (NodeFlags)(node.Flags);
    }
    return flags;
}

public static bool IsVarAwaitUsing(ж<Node> node) {
    return (NodeFlags)(GetCombinedNodeFlags(node) & NodeFlagsBlockScoped) == NodeFlagsAwaitUsing;
}

public static bool IsVarUsing(ж<Node> node) {
    return (NodeFlags)(GetCombinedNodeFlags(node) & NodeFlagsBlockScoped) == NodeFlagsUsing;
}

public static ж<Node> GetJSDocDeprecatedTag(ж<Node> node) {
    foreach (var (_, jsdoc) in range(node.JSDoc(nil))) {
        var tags = jsdoc.AsJSDoc().Tags;
        if (tags != nil) {
            foreach (var (_, tag) in range(tags.Nodes)) {
                if (IsJSDocDeprecatedTag(tag)) {
                    return tag;
                }
            }
        }
    }
    return default;
}

public static bool IsDeprecatedDeclaration(ж<Node> declaration) {
    return IsDeprecatedDeclarationWithCachedFlags(declaration, GetCombinedNodeFlags(declaration));
}

public static bool IsDeprecatedDeclarationWithCachedFlags(ж<Node> declaration, NodeFlags combinedFlags) {
    if ((NodeFlags)(combinedFlags & NodeFlagsPossiblyContainsDeprecatedTag) == 0) {
        return false;
    }
    for (var n = declaration; n != nil; n = n.Parent) {
        if ((NodeFlags)(n.Flags & NodeFlagsPossiblyContainsDeprecatedTag) != 0) {
            return GetJSDocDeprecatedTag(n) != nil;
        }
    }
    return false;
}

public static bool IsVarConst(ж<Node> node) {
    return (NodeFlags)(GetCombinedNodeFlags(node) & NodeFlagsBlockScoped) == NodeFlagsConst;
}

public static bool IsVarConstLike(ж<Node> node) {
    switch ((NodeFlags)(GetCombinedNodeFlags(node) & NodeFlagsBlockScoped)) {
    case var ᴛ when ᴛ == NodeFlagsConst || ᴛ == NodeFlagsUsing || ᴛ == NodeFlagsAwaitUsing: {
        return true;
    }}

    return false;
}

public static bool IsVarLet(ж<Node> node) {
    return (NodeFlags)(GetCombinedNodeFlags(node) & NodeFlagsBlockScoped) == NodeFlagsLet;
}

public static bool IsImportMeta(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == KindMetaProperty) {
        return node.AsMetaProperty().KeywordToken == KindImportKeyword && node.AsMetaProperty().Name().Text() == "meta"u8;
    }
    return false;
}

public static ж<Node> WalkUpBindingElementsAndPatterns(ж<Node> Ꮡbinding) {
    ref var binding = ref Ꮡbinding.DerefOrNull();

    var node = binding.Parent;
    while (IsBindingElement(node.Parent)) {
        node = node.Parent.Parent;
    }
    return node.Parent;
}

public static bool IsSourceFileJS(ж<SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    return @file.ScriptKind == core.ScriptKindJS || @file.ScriptKind == core.ScriptKindJSX;
}

public static bool IsInJSFile(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && (NodeFlags)(node.Flags & NodeFlagsJavaScriptFile) != 0;
}

public static bool IsDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == KindTypeParameter) {
        return node.Parent != nil;
    }
    return IsDeclarationNode(Ꮡnode);
}

public static bool IsDeclarationName(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    return !IsSourceFile(Ꮡname) && !IsBindingPattern(Ꮡname) && IsDeclaration(name.Parent) && name.Parent.Name() == Ꮡname;
}

public static bool IsDeclarationNameOrImportPropertyName(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    switch (name.Parent.Kind) {
    case var ᴛ when ᴛ == KindImportSpecifier || ᴛ == KindExportSpecifier: {
        return IsIdentifier(Ꮡname) || name.Kind == KindStringLiteral;
    }
    default: {
        return IsDeclarationName(Ꮡname);
    }}

}

public static bool IsLiteralComputedPropertyDeclarationName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsStringOrNumericLiteralLike(Ꮡnode) && node.Parent.Kind == KindComputedPropertyName && IsDeclaration(node.Parent.Parent);
}

public static bool IsExternalModuleImportEqualsDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindImportEqualsDeclaration && node.AsImportEqualsDeclaration().ModuleReference.Kind == KindExternalModuleReference;
}

public static bool IsModuleOrEnumDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindModuleDeclaration || node.Kind == KindEnumDeclaration;
}

public static bool IsLiteralImportTypeNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsImportTypeNode(Ꮡnode) && IsLiteralTypeNode(node.AsImportTypeNode().Argument) && IsStringLiteral(node.AsImportTypeNode().Argument.AsLiteralTypeNode().Literal);
}

public static bool IsJsxTagName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    switch (parent.Kind) {
    case var ᴛ when ᴛ == KindJsxOpeningElement || ᴛ == KindJsxClosingElement || ᴛ == KindJsxSelfClosingElement: {
        return parent.TagName() == Ꮡnode;
    }}

    return false;
}

public static bool IsImportOrExportSpecifier(ж<Node> node) {
    return IsImportSpecifier(node) || IsExportSpecifier(node);
}

public static bool IsVoidZero(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsVoidExpression(Ꮡnode) && IsNumericLiteral(node.Expression()) && node.Expression().Text() == "0"u8;
}

public static bool IsExportsIdentifier(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsIdentifier(Ꮡnode) && node.Text() == "exports"u8;
}

public static bool IsModuleIdentifier(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsIdentifier(Ꮡnode) && node.Text() == "module"u8;
}

public static bool IsThisIdentifier(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsIdentifier(Ꮡnode) && node.Text() == "this"u8;
}

public static bool IsThisParameter(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsParameterDeclaration(Ꮡnode) && node.Name() != nil && IsThisIdentifier(node.Name());
}

public static bool IsBindableStaticAccessExpression(ж<Node> Ꮡnode, bool excludeThisKeyword) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsPropertyAccessExpression(Ꮡnode) && (!excludeThisKeyword && node.Expression().Kind == KindThisKeyword || IsIdentifier(node.Name()) && IsBindableStaticNameExpression(node.Expression(), true)) || IsBindableStaticElementAccessExpression(Ꮡnode, excludeThisKeyword);
}

public static bool IsBindableStaticElementAccessExpression(ж<Node> Ꮡnode, bool excludeThisKeyword) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsLiteralLikeElementAccess(Ꮡnode) && ((!excludeThisKeyword && node.Expression().Kind == KindThisKeyword) || IsEntityNameExpression(node.Expression()) || IsBindableStaticAccessExpression(node.Expression(), true));
}

public static bool IsPrototypeAccess(ж<Node> node) {
    if (IsBindableStaticAccessExpression(node, false)) {
        var name = GetElementOrPropertyAccessName(node); if (name != nil) {
            return name.Text() == "prototype"u8;
        }
    }
    return false;
}

public static bool IsLiteralLikeElementAccess(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsElementAccessExpression(Ꮡnode) && IsStringOrNumericLiteralLike(node.AsElementAccessExpression().ArgumentExpression);
}

public static bool IsBindableStaticNameExpression(ж<Node> node, bool excludeThisKeyword) {
    return IsEntityNameExpression(node) || IsBindableStaticAccessExpression(node, excludeThisKeyword);
}

public static ж<Node> GetElementOrPropertyAccessName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        if (IsIdentifier(node.Name())) {
            return node.Name();
        }
        return default;
    }
    case var ᴛ when ᴛ == KindElementAccessExpression: {
        var arg = SkipParentheses(node.AsElementAccessExpression().ArgumentExpression); if (IsStringOrNumericLiteralLike(arg)) {
            return arg;
        }
        return default;
    }}

    throw panic("Unhandled case in GetElementOrPropertyAccessName");
}

public static ж<Expression> GetInitializerOfBinaryExpression(ж<BinaryExpression> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    while (IsBinaryExpression(expr.Right)) {
        Ꮡexpr = expr.Right.AsBinaryExpression(); expr = ref Ꮡexpr.DerefOrNull();
    }
    return expr.Right.Expression();
}

public static bool IsExpressionWithTypeArgumentsInClassExtendsClause(ж<Node> node) {
    return TryGetClassExtendingExpressionWithTypeArguments(node) != nil;
}

public static ж<ClassLikeDeclaration> TryGetClassExtendingExpressionWithTypeArguments(ж<Node> node) {
    if (!IsExpressionWithTypeArguments(node)) {
        return default;
    }
    var (cls, isImplements) = TryGetClassImplementingOrExtendingHeritageClauseElement(node);
    if (cls != nil && !isImplements) {
        return cls;
    }
    return default;
}

public static (ж<ClassLikeDeclaration> @class, bool isImplements) TryGetClassImplementingOrExtendingHeritageClauseElement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((IsExpressionWithTypeArguments(Ꮡnode) || IsTypeReferenceNode(Ꮡnode)) && IsHeritageClause(node.Parent) && IsClassLike(node.Parent.Parent)) {
        var ᴛ1 = node.Parent.AsHeritageClause().Token == KindImplementsKeyword;
        return (node.Parent.Parent, ᴛ1);
    }
    return (default, false);
}

public static ж<Node> GetNameOfDeclaration(ж<Node> declaration) {
    if (declaration == nil) {
        return default;
    }
    var nonAssignedName = GetNonAssignedNameOfDeclaration(declaration);
    if (nonAssignedName != nil) {
        return nonAssignedName;
    }
    if (IsFunctionExpression(declaration) || IsArrowFunction(declaration) || IsClassExpression(declaration)) {
        return GetAssignedName(declaration);
    }
    return default;
}

public static ж<Node> GetNonAssignedNameOfDeclaration(ж<Node> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    switch (declaration.Kind) {
    case var ᴛ when ᴛ == KindBinaryExpression || ᴛ == KindCallExpression: {
        switch (GetAssignmentDeclarationKind(Ꮡdeclaration)) {
        case var ᴛ2 when ᴛ2 == JSDeclarationKindProperty || ᴛ2 == JSDeclarationKindThisProperty || ᴛ2 == JSDeclarationKindExportsProperty: {
            var left = declaration.AsBinaryExpression().Left;
            var name = GetElementOrPropertyAccessName(left); if (name != nil) {
                return name;
            }
            return left;
        }
        case var ᴛ2 when ᴛ2 == JSDeclarationKindObjectDefinePropertyValue || ᴛ2 == JSDeclarationKindObjectDefinePropertyExports: {
            return declaration.Arguments()[1];
        }}

        return default;
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        var expr = declaration.Expression();
        if (IsIdentifier(expr)) {
            return expr;
        }
        return default;
    }}

    return declaration.Name();
}

public static ж<Node> GetAssignedName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (parent != nil) {
        switch (parent.Kind) {
        case var ᴛ when ᴛ == KindPropertyAssignment: {
            return parent.AsPropertyAssignment().Name();
        }
        case var ᴛ when ᴛ == KindBindingElement: {
            return parent.AsBindingElement().Name();
        }
        case var ᴛ when ᴛ == KindBinaryExpression: {
            if (Ꮡnode == parent.AsBinaryExpression().Right) {
                var left = parent.AsBinaryExpression().Left;
                switch (left.Kind) {
                case var ᴛ2 when ᴛ2 == KindIdentifier: {
                    return left;
                }
                case var ᴛ2 when ᴛ2 == KindPropertyAccessExpression: {
                    return left.AsPropertyAccessExpression().Name();
                }
                case var ᴛ2 when ᴛ2 == KindElementAccessExpression: {
                    var arg = SkipParentheses(left.AsElementAccessExpression().ArgumentExpression);
                    if (IsStringOrNumericLiteralLike(arg)) {
                        return arg;
                    }
                    break;
                }}

            }
            break;
        }
        case var ᴛ when ᴛ == KindVariableDeclaration: {
            var name = parent.AsVariableDeclaration().Name();
            if (IsIdentifier(name)) {
                return name;
            }
            break;
        }}

    }
    return default;
}

partial struct JSDeclarationKind /*num:nint*/;

public static JSDeclarationKind JSDeclarationKindNone => /* iota */ 0;
public static JSDeclarationKind JSDeclarationKindModuleExports => 1;
public static JSDeclarationKind JSDeclarationKindExportsProperty => 2;
public static JSDeclarationKind JSDeclarationKindThisProperty => 3;
public static JSDeclarationKind JSDeclarationKindProperty => 4;
public static JSDeclarationKind JSDeclarationKindObjectDefinePropertyValue => 5;
public static JSDeclarationKind JSDeclarationKindObjectDefinePropertyExports => 6;

public static JSDeclarationKind GetAssignmentDeclarationKind(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindBinaryExpression: {
        var bin = node.AsBinaryExpression();
        if (bin.OperatorToken.Kind == KindEqualsToken && IsAccessExpression(bin.Left)) {
            if (IsInJSFile(bin.Left)) {
                if (IsModuleExportsAccessExpression(bin.Left) && !IsExportsIdentifier(bin.Right)) {
                    return JSDeclarationKindModuleExports;
                }
                if ((IsModuleExportsAccessExpression(bin.Left.Expression()) || IsExportsIdentifier(bin.Left.Expression())) && GetElementOrPropertyAccessName(bin.Left) != nil) {
                    return JSDeclarationKindExportsProperty;
                }
                if (bin.Left.Expression().Kind == KindThisKeyword) {
                    return JSDeclarationKindThisProperty;
                }
            }
            if (bin.Left.Kind == KindPropertyAccessExpression && IsEntityNameExpressionEx(bin.Left.Expression(), IsInJSFile(bin.Left)) && IsIdentifier(bin.Left.Name()) || bin.Left.Kind == KindElementAccessExpression && IsEntityNameExpressionEx(bin.Left.Expression(), IsInJSFile(bin.Left))) {
                return JSDeclarationKindProperty;
            }
        }
        break;
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        if (IsInJSFile(Ꮡnode) && IsBindableObjectDefinePropertyCall(Ꮡnode)) {
            var entityName = node.Arguments()[0];
            if (IsExportsIdentifier(entityName) || IsModuleExportsAccessExpression(entityName)) {
                return JSDeclarationKindObjectDefinePropertyExports;
            }
            return JSDeclarationKindObjectDefinePropertyValue;
        }
        break;
    }}

    return JSDeclarationKindNone;
}

public static bool IsBindableObjectDefinePropertyCall(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var args = node.Arguments(); if (len(args) == 3) {
        var expr = node.Expression(); if (IsPropertyAccessExpression(expr) && IsIdentifier(expr.Expression()) && expr.Expression().Text() == "Object"u8 && expr.Name().Text() == "defineProperty"u8 && IsStringOrNumericLiteralLike(args[1]) && IsBindableStaticNameExpression(args[0], true)) {
            return true;
        }
    }
    return false;
}

public static bool HasDynamicName(ж<Node> declaration) {
    var name = GetNameOfDeclaration(declaration);
    return name != nil && IsDynamicName(name);
}

public static bool IsDynamicName(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    ж<Node> expr = default;
    switch (name.Kind) {
    case var ᴛ when ᴛ == KindComputedPropertyName: {
        expr = name.Expression();
        break;
    }
    case var ᴛ when ᴛ == KindElementAccessExpression: {
        expr = SkipParentheses(name.AsElementAccessExpression().ArgumentExpression);
        break;
    }
    default: {
        return false;
    }}

    return !IsStringOrNumericLiteralLike(expr) && !IsSignedNumericLiteral(expr);
}

public static bool IsEntityNameExpression(ж<Node> node) {
    return IsEntityNameExpressionEx(node, false);
}

public static bool IsEntityNameExpressionEx(ж<Node> Ꮡnode, bool allowJS) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsIdentifier(Ꮡnode) || IsPropertyAccessEntityNameExpression(Ꮡnode, allowJS) || allowJS && (node.Kind == KindThisKeyword || isElementAccessEntityNameExpression(Ꮡnode, allowJS));
}

public static bool IsPropertyAccessEntityNameExpression(ж<Node> Ꮡnode, bool allowJS) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsPropertyAccessExpression(Ꮡnode) && IsIdentifier(node.Name()) && IsEntityNameExpressionEx(node.Expression(), allowJS);
}

internal static bool isElementAccessEntityNameExpression(ж<Node> Ꮡnode, bool allowJS) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsElementAccessExpression(Ꮡnode) && IsStringOrNumericLiteralLike(node.AsElementAccessExpression().ArgumentExpression) && IsEntityNameExpressionEx(node.Expression(), allowJS);
}

public static bool IsDottedName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindIdentifier || ᴛ == KindThisKeyword || ᴛ == KindSuperKeyword || ᴛ == KindMetaProperty: {
        return true;
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression || ᴛ == KindParenthesizedExpression: {
        return IsDottedName(node.Expression());
    }}

    return false;
}

public static bool HasSamePropertyAccessName(ж<Node> Ꮡnode1, ж<Node> Ꮡnode2) {
    ref var node1 = ref Ꮡnode1.DerefOrNull();
    ref var node2 = ref Ꮡnode2.DerefOrNull();

    if (node1.Kind == KindIdentifier && node2.Kind == KindIdentifier) {
        return node1.Text() == node2.Text();
    } else if (node1.Kind == KindPropertyAccessExpression && node2.Kind == KindPropertyAccessExpression) {
        return node1.AsPropertyAccessExpression().Name().Text() == node2.AsPropertyAccessExpression().Name().Text() && HasSamePropertyAccessName(node1.Expression(), node2.Expression());
    }
    return false;
}

public static bool IsAmbientModule(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsModuleDeclaration(Ꮡnode) && (node.AsModuleDeclaration().Name().Kind == KindStringLiteral || IsGlobalScopeAugmentation(Ꮡnode));
}

public static bool IsAmbientModuleSymbolName(@string s) {
    var (_, ok) = TryGetAmbientModuleNameFromSymbolName(s);
    return ok;
}

public static (@string, bool) TryGetAmbientModuleNameFromSymbolName(@string s) {
    if (strings.HasPrefix(s, "\"") && strings.HasSuffix(s, "\"")) {
        return (s.slice(1, len(s) - 1), true);
    }

    @string patternPrefix = InternalSymbolNamePrefix + "\"";
    var (rest, ok) = strings.CutPrefix(s, patternPrefix);
    if (!ok) {
        return ("", false);
    }
    nint markerIndex = strings.LastIndex(rest, "\"pattern@");
    if (markerIndex < 1) {
        return ("", false);
    }
    return (rest.slice(0, markerIndex), true);
}

public static bool IsExternalModule(ж<SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    return @file.ExternalModuleIndicator != nil;
}

public static bool IsExternalOrCommonJSModule(ж<SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    return @file.ExternalModuleIndicator != nil || @file.CommonJSModuleIndicator != nil;
}

public static bool IsEffectiveExternalModule(ж<SourceFile> Ꮡnode, ж<core.CompilerOptions> ᏑcompilerOptions) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    return IsExternalModule(Ꮡnode) || (isCommonJSContainingModuleKind(compilerOptions.GetEmitModuleKind()) && node.CommonJSModuleIndicator != nil);
}

internal static bool isCommonJSContainingModuleKind(core.ModuleKind kind) {
    return kind == core.ModuleKindCommonJS || core.ModuleKindNode16 <= kind && kind <= core.ModuleKindNodeNext;
}

public static bool IsExternalModuleIndicator(ж<Statement> node) {
    return IsAnyImportOrReExport(node) || IsExportAssignment(node) || HasSyntacticModifier(node, ModifierFlagsExport);
}

public static bool IsExportNamespaceAsDefaultDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsExportDeclaration(Ꮡnode)) {
        var decl = node.AsExportDeclaration();
        return IsNamespaceExport(decl.ExportClause) && ModuleExportNameIsDefault(decl.ExportClause.Name());
    }
    return false;
}

public static bool IsGlobalScopeAugmentation(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsModuleDeclaration(Ꮡnode) && node.AsModuleDeclaration().Keyword == KindGlobalKeyword;
}

public static bool IsModuleAugmentationExternal(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Parent.Kind) {
    case var ᴛ when ᴛ == KindSourceFile: {
        return IsExternalModule(node.Parent.AsSourceFile());
    }
    case var ᴛ when ᴛ == KindModuleBlock: {
        var grandParent = node.Parent.Parent;
        return IsAmbientModule(grandParent) && IsSourceFile(grandParent.Parent) && !IsExternalModule(grandParent.Parent.AsSourceFile());
    }}

    return false;
}

public static bool IsModuleWithStringLiteralName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsModuleDeclaration(Ꮡnode) && node.Name().Kind == KindStringLiteral;
}

public static ж<Node> GetContainingClass(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return FindAncestor(node.Parent, IsClassLike);
}

public static slice<ж<HeritageClauseElement>> GetExtendsHeritageClauseElements(ж<Node> node) {
    return GetHeritageElements(node, KindExtendsKeyword);
}

public static slice<ж<HeritageClauseElement>> GetImplementsHeritageClauseElements(ж<Node> node) {
    return GetHeritageElements(node, KindImplementsKeyword);
}

public static slice<ж<HeritageClauseElement>> GetHeritageElements(ж<Node> node, Kind kind) {
    var clause = GetHeritageClause(node, kind);
    if (clause != nil) {
        return clause.AsHeritageClause().Types.Nodes;
    }
    return default;
}

public static ж<Node> GetHeritageClauseElementName(ж<HeritageClauseElement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsTypeReferenceNode(Ꮡnode)) {
        return node.AsTypeReferenceNode().TypeName;
    }
    return (~node.AsExpressionWithTypeArguments()).Expression;
}

public static bool IsNameOfHeritageClauseTypeReference(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (IsQualifiedName(node.Parent)) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return IsTypeReferenceNode(node.Parent) && node.Parent.AsTypeReferenceNode().TypeName == Ꮡnode && IsHeritageClause(node.Parent.Parent);
}

public static ж<Node> GetHeritageClause(ж<Node> node, Kind kind) {
    var clauses = getHeritageClauses(node);
    if (clauses != nil) {
        foreach (var (_, clause) in range(clauses.Nodes)) {
            if (clause.AsHeritageClause().Token == kind) {
                return clause;
            }
        }
    }
    return default;
}

internal static ж<NodeList> getHeritageClauses(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindClassDeclaration: {
        return node.AsClassDeclaration().HeritageClauses;
    }
    case var ᴛ when ᴛ == KindClassExpression: {
        return node.AsClassExpression().HeritageClauses;
    }
    case var ᴛ when ᴛ == KindInterfaceDeclaration: {
        return node.AsInterfaceDeclaration().HeritageClauses;
    }}

    return default;
}

public static bool IsPartOfTypeQuery(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (node.Kind == KindQualifiedName || node.Kind == KindIdentifier) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return node.Kind == KindTypeQuery;
}

public static bool IsPartOfParameterDeclaration(ж<Node> node) {
    return GetRootDeclaration(node).Kind == KindParameter;
}

public static bool IsInTopLevelContext(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsIdentifier(Ꮡnode)) {
        var parent = node.Parent;
        if ((IsClassDeclaration(parent) || IsFunctionDeclaration(parent)) && parent.Name() == Ꮡnode) {
            Ꮡnode = parent; node = ref Ꮡnode.DerefOrNull();
        }
    }
    var container = GetThisContainer(Ꮡnode, true, false);
    return IsSourceFile(container);
}

public static ж<Node> GetThisContainer(ж<Node> Ꮡnode, bool includeArrowFunctions, bool includeClassComputedPropertyName) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (;;) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
        if (Ꮡnode == nil) {
            throw panic("nil parent in getThisContainer");
        }
        switch (node.Kind) {
        case var ᴛ when ᴛ == KindComputedPropertyName: {
            if (includeClassComputedPropertyName && IsClassLike(node.Parent.Parent)) {
                return Ꮡnode;
            }
            Ꮡnode = node.Parent.Parent; node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == KindDecorator: {
            if (node.Parent.Kind == KindParameter && IsClassElement(node.Parent.Parent)) {
                Ꮡnode = node.Parent.Parent; node = ref Ꮡnode.DerefOrNull();
            } else if (IsClassElement(node.Parent)) {
                Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
            }
            break;
        }
        case var ᴛ when ᴛ == KindArrowFunction: {
            if (includeArrowFunctions) {
                return Ꮡnode;
            }
            break;
        }
        case var ᴛ when ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindModuleDeclaration || ᴛ == KindClassStaticBlockDeclaration || ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindMethodDeclaration || ᴛ == KindMethodSignature || ᴛ == KindConstructor || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindCallSignature || ᴛ == KindConstructSignature || ᴛ == KindIndexSignature || ᴛ == KindEnumDeclaration || ᴛ == KindSourceFile: {
            return Ꮡnode;
        }}

    }
}

public static ж<Node> GetSuperContainer(ж<Node> Ꮡnode, bool stopOnFunctions) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (Ꮡnode = node.Parent, node = ref Ꮡnode.DerefOrNull(); Ꮡnode != nil; Ꮡnode = node.Parent, node = ref Ꮡnode.DerefOrNull()) {
        switch (node.Kind) {
        case var ᴛ when ᴛ == KindComputedPropertyName: {
            Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindArrowFunction: {
            if (!stopOnFunctions) {
                continue;
            }
            return Ꮡnode;
        }
        case var ᴛ when ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindMethodDeclaration || ᴛ == KindMethodSignature || ᴛ == KindConstructor || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindClassStaticBlockDeclaration: {
            return Ꮡnode;
        }
        case var ᴛ when ᴛ == KindDecorator: {
            if (node.Parent.Kind == KindParameter && IsClassElement(node.Parent.Parent)) {
                Ꮡnode = node.Parent.Parent; node = ref Ꮡnode.DerefOrNull();
            } else if (IsClassElement(node.Parent)) {
                Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
            }
            break;
        }}

    }
    return default;
}

public static ж<Node> GetImmediatelyInvokedFunctionExpression(ж<Node> Ꮡfn) {
    ref var fn = ref Ꮡfn.DerefOrNull();

    if (IsFunctionExpressionOrArrowFunction(Ꮡfn)) {
        var prev = Ꮡfn;
        var parent = fn.Parent;
        while (IsParenthesizedExpression(parent)) {
            prev = parent;
            parent = parent.Parent;
        }
        if (IsCallExpression(parent) && parent.Expression() == prev) {
            return parent;
        }
    }
    return default;
}

public static bool IsEnumConst(ж<Node> node) {
    return (ModifierFlags)(GetCombinedModifierFlags(node) & ModifierFlagsConst) != 0;
}

public static bool ExpressionIsAlias(ж<Node> node) {
    return IsEntityNameExpression(node) || IsClassExpression(node);
}

public static bool IsInstanceOfExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsBinaryExpression(Ꮡnode) && node.AsBinaryExpression().OperatorToken.Kind == KindInstanceOfKeyword;
}

public static bool IsAnyImportOrReExport(ж<Node> node) {
    return IsImportNode(node) || IsExportDeclaration(node);
}

public static bool IsImportNode(ж<Node> node) {
    return IsAnyImportSyntax(node) || NodeKindIs(node, KindJSImportDeclaration);
}

public static bool IsAnyImportSyntax(ж<Node> node) {
    return NodeKindIs(node, KindImportDeclaration, KindImportEqualsDeclaration);
}

public static bool IsJsonSourceFile(ж<SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    return @file.ScriptKind == core.ScriptKindJSON;
}

public static bool IsInJsonFile(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (NodeFlags)(node.Flags & NodeFlagsJsonFile) != 0;
}

public static ж<Expression> GetExternalModuleName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindExportDeclaration: {
        return node.ModuleSpecifier();
    }
    case var ᴛ when ᴛ == KindImportEqualsDeclaration: {
        if (node.AsImportEqualsDeclaration().ModuleReference.Kind == KindExternalModuleReference) {
            return node.AsImportEqualsDeclaration().ModuleReference.Expression();
        }
        return default;
    }
    case var ᴛ when ᴛ == KindImportType: {
        return getImportTypeNodeLiteral(Ꮡnode);
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        return core.FirstOrNil(node.Arguments());
    }
    case var ᴛ when ᴛ == KindModuleDeclaration: {
        if (IsStringLiteral(node.AsModuleDeclaration().Name())) {
            return node.AsModuleDeclaration().Name();
        }
        return default;
    }}

    throw panic("Unhandled case in getExternalModuleName");
}

public static bool HasImportAttributes(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindExportDeclaration || ᴛ == KindImportType: {
        return true;
    }}

    return false;
}

public static ж<Node> GetImportAttributes(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        return (~node.AsImportDeclaration()).Attributes;
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        return (~node.AsExportDeclaration()).Attributes;
    }
    case var ᴛ when ᴛ == KindImportType: {
        return (~node.AsImportTypeNode()).Attributes;
    }}

    throw panic("Unhandled case in getImportAttributes: " + node.Kind.String());
}

internal static ж<Node> getImportTypeNodeLiteral(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsImportTypeNode(Ꮡnode)) {
        var importTypeNode = node.AsImportTypeNode();
        if (IsLiteralTypeNode(importTypeNode.Argument)) {
            var literalTypeNode = importTypeNode.Argument.AsLiteralTypeNode();
            if (IsStringLiteral(literalTypeNode.Literal)) {
                return literalTypeNode.Literal;
            }
        }
    }
    return default;
}

public static bool IsExpressionNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var exprᴛ1 = node.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == KindSuperKeyword || exprᴛ1 == KindNullKeyword || exprᴛ1 == KindTrueKeyword || exprᴛ1 == KindFalseKeyword || exprᴛ1 == KindRegularExpressionLiteral || exprᴛ1 == KindArrayLiteralExpression || exprᴛ1 == KindObjectLiteralExpression || exprᴛ1 == KindPropertyAccessExpression || exprᴛ1 == KindElementAccessExpression || exprᴛ1 == KindCallExpression || exprᴛ1 == KindNewExpression || exprᴛ1 == KindTaggedTemplateExpression || exprᴛ1 == KindAsExpression || exprᴛ1 == KindTypeAssertionExpression || exprᴛ1 == KindSatisfiesExpression || exprᴛ1 == KindNonNullExpression || exprᴛ1 == KindParenthesizedExpression || exprᴛ1 == KindFunctionExpression || exprᴛ1 == KindClassExpression || exprᴛ1 == KindArrowFunction || exprᴛ1 == KindVoidExpression || exprᴛ1 == KindDeleteExpression || exprᴛ1 == KindTypeOfExpression || exprᴛ1 == KindPrefixUnaryExpression || exprᴛ1 == KindPostfixUnaryExpression || exprᴛ1 == KindBinaryExpression || exprᴛ1 == KindConditionalExpression || exprᴛ1 == KindSpreadElement || exprᴛ1 == KindTemplateExpression || exprᴛ1 == KindOmittedExpression || exprᴛ1 == KindJsxElement || exprᴛ1 == KindJsxSelfClosingElement || exprᴛ1 == KindJsxFragment || exprᴛ1 == KindYieldExpression || exprᴛ1 == KindAwaitExpression) { matchᴛ1 = true;
        return true;
    }
    if (exprᴛ1 == KindMetaProperty) { matchᴛ1 = true;
        return !IsImportCall(node.Parent) || node.Parent.Expression() != Ꮡnode;
    }
    if (exprᴛ1 == KindExpressionWithTypeArguments) { matchᴛ1 = true;
        return !IsHeritageClause(node.Parent);
    }
    if (exprᴛ1 == KindQualifiedName) { matchᴛ1 = true;
        while (node.Parent.Kind == KindQualifiedName) {
            Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
        }
        return IsTypeQueryNode(node.Parent) || IsJSDocLinkLike(node.Parent) || IsJSDocNameReference(node.Parent) || IsJsxTagName(Ꮡnode);
    }
    if (exprᴛ1 == KindPrivateIdentifier) { matchᴛ1 = true;
        return IsBinaryExpression(node.Parent) && node.Parent.AsBinaryExpression().Left == Ꮡnode && node.Parent.AsBinaryExpression().OperatorToken.Kind == KindInKeyword;
    }
    if (exprᴛ1 == KindIdentifier) { matchᴛ1 = true;
        if (IsTypeQueryNode(node.Parent) || IsJSDocLinkLike(node.Parent) || IsJSDocNameReference(node.Parent) || IsJsxTagName(Ꮡnode)) {
            return true;
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && (exprᴛ1 == KindNumericLiteral || exprᴛ1 == KindBigIntLiteral || exprᴛ1 == KindStringLiteral || exprᴛ1 == KindNoSubstitutionTemplateLiteral || exprᴛ1 == KindThisKeyword)) {
        return IsInExpressionContext(Ꮡnode);
    }
    if (!matchᴛ1) { /* default: */
        return false;
    }
    return default;

}

public static bool IsInExpressionContext(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    switch (parent.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindParameter || ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindEnumMember || ᴛ == KindPropertyAssignment || ᴛ == KindBindingElement: {
        return parent.Initializer() == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindExpressionStatement || ᴛ == KindIfStatement || ᴛ == KindDoStatement || ᴛ == KindWhileStatement || ᴛ == KindReturnStatement || ᴛ == KindWithStatement || ᴛ == KindSwitchStatement || ᴛ == KindCaseClause || ᴛ == KindDefaultClause || ᴛ == KindThrowStatement || ᴛ == KindTypeAssertionExpression || ᴛ == KindAsExpression || ᴛ == KindTemplateSpan || ᴛ == KindComputedPropertyName || ᴛ == KindSatisfiesExpression: {
        return parent.Expression() == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindForStatement: {
        var s = parent.AsForStatement();
        return (~s).Initializer == Ꮡnode && (~s).Initializer.Kind != KindVariableDeclarationList || s.Condition == Ꮡnode || s.Incrementor == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
        var s = parent.AsForInOrOfStatement();
        return (~s).Initializer == Ꮡnode && (~s).Initializer.Kind != KindVariableDeclarationList || (~s).Expression == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindDecorator || ᴛ == KindJsxExpression || ᴛ == KindJsxSpreadAttribute || ᴛ == KindSpreadAssignment: {
        return true;
    }
    case var ᴛ when ᴛ == KindExpressionWithTypeArguments: {
        return parent.Expression() == Ꮡnode && !IsPartOfTypeNode(parent);
    }
    case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
        return parent.AsShorthandPropertyAssignment().ObjectAssignmentInitializer == Ꮡnode;
    }
    default: {
        return IsExpressionNode(parent);
    }}

}

public static bool IsPartOfTypeNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var kind = node.Kind;
    if (kind >= KindFirstTypeNode && kind <= KindLastTypeNode) {
        return true;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == KindAnyKeyword || ᴛ == KindUnknownKeyword || ᴛ == KindNumberKeyword || ᴛ == KindBigIntKeyword || ᴛ == KindStringKeyword || ᴛ == KindBooleanKeyword || ᴛ == KindSymbolKeyword || ᴛ == KindObjectKeyword || ᴛ == KindUndefinedKeyword || ᴛ == KindNullKeyword || ᴛ == KindNeverKeyword: {
        return true;
    }
    case var ᴛ when ᴛ == KindVoidKeyword: {
        return node.Parent.Kind != KindVoidExpression;
    }
    case var ᴛ when ᴛ == KindExpressionWithTypeArguments: {
        return isPartOfTypeExpressionWithTypeArguments(ref (Ꮡnode).DerefOrNull());
    }
    case var ᴛ when ᴛ == KindTypeParameter: {
        return node.Parent.Kind == KindMappedType || node.Parent.Kind == KindInferType;
    }
    case var ᴛ when ᴛ == KindIdentifier: {
        var parent = node.Parent;
        if (IsQualifiedName(parent) && parent.AsQualifiedName().Right == Ꮡnode) {
            return isPartOfTypeNodeInParent(parent);
        }
        if (IsPropertyAccessExpression(parent) && parent.AsPropertyAccessExpression().Name() == Ꮡnode) {
            return isPartOfTypeNodeInParent(parent);
        }
        return isPartOfTypeNodeInParent(Ꮡnode);
    }
    case var ᴛ when ᴛ == KindQualifiedName || ᴛ == KindPropertyAccessExpression || ᴛ == KindThisKeyword: {
        return isPartOfTypeNodeInParent(Ꮡnode);
    }}

    return false;
}

internal static bool isPartOfTypeNodeInParent(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (parent.Kind == KindTypeQuery) {
        return false;
    }
    if (parent.Kind == KindImportType) {
        return !parent.AsImportTypeNode().IsTypeOf;
    }

    if (parent.Kind >= KindFirstTypeNode && parent.Kind <= KindLastTypeNode) {
        return true;
    }
    switch (parent.Kind) {
    case var ᴛ when ᴛ == KindExpressionWithTypeArguments: {
        return isPartOfTypeExpressionWithTypeArguments(ref (parent).DerefOrNull());
    }
    case var ᴛ when ᴛ == KindTypeParameter: {
        return Ꮡnode == parent.AsTypeParameterDeclaration().Constraint;
    }
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindParameter || ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindArrowFunction || ᴛ == KindConstructor || ᴛ == KindMethodDeclaration || ᴛ == KindMethodSignature || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindCallSignature || ᴛ == KindConstructSignature || ᴛ == KindIndexSignature || ᴛ == KindTypeAssertionExpression: {
        return Ꮡnode == parent.Type();
    }
    case var ᴛ when ᴛ == KindCallExpression || ᴛ == KindNewExpression || ᴛ == KindTaggedTemplateExpression: {
        return slices.Contains(parent.TypeArguments(), Ꮡnode);
    }}

    return false;
}

internal static bool isPartOfTypeExpressionWithTypeArguments(ref Node node) {
    var parent = node.Parent;
    return IsHeritageClause(parent) && (!IsClassLike(parent.Parent) || parent.AsHeritageClause().Token == KindImplementsKeyword) || IsJSDocImplementsTag(parent) || IsJSDocAugmentsTag(parent);
}

public static bool IsJSDocLinkLike(ж<Node> node) {
    return NodeKindIs(node, KindJSDocLink, KindJSDocLinkCode, KindJSDocLinkPlain);
}

public static bool IsJSDocTag(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind >= KindFirstJSDocTagNode && node.Kind <= KindLastJSDocTagNode;
}

public static bool IsSuperCall(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsCallExpression(Ꮡnode) && node.Expression().Kind == KindSuperKeyword;
}

public static bool IsImportCall(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsCallExpression(Ꮡnode)) {
        return false;
    }
    var e = node.Expression();
    return e.Kind == KindImportKeyword || IsMetaProperty(e) && e.AsMetaProperty().KeywordToken == KindImportKeyword && e.Text() == "defer"u8;
}

public static bool IsComputedNonLiteralName(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    return IsComputedPropertyName(Ꮡname) && !IsStringOrNumericLiteralLike(name.Expression());
}

public static bool IsQuestionToken(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && node.Kind == KindQuestionToken;
}

public static @string EntityNameToString(ж<Node> Ꮡname, Func<ж<Node>, @string> getTextOfNode) {
    ref var name = ref Ꮡname.DerefOrNull();

    switch (name.Kind) {
    case var ᴛ when ᴛ == KindThisKeyword: {
        return "this";
    }
    case var ᴛ when ᴛ == KindIdentifier || ᴛ == KindPrivateIdentifier: {
        if (NodeIsSynthesized(Ꮡname) || getTextOfNode == nil) {
            return name.Text();
        }
        return getTextOfNode(Ꮡname);
    }
    case var ᴛ when ᴛ == KindQualifiedName: {
        return EntityNameToString(name.AsQualifiedName().Left, getTextOfNode) + "."u8 + EntityNameToString(name.AsQualifiedName().Right, getTextOfNode);
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        return EntityNameToString(name.Expression(), getTextOfNode) + "."u8 + EntityNameToString(name.AsPropertyAccessExpression().Name(), getTextOfNode);
    }
    case var ᴛ when ᴛ == KindJsxNamespacedName: {
        return EntityNameToString(name.AsJsxNamespacedName().Namespace, getTextOfNode) + ":"u8 + EntityNameToString(name.AsJsxNamespacedName().Name(), getTextOfNode);
    }}

    throw panic("Unhandled case in EntityNameToString");
}

public static @string GetTextOfPropertyName(ж<Node> name) {
    var (text, _) = TryGetTextOfPropertyName(name);
    return text;
}

public static (@string, bool) TryGetTextOfPropertyName(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    switch (name.Kind) {
    case var ᴛ when ᴛ == KindIdentifier || ᴛ == KindPrivateIdentifier || ᴛ == KindStringLiteral || ᴛ == KindNumericLiteral || ᴛ == KindBigIntLiteral || ᴛ == KindNoSubstitutionTemplateLiteral: {
        return (name.Text(), true);
    }
    case var ᴛ when ᴛ == KindComputedPropertyName: {
        if (IsStringOrNumericLiteralLike(name.Expression())) {
            return (name.Expression().Text(), true);
        }
        break;
    }
    case var ᴛ when ᴛ == KindJsxNamespacedName: {
        return (name.AsJsxNamespacedName().Namespace.Text() + ":" + name.Name().Text(), true);
    }}

    return ("", false);
}

public static bool IsJSDocNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind >= KindFirstJSDocNode && node.Kind <= KindLastJSDocNode;
}

public static bool IsNonWhitespaceToken(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsTokenKind(node.Kind) && !IsWhitespaceOnlyJsxText(Ꮡnode);
}

public static bool IsWhitespaceOnlyJsxText(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindJsxText && node.AsJsxText().ContainsOnlyTriviaWhiteSpaces;
}

public static ж<Node> GetNewTargetContainer(ж<Node> node) {
    var container = GetThisContainer(node, false, false);
    if (container != nil) {
        switch (container.Kind) {
        case var ᴛ when ᴛ == KindConstructor || ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression: {
            return container;
        }}

    }
    return default;
}

public static ж<Node> GetEnclosingBlockScopeContainer(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return FindAncestor(node.Parent, (ж<Node> current) => IsBlockScope(current, current.Parent));
}

public static bool IsBlockScope(ж<Node> Ꮡnode, ж<Node> parentNode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindSourceFile || ᴛ == KindCaseBlock || ᴛ == KindCatchClause || ᴛ == KindModuleDeclaration || ᴛ == KindForStatement || ᴛ == KindForInStatement || ᴛ == KindForOfStatement || ᴛ == KindConstructor || ᴛ == KindMethodDeclaration || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindArrowFunction || ᴛ == KindPropertyDeclaration || ᴛ == KindClassStaticBlockDeclaration: {
        return true;
    }
    case var ᴛ when ᴛ == KindBlock: {
        return !IsFunctionLikeOrClassStaticBlockDeclaration(parentNode);
    }}

    return false;
}

partial struct SemanticMeaning /*num:int32*/;

public static SemanticMeaning SemanticMeaningNone => 0;
public static SemanticMeaning SemanticMeaningValue => /* 1 << 0 */ 1;
public static SemanticMeaning SemanticMeaningType => /* 1 << 1 */ 2;
public static SemanticMeaning SemanticMeaningNamespace => /* 1 << 2 */ 4;
public static SemanticMeaning SemanticMeaningAll => /* SemanticMeaningValue | SemanticMeaningType | SemanticMeaningNamespace */ 7;

public static SemanticMeaning GetMeaningFromDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration: {
        return SemanticMeaningValue;
    }
    case var ᴛ when ᴛ == KindParameter || ᴛ == KindBindingElement || ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindPropertyAssignment || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindMethodDeclaration || ᴛ == KindMethodSignature || ᴛ == KindConstructor || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindArrowFunction || ᴛ == KindCatchClause || ᴛ == KindJsxAttribute: {
        return SemanticMeaningValue;
    }
    case var ᴛ when ᴛ == KindTypeParameter || ᴛ == KindInterfaceDeclaration || ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration || ᴛ == KindTypeLiteral: {
        return SemanticMeaningType;
    }
    case var ᴛ when ᴛ == KindEnumMember || ᴛ == KindClassDeclaration: {
        return (SemanticMeaning)(SemanticMeaningValue | SemanticMeaningType);
    }
    case var ᴛ when ᴛ == KindModuleDeclaration: {
        if (IsAmbientModule(Ꮡnode)) {
            return (SemanticMeaning)(SemanticMeaningNamespace | SemanticMeaningValue);
        } else if (GetModuleInstanceState(Ꮡnode) == ModuleInstanceStateInstantiated) {
            return (SemanticMeaning)(SemanticMeaningNamespace | SemanticMeaningValue);
        } else {
            return SemanticMeaningNamespace;
        }
        break;
    }
    case var ᴛ when ᴛ == KindEnumDeclaration || ᴛ == KindNamedImports || ᴛ == KindImportSpecifier || ᴛ == KindImportEqualsDeclaration || ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindExportAssignment || ᴛ == KindExportDeclaration: {
        return SemanticMeaningAll;
    }
    case var ᴛ when ᴛ == KindSourceFile: {
        return (SemanticMeaning)(SemanticMeaningNamespace | SemanticMeaningValue);
    }}

    return SemanticMeaningAll;
}

public static bool IsPropertyAccessOrQualifiedName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindPropertyAccessExpression || node.Kind == KindQualifiedName;
}

public static bool IsLabelName(ж<Node> node) {
    return IsLabelOfLabeledStatement(node) || IsJumpStatementTarget(node);
}

public static bool IsLabelOfLabeledStatement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsIdentifier(Ꮡnode)) {
        return false;
    }
    if (!IsLabeledStatement(node.Parent)) {
        return false;
    }
    return Ꮡnode == node.Parent.Label();
}

public static bool IsJumpStatementTarget(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsIdentifier(Ꮡnode)) {
        return false;
    }
    if (!IsBreakOrContinueStatement(node.Parent)) {
        return false;
    }
    return Ꮡnode == node.Parent.Label();
}

public static bool IsBreakOrContinueStatement(ж<Node> node) {
    return NodeKindIs(node, KindBreakStatement, KindContinueStatement);
}

internal static slice<ж<Node>> pushAncestor(slice<ж<Node>> ancestors, ж<Node> parent) {
    return append(ancestors, parent);
}

internal static (slice<ж<Node>>, ж<Node>) popAncestor(slice<ж<Node>> ancestors, ref Node node) {
    if (len(ancestors) == 0) {
        return (default, node.Parent);
    }
    nint n = len(ancestors) - 1;
    return (ancestors.slice(0, n), ancestors[n]);
}

partial struct ModuleInstanceState /*num:int32*/;

public static ModuleInstanceState ModuleInstanceStateUnknown => /* iota */ 0;
public static ModuleInstanceState ModuleInstanceStateNonInstantiated => 1;
public static ModuleInstanceState ModuleInstanceStateInstantiated => 2;
public static ModuleInstanceState ModuleInstanceStateConstEnumOnly => 3;

public static ModuleInstanceState GetModuleInstanceState(ж<Node> node) {
    return getModuleInstanceState(node, default, default);
}

internal static ModuleInstanceState getModuleInstanceState(ж<Node> Ꮡnode, slice<ж<Node>> ancestors, map<NodeId, ModuleInstanceState> visited) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var module = node.AsModuleDeclaration();
    if ((~module).Body != nil) {
        return getModuleInstanceStateCached((~module).Body, pushAncestor(ancestors, Ꮡnode), visited);
    } else {
        return ModuleInstanceStateInstantiated;
    }
}

internal static ModuleInstanceState getModuleInstanceStateCached(ж<Node> node, slice<ж<Node>> ancestors, map<NodeId, ModuleInstanceState> visited) {
    if (visited == nil) {
        visited = new map<NodeId, ModuleInstanceState>();
    }
    var nodeId = GetNodeId(node);
    var (cached, ok) = visited[nodeId, ꟷ]; if (ok) {
        if (cached != ModuleInstanceStateUnknown) {
            return cached;
        }
        return ModuleInstanceStateNonInstantiated;
    }
    visited[nodeId] = ModuleInstanceStateUnknown;
    var result = getModuleInstanceStateWorker(node, ancestors, visited);
    visited[nodeId] = result;
    return result;
}

internal static ModuleInstanceState getModuleInstanceStateWorker(ж<Node> Ꮡnode, slice<ж<Node>> ancestors, map<NodeId, ModuleInstanceState> visited) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindInterfaceDeclaration || ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration: {
        return ModuleInstanceStateNonInstantiated;
    }
    case var ᴛ when ᴛ == KindEnumDeclaration: {
        if (IsEnumConst(Ꮡnode)) {
            return ModuleInstanceStateConstEnumOnly;
        }
        break;
    }
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindImportEqualsDeclaration: {
        if (!HasSyntacticModifier(Ꮡnode, ModifierFlagsExport)) {
            return ModuleInstanceStateNonInstantiated;
        }
        break;
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        var decl = node.AsExportDeclaration();
        if ((~decl).ModuleSpecifier == nil && decl.ExportClause != nil && decl.ExportClause.Kind == KindNamedExports) {
            var state = ModuleInstanceStateNonInstantiated;
            ancestors = pushAncestor(ancestors, Ꮡnode);
            ancestors = pushAncestor(ancestors, decl.ExportClause);
            foreach (var (_, specifier) in range(decl.ExportClause.Elements())) {
                var specifierState = getModuleInstanceStateForAliasTarget(specifier, ancestors, visited);
                if (specifierState > state) {
                    state = specifierState;
                }
                if (state == ModuleInstanceStateInstantiated) {
                    return state;
                }
            }
            return state;
        }
        break;
    }
    case var ᴛ when ᴛ == KindModuleBlock: {
        var state = ModuleInstanceStateNonInstantiated;
        ancestors = pushAncestor(ancestors, Ꮡnode);
        var ancestorsʗ1 = ancestors;
        var visitedʗ1 = visited;
        node.ForEachChild((ж<Node> n) => {
            var childState = getModuleInstanceStateCached(n, ancestorsʗ1, visitedʗ1);
            switch (childState) {
            case var ᴛ2 when ᴛ2 == ModuleInstanceStateNonInstantiated: {
                return false;
            }
            case var ᴛ2 when ᴛ2 == ModuleInstanceStateConstEnumOnly: {
                state = ModuleInstanceStateConstEnumOnly;
                return false;
            }
            case var ᴛ2 when ᴛ2 == ModuleInstanceStateInstantiated: {
                state = ModuleInstanceStateInstantiated;
                return true;
            }}

            throw panic("Unhandled case in getModuleInstanceStateWorker");
        });
        return state;
    }
    case var ᴛ when ᴛ == KindModuleDeclaration: {
        return getModuleInstanceState(Ꮡnode, ancestors, visited);
    }}

    return ModuleInstanceStateInstantiated;
}

internal static ModuleInstanceState getModuleInstanceStateForAliasTarget(ж<Node> Ꮡnode, slice<ж<Node>> ancestors, map<NodeId, ModuleInstanceState> visited) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.PropertyNameOrName();
    if (name.Kind != KindIdentifier) {
        return ModuleInstanceStateInstantiated;
    }
    for (var (ancestorsΔ1, p) = popAncestor(ancestors, ref (Ꮡnode).DerefOrNull()); p != nil; (ancestorsΔ1, p) = popAncestor(ancestorsΔ1, ref (p).DerefOrNull())) {
        if (IsBlock(p) || IsModuleBlock(p) || IsSourceFile(p)) {
            var found = ModuleInstanceStateUnknown;
            var statementsAncestors = pushAncestor(ancestorsΔ1, p);
            foreach (var (_, statement) in range(p.Statements())) {
                if (NodeHasName(statement, name)) {
                    var state = getModuleInstanceStateCached(statement, statementsAncestors, visited);
                    if (found == ModuleInstanceStateUnknown || state > found) {
                        found = state;
                    }
                    if (found == ModuleInstanceStateInstantiated) {
                        return found;
                    }
                    if (statement.Kind == KindImportEqualsDeclaration) {
                        found = ModuleInstanceStateInstantiated;
                    }
                }
            }
            if (found != ModuleInstanceStateUnknown) {
                return found;
            }
        }
    }
    return ModuleInstanceStateInstantiated;
}

public static bool IsInstantiatedModule(ж<Node> node, bool preserveConstEnums) {
    var moduleState = GetModuleInstanceState(node);
    return moduleState == ModuleInstanceStateInstantiated || (preserveConstEnums && moduleState == ModuleInstanceStateConstEnumOnly);
}

public static bool NodeHasName(ж<Node> Ꮡstatement, ж<Node> Ꮡid) {
    ref var statement = ref Ꮡstatement.DerefOrNull();
    ref var id = ref Ꮡid.DerefOrNull();

    var name = statement.Name();
    if (name != nil) {
        return IsIdentifier(name) && name.Text() == id.Text();
    }
    if (IsVariableStatement(Ꮡstatement)) {
        var declarations = statement.AsVariableStatement().DeclarationList.AsVariableDeclarationList().Declarations.Nodes;
        return core.Some(declarations, (ж<Node> d) => NodeHasName(d, Ꮡid));
    }
    return false;
}

public static bool IsInternalModuleImportEqualsDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsImportEqualsDeclaration(Ꮡnode) && node.AsImportEqualsDeclaration().ModuleReference.Kind != KindExternalModuleReference;
}

public static bool IsConstAssertion(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindAsExpression || ᴛ == KindTypeAssertionExpression: {
        return IsConstTypeReference(node.Type());
    }}

    return false;
}

public static bool IsConstTypeReference(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsTypeReferenceNode(Ꮡnode) && len(node.TypeArguments()) == 0 && IsIdentifier(node.AsTypeReferenceNode().TypeName) && node.AsTypeReferenceNode().TypeName.Text() == "const"u8;
}

public static bool IsGlobalSourceFile(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindSourceFile && !IsExternalOrCommonJSModule(node.AsSourceFile());
}

public static bool IsParameterLike(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindParameter || ᴛ == KindTypeParameter: {
        return true;
    }}

    return false;
}

public static ж<Node> GetDeclarationOfKind(ж<Symbol> Ꮡsymbol, Kind kind) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    foreach (var (_, declaration) in range(symbol.Declarations)) {
        if (declaration.Kind == kind) {
            return declaration;
        }
    }
    return default;
}

public static ж<Node> FindConstructorDeclaration(ж<ClassLikeDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    foreach (var (_, member) in range(node.Members())) {
        if (IsConstructorDeclaration(member) && NodeIsPresent(member.Body())) {
            return member;
        }
    }
    return default;
}

public static ж<Node> GetFirstIdentifier(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindIdentifier: {
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == KindQualifiedName: {
        return GetFirstIdentifier(node.AsQualifiedName().Left);
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        return GetFirstIdentifier((~node.AsPropertyAccessExpression()).Expression);
    }}

    throw panic("Unhandled case in GetFirstIdentifier");
}

public static ж<Node> GetNamespaceDeclarationNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        var importClause = node.ImportClause();
        if (importClause != nil && importClause.AsImportClause().NamedBindings != nil && IsNamespaceImport(importClause.AsImportClause().NamedBindings)) {
            return importClause.AsImportClause().NamedBindings;
        }
        break;
    }
    case var ᴛ when ᴛ == KindImportEqualsDeclaration: {
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        var exportClause = node.AsExportDeclaration().ExportClause;
        if (exportClause != nil && IsNamespaceExport(exportClause)) {
            return exportClause;
        }
        break;
    }
    default: {
        throw panic("Unhandled case in getNamespaceDeclarationNode");
        break;
    }}

    return default;
}

public static bool ModuleExportNameIsDefault(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Text() == InternalSymbolNameDefault;
}

public static bool IsDefaultImport(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        var importClause = node.ImportClause();
        return importClause != nil && importClause.AsImportClause().name != nil;
    }}

    return false;
}

public static core.ModuleKind GetImpliedNodeFormatForFile(@string path, @string packageJsonType) {
    var impliedNodeFormat = core.ResolutionModeNone;
    if (tspath.FileExtensionIsOneOf(path, [tspath.ExtensionDmts, tspath.ExtensionMts, tspath.ExtensionMjs])) {
        impliedNodeFormat = core.ResolutionModeESM;
    } else if (tspath.FileExtensionIsOneOf(path, [tspath.ExtensionDcts, tspath.ExtensionCts, tspath.ExtensionCjs])) {
        impliedNodeFormat = core.ResolutionModeCommonJS;
    } else if (tspath.FileExtensionIsOneOf(path, [tspath.ExtensionDts, tspath.ExtensionTs, tspath.ExtensionTsx, tspath.ExtensionJs, tspath.ExtensionJsx])) {
        impliedNodeFormat = core.IfElse(packageJsonType == "module"u8, core.ResolutionModeESM, core.ResolutionModeCommonJS);
    }

    return impliedNodeFormat;
}

public static core.ModuleKind GetEmitModuleFormatOfFileWorker(@string fileName, ж<core.CompilerOptions> Ꮡoptions, SourceFileMetaData sourceFileMetaData) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    var result = GetImpliedNodeFormatForEmitWorker(fileName, options.GetEmitModuleKind(), sourceFileMetaData);
    if (result != core.ModuleKindNone) {
        return result;
    }
    return options.GetEmitModuleKind();
}

public static coreꓸResolutionMode GetImpliedNodeFormatForEmitWorker(@string fileName, core.ModuleKind emitModuleKind, SourceFileMetaData sourceFileMetaData) {
    if (core.ModuleKindNode16 <= emitModuleKind && emitModuleKind <= core.ModuleKindNodeNext) {
        return sourceFileMetaData.ImpliedNodeFormat;
    }
    if (sourceFileMetaData.ImpliedNodeFormat == core.ModuleKindCommonJS && (sourceFileMetaData.PackageJsonType == "commonjs"u8 || tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionCjs, tspath.ExtensionCts]))) {
        return core.ModuleKindCommonJS;
    }
    if (sourceFileMetaData.ImpliedNodeFormat == core.ModuleKindESNext && (sourceFileMetaData.PackageJsonType == "module"u8 || tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionMjs, tspath.ExtensionMts]))) {
        return core.ModuleKindESNext;
    }
    return core.ModuleKindNone;
}

public static ж<Node> GetDeclarationContainer(ж<Node> node) {
    return FindAncestor(GetRootDeclaration(node), (ж<Node> nodeΔ1) => {
        switch (nodeΔ1.Kind) {
        case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindVariableDeclarationList || ᴛ == KindImportSpecifier || ᴛ == KindNamedImports || ᴛ == KindNamespaceImport || ᴛ == KindImportClause: {
            return false;
        }
        default: {
            return true;
        }}

    }).Parent;
}

public static bool IsNonLocalAlias(ж<Symbol> Ꮡsymbol, SymbolFlags excludes) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡsymbol == nil) {
        return false;
    }
    return (SymbolFlags)(symbol.Flags & ((SymbolFlags)(SymbolFlagsAlias | excludes))) == SymbolFlagsAlias || (SymbolFlags)(symbol.Flags & SymbolFlagsAlias) != 0 && (SymbolFlags)(symbol.Flags & SymbolFlagsAssignment) != 0;
}

public static bool IsAliasSymbolDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportEqualsDeclaration || ᴛ == KindNamespaceExportDeclaration || ᴛ == KindNamespaceImport || ᴛ == KindNamespaceExport || ᴛ == KindImportSpecifier || ᴛ == KindExportSpecifier: {
        return true;
    }
    case var ᴛ when ᴛ == KindImportClause: {
        return node.AsImportClause().Name() != nil;
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        return ExpressionIsAlias(node.Expression());
    }
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindBindingElement: {
        return IsVariableDeclarationInitializedToRequire(Ꮡnode);
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        switch (GetAssignmentDeclarationKind(Ꮡnode)) {
        case var ᴛ2 when ᴛ2 == JSDeclarationKindModuleExports || ᴛ2 == JSDeclarationKindExportsProperty: {
            return ExpressionIsAlias(node.AsBinaryExpression().Right);
        }}

        break;
    }}

    return false;
}

public static bool IsParseTreeNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (NodeFlags)(node.Flags & NodeFlagsSynthesized) == 0;
}

public static ж<Node> GetNodeAtPosition(ж<SourceFile> @file, nint position, bool includeJSDoc) {
    var current = @file.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode();
    for (;;) {
        ref var child = ref heap<ж<Node>>(out var Ꮡchild);
        if (includeJSDoc) {
            foreach (var (_, jsdoc) in range(current.JSDoc(@file))) {
                if (nodeContainsPosition(jsdoc, position)) {
                    child = jsdoc;
                    break;
                }
            }
        }
        if (child == nil) {
            current.ForEachChild((ж<Node> node) => {
                if (nodeContainsPosition(node, position)) {
                    Ꮡchild.ValueSlot = node;
                    return true;
                }
                return false;
            });
        }
        if (child == nil || IsMetaProperty(child)) {
            return current;
        }
        current = child;
    }
}

internal static bool nodeContainsPosition(ж<Node> Ꮡnode, nint position) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind >= KindFirstNode && node.Pos() <= position && (position < node.End() || position == node.End() && node.Kind == KindEndOfFile);
}

internal static (nint index, nint size) findImportOrRequire(@string text, nint start) {
    nint index = default;
    nint size = default;

    index = max(start, 0);
    nint n = len(text);
    while (index < n) {
        nint next = strings.IndexAny(text.slice(index), "ir");
        if (next < 0) {
            break;
        }
        index += next;

        @string expected = default;
        if (text[index] == 'i') {
            size = 6;
            expected = "import";
        } else {
            size = 7;
            expected = "require";
        }
        if (index + size <= n && text.slice(index, index + size) == expected) {
            return (index, size);
        }
        index++;
    }

    return (-1, 0);
}

public static bool ForEachDynamicImportOrRequireCall(ж<SourceFile> Ꮡfile, bool includeTypeSpaceImports, bool requireStringLiteralLikeArgument, Func<ж<Node>, ж<Expression>, bool> cb) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    var isJavaScriptFile = IsInJSFile(Ꮡfile.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode());
    var (lastIndex, size) = findImportOrRequire(@file.Text(), 0);
    while (lastIndex >= 0) {
        var node = GetNodeAtPosition(Ꮡfile, lastIndex, isJavaScriptFile && includeTypeSpaceImports);
        if (isJavaScriptFile && IsRequireCall(node, requireStringLiteralLikeArgument)) {
            if (cb(node, node.Arguments()[0])) {
                return true;
            }
        } else if (IsImportCall(node) && len(node.Arguments()) > 0 && (!requireStringLiteralLikeArgument || IsStringLiteralLike(node.Arguments()[0]))) {
            if (cb(node, node.Arguments()[0])) {
                return true;
            }
        } else if (includeTypeSpaceImports && IsLiteralImportTypeNode(node)) {
            if (cb(node, node.AsImportTypeNode().Argument.AsLiteralTypeNode().Literal)) {
                return true;
            }
        }
        lastIndex += size;
        (lastIndex, size) = findImportOrRequire(@file.Text(), lastIndex);
    }
    return false;
}

public static bool IsRequireCall(ж<Node> Ꮡnode, bool requireStringLiteralLikeArgument) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsCallExpression(Ꮡnode)) {
        return false;
    }
    var call = node.AsCallExpression();
    if (!IsIdentifier((~call).Expression) || (~call).Expression.Text() != "require"u8) {
        return false;
    }
    if (len((~call).Arguments.Nodes) != 1) {
        return false;
    }
    return !requireStringLiteralLikeArgument || IsStringLiteralLike((~call).Arguments.Nodes[0]);
}

public static bool IsRequireVariableStatement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsVariableStatement(Ꮡnode)) {
        var declarations = node.AsVariableStatement().DeclarationList.AsVariableDeclarationList().Declarations.Nodes; if (len(declarations) > 0) {
            return core.Every<ж<Node>>(declarations, IsVariableDeclarationInitializedToRequire);
        }
    }
    return false;
}

public static @string GetJSXImplicitImportBase(ж<core.CompilerOptions> ᏑcompilerOptions, ж<SourceFile> @file) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    var jsxImportSourcePragma = GetPragmaFromSourceFile(@file, "jsximportsource");
    var jsxRuntimePragma = GetPragmaFromSourceFile(@file, "jsxruntime");
    if (GetPragmaArgument(jsxRuntimePragma, "factory") == "classic"u8) {
        return "";
    }
    if (compilerOptions.Jsx == core.JsxEmitReactJSX || compilerOptions.Jsx == core.JsxEmitReactJSXDev || compilerOptions.JsxImportSource != ""u8 || jsxImportSourcePragma != nil || GetPragmaArgument(jsxRuntimePragma, "factory") == "automatic"u8) {
        @string result = GetPragmaArgument(jsxImportSourcePragma, "factory");
        if (result == ""u8) {
            result = compilerOptions.JsxImportSource;
        }
        if (result == ""u8) {
            result = "react";
        }
        return result;
    }
    return "";
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string jsxDevRuntimeˢ = "jsx-dev-runtime"u8;
internal static readonly @string jsxRuntimeˢ = "jsx-runtime"u8;

public static @string GetJSXRuntimeImport(@string @base, ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (@base == ""u8) {
        return @base;
    }
    return @base + "/"u8 + core.IfElse(options.Jsx == core.JsxEmitReactJSXDev, jsxDevRuntimeˢ, jsxRuntimeˢ);
}

public static ж<Pragma> GetPragmaFromSourceFile(ж<SourceFile> Ꮡfile, @string name) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    ж<Pragma> result = default;
    if (Ꮡfile != nil) {
        foreach (var i in range(len(@file.Pragmas))) {
            if (@file.Pragmas[i].Name == name) {
                result = Ꮡ(@file.Pragmas, i);
            }
        }
    }
    return result;
}

public static @string GetPragmaArgument(ж<Pragma> Ꮡpragma, @string name) {
    ref var pragma = ref Ꮡpragma.DerefOrNull();

    if (Ꮡpragma != nil) {
        var (arg, ok) = pragma.Args[name, ꟷ]; if (ok) {
            return arg.Value;
        }
    }
    return "";
}

public static bool IsVariableDeclarationInitializedToRequire(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == KindBindingElement) {
        Ꮡnode = node.Parent.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return isVariableDeclarationInitializedWithRequireHelper(Ꮡnode, false);
}

public static bool IsVariableDeclarationInitializedToBareOrAccessedRequire(ж<Node> node) {
    return isVariableDeclarationInitializedWithRequireHelper(node, true);
}

internal static bool isVariableDeclarationInitializedWithRequireHelper(ж<Node> Ꮡnode, bool allowAccessedRequire) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsInJSFile(Ꮡnode)) {
        return false;
    }
    if (node.Kind != KindVariableDeclaration) {
        return false;
    }
    var initializer = node.Initializer();
    if (initializer == nil) {
        return false;
    }
    if (allowAccessedRequire) {
        initializer = GetLeftmostAccessExpression(initializer);
    }

    return (ModifierFlags)(node.Parent.Parent.ModifierFlags() & ModifierFlagsExport) == 0 && node.Type() == nil && IsRequireCall(initializer, true);
}

public static ж<Node> GetModuleSpecifierOfBareOrAccessedRequire(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isVariableDeclarationInitializedWithRequireHelper(Ꮡnode, false)) {
        return node.Initializer().Arguments()[0];
    }
    if (isVariableDeclarationInitializedWithRequireHelper(Ꮡnode, true)) {
        var leftmost = GetLeftmostAccessExpression(node.Initializer());
        if (IsRequireCall(leftmost, true)) {
            return leftmost.Arguments()[0];
        }
    }
    return default;
}

public static bool IsModuleExportsAccessExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsAccessExpression(Ꮡnode) && IsModuleIdentifier(node.Expression())) {
        var name = GetElementOrPropertyAccessName(Ꮡnode); if (name != nil) {
            return name.Text() == "exports"u8;
        }
    }
    return false;
}

public static bool IsModuleExportsQualifiedName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsQualifiedName(Ꮡnode) && IsModuleIdentifier(node.AsQualifiedName().Left) && node.AsQualifiedName().Right.Text() == "exports"u8;
}

public static bool IsCheckJSEnabledForFile(ж<SourceFile> ᏑsourceFile, ж<core.CompilerOptions> ᏑcompilerOptions) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    if (sourceFile.CheckJsDirective != nil) {
        return sourceFile.CheckJsDirective.Enabled;
    }
    return compilerOptions.CheckJs == core.TSTrue;
}

public static bool IsPlainJSFile(ж<SourceFile> Ꮡfile, core.Tristate checkJs) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    return Ꮡfile != nil && (@file.ScriptKind == core.ScriptKindJS || @file.ScriptKind == core.ScriptKindJSX) && @file.CheckJsDirective == nil && checkJs == core.TSUnknown;
}

public static ж<Node> GetLeftmostAccessExpression(ж<Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    while (IsAccessExpression(Ꮡexpr)) {
        Ꮡexpr = expr.Expression(); expr = ref Ꮡexpr.DerefOrNull();
    }
    return Ꮡexpr;
}

public static bool IsTypeOnlyImportDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportSpecifier: {
        return node.IsTypeOnly() || node.Parent.Parent.IsTypeOnly();
    }
    case var ᴛ when ᴛ == KindNamespaceImport: {
        return node.Parent.IsTypeOnly();
    }
    case var ᴛ when ᴛ == KindImportClause || ᴛ == KindImportEqualsDeclaration: {
        return node.IsTypeOnly();
    }}

    return false;
}

internal static bool isTypeOnlyExportDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindExportSpecifier: {
        return node.IsTypeOnly() || node.Parent.Parent.IsTypeOnly();
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        var d = node.AsExportDeclaration();
        return (~d).IsTypeOnly && (~d).ModuleSpecifier != nil && d.ExportClause == nil;
    }
    case var ᴛ when ᴛ == KindNamespaceExport: {
        return node.Parent.IsTypeOnly();
    }}

    return false;
}

public static bool IsTypeOnlyImportOrExportDeclaration(ж<Node> node) {
    return IsTypeOnlyImportDeclaration(node) || isTypeOnlyExportDeclaration(node);
}

public static bool IsExclusivelyTypeOnlyImportOrExport(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindExportDeclaration: {
        return node.IsTypeOnly();
    }
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        {
            var importClause = node.ImportClause(); if (importClause != nil) {
                return importClause.AsImportClause().of(ImportClause.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).of(NodeDefault.ᏑNode).IsTypeOnly();
            }
        }
        break;
    }
    case var ᴛ when ᴛ == KindJSDocImportTag: {
        {
            var importClause = node.ImportClause(); if (importClause != nil) {
                return importClause.AsImportClause().of(ImportClause.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).of(NodeDefault.ᏑNode).IsTypeOnly();
            }
        }
        break;
    }}

    return false;
}

public static ж<Node> GetClassLikeDeclarationOfSymbol(ж<Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return core.Find<ж<Node>>(symbol.Declarations, IsClassLike);
}

public static bool IsCallLikeExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindJsxOpeningElement || ᴛ == KindJsxSelfClosingElement || ᴛ == KindJsxOpeningFragment || ᴛ == KindCallExpression || ᴛ == KindNewExpression || ᴛ == KindTaggedTemplateExpression || ᴛ == KindDecorator: {
        return true;
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        return node.AsBinaryExpression().OperatorToken.Kind == KindInstanceOfKeyword;
    }}

    return false;
}

public static bool IsJsxCallLike(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindJsxOpeningElement || ᴛ == KindJsxSelfClosingElement || ᴛ == KindJsxOpeningFragment: {
        return true;
    }}

    return false;
}

public static bool IsCallLikeOrFunctionLikeExpression(ж<Node> node) {
    return IsCallLikeExpression(node) || IsFunctionExpressionOrArrowFunction(node);
}

public static bool NodeHasKind(ж<Node> Ꮡnode, Kind kind) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return false;
    }
    return node.Kind == kind;
}

public static bool IsContextualKeyword(Kind token) {
    return KindFirstContextualKeyword <= token && token <= KindLastContextualKeyword;
}

public static bool IsThisInTypeQuery(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsThisIdentifier(Ꮡnode)) {
        return false;
    }
    while (IsQualifiedName(node.Parent) && node.Parent.AsQualifiedName().Left == Ꮡnode) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return node.Parent.Kind == KindTypeQuery;
}

public static bool IsLet(ж<Node> node) {
    return (NodeFlags)(GetCombinedNodeFlags(node) & NodeFlagsBlockScoped) == NodeFlagsLet;
}

public static bool IsClassMemberModifier(Kind token) {
    return IsParameterPropertyModifier(token) || token == KindStaticKeyword || token == KindOverrideKeyword || token == KindAccessorKeyword;
}

public static bool IsParameterPropertyModifier(Kind kind) {
    return (ModifierFlags)(ModifierToFlag(kind) & ModifierFlagsParameterPropertyModifier) != 0;
}

public static bool ForEachChildAndJSDoc(ж<Node> Ꮡnode, ж<SourceFile> sourceFile, Func<ж<Node>, bool> v) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (visitNodes(v, Ꮡnode.JSDoc(sourceFile))) {
        return true;
    }
    return node.ForEachChild(v);
}

public static bool HasTypeArguments(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindCallExpression || ᴛ == KindNewExpression || ᴛ == KindTaggedTemplateExpression || ᴛ == KindTypeReference || ᴛ == KindExpressionWithTypeArguments || ᴛ == KindImportType || ᴛ == KindTypeQuery || ᴛ == KindJsxOpeningElement || ᴛ == KindJsxSelfClosingElement: {
        return true;
    }}

    return false;
}

public static bool IsTypeReferenceType(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindTypeReference || node.Kind == KindExpressionWithTypeArguments;
}

public static bool IsVariableLike(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindBindingElement || ᴛ == KindEnumMember || ᴛ == KindParameter || ᴛ == KindPropertyAssignment || ᴛ == KindPropertyDeclaration || ᴛ == KindPropertySignature || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindVariableDeclaration: {
        return true;
    }}

    return false;
}

public static bool HasInitializer(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindParameter || ᴛ == KindBindingElement || ᴛ == KindPropertyDeclaration || ᴛ == KindPropertyAssignment || ᴛ == KindEnumMember || ᴛ == KindForStatement || ᴛ == KindForInStatement || ᴛ == KindForOfStatement || ᴛ == KindJsxAttribute: {
        return node.Initializer() != nil;
    }
    default: {
        return false;
    }}

}

public static bool IsVariableParameterOrProperty(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindParameter || ᴛ == KindPropertySignature || ᴛ == KindPropertyDeclaration: {
        return true;
    }
    default: {
        return false;
    }}

}

public static ж<TypeNode> GetTypeAnnotationNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindParameter || ᴛ == KindPropertySignature || ᴛ == KindPropertyDeclaration || ᴛ == KindTypePredicate || ᴛ == KindParenthesizedType || ᴛ == KindTypeOperator || ᴛ == KindMappedType || ᴛ == KindTypeAssertionExpression || ᴛ == KindAsExpression || ᴛ == KindSatisfiesExpression || ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration || ᴛ == KindNamedTupleMember || ᴛ == KindOptionalType || ᴛ == KindRestType || ᴛ == KindTemplateLiteralTypeSpan || ᴛ == KindJSDocTypeExpression || ᴛ == KindJSDocPropertyTag || ᴛ == KindJSDocNullableType || ᴛ == KindJSDocNonNullableType || ᴛ == KindJSDocOptionalType: {
        return node.Type();
    }
    default: {
        var funcLike = node.FunctionLikeData();
        if (funcLike != nil) {
            return funcLike.Type;
        }
        return default;
    }}

}

public static bool IsObjectTypeDeclaration(ж<Node> node) {
    return IsClassLike(node) || IsInterfaceDeclaration(node) || IsTypeLiteralNode(node);
}

public static bool IsClassOrTypeElement(ж<Node> node) {
    return IsClassElement(node) || IsTypeElement(node);
}

public static ж<ExpressionWithTypeArgumentsNode> GetClassExtendsHeritageElement(ж<Node> node) {
    var heritageElements = GetHeritageElements(node, KindExtendsKeyword);
    if (len(heritageElements) > 0) {
        return heritageElements[0];
    }
    return default;
}

public static bool IsTypeKeywordToken(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindTypeKeyword;
}

public static bool IsJSDocSingleCommentNodeList(ж<NodeList> ᏑnodeList) {
    ref var nodeList = ref ᏑnodeList.DerefOrNull();

    if (ᏑnodeList == nil || len(nodeList.Nodes) == 0) {
        return false;
    }
    var parent = nodeList.Nodes[0].Parent;
    if (parent == nil) {
        return false;
    }
    return IsJSDocSingleCommentNode(parent) && ᏑnodeList == parent.CommentList();
}

public static bool IsJSDocSingleCommentNodeComment(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil || node.Parent == nil) {
        return false;
    }
    return IsJSDocSingleCommentNode(node.Parent) && Ꮡnode == node.Parent.CommentList().Nodes[0];
}

public static bool IsJSDocSingleCommentNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return hasComment(node.Kind) && node.CommentList() != nil && len(node.CommentList().Nodes) == 1;
}

public static bool IsValidTypeOnlyAliasUseSite(ж<Node> ᏑuseSite) {
    ref var useSite = ref ᏑuseSite.DerefOrNull();

    return (NodeFlags)(useSite.Flags & ((NodeFlags)(NodeFlagsAmbient | NodeFlagsJSDoc))) != 0 || IsPartOfTypeQuery(ᏑuseSite) || isIdentifierInNonEmittingHeritageClause(ᏑuseSite) || isPartOfPossiblyValidTypeOrAbstractComputedPropertyName(ᏑuseSite) || !(IsExpressionNode(ᏑuseSite) || isShorthandPropertyNameUseSite(ᏑuseSite));
}

internal static bool isIdentifierInNonEmittingHeritageClause(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsIdentifier(Ꮡnode)) {
        return false;
    }
    var parent = node.Parent;
    while (IsPropertyAccessExpression(parent) || IsExpressionWithTypeArguments(parent)) {
        parent = parent.Parent;
    }
    return IsHeritageClause(parent) && (parent.AsHeritageClause().Token == KindImplementsKeyword || IsInterfaceDeclaration(parent.Parent));
}

internal static bool isPartOfPossiblyValidTypeOrAbstractComputedPropertyName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (NodeKindIs(Ꮡnode, KindIdentifier, KindPropertyAccessExpression)) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    if (node.Kind != KindComputedPropertyName) {
        return false;
    }
    if (HasSyntacticModifier(node.Parent, ModifierFlagsAbstract)) {
        return true;
    }
    return NodeKindIs(node.Parent.Parent, KindInterfaceDeclaration, KindTypeLiteral);
}

internal static bool isShorthandPropertyNameUseSite(ж<Node> ᏑuseSite) {
    ref var useSite = ref ᏑuseSite.DerefOrNull();

    return IsIdentifier(ᏑuseSite) && IsShorthandPropertyAssignment(useSite.Parent) && useSite.Parent.AsShorthandPropertyAssignment().Name() == ᏑuseSite;
}

public static @string GetPropertyNameForPropertyNameNode(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    switch (name.Kind) {
    case var ᴛ when ᴛ == KindIdentifier || ᴛ == KindPrivateIdentifier || ᴛ == KindStringLiteral || ᴛ == KindNoSubstitutionTemplateLiteral || ᴛ == KindNumericLiteral || ᴛ == KindBigIntLiteral || ᴛ == KindJsxNamespacedName: {
        return name.Text();
    }
    case var ᴛ when ᴛ == KindComputedPropertyName: {
        var nameExpression = name.Expression();
        if (IsStringOrNumericLiteralLike(nameExpression)) {
            return nameExpression.Text();
        }
        if (IsSignedNumericLiteral(nameExpression)) {
            @string text = nameExpression.AsPrefixUnaryExpression().Operand.Text();
            if (nameExpression.AsPrefixUnaryExpression().Operator == KindMinusToken) {
                text = "-"u8 + text;
            }
            return text;
        }
        return InternalSymbolNameMissing;
    }}

    throw panic("Unhandled case in getPropertyNameForPropertyNameNode");
}

public static bool IsPartOfTypeOnlyImportOrExportDeclaration(ж<Node> node) {
    return FindAncestor(node, IsTypeOnlyImportOrExportDeclaration) != nil;
}

public static bool IsEmittableImport(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration: {
        return node.ImportClause() != nil && !node.ImportClause().IsTypeOnly();
    }
    case var ᴛ when ᴛ == KindExportDeclaration || ᴛ == KindImportEqualsDeclaration: {
        return !node.IsTypeOnly();
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        return IsImportCall(Ꮡnode);
    }}

    return false;
}

public static bool IsResolutionModeOverrideHost(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return false;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportType || ᴛ == KindExportDeclaration || ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        return true;
    }}

    return false;
}

public static bool HasResolutionModeOverride(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return false;
    }
    ж<ImportAttributesNode> attributes = default;
    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportType: {
        attributes = node.AsImportTypeNode().Value.Attributes;
        break;
    }
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        attributes = node.AsImportDeclaration().Value.Attributes;
        break;
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        attributes = node.AsExportDeclaration().Value.Attributes;
        break;
    }}

    if (attributes != nil) {
        var (_, ok) = attributes.GetResolutionModeOverride(default);
        return ok;
    }
    return false;
}

public static bool IsStringTextContainingNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindStringLiteral || IsTemplateLiteralKind(node.Kind);
}

public static bool IsTemplateLiteralKind(Kind kind) {
    return KindFirstTemplateToken <= kind && kind <= KindLastTemplateToken;
}

public static bool IsTemplateLiteralToken(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsTemplateLiteralKind(node.Kind);
}

public static ж<Node> GetExternalModuleImportEqualsDeclarationExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    debug.Assert(IsExternalModuleImportEqualsDeclaration(Ꮡnode));
    return node.AsImportEqualsDeclaration().ModuleReference.Expression();
}

public static slice<ж<Node>> CreateModifiersFromModifierFlags(ModifierFlags flags, Func<Kind, ж<Node>> createModifier) {
    slice<ж<Node>> result = default;
    if ((ModifierFlags)(flags & ModifierFlagsExport) != 0) {
        result = append(result, createModifier(KindExportKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsAmbient) != 0) {
        result = append(result, createModifier(KindDeclareKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsDefault) != 0) {
        result = append(result, createModifier(KindDefaultKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsConst) != 0) {
        result = append(result, createModifier(KindConstKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsPublic) != 0) {
        result = append(result, createModifier(KindPublicKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsPrivate) != 0) {
        result = append(result, createModifier(KindPrivateKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsProtected) != 0) {
        result = append(result, createModifier(KindProtectedKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsAbstract) != 0) {
        result = append(result, createModifier(KindAbstractKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsStatic) != 0) {
        result = append(result, createModifier(KindStaticKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsOverride) != 0) {
        result = append(result, createModifier(KindOverrideKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsReadonly) != 0) {
        result = append(result, createModifier(KindReadonlyKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsAccessor) != 0) {
        result = append(result, createModifier(KindAccessorKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsAsync) != 0) {
        result = append(result, createModifier(KindAsyncKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsIn) != 0) {
        result = append(result, createModifier(KindInKeyword));
    }
    if ((ModifierFlags)(flags & ModifierFlagsOut) != 0) {
        result = append(result, createModifier(KindOutKeyword));
    }
    return result;
}

public static ж<Node> GetThisParameter(ж<Node> Ꮡsignature) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (len(signature.Parameters()) != 0) {
        var thisParameter = signature.Parameters()[0];
        if (IsThisParameter(thisParameter)) {
            return thisParameter;
        }
    }
    return default;
}

public static ж<Node> ReplaceModifiers(ж<NodeFactory> Ꮡfactory, ж<Node> Ꮡnode, ж<ModifierList> modifierArray) {
    ref var factory = ref Ꮡfactory.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindTypeParameter: {
        return factory.UpdateTypeParameterDeclaration(
            node.AsTypeParameterDeclaration(),
            modifierArray,
            node.Name(),
            node.AsTypeParameterDeclaration().Constraint,
            (~node.AsTypeParameterDeclaration()).Expression,
            node.AsTypeParameterDeclaration().DefaultType);
    }
    case var ᴛ when ᴛ == KindParameter: {
        return factory.UpdateParameterDeclaration(
            node.AsParameterDeclaration(),
            modifierArray,
            node.AsParameterDeclaration().DotDotDotToken,
            node.Name(),
            node.QuestionToken(),
            node.Type(),
            node.Initializer());
    }
    case var ᴛ when ᴛ == KindConstructorType: {
        return factory.UpdateConstructorTypeNode(
            node.AsConstructorTypeNode(),
            modifierArray,
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type());
    }
    case var ᴛ when ᴛ == KindPropertySignature: {
        return factory.UpdatePropertySignatureDeclaration(
            node.AsPropertySignatureDeclaration(),
            modifierArray,
            node.Name(),
            node.PostfixToken(),
            node.Type(),
            node.Initializer());
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        return factory.UpdatePropertyDeclaration(
            node.AsPropertyDeclaration(),
            modifierArray,
            node.Name(),
            node.PostfixToken(),
            node.Type(),
            node.Initializer());
    }
    case var ᴛ when ᴛ == KindMethodSignature: {
        return factory.UpdateMethodSignatureDeclaration(
            node.AsMethodSignatureDeclaration(),
            modifierArray,
            node.Name(),
            node.PostfixToken(),
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type());
    }
    case var ᴛ when ᴛ == KindMethodDeclaration: {
        return factory.UpdateMethodDeclaration(
            node.AsMethodDeclaration(),
            modifierArray,
            node.AsMethodDeclaration().AsteriskToken,
            node.Name(),
            node.PostfixToken(),
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type(),
            node.AsMethodDeclaration().FullSignature,
            node.Body());
    }
    case var ᴛ when ᴛ == KindConstructor: {
        return factory.UpdateConstructorDeclaration(
            node.AsConstructorDeclaration(),
            modifierArray,
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type(),
            node.AsConstructorDeclaration().FullSignature,
            node.Body());
    }
    case var ᴛ when ᴛ == KindGetAccessor: {
        return factory.UpdateGetAccessorDeclaration(
            node.AsGetAccessorDeclaration(),
            modifierArray,
            node.Name(),
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type(),
            node.AsGetAccessorDeclaration().FullSignature,
            node.Body());
    }
    case var ᴛ when ᴛ == KindSetAccessor: {
        return factory.UpdateSetAccessorDeclaration(
            node.AsSetAccessorDeclaration(),
            modifierArray,
            node.Name(),
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type(),
            node.AsSetAccessorDeclaration().FullSignature,
            node.Body());
    }
    case var ᴛ when ᴛ == KindIndexSignature: {
        return factory.UpdateIndexSignatureDeclaration(
            node.AsIndexSignatureDeclaration(),
            modifierArray,
            node.ParameterList(),
            node.Type());
    }
    case var ᴛ when ᴛ == KindFunctionExpression: {
        return factory.UpdateFunctionExpression(
            node.AsFunctionExpression(),
            modifierArray,
            node.AsFunctionExpression().AsteriskToken,
            node.Name(),
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type(),
            node.AsFunctionExpression().FullSignature,
            node.Body());
    }
    case var ᴛ when ᴛ == KindArrowFunction: {
        return factory.UpdateArrowFunction(
            node.AsArrowFunction(),
            modifierArray,
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type(),
            node.AsArrowFunction().FullSignature,
            node.AsArrowFunction().EqualsGreaterThanToken,
            node.Body());
    }
    case var ᴛ when ᴛ == KindClassExpression: {
        return factory.UpdateClassExpression(
            node.AsClassExpression(),
            modifierArray,
            node.Name(),
            node.TypeParameterList(),
            node.AsClassExpression().HeritageClauses,
            node.MemberList());
    }
    case var ᴛ when ᴛ == KindVariableStatement: {
        return factory.UpdateVariableStatement(
            node.AsVariableStatement(),
            modifierArray,
            node.AsVariableStatement().DeclarationList);
    }
    case var ᴛ when ᴛ == KindFunctionDeclaration: {
        return factory.UpdateFunctionDeclaration(
            node.AsFunctionDeclaration(),
            modifierArray,
            node.AsFunctionDeclaration().AsteriskToken,
            node.Name(),
            node.TypeParameterList(),
            node.ParameterList(),
            node.Type(),
            node.AsFunctionDeclaration().FullSignature,
            node.Body());
    }
    case var ᴛ when ᴛ == KindClassDeclaration: {
        return factory.UpdateClassDeclaration(
            node.AsClassDeclaration(),
            modifierArray,
            node.Name(),
            node.TypeParameterList(),
            node.AsClassDeclaration().HeritageClauses,
            node.MemberList());
    }
    case var ᴛ when ᴛ == KindInterfaceDeclaration: {
        return factory.UpdateInterfaceDeclaration(
            node.AsInterfaceDeclaration(),
            modifierArray,
            node.Name(),
            node.TypeParameterList(),
            node.AsInterfaceDeclaration().HeritageClauses,
            node.MemberList());
    }
    case var ᴛ when ᴛ == KindTypeAliasDeclaration: {
        return factory.UpdateTypeAliasDeclaration(
            node.AsTypeAliasDeclaration(),
            modifierArray,
            node.Name(),
            node.TypeParameterList(),
            node.Type());
    }
    case var ᴛ when ᴛ == KindEnumDeclaration: {
        return factory.UpdateEnumDeclaration(
            node.AsEnumDeclaration(),
            modifierArray,
            node.Name(),
            node.MemberList());
    }
    case var ᴛ when ᴛ == KindModuleDeclaration: {
        return factory.UpdateModuleDeclaration(
            node.AsModuleDeclaration(),
            modifierArray,
            node.AsModuleDeclaration().Keyword,
            node.Name(),
            node.Attributes(),
            node.Body());
    }
    case var ᴛ when ᴛ == KindImportEqualsDeclaration: {
        return factory.UpdateImportEqualsDeclaration(
            node.AsImportEqualsDeclaration(),
            modifierArray,
            node.IsTypeOnly(),
            node.Name(),
            node.AsImportEqualsDeclaration().ModuleReference);
    }
    case var ᴛ when ᴛ == KindImportDeclaration: {
        return factory.UpdateImportDeclaration(
            node.AsImportDeclaration(),
            modifierArray,
            node.ImportClause(),
            node.ModuleSpecifier(),
            (~node.AsImportDeclaration()).Attributes);
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        return factory.UpdateExportAssignment(
            node.AsExportAssignment(),
            modifierArray,
            node.AsExportAssignment().IsExportEquals,
            node.Type(),
            node.Expression());
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        return factory.UpdateExportDeclaration(
            node.AsExportDeclaration(),
            modifierArray,
            node.IsTypeOnly(),
            node.AsExportDeclaration().ExportClause,
            node.ModuleSpecifier(),
            (~node.AsExportDeclaration()).Attributes);
    }}

    throw panic(fmt.Sprintf("Node that does not have modifiers tried to have modifier replaced: %d"u8, node.Kind));
}

public static bool IsLateVisibilityPaintedStatement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindImportEqualsDeclaration || ᴛ == KindVariableStatement || ᴛ == KindClassDeclaration || ᴛ == KindFunctionDeclaration || ᴛ == KindModuleDeclaration || ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration || ᴛ == KindInterfaceDeclaration || ᴛ == KindEnumDeclaration: {
        return true;
    }
    default: {
        return false;
    }}

}

public static bool IsExternalModuleAugmentation(ж<Node> node) {
    return IsAmbientModule(node) && IsModuleAugmentationExternal(node);
}

public static ж<SourceFile> GetSourceFileOfModule(ж<Symbol> Ꮡmodule) {
    ref var module = ref Ꮡmodule.DerefOrNull();

    var declaration = module.ValueDeclaration;
    if (declaration == nil) {
        declaration = GetNonAugmentationDeclaration(Ꮡmodule);
    }
    return GetSourceFileOfNode(declaration);
}

public static ж<Node> GetNonAugmentationDeclaration(ж<Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return core.Find(symbol.Declarations, (ж<Node> d) => !IsExternalModuleAugmentation(d) && !IsGlobalScopeAugmentation(d));
}

public static bool IsTypeDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindTypeParameter || ᴛ == KindClassDeclaration || ᴛ == KindInterfaceDeclaration || ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration || ᴛ == KindEnumDeclaration: {
        return true;
    }
    case var ᴛ when ᴛ == KindImportClause: {
        return node.IsTypeOnly();
    }
    case var ᴛ when ᴛ == KindImportSpecifier || ᴛ == KindExportSpecifier: {
        return node.Parent.Parent.IsTypeOnly();
    }
    default: {
        return false;
    }}

}

public static bool IsTypeDeclarationName(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    return name.Kind == KindIdentifier && IsTypeDeclaration(name.Parent) && GetNameOfDeclaration(name.Parent) == Ꮡname;
}

public static bool IsRightSideOfPropertyAccess(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Parent.Kind == KindPropertyAccessExpression && node.Parent.Name() == Ꮡnode;
}

public static bool IsArgumentExpressionOfElementAccess(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Parent != nil && node.Parent.Kind == KindElementAccessExpression && node.Parent.AsElementAccessExpression().ArgumentExpression == Ꮡnode;
}

public static ж<Node> ClimbPastPropertyAccess(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsRightSideOfPropertyAccess(Ꮡnode)) {
        return node.Parent;
    }
    return Ꮡnode;
}

internal static ж<Node> climbPastPropertyOrElementAccess(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsRightSideOfPropertyAccess(Ꮡnode) || IsArgumentExpressionOfElementAccess(Ꮡnode)) {
        return node.Parent;
    }
    return Ꮡnode;
}

internal static ж<Node> selectExpressionOfCallOrNewExpressionOrDecorator(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsCallExpression(Ꮡnode) || IsNewExpression(Ꮡnode) || IsDecorator(Ꮡnode)) {
        return node.Expression();
    }
    return default;
}

internal static ж<Node> selectTagOfTaggedTemplateExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsTaggedTemplateExpression(Ꮡnode)) {
        return node.AsTaggedTemplateExpression().Tag;
    }
    return default;
}

internal static ж<Node> selectTagNameOfJsxOpeningLikeElement(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (IsJsxOpeningElement(Ꮡnode) || IsJsxSelfClosingElement(Ꮡnode)) {
        return node.TagName();
    }
    return default;
}

public static bool IsCallExpressionTarget(ж<Node> node, bool includeElementAccess, bool skipPastOuterExpressions) {
    return isCalleeWorker(node, IsCallExpression, selectExpressionOfCallOrNewExpressionOrDecorator, includeElementAccess, skipPastOuterExpressions);
}

public static bool IsNewExpressionTarget(ж<Node> node, bool includeElementAccess, bool skipPastOuterExpressions) {
    return isCalleeWorker(node, IsNewExpression, selectExpressionOfCallOrNewExpressionOrDecorator, includeElementAccess, skipPastOuterExpressions);
}

public static bool IsCallOrNewExpressionTarget(ж<Node> node, bool includeElementAccess, bool skipPastOuterExpressions) {
    return isCalleeWorker(node, IsCallOrNewExpression, selectExpressionOfCallOrNewExpressionOrDecorator, includeElementAccess, skipPastOuterExpressions);
}

public static bool IsTaggedTemplateTag(ж<Node> node, bool includeElementAccess, bool skipPastOuterExpressions) {
    return isCalleeWorker(node, IsTaggedTemplateExpression, selectTagOfTaggedTemplateExpression, includeElementAccess, skipPastOuterExpressions);
}

public static bool IsDecoratorTarget(ж<Node> node, bool includeElementAccess, bool skipPastOuterExpressions) {
    return isCalleeWorker(node, IsDecorator, selectExpressionOfCallOrNewExpressionOrDecorator, includeElementAccess, skipPastOuterExpressions);
}

public static bool IsJsxOpeningLikeElementTagName(ж<Node> node, bool includeElementAccess, bool skipPastOuterExpressions) {
    return isCalleeWorker(node, IsJsxOpeningLikeElement, selectTagNameOfJsxOpeningLikeElement, includeElementAccess, skipPastOuterExpressions);
}

internal static bool isCalleeWorker(ж<Node> node, Func<ж<Node>, bool> pred, Func<ж<Node>, ж<Node>> calleeSelector, bool includeElementAccess, bool skipPastOuterExpressions) {
    ж<Node> target = default;
    if (includeElementAccess) {
        target = climbPastPropertyOrElementAccess(node);
    } else {
        target = ClimbPastPropertyAccess(node);
    }
    if (skipPastOuterExpressions) {
        if (IsExpression(target)) {
            target = SkipOuterExpressions(target, OEKAll);
        }
    }
    return target != nil && target.Parent != nil && pred(target.Parent) && calleeSelector(target.Parent) == target;
}

public static bool IsRightSideOfQualifiedNameOrPropertyAccess(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    switch (parent.Kind) {
    case var ᴛ when ᴛ == KindQualifiedName: {
        return parent.AsQualifiedName().Right == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        return parent.AsPropertyAccessExpression().Name() == Ꮡnode;
    }
    case var ᴛ when ᴛ == KindMetaProperty: {
        return parent.AsMetaProperty().Name() == Ꮡnode;
    }}

    return false;
}

public static bool ShouldTransformImportCall(@string fileName, ж<core.CompilerOptions> Ꮡoptions, core.ModuleKind impliedNodeFormatForEmit) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    var moduleKind = options.GetEmitModuleKind();
    if (core.ModuleKindNode16 <= moduleKind && moduleKind <= core.ModuleKindNodeNext || moduleKind == core.ModuleKindPreserve) {
        return false;
    }
    return impliedNodeFormatForEmit < core.ModuleKindES2015;
}

public static bool HasQuestionToken(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsQuestionToken(node.QuestionToken());
}

public static bool IsJsxOpeningLikeElement(ж<Node> node) {
    return IsJsxOpeningElement(node) || IsJsxSelfClosingElement(node);
}

public static ж<Node> GetInvokedExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindTaggedTemplateExpression: {
        return node.AsTaggedTemplateExpression().Tag;
    }
    case var ᴛ when ᴛ == KindJsxOpeningElement || ᴛ == KindJsxSelfClosingElement: {
        return node.TagName();
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        return node.AsBinaryExpression().Right;
    }
    case var ᴛ when ᴛ == KindJsxOpeningFragment: {
        return Ꮡnode;
    }
    default: {
        return node.Expression();
    }}

}

public static bool IsCallOrNewExpression(ж<Node> node) {
    return IsCallExpression(node) || IsNewExpression(node);
}

public static nint IndexOfNode(slice<ж<Node>> nodes, ж<Node> node) {
    var (index, ok) = slices.BinarySearchFunc<slice<ж<Node>>, ж<Node>, ж<Node>>(nodes, node, CompareNodePositions);
    if (ok) {
        return index;
    }
    return -1;
}

public static nint CompareNodePositions(ж<Node> Ꮡn1, ж<Node> Ꮡn2) {
    ref var n1 = ref Ꮡn1.DerefOrNull();
    ref var n2 = ref Ꮡn2.DerefOrNull();

    return core.CompareTextRanges(n1.Loc, n2.Loc);
}

public static bool IsUnterminatedLiteral(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsLiteralKind(node.Kind) && (TokenFlags)(node.LiteralLikeData().TokenFlags & TokenFlagsUnterminated) != 0 || IsTemplateLiteralKind(node.Kind) && (TokenFlags)(node.TemplateLiteralLikeData().TemplateFlags & TokenFlagsUnterminated) != 0;
}

public static bool IsInitializedProperty(ж<ClassElement> Ꮡmember) {
    ref var member = ref Ꮡmember.DerefOrNull();

    return member.Kind == KindPropertyDeclaration && member.Initializer() != nil;
}

public static bool IsTrivia(Kind token) {
    return KindFirstTriviaToken <= token && token <= KindLastTriviaToken;
}

public static bool HasDecorators(ж<Node> node) {
    return HasSyntacticModifier(node, ModifierFlagsDecorator);
}

partial struct hasFileNameImpl {
    internal @string fileName;
    internal tspath.Path path;
}

public static HasFileName NewHasFileName(@string fileName, tspath.Path path) {
    return new hasFileNameImplжHasFileName(Ꮡ(new hasFileNameImpl(
        fileName: fileName,
        path: path
    )));
}

} partial class astᴛhasFileNameImpl {

internal static @string FileName(this ref hasFileNameImpl h) {
    return h.fileName;
}

internal static tspath.Path Path(this ref hasFileNameImpl h) {
    return h.path;
}

} partial class ast_package {

public static slice<ж<JsxChild>> GetSemanticJsxChildren(slice<ж<JsxChild>> children) {
    return core.Filter(children, (ж<JsxChild> i) => {
        switch (i.Kind) {
        case var ᴛ when ᴛ == KindJsxExpression: {
            return i.Expression() != nil;
        }
        case var ᴛ when ᴛ == KindJsxText: {
            return !i.AsJsxText().ContainsOnlyTriviaWhiteSpaces;
        }
        default: {
            return true;
        }}

    });
}

internal static bool hasComment(Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == KindJSDoc || ᴛ == KindJSDocUnknownTag || ᴛ == KindJSDocAugmentsTag || ᴛ == KindJSDocImplementsTag || ᴛ == KindJSDocDeprecatedTag || ᴛ == KindJSDocPublicTag || ᴛ == KindJSDocPrivateTag || ᴛ == KindJSDocProtectedTag || ᴛ == KindJSDocReadonlyTag || ᴛ == KindJSDocOverrideTag || ᴛ == KindJSDocCallbackTag || ᴛ == KindJSDocOverloadTag || ᴛ == KindJSDocParameterTag || ᴛ == KindJSDocPropertyTag || ᴛ == KindJSDocReturnTag || ᴛ == KindJSDocThisTag || ᴛ == KindJSDocTypeTag || ᴛ == KindJSDocTemplateTag || ᴛ == KindJSDocTypedefTag || ᴛ == KindJSDocSeeTag || ᴛ == KindJSDocThrowsTag || ᴛ == KindJSDocSatisfiesTag || ᴛ == KindJSDocImportTag: {
        return true;
    }
    default: {
        return false;
    }}

}

public static bool IsAssignmentPattern(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindArrayLiteralExpression || node.Kind == KindObjectLiteralExpression;
}

public static slice<ж<Node>> GetElementsOfBindingOrAssignmentPattern(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    switch (name.Kind) {
    case var ᴛ when ᴛ == KindObjectBindingPattern || ᴛ == KindArrayBindingPattern || ᴛ == KindArrayLiteralExpression: {
        return name.Elements();
    }
    case var ᴛ when ᴛ == KindObjectLiteralExpression: {
        return name.Properties();
    }}

    return default;
}

public static bool IsDeclarationBindingElement(ж<Node> ᏑbindingElement) {
    ref var bindingElement = ref ᏑbindingElement.DerefOrNull();

    switch (bindingElement.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindParameter || ᴛ == KindBindingElement: {
        return true;
    }
    default: {
        return false;
    }}

}

public static ж<Node> GetTargetOfBindingOrAssignmentElement(ж<Node> ᏑbindingElement) {
    ref var bindingElement = ref ᏑbindingElement.DerefOrNull();

    if (IsDeclarationBindingElement(ᏑbindingElement)) {
        return bindingElement.Name();
    }

    if (IsObjectLiteralElement(ᏑbindingElement)) {
        switch (bindingElement.Kind) {
        case var ᴛ when ᴛ == KindPropertyAssignment: {
            return GetTargetOfBindingOrAssignmentElement(bindingElement.Initializer());
        }
        case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
            return bindingElement.Name();
        }
        case var ᴛ when ᴛ == KindSpreadAssignment: {
            return GetTargetOfBindingOrAssignmentElement(bindingElement.Expression());
        }}

        return default;
    }

    if (IsAssignmentExpression(ᏑbindingElement, true)) {
        return GetTargetOfBindingOrAssignmentElement(bindingElement.AsBinaryExpression().Left);
    }

    if (IsSpreadElement(ᏑbindingElement)) {
        return GetTargetOfBindingOrAssignmentElement(bindingElement.Expression());
    }

    return ᏑbindingElement;
}

public static ж<Node> TryGetPropertyNameOfBindingOrAssignmentElement(ж<Node> ᏑbindingElement) {
    ref var bindingElement = ref ᏑbindingElement.DerefOrNull();

    switch (bindingElement.Kind) {
    case var ᴛ when ᴛ == KindBindingElement: {
        if (bindingElement.PropertyName() != nil) {
            var propertyName = bindingElement.PropertyName();
            if (IsComputedPropertyName(propertyName) && IsStringOrNumericLiteralLike(propertyName.Expression())) {
                return propertyName.Expression();
            }
            return propertyName;
        }
        break;
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        if (bindingElement.Name() != nil) {
            var propertyName = bindingElement.Name();
            if (IsComputedPropertyName(propertyName) && IsStringOrNumericLiteralLike(propertyName.Expression())) {
                return propertyName.Expression();
            }
            return propertyName;
        }
        break;
    }
    case var ᴛ when ᴛ == KindSpreadAssignment: {
        return bindingElement.Name();
    }}

    var target = GetTargetOfBindingOrAssignmentElement(ᏑbindingElement);
    if (target != nil && IsPropertyName(target)) {
        return target;
    }
    return default;
}

public static bool ContainsObjectRestOrSpread(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((SubtreeFacts)(node.SubtreeFacts() & SubtreeContainsObjectRestOrSpread) != 0) {
        return true;
    }
    if ((SubtreeFacts)(node.SubtreeFacts() & SubtreeContainsESObjectRestOrSpread) != 0) {
        foreach (var (_, element) in range(GetElementsOfBindingOrAssignmentPattern(Ꮡnode))) {
            var target = GetTargetOfBindingOrAssignmentElement(element);
            if (target != nil && IsAssignmentPattern(target)) {
                if ((SubtreeFacts)(target.SubtreeFacts() & SubtreeContainsObjectRestOrSpread) != 0) {
                    return true;
                }
                if ((SubtreeFacts)(target.SubtreeFacts() & SubtreeContainsESObjectRestOrSpread) != 0) {
                    if (ContainsObjectRestOrSpread(target)) {
                        return true;
                    }
                }
            }
        }
    }
    return false;
}

public static bool IsEmptyObjectLiteral(ж<Node> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    return IsObjectLiteralExpression(Ꮡexpression) && len(expression.Properties()) == 0;
}

public static bool IsEmptyArrayLiteral(ж<Node> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    return IsArrayLiteralExpression(Ꮡexpression) && len(expression.Elements()) == 0;
}

public static ж<Node> GetRestIndicatorOfBindingOrAssignmentElement(ж<Node> ᏑbindingElement) {
    ref var bindingElement = ref ᏑbindingElement.DerefOrNull();

    switch (bindingElement.Kind) {
    case var ᴛ when ᴛ == KindParameter: {
        return bindingElement.AsParameterDeclaration().DotDotDotToken;
    }
    case var ᴛ when ᴛ == KindBindingElement: {
        return bindingElement.AsBindingElement().DotDotDotToken;
    }
    case var ᴛ when ᴛ == KindSpreadElement || ᴛ == KindSpreadAssignment: {
        return ᏑbindingElement;
    }}

    return default;
}

public static bool IsJSDocNameReferenceContext(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (NodeFlags)(node.Flags & NodeFlagsJSDoc) != 0 && FindAncestor(Ꮡnode, (ж<Node> nodeΔ1) => IsJSDocNameReference(nodeΔ1) || IsJSDocLinkLike(nodeΔ1)) != nil;
}

public static ж<Node> GetJSDocRoot(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return FindAncestor(node.Parent, (ж<Node> n) => n.Kind == KindJSDoc);
}

public static ж<Node> GetJSDocHost(ж<Node> node) {
    var jsDoc = GetJSDocRoot(node);
    if (jsDoc == nil) {
        return default;
    }
    return jsDoc.Parent;
}

public static ж<Node> GetHostSignatureFromJSDoc(ж<Node> node) {
    var host = GetJSDocHost(node);
    if (host == nil) {
        return default;
    }
    if (IsPropertySignatureDeclaration(host) && host.Type() != nil && IsFunctionLike(host.Type())) {
        return host.Type();
    }
    if (IsFunctionLike(host)) {
        return host;
    }
    return default;
}

public static ж<Node> GetNextJSDocCommentLocation(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent; if (parent != nil) {
        switch (parent.Kind) {
        case var ᴛ when ᴛ == KindPropertyAssignment || ᴛ == KindExportAssignment || ᴛ == KindPropertyDeclaration || ᴛ == KindVariableDeclaration || ᴛ == KindSatisfiesExpression || ᴛ == KindReturnStatement || ᴛ == KindVariableStatement || ᴛ == KindExpressionStatement: {
            return parent;
        }
        case var ᴛ when ᴛ == KindVariableDeclarationList: {
            if (parent.AsVariableDeclarationList().Declarations.Nodes[0] == Ꮡnode) {
                return parent;
            }
            break;
        }}

    }
    return default;
}

public static bool IsImportOrImportEqualsDeclaration(ж<Node> node) {
    return IsImportDeclaration(node) || IsImportEqualsDeclaration(node);
}

public static bool IsPrimitiveLiteralValue(ж<Node> Ꮡnode, bool includeBigInt) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindTrueKeyword || ᴛ == KindFalseKeyword || ᴛ == KindNumericLiteral || ᴛ == KindStringLiteral || ᴛ == KindNoSubstitutionTemplateLiteral: {
        return true;
    }
    case var ᴛ when ᴛ == KindBigIntLiteral: {
        return includeBigInt;
    }
    case var ᴛ when ᴛ == KindPrefixUnaryExpression: {
        if (node.AsPrefixUnaryExpression().Operator == KindMinusToken) {
            return IsNumericLiteral(node.AsPrefixUnaryExpression().Operand) || (includeBigInt && IsBigIntLiteral(node.AsPrefixUnaryExpression().Operand));
        }
        if (node.AsPrefixUnaryExpression().Operator == KindPlusToken) {
            return IsNumericLiteral(node.AsPrefixUnaryExpression().Operand);
        }
        return false;
    }
    default: {
        return false;
    }}

}

public static bool HasInferredType(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindParameter || ᴛ == KindPropertySignature || ᴛ == KindPropertyDeclaration || ᴛ == KindBindingElement || ᴛ == KindPropertyAccessExpression || ᴛ == KindElementAccessExpression || ᴛ == KindBinaryExpression || ᴛ == KindCallExpression || ᴛ == KindVariableDeclaration || ᴛ == KindExportAssignment || ᴛ == KindPropertyAssignment || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindJSDocParameterTag || ᴛ == KindJSDocPropertyTag: {
        return true;
    }
    default: {
        return false;
    }}

}

public static bool IsKeyword(Kind token) {
    return KindFirstKeyword <= token && token <= KindLastKeyword;
}

public static bool IsNonContextualKeyword(Kind token) {
    return IsKeyword(token) && !IsContextualKeyword(token);
}

public static bool HasModifier(ж<Node> Ꮡnode, ModifierFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ModifierFlags)(node.ModifierFlags() & flags) != 0;
}

public static bool IsExpandoInitializer(ж<Node> Ꮡdeclaration, ж<Node> Ꮡinitializer) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();
    ref var initializer = ref Ꮡinitializer.DerefOrNull();

    if (Ꮡinitializer == nil) {
        return false;
    }
    if (IsFunctionExpressionOrArrowFunction(Ꮡinitializer)) {
        return true;
    }
    if (IsInJSFile(Ꮡinitializer)) {
        return IsClassExpression(Ꮡinitializer) || (IsObjectLiteralExpression(Ꮡinitializer) && len(initializer.Properties()) == 0 && declaration.Type() == nil);
    }
    return false;
}

public static ж<Node> GetContainingFunction(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return FindAncestor(node.Parent, IsFunctionLike);
}

public static ж<Node> ImportFromModuleSpecifier(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var result = TryGetImportFromModuleSpecifier(Ꮡnode); if (result != nil) {
        return result;
    }
    debug.FailBadSyntaxKind(new NodeжFailBadSyntaxKind_node(node.Parent));
    return default;
}

public static ж<Node> TryGetImportFromModuleSpecifier(ж<StringLiteralLike> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Parent.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration || ᴛ == KindExportDeclaration: {
        return node.Parent;
    }
    case var ᴛ when ᴛ == KindExternalModuleReference: {
        return node.Parent.Parent;
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        if (IsImportCall(node.Parent) || IsRequireCall(node.Parent, false)) {
            return node.Parent;
        }
        return default;
    }
    case var ᴛ when ᴛ == KindLiteralType: {
        if (!IsStringLiteral(Ꮡnode)) {
            return default;
        }
        if (IsImportTypeNode(node.Parent.Parent)) {
            return node.Parent.Parent;
        }
        return default;
    }}

    return default;
}

public static bool IsImplicitlyExportedJSDocDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!IsSourceFile(node.Parent) || !IsExternalOrCommonJSModule(node.Parent.AsSourceFile())) {
        return false;
    }
    if (IsJSTypeAliasDeclaration(Ꮡnode)) {
        return true;
    }
    return IsModuleDeclaration(Ꮡnode) && (NodeFlags)(node.Flags & NodeFlagsReparsed) != 0;
}

public static bool HasContextSensitiveParameters(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.TypeParameters() == nil) {
        if (core.Some(node.Parameters(), (ж<Node> p) => p.Type() == nil)) {
            return true;
        }
        if (!IsArrowFunction(Ꮡnode)) {
            var parameter = core.FirstOrNil(node.Parameters());
            if (parameter == nil || !IsThisParameter(parameter)) {
                return (NodeFlags)(node.Flags & NodeFlagsContainsThis) != 0;
            }
        }
    }
    return false;
}

public static bool IsInfinityOrNaNString(@string name) {
    return name == "Infinity"u8 || name == "-Infinity"u8 || name == "NaN"u8;
}

public static ж<Node> GetFirstConstructorWithBody(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    foreach (var (_, member) in range(node.Members())) {
        if (IsConstructorDeclaration(member) && NodeIsPresent(member.Body())) {
            return member;
        }
    }
    return default;
}

public static bool IsPotentiallyExecutableNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (KindFirstStatement <= node.Kind && node.Kind <= KindLastStatement) {
        if (IsVariableStatement(Ꮡnode)) {
            var declarationList = node.AsVariableStatement().DeclarationList;
            if ((NodeFlags)(GetCombinedNodeFlags(declarationList) & NodeFlagsBlockScoped) != 0) {
                return true;
            }
            var declarations = declarationList.AsVariableDeclarationList().Declarations.Nodes;
            return core.Some(declarations, (ж<Node> d) => d.Initializer() != nil);
        }
        return true;
    }
    return IsClassDeclaration(Ꮡnode) || IsEnumDeclaration(Ꮡnode) || IsModuleDeclaration(Ꮡnode);
}

public static bool HasAbstractModifier(ж<Node> node) {
    return HasSyntacticModifier(node, ModifierFlagsAbstract);
}

public static bool HasAmbientModifier(ж<Node> node) {
    return HasSyntacticModifier(node, ModifierFlagsAmbient);
}

public static bool NodeCanBeDecorated(bool useLegacyDecorators, ж<Node> Ꮡnode, ж<Node> Ꮡparent, ж<Node> Ꮡgrandparent) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var parent = ref Ꮡparent.DerefOrNull();
    ref var grandparent = ref Ꮡgrandparent.DerefOrNull();

    if (useLegacyDecorators && node.Name() != nil && IsPrivateIdentifier(node.Name())) {
        return false;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == KindClassDeclaration: {
        return true;
    }
    case var ᴛ when ᴛ == KindClassExpression: {
        return !useLegacyDecorators;
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        return Ꮡparent != nil && (useLegacyDecorators && IsClassDeclaration(Ꮡparent) || !useLegacyDecorators && IsClassLike(Ꮡparent) && !HasAbstractModifier(Ꮡnode) && !HasAmbientModifier(Ꮡnode));
    }
    case var ᴛ when ᴛ == KindGetAccessor || ᴛ == KindSetAccessor || ᴛ == KindMethodDeclaration: {
        return Ꮡparent != nil && node.Body() != nil && (useLegacyDecorators && IsClassDeclaration(Ꮡparent) || !useLegacyDecorators && IsClassLike(Ꮡparent));
    }
    case var ᴛ when ᴛ == KindParameter: {
        if (!useLegacyDecorators) {
            return false;
        }
        return Ꮡparent != nil && parent.Body() != nil && (parent.Kind == KindConstructor || parent.Kind == KindMethodDeclaration || parent.Kind == KindSetAccessor) && GetThisParameter(Ꮡparent) != Ꮡnode && Ꮡgrandparent != nil && grandparent.Kind == KindClassDeclaration;
    }}

    return false;
}

public static bool ClassOrConstructorParameterIsDecorated(bool useLegacyDecorators, ж<Node> node) {
    if (NodeIsDecorated(useLegacyDecorators, node, nil, nil)) {
        return true;
    }
    var constructor = GetFirstConstructorWithBody(node);
    return constructor != nil && ChildIsDecorated(useLegacyDecorators, constructor, node);
}

public static bool ClassElementOrClassElementParameterIsDecorated(bool useLegacyDecorators, ж<Node> Ꮡnode, ж<Node> Ꮡparent) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var parent = ref Ꮡparent.DerefOrNull();

    ж<NodeList> parameters = default;
    if (IsAccessor(Ꮡnode)) {
        var decls = GetAllAccessorDeclarations(parent.Members(), Ꮡnode);
        ж<Node> firstAccessorWithDecorators = default;
        if (HasDecorators(decls.FirstAccessor)) {
            firstAccessorWithDecorators = decls.FirstAccessor;
        } else if (decls.SecondAccessor != nil && HasDecorators(decls.SecondAccessor)) {
            firstAccessorWithDecorators = decls.SecondAccessor;
        }
        if (firstAccessorWithDecorators == nil || Ꮡnode != firstAccessorWithDecorators) {
            return false;
        }
        if (decls.SetAccessor != nil) {
            parameters = decls.SetAccessor.Value.Parameters;
        }
    } else if (IsMethodDeclaration(Ꮡnode)) {
        parameters = node.ParameterList();
    }
    if (NodeIsDecorated(useLegacyDecorators, Ꮡnode, Ꮡparent, nil)) {
        return true;
    }
    if (parameters != nil && len(parameters.Nodes) > 0) {
        foreach (var (_, parameter) in range(parameters.Nodes)) {
            if (IsThisParameter(parameter)) {
                continue;
            }
            if (NodeIsDecorated(useLegacyDecorators, parameter, Ꮡnode, Ꮡparent)) {
                return true;
            }
        }
    }
    return false;
}

public static bool NodeIsDecorated(bool useLegacyDecorators, ж<Node> node, ж<Node> parent, ж<Node> grandparent) {
    return HasDecorators(node) && NodeCanBeDecorated(useLegacyDecorators, node, parent, grandparent);
}

public static bool NodeOrChildIsDecorated(bool useLegacyDecorators, ж<Node> node, ж<Node> parent, ж<Node> grandparent) {
    return NodeIsDecorated(useLegacyDecorators, node, parent, grandparent) || ChildIsDecorated(useLegacyDecorators, node, parent);
}

public static bool ChildIsDecorated(bool useLegacyDecorators, ж<Node> Ꮡnode, ж<Node> parent) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindClassDeclaration || ᴛ == KindClassExpression: {
        return core.Some(node.Members(), (ж<Node> m) => NodeOrChildIsDecorated(useLegacyDecorators, m, Ꮡnode, parent));
    }
    case var ᴛ when ᴛ == KindMethodDeclaration || ᴛ == KindSetAccessor || ᴛ == KindConstructor: {
        return core.Some(node.Parameters(), (ж<Node> p) => NodeIsDecorated(useLegacyDecorators, p, Ꮡnode, parent));
    }
    default: {
        return false;
    }}

}

partial struct AllAccessorDeclarations {
    public ж<AccessorDeclaration> FirstAccessor;
    public ж<AccessorDeclaration> SecondAccessor;
    public ж<SetAccessorDeclaration> SetAccessor;
    public ж<GetAccessorDeclaration> GetAccessor;
}

public static AllAccessorDeclarations GetAllAccessorDeclarationsForDeclaration(ж<AccessorDeclaration> Ꮡaccessor, slice<ж<Node>> declarationsOfSymbol) {
    ref var accessor = ref Ꮡaccessor.DerefOrNull();

    Kind otherKind = default;
    if (accessor.Kind == KindSetAccessor) {
        otherKind = KindGetAccessor;
    } else if (accessor.Kind == KindGetAccessor) {
        otherKind = KindSetAccessor;
    } else {
        throw panic(fmt.Sprintf("Unexpected node kind %q"u8, accessor.Kind));
    }
    ж<AccessorDeclaration> otherAccessor = default;
    foreach (var (_, d) in range(declarationsOfSymbol)) {
        if (d.Kind == otherKind) {
            otherAccessor = d;
            break;
        }
    }

    ж<AccessorDeclaration> firstAccessor = default;
    ж<AccessorDeclaration> secondAccessor = default;
    if (otherAccessor != nil && (otherAccessor.Pos() < accessor.Pos())) {
        firstAccessor = otherAccessor;
        secondAccessor = Ꮡaccessor;
    } else {
        firstAccessor = Ꮡaccessor;
        secondAccessor = otherAccessor;
    }

    ж<SetAccessorDeclaration> setAccessor = default;
    ж<GetAccessorDeclaration> getAccessor = default;
    if (accessor.Kind == KindSetAccessor) {
        setAccessor = accessor.AsSetAccessorDeclaration();
        if (otherAccessor != nil) {
            getAccessor = otherAccessor.AsGetAccessorDeclaration();
        }
    } else {
        getAccessor = accessor.AsGetAccessorDeclaration();
        if (otherAccessor != nil) {
            setAccessor = otherAccessor.AsSetAccessorDeclaration();
        }
    }

    return new AllAccessorDeclarations(
        FirstAccessor: firstAccessor,
        SecondAccessor: secondAccessor,
        SetAccessor: setAccessor,
        GetAccessor: getAccessor
    );
}

public static AllAccessorDeclarations GetAllAccessorDeclarations(slice<ж<Node>> parentDeclarations, ж<AccessorDeclaration> Ꮡaccessor) {
    ref var accessor = ref Ꮡaccessor.DerefOrNull();
    var matchesᴷ = new stackAppend<ж<Node>>();

    if (HasDynamicName(Ꮡaccessor)) {
        return GetAllAccessorDeclarationsForDeclaration(Ꮡaccessor, [Ꮡaccessor]);
    }

    @string accessorName = GetPropertyNameForPropertyNameNode(accessor.Name());
    var accessorStatic = IsStatic(Ꮡaccessor);
    slice<ж<Node>> matches = default;
    foreach (var (_, member) in range(parentDeclarations)) {
        if (!IsAccessor(member) || IsStatic(member) != accessorStatic) {
            continue;
        }
        @string memberName = GetPropertyNameForPropertyNameNode(member.Name());
        if (memberName == accessorName) {
            matches = append(matchesᴷ, matches, member);
        }
    }
    return GetAllAccessorDeclarationsForDeclaration(Ꮡaccessor, matches);
}

public static bool IsAsyncFunction(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindArrowFunction || ᴛ == KindMethodDeclaration: {
        var data = node.BodyData();
        return data.Body != nil && data.AsteriskToken == nil && HasSyntacticModifier(Ꮡnode, ModifierFlagsAsync);
    }}

    return false;
}

public static ж<Node> GetRestParameterElementType(ж<ParameterDeclarationNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return Ꮡnode;
    }
    if (node.Kind == KindArrayType) {
        return node.AsArrayTypeNode().ElementType;
    }
    if (node.Kind == KindTypeReference && (~node.AsTypeReferenceNode()).TypeArguments != nil) {
        return core.FirstOrNil((~node.AsTypeReferenceNode()).TypeArguments.Nodes);
    }
    return default;
}

public static bool TagNamesAreEquivalent(ж<Expression> Ꮡlhs, ж<Expression> Ꮡrhs) {
    ref var lhs = ref Ꮡlhs.DerefOrNull();
    ref var rhs = ref Ꮡrhs.DerefOrNull();

    if (lhs.Kind != rhs.Kind) {
        return false;
    }
    switch (lhs.Kind) {
    case var ᴛ when ᴛ == KindIdentifier: {
        return lhs.Text() == rhs.Text();
    }
    case var ᴛ when ᴛ == KindThisKeyword: {
        return true;
    }
    case var ᴛ when ᴛ == KindJsxNamespacedName: {
        return lhs.AsJsxNamespacedName().Namespace.Text() == rhs.AsJsxNamespacedName().Namespace.Text() && lhs.AsJsxNamespacedName().Name().Text() == rhs.AsJsxNamespacedName().Name().Text();
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        return lhs.AsPropertyAccessExpression().Name().Text() == rhs.AsPropertyAccessExpression().Name().Text() && TagNamesAreEquivalent(lhs.Expression(), rhs.Expression());
    }}

    throw panic("Unhandled case in TagNamesAreEquivalent");
}

public static bool IsTagName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Parent != nil && IsJSDocTag(node.Parent) && node.Parent.TagName() == Ꮡnode;
}

internal static bool literalIsName(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return IsDeclarationName(Ꮡnode) || node.Parent.Kind == KindExternalModuleReference || isArgumentOfElementAccessExpression(Ꮡnode) || IsLiteralComputedPropertyDeclarationName(Ꮡnode);
}

internal static bool isArgumentOfElementAccessExpression(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && node.Parent != nil && node.Parent.Kind == KindElementAccessExpression && node.Parent.AsElementAccessExpression().ArgumentExpression == Ꮡnode;
}

public static ж<Node> GetReparsedNodeForNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode != nil && (NodeFlags)(node.Flags & NodeFlagsJSDoc) != 0 && (NodeFlags)(node.Flags & NodeFlagsReparsed) == 0) {
        var @file = GetSourceFileOfNode(Ꮡnode); if (@file != nil && len(@file.ReparsedClones) != 0) {
            var (pos, found) = slices.BinarySearchFunc<slice<ж<Node>>, ж<Node>, ж<Node>>(@file.ReparsedClones, Ꮡnode, CompareNodePositions);
            if (!found && pos > 0) {
                pos--;
            }
            var candidate = @file.ReparsedClones[pos];
            if (node.Loc.ContainedBy(candidate.Loc)) {
                var reparsed = findCloneInNode(candidate, Ꮡnode); if (reparsed != nil) {
                    return reparsed;
                }
            }
        }
    }
    return Ꮡnode;
}

internal static ж<Node> findCloneInNode(ж<Node> Ꮡnode, ж<Node> Ꮡoriginal) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var original = ref Ꮡoriginal.DerefOrNull();

    for (;;) {
        if (node.Kind == original.Kind && node.Loc == original.Loc) {
            return Ꮡnode;
        }
        var foundContainingChild = node.ForEachChild((ж<Node> n) => {
            if (Ꮡoriginal.Value.Loc.ContainedBy(n.Loc)) {
                Ꮡnode = n;
                return true;
            }
            return false;
        });
        if (!foundContainingChild) {
            return default;
        }
    }
}

public static bool IsExpandoPropertyDeclaration(ж<Node> node) {
    return node != nil && IsBinaryExpression(node);
}

public static bool IsSuperProperty(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (IsPropertyAccessExpression(Ꮡnode) || IsElementAccessExpression(Ꮡnode)) && node.Expression().Kind == KindSuperKeyword;
}

public static bool IsNamedEvaluationSource(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        return !IsProtoSetter(node.AsPropertyAssignment().Name());
    }
    case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
        return node.AsShorthandPropertyAssignment().ObjectAssignmentInitializer != nil;
    }
    case var ᴛ when ᴛ == KindVariableDeclaration: {
        return IsIdentifier(node.AsVariableDeclaration().Name()) && node.Initializer() != nil;
    }
    case var ᴛ when ᴛ == KindParameter: {
        return IsIdentifier(node.AsParameterDeclaration().Name()) && node.Initializer() != nil && node.AsParameterDeclaration().DotDotDotToken == nil;
    }
    case var ᴛ when ᴛ == KindBindingElement: {
        return IsIdentifier(node.AsBindingElement().Name()) && node.Initializer() != nil && node.AsBindingElement().DotDotDotToken == nil;
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        return node.Initializer() != nil;
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        switch (node.AsBinaryExpression().OperatorToken.Kind) {
        case var ᴛ2 when ᴛ2 == KindEqualsToken || ᴛ2 == KindAmpersandAmpersandEqualsToken || ᴛ2 == KindBarBarEqualsToken || ᴛ2 == KindQuestionQuestionEqualsToken: {
            return IsIdentifier(node.AsBinaryExpression().Left);
        }}

        break;
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        return true;
    }}

    return false;
}

public static bool IsProtoSetter(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (IsIdentifier(Ꮡnode) || IsStringLiteral(Ꮡnode)) && node.Text() == "__proto__"u8;
}

public static bool IsStringLiteralLikeType(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindLiteralType && IsStringLiteralLike(node.AsLiteralTypeNode().Literal);
}

} // end ast_package
