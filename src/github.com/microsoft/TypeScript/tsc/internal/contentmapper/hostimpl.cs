namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using cmp = cmp_package;
using context = context_package;
using hex = encoding.hex_package;
using errors = errors_package;
using fmt = fmt_package;
using io = io_package;
using maps = maps_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using time = time_package;
using utf8 = unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using ipc = go.github.com.microsoft.TypeScript.tsc.@internal.ipc_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using jsonrpc = go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using xxh3 = go.github.com.zeebo.xxh3_package;
using MethodImplAttribute = global::System.Runtime.CompilerServices.MethodImplAttribute;
using MethodImplOptions = global::System.Runtime.CompilerServices.MethodImplOptions;
using encoding;
using go.github.com.go_json_experiment.json;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.zeebo;
using go.sync;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using unicode;
using static contentmapper_package;

partial class contentmapper_package {

internal static UntypedInt initializeTimeoutSeconds => 5;

internal static time.Duration initializeTimeout => /* initializeTimeoutSeconds * time.Second */ 5000000000;

public static readonly @string MethodInitialize = "initialize"u8;
public static readonly @string MethodOpenProject = "openProject"u8;
public static readonly @string MethodCloseProject = "closeProject"u8;
public static readonly @string MethodTransform = "transform"u8;

partial struct InitializeParams {
    public @string Locale; /*`json:"locale,omitempty"`*/
    public slice<PositionEncoding> PositionEncodings; /*`json:"positionEncodings"`*/
}

partial struct InitializeResult {
    public PositionEncoding PositionEncoding; /*`json:"positionEncoding"`*/
    public @string DiagnosticSource; /*`json:"diagnosticSource"`*/
}

partial struct OpenProjectParams {
    public @string ConfigFileName; /*`json:"configFileName"`*/
    public @string ProjectHandle; /*`json:"projectHandle"`*/
    public jsontext.Value Options; /*`json:"options,omitempty"`*/
    public jsontext.Value CompilerOptions; /*`json:"compilerOptions"`*/
}

partial struct OpenProjectResult {
    public @string ConfigIdentity; /*`json:"configIdentity"`*/
    public slice<@string> WatchedFiles; /*`json:"watchedFiles,omitempty"`*/
    public slice<OptionDiagnosticResult> OptionDiagnostics; /*`json:"optionDiagnostics,omitempty"`*/
}

partial struct OptionDiagnosticResult {
    public slice<jsontext.Value> Path; /*`json:"path"`*/
    public @string MessageText; /*`json:"messageText"`*/
    public int32 Code; /*`json:"code"`*/
}

partial struct CloseProjectParams {
    public @string ProjectHandle; /*`json:"projectHandle"`*/
}

partial struct PositionEncoding /*@string*/;

public static readonly PositionEncoding PositionEncodingUTF8 = "utf-8"u8;
public static readonly PositionEncoding PositionEncodingUTF16 = "utf-16"u8;

partial struct TransformParams {
    public @string FileName; /*`json:"fileName"`*/
    public @string Content; /*`json:"content"`*/
    public @string ProjectHandle; /*`json:"projectHandle"`*/
}

partial struct MappedOutput {
    public @string Text; /*`json:"text"`*/
    public @string Extension; /*`json:"extension"`*/
    public jsontext.Value Mappings; /*`json:"mappings,omitempty"`*/
    public ж<DiagnosticDirectives> DiagnosticDirectives; /*`json:"diagnosticDirectives,omitempty"`*/
}

partial struct DiagnosticDirectivePolicy /*num:uint8*/;

public static DiagnosticDirectivePolicy DiagnosticDirectivePolicyIgnore => /* iota */ 0;
public static DiagnosticDirectivePolicy DiagnosticDirectivePolicyExpect => 1;

partial struct UnusedExpectDirectiveDiagnostic {
    public int32 Code; /*`json:"code"`*/
    public @string MessageText; /*`json:"messageText"`*/
}

partial struct DiagnosticDirectives {
    public slice<UnusedExpectDirectiveDiagnostic> UnusedExpectDirectiveDiagnostics; /*`json:"unusedExpectDirectiveDiagnostics"`*/
    public slice<MappedDiagnosticDirective> Directives; /*`json:"directives"`*/
}

partial struct MappedDiagnosticDirective {
    public nint OriginalStart;
    public nint OriginalLength;
    public nint VirtualStart;
    public nint VirtualEnd;
    public DiagnosticDirectivePolicy Policy;
    public ж<nint> UnusedExpectDirectiveIndex;
}

internal static jsonꓸMarshalerTo _ᴛ1ʗ = new MappedDiagnosticDirective();
internal static jsonꓸUnmarshalerFrom _ᴛ2ʗ = new MappedDiagnosticDirectiveжUnmarshalerFrom(((ж<MappedDiagnosticDirective>)nil));

} partial class contentmapperᴛMappedDiagnosticDirective {

public static error MarshalJSONTo(this MappedDiagnosticDirective d, ж<jsontext.Encoder> enc) {
    var tuple = slice<any>([d.OriginalStart, d.OriginalLength, d.VirtualStart, d.VirtualEnd, d.Policy]);
    if (d.UnusedExpectDirectiveIndex != nil) {
        tuple = append(tuple, (any)(d.UnusedExpectDirectiveIndex.Value));
    }
    return json.MarshalEncode(enc, tuple);
}

public static error UnmarshalJSONFrom(this ж<MappedDiagnosticDirective> Ꮡd, ж<jsontext.Decoder> dec) {
    ref var d = ref Ꮡd.DerefOrNull();

    ref var tuple = ref heap<slice<jsontext.Value>>(out var Ꮡtuple);
    {
        var err = json.UnmarshalDecode(dec, Ꮡtuple); if (err != nil) {
            return err;
        }
    }
    if (len(tuple) != 5 && len(tuple) != 6) {
        return fmt.Errorf("diagnostic directive tuple must contain 5 or 6 elements, got %d"u8, len(tuple));
    }
    d = new MappedDiagnosticDirective();
    var fields = slice<any>([Ꮡd.ᏑOriginalStart, Ꮡd.ᏑOriginalLength, Ꮡd.ᏑVirtualStart, Ꮡd.ᏑVirtualEnd, Ꮡd.ᏑPolicy]);
    foreach (var (i, field) in range(fields)) {
        var err = json.Unmarshal(tuple[i], field); if (err != nil) {
            return fmt.Errorf("invalid diagnostic directive tuple element %d: %w"u8, i, err);
        }
    }
    if (len(tuple) == 6) {
        ref var index = ref heap(new nint(), out var Ꮡindex);
        d.UnusedExpectDirectiveIndex = Ꮡindex;
        var err = json.Unmarshal(tuple[5], d.UnusedExpectDirectiveIndex.OrTypedNil()); if (err != nil) {
            return fmt.Errorf("invalid diagnostic directive tuple element 5: %w"u8, err);
        }
    }
    return default;
}

} partial class contentmapper_package {

partial struct SupplementalOutput {
    /*embed*/ public MappedOutput MappedOutput;
}

partial struct TransformResult {
    /*embed*/ public MappedOutput MappedOutput;
    public slice<Diagnostic> Diagnostics; /*`json:"diagnostics,omitempty"`*/
    public slice<SupplementalOutput> Supplemental; /*`json:"supplemental,omitempty"`*/
}

partial struct Diagnostic {
    public @string MessageText; /*`json:"messageText"`*/
    public nint Start; /*`json:"start"`*/
    public nint Length; /*`json:"length"`*/
    public int32 Code; /*`json:"code"`*/
}

// type dialFunc is a methodless func type — rendered inline as its base delegate

partial struct host {
    internal context.Context ctx;
    internal Action cancel;
    internal Func<bool> stop;
    internal Func<context.Context, ж<Mapper>, locale.Locale, (ipc.Conn, io.Closer, PositionEncoding, @string, error)> dial;
    internal ж<timingCollector> timing;
    internal sync.RWMutex lifecycleMu;
    internal locale.Locale diagnosticLocale;
    internal sync.Mutex mu;
    internal map<@string, ж<mapperConn>> conns;
    internal map<@string, ж<projectEntry>> projects;
    internal map<@string, ж<projectLease>> projectLeases;
    internal uint64 nextProjectID;
}

partial struct projectEntry {
    internal ж<Mapper> mapper;
    internal ProjectSpec spec;
    internal @string projectHandle;
    internal bool opened;
    internal @string configIdentity;
    internal slice<@string> watchedFiles;
    internal slice<OptionDiagnostic> optionDiagnostics;
}

partial struct mapperConn {
    internal ipc.Conn conn;
    internal io.Closer closer;
    internal error err;
    internal PositionEncoding positionEncoding;
    internal @string diagnosticSource;
    internal nint refs;
}

partial struct operationTiming {
    internal atomic.Uint64 count;
    internal atomic.Int64 duration;
}

} partial class contentmapperᴛoperationTiming {

internal static void record(this ж<operationTiming> Ꮡt, time.Time start) {
    Ꮡt.Ꮡcount.Add(1);
    Ꮡt.Ꮡduration.Add((int64)time.Since(start));
}

internal static OperationTiming snapshot(this ж<operationTiming> Ꮡt) {
    return new OperationTiming(Count: Ꮡt.Ꮡcount.Load(), Duration: ((time.Duration)Ꮡt.Ꮡduration.Load()));
}

} partial class contentmapper_package {

partial struct timingCollector {
    internal sync.Mutex mu;
    internal map<@string, ж<mapperTimingCollector>> mappers;
    internal uint64 activeRequests;
    internal time.Time requestWaitStart;
    internal time.Duration requestWaitElapsed;
}

partial struct mapperTimingCollector {
    internal operationTiming spawn;
    internal operationTiming initialize;
    internal operationTiming openProject;
    internal operationTiming closeProject;
    internal operationTiming transform;
    internal ж<timingCollector> owner;
}

} partial class contentmapperᴛtimingCollector {

internal static ж<mapperTimingCollector> mapper(this ж<timingCollector> Ꮡt, @string identity) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var t = ref Ꮡt.DerefOrNull();

        t.mu.Lock();
        ᒐd1 = true;
        var timingΔ1 = t.mappers[identity]; if (timingΔ1 != nil) {
            return timingΔ1;
        }
        var timing = Ꮡ(new mapperTimingCollector(owner: Ꮡt));
        t.mappers[identity] = timing;
        return timing;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡt.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static Timings snapshot(this ж<timingCollector> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    t.mu.Lock();
    var requestWait = t.requestWaitElapsed;
    if (t.activeRequests != 0) {
        requestWait += time.Since(t.requestWaitStart);
    }
    var mappers = new map<@string, ж<mapperTimingCollector>>(len(t.mappers));
    maps.Copy<map<@string, ж<mapperTimingCollector>>, map<@string, ж<mapperTimingCollector>>, @string, ж<mapperTimingCollector>>(mappers, t.mappers);
    t.mu.Unlock();
    var result = new Timings(Mappers: new map<@string, MapperTimings>(len(mappers)), RequestWait: requestWait);
    foreach (var (identity, timing) in mappers) {
        result.Mappers[identity] = new MapperTimings(
            Spawn: timing.Ꮡspawn.snapshot(),
            Initialize: timing.Ꮡinitialize.snapshot(),
            OpenProject: timing.ᏑopenProject.snapshot(),
            CloseProject: timing.ᏑcloseProject.snapshot(),
            Transform: timing.Ꮡtransform.snapshot()
        );
    }
    return result;
}

} partial class contentmapperᴛmapperTimingCollector {

internal static time.Time startRequest(this ref mapperTimingCollector t) {
    t.owner.Ꮡmu.Lock();
    if (t.owner.activeRequests == 0) {
        t.owner.requestWaitStart = time.Now();
    }
    t.owner.activeRequests++;
    t.owner.Ꮡmu.Unlock();
    return time.Now();
}

internal static void finishRequest(this ref mapperTimingCollector t, ж<operationTiming> operation, time.Time start) {
    operation.record(start);
    t.owner.Ꮡmu.Lock();
    t.owner.activeRequests--;
    if (t.owner.activeRequests == 0) {
        t.owner.requestWaitElapsed += time.Since(t.owner.requestWaitStart);
    }
    t.owner.Ꮡmu.Unlock();
}

} partial class contentmapper_package {

internal static Host _ᴛ3ʗ = new hostжHost(((ж<host>)nil));

partial interface Spawner {
    (io.ReadWriteCloser, error) Spawn(slice<@string> command, @string dir, io.Writer stderr);
}

partial interface processExitState {
    (nint, bool) ExitCode();
}

public delegate (io.ReadWriteCloser, error) SpawnerFunc(slice<@string> command, @string dir, io.Writer stderr);

} partial class contentmapperᴛSpawnerFunc {

public static (io.ReadWriteCloser, error) Spawn(this SpawnerFunc f, slice<@string> command, @string dir, io.Writer stderr) {
    return f(command, dir, stderr);
}

} partial class contentmapper_package {

// type Logger is a methodless func type — rendered inline as its base delegate

partial struct HostOptions {
    public Action<@string> Logger;
}

partial struct loggingProtocol {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.ipc_package.Protocol Protocol;
    internal @string mapperName;
    internal Action<@string> logger;
}

} partial class contentmapperᴛloggingProtocol {

internal static void log(this ref loggingProtocol p, @string direction, any message) {
    var (data, err) = json.Marshal(message);
    if (err != nil) {
        p.logger(fmt.Sprintf("[content mapper: %s] %s: <failed to serialize: %v>"u8, p.mapperName, direction, err));
        return;
    }
    p.logger(fmt.Sprintf("[content mapper: %s] %s: %s"u8, p.mapperName, direction, data));
}

internal static (ж<jsonrpc.Message>, error) ReadMessage(this ref loggingProtocol p) {
    var (message, err) = p.Protocol.ReadMessage();
    if (err == nil) {
        p.log("receive", message.OrTypedNil());
    }
    return (message, err);
}

internal static error WriteRequest(this ref loggingProtocol p, ж<jsonrpc.ID> id, @string method, any @params) {
    var message = new jsonrpc.RequestMessage(ID: id, Method: method, Params: @params);
    p.log("send", message);
    return p.Protocol.WriteRequest(id, method, @params);
}

internal static error WriteNotification(this ref loggingProtocol p, @string method, any @params) {
    var message = new jsonrpc.RequestMessage(Method: method, Params: @params);
    p.log("send", message);
    return p.Protocol.WriteNotification(method, @params);
}

internal static error WriteResponse(this ref loggingProtocol p, ж<jsonrpc.ID> id, any result) {
    var message = new jsonrpc.ResponseMessage(ID: id, Result: result);
    p.log("send", message);
    return p.Protocol.WriteResponse(id, result);
}

internal static error WriteError(this ref loggingProtocol p, ж<jsonrpc.ID> id, ж<jsonrpc.ResponseError> responseError) {
    var message = new jsonrpc.ResponseMessage(ID: id, Error: responseError);
    p.log("send", message);
    return p.Protocol.WriteError(id, responseError);
}

} partial class contentmapper_package {

partial struct stderrLogger {
    internal @string mapperName;
    internal Action<@string> logger;
    internal sync.Mutex mu;
    internal @string pending;
}

} partial class contentmapperᴛstderrLogger {

internal static (nint, error) Write(this ж<stderrLogger> Ꮡw, slice<byte> data) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var w = ref Ꮡw.DerefOrNull();

        w.mu.Lock();
        ᒐd1 = true;
        w.pending += ((@string)data);
        for (;;) {
            nint index = strings.IndexByte(w.pending, (byte)'\n');
            if (index < 0) {
                break;
            }
            w.log(strings.TrimSuffix(w.pending.slice(0, index), "\r"));
            w.pending = w.pending.slice(index + 1);
        }
        return (len(data), default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡw.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static void flush(this ж<stderrLogger> Ꮡw) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var w = ref Ꮡw.DerefOrNull();

        w.mu.Lock();
        ᒐd1 = true;
        if (w.pending != ""u8) {
            w.log(strings.TrimSuffix(w.pending, "\r"));
            w.pending = "";
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡw.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static void log(this ref stderrLogger w, @string message) {
    w.logger(fmt.Sprintf("[content mapper: %s] stderr: %s"u8, w.mapperName, message));
}

} partial class contentmapper_package {

partial struct loggedProcess {
    /*embed*/ public io_package.ReadWriteCloser ReadWriteCloser;
    internal ж<stderrLogger> stderr;
}

} partial class contentmapperᴛloggedProcess {

// Go method set entry for the promoted 'ReadWriteCloser.Read()' - provided ONLY by the embedded
// interface field in *loggedProcess's method set; see the pointer-only satisfaction record.
internal static (nint, error) Read(this loggedProcess recvᴛ, slice<byte> p) => recvᴛ.ReadWriteCloser.Read(p);

// Go method set entry for the promoted 'ReadWriteCloser.Write()' - provided ONLY by the embedded
// interface field in *loggedProcess's method set; see the pointer-only satisfaction record.
internal static (nint, error) Write(this loggedProcess recvᴛ, slice<byte> p) => recvᴛ.ReadWriteCloser.Write(p);

internal static error Close(this ref loggedProcess p) {
    var err = p.ReadWriteCloser.Close();
    p.stderr.flush();
    return err;
}

} partial class contentmapper_package {

partial struct closeOnceReadWriteCloser {
    /*embed*/ public io_package.ReadWriteCloser ReadWriteCloser;
    internal sync.Once once;
    internal error err;
}

} partial class contentmapperᴛcloseOnceReadWriteCloser {

// Go method set entry for the promoted 'ReadWriteCloser.Read()' - provided ONLY by the embedded
// interface field in *closeOnceReadWriteCloser's method set; see the pointer-only satisfaction record.
internal static (nint, error) Read(this closeOnceReadWriteCloser recvᴛ, slice<byte> p) => recvᴛ.ReadWriteCloser.Read(p);

// Go method set entry for the promoted 'ReadWriteCloser.Write()' - provided ONLY by the embedded
// interface field in *closeOnceReadWriteCloser's method set; see the pointer-only satisfaction record.
internal static (nint, error) Write(this closeOnceReadWriteCloser recvᴛ, slice<byte> p) => recvᴛ.ReadWriteCloser.Write(p);

internal static error Close(this ж<closeOnceReadWriteCloser> Ꮡc) {
    ref var c = ref Ꮡc.DerefOrNull();

    Ꮡc.Ꮡonce.Do(() => {
        Ꮡc.Value.err = Ꮡc.Value.ReadWriteCloser.Close();
    });
    return c.err;
}

internal static (nint, bool) ExitCode(this ref closeOnceReadWriteCloser c) {
    var (state, ok) = c.ReadWriteCloser._<processExitState>(ᐧ); if (ok) {
        return state.ExitCode();
    }
    return (0, false);
}

} partial class contentmapper_package {

public static Host NewHost(context.Context ctx, Spawner spawner, locale.Locale diagnosticLocale) {
    return NewHostWithOptions(ctx, spawner, diagnosticLocale, new HostOptions());
}

public static Host NewHostWithOptions(context.Context ctx, Spawner spawner, locale.Locale diagnosticLocale, HostOptions options) {
    var logger = options.Logger;
    var timing = Ꮡ(new timingCollector(mappers: new map<@string, ж<mapperTimingCollector>>()));
    var loggerʗ1 = logger;
    var timingʗ1 = timing;
    return new hostжHost(newWithDial(ctx, diagnosticLocale, timing, [MethodImpl(MethodImplOptions.NoInlining)] (context.Context ctxΔ1, ж<Mapper> mapper, locale.Locale diagnosticLocaleΔ1) => {
        if (len(mapper.Exec) == 0) {
            return (default, default, (@string)"", "", fmt.Errorf("content mapper %q declares no command to run"u8, mapper.Package));
        }
        var mapperTiming = timingʗ1.mapper(mapper.Identity());
        ref var diagnosticName = ref heap<@string>(mapper.DiagnosticName(), out var ᏑdiagnosticName);
        var spawnStart = time.Now();
        io.Writer stderr = io.Discard;
        ж<stderrLogger> stderrLog = default;
        if (loggerʗ1 != nil) {
            stderrLog = Ꮡ(new stderrLogger(mapperName: diagnosticName, logger: loggerʗ1));
            stderr = new stderrLoggerжWriter(stderrLog);
        }
        ref var rwc = ref heap<io.ReadWriteCloser>(out var Ꮡrwc);
        (Ꮡrwc.ValueSlot, var err) = spawner.Spawn(mapper.Exec, mapper.PackageDirectory, stderr);
        mapperTiming.Ꮡspawn.record(spawnStart);
        if (err != nil) {
            return (default, default, (@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindProcessStart, MapperName: diagnosticName, Command: mapper.Exec[0], Detail: err.Error()))));
        }
        if (stderrLog != nil) {
            Ꮡrwc.ValueSlot = new loggedProcessжReadWriteCloser(Ꮡ(new loggedProcess(ReadWriteCloser: Ꮡrwc.ValueSlot, stderr: stderrLog)));
        }
        Ꮡrwc.ValueSlot = new closeOnceReadWriteCloserжReadWriteCloser(Ꮡ(new closeOnceReadWriteCloser(ReadWriteCloser: Ꮡrwc.ValueSlot)));
        var protocol = ((ipc.Protocol)new ipc.JSONRPCProtocolжProtocol(ipc.NewJSONRPCProtocol(new io_ReadWriteCloserᴠReadWriter(Ꮡrwc.ValueSlot))));
        if (loggerʗ1 != nil) {
            protocol = new loggingProtocolжProtocol(Ꮡ(new loggingProtocol(Protocol: protocol, mapperName: diagnosticName, logger: loggerʗ1)));
        }
        var conn = ipc.NewAsyncConnWithProtocol(Ꮡrwc.ValueSlot, protocol, new rejectHandler());
        var connʗ1 = conn;
        goǃ(() => {
            _ = connʗ1.Run(ctxΔ1);
            _ = Ꮡrwc.ValueSlot.Close();
        });
        var (initializeCtx, cancel) = context.WithTimeout(ctxΔ1, initializeTimeout);
        var initializeStart = mapperTiming.startRequest();
        (var positionEncoding, var diagnosticSource, err) = handshake(initializeCtx, new ipc.AsyncConnжConn(conn), diagnosticLocaleΔ1);
        mapperTiming.finishRequest(mapperTiming.Ꮡinitialize, initializeStart);
        var initializeCtxErr = initializeCtx.Err();
        cancel();
        if (err != nil) {
            ref var exitCode = ref heap(new nint(), out var ᏑexitCode);
            bool exited = default;
            if (Ꮡrwc.ValueSlot._<processExitState>(out var exitState)) {
                (exitCode, exited) = exitState.ExitCode();
            }
            _ = Ꮡrwc.ValueSlot.Close();
            var (initializeError, ok) = errors.AsType<ж<InitializeError>>(err); if (ok) {
                initializeError.MapperName = diagnosticName;
                return (default, default, (@string)"", "", new InitializeErrorжerror(initializeError));
            }
            if (exited) {
                return (default, default, (@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindProcessExit, MapperName: diagnosticName, ExitCode: exitCode))));
            }
            if (initializeCtxErr != nil || errors.Is(err, context.Canceled) || errors.Is(err, context.DeadlineExceeded)) {
                return (default, default, (@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindNoResponse, MapperName: diagnosticName, TimeoutSeconds: initializeTimeoutSeconds))));
            }
            return (default, default, (@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindRequest, MapperName: diagnosticName, Detail: err.Error()))));
        }
        return (new ipc.AsyncConnжConn(conn), Ꮡrwc.ValueSlot, positionEncoding, diagnosticSource, default);
    }));
}

