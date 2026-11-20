namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using cmp = cmp_package;
using errors = errors_package;
using fmt = fmt_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using parser = go.github.com.microsoft.TypeScript.tsc.@internal.parser_package;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using xxh3 = go.github.com.zeebo.xxh3_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.zeebo;
using go.sync;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using ꓸꓸꓸany = Span<any>;
using static compiler_package;

partial class compiler_package {

partial struct libResolution {
    internal @string libraryName;
    internal ж<module.ResolvedModule> resolution;
    internal slice<module.DiagAndArgs> trace;
}

internal static UntypedInt maxContentMapperFailures => 5;

partial struct LibFile {
    public @string Name;
    internal @string path;
    public bool Replaced;
}

partial struct sourceFileFromReferenceDiagnostic {
    internal ж<diagnostics.Message> message;
    internal slice<any> args;
}

partial struct fileLoader {
    internal ProgramOptions opts;
    internal ж<module.Resolver> resolver;
    internal @string defaultLibraryPath;
    internal tspath.ComparePathsOptions comparePathsOptions;
    internal slice<slice<@string>> supportedExtensions;
    internal slice<slice<@string>> supportedExtensionsWithJsonIfResolveJsonModule;
    internal slice<@string> contentMapperExtensions;
    internal ж<filesParser> filesParser;
    internal slice<ж<parseTask>> rootTasks;
    internal atomic.Int32 totalFileCount;
    internal atomic.Int32 libFileCount;
    internal sync.Mutex factoryMu;
    internal ast.NodeFactory factory;
    internal ж<projectReferenceFileMapper> projectReferenceFileMapper;
    internal collections.Set<tspath.Path> dtsDirectories;
    internal collections.SyncMap<@string, ж<LibFile>> pathForLibFileCache;
    internal collections.SyncMap<tspath.Path, ж<libResolution>> pathForLibFileResolutions;
    internal sync.Mutex contentMapperMu;
    internal map<ж<contentmapper.Mapper>, nint> contentMapperFailures;
    internal collections.Set<ж<contentmapper.Mapper>> contentMapperInitFailed;
    internal slice<ж<ast.Diagnostic>> contentMapperDiagnostics;
}

partial struct redirectsFile {
    internal nint index;
    internal @string fileName;
    internal tspath.Path path;
    internal tspath.Path target;
}

partial struct DuplicateSourceFile {
    public ast.SourceFileParseOptions ParseOptions;
    public ast.SourceFileParseOptions ContentMapperParseOptions;
    public xxh3.Uint128 Hash;
    public core.ScriptKind ScriptKind;
    public @string ContentMapper;
    public bool IsContentMapperFailureStub;
}

internal static ast.HasFileName _ᴛ3ʗ = new redirectsFileжHasFileName(((ж<redirectsFile>)nil));

} partial class compilerᴛredirectsFile {

internal static @string FileName(this ref redirectsFile r) {
    return r.fileName;
}

internal static tspath.Path Path(this ref redirectsFile r) {
    return r.path;
}

} partial class compiler_package {

partial struct processedFiles {
    internal ж<module.Resolver> resolver;
    internal slice<ж<ast.SourceFile>> files;
    internal slice<ж<DuplicateSourceFile>> duplicateSourceFiles;
    internal map<tspath.Path, ж<ast.SourceFile>> filesByPath;
    internal ж<projectReferenceFileMapper> projectReferenceFileMapper;
    internal slice<@string> missingFiles;
    internal map<tspath.Path, module.ModeAwareCache<ж<module.ResolvedModule>>> resolvedModules;
    internal map<tspath.Path, module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>>> typeResolutionsInFile;
    internal map<tspath.Path, ast.SourceFileMetaData> sourceFileMetaDatas;
    internal map<tspath.Path, ж<jsxRuntimeImportSpecifier>> jsxRuntimeImportSpecifiers;
    internal map<tspath.Path, ж<astꓸStringLiteralNode>> importHelpersImportSpecifiers;
    internal map<tspath.Path, ж<LibFile>> libFiles;
    internal collections.Set<tspath.Path> sourceFilesFoundSearchingNodeModules;
    internal ж<includeProcessor> includeProcessor;
    internal map<tspath.Path, @string> outputFileToProjectReferenceSource;
    internal map<tspath.Path, slice<@string>> redirectTargetsMap;
    internal map<tspath.Path, ж<redirectsFile>> redirectFilesByPath;
    internal slice<ж<ast.Diagnostic>> contentMapperDiagnostics;
    internal bool finishedProcessing;
}

partial struct jsxRuntimeImportSpecifier {
    internal @string moduleReference;
    internal ж<astꓸStringLiteralNode> specifier;
}

internal static processedFiles processAllProgramFiles(ProgramOptions opts, bool singleThreaded) {
    GoFrame ᒐ = default;
    try {
        var compilerOptions = opts.Config.CompilerOptions();
        var rootFiles = opts.Config.FileNames();
        var supportedExtensions = tsoptions.GetSupportedExtensions(compilerOptions, opts.Config.ContentMapperExtensions());
        var supportedExtensionsWithJsonIfResolveJsonModule = tsoptions.GetSupportedExtensionsWithJsonIfResolveJsonModule(compilerOptions, supportedExtensions);
        ref var maxNodeModuleJsDepth = ref heap(new nint(), out var ᏑmaxNodeModuleJsDepth);
        var p = opts.Config.CompilerOptions().MaxNodeModuleJsDepth; if (p != nil) {
            maxNodeModuleJsDepth = p.Value;
        }
        ref var loader = ref heap<fileLoader>(new fileLoader(
            opts: opts,
            defaultLibraryPath: tspath.GetNormalizedAbsolutePath(opts.Host.DefaultLibraryPath(), opts.Host.GetCurrentDirectory()),
            comparePathsOptions: new tspath.ComparePathsOptions(
                UseCaseSensitiveFileNames: opts.Host.FS().UseCaseSensitiveFileNames(),
                CurrentDirectory: opts.Host.GetCurrentDirectory()
            ),
            filesParser: Ꮡ(new filesParser(
                wg: core.NewWorkGroup(singleThreaded),
                maxDepth: maxNodeModuleJsDepth
            )),
            rootTasks: new slice<ж<parseTask>>(0, len(rootFiles) + len(compilerOptions.Lib)),
            supportedExtensions: supportedExtensions,
            supportedExtensionsWithJsonIfResolveJsonModule: supportedExtensionsWithJsonIfResolveJsonModule,
            contentMapperExtensions: opts.Config.ContentMapperExtensions()
        ), out var Ꮡloader);
        Ꮡloader.addProjectReferenceTasks(singleThreaded);
        loader.resolver = module.NewResolver(loader.projectReferenceFileMapper.host, compilerOptions, opts.TypingsLocation, opts.ProjectName, opts.Config.ContentMapperExtensions());
        if (opts.Tracing != nil) {
            var optsʗ1 = opts;
            var rootFilesʗ1 = rootFiles;
            defer(optsʗ1.Tracing.Push(tracing.PhaseProgram, "processRootFiles", new map<@string, any>{["count"] = len(rootFilesʗ1)}, false), ref ᒐ);
        }
        foreach (var (iᴛ1, rootFile) in range(rootFiles)) {
            ref var index = ref heap(new nint(), out var Ꮡindex);
            index = iᴛ1;

            loader.addRootFileTask(rootFile, nil, Ꮡ(new FileIncludeReason(kind: fileIncludeKindRootFile, data: index)));
        }
        if (len(rootFiles) > 0 && compilerOptions.NoLib.IsFalseOrUnknown()) {
            if (compilerOptions.Lib == nil) {
                @string name = tsoptions.GetDefaultLibFileName(compilerOptions);
                var libFile = Ꮡloader.pathForLibFile(name);
                loader.addRootTask(libFile.path, libFile, Ꮡ(new FileIncludeReason(kind: fileIncludeKindLibFile)));
            } else {
                foreach (var (iᴛ2, lib) in range(compilerOptions.Lib)) {
                    ref var index = ref heap(new nint(), out var Ꮡindex);
                    index = iᴛ2;

                    var (name, ok) = tsoptions.GetLibFileName(lib); if (ok) {
                        var libFile = Ꮡloader.pathForLibFile(name);
                        loader.addRootTask(libFile.path, libFile, Ꮡ(new FileIncludeReason(kind: fileIncludeKindLibFile, data: index)));
                    }
                }
            }
        }

        if (len(rootFiles) > 0 && !opts.SkipModuleResolution) {
            loader.addAutomaticTypeDirectiveTasks();
        }

        loader.filesParser.parse(Ꮡloader, loader.rootTasks);

        loader.projectReferenceFileMapper.loader = default;
        loader.projectReferenceFileMapper.host = default;

        return loader.filesParser.getProcessedFiles(Ꮡloader);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class compilerᴛfileLoader {

internal static tspath.Path toPath(this ref fileLoader p, @string @file) {
    return tspath.ToPath(@file, p.opts.Host.GetCurrentDirectory(), p.opts.Host.FS().UseCaseSensitiveFileNames());
}

internal static void addRootTask(this ref fileLoader p, @string fileName, ж<LibFile> libFile, ж<FileIncludeReason> includeReason) {
    ref var absPath = ref heap<@string>(tspath.GetNormalizedAbsolutePath(fileName, p.opts.Host.GetCurrentDirectory()), out var ᏑabsPath);
    if (p.opts.Config.CompilerOptions().AllowNonTsExtensions.IsTrue() || tspath.HasExtension(absPath)) {
        p.rootTasks = append(p.rootTasks, Ꮡ(new parseTask(
            normalizedFilePath: absPath,
            libFile: libFile,
            includeReason: includeReason
        )));
    }
}

internal static void addRootFileTask(this ref fileLoader p, @string fileName, ж<LibFile> libFile, ж<FileIncludeReason> includeReason) {
    @string currDir = p.opts.Host.GetCurrentDirectory();
    @string absPath = tspath.GetNormalizedAbsolutePath(fileName, currDir);
    @string containingFile = currDir;
    if (p.opts.Config.ConfigFile != nil) {
        containingFile = tspath.GetNormalizedAbsolutePath(p.opts.Config.ConfigFile.SourceFile.FileName(), currDir);
    }
    ref var resolvedFile = ref heap<@string>(out var ᏑresolvedFile);
    (resolvedFile, var diagnostic) = p.getSourceFileFromReference(absPath, fileName, containingFile, includeReason);
    var rootTask = Ꮡ(new parseTask(
        normalizedFilePath: resolvedFile,
        libFile: libFile,
        includeReason: includeReason
    ));
    if (diagnostic != nil) {
        rootTask.normalizedFilePath = absPath;
        rootTask.failedLookup = true;
        rootTask.processingDiagnostics = [Ꮡ(new processingDiagnostic(
            kind: processingDiagnosticKindExplainingFileInclude,
            data: Ꮡ(new includeExplainingDiagnostic(
                diagnosticReason: includeReason,
                message: diagnostic.message,
                args: diagnostic.args
            ))))
        ];
    }
    p.rootTasks = append(p.rootTasks, rootTask);
}

internal static void addAutomaticTypeDirectiveTasks(this ref fileLoader p) {
    @string containingDirectory = default;
    var compilerOptions = p.opts.Config.CompilerOptions();
    if (compilerOptions.ConfigFilePath != ""u8) {
        containingDirectory = tspath.GetDirectoryPath(compilerOptions.ConfigFilePath);
    } else {
        containingDirectory = p.opts.Host.GetCurrentDirectory();
    }
    ref var containingFileName = ref heap<@string>(tspath.CombinePaths(containingDirectory, module.InferredTypesContainingFile), out var ᏑcontainingFileName);
    p.rootTasks = append(p.rootTasks, Ꮡ(new parseTask(
        normalizedFilePath: containingFileName,
        isForAutomaticTypeDirective: true
    )));
}

internal static (slice<resolvedRef> toParse, module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>> typeResolutionsInFile, slice<module.DiagAndArgs> typeResolutionsTrace, slice<ж<processingDiagnostic>> pDiagnostics) resolveAutomaticTypeDirectives(this ref fileLoader p, @string containingFileName) {
    slice<resolvedRef> toParse = default;
    module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>> typeResolutionsInFile = default;
    slice<module.DiagAndArgs> typeResolutionsTrace = default;
    slice<ж<processingDiagnostic>> pDiagnostics = default;
    var pDiagnosticsᴷ = new stackAppend<ж<processingDiagnostic>>(moves: true);

    var automaticTypeDirectiveNames = module.GetAutomaticTypeDirectiveNames(p.opts.Config.CompilerOptions(), new CompilerHostᴠResolutionHost(p.opts.Host));
    if (len(automaticTypeDirectiveNames) != 0) {
        toParse = new slice<resolvedRef>(0, len(automaticTypeDirectiveNames));
        typeResolutionsInFile = new module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>>(len(automaticTypeDirectiveNames));
        foreach (var (_, vᴛ1) in range(automaticTypeDirectiveNames)) {
            ref var name = ref heap(new @string(), out var Ꮡname);
            name = vᴛ1;

            var resolutionMode = core.ResolutionModeNone;
            var (resolved, trace) = p.resolver.ResolveTypeReferenceDirective(name, containingFileName, resolutionMode, default);
            Action traceDone = default;
            if (p.opts.Tracing != nil) {
                traceDone = p.opts.Tracing.Push(tracing.PhaseProgram, "processTypeReferenceDirective", new map<@string, any>{["directive"] = name, ["hasResolved"] = resolved.IsResolved(), ["refKind"] = (nint)fileIncludeKindAutomaticTypeDirectiveFile}, false);
            }
            typeResolutionsInFile[new module.ModeAwareCacheKey(Name: name, Mode: resolutionMode)] = resolved;
            typeResolutionsTrace = appendꓸꓸꓸ(typeResolutionsTrace, trace);
            if (resolved.IsResolved()) {
                toParse = append(toParse, new resolvedRef(
                    fileName: resolved.ResolvedFileName,
                    increaseDepth: resolved.IsExternalLibraryImport,
                    elideOnDepth: false,
                    includeReason: Ꮡ(new FileIncludeReason(
                        kind: fileIncludeKindAutomaticTypeDirectiveFile,
                        data: Ꮡ(new automaticTypeDirectiveFileData(name, resolved.PackageId))
                    )),
                    packageId: resolved.PackageId
                ));
            } else {
                pDiagnostics = append(pDiagnosticsᴷ, pDiagnostics, Ꮡ(new processingDiagnostic(
                    kind: processingDiagnosticKindExplainingFileInclude,
                    data: Ꮡ(new includeExplainingDiagnostic(
                        diagnosticReason: Ꮡ(new FileIncludeReason(
                            kind: fileIncludeKindAutomaticTypeDirectiveFile,
                            data: Ꮡ(new automaticTypeDirectiveFileData(typeReference: name))
                        )),
                        message: diagnostics.Cannot_find_type_definition_file_for_0,
                        args: [name]
                    ))
                )));
            }
            if (traceDone != nil) {
                traceDone();
            }
        }
    }
    pDiagnostics = move2heap(pDiagnosticsᴷ, pDiagnostics);
    return (toParse, typeResolutionsInFile, typeResolutionsTrace, pDiagnostics);
}

internal static void addProjectReferenceTasks(this ж<fileLoader> Ꮡp, bool singleThreaded) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.projectReferenceFileMapper = Ꮡ(new projectReferenceFileMapper(
        opts: p.opts,
        host: new CompilerHostᴠResolutionHost(p.opts.Host)
    ));
    var projectReferences = p.opts.Config.ResolvedProjectReferencePaths();
    if (len(projectReferences) == 0) {
        return;
    }

    var parser = Ꮡ(new projectReferenceParser(
        loader: Ꮡp,
        wg: core.NewWorkGroup(singleThreaded)
    ));
    var rootTasks = createProjectReferenceParseTasks(projectReferences);
    parser.parse(rootTasks);
}

internal static void sortLibs(this ж<fileLoader> Ꮡp, slice<ж<ast.SourceFile>> libFiles) {
    slices.SortFunc(libFiles, (ж<ast.SourceFile> f1, ж<ast.SourceFile> f2) => cmp.Compare(Ꮡp.Value.getDefaultLibFilePriority(f1), Ꮡp.Value.getDefaultLibFilePriority(f2)));
}

internal static nint getDefaultLibFilePriority(this ref fileLoader p, ж<ast.SourceFile> Ꮡa) {
    ref var a = ref Ꮡa.DerefOrNull();

    @string defaultLibraryPath = tspath.RemoveTrailingDirectorySeparator(p.defaultLibraryPath);
    @string aFileName = a.FileName();

    if (strings.HasPrefix(aFileName, defaultLibraryPath) && len(aFileName) > len(defaultLibraryPath) && aFileName[len(defaultLibraryPath)] == tspath.DirectorySeparator) {
        @string basename = aFileName.slice(strings.LastIndexByte(aFileName, tspath.DirectorySeparator) + 1);
        if (basename == "lib.d.ts"u8 || basename == "lib.es6.d.ts"u8) {
            return 0;
        }
        @string name = strings.TrimSuffix(strings.TrimPrefix(basename, "lib."), ".d.ts");
        nint index = slices.Index(tsoptions.Libs, name);
        if (index != -1) {
            return index + 1;
        }
    }
    return len(tsoptions.Libs) + 2;
}

internal static ast.SourceFileMetaData loadSourceFileMetaData(this ref fileLoader p, @string fileName) {
    if (p.opts.SkipModuleResolution) {
        return new ast.SourceFileMetaData(
            ImpliedNodeFormat: ast.GetImpliedNodeFormatForFile(fileName, "")
        );
    }

    var packageJsonScope = p.resolver.GetPackageScopeForPath(tspath.GetDirectoryPath(fileName));
    var moduleResolutionKind = p.opts.Config.CompilerOptions().GetModuleResolutionKind();

    @string packageJsonType = default;
    @string packageJsonDirectory = default;
    if (packageJsonScope.Exists()) {
        packageJsonDirectory = packageJsonScope.PackageDirectory;
        var (value, ok) = packageJsonScope.Contents.of(packagejson.PackageJson.ᏑType).GetValue(); if (ok) {
            if (!tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionMts, tspath.ExtensionCts, tspath.ExtensionMjs, tspath.ExtensionCjs]) && core.ModuleResolutionKindNode16 <= moduleResolutionKind && moduleResolutionKind <= core.ModuleResolutionKindNodeNext || strings.Contains(fileName, "/node_modules/")) {
                packageJsonType = value;
            }
        }
    }

    var impliedNodeFormat = ast.GetImpliedNodeFormatForFile(fileName, packageJsonType);
    return new ast.SourceFileMetaData(
        PackageJsonType: packageJsonType,
        PackageJsonDirectory: packageJsonDirectory,
        ImpliedNodeFormat: impliedNodeFormat
    );
}

internal static ж<ast.SourceFile> parseSourceFile(this ж<fileLoader> Ꮡp, ж<parseTask> Ꮡt) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();
        ref var t = ref Ꮡt.DerefOrNull();

        if (p.opts.Tracing != nil) {
            defer(Ꮡp.Value.opts.Tracing.Push(tracing.PhaseParse, "createSourceFile", new map<@string, any>{["path"] = Ꮡt.Value.normalizedFilePath}, true), ref ᒐ);
        }
        tspath.Path path = p.toPath(t.normalizedFilePath);
        var options = p.projectReferenceFileMapper.getCompilerOptionsForFile(new parseTaskжHasFileName(Ꮡt));
        var parseOptions = new ast.SourceFileParseOptions(
            FileName: t.normalizedFilePath,
            Path: path,
            ExternalModuleIndicatorOptions: ast.GetExternalModuleIndicatorOptions(t.normalizedFilePath, options, t.metadata)
        );
        if (tspath.FileExtensionIsOneOf(t.normalizedFilePath, p.contentMapperExtensions)) {
            return Ꮡp.parseContentMappedFile(parseOptions);
        }
        return p.opts.Host.GetSourceFile(parseOptions);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.SourceFile> parseContentMappedFile(this ж<fileLoader> Ꮡp, ast.SourceFileParseOptions opts) {
    ref var p = ref Ꮡp.DerefOrNull();

    var mapper = p.opts.Config.GetContentMapperForFileName(opts.FileName);
    @string label = mapper.DiagnosticName();
    @string transformIdentity = p.getContentMapperTransformIdentity(mapper);
    if (Ꮡp.contentMapperUnavailable(mapper)) {
        return p.emptyContentMappedFile(opts, mapper.Identity(), transformIdentity);
    }
    var (files, err) = p.opts.Host.GetContentMappedSourceFiles(opts, mapper);
    if (err != nil) {
        var sourceFile = p.emptyContentMappedFile(opts, mapper.Identity(), transformIdentity);
        {
            var (transformError, ok) = errors.AsType<ж<contentmapper.TransformError>>(err); if (ok && transformError.Kind == contentmapper.TransformErrorKindInitialize) {
                Ꮡp.recordContentMapperInitializationFailure(mapper, label, new contentmapper.TransformErrorжerror(transformError));
                return sourceFile;
            }
        }
        if (Ꮡp.recordContentMapperFailure(mapper, label)) {
            ж<ast.Diagnostic> diagnostic = default;
            var (problem, ok) = errors.AsType<ж<spanmap.MappingError>>(err); if (ok) {
                diagnostic = contentMapperMappingDiagnostic(sourceFile, label, ref (problem).DerefOrNull());
            } else {
                diagnostic = contentMapperTransformDiagnostic(sourceFile, label, err);
            }
            sourceFile.SetDiagnostics(append(sourceFile.Diagnostics(), diagnostic));
        }
        return sourceFile;
    }
    return files.Canonical;
}

} partial class compiler_package {

internal static ж<ast.Diagnostic> contentMapperTransformDiagnostic(ж<ast.SourceFile> @file, @string label, error err) {
    {
        var (collision, ok) = errors.AsType<ж<contentmapper.SupplementalFileCollisionError>>(err); if (ok) {
            return contentMapperTransformDiagnosticChain(@file, label, diagnostics.Content_mapper_supplemental_output_file_0_conflicts_with_an_existing_file, collision.FileName);
        }
    }
    {
        var (transformError, ok) = errors.AsType<ж<contentmapper.TransformError>>(err); if (ok) {
            switch (transformError.Kind) {
            case var ᴛ when ᴛ == contentmapper.TransformErrorKindInitialize: {
                var (initializeError, okΔ4) = errors.AsType<ж<contentmapper.InitializeError>>(new contentmapper.TransformErrorжerror(transformError)); if (okΔ4) {
                    switch (initializeError.Kind) {
                    case var ᴛ2 when ᴛ2 == contentmapper.InitializeErrorKindPositionEncoding: {
                        return contentMapperTransformDiagnosticChain(@file, label, diagnostics.The_content_mapper_selected_unsupported_position_encoding_0, initializeError.PositionEncoding);
                    }
                    case var ᴛ2 when ᴛ2 == contentmapper.InitializeErrorKindEmptyDiagnosticSource: {
                        return contentMapperTransformDiagnosticChain(@file, label, diagnostics.The_content_mapper_diagnostic_source_must_not_be_empty);
                    }
                    case var ᴛ2 when ᴛ2 == contentmapper.InitializeErrorKindReservedDiagnosticSource: {
                        return contentMapperTransformDiagnosticChain(@file, label, diagnostics.The_content_mapper_diagnostic_source_0_is_reserved_by_TypeScript, initializeError.DiagnosticSource);
                    }}

                }
                return contentMapperTransformDiagnosticChain(@file, label, diagnostics.The_content_mapper_process_could_not_be_started_or_initialized);
            }
            case var ᴛ when ᴛ == contentmapper.TransformErrorKindProject: {
                return contentMapperTransformDiagnosticChain(@file, label, ContentMapperProjectErrorDiagnostic(new contentmapper.TransformErrorжerror(transformError)));
            }
            case var ᴛ when ᴛ == contentmapper.TransformErrorKindRequest: {
                return contentMapperTransformDiagnosticChain(@file, label, diagnostics.The_content_mapper_process_failed_while_handling_the_transform_request);
            }
            case var ᴛ when ᴛ == contentmapper.TransformErrorKindResponse: {
                var (extensionError, okΔ5) = errors.AsType<ж<contentmapper.InvalidVirtualExtensionError>>(new contentmapper.TransformErrorжerror(transformError)); if (okΔ5) {
                    return contentMapperTransformDiagnosticChain(@file, label, diagnostics.The_content_mapper_returned_an_output_with_unsupported_virtual_extension_0, extensionError.Extension);
                }
                var (directiveError, okΔ6) = errors.AsType<ж<contentmapper.DiagnosticDirectiveError>>(new contentmapper.TransformErrorжerror(transformError)); if (okΔ6) {
                    ж<ast.Diagnostic> detail = default;
                    switch (directiveError.Kind) {
                    case var ᴛ2 when ᴛ2 == contentmapper.DiagnosticDirectiveErrorKindInvalidRange: {
                        detail = ast.NewCompilerDiagnostic(diagnostics.Diagnostic_directive_0_returned_by_the_content_mapper_has_an_invalid_range, directiveError.Index);
                        break;
                    }
                    case var ᴛ2 when ᴛ2 == contentmapper.DiagnosticDirectiveErrorKindInvalidPolicy: {
                        detail = ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_returned_a_diagnostic_directive_with_invalid_policy_0, directiveError.Policy);
                        break;
                    }
                    case var ᴛ2 when ᴛ2 == contentmapper.DiagnosticDirectiveErrorKindExpectMissingUnusedDiagnostic: {
                        detail = ast.NewCompilerDiagnostic(diagnostics.Diagnostic_directive_0_returned_by_the_content_mapper_must_specify_unusedExpectDirectiveIndex_when_there_is_not_exactly_one_unusedExpectDirectiveDiagnostics_entry, directiveError.Index);
                        break;
                    }
                    case var ᴛ2 when ᴛ2 == contentmapper.DiagnosticDirectiveErrorKindInvalidUnusedDiagnosticIndex: {
                        detail = ast.NewCompilerDiagnostic(diagnostics.Diagnostic_directive_0_returned_by_the_content_mapper_has_an_invalid_unusedExpectDirectiveIndex, directiveError.Index);
                        break;
                    }
                    case var ᴛ2 when ᴛ2 == contentmapper.DiagnosticDirectiveErrorKindOverlap: {
                        detail = ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_returned_diagnostic_directives_with_overlapping_virtual_ranges);
                        break;
                    }}

                    if (detail != nil) {
                        if (directiveError.SupplementalIndex >= 0) {
                            detail = ast.NewDiagnosticChain(detail, diagnostics.The_invalid_diagnostic_directive_is_in_supplemental_output_0_returned_by_the_content_mapper, directiveError.SupplementalIndex);
                        }
                        return contentMapperTransformDiagnosticWithDetail(@file, label, detail);
                    }
                }
                return contentMapperTransformDiagnosticChain(@file, label, diagnostics.The_content_mapper_returned_an_invalid_transform_response);
            }
            case var ᴛ when ᴛ == contentmapper.TransformErrorKindMappings: {
                return ast.NewDiagnostic(@file, core.NewTextRange(0, 0), diagnostics.The_content_mapper_0_did_not_provide_the_required_position_mappings, label);
            }}

        }
    }
    return ast.NewDiagnostic(@file, core.NewTextRange(0, 0), diagnostics.The_content_mapper_0_failed_to_transform_this_file, label);
}

