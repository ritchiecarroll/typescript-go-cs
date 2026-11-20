namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using net = net_package;
using os = os_package;
using path = path_package;

partial class ipc_package {

internal static (net.Listener, error) newPipeListener(@string path) {
    _ = os.Remove(path);
    return net.Listen("unix", path);
}

public static @string GeneratePipePath(@string name) {
    return path.Join(os.TempDir(), name);
}

} // end ipc_package