internal static ж<host> newWithDial(context.Context ctx, locale.Locale diagnosticLocale, ж<timingCollector> timing, Func<context.Context, ж<Mapper>, locale.Locale, (ipc.Conn, io.Closer, PositionEncoding, @string, error)> dial) {
    var (hostCtx, cancel) = context.WithCancel(ctx);
    var h = Ꮡ(new host(ctx: hostCtx, cancel: cancel, dial: dial, timing: timing, diagnosticLocale: diagnosticLocale, conns: new map<@string, ж<mapperConn>>(), projects: new map<@string, ж<projectEntry>>(), projectLeases: new map<@string, ж<projectLease>>()));
    var hʗ1 = h;
    h.stop = context.AfterFunc(ctx, () => {
        _ = hʗ1.Close();
    });
    return h;
}

} partial class contentmapperᴛhost {

internal static Timings Timings(this ref host h) {
    return h.timing.snapshot();
}

internal static void SetLocale(this ж<host> Ꮡh, locale.Locale diagnosticLocale) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var h = ref Ꮡh.DerefOrNull();
        var closersᴷ = new stackAppend<io.Closer>(moves: true);

        Ꮡh.ᏑlifecycleMu.Lock();
        ᒐd1 = true;
        if (h.diagnosticLocale.String() == diagnosticLocale.String()) {
            return;
        }
        h.diagnosticLocale = diagnosticLocale;

        h.mu.Lock();
        slice<io.Closer> closers = default;
        foreach (var (_, entry) in h.conns) {
            if (entry.closer != nil) {
                closers = append(closersᴷ, closers, entry.closer);
            }
            entry.conn = default;
            entry.closer = default;
            entry.err = default;
            entry.positionEncoding = ""u8;
            entry.diagnosticSource = "";
        }
        foreach (var (_, project) in h.projects) {
            project.opened = false;
        }
        h.mu.Unlock();
        closers = move2heap(closersᴷ, closers);
        foreach (var (_, closer) in range(closers)) {
            _ = closer.Close();
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡh.ᏑlifecycleMu.Unlock(); ᒐ.Run(); }
}

