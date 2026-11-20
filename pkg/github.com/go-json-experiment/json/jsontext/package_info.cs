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
using static go.github.com.go_json_experiment.json.jsontext_package;

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
[assembly: GoDynamicTypeLift("696e746572666163657b417070656e64537461636b506f696e7465722862205b5d627974652c20776865726520696e7429205b5d627974653b206f6666736574417428706f7320696e742920696e7436343b206f7074696f6e732829202a6769746875622e636f6d2f676f2d6a736f6e2d6578706572696d656e742f6a736f6e2f696e7465726e616c2f6a736f6e6f7074732e5374727563747d", "wrapSyntacticError_state")]
[assembly: GoTypeAlias("Options", "go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<SyntacticError, error>(Pointer = true)]
[assembly: GoImplement<decoderState, wrapSyntacticError_state>(Pointer = true)]
[assembly: GoImplement<encoderState, wrapSyntacticError_state>(Pointer = true)]
[assembly: GoImplement<go.github.com.go_json_experiment.json.@internal.jsonflags_package.Bools, go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options>]
[assembly: GoImplement<ioError, error>(Pointer = true)]
[assembly: GoImplement<numError, error>(Pointer = true)]
[assembly: GoImplement<pointerSuffixError, error>(Pointer = true)]
[assembly: GoImplement<pointerSuffixError, error>(Promoted = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(Decoder))]
[assembly: GoFieldAccess(typeof(Encoder))]
[assembly: GoFieldAccess(typeof(decodeBuffer))]
[assembly: GoFieldAccess(typeof(decoderState))]
[assembly: GoFieldAccess(typeof(encoderState))]
[assembly: GoFieldAccess(typeof(go.github.com.go_json_experiment.json.@internal.jsonopts_package.Struct))]
[assembly: GoFieldAccess(typeof(go.github.com.go_json_experiment.json.@internal.jsonwire_package.InvalidTextError))]
[assembly: GoFieldAccess(typeof(pointerSuffixError))]
[assembly: GoFieldAccess(typeof(state))]
[assembly: GoFieldAccess(typeof(stateMachine))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/decode.go", "decode.cs", "ACf0AaKCgs6ylLS0toKClMaigoKCggAEFoLGtLiCgqiWgpSkggACFMqCqIKChIaKlIaCloKUgoKWgoKUgqSkpAAMFoaCgpSCgrqEpoKCgoKcgoSCmIKCgoK4AAQQgsSkgqaCgpaCgoKClIK6goKCgoKCgoKCuIKCgq6CqqyCgpSsgoKoooKUpLSClMaCgoLKgsaimIKCgpSC3IKAgqTMooKCgriqooKCpsyCxKSCgoKEgoKClIKUgpaCgoKClLqCgoKCgoKCgriCgrqClIKCgqaUgpSCpoKCgqaUgpSCpoKCgqaUgpSCpoKCgoKCgoKmlIKCgpSCgqaUgpSCqoKCgoKCgqaUgpSCpoKUgoKUgoKCpoKUgoKUgoKmgpSCgoKmgpSCgqaCAAYcgoLEpIKCgoSCgoKUgpSCloKCgoKUuoKCgoKCgoKCuIKCuoKCgoKClJS0goKCgpSCgqaUtLSClILGgpSCxoKUgoKsooKSgoKmgoKCgpSogoKoopSipIKkpAAEKqKCgoKCgoKClJS8ooKCgqSCxoLGgsaCyoLGpKSClKSCgoKCgpSUvIKCgoKCgoKClJS8ooKCgoKCgoKClJS8ooKCgoaCgoKCgoKClJSUvPKCgoKCgqiCgoKUloKCgqaCgqaChIKCgqaCgoKCgoKCgqaUgoKogoKCpoKClJaCgoKmgoKogoKCppSCtIKkAAkOtIKCgpSWgoKCpoKCpoKChIKCgqaCgqiCgoKmlIKCtIKkAAcSggACJIIAAjaEAAIehKSkpMqCxoKC", "264-267:1")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/encode.go", "encode.cs", "ACe2AaKCgs6ylLS0toKClMaigoKClIKCgoIABRiCxpaOqoKCqIKoloqCnoKmgqiCgoiClJSagoKEhoKCpsSCgriCmKimpNaqooKqloKCpLiClLS0xoKogoKCgoKCloKCgoKmgoKClKqCgqiCgoKWgoKCpoIABBqCxKKCloKClJaClIK0grSCtIKUgoKCgpSCgqaUtIKUtIKClIKClLSCgpSCgsaCgrSCtLSCqIKClAACGKKWgoKUlIKYgoKUmIKCgoKCgoK6goKClIKCppSCxoKUgsa4goKUAAQegsSilIKWgoKUloKCgoKUgoKCgqiUtIKCgoKUgoKmlLS0gpSClILGgpSClILGgqiCgpSsooKCgpSCgqaClIKCuKiCgoKmgpSCpqqCgpSCgoKUrKSClKSCgpSkgoKUpIKClKSAgoKkpICCgqSkpKTO1IKCgpSCloKClIKCgqaCgoKCgpSChIKogoKUgoKClIKCpoKClJaCgpSCgpSCgoKogoKUgoKUloKClJSCgpSCtIKUgoKkAAgQpIKCgpSCloKClIKCgqaCgoKEgqiCgpSCgoKUloKClJSCgpSCgrSClIKCpAAHEoIAAiSOgoKUAAI0hAACHoSkpKTKgsaCgg==")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/errors.go", "errors.cs", "ABo0gpSCAAwSgpSCABI+ioCCpIKCgoKUgIKCgpSCtILGpIiCnqbGooKCgoKCgoKCppSClIKUpoIACU6CgoKUgoKogoKClILIhoKCgpQ=")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/export.go", "export.cs", "ABZMgoKUAAkalpSilIKmopSCpqKUgqailIKmgoI=")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/options.go", "options.cs", "ABFsgoKUAAMUgoKUAAMSgoKUAAMSgoKUAAMagoKUAAMigoKUAAMigoKUAAMSgoKUAAMSgoKUAAMSgoKUAAMigoKUAAMehpSkpKSkpKiClAACFoKClA==")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/pools.go", "pools.cs", "AA8ujIgABhaigoiClIKUooIAAiKUtLS0goK0pqKCgoKUgoKmooKUgoKUgoKUvIyIlqKCgpSigqaigoKClIKCpqKCgpSCgpQ=")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/quote.go", "quote.cs", "AAwoooKClAACFKKCgpQ=")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/state.go", "state.cs", "ABNGnJaCgoQACzCCpoKCggAIKIKClKS2qoSCqoKqgoKoggACEIKCgoKCgpTqgoSClAAEKKKCgoKCgpSkxpSCtMbGgoSUtLTGAAksgoKClKqCqqKClKqCqoKUpKSCzIKUpILMgqqClKSkpIKCgsyClKSkpIKCzIKUpKSkgoKCzIKUpKSCggAEEIKClKSkpKTKgpSkpM6ClKSkAAQWgoKCggAVQoKogqiCqoKqgqqCrIKsgqqCqoKqgqiCqIIACzKCgoKClIK4gq6CgoKUAAkQgqyMrIKCgoKUgqqCqIKuhIKCqIKCgpSUrIKCqIKClIKUlLiCggAJEoKClKiigoKUuoKoggAMLoKCgoKClIK6gqiCgpS6gqyCgpSUgpSCgoKUlJiCgoKCgoK4hoKCgpSqgpSmgoKogoKUgoKUgg==", "143-152:1")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/token.go", "token.cs", "ABc8AAiiAZKSlJKSkpSChIKCloKogoKUrIKClAACHoKClAACEoKUpKSkpKiCgpSogoKUzoSEgpSkpKSkpKTKgpSClKqClKSk3IKEgoKClIKUhKaqioKClJSCgoKUgoSClpSCloKUpKSktqqChIKCgpSElKSkpMgAAi6CggACHoKCpoKEgpSCgoKClJSElKSCgpSkpMqCpKSkzAACHoKEgpSChKSkuKSkuIKClISUpIKUpIKClKSkpPrGgpSkpKQABiCOhIKUhKSkpoKClISUpIKUpIKClKSkpPrGgpSkpKTqgpSCgpSkpKQAFzaClKSkpKSkpKSkpKToABcwhA==")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/jsontext/value.go", "value.cs", "ABVAgoKUAAIS4oKCgoKUAAkigqiCgpQAAiTEgoKCggAFOKSqwoKCgoKClIKUAAUgogADLKIAA0aiAAMUhIKUqqSClIKqgoKUAA0cgoKY6qSCgoKUooKCvKaCloKCgqIAAiDCgKaCgoKUgoKCgoKCgpSCgpSClIKWgpSCggABFIaCgpiCuIK0xoKUloLGgpS0gvY=", "371-373:1", "", "108=AllowDuplicateNames/1/1/1,AllowInvalidUTF8/1/1/2,PreserveRawStrings/1/1/3;115=AllowDuplicateNames/1/1/1,AllowInvalidUTF8/1/1/2,PreserveRawStrings/1/1/3,Multiline/1/1/4;122=CanonicalizeRawInts/1/1/1,CanonicalizeRawFloats/1/1/2,ReorderRawObjects/1/1/3;165=TrimLeft/1/2/1,TrimLeft/2/2/2")]
// </GoSourcePositionMaps>

