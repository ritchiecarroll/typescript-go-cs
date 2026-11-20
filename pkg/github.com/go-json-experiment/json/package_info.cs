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
using static go.github.com.go_json_experiment.json_package;

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
[assembly: GoDynamicTypeLift("696e746572666163657b417070656e64537461636b506f696e746572285b5d627974652c20696e7429205b5d627974657d", "newSemanticErrorWithPosition_coderState")]
[assembly: GoDynamicTypeLift("7374727563747b656e636f64696e672e54657874417070656e6465727d", "makeInterfaceArshaler_iᴛ2")]
[assembly: GoDynamicTypeLift("7374727563747b656e636f64696e672e546578744d61727368616c65727d", "makeInterfaceArshaler_iᴛ3")]
[assembly: GoDynamicTypeLift("7374727563747b6769746875622e636f6d2f676f2d6a736f6e2d6578706572696d656e742f6a736f6e2e4d61727368616c6572546f7d", "makeInterfaceArshaler_i")]
[assembly: GoDynamicTypeLift("7374727563747b6769746875622e636f6d2f676f2d6a736f6e2d6578706572696d656e742f6a736f6e2e4d61727368616c65727d", "makeInterfaceArshaler_iᴛ1")]
[assembly: GoTypeAlias("Marshalers", "go.github.com.go_json_experiment.json_package.typedArshalers<go.github.com.go_json_experiment.json.jsontext_package.Encoder>")]
[assembly: GoTypeAlias("Options", "go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options")]
[assembly: GoTypeAlias("Unmarshalers", "go.github.com.go_json_experiment.json_package.typedArshalers<go.github.com.go_json_experiment.json.jsontext_package.Decoder>")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<SemanticError, error>(Pointer = true)]
[assembly: GoImplement<go.github.com.go_json_experiment.json.@internal.jsonflags_package.Bools, go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options>]
[assembly: GoImplement<go.github.com.go_json_experiment.json.jsontext_package.Decoder, coder>(Pointer = true)]
[assembly: GoImplement<go.github.com.go_json_experiment.json.jsontext_package.Encoder, coder>(Pointer = true)]
[assembly: GoImplement<go.github.com.go_json_experiment.json.jsontext_package.decoderState, newSemanticErrorWithPosition_coderState>(Pointer = true)]
[assembly: GoImplement<go.github.com.go_json_experiment.json.jsontext_package.encoderState, newSemanticErrorWithPosition_coderState>(Pointer = true)]
[assembly: GoImplement<makeInterfaceArshaler_i, MarshalerTo>(Promoted = true)]
[assembly: GoImplement<makeInterfaceArshaler_iᴛ1, Marshaler>(Promoted = true)]
[assembly: GoImplement<makeInterfaceArshaler_iᴛ2, encoding_package.TextAppender>(Promoted = true)]
[assembly: GoImplement<makeInterfaceArshaler_iᴛ3, encoding_package.TextMarshaler>(Promoted = true)]
[assembly: GoImplement<marshalersOption, go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options>(Pointer = true)]
[assembly: GoImplement<supportFormatTag, Options>(Promoted = true)]
[assembly: GoImplement<supportFormatTag, go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options>(Pointer = true)]
[assembly: GoImplement<unmarshalersOption, go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(Marshalers))]
[assembly: GoFieldAccess(typeof(SemanticError))]
[assembly: GoFieldAccess(typeof(Unmarshalers))]
[assembly: GoFieldAccess(typeof(arshaler))]
[assembly: GoFieldAccess(typeof(go.github.com.go_json_experiment.json.@internal.jsonopts_package.Struct))]
[assembly: GoFieldAccess(typeof(go.github.com.go_json_experiment.json.jsontext_package.SyntacticError))]
[assembly: GoFieldAccess(typeof(go.github.com.go_json_experiment.json.jsontext_package.decoderState))]
[assembly: GoFieldAccess(typeof(go.github.com.go_json_experiment.json.jsontext_package.encoderState))]
[assembly: GoFieldAccess(typeof(go.github.com.go_json_experiment.json.jsontext_package.stateMachine))]
[assembly: GoFieldAccess(typeof(structField))]
[assembly: GoFieldAccess(typeof(typedArshalers<>))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/arshal.go", "arshal.cs", "ACBgAAAQipgAAcQB4oKCgoKCgoKU7tKCgoKCgoKClAAFEtKCgoKCoJKCgoKUgqaCgpSCuIKClNaigoKYgoKCgpSCloKClIKClJQAAogC0oKCgoKCgpQABRLSgoKCgoKClAAFGtKCgoKCoJKCgoKUgriCgpTWsoKClIKYgoKCgpS6goKUgoKUgoKUlIKUAAcmggALMIKCpoKCloKmuoKCgpSCpqKClII=", "204-204:1;434-434:1", "", "", "48:27,70:27,163:27,183:27", "95:25,96:17,207:25,208:17")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/arshal_any.go", "arshal_any.cs", "ABZAopSkpKSClKSkpoKCgoKUroKktLSCgoKClJSkpKSCgpSkgpSCgpSkAAgOxIKCgoKUqIKCloKCgoKUuICCqIKUgoKAgqSAgsiCgoKClIKCgIKkgIK2lICCpNqigKSkxIaClIKCgoKUloSCgqaCgoKClKaAgqSqxIKCgoKUqIKCloKCgoKUuICCpIKCpoCCpNqCgKSkxIKCgoKCgoKUpoCCpA==", "", "", "", "124:31,256:31")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/arshal_default.go", "arshal_default.cs", "ACVOgoKCgoKEggAJHoKCgpSClIKUgoKmgoKUpqKmgpSkpKSkpKSkgoKUpIKClKSkpMiCgoKCgoKCgpSCuoKCgoKUpoKClKaUgoKCgoKUgqaCgpSClIKUpIKCxoKktLSCgpSUpMaUpoKCgoKCgoKClIK6goKCgoKCgoKClLyCgoKUgoKUlJSCgoKCgoKUgqaCgoKUgpSClKSCgoKClIKClKaClIKCpJSogoKCgoKCgoKCgoKCgoKWroKCgpKClIKCgpSkpLS0tLS0gqTWpKSEpqiCkoKUgoKClKSktLS0tLSCpNaktoKCgpSClIKUAAISgoKClIqCgqaCgoKCgqaClJSklKaCgoKCgoKCqIKCgoKUpoKogoKCgpSCgoKUgpSClLSClIKIgoKCgpSUlIKUxIKUgoKClIKCgoKClJSClIKUlLSUpoKCgoKCgoKogoKCgpSmgqiCgoKClIKCgpSClIKUtIKUgoiCgoKClJSUgpTEgpSCgoKCgpSUgpSCtJSmgoKCgoKCgoKClLiCgoKClKiCgoKClKaCqIKCgoKCgpS4goKClIKUgpS0goKkgqSCpIK2gpSIgoKCgpSUlIKUgIKkxIKUgoKClLSUAAgGAAAQgqqCgpSEtIKCgpSmgoKClIKktLTcgoKCloKCgoKUuIKAgqSCgoKCgoKCgpSCmoKmlIKCgoKElIKClKaCgtiCgoKUgoKAgqaCgoCCtrSMgoKEgoKCgoKChJSCgpSmgpqGgoCCpICC+oCCpMSSgoKClILWyIKClIKUgqSCgqaCgoKCgoKClIIAARCCroKCpoKEgoKCgpSClIKUgoKClIKUgqiChIKUgpSmqIKCgpSCgpSmgpSklK6CmKioyIKUioKqgpSSgoKClIKmgoKCgqaAgqSCgoKCgoKCgoK8hKiCpoKCgoKCrIQAAhqEgoKCgqaCqIKClJSCgpSCuoKClIKClIKCgoKogoKClIK+gpSUgoKCloKAgqSCgs6mgqaAgqSUkoKCgpSCpoKClIKUgpSkgoKCgqaCgoKEgoKClIKCgoKCgoKUpoKUgoKCgpSUhKaEgqiCgpSmpoSogoKUgoKUgoKUgoKCgoKClIKqgoKCgoKUpoKUpJTGgoKCgpSUpoKCgoKUlJTIhJSkpKSkpKSmgoKYgpS0goKClKaCgoKUgqS0tNyCgoKWgoKCgpS4goCCpIKClIKCgqaAgqTEgqKCgpSC1tqCgpSClIKkgoKClIKCgpSCgoKCgoKClIKCgpSCgoKUpoKUlIKUpJSmgpSCgpiClIKSgoKUgqaCgIKkgoKUgoKCpoCCpJSSgoKUgqaCgpSClIKUpIKCgpSCgoKCgpSClIKClIKClJSUgoKUgpSClKSUpoKCmIKUtIKCgpSogoKUlIKUgoKClILElIKClIKUgoKUlIKCgpSClIKAgqSOgqaUABIGioKCgoKCppKCgoKUgqaCpoKCiIKClLS0tLSmgoKCgpaGlJSCgoKClIKmgqaMgoKCgpSCpqaCgpSClIKMhoaClKaCgpSUtLSClMa0vIK8gpSCgpSCgpSoiqaCgoaIgqSkpLaUpoKClPyCAAoUgoKUgpK6hIKCgpSCkoKClIKC", "128-159:1;160-207:2;213-256:1;257-303:2;335-376:1;373-375:1.1;378-463:2;470-491:1;488-490:1.1;492-562:2;569-590:1;587-589:1.1;591-652:2;659-694:1;691-693:1.1;695-770:2;788-791:1;794-954:2;937-939:2.1;955-1077:3;1116-1118:1;1119-1284:2;1257-1274:2.1;1285-1417:3;1396-1398:3.1;1471-1473:1;1474-1536:2;1538-1605:3;1618-1620:1;1622-1649:2;1650-1711:3;1721-1723:1;1724-1751:2;1752-1791:3;1808-1856:1;1857-1957:2;1972-1974:1;1975-1990:2", "", "838=Key/2/2/1,implementsAny/1/1/1;918=Is/1/1/1,IsNil/1/1/1;970=Is/1/1/1,IsNil/1/1/1;1190=IsZero/1/1/1,Invoke/1/1/1;1206=Invoke/1/1/1;1907=Get/1/1/1;1949=Get/1/1/1", "848:35,1498:35,1773:35")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/arshal_embedded.go", "arshal_embedded.cs", "ABRGlJbSgoKCgqaCgqaCgoKmgoKClIKCgpSUgpSEgoKClIKCgqaCqIKClIKmgpSClJSCgoKUgoKSgoKUgoKCgqaUgoKUgoKCgIKkgoCCyIKCgpSCgoKAgqaCgIK2lOqigoKUlIKCgpSChoKCgqamgoKCgpSCgpSUgoKUgoKUgoKmgoKUgoKClA==", "115-128:1", "", "", "50:31")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/arshal_funcs.go", "arshal_funcs.cs", "AB4sggABKqIAAiyiAA88oKKgoqKCgoKCpoKUxqKClIKClKqCgoKUgoK4goKogqKCgqaoggAEGIKCgraCgoKCgpSClIKClIKUlKYAAhqCgoK2goKCgoKCgoKUgoKClJSClIKUlLgAAhaCgoK2goKUgoKCgoKUgpSmAAIYgoKCtoKCgpSCgoKCgoKUgoKClJSCgpSUgpSUuAACIoKUqILMgsaClAADEIKcqKgABhKClKSk7IKCgqY=", "145-152:1;173-194:1;214-241:1;260-276:1;295-328:1")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/arshal_methods.go", "arshal_methods.cs", "ABUsmIKCgoKCgoSCggARiAKogqaAgoKCkoSUgoCSgpSCgpSClKTIgIKCgpKElIKAgoKClIKUpMiAgoKCkoSUgoKCgoKUgpSCgpSClJTIgIKCgpKElIKCgoKCgoKClIKCgpSUgpSClJTIgIKCgoKCgoKUgoKUlIKUgoKCgoKUgpSUyICCgoKShJSCgpSCgoKClIKUyICCgoKShJSCgoKUgoKCgoKClIKCgpSUgoKUlIKUlMiqooKCpq6ClKSk", "179-199:1;185-188:1.1;205-222:2;228-253:3;259-290:4;295-324:5;330-349:6;355-392:7", "", "122=Encoder/1/1/1,NeedObjectName/1/1/1;155=Encoder/1/1/1,NeedObjectName/1/1/1;230=Decoder/1/1/1,NeedObjectName/1/1/1;257=Decoder/1/1/1,NeedObjectName/1/1/1")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/arshal_time.go", "arshal_time.cs", "ABw6gpYAAhKUgoKSgoKCgpSCloKClJSCpoKCgIKClKSUgpKCgoKClIKWgoKUlIKmgoKCgoKUpIKUpIKUgoKUgqSClIKUgqTGgoKCgoKCpoKmgoKAgoKUgpSklJKCgoKClIKUgqaCgoKCgpSkgpSkgpSCgoKUlIKkgpSCgpSUgqTGAAkcgpS0tLS0tLSkpoKmgpSkpMiilLS0tAANIoaClIKCgpSUtLS0tLS0tLSCtIK0tLS0tLS0tLS0tLS2gpS0poKmgqaClIKCiKSkgoLGpKTIopaCnoaUpKSklKTWpIKkguyCgoKCqoKCgoKCgoKkpKQABBCCgpSCgoKCgoKUgpSClAAEEgABWoKytoKCgqaskoKCqoKCgoKCgpSCgpSClIKUgoKCgoKCpoKCgoKCgoKCgpSCgoKClIKUlKSkpMqCgoKUqIKClLyCgoKClJSCpIKkgoLMgoKCgoKSlIK0grSCgoKCtIKUpKSkyoKCgoKCqoKClKqClKSkqoKGgoKClK6igoKCgoKUgpSUgpSogoKCgoKmqoKCgpSUqoKClA==", "46-76:1;78-133:2;136-160:3;161-215:4;406-408:1;543-550:1;555-588:2")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/errors.go", "errors.cs", "ABhQmLiCgqaCgq6CABNkgoKUtKSqogAEFqKCgpQABRKCgoKCpqqigoKEgpSmAAUSgoKClKyCgoKCpgAKIoKCgpSCgoKCgpSCgoKCtIKCgoKChpSkgoKClLS0lIIAAlaCgoKCgqamgoKUAAISgoKUxoKCgpaCpIK0grSCuJS0tLS0tLSCxoKCqIKCjIKCgoKCyoKCqIKCgoKCgoKUqqSCgsaCgsqCgoKUgqamgsaCgoKUAAYQgoKU", "", "", "42=isSyntacticError/1/1/1,IsIOError/1/1/1;79=OutputOffset/1/1/1,Encoder/1/2/1,CountNextDelimWhitespace/1/1/1,Encoder/2/2/2,AppendStackPointer/1/1/2;91=InputOffset/1/1/1,Decoder/1/2/1,CountNextDelimWhitespace/1/1/1,Decoder/2/2/2,AppendStackPointer/1/1/2;119=Decoder/1/1/2,AppendStackPointer/1/1/2,Kind/1/1/3")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/fields.go", "fields.cs", "AB06AAsggoKCgoKmgpSCvIIADxz0spKogoyYkoKClIKCgoKCgoKCgoKClIKUggAEEIKClLiS2IKCgoKUqoKCqoKClIKCgoKqlLSCgoKUtIKCqIKYlJTOgoKCgpiEgrqUvLq0upS0uIKYlIKCgoK4gpQAAxqCggADFoK8goKClIKUrIaCqpzugoKClIKGhqaAgqSCrIKClAAEIIKCgqYAFDCyloIAAhiEgpSogoSGmoKCgoKmgoKUgoKUlIKCloKChIKUgoKCuoKClIKClLS0lIKClIKCgoKUgoKClJS0tOa0tLS0goKUgoKCgoKCgpSCgriClAAGEpS0tJSupIKCpqaCpoIAAhSCgoKCgpSClLS0tIKCgoKUpIKUgpSkpIKUyIKogpSkpA==", "46-52:1;81-83:1;136-196:2;197-261:3;222-226:3.1;228-231:3.2;233-233:3.3;235-235:3.4;242-242:3.5;244-244:3.6;293-298:4;313-315:5;322-324:6;344-346:7;439-441:1", "", "203=implementsAny/2/2/1")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/fold.go", "fold.cs", "AAwihoKUgoSCgoKUlIKWgoKUAAISgoKCgpQ=")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/intern.go", "intern.cs", "AA8oopiCqoKUgoK0goK0goK4goKUgoKojM6CgoKCiIKCgoKCgpQ=")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/options.go", "options.cs", "ABOaAaKCggACHoKsggACPIKClAADJIKClAADEoKClAADEIKClAADGIKClAADMoKClAADEIKClAADEoIAAhCCAAgUsraCgpSClKSClKTGgpSCtILEpA==", "275-290:1;291-303:2")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/options_format.go", "options_format.cs", "AA8oksqCAASyAYKClA==")]
[assembly: GoPositionMap("github.com/go-json-experiment/json@v0.0.0-20260820222146-c27c302e5fc3/options_format_global.go", "options_format_global.cs", "AA4ogqaCgoKU")]
// </GoSourcePositionMaps>

