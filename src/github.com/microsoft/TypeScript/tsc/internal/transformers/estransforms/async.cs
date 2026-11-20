namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static estransforms_package;

partial class estransforms_package {

partial struct asyncContextFlags /*num:nint*/;

internal static asyncContextFlags asyncContextNonTopLevel => /* 1 << iota */ 1;
internal static asyncContextFlags asyncContextHasLexicalThis => 2;

partial struct lexicalArgumentsInfo {
    internal ж<astꓸIdentifierNode> binding;
    internal bool used;
}

partial struct asyncTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    /*embed*/ internal superAccessState superAccessState;
    internal asyncContextFlags contextFlags;
    internal ж<collections.Set<@string>> enclosingFunctionParameterNames;
    internal lexicalArgumentsInfo lexicalArguments;
    internal ж<ast.NodeVisitor> asyncBodyVisitor;
    internal ж<ast.NodeVisitor> fallbackNodeVisitor;
}

internal static ж<transformers.Transformer> newAsyncTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new asyncTransformer());
    var result = tx.of(asyncTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
    tx.of(asyncTransformer.ᏑsuperAccessState).initSuperAccessVisitor(tx.of(asyncTransformer.ᏑTransformer).EmitContext(), tx.of(asyncTransformer.ᏑTransformer).Factory());
    tx.asyncBodyVisitor = tx.of(asyncTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitAsyncBodyNode);
    tx.fallbackNodeVisitor = tx.of(asyncTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitFallback);
    return result;
}

} partial class estransformsᴛasyncTransformer {

internal static ж<ast.Node> visitSourceFile(this ref asyncTransformer tx, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsDeclarationFile) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    tx.setContextFlag(asyncContextNonTopLevel, false);
    tx.setContextFlag(asyncContextHasLexicalThis, false);
    var visited = tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AddEmitHelper(visited, tx.Transformer.EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    return visited;
}

internal static void setContextFlag(this ref asyncTransformer tx, asyncContextFlags flag, bool val) {
    if (val) {
        tx.contextFlags |= (asyncContextFlags)(flag);
    } else {
        tx.contextFlags &= ~(asyncContextFlags)(flag);
    }
}

internal static bool inContext(this ref asyncTransformer tx, asyncContextFlags flags) {
    return (asyncContextFlags)(tx.contextFlags & flags) != 0;
}

internal static bool inTopLevelContext(this ref asyncTransformer tx) {
    return !tx.inContext(asyncContextNonTopLevel);
}

internal static bool inHasLexicalThisContext(this ref asyncTransformer tx) {
    return tx.inContext(asyncContextHasLexicalThis);
}

internal static ж<ast.Node> doWithContext(this ж<asyncTransformer> Ꮡtx, asyncContextFlags flags, Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>> cb, ж<ast.Node> node) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    asyncContextFlags flagsToSet = (asyncContextFlags)(flags & ~tx.contextFlags);
    if (flagsToSet != 0) {
        tx.setContextFlag(flagsToSet, true);
        var result = cb(Ꮡtx, node);
        tx.setContextFlag(flagsToSet, false);
        return result;
    }
    return cb(Ꮡtx, node);
}

internal static ж<ast.Node> visitDefault(this ref asyncTransformer tx, ж<ast.Node> node) {
    return tx.Transformer.Visitor().VisitEachChild(node);
}

internal static ж<ast.Node> fallbackVisitor(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.capturedSuperProperties == nil && tx.lexicalArguments.binding == nil) {
        return Ꮡnode;
    }
    tx.superAccessState.trackSuperAccess(Ꮡnode);
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindConstructor: {
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindParameter || ᴛ == ast.KindBindingElement || ᴛ == ast.KindVariableDeclaration: {
        break;
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        if (tx.lexicalArguments.binding != nil && node.Text() == "arguments"u8 && !ast.IsIdentifierName(Ꮡnode) && !ast.IsLabelName(Ꮡnode)) {
            tx.lexicalArguments.used = true;
            return tx.lexicalArguments.binding;
        }
        break;
    }}

    return tx.fallbackNodeVisitor.VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitFallback(this ref asyncTransformer tx, ж<ast.Node> node) {
    return tx.fallbackVisitor(node);
}

