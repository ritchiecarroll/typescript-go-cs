namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using cmp = cmp_package;
using maps = maps_package;
using reflect = reflect_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using parser = go.github.com.microsoft.TypeScript.tsc.@internal.parser_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using vfsmatch = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.vfsmatch_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;
using iter = iter_package;
using time = time_package;
using ꓸꓸꓸany = Span<any>;
using ꓸꓸꓸstring = Span<@string>;
using static tsoptions_package;

partial class tsoptions_package {

partial struct extendsResult {
    internal ж<core.CompilerOptions> options;
    internal slice<any> include;
    internal slice<any> exclude;
    internal slice<any> files;
    internal slice<any> contentMappers;
    internal bool compileOnSave;
    internal collections.Set<@string> extendedSourceFiles;
}

internal static ж<CommandLineOption> compilerOptionsDeclaration;
internal static void initᴛcompilerOptionsDeclaration() { compilerOptionsDeclaration = Ꮡ(new CommandLineOption(
    Name: "compilerOptions",
    Kind: CommandLineOptionTypeObject,
    ElementOptions: CommandLineCompilerOptionsMap
)); }

internal static ж<CommandLineOption> compileOnSaveCommandLineOption;
internal static void initᴛcompileOnSaveCommandLineOption() { compileOnSaveCommandLineOption = Ꮡ(new CommandLineOption(
    Name: "compileOnSave",
    Kind: CommandLineOptionTypeBoolean,
    DefaultValueDescription: false
)); }

internal static ж<CommandLineOption> extendsOptionDeclaration;
internal static void initᴛextendsOptionDeclaration() { extendsOptionDeclaration = Ꮡ(new CommandLineOption(
    Name: "extends",
    Kind: CommandLineOptionTypeListOrElement,
    Category: diagnostics.File_Management,
    ElementOptions: commandLineOptionsToMap([
        Ꮡ(new CommandLineOption(Name: "extends", Kind: CommandLineOptionTypeString))
    ])
)); }

internal static ж<CommandLineOption> tsconfigRootOptionsMap;
internal static void initᴛtsconfigRootOptionsMap() { tsconfigRootOptionsMap = Ꮡ(new CommandLineOption(
    Name: "undefined",
    Kind: CommandLineOptionTypeObject,
    ElementOptions: commandLineOptionsToMap([
        compilerOptionsDeclaration,
        typeAcquisitionDeclaration,
        extendsOptionDeclaration,
        Ꮡ(new CommandLineOption(
            Name: "references",
            Kind: CommandLineOptionTypeList)),
        Ꮡ(new CommandLineOption(
            Name: "contentMappers",
            Kind: CommandLineOptionTypeList)),
        Ꮡ(new CommandLineOption(
            Name: "files",
            Kind: CommandLineOptionTypeList)),
        Ꮡ(new CommandLineOption(
            Name: "include",
            Kind: CommandLineOptionTypeList)),
        Ꮡ(new CommandLineOption(
            Name: "exclude",
            Kind: CommandLineOptionTypeList)),
        compileOnSaveCommandLineOption
    ])
)); }

partial struct configFileSpecs {
    internal any filesSpecs;
    internal any includeSpecs;
    internal any excludeSpecs;
    internal slice<@string> validatedFilesSpec;
    internal slice<@string> validatedIncludeSpecs;
    internal slice<@string> validatedExcludeSpecs;
    internal slice<@string> validatedFilesSpecBeforeSubstitution;
    internal slice<@string> validatedIncludeSpecsBeforeSubstitution;
    internal bool isDefaultIncludeSpec;
}

} partial class tsoptionsᴛconfigFileSpecs {

internal static bool matchesExclude(this ref configFileSpecs c, @string fileName, tspath.ComparePathsOptions comparePathsOptions) {
    if (len(c.validatedExcludeSpecs) == 0) {
        return false;
    }
    var excludeMatcher = vfsmatch.NewSpecMatcher(c.validatedExcludeSpecs, comparePathsOptions.CurrentDirectory, vfsmatch.UsageExclude, comparePathsOptions.UseCaseSensitiveFileNames);
    if (excludeMatcher == nil) {
        return false;
    }
    if (excludeMatcher.MatchString(fileName)) {
        return true;
    }
    if (!tspath.HasExtension(fileName)) {
        if (excludeMatcher.MatchString(tspath.EnsureTrailingDirectorySeparator(fileName))) {
            return true;
        }
    }
    return false;
}

internal static @string getMatchedIncludeSpec(this ref configFileSpecs c, @string fileName, tspath.ComparePathsOptions comparePathsOptions) {
    if (len(c.validatedIncludeSpecs) == 0) {
        return "";
    }
    foreach (var (index, spec) in range(c.validatedIncludeSpecs)) {
        var includeMatcher = vfsmatch.NewSpecMatcher([spec], comparePathsOptions.CurrentDirectory, vfsmatch.UsageFiles, comparePathsOptions.UseCaseSensitiveFileNames);
        if (includeMatcher != nil && includeMatcher.MatchString(fileName)) {
            return c.validatedIncludeSpecsBeforeSubstitution[index];
        }
    }
    return "";
}

internal static @string getMatchedFileSpec(this ref configFileSpecs c, @string fileName, tspath.ComparePathsOptions comparePathsOptions) {
    if (len(c.validatedFilesSpec) == 0) {
        return "";
    }
    tspath.Path filePath = tspath.ToPath(fileName, comparePathsOptions.CurrentDirectory, comparePathsOptions.UseCaseSensitiveFileNames);
    foreach (var (index, spec) in range(c.validatedFilesSpec)) {
        if (tspath.ToPath(spec, comparePathsOptions.CurrentDirectory, comparePathsOptions.UseCaseSensitiveFileNames) == filePath) {
            return c.validatedFilesSpecBeforeSubstitution[index];
        }
    }
    return "";
}

} partial class tsoptions_package {

partial interface ExtendedConfigCache {
    ж<ExtendedConfigCacheEntry> GetExtendedConfig(@string fileName, tspath.Path path, slice<tspath.Path> resolutionStack, ParseConfigHost host);
}

partial struct ExtendedConfigCacheEntry {
    internal ж<TsConfigSourceFile> extendedResult;
    internal ж<parsedTsconfig> extendedConfig;
    internal slice<ж<ast.Diagnostic>> errors;
}

} partial class tsoptionsᴛExtendedConfigCacheEntry {

public static slice<@string> ExtendedFileNames(this ref ExtendedConfigCacheEntry e) {
    if (e.extendedResult != nil) {
        return e.extendedResult.ExtendedSourceFiles;
    }
    return default;
}

} partial class tsoptions_package {

partial struct parsedTsconfig {
    internal any raw;
    internal ж<core.CompilerOptions> options;
    internal ж<core.TypeAcquisition> typeAcquisition;
    internal any extendedConfigPath;
}

internal static (ж<parsedTsconfig>, slice<ж<ast.Diagnostic>>) parseOwnConfigOfJsonSourceFile(ж<ast.SourceFile> sourceFile, ParseConfigHost host, @string basePath, @string configFileName) {
    var errorsᴷ = new stackAppend<ж<ast.Diagnostic>>(moves: true);

    var compilerOptions = getDefaultCompilerOptions(configFileName);
    var typeAcquisition = getDefaultTypeAcquisition(configFileName);
    ref var extendedConfigPath = ref heap<any>(out var ᏑextendedConfigPath);
    ref var rootCompilerOptions = ref heap<slice<ж<astꓸPropertyName>>>(out var ᏑrootCompilerOptions);
    slice<ж<ast.Diagnostic>> errors = default;
    var compilerOptionsʗ1 = compilerOptions;
    var typeAcquisitionʗ1 = typeAcquisition;
    var onPropertySet = (@string keyText, any value, ж<ast.PropertyAssignment> propertyAssignment, ж<CommandLineOption> parentOption, ж<CommandLineOption> option) => {
        slice<ж<ast.Diagnostic>> propertySetErrors = default;
        if (option != nil && option != extendsOptionDeclaration) {
            (value, propertySetErrors) = convertJsonOption(option, value, basePath, propertyAssignment, (~propertyAssignment).Initializer, sourceFile);
        }
        if (parentOption != nil && parentOption.Name != "undefined"u8 && value != nil) {
            if (option != nil && option.Name != ""u8) {
                slice<ж<ast.Diagnostic>> parseDiagnostics = default;
                var exprᴛ1 = parentOption.Name;
                switch (ᐧ) {
                case {} when exprᴛ1 == "compilerOptions"u8: {
                    parseDiagnostics = ParseCompilerOptions(option.Name, value, compilerOptionsʗ1);
                    break;
                }
                case {} when exprᴛ1 == "typeAcquisition"u8: {
                    parseDiagnostics = ParseTypeAcquisition(option.Name, value, typeAcquisitionʗ1);
                    break;
                }}

                propertySetErrors = appendꓸꓸꓸ(propertySetErrors, parseDiagnostics);
            } else if (keyText != ""u8 && extraKeyDiagnostics(parentOption.Name) != nil) {
                var unknownNameDiag = extraKeyDiagnostics(parentOption.Name);
                if (parentOption.ElementOptions != nil) {
                    var possibleOption = parentOption.ElementOptions.Get(keyText);
                    if (possibleOption == nil) {
                        possibleOption = parentOption.ElementOptions.GetSpellingSuggestion(keyText);
                    }
                    if (possibleOption != nil && possibleOption.Name != keyText) {
                        propertySetErrors = append(propertySetErrors, CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(
                            sourceFile,
                            propertyAssignment.Name(),
                            extraKeyDidYouMeanDiagnostics(parentOption.Name),
                            keyText,
                            possibleOption.Name));
                    } else {
                        propertySetErrors = append(propertySetErrors, createUnknownOptionError(
                            keyText,
                            unknownNameDiag,
                            "",
                            propertyAssignment.Name(),
                            sourceFile,
                            nil,
                            nil,
                            default));
                    }
                } else {
                }
            }
        } else if (parentOption == tsconfigRootOptionsMap) {
            if (option == extendsOptionDeclaration) {
                var (configPath, errΔ1) = getExtendsConfigPathOrArray(value, host, basePath, configFileName, propertyAssignment, (~propertyAssignment).Initializer, sourceFile);
                ᏑextendedConfigPath.ValueSlot = configPath;
                propertySetErrors = appendꓸꓸꓸ(propertySetErrors, errΔ1);
            } else if (option == nil) {
                if (keyText == "excludes"u8) {
                    propertySetErrors = append(propertySetErrors, CreateDiagnosticForNodeInSourceFile(sourceFile, propertyAssignment.Name(), diagnostics.Unknown_option_excludes_Did_you_mean_exclude));
                }
                if (core.Find(optionsForCompiler, (ж<CommandLineOption> optionΔ1) => optionΔ1.Name == keyText) != nil) {
                    ᏑrootCompilerOptions.ValueSlot = append(ᏑrootCompilerOptions.ValueSlot, propertyAssignment.Name());
                }
            }
        }
        return (value, propertySetErrors);
    };

    var (json, err) = convertConfigFileToObject(
        sourceFile,
        Ꮡ(new jsonConversionNotifier(
            tsconfigRootOptionsMap,
            onPropertySet
        )));
    errors = appendꓸꓸꓸ(errors, err);
    var (jsonObject, ok) = json._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (len(rootCompilerOptions) != 0 && ok && !jsonObject.Has(compilerOptionsˢ)) {
        errors = append(errorsᴷ, errors, CreateDiagnosticForNodeInSourceFile(
            sourceFile,
            rootCompilerOptions[0],
            diagnostics.X_0_should_be_set_inside_the_compilerOptions_object_of_the_config_json_file,
            ast.GetTextOfPropertyName(rootCompilerOptions[0])));
    }
    errors = move2heap(errorsᴷ, errors);
    return (Ꮡ(new parsedTsconfig(
        raw: json,
        options: compilerOptions,
        typeAcquisition: typeAcquisition,
        extendedConfigPath: extendedConfigPath
    )), errors);
}

partial struct TsConfigSourceFile {
    public slice<@string> ExtendedSourceFiles;
    internal ж<configFileSpecs> configFileSpecs;
    public ж<ast.SourceFile> SourceFile;
}

internal static ж<ast.SourceFile> tsconfigToSourceFile(ж<TsConfigSourceFile> ᏑtsconfigSourceFile) {
    ref var tsconfigSourceFile = ref ᏑtsconfigSourceFile.DerefOrNull();

    if (ᏑtsconfigSourceFile == nil) {
        return default;
    }
    return tsconfigSourceFile.SourceFile;
}

public static ж<TsConfigSourceFile> NewTsconfigSourceFileFromFilePath(@string configFileName, tspath.Path configPath, @string configSourceText) {
    var sourceFile = parser.ParseSourceFile(new ast.SourceFileParseOptions(
        FileName: configFileName,
        Path: configPath
    ), configSourceText, core.ScriptKindJSON);
    return Ꮡ(new TsConfigSourceFile(
        SourceFile: sourceFile
    ));
}

partial struct jsonConversionNotifier {
    internal ж<CommandLineOption> rootOptions;
    internal Func<@string, any, ж<ast.PropertyAssignment>, ж<CommandLineOption>, ж<CommandLineOption>, (any, slice<ж<ast.Diagnostic>>)> onPropertySet;
}

internal static (any, slice<ж<ast.Diagnostic>>) convertConfigFileToObject(ж<ast.SourceFile> ᏑsourceFile, ж<jsonConversionNotifier> jsonConversionNotifier) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    ж<astꓸExpression> rootExpression = default;
    if (len(sourceFile.Statements.Nodes) > 0) {
        rootExpression = sourceFile.Statements.Nodes[0].Expression();
    }
    if (rootExpression != nil && rootExpression.Kind != ast.KindObjectLiteralExpression) {
        @string baseFileName = "tsconfig.json";
        if (tspath.GetBaseFileName(sourceFile.FileName()) == "jsconfig.json"u8) {
            baseFileName = "jsconfig.json";
        }
        var errors = slice<ж<ast.Diagnostic>>([CreateDiagnosticForNodeInSourceFile(ᏑsourceFile, rootExpression, diagnostics.The_root_value_of_a_0_file_must_be_an_object, baseFileName)]);
        if (ast.IsArrayLiteralExpression(rootExpression)) {
            var firstObject = core.Find<ж<ast.Node>>(rootExpression.Elements(), ast.IsObjectLiteralExpression);
            if (firstObject != nil) {
                return convertToJson(ᏑsourceFile, firstObject, true, jsonConversionNotifier);
            }
        }
        return (Ꮡ(new collections.OrderedMap<@string, any>()), errors);
    }
    return convertToJson(ᏑsourceFile, rootExpression, true, jsonConversionNotifier);
}

