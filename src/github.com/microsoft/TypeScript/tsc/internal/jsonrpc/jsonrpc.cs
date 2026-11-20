namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using errors = errors_package;
using fmt = fmt_package;
using strconv = strconv_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using static jsonrpc_package;

partial class jsonrpc_package {

partial struct JSONRPCVersion {
}

internal static readonly @string jsonRPCVersion = @"""2.0"""u8;

} partial class jsonrpcᴛJSONRPCVersion {

public static (slice<byte>, error) MarshalJSON(this JSONRPCVersion _) {
    return (slice<byte>(jsonRPCVersion), default);
}

} partial class jsonrpc_package {

public static error ErrInvalidJSONRPCVersion = errors.New("invalid JSON-RPC version");

} partial class jsonrpcᴛJSONRPCVersion {

public static error UnmarshalJSON(this ref JSONRPCVersion _, slice<byte> data) {
    if (((sstring)data) != jsonRPCVersion) {
        return ErrInvalidJSONRPCVersion;
    }
    return default;
}

} partial class jsonrpc_package {

partial struct ID {
    internal @string str;
    internal int32 @int;
}

public static ж<ID> NewID(IntegerOrString rawValue) {
    if (rawValue.String != nil) {
        return Ꮡ(new ID(str: rawValue.String.Value));
    }
    return Ꮡ(new ID(@int: rawValue.Integer.Value));
}

public static ж<ID> NewIDString(@string str) {
    return Ꮡ(new ID(str: str));
}

public static ж<ID> NewIDInt(int32 i) {
    return Ꮡ(new ID(@int: i));
}

} partial class jsonrpcᴛID {

public static @string String(this ref ID id) {
    if (id.str != ""u8) {
        return id.str;
    }
    return strconv.Itoa((nint)id.@int);
}

public static (slice<byte>, error) MarshalJSON(this ref ID id) {
    if (id.str != ""u8) {
        return json.Marshal(id.str);
    }
    return json.Marshal(id.@int);
}

public static error UnmarshalJSON(this ж<ID> Ꮡid, slice<byte> data) {
    ref var id = ref Ꮡid.DerefOrNull();

    id = new ID();
    if (len(data) > 0 && data[0] == '"') {
        return json.Unmarshal(data, Ꮡid.Ꮡstr);
    }
    return json.Unmarshal(data, Ꮡid.Ꮡint);
}

public static (int32, bool) TryInt(this ж<ID> Ꮡid) {
    ref var id = ref Ꮡid.DerefOrNull();

    if (Ꮡid == nil || id.str != ""u8) {
        return (0, false);
    }
    return (id.@int, true);
}

public static int32 MustInt(this ref ID id) {
    if (id.str != ""u8) {
        throw panic("ID is not an integer");
    }
    return id.@int;
}

} partial class jsonrpc_package {

partial struct IntegerOrString {
    public ж<int32> Integer;
    public ж<@string> String;
}

partial struct ResponseError {
    public int32 Code; /*`json:"code"`*/
    public @string Message; /*`json:"message"`*/
    public any Data; /*`json:"data,omitzero"`*/
}

} partial class jsonrpcᴛResponseError {

public static @string String(this ж<ResponseError> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (Ꮡr == nil) {
        return "";
    }
    var (data, err) = json.Marshal(r.Data);
    if (err != nil) {
        return fmt.Sprintf("[%d]: %s\n%v"u8, r.Code, r.Message, data);
    }
    return fmt.Sprintf("[%d]: %s"u8, r.Code, r.Message);
}

public static @string Error(this ж<ResponseError> Ꮡr) {
    return Ꮡr.String();
}

} partial class jsonrpc_package {

public const int32 CodeParseError = -32700;
public const int32 CodeInvalidRequest = -32600;
public const int32 CodeMethodNotFound = -32601;
public const int32 CodeInvalidParams = -32602;
public const int32 CodeInternalError = -32603;

partial struct MessageKind /*num:nint*/;

public static MessageKind MessageKindNotification => /* iota */ 0;
public static MessageKind MessageKindRequest => 1;
public static MessageKind MessageKindResponse => 2;

partial struct Message {
    public JSONRPCVersion JSONRPC; /*`json:"jsonrpc"`*/
    public ж<ID> ID; /*`json:"id,omitzero"`*/
    public @string Method; /*`json:"method,omitzero"`*/
    public jsontext.Value Params; /*`json:"params,omitzero"`*/
    public jsontext.Value Result; /*`json:"result,omitzero"`*/
    public ж<ResponseError> Error; /*`json:"error,omitzero"`*/
}

} partial class jsonrpcᴛMessage {

public static MessageKind Kind(this ref Message m) {
    if (m.ID != nil && m.Method == ""u8) {
        return MessageKindResponse;
    }
    if (m.ID == nil) {
        return MessageKindNotification;
    }
    return MessageKindRequest;
}

public static bool IsRequest(this ref Message m) {
    return m.ID != nil && m.Method != ""u8;
}

public static bool IsNotification(this ref Message m) {
    return m.ID == nil && m.Method != ""u8;
}

public static bool IsResponse(this ref Message m) {
    return m.ID != nil && m.Method == ""u8;
}

} partial class jsonrpc_package {

partial struct RequestMessage {
    public JSONRPCVersion JSONRPC; /*`json:"jsonrpc"`*/
    public ж<ID> ID; /*`json:"id,omitzero"`*/
    public @string Method; /*`json:"method"`*/
    public any Params; /*`json:"params,omitzero"`*/
}

partial struct ResponseMessage {
    public JSONRPCVersion JSONRPC; /*`json:"jsonrpc"`*/
    public ж<ID> ID; /*`json:"id,omitzero"`*/
    public any Result; /*`json:"result,omitzero"`*/
    public ж<ResponseError> Error; /*`json:"error,omitzero"`*/
}

} // end jsonrpc_package
