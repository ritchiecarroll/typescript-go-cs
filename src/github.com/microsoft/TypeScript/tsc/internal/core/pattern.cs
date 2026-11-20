namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using static core_package;

partial class core_package {

partial struct Pattern {
    public @string Text;
    public nint StarIndex;
}

public static Pattern TryParsePattern(@string pattern) {
    nint starIndex = strings.Index(pattern, "*");
    if (starIndex == -1 || !strings.Contains(pattern.slice(starIndex + 1), "*")) {
        return new Pattern(Text: pattern, StarIndex: starIndex);
    }
    return new Pattern();
}

} partial class coreᴛPattern {

public static bool IsValid(this ref Pattern p) {
    return p.StarIndex == -1 || p.StarIndex < len(p.Text);
}

public static bool Matches(this ref Pattern p, @string candidate) {
    if (p.StarIndex == -1) {
        return p.Text == candidate;
    }
    return len(candidate) >= len(p.Text) - 1 && strings.HasPrefix(candidate, p.Text.slice(0, p.StarIndex)) && strings.HasSuffix(candidate, p.Text.slice(p.StarIndex + 1));
}

public static @string MatchedText(this ref Pattern p, @string candidate) {
    if (!p.Matches(candidate)) {
        throw panic("candidate does not match pattern");
    }
    if (p.StarIndex == -1) {
        return "";
    }
    return candidate.slice(p.StarIndex, len(candidate) - len(p.Text) + p.StarIndex + 1);
}

} partial class core_package {

public static T FindBestPatternMatch<T>(slice<T> values, Func<T, Pattern> getPattern, @string candidate) {
    T bestPattern = GoZero<T>();
    nint longestMatchPrefixLength = -1;
    foreach (var (_, value) in range(values)) {
        var pattern = getPattern(value);
        if ((pattern.StarIndex == -1 || pattern.StarIndex > longestMatchPrefixLength) && pattern.Matches(candidate)) {
            bestPattern = value;
            longestMatchPrefixLength = pattern.StarIndex;
        }
    }
    return bestPattern;
}

} // end core_package
