namespace go.github.com.microsoft.TypeScript.tsc.@internal.vfs;

using binary = encoding.binary_package;
using fmt = fmt_package;
using fs = io.fs_package;
using strings = strings_package;
using utf16 = unicode.utf16_package;
using @unsafe = unsafe_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using encoding;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using io;
using io = io_package;
using unicode;
using static internal_package;

partial class internal_package {

partial struct Common {
    public Func<@string, fs.FS> RootFor;
    public Func<@string, bool> IsReparsePoint;
}

public static nint RootLength(@string p) {
    nint l = tspath.GetEncodedRootLength(p);
    if (l == 0) {
        throw panic(fmt.Sprintf("vfs: path %q is not absolute"u8, p));
    } else if (l < 0) {
        return ~l;
    }
    return l;
}

public static (@string rootName, @string rest) SplitPath(@string p) {
    @string rootName = default;
    @string rest = default;

    p = tspath.NormalizePath(p);
    nint l = RootLength(p);
    (rootName, rest) = (p.slice(0, l), p.slice(l));
    rest = tspath.RemoveTrailingDirectorySeparator(rest);
    return (rootName, rest);
}

} partial class internalᴛCommon {

public static (fs.FS fsys, @string rootName, @string rest) RootAndPath(this ref Common vfs, @string path) {
    @string rootName = default;
    @string rest = default;

    (rootName, rest) = SplitPath(path);
    if (rest == ""u8) {
        rest = ".";
    }
    return (vfs.RootFor(rootName), rootName, rest);
}

public static fs.FileInfo Stat(this ref Common vfsΔ1, @string path) {
    var (fsys, _, rest) = vfsΔ1.RootAndPath(path);
    if (fsys == nil) {
        return default;
    }
    var (stat, err) = fs.Stat(fsys, rest);
    if (err != nil) {
        return default;
    }
    return stat;
}

public static bool FileExists(this ref Common vfs, @string path) {
    var stat = vfs.Stat(path);
    return stat != nil && !stat.IsDir();
}

public static bool DirectoryExists(this ref Common vfs, @string path) {
    var stat = vfs.Stat(path);
    return stat != nil && stat.IsDir();
}

public static vfs.Entries /*result*/ GetAccessibleEntries(this ref Common vfsΔ1, @string path) {
    vfs.Entries result = default;

    result.Symlinks = new map<@string, EmptyStruct>{};

    bool /*added*/ addToResult(@string name, fs.FileMode mode, bool isLink) {
        if (mode.IsDir()) {
            result.Directories = append(result.Directories, name);
        } else if (mode.IsRegular()) {
            result.Files = append(result.Files, name);
        } else {
            return false;
        }

        if (isLink) {
            result.Symlinks[name] = new EmptyStruct();
        }
        return true;
    }

    foreach (var (_, entry) in range(vfsΔ1.getEntries(path))) {
        var entryType = entry.Type();

        if (addToResult(entry.Name(), entryType, false)) {
            continue;
        }

        if ((fs.FileMode)(entryType & fs.ModeSymlink) != 0) {
            var stat = vfsΔ1.Stat(path + "/"u8 + entry.Name()); if (stat != nil) {
                addToResult(entry.Name(), stat.Mode(), true);
            }
            continue;
        }

        if ((fs.FileMode)(entryType & fs.ModeIrregular) != 0 && vfsΔ1.IsReparsePoint != nil) {
            @string fullPath = path + "/"u8 + entry.Name();
            if (vfsΔ1.IsReparsePoint(fullPath)) {
                var stat = vfsΔ1.Stat(fullPath); if (stat != nil) {
                    addToResult(entry.Name(), stat.Mode(), true);
                }
            }
            continue;
        }
    }

    return result;
}

internal static slice<fs.DirEntry> getEntries(this ref Common vfsΔ1, @string path) {
    var (fsys, _, rest) = vfsΔ1.RootAndPath(path);
    if (fsys == nil) {
        return default;
    }

    var (entries, err) = fs.ReadDir(fsys, rest);
    if (err != nil) {
        return default;
    }

    return entries;
}

public static (@string contents, bool ok) ReadFile(this ref Common vfs, @string path) {
    var (fsys, _, rest) = vfs.RootAndPath(path);
    if (fsys == nil) {
        return ("", false);
    }

    var (b, err) = fs.ReadFile(fsys, rest);
    if (err != nil) {
        return ("", false);
    }

    if (len(b) == 0) {
        return ("", true);
    }

    @string s = @unsafe.String(Ꮡ(b, 0), len(b));

    return decodeBytes(s);
}

} partial class internal_package {

internal static (@string contents, bool ok) decodeBytes(@string s) {
    array<byte> bom = new(2);
    if (len(s) >= 2) {
        bom.Assign(new byte[]{s[0], s[1]}.array());
        switch (bom) {
        case var ᴛ when ᴛ == new byte[]{0xFF, 0xFE}.array(): {
            return (decodeUtf16(s[2..], binary.LittleEndian), true);
        }
        case var ᴛ when ᴛ == new byte[]{0xFE, 0xFF}.array(): {
            return (decodeUtf16(s[2..], binary.BigEndian), true);
        }}

    }
    if (len(s) >= 3 && s[0] == 0xEF && s[1] == 0xBB && s[2] == 0xBF) {
        s = s[3..];
    }

    return (s, true);
}

internal static @string decodeUtf16(@string s, binary.ByteOrder order) {
    ref var ints = ref heap<slice<uint16>>(new slice<uint16>(len(s) / 2), out var Ꮡints);
    var err = binary.Read(new strings_ReaderжReader(strings.NewReader(s)), order, Ꮡints); if (err != nil) {
        return "";
    }
    return (@string)utf16.Decode(ints);
}

} // end internal_package
