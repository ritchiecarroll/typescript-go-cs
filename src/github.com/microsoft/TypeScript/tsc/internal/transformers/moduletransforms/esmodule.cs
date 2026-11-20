namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static moduletransforms_package;

partial class moduletransforms_package {

partial struct ESModuleTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal binder.ReferenceResolver resolver;
    internal Func<ast.HasFileName, core.ModuleKind> getEmitModuleFormatOfFile;
    internal ж<ast.SourceFile> currentSourceFile;
    internal ж<importRequireStatements> importRequireStatements;
    internal map<@string, ж<astꓸIdentifierNode>> helperNameSubstitutions;
}

partial struct importRequireStatements {
    internal slice<ж<astꓸStatement>> statements;
    internal ж<astꓸIdentifierNode> requireHelperName;
}

public static ж<transformers.Transformer> NewESModuleTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var compilerOptions = opts.CompilerOptions;
    var tx = Ꮡ(new ESModuleTransformer(compilerOptions: compilerOptions, resolver: opts.Resolver, getEmitModuleFormatOfFile: opts.GetEmitModuleFormatOfFile));
    return tx.of(ESModuleTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

} partial class moduletransformsᴛESModuleTransformer {

internal static ж<ast.Node> visit(this ref ESModuleTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        Ꮡnode = tx.visitSourceFile(node.AsSourceFile()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportDeclaration: {
        Ꮡnode = tx.visitImportDeclaration(node.AsImportDeclaration()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
        Ꮡnode = tx.visitImportEqualsDeclaration(node.AsImportEqualsDeclaration()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        Ꮡnode = tx.visitExportAssignment(node.AsExportAssignment()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportDeclaration: {
        Ꮡnode = tx.visitExportDeclaration(node.AsExportDeclaration()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        Ꮡnode = tx.visitCallExpression(node.AsCallExpression()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    default: {
        Ꮡnode = tx.Transformer.Visitor().VisitEachChild(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        break;
    }}

    return Ꮡnode;
}

internal static ж<ast.Node> visitSourceFile(this ref ESModuleTransformer tx, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsDeclarationFile || !(ast.IsExternalModule(Ꮡnode) || tx.compilerOptions.GetIsolatedModules())) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    tx.currentSourceFile = Ꮡnode;
    tx.importRequireStatements = default;

    var result = tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).AsSourceFile();
    tx.Transformer.EmitContext().AddEmitHelper(result.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.Transformer.EmitContext().ReadEmitHelpers().ꓸꓸꓸ);

    var externalHelpersImportDeclaration = createExternalHelpersImportDeclarationIfNeeded(tx.Transformer.EmitContext(), result, tx.compilerOptions, tx.getEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(Ꮡnode)), false, false, false);
    if (externalHelpersImportDeclaration != nil || tx.importRequireStatements != nil) {
        var (prologue, rest) = tx.Transformer.Factory().SplitStandardPrologue((~result).Statements.Nodes);
        (var custom, rest) = tx.Transformer.Factory().SplitCustomPrologue(rest);
        var statements = slices.Clone<slice<ж<astꓸStatement>>, ж<astꓸStatement>>(prologue);
        statements = appendꓸꓸꓸ(statements, custom);
        if (externalHelpersImportDeclaration != nil) {
            statements = append(statements, tx.Transformer.Visitor().VisitNode(externalHelpersImportDeclaration));
        }
        if (tx.importRequireStatements != nil) {
            statements = appendꓸꓸꓸ(statements, tx.importRequireStatements.statements);
        }
        statements = appendꓸꓸꓸ(statements, rest);
        var statementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
        statementList.Loc = result.Value.Statements.Loc;
        result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(result, statementList, node.EndOfFileToken).AsSourceFile();
    }

    if (ast.IsExternalModule(result) && tx.compilerOptions.GetEmitModuleKind() != core.ModuleKindPreserve && !core.Some<ж<ast.Node>>((~result).Statements.Nodes, ast.IsExternalModuleIndicator)) {
        var statements = slices.Clone<slice<ж<ast.Node>>, ж<ast.Node>>((~result).Statements.Nodes);
        statements = append(statements, createEmptyImports(tx.Transformer.Factory()));
        var statementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
        statementList.Loc = result.Value.Statements.Loc;
        result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(result, statementList, node.EndOfFileToken).AsSourceFile();
    }

    tx.importRequireStatements = default;
    tx.currentSourceFile = default;
    return result.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

internal static ж<ast.Node> visitImportDeclaration(this ref ESModuleTransformer tx, ж<ast.ImportDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!tx.compilerOptions.RewriteRelativeImportExtensions.IsTrue()) {
        return Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    var updatedModuleSpecifier = rewriteModuleSpecifier(tx.Transformer.EmitContext(), node.ModuleSpecifier, tx.compilerOptions);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateImportDeclaration(
        Ꮡnode,
        nil,
        tx.Transformer.Visitor().VisitNode(node.ImportClause),
        updatedModuleSpecifier,
        tx.Transformer.Visitor().VisitNode(node.Attributes));
}

internal static ж<ast.Node> visitImportEqualsDeclaration(this ref ESModuleTransformer tx, ж<ast.ImportEqualsDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.compilerOptions.GetEmitModuleKind() < core.ModuleKindNode16) {
        return default;
    }

    if (!ast.IsExternalModuleImportEqualsDeclaration(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        throw panic("import= for internal module references should be handled in an earlier transformer.");
    }

    var varStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                    node.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                    nil,
                    nil,
                    tx.createRequireCall(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))
            ])),
            ast.NodeFlagsConst));
    tx.Transformer.EmitContext().SetOriginal(varStatement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(varStatement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    slice<ж<astꓸStatement>> statements = default;
    statements = append(statements, varStatement);
    statements = tx.appendExportsOfImportEqualsDeclaration(statements, Ꮡnode);
    return transformers.SingleOrMany(statements, tx.Transformer.Factory());
}

internal static slice<ж<astꓸStatement>> appendExportsOfImportEqualsDeclaration(this ref ESModuleTransformer tx, slice<ж<astꓸStatement>> statements, ж<ast.ImportEqualsDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.HasSyntacticModifier(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport)) {
        statements = append(statements, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(
            nil,
            false,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(
                        false,
                        nil,
                        node.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())))
                ]))),
            nil,
            nil));
    }
    return statements;
}

