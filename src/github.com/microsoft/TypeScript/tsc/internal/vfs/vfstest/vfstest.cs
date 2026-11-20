namespace go.github.com.microsoft.TypeScript.tsc.@internal.vfs;

using errors = errors_package;
using fmt = fmt_package;
using fs = io.fs_package;
using iter = iter_package;
using maps = maps_package;
using path = path_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using fstest = testing.fstest_package;
using time = time_package;
using @unsafe = unsafe_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using iovfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.iovfs_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;
using io;
using testing;
using static vfstest_package;

partial class vfstest_package {

partial struct MapFS {
    internal sync.RWMutex mu;
    internal fstest.MapFS m;
    internal bool useCaseSensitiveFileNames;
    internal map<canonicalPath, canonicalPath> symlinks;
    internal Clock clock;
}

partial interface Clock {
    time.Time Now();
    time.Duration SinceStart();
}

partial struct clockImpl {
    internal time.Time start;
}

} partial class vfstestᴛclockImpl {

internal static time.Time Now(this ref clockImpl c) {
    return time.Now();
}

internal static time.Duration SinceStart(this ref clockImpl c) {
    return time.Since(c.start);
}

} partial class vfstest_package {

internal static iovfs.RealpathFS _ᴛ1ʗ = new MapFSжRealpathFS(((ж<MapFS>)nil));
internal static iovfs.WritableFS _ᴛ2ʗ = new MapFSжWritableFS(((ж<MapFS>)nil));

partial struct sys {
    internal any original;
    internal @string realpath;
}

public static vfs.FS FromMap<File>(map<@string, File> m, bool useCaseSensitiveFileNames) {
    return FromMapWithClock(m, useCaseSensitiveFileNames, new clockImplжClock(Ꮡ(new clockImpl(start: time.Now()))));
}

public static vfs.FS FromMapWithClock<File>(map<@string, File> m, bool useCaseSensitiveFileNames, Clock clock) {
    var posix = false;
    var windows = false;

    void checkPath(@string p) {
        if (!tspath.IsRootedDiskPath(p)) {
            throw panic(fmt.Sprintf("non-rooted path %q"u8, p));
        }

        @string normal = tspath.RemoveTrailingDirectorySeparator(tspath.NormalizePath(p)); if (normal != p) {
            throw panic(fmt.Sprintf("non-normalized path %q"u8, p));
        }

        if (strings.HasPrefix(p, "/")) {
            posix = true;
        } else {
            windows = true;
        }
    }

    var mfs = new fstest.MapFS(len(m));
    var keys = slices.Collect(maps.Keys<map<@string, File>, @string, File>(m));
    slices.SortFunc<slice<@string>, @string>(keys, comparePathsByParts);
    foreach (var (_, vᴛ1) in range(keys)) {
        var p = vᴛ1;

        var f = m[p];
        checkPath(p);

        ж<fstest.MapFile> @file = default;
        var switchᴛ1 = ((any)(f).OrTypedNilOf());
        switch (switchᴛ1.type()) {
        case @string fΔ1: {
            @file = Ꮡ(new fstest.MapFile(Data: slice<byte>(fΔ1), ModTime: clock.Now()));
            break;
        }
        case slice<byte> fΔ1: {
            @file = Ꮡ(new fstest.MapFile(Data: fΔ1, ModTime: clock.Now()));
            break;
        }
        case ж<fstest.MapFile> fΔ1: {
            ref var fCopy = ref heap<fstest.MapFile>(fΔ1.Value, out var ᏑfCopy);
            fCopy.ModTime = clock.Now();
            @file = ᏑfCopy;
            break;
        }
        default: {
            var fΔ1 = switchᴛ1;
            throw panic(fmt.Sprintf("invalid file type %T"u8, fΔ1));
            break;
        }}

        if ((fs.FileMode)(@file.Mode & fs.ModeSymlink) != 0) {
            @string target = (@string)@file.Data;
            checkPath(target);

            (target, _) = strings.CutPrefix(target, "/");
            ref var fileCopy = ref heap<fstest.MapFile>(@file.Value, out var ᏑfileCopy);
            fileCopy.Data = slice<byte>(target);
            @file = ᏑfileCopy;
        }

        (p, _) = strings.CutPrefix(p, "/");
        mfs[p] = @file;
    }

    if (posix && windows) {
        throw panic("mixed posix and windows paths");
    }

    return new iovfs_FsWithSysᴠFS(iovfs.From(new MapFSжFS(convertMapFS(mfs, useCaseSensitiveFileNames, clock)), useCaseSensitiveFileNames));
}

internal static ж<MapFS> convertMapFS(fstest.MapFS input, bool useCaseSensitiveFileNames, Clock clock) {
    if (clock == nil) {
        clock = new clockImplжClock(Ꮡ(new clockImpl(start: time.Now())));
    }
    var m = Ꮡ(new MapFS(
        m: new fstest.MapFS(len(input)),
        useCaseSensitiveFileNames: useCaseSensitiveFileNames,
        clock: clock
    ));

    var canonicalPaths = new map<canonicalPath, @string>(len(input));
    foreach (var (kᴛ1, _) in input) {
        var path = kᴛ1;

        canonicalPath canonical = m.getCanonicalPath(path);
        var (other, ok) = canonicalPaths[canonical, ꟷ]; if (ok) {
            (path, other) = (min(path, other), max(path, other));
            throw panic(fmt.Sprintf("duplicate path: %q and %q have the same canonical path"u8, path, other));
        }
        canonicalPaths[canonical] = path;
    }

    var inputKeys = slices.Collect(maps.Keys<fstest.MapFS, @string, ж<fstest.MapFile>>(input));
    slices.SortFunc<slice<@string>, @string>(inputKeys, comparePathsByParts);

    foreach (var (_, p) in range(inputKeys)) {
        var @file = input[p];

        @string dir = dirName(p); if (dir != ""u8) {
            var err = m.mkdirAll(dir, 511); if (err != nil) {
                throw panic(fmt.Sprintf("failed to create intermediate directories for %q: %v"u8, p, err));
            }
        }
        m.setEntry(p, m.getCanonicalPath(p), @file.Value);
    }

    return m;
}

internal static nint comparePathsByParts(@string a, @string b) {
    for (;;) {
        var (aStart, aEnd, aOk) = strings.Cut(a, "/");
        var (bStart, bEnd, bOk) = strings.Cut(b, "/");

        if (!aOk || !bOk) {
            return strings.Compare(a, b);
        }

        nint r = strings.Compare(aStart, bStart); if (r != 0) {
            return r;
        }

        (a, b) = (aEnd, bEnd);
    }
}

partial struct canonicalPath /*@string*/;

} partial class vfstestᴛMapFS {

internal static canonicalPath getCanonicalPath(this ref MapFS m, @string p) {
    return (canonicalPath)tspath.GetCanonicalFileName(p, m.useCaseSensitiveFileNames);
}

internal static (fs.File, error) open(this ref MapFS m, canonicalPath p) {
    return m.m.Open((@string)p);
}

internal static error remove(this ref MapFS m, @string path) {
    canonicalPath canonical = m.getCanonicalPath(path);
    @string canonicalString = (@string)canonical;
    var fileInfo = m.m[canonicalString];
    if (fileInfo == nil) {
        return default;
    }
    delete(m.m, canonicalString);
    delete(m.symlinks, canonical);

    if (fileInfo.Mode.IsDir()) {
        canonicalString += "/"u8;
        foreach (var (pathΔ1, _) in m.m) {
            if (strings.HasPrefix(pathΔ1, canonicalString)) {
                delete(m.m, pathΔ1);
                delete(m.symlinks, (canonicalPath)pathΔ1);
            }
        }
    }
    return default;
}

} partial class vfstest_package {

public static ж<fstest.MapFile> Symlink(@string target) {
    return Ꮡ(new fstest.MapFile(
        Data: slice<byte>(target),
        Mode: fs.ModeSymlink
    ));
}

} partial class vfstestᴛMapFS {

internal static (ж<fstest.MapFile>, canonicalPath, error) getFollowingSymlinks(this ref MapFS m, canonicalPath p) {
    return m.getFollowingSymlinksWorker(p, ""u8, ""u8);
}

} partial class vfstest_package {

partial struct brokenSymlinkError {
    internal canonicalPath from, to;
}

} partial class vfstestᴛbrokenSymlinkError {

internal static @string Error(this ref brokenSymlinkError e) {
    return fmt.Sprintf("broken symlink %q -> %q"u8, e.from, e.to);
}

} partial class vfstest_package {

internal static bool isBrokenSymlinkError(error err) {
    var (_, ok) = errors.AsType<ж<brokenSymlinkError>>(err);
    return ok;
}

} partial class vfstestᴛMapFS {

internal static (ж<fstest.MapFile>, canonicalPath, error) getFollowingSymlinksWorker(this ref MapFS m, canonicalPath p, canonicalPath symlinkFrom, canonicalPath symlinkTo) {
    {
        var (@file, ok) = m.m[((@string)p), ꟷ]; if (ok && (fs.FileMode)(@file.Mode & fs.ModeSymlink) == 0) {
            return (@file, p, default);
        }
    }

    {
        var (target, ok) = m.symlinks[p, ꟷ]; if (ok) {
            return m.getFollowingSymlinksWorker(target, p, target);
        }
    }

    foreach (var (other, target) in m.symlinks) {
        if (len(other) < len(p) && other == p.slice(0, len(other)) && p[len(other)] == '/') {
            return m.getFollowingSymlinksWorker(target + p.slice(len(other)), other, target);
        }
    }

    var err = fs.ErrNotExist;
    if (symlinkFrom != ""u8) {
        err = new brokenSymlinkErrorжerror(Ꮡ(new brokenSymlinkError(symlinkFrom, symlinkTo)));
    }
    return (default, p, err);
}

internal static void set(this ref MapFS m, canonicalPath p, ж<fstest.MapFile> @file) {
    m.m[((@string)p)] = @file;
}

internal static void setEntry(this ref MapFS m, @string realpath, canonicalPath canonical, fstest.MapFile @fileʗp) {
    ref var @file = ref heap(@fileʗp, out var Ꮡfile);

    if (realpath == ""u8 || canonical == ""u8) {
        throw panic("empty path");
    }

    @file.Sys = Ꮡ(new sys(
        original: @file.Sys,
        realpath: realpath
    ));
    m.set(canonical, Ꮡfile);

    if ((fs.FileMode)(@file.Mode & fs.ModeSymlink) != 0) {
        if (m.symlinks == nil) {
            m.symlinks = new map<canonicalPath, canonicalPath>();
        }
        m.symlinks[canonical] = m.getCanonicalPath((@string)@file.Data);
    }
}

} partial class vfstest_package {

internal static (@string before, @string after) splitPath(@string s, nint offset) {
    nint idx = strings.IndexByte(s.slice(offset), (byte)'/');
    if (idx < 0) {
        return (s, "");
    }
    return (s.slice(0, idx + offset), s.slice(idx + 1 + offset));
}

internal static @string dirName(@string p) {
    var (dir, _) = path.Split(p);
    return strings.TrimSuffix(dir, "/");
}

internal static @string baseName(@string p) {
    var (_, @file) = path.Split(p);
    return @file;
}

} partial class vfstestᴛMapFS {

internal static error mkdirAll(this ref MapFS m, @string p, fs.FileMode perm) {
    var toCreateᴷ = new stackAppend<@string>(moves: true, capacityUsed: true);

    if (p == ""u8) {
        throw panic("empty path");
    }

    {
        var (other, _, err) = m.getFollowingSymlinks(m.getCanonicalPath(p)); if (err == nil) {
            if (!other.Mode.IsDir()) {
                return fmt.Errorf("mkdir %q: path exists but is not a directory"u8, p);
            }
            return default;
        }
    }

    slice<@string> toCreate = default;
    nint offset = 0;
    for (;;) {
        var (dir, rest) = splitPath(p, offset);
        canonicalPath canonical = m.getCanonicalPath(dir);
        var (other, otherPath, err) = m.getFollowingSymlinks(canonical);
        if (err != nil) {
            if (!errors.Is(err, fs.ErrNotExist)) {
                return err;
            }
            toCreate = append(toCreateᴷ, toCreate, dir);
        } else {
            if (!other.Mode.IsDir()) {
                return fmt.Errorf("mkdir %q: path exists but is not a directory"u8, otherPath);
            }
            if (canonical != otherPath) {
                p = other.Sys._<ж<sys>>().realpath + "/"u8 + rest;
                toCreate = toCreate[..0];
                offset = 0;
                continue;
            }
        }
        if (rest == ""u8) {
            break;
        }
        offset = len(dir) + 1;
    }

    toCreate = move2heap(toCreateᴷ, toCreate);
    foreach (var (_, dir) in range(toCreate)) {
        m.setEntry(dir, m.getCanonicalPath(dir), new fstest.MapFile(
            Mode: (fs.FileMode)(fs.ModeDir | (fs.FileMode)(perm & ~(uint32)umask)),
            ModTime: m.clock.Now()
        ));
    }

    return default;
}

} partial class vfstest_package {

partial struct fileInfo {
    /*embed*/ public io.fs_package.FileInfo FileInfo;
    internal any sys;
    internal @string realpath;
}

} partial class vfstestᴛfileInfo {

// Go method set entry for the promoted 'FileInfo.IsDir()' - provided ONLY by the embedded
// interface field in *fileInfo's method set; see the pointer-only satisfaction record.
internal static bool IsDir(this fileInfo recvᴛ) => recvᴛ.FileInfo.IsDir();

// Go method set entry for the promoted 'FileInfo.ModTime()' - provided ONLY by the embedded
// interface field in *fileInfo's method set; see the pointer-only satisfaction record.
internal static time.Time ModTime(this fileInfo recvᴛ) => recvᴛ.FileInfo.ModTime();

// Go method set entry for the promoted 'FileInfo.Mode()' - provided ONLY by the embedded
// interface field in *fileInfo's method set; see the pointer-only satisfaction record.
internal static fs.FileMode Mode(this fileInfo recvᴛ) => recvᴛ.FileInfo.Mode();

// Go method set entry for the promoted 'FileInfo.Size()' - provided ONLY by the embedded
// interface field in *fileInfo's method set; see the pointer-only satisfaction record.
internal static int64 Size(this fileInfo recvᴛ) => recvᴛ.FileInfo.Size();

internal static @string Name(this ref fileInfo fi) {
    return baseName(fi.realpath);
}

internal static any Sys(this ref fileInfo fi) {
    return fi.sys;
}

} partial class vfstest_package {

partial struct @file {
    /*embed*/ public io.fs_package.File File;
    internal ж<fileInfo> fileInfo;
}

} partial class vfstestᴛfile {

// Go method set entry for the promoted 'File.Close()' - provided ONLY by the embedded
// interface field in *@file's method set; see the pointer-only satisfaction record.
internal static error Close(this @file recvᴛ) => recvᴛ.File.Close();

// Go method set entry for the promoted 'File.Read()' - provided ONLY by the embedded
// interface field in *@file's method set; see the pointer-only satisfaction record.
internal static (nint, error) Read(this @file recvᴛ, slice<byte> arg1ᴛ) => recvᴛ.File.Read(arg1ᴛ);

internal static (fs.FileInfo, error) Stat(this ref @file f) {
    return (new fileInfoжFileInfo(f.fileInfo), default);
}

} partial class vfstest_package {

partial struct readDirFile {
    /*embed*/ public io.fs_package.ReadDirFile ReadDirFile;
    internal ж<fileInfo> fileInfo;
}

} partial class vfstestᴛreadDirFile {

// Go method set entry for the promoted 'ReadDirFile.Close()' - provided ONLY by the embedded
// interface field in *readDirFile's method set; see the pointer-only satisfaction record.
internal static error Close(this readDirFile recvᴛ) => recvᴛ.ReadDirFile.Close();

// Go method set entry for the promoted 'ReadDirFile.Read()' - provided ONLY by the embedded
// interface field in *readDirFile's method set; see the pointer-only satisfaction record.
internal static (nint, error) Read(this readDirFile recvᴛ, slice<byte> arg1ᴛ) => recvᴛ.ReadDirFile.Read(arg1ᴛ);

internal static (fs.FileInfo, error) Stat(this ref readDirFile f) {
    return (new fileInfoжFileInfo(f.fileInfo), default);
}

internal static (slice<fs.DirEntry>, error) ReadDir(this ref readDirFile f, nint n) {
    var (list, err) = f.ReadDirFile.ReadDir(n);
    if (err != nil) {
        return (default, err);
    }

    var entries = new slice<fs.DirEntry>(len(list));
    foreach (var (i, entry) in range(list)) {
        var (ᴛ1, ᴛ2) = entry.Info();
        var info = must(ᴛ1, ᴛ2);
        var (newInfo, ok) = convertInfo(info);
        if (!ok) {
            throw panic(fmt.Sprintf("unexpected synthesized dir: %q"u8, info.Name()));
        }
        entries[i] = fs.FileInfoToDirEntry(new fileInfoжFileInfo(newInfo));
    }

    return (entries, default);
}

} partial class vfstestᴛMapFS {

public static (fs.File, error) Open(this ж<MapFS> Ꮡm, @string name) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.RLock();
        ᒐd1 = true;

        var (_, cp, _) = m.getFollowingSymlinks(m.getCanonicalPath(name));
        var (f, err) = m.open(cp);
        if (err != nil) {
            return (default, err);
        }

        var (ᴛ3, ᴛ4) = f.Stat();
        var info = must(ᴛ3, ᴛ4);

        var (newInfo, ok) = convertInfo(info);
        if (!ok) {
            if (name != "."u8) {
                throw panic(fmt.Sprintf("unexpected synthesized dir: %q"u8, name));
            }

            return (new readDirFileжFile(Ꮡ(new readDirFile(
                ReadDirFile: f._<fs.ReadDirFile>(),
                fileInfo: Ꮡ(new fileInfo(
                    FileInfo: info,
                    sys: info.Sys(),
                    realpath: "."
                ))
            ))), default);
        }

        var (fΔ1, okΔ1) = f._<fs.ReadDirFile>(ᐧ); if (okΔ1) {
            return (new readDirFileжFile(Ꮡ(new readDirFile(
                ReadDirFile: fΔ1,
                fileInfo: newInfo
            ))), default);
        }

        return (new fileжFile(Ꮡ(new @file(
            File: f,
            fileInfo: newInfo
        ))), default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.RUnlock(); ᒐ.Run(); }
}

