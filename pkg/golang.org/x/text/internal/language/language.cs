// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

using errors = errors_package;
using fmt = fmt_package;
using strings = strings_package;
using tag = go.golang.org.x.text.@internal.tag_package;
using static language_package;

partial class language_package {

internal static UntypedInt maxCoreSize => 12;
internal static UntypedInt max99thPercentileSize => 32;
internal static UntypedInt maxSimpleUExtensionSize => 14;

partial struct Tag {
    public Language LangID;
    public Region RegionID;
    public Script ScriptID;
    internal byte pVariant;
    internal uint16 pExt;
    internal @string str;
}

public static Tag Make(@string s) {
    var (t, _) = Parse(s);
    return t;
}

} partial class languageᴛTag {

public static (Language b, Script s, Region r) Raw(this Tag t) {
    return (t.LangID, t.ScriptID, t.RegionID);
}

internal static bool equalTags(this Tag t, Tag a) {
    return t.LangID == a.LangID && t.ScriptID == a.ScriptID && t.RegionID == a.RegionID;
}

public static bool IsRoot(this Tag t) {
    if ((nint)t.pVariant < len(t.str)) {
        return false;
    }
    return t.equalTags(Und);
}

public static bool IsPrivateUse(this Tag t) {
    return t.str != ""u8 && t.pVariant == 0;
}

public static void RemakeString(this ж<Tag> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();
    var bᴷ = new stackAppend<byte>();

    if (t.str == ""u8) {
        return;
    }
    @string extra = t.str.slice(t.pVariant);
    if (t.pVariant > 0) {
        extra = extra[1..];
    }
    if (Ꮡt.equalTags(Und) && strings.HasPrefix(extra, "x-")) {
        t.str = extra;
        t.pVariant = 0;
        t.pExt = 0;
        return;
    }
    array<byte> buf = new(32); /* max99thPercentileSize */
    var b = buf.slice(0, t.genCoreBytes(buf[..]));
    if (extra != ""u8) {
        nint diff = len(b) - (nint)t.pVariant;
        b = append(bᴷ, b, (byte)'-');
        b = append(b, extra.ꓸꓸꓸ);
        t.pVariant = (uint8)((nint)t.pVariant + diff);
        t.pExt = (uint16)((nint)t.pExt + diff);
    } else {
        t.pVariant = (uint8)len(b);
        t.pExt = (uint16)len(b);
    }
    t.str = (@string)b;
}

internal static nint genCoreBytes(this ref Tag t, slice<byte> buf) {
    nint n = t.LangID.StringToBuf(buf[..]);
    if (t.ScriptID != 0) {
        n += copy(buf.slice(n), "-"u8);
        n += copy(buf.slice(n), t.ScriptID.String());
    }
    if (t.RegionID != 0) {
        n += copy(buf.slice(n), "-"u8);
        n += copy(buf.slice(n), t.RegionID.String());
    }
    return n;
}

public static @string String(this Tag t) {
    if (t.str != ""u8) {
        return t.str;
    }
    if (t.ScriptID == 0 && t.RegionID == 0) {
        return t.LangID.String();
    }
    var buf = new byte[]{}.array(12);
    return (@string)(buf.slice(0, t.genCoreBytes(buf[..])));
}

public static (slice<byte> text, error err) MarshalText(this Tag t) {
    slice<byte> text = default;

    if (t.str != ""u8) {
        text = append(text, t.str.ꓸꓸꓸ);
    } else if (t.ScriptID == 0 && t.RegionID == 0) {
        text = append(text, t.LangID.String().ꓸꓸꓸ);
    } else {
        var buf = new byte[]{}.array(12);
        text = buf.slice(0, t.genCoreBytes(buf[..]));
    }
    return (text, default);
}

public static error UnmarshalText(this ref Tag t, slice<byte> text) {
    var (tag, err) = Parse((@string)text);
    t = tag;
    return err;
}

public static @string Variants(this Tag t) {
    if (t.pVariant == 0) {
        return "";
    }
    return t.str.slice(t.pVariant, t.pExt);
}

public static @string VariantOrPrivateUseTags(this Tag t) {
    if (t.pExt > 0) {
        return t.str.slice(t.pVariant, t.pExt);
    }
    return t.str.slice(t.pVariant);
}

public static bool HasString(this Tag t) {
    return t.str != ""u8;
}

public static Tag Parent(this Tag t) {
    if (t.str != ""u8) {
        var (b, s, r) = t.Raw();
        t = new Tag(LangID: b, ScriptID: s, RegionID: r);
        if (t.RegionID == 0 && t.ScriptID != 0 && t.LangID != 0) {
            var (@base, _) = addTags(new Tag(LangID: t.LangID));
            if (@base.ScriptID == t.ScriptID) {
                return new Tag(LangID: t.LangID);
            }
        }
        return t;
    }
    if (t.LangID != 0) {
        if (t.RegionID != 0) {
            var maxScript = t.ScriptID;
            if (maxScript == 0) {
                var (max, _) = addTags(t);
                maxScript = max.ScriptID;
            }

            foreach (var i in range(len(parents))) {
                if (((Language)parents[i].lang) == t.LangID && ((Script)parents[i].maxScript) == maxScript) {
                    foreach (var (_, r) in range(parents[i].fromRegion)) {
                        if (((Region)r) == t.RegionID) {
                            return new Tag(
                                LangID: t.LangID,
                                ScriptID: ((Script)parents[i].script),
                                RegionID: ((Region)parents[i].toRegion)
                            );
                        }
                    }
                }
            }

            var (@base, _) = addTags(new Tag(LangID: t.LangID));
            if (@base.ScriptID != maxScript) {
                return new Tag(LangID: t.LangID, ScriptID: maxScript);
            }
            return new Tag(LangID: t.LangID);
        } else if (t.ScriptID != 0) {
            var (@base, _) = addTags(new Tag(LangID: t.LangID));
            if (@base.ScriptID != t.ScriptID) {
                return Und;
            }
            return new Tag(LangID: t.LangID);
        }
    }
    return Und;
}

} partial class language_package {

public static (@string ext, error err) ParseExtension(@string s) {
    @string ext = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        defer(() => {
            if (recover() != nil) {
                ext = "";
                err = ErrSyntax;
            }
        }, ref ᒐ);

        ref var scan = ref heap<scanner>(makeScannerString(s), out var Ꮡscan);
        nint end = default;
        nint n = len(scan.token); if (n != 1) {
            (ext, err) = ("", ErrSyntax); goto ᒐdone;
        }
        scan.toLower(0, len(scan.b));
        end = parseExtension(Ꮡscan);
        if (end != len(s)) {
            (ext, err) = ("", ErrSyntax); goto ᒐdone;
        }
        (ext, err) = ((@string)scan.b, default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (ext, err);
}

} partial class languageᴛTag {

public static bool HasVariants(this Tag t) {
    return (uint16)t.pVariant < t.pExt;
}

public static bool HasExtensions(this Tag t) {
    return (nint)t.pExt < len(t.str);
}

public static (@string ext, bool ok) Extension(this Tag t, byte x) {
    for (nint i = (nint)t.pExt; i < len(t.str) - 1; ) {
        @string extΔ1 = default;
        (i, extΔ1) = getExtension(t.str, i);
        if (extΔ1[0] == x) {
            return (extΔ1, true);
        }
    }
    return ("", false);
}

public static slice<@string> Extensions(this Tag t) {
    var eᴷ = new stackAppend<@string>(moves: true);

    var e = slice<@string>([]);
    for (nint i = (nint)t.pExt; i < len(t.str) - 1; ) {
        @string ext = default;
        (i, ext) = getExtension(t.str, i);
        e = append(eᴷ, e, ext);
    }
    e = move2heap(eᴷ, e);
    return e;
}

public static @string TypeForKey(this Tag t, @string key) {
    var (_, start, end, _) = t.findTypeForKey(key); if (end != start) {
        @string s = t.str.slice(start, end);
        nint p = strings.IndexByte(s, (byte)'-'); if (p >= 0) {
            s = s.slice(0, p);
        }
        return s;
    }
    return "";
}

} partial class language_package {

internal static error errPrivateUse = errors.New("cannot set a key on a private use tag");
internal static error errInvalidArguments = errors.New("invalid key or type");

} partial class languageᴛTag {

public static (Tag, error) SetTypeForKey(this Tag t, @string key, @string value) {
    if (t.IsPrivateUse()) {
        return (t, errPrivateUse);
    }
    if (len(key) != 2) {
        return (t, errInvalidArguments);
    }

    if (value == ""u8) {
        var (start, sep, end, _) = t.findTypeForKey(key);
        if (start != sep) {
            switch (ᐧ) {
            case {} when t.str[start - 2] is not (byte)'-': {
                break;
            }
            case {} when (end == len(t.str)) || (end + 2 < len(t.str) && t.str[end + 2] == (byte)'-'): {
                start -= 2;
                break;
            }}

            if (start == (nint)t.pVariant && end == len(t.str)) {
                t.str = "";
                (t.pVariant, t.pExt) = (0, 0);
            } else {
                t.str = fmt.Sprintf("%s%s"u8, t.str.slice(0, start), t.str.slice(end));
            }
        }
        return (t, default);
    }

    if (len(value) < 3 || len(value) > 8) {
        return (t, errInvalidArguments);
    }

    array<byte> buf = new(26); /* maxCoreSize + maxSimpleUExtensionSize */
    nint uStart = default;

    if (t.str == ""u8) {
        uStart = t.genCoreBytes(buf[..]);
        buf[uStart] = (byte)'-';
        uStart++;
    }

    var b = buf.slice(uStart);
    copy(b, "u-"u8);
    copy(b[2..], key);
    b[4] = (byte)'-';
    b = b.slice(0, 5 + copy(b[5..], value));
    ref var scan = ref heap<scanner>(makeScanner(b), out var Ꮡscan);
    parseExtensions(Ꮡscan); if (scan.err != nil) {
        return (t, scan.err);
    }

    if (t.str == ""u8) {
        (t.pVariant, t.pExt) = ((byte)(uStart - 1), (uint16)(uStart - 1));
        t.str = (@string)(buf.slice(0, uStart + len(b)));
    } else {
        @string s = t.str;
        var (start, sep, end, hasExt) = t.findTypeForKey(key);
        if (start == sep) {
            if (hasExt) {
                b = b[2..];
            }
            t.str = fmt.Sprintf("%s-%s%s"u8, s.slice(0, sep), b, s.slice(end));
        } else {
            t.str = fmt.Sprintf("%s-%s%s"u8, s.slice(0, start + 3), value, s.slice(end));
        }
    }
    return (t, default);
}

internal static (nint start, nint sep, nint end, bool hasExt) findTypeForKey(this Tag t, @string key) {
    nint start = default;
    nint sep = default;
    nint end = default;

    nint p = (nint)t.pExt;
    if (len(key) != 2 || p == len(t.str) || p == 0) {
        return (p, p, p, false);
    }
    @string s = t.str;

    for (p++; s[p] != 'u'; p++) {
        if (s[p] > 'u') {
            p--;
            return (p, p, p, false);
        }
        p = nextExtension(s, p); if (p == len(s)) {
            return (len(s), len(s), len(s), false);
        }
    }
    p++;

    @string curKey = "";

    for (;;) {
        end = p;
        for (p++; p < len(s) && s[p] != '-'; p++) {
        }
        nint n = p - end - 1;
        if (n <= 2 && curKey == key) {
            if (sep < end) {
                sep++;
            }
            return (start, sep, end, true);
        }
        switch (n) {
        case 0 or 1: {
            return (end, end, end, true);
        }
        case 2: {
            curKey = s.slice(end + 1, p);
            if (curKey > key) {
                return (end, end, end, true);
            }
            start = end;
            sep = p;
            break;
        }}

    }
}

} partial class language_package {

public static (Language l, error err) ParseBase(@string s) {
    Language l = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        defer(() => {
            if (recover() != nil) {
                l = 0;
                err = ErrSyntax;
            }
        }, ref ᒐ);

        nint n = len(s); if (n < 2 || 3 < n) {
            (l, err) = (0, ErrSyntax); goto ᒐdone;
        }
        array<byte> buf = new(3);
        (l, err) = getLangID(buf.slice(0, copy(buf[..], s)));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (l, err);
}

public static (Script scr, error err) ParseScript(@string s) {
    Script scr = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        defer(() => {
            if (recover() != nil) {
                scr = 0;
                err = ErrSyntax;
            }
        }, ref ᒐ);

        if (len(s) != 4) {
            (scr, err) = (0, ErrSyntax); goto ᒐdone;
        }
        array<byte> buf = new(4);
        (scr, err) = getScriptID(script, buf.slice(0, copy(buf[..], s)));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (scr, err);
}

public static (Region, error) EncodeM49(nint r) {
    return getRegionM49(r);
}

public static (Region r, error err) ParseRegion(@string s) {
    Region r = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        defer(() => {
            if (recover() != nil) {
                r = 0;
                err = ErrSyntax;
            }
        }, ref ᒐ);

        nint n = len(s); if (n < 2 || 3 < n) {
            (r, err) = (0, ErrSyntax); goto ᒐdone;
        }
        array<byte> buf = new(3);
        (r, err) = getRegionID(buf.slice(0, copy(buf[..], s)));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (r, err);
}

} partial class languageᴛRegion {

public static bool IsCountry(this Region r) {
    if (r == 0 || r.IsGroup() || r.IsPrivateUse() && r != _XK) {
        return false;
    }
    return true;
}

public static bool IsGroup(this Region r) {
    if (r == 0) {
        return false;
    }
    return (nint)regionInclusion[r] < len(regionContainment);
}

public static bool Contains(this Region r, Region c) {
    if (r == c) {
        return true;
    }
    var g = regionInclusion[r];
    if (g >= nRegionGroups) {
        return false;
    }
    var m = regionContainment[g];

    var d = regionInclusion[c];
    var b = regionInclusionBits[d];

    if (d >= nRegionGroups) {
        return (b & m) != 0;
    }
    return (b & ~m) == 0;
}

} partial class language_package {

internal static error errNoTLD = errors.New("language: region is not a valid ccTLD");

} partial class languageᴛRegion {

public static (Region, error) TLD(this Region r) {
    if (r == _GB) {
        r = _UK;
    }
    if (((byte)(r.typ() & (byte)ccTLD)) == 0) {
        return (0, errNoTLD);
    }
    return (r, default);
}

public static Region Canonicalize(this Region r) {
    var cr = normRegion(r); if (cr != 0) {
        return cr;
    }
    return r;
}

} partial class language_package {

partial struct Variant {
    public uint8 ID;
    internal @string str;
}

public static (Variant v, error err) ParseVariant(@string s) {
    Variant v = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        defer(() => {
            if (recover() != nil) {
                v = new Variant();
                err = ErrSyntax;
            }
        }, ref ᒐ);

        s = strings.ToLower(s);
        var (id, ok) = variantIndex[s, ꟷ]; if (ok) {
            (v, err) = (new Variant(id, s), default); goto ᒐdone;
        }
        (v, err) = (new Variant(), NewValueError(slice<byte>(s)));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (v, err);
}

} partial class languageᴛVariant {

public static @string String(this Variant v) {
    return v.str;
}

} // end languageᴛVariant
