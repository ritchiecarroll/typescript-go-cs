namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static estransforms_package;

partial class estransforms_package {

partial struct forAwaitHierarchyFacts /*num:nint*/;

internal static forAwaitHierarchyFacts forAwaitHierarchyFactsNone => 0;

internal static forAwaitHierarchyFacts forAwaitHierarchyFactsHasLexicalThis => /* 1 << iota */ 1;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsIterationContainer => 2;
internal static UntypedInt forAwaitHierarchyFactsAncestorFactsMask => /* 1<<iota - 1 */ 3;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsSourceFileExcludes => /* forAwaitHierarchyFactsIterationContainer */ 2;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsStrictModeSourceFileIncludes => /* forAwaitHierarchyFactsNone */ 0;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsClassOrFunctionIncludes => /* forAwaitHierarchyFactsHasLexicalThis */ 1;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsClassOrFunctionExcludes => /* forAwaitHierarchyFactsIterationContainer */ 2;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsArrowFunctionIncludes => /* forAwaitHierarchyFactsNone */ 0;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsArrowFunctionExcludes => /* forAwaitHierarchyFactsClassOrFunctionExcludes */ 2;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsIterationStatementIncludes => /* forAwaitHierarchyFactsIterationContainer */ 2;
internal static forAwaitHierarchyFacts forAwaitHierarchyFactsIterationStatementExcludes => /* forAwaitHierarchyFactsNone */ 0;

partial struct forawaitTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    /*embed*/ internal superAccessState superAccessState;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ast.FunctionFlags enclosingFunctionFlags;
    internal forAwaitHierarchyFacts forAwaitHierarchyFacts;
    internal bool exportedVariableStatement;
    internal ж<ast.NodeVisitor> fallbackNodeVisitor;
    internal ж<ast.NodeVisitor> noAsyncModifierVisitor;
}

internal static ж<transformers.Transformer> newforawaitTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new forawaitTransformer(
        compilerOptions: opts.CompilerOptions
    ));
    var result = tx.of(forawaitTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
    tx.of(forawaitTransformer.ᏑsuperAccessState).initSuperAccessVisitor(tx.of(forawaitTransformer.ᏑTransformer).EmitContext(), tx.of(forawaitTransformer.ᏑTransformer).Factory());
    tx.fallbackNodeVisitor = tx.of(forawaitTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitFallback);
    tx.noAsyncModifierVisitor = tx.of(forawaitTransformer.ᏑTransformer).EmitContext().NewNodeVisitor((ж<ast.Node> node) => {
        if (node.Kind == ast.KindAsyncKeyword) {
            return default;
        }
        return node;
    });
    return result;
}

} partial class estransformsᴛforawaitTransformer {

internal static bool affectsSubtree(this ref forawaitTransformer tx, forAwaitHierarchyFacts excludeFacts, forAwaitHierarchyFacts includeFacts) {
    return tx.forAwaitHierarchyFacts != ((forAwaitHierarchyFacts)((forAwaitHierarchyFacts)(tx.forAwaitHierarchyFacts & ~excludeFacts) | includeFacts));
}

internal static forAwaitHierarchyFacts enterSubtree(this ref forawaitTransformer tx, forAwaitHierarchyFacts excludeFacts, forAwaitHierarchyFacts includeFacts) {
    forAwaitHierarchyFacts ancestorFacts = tx.forAwaitHierarchyFacts;
    tx.forAwaitHierarchyFacts = (forAwaitHierarchyFacts)(((forAwaitHierarchyFacts)((forAwaitHierarchyFacts)(tx.forAwaitHierarchyFacts & ~excludeFacts) | includeFacts)) & (nint)forAwaitHierarchyFactsAncestorFactsMask);
    return ancestorFacts;
}

internal static void exitSubtree(this ref forawaitTransformer tx, forAwaitHierarchyFacts ancestorFacts) {
    tx.forAwaitHierarchyFacts = ancestorFacts;
}

internal static ж<ast.ModifierList> visitModifiersNoAsync(this ref forawaitTransformer tx, ж<ast.ModifierList> modifiers) {
    return tx.noAsyncModifierVisitor.VisitModifiers(modifiers);
}

internal static ж<ast.Node> doWithHierarchyFacts(this ж<forawaitTransformer> Ꮡtx, Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>> cb, ж<ast.Node> node, forAwaitHierarchyFacts excludeFacts, forAwaitHierarchyFacts includeFacts) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (tx.affectsSubtree(excludeFacts, includeFacts)) {
        forAwaitHierarchyFacts ancestorFacts = tx.enterSubtree(excludeFacts, includeFacts);
        var result = cb(Ꮡtx, node);
        tx.exitSubtree(ancestorFacts);
        return result;
    }
    return cb(Ꮡtx, node);
}

