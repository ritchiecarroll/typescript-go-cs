namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using static core_package;

partial class core_package {

partial struct TextChange {
    /*embed*/ public TextRange TextRange;
    public @string NewText;
}

} partial class coreᴛTextChange {

public static @string ApplyTo(this TextChange t, @string text) {
    return text.slice(0, t.Pos()) + t.NewText + text.slice(t.End());
}

} partial class core_package {

public static @string ApplyBulkEdits(@string text, slice<TextChange> edits) {
    ref var b = ref heap<strings.Builder>(new strings.Builder(), out var Ꮡb);
    Ꮡb.Grow(len(text));
    nint lastEnd = 0;
    foreach (var (_, e) in range(edits)) {
        nint start = e.TextRange.Pos();
        if (start != lastEnd) {
            Ꮡb.WriteString(text.slice(lastEnd, e.TextRange.Pos()));
        }
        Ꮡb.WriteString(e.NewText);

        lastEnd = e.TextRange.End();
    }
    Ꮡb.WriteString(text.slice(lastEnd));

    return b.String();
}

} // end core_package