internal static reflect.Type orderedMapType = reflect.TypeFor<ж<collections.OrderedMap<@string, any>>>();

internal static bool isCompilerOptionsValue(ж<CommandLineOption> Ꮡoption, any value) {
    ref var option = ref Ꮡoption.DerefOrNull();

    if (Ꮡoption != nil) {
        if (value == nil) {
            return !option.DisallowNullOrUndefined();
        }
        if (option.Kind == "list"u8) {
            return reflect.TypeOf(value).Kind() == reflect.Slice;
        }
        if (option.Kind == "listOrElement"u8) {
            if (reflect.TypeOf(value).Kind() == reflect.Slice) {
                return true;
            } else {
                return isCompilerOptionsValue(option.Elements(), value);
            }
        }
        if (option.Kind == "string"u8) {
            return reflect.TypeOf(value).Kind() == reflect.String;
        }
        if (option.Kind == "boolean"u8) {
            return reflect.TypeOf(value).Kind() == reflect.Bool;
        }
        if (option.Kind == "number"u8) {
            return reflect.TypeOf(value).Kind() == reflect.Float64;
        }
        if (option.Kind == "object"u8) {
            return AreEqual(reflect.TypeOf(value), orderedMapType);
        }
        if (option.Kind == "enum"u8 && reflect.TypeOf(value).Kind() == reflect.String) {
            return true;
        }
    }
    return false;
}

internal static (any, slice<ж<ast.Diagnostic>>) validateJsonOptionValue(ref CommandLineOption opt, any val, ж<astꓸExpression> valueExpression, ж<ast.SourceFile> sourceFile) {
    if (val == nil) {
        return (default, default);
    }

    slice<ж<ast.Diagnostic>> errors = default;

    var exprᴛ1 = opt.extraValidation;
    switch (ᐧ) {
    case {} when exprᴛ1 == extraValidationSpec: {
        var diag = specToDiagnostic(val._<@string>(), false); if (diag != nil) {
            errors = append(errors, CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, valueExpression, diag));
        }
        break;
    }
    case {} when exprᴛ1 == extraValidationLocale: {
        var (_, ok) = locale.Parse(val._<@string>()); if (!ok) {
            errors = append(errors, CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, valueExpression, diagnostics.Locale_must_be_an_IETF_BCP_47_language_tag_Examples_Colon_0_1, "en", "ja-jp"));
        }
        break;
    }}

    if (len(errors) > 0) {
        return (default, errors);
    }
    return (val, default);
}

internal static (slice<any>, slice<ж<ast.Diagnostic>>) convertJsonOptionOfListType(ж<CommandLineOption> Ꮡoption, any values, @string basePath, ж<ast.PropertyAssignment> propertyAssignment, ж<ast.Node> valueExpression, ж<ast.SourceFile> sourceFile) {
    ref var option = ref Ꮡoption.DerefOrNull();

    ref var expression = ref heap<ж<ast.Node>>(out var Ꮡexpression);
    ref var errors = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡerrors);
    {
        var (valuesΔ1, ok) = values._<slice<any>>(ᐧ); if (ok) {
            var mappedValues = core.MapIndex(valuesΔ1, any (any v, nint index) => {
                if (valueExpression != nil) {
                    Ꮡexpression.ValueSlot = valueExpression.Value.Elements()[index];
                }
                var (result, err) = convertJsonOption(Ꮡoption.Value.Elements(), v, basePath, propertyAssignment, Ꮡexpression.ValueSlot, sourceFile);
                Ꮡerrors.ValueSlot = appendꓸꓸꓸ(Ꮡerrors.ValueSlot, err);
                return result;
            });
            var filteredValues = mappedValues;
            if (!option.listPreserveFalsyValues) {
                filteredValues = core.Filter(mappedValues, (any v) => (v != nil && !AreEqual(v, false) && !AreEqual(v, (nint)0) && !AreEqual(v, (@string)(""))));
            }
            return (filteredValues, errors);
        }
    }
    return (default, errors);
}

internal static readonly @string configDirTemplate = "${configDir}"u8;

internal static bool startsWithConfigDirTemplate(any value) {
    var (str, ok) = value._<@string>(ᐧ);
    if (!ok) {
        return false;
    }
    return strings.HasPrefix(strings.ToLower(str), strings.ToLower(configDirTemplate));
}

internal static any normalizeNonListOptionValue(ref CommandLineOption option, @string basePath, any value) {
    if (option.IsFilePath) {
        value = tspath.NormalizeSlashes(value._<@string>());
        if (!startsWithConfigDirTemplate(value)) {
            value = tspath.GetNormalizedAbsolutePath(value._<@string>(), basePath);
        }
        if (AreEqual(value, (@string)(""))) {
            value = (@string)".";
        }
    }
    return value;
}

internal static (any, slice<ж<ast.Diagnostic>>) convertJsonOption(ж<CommandLineOption> Ꮡopt, any value, @string basePath, ж<ast.PropertyAssignment> ᏑpropertyAssignment, ж<astꓸExpression> valueExpression, ж<ast.SourceFile> sourceFile) {
    ref var opt = ref Ꮡopt.DerefOrNull();
    ref var propertyAssignment = ref ᏑpropertyAssignment.DerefOrNull();

    if (opt.IsCommandLineOnly) {
        ж<ast.Node> nodeValue = default;
        if (ᏑpropertyAssignment != nil) {
            nodeValue = propertyAssignment.Name();
        }
        if (sourceFile == nil && nodeValue == nil) {
            return (default, slice<ж<ast.Diagnostic>>([ast.NewCompilerDiagnostic(diagnostics.Option_0_can_only_be_specified_on_command_line, opt.Name)]));
        } else {
            return (default, slice<ж<ast.Diagnostic>>([CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, nodeValue, diagnostics.Option_0_can_only_be_specified_on_command_line, opt.Name)]));
        }
    }
    if (isCompilerOptionsValue(Ꮡopt, value)) {
        var exprᴛ1 = opt.Kind;
        switch (ᐧ) {
        case {} when exprᴛ1 == CommandLineOptionTypeList: {
            var (ᴛ1, ᴛ2) = convertJsonOptionOfListType(Ꮡopt, value, basePath, ᏑpropertyAssignment, valueExpression, sourceFile);
            return (ᴛ1, ᴛ2);
        }
        case {} when exprᴛ1 == CommandLineOptionTypeListOrElement: {
            if (reflect.TypeOf(value).Kind() == reflect.Slice) {
                var (ᴛ1, ᴛ2) = convertJsonOptionOfListType(Ꮡopt, value, basePath, ᏑpropertyAssignment, valueExpression, sourceFile);
                return (ᴛ1, ᴛ2);
            } else {
                return convertJsonOption(opt.Elements(), value, basePath, ᏑpropertyAssignment, valueExpression, sourceFile);
            }
            break;
        }
        case {} when exprᴛ1 == CommandLineOptionTypeEnum: {
            if (value == nil) {
                return (default, default);
            }
            return convertJsonOptionOfEnumType(Ꮡopt, value._<@string>(), valueExpression, sourceFile);
        }}

        var (validatedValue, errors) = validateJsonOptionValue(ref (Ꮡopt).DerefOrNull(), value, valueExpression, sourceFile);
        if (len(errors) > 0 || validatedValue == nil) {
            return (validatedValue, errors);
        } else {
            return (normalizeNonListOptionValue(ref (Ꮡopt).DerefOrNull(), basePath, validatedValue), errors);
        }
    } else {
        return (default, slice<ж<ast.Diagnostic>>([CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(sourceFile, valueExpression, diagnostics.Compiler_option_0_requires_a_value_of_type_1, opt.Name, getCompilerOptionValueTypeString(Ꮡopt))]));
    }
}

internal static (slice<@string>, slice<ж<ast.Diagnostic>>) getExtendsConfigPathOrArray(CompilerOptionsValue value, ParseConfigHost host, @string basePath, @string configFileName, ж<ast.PropertyAssignment> propertyAssignment, ж<astꓸExpression> ᏑvalueExpression, ж<ast.SourceFile> sourceFile) {
    ref var valueExpression = ref ᏑvalueExpression.DerefOrNull();

    slice<@string> extendedConfigPathArray = default;
    @string newBase = basePath;
    if (configFileName != ""u8) {
        newBase = directoryOfCombinedPath(configFileName, basePath);
    }
    if (value == nil) {
        var (_, errorsΔ1) = convertJsonOption(extendsOptionDeclaration, value, basePath, propertyAssignment, ᏑvalueExpression, sourceFile);
        return (extendedConfigPathArray, errorsΔ1);
    }
    if (reflect.TypeOf(value).Kind() == reflect.String) {
        var (val, err) = getExtendsConfigPath(value._<@string>(), host, newBase, ᏑvalueExpression, sourceFile);
        if (val != ""u8) {
            extendedConfigPathArray = append(extendedConfigPathArray, val);
        }
        return (extendedConfigPathArray, err);
    }
    slice<ж<ast.Diagnostic>> errors = default;
    if (reflect.TypeOf(value).Kind() == reflect.Slice) {
        foreach (var (index, fileName) in range(value._<slice<any>>())) {
            ж<astꓸExpression> expression = default;
            if (ᏑvalueExpression != nil) {
                expression = valueExpression.Elements()[index];
            }
            if (reflect.TypeOf(fileName).Kind() == reflect.String) {
                var (val, err) = getExtendsConfigPath(fileName._<@string>(), host, newBase, expression, sourceFile);
                if (val != ""u8) {
                    extendedConfigPathArray = append(extendedConfigPathArray, val);
                }
                errors = appendꓸꓸꓸ(errors, err);
            } else {
                var (_, err) = convertJsonOption(extendsOptionDeclaration.Elements(), value, basePath, propertyAssignment, expression, sourceFile);
                errors = appendꓸꓸꓸ(errors, err);
            }
        }
    } else {
        (_, errors) = convertJsonOption(extendsOptionDeclaration, value, basePath, propertyAssignment, ᏑvalueExpression, sourceFile);
    }
    return (extendedConfigPathArray, errors);
}