namespace go.github.com.go_json_experiment.json;

[GoPackage("jsontext", ImportPath = "github.com/go-json-experiment/json/jsontext")]
public static partial class jsontext_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface wrapSyntacticError_state {}
    internal partial struct bufferStatistics {}
    internal partial struct decodeBuffer {}
    internal partial struct encodeBuffer {}
    internal partial struct ioError {}
    internal partial struct nonComparable {}
    internal partial struct numError {}
    internal partial struct objectMember {}
    internal partial struct pointerSuffixError {}
    internal partial struct requireKeyedLiterals {}
    internal partial struct state {}
    public partial struct Decoder {}
    public partial struct Encoder {}
    public partial struct Kind {}
    public partial struct Pointer {}
    public partial struct SyntacticError {}
    public partial struct Token {}
    public partial struct Value {}
    public partial struct decoderState {}
    public partial struct encoderState {}
    public partial struct export {}
    public partial struct exporter {}
    public partial struct objectNameStack {}
    public partial struct objectNamespace {}
    public partial struct objectNamespaceStack {}
    public partial struct stateEntry {}
    public partial struct stateMachine {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸbytes() => builtin.initPackage(typeof(bytes_package));
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternal() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.internal_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternalꓸjsonflags() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.@internal.jsonflags_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternalꓸjsonopts() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.@internal.jsonopts_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternalꓸjsonwire() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.@internal.jsonwire_package));
    [GoInit] internal static void initᴛᴛimportꓸio() => builtin.initPackage(typeof(io_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
    [GoInit] internal static void initᴛᴛimportꓸmath() => builtin.initPackage(typeof(math_package));
    [GoInit] internal static void initᴛᴛimportꓸmathꓸbits() => builtin.initPackage(typeof(go.math.bits_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸunicodeꓸutf8() => builtin.initPackage(typeof(unicode.utf8_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(jsontext_package.Decoder)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛDecoder;
[GoMethodHost(typeof(jsontext_package.Encoder)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛEncoder;
[GoMethodHost(typeof(jsontext_package.Kind)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛKind;
[GoMethodHost(typeof(jsontext_package.Pointer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛPointer;
[GoMethodHost(typeof(jsontext_package.SyntacticError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛSyntacticError;
[GoMethodHost(typeof(jsontext_package.Token)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛToken;
[GoMethodHost(typeof(jsontext_package.Value)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛValue;
[GoMethodHost(typeof(jsontext_package.decodeBuffer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛdecodeBuffer;
[GoMethodHost(typeof(jsontext_package.decoderState)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛdecoderState;
[GoMethodHost(typeof(jsontext_package.encodeBuffer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛencodeBuffer;
[GoMethodHost(typeof(jsontext_package.encoderState)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛencoderState;
[GoMethodHost(typeof(jsontext_package.export)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛexport;
[GoMethodHost(typeof(jsontext_package.exporter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛexporter;
[GoMethodHost(typeof(jsontext_package.ioError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛioError;
[GoMethodHost(typeof(jsontext_package.numError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛnumError;
[GoMethodHost(typeof(jsontext_package.objectMember)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛobjectMember;
[GoMethodHost(typeof(jsontext_package.objectNameStack)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛobjectNameStack;
[GoMethodHost(typeof(jsontext_package.objectNamespace)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛobjectNamespace;
[GoMethodHost(typeof(jsontext_package.objectNamespaceStack)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛobjectNamespaceStack;
[GoMethodHost(typeof(jsontext_package.pointerSuffixError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛpointerSuffixError;
[GoMethodHost(typeof(jsontext_package.state)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛstate;
[GoMethodHost(typeof(jsontext_package.stateEntry)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛstateEntry;
[GoMethodHost(typeof(jsontext_package.stateMachine)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsontextᴛstateMachine;
// </MethodHolders>
