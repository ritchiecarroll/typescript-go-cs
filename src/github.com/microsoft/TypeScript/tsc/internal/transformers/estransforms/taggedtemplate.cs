namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static estransforms_package;

partial class estransforms_package {

internal static ж<strings.Replacer> newlineNormalizer = strings.NewReplacer("\r\n", "\n", "\r", "\n");

partial struct taggedTemplateTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<ast.SourceFile> currentSourceFile;
    internal slice<ж<ast.Node>> taggedTemplateStringDeclarations;
}

internal static ж<transformers.Transformer> newTaggedTemplateLiftRestrictionTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new taggedTemplateTransformer());
    return tx.of(taggedTemplateTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} partial class estransformsᴛtaggedTemplateTransformer {

internal static ж<ast.Node> visit(this ref taggedTemplateTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsInvalidTemplateEscape) == 0) {
        return Ꮡnode;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        return tx.visitSourceFile(node.AsSourceFile());
    }
    case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
        return tx.visitTaggedTemplateExpression(node.AsTaggedTemplateExpression());
    }
    default: {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitSourceFile(this ref taggedTemplateTransformer tx, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    tx.currentSourceFile = Ꮡnode;
    tx.taggedTemplateStringDeclarations = default;
    var visited = tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    if (len(tx.taggedTemplateStringDeclarations) > 0) {
        var visitedSourceFile = visited.AsSourceFile();
        var statements = append(
            slices.Clip<slice<ж<ast.Node>>, ж<ast.Node>>((~visitedSourceFile).Statements.Nodes),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(tx.taggedTemplateStringDeclarations),
                    ast.NodeFlagsNone)));
        var stmtList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
        stmtList.Loc = node.Statements.Loc;
        visited = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(visitedSourceFile, stmtList, visitedSourceFile.EndOfFileToken);
    }

    tx.Transformer.EmitContext().AddEmitHelper(visited, tx.Transformer.EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    return visited;
}

internal static ж<ast.Node> visitTaggedTemplateExpression(this ref taggedTemplateTransformer tx, ж<ast.TaggedTemplateExpression> node) {
    return tx.processTaggedTemplateExpression(node);
}

internal static ж<ast.Node> processTaggedTemplateExpression(this ref taggedTemplateTransformer tx, ж<ast.TaggedTemplateExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var tag = tx.Transformer.Visitor().VisitNode(node.Tag);
    var template = node.Template;

    if (!hasInvalidEscape(template)) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var f = tx.Transformer.Factory();

    var templateArguments = slice<ж<ast.Node>>([default]);
    slice<ж<ast.Node>> cookedStrings = default;
    slice<ж<ast.Node>> rawStrings = default;

    if (ast.IsNoSubstitutionTemplateLiteral(template)) {
        cookedStrings = append(cookedStrings, createTemplateCooked(f, ref (template.TemplateLiteralLikeData()).DerefOrNull()));
        rawStrings = append(rawStrings, getRawLiteral(f, template));
    } else {
        var te = template.AsTemplateExpression();
        cookedStrings = append(cookedStrings, createTemplateCooked(f, ref (te.Head.TemplateLiteralLikeData()).DerefOrNull()));
        rawStrings = append(rawStrings, getRawLiteral(f, te.Head));
        foreach (var (_, span) in range(te.TemplateSpans.Nodes)) {
            var ts = span.AsTemplateSpan();
            cookedStrings = append(cookedStrings, createTemplateCooked(f, ref (ts.Literal.TemplateLiteralLikeData()).DerefOrNull()));
            rawStrings = append(rawStrings, getRawLiteral(f, ts.Literal));
            templateArguments = append(templateArguments, tx.Transformer.Visitor().VisitNode((~ts).Expression));
        }
    }

    var helperCall = f.NewTemplateObjectHelper(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(cookedStrings), false),
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(rawStrings), false));

    if (ast.IsExternalModule(tx.currentSourceFile)) {
        var tempVar = f.NewUniqueName("templateObject");
        tx.taggedTemplateStringDeclarations = append(
            tx.taggedTemplateStringDeclarations,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(tempVar, nil, nil, nil));
        templateArguments[0] = f.NewLogicalORExpression(
            tempVar,
            f.NewAssignmentExpression(tempVar, helperCall));
    } else {
        templateArguments[0] = helperCall;
    }

    var call = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(tag, nil, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(templateArguments), ast.NodeFlagsNone);
    call.Loc = node.Loc;
    return call;
}

} partial class estransforms_package {

internal static ж<ast.Node> createTemplateCooked(ж<printer.NodeFactory> Ꮡf, ref ast.TemplateLiteralLikeNodeBase template) {
    ref var f = ref Ꮡf.DerefOrNull();

    if ((ast.TokenFlags)(template.TemplateFlags & ast.TokenFlagsIsInvalid) != 0) {
        return f.NewVoidZeroExpression();
    }
    return Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(template.Text, ast.TokenFlagsNone);
}

internal static ж<ast.Node> getRawLiteral(ж<printer.NodeFactory> f, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    @string text = node.TemplateLiteralLikeData().RawText;
    if (text == ""u8) {
        text = scanner.GetSourceTextOfNodeFromSourceFile(ast.GetSourceFileOfNode(Ꮡnode), Ꮡnode, false);
        var isLast = node.Kind == ast.KindNoSubstitutionTemplateLiteral || node.Kind == ast.KindTemplateTail;
        nint endLen = 2;
        if (isLast) {
            endLen = 1;
        }
        text = text.slice(1, len(text) - endLen);
    }

    text = newlineNormalizer.Replace(text);

    var result = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(text, ast.TokenFlagsNone);
    result.Loc = node.Loc;
    return result;
}

internal static bool hasInvalidEscape(ж<ast.Node> Ꮡtemplate) {
    ref var template = ref Ꮡtemplate.DerefOrNull();

    if (ast.IsNoSubstitutionTemplateLiteral(Ꮡtemplate)) {
        return (ast.TokenFlags)(template.TemplateLiteralLikeData().TemplateFlags & ast.TokenFlagsContainsInvalidEscape) != 0;
    }
    var te = template.AsTemplateExpression();
    if ((ast.TokenFlags)(te.Head.TemplateLiteralLikeData().TemplateFlags & ast.TokenFlagsContainsInvalidEscape) != 0) {
        return true;
    }
    foreach (var (_, span) in range(te.TemplateSpans.Nodes)) {
        if ((ast.TokenFlags)(span.AsTemplateSpan().Literal.TemplateLiteralLikeData().TemplateFlags & ast.TokenFlagsContainsInvalidEscape) != 0) {
            return true;
        }
    }
    return false;
}

} // end estransforms_package
