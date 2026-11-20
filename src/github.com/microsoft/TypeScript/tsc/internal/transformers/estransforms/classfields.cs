namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using iter = iter_package;
using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸжastꓸExpression = Span<ж<astꓸExpression>>;
using static estransforms_package;

partial class estransforms_package {

partial struct classFacts /*num:nint*/;

internal static classFacts classFactsNone => 0;
internal static classFacts classFactsClassWasDecorated => /* 1 << 0 */ 1;
internal static classFacts classFactsNeedsClassConstructorReference => /* 1 << 1 */ 2;
internal static classFacts classFactsNeedsClassSuperReference => /* 1 << 2 */ 4;
internal static classFacts classFactsNeedsSubstitutionForThisInClassStaticField => /* 1 << 3 */ 8;
internal static classFacts classFactsWillHoistInitializersToConstructor => /* 1 << 4 */ 16;

partial struct privateIdentifierInfo {
    internal printer.PrivateIdentifierKind kind;
    internal ж<astꓸIdentifierNode> brandCheckIdentifier;
    internal bool isStatic;
    internal bool isValid;
    internal ж<astꓸIdentifierNode> variableName;
    internal ж<astꓸIdentifierNode> methodName;
    internal ж<astꓸIdentifierNode> getterName;
    internal ж<astꓸIdentifierNode> setterName;
}

partial struct privateEnvironmentData {
    internal ж<astꓸIdentifierNode> className;
    internal ж<astꓸIdentifierNode> weakSetName;
}

partial struct privateEnvironment {
    internal privateEnvironmentData data;
    internal map<@string, ж<privateIdentifierInfo>> members;
    internal map<ж<ast.Node>, ж<privateIdentifierInfo>> generatedIdentifiers;
}

partial struct classLexicalEnvironment {
    internal classFacts facts;
    internal ж<astꓸIdentifierNode> classConstructor;
    internal ж<astꓸIdentifierNode> classThis;
    internal ж<astꓸIdentifierNode> superClassReference;
}

partial struct classLexicalEnv {
    internal ж<classLexicalEnv> previous;
    internal ж<classLexicalEnvironment> data;
    internal ж<privateEnvironment> privateEnv;
}

partial struct classFieldsTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal binder.ReferenceResolver resolver;
    internal bool shouldTransformInitializersUsingSet;
    internal bool shouldTransformInitializersUsingDefine;
    internal bool shouldTransformInitializers;
    internal bool shouldTransformPrivateElementsOrClassStaticBlocks;
    internal bool shouldTransformAutoAccessors;
    internal bool shouldTransformThisInStaticInitializers;
    internal bool shouldTransformSuperInStaticInitializers;
    internal bool shouldTransformPrivateStaticElementsInFile;
    internal bool legacyDecorators;
    internal slice<ж<astꓸExpression>> pendingExpressions;
    internal slice<ж<astꓸStatement>> pendingStatements;
    internal ж<classLexicalEnv> lexicalEnvironment;
    internal ж<astꓸClassLikeDeclaration> currentClassContainer;
    internal ж<astꓸClassElement> currentClassElement;
    internal map<ж<ast.Node>, ж<astꓸIdentifierNode>> classAliases;
    internal collections.Set<ж<ast.Node>> enclosingClassDeclarations;
    internal bool inIterationStatement;
    internal bool insideComputedPropertyName;
    internal ж<ast.Node> parentNode;
    internal ж<ast.Node> currentNode;
    internal ж<ast.NodeVisitor> modifierVisitor;
    internal ж<ast.NodeVisitor> discardedValueVisitor;
    internal ж<ast.NodeVisitor> heritageClauseVisitor;
    internal ж<ast.NodeVisitor> assignmentTargetVisitor;
    internal ж<ast.NodeVisitor> classElementVisitor;
    internal ж<ast.NodeVisitor> accessorFieldResultVisitor;
    internal ж<ast.NodeVisitor> arrayAssignmentElementVisitor;
    internal ж<ast.NodeVisitor> objectAssignmentElementVisitor;
    internal ж<ast.NodeVisitor> substitutionVisitor;
    internal Func<ж<anonymousFunctionDefinition>, bool> isAnonymousClassNeedingAssignedName;
}

