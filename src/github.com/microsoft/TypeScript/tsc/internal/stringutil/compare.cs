global using Comparison = nint;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using unicode = unicode_package;
using utf8 = go.unicode.utf8_package;
using go.unicode;

partial class stringutil_package {

public static bool EquateStringCaseInsensitive(@string a, @string b) {
    return strings.EqualFold(a, b);
}

public static bool EquateStringCaseSensitive(@string a, @string b) {
    return a == b;
}

public static Func<@string, @string, bool> GetStringEqualityComparer(bool ignoreCase) {
    if (ignoreCase) {
        return EquateStringCaseInsensitive;
    }
    return EquateStringCaseSensitive;
}

public const Comparison ComparisonLessThan = -1;
public const Comparison ComparisonEqual = 0;
public const Comparison ComparisonGreaterThan = 1;

public static Comparison CompareStringsCaseInsensitive(@string a, @string b) {
    if (a == b) {
        return ComparisonEqual;
    }
    for (;;) {
        var (ca, sa) = utf8.DecodeRuneInString(a);
        var (cb, sb) = utf8.DecodeRuneInString(b);
        if (sa == 0) {
            if (sb == 0) {
                return ComparisonEqual;
            }
            return ComparisonLessThan;
        }
        if (sb == 0) {
            return ComparisonGreaterThan;
        }
        var lca = unicode.ToLower(ca);
        var lcb = unicode.ToLower(cb);
        if (lca != lcb) {
            if (lca < lcb) {
                return ComparisonLessThan;
            }
            return ComparisonGreaterThan;
        }
        a = a.slice(sa);
        b = b.slice(sb);
    }
}

public static Comparison CompareStringsCaseSensitive(@string a, @string b) {
    return strings.Compare(a, b);
}

public static Func<@string, @string, Comparison> GetStringComparer(bool ignoreCase) {
    if (ignoreCase) {
        return CompareStringsCaseInsensitive;
    }
    return CompareStringsCaseSensitive;
}

public static bool HasPrefix(@string s, @string prefix, bool caseSensitive) {
    if (caseSensitive) {
        return strings.HasPrefix(s, prefix);
    }
    if (len(prefix) > len(s)) {
        return false;
    }
    return strings.EqualFold(s.slice(0, len(prefix)), prefix);
}

public static bool HasSuffix(@string s, @string suffix, bool caseSensitive) {
    if (caseSensitive) {
        return strings.HasSuffix(s, suffix);
    }
    if (len(suffix) > len(s)) {
        return false;
    }
    return strings.EqualFold(s.slice(len(s) - len(suffix)), suffix);
}

public static bool HasPrefixAndSuffixWithoutOverlap(@string s, @string prefix, @string suffix, bool caseSensitive) {
    if (len(prefix) + len(suffix) > len(s)) {
        return false;
    }

    return HasPrefix(s, prefix, caseSensitive) && HasSuffix(s, suffix, caseSensitive);
}

public static Comparison CompareStringsCaseInsensitiveThenSensitive(@string a, @string b) {
    Comparison cmp = CompareStringsCaseInsensitive(a, b);
    if (cmp != ComparisonEqual) {
        return cmp;
    }
    return CompareStringsCaseSensitive(a, b);
}

public static Comparison CompareStringsCaseInsensitiveEslintCompatible(@string a, @string b) {
    if (a == b) {
        return ComparisonEqual;
    }
    a = strings.ToLower(a);
    b = strings.ToLower(b);
    return strings.Compare(a, b);
}

} // end stringutil_package
