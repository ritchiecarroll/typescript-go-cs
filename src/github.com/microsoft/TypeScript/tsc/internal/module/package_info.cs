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
global using coreꓸResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.module_package;

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
[assembly: GoImplement(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package.Expected<>), typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package.TypeValidatedField), Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(ParsedPatterns))]
[assembly: GoFieldAccess(typeof(ResolvedModule))]
[assembly: GoFieldAccess(typeof(Resolver))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package.InfoCacheEntry))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package.PackageJson))]
[assembly: GoFieldAccess(typeof(moduleResolutionCache))]
[assembly: GoFieldAccess(typeof(parsedPatternsCache))]
[assembly: GoFieldAccess(typeof(resolutionState))]
[assembly: GoFieldAccess(typeof(resolved))]
[assembly: GoFieldAccess(typeof(typeRefDirectiveResolutionCache))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/module/cache.go", "cache.cs", "ABksgqaCABIegqaCAAoOgoKClAALHorKgoKU", "", "", "84=NewInfoCache/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/module/resolver.go", "resolver.cs", "ACU4gqaixoKmggANHLKCuKKClAAVOAACFAAHEIKCgpSmgqaUgoK0goK0grSmgoKUgpQADyKOAAkUjtyClJTGgoKUpqKmgsaCgtisgpSUAAcQgoK4lIKCgqaClIKmlKaigpTugoK4goKCpoKCgqaCuIKUgrS2goKClKa4gpSmooKCgoKClKayhqYACBSClIKClIKCpqKCgoKCxqKCgoIABhAACRKkgoKUgoKCgoKUlISCgoKUpoKmgqiCgoKUgpSClIKUpoKCgpSmgoKClKyigpSCgoKCgpSUgoKClJSCpqaiuIKUlvaigoKClKaCAAASgpSCgoKClJSmooCCtoKCgqaCgqaCgpSUgpSAgqSCgriCgrqmooKChJSCgpSCgoKUgoKClAABHoKUgoKClKaigoKUlIKCgoKUlIaClKaCpoKUprSCpoKClLSCgoLGgpSCgriClKa+goK4goKCppSCgoKCgoKCgoKCgoKCuPbSlIKCgpSUgoKCgpSCgoKUkoKClIIABxCUgpSUgoKUlIKCgpSUhoKCgpSmgoKClJSUgoKUlIKClICCgqSmgpSCgoKUgoKClIKUgoKmgriClKSCgpSUgoLagpSmgpTWhAAGHoKEgoSUAAgWgqaCgrqCgpSCgoKCgoKClIKCgoIACBKmhoKCgpSClKaigoIAARCChIKClIKogoKUlKaiqIKClJamooKCgpSmgoK4goKCgpSUpqYAAhKCgoKCpoKClKaEhIKClIKCpoKCyoKCgoKmgoKUioKCpqaCgoSmgoKMlIKCgoKUgoLKpqKCiIKCgqamooKUgoKCgoKCgpSmooKUgoKUgoKClIKCgoK4poKCuIaUpqKCpoSUuIKmooKCppSCggAIGKKCgoKUgoKCgpaCgpIABRCCgpSCgoK4pqKCpoKmlIKCiIKClJaCpoKCuIKClJaClIaAgraCloKElIKClIKmgqamooKUgoKCgpSUgoKCgqamgoKUmoKUpoSCgqiCpqaCgoKUgoSCgpSmgoKUpoKCgqaUgoKmgoKmgoKmpIKCpoKCpoKCpqSCgqaCgqaqgoCCpICCtoKCpoKAgqSAgrakgoCCpICCtoKCpoKAgqSAgraCgqakhIKCpoKCpsiCgpLcpoKCpoKCgoKCpqaCgoKUgoKUpqKCgqamspiCgoKCyoKCqoKCqIKCgoKUgoKCpoKClKaCgoKUlIKUgoKElLiCgoSUuoKClJSsgoKSjoLcpoKS7qaigpSClIKAgqSAgraClKaClIKCgpSUgpS4goSCkoKUAAcQgpSClMqmooKCgoKCgpQACBKmooKCgpSClIKCgpSCgoKCgoKCgoKCgoKClIKmpoKCgpSmgoKClIKmgpSmooKUgoKUlIKClNaCgpSClKaCgpTGooKClIKClKaClIKUgqailJS0tLSCgoKUgoKClKaCgoKClIKClJQACRCixoKUgoKCgoKUyoKCgpSCpoKCgpS43KKCyoKUAAIQgoKCgoKUpoKClIKClKiCrIKCprKCgpSogoKCgoKCgoKCgoaUgoKCAAYSgoKClKYAEz6CgpTGsoKCgoKCpoLMggAHEoIACBSCgoKmAAgUgpSmgoKCgoKUAAsUioKUgoKCgpSCgoIACRaCgoKUggAHEoKClIKCAAcSgoKUgoKCgqaCgoKCgoKUgoKClLiCgoLclILGgoKmxramgoKmgoKCuA==", "498-503:1;510-510:1;763-765:1;1013-1020:1;1097-1119:1;1256-1258:1;1344-1346:1;1636-1659:1;2252-2337:1", "", "109=GetCompilerOptionsWithRedirect/1/1/3;175=GetCurrentDirectory/1/1/2,FS/1/1/2,UseCaseSensitiveFileNames/1/1/2,newCaches/1/1/2;237=getRedirectConfigName/1/1/4;277=getRedirectConfigName/1/1/4;354=IsExternalModuleNameRelative/1/1/1,ExtensionIsOneOf/1/1/2;397=String/1/1/4;541=IsExternalModuleNameRelative/1/1/4,IsResolved/1/1/5,extensionIsOk/1/1/7,Contains/1/1/8;598=Contains/1/1/2;916=Contains/1/1/2,GetDirectoryPath/1/1/4,FS/1/1/7,UseCaseSensitiveFileNames/1/1/7,GetCurrentDirectory/1/1/8,ContainsPath/1/1/3;929=IfElse/1/2/3,IfElse/2/2/8;944=FS/1/1/1,UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;1124=Exists/1/1/1,IsFalsy/1/1/2;1147=IsExternalModuleNameRelative/1/1/3;1203=FS/1/1/1,UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;1818=readPackageJsonPeerDependencies/1/1/4;2201=FS/1/1/1,GetCurrentDirectory/1/1/2,Array/1/1/4;2216=GetRelativePathFromDirectory/1/1/2,ResolvePath/1/1/2;2263=FS/1/1/1,GetCurrentDirectory/1/1/2,Array/1/1/4,AsString/1/1/7,Replace/1/1/7,ChangeFullExtension/1/1/7;2279=AsString/1/1/5,HasSuffix/1/1/5,IfElse/1/1/5;2293=ResolvePath/1/1/2,AsString/1/1/5,HasSuffix/1/1/5,IfElse/1/1/5", "", "785:33,786:25")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/module/types.go", "types.cs", "ADRsgqaCgpQAEh6iAA8YggAOHKKCgpSClIKUgpSmgoKClIKUgpSClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/module/util.go", "util.cs", "AAwayIKClIKClKaCgoKCpoKCgpSmgoKCgoKCpoKUpoKCgoKUlKaCgoKUpoKmgoKClKaCgoKCgpSCgqaClIKUgpSClIKUgpSsooKClKaCgpSmgoKUpoKClKaCpqqkpIKUpKSkzKKClKSClKSkpKQ=", "126-131:1;133-138:2;140-145:3;147-152:4")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("module", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/module")]
public static partial class module_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct caches {}
    internal partial struct extensions {}
    internal partial struct moduleResolutionCache {}
    internal partial struct moduleResolutionCacheKey {}
    internal partial struct parsedPatternsCache {}
    internal partial struct resolutionState {}
    internal partial struct resolved {}
    internal partial struct tracer {}
    internal partial struct typeRefDirectiveResolutionCache {}
    internal partial struct typeRefDirectiveResolutionCacheKey {}
    public partial interface ResolutionHost {}
    public partial interface ResolvedProjectReference {}
    public partial struct DiagAndArgs {}
    public partial struct Ending {}
    public partial struct ModeAwareCache<T> {}
    public partial struct ModeAwareCacheKey {}
    public partial struct NodeResolutionFeatures {}
    public partial struct PackageId {}
    public partial struct ParsedPatterns {}
    public partial struct ResolvedEntrypoint {}
    public partial struct ResolvedModule {}
    public partial struct ResolvedTypeReferenceDirective {}
    public partial struct Resolver {}
    public partial struct ResolverOptions {}
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
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸpackagejson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸsemver() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.semver_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸvfs() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸvfsꓸvfsmatch() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.vfs.vfsmatch_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸmathꓸbits() => builtin.initPackage(typeof(math.bits_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(module_package.PackageId)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛPackageId;
[GoMethodHost(typeof(module_package.ResolvedEntrypoint)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛResolvedEntrypoint;
[GoMethodHost(typeof(module_package.ResolvedModule)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛResolvedModule;
[GoMethodHost(typeof(module_package.ResolvedTypeReferenceDirective)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛResolvedTypeReferenceDirective;
[GoMethodHost(typeof(module_package.Resolver)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛResolver;
[GoMethodHost(typeof(module_package.extensions)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛextensions;
[GoMethodHost(typeof(module_package.moduleResolutionCache)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛmoduleResolutionCache;
[GoMethodHost(typeof(module_package.parsedPatternsCache)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛparsedPatternsCache;
[GoMethodHost(typeof(module_package.resolutionState)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛresolutionState;
[GoMethodHost(typeof(module_package.resolved)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛresolved;
[GoMethodHost(typeof(module_package.tracer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛtracer;
[GoMethodHost(typeof(module_package.typeRefDirectiveResolutionCache)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduleᴛtypeRefDirectiveResolutionCache;
// </MethodHolders>
