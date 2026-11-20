namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using iter = iter_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using glob = go.github.com.microsoft.TypeScript.tsc.@internal.glob_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using outputpaths = go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using vfsmatch = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.vfsmatch_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;
using time = time_package;
using static tsoptions_package;

partial class tsoptions_package {

internal static readonly @string fileGlobPattern = "*.{js,jsx,mjs,cjs,ts,tsx,mts,cts,json}"u8;
internal static readonly @string recursiveFileGlobPattern = "**/*.{js,jsx,mjs,cjs,ts,tsx,mts,cts,json}"u8;

} partial class tsoptionsᴛParsedCommandLine {

internal static (@string fileGlob, @string recursiveFileGlob) fileGlobPatterns(this ж<ParsedCommandLine> Ꮡp) {
    @string fileGlob = default;
    var extensionsᴷ = new stackAppend<@string>();

    var mapperExtensions = Ꮡp.ContentMapperExtensions();
    if (len(mapperExtensions) == 0) {
        return (fileGlobPattern, recursiveFileGlobPattern);
    }
    var extensions = new slice<@string>(0, 9 + len(mapperExtensions));
    extensions = append(extensionsᴷ, extensions, (@string)"js", (@string)"jsx", (@string)"mjs", (@string)"cjs", (@string)"ts", (@string)"tsx", (@string)"mts", (@string)"cts", (@string)"json");
    foreach (var (_, extension) in range(mapperExtensions)) {
        extensions = append(extensions, strings.TrimPrefix(extension, "."));
    }
    fileGlob = "*.{"u8 + strings.Join(extensions, ",") + "}"u8;
    return (fileGlob, "**/" + fileGlob);
}

} partial class tsoptions_package {

partial struct ParsedCommandLine {
    public ж<ParsedOptions> ParsedConfig; /*`json:"parsedConfig"`*/
    public ж<TsConfigSourceFile> ConfigFile; /*`json:"configFile"`*/
    public slice<ж<ast.Diagnostic>> Errors; /*`json:"errors"`*/
    public any Raw; /*`json:"raw"`*/
    public ж<bool> CompileOnSave; /*`json:"compileOnSave"`*/
    internal tspath.ComparePathsOptions comparePathsOptions;
    internal sync.Once wildcardDirectoriesOnce;
    internal map<@string, bool> wildcardDirectories;
    internal sync.Once includeGlobsOnce;
    internal slice<ж<glob.Glob>> includeGlobs;
    internal sync.Once sourceAndOutputMapsOnce;
    internal map<tspath.Path, ж<SourceOutputAndProjectReference>> sourceToProjectReference;
    internal map<tspath.Path, ж<SourceOutputAndProjectReference>> outputDtsToProjectReference;
    internal @string commonSourceDirectory;
    internal sync.Once commonSourceDirectoryOnce;
    internal slice<@string> resolvedProjectReferencePaths;
    internal sync.Once resolvedProjectReferencePathsOnce;
    internal nint literalFileNamesLen;
    internal map<tspath.Path, @string> fileNamesByPath;
    internal sync.Once fileNamesByPathOnce;
    internal locale.Locale locale;
    internal sync.Once localeOnce;
}

public static ж<ParsedCommandLine> NewParsedCommandLine(ж<core.CompilerOptions> compilerOptions, slice<@string> rootFileNames, slice<ж<core.ProjectReference>> projectReferences, tspath.ComparePathsOptions comparePathsOptions) {
    return Ꮡ(new ParsedCommandLine(
        ParsedConfig: Ꮡ(new ParsedOptions(
            CompilerOptions: compilerOptions,
            FileNames: rootFileNames,
            ProjectReferences: projectReferences
        )),
        comparePathsOptions: comparePathsOptions
    ));
}

} partial class tsoptionsᴛParsedCommandLine {

public static ж<ParsedCommandLine> WithFileNames(this ref ParsedCommandLine p, slice<@string> fileNames) {
    ref var parsedConfig = ref heap<ParsedOptions>(p.ParsedConfig.Value, out var ᏑparsedConfig);
    parsedConfig.FileNames = fileNames;
    return Ꮡ(new ParsedCommandLine(
        ParsedConfig: ᏑparsedConfig,
        ConfigFile: p.ConfigFile,
        Errors: p.Errors,
        Raw: p.Raw,
        CompileOnSave: p.CompileOnSave,
        comparePathsOptions: p.comparePathsOptions,
        wildcardDirectories: p.wildcardDirectories,
        includeGlobs: p.includeGlobs,
        literalFileNamesLen: p.literalFileNamesLen
    ));
}

} partial class tsoptions_package {

partial struct SourceOutputAndProjectReference {
    public @string Source;
    public @string OutputDts;
    public ж<ParsedCommandLine> Resolved;
}

internal static module.ResolvedProjectReference _ᴛ1ʗ = new ParsedCommandLineжResolvedProjectReference(((ж<ParsedCommandLine>)nil));
internal static outputpaths.OutputPathsHost _ᴛ2ʗ = new ParsedCommandLineжOutputPathsHost(((ж<ParsedCommandLine>)nil));

} partial class tsoptionsᴛParsedCommandLine {

public static @string ConfigName(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp == nil || p.ConfigFile == nil) {
        return "";
    }
    return p.ConfigFile.SourceFile.FileName();
}

