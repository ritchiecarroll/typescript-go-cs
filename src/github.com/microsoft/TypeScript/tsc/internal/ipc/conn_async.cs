namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using errors = errors_package;
using fmt = fmt_package;
using io = io_package;
using debug = runtime.debug_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using time = time_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using jsonrpc = go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package;
using MethodImplAttribute = global::System.Runtime.CompilerServices.MethodImplAttribute;
using MethodImplOptions = global::System.Runtime.CompilerServices.MethodImplOptions;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.sync;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using runtime;
using static ipc_package;

partial class ipc_package {

partial struct AsyncConn {
    internal io.ReadWriteCloser rwc;
    internal Protocol protocol;
    internal Handler handler;
    internal ж<timingCollector> timing;
    internal atomic.Int64 seq;
    internal map<jsonrpc.ID, channel<ж<Message>>> pending;
    internal sync.Mutex pendingMu;
    internal error terminal;
    internal bool hasCause;
    internal sync.Mutex writeMu;
    internal sync.WaitGroup handlers;
}

public static ж<AsyncConn> NewAsyncConn(io.ReadWriteCloser rwc, Handler handler) {
    return NewAsyncConnWithProtocol(rwc, new JSONRPCProtocolжProtocol(NewJSONRPCProtocol(new io_ReadWriteCloserᴠReadWriter(rwc))), handler);
}

public static ж<AsyncConn> NewAsyncConnWithProtocol(io.ReadWriteCloser rwc, Protocol protocol, Handler handler) {
    return Ꮡ(new AsyncConn(
        rwc: rwc,
        protocol: protocol,
        handler: handler,
        pending: new map<jsonrpc.ID, channel<ж<Message>>>()
    ));
}

} partial class ipcᴛAsyncConn {

public static void SetCollectTiming(this ref AsyncConn c, bool enabled) {
    if (enabled) {
        c.timing = newTimingCollector();
    } else {
        c.timing = default;
    }
}

public static error /*err*/ Run(this ж<AsyncConn> Ꮡc, context.Context ctx) {
    error err = default;
    GoFrame ᒐ = default;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        var (handlerCtx, cancelHandlers) = context.WithCancel(ctx);
        var requestErrors = new channel<error>(1);
        var cancelHandlersʗ1 = cancelHandlers;
        var requestErrorsʗ1 = requestErrors;
        defer(() => {
            Ꮡc.closePendingCalls(err);
            cancelHandlersʗ1();
            Ꮡc.Ꮡhandlers.Wait();
            var requestErrorsᴛ1 = requestErrorsʗ1;
            switch (trySelect(ᐸꟷ(requestErrorsᴛ1, ꓸꓸꓸ))) {
            case 0 when requestErrorsᴛ1.ꟷᐳ(out var requestErr): {
                err = errors.Join(err, requestErr);
                break;
            }
            default: {
                break;
            }}
        }, ref ᒐ);
        for (;;) {
            if (ctx.Err() != nil) {
                err = ctx.Err(); goto ᒐdone;
            }

            var (msg, errΔ1) = c.protocol.ReadMessage();
            if (errΔ1 != nil) {
                if (errors.Is(errΔ1, io.EOF)) {
                    err = default; goto ᒐdone;
                }
                err = errΔ1; goto ᒐdone;
            }

            if (msg.IsResponse()) {
                Ꮡc.handleResponse(msg);
            } else if (msg.IsRequest()) {
                var handlerCtxʗ1 = handlerCtx;
                var msgʗ1 = msg;
                var requestErrorsʗ2 = requestErrors;
                Ꮡc.Ꮡhandlers.Go(() => {
                    var requestErr = Ꮡc.handleRequest(handlerCtxʗ1, msgʗ1); if (requestErr != nil) {
                        if (Ꮡc.recordRequestError(requestErr, requestErrorsʗ2.AsSendOnly())) {
                            if (Ꮡc.Value.rwc != nil) {
                                _ = Ꮡc.Value.rwc.Close();
                            }
                        }
                    }
                });
            } else if (msg.IsNotification()) {
                var handlerCtxʗ2 = handlerCtx;
                var msgʗ2 = msg;
                Ꮡc.Ꮡhandlers.Go(() => {
                    Ꮡc.Value.handleNotification(handlerCtxʗ2, msgʗ2);
                });
            }
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return err;
}

internal static void closePendingCalls(this ж<AsyncConn> Ꮡc, error runErr) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.pendingMu.Lock();
        ᒐd1 = true;
        c.recordTerminalErrorLocked(runErr);
        c.closePendingCallsLocked();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().pendingMu.Unlock(); ᒐ.Run(); }
}

internal static bool recordRequestError(this ж<AsyncConn> Ꮡc, error requestErr, channel/*<-*/<error> requestErrors) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.pendingMu.Lock();
        ᒐd1 = true;
        if (!c.recordTerminalErrorLocked(requestErr)) {
            return false;
        }
        requestErrors.ᐸꟷ(requestErr);
        c.closePendingCallsLocked();
        return true;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().pendingMu.Unlock(); ᒐ.Run(); }
}

internal static bool recordTerminalErrorLocked(this ref AsyncConn c, error terminalErr) {
    if (c.terminal == nil) {
        c.terminal = ErrConnClosed;
        if (terminalErr != nil) {
            c.terminal = errors.Join(c.terminal, terminalErr);
            c.hasCause = true;
            return true;
        }
    } else if (!c.hasCause && terminalErr != nil) {
        c.terminal = errors.Join(c.terminal, terminalErr);
        c.hasCause = true;
        return true;
    }
    return false;
}

internal static void closePendingCallsLocked(this ref AsyncConn c) {
    foreach (var (id, ch) in c.pending) {
        close(ch);
        delete(c.pending, id);
    }
}