internal static Project Project(this ж<host> Ꮡh, ProjectSpec spec) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    bool ᒐd2 = false;
    try {
        ref var h = ref Ꮡh.DerefOrNull();

        Ꮡh.ᏑlifecycleMu.RLock();
        ᒐd1 = true;

        ref var key = ref heap<@string>(projectSpecKey(spec), out var Ꮡkey);
        h.mu.Lock();
        ᒐd2 = true;
        if (h.projects == nil) {
            return default;
        }
        var leaseΔ1 = h.projectLeases[key]; if (leaseΔ1 != nil) {
            return leaseΔ1.retainLocked();
        }
        var lease = Ꮡ(new projectLease(host: Ꮡh, key: key, mappers: slices.Clone<slice<ж<Mapper>>, ж<Mapper>>(spec.Mappers), entries: new map<ж<Mapper>, @string>(len(spec.Mappers))));
        lease.refs = 1;
        foreach (var (_, mapper) in range(spec.Mappers)) {
            ref var entryKey = ref heap<@string>(fmt.Sprintf("%s:%d"u8, mapper.Identity(), h.nextProjectID), out var ᏑentryKey);
            h.nextProjectID++;
            var entry = Ꮡ(new projectEntry(mapper: mapper, spec: spec, projectHandle: entryKey));
            h.projects[entryKey] = entry;
            var connEntry = h.conns[mapper.Identity()];
            if (connEntry == nil) {
                connEntry = Ꮡ(new mapperConn());
                h.conns[mapper.Identity()] = connEntry;
            }
            connEntry.refs++;
            lease.entries[mapper] = entryKey;
        }
        h.projectLeases[key] = lease;
        return new projectLeaseжProject(lease);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd2) Ꮡh.DerefOrNull().mu.Unlock(); if (ᒐd1) Ꮡh.ᏑlifecycleMu.RUnlock(); ᒐ.Run(); }
}

} partial class contentmapper_package {

internal static @string projectSpecKey(ProjectSpec spec) {
    ref var key = ref heap(new strings.Builder(), out var Ꮡkey);
    fmt.Fprintf(new strings_BuilderжWriter(Ꮡkey), "%s\x00%p"u8, spec.ConfigFileName, spec.CompilerOptions.OrTypedNil());
    foreach (var (_, mapper) in range(spec.Mappers)) {
        fmt.Fprintf(new strings_BuilderжWriter(Ꮡkey), "\x00%p"u8, mapper.OrTypedNil());
    }
    return key.String();
}

internal static @string combinedIdentity(ж<Mapper> Ꮡmapper, @string configIdentity, ж<core.CompilerOptions> compilerOptions) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();
    var bufᴷ = new stackAppend<byte>();

    var transformIdentity = mapper.TransformIdentity(compilerOptions).Bytes();
    var buf = new slice<byte>(0, len(mapper.Identity()) + len(mapper.Options) + len(configIdentity) + len(transformIdentity) + 3);
    buf = append(buf, mapper.Identity().ꓸꓸꓸ);
    buf = append(bufᴷ, buf, (byte)0);
    buf = appendꓸꓸꓸ(buf, mapper.Options);
    buf = append(buf, (byte)0);
    buf = append(buf, configIdentity.ꓸꓸꓸ);
    buf = append(buf, (byte)0);
    buf = appendꓸꓸꓸ(buf, transformIdentity[..]);
    var hash = xxh3.Hash128(buf).Bytes();
    return mapper.Identity() + ":"u8 + hex.EncodeToString(hash[..]);
}

} partial class contentmapperᴛhost {

internal static error openProjectLocked(this ref host h, context.Context ctx, ж<projectEntry> Ꮡentry) {
    ref var entry = ref Ꮡentry.DerefOrNull();

    if (entry.opened) {
        return default;
    }
    var (conn, _, diagnosticSource, err) = h.connForLocked(entry.mapper);
    if (err != nil) {
        return err;
    }
    (var compilerOptions, err) = json.Marshal(entry.spec.CompilerOptions.OrTypedNil());
    if (err != nil) {
        return err;
    }
    var mapperTiming = h.timing.mapper(entry.mapper.Identity());
    var start = mapperTiming.startRequest();
    (var raw, err) = conn.Call(ctx, MethodOpenProject, new OpenProjectParams(
        ConfigFileName: entry.spec.ConfigFileName,
        ProjectHandle: entry.projectHandle,
        Options: entry.mapper.Options,
        CompilerOptions: compilerOptions
    ));
    mapperTiming.finishRequest(mapperTiming.ᏑopenProject, start);
    if (err != nil) {
        return err;
    }
    ref var result = ref heap(new OpenProjectResult(), out var Ꮡresult);
    var errΔ1 = json.Unmarshal(raw, Ꮡresult); if (errΔ1 != nil) {
        return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindMalformedResponse)));
    }
    if (entry.mapper.DynamicConfig && result.ConfigIdentity == ""u8) {
        return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindMissingConfigIdentity)));
    }
    if (!entry.mapper.DynamicConfig && result.ConfigIdentity != ""u8) {
        return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindUnexpectedConfigIdentity)));
    }
    if (!entry.mapper.DynamicConfig && len(result.WatchedFiles) != 0) {
        return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindUnexpectedWatchedFiles)));
    }
    entry.configIdentity = result.ConfigIdentity;
    foreach (var (_, fileName) in range(result.WatchedFiles)) {
        if (!tspath.PathIsAbsolute(fileName)) {
            return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindNonAbsoluteWatchedFile)));
        }
    }
    entry.watchedFiles = slices.Clone<slice<@string>, @string>(result.WatchedFiles);
    entry.optionDiagnostics = new slice<OptionDiagnostic>(len(result.OptionDiagnostics));
    foreach (var (i, diagnostic) in range(result.OptionDiagnostics)) {
        var path = new slice<OptionPathSegment>(len(diagnostic.Path));
        foreach (var (j, rawSegment) in range(diagnostic.Path)) {
            switch (rawSegment.Kind()) {
            case var ᴛ when ᴛ == (jsontext.Kind)((rune)'"'): {
                ref var property = ref heap(new @string(), out var Ꮡproperty);
                var errΔ4 = json.Unmarshal(rawSegment, Ꮡproperty); if (errΔ4 != nil) {
                    return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindMalformedResponse)));
                }
                path[j].Property = property;
                break;
            }
            case var ᴛ when ᴛ == (jsontext.Kind)((rune)'0'): {
                ref var index = ref heap(new nint(), out var Ꮡindex);
                var errΔ5 = json.Unmarshal(rawSegment, Ꮡindex); if (errΔ5 != nil || index < 0) {
                    return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindMalformedResponse)));
                }
                path[j].Index = index;
                path[j].IsIndex = true;
                break;
            }
            default: {
                return new ProjectErrorжerror(Ꮡ(new ProjectError(Kind: ProjectErrorKindMalformedResponse)));
            }}

        }
        entry.optionDiagnostics[i] = new OptionDiagnostic(
            Mapper: entry.mapper,
            Path: path,
            Source: diagnosticSource,
            Code: diagnostic.Code,
            MessageText: diagnostic.MessageText
        );
    }
    entry.opened = true;
    return default;
}

