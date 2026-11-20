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
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package;

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
[assembly: GoTypeAlias("SourceMappedPosition", "go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package.MappedPosition")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<strings_package.Builder, io_package.Writer>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(ECMALineInfo))]
[assembly: GoFieldAccess(typeof(Generator))]
[assembly: GoFieldAccess(typeof(MappedPosition))]
[assembly: GoFieldAccess(typeof(Mapping))]
[assembly: GoFieldAccess(typeof(RawSourceMap))]
[assembly: GoFieldAccess(typeof(SourceMappedPosition))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/sourcemap/decoder.go", "decoder.cs", "ABUmsgACEIIAFzSCxoKmgqaCpoKmgoKCgtyCgoKEgoKCpoSCpoKCgoKUgqaClIKClIKUgqaCgpSClIKmgoKUgqaCgoKClIKmgsqmpoKCgoKCgoKCpoKCpoKmgoKmgqaCpoKCgoKCgoKogoKCqJaCqISWgqbGgpSkpKSkpA==", "76-82:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/sourcemap/generator.go", "generator.cs", "AEKEAYIACRCWgt6CgoKCgoKUpqiigpSClIKogoKCgoKClJSmgqqCAAIQggACEoKmjIKUqIKCgoSUgoLKooKohIKCgoKolISWgrqClISCloKWgpSEgrimooSCgoKCgqaCgoKCgoKCzKKClIKUgoKCqKKClIKUgpSClIKUgpSCqKKClIKUgpSClIKUgpSClIKoooKCgpSCgpQAChaCgoKUqIKmgoKCgoKCgoKCxoKUpKSkpKQ=", "", "", "213=isBacktrackingSourcePosition/1/1/1;314=String/1/1/6,Clone/1/1/7")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/sourcemap/lineinfo.go", "lineinfo.cs", "AA0UgvyCpoKCgoKUlA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/sourcemap/source_mapper.go", "source_mapper.cs", "ACZEggANIoKCgoKUlIKGgoKCpoKCloKEgoKCAAcSgoKCgt647oKogoKUgoLclIKCgpQAAhCChpwAEB6ygpSCppiCpoKCqNyygpSCgpSClIKYgqaCgqj8ooKCgoKCqLiCgpSCgoKCpqaCgoSogqamgoKCgpSClKaCgqiCgoKUgpSCgpSUgpSCgqY=", "61-63:1;132-135:2;136-140:3;145-147:4;148-152:5;177-179:1;209-211:1;265-265:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/sourcemap/util.go", "util.cs", "AAkWooKCgoKCgpSClIK4")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("sourcemap", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/sourcemap")]
public static partial class sourcemap_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    public partial interface Host {}
    public partial interface Source {}
    public partial struct DocumentPosition {}
    public partial struct DocumentPositionMapper {}
    public partial struct ECMALineInfo {}
    public partial struct Generator {}
    public partial struct MappedPosition {}
    public partial struct Mapping {}
    public partial struct MappingsDecoder {}
    public partial struct NameIndex {}
    public partial struct RawSourceMap {}
    public partial struct SourceIndex {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸencodingꓸbase64() => builtin.initPackage(typeof(encoding.base64_package));
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸunicode() => builtin.initPackage(typeof(unicode_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(sourcemap_package.DocumentPositionMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class sourcemapᴛDocumentPositionMapper;
[GoMethodHost(typeof(sourcemap_package.ECMALineInfo)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class sourcemapᴛECMALineInfo;
[GoMethodHost(typeof(sourcemap_package.Generator)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class sourcemapᴛGenerator;
[GoMethodHost(typeof(sourcemap_package.MappedPosition)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class sourcemapᴛMappedPosition;
[GoMethodHost(typeof(sourcemap_package.Mapping)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class sourcemapᴛMapping;
[GoMethodHost(typeof(sourcemap_package.MappingsDecoder)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class sourcemapᴛMappingsDecoder;
// </MethodHolders>
