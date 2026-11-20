// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json.@internal;

using io = io_package;
using math = math_package;
using slices = slices_package;
using strconv = strconv_package;
using utf16 = go.unicode.utf16_package;
using utf8 = go.unicode.utf8_package;
using go.unicode;
using static jsonwire_package;

partial class jsonwire_package {

partial struct ValueFlags /*num:nuint*/;

internal static ValueFlags _ᴛ1ʗ => /* (1 << iota) / 2 */ 0;
internal static ValueFlags stringNonVerbatim => 1;
internal static ValueFlags stringNonCanonical => 2;

} partial class jsonwireᴛValueFlags {

public static void Join(this ref ValueFlags f, ValueFlags f2) { f |= (ValueFlags)(f2); }
public static bool IsVerbatim(this ValueFlags f) { return (ValueFlags)(f & stringNonVerbatim) == 0; }
public static bool IsCanonical(this ValueFlags f) { return (ValueFlags)(f & stringNonCanonical) == 0; }

} partial class jsonwire_package {

public static nint /*n*/ ConsumeWhitespace(slice<byte> b) {
    nint n = default;

    while (len(b) > n && (b[n] == ' ' || b[n] == '\t' || b[n] == '\r' || b[n] == '\n')) {
        n++;
    }
    return n;
}

public static nint ConsumeNull(slice<byte> b) {
    @string literal = "null";
    if (len(b) >= len(literal) && ((sstring)(b[..(int)(len(literal))])) == literal) {
        return len(literal);
    }
    return 0;
}

public static nint ConsumeFalse(slice<byte> b) {
    @string literal = "false";
    if (len(b) >= len(literal) && ((sstring)(b[..(int)(len(literal))])) == literal) {
        return len(literal);
    }
    return 0;
}

public static nint ConsumeTrue(slice<byte> b) {
    @string literal = "true";
    if (len(b) >= len(literal) && ((sstring)(b[..(int)(len(literal))])) == literal) {
        return len(literal);
    }
    return 0;
}

public static (nint n, error err) ConsumeLiteral(slice<byte> b, @string lit) {
    for (nint i = 0; i < len(b) && i < len(lit); i++) {
        if (b[i] != lit[i]) {
            return (i, NewInvalidCharacterError(b.slice(i), "in literal "u8 + lit + " (expecting "u8 + strconv.QuoteRune((rune)lit[i]) + ")"u8));
        }
    }
    if (len(b) < len(lit)) {
        return (len(b), io.ErrUnexpectedEOF);
    }
    return (len(lit), default);
}

public static nint /*n*/ ConsumeSimpleString(slice<byte> b) {
    nint n = default;

    if (len(b) > 0 && b[0] == '"') {
        n++;
        while (len(b) > n && b[n] < utf8.RuneSelf && escapeASCII[b[n]] == 0) {
            n++;
        }
        if ((nuint)len(b) > (nuint)n && b[n] == '"') {
            n++;
            return n;
        }
    }
    return 0;
}

public static (nint n, error err) ConsumeString(ж<ValueFlags> flags, slice<byte> b, bool validateUTF8) {
    return ConsumeStringResumable(flags, b, 0, validateUTF8);
}

public static (nint n, error err) ConsumeStringResumable(ж<ValueFlags> Ꮡflags, slice<byte> b, nint resumeOffset, bool validateUTF8) {
    nint n = default;
    ref var flags = ref Ꮡflags.DerefOrNull();

    switch (ᐧ) {
    case {} when resumeOffset is > 0: {
        n = resumeOffset;
        break;
    }
    case {} when (nuint)len(b) is 0: {
        return (n, io.ErrUnexpectedEOF);
    }
    case {} when b[0] is (byte)'"': {
        n++;
        break;
    }
    default: {
        return (n, NewInvalidCharacterError(b.slice(n), @"at start of string (expecting '""')"));
    }}

    while ((nuint)len(b) > (nuint)n) {
        bool noEscape(byte c) => c < utf8.RuneSelf && ' ' <= c && c != '\\' && c != '"';
        while ((nuint)len(b) > (nuint)n && noEscape(b[n])) {
            n++;
        }
        if ((nuint)len(b) <= (nuint)n) {
            return (n, io.ErrUnexpectedEOF);
        }

        if (b[n] == '"') {
            n++;
            return (n, default);
        }

        var (r, rn) = utf8.DecodeRune(b.slice(n));
        switch (ᐧ) {
        case {} when rn is > 1: {
            n += rn;
            break;
        }
        case {} when r is '\\': {
            flags.Join(stringNonVerbatim);
            resumeOffset = n;
            if ((nuint)len(b) < (nuint)(n + 2)) {
                return (resumeOffset, io.ErrUnexpectedEOF);
            }
            var rΔ4 = b[n + 1];
            switch (rΔ4) {
            case (byte)'/': {
                flags.Join(stringNonCanonical);
                n += 2;
                break;
            }
            case (byte)'"' or (byte)'\\' or (byte)'b' or (byte)'f' or (byte)'n' or (byte)'r' or (byte)'t': {
                n += 2;
                break;
            }
            case (byte)'u': {
                if ((nuint)len(b) < (nuint)(n + 6)) {
                    if (hasEscapedUTF16Prefix(b.slice(n), false)) {
                        return (resumeOffset, io.ErrUnexpectedEOF);
                    }
                    flags.Join(stringNonCanonical);
                    return (n, NewInvalidEscapeSequenceError(b.slice(n)));
                }
                var (v1, ok) = parseHexUint16(b.slice(n + 2, n + 6));
                if (!ok) {
                    flags.Join(stringNonCanonical);
                    return (n, NewInvalidEscapeSequenceError(b.slice(n, n + 6)));
                }
                switch (v1) {
                case '\b' or '\f' or '\n' or '\r' or '\t': {
                    flags.Join(stringNonCanonical);
                    break;
                }
                default: {
                    if (v1 >= ' ') {
                        flags.Join(stringNonCanonical);
                    } else {
                        foreach (var (_, c) in range(b.slice(n + 2, n + 6))) {
                            if ('A' <= c && c <= 'F') {
                                flags.Join(stringNonCanonical);
                            }
                        }
                    }
                    break;
                }}

                n += 6;

                var rΔ6 = (rune)v1;
                if (validateUTF8 && utf16.IsSurrogate(rΔ6)) {
                    if ((nuint)len(b) < (nuint)(n + 6)) {
                        if (hasEscapedUTF16Prefix(b.slice(n), true)) {
                            return (resumeOffset, io.ErrUnexpectedEOF);
                        }
                        flags.Join(stringNonCanonical);
                        return (n - 6, NewInvalidEscapeSequenceError(b.slice(n - 6)));
                    } else if (parseHexUint16(b.slice(n + 2, n + 6)) is var (v2, okΔ1) && (b[n] != '\\' || b[n + 1] != 'u' || !okΔ1)) {
                        flags.Join(stringNonCanonical);
                        return (n - 6, NewInvalidEscapeSequenceError(b.slice(n - 6, n + 6)));
                    } else {
                        rΔ6 = utf16.DecodeRune((rune)v1, (rune)v2); if (rΔ6 == utf8.RuneError) {
                            flags.Join(stringNonCanonical);
                            return (n - 6, NewInvalidEscapeSequenceError(b.slice(n - 6, n + 6)));
                        } else {
                            n += 6;
                        }
                    }
                }
                break;
            }
            default: {
                flags.Join(stringNonCanonical);
                return (n, NewInvalidEscapeSequenceError(b.slice(n, n + 2)));
            }}

            break;
        }
        case {} when r == utf8.RuneError: {
            if (!utf8.FullRune(b.slice(n))) {
                return (n, io.ErrUnexpectedEOF);
            }
            flags.Join((ValueFlags)(stringNonVerbatim | stringNonCanonical));
            if (validateUTF8) {
                return (n, ErrInvalidUTF8);
            }
            n++;
            break;
        }
        case {} when r is < ' ': {
            flags.Join((ValueFlags)(stringNonVerbatim | stringNonCanonical));
            return (n, NewInvalidCharacterError(b.slice(n), "in string (expecting non-control character)"));
        }
        default: {
            throw panic("BUG: unhandled character " + QuoteRune(b.slice(n)));
            break;
        }}

    }
    return (n, io.ErrUnexpectedEOF);
}

public static (slice<byte> v, error err) AppendUnquote(slice<byte> dst, slice<byte> src) {
    error err = default;

    dst = slices.Grow<slice<byte>, byte>(dst, len(src));

    nint i = default;
    nint n = default;
    switch (ᐧ) {
    case {} when (nuint)len(src) is 0: {
        return (dst, io.ErrUnexpectedEOF);
    }
    case {} when src[0] is (byte)'"': {
        (i, n) = (1, 1);
        break;
    }
    default: {
        return (dst, NewInvalidCharacterError(src, @"at start of string (expecting '""')"));
    }}

    while ((nuint)len(src) > (nuint)n) {
        bool noEscape(byte c) => c < utf8.RuneSelf && ' ' <= c && c != '\\' && c != '"';
        while ((nuint)len(src) > (nuint)n && noEscape(src[n])) {
            n++;
        }
        if ((nuint)len(src) <= (nuint)n) {
            dst = appendꓸꓸꓸ(dst, src.slice(i, n));
            return (dst, io.ErrUnexpectedEOF);
        }

        if (src[n] == '"') {
            dst = appendꓸꓸꓸ(dst, src.slice(i, n));
            n++;
            if (n < len(src)) {
                err = NewInvalidCharacterError(src.slice(n), "after string value");
            }
            return (dst, err);
        }

        var (r, rn) = utf8.DecodeRune(src.slice(n));
        switch (ᐧ) {
        case {} when rn is > 1: {
            n += rn;
            break;
        }
        case {} when r is '\\': {
            dst = appendꓸꓸꓸ(dst, src.slice(i, n));

            if ((nuint)len(src) < (nuint)(n + 2)) {
                return (dst, io.ErrUnexpectedEOF);
            }
            var rΔ4 = src[n + 1];
            switch (rΔ4) {
            case (byte)'"' or (byte)'\\' or (byte)'/': {
                dst = append(dst, rΔ4);
                n += 2;
                break;
            }
            case (byte)'b': {
                dst = append(dst, (byte)'\b');
                n += 2;
                break;
            }
            case (byte)'f': {
                dst = append(dst, (byte)'\f');
                n += 2;
                break;
            }
            case (byte)'n': {
                dst = append(dst, (byte)'\n');
                n += 2;
                break;
            }
            case (byte)'r': {
                dst = append(dst, (byte)'\r');
                n += 2;
                break;
            }
            case (byte)'t': {
                dst = append(dst, (byte)'\t');
                n += 2;
                break;
            }
            case (byte)'u': {
                if ((nuint)len(src) < (nuint)(n + 6)) {
                    if (hasEscapedUTF16Prefix(src.slice(n), false)) {
                        return (dst, io.ErrUnexpectedEOF);
                    }
                    return (dst, NewInvalidEscapeSequenceError(src.slice(n)));
                }
                var (v1, ok) = parseHexUint16(src.slice(n + 2, n + 6));
                if (!ok) {
                    return (dst, NewInvalidEscapeSequenceError(src.slice(n, n + 6)));
                }
                n += 6;

                var rΔ6 = (rune)v1;
                if (utf16.IsSurrogate(rΔ6)) {
                    rΔ6 = utf8.RuneError;
                    if ((nuint)len(src) < (nuint)(n + 6)) {
                        if (hasEscapedUTF16Prefix(src.slice(n), true)) {
                            return (utf8.AppendRune(dst, rΔ6), io.ErrUnexpectedEOF);
                        }
                        err = NewInvalidEscapeSequenceError(src.slice(n - 6));
                    } else if (parseHexUint16(src.slice(n + 2, n + 6)) is var (v2, okΔ1) && (src[n] != '\\' || src[n + 1] != 'u' || !okΔ1)) {
                        err = NewInvalidEscapeSequenceError(src.slice(n - 6, n + 6));
                    } else {
                        rΔ6 = utf16.DecodeRune((rune)v1, (rune)v2); if (rΔ6 == utf8.RuneError) {
                            err = NewInvalidEscapeSequenceError(src.slice(n - 6, n + 6));
                        } else {
                            n += 6;
                        }
                    }
                }

                dst = utf8.AppendRune(dst, rΔ6);
                break;
            }
            default: {
                return (dst, NewInvalidEscapeSequenceError(src.slice(n, n + 2)));
            }}

            i = n;
            break;
        }
        case {} when r == utf8.RuneError: {
            dst = appendꓸꓸꓸ(dst, src.slice(i, n));
            if (!utf8.FullRune(src.slice(n))) {
                return (dst, io.ErrUnexpectedEOF);
            }
            dst = append(dst, ((@string)"\uFFFD").ꓸꓸꓸ);
            n += rn;
            i = n;
            err = ErrInvalidUTF8;
            break;
        }
        case {} when r is < ' ': {
            dst = appendꓸꓸꓸ(dst, src.slice(i, n));
            return (dst, NewInvalidCharacterError(src.slice(n), "in string (expecting non-control character)"));
        }
        default: {
            throw panic("BUG: unhandled character " + QuoteRune(src.slice(n)));
            break;
        }}

    }
    dst = appendꓸꓸꓸ(dst, src.slice(i, n));
    return (dst, io.ErrUnexpectedEOF);
}

internal static bool hasEscapedUTF16Prefix(slice<byte> b, bool lowerSurrogateHalf) {
    foreach (var i in range(len(b))) {
        var c = b[i];
        switch (ᐧ) {
        case {} when i == 0 && c != (byte)'\\': {
            return false;
        }
        case {} when i == 1 && c != (byte)'u': {
            return false;
        }
        case {} when i == 2 && lowerSurrogateHalf && c != (byte)'d' && c != (byte)'D': {
            return false;
        }
        case {} when i == 3 && lowerSurrogateHalf && !('c' <= c && c <= 'f') && !('C' <= c && c <= 'F'): {
            return false;
        }
        case {} when i >= 2 && i < 6 && !('0' <= c && c <= '9') && !('a' <= c && c <= 'f') && !('A' <= c && c <= 'F'): {
            return false;
        }}

    }
    return true;
}

public static slice<byte> UnquoteMayCopy(slice<byte> b, bool isVerbatim) {
    if (isVerbatim) {
        return b.slice(len(@""""), len(b) - len(@""""));
    }
    (b, _) = AppendUnquote(default, b);
    return b;
}

public static nint /*n*/ ConsumeSimpleNumber(slice<byte> b) {
    nint n = default;

    if (len(b) > 0) {
        if (b[0] == '0') {
            n++;
        } else if ('1' <= b[0] && b[0] <= '9') {
            n++;
            while (len(b) > n && ('0' <= b[n] && b[n] <= '9')) {
                n++;
            }
        } else {
            return 0;
        }
        if ((nuint)len(b) <= (nuint)n || (b[n] != '.' && b[n] != 'e' && b[n] != 'E')) {
            return n;
        }
    }
    return 0;
}

partial struct ConsumeNumberState /*num:nuint*/;

internal static ConsumeNumberState consumeNumberInit => /* iota */ 0;
internal static ConsumeNumberState beforeIntegerDigits => 1;
internal static ConsumeNumberState withinIntegerDigits => 2;
internal static ConsumeNumberState beforeFractionalDigits => 3;
internal static ConsumeNumberState withinFractionalDigits => 4;
internal static ConsumeNumberState beforeExponentDigits => 5;
internal static ConsumeNumberState withinExponentDigits => 6;

public static (nint n, error err) ConsumeNumber(slice<byte> b) {
    nint n = default;
    error err = default;

    (n, _, err) = ConsumeNumberResumable(b, 0, consumeNumberInit);
    return (n, err);
}

public static (nint n, ConsumeNumberState, error err) ConsumeNumberResumable(slice<byte> b, nint resumeOffset, ConsumeNumberState state) {
    nint n = default;

    n = resumeOffset;
    if (state > consumeNumberInit) {
        switch (state) {
        case var ᴛ when ᴛ == withinIntegerDigits || ᴛ == withinFractionalDigits || ᴛ == withinExponentDigits: {
            while ((nuint)len(b) > (nuint)n && ('0' <= b[n] && b[n] <= '9')) {
                n++;
            }
            if ((nuint)len(b) <= (nuint)n) {
                return (n, state, default);
            }
            state++;
            break;
        }}

        switch (state) {
        case var ᴛ when ᴛ == beforeIntegerDigits: {
            goto beforeInteger;
            break;
        }
        case var ᴛ when ᴛ == beforeFractionalDigits: {
            goto beforeFractional;
            break;
        }
        case var ᴛ when ᴛ == beforeExponentDigits: {
            goto beforeExponent;
            break;
        }
        default: {
            return (n, state, default);
        }}

    }

beforeInteger:
    resumeOffset = n;
    if ((nuint)len(b) > 0 && b[0] == '-') {
        n++;
    }
    switch (ᐧ) {
    case {} when (nuint)len(b) <= (nuint)n: {
        return (resumeOffset, beforeIntegerDigits, io.ErrUnexpectedEOF);
    }
    case {} when b[n] is (byte)'0': {
        n++;
        state = beforeFractionalDigits;
        break;
    }
    case {} when (byte)'1' <= b[n] && b[n] <= (byte)'9': {
        n++;
        while ((nuint)len(b) > (nuint)n && ('0' <= b[n] && b[n] <= '9')) {
            n++;
        }
        state = withinIntegerDigits;
        break;
    }
    default: {
        return (n, state, NewInvalidCharacterError(b.slice(n), "in number (expecting digit)"));
    }}

beforeFractional:
    if ((nuint)len(b) > (nuint)n && b[n] == '.') {
        resumeOffset = n;
        n++;
        switch (ᐧ) {
        case {} when (nuint)len(b) <= (nuint)n: {
            return (resumeOffset, beforeFractionalDigits, io.ErrUnexpectedEOF);
        }
        case {} when (byte)'0' <= b[n] && b[n] <= (byte)'9': {
            n++;
            break;
        }
        default: {
            return (n, state, NewInvalidCharacterError(b.slice(n), "in number (expecting digit)"));
        }}

        while ((nuint)len(b) > (nuint)n && ('0' <= b[n] && b[n] <= '9')) {
            n++;
        }
        state = withinFractionalDigits;
    }

beforeExponent:
    if ((nuint)len(b) > (nuint)n && (b[n] == 'e' || b[n] == 'E')) {
        resumeOffset = n;
        n++;
        if ((nuint)len(b) > (nuint)n && (b[n] == '-' || b[n] == '+')) {
            n++;
        }
        switch (ᐧ) {
        case {} when (nuint)len(b) <= (nuint)n: {
            return (resumeOffset, beforeExponentDigits, io.ErrUnexpectedEOF);
        }
        case {} when (byte)'0' <= b[n] && b[n] <= (byte)'9': {
            n++;
            break;
        }
        default: {
            return (n, state, NewInvalidCharacterError(b.slice(n), "in number (expecting digit)"));
        }}

        while ((nuint)len(b) > (nuint)n && ('0' <= b[n] && b[n] <= '9')) {
            n++;
        }
        state = withinExponentDigits;
    }

    return (n, state, default);
}

internal static (uint16 v, bool ok) parseHexUint16(slice<byte> b) {
    uint16 v = default;

    if (len(b) != 4) {
        return (0, false);
    }
    foreach (var i in range(4)) {
        var c = b[i];
        switch (ᐧ) {
        case {} when (byte)'0' <= c && c <= (byte)'9': {
            c = (byte)(c - '0');
            break;
        }
        case {} when (byte)'a' <= c && c <= (byte)'f': {
            c = (byte)(10 + c - 'a');
            break;
        }
        case {} when (byte)'A' <= c && c <= (byte)'F': {
            c = (byte)(10 + c - 'A');
            break;
        }
        default: {
            return (0, false);
        }}

        v = (uint16)(v * 16 + (uint16)c);
    }
    return (v, true);
}

public static (uint64 v, bool ok) ParseUint(slice<byte> b) {
    uint64 v = default;

    const nint unsafeWidth = 20;
    nint n = default;
    for (; len(b) > n && ('0' <= b[n] && b[n] <= '9'); n++) {
        v = 10 * v + (uint64)((byte)(b[n] - '0'));
    }
    switch (ᐧ) {
    case {} when n == 0 || len(b) != n || (b[0] == (byte)'0' && ((sstring)b) != "0"u8): {
        return (0, false);
    }
    case {} when n >= unsafeWidth && (b[0] != (byte)'1' || v < 10000000000000000000 || n > unsafeWidth): {
        return (math.MaxUint64, false);
    }}

    return (v, true);
}

} // end jsonwire_package
