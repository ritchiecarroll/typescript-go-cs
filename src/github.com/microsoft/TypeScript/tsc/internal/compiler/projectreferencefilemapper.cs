namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using static compiler_package;

partial class compiler_package {

partial struct projectReferenceFileMapper {
    internal ProgramOptions opts;
    internal module.ResolutionHost host;
    internal ж<fileLoader> loader;
    internal map<tspath.Path, ж<tsoptions.ParsedCommandLine>> configToProjectReference;
    internal map<tspath.Path, slice<tspath.Path>> referencesInConfigFile;
    internal map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>> sourceToProjectReference;
    internal map<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>> outputDtsToProjectReference;
    internal collections.SyncMap<tspath.Path, ж<tsoptions.SourceOutputAndProjectReference>> realpathDtsToSource;
}

} partial class compilerᴛprojectReferenceFileMapper {

internal static tspath.Path rootConfigPath(this ref projectReferenceFileMapper mapper) {
    if (mapper.opts.Config.ConfigFile == nil) {
        return (@string)""u8;
    }
    return mapper.opts.Config.ConfigFile.SourceFile.Path();
}

internal static @string getParseFileRedirect(this ж<projectReferenceFileMapper> Ꮡmapper, ast.HasFileName @file) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    if (mapper.opts.canUseProjectReferenceSource()) {
        var source = mapper.getProjectReferenceFromOutputDts(@file.Path());
        if (source == nil) {
            source = Ꮡmapper.getSourceToDtsIfSymlink(@file);
        }
        if (source != nil) {
            return source.Source;
        }
    } else {
        var output = mapper.getProjectReferenceFromSource(@file.Path());
        if (output != nil && output.OutputDts != ""u8) {
            return output.OutputDts;
        }
    }
    return "";
}

internal static slice<ж<tsoptions.ParsedCommandLine>> getResolvedProjectReferences(this ref projectReferenceFileMapper mapper) {
    var (refs, ok) = mapper.referencesInConfigFile[mapper.rootConfigPath(), ꟷ];
    slice<ж<tsoptions.ParsedCommandLine>> result = default;
    if (ok) {
        result = new slice<ж<tsoptions.ParsedCommandLine>>(0, len(refs));
        foreach (var (_, refPath) in range(refs)) {
            var (refConfig, _) = mapper.configToProjectReference[refPath, ꟷ];
            result = append(result, refConfig);
        }
    }
    return result;
}

internal static ж<tsoptions.SourceOutputAndProjectReference> getProjectReferenceFromSource(this ref projectReferenceFileMapper mapper, tspath.Path path) {
    return mapper.sourceToProjectReference[path];
}

internal static ж<tsoptions.SourceOutputAndProjectReference> getProjectReferenceFromOutputDts(this ref projectReferenceFileMapper mapper, tspath.Path path) {
    return mapper.outputDtsToProjectReference[path];
}

internal static bool isSourceFromProjectReference(this ref projectReferenceFileMapper mapper, tspath.Path path) {
    return mapper.opts.canUseProjectReferenceSource() && mapper.getProjectReferenceFromSource(path) != nil;
}

internal static ж<core.CompilerOptions> getCompilerOptionsForFile(this ж<projectReferenceFileMapper> Ꮡmapper, ast.HasFileName @file) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    var redirect = Ꮡmapper.getRedirectParsedCommandLineForResolution(@file);
    return module.GetCompilerOptionsWithRedirect(mapper.opts.Config.CompilerOptions(), new tsoptions.ParsedCommandLineжResolvedProjectReference(redirect));
}

internal static ж<tsoptions.ParsedCommandLine> getRedirectParsedCommandLineForResolution(this ж<projectReferenceFileMapper> Ꮡmapper, ast.HasFileName @file) {
    var (redirect, _) = Ꮡmapper.getRedirectForResolution(@file);
    return redirect;
}

internal static (ж<tsoptions.ParsedCommandLine>, @string) getRedirectForResolution(this ж<projectReferenceFileMapper> Ꮡmapper, ast.HasFileName @file) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    tspath.Path path = @file.Path();
    var output = mapper.getProjectReferenceFromSource(path);
    if (output != nil) {
        return (output.Resolved, output.Source);
    }

    var resultFromDts = mapper.getProjectReferenceFromOutputDts(path);
    if (resultFromDts != nil) {
        return (resultFromDts.Resolved, resultFromDts.Source);
    }

    var realpathDtsToSource = Ꮡmapper.getSourceToDtsIfSymlink(@file);
    if (realpathDtsToSource != nil) {
        return (realpathDtsToSource.Resolved, realpathDtsToSource.Source);
    }
    return (default, @file.FileName());
}