public static map<tspath.Path, ж<SourceOutputAndProjectReference>> SourceToProjectReference(this ref ParsedCommandLine p) {
    return p.sourceToProjectReference;
}

public static map<tspath.Path, ж<SourceOutputAndProjectReference>> OutputDtsToProjectReference(this ref ParsedCommandLine p) {
    return p.outputDtsToProjectReference;
}

public static void ParseInputOutputNames(this ж<ParsedCommandLine> Ꮡp) {
    Ꮡp.ᏑsourceAndOutputMapsOnce.Do(() => {
        var sourceToOutput = new map<tspath.Path, ж<SourceOutputAndProjectReference>>{};
        var outputDtsToSource = new map<tspath.Path, ж<SourceOutputAndProjectReference>>{};

        foreach (var (iᴛ1, vᴛ1) in range<@string, @string>(Ꮡp.getOutputDeclarationAndSourceFileNames().Invoke)) {
            ref var outputDts = ref heap(new @string(), out var ᏑoutputDts);
            outputDts = iᴛ1;
            ref var source = ref heap(new @string(), out var Ꮡsource);
            source = vᴛ1;

            tspath.Path path = tspath.ToPath(source, Ꮡp.Value.GetCurrentDirectory(), Ꮡp.Value.UseCaseSensitiveFileNames());
            var projectReference = Ꮡ(new SourceOutputAndProjectReference(
                Source: source,
                OutputDts: outputDts,
                Resolved: Ꮡp
            ));
            if (outputDts != ""u8) {
                outputDtsToSource[tspath.ToPath(outputDts, Ꮡp.Value.GetCurrentDirectory(), Ꮡp.Value.UseCaseSensitiveFileNames())] = projectReference;
            }
            sourceToOutput[path] = projectReference;
        }
        Ꮡp.Value.outputDtsToProjectReference = outputDtsToSource;
        Ꮡp.Value.sourceToProjectReference = sourceToOutput;
    });
}

public static @string CommonSourceDirectory(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑcommonSourceDirectoryOnce.Do(() => {
        var files = () => core.Filter(Ꮡp.Value.ParsedConfig.FileNames, (@string @file) => !(Ꮡp.Value.ParsedConfig.CompilerOptions.NoEmitForJsFiles.IsTrue() && tspath.HasJSFileExtension(@file)) && !tspath.IsDeclarationFileName(@file));

        Ꮡp.Value.commonSourceDirectory = outputpaths.GetCommonSourceDirectory(
            Ꮡp.Value.ParsedConfig.CompilerOptions,
            files,
            Ꮡp.Value.GetCurrentDirectory(),
            Ꮡp.Value.UseCaseSensitiveFileNames(),
            Ꮡp.checkSourceFilesBelongToPath);
    });
    return p.commonSourceDirectory;
}

internal static bool checkSourceFilesBelongToPath(this ref ParsedCommandLine p, slice<@string> sourceFiles, @string rootDirectory) {
    var allFilesBelongToPath = true;
    foreach (var (_, @file) in range(sourceFiles)) {
        @string absoluteSourceFilePath = tspath.GetCanonicalFileName(tspath.GetNormalizedAbsolutePath(@file, p.GetCurrentDirectory()), p.UseCaseSensitiveFileNames());
        if (!tspath.ContainsPath(rootDirectory, @file, p.comparePathsOptions)) {
            p.Errors = append(p.Errors, ast.NewCompilerDiagnostic(diagnostics.File_0_is_not_under_rootDir_1_rootDir_is_expected_to_contain_all_source_files, absoluteSourceFilePath, rootDirectory));
            allFilesBelongToPath = false;
        }
    }

    return allFilesBelongToPath;
}