internal static ж<ast.Node> visit(this ж<asyncTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if ((printer.EmitFlags)(Ꮡtx.of(asyncTransformer.ᏑTransformer).EmitContext().EmitFlags(Ꮡnode) & printer.EFNoLexicalThis) != 0 && tx.inHasLexicalThisContext()) {
            tx.setContextFlag(asyncContextHasLexicalThis, false);
            defer(Ꮡtx.setContextFlag, asyncContextHasLexicalThis, (bool)true, ref ᒐ);
        }

        if ((ast.SubtreeFacts)(node.SubtreeFacts() & ((ast.SubtreeFacts)(ast.SubtreeContainsAnyAwait | ast.SubtreeContainsAwait))) == 0) {
            return tx.fallbackVisitor(Ꮡnode);
        }
        Ꮡtx.of(asyncTransformer.ᏑsuperAccessState).trackSuperAccess(Ꮡnode);
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindAsyncKeyword: {
            return default;
        }
        case var ᴛ when ᴛ == ast.KindSourceFile: {
            return tx.visitSourceFile(node.AsSourceFile());
        }
        case var ᴛ when ᴛ == ast.KindAwaitExpression: {
            return tx.visitAwaitExpression(node.AsAwaitExpression());
        }
        case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
            return Ꮡtx.doWithContext((asyncContextFlags)(asyncContextNonTopLevel | asyncContextHasLexicalThis), ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitMethodDeclaration)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
            return Ꮡtx.doWithContext((asyncContextFlags)(asyncContextNonTopLevel | asyncContextHasLexicalThis), ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitFunctionDeclaration)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindFunctionExpression: {
            return Ꮡtx.doWithContext((asyncContextFlags)(asyncContextNonTopLevel | asyncContextHasLexicalThis), ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitFunctionExpression)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindArrowFunction: {
            return Ꮡtx.doWithContext(asyncContextNonTopLevel, ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitArrowFunction)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindGetAccessor: {
            return Ꮡtx.doWithContext((asyncContextFlags)(asyncContextNonTopLevel | asyncContextHasLexicalThis), ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitGetAccessorDeclaration)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindSetAccessor: {
            return Ꮡtx.doWithContext((asyncContextFlags)(asyncContextNonTopLevel | asyncContextHasLexicalThis), ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitSetAccessorDeclaration)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindConstructor: {
            return Ꮡtx.doWithContext((asyncContextFlags)(asyncContextNonTopLevel | asyncContextHasLexicalThis), ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitConstructorDeclaration)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression: {
            return Ꮡtx.doWithContext((asyncContextFlags)(asyncContextNonTopLevel | asyncContextHasLexicalThis), ((Func<ж<asyncTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛasyncTransformer.visitDefault)), Ꮡnode);
        }
        default: {
            return Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }}

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitAsyncBodyNode(this ж<asyncTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNodeWithPossibleHoistedDeclaration(ref (Ꮡnode).DerefOrNull())) {
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindVariableStatement: {
            return Ꮡtx.visitVariableStatementInAsyncBody(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindForStatement: {
            return Ꮡtx.visitForStatementInAsyncBody(node.AsForStatement());
        }
        case var ᴛ when ᴛ == ast.KindForInStatement: {
            return Ꮡtx.visitForInStatementInAsyncBody(node.AsForInOrOfStatement());
        }
        case var ᴛ when ᴛ == ast.KindForOfStatement: {
            return Ꮡtx.visitForOfStatementInAsyncBody(node.AsForInOrOfStatement());
        }
        case var ᴛ when ᴛ == ast.KindCatchClause: {
            return tx.visitCatchClauseInAsyncBody(node.AsCatchClause());
        }
        case var ᴛ when ᴛ == ast.KindBlock || ᴛ == ast.KindSwitchStatement || ᴛ == ast.KindCaseBlock || ᴛ == ast.KindCaseClause || ᴛ == ast.KindDefaultClause || ᴛ == ast.KindTryStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindIfStatement || ᴛ == ast.KindWithStatement || ᴛ == ast.KindLabeledStatement: {
            return tx.asyncBodyVisitor.VisitEachChild(Ꮡnode);
        }}

    }
    return Ꮡtx.visit(Ꮡnode);
}

internal static ж<ast.Node> visitCatchClauseInAsyncBody(this ref asyncTransformer tx, ж<ast.CatchClause> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var catchClauseNames = Ꮡ(new collections.Set<@string>());
    if (node.VariableDeclaration != nil) {
        tx.recordDeclarationName(node.VariableDeclaration, catchClauseNames);
    }

    ж<collections.Set<@string>> catchClauseUnshadowedNames = default;
    foreach (var (escapedName, _) in catchClauseNames.Keys()) {
        if (tx.enclosingFunctionParameterNames != nil && tx.enclosingFunctionParameterNames.Has(escapedName)) {
            if (catchClauseUnshadowedNames == nil) {
                catchClauseUnshadowedNames = tx.enclosingFunctionParameterNames.Clone();
            }
            catchClauseUnshadowedNames.Delete(escapedName);
        }
    }

    if (catchClauseUnshadowedNames != nil) {
        var savedEnclosingFunctionParameterNames = tx.enclosingFunctionParameterNames;
        tx.enclosingFunctionParameterNames = catchClauseUnshadowedNames;
        var result = tx.asyncBodyVisitor.VisitEachChild(Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.enclosingFunctionParameterNames = savedEnclosingFunctionParameterNames;
        return result;
    }
    return tx.asyncBodyVisitor.VisitEachChild(Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitVariableStatementInAsyncBody(this ж<asyncTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var declList = node.AsVariableStatement().DeclarationList;
    if (Ꮡtx.isVariableDeclarationListWithCollidingName(declList)) {
        var expression = tx.visitVariableDeclarationListWithCollidingNames(declList.AsVariableDeclarationList(), false);
        if (expression != nil) {
            return Ꮡtx.of(asyncTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
        }
        return default;
    }
    return Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitForInStatementInAsyncBody(this ж<asyncTransformer> Ꮡtx, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> visitedInitializer = default;
    if (Ꮡtx.isVariableDeclarationListWithCollidingName(node.Initializer)) {
        visitedInitializer = tx.visitVariableDeclarationListWithCollidingNames(node.Initializer.AsVariableDeclarationList(), true);
    } else {
        visitedInitializer = Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.Initializer);
    }

    return Ꮡtx.of(asyncTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForInOrOfStatement(
        Ꮡnode,
        nil,
        visitedInitializer,
        Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.Expression),
        tx.asyncBodyVisitor.VisitEmbeddedStatement(node.Statement));
}

internal static ж<ast.Node> visitForOfStatementInAsyncBody(this ж<asyncTransformer> Ꮡtx, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> visitedInitializer = default;
    if (Ꮡtx.isVariableDeclarationListWithCollidingName(node.Initializer)) {
        visitedInitializer = tx.visitVariableDeclarationListWithCollidingNames(node.Initializer.AsVariableDeclarationList(), true);
    } else {
        visitedInitializer = Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.Initializer);
    }

    return Ꮡtx.of(asyncTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForInOrOfStatement(
        Ꮡnode,
        Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.AwaitModifier),
        visitedInitializer,
        Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.Expression),
        tx.asyncBodyVisitor.VisitEmbeddedStatement(node.Statement));
}

internal static ж<ast.Node> visitForStatementInAsyncBody(this ж<asyncTransformer> Ꮡtx, ж<ast.ForStatement> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var initializer = node.Initializer;
    ж<ast.Node> visitedInitializer = default;
    if (initializer != nil && Ꮡtx.isVariableDeclarationListWithCollidingName(initializer)) {
        visitedInitializer = tx.visitVariableDeclarationListWithCollidingNames(initializer.AsVariableDeclarationList(), false);
    } else {
        visitedInitializer = Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.Initializer);
    }

    return Ꮡtx.of(asyncTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForStatement(
        Ꮡnode,
        visitedInitializer,
        Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.Condition),
        Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitNode(node.Incrementor),
        tx.asyncBodyVisitor.VisitEmbeddedStatement(node.Statement));
}

internal static ж<ast.Node> visitAwaitExpression(this ref asyncTransformer tx, ж<ast.AwaitExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.inTopLevelContext()) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    var yieldExpr = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewYieldExpression(
        nil,
        tx.Transformer.Visitor().VisitNode(node.Expression));
    yieldExpr.Loc = node.Loc;
    tx.Transformer.EmitContext().SetOriginal(yieldExpr, Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return yieldExpr;
}

internal static ж<ast.Node> visitConstructorDeclaration(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsConstructorDeclaration();
    var savedLexicalArguments = tx.lexicalArguments;
    tx.lexicalArguments = new lexicalArgumentsInfo();
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(
        decl,
        tx.Transformer.Visitor().VisitModifiers(decl.of(ast.ConstructorDeclaration.ᏑModifiersBase).Modifiers()),
        nil,
        tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor()),
        nil,
        nil,
        tx.transformMethodBody(Ꮡnode));
    tx.lexicalArguments = savedLexicalArguments;
    return updated;
}

internal static ж<ast.Node> visitMethodDeclaration(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsMethodDeclaration();
    var functionFlags = ast.GetFunctionFlags(Ꮡnode);
    var savedLexicalArguments = tx.lexicalArguments;
    tx.lexicalArguments = new lexicalArgumentsInfo();

    ж<ast.NodeList> parameters = default;
    ж<ast.Node> body = default;
    if ((ast.FunctionFlags)(functionFlags & ast.FunctionFlagsAsync) != 0) {
        parameters = tx.transformAsyncFunctionParameterList(Ꮡnode);
        body = tx.transformAsyncFunctionBody(Ꮡnode, parameters);
    } else {
        parameters = tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor());
        body = tx.transformMethodBody(Ꮡnode);
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(
        decl,
        tx.Transformer.Visitor().VisitModifiers(decl.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers()),
        decl.AsteriskToken,
        decl.Name(),
        nil,
        nil,
        parameters,
        nil,
        nil,
        body);
    tx.lexicalArguments = savedLexicalArguments;
    return updated;
}

internal static ж<ast.Node> visitGetAccessorDeclaration(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsGetAccessorDeclaration();
    var savedLexicalArguments = tx.lexicalArguments;
    tx.lexicalArguments = new lexicalArgumentsInfo();
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(
        decl,
        tx.Transformer.Visitor().VisitModifiers(decl.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()),
        decl.Name(),
        nil,
        tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor()),
        nil,
        nil,
        tx.transformMethodBody(Ꮡnode));
    tx.lexicalArguments = savedLexicalArguments;
    return updated;
}

internal static ж<ast.Node> visitSetAccessorDeclaration(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsSetAccessorDeclaration();
    var savedLexicalArguments = tx.lexicalArguments;
    tx.lexicalArguments = new lexicalArgumentsInfo();
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(
        decl,
        tx.Transformer.Visitor().VisitModifiers(decl.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()),
        decl.Name(),
        nil,
        tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor()),
        nil,
        nil,
        tx.transformMethodBody(Ꮡnode));
    tx.lexicalArguments = savedLexicalArguments;
    return updated;
}

internal static ж<ast.Node> visitFunctionDeclaration(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsFunctionDeclaration();
    var functionFlags = ast.GetFunctionFlags(Ꮡnode);
    var savedLexicalArguments = tx.lexicalArguments;
    tx.lexicalArguments = new lexicalArgumentsInfo();

    ж<ast.NodeList> parameters = default;
    ж<ast.Node> body = default;
    if ((ast.FunctionFlags)(functionFlags & ast.FunctionFlagsAsync) != 0) {
        parameters = tx.transformAsyncFunctionParameterList(Ꮡnode);
        body = tx.transformAsyncFunctionBody(Ꮡnode, parameters);
    } else {
        parameters = tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor());
        body = tx.Transformer.EmitContext().VisitFunctionBody((~decl).Body, tx.Transformer.Visitor());
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(
        decl,
        tx.Transformer.Visitor().VisitModifiers(decl.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers()),
        decl.AsteriskToken,
        tx.Transformer.Visitor().VisitNode(decl.Name()),
        nil,
        parameters,
        nil,
        nil,
        body);
    tx.lexicalArguments = savedLexicalArguments;
    return updated;
}

internal static ж<ast.Node> visitFunctionExpression(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsFunctionExpression();
    var functionFlags = ast.GetFunctionFlags(Ꮡnode);
    var savedLexicalArguments = tx.lexicalArguments;
    tx.lexicalArguments = new lexicalArgumentsInfo();

    ж<ast.NodeList> parameters = default;
    ж<ast.Node> body = default;
    if ((ast.FunctionFlags)(functionFlags & ast.FunctionFlagsAsync) != 0) {
        parameters = tx.transformAsyncFunctionParameterList(Ꮡnode);
        body = tx.transformAsyncFunctionBody(Ꮡnode, parameters);
    } else {
        parameters = tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor());
        body = tx.Transformer.EmitContext().VisitFunctionBody((~decl).Body, tx.Transformer.Visitor());
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionExpression(
        decl,
        tx.Transformer.Visitor().VisitModifiers(decl.of(ast.FunctionExpression.ᏑModifiersBase).Modifiers()),
        decl.AsteriskToken,
        tx.Transformer.Visitor().VisitNode(decl.Name()),
        nil,
        parameters,
        nil,
        nil,
        body);
    tx.lexicalArguments = savedLexicalArguments;
    return updated;
}

internal static ж<ast.Node> visitArrowFunction(this ж<asyncTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if ((printer.EmitFlags)(Ꮡtx.of(asyncTransformer.ᏑTransformer).EmitContext().EmitFlags(Ꮡnode) & printer.EFNoLexicalArguments) != 0) {
            ref var savedLexicalArguments = ref heap<lexicalArgumentsInfo>(tx.lexicalArguments, out var ᏑsavedLexicalArguments);
            tx.lexicalArguments = new lexicalArgumentsInfo();
            var savedLexicalArgumentsʗ1 = savedLexicalArguments;
            defer(() => {
                Ꮡtx.Value.lexicalArguments = savedLexicalArgumentsʗ1;
            }, ref ᒐ);
        }

        var decl = node.AsArrowFunction();
        var functionFlags = ast.GetFunctionFlags(Ꮡnode);

        ж<ast.NodeList> parameters = default;
        ж<ast.Node> body = default;
        if ((ast.FunctionFlags)(functionFlags & ast.FunctionFlagsAsync) != 0) {
            parameters = tx.transformAsyncFunctionParameterList(Ꮡnode);
            body = tx.transformAsyncFunctionBody(Ꮡnode, parameters);
        } else {
            parameters = Ꮡtx.of(asyncTransformer.ᏑTransformer).EmitContext().VisitParameters((~decl).Parameters, Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor());
            body = Ꮡtx.of(asyncTransformer.ᏑTransformer).EmitContext().VisitFunctionBody((~decl).Body, Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor());
        }

        return Ꮡtx.of(asyncTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateArrowFunction(
            decl,
            Ꮡtx.of(asyncTransformer.ᏑTransformer).Visitor().VisitModifiers(decl.of(ast.ArrowFunction.ᏑModifiersBase).Modifiers()),
            nil,
            parameters,
            nil,
            nil,
            decl.EqualsGreaterThanToken,
            body);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static void recordDeclarationName(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode, ж<collections.Set<@string>> Ꮡnames) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var names = ref Ꮡnames.DerefOrNull();

    var name = node.Name();
    if (name == nil) {
        return;
    }
    if (ast.IsIdentifier(name)) {
        names.Add(name.Text());
    } else if (ast.IsBindingPattern(name)) {
        foreach (var (_, element) in range((~name.AsBindingPattern()).Elements.Nodes)) {
            if (!ast.IsOmittedExpression(element)) {
                tx.recordDeclarationName(element, Ꮡnames);
            }
        }
    }
}

internal static bool isVariableDeclarationListWithCollidingName(this ж<asyncTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && ast.IsVariableDeclarationList(Ꮡnode) && (ast.NodeFlags)(node.Flags & ast.NodeFlagsBlockScoped) == 0 && slices.ContainsFunc<slice<ж<ast.Node>>, ж<ast.Node>>(node.AsVariableDeclarationList().Declarations.Nodes, Ꮡtx.collidesWithParameterName);
}

internal static ж<ast.Node> visitVariableDeclarationListWithCollidingNames(this ref asyncTransformer tx, ж<ast.VariableDeclarationList> Ꮡnode, bool hasReceiver) {
    ref var node = ref Ꮡnode.DerefOrNull();
    var variablesᴷ = new stackAppend<ж<ast.Node>>(moves: true);
    var expressionsᴷ = new stackAppend<ж<ast.Node>>();

    tx.hoistVariableDeclarationList(Ꮡnode);

    slice<ж<ast.Node>> variables = default;
    foreach (var (_, decl) in range(node.Declarations.Nodes)) {
        if ((~decl.AsVariableDeclaration()).Initializer != nil) {
            variables = append(variablesᴷ, variables, decl);
        }
    }

    if (len(variables) == 0) {
        if (hasReceiver) {
            var name = node.Declarations.Nodes[0].Name();
            ж<ast.Node> target = default;
            if (ast.IsBindingPattern(name)) {
                target = transformers.ConvertBindingPatternToAssignmentPattern(tx.Transformer.EmitContext(), name.AsBindingPattern());
            } else {
                target = name;
            }
            return tx.Transformer.Visitor().VisitNode(target);
        }
        return default;
    }

    slice<ж<ast.Node>> expressions = default;
    variables = move2heap(variablesᴷ, variables);
    foreach (var (_, variable) in range(variables)) {
        expressions = append(expressionsᴷ, expressions, tx.transformInitializedVariable(variable.AsVariableDeclaration()));
    }
    return tx.Transformer.Factory().InlineExpressions(expressions);
}

internal static void hoistVariableDeclarationList(this ref asyncTransformer tx, ж<ast.VariableDeclarationList> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    foreach (var (_, decl) in range(node.Declarations.Nodes)) {
        tx.hoistVariable(decl);
    }
}

internal static void hoistVariable(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    if (name == nil) {
        return;
    }
    if (ast.IsIdentifier(name)) {
        tx.Transformer.EmitContext().AddVariableDeclaration(name);
    } else if (ast.IsBindingPattern(name)) {
        foreach (var (_, element) in range((~name.AsBindingPattern()).Elements.Nodes)) {
            if (!ast.IsOmittedExpression(element)) {
                tx.hoistVariable(element);
            }
        }
    }
}

internal static ж<ast.Node> transformInitializedVariable(this ref asyncTransformer tx, ж<ast.VariableDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> target = default;
    if (ast.IsBindingPattern(node.Name())) {
        target = transformers.ConvertBindingPatternToAssignmentPattern(tx.Transformer.EmitContext(), node.Name().AsBindingPattern());
    } else {
        target = node.Name();
    }
    var converted = tx.Transformer.Factory().NewAssignmentExpression(target, node.Initializer);
    tx.Transformer.EmitContext().SetSourceMapRange(converted, node.Loc);
    return tx.Transformer.Visitor().VisitNode(converted);
}

internal static bool collidesWithParameterName(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    if (name == nil) {
        return false;
    }
    if (ast.IsIdentifier(name)) {
        return tx.enclosingFunctionParameterNames != nil && tx.enclosingFunctionParameterNames.Has(name.Text());
    }
    if (ast.IsBindingPattern(name)) {
        foreach (var (_, element) in range((~name.AsBindingPattern()).Elements.Nodes)) {
            if (!ast.IsOmittedExpression(element) && tx.collidesWithParameterName(element)) {
                return true;
            }
        }
    }
    return false;
}

internal static ж<ast.Node> transformMethodBody(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var savedCapturedSuperProperties = tx.capturedSuperProperties;
    var savedHasSuperElementAccess = tx.hasSuperElementAccess;
    var savedHasSuperPropertyAssignment = tx.hasSuperPropertyAssignment;
    var savedSuperBinding = tx.superBinding;
    var savedSuperIndexBinding = tx.superIndexBinding;
    tx.capturedSuperProperties = Ꮡ(new collections.OrderedSet<@string>());
    tx.hasSuperElementAccess = false;
    tx.hasSuperPropertyAssignment = false;
    tx.superBinding = tx.Transformer.Factory().NewUniqueNameEx("_super", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));
    tx.superIndexBinding = tx.Transformer.Factory().NewUniqueNameEx("_superIndex", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));

    tx.Transformer.EmitContext().StartVariableEnvironment();
    var updated = tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor());

    var emitSuperHelpers = (tx.capturedSuperProperties.Size() > 0 || tx.hasSuperElementAccess) && ((ast.FunctionFlags)(ast.GetFunctionFlags(tx.getOriginalIfFunctionLike(Ꮡnode)) & ast.FunctionFlagsAsyncGenerator)) != ast.FunctionFlagsAsyncGenerator;

    if (emitSuperHelpers) {
        if (tx.capturedSuperProperties.Size() > 0) {
            tx.Transformer.EmitContext().AddInitializationStatement(tx.superAccessState.createSuperAccessVariableStatement());
        }
    }

    var mergedStatements = tx.Transformer.EmitContext().EndAndMergeVariableEnvironmentList(updated.StatementList());
    if (emitSuperHelpers && tx.hasSuperElementAccess && !updated.AsBlock().MultiLine) {
        var newBlock = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(mergedStatements, true);
        newBlock.Loc = updated.Loc;
        updated = newBlock;
    } else {
        updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(updated.AsBlock(), mergedStatements, updated.AsBlock().MultiLine);
    }

    if (emitSuperHelpers && tx.hasSuperElementAccess) {
        if (tx.hasSuperPropertyAssignment) {
            tx.Transformer.EmitContext().AddEmitHelper(updated, printer.AdvancedAsyncSuperHelper);
        } else {
            tx.Transformer.EmitContext().AddEmitHelper(updated, printer.AsyncSuperHelper);
        }
    }

    tx.capturedSuperProperties = savedCapturedSuperProperties;
    tx.hasSuperElementAccess = savedHasSuperElementAccess;
    tx.hasSuperPropertyAssignment = savedHasSuperPropertyAssignment;
    tx.superBinding = savedSuperBinding;
    tx.superIndexBinding = savedSuperIndexBinding;
    return updated;
}

internal static ж<ast.Node> createCaptureArgumentsStatement(this ref asyncTransformer tx) {
    var variable = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
        tx.lexicalArguments.binding,
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("arguments"));
    var declList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([variable]), ast.NodeFlagsNone);
    var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, declList);
    tx.Transformer.EmitContext().AddEmitFlags(statement, (printer.EmitFlags)(printer.EFStartOnNewLine | printer.EFCustomPrologue));
    return statement;
}