public static ж<diagnostics.Message> ContentMapperProjectErrorDiagnostic(error err) {
    var (projectError, ok) = errors.AsType<ж<contentmapper.ProjectError>>(err); if (ok) {
        switch (projectError.Kind) {
        case var ᴛ when ᴛ == contentmapper.ProjectErrorKindMalformedResponse: {
            return diagnostics.The_content_mapper_returned_a_project_response_that_could_not_be_decoded;
        }
        case var ᴛ when ᴛ == contentmapper.ProjectErrorKindMissingConfigIdentity: {
            return diagnostics.The_content_mapper_did_not_return_configIdentity_which_is_required_when_the_content_mapper_has_dynamicConfig_Colon_true_in_its_package_json;
        }
        case var ᴛ when ᴛ == contentmapper.ProjectErrorKindNonAbsoluteWatchedFile: {
            return diagnostics.The_content_mapper_returned_a_non_absolute_path_in_watchedFiles;
        }
        case var ᴛ when ᴛ == contentmapper.ProjectErrorKindUnexpectedConfigIdentity: {
            return diagnostics.The_content_mapper_returned_configIdentity_which_is_only_allowed_when_it_declares_dynamicConfig_Colon_true_in_its_package_json;
        }
        case var ᴛ when ᴛ == contentmapper.ProjectErrorKindUnexpectedWatchedFiles: {
            return diagnostics.The_content_mapper_returned_watchedFiles_which_is_only_allowed_when_it_declares_dynamicConfig_Colon_true_in_its_package_json;
        }}

    }
    return diagnostics.The_content_mapper_process_failed_while_handling_the_project_request;
}