public static @string GetCurrentDirectory(this ref ParsedCommandLine p) {
    return p.comparePathsOptions.CurrentDirectory;
}

public static bool UseCaseSensitiveFileNames(this ref ParsedCommandLine p) {
    return p.comparePathsOptions.UseCaseSensitiveFileNames;
}

internal static iter.Seq2<@string, @string> getOutputDeclarationAndSourceFileNames(this ж<ParsedCommandLine> Ꮡp) {
    return (Func<@string, @string, bool> yield) => {
        foreach (var (_, fileName) in range(Ꮡp.Value.ParsedConfig.FileNames)) {
            @string outputDts = default;
            if (!tspath.IsDeclarationFileName(fileName) && !tspath.FileExtensionIs(fileName, tspath.ExtensionJson)) {
                outputDts = outputpaths.GetOutputDeclarationFileNameWorker(fileName, Ꮡp.CompilerOptions(), new ParsedCommandLineжOutputPathsHost(Ꮡp));
            }
            if (!yield(outputDts, fileName)) {
                return;
            }
        }
    };
}

public static iter.Seq<@string> GetOutputFileNames(this ж<ParsedCommandLine> Ꮡp) {
    return (Func<@string, bool> yield) => {
        foreach (var (_, fileName) in range(Ꮡp.Value.ParsedConfig.FileNames)) {
            if (tspath.IsDeclarationFileName(fileName)) {
                continue;
            }
            @string jsFileName = outputpaths.GetOutputJSFileName(fileName, Ꮡp.CompilerOptions(), new ParsedCommandLineжOutputPathsHost(Ꮡp));
            var isJson = tspath.FileExtensionIs(fileName, tspath.ExtensionJson);
            if (jsFileName != ""u8) {
                if (!yield(jsFileName)) {
                    return;
                }
                if (!isJson) {
                    @string sourceMap = outputpaths.GetSourceMapFilePath(jsFileName, Ꮡp.CompilerOptions());
                    if (sourceMap != ""u8) {
                        if (!yield(sourceMap)) {
                            return;
                        }
                    }
                }
            }
            if (isJson) {
                continue;
            }
            if (Ꮡp.CompilerOptions().GetEmitDeclarations()) {
                @string dtsFileName = outputpaths.GetOutputDeclarationFileNameWorker(fileName, Ꮡp.CompilerOptions(), new ParsedCommandLineжOutputPathsHost(Ꮡp));
                if (dtsFileName != ""u8) {
                    if (!yield(dtsFileName)) {
                        return;
                    }
                    if (Ꮡp.GetContentMapperForFileName(fileName) == nil && Ꮡp.CompilerOptions().GetAreDeclarationMapsEnabled()) {
                        @string declarationMap = dtsFileName + ".map"u8;
                        if (!yield(declarationMap)) {
                            return;
                        }
                    }
                }
            }
        }
    };
}

public static @string GetBuildInfoFileName(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return outputpaths.GetBuildInfoFileName(Ꮡp.CompilerOptions(), p.comparePathsOptions);
}

public static map<@string, bool> WildcardDirectories(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp == nil) {
        return default;
    }

    Ꮡp.ᏑwildcardDirectoriesOnce.Do(() => {
        if (Ꮡp.Value.wildcardDirectories == nil) {
            Ꮡp.Value.wildcardDirectories = getWildcardDirectories(
                Ꮡp.Value.ConfigFile.configFileSpecs.validatedIncludeSpecs,
                Ꮡp.Value.ConfigFile.configFileSpecs.validatedExcludeSpecs,
                Ꮡp.Value.comparePathsOptions);
        }
    });

    return p.wildcardDirectories;
}

public static slice<ж<glob.Glob>> WildcardDirectoryGlobs(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var wildcardDirectories = Ꮡp.WildcardDirectories();
    if (wildcardDirectories == nil) {
        return default;
    }

    var wildcardDirectoriesʗ1 = wildcardDirectories;
    Ꮡp.ᏑincludeGlobsOnce.Do(() => {
        if (Ꮡp.Value.includeGlobs == nil) {
            var (fileGlob, recursiveFileGlob) = Ꮡp.fileGlobPatterns();
            var globs = new slice<ж<glob.Glob>>(0, len(wildcardDirectoriesʗ1));
            foreach (var (dir, recursive) in wildcardDirectoriesʗ1) {
                var (parsed, err) = glob.Parse(fmt.Sprintf("%s/%s"u8, tspath.NormalizePath(dir), core.IfElse(recursive, recursiveFileGlob, fileGlob))); if (err == nil) {
                    globs = append(globs, parsed);
                }
            }
            Ꮡp.Value.includeGlobs = globs;
        }
    });

    return p.includeGlobs;
}