internal static ж<ast.NodeList> transformAsyncFunctionParameterList(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isSimpleParameterList(node.Parameters())) {
        return tx.Transformer.EmitContext().VisitParameters(node.ParameterList(), tx.Transformer.Visitor());
    }

    slice<ж<ast.Node>> newParameters = default;
    foreach (var (_, parameter) in range(node.Parameters())) {
        var param = parameter.AsParameterDeclaration();
        if ((~param).Initializer != nil || param.DotDotDotToken != nil) {
            if (node.Kind == ast.KindArrowFunction) {
                var restParameter = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(
                    nil,
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindDotDotDotToken),
                    tx.Transformer.Factory().NewUniqueNameEx("args", new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes)),
                    nil,
                    nil,
                    nil);
                newParameters = append(newParameters, restParameter);
            }
            break;
        }
        var newParameter = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(
            nil,
            nil,
            tx.Transformer.Factory().NewGeneratedNameForNodeEx(param.Name(), new printer.AutoGenerateOptions(Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes)),
            nil,
            nil,
            nil);
        newParameters = append(newParameters, newParameter);
    }
    var newParametersArray = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newParameters);
    newParametersArray.Loc = node.ParameterList().Loc;
    return newParametersArray;
}

internal static ж<ast.Node> transformAsyncFunctionBody(this ref asyncTransformer tx, ж<ast.Node> Ꮡnode, ж<ast.NodeList> ᏑouterParameters) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var outerParameters = ref ᏑouterParameters.DerefOrNull();

    var isArrow = node.Kind == ast.KindArrowFunction;
    var savedCapturedSuperProperties = tx.capturedSuperProperties;
    var savedHasSuperElementAccess = tx.hasSuperElementAccess;
    var savedHasSuperPropertyAssignment = tx.hasSuperPropertyAssignment;
    var savedSuperBinding = tx.superBinding;
    var savedSuperIndexBinding = tx.superIndexBinding;
    if (!isArrow) {
        tx.capturedSuperProperties = Ꮡ(new collections.OrderedSet<@string>());
        tx.hasSuperElementAccess = false;
        tx.hasSuperPropertyAssignment = false;
        tx.superBinding = tx.Transformer.Factory().NewUniqueNameEx("_super", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));
        tx.superIndexBinding = tx.Transformer.Factory().NewUniqueNameEx("_superIndex", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));
    }

    var innerParameters = (ж<ast.NodeList>)nil;
    if (!isSimpleParameterList(node.Parameters())) {
        innerParameters = tx.Transformer.EmitContext().VisitParameters(node.ParameterList(), tx.Transformer.Visitor());
    }

    var savedLexicalArguments = tx.lexicalArguments;
    var captureLexicalArguments = tx.lexicalArguments.binding == nil;
    if (captureLexicalArguments) {
        tx.lexicalArguments = new lexicalArgumentsInfo(
            binding: tx.Transformer.Factory().NewUniqueName("arguments")
        );
    }

    ж<astꓸExpression> argumentsExpression = default;
    if (innerParameters != nil) {
        if (isArrow) {
            slice<ж<ast.Node>> parameterBindings = default;
            nint outerLen = len(outerParameters.Nodes);
            foreach (var (i, param) in range(node.Parameters())) {
                if (i >= outerLen) {
                    break;
                }
                var originalParameter = param.AsParameterDeclaration();
                var outerParameter = outerParameters.Nodes[i].AsParameterDeclaration();
                if ((~originalParameter).Initializer != nil || originalParameter.DotDotDotToken != nil) {
                    parameterBindings = append(parameterBindings, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSpreadElement(outerParameter.Name()));
                    break;
                }
                parameterBindings = append(parameterBindings, outerParameter.Name());
            }
            argumentsExpression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(parameterBindings), false);
        } else {
            argumentsExpression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("arguments");
        }
    }

    var savedEnclosingFunctionParameterNames = tx.enclosingFunctionParameterNames;
    tx.enclosingFunctionParameterNames = Ꮡ(new collections.Set<@string>());
    foreach (var (_, parameter) in range(node.Parameters())) {
        tx.recordDeclarationName(parameter, tx.enclosingFunctionParameterNames);
    }

    var hasLexicalThis = tx.inHasLexicalThisContext();

    var asyncBody = tx.transformAsyncFunctionBodyWorker(node.Body());
    asyncBody = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(
        asyncBody.AsBlock(),
        tx.Transformer.EmitContext().EndAndMergeVariableEnvironmentList(asyncBody.StatementList()),
        asyncBody.AsBlock().MultiLine);

    var emitSuperHelpers = tx.capturedSuperProperties != nil && (tx.capturedSuperProperties.Size() > 0 || tx.hasSuperElementAccess);
    if (emitSuperHelpers) {
        innerParameters = tx.superAccessVisitor.VisitNodes(innerParameters);
        asyncBody = tx.superAccessState.substituteSuperAccessesInBody(asyncBody);
    }

    ж<ast.Node> result = default;
    if (!isArrow) {
        tx.Transformer.EmitContext().StartVariableEnvironment();

        if (emitSuperHelpers) {
            if (tx.capturedSuperProperties.Size() > 0) {
                tx.Transformer.EmitContext().AddInitializationStatement(tx.superAccessState.createSuperAccessVariableStatement());
            }
        }

        if (captureLexicalArguments && tx.lexicalArguments.used) {
            tx.Transformer.EmitContext().AddInitializationStatement(tx.createCaptureArgumentsStatement());
        }

        var statements = slice<ж<ast.Node>>([
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(
                tx.Transformer.Factory().NewAwaiterHelper(
                    hasLexicalThis,
                    argumentsExpression,
                    innerParameters,
                    asyncBody))
        ]);

        var block = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(
            tx.Transformer.EmitContext().EndAndMergeVariableEnvironmentList(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements)),
            true);
        block.Loc = node.Body().Loc;

        if (emitSuperHelpers && tx.hasSuperElementAccess) {
            if (tx.hasSuperPropertyAssignment) {
                tx.Transformer.EmitContext().AddEmitHelper(block, printer.AdvancedAsyncSuperHelper);
            } else {
                tx.Transformer.EmitContext().AddEmitHelper(block, printer.AsyncSuperHelper);
            }
        }

        result = block;
    } else {
        result = tx.Transformer.Factory().NewAwaiterHelper(
            hasLexicalThis,
            argumentsExpression,
            innerParameters,
            asyncBody);

        if (captureLexicalArguments && tx.lexicalArguments.used) {
            var block = tx.Transformer.EmitContext().ConvertToFunctionBlock(result, true);
            if (!ast.IsBlock(result)) {
                tx.Transformer.EmitContext().SetOriginal(block.StatementList().Nodes[0], result);
            }
            result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(
                block.AsBlock(),
                tx.Transformer.EmitContext().MergeEnvironmentList(block.StatementList(), slice<ж<ast.Node>>([tx.createCaptureArgumentsStatement()])),
                block.AsBlock().MultiLine);
        }
    }

    tx.enclosingFunctionParameterNames = savedEnclosingFunctionParameterNames;
    if (!isArrow) {
        tx.capturedSuperProperties = savedCapturedSuperProperties;
        tx.hasSuperElementAccess = savedHasSuperElementAccess;
        tx.hasSuperPropertyAssignment = savedHasSuperPropertyAssignment;
        tx.superBinding = savedSuperBinding;
        tx.superIndexBinding = savedSuperIndexBinding;
        tx.lexicalArguments = savedLexicalArguments;
    } else if (captureLexicalArguments && !tx.lexicalArguments.used) {
        tx.lexicalArguments = savedLexicalArguments;
    } else if (captureLexicalArguments) {
        tx.lexicalArguments.used = false;
    }
    return result;
}

