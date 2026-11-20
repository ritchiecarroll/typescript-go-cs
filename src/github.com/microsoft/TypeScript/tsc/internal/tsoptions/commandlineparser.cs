namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using time = time_package;
using static tsoptions_package;

partial class tsoptions_package {

} partial class tsoptionsᴛcommandLineParser {

internal static ж<AlternateModeDiagnostics> AlternateMode(this ref commandLineParser p) {
    return p.workerDiagnostics.didYouMean.alternateMode;
}

internal static slice<ж<CommandLineOption>> OptionsDeclarations(this ref commandLineParser p) {
    return p.workerDiagnostics.didYouMean.OptionDeclarations;
}

internal static ж<diagnostics.Message> UnknownOptionDiagnostic(this ref commandLineParser p) {
    return p.workerDiagnostics.didYouMean.UnknownOptionDiagnostic;
}

internal static ж<diagnostics.Message> UnknownDidYouMeanDiagnostic(this ref commandLineParser p) {
    return p.workerDiagnostics.didYouMean.UnknownDidYouMeanDiagnostic;
}

} partial class tsoptions_package {

partial struct commandLineParser {
    internal ж<ParseCommandLineWorkerDiagnostics> workerDiagnostics;
    internal ж<NameMap> optionsMap;
    internal Δvfs.FS fs;
    internal @string currentDirectory;
    internal ж<collections.OrderedMap<@string, any>> options;
    internal slice<@string> fileNames;
    internal slice<ж<ast.Diagnostic>> errors;
    internal collections.Set<tspath.Path> responseFileStack;
}

public static ж<ParsedCommandLine> ParseCommandLine(slice<@string> commandLine, ParseConfigHost host) {
    if (commandLine == nil) {
        commandLine = [];
    }
    var parser = parseCommandLineWorker(CompilerOptionsDidYouMeanDiagnostics, commandLine, host.FS(), host.GetCurrentDirectory());
    var options = convertToOptionsWithAbsolutePaths(parser.options.Clone(), CommandLineCompilerOptionsMap, host.GetCurrentDirectory());
    var compilerOptions = ((ж<compilerOptionsParser>)(convertMapToOptions<compilerOptionsParserжoptionParserᴾ>(options, Ꮡ(new compilerOptionsParser(Ꮡ(new core.CompilerOptions())))))).CompilerOptions;
    var watchOptions = ((ж<watchOptionsParser>)(convertMapToOptions<watchOptionsParserжoptionParserᴾ>(options, Ꮡ(new watchOptionsParser(Ꮡ(new core.WatchOptions())))))).WatchOptions;
    var result = NewParsedCommandLine(compilerOptions, parser.fileNames, default, new tspath.ComparePathsOptions(
        UseCaseSensitiveFileNames: host.FS().UseCaseSensitiveFileNames(),
        CurrentDirectory: host.GetCurrentDirectory()
    ));
    result.ParsedConfig.WatchOptions = watchOptions;
    result.Errors = parser.errors;
    result.Raw = parser.options.OrTypedNil();
    return result;
}

public static ж<ParsedBuildCommandLine> ParseBuildCommandLine(slice<@string> commandLine, ParseConfigHost host) {
    if (commandLine == nil) {
        commandLine = [];
    }
    var parser = parseCommandLineWorker(buildOptionsDidYouMeanDiagnostics, commandLine, host.FS(), host.GetCurrentDirectory());
    var compilerOptions = Ꮡ(new core.CompilerOptions());
    foreach (var (key, value) in range<@string, any>(parser.options.Entries().Invoke)) {
        var buildOption = BuildNameMap.Get(key);
        if (buildOption == ᏑTscBuildOption || buildOption == CompilerNameMap.Get(key)) {
            ParseCompilerOptions(key, value, compilerOptions);
        }
    }
    var result = Ꮡ(new ParsedBuildCommandLine(
        BuildOptions: ((ж<buildOptionsParser>)(convertMapToOptions<buildOptionsParserжoptionParserᴾ>(parser.options, Ꮡ(new buildOptionsParser(Ꮡ(new core.BuildOptions())))))).BuildOptions,
        CompilerOptions: compilerOptions,
        WatchOptions: ((ж<watchOptionsParser>)(convertMapToOptions<watchOptionsParserжoptionParserᴾ>(parser.options, Ꮡ(new watchOptionsParser(Ꮡ(new core.WatchOptions())))))).WatchOptions,
        Projects: parser.fileNames,
        Errors: parser.errors,
        Raw: parser.options.OrTypedNil(),

        comparePathsOptions: new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.FS().UseCaseSensitiveFileNames(),
            CurrentDirectory: host.GetCurrentDirectory()
        )
    ));

    if (len(result.Projects) == 0) {
        result.Projects = append(result.Projects, (@string)".");
    }

    if (result.BuildOptions.Clean.IsTrue() && result.BuildOptions.Force.IsTrue()) {
        result.Errors = append(result.Errors, ast.NewCompilerDiagnostic(diagnostics.Options_0_and_1_cannot_be_combined, "clean", "force"));
    }
    if (result.BuildOptions.Clean.IsTrue() && result.BuildOptions.Verbose.IsTrue()) {
        result.Errors = append(result.Errors, ast.NewCompilerDiagnostic(diagnostics.Options_0_and_1_cannot_be_combined, "clean", "verbose"));
    }
    if (result.BuildOptions.Clean.IsTrue() && result.CompilerOptions.Watch.IsTrue()) {
        result.Errors = append(result.Errors, ast.NewCompilerDiagnostic(diagnostics.Options_0_and_1_cannot_be_combined, "clean", "watch"));
    }
    if (result.CompilerOptions.Watch.IsTrue() && result.BuildOptions.Dry.IsTrue()) {
        result.Errors = append(result.Errors, ast.NewCompilerDiagnostic(diagnostics.Options_0_and_1_cannot_be_combined, "watch", "dry"));
    }

    return result;
}