public static (@string, error) Realpath(this ж<MapFS> Ꮡm, @string name) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.RLock();
        ᒐd1 = true;

        var (@file, _, err) = m.getFollowingSymlinks(m.getCanonicalPath(name));
        if (err != nil) {
            return ("", err);
        }
        return (@file.Sys._<ж<sys>>().realpath, default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.RUnlock(); ᒐ.Run(); }
}

} partial class vfstest_package {

internal static (ж<fileInfo>, bool) convertInfo(fs.FileInfo info) {
    var (sys, ok) = info.Sys()._<ж<sys>>(ᐧ);
    if (!ok) {
        return (default, false);
    }
    return (Ꮡ(new fileInfo(
        FileInfo: info,
        sys: sys.original,
        realpath: sys.realpath
    )), true);
}

internal static UntypedInt umask => /* 0o022 */ 18;

} partial class vfstestᴛMapFS {

public static error MkdirAll(this ж<MapFS> Ꮡm, @string path, fs.FileMode perm) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.Lock();
        ᒐd1 = true;

        return m.mkdirAll(path, perm);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.Unlock(); ᒐ.Run(); }
}

public static void AddSymlink(this ж<MapFS> Ꮡm, @string path, @string target) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.Lock();
        ᒐd1 = true;

        canonicalPath canonical = m.getCanonicalPath(path);
        m.setEntry(path, canonical, new fstest.MapFile(
            Data: slice<byte>(target),
            Mode: fs.ModeSymlink
        ));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.Unlock(); ᒐ.Run(); }
}

