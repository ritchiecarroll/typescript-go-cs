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
using static go.github.com.go_json_experiment.json.@internal.jsonopts_package;

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
[assembly: GoImplement<ByteLimit, Options>(Pointer = true)]
[assembly: GoImplement<ByteLimit, Options>]
[assembly: GoImplement<DepthLimit, Options>(Pointer = true)]
[assembly: GoImplement<DepthLimit, Options>]
[assembly: GoImplement<Indent, Options>(Pointer = true)]
[assembly: GoImplement<Indent, Options>]
[assembly: GoImplement<IndentPrefix, Options>(Pointer = true)]
[assembly: GoImplement<IndentPrefix, Options>]
[assembly: GoImplement<Struct, Options>(Pointer = true)]
[assembly: GoImplement<supportFormatTag, Options>(Pointer = true)]
[assembly: GoImplement<supportFormatTag, Options>(Promoted = true)]
[assembly: GoImplement<supportFormatTag, Options>]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(Struct))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/internal/jsonopts/options.go", "options.cs", "ACpqAAcQAAkOuIC0hIKCgoKogqSCgoKUpIKCpIKUpIKUpIKUpIKUtIK8gNSigpS0tIKUxoK0grSCtIK0goKClIKUgpSClIKUgpSC6NyCgpSClIKC2oIADB6ysrI=", "", "", "224=Get/3/7/1,Get/4/7/1,Get/5/7/2,Get/6/7/2,Get/7/7/3")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/internal/jsonopts/options_format.go", "options_format.cs", "AA8sksqCAASyAYKClA==")]
// </GoSourcePositionMaps>

namespace go.github.com.go_json_experiment.json.@internal;

[GoPackage("jsonopts", ImportPath = "github.com/go-json-experiment/json/internal/jsonopts")]
public static partial class jsonopts_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface experimentalFormatTagSupporter {}
    internal partial struct supportFormatTag {}
    public partial interface Options {}
    public partial struct ArshalValues {}
    public partial struct ByteLimit {}
    public partial struct CoderValues {}
    public partial struct DepthLimit {}
    public partial struct Indent {}
    public partial struct IndentPrefix {}
    public partial struct Struct {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternal() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.internal_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternalꓸjsonflags() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.@internal.jsonflags_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(jsonopts_package.ByteLimit)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonoptsᴛByteLimit;
[GoMethodHost(typeof(jsonopts_package.DepthLimit)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonoptsᴛDepthLimit;
[GoMethodHost(typeof(jsonopts_package.Indent)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonoptsᴛIndent;
[GoMethodHost(typeof(jsonopts_package.IndentPrefix)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonoptsᴛIndentPrefix;
[GoMethodHost(typeof(jsonopts_package.Struct)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonoptsᴛStruct;
[GoMethodHost(typeof(jsonopts_package.supportFormatTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonoptsᴛsupportFormatTag;
// </MethodHolders>