internal static void handleResponse(this ж<AsyncConn> Ꮡc, ж<Message> Ꮡmsg) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var msg = ref Ꮡmsg.DerefOrNull();

    c.pendingMu.Lock();
    var (ch, ok) = c.pending[msg.ID.Value, ꟷ];
    if (ok) {
        delete(c.pending, msg.ID.Value);
    }
    c.pendingMu.Unlock();

    if (ok) {
        ch.ᐸꟷ(Ꮡmsg);
        close(ch);
    }
}

internal static error /*retErr*/ handleRequest(this ж<AsyncConn> Ꮡc, context.Context ctx, ж<Message> Ꮡmsg) {
    error retErr = default;
    GoFrame ᒐ = default;
    try {
        ref var c = ref Ꮡc.DerefOrNull();
        ref var msg = ref Ꮡmsg.DerefOrNull();

        var exprᴛ1 = msg.Method;
        switch (ᐧ) {
        case {} when exprᴛ1 == ((@string)MethodGetServerTiming): {
            c.writeMu.Lock();
            var writeErrΔ2 = c.protocol.WriteResponse(msg.ID, serverTimingSnapshot(c.timing));
            c.writeMu.Unlock();
            if (writeErrΔ2 != nil) {
                retErr = fmt.Errorf("ipc: failed to write server timing response: %w"u8, writeErrΔ2); goto ᒐdone;
            }
            retErr = default; goto ᒐdone;
        }
        case {} when exprᴛ1 == ((@string)MethodResetServerTiming): {
            if (c.timing != nil) {
                c.timing.reset();
            }
            c.writeMu.Lock();
            var writeErrΔ3 = c.protocol.WriteResponse(msg.ID, default);
            c.writeMu.Unlock();
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

                Ꮡc.Value.writeMu.Lock();
                var writeErrΔ4 = Ꮡc.Value.protocol.WriteError(Ꮡmsg.Value.ID, Ꮡ(new jsonrpc.ResponseError(
                    Code: jsonrpc.CodeInternalError,
                    Message: Ꮡerr.ValueSlot.Error()
                )));
                Ꮡc.Value.writeMu.Unlock();

                if (writeErrΔ4 != nil) {
                    retErr = fmt.Errorf("ipc: failed to write panic error response: %w (original panic: %v)"u8, writeErrΔ4, r);
                }
            }
        }, ref ᒐ);

        (result, err) = c.handler.HandleRequest(ctx, msg.Method, msg.Params);

        if (c.timing != nil) {
            c.timing.record(msg.Method, time.Since(start));
        }

        c.writeMu.Lock();
        defer(Ꮡc.ᏑwriteMu.Unlock, ref ᒐ);

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

internal static void handleNotification(this ref AsyncConn c, context.Context ctx, ж<Message> Ꮡmsg) {
    ref var msg = ref Ꮡmsg.DerefOrNull();

    _ = c.handler.HandleNotification(ctx, msg.Method, msg.Params);
}

public static (jsontext.Value, error) Call(this ж<AsyncConn> Ꮡc, context.Context ctx, @string method, any @params) {
    GoFrame ᒐ = default;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        var id = jsonrpc.NewIDString(fmt.Sprintf("api%d"u8, Ꮡc.Ꮡseq.Add(1)));

        var responseChan = new channel<ж<Message>>(1);
        c.pendingMu.Lock();
        if (c.terminal != nil) {
            var errΔ1 = c.terminal;
            c.pendingMu.Unlock();
            return (default, errΔ1);
        }
        c.pending[id.Value] = responseChan;
        c.pendingMu.Unlock();

        var idʗ1 = id;
        defer(() => {
            GoFrame ᒐ = default;
            try {
                Ꮡc.Value.pendingMu.Lock();
                defer(Ꮡc.ᏑpendingMu.Unlock, ref ᒐ);
                var (ch, ok) = Ꮡc.Value.pending[idʗ1.Value, ꟷ]; if (ok) {
                    close(ch);
                    delete(Ꮡc.Value.pending, idʗ1.Value);
                }
            }
            catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
            finally { ᒐ.Run(); }
        }, ref ᒐ);

        c.writeMu.Lock();
        var err = c.protocol.WriteRequest(id, method, @params);
        c.writeMu.Unlock();

        if (err != nil) {
            return (default, err);
        }

        var selᴛ2 = ctx.Done();
        var responseChanᴛ1 = responseChan;
        switch (select(ᐸꟷ(selᴛ2, ꓸꓸꓸ), ᐸꟷ(responseChanᴛ1, ꓸꓸꓸ))) {
        case 0 when selᴛ2.ꟷᐳ(out _): {
            return (default, ctx.Err());
        }
        case 1 when responseChanᴛ1.ꟷᐳ(out var resp, out var ok): {
            if (!ok) {
                c.pendingMu.Lock();
                var errΔ2 = c.terminal;
                c.pendingMu.Unlock();
                return (default, errΔ2);
            }
            if (resp.Error != nil) {
                return (default, fmt.Errorf("ipc: remote error [%d]: %s"u8, resp.Error.Code, resp.Error.Message));
            }
            return (resp.Result, default);
        }}
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static error Notify(this ж<AsyncConn> Ꮡc, context.Context ctx, @string method, any @params) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.pendingMu.Lock();
        if (c.terminal != nil) {
            var err = c.terminal;
            c.pendingMu.Unlock();
            return err;
        }
        c.pendingMu.Unlock();
        c.writeMu.Lock();
        ᒐd1 = true;
        return c.protocol.WriteNotification(method, @params);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().writeMu.Unlock(); ᒐ.Run(); }
}

} // end ipcᴛAsyncConn