internal static ж<ast.Node> visitExportAssignment(this ref ESModuleTransformer tx, ж<ast.ExportAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!node.IsExportEquals) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    if (tx.compilerOptions.GetEmitModuleKind() != core.ModuleKindPreserve) {
        return default;
    }
    var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
        tx.Transformer.Factory().NewAssignmentExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("module"),
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("exports"),
                ast.NodeFlagsNone),
            tx.Transformer.Visitor().VisitNode(node.Expression)));
    tx.Transformer.EmitContext().SetOriginal(statement, Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return statement;
}

internal static ж<ast.Node> visitExportDeclaration(this ref ESModuleTransformer tx, ж<ast.ExportDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.ModuleSpecifier == nil) {
        return Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    var updatedModuleSpecifier = rewriteModuleSpecifier(tx.Transformer.EmitContext(), node.ModuleSpecifier, tx.compilerOptions);
    if (tx.compilerOptions.Module > core.ModuleKindES2015 || node.ExportClause == nil || !ast.IsNamespaceExport(node.ExportClause)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExportDeclaration(
            Ꮡnode,
            nil,
            false,
            node.ExportClause,
            updatedModuleSpecifier,
            tx.Transformer.Visitor().VisitNode(node.Attributes));
    }

    var oldIdentifier = node.ExportClause.Name();
    var synthName = tx.Transformer.Factory().NewGeneratedNameForNode(oldIdentifier);
    var importDecl = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportDeclaration(
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportClause(
            ast.KindUnknown,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamespaceImport(synthName)),
        updatedModuleSpecifier,
        tx.Transformer.Visitor().VisitNode(node.Attributes));
    tx.Transformer.EmitContext().SetOriginal(importDecl, node.ExportClause);

    ж<ast.Node> exportDecl = default;
    if (ast.IsExportNamespaceAsDefaultDeclaration(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        exportDecl = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(nil, false, nil, synthName);
    } else {
        exportDecl = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(
            nil,
            false,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(false, synthName, oldIdentifier)
                ]))),
            nil,
            nil);
    }
    tx.Transformer.EmitContext().SetOriginal(exportDecl, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return transformers.SingleOrMany([importDecl, exportDecl], tx.Transformer.Factory());
}

