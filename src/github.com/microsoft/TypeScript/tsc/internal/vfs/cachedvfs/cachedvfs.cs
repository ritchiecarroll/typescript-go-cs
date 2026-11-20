namespace go.github.com.microsoft.TypeScript.tsc.@internal.vfs;

using atomic = sync.atomic_package;
using time = time_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using fs = io.fs_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using sync;
using static cachedvfs_package;

partial class cachedvfs_package {

partial struct FS {
    internal vfs.FS fs;
    internal atomic.Bool enabled;
    internal collections.SyncMap<@string, bool> directoryExistsCache;
    internal collections.SyncMap<@string, bool> fileExistsCache;
    internal collections.SyncMap<@string, vfs.Entries> getAccessibleEntriesCache;
    internal collections.SyncMap<@string, @string> realpathCache;
    internal collections.SyncMap<@string, fs.FileInfo> statCache;
}

internal static vfs.FS _ᴛ1ʗ = new FSжFS(((ж<FS>)nil));

public static ж<FS> From(vfs.FS fs) {
    var fsys = Ꮡ(new FS(fs: fs));
    fsys.Ꮡenabled.Store(true);
    return fsys;
}

} partial class cachedvfsᴛFS {

public static void DisableAndClearCache(this ж<FS> Ꮡfsys) {
    if (Ꮡfsys.Ꮡenabled.CompareAndSwap(true, false)) {
        Ꮡfsys.ClearCache();
    }
}

public static void Enable(this ж<FS> Ꮡfsys) {
    Ꮡfsys.Ꮡenabled.Store(true);
}

public static void ClearCache(this ж<FS> Ꮡfsys) {
    Ꮡfsys.ᏑdirectoryExistsCache.Clear();
    Ꮡfsys.ᏑfileExistsCache.Clear();
    Ꮡfsys.ᏑgetAccessibleEntriesCache.Clear();
    Ꮡfsys.ᏑrealpathCache.Clear();
    Ꮡfsys.ᏑstatCache.Clear();
}

public static bool DirectoryExists(this ж<FS> Ꮡfsys, @string path) {
    ref var fsys = ref Ꮡfsys.DerefOrNull();

    if (Ꮡfsys.Ꮡenabled.Load()) {
        var (retΔ1, ok) = Ꮡfsys.ᏑdirectoryExistsCache.Load(path); if (ok) {
            return retΔ1;
        }
    }

    var ret = fsys.fs.DirectoryExists(path);

    if (Ꮡfsys.Ꮡenabled.Load()) {
        Ꮡfsys.ᏑdirectoryExistsCache.Store(path, ret);
    }

    return ret;
}

public static bool FileExists(this ж<FS> Ꮡfsys, @string path) {
    ref var fsys = ref Ꮡfsys.DerefOrNull();

    if (Ꮡfsys.Ꮡenabled.Load()) {
        var (retΔ1, ok) = Ꮡfsys.ᏑfileExistsCache.Load(path); if (ok) {
            return retΔ1;
        }
    }

    var ret = fsys.fs.FileExists(path);

    if (Ꮡfsys.Ꮡenabled.Load()) {
        Ꮡfsys.ᏑfileExistsCache.Store(path, ret);
    }

    return ret;
}

public static vfs.Entries GetAccessibleEntries(this ж<FS> Ꮡfsys, @string path) {
    ref var fsys = ref Ꮡfsys.DerefOrNull();

    if (Ꮡfsys.Ꮡenabled.Load()) {
        var (retΔ1, ok) = Ꮡfsys.ᏑgetAccessibleEntriesCache.Load(path); if (ok) {
            return retΔ1;
        }
    }

    var ret = fsys.fs.GetAccessibleEntries(path);

    if (Ꮡfsys.Ꮡenabled.Load()) {
        Ꮡfsys.ᏑgetAccessibleEntriesCache.Store(path, ret);
    }

    return ret;
}

public static (@string contents, bool ok) ReadFile(this ref FS fsys, @string path) {
    return fsys.fs.ReadFile(path);
}

public static @string Realpath(this ж<FS> Ꮡfsys, @string path) {
    ref var fsys = ref Ꮡfsys.DerefOrNull();

    if (Ꮡfsys.Ꮡenabled.Load()) {
        var (retΔ1, ok) = Ꮡfsys.ᏑrealpathCache.Load(path); if (ok) {
            return retΔ1;
        }
    }

    @string ret = fsys.fs.Realpath(path);

    if (Ꮡfsys.Ꮡenabled.Load()) {
        Ꮡfsys.ᏑrealpathCache.Store(path, ret);
    }

    return ret;
}

public static error Remove(this ref FS fsys, @string path) {
    return fsys.fs.Remove(path);
}

public static error Chtimes(this ref FS fsys, @string path, time.Time aTime, time.Time mTime) {
    return fsys.fs.Chtimes(path, aTime, mTime);
}

public static fs.FileInfo Stat(this ж<FS> Ꮡfsys, @string path) {
    ref var fsys = ref Ꮡfsys.DerefOrNull();

    if (Ꮡfsys.Ꮡenabled.Load()) {
        var (retΔ1, ok) = Ꮡfsys.ᏑstatCache.Load(path); if (ok) {
            return retΔ1;
        }
    }

    var ret = fsys.fs.Stat(path);

    if (Ꮡfsys.Ꮡenabled.Load()) {
        Ꮡfsys.ᏑstatCache.Store(path, ret);
    }

    return ret;
}

public static bool UseCaseSensitiveFileNames(this ref FS fsys) {
    return fsys.fs.UseCaseSensitiveFileNames();
}

public static error WriteFile(this ref FS fsys, @string path, @string data) {
    return fsys.fs.WriteFile(path, data);
}

public static error AppendFile(this ref FS fsys, @string path, @string data) {
    return fsys.fs.AppendFile(path, data);
}

} // end cachedvfsᴛFS