internal static (@string, slice<ж<ast.Diagnostic>>) getExtendsConfigPath(@string extendedConfig, ParseConfigHost host, @string basePath, ж<astꓸExpression> valueExpression, ж<ast.SourceFile> sourceFile) {
    extendedConfig = tspath.NormalizeSlashes(extendedConfig);
    slice<ж<ast.Diagnostic>> errors = default;
    ж<ast.SourceFile> errorFile = default;
    if (sourceFile != nil) {
        errorFile = sourceFile;
    }
    if (tspath.IsRootedDiskPath(extendedConfig) || strings.HasPrefix(extendedConfig, "./") || strings.HasPrefix(extendedConfig, "../")) {
        @string extendedConfigPath = tspath.GetNormalizedAbsolutePath(extendedConfig, basePath);
        if (!host.FS().FileExists(extendedConfigPath) && !strings.HasSuffix(extendedConfigPath, tspath.ExtensionJson)) {
            extendedConfigPath = extendedConfigPath + tspath.ExtensionJson;
            if (!host.FS().FileExists(extendedConfigPath)) {
                errors = append(errors, CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(errorFile, valueExpression, diagnostics.File_0_not_found, extendedConfig));
                return ("", errors);
            }
        }
        return (extendedConfigPath, errors);
    }
    var resolverHost = Ꮡ(new resolverHost(host));
    var resolved = module.ResolveConfig(extendedConfig, tspath.CombinePaths(basePath, "tsconfig.json"), new resolverHostжResolutionHost(resolverHost)); if (resolved.IsResolved()) {
        return (resolved.ResolvedFileName, errors);
    }
    if (extendedConfig == ""u8) {
        errors = append(errors, CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(errorFile, valueExpression, diagnostics.Compiler_option_0_cannot_be_given_an_empty_string, "extends"));
    } else {
        errors = append(errors, CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(errorFile, valueExpression, diagnostics.File_0_not_found, extendedConfig));
    }
    return ("", errors);
}

partial struct tsConfigOptions {
    internal map<@string, slice<@string>> prop;
    internal slice<ж<core.ProjectReference>> references;
    internal @string notDefined;
}

