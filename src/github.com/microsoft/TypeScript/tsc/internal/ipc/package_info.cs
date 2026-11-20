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
using static go.github.com.microsoft.TypeScript.tsc.@internal.ipc_package;

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
[assembly: GoTypeAlias("Message", "go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package.Message")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<AsyncConn, Conn>(Pointer = true)]
[assembly: GoImplement<JSONRPCProtocol, Protocol>(Pointer = true)]
[assembly: GoImplement<PipeTransport, Transport>(Pointer = true)]
[assembly: GoImplement<StdioTransport, Transport>(Pointer = true)]
[assembly: GoImplement<SyncConn, Conn>(Pointer = true)]
[assembly: GoImplement<io_package.ReadWriteCloser, io_package.ReadWriter>]
[assembly: GoImplement<stdioConn, io_package.ReadWriteCloser>(Pointer = true)]
[assembly: GoImplement<stdioConn, io_package.Reader>(Promoted = true)]
[assembly: GoImplement<stdioConn, io_package.Writer>(Promoted = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(AsyncConn))]
[assembly: GoFieldAccess(typeof(Message))]
[assembly: GoFieldAccess(typeof(SyncConn))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package.ResponseError))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ipc/conn.go", "conn.cs", "AAsWggAMMoKClIKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ipc/conn_async.go", "conn_async.cs", "ACRSgqiCAAkWgoKUvNKCgqKCgoKk6oKCpoKCgpSmgoKCsoKCgsqCogAJDtKCgoKipNKCgoKUgoLWgoKCgoKClIKCgpSmgoKCurKCgoKUlIKCuuakgoKCgpSkgpSCgoKClKaClIKCqIKCgpSCuJSCypSCpoKUgoLKpoKU6KKoxJaCgoKCgpSClLKCgoKCtKaCgpSCprSkgoKCgpSClPrSgoKCgpSCgoI=", "71-81:1;98-106:2;108-110:3;207-223:1;271-278:1", "", "245=Error/1/1/2;268=Error/1/1/2", "", "70:21,71:13,239:64,240:13,310:21,311:13")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ipc/conn_sync.go", "conn_sync.cs", "ABxEggAIFIKClLyigoKmgoKUgoKUpoKClIKWzOakgoKCgpSkgpSCgoKClKaClIKCqIKCgpSCuJSCypSCpoKUgoLKpoKU6KKq3IKUlIKmgqiCgqaCgpSo2NKCgg==", "120-136:1", "", "123=Error/1/1/2;146=Error/1/1/2", "", "117:64,118:13")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ipc/protocol_jsonrpc.go", "protocol_jsonrpc.cs", "ABEiloL+goKCpoKCpqiCyoKClKiCuIKClKiCgpS4goKUqIK4goKU", "", "", "21=NewReader/1/1/1,NewWriter/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ipc/timing.go", "timing.cs", "ACV+gsjSlIKUgpTKgpSCtKjSgpSCgpQACRDSgpSCgqLIgoKUqoIABRKCgpQ=", "", "", "58=Now/1/1/3,UnixMilli/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ipc/transport.go", "transport.cs", "ABIugoKClMiCqIKoggAKGIL+goKUggAHEoIADRSCgoKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ipc/transport_unix.go", "transport_unix.cs", "AAgYhIKohA==")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("ipc", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/ipc")]
public static partial class ipc_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct serverTimingInfo {}
    internal partial struct stdioConn {}
    internal partial struct timingCollector {}
    public partial interface Conn {}
    public partial interface Handler {}
    public partial interface Protocol {}
    public partial interface Transport {}
    public partial struct AsyncConn {}
    public partial struct JSONRPCProtocol {}
    public partial struct PipeTransport {}
    public partial struct StdioTransport {}
    public partial struct SyncConn {}
    public partial struct serverRequestTiming {}
    public partial struct serverTimingTotals {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸcontext() => builtin.initPackage(typeof(context_package));
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjsonrpc() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package));
    [GoInit] internal static void initᴛᴛimportꓸio() => builtin.initPackage(typeof(io_package));
    [GoInit] internal static void initᴛᴛimportꓸnet() => builtin.initPackage(typeof(net_package));
    [GoInit] internal static void initᴛᴛimportꓸos() => builtin.initPackage(typeof(os_package));
    [GoInit] internal static void initᴛᴛimportꓸpath() => builtin.initPackage(typeof(path_package));
    [GoInit] internal static void initᴛᴛimportꓸruntimeꓸdebug() => builtin.initPackage(typeof(runtime.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸsyncꓸatomic() => builtin.initPackage(typeof(go.sync.atomic_package));
    [GoInit] internal static void initᴛᴛimportꓸtime() => builtin.initPackage(typeof(time_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(ipc_package.AsyncConn)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class ipcᴛAsyncConn;
[GoMethodHost(typeof(ipc_package.JSONRPCProtocol)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class ipcᴛJSONRPCProtocol;
[GoMethodHost(typeof(ipc_package.PipeTransport)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class ipcᴛPipeTransport;
[GoMethodHost(typeof(ipc_package.StdioTransport)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class ipcᴛStdioTransport;
[GoMethodHost(typeof(ipc_package.SyncConn)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class ipcᴛSyncConn;
[GoMethodHost(typeof(ipc_package.stdioConn)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class ipcᴛstdioConn;
[GoMethodHost(typeof(ipc_package.timingCollector)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class ipcᴛtimingCollector;
// </MethodHolders>
