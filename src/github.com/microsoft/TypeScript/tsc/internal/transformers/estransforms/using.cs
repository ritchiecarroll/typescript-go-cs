namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static estransforms_package;

partial class estransforms_package {

partial struct usingDeclarationTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal map<@string, ж<astꓸExportSpecifierNode>> exportBindings;
    internal slice<@string> exportBindingNames;
    internal slice<ж<astꓸVariableDeclarationNode>> exportVars;
    internal ж<astꓸIdentifierNode> defaultExportBinding;
    internal ж<astꓸIdentifierNode> exportEqualsBinding;
}

internal static ж<transformers.Transformer> newUsingDeclarationTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var tx = Ꮡ(new usingDeclarationTransformer());
    return tx.of(usingDeclarationTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
}

partial struct usingKind /*num:nuint*/;

internal static usingKind usingKindNone => /* iota */ 0;
internal static usingKind usingKindSync => 1;
internal static usingKind usingKindAsync => 2;

} partial class estransformsᴛusingDeclarationTransformer {

internal static ж<ast.Node> visit(this ж<usingDeclarationTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsUsing) == 0) {
        return Ꮡnode;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        Ꮡnode = Ꮡtx.visitSourceFile(node.AsSourceFile()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindBlock: {
        Ꮡnode = Ꮡtx.visitBlock(node.AsBlock()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        Ꮡnode = tx.visitForStatement(node.AsForStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    case var ᴛ when ᴛ == ast.KindForOfStatement: {
        Ꮡnode = tx.visitForOfStatement(node.AsForInOrOfStatement()); node = ref Ꮡnode.DerefOrNull();
        break;
    }
    default: {
        Ꮡnode = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        break;
    }}

    return Ꮡnode;
}

internal static ж<ast.Node> visitSourceFile(this ж<usingDeclarationTransformer> Ꮡtx, ж<ast.SourceFile> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsDeclarationFile) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    ж<astꓸSourceFileNode> visited = default;
    usingKind usingKind = getUsingKindOfStatements(node.Statements.Nodes);
    if (usingKind != usingKindNone) {
        Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).EmitContext().StartVariableEnvironment();

        tx.exportBindings = new map<@string, ж<astꓸExportSpecifierNode>>();
        tx.exportVars = default;

        var (prologue, rest) = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().SplitStandardPrologue(node.Statements.Nodes);
        ref var topLevelStatements = ref heap<slice<ж<astꓸStatement>>>(out var ᏑtopLevelStatements);
        var (ᴛ1, ᴛ2) = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Visitor().VisitSlice(prologue);
        topLevelStatements = appendꓸꓸꓸ(topLevelStatements, core.FirstResult(ᴛ1, ᴛ2));

        nint pos = 0;
        while (pos < len(rest)) {
            var statement = rest[pos];
            if (getUsingKind(statement) != usingKindNone) {
                if (pos > 0) {
                    var (ᴛ3, ᴛ4) = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Visitor().VisitSlice(rest.slice(0, pos));
                    topLevelStatements = appendꓸꓸꓸ(topLevelStatements, core.FirstResult(ᴛ3, ᴛ4));
                }
                break;
            }
            pos++;
        }

        if (pos >= len(rest)) {
            throw panic("Should have encountered at least one 'using' statement.");
        }

        var envBinding = tx.createEnvBinding();
        var bodyStatements = Ꮡtx.transformUsingDeclarations(rest.slice(pos), envBinding, ᏑtopLevelStatements);

        if (len(tx.exportBindings) > 0) {
            var exportSpecifiers = new slice<ж<astꓸExportSpecifierNode>>(0, len(tx.exportBindingNames));
            foreach (var (_, name) in range(tx.exportBindingNames)) {
                var specifier = tx.exportBindings[name];
                debug.Assert(specifier != nil, "Missing export binding for hoisted export name");
                exportSpecifiers = append(exportSpecifiers, specifier);
            }
            topLevelStatements = append(
                topLevelStatements,
                Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(
                    nil,
                    false,
                    Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(
                        Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(
                            exportSpecifiers)),
                    nil,
                    nil));
        }

        topLevelStatements = appendꓸꓸꓸ(topLevelStatements, Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).EmitContext().EndVariableEnvironment());
        if (len(tx.exportVars) > 0) {
            topLevelStatements = append(topLevelStatements, Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
                Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(slice<ж<ast.Node>>([
                    Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifier(ast.KindExportKeyword)
                ])),
                Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                    Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(tx.exportVars),
                    ast.NodeFlagsLet)));
        }
        topLevelStatements = appendꓸꓸꓸ(topLevelStatements, tx.createDownlevelUsingStatements(bodyStatements, envBinding, usingKind == usingKindAsync));

        if (tx.exportEqualsBinding != nil) {
            topLevelStatements = append(topLevelStatements, Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(
                nil,
                true,
                nil,
                tx.exportEqualsBinding));
        }

        visited = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(Ꮡnode, Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(topLevelStatements), node.EndOfFileToken);
    } else {
        visited = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).EmitContext().AddEmitHelper(visited, Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    tx.exportVars = default;
    tx.exportBindings = default;
    tx.exportBindingNames = default;
    tx.defaultExportBinding = default;
    tx.exportEqualsBinding = default;
    return visited;
}