internal static ж<ast.Node> visitDefault(this ref forawaitTransformer tx, ж<ast.Node> node) {
    return tx.Transformer.Visitor().VisitEachChild(node);
}

internal static ж<ast.Node> fallbackVisitor(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.capturedSuperProperties == nil) {
        return Ꮡnode;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindConstructor: {
        return Ꮡnode;
    }}

    tx.superAccessState.trackSuperAccess(Ꮡnode);
    return tx.fallbackNodeVisitor.VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitFallback(this ref forawaitTransformer tx, ж<ast.Node> node) {
    return tx.fallbackVisitor(node);
}

internal static ж<ast.Node> visit(this ж<forawaitTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsForAwaitOrAsyncGenerator) == 0) {
        return tx.fallbackVisitor(Ꮡnode);
    }
    Ꮡtx.of(forawaitTransformer.ᏑsuperAccessState).trackSuperAccess(Ꮡnode);
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        return tx.visitSourceFile(node.AsSourceFile());
    }
    case var ᴛ when ᴛ == ast.KindAwaitExpression: {
        return tx.visitAwaitExpression(node.AsAwaitExpression());
    }
    case var ᴛ when ᴛ == ast.KindYieldExpression: {
        return tx.visitYieldExpression(node.AsYieldExpression());
    }
    case var ᴛ when ᴛ == ast.KindReturnStatement: {
        return tx.visitReturnStatement(node.AsReturnStatement());
    }
    case var ᴛ when ᴛ == ast.KindLabeledStatement: {
        return tx.visitLabeledStatement(node.AsLabeledStatement());
    }
    case var ᴛ when ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindForInStatement: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitDefault)),
            Ꮡnode,
            forAwaitHierarchyFactsIterationStatementExcludes,
            forAwaitHierarchyFactsIterationStatementIncludes);
    }
    case var ᴛ when ᴛ == ast.KindForOfStatement: {
        return tx.visitForOfStatement(node.AsForInOrOfStatement(), nil);
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitDefault)),
            Ꮡnode,
            forAwaitHierarchyFactsIterationStatementExcludes,
            forAwaitHierarchyFactsIterationStatementIncludes);
    }
    case var ᴛ when ᴛ == ast.KindConstructor: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitConstructorDeclaration)),
            Ꮡnode,
            forAwaitHierarchyFactsClassOrFunctionExcludes,
            forAwaitHierarchyFactsClassOrFunctionIncludes);
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitMethodDeclaration)),
            Ꮡnode,
            forAwaitHierarchyFactsClassOrFunctionExcludes,
            forAwaitHierarchyFactsClassOrFunctionIncludes);
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitGetAccessorDeclaration)),
            Ꮡnode,
            forAwaitHierarchyFactsClassOrFunctionExcludes,
            forAwaitHierarchyFactsClassOrFunctionIncludes);
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitSetAccessorDeclaration)),
            Ꮡnode,
            forAwaitHierarchyFactsClassOrFunctionExcludes,
            forAwaitHierarchyFactsClassOrFunctionIncludes);
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitFunctionDeclaration)),
            Ꮡnode,
            forAwaitHierarchyFactsClassOrFunctionExcludes,
            forAwaitHierarchyFactsClassOrFunctionIncludes);
    }
    case var ᴛ when ᴛ == ast.KindFunctionExpression: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitFunctionExpression)),
            Ꮡnode,
            forAwaitHierarchyFactsClassOrFunctionExcludes,
            forAwaitHierarchyFactsClassOrFunctionIncludes);
    }
    case var ᴛ when ᴛ == ast.KindArrowFunction: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitArrowFunction)),
            Ꮡnode,
            forAwaitHierarchyFactsArrowFunctionExcludes,
            forAwaitHierarchyFactsArrowFunctionIncludes);
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression: {
        return Ꮡtx.doWithHierarchyFacts(
            ((Func<ж<forawaitTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛforawaitTransformer.visitDefault)),
            Ꮡnode,
            forAwaitHierarchyFactsClassOrFunctionExcludes,
            forAwaitHierarchyFactsClassOrFunctionIncludes);
    }
    default: {
        return Ꮡtx.of(forawaitTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitAwaitExpression(this ref forawaitTransformer tx, ж<ast.AwaitExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0 && (ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        var result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewYieldExpression(
            nil,
            tx.Transformer.Factory().NewAwaitHelper(tx.Transformer.Visitor().VisitNode(node.Expression)));
        result.Loc = node.Loc;
        tx.Transformer.EmitContext().SetOriginal(result, Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return result;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitYieldExpression(this ref forawaitTransformer tx, ж<ast.YieldExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0 && (ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        if (node.AsteriskToken != nil) {
            var expression = tx.Transformer.Visitor().VisitNode(node.Expression);

            var asyncValuesResult = tx.Transformer.Factory().NewAsyncValuesHelper(expression);
            asyncValuesResult.Loc = expression.Loc;

            var asyncDelegatorResult = tx.Transformer.Factory().NewAsyncDelegatorHelper(asyncValuesResult);
            asyncDelegatorResult.Loc = expression.Loc;

            var innerYield = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateYieldExpression(
                Ꮡnode,
                node.AsteriskToken,
                asyncDelegatorResult);

            var awaitedYield = tx.Transformer.Factory().NewAwaitHelper(innerYield);

            var resultΔ1 = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewYieldExpression(
                nil,
                awaitedYield);
            resultΔ1.Loc = node.Loc;
            tx.Transformer.EmitContext().SetOriginal(resultΔ1, Ꮡnode.of(ast.YieldExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            return resultΔ1;
        }

        ж<ast.Node> innerExpression = default;
        if (node.Expression != nil) {
            innerExpression = tx.Transformer.Visitor().VisitNode(node.Expression);
        } else {
            innerExpression = tx.Transformer.Factory().NewVoidZeroExpression();
        }

        var result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewYieldExpression(
            nil,
            tx.createDownlevelAwait(innerExpression));
        result.Loc = node.Loc;
        tx.Transformer.EmitContext().SetOriginal(result, Ꮡnode.of(ast.YieldExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return result;
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.YieldExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitReturnStatement(this ref forawaitTransformer tx, ж<ast.ReturnStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0 && (ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        ж<ast.Node> expression = default;
        if (node.Expression != nil) {
            expression = tx.Transformer.Visitor().VisitNode(node.Expression);
        } else {
            expression = tx.Transformer.Factory().NewVoidZeroExpression();
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateReturnStatement(
            Ꮡnode,
            tx.createDownlevelAwait(expression));
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ReturnStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitLabeledStatement(this ref forawaitTransformer tx, ж<ast.LabeledStatement> node) {
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0) {
        var statement = unwrapInnermostStatementOfLabel(node);
        if (statement.Kind == ast.KindForOfStatement && statement.AsForInOrOfStatement().AwaitModifier != nil) {
            return tx.visitForOfStatement(statement.AsForInOrOfStatement(), node);
        }
        return tx.Transformer.Factory().RestoreEnclosingLabel(tx.Transformer.Visitor().VisitNode(statement), node);
    }
    return tx.Transformer.Visitor().VisitEachChild(node.of(ast.LabeledStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

} partial class estransforms_package {

internal static ж<ast.Node> unwrapInnermostStatementOfLabel(ж<ast.LabeledStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (;;) {
        if (node.Statement.Kind != ast.KindLabeledStatement) {
            return node.Statement;
        }
        Ꮡnode = node.Statement.AsLabeledStatement(); node = ref Ꮡnode.DerefOrNull();
    }
}

} partial class estransformsᴛforawaitTransformer {

internal static ж<ast.Node> visitSourceFile(this ref forawaitTransformer tx, ж<ast.SourceFile> node) {
    forAwaitHierarchyFacts ancestorFacts = tx.enterSubtree(
        forAwaitHierarchyFactsSourceFileExcludes,
        forAwaitHierarchyFactsStrictModeSourceFileIncludes);
    tx.exportedVariableStatement = false;
    var visited = tx.Transformer.Visitor().VisitEachChild(node.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AddEmitHelper(visited, tx.Transformer.EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    tx.exitSubtree(ancestorFacts);
    return visited;
}

internal static ж<ast.Node> visitForOfStatement(this ref forawaitTransformer tx, ж<ast.ForInOrOfStatement> Ꮡnode, ж<ast.LabeledStatement> outermostLabeledStatement) {
    ref var node = ref Ꮡnode.DerefOrNull();

    forAwaitHierarchyFacts ancestorFacts = tx.enterSubtree(forAwaitHierarchyFactsIterationStatementExcludes, forAwaitHierarchyFactsIterationStatementIncludes);
    ж<ast.Node> result = default;
    if (node.AwaitModifier != nil) {
        result = tx.transformForAwaitOfStatement(Ꮡnode, outermostLabeledStatement, ancestorFacts);
    } else {
        result = tx.Transformer.Factory().RestoreEnclosingLabel(tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), outermostLabeledStatement);
    }
    tx.exitSubtree(ancestorFacts);
    return result;
}

internal static ж<ast.Node> convertForOfStatementHead(this ref forawaitTransformer tx, ж<ast.ForInOrOfStatement> Ꮡnode, ж<ast.Node> boundValue, ж<ast.Node> nonUserCode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var value = f.NewTempVariable();
    tx.Transformer.EmitContext().AddVariableDeclaration(value);
    var iteratorValueExpression = f.NewAssignmentExpression(value, boundValue);
    var iteratorValueStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(iteratorValueExpression);
    tx.Transformer.EmitContext().SetSourceMapRange(iteratorValueStatement, node.Expression.Loc);

    var exitNonUserCodeExpression = f.NewAssignmentExpression(nonUserCode, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindFalseKeyword));
    var exitNonUserCodeStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(exitNonUserCodeExpression);
    tx.Transformer.EmitContext().SetSourceMapRange(exitNonUserCodeStatement, node.Expression.Loc);

    var statements = slice<ж<ast.Node>>([iteratorValueStatement, exitNonUserCodeStatement]);
    var binding = tx.Transformer.Factory().CreateForOfBindingStatement(node.Initializer, value);
    statements = append(statements, tx.Transformer.Visitor().VisitNode(binding));

    core.TextRange bodyLocation = default;
    core.TextRange statementsLocation = default;
    var statement = tx.Transformer.Visitor().VisitEmbeddedStatement(node.Statement);
    if (ast.IsBlock(statement)) {
        statements = appendꓸꓸꓸ(statements, statement.Statements());
        bodyLocation = statement.Loc;
        statementsLocation = statement.StatementList().Loc;
    } else {
        statements = append(statements, statement);
    }

    var stmtList = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
    stmtList.Loc = statementsLocation;
    var block = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(stmtList, true);
    block.Loc = bodyLocation;
    return block;
}

internal static ж<ast.Node> createDownlevelAwait(this ref forawaitTransformer tx, ж<ast.Node> expression) {
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewYieldExpression(
            nil,
            tx.Transformer.Factory().NewAwaitHelper(expression));
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewAwaitExpression(expression);
}

internal static ж<ast.Node> transformForAwaitOfStatement(this ref forawaitTransformer tx, ж<ast.ForInOrOfStatement> Ꮡnode, ж<ast.LabeledStatement> outermostLabeledStatement, forAwaitHierarchyFacts ancestorFacts) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);

    ж<ast.Node> iterator = default;
    if (ast.IsIdentifier(expression)) {
        iterator = f.NewGeneratedNameForNode(expression);
    } else {
        iterator = f.NewTempVariable();
    }

    ж<ast.Node> result = default;
    if (ast.IsIdentifier(expression)) {
        result = f.NewGeneratedNameForNode(iterator);
    } else {
        result = f.NewTempVariable();
    }

    var nonUserCode = f.NewTempVariable();
    var done = f.NewTempVariable();
    tx.Transformer.EmitContext().AddVariableDeclaration(done);
    var errorRecord = f.NewUniqueName("e");
    var catchVariable = f.NewGeneratedNameForNode(errorRecord);
    var returnMethod = f.NewTempVariable();
    var callValues = f.NewAsyncValuesHelper(expression);
    callValues.Loc = node.Expression.Loc;
    var callNext = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(iterator, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("next"), ast.NodeFlagsNone),
        nil, nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
        ast.NodeFlagsNone);
    var getDone = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(result, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("done"), ast.NodeFlagsNone);
    var getValue = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(result, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), ast.NodeFlagsNone);
    var callReturn = f.NewFunctionCallCall(returnMethod, iterator, []);

    tx.Transformer.EmitContext().AddVariableDeclaration(errorRecord);
    tx.Transformer.EmitContext().AddVariableDeclaration(returnMethod);

    ж<ast.Node> initializer = default;
    if ((forAwaitHierarchyFacts)(ancestorFacts & forAwaitHierarchyFactsIterationContainer) != 0) {
        initializer = f.InlineExpressions(slice<ж<ast.Node>>([
            f.NewAssignmentExpression(errorRecord, f.NewVoidZeroExpression()),
            callValues
        ]));
    } else {
        initializer = callValues;
    }

    var iteratorDecl = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(iterator, nil, nil, initializer);
    iteratorDecl.Loc = node.Expression.Loc;
    var varDeclList = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(nonUserCode, nil, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindTrueKeyword)),
        iteratorDecl,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(result, nil, nil, nil)
    ])), ast.NodeFlagsNone);
    varDeclList.Loc = node.Expression.Loc;

    var condition = f.InlineExpressions(slice<ж<ast.Node>>([
        f.NewAssignmentExpression(result, tx.createDownlevelAwait(callNext)),
        f.NewAssignmentExpression(done, getDone),
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindExclamationToken, done)
    ]));

    var incrementor = f.NewAssignmentExpression(nonUserCode, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindTrueKeyword));

    var forStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewForStatement(
        varDeclList,
        condition,
        incrementor,
        tx.convertForOfStatementHead(Ꮡnode, getValue, nonUserCode));
    forStatement.Loc = node.Loc;
    tx.Transformer.EmitContext().AddEmitFlags(forStatement, printer.EFNoTokenTrailingSourceMaps);
    tx.Transformer.EmitContext().SetOriginal(forStatement, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    var tryBlock = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
        f.RestoreEnclosingLabel(forStatement, outermostLabeledStatement)
    ])), true);

    var catchBody = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
            f.NewAssignmentExpression(
                errorRecord,
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("error"), nil, nil, catchVariable)
                ])), false)))
    ])), false);
    tx.Transformer.EmitContext().AddEmitFlags(catchBody, printer.EFSingleLine);
    var catchClause = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewCatchClause(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(catchVariable, nil, nil, nil),
        catchBody);

    var innerIfCondition = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
            nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindExclamationToken, nonUserCode),
            nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindAmpersandAmpersandToken),
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindExclamationToken, done)),
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindAmpersandAmpersandToken),
        f.NewAssignmentExpression(
            returnMethod,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(iterator, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("return"), ast.NodeFlagsNone)));
    var innerIfStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIfStatement(
        innerIfCondition,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(tx.createDownlevelAwait(callReturn)),
        nil);
    tx.Transformer.EmitContext().AddEmitFlags(innerIfStatement, printer.EFSingleLine);

    var innerTryBlock = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([innerIfStatement]), false);

    var innerFinallyIf = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIfStatement(
        errorRecord,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewThrowStatement(
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(errorRecord, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("error"), ast.NodeFlagsNone)),
        nil);
    tx.Transformer.EmitContext().AddEmitFlags(innerFinallyIf, printer.EFSingleLine);
    var innerFinallyBlock = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([innerFinallyIf]), false);
    tx.Transformer.EmitContext().AddEmitFlags(innerFinallyBlock, printer.EFSingleLine);

    var innerTryStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewTryStatement(innerTryBlock, nil, innerFinallyBlock);
    var finallyBlock = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([innerTryStatement]), true);

    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewTryStatement(tryBlock, catchClause, finallyBlock);
}

