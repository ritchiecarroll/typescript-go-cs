namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using strconv = strconv_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.sync;
using static printer_package;

partial class printer_package {

partial struct NodeFactory {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeFactory ΔNodeFactory;
    internal ж<EmitContext> emitContext;
}

public static ж<NodeFactory> NewNodeFactory(ж<EmitContext> context) {
    return Ꮡ(new NodeFactory(
        ΔNodeFactory: ast.NewNodeFactory(new ast.NodeFactoryHooks(
            OnCreate: context.onCreate,
            OnUpdate: context.onUpdate,
            OnClone: context.onClone
        )).Value,
        emitContext: context
    ));
}

} partial class printerᴛNodeFactory {

internal static ж<astꓸIdentifierNode> newGeneratedIdentifier(this ref NodeFactory f, GeneratedIdentifierFlags kind, @string text, ж<ast.Node> Ꮡnode, AutoGenerateOptions options) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ref var id = ref heap<AutoGenerateId>((AutoGenerateId)ᏑnextAutoGenerateId.Add(1), out var Ꮡid);

    if (len(text) == 0) {
        switch (ᐧ) {
        case {} when Ꮡnode == nil: {
            text = fmt.Sprintf("(auto@%d)"u8, id);
            break;
        }
        case {} when ast.IsMemberName(Ꮡnode): {
            text = node.Text();
            break;
        }
        default: {
            text = fmt.Sprintf("(generated@%v)"u8, ast.GetNodeId(f.emitContext.getNodeForGeneratedNameWorker(Ꮡnode, id)));
            break;
        }}

        text = FormatGeneratedName(false, options.Prefix, text, options.Suffix);
    }

    var name = f.ΔNodeFactory.NewIdentifier(text);
    var autoGenerate = Ꮡ(new AutoGenerateInfo(
        Id: id,
        Flags: (GeneratedIdentifierFlags)(kind | ((GeneratedIdentifierFlags)(options.Flags & ~GeneratedIdentifierFlagsKindMask))),
        Prefix: options.Prefix,
        Suffix: options.Suffix,
        Node: Ꮡnode
    ));
    if (f.emitContext.autoGenerate == nil) {
        f.emitContext.autoGenerate = new map<ж<astꓸMemberName>, ж<AutoGenerateInfo>>();
    }
    f.emitContext.autoGenerate[name] = autoGenerate;
    return name;
}

public static ж<astꓸIdentifierNode> NewTempVariable(this ref NodeFactory f) {
    return f.NewTempVariableEx(new AutoGenerateOptions());
}

public static ж<astꓸIdentifierNode> NewTempVariableEx(this ref NodeFactory f, AutoGenerateOptions options) {
    return f.newGeneratedIdentifier(GeneratedIdentifierFlagsAuto, "", nil, options);
}

public static ж<astꓸIdentifierNode> NewLoopVariable(this ref NodeFactory f) {
    return f.NewLoopVariableEx(new AutoGenerateOptions());
}

public static ж<astꓸIdentifierNode> NewLoopVariableEx(this ref NodeFactory f, AutoGenerateOptions options) {
    return f.newGeneratedIdentifier(GeneratedIdentifierFlagsLoop, "", nil, options);
}

public static ж<astꓸIdentifierNode> NewUniqueName(this ref NodeFactory f, @string text) {
    return f.NewUniqueNameEx(text, new AutoGenerateOptions());
}

public static ж<astꓸIdentifierNode> NewUniqueNameEx(this ref NodeFactory f, @string text, AutoGenerateOptions options) {
    return f.newGeneratedIdentifier(GeneratedIdentifierFlagsUnique, text, nil, options);
}

public static ж<astꓸIdentifierNode> NewGeneratedNameForNode(this ref NodeFactory f, ж<ast.Node> node) {
    return f.NewGeneratedNameForNodeEx(node, new AutoGenerateOptions());
}

public static ж<astꓸIdentifierNode> NewGeneratedNameForNodeEx(this ref NodeFactory f, ж<ast.Node> node, AutoGenerateOptions options) {
    if (len(options.Prefix) > 0 || len(options.Suffix) > 0) {
        options.Flags |= (GeneratedIdentifierFlags)(GeneratedIdentifierFlagsOptimistic);
    }

    return f.newGeneratedIdentifier(GeneratedIdentifierFlagsNode, "", node, options);
}

internal static ж<astꓸPrivateIdentifierNode> newGeneratedPrivateIdentifier(this ref NodeFactory f, GeneratedIdentifierFlags kind, @string text, ж<ast.Node> Ꮡnode, AutoGenerateOptions options) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ref var id = ref heap<AutoGenerateId>((AutoGenerateId)ᏑnextAutoGenerateId.Add(1), out var Ꮡid);

    if (len(text) == 0) {
        switch (ᐧ) {
        case {} when Ꮡnode == nil: {
            text = fmt.Sprintf("(auto@%d)"u8, id);
            break;
        }
        case {} when ast.IsMemberName(Ꮡnode): {
            text = node.Text();
            break;
        }
        default: {
            text = fmt.Sprintf("(generated@%v)"u8, ast.GetNodeId(f.emitContext.getNodeForGeneratedNameWorker(Ꮡnode, id)));
            break;
        }}

        text = FormatGeneratedName(true, options.Prefix, text, options.Suffix);
    } else if (!strings.HasPrefix(text, "#")) {
        throw panic("First character of private identifier must be #: " + text);
    }

    var name = f.ΔNodeFactory.NewPrivateIdentifier(text);
    var autoGenerate = Ꮡ(new AutoGenerateInfo(
        Id: id,
        Flags: (GeneratedIdentifierFlags)(kind | ((GeneratedIdentifierFlags)(options.Flags & ~(nint)GeneratedIdentifierFlagsKindMask))),
        Prefix: options.Prefix,
        Suffix: options.Suffix,
        Node: Ꮡnode
    ));
    if (f.emitContext.autoGenerate == nil) {
        f.emitContext.autoGenerate = new map<ж<astꓸMemberName>, ж<AutoGenerateInfo>>();
    }
    f.emitContext.autoGenerate[name] = autoGenerate;
    return name;
}

public static ж<astꓸPrivateIdentifierNode> NewUniquePrivateName(this ref NodeFactory f, @string text) {
    return f.NewUniquePrivateNameEx(text, new AutoGenerateOptions());
}

public static ж<astꓸPrivateIdentifierNode> NewUniquePrivateNameEx(this ref NodeFactory f, @string text, AutoGenerateOptions options) {
    return f.newGeneratedPrivateIdentifier(GeneratedIdentifierFlagsUnique, text, nil, options);
}

