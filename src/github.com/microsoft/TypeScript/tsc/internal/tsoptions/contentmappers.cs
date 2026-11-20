namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;

partial class tsoptions_package {

internal static (contentmapper.Manifest, @string, ж<ast.Diagnostic>) resolveContentMapperManifest(ParseConfigHost host, @string containingFile, @string packageName) {
    var resolver = module.NewResolver(new ParseConfigHostᴠResolutionHost(host), Ꮡ(new core.CompilerOptions(ModuleResolution: core.ModuleResolutionKindBundler)), "", "", default);
    var resolved = resolver.ResolvePackageDirectory(packageName, containingFile, core.ResolutionModeNone, default);
    if (resolved == nil || resolved.ResolvedFileName == ""u8) {
        return (new contentmapper.Manifest(), "", ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_package_0_could_not_be_resolved, packageName));
    }
    @string packageDirectory = resolved.ResolvedFileName;

    @string packageJsonPath = tspath.CombinePaths(packageDirectory, "package.json");
    var (contents, ok) = host.FS().ReadFile(packageJsonPath);
    if (!ok) {
        return (new contentmapper.Manifest(), packageDirectory, ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_package_0_could_not_be_resolved, packageName));
    }
    var (fields, err) = packagejson.Parse(slice<byte>(contents));
    if (err != nil) {
        return (new contentmapper.Manifest(), packageDirectory, ast.NewCompilerDiagnostic(diagnostics.The_package_json_of_the_content_mapper_package_0_could_not_be_parsed, packageName));
    }
    var (name, _) = fields.Name.GetValue();
    if (name == ""u8) {
        return (new contentmapper.Manifest(), packageDirectory, ast.NewCompilerDiagnostic(diagnostics.The_package_json_of_the_content_mapper_package_0_does_not_specify_a_name, packageName));
    }
    var (version, _) = fields.Version.GetValue();

    (var cm, ok) = fields.ContentMapper.GetValue();
    if (!ok) {
        return (new contentmapper.Manifest(), packageDirectory, ast.NewCompilerDiagnostic(diagnostics.The_package_json_of_the_content_mapper_package_0_does_not_declare_a_typescript_contentMapper_object, packageName));
    }
    (var exec, ok) = cm.Exec.GetValue();
    if (!ok || len(exec) == 0) {
        return (new contentmapper.Manifest(), packageDirectory, ast.NewCompilerDiagnostic(diagnostics.The_typescript_contentMapper_exec_of_the_content_mapper_package_0_must_be_a_non_empty_array_of_strings, packageName));
    }
    var (compilerOptions, _) = cm.CompilerOptions.GetValue();
    var (dynamicConfig, _) = cm.DynamicConfig.GetValue();
    return (new contentmapper.Manifest(Name: name, Version: version, Exec: exec, CompilerOptions: compilerOptions, DynamicConfig: dynamicConfig), packageDirectory, default);
}

} // end tsoptions_package