internal static ж<ast.Node> visitBlock(this ж<usingDeclarationTransformer> Ꮡtx, ж<ast.Block> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    usingKind usingKind = getUsingKindOfStatements(node.Statements.Nodes);
    if (usingKind != usingKindNone) {
        var (prologue, rest) = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().SplitStandardPrologue(node.Statements.Nodes);
        var envBinding = tx.createEnvBinding();
        var statements = new slice<ж<astꓸStatement>>(0, len(prologue) + 2);
        var (ᴛ5, ᴛ6) = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Visitor().VisitSlice(prologue);
        statements = appendꓸꓸꓸ(statements, core.FirstResult(ᴛ5, ᴛ6));
        statements = appendꓸꓸꓸ(statements, tx.createDownlevelUsingStatements(
            Ꮡtx.transformUsingDeclarations(rest, envBinding, nil),
            envBinding,
            usingKind == usingKindAsync));
        var statementList = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
        statementList.Loc = node.Statements.Loc;
        return Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(Ꮡnode, statementList, node.MultiLine);
    }
    return Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitForStatement(this ref usingDeclarationTransformer tx, ж<ast.ForStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Initializer != nil && isUsingVariableDeclarationList(node.Initializer)) {
        return tx.Transformer.Visitor().VisitNode(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸStatement>>([
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, node.Initializer),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForStatement(
                    Ꮡnode,
                    nil,
                    node.Condition,
                    node.Incrementor,
                    node.Statement)
            ])), false));
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitForOfStatement(this ref usingDeclarationTransformer tx, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isUsingVariableDeclarationList(node.Initializer)) {
        var forInitializer = node.Initializer.AsVariableDeclarationList();
        var forDecl = core.FirstOrNil(forInitializer.Declarations.Nodes);
        if (forDecl == nil) {
            forDecl = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(tx.Transformer.Factory().NewTempVariable(), nil, nil, nil);
        }

        var isAwaitUsing = getUsingKindOfVariableDeclarationList(ref (forInitializer).DerefOrNull()) == usingKindAsync;
        var temp = tx.Transformer.Factory().NewGeneratedNameForNode(forDecl.Name());
        var usingVar = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(forDecl.AsVariableDeclaration(), forDecl.Name(), nil, nil, temp);
        var usingVarList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([usingVar]),
            core.IfElse(isAwaitUsing, ast.NodeFlagsAwaitUsing, ast.NodeFlagsUsing));
        var usingVarStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, usingVarList);
        ж<astꓸStatement> statement = default;
        if (ast.IsBlock(node.Statement)) {
            var statements = new slice<ж<astꓸStatement>>(0, len(node.Statement.Statements()) + 1);
            statements = append(statements, usingVarStatement);
            statements = appendꓸꓸꓸ(statements, node.Statement.Statements());
            statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(
                node.Statement.AsBlock(),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements),
                node.Statement.AsBlock().MultiLine);
        } else {
            statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([
                    usingVarStatement,
                    node.Statement
                ]),
                true);
        }
        return tx.Transformer.Visitor().VisitNode(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForInOrOfStatement(
                Ꮡnode,
                node.AwaitModifier,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸVariableDeclarationNode>>([
                        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(temp, nil, nil, nil)
                    ])),
                    ast.NodeFlagsConst),
                node.Expression,
                statement));
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static slice<ж<ast.Node>> transformUsingDeclarations(this ж<usingDeclarationTransformer> Ꮡtx, slice<ж<astꓸStatement>> statementsIn, ж<astꓸIdentifierNode> envBinding, ж<slice<ж<astꓸStatement>>> topLevelStatements) {
    ref var statements = ref heap<slice<ж<astꓸStatement>>>(out var Ꮡstatements);

    ж<astꓸStatement> hoist(ж<astꓸStatement> node) {
        if (topLevelStatements == nil) {
            return node;
        }

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindExportDeclaration || ᴛ == ast.KindFunctionDeclaration: {
            Ꮡtx.Value.hoistImportOrExportOrHoistedDeclaration(node, topLevelStatements);
            return default;
        }
        case var ᴛ when ᴛ == ast.KindExportAssignment: {
            return Ꮡtx.Value.hoistExportAssignment(node.AsExportAssignment());
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            return Ꮡtx.Value.hoistClassDeclaration(node.AsClassDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindVariableStatement: {
            return Ꮡtx.Value.hoistVariableStatement(node.AsVariableStatement());
        }}

        return node;
    }

    var hoistʗ1 = hoist;
    void hoistOrAppendNode(ж<ast.Node> node) {
        node = hoistʗ1(node);
        if (node != nil) {
            Ꮡstatements.ValueSlot = append(Ꮡstatements.ValueSlot, node);
        }
    }

    foreach (var (_, statement) in range(statementsIn)) {
        usingKind usingKind = getUsingKind(statement);
        if (usingKind != usingKindNone) {
            var varStatement = statement.AsVariableStatement();
            var declarationList = varStatement.DeclarationList;
            slice<ж<astꓸVariableDeclarationNode>> declarations = default;
            foreach (var (_, vᴛ1) in range(declarationList.AsVariableDeclarationList().Declarations.Nodes)) {
                var declaration = vᴛ1;

                if (!ast.IsIdentifier(declaration.Name())) {
                    declarations = default;
                    break;
                }

                if (isNamedEvaluation(Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).EmitContext(), declaration)) {
                    declaration = transformNamedEvaluation(Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).EmitContext(), declaration, false, "");
                }

                var initializer = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Visitor().VisitNode(declaration.Initializer());
                if (initializer == nil) {
                    initializer = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().NewVoidZeroExpression();
                }
                declarations = append(declarations, Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(
                    declaration.AsVariableDeclaration(),
                    declaration.Name(),
                    nil,
                    nil,
                    Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().NewAddDisposableResourceHelper(
                        envBinding,
                        initializer,
                        usingKind == usingKindAsync)));
            }

            if (len(declarations) > 0) {
                var varList = Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(declarations), ast.NodeFlagsConst);
                Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).EmitContext().SetOriginal(varList, declarationList);
                varList.Loc = declarationList.Loc;
                hoistOrAppendNode(Ꮡtx.of(usingDeclarationTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableStatement(varStatement, nil, varList));
                continue;
            }
        }

        var result = Ꮡtx.visit(statement); if (result != nil) {
            if (result.Kind == ast.KindSyntaxList) {
                foreach (var (_, node) in range((~result.AsSyntaxList()).Children)) {
                    hoistOrAppendNode(node);
                }
            } else {
                hoistOrAppendNode(result);
            }
        }
    }
    return statements;
}