internal static ж<ast.Node> visitConstructorDeclaration(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsConstructorDeclaration();
    var savedEnclosingFunctionFlags = tx.enclosingFunctionFlags;
    tx.enclosingFunctionFlags = ast.GetFunctionFlags(Ꮡnode);
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(
        decl,
        decl.of(ast.ConstructorDeclaration.ᏑModifiersBase).Modifiers(),
        nil,
        tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor()),
        nil,
        nil,
        tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor()));
    tx.enclosingFunctionFlags = savedEnclosingFunctionFlags;
    return updated;
}

internal static ж<ast.Node> visitGetAccessorDeclaration(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsGetAccessorDeclaration();
    var savedEnclosingFunctionFlags = tx.enclosingFunctionFlags;
    tx.enclosingFunctionFlags = ast.GetFunctionFlags(Ꮡnode);
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(
        decl,
        decl.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers(),
        tx.Transformer.Visitor().VisitNode(decl.Name()),
        nil,
        tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor()),
        nil,
        nil,
        tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor()));
    tx.enclosingFunctionFlags = savedEnclosingFunctionFlags;
    return updated;
}

internal static ж<ast.Node> visitSetAccessorDeclaration(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsSetAccessorDeclaration();
    var savedEnclosingFunctionFlags = tx.enclosingFunctionFlags;
    tx.enclosingFunctionFlags = ast.GetFunctionFlags(Ꮡnode);
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(
        decl,
        decl.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers(),
        tx.Transformer.Visitor().VisitNode(decl.Name()),
        nil,
        tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor()),
        nil,
        nil,
        tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor()));
    tx.enclosingFunctionFlags = savedEnclosingFunctionFlags;
    return updated;
}

