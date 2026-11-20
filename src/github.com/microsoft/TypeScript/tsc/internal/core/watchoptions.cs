namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using time = time_package;
using static core_package;

partial class core_package {

partial struct WatchOptions {
    public ж<nint> Interval; /*`json:"watchInterval"`*/
    public WatchFileKind FileKind; /*`json:"watchFile"`*/
    public WatchDirectoryKind DirectoryKind; /*`json:"watchDirectory"`*/
    public PollingKind FallbackPolling; /*`json:"fallbackPolling"`*/
    public Tristate SyncWatchDir; /*`json:"synchronousWatchDirectory"`*/
    public slice<@string> ExcludeDir; /*`json:"excludeDirectories"`*/
    public slice<@string> ExcludeFiles; /*`json:"excludeFiles"`*/
}

partial struct WatchFileKind /*num:int32*/;

public static WatchFileKind WatchFileKindNone => 0;
public static WatchFileKind WatchFileKindFixedPollingInterval => 1;
public static WatchFileKind WatchFileKindPriorityPollingInterval => 2;
public static WatchFileKind WatchFileKindDynamicPriorityPolling => 3;
public static WatchFileKind WatchFileKindFixedChunkSizePolling => 4;
public static WatchFileKind WatchFileKindUseFsEvents => 5;
public static WatchFileKind WatchFileKindUseFsEventsOnParentDirectory => 6;

partial struct WatchDirectoryKind /*num:int32*/;

public static WatchDirectoryKind WatchDirectoryKindNone => 0;
public static WatchDirectoryKind WatchDirectoryKindUseFsEvents => 1;
public static WatchDirectoryKind WatchDirectoryKindFixedPollingInterval => 2;
public static WatchDirectoryKind WatchDirectoryKindDynamicPriorityPolling => 3;
public static WatchDirectoryKind WatchDirectoryKindFixedChunkSizePolling => 4;

partial struct PollingKind /*num:int32*/;

public static PollingKind PollingKindNone => 0;
public static PollingKind PollingKindFixedInterval => 1;
public static PollingKind PollingKindPriorityInterval => 2;
public static PollingKind PollingKindDynamicPriority => 3;
public static PollingKind PollingKindFixedChunkSize => 4;

} partial class coreᴛWatchOptions {

public static time.Duration WatchInterval(this ж<WatchOptions> Ꮡw) {
    ref var w = ref Ꮡw.DerefOrNull();

    var watchInterval = 2000 * time.Millisecond;
    if (Ꮡw != nil && w.Interval != nil) {
        watchInterval = ((time.Duration)(int64)(w.Interval.Value)) * time.Millisecond;
    }
    return watchInterval;
}

} // end coreᴛWatchOptions
