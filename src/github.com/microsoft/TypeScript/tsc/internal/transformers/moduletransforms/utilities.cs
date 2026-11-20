namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using outputpaths = go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class moduletransforms_package {

internal static bool isDeclarationNameOfEnumOrNamespace(ж<printer.EmitContext> ᏑemitContext, ж<astꓸIdentifierNode> node) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    var original = emitContext.MostOriginal(node); if (original != nil && original.Parent != nil) {
        switch (original.Parent.Kind) {
        case var ᴛ when ᴛ == ast.KindEnumDeclaration || ᴛ == ast.KindModuleDeclaration: {
            return original == original.Parent.Name();
        }}

    }
    return false;
}

internal static ж<astꓸExpression> rewriteModuleSpecifier(ж<printer.EmitContext> ᏑemitContext, ж<astꓸExpression> Ꮡnode, ж<core.CompilerOptions> ᏑcompilerOptions) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    if (Ꮡnode == nil || !ast.IsStringLiteral(Ꮡnode) || !core.ShouldRewriteModuleSpecifier(node.Text(), ᏑcompilerOptions)) {
        return Ꮡnode;
    }
    @string updatedText = tspath.ChangeExtension(node.Text(), outputpaths.GetOutputExtension(node.Text(), compilerOptions.Jsx));
    if (updatedText != node.Text()) {
        var updated = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(updatedText, node.AsStringLiteral().TokenFlags);
        emitContext.SetOriginal(updated, Ꮡnode);
        emitContext.AssignCommentAndSourceMapRanges(updated, Ꮡnode);
        return updated;
    }
    return Ꮡnode;
}

internal static ж<astꓸStatement> createEmptyImports(ж<printer.NodeFactory> factory) {
    return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewExportDeclaration(
        nil,
        false,
        factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedExports(factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default)),
        nil,
        nil);
}

internal static ж<astꓸStringLiteralNode> getExternalModuleNameLiteral(ж<printer.NodeFactory> factory, ж<ast.Node> importNode, ref ast.SourceFile sourceFile, any host, printer.EmitResolver resolver, ref core.CompilerOptions compilerOptions) {
    var moduleName = ast.GetExternalModuleName(importNode);
    if (moduleName != nil && ast.IsStringLiteral(moduleName)) {
        var name = tryGetModuleNameFromDeclaration(importNode, host, ref (factory).DerefOrNull(), resolver, ref compilerOptions);
        if (name == nil) {
            name = tryRenameExternalModule(ref (factory).DerefOrNull(), ref (moduleName).DerefOrNull(), ref sourceFile);
        }
        if (name == nil) {
            name = factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(moduleName.Text(), ast.TokenFlagsNone);
        }
        return name;
    }
    return default;
}

internal static ж<astꓸStringLiteralNode> tryGetModuleNameFromFile(ref printer.NodeFactory factory, ж<ast.SourceFile> @file, any host, ref core.CompilerOptions options) {
    if (@file == nil) {
        return default;
    }
    return default;
}

internal static ж<astꓸStringLiteralNode> tryGetModuleNameFromDeclaration(ж<ast.Node> declaration, any host, ref printer.NodeFactory factory, printer.EmitResolver resolver, ref core.CompilerOptions compilerOptions) {
    if (resolver == nil) {
        return default;
    }
    return tryGetModuleNameFromFile(ref factory, resolver.GetExternalModuleFileFromDeclaration(declaration), host, ref compilerOptions);
}

internal static @string getExternalModuleNameFromPath(any host, @string fileName, @string referencePath) {
    return "";
}

internal static ж<astꓸStringLiteralNode> tryRenameExternalModule(ref printer.NodeFactory factory, ref astꓸLiteralExpression moduleName, ref ast.SourceFile sourceFile) {
    return default;
}

internal static bool isFileLevelReservedGeneratedIdentifier(ж<printer.EmitContext> ᏑemitContext, ж<astꓸIdentifierNode> name) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    var info = emitContext.GetAutoGenerateInfo(name);
    return info != nil && info.Flags.IsFileLevel() && info.Flags.IsOptimistic() && info.Flags.IsReservedInNestedScopes();
}

internal static bool isSimpleInlineableExpression(ж<astꓸExpression> expression) {
    return !ast.IsIdentifier(expression) && transformers.IsSimpleCopiableExpression(expression);
}

} // end moduletransforms_package