internal static ж<ast.Node> visitCallExpression(this ref ESModuleTransformer tx, ж<ast.CallExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.compilerOptions.RewriteRelativeImportExtensions.IsTrue()) {
        if (ast.IsImportCall(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && len(node.Arguments.Nodes) > 0 || ast.IsInJSFile(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && ast.IsRequireCall(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false)) {
            return tx.visitImportOrRequireCall(Ꮡnode);
        }
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitImportOrRequireCall(this ref ESModuleTransformer tx, ж<ast.CallExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (len(node.Arguments.Nodes) == 0) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);

    ж<astꓸExpression> argument = default;
    if (ast.IsStringLiteralLike(node.Arguments.Nodes[0])) {
        argument = rewriteModuleSpecifier(tx.Transformer.EmitContext(), node.Arguments.Nodes[0], tx.compilerOptions);
    } else {
        argument = tx.Transformer.Factory().NewRewriteRelativeImportExtensionsHelper(node.Arguments.Nodes[0], tx.compilerOptions.Jsx == core.JsxEmitPreserve);
    }

    slice<ж<astꓸExpression>> arguments = default;
    arguments = append(arguments, argument);

    var (ᴛ1, ᴛ2) = tx.Transformer.Visitor().VisitSlice(node.Arguments.Nodes[1..]);
    var rest = core.FirstResult(ᴛ1, ᴛ2);
    arguments = appendꓸꓸꓸ(arguments, rest);

    var argumentList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(arguments);
    argumentList.Loc = node.Arguments.Loc;
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(
        Ꮡnode,
        expression,
        node.QuestionDotToken,
        nil,
        argumentList,
        node.Flags);
}

internal static ж<astꓸExpression> createRequireCall(this ref ESModuleTransformer tx, ж<ast.Node> node) {
    var moduleName = getExternalModuleNameLiteral(tx.Transformer.Factory(), node, ref (tx.currentSourceFile).DerefOrNull(), default, default, ref (tx.compilerOptions).DerefOrNull());

    slice<ж<astꓸExpression>> args = default;
    if (moduleName != nil) {
        args = append(args, rewriteModuleSpecifier(tx.Transformer.EmitContext(), moduleName, tx.compilerOptions));
    }

    if (tx.compilerOptions.GetEmitModuleKind() == core.ModuleKindPreserve) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("require"),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(args),
            ast.NodeFlagsNone);
    }

    if (tx.importRequireStatements == nil) {
        var createRequireName = tx.Transformer.Factory().NewUniqueNameEx("_createRequire", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));
        var importStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportDeclaration(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportClause(
                ast.KindUnknown,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedImports(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportSpecifier(
                            false,
                            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("createRequire"),
                            createRequireName)
                    ])))),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("module", ast.TokenFlagsNone),
            nil);
        tx.Transformer.EmitContext().AddEmitFlags(importStatement, printer.EFCustomPrologue);

        var requireHelperName = tx.Transformer.Factory().NewUniqueNameEx("__require", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)));
        var requireStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                        requireHelperName,
                        nil,
                        nil,
                        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
                            createRequireName.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                            nil,
                            nil,
                            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸExpression>>([
                                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewMetaProperty(ast.KindImportKeyword, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("meta")),
                                    nil,
                                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("url"),
                                    ast.NodeFlagsNone)
                            ])),
                            ast.NodeFlagsNone))
                ])),
                ast.NodeFlagsConst));
        tx.Transformer.EmitContext().AddEmitFlags(requireStatement, printer.EFCustomPrologue);
        tx.importRequireStatements = Ꮡ(new importRequireStatements(
            statements: [importStatement, requireStatement],
            requireHelperName: requireHelperName
        ));
    }

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        tx.importRequireStatements.requireHelperName.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(args),
        ast.NodeFlagsNone);
}

} // end moduletransformsᴛESModuleTransformer