internal static void hoistImportOrExportOrHoistedDeclaration(this ref usingDeclarationTransformer tx, ж<astꓸStatement> node, ж<slice<ж<astꓸStatement>>> ᏑtopLevelStatements) {
    ref var topLevelStatements = ref ᏑtopLevelStatements.DerefOrNull();

    topLevelStatements = append(topLevelStatements, node);
}

internal static ж<astꓸStatement> hoistExportAssignment(this ref usingDeclarationTransformer tx, ж<ast.ExportAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsExportEquals) {
        return tx.hoistExportEquals(Ꮡnode);
    } else {
        return tx.hoistExportDefault(Ꮡnode);
    }
}

internal static ж<astꓸStatement> hoistExportDefault(this ref usingDeclarationTransformer tx, ж<ast.ExportAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.defaultExportBinding != nil) {
        return Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    tx.defaultExportBinding = tx.Transformer.Factory().NewUniqueNameEx("_default", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((UntypedInt)(printer.GeneratedIdentifierFlagsReservedInNestedScopes | printer.GeneratedIdentifierFlagsFileLevel) | (nint)printer.GeneratedIdentifierFlagsOptimistic)));
    tx.hoistBindingIdentifier(tx.defaultExportBinding, true, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("default"), Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    var expression = node.Expression;
    var innerExpression = ast.SkipOuterExpressions(expression, ast.OEKAll);
    if (isNamedEvaluation(tx.Transformer.EmitContext(), innerExpression)) {
        innerExpression = transformNamedEvaluation(tx.Transformer.EmitContext(), innerExpression, false, "default");
        expression = tx.Transformer.Factory().RestoreOuterExpressions(expression, innerExpression, ast.OEKAll);
    }

    var assignment = tx.Transformer.Factory().NewAssignmentExpression(tx.defaultExportBinding, expression);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(assignment);
}

