namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using static core_package;

partial class core_package {

partial interface Semaphore {
    Action /*release*/ Acquire();
    (Action release, bool acquired) TryAcquire(context.Context ctx);
}

internal static Semaphore _ᴛ5ʗ = new UnlimitedSemaphore();

partial struct UnlimitedSemaphore {
}

} partial class coreᴛUnlimitedSemaphore {

public static Action /*release*/ Acquire(this UnlimitedSemaphore s) {
    return () => {
    };
}

public static (Action release, bool acquired) TryAcquire(this UnlimitedSemaphore s, context.Context ctx) {
    return (() => {
    }, true);
}

} partial class core_package {

internal static Semaphore _ᴛ6ʗ = new LimitedSemaphoreжSemaphore(((ж<LimitedSemaphore>)nil));

partial struct LimitedSemaphore {
    internal channel<EmptyStruct> ch;
    internal Action release;
}

public static ж<LimitedSemaphore> NewLimitedSemaphore(nint maxConcurrency) {
    if (maxConcurrency <= 0) {
        throw panic("maxConcurrency must be positive");
    }
    var s = Ꮡ(new LimitedSemaphore(
        ch: new channel<EmptyStruct>(maxConcurrency)
    ));
    var sʗ1 = s;
    s.release = () => {
        ᐸꟷ(sʗ1.ch);
    };
    return s;
}

} partial class coreᴛLimitedSemaphore {

public static Action /*release*/ Acquire(this ref LimitedSemaphore s) {
    s.ch.ᐸꟷ(new EmptyStruct());
    return s.release;
}

public static (Action release, bool acquired) TryAcquire(this ref LimitedSemaphore s, context.Context ctx) {
    var selᴛ1 = s.ch.ᐸꟷ(new EmptyStruct(), ꓸꓸꓸ);
    var selᴛ2 = ctx.Done();
    switch (select(selᴛ1, ᐸꟷ(selᴛ2, ꓸꓸꓸ))) {
    case 0: {
        return (s.release, true);
    }
    case 1 when selᴛ2.ꟷᐳ(out _): {
        return (() => {
        }, false);
    }}
    return default;
}

} // end coreᴛLimitedSemaphore