internal static ж<ast.Node> visitMethodDeclaration(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsMethodDeclaration();
    var savedEnclosingFunctionFlags = tx.enclosingFunctionFlags;
    tx.enclosingFunctionFlags = ast.GetFunctionFlags(Ꮡnode);

    ж<ast.ModifierList> modifiers = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        modifiers = tx.visitModifiersNoAsync(decl.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers());
    } else {
        modifiers = decl.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers();
    }

    ж<astꓸTokenNode> asteriskToken = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0) {
        asteriskToken = default;
    } else {
        asteriskToken = decl.AsteriskToken;
    }

    ж<ast.NodeList> parameters = default;
    ж<ast.Node> body = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0 && (ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        parameters = tx.transformAsyncGeneratorFunctionParameterList(Ꮡnode);
        body = tx.transformAsyncGeneratorFunctionBody(Ꮡnode);
    } else {
        parameters = tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor());
        body = tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor());
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(
        decl,
        modifiers,
        asteriskToken,
        tx.Transformer.Visitor().VisitNode(decl.Name()),
        nil,
        nil,
        parameters,
        nil,
        nil,
        body);
    tx.enclosingFunctionFlags = savedEnclosingFunctionFlags;
    return updated;
}

internal static ж<ast.Node> visitFunctionDeclaration(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsFunctionDeclaration();
    var savedEnclosingFunctionFlags = tx.enclosingFunctionFlags;
    tx.enclosingFunctionFlags = ast.GetFunctionFlags(Ꮡnode);

    ж<ast.ModifierList> modifiers = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        modifiers = tx.visitModifiersNoAsync(decl.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers());
    } else {
        modifiers = decl.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers();
    }

    ж<astꓸTokenNode> asteriskToken = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0) {
        asteriskToken = default;
    } else {
        asteriskToken = decl.AsteriskToken;
    }

    ж<ast.NodeList> parameters = default;
    ж<ast.Node> body = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0 && (ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        parameters = tx.transformAsyncGeneratorFunctionParameterList(Ꮡnode);
        body = tx.transformAsyncGeneratorFunctionBody(Ꮡnode);
    } else {
        parameters = tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor());
        body = tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor());
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(
        decl,
        modifiers,
        asteriskToken,
        decl.Name(),
        nil,
        parameters,
        nil,
        nil,
        body);
    tx.enclosingFunctionFlags = savedEnclosingFunctionFlags;
    return updated;
}