partial struct CommandLineOptionNameMap /*map[@string, ж<CommandLineOption>]*/;

} partial class tsoptionsᴛCommandLineOptionNameMap {

public static ж<CommandLineOption> Get(this CommandLineOptionNameMap m, @string name) {
    var (opt, ok) = m[name, ꟷ];
    if (!ok) {
        (opt, _) = m[strings.ToLower(name), ꟷ];
    }
    return opt;
}

public static ж<CommandLineOption> GetSpellingSuggestion(this CommandLineOptionNameMap m, @string name) {
    return core.GetSpellingSuggestion(
        name,
        maps.Values<CommandLineOptionNameMap, @string, ж<CommandLineOption>>(m),
        (ж<CommandLineOption> option) => option.Name,
        (ж<CommandLineOption> a, ж<CommandLineOption> b) => strings.Compare(a.Name, b.Name));
}

} partial class tsoptions_package {

internal static CommandLineOptionNameMap commandLineOptionsToMap(slice<ж<CommandLineOption>> compilerOptions) {
    var result = new map<@string, ж<CommandLineOption>>(len(compilerOptions) * 2);
    foreach (var i in range(len(compilerOptions))) {
        result[compilerOptions[i].Name] = compilerOptions[i];
        result[strings.ToLower(compilerOptions[i].Name)] = compilerOptions[i];
    }
    return result;
}

public static readonly ж<CommandLineOptionNameMap> ᏑCommandLineCompilerOptionsMap = new StaticBox<CommandLineOptionNameMap>(default(CommandLineOptionNameMap));
public static ref CommandLineOptionNameMap CommandLineCompilerOptionsMap => ref ((StaticBox<CommandLineOptionNameMap>)ᏑCommandLineCompilerOptionsMap).ValueSlot;
internal static void initᴛCommandLineCompilerOptionsMap() { CommandLineCompilerOptionsMap = commandLineOptionsToMap(OptionsDeclarations); }

internal static O convertMapToOptions<O>(ж<collections.OrderedMap<@string, any>> compilerOptions, O result)
    where O : optionParser
{
    foreach (var (key, value) in range<@string, any>(compilerOptions.Entries().Invoke)) {
        result.ParseOption(key, value);
    }
    return result;
}

internal static (O, slice<ж<ast.Diagnostic>>) convertOptionsFromJson<O>(CommandLineOptionNameMap optionsNameMap, any jsonOptions, @string basePath, O result)
    where O : optionParser
{
    if (jsonOptions == nil) {
        return (result, default);
    }
    var (jsonMap, ok) = jsonOptions._<ж<collections.OrderedMap<@string, any>>>(ᐧ);
    if (!ok) {
        return (result, default);
    }
    slice<ж<ast.Diagnostic>> errors = default;
    foreach (var (key, value) in range<@string, any>(jsonMap.Entries().Invoke)) {
        var opt = optionsNameMap.Get(key);
        if (opt != nil && opt.Name != key) {
            errors = append(errors, CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(nil, nil, result.UnknownDidYouMeanDiagnostic(), key, opt.Name));
            continue;
        }
        if (opt == nil) {
            errors = append(errors, createUnknownOptionError(key, result.UnknownOptionDiagnostic(), "", nil, nil, nil, result.UnknownDidYouMeanDiagnostic(), optionsNameMap));
            continue;
        }

        var (convertJson, err) = convertJsonOption(opt, value, basePath, nil, nil, nil);
        errors = appendꓸꓸꓸ(errors, err);
        var compilerOptionsErr = result.ParseOption(key, convertJson);
        errors = appendꓸꓸꓸ(errors, compilerOptionsErr);
    }
    return (result, errors);
}

internal static (any, slice<ж<ast.Diagnostic>>) convertArrayLiteralExpressionToJson(ж<ast.SourceFile> sourceFile, slice<ж<astꓸExpression>> elements, ж<CommandLineOption> elementOption, bool returnValue) {
    if (!returnValue) {
        foreach (var (_, element) in range(elements)) {
            convertPropertyValueToJson(sourceFile, element, elementOption, returnValue, nil);
        }
        return (default, default);
    }
    if (len(elements) == 0) {
        return (slice<any>([]), default);
    }
    slice<ж<ast.Diagnostic>> errors = default;
    slice<any> value = default;
    foreach (var (_, element) in range(elements)) {
        var (convertedValue, err) = convertPropertyValueToJson(sourceFile, element, elementOption, returnValue, nil);
        errors = appendꓸꓸꓸ(errors, err);
        if (convertedValue != nil) {
            value = append(value, convertedValue);
        }
    }
    return (value, errors);
}

internal static @string directoryOfCombinedPath(@string fileName, @string basePath) {
    return tspath.GetDirectoryPath(tspath.GetNormalizedAbsolutePath(fileName, basePath));
}

public static (any, slice<ж<ast.Diagnostic>>) ParseConfigFileTextToJson(@string fileName, tspath.Path path, @string jsonText) {
    var jsonSourceFile = parser.ParseSourceFile(new ast.SourceFileParseOptions(
        FileName: fileName,
        Path: path
    ), jsonText, core.ScriptKindJSON);
    var (config, errors) = convertConfigFileToObject(jsonSourceFile, nil);
    if (len(jsonSourceFile.Diagnostics()) > 0) {
        errors = slice<ж<ast.Diagnostic>>([jsonSourceFile.Diagnostics()[0]]);
    }
    return (config, errors);
}

partial interface ParseConfigHost {
    Δvfs.FS FS();
    @string GetCurrentDirectory();
}

partial struct resolverHost {
    /*embed*/ public ParseConfigHost ParseConfigHost;
}

} partial class tsoptionsᴛresolverHost {

internal static void Trace(this ref resolverHost r, @string msg) { }

} partial class tsoptions_package {

public static ж<ParsedCommandLine> ParseJsonSourceFileConfigFileContent(ж<TsConfigSourceFile> sourceFile, ParseConfigHost host, @string basePath, ж<core.CompilerOptions> existingOptions, ж<collections.OrderedMap<@string, any>> existingOptionsRaw, @string configFileName, slice<tspath.Path> resolutionStack, ExtendedConfigCache extendedConfigCache) {
    var result = parseJsonConfigFileContentWorker(nil, sourceFile, host, basePath, existingOptions, existingOptionsRaw, configFileName, resolutionStack, extendedConfigCache);
    return result;
}

internal static (ж<collections.OrderedMap<@string, any>>, slice<ж<ast.Diagnostic>>) convertObjectLiteralExpressionToJson(ж<ast.SourceFile> sourceFile, bool returnValue, ref ast.ObjectLiteralExpression node, ж<CommandLineOption> ᏑobjectOption, ж<jsonConversionNotifier> ᏑjsonConversionNotifier) {
    ref var objectOption = ref ᏑobjectOption.DerefOrNull();
    ref var jsonConversionNotifier = ref ᏑjsonConversionNotifier.DerefOrNull();

    ж<collections.OrderedMap<@string, any>> result = default;
    if (returnValue) {
        result = Ꮡ(new collections.OrderedMap<@string, any>());
    }
    slice<ж<ast.Diagnostic>> errors = default;
    foreach (var (_, element) in range(node.Properties.Nodes)) {
        if (element.Kind != ast.KindPropertyAssignment) {
            errors = append(errors, ast.NewDiagnostic(sourceFile, element.Loc, diagnostics.Property_assignment_expected));
            continue;
        }

        var token = element.QuestionToken(); if (token != nil) {
            errors = append(errors, ast.NewDiagnostic(sourceFile, token.Loc, diagnostics.The_0_modifier_can_only_be_used_in_TypeScript_files, "?"));
        }
        @string textOfKey = "";
        if (!ast.IsComputedNonLiteralName(element.Name())) {
            (textOfKey, _) = ast.TryGetTextOfPropertyName(element.Name());
        }
        @string keyText = textOfKey;
        ж<CommandLineOption> option = default;
        if (keyText != ""u8 && ᏑobjectOption != nil && objectOption.ElementOptions != nil) {
            option = objectOption.ElementOptions.Get(keyText);
            if (option != nil && option.Name != keyText) {
                option = default;
            }
        }
        var (value, err) = convertPropertyValueToJson(sourceFile, (~element.AsPropertyAssignment()).Initializer, option, returnValue, ᏑjsonConversionNotifier);
        errors = appendꓸꓸꓸ(errors, err);
        if (keyText != ""u8) {
            if (returnValue) {
                result.Set(keyText, value);
            }
            if (ᏑjsonConversionNotifier != nil) {
                var (_, errΔ1) = jsonConversionNotifier.onPropertySet(keyText, value, element.AsPropertyAssignment(), ᏑobjectOption, option);
                errors = appendꓸꓸꓸ(errors, errΔ1);
            }
        }
    }
    return (result, errors);
}

internal static (any, slice<ж<ast.Diagnostic>>) convertToJson(ж<ast.SourceFile> sourceFile, ж<astꓸExpression> rootExpression, bool returnValue, ж<jsonConversionNotifier> ᏑjsonConversionNotifier) {
    ref var jsonConversionNotifier = ref ᏑjsonConversionNotifier.DerefOrNull();

    if (rootExpression == nil) {
        if (returnValue) {
            return (new EmptyStruct(), default);
        } else {
            return (default, default);
        }
    }
    ж<CommandLineOption> rootOptions = default;
    if (ᏑjsonConversionNotifier != nil) {
        rootOptions = jsonConversionNotifier.rootOptions;
    }
    return convertPropertyValueToJson(sourceFile, rootExpression, rootOptions, returnValue, ᏑjsonConversionNotifier);
}

internal static bool isDoubleQuotedString(ж<ast.Node> node) {
    return ast.IsStringLiteral(node);
}

internal static (any, slice<ж<ast.Diagnostic>>) convertPropertyValueToJson(ж<ast.SourceFile> sourceFile, ж<astꓸExpression> ᏑvalueExpression, ж<CommandLineOption> Ꮡoption, bool returnValue, ж<jsonConversionNotifier> jsonConversionNotifier) {
    ref var valueExpression = ref ᏑvalueExpression.DerefOrNull();
    ref var option = ref Ꮡoption.DerefOrNull();

    switch (valueExpression.Kind) {
    case var ᴛ when ᴛ == ast.KindTrueKeyword: {
        return (true, default);
    }
    case var ᴛ when ᴛ == ast.KindFalseKeyword: {
        return (false, default);
    }
    case var ᴛ when ᴛ == ast.KindNullKeyword: {
        return (default, default);
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        if (!isDoubleQuotedString(ᏑvalueExpression)) {
            return (valueExpression.Text(), slice<ж<ast.Diagnostic>>([ast.NewDiagnostic(sourceFile, valueExpression.Loc, diagnostics.String_literal_with_double_quotes_expected)]));
        }
        return (valueExpression.Text(), default);
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral: {
        return ((float64)jsnum.FromString(valueExpression.Text()), default);
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        if (valueExpression.AsPrefixUnaryExpression().Operator != ast.KindMinusToken || valueExpression.AsPrefixUnaryExpression().Operand.Kind != ast.KindNumericLiteral) {
            break;
        }
        return ((float64)(-jsnum.FromString(valueExpression.AsPrefixUnaryExpression().Operand.Text())), default);
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        var objectLiteralExpression = valueExpression.AsObjectLiteralExpression();
        var (ᴛ1, ᴛ2) = convertObjectLiteralExpressionToJson(sourceFile, returnValue, ref (objectLiteralExpression).DerefOrNull(), Ꮡoption, jsonConversionNotifier);
        return (ᴛ1.OrTypedNil(), ᴛ2);
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        var (result, errorsΔ2) = convertArrayLiteralExpressionToJson(
            sourceFile,
            valueExpression.Elements(),
            Ꮡoption,
            returnValue);
        return (result, errorsΔ2);
    }}

    slice<ж<ast.Diagnostic>> errors = default;
    if (Ꮡoption != nil) {
        errors = slice<ж<ast.Diagnostic>>([ast.NewDiagnostic(sourceFile, valueExpression.Loc, diagnostics.Compiler_option_0_requires_a_value_of_type_1, option.Name, getCompilerOptionValueTypeString(Ꮡoption))]);
    } else {
        errors = slice<ж<ast.Diagnostic>>([ast.NewDiagnostic(sourceFile, valueExpression.Loc, diagnostics.Property_value_can_only_be_string_literal_numeric_literal_true_false_null_object_literal_or_array_literal)]);
    }
    return (default, errors);
}

public static ж<ParsedCommandLine> ParseJsonConfigFileContent(any json, ParseConfigHost host, @string basePath, ж<core.CompilerOptions> existingOptions, @string configFileName, slice<tspath.Path> resolutionStack, ExtendedConfigCache extendedConfigCache) {
    var normalized = normalizeJsonValue(json);
    var (jsonObject, ok) = normalized._<ж<collections.OrderedMap<@string, any>>>(ᐧ);
    if (!ok) {
        jsonObject = Ꮡ(new collections.OrderedMap<@string, any>());
    }
    var result = parseJsonConfigFileContentWorker(jsonObject, nil, host, basePath, existingOptions, nil, configFileName, resolutionStack, extendedConfigCache);
    return result;
}

internal static any normalizeJsonValue(any value) {
    switch (value.type()) {
    case ж<collections.OrderedMap<@string, any>> valueΔ1: {
        foreach (var (key, child) in range<@string, any>(valueΔ1.Entries().Invoke)) {
            valueΔ1.Set(key, normalizeJsonValue(child));
        }
        return valueΔ1.OrTypedNil();
    }
    case map<@string, any> valueΔ1: {
        var result = collections.NewOrderedMapWithSizeHint<@string, any>(len(valueΔ1));
        foreach (var (_, key) in range(slices.Sorted(maps.Keys<map<@string, any>, @string, any>(valueΔ1)))) {
            var child = valueΔ1[key];
            result.Set(key, normalizeJsonValue(child));
        }
        return result.OrTypedNil();
    }
    case slice<any> valueΔ1: {
        var result = new slice<any>(len(valueΔ1));
        foreach (var (i, child) in range(valueΔ1)) {
            result[i] = normalizeJsonValue(child);
        }
        return result;
    }
    default: {
        var valueΔ1 = value;
        var reflected = reflect.ValueOf(valueΔ1);
        if (!reflected.IsValid() || (reflected.Kind() != reflect.Slice && reflected.Kind() != reflect.Array)) {
            return valueΔ1;
        }
        if (reflected.Kind() == reflect.Slice && reflected.IsNil()) {
            return default;
        }
        var result = new slice<any>(reflected.Len());
        foreach (var i in range(reflected.Len())) {
            result[i] = normalizeJsonValue(reflected.Index(i).Interface());
        }
        return result;
    }}
}

internal static (any, slice<ж<ast.Diagnostic>>) convertToObject(ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    ж<astꓸExpression> rootExpression = default;
    if (len(sourceFile.Statements.Nodes) != 0) {
        rootExpression = sourceFile.Statements.Nodes[0].Expression();
    }
    return convertToJson(ᏑsourceFile, rootExpression, true, nil);
}

internal static ж<core.CompilerOptions> getDefaultCompilerOptions(@string configFileName) {
    var options = Ꮡ(new core.CompilerOptions());
    if (configFileName != ""u8 && tspath.GetBaseFileName(configFileName) == "jsconfig.json"u8) {
        ref var depth = ref heap<nint>(2, out var Ꮡdepth);
        options = Ꮡ(new core.CompilerOptions(
            AllowJs: core.TSTrue,
            MaxNodeModuleJsDepth: Ꮡdepth,
            SkipLibCheck: core.TSTrue,
            NoEmit: core.TSTrue
        ));
    }
    return options;
}

internal static ж<core.TypeAcquisition> getDefaultTypeAcquisition(@string configFileName) {
    var options = Ꮡ(new core.TypeAcquisition());
    if (configFileName != ""u8 && tspath.GetBaseFileName(configFileName) == "jsconfig.json"u8) {
        options.Enable = core.TSTrue;
    }
    return options;
}

internal static (ж<core.CompilerOptions>, slice<ж<ast.Diagnostic>>) convertCompilerOptionsFromJsonWorker(any jsonOptions, @string basePath, @string configFileName) {
    var options = getDefaultCompilerOptions(configFileName);
    var (_, errors) = (convertOptionsFromJson<compilerOptionsParserжoptionParserᴾ>(CommandLineCompilerOptionsMap, jsonOptions, basePath, Ꮡ(new compilerOptionsParser(options))) switch { var ᴛr => ((ж<compilerOptionsParser>)ᴛr.Item1, ᴛr.Item2) });
    if (configFileName != ""u8) {
        options.ConfigFilePath = tspath.NormalizeSlashes(configFileName);
    }
    return (options, errors);
}

internal static (ж<core.TypeAcquisition>, slice<ж<ast.Diagnostic>>) convertTypeAcquisitionFromJsonWorker(any jsonOptions, @string basePath, @string configFileName) {
    var options = getDefaultTypeAcquisition(configFileName);
    var (_, errors) = (convertOptionsFromJson<typeAcquisitionParserжoptionParserᴾ>(typeAcquisitionDeclaration.ElementOptions, jsonOptions, basePath, Ꮡ(new typeAcquisitionParser(options))) switch { var ᴛr => ((ж<typeAcquisitionParser>)ᴛr.Item1, ᴛr.Item2) });
    return (options, errors);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string compileOnSaveˢ = "compileOnSave"u8;

internal static (ж<parsedTsconfig>, slice<ж<ast.Diagnostic>>) parseOwnConfigOfJson(ж<collections.OrderedMap<@string, any>> Ꮡjson, ParseConfigHost host, @string basePath, @string configFileName) {
    ref var json = ref Ꮡjson.DerefOrNull();

    slice<ж<ast.Diagnostic>> errors = default;
    if (json.Has(excludesˢ)) {
        errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Unknown_option_excludes_Did_you_mean_exclude));
    }
    var (options, err) = convertCompilerOptionsFromJsonWorker(json.GetOrZero(compilerOptionsˢ), basePath, configFileName);
    var (typeAcquisition, err2) = convertTypeAcquisitionFromJsonWorker(json.GetOrZero(typeAcquisitionˢ), basePath, configFileName);
    errors = appendꓸꓸꓸ(appendꓸꓸꓸ(errors, err), err2);
    var (compileOnSave, ok) = json.Get(compileOnSaveˢ); if (ok) {
        var (converted, compileOnSaveErrors) = convertJsonOption(compileOnSaveCommandLineOption, compileOnSave, basePath, nil, nil, nil);
        errors = appendꓸꓸꓸ(errors, compileOnSaveErrors);
        json.Set(compileOnSaveˢ, converted);
    }
    slice<@string> extendedConfigPath = default;
    var extends = json.GetOrZero(extendsˢ); if (extends != nil && !AreEqual(extends, (@string)(""))) {
        (extendedConfigPath, err) = getExtendsConfigPathOrArray(extends, host, basePath, configFileName, nil, nil, nil);
        errors = appendꓸꓸꓸ(errors, err);
    }
    var parsedConfig = Ꮡ(new parsedTsconfig(
        raw: Ꮡjson.OrTypedNil(),
        options: options,
        typeAcquisition: typeAcquisition,
        extendedConfigPath: extendedConfigPath
    ));
    return (parsedConfig, errors);
}

internal static (ж<TsConfigSourceFile>, slice<ж<ast.Diagnostic>>) readJsonConfigFile(@string fileName, tspath.Path path, Func<@string, (@string, bool)> readFile) {
    var (text, diagnostic) = tryReadFile(fileName, readFile, []);
    if (text != ""u8) {
        return (Ꮡ(new TsConfigSourceFile(
            SourceFile: parser.ParseSourceFile(new ast.SourceFileParseOptions(
                FileName: fileName,
                Path: path
            ), text, core.ScriptKindJSON)
        )), diagnostic);
    } else {
        var factory = Ꮡ(new ast.NodeFactory());
        var @file = Ꮡ(new TsConfigSourceFile(
            SourceFile: factory.NewSourceFile(new ast.SourceFileParseOptions(FileName: fileName, Path: path), "", factory.NewNodeList([]), factory.NewToken(ast.KindEndOfFile)).AsSourceFile()
        ));
        @file.SourceFile.SetDiagnostics(diagnostic);
        return (@file, diagnostic);
    }
}

internal static (ж<parsedTsconfig>, slice<ж<ast.Diagnostic>>) getExtendedConfig(ж<TsConfigSourceFile> sourceFile, @string extendedConfigFileName, ParseConfigHost host, slice<tspath.Path> resolutionStack, ExtendedConfigCache extendedConfigCache, ж<extendsResult> Ꮡresult) {
    ref var result = ref Ꮡresult.DerefOrNull();

    slice<ж<ast.Diagnostic>> errors = default;
    tspath.Path extendedConfigPath = tspath.ToPath(extendedConfigFileName, host.GetCurrentDirectory(), host.FS().UseCaseSensitiveFileNames());

    ж<ExtendedConfigCacheEntry> cacheEntry = default;
    if (extendedConfigCache != nil && !slices.Contains(resolutionStack, extendedConfigPath)) {
        cacheEntry = extendedConfigCache.GetExtendedConfig(extendedConfigFileName, extendedConfigPath, resolutionStack, host);
    } else {
        cacheEntry = ParseExtendedConfig(extendedConfigFileName, extendedConfigPath, resolutionStack, host, extendedConfigCache);
    }

    if (len(cacheEntry.errors) > 0) {
        errors = appendꓸꓸꓸ(errors, cacheEntry.errors);
    }

    if (cacheEntry.extendedResult != nil) {
        if (sourceFile != nil) {
            result.extendedSourceFiles.Add(cacheEntry.extendedResult.SourceFile.FileName());
            foreach (var (_, extendedSourceFile) in range(cacheEntry.extendedResult.ExtendedSourceFiles)) {
                result.extendedSourceFiles.Add(extendedSourceFile);
            }
        }
    }
    return (cacheEntry.extendedConfig, errors);
}

public static ж<ExtendedConfigCacheEntry> ParseExtendedConfig(@string fileName, tspath.Path path, slice<tspath.Path> resolutionStack, ParseConfigHost host, ExtendedConfigCache extendedConfigCache) {
    var (extendedResult, readErrors) = readJsonConfigFile(fileName, path, host.FS().ReadFile);
    var entry = Ꮡ(new ExtendedConfigCacheEntry(
        extendedResult: extendedResult
    ));

    if (len(readErrors) > 0) {
        entry.errors = readErrors;
        return entry;
    }

    var parseDiagnostics = extendedResult.SourceFile.Diagnostics(); if (len(parseDiagnostics) > 0) {
        entry.errors = parseDiagnostics;
        return entry;
    }

    slice<ж<ast.Diagnostic>> parseErrors = default;
    (entry.extendedConfig, parseErrors) = parseConfig(nil, extendedResult, host, tspath.GetDirectoryPath(fileName), tspath.GetBaseFileName(fileName), resolutionStack, extendedConfigCache);
    entry.errors = parseErrors;
    return entry;
}

internal static (ж<parsedTsconfig>, slice<ж<ast.Diagnostic>>) parseConfig(ж<collections.OrderedMap<@string, any>> json, ж<TsConfigSourceFile> ᏑsourceFile, ParseConfigHost host, @string basePath, @string configFileName, slice<tspath.Path> resolutionStack, ExtendedConfigCache extendedConfigCache) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    basePath = tspath.NormalizeSlashes(basePath);
    tspath.Path resolvedPath = tspath.ToPath(configFileName, basePath, host.FS().UseCaseSensitiveFileNames());
    ref var errors = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡerrors);
    if (slices.Contains(resolutionStack, resolvedPath)) {
        ж<parsedTsconfig> result = default;
        errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Circularity_detected_while_resolving_configuration_Colon_0));
        if (json.Size() == 0) {
            result = Ꮡ(new parsedTsconfig(raw: json.OrTypedNil()));
        } else {
            var (rawResult, errΔ1) = convertToObject(sourceFile.SourceFile);
            errors = appendꓸꓸꓸ(errors, errΔ1);
            result = Ꮡ(new parsedTsconfig(raw: rawResult));
        }
        return (result, errors);
    }

    ж<parsedTsconfig> ownConfig = default;
    slice<ж<ast.Diagnostic>> err = default;
    if (json != nil) {
        (ownConfig, err) = parseOwnConfigOfJson(json, host, basePath, configFileName);
    } else {
        (ownConfig, err) = parseOwnConfigOfJsonSourceFile(tsconfigToSourceFile(ᏑsourceFile), host, basePath, configFileName);
    }
    errors = appendꓸꓸꓸ(errors, err);
    if (ownConfig.options != nil && ownConfig.options.Paths != nil) {
        ownConfig.options.PathsBasePath = basePath;
    }

    var ownConfigʗ1 = ownConfig;
    void applyExtendedConfig(ж<extendsResult> result, @string extendedConfigPath) {
        var (extendedConfig, extendedErrors) = getExtendedConfig(ᏑsourceFile, extendedConfigPath, host, resolutionStack, extendedConfigCache, result);
        Ꮡerrors.ValueSlot = appendꓸꓸꓸ(Ꮡerrors.ValueSlot, extendedErrors);
        if (extendedConfig != nil && extendedConfig.options != nil) {
            var extendsRaw = extendedConfig.raw;
            @string relativeDifference = "";
            var extendsRawʗ1 = extendsRaw;
            var ownConfigʗ2 = ownConfigʗ1;
            void setPropertyValue(@string propertyName) {
                {
                    var (rawMap, ok) = ownConfigʗ2.raw._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok && rawMap.Has(propertyName)) {
                        return;
                    }
                }
                if (propertyName == "include"u8 || propertyName == "exclude"u8 || propertyName == "files"u8) {
                    {
                        var (rawMap, ok) = extendsRawʗ1._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok && rawMap.Has(propertyName)) {
                            {
                                var (Δslice, _) = rawMap.GetOrZero(propertyName)._<slice<any>>(ᐧ); if (Δslice != nil) {
                                    var value = core.Map(Δslice, any (any path) => {
                                        var (pathStr, isString) = path._<@string>(ᐧ);
                                        if (!isString) {
                                            return path;
                                        }
                                        if (startsWithConfigDirTemplate(path) || tspath.IsRootedDiskPath(pathStr)) {
                                            return pathStr;
                                        } else {
                                            if (relativeDifference == ""u8) {
                                                var t = new tspath.ComparePathsOptions(
                                                    UseCaseSensitiveFileNames: host.FS().UseCaseSensitiveFileNames(),
                                                    CurrentDirectory: basePath
                                                );
                                                relativeDifference = tspath.ConvertToRelativePath(tspath.GetDirectoryPath(extendedConfigPath), t);
                                            }
                                            return tspath.CombinePaths(relativeDifference, pathStr);
                                        }
                                    });
                                    if (propertyName == "include"u8) {
                                        result.include = value;
                                    } else if (propertyName == "exclude"u8) {
                                        result.exclude = value;
                                    } else if (propertyName == "files"u8) {
                                        result.files = value;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            setPropertyValue("include");
            setPropertyValue("exclude");
            setPropertyValue("files");
            {
                var (extendedRawMap, ok) = extendsRaw._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok && extendedRawMap.Has(contentMappersˢ)) {
                    (result.contentMappers, _) = extendedRawMap.GetOrZero(contentMappersˢ)._<slice<any>>(ᐧ);
                }
            }
            {
                var (extendedRawMap, ok) = extendsRaw._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok && extendedRawMap.Has(compileOnSaveˢ)) {
                    var (compileOnSave, okΔ1) = extendedRawMap.GetOrZero(compileOnSaveˢ)._<bool>(ᐧ); if (okΔ1) {
                        result.compileOnSave = compileOnSave;
                    }
                }
            }
            mergeCompilerOptions(result.options, extendedConfig.options, extendsRaw);
        }
    }

    if (ownConfig.extendedConfigPath != nil) {
        resolutionStack = append(resolutionStack, resolvedPath);
        ж<extendsResult> result = Ꮡ(new extendsResult(
            options: Ꮡ(new core.CompilerOptions())
        ));
        if (reflect.TypeOf(ownConfig.extendedConfigPath).Kind() == reflect.String) {
            applyExtendedConfig(result, ownConfig.extendedConfigPath._<@string>());
        } else if (ownConfig.extendedConfigPath._<slice<@string>>(out var configPath)) {
            foreach (var (_, extendedConfigPath) in range(configPath)) {
                applyExtendedConfig(result, extendedConfigPath);
            }
        }
        if (result.include != nil) {
            ownConfig.raw._<ж<collections.OrderedMap<@string, any>>>().Set(includeˢ, result.include);
        }
        if (result.exclude != nil) {
            ownConfig.raw._<ж<collections.OrderedMap<@string, any>>>().Set(excludeˢ, result.exclude);
        }
        if (result.files != nil) {
            ownConfig.raw._<ж<collections.OrderedMap<@string, any>>>().Set(filesˢ, result.files);
        }
        if (result.contentMappers != nil && !ownConfig.raw._<ж<collections.OrderedMap<@string, any>>>().Has(contentMappersˢ)) {
            ownConfig.raw._<ж<collections.OrderedMap<@string, any>>>().Set(contentMappersˢ, result.contentMappers);
        }
        if (result.compileOnSave && !ownConfig.raw._<ж<collections.OrderedMap<@string, any>>>().Has(compileOnSaveˢ)) {
            ownConfig.raw._<ж<collections.OrderedMap<@string, any>>>().Set(compileOnSaveˢ, result.compileOnSave);
        }
        if (ᏑsourceFile != nil) {
            foreach (var (extendedSourceFile, _) in result.ᏑextendedSourceFiles.Keys()) {
                sourceFile.ExtendedSourceFiles = core.InsertSorted<@string>(sourceFile.ExtendedSourceFiles, extendedSourceFile, cmp.Compare<@string>);
            }
        }
        ownConfig.options = mergeCompilerOptions(result.options, ownConfig.options, ownConfig.raw);
    }
    return (ownConfig, errors);
}

internal static readonly @string defaultIncludeSpec = "**/*"u8;

partial struct propOfRaw {
    internal slice<any> sliceValue;
    internal @string wrongValue;
}

internal static bool isStringValue(any value) {
    var (_, ok) = value._<@string>(ᐧ);
    return ok;
}

internal static ж<ParsedCommandLine> parseJsonConfigFileContentWorker(ж<collections.OrderedMap<@string, any>> json, ж<TsConfigSourceFile> ᏑsourceFile, ParseConfigHost host, @string basePath, ж<core.CompilerOptions> existingOptions, ж<collections.OrderedMap<@string, any>> existingOptionsRaw, @string configFileName, slice<tspath.Path> resolutionStack, ExtendedConfigCache extendedConfigCache) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    var contentMapperIndicesᴷ = new stackAppend<nint>();
    var contentMapperExtensionsᴷ = new stackAppend<@string>();

    debug.Assert((json == nil && ᏑsourceFile != nil) || (json != nil && ᏑsourceFile == nil));

    @string basePathForFileNames = "";
    if (configFileName != ""u8) {
        basePathForFileNames = tspath.NormalizePath(directoryOfCombinedPath(configFileName, basePath));
    } else {
        basePathForFileNames = tspath.NormalizePath(basePath);
    }

    ref var errors = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡerrors);
    (var parsedConfig, errors) = parseConfig(json, ᏑsourceFile, host, basePath, configFileName, resolutionStack, extendedConfigCache);
    mergeCompilerOptions(parsedConfig.options, existingOptions, existingOptionsRaw.OrTypedNil());
    handleOptionConfigDirTemplateSubstitution(parsedConfig.options, basePathForFileNames);
    var rawConfig = parseJsonToStringKey(parsedConfig.raw);
    if (configFileName != ""u8 && parsedConfig.options != nil) {
        parsedConfig.options.ConfigFilePath = tspath.NormalizeSlashes(configFileName);
    }
    var rawConfigʗ1 = rawConfig;
    propOfRaw getPropFromRaw(@string prop, Func<any, bool> validateElement, @string elementTypeName) {
        var (value, exists) = rawConfigʗ1.Get(prop);
        if (exists && value != nil) {
            if (reflect.TypeOf(value).Kind() == reflect.Slice) {
                var result = rawConfigʗ1.GetOrZero(prop);
                var (_, ok) = result._<slice<any>>(ᐧ); if (ok) {
                    if (ᏑsourceFile == nil && !core.Every(result._<slice<any>>(), validateElement)) {
                        Ꮡerrors.ValueSlot = append(Ꮡerrors.ValueSlot, ast.NewCompilerDiagnostic(diagnostics.Compiler_option_0_requires_a_value_of_type_1, prop, elementTypeName));
                    }
                }
                return new propOfRaw(sliceValue: result._<slice<any>>());
            } else if (ᏑsourceFile == nil) {
                Ꮡerrors.ValueSlot = append(Ꮡerrors.ValueSlot, ast.NewCompilerDiagnostic(diagnostics.Compiler_option_0_requires_a_value_of_type_1, prop, "Array"));
                return new propOfRaw(sliceValue: default, wrongValue: "not-array");
            }
        }
        return new propOfRaw(sliceValue: default, wrongValue: "no-prop");
    }
    var referencesOfRaw = getPropFromRaw("references", (any element) => AreEqual(reflect.TypeOf(element), orderedMapType), "object");
    var fileSpecs = getPropFromRaw("files", isStringValue, "string");
    if (fileSpecs.sliceValue != nil || fileSpecs.wrongValue == ""u8) {
        var hasZeroOrNoReferences = false;
        if (referencesOfRaw.wrongValue == "no-prop"u8 || referencesOfRaw.wrongValue == "not-array"u8 || len(referencesOfRaw.sliceValue) == 0) {
            hasZeroOrNoReferences = true;
        }
        var hasExtends = rawConfig.GetOrZero(extendsˢ);
        if (fileSpecs.sliceValue != nil && len(fileSpecs.sliceValue) == 0 && hasZeroOrNoReferences && hasExtends == nil) {
            if (ᏑsourceFile != nil) {
                @string fileName = default;
                if (configFileName != ""u8) {
                    fileName = configFileName;
                } else {
                    fileName = "tsconfig.json";
                }
                var diagnosticMessage = diagnostics.The_files_list_in_config_file_0_is_empty;
                var nodeValue = ForEachTsConfigPropArray(sourceFile.SourceFile, "files", ж<ast.Node> (ж<ast.PropertyAssignment> property) => (~property).Initializer);
                errors = append(errors, CreateDiagnosticForNodeInSourceFile(sourceFile.SourceFile, nodeValue, diagnosticMessage, fileName));
            } else {
                errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.The_files_list_in_config_file_0_is_empty, configFileName));
            }
        }
    }
    var includeSpecs = getPropFromRaw("include", isStringValue, "string");
    var excludeSpecs = getPropFromRaw("exclude", isStringValue, "string");
    var isDefaultIncludeSpec = false;
    if (excludeSpecs.wrongValue == "no-prop"u8 && parsedConfig.options != nil) {
        @string outDir = parsedConfig.options.OutDir;
        @string declarationDir = parsedConfig.options.DeclarationDir;
        if (outDir != ""u8 || declarationDir != ""u8) {
            slice<any> values = default;
            if (outDir != ""u8) {
                values = append(values, (any)(outDir));
            }
            if (declarationDir != ""u8) {
                values = append(values, (any)(declarationDir));
            }
            excludeSpecs = new propOfRaw(sliceValue: values);
        }
    }
    if (fileSpecs.sliceValue == nil && includeSpecs.sliceValue == nil) {
        includeSpecs = new propOfRaw(sliceValue: [defaultIncludeSpec]);
        isDefaultIncludeSpec = true;
    }
    slice<@string> validatedIncludeSpecs = default;
    slice<@string> validatedIncludeSpecsBeforeSubstitution = default;
    slice<@string> validatedExcludeSpecs = default;
    slice<@string> validatedFilesSpec = default;
    slice<@string> validatedFilesSpecBeforeSubstitution = default;
    if (includeSpecs.sliceValue != nil) {
        slice<ж<ast.Diagnostic>> err = default;
        (validatedIncludeSpecsBeforeSubstitution, err) = validateSpecs(includeSpecs.sliceValue, true, tsconfigToSourceFile(ᏑsourceFile), "include");
        errors = appendꓸꓸꓸ(errors, err);
        validatedIncludeSpecs = getSubstitutedStringArrayWithConfigDirTemplate(validatedIncludeSpecsBeforeSubstitution, basePathForFileNames); if (validatedIncludeSpecs == nil) {
            validatedIncludeSpecs = validatedIncludeSpecsBeforeSubstitution;
        }
    }
    if (excludeSpecs.sliceValue != nil) {
        slice<ж<ast.Diagnostic>> err = default;
        (validatedExcludeSpecs, err) = validateSpecs(excludeSpecs.sliceValue, false, tsconfigToSourceFile(ᏑsourceFile), "exclude");
        errors = appendꓸꓸꓸ(errors, err);
        var validatedExcludeSpecsWithSubstitution = getSubstitutedStringArrayWithConfigDirTemplate(validatedExcludeSpecs, basePathForFileNames); if (validatedExcludeSpecsWithSubstitution != nil) {
            validatedExcludeSpecs = validatedExcludeSpecsWithSubstitution;
        }
    }
    if (fileSpecs.sliceValue != nil) {
        var fileSpecsΔ1 = core.Filter<any>(fileSpecs.sliceValue, isStringValue);
        foreach (var (_, spec) in range(fileSpecsΔ1)) {
            var (specΔ1, ok) = spec._<@string>(ᐧ); if (ok) {
                validatedFilesSpecBeforeSubstitution = append(validatedFilesSpecBeforeSubstitution, specΔ1);
            }
        }
        validatedFilesSpec = getSubstitutedStringArrayWithConfigDirTemplate(validatedFilesSpecBeforeSubstitution, basePathForFileNames); if (validatedFilesSpec == nil) {
            validatedFilesSpec = validatedFilesSpecBeforeSubstitution;
        }
    }
    ref var configFileSpecs = ref heap<configFileSpecs>(new configFileSpecs(
        fileSpecs.sliceValue,
        includeSpecs.sliceValue,
        excludeSpecs.sliceValue,
        validatedFilesSpec,
        validatedIncludeSpecs,
        validatedExcludeSpecs,
        validatedFilesSpecBeforeSubstitution,
        validatedIncludeSpecsBeforeSubstitution,
        isDefaultIncludeSpec
    ), out var ᏑconfigFileSpecs);

    if (ᏑsourceFile != nil) {
        sourceFile.configFileSpecs = ᏑconfigFileSpecs;
    }

    ж<ast.SourceFile> contentMapperSourceFile = default;
    if (ᏑsourceFile != nil) {
        contentMapperSourceFile = sourceFile.SourceFile;
    }
    slice<ж<contentmapper.Mapper>> contentMappers = default;
    slice<nint> contentMapperIndices = default;
    var contentMappersOfRaw = getPropFromRaw("contentMappers", (any element) => AreEqual(reflect.TypeOf(element), orderedMapType), "object");
    foreach (var (i, element) in range(contentMappersOfRaw.sliceValue)) {
        var (mapper, mapperErrors) = parseContentMapper(element);
        foreach (var (_, mapperError) in range(mapperErrors)) {
            errors = append(errors, setContentMapperDiagnosticLocation(mapperError, contentMapperSourceFile, getContentMapperSyntax(contentMapperSourceFile, i, "")));
        }
        if (mapper != nil) {
            contentMappers = append(contentMappers, mapper);
            contentMapperIndices = append(contentMapperIndicesᴷ, contentMapperIndices, i);
        }
    }
    nint totalContentMapperExtensions = 0;
    foreach (var (_, mapper) in range(contentMappers)) {
        totalContentMapperExtensions += len(mapper.Definition.Extensions);
    }
    var seenContentMapperExtensions = new map<@string, EmptyStruct>(totalContentMapperExtensions);
    var contentMapperExtensions = new slice<@string>(0, totalContentMapperExtensions);
    var nativeExtensions = core.Flatten(tspath.AllSupportedExtensionsWithJson);
    @string canonicalExtension(@string extension) => tspath.GetCanonicalFileName(extension, host.FS().UseCaseSensitiveFileNames());
    foreach (var (j, mapper) in range(contentMappers)) {
        var validExtensions = new slice<@string>(0, len(mapper.Definition.Extensions));
        foreach (var (_, ext) in range(mapper.Definition.Extensions)) {
            var extNode = getContentMapperExtensionSyntax(contentMapperSourceFile, contentMapperIndices[j], ext);
            @string canonicalExt = canonicalExtension(ext);
            switch (ᐧ) {
            case {} when !strings.HasPrefix(ext, "."): {
                errors = append(errors, setContentMapperDiagnosticLocation(ast.NewCompilerDiagnostic(diagnostics.Content_mapper_file_extension_0_must_begin_with_a, ext), contentMapperSourceFile, extNode));
                break;
            }
            case {} when slices.ContainsFunc(nativeExtensions, 
            (@string nativeExtension) => strings.EqualFold(nativeExtension, ext)): {
                errors = append(errors, setContentMapperDiagnosticLocation(ast.NewCompilerDiagnostic(diagnostics.Content_mapper_file_extension_0_is_a_built_in_extension_and_cannot_be_registered_by_a_content_mapper, ext), contentMapperSourceFile, extNode));
                break;
            }
            default: {
                var (_, seen) = seenContentMapperExtensions[canonicalExt, ꟷ]; if (seen) {
                    errors = append(errors, setContentMapperDiagnosticLocation(ast.NewCompilerDiagnostic(diagnostics.Content_mapper_file_extension_0_is_registered_by_more_than_one_content_mapper, ext), contentMapperSourceFile, extNode));
                } else {
                    seenContentMapperExtensions[canonicalExt] = new EmptyStruct();
                    contentMapperExtensions = append(contentMapperExtensionsᴷ, contentMapperExtensions, ext);
                    validExtensions = append(validExtensions, ext);
                }
                break;
            }}

        }
        mapper.Definition.Extensions = validExtensions;
    }
    if (len(contentMappers) > 0 && !(parsedConfig.options != nil && parsedConfig.options.RunExternalCode.IsTrue())) {
        errors = append(errors, setContentMapperDiagnosticLocation(ast.NewCompilerDiagnostic(diagnostics.Content_mappers_require_the_runExternalCode_command_line_flag_to_be_enabled), contentMapperSourceFile, getContentMappersKeySyntax(contentMapperSourceFile)));
        contentMappers = default;
        contentMapperExtensions = default;
    } else if (len(contentMappers) > 0) {
        @string containingFile = configFileName;
        if (containingFile == ""u8) {
            containingFile = tspath.CombinePaths(basePathForFileNames, "tsconfig.json");
        }
        var resolvedContentMappers = new slice<ж<contentmapper.Mapper>>(0, len(contentMappers));
        foreach (var (j, mapper) in range(contentMappers)) {
            var (manifest, packageDirectory, diagnostic) = resolveContentMapperManifest(host, containingFile, mapper.Package);
            mapper.PackageDirectory = packageDirectory;
            if (diagnostic != nil) {
                errors = append(errors, setContentMapperDiagnosticLocation(diagnostic, contentMapperSourceFile, getContentMapperSyntax(contentMapperSourceFile, contentMapperIndices[j], "package")));
                continue;
            }
            mapper.Manifest = manifest;
            resolvedContentMappers = append(resolvedContentMappers, mapper);
        }
        contentMappers = resolvedContentMappers;
        contentMapperExtensions = core.FlatMap(contentMappers, slice<@string> (ж<contentmapper.Mapper> mapper) => mapper.Definition.Extensions);
    }

    var contentMapperExtensionsʗ1 = contentMapperExtensions;
    var parsedConfigʗ1 = parsedConfig;
    var rawConfigʗ2 = rawConfig;
    var resolutionStackʗ1 = resolutionStack;
    (slice<@string>, nint) getFileNames(@string basePathΔ1) {
        var parsedConfigOptions = parsedConfigʗ1.options;
        var (fileNamesΔ1, literalFileNamesLenΔ1) = getFileNamesFromConfigSpecs(ᏑconfigFileSpecs.Value, basePathΔ1, parsedConfigOptions, host.FS(), contentMapperExtensionsʗ1);
        if (shouldReportNoInputFiles(fileNamesΔ1, canJsonReportNoInputFiles(rawConfigʗ2), resolutionStackʗ1)) {
            var includeSpecsΔ1 = ᏑconfigFileSpecs.Value.includeSpecs;
            var excludeSpecsΔ1 = ᏑconfigFileSpecs.Value.excludeSpecs;
            if (includeSpecsΔ1 == nil) {
                includeSpecsΔ1 = slice<@string>([]);
            }
            if (excludeSpecsΔ1 == nil) {
                excludeSpecsΔ1 = slice<@string>([]);
            }
            var (ᴛ1, ᴛ2) = core.StringifyJson(includeSpecsΔ1, "", "");

            var (ᴛ3, ᴛ4) = core.StringifyJson(excludeSpecsΔ1, "", "");
            Ꮡerrors.ValueSlot = append(Ꮡerrors.ValueSlot, ast.NewCompilerDiagnostic(diagnostics.No_inputs_were_found_in_config_file_0_Specified_include_paths_were_1_and_exclude_paths_were_2, configFileName, core.Must(ᴛ1, ᴛ2), core.Must(ᴛ3, ᴛ4)));
        }
        return (fileNamesΔ1, literalFileNamesLenΔ1);
    }

    var getPropFromRawʗ1 = getPropFromRaw;
    slice<ж<core.ProjectReference>> getProjectReferences(@string basePathΔ2) {
        slice<ж<core.ProjectReference>> projectReferences = default;
        var newReferencesOfRaw = getPropFromRawʗ1("references", (any element) => AreEqual(reflect.TypeOf(element), orderedMapType), "object");
        if (newReferencesOfRaw.sliceValue != nil) {
            projectReferences = [];
            foreach (var (index, reference) in range(newReferencesOfRaw.sliceValue)) {
                var @ref = parseProjectReference(reference);
                if (@ref == nil) {
                    continue;
                }
                if (!@ref.hasPath || !@ref.pathValid) {
                    Ꮡerrors.ValueSlot = append(Ꮡerrors.ValueSlot, createDiagnosticAtProjectReferenceProperty(ᏑsourceFile, index, "path", diagnostics.Compiler_option_0_requires_a_value_of_type_1, "reference.path", "string"));
                    continue;
                }
                if (@ref.reference.Path == ""u8) {
                    Ꮡerrors.ValueSlot = append(Ꮡerrors.ValueSlot, createDiagnosticAtProjectReferenceProperty(ᏑsourceFile, index, "path", diagnostics.Compiler_option_0_cannot_be_given_an_empty_string, "reference.path"));
                    continue;
                }
                if (@ref.hasCircular && !@ref.circularValid) {
                    Ꮡerrors.ValueSlot = append(Ꮡerrors.ValueSlot, createDiagnosticAtProjectReferenceProperty(ᏑsourceFile, index, "circular", diagnostics.Compiler_option_0_requires_a_value_of_type_1, "reference.circular", "boolean"));
                }
                projectReferences = append(projectReferences, Ꮡ(new core.ProjectReference(
                    Path: tspath.GetNormalizedAbsolutePath(@ref.reference.Path, basePathΔ2),
                    OriginalPath: @ref.reference.Path,
                    Circular: @ref.reference.Circular
                )));
            }
        }
        return projectReferences;
    }

    ref var literalFileNamesLen = ref heap<nint>(out var ᏑliteralFileNamesLen);
    (var fileNames, literalFileNamesLen) = getFileNames(basePathForFileNames);
    var compileOnSave = Ꮡ((bool)(false));
    if (parsedConfig.raw._<ж<collections.OrderedMap<@string, any>>>(out var raw)) {
        ref var value = ref heap<bool>(out var Ꮡvalue);
        (value, var okΔ1) = raw.GetOrZero(compileOnSaveˢ)._<bool>(ᐧ); if (okΔ1) {
            compileOnSave = Ꮡvalue;
        }
    }
    return Ꮡ(new ParsedCommandLine(
        ParsedConfig: Ꮡ(new ParsedOptions(
            CompilerOptions: parsedConfig.options,
            TypeAcquisition: parsedConfig.typeAcquisition,
            FileNames: fileNames,
            ProjectReferences: getProjectReferences(basePathForFileNames),
            ContentMappers: contentMappers
        )),
        ConfigFile: ᏑsourceFile,
        Raw: parsedConfig.raw,
        Errors: errors,
        CompileOnSave: compileOnSave,

        comparePathsOptions: new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.FS().UseCaseSensitiveFileNames(),
            CurrentDirectory: basePathForFileNames
        ),
        literalFileNamesLen: literalFileNamesLen
    ));
}

