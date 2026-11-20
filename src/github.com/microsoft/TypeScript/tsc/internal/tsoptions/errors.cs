namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using ꓸꓸꓸany = Span<any>;
using tsoptions = tsoptions_package;
using static tsoptions_package;

partial class tsoptions_package {

internal static ж<ast.Diagnostic> createDiagnosticForInvalidEnumType(ж<CommandLineOption> Ꮡopt, ж<ast.SourceFile> sourceFile, ж<ast.Node> node) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    var namesOfType = slices.Collect(opt.EnumMap().Keys());
    @string stringNames = formatEnumTypeKeys(Ꮡopt, namesOfType);
    @string optName = "--"u8 + opt.Name;
    return CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, node, diagnostics.Argument_for_0_option_must_be_Colon_1, optName, stringNames);
}

internal static @string formatEnumTypeKeys(ж<CommandLineOption> Ꮡopt, slice<@string> keys) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    if (opt.DeprecatedKeys() != nil) {
        keys = core.Filter(keys, (@string key) => !Ꮡopt.Value.DeprecatedKeys().Has(key));
    }
    return "'"u8 + strings.Join(keys, "', '") + "'"u8;
}

internal static @string getCompilerOptionValueTypeString(ж<CommandLineOption> Ꮡoption) {
    ref var option = ref Ꮡoption.DerefOrNull();

    var exprᴛ1 = option.Kind;
    switch (ᐧ) {
    case {} when exprᴛ1 == CommandLineOptionTypeListOrElement: {
        return fmt.Sprintf("%v or Array"u8, getCompilerOptionValueTypeString(option.Elements()));
    }
    case {} when exprᴛ1 == CommandLineOptionTypeList: {
        return "Array";
    }
    default: {
        return (@string)option.Kind;
    }}

}

} partial class tsoptionsᴛcommandLineParser {

internal static ж<ast.Diagnostic> createUnknownOptionError(this ref commandLineParser parser, @string unknownOption, @string unknownOptionErrorText, ж<ast.Node> node, ж<ast.SourceFile> sourceFile) {
    return tsoptions.createUnknownOptionError(
        unknownOption,
        parser.UnknownOptionDiagnostic(),
        unknownOptionErrorText,
        node,
        sourceFile,
        parser.AlternateMode(),
        parser.UnknownDidYouMeanDiagnostic(),
        commandLineOptionsToMap(parser.workerDiagnostics.didYouMean.OptionDeclarations));
}

} partial class tsoptions_package {

internal static ж<ast.Diagnostic> createUnknownOptionError(@string unknownOption, ж<diagnostics.Message> unknownOptionDiagnostic, @string unknownOptionErrorText, ж<ast.Node> node, ж<ast.SourceFile> sourceFile, ж<AlternateModeDiagnostics> ᏑalternateMode, ж<diagnostics.Message> unknownDidYouMeanDiagnostic, CommandLineOptionNameMap optionsNameMap) {
    ref var alternateMode = ref ᏑalternateMode.DerefOrNull();

    if (ᏑalternateMode != nil && alternateMode.optionsNameMap != nil) {
        var otherOption = alternateMode.optionsNameMap.Get(strings.ToLower(unknownOption));
        if (otherOption != nil) {
            var diagnostic = alternateMode.diagnostic;
            if (otherOption.Name == "build"u8) {
                diagnostic = diagnostics.Option_build_must_be_the_first_command_line_argument;
            }
            return CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, node, diagnostic, unknownOption);
        }
    }
    if (unknownOptionErrorText == ""u8) {
        unknownOptionErrorText = unknownOption;
    }
    if (unknownDidYouMeanDiagnostic != nil && optionsNameMap != nil) {
        var possibleOption = optionsNameMap.GetSpellingSuggestion(unknownOption); if (possibleOption != nil) {
            return CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, node, unknownDidYouMeanDiagnostic, unknownOptionErrorText, possibleOption.Name);
        }
    }
    return CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, node, unknownOptionDiagnostic, unknownOptionErrorText);
}

public static ж<ast.Diagnostic> CreateDiagnosticForNodeInSourceFile(ж<ast.SourceFile> ᏑsourceFile, ж<ast.Node> Ꮡnode, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.NewDiagnostic(ᏑsourceFile, core.NewTextRange(scanner.SkipTrivia(sourceFile.Text(), node.Loc.Pos()), node.End()), message, args.ꓸꓸꓸ);
}

public static ж<ast.Diagnostic> CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(ж<ast.SourceFile> sourceFile, ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    if (sourceFile != nil && node != nil) {
        return CreateDiagnosticForNodeInSourceFile(sourceFile, node, message, args.ꓸꓸꓸ);
    }
    return ast.NewCompilerDiagnostic(message, args.ꓸꓸꓸ);
}

internal static ж<diagnostics.Message> extraKeyDiagnostics(@string s) {
    switch (ᐧ) {
    case {} when s == "compilerOptions"u8: {
        return diagnostics.Unknown_compiler_option_0;
    }
    case {} when s == "watchOptions"u8: {
        return diagnostics.Unknown_watch_option_0;
    }
    case {} when s == "typeAcquisition"u8: {
        return diagnostics.Unknown_type_acquisition_option_0;
    }
    case {} when s == "buildOptions"u8: {
        return diagnostics.Unknown_build_option_0;
    }
    default: {
        return default;
    }}

}

internal static ж<diagnostics.Message> extraKeyDidYouMeanDiagnostics(@string s) {
    switch (ᐧ) {
    case {} when s == "compilerOptions"u8: {
        return diagnostics.Unknown_compiler_option_0_Did_you_mean_1;
    }
    case {} when s == "watchOptions"u8: {
        return diagnostics.Unknown_watch_option_0_Did_you_mean_1;
    }
    case {} when s == "typeAcquisition"u8: {
        return diagnostics.Unknown_type_acquisition_option_0_Did_you_mean_1;
    }
    case {} when s == "buildOptions"u8: {
        return diagnostics.Unknown_build_option_0_Did_you_mean_1;
    }
    default: {
        return default;
    }}

}

} // end tsoptions_package