internal static ж<transformers.Transformer> newClassFieldsTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var languageVersion = opts.CompilerOptions.GetEmitScriptTarget();
    var useDefineForClassFields = opts.CompilerOptions.GetUseDefineForClassFields();

    if (languageVersion >= core.ScriptTargetESNext && useDefineForClassFields) {
        return default;
    }

    var tx = Ꮡ(new classFieldsTransformer(
        compilerOptions: opts.CompilerOptions,
        resolver: opts.Resolver,
        legacyDecorators: opts.CompilerOptions.ExperimentalDecorators.IsTrue()
    ));

    tx.shouldTransformInitializersUsingSet = !useDefineForClassFields;

    tx.shouldTransformInitializersUsingDefine = useDefineForClassFields && languageVersion < core.ScriptTargetES2022;

    tx.shouldTransformInitializers = tx.shouldTransformInitializersUsingSet || tx.shouldTransformInitializersUsingDefine;

    tx.shouldTransformPrivateElementsOrClassStaticBlocks = languageVersion < core.ScriptTargetES2022;

    tx.shouldTransformAutoAccessors = languageVersion < core.ScriptTargetESNext;

    tx.shouldTransformThisInStaticInitializers = languageVersion < core.ScriptTargetES2022;

    tx.shouldTransformSuperInStaticInitializers = tx.shouldTransformThisInStaticInitializers;

    var result = tx.of(classFieldsTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
    tx.modifierVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitModifier);
    tx.discardedValueVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitDiscardedValue);
    tx.heritageClauseVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitHeritageClause);
    tx.assignmentTargetVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitAssignmentTarget);
    tx.classElementVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitClassElement);
    tx.accessorFieldResultVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitAccessorFieldResult);
    tx.arrayAssignmentElementVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitArrayAssignmentElement);
    tx.objectAssignmentElementVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitObjectAssignmentElement);
    tx.substitutionVisitor = tx.of(classFieldsTransformer.ᏑTransformer).EmitContext().NewNodeVisitor(tx.visitForSubstitution);
    var txʗ1 = tx;
        tx.isAnonymousClassNeedingAssignedName = (ж<anonymousFunctionDefinition> p1) => txʗ1.isAnonymousClassNeedingAssignedNameWorker(p1);

    return result;
}

} partial class estransformsᴛclassFieldsTransformer {

internal static bool requiresBlockScopedVar(this ref classFieldsTransformer tx) {
    return tx.inIterationStatement && tx.currentClassContainer != nil && ast.IsClassExpression(tx.currentClassContainer);
}

internal static bool classExpressionNeedsBlockScopedTemp(this ref classFieldsTransformer tx) {
    if (!tx.requiresBlockScopedVar()) {
        return false;
    }
    foreach (var (_, member) in range(tx.currentClassContainer.Members())) {
        if (ast.IsPropertyDeclaration(member) && !ast.HasStaticModifier(member) && member.Name() != nil && ast.IsComputedPropertyName(member.Name())) {
            return true;
        }
    }
    return false;
}

internal static ж<ast.Node> visitSourceFile(this ж<classFieldsTransformer> Ꮡtx, ж<ast.SourceFile> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsDeclarationFile) {
        return Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    tx.lexicalEnvironment = default;
    tx.shouldTransformPrivateStaticElementsInFile = (printer.EmitFlags)(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().EmitFlags(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) & printer.EFTransformPrivateStaticElements) != 0;
    tx.classAliases = new map<ж<ast.Node>, ж<astꓸIdentifierNode>>();
    Ꮡtx.ᏑenclosingClassDeclarations.Clear();
    var visited = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddEmitHelper(visited, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    tx.classAliases = default;
    Ꮡtx.ᏑenclosingClassDeclarations.Clear();
    return visited;
}

internal static ж<ast.Node> visitModifier(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindAccessorKeyword) {
        if (tx.shouldTransformAutoAccessorsInCurrentClass()) {
            return default;
        }
        return Ꮡnode;
    }
    if (ast.IsModifier(Ꮡnode)) {
        return Ꮡnode;
    }
    return default;
}

internal static ж<ast.Node> /*grandparentNode*/ pushNode(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    ж<ast.Node> grandparentNode = default;

    grandparentNode = tx.parentNode;
    tx.parentNode = tx.currentNode;
    tx.currentNode = node;
    return grandparentNode;
}

internal static void popNode(this ref classFieldsTransformer tx, ж<ast.Node> grandparentNode) {
    tx.currentNode = tx.parentNode;
    tx.parentNode = grandparentNode;
}

internal static ж<ast.Node> visitForSubstitution(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindIdentifier) {
        return Ꮡtx.visitIdentifier(node.AsIdentifier());
    }
    if (node.Kind == ast.KindPropertyAccessExpression && ast.IsIdentifier(node.AsPropertyAccessExpression().Name())) {
        return tx.visitPropertyAccessExpressionForSubstitution(node.AsPropertyAccessExpression());
    }
    return tx.substitutionVisitor.VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visit(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var grandparentNode = tx.pushNode(Ꮡnode);
        defer(Ꮡtx.popNode, grandparentNode, ref ᒐ);

        if ((ast.SubtreeFacts)(node.SubtreeFacts() & ((ast.SubtreeFacts)(ast.SubtreeContainsClassFields | ast.SubtreeContainsLexicalThisOrSuper))) == 0) {
            if (tx.currentClassContainer != nil && len(tx.classAliases) > 0) {
                return Ꮡtx.visitForSubstitution(Ꮡnode);
            }
            return Ꮡnode;
        }

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindSourceFile: {
            return Ꮡtx.visitSourceFile(node.AsSourceFile());
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            return Ꮡtx.visitClassDeclaration(node.AsClassDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindClassExpression: {
            return Ꮡtx.visitClassExpression(node.AsClassExpression());
        }
        case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration || ᴛ == ast.KindPropertyDeclaration: {
            throw panic("Use `classElementVisitor` instead.");
            break;
        }
        case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
            return tx.visitPropertyAssignment(node.AsPropertyAssignment());
        }
        case var ᴛ when ᴛ == ast.KindVariableStatement: {
            return tx.visitVariableStatement(node.AsVariableStatement());
        }
        case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
            return tx.visitVariableDeclaration(node.AsVariableDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindParameter: {
            return tx.visitParameterDeclaration(node.AsParameterDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindBindingElement: {
            return tx.visitBindingElement(node.AsBindingElement());
        }
        case var ᴛ when ᴛ == ast.KindExportAssignment: {
            return tx.visitExportAssignment(node.AsExportAssignment());
        }
        case var ᴛ when ᴛ == ast.KindPrivateIdentifier: {
            return tx.visitPrivateIdentifier(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
            return tx.visitPropertyAccessExpression(node.AsPropertyAccessExpression());
        }
        case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
            return tx.visitElementAccessExpression(node.AsElementAccessExpression());
        }
        case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression || ᴛ == ast.KindPostfixUnaryExpression: {
            return tx.visitPreOrPostfixUnaryExpression(Ꮡnode, false);
        }
        case var ᴛ when ᴛ == ast.KindBinaryExpression: {
            return tx.visitBinaryExpression(node.AsBinaryExpression(), false);
        }
        case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
            return tx.visitParenthesizedExpression(node.AsParenthesizedExpression(), false);
        }
        case var ᴛ when ᴛ == ast.KindCallExpression: {
            return tx.visitCallExpression(node.AsCallExpression());
        }
        case var ᴛ when ᴛ == ast.KindExpressionStatement: {
            return tx.visitExpressionStatement(node.AsExpressionStatement());
        }
        case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
            return tx.visitTaggedTemplateExpression(node.AsTaggedTemplateExpression());
        }
        case var ᴛ when ᴛ == ast.KindForStatement: {
            return tx.visitForStatement(node.AsForStatement());
        }
        case var ᴛ when ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement: {
            return Ꮡtx.setInIterationStatementAnd(true, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitEachChildOfNode)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindThisKeyword: {
            return tx.visitThisExpression(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression: {
            return Ꮡtx.setInIterationStatementAnd(false, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitFunctionExpressionOrDeclaration)), Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindConstructor || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
            return Ꮡtx.setInIterationStatementAnd(false, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.setClassElementAndVisitEachChild)), Ꮡnode);
        }
        default: {
            return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }}

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitDiscardedValue(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression || ᴛ == ast.KindPostfixUnaryExpression: {
        return tx.visitPreOrPostfixUnaryExpression(Ꮡnode, true);
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return tx.visitBinaryExpression(node.AsBinaryExpression(), true);
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return tx.visitParenthesizedExpression(node.AsParenthesizedExpression(), true);
    }
    default: {
        return Ꮡtx.visit(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitHeritageClause(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindHeritageClause: {
        return tx.heritageClauseVisitor.VisitEachChild(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindExpressionWithTypeArguments: {
        return tx.visitExpressionWithTypeArgumentsInHeritageClause(node.AsExpressionWithTypeArguments());
    }
    default: {
        return Ꮡtx.visit(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitAssignmentTarget(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindArrayLiteralExpression: {
        return tx.visitAssignmentPattern(Ꮡnode);
    }
    default: {
        return Ꮡtx.visit(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitDestructuringAssignmentTarget(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsObjectLiteralExpression(Ꮡnode) || ast.IsArrayLiteralExpression(Ꮡnode)) {
        return tx.visitAssignmentPattern(Ꮡnode);
    }
    if (ast.IsPropertyAccessExpression(Ꮡnode) && ast.IsPrivateIdentifier(node.AsPropertyAccessExpression().Name())) {
        return tx.wrapPrivateIdentifierForDestructuringTarget(Ꮡnode);
    }
    if (tx.shouldTransformSuperInStaticInitializers && tx.currentClassElement != nil && ast.IsSuperProperty(Ꮡnode) && isStaticPropertyDeclarationOrClassStaticBlock(tx.currentClassElement) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
        var data = tx.lexicalEnvironment.data;
        if ((classFacts)(data.facts & classFactsClassWasDecorated) != 0) {
            return tx.visitInvalidSuperProperty(Ꮡnode);
        }
        if (data.classConstructor != nil && data.superClassReference != nil) {
            ж<astꓸExpression> name = default;
            if (ast.IsElementAccessExpression(Ꮡnode)) {
                name = tx.Transformer.Visitor().VisitNode(node.AsElementAccessExpression().ArgumentExpression);
            } else if (ast.IsPropertyAccessExpression(Ꮡnode) && ast.IsIdentifier(node.AsPropertyAccessExpression().Name())) {
                name = tx.Transformer.Factory().NewStringLiteralFromNode(node.AsPropertyAccessExpression().Name());
            }
            if (name != nil) {
                var temp = tx.Transformer.Factory().NewTempVariable();
                var setExpr = tx.Transformer.Factory().NewReflectSetCall(
                    data.superClassReference,
                    name,
                    temp,
                    data.classConstructor);
                return tx.Transformer.Factory().NewAssignmentTargetWrapper(temp, setExpr);
            }
        }
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitClassElement(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindConstructor: {
        return Ꮡtx.setCurrentClassElementAnd(Ꮡnode, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitConstructorDeclaration)), Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindMethodDeclaration: {
        return Ꮡtx.setCurrentClassElementAnd(Ꮡnode, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitMethodOrAccessorDeclaration)), Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return Ꮡtx.setCurrentClassElementAnd(Ꮡnode, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitPropertyDeclaration)), Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration: {
        return Ꮡtx.setCurrentClassElementAnd(Ꮡnode, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitClassStaticBlockDeclaration)), Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindComputedPropertyName: {
        return tx.visitComputedPropertyName(node.AsComputedPropertyName());
    }
    case var ᴛ when ᴛ == ast.KindSemicolonClassElement: {
        return Ꮡnode;
    }
    default: {
        if (ast.IsModifierLike(Ꮡnode)) {
            return tx.visitModifier(Ꮡnode);
        }
        return Ꮡtx.visit(Ꮡnode);
    }}

}

internal static ж<astꓸPropertyName> visitPropertyName(this ref classFieldsTransformer tx, ж<astꓸPropertyName> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (ast.IsComputedPropertyName(Ꮡname)) {
        return tx.visitComputedPropertyName(name.AsComputedPropertyName());
    }
    return tx.Transformer.Visitor().VisitNode(Ꮡname);
}

internal static ж<ast.Node> visitAccessorFieldResult(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return Ꮡtx.transformFieldInitializer(node.AsPropertyDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
        return Ꮡtx.visitClassElement(Ꮡnode);
    }
    default: {
        debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(Ꮡnode), "Expected node to either be a PropertyDeclaration, GetAccessorDeclaration, or SetAccessorDeclaration");
        return default;
    }}

}

internal static ж<ast.Node> visitIdentifier(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Identifier> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var declaration = tx.resolver.GetReferencedValueDeclaration(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    if (declaration != nil) {
        var (alias, ok) = tx.classAliases[declaration, ꟷ]; if (ok && Ꮡtx.ᏑenclosingClassDeclarations.Has(declaration)) {
            var clone = alias.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory()));
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetSourceMapRange(clone, node.Loc);
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetCommentRange(clone, node.Loc);
            return clone;
        }
    }
    return Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

internal static ж<ast.Node> visitPrivateIdentifier(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    if (!tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
        return node;
    }
    if (tx.parentNode != nil && ast.IsStatement(tx.parentNode)) {
        return node;
    }
    var result = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("");
    tx.Transformer.EmitContext().SetOriginal(result, node);
    return result;
}

internal static ж<ast.Node> transformPrivateIdentifierInInExpression(this ref classFieldsTransformer tx, ж<ast.BinaryExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var info = tx.accessPrivateIdentifier(node.Left);
    if (info != nil) {
        var receiver = tx.Transformer.Visitor().VisitNode(node.Right);
        var result = tx.Transformer.Factory().NewClassPrivateFieldInHelper(info.brandCheckIdentifier, receiver);
        tx.Transformer.EmitContext().SetOriginal(result, Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return result;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitPropertyAssignment(this ref classFieldsTransformer tx, ж<ast.PropertyAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode.of(ast.PropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode.of(ast.PropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, "").AsPropertyAssignment(); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.PropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitVariableStatement(this ref classFieldsTransformer tx, ж<ast.VariableStatement> node) {
    var savedPendingStatements = tx.pendingStatements;
    tx.pendingStatements = default;

    var visitedNode = tx.Transformer.Visitor().VisitEachChild(node.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    if (len(tx.pendingStatements) > 0) {
        var result = new slice<ж<ast.Node>>(0, 1 + len(tx.pendingStatements));
        result = append(result, visitedNode);
        result = appendꓸꓸꓸ(result, tx.pendingStatements);
        tx.pendingStatements = savedPendingStatements;
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(result);
    }

    tx.pendingStatements = savedPendingStatements;
    return visitedNode;
}

internal static ж<ast.Node> visitVariableDeclaration(this ref classFieldsTransformer tx, ж<ast.VariableDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, "").AsVariableDeclaration(); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitParameterDeclaration(this ref classFieldsTransformer tx, ж<ast.ParameterDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, "").AsParameterDeclaration(); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitBindingElement(this ref classFieldsTransformer tx, ж<ast.BindingElement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, "").AsBindingElement(); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitExportAssignment(this ref classFieldsTransformer tx, ж<ast.ExportAssignment> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
        @string assignedName = "";
        if (!node.IsExportEquals) {
            assignedName = "default";
        }
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), true, assignedName).AsExportAssignment(); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<astꓸExpression> injectPendingExpressions(this ref classFieldsTransformer tx, ж<astꓸExpression> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    if (len(tx.pendingExpressions) > 0) {
        if (ast.IsParenthesizedExpression(Ꮡexpression)) {
            tx.pendingExpressions = append(tx.pendingExpressions, expression.Expression());
            Ꮡexpression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(
                expression.AsParenthesizedExpression(),
                tx.Transformer.Factory().InlineExpressions(tx.pendingExpressions)); expression = ref Ꮡexpression.DerefOrNull();
        } else {
            var exprs = append(tx.pendingExpressions, Ꮡexpression);
            Ꮡexpression = tx.Transformer.Factory().InlineExpressions(exprs); expression = ref Ꮡexpression.DerefOrNull();
        }
        tx.pendingExpressions = default;
    }
    return Ꮡexpression;
}

internal static ж<ast.Node> visitComputedPropertyName(this ref classFieldsTransformer tx, ж<ast.ComputedPropertyName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var savedLexicalEnvironment = tx.lexicalEnvironment;
    var savedInsideComputedPropertyName = tx.insideComputedPropertyName;
    tx.insideComputedPropertyName = true;
    if (tx.lexicalEnvironment != nil && tx.lexicalEnvironment.previous != nil) {
        tx.lexicalEnvironment = tx.lexicalEnvironment.previous;
    }
    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);
    tx.lexicalEnvironment = savedLexicalEnvironment;
    tx.insideComputedPropertyName = savedInsideComputedPropertyName;
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(Ꮡnode, tx.injectPendingExpressions(expression));
}

internal static ж<ast.Node> visitConstructorDeclaration(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.currentClassContainer != nil) {
        return Ꮡtx.transformConstructor(node.AsConstructorDeclaration(), tx.currentClassContainer);
    }
    return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
}

internal static bool shouldTransformClassElementToWeakMap(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    if (tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
        return true;
    }
    return tx.shouldAlwaysTransformPrivateStaticElements(node);
}

internal static bool shouldAlwaysTransformPrivateStaticElements(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    return ast.HasStaticModifier(node) && (printer.EmitFlags)(tx.Transformer.EmitContext().EmitFlags(node) & printer.EFTransformPrivateStaticElements) != 0;
}

internal static bool nodeHasTransformPrivateStaticElementsFlag(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    return (printer.EmitFlags)(tx.Transformer.EmitContext().EmitFlags(node) & printer.EFTransformPrivateStaticElements) != 0;
}

internal static ж<ast.Node> visitMethodOrAccessorDeclaration(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    debug.Assert(!ast.HasDecorators(Ꮡnode));

    if (!ast.IsPrivateIdentifierClassElementDeclaration(Ꮡnode) || !tx.shouldTransformClassElementToWeakMap(Ꮡnode)) {
        return tx.classElementVisitor.VisitEachChild(Ꮡnode);
    }

    var info = tx.accessPrivateIdentifier(node.Name());
    debug.Assert(info != nil, "Undeclared private name for property declaration.");
    if (!info.isValid) {
        return Ꮡnode;
    }

    var functionName = tx.getHoistedFunctionName(Ꮡnode);
    if (functionName != nil) {
        var modifiers = tx.extractNonStaticNonAccessorModifiers(Ꮡnode);
        tx.Transformer.EmitContext().StartVariableEnvironment();
        var saved = tx.inIterationStatement;
        tx.inIterationStatement = false;
        var body = tx.Transformer.EmitContext().VisitFunctionBody(node.Body(), tx.Transformer.Visitor());
        var @params = tx.Transformer.Visitor().VisitNodes(node.ParameterList());
        tx.inIterationStatement = saved;

        var funcExpr = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionExpression(modifiers, node.BodyData().AsteriskToken, functionName, nil, @params, nil, nil, body);
        var assignment = tx.Transformer.Factory().NewAssignmentExpression(functionName, funcExpr);
        tx.addPendingExpressions(assignment);
    }

    return default;
}

internal static ж<ast.ModifierList> extractNonStaticNonAccessorModifiers(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return transformers.ExtractModifiers(tx.Transformer.EmitContext(), node.Modifiers(), ~((ast.ModifierFlags)((ast.ModifierFlags)(ast.ModifierFlagsStatic | ast.ModifierFlagsAccessor))));
}

internal static ж<ast.Node> setCurrentClassElementAnd(this ж<classFieldsTransformer> Ꮡtx, ж<astꓸClassElement> classElement, Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>> visitor, ж<ast.Node> node) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (classElement != tx.currentClassElement) {
        var saved = tx.currentClassElement;
        tx.currentClassElement = classElement;
        var result = visitor(Ꮡtx, node);
        tx.currentClassElement = saved;
        return result;
    }
    return visitor(Ꮡtx, node);
}

internal static ж<ast.Node> visitEachChildOfNode(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    return tx.Transformer.Visitor().VisitEachChild(node);
}

internal static ж<ast.Node> setInIterationStatementAnd(this ж<classFieldsTransformer> Ꮡtx, bool inIteration, Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>> visitor, ж<ast.Node> node) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (tx.inIterationStatement != inIteration) {
        var saved = tx.inIterationStatement;
        tx.inIterationStatement = inIteration;
        var result = visitor(Ꮡtx, node);
        tx.inIterationStatement = saved;
        return result;
    }
    return visitor(Ꮡtx, node);
}

internal static ж<ast.Node> clearClassElementAndVisitEachChild(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> node) {
    return Ꮡtx.setCurrentClassElementAnd(nil, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitEachChildOfNode)), node);
}

internal static ж<ast.Node> visitFunctionExpressionOrDeclaration(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> node) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    if (tx.currentClassElement != nil) {
        var original = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(node);
        if (original != node && tx.currentClassContainer != nil) {
            foreach (var (_, member) in range(tx.currentClassContainer.Members())) {
                if (Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(member) == original && ast.IsStatic(member)) {
                    return tx.visitEachChildOfNode(node);
                }
            }
        }
    }
    return Ꮡtx.setCurrentClassElementAnd(nil, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitEachChildOfNode)), node);
}

internal static ж<ast.Node> setClassElementAndVisitEachChild(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> node) {
    return Ꮡtx.setCurrentClassElementAnd(node, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitEachChildOfNode)), node);
}

internal static ж<astꓸIdentifierNode> getHoistedFunctionName(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    debug.Assert(node.Name() != nil && ast.IsPrivateIdentifier(node.Name()));
    var info = tx.accessPrivateIdentifier(node.Name());
    debug.Assert(info != nil, "Undeclared private name for property declaration.");
    if (info.kind == printer.PrivateIdentifierKindMethod) {
        return info.methodName;
    }
    if (info.kind == printer.PrivateIdentifierKindAccessor) {
        if (ast.IsGetAccessorDeclaration(Ꮡnode)) {
            return info.getterName;
        }
        if (ast.IsSetAccessorDeclaration(Ꮡnode)) {
            return info.setterName;
        }
    }
    return default;
}

internal static ж<astꓸExpression> tryGetClassThis(this ref classFieldsTransformer tx) {
    var classThis = tx.tryGetClassThisNoContainer(); if (classThis != nil) {
        return classThis;
    }
    if (tx.currentClassContainer != nil) {
        return tx.currentClassContainer.Name();
    }
    return default;
}

internal static ж<astꓸExpression> tryGetClassThisNoContainer(this ref classFieldsTransformer tx) {
    var lex = tx.getClassLexicalEnvironment();
    if (lex.classThis != nil) {
        return lex.classThis;
    }
    if (lex.classConstructor != nil) {
        return lex.classConstructor;
    }
    return default;
}

internal static ж<ast.Node> transformAutoAccessor(this ref classFieldsTransformer tx, ж<ast.PropertyDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var commentRange = tx.Transformer.EmitContext().CommentRange(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var sourceMapRange = tx.Transformer.EmitContext().SourceMapRange(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    var name = node.Name();
    var getterName = name;
    var setterName = name;
    if (ast.IsComputedPropertyName(name) && !transformers.IsSimpleInlineableExpression(name.Expression())) {
        var cacheAssignment = findComputedPropertyNameCacheAssignment(ref (tx.Transformer.EmitContext()).DerefOrNull(), name);
        if (cacheAssignment != nil) {
            getterName = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(name.AsComputedPropertyName(), tx.Transformer.Visitor().VisitNode(name.Expression()));
            setterName = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(name.AsComputedPropertyName(), cacheAssignment.Left);
        } else {
            var temp = tx.Transformer.Factory().NewTempVariable();
            tx.Transformer.EmitContext().SetSourceMapRange(temp, name.Expression().Loc);
            tx.Transformer.EmitContext().AddVariableDeclaration(temp);
            var expression = tx.Transformer.Visitor().VisitNode(name.Expression());
            var assignment = tx.Transformer.Factory().NewAssignmentExpression(temp, expression);
            tx.Transformer.EmitContext().SetSourceMapRange(assignment, name.Expression().Loc);
            getterName = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(name.AsComputedPropertyName(), assignment);
            setterName = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(name.AsComputedPropertyName(), temp);
        }
    }

    var modifiers = tx.modifierVisitor.VisitModifiers(Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers());
    var backingField = createAccessorPropertyBackingField(tx.Transformer.Factory(), Ꮡnode, modifiers, node.Initializer);
    tx.Transformer.EmitContext().SetOriginal(backingField, Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AddEmitFlags(backingField, printer.EFNoComments);
    tx.Transformer.EmitContext().SetSourceMapRange(backingField, sourceMapRange);

    ж<astꓸExpression> receiver = default;
    if (ast.IsStatic(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        receiver = tx.tryGetClassThis();
        if (receiver == nil) {
            receiver = tx.Transformer.Factory().NewThisExpression();
        }
    } else {
        receiver = tx.Transformer.Factory().NewThisExpression();
    }

    var getter = tx.createAccessorPropertyGetRedirector(Ꮡnode, modifiers, getterName, receiver);
    tx.Transformer.EmitContext().SetOriginal(getter, Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().SetCommentRange(getter, commentRange);
    tx.Transformer.EmitContext().SetSourceMapRange(getter, sourceMapRange);

    ж<ast.ModifierList> setterModifiers = default;
    if (modifiers != nil) {
        setterModifiers = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(ast.CreateModifiersFromModifierFlags(modifiers.ModifierFlags, tx.Transformer.Factory().NewModifier));
    }
    var setter = tx.createAccessorPropertySetRedirector(Ꮡnode, setterModifiers, setterName, receiver);
    tx.Transformer.EmitContext().SetOriginal(setter, Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AddEmitFlags(setter, printer.EFNoComments);
    tx.Transformer.EmitContext().SetSourceMapRange(setter, sourceMapRange);

    var (visited, _) = tx.accessorFieldResultVisitor.VisitSlice([backingField, getter, setter]);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(visited);
}

internal static ж<ast.Node> transformPrivateFieldInitializer(this ж<classFieldsTransformer> Ꮡtx, ж<ast.PropertyDeclaration> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.shouldTransformClassElementToWeakMap(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var info = tx.accessPrivateIdentifier(node.Name());
        debug.Assert(info != nil, "Undeclared private name for property declaration.");

        if (!info.isValid) {
            return Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        }

        if (info.isStatic && !tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
            var statement = Ꮡtx.transformPropertyOrClassStaticBlock(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewThisExpression());
            if (statement != nil) {
                return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(
                    nil,
                    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([statement]), true));
            }
        }

        return default;
    }

    if (tx.shouldTransformInitializersUsingSet && !ast.HasStaticModifier(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil && (classFacts)(tx.lexicalEnvironment.data.facts & classFactsWillHoistInitializersToConstructor) != 0) {
        return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
            Ꮡnode,
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitModifiers(Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers()),
            node.Name(),
            nil,
            nil,
            nil);
    }

    if (isNamedEvaluationAnd(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, "").AsPropertyDeclaration(); node = ref Ꮡnode.DerefOrNull();
    }

    return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
        Ꮡnode,
        tx.modifierVisitor.VisitModifiers(Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers()),
        tx.visitPropertyName(node.Name()),
        nil,
        nil,
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitNode(node.Initializer));
}

internal static ж<ast.Node> transformPublicFieldInitializer(this ж<classFieldsTransformer> Ꮡtx, ж<ast.PropertyDeclaration> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.shouldTransformInitializers && !ast.IsAutoAccessorPropertyDeclaration(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var expr = tx.getPropertyNameExpressionIfNeeded(node.Name(), node.Initializer != nil || tx.compilerOptions.GetUseDefineForClassFields());
        if (expr != nil) {
            foreach (var e in range<ж<astꓸExpression>>(flattenCommaList(expr).Invoke)) {
                tx.addPendingExpressions(e);
            }
        }

        if (ast.IsStatic(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && !tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
            var initializerStatement = Ꮡtx.transformPropertyOrClassStaticBlock(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewThisExpression());
            if (initializerStatement != nil) {
                var staticBlock = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(
                    nil,
                    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([initializerStatement]), false));

                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetOriginal(staticBlock, Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetCommentRange(staticBlock, node.Loc);

                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddEmitFlags(initializerStatement, printer.EFNoComments);
                return staticBlock;
            }
        }

        return default;
    }

    return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
        Ꮡnode,
        tx.modifierVisitor.VisitModifiers(Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers()),
        tx.visitPropertyName(node.Name()),
        nil,
        nil,
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitNode(node.Initializer));
}

internal static ж<ast.Node> transformFieldInitializer(this ж<classFieldsTransformer> Ꮡtx, ж<ast.PropertyDeclaration> node) {
    debug.Assert(!ast.HasDecorators(node.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), "Decorators should already have been transformed and elided.");
    if (ast.IsPrivateIdentifierClassElementDeclaration(node.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return Ꮡtx.transformPrivateFieldInitializer(node);
    }
    return Ꮡtx.transformPublicFieldInitializer(node);
}

internal static bool shouldTransformAutoAccessorsInCurrentClass(this ref classFieldsTransformer tx) {
    if (tx.shouldTransformAutoAccessors) {
        return true;
    }
    return tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil && (classFacts)(tx.lexicalEnvironment.data.facts & classFactsWillHoistInitializersToConstructor) != 0;
}

internal static ж<ast.Node> visitPropertyDeclaration(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var propDecl = node.AsPropertyDeclaration();
    if (ast.IsAutoAccessorPropertyDeclaration(Ꮡnode) && (tx.shouldTransformAutoAccessorsInCurrentClass() || ast.HasStaticModifier(Ꮡnode) && tx.shouldAlwaysTransformPrivateStaticElements(Ꮡnode))) {
        return tx.transformAutoAccessor(propDecl);
    }
    return Ꮡtx.transformFieldInitializer(propDecl);
}

internal static ж<astꓸExpression> createPrivateIdentifierAccess(this ref classFieldsTransformer tx, ж<privateIdentifierInfo> info, ж<astꓸExpression> Ꮡreceiver) {
    ref var receiver = ref Ꮡreceiver.DerefOrNull();

    Ꮡreceiver = tx.Transformer.Visitor().VisitNode(Ꮡreceiver); receiver = ref Ꮡreceiver.DerefOrNull();
    return tx.createPrivateIdentifierAccessHelper(info, Ꮡreceiver);
}

internal static ж<astꓸExpression> createPrivateIdentifierAccessHelper(this ref classFieldsTransformer tx, ж<privateIdentifierInfo> Ꮡinfo, ж<astꓸExpression> Ꮡreceiver) {
    ref var info = ref Ꮡinfo.DerefOrNull();
    ref var receiver = ref Ꮡreceiver.DerefOrNull();

    tx.Transformer.EmitContext().SetCommentRange(Ꮡreceiver, core.NewTextRange(-1, receiver.End()));

    var exprᴛ1 = info.kind;
    switch (ᐧ) {
    case {} when exprᴛ1 == printer.PrivateIdentifierKindAccessor: {
        return tx.Transformer.Factory().NewClassPrivateFieldGetHelper(
            Ꮡreceiver,
            info.brandCheckIdentifier,
            info.kind,
            info.getterName);
    }
    case {} when exprᴛ1 == printer.PrivateIdentifierKindMethod: {
        return tx.Transformer.Factory().NewClassPrivateFieldGetHelper(
            Ꮡreceiver,
            info.brandCheckIdentifier,
            info.kind,
            info.methodName);
    }
    case {} when exprᴛ1 == printer.PrivateIdentifierKindField: {
        ж<astꓸIdentifierNode> f = default;
        if (info.isStatic) {
            f = info.variableName;
        }
        return tx.Transformer.Factory().NewClassPrivateFieldGetHelper(
            Ꮡreceiver,
            info.brandCheckIdentifier,
            info.kind,
            f);
    }
    case {} when exprᴛ1 == printer.PrivateIdentifierKindUntransformed: {
        debug.Fail("Access helpers should not be created for untransformed private elements");
        return default;
    }}

    debug.AssertNever(Ꮡinfo.OrTypedNil(), "Unknown private element type");
    return default;
}

internal static ж<ast.Node> visitPropertyAccessExpression(this ref classFieldsTransformer tx, ж<ast.PropertyAccessExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPrivateIdentifier(node.Name())) {
        var info = tx.accessPrivateIdentifier(node.Name());
        if (info != nil) {
            var result = tx.createPrivateIdentifierAccess(info, node.Expression);
            tx.Transformer.EmitContext().SetOriginal(result, Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            result.Loc = node.Loc;
            return result;
        }
    }
    if (tx.shouldTransformSuperInStaticInitializers && tx.currentClassElement != nil && ast.IsSuperProperty(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && ast.IsIdentifier(node.Name()) && isStaticPropertyDeclarationOrClassStaticBlock(tx.currentClassElement) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
        var data = tx.lexicalEnvironment.data;
        if ((classFacts)(data.facts & classFactsClassWasDecorated) != 0) {
            return tx.visitInvalidSuperProperty(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }
        if (data.classConstructor != nil && data.superClassReference != nil) {
            var superProperty = tx.Transformer.Factory().NewReflectGetCall(
                data.superClassReference,
                tx.Transformer.Factory().NewStringLiteralFromNode(node.Name()),
                data.classConstructor);
            tx.Transformer.EmitContext().SetOriginal(superProperty, node.Expression);
            superProperty.Loc = node.Expression.Loc;
            return superProperty;
        }
    }
    if (ast.IsIdentifier(node.Name())) {
        return tx.visitPropertyAccessExpressionForSubstitution(Ꮡnode);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitPropertyAccessExpressionForSubstitution(this ref classFieldsTransformer tx, ж<ast.PropertyAccessExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);
    if (expression != node.Expression) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAccessExpression(Ꮡnode, expression, node.QuestionDotToken, node.Name(), node.Flags);
    }
    return Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

internal static ж<ast.Node> visitElementAccessExpression(this ref classFieldsTransformer tx, ж<ast.ElementAccessExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.shouldTransformSuperInStaticInitializers && tx.currentClassElement != nil && ast.IsSuperProperty(Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && isStaticPropertyDeclarationOrClassStaticBlock(tx.currentClassElement) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
        var data = tx.lexicalEnvironment.data;
        if ((classFacts)(data.facts & classFactsClassWasDecorated) != 0) {
            return tx.visitInvalidSuperProperty(Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }
        if (data.classConstructor != nil && data.superClassReference != nil) {
            var superProperty = tx.Transformer.Factory().NewReflectGetCall(
                data.superClassReference,
                tx.Transformer.Visitor().VisitNode(node.ArgumentExpression),
                data.classConstructor);
            tx.Transformer.EmitContext().SetOriginal(superProperty, node.Expression);
            superProperty.Loc = node.Expression.Loc;
            return superProperty;
        }
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitPreOrPostfixUnaryExpression(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode, bool discarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ast.Kind @operator = default;
    ж<ast.Node> operand = default;
    if (ast.IsPrefixUnaryExpression(Ꮡnode)) {
        @operator = node.AsPrefixUnaryExpression().Operator;
        operand = node.AsPrefixUnaryExpression().Operand;
    } else {
        @operator = node.AsPostfixUnaryExpression().Operator;
        operand = node.AsPostfixUnaryExpression().Operand;
    }

    if (@operator == ast.KindPlusPlusToken || @operator == ast.KindMinusMinusToken) {
        var operandSkipped = ast.SkipParentheses(operand);

        if (ast.IsPropertyAccessExpression(operandSkipped) && ast.IsPrivateIdentifier(operandSkipped.Name())) {
            var info = tx.accessPrivateIdentifier(operandSkipped.Name());
            if (info != nil) {
                var receiver = tx.Transformer.Visitor().VisitNode(operandSkipped.Expression());
                var (readExpression, initializeExpression) = tx.createCopiableReceiverExpr(receiver);

                var expression = tx.createPrivateIdentifierAccessHelper(info, readExpression);
                ж<astꓸIdentifierNode> temp = default;
                if (!ast.IsPrefixUnaryExpression(Ꮡnode) && !discarded) {
                    temp = tx.Transformer.Factory().NewTempVariable();
                    tx.Transformer.EmitContext().AddVariableDeclaration(temp);
                }
                expression = expandPreOrPostfixIncrementOrDecrementExpression(tx.Transformer.Factory(), tx.Transformer.EmitContext(), Ꮡnode, expression, temp);
                var assignReceiver = readExpression;
                if (initializeExpression != nil) {
                    assignReceiver = initializeExpression;
                }
                expression = tx.createPrivateIdentifierAssignment(info, assignReceiver, expression, ast.KindEqualsToken);
                tx.Transformer.EmitContext().SetOriginal(expression, Ꮡnode);
                expression.Loc = node.Loc;
                if (temp != nil) {
                    expression = tx.Transformer.Factory().NewCommaExpression(expression, temp);
                    expression.Loc = node.Loc;
                }
                return expression;
            }
        } else if (tx.shouldTransformSuperInStaticInitializers && tx.currentClassElement != nil && ast.IsSuperProperty(operandSkipped) && isStaticPropertyDeclarationOrClassStaticBlock(tx.currentClassElement) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
            var data = tx.lexicalEnvironment.data;
            if ((classFacts)(data.facts & classFactsClassWasDecorated) != 0) {
                var visitedExpr = tx.visitInvalidSuperProperty(operandSkipped);
                if (ast.IsPrefixUnaryExpression(Ꮡnode)) {
                    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePrefixUnaryExpression(node.AsPrefixUnaryExpression(), node.AsPrefixUnaryExpression().Operator, visitedExpr);
                }
                return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePostfixUnaryExpression(node.AsPostfixUnaryExpression(), visitedExpr, node.AsPostfixUnaryExpression().Operator);
            }
            if (data.classConstructor != nil && data.superClassReference != nil) {
                ж<astꓸExpression> setterName = default;
                ж<astꓸExpression> getterName = default;
                if (ast.IsPropertyAccessExpression(operandSkipped)) {
                    if (ast.IsIdentifier(operandSkipped.Name())) {
                        getterName = tx.Transformer.Factory().NewStringLiteralFromNode(operandSkipped.Name());
                        setterName = getterName;
                    }
                } else if (ast.IsElementAccessExpression(operandSkipped)) {
                    if (transformers.IsSimpleInlineableExpression(operandSkipped.AsElementAccessExpression().ArgumentExpression)) {
                        getterName = operandSkipped.AsElementAccessExpression().ArgumentExpression;
                        setterName = getterName;
                    } else {
                        getterName = tx.Transformer.Factory().NewTempVariable();
                        tx.Transformer.EmitContext().AddVariableDeclaration(getterName);
                        setterName = tx.Transformer.Factory().NewAssignmentExpression(getterName, tx.Transformer.Visitor().VisitNode(operandSkipped.AsElementAccessExpression().ArgumentExpression));
                    }
                }
                if (setterName != nil && getterName != nil) {
                    var expression = tx.Transformer.Factory().NewReflectGetCall(data.superClassReference, getterName, data.classConstructor);
                    expression.Loc = operandSkipped.Loc;

                    ж<astꓸIdentifierNode> temp = default;
                    if (!discarded) {
                        temp = tx.Transformer.Factory().NewTempVariable();
                        tx.Transformer.EmitContext().AddVariableDeclaration(temp);
                    }
                    expression = expandPreOrPostfixIncrementOrDecrementExpression(tx.Transformer.Factory(), tx.Transformer.EmitContext(), Ꮡnode, expression, temp);
                    expression = tx.Transformer.Factory().NewReflectSetCall(data.superClassReference, setterName, expression, data.classConstructor);
                    tx.Transformer.EmitContext().SetOriginal(expression, Ꮡnode);
                    expression.Loc = node.Loc;
                    if (temp != nil) {
                        expression = tx.Transformer.Factory().NewCommaExpression(expression, temp);
                        expression.Loc = node.Loc;
                    }
                    return expression;
                }
            }
        }
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitForStatement(this ref classFieldsTransformer tx, ж<ast.ForStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var initializer = tx.discardedValueVisitor.VisitNode(node.Initializer);
    var condition = tx.Transformer.Visitor().VisitNode(node.Condition);
    var incrementor = tx.discardedValueVisitor.VisitNode(node.Incrementor);
    var saved = tx.inIterationStatement;
    tx.inIterationStatement = true;
    var body = tx.Transformer.EmitContext().VisitIterationBody(node.Statement, tx.Transformer.Visitor());
    tx.inIterationStatement = saved;
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForStatement(Ꮡnode, initializer, condition, incrementor, body);
}

internal static ж<ast.Node> visitExpressionStatement(this ref classFieldsTransformer tx, ж<ast.ExpressionStatement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPrivateIdentifier(node.Expression) && tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
        return Ꮡnode.of(ast.ExpressionStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExpressionStatement(
        Ꮡnode,
        tx.discardedValueVisitor.VisitNode(node.Expression));
}

internal static (ж<astꓸExpression> readExpression, ж<astꓸExpression> initializeExpression) createCopiableReceiverExpr(this ref classFieldsTransformer tx, ж<astꓸExpression> Ꮡreceiver) {
    ж<astꓸExpression> readExpression = default;
    ж<astꓸExpression> initializeExpression = default;
    ref var receiver = ref Ꮡreceiver.DerefOrNull();

    var clone = Ꮡreceiver;
    if (!ast.NodeIsSynthesized(Ꮡreceiver)) {
        clone = receiver.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
    }
    if (transformers.IsSimpleInlineableExpression(Ꮡreceiver)) {
        return (clone, default);
    }
    readExpression = tx.Transformer.Factory().NewTempVariable();
    tx.Transformer.EmitContext().AddVariableDeclaration(readExpression);
    initializeExpression = tx.Transformer.Factory().NewAssignmentExpression(readExpression, clone);
    return (readExpression, initializeExpression);
}

internal static ж<ast.Node> visitCallExpression(this ref classFieldsTransformer tx, ж<ast.CallExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPropertyAccessExpression(node.Expression) && ast.IsPrivateIdentifier(node.Expression.AsPropertyAccessExpression().Name()) && tx.accessPrivateIdentifier(node.Expression.AsPropertyAccessExpression().Name()) != nil) {
        var (thisArg, target) = tx.createCallBinding(node.Expression);
        var visitedTarget = tx.Transformer.Visitor().VisitNode(target);
        var visitedThisArg = tx.Transformer.Visitor().VisitNode(thisArg);
        var visitedArgs = tx.Transformer.Visitor().VisitNodes(node.Arguments);
        var allArgs = new slice<ж<ast.Node>>(0, 1 + len(visitedArgs.Nodes));
        allArgs = append(allArgs, visitedThisArg);
        allArgs = appendꓸꓸꓸ(allArgs, visitedArgs.Nodes);
        if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(
                Ꮡnode,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(visitedTarget, node.QuestionDotToken, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("call"), ast.NodeFlagsOptionalChain),
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(allArgs),
                node.Flags);
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(
            Ꮡnode,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(visitedTarget, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("call"), ast.NodeFlagsNone),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(allArgs),
            node.Flags);
    }

    if (tx.shouldTransformSuperInStaticInitializers && tx.currentClassElement != nil && ast.IsSuperProperty(node.Expression) && isStaticPropertyDeclarationOrClassStaticBlock(tx.currentClassElement) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil && tx.lexicalEnvironment.data.classConstructor != nil) {
        var invocation = tx.Transformer.Factory().NewFunctionCallCall(
            tx.Transformer.Visitor().VisitNode(node.Expression),
            tx.lexicalEnvironment.data.classConstructor,
            tx.Transformer.Visitor().VisitNodes(node.Arguments).Nodes);
        tx.Transformer.EmitContext().SetOriginal(invocation, Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        invocation.Loc = node.Loc;
        return invocation;
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitTaggedTemplateExpression(this ref classFieldsTransformer tx, ж<ast.TaggedTemplateExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPropertyAccessExpression(node.Tag) && ast.IsPrivateIdentifier(node.Tag.AsPropertyAccessExpression().Name()) && tx.accessPrivateIdentifier(node.Tag.AsPropertyAccessExpression().Name()) != nil) {
        var (thisArg, target) = tx.createCallBinding(node.Tag);
        var bindExpr = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(tx.Transformer.Visitor().VisitNode(target), nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("bind"), ast.NodeFlagsNone),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([tx.Transformer.Visitor().VisitNode(thisArg)])),
            ast.NodeFlagsNone);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTaggedTemplateExpression(
            Ꮡnode,
            bindExpr,
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Template),
            node.Flags);
    }

    if (tx.shouldTransformSuperInStaticInitializers && tx.currentClassElement != nil && ast.IsSuperProperty(node.Tag) && isStaticPropertyDeclarationOrClassStaticBlock(tx.currentClassElement) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil && tx.lexicalEnvironment.data.classConstructor != nil) {
        var invocation = tx.Transformer.Factory().NewFunctionBindCall(
            tx.Transformer.Visitor().VisitNode(node.Tag),
            tx.lexicalEnvironment.data.classConstructor,
            default);
        tx.Transformer.EmitContext().SetOriginal(invocation, Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        invocation.Loc = node.Loc;
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTaggedTemplateExpression(
            Ꮡnode,
            invocation,
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Template),
            node.Flags);
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<astꓸExpression> transformClassStaticBlockDeclaration(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
        if (isClassThisAssignmentBlock(tx.Transformer.EmitContext(), Ꮡnode)) {
            var result = tx.Transformer.Visitor().VisitNode((~(~node.AsClassStaticBlockDeclaration()).Body.AsBlock()).Statements.Nodes[0].Expression());
            if (ast.IsAssignmentExpression(result, true)) {
                var binary = result.AsBinaryExpression();
                if (binary.Left == binary.Right) {
                    return default;
                }
            }
            return result;
        }

        if (isClassNamedEvaluationHelperBlock(tx.Transformer.EmitContext(), Ꮡnode)) {
            return tx.Transformer.Visitor().VisitNode((~(~node.AsClassStaticBlockDeclaration()).Body.AsBlock()).Statements.Nodes[0].Expression());
        }

        tx.Transformer.EmitContext().StartVariableEnvironment();
        var statements = tx.setCurrentClassElementAndVisitStatements(Ꮡnode, (~(~node.AsClassStaticBlockDeclaration()).Body.AsBlock()).Statements.Nodes);
        statements = tx.Transformer.EmitContext().EndAndMergeVariableEnvironment(statements);

        var iife = tx.Transformer.Factory().NewImmediatelyInvokedArrowFunction(statements);
        var arrowFunction = ast.SkipParentheses(iife.Expression());
        tx.Transformer.EmitContext().SetOriginal(arrowFunction, Ꮡnode);
        tx.Transformer.EmitContext().AddEmitFlags(arrowFunction, printer.EFNoLexicalArguments);
        (~arrowFunction.AsArrowFunction()).Body.AsBlock().Value.Statements.Loc = (~node.AsClassStaticBlockDeclaration()).Body.AsBlock().Value.Statements.Loc;
        tx.Transformer.EmitContext().SetOriginal(iife, Ꮡnode);
        tx.Transformer.EmitContext().AssignSourceMapRange(iife, Ꮡnode);
        tx.Transformer.EmitContext().AddEmitFlags(arrowFunction, printer.EFNoLexicalThis);
        return iife;
    }
    return default;
}

internal static slice<ж<astꓸStatement>> setCurrentClassElementAndVisitStatements(this ref classFieldsTransformer tx, ж<ast.Node> classElement, slice<ж<astꓸStatement>> statements) {
    var savedCurrentClassElement = tx.currentClassElement;
    tx.currentClassElement = classElement;
    var (result, _) = tx.Transformer.Visitor().VisitSlice(statements);
    tx.currentClassElement = savedCurrentClassElement;
    return result;
}

internal static bool isAnonymousClassNeedingAssignedNameWorker(this ж<classFieldsTransformer> Ꮡtx, ж<anonymousFunctionDefinition> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsClassExpression(Ꮡnode) && node.Name() == nil) {
        var staticPropertiesOrClassStaticBlocks = tx.getStaticPropertiesAndClassStaticBlock(Ꮡnode);
        if (core.Some(staticPropertiesOrClassStaticBlocks, (ж<ast.Node> n) => isClassNamedEvaluationHelperBlock(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), n))) {
            return false;
        }
        var hasTransformableStatics = (tx.shouldTransformPrivateElementsOrClassStaticBlocks || tx.nodeHasTransformPrivateStaticElementsFlag(Ꮡnode)) && core.Some(staticPropertiesOrClassStaticBlocks, (ж<ast.Node> n) => ast.IsClassStaticBlockDeclaration(n) || ast.IsPrivateIdentifierClassElementDeclaration(n) || Ꮡtx.Value.shouldTransformInitializers && ast.IsInitializedProperty(n));
        return hasTransformableStatics;
    }
    return false;
}

internal static ж<ast.Node> visitBinaryExpression(this ref classFieldsTransformer tx, ж<ast.BinaryExpression> Ꮡnode, bool discarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsDestructuringAssignment(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var savedPendingExpressions = tx.pendingExpressions;
        tx.pendingExpressions = default;
        var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(
            Ꮡnode,
            nil,
            tx.assignmentTargetVisitor.VisitNode(node.Left),
            nil,
            node.OperatorToken,
            tx.Transformer.Visitor().VisitNode(node.Right));
        ж<astꓸExpression> result = default;
        if (len(tx.pendingExpressions) > 0) {
            var exprs = append(tx.pendingExpressions, updated);
            result = tx.Transformer.Factory().InlineExpressions(exprs);
        } else {
            result = updated;
        }
        tx.pendingExpressions = savedPendingExpressions;
        return result;
    }

    if (ast.IsAssignmentExpression(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false)) {
        if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
            Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, "").AsBinaryExpression(); node = ref Ꮡnode.DerefOrNull();
            debug.Assert(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode() != nil && ast.IsAssignmentExpression(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false));
        }

        var left = ast.SkipOuterExpressions(node.Left, (ast.OuterExpressionKinds)(ast.OEKPartiallyEmittedExpressions | ast.OEKParentheses));
        if (ast.IsPropertyAccessExpression(left) && ast.IsPrivateIdentifier(left.Name())) {
            var info = tx.accessPrivateIdentifier(left.Name());
            if (info != nil) {
                var result = tx.createPrivateIdentifierAssignment(info, left.Expression(), node.Right, node.OperatorToken.Kind);
                tx.Transformer.EmitContext().SetOriginal(result, Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                result.Loc = node.Loc;
                return result;
            }
        } else if (tx.shouldTransformSuperInStaticInitializers && tx.currentClassElement != nil && ast.IsSuperProperty(node.Left) && isStaticPropertyDeclarationOrClassStaticBlock(tx.currentClassElement) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
            var data = tx.lexicalEnvironment.data;
            if ((classFacts)(data.facts & classFactsClassWasDecorated) != 0) {
                return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(
                    Ꮡnode,
                    nil,
                    tx.visitInvalidSuperProperty(node.Left),
                    nil,
                    node.OperatorToken,
                    tx.Transformer.Visitor().VisitNode(node.Right));
            }
            if (data.classConstructor != nil && data.superClassReference != nil) {
                ж<astꓸExpression> setterName = default;
                if (ast.IsElementAccessExpression(node.Left)) {
                    setterName = tx.Transformer.Visitor().VisitNode(node.Left.AsElementAccessExpression().ArgumentExpression);
                } else if (ast.IsPropertyAccessExpression(node.Left) && ast.IsIdentifier(node.Left.AsPropertyAccessExpression().Name())) {
                    setterName = tx.Transformer.Factory().NewStringLiteralFromNode(node.Left.AsPropertyAccessExpression().Name());
                }
                if (setterName != nil) {
                    var expression = tx.Transformer.Visitor().VisitNode(node.Right);
                    if (ast.IsCompoundAssignment(node.OperatorToken.Kind)) {
                        var getterName = setterName;
                        if (!transformers.IsSimpleInlineableExpression(setterName)) {
                            getterName = tx.Transformer.Factory().NewTempVariable();
                            tx.Transformer.EmitContext().AddVariableDeclaration(getterName);
                            setterName = tx.Transformer.Factory().NewAssignmentExpression(getterName, setterName);
                        }
                        var superPropertyGet = tx.Transformer.Factory().NewReflectGetCall(
                            data.superClassReference,
                            getterName,
                            data.classConstructor);
                        tx.Transformer.EmitContext().SetOriginal(superPropertyGet, node.Left);
                        superPropertyGet.Loc = node.Left.Loc;
                        expression = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
                            nil,
                            superPropertyGet,
                            nil,
                            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(transformers.GetNonAssignmentOperatorForCompoundAssignment(node.OperatorToken.Kind)),
                            expression);
                        expression.Loc = node.Loc;
                    }

                    ж<astꓸIdentifierNode> temp = default;
                    if (!discarded) {
                        temp = tx.Transformer.Factory().NewTempVariable();
                        tx.Transformer.EmitContext().AddVariableDeclaration(temp);
                    }
                    if (temp != nil) {
                        expression = tx.Transformer.Factory().NewAssignmentExpression(temp, expression);
                        expression.Loc = node.Loc;
                    }

                    expression = tx.Transformer.Factory().NewReflectSetCall(
                        data.superClassReference,
                        setterName,
                        expression,
                        data.classConstructor);
                    tx.Transformer.EmitContext().SetOriginal(expression, Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                    expression.Loc = node.Loc;

                    if (temp != nil) {
                        expression = tx.Transformer.Factory().NewCommaExpression(expression, temp);
                        expression.Loc = node.Loc;
                    }
                    return expression;
                }
            }
        }
    }

    if (node.OperatorToken.Kind == ast.KindInKeyword && ast.IsPrivateIdentifier(node.Left)) {
        return tx.transformPrivateIdentifierInInExpression(Ꮡnode);
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitParenthesizedExpression(this ref classFieldsTransformer tx, ж<ast.ParenthesizedExpression> Ꮡnode, bool discarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (discarded) {
        var expressionΔ1 = tx.discardedValueVisitor.VisitNode(node.Expression);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(Ꮡnode, expressionΔ1);
    }
    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(Ꮡnode, expression);
}

internal static ж<astꓸExpression> createPrivateIdentifierAssignment(this ref classFieldsTransformer tx, ж<privateIdentifierInfo> Ꮡinfo, ж<astꓸExpression> Ꮡreceiver, ж<astꓸExpression> Ꮡright, ast.Kind @operator) {
    ref var info = ref Ꮡinfo.DerefOrNull();
    ref var receiver = ref Ꮡreceiver.DerefOrNull();
    ref var right = ref Ꮡright.DerefOrNull();

    Ꮡreceiver = tx.Transformer.Visitor().VisitNode(Ꮡreceiver); receiver = ref Ꮡreceiver.DerefOrNull();
    Ꮡright = tx.Transformer.Visitor().VisitNode(Ꮡright); right = ref Ꮡright.DerefOrNull();

    if (ast.IsCompoundAssignment(@operator)) {
        var (readExpression, initializeExpression) = tx.createCopiableReceiverExpr(Ꮡreceiver);
        if (initializeExpression != nil) {
            Ꮡreceiver = initializeExpression; receiver = ref Ꮡreceiver.DerefOrNull();
        } else {
            Ꮡreceiver = readExpression; receiver = ref Ꮡreceiver.DerefOrNull();
        }
        Ꮡright = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(
            nil,
            tx.createPrivateIdentifierAccessHelper(Ꮡinfo, readExpression),
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(transformers.GetNonAssignmentOperatorForCompoundAssignment(@operator)),
            Ꮡright); right = ref Ꮡright.DerefOrNull();
    }

    tx.Transformer.EmitContext().SetCommentRange(Ꮡreceiver, core.NewTextRange(-1, receiver.End()));

    var exprᴛ1 = info.kind;
    switch (ᐧ) {
    case {} when exprᴛ1 == printer.PrivateIdentifierKindAccessor: {
        return tx.Transformer.Factory().NewClassPrivateFieldSetHelper(
            Ꮡreceiver,
            info.brandCheckIdentifier,
            Ꮡright,
            info.kind,
            info.setterName);
    }
    case {} when exprᴛ1 == printer.PrivateIdentifierKindMethod: {
        return tx.Transformer.Factory().NewClassPrivateFieldSetHelper(
            Ꮡreceiver,
            info.brandCheckIdentifier,
            Ꮡright,
            info.kind,
            nil);
    }
    case {} when exprᴛ1 == printer.PrivateIdentifierKindField: {
        ж<astꓸIdentifierNode> f = default;
        if (info.isStatic) {
            f = info.variableName;
        }
        return tx.Transformer.Factory().NewClassPrivateFieldSetHelper(
            Ꮡreceiver,
            info.brandCheckIdentifier,
            Ꮡright,
            info.kind,
            f);
    }
    case {} when exprᴛ1 == printer.PrivateIdentifierKindUntransformed: {
        debug.Fail("Access helpers should not be created for untransformed private elements");
        return default;
    }}

    debug.AssertNever(Ꮡinfo.OrTypedNil(), "Unknown private element type");
    return default;
}

internal static slice<ж<ast.Node>> getPrivateInstanceMethodsAndAccessors(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return core.Filter<ж<ast.Node>>(node.Members(), isNonStaticMethodOrAccessorWithPrivateName);
}

internal static bool memberContainsConstructorReference(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡmember, ж<ast.Node> classDecl) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var classOriginal = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(classDecl);
    var className = ast.GetNameOfDeclaration(classDecl);
    ref var check = ref heap<Func<ж<ast.Node>, bool>>(out var Ꮡcheck);
    var classNameʗ1 = className;
    var classOriginalʗ1 = classOriginal;
    check = (ж<ast.Node> n) => {
        if (ast.IsIdentifier(n) && n != classNameʗ1) {
            var decl = Ꮡtx.Value.resolver.GetReferencedValueDeclaration(n);
            if (decl == classOriginalʗ1) {
                return true;
            }
        }
        if (ast.IsPropertyAccessExpression(n)) {
            return Ꮡcheck.ValueSlot(n.Expression());
        }
        return n.ForEachChild(Ꮡcheck.ValueSlot);
    };
    if (ast.IsClassStaticBlockDeclaration(Ꮡmember)) {
        var body = member.AsClassStaticBlockDeclaration().Value.Body;
        if (body != nil && check(body.AsNode())) {
            return true;
        }
    } else {
        var body = member.Body();
        if (body != nil && check(body)) {
            return true;
        }
    }
    if (ast.IsPropertyDeclaration(Ꮡmember)) {
        var init = member.Initializer();
        if (init != nil && check(init)) {
            return true;
        }
    }
    return false;
}

internal static bool classContainsConstructorReference(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    foreach (var (_, member) in range(node.Members())) {
        if (Ꮡtx.memberContainsConstructorReference(member, Ꮡnode)) {
            return true;
        }
    }
    return false;
}

internal static classFacts getClassFacts(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    classFacts facts = classFactsNone;

    var original = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡnode);
    if (ast.IsClassLike(original) && ast.ClassOrConstructorParameterIsDecorated(tx.legacyDecorators, original)) {
        facts |= (classFacts)(classFactsClassWasDecorated);
    }

    if (tx.shouldTransformPrivateElementsOrClassStaticBlocks && (classHasClassThisAssignment(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), Ꮡnode) || classHasExplicitlyAssignedName(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), Ꮡnode))) {
        facts |= (classFacts)(classFactsNeedsClassConstructorReference);
    }

    bool containsPublicInstanceFields = default;
    bool containsInitializedPublicInstanceFields = default;
    bool containsInstancePrivateElements = default;
    bool containsInstanceAutoAccessors = default;

    foreach (var (_, member) in range(node.Members())) {
        if (ast.IsStatic(member)) {
            if (member.Name() != nil && (ast.IsPrivateIdentifier(member.Name()) || ast.IsAutoAccessorPropertyDeclaration(member)) && tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
                facts |= (classFacts)(classFactsNeedsClassConstructorReference);
            } else if (ast.IsAutoAccessorPropertyDeclaration(member) && tx.shouldTransformAutoAccessors && node.Name() == nil && Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode) == nil) {
                facts |= (classFacts)(classFactsNeedsClassConstructorReference);
            }
            if (ast.IsPropertyDeclaration(member) || ast.IsClassStaticBlockDeclaration(member)) {
                if (tx.shouldTransformThisInStaticInitializers && (ast.SubtreeFacts)(member.SubtreeFacts() & ast.SubtreeContainsLexicalThis) != 0) {
                    facts |= (classFacts)(classFactsNeedsSubstitutionForThisInClassStaticField);
                    if ((classFacts)(facts & classFactsClassWasDecorated) == 0) {
                        facts |= (classFacts)(classFactsNeedsClassConstructorReference);
                    }
                }
                if (tx.shouldTransformSuperInStaticInitializers && (ast.SubtreeFacts)(member.SubtreeFacts() & ast.SubtreeContainsLexicalSuper) != 0) {
                    if ((classFacts)(facts & classFactsClassWasDecorated) == 0) {
                        facts |= (classFacts)((classFacts)(classFactsNeedsClassConstructorReference | classFactsNeedsClassSuperReference));
                    }
                }
            }
        } else if (!ast.HasAbstractModifier(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(member))) {
            if (ast.IsAutoAccessorPropertyDeclaration(member)) {
                containsInstanceAutoAccessors = true;
                containsInstancePrivateElements = containsInstancePrivateElements || ast.IsPrivateIdentifierClassElementDeclaration(member);
            } else if (ast.IsPrivateIdentifierClassElementDeclaration(member)) {
                containsInstancePrivateElements = true;
                if (Ꮡtx.memberContainsConstructorReference(member, Ꮡnode)) {
                    facts |= (classFacts)(classFactsNeedsClassConstructorReference);
                }
            } else if (ast.IsPropertyDeclaration(member)) {
                containsPublicInstanceFields = true;
                containsInitializedPublicInstanceFields = containsInitializedPublicInstanceFields || member.Initializer() != nil;
            }
        }
    }

    var willHoistInitializersToConstructor = (tx.shouldTransformInitializersUsingDefine && containsPublicInstanceFields) || (tx.shouldTransformInitializersUsingSet && containsInitializedPublicInstanceFields) || (tx.shouldTransformPrivateElementsOrClassStaticBlocks && containsInstancePrivateElements) || (tx.shouldTransformPrivateElementsOrClassStaticBlocks && containsInstanceAutoAccessors && tx.shouldTransformAutoAccessors);

    if (willHoistInitializersToConstructor) {
        facts |= (classFacts)(classFactsWillHoistInitializersToConstructor);
    }

    return facts;
}

internal static ж<ast.Node> visitExpressionWithTypeArgumentsInHeritageClause(this ref classFieldsTransformer tx, ж<ast.ExpressionWithTypeArguments> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    classFacts facts = classFactsNone;
    if (tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
        facts = tx.lexicalEnvironment.data.facts;
    }
    if ((classFacts)(facts & classFactsNeedsClassSuperReference) != 0) {
        var temp = tx.Transformer.Factory().NewTempVariableEx(new printer.AutoGenerateOptions(
            Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes
        ));
        tx.Transformer.EmitContext().AddVariableDeclaration(temp);
        tx.getClassLexicalEnvironment().superClassReference = temp;
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExpressionWithTypeArguments(
            Ꮡnode,
            tx.Transformer.Factory().NewAssignmentExpression(temp, tx.Transformer.Visitor().VisitNode(node.Expression)),
            nil);
    }
    return tx.heritageClauseVisitor.VisitEachChild(Ꮡnode.of(ast.ExpressionWithTypeArguments.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitInNewClassLexicalEnvironment(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> node, Func<ж<classFieldsTransformer>, ж<ast.Node>, classFacts, ж<ast.Node>> visitor) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    var savedCurrentClassContainer = tx.currentClassContainer;
    var savedPendingExpressions = tx.pendingExpressions;
    var savedLexicalEnvironment = tx.lexicalEnvironment;
    tx.currentClassContainer = node;
    tx.pendingExpressions = default;
    tx.startClassLexicalEnvironment();
    var original = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(node);
    tx.enclosingClassDeclarations.Add(original);

    if (tx.shouldTransformPrivateElementsOrClassStaticBlocks || tx.nodeHasTransformPrivateStaticElementsFlag(node)) {
        var name = ast.GetNameOfDeclaration(node);
        if (name != nil && ast.IsIdentifier(name)) {
            tx.getPrivateIdentifierEnvironment().data.className = name;
        } else if (Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AssignedName(node) is var assignedName && assignedName != nil) {
            if (ast.IsStringLiteral(assignedName)) {
                var textSourceNode = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().TextSource(assignedName); if (textSourceNode != nil && ast.IsIdentifier(textSourceNode)) {
                    tx.getPrivateIdentifierEnvironment().data.className = textSourceNode;
                } else if (scanner.IsIdentifierText(assignedName.Text(), core.LanguageVariantStandard)) {
                    var prefixName = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(assignedName.Text());
                    tx.getPrivateIdentifierEnvironment().data.className = prefixName;
                }
            }
        }
    }

    if (tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
        var privateInstanceMethodsAndAccessors = tx.getPrivateInstanceMethodsAndAccessors(node);
        if (len(privateInstanceMethodsAndAccessors) > 0) {
            tx.getPrivateIdentifierEnvironment().data.weakSetName = tx.createHoistedVariableForClass(
                "instances",
                privateInstanceMethodsAndAccessors[0].Name(),
                "");
        }
    }

    classFacts facts = Ꮡtx.getClassFacts(node);
    if (facts != classFactsNone) {
        tx.getClassLexicalEnvironment().facts = facts;
    }

    var result = visitor(Ꮡtx, node, facts);
    tx.enclosingClassDeclarations.Delete(original);
    tx.endClassLexicalEnvironment();
    debug.Assert(tx.lexicalEnvironment == savedLexicalEnvironment);
    tx.currentClassContainer = savedCurrentClassContainer;
    tx.pendingExpressions = savedPendingExpressions;
    tx.lexicalEnvironment = savedLexicalEnvironment;
    return result;
}

internal static ж<ast.Node> visitClassDeclaration(this ж<classFieldsTransformer> Ꮡtx, ж<ast.ClassDeclaration> node) {
    return Ꮡtx.visitInNewClassLexicalEnvironment(node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ((Func<ж<classFieldsTransformer>, ж<ast.Node>, classFacts, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitClassDeclarationInNewClassLexicalEnvironment)));
}

internal static ж<ast.Node> visitClassDeclarationInNewClassLexicalEnvironment(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode, classFacts facts) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    var statementsᴷ = new stackAppend<ж<ast.Node>>();

    var classDecl = node.AsClassDeclaration();
    ж<astꓸExpression> pendingClassReferenceAssignment = default;
    if ((classFacts)(facts & classFactsNeedsClassConstructorReference) != 0) {
        if (tx.shouldTransformPrivateElementsOrClassStaticBlocks && Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode) != nil) {
            var classThis = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode);
            tx.getClassLexicalEnvironment().classConstructor = classThis;
            pendingClassReferenceAssignment = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(
                classThis,
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().GetLocalName(Ꮡnode));
        } else {
            var temp = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewTempVariableEx(new printer.AutoGenerateOptions(
                Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes
            ));
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddVariableDeclaration(temp);
            tx.getClassLexicalEnvironment().classConstructor = temp.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory()));
            pendingClassReferenceAssignment = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(
                temp,
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().GetLocalName(Ꮡnode));
        }
    }

    if (Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode) != nil) {
        tx.getClassLexicalEnvironment().classThis = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode);
    }

    var isClassWithConstructorReference = Ꮡtx.classContainsConstructorReference(Ꮡnode);

    var alias = tx.getClassLexicalEnvironment().classConstructor;
    if (isClassWithConstructorReference && alias != nil) {
        tx.classAliases[Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡnode)] = alias;
    }

    var modifiers = tx.modifierVisitor.VisitModifiers(classDecl.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers());
    var heritageClauses = tx.heritageClauseVisitor.VisitNodes(classDecl.HeritageClauses);
    var (members, membersPrologue) = Ꮡtx.transformClassMembers(Ꮡnode);

    slice<ж<ast.Node>> statements = default;

    if (pendingClassReferenceAssignment != nil) {
        tx.pendingExpressions = appendꓸꓸꓸ(slice<ж<astꓸExpression>>([pendingClassReferenceAssignment]), tx.pendingExpressions);
    }

    if (len(tx.pendingExpressions) > 0) {
        statements = append(statementsᴷ, statements, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().InlineExpressions(tx.pendingExpressions)));
    }

    var name = classDecl.Name();

    if (tx.shouldTransformInitializersUsingSet || tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
        var staticProperties = tx.getStaticPropertiesAndClassStaticBlock(Ꮡnode);
        if (len(staticProperties) > 0) {
            if (name == nil) {
                name = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewGeneratedNameForNode(Ꮡnode);
            }
            statements = Ꮡtx.addPropertyOrClassStaticBlockStatements(statements, staticProperties, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().GetLocalName(Ꮡnode));
        }
    }

    var isExport = ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsExport);
    var isDefault = ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsDefault);

    if (len(statements) > 0 && isExport && isDefault) {
        modifiers = transformers.ExtractModifiers(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), modifiers, ~((ast.ModifierFlags)ast.ModifierFlagsExportDefault));
        var exportAssignment = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExportAssignment(nil, false, nil, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().GetLocalName(Ꮡnode));
        statements = append(statements, exportAssignment);
    }

    var updatedClass = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
        classDecl,
        modifiers,
        name,
        nil,
        heritageClauses,
        members);

    var result = new slice<ж<ast.Node>>(0, 1 + len(statements) + 1);
    if (membersPrologue != nil) {
        result = append(result, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(membersPrologue));
    }
    result = append(result, updatedClass);
    result = appendꓸꓸꓸ(result, statements);
    return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(result);
}

internal static ж<ast.Node> visitClassExpression(this ж<classFieldsTransformer> Ꮡtx, ж<ast.ClassExpression> node) {
    return Ꮡtx.visitInNewClassLexicalEnvironment(node.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ((Func<ж<classFieldsTransformer>, ж<ast.Node>, classFacts, ж<ast.Node>>)(estransformsᴛclassFieldsTransformer.visitClassExpressionInNewClassLexicalEnvironment)));
}

internal static ж<ast.Node> visitClassExpressionInNewClassLexicalEnvironment(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode, classFacts facts) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    var expressionsᴷ = new stackAppend<ж<astꓸExpression>>();

    var classExpr = node.AsClassExpression();

    var isDecoratedClassDeclaration = (classFacts)(facts & classFactsClassWasDecorated) != 0;

    if (Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode) != nil) {
        tx.getClassLexicalEnvironment().classThis = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode);
    }

    ж<astꓸIdentifierNode> temp = default;
    if ((classFacts)(facts & classFactsNeedsClassConstructorReference) != 0) {
        if ((tx.shouldTransformPrivateElementsOrClassStaticBlocks || tx.nodeHasTransformPrivateStaticElementsFlag(Ꮡnode)) && Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode) != nil) {
            var classThis = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode);
            tx.getClassLexicalEnvironment().classConstructor = classThis;
            temp = classThis;
        } else {
            temp = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewTempVariableEx(new printer.AutoGenerateOptions(
                Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes
            ));
            if (tx.classExpressionNeedsBlockScopedTemp()) {
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddLexicalDeclaration(temp);
            } else {
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddVariableDeclaration(temp);
            }
            tx.getClassLexicalEnvironment().classConstructor = temp.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory()));
        }
    }

    var staticPropertiesOrClassStaticBlocks = tx.getStaticPropertiesAndClassStaticBlock(Ꮡnode);

    var isClassWithConstructorReference = false;
    var hasTransformableStatics = false;
    var deferTempDeclaration = false;
    if (!isDecoratedClassDeclaration) {
        isClassWithConstructorReference = Ꮡtx.classContainsConstructorReference(Ꮡnode);
        hasTransformableStatics = (tx.shouldTransformPrivateElementsOrClassStaticBlocks || tx.nodeHasTransformPrivateStaticElementsFlag(Ꮡnode)) && core.Some(staticPropertiesOrClassStaticBlocks, (ж<ast.Node> n) => ast.IsClassStaticBlockDeclaration(n) || ast.IsPrivateIdentifierClassElementDeclaration(n) || (Ꮡtx.Value.shouldTransformInitializers && ast.IsInitializedProperty(n)));

        var willHavePrivatePendingExpressions = tx.shouldTransformPrivateElementsOrClassStaticBlocks && core.Some(node.Members(), (ж<ast.Node> n) => ast.IsPrivateIdentifierClassElementDeclaration(n) && !ast.HasStaticModifier(n) && Ꮡtx.Value.shouldTransformClassElementToWeakMap(n));
        var willNeedTempWrapper = hasTransformableStatics || willHavePrivatePendingExpressions;

        if (isClassWithConstructorReference && willNeedTempWrapper && tx.getClassLexicalEnvironment().classConstructor == nil) {
            temp = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewTempVariableEx(new printer.AutoGenerateOptions(
                Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes
            ));
            deferTempDeclaration = true;
            tx.getClassLexicalEnvironment().classConstructor = temp.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory()));
        }
        var alias = tx.getClassLexicalEnvironment().classConstructor; if (isClassWithConstructorReference && willNeedTempWrapper && alias != nil) {
            tx.classAliases[Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡnode)] = alias;
        }
    }

    var modifiers = tx.modifierVisitor.VisitModifiers(classExpr.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers());
    var heritageClauses = tx.heritageClauseVisitor.VisitNodes(classExpr.HeritageClauses);
    var (members, membersPrologue) = Ꮡtx.transformClassMembers(Ꮡnode);

    if (deferTempDeclaration) {
        if (tx.classExpressionNeedsBlockScopedTemp()) {
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddLexicalDeclaration(temp);
        } else {
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddVariableDeclaration(temp);
        }
    }

    var classExpression = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(
        classExpr,
        modifiers,
        classExpr.Name(),
        nil,
        heritageClauses,
        members);

    slice<ж<astꓸExpression>> expressions = default;
    if (membersPrologue != nil) {
        expressions = append(expressionsᴷ, expressions, membersPrologue);
    }

    if (!isDecoratedClassDeclaration) {
        if (hasTransformableStatics || len(tx.pendingExpressions) > 0) {
            if (temp == nil) {
                temp = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewTempVariableEx(new printer.AutoGenerateOptions(
                    Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes
                ));
                if (tx.classExpressionNeedsBlockScopedTemp()) {
                    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddLexicalDeclaration(temp);
                } else {
                    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddVariableDeclaration(temp);
                }
                tx.getClassLexicalEnvironment().classConstructor = temp.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory()));
                if (isClassWithConstructorReference) {
                    tx.classAliases[Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡnode)] = tx.getClassLexicalEnvironment().classConstructor;
                }
            }

            expressions = append(expressions, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(temp, classExpression));

            expressions = appendꓸꓸꓸ(expressions, tx.pendingExpressions);

            expressions = appendꓸꓸꓸ(expressions, Ꮡtx.generateInitializedPropertyExpressionsOrClassStaticBlock(staticPropertiesOrClassStaticBlocks, temp));
            expressions = append(expressions, temp.Clone(new printer.NodeFactoryжNodeFactoryCoercible(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory())));
        } else {
            expressions = append(expressions, classExpression);
        }
    } else {
        if (len(tx.pendingExpressions) > 0) {
            foreach (var (_, expr) in range(tx.pendingExpressions)) {
                tx.pendingStatements = append(tx.pendingStatements, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expr));
            }
        }

        if (len(staticPropertiesOrClassStaticBlocks) > 0) {
            var classThisOrName = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode);
            if (classThisOrName == nil) {
                classThisOrName = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().GetLocalName(Ꮡnode);
            }
            tx.pendingStatements = Ꮡtx.addPropertyOrClassStaticBlockStatements(tx.pendingStatements, staticPropertiesOrClassStaticBlocks, classThisOrName);
        }

        if (temp != nil) {
            expressions = append(expressions, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(temp, classExpression));
        } else if (tx.shouldTransformPrivateElementsOrClassStaticBlocks && Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode) != nil) {
            expressions = append(expressions, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().ClassThis(Ꮡnode), classExpression));
        } else {
            expressions = append(expressions, classExpression);
        }
    }

    if (len(expressions) > 1) {
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddEmitFlags(classExpression, printer.EFIndented);
        foreach (var (_, expr) in range(expressions)) {
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddEmitFlags(expr, printer.EFStartOnNewLine);
        }
    }
    return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().InlineExpressions(expressions);
}

