namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class declarations_package {

internal static bool needsScopeMarker(ж<ast.Node> result) {
    return !ast.IsAnyImportOrReExport(result) && !ast.IsExportAssignment(result) && !ast.HasSyntacticModifier(result, ast.ModifierFlagsExport) && !ast.IsAmbientModule(result);
}

internal static bool canHaveLiteralInitializer(DeclarationEmitHost host, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertySignature: {
        return host.GetEffectiveDeclarationFlags(Ꮡnode, ast.ModifierFlagsPrivate) == 0;
    }
    case var ᴛ when ᴛ == ast.KindParameter || ᴛ == ast.KindVariableDeclaration: {
        return true;
    }}

    return false;
}

internal static bool canProduceDiagnostics(ж<ast.Node> node) {
    return ast.IsVariableDeclaration(node) || ast.IsPropertyDeclaration(node) || ast.IsPropertySignatureDeclaration(node) || ast.IsBindingElement(node) || ast.IsSetAccessorDeclaration(node) || ast.IsGetAccessorDeclaration(node) || ast.IsConstructSignatureDeclaration(node) || ast.IsCallSignatureDeclaration(node) || ast.IsMethodDeclaration(node) || ast.IsMethodSignatureDeclaration(node) || ast.IsFunctionDeclaration(node) || ast.IsParameterDeclaration(node) || ast.IsTypeParameterDeclaration(node) || ast.IsExpressionWithTypeArguments(node) || ast.IsImportEqualsDeclaration(node) || ast.IsTypeAliasDeclaration(node) || ast.IsJSTypeAliasDeclaration(node) || ast.IsConstructorDeclaration(node) || ast.IsIndexSignatureDeclaration(node) || ast.IsPropertyAccessExpression(node) || ast.IsElementAccessExpression(node) || ast.IsBinaryExpression(node) || ast.IsCallExpression(node);
}

internal static bool canReuseModifierNodes(slice<ж<ast.Node>> nodes) {
    foreach (var (_, node) in range(nodes)) {
        if (ast.IsModifier(node) && (ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
            return false;
        }
    }
    return true;
}

internal static bool isDeclarationAndNotVisible(ж<printer.EmitContext> ᏑemitContext, printer.EmitResolver resolver, ж<ast.Node> Ꮡnode) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = emitContext.ParseNode(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindEnumDeclaration: {
        return !resolver.IsDeclarationVisible(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        return !getBindingNameVisible(resolver, Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindJSImportDeclaration || ᴛ == ast.KindExportDeclaration || ᴛ == ast.KindExportAssignment: {
        return false;
    }
    case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration: {
        return true;
    }}

    return false;
}

internal static bool getBindingNameVisible(printer.EmitResolver resolver, ж<ast.Node> Ꮡelem) {
    ref var elem = ref Ꮡelem.DerefOrNull();

    if (ast.IsOmittedExpression(Ꮡelem)) {
        return false;
    }
    if (elem.Name() == nil) {
        return false;
    }
    if (ast.IsBindingPattern(elem.Name())) {
        foreach (var (_, elemΔ1) in range(elem.Name().Elements())) {
            if (getBindingNameVisible(resolver, elemΔ1)) {
                return true;
            }
        }
        return false;
    } else {
        return resolver.IsDeclarationVisible(Ꮡelem);
    }
}

internal static bool isEnclosingDeclaration(ж<ast.Node> node) {
    return ast.IsSourceFile(node) || ast.IsTypeAliasDeclaration(node) || ast.IsJSTypeAliasDeclaration(node) || ast.IsModuleDeclaration(node) || ast.IsClassDeclaration(node) || ast.IsInterfaceDeclaration(node) || ast.IsFunctionLike(node) || ast.IsIndexSignatureDeclaration(node) || ast.IsMappedTypeNode(node) || ast.IsVariableDeclaration(node);
}

internal static bool isAlwaysType(ref ast.Node node) {
    if (node.Kind == ast.KindInterfaceDeclaration) {
        return true;
    }
    return false;
}

internal static ast.ModifierFlags maskModifierFlags(ж<ast.Node> node, ast.ModifierFlags modifierMask, ast.ModifierFlags modifierAdditions) {
    var flags = (ast.ModifierFlags)(((ast.ModifierFlags)(ast.GetCombinedModifierFlags(node) & modifierMask)) | modifierAdditions);
    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsDefault) != 0 && ((ast.ModifierFlags)(flags & ast.ModifierFlagsExport) == 0)) {
        flags ^= (ast.ModifierFlags)(ast.ModifierFlagsExport);
    }
    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsDefault) != 0 && (ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0) {
        flags ^= (ast.ModifierFlags)(ast.ModifierFlagsAmbient);
    }
    return flags;
}

internal static ж<ast.Node> unwrapParenthesizedExpression(ж<ast.Node> Ꮡo) {
    ref var o = ref Ꮡo.DerefOrNull();

    while (o.Kind == ast.KindParenthesizedExpression) {
        Ꮡo = o.Expression(); o = ref Ꮡo.DerefOrNull();
    }
    return Ꮡo;
}

internal static bool isPrivateMethodTypeParameter(DeclarationEmitHost host, ж<ast.TypeParameterDeclaration> node) {
    return node.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode().Parent.Kind == ast.KindMethodDeclaration && host.GetEffectiveDeclarationFlags(node.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode().Parent, ast.ModifierFlagsPrivate) != 0;
}

internal static bool shouldEmitFunctionProperties(ref ast.FunctionDeclaration input) {
    if (input.Body != nil) {
        return true;
    }
    return !core.Every(input.Symbol.Declarations, (ж<ast.Node> decl) => !ast.IsFunctionDeclaration(decl) || (~decl.AsFunctionDeclaration()).Body == nil);
}

internal static ж<ast.Node> getEffectiveBaseTypeNode(ж<ast.Node> node) {
    var baseType = ast.GetClassExtendsHeritageElement(node);
    return baseType;
}

internal static bool isScopeMarker(ж<ast.Node> node) {
    return ast.IsExportAssignment(node) || ast.IsExportDeclaration(node);
}

internal static bool hasScopeMarker(ж<astꓸStatementList> Ꮡstatements) {
    ref var statements = ref Ꮡstatements.DerefOrNull();

    if (Ꮡstatements == nil) {
        return false;
    }
    return core.Some<ж<ast.Node>>(statements.Nodes, isScopeMarker);
}

} // end declarations_package