internal static bool canJsonReportNoInputFiles(ж<collections.OrderedMap<@string, any>> ᏑrawConfig) {
    ref var rawConfig = ref ᏑrawConfig.DerefOrNull();

    var filesExists = rawConfig.Has(filesˢ);
    var referencesExists = rawConfig.Has(referencesˢ);
    return !filesExists && !referencesExists;
}

internal static bool shouldReportNoInputFiles(slice<@string> fileNames, bool canJsonReportNoInputFiles, slice<tspath.Path> resolutionStack) {
    return len(fileNames) == 0 && canJsonReportNoInputFiles && len(resolutionStack) == 0;
}

internal static (slice<@string>, slice<ж<ast.Diagnostic>>) validateSpecs(any specs, bool disallowTrailingRecursion, ж<ast.SourceFile> jsonSourceFile, @string specKey) {
    var errorsᴷ = new stackAppend<ж<ast.Diagnostic>>(moves: true);
    var finalSpecsᴷ = new stackAppend<@string>(moves: true);

    ж<ast.Diagnostic> createDiagnostic(ж<diagnostics.Message> message, @string spec) {
        var element = GetTsConfigPropArrayElementValue(jsonSourceFile, specKey, spec);
        ж<ast.Node> node = default;
        if (element != nil) {
            node = element.of(ast.StringLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        }
        return CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(jsonSourceFile, node, message, spec);
    }
    slice<ж<ast.Diagnostic>> errors = default;
    slice<@string> finalSpecs = default;
    foreach (var (_, value) in range(specs._<slice<any>>())) {
        var (spec, ok) = value._<@string>(ᐧ);
        if (!ok) {
            continue;
        }
        var diag = specToDiagnostic(spec, disallowTrailingRecursion);
        if (diag != nil) {
            errors = append(errorsᴷ, errors, createDiagnostic(diag, spec));
        } else {
            finalSpecs = append(finalSpecsᴷ, finalSpecs, spec);
        }
    }
    errors = move2heap(errorsᴷ, errors);
    finalSpecs = move2heap(finalSpecsᴷ, finalSpecs);
    return (finalSpecs, errors);
}

internal static ж<diagnostics.Message> specToDiagnostic(@string spec, bool disallowTrailingRecursion) {
    if (disallowTrailingRecursion && invalidTrailingRecursion(spec)) {
        return diagnostics.File_specification_cannot_end_in_a_recursive_directory_wildcard_Asterisk_Asterisk_Colon_0;
    }
    if (invalidDotDotAfterRecursiveWildcard(spec)) {
        return diagnostics.File_specification_cannot_contain_a_parent_directory_that_appears_after_a_recursive_directory_wildcard_Asterisk_Asterisk_Colon_0;
    }
    return default;
}

internal static bool invalidTrailingRecursion(@string spec) {
    @string s = strings.TrimSuffix(spec, "/");
    return s == "**"u8 || strings.HasSuffix(s, "/**");
}

internal static bool invalidDotDotAfterRecursiveWildcard(@string s) {
    nint wildcardIndex = default;
    if (strings.HasPrefix(s, "**/")) {
        wildcardIndex = 0;
    } else {
        wildcardIndex = strings.Index(s, "/**/");
    }
    if (wildcardIndex == -1) {
        return false;
    }
    nint lastDotIndex = default;
    if (strings.HasSuffix(s, "/..")) {
        lastDotIndex = len(s);
    } else {
        lastDotIndex = strings.LastIndex(s, "/../");
    }
    return lastDotIndex > wildcardIndex;
}

public static ж<ast.StringLiteral> GetTsConfigPropArrayElementValue(ж<ast.SourceFile> tsConfigSourceFile, @string propKey, @string elementValue) {
    var callback = GetCallbackForFindingPropertyAssignmentByValue(elementValue);
    var callbackʗ1 = callback;
    return ForEachTsConfigPropArray(tsConfigSourceFile, propKey, ж<ast.StringLiteral> (ж<ast.PropertyAssignment> property) => {
        var value = callbackʗ1(property); if (value != nil) {
            return value.AsStringLiteral();
        }
        return default;
    });
}

public static ж<T> ForEachTsConfigPropArray<T>(ж<ast.SourceFile> tsConfigSourceFile, @string propKey, Func<ж<ast.PropertyAssignment>, ж<T>> callback) {
    if (tsConfigSourceFile != nil) {
        return ForEachPropertyAssignment(getTsConfigObjectLiteralExpression(tsConfigSourceFile), propKey, callback);
    }
    return default;
}

public static ж<ast.Diagnostic> CreateDiagnosticAtReferenceSyntax(ж<ParsedCommandLine> Ꮡconfig, nint index, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.slice();
    ref var config = ref Ꮡconfig.DerefOrNull();

    var argsʗ1 = args;
    return ForEachTsConfigPropArray(config.ConfigFile.SourceFile, "references", ж<ast.Diagnostic> (ж<ast.PropertyAssignment> property) => {
        if (ast.IsArrayLiteralExpression((~property).Initializer)) {
            var value = (~property).Initializer.Elements();
            if (len(value) > index) {
                return CreateDiagnosticForNodeInSourceFile(Ꮡconfig.Value.ConfigFile.SourceFile, value[index], message, argsʗ1.ꓸꓸꓸ);
            }
        }
        return default;
    });
}

internal static ж<ast.Diagnostic> createDiagnosticAtProjectReferenceProperty(ж<TsConfigSourceFile> ᏑsourceFile, nint index, @string propertyName, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    ж<ast.Node> node = default;
    if (ᏑsourceFile != nil) {
        node = ForEachTsConfigPropArray(sourceFile.SourceFile, "references", ж<ast.Node> (ж<ast.PropertyAssignment> property) => {
            if (ast.IsArrayLiteralExpression((~property).Initializer)) {
                var elements = (~property).Initializer.Elements();
                if (len(elements) > index && ast.IsObjectLiteralExpression(elements[index])) {
                    {
                        var propertyNode = ForEachPropertyAssignment(elements[index].AsObjectLiteralExpression(), propertyName, ж<ast.Node> (ж<ast.PropertyAssignment> propertyΔ1) => (~propertyΔ1).Initializer); if (propertyNode != nil) {
                            return propertyNode;
                        }
                    }
                    return elements[index];
                }
            }
            return default;
        });
    }
    return CreateDiagnosticForNodeInSourceFileOrCompilerDiagnostic(tsconfigToSourceFile(ᏑsourceFile), node, message, args.ꓸꓸꓸ);
}

public static Func<ж<ast.PropertyAssignment>, ж<ast.Node>> GetCallbackForFindingPropertyAssignmentByValue(@string value) {
    return (ж<ast.PropertyAssignment> property) => {
        if (ast.IsArrayLiteralExpression((~property).Initializer)) {
            return core.Find((~property).Initializer.Elements(), (ж<ast.Node> element) => ast.IsStringLiteral(element) && element.Text() == value);
        }
        return default;
    };
}

public static ж<ast.Node> GetOptionsSyntaxByArrayElementValue(ж<ast.ObjectLiteralExpression> objectLiteral, @string propKey, @string elementValue) {
    return ForEachPropertyAssignment(objectLiteral, propKey, GetCallbackForFindingPropertyAssignmentByValue(elementValue));
}

internal static ж<ast.Node> getContentMapperSyntax(ж<ast.SourceFile> sourceFile, nint index, @string subKey) {
    if (sourceFile == nil) {
        return default;
    }
    return ForEachTsConfigPropArray(sourceFile, "contentMappers", ж<ast.Node> (ж<ast.PropertyAssignment> property) => {
        if (!ast.IsArrayLiteralExpression((~property).Initializer)) {
            return (~property).Initializer;
        }
        var elements = (~property).Initializer.Elements();
        if (index < 0 || index >= len(elements)) {
            return (~property).Initializer;
        }
        var element = elements[index];
        if (subKey != ""u8 && ast.IsObjectLiteralExpression(element)) {
            {
                var node = ForEachPropertyAssignment(element.AsObjectLiteralExpression(), subKey, ж<ast.Node> (ж<ast.PropertyAssignment> propertyΔ1) => (~propertyΔ1).Initializer); if (node != nil) {
                    return node;
                }
            }
        }
        return element;
    });
}

public static (ж<ast.SourceFile>, core.TextRange) GetContentMapperOptionDiagnosticLocation(ж<ParsedCommandLine> Ꮡconfig, ж<contentmapper.Mapper> mapper, slice<contentmapper.OptionPathSegment> path) {
    ref var config = ref Ꮡconfig.DerefOrNull();

    if (Ꮡconfig == nil || config.ConfigFile == nil) {
        return (default, core.UndefinedTextRange());
    }
    nint index = slices.Index(Ꮡconfig.ContentMappers(), mapper);
    var mapperNode = getContentMapperSyntax(config.ConfigFile.SourceFile, index, "");
    var node = getContentMapperSyntax(config.ConfigFile.SourceFile, index, "options");
    if (node == nil) {
        node = mapperNode;
    }
    foreach (var (_, segment) in range(path)) {
        ж<ast.Node> next = default;
        switch (ᐧ) {
        case {} when segment.IsIndex && ast.IsArrayLiteralExpression(node): {
            var elements = node.Elements();
            if (segment.Index < len(elements)) {
                next = elements[segment.Index];
            }
            break;
        }
        case {} when !segment.IsIndex && ast.IsObjectLiteralExpression(node): {
            next = ForEachPropertyAssignment(node.AsObjectLiteralExpression(), segment.Property, ж<ast.Node> (ж<ast.PropertyAssignment> property) => (~property).Initializer);
            break;
        }}

        if (next == nil) {
            break;
        }
        node = next;
    }
    if (node == nil) {
        return (default, core.UndefinedTextRange());
    }
    var @file = config.ConfigFile.SourceFile;
    return (@file, core.NewTextRange(scanner.SkipTrivia(@file.Text(), node.Pos()), node.End()));
}

internal static ж<ast.Node> getContentMappersKeySyntax(ж<ast.SourceFile> sourceFile) {
    if (sourceFile == nil) {
        return default;
    }
    return ForEachTsConfigPropArray(sourceFile, "contentMappers", ж<ast.Node> (ж<ast.PropertyAssignment> property) => property.Name());
}

internal static ж<ast.Node> getContentMapperExtensionSyntax(ж<ast.SourceFile> sourceFile, nint index, @string ext) {
    var node = getContentMapperSyntax(sourceFile, index, "extensions");
    if (node != nil && ast.IsArrayLiteralExpression(node)) {
        {
            var element = core.Find(node.Elements(), (ж<ast.Node> elementΔ1) => ast.IsStringLiteral(elementΔ1) && elementΔ1.Text() == ext); if (element != nil) {
                return element;
            }
        }
    }
    return node;
}

internal static ж<ast.Diagnostic> setContentMapperDiagnosticLocation(ж<ast.Diagnostic> Ꮡdiagnostic, ж<ast.SourceFile> ᏑsourceFile, ж<ast.Node> Ꮡnode) {
    ref var diagnostic = ref Ꮡdiagnostic.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ᏑsourceFile != nil && Ꮡnode != nil) {
        diagnostic.SetFile(ᏑsourceFile);
        diagnostic.SetLocation(core.NewTextRange(scanner.SkipTrivia(sourceFile.Text(), node.Pos()), node.End()));
    }
    return Ꮡdiagnostic;
}