internal static ж<ast.Node> visitClassStaticBlockDeclaration(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    if (!tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
        return tx.Transformer.Visitor().VisitEachChild(node);
    }
    return default;
}

internal static ж<ast.Node> visitThisExpression(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    if (tx.insideComputedPropertyName && tx.shouldTransformThisInStaticInitializers && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
        if ((classFacts)(tx.lexicalEnvironment.data.facts & classFactsClassWasDecorated) == 0 || tx.legacyDecorators) {
            var classThis = tx.tryGetClassThisNoContainer(); if (classThis != nil) {
                return classThis;
            }
        }
    }
    if (tx.shouldTransformThisInStaticInitializers && tx.currentClassElement != nil && (ast.IsClassStaticBlockDeclaration(tx.currentClassElement) || (ast.IsPropertyDeclaration(tx.currentClassElement) && ast.HasStaticModifier(tx.currentClassElement))) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil) {
        var classThis = tx.tryGetClassThisNoContainer(); if (classThis != nil) {
            return classThis;
        }
        if ((classFacts)(tx.lexicalEnvironment.data.facts & classFactsClassWasDecorated) != 0 && tx.legacyDecorators) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression(tx.Transformer.Factory().NewVoidZeroExpression());
        }
    }
    return node;
}

internal static (ж<ast.NodeList> members, ж<astꓸExpression> prologue) transformClassMembers(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ж<ast.NodeList> members = default;
    ж<astꓸExpression> prologue = default;
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var shouldTransformPrivateStaticElementsInClass = (printer.EmitFlags)(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().EmitFlags(Ꮡnode) & printer.EFTransformPrivateStaticElements) != 0;

    if (tx.shouldTransformPrivateElementsOrClassStaticBlocks || tx.shouldTransformPrivateStaticElementsInFile) {
        foreach (var (_, member) in range(node.Members())) {
            if (ast.IsPrivateIdentifierClassElementDeclaration(member)) {
                if (tx.shouldTransformClassElementToWeakMap(member)) {
                    tx.addPrivateIdentifierToEnvironment(member);
                } else {
                    var env = tx.getPrivateIdentifierEnvironment();
                    tx.setPrivateIdentifier(env, member.Name(), Ꮡ(new privateIdentifierInfo(
                        kind: printer.PrivateIdentifierKindUntransformed
                    )));
                }
            }
        }

        if (tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
            if (len(tx.getPrivateInstanceMethodsAndAccessors(Ꮡnode)) > 0) {
                tx.createBrandCheckWeakSetForPrivateMethods();
            }
        }

        if (tx.shouldTransformAutoAccessorsInCurrentClass()) {
            foreach (var (_, member) in range(node.Members())) {
                if (ast.IsAutoAccessorPropertyDeclaration(member)) {
                    var storageName = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewGeneratedPrivateNameForNodeEx(member.Name(), new printer.AutoGenerateOptions(Suffix: "_accessor_storage"));
                    if (tx.shouldTransformPrivateElementsOrClassStaticBlocks || shouldTransformPrivateStaticElementsInClass && ast.HasStaticModifier(member)) {
                        tx.addPrivateIdentifierPropertyDeclarationToEnvironment(member, storageName);
                    } else {
                        var env = tx.getPrivateIdentifierEnvironment();
                        var (_, ok) = tx.getPrivateIdentifier(env, storageName); if (!ok) {
                            tx.setPrivateIdentifier(env, storageName, Ꮡ(new privateIdentifierInfo(
                                kind: printer.PrivateIdentifierKindUntransformed
                            )));
                        }
                    }
                }
            }
        }
    }

    members = tx.classElementVisitor.VisitNodes(node.MemberList());

    ж<ast.Node> syntheticConstructor = default;
    if (!core.Some<ж<ast.Node>>(members.Nodes, ast.IsConstructorDeclaration)) {
        syntheticConstructor = Ꮡtx.transformConstructor(nil, Ꮡnode);
    }

    ж<ast.Node> syntheticStaticBlock = default;
    if (!tx.shouldTransformPrivateElementsOrClassStaticBlocks && len(tx.pendingExpressions) > 0) {
        var statement = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().InlineExpressions(tx.pendingExpressions));
        if ((ast.SubtreeFacts)(statement.SubtreeFacts() & ast.SubtreeContainsLexicalThisOrSuper) != 0) {
            var temp = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewTempVariable();
            Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddVariableDeclaration(temp);
            var arrow = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrowFunction(
                nil,
                nil,
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default),
                nil,
                nil,
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsGreaterThanToken),
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([statement]), false));
            prologue = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(temp, arrow);
            statement = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(temp, nil, nil, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), ast.NodeFlagsNone));
        }

        var block = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([statement]), false);
        syntheticStaticBlock = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(nil, block);
        tx.pendingExpressions = default;
    }

    if (syntheticConstructor != nil || syntheticStaticBlock != nil) {
        var membersArray = new slice<ж<ast.Node>>(0, len(members.Nodes) + 2);

        nint classThisIdx = slices.IndexFunc(members.Nodes, (ж<ast.Node> n) => isClassThisAssignmentBlock(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), n));
        nint namedEvalIdx = slices.IndexFunc(members.Nodes, (ж<ast.Node> n) => isClassNamedEvaluationHelperBlock(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext(), n));

        if (classThisIdx >= 0) {
            membersArray = append(membersArray, members.Nodes[classThisIdx]);
        }
        if (namedEvalIdx >= 0) {
            membersArray = append(membersArray, members.Nodes[namedEvalIdx]);
        }
        if (syntheticConstructor != nil) {
            membersArray = append(membersArray, syntheticConstructor);
        }
        if (syntheticStaticBlock != nil) {
            membersArray = append(membersArray, syntheticStaticBlock);
        }

        foreach (var (i, member) in range(members.Nodes)) {
            if (i != classThisIdx && i != namedEvalIdx) {
                membersArray = append(membersArray, member);
            }
        }
        members = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(membersArray);
        members.Loc = node.MemberList().Loc;
    }

    return (members, prologue);
}

