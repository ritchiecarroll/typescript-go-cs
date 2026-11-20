namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using utf8 = unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using unicode;
using static printer_package;

partial class printer_package {

internal static EmitTextWriter _ᴛ2ʗ = new textWriterжEmitTextWriter(Ꮡ(new textWriter()));

partial struct textWriter {
    internal @string newLine;
    internal nint indentSize;
    internal strings.Builder builder;
    internal @string lastWritten;
    internal nint indent;
    internal bool lineStart;
    internal nint lineCount;
    internal nint linePos;
    internal bool hasTrailingCommentState;
}

} partial class printerᴛtextWriter {

internal static void Clear(this ref textWriter w) {
    w = new textWriter(newLine: w.newLine, indentSize: w.indentSize, lineStart: true);
}

internal static void Grow(this ж<textWriter> Ꮡw, nint n) {
    Ꮡw.Ꮡbuilder.Grow(n);
}

internal static void DecreaseIndent(this ref textWriter w) {
    w.indent--;
}

internal static core.UTF16Offset GetColumn(this ref textWriter w) {
    if (w.lineStart) {
        return (core.UTF16Offset)(w.indent * w.indentSize);
    }
    return core.UTF16Len(w.builder.String().slice(w.linePos));
}

internal static nint GetIndent(this ref textWriter w) {
    return w.indent;
}

internal static nint GetLine(this ref textWriter w) {
    return w.lineCount;
}

internal static @string String(this ref textWriter w) {
    return w.builder.String();
}

internal static nint GetTextPos(this ref textWriter w) {
    return w.builder.Len();
}

internal static bool HasTrailingComment(this textWriter w) {
    return w.hasTrailingCommentState;
}

internal static bool HasTrailingWhitespace(this ref textWriter w) {
    if (w.builder.Len() == 0) {
        return false;
    }
    var (ch, _) = utf8.DecodeLastRuneInString(w.lastWritten);
    if (ch == utf8.RuneError) {
        return false;
    }
    return stringutil.IsWhiteSpaceLike(ch);
}

internal static void IncreaseIndent(this ref textWriter w) {
    w.indent++;
}

internal static bool IsAtStartOfLine(this ref textWriter w) {
    return w.lineStart;
}

internal static void RawWrite(this ж<textWriter> Ꮡw, @string s) {
    ref var w = ref Ꮡw.DerefOrNull();

    if (s != ""u8) {
        Ꮡw.Ꮡbuilder.WriteString(s);
        w.lastWritten = s;
        w.hasTrailingCommentState = false;
    }
    w.updateLineCountAndPosFor(s);
}

internal static void updateLineCountAndPosFor(this ref textWriter w, @string s) {
    nint count = default;
    core.TextPos lastLineStart = default;

    foreach (var lineStart in range<core.TextPos>(core.ComputeECMALineStartsSeq(s).Invoke)) {
        count++;
        lastLineStart = lineStart;
    }

    if (count > 1) {
        w.lineCount += count - 1;
        nint curLen = w.builder.Len();
        w.linePos = curLen - len(s) + (nint)lastLineStart;
        w.lineStart = (w.linePos - curLen) == 0;
        return;
    }
    w.lineStart = false;
}

} partial class printer_package {

internal static UntypedInt defaultIndentSize => 4;

public static nint GetDefaultIndentSize() {
    return defaultIndentSize;
}

internal static @string getIndentString(nint indent, nint indentSize) {
    if (indent == 0) {
        return "";
    }
    return strings.Repeat(" ", indent * indentSize);
}

} partial class printerᴛtextWriter {

internal static void writeText(this ж<textWriter> Ꮡw, @string s) {
    ref var w = ref Ꮡw.DerefOrNull();

    if (s != ""u8) {
        if (w.lineStart) {
            Ꮡw.Ꮡbuilder.WriteString(getIndentString(w.indent, w.indentSize));
            w.lineStart = false;
        }
        Ꮡw.Ꮡbuilder.WriteString(s);
        w.lastWritten = s;
        w.updateLineCountAndPosFor(s);
    }
}

internal static void Write(this ж<textWriter> Ꮡw, @string s) {
    ref var w = ref Ꮡw.DerefOrNull();

    if (s != ""u8) {
        w.hasTrailingCommentState = false;
    }
    Ꮡw.writeText(s);
}

internal static void WriteComment(this ж<textWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    if (text != ""u8) {
        w.hasTrailingCommentState = true;
    }
    Ꮡw.writeText(text);
}

internal static void WriteKeyword(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

internal static void writeLineRaw(this ж<textWriter> Ꮡw) {
    ref var w = ref Ꮡw.DerefOrNull();

    Ꮡw.Ꮡbuilder.WriteString(w.newLine);
    w.lastWritten = w.newLine;
    w.lineCount++;
    w.linePos = w.builder.Len();
    w.lineStart = true;
    w.hasTrailingCommentState = false;
}

internal static void WriteLine(this ж<textWriter> Ꮡw) {
    ref var w = ref Ꮡw.DerefOrNull();

    if (!w.lineStart) {
        Ꮡw.writeLineRaw();
    }
}

internal static void WriteLineForce(this ж<textWriter> Ꮡw, bool force) {
    ref var w = ref Ꮡw.DerefOrNull();

    if (!w.lineStart || force) {
        Ꮡw.writeLineRaw();
    }
}

internal static void WriteLiteral(this ж<textWriter> Ꮡw, @string s) {
    Ꮡw.Write(s);
}

internal static void WriteOperator(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

internal static void WriteParameter(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

internal static void WriteProperty(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

internal static void WritePunctuation(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

internal static void WriteSpace(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

internal static void WriteStringLiteral(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

internal static void WriteSymbol(this ж<textWriter> Ꮡw, @string text, ж<ast.Symbol> symbol) {
    Ꮡw.Write(text);
}

internal static void WriteTrailingSemicolon(this ж<textWriter> Ꮡw, @string text) {
    Ꮡw.Write(text);
}

} partial class printer_package {

public static EmitTextWriter NewTextWriter(@string newLine, nint indentSize) {
    if (indentSize <= 0) {
        indentSize = 4;
    }
    ref var w = ref heap(new textWriter(), out var Ꮡw);
    w.newLine = newLine;
    w.indentSize = indentSize;
    w.Clear();
    return new textWriterжEmitTextWriter(Ꮡw);
}

} // end printer_package
