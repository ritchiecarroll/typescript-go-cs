namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using regexp = regexp_package;
using strings = strings_package;
using unicode = unicode_package;
using utf16 = go.unicode.utf16_package;
using utf8 = go.unicode.utf8_package;
using go.unicode;

partial class stringutil_package {

public static bool IsWhiteSpaceLike(rune ch) {
    return IsWhiteSpaceSingleLine(ch) || IsLineBreak(ch);
}

public static bool IsWhiteSpaceSingleLine(rune ch) {
    switch (ch) {
    case ' ' or '\t' or '\v' or '\f' or 0x0085 or 0x00A0 or 0x1680 or 0x2000 or 0x2001 or 0x2002 or 0x2003 or 0x2004 or 0x2005 or 0x2006 or 0x2007 or 0x2008 or 0x2009 or 0x200A or 0x200B or 0x202F or 0x205F or 0x3000 or 0xFEFF: {
        return true;
    }}

    return false;
}

public static bool IsLineBreak(rune ch) {
    switch (ch) {
    case '\n' or '\r' or 0x2028 or 0x2029: {
        return true;
    }}

    return false;
}

public static bool IsDigit(rune ch) {
    return ch >= '0' && ch <= '9';
}

public static bool IsOctalDigit(rune ch) {
    return ch >= '0' && ch <= '7';
}

public static bool IsHexDigit(rune ch) {
    return ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f';
}

public static bool IsASCIILetter(rune ch) {
    return ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z';
}

public static slice<@string> SplitLines(@string text) {
    var lines = new slice<@string>(0, strings.Count(text, "\n") + 1);
    nint start = 0;
    nint pos = 0;
    while (pos < len(text)) {
        var exprᴛ1 = text[pos];
        var matchᴛ1 = false;
        if (exprᴛ1 is (byte)'\r') {
            if (pos + 1 < len(text) && text[pos + 1] == '\n') {
                lines = append(lines, text.slice(start, pos));
                pos += 2;
                start = pos;
                continue;
            }
            fallthrough = true;
        }
        if (fallthrough || !matchᴛ1 && exprᴛ1 is (byte)'\n') { matchᴛ1 = true;
            lines = append(lines, text.slice(start, pos));
            pos++;
            start = pos;
            continue;
        }

        pos++;
    }
    if (start < len(text)) {
        lines = append(lines, text.slice(start));
    }
    return lines;
}

public static nint GuessIndentation(slice<@string> lines) {
    const nint MAX_SMI_X86 = 0x3fff_ffff;
    nint indentation = MAX_SMI_X86;
    foreach (var (_, line) in range(lines)) {
        if (len(line) == 0) {
            continue;
        }
        nint i = 0;
        while (i < len(line) && i < indentation) {
            var (ch, size) = utf8.DecodeRuneInString(line.slice(i));
            if (!IsWhiteSpaceLike(ch)) {
                break;
            }
            i += size;
        }
        if (i < indentation) {
            indentation = i;
        }
        if (indentation == 0) {
            return 0;
        }
    }
    if (indentation == MAX_SMI_X86) {
        return 0;
    }
    return indentation;
}

public static @string EncodeURI(@string s) {
    ref var builder = ref heap(new strings.Builder(), out var Ꮡbuilder);
    foreach (var i in range(len(s))) {
        var b = s[i];
        if (!shouldEscapeForEncodeURI(b)) {
            Ꮡbuilder.WriteByte(b);
            continue;
        }

        foreach (var (_, escaped) in range(slice<byte>(s.slice(i, i + 1)))) {
            Ꮡbuilder.WriteByte((byte)'%');
            Ꮡbuilder.WriteByte(upperhex[(escaped >> 4)]);
            Ꮡbuilder.WriteByte(upperhex[(byte)(escaped & 0x0f)]);
        }
    }
    return builder.String();
}

internal static readonly @string upperhex = "0123456789ABCDEF"u8;

internal static bool shouldEscapeForEncodeURI(byte b) {
    switch (ᐧ) {
    case {} when b >= (byte)'A' && b <= (byte)'Z': {
        return false;
    }
    case {} when b >= (byte)'a' && b <= (byte)'z': {
        return false;
    }
    case {} when b >= (byte)'0' && b <= (byte)'9': {
        return false;
    }}

    switch (b) {
    case (byte)';' or (byte)'/' or (byte)'?' or (byte)':' or (byte)'@' or (byte)'&' or (byte)'=' or (byte)'+' or (byte)'$' or (byte)',' or (byte)'#' or (byte)'-' or (byte)'_' or (byte)'.' or (byte)'!' or (byte)'~' or (byte)'*' or (byte)'\'' or (byte)'(' or (byte)')': {
        return false;
    }
    default: {
        return true;
    }}

}

internal static nint getByteOrderMarkLength(@string text) {
    if (len(text) >= 1) {
        var ch0 = text[0];
        if (ch0 == 0xfe) {
            if (len(text) >= 2 && text[1] == 0xff) {
                return 2;
            }
            return 0;
        }
        if (ch0 == 0xff) {
            if (len(text) >= 2 && text[1] == 0xfe) {
                return 2;
            }
            return 0;
        }
        if (ch0 == 0xef) {
            if (len(text) >= 3 && text[1] == 0xbb && text[2] == 0xbf) {
                return 3;
            }
            return 0;
        }
    }
    return 0;
}

public static @string RemoveByteOrderMark(@string text) {
    nint length = getByteOrderMarkLength(text);
    if (length > 0) {
        return text.slice(length);
    }
    return text;
}

public static @string AddUTF8ByteOrderMark(@string text) {
    if (getByteOrderMarkLength(text) == 0) {
        return ((@string)(new byte[]{0xef, 0xbb, 0xbf})) + text;
    }
    return text;
}

public static @string StripQuotes(@string name) {
    if (len(name) < 2) {
        return name;
    }
    var (firstChar, _) = utf8.DecodeRuneInString(name);
    var (lastChar, _) = utf8.DecodeLastRuneInString(name);
    if (firstChar == lastChar && (firstChar == '\'' || firstChar == '"' || firstChar == '`')) {
        return name.slice(1, len(name) - 1);
    }
    return name;
}

internal static ж<regexp.Regexp> matchSlashSomething = regexp.MustCompile(@"\\.");

internal static @string matchSlashReplacer(@string @in) {
    return @in[1..];
}

public static @string UnquoteString(@string str) {
    @string inner = StripQuotes(str);
    return matchSlashSomething.ReplaceAllStringFunc(inner, matchSlashReplacer);
}

public static @string LowerFirstChar(@string str) {
    var (@char, size) = utf8.DecodeRuneInString(str);
    if (size > 0) {
        return ((@string)unicode.ToLower(@char)) + str.slice(size);
    }
    return str;
}

public static @string TruncateByRunes(@string str, nint maxLength) {
    if (len(str) < maxLength) {
        return str;
    }
    if (maxLength <= 0) {
        return "";
    }
    nint runeCount = default;
    foreach (var (i, _) in str) {
        runeCount++;
        if (runeCount > maxLength) {
            return str.slice(0, i);
        }
    }
    return str;
}

public static UntypedInt SurrogateLowStart => 0xDC00;

public static bool IsHighSurrogate(rune ch) {
    return utf16.IsSurrogate(ch) && ch < SurrogateLowStart;
}

public static bool IsLowSurrogate(rune ch) {
    return utf16.IsSurrogate(ch) && ch >= SurrogateLowStart;
}

public static bool IsSurrogate(rune ch) {
    return utf16.IsSurrogate(ch);
}

public static rune SurrogatePairToCodePoint(rune high, rune low) {
    return utf16.DecodeRune(high, low);
}

public static (rune high, rune low) CodePointToSurrogatePair(rune ch) {
    return utf16.EncodeRune(ch);
}

internal static UntypedInt surrogateUTF8Lead => 0xED;
internal static UntypedInt surrogateUTF8LeadBits => 0xD000;
internal static UntypedInt utf8ContMarker => 0x80;
internal static UntypedInt utf8ContMax => 0xBF;
internal static UntypedInt utf8ContMask => 0x3F;
internal static UntypedInt surrogateUTF8Byte1Min => 0xA0;
internal static UntypedInt surrogateUTF8Byte1Max => 0xBF;

public static @string EncodeJSStringRune(rune ch) {
    if (IsSurrogate(ch)) {
        return (@string)slice<byte>([
            surrogateUTF8Lead,
            (byte)((rune)utf8ContMarker | ((ch >> 6) & (rune)utf8ContMask)),
            (byte)((rune)utf8ContMarker | (ch & (rune)utf8ContMask))
        ]);
    }
    return (@string)ch;
}

public static (rune, nint) DecodeJSStringRune(@string s) {
    if (len(s) >= 3 && s[0] == surrogateUTF8Lead && s[1] >= surrogateUTF8Byte1Min && s[1] <= surrogateUTF8Byte1Max && s[2] >= utf8ContMarker && s[2] <= utf8ContMax) {
        return ((rune)surrogateUTF8LeadBits | (rune)((byte)(s[1] & (byte)utf8ContMask)) << 6 | (rune)((byte)(s[2] & (byte)utf8ContMask)), 3);
    }
    return utf8.DecodeRuneInString(s);
}

public static @string CombineSurrogatePairs(@string s) {
    if (strings.IndexByte(s, surrogateUTF8Lead) < 0) {
        return s;
    }
    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    Ꮡb.Grow(len(s));
    for (nint i = 0; i < len(s); ) {
        var (r, size) = DecodeJSStringRune(s.slice(i));
        if (IsHighSurrogate(r)) {
            var (low, lowSize) = DecodeJSStringRune(s.slice(i + size)); if (IsLowSurrogate(low)) {
                Ꮡb.WriteRune(SurrogatePairToCodePoint(r, low));
                i += size + lowSize;
                continue;
            }
        }
        Ꮡb.WriteString(s.slice(i, i + size));
        i += size;
    }
    return b.String();
}

} // end stringutil_package
