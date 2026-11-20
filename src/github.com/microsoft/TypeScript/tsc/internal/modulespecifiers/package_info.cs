// go2cs code converter defines `global using` statements here for imported type
// aliases as package references are encountered via `import' statements. Exported
// type aliases that need a `global using` declaration will be loaded from the
// referenced package by parsing its 'package_info.cs' source file and reading its
// defined `GoTypeAlias` attributes.

// Package name separator "dot" used in imported type aliases is extended Unicode
// character '\uA4F8' which is a valid character in a C# identifier name. This is
// used to simulate Go's package level type aliases since C# does not yet support
// importing type aliases at a namespace level.

// <ImportedTypeAliases>
global using astꓸLiteralLikeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStringLiteralLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using coreꓸResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package;

// For encountered type alias declarations, e.g., `type Table = map[string]int`,
// go2cs code converter will generate a `global using` statement for the alias in
// the converted source, e.g.: `global using Table = go.map<go.@string, nint>;`.
// Although scope of `global using` is available to all files in the project, all
// converted Go code for the project targets the same package, so `global using`
// statements will effectively have package level scope.

// Additionally, `GoTypeAlias` attributes will be generated here for exported type
// aliases. This allows the type alias to be imported and used from other packages
// when referenced.

// <ExportedTypeAliases>
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<ModuleSpecifierGenerationHost, go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package.OutputPathsHost>]
[assembly: GoImplement<SourceFileForSpecifierGeneration, go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HasFileName>]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile, SourceFileForSpecifierGeneration>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(NodeModulePathParts))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Symbol))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolvedModule))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package.PackageJson))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package.SourceOutputAndProjectReference))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/modulespecifiers/compare.go", "compare.cs", "7oKCgpQ=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/modulespecifiers/preferences.go", "preferences.cs", "AAscoqaCgoKCpqaKgoKCgqqCgoSElISUgpSCyoKUpqyClISCmIKUpoKmgq6EgpSmpgAAEIKClIKmgpQADBwAAhAABhCCggAHEoKChoKClIKClJSUgpSkpIKUpIKUpLSmjoKCgoKUpqS0tNoACBY=", "240-249:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/modulespecifiers/specifiers.go", "specifiers.cs", "ABYmAAAUAAoYAAAUgoKClKaCgqiUAAcSpgAAEgAGEAAOIKKCggADHIKCpoKCpoKClIKClIKClIKWgqYACBKIgu4AACCIpq6CgoKmgpqCgoKCgoKCpoKClIKClJSCgoKUqoKsgqqsgoKCgoKmgoKClIKCgpSCgoKCAAgSgoKC2IKCqIKmgoKmAAYQgoLKgri6goKCAAgSpgADEoKUgoKCgoKCgoKmgoSUgoKClISUgriCpp6CgoKUgoKClIKChriCgpQABxKClIKCgoqUlAAAFLiCpIKkgpSmAAISgpSCppSCgoKUgtyCgpSmgoKCgoKUpoKCAAgUgoIACBSCpoKClJSCpoKCgpSCpoKmgoKClJSClIKCAAASpoKUAAMapqiClKaMgqaCgqaCgoKUgoKClIKUhIK4lIKGlKSkqIKCgoKCpoKUlKSCyAAAEIKCpoKCgoKCuIKCgsqClKYAABSCgqiClIKCgoKCgoQABxKCgoKCgpSCloKCgpSCloKCgsqCpoaUgqiCAAkUAAISgoKUgoKCgoKEgoKUuIKCpoKIggABEIKCgqaUgoIACBSCyoLugoKUgoIABhCClKiCgoKCgoKCuI6CuISCAAASuKYAABKMgoKCgoKClIKCuAAMHAACEIKmgoKUlIKCpoKUpIKCgoKClIKUgoKCgpQAChiC2gAHEAACEIKCgoKCgpQAAU6CgszKgtyCgoKIgoK4iLimAAAQ1gAAGJSkloKCgoKmgoKClJTKlIjGgoKUgoKUgoKUgoKUgoKCgsaCgoKClIKClIKClIKClIKCgoKC2KSCgoKC2oKCgoKC6qQAAhIAABIAChgAABQAChgAABSCgpSCgqaCgrg=", "217-219:1;286-286:1;308-345:2;332-341:2.1;410-410:1;1182-1182:1;1183-1185:2", "", "63=GetSourceOfProjectReferenceIfOutputIncluded/1/1/1,getInfo/1/1/1;134=UseCaseSensitiveFileNames/1/1/3;187=Contains/2/3/1,Contains/3/3/2;219=ContainsNodeModules/1/1/2;231=GetGlobalTypingsCacheLocation/1/1/1,GetDirectoryPath/1/1/2;251=UseCaseSensitiveFileNames/1/1/4;260=ContainsNodeModules/1/1/2;277=ContainsNodeModules/1/1/2;407=UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;432=prefersTsExtension/1/1/6;498=UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;591=UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;751=GetPaths/1/1/2;777=UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;921=HasPrefix/1/1/1,HasSuffix/1/1/2,validateEnding/1/1/3;965=UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;972=ComparePaths/2/4/1,ComparePaths/3/4/2,ComparePaths/4/4/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/modulespecifiers/util.go", "util.cs", "ABo0loSAgqSAgqimgqaCgoKClIKmpqKUgoKCgoKCgriCgpSClPyUgoKCgqaClIKCpoKmgpSCgqaCgoKUggAFHoKClKaClKSkpsyChIaUgoKCgqaCgoKUroKCgpSmhsyCgoKCuKaigoKCgqa2gqaCuIKUpoKClIKUpoKCgoKUpoIAECaKgoKClIKClIKCgpSCgoLGgpSCxoKU6pSC7qYAAhCCgoKCpqaCgoKmpoKCgqaCgqaCgqaCpoKCpqoAAhCCgqaCAAgUloKCloKkhIKCgpSUgpaCpKiClKSAgqSkgoKClJaAgqSkqIKUpIKCgpSWpKg=", "", "", "158=Contains/1/1/1,HasSuffix/2/2/2;381=GetDefaultResolutionModeForFile/1/1/6")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("modulespecifiers", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/modulespecifiers")]
public static partial class modulespecifiers_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct ambientModuleInfo {}
    internal partial struct nodeModulesPathParseState {}
    internal partial struct pkgJsonDirAttemptResult {}
    internal partial struct regexPatternCacheKey {}
    internal partial struct specPair {}
    public partial interface CheckerShape {}
    public partial interface ModuleSpecifierGenerationHost {}
    public partial interface SourceFileForSpecifierGeneration {}
    public partial struct ImportModuleSpecifierEndingPreference {}
    public partial struct ImportModuleSpecifierPreference {}
    public partial struct Info {}
    public partial struct MatchingMode {}
    public partial struct ModulePath {}
    public partial struct ModuleSpecifierEnding {}
    public partial struct ModuleSpecifierOptions {}
    public partial struct ModuleSpecifierPreferences {}
    public partial struct ModuleSpecifiersResult {}
    public partial struct NodeModulePathParts {}
    public partial struct RelativePreferenceKind {}
    public partial struct ResultKind {}
    public partial struct UserPreferences {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸmodule() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸoutputpaths() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸpackagejson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸsymlinks() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtsoptions() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸregexp() => builtin.initPackage(typeof(regexp_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    // </ImportInitializers>
}
