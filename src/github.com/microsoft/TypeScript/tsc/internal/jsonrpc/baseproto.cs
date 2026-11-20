namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using bufio = bufio_package;
using bytes = bytes_package;
using errors = errors_package;
using fmt = fmt_package;
using io = io_package;
using strconv = strconv_package;
using static jsonrpc_package;

partial class jsonrpc_package {

public static error ErrInvalidHeader = errors.New("jsonrpc: invalid header");
public static error ErrInvalidContentLength = errors.New("jsonrpc: invalid content length");
public static error ErrNoContentLength = errors.New("jsonrpc: no content length");

partial struct Reader {
    internal ж<bufio.Reader> r;
}

public static ж<Reader> NewReader(io.Reader r) {
    return Ꮡ(new Reader(
        r: bufio.NewReader(r)
    ));
}

} partial class jsonrpcᴛReader {

public static (slice<byte>, error) Read(this ref Reader r) {
    int64 contentLength = default;

    for (;;) {
        var (line, err) = r.r.ReadBytes((byte)'\n');
        if (err != nil) {
            if (errors.Is(err, io.EOF)) {
                return (default, io.EOF);
            }
            return (default, fmt.Errorf("jsonrpc: read header: %w"u8, err));
        }

        if (bytes.Equal(line, slice<byte>("\r\n"u8))) {
            break;
        }

        var (key, value, ok) = bytes.Cut(line, slice<byte>(":"u8));
        if (!ok) {
            return (default, fmt.Errorf("%w: %q"u8, ErrInvalidHeader, line));
        }

        if (bytes.Equal(key, slice<byte>("Content-Length"u8))) {
            (contentLength, err) = strconv.ParseInt((@string)bytes.TrimSpace(value), 10, 64);
            if (err != nil) {
                return (default, fmt.Errorf("%w: parse error: %w"u8, ErrInvalidContentLength, err));
            }
            if (contentLength < 0) {
                return (default, fmt.Errorf("%w: negative value %d"u8, ErrInvalidContentLength, contentLength));
            }
        }
    }

    if (contentLength <= 0) {
        return (default, ErrNoContentLength);
    }

    var data = new slice<byte>((nint)(contentLength));
    {
        var (_, err) = io.ReadFull(new bufio_ReaderжReader(r.r), data); if (err != nil) {
            return (default, fmt.Errorf("jsonrpc: read content: %w"u8, err));
        }
    }

    return (data, default);
}

} partial class jsonrpc_package {

partial struct Writer {
    internal ж<bufio.Writer> w;
}

public static ж<Writer> NewWriter(io.Writer w) {
    return Ꮡ(new Writer(
        w: bufio.NewWriter(w)
    ));
}

} partial class jsonrpcᴛWriter {

public static error Write(this ref Writer w, slice<byte> data) {
    {
        var (_, err) = fmt.Fprintf(new bufio_WriterжWriter(w.w), "Content-Length: %d\r\n\r\n"u8, len(data)); if (err != nil) {
            return err;
        }
    }
    {
        var (_, err) = w.w.Write(data); if (err != nil) {
            return err;
        }
    }
    return w.w.Flush();
}

} // end jsonrpcᴛWriter
