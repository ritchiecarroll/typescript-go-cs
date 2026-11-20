namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using static moduletransforms_package;

partial class moduletransforms_package {

partial struct CommonJSModuleTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<ast.NodeVisitor> topLevelVisitor;
    internal ж<ast.NodeVisitor> topLevelNestedVisitor;
    internal ж<ast.NodeVisitor> discardedValueVisitor;
    internal ж<ast.NodeVisitor> assignmentPatternVisitor;
    internal ж<core.CompilerOptions> compilerOptions;
    internal binder.ReferenceResolver resolver;
    internal Func<ast.HasFileName, core.ModuleKind> getEmitModuleFormatOfFile;
    internal core.ModuleKind moduleKind;
    internal core.ScriptTarget languageVersion;
    internal ж<ast.SourceFile> currentSourceFile;
    internal ж<externalModuleInfo> currentModuleInfo;
    internal ж<ast.Node> parentNode;
    internal ж<ast.Node> currentNode;
}

public static ж<transformers.Transformer> NewCommonJSModuleTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var compilerOptions = opts.CompilerOptions;
    var emitContext = opts.Context;
    var tx = Ꮡ(new CommonJSModuleTransformer(compilerOptions: compilerOptions, resolver: opts.Resolver, getEmitModuleFormatOfFile: opts.GetEmitModuleFormatOfFile));
    tx.topLevelVisitor = emitContext.NewNodeVisitor(tx.visitTopLevel);
    tx.topLevelNestedVisitor = emitContext.NewNodeVisitor(tx.visitTopLevelNested);
    tx.discardedValueVisitor = emitContext.NewNodeVisitor(tx.visitDiscardedValue);
    tx.assignmentPatternVisitor = emitContext.NewNodeVisitor(tx.visitAssignmentPattern);
    tx.languageVersion = compilerOptions.GetEmitScriptTarget();
    tx.moduleKind = compilerOptions.GetEmitModuleKind();
    return tx.of(CommonJSModuleTransformer.ᏑTransformer).NewTransformer(tx.visit, emitContext);
}

} partial class moduletransformsᴛCommonJSModuleTransformer {

internal static ж<ast.Node> /*grandparentNode*/ pushNode(this ref CommonJSModuleTransformer tx, ж<ast.Node> node) {
    ж<ast.Node> grandparentNode = default;

    grandparentNode = tx.parentNode;
    tx.parentNode = tx.currentNode;
    tx.currentNode = node;
    return grandparentNode;
}

internal static void popNode(this ref CommonJSModuleTransformer tx, ж<ast.Node> grandparentNode) {
    tx.currentNode = tx.parentNode;
    tx.parentNode = grandparentNode;
}

internal static ж<ast.Node> visitTopLevel(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var grandparentNode = tx.pushNode(Ꮡnode);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindImportDeclaration: {
            Ꮡnode = tx.visitTopLevelImportDeclaration(node.AsImportDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
            Ꮡnode = tx.visitTopLevelImportEqualsDeclaration(node.AsImportEqualsDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindExportDeclaration: {
            Ꮡnode = tx.visitTopLevelExportDeclaration(node.AsExportDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindExportAssignment: {
            Ꮡnode = tx.visitTopLevelExportAssignment(node.AsExportAssignment()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
            Ꮡnode = tx.visitTopLevelFunctionDeclaration(node.AsFunctionDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            Ꮡnode = tx.visitTopLevelClassDeclaration(node.AsClassDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindVariableStatement: {
            Ꮡnode = Ꮡtx.visitTopLevelVariableStatement(node.AsVariableStatement()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        default: {
            Ꮡnode = Ꮡtx.visitTopLevelNestedNoStack(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
            break;
        }}

        return Ꮡnode;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitTopLevelNested(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();

        var grandparentNode = tx.pushNode(node);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        return Ꮡtx.visitTopLevelNestedNoStack(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitTopLevelNestedNoStack(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableStatement: {
        Ꮡnode = Ꮡtx.visitTopLevelVariableStatement(node.AsVariableStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        Ꮡnode = tx.visitTopLevelNestedForStatement(node.AsForStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement: {
        Ꮡnode = tx.visitTopLevelNestedForInOrOfStatement(node.AsForInOrOfStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindDoStatement: {
        Ꮡnode = tx.visitTopLevelNestedDoStatement(node.AsDoStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindWhileStatement: {
        Ꮡnode = tx.visitTopLevelNestedWhileStatement(node.AsWhileStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindLabeledStatement: {
        Ꮡnode = tx.visitTopLevelNestedLabeledStatement(node.AsLabeledStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindWithStatement: {
        Ꮡnode = tx.visitTopLevelNestedWithStatement(node.AsWithStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindIfStatement: {
        Ꮡnode = tx.visitTopLevelNestedIfStatement(node.AsIfStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindSwitchStatement: {
        Ꮡnode = tx.visitTopLevelNestedSwitchStatement(node.AsSwitchStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseBlock: {
        Ꮡnode = tx.visitTopLevelNestedCaseBlock(node.AsCaseBlock()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseClause || ᴛ == ast.KindDefaultClause: {
        Ꮡnode = tx.visitTopLevelNestedCaseOrDefaultClause(node.AsCaseOrDefaultClause()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindTryStatement: {
        Ꮡnode = tx.visitTopLevelNestedTryStatement(node.AsTryStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindCatchClause: {
        Ꮡnode = tx.visitTopLevelNestedCatchClause(node.AsCatchClause()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindBlock: {
        Ꮡnode = tx.visitTopLevelNestedBlock(node.AsBlock()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    default: {
        Ꮡnode = Ꮡtx.visitNoStack(Ꮡnode, false); node = ref Ꮡnode.DerefOrNull();
        break;
    }}

    return Ꮡnode;
}

internal static ж<ast.Node> visit(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();

        var grandparentNode = tx.pushNode(node);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        return Ꮡtx.visitNoStack(node, false);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitNoStack(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> Ꮡnode, bool resultIsDiscarded) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsSourceFile(Ꮡnode) && (ast.SubtreeFacts)(node.SubtreeFacts() & ((ast.SubtreeFacts)(ast.SubtreeContainsDynamicImport | ast.SubtreeContainsIdentifier))) == 0) {
        return Ꮡnode;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        Ꮡnode = Ꮡtx.visitSourceFile(node.AsSourceFile()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        Ꮡnode = tx.visitForStatement(node.AsForStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement: {
        Ꮡnode = tx.visitForInOrOfStatement(node.AsForInOrOfStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindExpressionStatement: {
        Ꮡnode = tx.visitExpressionStatement(node.AsExpressionStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindVoidExpression: {
        Ꮡnode = tx.visitVoidExpression(node.AsVoidExpression()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        Ꮡnode = tx.visitParenthesizedExpression(node.AsParenthesizedExpression(), resultIsDiscarded); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindPartiallyEmittedExpression: {
        Ꮡnode = tx.visitPartiallyEmittedExpression(node.AsPartiallyEmittedExpression(), resultIsDiscarded); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        Ꮡnode = tx.visitCallExpression(node.AsCallExpression()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
        Ꮡnode = tx.visitTaggedTemplateExpression(node.AsTaggedTemplateExpression()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        Ꮡnode = Ꮡtx.visitBinaryExpression(node.AsBinaryExpression(), resultIsDiscarded); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        Ꮡnode = tx.visitPrefixUnaryExpression(node.AsPrefixUnaryExpression(), resultIsDiscarded); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindPostfixUnaryExpression: {
        Ꮡnode = tx.visitPostfixUnaryExpression(node.AsPostfixUnaryExpression(), resultIsDiscarded); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        Ꮡnode = tx.visitShorthandPropertyAssignment(node.AsShorthandPropertyAssignment()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡnode = tx.visitIdentifier(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    default: {
        Ꮡnode = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        break;
    }}

    return Ꮡnode;
}

internal static ж<ast.Node> visitDiscardedValue(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();

        var grandparentNode = tx.pushNode(node);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        return Ꮡtx.visitNoStack(node, true);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitAssignmentPattern(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();

        var grandparentNode = tx.pushNode(node);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        return Ꮡtx.visitAssignmentPatternNoStack(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitAssignmentPatternNoStack(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindArrayLiteralExpression: {
        Ꮡnode = tx.assignmentPatternVisitor.VisitEachChild(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
        Ꮡnode = tx.visitAssignmentProperty(node.AsPropertyAssignment()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        Ꮡnode = Ꮡtx.visitShorthandAssignmentProperty(node.AsShorthandPropertyAssignment()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindSpreadAssignment: {
        Ꮡnode = Ꮡtx.visitAssignmentRestProperty(node.AsSpreadAssignment()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindSpreadElement: {
        Ꮡnode = Ꮡtx.visitAssignmentRestElement(node.AsSpreadElement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    default: {
        if (ast.IsExpression(Ꮡnode)) {
            Ꮡnode = Ꮡtx.visitAssignmentElement(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
            break;
        }

        Ꮡnode = Ꮡtx.visitNoStack(Ꮡnode, false); node = ref Ꮡnode.DerefOrNull();
        break;
    }}

    return Ꮡnode;
}

internal static ж<ast.Node> visitSourceFile(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.SourceFile> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsDeclarationFile || !(ast.IsEffectiveExternalModule(Ꮡnode, tx.compilerOptions) || (ast.SubtreeFacts)(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).SubtreeFacts() & ast.SubtreeContainsDynamicImport) != 0)) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    tx.currentSourceFile = Ꮡnode;
    tx.currentModuleInfo = collectExternalModuleInfo(Ꮡnode, tx.compilerOptions, Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), tx.resolver);
    var updated = Ꮡtx.transformCommonJSModule(Ꮡnode);
    tx.currentSourceFile = default;
    tx.currentModuleInfo = default;
    return updated;
}

internal static bool shouldEmitUnderscoreUnderscoreESModule(this ref CommonJSModuleTransformer tx) {
    if (tspath.FileExtensionIsOneOf(tx.currentSourceFile.FileName(), tspath.SupportedJSExtensionsFlat) && tx.currentSourceFile.CommonJSModuleIndicator != nil && (tx.currentSourceFile.ExternalModuleIndicator == nil || tx.currentSourceFile.ExternalModuleIndicator.Kind == ast.KindSourceFile)) {
        return false;
    }
    if (tx.currentModuleInfo.exportEquals == nil && ast.IsExternalModule(tx.currentSourceFile)) {
        return true;
    }
    return false;
}

internal static ж<astꓸStatement> createUnderscoreUnderscoreESModule(this ref CommonJSModuleTransformer tx) {
    var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object"),
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("defineProperty"),
                ast.NodeFlagsNone),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("__esModule", ast.TokenFlagsNone),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(
                            nil,
                            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"),
                            nil,
                            nil,
                            tx.Transformer.Factory().NewTrueExpression())
                    ])),
                    false)
            ])),
            ast.NodeFlagsNone));
    tx.Transformer.EmitContext().SetEmitFlags(statement, printer.EFCustomPrologue);
    return statement;
}

internal static ж<ast.Node> transformCommonJSModule(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.SourceFile> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().StartVariableEnvironment();

    var (prologue, rest) = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().SplitStandardPrologue(node.Statements.Nodes);
    var statements = slices.Clone<slice<ж<astꓸStatement>>, ж<astꓸStatement>>(prologue);

    (var custom, rest) = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().SplitCustomPrologue(rest);
    var (ᴛ1, ᴛ2) = tx.topLevelVisitor.VisitSlice(custom);
    statements = appendꓸꓸꓸ(statements, core.FirstResult(ᴛ1, ᴛ2));

    if (tx.shouldEmitUnderscoreUnderscoreESModule()) {
        statements = append(statements, tx.createUnderscoreUnderscoreESModule());
    }

    if (len(tx.currentModuleInfo.exportedNames) > 0) {
        const nint chunkSize = 50;
        nint l = len(tx.currentModuleInfo.exportedNames);
        for (nint i = 0; i < l; i += chunkSize) {
            var right = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewVoidZeroExpression();
            foreach (var (_, nextId) in range(tx.currentModuleInfo.exportedNames.slice(i, min(i + chunkSize, l)))) {
                ж<astꓸExpression> left = default;
                if (nextId.Kind == ast.KindStringLiteral) {
                    left = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(
                        Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                        nil,
                        Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewStringLiteralFromNode(nextId),
                        ast.NodeFlagsNone);
                } else {
                    var name = nextId.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory()));
                    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().SetEmitFlags(name, (printer.EmitFlags)(printer.EFNoSourceMap | printer.EFNoComments));
                    left = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                        Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                        nil,
                        name,
                        ast.NodeFlagsNone);
                }
                right = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewAssignmentExpression(left, right);
            }
            var statement = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(right);
            Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFCustomPrologue);
            statements = append(statements, statement);
        }
    }

    nint exportedFunctionsStart = len(statements);
    foreach (var f in range<ж<astꓸFunctionDeclarationNode>>(tx.currentModuleInfo.ᏑexportedFunctions.Values().Invoke)) {
        statements = tx.appendExportsOfClassOrFunctionDeclaration(statements, f.AsNode());
    }
    foreach (var (_, s) in range(statements.slice(exportedFunctionsStart))) {
        Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AddEmitFlags(s, printer.EFCustomPrologue);
    }

    (rest, _) = tx.topLevelVisitor.VisitSlice(rest);
    statements = appendꓸꓸꓸ(statements, rest);

    statements = Ꮡtx.appendExportEqualsIfNeeded(statements);

    statements = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().EndAndMergeVariableEnvironment(statements);

    var statementList = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
    statementList.Loc = node.Statements.Loc;
    var result = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(Ꮡnode, statementList, node.EndOfFileToken).AsSourceFile();
    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AddEmitHelper(result.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().ReadEmitHelpers().ꓸꓸꓸ);

    var externalHelpersImportDeclaration = createExternalHelpersImportDeclarationIfNeeded(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), result, tx.compilerOptions, tx.getEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(Ꮡnode)), false, false, false);
    if (externalHelpersImportDeclaration != nil) {
        var (prologueΔ1, restΔ1) = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().SplitStandardPrologue((~result).Statements.Nodes);
        (var customΔ1, restΔ1) = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().SplitCustomPrologue(restΔ1);
        var statementsΔ1 = slices.Clone<slice<ж<astꓸStatement>>, ж<astꓸStatement>>(prologueΔ1);
        statementsΔ1 = appendꓸꓸꓸ(statementsΔ1, customΔ1);
        statementsΔ1 = append(statementsΔ1, tx.topLevelVisitor.VisitNode(externalHelpersImportDeclaration));
        statementsΔ1 = appendꓸꓸꓸ(statementsΔ1, restΔ1);
        var statementListΔ1 = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statementsΔ1);
        statementListΔ1.Loc = result.Value.Statements.Loc;
        result = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(result, statementListΔ1, node.EndOfFileToken).AsSourceFile();
    }

    return result.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

internal static slice<ж<astꓸStatement>> appendExportEqualsIfNeeded(this ж<CommonJSModuleTransformer> Ꮡtx, slice<ж<astꓸStatement>> statements) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (tx.currentModuleInfo.exportEquals != nil) {
        var expressionResult = Ꮡtx.visitExportEquals(tx.currentModuleInfo.exportEquals);
        if (expressionResult != nil) {
            var statement = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewAssignmentExpression(
                    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                        Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("module"),
                        nil,
                        Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                        ast.NodeFlagsNone),
                    expressionResult));

            Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AssignCommentAndSourceMapRanges(statement, tx.currentModuleInfo.exportEquals.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFNoComments);
            statements = append(statements, statement);
        }
    }
    return statements;
}

internal static ж<astꓸExpression> visitExportEquals(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.ExportAssignment> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var grandparentNode = tx.pushNode(Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);
        return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode(node.Expression);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static slice<ж<astꓸStatement>> appendExportsOfImportDeclaration(this ref CommonJSModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<ast.ImportDeclaration> Ꮡdecl) {
    ref var decl = ref Ꮡdecl.DerefOrNull();

    if (tx.currentModuleInfo.exportEquals != nil) {
        return statements;
    }

    var importClause = decl.ImportClause;
    if (importClause == nil) {
        return statements;
    }

    var seen = Ꮡ(new collections.Set<@string>());
    if (importClause.Name() != nil) {
        statements = tx.appendExportsOfDeclaration(statements, importClause, seen, false);
    }

    var namedBindings = importClause.AsImportClause().NamedBindings;
    if (namedBindings != nil) {
        switch (namedBindings.Kind) {
        case var ᴛ when ᴛ == ast.KindNamespaceImport: {
            statements = tx.appendExportsOfDeclaration(statements, namedBindings, seen, false);
            break;
        }
        case var ᴛ when ᴛ == ast.KindNamedImports: {
            foreach (var (_, importBinding) in range(namedBindings.Elements())) {
                statements = tx.appendExportsOfDeclaration(statements, importBinding, seen, true);
            }
            break;
        }}

    }

    return statements;
}

internal static slice<ж<astꓸStatement>> appendExportsOfVariableStatement(this ref CommonJSModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<ast.VariableStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.appendExportsOfVariableDeclarationList(statements, node.DeclarationList.AsVariableDeclarationList(), false);
}

internal static slice<ж<astꓸStatement>> appendExportsOfVariableDeclarationList(this ref CommonJSModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<ast.VariableDeclarationList> Ꮡnode, bool isForInOrOfInitializer) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.currentModuleInfo.exportEquals != nil) {
        return statements;
    }

    foreach (var (_, decl) in range(node.Declarations.Nodes)) {
        statements = tx.appendExportsOfBindingElement(statements, decl, isForInOrOfInitializer);
    }

    return statements;
}

internal static slice<ж<astꓸStatement>> appendExportsOfBindingElement(this ref CommonJSModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<ast.Node> Ꮡdecl, bool isForInOrOfInitializer) {
    ref var decl = ref Ꮡdecl.DerefOrNull();

    if (tx.currentModuleInfo.exportEquals != nil || decl.Name() == nil) {
        return statements;
    }

    if (ast.IsBindingPattern(decl.Name())) {
        foreach (var (_, element) in range(decl.Name().Elements())) {
            if (!ast.IsOmittedExpression(element)) {
                statements = tx.appendExportsOfBindingElement(statements, element, isForInOrOfInitializer);
            }
        }
    } else if (!transformers.IsGeneratedIdentifier(tx.Transformer.EmitContext(), decl.Name()) && (!ast.IsVariableDeclaration(Ꮡdecl) || decl.Initializer() != nil || isForInOrOfInitializer)) {
        statements = tx.appendExportsOfDeclaration(statements, Ꮡdecl, nil, false);
    }

    return statements;
}

internal static slice<ж<astꓸStatement>> appendExportsOfClassOrFunctionDeclaration(this ref CommonJSModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<astꓸDeclaration> Ꮡdecl) {
    ref var decl = ref Ꮡdecl.DerefOrNull();

    if (tx.currentModuleInfo.exportEquals != nil) {
        return statements;
    }

    var seen = Ꮡ(new collections.Set<@string>());
    if (ast.HasSyntacticModifier(Ꮡdecl, ast.ModifierFlagsExport)) {
        ж<astꓸIdentifierNode> exportName = default;
        if (ast.HasSyntacticModifier(Ꮡdecl, ast.ModifierFlagsDefault)) {
            exportName = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("default");
        } else {
            exportName = tx.Transformer.Factory().GetDeclarationName(Ꮡdecl);
        }

        var exportValue = tx.Transformer.Factory().GetLocalName(Ꮡdecl);
        statements = tx.appendExportStatement(statements, seen, exportName, exportValue, Ꮡdecl.ᏑLoc, false, false);
    }

    if (decl.Name() != nil) {
        return tx.appendExportsOfDeclaration(statements, Ꮡdecl, seen, false);
    }

    return statements;
}

internal static slice<ж<astꓸStatement>> appendExportsOfDeclaration(this ref CommonJSModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<astꓸDeclaration> Ꮡdecl, ж<collections.Set<@string>> Ꮡseen, bool liveBinding) {
    ref var decl = ref Ꮡdecl.DerefOrNull();
    ref var seen = ref Ꮡseen.DerefOrNull();

    if (tx.currentModuleInfo.exportEquals != nil) {
        return statements;
    }

    if (Ꮡseen == nil) {
        Ꮡseen = Ꮡ(new collections.Set<@string>()); seen = ref Ꮡseen.DerefOrNull();
    }

    var name = decl.Name(); if (tx.currentModuleInfo.ᏑexportSpecifiers.Len() > 0 && name != nil && ast.IsIdentifier(name)) {
        name = tx.Transformer.Factory().GetDeclarationName(Ꮡdecl);
        var exportSpecifiers = tx.currentModuleInfo.ᏑexportSpecifiers.Get(name.Text());
        if (len(exportSpecifiers) > 0) {
            var exportValue = tx.visitExpressionIdentifier(name);
            foreach (var (_, exportSpecifier) in range(exportSpecifiers)) {
                statements = tx.appendExportStatement(statements, Ꮡseen, exportSpecifier.Name(), exportValue, exportSpecifier.Name().ᏑLoc, false, liveBinding);
            }
        }
    }

    return statements;
}

internal static slice<ж<astꓸStatement>> appendExportStatement(this ref CommonJSModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<collections.Set<@string>> Ꮡseen, ж<astꓸModuleExportName> ᏑexportName, ж<astꓸExpression> expression, ж<core.TextRange> location, bool allowComments, bool liveBinding) {
    ref var seen = ref Ꮡseen.DerefOrNull();
    ref var exportName = ref ᏑexportName.DerefOrNull();

    if (exportName.Kind != ast.KindStringLiteral) {
        if (Ꮡseen.Has(exportName.Text())) {
            return statements;
        }
        seen.Add(exportName.Text());
    }
    statements = append(statements, tx.createExportStatement(ᏑexportName, expression, location, allowComments, liveBinding));
    return statements;
}

internal static ж<astꓸStatement> createExportStatement(this ref CommonJSModuleTransformer tx, ж<astꓸModuleExportName> name, ж<astꓸExpression> value, ж<core.TextRange> Ꮡlocation, bool allowComments, bool liveBinding) {
    ref var location = ref Ꮡlocation.DerefOrNull();

    var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(tx.createExportExpression(name, value, nil, liveBinding));
    if (Ꮡlocation != nil) {
        tx.Transformer.EmitContext().SetCommentRange(statement, location);
    }
    tx.Transformer.EmitContext().AddEmitFlags(statement, printer.EFStartOnNewLine);
    if (!allowComments) {
        tx.Transformer.EmitContext().AddEmitFlags(statement, printer.EFNoComments);
    }
    return statement;
}

internal static ж<astꓸExpression> createExportExpression(this ref CommonJSModuleTransformer tx, ж<astꓸModuleExportName> Ꮡname, ж<astꓸExpression> value, ж<core.TextRange> Ꮡlocation, bool liveBinding) {
    ref var name = ref Ꮡname.DerefOrNull();
    ref var location = ref Ꮡlocation.DerefOrNull();

    ж<astꓸExpression> expression = default;
    if (liveBinding) {
        expression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object"),
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("defineProperty"),
                ast.NodeFlagsNone),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                tx.Transformer.Factory().NewStringLiteralFromNode(Ꮡname),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(
                            nil,
                            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("enumerable"),
                            nil,
                            nil,
                            tx.Transformer.Factory().NewTrueExpression()),
                        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(
                            nil,
                            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("get"),
                            nil,
                            nil,
                            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionExpression(
                                nil,
                                nil,
                                nil,
                                nil,
                                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
                                nil,
                                nil,
                                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(
                                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                                        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(value)
                                    ])),
                                    false)))
                    ])),
                    false)
            ])),
            ast.NodeFlagsNone);
    } else {
        ж<astꓸExpression> left = default;
        if (name.Kind == ast.KindStringLiteral) {
            left = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                nil,
                tx.Transformer.Factory().NewStringLiteralFromNode(Ꮡname),
                ast.NodeFlagsNone);
        } else {
            left = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                nil,
                name.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                ast.NodeFlagsNone);
        }
        expression = tx.Transformer.Factory().NewAssignmentExpression(left, value);
    }
    if (Ꮡlocation != nil) {
        tx.Transformer.EmitContext().SetCommentRange(expression, location);
    }
    return expression;
}

internal static ж<ast.Node> createRequireCall(this ref CommonJSModuleTransformer tx, ж<ast.Node> node) {
    slice<ж<astꓸExpression>> args = default;
    var moduleName = getExternalModuleNameLiteral(tx.Transformer.Factory(), node, ref (tx.currentSourceFile).DerefOrNull(), default, default, ref (tx.compilerOptions).DerefOrNull());
    if (moduleName != nil) {
        args = append(args, rewriteModuleSpecifier(tx.Transformer.EmitContext(), moduleName, tx.compilerOptions));
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("require"),
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(args),
        ast.NodeFlagsNone);
}

internal static ж<astꓸExpression> getHelperExpressionForExport(this ref CommonJSModuleTransformer tx, ж<ast.ExportDeclaration> node, ж<astꓸExpression> innerExpr) {
    if (getExportNeedsImportStarHelper(node)) {
        return tx.Transformer.Visitor().VisitNode(tx.Transformer.Factory().NewImportStarHelper(innerExpr));
    }
    return innerExpr;
}

internal static ж<astꓸExpression> getHelperExpressionForImport(this ref CommonJSModuleTransformer tx, ж<ast.ImportDeclaration> node, ж<astꓸExpression> innerExpr) {
    if (getImportNeedsImportStarHelper(node)) {
        return tx.Transformer.Visitor().VisitNode(tx.Transformer.Factory().NewImportStarHelper(innerExpr));
    }
    if (getImportNeedsImportDefaultHelper(node)) {
        return tx.Transformer.Visitor().VisitNode(tx.Transformer.Factory().NewImportDefaultHelper(innerExpr));
    }
    return innerExpr;
}

internal static ж<ast.Node> visitTopLevelImportDeclaration(this ref CommonJSModuleTransformer tx, ж<ast.ImportDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.ImportClause == nil) {
        var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(tx.createRequireCall(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
        tx.Transformer.EmitContext().SetOriginal(statement, Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(statement, Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return statement;
    }

    slice<ж<astꓸStatement>> statements = default;
    slice<ж<astꓸVariableDeclarationNode>> variables = default;
    var namespaceDeclaration = ast.GetNamespaceDeclarationNode(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (namespaceDeclaration != nil && !ast.IsDefaultImport(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        variables = append(
            variables,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                namespaceDeclaration.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                nil,
                nil,
                tx.getHelperExpressionForImport(Ꮡnode, tx.createRequireCall(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))));
    } else {
        variables = append(
            variables,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
                nil,
                nil,
                tx.getHelperExpressionForImport(Ꮡnode, tx.createRequireCall(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))));

        if (namespaceDeclaration != nil && ast.IsDefaultImport(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            variables = append(
                variables,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                    namespaceDeclaration.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                    nil,
                    nil,
                    tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())));
        }
    }

    var varStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(variables),
            ast.NodeFlagsConst));

    tx.Transformer.EmitContext().SetOriginal(varStatement, Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(varStatement, Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    statements = append(statements, varStatement);
    statements = tx.appendExportsOfImportDeclaration(statements, Ꮡnode);
    return transformers.SingleOrMany(statements, tx.Transformer.Factory());
}

internal static ж<ast.Node> visitTopLevelImportEqualsDeclaration(this ref CommonJSModuleTransformer tx, ж<ast.ImportEqualsDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsExternalModuleImportEqualsDeclaration(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        throw panic("import= for internal module references should be handled in an earlier transformer.");
    }

    slice<ж<astꓸStatement>> statements = default;
    if (ast.HasSyntacticModifier(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport)) {
        var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
            tx.createExportExpression(
                node.Name(),
                tx.createRequireCall(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
                Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑLoc),
                false));

        tx.Transformer.EmitContext().SetOriginal(statement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(statement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        statements = append(statements, statement);
    } else {
        var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸVariableDeclarationNode>>([
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                        node.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                        nil,
                        nil,
                        tx.createRequireCall(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))
                ])),
                ast.NodeFlagsConst));
        tx.Transformer.EmitContext().SetOriginal(statement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(statement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        statements = append(statements, statement);
    }

    statements = tx.appendExportsOfDeclaration(statements, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil, false);
    return transformers.SingleOrMany(statements, tx.Transformer.Factory());
}

internal static ж<ast.Node> visitTopLevelExportDeclaration(this ref CommonJSModuleTransformer tx, ж<ast.ExportDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.ModuleSpecifier == nil) {
        return default;
    }

    var generatedName = tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.ExportClause != nil && ast.IsNamedExports(node.ExportClause)) {
        slice<ж<astꓸStatement>> statements = default;
        var varStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸVariableDeclarationNode>>([
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                        generatedName,
                        nil,
                        nil,
                        tx.createRequireCall(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))
                ])),
                ast.NodeFlagsNone));
        tx.Transformer.EmitContext().SetOriginal(varStatement, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(varStatement, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        statements = append(statements, varStatement);

        foreach (var (_, specifier) in range(node.ExportClause.Elements())) {
            var specifierName = specifier.PropertyNameOrName();
            var exportNeedsImportDefault = ast.ModuleExportNameIsDefault(specifierName);

            ж<ast.Node> target = default;
            if (exportNeedsImportDefault) {
                target = tx.Transformer.Factory().NewImportDefaultHelper(generatedName);
            } else {
                target = generatedName;
            }

            ж<ast.Node> exportName = default;
            if (ast.IsStringLiteral(specifier.Name())) {
                exportName = tx.Transformer.Factory().NewStringLiteralFromNode(specifier.Name());
            } else {
                exportName = tx.Transformer.Factory().GetExportName(specifier.AsNode());
            }

            ж<ast.Node> exportedValue = default;
            if (ast.IsStringLiteral(specifierName)) {
                exportedValue = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(target, nil, specifierName, ast.NodeFlagsNone);
            } else {
                exportedValue = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(target, nil, specifierName, ast.NodeFlagsNone);
            }
            var statementΔ1 = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                tx.createExportExpression(
                    exportName,
                    exportedValue,
                    nil,
                    true));
            tx.Transformer.EmitContext().SetOriginal(statementΔ1, specifier.AsNode());
            tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(statementΔ1, specifier.AsNode());
            statements = append(statements, statementΔ1);
        }

        return transformers.SingleOrMany(statements, tx.Transformer.Factory());
    }

    if (node.ExportClause != nil) {
        ж<ast.Node> exportName = default;
        if (ast.IsStringLiteral(node.ExportClause.Name())) {
            exportName = tx.Transformer.Factory().NewStringLiteralFromNode(node.ExportClause.Name());
        } else {
            exportName = node.ExportClause.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
        }
        var statementΔ2 = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
            tx.createExportExpression(
                exportName,
                tx.getHelperExpressionForExport(
                    Ꮡnode,
                    tx.createRequireCall(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())),
                nil,
                false));
        tx.Transformer.EmitContext().SetOriginal(statementΔ2, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(statementΔ2, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return statementΔ2;
    }

    var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
        tx.Transformer.Visitor().VisitNode(tx.Transformer.Factory().NewExportStarHelper(tx.createRequireCall(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"))));
    tx.Transformer.EmitContext().SetOriginal(statement, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(statement, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return statement;
}

internal static ж<ast.Node> visitTopLevelExportAssignment(this ref CommonJSModuleTransformer tx, ж<ast.ExportAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsExportEquals) {
        return default;
    }

    return tx.createExportStatement(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("default"),
        tx.Transformer.Visitor().VisitNode(node.Expression),
        Ꮡnode.of(ast.ExportAssignment.ᏑLoc),
        true,
        false);
}

internal static ж<ast.Node> visitTopLevelFunctionDeclaration(this ref CommonJSModuleTransformer tx, ж<ast.FunctionDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.HasSyntacticModifier(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(
            Ꮡnode,
            transformers.ExtractModifiers(tx.Transformer.EmitContext(), Ꮡnode.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers(), ~((ast.ModifierFlags)ast.ModifierFlagsExportDefault)),
            node.AsteriskToken,
            tx.Transformer.Factory().GetDeclarationName(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            tx.Transformer.Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Body));
    } else {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
}

internal static ж<ast.Node> visitTopLevelClassDeclaration(this ref CommonJSModuleTransformer tx, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    slice<ж<astꓸStatement>> statements = default;
    if (ast.HasSyntacticModifier(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport)) {
        statements = append(statements, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
            Ꮡnode,
            tx.Transformer.Visitor().VisitModifiers(transformers.ExtractModifiers(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), ~((ast.ModifierFlags)ast.ModifierFlagsExportDefault))),
            tx.Transformer.Factory().GetDeclarationName(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            tx.Transformer.Visitor().VisitNodes(node.HeritageClauses),
            tx.Transformer.Visitor().VisitNodes(node.Members)));
    } else {
        statements = append(statements, tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    statements = tx.appendExportsOfClassOrFunctionDeclaration(statements, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return transformers.SingleOrMany(statements, tx.Transformer.Factory());
}

internal static ж<ast.Node> visitTopLevelVariableStatement(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.VariableStatement> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    ref var statements = ref heap<slice<ж<astꓸStatement>>>(out var Ꮡstatements);
    if (ast.HasSyntacticModifier(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport)) {
        ref var variables = ref heap<slice<ж<astꓸVariableDeclarationNode>>>(out var Ꮡvariables);
        ref var expressions = ref heap<slice<ж<astꓸExpression>>>(out var Ꮡexpressions);
        ref var modifiers = ref heap<ж<ast.ModifierList>>(out var Ꮡmodifiers);

        void commitPendingVariables() {
            if (len(Ꮡvariables.ValueSlot) > 0) {
                var variableList = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(Ꮡvariables.ValueSlot);
                var statement = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableStatement(
                    Ꮡnode,
                    Ꮡmodifiers.ValueSlot,
                    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclarationList(
                        Ꮡnode.Value.DeclarationList.AsVariableDeclarationList(),
                        variableList,
                        Ꮡnode.Value.DeclarationList.Flags));
                if (len(Ꮡstatements.ValueSlot) > 0) {
                    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFNoComments);
                }
                Ꮡstatements.ValueSlot = append(Ꮡstatements.ValueSlot, statement);
                Ꮡvariables.ValueSlot = default;
            }
        }

        void commitPendingExpressions() {
            if (len(Ꮡexpressions.ValueSlot) > 0) {
                var statement = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().InlineExpressions(Ꮡexpressions.ValueSlot));
                Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AssignCommentAndSourceMapRanges(statement, Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                if (len(Ꮡstatements.ValueSlot) > 0) {
                    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFNoComments);
                }
                Ꮡstatements.ValueSlot = append(Ꮡstatements.ValueSlot, statement);
                Ꮡexpressions.ValueSlot = default;
            }
        }

        var commitPendingExpressionsʗ1 = commitPendingExpressions;
        void pushVariable(ж<astꓸVariableDeclarationNode> variable) {
            commitPendingExpressionsʗ1();
            Ꮡvariables.ValueSlot = append(Ꮡvariables.ValueSlot, variable);
        }

        var commitPendingVariablesʗ1 = commitPendingVariables;
        void pushExpression(ж<astꓸExpression> expression) {
            commitPendingVariablesʗ1();
            Ꮡexpressions.ValueSlot = append(Ꮡexpressions.ValueSlot, expression);
        }

        foreach (var (_, vᴛ1) in range(node.DeclarationList.AsVariableDeclarationList().Declarations.Nodes)) {
            var variable = vᴛ1;

            var v = variable.AsVariableDeclaration();

            if (ast.IsIdentifier(v.Name()) && transformers.IsLocalName(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), v.Name())) {
                if (modifiers == nil) {
                    modifiers = transformers.ExtractModifiers(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), Ꮡnode.of(ast.VariableStatement.ᏑModifiersBase).Modifiers(), ~((ast.ModifierFlags)ast.ModifierFlagsExportDefault));
                }

                if ((~v).Initializer != nil) {
                    variable = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(
                        v,
                        v.Name(),
                        nil,
                        nil,
                        tx.createExportExpression(
                            v.Name(),
                            Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode((~v).Initializer),
                            nil,
                            false));
                }

                pushVariable(variable);
            } else if ((~v).Initializer != nil && !ast.IsBindingPattern(v.Name()) && (ast.IsArrowFunction((~v).Initializer) || ast.IsFunctionExpression((~v).Initializer) || ast.IsClassExpression((~v).Initializer))) {
                pushVariable(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                    v.Name(),
                    v.ExclamationToken,
                    (~v).Type,
                    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode((~v).Initializer)));

                var propertyAccess = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                    nil,
                    v.Name(),
                    ast.NodeFlagsNone);
                Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AssignCommentAndSourceMapRanges(propertyAccess, v.Name());

                pushExpression(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewAssignmentExpression(
                    propertyAccess,
                    v.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory()))));
            } else if (ast.IsIdentifier(v.Name())) {
                var expression = Ꮡtx.transformInitializedVariable(v);
                if (expression != nil) {
                    pushExpression(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode(expression));
                }
            } else if (ast.IsBindingPattern(v.Name())) {
                var expression = Ꮡtx.transformInitializedVariable(v);
                if (expression != nil) {
                    pushExpression(expression);
                }
            } else {
                var expression = transformers.ConvertVariableDeclarationToAssignmentExpression(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), v);
                if (expression != nil) {
                    pushExpression(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode(expression));
                }
            }
        }

        commitPendingVariables();
        commitPendingExpressions();
        statements = tx.appendExportsOfVariableStatement(statements, Ꮡnode);
        return transformers.SingleOrMany(statements, Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory());
    }
    return tx.visitTopLevelNestedVariableStatement(Ꮡnode);
}

internal static ж<astꓸExpression> transformInitializedVariable(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.VariableDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if (node.Initializer == nil) {
            return default;
        }
        var name = node.Name();
        if (ast.IsBindingPattern(name)) {
            var assignment = transformers.ConvertVariableDeclarationToAssignmentExpression(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), Ꮡnode);
            var grandparentNode = tx.pushNode(assignment);
            defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);
            return Ꮡtx.visitDestructuringAssignment(assignment.AsBinaryExpression(), true);
        }
        var propertyAccess = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
            nil,
            name,
            ast.NodeFlagsNone);
        Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AssignCommentAndSourceMapRanges(propertyAccess, name);
        return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewAssignmentExpression(propertyAccess, node.Initializer);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitTopLevelNestedVariableStatement(this ref CommonJSModuleTransformer tx, ж<ast.VariableStatement> node) {
    slice<ж<astꓸStatement>> statements = default;
    statements = append(statements, tx.Transformer.Visitor().VisitEachChild(node.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    statements = tx.appendExportsOfVariableStatement(statements, node);
    return transformers.SingleOrMany(statements, tx.Transformer.Factory());
}

internal static ж<ast.Node> visitTopLevelNestedForStatement(this ref CommonJSModuleTransformer tx, ж<ast.ForStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Initializer != nil && ast.IsVariableDeclarationList(node.Initializer) && (ast.NodeFlags)(node.Initializer.Flags & ast.NodeFlagsBlockScoped) == 0) {
        var exportStatements = tx.appendExportsOfVariableDeclarationList(default, node.Initializer.AsVariableDeclarationList(), false);
        if (len(exportStatements) > 0) {
            slice<ж<astꓸStatement>> statements = default;
            var varDeclList = tx.discardedValueVisitor.VisitNode(node.Initializer);
            var varStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, varDeclList);
            statements = append(statements, varStatement);
            statements = appendꓸꓸꓸ(statements, exportStatements);

            var condition = tx.Transformer.Visitor().VisitNode(node.Condition);
            var incrementor = tx.discardedValueVisitor.VisitNode(node.Incrementor);
            var body = tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor);
            statements = append(statements, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForStatement(
                Ꮡnode,
                nil,
                condition,
                incrementor,
                body));
            return transformers.SingleOrMany(statements, tx.Transformer.Factory());
        }
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForStatement(
        Ꮡnode,
        tx.discardedValueVisitor.VisitNode(node.Initializer),
        tx.Transformer.Visitor().VisitNode(node.Condition),
        tx.discardedValueVisitor.VisitNode(node.Incrementor),
        tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor));
}

internal static ж<ast.Node> visitTopLevelNestedForInOrOfStatement(this ref CommonJSModuleTransformer tx, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsVariableDeclarationList(node.Initializer) && (ast.NodeFlags)(node.Initializer.Flags & ast.NodeFlagsBlockScoped) == 0) {
        var exportStatements = tx.appendExportsOfVariableDeclarationList(default, node.Initializer.AsVariableDeclarationList(), true);
        if (len(exportStatements) > 0) {
            var initializer = tx.discardedValueVisitor.VisitNode(node.Initializer);
            var expression = tx.Transformer.Visitor().VisitNode(node.Expression);
            var body = tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor);
            if (ast.IsBlock(body)) {
                var block = body.AsBlock();
                var bodyStatements = appendꓸꓸꓸ(exportStatements, (~block).Statements.Nodes);
                var bodyStatementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(bodyStatements);
                bodyStatementList.Loc = block.Value.Statements.Loc;
                body = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(block, bodyStatementList, block.MultiLine);
            } else {
                var bodyStatements = append(exportStatements, body);
                body = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(bodyStatements), true);
            }
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForInOrOfStatement(Ꮡnode, node.AwaitModifier, initializer, expression, body);
        }
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForInOrOfStatement(
        Ꮡnode,
        node.AwaitModifier,
        tx.discardedValueVisitor.VisitNode(node.Initializer),
        tx.Transformer.Visitor().VisitNode(node.Expression),
        tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor));
}

internal static ж<ast.Node> visitTopLevelNestedDoStatement(this ref CommonJSModuleTransformer tx, ж<ast.DoStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateDoStatement(
        Ꮡnode,
        tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor),
        tx.Transformer.Visitor().VisitNode(node.Expression));
}

internal static ж<ast.Node> visitTopLevelNestedWhileStatement(this ref CommonJSModuleTransformer tx, ж<ast.WhileStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateWhileStatement(
        Ꮡnode,
        tx.Transformer.Visitor().VisitNode(node.Expression),
        tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor));
}

internal static ж<ast.Node> visitTopLevelNestedLabeledStatement(this ref CommonJSModuleTransformer tx, ж<ast.LabeledStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var statement = tx.topLevelNestedVisitor.VisitEmbeddedStatement(node.Statement);
    if (statement == nil) {
        statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewEmptyStatement();
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateLabeledStatement(Ꮡnode, node.Label, statement);
}

internal static ж<ast.Node> visitTopLevelNestedWithStatement(this ref CommonJSModuleTransformer tx, ж<ast.WithStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateWithStatement(
        Ꮡnode,
        tx.Transformer.Visitor().VisitNode(node.Expression),
        tx.topLevelNestedVisitor.VisitEmbeddedStatement(node.Statement));
}

internal static ж<ast.Node> visitTopLevelNestedIfStatement(this ref CommonJSModuleTransformer tx, ж<ast.IfStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);
    var thenStatement = tx.topLevelNestedVisitor.VisitEmbeddedStatement(node.ThenStatement);
    if (thenStatement == nil) {
        thenStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false);
    }
    var elseStatement = tx.topLevelNestedVisitor.VisitEmbeddedStatement(node.ElseStatement);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateIfStatement(Ꮡnode, expression, thenStatement, elseStatement);
}

internal static ж<ast.Node> visitTopLevelNestedSwitchStatement(this ref CommonJSModuleTransformer tx, ж<ast.SwitchStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSwitchStatement(
        Ꮡnode,
        tx.Transformer.Visitor().VisitNode(node.Expression),
        tx.topLevelNestedVisitor.VisitNode(node.CaseBlock));
}

internal static ж<ast.Node> visitTopLevelNestedCaseBlock(this ref CommonJSModuleTransformer tx, ж<ast.CaseBlock> node) {
    return tx.topLevelNestedVisitor.VisitEachChild(node.of(ast.CaseBlock.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitTopLevelNestedCaseOrDefaultClause(this ref CommonJSModuleTransformer tx, ж<ast.CaseOrDefaultClause> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCaseOrDefaultClause(
        Ꮡnode,
        tx.Transformer.Visitor().VisitNode(node.Expression),
        tx.topLevelNestedVisitor.VisitNodes(node.Statements));
}

internal static ж<ast.Node> visitTopLevelNestedTryStatement(this ref CommonJSModuleTransformer tx, ж<ast.TryStatement> node) {
    return tx.topLevelNestedVisitor.VisitEachChild(node.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitTopLevelNestedCatchClause(this ref CommonJSModuleTransformer tx, ж<ast.CatchClause> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCatchClause(
        Ꮡnode,
        node.VariableDeclaration,
        tx.topLevelNestedVisitor.VisitNode(node.Block));
}

internal static ж<ast.Node> visitTopLevelNestedBlock(this ref CommonJSModuleTransformer tx, ж<ast.Block> node) {
    return tx.topLevelNestedVisitor.VisitEachChild(node.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitForStatement(this ref CommonJSModuleTransformer tx, ж<ast.ForStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForStatement(
        Ꮡnode,
        tx.discardedValueVisitor.VisitNode(node.Initializer),
        tx.Transformer.Visitor().VisitNode(node.Condition),
        tx.discardedValueVisitor.VisitNode(node.Incrementor),
        tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor));
}

internal static ж<ast.Node> visitForInOrOfStatement(this ref CommonJSModuleTransformer tx, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForInOrOfStatement(
        Ꮡnode,
        node.AwaitModifier,
        tx.discardedValueVisitor.VisitNode(node.Initializer),
        tx.Transformer.Visitor().VisitNode(node.Expression),
        tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.topLevelNestedVisitor));
}

internal static ж<ast.Node> visitExpressionStatement(this ref CommonJSModuleTransformer tx, ж<ast.ExpressionStatement> node) {
    return tx.discardedValueVisitor.VisitEachChild(node.of(ast.ExpressionStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitVoidExpression(this ref CommonJSModuleTransformer tx, ж<ast.VoidExpression> node) {
    return tx.discardedValueVisitor.VisitEachChild(node.of(ast.VoidExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitParenthesizedExpression(this ref CommonJSModuleTransformer tx, ж<ast.ParenthesizedExpression> Ꮡnode, bool resultIsDiscarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expression = core.IfElse(resultIsDiscarded, tx.discardedValueVisitor, tx.Transformer.Visitor()).VisitNode(node.Expression);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(Ꮡnode, expression);
}

internal static ж<ast.Node> visitPartiallyEmittedExpression(this ref CommonJSModuleTransformer tx, ж<ast.PartiallyEmittedExpression> Ꮡnode, bool resultIsDiscarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expression = core.IfElse(resultIsDiscarded, tx.discardedValueVisitor, tx.Transformer.Visitor()).VisitNode(node.Expression);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePartiallyEmittedExpression(Ꮡnode, expression);
}

internal static ж<ast.Node> visitBinaryExpression(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.BinaryExpression> node, bool resultIsDiscarded) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (ast.IsDestructuringAssignment(node.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return Ꮡtx.visitDestructuringAssignment(node, resultIsDiscarded);
    }

    if (ast.IsAssignmentExpression(node.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false)) {
        return tx.visitAssignmentExpression(node);
    }

    if (ast.IsCommaExpression(node.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return tx.visitCommaExpression(node, resultIsDiscarded);
    }

    return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitEachChild(node.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitAssignmentExpression(this ref CommonJSModuleTransformer tx, ж<ast.BinaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsIdentifier(node.Left) && (!transformers.IsGeneratedIdentifier(tx.Transformer.EmitContext(), node.Left) || isFileLevelReservedGeneratedIdentifier(tx.Transformer.EmitContext(), node.Left)) && !transformers.IsLocalName(tx.Transformer.EmitContext(), node.Left)) {
        var exportedNames = tx.getExports(node.Left);
        if (len(exportedNames) > 0) {
            var expression = tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            foreach (var (_, exportName) in range(exportedNames)) {
                expression = tx.createExportExpression(exportName, expression, Ꮡnode.of(ast.BinaryExpression.ᏑLoc), false);
            }
            return expression;
        }
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitDestructuringAssignment(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.BinaryExpression> Ꮡnode, bool valueIsDiscarded) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.destructuringNeedsFlattening(node.Left)) {
        return transformers.FlattenDestructuringAssignment(
            Ꮡtx.ᏑTransformer,
            Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(),
            !valueIsDiscarded,
            transformers.FlattenLevelAll,
            new Func<ж<astꓸIdentifierNode>, ж<astꓸExpression>, ж<core.TextRange>, ж<astꓸExpression>>(Ꮡtx.createAllExportExpressions));
    }
    return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static bool destructuringNeedsFlattening(this ref CommonJSModuleTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsObjectLiteralExpression(Ꮡnode)) {
        foreach (var (_, elem) in range(node.Properties())) {
            switch (elem.Kind) {
            case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
                if (tx.destructuringNeedsFlattening(elem.Initializer())) {
                    return true;
                }
                break;
            }
            case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
                if (tx.destructuringNeedsFlattening(elem.Name())) {
                    return true;
                }
                break;
            }
            case var ᴛ when ᴛ == ast.KindSpreadAssignment: {
                if (tx.destructuringNeedsFlattening(elem.Expression())) {
                    return true;
                }
                break;
            }
            case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
                return false;
            }}

        }
    } else if (ast.IsArrayLiteralExpression(Ꮡnode)) {
        foreach (var (_, elem) in range((~node.AsArrayLiteralExpression()).Elements.Nodes)) {
            if (ast.IsSpreadElement(elem)) {
                if (tx.destructuringNeedsFlattening(elem.Expression())) {
                    return true;
                }
            } else if (tx.destructuringNeedsFlattening(elem)) {
                return true;
            }
        }
    } else if (ast.IsIdentifier(Ꮡnode)) {
        var exportedNames = tx.getExports(Ꮡnode);
        if (transformers.IsExportName(tx.Transformer.EmitContext(), Ꮡnode)) {
            return len(exportedNames) > 1;
        }
        if (len(exportedNames) == 0) {
            return false;
        }
        if (len(exportedNames) == 1 && tx.isDirectExport(Ꮡnode) && exportedNames[0].Text() == node.Text()) {
            return false;
        }
        return true;
    }
    return false;
}

internal static ж<astꓸExpression> createAllExportExpressions(this ref CommonJSModuleTransformer tx, ж<astꓸIdentifierNode> Ꮡname, ж<astꓸExpression> value, ж<core.TextRange> location) {
    ref var name = ref Ꮡname.DerefOrNull();

    var exportedNames = tx.getExports(Ꮡname);
    if (len(exportedNames) > 0) {
        ж<astꓸExpression> expression = default;
        if (tx.isDirectExport(Ꮡname)) {
            var exportName = name.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
            tx.Transformer.EmitContext().AddEmitFlags(exportName, (printer.EmitFlags)(printer.EFNoComments | printer.EFNoSourceMap));
            var propertyAccess = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                nil,
                exportName,
                ast.NodeFlagsNone);
            tx.Transformer.EmitContext().AddEmitFlags(propertyAccess, printer.EFNoComments);
            expression = tx.Transformer.Factory().NewAssignmentExpression(propertyAccess, value);
            tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, Ꮡname);
        } else {
            expression = tx.Transformer.Factory().NewAssignmentExpression(Ꮡname, value);
        }
        foreach (var (_, exportName) in range(exportedNames)) {
            expression = tx.createExportExpression(exportName, expression, location, false);
        }
        return expression;
    }
    if (tx.isDirectExport(Ꮡname)) {
        var exportName = name.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
        tx.Transformer.EmitContext().AddEmitFlags(exportName, (printer.EmitFlags)(printer.EFNoComments | printer.EFNoSourceMap));
        var propertyAccess = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
            nil,
            exportName,
            ast.NodeFlagsNone);
        tx.Transformer.EmitContext().AddEmitFlags(propertyAccess, printer.EFNoComments);
        var result = tx.Transformer.Factory().NewAssignmentExpression(propertyAccess, value);
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(result, Ꮡname);
        return result;
    }
    return tx.Transformer.Factory().NewAssignmentExpression(Ꮡname, value);
}

internal static bool isDirectExport(this ref CommonJSModuleTransformer tx, ж<astꓸIdentifierNode> name) {
    var exportContainer = tx.resolver.GetReferencedExportContainer(tx.Transformer.EmitContext().MostOriginal(name), false);
    return exportContainer != nil && ast.IsSourceFile(exportContainer);
}

internal static ж<ast.Node> visitAssignmentProperty(this ref CommonJSModuleTransformer tx, ж<ast.PropertyAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAssignment(
        Ꮡnode,
        nil,
        tx.Transformer.Visitor().VisitNode(node.Name()),
        nil,
        nil,
        tx.assignmentPatternVisitor.VisitNode(node.Initializer));
}

internal static ж<ast.Node> visitShorthandAssignmentProperty(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.ShorthandPropertyAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var target = Ꮡtx.visitDestructuringAssignmentTargetNoStack(node.Name());
    if (ast.IsIdentifier(target)) {
        return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateShorthandPropertyAssignment(
            Ꮡnode,
            nil,
            target,
            nil,
            nil,
            node.EqualsToken,
            Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode(node.ObjectAssignmentInitializer));
    }
    if (node.ObjectAssignmentInitializer != nil) {
        var equalsToken = node.EqualsToken;
        if (equalsToken == nil) {
            equalsToken = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsToken);
        }
        target = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
            nil,
            target,
            nil,
            equalsToken,
            Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode(node.ObjectAssignmentInitializer));
    }
    var updated = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(
        nil,
        node.Name(),
        nil,
        nil,
        target);
    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().SetOriginal(updated, Ꮡnode.of(ast.ShorthandPropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext().AssignCommentAndSourceMapRanges(updated, Ꮡnode.of(ast.ShorthandPropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return updated;
}

internal static ж<ast.Node> visitAssignmentRestProperty(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.SpreadAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSpreadAssignment(
        Ꮡnode,
        Ꮡtx.visitDestructuringAssignmentTarget(node.Expression));
}

internal static ж<ast.Node> visitAssignmentRestElement(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.SpreadElement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSpreadElement(
        Ꮡnode,
        Ꮡtx.visitDestructuringAssignmentTarget(node.Expression));
}

internal static ж<ast.Node> visitAssignmentElement(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBinaryExpression(Ꮡnode)) {
        var n = node.AsBinaryExpression();
        if (n.OperatorToken.Kind == ast.KindEqualsToken) {
            return Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(
                n,
                nil,
                Ꮡtx.visitDestructuringAssignmentTarget(n.Left),
                nil,
                n.OperatorToken,
                Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Visitor().VisitNode(n.Right));
        }
    }

    return Ꮡtx.visitDestructuringAssignmentTargetNoStack(Ꮡnode);
}

internal static ж<ast.Node> visitDestructuringAssignmentTarget(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var grandparentNode = tx.pushNode(Ꮡnode);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindArrayLiteralExpression: {
            Ꮡnode = Ꮡtx.visitAssignmentPatternNoStack(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        default: {
            Ꮡnode = Ꮡtx.visitDestructuringAssignmentTargetNoStack(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
            break;
        }}

        return Ꮡnode;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitDestructuringAssignmentTargetNoStack(this ж<CommonJSModuleTransformer> Ꮡtx, ж<ast.Node> node) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (ast.IsIdentifier(node) && (!transformers.IsGeneratedIdentifier(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), node) || isFileLevelReservedGeneratedIdentifier(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), node)) && !transformers.IsLocalName(Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).EmitContext(), node)) {
        var expression = tx.visitExpressionIdentifier(node);
        var exportedNames = tx.getExports(node);
        if (len(exportedNames) > 0) {
            var value = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewUniqueNameEx("value", new printer.AutoGenerateOptions(
                Flags: printer.GeneratedIdentifierFlagsOptimistic
            ));
            expression = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().NewAssignmentExpression(expression, value);

            foreach (var (_, exportName) in range(exportedNames)) {
                expression = tx.createExportExpression(exportName, expression, nil, false);
            }

            var statement = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
            var statementList = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([statement]);
            var param = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(
                nil,
                nil,
                value,
                nil,
                nil,
                nil);
            var valueSetter = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSetAccessorDeclaration(
                nil,
                Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"),
                nil,
                Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([param]),
                nil,
                nil,
                Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(statementList, false));
            var propertyList = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([valueSetter]);
            expression = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(propertyList, false);
            expression = Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(expression, nil, Ꮡtx.of(CommonJSModuleTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), ast.NodeFlagsNone);
        }
        return expression;
    }

    return Ꮡtx.visitNoStack(node, false);
}

internal static ж<ast.Node> visitCommaExpression(this ref CommonJSModuleTransformer tx, ж<ast.BinaryExpression> Ꮡnode, bool resultIsDiscarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var left = tx.discardedValueVisitor.VisitNode(node.Left);
    var right = core.IfElse(resultIsDiscarded, tx.discardedValueVisitor, tx.Transformer.Visitor()).VisitNode(node.Right);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(Ꮡnode, nil, left, nil, node.OperatorToken, right);
}

internal static ж<ast.Node> visitPrefixUnaryExpression(this ref CommonJSModuleTransformer tx, ж<ast.PrefixUnaryExpression> Ꮡnode, bool resultIsDiscarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((node.Operator == ast.KindPlusPlusToken || node.Operator == ast.KindMinusMinusToken) && ast.IsIdentifier(node.Operand) && !transformers.IsLocalName(tx.Transformer.EmitContext(), node.Operand)) {
        var exportedNames = tx.getExports(node.Operand);
        if (len(exportedNames) > 0) {
            var expression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePrefixUnaryExpression(Ꮡnode, node.Operator, tx.Transformer.Visitor().VisitNode(node.Operand));
            foreach (var (_, exportName) in range(exportedNames)) {
                expression = tx.createExportExpression(exportName, expression, nil, false);
                tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            }
            return expression;
        }
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitPostfixUnaryExpression(this ref CommonJSModuleTransformer tx, ж<ast.PostfixUnaryExpression> Ꮡnode, bool resultIsDiscarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((node.Operator == ast.KindPlusPlusToken || node.Operator == ast.KindMinusMinusToken) && ast.IsIdentifier(node.Operand) && !transformers.IsLocalName(tx.Transformer.EmitContext(), node.Operand)) {
        var exportedNames = tx.getExports(node.Operand);
        if (len(exportedNames) > 0) {
            ж<astꓸIdentifierNode> temp = default;
            var expression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePostfixUnaryExpression(Ꮡnode, tx.Transformer.Visitor().VisitNode(node.Operand), node.Operator);
            if (!resultIsDiscarded) {
                temp = tx.Transformer.Factory().NewTempVariable();
                tx.Transformer.EmitContext().AddVariableDeclaration(temp);

                expression = tx.Transformer.Factory().NewAssignmentExpression(temp, expression);
                tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            }

            expression = tx.Transformer.Factory().NewCommaExpression(expression, node.Operand.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())));
            tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

            foreach (var (_, exportName) in range(exportedNames)) {
                expression = tx.createExportExpression(exportName, expression, nil, false);
                tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            }

            if (temp != nil) {
                expression = tx.Transformer.Factory().NewCommaExpression(expression, temp.AsNode());
                tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            }

            return expression;
        }
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitCallExpression(this ref CommonJSModuleTransformer tx, ж<ast.CallExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var needsRewrite = false;
    if (tx.compilerOptions.RewriteRelativeImportExtensions.IsTrue()) {
        if (ast.IsImportCall(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && len(node.Arguments.Nodes) > 0 || ast.IsInJSFile(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && ast.IsRequireCall(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false)) {
            needsRewrite = true;
        }
    }
    if (ast.IsImportCall(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && tx.shouldTransformImportCall()) {
        return tx.visitImportCallExpression(Ꮡnode, needsRewrite);
    }
    if (needsRewrite) {
        return tx.shimOrRewriteImportOrRequireCall(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).AsCallExpression());
    }
    if (ast.IsIdentifier(node.Expression)) {
        var expression = tx.visitExpressionIdentifier(node.Expression);
        var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(
            Ꮡnode,
            expression,
            node.QuestionDotToken,
            nil,
            tx.Transformer.Visitor().VisitNodes(node.Arguments),
            node.Flags);
        if (!ast.IsIdentifier(expression) && !transformers.IsHelperName(tx.Transformer.EmitContext(), node.Expression)) {
            tx.Transformer.EmitContext().AddEmitFlags(updated, printer.EFIndirectCall);
        }
        return updated;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static bool shouldTransformImportCall(this ref CommonJSModuleTransformer tx) {
    return ast.ShouldTransformImportCall(tx.currentSourceFile.FileName(), tx.compilerOptions, tx.getEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(tx.currentSourceFile)));
}

internal static ж<ast.Node> visitImportCallExpression(this ref CommonJSModuleTransformer tx, ж<ast.CallExpression> Ꮡnode, bool rewriteOrShim) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.moduleKind == core.ModuleKindNone && tx.languageVersion >= core.ScriptTargetES2020) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var externalModuleName = getExternalModuleNameLiteral(tx.Transformer.Factory(), Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ref (tx.currentSourceFile).DerefOrNull(), default, default, ref (tx.compilerOptions).DerefOrNull());
    var firstArgument = tx.Transformer.Visitor().VisitNode(core.FirstOrNil(node.Arguments.Nodes));

    ж<astꓸExpression> argument = default;
    if (externalModuleName != nil && (firstArgument == nil || !ast.IsStringLiteral(firstArgument) || firstArgument.Text() != externalModuleName.Text())) {
        argument = externalModuleName;
    } else if (firstArgument != nil && rewriteOrShim) {
        if (ast.IsStringLiteral(firstArgument)) {
            argument = rewriteModuleSpecifier(tx.Transformer.EmitContext(), firstArgument, tx.compilerOptions);
        } else {
            argument = tx.Transformer.Factory().NewRewriteRelativeImportExtensionsHelper(firstArgument, tx.compilerOptions.Jsx == core.JsxEmitPreserve);
        }
    } else {
        argument = firstArgument;
    }
    return tx.createImportCallExpressionCommonJS(argument);
}

internal static ж<astꓸExpression> createImportCallExpressionCommonJS(this ref CommonJSModuleTransformer tx, ж<astꓸExpression> arg) {
    var needSyncEval = arg != nil && !isSimpleInlineableExpression(arg);

    slice<ж<astꓸExpression>> promiseResolveArguments = default;
    if (needSyncEval) {
        promiseResolveArguments = slice<ж<astꓸExpression>>([
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewTemplateExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewTemplateHead("", "", ast.TokenFlagsNone),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸTemplateSpanNode>>([
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewTemplateSpan(arg, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewTemplateTail("", "", ast.TokenFlagsNone))
                ])))
        ]);
    }
    var promiseResolveCall = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Promise"),
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("resolve"),
            ast.NodeFlagsNone),
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(promiseResolveArguments),
        ast.NodeFlagsNone);

    slice<ж<astꓸExpression>> requireArguments = default;
    if (needSyncEval) {
        requireArguments = slice<ж<astꓸExpression>>([
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("s")
        ]);
    } else if (arg != nil) {
        requireArguments = [arg];
    }

    var requireCall = tx.Transformer.Factory().NewImportStarHelper(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("require"),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(requireArguments),
            ast.NodeFlagsNone));

    slice<ж<astꓸParameterDeclarationNode>> parameters = default;
    if (needSyncEval) {
        parameters = slice<ж<astꓸParameterDeclarationNode>>([
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("s"),
                nil,
                nil,
                nil)
        ]);
    }

    var function = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrowFunction(
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(parameters),
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsGreaterThanToken),
        requireCall);

    var downleveledImport = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            promiseResolveCall,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("then"),
            ast.NodeFlagsNone),
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([function]),
        ast.NodeFlagsNone);
    return downleveledImport;
}

internal static ж<ast.Node> shimOrRewriteImportOrRequireCall(this ref CommonJSModuleTransformer tx, ж<ast.CallExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);
    var argumentsList = node.Arguments;
    if (len(node.Arguments.Nodes) > 0) {
        var firstArgument = tx.Transformer.Visitor().VisitNode(node.Arguments.Nodes[0]);
        var firstArgumentChanged = false;
        if (ast.IsStringLiteralLike(firstArgument)) {
            var rewritten = rewriteModuleSpecifier(tx.Transformer.EmitContext(), firstArgument, tx.compilerOptions);
            firstArgumentChanged = rewritten != firstArgument;
            firstArgument = rewritten;
        } else {
            firstArgument = tx.Transformer.Factory().NewRewriteRelativeImportExtensionsHelper(firstArgument, tx.compilerOptions.Jsx == core.JsxEmitPreserve);
            firstArgumentChanged = true;
        }

        var (rest, restChanged) = tx.Transformer.Visitor().VisitSlice(node.Arguments.Nodes[1..]);
        if (firstArgumentChanged || restChanged) {
            var arguments = appendꓸꓸꓸ(slice<ж<astꓸExpression>>([firstArgument]), rest);
            argumentsList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(arguments);
            argumentsList.Loc = node.Arguments.Loc;
        }
    }

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(
        Ꮡnode,
        expression,
        node.QuestionDotToken,
        nil,
        argumentsList,
        node.Flags);
}

internal static ж<ast.Node> visitTaggedTemplateExpression(this ref CommonJSModuleTransformer tx, ж<ast.TaggedTemplateExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsIdentifier(node.Tag)) {
        var expression = tx.visitExpressionIdentifier(node.Tag);
        var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTaggedTemplateExpression(
            Ꮡnode,
            expression,
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Template),
            node.Flags);
        if (!ast.IsIdentifier(expression) && !transformers.IsHelperName(tx.Transformer.EmitContext(), node.Tag)) {
            tx.Transformer.EmitContext().AddEmitFlags(updated, printer.EFIndirectCall);
        }
        return updated;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitShorthandPropertyAssignment(this ref CommonJSModuleTransformer tx, ж<ast.ShorthandPropertyAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    var exportedOrImportedName = tx.visitExpressionIdentifier(name);
    if (exportedOrImportedName != name) {
        var expression = exportedOrImportedName;
        if (node.ObjectAssignmentInitializer != nil) {
            expression = tx.Transformer.Factory().NewAssignmentExpression(
                expression,
                tx.Transformer.Visitor().VisitNode(node.ObjectAssignmentInitializer));
        }
        var assignment = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, name, nil, nil, expression);
        assignment.Loc = node.Loc;
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(assignment, Ꮡnode.of(ast.ShorthandPropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return assignment;
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateShorthandPropertyAssignment(
        Ꮡnode,
        nil,
        exportedOrImportedName,
        nil,
        nil,
        node.EqualsToken,
        tx.Transformer.Visitor().VisitNode(node.ObjectAssignmentInitializer));
}

internal static ж<ast.Node> visitIdentifier(this ref CommonJSModuleTransformer tx, ж<astꓸIdentifierNode> node) {
    if (transformers.IsIdentifierReference(node, tx.parentNode)) {
        return tx.visitExpressionIdentifier(node);
    }
    return node;
}

internal static ж<ast.Node> visitExpressionIdentifier(this ref CommonJSModuleTransformer tx, ж<astꓸIdentifierNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var info = tx.Transformer.EmitContext().GetAutoGenerateInfo(Ꮡnode); if (!(info != nil && !info.Flags.HasAllowNameSubstitution()) && !transformers.IsHelperName(tx.Transformer.EmitContext(), Ꮡnode) && !transformers.IsLocalName(tx.Transformer.EmitContext(), Ꮡnode) && !isDeclarationNameOfEnumOrNamespace(tx.Transformer.EmitContext(), Ꮡnode)) {
        var exportContainer = tx.resolver.GetReferencedExportContainer(tx.Transformer.EmitContext().MostOriginal(Ꮡnode), transformers.IsExportName(tx.Transformer.EmitContext(), Ꮡnode));
        if (exportContainer != nil && ast.IsSourceFile(exportContainer)) {
            var reference = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                nil,
                node.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                ast.NodeFlagsNone);
            tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(reference, Ꮡnode);
            reference.Loc = node.Loc;
            return reference;
        }

        var importDeclaration = tx.resolver.GetReferencedImportDeclaration(tx.Transformer.EmitContext().MostOriginal(Ꮡnode));
        if (importDeclaration != nil) {
            if (ast.IsImportClause(importDeclaration)) {
                var reference = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                    tx.Transformer.Factory().NewGeneratedNameForNode(importDeclaration.Parent),
                    nil,
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("default"),
                    ast.NodeFlagsNone);
                tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(reference, Ꮡnode);
                reference.Loc = node.Loc;
                return reference;
            }
            if (ast.IsImportSpecifier(importDeclaration)) {
                var name = importDeclaration.AsImportSpecifier().of(ast.ImportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).PropertyNameOrName();
                var decl = ast.FindAncestor(importDeclaration, ast.IsImportDeclaration);
                var target = tx.Transformer.Factory().NewGeneratedNameForNode(core.Coalesce<ast.Node>(decl, importDeclaration));
                ж<ast.Node> reference = default;
                if (ast.IsStringLiteral(name)) {
                    reference = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(
                        target,
                        nil,
                        tx.Transformer.Factory().NewStringLiteralFromNode(name),
                        ast.NodeFlagsNone);
                } else {
                    var referenceName = name.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
                    tx.Transformer.EmitContext().AddEmitFlags(referenceName, (printer.EmitFlags)(printer.EFNoSourceMap | printer.EFNoComments));
                    reference = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                        target,
                        nil,
                        referenceName,
                        ast.NodeFlagsNone);
                }
                tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(reference, Ꮡnode);
                reference.Loc = node.Loc;
                return reference;
            }
        }
    }
    return Ꮡnode;
}

internal static slice<ж<astꓸModuleExportName>> getExports(this ref CommonJSModuleTransformer tx, ж<astꓸIdentifierNode> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();
    var bindingsᴷ = new stackAppend<ж<astꓸModuleExportName>>(moves: true);
    var exportedNamesᴷ = new stackAppend<ж<astꓸModuleExportName>>(moves: true);

    if (!transformers.IsGeneratedIdentifier(tx.Transformer.EmitContext(), Ꮡname)) {
        var importDeclaration = tx.resolver.GetReferencedImportDeclaration(tx.Transformer.EmitContext().MostOriginal(Ꮡname));
        if (importDeclaration != nil) {
            return tx.currentModuleInfo.ᏑexportedBindings.Get(importDeclaration);
        }

        ref var bindingsSet = ref heap(new collections.Set<ж<astꓸModuleExportName>>(), out var ᏑbindingsSet);
        slice<ж<astꓸModuleExportName>> bindings = default;
        var declarations = tx.resolver.GetReferencedValueDeclarations(tx.Transformer.EmitContext().MostOriginal(Ꮡname));
        if (declarations != nil) {
            foreach (var (_, declaration) in range(declarations)) {
                var exportedBindings = tx.currentModuleInfo.ᏑexportedBindings.Get(declaration);
                foreach (var (_, binding) in range(exportedBindings)) {
                    if (!ᏑbindingsSet.Has(binding)) {
                        bindingsSet.Add(binding);
                        bindings = append(bindingsᴷ, bindings, binding);
                    }
                }
            }
            bindings = move2heap(bindingsᴷ, bindings);
            return bindings;
        }
    } else if (isFileLevelReservedGeneratedIdentifier(tx.Transformer.EmitContext(), Ꮡname)) {
        var exportSpecifiers = tx.currentModuleInfo.ᏑexportSpecifiers.Get(name.Text());
        if (exportSpecifiers != nil) {
            slice<ж<astꓸModuleExportName>> exportedNames = default;
            foreach (var (_, exportSpecifier) in range(exportSpecifiers)) {
                exportedNames = append(exportedNamesᴷ, exportedNames, exportSpecifier.Name());
            }
            exportedNames = move2heap(exportedNamesᴷ, exportedNames);
            return exportedNames;
        }
    }
    return default;
}

} // end moduletransformsᴛCommonJSModuleTransformer