public static ж<T> ForEachPropertyAssignment<T>(ж<ast.ObjectLiteralExpression> ᏑobjectLiteral, @string key, Func<ж<ast.PropertyAssignment>, ж<T>> callback, params ꓸꓸꓸstring key2ʗp) {
    var key2 = key2ʗp.sslice();
    ref var objectLiteral = ref ᏑobjectLiteral.DerefOrNull();

    if (ᏑobjectLiteral != nil) {
        foreach (var (_, property) in range(objectLiteral.Properties.Nodes)) {
            if (!ast.IsPropertyAssignment(property)) {
                continue;
            }
            var (propName, ok) = ast.TryGetTextOfPropertyName(property.Name()); if (ok) {
                if (propName == key || (len(key2) > 0 && key2[0] == propName)) {
                    return callback(property.AsPropertyAssignment());
                }
            }
        }
    }
    return default;
}

internal static ж<ast.ObjectLiteralExpression> getTsConfigObjectLiteralExpression(ж<ast.SourceFile> ᏑtsConfigSourceFile) {
    ref var tsConfigSourceFile = ref ᏑtsConfigSourceFile.DerefOrNull();

    if (ᏑtsConfigSourceFile != nil && tsConfigSourceFile.Statements != nil && len(tsConfigSourceFile.Statements.Nodes) > 0) {
        var expression = tsConfigSourceFile.Statements.Nodes[0].Expression();
        if (ast.IsObjectLiteralExpression(expression)) {
            return expression.AsObjectLiteralExpression();
        }
    }
    return default;
}

