namespace go;

using context = context_package;
using fmt = fmt_package;
using os = os_package;
using strings = strings_package;
using ast = github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using bundled = github.com.microsoft.TypeScript.tsc.@internal.bundled_package;
using compiler = github.com.microsoft.TypeScript.tsc.@internal.compiler_package;
using core = github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tsoptions = github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using vfstest = github.com.microsoft.TypeScript.tsc.@internal.vfs.vfstest_package;
using collections = github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using contentmapper = github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using diagnostics = github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using github.com.microsoft.TypeScript.tsc.@internal;
using github.com.microsoft.TypeScript.tsc.@internal.vfs;
using time = time_package;
using vfs = github.com.microsoft.TypeScript.tsc.@internal.vfs_package;

partial class main_package {

internal static readonly @string tsconfig = @"{ ""compilerOptions"": { ""strict"": true }, ""files"": [""main.ts""] }"u8;

internal static void report(@string kind, slice<ж<ast.Diagnostic>> diags) {
    foreach (var (_, d) in range(diags)) {
        @string @file = "";
        if (d.File() != nil) {
            @file = d.File().FileName();
        }
        fmt.Printf("%s TS%d %s [%d,+%d] %s\n"u8, kind, d.Code(), @file, d.Pos(), d.Len(), d.String());
        ref var chain = ref heap<Action<slice<ж<ast.Diagnostic>>, nint>>(out var Ꮡchain);
        chain = (slice<ж<ast.Diagnostic>> ds, nint depth) => {
            foreach (var (_, c) in range(ds)) {
                fmt.Printf("%s  TS%d %s\n"u8, strings.Repeat("  ", depth), c.Code(), c.String());
                Ꮡchain.ValueSlot(c.MessageChain(), depth + 1);
            }
        };
        chain(d.MessageChain(), 1);
    }
}

internal static void main() {
    var ctx = context.Background();
    foreach (var (_, name) in range(os.Args[1..])) {
        var (text, err) = os.ReadFile(name);
        if (err != nil) {
            fmt.Println("read error:", err);
            os.Exit(1);
        }
        fmt.Printf("file %s (%d bytes)\n"u8, name.slice(strings.LastIndexAny(name, @"/\") + 1), len(text));
        var fs = bundled.WrapFS(vfstest.FromMap(new map<@string, @string>{["/main.ts"] = ((@string)text), ["/tsconfig.json"] = tsconfig}, true));
        var host = compiler.NewCompilerHost("/", fs, bundled.LibPath(), default, default, default);
        var (parsed, errs) = tsoptions.GetParsedCommandLineOfConfigFile("/tsconfig.json", Ꮡ(new core.CompilerOptions()), nil, new compiler_CompilerHostᴠParseConfigHost(host), default);
        report("config", errs);
        var p = compiler.NewProgram(new compiler.ProgramOptions(Config: parsed, Host: host, SingleThreaded: core.TSTrue));
        report("options", p.GetConfigFileParsingDiagnostics());
        var @file = p.GetSourceFile("/main.ts");
        report("syntax", p.GetSyntacticDiagnostics(ctx, @file));
        report("global", p.GetGlobalDiagnostics(ctx));
        report("semantic", p.GetSemanticDiagnostics(ctx, @file));
        fmt.Printf("files %d\n"u8, len(p.GetSourceFiles()));
    }
}

} // end main_package
