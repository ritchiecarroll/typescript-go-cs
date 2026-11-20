namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tstransforms_package;

partial class tstransforms_package {

partial struct metadataSerializer {
    internal printer.EmitResolver resolver;
    internal core.ScriptTarget languageVersion;
    internal bool strictNullChecks;
    internal ж<printer.NodeFactory> f;
    internal ж<printer.EmitContext> ec;
    internal metadataSerializerContext c;
}

partial struct metadataSerializerContext {
    internal ж<ast.Node> currentLexicalScope;
    internal ж<ast.Node> currentNameScope;
    internal bool serializingConditionalTypeBranch;
}

internal static ж<metadataSerializer> newMetadataSerializer(printer.EmitResolver resolver, ж<printer.NodeFactory> f, ж<printer.EmitContext> ec, core.ScriptTarget languageVersion, bool strictNullChecks) {
    return Ꮡ(new metadataSerializer(resolver: resolver, languageVersion: languageVersion, f: f, ec: ec, strictNullChecks: strictNullChecks));
}

} partial class tstransformsᴛmetadataSerializer {

internal static void setContext(this ref metadataSerializer s, metadataSerializerContext ctx) {
    s.c = ctx;
}

internal static ж<ast.Node> SerializeTypeOfNode(this ж<metadataSerializer> Ꮡs, metadataSerializerContext ctx, ж<ast.Node> node, ж<ast.Node> container) {
    GoFrame ᒐ = default;
    try {
        ref var s = ref Ꮡs.DerefOrNull();

        ref var oldCtx = ref heap<metadataSerializerContext>(s.c, out var ᏑoldCtx);
        s.c = ctx;
        defer(Ꮡs.setContext, oldCtx, ref ᒐ);
        return Ꮡs.serializeTypeOfNode(node, container);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> SerializeParameterTypesOfNode(this ж<metadataSerializer> Ꮡs, metadataSerializerContext ctx, ж<ast.Node> node, ж<ast.Node> container) {
    GoFrame ᒐ = default;
    try {
        ref var s = ref Ꮡs.DerefOrNull();

        ref var oldCtx = ref heap<metadataSerializerContext>(s.c, out var ᏑoldCtx);
        s.c = ctx;
        defer(Ꮡs.setContext, oldCtx, ref ᒐ);
        return Ꮡs.serializeParameterTypesOfNode(node, container);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> SerializeReturnTypeOfNode(this ж<metadataSerializer> Ꮡs, metadataSerializerContext ctx, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    try {
        ref var s = ref Ꮡs.DerefOrNull();

        ref var oldCtx = ref heap<metadataSerializerContext>(s.c, out var ᏑoldCtx);
        s.c = ctx;
        defer(Ꮡs.setContext, oldCtx, ref ᒐ);
        return Ꮡs.serializeReturnTypeOfNode(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class tstransforms_package {

public static ж<ast.Node> GetSetAccessorValueParameter(ж<ast.SetAccessorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode != nil && len(node.Parameters.Nodes) > 0) {
        if (len(node.Parameters.Nodes) >= 2 && ast.IsThisParameter(node.Parameters.Nodes[0])) {
            return node.Parameters.Nodes[1];
        }
        return node.Parameters.Nodes[0];
    }
    return default;
}

internal static ж<ast.Node> getSetAccessorTypeAnnotationNode(ж<ast.SetAccessorDeclaration> node) {
    var p = GetSetAccessorValueParameter(node);
    if (p != nil && p.Type() != nil) {
        return p.Type();
    }
    return default;
}

internal static ж<ast.Node> getAccessorTypeNode(ж<ast.Node> node, ж<ast.Node> Ꮡcontainer) {
    ref var container = ref Ꮡcontainer.DerefOrNull();

    var accessors = ast.GetAllAccessorDeclarations(container.Members(), node);
    if (accessors.SetAccessor != nil) {
        return getSetAccessorTypeAnnotationNode(accessors.SetAccessor);
    }
    if (accessors.GetAccessor != nil) {
        return (~accessors.GetAccessor).Type;
    }
    return default;
}

} partial class tstransformsᴛmetadataSerializer {

internal static ж<ast.Node> serializeTypeOfNode(this ж<metadataSerializer> Ꮡs, ж<ast.Node> Ꮡnode, ж<ast.Node> container) {
    ref var s = ref Ꮡs.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindParameter: {
        return Ꮡs.serializeTypeNode(node.Type());
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
        return Ꮡs.serializeTypeNode(getAccessorTypeNode(Ꮡnode, container));
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression || ᴛ == ast.KindMethodDeclaration: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Function");
    }
    default: {
        return s.f.NewVoidZeroExpression();
    }}

}

internal static ж<ast.Node> serializeParameterTypesOfNode(this ж<metadataSerializer> Ꮡs, ж<ast.Node> Ꮡnode, ж<ast.Node> container) {
    ref var s = ref Ꮡs.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> valueDeclaration = default;
    if (ast.IsClassLike(Ꮡnode)) {
        valueDeclaration = ast.GetFirstConstructorWithBody(Ꮡnode);
    } else if (ast.IsFunctionLike(Ꮡnode) && ast.NodeIsPresent(node.Body())) {
        valueDeclaration = Ꮡnode;
    }

    if (valueDeclaration == nil) {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), false);
    }

    slice<ж<ast.Node>> expressions = default;
    var parameters = getParametersOfDecoratedDeclaration(valueDeclaration, container);
    foreach (var (i, parameter) in range(parameters.Nodes)) {
        if (i == 0 && ast.IsIdentifier(parameter.Name()) && parameter.Name().Text() == "this"u8) {
            continue;
        }
        if (parameter.AsParameterDeclaration().DotDotDotToken != nil) {
            expressions = append(expressions, Ꮡs.serializeTypeNode(ast.GetRestParameterElementType(parameter.Type())));
        } else {
            expressions = append(expressions, Ꮡs.serializeTypeOfNode(parameter, container));
        }
    }
    return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(expressions), false);
}

} partial class tstransforms_package {

internal static ж<ast.NodeList> getParametersOfDecoratedDeclaration(ж<ast.Node> Ꮡnode, ж<ast.Node> Ꮡcontainer) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var container = ref Ꮡcontainer.DerefOrNull();

    if (Ꮡcontainer != nil && node.Kind == ast.KindGetAccessor) {
        var acc = ast.GetAllAccessorDeclarations(container.Members(), Ꮡnode);
        if (acc.SetAccessor != nil) {
            return (~acc.SetAccessor).Parameters;
        }
    }
    return node.ParameterList();
}

} partial class tstransformsᴛmetadataSerializer {

internal static ж<ast.Node> serializeReturnTypeOfNode(this ж<metadataSerializer> Ꮡs, ж<ast.Node> Ꮡnode) {
    ref var s = ref Ꮡs.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsFunctionLike(Ꮡnode) && node.Type() != nil) {
        return Ꮡs.serializeTypeNode(node.Type());
    } else if (ast.IsAsyncFunction(Ꮡnode)) {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Promise");
    }
    return s.f.NewVoidZeroExpression();
}

internal static ж<ast.Node> serializeTypeNode(this ж<metadataSerializer> Ꮡs, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var s = ref Ꮡs.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if (Ꮡnode == nil) {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
        }

        Ꮡnode = ast.SkipTypeParentheses(Ꮡnode); node = ref Ꮡnode.DerefOrNull();

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindVoidKeyword || ᴛ == ast.KindUndefinedKeyword || ᴛ == ast.KindNeverKeyword: {
            return s.f.NewVoidZeroExpression();
        }
        case var ᴛ when ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType: {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Function");
        }
        case var ᴛ when ᴛ == ast.KindArrayType || ᴛ == ast.KindTupleType: {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Array");
        }
        case var ᴛ when ᴛ == ast.KindTypePredicate: {
            if (node.AsTypePredicateNode().AssertsModifier != nil) {
                return s.f.NewVoidZeroExpression();
            }
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Boolean");
        }
        case var ᴛ when ᴛ == ast.KindBooleanKeyword: {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Boolean");
        }
        case var ᴛ when ᴛ == ast.KindTemplateLiteralType || ᴛ == ast.KindStringKeyword: {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("String");
        }
        case var ᴛ when ᴛ == ast.KindObjectKeyword: {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
        }
        case var ᴛ when ᴛ == ast.KindLiteralType: {
            return s.serializeLiteralOfLiteralTypeNode(node.AsLiteralTypeNode().Literal);
        }
        case var ᴛ when ᴛ == ast.KindNumberKeyword: {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Number");
        }
        case var ᴛ when ᴛ == ast.KindBigIntKeyword: {
            return s.serializeBigIntConstructor();
        }
        case var ᴛ when ᴛ == ast.KindSymbolKeyword: {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Symbol");
        }
        case var ᴛ when ᴛ == ast.KindTypeReference: {
            return s.serializeTypeReferenceNode(node.AsTypeReferenceNode());
        }
        case var ᴛ when ᴛ == ast.KindIntersectionType: {
            return Ꮡs.serializeUnionOrIntersectionConstituents(node.AsIntersectionTypeNode().Types.Nodes, true);
        }
        case var ᴛ when ᴛ == ast.KindUnionType: {
            return Ꮡs.serializeUnionOrIntersectionConstituents(node.AsUnionTypeNode().Types.Nodes, false);
        }
        case var ᴛ when ᴛ == ast.KindConditionalType: {
            var oldState = s.c.serializingConditionalTypeBranch;
            s.c.serializingConditionalTypeBranch = true;
            defer(() => {
                Ꮡs.Value.c.serializingConditionalTypeBranch = oldState;
            }, ref ᒐ);
            return Ꮡs.serializeUnionOrIntersectionConstituents(slice<ж<ast.Node>>([node.AsConditionalTypeNode().TrueType, node.AsConditionalTypeNode().FalseType]), false);
        }
        case var ᴛ when ᴛ == ast.KindTypeOperator: {
            if (node.AsTypeOperatorNode().Operator == ast.KindReadonlyKeyword) {
                return Ꮡs.serializeTypeNode(node.Type());
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindTypeQuery || ᴛ == ast.KindIndexedAccessType || ᴛ == ast.KindMappedType || ᴛ == ast.KindTypeLiteral || ᴛ == ast.KindAnyKeyword || ᴛ == ast.KindUnknownKeyword || ᴛ == ast.KindThisType || ᴛ == ast.KindImportType: {
            break;
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocAllType || ᴛ == ast.KindJSDocVariadicType: {
            break;
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocNullableType || ᴛ == ast.KindJSDocNonNullableType || ᴛ == ast.KindJSDocOptionalType: {
            return Ꮡs.serializeTypeNode(node.Type());
        }
        default: {
            debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(Ꮡnode));
            return default;
        }}

        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> serializeUnionOrIntersectionConstituents(this ж<metadataSerializer> Ꮡs, slice<ж<ast.Node>> types, bool isIntersection) {
    ref var s = ref Ꮡs.DerefOrNull();

    ж<ast.Node> serializedType = default;
    foreach (var (_, vᴛ1) in range(types)) {
        var typeNode = vᴛ1;

        typeNode = ast.SkipTypeParentheses(typeNode);
        if (typeNode.Kind == ast.KindNeverKeyword) {
            if (isIntersection) {
                return s.f.NewVoidZeroExpression();
            }
            continue;
        }

        if (typeNode.Kind == ast.KindUnknownKeyword) {
            if (!isIntersection) {
                return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
            }
            continue;
        }

        if (typeNode.Kind == ast.KindAnyKeyword) {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
        }

        if (!s.strictNullChecks && ((ast.IsLiteralTypeNode(typeNode) && typeNode.AsLiteralTypeNode().Literal.Kind == ast.KindNullKeyword) || typeNode.Kind == ast.KindUndefinedKeyword)) {
            continue;
        }

        var serializedConstituent = Ꮡs.serializeTypeNode(typeNode);
        if (ast.IsIdentifier(serializedConstituent) && (~serializedConstituent.AsIdentifier()).Text == "Object"u8) {
            return serializedConstituent;
        }

        if (serializedType != nil) {
            if (!s.equateSerializedTypeNodes(serializedType, serializedConstituent)) {
                return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
            }
        } else {
            serializedType = serializedConstituent;
        }
    }

    if (serializedType != nil) {
        return serializedType;
    }
    return s.f.NewVoidZeroExpression();
}

internal static ж<ast.Node> serializeLiteralOfLiteralTypeNode(this ref metadataSerializer s, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindStringLiteral || ᴛ == ast.KindNoSubstitutionTemplateLiteral: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("String");
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        var operand = node.AsPrefixUnaryExpression().Operand;
        switch (operand.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindNumericLiteral || ᴛ2 == ast.KindBigIntLiteral: {
            return s.serializeLiteralOfLiteralTypeNode(operand);
        }
        default: {
            debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(operand));
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Number");
    }
    case var ᴛ when ᴛ == ast.KindBigIntLiteral: {
        return s.serializeBigIntConstructor();
    }
    case var ᴛ when ᴛ == ast.KindTrueKeyword || ᴛ == ast.KindFalseKeyword: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Boolean");
    }
    case var ᴛ when ᴛ == ast.KindNullKeyword: {
        return s.f.NewVoidZeroExpression();
    }
    default: {
        debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(Ꮡnode));
        return default;
    }}

    return default;
}

internal static ж<ast.Node> serializeTypeReferenceNode(this ref metadataSerializer s, ж<ast.TypeReferenceNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var serialScope = s.c.currentNameScope;
    if (serialScope == nil) {
        serialScope = s.c.currentLexicalScope;
    }
    var kind = s.resolver.GetTypeReferenceSerializationKind(s.ec.ParseNode(node.TypeName), s.ec.ParseNode(serialScope));
    switch (kind) {
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindUnknown: {
        if (s.c.serializingConditionalTypeBranch) {
            return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
        }

        var serialized = s.serializeEntityNameAsExpressionFallback(node.TypeName);
        var temp = s.f.NewTempVariable();
        s.ec.AddVariableDeclaration(temp);
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
            s.f.NewTypeCheck(s.f.NewAssignmentExpression(temp, serialized), "function"),
            s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
            temp,
            s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
            s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object"));
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindTypeWithConstructSignatureAndValue: {
        return s.serializeEntityNameAsExpression(node.TypeName);
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindVoidNullableOrNeverType: {
        return s.f.NewVoidZeroExpression();
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindBigIntLikeType: {
        return s.serializeBigIntConstructor();
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindBooleanType: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Boolean");
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindNumberLikeType: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Number");
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindStringLikeType: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("String");
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindArrayLikeType: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Array");
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindESSymbolType: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Symbol");
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindTypeWithCallSignature: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Function");
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindPromise: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Promise");
    }
    case var ᴛ when ᴛ == printer.TypeReferenceSerializationKindObjectType: {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object");
    }
    default: {
        debug.AssertNever(kind, "unknown type reference serialization kind");
        return default;
    }}

}

internal static ж<ast.Node> serializeBigIntConstructor(this ref metadataSerializer s) {
    if (s.languageVersion >= core.ScriptTargetES2020) {
        return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("BigInt");
    }
    return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
        s.f.NewTypeCheck(s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("BigInt"), "function"),
        s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
        s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("BigInt"),
        s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
        s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object"));
}

internal static ж<ast.Node> serializeEntityNameAsExpression(this ref metadataSerializer s, ж<astꓸEntityName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        var name = node.Clone(new printer.NodeFactoryжNodeFactoryCoercible(s.f));
        name.Loc = node.Loc;
        s.ec.UnsetOriginal(name);
        name.Parent = s.ec.ParseNode(s.c.currentLexicalScope);
        return name;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        return s.serializeQualifiedNameAsExpression(node.AsQualifiedName());
    }}

    return default;
}

internal static ж<ast.Node> serializeQualifiedNameAsExpression(this ref metadataSerializer s, ж<ast.QualifiedName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(s.serializeEntityNameAsExpression(node.Left), nil, node.Right, ast.NodeFlagsNone);
}

internal static ж<ast.Node> serializeEntityNameAsExpressionFallback(this ref metadataSerializer s, ж<astꓸEntityName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindIdentifier) {
        var copied = s.serializeEntityNameAsExpression(Ꮡnode);
        return s.createCheckedValue(copied, copied);
    }
    if (node.AsQualifiedName().Left.Kind == ast.KindIdentifier) {
        return s.createCheckedValue(s.serializeEntityNameAsExpression(node.AsQualifiedName().Left), s.serializeEntityNameAsExpression(Ꮡnode));
    }
    var left = s.serializeEntityNameAsExpressionFallback(node.AsQualifiedName().Left);
    var temp = s.f.NewTempVariable();
    s.ec.AddVariableDeclaration(temp);
    return s.f.NewLogicalANDExpression(
        s.f.NewLogicalANDExpression(
            left.AsBinaryExpression().Left,
            s.f.NewStrictInequalityExpression(s.f.NewAssignmentExpression(temp, left.AsBinaryExpression().Right), s.f.NewVoidZeroExpression())),
        s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(temp, nil, node.AsQualifiedName().Right, ast.NodeFlagsNone));
}

internal static ж<ast.Node> createCheckedValue(this ref metadataSerializer s, ж<ast.Node> left, ж<ast.Node> right) {
    return s.f.NewLogicalANDExpression(
        s.f.NewStrictInequalityExpression(s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewTypeOfExpression(left), s.f.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("undefined", ast.TokenFlagsNone)),
        right);
}

internal static bool equateSerializedTypeNodes(this ref metadataSerializer s, ж<ast.Node> Ꮡleft, ж<ast.Node> Ꮡright) {
    ref var left = ref Ꮡleft.DerefOrNull();
    ref var right = ref Ꮡright.DerefOrNull();

    if (transformers.IsGeneratedIdentifier(s.ec, Ꮡleft)) {
        return transformers.IsGeneratedIdentifier(s.ec, Ꮡright);
    }
    if (ast.IsIdentifier(Ꮡleft)) {
        return ast.IsIdentifier(Ꮡright) && left.Text() == right.Text();
    }
    if (ast.IsPropertyAccessExpression(Ꮡleft)) {
        return ast.IsPropertyAccessExpression(Ꮡright) && s.equateSerializedTypeNodes(left.Expression(), right.Expression()) && s.equateSerializedTypeNodes(left.Name(), right.Name());
    }
    if (ast.IsVoidExpression(Ꮡleft)) {
        return ast.IsVoidExpression(Ꮡright) && ast.IsNumericLiteral(left.Expression()) && ast.IsNumericLiteral(right.Expression()) && left.Expression().Text() == "0"u8 && right.Expression().Text() == "0"u8;
    }
    if (ast.IsStringLiteral(Ꮡleft)) {
        return ast.IsStringLiteral(Ꮡright) && left.Text() == right.Text();
    }
    if (ast.IsTypeOfExpression(Ꮡleft)) {
        return ast.IsTypeOfExpression(Ꮡright) && s.equateSerializedTypeNodes(left.Expression(), right.Expression());
    }
    if (ast.IsParenthesizedExpression(Ꮡleft)) {
        return ast.IsParenthesizedExpression(Ꮡright) && s.equateSerializedTypeNodes(left.Expression(), right.Expression());
    }
    if (ast.IsConditionalExpression(Ꮡleft)) {
        return ast.IsConditionalExpression(Ꮡright) && s.equateSerializedTypeNodes(left.AsConditionalExpression().Condition, right.AsConditionalExpression().Condition) && s.equateSerializedTypeNodes(left.AsConditionalExpression().WhenTrue, right.AsConditionalExpression().WhenTrue) && s.equateSerializedTypeNodes(left.AsConditionalExpression().WhenFalse, right.AsConditionalExpression().WhenFalse);
    }
    if (ast.IsBinaryExpression(Ꮡleft)) {
        return ast.IsBinaryExpression(Ꮡright) && left.AsBinaryExpression().OperatorToken.Kind == right.AsBinaryExpression().OperatorToken.Kind && s.equateSerializedTypeNodes(left.AsBinaryExpression().Left, right.AsBinaryExpression().Left) && s.equateSerializedTypeNodes(left.AsBinaryExpression().Right, right.AsBinaryExpression().Right);
    }
    return false;
}

} // end tstransformsᴛmetadataSerializer
