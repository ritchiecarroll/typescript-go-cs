namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using fmt = fmt_package;
using io = io_package;
using maps = maps_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using checker = go.github.com.microsoft.TypeScript.tsc.@internal.checker_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using modulespecifiers = go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package;
using outputpaths = go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using parser = go.github.com.microsoft.TypeScript.tsc.@internal.parser_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using sourcemap = go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;
using symlinks = go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.sync;
using iter = iter_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using ꓸꓸꓸany = Span<any>;
using compiler = compiler_package;
using static compiler_package;

partial class compiler_package {

partial struct ProgramOptions {
    public CompilerHost Host;
    public ж<tsoptions.ParsedCommandLine> Config;
    public bool UseSourceOfProjectReference;
    public core.Tristate SingleThreaded;
    public Func<ж<Program>, CheckerPool> CreateCheckerPool;
    public @string TypingsLocation;
    public @string ProjectName;
    public ж<tracing.Tracing> Tracing;
    public bool SkipModuleResolution;
}

} partial class compilerᴛProgramOptions {

internal static bool canUseProjectReferenceSource(this ref ProgramOptions p) {
    return p.UseSourceOfProjectReference && !p.Config.CompilerOptions().DisableSourceOfProjectReferenceRedirect.IsTrue();
}

} partial class compiler_package {

partial struct lazyValue<T> {
    internal ж<T> value;
    internal sync.Once once;
    internal atomic.Bool initialized;
}

} partial class compilerᴛlazyValue {

internal static ж<T> getValue<T>(this ж<lazyValue<T>> Ꮡl, Func<ж<T>> compute) {
    ref var l = ref Ꮡl.DerefOrNull();

    Ꮡl.Ꮡonce.Do(() => {
        if (Ꮡl.Value.value == nil) {
            Ꮡl.Value.value = compute();
        }
        Ꮡl.Ꮡinitialized.Store(true);
    });
    return l.value;
}

internal static void tryReuse<T>(this ж<lazyValue<T>> Ꮡl, ж<lazyValue<T>> Ꮡfrom) {
    ref var l = ref Ꮡl.DerefOrNull();
    ref var from = ref Ꮡfrom.DerefOrNull();

    if (Ꮡfrom.Ꮡinitialized.Load()) {
        l.value = from.value;
        Ꮡl.Ꮡinitialized.Store(true);
    }
}

} partial class compiler_package {

partial struct packageNamesInfo {
    internal ж<collections.Set<@string>> resolved;
    internal ж<collections.Set<@string>> unresolved;
    internal ж<collections.Set<@string>> deepImportPackages;
}

partial struct Program {
    internal ProgramOptions opts;
    internal CheckerPool checkerPool;
    internal ж<checkerPool> compilerCheckerPool;
    internal tspath.ComparePathsOptions comparePathsOptions;
    /*embed*/ internal processedFiles processedFiles;
    internal core.Tristate usesUriStyleNodeCoreModules;
    internal @string commonSourceDirectory;
    internal sync.Once commonSourceDirectoryOnce;
    internal collections.SyncMap<ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> declarationDiagnosticCache;
    internal slice<ж<ast.Diagnostic>> programDiagnostics;
    internal collections.Set<tspath.Path> hasEmitBlockingDiagnostics;
    internal slice<ж<ast.Diagnostic>> contentMapperOptionDiagnostics;
    internal sync.Once sourceFilesToEmitOnce;
    internal slice<ж<ast.SourceFile>> sourceFilesToEmit;
    internal lazyValue<collections.Set<@string>> unresolvedImports;
    internal lazyValue<symlinks.KnownSymlinks> knownSymlinks;
    internal lazyValue<packageNamesInfo> packageNames;
    internal sync.Once hasTSFileOnce;
    internal bool hasTSFile;
    internal sync.Once packagesMapOnce;
    internal map<@string, bool> packagesMap;
}

} partial class compilerᴛProgram {

public static bool FileExists(this ref Program p, @string path) {
    return p.Host().FS().FileExists(path);
}

public static @string GetCurrentDirectory(this ref Program p) {
    return p.Host().GetCurrentDirectory();
}

public static contentmapper.Project ContentMapperProject(this ref Program p) {
    return p.opts.Host.ContentMapperProject();
}

public static @string GetGlobalTypingsCacheLocation(this ref Program p) {
    return p.opts.TypingsLocation;
}

public static @string GetNearestAncestorDirectoryWithPackageJson(this ref Program p, @string dirname) {
    var @scoped = p.resolver.GetPackageScopeForPath(dirname);
    if (@scoped != nil && @scoped.Exists()) {
        return @scoped.PackageDirectory;
    }
    return "";
}

public static ж<packagejson.InfoCacheEntry> GetPackageJsonInfo(this ref Program p, @string pkgJsonPath) {
    @string directory = tspath.GetDirectoryPath(pkgJsonPath);
    var @scoped = p.resolver.GetPackageScopeForPath(directory);
    if (@scoped != nil && @scoped.Exists() && @scoped.PackageDirectory == directory) {
        return @scoped;
    }
    return default;
}

public static void PackageJsonCacheEntries(this ref Program p, Func<tspath.Path, ж<packagejson.InfoCacheEntry>, bool> f) {
    p.resolver.PackageJsonCacheEntries(f);
}

public static slice<@string> GetRedirectTargets(this ref Program p, tspath.Path path) {
    return p.redirectTargetsMap[path];
}

public static @string GetSourceOfProjectReferenceIfOutputIncluded(this ref Program p, ast.HasFileName @file) {
    var (source, ok) = p.outputFileToProjectReferenceSource[@file.Path(), ꟷ]; if (ok) {
        return source;
    }
    return @file.FileName();
}

public static ж<tsoptions.SourceOutputAndProjectReference> GetProjectReferenceFromSource(this ref Program p, tspath.Path path) {
    return p.projectReferenceFileMapper.getProjectReferenceFromSource(path);
}

public static bool IsSourceFromProjectReference(this ref Program p, tspath.Path path) {
    return p.projectReferenceFileMapper.isSourceFromProjectReference(path);
}

public static ж<tsoptions.SourceOutputAndProjectReference> GetProjectReferenceFromOutputDts(this ref Program p, tspath.Path path) {
    return p.projectReferenceFileMapper.getProjectReferenceFromOutputDts(path);
}

public static (ж<tsoptions.ParsedCommandLine>, bool) GetResolvedProjectReferenceFor(this ref Program p, tspath.Path path) {
    return p.projectReferenceFileMapper.getResolvedReferenceFor(path);
}

public static ж<tsoptions.ParsedCommandLine> GetRedirectForResolution(this ref Program p, ast.HasFileName @file) {
    var (redirect, _) = p.projectReferenceFileMapper.getRedirectForResolution(@file);
    return redirect;
}

public static @string GetParseFileRedirect(this ref Program p, @string fileName) {
    return p.projectReferenceFileMapper.getParseFileRedirect(ast.NewHasFileName(fileName, p.toPath(fileName)));
}

public static slice<ж<tsoptions.ParsedCommandLine>> GetResolvedProjectReferences(this ref Program p) {
    return p.projectReferenceFileMapper.getResolvedProjectReferences();
}

public static bool RangeResolvedProjectReference(this ref Program p, Func<tspath.Path, ж<tsoptions.ParsedCommandLine>, ж<tsoptions.ParsedCommandLine>, nint, bool> f) {
    return p.projectReferenceFileMapper.rangeResolvedProjectReference(f);
}

public static bool RangeResolvedProjectReferenceInChildConfig(this ref Program p, ж<tsoptions.ParsedCommandLine> childConfig, Func<tspath.Path, ж<tsoptions.ParsedCommandLine>, ж<tsoptions.ParsedCommandLine>, nint, bool> f) {
    return p.projectReferenceFileMapper.rangeResolvedProjectReferenceInChildConfig(childConfig, f);
}

public static bool UseCaseSensitiveFileNames(this ref Program p) {
    return p.Host().FS().UseCaseSensitiveFileNames();
}

public static core.Tristate UsesUriStyleNodeCoreModules(this ref Program p) {
    return p.usesUriStyleNodeCoreModules;
}

} partial class compiler_package {

internal static checker.Program _ᴛ5ʗ = new ProgramжProgram(((ж<Program>)nil));

} partial class compilerᴛProgram {

public static ж<ast.SourceFile> GetSourceFileFromReference(this ref Program p, ж<ast.SourceFile> Ꮡorigin, ж<ast.FileReference> Ꮡref) {
    ref var origin = ref Ꮡorigin.DerefOrNull();
    ref var @ref = ref Ꮡref.DerefOrNull();

    @string fileName = tspath.ResolvePath(tspath.GetDirectoryPath(origin.FileName()), @ref.FileName);
    var supportedExtensionsBase = tsoptions.GetSupportedExtensions(p.Options(), p.CommandLine().ContentMapperExtensions());
    var supportedExtensions = tsoptions.GetSupportedExtensionsWithJsonIfResolveJsonModule(p.Options(), supportedExtensionsBase);
    var allowNonTsExtensions = p.Options().AllowNonTsExtensions.IsTrue();
    if (tspath.HasExtension(fileName)) {
        if (!allowNonTsExtensions) {
            @string canonicalFileName = tspath.GetCanonicalFileName(fileName, p.UseCaseSensitiveFileNames());
            var supported = false;
            foreach (var (_, group) in range(supportedExtensions)) {
                if (tspath.FileExtensionIsOneOf(canonicalFileName, group)) {
                    supported = true;
                    break;
                }
            }
            if (!supported) {
                return default;
            }
        }

        return p.GetSourceFileForResolvedModule(fileName);
    }
    if (allowNonTsExtensions) {
        var extensionless = p.GetSourceFileForResolvedModule(fileName);
        if (extensionless != nil) {
            return extensionless;
        }
    }

    foreach (var (_, ext) in range(supportedExtensions[0])) {
        var result = p.GetSourceFileForResolvedModule(fileName + ext);
        if (result != nil) {
            return result;
        }
    }
    return default;
}

} partial class compiler_package {

public static ж<Program> NewProgram(ProgramOptions opts) {
    GoFrame ᒐ = default;
    try {
        var p = Ꮡ(new Program(opts: opts));
        if (p.opts.Tracing != nil) {
            var optsʗ1 = opts;
            var pʗ1 = p;
            defer(pʗ1.opts.Tracing.Push(tracing.PhaseProgram, "createProgram", new map<@string, any>{["configFilePath"] = optsʗ1.Config.CompilerOptions().ConfigFilePath}, true), ref ᒐ);
        }
        p.processedFiles = processAllProgramFiles(p.opts, p.SingleThreaded());
        p.initCheckerPool();
        p.verifyCompilerOptions();
        p.collectContentMapperOptionDiagnostics();
        return p;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class compilerᴛProgram {

public static (ж<Program>, ж<ast.SourceFile>, bool) UpdateProgram(this ж<Program> Ꮡp, tspath.Path changedFilePath, CompilerHost newHost, Func<ж<Program>, CheckerPool> createCheckerPool) {
    ref var p = ref Ꮡp.DerefOrNull();

    var (result, newFile, reused) = Ꮡp.ReuseProgram(changedFilePath, newHost, createCheckerPool); if (reused) {
        return (result, newFile, true);
    } else {
        var newOpts = p.opts;
        newOpts.Host = newHost;
        if (createCheckerPool != nil) {
            newOpts.CreateCheckerPool = createCheckerPool;
        }
        return (NewProgram(newOpts), newFile, false);
    }
}

public static (ж<Program>, ж<ast.SourceFile>, bool) ReuseProgram(this ж<Program> Ꮡp, tspath.Path changedFilePath, CompilerHost newHost, Func<ж<Program>, CheckerPool> createCheckerPool) {
    ref var p = ref Ꮡp.DerefOrNull();

    ref var newOpts = ref heap<ProgramOptions>(p.opts, out var ᏑnewOpts);
    newOpts.Host = newHost;
    if (createCheckerPool != nil) {
        newOpts.CreateCheckerPool = createCheckerPool;
    }

    var oldFile = p.filesByPath[changedFilePath];
    ж<ast.SourceFile> newFile = default;
    slice<ж<ast.SourceFile>> oldSupplementalFiles = default;
    slice<ж<ast.SourceFile>> newSupplementalFiles = default;
    if (oldFile.ContentMapper() != ""u8) {
        var mapper = newOpts.Config.GetContentMapperForFileName(oldFile.FileName());
        error err = default;
        var (files, transformErr) = newHost.GetContentMappedSourceFiles(oldFile.ParseOptions(), mapper);
        (newFile, err) = (files.Canonical, transformErr);
        if (err != nil) {
            return (default, default, false);
        }
        oldSupplementalFiles = oldFile.SupplementalSourceFiles();
        newSupplementalFiles = files.Supplemental;
    } else {
        newFile = newHost.GetSourceFile(oldFile.ParseOptions());
    }

    var (_, inRedirectFiles) = p.redirectFilesByPath[changedFilePath, ꟷ];
    var (_, isRedirectTarget) = p.redirectTargetsMap[changedFilePath, ꟷ];
    if (inRedirectFiles || isRedirectTarget) {
        return (default, newFile, false);
    }

    if (!Ꮡp.canReplaceFileInProgram(oldFile, newFile)) {
        return (default, newFile, false);
    }
    if (p.importHelpersImportSpecifiers[oldFile.Path()] != nil || p.needsImportHelpersImportSpecifier(newFile)) {
        return (default, newFile, false);
    }
    if (p.jsxRuntimeImportSpecifiers[oldFile.Path()] != nil || p.jsxRuntimeImportSpecifier(newFile) != ""u8) {
        return (default, newFile, false);
    }
    if (len(oldSupplementalFiles) != len(newSupplementalFiles)) {
        return (default, newFile, false);
    }
    foreach (var (i, oldSupplemental) in range(oldSupplementalFiles)) {
        var newSupplemental = newSupplementalFiles[i];
        if (oldSupplemental.Path() != newSupplemental.Path() || !Ꮡp.canReplaceFileInProgram(oldSupplemental, newSupplemental)) {
            return (default, newFile, false);
        }
        if (p.importHelpersImportSpecifiers[oldSupplemental.Path()] != nil || p.needsImportHelpersImportSpecifier(newSupplemental)) {
            return (default, newFile, false);
        }
        if (p.jsxRuntimeImportSpecifiers[oldSupplemental.Path()] != nil || p.jsxRuntimeImportSpecifier(newSupplemental) != ""u8) {
            return (default, newFile, false);
        }
    }
    var result = Ꮡ(new Program(
        opts: newOpts,
        comparePathsOptions: p.comparePathsOptions,
        processedFiles: p.processedFiles,
        usesUriStyleNodeCoreModules: p.usesUriStyleNodeCoreModules,
        programDiagnostics: p.programDiagnostics,
        hasEmitBlockingDiagnostics: p.hasEmitBlockingDiagnostics,
        contentMapperOptionDiagnostics: slices.Clone<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(p.contentMapperOptionDiagnostics)
    ));
    result.ᏑunresolvedImports.tryReuse(Ꮡp.ᏑunresolvedImports);
    result.ᏑknownSymlinks.tryReuse(Ꮡp.ᏑknownSymlinks);
    result.ᏑpackageNames.tryReuse(Ꮡp.ᏑpackageNames);
    result.initCheckerPool();
    var newFileʗ1 = newFile;
    nint index = core.FindIndex(result.files, (ж<ast.SourceFile> @file) => @file.Path() == newFileʗ1.Path());
    result.files = slices.Clone<slice<ж<ast.SourceFile>>, ж<ast.SourceFile>>(result.files);
    result.files[index] = newFile;
    result.filesByPath = maps.Clone<map<tspath.Path, ж<ast.SourceFile>>, tspath.Path, ж<ast.SourceFile>>(result.filesByPath);
    result.filesByPath[newFile.Path()] = newFile;
    if (len(oldSupplementalFiles) != 0) {
        foreach (var (i, oldSupplemental) in range(oldSupplementalFiles)) {
            var newSupplemental = newSupplementalFiles[i];
            var oldSupplementalʗ1 = oldSupplemental;
            nint supplementalIndex = core.FindIndex(result.files, (ж<ast.SourceFile> @file) => @file == oldSupplementalʗ1);
            result.files[supplementalIndex] = newSupplemental;
            result.filesByPath[newSupplemental.Path()] = newSupplemental;
        }
    }
    updateFileIncludeProcessor(ref (result).DerefOrNull());
    return (result, newFile, true);
}

internal static void initCheckerPool(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (!p.finishedProcessing) {
        throw panic("Program must finish processing files before initializing checker pool");
    }

    if (p.opts.CreateCheckerPool != nil) {
        p.checkerPool = p.opts.CreateCheckerPool(Ꮡp);
    } else {
        var pool = newCheckerPoolWithTracing(Ꮡp, p.opts.Tracing);
        p.checkerPool = new checkerPoolжCheckerPool(pool);
        p.compilerCheckerPool = pool;
    }
}

public static CheckerPool GetCheckerPool(this ref Program p) {
    return p.checkerPool;
}

internal static bool canReplaceFileInProgram(this ж<Program> Ꮡp, ж<ast.SourceFile> Ꮡfile1, ж<ast.SourceFile> Ꮡfile2) {
    ref var file1 = ref Ꮡfile1.DerefOrNull();
    ref var file2 = ref Ꮡfile2.DerefOrNull();

    return Ꮡfile2 != nil && file1.ParseOptions() == file2.ParseOptions() && file1.ScriptKind == file2.ScriptKind && ast.IsExternalOrCommonJSModule(Ꮡfile1) == ast.IsExternalOrCommonJSModule(Ꮡfile2) && file1.UsesUriStyleNodeCoreModules == file2.UsesUriStyleNodeCoreModules && slices.EqualFunc(file1.Imports(), file2.Imports(), (ж<ast.Node> n1, ж<ast.Node> n2) => equalModuleSpecifiers(n1, n2) && Ꮡp.Value.GetModeForUsageLocation(new ast.SourceFileжHasFileName(Ꮡfile1), n1) == Ꮡp.Value.GetModeForUsageLocation(new ast.SourceFileжHasFileName(Ꮡfile2), n2)) && slices.EqualFunc<slice<ж<astꓸModuleName>>, slice<ж<astꓸModuleName>>, ж<ast.Node>, ж<ast.Node>>(file1.ModuleAugmentations, file2.ModuleAugmentations, equalModuleAugmentationNames) && slices.Equal<slice<@string>, @string>(file1.AmbientModuleNames, file2.AmbientModuleNames) && slices.EqualFunc<slice<ж<ast.FileReference>>, slice<ж<ast.FileReference>>, ж<ast.FileReference>, ж<ast.FileReference>>(file1.ReferencedFiles, file2.ReferencedFiles, equalFileReferences) && slices.EqualFunc<slice<ж<ast.FileReference>>, slice<ж<ast.FileReference>>, ж<ast.FileReference>, ж<ast.FileReference>>(file1.TypeReferenceDirectives, file2.TypeReferenceDirectives, equalFileReferences) && slices.EqualFunc<slice<ж<ast.FileReference>>, slice<ж<ast.FileReference>>, ж<ast.FileReference>, ж<ast.FileReference>>(file1.LibReferenceDirectives, file2.LibReferenceDirectives, equalFileReferences) && equalCheckJSDirectives(file1.CheckJsDirective, file2.CheckJsDirective);
}

internal static bool needsImportHelpersImportSpecifier(this ref Program p, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    var (redirect, _) = p.projectReferenceFileMapper.getRedirectForResolution(new ast.SourceFileжHasFileName(Ꮡfile));
    var optionsForFile = module.GetCompilerOptionsWithRedirect(p.opts.Config.CompilerOptions(), new tsoptions.ParsedCommandLineжResolvedProjectReference(redirect));
    if (!optionsForFile.ImportHelpers.IsTrue()) {
        return false;
    }
    var isJavaScriptFile = ast.IsSourceFileJS(Ꮡfile);
    var isExternalModuleFile = ast.IsExternalModule(Ꮡfile);
    if (!isJavaScriptFile && (@file.IsDeclarationFile || (!optionsForFile.GetIsolatedModules() && !isExternalModuleFile))) {
        return false;
    }
    return true;
}

internal static @string jsxRuntimeImportSpecifier(this ref Program p, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (!ast.IsSourceFileJS(Ꮡfile) && @file.ScriptKind != core.ScriptKindTSX) {
        return "";
    }
    var (redirect, _) = p.projectReferenceFileMapper.getRedirectForResolution(new ast.SourceFileжHasFileName(Ꮡfile));
    var optionsForFile = module.GetCompilerOptionsWithRedirect(p.opts.Config.CompilerOptions(), new tsoptions.ParsedCommandLineжResolvedProjectReference(redirect));
    return ast.GetJSXRuntimeImport(ast.GetJSXImplicitImportBase(optionsForFile, Ꮡfile), optionsForFile);
}

} partial class compiler_package {

internal static bool equalModuleSpecifiers(ж<ast.Node> Ꮡn1, ж<ast.Node> Ꮡn2) {
    ref var n1 = ref Ꮡn1.DerefOrNull();
    ref var n2 = ref Ꮡn2.DerefOrNull();

    return n1.Kind == n2.Kind && (!ast.IsStringLiteral(Ꮡn1) || n1.Text() == n2.Text());
}

internal static bool equalModuleAugmentationNames(ж<ast.Node> Ꮡn1, ж<ast.Node> Ꮡn2) {
    ref var n1 = ref Ꮡn1.DerefOrNull();
    ref var n2 = ref Ꮡn2.DerefOrNull();

    return n1.Kind == n2.Kind && n1.Text() == n2.Text();
}

internal static bool equalFileReferences(ж<ast.FileReference> Ꮡf1, ж<ast.FileReference> Ꮡf2) {
    ref var f1 = ref Ꮡf1.DerefOrNull();
    ref var f2 = ref Ꮡf2.DerefOrNull();

    return f1.FileName == f2.FileName && f1.ResolutionMode == f2.ResolutionMode && f1.Preserve == f2.Preserve;
}

internal static bool equalCheckJSDirectives(ж<ast.CheckJsDirective> Ꮡd1, ж<ast.CheckJsDirective> Ꮡd2) {
    ref var d1 = ref Ꮡd1.DerefOrNull();
    ref var d2 = ref Ꮡd2.DerefOrNull();

    return Ꮡd1 == nil && Ꮡd2 == nil || Ꮡd1 != nil && Ꮡd2 != nil && d1.Enabled == d2.Enabled;
}

} partial class compilerᴛProgram {

public static slice<ж<ast.SourceFile>> SourceFiles(this ref Program p) { return p.files; }
public static slice<ж<DuplicateSourceFile>> DuplicateSourceFiles(this ref Program p) { return p.duplicateSourceFiles; }
public static ж<core.CompilerOptions> Options(this ref Program p) { return p.opts.Config.CompilerOptions(); }

public static ж<contentmapper.Mapper> GetContentMapper(this ref Program p, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (@file.ContentMapper() == ""u8) {
        return default;
    }
    var mapper = p.opts.Config.GetContentMapperForFileName(@file.FileName());
    if (mapper != nil && mapper.Identity() == @file.ContentMapper()) {
        return mapper;
    }
    return default;
}

public static slice<@string> ContentMapperExtensions(this ref Program p) { return p.opts.Config.ContentMapperExtensions(); }
public static ж<tsoptions.ParsedCommandLine> CommandLine(this ref Program p) { return p.opts.Config; }
public static CompilerHost Host(this ref Program p) { return p.opts.Host; }
public static ж<tracing.Tracing> Tracing(this ref Program p) { return p.opts.Tracing; }
public static slice<ж<ast.Diagnostic>> GetConfigFileParsingDiagnostics(this ref Program p) {
    return slices.Clip<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(p.opts.Config.GetConfigFileParsingDiagnostics());
}

public static ж<collections.Set<@string>> GetUnresolvedImports(this ж<Program> Ꮡp) {
    return Ꮡp.ᏑunresolvedImports.getValue(Ꮡp.extractUnresolvedImports);
}

internal static ж<collections.Set<@string>> extractUnresolvedImports(this ref Program p) {
    var unresolvedSet = Ꮡ(new collections.Set<@string>());

    foreach (var (_, sourceFile) in range(p.files)) {
        var unresolvedImports = p.extractUnresolvedImportsFromSourceFile(sourceFile);
        foreach (var (_, imp) in range(unresolvedImports)) {
            unresolvedSet.Add(imp);
        }
    }

    return unresolvedSet;
}

internal static slice<@string> extractUnresolvedImportsFromSourceFile(this ref Program p, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();
    var unresolvedImportsᴷ = new stackAppend<@string>(moves: true);

    slice<@string> unresolvedImports = default;

    var resolvedModules = p.resolvedModules[@file.Path()];
    foreach (var (cacheKey, resolution) in resolvedModules) {
        var resolved = resolution.IsResolved();
        if ((!resolved || !tspath.ExtensionIsOneOf(resolution.Extension, tspath.SupportedTSExtensionsWithJsonFlat)) && !tspath.IsExternalModuleNameRelative(cacheKey.Name)) {
            unresolvedImports = append(unresolvedImportsᴷ, unresolvedImports, cacheKey.Name);
        }
    }

    unresolvedImports = move2heap(unresolvedImportsᴷ, unresolvedImports);
    return unresolvedImports;
}

public static bool SingleThreaded(this ref Program p) {
    return p.opts.SingleThreaded.DefaultIfUnknown(p.Options().SingleThreaded).IsTrue();
}

public static void BindSourceFiles(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var wg = core.NewWorkGroup(p.SingleThreaded());
    foreach (var (_, @file) in range(p.files)) {
        if (!@file.IsBound()) {
            var fileʗ1 = @file;
            wg.Queue(() => {
                GoFrame ᒐ = default;
                try {
                    if (Ꮡp.Value.opts.Tracing != nil) {
                        var fileʗ2 = fileʗ1;
                        defer(Ꮡp.Value.opts.Tracing.Push(tracing.PhaseBind, "bindSourceFile", new map<@string, any>{["path"] = ((@string)fileʗ2.Path())}, true), ref ᒐ);
                    }
                    binder.BindSourceFile(fileʗ1);
                }
                catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
                finally { ᒐ.Run(); }
            });
        }
    }
    wg.RunAndWait();
}

public static (ж<checker.Checker>, Action) GetTypeChecker(this ref Program p, context.Context ctx) {
    if (p.compilerCheckerPool != nil) {
        return p.compilerCheckerPool.getCheckerNonExclusive();
    }
    return p.checkerPool.GetChecker(ctx, nil);
}

public static void ForEachCheckerParallel(this ref Program p, Action<nint, ж<checker.Checker>> cb) {
    if (p.compilerCheckerPool != nil) {
        p.compilerCheckerPool.forEachCheckerParallel(cb);
    }
}

public static (ж<checker.Checker>, Action) GetTypeCheckerForFile(this ref Program p, context.Context ctx, ж<ast.SourceFile> @file) {
    if (p.compilerCheckerPool != nil) {
        return p.compilerCheckerPool.getCheckerForFileNonExclusive(@file);
    }
    return p.checkerPool.GetChecker(ctx, @file);
}

public static (ж<checker.Checker>, Action) GetTypeCheckerForFileExclusive(this ref Program p, context.Context ctx, ж<ast.SourceFile> @file) {
    if (p.compilerCheckerPool != nil) {
        return p.compilerCheckerPool.getCheckerForFileExclusive(ctx, @file);
    }
    return p.checkerPool.GetChecker(ctx, @file);
}

public static ж<module.ResolvedModule> GetResolvedModule(this ref Program p, ast.HasFileName @file, @string moduleReference, coreꓸResolutionMode mode) {
    var (resolutions, ok) = p.resolvedModules[@file.Path(), ꟷ]; if (ok) {
        var (resolved, okΔ1) = resolutions[new module.ModeAwareCacheKey(Name: moduleReference, Mode: mode), ꟷ]; if (okΔ1) {
            return resolved;
        }
    }
    return default;
}

public static ж<module.ResolvedModule> GetResolvedModuleFromModuleSpecifier(this ref Program p, ast.HasFileName @file, ж<astꓸStringLiteralLike> ᏑmoduleSpecifier) {
    ref var moduleSpecifier = ref ᏑmoduleSpecifier.DerefOrNull();

    if (!ast.IsStringLiteralLike(ᏑmoduleSpecifier)) {
        throw panic("moduleSpecifier must be a StringLiteralLike");
    }
    var mode = p.GetModeForUsageLocation(@file, ᏑmoduleSpecifier);
    return p.GetResolvedModule(@file, moduleSpecifier.Text(), mode);
}

public static map<tspath.Path, module.ModeAwareCache<ж<module.ResolvedModule>>> GetResolvedModules(this ref Program p) {
    return p.resolvedModules;
}

public static map<@string, bool> GetPackagesMap(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑpackagesMapOnce.Do(() => {
        Ꮡp.Value.packagesMap = new map<@string, bool>();
        foreach (var (_, resolvedModulesInFile) in Ꮡp.Value.resolvedModules) {
            foreach (var (_, mod) in resolvedModulesInFile) {
                if (mod.PackageId.Name != ""u8) {
                    Ꮡp.Value.packagesMap[mod.PackageId.Name] = Ꮡp.Value.packagesMap[mod.PackageId.Name] || mod.Extension == tspath.ExtensionDts;
                }
            }
        }
    });
    return p.packagesMap;
}

internal static slice<ж<ast.Diagnostic>> collectDiagnostics(this ref Program p, context.Context ctx, ж<ast.SourceFile> sourceFile, bool concurrent, Func<context.Context, ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> collect) {
    slice<ж<ast.Diagnostic>> result = default;
    if (sourceFile != nil) {
        result = collect(ctx, sourceFile);
    } else {
        var diagnostics = p.collectDiagnosticsFromFiles(ctx, p.files, concurrent, collect);
        result = slices.Concat<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(diagnostics.ꓸꓸꓸ);
    }
    return filterAndSortDiagnostics(result);
}

internal static slice<slice<ж<ast.Diagnostic>>> collectDiagnosticsFromFiles(this ref Program p, context.Context ctx, slice<ж<ast.SourceFile>> sourceFiles, bool concurrent, Func<context.Context, ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> collect) {
    var diagnostics = new slice<slice<ж<ast.Diagnostic>>>(len(sourceFiles));
    var wg = core.NewWorkGroup(!concurrent || p.SingleThreaded());
    foreach (var (i, @file) in range(sourceFiles)) {
        var diagnosticsʗ1 = diagnostics;
        var fileʗ1 = @file;
        wg.Queue(() => {
            diagnosticsʗ1[i] = collect(ctx, fileʗ1);
        });
    }
    wg.RunAndWait();
    return diagnostics;
}

internal static slice<ж<ast.Diagnostic>> collectCheckerDiagnostics(this ж<Program> Ꮡp, context.Context ctx, ж<ast.SourceFile> sourceFile, Func<context.Context, ж<checker.Checker>, ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> collect) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (sourceFile != nil) {
        if (p.SkipTypeChecking(sourceFile, false)) {
            return default;
        }
        var (c, done) = p.GetTypeCheckerForFileExclusive(ctx, sourceFile);
        var result = collect(ctx, c, sourceFile);
        done();
        return filterAndSortDiagnostics(result);
    }
    return filterAndSortDiagnostics(slices.Concat<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(Ꮡp.collectCheckerDiagnosticsFromFiles(ctx, p.files, collect).ꓸꓸꓸ));
}

} partial class compiler_package {

internal static slice<ж<ast.Diagnostic>> filterAndSortDiagnostics(slice<ж<ast.Diagnostic>> diags) {
    return SortAndDeduplicateDiagnostics(core.Filter(diags, (ж<ast.Diagnostic> diag) => {
        var @file = diag.File();
        if (!diag.ReportsUnnecessary() || @file == nil || @file.SpanMap() == nil || diag.Source() != ""u8) {
            return true;
        }
        var (_, fidelity) = @file.SpanMap().VirtualToOriginalSpan(diag.Loc());
        return fidelity != spanmap.FidelityNone;
    }));
}

} partial class compilerᴛProgram {

internal static slice<slice<ж<ast.Diagnostic>>> collectCheckerDiagnosticsFromFiles(this ж<Program> Ꮡp, context.Context ctx, slice<ж<ast.SourceFile>> sourceFiles, Func<context.Context, ж<checker.Checker>, ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> collect) {
    ref var p = ref Ꮡp.DerefOrNull();

    var diagnostics = new slice<slice<ж<ast.Diagnostic>>>(len(sourceFiles));
    if (p.compilerCheckerPool != nil) {
        var diagnosticsʗ1 = diagnostics;
        p.compilerCheckerPool.forEachCheckerGroupDo(ctx, sourceFiles, p.SingleThreaded(), (ж<checker.Checker> c, nint fileIndex, ж<ast.SourceFile> @file) => {
            diagnosticsʗ1[fileIndex] = collect(ctx, c, @file);
        });
    } else {
        var wg = core.NewWorkGroup(p.SingleThreaded());
        foreach (var (i, @file) in range(sourceFiles)) {
            if (p.SkipTypeChecking(@file, false)) {
                continue;
            }
            var diagnosticsʗ2 = diagnostics;
            var fileʗ1 = @file;
            wg.Queue(() => {
                var (c, done) = Ꮡp.Value.checkerPool.GetChecker(ctx, fileʗ1);
                diagnosticsʗ2[i] = collect(ctx, c, fileʗ1);
                done();
            });
        }
        wg.RunAndWait();
    }
    return diagnostics;
}

public static slice<ж<ast.Diagnostic>> GetSyntacticDiagnostics(this ж<Program> Ꮡp, context.Context ctx, ж<ast.SourceFile> sourceFile) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.collectDiagnostics(ctx, sourceFile, false, (context.Context _, ж<ast.SourceFile> @file) => {
        var diags = core.Concatenate(@file.Diagnostics(), @file.JSDiagnostics());
        if (ast.IsSourceFileJS(@file) && !ast.IsCheckJSEnabledForFile(@file, Ꮡp.Value.Options())) {
            diags = appendꓸꓸꓸ(diags, getAdditionalJSSyntacticDiagnostics(@file, ref (Ꮡp.Value.Options()).DerefOrNull()));
        }
        return diags;
    });
}

} partial class compiler_package {

internal static slice<ж<ast.Diagnostic>> getAdditionalJSSyntacticDiagnostics(ж<ast.SourceFile> @file, ref core.CompilerOptions options) {
    if (options.ExperimentalDecorators.IsTrue()) {
        return default;
    }
    ref var diags = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡdiags);
    ref var walk = ref heap<Func<ж<ast.Node>, bool>>(out var Ꮡwalk);
    walk = (ж<ast.Node> node) => {
        if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsDecorators) == 0) {
            return false;
        }
        if (node.Kind == ast.KindParameter && ast.HasDecorators(node)) {
            var decorator = core.Find<ж<ast.Node>>(node.ModifierNodes(), ast.IsDecorator);
            if (decorator != nil) {
                Ꮡdiags.ValueSlot = append(Ꮡdiags.ValueSlot, ast.NewDiagnostic(@file, decorator.Loc, diagnostics.Decorators_are_not_valid_here));
            }
        }
        node.ForEachChild(Ꮡwalk.ValueSlot);
        return false;
    };
    @file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode().ForEachChild(walk);
    return diags;
}

} partial class compilerᴛProgram {

public static slice<ж<ast.Diagnostic>> GetBindDiagnostics(this ж<Program> Ꮡp, context.Context ctx, ж<ast.SourceFile> sourceFile) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (sourceFile != nil) {
        binder.BindSourceFile(sourceFile);
    } else {
        Ꮡp.BindSourceFiles();
    }
    return p.collectDiagnostics(ctx, sourceFile, false, (context.Context _, ж<ast.SourceFile> @file) => @file.BindDiagnostics());
}

