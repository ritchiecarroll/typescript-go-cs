namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using time = time_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using io = io_package;
using static contentmapper_package;

partial class contentmapper_package {

partial struct TransformErrorKind /*num:uint8*/;

public static TransformErrorKind TransformErrorKindUnknown => /* iota */ 0;
public static TransformErrorKind TransformErrorKindInitialize => 1;
public static TransformErrorKind TransformErrorKindProject => 2;
public static TransformErrorKind TransformErrorKindRequest => 3;
public static TransformErrorKind TransformErrorKindResponse => 4;
public static TransformErrorKind TransformErrorKindMappings => 5;

partial struct TransformError {
    public TransformErrorKind Kind;
    internal error err;
}

public static ж<TransformError> NewTransformError(TransformErrorKind kind, error err) {
    return Ꮡ(new TransformError(Kind: kind, err: err));
}

} partial class contentmapperᴛTransformError {

public static @string Error(this ref TransformError e) {
    return fmt.Sprintf("content mapper transform failed: %v"u8, e.err);
}

public static error Unwrap(this ref TransformError e) { return e.err; }

} partial class contentmapper_package {

partial struct DiagnosticDirectiveErrorKind /*num:uint8*/;

public static DiagnosticDirectiveErrorKind DiagnosticDirectiveErrorKindInvalidRange => /* iota */ 0;
public static DiagnosticDirectiveErrorKind DiagnosticDirectiveErrorKindInvalidPolicy => 1;
public static DiagnosticDirectiveErrorKind DiagnosticDirectiveErrorKindExpectMissingUnusedDiagnostic => 2;
public static DiagnosticDirectiveErrorKind DiagnosticDirectiveErrorKindInvalidUnusedDiagnosticIndex => 3;
public static DiagnosticDirectiveErrorKind DiagnosticDirectiveErrorKindOverlap => 4;

partial struct DiagnosticDirectiveError {
    public DiagnosticDirectiveErrorKind Kind;
    public nint Index;
    public nint SupplementalIndex;
    public DiagnosticDirectivePolicy Policy;
}

} partial class contentmapperᴛDiagnosticDirectiveError {

public static @string Error(this ref DiagnosticDirectiveError e) {
    return fmt.Sprintf("invalid content mapper diagnostic directive %d"u8, e.Index);
}

} partial class contentmapper_package {

partial struct InvalidVirtualExtensionError {
    public @string Extension;
}

} partial class contentmapperᴛInvalidVirtualExtensionError {

public static @string Error(this ref InvalidVirtualExtensionError e) {
    return fmt.Sprintf("invalid virtual extension %q"u8, e.Extension);
}

} partial class contentmapper_package {

partial struct ProjectErrorKind /*num:uint8*/;

public static ProjectErrorKind ProjectErrorKindMalformedResponse => /* iota */ 0;
public static ProjectErrorKind ProjectErrorKindMissingConfigIdentity => 1;
public static ProjectErrorKind ProjectErrorKindNonAbsoluteWatchedFile => 2;
public static ProjectErrorKind ProjectErrorKindUnexpectedConfigIdentity => 3;
public static ProjectErrorKind ProjectErrorKindUnexpectedWatchedFiles => 4;

partial struct ProjectError {
    public ProjectErrorKind Kind;
}

} partial class contentmapperᴛProjectError {

public static @string Error(this ref ProjectError e) {
    switch (e.Kind) {
    case var ᴛ when ᴛ == ProjectErrorKindMalformedResponse: {
        return "content mapper returned a malformed project response";
    }
    case var ᴛ when ᴛ == ProjectErrorKindMissingConfigIdentity: {
        return "content mapper did not return configIdentity for dynamic configuration";
    }
    case var ᴛ when ᴛ == ProjectErrorKindNonAbsoluteWatchedFile: {
        return "content mapper returned a non-absolute path in watchedFiles";
    }
    case var ᴛ when ᴛ == ProjectErrorKindUnexpectedConfigIdentity: {
        return "content mapper returned configIdentity without declaring dynamicConfig";
    }
    case var ᴛ when ᴛ == ProjectErrorKindUnexpectedWatchedFiles: {
        return "content mapper returned watchedFiles without declaring dynamicConfig";
    }
    default: {
        return "content mapper returned an invalid project response";
    }}

}

} partial class contentmapper_package {

partial struct InitializeErrorKind /*num:uint8*/;

public static InitializeErrorKind InitializeErrorKindProcessStart => /* iota */ 0;
public static InitializeErrorKind InitializeErrorKindProcessExit => 1;
public static InitializeErrorKind InitializeErrorKindNoResponse => 2;
public static InitializeErrorKind InitializeErrorKindInvalidResponse => 3;
public static InitializeErrorKind InitializeErrorKindRequest => 4;
public static InitializeErrorKind InitializeErrorKindPositionEncoding => 5;
public static InitializeErrorKind InitializeErrorKindEmptyDiagnosticSource => 6;
public static InitializeErrorKind InitializeErrorKindReservedDiagnosticSource => 7;

partial struct InitializeError {
    public InitializeErrorKind Kind;
    public @string MapperName;
    public @string Command;
    public @string Detail;
    public nint ExitCode;
    public nint TimeoutSeconds;
    public PositionEncoding PositionEncoding;
    public @string DiagnosticSource;
}

partial struct SupplementalFileCollisionError {
    public @string FileName;
}

} partial class contentmapperᴛSupplementalFileCollisionError {

public static @string Error(this ref SupplementalFileCollisionError e) {
    return fmt.Sprintf("content mapper supplemental output file %q already exists"u8, e.FileName);
}

} partial class contentmapperᴛInitializeError {

public static @string Error(this ref InitializeError e) {
    switch (e.Kind) {
    case var ᴛ when ᴛ == InitializeErrorKindProcessStart: {
        return fmt.Sprintf("could not start content mapper command %q: %s"u8, e.Command, e.Detail);
    }
    case var ᴛ when ᴛ == InitializeErrorKindProcessExit: {
        return fmt.Sprintf("content mapper process exited before initialization with code %d"u8, e.ExitCode);
    }
    case var ᴛ when ᴛ == InitializeErrorKindNoResponse: {
        return "content mapper did not respond to the initialize request";
    }
    case var ᴛ when ᴛ == InitializeErrorKindInvalidResponse: {
        return "content mapper returned an invalid initialize response: "u8 + e.Detail;
    }
    case var ᴛ when ᴛ == InitializeErrorKindRequest: {
        return "content mapper initialize request failed: "u8 + e.Detail;
    }
    case var ᴛ when ᴛ == InitializeErrorKindPositionEncoding: {
        return fmt.Sprintf("unsupported position encoding %q"u8, e.PositionEncoding);
    }
    case var ᴛ when ᴛ == InitializeErrorKindEmptyDiagnosticSource: {
        return "diagnostic source must not be empty";
    }
    case var ᴛ when ᴛ == InitializeErrorKindReservedDiagnosticSource: {
        return fmt.Sprintf("diagnostic source %q is reserved by TypeScript"u8, e.DiagnosticSource);
    }
    default: {
        return "content mapper initialization failed";
    }}

}

} partial class contentmapper_package {

partial struct Result {
    public @string Text;
    public @string VirtualExtension;
    public slice<ж<ast.Diagnostic>> Diagnostics;
    public ж<spanmap.SpanMap> Mappings;
    public slice<ast.MappedDiagnosticDirective> DiagnosticDirectives;
    public slice<MappedResult> Supplemental;
}

partial struct MappedResult {
    public @string Text;
    public @string VirtualExtension;
    public ж<spanmap.SpanMap> Mappings;
    public slice<ast.MappedDiagnosticDirective> DiagnosticDirectives;
}

partial struct Request {
    public @string FileName;
    public @string Content;
}

partial struct ProjectSpec {
    public @string ConfigFileName;
    public slice<ж<Mapper>> Mappers;
    public ж<core.CompilerOptions> CompilerOptions;
}

partial struct OptionPathSegment {
    public @string Property;
    public nint Index;
    public bool IsIndex;
}

partial struct OptionDiagnostic {
    public ж<Mapper> Mapper;
    public slice<OptionPathSegment> Path;
    public @string Source;
    public int32 Code;
    public @string MessageText;
}

partial struct OperationTiming {
    public uint64 Count;
    public time.Duration Duration;
}

partial struct MapperTimings {
    public OperationTiming Spawn;
    public OperationTiming Initialize;
    public OperationTiming OpenProject;
    public OperationTiming CloseProject;
    public OperationTiming Transform;
}

partial struct Timings {
    public map<@string, MapperTimings> Mappers;
    public time.Duration RequestWait;
}

} partial class contentmapperᴛTimings {

public static Timings Since(this Timings t, Timings previous) {
    var result = new Timings(
        Mappers: new map<@string, MapperTimings>(len(t.Mappers)),
        RequestWait: max(t.RequestWait - previous.RequestWait, (time.Duration)0)
    );
    foreach (var (identity, current) in t.Mappers) {
        var before = previous.Mappers[identity];
        result.Mappers[identity] = new MapperTimings(
            Spawn: operationTimingSince(current.Spawn, before.Spawn),
            Initialize: operationTimingSince(current.Initialize, before.Initialize),
            OpenProject: operationTimingSince(current.OpenProject, before.OpenProject),
            CloseProject: operationTimingSince(current.CloseProject, before.CloseProject),
            Transform: operationTimingSince(current.Transform, before.Transform)
        );
    }
    return result;
}

} partial class contentmapper_package {

internal static OperationTiming operationTimingSince(OperationTiming current, OperationTiming previous) {
    return new OperationTiming(
        Count: current.Count - min(current.Count, previous.Count),
        Duration: max(current.Duration - previous.Duration, (time.Duration)0)
    );
}

partial interface Project :
    io.Closer
{
    error Refresh();
    (slice<@string>, error) Identities();
    (@string, error) Identity(ж<Mapper> mapper);
    (slice<@string>, error) WatchedFiles();
    slice<OptionDiagnostic> Diagnostics();
    (Result result, error err) Transform(ж<Mapper> mapper, Request request);
}

partial interface Host :
    io.Closer
{
    Timings Timings();
    Project Project(ProjectSpec spec);
    Action /*release*/ Acquire(slice<ж<Mapper>> mappers);
    void SetLocale(locale.Locale locale);
    (Result result, error err) Transform(ж<Mapper> mapper, Request request);
}

} // end contentmapper_package