internal static error closeProject(this ref host h, ж<Mapper> Ꮡmapper, ipc.Conn conn, @string projectHandle) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    var mapperTiming = h.timing.mapper(mapper.Identity());
    var start = mapperTiming.startRequest();
    var (_, err) = conn.Call(h.ctx, MethodCloseProject, new CloseProjectParams(ProjectHandle: projectHandle));
    mapperTiming.finishRequest(mapperTiming.ᏑcloseProject, start);
    return err;
}

internal static Action Acquire(this ж<host> Ꮡh, slice<ж<Mapper>> mappers) {
    ref var h = ref Ꮡh.DerefOrNull();

    var seen = new map<@string, EmptyStruct>(len(mappers));
    var identities = new slice<@string>(0, len(mappers));
    h.mu.Lock();
    if (h.conns != nil) {
        foreach (var (_, mapper) in range(mappers)) {
            @string identity = mapper.Identity();
            var (_, ok) = seen[identity, ꟷ]; if (ok) {
                continue;
            }
            seen[identity] = new EmptyStruct();
            identities = append(identities, identity);
            var entry = h.conns[identity];
            if (entry == nil) {
                entry = Ꮡ(new mapperConn());
                h.conns[identity] = entry;
            }
            entry.refs++;
        }
    }
    h.mu.Unlock();
    var identitiesʗ1 = identities;
    return sync.OnceFunc(() => {
        Ꮡh.release(identitiesʗ1);
    });
}

