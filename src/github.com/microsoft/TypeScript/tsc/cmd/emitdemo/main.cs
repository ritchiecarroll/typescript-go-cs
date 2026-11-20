namespace go;

using context = context_package;
using fmt = fmt_package;
using os = os_package;
using sort = sort_package;
using strings = strings_package;
using bundled = github.com.microsoft.TypeScript.tsc.@internal.bundled_package;
using compiler = github.com.microsoft.TypeScript.tsc.@internal.compiler_package;
using core = github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tsoptions = github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfstest = github.com.microsoft.TypeScript.tsc.@internal.vfs.vfstest_package;
using ast = github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using contentmapper = github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using diagnostics = github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using github.com.microsoft.TypeScript.tsc.@internal;
using github.com.microsoft.TypeScript.tsc.@internal.vfs;
using time = time_package;
using vfs = github.com.microsoft.TypeScript.tsc.@internal.vfs_package;

partial class main_package {

partial struct output {
    internal @string name, text;
}

internal static void main() {
    foreach (var (_, name) in range(os.Args[1..])) {
        var (text, err) = os.ReadFile(name);
        if (err != nil) {
            fmt.Println("read error:", err);
            os.Exit(1);
        }
        @string @base = name.slice(strings.LastIndexAny(name, @"/\") + 1);
        fmt.Printf("file %s (%d bytes)\n"u8, @base, len(text));
        var fs = bundled.WrapFS(vfstest.FromMap(new map<@string, @string>{["/main.ts"] = ((@string)text)}, true));
        var opts = Ꮡ(new core.CompilerOptions(Declaration: core.TSTrue, NewLine: core.NewLineKindLF));
        var config = tsoptions.NewParsedCommandLine(opts, ["/main.ts"], default,
            new tspath.ComparePathsOptions(UseCaseSensitiveFileNames: true, CurrentDirectory: "/"));
        var host = compiler.NewCompilerHost("/", fs, bundled.LibPath(), default, default, default);
        var program = compiler.NewProgram(new compiler.ProgramOptions(Config: config, Host: host, SingleThreaded: core.TSTrue));
        ref var outs = ref heap<slice<output>>(out var Ꮡouts);
        var res = program.Emit(context.Background(), new compiler.EmitOptions(
            WriteFile: (@string fileName, @string textΔ1, ж<compiler.WriteFileData> _) => {
                Ꮡouts.ValueSlot = append(Ꮡouts.ValueSlot, new output(fileName, textΔ1));
                return default;
            }
        ));
        sort.Slice(outs, (nint i, nint j) => Ꮡouts.ValueSlot[i].name < Ꮡouts.ValueSlot[j].name);
        fmt.Printf("skipped %v, diagnostics %d, outputs %d\n"u8, res.EmitSkipped, len(res.Diagnostics), len(outs));
        foreach (var (_, d) in range(res.Diagnostics)) {
            fmt.Printf("diag TS%d [%d,+%d] %s\n"u8, d.Code(), d.Pos(), d.Len(), d.String());
        }
        foreach (var (_, o) in range(outs)) {
            fmt.Printf("emit %s (%d bytes)\n%s\n--- end %s\n"u8, o.name, len(o.text), o.text, o.name);
        }
    }
}

} // end main_package
