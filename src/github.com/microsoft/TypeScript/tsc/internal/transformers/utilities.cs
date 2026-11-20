namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class transformers_package {

public static bool IsGeneratedIdentifier(ж<printer.EmitContext> ᏑemitContext, ж<astꓸIdentifierNode> name) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    return emitContext.HasAutoGenerateInfo(name);
}

public static bool IsHelperName(ж<printer.EmitContext> ᏑemitContext, ж<astꓸIdentifierNode> name) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    return (printer.EmitFlags)(emitContext.EmitFlags(name) & printer.EFHelperName) != 0;
}

public static bool IsLocalName(ж<printer.EmitContext> ᏑemitContext, ж<astꓸIdentifierNode> name) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    return (printer.EmitFlags)(emitContext.EmitFlags(name) & printer.EFLocalName) != 0;
}

public static bool IsExportName(ж<printer.EmitContext> ᏑemitContext, ж<astꓸIdentifierNode> name) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    return (printer.EmitFlags)(emitContext.EmitFlags(name) & printer.EFExportName) != 0;
}

public static bool IsIdentifierReference(ж<astꓸIdentifierNode> name, ж<ast.Node> Ꮡparent) {
    ref var parent = ref Ꮡparent.DerefOrNull();

    switch (parent.Kind) {
    case var ᴛ when ᴛ == ast.KindBinaryExpression || ᴛ == ast.KindPrefixUnaryExpression || ᴛ == ast.KindPostfixUnaryExpression || ᴛ == ast.KindYieldExpression || ᴛ == ast.KindAsExpression || ᴛ == ast.KindSatisfiesExpression || ᴛ == ast.KindElementAccessExpression || ᴛ == ast.KindNonNullExpression || ᴛ == ast.KindSpreadElement || ᴛ == ast.KindSpreadAssignment || ᴛ == ast.KindParenthesizedExpression || ᴛ == ast.KindArrayLiteralExpression || ᴛ == ast.KindDeleteExpression || ᴛ == ast.KindTypeOfExpression || ᴛ == ast.KindVoidExpression || ᴛ == ast.KindAwaitExpression || ᴛ == ast.KindTypeAssertionExpression || ᴛ == ast.KindExpressionWithTypeArguments || ᴛ == ast.KindJsxSelfClosingElement || ᴛ == ast.KindJsxSpreadAttribute || ᴛ == ast.KindJsxExpression || ᴛ == ast.KindPartiallyEmittedExpression: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindComputedPropertyName || ᴛ == ast.KindDecorator || ᴛ == ast.KindIfStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindWithStatement || ᴛ == ast.KindReturnStatement || ᴛ == ast.KindSwitchStatement || ᴛ == ast.KindCaseClause || ᴛ == ast.KindThrowStatement || ᴛ == ast.KindExpressionStatement || ᴛ == ast.KindExportAssignment || ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindTemplateSpan: {
        return parent.Expression() == name;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindParameter || ᴛ == ast.KindBindingElement || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertySignature || ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindEnumMember || ᴛ == ast.KindJsxAttribute: {
        return parent.Initializer() == name;
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        return parent.AsShorthandPropertyAssignment().ObjectAssignmentInitializer == name;
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        return parent.Initializer() == name || parent.AsForStatement().Condition == name || parent.AsForStatement().Incrementor == name;
    }
    case var ᴛ when ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement: {
        return parent.Initializer() == name || parent.Expression() == name;
    }
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
        return parent.AsImportEqualsDeclaration().ModuleReference == name;
    }
    case var ᴛ when ᴛ == ast.KindArrowFunction: {
        return parent.Body() == name;
    }
    case var ᴛ when ᴛ == ast.KindConditionalExpression: {
        return parent.AsConditionalExpression().Condition == name || parent.AsConditionalExpression().WhenTrue == name || parent.AsConditionalExpression().WhenFalse == name;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression || ᴛ == ast.KindNewExpression: {
        return parent.Expression() == name || slices.Contains(parent.Arguments(), name);
    }
    case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
        return parent.AsTaggedTemplateExpression().Tag == name;
    }
    case var ᴛ when ᴛ == ast.KindImportAttribute: {
        return (~parent.AsImportAttribute()).Value == name;
    }
    case var ᴛ when ᴛ == ast.KindJsxOpeningElement || ᴛ == ast.KindJsxClosingElement: {
        return parent.TagName() == name;
    }
    default: {
        return false;
    }}

}