internal static ж<commandLineParser> parseCommandLineWorker(ж<ParseCommandLineWorkerDiagnostics> parseCommandLineWithDiagnostics, slice<@string> commandLine, Δvfs.FS fs, @string currentDirectory) {
    var parser = Ꮡ(new commandLineParser(
        fs: fs,
        currentDirectory: currentDirectory,
        workerDiagnostics: parseCommandLineWithDiagnostics,
        fileNames: [],
        options: Ꮡ(new collections.OrderedMap<@string, any>()),
        errors: []
    ));
    parser.optionsMap = GetNameMapFromList(parser.OptionsDeclarations());
    parser.parseStrings(commandLine);
    return parser;
}

} partial class tsoptionsᴛcommandLineParser {

internal static void parseStrings(this ж<commandLineParser> Ꮡp, slice<@string> args) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint i = 0;
    while (i < len(args)) {
        @string s = args[i];
        i++;
        if (s == ""u8) {
            continue;
        }
        switch (s[0]) {
        case (byte)'@': {
            Ꮡp.parseResponseFile(s[1..]);
            break;
        }
        case (byte)'-': {
            @string inputOptionName = getInputOptionName(s);
            var opt = p.optionsMap.GetOptionDeclarationFromName(inputOptionName, true);
            if (opt != nil) {
                i = p.parseOptionValue(args, i, opt, p.workerDiagnostics.OptionTypeMismatchDiagnostic);
            } else {
                var watchOpt = WatchNameMap.GetOptionDeclarationFromName(inputOptionName, true);
                if (watchOpt != nil) {
                    i = p.parseOptionValue(args, i, watchOpt, watchOptionsDidYouMeanDiagnostics.OptionTypeMismatchDiagnostic);
                } else {
                    p.errors = append(p.errors, p.createUnknownOptionError(inputOptionName, s, nil, nil));
                }
            }
            break;
        }
        default: {
            p.fileNames = append(p.fileNames, s);
            break;
        }}

    }
}

} partial class tsoptions_package {

internal static @string getInputOptionName(@string input) {
    return strings.TrimPrefix(strings.TrimPrefix(input, "-"), "-");
}

} partial class tsoptionsᴛcommandLineParser {

internal static void parseResponseFile(this ж<commandLineParser> Ꮡp, @string fileName) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();
        var argsᴷ = new stackAppend<@string>();

        fileName = tspath.GetNormalizedAbsolutePath(fileName, p.currentDirectory);
        tspath.Path path = tspath.ToPath(fileName, p.currentDirectory, p.fs.UseCaseSensitiveFileNames());
        if (Ꮡp.ᏑresponseFileStack.Has(path)) {
            return;
        }
        p.responseFileStack.Add(path);
        defer(Ꮡp.ᏑresponseFileStack.Delete, path, ref ᒐ);

        var (fileContents, errors) = tryReadFile(fileName, (@string fileNameΔ1) => {
            if (Ꮡp.Value.fs == nil) {
                return ("", false);
            }
            var (read, err) = Ꮡp.Value.fs.ReadFile(fileNameΔ1);
            return (read, err);
        }, p.errors);
        p.errors = errors;

        if (fileContents == ""u8) {
            return;
        }

        slice<@string> args = default;
        var text = slice<rune>(fileContents);
        nint textLength = len(text);
        nint pos = 0;
        while (pos < textLength) {
            while (pos < textLength && text[pos] <= ' ') {
                pos++;
            }
            if (pos >= textLength) {
                break;
            }
            nint start = pos;
            if (text[pos] == '"') {
                pos++;
                while (pos < textLength && text[pos] != '"') {
                    pos++;
                }
                if (pos < textLength) {
                    args = append(argsᴷ, args, (@string)(text.slice(start + 1, pos)));
                    pos++;
                } else {
                    p.errors = append(p.errors, ast.NewCompilerDiagnostic(diagnostics.Unterminated_quoted_string_in_response_file_0, fileName));
                }
            } else {
                while (pos < textLength && text[pos] > ' ') {
                    pos++;
                }
                args = append(args, (@string)(text.slice(start, pos)));
            }
        }
        Ꮡp.parseStrings(args);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} partial class tsoptions_package {

internal static (@string, slice<ж<ast.Diagnostic>>) tryReadFile(@string fileName, Func<@string, (@string, bool)> readFile, slice<ж<ast.Diagnostic>> errors) {
    var (text, e) = readFile(fileName);

    if (!e) {
        text = "";
        errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Cannot_read_file_0, fileName));
    }
    return (text, errors);
}

} partial class tsoptionsᴛcommandLineParser {

internal static nint parseOptionValue(this ref commandLineParser p, slice<@string> args, nint i, ж<CommandLineOption> Ꮡopt, ж<diagnostics.Message> diag) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    if (opt.IsTSConfigOnly && i <= len(args)) {
        @string optValue = "";
        if (i < len(args)) {
            optValue = args[i];
        }
        if (optValue == "null"u8) {
            p.options.Set(opt.Name, default);
            i++;
        } else if (opt.Kind == "boolean"u8) {
            if (optValue == "false"u8) {
                p.options.Set(opt.Name, false);
                i++;
            } else {
                if (optValue == "true"u8) {
                    i++;
                }
                p.errors = append(p.errors, ast.NewCompilerDiagnostic(diagnostics.Option_0_can_only_be_specified_in_tsconfig_json_file_or_set_to_false_or_null_on_command_line, opt.Name));
            }
        } else {
            p.errors = append(p.errors, ast.NewCompilerDiagnostic(diagnostics.Option_0_can_only_be_specified_in_tsconfig_json_file_or_set_to_null_on_command_line, opt.Name));
            if (len(optValue) != 0 && !strings.HasPrefix(optValue, "-")) {
                i++;
            }
        }
    } else {
        if (i >= len(args)) {
            if (opt.Kind != "boolean"u8) {
                p.errors = append(p.errors, ast.NewCompilerDiagnostic(diag, opt.Name, getCompilerOptionValueTypeString(Ꮡopt)));
                if (opt.Kind == "list"u8) {
                    p.options.Set(opt.Name, slice<@string>([]));
                } else if (opt.Kind == "enum"u8) {
                    p.errors = append(p.errors, createDiagnosticForInvalidEnumType(Ꮡopt, nil, nil));
                }
            } else {
                p.options.Set(opt.Name, true);
            }
            return i;
        }
        if (args[i] != "null") {
            var exprᴛ1 = opt.Kind;
            switch (ᐧ) {
            case {} when exprᴛ1 == (CommandLineOptionKind)("number"u8): {
                var (num, e) = strconv.Atoi(args[i]);
                if (e == nil) {
                    if (num >= opt.minValue) {
                        p.options.Set(opt.Name, num);
                    } else {
                        p.errors = append(p.errors, ast.NewCompilerDiagnostic(diagnostics.Option_0_requires_value_to_be_greater_than_1, opt.Name, strconv.Itoa(opt.minValue)));
                    }
                } else {
                    p.errors = append(p.errors, ast.NewCompilerDiagnostic(diag, opt.Name, "number"));
                }
                i++;
                break;
            }
            case {} when exprᴛ1 == (CommandLineOptionKind)("boolean"u8): {
                @string optValue = args[i];

                if (optValue == "false"u8) {
                    p.options.Set(opt.Name, false);
                } else {
                    p.options.Set(opt.Name, true);
                }
                if (optValue == "false"u8 || optValue == "true"u8) {
                    i++;
                }
                break;
            }
            case {} when exprᴛ1 == (CommandLineOptionKind)("string"u8): {
                var (val, err) = validateJsonOptionValue(ref (Ꮡopt).DerefOrNull(), args[i], nil, nil);
                if (err == nil) {
                    p.options.Set(opt.Name, val);
                } else {
                    p.errors = appendꓸꓸꓸ(p.errors, err);
                }
                i++;
                break;
            }
            case {} when exprᴛ1 == (CommandLineOptionKind)("list"u8): {
                var (result, err) = p.parseListTypeOption(Ꮡopt, args[i]);
                p.options.Set(opt.Name, result);
                p.errors = appendꓸꓸꓸ(p.errors, err);
                if (len(result) > 0 || len(err) > 0) {
                    i++;
                }
                break;
            }
            case {} when exprᴛ1 == (CommandLineOptionKind)("listOrElement"u8): {
                throw panic("listOrElement not supported here");
                break;
            }
            default: {
                var (val, err) = convertJsonOptionOfEnumType(Ꮡopt, strings.TrimFunc(args[i], stringutil.IsWhiteSpaceLike), nil, nil);
                p.options.Set(opt.Name, val);
                p.errors = appendꓸꓸꓸ(p.errors, err);
                i++;
                break;
            }}

        } else {
            p.options.Set(opt.Name, default);
            i++;
        }
    }
    return i;
}