internal static void createBrandCheckWeakSetForPrivateMethods(this ref classFieldsTransformer tx) {
    var env = tx.getPrivateIdentifierEnvironment();
    var weakSetName = env.data.weakSetName;
    debug.Assert(weakSetName != nil, "weakSetName should be set in private identifier environment");

    tx.addPendingExpressions(
        tx.Transformer.Factory().NewAssignmentExpression(
            weakSetName,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNewExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("WeakSet"),
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default))));
}

internal static ж<ast.Node> transformConstructor(this ж<classFieldsTransformer> Ꮡtx, ж<ast.ConstructorDeclaration> Ꮡconstructor, ж<ast.Node> Ꮡcontainer) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var constructor = ref Ꮡconstructor.DerefOrNull();
    ref var container = ref Ꮡcontainer.DerefOrNull();

    if (tx.lexicalEnvironment == nil || tx.lexicalEnvironment.data == nil || (classFacts)(tx.lexicalEnvironment.data.facts & classFactsWillHoistInitializersToConstructor) == 0) {
        if (Ꮡconstructor != nil) {
            return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡconstructor.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }
        return default;
    }

    var extendsClauseElement = ast.GetClassExtendsHeritageElement(Ꮡcontainer);
    var isDerivedClass = extendsClauseElement != nil && ast.SkipOuterExpressions(extendsClauseElement.Expression(), ast.OEKAll).Kind != ast.KindNullKeyword;

    ж<ast.NodeList> parameters = default;
    if (Ꮡconstructor != nil) {
        parameters = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitNodes(constructor.Parameters);
    }

    var body = Ꮡtx.transformConstructorBody(Ꮡcontainer, Ꮡconstructor, isDerivedClass);
    if (body == nil) {
        if (Ꮡconstructor != nil) {
            return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡconstructor.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }
        return default;
    }

    if (Ꮡconstructor != nil) {
        debug.Assert(parameters != nil);
        return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(
            Ꮡconstructor,
            nil,
            nil,
            parameters,
            nil,
            nil,
            body);
    }

    if (parameters == nil) {
        parameters = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default);
    }

    var result = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewConstructorDeclaration(
        nil,
        nil,
        parameters,
        nil,
        nil,
        body);
    result.Loc = container.Loc;
    return result;
}

