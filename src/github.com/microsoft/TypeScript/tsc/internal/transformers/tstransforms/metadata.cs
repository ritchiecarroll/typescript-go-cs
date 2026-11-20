namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tstransforms_package;

partial class tstransforms_package {

public const bool USE_NEW_TYPE_METADATA_FORMAT = false;

partial struct MetadataTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal bool legacyDecorators;
    internal printer.EmitResolver resolver;
    internal ж<metadataSerializer> serializer;
    internal core.ScriptTarget languageVersion;
    internal bool strictNullChecks;
    internal ж<ast.Node> parent;
    internal ж<ast.Node> currentLexicalScope;
}

public static ж<transformers.Transformer> NewMetadataTransformer(ж<transformers.TransformOptions> Ꮡopt) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    var tx = Ꮡ(new MetadataTransformer(
        legacyDecorators: opt.CompilerOptions.ExperimentalDecorators.IsTrue(),
        resolver: opt.EmitResolver,
        languageVersion: opt.CompilerOptions.GetEmitScriptTarget(),
        strictNullChecks: opt.CompilerOptions.GetStrictOptionValue(opt.CompilerOptions.StrictNullChecks)
    ));
    return tx.of(MetadataTransformer.ᏑTransformer).NewTransformer(tx.visit, opt.Context);
}

} partial class tstransformsᴛMetadataTransformer {

internal static ж<ast.Node> visit(this ж<MetadataTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        if (((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsDecorators)) == 0) {
            return Ꮡnode;
        }

        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            return Ꮡtx.visitClassDeclaration(node.AsClassDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindClassExpression: {
            return Ꮡtx.visitClassExpression(node.AsClassExpression());
        }
        case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
            return tx.visitPropertyDeclaration(node.AsPropertyDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
            return tx.visitMethodDeclaration(node.AsMethodDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindSetAccessor: {
            return tx.visitSetAccessor(node.AsSetAccessorDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindGetAccessor: {
            return tx.visitGetAccessor(node.AsGetAccessorDeclaration());
        }
        case var ᴛ when ᴛ == ast.KindSourceFile: {
            tx.parent = default;
            defer(Ꮡtx.setParent, (ж<ast.Node>)(nil), ref ᒐ);
            tx.currentLexicalScope = Ꮡnode;
            defer(Ꮡtx.setCurrentLexicalScope, (ж<ast.Node>)(nil), ref ᒐ);
            tx.serializer = newMetadataSerializer(tx.resolver, Ꮡtx.of(MetadataTransformer.ᏑTransformer).Factory(), Ꮡtx.of(MetadataTransformer.ᏑTransformer).EmitContext(), tx.languageVersion, tx.strictNullChecks);
            var updated = Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).EmitContext().AddEmitHelper(updated, Ꮡtx.of(MetadataTransformer.ᏑTransformer).EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
            return updated;
        }
        case var ᴛ when ᴛ == ast.KindModuleBlock || ᴛ == ast.KindBlock || ᴛ == ast.KindCaseBlock: {
            var oldScope = tx.currentLexicalScope;
            tx.currentLexicalScope = Ꮡnode;
            defer(Ꮡtx.setCurrentLexicalScope, oldScope, ref ᒐ);
            return Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }
        default: {
            return Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        }}

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static void setParent(this ref MetadataTransformer tx, ж<ast.Node> node) {
    tx.parent = node;
}

internal static void setCurrentLexicalScope(this ref MetadataTransformer tx, ж<ast.Node> node) {
    tx.currentLexicalScope = node;
}

internal static ж<ast.Node> visitClassExpression(this ж<MetadataTransformer> Ꮡtx, ж<ast.ClassExpression> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var oldParent = tx.parent;
        tx.parent = Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        defer(Ꮡtx.setParent, oldParent, ref ᒐ);

        if (!ast.ClassOrConstructorParameterIsDecorated(tx.legacyDecorators, Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            return Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }
        var modifiers = tx.injectClassTypeMetadata(Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitModifiers(Ꮡnode.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers()), Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return Ꮡtx.of(MetadataTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(
            Ꮡnode,
            modifiers,
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNode(node.Name()),
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNodes(node.TypeParameters),
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNodes(node.HeritageClauses),
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNodes(node.Members));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitClassDeclaration(this ж<MetadataTransformer> Ꮡtx, ж<ast.ClassDeclaration> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var oldParent = tx.parent;
        tx.parent = Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        defer(Ꮡtx.setParent, oldParent, ref ᒐ);

        if (!ast.ClassOrConstructorParameterIsDecorated(tx.legacyDecorators, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            return Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        }
        var modifiers = tx.injectClassTypeMetadata(Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitModifiers(Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers()), Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return Ꮡtx.of(MetadataTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
            Ꮡnode,
            modifiers,
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNode(node.Name()),
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNodes(node.TypeParameters),
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNodes(node.HeritageClauses),
            Ꮡtx.of(MetadataTransformer.ᏑTransformer).Visitor().VisitNodes(node.Members));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> visitPropertyDeclaration(this ref MetadataTransformer tx, ж<ast.PropertyDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.HasDecorators(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var modifiers = tx.injectClassElementTypeMetadata(tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers()), Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.parent);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
        Ꮡnode,
        modifiers,
        tx.Transformer.Visitor().VisitNode(node.Name()),
        tx.Transformer.Visitor().VisitNode(node.PostfixToken),
        tx.Transformer.Visitor().VisitNode(node.Type),
        tx.Transformer.Visitor().VisitNode(node.Initializer));
}

internal static ж<ast.Node> visitMethodDeclaration(this ref MetadataTransformer tx, ж<ast.MethodDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.HasDecorators(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && len(getDecoratorsOfParameters(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) == 0) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var modifiers = tx.injectClassElementTypeMetadata(tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers()), Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.parent);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(
        Ꮡnode,
        modifiers,
        tx.Transformer.Visitor().VisitNode(node.AsteriskToken),
        tx.Transformer.Visitor().VisitNode(node.Name()),
        tx.Transformer.Visitor().VisitNode(node.PostfixToken),
        tx.Transformer.Visitor().VisitNodes(node.TypeParameters),
        tx.Transformer.Visitor().VisitNodes(node.Parameters),
        tx.Transformer.Visitor().VisitNode(node.Type),
        tx.Transformer.Visitor().VisitNode(node.FullSignature),
        tx.Transformer.Visitor().VisitNode(node.Body));
}

internal static ж<ast.Node> visitSetAccessor(this ref MetadataTransformer tx, ж<ast.SetAccessorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.HasDecorators(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && len(getDecoratorsOfParameters(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) == 0) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var modifiers = tx.injectClassElementTypeMetadata(tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()), Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.parent);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(
        Ꮡnode,
        modifiers,
        tx.Transformer.Visitor().VisitNode(node.Name()),
        tx.Transformer.Visitor().VisitNodes(node.TypeParameters),
        tx.Transformer.Visitor().VisitNodes(node.Parameters),
        tx.Transformer.Visitor().VisitNode(node.Type),
        tx.Transformer.Visitor().VisitNode(node.FullSignature),
        tx.Transformer.Visitor().VisitNode(node.Body));
}

internal static ж<ast.Node> visitGetAccessor(this ref MetadataTransformer tx, ж<ast.GetAccessorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.HasDecorators(Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    var modifiers = tx.injectClassElementTypeMetadata(tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()), Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tx.parent);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(
        Ꮡnode,
        modifiers,
        tx.Transformer.Visitor().VisitNode(node.Name()),
        tx.Transformer.Visitor().VisitNodes(node.TypeParameters),
        tx.Transformer.Visitor().VisitNodes(node.Parameters),
        tx.Transformer.Visitor().VisitNode(node.Type),
        tx.Transformer.Visitor().VisitNode(node.FullSignature),
        tx.Transformer.Visitor().VisitNode(node.Body));
}

internal static ж<ast.ModifierList> injectClassTypeMetadata(this ref MetadataTransformer tx, ж<ast.ModifierList> Ꮡlist, ж<ast.Node> node) {
    ref var list = ref Ꮡlist.DerefOrNull();

    var metadata = tx.getTypeMetadata(node, node);
    if (len(metadata) > 0) {
        slice<ж<ast.Node>> originalNodes = default;
        if (Ꮡlist != nil) {
            originalNodes = list.Nodes;
        }
        if (len(originalNodes) == 0) {
            var resΔ1 = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(metadata);
            if (Ꮡlist != nil) {
                resΔ1.Loc = list.Loc;
            }
            return resΔ1;
        }
        slice<ж<ast.Node>> modifiersArray = default;
        if (ast.IsModifier(originalNodes[0]) && (originalNodes[0].Kind == ast.KindDefaultKeyword || originalNodes[0].Kind == ast.KindExportKeyword)) {
            modifiersArray = append(modifiersArray, originalNodes[0]);
            if (len(originalNodes) > 1 && (originalNodes[1].Kind == ast.KindDefaultKeyword || originalNodes[1].Kind == ast.KindExportKeyword)) {
                modifiersArray = append(modifiersArray, originalNodes[1]);
            }
        }
        nint restStart = len(modifiersArray);
        var decos = core.Filter<ж<ast.Node>>(originalNodes, ast.IsDecorator);
        modifiersArray = appendꓸꓸꓸ(modifiersArray, decos);
        modifiersArray = appendꓸꓸꓸ(modifiersArray, metadata);
        var otherModifiers = core.Filter<ж<ast.Node>>(originalNodes.slice(restStart), ast.IsModifier);
        modifiersArray = appendꓸꓸꓸ(modifiersArray, otherModifiers);
        var res = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(modifiersArray);
        res.Loc = list.Loc;
        return res;
    }
    return Ꮡlist;
}

internal static ж<ast.ModifierList> injectClassElementTypeMetadata(this ref MetadataTransformer tx, ж<ast.ModifierList> Ꮡlist, ж<ast.Node> node, ж<ast.Node> container) {
    ref var list = ref Ꮡlist.DerefOrNull();

    if (!ast.IsClassLike(container)) {
        return Ꮡlist;
    }
    if (!ast.ClassElementOrClassElementParameterIsDecorated(tx.legacyDecorators, node, container)) {
        return Ꮡlist;
    }
    var metadata = tx.getTypeMetadata(node, container);
    if (len(metadata) > 0) {
        slice<ж<ast.Node>> originalNodes = default;
        if (Ꮡlist != nil) {
            originalNodes = list.Nodes;
        }
        if (len(originalNodes) == 0) {
            var resΔ1 = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(metadata);
            if (Ꮡlist != nil) {
                resΔ1.Loc = list.Loc;
            }
            return resΔ1;
        }
        slice<ж<ast.Node>> modifiersArray = default;
        var decos = core.Filter<ж<ast.Node>>(originalNodes, ast.IsDecorator);
        modifiersArray = appendꓸꓸꓸ(modifiersArray, decos);
        modifiersArray = appendꓸꓸꓸ(modifiersArray, metadata);
        var modifiers = core.Filter<ж<ast.Node>>(originalNodes, ast.IsModifier);
        modifiersArray = appendꓸꓸꓸ(modifiersArray, modifiers);
        var res = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(modifiersArray);
        res.Loc = list.Loc;
        return res;
    }
    return Ꮡlist;
}

internal static slice<ж<ast.Node>> getTypeMetadata(this ref MetadataTransformer tx, ж<ast.Node> node, ж<ast.Node> container) {
    if (!tx.legacyDecorators) {
        return default;
    }
    if (USE_NEW_TYPE_METADATA_FORMAT) {
        return tx.getNewTypeMetadata(node, container);
    }
    return tx.getOldTypeMetadata(node, container);
}

internal static slice<ж<ast.Node>> getOldTypeMetadata(this ref MetadataTransformer tx, ж<ast.Node> node, ж<ast.Node> container) {
    slice<ж<ast.Node>> decorators = default;
    if (tx.shouldAddTypeMetadata(node)) {
        var typeMetadata = tx.Transformer.Factory().NewMetadataHelper("design:type", tx.serializer.SerializeTypeOfNode(new metadataSerializerContext(currentLexicalScope: tx.currentLexicalScope, currentNameScope: container), node, container));
        decorators = append(decorators, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewDecorator(typeMetadata));
    }
    if (tx.shouldAddParamTypesMetadata(node)) {
        var paramTypesMetadata = tx.Transformer.Factory().NewMetadataHelper("design:paramtypes", tx.serializer.SerializeParameterTypesOfNode(new metadataSerializerContext(currentLexicalScope: tx.currentLexicalScope, currentNameScope: container), node, container));
        decorators = append(decorators, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewDecorator(paramTypesMetadata));
    }
    if (tx.shouldAddReturnTypeMetadata(node)) {
        var returnTypeMetadata = tx.Transformer.Factory().NewMetadataHelper("design:returntype", tx.serializer.SerializeReturnTypeOfNode(new metadataSerializerContext(currentLexicalScope: tx.currentLexicalScope, currentNameScope: container), node));
        decorators = append(decorators, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewDecorator(returnTypeMetadata));
    }
    return decorators;
}

internal static slice<ж<ast.Node>> getNewTypeMetadata(this ref MetadataTransformer tx, ж<ast.Node> node, ж<ast.Node> container) {
    slice<ж<ast.Node>> properties = default;
    if (tx.shouldAddTypeMetadata(node)) {
        properties = append(properties, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("type"),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrowFunction(
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsGreaterThanToken),
                tx.serializer.SerializeTypeOfNode(new metadataSerializerContext(currentLexicalScope: tx.currentLexicalScope, currentNameScope: container), node, container))));
    }
    if (tx.shouldAddParamTypesMetadata(node)) {
        properties = append(properties, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("paramTypes"),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrowFunction(
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsGreaterThanToken),
                tx.serializer.SerializeParameterTypesOfNode(new metadataSerializerContext(currentLexicalScope: tx.currentLexicalScope, currentNameScope: container), node, container))));
    }
    if (tx.shouldAddReturnTypeMetadata(node)) {
        properties = append(properties, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("returnType"),
            nil,
            nil,
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrowFunction(
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
                nil,
                nil,
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindEqualsGreaterThanToken),
                tx.serializer.SerializeReturnTypeOfNode(new metadataSerializerContext(currentLexicalScope: tx.currentLexicalScope, currentNameScope: container), node))));
    }
    if (len(properties) > 0) {
        var typeInfoMetadata = tx.Transformer.Factory().NewMetadataHelper("design:typeinfo", tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(properties), true));
        return slice<ж<ast.Node>>([tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewDecorator(typeInfoMetadata)]);
    }
    return default;
}

internal static bool shouldAddTypeMetadata(this ref MetadataTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindPropertyDeclaration: {
        return true;
    }}

    return false;
}

internal static bool shouldAddReturnTypeMetadata(this ref MetadataTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == ast.KindMethodDeclaration;
}

internal static bool shouldAddParamTypesMetadata(this ref MetadataTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression: {
        return ast.GetFirstConstructorWithBody(Ꮡnode) != nil;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
        return true;
    }}

    return false;
}

} // end tstransformsᴛMetadataTransformer
