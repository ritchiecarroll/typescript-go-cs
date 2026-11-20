namespace go;

using context = context_package;
using fmt = fmt_package;
using fs = io.fs_package;
using os = os_package;
using filepath = path.filepath_package;
using sort = sort_package;
using Δsync = sync_package;
using ast = github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using bundled = github.com.microsoft.TypeScript.tsc.@internal.bundled_package;
using compiler = github.com.microsoft.TypeScript.tsc.@internal.compiler_package;
using tsoptions = github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using vfs = github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using vfstest = github.com.microsoft.TypeScript.tsc.@internal.vfs.vfstest_package;
using collections = github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using contentmapper = github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using github.com.microsoft.TypeScript.tsc.@internal;
using github.com.microsoft.TypeScript.tsc.@internal.vfs;
using io;
using path;
using time = time_package;
using static main_package;

partial class main_package {

partial struct sys {
    internal vfs.FS fs;
}

} partial class mainᴛsys {

internal static vfs.FS FS(this ref sys s) { return s.fs; }
internal static @string GetCurrentDirectory(this ref sys s) { return "/proj"; }

} partial class main_package {

partial struct output {
    internal @string name, text;
}

internal static void printDiag(ж<ast.Diagnostic> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    @string @file = "<global>";
    if (d.File() != nil) {
        @file = d.File().FileName();
    }
    fmt.Printf("diag %s TS%d [%d,+%d] %s\n"u8, @file, d.Code(), d.Pos(), d.Len(), d.String());
}

internal static void main() {
    var namesᴷ = new stackAppend<@string>(moves: true);

    foreach (var (_, dir) in range(os.Args[1..])) {
        var files = new map<@string, @string>{};
        var filesʗ1 = files;
        var err = filepath.WalkDir(dir, error (@string p, fs.DirEntry e, error errΔ1) => {
            if (errΔ1 != nil || e.IsDir()) {
                return errΔ1;
            }
            (var b, errΔ1) = os.ReadFile(p);
            if (errΔ1 != nil) {
                return errΔ1;
            }
            var (rel, _) = filepath.Rel(dir, p);
            filesʗ1["/proj/"u8 + filepath.ToSlash(rel)] = (@string)b;
            return default;
        });
        if (err != nil) {
            fmt.Println("read error:", err);
            os.Exit(1);
        }
        fmt.Printf("project %s (%d files)\n"u8, filepath.Base(dir), len(files));
        var s = Ꮡ(new sys(bundled.WrapFS(vfstest.FromMap(files, true))));
        var (config, errs) = tsoptions.GetParsedCommandLineOfConfigFile("/proj/tsconfig.json", nil, nil, new sysжParseConfigHost(s), default);
        foreach (var (_, d) in range(errs)) {
            printDiag(d);
        }
        if (config == nil) {
            continue;
        }
        var host = compiler.NewCompilerHost("/proj", s.fs, bundled.LibPath(), default, default, default);
        var program = compiler.NewProgram(new compiler.ProgramOptions(Config: config, Host: host));
        var ctx = context.Background();
        var names = slice<@string>([]);
        foreach (var (_, f) in range(program.SourceFiles())) {
            if (!bundled.IsBundled(f.FileName())) {
                names = append(namesᴷ, names, f.FileName());
            }
        }
        fmt.Printf("files %d (%d bundled)\n"u8, len(program.SourceFiles()), len(program.SourceFiles()) - len(names));
        names = move2heap(namesᴷ, names);
        foreach (var (_, n) in range(names)) {
            fmt.Printf("source %s\n"u8, n);
        }
        var diags = compiler.SortAndDeduplicateDiagnostics(compiler.GetDiagnosticsOfAnyProgram(ctx, new compiler.ProgramжProgramLike(program), default, false,
            program.GetBindDiagnostics, program.GetSemanticDiagnostics));
        fmt.Printf("diagnostics %d\n"u8, len(diags));
        foreach (var (_, d) in range(diags)) {
            printDiag(d);
        }
        ref var outs = ref heap<slice<output>>(out var Ꮡouts);
        ref var mu = ref heap(new Δsync.Mutex(), out var Ꮡmu);
        var res = program.Emit(ctx, new compiler.EmitOptions(
            WriteFile: (@string fileName, @string text, ж<compiler.WriteFileData> _Δp2) => {
                Ꮡmu.Lock();
                Ꮡouts.ValueSlot = append(Ꮡouts.ValueSlot, new output(fileName, text));
                Ꮡmu.Unlock();
                return default;
            }
        ));
        sort.Slice(outs, (nint i, nint j) => Ꮡouts.ValueSlot[i].name < Ꮡouts.ValueSlot[j].name);
        fmt.Printf("skipped %v, emit diagnostics %d, outputs %d\n"u8, res.EmitSkipped, len(res.Diagnostics), len(outs));
        foreach (var (_, d) in range(res.Diagnostics)) {
            printDiag(d);
        }
        foreach (var (_, o) in range(outs)) {
            fmt.Printf("emit %s (%d bytes)\n%s\n--- end %s\n"u8, o.name, len(o.text), o.text, o.name);
        }
    }
}

} // end main_package
