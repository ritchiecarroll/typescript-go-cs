namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using errors = errors_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;

partial class ipc_package {

public static error ErrConnClosed = errors.New("ipc: connection closed");
public static error ErrRequestTimeout = errors.New("ipc: request timeout");

partial interface Handler {
    (any, error) HandleRequest(context.Context ctx, @string method, jsontext.Value @params);
    error HandleNotification(context.Context ctx, @string method, jsontext.Value @params);
}

partial interface Conn {
    error Run(context.Context ctx);
    (jsontext.Value, error) Call(context.Context ctx, @string method, any @params);
    error Notify(context.Context ctx, @string method, any @params);
}

public static (ж<T>, error) UnmarshalParams<T>(jsontext.Value @params) {
    if (len(@params) == 0) {
        return (default, default);
    }
    ref var v = ref heap<T>(out var Ꮡv);
    var err = json.Unmarshal(@params, Ꮡv); if (err != nil) {
        return (default, err);
    }
    return (Ꮡv, default);
}

} // end ipc_package
