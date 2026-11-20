// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

using bytes = bytes_package;
using fmt = fmt_package;
using sort = sort_package;
using strconv = strconv_package;
using tag = go.golang.org.x.text.@internal.tag_package;
using go.golang.org.x.text.@internal;
using io = io_package;
using static language_package;

partial class language_package {

internal static (nint index, error err) findIndex(tag.Index idx, slice<byte> key, @string form) {
    if (!tag.FixCase(form, key)) {
        return (0, ErrSyntax);
    }
    nint i = idx.Index(key);
    if (i == -1) {
        return (0, NewValueError(key));
    }
    return (i, default);
}

internal static nint searchUint(slice<uint16> imap, uint16 key) {
    var imapʗ1 = imap;
    return sort.Search(len(imap), (nint i) => imapʗ1[i] >= key);
}

partial struct Language /*num:uint16*/;

internal static (Language, error) getLangID(slice<byte> s) {
    if (len(s) == 2) {
        return getLangISO2(s);
    }
    return getLangISO3(s);
}

} partial class languageᴛLanguage {

public static (Language, AliasType) Canonicalize(this Language id) {
    return normLang(id);
}

} partial class language_package {

internal static (Language, AliasType) normLang(Language id) {
    nint k = sort.Search(len(AliasMap), (nint i) => AliasMap[i].From >= (uint16)id);
    if (k < len(AliasMap) && AliasMap[k].From == (uint16)id) {
        return ((Language)AliasMap[k].To, AliasTypes[k]);
    }
    return (id, AliasTypeUnknown);
}

internal static (Language, error) getLangISO2(slice<byte> s) {
    if (!tag.FixCase("zz", s)) {
        return (0, ErrSyntax);
    }
    nint i = lang.Index(s); if (i != -1 && lang.Elem(i)[3] != 0) {
        return ((Language)(uint16)i, default);
    }
    return (0, NewValueError(s));
}

internal static UntypedInt @base => /* 'z' - 'a' + 1 */ 26;

internal static nuint strToInt(slice<byte> s) {
    nuint v = (nuint)0;
    for (nint i = 0; i < len(s); i++) {
        v *= @base;
        v += (nuint)((byte)(s[i] - 'a'));
    }
    return v;
}

internal static void intToStr(nuint v, slice<byte> s) {
    for (nint i = len(s) - 1; i >= 0; i--) {
        s[i] = (byte)((byte)(v % (nuint)@base) + 'a');
        v /= @base;
    }
}

internal static (Language, error) getLangISO3(slice<byte> s) {
    if (tag.FixCase("und", s)) {
        for (nint i = lang.Index(s[..2]); i != -1; i = lang.Next(s[..2], i)) {
            @string e = lang.Elem(i); if (e[3] == 0 && e[2] == s[2]) {
                var id = (Language)(uint16)i;
                if (id == nonCanonicalUnd) {
                    return (0, default);
                }
                return (id, default);
            }
        }
        {
            nint i = altLangISO3.Index(s); if (i != -1) {
                return ((Language)altLangIndex[altLangISO3.Elem(i)[3]], default);
            }
        }
        nuint n = strToInt(s);
        if ((uint8)(langNoIndex[n / 8] & ((uint8)(1 << (int)(n % 8)))) != 0) {
            return ((Language)(((Language)(uint16)n) + (uint16)langNoIndexOffset), default);
        }
        for (nint i = lang.Index(s[..1]); i != -1; i = lang.Next(s[..1], i)) {
            @string e = lang.Elem(i); if (e[2] == s[1] && e[3] == s[2]) {
                return ((Language)(uint16)i, default);
            }
        }
        return (0, NewValueError(s));
    }
    return (0, ErrSyntax);
}

} partial class languageᴛLanguage {

public static nint StringToBuf(this Language id, slice<byte> b) {
    if (id >= langNoIndexOffset) {
        intToStr((nuint)id - (nuint)langNoIndexOffset, b[..3]);
        return 3;
    } else if (id == 0) {
        return copy(b, "und"u8);
    }
    tag.Index l = lang.slice((Language)(uint16)(id << 2));
    if (l[3] == 0) {
        return copy(b, l[..3]);
    }
    return copy(b, l[..2]);
}

public static @string String(this Language b) {
    if (b == 0) {
        return "und";
    } else if (b >= langNoIndexOffset) {
        b -= langNoIndexOffset;
        var buf = new byte[]{}.array(3);
        intToStr((nuint)b, buf[..]);
        return (@string)(buf[..]);
    }
    @string l = lang.Elem((nint)b);
    if (l[3] == 0) {
        return l[..3];
    }
    return l[..2];
}

public static @string ISO3(this Language b) {
    if (b == 0 || b >= langNoIndexOffset) {
        return b.String();
    }
    @string l = lang.Elem((nint)b);
    if (l[3] == 0) {
        return l[..3];
    } else if (l[2] == 0) {
        return altLangISO3.Elem((nint)l[3])[..3];
    }
    return l[0..1] + l[2..4];
}

public static bool IsPrivateUse(this Language b) {
    return langPrivateStart <= b && b <= langPrivateEnd;
}

public static Script SuppressScript(this Language b) {
    if (b < langNoIndexOffset) {
        return (Script)(uint16)suppressScript[b];
    }
    return 0;
}

} partial class language_package {

partial struct Region /*num:uint16*/;

internal static (Region, error) getRegionID(slice<byte> s) {
    if (len(s) == 3) {
        if (isAlpha(s[0])) {
            return getRegionISO3(s);
        }
        var (i, err) = strconv.ParseUint((@string)s, 10, 10); if (err == nil) {
            return getRegionM49((nint)i);
        }
    }
    return getRegionISO2(s);
}

internal static (Region, error) getRegionISO2(slice<byte> s) {
    var (i, err) = findIndex(regionISO, s, "ZZ");
    if (err != nil) {
        return (0, err);
    }
    return ((Region)(((Region)(uint16)i) + (uint16)isoRegionOffset), default);
}

internal static (Region, error) getRegionISO3(slice<byte> s) {
    if (tag.FixCase("ZZZ", s)) {
        for (nint i = regionISO.Index(s[..1]); i != -1; i = regionISO.Next(s[..1], i)) {
            @string e = regionISO.Elem(i); if (e[2] == s[1] && e[3] == s[2]) {
                return ((Region)(((Region)(uint16)i) + (uint16)isoRegionOffset), default);
            }
        }
        for (nint i = 0; i < len(altRegionISO3); i += 3) {
            if (tag.Compare(altRegionISO3.slice(i, i + 3), s) == 0) {
                return ((Region)altRegionIDs[i / 3], default);
            }
        }
        return (0, NewValueError(s));
    }
    return (0, ErrSyntax);
}

internal static (Region, error) getRegionM49(nint n) {
    if (0 < n && n <= 999) {
        UntypedInt searchBits = 7;
        UntypedInt regionBits = 9;
        const uint16 regionMask = /* 1<<regionBits - 1 */ 511;
        nint idx = n >> (int)(searchBits);
        var buf = fromM49.slice(m49Index[idx], m49Index[idx + 1]);
        var val = (uint16)((uint16)n << (int)(regionBits));
        var bufʗ1 = buf;
        nint i = sort.Search(len(buf), (nint iΔ1) => bufʗ1[iΔ1] >= val);
        var r = fromM49[(nint)m49Index[idx] + i]; if ((uint16)(r & ~regionMask) == val) {
            return ((Region)((uint16)(r & regionMask)), default);
        }
    }
    ValueError e = new();
    fmt.Fprint(new bytes_BufferжWriter(bytes.NewBuffer((slice<byte>)(e.v[..]))), n);
    return (0, e.ΔClone());
}

internal static Region normRegion(Region r) {
    ref var m = ref heap<array<FromTo>>(regionOldMap.Clone(), out var Ꮡm);
    var mʗ1 = m;
    nint k = sort.Search(len(m), (nint i) => mʗ1[i].From >= (uint16)r);
    if (k < len(m) && m[k].From == (uint16)r) {
        return (Region)m[k].To;
    }
    return 0;
}

internal static UntypedInt iso3166UserAssigned => /* 1 << iota */ 1;
internal static UntypedInt ccTLD => 2;
internal static UntypedInt bcp47Region => 4;

} partial class languageᴛRegion {

internal static byte typ(this Region r) {
    return regionTypes[r];
}

public static @string String(this Region r) {
    if (r < isoRegionOffset) {
        if (r == 0) {
            return "ZZ";
        }
        return fmt.Sprintf("%03d"u8, r.M49());
    }
    r -= isoRegionOffset;
    return regionISO.Elem((nint)r)[..2];
}

public static @string ISO3(this Region r) {
    if (r < isoRegionOffset) {
        return "ZZZ";
    }
    r -= isoRegionOffset;
    @string reg = regionISO.Elem((nint)r);
    switch (reg[2]) {
    case 0: {
        return altRegionISO3.slice(reg[3])[..3];
    }
    case (byte)' ': {
        return "ZZZ";
    }}

    return reg[0..1] + reg[2..4];
}

public static nint M49(this Region r) {
    return (nint)m49[r];
}

public static bool IsPrivateUse(this Region r) {
    return (byte)(r.typ() & (byte)iso3166UserAssigned) != 0;
}

} partial class language_package {

partial struct Script /*num:uint16*/;

internal static (Script, error) getScriptID(tag.Index idx, slice<byte> s) {
    var (i, err) = findIndex(idx, s, "Zzzz");
    return ((Script)(uint16)i, err);
}

} partial class languageᴛScript {

public static @string String(this Script s) {
    if (s == 0) {
        return "Zzzz";
    }
    return script.Elem((nint)s);
}

public static bool IsPrivateUse(this Script s) {
    return _Qaaa <= s && s <= _Qabx;
}

} partial class language_package {

internal const nint maxAltTaglen = /* len("en-US-POSIX") */ 11;
internal const nint maxLen = /* maxAltTaglen */ 11;

[GoLazy] partial class lazyᴛgrandfatheredMap { internal static map<array<byte>, int16> grandfatheredMap = new map<array<byte>, int16>{
    [new byte[]{(byte)'a', (byte)'r', (byte)'t', (byte)'-', (byte)'l', (byte)'o', (byte)'j', (byte)'b', (byte)'a', (byte)'n'}.array(11)] = _jbo,
    [new byte[]{(byte)'i', (byte)'-', (byte)'a', (byte)'m', (byte)'i'}.array(11)] = _ami,
    [new byte[]{(byte)'i', (byte)'-', (byte)'b', (byte)'n', (byte)'n'}.array(11)] = _bnn,
    [new byte[]{(byte)'i', (byte)'-', (byte)'h', (byte)'a', (byte)'k'}.array(11)] = _hak,
    [new byte[]{(byte)'i', (byte)'-', (byte)'k', (byte)'l', (byte)'i', (byte)'n', (byte)'g', (byte)'o', (byte)'n'}.array(11)] = _tlh,
    [new byte[]{(byte)'i', (byte)'-', (byte)'l', (byte)'u', (byte)'x'}.array(11)] = _lb,
    [new byte[]{(byte)'i', (byte)'-', (byte)'n', (byte)'a', (byte)'v', (byte)'a', (byte)'j', (byte)'o'}.array(11)] = _nv,
    [new byte[]{(byte)'i', (byte)'-', (byte)'p', (byte)'w', (byte)'n'}.array(11)] = _pwn,
    [new byte[]{(byte)'i', (byte)'-', (byte)'t', (byte)'a', (byte)'o'}.array(11)] = _tao,
    [new byte[]{(byte)'i', (byte)'-', (byte)'t', (byte)'a', (byte)'y'}.array(11)] = _tay,
    [new byte[]{(byte)'i', (byte)'-', (byte)'t', (byte)'s', (byte)'u'}.array(11)] = _tsu,
    [new byte[]{(byte)'n', (byte)'o', (byte)'-', (byte)'b', (byte)'o', (byte)'k'}.array(11)] = _nb,
    [new byte[]{(byte)'n', (byte)'o', (byte)'-', (byte)'n', (byte)'y', (byte)'n'}.array(11)] = _nn,
    [new byte[]{(byte)'s', (byte)'g', (byte)'n', (byte)'-', (byte)'b', (byte)'e', (byte)'-', (byte)'f', (byte)'r'}.array(11)] = _sfb,
    [new byte[]{(byte)'s', (byte)'g', (byte)'n', (byte)'-', (byte)'b', (byte)'e', (byte)'-', (byte)'n', (byte)'l'}.array(11)] = _vgt,
    [new byte[]{(byte)'s', (byte)'g', (byte)'n', (byte)'-', (byte)'c', (byte)'h', (byte)'-', (byte)'d', (byte)'e'}.array(11)] = _sgg,
    [new byte[]{(byte)'z', (byte)'h', (byte)'-', (byte)'g', (byte)'u', (byte)'o', (byte)'y', (byte)'u'}.array(11)] = _cmn,
    [new byte[]{(byte)'z', (byte)'h', (byte)'-', (byte)'h', (byte)'a', (byte)'k', (byte)'k', (byte)'a'}.array(11)] = _hak,
    [new byte[]{(byte)'z', (byte)'h', (byte)'-', (byte)'m', (byte)'i', (byte)'n', (byte)'-', (byte)'n', (byte)'a', (byte)'n'}.array(11)] = _nan,
    [new byte[]{(byte)'z', (byte)'h', (byte)'-', (byte)'x', (byte)'i', (byte)'a', (byte)'n', (byte)'g'}.array(11)] = _hsn,

    [new byte[]{(byte)'c', (byte)'e', (byte)'l', (byte)'-', (byte)'g', (byte)'a', (byte)'u', (byte)'l', (byte)'i', (byte)'s', (byte)'h'}.array()] = -1,
    [new byte[]{(byte)'e', (byte)'n', (byte)'-', (byte)'g', (byte)'b', (byte)'-', (byte)'o', (byte)'e', (byte)'d'}.array(11)] = -2,
    [new byte[]{(byte)'i', (byte)'-', (byte)'d', (byte)'e', (byte)'f', (byte)'a', (byte)'u', (byte)'l', (byte)'t'}.array(11)] = -3,
    [new byte[]{(byte)'i', (byte)'-', (byte)'e', (byte)'n', (byte)'o', (byte)'c', (byte)'h', (byte)'i', (byte)'a', (byte)'n'}.array(11)] = -4,
    [new byte[]{(byte)'i', (byte)'-', (byte)'m', (byte)'i', (byte)'n', (byte)'g', (byte)'o'}.array(11)] = -5,
    [new byte[]{(byte)'z', (byte)'h', (byte)'-', (byte)'m', (byte)'i', (byte)'n'}.array(11)] = -6,

    [new byte[]{(byte)'r', (byte)'o', (byte)'o', (byte)'t'}.array(11)] = 0,
    [new byte[]{(byte)'e', (byte)'n', (byte)'-', (byte)'u', (byte)'s', (byte)'-', (byte)'p', (byte)'o', (byte)'s', (byte)'i', (byte)'x'}.array()] = -7
}; }
internal static array<uint8> altTagIndex = new uint8[]{0, 17, 31, 45, 61, 74, 86, 102}.array();
internal static @string altTags = "xtg-x-cel-gaulishen-GB-oxendicten-x-i-defaultund-x-i-enochiansee-x-i-mingonan-x-zh-minen-US-u-va-posix";

internal static (Tag t, bool ok) grandfathered(/*[11]*/ array<byte> s) {
    Tag t = default;
    s = s.Clone();

    var (v, okΔ1) = grandfatheredMap[s, ꟷ]; if (okΔ1) {
        if (v < 0) {
            return (Make(altTags.slice(altTagIndex[(int16)(-v - 1)], altTagIndex[(int16)(-v)])), true);
        }
        t.LangID = (Language)(uint16)v;
        return (t, true);
    }
    return (t, false);
}

} // end language_package