internal static ж<ast.Node> visitArrowFunction(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsArrowFunction();
    var savedEnclosingFunctionFlags = tx.enclosingFunctionFlags;
    tx.enclosingFunctionFlags = ast.GetFunctionFlags(Ꮡnode);
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateArrowFunction(
        decl,
        decl.of(ast.ArrowFunction.ᏑModifiersBase).Modifiers(),
        nil,
        tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor()),
        nil,
        nil,
        decl.EqualsGreaterThanToken,
        tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor()));
    tx.enclosingFunctionFlags = savedEnclosingFunctionFlags;
    return updated;
}

internal static ж<ast.Node> visitFunctionExpression(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = node.AsFunctionExpression();
    var savedEnclosingFunctionFlags = tx.enclosingFunctionFlags;
    tx.enclosingFunctionFlags = ast.GetFunctionFlags(Ꮡnode);

    ж<ast.ModifierList> modifiers = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        modifiers = tx.visitModifiersNoAsync(decl.of(ast.FunctionExpression.ᏑModifiersBase).Modifiers());
    } else {
        modifiers = decl.of(ast.FunctionExpression.ᏑModifiersBase).Modifiers();
    }

    ж<astꓸTokenNode> asteriskToken = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0) {
        asteriskToken = default;
    } else {
        asteriskToken = decl.AsteriskToken;
    }

    ж<ast.NodeList> parameters = default;
    ж<ast.Node> body = default;
    if ((ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsAsync) != 0 && (ast.FunctionFlags)(tx.enclosingFunctionFlags & ast.FunctionFlagsGenerator) != 0) {
        parameters = tx.transformAsyncGeneratorFunctionParameterList(Ꮡnode);
        body = tx.transformAsyncGeneratorFunctionBody(Ꮡnode);
    } else {
        parameters = tx.Transformer.EmitContext().VisitParameters((~decl).Parameters, tx.Transformer.Visitor());
        body = tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor());
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionExpression(
        decl,
        modifiers,
        asteriskToken,
        decl.Name(),
        nil,
        parameters,
        nil,
        nil,
        body);
    tx.enclosingFunctionFlags = savedEnclosingFunctionFlags;
    return updated;
}