internal static (Result, error) Transform(this ж<host> Ꮡh, ж<Mapper> mapper, Request request) {
    GoFrame ᒐ = default;
    try {
        var project = Ꮡh.Project(new ProjectSpec(
            Mappers: [mapper],
            CompilerOptions: Ꮡ(new core.CompilerOptions())
        ));
        var projectʗ1 = project;
        defer(() => projectʗ1.Close(), ref ᒐ);
        return project.Transform(mapper, request);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (Result, error) transformLocked(this ж<host> Ꮡh, ж<Mapper> Ꮡmapper, Request request, @string projectHandle) {
    ref var h = ref Ꮡh.DerefOrNull();
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    if (projectHandle == ""u8) {
        return (new Result(), errors.New("content mapper project handle is required"));
    }
    var (conn, positionEncoding, diagnosticSource, err) = Ꮡh.connFor(Ꮡmapper);
    if (err != nil) {
        return (new Result(), new TransformErrorжerror(NewTransformError(TransformErrorKindInitialize, err)));
    }
    var mapperTiming = h.timing.mapper(mapper.Identity());
    var start = mapperTiming.startRequest();
    (var raw, err) = conn.Call(h.ctx, MethodTransform, new TransformParams(
        FileName: request.FileName,
        Content: request.Content,
        ProjectHandle: projectHandle
    ));
    mapperTiming.finishRequest(mapperTiming.Ꮡtransform, start);
    if (err != nil) {
        return (new Result(), new TransformErrorжerror(NewTransformError(TransformErrorKindRequest, err)));
    }
    (var decoded, err) = decodeTransformResult(raw, request.Content, positionEncoding, diagnosticSource);
    if (err != nil) {
        return (new Result(), new TransformErrorжerror(NewTransformError(TransformErrorKindResponse, err)));
    }
    return (decoded, default);
}

internal static error Close(this ж<host> Ꮡh) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var h = ref Ꮡh.DerefOrNull();
        var closersᴷ = new stackAppend<io.Closer>(moves: true);
        var errsᴷ = new stackAppend<error>();

        Ꮡh.ᏑlifecycleMu.Lock();
        ᒐd1 = true;
        h.stop();
        h.cancel();
        h.mu.Lock();
        slice<io.Closer> closers = default;
        foreach (var (_, mc) in h.conns) {
            if (mc.closer != nil) {
                closers = append(closersᴷ, closers, mc.closer);
            }
        }
        h.conns = default;
        h.projects = default;
        h.projectLeases = default;
        h.mu.Unlock();
        slice<error> errs = default;
        closers = move2heap(closersᴷ, closers);
        foreach (var (_, closer) in range(closers)) {
            var err = closer.Close(); if (err != nil) {
                errs = append(errsᴷ, errs, err);
            }
        }
        return errors.Join(errs.ꓸꓸꓸ);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡh.ᏑlifecycleMu.Unlock(); ᒐ.Run(); }
}

