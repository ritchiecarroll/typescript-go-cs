namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static estransforms_package;

partial class estransforms_package {

partial struct objectRestSpreadTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal bool inExportedVariableStatement;
    internal bool expressionResultIsUnused;
    internal map<ж<ast.Node>, EmptyStruct> parametersWithPrecedingObjectRestOrSpread;
}

} partial class estransformsᴛobjectRestSpreadTransformer {

internal static ж<ast.Node> visit(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsESObjectRestOrSpread) == 0 && ch.parametersWithPrecedingObjectRestOrSpread == nil) {
            return Ꮡnode;
        }
        var expressionResultIsUnused = ch.expressionResultIsUnused;
        ch.expressionResultIsUnused = false;
        defer(() => {
            Ꮡch.Value.expressionResultIsUnused = expressionResultIsUnused;
        }, ref ᒐ);
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindSourceFile: {
            return ch.visitSourceFile(node.AsSourceFile());
        }
        case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
            return ch.visitObjectLiteralExpression(node.AsObjectLiteralExpression());
        }
        case var ᴛ when ᴛ == ast.KindBinaryExpression: {
            return Ꮡch.visitBinaryExpression(node.AsBinaryExpression(), expressionResultIsUnused);
        }
        case var ᴛ when ᴛ == ast.KindExpressionStatement: {
            ch.expressionResultIsUnused = true;
            return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
            ch.expressionResultIsUnused = expressionResultIsUnused;
            return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindForOfStatement: {
            return ch.visitForOftatement(node.AsForInOrOfStatement());
        }
        case var ᴛ when ᴛ == ast.KindVariableStatement: {
            return ch.visitVariableStatement(node.AsVariableStatement());
        }
        case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
            return Ꮡch.visitVariableDeclaration(node.AsVariableDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindCatchClause: {
            return Ꮡch.visitCatchClause(node.AsCatchClause());
        }
        case var ᴛ when ᴛ == ast.KindParameter: {
            return ch.visitParameter(node.AsParameterDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindConstructor: {
            return Ꮡch.visitContructorDeclaration(node.AsConstructorDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindGetAccessor: {
            return Ꮡch.visitGetAccessorDeclaration(node.AsGetAccessorDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindSetAccessor: {
            return Ꮡch.visitSetAccessorDeclaration(node.AsSetAccessorDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
            return Ꮡch.visitMethodDeclaration(node.AsMethodDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
            return Ꮡch.visitFunctionDeclaration(node.AsFunctionDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindArrowFunction: {
            return Ꮡch.visitArrowFunction(node.AsArrowFunction());
        }
        case var ᴛ when ᴛ == ast.KindFunctionExpression: {
            return Ꮡch.visitFunctionExpression(node.AsFunctionExpression());
        }
        default: {
            return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }}

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitSourceFile(this ref objectRestSpreadTransformer ch, ж<ast.SourceFile> node) {
    var visited = ch.Transformer.Visitor().VisitEachChild(node.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    ch.Transformer.EmitContext().AddEmitHelper(visited.AsNode(), ch.Transformer.EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    return visited;
}

internal static ж<ast.Node> visitParameter(this ref objectRestSpreadTransformer ch, ж<ast.ParameterDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ch.parametersWithPrecedingObjectRestOrSpread != nil) {
        var (_, ok) = ch.parametersWithPrecedingObjectRestOrSpread[Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ꟷ]; if (ok) {
            var name = node.Name();
            if (ast.IsBindingPattern(name)) {
                name = ch.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            }
            return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
                Ꮡnode,
                nil,
                node.DotDotDotToken,
                name,
                nil,
                nil,
                nil);
        }
    }
    if ((ast.SubtreeFacts)(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread) != 0) {
        return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
            Ꮡnode,
            nil,
            node.DotDotDotToken,
            ch.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            nil,
            ch.Transformer.Visitor().VisitNode(node.Initializer));
    }
    return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static map<ж<ast.Node>, EmptyStruct> collectParametersWithPrecedingObjectRestOrSpread(this ref objectRestSpreadTransformer ch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    map<ж<ast.Node>, EmptyStruct> result = default;
    foreach (var (_, parameter) in range(node.Parameters())) {
        if (result != nil) {
            result[parameter] = new EmptyStruct();
        } else if ((ast.SubtreeFacts)(parameter.SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread) != 0) {
            result = new map<ж<ast.Node>, EmptyStruct>();
        }
    }
    return result;
}

} partial class estransforms_package {

partial struct oldParamScope /*map[ж<ast.Node>, EmptyStruct]*/;

} partial class estransformsᴛobjectRestSpreadTransformer {

internal static oldParamScope enterParameterListContext(this ref objectRestSpreadTransformer ch, ж<ast.Node> node) {
    var old = ch.parametersWithPrecedingObjectRestOrSpread;
    ch.parametersWithPrecedingObjectRestOrSpread = ch.collectParametersWithPrecedingObjectRestOrSpread(node);
    return (oldParamScope)old;
}

internal static void exitParameterListContext(this ref objectRestSpreadTransformer ch, oldParamScope scope) {
    ch.parametersWithPrecedingObjectRestOrSpread = (map<ж<ast.Node>, EmptyStruct>)scope;
}

internal static ж<ast.Node> visitContructorDeclaration(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.ConstructorDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var old = ch.enterParameterListContext(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡch.exitParameterListContext, old, ref ᒐ);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(
            Ꮡnode,
            Ꮡnode.of(ast.ConstructorDeclaration.ᏑModifiersBase).Modifiers(),
            nil,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            Ꮡch.transformFunctionBody(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitGetAccessorDeclaration(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.GetAccessorDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var old = ch.enterParameterListContext(Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡch.exitParameterListContext, old, ref ᒐ);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(
            Ꮡnode,
            Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers(),
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Name()),
            nil,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            Ꮡch.transformFunctionBody(Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitSetAccessorDeclaration(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.SetAccessorDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var old = ch.enterParameterListContext(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡch.exitParameterListContext, old, ref ᒐ);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(
            Ꮡnode,
            Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers(),
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Name()),
            nil,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            Ꮡch.transformFunctionBody(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitMethodDeclaration(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.MethodDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var old = ch.enterParameterListContext(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡch.exitParameterListContext, old, ref ᒐ);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(
            Ꮡnode,
            Ꮡnode.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers(),
            node.AsteriskToken,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Name()),
            node.PostfixToken,
            nil,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            Ꮡch.transformFunctionBody(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitFunctionDeclaration(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.FunctionDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var old = ch.enterParameterListContext(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡch.exitParameterListContext, old, ref ᒐ);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(
            Ꮡnode,
            Ꮡnode.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers(),
            node.AsteriskToken,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Name()),
            nil,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            Ꮡch.transformFunctionBody(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitArrowFunction(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.ArrowFunction> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var old = ch.enterParameterListContext(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡch.exitParameterListContext, old, ref ᒐ);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateArrowFunction(
            Ꮡnode,
            Ꮡnode.of(ast.ArrowFunction.ᏑModifiersBase).Modifiers(),
            nil,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            node.EqualsGreaterThanToken,
            Ꮡch.transformFunctionBody(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitFunctionExpression(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.FunctionExpression> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var ch = ref Ꮡch.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var old = ch.enterParameterListContext(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡch.exitParameterListContext, old, ref ᒐ);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionExpression(
            Ꮡnode,
            Ꮡnode.of(ast.FunctionExpression.ᏑModifiersBase).Modifiers(),
            node.AsteriskToken,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Name()),
            nil,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            Ꮡch.transformFunctionBody(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> transformFunctionBody(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();
    var suffixᴷ = new stackAppend<ж<ast.Node>>();

    Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().StartVariableEnvironment();
    var body = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Body());
    var extras = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().EndVariableEnvironment();
    Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().StartVariableEnvironment();
    var newStatements = Ꮡch.collectObjectRestAssignments(Ꮡnode);
    extras = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().EndAndMergeVariableEnvironment(extras);
    if (len(newStatements) == 0 && len(extras) == 0) {
        return body;
    }

    if (body == nil) {
        body = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), true);
    }
    slice<ж<ast.Node>> prefix = default;
    slice<ж<ast.Node>> suffix = default;
    if (ast.IsBlock(body)) {
        var custom = false;
        foreach (var (i, statement) in range(body.Statements())) {
            if (!custom && ast.IsPrologueDirective(statement)) {
                prefix = append(prefix, statement);
            } else if ((printer.EmitFlags)(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().EmitFlags(statement) & printer.EFCustomPrologue) != 0) {
                custom = true;
                prefix = append(prefix, statement);
            } else {
                suffix = body.Statements().slice(i);
                break;
            }
        }
    } else {
        var ret = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(body);
        ret.Loc = body.Loc;
        var list = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]);
        list.Loc = body.Loc;
        body = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(list, true);
        suffix = append(suffixᴷ, suffix, ret);
    }

    var newStatementList = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(appendꓸꓸꓸ(appendꓸꓸꓸ(appendꓸꓸꓸ(prefix, extras), newStatements), suffix));
    newStatementList.Loc = body.StatementList().Loc;
    return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(body.AsBlock(), newStatementList, body.AsBlock().MultiLine);
}

internal static slice<ж<ast.Node>> collectObjectRestAssignments(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var containsPrecedingObjectRestOrSpread = false;
    slice<ж<ast.Node>> results = default;
    foreach (var (_, parameter) in range(node.Parameters())) {
        if (containsPrecedingObjectRestOrSpread) {
            if (ast.IsBindingPattern(parameter.Name())) {
                if (len(parameter.Name().Elements()) > 0) {
                    var declarations = transformers.FlattenDestructuringBinding(
                        Ꮡch.ᏑTransformer,
                        parameter, Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().NewGeneratedNameForNode(parameter),
                        transformers.FlattenLevelAll, false, false);
                    if (declarations != nil) {
                        var declarationList = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), ast.NodeFlagsNone);
                        var decls = slice<ж<ast.Node>>([declarations]);
                        if (declarations.Kind == ast.KindSyntaxList) {
                            decls = declarations.AsSyntaxList().Value.Children;
                        }
                        declarationList.AsVariableDeclarationList().Declarations.Nodes = appendꓸꓸꓸ(declarationList.AsVariableDeclarationList().Declarations.Nodes, decls);
                        var statement = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, declarationList);
                        Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFCustomPrologue);
                        results = append(results, statement);
                    }
                } else if (parameter.Initializer() != nil) {
                    var name = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().NewGeneratedNameForNode(parameter);
                    var initializer = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(parameter.Initializer());
                    var assignment = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().NewAssignmentExpression(name, initializer);
                    var statement = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(assignment);
                    Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFCustomPrologue);
                    results = append(results, statement);
                }
            } else if (parameter.Initializer() != nil) {
                var name = parameter.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory()));
                name.Loc = parameter.Name().Loc;
                Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(name, printer.EFNoSourceMap);

                var initializer = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(parameter.Initializer());
                Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(initializer, (printer.EmitFlags)(printer.EFNoSourceMap | printer.EFNoComments));

                var assignment = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().NewAssignmentExpression(name, initializer);
                assignment.Loc = parameter.Loc;
                Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(assignment, printer.EFNoComments);

                var block = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(assignment)])), false);
                block.Loc = parameter.Loc;
                Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(block, (printer.EmitFlags)((printer.EmitFlags)((printer.EmitFlags)(printer.EFSingleLine | printer.EFNoTrailingSourceMap) | printer.EFNoTokenSourceMaps) | printer.EFNoComments));

                var typeCheck = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().NewTypeCheck(name.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory())), "undefined");
                var statement = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIfStatement(typeCheck, block, nil);
                statement.Loc = parameter.Loc;
                Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, (printer.EmitFlags)((printer.EmitFlags)((printer.EmitFlags)((printer.EmitFlags)(printer.EFNoTokenSourceMaps | printer.EFNoTrailingSourceMap) | printer.EFCustomPrologue) | printer.EFNoComments) | printer.EFStartOnNewLine));
                results = append(results, statement);
            }
        } else if ((ast.SubtreeFacts)(parameter.SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread) != 0) {
            containsPrecedingObjectRestOrSpread = true;
            var declarations = transformers.FlattenDestructuringBinding(
                Ꮡch.ᏑTransformer,
                parameter, Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().NewGeneratedNameForNode(parameter),
                transformers.FlattenLevelObjectRest, false, true);
            if (declarations != nil) {
                var declarationList = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), ast.NodeFlagsNone);
                var decls = slice<ж<ast.Node>>([declarations]);
                if (declarations.Kind == ast.KindSyntaxList) {
                    decls = declarations.AsSyntaxList().Value.Children;
                }
                declarationList.AsVariableDeclarationList().Declarations.Nodes = appendꓸꓸꓸ(declarationList.AsVariableDeclarationList().Declarations.Nodes, decls);
                var statement = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, declarationList);
                Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFCustomPrologue);
                results = append(results, statement);
            }
        }
    }

    return results;
}

internal static ж<ast.Node> visitCatchClause(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.CatchClause> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.VariableDeclaration != nil && ast.IsBindingPattern(node.VariableDeclaration.Name()) && (ast.SubtreeFacts)(node.VariableDeclaration.Name().SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread) != 0) {
        var name = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().NewGeneratedNameForNode(node.VariableDeclaration.Name());
        var updatedDecl = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(node.VariableDeclaration.AsVariableDeclaration(), node.VariableDeclaration.Name(), nil, nil, name);
        var visitedBindings = transformers.FlattenDestructuringBinding(
            Ꮡch.ᏑTransformer,
            updatedDecl, nil,
            transformers.FlattenLevelObjectRest, false, false);
        var block = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Block);
        if (visitedBindings != nil) {
            slice<ж<ast.Node>> decls = default;
            if (visitedBindings.Kind == ast.KindSyntaxList) {
                decls = visitedBindings.AsSyntaxList().Value.Children;
            } else {
                decls = [visitedBindings];
            }
            var newStatement = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(decls), ast.NodeFlagsNone));
            var statements = slice<ж<ast.Node>>([newStatement]);
            statements = appendꓸꓸꓸ(statements, block.Statements());
            var statementList = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
            statementList.Loc = block.StatementList().Loc;

            block = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(block.AsBlock(), statementList, block.AsBlock().MultiLine);
        }
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCatchClause(
            Ꮡnode,
            Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(node.VariableDeclaration.AsVariableDeclaration(), name, nil, nil, nil),
            block);
    }
    return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitVariableStatement(this ref objectRestSpreadTransformer ch, ж<ast.VariableStatement> node) {
    if (ast.HasSyntacticModifier(node.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport)) {
        var oldInExportedVariableStatement = ch.inExportedVariableStatement;
        ch.inExportedVariableStatement = true;
        var result = ch.Transformer.Visitor().VisitEachChild(node.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        ch.inExportedVariableStatement = oldInExportedVariableStatement;
        return result;
    }
    return ch.Transformer.Visitor().VisitEachChild(node.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitVariableDeclaration(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.VariableDeclaration> node) {
    ref var ch = ref Ꮡch.DerefOrNull();

    if (ch.inExportedVariableStatement) {
        ch.inExportedVariableStatement = false;
        var result = Ꮡch.visitVariableDeclarationWorker(node, true);
        ch.inExportedVariableStatement = true;
        return result;
    }
    return Ꮡch.visitVariableDeclarationWorker(node, false);
}

internal static ж<ast.Node> visitVariableDeclarationWorker(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.VariableDeclaration> Ꮡnode, bool exported) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBindingPattern(node.Name()) && (ast.SubtreeFacts)(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread) != 0) {
        return transformers.FlattenDestructuringBinding(
            Ꮡch.ᏑTransformer,
            Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil,
            transformers.FlattenLevelObjectRest, exported, false);
    }
    return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitForOftatement(this ref objectRestSpreadTransformer ch, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.Initializer.SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread) != 0 || (ast.IsAssignmentPattern(node.Initializer) && ast.ContainsObjectRestOrSpread(node.Initializer))) {
        var initializerWithoutParens = ast.SkipParentheses(node.Initializer);
        if (ast.IsVariableDeclarationList(initializerWithoutParens) || ast.IsAssignmentPattern(initializerWithoutParens)) {
            core.TextRange bodyLocation = default;
            core.TextRange statementsLocation = default;
            var temp = ch.Transformer.Factory().NewTempVariable();
            var res = ch.Transformer.Visitor().VisitNode(ch.Transformer.Factory().CreateForOfBindingStatement(initializerWithoutParens, temp));
            var statements = new slice<ж<ast.Node>>(0, 1);
            if (res != nil) {
                statements = append(statements, res);
            }
            if (ast.IsBlock(node.Statement)) {
                foreach (var (_, statement) in range(node.Statement.Statements())) {
                    var visited = ch.Transformer.Visitor().VisitEachChild(statement);
                    if (visited != nil) {
                        statements = append(statements, visited);
                    }
                }
                bodyLocation = node.Statement.Loc;
                statementsLocation = node.Statement.StatementList().Loc;
            } else if (node.Statement != nil) {
                statements = append(statements, ch.Transformer.Visitor().VisitEachChild(node.Statement));
                bodyLocation = node.Statement.Loc;
                statementsLocation = node.Statement.Loc;
            }

            var list = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(temp, nil, nil, nil)])),
                ast.NodeFlagsLet);
            list.Loc = node.Initializer.Loc;

            var expr = ch.Transformer.Visitor().VisitEachChild(node.Expression);

            var statementsList = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
            statementsList.Loc = statementsLocation;

            var block = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(statementsList, true);
            block.Loc = bodyLocation;

            return ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForInOrOfStatement(
                Ꮡnode,
                node.AwaitModifier,
                list,
                expr,
                block);
        }
    }
    return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitBinaryExpression(this ж<objectRestSpreadTransformer> Ꮡch, ж<ast.BinaryExpression> Ꮡnode, bool expressionResultIsUnused) {
    ref var ch = ref Ꮡch.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsDestructuringAssignment(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && ast.ContainsObjectRestOrSpread(node.Left)) {
        return transformers.FlattenDestructuringAssignment(
            Ꮡch.ᏑTransformer,
            Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), !expressionResultIsUnused,
            transformers.FlattenLevelObjectRest, default);
    }
    if (node.OperatorToken.Kind == ast.KindCommaToken) {
        ch.expressionResultIsUnused = true;
        var left = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Left);
        ch.expressionResultIsUnused = expressionResultIsUnused;
        var right = Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitNode(node.Right);
        return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(Ꮡnode, nil, left, nil, node.OperatorToken, right);
    }
    return Ꮡch.of(objectRestSpreadTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitObjectLiteralExpression(this ref objectRestSpreadTransformer ch, ж<ast.ObjectLiteralExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (((ast.SubtreeFacts)(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread)) == 0) {
        return ch.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var objects = ch.chunkObjectLiteralElements(node.Properties);
    if (len(objects) > 0 && objects[0].Kind != ast.KindObjectLiteralExpression) {
        objects = appendꓸꓸꓸ(slice<ж<ast.Node>>([ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false)]), objects);
    }
    var expression = objects[0];
    if (len(objects) > 1) {
        foreach (var (i, obj) in range(objects)) {
            if (i == 0) {
                continue;
            }
            expression = ch.Transformer.Factory().NewAssignHelper([expression, obj], ch.compilerOptions.GetEmitScriptTarget());
        }
        return expression;
    }
    return ch.Transformer.Factory().NewAssignHelper(objects, ch.compilerOptions.GetEmitScriptTarget());
}

internal static slice<ж<ast.Node>> chunkObjectLiteralElements(this ref objectRestSpreadTransformer ch, ж<ast.NodeList> Ꮡlist) {
    ref var list = ref Ꮡlist.DerefOrNull();

    if (Ꮡlist == nil || len(list.Nodes) == 0) {
        return default;
    }
    var elements = list.Nodes;
    slice<ж<ast.Node>> chunkObject = default;
    var objects = new slice<ж<ast.Node>>(0, 1);
    foreach (var (_, e) in range(elements)) {
        if (e.Kind == ast.KindSpreadAssignment) {
            if (len(chunkObject) > 0) {
                objects = append(objects, ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(chunkObject), false));
                chunkObject = default;
            }
            var target = e.Expression();
            objects = append(objects, ch.Transformer.Visitor().VisitNode(target));
        } else {
            ж<ast.Node> elem = default;
            if (e.Kind == ast.KindPropertyAssignment) {
                elem = ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, e.Name(), nil, nil, ch.Transformer.Visitor().VisitNode(e.Initializer()));
            } else {
                elem = ch.Transformer.Visitor().VisitNode(e);
            }
            chunkObject = append(chunkObject, elem);
        }
    }
    if (len(chunkObject) > 0) {
        objects = append(objects, ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(ch.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(chunkObject), false));
    }
    return objects;
}

} partial class estransforms_package {

internal static ж<transformers.Transformer> newObjectRestSpreadTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new objectRestSpreadTransformer(compilerOptions: opts.CompilerOptions));
    return tx.of(objectRestSpreadTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} // end estransforms_package