internal static ж<ast.Diagnostic> contentMapperTransformDiagnosticChain(ж<ast.SourceFile> @file, @string label, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    return contentMapperTransformDiagnosticWithDetail(@file, label, ast.NewCompilerDiagnostic(message, args.ꓸꓸꓸ));
}

internal static ж<ast.Diagnostic> contentMapperTransformDiagnosticWithDetail(ж<ast.SourceFile> @file, @string label, ж<ast.Diagnostic> detail) {
    return ast.NewDiagnostic(
        @file,
        core.NewTextRange(0, 0),
        diagnostics.The_content_mapper_0_failed_to_transform_this_file,
        label).AddMessageChain(detail);
}

internal static ж<ast.Diagnostic> contentMapperMappingDiagnostic(ж<ast.SourceFile> @file, @string label, ref spanmap.MappingError problem) {
    var loc = core.NewTextRange(0, 0);
    switch (problem.Kind) {
    case var ᴛ when ᴛ == spanmap.MappingErrorKindOverlap: {
        return ast.NewDiagnostic(@file, loc, diagnostics.The_content_mapper_0_produced_overlapping_or_out_of_order_position_mappings_near_virtual_offset_1, label, (nint)problem.VirtualPos);
    }
    case var ᴛ when ᴛ == spanmap.MappingErrorKindOutOfBounds: {
        return ast.NewDiagnostic(@file, loc, diagnostics.The_content_mapper_0_produced_a_position_mapping_that_points_outside_the_original_content_original_offset_1, label, (nint)problem.OriginalPos);
    }
    case var ᴛ when ᴛ == spanmap.MappingErrorKindVerbatimMismatch: {
        return ast.NewDiagnostic(@file, loc, diagnostics.The_content_mapper_0_produced_a_verbatim_mapping_that_does_not_match_the_original_content_virtual_offset_1_original_offset_2, label, (nint)problem.VirtualPos, (nint)problem.OriginalPos);
    }
    case var ᴛ when ᴛ == spanmap.MappingErrorKindKind: {
        return ast.NewDiagnostic(@file, loc, diagnostics.The_content_mapper_0_produced_a_position_mapping_with_an_invalid_kind_near_virtual_offset_1, label, (nint)problem.VirtualPos);
    }
    case var ᴛ when ᴛ == spanmap.MappingErrorKindFeature: {
        return ast.NewDiagnostic(@file, loc, diagnostics.The_content_mapper_0_produced_invalid_mapping_features_near_original_offset_1, label, (nint)problem.OriginalPos);
    }
    default: {
        return ast.NewDiagnostic(@file, loc, diagnostics.The_content_mapper_0_did_not_provide_the_required_position_mappings, label);
    }}

}

} partial class compilerᴛfileLoader {

internal static @string getContentMapperTransformIdentity(this ref fileLoader p, ж<contentmapper.Mapper> Ꮡmapper) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    var project = p.opts.Host.ContentMapperProject(); if (project != nil) {
        var (identity, err) = project.Identity(Ꮡmapper); if (err == nil) {
            return identity;
        }
    }
    return fmt.Sprintf("%x"u8, mapper.TransformIdentity(p.opts.Config.CompilerOptions()).Bytes());
}