internal static ж<astꓸExpression> convertBindingElementToArrayAssignmentElement(ж<printer.EmitContext> ᏑemitContext, ж<ast.BindingElement> Ꮡelement) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();

    if (element.Name() == nil) {
        var elision = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewOmittedExpression();
        emitContext.SetOriginal(elision, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        emitContext.AssignCommentAndSourceMapRanges(elision, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return elision;
    }
    if (element.DotDotDotToken != nil) {
        var spread = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewSpreadElement(element.Name());
        emitContext.SetOriginal(spread, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        emitContext.AssignCommentAndSourceMapRanges(spread, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return spread;
    }
    var expression = convertBindingNameToAssignmentElementTarget(ᏑemitContext, element.Name());
    if (element.Initializer != nil) {
        var assignment = emitContext.Factory.NewAssignmentExpression(expression, element.Initializer);
        emitContext.SetOriginal(assignment, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        emitContext.AssignCommentAndSourceMapRanges(assignment, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return assignment;
    }
    return expression;
}

internal static ж<astꓸObjectLiteralElement> convertBindingElementToObjectAssignmentElement(ж<printer.EmitContext> ᏑemitContext, ж<ast.BindingElement> Ꮡelement) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();

    if (element.DotDotDotToken != nil) {
        var spread = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewSpreadAssignment(element.Name());
        emitContext.SetOriginal(spread, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        emitContext.AssignCommentAndSourceMapRanges(spread, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return spread;
    }
    if (element.PropertyName != nil) {
        var expression = convertBindingNameToAssignmentElementTarget(ᏑemitContext, element.Name());
        if (element.Initializer != nil) {
            expression = emitContext.Factory.NewAssignmentExpression(expression, element.Initializer);
        }
        var assignmentΔ1 = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, element.PropertyName, nil, nil, expression);
        emitContext.SetOriginal(assignmentΔ1, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        emitContext.AssignCommentAndSourceMapRanges(assignmentΔ1, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return assignmentΔ1;
    }
    ж<astꓸTokenNode> equalsToken = default;
    if (element.Initializer != nil) {
        equalsToken = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsToken);
    }
    var assignment = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewShorthandPropertyAssignment(
        nil,
        element.Name(),
        nil,
        nil,
        equalsToken,
        element.Initializer);
    emitContext.SetOriginal(assignment, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    emitContext.AssignCommentAndSourceMapRanges(assignment, Ꮡelement.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return assignment;
}

public static ж<astꓸExpression> ConvertBindingPatternToAssignmentPattern(ж<printer.EmitContext> emitContext, ж<ast.BindingPattern> Ꮡelement) {
    ref var element = ref Ꮡelement.DerefOrNull();

    switch (element.Kind) {
    case var ᴛ when ᴛ == ast.KindArrayBindingPattern: {
        return convertBindingElementToArrayAssignmentPattern(emitContext, Ꮡelement);
    }
    case var ᴛ when ᴛ == ast.KindObjectBindingPattern: {
        return convertBindingElementToObjectAssignmentPattern(emitContext, Ꮡelement);
    }
    default: {
        throw panic("Unknown binding pattern");
        break;
    }}

}

internal static ж<astꓸExpression> convertBindingElementToObjectAssignmentPattern(ж<printer.EmitContext> ᏑemitContext, ж<ast.BindingPattern> Ꮡelement) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();

    slice<ж<astꓸObjectLiteralElement>> properties = default;
    foreach (var (_, elementΔ1) in range(element.Elements.Nodes)) {
        properties = append(properties, convertBindingElementToObjectAssignmentElement(ᏑemitContext, elementΔ1.AsBindingElement()));
    }
    var propertyList = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(properties);
    propertyList.Loc = element.Elements.Loc;
    var @object = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(propertyList, false);
    emitContext.SetOriginal(@object, Ꮡelement.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    emitContext.AssignCommentAndSourceMapRanges(@object, Ꮡelement.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return @object;
}

internal static ж<astꓸExpression> convertBindingElementToArrayAssignmentPattern(ж<printer.EmitContext> ᏑemitContext, ж<ast.BindingPattern> Ꮡelement) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();

    slice<ж<astꓸExpression>> elements = default;
    foreach (var (_, elementΔ1) in range(element.Elements.Nodes)) {
        elements = append(elements, convertBindingElementToArrayAssignmentElement(ᏑemitContext, elementΔ1.AsBindingElement()));
    }
    var elementList = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(elements);
    elementList.Loc = element.Elements.Loc;
    var @object = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(elementList, false);
    emitContext.SetOriginal(@object, Ꮡelement.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    emitContext.AssignCommentAndSourceMapRanges(@object, Ꮡelement.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return @object;
}

internal static ж<astꓸExpression> convertBindingNameToAssignmentElementTarget(ж<printer.EmitContext> emitContext, ж<ast.Node> Ꮡelement) {
    ref var element = ref Ꮡelement.DerefOrNull();

    if (ast.IsBindingPattern(Ꮡelement)) {
        return ConvertBindingPatternToAssignmentPattern(emitContext, element.AsBindingPattern());
    }
    return Ꮡelement;
}

public static ж<astꓸExpression> ConvertVariableDeclarationToAssignmentExpression(ж<printer.EmitContext> ᏑemitContext, ж<ast.VariableDeclaration> Ꮡelement) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();

    if (element.Initializer == nil) {
        return default;
    }
    var expression = convertBindingNameToAssignmentElementTarget(ᏑemitContext, element.Name());
    var assignment = emitContext.Factory.NewAssignmentExpression(expression, element.Initializer);
    emitContext.SetOriginal(assignment, Ꮡelement.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    emitContext.AssignCommentAndSourceMapRanges(assignment, Ꮡelement.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return assignment;
}

public static ж<ast.Node> SingleOrMany(slice<ж<ast.Node>> nodes, ж<printer.NodeFactory> factory) {
    if (nodes == nil) {
        return default;
    }
    if (len(nodes) == 1) {
        return nodes[0];
    }
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(nodes);
}

public static bool IsSimpleCopiableExpression(ж<astꓸExpression> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    return ast.IsStringLiteralLike(Ꮡexpression) || ast.IsNumericLiteral(Ꮡexpression) || ast.IsKeywordKind(expression.Kind) || ast.IsIdentifier(Ꮡexpression);
}

public static bool IsOriginalNodeSingleLine(ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> node) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    if (node == nil) {
        return false;
    }
    var original = emitContext.MostOriginal(node);
    if (original == nil) {
        return false;
    }
    var source = ast.GetSourceFileOfNode(original);
    if (source == nil) {
        return false;
    }
    nint startLine = scanner.GetECMALineOfPosition(new ast.SourceFileжSourceFileLike(source), original.Loc.Pos());
    nint endLine = scanner.GetECMALineOfPosition(new ast.SourceFileжSourceFileLike(source), original.Loc.End());
    return startLine == endLine;
}

public static bool IsSimpleInlineableExpression(ж<astꓸExpression> expression) {
    return !ast.IsIdentifier(expression) && IsSimpleCopiableExpression(expression);
}

public static slice<nint> FindSuperStatementIndexPath(slice<ж<astꓸStatement>> statements, nint start) {
    var indices = findSuperStatementIndexPathWorker(statements, start, default);
    slices.Reverse<slice<nint>, nint>(indices);
    return indices;
}

internal static slice<nint> findSuperStatementIndexPathWorker(slice<ж<astꓸStatement>> statements, nint start, slice<nint> indices) {
    for (nint i = start; i < len(statements); i++) {
        var statement = statements[i];
        if (GetSuperCallFromStatement(statement) != nil) {
            return append(indices, i);
        } else if (ast.IsTryStatement(statement)) {
            var result = findSuperStatementIndexPathWorker(statement.AsTryStatement().TryBlock.Statements(), 0, indices); if (result != nil) {
                return append(result, i);
            }
        }
    }
    return default;
}

public static ж<ast.Node> GetSuperCallFromStatement(ж<astꓸStatement> Ꮡstatement) {
    ref var statement = ref Ꮡstatement.DerefOrNull();

    if (!ast.IsExpressionStatement(Ꮡstatement)) {
        return default;
    }
    var expression = ast.SkipParentheses(statement.Expression());
    if (ast.IsSuperCall(expression)) {
        return expression;
    }
    return default;
}

public static core.TextRange MoveRangePastModifiers(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPropertyDeclaration(Ꮡnode) || ast.IsMethodDeclaration(Ꮡnode)) {
        return core.NewTextRange(node.Name().Pos(), node.End());
    }

    ж<ast.Node> lastModifier = default;
    if (ast.CanHaveModifiers(Ꮡnode)) {
        lastModifier = core.LastOrNil(node.ModifierNodes());
    }

    if (lastModifier != nil && !ast.PositionIsSynthesized(lastModifier.End())) {
        return core.NewTextRange(lastModifier.End(), node.End());
    }
    return MoveRangePastDecorators(Ꮡnode);
}

public static core.TextRange MoveRangePastDecorators(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> lastDecorator = default;
    if (ast.CanHaveModifiers(Ꮡnode)) {
        var nodes = node.ModifierNodes();
        if (nodes != nil) {
            lastDecorator = core.FindLast<ж<ast.Node>>(nodes, ast.IsDecorator);
        }
    }

    if (lastDecorator != nil && !ast.PositionIsSynthesized(lastDecorator.End())) {
        return core.NewTextRange(lastDecorator.End(), node.End());
    }
    return node.Loc;
}

public static ast.Kind GetNonAssignmentOperatorForCompoundAssignment(ast.Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == ast.KindPlusEqualsToken: {
        return ast.KindPlusToken;
    }
    case var ᴛ when ᴛ == ast.KindMinusEqualsToken: {
        return ast.KindMinusToken;
    }
    case var ᴛ when ᴛ == ast.KindAsteriskEqualsToken: {
        return ast.KindAsteriskToken;
    }
    case var ᴛ when ᴛ == ast.KindAsteriskAsteriskEqualsToken: {
        return ast.KindAsteriskAsteriskToken;
    }
    case var ᴛ when ᴛ == ast.KindSlashEqualsToken: {
        return ast.KindSlashToken;
    }
    case var ᴛ when ᴛ == ast.KindPercentEqualsToken: {
        return ast.KindPercentToken;
    }
    case var ᴛ when ᴛ == ast.KindLessThanLessThanEqualsToken: {
        return ast.KindLessThanLessThanToken;
    }
    case var ᴛ when ᴛ == ast.KindGreaterThanGreaterThanEqualsToken: {
        return ast.KindGreaterThanGreaterThanToken;
    }
    case var ᴛ when ᴛ == ast.KindGreaterThanGreaterThanGreaterThanEqualsToken: {
        return ast.KindGreaterThanGreaterThanGreaterThanToken;
    }
    case var ᴛ when ᴛ == ast.KindAmpersandEqualsToken: {
        return ast.KindAmpersandToken;
    }
    case var ᴛ when ᴛ == ast.KindBarEqualsToken: {
        return ast.KindBarToken;
    }
    case var ᴛ when ᴛ == ast.KindCaretEqualsToken: {
        return ast.KindCaretToken;
    }
    case var ᴛ when ᴛ == ast.KindBarBarEqualsToken: {
        return ast.KindBarBarToken;
    }
    case var ᴛ when ᴛ == ast.KindAmpersandAmpersandEqualsToken: {
        return ast.KindAmpersandAmpersandToken;
    }
    case var ᴛ when ᴛ == ast.KindQuestionQuestionEqualsToken: {
        return ast.KindQuestionQuestionToken;
    }}

    return kind;
}

} // end transformers_package