internal static slice<ж<astꓸStatement>> transformConstructorBodyWorker(this ref classFieldsTransformer tx, slice<ж<astꓸStatement>> statementsOut, slice<ж<astꓸStatement>> statementsIn, nint statementOffset, slice<nint> superPath, nint superPathDepth, slice<ж<astꓸStatement>> initializerStatements, ж<ast.ConstructorDeclaration> constructor) {
    var statementsOutᴷ = new stackAppend<ж<astꓸStatement>>(moves: true);

    nint superStatementIndex = superPath[superPathDepth];
    var superStatement = statementsIn[superStatementIndex];

    var (visited, _) = tx.Transformer.Visitor().VisitSlice(statementsIn.slice(statementOffset, superStatementIndex));
    statementsOut = appendꓸꓸꓸ(statementsOut, visited);
    statementOffset = superStatementIndex + 1;

    if (ast.IsTryStatement(superStatement)) {
        var tryBlock = superStatement.AsTryStatement().TryBlock.AsBlock();
        var tryBlockStatements = tx.transformConstructorBodyWorker(
            default,
            (~tryBlock).Statements.Nodes,
            0,
            superPath,
            superPathDepth + 1,
            initializerStatements,
            constructor);
        var tryStatementList = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(tryBlockStatements);
        tryStatementList.Loc = tryBlock.Value.Statements.Loc;

        var catchClause = tx.Transformer.Visitor().VisitNode(superStatement.AsTryStatement().CatchClause);
        var finallyBlock = tx.Transformer.Visitor().VisitNode(superStatement.AsTryStatement().FinallyBlock);

        var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTryStatement(
            superStatement.AsTryStatement(),
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBlock(tryBlock, tryStatementList, tryBlock.MultiLine),
            catchClause,
            finallyBlock);
        statementsOut = append(statementsOutᴷ, statementsOut, updated);
    } else {
        var (visitedΔ1, _) = tx.Transformer.Visitor().VisitSlice(statementsIn.slice(superStatementIndex, superStatementIndex + 1));
        statementsOut = appendꓸꓸꓸ(statementsOut, visitedΔ1);

        while (statementOffset < len(statementsIn)) {
            var stmt = statementsIn[statementOffset];
            var orig = tx.Transformer.EmitContext().MostOriginal(stmt);
            if (ast.IsParameterPropertyDeclaration(orig, constructor.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
                statementOffset++;
            } else {
                break;
            }
        }

        statementsOut = appendꓸꓸꓸ(statementsOut, initializerStatements);
    }

    var (visited2, _) = tx.Transformer.Visitor().VisitSlice(statementsIn.slice(statementOffset));
    statementsOut = appendꓸꓸꓸ(statementsOut, visited2);
    statementsOut = move2heap(statementsOutᴷ, statementsOut);
    return statementsOut;
}

internal static ж<ast.Node> transformConstructorBody(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡcontainer, ж<ast.ConstructorDeclaration> Ꮡconstructor, bool isDerivedClass) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var container = ref Ꮡcontainer.DerefOrNull();
    ref var constructor = ref Ꮡconstructor.DerefOrNull();

    var instanceProperties = tx.getProperties(Ꮡcontainer, false, false);
    var properties = instanceProperties;
    if (!tx.compilerOptions.GetUseDefineForClassFields()) {
        properties = core.Filter(properties, (ж<ast.Node> prop) => prop.Initializer() != nil || ast.IsPrivateIdentifier(prop.Name()) || ast.HasAccessorModifier(prop));
    }

    var privateMethodsAndAccessors = tx.getPrivateInstanceMethodsAndAccessors(Ꮡcontainer);
    var needsConstructorBody = len(properties) > 0 || len(privateMethodsAndAccessors) > 0;

    if (Ꮡconstructor == nil && !needsConstructorBody) {
        return Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().VisitFunctionBody(nil, Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor());
    }

    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().StartVariableEnvironment();

    var needsSyntheticConstructor = Ꮡconstructor == nil && isDerivedClass;
    slice<ж<astꓸStatement>> statements = default;

    slice<ж<astꓸStatement>> initializerStatements = default;
    var receiver = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().NewThisExpression();

    initializerStatements = tx.addInstanceMethodStatements(initializerStatements, privateMethodsAndAccessors, receiver);

    if (Ꮡconstructor != nil) {
        var parameterProperties = core.Filter(instanceProperties, (ж<ast.Node> prop) => ast.IsParameterPropertyDeclaration(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(prop), Ꮡconstructor.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
        var nonParameterProperties = core.Filter(properties, (ж<ast.Node> prop) => !ast.IsParameterPropertyDeclaration(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(prop), Ꮡconstructor.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
        initializerStatements = Ꮡtx.addPropertyOrClassStaticBlockStatements(initializerStatements, parameterProperties, receiver);
        initializerStatements = Ꮡtx.addPropertyOrClassStaticBlockStatements(initializerStatements, nonParameterProperties, receiver);
    } else {
        initializerStatements = Ꮡtx.addPropertyOrClassStaticBlockStatements(initializerStatements, properties, receiver);
    }

    if (Ꮡconstructor != nil && constructor.Body != nil) {
        var body = constructor.Body.AsBlock();

        foreach (var (_, stmt) in range((~body).Statements.Nodes)) {
            if (ast.IsPrologueDirective(stmt)) {
                statements = append(statements, stmt);
            } else {
                break;
            }
        }
        nint statementOffset = len(statements);

        var superPath = transformers.FindSuperStatementIndexPath((~body).Statements.Nodes, statementOffset);
        if (len(superPath) > 0) {
            statements = tx.transformConstructorBodyWorker(statements, (~body).Statements.Nodes, statementOffset, superPath, 0, initializerStatements, Ꮡconstructor);
        } else {
            while (statementOffset < len((~body).Statements.Nodes)) {
                var stmt = (~body).Statements.Nodes[statementOffset];
                var orig = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(stmt);
                if (ast.IsParameterPropertyDeclaration(orig, Ꮡconstructor.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
                    statementOffset++;
                } else {
                    break;
                }
            }
            statements = appendꓸꓸꓸ(statements, initializerStatements);
            var (visited, _) = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Visitor().VisitSlice((~body).Statements.Nodes.slice(statementOffset));
            statements = appendꓸꓸꓸ(statements, visited);
        }
    } else {
        if (needsSyntheticConstructor) {
            var superCall = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
                    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindSuperKeyword),
                    nil,
                    nil,
                    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSpreadElement(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("arguments"))
                    ])),
                    ast.NodeFlagsNone));
            statements = append(statements, superCall);
        }
        statements = appendꓸꓸꓸ(statements, initializerStatements);
    }

    statements = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().EndAndMergeVariableEnvironment(statements);

    if (len(statements) == 0 && Ꮡconstructor == nil) {
        return default;
    }

    bool multiLine = default;
    if (Ꮡconstructor != nil && constructor.Body != nil && len((~constructor.Body.AsBlock()).Statements.Nodes) >= len(statements)) {
        multiLine = constructor.Body.AsBlock().MultiLine;
    } else {
        multiLine = len(statements) > 0;
    }

    var statementList = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements);
    if (Ꮡconstructor != nil && constructor.Body != nil) {
        statementList.Loc = constructor.Body.AsBlock().Value.Statements.Loc;
    } else {
        statementList.Loc = core.NewTextRange(container.MemberList().Loc.Pos(), container.MemberList().Loc.End());
    }

    var block = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(statementList, multiLine);
    if (Ꮡconstructor != nil && constructor.Body != nil) {
        block.Loc = constructor.Body.Loc;
    }
    return block;
}

