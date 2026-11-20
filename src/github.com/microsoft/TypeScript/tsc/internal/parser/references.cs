namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class parser_package {

internal static void collectExternalModuleReferences(ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    foreach (var (_, node) in range(@file.Statements.Nodes)) {
        collectModuleReferences(Ꮡfile, node, false);
    }

    if ((ast.NodeFlags)(@file.Flags & ast.NodeFlagsPossiblyContainsDynamicImport) != 0 || ast.IsInJSFile(Ꮡfile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        ast.ForEachDynamicImportOrRequireCall(Ꮡfile, true, true, (ж<ast.Node> node, ж<astꓸExpression> moduleSpecifier) => {
            ast.SetImportsOfSourceFile(Ꮡfile, append(Ꮡfile.Value.Imports(), moduleSpecifier));
            return false;
        });
    }
}

internal static void collectModuleReferences(ж<ast.SourceFile> Ꮡfile, ж<astꓸStatement> Ꮡnode, bool inAmbientModule) {
    ref var @file = ref Ꮡfile.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsAnyImportOrReExport(Ꮡnode)) {
        var moduleNameExpr = ast.GetExternalModuleName(Ꮡnode);
        if (moduleNameExpr != nil && ast.IsStringLiteral(moduleNameExpr)) {
            @string moduleName = moduleNameExpr.Text();
            if (moduleName != ""u8 && (!inAmbientModule || !tspath.IsExternalModuleNameRelative(moduleName))) {
                ast.SetImportsOfSourceFile(Ꮡfile, append(@file.Imports(), moduleNameExpr));
                if (@file.UsesUriStyleNodeCoreModules != core.TSTrue && !@file.IsDeclarationFile) {
                    if (strings.HasPrefix(moduleName, "node:") && !core.ExclusivelyPrefixedNodeCoreModules[moduleName]) {
                        @file.UsesUriStyleNodeCoreModules = core.TSTrue;
                    } else if (@file.UsesUriStyleNodeCoreModules == core.TSUnknown && core.UnprefixedNodeCoreModules[moduleName]) {
                        @file.UsesUriStyleNodeCoreModules = core.TSFalse;
                    }
                }
            }
        }
        return;
    }
    if (ast.IsModuleDeclaration(Ꮡnode) && ast.IsAmbientModule(Ꮡnode) && (inAmbientModule || ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsAmbient) || @file.IsDeclarationFile)) {
        @string nameText = node.AsModuleDeclaration().Name().Text();
        if (ast.IsExternalModule(Ꮡfile) || (inAmbientModule && !tspath.IsExternalModuleNameRelative(nameText))) {
            @file.ModuleAugmentations = append(@file.ModuleAugmentations, node.AsModuleDeclaration().Name());
        } else if (!inAmbientModule) {
            @file.AmbientModuleNames = append(@file.AmbientModuleNames, nameText);
            if (node.Body() != nil) {
                foreach (var (_, statement) in range(node.Body().Statements())) {
                    collectModuleReferences(Ꮡfile, statement, true);
                }
            }
        }
    }
}

} // end parser_package