internal static (slice<any>, slice<ж<ast.Diagnostic>>) parseListTypeOption(this ref commandLineParser p, ж<CommandLineOption> opt, @string value) {
    return ParseListTypeOption(opt, value);
}

} partial class tsoptions_package {

public static (slice<any>, slice<ж<ast.Diagnostic>>) ParseListTypeOption(ж<CommandLineOption> Ꮡopt, @string value) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    value = strings.TrimSpace(value);
    ref var errors = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡerrors);
    if (strings.HasPrefix(value, "-")) {
        return (slice<any>([]), errors);
    }
    if (opt.Kind == "listOrElement"u8 && !strings.ContainsRune(value, ',')) {
        var (val, err) = validateJsonOptionValue(ref (Ꮡopt).DerefOrNull(), value, nil, nil);
        if (err != nil) {
            return (slice<any>([]), err);
        }
        return (slice<any>([val._<@string>()]), errors);
    }
    if (value == ""u8) {
        return (slice<any>([]), errors);
    }
    var values = strings.Split(value, ",");
    var exprᴛ1 = opt.Elements().Kind;
    switch (ᐧ) {
    case {} when exprᴛ1 == (CommandLineOptionKind)("string"u8): {
        var elements = core.MapFiltered(values, (any, bool) (@string v) => {
            var (val, err) = validateJsonOptionValue(ref (Ꮡopt.Value.Elements()).DerefOrNull(), v, nil, nil);
            var (s, ok) = val._<@string>(ᐧ); if (ok && len(err) == 0 && s != ""u8) {
                return (s, true);
            }
            Ꮡerrors.ValueSlot = appendꓸꓸꓸ(Ꮡerrors.ValueSlot, err);
            return ((@string)""u8, false);
        });
        return (elements, errors);
    }
    case {} when exprᴛ1 == (CommandLineOptionKind)("boolean"u8) || exprᴛ1 == (CommandLineOptionKind)("object"u8) || exprᴛ1 == (CommandLineOptionKind)("number"u8): {
        throw panic("List of "u8 + opt.Elements().Kind + " is not yet supported."u8);
        break;
    }
    default: {
        var result = core.MapFiltered(values, (any, bool) (@string v) => {
            var (val, err) = convertJsonOptionOfEnumType(Ꮡopt.Value.Elements(), strings.TrimFunc(v, stringutil.IsWhiteSpaceLike), nil, nil);
            var (s, ok) = val._<@string>(ᐧ); if (ok && len(err) == 0 && s != ""u8) {
                return (s, true);
            }
            Ꮡerrors.ValueSlot = appendꓸꓸꓸ(Ꮡerrors.ValueSlot, err);
            return ((@string)""u8, false);
        });
        return (result, errors);
    }}

}

internal static (any, slice<ж<ast.Diagnostic>>) convertJsonOptionOfEnumType(ж<CommandLineOption> Ꮡopt, @string value, ж<astꓸExpression> valueExpression, ж<ast.SourceFile> sourceFile) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    if (value == ""u8) {
        return (default, default);
    }
    @string key = strings.ToLower(value);
    var typeMap = opt.EnumMap();
    if (typeMap == nil) {
        return (default, default);
    }
    var (val, ok) = typeMap.Get(key);
    if (ok) {
        return validateJsonOptionValue(ref (Ꮡopt).DerefOrNull(), val, valueExpression, sourceFile);
    }
    return (default, slice<ж<ast.Diagnostic>>([createDiagnosticForInvalidEnumType(Ꮡopt, sourceFile, valueExpression)]));
}

} // end tsoptions_package