public static ж<astꓸPrivateIdentifierNode> NewGeneratedPrivateNameForNode(this ref NodeFactory f, ж<ast.Node> node) {
    return f.NewGeneratedPrivateNameForNodeEx(node, new AutoGenerateOptions());
}

public static ж<astꓸPrivateIdentifierNode> NewGeneratedPrivateNameForNodeEx(this ref NodeFactory f, ж<ast.Node> node, AutoGenerateOptions options) {
    if (len(options.Prefix) > 0 || len(options.Suffix) > 0) {
        options.Flags |= (GeneratedIdentifierFlags)(GeneratedIdentifierFlagsOptimistic);
    }

    return f.newGeneratedPrivateIdentifier(GeneratedIdentifierFlagsNode, "", node, options);
}

public static ж<ast.Node> NewStringLiteralFromNode(this ref NodeFactory f, ж<ast.Node> ᏑtextSourceNode) {
    ref var textSourceNode = ref ᏑtextSourceNode.DerefOrNull();

    @string text = default;
    switch (textSourceNode.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindPrivateIdentifier || ᴛ == ast.KindJsxNamespacedName || ᴛ == ast.KindStringLiteral || ᴛ == ast.KindNumericLiteral || ᴛ == ast.KindBigIntLiteral || ᴛ == ast.KindNoSubstitutionTemplateLiteral || ᴛ == ast.KindTemplateHead || ᴛ == ast.KindTemplateMiddle || ᴛ == ast.KindTemplateTail || ᴛ == ast.KindRegularExpressionLiteral: {
        text = textSourceNode.Text();
        break;
    }}

    var node = f.ΔNodeFactory.NewStringLiteral(text, ast.TokenFlagsNone);
    if (f.emitContext.textSource == nil) {
        f.emitContext.textSource = new map<ж<astꓸStringLiteralNode>, ж<ast.Node>>();
    }
    f.emitContext.textSource[node] = ᏑtextSourceNode;
    return node;
}

public static ж<astꓸExpression> NewThisExpression(this ref NodeFactory f) {
    return f.ΔNodeFactory.NewKeywordExpression(ast.KindThisKeyword);
}

public static ж<astꓸExpression> NewTrueExpression(this ref NodeFactory f) {
    return f.ΔNodeFactory.NewKeywordExpression(ast.KindTrueKeyword);
}

public static ж<astꓸExpression> NewFalseExpression(this ref NodeFactory f) {
    return f.ΔNodeFactory.NewKeywordExpression(ast.KindFalseKeyword);
}

public static ж<astꓸExpression> NewCommaExpression(this ref NodeFactory f, ж<astꓸExpression> left, ж<astꓸExpression> right) {
    return f.ΔNodeFactory.NewBinaryExpression(nil, left, nil, f.ΔNodeFactory.NewToken(ast.KindCommaToken), right);
}

public static ж<astꓸExpression> NewAssignmentExpression(this ref NodeFactory f, ж<astꓸExpression> left, ж<astꓸExpression> right) {
    return f.ΔNodeFactory.NewBinaryExpression(nil, left, nil, f.ΔNodeFactory.NewToken(ast.KindEqualsToken), right);
}

public static ж<astꓸExpression> NewLogicalORExpression(this ref NodeFactory f, ж<astꓸExpression> left, ж<astꓸExpression> right) {
    return f.ΔNodeFactory.NewBinaryExpression(nil, left, nil, f.ΔNodeFactory.NewToken(ast.KindBarBarToken), right);
}

public static ж<astꓸExpression> NewLogicalANDExpression(this ref NodeFactory f, ж<astꓸExpression> left, ж<astꓸExpression> right) {
    return f.ΔNodeFactory.NewBinaryExpression(nil, left, nil, f.ΔNodeFactory.NewToken(ast.KindAmpersandAmpersandToken), right);
}

public static ж<astꓸExpression> NewStrictEqualityExpression(this ref NodeFactory f, ж<astꓸExpression> left, ж<astꓸExpression> right) {
    return f.ΔNodeFactory.NewBinaryExpression(nil, left, nil, f.ΔNodeFactory.NewToken(ast.KindEqualsEqualsEqualsToken), right);
}

public static ж<astꓸExpression> NewStrictInequalityExpression(this ref NodeFactory f, ж<astꓸExpression> left, ж<astꓸExpression> right) {
    return f.ΔNodeFactory.NewBinaryExpression(nil, left, nil, f.ΔNodeFactory.NewToken(ast.KindExclamationEqualsEqualsToken), right);
}

public static ж<astꓸExpression> NewVoidZeroExpression(this ref NodeFactory f) {
    return f.ΔNodeFactory.NewVoidExpression(f.ΔNodeFactory.NewNumericLiteral("0", ast.TokenFlagsNone));
}

} partial class printer_package {

internal static slice<ж<astꓸExpression>> flattenCommaElement(ж<astꓸExpression> Ꮡnode, slice<ж<astꓸExpression>> expressions) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBinaryExpression(Ꮡnode) && ast.NodeIsSynthesized(Ꮡnode) && node.AsBinaryExpression().OperatorToken.Kind == ast.KindCommaToken) {
        expressions = flattenCommaElement(node.AsBinaryExpression().Left, expressions);
        expressions = flattenCommaElement(node.AsBinaryExpression().Right, expressions);
    } else {
        expressions = append(expressions, Ꮡnode);
    }
    return expressions;
}

