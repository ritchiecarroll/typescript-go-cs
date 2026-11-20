namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using time = time_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using symlinks = go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using cachedvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.cachedvfs_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using fs = go.io.fs_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using static compiler_package;

partial class compiler_package {

partial struct projectReferenceDtsFakingHost {
    internal CompilerHost host;
    internal ж<cachedvfs.FS> fs;
}

internal static module.ResolutionHost _ᴛ6ʗ = new projectReferenceDtsFakingHostжResolutionHost(((ж<projectReferenceDtsFakingHost>)nil));

internal static module.ResolutionHost newProjectReferenceDtsFakingHost(ref fileLoader loader) {
    var host = Ꮡ(new projectReferenceDtsFakingHost(
        host: loader.opts.Host,
        fs: cachedvfs.From(new projectReferenceDtsFakingVfsжFS(Ꮡ(new projectReferenceDtsFakingVfs(
            projectReferenceFileMapper: loader.projectReferenceFileMapper,
            dtsDirectories: loader.dtsDirectories,
            knownSymlinks: new symlinks.KnownSymlinks()
        ))))
    ));
    return new projectReferenceDtsFakingHostжResolutionHost(host);
}

} partial class compilerᴛprojectReferenceDtsFakingHost {

internal static Δvfs.FS FS(this ref projectReferenceDtsFakingHost h) {
    return new cachedvfs.FSжFS(h.fs);
}

internal static @string GetCurrentDirectory(this ref projectReferenceDtsFakingHost h) {
    return h.host.GetCurrentDirectory();
}

} partial class compiler_package {

partial struct projectReferenceDtsFakingVfs {
    internal ж<projectReferenceFileMapper> projectReferenceFileMapper;
    internal collections.Set<tspath.Path> dtsDirectories;
    internal symlinks.KnownSymlinks knownSymlinks;
}

internal static Δvfs.FS _ᴛ7ʗ = new projectReferenceDtsFakingVfsжFS(((ж<projectReferenceDtsFakingVfs>)nil));

} partial class compilerᴛprojectReferenceDtsFakingVfs {

internal static bool UseCaseSensitiveFileNames(this ref projectReferenceDtsFakingVfs fs) {
    return fs.projectReferenceFileMapper.opts.Host.FS().UseCaseSensitiveFileNames();
}

internal static bool FileExists(this ж<projectReferenceDtsFakingVfs> Ꮡfs, @string path) {
    ref var fs = ref Ꮡfs.DerefOrNull();

    if (fs.projectReferenceFileMapper.opts.Host.FS().FileExists(path)) {
        return true;
    }
    if (!tspath.IsDeclarationFileName(path)) {
        return false;
    }
    return Ꮡfs.fileOrDirectoryExistsUsingSource(path, true);
}

internal static (@string contents, bool ok) ReadFile(this ref projectReferenceDtsFakingVfs fs, @string path) {
    return fs.projectReferenceFileMapper.opts.Host.FS().ReadFile(path);
}

internal static error WriteFile(this ref projectReferenceDtsFakingVfs fs, @string path, @string data) {
    throw panic("should not be called by resolver");
}

internal static error AppendFile(this ref projectReferenceDtsFakingVfs fs, @string path, @string data) {
    throw panic("should not be called by resolver");
}

internal static error Remove(this ref projectReferenceDtsFakingVfs fs, @string path) {
    throw panic("should not be called by resolver");
}

internal static error Chtimes(this ref projectReferenceDtsFakingVfs fs, @string path, time.Time aTime, time.Time mTime) {
    throw panic("should not be called by resolver");
}

internal static bool DirectoryExists(this ж<projectReferenceDtsFakingVfs> Ꮡfs, @string path) {
    ref var fs = ref Ꮡfs.DerefOrNull();

    if (fs.projectReferenceFileMapper.opts.Host.FS().DirectoryExists(path)) {
        Ꮡfs.handleDirectoryCouldBeSymlink(path);
        return true;
    }
    return Ꮡfs.fileOrDirectoryExistsUsingSource(path, false);
}

internal static Δvfs.Entries GetAccessibleEntries(this ref projectReferenceDtsFakingVfs fs, @string path) {
    throw panic("should not be called by resolver");
}

internal static fs.FileInfo Stat(this ref projectReferenceDtsFakingVfs fs, @string path) {
    throw panic("should not be called by resolver");
}

internal static @string Realpath(this ж<projectReferenceDtsFakingVfs> Ꮡfs, @string path) {
    ref var fs = ref Ꮡfs.DerefOrNull();

    var (result, ok) = Ꮡfs.ᏑknownSymlinks.Files().Load(fs.toPath(path));
    if (ok) {
        return result;
    }
    return fs.projectReferenceFileMapper.opts.Host.FS().Realpath(path);
}

internal static tspath.Path toPath(this ref projectReferenceDtsFakingVfs fs, @string path) {
    return tspath.ToPath(path, fs.projectReferenceFileMapper.opts.Host.GetCurrentDirectory(), fs.UseCaseSensitiveFileNames());
}

internal static void handleDirectoryCouldBeSymlink(this ж<projectReferenceDtsFakingVfs> Ꮡfs, @string directory) {
    ref var fs = ref Ꮡfs.DerefOrNull();

    if (tspath.ContainsIgnoredPath(directory)) {
        return;
    }

    if (!strings.Contains(directory, "/node_modules/")) {
        return;
    }

    tspath.Path directoryPath = (tspath.Path)tspath.EnsureTrailingDirectorySeparator((@string)fs.toPath(directory));
    var (_, ok) = Ꮡfs.ᏑknownSymlinks.Directories().Load(directoryPath); if (ok) {
        return;
    }

    @string realDirectory = Ꮡfs.Realpath(directory);
    ref var realPath = ref heap(new tspath.Path(), out var ᏑrealPath);
    if (realDirectory == directory) {
        return;
    }
    realPath = (tspath.Path)tspath.EnsureTrailingDirectorySeparator((@string)fs.toPath(realDirectory)); if (realPath == directoryPath) {
        return;
    }
    Ꮡfs.ᏑknownSymlinks.SetDirectory(directory, directoryPath, Ꮡ(new symlinks.KnownDirectoryLink(
        Real: tspath.EnsureTrailingDirectorySeparator(realDirectory),
        RealPath: realPath
    )));
}

internal static bool fileOrDirectoryExistsUsingSource(this ж<projectReferenceDtsFakingVfs> Ꮡfs, @string fileOrDirectory, bool isFile) {
    ref var fs = ref Ꮡfs.DerefOrNull();

    var fileOrDirectoryExistsUsingSource = core.IfElse<Func<@string, core.Tristate>>(isFile, Ꮡfs.fileExistsIfProjectReferenceDts, Ꮡfs.directoryExistsIfProjectReferenceDeclDir);
    var result = fileOrDirectoryExistsUsingSource(fileOrDirectory);
    if (result != core.TSUnknown) {
        return result == core.TSTrue;
    }

    tspath.Path fileOrDirectoryPath = fs.toPath(fileOrDirectory);
    if (!strings.Contains((@string)fileOrDirectoryPath, "/node_modules/")) {
        return false;
    }
    @string packageRoot = module.ParseNodeModuleFromPath(fileOrDirectory, true); if (packageRoot != ""u8) {
        Ꮡfs.handleDirectoryCouldBeSymlink(packageRoot);
    }
    var knownDirectoryLinks = Ꮡfs.ᏑknownSymlinks.Directories();
    if (knownDirectoryLinks.Size() == 0) {
        return false;
    }
    if (isFile) {
        var (_, ok) = Ꮡfs.ᏑknownSymlinks.Files().Load(fileOrDirectoryPath);
        if (ok) {
            return true;
        }
    }

    bool exists = default;
    var fileOrDirectoryExistsUsingSourceʗ1 = fileOrDirectoryExistsUsingSource;
    knownDirectoryLinks.Range((tspath.Path directoryPath, ж<symlinks.KnownDirectoryLink> knownDirectoryLink) => {
        var (relative, hasPrefix) = strings.CutPrefix((@string)fileOrDirectoryPath, (@string)directoryPath);
        if (!hasPrefix) {
            return true;
        }
        exists = fileOrDirectoryExistsUsingSourceʗ1(((@string)knownDirectoryLink.RealPath) + relative).IsTrue(); if (exists) {
            if (isFile) {
                @string absolutePath = tspath.GetNormalizedAbsolutePath(fileOrDirectory, Ꮡfs.Value.projectReferenceFileMapper.opts.Host.GetCurrentDirectory());
                Ꮡfs.ᏑknownSymlinks.SetFile(
                    absolutePath,
                    fileOrDirectoryPath,
                    knownDirectoryLink.Real + absolutePath.slice(len(directoryPath)));
            }
            return false;
        }
        return true;
    });
    return exists;
}

internal static core.Tristate fileExistsIfProjectReferenceDts(this ref projectReferenceDtsFakingVfs fs, @string @file) {
    var source = fs.projectReferenceFileMapper.getProjectReferenceFromOutputDts(fs.toPath(@file));
    if (source != nil) {
        return core.IfElse(fs.projectReferenceFileMapper.opts.Host.FS().FileExists(source.Source), core.TSTrue, core.TSFalse);
    }
    return core.TSUnknown;
}

internal static core.Tristate directoryExistsIfProjectReferenceDeclDir(this ж<projectReferenceDtsFakingVfs> Ꮡfs, @string dir) {
    ref var fs = ref Ꮡfs.DerefOrNull();

    tspath.Path dirPath = fs.toPath(dir);
    foreach (var (declDirPath, _) in Ꮡfs.ᏑdtsDirectories.Keys()) {
        if (dirPath.ContainsPath(declDirPath) || declDirPath.ContainsPath(dirPath)) {
            return core.TSTrue;
        }
    }
    return core.TSUnknown;
}

} // end compilerᴛprojectReferenceDtsFakingVfs
