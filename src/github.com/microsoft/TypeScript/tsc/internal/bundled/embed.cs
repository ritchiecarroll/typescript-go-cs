namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fs = io.fs_package;
using strings = strings_package;
using time = time_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using io;
using static bundled_package;

partial class bundled_package {

internal const bool embedded = true;

internal static readonly @string scheme = "bundled:///"u8;

internal static (@string rest, bool ok) splitPath(@string path) {
    return strings.CutPrefix(path, scheme);
}

internal static @string libPath() {
    return scheme + "libs";
}

public static bool IsBundled(@string path) {
    var (_, ok) = splitPath(path);
    return ok;
}

partial struct wrappedFS {
    internal vfs.FS fs;
}

internal static vfs.FS _ᴛ1ʗ = new wrappedFSжFS(((ж<wrappedFS>)nil));

internal static vfs.FS wrapFS(vfs.FS fs) {
    return new wrappedFSжFS(Ꮡ(new wrappedFS(fs: fs)));
}

} partial class bundledᴛwrappedFS {

internal static bool UseCaseSensitiveFileNames(this ref wrappedFS vfs) {
    return vfs.fs.UseCaseSensitiveFileNames();
}

internal static bool FileExists(this ref wrappedFS vfs, @string path) {
    var (rest, ok) = splitPath(path); if (ok) {
        var (_, okΔ1) = embeddedContents[rest, ꟷ];
        return okΔ1;
    }
    return vfs.fs.FileExists(path);
}

internal static (@string contents, bool ok) ReadFile(this ref wrappedFS vfs, @string path) {
    @string contents = default;

    var (rest, okΔ1) = splitPath(path); if (okΔ1) {
        (contents, okΔ1) = embeddedContents[rest, ꟷ];
        return (contents, okΔ1);
    }
    return vfs.fs.ReadFile(path);
}

internal static bool DirectoryExists(this ref wrappedFS vfs, @string path) {
    var (rest, ok) = splitPath(path); if (ok) {
        return rest == "libs"u8;
    }
    return vfs.fs.DirectoryExists(path);
}

internal static vfs.Entries /*result*/ GetAccessibleEntries(this ref wrappedFS vfsΔ1, @string path) {
    vfs.Entries result = default;

    var (rest, ok) = splitPath(path); if (ok) {
        if (rest == ""u8) {
            result.Directories = ["libs"];
        } else if (rest == "libs"u8) {
            result.Files = LibNames;
        }
        return result;
    }
    return vfsΔ1.fs.GetAccessibleEntries(path);
}

internal static fs.FileInfo Stat(this ref wrappedFS vfsΔ1, @string path) {
    ref var rest = ref heap<@string>(out var Ꮡrest);
    (rest, var ok) = splitPath(path); if (ok) {
        if (rest == ""u8 || rest == "libs"u8) {
            return new fileInfoжFileInfo(Ꮡ(new fileInfo(name: rest, mode: fs.ModeDir)));
        }
        var (lib, okΔ1) = embeddedContents[rest, ꟷ]; if (okΔ1) {
            ref var libName = ref heap<@string>(out var ᏑlibName);
            (libName, _) = strings.CutPrefix(rest, "libs/");
            return new fileInfoжFileInfo(Ꮡ(new fileInfo(name: libName, size: (int64)len(lib))));
        }
        return default;
    }
    return vfsΔ1.fs.Stat(path);
}

internal static @string Realpath(this ref wrappedFS vfs, @string path) {
    var (_, ok) = splitPath(path); if (ok) {
        return path;
    }
    return vfs.fs.Realpath(path);
}

internal static error WriteFile(this ref wrappedFS vfs, @string path, @string data) {
    var (_, ok) = splitPath(path); if (ok) {
        throw panic("cannot write to embedded file system");
    }
    return vfs.fs.WriteFile(path, data);
}

internal static error AppendFile(this ref wrappedFS vfs, @string path, @string data) {
    var (_, ok) = splitPath(path); if (ok) {
        throw panic("cannot write to embedded file system");
    }
    return vfs.fs.AppendFile(path, data);
}

internal static error Remove(this ref wrappedFS vfs, @string path) {
    var (_, ok) = splitPath(path); if (ok) {
        throw panic("cannot remove from embedded file system");
    }
    return vfs.fs.Remove(path);
}

internal static error Chtimes(this ref wrappedFS vfs, @string path, time.Time aTime, time.Time mTime) {
    var (_, ok) = splitPath(path); if (ok) {
        throw panic("cannot change times on embedded file system");
    }
    return vfs.fs.Chtimes(path, aTime, mTime);
}

} partial class bundled_package {

partial struct fileInfo {
    internal fs.FileMode mode;
    internal @string name;
    internal int64 size;
}

internal static fs.FileInfo _ᴛ2ʗ = new fileInfoжFileInfo(((ж<fileInfo>)nil));
internal static fs.DirEntry _ᴛ3ʗ = new fileInfoжDirEntry(((ж<fileInfo>)nil));

} partial class bundledᴛfileInfo {

internal static bool IsDir(this ref fileInfo fi) {
    return fi.mode.IsDir();
}

internal static time.Time ModTime(this ref fileInfo fi) {
    return new time.Time();
}

internal static fs.FileMode Mode(this ref fileInfo fi) {
    return fi.mode;
}

internal static @string Name(this ref fileInfo fi) {
    return fi.name;
}

internal static int64 Size(this ref fileInfo fi) {
    return fi.size;
}

internal static any Sys(this ref fileInfo fi) {
    return default;
}

internal static (fs.FileInfo, error) Info(this ж<fileInfo> Ꮡfi) {
    return (new fileInfoжFileInfo(Ꮡfi), default);
}

internal static fs.FileMode Type(this ref fileInfo fi) {
    return fi.mode.Type();
}

} // end bundledᴛfileInfo
