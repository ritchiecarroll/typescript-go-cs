namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using outputpaths = go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using symlinks = go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package;
using declarations = go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using checker = go.github.com.microsoft.TypeScript.tsc.@internal.checker_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.transformers;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using static compiler_package;

partial class compiler_package {

partial interface EmitHost :
    printer.EmitHost,
    declarations.DeclarationEmitHost
{
    ж<core.CompilerOptions> Options();
    slice<ж<ast.SourceFile>> SourceFiles();
    bool UseCaseSensitiveFileNames();
    @string GetCurrentDirectory();
    @string CommonSourceDirectory();
    bool IsEmitBlocked(@string @file);
    new printer.EmitResolver GetEmitResolver();
    new ж<tsoptions.SourceOutputAndProjectReference> GetProjectReferenceFromSource(tspath.Path path);
}

internal static EmitHost _ᴛ2ʗ = new emitHostжEmitHost(((ж<emitHost>)nil));

partial struct emitHost {
    internal ж<Program> program;
    internal printer.EmitResolver emitResolver;
}

internal static (ж<emitHost>, Action) newEmitHost(context.Context ctx, ж<Program> Ꮡprogram, ж<ast.SourceFile> @file) {
    ref var program = ref Ꮡprogram.DerefOrNull();

    var (checker, done) = program.GetTypeCheckerForFile(ctx, @file);
    return (Ꮡ(new emitHost(
        program: Ꮡprogram,
        emitResolver: new checker.EmitResolverжEmitResolver(checker.GetEmitResolver())
    )), done);
}

} partial class compilerᴛemitHost {

internal static coreꓸResolutionMode GetModeForUsageLocation(this ref emitHost host, ast.HasFileName @file, ж<astꓸStringLiteralLike> moduleSpecifier) {
    return host.program.GetModeForUsageLocation(@file, moduleSpecifier);
}

internal static ж<module.ResolvedModule> GetResolvedModuleFromModuleSpecifier(this ref emitHost host, ast.HasFileName @file, ж<astꓸStringLiteralLike> moduleSpecifier) {
    return host.program.GetResolvedModuleFromModuleSpecifier(@file, moduleSpecifier);
}

internal static coreꓸResolutionMode GetDefaultResolutionModeForFile(this ref emitHost host, ast.HasFileName @file) {
    return host.program.GetDefaultResolutionModeForFile(@file);
}

internal static core.ModuleKind GetEmitModuleFormatOfFile(this ref emitHost host, ast.HasFileName @file) {
    return host.program.GetEmitModuleFormatOfFile(@file);
}

internal static bool FileExists(this ref emitHost host, @string path) {
    return host.program.FileExists(path);
}

internal static @string GetGlobalTypingsCacheLocation(this ref emitHost host) {
    return host.program.GetGlobalTypingsCacheLocation();
}

internal static @string GetNearestAncestorDirectoryWithPackageJson(this ref emitHost host, @string dirname) {
    return host.program.GetNearestAncestorDirectoryWithPackageJson(dirname);
}

internal static ж<packagejson.InfoCacheEntry> GetPackageJsonInfo(this ref emitHost host, @string pkgJsonPath) {
    return host.program.GetPackageJsonInfo(pkgJsonPath);
}

internal static @string GetSourceOfProjectReferenceIfOutputIncluded(this ref emitHost host, ast.HasFileName @file) {
    return host.program.GetSourceOfProjectReferenceIfOutputIncluded(@file);
}

internal static ж<tsoptions.SourceOutputAndProjectReference> GetProjectReferenceFromSource(this ref emitHost host, tspath.Path path) {
    return host.program.GetProjectReferenceFromSource(path);
}

internal static slice<@string> GetRedirectTargets(this ref emitHost host, tspath.Path path) {
    return host.program.GetRedirectTargets(path);
}

internal static ast.ModifierFlags GetEffectiveDeclarationFlags(this ref emitHost host, ж<ast.Node> node, ast.ModifierFlags flags) {
    return host.GetEmitResolver().GetEffectiveDeclarationFlags(node, flags);
}

internal static declarations.OutputPaths GetOutputPathsFor(this ж<emitHost> Ꮡhost, ж<ast.SourceFile> @file, bool forceDtsPaths) {
    ref var host = ref Ꮡhost.DerefOrNull();

    return new outputpaths_OutputPathsжOutputPaths(outputpaths.GetOutputPathsFor(@file, host.Options(), new emitHostжOutputPathsHost(Ꮡhost), new outputpaths.ForceEmitPaths(Dts: forceDtsPaths)));
}

internal static bool SourceFileMayBeEmitted(this ж<emitHost> Ꮡhost, ж<ast.SourceFile> @file, bool forceDtsEmit) {
    return sourceFileMayBeEmitted(@file, new emitHostжSourceFileMayBeEmittedHost(Ꮡhost), forceDtsEmit, false);
}

internal static ж<ast.SourceFile> GetSourceFileFromReference(this ref emitHost host, ж<ast.SourceFile> origin, ж<ast.FileReference> @ref) {
    return host.program.GetSourceFileFromReference(origin, @ref);
}

internal static ж<core.CompilerOptions> Options(this ref emitHost host) { return host.program.Options(); }
internal static slice<ж<ast.SourceFile>> SourceFiles(this ref emitHost host) { return host.program.SourceFiles(); }
internal static @string GetCurrentDirectory(this ref emitHost host) { return host.program.GetCurrentDirectory(); }
internal static @string CommonSourceDirectory(this ref emitHost host) { return host.program.CommonSourceDirectory(); }

internal static slice<@string> ContentMapperExtensions(this ref emitHost host) {
    return host.program.ContentMapperExtensions();
}

internal static bool UseCaseSensitiveFileNames(this ref emitHost host) {
    return host.program.UseCaseSensitiveFileNames();
}

internal static bool IsEmitBlocked(this ref emitHost host, @string @file) {
    return host.program.IsEmitBlocked(@file);
}

internal static error WriteFile(this ref emitHost host, @string fileName, @string text) {
    return host.program.Host().FS().WriteFile(fileName, text);
}

internal static printer.EmitResolver GetEmitResolver(this ref emitHost host) {
    return host.emitResolver;
}

internal static bool IsSourceFileFromExternalLibrary(this ref emitHost host, ж<ast.SourceFile> @file) {
    return host.program.IsSourceFileFromExternalLibrary(@file);
}

internal static ж<symlinks.KnownSymlinks> GetSymlinkCache(this ref emitHost host) {
    return host.program.GetSymlinkCache();
}

internal static ж<module.ResolvedModule> ResolveModuleName(this ref emitHost host, @string moduleName, @string containingFile, coreꓸResolutionMode resolutionMode) {
    var (resolved, _) = host.program.resolver.ResolveModuleName(moduleName, containingFile, resolutionMode, default);
    return resolved;
}

} // end compilerᴛemitHost
