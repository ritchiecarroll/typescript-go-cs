namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using errors = errors_package;
using math = math_package;
using big = go.math.big_package;
using strconv = strconv_package;
using strings = strings_package;
using unicode = unicode_package;
using utf8 = go.unicode.utf8_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.math;
using go.unicode;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static jsnum_package;

partial class jsnum_package {

} partial class jsnumᴛNumber {

public static @string String(this Number n) {
    switch (ᐧ) {
    case {} when n.IsNaN(): {
        return "NaN";
    }
    case {} when n.IsInf(): {
        if (n < 0D) {
            return "-Infinity";
        }
        return "Infinity";
    }}

    if (MinSafeInteger <= n && n <= MaxSafeInteger) {
        var i = (int64)n; if ((float64)i == (float64)n) {
            return strconv.FormatInt(i, 10);
        }
    }

    var (b, _) = json.Marshal((float64)n);
    return (@string)b;
}

} partial class jsnum_package {

public static Number FromString(@string s) {
    s = strings.TrimFunc(s, isStrWhiteSpace);

    switch (ᐧ) {
    case {} when s == ""u8: {
        return 0D;
    }
    case {} when s == "Infinity"u8 || s == "+Infinity"u8: {
        return Inf(1);
    }
    case {} when s == "-Infinity"u8: {
        return Inf(-1);
    }}

    foreach (var (_, r) in s) {
        if (!isNumberRune(r)) {
            return NaN();
        }
    }

    var (n, ok) = tryParseInt(s); if (ok) {
        return n;
    }

    (s, var negative) = strings.CutPrefix(s, "-");

    if (!negative) {
        (s, _) = strings.CutPrefix(s, "+");
    }

    var (first, _) = utf8.DecodeRuneInString(s); if (!stringutil.IsDigit(first) && first != '.') {
        return NaN();
    }

    var f = parseFloatString(s);
    if (math.IsNaN(f)) {
        return NaN();
    }

    var sign = 1.0D;
    if (negative) {
        sign = -1.0D;
    }
    return (Number)math.Copysign(f, sign);
}

internal static bool isStrWhiteSpace(rune r) {
    switch (r) {
    case '\n' or '\r' or 0x2028 or 0x2029: {
        return true;
    }
    case '\t' or '\v' or '\f' or 0xFEFF: {
        return true;
    }}

    return unicode.Is(unicode.Zs, r);
}

internal static error errUnknownPrefix = errors.New("unknown number prefix");

internal static (Number, bool) tryParseInt(@string s) {
    int64 i = default;
    error err = default;
    bool hasIntResult = default;

    if (len(s) > 2) {
        @string prefix = s[..2];
        @string rest = s[2..];
        switch (ᐧ) {
        case {} when prefix == "0b"u8 || prefix == "0B"u8: {
            if (!isAllBinaryDigits(rest)) {
                return (NaN(), true);
            }
            (i, err) = strconv.ParseInt(rest, 2, 64);
            hasIntResult = true;
            break;
        }
        case {} when prefix == "0o"u8 || prefix == "0O"u8: {
            if (!isAllOctalDigits(rest)) {
                return (NaN(), true);
            }
            (i, err) = strconv.ParseInt(rest, 8, 64);
            hasIntResult = true;
            break;
        }
        case {} when prefix == "0x"u8 || prefix == "0X"u8: {
            if (!isAllHexDigits(rest)) {
                return (NaN(), true);
            }
            (i, err) = strconv.ParseInt(rest, 16, 64);
            hasIntResult = true;
            break;
        }}

    }

    if (!hasIntResult) {
        s = trimLeadingZeros(s);
        if (!isAllDigits(s)) {
            return (0D, false);
        }
        (i, err) = strconv.ParseInt(s, 10, 64);
        hasIntResult = true;
    }

    if (hasIntResult && err == nil) {
        return ((Number)(float64)i, true);
    }

    var (bi, ok) = @new<big.Int>().SetString(s, 0);
    if (!ok) {
        return (NaN(), true);
    }

    var (f, _) = bi.Float64();
    return ((Number)f, true);
}

internal static float64 parseFloatString(@string s) {
    bool hasDot = default;
    bool hasExp = default;

    @string a = default;
    @string b = default;
    @string c = default;
    @string rest = default;

    (a, rest, hasDot) = strings.Cut(s, ".");
    if (hasDot) {
        (b, c, hasExp) = cutAny(rest, "eE");
    } else {
        (a, c, hasExp) = cutAny(s, "eE");
    }

    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    Ꮡsb.Grow(len(a) + len(b) + len(c) + 3);

    if (a == ""u8) {
        if (hasDot && b == ""u8) {
            return math.NaN();
        }
        if (hasExp && c == ""u8) {
            return math.NaN();
        }
        Ꮡsb.WriteString("0");
    } else {
        a = trimLeadingZeros(a);
        if (!isAllDigits(a)) {
            return math.NaN();
        }
        Ꮡsb.WriteString(a);
    }

    if (hasDot) {
        Ꮡsb.WriteString(".");
        if (b == ""u8) {
            Ꮡsb.WriteString("0");
        } else {
            b = trimTrailingZeros(b);
            if (!isAllDigits(b)) {
                return math.NaN();
            }
            Ꮡsb.WriteString(b);
        }
    }

    if (hasExp) {
        Ꮡsb.WriteString("e");

        var (cΔ1, negative) = strings.CutPrefix(c, "-");
        if (negative) {
            Ꮡsb.WriteString("-");
        } else {
            (cΔ1, _) = strings.CutPrefix(cΔ1, "+");
        }
        cΔ1 = trimLeadingZeros(cΔ1);
        if (!isAllDigits(cΔ1)) {
            return math.NaN();
        }
        Ꮡsb.WriteString(cΔ1);
    }

    return stringToFloat64(sb.String());
}

internal static (@string before, @string after, bool found) cutAny(@string s, @string cutset) {
    @string before = default;
    @string after = default;

    nint i = strings.IndexAny(s, cutset); if (i >= 0) {
        before = s.slice(0, i);
        @string afterAndFound = s.slice(i);
        var (_, size) = utf8.DecodeRuneInString(afterAndFound);
        after = afterAndFound.slice(size);
        return (before, after, true);
    }
    return (s, "", false);
}

internal static @string trimLeadingZeros(@string s) {
    if (strings.HasPrefix(s, "0")) {
        s = strings.TrimLeft(s, "0");
        if (s == ""u8) {
            return "0";
        }
    }
    return s;
}

internal static @string trimTrailingZeros(@string s) {
    if (strings.HasSuffix(s, "0")) {
        s = strings.TrimRight(s, "0");
        if (s == ""u8) {
            return "0";
        }
    }
    return s;
}

internal static float64 stringToFloat64(@string s) {
    var (f, err) = strconv.ParseFloat(s, 64); if (err == nil) {
        return f;
    } else {
        if (errors.Is(err, strconv.ErrRange)) {
            return f;
        }
    }
    return math.NaN();
}

internal static bool isAllDigits(@string s) {
    foreach (var (_, r) in s) {
        if (!stringutil.IsDigit(r)) {
            return false;
        }
    }
    return true;
}

internal static bool isAllBinaryDigits(@string s) {
    foreach (var (_, r) in s) {
        if (r != '0' && r != '1') {
            return false;
        }
    }
    return true;
}

internal static bool isAllOctalDigits(@string s) {
    foreach (var (_, r) in s) {
        if (!stringutil.IsOctalDigit(r)) {
            return false;
        }
    }
    return true;
}

internal static bool isAllHexDigits(@string s) {
    foreach (var (_, r) in s) {
        if (!stringutil.IsHexDigit(r)) {
            return false;
        }
    }
    return true;
}

internal static bool isNumberRune(rune r) {
    if (stringutil.IsDigit(r)) {
        return true;
    }

    if ('a' <= r && r <= 'f') {
        return true;
    }

    if ('A' <= r && r <= 'F') {
        return true;
    }

    switch (r) {
    case '.' or '-' or '+' or 'x' or 'X' or 'o' or 'O': {
        return true;
    }}

    return false;
}

} // end jsnum_package