public static slice<@string> LiteralFileNames(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp != nil && p.ConfigFile != nil) {
        return p.FileNames().slice(0, p.literalFileNamesLen);
    }
    return default;
}

public static void SetParsedOptions(this ref ParsedCommandLine p, ж<ParsedOptions> o) {
    p.ParsedConfig = o;
}

public static void SetCompilerOptions(this ref ParsedCommandLine p, ж<core.CompilerOptions> o) {
    p.ParsedConfig.CompilerOptions = o;
}

public static ж<core.CompilerOptions> CompilerOptions(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp == nil) {
        return default;
    }
    return p.ParsedConfig.CompilerOptions;
}

public static void SetTypeAcquisition(this ref ParsedCommandLine p, ж<core.TypeAcquisition> o) {
    p.ParsedConfig.TypeAcquisition = o;
}

public static ж<core.TypeAcquisition> TypeAcquisition(this ref ParsedCommandLine p) {
    return p.ParsedConfig.TypeAcquisition;
}

public static slice<@string> FileNames(this ref ParsedCommandLine p) {
    return p.ParsedConfig.FileNames;
}

public static map<tspath.Path, @string> FileNamesByPath(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑfileNamesByPathOnce.Do(() => {
        Ꮡp.Value.fileNamesByPath = new map<tspath.Path, @string>(len(Ꮡp.Value.ParsedConfig.FileNames));
        foreach (var (_, fileName) in range(Ꮡp.Value.ParsedConfig.FileNames)) {
            tspath.Path path = tspath.ToPath(fileName, Ꮡp.Value.GetCurrentDirectory(), Ꮡp.Value.UseCaseSensitiveFileNames());
            Ꮡp.Value.fileNamesByPath[path] = fileName;
        }
    });
    return p.fileNamesByPath;
}

public static slice<ж<core.ProjectReference>> ProjectReferences(this ref ParsedCommandLine p) {
    return p.ParsedConfig.ProjectReferences;
}

public static slice<ж<contentmapper.Mapper>> ContentMappers(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp == nil || p.ParsedConfig == nil) {
        return default;
    }
    return p.ParsedConfig.ContentMappers;
}

public static slice<@string> ContentMapperExtensions(this ж<ParsedCommandLine> Ꮡp) {
    return core.FlatMap(Ꮡp.ContentMappers(), slice<@string> (ж<contentmapper.Mapper> m) => m.Definition.Extensions);
}

public static ж<contentmapper.Mapper> GetContentMapperForFileName(this ж<ParsedCommandLine> Ꮡp, @string fileName) {
    ref var p = ref Ꮡp.DerefOrNull();

    var ignoreCase = !p.UseCaseSensitiveFileNames();
    @string extension = tspath.GetLongestExtensionFromPath(fileName, Ꮡp.ContentMapperExtensions(), ignoreCase);
    foreach (var (_, mapper) in range(Ꮡp.ContentMappers())) {
        if (slices.ContainsFunc(mapper.Definition.Extensions, (@string mapperExtension) => extension == mapperExtension || ignoreCase && strings.EqualFold(extension, mapperExtension))) {
            return mapper;
        }
    }
    return default;
}

public static slice<@string> ResolvedProjectReferencePaths(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑresolvedProjectReferencePathsOnce.Do(() => {
        Ꮡp.Value.resolvedProjectReferencePaths = core.Map<ж<core.ProjectReference>, @string>(Ꮡp.Value.ParsedConfig.ProjectReferences, core.ResolveProjectReferencePath);
    });
    return p.resolvedProjectReferencePaths;
}

public static slice<@string> ExtendedSourceFiles(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp == nil || p.ConfigFile == nil) {
        return default;
    }
    return p.ConfigFile.ExtendedSourceFiles;
}

public static slice<ж<ast.Diagnostic>> GetConfigFileParsingDiagnostics(this ref ParsedCommandLine p) {
    if (p.ConfigFile != nil) {
        return slices.Concat<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(p.ConfigFile.SourceFile.Diagnostics(), p.Errors);
    }
    return p.Errors;
}

