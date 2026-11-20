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
global using jsonꓸUnmarshalerFrom = go.github.com.go_json_experiment.json_package.UnmarshalerFrom;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;

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
[assembly: GoImplement<ExportsOrImports, go.github.com.go_json_experiment.json_package.UnmarshalerFrom>(Pointer = true)]
[assembly: GoImplement<JSONValue, go.github.com.go_json_experiment.json_package.UnmarshalerFrom>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(ExportsOrImports))]
[assembly: GoFieldAccess(typeof(InfoCache))]
[assembly: GoFieldAccess(typeof(PackageJson))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/packagejson/cache.go", "cache.cs", "AA4aABUeooKCuJSCuKbKgoKCuJSCgriUuLjKgoKmAAwSoqaigpSClIKCgpSCgoKUlJSCAAwSoqaigpSmooKUAAIcooKUAA4agvyigoKUpqKCgqaC", "29-79:1", "", "49=String/1/1/2;72=String/1/1/2;78=AsObject/1/1/2;86=VersionMajorMinor/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/packagejson/expected.go", "expected.cs", "ABMgooKClIKUlLS0tLS0poKmgqaCpoKUpKSkpqTIgsaC")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/packagejson/exportsorimports.go", "exportsorimports.cs", "ABgutIKmgoKUpoKClKaigqaigqaigqaigoCCgoKCgoKCgoK4goKUgoK2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/packagejson/jsonvalue.go", "jsonvalue.cs", "ABksgpSkpKSkpKQADRKCpoKUpKSkpMiCgpSmgoKUpoKClMa0gsaigoKCgoKCgoKUgoKCgoKUgoKCgoKCgpSClKailICCpIKCpIKAgtaAgqSCgoKClJSAgqSCtIKAgqSCtIKAgtaCgILW")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/packagejson/packagejson.go", "packagejson.cs", "ACREgoCCgraAgoK2gIKCtoCCgramgoCCgoLIgIKCgsiAgoKCyICCgoLsgoKCgoKCgoKCgpSClIKUABwogoyClII=")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("packagejson", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/packagejson")]
public static partial class packagejson_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct Parse_parsed {}
    internal partial struct diagnosticAndArgs {}
    internal partial struct objectKind {}
    internal partial struct typeScriptFields {}
    public partial interface TypeValidatedField {}
    public partial struct ContentMapperFields {}
    public partial struct DependencyFields {}
    public partial struct Expected<T> {}
    public partial struct ExportsOrImports {}
    public partial struct Fields {}
    public partial struct HeaderFields {}
    public partial struct InfoCache {}
    public partial struct InfoCacheEntry {}
    public partial struct JSONValue {}
    public partial struct JSONValueType {}
    public partial struct PackageJson {}
    public partial struct PathFields {}
    public partial struct VersionPaths {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸsemver() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.semver_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸreflect() => builtin.initPackage(typeof(reflect_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(packagejson_package.DependencyFields)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛDependencyFields;
[GoMethodHost(typeof(packagejson_package.Expected<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛExpected;
[GoMethodHost(typeof(packagejson_package.ExportsOrImports)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛExportsOrImports;
[GoMethodHost(typeof(packagejson_package.InfoCache)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛInfoCache;
[GoMethodHost(typeof(packagejson_package.InfoCacheEntry)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛInfoCacheEntry;
[GoMethodHost(typeof(packagejson_package.JSONValue)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛJSONValue;
[GoMethodHost(typeof(packagejson_package.JSONValueType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛJSONValueType;
[GoMethodHost(typeof(packagejson_package.PackageJson)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛPackageJson;
[GoMethodHost(typeof(packagejson_package.VersionPaths)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class packagejsonᴛVersionPaths;
// </MethodHolders>
