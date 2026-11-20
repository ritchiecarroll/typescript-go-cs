namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using unicode = unicode_package;
using utf8 = go.unicode.utf8_package;
using go.unicode;

partial class stringutil_package {

public static @string ToLowerJS(@string str) {
    {
        var (ascii, ok) = toLowerASCII(str); if (ok) {
            return ascii;
        }
    }

    ref var builder = ref heap(new strings.Builder(), out var Ꮡbuilder);
    Ꮡbuilder.Grow(len(str));
    var casedBefore = false;
    for (nint i = 0; i < len(str); ) {
        var (r, size) = DecodeJSStringRune(str.slice(i));
        i += size;
        if (IsSurrogate(r)) {
            Ꮡbuilder.WriteString(EncodeJSStringRune(r));
        } else if (specialCasingMappings[r, ꟷ] is (var mapping, true)) {
            if (mapping.condition == specialCasingConditionFinalSigma && isFinalSigmaContext(casedBefore, str, i)) {
                Ꮡbuilder.WriteString(mapping.conditionalLower);
            } else {
                Ꮡbuilder.WriteString(mapping.lower);
            }
        } else {
            Ꮡbuilder.WriteRune(r);
        }
        if (!isUnicodeCaseIgnorable(r)) {
            casedBefore = isSigmaCased(r);
        }
    }
    return builder.String();
}

public static @string ToUpperJS(@string str) {
    {
        var (ascii, ok) = toUpperASCII(str); if (ok) {
            return ascii;
        }
    }

    ref var builder = ref heap(new strings.Builder(), out var Ꮡbuilder);
    Ꮡbuilder.Grow(len(str));
    for (nint i = 0; i < len(str); ) {
        var (r, size) = DecodeJSStringRune(str.slice(i));
        if (IsSurrogate(r)) {
            Ꮡbuilder.WriteString(str.slice(i, i + size));
        } else if (specialCasingMappings[r, ꟷ] is (var mapping, true)) {
            Ꮡbuilder.WriteString(mapping.upper);
        } else {
            Ꮡbuilder.WriteRune(r);
        }
        i += size;
    }

    return builder.String();
}

internal static (@string, bool) toLowerASCII(@string str) {
    var needsMapping = false;
    foreach (var i in range(len(str))) {
        var ch = str[i];
        if (ch >= utf8.RuneSelf) {
            return ("", false);
        }
        needsMapping = needsMapping || ('A' <= ch && ch <= 'Z');
    }
    if (!needsMapping) {
        return (str, true);
    }

    var buf = slice<byte>(str);
    foreach (var (i, ch) in range(buf)) {
        if ('A' <= ch && ch <= 'Z') {
            buf[i] = (byte)(ch + ((rune)'a' - (rune)'A'));
        }
    }
    return ((@string)buf, true);
}

internal static (@string, bool) toUpperASCII(@string str) {
    var needsMapping = false;
    foreach (var i in range(len(str))) {
        var ch = str[i];
        if (ch >= utf8.RuneSelf) {
            return ("", false);
        }
        needsMapping = needsMapping || ('a' <= ch && ch <= 'z');
    }
    if (!needsMapping) {
        return (str, true);
    }

    var buf = slice<byte>(str);
    foreach (var (i, ch) in range(buf)) {
        if ('a' <= ch && ch <= 'z') {
            buf[i] = (byte)(ch - ((rune)'a' - (rune)'A'));
        }
    }
    return ((@string)buf, true);
}

internal static bool isFinalSigmaContext(bool casedBefore, @string str, nint afterOffset) {
    return casedBefore && !hasSigmaCasedAfter(str, afterOffset);
}

internal static bool hasSigmaCasedAfter(@string str, nint start) {
    for (nint i = start; i < len(str); ) {
        var (r, size) = DecodeJSStringRune(str.slice(i));
        i += size;
        if (isUnicodeCaseIgnorable(r)) {
            continue;
        }
        return isSigmaCased(r);
    }
    return false;
}

internal static bool isSigmaCased(rune r) {
    return unicode.Is(unicodeCasedRanges, r);
}

internal static bool isUnicodeCaseIgnorable(rune r) {
    return unicode.Is(unicodeCaseIgnorableRanges, r);
}

} // end stringutil_package