internal static ж<ast.SourceFile> emptyContentMappedFile(this ref fileLoader p, ast.SourceFileParseOptions opts, @string mapperIdentity, @string transformIdentity) {
    var (content, _) = p.opts.Host.FS().ReadFile(opts.FileName);
    var sourceFile = parser.ParseSourceFile(opts, "", core.ScriptKindTS);
    sourceFile.SetContentMapperInfo(new ast.ContentMapperSourceFileInfo(
        ContentMapper: mapperIdentity,
        TransformIdentity: transformIdentity,
        ParseOptions: opts,
        VirtualFileName: opts.FileName + tspath.ExtensionTs,
        OriginalText: content
    ));
    return sourceFile;
}

} partial class compiler_package {

public static ж<ast.Diagnostic> ContentMapperInitializationDiagnostic(@string label, error err) {
    {
        var (initializeError, ok) = errors.AsType<ж<contentmapper.InitializeError>>(err); if (ok && label == ""u8) {
            label = initializeError.MapperName;
        }
    }
    var diagnostic = ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_0_could_not_be_initialized, label);
    {
        var (initializeError, ok) = errors.AsType<ж<contentmapper.InitializeError>>(err); if (ok) {
            switch (initializeError.Kind) {
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindProcessStart: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_command_0_could_not_be_started_Colon_1, initializeError.Command, initializeError.Detail));
            }
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindProcessExit: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_process_exited_before_responding_to_the_initialize_request_exit_code_0, initializeError.ExitCode));
            }
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindNoResponse: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_did_not_respond_to_the_initialize_request_within_0_seconds, initializeError.TimeoutSeconds));
            }
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindInvalidResponse: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_returned_an_initialize_response_that_could_not_be_decoded_Colon_0, initializeError.Detail));
            }
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindRequest: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_s_initialize_request_failed_Colon_0, initializeError.Detail));
            }
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindPositionEncoding: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_selected_unsupported_position_encoding_0, initializeError.PositionEncoding));
            }
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindEmptyDiagnosticSource: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_diagnostic_source_must_not_be_empty));
            }
            case var ᴛ when ᴛ == contentmapper.InitializeErrorKindReservedDiagnosticSource: {
                return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_diagnostic_source_0_is_reserved_by_TypeScript, initializeError.DiagnosticSource));
            }}

        }
    }
    return diagnostic.AddMessageChain(ast.NewCompilerDiagnostic(diagnostics.The_content_mapper_process_could_not_be_started_or_initialized));
}

