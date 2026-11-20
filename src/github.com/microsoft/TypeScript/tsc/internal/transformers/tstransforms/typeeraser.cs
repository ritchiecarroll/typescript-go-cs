namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static tstransforms_package;

partial class tstransforms_package {

partial struct TypeEraserTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ж<ast.Node> parentNode;
    internal ж<ast.Node> currentNode;
}

public static ж<transformers.Transformer> NewTypeEraserTransformer(ж<transformers.TransformOptions> Ꮡopt) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    var compilerOptions = opt.CompilerOptions;
    var emitContext = opt.Context;
    var tx = Ꮡ(new TypeEraserTransformer(compilerOptions: compilerOptions));
    return tx.of(TypeEraserTransformer.ᏑTransformer).NewTransformer(tx.visit, emitContext);
}

} partial class tstransformsᴛTypeEraserTransformer {

internal static ж<ast.Node> /*grandparentNode*/ pushNode(this ref TypeEraserTransformer tx, ж<ast.Node> node) {
    ж<ast.Node> grandparentNode = default;

    grandparentNode = tx.parentNode;
    tx.parentNode = tx.currentNode;
    tx.currentNode = node;
    return grandparentNode;
}

internal static void popNode(this ref TypeEraserTransformer tx, ж<ast.Node> grandparentNode) {
    tx.currentNode = tx.parentNode;
    tx.parentNode = grandparentNode;
}

internal static ж<astꓸStatement> elide(this ref TypeEraserTransformer tx, ж<astꓸStatement> node) {
    return tx.Transformer.EmitContext().NewNotEmittedStatement(node.AsNode());
}

internal static ж<ast.Node> visit(this ж<TypeEraserTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsTypeScript) == 0) {
            return Ꮡnode;
        }

        if (ast.IsStatement(Ꮡnode) && ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsAmbient)) {
            return tx.elide(Ꮡnode);
        }

        var grandparentNode = tx.pushNode(Ꮡnode);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindPublicKeyword || ᴛ == ast.KindPrivateKeyword || ᴛ == ast.KindProtectedKeyword || ᴛ == ast.KindAbstractKeyword || ᴛ == ast.KindOverrideKeyword || ᴛ == ast.KindConstKeyword || ᴛ == ast.KindDeclareKeyword || ᴛ == ast.KindReadonlyKeyword || ᴛ == ast.KindArrayType || ᴛ == ast.KindTupleType || ᴛ == ast.KindOptionalType || ᴛ == ast.KindRestType || ᴛ == ast.KindTypeLiteral || ᴛ == ast.KindTypePredicate || ᴛ == ast.KindTypeParameter || ᴛ == ast.KindAnyKeyword || ᴛ == ast.KindUnknownKeyword || ᴛ == ast.KindBooleanKeyword || ᴛ == ast.KindStringKeyword || ᴛ == ast.KindNumberKeyword || ᴛ == ast.KindNeverKeyword || ᴛ == ast.KindVoidKeyword || ᴛ == ast.KindSymbolKeyword || ᴛ == ast.KindConstructorType || ᴛ == ast.KindFunctionType || ᴛ == ast.KindTypeQuery || ᴛ == ast.KindTypeReference || ᴛ == ast.KindUnionType || ᴛ == ast.KindIntersectionType || ᴛ == ast.KindConditionalType || ᴛ == ast.KindParenthesizedType || ᴛ == ast.KindThisType || ᴛ == ast.KindTypeOperator || ᴛ == ast.KindIndexedAccessType || ᴛ == ast.KindMappedType || ᴛ == ast.KindLiteralType || ᴛ == ast.KindIndexSignature: {
            return default;
        }
        case var ᴛ when ᴛ == ast.KindInKeyword || ᴛ == ast.KindOutKeyword: {
            if (tx.parentNode == nil || !ast.IsBinaryExpression(tx.parentNode)) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindJSImportDeclaration: {
            return default;
        }
        case var ᴛ when ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindInterfaceDeclaration: {
            return tx.elide(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindNamespaceExportDeclaration: {
            return default;
        }
        case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
            if (!ast.IsIdentifier(node.Name()) || !ast.IsInstantiatedModule(Ꮡnode, tx.compilerOptions.ShouldPreserveConstEnums()) || (~getInnermostModuleDeclarationFromDottedModule(node.AsModuleDeclaration())).Body == nil) {
                return tx.elide(Ꮡnode);
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindExpressionWithTypeArguments: {
            var n = node.AsExpressionWithTypeArguments();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExpressionWithTypeArguments(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Expression), nil);
        }
        case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
            if (tx.compilerOptions.ExperimentalDecorators.IsTrue() && ast.HasSyntacticModifier(Ꮡnode, (ast.ModifierFlags)(ast.ModifierFlagsAmbient | ast.ModifierFlagsAbstract)) && ast.HasDecorators(Ꮡnode)) {
                var nΔ1 = node.AsPropertyDeclaration();
                return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(nΔ1, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(nΔ1.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers()), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(nΔ1.Name()), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~nΔ1).Initializer));
            }
            if (ast.HasSyntacticModifier(Ꮡnode, (ast.ModifierFlags)(ast.ModifierFlagsAmbient | ast.ModifierFlagsAbstract))) {
                return default;
            }
            var n = node.AsPropertyDeclaration();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers()), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Initializer));
        }
        case var ᴛ when ᴛ == ast.KindConstructor: {
            var n = node.AsConstructorDeclaration();
            if (ast.NodeIsMissing((~n).Body)) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(n, nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Parameters), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Body));
        }
        case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
            var n = node.AsMethodDeclaration();
            if (ast.NodeIsMissing((~n).Body)) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers()), n.AsteriskToken, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Parameters), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Body));
        }
        case var ᴛ when ᴛ == ast.KindGetAccessor: {
            var n = node.AsGetAccessorDeclaration();
            if (ast.NodeIsMissing((~n).Body) && ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsAbstract)) {
                return default;
            }
            var body = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Body);
            if (body == nil) {
                body = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false);
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Parameters), nil, nil, body);
        }
        case var ᴛ when ᴛ == ast.KindSetAccessor: {
            var n = node.AsSetAccessorDeclaration();
            if (ast.NodeIsMissing((~n).Body) && ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsAbstract)) {
                return default;
            }
            var body = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Body);
            if (body == nil) {
                body = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false);
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Parameters), nil, nil, body);
        }
        case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
            var n = node.AsVariableDeclaration();
            var updated = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Initializer));
            if ((~n).Type != nil) {
                Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).EmitContext().SetTypeNode(updated.AsVariableDeclaration().Name(), (~n).Type);
            }
            return updated;
        }
        case var ᴛ when ᴛ == ast.KindHeritageClause: {
            var n = node.AsHeritageClause();
            if (n.Token == ast.KindImplementsKeyword) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateHeritageClause(n, n.Token, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes(n.Types));
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            var n = node.AsClassDeclaration();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers()), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes(n.HeritageClauses), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Members));
        }
        case var ᴛ when ᴛ == ast.KindClassExpression: {
            var n = node.AsClassExpression();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers()), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes(n.HeritageClauses), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Members));
        }
        case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
            var n = node.AsFunctionDeclaration();
            if (ast.NodeIsMissing((~n).Body)) {
                return tx.elide(Ꮡnode);
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers()), n.AsteriskToken, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Parameters), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Body));
        }
        case var ᴛ when ᴛ == ast.KindFunctionExpression: {
            var n = node.AsFunctionExpression();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionExpression(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.FunctionExpression.ᏑModifiersBase).Modifiers()), n.AsteriskToken, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Parameters), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Body));
        }
        case var ᴛ when ᴛ == ast.KindArrowFunction: {
            var n = node.AsArrowFunction();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateArrowFunction(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitModifiers(n.of(ast.ArrowFunction.ᏑModifiersBase).Modifiers()), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Parameters), nil, nil, n.EqualsGreaterThanToken, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Body));
        }
        case var ᴛ when ᴛ == ast.KindParameter: {
            if (ast.IsThisParameter(Ꮡnode)) {
                return default;
            }
            var n = node.AsParameterDeclaration();
            ж<ast.ModifierList> modifiers = default;
            if (ast.IsParameterPropertyDeclaration(Ꮡnode, tx.parentNode)) {
                modifiers = transformers.ExtractModifiers(Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).EmitContext(), n.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers(), ast.ModifierFlagsParameterPropertyModifier);
            }
            if (ast.HasDecorators(Ꮡnode)) {
                var decorators = node.Decorators();
                var (visited, _) = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitSlice(decorators);
                if (modifiers == nil) {
                    modifiers = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(visited);
                } else {
                    modifiers = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slices.Concat<slice<ж<ast.Node>>, ж<ast.Node>>(modifiers.Nodes, visited));
                }
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(n, modifiers, n.DotDotDotToken, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Name()), nil, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Initializer));
        }
        case var ᴛ when ᴛ == ast.KindCallExpression: {
            var n = node.AsCallExpression();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Expression), (~n).QuestionDotToken, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Arguments), n.Flags);
        }
        case var ᴛ when ᴛ == ast.KindNewExpression: {
            var n = node.AsNewExpression();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateNewExpression(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Expression), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Arguments));
        }
        case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
            var n = node.AsTaggedTemplateExpression();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTaggedTemplateExpression(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Tag), (~n).QuestionDotToken, nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.Template), n.Flags);
        }
        case var ᴛ when ᴛ == ast.KindNonNullExpression || ᴛ == ast.KindTypeAssertionExpression || ᴛ == ast.KindAsExpression || ᴛ == ast.KindSatisfiesExpression: {
            var partial = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPartiallyEmittedExpression(Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(node.Expression()));
            Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).EmitContext().SetOriginal(partial, Ꮡnode);
            partial.Loc = node.Loc;
            return partial;
        }
        case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
            if (!ast.IsJSDocTypeAssertion(Ꮡnode)) {
                var n = node.AsParenthesizedExpression();
                var expression = ast.SkipOuterExpressions((~n).Expression, ast.OEKAllExceptAssertionsOrExpressionsWithTypeArguments);
                if (ast.IsAssertionExpression(expression) || ast.IsSatisfiesExpression(expression)) {
                    var partial = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPartiallyEmittedExpression(Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Expression));
                    Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).EmitContext().SetOriginal(partial, Ꮡnode);
                    partial.Loc = node.Loc;
                    return partial;
                }
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindJsxSelfClosingElement: {
            var n = node.AsJsxSelfClosingElement();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateJsxSelfClosingElement(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).TagName), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Attributes));
        }
        case var ᴛ when ᴛ == ast.KindJsxOpeningElement: {
            var n = node.AsJsxOpeningElement();
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateJsxOpeningElement(n, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).TagName), nil, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Attributes));
        }
        case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
            var n = node.AsImportEqualsDeclaration();
            if ((~n).IsTypeOnly) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindImportDeclaration: {
            var n = node.AsImportDeclaration();
            if ((~n).ImportClause == nil) {
                return Ꮡnode;
            }
            var importClause = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).ImportClause);
            if (importClause == nil) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(n, n.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(), importClause, (~n).ModuleSpecifier, (~n).Attributes);
        }
        case var ᴛ when ᴛ == ast.KindImportClause: {
            var n = node.AsImportClause();
            if (n.of(ast.ImportClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).IsTypeOnly()) {
                return default;
            }
            var name = n.Name();
            var namedBindings = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.NamedBindings);
            if (name == nil && namedBindings == nil) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportClause(n, n.PhaseModifier, name, namedBindings);
        }
        case var ᴛ when ᴛ == ast.KindNamedImports: {
            var n = node.AsNamedImports();
            if (len((~n).Elements.Nodes) == 0) {
                return Ꮡnode;
            }
            var elements = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Elements);
            if (!tx.compilerOptions.VerbatimModuleSyntax.IsTrue() && len(elements.Nodes) == 0) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateNamedImports(n, elements);
        }
        case var ᴛ when ᴛ == ast.KindImportSpecifier: {
            var n = node.AsImportSpecifier();
            if ((~n).IsTypeOnly) {
                return default;
            }
            return Ꮡnode;
        }
        case var ᴛ when ᴛ == ast.KindExportDeclaration: {
            var n = node.AsExportDeclaration();
            if ((~n).IsTypeOnly) {
                return default;
            }
            ж<ast.Node> exportClause = default;
            if (n.ExportClause != nil) {
                exportClause = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode(n.ExportClause);
                if (exportClause == nil) {
                    return default;
                }
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExportDeclaration(n, nil, false, exportClause, Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).ModuleSpecifier), Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNode((~n).Attributes));
        }
        case var ᴛ when ᴛ == ast.KindNamedExports: {
            var n = node.AsNamedExports();
            if (len((~n).Elements.Nodes) == 0) {
                return Ꮡnode;
            }

            var elements = Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitNodes((~n).Elements);
            if (!tx.compilerOptions.VerbatimModuleSyntax.IsTrue() && len(elements.Nodes) == 0) {
                return default;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateNamedExports(n, elements);
        }
        case var ᴛ when ᴛ == ast.KindExportSpecifier: {
            var n = node.AsExportSpecifier();
            if ((~n).IsTypeOnly) {
                return default;
            }
            return Ꮡnode;
        }
        case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
            if (ast.IsEnumConst(Ꮡnode)) {
                return Ꮡnode;
            }
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        default: {
            return Ꮡtx.of(TypeEraserTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }}

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} // end tstransformsᴛTypeEraserTransformer
