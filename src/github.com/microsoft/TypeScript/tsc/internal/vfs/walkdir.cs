namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using errors = errors_package;
using fs = io.fs_package;
using slices = slices_package;
using time = time_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using io;
using static vfs_package;

partial class vfs_package {

public static error WalkDir(FS fileSystem, @string root, Func<@string, fs.DirEntry, error, error> walkFn) {
    var rootInfo = fileSystem.Stat(root);
    if (rootInfo == nil) {
        return normalizeWalkDirError(walkFn(root, default, ErrNotExist));
    }

    var useCaseSensitiveFileNames = fileSystem.UseCaseSensitiveFileNames();
    @string rootPrefix = root.slice(0, tspath.GetRootLength(root));
    bool sameRoot(@string path) {
        nint pathRootLength = tspath.GetRootLength(path);
        return pathRootLength == len(rootPrefix) && tspath.ComparePaths(
            path.slice(0, pathRootLength),
            rootPrefix,
            new tspath.ComparePathsOptions(UseCaseSensitiveFileNames: useCaseSensitiveFileNames)) == 0;
    }
    bool equivalent(@string left, @string right) => tspath.ComparePaths(
            left,
            right,
            new tspath.ComparePathsOptions(UseCaseSensitiveFileNames: useCaseSensitiveFileNames)) == 0;
    @string canonicalize(@string path) => tspath.GetCanonicalFileName(tspath.NormalizePath(path), useCaseSensitiveFileNames);

    var visited = new map<@string, EmptyStruct>{};
    ref var visit = ref heap<Func<@string, fs.DirEntry, @string, error>>(out var Ꮡvisit);
    var canonicalizeʗ1 = canonicalize;
    var equivalentʗ1 = equivalent;
    var sameRootʗ1 = sameRoot;
    var visitedʗ1 = visited;
    visit = error (@string path, fs.DirEntry entry, @string realpath) => {
        if (entry.IsDir()) {
            @string canonicalRealpath = canonicalizeʗ1(realpath);
            var (_, ok) = visitedʗ1[canonicalRealpath, ꟷ]; if (ok) {
                return default;
            }
            visitedʗ1[canonicalRealpath] = new EmptyStruct();
        }

        {
            var err = walkFn(path, entry, default); if (err != nil) {
                if (errors.Is(err, fs.SkipDir) && entry.IsDir()) {
                    return default;
                }
                return err;
            }
        }
        if (!entry.IsDir()) {
            return default;
        }

        var entries = fileSystem.GetAccessibleEntries(path);
        var directories = new map<@string, EmptyStruct>(len(entries.Directories));
        foreach (var (_, name) in range(entries.Directories)) {
            directories[name] = new EmptyStruct();
        }
        var names = appendꓸꓸꓸ(slices.Clone<slice<@string>, @string>(entries.Directories), entries.Files);
        slices.Sort<slice<@string>, @string>(names);
        foreach (var (_, vᴛ1) in range(names)) {
            ref var name = ref heap(new @string(), out var Ꮡname);
            name = vᴛ1;

            ref var childPath = ref heap<@string>(tspath.CombinePaths(path, name), out var ᏑchildPath);
            if (!sameRootʗ1(childPath)) {
                continue;
            }

            ref var mode = ref heap<fs.FileMode>((fs.FileMode)0, out var Ꮡmode);
            var (_, ok) = directories[name, ꟷ]; if (ok) {
                mode = fs.ModeDir;
            }
            @string childRealpath = "";
            var isSymlink = false;
            if (entries.Symlinks != nil) {
                (_, isSymlink) = entries.Symlinks[name, ꟷ];
                if (!isSymlink && mode.IsDir()) {
                    childRealpath = tspath.CombinePaths(realpath, name);
                }
            } else {
                childRealpath = fileSystem.Realpath(childPath);
                isSymlink = !equivalentʗ1(childRealpath, tspath.CombinePaths(realpath, name));
            }
            if (isSymlink) {
                mode = fs.ModeSymlink;
            }
            var childEntry = Ꮡ(new walkDirEntry(
                fileSystem: fileSystem,
                path: childPath,
                name: name,
                mode: mode
            ));
            if (!mode.IsDir()) {
                var err = Ꮡvisit.ValueSlot(childPath, new walkDirEntryжDirEntry(childEntry), ""); if (err != nil) {
                    if (errors.Is(err, fs.SkipDir)) {
                        return default;
                    }
                    return err;
                }
                continue;
            }

            if (childRealpath == ""u8) {
                childRealpath = fileSystem.Realpath(childPath);
            }
            {
                var err = Ꮡvisit.ValueSlot(childPath, new walkDirEntryжDirEntry(childEntry), childRealpath); if (err != nil) {
                    if (errors.Is(err, fs.SkipDir)) {
                        return default;
                    }
                    return err;
                }
            }
        }
        return default;
    };

    var rootEntry = fs.FileInfoToDirEntry(rootInfo);
    @string rootRealpath = fileSystem.Realpath(root);
    if (tspath.GetRootLength(root) != len(root)) {
        @string parent = tspath.GetDirectoryPath(root);
        @string expectedRealpath = tspath.CombinePaths(fileSystem.Realpath(parent), tspath.GetBaseFileName(root));
        if (!equivalent(rootRealpath, expectedRealpath)) {
            rootEntry = new walkDirEntryжDirEntry(Ꮡ(new walkDirEntry(
                fileSystem: fileSystem,
                path: root,
                name: tspath.GetBaseFileName(root),
                mode: fs.ModeSymlink
            )));
        }
    }
    return normalizeWalkDirError(visit(root, rootEntry, rootRealpath));
}

internal static error normalizeWalkDirError(error err) {
    if (errors.Is(err, fs.SkipDir) || errors.Is(err, fs.SkipAll)) {
        return default;
    }
    return err;
}

partial struct walkDirEntry {
    internal FS fileSystem;
    internal @string path;
    internal @string name;
    internal fs.FileMode mode;
}

} partial class vfsᴛwalkDirEntry {

internal static @string Name(this ref walkDirEntry e) { return e.name; }

internal static bool IsDir(this ref walkDirEntry e) { return e.mode.IsDir(); }

internal static fs.FileMode Type(this ref walkDirEntry e) { return e.mode.Type(); }

internal static (fs.FileInfo, error) Info(this ref walkDirEntry e) {
    if ((fs.FileMode)(e.mode & fs.ModeSymlink) != 0) {
        return (new walkDirFileInfoжFileInfo(Ꮡ(new walkDirFileInfo(name: e.name, mode: e.mode))), default);
    }
    var info = e.fileSystem.Stat(e.path);
    if (info == nil) {
        return (default, ErrNotExist);
    }
    return (info, default);
}

} partial class vfs_package {

partial struct walkDirFileInfo {
    internal @string name;
    internal fs.FileMode mode;
}

} partial class vfsᴛwalkDirFileInfo {

internal static @string Name(this ref walkDirFileInfo i) { return i.name; }
internal static int64 Size(this ref walkDirFileInfo i) { return 0; }
internal static fs.FileMode Mode(this ref walkDirFileInfo i) { return i.mode; }
internal static time.Time ModTime(this ref walkDirFileInfo i) { return new time.Time(); }
internal static bool IsDir(this ref walkDirFileInfo i) { return i.mode.IsDir(); }
internal static any Sys(this ref walkDirFileInfo i) { return default; }

} // end vfsᴛwalkDirFileInfo