public static ж<ast.Diagnostic> ContentMapperProjectDiagnostic(error err) {
    var (_, ok) = errors.AsType<ж<contentmapper.InitializeError>>(err); if (ok) {
        return ContentMapperInitializationDiagnostic("", err);
    }
    return ast.NewCompilerDiagnostic(ContentMapperProjectErrorDiagnostic(err));
}

} partial class compilerᴛfileLoader {

internal static bool contentMapperUnavailable(this ж<fileLoader> Ꮡp, ж<contentmapper.Mapper> mapper) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        if (mapper == nil) {
            return false;
        }
        p.contentMapperMu.Lock();
        ᒐd1 = true;
        return Ꮡp.ᏑcontentMapperInitFailed.Has(mapper) || p.contentMapperFailures[mapper] >= maxContentMapperFailures;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡp.DerefOrNull().contentMapperMu.Unlock(); ᒐ.Run(); }
}

internal static void recordContentMapperInitializationFailure(this ж<fileLoader> Ꮡp, ж<contentmapper.Mapper> mapper, @string label, error err) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.contentMapperMu.Lock();
        ᒐd1 = true;
        if (Ꮡp.ᏑcontentMapperInitFailed.Has(mapper)) {
            return;
        }
        p.contentMapperInitFailed.Add(mapper);
        p.contentMapperDiagnostics = append(p.contentMapperDiagnostics, ContentMapperInitializationDiagnostic(label, err));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡp.DerefOrNull().contentMapperMu.Unlock(); ᒐ.Run(); }
}

internal static bool recordContentMapperFailure(this ж<fileLoader> Ꮡp, ж<contentmapper.Mapper> mapper, @string label) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.contentMapperMu.Lock();
        ᒐd1 = true;
        if (p.contentMapperFailures == nil) {
            p.contentMapperFailures = new map<ж<contentmapper.Mapper>, nint>();
        }
        if (p.contentMapperFailures[mapper] >= maxContentMapperFailures) {
            return false;
        }
        p.contentMapperFailures[mapper]++;
        if (p.contentMapperFailures[mapper] >= maxContentMapperFailures) {
            p.contentMapperDiagnostics = append(p.contentMapperDiagnostics, ast.NewCompilerDiagnostic(
                diagnostics.The_content_mapper_0_failed_1_times_and_will_not_be_used,
                label,
                (nint)(maxContentMapperFailures)));
        }
        return true;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡp.DerefOrNull().contentMapperMu.Unlock(); ᒐ.Run(); }
}