internal static slice<ж<ast.Node>> addPropertyOrClassStaticBlockStatements(this ж<classFieldsTransformer> Ꮡtx, slice<ж<ast.Node>> statements, slice<ж<ast.Node>> properties, ж<astꓸExpression> receiver) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    var statementsᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    foreach (var (_, property) in range(properties)) {
        if (ast.IsStatic(property) && !tx.shouldTransformPrivateElementsOrClassStaticBlocks) {
            continue;
        }
        var statement = Ꮡtx.transformPropertyOrClassStaticBlock(property, receiver);
        if (statement != nil) {
            statements = append(statementsᴷ, statements, statement);
        }
    }
    statements = move2heap(statementsᴷ, statements);
    return statements;
}

internal static ж<ast.Node> transformPropertyOrClassStaticBlock(this ж<classFieldsTransformer> Ꮡtx, ж<ast.Node> Ꮡproperty, ж<astꓸExpression> receiver) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var property = ref Ꮡproperty.DerefOrNull();

    ж<astꓸExpression> expression = default;
    if (ast.IsClassStaticBlockDeclaration(Ꮡproperty)) {
        expression = Ꮡtx.setCurrentClassElementAnd(Ꮡproperty, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<astꓸExpression>>)(estransformsᴛclassFieldsTransformer.transformClassStaticBlockDeclaration)), Ꮡproperty);
    } else {
        expression = tx.transformProperty(property.AsPropertyDeclaration(), receiver);
    }
    if (expression == nil) {
        return default;
    }

    var statement = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetOriginal(statement, Ꮡproperty);
    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, (printer.EmitFlags)(Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().EmitFlags(Ꮡproperty) & printer.EFNoComments));
    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetCommentRange(statement, property.Loc);

    var propertyOriginalNode = Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡproperty);
    if (ast.IsParameterDeclaration(propertyOriginalNode)) {
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetSourceMapRange(statement, propertyOriginalNode.Loc);
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFNoComments);
    } else {
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetSourceMapRange(statement, transformers.MoveRangePastModifiers(Ꮡproperty));
    }

    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetSyntheticLeadingComments(expression, default);
    Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetSyntheticTrailingComments(expression, default);

    if (ast.HasAccessorModifier(propertyOriginalNode)) {
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AddEmitFlags(statement, printer.EFNoComments);
    }

    return statement;
}

internal static slice<ж<astꓸExpression>> generateInitializedPropertyExpressionsOrClassStaticBlock(this ж<classFieldsTransformer> Ꮡtx, slice<ж<ast.Node>> propertiesOrClassStaticBlocks, ж<astꓸExpression> receiver) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    var expressionsᴷ = new stackAppend<ж<astꓸExpression>>(moves: true);

    slice<ж<astꓸExpression>> expressions = default;
    foreach (var (_, property) in range(propertiesOrClassStaticBlocks)) {
        ж<astꓸExpression> expression = default;
        if (ast.IsClassStaticBlockDeclaration(property)) {
            expression = Ꮡtx.setCurrentClassElementAnd(property, ((Func<ж<classFieldsTransformer>, ж<ast.Node>, ж<astꓸExpression>>)(estransformsᴛclassFieldsTransformer.transformClassStaticBlockDeclaration)), property);
        } else {
            expression = tx.transformProperty(property.AsPropertyDeclaration(), receiver);
        }
        if (expression == nil) {
            continue;
        }
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().SetOriginalEx(expression, property, true);
        Ꮡtx.of(classFieldsTransformer.ᏑTransformer).EmitContext().AssignCommentAndSourceMapRanges(expression, property);
        expressions = append(expressionsᴷ, expressions, expression);
    }
    expressions = move2heap(expressionsᴷ, expressions);
    return expressions;
}

internal static ж<astꓸExpression> transformProperty(this ref classFieldsTransformer tx, ж<ast.PropertyDeclaration> Ꮡproperty, ж<astꓸExpression> receiver) {
    ref var property = ref Ꮡproperty.DerefOrNull();

    var savedCurrentClassElement = tx.currentClassElement;
    var transformed = tx.transformPropertyWorker(Ꮡproperty, receiver);
    if (transformed != nil && ast.HasStaticModifier(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        tx.Transformer.EmitContext().AddEmitFlags(transformed, printer.EFNoLexicalThis);
    }
    if (transformed != nil && ast.HasStaticModifier(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && tx.lexicalEnvironment != nil && tx.lexicalEnvironment.data != nil && tx.lexicalEnvironment.data.facts != 0) {
        tx.Transformer.EmitContext().SetOriginal(transformed, Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().SetSourceMapRange(transformed, tx.Transformer.EmitContext().SourceMapRange(property.Name()));
    }
    tx.currentClassElement = savedCurrentClassElement;
    return transformed;
}

internal static ж<astꓸExpression> transformPropertyWorker(this ref classFieldsTransformer tx, ж<ast.PropertyDeclaration> Ꮡproperty, ж<astꓸExpression> receiver) {
    ref var property = ref Ꮡproperty.DerefOrNull();

    var emitAssignment = !tx.compilerOptions.GetUseDefineForClassFields();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡproperty = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false, "").AsPropertyDeclaration(); property = ref Ꮡproperty.DerefOrNull();
    }

    var propertyName = property.Name();
    if (ast.HasAccessorModifier(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        propertyName = tx.Transformer.Factory().NewGeneratedPrivateNameForNodeEx(property.Name(), new printer.AutoGenerateOptions(Suffix: "_accessor_storage"));
    } else if (ast.IsComputedPropertyName(propertyName) && !transformers.IsSimpleInlineableExpression(propertyName.Expression())) {
        propertyName = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(
            propertyName.AsComputedPropertyName(),
            tx.Transformer.Factory().NewGeneratedNameForNode(propertyName));
    }

    if (ast.HasStaticModifier(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        tx.currentClassElement = Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    if (ast.IsPrivateIdentifier(propertyName) && tx.shouldTransformClassElementToWeakMap(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var info = tx.accessPrivateIdentifier(propertyName);
        if (info != nil) {
            if (info.kind == printer.PrivateIdentifierKindField) {
                if (!info.isStatic) {
                    return createPrivateInstanceFieldInitializer(
                        tx.Transformer.Factory(),
                        receiver,
                        tx.Transformer.Visitor().VisitNode(property.Initializer),
                        info.brandCheckIdentifier);
                }
                return createPrivateStaticFieldInitializer(
                    tx.Transformer.Factory(),
                    info.variableName,
                    tx.Transformer.Visitor().VisitNode(property.Initializer));
            }
            return default;
        } else {
            debug.Fail("Undeclared private name for property declaration.");
        }
    }

    if ((ast.IsPrivateIdentifier(propertyName) || ast.HasStaticModifier(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) && property.Initializer == nil) {
        return default;
    }

    if (ast.HasAbstractModifier(tx.Transformer.EmitContext().MostOriginal(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))) {
        return default;
    }

    var initializer = tx.Transformer.Visitor().VisitNode(property.Initializer);
    var propertyOriginalNode = tx.Transformer.EmitContext().MostOriginal(Ꮡproperty.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (ast.IsParameterPropertyDeclaration(propertyOriginalNode, propertyOriginalNode.Parent) && ast.IsIdentifier(propertyName)) {
        var localName = propertyName.Clone(new printer.NodeFactoryжNodeFactoryCoercible(tx.Transformer.Factory()));
        if (initializer != nil) {
            if (ast.IsParenthesizedExpression(initializer) && ast.IsCommaExpression(initializer.Expression()) && tx.Transformer.EmitContext().IsCallToHelper(initializer.Expression().AsBinaryExpression().Left, "__runInitializers") && ast.IsVoidExpression(initializer.Expression().AsBinaryExpression().Right) && ast.IsNumericLiteral(initializer.Expression().AsBinaryExpression().Right.Expression())) {
                initializer = initializer.Expression().AsBinaryExpression().Left;
            }
            initializer = tx.Transformer.Factory().InlineExpressions([initializer, localName]);
        } else {
            initializer = localName;
        }
        tx.Transformer.EmitContext().AddEmitFlags(propertyName, (printer.EmitFlags)(printer.EFNoComments | printer.EFNoSourceMap));
        tx.Transformer.EmitContext().SetSourceMapRange(localName, propertyOriginalNode.Name().Loc);
        tx.Transformer.EmitContext().AddEmitFlags(localName, printer.EFNoComments);
    } else if (initializer == nil) {
        initializer = tx.Transformer.Factory().NewVoidZeroExpression();
    }

    if (emitAssignment || ast.IsPrivateIdentifier(propertyName)) {
        var memberAccess = createMemberAccessForPropertyName(tx.Transformer.Factory(), tx.Transformer.EmitContext(), receiver, propertyName, ref (propertyName).DerefOrNull());
        tx.Transformer.EmitContext().AddEmitFlags(memberAccess, printer.EFNoLeadingComments);
        return tx.Transformer.Factory().NewAssignmentExpression(memberAccess, initializer);
    }

    ж<astꓸExpression> name = default;
    if (ast.IsComputedPropertyName(propertyName)) {
        name = propertyName.Expression();
    } else if (ast.IsIdentifier(propertyName)) {
        name = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(propertyName.Text(), ast.TokenFlagsNone);
    } else {
        name = propertyName;
    }
    var descriptor = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("enumerable"), nil, nil, tx.Transformer.Factory().NewTrueExpression()),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("configurable"), nil, nil, tx.Transformer.Factory().NewTrueExpression()),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("writable"), nil, nil, tx.Transformer.Factory().NewTrueExpression()),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), nil, nil, initializer)
    ])), true);
    return tx.Transformer.Factory().NewObjectDefinePropertyCall(receiver, name, descriptor);
}