internal static (ipc.Conn, PositionEncoding, @string, error) connFor(this ж<host> Ꮡh, ж<Mapper> mapper) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var h = ref Ꮡh.DerefOrNull();

        h.mu.Lock();
        ᒐd1 = true;
        return h.connForLocked(mapper);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡh.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static (ipc.Conn, PositionEncoding, @string, error) connForLocked(this ref host h, ж<Mapper> Ꮡmapper) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    if (h.conns == nil) {
        return (default, (@string)"", "", errors.New("content mapper host is closed"));
    }
    @string identity = mapper.Identity();
    var entry = h.conns[identity];
    if (entry == nil) {
        entry = Ꮡ(new mapperConn());
        h.conns[identity] = entry;
    }
    if (entry.conn != nil || entry.err != nil) {
        return (entry.conn, entry.positionEncoding, entry.diagnosticSource, entry.err);
    }
    var (conn, closer, positionEncoding, diagnosticSource, err) = h.dial(h.ctx, Ꮡmapper, h.diagnosticLocale);
    entry.conn = conn;
    entry.closer = closer;
    entry.err = err;
    entry.positionEncoding = positionEncoding;
    entry.diagnosticSource = diagnosticSource;
    return (conn, positionEncoding, diagnosticSource, err);
}

} partial class contentmapper_package {

partial struct projectLease {
    internal ж<host> host;
    internal @string key;
    internal slice<ж<Mapper>> mappers;
    internal map<ж<Mapper>, @string> entries;
    internal nint refs;
    internal sync.Once once;
}

partial struct retainedProject {
    /*embed*/ internal ж<projectLease> projectLease;
    internal sync.Once once;
}

} partial class contentmapperᴛprojectLease {

internal static Project retainLocked(this ж<projectLease> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.refs++;
    return new retainedProjectжProject(Ꮡ(new retainedProject(projectLease: Ꮡp)));
}

} partial class contentmapperᴛretainedProject {

internal static error /*err*/ Close(this ж<retainedProject> Ꮡp) {
    error err = default;

    Ꮡp.Ꮡonce.Do(() => {
        err = Ꮡp.Value.projectLease.release();
    });
    return err;
}

} partial class contentmapperᴛprojectLease {

internal static error Refresh(this ж<projectLease> Ꮡp) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.host.ᏑlifecycleMu.RLock();
        defer(Ꮡp.Value.host.ᏑlifecycleMu.RUnlock, ref ᒐ);
        p.host.Ꮡmu.Lock();
        defer(Ꮡp.Value.host.Ꮡmu.Unlock, ref ᒐ);
        if (p.host.projects == nil) {
            return default;
        }
        error result = default;
        foreach (var (_, key) in p.entries) {
            var entry = p.host.projects[key];
            if (entry == nil || !entry.opened) {
                continue;
            }
            var connEntry = p.host.conns[entry.mapper.Identity()]; if (connEntry != nil && connEntry.conn != nil) {
                result = errors.Join(result, p.host.closeProject(entry.mapper, connEntry.conn, entry.projectHandle));
            }
            entry.opened = false;
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (slice<@string>, error) Identities(this ж<projectLease> Ꮡp) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.host.ᏑlifecycleMu.RLock();
        defer(Ꮡp.Value.host.ᏑlifecycleMu.RUnlock, ref ᒐ);
        p.host.Ꮡmu.Lock();
        defer(Ꮡp.Value.host.Ꮡmu.Unlock, ref ᒐ);
        if (p.host.projects == nil) {
            return (default, default);
        }
        var identities = new slice<@string>(0, len(p.entries));
        foreach (var (_, mapper) in range(p.mappers)) {
            @string key = p.entries[mapper];
            var entry = p.host.projects[key];
            if (entry == nil) {
                continue;
            }
            if (mapper.DynamicConfig) {
                var err = p.host.openProjectLocked(p.host.ctx, entry); if (err != nil) {
                    return (default, err);
                }
                identities = append(identities, combinedIdentity(mapper, entry.configIdentity, entry.spec.CompilerOptions));
            } else {
                var hash = mapper.TransformIdentity(entry.spec.CompilerOptions).Bytes();
                identities = append(identities, mapper.Identity() + ":"u8 + hex.EncodeToString(hash[..]));
            }
        }
        return (identities, default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (@string, error) Identity(this ж<projectLease> Ꮡp, ж<Mapper> Ꮡmapper) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();
        ref var mapper = ref Ꮡmapper.DerefOrNull();

        p.host.ᏑlifecycleMu.RLock();
        defer(Ꮡp.Value.host.ᏑlifecycleMu.RUnlock, ref ᒐ);
        p.host.Ꮡmu.Lock();
        defer(Ꮡp.Value.host.Ꮡmu.Unlock, ref ᒐ);
        if (p.host.projects == nil) {
            return ("", default);
        }
        var (key, ok) = p.entries[Ꮡmapper, ꟷ];
        if (!ok) {
            return ("", default);
        }
        var entry = p.host.projects[key];
        if (entry == nil) {
            return ("", default);
        }
        if (mapper.DynamicConfig) {
            var err = p.host.openProjectLocked(p.host.ctx, entry); if (err != nil) {
                return ("", err);
            }
            return (combinedIdentity(Ꮡmapper, entry.configIdentity, entry.spec.CompilerOptions), default);
        }
        var hash = mapper.TransformIdentity(entry.spec.CompilerOptions).Bytes();
        return (mapper.Identity() + ":" + hex.EncodeToString(hash[..]), default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (slice<@string>, error) WatchedFiles(this ж<projectLease> Ꮡp) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.host.ᏑlifecycleMu.RLock();
        defer(Ꮡp.Value.host.ᏑlifecycleMu.RUnlock, ref ᒐ);
        p.host.Ꮡmu.Lock();
        defer(Ꮡp.Value.host.Ꮡmu.Unlock, ref ᒐ);
        if (p.host.projects == nil) {
            return (default, default);
        }
        slice<@string> files = default;
        foreach (var (_, key) in p.entries) {
            var entry = p.host.projects[key];
            if (entry == nil) {
                continue;
            }
            if (entry.mapper.DynamicConfig) {
                var err = p.host.openProjectLocked(p.host.ctx, entry); if (err != nil) {
                    return (default, err);
                }
            }
            files = appendꓸꓸꓸ(files, entry.watchedFiles);
        }
        slices.Sort<slice<@string>, @string>(files);
        return (slices.Compact<slice<@string>, @string>(files), default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static slice<OptionDiagnostic> Diagnostics(this ж<projectLease> Ꮡp) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.host.ᏑlifecycleMu.RLock();
        defer(Ꮡp.Value.host.ᏑlifecycleMu.RUnlock, ref ᒐ);
        p.host.Ꮡmu.Lock();
        defer(Ꮡp.Value.host.Ꮡmu.Unlock, ref ᒐ);
        if (p.host.projects == nil) {
            return default;
        }
        slice<OptionDiagnostic> diagnostics = default;
        foreach (var (_, mapper) in range(p.mappers)) {
            var entry = p.host.projects[p.entries[mapper]];
            if (entry == nil) {
                continue;
            }
            if (!entry.opened) {
                continue;
            }
            diagnostics = appendꓸꓸꓸ(diagnostics, entry.optionDiagnostics);
        }
        return diagnostics;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (Result, error) Transform(this ж<projectLease> Ꮡp, ж<Mapper> mapper, Request request) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.host.ᏑlifecycleMu.RLock();
        defer(Ꮡp.Value.host.ᏑlifecycleMu.RUnlock, ref ᒐ);
        p.host.Ꮡmu.Lock();
        var entry = p.host.projects[p.entries[mapper]];
        if (entry == nil) {
            p.host.Ꮡmu.Unlock();
            return (new Result(), errors.New("content mapper project is closed"));
        }
        var err = p.host.openProjectLocked(p.host.ctx, entry); if (err != nil) {
            p.host.Ꮡmu.Unlock();
            var (_, ok) = errors.AsType<ж<InitializeError>>(err); if (ok) {
                return (new Result(), new TransformErrorжerror(NewTransformError(TransformErrorKindInitialize, err)));
            }
            return (new Result(), new TransformErrorжerror(NewTransformError(TransformErrorKindProject, err)));
        }
        @string handle = entry.projectHandle;
        p.host.Ꮡmu.Unlock();
        return p.host.transformLocked(mapper, request, handle);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static error Close(this ж<projectLease> Ꮡp) {
    ref var result = ref heap<error>(out var Ꮡresult);
    Ꮡp.Ꮡonce.Do(() => {
        Ꮡresult.ValueSlot = Ꮡp.release();
    });
    return result;
}

internal static error release(this ж<projectLease> Ꮡp) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();
        var releasedIdentitiesᴷ = new stackAppend<@string>();

        error result = default;
        {
            p.host.ᏑlifecycleMu.RLock();
            defer(Ꮡp.Value.host.ᏑlifecycleMu.RUnlock, ref ᒐ);
            slice<@string> releasedIdentities = default;
            p.host.Ꮡmu.Lock();
            p.refs--;
            if (p.refs < 0) {
                p.host.Ꮡmu.Unlock();
                throw panic("content mapper project reference count below zero");
            }
            if (p.refs != 0) {
                p.host.Ꮡmu.Unlock();
                return default;
            }
            if (p.host.projectLeases[p.key] == Ꮡp) {
                delete(p.host.projectLeases, p.key);
            }
            foreach (var (_, key) in p.entries) {
                var entry = p.host.projects[key];
                if (entry == nil) {
                    continue;
                }
                if (entry.opened) {
                    var connEntry = p.host.conns[entry.mapper.Identity()]; if (connEntry != nil && connEntry.conn != nil) {
                        result = errors.Join(result, p.host.closeProject(entry.mapper, connEntry.conn, entry.projectHandle));
                    }
                }
                delete(p.host.projects, key);
                releasedIdentities = append(releasedIdentitiesᴷ, releasedIdentities, entry.mapper.Identity());
            }
            p.host.Ꮡmu.Unlock();
            p.host.release(releasedIdentities);
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class contentmapperᴛhost {

internal static void release(this ж<host> Ꮡh, slice<@string> identities) {
    ref var h = ref Ꮡh.DerefOrNull();
    var closersᴷ = new stackAppend<io.Closer>(moves: true);

    slice<io.Closer> closers = default;
    h.mu.Lock();
    if (h.conns != nil) {
        foreach (var (_, identity) in range(identities)) {
            var entry = h.conns[identity];
            if (entry == nil) {
                continue;
            }
            entry.refs--;
            if (entry.refs == 0) {
                delete(h.conns, identity);
                if (entry.closer != nil) {
                    closers = append(closersᴷ, closers, entry.closer);
                }
            }
        }
    }
    h.mu.Unlock();
    closers = move2heap(closersᴷ, closers);
    foreach (var (_, closer) in range(closers)) {
        _ = closer.Close();
    }
}

} partial class contentmapper_package {

internal static (PositionEncoding, @string, error) handshake(context.Context ctx, ipc.Conn conn, locale.Locale diagnosticLocale) {
    var (raw, err) = conn.Call(ctx, MethodInitialize, new InitializeParams(
        Locale: diagnosticLocale.String(),
        PositionEncodings: [PositionEncodingUTF8, PositionEncodingUTF16]
    ));
    if (err != nil) {
        return ((@string)"", "", err);
    }
    ref var res = ref heap(new InitializeResult(), out var Ꮡres);
    var errΔ1 = json.Unmarshal(raw, Ꮡres); if (errΔ1 != nil) {
        return ((@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindInvalidResponse, Detail: errΔ1.Error()))));
    }
    if (res.PositionEncoding != PositionEncodingUTF8 && res.PositionEncoding != PositionEncodingUTF16) {
        return ((@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindPositionEncoding, PositionEncoding: res.PositionEncoding))));
    }
    if (strings.TrimSpace(res.DiagnosticSource) == ""u8) {
        return ((@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindEmptyDiagnosticSource))));
    }
    if (strings.EqualFold(res.DiagnosticSource, "typescript") || strings.EqualFold(res.DiagnosticSource, "tsc")) {
        return ((@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindReservedDiagnosticSource, DiagnosticSource: res.DiagnosticSource))));
    }
    var nativeExtensions = core.Flatten(tspath.AllSupportedExtensionsWithJson);
    if (slices.ContainsFunc(nativeExtensions, (@string extension) => strings.EqualFold(Ꮡres.Value.DiagnosticSource, strings.TrimPrefix(extension, ".")))) {
        return ((@string)"", "", new InitializeErrorжerror(Ꮡ(new InitializeError(Kind: InitializeErrorKindReservedDiagnosticSource, DiagnosticSource: res.DiagnosticSource))));
    }
    return (res.PositionEncoding, res.DiagnosticSource, default);
}