internal static bool isSupportedExtension(this ref fileLoader p, @string canonicalFileName) {
    foreach (var (_, group) in range(p.supportedExtensionsWithJsonIfResolveJsonModule)) {
        if (tspath.FileExtensionIsOneOf(canonicalFileName, group)) {
            return true;
        }
    }
    return false;
}

internal static (@string, ж<sourceFileFromReferenceDiagnostic>) getSourceFileFromReference(this ref fileLoader p, @string fileName, @string referenceText, @string containingFile, ж<FileIncludeReason> includeReason) {
    var options = p.opts.Config.CompilerOptions();
    var allowNonTsExtensions = options.AllowNonTsExtensions.IsTrue();
    @string diagnosticFileName = tspath.NormalizeSlashes(referenceText);

    if (tspath.HasExtension(fileName)) {
        @string canonicalFileName = tspath.GetCanonicalFileName(fileName, p.opts.Host.FS().UseCaseSensitiveFileNames());
        if (!allowNonTsExtensions && !p.isSupportedExtension(canonicalFileName)) {
            if (tspath.HasJSFileExtension(canonicalFileName)) {
                return ("", Ꮡ(new sourceFileFromReferenceDiagnostic(message: diagnostics.File_0_is_a_JavaScript_file_Did_you_mean_to_enable_the_allowJs_option, args: [diagnosticFileName])));
            }
            return ("", Ꮡ(new sourceFileFromReferenceDiagnostic(message: diagnostics.File_0_has_an_unsupported_extension_The_only_supported_extensions_are_1, args: slice<any>([diagnosticFileName, "'" + strings.Join(core.Flatten(p.supportedExtensions), "', '") + "'"]))));
        }

        if (!p.opts.Host.FS().FileExists(fileName)) {
            return ("", Ꮡ(new sourceFileFromReferenceDiagnostic(message: diagnostics.File_0_not_found, args: [diagnosticFileName])));
        }

        if (includeReason.isReferencedFile() && tspath.GetCanonicalFileName(containingFile, p.opts.Host.FS().UseCaseSensitiveFileNames()) == canonicalFileName) {
            return ("", Ꮡ(new sourceFileFromReferenceDiagnostic(message: diagnostics.A_file_cannot_have_a_reference_to_itself)));
        }
        return (fileName, default);
    }

    if (allowNonTsExtensions && p.opts.Host.FS().FileExists(fileName)) {
        return (fileName, default);
    }

    if (allowNonTsExtensions) {
        return ("", Ꮡ(new sourceFileFromReferenceDiagnostic(message: diagnostics.File_0_not_found, args: [diagnosticFileName])));
    }

    foreach (var (_, ext) in range(p.supportedExtensions[0])) {
        @string candidate = fileName + ext;
        if (p.opts.Host.FS().FileExists(candidate)) {
            return (candidate, default);
        }
    }

    return ("", Ꮡ(new sourceFileFromReferenceDiagnostic(message: diagnostics.Could_not_resolve_the_path_0_with_the_extensions_Colon_1, args: slice<any>([diagnosticFileName, "'" + strings.Join(core.Flatten(p.supportedExtensions), "', '") + "'"]))));
}

internal static (ж<resolvedRef>, ж<processingDiagnostic>) resolveTripleslashPathReference(this ref fileLoader p, @string moduleName, @string containingFile, nint index) {
    @string basePath = tspath.GetDirectoryPath(containingFile);
    @string referencedFileName = moduleName;

    if (!tspath.IsRootedDiskPath(moduleName)) {
        referencedFileName = tspath.CombinePaths(basePath, moduleName);
    }
    @string normalizedFileName = tspath.NormalizePath(referencedFileName);
    var includeReason = Ꮡ(new FileIncludeReason(
        kind: fileIncludeKindReferenceFile,
        data: Ꮡ(new referencedFileData(
            @file: p.toPath(containingFile),
            index: index
        ))
    ));

    ref var resolvedFileName = ref heap<@string>(out var ᏑresolvedFileName);
    (resolvedFileName, var diagnostic) = p.getSourceFileFromReference(
        normalizedFileName,
        moduleName,
        containingFile,
        includeReason);
    if (diagnostic != nil) {
        return (default, Ꮡ(new processingDiagnostic(
            kind: processingDiagnosticKindExplainingFileInclude,
            data: Ꮡ(new includeExplainingDiagnostic(
                diagnosticReason: includeReason,
                message: diagnostic.message,
                args: diagnostic.args
            ))
        )));
    }

    return (Ꮡ(new resolvedRef(
        fileName: resolvedFileName,
        includeReason: includeReason
    )), default);
}

internal static void resolveTypeReferenceDirectives(this ж<fileLoader> Ꮡp, ж<parseTask> Ꮡt) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();
        ref var t = ref Ꮡt.DerefOrNull();

        var @file = t.@file;
        if (len(@file.TypeReferenceDirectives) == 0) {
            return;
        }
        if (p.opts.Tracing != nil) {
            var fileʗ1 = @file;
            defer(Ꮡp.Value.opts.Tracing.Push(tracing.PhaseProgram, "resolveTypeReferenceDirectiveNamesWorker", new map<@string, any>{["containingFileName"] = fileʗ1.FileName()}, false), ref ᒐ);
        }
        var meta = t.metadata;

        var typeResolutionsInFile = new module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>>(len(@file.TypeReferenceDirectives));
        slice<module.DiagAndArgs> typeResolutionsTrace = default;
        foreach (var (iᴛ1, @ref) in range(@file.TypeReferenceDirectives)) {
            ref var index = ref heap(new nint(), out var Ꮡindex);
            index = iᴛ1;

            var (redirect, fileName) = p.projectReferenceFileMapper.getRedirectForResolution(new ast.SourceFileжHasFileName(@file));
            var resolutionMode = getModeForTypeReferenceDirectiveInFile(ref (@ref).DerefOrNull(), @file, meta, module.GetCompilerOptionsWithRedirect(p.opts.Config.CompilerOptions(), new tsoptions.ParsedCommandLineжResolvedProjectReference(redirect)));
            var (resolved, trace) = p.resolver.ResolveTypeReferenceDirective(@ref.FileName, fileName, resolutionMode, new tsoptions.ParsedCommandLineжResolvedProjectReference(redirect));
            Action traceDone = default;
            if (p.opts.Tracing != nil) {
                traceDone = p.opts.Tracing.Push(tracing.PhaseProgram, "processTypeReferenceDirective", new map<@string, any>{["directive"] = @ref.FileName, ["hasResolved"] = resolved.IsResolved(), ["refKind"] = (nint)fileIncludeKindTypeReferenceDirective, ["refPath"] = ((@string)t.path)}, false);
            }
            typeResolutionsInFile[new module.ModeAwareCacheKey(Name: @ref.FileName, Mode: resolutionMode)] = resolved;
            var includeReason = Ꮡ(new FileIncludeReason(
                kind: fileIncludeKindTypeReferenceDirective,
                data: Ꮡ(new referencedFileData(
                    @file: t.path,
                    index: index
                ))
            ));
            typeResolutionsTrace = appendꓸꓸꓸ(typeResolutionsTrace, trace);

            if (resolved.IsResolved()) {
                t.addSubTask(new resolvedRef(
                    fileName: resolved.ResolvedFileName,
                    increaseDepth: resolved.IsExternalLibraryImport,
                    elideOnDepth: false,
                    includeReason: includeReason,
                    packageId: resolved.PackageId
                ), nil);
            } else {
                t.processingDiagnostics = append(t.processingDiagnostics, Ꮡ(new processingDiagnostic(
                    kind: processingDiagnosticKindUnknownReference,
                    data: includeReason.OrTypedNil()
                )));
            }
            if (traceDone != nil) {
                traceDone();
            }
        }

        t.typeResolutionsInFile = typeResolutionsInFile;
        t.typeResolutionsTrace = typeResolutionsTrace;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

