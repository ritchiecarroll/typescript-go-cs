namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using errgroup = golang.org.x.sync.errgroup_package;
using go.sync;
using golang.org.x.sync;
using static core_package;

partial class core_package {

partial interface WorkGroup {
    void Queue(Action fn);
    void RunAndWait();
}

public static WorkGroup NewWorkGroup(bool singleThreaded) {
    if (singleThreaded) {
        return new singleThreadedWorkGroupжWorkGroup(Ꮡ(new singleThreadedWorkGroup()));
    }
    return new parallelWorkGroupжWorkGroup(Ꮡ(new parallelWorkGroup()));
}

partial struct parallelWorkGroup {
    internal atomic.Bool done;
    internal sync.WaitGroup wg;
}

internal static WorkGroup _ᴛ8ʗ = new parallelWorkGroupжWorkGroup(((ж<parallelWorkGroup>)nil));

} partial class coreᴛparallelWorkGroup {

internal static void Queue(this ж<parallelWorkGroup> Ꮡw, Action fn) {
    if (Ꮡw.Ꮡdone.Load()) {
        throw panic("Queue called after RunAndWait returned");
    }

    Ꮡw.Ꮡwg.Go(() => {
        fn();
    });
}

internal static void RunAndWait(this ж<parallelWorkGroup> Ꮡw) {
    GoFrame ᒐ = default;
    try {
        defer(Ꮡw.Ꮡdone.Store, (bool)true, ref ᒐ);
        Ꮡw.Ꮡwg.Wait();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} partial class core_package {

partial struct singleThreadedWorkGroup {
    internal atomic.Bool done;
    internal sync.Mutex fnsMu;
    internal slice<Action> fns;
}

internal static WorkGroup _ᴛ9ʗ = new singleThreadedWorkGroupжWorkGroup(((ж<singleThreadedWorkGroup>)nil));

} partial class coreᴛsingleThreadedWorkGroup {

internal static void Queue(this ж<singleThreadedWorkGroup> Ꮡw, Action fn) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var w = ref Ꮡw.DerefOrNull();

        if (Ꮡw.Ꮡdone.Load()) {
            throw panic("Queue called after RunAndWait returned");
        }

        w.fnsMu.Lock();
        ᒐd1 = true;
        w.fns = append(w.fns, fn);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡw.DerefOrNull().fnsMu.Unlock(); ᒐ.Run(); }
}

internal static void RunAndWait(this ж<singleThreadedWorkGroup> Ꮡw) {
    GoFrame ᒐ = default;
    try {
        defer(Ꮡw.Ꮡdone.Store, (bool)true, ref ᒐ);
        for (;;) {
            var fn = Ꮡw.pop();
            if (fn == nil) {
                return;
            }
            fn();
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static Action pop(this ж<singleThreadedWorkGroup> Ꮡw) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var w = ref Ꮡw.DerefOrNull();

        w.fnsMu.Lock();
        ᒐd1 = true;
        if (len(w.fns) == 0) {
            return default;
        }
        nint end = len(w.fns) - 1;
        var fn = w.fns[end];
        w.fns[end] = default;
        w.fns = w.fns.slice(0, end);
        return fn;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡw.DerefOrNull().fnsMu.Unlock(); ᒐ.Run(); }
}

} partial class core_package {

partial struct ThrottleGroup {
    internal channel<EmptyStruct> semaphore;
    internal ж<errgroup.Group> group;
}

public static ж<ThrottleGroup> NewThrottleGroup(context.Context ctx, channel<EmptyStruct> semaphore) {
    var (g, _) = errgroup.WithContext(ctx);
    return Ꮡ(new ThrottleGroup(
        semaphore: semaphore,
        group: g
    ));
}

} partial class coreᴛThrottleGroup {

public static void Go(this ж<ThrottleGroup> Ꮡtg, Func<error> fn) {
    ref var tg = ref Ꮡtg.DerefOrNull();

    tg.group.Go(() => {
        GoFrame ᒐ = default;
        try {
            Ꮡtg.Value.semaphore.ᐸꟷ(new EmptyStruct());
            defer(() => {
                ᐸꟷ(Ꮡtg.Value.semaphore);
            }, ref ᒐ);
            return fn();
        }
        catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
        finally { ᒐ.Run(); }
    });
}

public static error Wait(this ref ThrottleGroup tg) {
    return tg.group.Wait();
}

} // end coreᴛThrottleGroup
