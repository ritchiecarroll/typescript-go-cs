namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using sync = sync_package;
using time = time_package;
using static ipc_package;

partial class ipc_package {

public static readonly @string MethodGetServerTiming = "getServerTiming"u8;
public static readonly @string MethodResetServerTiming = "resetServerTiming"u8;

internal static UntypedInt serverRecentRequestCapacity => 5;

public partial struct serverRequestTiming {
    public @string Method; /*`json:"method"`*/
    public float64 ProcessingTimeMs; /*`json:"processingTimeMs"`*/
    public int64 Timestamp; /*`json:"timestamp"`*/
}

public partial struct serverTimingTotals {
    public uint64 RequestCount; /*`json:"requestCount"`*/
    public float64 TotalProcessingTimeMs; /*`json:"totalProcessingTimeMs"`*/
}

partial struct serverTimingInfo {
    public bool Enabled; /*`json:"enabled"`*/
    public serverTimingTotals Totals; /*`json:"totals"`*/
    public slice<serverRequestTiming> RecentRequests; /*`json:"recentRequests"`*/
}

partial struct timingCollector {
    internal sync.Mutex mu;
    internal serverTimingTotals totals;
    internal slice<serverRequestTiming> ring;
    internal nint head;
}

internal static ж<timingCollector> newTimingCollector() {
    return Ꮡ(new timingCollector());
}

} partial class ipcᴛtimingCollector {

internal static void record(this ж<timingCollector> Ꮡc, @string method, time.Duration d) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        var processingMs = durationToMillis(d);

        c.mu.Lock();
        ᒐd1 = true;

        c.totals.RequestCount++;
        c.totals.TotalProcessingTimeMs += processingMs;

        var entry = new serverRequestTiming(
            Method: method,
            ProcessingTimeMs: processingMs,
            Timestamp: time.Now().UnixMilli()
        );
        if (len(c.ring) < serverRecentRequestCapacity) {
            c.ring = append(c.ring, entry);
        } else {
            c.ring[c.head] = entry;
            c.head = (c.head + 1) % (nint)serverRecentRequestCapacity;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static serverTimingInfo snapshot(this ж<timingCollector> Ꮡc) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        var recent = new slice<serverRequestTiming>(0, len(c.ring));
        foreach (var i in range(len(c.ring))) {
            recent = append(recent, c.ring[(c.head + i) % len(c.ring)]);
        }
        return new serverTimingInfo(
            Enabled: true,
            Totals: c.totals,
            RecentRequests: recent
        );
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static void reset(this ж<timingCollector> Ꮡc) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        c.totals = new serverTimingTotals();
        c.ring = default;
        c.head = 0;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

} partial class ipc_package {

internal static serverTimingInfo serverTimingSnapshot(ж<timingCollector> c) {
    if (c == nil) {
        return disabledServerTimingInfo();
    }
    return c.snapshot();
}

internal static serverTimingInfo disabledServerTimingInfo() {
    return new serverTimingInfo(
        Enabled: false,
        RecentRequests: []
    );
}

internal static float64 durationToMillis(time.Duration d) {
    if (d < 0) {
        return 0D;
    }
    return (float64)d / (float64)time.Millisecond;
}

} // end ipc_package