internal static readonly @string externalHelpersModuleNameText = "tslib"u8;

} partial class compilerᴛfileLoader {

internal static void resolveImportsAndModuleAugmentations(this ж<fileLoader> Ꮡp, ж<parseTask> Ꮡt) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();
        ref var t = ref Ꮡt.DerefOrNull();
        var moduleNamesᴷ = new stackAppend<ж<ast.Node>>();

        if (p.opts.Tracing != nil) {
            defer(Ꮡp.Value.opts.Tracing.Push(tracing.PhaseProgram, "resolveModuleNamesWorker", new map<@string, any>{["containingFileName"] = Ꮡt.Value.@file.FileName()}, false), ref ᒐ);
        }
        var @file = t.@file;
        var meta = t.metadata;

        var moduleNames = new slice<ж<ast.Node>>(0, len(@file.Imports()) + len(@file.ModuleAugmentations) + 2);

        var isJavaScriptFile = ast.IsSourceFileJS(@file);
        var isExternalModuleFile = ast.IsExternalModule(@file);

        var (redirect, fileName) = p.projectReferenceFileMapper.getRedirectForResolution(new ast.SourceFileжHasFileName(@file));
        var optionsForFile = module.GetCompilerOptionsWithRedirect(p.opts.Config.CompilerOptions(), new tsoptions.ParsedCommandLineжResolvedProjectReference(redirect));
        if (isJavaScriptFile || (!@file.IsDeclarationFile && (optionsForFile.GetIsolatedModules() || isExternalModuleFile))) {
            if (optionsForFile.ImportHelpers.IsTrue()) {
                var specifier = Ꮡp.createSyntheticImport(externalHelpersModuleNameText, @file);
                moduleNames = append(moduleNamesᴷ, moduleNames, specifier);
                t.importHelpersImportSpecifier = specifier;
            }
        }

        if (isJavaScriptFile || @file.ScriptKind == core.ScriptKindTSX) {
            ref var jsxImport = ref heap<@string>(ast.GetJSXRuntimeImport(ast.GetJSXImplicitImportBase(optionsForFile, @file), optionsForFile), out var ᏑjsxImport);
            if (jsxImport != ""u8) {
                var specifier = Ꮡp.createSyntheticImport(jsxImport, @file);
                moduleNames = append(moduleNames, specifier);
                t.jsxRuntimeImportSpecifier = Ꮡ(new jsxRuntimeImportSpecifier(
                    moduleReference: jsxImport,
                    specifier: specifier
                ));
            }
        }

        nint importsStart = len(moduleNames);

        moduleNames = appendꓸꓸꓸ(moduleNames, @file.Imports());
        foreach (var (_, imp) in range(@file.ModuleAugmentations)) {
            if (imp.Kind == ast.KindStringLiteral) {
                moduleNames = append(moduleNames, imp);
            }
        }

        if (p.opts.SkipModuleResolution) {
            return;
        }

        if (len(moduleNames) != 0) {
            var resolutionsInFile = new module.ModeAwareCache<ж<module.ResolvedModule>>(len(moduleNames));
            slice<module.DiagAndArgs> resolutionsTrace = default;

            foreach (var (index, entry) in range(moduleNames)) {
                @string moduleName = entry.Text();
                if (moduleName == ""u8) {
                    continue;
                }

                var mode = getModeForUsageLocation(@file.FileName(), meta, ref (entry).DerefOrNull(), optionsForFile);
                var (resolvedModule, trace) = p.resolver.ResolveModuleName(moduleName, fileName, mode, new tsoptions.ParsedCommandLineжResolvedProjectReference(redirect));
                resolutionsInFile[new module.ModeAwareCacheKey(Name: moduleName, Mode: mode)] = resolvedModule;
                resolutionsTrace = appendꓸꓸꓸ(resolutionsTrace, trace);

                if (!resolvedModule.IsResolved()) {
                    continue;
                }

                @string resolvedFileName = resolvedModule.ResolvedFileName;
                var isFromNodeModulesSearch = resolvedModule.IsExternalLibraryImport;
                var isJsFile = !resolvedModule.ResolvedUsingExtraExtensions && !tspath.FileExtensionIsOneOf(resolvedFileName, tspath.SupportedTSExtensionsWithJsonFlat) && p.projectReferenceFileMapper.getRedirectParsedCommandLineForResolution(ast.NewHasFileName(resolvedFileName, p.toPath(resolvedFileName))) == nil;
                var isJsFileFromNodeModules = isFromNodeModulesSearch && isJsFile && strings.Contains(resolvedFileName, "/node_modules/");

                ref var importIndex = ref heap<nint>(index - importsStart, out var ᏑimportIndex);

                var shouldAddFile = moduleName != ""u8 && module.GetResolutionDiagnostic(optionsForFile, resolvedModule, @file) == nil && !optionsForFile.NoResolve.IsTrue() && !(isJsFile && !optionsForFile.GetAllowJS()) && (importIndex < 0 || (importIndex < len(@file.Imports()) && (ast.IsInJSFile(@file.Imports()[importIndex]) || (ast.NodeFlags)(@file.Imports()[importIndex].Flags & ast.NodeFlagsJSDoc) == 0)));

                if (shouldAddFile) {
                    t.addSubTask(new resolvedRef(
                        fileName: resolvedFileName,
                        increaseDepth: resolvedModule.IsExternalLibraryImport,
                        elideOnDepth: isJsFileFromNodeModules,
                        includeReason: Ꮡ(new FileIncludeReason(
                            kind: fileIncludeKindImport,
                            data: Ꮡ(new referencedFileData(
                                @file: t.path,
                                index: importIndex,
                                synthetic: core.IfElse(importIndex < 0, entry, (ж<ast.Node>)(nil))
                            ))
                        )),
                        packageId: resolvedModule.PackageId
                    ), nil);
                }
            }

            t.resolutionsInFile = resolutionsInFile;
            t.resolutionsTrace = resolutionsTrace;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static ж<astꓸStringLiteralNode> createSyntheticImport(this ж<fileLoader> Ꮡp, @string text, ж<ast.SourceFile> @file) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        p.factoryMu.Lock();
        ᒐd1 = true;
        var externalHelpersModuleReference = p.factory.NewStringLiteral(text, ast.TokenFlagsNone);
        var importDecl = p.factory.NewImportDeclaration(nil, nil, externalHelpersModuleReference, nil);
        externalHelpersModuleReference.Parent = importDecl;
        importDecl.Parent = @file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        return externalHelpersModuleReference;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡp.DerefOrNull().factoryMu.Unlock(); ᒐ.Run(); }
}

