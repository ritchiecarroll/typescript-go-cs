namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using errors = errors_package;
using fmt = fmt_package;
using io = io_package;
using debug = runtime.debug_package;
using sync = sync_package;
using time = time_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using jsonrpc = go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package;
using MethodImplAttribute = global::System.Runtime.CompilerServices.MethodImplAttribute;
using MethodImplOptions = global::System.Runtime.CompilerServices.MethodImplOptions;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using runtime;
using static ipc_package;

partial class ipc_package {

partial struct SyncConn {
    internal io.ReadWriteCloser rwc;
    internal Protocol protocol;
    internal Handler handler;
    internal ж<timingCollector> timing;
    internal sync.Mutex mu;
}

public static ж<SyncConn> NewSyncConn(io.ReadWriteCloser rwc, Protocol protocol, Handler handler) {
    return Ꮡ(new SyncConn(
        rwc: rwc,
        protocol: protocol,
        handler: handler
    ));
}

} partial class ipcᴛSyncConn {

public static void SetCollectTiming(this ref SyncConn c, bool enabled) {
    if (enabled) {
        c.timing = newTimingCollector();
    } else {
        c.timing = default;
    }
}

public static error Run(this ж<SyncConn> Ꮡc, context.Context ctx) {
    ref var c = ref Ꮡc.DerefOrNull();

    for (;;) {
        if (ctx.Err() != nil) {
            return ctx.Err();
        }

        c.mu.Lock();
        var (msg, err) = c.protocol.ReadMessage();
        c.mu.Unlock();

        if (err != nil) {
            if (errors.Is(err, io.EOF)) {
                return default;
            }
            return err;
        }

        if (msg.IsRequest()) {
            var errΔ1 = Ꮡc.handleRequest(ctx, msg); if (errΔ1 != nil) {
                return errΔ1;
            }
        } else if (msg.IsNotification()) {
            c.handleNotification(ctx, msg);
        } else {
            return errors.New("ipc: unexpected response message in sync connection");
        }
    }
}

internal static error /*retErr*/ handleRequest(this ж<SyncConn> Ꮡc, context.Context ctx, ж<Message> Ꮡmsg) {
    error retErr = default;
    GoFrame ᒐ = default;
    try {
        ref var c = ref Ꮡc.DerefOrNull();
        ref var msg = ref Ꮡmsg.DerefOrNull();

        var exprᴛ1 = msg.Method;
        switch (ᐧ) {
        case {} when exprᴛ1 == ((@string)MethodGetServerTiming): {
            c.mu.Lock();
            var writeErrΔ2 = c.protocol.WriteResponse(msg.ID, serverTimingSnapshot(c.timing));
            c.mu.Unlock();
            if (writeErrΔ2 != nil) {
                retErr = fmt.Errorf("ipc: failed to write server timing response: %w"u8, writeErrΔ2); goto ᒐdone;
            }
            retErr = default; goto ᒐdone;
        }
        case {} when exprᴛ1 == ((@string)MethodResetServerTiming): {
            if (c.timing != nil) {
                c.timing.reset();
            }
            c.mu.Lock();
            var writeErrΔ3 = c.protocol.WriteResponse(msg.ID, default);
            c.mu.Unlock();
            if (writeErrΔ3 != nil) {
                retErr = fmt.Errorf("ipc: failed to write reset server timing response: %w"u8, writeErrΔ3); goto ᒐdone;
            }
            retErr = default; goto ᒐdone;
        }}

        any result = default;
        ref var err = ref heap<error>(out var Ꮡerr);

        var start = new time.Time();
        if (c.timing != nil) {
            start = time.Now();
        }

        defer([MethodImpl(MethodImplOptions.NoInlining)] () => {
            var r = recover(); if (r != nil) {
                @string stack = (@string)debug.Stack();
                Ꮡerr.ValueSlot = fmt.Errorf("panic: %v\n%s"u8, r, stack);

                Ꮡc.Value.mu.Lock();
                var writeErrΔ4 = Ꮡc.Value.protocol.WriteError(Ꮡmsg.Value.ID, Ꮡ(new jsonrpc.ResponseError(
                    Code: jsonrpc.CodeInternalError,
                    Message: Ꮡerr.ValueSlot.Error()
                )));
                Ꮡc.Value.mu.Unlock();

                if (writeErrΔ4 != nil) {
                    retErr = fmt.Errorf("ipc: failed to write panic error response: %w (original panic: %v)"u8, writeErrΔ4, r);
                }
            }
        }, ref ᒐ);

        (result, err) = c.handler.HandleRequest(ctx, msg.Method, msg.Params);

        if (c.timing != nil) {
            c.timing.record(msg.Method, time.Since(start));
        }

        c.mu.Lock();
        defer(Ꮡc.Ꮡmu.Unlock, ref ᒐ);

        error writeErr = default;
        if (err != nil) {
            writeErr = c.protocol.WriteError(msg.ID, Ꮡ(new jsonrpc.ResponseError(
                Code: jsonrpc.CodeInternalError,
                Message: err.Error()
            )));
        } else {
            writeErr = c.protocol.WriteResponse(msg.ID, result);
        }

        if (writeErr != nil) {
            retErr = fmt.Errorf("ipc: failed to write response: %w"u8, writeErr); goto ᒐdone;
        }
        retErr = default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return retErr;
}

internal static void handleNotification(this ref SyncConn c, context.Context ctx, ж<Message> Ꮡmsg) {
    ref var msg = ref Ꮡmsg.DerefOrNull();

    _ = c.handler.HandleNotification(ctx, msg.Method, msg.Params);
}

public static (jsontext.Value, error) Call(this ж<SyncConn> Ꮡc, context.Context ctx, @string method, any @params) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        var id = jsonrpc.NewIDString(method);

        var errΔ1 = c.protocol.WriteRequest(id, method, @params); if (errΔ1 != nil) {
            return (default, errΔ1);
        }

        if (ctx.Err() != nil) {
            return (default, ctx.Err());
        }

        var (msg, err) = c.protocol.ReadMessage();
        if (err != nil) {
            return (default, err);
        }

        if (msg.IsResponse() && msg.ID != nil && msg.ID.String() == method) {
            if (msg.Error != nil) {
                return (default, fmt.Errorf("ipc: remote error [%d]: %s"u8, msg.Error.Code, msg.Error.Message));
            }
            return (msg.Result, default);
        }

        return (default, fmt.Errorf("ipc: unexpected message while waiting for %q response"u8, method));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

public static error Notify(this ж<SyncConn> Ꮡc, context.Context ctx, @string method, any @params) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;
        return c.protocol.WriteNotification(method, @params);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

} // end ipcᴛSyncConn
