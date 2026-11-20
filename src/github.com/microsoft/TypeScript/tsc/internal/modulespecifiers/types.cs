namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using symlinks = go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using outputpaths = go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package;

partial class modulespecifiers_package {

partial interface SourceFileForSpecifierGeneration :
    ast.HasFileName
{
    slice<ж<astꓸStringLiteralLike>> Imports();
    bool IsJS();
}

partial interface CheckerShape {
    ж<ast.Symbol> GetSymbolAtLocation(ж<ast.Node> node);
    ж<ast.Symbol> GetAliasedSymbol(ж<ast.Symbol> symbol);
}

partial struct ResultKind /*num:uint8*/;

public static ResultKind ResultKindNone => /* iota */ 0;
public static ResultKind ResultKindNodeModules => 1;
public static ResultKind ResultKindPaths => 2;
public static ResultKind ResultKindRedirect => 3;
public static ResultKind ResultKindRelative => 4;
public static ResultKind ResultKindAmbient => 5;

partial struct ModuleSpecifiersResult {
    public slice<@string> Specifiers;
    public ResultKind Kind;
    public ж<ast.Symbol> AmbientModuleSymbol;
}

partial struct ModulePath {
    public @string FileName;
    public bool IsInNodeModules;
    public bool IsRedirect;
}

partial interface ModuleSpecifierGenerationHost :
    outputpaths.OutputPathsHost
{
    ж<symlinks.KnownSymlinks> GetSymlinkCache();
    @string GetGlobalTypingsCacheLocation();
    ж<tsoptions.SourceOutputAndProjectReference> GetProjectReferenceFromSource(tspath.Path path);
    slice<@string> GetRedirectTargets(tspath.Path path);
    @string GetSourceOfProjectReferenceIfOutputIncluded(ast.HasFileName @file);
    bool FileExists(@string path);
    @string GetNearestAncestorDirectoryWithPackageJson(@string dirname);
    ж<packagejson.InfoCacheEntry> GetPackageJsonInfo(@string pkgJsonPath);
    coreꓸResolutionMode GetDefaultResolutionModeForFile(ast.HasFileName @file);
    ж<module.ResolvedModule> GetResolvedModuleFromModuleSpecifier(ast.HasFileName @file, ж<astꓸStringLiteralLike> moduleSpecifier);
    coreꓸResolutionMode GetModeForUsageLocation(ast.HasFileName @file, ж<astꓸStringLiteralLike> moduleSpecifier);
}

partial struct ImportModuleSpecifierPreference /*@string*/;

public static readonly ImportModuleSpecifierPreference ImportModuleSpecifierPreferenceNone = ""u8;
public static readonly ImportModuleSpecifierPreference ImportModuleSpecifierPreferenceShortest = "shortest"u8;
public static readonly ImportModuleSpecifierPreference ImportModuleSpecifierPreferenceProjectRelative = "project-relative"u8;
public static readonly ImportModuleSpecifierPreference ImportModuleSpecifierPreferenceRelative = "relative"u8;
public static readonly ImportModuleSpecifierPreference ImportModuleSpecifierPreferenceNonRelative = "non-relative"u8;

partial struct ImportModuleSpecifierEndingPreference /*@string*/;

public static readonly ImportModuleSpecifierEndingPreference ImportModuleSpecifierEndingPreferenceNone = ""u8;
public static readonly ImportModuleSpecifierEndingPreference ImportModuleSpecifierEndingPreferenceAuto = "auto"u8;
public static readonly ImportModuleSpecifierEndingPreference ImportModuleSpecifierEndingPreferenceMinimal = "minimal"u8;
public static readonly ImportModuleSpecifierEndingPreference ImportModuleSpecifierEndingPreferenceIndex = "index"u8;
public static readonly ImportModuleSpecifierEndingPreference ImportModuleSpecifierEndingPreferenceJs = "js"u8;

partial struct UserPreferences {
    public ImportModuleSpecifierPreference ImportModuleSpecifierPreference;
    public ImportModuleSpecifierEndingPreference ImportModuleSpecifierEnding;
    public slice<@string> AutoImportSpecifierExcludeRegexes;
}

partial struct ModuleSpecifierOptions {
    public coreꓸResolutionMode OverrideImportMode;
}

partial struct RelativePreferenceKind /*num:uint8*/;

public static RelativePreferenceKind RelativePreferenceRelative => /* iota */ 0;
public static RelativePreferenceKind RelativePreferenceNonRelative => 1;
public static RelativePreferenceKind RelativePreferenceShortest => 2;
public static RelativePreferenceKind RelativePreferenceExternalNonRelative => 3;

partial struct ModuleSpecifierEnding /*num:uint8*/;

public static ModuleSpecifierEnding ModuleSpecifierEndingMinimal => /* iota */ 0;
public static ModuleSpecifierEnding ModuleSpecifierEndingIndex => 1;
public static ModuleSpecifierEnding ModuleSpecifierEndingJsExtension => 2;
public static ModuleSpecifierEnding ModuleSpecifierEndingTsExtension => 3;

partial struct MatchingMode /*num:uint8*/;

public static MatchingMode MatchingModeExact => /* iota */ 0;
public static MatchingMode MatchingModeDirectory => 1;
public static MatchingMode MatchingModePattern => 2;

} // end modulespecifiers_package