public static slice<ж<ast.Diagnostic>> GetSemanticDiagnostics(this ж<Program> Ꮡp, context.Context ctx, ж<ast.SourceFile> sourceFile) {
    return Ꮡp.collectCheckerDiagnostics(ctx, sourceFile, Ꮡp.getSemanticDiagnosticsWithChecker);
}

public static map<ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> GetSemanticDiagnosticsWithoutNoEmitFiltering(this ж<Program> Ꮡp, context.Context ctx, slice<ж<ast.SourceFile>> sourceFiles) {
    var allDiags = Ꮡp.collectCheckerDiagnosticsFromFiles(ctx, sourceFiles, Ꮡp.getBindAndCheckDiagnosticsWithChecker);
    var result = new map<ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>>(len(sourceFiles));
    foreach (var (i, diags) in range(allDiags)) {
        result[sourceFiles[i]] = filterAndSortDiagnostics(diags);
    }
    return result;
}

public static slice<ж<ast.Diagnostic>> GetSuggestionDiagnostics(this ж<Program> Ꮡp, context.Context ctx, ж<ast.SourceFile> sourceFile) {
    return Ꮡp.collectCheckerDiagnostics(ctx, sourceFile, Ꮡp.getSuggestionDiagnosticsWithChecker);
}

public static slice<ж<ast.Diagnostic>> GetProgramDiagnostics(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return SortAndDeduplicateDiagnostics(slices.Concat<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(
        p.programDiagnostics,
        p.contentMapperDiagnostics,
        p.contentMapperOptionDiagnostics,
        p.includeProcessor.getDiagnostics(Ꮡp).GetGlobalDiagnostics()));
}