internal static ж<astꓸStatement> hoistExportEquals(this ref usingDeclarationTransformer tx, ж<ast.ExportAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.exportEqualsBinding != nil) {
        return Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    tx.exportEqualsBinding = tx.Transformer.Factory().NewUniqueNameEx("_default", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((UntypedInt)(printer.GeneratedIdentifierFlagsReservedInNestedScopes | printer.GeneratedIdentifierFlagsFileLevel) | (nint)printer.GeneratedIdentifierFlagsOptimistic)));
    tx.Transformer.EmitContext().AddVariableDeclaration(tx.exportEqualsBinding);

    var assignment = tx.Transformer.Factory().NewAssignmentExpression(tx.exportEqualsBinding, node.Expression);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(assignment);
}

internal static ж<astꓸStatement> hoistClassDeclaration(this ref usingDeclarationTransformer tx, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Name() == nil && tx.defaultExportBinding != nil) {
        return Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    var isExported = ast.HasSyntacticModifier(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport);
    var isDefault = ast.HasSyntacticModifier(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsDefault);

    var expression = convertClassDeclarationToClassExpression(tx.Transformer.EmitContext(), Ꮡnode);
    if (node.Name() != nil) {
        tx.hoistBindingIdentifier(tx.Transformer.Factory().GetLocalName(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), isExported && !isDefault, nil, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        expression = tx.Transformer.Factory().NewAssignmentExpression(tx.Transformer.Factory().GetDeclarationName(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), expression);
        tx.Transformer.EmitContext().SetOriginal(expression, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().SetSourceMapRange(expression, node.Loc);
        tx.Transformer.EmitContext().SetCommentRange(expression, node.Loc);
        if (isNamedEvaluation(tx.Transformer.EmitContext(), expression)) {
            expression = transformNamedEvaluation(tx.Transformer.EmitContext(), expression, false, "");
        }
    }

    if (isDefault && tx.defaultExportBinding == nil) {
        tx.defaultExportBinding = tx.Transformer.Factory().NewUniqueNameEx("_default", new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((UntypedInt)(printer.GeneratedIdentifierFlagsReservedInNestedScopes | printer.GeneratedIdentifierFlagsFileLevel) | (nint)printer.GeneratedIdentifierFlagsOptimistic)));
        tx.hoistBindingIdentifier(tx.defaultExportBinding, true, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("default"), Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        expression = tx.Transformer.Factory().NewAssignmentExpression(tx.defaultExportBinding, expression);
        tx.Transformer.EmitContext().SetOriginal(expression, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (isNamedEvaluation(tx.Transformer.EmitContext(), expression)) {
            expression = transformNamedEvaluation(tx.Transformer.EmitContext(), expression, false, "default");
        }
    }

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
}

internal static ж<astꓸStatement> hoistVariableStatement(this ref usingDeclarationTransformer tx, ж<ast.VariableStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();
    var expressionsᴷ = new stackAppend<ж<astꓸExpression>>();

    slice<ж<astꓸExpression>> expressions = default;
    var isExported = ast.HasSyntacticModifier(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport);
    foreach (var (_, variable) in range(node.DeclarationList.AsVariableDeclarationList().Declarations.Nodes)) {
        tx.hoistBindingElement(variable, isExported, variable);
        if (variable.Initializer() != nil) {
            expressions = append(expressionsᴷ, expressions, tx.hoistInitializedVariable(variable.AsVariableDeclaration()));
        }
    }
    if (len(expressions) > 0) {
        var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(tx.Transformer.Factory().InlineExpressions(expressions));
        tx.Transformer.EmitContext().SetOriginal(statement, Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().SetCommentRange(statement, node.Loc);
        tx.Transformer.EmitContext().SetSourceMapRange(statement, node.Loc);
        return statement;
    }
    return default;
}

internal static ж<astꓸExpression> hoistInitializedVariable(this ref usingDeclarationTransformer tx, ж<ast.VariableDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Initializer == nil) {
        throw panic("Expected initializer");
    }
    ж<astꓸExpression> target = default;
    if (ast.IsIdentifier(node.Name())) {
        target = node.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
        tx.Transformer.EmitContext().SetEmitFlags(target, (printer.EmitFlags)(tx.Transformer.EmitContext().EmitFlags(target) & ~((printer.EmitFlags)((printer.EmitFlags)(printer.EFLocalName | printer.EFExportName)))));
    } else {
        target = transformers.ConvertBindingPatternToAssignmentPattern(tx.Transformer.EmitContext(), node.Name().AsBindingPattern());
    }

    var assignment = tx.Transformer.Factory().NewAssignmentExpression(target, node.Initializer);
    tx.Transformer.EmitContext().SetOriginal(assignment, Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().SetCommentRange(assignment, node.Loc);
    tx.Transformer.EmitContext().SetSourceMapRange(assignment, node.Loc);
    return assignment;
}

internal static void hoistBindingElement(this ref usingDeclarationTransformer tx, ж<ast.Node> Ꮡnode, bool isExportedDeclaration, ж<ast.Node> original) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBindingPattern(node.Name())) {
        foreach (var (_, element) in range(node.Name().Elements())) {
            if (element.Name() != nil) {
                tx.hoistBindingElement(element, isExportedDeclaration, original);
            }
        }
    } else {
        tx.hoistBindingIdentifier(node.Name(), isExportedDeclaration, nil, original);
    }
}

internal static void hoistBindingIdentifier(this ref usingDeclarationTransformer tx, ж<astꓸIdentifierNode> node, bool isExport, ж<astꓸIdentifierNode> exportAlias, ж<ast.Node> original) {
    var name = node;
    if (!transformers.IsGeneratedIdentifier(tx.Transformer.EmitContext(), node)) {
        name = name.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
    }
    if (isExport) {
        if (exportAlias == nil && !transformers.IsLocalName(tx.Transformer.EmitContext(), name)) {
            var varDecl = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(name, nil, nil, nil);
            if (original != nil) {
                tx.Transformer.EmitContext().SetOriginal(varDecl, original);
            }
            tx.exportVars = append(tx.exportVars, varDecl);
            return;
        }

        ж<astꓸModuleExportName> localName = default;
        ж<astꓸModuleExportName> exportName = default;
        if (exportAlias != nil) {
            localName = name;
            exportName = exportAlias;
        } else {
            exportName = name;
        }
        var specifier = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportSpecifier(false, localName, exportName);
        if (original != nil) {
            tx.Transformer.EmitContext().SetOriginal(specifier, original);
        }
        if (tx.exportBindings == nil) {
            tx.exportBindings = new map<@string, ж<astꓸExportSpecifierNode>>();
        }
        var (_, ok) = tx.exportBindings[name.Text(), ꟷ]; if (!ok) {
            tx.exportBindingNames = append(tx.exportBindingNames, name.Text());
        }
        tx.exportBindings[name.Text()] = specifier;
    }
    tx.Transformer.EmitContext().AddVariableDeclaration(name);
}

internal static ж<astꓸIdentifierNode> createEnvBinding(this ref usingDeclarationTransformer tx) {
    return tx.Transformer.Factory().NewUniqueName("env");
}

internal static slice<ж<astꓸStatement>> createDownlevelUsingStatements(this ref usingDeclarationTransformer tx, slice<ж<ast.Node>> bodyStatements, ж<astꓸIdentifierNode> envBinding, bool async) {
    var statements = new slice<ж<astꓸStatement>>(0, 2);

    var envObject = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸExpression>>([
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("stack"), nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(nil, false)),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("error"), nil, nil, tx.Transformer.Factory().NewVoidZeroExpression()),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("hasError"), nil, nil, tx.Transformer.Factory().NewFalseExpression())
    ])), false);
    var envVar = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(envBinding, nil, nil, envObject);
    var envVarList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([envVar]), ast.NodeFlagsConst);
    var envVarStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, envVarList);
    statements = append(statements, envVarStatement);

    var tryBlock = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(bodyStatements), true);
    var bodyCatchBinding = tx.Transformer.Factory().NewUniqueName("e");
    var catchClause = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCatchClause(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
            bodyCatchBinding,
            nil,
            nil,
            nil),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸStatement>>([
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                tx.Transformer.Factory().NewAssignmentExpression(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(envBinding, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("error"), ast.NodeFlagsNone),
                    bodyCatchBinding)),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                tx.Transformer.Factory().NewAssignmentExpression(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(envBinding, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("hasError"), ast.NodeFlagsNone),
                    tx.Transformer.Factory().NewTrueExpression()))
        ])), true));

    ж<astꓸBlockNode> finallyBlock = default;
    if (async) {
        var result = tx.Transformer.Factory().NewUniqueName("result");
        finallyBlock = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸStatement>>([
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸVariableDeclarationNode>>([
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                        result,
                        nil,
                        nil,
                        tx.Transformer.Factory().NewDisposeResourcesHelper(envBinding))
                ])), ast.NodeFlagsConst)),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIfStatement(result, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewAwaitExpression(result)), nil)
        ])), true);
    } else {
        finallyBlock = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<astꓸStatement>>([
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                tx.Transformer.Factory().NewDisposeResourcesHelper(envBinding))
        ])), true);
    }

    var tryStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewTryStatement(tryBlock, catchClause, finallyBlock);
    statements = append(statements, tryStatement);
    return statements;
}

} partial class estransforms_package {

internal static bool isUsingVariableDeclarationList(ж<astꓸForInitializer> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsVariableDeclarationList(Ꮡnode) && getUsingKindOfVariableDeclarationList(ref (node.AsVariableDeclarationList()).DerefOrNull()) != usingKindNone;
}

internal static usingKind getUsingKindOfVariableDeclarationList(ref ast.VariableDeclarationList node) {
    switch ((ast.NodeFlags)(node.Flags & ast.NodeFlagsBlockScoped)) {
    case var ᴛ when ᴛ == ast.NodeFlagsAwaitUsing: {
        return usingKindAsync;
    }
    case var ᴛ when ᴛ == ast.NodeFlagsUsing: {
        return usingKindSync;
    }
    default: {
        return usingKindNone;
    }}

}

internal static usingKind getUsingKindOfVariableStatement(ref ast.VariableStatement node) {
    return getUsingKindOfVariableDeclarationList(ref (node.DeclarationList.AsVariableDeclarationList()).DerefOrNull());
}

internal static usingKind getUsingKind(ж<ast.Node> Ꮡstatement) {
    ref var statement = ref Ꮡstatement.DerefOrNull();

    if (ast.IsVariableStatement(Ꮡstatement)) {
        return getUsingKindOfVariableStatement(ref (statement.AsVariableStatement()).DerefOrNull());
    }
    return usingKindNone;
}

internal static usingKind getUsingKindOfStatements(slice<ж<ast.Node>> statements) {
    usingKind result = usingKindNone;
    foreach (var (_, statement) in range(statements)) {
        usingKind usingKind = getUsingKind(statement);
        if (usingKind == usingKindAsync) {
            return usingKindAsync;
        }
        if (usingKind > result) {
            result = usingKind;
        }
    }
    return result;
}

} // end estransforms_package
