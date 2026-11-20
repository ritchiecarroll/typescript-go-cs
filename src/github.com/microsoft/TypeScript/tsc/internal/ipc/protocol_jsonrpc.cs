namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using io = io_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using jsonrpc = go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using static ipc_package;

partial class ipc_package {

partial struct JSONRPCProtocol {
    internal ж<jsonrpc.Reader> reader;
    internal ж<jsonrpc.Writer> writer;
}

internal static Protocol _ᴛ1ʗ = new JSONRPCProtocolжProtocol(((ж<JSONRPCProtocol>)nil));

public static ж<JSONRPCProtocol> NewJSONRPCProtocol(io.ReadWriter rw) {
    return Ꮡ(new JSONRPCProtocol(
        reader: jsonrpc.NewReader(rw),
        writer: jsonrpc.NewWriter(rw)
    ));
}

} partial class ipcᴛJSONRPCProtocol {

public static (ж<Message>, error) ReadMessage(this ref JSONRPCProtocol p) {
    var (data, err) = p.reader.Read();
    if (err != nil) {
        return (default, err);
    }

    ref var msg = ref heap(new Message(), out var Ꮡmsg);
    var errΔ1 = json.Unmarshal(data, Ꮡmsg); if (errΔ1 != nil) {
        return (default, errΔ1);
    }

    return (Ꮡmsg, default);
}

public static error WriteRequest(this ref JSONRPCProtocol p, ж<jsonrpc.ID> id, @string method, any @params) {
    var msg = new jsonrpc.RequestMessage(
        ID: id,
        Method: method,
        Params: @params
    );
    var (data, err) = json.Marshal(msg);
    if (err != nil) {
        return err;
    }
    return p.writer.Write(data);
}

public static error WriteNotification(this ref JSONRPCProtocol p, @string method, any @params) {
    var msg = new jsonrpc.RequestMessage(
        Method: method,
        Params: @params
    );
    var (data, err) = json.Marshal(msg);
    if (err != nil) {
        return err;
    }
    return p.writer.Write(data);
}

public static error WriteResponse(this ref JSONRPCProtocol p, ж<jsonrpc.ID> id, any result) {
    if (result == nil) {
        result = (jsontext.Value)slice<byte>((@string)"null"u8);
    }
    var msg = new jsonrpc.ResponseMessage(
        ID: id,
        Result: result
    );
    var (data, err) = json.Marshal(msg);
    if (err != nil) {
        return err;
    }
    return p.writer.Write(data);
}

public static error WriteError(this ref JSONRPCProtocol p, ж<jsonrpc.ID> id, ж<jsonrpc.ResponseError> respErr) {
    var msg = new jsonrpc.ResponseMessage(
        ID: id,
        Error: respErr
    );
    var (data, err) = json.Marshal(msg);
    if (err != nil) {
        return err;
    }
    return p.writer.Write(data);
}

} // end ipcᴛJSONRPCProtocol