internal static ж<LibFile> pathForLibFile(this ж<fileLoader> Ꮡp, @string name) {
    ref var p = ref Ꮡp.DerefOrNull();

    var (cached, ok) = Ꮡp.ᏑpathForLibFileCache.Load(name); if (ok) {
        return cached;
    }

    ref var path = ref heap<@string>(tspath.CombinePaths(p.defaultLibraryPath, name), out var Ꮡpath);
    ref var replaced = ref heap<bool>(false, out var Ꮡreplaced);
    if (!p.opts.SkipModuleResolution && p.opts.Config.CompilerOptions().LibReplacement.IsTrue() && name != "lib.d.ts"u8) {
        ref var libraryName = ref heap<@string>(getLibraryNameFromLibFileName(name), out var ᏑlibraryName);
        @string resolveFrom = getInferredLibraryNameResolveFrom(ref (p.opts.Config.CompilerOptions()).DerefOrNull(), p.opts.Host.GetCurrentDirectory(), name);
        var (resolution, trace) = Ꮡp.resolveLibrary(libraryName, resolveFrom);
        if (resolution.IsResolved()) {
            path = resolution.ResolvedFileName;
            replaced = true;
        }
        Ꮡp.ᏑpathForLibFileResolutions.LoadOrStore(p.toPath(resolveFrom), Ꮡ(new libResolution(
            libraryName: libraryName,
            resolution: resolution,
            trace: trace
        )));
    }

    var (libPath, _) = Ꮡp.ᏑpathForLibFileCache.LoadOrStore(name, Ꮡ(new LibFile(name, path, replaced)));
    return libPath;
}

internal static (ж<module.ResolvedModule>, slice<module.DiagAndArgs>) resolveLibrary(this ж<fileLoader> Ꮡp, @string libraryName, @string resolveFrom) {
    GoFrame ᒐ = default;
    try {
        ref var p = ref Ꮡp.DerefOrNull();

        {
            var tr = p.opts.Tracing; if (tr != nil) {
                var trʗ1 = tr;
                defer(trʗ1.Push(tracing.PhaseProgram, "resolveLibrary", new map<@string, any>{["resolveFrom"] = resolveFrom}, false), ref ᒐ);
            }
        }
        return p.resolver.ResolveModuleName(libraryName, resolveFrom, core.ModuleKindCommonJS, default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

internal static @string getLibraryNameFromLibFileName(@string libFileName) {
    var components = strings.Split(libFileName, ".");
    ref var path = ref heap(new strings.Builder(), out var Ꮡpath);
    Ꮡpath.WriteString("@typescript/lib-");
    if (len(components) > 1) {
        Ꮡpath.WriteString(components[1]);
    }
    nint i = 2;
    while (i < len(components) && components[i] != "" && components[i] != "d") {
        if (i == 2) {
            Ꮡpath.WriteByte((byte)'/');
        } else {
            Ꮡpath.WriteByte((byte)'-');
        }
        Ꮡpath.WriteString(components[i]);
        i++;
    }
    return path.String();
}

internal static @string getInferredLibraryNameResolveFrom(ref core.CompilerOptions options, @string currentDirectory, @string libFileName) {
    @string containingDirectory = default;
    if (options.ConfigFilePath != ""u8) {
        containingDirectory = tspath.GetDirectoryPath(options.ConfigFilePath);
    } else {
        containingDirectory = currentDirectory;
    }
    return tspath.CombinePaths(containingDirectory, "__lib_node_modules_lookup_"u8 + libFileName + "__.ts"u8);
}

internal static coreꓸResolutionMode getModeForTypeReferenceDirectiveInFile(ref ast.FileReference @ref, ж<ast.SourceFile> Ꮡfile, ast.SourceFileMetaData meta, ж<core.CompilerOptions> options) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (@ref.ResolutionMode != core.ResolutionModeNone) {
        return @ref.ResolutionMode;
    } else {
        return getDefaultResolutionModeForFile(@file.FileName(), meta, options);
    }
}

internal static coreꓸResolutionMode getDefaultResolutionModeForFile(@string fileName, ast.SourceFileMetaData meta, ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (importSyntaxAffectsModuleResolution(Ꮡoptions)) {
        return ast.GetImpliedNodeFormatForEmitWorker(fileName, options.GetEmitModuleKind(), meta);
    } else {
        return core.ResolutionModeNone;
    }
}

internal static coreꓸResolutionMode getModeForUsageLocation(@string fileName, ast.SourceFileMetaData meta, ref astꓸStringLiteralLike usage, ж<core.CompilerOptions> options) {
    if (ast.IsImportDeclaration(usage.Parent) || usage.Parent.Kind == ast.KindJSImportDeclaration || ast.IsExportDeclaration(usage.Parent) || ast.IsJSDocImportTag(usage.Parent)) {
        var isTypeOnly = ast.IsExclusivelyTypeOnlyImportOrExport(usage.Parent);
        if (isTypeOnly) {
            coreꓸResolutionMode @override = default;
            bool ok = default;
            switch (usage.Parent.Kind) {
            case var ᴛ when ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindJSImportDeclaration: {
                (@override, ok) = (~usage.Parent.AsImportDeclaration()).Attributes.GetResolutionModeOverride(default);
                break;
            }
            case var ᴛ when ᴛ == ast.KindExportDeclaration: {
                (@override, ok) = (~usage.Parent.AsExportDeclaration()).Attributes.GetResolutionModeOverride(default);
                break;
            }
            case var ᴛ when ᴛ == ast.KindJSDocImportTag: {
                (@override, ok) = (~usage.Parent.AsJSDocImportTag()).Attributes.GetResolutionModeOverride(default);
                break;
            }}

            if (ok) {
                return @override;
            }
        }
    }
    if (ast.IsLiteralTypeNode(usage.Parent) && ast.IsImportTypeNode(usage.Parent.Parent)) {
        var (@override, ok) = (~usage.Parent.Parent.AsImportTypeNode()).Attributes.GetResolutionModeOverride(default); if (ok) {
            return @override;
        }
    }

    if (options != nil && importSyntaxAffectsModuleResolution(options)) {
        return getEmitSyntaxForUsageLocationWorker(fileName, meta, ref usage, options);
    }

    return core.ResolutionModeNone;
}

internal static bool importSyntaxAffectsModuleResolution(ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    var moduleResolution = options.GetModuleResolutionKind();
    return core.ModuleResolutionKindNode16 <= moduleResolution && moduleResolution <= core.ModuleResolutionKindNodeNext || options.GetResolvePackageJsonExports() || options.GetResolvePackageJsonImports();
}

internal static coreꓸResolutionMode getEmitSyntaxForUsageLocationWorker(@string fileName, ast.SourceFileMetaData meta, ref ast.Node usage, ж<core.CompilerOptions> options) {
    if (ast.IsRequireCall(usage.Parent, false) || ast.IsExternalModuleReference(usage.Parent) && ast.IsImportEqualsDeclaration(usage.Parent.Parent)) {
        return core.ModuleKindCommonJS;
    }
    var fileEmitMode = ast.GetEmitModuleFormatOfFileWorker(fileName, options, meta);
    if (ast.IsImportCall(ast.WalkUpParenthesizedExpressions(usage.Parent))) {
        if (ast.ShouldTransformImportCall(fileName, options, fileEmitMode)) {
            return core.ModuleKindCommonJS;
        } else {
            return core.ModuleKindESNext;
        }
    }
    if (fileEmitMode == core.ModuleKindCommonJS) {
        return core.ModuleKindCommonJS;
    } else {
        if (fileEmitMode.IsNonNodeESM() || fileEmitMode == core.ModuleKindPreserve) {
            return core.ModuleKindESNext;
        }
    }
    return core.ModuleKindNone;
}

} // end compiler_package