internal static (ж<tsoptions.ParsedCommandLine>, bool) getResolvedReferenceFor(this ref projectReferenceFileMapper mapper, tspath.Path path) {
    var (config, ok) = mapper.configToProjectReference[path, ꟷ];
    return (config, ok);
}

internal static bool rangeResolvedProjectReference(this ref projectReferenceFileMapper mapper, Func<tspath.Path, ж<tsoptions.ParsedCommandLine>, ж<tsoptions.ParsedCommandLine>, nint, bool> f) {
    if (len(mapper.opts.Config.ProjectReferences()) == 0) {
        return false;
    }
    var seenRef = collections.NewSetWithSizeHint<tspath.Path>(len(mapper.referencesInConfigFile));
    tspath.Path rootConfigPath = mapper.rootConfigPath();
    seenRef.Add(rootConfigPath);
    var refs = mapper.referencesInConfigFile[rootConfigPath];
    return mapper.rangeResolvedReferenceWorker(refs, f, mapper.opts.Config, seenRef);
}

internal static bool rangeResolvedReferenceWorker(this ref projectReferenceFileMapper mapper, slice<tspath.Path> references, Func<tspath.Path, ж<tsoptions.ParsedCommandLine>, ж<tsoptions.ParsedCommandLine>, nint, bool> f, ж<tsoptions.ParsedCommandLine> parent, ж<collections.Set<tspath.Path>> seenRef) {
    foreach (var (index, path) in range(references)) {
        if (!seenRef.AddIfAbsent(path)) {
            continue;
        }
        var (config, _) = mapper.configToProjectReference[path, ꟷ];
        if (!f(path, config, parent, index)) {
            return false;
        }
        if (!mapper.rangeResolvedReferenceWorker(mapper.referencesInConfigFile[path], f, config, seenRef)) {
            return false;
        }
    }
    return true;
}

internal static bool rangeResolvedProjectReferenceInChildConfig(this ref projectReferenceFileMapper mapper, ж<tsoptions.ParsedCommandLine> ᏑchildConfig, Func<tspath.Path, ж<tsoptions.ParsedCommandLine>, ж<tsoptions.ParsedCommandLine>, nint, bool> f) {
    ref var childConfig = ref ᏑchildConfig.DerefOrNull();

    if (ᏑchildConfig == nil || childConfig.ConfigFile == nil) {
        return false;
    }
    var seenRef = collections.NewSetWithSizeHint<tspath.Path>(len(mapper.referencesInConfigFile));
    seenRef.Add(childConfig.ConfigFile.SourceFile.Path());
    var refs = mapper.referencesInConfigFile[childConfig.ConfigFile.SourceFile.Path()];
    return mapper.rangeResolvedReferenceWorker(refs, f, mapper.opts.Config, seenRef);
}

internal static ж<tsoptions.SourceOutputAndProjectReference> getSourceToDtsIfSymlink(this ж<projectReferenceFileMapper> Ꮡmapper, ast.HasFileName @file) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    tspath.Path path = @file.Path();
    var (realpathDtsToSource, ok) = Ꮡmapper.ᏑrealpathDtsToSource.Load(path);
    if (ok) {
        return realpathDtsToSource;
    }
    if (mapper.loader != nil && mapper.opts.Config.CompilerOptions().PreserveSymlinks == core.TSTrue) {
        @string fileName = @file.FileName();
        if (!strings.Contains(fileName, "/node_modules/")) {
            Ꮡmapper.ᏑrealpathDtsToSource.Store(path, nil);
        } else {
            tspath.Path realDeclarationPath = mapper.loader.toPath(mapper.host.FS().Realpath(fileName));
            if (realDeclarationPath == path) {
                Ꮡmapper.ᏑrealpathDtsToSource.Store(path, nil);
            } else {
                var realpathDtsToSourceΔ1 = mapper.getProjectReferenceFromOutputDts(realDeclarationPath);
                if (realpathDtsToSourceΔ1 != nil) {
                    Ꮡmapper.ᏑrealpathDtsToSource.Store(path, realpathDtsToSourceΔ1);
                    return realpathDtsToSourceΔ1;
                }
                Ꮡmapper.ᏑrealpathDtsToSource.Store(path, nil);
            }
        }
    }
    return default;
}

} // end compilerᴛprojectReferenceFileMapper