internal static @string getSubstitutedPathWithConfigDirTemplate(@string value, @string basePath) {
    return tspath.GetNormalizedAbsolutePath(strings.Replace(value, configDirTemplate, "./", 1), basePath);
}

internal static slice<@string> getSubstitutedStringArrayWithConfigDirTemplate(slice<@string> list, @string basePath) {
    slice<@string> result = default;
    foreach (var (i, element) in range(list)) {
        if (startsWithConfigDirTemplate(element)) {
            if (result == nil) {
                result = slices.Clone<slice<@string>, @string>(list);
            }
            result[i] = getSubstitutedPathWithConfigDirTemplate(element, basePath);
        }
    }
    if (result != nil) {
        return result;
    }
    return default;
}

internal static void handleOptionConfigDirTemplateSubstitution(ж<core.CompilerOptions> ᏑcompilerOptions, @string basePath) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    if (ᏑcompilerOptions == nil) {
        return;
    }

    ж<collections.OrderedMap<@string, slice<@string>>> paths = default;
    foreach (var (k, v) in range<@string, slice<@string>>(compilerOptions.Paths.Entries().Invoke)) {
        var substitution = getSubstitutedStringArrayWithConfigDirTemplate(v, basePath); if (substitution != nil) {
            if (paths == nil) {
                paths = compilerOptions.Paths.Clone();
                compilerOptions.Paths = paths;
            }
            paths.Set(k, substitution);
        }
    }

    var rootDirs = getSubstitutedStringArrayWithConfigDirTemplate(compilerOptions.RootDirs, basePath); if (rootDirs != nil) {
        compilerOptions.RootDirs = rootDirs;
    }
    var typeRoots = getSubstitutedStringArrayWithConfigDirTemplate(compilerOptions.TypeRoots, basePath); if (typeRoots != nil) {
        compilerOptions.TypeRoots = typeRoots;
    }
    if (startsWithConfigDirTemplate(compilerOptions.GenerateCpuProfile)) {
        compilerOptions.GenerateCpuProfile = getSubstitutedPathWithConfigDirTemplate(compilerOptions.GenerateCpuProfile, basePath);
    }
    if (startsWithConfigDirTemplate(compilerOptions.GenerateTrace)) {
        compilerOptions.GenerateTrace = getSubstitutedPathWithConfigDirTemplate(compilerOptions.GenerateTrace, basePath);
    }
    if (startsWithConfigDirTemplate(compilerOptions.OutFile)) {
        compilerOptions.OutFile = getSubstitutedPathWithConfigDirTemplate(compilerOptions.OutFile, basePath);
    }
    if (startsWithConfigDirTemplate(compilerOptions.OutDir)) {
        compilerOptions.OutDir = getSubstitutedPathWithConfigDirTemplate(compilerOptions.OutDir, basePath);
    }
    if (startsWithConfigDirTemplate(compilerOptions.RootDir)) {
        compilerOptions.RootDir = getSubstitutedPathWithConfigDirTemplate(compilerOptions.RootDir, basePath);
    }
    if (startsWithConfigDirTemplate(compilerOptions.TsBuildInfoFile)) {
        compilerOptions.TsBuildInfoFile = getSubstitutedPathWithConfigDirTemplate(compilerOptions.TsBuildInfoFile, basePath);
    }
    if (startsWithConfigDirTemplate(compilerOptions.BaseUrl)) {
        compilerOptions.BaseUrl = getSubstitutedPathWithConfigDirTemplate(compilerOptions.BaseUrl, basePath);
    }
    if (startsWithConfigDirTemplate(compilerOptions.DeclarationDir)) {
        compilerOptions.DeclarationDir = getSubstitutedPathWithConfigDirTemplate(compilerOptions.DeclarationDir, basePath);
    }
}

