namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using sync = sync_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class tsoptions_package {

partial struct DidYouMeanOptionsDiagnostics {
    internal ж<AlternateModeDiagnostics> alternateMode;
    public slice<ж<CommandLineOption>> OptionDeclarations;
    public ж<diagnostics.Message> UnknownOptionDiagnostic;
    public ж<diagnostics.Message> UnknownDidYouMeanDiagnostic;
}

partial struct AlternateModeDiagnostics {
    internal ж<diagnostics.Message> diagnostic;
    internal ж<NameMap> optionsNameMap;
}

partial struct ParseCommandLineWorkerDiagnostics {
    internal DidYouMeanOptionsDiagnostics didYouMean;
    internal ж<NameMap> optionsNameMap;
    internal sync.Once optionsNameMapOnce;
    public ж<diagnostics.Message> OptionTypeMismatchDiagnostic;
}

public static ж<ParseCommandLineWorkerDiagnostics> CompilerOptionsDidYouMeanDiagnostics;
internal static void initᴛCompilerOptionsDidYouMeanDiagnostics() { CompilerOptionsDidYouMeanDiagnostics = getParseCommandLineWorkerDiagnostics(OptionsDeclarations); }

internal static ж<ParseCommandLineWorkerDiagnostics> getParseCommandLineWorkerDiagnostics(slice<ж<CommandLineOption>> decls) {
    return Ꮡ(new ParseCommandLineWorkerDiagnostics(
        didYouMean: new DidYouMeanOptionsDiagnostics(
            alternateMode: Ꮡ(new AlternateModeDiagnostics(
                diagnostic: diagnostics.Compiler_option_0_may_only_be_used_with_build,
                optionsNameMap: BuildNameMap
            )),
            OptionDeclarations: decls,
            UnknownOptionDiagnostic: diagnostics.Unknown_compiler_option_0,
            UnknownDidYouMeanDiagnostic: diagnostics.Unknown_compiler_option_0_Did_you_mean_1
        ),
        OptionTypeMismatchDiagnostic: diagnostics.Compiler_option_0_expects_an_argument
    ));
}

internal static ж<ParseCommandLineWorkerDiagnostics> watchOptionsDidYouMeanDiagnostics;
internal static void initᴛwatchOptionsDidYouMeanDiagnostics() { watchOptionsDidYouMeanDiagnostics = Ꮡ(new ParseCommandLineWorkerDiagnostics(
    didYouMean: new DidYouMeanOptionsDiagnostics(
        OptionDeclarations: OptionsForWatch,
        UnknownOptionDiagnostic: diagnostics.Unknown_watch_option_0,
        UnknownDidYouMeanDiagnostic: diagnostics.Unknown_watch_option_0_Did_you_mean_1
    ),
    OptionTypeMismatchDiagnostic: diagnostics.Watch_option_0_requires_a_value_of_type_1
)); }

internal static ж<ParseCommandLineWorkerDiagnostics> buildOptionsDidYouMeanDiagnostics;
internal static void initᴛbuildOptionsDidYouMeanDiagnostics() { buildOptionsDidYouMeanDiagnostics = Ꮡ(new ParseCommandLineWorkerDiagnostics(
    didYouMean: new DidYouMeanOptionsDiagnostics(
        alternateMode: Ꮡ(new AlternateModeDiagnostics(
            diagnostic: diagnostics.Compiler_option_0_may_not_be_used_with_build,
            optionsNameMap: CompilerNameMap
        )),
        OptionDeclarations: BuildOpts,
        UnknownOptionDiagnostic: diagnostics.Unknown_build_option_0,
        UnknownDidYouMeanDiagnostic: diagnostics.Unknown_build_option_0_Did_you_mean_1
    ),
    OptionTypeMismatchDiagnostic: diagnostics.Build_option_0_requires_a_value_of_type_1
)); }

} // end tsoptions_package