internal static ж<ast.Node> transformAsyncFunctionBodyWorker(this ref asyncTransformer tx, ж<ast.Node> Ꮡbody) {
    ref var body = ref Ꮡbody.DerefOrNull();

    if (ast.IsBlock(Ꮡbody)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(
            body.AsBlock(),
            tx.asyncBodyVisitor.VisitNodes(body.StatementList()),
            body.AsBlock().MultiLine);
    }
    var visited = tx.asyncBodyVisitor.VisitNode(Ꮡbody);
    var ret = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(visited);
    ret.Loc = body.Loc;
    var list = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([ret]);
    list.Loc = body.Loc;
    var block = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(list, false);
    block.Loc = body.Loc;
    return block;
}

} partial class estransforms_package {

internal static bool assignmentTargetContainsSuperProperty(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        return node.Expression().Kind == ast.KindSuperKeyword;
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return assignmentTargetContainsSuperProperty((~node.AsParenthesizedExpression()).Expression);
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        return slices.ContainsFunc<slice<ж<ast.Node>>, ж<ast.Node>>((~node.AsArrayLiteralExpression()).Elements.Nodes, assignmentTargetContainsSuperProperty);
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        foreach (var (_, prop) in range((~node.AsObjectLiteralExpression()).Properties.Nodes)) {
            switch (prop.Kind) {
            case var ᴛ2 when ᴛ2 == ast.KindPropertyAssignment: {
                if (assignmentTargetContainsSuperProperty((~prop.AsPropertyAssignment()).Initializer)) {
                    return true;
                }
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindShorthandPropertyAssignment: {
                if (assignmentTargetContainsSuperProperty(prop.AsShorthandPropertyAssignment().Name())) {
                    return true;
                }
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindSpreadAssignment: {
                if (assignmentTargetContainsSuperProperty((~prop.AsSpreadAssignment()).Expression)) {
                    return true;
                }
                break;
            }}

        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindSpreadElement: {
        return assignmentTargetContainsSuperProperty((~node.AsSpreadElement()).Expression);
    }}

    return false;
}

internal static bool isUpdateExpression(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPrefixUnaryExpression(Ꮡnode)) {
        var op = node.AsPrefixUnaryExpression().Operator;
        return op == ast.KindPlusPlusToken || op == ast.KindMinusMinusToken;
    }
    if (ast.IsPostfixUnaryExpression(Ꮡnode)) {
        var op = node.AsPostfixUnaryExpression().Operator;
        return op == ast.KindPlusPlusToken || op == ast.KindMinusMinusToken;
    }
    return false;
}

} partial class estransformsᴛasyncTransformer {

internal static ж<ast.Node> getOriginalIfFunctionLike(this ref asyncTransformer tx, ж<ast.Node> node) {
    var original = tx.Transformer.EmitContext().MostOriginal(node);
    if (original != nil && ast.IsFunctionLikeDeclaration(original)) {
        return original;
    }
    return node;
}

} partial class estransforms_package {

internal static bool isSimpleParameterList(slice<ж<ast.Node>> @params) {
    foreach (var (_, param) in range(@params)) {
        var p = param.AsParameterDeclaration();
        if ((~p).Initializer != nil || !ast.IsIdentifier(p.Name())) {
            return false;
        }
    }
    return true;
}

internal static bool isNodeWithPossibleHoistedDeclaration(ref ast.Node node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindBlock || ᴛ == ast.KindVariableStatement || ᴛ == ast.KindWithStatement || ᴛ == ast.KindIfStatement || ᴛ == ast.KindSwitchStatement || ᴛ == ast.KindCaseBlock || ᴛ == ast.KindCaseClause || ᴛ == ast.KindDefaultClause || ᴛ == ast.KindLabeledStatement || ᴛ == ast.KindForStatement || ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindTryStatement || ᴛ == ast.KindCatchClause: {
        return true;
    }}

    return false;
}

} // end estransforms_package
