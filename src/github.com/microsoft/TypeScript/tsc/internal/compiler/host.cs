namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using parser = go.github.com.microsoft.TypeScript.tsc.@internal.parser_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using cachedvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.cachedvfs_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using time = time_package;
using ꓸꓸꓸany = Span<any>;
using static compiler_package;

partial class compiler_package {

partial interface CompilerHost :
    module.ResolutionHost
{
    @string DefaultLibraryPath();
    void Trace(ж<diagnostics.Message> msg, params ꓸꓸꓸany argsʗp);
    ж<ast.SourceFile> GetSourceFile(ast.SourceFileParseOptions opts);
    (contentmapper.SourceFiles, error) GetContentMappedSourceFiles(ast.SourceFileParseOptions parseOptions, ж<contentmapper.Mapper> mapper);
    contentmapper.Project ContentMapperProject();
    ж<tsoptions.ParsedCommandLine> GetResolvedProjectReference(@string fileName, tspath.Path path);
}

internal static CompilerHost _ᴛ4ʗ = new compilerHostжCompilerHost(((ж<compilerHost>)nil));

partial struct compilerHost {
    internal @string currentDirectory;
    internal Δvfs.FS fs;
    internal @string defaultLibraryPath;
    internal tsoptions.ExtendedConfigCache extendedConfigCache;
    internal Actionꓸꓸꓸ<ж<diagnostics.Message>, any> trace;
    internal contentmapper.Project contentMapperProject;
}

public static CompilerHost NewCachedFSCompilerHost(@string currentDirectory, Δvfs.FS fs, @string defaultLibraryPath, tsoptions.ExtendedConfigCache extendedConfigCache, Actionꓸꓸꓸ<ж<diagnostics.Message>, any> trace, contentmapper.Project contentMapperProject) {
    return NewCompilerHost(currentDirectory, new cachedvfs.FSжFS(cachedvfs.From(fs)), defaultLibraryPath, extendedConfigCache, trace, contentMapperProject);
}

public static CompilerHost NewCompilerHost(@string currentDirectory, Δvfs.FS fs, @string defaultLibraryPath, tsoptions.ExtendedConfigCache extendedConfigCache, Actionꓸꓸꓸ<ж<diagnostics.Message>, any> trace, contentmapper.Project contentMapperProject) {
    if (trace == nil) {
        trace = (ж<diagnostics.Message> msg, params ꓸꓸꓸany argsʗp) => {
            var args = argsʗp.sslice();
        };
    }
    return new compilerHostжCompilerHost(Ꮡ(new compilerHost(
        currentDirectory: currentDirectory,
        fs: fs,
        defaultLibraryPath: defaultLibraryPath,
        extendedConfigCache: extendedConfigCache,
        trace: trace,
        contentMapperProject: contentMapperProject
    )));
}

} partial class compilerᴛcompilerHost {

internal static Δvfs.FS FS(this ref compilerHost h) {
    return h.fs;
}

internal static @string DefaultLibraryPath(this ref compilerHost h) {
    return h.defaultLibraryPath;
}

internal static @string GetCurrentDirectory(this ref compilerHost h) {
    return h.currentDirectory;
}

internal static void Trace(this ref compilerHost h, ж<diagnostics.Message> msg, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    h.trace(msg, args.ꓸꓸꓸ);
}

internal static ж<ast.SourceFile> GetSourceFile(this ref compilerHost h, ast.SourceFileParseOptions opts) {
    var (text, ok) = h.FS().ReadFile(opts.FileName);
    if (!ok) {
        return default;
    }
    return parser.ParseSourceFile(opts, text, core.EnsureScriptKindFromFileName(opts.FileName));
}

internal static (contentmapper.SourceFiles, error) GetContentMappedSourceFiles(this ref compilerHost h, ast.SourceFileParseOptions parseOptions, ж<contentmapper.Mapper> mapper) {
    if (h.contentMapperProject == nil) {
        return (new contentmapper.SourceFiles(), contentmapper.ErrProjectUnavailable);
    }
    var (content, ok) = h.FS().ReadFile(parseOptions.FileName);
    if (!ok) {
        return (new contentmapper.SourceFiles(), default);
    }
    var (files, err) = contentmapper.TransformAndParse(parseOptions, content, mapper, h.contentMapperProject);
    if (err == nil) {
        err = contentmapper.CheckSupplementalFileNameCollisions(files, h.FS().FileExists);
    }
    return (files, err);
}

internal static contentmapper.Project ContentMapperProject(this ref compilerHost h) {
    return h.contentMapperProject;
}

internal static ж<tsoptions.ParsedCommandLine> GetResolvedProjectReference(this ж<compilerHost> Ꮡh, @string fileName, tspath.Path path) {
    ref var h = ref Ꮡh.DerefOrNull();

    var (commandLine, _) = tsoptions.GetParsedCommandLineOfConfigFilePath(fileName, path, nil, nil, new compilerHostжParseConfigHost(Ꮡh), h.extendedConfigCache);
    return commandLine;
}

} // end compilerᴛcompilerHost