internal static ж<ast.NodeList> transformAsyncGeneratorFunctionParameterList(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isSimpleParameterList(node.Parameters())) {
        return tx.Transformer.EmitContext().VisitParameters(node.ParameterList(), tx.Transformer.Visitor());
    }
    slice<ж<ast.Node>> newParameters = default;
    foreach (var (_, parameter) in range(node.Parameters())) {
        var param = parameter.AsParameterDeclaration();
        if ((~param).Initializer != nil || param.DotDotDotToken != nil) {
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

internal static ж<ast.Node> transformAsyncGeneratorFunctionBody(this ref forawaitTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    ж<ast.NodeList> innerParameters = default;
    if (!isSimpleParameterList(node.Parameters())) {
        innerParameters = tx.Transformer.EmitContext().VisitParameters(node.ParameterList(), tx.Transformer.Visitor());
    }

    var savedCapturedSuperProperties = tx.capturedSuperProperties;
    var savedHasSuperElementAccess = tx.hasSuperElementAccess;
    var savedHasSuperPropertyAssignment = tx.hasSuperPropertyAssignment;
    var savedSuperBinding = tx.superBinding;
    var savedSuperIndexBinding = tx.superIndexBinding;
    tx.capturedSuperProperties = Ꮡ(new collections.OrderedSet<@string>());
    tx.hasSuperElementAccess = false;
    tx.hasSuperPropertyAssignment = false;
    tx.superBinding = f.NewUniqueNameEx("_super", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));
    tx.superIndexBinding = f.NewUniqueNameEx("_superIndex", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));

    var asyncBody = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(
        node.Body().AsBlock(),
        tx.Transformer.Visitor().VisitNodes(node.Body().StatementList()),
        node.Body().AsBlock().MultiLine);
    asyncBody = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(
        asyncBody.AsBlock(),
        tx.Transformer.EmitContext().EndAndMergeVariableEnvironmentList(asyncBody.StatementList()),
        asyncBody.AsBlock().MultiLine);

    var emitSuperHelpers = tx.capturedSuperProperties.Size() > 0 || tx.hasSuperElementAccess;
    if (emitSuperHelpers) {
        asyncBody = tx.superAccessState.substituteSuperAccessesInBody(asyncBody);
    }

    ж<ast.NodeList> innerParams = default;
    if (innerParameters != nil) {
        innerParams = innerParameters;
    } else {
        innerParams = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]);
    }

    ж<ast.Node> name = default;
    if (node.Name() != nil) {
        name = f.NewGeneratedNameForNode(node.Name());
    }

    var generatorFunc = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionExpression(
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindAsteriskToken),
        name,
        nil,
        innerParams,
        nil,
        nil,
        asyncBody);

    var returnStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(
        f.NewAsyncGeneratorHelper(
            generatorFunc,
            (forAwaitHierarchyFacts)(tx.forAwaitHierarchyFacts & forAwaitHierarchyFactsHasLexicalThis) != 0));

    tx.Transformer.EmitContext().StartVariableEnvironment();
    if (emitSuperHelpers) {
        if (tx.capturedSuperProperties.Size() > 0) {
            tx.Transformer.EmitContext().AddInitializationStatement(tx.superAccessState.createSuperAccessVariableStatement());
        }
    }

    var outerStatements = slice<ж<ast.Node>>([returnStatement]);

    var block = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(
        node.Body().AsBlock(),
        tx.Transformer.EmitContext().EndAndMergeVariableEnvironmentList(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(outerStatements)),
        node.Body().AsBlock().MultiLine);

    if (emitSuperHelpers && tx.hasSuperElementAccess) {
        if (tx.hasSuperPropertyAssignment) {
            tx.Transformer.EmitContext().AddEmitHelper(block, printer.AdvancedAsyncSuperHelper);
        } else {
            tx.Transformer.EmitContext().AddEmitHelper(block, printer.AsyncSuperHelper);
        }
    }

    tx.capturedSuperProperties = savedCapturedSuperProperties;
    tx.hasSuperElementAccess = savedHasSuperElementAccess;
    tx.hasSuperPropertyAssignment = savedHasSuperPropertyAssignment;
    tx.superBinding = savedSuperBinding;
    tx.superIndexBinding = savedSuperIndexBinding;

    return block;
}

} // end estransformsᴛforawaitTransformer
