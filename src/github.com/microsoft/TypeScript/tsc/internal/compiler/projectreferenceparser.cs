namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using static compiler_package;

partial class compiler_package {

partial struct projectReferenceParseTask {
    internal @string configName;
    internal ж<tsoptions.ParsedCommandLine> resolved;
    internal slice<ж<projectReferenceParseTask>> subTasks;
}

} partial class compilerᴛprojectReferenceParseTask {

internal static void parse(this ж<projectReferenceParseTask> Ꮡt, ж<projectReferenceParser> ᏑprojectReferenceParser) {
    GoFrame ᒐ = default;
    try {
        ref var t = ref Ꮡt.DerefOrNull();
        ref var projectReferenceParser = ref ᏑprojectReferenceParser.DerefOrNull();

        var loader = projectReferenceParser.loader;
        {
            var tr = loader.opts.Tracing; if (tr != nil) {
                var trʗ1 = tr;
                defer(trʗ1.Push(tracing.PhaseParse, "parseJsonSourceFileConfigFileContent", new map<@string, any>{["path"] = Ꮡt.Value.configName}, false), ref ᒐ);
            }
        }
        t.resolved = loader.opts.Host.GetResolvedProjectReference(t.configName, loader.toPath(t.configName));
        if (t.resolved == nil) {
            return;
        }
        t.resolved.ParseInputOutputNames();
        var subReferences = t.resolved.ResolvedProjectReferencePaths(); if (len(subReferences) > 0) {
            t.subTasks = createProjectReferenceParseTasks(subReferences);
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

internal static slice<ж<projectReferenceParseTask>> createProjectReferenceParseTasks(slice<@string> projectReferences) {
    return core.Map(projectReferences, ж<projectReferenceParseTask> (@string configName) => Ꮡ(new projectReferenceParseTask(
            configName: configName
        )));
}

partial struct projectReferenceParser {
    internal ж<fileLoader> loader;
    internal core.WorkGroup wg;
    internal collections.SyncMap<tspath.Path, ж<projectReferenceParseTask>> tasksByFileName;
}

} partial class compilerᴛprojectReferenceParser {

internal static void parse(this ж<projectReferenceParser> Ꮡp, slice<ж<projectReferenceParseTask>> tasks) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.loader.projectReferenceFileMapper.loader = p.loader;
    Ꮡp.start(tasks);
    p.wg.RunAndWait();
    Ꮡp.initMapper(tasks);
}

internal static void start(this ж<projectReferenceParser> Ꮡp, slice<ж<projectReferenceParseTask>> tasks) {
    ref var p = ref Ꮡp.DerefOrNull();

    foreach (var (i, task) in range(tasks)) {
        tspath.Path path = p.loader.toPath(task.configName);
        {
            var (loadedTask, loaded) = Ꮡp.ᏑtasksByFileName.LoadOrStore(path, task); if (loaded) {
                tasks[i] = loadedTask;
            } else {
                var taskʗ1 = task;
                p.wg.Queue(() => {
                    taskʗ1.parse(Ꮡp);
                    Ꮡp.start(taskʗ1.subTasks);
                });
            }
        }
    }
}

internal static void initMapper(this ж<projectReferenceParser> Ꮡp, slice<ж<projectReferenceParseTask>> tasks) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint totalReferences = Ꮡp.ᏑtasksByFileName.Size() + 1;
    p.loader.projectReferenceFileMapper.configToProjectReference = new map<tspath.Path, ж<tsoptions.ParsedCommandLine>>(totalReferences);
    p.loader.projectReferenceFileMapper.referencesInConfigFile = new map<tspath.Path, slice<tspath.Path>>(totalReferences);
    p.loader.projectReferenceFileMapper.sourceToProjectReference = new map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>();
    p.loader.projectReferenceFileMapper.outputDtsToProjectReference = new map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>();
    p.loader.projectReferenceFileMapper.referencesInConfigFile[p.loader.projectReferenceFileMapper.rootConfigPath()] = p.initMapperWorker(tasks, Ꮡ(new collections.Set<ж<projectReferenceParseTask>>()));
    if (p.loader.projectReferenceFileMapper.Ꮡopts.canUseProjectReferenceSource() && len(p.loader.projectReferenceFileMapper.outputDtsToProjectReference) != 0) {
        p.loader.projectReferenceFileMapper.host = newProjectReferenceDtsFakingHost(ref (p.loader).DerefOrNull());
    }
}

internal static slice<tspath.Path> initMapperWorker(this ref projectReferenceParser p, slice<ж<projectReferenceParseTask>> tasks, ж<collections.Set<ж<projectReferenceParseTask>>> seen) {
    if (len(tasks) == 0) {
        return default;
    }
    var results = new slice<tspath.Path>(0, len(tasks));
    foreach (var (_, task) in range(tasks)) {
        tspath.Path path = p.loader.toPath(task.configName);
        results = append(results, path);
        if (!seen.AddIfAbsent(task)) {
            continue;
        }
        p.loader.projectReferenceFileMapper.configToProjectReference[path] = task.resolved;
        if (task.resolved != nil && p.loader.projectReferenceFileMapper.opts.Config.ConfigFile != task.resolved.ConfigFile) {
            maps.Copy<map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>, map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>, tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>(p.loader.projectReferenceFileMapper.sourceToProjectReference, task.resolved.SourceToProjectReference());
            maps.Copy<map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>, map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>, tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>>(p.loader.projectReferenceFileMapper.outputDtsToProjectReference, task.resolved.OutputDtsToProjectReference());
            if (p.loader.projectReferenceFileMapper.Ꮡopts.canUseProjectReferenceSource()) {
                @string declDir = task.resolved.CompilerOptions().DeclarationDir;
                if (declDir == ""u8) {
                    declDir = task.resolved.CompilerOptions().OutDir;
                }
                if (declDir != ""u8) {
                    p.loader.ᏑdtsDirectories.Add(p.loader.toPath(declDir));
                }
            }
        }
        var referencesInConfig = p.initMapperWorker(task.subTasks, seen);
        p.loader.projectReferenceFileMapper.referencesInConfigFile[path] = referencesInConfig;
    }
    return results;
}

} // end compilerᴛprojectReferenceParser