internal static void collectContentMapperOptionDiagnostics(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var project = p.ContentMapperProject();
    if (project == nil) {
        return;
    }
    var optionDiagnostics = project.Diagnostics();
    p.contentMapperOptionDiagnostics = core.Map(optionDiagnostics, ж<ast.Diagnostic> (contentmapper.OptionDiagnostic diagnostic) => {
        var (@file, loc) = tsoptions.GetContentMapperOptionDiagnosticLocation(Ꮡp.Value.opts.Config, diagnostic.Mapper, diagnostic.Path);
        return ast.NewExternalDiagnostic(@file, loc, diagnostic.Source, diagnostics.CategoryError, diagnostic.Code, diagnostic.MessageText);
    });
}

public static slice<ж<ast.Diagnostic>> GetIncludeProcessorDiagnostics(this ж<Program> Ꮡp, ж<ast.SourceFile> sourceFile) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.SkipTypeChecking(sourceFile, false)) {
        return default;
    }
    var (filtered, _) = p.getDiagnosticsWithPrecedingDirectives(sourceFile, p.includeProcessor.getDiagnostics(Ꮡp).GetDiagnosticsForFile(sourceFile));
    return filtered;
}

public static bool SkipTypeChecking(this ref Program p, ж<ast.SourceFile> ᏑsourceFile, bool ignoreNoCheck) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    return (!ignoreNoCheck && p.Options().NoCheck.IsTrue()) || p.Options().SkipLibCheck.IsTrue() && sourceFile.IsDeclarationFile || p.Options().SkipDefaultLibCheck.IsTrue() && p.IsSourceFileDefaultLibrary(sourceFile.Path()) || p.IsSourceFromProjectReference(sourceFile.Path()) || !p.canIncludeBindAndCheckDiagnostics(ᏑsourceFile);
}

internal static bool canIncludeBindAndCheckDiagnostics(this ref Program p, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (sourceFile.CheckJsDirective != nil && !sourceFile.CheckJsDirective.Enabled) {
        return false;
    }

    if (sourceFile.ScriptKind == core.ScriptKindTS || sourceFile.ScriptKind == core.ScriptKindTSX) {
        return true;
    }

    var isJS = sourceFile.ScriptKind == core.ScriptKindJS || sourceFile.ScriptKind == core.ScriptKindJSX;
    var isCheckJS = isJS && ast.IsCheckJSEnabledForFile(ᏑsourceFile, p.Options());
    var isPlainJS = ast.IsPlainJSFile(ᏑsourceFile, p.Options().CheckJs);

    return isPlainJS || isCheckJS;
}

internal static slice<ж<ast.SourceFile>> getSourceFilesToEmit(this ж<Program> Ꮡp, slice<ж<ast.SourceFile>> targetSourceFiles, bool forceDtsEmit, bool forceJsEmit) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (targetSourceFiles == nil && !forceDtsEmit && !forceJsEmit) {
        Ꮡp.ᏑsourceFilesToEmitOnce.Do(() => {
            Ꮡp.Value.sourceFilesToEmit = compiler.getSourceFilesToEmit(new ProgramжSourceFileMayBeEmittedHost(Ꮡp), default, false, false);
        });
        return p.sourceFilesToEmit;
    }
    return compiler.getSourceFilesToEmit(new ProgramжSourceFileMayBeEmittedHost(Ꮡp), targetSourceFiles, forceDtsEmit, forceJsEmit);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly object compositeˢ = (@string)"composite"u8;
internal static readonly object declarationMapˢ = (@string)"declarationMap"u8;
internal static readonly @string verbatimModuleSyntaxˢ = "verbatimModuleSyntax"u8;
internal static readonly @string isolatedModulesˢ = "isolatedModules"u8;
internal static readonly object bundlerˢ = (@string)"bundler"u8;