internal static slice<ж<astꓸExpression>> flattenCommaElements(slice<ж<astꓸExpression>> expressions) {
    slice<ж<astꓸExpression>> result = default;
    foreach (var (_, expression) in range(expressions)) {
        result = flattenCommaElement(expression, result);
    }
    return result;
}

} partial class printerᴛNodeFactory {

public static ж<astꓸExpression> InlineExpressions(this ref NodeFactory f, slice<ж<astꓸExpression>> expressions) {
    if (len(expressions) == 0) {
        return default;
    }
    if (len(expressions) == 1) {
        return expressions[0];
    }
    expressions = flattenCommaElements(expressions);
    var expression = expressions[0];
    foreach (var (_, next) in range(expressions[1..])) {
        expression = f.NewCommaExpression(expression, next);
    }
    return expression;
}

public static ж<astꓸExpression> CreateExpressionFromEntityName(this ж<NodeFactory> Ꮡf, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsQualifiedName(Ꮡnode)) {
        var left = Ꮡf.CreateExpressionFromEntityName(node.AsQualifiedName().Left);
        var right = node.AsQualifiedName().Right.Clone(new ast.NodeFactoryжNodeFactoryCoercible(Ꮡf.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory()));
        right.Loc = node.AsQualifiedName().Right.Loc;
        right.Parent = node.AsQualifiedName().Right.Parent;
        var propAccess = Ꮡf.of(NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(left, nil, right, ast.NodeFlagsNone);
        propAccess.Loc = node.Loc;
        return propAccess;
    }
    var res = node.Clone(new ast.NodeFactoryжNodeFactoryCoercible(Ꮡf.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory()));
    res.Loc = node.Loc;
    res.Parent = node.Parent;
    return res;
}

public static ж<ast.Node> RestoreEnclosingLabel(this ref NodeFactory f, ж<ast.Node> node, ж<ast.LabeledStatement> ᏑoutermostLabeledStatement) {
    ref var outermostLabeledStatement = ref ᏑoutermostLabeledStatement.DerefOrNull();

    if (ᏑoutermostLabeledStatement == nil) {
        return node;
    }
    var innerLabel = node;
    if (ast.IsLabeledStatement(outermostLabeledStatement.Statement)) {
        innerLabel = f.RestoreEnclosingLabel(node, outermostLabeledStatement.Statement.AsLabeledStatement());
    }
    return f.ΔNodeFactory.UpdateLabeledStatement(
        ᏑoutermostLabeledStatement,
        outermostLabeledStatement.Label,
        innerLabel);
}

public static ж<ast.Node> CreateForOfBindingStatement(this ref NodeFactory f, ж<ast.Node> Ꮡnode, ж<ast.Node> boundValue) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsVariableDeclarationList(Ꮡnode)) {
        var firstDeclaration = node.AsVariableDeclarationList().Declarations.Nodes[0];
        var updatedDeclaration = f.ΔNodeFactory.UpdateVariableDeclaration(
            firstDeclaration.AsVariableDeclaration(),
            firstDeclaration.Name(),
            nil,
            nil,
            boundValue);
        var statementΔ1 = f.ΔNodeFactory.NewVariableStatement(
            nil,
            f.ΔNodeFactory.UpdateVariableDeclarationList(
                node.AsVariableDeclarationList(),
                f.ΔNodeFactory.NewNodeList([updatedDeclaration]),
                node.AsVariableDeclarationList().Flags));
        statementΔ1.Loc = node.Loc;
        return statementΔ1;
    }
    var updatedExpression = f.NewAssignmentExpression(Ꮡnode, boundValue);
    updatedExpression.Loc = node.Loc;
    var statement = f.ΔNodeFactory.NewExpressionStatement(updatedExpression);
    statement.Loc = node.Loc;
    return statement;
}

public static ж<ast.Node> NewTypeCheck(this ref NodeFactory f, ж<ast.Node> value, @string tag) {
    if (tag == "null"u8) {
        return f.NewStrictEqualityExpression(value, f.ΔNodeFactory.NewKeywordExpression(ast.KindNullKeyword));
    } else if (tag == "undefined"u8) {
        return f.NewStrictEqualityExpression(value, f.NewVoidZeroExpression());
    } else {
        return f.NewStrictEqualityExpression(f.ΔNodeFactory.NewTypeOfExpression(value), f.ΔNodeFactory.NewStringLiteral(tag, ast.TokenFlagsNone));
    }
}

public static ж<ast.Node> NewMethodCall(this ref NodeFactory f, ж<ast.Node> Ꮡobject, ж<ast.Node> methodName, slice<ж<ast.Node>> argumentsList) {
    ref var @object = ref Ꮡobject.DerefOrNull();

    if (ast.IsCallExpression(Ꮡobject) && ((ast.NodeFlags)(@object.Flags & ast.NodeFlagsOptionalChain) != 0)) {
        return f.ΔNodeFactory.NewCallExpression(
            f.ΔNodeFactory.NewPropertyAccessExpression(Ꮡobject, nil, methodName, ast.NodeFlagsNone),
            nil,
            nil,
            f.ΔNodeFactory.NewNodeList(argumentsList),
            ast.NodeFlagsOptionalChain);
    }
    return f.ΔNodeFactory.NewCallExpression(
        f.ΔNodeFactory.NewPropertyAccessExpression(Ꮡobject, nil, methodName, ast.NodeFlagsNone),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(argumentsList),
        ast.NodeFlagsNone);
}

public static ж<ast.Node> NewGlobalMethodCall(this ref NodeFactory f, @string globalObjectName, @string methodName, slice<ж<ast.Node>> argumentsList) {
    return f.NewMethodCall(f.ΔNodeFactory.NewIdentifier(globalObjectName), f.ΔNodeFactory.NewIdentifier(methodName), argumentsList);
}

public static ж<ast.Node> NewFunctionCallCall(this ref NodeFactory f, ж<astꓸExpression> target, ж<astꓸExpression> thisArg, slice<ж<ast.Node>> argumentsList) {
    if (thisArg == nil) {
        throw panic("Attempted to construct function call call without this argument expression");
    }
    var args = appendꓸꓸꓸ(slice<ж<astꓸExpression>>([thisArg]), argumentsList);
    return f.NewMethodCall(target, f.ΔNodeFactory.NewIdentifier("call"), args);
}

public static ж<ast.Node> NewArraySliceCall(this ref NodeFactory f, ж<astꓸExpression> Δarray, nint start) {
    slice<ж<ast.Node>> args = default;
    if (start != 0) {
        args = append(args, f.ΔNodeFactory.NewNumericLiteral(strconv.Itoa(start), ast.TokenFlagsNone));
    }
    return f.NewMethodCall(Δarray, f.ΔNodeFactory.NewIdentifier("slice"), args);
}

internal static bool isIgnorableParen(this ref NodeFactory f, ж<astꓸExpression> node) {
    return ast.IsParenthesizedExpression(node) && ast.NodeIsSynthesized(node) && ast.RangeIsSynthesized(f.emitContext.SourceMapRange(node)) && ast.RangeIsSynthesized(f.emitContext.CommentRange(node));
}

internal static ж<astꓸExpression> updateOuterExpression(this ref NodeFactory f, ж<astꓸExpression> ᏑouterExpression, ж<astꓸExpression> expression) {
    ref var outerExpression = ref ᏑouterExpression.DerefOrNull();

    switch (outerExpression.Kind) {
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return f.ΔNodeFactory.UpdateParenthesizedExpression(outerExpression.AsParenthesizedExpression(), expression);
    }
    case var ᴛ when ᴛ == ast.KindTypeAssertionExpression: {
        return f.ΔNodeFactory.UpdateTypeAssertion(outerExpression.AsTypeAssertion(), outerExpression.Type(), expression);
    }
    case var ᴛ when ᴛ == ast.KindAsExpression: {
        return f.ΔNodeFactory.UpdateAsExpression(outerExpression.AsAsExpression(), expression, outerExpression.Type());
    }
    case var ᴛ when ᴛ == ast.KindSatisfiesExpression: {
        return f.ΔNodeFactory.UpdateSatisfiesExpression(outerExpression.AsSatisfiesExpression(), expression, outerExpression.Type());
    }
    case var ᴛ when ᴛ == ast.KindNonNullExpression: {
        return f.ΔNodeFactory.UpdateNonNullExpression(outerExpression.AsNonNullExpression(), expression, outerExpression.Flags);
    }
    case var ᴛ when ᴛ == ast.KindExpressionWithTypeArguments: {
        return f.ΔNodeFactory.UpdateExpressionWithTypeArguments(outerExpression.AsExpressionWithTypeArguments(), expression, outerExpression.TypeArgumentList());
    }
    case var ᴛ when ᴛ == ast.KindPartiallyEmittedExpression: {
        return f.ΔNodeFactory.UpdatePartiallyEmittedExpression(outerExpression.AsPartiallyEmittedExpression(), expression);
    }
    default: {
        throw panic(fmt.Sprintf("Unexpected outer expression kind: %s"u8, outerExpression.Kind));
        break;
    }}

}

