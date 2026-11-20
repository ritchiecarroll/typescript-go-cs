namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using io = io_package;
using net = net_package;
using static ipc_package;

partial class ipc_package {

partial interface Transport :
    io.Closer
{
    (io.ReadWriteCloser, error) Accept();
}

partial struct PipeTransport {
    internal net.Listener listener;
}

public static (ж<PipeTransport>, error) NewPipeTransport(@string path) {
    var (listener, err) = newPipeListener(path);
    if (err != nil) {
        return (default, err);
    }
    return (Ꮡ(new PipeTransport(listener: listener)), default);
}

} partial class ipcᴛPipeTransport {

public static (io.ReadWriteCloser, error) Accept(this ref PipeTransport t) {
    return t.listener.Accept();
}

public static error Close(this ref PipeTransport t) {
    return t.listener.Close();
}

public static @string Path(this ref PipeTransport t) {
    return t.listener.Addr().String();
}

} partial class ipc_package {

partial struct StdioTransport {
    internal io.ReadCloser stdin;
    internal io.WriteCloser stdout;
    internal bool used;
}

public static ж<StdioTransport> NewStdioTransport(io.ReadCloser stdin, io.WriteCloser stdout) {
    return Ꮡ(new StdioTransport(
        stdin: stdin,
        stdout: stdout
    ));
}

} partial class ipcᴛStdioTransport {

public static (io.ReadWriteCloser, error) Accept(this ref StdioTransport t) {
    if (t.used) {
        return (default, io.EOF);
    }
    t.used = true;
    return (new stdioConnжReadWriteCloser(Ꮡ(new stdioConn(
        Reader: t.stdin,
        Writer: t.stdout,
        stdin: t.stdin,
        stdout: t.stdout
    ))), default);
}

public static error Close(this ref StdioTransport t) {
    return default;
}

} partial class ipc_package {

partial struct stdioConn {
    /*embed*/ public io_package.Reader Reader;
    /*embed*/ public io_package.Writer Writer;
    internal io.ReadCloser stdin;
    internal io.WriteCloser stdout;
}

} partial class ipcᴛstdioConn {

internal static error Close(this ref stdioConn c) {
    var err1 = c.stdin.Close();
    var err2 = c.stdout.Close();
    if (err1 != nil) {
        return err1;
    }
    return err2;
}

} // end ipcᴛstdioConn