public static error WriteFile(this ж<MapFS> Ꮡm, @string path, @string data, fs.FileMode perm) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.Lock();
        ᒐd1 = true;

        @string parent = dirName(path); if (parent != ""u8) {
            canonicalPath canonical = m.getCanonicalPath(parent);
            var (parentFile, _, errΔ1) = m.getFollowingSymlinks(canonical);
            if (errΔ1 != nil) {
                return fmt.Errorf("write %q: %w"u8, path, errΔ1);
            }
            if (!parentFile.Mode.IsDir()) {
                return fmt.Errorf("write %q: parent path exists but is not a directory"u8, path);
            }
        }

        var (@file, cp, err) = m.getFollowingSymlinks(m.getCanonicalPath(path));
        if (err != nil) {
            if (!errors.Is(err, fs.ErrNotExist) && !isBrokenSymlinkError(err)) {
                throw panic(err);
            }
        } else {
            if (!@file.Mode.IsRegular()) {
                return fmt.Errorf("write %q: path exists but is not a regular file"u8, path);
            }
        }

        m.setEntry(path, cp, new fstest.MapFile(
            Data: @unsafe.Slice(@unsafe.StringData(data), len(data)),
            ModTime: m.clock.Now(),
            Mode: (fs.FileMode)(perm & ~(uint32)umask)
        ));

        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.Unlock(); ᒐ.Run(); }
}