internal static void verifyCompilerOptions(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();
    var emittedFilesᴷ = new stackAppend<@string>();

    var options = p.Options();

    var sourceFile = core.Memoize(ж<ast.SourceFile> () => {
        var configFile = Ꮡp.Value.opts.Config.ConfigFile;
        if (configFile == nil) {
            return default;
        }
        return configFile.SourceFile;
    });

    var sourceFileʗ1 = sourceFile;
    var configFilePath = core.Memoize(@string () => {
        var @file = sourceFileʗ1();
        if (@file != nil) {
            return @file.FileName();
        }
        return "";
    });

    var sourceFileʗ2 = sourceFile;
    var getCompilerOptionsPropertySyntax = core.Memoize(ж<ast.PropertyAssignment> () => tsoptions.ForEachTsConfigPropArray<ast.PropertyAssignment>(sourceFileʗ2(), "compilerOptions", core.Identity<ж<ast.PropertyAssignment>>));

    var getCompilerOptionsPropertySyntaxʗ1 = getCompilerOptionsPropertySyntax;
    var getCompilerOptionsObjectLiteralSyntax = core.Memoize(ж<ast.ObjectLiteralExpression> () => {
        var compilerOptionsProperty = getCompilerOptionsPropertySyntaxʗ1();
        if (compilerOptionsProperty != nil && (~compilerOptionsProperty).Initializer != nil && ast.IsObjectLiteralExpression((~compilerOptionsProperty).Initializer)) {
            return (~compilerOptionsProperty).Initializer.AsObjectLiteralExpression();
        }
        return default;
    });

    var sourceFileʗ3 = sourceFile;
    ж<ast.Diagnostic> createOptionDiagnosticInObjectLiteralSyntax(ж<ast.ObjectLiteralExpression> objectLiteral, bool onKey, @string key1, @string key2, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.slice();
        var argsʗ1 = args;
        var sourceFileʗ4 = sourceFileʗ3;
        var diag = tsoptions.ForEachPropertyAssignment(objectLiteral, key1, ж<ast.Diagnostic> (ж<ast.PropertyAssignment> property) => tsoptions.CreateDiagnosticForNodeInSourceFile(sourceFileʗ4(), core.IfElse(onKey, property.Name(), (~property).Initializer), message, argsʗ1.ꓸꓸꓸ), key2);
        if (diag != nil) {
            Ꮡp.Value.programDiagnostics = append(Ꮡp.Value.programDiagnostics, diag);
        }
        return diag;
    }

    var getCompilerOptionsPropertySyntaxʗ2 = getCompilerOptionsPropertySyntax;
    var sourceFileʗ5 = sourceFile;
    ж<ast.Diagnostic> createCompilerOptionsDiagnostic(ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.sslice();
        var compilerOptionsProperty = getCompilerOptionsPropertySyntaxʗ2();
        ж<ast.Diagnostic> diag = default;
        if (compilerOptionsProperty != nil) {
            diag = tsoptions.CreateDiagnosticForNodeInSourceFile(sourceFileʗ5(), compilerOptionsProperty.Name(), message, args.ꓸꓸꓸ);
        } else {
            diag = ast.NewCompilerDiagnostic(message, args.ꓸꓸꓸ);
        }
        Ꮡp.Value.programDiagnostics = append(Ꮡp.Value.programDiagnostics, diag);
        return diag;
    }

    var createCompilerOptionsDiagnosticʗ1 = createCompilerOptionsDiagnostic;
    var createOptionDiagnosticInObjectLiteralSyntaxʗ1 = createOptionDiagnosticInObjectLiteralSyntax;
    var getCompilerOptionsObjectLiteralSyntaxʗ1 = getCompilerOptionsObjectLiteralSyntax;
    ж<ast.Diagnostic> createDiagnosticForOption(bool onKey, @string option1, @string option2, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.sslice();
        var diag = createOptionDiagnosticInObjectLiteralSyntaxʗ1(getCompilerOptionsObjectLiteralSyntaxʗ1(), onKey, option1, option2, message, args.ꓸꓸꓸ);
        if (diag == nil) {
            diag = createCompilerOptionsDiagnosticʗ1(message, args.ꓸꓸꓸ);
        }
        return diag;
    }

    var createDiagnosticForOptionʗ1 = createDiagnosticForOption;
    void createDiagnosticForOptionName(ж<diagnostics.Message> message, @string option1, @string option2, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.sslice();
        var newArgs = new slice<any>(0, len(args) + 2);
        newArgs = append(newArgs, (any)(option1), (any)(option2));
        newArgs = appendꓸꓸꓸ(newArgs, args);
        createDiagnosticForOptionʗ1(true, option1, option2, message, newArgs.ꓸꓸꓸ);
    }

    var createDiagnosticForOptionʗ2 = createDiagnosticForOption;
    void createOptionValueDiagnostic(@string option1, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.sslice();
        createDiagnosticForOptionʗ2(false, option1, ""u8, message, args.ꓸꓸꓸ);
    }

    var createDiagnosticForOptionʗ3 = createDiagnosticForOption;
    void createRemovedOptionDiagnostic(@string name, @string value, @string useInstead) {
        ж<diagnostics.Message> message = default;
        slice<any> args = default;
        if (value == ""u8) {
            message = diagnostics.Option_0_has_been_removed_Please_remove_it_from_your_configuration;
            args = [name];
        } else {
            message = diagnostics.Option_0_1_has_been_removed_Please_remove_it_from_your_configuration;
            args = [name, value];
        }

        var diag = createDiagnosticForOptionʗ3(value == ""u8, name, ""u8, message, args.ꓸꓸꓸ);
        if (useInstead != ""u8) {
            diag.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.Use_0_instead, useInstead));
        }
    }

    if (options.BaseUrl != ""u8) {
        @string useInstead = default;
        if (configFilePath() != ""u8) {
            @string relative = tspath.GetRelativePathFromFile(configFilePath(), options.BaseUrl, p.comparePathsOptions);
            if (!(strings.HasPrefix(relative, "./") || strings.HasPrefix(relative, "../"))) {
                relative = "./"u8 + relative;
            }
            @string suggestion = tspath.CombinePaths(relative, "*");
            var (ᴛ1, ᴛ2) = json.Marshal(suggestion);
            useInstead = fmt.Sprintf(@"""paths"": {""*"": [%s]}"u8, core.Must(ᴛ1, ᴛ2));
        }
        createRemovedOptionDiagnostic("baseUrl", "", useInstead);
    }

    if (options.OutFile != ""u8) {
        createRemovedOptionDiagnostic("outFile", "", "");
    }

    if (options.Target == core.ScriptTargetES5) {
        createRemovedOptionDiagnostic("target", "ES5", "");
    }

    if (options.Module == core.ModuleKindAMD) {
        createRemovedOptionDiagnostic("module", "AMD", "");
    }
    if (options.Module == core.ModuleKindSystem) {
        createRemovedOptionDiagnostic("module", "System", "");
    }
    if (options.Module == core.ModuleKindUMD) {
        createRemovedOptionDiagnostic("module", "UMD", "");
    }

    if (options.ModuleResolution == core.ModuleResolutionKindClassic) {
        createRemovedOptionDiagnostic("moduleResolution", "Classic", "");
    }

    if (options.AlwaysStrict.IsFalse()) {
        createRemovedOptionDiagnostic("alwaysStrict", "false", "");
    }

    if (options.ESModuleInterop.IsFalse()) {
        createRemovedOptionDiagnostic("esModuleInterop", "false", "");
    }

    if (options.AllowSyntheticDefaultImports.IsFalse()) {
        createRemovedOptionDiagnostic("allowSyntheticDefaultImports", "false", "");
    }

    if (options.ModuleResolution == core.ModuleResolutionKindNode10) {
        createRemovedOptionDiagnostic("moduleResolution", "node10", "");
    }

    if (!options.DownlevelIteration.IsUnknown()) {
        createRemovedOptionDiagnostic("downlevelIteration", "", "");
    }

    if (options.StrictPropertyInitialization.IsTrue() && !options.GetStrictOptionValue(options.StrictNullChecks)) {
        createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1, "strictPropertyInitialization", "strictNullChecks");
    }
    if (options.ExactOptionalPropertyTypes.IsTrue() && !options.GetStrictOptionValue(options.StrictNullChecks)) {
        createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1, "exactOptionalPropertyTypes", "strictNullChecks");
    }

    if (options.IsolatedDeclarations.IsTrue()) {
        if (options.GetAllowJS()) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_with_option_1, "allowJs", "isolatedDeclarations");
        }
        if (!options.GetEmitDeclarations()) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1_or_option_2, "isolatedDeclarations", "declaration", compositeˢ);
        }
    }

    if (options.InlineSourceMap.IsTrue()) {
        if (options.SourceMap.IsTrue()) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_with_option_1, "sourceMap", "inlineSourceMap");
        }
        if (options.MapRoot != ""u8) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_with_option_1, "mapRoot", "inlineSourceMap");
        }
    }

    if (options.Composite.IsTrue()) {
        if (options.Declaration.IsFalse()) {
            createDiagnosticForOptionName(diagnostics.Composite_projects_may_not_disable_declaration_emit, "declaration", "");
        }
        if (options.Incremental.IsFalse()) {
            createDiagnosticForOptionName(diagnostics.Composite_projects_may_not_disable_incremental_compilation, "declaration", "");
        }
    }

    if (options.TsBuildInfoFile == ""u8 && options.Incremental.IsTrue() && options.ConfigFilePath == ""u8) {
        createCompilerOptionsDiagnostic(diagnostics.Option_incremental_is_only_valid_with_a_known_configuration_file_like_tsconfig_json_or_when_tsBuildInfoFile_is_explicitly_provided);
    }

    Ꮡp.verifyProjectReferences();

    if (options.Composite.IsTrue()) {
        ref var rootPaths = ref heap(new collections.Set<tspath.Path>(), out var ᏑrootPaths);
        foreach (var (_, fileName) in range(p.opts.Config.FileNames())) {
            rootPaths.Add(p.toPath(fileName));
        }

        foreach (var (_, @file) in range(p.files)) {
            if (sourceFileMayBeEmitted(@file, new ProgramжSourceFileMayBeEmittedHost(Ꮡp), false, false) && !ᏑrootPaths.Has(@file.Path())) {
                p.includeProcessor.addProcessingDiagnostic(Ꮡ(new processingDiagnostic(
                    kind: processingDiagnosticKindExplainingFileInclude,
                    data: Ꮡ(new includeExplainingDiagnostic(
                        @file: @file.Path(),
                        message: diagnostics.File_0_is_not_listed_within_the_file_list_of_project_1_Projects_must_list_all_files_or_use_an_include_pattern,
                        args: slice<any>([@file.FileName(), configFilePath()])
                    ))
                )));
            }
        }
    }

    var getCompilerOptionsObjectLiteralSyntaxʗ2 = getCompilerOptionsObjectLiteralSyntax;
    ж<ast.Diagnostic> forEachOptionPathsSyntax(Func<ж<ast.PropertyAssignment>, ж<ast.Diagnostic>> callback) => tsoptions.ForEachPropertyAssignment(getCompilerOptionsObjectLiteralSyntaxʗ2(), "paths", callback);

    var createCompilerOptionsDiagnosticʗ2 = createCompilerOptionsDiagnostic;
    var createOptionDiagnosticInObjectLiteralSyntaxʗ2 = createOptionDiagnosticInObjectLiteralSyntax;
    var forEachOptionPathsSyntaxʗ1 = forEachOptionPathsSyntax;
    ж<ast.Diagnostic> createDiagnosticForOptionPaths(bool onKey, @string key, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.slice();
        var argsʗ2 = args;
        var createOptionDiagnosticInObjectLiteralSyntaxʗ3 = createOptionDiagnosticInObjectLiteralSyntaxʗ2;
        var diag = forEachOptionPathsSyntaxʗ1((ж<ast.PropertyAssignment> pathProp) => {
            if (ast.IsObjectLiteralExpression((~pathProp).Initializer)) {
                return createOptionDiagnosticInObjectLiteralSyntaxʗ3((~pathProp).Initializer.AsObjectLiteralExpression(), onKey, key, ""u8, message, argsʗ2.ꓸꓸꓸ);
            }
            return default;
        });
        if (diag == nil) {
            diag = createCompilerOptionsDiagnosticʗ2(message, args.ꓸꓸꓸ);
        }
        return diag;
    }

    var createCompilerOptionsDiagnosticʗ3 = createCompilerOptionsDiagnostic;
    var forEachOptionPathsSyntaxʗ2 = forEachOptionPathsSyntax;
    var sourceFileʗ6 = sourceFile;
    ж<ast.Diagnostic> createDiagnosticForOptionPathKeyValue(@string key, nint valueIndex, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.slice();
        var argsʗ3 = args;
        var sourceFileʗ7 = sourceFileʗ6;
        var diag = forEachOptionPathsSyntaxʗ2((ж<ast.PropertyAssignment> pathProp) => {
            if (ast.IsObjectLiteralExpression((~pathProp).Initializer)) {
                var argsʗ4 = argsʗ3;
                var sourceFileʗ8 = sourceFileʗ7;
                return tsoptions.ForEachPropertyAssignment((~pathProp).Initializer.AsObjectLiteralExpression(), key, ж<ast.Diagnostic> (ж<ast.PropertyAssignment> keyProps) => {
                    var initializer = keyProps.Value.Initializer;
                    if (ast.IsArrayLiteralExpression(initializer)) {
                        var elements = initializer.ElementList();
                        if (elements != nil && len(elements.Nodes) > valueIndex) {
                            var diagΔ1 = tsoptions.CreateDiagnosticForNodeInSourceFile(sourceFileʗ8(), elements.Nodes[valueIndex], message, argsʗ4.ꓸꓸꓸ);
                            Ꮡp.Value.programDiagnostics = append(Ꮡp.Value.programDiagnostics, diagΔ1);
                            return diagΔ1;
                        }
                    }
                    return default;
                });
            }
            return default;
        });
        if (diag == nil) {
            diag = createCompilerOptionsDiagnosticʗ3(message, args.ꓸꓸꓸ);
        }
        return diag;
    }

    foreach (var (key, value) in range<@string, slice<@string>>(options.Paths.Entries().Invoke)) {
        if (!hasZeroOrOneAsteriskCharacter(key)) {
            createDiagnosticForOptionPaths(true, key, diagnostics.Pattern_0_can_have_at_most_one_Asterisk_character, key);
        }
        if (value == nil) {
            createDiagnosticForOptionPaths(false, key, diagnostics.Substitutions_for_pattern_0_should_be_an_array, key);
        } else if (len(value) == 0) {
            createDiagnosticForOptionPaths(false, key, diagnostics.Substitutions_for_pattern_0_shouldn_t_be_an_empty_array, key);
        }
        foreach (var (i, subst) in range(value)) {
            if (!hasZeroOrOneAsteriskCharacter(subst)) {
                createDiagnosticForOptionPathKeyValue(key, i, diagnostics.Substitution_0_in_pattern_1_can_have_at_most_one_Asterisk_character, subst, key);
            }
            if (!tspath.PathIsRelative(subst) && !tspath.PathIsAbsolute(subst)) {
                createDiagnosticForOptionPathKeyValue(key, i, diagnostics.Non_relative_paths_are_not_allowed_Did_you_forget_a_leading_Slash);
            }
        }
    }

    if (options.SourceMap.IsFalseOrUnknown() && options.InlineSourceMap.IsFalseOrUnknown()) {
        if (options.InlineSources.IsTrue()) {
            createDiagnosticForOptionName(diagnostics.Option_0_can_only_be_used_when_either_option_inlineSourceMap_or_option_sourceMap_is_provided, "inlineSources", "");
        }
        if (options.SourceRoot != ""u8) {
            createDiagnosticForOptionName(diagnostics.Option_0_can_only_be_used_when_either_option_inlineSourceMap_or_option_sourceMap_is_provided, "sourceRoot", "");
        }
    }

    if (options.MapRoot != ""u8 && !(options.SourceMap.IsTrue() || options.DeclarationMap.IsTrue())) {
        createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1_or_option_2, "mapRoot", "sourceMap", declarationMapˢ);
    }

    if (options.DeclarationDir != ""u8) {
        if (!options.GetEmitDeclarations()) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1_or_option_2, "declarationDir", "declaration", compositeˢ);
        }
    }

    if (options.DeclarationMap.IsTrue() && !options.GetEmitDeclarations()) {
        createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1_or_option_2, "declarationMap", "declaration", compositeˢ);
    }

    if (options.Lib != nil && options.NoLib.IsTrue()) {
        createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_with_option_1, "lib", "noLib");
    }

    if (options.IsolatedModules.IsTrue() || options.VerbatimModuleSyntax.IsTrue()) {
        if (options.PreserveConstEnums.IsFalse()) {
            createDiagnosticForOptionName(diagnostics.Option_preserveConstEnums_cannot_be_disabled_when_0_is_enabled, core.IfElse(options.VerbatimModuleSyntax.IsTrue(), verbatimModuleSyntaxˢ, isolatedModulesˢ), "preserveConstEnums");
        }
    }

    if (options.OutDir != ""u8 || options.RootDir != ""u8 || options.SourceRoot != ""u8 || options.MapRoot != ""u8 || (options.GetEmitDeclarations() && options.DeclarationDir != ""u8)) {
        @string dir = Ꮡp.CommonSourceDirectory();
        if (options.OutDir != ""u8 && dir == ""u8 && core.Some(p.files, (ж<ast.SourceFile> f) => tspath.GetRootLength(f.FileName()) > 1)) {
            createDiagnosticForOptionName(diagnostics.Cannot_find_the_common_subdirectory_path_for_the_input_files, "outDir", "");
        }
    }

    if (!options.NoEmit.IsTrue() && !options.Composite.IsTrue() && options.RootDir == ""u8 && options.ConfigFilePath != ""u8 && (options.OutDir != ""u8 || (options.GetEmitDeclarations() && options.DeclarationDir != ""u8) || options.OutFile != ""u8)) {
        @string dir = Ꮡp.CommonSourceDirectory();
        slice<@string> emittedFiles = default;
        foreach (var (_, @file) in range(p.files)) {
            if (!@file.IsDeclarationFile && sourceFileMayBeEmitted(@file, new ProgramжSourceFileMayBeEmittedHost(Ꮡp), false, false)) {
                emittedFiles = append(emittedFilesᴷ, emittedFiles, @file.FileName());
            }
        }
        @string dir59 = outputpaths.GetComputedCommonSourceDirectory(emittedFiles, p.GetCurrentDirectory(), p.UseCaseSensitiveFileNames());
        if (dir59 != ""u8 && tspath.GetCanonicalFileName(dir, p.UseCaseSensitiveFileNames()) != tspath.GetCanonicalFileName(dir59, p.UseCaseSensitiveFileNames())) {
            @string option1 = default;
            if (options.OutFile != ""u8) {
                option1 = "outFile";
            } else if (options.OutDir != ""u8) {
                option1 = "outDir";
            } else {
                option1 = "declarationDir";
            }
            @string option2 = default;
            if (options.OutFile == ""u8 && options.OutDir != ""u8) {
                option2 = "declarationDir";
            }
            var diag = createDiagnosticForOption(
                true,
                option1,
                option2,
                diagnostics.The_common_source_directory_of_0_is_1_The_rootDir_setting_must_be_explicitly_set_to_this_or_another_path_to_adjust_your_output_s_file_layout,
                tspath.GetBaseFileName(options.ConfigFilePath),
                tspath.GetRelativePathFromFile(options.ConfigFilePath, dir59, p.comparePathsOptions));
            diag.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.Visit_https_Colon_Slash_Slashaka_ms_Slashts6_for_migration_information));
        }
    }

    if (options.CheckJs.IsTrue() && !options.GetAllowJS()) {
        createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1, "checkJs", "allowJs");
    }

    if (options.EmitDeclarationOnly.IsTrue()) {
        if (!options.GetEmitDeclarations()) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1_or_option_2, "emitDeclarationOnly", "declaration", compositeˢ);
        }
    }

    if (options.EmitDecoratorMetadata.IsTrue() && options.ExperimentalDecorators.IsFalseOrUnknown()) {
        createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1, "emitDecoratorMetadata", "experimentalDecorators");
    }

    if (options.JsxFactory != ""u8) {
        if (options.ReactNamespace != ""u8) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_with_option_1, "reactNamespace", "jsxFactory");
        }
        if (options.Jsx == core.JsxEmitReactJSX || options.Jsx == core.JsxEmitReactJSXDev) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_when_option_jsx_is_1, "jsxFactory", options.Jsx.String());
        }
        if (parser.ParseIsolatedEntityName(options.JsxFactory) == nil) {
            createOptionValueDiagnostic("jsxFactory", diagnostics.Invalid_value_for_jsxFactory_0_is_not_a_valid_identifier_or_qualified_name, options.JsxFactory);
        }
    } else if (options.ReactNamespace != ""u8 && !scanner.IsIdentifierText(options.ReactNamespace, core.LanguageVariantStandard)) {
        createOptionValueDiagnostic("reactNamespace", diagnostics.Invalid_value_for_reactNamespace_0_is_not_a_valid_identifier, options.ReactNamespace);
    }

    if (options.JsxFragmentFactory != ""u8) {
        if (options.JsxFactory == ""u8) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_without_specifying_option_1, "jsxFragmentFactory", "jsxFactory");
        }
        if (options.Jsx == core.JsxEmitReactJSX || options.Jsx == core.JsxEmitReactJSXDev) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_when_option_jsx_is_1, "jsxFragmentFactory", options.Jsx.String());
        }
        if (parser.ParseIsolatedEntityName(options.JsxFragmentFactory) == nil) {
            createOptionValueDiagnostic("jsxFragmentFactory", diagnostics.Invalid_value_for_jsxFragmentFactory_0_is_not_a_valid_identifier_or_qualified_name, options.JsxFragmentFactory);
        }
    }

    if (options.ReactNamespace != ""u8) {
        if (options.Jsx == core.JsxEmitReactJSX || options.Jsx == core.JsxEmitReactJSXDev) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_when_option_jsx_is_1, "reactNamespace", options.Jsx.String());
        }
    }

    if (options.JsxImportSource != ""u8) {
        if (options.Jsx == core.JsxEmitReact) {
            createDiagnosticForOptionName(diagnostics.Option_0_cannot_be_specified_when_option_jsx_is_1, "jsxImportSource", options.Jsx.String());
        }
    }

    var moduleKind = options.GetEmitModuleKind();

    if (options.AllowImportingTsExtensions.IsTrue() && !(options.NoEmit.IsTrue() || options.EmitDeclarationOnly.IsTrue() || options.RewriteRelativeImportExtensions.IsTrue())) {
        createOptionValueDiagnostic("allowImportingTsExtensions", diagnostics.Option_allowImportingTsExtensions_can_only_be_used_when_one_of_noEmit_emitDeclarationOnly_or_rewriteRelativeImportExtensions_is_set);
    }

    var moduleResolution = options.GetModuleResolutionKind();
    if (options.ResolvePackageJsonExports.IsTrue() && !moduleResolutionSupportsPackageJsonExportsAndImports(moduleResolution)) {
        createDiagnosticForOptionName(diagnostics.Option_0_can_only_be_used_when_moduleResolution_is_set_to_node16_nodenext_or_bundler, "resolvePackageJsonExports", "");
    }
    if (options.ResolvePackageJsonImports.IsTrue() && !moduleResolutionSupportsPackageJsonExportsAndImports(moduleResolution)) {
        createDiagnosticForOptionName(diagnostics.Option_0_can_only_be_used_when_moduleResolution_is_set_to_node16_nodenext_or_bundler, "resolvePackageJsonImports", "");
    }
    if (options.CustomConditions != nil && !moduleResolutionSupportsPackageJsonExportsAndImports(moduleResolution)) {
        createDiagnosticForOptionName(diagnostics.Option_0_can_only_be_used_when_moduleResolution_is_set_to_node16_nodenext_or_bundler, "customConditions", "");
    }

    if (moduleResolution == core.ModuleResolutionKindBundler && !emitModuleKindIsNonNodeESM(moduleKind) && moduleKind != core.ModuleKindPreserve && moduleKind != core.ModuleKindCommonJS) {
        createOptionValueDiagnostic("moduleResolution", diagnostics.Option_0_can_only_be_used_when_module_is_set_to_preserve_commonjs_or_es2015_or_later, bundlerˢ);
    }

    if (core.ModuleKindNode16 <= moduleKind && moduleKind <= core.ModuleKindNodeNext && !(core.ModuleResolutionKindNode16 <= moduleResolution && moduleResolution <= core.ModuleResolutionKindNodeNext)) {
        @string moduleKindName = moduleKind.String();
        @string moduleResolutionName = default;
        var (v, ok) = core.ModuleKindToModuleResolutionKind[moduleKind, ꟷ]; if (ok) {
            moduleResolutionName = v.String();
        } else {
            moduleResolutionName = "Node16";
        }
        createOptionValueDiagnostic("moduleResolution", diagnostics.Option_moduleResolution_must_be_set_to_0_or_left_unspecified_when_option_module_is_set_to_1, moduleResolutionName, moduleKindName);
    } else if (core.ModuleResolutionKindNode16 <= moduleResolution && moduleResolution <= core.ModuleResolutionKindNodeNext && !(core.ModuleKindNode16 <= moduleKind && moduleKind <= core.ModuleKindNodeNext)) {
        @string moduleResolutionName = moduleResolution.String();
        createOptionValueDiagnostic("module", diagnostics.Option_module_must_be_set_to_0_when_option_moduleResolution_is_set_to_1, moduleResolutionName, moduleResolutionName);
    }

    if (!options.NoEmit.IsTrue() && !options.SuppressOutputPathCheck.IsTrue()) {
        ref var emitFilesSeen = ref heap(new collections.Set<@string>(), out var ᏑemitFilesSeen);

        var configFilePathʗ1 = configFilePath;
        void verifyEmitFilePath(@string emitFileName) {
            if (emitFileName != ""u8) {
                tspath.Path emitFilePath = Ꮡp.Value.toPath(emitFileName);
                var (_, ok) = Ꮡp.Value.filesByPath[emitFilePath, ꟷ]; if (ok) {
                    var diag = ast.NewCompilerDiagnostic(diagnostics.Cannot_write_file_0_because_it_would_overwrite_input_file, emitFileName);
                    if (configFilePathʗ1() == ""u8) {
                        diag.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.Adding_a_tsconfig_json_file_will_help_organize_projects_that_contain_both_TypeScript_and_JavaScript_files_Learn_more_at_https_Colon_Slash_Slashaka_ms_Slashtsconfig));
                    }
                    Ꮡp.Value.blockEmittingOfFile(emitFileName, diag);
                }

                @string emitFileKey = default;
                if (!Ꮡp.Value.Host().FS().UseCaseSensitiveFileNames()) {
                    emitFileKey = tspath.ToFileNameLowerCase((@string)emitFilePath);
                } else {
                    emitFileKey = (@string)emitFilePath;
                }

                if (ᏑemitFilesSeen.Has(emitFileKey)) {
                    Ꮡp.Value.blockEmittingOfFile(emitFileName, ast.NewCompilerDiagnostic(diagnostics.Cannot_write_file_0_because_it_would_be_overwritten_by_multiple_input_files, emitFileName));
                } else {
                    ᏑemitFilesSeen.Value.Add(emitFileKey);
                }
            }
        }

        var verifyEmitFilePathʗ1 = verifyEmitFilePath;
        outputpaths.ForEachEmittedFile(new ProgramжOutputPathsHost(Ꮡp), options, (ж<outputpaths.OutputPaths> emitFileNames, ж<ast.SourceFile> sourceFileΔ1) => {
            verifyEmitFilePathʗ1(emitFileNames.JsFilePath());
            verifyEmitFilePathʗ1(emitFileNames.SourceMapFilePath());
            verifyEmitFilePathʗ1(emitFileNames.DeclarationFilePath());
            verifyEmitFilePathʗ1(emitFileNames.DeclarationMapPath());
            return false;
        }, Ꮡp.getSourceFilesToEmit(default, false, false), false);
        verifyEmitFilePath(p.opts.Config.GetBuildInfoFileName());
    }
}

internal static void blockEmittingOfFile(this ref Program p, @string emitFileName, ж<ast.Diagnostic> diag) {
    p.hasEmitBlockingDiagnostics.Add(p.toPath(emitFileName));
    p.programDiagnostics = append(p.programDiagnostics, diag);
}

public static bool IsEmitBlocked(this ж<Program> Ꮡp, @string emitFileName) {
    ref var p = ref Ꮡp.DerefOrNull();

    return Ꮡp.ᏑhasEmitBlockingDiagnostics.Has(p.toPath(emitFileName));
}

internal static void verifyProjectReferences(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    @string buildInfoFileName = core.IfElse(!p.Options().SuppressOutputPathCheck.IsTrue(), p.opts.Config.GetBuildInfoFileName(), (@string)"");
    void createDiagnosticForReference(ж<tsoptions.ParsedCommandLine> config, nint index, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
        var args = argsʗp.sslice();
        var diag = tsoptions.CreateDiagnosticAtReferenceSyntax(config, index, message, args.ꓸꓸꓸ);
        if (diag == nil) {
            diag = ast.NewCompilerDiagnostic(message, args.ꓸꓸꓸ);
        }
        Ꮡp.Value.programDiagnostics = append(Ꮡp.Value.programDiagnostics, diag);
    }

    var createDiagnosticForReferenceʗ1 = createDiagnosticForReference;
    p.RangeResolvedProjectReference((tspath.Path path, ж<tsoptions.ParsedCommandLine> config, ж<tsoptions.ParsedCommandLine> parent, nint index) => {
        var @ref = parent.ProjectReferences()[index];
        if (config == nil) {
            createDiagnosticForReferenceʗ1(parent, index, diagnostics.File_0_not_found, @ref.Path);
            return true;
        }
        var refOptions = config.CompilerOptions();
        if (!refOptions.Composite.IsTrue() || refOptions.NoEmit.IsTrue()) {
            if (len(parent.FileNames()) > 0) {
                if (!refOptions.Composite.IsTrue()) {
                    createDiagnosticForReferenceʗ1(parent, index, diagnostics.Referenced_project_0_must_have_setting_composite_Colon_true, @ref.Path);
                }
                if (refOptions.NoEmit.IsTrue()) {
                    createDiagnosticForReferenceʗ1(parent, index, diagnostics.Referenced_project_0_may_not_disable_emit, @ref.Path);
                }
            }
        }
        if (buildInfoFileName != ""u8 && buildInfoFileName == config.GetBuildInfoFileName()) {
            createDiagnosticForReferenceʗ1(parent, index, diagnostics.Cannot_write_file_0_because_it_will_overwrite_tsbuildinfo_file_generated_by_referenced_project_1, buildInfoFileName, @ref.Path);
            Ꮡp.Value.hasEmitBlockingDiagnostics.Add(Ꮡp.Value.toPath(buildInfoFileName));
        }
        return true;
    });
}

} partial class compiler_package {

internal static bool hasZeroOrOneAsteriskCharacter(@string str) {
    var seenAsterisk = false;
    foreach (var (_, ch) in str) {
        if (ch == '*') {
            if (!seenAsterisk) {
                seenAsterisk = true;
            } else {
                return false;
            }
        }
    }
    return true;
}

internal static bool moduleResolutionSupportsPackageJsonExportsAndImports(core.ModuleResolutionKind moduleResolution) {
    return moduleResolution >= core.ModuleResolutionKindNode16 && moduleResolution <= core.ModuleResolutionKindNodeNext || moduleResolution == core.ModuleResolutionKindBundler;
}

internal static bool emitModuleKindIsNonNodeESM(core.ModuleKind moduleKind) {
    return moduleKind >= core.ModuleKindES2015 && moduleKind <= core.ModuleKindESNext;
}

} partial class compilerᴛProgram {

public static slice<ж<ast.Diagnostic>> GetGlobalDiagnostics(this ref Program p, context.Context ctx) {
    if (len(p.files) == 0) {
        return default;
    }
    if (p.compilerCheckerPool != nil) {
        return p.compilerCheckerPool.GetGlobalDiagnostics();
    }
    return default;
}

public static slice<ж<ast.Diagnostic>> GetDeclarationDiagnostics(this ж<Program> Ꮡp, context.Context ctx, ж<ast.SourceFile> sourceFile) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.collectDiagnostics(ctx, sourceFile, true, Ꮡp.getDeclarationDiagnosticsForFile);
}

} partial class compiler_package {

public static slice<ж<ast.Diagnostic>> FilterNoEmitSemanticDiagnostics(slice<ж<ast.Diagnostic>> diagnostics, ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (!options.NoEmit.IsTrue()) {
        return diagnostics;
    }
    return core.Filter(diagnostics, (ж<ast.Diagnostic> d) => !d.SkippedOnNoEmit());
}

} partial class compilerᴛProgram {

internal static slice<ж<ast.Diagnostic>> getSemanticDiagnosticsWithChecker(this ж<Program> Ꮡp, context.Context ctx, ж<checker.Checker> c, ж<ast.SourceFile> sourceFile) {
    ref var p = ref Ꮡp.DerefOrNull();

    return core.Concatenate(
        FilterNoEmitSemanticDiagnostics(p.getBindAndCheckDiagnosticsWithChecker(ctx, c, sourceFile), p.Options()),
        Ꮡp.GetIncludeProcessorDiagnostics(sourceFile));
}

internal static slice<ж<ast.Diagnostic>> getBindAndCheckDiagnosticsWithChecker(this ref Program p, context.Context ctx, ж<checker.Checker> fileChecker, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    var compilerOptions = p.Options();
    if (p.SkipTypeChecking(ᏑsourceFile, false)) {
        return default;
    }

    var diags = slices.Clip<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(sourceFile.BindDiagnostics());
    diags = appendꓸꓸꓸ(diags, fileChecker.GetDiagnostics(ctx, ᏑsourceFile));

    var isPlainJS = ast.IsPlainJSFile(ᏑsourceFile, compilerOptions.CheckJs);
    if (isPlainJS) {
        return core.Filter(diags, (ж<ast.Diagnostic> d) => plainJSErrors.Has(d.Code()));
    }

    var isJS = sourceFile.ScriptKind == core.ScriptKindJS || sourceFile.ScriptKind == core.ScriptKindJSX;
    var isCheckJS = isJS && ast.IsCheckJSEnabledForFile(ᏑsourceFile, compilerOptions);
    if (isCheckJS) {
        diags = appendꓸꓸꓸ(diags, sourceFile.JSDocDiagnostics());
    }

    var (filtered, directivesByLine) = p.getDiagnosticsWithPrecedingDirectives(ᏑsourceFile, diags);
    foreach (var (_, directive) in directivesByLine) {
        if (directive.Kind == ast.CommentDirectiveKindExpectError) {
            filtered = append(filtered, ast.NewDiagnostic(ᏑsourceFile, directive.Loc, diagnostics.Unused_ts_expect_error_directive));
        }
    }
    filtered = applyContentMapperDiagnosticDirectives(ᏑsourceFile, filtered);
    return filtered;
}

} partial class compiler_package {

internal static slice<ж<ast.Diagnostic>> applyContentMapperDiagnosticDirectives(ж<ast.SourceFile> ᏑsourceFile, slice<ж<ast.Diagnostic>> diags) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    var directives = sourceFile.DiagnosticDirectives();
    if (len(directives) == 0) {
        return diags;
    }
    var used = new slice<bool>(len(directives));
    var directivesʗ1 = directives;
    var usedʗ1 = used;
    bool markUsed(ж<ast.Diagnostic> diag) {
        if (diag.Source() != ""u8) {
            return false;
        }
        foreach (var (i, directive) in range(directivesʗ1)) {
            if (diag.Pos() >= directive.VirtualRange.Pos() && diag.Pos() < directive.VirtualRange.End()) {
                usedʗ1[i] = true;
                return true;
            }
        }
        return false;
    }
    var markUsedʗ1 = markUsed;
    var filtered = core.Filter(diags, (ж<ast.Diagnostic> diag) => !markUsedʗ1(diag));
    foreach (var (i, directive) in range(directives)) {
        if (directive.Policy == ast.MappedDiagnosticDirectivePolicyExpect && !used[i]) {
            filtered = append(filtered, ast.NewExternalDiagnostic(
                ᏑsourceFile,
                directive.OriginalRange,
                directive.Source,
                diagnostics.CategoryError,
                directive.UnusedCode,
                directive.UnusedMessageText));
        }
    }
    return filtered;
}

} partial class compilerᴛProgram {

internal static (slice<ж<ast.Diagnostic>>, map<nint, ast.CommentDirective>) getDiagnosticsWithPrecedingDirectives(this ref Program p, ж<ast.SourceFile> ᏑsourceFile, slice<ж<ast.Diagnostic>> diags) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (len(sourceFile.CommentDirectives) == 0) {
        return (diags, default);
    }
    var directivesByLine = new map<nint, ast.CommentDirective>();
    foreach (var (_, directive) in range(sourceFile.CommentDirectives)) {
        nint line = scanner.GetECMALineOfPosition(new ast.SourceFileжSourceFileLike(ᏑsourceFile), directive.Loc.Pos());
        directivesByLine[line] = directive;
    }
    var lineStarts = scanner.GetECMALineStarts(new ast.SourceFileжSourceFileLike(ᏑsourceFile));
    var filtered = new slice<ж<ast.Diagnostic>>(0, len(diags));
    foreach (var (_, diagnostic) in range(diags)) {
        var ignoreDiagnostic = false;
        for (nint line = scanner.ComputeLineOfPosition(lineStarts, diagnostic.Pos()) - 1; line >= 0; line--) {
            var (directive, ok) = directivesByLine[line, ꟷ]; if (ok) {
                ignoreDiagnostic = true;
                directive.Kind = ast.CommentDirectiveKindIgnore;
                directivesByLine[line] = directive;
                break;
            }
            if (!isCommentOrBlankLine(sourceFile.Text(), (nint)lineStarts[line])) {
                break;
            }
        }
        if (!ignoreDiagnostic) {
            filtered = append(filtered, diagnostic);
        }
    }
    return (filtered, directivesByLine);
}

internal static slice<ж<ast.Diagnostic>> getDeclarationDiagnosticsForFile(this ж<Program> Ꮡp, context.Context ctx, ж<ast.SourceFile> ᏑsourceFile) {
    GoFrame ᒐ = default;
    try {
        ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

        if (sourceFile.IsDeclarationFile) {
            return [];
        }

        var (cached, ok) = Ꮡp.ᏑdeclarationDiagnosticCache.Load(ᏑsourceFile); if (ok) {
            return cached;
        }

        var (host, done) = newEmitHost(ctx, Ꮡp, ᏑsourceFile);
        var doneʗ1 = done;
        defer(doneʗ1, ref ᒐ);
        var diagnostics = getDeclarationDiagnostics(new emitHostжEmitHost(host), ᏑsourceFile);
        (diagnostics, _) = Ꮡp.ᏑdeclarationDiagnosticCache.LoadOrStore(ᏑsourceFile, diagnostics);
        return diagnostics;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static slice<ж<ast.Diagnostic>> getSuggestionDiagnosticsWithChecker(this ref Program p, context.Context ctx, ж<checker.Checker> fileChecker, ж<ast.SourceFile> sourceFile) {
    if (p.SkipTypeChecking(sourceFile, false)) {
        return default;
    }

    return fileChecker.GetSuggestionDiagnostics(ctx, sourceFile);
}

} partial class compiler_package {

internal static bool isCommentOrBlankLine(@string text, nint pos) {
    while (pos < len(text) && (text[pos] == ' ' || text[pos] == '\t')) {
        pos++;
    }
    return pos == len(text) || pos < len(text) && (text[pos] == '\r' || text[pos] == '\n') || pos + 1 < len(text) && text[pos] == '/' && text[pos + 1] == '/';
}

public static slice<ж<ast.Diagnostic>> SortAndDeduplicateDiagnostics(slice<ж<ast.Diagnostic>> diagnostics) {
    diagnostics = slices.Clone<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(diagnostics);
    slices.SortFunc<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(diagnostics, ast.CompareDiagnostics);
    return compactAndMergeRelatedInfos(diagnostics);
}

internal static slice<ж<ast.Diagnostic>> compactAndMergeRelatedInfos(slice<ж<ast.Diagnostic>> diagnostics) {
    if (len(diagnostics) < 2) {
        return diagnostics;
    }
    nint i = 0;
    nint j = 0;
    while (i < len(diagnostics)) {
        var d = diagnostics[i];
        nint n = 1;
        while (i + n < len(diagnostics) && ast.EqualDiagnosticsNoRelatedInfo(d, diagnostics[i + n])) {
            n++;
        }
        if (n > 1) {
            slice<ж<ast.Diagnostic>> relatedInfos = default;
            foreach (var k in range(n)) {
                relatedInfos = appendꓸꓸꓸ(relatedInfos, diagnostics[i + k].RelatedInformation());
            }
            if (relatedInfos != nil) {
                slices.SortFunc<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(relatedInfos, ast.CompareDiagnostics);
                relatedInfos = slices.CompactFunc<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(relatedInfos, ast.EqualDiagnostics);
                d = d.Clone().SetRelatedInfo(relatedInfos);
            }
        }
        diagnostics[j] = d;
        i += n;
        j++;
    }
    clear(diagnostics.slice(j));
    return diagnostics.slice(0, j);
}

} partial class compilerᴛProgram {

public static nint LineCount(this ref Program p) {
    nint count = default;
    foreach (var (_, @file) in range(p.files)) {
        count += len(@file.ECMALineMap());
    }
    return count;
}

public static nint IdentifierCount(this ref Program p) {
    nint count = default;
    foreach (var (_, @file) in range(p.files)) {
        count += @file.IdentifierCount;
    }
    return count;
}

public static nint SymbolCount(this ref Program p) {
    nint count = default;
    foreach (var (_, @file) in range(p.files)) {
        count += @file.SymbolCount;
    }
    ref var val = ref heap(new atomic.Uint32(), out var Ꮡval);
    Ꮡval.Store((uint32)count);
    p.ForEachCheckerParallel((nint _, ж<checker.Checker> c) => {
        Ꮡval.Add(c.SymbolCount);
    });
    return (nint)Ꮡval.Load();
}

public static nint TypeCount(this ref Program p) {
    ref var val = ref heap(new atomic.Uint32(), out var Ꮡval);
    p.ForEachCheckerParallel((nint _, ж<checker.Checker> c) => {
        Ꮡval.Add(c.TypeCount);
    });
    return (nint)Ꮡval.Load();
}

public static nint InstantiationCount(this ref Program p) {
    ref var val = ref heap(new atomic.Uint32(), out var Ꮡval);
    p.ForEachCheckerParallel((nint _, ж<checker.Checker> c) => {
        Ꮡval.Add(c.TotalInstantiationCount);
    });
    return (nint)Ꮡval.Load();
}

public static ж<Program> Program(this ж<Program> Ꮡp) {
    return Ꮡp;
}

public static ast.SourceFileMetaData GetSourceFileMetaData(this ref Program p, tspath.Path path) {
    return p.sourceFileMetaDatas[path];
}

public static core.ModuleKind GetEmitModuleFormatOfFile(this ref Program p, ast.HasFileName sourceFile) {
    return ast.GetEmitModuleFormatOfFileWorker(sourceFile.FileName(), p.projectReferenceFileMapper.getCompilerOptionsForFile(sourceFile), p.GetSourceFileMetaData(sourceFile.Path()));
}

public static coreꓸResolutionMode GetEmitSyntaxForUsageLocation(this ref Program p, ast.HasFileName sourceFile, ж<astꓸStringLiteralLike> location) {
    return getEmitSyntaxForUsageLocationWorker(sourceFile.FileName(), p.sourceFileMetaDatas[sourceFile.Path()], ref (location).DerefOrNull(), p.projectReferenceFileMapper.getCompilerOptionsForFile(sourceFile));
}

public static coreꓸResolutionMode GetImpliedNodeFormatForEmit(this ref Program p, ast.HasFileName sourceFile) {
    return ast.GetImpliedNodeFormatForEmitWorker(sourceFile.FileName(), p.projectReferenceFileMapper.getCompilerOptionsForFile(sourceFile).GetEmitModuleKind(), p.GetSourceFileMetaData(sourceFile.Path()));
}

public static coreꓸResolutionMode GetModeForUsageLocation(this ref Program p, ast.HasFileName sourceFile, ж<astꓸStringLiteralLike> location) {
    return getModeForUsageLocation(sourceFile.FileName(), p.sourceFileMetaDatas[sourceFile.Path()], ref (location).DerefOrNull(), p.projectReferenceFileMapper.getCompilerOptionsForFile(sourceFile));
}

public static coreꓸResolutionMode GetModeForResolutionAtIndex(this ref Program p, ж<ast.SourceFile> ᏑsourceFile, nint index) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    var imports = sourceFile.Imports();
    if (index < len(imports)) {
        return p.GetModeForUsageLocation(new ast.SourceFileжHasFileName(ᏑsourceFile), imports[index]);
    }
    index -= len(imports);
    foreach (var (_, augmentation) in range(sourceFile.ModuleAugmentations)) {
        if (augmentation.Kind == ast.KindStringLiteral) {
            if (index == 0) {
                return p.GetModeForUsageLocation(new ast.SourceFileжHasFileName(ᏑsourceFile), augmentation);
            }
            index--;
        }
    }
    throw panic("resolution index out of range");
}

public static coreꓸResolutionMode GetDefaultResolutionModeForFile(this ref Program p, ast.HasFileName sourceFile) {
    return getDefaultResolutionModeForFile(sourceFile.FileName(), p.sourceFileMetaDatas[sourceFile.Path()], p.projectReferenceFileMapper.getCompilerOptionsForFile(sourceFile));
}

public static bool IsSourceFileDefaultLibrary(this ref Program p, tspath.Path path) {
    var (_, ok) = p.libFiles[path, ꟷ];
    return ok;
}

public static bool IsGlobalTypingsFile(this ref Program p, @string fileName) {
    if (!tspath.IsDeclarationFileName(fileName)) {
        return false;
    }
    return tspath.ContainsPath(p.GetGlobalTypingsCacheLocation(), fileName, p.comparePathsOptions);
}

public static ж<LibFile> GetDefaultLibFile(this ref Program p, tspath.Path path) {
    var (libFile, ok) = p.libFiles[path, ꟷ]; if (ok) {
        return libFile;
    }
    return default;
}

public static @string CommonSourceDirectory(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑcommonSourceDirectoryOnce.Do(() => {
        var files = () => core.MapFiltered(Ꮡp.Value.files, (@string, bool) (ж<ast.SourceFile> @file) => (@file.FileName(), sourceFileMayBeEmitted(@file, new ProgramжSourceFileMayBeEmittedHost(Ꮡp), false, false) && !@file.IsDeclarationFile));
        Ꮡp.Value.commonSourceDirectory = outputpaths.GetCommonSourceDirectory(
            Ꮡp.Value.Options(),
            files,
            Ꮡp.Value.GetCurrentDirectory(),
            Ꮡp.Value.UseCaseSensitiveFileNames(),
            Ꮡp.checkSourceFilesBelongToPath);
    });
    return p.commonSourceDirectory;
}

internal static bool checkSourceFilesBelongToPath(this ref Program p, slice<@string> sourceFiles, @string rootDirectory) {
    var allFilesBelongToPath = true;
    foreach (var (_, @file) in range(sourceFiles)) {
        @string absoluteSourceFilePath = tspath.GetCanonicalFileName(tspath.GetNormalizedAbsolutePath(@file, p.GetCurrentDirectory()), p.UseCaseSensitiveFileNames());
        if (!tspath.ContainsPath(rootDirectory, @file, p.comparePathsOptions)) {
            p.includeProcessor.addProcessingDiagnostic(Ꮡ(new processingDiagnostic(
                kind: processingDiagnosticKindExplainingFileInclude,
                data: Ꮡ(new includeExplainingDiagnostic(
                    @file: ((tspath.Path)absoluteSourceFilePath),
                    message: diagnostics.File_0_is_not_under_rootDir_1_rootDir_is_expected_to_contain_all_source_files,
                    args: [@file, rootDirectory]
                ))
            )));
            allFilesBelongToPath = false;
        }
    }

    return allFilesBelongToPath;
}

} partial class compiler_package {

partial struct WriteFileData {
    public nint SourceMapUrlPos;
    public any BuildInfo;
    public slice<ж<ast.Diagnostic>> Diagnostics;
    public bool SkippedDtsWrite;
    public ж<ast.SourceFile> SourceFile;
}

// type WriteFile is a methodless func type — rendered inline as its base delegate

partial struct EmitOptions {
    public slice<ж<ast.SourceFile>> TargetSourceFiles;
    public EmitOnly EmitOnly;
    public bool ForceEmit;
    public Func<@string, @string, ж<WriteFileData>, error> WriteFile;
}

partial struct EmitResult {
    public bool EmitSkipped;
    public slice<ж<ast.Diagnostic>> Diagnostics;
    public slice<@string> EmittedFiles;
    public slice<ж<SourceMapEmitResult>> SourceMaps;
}

partial struct SourceMapEmitResult {
    public slice<@string> InputSourceFileNames;
    public ж<sourcemap.RawSourceMap> SourceMap;
    public @string GeneratedFile;
}

} partial class compilerᴛProgram {

public static ж<EmitResult> Emit(this ж<Program> Ꮡp, context.Context ctx, EmitOptions options) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();
        var emittersᴷ = new stackAppend<ж<emitter>>();

        {
            var tr = p.opts.Tracing; if (tr != nil) {
                var trʗ1 = tr;
                defer(trʗ1.Push(tracing.PhaseEmit, "emit", default, true), ref ᒐ);
            }
        }

        if (!options.ForceEmit && options.EmitOnly != EmitOnlyBuilderSignature) {
            var result = HandleNoEmitOptions(
                ctx,
                new ProgramжProgramLike(Ꮡp),
                options.TargetSourceFiles,
                default);
            if (result != nil || ctx.Err() != nil) {
                return result;
            }
        }

        @string newLine = p.Options().NewLine.GetNewLineCharacter();
        var writerPool = Ꮡ(new sync.Pool(
            New: () => printer.NewTextWriter(newLine, 0)
        ));
        var wg = core.NewWorkGroup(p.SingleThreaded());
        slice<ж<emitter>> emitters = default;
        var forceDtsEmit = options.EmitOnly == EmitOnlyBuilderSignature || options.ForceEmit && options.EmitOnly == EmitOnlyDts;
        var forceJsEmit = options.ForceEmit && options.EmitOnly == EmitOnlyJs;
        var sourceFiles = Ꮡp.getSourceFilesToEmit(options.TargetSourceFiles, forceDtsEmit, forceJsEmit);

        foreach (var (_, sourceFile) in range(sourceFiles)) {
            var emitter = Ꮡ(new emitter(
                writer: default,
                sourceFile: sourceFile,
                emitOnly: options.EmitOnly,
                forceEmit: options.ForceEmit,
                writeFile: options.WriteFile,
                tr: p.opts.Tracing
            ));
            emitters = append(emittersᴷ, emitters, emitter);
            var emitterʗ1 = emitter;
            var optionsʗ1 = options;
            var sourceFileʗ1 = sourceFile;
            var writerPoolʗ1 = writerPool;
            wg.Queue(() => {
                GoFrame ᒐ = default;
                try {
                    var (host, done) = newEmitHost(ctx, Ꮡp, sourceFileʗ1);
                    var doneʗ1 = done;
                    defer(doneʗ1, ref ᒐ);
                    emitterʗ1.host = new emitHostжEmitHost(host);

                    var writer = writerPoolʗ1.Get()._<printer.EmitTextWriter>();
                    writer.Clear();

                    emitterʗ1.writer = writer;
                    emitterʗ1.paths = outputpaths.GetOutputPathsFor(sourceFileʗ1, host.Options(), new emitHostжOutputPathsHost(host), new outputpaths.ForceEmitPaths(
                        Dts: forceDtsEmit,
                        Js: forceJsEmit,
                        DeclarationMap: optionsʗ1.ForceEmit && optionsʗ1.EmitOnly == EmitOnlyDts
                    ));
                    emitterʗ1.emit();
                    emitterʗ1.writer = default;

                    writerPoolʗ1.Put(writer);
                }
                catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
                finally { ᒐ.Run(); }
            });
        }

        wg.RunAndWait();

        return CombineEmitResults(core.Map(emitters, ж<EmitResult> (ж<emitter> e) => e.ᏑemitResult));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

public static ж<EmitResult> CombineEmitResults(slice<ж<EmitResult>> results) {
    var result = Ꮡ(new EmitResult());
    foreach (var (_, emitResult) in range(results)) {
        if (emitResult == nil) {
            continue;
        }
        if (emitResult.EmitSkipped) {
            result.EmitSkipped = true;
        }
        result.Diagnostics = appendꓸꓸꓸ(result.Diagnostics, emitResult.Diagnostics);
        result.EmittedFiles = appendꓸꓸꓸ(result.EmittedFiles, emitResult.EmittedFiles);
        if (emitResult.SourceMaps != nil) {
            result.SourceMaps = appendꓸꓸꓸ(result.SourceMaps, emitResult.SourceMaps);
        }
    }
    return result;
}

partial interface ProgramLike {
    ж<core.CompilerOptions> Options();
    ж<ast.SourceFile> GetSourceFile(@string path);
    slice<ж<ast.SourceFile>> GetSourceFiles();
    slice<ж<ast.Diagnostic>> GetConfigFileParsingDiagnostics();
    slice<ж<ast.Diagnostic>> GetSyntacticDiagnostics(context.Context ctx, ж<ast.SourceFile> @file);
    slice<ж<ast.Diagnostic>> GetBindDiagnostics(context.Context ctx, ж<ast.SourceFile> @file);
    slice<ж<ast.Diagnostic>> GetProgramDiagnostics();
    slice<ж<ast.Diagnostic>> GetGlobalDiagnostics(context.Context ctx);
    slice<ж<ast.Diagnostic>> GetSemanticDiagnostics(context.Context ctx, ж<ast.SourceFile> @file);
    slice<ж<ast.Diagnostic>> GetDeclarationDiagnostics(context.Context ctx, ж<ast.SourceFile> @file);
    slice<ж<ast.Diagnostic>> GetSuggestionDiagnostics(context.Context ctx, ж<ast.SourceFile> @file);
    ж<EmitResult> Emit(context.Context ctx, EmitOptions options);
    @string CommonSourceDirectory();
    bool IsSourceFileDefaultLibrary(tspath.Path path);
    ж<Program> Program();
}

public static ж<EmitResult> HandleNoEmitOptions(context.Context ctx, ProgramLike program, slice<ж<ast.SourceFile>> files, Func<ж<EmitResult>> emitBuildInfo) {
    if (!program.Options().NoEmit.IsTrue()) {
        if (!program.Options().NoEmitOnError.IsTrue()) {
            return default;
        }

        var diagnostics = GetDiagnosticsOfAnyProgram(
            ctx,
            program,
            files,
            true,
            program.GetBindDiagnostics,
            program.GetSemanticDiagnostics);
        if (len(diagnostics) == 0) {
            return default;
        }
        return Ꮡ(new EmitResult(
            Diagnostics: diagnostics,
            EmitSkipped: true
        ));
    }
    if (files != nil) {
        return Ꮡ(new EmitResult(EmitSkipped: true));
    }
    if (emitBuildInfo != nil) {
        var result = emitBuildInfo();
        if (result != nil) {
            return result;
        }
    }
    return Ꮡ(new EmitResult());
}

public static slice<ж<ast.Diagnostic>> GetDiagnosticsOfAnyProgram(context.Context ctx, ProgramLike program, slice<ж<ast.SourceFile>> files, bool skipNoEmitCheckForDtsDiagnostics, Func<context.Context, ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> getBindDiagnostics, Func<context.Context, ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> getSemanticDiagnostics) {
    var allDiagnostics = slices.Clip<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(program.GetConfigFileParsingDiagnostics());
    nint configFileParsingDiagnosticsLength = len(allDiagnostics);

    var filesʗ1 = files;
    slice<ж<ast.Diagnostic>> appendDiagnosticsForAllFiles(slice<ж<ast.Diagnostic>> diagnostics, Func<context.Context, ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>> getDiagnostics) {
        if (filesʗ1 == nil) {
            return appendꓸꓸꓸ(diagnostics, getDiagnostics(ctx, nil));
        }
        foreach (var (_, @file) in range(filesʗ1)) {
            diagnostics = appendꓸꓸꓸ(diagnostics, getDiagnostics(ctx, @file));
        }
        return diagnostics;
    }

    var syntacticDiagnostics = appendDiagnosticsForAllFiles(default, program.GetSyntacticDiagnostics);
    if (len(syntacticDiagnostics) > 0) {
        allDiagnostics = appendꓸꓸꓸ(allDiagnostics, program.Program().contentMapperDiagnostics);
    }
    allDiagnostics = appendꓸꓸꓸ(allDiagnostics, syntacticDiagnostics);

    if (len(allDiagnostics) == configFileParsingDiagnosticsLength) {
        allDiagnostics = appendꓸꓸꓸ(allDiagnostics, program.GetProgramDiagnostics());

        appendDiagnosticsForAllFiles(default, getBindDiagnostics);

        if (program.Options().ListFilesOnly.IsFalseOrUnknown()) {
            allDiagnostics = appendꓸꓸꓸ(allDiagnostics, program.GetGlobalDiagnostics(ctx));

            if (len(allDiagnostics) == configFileParsingDiagnosticsLength) {
                allDiagnostics = appendDiagnosticsForAllFiles(allDiagnostics, getSemanticDiagnostics);
                allDiagnostics = appendꓸꓸꓸ(allDiagnostics, program.GetGlobalDiagnostics(ctx));
            }

            if ((skipNoEmitCheckForDtsDiagnostics || program.Options().NoEmit.IsTrue()) && program.Options().GetEmitDeclarations() && len(allDiagnostics) == configFileParsingDiagnosticsLength) {
                allDiagnostics = appendDiagnosticsForAllFiles(allDiagnostics, program.GetDeclarationDiagnostics);
            }
        }
    }
    return allDiagnostics;
}

} partial class compilerᴛProgram {

internal static tspath.Path toPath(this ref Program p, @string filename) {
    return tspath.ToPath(filename, p.GetCurrentDirectory(), p.UseCaseSensitiveFileNames());
}

public static ж<ast.SourceFile> GetSourceFile(this ref Program p, @string filename) {
    tspath.Path path = p.toPath(filename);
    return p.GetSourceFileByPath(path);
}

public static ж<ast.SourceFile> GetSourceFileForResolvedModule(this ref Program p, @string fileName) {
    var @file = p.GetSourceFile(fileName);
    if (@file == nil) {
        @string filename = p.GetParseFileRedirect(fileName);
        if (filename != ""u8) {
            return p.GetSourceFile(filename);
        }
    }
    return @file;
}

public static map<tspath.Path, ж<ast.SourceFile>> FilesByPath(this ref Program p) {
    return p.filesByPath;
}

public static ж<ast.SourceFile> GetSourceFileByPath(this ref Program p, tspath.Path path) {
    return p.filesByPath[path];
}

public static bool HasSameFileNames(this ref Program p, ж<Program> Ꮡother) {
    ref var other = ref Ꮡother.DerefOrNull();

    return maps.EqualFunc<map<tspath.Path, ж<ast.SourceFile>>, map<tspath.Path, ж<ast.SourceFile>>, tspath.Path, ж<ast.SourceFile>, ж<ast.SourceFile>>(p.filesByPath, other.filesByPath, (ж<ast.SourceFile> a, ж<ast.SourceFile> b) => a.FileName() == b.FileName()) && maps.EqualFunc<map<tspath.Path, ж<redirectsFile>>, map<tspath.Path, ж<redirectsFile>>, tspath.Path, ж<redirectsFile>, ж<redirectsFile>>(p.redirectFilesByPath, other.redirectFilesByPath, (ж<redirectsFile> a, ж<redirectsFile> b) => a.FileName() == b.FileName());
}

public static slice<ж<ast.SourceFile>> GetSourceFiles(this ref Program p) {
    return p.files;
}

public static map<tspath.Path, slice<ж<FileIncludeReason>>> GetIncludeReasons(this ref Program p) {
    return p.includeProcessor.fileIncludeReasons;
}

public static bool IsMissingPath(this ж<Program> Ꮡp, tspath.Path path) {
    ref var p = ref Ꮡp.DerefOrNull();

    return slices.ContainsFunc(p.missingFiles, (@string missingPath) => Ꮡp.Value.toPath(missingPath) == path);
}

public static void ExplainFiles(this ж<Program> Ꮡp, io.Writer w, locale.Locale localeΔ1) {
    ref var p = ref Ꮡp.DerefOrNull();

    var toRelativeFileName = @string (@string fileName) => tspath.GetRelativePathFromDirectory(Ꮡp.Value.GetCurrentDirectory(), fileName, Ꮡp.Value.comparePathsOptions);
    nint filesExplained = 0;
    var localeʗ1 = localeΔ1;
    var toRelativeFileNameʗ1 = toRelativeFileName;
    void explainFile(ast.HasFileName @file) {
        fmt.Fprintln(w, toRelativeFileNameʗ1(@file.FileName()));
        foreach (var (_, reason) in range(Ꮡp.Value.includeProcessor.fileIncludeReasons[@file.Path()])) {
            fmt.Fprintln(w, "  ", reason.toDiagnostic(Ꮡp, true).Localize(localeʗ1));
        }
        foreach (var (_, diag) in range(Ꮡp.Value.includeProcessor.explainRedirectAndImpliedFormat(Ꮡp, @file.Path(), toRelativeFileNameʗ1))) {
            fmt.Fprintln(w, "  ", diag.Localize(localeʗ1));
        }
        filesExplained++;
    }

    var redirectFiles = slices.Collect(maps.Values<map<tspath.Path, ж<redirectsFile>>, tspath.Path, ж<redirectsFile>>(p.redirectFilesByPath));
    slices.SortFunc(redirectFiles, (ж<redirectsFile> a, ж<redirectsFile> b) => a.index - b.index);

    var files = p.GetSourceFiles();
    nint sourceFileIndex = 0;
    var explainFileʗ1 = explainFile;
    var filesʗ1 = files;
    void explainSourceFiles(nint endIndex) {
        while (filesExplained < endIndex) {
            explainFileʗ1(new ast.SourceFileжHasFileName(filesʗ1[sourceFileIndex]));
            sourceFileIndex++;
        }
    }

    foreach (var (_, redirectFile) in range(redirectFiles)) {
        explainSourceFiles(redirectFile.index);
        explainFile(new redirectsFileжHasFileName(redirectFile));
    }

    explainSourceFiles(len(files) + len(redirectFiles));
}

public static ж<ast.SourceFile> GetLibFileFromReference(this ref Program p, ж<ast.FileReference> Ꮡref) {
    ref var @ref = ref Ꮡref.DerefOrNull();

    var (path, ok) = tsoptions.GetLibFileName(@ref.FileName);
    if (!ok) {
        return default;
    }
    var (sourceFile, okΔ1) = p.filesByPath[((tspath.Path)path), ꟷ]; if (okΔ1) {
        return sourceFile;
    }
    return default;
}

public static ж<module.ResolvedTypeReferenceDirective> GetResolvedTypeReferenceDirectiveFromTypeReferenceDirective(this ref Program p, ж<ast.FileReference> ᏑtypeRef, ж<ast.SourceFile> sourceFile) {
    ref var typeRef = ref ᏑtypeRef.DerefOrNull();

    return p.GetResolvedTypeReferenceDirective(new ast.SourceFileжHasFileName(sourceFile), typeRef.FileName, p.getModeForTypeReferenceDirectiveInFile(ᏑtypeRef, sourceFile));
}

public static ж<module.ResolvedTypeReferenceDirective> GetResolvedTypeReferenceDirective(this ref Program p, ast.HasFileName @file, @string typeDirectiveName, coreꓸResolutionMode mode) {
    var (resolutions, ok) = p.typeResolutionsInFile[@file.Path(), ꟷ]; if (ok) {
        var (resolved, okΔ1) = resolutions[new module.ModeAwareCacheKey(Name: typeDirectiveName, Mode: mode), ꟷ]; if (okΔ1) {
            return resolved;
        }
    }
    return default;
}

public static map<tspath.Path, module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>>> GetResolvedTypeReferenceDirectives(this ref Program p) {
    return p.typeResolutionsInFile;
}

internal static coreꓸResolutionMode getModeForTypeReferenceDirectiveInFile(this ref Program p, ж<ast.FileReference> Ꮡref, ж<ast.SourceFile> sourceFile) {
    ref var @ref = ref Ꮡref.DerefOrNull();

    if (@ref.ResolutionMode != core.ResolutionModeNone) {
        return @ref.ResolutionMode;
    }
    return p.GetDefaultResolutionModeForFile(new ast.SourceFileжHasFileName(sourceFile));
}

public static bool IsSourceFileFromExternalLibrary(this ж<Program> Ꮡp, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    return Ꮡp.of(compiler_package.Program.ᏑsourceFilesFoundSearchingNodeModules).Has(@file.Path());
}

public static (@string moduleReference, ж<ast.Node> specifier) GetJSXRuntimeImportSpecifier(this ref Program p, tspath.Path path) {
    var result = p.jsxRuntimeImportSpecifiers[path]; if (result != nil) {
        return (result.moduleReference, result.specifier);
    }
    return ("", default);
}

public static ж<ast.Node> GetImportHelpersImportSpecifier(this ref Program p, tspath.Path path) {
    return p.importHelpersImportSpecifiers[path];
}

public static bool SourceFileMayBeEmitted(this ж<Program> Ꮡp, ж<ast.SourceFile> sourceFile, bool forceDtsEmit) {
    return sourceFileMayBeEmitted(sourceFile, new ProgramжSourceFileMayBeEmittedHost(Ꮡp), forceDtsEmit, false);
}

public static ж<collections.Set<@string>> ResolvedPackageNames(this ж<Program> Ꮡp) {
    return Ꮡp.collectPackageNames().resolved;
}

public static ж<collections.Set<@string>> UnresolvedPackageNames(this ж<Program> Ꮡp) {
    return Ꮡp.collectPackageNames().unresolved;
}

public static ж<collections.Set<@string>> DeepImportPackageNames(this ж<Program> Ꮡp) {
    return Ꮡp.collectPackageNames().deepImportPackages;
}

internal static ж<packageNamesInfo> collectPackageNames(this ж<Program> Ꮡp) {
    return Ꮡp.ᏑpackageNames.getValue(() => {
        var packageNames = Ꮡ(new packageNamesInfo(Ꮡ(new collections.Set<@string>()), Ꮡ(new collections.Set<@string>()), Ꮡ(new collections.Set<@string>())));
        foreach (var (_, @file) in range(Ꮡp.Value.files)) {
            if (Ꮡp.Value.IsSourceFileDefaultLibrary(@file.Path()) || Ꮡp.IsSourceFileFromExternalLibrary(@file) || strings.Contains(@file.FileName(), "/node_modules/")) {
                continue;
            }
            foreach (var (_, imp) in range(@file.Imports())) {
                if (tspath.IsExternalModuleNameRelative(imp.Text())) {
                    continue;
                }
                var (resolvedModules, ok) = Ꮡp.Value.resolvedModules[@file.Path(), ꟷ]; if (ok) {
                    var key = new module.ModeAwareCacheKey(Name: imp.Text(), Mode: Ꮡp.Value.GetModeForUsageLocation(new ast.SourceFileжHasFileName(@file), imp));
                    var (resolvedModule, okΔ1) = resolvedModules[key, ꟷ]; if (okΔ1 && resolvedModule.IsResolved()) {
                        if (!resolvedModule.IsExternalLibraryImport) {
                            continue;
                        }
                        @string name = resolvedModule.PackageId.Name;
                        if (name == ""u8) {
                            var packageScope = Ꮡp.Value.resolver.GetPackageScopeForPath(resolvedModule.ResolvedFileName); if (packageScope != nil && packageScope.Exists()) {
                                var (scopeName, okΔ2) = packageScope.Contents.of(packagejson.PackageJson.ᏑName).GetValue(); if (okΔ2) {
                                    name = scopeName;
                                }
                            }
                        }
                        if (name == ""u8) {
                            name = modulespecifiers.GetPackageNameFromDirectory(resolvedModule.ResolvedFileName);
                        }
                        if (name != ""u8) {
                            packageNames.resolved.Add(name);
                            var (_, rest) = module.ParsePackageName(imp.Text());
                            if (rest != ""u8) {
                                var scope = Ꮡp.Value.resolver.GetPackageScopeForPath(resolvedModule.ResolvedFileName); if (scope != nil && scope.Exists() && !scope.Contents.of(packagejson.PackageJson.ᏑExports).of(packagejson.ExportsOrImports.ᏑJSONValue).IsPresent()) {
                                    packageNames.deepImportPackages.Add(module.GetPackageNameFromTypesPackageName(name));
                                }
                            }
                        }
                        continue;
                    }
                }
                packageNames.unresolved.Add(imp.Text());
            }
        }
        return packageNames;
    });
}

public static bool IsLibFile(this ref Program p, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    var (_, ok) = p.libFiles[sourceFile.Path(), ꟷ];
    return ok;
}

public static bool HasTSFile(this ж<Program> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.ᏑhasTSFileOnce.Do(() => {
        foreach (var (_, @file) in range(Ꮡp.Value.files)) {
            if (tspath.HasImplementationTSFileExtension(@file.FileName())) {
                Ꮡp.Value.hasTSFile = true;
                break;
            }
        }
    });
    return p.hasTSFile;
}

public static ж<symlinks.KnownSymlinks> GetSymlinkCache(this ж<Program> Ꮡp) {
    return Ꮡp.ᏑknownSymlinks.getValue(() => {
        var knownSymlinks = symlinks.NewKnownSymlink(Ꮡp.Value.GetCurrentDirectory(), Ꮡp.Value.UseCaseSensitiveFileNames());

        if (len(Ꮡp.Value.resolvedModules) > 0 || len(Ꮡp.Value.typeResolutionsInFile) > 0) {
            knownSymlinks.SetSymlinksFromResolutions(Ꮡp.ForEachResolvedModule, Ꮡp.ForEachResolvedTypeReferenceDirective);
        }

        ref var seenPackageJsons = ref heap(new collections.Set<tspath.Path>(), out var ᏑseenPackageJsons);
        foreach (var (filePath, meta) in Ꮡp.Value.sourceFileMetaDatas) {
            if (meta.PackageJsonDirectory == ""u8 || !Ꮡp.SourceFileMayBeEmitted(Ꮡp.Value.GetSourceFileByPath(filePath), false) || !ᏑseenPackageJsons.AddIfAbsent(Ꮡp.Value.toPath(meta.PackageJsonDirectory))) {
                continue;
            }
            @string packageJsonName = tspath.CombinePaths(meta.PackageJsonDirectory, "package.json");
            var info = Ꮡp.Value.GetPackageJsonInfo(packageJsonName);
            if (info.GetContents() == nil) {
                continue;
            }

            foreach (var (dep, _) in info.GetContents().of(packagejson.PackageJson.ᏑFields).of(packagejson.Fields.ᏑDependencyFields).GetRuntimeDependencyNames().Keys()) {
                tspath.Path possibleDirectoryPath = Ꮡp.Value.toPath(tspath.CombinePaths(meta.PackageJsonDirectory, "node_modules", dep));
                if (knownSymlinks.HasDirectory(possibleDirectoryPath)) {
                    continue;
                }
                if (!strings.HasPrefix(dep, "@types")) {
                    tspath.Path possibleTypesDirectoryPath = Ꮡp.Value.toPath(tspath.CombinePaths(meta.PackageJsonDirectory, "node_modules", module.GetTypesPackageName(dep)));
                    if (knownSymlinks.HasDirectory(possibleTypesDirectoryPath)) {
                        continue;
                    }
                }

                var packageResolution = Ꮡp.Value.resolver.ResolvePackageDirectory(dep, packageJsonName, core.ResolutionModeCommonJS, default); if (packageResolution.IsResolved() && packageResolution.OriginalPath != ""u8) {
                    knownSymlinks.ProcessResolution(
                        tspath.CombinePaths(packageResolution.OriginalPath, "package.json"),
                        tspath.CombinePaths(packageResolution.ResolvedFileName, "package.json"));
                }
            }
        }
        return knownSymlinks;
    });
}

public static ж<module.ResolvedModule> ResolveModuleName(this ref Program p, @string moduleName, @string containingFile, coreꓸResolutionMode resolutionMode) {
    var (resolved, _) = p.resolver.ResolveModuleName(moduleName, containingFile, resolutionMode, default);
    return resolved;
}

public static void ForEachResolvedModule(this ref Program p, Action<ж<module.ResolvedModule>, @string, coreꓸResolutionMode, tspath.Path> callback, ж<ast.SourceFile> @file) {
    forEachResolution(p.resolvedModules, callback, @file);
}

public static void ForEachResolvedTypeReferenceDirective(this ref Program p, Action<ж<module.ResolvedTypeReferenceDirective>, @string, coreꓸResolutionMode, tspath.Path> callback, ж<ast.SourceFile> @file) {
    forEachResolution(p.typeResolutionsInFile, callback, @file);
}

} partial class compiler_package {

internal static void forEachResolution<T>(map<tspath.Path, module.ModeAwareCache<T>> resolutionCache, Action<T, @string, coreꓸResolutionMode, tspath.Path> callback, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (Ꮡfile != nil) {
        var (resolutions, ok) = resolutionCache[@file.Path(), ꟷ]; if (ok) {
            foreach (var (key, resolution) in resolutions) {
                callback(resolution, key.Name, key.Mode, @file.Path());
            }
        }
    } else {
        foreach (var (filePath, resolutions) in resolutionCache) {
            foreach (var (key, resolution) in resolutions) {
                callback(resolution, key.Name, key.Mode, filePath);
            }
        }
    }
}

internal static ж<collections.Set<int32>> plainJSErrors = collections.NewSetFromItems(
    diagnostics.Cannot_redeclare_block_scoped_variable_0.Code(),
    diagnostics.A_module_cannot_have_multiple_default_exports.Code(),
    diagnostics.Another_export_default_is_here.Code(),
    diagnostics.The_first_export_default_is_here.Code(),
    diagnostics.Identifier_expected_0_is_a_reserved_word_at_the_top_level_of_a_module.Code(),
    diagnostics.Identifier_expected_0_is_a_reserved_word_in_strict_mode_Modules_are_automatically_in_strict_mode.Code(),
    diagnostics.Identifier_expected_0_is_a_reserved_word_that_cannot_be_used_here.Code(),
    diagnostics.X_constructor_is_a_reserved_word.Code(),
    diagnostics.X_delete_cannot_be_called_on_an_identifier_in_strict_mode.Code(),
    diagnostics.Code_contained_in_a_class_is_evaluated_in_JavaScript_s_strict_mode_which_does_not_allow_this_use_of_0_For_more_information_see_https_Colon_Slash_Slashdeveloper_mozilla_org_Slashen_US_Slashdocs_SlashWeb_SlashJavaScript_SlashReference_SlashStrict_mode.Code(),
    diagnostics.Invalid_use_of_0_Modules_are_automatically_in_strict_mode.Code(),
    diagnostics.Invalid_use_of_0_in_strict_mode.Code(),
    diagnostics.A_label_is_not_allowed_here.Code(),
    diagnostics.X_with_statements_are_not_allowed_in_strict_mode.Code(),
    diagnostics.A_break_statement_can_only_be_used_within_an_enclosing_iteration_or_switch_statement.Code(),
    diagnostics.A_break_statement_can_only_jump_to_a_label_of_an_enclosing_statement.Code(),
    diagnostics.A_class_declaration_without_the_default_modifier_must_have_a_name.Code(),
    diagnostics.A_class_member_cannot_have_the_0_keyword.Code(),
    diagnostics.A_comma_expression_is_not_allowed_in_a_computed_property_name.Code(),
    diagnostics.A_continue_statement_can_only_be_used_within_an_enclosing_iteration_statement.Code(),
    diagnostics.A_continue_statement_can_only_jump_to_a_label_of_an_enclosing_iteration_statement.Code(),
    diagnostics.A_default_clause_cannot_appear_more_than_once_in_a_switch_statement.Code(),
    diagnostics.A_default_export_must_be_at_the_top_level_of_a_file_or_module_declaration.Code(),
    diagnostics.A_definite_assignment_assertion_is_not_permitted_in_this_context.Code(),
    diagnostics.A_destructuring_declaration_must_have_an_initializer.Code(),
    diagnostics.A_get_accessor_cannot_have_parameters.Code(),
    diagnostics.A_rest_element_cannot_contain_a_binding_pattern.Code(),
    diagnostics.A_rest_element_cannot_have_a_property_name.Code(),
    diagnostics.A_rest_element_cannot_have_an_initializer.Code(),
    diagnostics.A_rest_element_must_be_last_in_a_destructuring_pattern.Code(),
    diagnostics.A_rest_parameter_cannot_have_an_initializer.Code(),
    diagnostics.A_rest_parameter_must_be_last_in_a_parameter_list.Code(),
    diagnostics.A_rest_parameter_or_binding_pattern_may_not_have_a_trailing_comma.Code(),
    diagnostics.A_return_statement_cannot_be_used_inside_a_class_static_block.Code(),
    diagnostics.A_set_accessor_cannot_have_rest_parameter.Code(),
    diagnostics.A_set_accessor_must_have_exactly_one_parameter.Code(),
    diagnostics.An_export_declaration_can_only_be_used_at_the_top_level_of_a_module.Code(),
    diagnostics.An_export_declaration_cannot_have_modifiers.Code(),
    diagnostics.An_import_declaration_can_only_be_used_at_the_top_level_of_a_module.Code(),
    diagnostics.An_import_declaration_cannot_have_modifiers.Code(),
    diagnostics.An_object_member_cannot_be_declared_optional.Code(),
    diagnostics.Argument_of_dynamic_import_cannot_be_spread_element.Code(),
    diagnostics.Cannot_assign_to_private_method_0_Private_methods_are_not_writable.Code(),
    diagnostics.Cannot_redeclare_identifier_0_in_catch_clause.Code(),
    diagnostics.Catch_clause_variable_cannot_have_an_initializer.Code(),
    diagnostics.Class_decorators_can_t_be_used_with_static_private_identifier_Consider_removing_the_experimental_decorator.Code(),
    diagnostics.Classes_can_only_extend_a_single_class.Code(),
    diagnostics.Classes_may_not_have_a_field_named_constructor.Code(),
    diagnostics.Did_you_mean_to_use_a_Colon_An_can_only_follow_a_property_name_when_the_containing_object_literal_is_part_of_a_destructuring_pattern.Code(),
    diagnostics.Duplicate_label_0.Code(),
    diagnostics.Dynamic_imports_can_only_accept_a_module_specifier_and_an_optional_set_of_attributes_as_arguments.Code(),
    diagnostics.X_for_await_loops_cannot_be_used_inside_a_class_static_block.Code(),
    diagnostics.JSX_attributes_must_only_be_assigned_a_non_empty_expression.Code(),
    diagnostics.JSX_elements_cannot_have_multiple_attributes_with_the_same_name.Code(),
    diagnostics.JSX_expressions_may_not_use_the_comma_operator_Did_you_mean_to_write_an_array.Code(),
    diagnostics.JSX_property_access_expressions_cannot_include_JSX_namespace_names.Code(),
    diagnostics.Jump_target_cannot_cross_function_boundary.Code(),
    diagnostics.Line_terminator_not_permitted_before_arrow.Code(),
    diagnostics.Modifiers_cannot_appear_here.Code(),
    diagnostics.Only_a_single_variable_declaration_is_allowed_in_a_for_in_statement.Code(),
    diagnostics.Only_a_single_variable_declaration_is_allowed_in_a_for_of_statement.Code(),
    diagnostics.Private_identifiers_are_not_allowed_outside_class_bodies.Code(),
    diagnostics.Private_identifiers_are_only_allowed_in_class_bodies_and_may_only_be_used_as_part_of_a_class_member_declaration_property_access_or_on_the_left_hand_side_of_an_in_expression.Code(),
    diagnostics.Property_0_is_not_accessible_outside_class_1_because_it_has_a_private_identifier.Code(),
    diagnostics.Tagged_template_expressions_are_not_permitted_in_an_optional_chain.Code(),
    diagnostics.The_left_hand_side_of_a_for_of_statement_may_not_be_async.Code(),
    diagnostics.The_variable_declaration_of_a_for_in_statement_cannot_have_an_initializer.Code(),
    diagnostics.The_variable_declaration_of_a_for_of_statement_cannot_have_an_initializer.Code(),
    diagnostics.Trailing_comma_not_allowed.Code(),
    diagnostics.Variable_declaration_list_cannot_be_empty.Code(),
    diagnostics.X_0_and_1_operations_cannot_be_mixed_without_parentheses.Code(),
    diagnostics.X_0_expected.Code(),
    diagnostics.X_0_is_not_a_valid_meta_property_for_keyword_1_Did_you_mean_2.Code(),
    diagnostics.X_0_list_cannot_be_empty.Code(),
    diagnostics.X_0_modifier_already_seen.Code(),
    diagnostics.X_0_modifier_cannot_appear_on_a_constructor_declaration.Code(),
    diagnostics.X_0_modifier_cannot_appear_on_a_module_or_namespace_element.Code(),
    diagnostics.X_0_modifier_cannot_appear_on_a_parameter.Code(),
    diagnostics.X_0_modifier_cannot_appear_on_class_elements_of_this_kind.Code(),
    diagnostics.X_0_modifier_cannot_be_used_here.Code(),
    diagnostics.X_0_modifier_must_precede_1_modifier.Code(),
    diagnostics.X_0_declarations_can_only_be_declared_inside_a_block.Code(),
    diagnostics.X_0_declarations_must_be_initialized.Code(),
    diagnostics.X_extends_clause_already_seen.Code(),
    diagnostics.X_let_is_not_allowed_to_be_used_as_a_name_in_let_or_const_declarations.Code(),
    diagnostics.Class_constructor_may_not_be_a_generator.Code(),
    diagnostics.Class_constructor_may_not_be_an_accessor.Code(),
    diagnostics.X_await_expressions_are_only_allowed_within_async_functions_and_at_the_top_levels_of_modules.Code(),
    diagnostics.X_await_using_statements_are_only_allowed_within_async_functions_and_at_the_top_levels_of_modules.Code(),
    diagnostics.Private_field_0_must_be_declared_in_an_enclosing_class.Code(),
    diagnostics.This_condition_will_always_return_0_since_JavaScript_compares_objects_by_reference_not_value.Code());

} // end compiler_package