namespace go.github.com.go_json_experiment;

[GoPackage("json", ImportPath = "github.com/go-json-experiment/json")]
public static partial class json_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface coder {}
    internal partial interface newSemanticErrorWithPosition_coderState {}
    internal partial struct addressableValue {}
    internal partial struct arshaler {}
    internal partial struct durationArshaler {}
    internal partial struct fieldOptions {}
    internal partial struct makeInterfaceArshaler_i {}
    internal partial struct makeInterfaceArshaler_iᴛ1 {}
    internal partial struct makeInterfaceArshaler_iᴛ2 {}
    internal partial struct makeInterfaceArshaler_iᴛ3 {}
    [GoLocalName("member")] internal partial struct makeMapArshaler_member {}
    [GoLocalName("queueEntry")] internal partial struct makeStructFields_queueEntry {}
    internal partial struct marshalersOption {}
    internal partial struct nonComparable {}
    internal partial struct requireKeyedLiterals {}
    internal partial struct stringSlice {}
    internal partial struct structField {}
    internal partial struct structFields {}
    internal partial struct supportFormatTag {}
    internal partial struct timeArshaler {}
    internal partial struct typedArshaler<Coder> {}
    internal partial struct typedPointer {}
    internal partial struct uintSet {}
    internal partial struct uintSet64 {}
    internal partial struct unmarshalersOption {}
    public partial interface Marshaler {}
    public partial interface MarshalerTo {}
    public partial interface Unmarshaler {}
    public partial interface UnmarshalerFrom {}
    public partial interface isZeroer {}
    public partial struct SemanticError {}
    public partial struct typedArshalers<Coder> {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸbytes() => builtin.initPackage(typeof(bytes_package));
    [GoInit] internal static void initᴛᴛimportꓸencodingꓸbase32() => builtin.initPackage(typeof(go.encoding.base32_package));
    [GoInit] internal static void initᴛᴛimportꓸencodingꓸbase64() => builtin.initPackage(typeof(go.encoding.base64_package));
    [GoInit] internal static void initᴛᴛimportꓸencodingꓸbinary() => builtin.initPackage(typeof(go.encoding.binary_package));
    [GoInit] internal static void initᴛᴛimportꓸencodingꓸhex() => builtin.initPackage(typeof(go.encoding.hex_package));
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternal() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.internal_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternalꓸjsonflags() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.@internal.jsonflags_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternalꓸjsonopts() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.@internal.jsonopts_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸinternalꓸjsonwire() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.@internal.jsonwire_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸgo_json_experimentꓸjsonꓸjsontext() => builtin.initPackage(typeof(go.github.com.go_json_experiment.json.jsontext_package));
    [GoInit] internal static void initᴛᴛimportꓸio() => builtin.initPackage(typeof(io_package));
    [GoInit] internal static void initᴛᴛimportꓸmath() => builtin.initPackage(typeof(math_package));
    [GoInit] internal static void initᴛᴛimportꓸmathꓸbits() => builtin.initPackage(typeof(go.math.bits_package));
    [GoInit] internal static void initᴛᴛimportꓸreflect() => builtin.initPackage(typeof(reflect_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸsyncꓸatomic() => builtin.initPackage(typeof(go.sync.atomic_package));
    [GoInit] internal static void initᴛᴛimportꓸtime() => builtin.initPackage(typeof(time_package));
    [GoInit] internal static void initᴛᴛimportꓸunicode() => builtin.initPackage(typeof(unicode_package));
    [GoInit] internal static void initᴛᴛimportꓸunicodeꓸutf8() => builtin.initPackage(typeof(go.unicode.utf8_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(json_package.SemanticError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛSemanticError;
[GoMethodHost(typeof(json_package.addressableValue)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛaddressableValue;
[GoMethodHost(typeof(json_package.durationArshaler)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛdurationArshaler;
[GoMethodHost(typeof(json_package.marshalersOption)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛmarshalersOption;
[GoMethodHost(typeof(json_package.structField)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛstructField;
[GoMethodHost(typeof(json_package.structFields)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛstructFields;
[GoMethodHost(typeof(json_package.supportFormatTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛsupportFormatTag;
[GoMethodHost(typeof(json_package.timeArshaler)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛtimeArshaler;
[GoMethodHost(typeof(json_package.typedArshalers<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛtypedArshalers;
[GoMethodHost(typeof(json_package.uintSet)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛuintSet;
[GoMethodHost(typeof(json_package.uintSet64)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛuintSet64;
[GoMethodHost(typeof(json_package.unmarshalersOption)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsonᴛunmarshalersOption;
// </MethodHolders>
