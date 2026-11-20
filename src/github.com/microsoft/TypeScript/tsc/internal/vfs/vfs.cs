global using DirEntry = go.io.fs_package.DirEntry;
global using FileInfo = go.io.fs_package.FileInfo;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fs = io.fs_package;
using time = time_package;
using io;

partial class vfs_package {

partial interface FS {
    bool UseCaseSensitiveFileNames();
    bool FileExists(@string path);
    (@string contents, bool ok) ReadFile(@string path);
    error WriteFile(@string path, @string data);
    error AppendFile(@string path, @string data);
    error Remove(@string path);
    error Chtimes(@string path, time.Time aTime, time.Time mTime);
    bool DirectoryExists(@string path);
    Entries GetAccessibleEntries(@string path);
    FileInfo Stat(@string path);
    @string Realpath(@string path);
}

partial struct Entries {
    public slice<@string> Files;
    public slice<@string> Directories;
    public map<@string, EmptyStruct> Symlinks;
}

public static error ErrInvalid = fs.ErrInvalid;
public static error ErrPermission = fs.ErrPermission;
public static error ErrExist = fs.ErrExist;
public static error ErrNotExist = fs.ErrNotExist;
public static error ErrClosed = fs.ErrClosed;

} // end vfs_package
