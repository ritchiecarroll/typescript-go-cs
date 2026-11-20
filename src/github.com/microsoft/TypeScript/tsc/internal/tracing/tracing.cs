namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using maps = maps_package;
using math = math_package;
using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using time = time_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using xxh3 = go.github.com.zeebo.xxh3_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.zeebo;
using go.sync;
using io = io_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static tracing_package;

partial class tracing_package {

partial interface Tracer {
    void RecordType(TracedType t);
    error DumpTypes();
}

partial interface TracedType {
    uint32 Id();
    slice<@string> FormatFlags();
    bool IsConditional();
    ж<ast.Symbol> Symbol();
    ж<ast.Symbol> AliasSymbol();
    slice<TracedType> AliasTypeArguments();
    @string IntrinsicName();
    slice<TracedType> UnionTypes();
    slice<TracedType> IntersectionTypes();
    TracedType IndexType();
    TracedType IndexedAccessObjectType();
    TracedType IndexedAccessIndexType();
    TracedType ConditionalCheckType();
    TracedType ConditionalExtendsType();
    TracedType ConditionalTrueType();
    TracedType ConditionalFalseType();
    TracedType SubstitutionBaseType();
    TracedType SubstitutionConstraintType();
    TracedType ReferenceTarget();
    slice<TracedType> ReferenceTypeArguments();
    ж<ast.Node> ReferenceNode();
    TracedType ReverseMappedSourceType();
    TracedType ReverseMappedMappedType();
    TracedType ReverseMappedConstraintType();
    TracedType EvolvingArrayElementType();
    TracedType EvolvingArrayFinalType();
    bool IsTuple();
    ж<ast.Node> Pattern();
    any RecursionIdentity();
    @string Display();
}

partial struct TraceRecord {
    public @string ConfigFilePath; /*`json:"configFilePath,omitzero"`*/
    public @string TracePath; /*`json:"tracePath,omitzero"`*/
    public @string TypesPath; /*`json:"typesPath,omitzero"`*/
    public nint CheckerID; /*`json:"checkerId"`*/
}

partial struct traceEvent {
    public nint PID; /*`json:"pid"`*/
    public nint TID; /*`json:"tid"`*/
    public @string PH; /*`json:"ph"`*/
    public @string Cat; /*`json:"cat"`*/
    public float64 TS; /*`json:"ts"`*/
    public @string Name; /*`json:"name,omitzero"`*/
    public @string S; /*`json:"s,omitzero"`*/
    public ж<float64> Dur; /*`json:"dur,omitzero"`*/
    public map<@string, any> Args; /*`json:"args,omitzero"`*/
}

internal static time.Duration sampleInterval => /* 10 * time.Millisecond */ 10000000;

internal static readonly @string traceFileName = "trace.json"u8;

internal static UntypedInt mainThreadID => 1;
internal static UntypedInt firstSyntheticThreadID => 2;
internal static UntypedInt firstFileThreadID => 1_000_000;
internal static UntypedInt fileThreadIDHashRange => 1_000_000_000;

internal static array<@string> traceThreadArgKeys = new @string[]{"path", "fileName", "containingFileName", "jsFilePath", "declarationFilePath"}.array();

internal static UntypedInt flushThreshold => /* 256 * 1024 */ 262144;

partial struct Tracing {
    internal vfs.FS fs;
    internal @string traceDir;
    internal @string tracePath;
    internal @string configFilePath;
    internal slice<TraceRecord> legend;
    internal slice<ж<typeTracer>> tracers;
    internal strings.Builder traceContent;
    internal atomic.Bool traceStarted;
    internal map<traceThreadKey, nint> threadIDs;
    internal map<nint, traceThreadKey> threadKeys;
    internal float64 metadataTS;
    internal bool deterministic;
    internal uint64 timestampCounter;
    internal time.Time startTime;
    internal sync.Mutex mu;
    internal error flushErr;
}

partial struct Phase /*@string*/;

public static readonly Phase PhaseParse = "parse"u8;
public static readonly Phase PhaseProgram = "program"u8;
public static readonly Phase PhaseBind = "bind"u8;
public static readonly Phase PhaseCheck = "check"u8;
public static readonly Phase PhaseCheckTypes = "checkTypes"u8;
public static readonly Phase PhaseEmit = "emit"u8;
public static readonly Phase PhaseSession = "session"u8;

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly object tsgoˢ = (@string)"tsgo"u8;
internal static readonly object mainˢ = (@string)"Main"u8;

public static (ж<Tracing>, error) StartTracing(vfs.FS fs, @string traceDir, @string configFilePath, bool deterministic) {
    var tr = Ꮡ(new Tracing(
        fs: fs,
        traceDir: traceDir,
        tracePath: tspath.CombinePaths(traceDir, traceFileName),
        configFilePath: configFilePath,
        legend: [],
        tracers: [],
        threadIDs: new map<traceThreadKey, nint>(),
        threadKeys: new map<nint, traceThreadKey>(),
        deterministic: deterministic,
        startTime: time.Now()
    ));
    tr.ᏑtraceStarted.Store(true);

    tr.ᏑtraceContent.WriteString("[\n");

    var metaTs = tr.timestamp();
    tr.metadataTS = metaTs;
    tr.writeEvent(new traceEvent(PID: 1, TID: mainThreadID, PH: "M", Cat: "__metadata", TS: metaTs, Name: "process_name", Args: new map<@string, any>{["name"] = tsgoˢ}));
    tr.ᏑtraceContent.WriteString(",\n");
    tr.writeEvent(new traceEvent(PID: 1, TID: mainThreadID, PH: "M", Cat: "__metadata", TS: metaTs, Name: "thread_name", Args: new map<@string, any>{["name"] = mainˢ}));
    tr.ᏑtraceContent.WriteString(",\n");
    tr.writeEvent(new traceEvent(PID: 1, TID: mainThreadID, PH: "M", Cat: "disabled-by-default-devtools.timeline", TS: metaTs, Name: "TracingStartedInBrowser"));

    var err = tr.fs.WriteFile(tr.tracePath, tr.ᏑtraceContent.String()); if (err != nil) {
        return (default, fmt.Errorf("failed to write trace file header: %w"u8, err));
    }
    tr.ᏑtraceContent.Reset();

    return (tr, default);
}

} partial class tracingᴛTracing {

internal static float64 timestamp(this ref Tracing tr) {
    if (tr.deterministic) {
        tr.timestampCounter++;
        return (float64)tr.timestampCounter;
    }
    return (float64)time.Since(tr.startTime).Nanoseconds() / 1000.0D;
}

} partial class tracing_package {

internal static void writeEventTo(ж<strings.Builder> buf, traceEvent @event) {
    var err = json.MarshalWrite(new strings_BuilderжWriter(buf), @event, json.Deterministic(true)); if (err != nil) {
        throw panic(fmt.Sprintf("failed to marshal trace event: %v"u8, err));
    }
}

} partial class tracingᴛTracing {

internal static void writeEvent(this ж<Tracing> Ꮡtr, traceEvent @event) {
    writeEventTo(Ꮡtr.ᏑtraceContent, @event);
}

internal static void maybeFlushLocked(this ref Tracing tr) {
    if (tr.flushErr != nil) {
        tr.traceContent.Reset();
        return;
    }
    if (tr.traceContent.Len() < flushThreshold) {
        return;
    }
    var err = tr.fs.AppendFile(tr.tracePath, tr.traceContent.String()); if (err != nil) {
        tr.flushErr = fmt.Errorf("failed to flush trace file: %w"u8, err);
    }
    tr.traceContent.Reset();
}

public static void Instant(this ж<Tracing> Ꮡtr, Phase phase, @string name, map<@string, any> args) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var tr = ref Ꮡtr.DerefOrNull();

        if (Ꮡtr == nil || !Ꮡtr.ᏑtraceStarted.Load()) {
            return;
        }

        tr.mu.Lock();
        ᒐd1 = true;

        if (!Ꮡtr.ᏑtraceStarted.Load()) {
            return;
        }

        var ts = tr.timestamp();
        nint tid = Ꮡtr.threadIDLocked(args);
        Ꮡtr.ᏑtraceContent.WriteString(",\n");
        Ꮡtr.writeEvent(new traceEvent(PID: 1, TID: tid, PH: "I", Cat: ((@string)phase), TS: ts, Name: name, S: "g", Args: args));
        tr.maybeFlushLocked();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡtr.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

public static Action Push(this ж<Tracing> Ꮡtr, Phase phase, @string name, map<@string, any> args, bool separateBeginAndEnd) {
    ref var tr = ref Ꮡtr.DerefOrNull();

    if (Ꮡtr == nil || !Ꮡtr.ᏑtraceStarted.Load()) {
        return () => {
        };
    }

    if (separateBeginAndEnd) {
        tr.mu.Lock();
        if (!Ꮡtr.ᏑtraceStarted.Load()) {
            tr.mu.Unlock();
            return () => {
            };
        }
        var ts = tr.timestamp();
        nint tid = Ꮡtr.threadIDLocked(args);
        Ꮡtr.ᏑtraceContent.WriteString(",\n");
        Ꮡtr.writeEvent(new traceEvent(PID: 1, TID: tid, PH: "B", Cat: ((@string)phase), TS: ts, Name: name, Args: args));
        tr.maybeFlushLocked();
        tr.mu.Unlock();

        return () => {
            GoFrame ᒐ = default;
            try {
                Ꮡtr.Value.mu.Lock();
                defer(Ꮡtr.Ꮡmu.Unlock, ref ᒐ);
                if (!Ꮡtr.ᏑtraceStarted.Load()) {
                    return;
                }
                var endTs = Ꮡtr.Value.timestamp();
                Ꮡtr.ᏑtraceContent.WriteString(",\n");
                Ꮡtr.writeEvent(new traceEvent(PID: 1, TID: tid, PH: "E", Cat: ((@string)phase), TS: endTs, Name: name, Args: args));
                Ꮡtr.Value.maybeFlushLocked();
            }
            catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
            finally { ᒐ.Run(); }
        };
    }

    if (tr.deterministic) {
        return () => {
        };
    }
    ref var startTime = ref heap<time.Time>(time.Now(), out var ᏑstartTime);
    args = maps.Clone<map<@string, any>, @string, any>(args);
    var startTimeʗ1 = startTime;
    return () => {
        GoFrame ᒐ = default;
        try {
            ref var dur = ref heap<float64>((float64)time.Since(startTimeʗ1).Nanoseconds() / 1000.0D, out var Ꮡdur);
            var startMicros = (float64)startTimeʗ1.Sub(Ꮡtr.Value.startTime).Nanoseconds() / 1000.0D;
            var intervalMicros = (float64)sampleInterval.Nanoseconds() / 1000.0D;
            if (intervalMicros - math.Mod(startMicros, intervalMicros) > dur) {
                return;
            }
            Ꮡtr.Value.mu.Lock();
            defer(Ꮡtr.Ꮡmu.Unlock, ref ᒐ);
            if (!Ꮡtr.ᏑtraceStarted.Load()) {
                return;
            }
            nint tid = Ꮡtr.threadIDLocked(args);
            Ꮡtr.ᏑtraceContent.WriteString(",\n");
            Ꮡtr.writeEvent(new traceEvent(PID: 1, TID: tid, PH: "X", Cat: ((@string)phase), TS: startMicros, Name: name, Dur: Ꮡdur, Args: args));
            Ꮡtr.Value.maybeFlushLocked();
        }
        catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
        finally { ᒐ.Run(); }
    };
}

internal static nint threadIDLocked(this ж<Tracing> Ꮡtr, map<@string, any> args) {
    ref var tr = ref Ꮡtr.DerefOrNull();

    var (key, ok) = traceThreadKeyFromArgs(args);
    if (!ok) {
        return mainThreadID;
    }

    var (tidΔ1, okΔ1) = tr.threadIDs[key, ꟷ]; if (okΔ1) {
        return tidΔ1;
    }

    nint tid = key.defaultThreadID();
    for (;;) {
        var (existingKey, okΔ2) = tr.threadKeys[tid, ꟷ]; if (!okΔ2 || existingKey == key) {
            break;
        }
        tid++;
    }
    tr.threadIDs[key] = tid;
    tr.threadKeys[tid] = key;
    Ꮡtr.writeThreadNameEventLocked(tid, key.displayName());
    return tid;
}

internal static void writeThreadNameEventLocked(this ж<Tracing> Ꮡtr, nint tid, @string name) {
    ref var tr = ref Ꮡtr.DerefOrNull();

    Ꮡtr.ᏑtraceContent.WriteString(",\n");
    Ꮡtr.writeEvent(new traceEvent(PID: 1, TID: tid, PH: "M", Cat: "__metadata", TS: tr.metadataTS, Name: "thread_name", Args: new map<@string, any>{["name"] = name}));
}

} partial class tracing_package {

partial struct traceThreadKind /*@string*/;

internal static readonly traceThreadKind traceThreadKindChecker = "checker"u8;
internal static readonly traceThreadKind traceThreadKindFile = "file"u8;

partial struct traceThreadKey {
    internal traceThreadKind kind;
    internal @string text;
    internal nint index;
    internal bool hasIndex;
}

internal static (traceThreadKey, bool) traceThreadKeyFromArgs(map<@string, any> args) {
    if (len(args) == 0) {
        return (new traceThreadKey(), false);
    }

    if (args["checkerId"]._<nint>(out var checkerID)) {
        return (new traceThreadKey(kind: traceThreadKindChecker, index: checkerID, hasIndex: true), true);
    }

    foreach (var (_, key) in range(traceThreadArgKeys.ΔRangeSnapshot())) {
        var (value, ok) = args[key, ꟷ]; if (ok) {
            var (path, okΔ1) = value._<@string>(ᐧ); if (okΔ1 && path != ""u8) {
                return (new traceThreadKey(kind: traceThreadKindFile, text: path), true);
            }
        }
    }

    return (new traceThreadKey(), false);
}

} partial class tracingᴛtraceThreadKey {

internal static nint defaultThreadID(this traceThreadKey key) {
    if (key.kind == traceThreadKindChecker && key.hasIndex && key.index >= 0) {
        return (nint)firstSyntheticThreadID + key.index;
    }
    return stableTraceThreadID(key);
}

internal static @string displayName(this traceThreadKey key) {
    if (key.hasIndex) {
        return ((@string)key.kind) + ":"u8 + strconv.Itoa(key.index);
    }
    return ((@string)key.kind) + ":"u8 + key.text;
}

} partial class tracing_package {

internal static nint stableTraceThreadID(traceThreadKey key) {
    var hash = xxh3.New();
    (_, _) = hash.WriteString((@string)key.kind);
    (_, _) = hash.WriteString(":");
    if (key.hasIndex) {
        (_, _) = hash.WriteString(strconv.Itoa(key.index));
    } else {
        (_, _) = hash.WriteString(key.text);
    }
    return (nint)firstFileThreadID + (nint)(hash.Sum64() % (uint64)fileThreadIDHashRange);
}

} partial class tracingᴛTracing {

public static Tracer NewTypeTracer(this ж<Tracing> Ꮡtr, nint checkerIndex) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var tr = ref Ꮡtr.DerefOrNull();

        tr.mu.Lock();
        ᒐd1 = true;

        ref var typesPath = ref heap<@string>(tspath.CombinePaths(tr.traceDir, fmt.Sprintf("types_%d.json"u8, checkerIndex)), out var ᏑtypesPath);
        var tracer = Ꮡ(new typeTracer(
            fs: tr.fs,
            checkerIndex: checkerIndex,
            typesPath: typesPath,
            types: []
        ));
        tr.tracers = append(tr.tracers, tracer);
        tr.legend = append(tr.legend, new TraceRecord(
            ConfigFilePath: tr.configFilePath,
            TracePath: tr.tracePath,
            TypesPath: typesPath,
            CheckerID: checkerIndex
        ));
        return new typeTracerжTracer(tracer);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡtr.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

public static error StopTracing(this ж<Tracing> Ꮡtr) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var tr = ref Ꮡtr.DerefOrNull();

        foreach (var (_, tracer) in range(tr.tracers)) {
            var errΔ1 = tracer.DumpTypes(); if (errΔ1 != nil) {
                return fmt.Errorf("failed to dump types for checker %d: %w"u8, tracer.checkerIndex, errΔ1);
            }
        }

        tr.mu.Lock();
        ᒐd1 = true;

        if (Ꮡtr.ᏑtraceStarted.Load()) {
            if (tr.flushErr != nil) {
                tr.traceContent.Reset();
                Ꮡtr.ᏑtraceStarted.Store(false);
                return tr.flushErr;
            }
            var errΔ2 = tr.fs.AppendFile(tr.tracePath, tr.traceContent.String() + "\n]\n"u8); if (errΔ2 != nil) {
                return fmt.Errorf("failed to write trace file: %w"u8, errΔ2);
            }
            tr.traceContent.Reset();
            Ꮡtr.ᏑtraceStarted.Store(false);
        }

        slices.SortFunc(tr.legend, (TraceRecord a, TraceRecord b) => strings.Compare(a.TypesPath, b.TypesPath));

        @string legendPath = tspath.CombinePaths(tr.traceDir, "legend.json");
        var (legendData, err) = json.MarshalIndent(tr.legend, "", "  ");
        if (err != nil) {
            return fmt.Errorf("failed to marshal legend file: %w"u8, err);
        }
        var errΔ3 = tr.fs.WriteFile(legendPath, (@string)legendData); if (errΔ3 != nil) {
            return fmt.Errorf("failed to write legend file: %w"u8, errΔ3);
        }

        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡtr.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

} partial class tracing_package {

partial struct typeTracer {
    internal vfs.FS fs;
    internal nint checkerIndex;
    internal @string typesPath;
    internal slice<TracedType> types;
    internal sync.Mutex mu;
}

} partial class tracingᴛtypeTracer {

internal static void RecordType(this ж<typeTracer> Ꮡt, TracedType typ) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var t = ref Ꮡt.DerefOrNull();

        t.mu.Lock();
        ᒐd1 = true;
        t.types = append(t.types, typ);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡt.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static error DumpTypes(this ж<typeTracer> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    t.mu.Lock();
    var types = slices.Clone<slice<TracedType>, TracedType>(t.types);
    t.mu.Unlock();

    if (len(types) == 0) {
        return default;
    }

    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    Ꮡsb.WriteString("[");

    var recursionIdentityMap = new map<any, nint>();

    foreach (var (i, typ) in range(types)) {
        var descriptor = t.buildTypeDescriptor(typ, recursionIdentityMap);

        var err = json.MarshalWrite(new strings_BuilderжWriter(Ꮡsb), descriptor); if (err != nil) {
            return fmt.Errorf("failed to marshal type %d: %w"u8, typ.Id(), err);
        }

        if (i < len(types) - 1) {
            Ꮡsb.WriteString(",\n");
        }
    }

    Ꮡsb.WriteString("]\n");

    return t.fs.WriteFile(t.typesPath, sb.String());
}

} partial class tracing_package {

partial struct TypeDescriptor {
    public uint32 ID; /*`json:"id"`*/
    public @string IntrinsicName; /*`json:"intrinsicName,omitzero"`*/
    public @string SymbolName; /*`json:"symbolName,omitzero"`*/
    public ж<nint> RecursionID; /*`json:"recursionId,omitzero"`*/
    public bool IsTuple; /*`json:"isTuple,omitzero"`*/
    public slice<uint32> UnionTypes; /*`json:"unionTypes,omitzero"`*/
    public slice<uint32> IntersectionTypes; /*`json:"intersectionTypes,omitzero"`*/
    public slice<uint32> AliasTypeArguments; /*`json:"aliasTypeArguments,omitzero"`*/
    public ж<uint32> KeyofType; /*`json:"keyofType,omitzero"`*/
    public ж<uint32> IndexedAccessObjectType; /*`json:"indexedAccessObjectType,omitzero"`*/
    public ж<uint32> IndexedAccessIndexType; /*`json:"indexedAccessIndexType,omitzero"`*/
    public ж<uint32> ConditionalCheckType; /*`json:"conditionalCheckType,omitzero"`*/
    public ж<uint32> ConditionalExtendsType; /*`json:"conditionalExtendsType,omitzero"`*/
    public ж<int32> ConditionalTrueType; /*`json:"conditionalTrueType,omitzero"`*/
    public ж<int32> ConditionalFalseType; /*`json:"conditionalFalseType,omitzero"`*/
    public ж<uint32> SubstitutionBaseType; /*`json:"substitutionBaseType,omitzero"`*/
    public ж<uint32> ConstraintType; /*`json:"constraintType,omitzero"`*/
    public ж<uint32> InstantiatedType; /*`json:"instantiatedType,omitzero"`*/
    public slice<uint32> TypeArguments; /*`json:"typeArguments,omitzero"`*/
    public ж<Location> ReferenceLocation; /*`json:"referenceLocation,omitzero"`*/
    public ж<uint32> ReverseMappedSourceType; /*`json:"reverseMappedSourceType,omitzero"`*/
    public ж<uint32> ReverseMappedMappedType; /*`json:"reverseMappedMappedType,omitzero"`*/
    public ж<uint32> ReverseMappedConstraintType; /*`json:"reverseMappedConstraintType,omitzero"`*/
    public ж<uint32> EvolvingArrayElementType; /*`json:"evolvingArrayElementType,omitzero"`*/
    public ж<uint32> EvolvingArrayFinalType; /*`json:"evolvingArrayFinalType,omitzero"`*/
    public ж<Location> DestructuringPattern; /*`json:"destructuringPattern,omitzero"`*/
    public ж<Location> FirstDeclaration; /*`json:"firstDeclaration,omitzero"`*/
    public slice<@string> Flags; /*`json:"flags"`*/
    public @string Display; /*`json:"display,omitzero"`*/
}

partial struct Location {
    public @string Path; /*`json:"path"`*/
    public ж<LineAndChar> Start; /*`json:"start,omitzero"`*/
    public ж<LineAndChar> End; /*`json:"end,omitzero"`*/
}

partial struct LineAndChar {
    public nint Line; /*`json:"line"`*/
    public nint Character; /*`json:"character"`*/
}

} partial class tracingᴛtypeTracer {

internal static TypeDescriptor buildTypeDescriptor(this ref typeTracer t, TracedType typ, map<any, nint> recursionIdentityMap) {
    var symbol = typ.Symbol();
    var aliasSymbol = typ.AliasSymbol();

    var desc = new TypeDescriptor(
        ID: typ.Id(),
        Flags: typ.FormatFlags()
    );

    var identity = typ.RecursionIdentity(); if (identity != nil) {
        ref var token = ref heap<nint>(out var Ꮡtoken);
        (token, var ok) = recursionIdentityMap[identity, ꟷ];
        if (!ok) {
            token = len(recursionIdentityMap);
            recursionIdentityMap[identity] = token;
        }
        desc.RecursionID = Ꮡtoken;
    }

    @string name = typ.IntrinsicName(); if (name != ""u8) {
        desc.IntrinsicName = name;
    }

    var sym = aliasSymbol; if (sym != nil) {
        desc.SymbolName = ast.EscapeAllInternalSymbolNames(sym.Name);
    } else if (symbol != nil) {
        desc.SymbolName = ast.EscapeAllInternalSymbolNames(symbol.Name);
    }

    if (typ.IsTuple()) {
        desc.IsTuple = true;
    }

    {
        var types = typ.UnionTypes(); if (len(types) > 0) {
            desc.UnionTypes = mapTypeIds(types);
        }
    }

    {
        var types = typ.IntersectionTypes(); if (len(types) > 0) {
            desc.IntersectionTypes = mapTypeIds(types);
        }
    }

    {
        var args = typ.AliasTypeArguments(); if (len(args) > 0) {
            desc.AliasTypeArguments = mapTypeIds(args);
        }
    }

    var indexType = typ.IndexType(); if (indexType != nil) {
        desc.KeyofType = Ꮡ(indexType.Id());
    }

    var objType = typ.IndexedAccessObjectType(); if (objType != nil) {
        desc.IndexedAccessObjectType = Ꮡ(objType.Id());
    }
    var idxType = typ.IndexedAccessIndexType(); if (idxType != nil) {
        desc.IndexedAccessIndexType = Ꮡ(idxType.Id());
    }

    if (typ.IsConditional()) {
        var checkType = typ.ConditionalCheckType(); if (checkType != nil) {
            desc.ConditionalCheckType = Ꮡ(checkType.Id());
        }
        var extendsType = typ.ConditionalExtendsType(); if (extendsType != nil) {
            desc.ConditionalExtendsType = Ꮡ(extendsType.Id());
        }
        var trueType = typ.ConditionalTrueType(); if (trueType != nil) {
            desc.ConditionalTrueType = Ꮡ((int32)trueType.Id());
        } else {
            desc.ConditionalTrueType = Ꮡ((int32)(-1));
        }
        var falseType = typ.ConditionalFalseType(); if (falseType != nil) {
            desc.ConditionalFalseType = Ꮡ((int32)falseType.Id());
        } else {
            desc.ConditionalFalseType = Ꮡ((int32)(-1));
        }
    }

    var baseType = typ.SubstitutionBaseType(); if (baseType != nil) {
        desc.SubstitutionBaseType = Ꮡ(baseType.Id());
    }
    var constraint = typ.SubstitutionConstraintType(); if (constraint != nil) {
        desc.ConstraintType = Ꮡ(constraint.Id());
    }

    var target = typ.ReferenceTarget(); if (target != nil) {
        desc.InstantiatedType = Ꮡ(target.Id());
    }
    {
        var args = typ.ReferenceTypeArguments(); if (len(args) > 0) {
            desc.TypeArguments = mapTypeIds(args);
        }
    }
    var node = typ.ReferenceNode(); if (node != nil) {
        desc.ReferenceLocation = getLocation(node);
    }

    var sourceType = typ.ReverseMappedSourceType(); if (sourceType != nil) {
        desc.ReverseMappedSourceType = Ꮡ(sourceType.Id());
    }
    var mappedType = typ.ReverseMappedMappedType(); if (mappedType != nil) {
        desc.ReverseMappedMappedType = Ꮡ(mappedType.Id());
    }
    var constraintType = typ.ReverseMappedConstraintType(); if (constraintType != nil) {
        desc.ReverseMappedConstraintType = Ꮡ(constraintType.Id());
    }

    var elemType = typ.EvolvingArrayElementType(); if (elemType != nil) {
        desc.EvolvingArrayElementType = Ꮡ(elemType.Id());
    }
    var finalType = typ.EvolvingArrayFinalType(); if (finalType != nil) {
        desc.EvolvingArrayFinalType = Ꮡ(finalType.Id());
    }

    var pattern = typ.Pattern(); if (pattern != nil) {
        desc.DestructuringPattern = getLocation(pattern);
    }

    var firstDeclSymbol = aliasSymbol;
    if (firstDeclSymbol == nil) {
        firstDeclSymbol = symbol;
    }
    if (firstDeclSymbol != nil && len(firstDeclSymbol.Declarations) > 0) {
        desc.FirstDeclaration = getLocation(firstDeclSymbol.Declarations[0]);
    }

    @string display = typ.Display(); if (display != ""u8) {
        desc.Display = display;
    }

    return desc;
}

} partial class tracing_package {

internal static slice<uint32> mapTypeIds(slice<TracedType> types) {
    if (len(types) == 0) {
        return default;
    }
    var ids = new slice<uint32>(len(types));
    foreach (var (i, t) in range(types)) {
        if (t != nil) {
            ids[i] = t.Id();
        }
    }
    return ids;
}

internal static ж<Location> getLocation(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return default;
    }
    var @file = ast.GetSourceFileOfNode(Ꮡnode);
    if (@file == nil) {
        return default;
    }

    nint startPos = scanner.GetTokenPosOfNode(Ꮡnode, @file, false);
    var (startLine, startChar) = scanner.GetECMALineAndUTF16CharacterOfPosition(new ast.SourceFileжSourceFileLike(@file), startPos);
    var (endLine, endChar) = scanner.GetECMALineAndUTF16CharacterOfPosition(new ast.SourceFileжSourceFileLike(@file), node.End());

    return Ꮡ(new Location(
        Path: ((@string)tspath.ToPath(@file.FileName(), "", false)),
        Start: Ꮡ(new LineAndChar(
            Line: startLine + 1,
            Character: (nint)startChar + 1
        )),
        End: Ꮡ(new LineAndChar(
            Line: endLine + 1,
            Character: (nint)endChar + 1
        ))
    ));
}

} // end tracing_package
