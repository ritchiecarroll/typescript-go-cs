namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static sourcemap_package;

partial class sourcemap_package {

partial struct ECMALineInfo {
    internal @string text;
    internal core.ECMALineStarts lineStarts;
}

public static ж<ECMALineInfo> CreateECMALineInfo(@string text, core.ECMALineStarts lineStarts) {
    return Ꮡ(new ECMALineInfo(
        text: text,
        lineStarts: lineStarts
    ));
}

} partial class sourcemapᴛECMALineInfo {

public static nint LineCount(this ref ECMALineInfo li) {
    return len(li.lineStarts);
}

public static @string LineText(this ref ECMALineInfo li, nint line) {
    var pos = li.lineStarts[line];
    core.TextPos end = default;
    if (line + 1 < len(li.lineStarts)) {
        end = li.lineStarts[line + 1];
    } else {
        end = (core.TextPos)(int32)len(li.text);
    }
    return li.text.slice(pos, end);
}

} // end sourcemapᴛECMALineInfo