internal static slice<ж<astꓸStatement>> addInstanceMethodStatements(this ref classFieldsTransformer tx, slice<ж<astꓸStatement>> statements, slice<ж<ast.Node>> methods, ж<astꓸExpression> receiver) {
    if (!tx.shouldTransformPrivateElementsOrClassStaticBlocks || len(methods) == 0) {
        return statements;
    }

    var env = tx.getPrivateIdentifierEnvironment();
    var weakSetName = env.data.weakSetName;
    debug.Assert(weakSetName != nil, "weakSetName should be set in private identifier environment");

    return append(
        statements,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
            createPrivateInstanceMethodInitializer(tx.Transformer.Factory(), receiver, weakSetName)));
}

internal static ж<ast.Node> visitInvalidSuperProperty(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPropertyAccessExpression(Ꮡnode)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAccessExpression(
            node.AsPropertyAccessExpression(),
            tx.Transformer.Factory().NewVoidZeroExpression(),
            nil,
            node.Name(),
            node.Flags);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateElementAccessExpression(
        node.AsElementAccessExpression(),
        tx.Transformer.Factory().NewVoidZeroExpression(),
        nil,
        tx.Transformer.Visitor().VisitNode(node.AsElementAccessExpression().ArgumentExpression),
        node.Flags);
}

internal static ж<astꓸExpression> getPropertyNameExpressionIfNeeded(this ref classFieldsTransformer tx, ж<astꓸPropertyName> Ꮡname, bool shouldHoist) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (!ast.IsComputedPropertyName(Ꮡname)) {
        return default;
    }
    var cacheAssignment = findComputedPropertyNameCacheAssignment(ref (tx.Transformer.EmitContext()).DerefOrNull(), Ꮡname);
    var savedLexicalEnvironment = tx.lexicalEnvironment;
    var savedInsideComputedPropertyName = tx.insideComputedPropertyName;
    tx.insideComputedPropertyName = true;
    if (tx.lexicalEnvironment != nil && tx.lexicalEnvironment.previous != nil) {
        tx.lexicalEnvironment = tx.lexicalEnvironment.previous;
    }
    var expression = tx.Transformer.Visitor().VisitNode(name.Expression());
    tx.lexicalEnvironment = savedLexicalEnvironment;
    tx.insideComputedPropertyName = savedInsideComputedPropertyName;
    var innerExpression = ast.SkipPartiallyEmittedExpressions(expression);
    var inlinable = transformers.IsSimpleInlineableExpression(innerExpression);
    var alreadyTransformed = cacheAssignment != nil || (ast.IsAssignmentExpression(innerExpression, true) && ast.IsIdentifier(innerExpression.AsBinaryExpression().Left) && transformers.IsGeneratedIdentifier(tx.Transformer.EmitContext(), innerExpression.AsBinaryExpression().Left));
    if (!alreadyTransformed && !inlinable && shouldHoist) {
        var generatedName = tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡname);
        if (tx.requiresBlockScopedVar()) {
            tx.Transformer.EmitContext().AddLexicalDeclaration(generatedName);
        } else {
            tx.Transformer.EmitContext().AddVariableDeclaration(generatedName);
        }
        return tx.Transformer.Factory().NewAssignmentExpression(generatedName, expression);
    }
    if (inlinable || ast.IsIdentifier(innerExpression)) {
        return default;
    }
    return expression;
}

internal static void startClassLexicalEnvironment(this ref classFieldsTransformer tx) {
    tx.lexicalEnvironment = Ꮡ(new classLexicalEnv(previous: tx.lexicalEnvironment));
}

internal static void endClassLexicalEnvironment(this ref classFieldsTransformer tx) {
    tx.lexicalEnvironment = tx.lexicalEnvironment.previous;
}

internal static ж<classLexicalEnvironment> getClassLexicalEnvironment(this ref classFieldsTransformer tx) {
    debug.Assert(tx.lexicalEnvironment != nil);
    if (tx.lexicalEnvironment.data == nil) {
        tx.lexicalEnvironment.data = Ꮡ(new classLexicalEnvironment());
    }
    return tx.lexicalEnvironment.data;
}

internal static ж<privateEnvironment> getPrivateIdentifierEnvironment(this ref classFieldsTransformer tx) {
    debug.Assert(tx.lexicalEnvironment != nil);
    if (tx.lexicalEnvironment.privateEnv == nil) {
        tx.lexicalEnvironment.privateEnv = Ꮡ(new privateEnvironment(
            members: new map<@string, ж<privateIdentifierInfo>>()
        ));
    }
    return tx.lexicalEnvironment.privateEnv;
}

internal static void addPendingExpressions(this ref classFieldsTransformer tx, params ꓸꓸꓸжastꓸExpression exprsʗp) {
    var exprs = exprsʗp.sslice();

    tx.pendingExpressions = appendꓸꓸꓸ(tx.pendingExpressions, exprs);
}

internal static void addPrivateIdentifierPropertyDeclarationToEnvironment(this ref classFieldsTransformer tx, ж<ast.Node> node, ж<ast.Node> name) {
    var lex = tx.getClassLexicalEnvironment();
    var env = tx.getPrivateIdentifierEnvironment();
    var isStatic = ast.HasStaticModifier(node);
    var (previousInfo, _) = tx.getPrivateIdentifier(env, name);
    ref var isValid = ref heap<bool>(!tx.isReservedPrivateName(name) && previousInfo == nil, out var ᏑisValid);

    if (isStatic) {
        var brandCheckIdentifier = lex.classThis;
        if (brandCheckIdentifier == nil) {
            brandCheckIdentifier = lex.classConstructor;
        }
        var variableName = tx.createHoistedVariableForPrivateName(name, "");
        tx.setPrivateIdentifier(env, name, Ꮡ(new privateIdentifierInfo(
            kind: printer.PrivateIdentifierKindField,
            isStatic: true,
            brandCheckIdentifier: brandCheckIdentifier,
            variableName: variableName,
            isValid: isValid
        )));
    } else {
        var weakMapName = tx.createHoistedVariableForPrivateName(name, "");
        tx.setPrivateIdentifier(env, name, Ꮡ(new privateIdentifierInfo(
            kind: printer.PrivateIdentifierKindField,
            isStatic: false,
            brandCheckIdentifier: weakMapName,
            isValid: isValid
        )));
        tx.addPendingExpressions(
            tx.Transformer.Factory().NewAssignmentExpression(
                weakMapName,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNewExpression(
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("WeakMap"),
                    nil,
                    tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default))));
    }
}

internal static void addPrivateIdentifierMethodToEnvironment(this ref classFieldsTransformer tx, ж<ast.Node> name, ж<classLexicalEnvironment> Ꮡlex, ж<privateEnvironment> Ꮡenv, bool isStatic, bool isValid) {
    ref var lex = ref Ꮡlex.DerefOrNull();
    ref var env = ref Ꮡenv.DerefOrNull();

    var methodName = tx.createHoistedVariableForPrivateName(name, "");
    ж<astꓸIdentifierNode> brandCheckIdentifier = default;
    if (isStatic) {
        brandCheckIdentifier = lex.classThis;
        if (brandCheckIdentifier == nil) {
            brandCheckIdentifier = lex.classConstructor;
        }
        debug.Assert(brandCheckIdentifier != nil, "classConstructor should be set in private identifier environment");
    } else {
        brandCheckIdentifier = env.data.weakSetName;
    }
    tx.setPrivateIdentifier(Ꮡenv, name, Ꮡ(new privateIdentifierInfo(
        kind: printer.PrivateIdentifierKindMethod,
        methodName: methodName,
        brandCheckIdentifier: brandCheckIdentifier,
        isStatic: isStatic,
        isValid: isValid
    )));
}

internal static void addPrivateIdentifierGetAccessorToEnvironment(this ref classFieldsTransformer tx, ж<ast.Node> name, ж<classLexicalEnvironment> Ꮡlex, ж<privateEnvironment> Ꮡenv, bool isStatic, bool isValid, ж<privateIdentifierInfo> ᏑpreviousInfo) {
    ref var lex = ref Ꮡlex.DerefOrNull();
    ref var env = ref Ꮡenv.DerefOrNull();
    ref var previousInfo = ref ᏑpreviousInfo.DerefOrNull();

    var getterName = tx.createHoistedVariableForPrivateName(name, "_get");
    ж<astꓸIdentifierNode> brandCheckIdentifier = default;
    if (isStatic) {
        brandCheckIdentifier = lex.classThis;
        if (brandCheckIdentifier == nil) {
            brandCheckIdentifier = lex.classConstructor;
        }
        debug.Assert(brandCheckIdentifier != nil, "classConstructor should be set in private identifier environment");
    } else {
        brandCheckIdentifier = env.data.weakSetName;
        debug.Assert(brandCheckIdentifier != nil, "weakSetName should be set in private identifier environment");
    }

    if (ᏑpreviousInfo != nil && previousInfo.kind == printer.PrivateIdentifierKindAccessor && previousInfo.isStatic == isStatic && previousInfo.getterName == nil) {
        previousInfo.getterName = getterName;
    } else {
        tx.setPrivateIdentifier(Ꮡenv, name, Ꮡ(new privateIdentifierInfo(
            kind: printer.PrivateIdentifierKindAccessor,
            getterName: getterName,
            brandCheckIdentifier: brandCheckIdentifier,
            isStatic: isStatic,
            isValid: isValid
        )));
    }
}

internal static void addPrivateIdentifierSetAccessorToEnvironment(this ref classFieldsTransformer tx, ж<ast.Node> name, ж<classLexicalEnvironment> Ꮡlex, ж<privateEnvironment> Ꮡenv, bool isStatic, bool isValid, ж<privateIdentifierInfo> ᏑpreviousInfo) {
    ref var lex = ref Ꮡlex.DerefOrNull();
    ref var env = ref Ꮡenv.DerefOrNull();
    ref var previousInfo = ref ᏑpreviousInfo.DerefOrNull();

    var setterName = tx.createHoistedVariableForPrivateName(name, "_set");
    ж<astꓸIdentifierNode> brandCheckIdentifier = default;
    if (isStatic) {
        brandCheckIdentifier = lex.classThis;
        if (brandCheckIdentifier == nil) {
            brandCheckIdentifier = lex.classConstructor;
        }
        debug.Assert(brandCheckIdentifier != nil, "classConstructor should be set in private identifier environment");
    } else {
        brandCheckIdentifier = env.data.weakSetName;
        debug.Assert(brandCheckIdentifier != nil, "weakSetName should be set in private identifier environment");
    }

    if (ᏑpreviousInfo != nil && previousInfo.kind == printer.PrivateIdentifierKindAccessor && previousInfo.isStatic == isStatic && previousInfo.setterName == nil) {
        previousInfo.setterName = setterName;
    } else {
        tx.setPrivateIdentifier(Ꮡenv, name, Ꮡ(new privateIdentifierInfo(
            kind: printer.PrivateIdentifierKindAccessor,
            setterName: setterName,
            brandCheckIdentifier: brandCheckIdentifier,
            isStatic: isStatic,
            isValid: isValid
        )));
    }
}

internal static void addPrivateIdentifierAutoAccessorToEnvironment(this ref classFieldsTransformer tx, ж<ast.Node> node, ж<ast.Node> name, ж<classLexicalEnvironment> Ꮡlex, ж<privateEnvironment> Ꮡenv, bool isStatic, bool isValid) {
    ref var lex = ref Ꮡlex.DerefOrNull();
    ref var env = ref Ꮡenv.DerefOrNull();

    var getterName = tx.createHoistedVariableForPrivateName(name, "_get");
    var setterName = tx.createHoistedVariableForPrivateName(name, "_set");
    ж<astꓸIdentifierNode> brandCheckIdentifier = default;
    if (isStatic) {
        brandCheckIdentifier = lex.classThis;
        if (brandCheckIdentifier == nil) {
            brandCheckIdentifier = lex.classConstructor;
        }
        debug.Assert(brandCheckIdentifier != nil, "classConstructor should be set in private identifier environment");
    } else {
        brandCheckIdentifier = env.data.weakSetName;
        debug.Assert(brandCheckIdentifier != nil, "weakSetName should be set in private identifier environment");
    }

    tx.setPrivateIdentifier(Ꮡenv, name, Ꮡ(new privateIdentifierInfo(
        kind: printer.PrivateIdentifierKindAccessor,
        getterName: getterName,
        setterName: setterName,
        brandCheckIdentifier: brandCheckIdentifier,
        isStatic: isStatic,
        isValid: isValid
    )));
}

internal static void addPrivateIdentifierToEnvironment(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var lex = tx.getClassLexicalEnvironment();
    var env = tx.getPrivateIdentifierEnvironment();
    var name = node.Name();
    var isStatic = ast.HasStaticModifier(Ꮡnode);
    var (previousInfo, _) = tx.getPrivateIdentifier(env, name);
    var isValid = !tx.isReservedPrivateName(name) && previousInfo == nil;

    if (ast.IsAutoAccessorPropertyDeclaration(Ꮡnode)) {
        tx.addPrivateIdentifierAutoAccessorToEnvironment(Ꮡnode, name, lex, env, isStatic, isValid);
    } else if (ast.IsPropertyDeclaration(Ꮡnode)) {
        tx.addPrivateIdentifierPropertyDeclarationToEnvironment(Ꮡnode, name);
    } else if (ast.IsMethodDeclaration(Ꮡnode)) {
        tx.addPrivateIdentifierMethodToEnvironment(name, lex, env, isStatic, isValid);
    } else if (ast.IsGetAccessorDeclaration(Ꮡnode)) {
        tx.addPrivateIdentifierGetAccessorToEnvironment(name, lex, env, isStatic, isValid, previousInfo);
    } else if (ast.IsSetAccessorDeclaration(Ꮡnode)) {
        tx.addPrivateIdentifierSetAccessorToEnvironment(name, lex, env, isStatic, isValid, previousInfo);
    }
}

internal static void setPrivateIdentifier(this ref classFieldsTransformer tx, ж<privateEnvironment> Ꮡenv, ж<ast.Node> Ꮡname, ж<privateIdentifierInfo> info) {
    ref var env = ref Ꮡenv.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    if (tx.Transformer.EmitContext().HasAutoGenerateInfo(Ꮡname)) {
        if (env.generatedIdentifiers == nil) {
            env.generatedIdentifiers = new map<ж<ast.Node>, ж<privateIdentifierInfo>>();
        }
        env.generatedIdentifiers[tx.Transformer.EmitContext().GetNodeForGeneratedName(Ꮡname)] = info;
    } else {
        env.members[name.Text()] = info;
    }
}

internal static (ж<privateIdentifierInfo>, bool) getPrivateIdentifier(this ref classFieldsTransformer tx, ж<privateEnvironment> Ꮡenv, ж<ast.Node> Ꮡname) {
    ref var env = ref Ꮡenv.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    if (tx.Transformer.EmitContext().HasAutoGenerateInfo(Ꮡname)) {
        var (infoΔ1, okΔ1) = env.generatedIdentifiers[tx.Transformer.EmitContext().GetNodeForGeneratedName(Ꮡname), ꟷ];
        return (infoΔ1, okΔ1);
    }
    var (info, ok) = env.members[name.Text(), ꟷ];
    return (info, ok);
}

internal static ж<astꓸIdentifierNode> createHoistedVariableForClass(this ref classFieldsTransformer tx, @string nameText, ж<ast.Node> node, @string suffix) {
    var env = tx.getPrivateIdentifierEnvironment();
    ж<astꓸIdentifierNode> identifier = default;
    if (env.data.className != nil) {
        @string prefix = "_"u8 + env.data.className.Text() + "_"u8;
        identifier = tx.Transformer.Factory().NewUniqueNameEx(prefix + nameText, new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsReservedInNestedScopes),
            Suffix: suffix
        ));
    } else {
        identifier = tx.Transformer.Factory().NewUniqueNameEx("_"u8 + nameText, new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsReservedInNestedScopes),
            Suffix: suffix
        ));
    }
    if (tx.requiresBlockScopedVar()) {
        tx.Transformer.EmitContext().AddLexicalDeclaration(identifier);
    } else {
        tx.Transformer.EmitContext().AddVariableDeclaration(identifier);
    }
    return identifier;
}

internal static ж<astꓸIdentifierNode> createHoistedVariableForClassFromNode(this ref classFieldsTransformer tx, ж<ast.Node> name, @string suffix) {
    var env = tx.getPrivateIdentifierEnvironment();
    @string prefix = default;
    if (env.data.className != nil) {
        prefix = "_"u8 + env.data.className.Text() + "_"u8;
    } else {
        prefix = "_";
    }
    var identifier = tx.Transformer.Factory().NewGeneratedNameForNodeEx(name, new printer.AutoGenerateOptions(
        Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsReservedInNestedScopes),
        Prefix: prefix,
        Suffix: suffix
    ));
    if (tx.requiresBlockScopedVar()) {
        tx.Transformer.EmitContext().AddLexicalDeclaration(identifier);
    } else {
        tx.Transformer.EmitContext().AddVariableDeclaration(identifier);
    }
    return identifier;
}

