// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

using bytes = bytes_package;
using errors = errors_package;
using fmt = fmt_package;
using sort = sort_package;
using tag = go.golang.org.x.text.@internal.tag_package;
using go.golang.org.x.text.@internal;
using static language_package;

partial class language_package {

internal static bool isAlpha(byte b) {
    return b > '9';
}

internal static bool isAlphaNum(slice<byte> s) {
    foreach (var (_, c) in range(s)) {
        if (!('a' <= c && c <= 'z' || 'A' <= c && c <= 'Z' || '0' <= c && c <= '9')) {
            return false;
        }
    }
    return true;
}

public static error ErrSyntax = errors.New("language: tag is not well-formed");

public static error ErrDuplicateKey = errors.New("language: different values for same key in -u extension");

partial struct ValueError {
    internal array<byte> v = new(8);
}

public static ValueError NewValueError(slice<byte> tag) {
    ValueError e = new();
    copy(e.v[..], tag);
    return e.ΔClone();
}

} partial class languageᴛValueError {

internal static slice<byte> tag(this ValueError e) {
    e = e.ΔClone();

    nint n = bytes.IndexByte(e.v[..], 0);
    if (n == -1) {
        n = 8;
    }
    return e.v.slice(0, n);
}

public static @string Error(this ValueError e) {
    e = e.ΔClone();

    return fmt.Sprintf("language: subtag %q is well-formed but unknown"u8, e.tag());
}

public static @string Subtag(this ValueError e) {
    e = e.ΔClone();

    return (@string)e.tag();
}

} partial class language_package {

partial struct scanner {
    internal slice<byte> b;
    internal array<byte> bytes = new(max99thPercentileSize);
    internal slice<byte> token;
    internal nint start;
    internal nint end;
    internal nint next;
    internal error err;
    internal bool done;
}

internal static scanner makeScannerString(@string s) {
    var scan = new scanner();
    if (len(s) <= len(scan.bytes)) {
        scan.b = scan.bytes.slice(0, copy(scan.bytes[..], s));
    } else {
        scan.b = slice<byte>(s);
    }
    scan.init();
    return scan.ΔClone();
}

internal static scanner makeScanner(slice<byte> b) {
    var scan = new scanner(b: b);
    scan.init();
    return scan.ΔClone();
}

} partial class languageᴛscanner {

internal static void init(this ref scanner s) {
    foreach (var (i, c) in range(s.b)) {
        if (c == '_') {
            s.b[i] = (byte)'-';
        }
    }
    s.scan();
}

internal static void toLower(this ref scanner s, nint start, nint end) {
    for (nint i = start; i < end; i++) {
        var c = s.b[i];
        if ('A' <= c && c <= 'Z') {
            s.b[i] += (byte)((rune)'a' - (rune)'A');
        }
    }
}

internal static void setError(this ref scanner s, error e) {
    if (s.err == nil || (AreEqual(e, ErrSyntax) && !AreEqual(s.err, ErrSyntax))) {
        s.err = e;
    }
}

internal static void resizeRange(this ref scanner s, nint oldStart, nint oldEnd, nint newSize) {
    s.start = oldStart;
    nint end = oldStart + newSize; if (end != oldEnd) {
        nint diff = end - oldEnd;
        slice<byte> b = default;
        nint n = len(s.b) + diff; if (n > cap(s.b)) {
            b = new slice<byte>(n);
            copy(b, s.b.slice(0, oldStart));
        } else {
            b = s.b.slice(0, n);
        }
        copy(b.slice(end), s.b.slice(oldEnd));
        s.b = b;
        s.next = end + (s.next - s.end);
        s.end = end;
    }
}

internal static void replace(this ref scanner s, @string repl) {
    s.resizeRange(s.start, s.end, len(repl));
    copy(s.b.slice(s.start), repl);
}

internal static void gobble(this ref scanner s, error e) {
    s.setError(e);
    if (s.start == 0) {
        s.b = s.b.slice(0, +copy(s.b, s.b.slice(s.next)));
        s.end = 0;
    } else {
        s.b = s.b.slice(0, s.start - 1 + copy(s.b.slice(s.start - 1), s.b.slice(s.end)));
        s.end = s.start - 1;
    }
    s.next = s.start;
}

internal static void deleteRange(this ref scanner s, nint start, nint end) {
    s.b = s.b.slice(0, start + copy(s.b.slice(start), s.b.slice(end)));
    nint diff = end - start;
    s.next -= diff;
    s.start -= diff;
    s.end -= diff;
}

internal static nint /*end*/ scan(this ref scanner s) {
    nint end = default;

    end = s.end;
    s.token = default;
    for (s.start = s.next; s.next < len(s.b); ) {
        nint i = bytes.IndexByte(s.b.slice(s.next), (byte)'-');
        if (i == -1) {
            s.end = len(s.b);
            s.next = len(s.b);
            i = s.end - s.start;
        } else {
            s.end = s.next + i;
            s.next = s.end + 1;
        }
        var token = s.b.slice(s.start, s.end);
        if (i < 1 || i > 8 || !isAlphaNum(token)) {
            s.gobble(ErrSyntax);
            continue;
        }
        s.token = token;
        return end;
    }
    nint n = len(s.b); if (n > 0 && s.b[n - 1] == '-') {
        s.setError(ErrSyntax);
        s.b = s.b.slice(0, len(s.b) - 1);
    }
    s.done = true;
    return end;
}

internal static nint /*end*/ acceptMinSize(this ref scanner s, nint min) {
    nint end = default;

    end = s.end;
    s.scan();
    for (; len(s.token) >= min; s.scan()) {
        end = s.end;
    }
    return end;
}

} partial class language_package {

public static (Tag t, error err) Parse(@string s) {
    Tag t = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        if (s == ""u8) {
            (t, err) = (Und, ErrSyntax); goto ᒐdone;
        }
        defer(() => {
            if (recover() != nil) {
                t = Und;
                err = ErrSyntax;
                return;
            }
        }, ref ᒐ);
        if (len(s) <= maxAltTaglen) {
            var b = new byte[]{}.array(11);
            foreach (var (i, rᴛ1) in s) {
                var c = rᴛ1;

                if ('A' <= c && c <= 'Z') {
                    c += (rune)'a' - (rune)'A';
                } else if (c == '_') {
                    c = '-';
                }
                b[i] = (byte)c;
            }
            var (tΔ1, ok) = grandfathered(b); if (ok) {
                (t, err) = (tΔ1, default); goto ᒐdone;
            }
        }
        ref var scan = ref heap<scanner>(makeScannerString(s), out var Ꮡscan);
        (t, err) = parse(Ꮡscan, s);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (t, err);
}

internal static (Tag t, error err) parse(ж<scanner> Ꮡscan, @string s) {
    Tag t = default;
    ref var scan = ref Ꮡscan.DerefOrNull();

    t = Und;
    nint end = default;
    nint n = len(scan.token); if (n <= 1) {
        scan.toLower(0, len(scan.b));
        if (n == 0 || scan.token[0] != 'x') {
            return (t, ErrSyntax);
        }
        end = parseExtensions(Ꮡscan);
    } else if (n >= 4) {
        return (Und, ErrSyntax);
    } else {
        (t, end) = parseTag(Ꮡscan, true);
        nint nΔ1 = len(scan.token); if (nΔ1 == 1) {
            t.pExt = (uint16)end;
            end = parseExtensions(Ꮡscan);
        } else if (end < len(scan.b)) {
            scan.setError(ErrSyntax);
            scan.b = scan.b.slice(0, end);
        }
    }
    if ((nint)t.pVariant < len(scan.b)) {
        if (end < len(s)) {
            s = s.slice(0, end);
        }
        if (len(s) > 0 && tag.Compare(s, scan.b) == 0) {
            t.str = s;
        } else {
            t.str = (@string)scan.b;
        }
    } else {
        (t.pVariant, t.pExt) = (0, 0);
    }
    return (t, scan.err);
}

internal static (Tag t, nint end) parseTag(ж<scanner> Ꮡscan, bool doNorm) {
    Tag t = default;
    nint end = default;
    ref var scan = ref Ꮡscan.DerefOrNull();

    error e = default;
    (t.LangID, e) = getLangID(scan.token);
    scan.setError(e);
    scan.replace(t.LangID.String());
    nint langStart = scan.start;
    end = scan.scan();
    while (len(scan.token) == 3 && isAlpha(scan.token[0])) {
        if (doNorm) {
            var (lang, eΔ1) = getLangID(scan.token);
            if (lang != 0) {
                t.LangID = lang;
                @string langStr = lang.String();
                copy(scan.b.slice(langStart), langStr);
                scan.b[langStart + len(langStr)] = (byte)'-';
                scan.start = langStart + len(langStr) + 1;
            }
            scan.gobble(eΔ1);
        }
        end = scan.scan();
    }
    if (len(scan.token) == 4 && isAlpha(scan.token[0])) {
        (t.ScriptID, e) = getScriptID(script, scan.token);
        if (t.ScriptID == 0) {
            scan.gobble(e);
        }
        end = scan.scan();
    }
    nint n = len(scan.token); if (n >= 2 && n <= 3) {
        (t.RegionID, e) = getRegionID(scan.token);
        if (t.RegionID == 0) {
            scan.gobble(e);
        } else {
            scan.replace(t.RegionID.String());
        }
        end = scan.scan();
    }
    scan.toLower(scan.start, len(scan.b));
    t.pVariant = (byte)end;
    end = parseVariants(Ꮡscan, end, t);
    t.pExt = (uint16)end;
    return (t, end);
}

internal static slice<byte> separator = [(byte)'-'];

internal static nint parseVariants(ж<scanner> Ꮡscan, nint end, Tag t) {
    ref var scan = ref Ꮡscan.DerefOrNull();

    nint start = scan.start;
    var varIDBuf = new uint8[]{}.array(4);
    var variantBuf = new slice<byte>[]{}.array(4);
    var varID = varIDBuf[..0];
    var variant = variantBuf[..0];
    nint last = -1;
    var needSort = false;
    for (; len(scan.token) >= 4; scan.scan()) {
        var (v, ok) = variantIndex[tmpstring(scan.token), ꟷ];
        if (!ok) {
            scan.gobble(NewValueError(scan.token));
            continue;
        }
        varID = append(varID, v);
        variant = append(variant, scan.token);
        if (!needSort) {
            if (last < (nint)v) {
                last = (nint)v;
            } else {
                needSort = true;
                const nint maxVariants = 8;
                if (len(varID) > maxVariants) {
                    break;
                }
            }
        }
        end = scan.end;
    }
    if (needSort) {
        sort.Sort(new variantsSort(varID, variant));
        nint k = 0;
        nint l = -1;
        foreach (var (i, v) in range(varID)) {
            nint w = (nint)v;
            if (l == w) {
                continue;
            }
            varID[k] = varID[i];
            variant[k] = variant[i];
            k++;
            l = w;
        }
        var str = bytes.Join(variant.slice(0, k), separator); if (len(str) == 0) {
            end = start - 1;
        } else {
            scan.resizeRange(start, end, len(str));
            copy(scan.b.slice(scan.start), str);
            end = scan.end;
        }
    }
    return end;
}

partial struct variantsSort {
    internal slice<uint8> i;
    internal slice<slice<byte>> v;
}

} partial class languageᴛvariantsSort {

internal static nint Len(this variantsSort s) {
    return len(s.i);
}

internal static void Swap(this variantsSort s, nint i, nint j) {
    (s.i[i], s.i[j]) = (s.i[j], s.i[i]);
    (s.v[i], s.v[j]) = (s.v[j], s.v[i]);
}

internal static bool Less(this variantsSort s, nint i, nint j) {
    return s.i[i] < s.i[j];
}

} partial class language_package {

partial struct bytesSort {
    internal slice<slice<byte>> b;
    internal nint n;
}

} partial class languageᴛbytesSort {

internal static nint Len(this bytesSort b) {
    return len(b.b);
}

internal static void Swap(this bytesSort b, nint i, nint j) {
    (b.b[i], b.b[j]) = (b.b[j], b.b[i]);
}

internal static bool Less(this bytesSort b, nint i, nint j) {
    for (nint k = 0; k < b.n; k++) {
        if (b.b[i][k] == b.b[j][k]) {
            continue;
        }
        return b.b[i][k] < b.b[j][k];
    }
    return false;
}

} partial class language_package {

internal static nint parseExtensions(ж<scanner> Ꮡscan) {
    ref var scan = ref Ꮡscan.DerefOrNull();

    nint start = scan.start;
    var exts = slice<slice<byte>>([]);
    var @private = slice<byte>([]);
    nint end = scan.end;
    while (len(scan.token) == 1) {
        nint extStart = scan.start;
        var ext = scan.token[0];
        end = parseExtension(Ꮡscan);
        var extension = scan.b.slice(extStart, end);
        if (len(extension) < 3 || (ext != 'x' && len(extension) < 4)) {
            scan.setError(ErrSyntax);
            end = extStart;
            continue;
        } else if (start == extStart && (ext == 'x' || scan.start == len(scan.b))) {
            scan.b = scan.b.slice(0, end);
            return end;
        } else if (ext == 'x') {
            @private = extension;
            break;
        }
        exts = append(exts, extension);
    }
    sort.Sort(new bytesSort(exts, 1));
    if (len(@private) > 0) {
        exts = append(exts, @private);
    }
    scan.b = scan.b.slice(0, start);
    if (len(exts) > 0) {
        scan.b = appendꓸꓸꓸ(scan.b, bytes.Join(exts, separator));
    } else if (start > 0) {
        scan.b = scan.b.slice(0, start - 1);
    }
    return end;
}

internal static nint parseExtension(ж<scanner> Ꮡscan) {
    ref var scan = ref Ꮡscan.DerefOrNull();

    nint start = scan.start;
    nint end = scan.end;
    switch (scan.token[0]) {
    case (byte)'u': {
        nint attrStart = end;
        scan.scan();
        for (var lastΔ1 = slice<byte>([]); len(scan.token) > 2; scan.scan()) {
            if (bytes.Compare(scan.token, lastΔ1) != -1) {
                nint p = attrStart + 1;
                scan.next = p;
                var attrs = slice<slice<byte>>([]);
                for (scan.scan(); len(scan.token) > 2; scan.scan()) {
                    attrs = append(attrs, scan.token);
                    end = scan.end;
                }
                sort.Sort(new bytesSort(attrs, 3));
                copy(scan.b.slice(p), bytes.Join(attrs, separator));
                break;
            }
            lastΔ1 = scan.token;
            end = scan.end;
        }
        slice<byte> last = default;
        slice<byte> key = default;
        for (nint attrEnd = end; len(scan.token) == 2; last = key) {
            key = scan.token;
            end = scan.end;
            for (scan.scan(); end < scan.end && len(scan.token) > 2; scan.scan()) {
                end = scan.end;
            }
            if (bytes.Compare(key, last) != 1 || scan.err != nil) {
                nint p = attrEnd + 1;
                scan.next = p;
                var keys = slice<slice<byte>>([]);
                for (scan.scan(); len(scan.token) == 2; ) {
                    nint keyStart = scan.start;
                    end = scan.end;
                    for (scan.scan(); end < scan.end && len(scan.token) > 2; scan.scan()) {
                        end = scan.end;
                    }
                    keys = append(keys, scan.b.slice(keyStart, end));
                }
                sort.Stable(new bytesSort(keys, 2));
                nint n = len(keys); if (n > 0) {
                    nint k = 0;
                    for (nint i = 1; i < n; i++) {
                        if (!bytes.Equal(keys[k][..2], keys[i][..2])) {
                            k++;
                            keys[k] = keys[i];
                        } else if (!bytes.Equal(keys[k], keys[i])) {
                            scan.setError(ErrDuplicateKey);
                        }
                    }
                    keys = keys.slice(0, k + 1);
                }
                var reordered = bytes.Join(keys, separator);
                nint e = p + len(reordered); if (e < end) {
                    scan.deleteRange(e, end);
                    end = e;
                }
                copy(scan.b.slice(p), reordered);
                break;
            }
        }
        break;
    }
    case (byte)'t': {
        scan.scan();
        {
            nint n = len(scan.token); if (n >= 2 && n <= 3 && isAlpha(scan.token[1])) {
                (_, end) = parseTag(Ꮡscan, false);
                scan.toLower(start, end);
            }
        }
        while (len(scan.token) == 2 && !isAlpha(scan.token[1])) {
            end = scan.acceptMinSize(3);
        }
        break;
    }
    case (byte)'x': {
        end = scan.acceptMinSize(1);
        break;
    }
    default: {
        end = scan.acceptMinSize(2);
        break;
    }}

    return end;
}

internal static (nint end, @string ext) getExtension(@string s, nint p) {
    nint end = default;

    if (s[p] == '-') {
        p++;
    }
    if (s[p] == 'x') {
        return (len(s), s.slice(p));
    }
    end = nextExtension(s, p);
    return (end, s.slice(p, end));
}

internal static nint nextExtension(@string s, nint p) {
    for (nint n = len(s) - 3; p < n; ) {
        if (s[p] == '-') {
            if (s[p + 2] == '-') {
                return p;
            }
            p += 3;
        } else {
            p++;
        }
    }
    return len(s);
}

} // end language_package