public static ж<astꓸExpression> RestoreOuterExpressions(this ref NodeFactory f, ж<astꓸExpression> ᏑouterExpression, ж<astꓸExpression> innerExpression, ast.OuterExpressionKinds kinds) {
    ref var outerExpression = ref ᏑouterExpression.DerefOrNull();

    if (ᏑouterExpression != nil && ast.IsOuterExpression(ᏑouterExpression, kinds) && !f.isIgnorableParen(ᏑouterExpression)) {
        return f.updateOuterExpression(
            ᏑouterExpression,
            f.RestoreOuterExpressions(outerExpression.Expression(), innerExpression, ast.OEKAll));
    }
    return innerExpression;
}

public static slice<ж<astꓸStatement>> EnsureUseStrict(this ref NodeFactory f, slice<ж<astꓸStatement>> statements) {
    foreach (var (_, statement) in range(statements)) {
        if (ast.IsPrologueDirective(statement) && statement.Expression().Text() == "use strict"u8) {
            return statements;
        } else {
            break;
        }
    }
    var useStrictPrologue = f.ΔNodeFactory.NewExpressionStatement(f.ΔNodeFactory.NewStringLiteral("use strict", ast.TokenFlagsNone));
    statements = appendꓸꓸꓸ(slice<ж<astꓸStatement>>([useStrictPrologue]), statements);
    return statements;
}

public static (slice<ж<astꓸStatement>> prologue, slice<ж<astꓸStatement>> rest) SplitStandardPrologue(this ref NodeFactory f, slice<ж<astꓸStatement>> source) {
    foreach (var (i, statement) in range(source)) {
        if (!ast.IsPrologueDirective(statement)) {
            return (source.slice(0, i), source.slice(i));
        }
    }
    return (source, default);
}

public static (slice<ж<astꓸStatement>> prologue, slice<ж<astꓸStatement>> rest) SplitCustomPrologue(this ref NodeFactory f, slice<ж<astꓸStatement>> source) {
    foreach (var (i, statement) in range(source)) {
        if (ast.IsPrologueDirective(statement) || (EmitFlags)(f.emitContext.EmitFlags(statement) & EFCustomPrologue) == 0) {
            return (source.slice(0, i), source.slice(i));
        }
    }
    return (default, source);
}

} partial class printer_package {

partial struct NameOptions {
    public bool AllowComments;
    public bool AllowSourceMaps;
}

partial struct AssignedNameOptions {
    public bool AllowComments;
    public bool AllowSourceMaps;
    public bool IgnoreAssignedName;
}

} partial class printerᴛNodeFactory {

internal static ж<astꓸIdentifierNode> getName(this ж<NodeFactory> Ꮡf, ж<astꓸDeclaration> node, EmitFlags emitFlags, AssignedNameOptions opts) {
    ref var f = ref Ꮡf.DerefOrNull();

    ж<astꓸIdentifierNode> nodeName = default;
    if (node != nil) {
        if (opts.IgnoreAssignedName) {
            nodeName = ast.GetNonAssignedNameOfDeclaration(node);
        } else {
            nodeName = ast.GetNameOfDeclaration(node);
        }
    }

    if (nodeName != nil) {
        var name = nodeName.Clone(new NodeFactoryжNodeFactoryCoercible(Ꮡf));
        if (!opts.AllowComments) {
            emitFlags |= (EmitFlags)(EFNoComments);
        }
        if (!opts.AllowSourceMaps) {
            emitFlags |= (EmitFlags)(EFNoSourceMap);
        }
        f.emitContext.AddEmitFlags(name, emitFlags);
        return name;
    }

    return f.NewGeneratedNameForNode(node);
}

public static ж<astꓸIdentifierNode> GetLocalName(this ж<NodeFactory> Ꮡf, ж<astꓸDeclaration> node) {
    return Ꮡf.GetLocalNameEx(node, new AssignedNameOptions());
}

public static ж<astꓸIdentifierNode> GetLocalNameEx(this ж<NodeFactory> Ꮡf, ж<astꓸDeclaration> node, AssignedNameOptions opts) {
    return Ꮡf.getName(node, EFLocalName, opts);
}

public static ж<astꓸIdentifierNode> GetExportName(this ж<NodeFactory> Ꮡf, ж<astꓸDeclaration> node) {
    return Ꮡf.GetExportNameEx(node, new AssignedNameOptions());
}

public static ж<astꓸIdentifierNode> GetExportNameEx(this ж<NodeFactory> Ꮡf, ж<astꓸDeclaration> node, AssignedNameOptions opts) {
    return Ꮡf.getName(node, EFExportName, opts);
}

public static ж<astꓸIdentifierNode> GetDeclarationName(this ж<NodeFactory> Ꮡf, ж<astꓸDeclaration> node) {
    return Ꮡf.GetDeclarationNameEx(node, new NameOptions());
}

public static ж<astꓸIdentifierNode> GetDeclarationNameEx(this ж<NodeFactory> Ꮡf, ж<astꓸDeclaration> node, NameOptions opts) {
    return Ꮡf.getName(node, EFNone, new AssignedNameOptions(AllowComments: opts.AllowComments, AllowSourceMaps: opts.AllowSourceMaps));
}

public static ж<astꓸIdentifierNode> GetNamespaceMemberName(this ж<NodeFactory> Ꮡf, ж<astꓸIdentifierNode> ns, ж<astꓸIdentifierNode> Ꮡname, NameOptions opts) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    if (!f.emitContext.HasAutoGenerateInfo(Ꮡname)) {
        Ꮡname = name.Clone(new NodeFactoryжNodeFactoryCoercible(Ꮡf)); name = ref Ꮡname.DerefOrNull();
    }
    var qualifiedName = Ꮡf.of(NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(ns, nil, Ꮡname, ast.NodeFlagsNone);
    f.emitContext.AssignCommentAndSourceMapRanges(qualifiedName, Ꮡname);
    if (!opts.AllowComments) {
        f.emitContext.AddEmitFlags(qualifiedName, EFNoComments);
    }
    if (!opts.AllowSourceMaps) {
        f.emitContext.AddEmitFlags(qualifiedName, EFNoSourceMap);
    }
    return qualifiedName;
}

public static ж<ast.Node> GetExternalModuleOrNamespaceExportName(this ж<NodeFactory> Ꮡf, ж<astꓸIdentifierNode> ns, ж<astꓸDeclaration> node, bool allowComments, bool allowSourceMaps) {
    if (ns != nil && ast.HasSyntacticModifier(node, ast.ModifierFlagsExport)) {
        var nameOpts = new NameOptions(AllowComments: allowComments, AllowSourceMaps: allowSourceMaps);
        return Ꮡf.GetNamespaceMemberName(ns, Ꮡf.GetDeclarationNameEx(node, nameOpts), nameOpts);
    }
    return Ꮡf.GetExportNameEx(node, new AssignedNameOptions(AllowComments: allowComments, AllowSourceMaps: allowSourceMaps));
}

public static ж<astꓸIdentifierNode> NewUnscopedHelperName(this ref NodeFactory f, @string name) {
    var node = f.ΔNodeFactory.NewIdentifier(name);
    f.emitContext.SetEmitFlags(node, EFHelperName);
    return node;
}

public static ж<astꓸExpression> NewDecorateHelper(this ref NodeFactory f, slice<ж<ast.Node>> decoratorExpressions, ж<ast.Node> target, ж<ast.Node> memberName, ж<ast.Node> descriptor) {
    f.emitContext.RequestEmitHelper(decorateHelper);

    slice<ж<ast.Node>> argumentsArray = default;
    argumentsArray = append(argumentsArray, f.ΔNodeFactory.NewArrayLiteralExpression(f.ΔNodeFactory.NewNodeList(decoratorExpressions), true));
    argumentsArray = append(argumentsArray, target);
    if (memberName != nil) {
        argumentsArray = append(argumentsArray, memberName);
        if (descriptor != nil) {
            argumentsArray = append(argumentsArray, descriptor);
        }
    }

    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__decorate"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(argumentsArray),
        ast.NodeFlagsNone);
}

