namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tstransforms_package;

partial class tstransforms_package {

partial struct RuntimeSyntaxTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ж<ast.Node> parentNode;
    internal ж<ast.Node> currentNode;
    internal ж<ast.Node> currentSourceFile;
    internal ж<ast.Node> currentScope;
    internal map<@string, ж<ast.Node>> currentScopeFirstDeclarationsOfName;
    internal ж<astꓸEnumDeclarationNode> currentEnum;
    internal ж<astꓸModuleDeclarationNode> currentNamespace;
    internal binder.ReferenceResolver resolver;
    internal printer.EmitResolver emitResolver;
}

public static ж<transformers.Transformer> NewRuntimeSyntaxTransformer(ж<transformers.TransformOptions> Ꮡopt) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    var compilerOptions = opt.CompilerOptions;
    var emitContext = opt.Context;
    var tx = Ꮡ(new RuntimeSyntaxTransformer(compilerOptions: compilerOptions, resolver: opt.Resolver, emitResolver: opt.EmitResolver));
    return tx.of(RuntimeSyntaxTransformer.ᏑTransformer).NewTransformer(tx.visit, emitContext);
}

} partial class tstransformsᴛRuntimeSyntaxTransformer {

internal static ж<ast.Node> /*grandparentNode*/ pushNode(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> node) {
    ж<ast.Node> grandparentNode = default;

    grandparentNode = tx.parentNode;
    tx.parentNode = tx.currentNode;
    tx.currentNode = node;
    return grandparentNode;
}

internal static void popNode(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> grandparentNode) {
    tx.currentNode = tx.parentNode;
    tx.parentNode = grandparentNode;
}

internal static (ж<ast.Node> savedCurrentScope, map<@string, ж<ast.Node>> savedCurrentScopeFirstDeclarationsOfName) pushScope(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> Ꮡnode) {
    ж<ast.Node> savedCurrentScope = default;
    map<@string, ж<ast.Node>> savedCurrentScopeFirstDeclarationsOfName = default;
    ref var node = ref Ꮡnode.DerefOrNull();

    savedCurrentScope = tx.currentScope;
    savedCurrentScopeFirstDeclarationsOfName = tx.currentScopeFirstDeclarationsOfName;
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        tx.currentScope = Ꮡnode;
        tx.currentSourceFile = Ꮡnode;
        tx.currentScopeFirstDeclarationsOfName = default;
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseBlock || ᴛ == ast.KindModuleBlock || ᴛ == ast.KindBlock: {
        tx.currentScope = Ꮡnode;
        tx.currentScopeFirstDeclarationsOfName = default;
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindVariableStatement: {
        tx.recordDeclarationInScope(Ꮡnode);
        break;
    }}

    return (savedCurrentScope, savedCurrentScopeFirstDeclarationsOfName);
}

internal static void popScope(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> savedCurrentScope, map<@string, ж<ast.Node>> savedCurrentScopeFirstDeclarationsOfName) {
    if (tx.currentScope != savedCurrentScope) {
        tx.currentScopeFirstDeclarationsOfName = savedCurrentScopeFirstDeclarationsOfName;
    }

    tx.currentScope = savedCurrentScope;
}

internal static ж<ast.Node> visit(this ж<RuntimeSyntaxTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var grandparentNode = tx.pushNode(Ꮡnode);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        var (savedCurrentScope, savedCurrentScopeFirstDeclarationsOfName) = tx.pushScope(Ꮡnode);
        defer(Ꮡtx.popScope, savedCurrentScope, savedCurrentScopeFirstDeclarationsOfName, ref ᒐ);

        if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsTypeScript) == 0 && (tx.currentNamespace == nil && tx.currentEnum == nil || (ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsIdentifier) == 0)) {
            return Ꮡnode;
        }

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindPublicKeyword || ᴛ == ast.KindPrivateKeyword || ᴛ == ast.KindProtectedKeyword || ᴛ == ast.KindReadonlyKeyword || ᴛ == ast.KindOverrideKeyword: {
            Ꮡnode = default; node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
            Ꮡnode = tx.visitEnumDeclaration(node.AsEnumDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
            Ꮡnode = tx.visitModuleDeclaration(node.AsModuleDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            Ꮡnode = tx.visitClassDeclaration(node.AsClassDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindClassExpression: {
            Ꮡnode = tx.visitClassExpression(node.AsClassExpression()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindConstructor: {
            Ꮡnode = tx.visitConstructorDeclaration(node.AsConstructorDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
            Ꮡnode = tx.visitFunctionDeclaration(node.AsFunctionDeclaration()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindVariableStatement: {
            Ꮡnode = Ꮡtx.visitVariableStatement(node.AsVariableStatement()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindExportDeclaration || ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindImportClause: {
            if (tx.currentNamespace != nil && tx.currentScope != nil && tx.currentScope.Kind != ast.KindBlock) {
                Ꮡnode = default; node = ref Ꮡnode.DerefOrNull();
            } else {
                Ꮡnode = Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
            if (tx.currentNamespace != nil && tx.currentScope != nil && tx.currentScope.Kind != ast.KindBlock && node.AsImportEqualsDeclaration().ModuleReference.Kind == ast.KindExternalModuleReference) {
                Ꮡnode = default; node = ref Ꮡnode.DerefOrNull();
            } else if (tx.currentNamespace != nil && tx.currentScope != nil && tx.currentScope.Kind == ast.KindBlock && node.AsImportEqualsDeclaration().ModuleReference.Kind != ast.KindExternalModuleReference) {
                Ꮡnode = default; node = ref Ꮡnode.DerefOrNull();
            } else {
                Ꮡnode = tx.visitImportEqualsDeclaration(node.AsImportEqualsDeclaration()); node = ref Ꮡnode.DerefOrNull();
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindIdentifier: {
            Ꮡnode = tx.visitIdentifier(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
            Ꮡnode = tx.visitShorthandPropertyAssignment(node.AsShorthandPropertyAssignment()); node = ref Ꮡnode.DerefOrNull();
            break;
        }
        default: {
            Ꮡnode = Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
            break;
        }}

        return Ꮡnode;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static void recordDeclarationInScope(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableStatement: {
        tx.recordDeclarationInScope(node.AsVariableStatement().DeclarationList);
        return;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclarationList: {
        foreach (var (_, decl) in range(node.AsVariableDeclarationList().Declarations.Nodes)) {
            tx.recordDeclarationInScope(decl);
        }
        return;
    }
    case var ᴛ when ᴛ == ast.KindArrayBindingPattern || ᴛ == ast.KindObjectBindingPattern: {
        foreach (var (_, element) in range(node.Elements())) {
            tx.recordDeclarationInScope(element);
        }
        return;
    }}

    var name = node.Name();
    if (name != nil) {
        if (ast.IsIdentifier(name)) {
            if (tx.currentScopeFirstDeclarationsOfName == nil) {
                tx.currentScopeFirstDeclarationsOfName = new map<@string, ж<ast.Node>>();
            }
            @string text = name.Text();
            var (_, found) = tx.currentScopeFirstDeclarationsOfName[text, ꟷ]; if (!found) {
                tx.currentScopeFirstDeclarationsOfName[text] = Ꮡnode;
            }
        } else if (ast.IsBindingPattern(name)) {
            tx.recordDeclarationInScope(name);
        }
    }
}

internal static bool isFirstDeclarationInScope(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    if (name != nil && ast.IsIdentifier(name)) {
        @string text = name.Text();
        var (firstDeclaration, found) = tx.currentScopeFirstDeclarationsOfName[text, ꟷ]; if (found) {
            return firstDeclaration == Ꮡnode;
        }
    }
    return false;
}

internal static bool isExportOfNamespace(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.currentNamespace != nil && (tx.currentScope == nil || tx.currentScope.Kind != ast.KindBlock) && (ast.ModifierFlags)(node.ModifierFlags() & ast.ModifierFlagsExport) != 0;
}

internal static ж<astꓸExpression> getExpressionForPropertyName(this ref RuntimeSyntaxTransformer tx, ж<ast.EnumMember> Ꮡmember) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var name = member.Name();
    switch (name.Kind) {
    case var ᴛ when ᴛ == ast.KindPrivateIdentifier: {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("");
    }
    case var ᴛ when ᴛ == ast.KindComputedPropertyName: {
        var n = name.AsComputedPropertyName();
        return tx.Transformer.Visitor().VisitNode((~n).Expression);
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(name.Text(), ast.TokenFlagsNone);
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(name.Text(), ast.TokenFlagsNone);
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral: {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(name.Text(), ast.TokenFlagsNone);
    }
    default: {
        return name;
    }}

}

internal static ж<astꓸExpression> getEnumQualifiedElement(this ref RuntimeSyntaxTransformer tx, ж<ast.EnumDeclaration> @enum, ж<ast.EnumMember> member) {
    var prop = tx.getNamespaceQualifiedElement(tx.getNamespaceContainerName(@enum.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), tx.getExpressionForPropertyName(member));
    tx.Transformer.EmitContext().AddEmitFlags(prop, (printer.EmitFlags)((printer.EmitFlags)((printer.EmitFlags)(printer.EFNoComments | printer.EFNoNestedComments) | printer.EFNoSourceMap) | printer.EFNoNestedSourceMaps));
    return prop;
}

internal static ж<astꓸIdentifierNode> getNamespaceContainerName(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> node) {
    return tx.Transformer.Factory().NewGeneratedNameForNode(node);
}

internal static ж<astꓸExpression> getNamespaceQualifiedProperty(this ref RuntimeSyntaxTransformer tx, ж<astꓸIdentifierNode> ns, ж<astꓸIdentifierNode> name) {
    return tx.Transformer.Factory().GetNamespaceMemberName(ns, name, new printer.NameOptions(AllowSourceMaps: true));
}

internal static ж<astꓸExpression> getNamespaceQualifiedElement(this ref RuntimeSyntaxTransformer tx, ж<astꓸIdentifierNode> ns, ж<astꓸExpression> expression) {
    var qualifiedName = tx.Transformer.EmitContext().Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(ns, nil, expression, ast.NodeFlagsNone);
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(qualifiedName, expression);
    return qualifiedName;
}

internal static ж<astꓸExpression> getExportQualifiedReferenceToDeclaration(this ref RuntimeSyntaxTransformer tx, ж<astꓸDeclaration> node) {
    if (tx.isExportOfNamespace(node.AsNode())) {
        return tx.Transformer.Factory().GetExternalModuleOrNamespaceExportName(tx.getNamespaceContainerName(tx.currentNamespace), node, false, true);
    }
    return tx.Transformer.Factory().GetDeclarationNameEx(node.AsNode(), new printer.NameOptions(AllowSourceMaps: true));
}

internal static (slice<ж<astꓸStatement>>, bool) addVarForDeclaration(this ref RuntimeSyntaxTransformer tx, slice<ж<astꓸStatement>> statements, ж<astꓸDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    tx.recordDeclarationInScope(Ꮡnode);
    if (!tx.isFirstDeclarationInScope(Ꮡnode)) {
        return (statements, false);
    }

    var name = tx.Transformer.Factory().GetLocalNameEx(Ꮡnode, new printer.AssignedNameOptions(AllowSourceMaps: true));
    var varDecl = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(name, nil, nil, nil);
    var varFlags = core.IfElse(tx.currentScope == tx.currentSourceFile, ast.NodeFlagsNone, ast.NodeFlagsLet);
    var varDecls = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), varFlags);
    var modifierMask = ~((ast.ModifierFlags)((ast.ModifierFlags)(ast.ModifierFlagsTypeScriptModifier | ast.ModifierFlagsDecorator)));
    if (tx.currentNamespace != nil) {
        modifierMask &= unchecked((ast.ModifierFlags)~(ast.ModifierFlags)(ast.ModifierFlagsExport));
    }
    var modifiers = transformers.ExtractModifiers(tx.Transformer.EmitContext(), node.Modifiers(), modifierMask);
    var varStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(modifiers, varDecls);

    tx.Transformer.EmitContext().SetOriginal(varDecl, Ꮡnode);
    tx.Transformer.EmitContext().SetOriginal(varStatement, Ꮡnode);

    if (ast.IsEnumDeclaration(Ꮡnode)) {
        tx.Transformer.EmitContext().SetSourceMapRange(varDecls, node.Loc);
    } else {
        tx.Transformer.EmitContext().SetSourceMapRange(varStatement, node.Loc);
    }

    tx.Transformer.EmitContext().SetCommentRange(varStatement, node.Loc);
    tx.Transformer.EmitContext().AddEmitFlags(varStatement, printer.EFNoTrailingComments);
    statements = append(statements, varStatement);

    return (statements, true);
}

internal static ж<ast.Node> visitEnumDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.EnumDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!tx.shouldEmitEnumDeclaration(Ꮡnode)) {
        return tx.Transformer.EmitContext().NewNotEmittedStatement(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var statements = slice<ж<astꓸStatement>>([]);

    (statements, var varAdded) = tx.addVarForDeclaration(statements, Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    var emitFlags = printer.EFNone;
    if (varAdded && (tx.compilerOptions.GetEmitModuleKind() != core.ModuleKindSystem || tx.currentScope != tx.currentSourceFile)) {
        emitFlags |= (printer.EmitFlags)(printer.EFNoLeadingComments);
    }

    var enumArg = tx.Transformer.Factory().NewLogicalORExpression(
        tx.getExportQualifiedReferenceToDeclaration(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        tx.Transformer.Factory().NewAssignmentExpression(
            tx.getExportQualifiedReferenceToDeclaration(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), false)));

    if (tx.isExportOfNamespace(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var localName = tx.Transformer.Factory().GetLocalNameEx(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), new printer.AssignedNameOptions(AllowSourceMaps: true));

        enumArg = tx.Transformer.Factory().NewAssignmentExpression(localName, enumArg);
    }

    var enumParamName = tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().SetSourceMapRange(enumParamName, node.Name().Loc);

    var enumParam = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(nil, nil, enumParamName, nil, nil, nil);
    var enumBody = tx.transformEnumBody(Ꮡnode);
    var enumFunc = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionExpression(nil, nil, nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([enumParam]), nil, nil, enumBody);
    var enumCall = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression(enumFunc), nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([enumArg]), ast.NodeFlagsNone);
    var enumStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(enumCall);
    tx.Transformer.EmitContext().SetOriginal(enumStatement, Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(enumStatement, Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AddEmitFlags(enumStatement, emitFlags);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(append(statements, enumStatement));
}

internal static ж<astꓸBlockNode> transformEnumBody(this ref RuntimeSyntaxTransformer tx, ж<ast.EnumDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var savedCurrentEnum = tx.currentEnum;
    tx.currentEnum = Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();

    Ꮡnode = tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).AsEnumDeclaration(); node = ref Ꮡnode.DerefOrNull();

    var statements = slice<ж<astꓸStatement>>([]);
    foreach (var i in range(len(node.Members.Nodes))) {
        statements = tx.transformEnumMember(
            statements,
            Ꮡnode,
            i);
    }

    var statementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
    statementList.Loc = node.Members.Loc;

    tx.currentEnum = savedCurrentEnum;
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(statementList, true);
}

internal static slice<ж<astꓸStatement>> transformEnumMember(this ref RuntimeSyntaxTransformer tx, slice<ж<astꓸStatement>> statements, ж<ast.EnumDeclaration> Ꮡenum, nint index) {
    ref var @enum = ref Ꮡenum.DerefOrNull();

    var memberNode = @enum.Members.Nodes[index];
    var member = memberNode.AsEnumMember();

    var savedParent = tx.parentNode;
    tx.parentNode = tx.currentNode;
    tx.currentNode = memberNode;

    var expression = member.Value.Initializer;

    bool useExplicitReverseMapping = default;

    var parseNode = tx.Transformer.EmitContext().ParseNode(memberNode);
    var result = tx.emitResolver.GetEnumMemberValue(parseNode);
    switch (result.Value.type()) {
    case jsnum.Number value: {
        expression = core.Coalesce<astꓸExpression>(constantExpression(value, tx.Transformer.Factory()), expression);
        useExplicitReverseMapping = true;
        break;
    }
    case @string value: {
        expression = core.Coalesce<astꓸExpression>(constantExpression(value, tx.Transformer.Factory()), expression);
        break;
    }
    default: {
        var value = result.Value;
        if (expression == nil) {
            expression = tx.Transformer.Factory().NewVoidZeroExpression();
        }
        useExplicitReverseMapping = !result.IsSyntacticallyString;
        break;
    }}

    expression = tx.Transformer.Factory().NewAssignmentExpression(
        tx.getEnumQualifiedElement(Ꮡenum, member),
        expression);

    if (useExplicitReverseMapping) {
        expression = tx.Transformer.Factory().NewAssignmentExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(
                tx.getNamespaceContainerName(Ꮡenum.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
                nil,
                expression,
                ast.NodeFlagsNone),
            tx.getExpressionForPropertyName(member));
    }

    var memberStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, member.of(ast.EnumMember.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(memberStatement, member.of(ast.EnumMember.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    statements = append(statements, memberStatement);

    tx.currentNode = tx.parentNode;
    tx.parentNode = savedParent;
    return statements;
}

internal static ж<ast.Node> visitModuleDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.ModuleDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!tx.shouldEmitModuleDeclaration(Ꮡnode)) {
        return tx.Transformer.EmitContext().NewNotEmittedStatement(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var statements = slice<ж<astꓸStatement>>([]);

    (statements, var varAdded) = tx.addVarForDeclaration(statements, Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    var emitFlags = printer.EFNone;
    if (varAdded && (tx.compilerOptions.GetEmitModuleKind() != core.ModuleKindSystem || tx.currentScope != tx.currentSourceFile)) {
        emitFlags |= (printer.EmitFlags)(printer.EFNoLeadingComments);
    }

    var moduleArg = tx.Transformer.Factory().NewLogicalORExpression(
        tx.getExportQualifiedReferenceToDeclaration(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        tx.Transformer.Factory().NewAssignmentExpression(
            tx.getExportQualifiedReferenceToDeclaration(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), false)));

    if (tx.isExportOfNamespace(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var localName = tx.Transformer.Factory().GetLocalNameEx(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), new printer.AssignedNameOptions(AllowSourceMaps: true));

        moduleArg = tx.Transformer.Factory().NewAssignmentExpression(localName, moduleArg);
    }

    var moduleParamName = tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().SetSourceMapRange(moduleParamName, node.Name().Loc);

    var moduleParam = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(nil, nil, moduleParamName, nil, nil, nil);
    var moduleBody = tx.transformModuleBody(Ꮡnode, tx.getNamespaceContainerName(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    var moduleFunc = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionExpression(nil, nil, nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([moduleParam]), nil, nil, moduleBody);
    var moduleCall = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression(moduleFunc), nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([moduleArg]), ast.NodeFlagsNone);
    var moduleStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(moduleCall);
    tx.Transformer.EmitContext().SetOriginal(moduleStatement, Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(moduleStatement, Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AddEmitFlags(moduleStatement, emitFlags);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(append(statements, moduleStatement));
}

internal static ж<astꓸBlockNode> transformModuleBody(this ref RuntimeSyntaxTransformer tx, ж<ast.ModuleDeclaration> Ꮡnode, ж<astꓸIdentifierNode> namespaceLocalName) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var savedCurrentNamespace = tx.currentNamespace;
    var savedCurrentScope = tx.currentScope;
    var savedCurrentScopeFirstDeclarationsOfName = tx.currentScopeFirstDeclarationsOfName;

    tx.currentNamespace = Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    tx.currentScopeFirstDeclarationsOfName = default;

    slice<ж<astꓸStatement>> statements = default;
    tx.Transformer.EmitContext().StartVariableEnvironment();

    core.TextRange statementsLocation = default;
    core.TextRange blockLocation = default;
    if (node.Body != nil) {
        if (node.Body.Kind == ast.KindModuleBlock) {
            Ꮡnode = tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).AsModuleDeclaration(); node = ref Ꮡnode.DerefOrNull();
            var body = node.Body.AsModuleBlock();
            statements = body.Value.Statements.Nodes;
            statementsLocation = body.Value.Statements.Loc;
            blockLocation = body.Loc;
        } else {
            (statements, _) = tx.Transformer.Visitor().VisitSlice([node.Body]);
            var moduleBlock = (~getInnermostModuleDeclarationFromDottedModule(Ꮡnode)).Body.AsModuleBlock();
            statementsLocation = (~moduleBlock).Statements.Loc.WithPos(-1);
        }
    }

    tx.currentNamespace = savedCurrentNamespace;
    tx.currentScope = savedCurrentScope;
    tx.currentScopeFirstDeclarationsOfName = savedCurrentScopeFirstDeclarationsOfName;

    statements = tx.Transformer.EmitContext().EndAndMergeVariableEnvironment(statements);
    var statementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
    statementList.Loc = statementsLocation;
    var block = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(statementList, true);
    block.Loc = blockLocation;

    if (node.Body == nil || node.Body.Kind != ast.KindModuleBlock) {
        tx.Transformer.EmitContext().AddEmitFlags(block, printer.EFNoComments);
    }
    return block;
}

internal static ж<ast.Node> visitImportEqualsDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.ImportEqualsDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.ModuleReference.Kind == ast.KindExternalModuleReference) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var moduleReference = tx.Transformer.Factory().CreateExpressionFromEntityName(node.ModuleReference);
    tx.Transformer.EmitContext().SetEmitFlags(moduleReference, (printer.EmitFlags)(printer.EFNoComments | printer.EFNoNestedComments));
    if (!tx.isExportOfNamespace(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var varDecl = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(node.Name(), nil, nil, moduleReference);
        tx.Transformer.EmitContext().SetOriginal(varDecl, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var varList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsNone);
        var varModifiers = transformers.ExtractModifiers(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑModifiersBase).Modifiers(), ast.ModifierFlagsExport);
        var varStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(varModifiers, varList);
        tx.Transformer.EmitContext().SetOriginal(varStatement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(varStatement, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return varStatement;
    } else {
        var statement = tx.createExportStatement(node.Name(), moduleReference, node.Loc, node.Loc, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        statement.Loc = node.Loc;
        return statement;
    }
}

internal static ж<ast.Node> visitVariableStatement(this ж<RuntimeSyntaxTransformer> Ꮡtx, ж<ast.VariableStatement> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    var expressionsᴷ = new stackAppend<ж<astꓸExpression>>();

    if (tx.isExportOfNamespace(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var expressions = slice<ж<astꓸExpression>>([]);
        foreach (var (_, declaration) in range(node.DeclarationList.AsVariableDeclarationList().Declarations.Nodes)) {
            var v = declaration.AsVariableDeclaration();
            if ((~v).Initializer == nil) {
                continue;
            }
            if (ast.IsBindingPattern(v.Name())) {
                var expressionΔ1 = transformers.FlattenDestructuringAssignment(
                    Ꮡtx.ᏑTransformer,
                    Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).Visitor().VisitNode(declaration),
                    false,
                    transformers.FlattenLevelAll,
                    new Func<ж<astꓸIdentifierNode>, ж<astꓸExpression>, ж<core.TextRange>, ж<astꓸExpression>>(Ꮡtx.createNamespaceExportExpression));
                if (expressionΔ1 != nil) {
                    expressions = append(expressionsᴷ, expressions, expressionΔ1);
                }
            } else {
                var expressionΔ2 = transformers.ConvertVariableDeclarationToAssignmentExpression(Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).EmitContext(), v);
                if (expressionΔ2 != nil) {
                    expressions = append(expressions, expressionΔ2);
                }
            }
        }
        if (len(expressions) == 0) {
            return default;
        }
        var expression = Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).Factory().InlineExpressions(expressions);
        var statement = Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
        Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).EmitContext().SetOriginal(statement, Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).EmitContext().AssignCommentAndSourceMapRanges(statement, Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

        var savedCurrent = tx.currentNode;
        tx.currentNode = statement;
        statement = Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).Visitor().VisitEachChild(statement);
        tx.currentNode = savedCurrent;
        return statement;
    }
    return Ꮡtx.of(RuntimeSyntaxTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<astꓸExpression> createNamespaceExportExpression(this ref RuntimeSyntaxTransformer tx, ж<astꓸIdentifierNode> exportName, ж<astꓸExpression> exportValue, ж<core.TextRange> Ꮡlocation) {
    ref var location = ref Ꮡlocation.DerefOrNull();

    var memberName = tx.getNamespaceQualifiedProperty(tx.getNamespaceContainerName(tx.currentNamespace), exportName);
    var expression = tx.Transformer.Factory().NewAssignmentExpression(memberName, exportValue);
    if (Ꮡlocation != nil) {
        expression.Loc = location;
    }
    return expression;
}

internal static ж<ast.Node> visitFunctionDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.FunctionDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.isExportOfNamespace(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateFunctionDeclaration(
            Ꮡnode,
            tx.Transformer.Visitor().VisitModifiers(transformers.ExtractModifiers(tx.Transformer.EmitContext(), Ꮡnode.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers(), ~((ast.ModifierFlags)ast.ModifierFlagsExport))),
            node.AsteriskToken,
            tx.Transformer.Visitor().VisitNode(node.Name()),
            nil,
            tx.Transformer.Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Body));
        var export = tx.createExportStatementForDeclaration(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (export != nil) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([updated, export]);
        }
        return updated;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static slice<ж<ast.ParameterDeclaration>> getParameterProperties(this ref RuntimeSyntaxTransformer tx, ж<ast.Node> Ꮡconstructor) {
    ref var constructor = ref Ꮡconstructor.DerefOrNull();
    var parameterPropertiesᴷ = new stackAppend<ж<ast.ParameterDeclaration>>(moves: true);

    slice<ж<ast.ParameterDeclaration>> parameterProperties = default;
    if (Ꮡconstructor != nil) {
        foreach (var (_, parameter) in range(constructor.Parameters())) {
            if (ast.IsParameterPropertyDeclaration(parameter, Ꮡconstructor)) {
                parameterProperties = append(parameterPropertiesᴷ, parameterProperties, parameter.AsParameterDeclaration());
            }
        }
    }
    parameterProperties = move2heap(parameterPropertiesᴷ, parameterProperties);
    return parameterProperties;
}

internal static ж<ast.Node> visitClassDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var exported = tx.isExportOfNamespace(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    ж<ast.ModifierList> modifiers = default;
    if (exported) {
        modifiers = tx.Transformer.Visitor().VisitModifiers(transformers.ExtractModifiers(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), ~((ast.ModifierFlags)ast.ModifierFlagsExportDefault)));
    } else {
        modifiers = tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers());
    }

    var name = tx.Transformer.Visitor().VisitNode(node.Name());
    if (name == nil && (exported || ast.ChildIsDecorated(tx.compilerOptions.ExperimentalDecorators.IsTrue(), Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil))) {
        name = tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    var heritageClauses = tx.Transformer.Visitor().VisitNodes(node.HeritageClauses);
    var members = tx.Transformer.Visitor().VisitNodes(node.Members);
    var parameterProperties = tx.getParameterProperties(core.Find<ж<ast.Node>>(node.Members.Nodes, ast.IsConstructorDeclaration));

    if (len(parameterProperties) > 0) {
        slice<ж<astꓸClassElement>> newMembers = default;
        foreach (var (_, parameter) in range(parameterProperties)) {
            if (ast.IsIdentifier(parameter.Name())) {
                var parameterProperty = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(
                    nil,
                    parameter.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                    nil,
                    nil,
                    nil);
                tx.Transformer.EmitContext().SetOriginal(parameterProperty, parameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                newMembers = append(newMembers, parameterProperty);
            }
        }
        if (len(newMembers) > 0) {
            newMembers = appendꓸꓸꓸ(newMembers, members.Nodes);
            members = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newMembers);
            members.Loc = node.Members.Loc;
        }
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(Ꮡnode, modifiers, name, nil, heritageClauses, members);
    if (exported) {
        var export = tx.createExportStatementForDeclaration(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (export != nil) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList([updated, export]);
        }
    }
    return updated;
}

internal static ж<ast.Node> visitClassExpression(this ref RuntimeSyntaxTransformer tx, ж<ast.ClassExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var modifiers = tx.Transformer.Visitor().VisitModifiers(transformers.ExtractModifiers(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), ~((ast.ModifierFlags)ast.ModifierFlagsExportDefault)));
    var name = tx.Transformer.Visitor().VisitNode(node.Name());
    var heritageClauses = tx.Transformer.Visitor().VisitNodes(node.HeritageClauses);
    var members = tx.Transformer.Visitor().VisitNodes(node.Members);
    var parameterProperties = tx.getParameterProperties(core.Find<ж<ast.Node>>(node.Members.Nodes, ast.IsConstructorDeclaration));

    if (len(parameterProperties) > 0) {
        slice<ж<astꓸClassElement>> newMembers = default;
        foreach (var (_, parameter) in range(parameterProperties)) {
            if (ast.IsIdentifier(parameter.Name())) {
                var parameterProperty = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyDeclaration(
                    nil,
                    parameter.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory())),
                    nil,
                    nil,
                    nil);
                tx.Transformer.EmitContext().SetOriginal(parameterProperty, parameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                newMembers = append(newMembers, parameterProperty);
            }
        }
        if (len(newMembers) > 0) {
            newMembers = appendꓸꓸꓸ(newMembers, members.Nodes);
            members = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newMembers);
            members.Loc = node.Members.Loc;
        }
    }

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(Ꮡnode, modifiers, name, nil, heritageClauses, members);
}

internal static ж<ast.Node> visitConstructorDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.ConstructorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var modifiers = tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.ConstructorDeclaration.ᏑModifiersBase).Modifiers());
    var parameters = tx.Transformer.EmitContext().VisitParameters(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).ParameterList(), tx.Transformer.Visitor());
    var body = tx.visitConstructorBody(node.Body.AsBlock(), Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(Ꮡnode, modifiers, nil, parameters, nil, nil, body);
}

internal static ж<ast.Node> visitConstructorBody(this ref RuntimeSyntaxTransformer tx, ж<ast.Block> Ꮡbody, ж<ast.Node> constructor) {
    ref var body = ref Ꮡbody.DerefOrNull();
    var parameterPropertyAssignmentsᴷ = new stackAppend<ж<astꓸStatement>>();

    var parameterProperties = tx.getParameterProperties(constructor);
    if (len(parameterProperties) == 0) {
        return tx.Transformer.EmitContext().VisitFunctionBody(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.Transformer.Visitor());
    }

    var grandparentOfBody = tx.pushNode(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var (savedCurrentScope, savedCurrentScopeFirstDeclarationsOfName) = tx.pushScope(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    tx.Transformer.EmitContext().StartVariableEnvironment();
    var (prologue, rest) = tx.Transformer.Factory().SplitStandardPrologue(body.Statements.Nodes);
    var statements = slices.Clone<slice<ж<astꓸStatement>>, ж<astꓸStatement>>(prologue);

    slice<ж<astꓸStatement>> parameterPropertyAssignments = default;
    foreach (var (_, parameter) in range(parameterProperties)) {
        if (ast.IsIdentifier(parameter.Name())) {
            var propertyName = parameter.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
            propertyName.Parent = parameter.Name().Parent;
            tx.Transformer.EmitContext().AddEmitFlags(propertyName, (printer.EmitFlags)(printer.EFNoComments | printer.EFNoSourceMap));

            var localName = parameter.Name().Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
            localName.Parent = parameter.Name().Parent;
            tx.Transformer.EmitContext().AddEmitFlags(localName, printer.EFNoComments);

            var parameterProperty = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                tx.Transformer.Factory().NewAssignmentExpression(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                        tx.Transformer.Factory().NewThisExpression(),
                        nil,
                        propertyName,
                        ast.NodeFlagsNone),
                    localName));
            tx.Transformer.EmitContext().SetOriginal(parameterProperty, parameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            tx.Transformer.EmitContext().AddEmitFlags(parameterProperty, printer.EFStartOnNewLine);
            parameterPropertyAssignments = append(parameterPropertyAssignmentsᴷ, parameterPropertyAssignments, parameterProperty);
        }
    }

    var superPath = transformers.FindSuperStatementIndexPath(rest, 0);

    if (len(superPath) > 0) {
        statements = appendꓸꓸꓸ(statements, tx.transformConstructorBodyWorker(rest, superPath, parameterPropertyAssignments));
    } else {
        statements = appendꓸꓸꓸ(statements, parameterPropertyAssignments);
        var (ᴛ1, ᴛ2) = tx.Transformer.Visitor().VisitSlice(rest);
        statements = appendꓸꓸꓸ(statements, core.FirstResult(ᴛ1, ᴛ2));
    }

    statements = tx.Transformer.EmitContext().EndAndMergeVariableEnvironment(statements);
    var statementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
    statementList.Loc = body.Statements.Loc;

    tx.popScope(savedCurrentScope, savedCurrentScopeFirstDeclarationsOfName);
    tx.popNode(grandparentOfBody);
    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(statementList, true);
    tx.Transformer.EmitContext().SetOriginal(updated, Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    updated.Loc = body.Loc;
    return updated;
}

internal static slice<ж<astꓸStatement>> transformConstructorBodyWorker(this ref RuntimeSyntaxTransformer tx, slice<ж<astꓸStatement>> statementsIn, slice<nint> superPath, slice<ж<astꓸStatement>> initializerStatements) {
    var statementsOutᴷ = new stackAppend<ж<astꓸStatement>>(moves: true);

    slice<ж<astꓸStatement>> statementsOut = default;
    nint superStatementIndex = superPath[0];
    var superStatement = statementsIn[superStatementIndex];

    var (ᴛ3, ᴛ4) = tx.Transformer.Visitor().VisitSlice(statementsIn.slice(0, superStatementIndex));
    statementsOut = appendꓸꓸꓸ(statementsOut, core.FirstResult(ᴛ3, ᴛ4));

    if (ast.IsTryStatement(superStatement)) {
        var tryStatement = superStatement.AsTryStatement();
        var tryBlock = tryStatement.TryBlock.AsBlock();

        var grandparentOfTryStatement = tx.pushNode(tryStatement.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var grandparentOfTryBlock = tx.pushNode(tryBlock.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var (savedCurrentScope, savedCurrentScopeFirstDeclarationsOfName) = tx.pushScope(tryBlock.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

        var tryBlockStatements = tx.transformConstructorBodyWorker(
            (~tryBlock).Statements.Nodes,
            superPath[1..],
            initializerStatements);

        tx.popScope(savedCurrentScope, savedCurrentScopeFirstDeclarationsOfName);
        tx.popNode(grandparentOfTryBlock);

        var tryBlockStatementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(tryBlockStatements);
        tryBlockStatementList.Loc = tryBlock.Value.Statements.Loc;
        statementsOut = append(statementsOutᴷ, statementsOut, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTryStatement(
            tryStatement,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(tryBlock, tryBlockStatementList, tryBlock.MultiLine),
            tx.Transformer.Visitor().VisitNode(tryStatement.CatchClause),
            tx.Transformer.Visitor().VisitNode(tryStatement.FinallyBlock)));

        tx.popNode(grandparentOfTryStatement);
    } else {
        var (ᴛ5, ᴛ6) = tx.Transformer.Visitor().VisitSlice(statementsIn.slice(superStatementIndex, superStatementIndex + 1));
        statementsOut = appendꓸꓸꓸ(statementsOut, core.FirstResult(ᴛ5, ᴛ6));

        statementsOut = appendꓸꓸꓸ(statementsOut, initializerStatements);
    }

    var (ᴛ7, ᴛ8) = tx.Transformer.Visitor().VisitSlice(statementsIn.slice(superStatementIndex + 1));
    statementsOut = appendꓸꓸꓸ(statementsOut, core.FirstResult(ᴛ7, ᴛ8));
    statementsOut = move2heap(statementsOutᴷ, statementsOut);
    return statementsOut;
}

internal static ж<ast.Node> visitShorthandPropertyAssignment(this ref RuntimeSyntaxTransformer tx, ж<ast.ShorthandPropertyAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    var exportedOrImportedName = tx.visitExpressionIdentifier(name);
    if (exportedOrImportedName != name) {
        var expression = exportedOrImportedName;
        if (node.ObjectAssignmentInitializer != nil) {
            var equalsToken = node.EqualsToken;
            if (equalsToken == nil) {
                equalsToken = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsToken);
            }
            expression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
                nil,
                expression,
                nil,
                equalsToken,
                tx.Transformer.Visitor().VisitNode(node.ObjectAssignmentInitializer));
        }

        var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, node.Name(), nil, nil, expression);
        updated.Loc = node.Loc;
        tx.Transformer.EmitContext().SetOriginal(updated, Ꮡnode.of(ast.ShorthandPropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(updated, Ꮡnode.of(ast.ShorthandPropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return updated;
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

internal static ж<ast.Node> visitIdentifier(this ref RuntimeSyntaxTransformer tx, ж<astꓸIdentifierNode> node) {
    if (transformers.IsIdentifierReference(node, tx.parentNode)) {
        return tx.visitExpressionIdentifier(node);
    }
    return node;
}

internal static ж<ast.Node> visitExpressionIdentifier(this ref RuntimeSyntaxTransformer tx, ж<astꓸIdentifierNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((tx.currentEnum != nil || tx.currentNamespace != nil) && !transformers.IsGeneratedIdentifier(tx.Transformer.EmitContext(), Ꮡnode) && !transformers.IsLocalName(tx.Transformer.EmitContext(), Ꮡnode)) {
        var location = tx.Transformer.EmitContext().MostOriginal(Ꮡnode.AsNode());
        var container = tx.resolver.GetReferencedExportContainer(location, false);
        if (container != nil && (ast.IsEnumDeclaration(container) || ast.IsModuleDeclaration(container))) {
            var containerName = tx.getNamespaceContainerName(container);

            var memberName = node.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
            tx.Transformer.EmitContext().SetEmitFlags(memberName, (printer.EmitFlags)(printer.EFNoComments | printer.EFNoSourceMap));

            var expression = tx.Transformer.Factory().GetNamespaceMemberName(containerName, memberName, new printer.NameOptions(AllowSourceMaps: true));
            tx.Transformer.EmitContext().AssignCommentAndSourceMapRanges(expression, Ꮡnode.AsNode());
            return expression;
        }
    }
    return Ꮡnode;
}

internal static ж<astꓸStatement> createExportStatementForDeclaration(this ref RuntimeSyntaxTransformer tx, ж<astꓸDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var exportName = tx.Transformer.Factory().GetExternalModuleOrNamespaceExportName(tx.getNamespaceContainerName(tx.currentNamespace), Ꮡnode, false, true);
    var localName = tx.Transformer.Factory().GetLocalName(Ꮡnode);
    var expression = tx.Transformer.Factory().NewAssignmentExpression(exportName, localName);
    var exportAssignmentSourceMapRange = node.Loc;
    if (node.Name() != nil) {
        exportAssignmentSourceMapRange = exportAssignmentSourceMapRange.WithPos(node.Name().Pos());
    }
    tx.Transformer.EmitContext().SetSourceMapRange(expression, exportAssignmentSourceMapRange);

    var statement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
    var exportStatementSourceMapRange = node.Loc.WithPos(-1);
    tx.Transformer.EmitContext().SetSourceMapRange(statement, exportStatementSourceMapRange);
    return statement;
}

internal static ж<astꓸExpression> createExportAssignment(this ref RuntimeSyntaxTransformer tx, ж<astꓸIdentifierNode> name, ж<astꓸExpression> expression, core.TextRange exportAssignmentSourceMapRange, ж<ast.Node> original) {
    var exportName = tx.getNamespaceQualifiedProperty(tx.getNamespaceContainerName(tx.currentNamespace), name);
    var exportAssignment = tx.Transformer.Factory().NewAssignmentExpression(exportName, expression);
    tx.Transformer.EmitContext().SetOriginal(exportAssignment, original);
    tx.Transformer.EmitContext().SetSourceMapRange(exportAssignment, exportAssignmentSourceMapRange);
    return exportAssignment;
}

internal static ж<astꓸStatement> createExportStatement(this ref RuntimeSyntaxTransformer tx, ж<astꓸIdentifierNode> name, ж<astꓸExpression> expression, core.TextRange exportAssignmentSourceMapRange, core.TextRange exportStatementSourceMapRange, ж<ast.Node> original) {
    var exportStatement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(tx.createExportAssignment(name, expression, exportAssignmentSourceMapRange, original));
    tx.Transformer.EmitContext().SetOriginal(exportStatement, original);
    tx.Transformer.EmitContext().SetSourceMapRange(exportStatement, exportStatementSourceMapRange);
    return exportStatement;
}

internal static bool shouldEmitEnumDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.EnumDeclaration> node) {
    return !ast.IsEnumConst(node.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) || tx.compilerOptions.ShouldPreserveConstEnums();
}

internal static bool shouldEmitModuleDeclaration(this ref RuntimeSyntaxTransformer tx, ж<ast.ModuleDeclaration> node) {
    var pn = tx.Transformer.EmitContext().ParseNode(node.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (pn == nil) {
        return true;
    }
    return ast.IsInstantiatedModule(pn, tx.compilerOptions.ShouldPreserveConstEnums());
}

} partial class tstransforms_package {

internal static ж<ast.ModuleDeclaration> getInnermostModuleDeclarationFromDottedModule(ж<ast.ModuleDeclaration> ᏑmoduleDeclaration) {
    ref var moduleDeclaration = ref ᏑmoduleDeclaration.DerefOrNull();

    while (moduleDeclaration.Body != nil && moduleDeclaration.Body.Kind == ast.KindModuleDeclaration) {
        ᏑmoduleDeclaration = moduleDeclaration.Body.AsModuleDeclaration(); moduleDeclaration = ref ᏑmoduleDeclaration.DerefOrNull();
    }
    return ᏑmoduleDeclaration;
}

} // end tstransforms_package