public static error AppendFile(this ж<MapFS> Ꮡm, @string path, @string data, fs.FileMode perm) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.Lock();
        ᒐd1 = true;

        @string parent = dirName(path); if (parent != ""u8) {
            canonicalPath canonical = m.getCanonicalPath(parent);
            var (parentFile, _, errΔ1) = m.getFollowingSymlinks(canonical);
            if (errΔ1 != nil) {
                return fmt.Errorf("append %q: %w"u8, path, errΔ1);
            }
            if (!parentFile.Mode.IsDir()) {
                return fmt.Errorf("append %q: parent path exists but is not a directory"u8, path);
            }
        }

        slice<byte> existing = default;
        fs.FileMode existingMode = default;
        var (@file, cp, err) = m.getFollowingSymlinks(m.getCanonicalPath(path));
        if (err != nil) {
            if (!errors.Is(err, fs.ErrNotExist) && !isBrokenSymlinkError(err)) {
                throw panic(err);
            }
        } else {
            if (!@file.Mode.IsRegular()) {
                return fmt.Errorf("append %q: path exists but is not a regular file"u8, path);
            }
            existing = @file.Data;
            existingMode = @file.Mode;
        }

        var combined = new slice<byte>(0, len(existing) + len(data));
        combined = appendꓸꓸꓸ(combined, existing);
        combined = append(combined, data.ꓸꓸꓸ);

        var mode = existingMode;
        if (mode == 0) {
            mode = (fs.FileMode)(perm & ~(uint32)umask);
        }

        m.setEntry(path, cp, new fstest.MapFile(
            Data: combined,
            ModTime: m.clock.Now(),
            Mode: mode
        ));

        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.Unlock(); ᒐ.Run(); }
}