internal static ж<astꓸIdentifierNode> createHoistedVariableForPrivateName(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡname, @string suffix) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (tx.Transformer.EmitContext().HasAutoGenerateInfo(Ꮡname)) {
        return tx.createHoistedVariableForClassFromNode(Ꮡname, suffix);
    }
    @string text = name.Text();
    if (len(text) >= 1 && text[0] == '#') {
        text = text[1..];
    }
    return tx.createHoistedVariableForClass(text, Ꮡname, suffix);
}

internal static ж<privateIdentifierInfo> accessPrivateIdentifier(this ref classFieldsTransformer tx, ж<ast.Node> name) {
    for (var env = tx.lexicalEnvironment; env != nil; env = env.previous) {
        if (env.privateEnv != nil) {
            var (info, ok) = tx.getPrivateIdentifier(env.privateEnv, name); if (ok) {
                if (info.kind == printer.PrivateIdentifierKindUntransformed) {
                    return default;
                }
                return info;
            }
        }
    }
    return default;
}

internal static ж<ast.Node> wrapPrivateIdentifierForDestructuringTarget(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var prop = node.AsPropertyAccessExpression();
    var parameter = tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode);
    var info = tx.accessPrivateIdentifier(prop.Name());
    if (info == nil) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }
    var receiver = prop.Value.Expression;
    var isThisOrSuperProperty = (~prop).Expression.Kind == ast.KindThisKeyword || (~prop).Expression.Kind == ast.KindSuperKeyword;
    if (isThisOrSuperProperty || !transformers.IsSimpleCopiableExpression((~prop).Expression)) {
        receiver = tx.Transformer.Factory().NewTempVariableEx(new printer.AutoGenerateOptions(
            Flags: printer.GeneratedIdentifierFlagsReservedInNestedScopes
        ));
        tx.Transformer.EmitContext().AddVariableDeclaration(receiver);
        tx.pendingExpressions = append(
            tx.pendingExpressions,
            tx.Transformer.Factory().NewAssignmentExpression(receiver, tx.Transformer.Visitor().VisitNode((~prop).Expression)));
    }
    var assignExpr = tx.createPrivateIdentifierAssignment(info, receiver, parameter, ast.KindEqualsToken);
    return tx.Transformer.Factory().NewAssignmentTargetWrapper(parameter, assignExpr);
}

internal static ж<ast.Node> visitAssignmentElement(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode, tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode, false, ""); node = ref Ꮡnode.DerefOrNull();
    }
    if (ast.IsAssignmentExpression(Ꮡnode, true)) {
        var left = tx.visitDestructuringAssignmentTarget(node.AsBinaryExpression().Left);
        var right = tx.Transformer.Visitor().VisitNode(node.AsBinaryExpression().Right);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(
            node.AsBinaryExpression(),
            nil,
            left,
            nil,
            node.AsBinaryExpression().OperatorToken,
            right);
    }
    return tx.visitDestructuringAssignmentTarget(Ꮡnode);
}

internal static ж<ast.Node> visitAssignmentRestElement(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var spread = node.AsSpreadElement();
    if (ast.IsLeftHandSideExpression((~spread).Expression)) {
        var expr = tx.visitDestructuringAssignmentTarget((~spread).Expression);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSpreadElement(spread, expr);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitArrayAssignmentElement(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsArrayBindingOrAssignmentElement(Ꮡnode)) {
        if (ast.IsSpreadElement(Ꮡnode)) {
            return tx.visitAssignmentRestElement(Ꮡnode);
        }
        if (node.Kind != ast.KindOmittedExpression) {
            return tx.visitAssignmentElement(Ꮡnode);
        }
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitAssignmentProperty(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var prop = node.AsPropertyAssignment();
    var name = tx.Transformer.Visitor().VisitNode(prop.Name());
    var init = prop.Value.Initializer;
    if (ast.IsAssignmentExpression(init, true)) {
        var assignElem = tx.visitAssignmentElement(init);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAssignment(prop, nil, name, nil, nil, assignElem);
    }
    if (ast.IsLeftHandSideExpression(init)) {
        var target = tx.visitDestructuringAssignmentTarget(init);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAssignment(prop, nil, name, nil, nil, target);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitShorthandAssignmentProperty(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode, tx.isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode, false, ""); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitAssignmentRestProperty(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var spread = node.AsSpreadAssignment();
    if (ast.IsLeftHandSideExpression((~spread).Expression)) {
        var expr = tx.visitDestructuringAssignmentTarget((~spread).Expression);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSpreadAssignment(spread, expr);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitObjectAssignmentElement(this ref classFieldsTransformer tx, ж<ast.Node> node) {
    debug.Assert(node != nil && ast.IsObjectBindingOrAssignmentElement(node));
    if (ast.IsSpreadAssignment(node)) {
        return tx.visitAssignmentRestProperty(node);
    }
    if (ast.IsShorthandPropertyAssignment(node)) {
        return tx.visitShorthandAssignmentProperty(node);
    }
    if (ast.IsPropertyAssignment(node)) {
        return tx.visitAssignmentProperty(node);
    }
    return tx.Transformer.Visitor().VisitEachChild(node);
}

internal static ж<ast.Node> visitAssignmentPattern(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsArrayLiteralExpression(Ꮡnode)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateArrayLiteralExpression(
            node.AsArrayLiteralExpression(),
            tx.arrayAssignmentElementVisitor.VisitNodes((~node.AsArrayLiteralExpression()).Elements),
            node.AsArrayLiteralExpression().MultiLine);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateObjectLiteralExpression(
        node.AsObjectLiteralExpression(),
        tx.objectAssignmentElementVisitor.VisitNodes((~node.AsObjectLiteralExpression()).Properties),
        node.AsObjectLiteralExpression().MultiLine);
}

} partial class estransforms_package {

internal static ж<astꓸExpression> createPrivateStaticFieldInitializer(ж<printer.NodeFactory> Ꮡfactory, ж<astꓸIdentifierNode> variableName, ж<astꓸExpression> Ꮡinitializer) {
    ref var factory = ref Ꮡfactory.DerefOrNull();
    ref var initializer = ref Ꮡinitializer.DerefOrNull();

    if (Ꮡinitializer == nil) {
        Ꮡinitializer = factory.NewVoidZeroExpression(); initializer = ref Ꮡinitializer.DerefOrNull();
    }
    return factory.NewAssignmentExpression(
        variableName,
        Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(
            Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), nil, nil, Ꮡinitializer)
            ])),
            false));
}

internal static ж<astꓸExpression> createPrivateInstanceFieldInitializer(ж<printer.NodeFactory> Ꮡfactory, ж<astꓸExpression> receiver, ж<astꓸExpression> Ꮡinitializer, ж<astꓸIdentifierNode> weakMapName) {
    ref var factory = ref Ꮡfactory.DerefOrNull();
    ref var initializer = ref Ꮡinitializer.DerefOrNull();

    if (Ꮡinitializer == nil) {
        Ꮡinitializer = factory.NewVoidZeroExpression(); initializer = ref Ꮡinitializer.DerefOrNull();
    }
    return factory.NewMethodCall(weakMapName, Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("set"), [receiver, Ꮡinitializer]);
}

internal static ж<astꓸExpression> createPrivateInstanceMethodInitializer(ж<printer.NodeFactory> Ꮡfactory, ж<astꓸExpression> receiver, ж<astꓸIdentifierNode> weakSetName) {
    ref var factory = ref Ꮡfactory.DerefOrNull();

    return factory.NewMethodCall(weakSetName, Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("add"), [receiver]);
}

} partial class estransformsᴛclassFieldsTransformer {

internal static bool isReservedPrivateName(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return !(ast.IsPrivateIdentifier(Ꮡnode) && tx.Transformer.EmitContext().HasAutoGenerateInfo(Ꮡnode)) && node.Text() == "#constructor"u8;
}

} partial class estransforms_package {

internal static bool isStaticPropertyDeclarationOrClassStaticBlock(ж<ast.Node> node) {
    return ast.IsClassStaticBlockDeclaration(node) || (ast.IsPropertyDeclaration(node) && ast.HasStaticModifier(node));
}

} partial class estransformsᴛclassFieldsTransformer {

internal static slice<ж<ast.Node>> getProperties(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode, bool requireInitializer, bool isStatic) {
    ref var node = ref Ꮡnode.DerefOrNull();
    var resultᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    slice<ж<ast.Node>> result = default;
    foreach (var (_, member) in range(node.Members())) {
        if (ast.IsPropertyDeclaration(member) && (!requireInitializer || member.Initializer() != nil) && ast.HasStaticModifier(member) == isStatic) {
            result = append(resultᴷ, result, member);
        }
    }
    result = move2heap(resultᴷ, result);
    return result;
}

internal static slice<ж<ast.Node>> getStaticPropertiesAndClassStaticBlock(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();
    var resultᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    slice<ж<ast.Node>> result = default;
    foreach (var (_, member) in range(node.Members())) {
        if (ast.IsClassStaticBlockDeclaration(member) || (ast.IsPropertyDeclaration(member) && ast.HasStaticModifier(member))) {
            result = append(resultᴷ, result, member);
        }
    }
    result = move2heap(resultᴷ, result);
    return result;
}

} partial class estransforms_package {

internal static bool classHasClassThisAssignment(ж<printer.EmitContext> emitContext, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    foreach (var (_, member) in range(node.Members())) {
        if (isClassThisAssignmentBlock(emitContext, member)) {
            return true;
        }
    }
    return false;
}

internal static bool isNonStaticMethodOrAccessorWithPrivateName(ж<ast.Node> Ꮡmember) {
    ref var member = ref Ꮡmember.DerefOrNull();

    return !ast.IsStatic(Ꮡmember) && (ast.IsMethodOrAccessor(Ꮡmember) || ast.IsAutoAccessorPropertyDeclaration(Ꮡmember)) && ast.IsPrivateIdentifier(member.Name());
}

internal static ж<astꓸExpression> createMemberAccessForPropertyName(ж<printer.NodeFactory> factory, ж<printer.EmitContext> ᏑemitContext, ж<astꓸExpression> receiver, ж<astꓸPropertyName> Ꮡname, ref astꓸPropertyName location) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    if (ast.IsComputedPropertyName(Ꮡname)) {
        var expressionΔ1 = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(receiver, nil, name.Expression(), ast.NodeFlagsNone);
        expressionΔ1.Loc = location.Loc;
        return expressionΔ1;
    }
    ж<astꓸExpression> expression = default;
    if (ast.IsIdentifier(Ꮡname) || ast.IsPrivateIdentifier(Ꮡname)) {
        expression = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(receiver, nil, Ꮡname, ast.NodeFlagsNone);
    } else {
        expression = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(receiver, nil, Ꮡname, ast.NodeFlagsNone);
    }
    emitContext.SetCommentRange(expression, name.Loc);
    emitContext.SetSourceMapRange(expression, name.Loc);
    emitContext.AddEmitFlags(expression, printer.EFNoNestedSourceMaps);
    return expression;
}

} partial class estransformsᴛclassFieldsTransformer {

internal static (ж<astꓸExpression> thisArg, ж<astꓸExpression> target) createCallBinding(this ref classFieldsTransformer tx, ж<ast.Node> Ꮡnode) {
    ж<astꓸExpression> thisArg = default;
    ж<astꓸExpression> target = default;
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsSuperProperty(Ꮡnode)) {
        return (tx.Transformer.Factory().NewThisExpression(), Ꮡnode);
    }
    if (ast.IsPropertyAccessExpression(Ꮡnode)) {
        var expr = node.AsPropertyAccessExpression();
        if (shouldBeCapturedInTempVariable((~expr).Expression)) {
            thisArg = tx.Transformer.Factory().NewTempVariable();
            tx.Transformer.EmitContext().AddVariableDeclaration(thisArg);
            target = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression(
                    tx.Transformer.Factory().NewAssignmentExpression(thisArg, (~expr).Expression)),
                nil,
                expr.Name(),
                ast.NodeFlagsNone);
            return (thisArg, target);
        }
        return ((~expr).Expression, Ꮡnode);
    }
    thisArg = tx.Transformer.Factory().NewVoidZeroExpression();
    target = Ꮡnode;
    return (thisArg, target);
}

} partial class estransforms_package {

internal static bool shouldBeCapturedInTempVariable(ж<ast.Node> node) {
    var target = ast.SkipParentheses(node);
    switch (target.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindThisKeyword || ᴛ == ast.KindNumericLiteral || ᴛ == ast.KindBigIntLiteral || ᴛ == ast.KindStringLiteral: {
        return false;
    }
    default: {
        return true;
    }}

}

} partial class estransformsᴛclassFieldsTransformer {

internal static ж<ast.Node> createAccessorPropertyGetRedirector(this ref classFieldsTransformer tx, ж<ast.PropertyDeclaration> Ꮡnode, ж<ast.ModifierList> modifiers, ж<astꓸPropertyName> name, ж<astꓸExpression> receiver) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var backingFieldName = tx.Transformer.Factory().NewGeneratedPrivateNameForNodeEx(node.Name(), new printer.AutoGenerateOptions(Suffix: "_accessor_storage"));
    var returnExpr = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
        receiver,
        nil,
        backingFieldName,
        ast.NodeFlagsNone);
    var returnStmt = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(returnExpr);
    var body = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([returnStmt]), false);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewGetAccessorDeclaration(
        modifiers,
        name,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
        nil,
        nil,
        body);
}

internal static ж<ast.Node> createAccessorPropertySetRedirector(this ref classFieldsTransformer tx, ж<ast.PropertyDeclaration> Ꮡnode, ж<ast.ModifierList> modifiers, ж<astꓸPropertyName> name, ж<astꓸExpression> receiver) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var backingFieldName = tx.Transformer.Factory().NewGeneratedPrivateNameForNodeEx(node.Name(), new printer.AutoGenerateOptions(Suffix: "_accessor_storage"));
    var valueParam = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"),
        nil,
        nil,
        nil);
    var assignExpr = tx.Transformer.Factory().NewAssignmentExpression(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
            receiver,
            nil,
            backingFieldName,
            ast.NodeFlagsNone),
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"));
    var exprStmt = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(assignExpr);
    var body = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([exprStmt]), false);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSetAccessorDeclaration(
        modifiers,
        name,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([valueParam]),
        nil,
        nil,
        body);
}

} partial class estransforms_package {

internal static iter.Seq<ж<astꓸExpression>> flattenCommaList(ж<astꓸExpression> node) {
    return (Func<ж<astꓸExpression>, bool> yield) => {
        flattenCommaListWorker(node, yield);
    };
}

internal static bool flattenCommaListWorker(ж<astꓸExpression> Ꮡnode, Func<ж<astꓸExpression>, bool> yield) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsParenthesizedExpression(Ꮡnode) && ast.NodeIsSynthesized(Ꮡnode)) {
        return flattenCommaListWorker(node.Expression(), yield);
    } else if (ast.IsCommaExpression(Ꮡnode.AsNode())) {
        return flattenCommaListWorker(node.AsBinaryExpression().Left, yield) && flattenCommaListWorker(node.AsBinaryExpression().Right, yield);
    } else {
        return yield(Ꮡnode);
    }
}

internal static ж<ast.BinaryExpression> findComputedPropertyNameCacheAssignment(ref printer.EmitContext emitContext, ж<ast.Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    var node = name.Expression();
    for (;;) {
        node = ast.SkipOuterExpressions(node, 0);
        if (ast.IsBinaryExpression(node) && node.AsBinaryExpression().OperatorToken.Kind == ast.KindCommaToken) {
            node = node.AsBinaryExpression().Right;
            continue;
        }
        if (ast.IsAssignmentExpression(node, true) && ast.IsIdentifier(node.AsBinaryExpression().Left)) {
            return node.AsBinaryExpression();
        }
        break;
    }
    return default;
}

internal static ж<astꓸExpression> expandPreOrPostfixIncrementOrDecrementExpression(ж<printer.NodeFactory> Ꮡfactory, ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> Ꮡnode, ж<astꓸExpression> Ꮡexpression, ж<astꓸIdentifierNode> resultVariable) {
    ref var factory = ref Ꮡfactory.DerefOrNull();
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var expression = ref Ꮡexpression.DerefOrNull();

    ast.Kind @operator = default;
    ж<ast.Node> operand = default;
    if (ast.IsPrefixUnaryExpression(Ꮡnode)) {
        @operator = node.AsPrefixUnaryExpression().Operator;
        operand = node.AsPrefixUnaryExpression().Operand;
    } else {
        @operator = node.AsPostfixUnaryExpression().Operator;
        operand = node.AsPostfixUnaryExpression().Operand;
    }

    var temp = factory.NewTempVariable();
    emitContext.AddVariableDeclaration(temp);
    Ꮡexpression = factory.NewAssignmentExpression(temp, Ꮡexpression); expression = ref Ꮡexpression.DerefOrNull();
    expression.Loc = operand.Loc;

    ж<astꓸExpression> operation = default;
    if (ast.IsPrefixUnaryExpression(Ꮡnode)) {
        operation = Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(@operator, temp);
    } else {
        operation = Ꮡfactory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPostfixUnaryExpression(temp, @operator);
    }
    operation.Loc = node.Loc;

    if (resultVariable != nil) {
        operation = factory.NewAssignmentExpression(resultVariable, operation);
        operation.Loc = node.Loc;
    }

    Ꮡexpression = factory.NewCommaExpression(Ꮡexpression, operation); expression = ref Ꮡexpression.DerefOrNull();
    expression.Loc = node.Loc;

    if (ast.IsPostfixUnaryExpression(Ꮡnode)) {
        Ꮡexpression = factory.NewCommaExpression(Ꮡexpression, temp); expression = ref Ꮡexpression.DerefOrNull();
        expression.Loc = node.Loc;
    }

    return Ꮡexpression;
}

} // end estransforms_package
