namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using utf8 = unicode.utf8_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using unicode;
using static ast_package;

partial class ast_package {

partial struct PositionMap {
    internal bool asciiOnly;
    internal slice<positionMapEntry> entries;
}

partial struct positionMapEntry {
    internal nint utf8Pos;
    internal nint delta;
}

public static ж<PositionMap> ComputePositionMap(@string text) {
    var pm = Ꮡ(new PositionMap());
    nint delta = 0;
    for (nint i = 0; i < len(text); ) {
        var b = text[i];
        if (b < utf8.RuneSelf) {
            i++;
            continue;
        }
        var (r, size) = stringutil.DecodeJSStringRune(text.slice(i));
        nint utf16Size = 1;
        if (r >= 0x10000) {
            utf16Size = 2;
        }
        delta += size - utf16Size;
        pm.entries = append(pm.entries, new positionMapEntry(utf8Pos: i + size, delta: delta));
        i += size;
    }
    pm.asciiOnly = len(pm.entries) == 0;
    return pm;
}

} partial class astᴛPositionMap {

public static bool IsAsciiOnly(this ref PositionMap pm) {
    return pm.asciiOnly;
}

public static nint UTF8ToUTF16(this ref PositionMap pm, nint utf8Offset) {
    if (pm.asciiOnly) {
        return utf8Offset;
    }
    nint lo = 0;
    nint hi = len(pm.entries);
    while (lo < hi) {
        nint mid = lo + (hi - lo) / 2;
        if (pm.entries[mid].utf8Pos <= utf8Offset) {
            lo = mid + 1;
        } else {
            hi = mid;
        }
    }
    if (lo == 0) {
        return utf8Offset;
    }
    return utf8Offset - pm.entries[lo - 1].delta;
}

public static nint UTF16ToUTF8(this ref PositionMap pm, nint utf16Offset) {
    if (pm.asciiOnly) {
        return utf16Offset;
    }
    nint lo = 0;
    nint hi = len(pm.entries);
    while (lo < hi) {
        nint mid = lo + (hi - lo) / 2;
        nint utf16Pos = pm.entries[mid].utf8Pos - pm.entries[mid].delta;
        if (utf16Pos <= utf16Offset) {
            lo = mid + 1;
        } else {
            hi = mid;
        }
    }
    if (lo == 0) {
        return utf16Offset;
    }
    return utf16Offset + pm.entries[lo - 1].delta;
}

} // end astᴛPositionMap
