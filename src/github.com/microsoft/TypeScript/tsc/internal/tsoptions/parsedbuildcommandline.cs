namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tsoptions_package;

partial class tsoptions_package {

partial struct ParsedBuildCommandLine {
    public ж<core.BuildOptions> BuildOptions; /*`json:"buildOptions"`*/
    public ж<core.CompilerOptions> CompilerOptions; /*`json:"compilerOptions"`*/
    public ж<core.WatchOptions> WatchOptions; /*`json:"watchOptions"`*/
    public slice<@string> Projects; /*`json:"projects"`*/
    public slice<ж<ast.Diagnostic>> Errors; /*`json:"errors"`*/
    public any Raw; /*`json:"raw"`*/
    internal tspath.ComparePathsOptions comparePathsOptions;
    internal slice<@string> resolvedProjectPaths;
    internal sync.Once resolvedProjectPathsOnce;
    internal locale.Locale locale;
    internal sync.Once localeOnce;
}

} partial class tsoptionsᴛParsedBuildCommandLine {

public static slice<@string> ResolvedProjectPaths(this ж<ParsedBuildCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑresolvedProjectPathsOnce.Do(() => {
        Ꮡp.Value.resolvedProjectPaths = core.Map(Ꮡp.Value.Projects, @string (@string project) => core.ResolveConfigFileNameOfProjectReference(
                tspath.ResolvePath(Ꮡp.Value.comparePathsOptions.CurrentDirectory, project)));
    });
    return p.resolvedProjectPaths;
}

public static locale.Locale Locale(this ж<ParsedBuildCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑlocaleOnce.Do(() => {
        (Ꮡp.Value.locale, _) = locale.Parse(Ꮡp.Value.CompilerOptions.Locale);
    });
    return p.locale;
}

} // end tsoptionsᴛParsedBuildCommandLine