internal static bool hasFileWithHigherPriorityExtension(@string @file, slice<slice<@string>> extensions, Func<@string, bool> hasFile) {
    slice<@string> extensionGroup = default;
    foreach (var (_, group) in range(extensions)) {
        if (tspath.FileExtensionIsOneOf(@file, group)) {
            extensionGroup = appendꓸꓸꓸ(extensionGroup, group);
        }
    }
    if (len(extensionGroup) == 0) {
        return false;
    }
    foreach (var (_, ext) in range(extensionGroup)) {
        if (tspath.FileExtensionIs(@file, ext) && (ext != tspath.ExtensionTs || !tspath.FileExtensionIs(@file, tspath.ExtensionDts))) {
            return false;
        }
        if (hasFile(tspath.ChangeExtension(@file, ext))) {
            if (ext == tspath.ExtensionDts && (tspath.FileExtensionIs(@file, tspath.ExtensionJs) || tspath.FileExtensionIs(@file, tspath.ExtensionJsx))) {
                continue;
            }
            return true;
        }
    }
    return false;
}

internal static void removeWildcardFilesWithLowerPriorityExtension(@string @file, ж<collections.OrderedMap<@string, @string>> ᏑwildcardFiles, slice<slice<@string>> extensions, Func<@string, @string> keyMapper) {
    ref var wildcardFiles = ref ᏑwildcardFiles.DerefOrNull();

    slice<@string> extensionGroup = default;
    foreach (var (_, group) in range(extensions)) {
        if (tspath.FileExtensionIsOneOf(@file, group)) {
            extensionGroup = appendꓸꓸꓸ(extensionGroup, group);
        }
    }
    if (extensionGroup == nil) {
        return;
    }
    for (nint i = len(extensionGroup) - 1; i >= 0; i--) {
        @string ext = extensionGroup[i];
        if (tspath.FileExtensionIs(@file, ext)) {
            return;
        }
        @string lowerPriorityPath = keyMapper(tspath.ChangeExtension(@file, ext));
        wildcardFiles.Delete(lowerPriorityPath);
    }
}

internal static (slice<@string>, nint) getFileNamesFromConfigSpecs(configFileSpecs configFileSpecs, @string basePath, ж<core.CompilerOptions> options, Δvfs.FS host, slice<@string> extraExtensions) {
    basePath = tspath.NormalizePath(basePath);
    var keyMappper = @string (@string value) => tspath.GetCanonicalFileName(value, host.UseCaseSensitiveFileNames());
    ref var literalFileMap = ref heap(new collections.OrderedMap<@string, @string>(), out var ᏑliteralFileMap);
    ref var wildcardFileMap = ref heap(new collections.OrderedMap<@string, @string>(), out var ᏑwildcardFileMap);
    ref var wildCardJsonFileMap = ref heap(new collections.OrderedMap<@string, @string>(), out var ᏑwildCardJsonFileMap);
    var validatedFilesSpec = configFileSpecs.validatedFilesSpec;
    var validatedIncludeSpecs = configFileSpecs.validatedIncludeSpecs;
    var validatedExcludeSpecs = configFileSpecs.validatedExcludeSpecs;
    var supportedExtensions = GetSupportedExtensions(options, extraExtensions);
    var supportedExtensionsWithJsonIfResolveJsonModule = GetSupportedExtensionsWithJsonIfResolveJsonModule(options, supportedExtensions);
    foreach (var (_, fileName) in range(validatedFilesSpec)) {
        @string @file = tspath.GetNormalizedAbsolutePath(fileName, basePath);
        literalFileMap.Set(keyMappper(fileName), @file);
    }

    ж<vfsmatch.SpecMatcher> jsonOnlyIncludeMatchers = default;
    if (len(validatedIncludeSpecs) > 0) {
        var filesΔ1 = vfsmatch.ReadDirectory(host, basePath, basePath, core.Flatten(supportedExtensionsWithJsonIfResolveJsonModule), validatedExcludeSpecs, validatedIncludeSpecs, vfsmatch.UnlimitedDepth);
        foreach (var (_, @file) in range(filesΔ1)) {
            if (tspath.FileExtensionIs(@file, tspath.ExtensionJson)) {
                if (jsonOnlyIncludeMatchers == nil) {
                    var includes = core.Filter(validatedIncludeSpecs, (@string include) => strings.HasSuffix(include, tspath.ExtensionJson));
                    jsonOnlyIncludeMatchers = vfsmatch.NewSpecMatcher(includes, basePath, vfsmatch.UsageFiles, host.UseCaseSensitiveFileNames());
                }
                nint includeIndex = -1;
                if (jsonOnlyIncludeMatchers != nil) {
                    includeIndex = jsonOnlyIncludeMatchers.MatchIndex(@file);
                }
                if (includeIndex != -1) {
                    @string keyΔ1 = keyMappper(@file);
                    if (!literalFileMap.Has(keyΔ1) && !wildCardJsonFileMap.Has(keyΔ1)) {
                        wildCardJsonFileMap.Set(keyΔ1, @file);
                    }
                }
                continue;
            }
            var keyMappperʗ1 = keyMappper;
            if (hasFileWithHigherPriorityExtension(@file, supportedExtensions, (@string fileName) => {
                @string canonicalFileName = keyMappperʗ1(fileName);
                return ᏑliteralFileMap.Value.Has(canonicalFileName) || ᏑwildcardFileMap.Value.Has(canonicalFileName);
            })) {
                continue;
            }
            removeWildcardFilesWithLowerPriorityExtension(@file, ᏑwildcardFileMap, supportedExtensions, keyMappper);
            @string key = keyMappper(@file);
            if (!literalFileMap.Has(key) && !wildcardFileMap.Has(key)) {
                wildcardFileMap.Set(key, @file);
            }
        }
    }
    var files = new slice<@string>(0, ᏑliteralFileMap.Size() + ᏑwildcardFileMap.Size() + ᏑwildCardJsonFileMap.Size());
    foreach (var @file in range<@string>(ᏑliteralFileMap.Values().Invoke)) {
        files = append(files, @file);
    }
    foreach (var @file in range<@string>(ᏑwildcardFileMap.Values().Invoke)) {
        files = append(files, @file);
    }
    foreach (var @file in range<@string>(ᏑwildCardJsonFileMap.Values().Invoke)) {
        files = append(files, @file);
    }
    return (files, ᏑliteralFileMap.Size());
}

public static slice<slice<@string>> GetSupportedExtensions(ж<core.CompilerOptions> ᏑcompilerOptions, slice<@string> extraExtensions) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();
    var resultᴷ = new stackAppend<slice<@string>>();

    var needJSExtensions = compilerOptions.GetAllowJS();
    slice<slice<@string>> builtins = default;
    if (needJSExtensions) {
        builtins = tspath.AllSupportedExtensions;
    } else {
        builtins = tspath.SupportedTSExtensions;
    }
    if (len(extraExtensions) == 0) {
        return builtins;
    }
    var flatBuiltins = core.Flatten(builtins);
    slice<slice<@string>> result = default;
    foreach (var (_, ext) in range(extraExtensions)) {
        if (!slices.Contains(flatBuiltins, ext)) {
            result = append(resultᴷ, result, slice<@string>([ext]));
        }
    }
    if (len(result) == 0) {
        return builtins;
    }
    return slices.Concat<slice<slice<@string>>, slice<@string>>(builtins, result);
}

public static slice<slice<@string>> GetSupportedExtensionsWithJsonIfResolveJsonModule(ж<core.CompilerOptions> ᏑcompilerOptions, slice<slice<@string>> supportedExtensions) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    if (ᏑcompilerOptions == nil || !compilerOptions.GetResolveJsonModule()) {
        return supportedExtensions;
    }
    if (core.Same(supportedExtensions, tspath.AllSupportedExtensions)) {
        return tspath.AllSupportedExtensionsWithJson;
    }
    if (core.Same(supportedExtensions, tspath.SupportedTSExtensions)) {
        return tspath.SupportedTSExtensionsWithJson;
    }
    return slices.Concat<slice<slice<@string>>, slice<@string>>(supportedExtensions, slice<slice<@string>>([[tspath.ExtensionJson]]));
}

public static (ж<ParsedCommandLine>, slice<ж<ast.Diagnostic>>) GetParsedCommandLineOfConfigFile(@string configFileName, ж<core.CompilerOptions> options, ж<collections.OrderedMap<@string, any>> optionsRaw, ParseConfigHost sys, ExtendedConfigCache extendedConfigCache) {
    configFileName = tspath.GetNormalizedAbsolutePath(configFileName, sys.GetCurrentDirectory());
    return GetParsedCommandLineOfConfigFilePath(configFileName, tspath.ToPath(configFileName, sys.GetCurrentDirectory(), sys.FS().UseCaseSensitiveFileNames()), options, optionsRaw, sys, extendedConfigCache);
}

public static (ж<ParsedCommandLine>, slice<ж<ast.Diagnostic>>) GetParsedCommandLineOfConfigFilePath(@string configFileName, tspath.Path path, ж<core.CompilerOptions> options, ж<collections.OrderedMap<@string, any>> optionsRaw, ParseConfigHost sys, ExtendedConfigCache extendedConfigCache) {
    var errors = slice<ж<ast.Diagnostic>>([]);
    (var configFileText, errors) = tryReadFile(configFileName, sys.FS().ReadFile, errors);
    if (len(errors) > 0) {
        return (default, errors);
    }

    var tsConfigSourceFile = NewTsconfigSourceFileFromFilePath(configFileName, path, configFileText);
    return (ParseJsonSourceFileConfigFileContent(
        tsConfigSourceFile,
        sys,
        tspath.GetDirectoryPath(configFileName),
        options,
        optionsRaw,
        configFileName,
        default,
        extendedConfigCache), default);
}

} // end tsoptions_package