public static error Remove(this ж<MapFS> Ꮡm, @string path) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.Lock();
        ᒐd1 = true;

        return m.remove(path);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.Unlock(); ᒐ.Run(); }
}

public static error Chtimes(this ж<MapFS> Ꮡm, @string path, time.Time aTime, time.Time mTime) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        Ꮡm.Ꮡmu.Lock();
        ᒐd1 = true;
        canonicalPath canonical = m.getCanonicalPath(path);
        @string canonicalString = (@string)canonical;
        var fileInfo = m.m[canonicalString];
        if (fileInfo == nil) {
            return fs.ErrNotExist;
        }
        fileInfo.ModTime = mTime;
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.Unlock(); ᒐ.Run(); }
}

public static (@string, bool) GetTargetOfSymlink(this ж<MapFS> Ꮡm, @string path) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        (path, _) = strings.CutPrefix(path, "/");
        Ꮡm.Ꮡmu.RLock();
        ᒐd1 = true;
        canonicalPath canonical = m.getCanonicalPath(path);
        @string canonicalString = (@string)canonical;
        var (fileInfo, ok) = m.m[canonicalString, ꟷ]; if (ok) {
            if ((fs.FileMode)(fileInfo.Mode & fs.ModeSymlink) != 0) {
                return ("/" + ((sstring)fileInfo.Data), true);
            }
        }
        return ("", false);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.RUnlock(); ᒐ.Run(); }
}

