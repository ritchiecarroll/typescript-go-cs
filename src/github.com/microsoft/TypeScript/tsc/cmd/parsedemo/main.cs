namespace go;

using bufio = bufio_package;
using sha256 = crypto.sha256_package;
using fmt = fmt_package;
using Δio = io_package;
using os = os_package;
using filepath = path.filepath_package;
using strings = strings_package;
using ast = github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = github.com.microsoft.TypeScript.tsc.@internal.core_package;
using parser = github.com.microsoft.TypeScript.tsc.@internal.parser_package;
using tspath = github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using crypto;
using github.com.microsoft.TypeScript.tsc.@internal;
using path;
using Δhash = hash_package;

partial class main_package {

internal static (nint diags, nint nodes) dump(Δio.Writer w, @string fileName, @string text, bool jsx, bool force) {
    nint diags = default;
    nint nodes = default;

    var opts = new ast.SourceFileParseOptions(FileName: fileName, Path: ((tspath.Path)fileName),
        ExternalModuleIndicatorOptions: new ast.ExternalModuleIndicatorOptions(JSX: jsx, Force: force));
    var @file = parser.ParseSourceFile(opts, text, core.GetScriptKindFromFileName(fileName));
    foreach (var (_, d) in range(@file.Diagnostics())) {
        fmt.Fprintf(w, "diag TS%d [%d,+%d] %s\n"u8, d.Code(), d.Pos(), d.Len(), d.String());
        diags++;
    }
    ref var walk = ref heap<Func<ж<ast.Node>, nint, bool>>(out var Ꮡwalk);
    walk = (ж<ast.Node> n, nint depth) => {
        fmt.Fprintf(w, "%s%v [%d,%d]\n"u8, strings.Repeat("  ", depth), n.Kind, n.Pos(), n.End());
        nodes++;
        n.ForEachChild((ж<ast.Node> c) => Ꮡwalk.ValueSlot(c, depth + 1));
        return false;
    };
    walk(@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), 0);
    return (diags, nodes);
}

internal static (@string fileName, @string text) unit(@string dir, @string idx, @string ext) {
    var (b, err) = os.ReadFile(filepath.Join(dir, "units", idx + ".txt"));
    if (err != nil) {
        fmt.Println("read error:", err);
        os.Exit(1);
    }
    return ("/index" + ext, (@string)b);
}

internal static @string /*line*/ digest(@string fileName, @string text, bool jsx, bool force) {
    @string line = default;
    GoFrame ᒐ = default;
    try {
        defer(() => {
            var r = recover(); if (r != nil) {
                line = fmt.Sprintf("PANIC %v"u8, r);
            }
        }, ref ᒐ);
        var h = sha256.New();
        var (d, n) = dump(h, fileName, text, jsx, force);
        line = fmt.Sprintf("%x %d %d"u8, h.Sum(default), d, n);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    return line;
}

internal static slice<@string> manifest(@string dir) {
    var (man, err) = os.ReadFile(filepath.Join(dir, "manifest.tsv"));
    if (err != nil) {
        fmt.Println("read error:", err);
        os.Exit(1);
    }
    return strings.Split(strings.TrimSuffix((@string)man, "\n"), "\n");
}

internal static void main() {
    GoFrame ᒐ = default;
    try {
        var args = os.Args[1..];
        switch (ᐧ) {
        case {} when (len(args) == 2 || len(args) == 3) && args[0] == "-corpus": {
            var rows = manifest(args[1]);
            if (len(args) == 3) {
                while (len(rows) > 0 && strings.Split(rows[0], "\t")[0] != args[2]) {
                    rows = rows[1..];
                }
            }
            var @out = bufio.NewWriter(new os.FileжWriter(os.Stdout));
            var outʗ1 = @out;
            defer(() => outʗ1.Flush(), ref ᒐ);
            foreach (var (_, row) in range(rows)) {
                var f = strings.Split(row, "\t");
                var (fileName, text) = unit(args[1], f[0], f[1]);
                fmt.Fprintf(new bufio_WriterжWriter(@out), "%s %s\n"u8, f[0], digest(fileName, text, f[2] == "true", f[3] == "true"));
            }
            break;
        }
        case {} when len(args) == 3 && args[0] == "-unit": {
            foreach (var (_, row) in range(manifest(args[1]))) {
                var f = strings.Split(row, "\t"); if (f[0] == args[2]) {
                    var (fileName, text) = unit(args[1], f[0], f[1]);
                    dump(new os.FileжWriter(os.Stdout), fileName, text, f[2] == "true", f[3] == "true");
                }
            }
            break;
        }
        default: {
            foreach (var (_, name) in range(args)) {
                var (text, err) = os.ReadFile(name);
                if (err != nil) {
                    fmt.Println("read error:", err);
                    os.Exit(1);
                }
                @string @base = name.slice(strings.LastIndexAny(name, @"/\") + 1);
                fmt.Printf("file %s (%d bytes)\n"u8, @base, len(text));
                dump(new os.FileжWriter(os.Stdout), "/"u8 + @base, (@string)text, false, false);
            }
            break;
        }}

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} // end main_package
