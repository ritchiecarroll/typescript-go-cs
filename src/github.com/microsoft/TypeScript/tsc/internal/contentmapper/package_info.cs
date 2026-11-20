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
global using jsonꓸMarshalerTo = go.github.com.go_json_experiment.json_package.MarshalerTo;
global using jsonꓸUnmarshalerFrom = go.github.com.go_json_experiment.json_package.UnmarshalerFrom;
global using jsonꓸValue = go.github.com.go_json_experiment.json.jsontext_package.Value;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;

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
[assembly: GoImplement<DiagnosticDirectiveError, error>(Pointer = true)]
[assembly: GoImplement<InitializeError, error>(Pointer = true)]
[assembly: GoImplement<InvalidVirtualExtensionError, error>(Pointer = true)]
[assembly: GoImplement<MappedDiagnosticDirective, go.github.com.go_json_experiment.json_package.MarshalerTo>]
[assembly: GoImplement<MappedDiagnosticDirective, go.github.com.go_json_experiment.json_package.UnmarshalerFrom>(Pointer = true)]
[assembly: GoImplement<ProjectError, error>(Pointer = true)]
[assembly: GoImplement<SupplementalFileCollisionError, error>(Pointer = true)]
[assembly: GoImplement<TransformError, error>(Pointer = true)]
[assembly: GoImplement<closeOnceReadWriteCloser, io_package.ReadWriteCloser>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package.MappingError, error>(Pointer = true)]
[assembly: GoImplement<host, Host>(Pointer = true)]
[assembly: GoImplement<io_package.ReadWriteCloser, io_package.ReadWriter>]
[assembly: GoImplement<loggedProcess, io_package.ReadWriteCloser>(Pointer = true)]
[assembly: GoImplement<loggingProtocol, go.github.com.microsoft.TypeScript.tsc.@internal.ipc_package.Protocol>(Pointer = true)]
[assembly: GoImplement<projectLease, Project>(Pointer = true)]
[assembly: GoImplement<rejectHandler, go.github.com.microsoft.TypeScript.tsc.@internal.ipc_package.Handler>]
[assembly: GoImplement<retainedProject, Project>(Pointer = true)]
[assembly: GoImplement<stderrLogger, io_package.Writer>(Pointer = true)]
[assembly: GoImplement<strings_package.Builder, io_package.Writer>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(DiagnosticDirectiveError))]
[assembly: GoFieldAccess(typeof(InitializeError))]
[assembly: GoFieldAccess(typeof(MappedDiagnosticDirective))]
[assembly: GoFieldAccess(typeof(Mapper))]
[assembly: GoFieldAccess(typeof(closeOnceReadWriteCloser))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package.Segment))]
[assembly: GoFieldAccess(typeof(host))]
[assembly: GoFieldAccess(typeof(mapperConn))]
[assembly: GoFieldAccess(typeof(mapperTimingCollector))]
[assembly: GoFieldAccess(typeof(operationTiming))]
[assembly: GoFieldAccess(typeof(positionNormalizer))]
[assembly: GoFieldAccess(typeof(projectEntry))]
[assembly: GoFieldAccess(typeof(projectLease))]
[assembly: GoFieldAccess(typeof(retainedProject))]
[assembly: GoFieldAccess(typeof(timingCollector))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/contentmapper/contentmapper.go", "contentmapper.cs", "ABI0ABZAqILIgpSkpMyCgpSmgpSkpAAEFKKCgoKCgoKCgqyCgoKUgoKCgoKUgoKUgoKUlMiCgoKCgoKm")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/contentmapper/host.go", "host.cs", "ABxAgsaCpgAUKoIAChCCABImgpSkpKSkpAAiRoLGgpSkpKSkpKSkpABDqAGCuIKCAAcQxoI=", "", "", "253=operationTimingSince/1/5/1,operationTimingSince/2/5/2,operationTimingSince/3/5/3,operationTimingSince/4/5/4,operationTimingSince/5/5/5")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/contentmapper/hostimpl.go", "hostimpl.cs", "AHu4AoK2goKClKaigoCCpIKUgoKCgqaCgoKCpgBAlgGCgqaCABco0oKCgpSCgtaigoKClIKCgoKCAAcQxoKCgpSCgqaCgoKCgpTGAA0gggASIoKCgoKUpoKCgpSmgoKCpoKCgqaCgoKmgoKCAA0U0oKCgoKCgpSClNbSgoKCgrSkggATEIKCggAUEqKAkqaCgpTOgqiCgoKigpSCgoKCgoKClJKCgpSClIKCgpSCkoKUgoKCgoKCgoKCgpSCgoKUgpSClJS4goKCkJLGgqbigoKClJSCgoKClIKCgoKUgpSCkrSk4oKUgoKCgpSClIKCgoKCgoKCgoKUgpSC9oKCgoKUprKCgoKCgoKCgoKCxqKClIKClIKClIKC3IKClIKClIKUgpSClIKCgqaCgoKCgpSCgpS0goKUgrS2AAcQgqaigoKCgqaigoKCgoKCgpSCgoKCgpSmgpC4oriS1rKClIKClIKCyoKClIKClKrygoKCgoKCgoKmgoKCgoKSgqba0oKC1qKClIKCgoKUgpSCgoKCgoIAFCKigsaigJLGwoKCgoKClIKCgoKUgpSU1sKCgoKCgpSCgoKCgpSCgpSUgqbW0oKCgoKClIKClIKClIKClJSC1sKCgoKCgpSCgoKClIKCppSC1sKCgoKCgpSCgoKClIKUlNbCgoKCgoKClIKCgpSUgoLWgoKClKbSgoKCgoKCgoKClIKClIKUgoKClIKCpoKUgpT2soKCgoKCgpSCgoKCyoKS2IK4gpSCgpSClIKUgpSChpSmgoKClIKClNyCgoKClJSUgoKUgoKUgoKUAAcSpoKClLiCgpSCgpqCgoKUgoKmlIKClPbCgpSCgrLegpS0goKCgpSClIKCgrSCgqSClIKClIKClIKCgoKCgpSmgpSUioKUhoKCpqaygoKCgoKClIKClIKClIKCpgAJFIKClLSCtKTGgoKmgoKUgpSCpLS0gpQACQ6CpoI=", "472-472:1;495-552:1;522-525:1.1;558-558:1;757-757:1;873-873:1;1023-1025:1;1113-1115:1;1215-1221:1;1280-1282:2", "", "290=snapshot/1/5/1,snapshot/2/5/2,snapshot/3/5/3,snapshot/4/5/4,snapshot/5/5/5;1265=String/1/1/1;1329=NewTextRange/1/1/2", "843:21")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/contentmapper/transform.go", "transform.cs", "ABI0jIKClLiClKiigpSAgqSCgpSCgoKClIKGgpSUgoKClIKUgoKUgoKCgqaUlIIACRSCggAKFqaCqIKCgqY=", "", "", "98=FileName/1/1/4")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("contentmapper", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/contentmapper")]
public static partial class contentmapper_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface processExitState {}
    internal partial struct closeOnceReadWriteCloser {}
    internal partial struct host {}
    internal partial struct loggedProcess {}
    internal partial struct loggingProtocol {}
    internal partial struct mapperConn {}
    internal partial struct mapperTimingCollector {}
    [GoLocalName("indexedDirective")] internal partial struct normalizeDiagnosticDirectives_indexedDirective {}
    internal partial struct operationTiming {}
    internal partial struct positionNormalizer {}
    internal partial struct projectEntry {}
    internal partial struct projectLease {}
    internal partial struct rejectHandler {}
    internal partial struct retainedProject {}
    internal partial struct stderrLogger {}
    internal partial struct timingCollector {}
    public partial interface Host {}
    public partial interface Project {}
    public partial interface Spawner {}
    public partial struct CloseProjectParams {}
    public partial struct Definition {}
    public partial struct Diagnostic {}
    public partial struct DiagnosticDirectiveError {}
    public partial struct DiagnosticDirectiveErrorKind {}
    public partial struct DiagnosticDirectivePolicy {}
    public partial struct DiagnosticDirectives {}
    public partial struct HostOptions {}
    public partial struct InitializeError {}
    public partial struct InitializeErrorKind {}
    public partial struct InitializeParams {}
    public partial struct InitializeResult {}
    public partial struct InvalidVirtualExtensionError {}
    public partial struct Manifest {}
    public partial struct MappedDiagnosticDirective {}
    public partial struct MappedOutput {}
    public partial struct MappedResult {}
    public partial struct Mapper {}
    public partial struct MapperTimings {}
    public partial struct OpenProjectParams {}
    public partial struct OpenProjectResult {}
    public partial struct OperationTiming {}
    public partial struct OptionDiagnostic {}
    public partial struct OptionDiagnosticResult {}
    public partial struct OptionPathSegment {}
    public partial struct PositionEncoding {}
    public partial struct ProjectError {}
    public partial struct ProjectErrorKind {}
    public partial struct ProjectSpec {}
    public partial struct Request {}
    public partial struct Result {}
    public partial struct SourceFiles {}
    public partial struct SupplementalFileCollisionError {}
    public partial struct SupplementalOutput {}
    public partial struct Timings {}
    public partial struct TransformError {}
    public partial struct TransformErrorKind {}
    public partial struct TransformParams {}
    public partial struct TransformResult {}
    public partial struct UnusedExpectDirectiveDiagnostic {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸcontext() => builtin.initPackage(typeof(context_package));
    [GoInit] internal static void initᴛᴛimportꓸencodingꓸhex() => builtin.initPackage(typeof(encoding.hex_package));
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸipc() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ipc_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjsonrpc() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸlocale() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.locale_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸparser() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.parser_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸspanmap() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸzeeboꓸxxh3() => builtin.initPackage(typeof(go.github.com.zeebo.xxh3_package));
    [GoInit] internal static void initᴛᴛimportꓸio() => builtin.initPackage(typeof(io_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸreflect() => builtin.initPackage(typeof(reflect_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸsyncꓸatomic() => builtin.initPackage(typeof(go.sync.atomic_package));
    [GoInit] internal static void initᴛᴛimportꓸtime() => builtin.initPackage(typeof(time_package));
    [GoInit] internal static void initᴛᴛimportꓸunicodeꓸutf8() => builtin.initPackage(typeof(unicode.utf8_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(contentmapper_package.DiagnosticDirectiveError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛDiagnosticDirectiveError;
[GoMethodHost(typeof(contentmapper_package.InitializeError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛInitializeError;
[GoMethodHost(typeof(contentmapper_package.InvalidVirtualExtensionError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛInvalidVirtualExtensionError;
[GoMethodHost(typeof(contentmapper_package.MappedDiagnosticDirective)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛMappedDiagnosticDirective;
[GoMethodHost(typeof(contentmapper_package.Mapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛMapper;
[GoMethodHost(typeof(contentmapper_package.ProjectError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛProjectError;
[GoMethodHost(typeof(contentmapper_package.SpawnerFunc)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛSpawnerFunc;
[GoMethodHost(typeof(contentmapper_package.SupplementalFileCollisionError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛSupplementalFileCollisionError;
[GoMethodHost(typeof(contentmapper_package.Timings)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛTimings;
[GoMethodHost(typeof(contentmapper_package.TransformError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛTransformError;
[GoMethodHost(typeof(contentmapper_package.closeOnceReadWriteCloser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛcloseOnceReadWriteCloser;
[GoMethodHost(typeof(contentmapper_package.host)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛhost;
[GoMethodHost(typeof(contentmapper_package.loggedProcess)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛloggedProcess;
[GoMethodHost(typeof(contentmapper_package.loggingProtocol)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛloggingProtocol;
[GoMethodHost(typeof(contentmapper_package.mapperTimingCollector)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛmapperTimingCollector;
[GoMethodHost(typeof(contentmapper_package.operationTiming)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛoperationTiming;
[GoMethodHost(typeof(contentmapper_package.positionNormalizer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛpositionNormalizer;
[GoMethodHost(typeof(contentmapper_package.projectLease)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛprojectLease;
[GoMethodHost(typeof(contentmapper_package.rejectHandler)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛrejectHandler;
[GoMethodHost(typeof(contentmapper_package.retainedProject)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛretainedProject;
[GoMethodHost(typeof(contentmapper_package.stderrLogger)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛstderrLogger;
[GoMethodHost(typeof(contentmapper_package.timingCollector)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class contentmapperᴛtimingCollector;
// </MethodHolders>