public static time.Time GetModTime(this ж<MapFS> Ꮡm, @string path) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        (path, _) = strings.CutPrefix(path, "/");
        Ꮡm.Ꮡmu.RLock();
        ᒐd1 = true;
        canonicalPath canonical = m.getCanonicalPath(path);
        @string canonicalString = (@string)canonical;
        var (fileInfo, ok) = m.m[canonicalString, ꟷ]; if (ok) {
            return fileInfo.ModTime;
        }
        return new time.Time();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.RUnlock(); ᒐ.Run(); }
}

public static iter.Seq2<@string, ж<fstest.MapFile>> Entries(this ж<MapFS> Ꮡm) {
    ref var m = ref Ꮡm.DerefOrNull();

    return (Func<@string, ж<fstest.MapFile>, bool> yield) => {
        GoFrame ᒐ = default;
        try {
            Ꮡm.Ꮡmu.RLock();
            defer(Ꮡm.Ꮡmu.RUnlock, ref ᒐ);
            var inputKeys = slices.Collect(maps.Keys<fstest.MapFS, @string, ж<fstest.MapFile>>(Ꮡm.Value.m));
            slices.SortFunc<slice<@string>, @string>(inputKeys, comparePathsByParts);

            foreach (var (_, p) in range(inputKeys)) {
                var @file = Ꮡm.Value.m[p];
                @string path = @file.Sys._<ж<sys>>().realpath;
                if (!tspath.PathIsAbsolute(path)) {
                    path = "/"u8 + path;
                }
                if (!yield(path, @file)) {
                    break;
                }
            }
        }
        catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
        finally { ᒐ.Run(); }
    };
}

public static ж<fstest.MapFile> GetFileInfo(this ж<MapFS> Ꮡm, @string path) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var m = ref Ꮡm.DerefOrNull();

        (path, _) = strings.CutPrefix(path, "/");
        Ꮡm.Ꮡmu.RLock();
        ᒐd1 = true;
        canonicalPath canonical = m.getCanonicalPath(path);
        @string canonicalString = (@string)canonical;
        return m.m[canonicalString];
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡm.Ꮡmu.RUnlock(); ᒐ.Run(); }
}

} partial class vfstest_package {

internal static T must<T>(T v, error err) {
    if (err != nil) {
        throw panic(err);
    }
    return v;
}

} // end vfstest_package
