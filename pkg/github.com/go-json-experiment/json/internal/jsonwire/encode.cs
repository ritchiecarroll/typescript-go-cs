// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json.@internal;

using math = math_package;
using slices = slices_package;
using strconv = strconv_package;
using utf16 = go.unicode.utf16_package;
using utf8 = go.unicode.utf8_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using go.github.com.go_json_experiment.json.@internal;
using go.unicode;

partial class jsonwire_package {

[GoLazy] partial class lazyᴛescapeASCII { internal static array<uint8> escapeASCII = new uint8[]{
    1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
    1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
    0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
}.array(); }

public static bool NeedEscape(slice<byte> src) {
    nint i = default;
    while ((nuint)len(src) > (nuint)i) {
        var c = src[i]; if (c < utf8.RuneSelf) {
            if (escapeASCII[c] > 0) {
                return true;
            }
            i++;
        } else {
            var (r, rn) = utf8.DecodeRune(src.slice(i));
            if (r == utf8.RuneError || r == '\u2028' || r == '\u2029') {
                return true;
            }
            i += rn;
        }
    }
    return false;
}

public static (slice<byte>, error) AppendQuote(slice<byte> dst, slice<byte> src, ж<jsonflags.Flags> flags) {
    nint i = default;
    nint n = default;
    bool hasInvalidUTF8 = default;
    dst = slices.Grow<slice<byte>, byte>(dst, len(@"""") + len(src) + len(@""""));
    dst = append(dst, (byte)'"');
    while ((nuint)len(src) > (nuint)n) {
        var c = src[n]; if (c < utf8.RuneSelf) {
            n++;
            if (escapeASCII[c] == 0) {
                continue;
            }
            if (!(c == '<' || c == '>' || c == '&') || flags.Get(jsonflags.EscapeForHTML)) {
                dst = appendꓸꓸꓸ(dst, src.slice(i, n - 1));
                dst = appendEscapedASCII(dst, c);
                i = n;
            }
        } else {
            var (r, rn) = utf8.DecodeRune(src.slice(n));
            n += rn;
            if (r != utf8.RuneError && r != '\u2028' && r != '\u2029') {
                continue;
            }
            switch (ᐧ) {
            case {} when isInvalidUTF8(r, rn): {
                hasInvalidUTF8 = true;
                dst = appendꓸꓸꓸ(dst, src.slice(i, n - rn));
                dst = append(dst, ((@string)"\ufffd").ꓸꓸꓸ);
                i = n;
                break;
            }
            case {} when (r == '\u2028' || r == '\u2029') && flags.Get(jsonflags.EscapeForJS): {
                dst = appendꓸꓸꓸ(dst, src.slice(i, n - rn));
                dst = appendEscapedUnicode(dst, r);
                i = n;
                break;
            }}

        }
    }
    dst = appendꓸꓸꓸ(dst, src.slice(i, n));
    dst = append(dst, (byte)'"');
    if (hasInvalidUTF8 && !flags.Get(jsonflags.AllowInvalidUTF8)) {
        return (dst, ErrInvalidUTF8);
    }
    return (dst, default);
}

internal static slice<byte> appendEscapedASCII(slice<byte> dst, byte c) {
    switch (c) {
    case (byte)'"' or (byte)'\\': {
        dst = append(dst, (byte)'\\', c);
        break;
    }
    case (byte)'\b': {
        dst = append(dst, ((@string)"\\b").ꓸꓸꓸ);
        break;
    }
    case (byte)'\f': {
        dst = append(dst, ((@string)"\\f").ꓸꓸꓸ);
        break;
    }
    case (byte)'\n': {
        dst = append(dst, ((@string)"\\n").ꓸꓸꓸ);
        break;
    }
    case (byte)'\r': {
        dst = append(dst, ((@string)"\\r").ꓸꓸꓸ);
        break;
    }
    case (byte)'\t': {
        dst = append(dst, ((@string)"\\t").ꓸꓸꓸ);
        break;
    }
    default: {
        dst = appendEscapedUTF16(dst, (uint16)c);
        break;
    }}

    return dst;
}

internal static slice<byte> appendEscapedUnicode(slice<byte> dst, rune r) {
    var (r1, r2) = utf16.EncodeRune(r); if (r1 != '\ufffd' && r2 != '\ufffd') {
        dst = appendEscapedUTF16(dst, (uint16)r1);
        dst = appendEscapedUTF16(dst, (uint16)r2);
    } else {
        dst = appendEscapedUTF16(dst, (uint16)r);
    }
    return dst;
}

internal static slice<byte> appendEscapedUTF16(slice<byte> dst, uint16 x) {
    @string hex = "0123456789abcdef";
    return append(dst, (byte)'\\', (byte)'u', hex[(uint16)((x >> 12) & 0xf)], hex[(uint16)((x >> 8) & 0xf)], hex[(uint16)((x >> 4) & 0xf)], hex[(uint16)((x >> 0) & 0xf)]);
}

public static (slice<byte>, nint, error) ReformatString(slice<byte> dst, slice<byte> src, ж<jsonflags.Flags> flags) {
    ref var valFlags = ref heap(new ValueFlags(), out var ᏑvalFlags);
    var (n, err) = ConsumeString(ᏑvalFlags, src, !flags.Get(jsonflags.AllowInvalidUTF8));
    if (err != nil) {
        return (dst, n, err);
    }

    if (!flags.Get(jsonflags.AnyEscape) && (valFlags.IsCanonical() || flags.Get(jsonflags.PreserveRawStrings))) {
        dst = appendꓸꓸꓸ(dst, src.slice(0, n));
        return (dst, n, default);
    }

    if (flags.Get(jsonflags.PreserveRawStrings)) {
        nint i = default;
        nint lastAppendIndex = default;
        while (i < n) {
            var c = src[i]; if (c < utf8.RuneSelf) {
                if ((c == '<' || c == '>' || c == '&') && flags.Get(jsonflags.EscapeForHTML)) {
                    dst = appendꓸꓸꓸ(dst, src.slice(lastAppendIndex, i));
                    dst = appendEscapedASCII(dst, c);
                    lastAppendIndex = i + 1;
                }
                i++;
            } else {
                var (r, rn) = utf8.DecodeRune(src.slice(i));
                if ((r == '\u2028' || r == '\u2029') && flags.Get(jsonflags.EscapeForJS)) {
                    dst = appendꓸꓸꓸ(dst, src.slice(lastAppendIndex, i));
                    dst = appendEscapedUnicode(dst, r);
                    lastAppendIndex = i + rn;
                }
                i += rn;
            }
        }
        return (appendꓸꓸꓸ(dst, src.slice(lastAppendIndex, n)), n, default);
    }

    var (b, _) = AppendUnquote(default, src.slice(0, n));
    (dst, _) = AppendQuote(dst, b, flags);
    return (dst, n, default);
}

public static slice<byte> AppendFloat(slice<byte> dst, float64 src, nint bits) {
    if (bits == 32) {
        src = (float64)(float32)src;
    }

    var abs = math.Abs(src);
    var fmt = (byte)'f';
    if (abs != 0D) {
        if (bits == 64 && ((float64)abs < 1e-6D || (float64)abs >= 1e21D) || bits == 32 && ((float32)abs < 1e-6F || (float32)abs >= 1e21F)) {
            fmt = (byte)'e';
        }
    }
    dst = strconv.AppendFloat(dst, src, fmt, -1, bits);
    if (fmt == 'e') {
        nint n = len(dst);
        if (n >= 4 && dst[n - 4] == 'e' && dst[n - 3] == '-' && dst[n - 2] == '0') {
            dst[n - 2] = dst[n - 1];
            dst = dst.slice(0, n - 1);
        }
    }
    return dst;
}

public static (slice<byte>, nint, error) ReformatNumber(slice<byte> dst, slice<byte> src, ж<jsonflags.Flags> flags) {
    var (n, err) = ConsumeNumber(src);
    if (err != nil) {
        return (dst, n, err);
    }
    if (!flags.Get(jsonflags.CanonicalizeNumbers)) {
        dst = appendꓸꓸꓸ(dst, src.slice(0, n));
        return (dst, n, default);
    }

    bool isFloat = default;
    foreach (var (_, c) in range(src.slice(0, n))) {
        if (c == '.' || c == 'e' || c == 'E') {
            isFloat = true;
            break;
        }
    }

    switch (ᐧ) {
    case {} when ((sstring)(src.slice(0, n))) == "-0"u8: {
        break;
        break;
    }
    case {} when isFloat: {
        if (!flags.Get(jsonflags.CanonicalizeRawFloats)) {
            dst = appendꓸꓸꓸ(dst, src.slice(0, n));
            return (dst, n, default);
        }
        break;
    }
    default: {
        const nint maxExactIntegerDigits = 16;
        if (!flags.Get(jsonflags.CanonicalizeRawInts) || n < maxExactIntegerDigits) {
            dst = appendꓸꓸꓸ(dst, src.slice(0, n));
            return (dst, n, default);
        }
        break;
    }}

    var (fv, _) = strconv.ParseFloat((@string)(src.slice(0, n)), 64);
    switch (ᐧ) {
    case {} when fv is 0D: {
        fv = 0D;
        break;
    }
    case {} when math.IsInf(fv, +1): {
        fv = +math.MaxFloat64;
        break;
    }
    case {} when math.IsInf(fv, -1): {
        fv = -math.MaxFloat64;
        break;
    }}

    return (AppendFloat(dst, fv, 64), n, default);
}

} // end jsonwire_package
