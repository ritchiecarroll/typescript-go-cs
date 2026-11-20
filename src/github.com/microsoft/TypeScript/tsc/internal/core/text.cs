namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using static core_package;

partial class core_package {

partial struct TextPos /*num:int32*/;

partial struct TextRange {
    internal TextPos pos;
    internal TextPos end;
}

public static TextRange NewTextRange(nint pos, nint end) {
    return new TextRange(pos: ((TextPos)(int32)pos), end: ((TextPos)(int32)end));
}

public static TextRange UndefinedTextRange() {
    return new TextRange(pos: ((TextPos)(-1)), end: ((TextPos)(-1)));
}

} partial class coreᴛTextRange {

public static nint Pos(this TextRange t) {
    return (nint)t.pos;
}

public static nint End(this TextRange t) {
    return (nint)t.end;
}

public static nint Len(this TextRange t) {
    return (nint)(t.end - t.pos);
}

public static bool IsValid(this TextRange t) {
    return t.pos >= 0 || t.end >= 0;
}

public static bool Contains(this TextRange t, nint pos) {
    return pos >= (nint)t.pos && pos < (nint)t.end;
}

public static bool ContainsInclusive(this TextRange t, nint pos) {
    return pos >= (nint)t.pos && pos <= (nint)t.end;
}

public static bool ContainsExclusive(this TextRange t, nint pos) {
    return (nint)t.pos < pos && pos < (nint)t.end;
}

public static TextRange WithPos(this TextRange t, nint pos) {
    return new TextRange(pos: ((TextPos)(int32)pos), end: t.end);
}

public static TextRange WithEnd(this TextRange t, nint end) {
    return new TextRange(pos: t.pos, end: ((TextPos)(int32)end));
}

public static bool ContainedBy(this TextRange t, TextRange t2) {
    return t2.pos <= t.pos && t2.end >= t.end;
}

public static bool Overlaps(this TextRange t, TextRange t2) {
    var start = max(t.pos, t2.pos);
    var end = min(t.end, t2.end);
    return start < end;
}

public static bool Intersects(this TextRange t, TextRange t2) {
    var start = max(t.pos, t2.pos);
    var end = min(t.end, t2.end);
    return start <= end;
}

} partial class core_package {

public static nint CompareTextRanges(TextRange r1, TextRange r2) {
    nint c = (nint)r1.pos - (nint)r2.pos;
    if (c != 0) {
        return c;
    }
    return (nint)r1.end - (nint)r2.end;
}

} // end core_package