public static bool PossiblyMatchesFileName(this ж<ParsedCommandLine> Ꮡp, @string fileName) {
    ref var p = ref Ꮡp.DerefOrNull();

    tspath.Path path = tspath.ToPath(fileName, p.GetCurrentDirectory(), p.UseCaseSensitiveFileNames());
    var (_, ok) = Ꮡp.FileNamesByPath()[path, ꟷ]; if (ok) {
        return true;
    }

    foreach (var (_, include) in range(p.ConfigFile.configFileSpecs.validatedIncludeSpecs)) {
        if (!strings.ContainsAny(include, "*?") && !vfsmatch.IsImplicitGlob(include)) {
            tspath.Path includePath = tspath.ToPath(include, p.GetCurrentDirectory(), p.UseCaseSensitiveFileNames());
            if (includePath == path) {
                return true;
            }
        }
    }
    if (Ꮡp.GetContentMapperForFileName(fileName) != nil) {
        tspath.Path directoryPath = path.GetDirectoryPath();
        if (Ꮡp.PossiblyMatchesDirectoryName(directoryPath)) {
            return true;
        }
    }
    var wildcardDirectoryGlobs = Ꮡp.WildcardDirectoryGlobs(); if (len(wildcardDirectoryGlobs) > 0) {
        foreach (var (_, glob) in range(wildcardDirectoryGlobs)) {
            if (glob.Match(fileName)) {
                return true;
            }
        }
    }
    return false;
}

public static bool PossiblyMatchesDirectoryName(this ж<ParsedCommandLine> Ꮡp, tspath.Path directoryPath) {
    ref var p = ref Ꮡp.DerefOrNull();

    foreach (var (wildcardDir, recursive) in Ꮡp.WildcardDirectories()) {
        tspath.Path wildcardDirPath = tspath.ToPath(wildcardDir, p.GetCurrentDirectory(), p.UseCaseSensitiveFileNames());
        if (recursive) {
            if (wildcardDirPath.ContainsPath(directoryPath)) {
                return true;
            }
        } else {
            if (wildcardDirPath == directoryPath) {
                return true;
            }
        }
    }
    return false;
}

public static @string GetMatchedFileSpec(this ref ParsedCommandLine p, @string fileName) {
    return p.ConfigFile.configFileSpecs.getMatchedFileSpec(fileName, p.comparePathsOptions);
}

public static (@string, bool) GetMatchedIncludeSpec(this ref ParsedCommandLine p, @string fileName) {
    if (len(p.ConfigFile.configFileSpecs.validatedIncludeSpecs) == 0) {
        return ("", false);
    }

    if (p.ConfigFile.configFileSpecs.isDefaultIncludeSpec) {
        return (p.ConfigFile.configFileSpecs.validatedIncludeSpecs[0], true);
    }

    return (p.ConfigFile.configFileSpecs.getMatchedIncludeSpec(fileName, p.comparePathsOptions), false);
}

public static ж<ParsedCommandLine> ReloadFileNamesOfParsedCommandLine(this ж<ParsedCommandLine> Ꮡp, Δvfs.FS fs) {
    ref var p = ref Ꮡp.DerefOrNull();

    ref var parsedConfig = ref heap<ParsedOptions>(p.ParsedConfig.Value, out var ᏑparsedConfig);
    var (fileNames, literalFileNamesLen) = getFileNamesFromConfigSpecs(
        p.ConfigFile.configFileSpecs.Value,
        p.GetCurrentDirectory(),
        Ꮡp.CompilerOptions(),
        fs,
        Ꮡp.ContentMapperExtensions());
    parsedConfig.FileNames = fileNames;
    ref var parsedCommandLine = ref heap<ParsedCommandLine>(new ParsedCommandLine(
        ParsedConfig: ᏑparsedConfig,
        ConfigFile: p.ConfigFile,
        Errors: p.Errors,
        Raw: p.Raw,
        CompileOnSave: p.CompileOnSave,
        comparePathsOptions: p.comparePathsOptions,
        wildcardDirectories: p.wildcardDirectories,
        includeGlobs: p.includeGlobs,
        literalFileNamesLen: literalFileNamesLen
    ), out var ᏑparsedCommandLine);
    return ᏑparsedCommandLine;
}

public static locale.Locale Locale(this ж<ParsedCommandLine> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑlocaleOnce.Do(() => {
        (Ꮡp.Value.locale, _) = locale.Parse(Ꮡp.CompilerOptions().Locale);
    });
    return p.locale;
}

} // end tsoptionsᴛParsedCommandLine
