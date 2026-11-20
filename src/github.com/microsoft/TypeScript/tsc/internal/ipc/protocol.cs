global using Message = go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package.Message;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using jsonrpc = go.github.com.microsoft.TypeScript.tsc.@internal.jsonrpc_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ipc_package {

partial interface Protocol {
    (ж<Message>, error) ReadMessage();
    error WriteRequest(ж<jsonrpc.ID> id, @string method, any @params);
    error WriteNotification(@string method, any @params);
    error WriteResponse(ж<jsonrpc.ID> id, any result);
    error WriteError(ж<jsonrpc.ID> id, ж<jsonrpc.ResponseError> err);
}

} // end ipc_package
