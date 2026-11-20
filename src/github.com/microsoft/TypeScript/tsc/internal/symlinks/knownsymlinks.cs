namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static symlinks_package;

partial class symlinks_package {

partial struct KnownDirectoryLink {
    public @string Real;
    public tspath.Path RealPath;
}

partial struct KnownSymlinks {
    internal collections.SyncMap<tspath.Path, ж<KnownDirectoryLink>> directories;
    internal collections.SyncMap<tspath.Path, ж<collections.SyncSet<@string>>> directoriesByRealpath;
    internal collections.SyncMap<tspath.Path, @string> files;
    internal collections.SyncMap<tspath.Path, ж<collections.SyncSet<@string>>> filesByRealpath;
    internal @string cwd;
    internal bool useCaseSensitiveFileNames;
}

} partial class symlinksᴛKnownSymlinks {

public static bool HasDirectory(this ж<KnownSymlinks> Ꮡcache, tspath.Path symlinkPath) {
    var (_, ok) = Ꮡcache.Ꮡdirectories.Load(symlinkPath.EnsureTrailingDirectorySeparator());
    return ok;
}

public static ж<collections.SyncMap<tspath.Path, ж<KnownDirectoryLink>>> Directories(this ж<KnownSymlinks> Ꮡcache) {
    return Ꮡcache.Ꮡdirectories;
}

public static ж<collections.SyncMap<tspath.Path, ж<collections.SyncSet<@string>>>> DirectoriesByRealpath(this ж<KnownSymlinks> Ꮡcache) {
    return Ꮡcache.ᏑdirectoriesByRealpath;
}

public static ж<collections.SyncMap<tspath.Path, @string>> Files(this ж<KnownSymlinks> Ꮡcache) {
    return Ꮡcache.Ꮡfiles;
}

public static ж<collections.SyncMap<tspath.Path, ж<collections.SyncSet<@string>>>> FilesByRealpath(this ж<KnownSymlinks> Ꮡcache) {
    return Ꮡcache.ᏑfilesByRealpath;
}

public static void SetDirectory(this ж<KnownSymlinks> Ꮡcache, @string symlink, tspath.Path symlinkPath, ж<KnownDirectoryLink> ᏑrealDirectory) {
    ref var realDirectory = ref ᏑrealDirectory.DerefOrNull();

    if (ᏑrealDirectory != nil) {
        var (_, ok) = Ꮡcache.Ꮡdirectories.Load(symlinkPath); if (!ok) {
            var (set, _) = Ꮡcache.ᏑdirectoriesByRealpath.LoadOrStore(realDirectory.RealPath, Ꮡ(new collections.SyncSet<@string>()));
            set.Add(symlink);
        }
    }
    Ꮡcache.Ꮡdirectories.Store(symlinkPath, ᏑrealDirectory);
}

public static void SetFile(this ж<KnownSymlinks> Ꮡcache, @string symlink, tspath.Path symlinkPath, @string realpath) {
    ref var cache = ref Ꮡcache.DerefOrNull();

    var (_, ok) = Ꮡcache.Ꮡfiles.Load(symlinkPath); if (!ok) {
        tspath.Path realpathPath = tspath.ToPath(realpath, cache.cwd, cache.useCaseSensitiveFileNames);
        var (set, _) = Ꮡcache.ᏑfilesByRealpath.LoadOrStore(realpathPath, Ꮡ(new collections.SyncSet<@string>()));
        set.Add(symlink);
    }
    Ꮡcache.Ꮡfiles.Store(symlinkPath, realpath);
}

} partial class symlinks_package {

public static ж<KnownSymlinks> NewKnownSymlink(@string currentDirectory, bool useCaseSensitiveFileNames) {
    return Ꮡ(new KnownSymlinks(
        cwd: currentDirectory,
        useCaseSensitiveFileNames: useCaseSensitiveFileNames
    ));
}

} partial class symlinksᴛKnownSymlinks {

public static void SetSymlinksFromResolutions(this ж<KnownSymlinks> Ꮡcache, Action<Action<ж<module.ResolvedModule>, @string, coreꓸResolutionMode, tspath.Path>, ж<ast.SourceFile>> forEachResolvedModule, Action<Action<ж<module.ResolvedTypeReferenceDirective>, @string, coreꓸResolutionMode, tspath.Path>, ж<ast.SourceFile>> forEachResolvedTypeReferenceDirective) {
    forEachResolvedModule((ж<module.ResolvedModule> resolution, @string moduleName, coreꓸResolutionMode mode, tspath.Path filePath) => {
        Ꮡcache.ProcessResolution(resolution.OriginalPath, resolution.ResolvedFileName);
    }, nil);
    forEachResolvedTypeReferenceDirective((ж<module.ResolvedTypeReferenceDirective> resolution, @string moduleName, coreꓸResolutionMode mode, tspath.Path filePath) => {
        Ꮡcache.ProcessResolution(resolution.OriginalPath, resolution.ResolvedFileName);
    }, nil);
}

public static void ProcessResolution(this ж<KnownSymlinks> Ꮡcache, @string originalPath, @string resolvedFileName) {
    ref var cache = ref Ꮡcache.DerefOrNull();

    if (originalPath == ""u8 || resolvedFileName == ""u8) {
        return;
    }
    Ꮡcache.SetFile(originalPath, tspath.ToPath(originalPath, cache.cwd, cache.useCaseSensitiveFileNames), resolvedFileName);
    var (commonResolved, commonOriginal) = cache.guessDirectorySymlink(resolvedFileName, originalPath, cache.cwd);
    if (commonResolved != ""u8 && commonOriginal != ""u8) {
        tspath.Path symlinkPath = tspath.ToPath(commonOriginal, cache.cwd, cache.useCaseSensitiveFileNames);
        if (!tspath.ContainsIgnoredPath((@string)symlinkPath)) {
            Ꮡcache.SetDirectory(
                commonOriginal,
                symlinkPath.EnsureTrailingDirectorySeparator(),
                Ꮡ(new KnownDirectoryLink(
                    Real: tspath.EnsureTrailingDirectorySeparator(commonResolved),
                    RealPath: tspath.ToPath(commonResolved, cache.cwd, cache.useCaseSensitiveFileNames).EnsureTrailingDirectorySeparator()
                )));
        }
    }
}

internal static (@string, @string) guessDirectorySymlink(this ref KnownSymlinks cache, @string a, @string b, @string cwd) {
    var aParts = tspath.GetPathComponents(tspath.GetNormalizedAbsolutePath(a, cwd), "");
    var bParts = tspath.GetPathComponents(tspath.GetNormalizedAbsolutePath(b, cwd), "");
    var isDirectory = false;
    while (len(aParts) >= 2 && len(bParts) >= 2 && !cache.isNodeModulesOrScopedPackageDirectory(aParts[len(aParts) - 2]) && !cache.isNodeModulesOrScopedPackageDirectory(bParts[len(bParts) - 2]) && tspath.GetCanonicalFileName(aParts[len(aParts) - 1], cache.useCaseSensitiveFileNames) == tspath.GetCanonicalFileName(bParts[len(bParts) - 1], cache.useCaseSensitiveFileNames)) {
        aParts = aParts.slice(0, len(aParts) - 1);
        bParts = bParts.slice(0, len(bParts) - 1);
        isDirectory = true;
    }
    if (isDirectory) {
        return (tspath.GetPathFromPathComponents(aParts), tspath.GetPathFromPathComponents(bParts));
    }
    return ("", "");
}

internal static bool isNodeModulesOrScopedPackageDirectory(this ref KnownSymlinks cache, @string s) {
    return s != ""u8 && (tspath.GetCanonicalFileName(s, cache.useCaseSensitiveFileNames) == "node_modules"u8 || strings.HasPrefix(s, "@"));
}

} // end symlinksᴛKnownSymlinks
