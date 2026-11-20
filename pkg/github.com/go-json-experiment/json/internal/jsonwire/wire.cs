// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json.@internal;

using cmp = cmp_package;
using errors = errors_package;
using strconv = strconv_package;
using strings = strings_package;
using unicode = unicode_package;
using utf16 = go.unicode.utf16_package;
using utf8 = go.unicode.utf8_package;
using go.unicode;
using static jsonwire_package;

partial class jsonwire_package {

public static slice<byte> TrimSuffixWhitespace(slice<byte> b) {
    nint n = len(b) - 1;
    while (n >= 0 && (b[n] == ' ' || b[n] == '\t' || b[n] == '\r' || b[n] == '\n')) {
        n--;
    }
    return b.slice(0, n + 1);
}

public static slice<byte> TrimSuffixString(slice<byte> b) {
    if (len(b) > 0 && b[len(b) - 1] == '"') {
        b = b.slice(0, len(b) - 1);
    }
    while (len(b) >= 2 && !(b[len(b) - 1] == '"' && b[len(b) - 2] != '\\')) {
        b = b.slice(0, len(b) - 1);
    }
    if (len(b) > 0 && b[len(b) - 1] == '"') {
        b = b.slice(0, len(b) - 1);
    }
    return b;
}

public static bool HasSuffixByte(slice<byte> b, byte c) {
    return len(b) > 0 && b[len(b) - 1] == c;
}

public static slice<byte> TrimSuffixByte(slice<byte> b, byte c) {
    if (len(b) > 0 && b[len(b) - 1] == c) {
        return b.slice(0, len(b) - 1);
    }
    return b;
}

public static @string QuoteRune(slice<byte> b) {
    var (r, n) = utf8.DecodeRune(b);
    if (r == utf8.RuneError && n == 1) {
        return @"'\x"u8 + strconv.FormatUint((uint64)b[0], 16) + @"'"u8;
    }
    return strconv.QuoteRune(r);
}

public static nint CompareUTF16(slice<byte> x, slice<byte> y) {
    bool isUTF16Self(rune r) => ('\u0000' <= r && r <= '\uD7FF') || ('\uE000' <= r && r <= '\uFFFF');

    for (;;) {
        if (len(x) == 0 || len(y) == 0) {
            return cmp.Compare(len(x), len(y));
        }

        if (x[0] < utf8.RuneSelf || y[0] < utf8.RuneSelf) {
            if (x[0] != y[0]) {
                return cmp.Compare(x[0], y[0]);
            }
            (x, y) = (x[1..], y[1..]);
            continue;
        }

        var (rx, nx) = utf8.DecodeRune(x);
        var (ry, ny) = utf8.DecodeRune(y);

        var selfx = isUTF16Self(rx);
        var selfy = isUTF16Self(ry);
        switch (ᐧ) {
        case {} when selfx && !selfy: {
            (ry, _) = utf16.EncodeRune(ry);
            break;
        }
        case {} when selfy && !selfx: {
            (rx, _) = utf16.EncodeRune(rx);
            break;
        }}

        if (rx != ry) {
            return cmp.Compare(rx, ry);
        }

        if (isInvalidUTF8(rx, nx) || isInvalidUTF8(ry, ny)) {
            if (x[0] != y[0]) {
                return cmp.Compare(x[0], y[0]);
            }
        }
        (x, y) = (x.slice(nx), y.slice(ny));
    }
}

public static error ErrInvalidUTF8 = errors.New("invalid UTF-8");

public static error NewInvalidCharacterError<Bytes>(Bytes prefix, @string where)
    where Bytes : /* ~[]byte | ~string */ IByteSeq<Bytes, byte>, new()
{
    var (_, n) = utf8.DecodeRune(prefix.ToSlice());
    return new InvalidTextErrorжerror(Ꮡ(new InvalidTextError("character", prefix.slice(0, n).ToGoString(), where)));
}

public static error NewInvalidEscapeSequenceError<Bytes>(Bytes what)
    where Bytes : /* ~[]byte | ~string */ IByteSeq<Bytes, byte>, new()
{
    if (len(what) > 6) {
        return new InvalidTextErrorжerror(Ꮡ(new InvalidTextError("surrogate pair", what.ToGoString(), "in string")));
    }
    return new InvalidTextErrorжerror(Ꮡ(new InvalidTextError("escape sequence", what.ToGoString(), "in string")));
}

partial struct InvalidTextError {
    public @string Label;
    public @string What;
    public @string Where;
}

} partial class jsonwireᴛInvalidTextError {

public static @string Error(this ref InvalidTextError e) {
    @string what = default;
    var needEscape = strings.ContainsFunc(e.What, (rune r) => r == '`' || r == utf8.RuneError || unicode.IsSpace(r) || !unicode.IsPrint(r));
    switch (ᐧ) {
    case {} when utf8.RuneCount(slice<byte>(e.What)) is 1: {
        what = QuoteRune(slice<byte>(e.What));
        break;
    }
    case {} when needEscape: {
        what = strconv.Quote(e.What);
        break;
    }
    default: {
        what = "`"u8 + e.What + "`"u8;
        break;
    }}

    return strings.TrimSuffix("invalid "u8 + e.Label + " "u8 + what + " "u8 + e.Where, " ");
}

} partial class jsonwire_package {

public static @string TruncatePointer(@string s, nint n) {
    if (len(s) <= n) {
        return s;
    }
    nint i = n / 2;
    nint j = len(s) - n / 2;

    {
        nint k = strings.LastIndexByte(s.slice(0, i), (byte)'/'); if (k > 0) {
            i = k;
        }
    }
    {
        nint k = strings.IndexByte(s.slice(j), (byte)'/'); if (k >= 0) {
            j += k + len("/");
        }
    }

    for (;;) {
        var (ᴛ1, ᴛ2) = utf8.DecodeLastRuneInString(s.slice(0, i));
        if (!(i > 0 && isInvalidUTF8(ᴛ1, ᴛ2))) break;
        i--;
    }
    for (;;) {
        var (ᴛ3, ᴛ4) = utf8.DecodeRuneInString(s.slice(j));
        if (!(j < len(s) && isInvalidUTF8(ᴛ3, ᴛ4))) break;
        j++;
    }

    @string middle = default;
    switch (strings.Count(s.slice(i, j), "/")) {
    case 0: {
        middle = "…";
        break;
    }
    case 1: {
        middle = "…/…";
        break;
    }
    default: {
        middle = "…/…/…";
        break;
    }}

    if (strings.HasPrefix(s.slice(i, j), "/") && middle != "…"u8) {
        middle = strings.TrimPrefix(middle, "…");
    }
    if (strings.HasSuffix(s.slice(i, j), "/") && middle != "…"u8) {
        middle = strings.TrimSuffix(middle, "…");
    }
    return s.slice(0, i) + middle + s.slice(j);
}

internal static bool isInvalidUTF8(rune r, nint rn) {
    return r == utf8.RuneError && rn == 1;
}

} // end jsonwire_package