public static ж<ast.Node> NewMetadataHelper(this ref NodeFactory f, @string metadataKey, ж<ast.Node> metadataValue) {
    f.emitContext.RequestEmitHelper(metadataHelper);

    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__metadata"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(slice<ж<ast.Node>>([
            f.ΔNodeFactory.NewStringLiteral(metadataKey, ast.TokenFlagsNone),
            metadataValue
        ])),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewParamHelper(this ref NodeFactory f, ж<ast.Node> expression, nint parameterOffset, core.TextRange location) {
    f.emitContext.RequestEmitHelper(paramHelper);
    var helper = f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__param"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(slice<ж<astꓸExpression>>([f.ΔNodeFactory.NewNumericLiteral(strconv.Itoa(parameterOffset), ast.TokenFlagsNone), expression])),
        ast.NodeFlagsNone);
    helper.Loc = location;
    return helper;
}

public static ж<astꓸExpression> NewAddDisposableResourceHelper(this ref NodeFactory f, ж<astꓸExpression> envBinding, ж<astꓸExpression> value, bool async) {
    f.emitContext.RequestEmitHelper(addDisposableResourceHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__addDisposableResource"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(slice<ж<astꓸExpression>>([envBinding, value, f.ΔNodeFactory.NewKeywordExpression(core.IfElse(async, ast.KindTrueKeyword, ast.KindFalseKeyword))])),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewDisposeResourcesHelper(this ref NodeFactory f, ж<astꓸExpression> envBinding) {
    f.emitContext.RequestEmitHelper(disposeResourcesHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__disposeResources"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([envBinding]),
        ast.NodeFlagsNone);
}

} partial class printer_package {

partial struct PrivateIdentifierKind /*@string*/;

public static readonly PrivateIdentifierKind PrivateIdentifierKindField = "f"u8;
public static readonly PrivateIdentifierKind PrivateIdentifierKindMethod = "m"u8;
public static readonly PrivateIdentifierKind PrivateIdentifierKindAccessor = "a"u8;
public static readonly PrivateIdentifierKind PrivateIdentifierKindUntransformed = "untransformed"u8;

} partial class printerᴛNodeFactory {

public static ж<astꓸExpression> NewClassPrivateFieldGetHelper(this ref NodeFactory f, ж<astꓸExpression> receiver, ж<astꓸIdentifierNode> state, PrivateIdentifierKind kind, ж<astꓸIdentifierNode> fn) {
    f.emitContext.RequestEmitHelper(classPrivateFieldGetHelper);
    slice<ж<ast.Node>> args = default;
    if (fn == nil) {
        args = slice<ж<ast.Node>>([receiver, state, f.ΔNodeFactory.NewStringLiteral((@string)kind, ast.TokenFlagsNone)]);
    } else {
        args = slice<ж<ast.Node>>([receiver, state, f.ΔNodeFactory.NewStringLiteral((@string)kind, ast.TokenFlagsNone), fn]);
    }
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__classPrivateFieldGet"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(args),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewClassPrivateFieldSetHelper(this ref NodeFactory f, ж<astꓸExpression> receiver, ж<astꓸIdentifierNode> state, ж<astꓸExpression> value, PrivateIdentifierKind kind, ж<astꓸIdentifierNode> fn) {
    f.emitContext.RequestEmitHelper(classPrivateFieldSetHelper);
    slice<ж<ast.Node>> args = default;
    if (fn == nil) {
        args = slice<ж<ast.Node>>([receiver, state, value, f.ΔNodeFactory.NewStringLiteral((@string)kind, ast.TokenFlagsNone)]);
    } else {
        args = slice<ж<ast.Node>>([receiver, state, value, f.ΔNodeFactory.NewStringLiteral((@string)kind, ast.TokenFlagsNone), fn]);
    }
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__classPrivateFieldSet"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(args),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewClassPrivateFieldInHelper(this ref NodeFactory f, ж<astꓸIdentifierNode> state, ж<astꓸExpression> receiver) {
    f.emitContext.RequestEmitHelper(classPrivateFieldInHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__classPrivateFieldIn"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([state, receiver]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewObjectDefinePropertyCall(this ref NodeFactory f, ж<astꓸExpression> target, ж<astꓸExpression> name, ж<astꓸExpression> descriptor) {
    return f.ΔNodeFactory.NewCallExpression(
        f.ΔNodeFactory.NewPropertyAccessExpression(
            f.ΔNodeFactory.NewIdentifier("Object"),
            nil,
            f.ΔNodeFactory.NewIdentifier("defineProperty"),
            ast.NodeFlagsNone),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([target, name, descriptor]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewReflectGetCall(this ref NodeFactory f, ж<astꓸExpression> target, ж<astꓸExpression> propertyKey, ж<astꓸExpression> receiver) {
    return f.ΔNodeFactory.NewCallExpression(
        f.ΔNodeFactory.NewPropertyAccessExpression(
            f.ΔNodeFactory.NewIdentifier("Reflect"),
            nil,
            f.ΔNodeFactory.NewIdentifier("get"),
            ast.NodeFlagsNone),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([target, propertyKey, receiver]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewReflectSetCall(this ref NodeFactory f, ж<astꓸExpression> target, ж<astꓸExpression> propertyKey, ж<astꓸExpression> value, ж<astꓸExpression> receiver) {
    return f.ΔNodeFactory.NewCallExpression(
        f.ΔNodeFactory.NewPropertyAccessExpression(
            f.ΔNodeFactory.NewIdentifier("Reflect"),
            nil,
            f.ΔNodeFactory.NewIdentifier("set"),
            ast.NodeFlagsNone),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([target, propertyKey, value, receiver]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewFunctionBindCall(this ref NodeFactory f, ж<astꓸExpression> target, ж<astꓸExpression> thisArg, slice<ж<ast.Node>> argumentsList) {
    var args = new slice<ж<ast.Node>>(0, 1 + len(argumentsList));
    args = append(args, thisArg);
    args = appendꓸꓸꓸ(args, argumentsList);
    return f.NewMethodCall(target, f.ΔNodeFactory.NewIdentifier("bind"), args);
}

public static ж<astꓸExpression> NewImmediatelyInvokedArrowFunction(this ref NodeFactory f, slice<ж<astꓸStatement>> statements) {
    var arrow = f.ΔNodeFactory.NewArrowFunction(
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([]),
        nil,
        nil,
        f.ΔNodeFactory.NewToken(ast.KindEqualsGreaterThanToken),
        f.ΔNodeFactory.NewBlock(f.ΔNodeFactory.NewNodeList(statements), true));
    return f.ΔNodeFactory.NewCallExpression(
        f.ΔNodeFactory.NewParenthesizedExpression(arrow),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([]),
        ast.NodeFlagsNone);
}

public static ж<astꓸStatement> NewExportDefault(this ref NodeFactory f, ж<astꓸExpression> expression) {
    return f.ΔNodeFactory.NewExportAssignment(nil, false, nil, expression);
}

public static ж<astꓸStatement> NewExternalModuleExport(this ref NodeFactory f, ж<astꓸIdentifierNode> name) {
    var specifier = f.ΔNodeFactory.NewExportSpecifier(false, nil, name);
    var namedExports = f.ΔNodeFactory.NewNamedExports(f.ΔNodeFactory.NewNodeList([specifier]));
    return f.ΔNodeFactory.NewExportDeclaration(nil, false, namedExports, nil, nil);
}

public static ж<astꓸExpression> NewAssignHelper(this ref NodeFactory f, slice<ж<astꓸExpression>> attributesSegments, core.ScriptTarget scriptTarget) {
    return f.ΔNodeFactory.NewCallExpression(f.ΔNodeFactory.NewPropertyAccessExpression(f.ΔNodeFactory.NewIdentifier("Object"), nil, f.ΔNodeFactory.NewIdentifier("assign"), ast.NodeFlagsNone), nil, nil, f.ΔNodeFactory.NewNodeList(attributesSegments), ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewRestHelper(this ref NodeFactory f, ж<astꓸExpression> value, slice<ж<ast.Node>> elements, slice<ж<ast.Node>> computedTempVariables, core.TextRange location) {
    f.emitContext.RequestEmitHelper(restHelper);
    slice<ж<ast.Node>> propertyNames = default;
    nint computedTempVariableOffset = 0;
    foreach (var (i, element) in range(elements)) {
        if (i == len(elements) - 1) {
            break;
        }
        var propertyName = ast.TryGetPropertyNameOfBindingOrAssignmentElement(element);
        if (propertyName != nil) {
            if (ast.IsComputedPropertyName(propertyName)) {
                debug.Assert(computedTempVariables != nil, "Encountered computed property name but 'computedTempVariables' argument was not provided.");
                var temp = computedTempVariables[computedTempVariableOffset];
                computedTempVariableOffset++;
                propertyNames = append(propertyNames, f.ΔNodeFactory.NewConditionalExpression(
                    f.NewTypeCheck(temp, "symbol"),
                    f.ΔNodeFactory.NewToken(ast.KindQuestionToken),
                    temp,
                    f.ΔNodeFactory.NewToken(ast.KindColonToken),
                    f.ΔNodeFactory.NewBinaryExpression(nil, temp, nil, f.ΔNodeFactory.NewToken(ast.KindPlusToken), f.ΔNodeFactory.NewStringLiteral("", ast.TokenFlagsNone))));
            } else {
                propertyNames = append(propertyNames, f.NewStringLiteralFromNode(propertyName));
            }
        }
    }
    var propNames = f.ΔNodeFactory.NewArrayLiteralExpression(f.ΔNodeFactory.NewNodeList(propertyNames), false);
    propNames.Loc = location;
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__rest"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([
            value,
            propNames
        ]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewAwaitHelper(this ref NodeFactory f, ж<astꓸExpression> expression) {
    f.emitContext.RequestEmitHelper(awaitHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__await"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([expression]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewAsyncGeneratorHelper(this ref NodeFactory f, ж<astꓸExpression> generatorFunc, bool hasLexicalThis) {
    f.emitContext.RequestEmitHelper(awaitHelper);
    f.emitContext.RequestEmitHelper(asyncGeneratorHelper);

    f.emitContext.AddEmitFlags(generatorFunc, (EmitFlags)(EFAsyncFunctionBody | EFReuseTempVariableScope));

    ж<astꓸExpression> thisArg = default;
    if (hasLexicalThis) {
        thisArg = f.ΔNodeFactory.NewKeywordExpression(ast.KindThisKeyword);
    } else {
        thisArg = f.NewVoidZeroExpression();
    }

    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__asyncGenerator"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(slice<ж<astꓸExpression>>([
            thisArg,
            f.ΔNodeFactory.NewIdentifier("arguments"),
            generatorFunc
        ])),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewAsyncDelegatorHelper(this ref NodeFactory f, ж<astꓸExpression> expression) {
    f.emitContext.RequestEmitHelper(awaitHelper);
    f.emitContext.RequestEmitHelper(asyncDelegatorHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__asyncDelegator"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([expression]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewAsyncValuesHelper(this ref NodeFactory f, ж<astꓸExpression> expression) {
    f.emitContext.RequestEmitHelper(asyncValuesHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__asyncValues"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([expression]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewAwaiterHelper(this ref NodeFactory f, bool hasLexicalThis, ж<astꓸExpression> argumentsExpression, ж<ast.NodeList> parameters, ж<astꓸBlockNode> body) {
    f.emitContext.RequestEmitHelper(awaiterHelper);

    ж<ast.NodeList> @params = default;
    if (parameters != nil) {
        @params = parameters;
    } else {
        @params = f.ΔNodeFactory.NewNodeList([]);
    }

    var generatorFunc = f.ΔNodeFactory.NewFunctionExpression(
        nil,
        f.ΔNodeFactory.NewToken(ast.KindAsteriskToken),
        nil,
        nil,
        @params,
        nil,
        nil,
        body);

    f.emitContext.AddEmitFlags(generatorFunc, (EmitFlags)(EFAsyncFunctionBody | EFReuseTempVariableScope));

    ж<astꓸExpression> thisArg = default;
    if (hasLexicalThis) {
        thisArg = f.ΔNodeFactory.NewKeywordExpression(ast.KindThisKeyword);
    } else {
        thisArg = f.NewVoidZeroExpression();
    }

    ж<astꓸExpression> argsArg = default;
    if (argumentsExpression != nil) {
        argsArg = argumentsExpression;
    } else {
        argsArg = f.NewVoidZeroExpression();
    }

    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__awaiter"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(slice<ж<astꓸExpression>>([
            thisArg,
            argsArg,
            f.NewVoidZeroExpression(),
            generatorFunc
        ])),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewESDecorateClassContextObject(this ref NodeFactory f, ж<astꓸExpression> nameExpr, ж<astꓸIdentifierNode> metadata) {
    var props = slice<ж<ast.Node>>([
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("kind"), nil, nil, f.ΔNodeFactory.NewStringLiteral("class", 0)),
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("name"), nil, nil, nameExpr),
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("metadata"), nil, nil, metadata)
    ]);
    return f.ΔNodeFactory.NewObjectLiteralExpression(f.ΔNodeFactory.NewNodeList(props), false);
}

public static ж<ast.Node> NewESDecorateClassElementAccessGetMethod(this ref NodeFactory f, bool nameComputed, ж<astꓸExpression> nameExpr) {
    ж<astꓸExpression> accessor = default;
    if (nameComputed) {
        accessor = f.ΔNodeFactory.NewElementAccessExpression(f.ΔNodeFactory.NewIdentifier("obj"), nil, nameExpr, ast.NodeFlagsNone);
    } else {
        accessor = f.ΔNodeFactory.NewPropertyAccessExpression(f.ΔNodeFactory.NewIdentifier("obj"), nil, nameExpr, ast.NodeFlagsNone);
    }

    var objParam = f.ΔNodeFactory.NewParameterDeclaration(nil, nil, f.ΔNodeFactory.NewIdentifier("obj"), nil, nil, nil);

    var arrow = f.ΔNodeFactory.NewArrowFunction(
        nil, nil,
        f.ΔNodeFactory.NewNodeList([objParam]),
        nil, nil,
        f.ΔNodeFactory.NewToken(ast.KindEqualsGreaterThanToken),
        accessor);

    return f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("get"), nil, nil, arrow);
}

public static ж<ast.Node> NewESDecorateClassElementAccessSetMethod(this ref NodeFactory f, bool nameComputed, ж<astꓸExpression> nameExpr) {
    ж<astꓸExpression> accessor = default;
    if (nameComputed) {
        accessor = f.ΔNodeFactory.NewElementAccessExpression(f.ΔNodeFactory.NewIdentifier("obj"), nil, nameExpr, ast.NodeFlagsNone);
    } else {
        accessor = f.ΔNodeFactory.NewPropertyAccessExpression(f.ΔNodeFactory.NewIdentifier("obj"), nil, nameExpr, ast.NodeFlagsNone);
    }

    var assignment = f.NewAssignmentExpression(accessor, f.ΔNodeFactory.NewIdentifier("value"));
    var stmt = f.ΔNodeFactory.NewExpressionStatement(assignment);
    var body = f.ΔNodeFactory.NewBlock(f.ΔNodeFactory.NewNodeList([stmt]), false);

    var objParam = f.ΔNodeFactory.NewParameterDeclaration(nil, nil, f.ΔNodeFactory.NewIdentifier("obj"), nil, nil, nil);
    var valueParam = f.ΔNodeFactory.NewParameterDeclaration(nil, nil, f.ΔNodeFactory.NewIdentifier("value"), nil, nil, nil);

    var arrow = f.ΔNodeFactory.NewArrowFunction(
        nil, nil,
        f.ΔNodeFactory.NewNodeList([objParam, valueParam]),
        nil, nil,
        f.ΔNodeFactory.NewToken(ast.KindEqualsGreaterThanToken),
        body);

    return f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("set"), nil, nil, arrow);
}

public static ж<ast.Node> NewESDecorateClassElementAccessHasMethod(this ref NodeFactory f, bool nameComputed, ж<astꓸExpression> nameExpr) {
    ж<astꓸExpression> propertyName = default;
    if (!nameComputed && nameExpr != nil && ast.IsIdentifier(nameExpr)) {
        propertyName = f.NewStringLiteralFromNode(nameExpr);
    } else {
        propertyName = nameExpr;
    }

    var objParam = f.ΔNodeFactory.NewParameterDeclaration(nil, nil, f.ΔNodeFactory.NewIdentifier("obj"), nil, nil, nil);
    var inExpr = f.ΔNodeFactory.NewBinaryExpression(nil, propertyName, nil, f.ΔNodeFactory.NewToken(ast.KindInKeyword), f.ΔNodeFactory.NewIdentifier("obj"));

    var arrow = f.ΔNodeFactory.NewArrowFunction(
        nil, nil,
        f.ΔNodeFactory.NewNodeList([objParam]),
        nil, nil,
        f.ΔNodeFactory.NewToken(ast.KindEqualsGreaterThanToken),
        inExpr);

    return f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("has"), nil, nil, arrow);
}

public static ж<astꓸExpression> NewESDecorateClassElementAccessObject(this ref NodeFactory f, bool nameComputed, ж<astꓸExpression> nameExpr, bool hasGet, bool hasSet) {
    var accessProps = slice<ж<ast.Node>>([]);

    accessProps = append(accessProps, f.NewESDecorateClassElementAccessHasMethod(nameComputed, nameExpr));

    if (hasGet) {
        accessProps = append(accessProps, f.NewESDecorateClassElementAccessGetMethod(nameComputed, nameExpr));
    }

    if (hasSet) {
        accessProps = append(accessProps, f.NewESDecorateClassElementAccessSetMethod(nameComputed, nameExpr));
    }

    return f.ΔNodeFactory.NewObjectLiteralExpression(f.ΔNodeFactory.NewNodeList(accessProps), false);
}

public static ж<astꓸExpression> NewESDecorateClassElementContextObject(this ref NodeFactory f, @string kind, bool nameComputed, ж<astꓸExpression> nameExpr, bool isStatic, bool isPrivate, bool hasGet, bool hasSet, ж<astꓸIdentifierNode> metadata) {
    ж<astꓸExpression> nameValue = default;
    if (!nameComputed && nameExpr != nil && (ast.IsPrivateIdentifier(nameExpr) || ast.IsIdentifier(nameExpr))) {
        nameValue = f.NewStringLiteralFromNode(nameExpr);
    } else {
        nameValue = nameExpr;
    }

    var accessObj = f.NewESDecorateClassElementAccessObject(nameComputed, nameExpr, hasGet, hasSet);

    ж<ast.Node> staticExpr = default;
    if (isStatic) {
        staticExpr = f.NewTrueExpression();
    } else {
        staticExpr = f.NewFalseExpression();
    }

    ж<ast.Node> privateExpr = default;
    if (isPrivate) {
        privateExpr = f.NewTrueExpression();
    } else {
        privateExpr = f.NewFalseExpression();
    }

    var props = slice<ж<ast.Node>>([
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("kind"), nil, nil, f.ΔNodeFactory.NewStringLiteral(kind, 0)),
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("name"), nil, nil, nameValue),
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("static"), nil, nil, staticExpr),
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("private"), nil, nil, privateExpr),
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("access"), nil, nil, accessObj),
        f.ΔNodeFactory.NewPropertyAssignment(nil, f.ΔNodeFactory.NewIdentifier("metadata"), nil, nil, metadata)
    ]);
    return f.ΔNodeFactory.NewObjectLiteralExpression(f.ΔNodeFactory.NewNodeList(props), false);
}

public static ж<astꓸExpression> NewESDecorateHelper(this ref NodeFactory f, ж<astꓸExpression> ctor, ж<astꓸExpression> descriptorIn, ж<astꓸExpression> decorators, ж<astꓸExpression> contextIn, ж<astꓸExpression> initializers, ж<astꓸExpression> extraInitializers) {
    f.emitContext.RequestEmitHelper(esDecorateHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__esDecorate"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([ctor, descriptorIn, decorators, contextIn, initializers, extraInitializers]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewRunInitializersHelper(this ref NodeFactory f, ж<astꓸExpression> thisArg, ж<astꓸExpression> initializers, ж<astꓸExpression> value) {
    f.emitContext.RequestEmitHelper(runInitializersHelper);
    slice<ж<astꓸExpression>> arguments = default;
    if (value != nil) {
        arguments = [thisArg, initializers, value];
    } else {
        arguments = [thisArg, initializers];
    }
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__runInitializers"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(arguments),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewTemplateObjectHelper(this ref NodeFactory f, ж<astꓸExpression> cookedArray, ж<astꓸExpression> rawArray) {
    f.emitContext.RequestEmitHelper(makeTemplateObjectHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__makeTemplateObject"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([cookedArray, rawArray]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewPropKeyHelper(this ref NodeFactory f, ж<astꓸExpression> expr) {
    f.emitContext.RequestEmitHelper(propKeyHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__propKey"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([expr]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewSetFunctionNameHelper(this ref NodeFactory f, ж<astꓸExpression> fn, ж<astꓸExpression> name, @string prefix) {
    f.emitContext.RequestEmitHelper(setFunctionNameHelper);
    slice<ж<astꓸExpression>> arguments = default;
    if (len(prefix) > 0) {
        arguments = slice<ж<astꓸExpression>>([fn, name, f.ΔNodeFactory.NewStringLiteral(prefix, ast.TokenFlagsNone)]);
    } else {
        arguments = [fn, name];
    }
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__setFunctionName"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(arguments),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewImportDefaultHelper(this ref NodeFactory f, ж<astꓸExpression> expression) {
    f.emitContext.RequestEmitHelper(importDefaultHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__importDefault"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([expression]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewImportStarHelper(this ref NodeFactory f, ж<astꓸExpression> expression) {
    f.emitContext.RequestEmitHelper(importStarHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__importStar"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([expression]),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewExportStarHelper(this ref NodeFactory f, ж<astꓸExpression> moduleExpression, ж<astꓸExpression> exportsExpression) {
    f.emitContext.RequestEmitHelper(exportStarHelper);
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__exportStar"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList([moduleExpression, exportsExpression]),
        ast.NodeFlagsNone);
}

public static ж<ast.Node> NewAssignmentTargetWrapper(this ref NodeFactory f, ж<astꓸIdentifierNode> paramName, ж<astꓸExpression> expression) {
    var setAccessor = f.ΔNodeFactory.NewSetAccessorDeclaration(
        nil,
        f.ΔNodeFactory.NewIdentifier("value"),
        nil,
        f.ΔNodeFactory.NewNodeList(slice<ж<ast.Node>>([
            f.ΔNodeFactory.NewParameterDeclaration(nil, nil, paramName, nil, nil, nil)
        ])),
        nil,
        nil,
        f.ΔNodeFactory.NewBlock(f.ΔNodeFactory.NewNodeList(slice<ж<ast.Node>>([
            f.ΔNodeFactory.NewExpressionStatement(expression)
        ])), false));
    var objLiteral = f.ΔNodeFactory.NewObjectLiteralExpression(f.ΔNodeFactory.NewNodeList([setAccessor]), false);
    return f.ΔNodeFactory.NewPropertyAccessExpression(
        f.ΔNodeFactory.NewParenthesizedExpression(objLiteral),
        nil,
        f.ΔNodeFactory.NewIdentifier("value"),
        ast.NodeFlagsNone);
}

public static ж<astꓸExpression> NewRewriteRelativeImportExtensionsHelper(this ref NodeFactory f, ж<ast.Node> firstArgument, bool preserveJsx) {
    f.emitContext.RequestEmitHelper(rewriteRelativeImportExtensionsHelper);
    slice<ж<astꓸExpression>> arguments = default;
    if (preserveJsx) {
        arguments = slice<ж<astꓸExpression>>([firstArgument, f.ΔNodeFactory.NewToken(ast.KindTrueKeyword)]);
    } else {
        arguments = [firstArgument];
    }
    return f.ΔNodeFactory.NewCallExpression(
        f.NewUnscopedHelperName("__rewriteRelativeImportExtension"),
        nil,
        nil,
        f.ΔNodeFactory.NewNodeList(arguments),
        ast.NodeFlagsNone);
}

} // end printerᴛNodeFactory
