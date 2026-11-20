namespace go.github.com.microsoft.TypeScript.tsc.@internal.vfs;

using fmt = fmt_package;
using fs = io.fs_package;
using strings = strings_package;
using time = time_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using @internal = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.internal_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;
using io;
using static iovfs_package;

partial class iovfs_package {

partial interface RealpathFS :
    fs.FS
{
    (@string, error) Realpath(@string path);
}

partial interface WritableFS :
    fs.FS
{
    error WriteFile(@string path, @string data, fs.FileMode perm);
    error AppendFile(@string path, @string data, fs.FileMode perm);
    error MkdirAll(@string path, fs.FileMode perm);
    error Remove(@string path);
    error Chtimes(@string path, time.Time aTime, time.Time mTime);
}

partial interface FsWithSys :
    vfs.FS
{
    fs.FS FSys();
}

public static FsWithSys From(fs.FS fsys, bool useCaseSensitiveFileNames) {
    Func<@string, (@string, error)> realpath = default;
    {
        var (fsysΔ1, ok) = fsys._<RealpathFS>(ᐧ); if (ok) {
            var fsysʗ1 = fsysΔ1;
            realpath = (@string, error) (@string path) => {
                var (rest, hadSlash) = strings.CutPrefix(path, "/");
                var (rp, err) = fsysʗ1.Realpath(rest);
                if (err != nil) {
                    return ("", err);
                }
                if (hadSlash) {
                    return ("/" + rp, default);
                }
                return (rp, default);
            };
        } else {
            realpath = (@string, error) (@string path) => (path, default);
        }
    }

    Func<@string, @string, error> writeFile = default;
    Func<@string, @string, error> appendFile = default;
    Func<@string, error> mkdirAll = default;
    Func<@string, error> remove = default;
    Func<@string, time.Time, time.Time, error> chtimes = default;
    {
        var (fsysΔ2, ok) = fsys._<WritableFS>(ᐧ); if (ok) {
            var fsysʗ2 = fsysΔ2;
            writeFile = (@string path, @string content) => {
                var (rest, _) = strings.CutPrefix(path, "/");
                return fsysʗ2.WriteFile(rest, content, 438);
            };
            var fsysʗ3 = fsysΔ2;
            appendFile = (@string path, @string content) => {
                var (rest, _) = strings.CutPrefix(path, "/");
                return fsysʗ3.AppendFile(rest, content, 438);
            };
            var fsysʗ4 = fsysΔ2;
            mkdirAll = (@string path) => {
                var (rest, _) = strings.CutPrefix(path, "/");
                return fsysʗ4.MkdirAll(rest, 511);
            };
            var fsysʗ5 = fsysΔ2;
            remove = (@string path) => {
                var (rest, _) = strings.CutPrefix(path, "/");
                return fsysʗ5.Remove(rest);
            };
            var fsysʗ6 = fsysΔ2;
            chtimes = (@string path, time.Time aTime, time.Time mTime) => {
                var (rest, _) = strings.CutPrefix(path, "/");
                return fsysʗ6.Chtimes(rest, aTime, mTime);
            };
        } else {
            writeFile = error (@string _Δp0, @string _Δp1) => {
                throw panic("writeFile not supported");
            };
            appendFile = error (@string _Δp0, @string _Δp1) => {
                throw panic("appendFile not supported");
            };
            mkdirAll = error (@string _Δp0) => {
                throw panic("mkdirAll not supported");
            };
            remove = error (@string _Δp0) => {
                throw panic("remove not supported");
            };
            chtimes = error (@string _Δp0, time.Time _Δp1, time.Time _Δp2) => {
                throw panic("chtimes not supported");
            };
        }
    }

    return new ioFSжFsWithSys(Ꮡ(new ioFS(
        common: new @internal.Common(
            RootFor: fs.FS (@string root) => {
                if (root == "/"u8) {
                    return fsys;
                }

                @string p = tspath.RemoveTrailingDirectorySeparator(root);
                var (sub, err) = fs.Sub(fsys, p);
                if (err != nil) {
                    if (tspath.IsUrl(root)) {
                        return default;
                    }
                    throw panic(fmt.Sprintf("vfs: failed to create sub file system for %q: %v"u8, p, err));
                }
                return sub;
            }
        ),
        useCaseSensitiveFileNames: useCaseSensitiveFileNames,
        realpath: realpath,
        writeFile: writeFile,
        appendFile: appendFile,
        mkdirAll: mkdirAll,
        remove: remove,
        chtimes: chtimes,
        fsys: fsys
    )));
}

partial struct ioFS {
    internal @internal.Common common;
    internal bool useCaseSensitiveFileNames;
    internal Func<@string, (@string, error)> realpath;
    internal Func<@string, @string, error> writeFile;
    internal Func<@string, @string, error> appendFile;
    internal Func<@string, error> mkdirAll;
    internal Func<@string, error> remove;
    internal Func<@string, time.Time, time.Time, error> chtimes;
    internal fs.FS fsys;
}

internal static FsWithSys _ᴛ1ʗ = new ioFSжFsWithSys(((ж<ioFS>)nil));

} partial class iovfsᴛioFS {

internal static bool UseCaseSensitiveFileNames(this ref ioFS vfs) {
    return vfs.useCaseSensitiveFileNames;
}

internal static bool DirectoryExists(this ref ioFS vfs, @string path) {
    return vfs.common.DirectoryExists(path);
}

internal static bool FileExists(this ref ioFS vfs, @string path) {
    return vfs.common.FileExists(path);
}

internal static vfs.Entries GetAccessibleEntries(this ref ioFS vfsΔ1, @string path) {
    return vfsΔ1.common.GetAccessibleEntries(path);
}

internal static fs.FileInfo Stat(this ref ioFS vfsΔ1, @string path) {
    _ = @internal.RootLength(path);
    return vfsΔ1.common.Stat(path);
}

internal static (@string contents, bool ok) ReadFile(this ref ioFS vfs, @string path) {
    return vfs.common.ReadFile(path);
}

internal static error Remove(this ref ioFS vfs, @string path) {
    _ = @internal.RootLength(path);
    return vfs.remove(path);
}

internal static error Chtimes(this ref ioFS vfs, @string path, time.Time aTime, time.Time mTime) {
    _ = @internal.RootLength(path);
    return vfs.chtimes(path, aTime, mTime);
}

internal static @string Realpath(this ref ioFS vfs, @string path) {
    var (root, rest) = @internal.SplitPath(path);
    var (realpath, err) = vfs.realpath(root + rest);
    if (err != nil) {
        return path;
    }
    return realpath;
}

internal static error writeFileEnsuringDir(this ref ioFS vfs, @string path, @string content, Func<@string, @string, error> write) {
    _ = @internal.RootLength(path);
    {
        var err = write(path, content); if (err == nil) {
            return default;
        }
    }
    {
        var err = vfs.mkdirAll(tspath.GetDirectoryPath(tspath.NormalizePath(path))); if (err != nil) {
            return err;
        }
    }
    return write(path, content);
}

internal static error WriteFile(this ref ioFS vfs, @string path, @string content) {
    return vfs.writeFileEnsuringDir(path, content, vfs.writeFile);
}

internal static error AppendFile(this ref ioFS vfs, @string path, @string content) {
    return vfs.writeFileEnsuringDir(path, content, vfs.appendFile);
}

internal static fs.FS FSys(this ref ioFS vfs) {
    return vfs.fsys;
}

} // end iovfsᴛioFS
