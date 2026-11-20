namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using unicode = unicode_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class sourcemap_package {

public static @string TryGetSourceMappingURL(ж<ECMALineInfo> ᏑlineInfo) {
    ref var lineInfo = ref ᏑlineInfo.DerefOrNull();

    if (ᏑlineInfo != nil) {
        for (nint index = lineInfo.LineCount() - 1; index >= 0; index--) {
            @string line = lineInfo.LineText(index);
            line = strings.TrimLeftFunc(line, unicode.IsSpace);
            line = strings.TrimRightFunc(line, stringutil.IsLineBreak);
            if (len(line) == 0) {
                continue;
            }
            if (len(line) < 4 || !strings.HasPrefix(line, "//") || line[2] != '#' && line[2] != '@' || line[3] != ' ') {
                break;
            }
            var (url, ok) = strings.CutPrefix(line[4..], "sourceMappingURL="); if (ok) {
                return strings.TrimRightFunc(url, unicode.IsSpace);
            }
        }
    }
    return "";
}

} // end sourcemap_package