internal static (Result, error) decodeTransformResult(jsontext.Value raw, @string originalText, PositionEncoding positionEncoding, @string diagnosticSource) {
    ref var res = ref heap(new TransformResult(), out var Ꮡres);
    var errΔ1 = json.Unmarshal(raw, Ꮡres); if (errΔ1 != nil) {
        return (new Result(), errΔ1);
    }
    var (mapped, originalPositions, err) = decodeMappedOutput(res.MappedOutput, originalText, positionEncoding, diagnosticSource);
    if (err != nil) {
        return (new Result(), err);
    }
    var result = new Result(
        Text: mapped.Text,
        VirtualExtension: mapped.VirtualExtension,
        Mappings: mapped.Mappings,
        DiagnosticDirectives: mapped.DiagnosticDirectives
    );
    foreach (var (supplementalIndex, supplemental) in range(res.Supplemental)) {
        var (mappedΔ1, _, errΔ2) = decodeMappedOutput(supplemental.MappedOutput, originalText, positionEncoding, diagnosticSource);
        if (errΔ2 != nil) {
            var (directiveError, ok) = errors.AsType<ж<DiagnosticDirectiveError>>(errΔ2); if (ok) {
                directiveError.SupplementalIndex = supplementalIndex;
            }
            return (new Result(), errΔ2);
        }
        result.Supplemental = append(result.Supplemental, mappedΔ1);
    }
    foreach (var (_, d) in range(res.Diagnostics)) {
        if (d.Start < 0 || d.Length < 0 || d.Start > (nint)(~(nuint)0 >> 1) - d.Length) {
            return (new Result(), fmt.Errorf("invalid content mapper diagnostic range [%d, %d)"u8, d.Start, d.Start + d.Length));
        }
        var (start, errΔ3) = originalPositions.normalize(d.Start);
        if (errΔ3 != nil) {
            return (new Result(), fmt.Errorf("invalid content mapper diagnostic start: %w"u8, errΔ3));
        }
        (var end, errΔ3) = originalPositions.normalize(d.Start + d.Length);
        if (errΔ3 != nil) {
            return (new Result(), fmt.Errorf("invalid content mapper diagnostic end: %w"u8, errΔ3));
        }
        result.Diagnostics = append(result.Diagnostics, ast.NewExternalDiagnostic(
            nil,
            core.NewTextRange(start, end),
            diagnosticSource,
            diagnostics.CategoryError,
            d.Code,
            d.MessageText));
    }
    return (result, default);
}

internal static (MappedResult, ж<positionNormalizer>, error) decodeMappedOutput(MappedOutput output, @string originalText, PositionEncoding positionEncoding, @string diagnosticSource) {
    if (!IsSupportedVirtualExtension(output.Extension)) {
        return (new MappedResult(), default, new InvalidVirtualExtensionErrorжerror(Ꮡ(new InvalidVirtualExtensionError(Extension: output.Extension))));
    }
    var result = new MappedResult(
        Text: output.Text,
        VirtualExtension: output.Extension
    );
    var (virtualPositions, err) = newPositionNormalizer(output.Text, positionEncoding);
    if (err != nil) {
        return (new MappedResult(), default, err);
    }
    (var originalPositions, err) = newPositionNormalizer(originalText, positionEncoding);
    if (err != nil) {
        return (new MappedResult(), default, err);
    }
    if (len(output.Mappings) > 0) {
        var (mappings, unmarshalErr) = spanmap.Unmarshal(output.Mappings);
        if (unmarshalErr != nil) {
            return (new MappedResult(), default, unmarshalErr);
        }
        (result.Mappings, err) = normalizeMappings(mappings, virtualPositions, originalPositions);
        if (err != nil) {
            return (new MappedResult(), default, err);
        }
    } else {
        result.Mappings = spanmap.New(default);
    }
    (result.DiagnosticDirectives, err) = normalizeDiagnosticDirectives(output.DiagnosticDirectives, virtualPositions, originalPositions, diagnosticSource);
    if (err != nil) {
        return (new MappedResult(), default, err);
    }
    return (result, originalPositions, default);
}

internal partial struct normalizeDiagnosticDirectives_indexedDirective /*dyn*/ {
    internal ast.MappedDiagnosticDirective directive;
    internal nint index;
}

