namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using math = math_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.sync;
using iter = iter_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using static compiler_package;

partial class compiler_package {

partial struct parseTask {
    internal @string normalizedFilePath;
    internal tspath.Path path;
    internal ж<ast.SourceFile> @file;
    internal ж<LibFile> libFile;
    internal ж<parseTask> redirectedParseTask;
    internal slice<ж<parseTask>> subTasks;
    internal bool loaded;
    internal bool startedSubTasks;
    internal bool isForAutomaticTypeDirective;
    internal bool isContentMapperSupplemental;
    internal bool failedLookup;
    internal ж<FileIncludeReason> includeReason;
    internal module.PackageId packageId;
    internal ast.SourceFileMetaData metadata;
    internal module.ModeAwareCache<ж<module.ResolvedModule>> resolutionsInFile;
    internal slice<module.DiagAndArgs> resolutionsTrace;
    internal module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>> typeResolutionsInFile;
    internal slice<module.DiagAndArgs> typeResolutionsTrace;
    internal slice<ж<ast.Diagnostic>> resolutionDiagnostics;
    internal slice<ж<processingDiagnostic>> processingDiagnostics;
    internal ж<astꓸStringLiteralNode> importHelpersImportSpecifier;
    internal ж<jsxRuntimeImportSpecifier> jsxRuntimeImportSpecifier;
    internal bool increaseDepth;
    internal bool elideOnDepth;
    internal ж<parseTask> loadedTask;
    internal slice<ж<FileIncludeReason>> allIncludeReasons;
}

} partial class compilerᴛparseTask {

internal static @string FileName(this ref parseTask t) {
    return t.normalizedFilePath;
}

internal static tspath.Path Path(this ref parseTask t) {
    return t.path;
}

internal static void load(this ж<parseTask> Ꮡt, ж<fileLoader> Ꮡloader) {
    GoFrame ᒐ = default;
    try {
        ref var t = ref Ꮡt.DerefOrNull();
        ref var loader = ref Ꮡloader.DerefOrNull();

        t.loaded = true;
        if (t.isForAutomaticTypeDirective) {
            Ꮡt.loadAutomaticTypeDirectives(Ꮡloader);
            return;
        }
        if (t.failedLookup) {
            return;
        }
        if (loader.opts.Tracing != nil) {
            defer(Ꮡloader.Value.opts.Tracing.Push(tracing.PhaseProgram, "findSourceFile", new map<@string, any>{["fileName"] = Ꮡt.Value.normalizedFilePath}, false), ref ᒐ);
        }
        @string redirect = loader.projectReferenceFileMapper.getParseFileRedirect(new parseTaskжHasFileName(Ꮡt));
        if (redirect != ""u8) {
            t.redirect(Ꮡloader, redirect);
            return;
        }

        if (!t.isContentMapperSupplemental && tspath.HasExtension(t.normalizedFilePath)) {
            var compilerOptionsΔ1 = loader.opts.Config.CompilerOptions();
            var allowNonTsExtensions = compilerOptionsΔ1.AllowNonTsExtensions.IsTrue();
            if (!allowNonTsExtensions) {
                @string canonicalFileName = tspath.GetCanonicalFileName(t.normalizedFilePath, loader.opts.Host.FS().UseCaseSensitiveFileNames());
                if (!loader.isSupportedExtension(canonicalFileName)) {
                    if (tspath.HasJSFileExtension(canonicalFileName)) {
                        t.processingDiagnostics = append(t.processingDiagnostics, Ꮡ(new processingDiagnostic(
                            kind: processingDiagnosticKindExplainingFileInclude,
                            data: Ꮡ(new includeExplainingDiagnostic(
                                diagnosticReason: t.includeReason,
                                message: diagnostics.File_0_is_a_JavaScript_file_Did_you_mean_to_enable_the_allowJs_option,
                                args: [t.normalizedFilePath]
                            ))
                        )));
                    } else {
                        t.processingDiagnostics = append(t.processingDiagnostics, Ꮡ(new processingDiagnostic(
                            kind: processingDiagnosticKindExplainingFileInclude,
                            data: Ꮡ(new includeExplainingDiagnostic(
                                diagnosticReason: t.includeReason,
                                message: diagnostics.File_0_has_an_unsupported_extension_The_only_supported_extensions_are_1,
                                args: slice<any>([t.normalizedFilePath, "'" + strings.Join(core.Flatten(loader.supportedExtensions), "', '") + "'"])
                            ))
                        )));
                    }
                    return;
                }
            }
        }

        Ꮡloader.ᏑtotalFileCount.Add(1);
        if (t.libFile != nil) {
            Ꮡloader.ᏑlibFileCount.Add(1);
            t.metadata = new ast.SourceFileMetaData(ImpliedNodeFormat: core.ResolutionModeCommonJS);
        } else {
            t.metadata = loader.loadSourceFileMetaData(t.normalizedFilePath);
        }

        var @file = t.@file;
        if (@file == nil) {
            @file = Ꮡloader.parseSourceFile(Ꮡt);
        }
        if (@file == nil) {
            return;
        }

        t.@file = @file;
        @string virtualFileName = @file.VirtualFileName(); if (virtualFileName != ""u8) {
            t.metadata.ImpliedNodeFormat = ast.GetImpliedNodeFormatForFile(virtualFileName, t.metadata.PackageJsonType);
        }
        t.subTasks = new slice<ж<parseTask>>(0, len(@file.ReferencedFiles) + len(@file.Imports()) + len(@file.ModuleAugmentations));

        var compilerOptions = loader.opts.Config.CompilerOptions();
        if (!compilerOptions.NoResolve.IsTrue() && !loader.opts.SkipModuleResolution) {
            foreach (var (index, @ref) in range(@file.ReferencedFiles)) {
                var (resolvedRef, processingDiagnostic) = loader.resolveTripleslashPathReference(@ref.FileName, @file.FileName(), index);
                if (processingDiagnostic != nil) {
                    t.processingDiagnostics = append(t.processingDiagnostics, processingDiagnostic);
                    continue;
                }
                t.addSubTask(resolvedRef.Value, nil);
            }

            Ꮡloader.resolveTypeReferenceDirectives(Ꮡt);
        }

        if (compilerOptions.NoLib != core.TSTrue && !loader.opts.SkipModuleResolution) {
            foreach (var (iᴛ1, lib) in range(@file.LibReferenceDirectives)) {
                ref var index = ref heap(new nint(), out var Ꮡindex);
                index = iᴛ1;

                var includeReason = Ꮡ(new FileIncludeReason(
                    kind: fileIncludeKindLibReferenceDirective,
                    data: Ꮡ(new referencedFileData(
                        @file: t.path,
                        index: index
                    ))
                ));
                var (name, ok) = tsoptions.GetLibFileName(lib.FileName); if (ok) {
                    var libFile = Ꮡloader.pathForLibFile(name);
                    t.addSubTask(new resolvedRef(
                        fileName: libFile.path,
                        includeReason: includeReason
                    ), libFile);
                } else {
                    t.processingDiagnostics = append(t.processingDiagnostics, Ꮡ(new processingDiagnostic(
                        kind: processingDiagnosticKindUnknownReference,
                        data: includeReason.OrTypedNil()
                    )));
                }
            }
        }

        Ꮡloader.resolveImportsAndModuleAugmentations(Ꮡt);
        foreach (var (_, supplemental) in range(@file.SupplementalSourceFiles())) {
            t.subTasks = append(t.subTasks, Ꮡ(new parseTask(
                normalizedFilePath: supplemental.FileName(),
                @file: supplemental,
                isContentMapperSupplemental: true,
                includeReason: Ꮡ(new FileIncludeReason(
                    kind: fileIncludeKindContentMapperSupplemental,
                    data: t.path
                ))
            )));
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static void redirect(this ref parseTask t, ж<fileLoader> loader, @string fileName) {
    t.redirectedParseTask = Ꮡ(new parseTask(
        normalizedFilePath: tspath.NormalizePath(fileName),
        libFile: t.libFile,
        includeReason: t.includeReason
    ));
    t.subTasks = [t.redirectedParseTask];
}

internal static void loadAutomaticTypeDirectives(this ж<parseTask> Ꮡt, ж<fileLoader> Ꮡloader) {
    GoFrame ᒐ = default;
    try {
        ref var t = ref Ꮡt.DerefOrNull();
        ref var loader = ref Ꮡloader.DerefOrNull();

        if (loader.opts.Tracing != nil) {
            defer(Ꮡloader.Value.opts.Tracing.Push(tracing.PhaseProgram, "processTypeReferences", default, false), ref ᒐ);
        }
        var (toParseTypeRefs, typeResolutionsInFile, typeResolutionsTrace, pDiagnostics) = loader.resolveAutomaticTypeDirectives(t.normalizedFilePath);
        t.typeResolutionsInFile = typeResolutionsInFile;
        t.typeResolutionsTrace = typeResolutionsTrace;
        t.processingDiagnostics = appendꓸꓸꓸ(t.processingDiagnostics, pDiagnostics);
        foreach (var (_, typeResolution) in range(toParseTypeRefs)) {
            t.addSubTask(typeResolution, nil);
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

partial struct resolvedRef {
    internal @string fileName;
    internal bool increaseDepth;
    internal bool elideOnDepth;
    internal ж<FileIncludeReason> includeReason;
    internal module.PackageId packageId;
}

} partial class compilerᴛparseTask {

internal static void addSubTask(this ref parseTask t, resolvedRef @ref, ж<LibFile> libFile) {
    ref var normalizedFilePath = ref heap<@string>(tspath.NormalizePath(@ref.fileName), out var ᏑnormalizedFilePath);
    var subTask = Ꮡ(new parseTask(
        normalizedFilePath: normalizedFilePath,
        libFile: libFile,
        increaseDepth: @ref.increaseDepth,
        elideOnDepth: @ref.elideOnDepth,
        includeReason: @ref.includeReason,
        packageId: @ref.packageId
    ));
    t.subTasks = append(t.subTasks, subTask);
}

} partial class compiler_package {

partial struct filesParser {
    internal core.WorkGroup wg;
    internal collections.SyncMap<tspath.Path, ж<parseTaskData>> taskDataByPath;
    internal nint maxDepth;
}

[GoLazy] partial class lazyᴛparseTaskDataPool { internal static readonly ж<sync.Pool> ᏑparseTaskDataPool = new StaticBox<sync.Pool>(new sync.Pool(
    New: () => Ꮡ(new parseTaskData(
            tasks: new map<@string, ж<parseTask>>(1)
        ))
)); }
internal static ref sync.Pool parseTaskDataPool => ref ((StaticBox<sync.Pool>)ᏑparseTaskDataPool).ValueSlot;

internal static ж<parseTaskData> getParseTaskData(ж<parseTask> Ꮡtask) {
    ref var task = ref Ꮡtask.DerefOrNull();

    var td = ᏑparseTaskDataPool.Get()._<ж<parseTaskData>>();
    td.tasks[task.normalizedFilePath] = Ꮡtask;
    td.lowestDepth = math.MaxInt;
    return td;
}

internal static void putParseTaskData(ж<parseTaskData> Ꮡtd) {
    ref var td = ref Ꮡtd.DerefOrNull();

    clear(td.tasks);
    ᏑparseTaskDataPool.Put(Ꮡtd.OrTypedNil());
}

partial struct parseTaskData {
    internal map<@string, ж<parseTask>> tasks;
    internal sync.Mutex mu;
    internal nint lowestDepth;
    internal bool startedSubTasks;
    internal module.PackageId packageId;
}

} partial class compilerᴛfilesParser {

internal static void parse(this ж<filesParser> Ꮡw, ж<fileLoader> loader, slice<ж<parseTask>> tasks) {
    ref var w = ref Ꮡw.DerefOrNull();

    Ꮡw.start(loader, tasks, 0);
    w.wg.RunAndWait();
}

internal static void start(this ж<filesParser> Ꮡw, ж<fileLoader> Ꮡloader, slice<ж<parseTask>> tasks, nint depth) {
    ref var w = ref Ꮡw.DerefOrNull();
    ref var loader = ref Ꮡloader.DerefOrNull();

    foreach (var (i, task) in range(tasks)) {
        task.path = loader.toPath(task.normalizedFilePath);
        var candidate = getParseTaskData(task);
        var (data, loaded) = Ꮡw.ᏑtaskDataByPath.LoadOrStore(task.path, candidate);
        if (loaded) {
            putParseTaskData(candidate);
        }

        var dataʗ1 = data;
        var taskʗ1 = task;
        var tasksʗ1 = tasks;
        w.wg.Queue(() => {
            GoFrame ᒐ = default;
            try {
                dataʗ1.Ꮡmu.Lock();
                var dataʗ2 = dataʗ1;
                defer(dataʗ2.Ꮡmu.Unlock, ref ᒐ);

                var startSubtasks = false;
                if (loaded) {
                    var (existingTask, ok) = dataʗ1.tasks[taskʗ1.normalizedFilePath, ꟷ]; if (ok) {
                        tasksʗ1[i].loadedTask = existingTask;
                    } else {
                        dataʗ1.tasks[taskʗ1.normalizedFilePath] = taskʗ1;
                        startSubtasks = dataʗ1.startedSubTasks;
                    }
                }

                if (dataʗ1.packageId.Name == ""u8 && taskʗ1.packageId.Name != ""u8) {
                    dataʗ1.packageId = taskʗ1.packageId;
                }

                nint currentDepth = core.IfElse(taskʗ1.increaseDepth, depth + 1, depth);
                if (currentDepth < dataʗ1.lowestDepth) {
                    dataʗ1.lowestDepth = currentDepth;
                    startSubtasks = true;
                    dataʗ1.startedSubTasks = true;
                }

                if (taskʗ1.elideOnDepth && currentDepth > Ꮡw.Value.maxDepth) {
                    return;
                }

                foreach (var (_, taskByFileName) in dataʗ1.tasks) {
                    var loadSubTasks = startSubtasks;
                    if (!taskByFileName.loaded) {
                        taskByFileName.load(Ꮡloader);
                        if (taskByFileName.redirectedParseTask != nil) {
                            loadSubTasks = true;
                            dataʗ1.startedSubTasks = true;
                        }
                    }
                    if (!taskByFileName.startedSubTasks && loadSubTasks) {
                        taskByFileName.startedSubTasks = true;
                        Ꮡw.start(Ꮡloader, taskByFileName.subTasks, dataʗ1.lowestDepth);
                    }
                }
            }
            catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
            finally { ᒐ.Run(); }
        });
    }
}

internal static processedFiles getProcessedFiles(this ж<filesParser> Ꮡw, ж<fileLoader> Ꮡloader) {
    ref var loader = ref Ꮡloader.DerefOrNull();

    nint totalFileCount = (nint)Ꮡloader.ᏑtotalFileCount.Load();
    nint libFileCount = (nint)Ꮡloader.ᏑlibFileCount.Load();

    ref var missingFiles = ref heap<slice<@string>>(out var ᏑmissingFiles);
    ref var duplicateSourceFiles = ref heap<slice<ж<DuplicateSourceFile>>>(out var ᏑduplicateSourceFiles);
    ref var files = ref heap<slice<ж<ast.SourceFile>>>(new slice<ж<ast.SourceFile>>(0, totalFileCount - libFileCount), out var Ꮡfiles);
    ref var libFiles = ref heap<slice<ж<ast.SourceFile>>>(new slice<ж<ast.SourceFile>>(0, totalFileCount), out var ᏑlibFiles);

    var filesByPath = new map<tspath.Path, ж<ast.SourceFile>>(totalFileCount);
    map<@string, ж<parseTask>> tasksSeenByNameIgnoreCase = default;
    if (loader.comparePathsOptions.UseCaseSensitiveFileNames) {
        tasksSeenByNameIgnoreCase = new map<@string, ж<parseTask>>(totalFileCount);
    }

    var includeProcessor = Ꮡ(new includeProcessor(
        fileIncludeReasons: new map<tspath.Path, slice<ж<FileIncludeReason>>>(totalFileCount)
    ));
    map<tspath.Path, @string> outputFileToProjectReferenceSource = default;
    if (!loader.opts.canUseProjectReferenceSource()) {
        outputFileToProjectReferenceSource = new map<tspath.Path, @string>(totalFileCount);
    }
    var resolvedModules = new map<tspath.Path, module.ModeAwareCache<ж<module.ResolvedModule>>>(totalFileCount + 1);
    var typeResolutionsInFile = new map<tspath.Path, module.ModeAwareCache<ж<module.ResolvedTypeReferenceDirective>>>(totalFileCount);
    var sourceFileMetaDatas = new map<tspath.Path, ast.SourceFileMetaData>(totalFileCount);
    ref var jsxRuntimeImportSpecifiers = ref heap<map<tspath.Path, ж<jsxRuntimeImportSpecifier>>>(out var ᏑjsxRuntimeImportSpecifiers);
    ref var importHelpersImportSpecifiers = ref heap<map<tspath.Path, ж<astꓸStringLiteralNode>>>(out var ᏑimportHelpersImportSpecifiers);
    collections.Set<tspath.Path> sourceFilesFoundSearchingNodeModules = default;
    var libFilesMap = new map<tspath.Path, ж<LibFile>>(libFileCount);

    map<tspath.Path, slice<@string>> redirectTargetsMap = default;
    ref var redirectFilesByPath = ref heap<map<tspath.Path, ж<redirectsFile>>>(out var ᏑredirectFilesByPath);
    map<module.PackageId, ж<ast.SourceFile>> packageIdToSourceFile = default;
    if (!loader.opts.Config.CompilerOptions().DeduplicatePackages.IsFalse()) {
        redirectTargetsMap = new map<tspath.Path, slice<@string>>();
        packageIdToSourceFile = new map<module.PackageId, ж<ast.SourceFile>>();
    }

    ref var collectFiles = ref heap<Action<slice<ж<parseTask>>, map<ж<parseTaskData>, @string>>>(out var ᏑcollectFiles);
    ref var recordedDuplicates = ref heap<map<ж<parseTaskData>, ж<collections.Set<@string>>>>(out var ᏑrecordedDuplicates);
    var filesByPathʗ1 = filesByPath;
    var includeProcessorʗ1 = includeProcessor;
    var libFilesMapʗ1 = libFilesMap;
    var outputFileToProjectReferenceSourceʗ1 = outputFileToProjectReferenceSource;
    var packageIdToSourceFileʗ1 = packageIdToSourceFile;
    var redirectTargetsMapʗ1 = redirectTargetsMap;
    var resolvedModulesʗ1 = resolvedModules;
    var sourceFileMetaDatasʗ1 = sourceFileMetaDatas;
    var tasksSeenByNameIgnoreCaseʗ1 = tasksSeenByNameIgnoreCase;
    var typeResolutionsInFileʗ1 = typeResolutionsInFile;
    collectFiles = (slice<ж<parseTask>> tasks, map<ж<parseTaskData>, @string> seen) => {
        foreach (var (_, vᴛ1) in range(tasks)) {
            var task = vᴛ1;

            var includeReason = task.includeReason;
            if (task.redirectedParseTask == nil && !task.isForAutomaticTypeDirective) {
                if (task.loadedTask != nil) {
                    task = task.loadedTask;
                }
                Ꮡw.Value.addIncludeReason(includeProcessorʗ1, task, includeReason);
            }
            var (data, _) = Ꮡw.ᏑtaskDataByPath.Load(task.path);
            if (!task.loaded) {
                continue;
            }

            if (seen[data, ꟷ] is (var checkedName, true)) {
                if (task.@file != nil && checkedName != task.normalizedFilePath) {
                    if (ᏑrecordedDuplicates.ValueSlot == nil) {
                        ᏑrecordedDuplicates.ValueSlot = new map<ж<parseTaskData>, ж<collections.Set<@string>>>();
                    }
                    var dups = ᏑrecordedDuplicates.ValueSlot[data];
                    if (dups == nil) {
                        dups = Ꮡ(new collections.Set<@string>());
                        ᏑrecordedDuplicates.ValueSlot[data] = dups;
                    }
                    if (dups.AddIfAbsent(task.normalizedFilePath)) {
                        ᏑduplicateSourceFiles.ValueSlot = append(ᏑduplicateSourceFiles.ValueSlot, Ꮡ(new DuplicateSourceFile(
                            ParseOptions: task.@file.ParseOptions(),
                            ContentMapperParseOptions: task.@file.ContentMapperParseOptions(),
                            Hash: task.@file.Hash,
                            ScriptKind: task.@file.ScriptKind,
                            ContentMapper: task.@file.ContentMapper(),
                            IsContentMapperFailureStub: task.@file.IsContentMapperFailureStub()
                        )));
                    }
                }
                if (!Ꮡloader.Value.opts.Config.CompilerOptions().ForceConsistentCasingInFileNames.IsFalse()) {
                    @string checkedAbsolutePath = tspath.GetNormalizedAbsolutePathWithoutRoot(checkedName, Ꮡloader.Value.comparePathsOptions.CurrentDirectory);
                    @string inputAbsolutePath = tspath.GetNormalizedAbsolutePathWithoutRoot(task.normalizedFilePath, Ꮡloader.Value.comparePathsOptions.CurrentDirectory);
                    if (checkedAbsolutePath != inputAbsolutePath) {
                        includeProcessorʗ1.addProcessingDiagnosticsForFileCasing(task.path, checkedName, task.normalizedFilePath, includeReason);
                    }
                }
                continue;
            } else {
                seen[data] = task.normalizedFilePath;
            }

            if (tasksSeenByNameIgnoreCaseʗ1 != nil) {
                @string pathLowerCase = tspath.ToFileNameLowerCase((@string)task.path);
                var (taskByIgnoreCase, ok) = tasksSeenByNameIgnoreCaseʗ1[pathLowerCase, ꟷ]; if (ok) {
                    includeProcessorʗ1.addProcessingDiagnosticsForFileCasing(taskByIgnoreCase.path, taskByIgnoreCase.normalizedFilePath, task.normalizedFilePath, includeReason);
                } else {
                    tasksSeenByNameIgnoreCaseʗ1[pathLowerCase] = task;
                }
            }

            foreach (var (_, trace) in range(task.typeResolutionsTrace)) {
                Ꮡloader.Value.opts.Host.Trace(trace.Message, trace.Args.ꓸꓸꓸ);
            }
            foreach (var (_, trace) in range(task.resolutionsTrace)) {
                Ꮡloader.Value.opts.Host.Trace(trace.Message, trace.Args.ꓸꓸꓸ);
            }

            var @file = task.@file;
            if (packageIdToSourceFileʗ1 != nil && data.packageId.Name != ""u8) {
                var (packageIdFile, exists) = packageIdToSourceFileʗ1[data.packageId, ꟷ]; if (exists) {
                    if (@file != nil) {
                        ᏑduplicateSourceFiles.ValueSlot = append(ᏑduplicateSourceFiles.ValueSlot, Ꮡ(new DuplicateSourceFile(
                            ParseOptions: @file.ParseOptions(),
                            ContentMapperParseOptions: @file.ContentMapperParseOptions(),
                            Hash: @file.Hash,
                            ScriptKind: @file.ScriptKind,
                            ContentMapper: @file.ContentMapper(),
                            IsContentMapperFailureStub: @file.IsContentMapperFailureStub()
                        )));
                    }
                    redirectTargetsMapʗ1[packageIdFile.Path()] = append(redirectTargetsMapʗ1[packageIdFile.Path()], task.normalizedFilePath);
                    if (ᏑredirectFilesByPath.ValueSlot == nil) {
                        ᏑredirectFilesByPath.ValueSlot = new map<tspath.Path, ж<redirectsFile>>(totalFileCount);
                    }
                    ᏑredirectFilesByPath.ValueSlot[task.path] = Ꮡ(new redirectsFile(
                        index: len(Ꮡfiles.ValueSlot) + len(ᏑredirectFilesByPath.ValueSlot),
                        fileName: task.normalizedFilePath,
                        path: task.path,
                        target: packageIdFile.Path()
                    ));
                    filesByPathʗ1[task.path] = packageIdFile;
                    if (data.lowestDepth > 0) {
                        sourceFilesFoundSearchingNodeModules.Add(task.path);
                    }
                    continue;
                } else if (@file != nil) {
                    packageIdToSourceFileʗ1[data.packageId] = @file;
                }
            }

            var subTasks = task.subTasks; if (len(subTasks) > 0) {
                ᏑcollectFiles.ValueSlot(subTasks, seen);
            }

            if (task.redirectedParseTask != nil) {
                if (!Ꮡloader.Value.opts.canUseProjectReferenceSource()) {
                    outputFileToProjectReferenceSourceʗ1[task.redirectedParseTask.path] = task.FileName();
                }
                continue;
            }

            if (task.isForAutomaticTypeDirective) {
                typeResolutionsInFileʗ1[task.path] = task.typeResolutionsInFile;
                if (len(task.processingDiagnostics) > 0) {
                    includeProcessorʗ1.processingDiagnostics = appendꓸꓸꓸ(includeProcessorʗ1.processingDiagnostics, task.processingDiagnostics);
                }
                continue;
            }

            tspath.Path path = task.path;

            if (len(task.processingDiagnostics) > 0) {
                includeProcessorʗ1.processingDiagnostics = appendꓸꓸꓸ(includeProcessorʗ1.processingDiagnostics, task.processingDiagnostics);
            }

            if (@file == nil) {
                ᏑmissingFiles.ValueSlot = append(ᏑmissingFiles.ValueSlot, task.normalizedFilePath);
                continue;
            }

            if (task.libFile != nil) {
                ᏑlibFiles.ValueSlot = append(ᏑlibFiles.ValueSlot, @file);
                libFilesMapʗ1[path] = task.libFile;
            } else {
                Ꮡfiles.ValueSlot = append(Ꮡfiles.ValueSlot, @file);
            }
            filesByPathʗ1[path] = @file;
            resolvedModulesʗ1[path] = task.resolutionsInFile;
            typeResolutionsInFileʗ1[path] = task.typeResolutionsInFile;
            sourceFileMetaDatasʗ1[path] = task.metadata;

            if (task.jsxRuntimeImportSpecifier != nil) {
                if (ᏑjsxRuntimeImportSpecifiers.ValueSlot == nil) {
                    ᏑjsxRuntimeImportSpecifiers.ValueSlot = new map<tspath.Path, ж<jsxRuntimeImportSpecifier>>(totalFileCount);
                }
                ᏑjsxRuntimeImportSpecifiers.ValueSlot[path] = task.jsxRuntimeImportSpecifier;
            }
            if (task.importHelpersImportSpecifier != nil) {
                if (ᏑimportHelpersImportSpecifiers.ValueSlot == nil) {
                    ᏑimportHelpersImportSpecifiers.ValueSlot = new map<tspath.Path, ж<astꓸStringLiteralNode>>(totalFileCount);
                }
                ᏑimportHelpersImportSpecifiers.ValueSlot[path] = task.importHelpersImportSpecifier;
            }
            if (data.lowestDepth > 0) {
                sourceFilesFoundSearchingNodeModules.Add(path);
            }
        }
    };

    collectFiles(loader.rootTasks, new map<ж<parseTaskData>, @string>(totalFileCount));
    Ꮡloader.sortLibs(libFiles);

    var allFiles = appendꓸꓸꓸ(libFiles, files);
    foreach (var (_, redirectFile) in redirectFilesByPath) {
        redirectFile.index += len(libFiles);
    }

    var keys = slices.Collect(Ꮡloader.ᏑpathForLibFileResolutions.Keys());
    slices.Sort<slice<tspath.Path>, tspath.Path>(keys);
    foreach (var (_, key) in range(keys)) {
        var (value, _) = Ꮡloader.ᏑpathForLibFileResolutions.Load(key);
        resolvedModules[key] = new module.ModeAwareCache<ж<module.ResolvedModule>>(new map<module.ModeAwareCacheKey, ж<module.ResolvedModule>>{
            [new module.ModeAwareCacheKey(Name: value.libraryName, Mode: core.ModuleKindCommonJS)] = value.resolution
        });
        foreach (var (_, trace) in range(value.trace)) {
            loader.opts.Host.Trace(trace.Message, trace.Args.ꓸꓸꓸ);
        }
    }

    return new processedFiles(
        finishedProcessing: true,
        resolver: loader.resolver,
        files: allFiles,
        duplicateSourceFiles: duplicateSourceFiles,
        filesByPath: filesByPath,
        projectReferenceFileMapper: loader.projectReferenceFileMapper,
        resolvedModules: resolvedModules,
        typeResolutionsInFile: typeResolutionsInFile,
        sourceFileMetaDatas: sourceFileMetaDatas,
        jsxRuntimeImportSpecifiers: jsxRuntimeImportSpecifiers,
        importHelpersImportSpecifiers: importHelpersImportSpecifiers,
        sourceFilesFoundSearchingNodeModules: sourceFilesFoundSearchingNodeModules,
        libFiles: libFilesMap,
        missingFiles: missingFiles,
        includeProcessor: includeProcessor,
        outputFileToProjectReferenceSource: outputFileToProjectReferenceSource,
        redirectTargetsMap: redirectTargetsMap,
        redirectFilesByPath: redirectFilesByPath,
        contentMapperDiagnostics: loader.contentMapperDiagnostics
    );
}

internal static void addIncludeReason(this ref filesParser w, ж<includeProcessor> ᏑincludeProcessor, ж<parseTask> Ꮡtask, ж<FileIncludeReason> reason) {
    ref var includeProcessor = ref ᏑincludeProcessor.DerefOrNull();
    ref var task = ref Ꮡtask.DerefOrNull();

    if (task.redirectedParseTask != nil) {
        w.addIncludeReason(ᏑincludeProcessor, task.redirectedParseTask, reason);
    } else if (task.loaded) {
        var (existing, ok) = includeProcessor.fileIncludeReasons[task.path, ꟷ]; if (ok) {
            includeProcessor.fileIncludeReasons[task.path] = append(existing, reason);
        } else {
            includeProcessor.fileIncludeReasons[task.path] = [reason];
        }
    }
}

} // end compilerᴛfilesParser