internal static (slice<ast.MappedDiagnosticDirective>, error) normalizeDiagnosticDirectives(ж<DiagnosticDirectives> ᏑdiagnosticDirectives, ж<positionNormalizer> ᏑvirtualPositions, ж<positionNormalizer> ᏑoriginalPositions, @string diagnosticSource) {
    ref var diagnosticDirectives = ref ᏑdiagnosticDirectives.DerefOrNull();
    ref var virtualPositions = ref ᏑvirtualPositions.DerefOrNull();
    ref var originalPositions = ref ᏑoriginalPositions.DerefOrNull();

    if (ᏑdiagnosticDirectives == nil) {
        return (default, default);
    }
    var directives = diagnosticDirectives.Directives;
    var result = new slice<ast.MappedDiagnosticDirective>(len(directives));
    foreach (var (iᴛ1, directive) in range(directives)) {
        ref var i = ref heap(new nint(), out var Ꮡi);
        i = iᴛ1;

        var iʗ1 = i;
        ж<DiagnosticDirectiveError> directiveError(DiagnosticDirectiveErrorKind kind) => Ꮡ(new DiagnosticDirectiveError(
                Kind: kind,
                Index: iʗ1,
                SupplementalIndex: -1
            ));
        var normalized = new ast.MappedDiagnosticDirective(Source: diagnosticSource);
        switch (directive.Policy) {
        case var ᴛ when ᴛ == DiagnosticDirectivePolicyIgnore: {
            normalized.Policy = ast.MappedDiagnosticDirectivePolicyIgnore;
            break;
        }
        case var ᴛ when ᴛ == DiagnosticDirectivePolicyExpect: {
            nint unusedDiagnosticIndex = 0;
            if (directive.UnusedExpectDirectiveIndex != nil) {
                unusedDiagnosticIndex = directive.UnusedExpectDirectiveIndex.Value;
            } else if (len(diagnosticDirectives.UnusedExpectDirectiveDiagnostics) != 1) {
                return (default, new DiagnosticDirectiveErrorжerror(directiveError(DiagnosticDirectiveErrorKindExpectMissingUnusedDiagnostic)));
            }
            if (unusedDiagnosticIndex < 0 || unusedDiagnosticIndex >= len(diagnosticDirectives.UnusedExpectDirectiveDiagnostics)) {
                return (default, new DiagnosticDirectiveErrorжerror(directiveError(DiagnosticDirectiveErrorKindInvalidUnusedDiagnosticIndex)));
            }
            var unusedDiagnostic = diagnosticDirectives.UnusedExpectDirectiveDiagnostics[unusedDiagnosticIndex];
            normalized.Policy = ast.MappedDiagnosticDirectivePolicyExpect;
            normalized.UnusedCode = unusedDiagnostic.Code;
            normalized.UnusedMessageText = unusedDiagnostic.MessageText;
            break;
        }
        default: {
            var validationError = directiveError(DiagnosticDirectiveErrorKindInvalidPolicy);
            validationError.Policy = directive.Policy;
            return (default, new DiagnosticDirectiveErrorжerror(validationError));
        }}

        if (directive.VirtualStart < 0 || directive.VirtualEnd < directive.VirtualStart) {
            return (default, new DiagnosticDirectiveErrorжerror(directiveError(DiagnosticDirectiveErrorKindInvalidRange)));
        }
        var (virtualStart, err) = virtualPositions.normalize(directive.VirtualStart);
        if (err != nil) {
            return (default, new DiagnosticDirectiveErrorжerror(directiveError(DiagnosticDirectiveErrorKindInvalidRange)));
        }
        (var virtualEnd, err) = virtualPositions.normalize(directive.VirtualEnd);
        if (err != nil) {
            return (default, new DiagnosticDirectiveErrorжerror(directiveError(DiagnosticDirectiveErrorKindInvalidRange)));
        }
        normalized.VirtualRange = core.NewTextRange(virtualStart, virtualEnd);
        var validOriginalRange = directive.OriginalStart >= 0 && directive.OriginalLength >= 0 && directive.OriginalStart <= (nint)(~(nuint)0 >> 1) - directive.OriginalLength;
        if (validOriginalRange) {
            var (originalStart, startErr) = originalPositions.normalize(directive.OriginalStart);
            var (originalEnd, endErr) = originalPositions.normalize(directive.OriginalStart + directive.OriginalLength);
            if (startErr == nil && endErr == nil) {
                normalized.OriginalRange = core.NewTextRange(originalStart, originalEnd);
            } else {
                validOriginalRange = false;
            }
        }
        if (normalized.Policy == ast.MappedDiagnosticDirectivePolicyExpect && !validOriginalRange) {
            return (default, new DiagnosticDirectiveErrorжerror(directiveError(DiagnosticDirectiveErrorKindInvalidRange)));
        }
        result[i] = normalized;
    }
    var sorted = new slice<normalizeDiagnosticDirectives_indexedDirective>(len(result));
    foreach (var (i, directive) in range(result)) {
        sorted[i] = new normalizeDiagnosticDirectives_indexedDirective(directive: directive, index: i);
    }
    slices.SortFunc(sorted, (normalizeDiagnosticDirectives_indexedDirective a, normalizeDiagnosticDirectives_indexedDirective b) => cmp.Compare(a.directive.VirtualRange.Pos(), b.directive.VirtualRange.Pos()));
    for (nint i = 1; i < len(sorted); i++) {
        if (sorted[i].directive.VirtualRange.Pos() < sorted[i - 1].directive.VirtualRange.End()) {
            return (default, new DiagnosticDirectiveErrorжerror(Ꮡ(new DiagnosticDirectiveError(Kind: DiagnosticDirectiveErrorKindOverlap, Index: sorted[i].index, SupplementalIndex: -1))));
        }
    }
    return (result, default);
}

internal static (ж<spanmap.SpanMap>, error) normalizeMappings(ж<spanmap.SpanMap> mappings, ж<positionNormalizer> ᏑvirtualPositions, ж<positionNormalizer> ᏑoriginalPositions) {
    ref var virtualPositions = ref ᏑvirtualPositions.DerefOrNull();
    ref var originalPositions = ref ᏑoriginalPositions.DerefOrNull();

    var segments = mappings.Segments();
    foreach (var i in range(len(segments))) {
        var segment = Ꮡ(segments, i);
        error err = default;
        (segment.VirtualStart, err) = virtualPositions.normalizeTextPos(segment.VirtualStart);
        if (err != nil) {
            return (default, fmt.Errorf("invalid content mapper mapping %d virtual start: %w"u8, i, err));
        }
        (segment.VirtualEnd, err) = virtualPositions.normalizeTextPos(segment.VirtualEnd);
        if (err != nil) {
            return (default, fmt.Errorf("invalid content mapper mapping %d virtual end: %w"u8, i, err));
        }
        (segment.OriginalStart, err) = originalPositions.normalizeTextPos(segment.OriginalStart);
        if (err != nil) {
            return (default, fmt.Errorf("invalid content mapper mapping %d original start: %w"u8, i, err));
        }
        (segment.OriginalEnd, err) = originalPositions.normalizeTextPos(segment.OriginalEnd);
        if (err != nil) {
            return (default, fmt.Errorf("invalid content mapper mapping %d original end: %w"u8, i, err));
        }
    }
    return (spanmap.New(segments), default);
}

partial struct positionNormalizer {
    internal @string text;
    internal PositionEncoding encoding;
    internal ж<ast.PositionMap> positionMap;
    internal nint length;
}

internal static (ж<positionNormalizer>, error) newPositionNormalizer(@string text, PositionEncoding encoding) {
    var normalizer = Ꮡ(new positionNormalizer(text: text, encoding: encoding));
    switch (ᐧ) {
    case {} when encoding == PositionEncodingUTF8: {
        normalizer.length = len(text);
        break;
    }
    case {} when encoding == PositionEncodingUTF16: {
        normalizer.positionMap = ast.ComputePositionMap(text);
        normalizer.length = normalizer.positionMap.UTF8ToUTF16(len(text));
        break;
    }
    default: {
        return (default, fmt.Errorf("unsupported position encoding %q"u8, encoding));
    }}

    return (normalizer, default);
}

} partial class contentmapperᴛpositionNormalizer {

internal static (core.TextPos, error) normalizeTextPos(this ref positionNormalizer n, core.TextPos position) {
    var (normalized, err) = n.normalize((nint)position);
    return ((core.TextPos)(int32)normalized, err);
}

internal static (nint, error) normalize(this ref positionNormalizer n, nint position) {
    if (position < 0) {
        return (0, fmt.Errorf("position %d is negative"u8, position));
    }
    if (position > n.length) {
        return (0, fmt.Errorf("position %d exceeds %s length %d"u8, position, n.encoding, n.length));
    }
    nint bytePosition = default;
    var exprᴛ1 = n.encoding;
    switch (ᐧ) {
    case {} when exprᴛ1 == PositionEncodingUTF8: {
        bytePosition = position;
        break;
    }
    case {} when exprᴛ1 == PositionEncodingUTF16: {
        bytePosition = n.positionMap.UTF16ToUTF8(position);
        break;
    }}

    if (bytePosition < len(n.text) && !utf8.RuneStart(n.text[bytePosition])) {
        return (0, fmt.Errorf("position %d splits a Unicode code point"u8, position));
    }
    return (bytePosition, default);
}

} partial class contentmapper_package {

partial struct rejectHandler {
}

} partial class contentmapperᴛrejectHandler {

internal static (any, error) HandleRequest(this rejectHandler _, context.Context ctx, @string method, jsontext.Value @params) {
    return (default, fmt.Errorf("content mapper sent an unexpected request: %s"u8, method));
}

internal static error HandleNotification(this rejectHandler _, context.Context ctx, @string method, jsontext.Value @params) {
    return default;
}

} // end contentmapperᴛrejectHandler
