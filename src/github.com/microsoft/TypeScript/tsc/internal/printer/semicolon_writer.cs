namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static printer_package;

partial class printer_package {

partial struct trailingSemicolonDeferringWriter {
    internal EmitTextWriter inner;
    internal bool hasPendingSemicolon;
}

internal static EmitTextWriter getTrailingSemicolonDeferringWriter(EmitTextWriter writer) {
    return new trailingSemicolonDeferringWriterжEmitTextWriter(Ꮡ(new trailingSemicolonDeferringWriter(inner: writer)));
}

} partial class printerᴛtrailingSemicolonDeferringWriter {

internal static void commitSemicolon(this ref trailingSemicolonDeferringWriter w) {
    if (w.hasPendingSemicolon) {
        w.inner.WriteTrailingSemicolon(";");
        w.hasPendingSemicolon = false;
    }
}

internal static void Write(this ref trailingSemicolonDeferringWriter w, @string s) {
    w.commitSemicolon();
    w.inner.Write(s);
}

internal static void WriteTrailingSemicolon(this ref trailingSemicolonDeferringWriter w, @string _) {
    w.hasPendingSemicolon = true;
}

internal static void WriteComment(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WriteComment(text);
}

internal static void WriteKeyword(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WriteKeyword(text);
}

internal static void WriteOperator(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WriteOperator(text);
}

internal static void WritePunctuation(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WritePunctuation(text);
}

internal static void WriteSpace(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WriteSpace(text);
}

internal static void WriteStringLiteral(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WriteStringLiteral(text);
}

internal static void WriteParameter(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WriteParameter(text);
}

internal static void WriteProperty(this ref trailingSemicolonDeferringWriter w, @string text) {
    w.commitSemicolon();
    w.inner.WriteProperty(text);
}

internal static void WriteSymbol(this ref trailingSemicolonDeferringWriter w, @string text, ж<ast.Symbol> symbol) {
    w.commitSemicolon();
    w.inner.WriteSymbol(text, symbol);
}

internal static void WriteLine(this ref trailingSemicolonDeferringWriter w) {
    w.commitSemicolon();
    w.inner.WriteLine();
}

internal static void WriteLineForce(this ref trailingSemicolonDeferringWriter w, bool force) {
    w.commitSemicolon();
    w.inner.WriteLineForce(force);
}

internal static void IncreaseIndent(this ref trailingSemicolonDeferringWriter w) {
    w.commitSemicolon();
    w.inner.IncreaseIndent();
}

internal static void DecreaseIndent(this ref trailingSemicolonDeferringWriter w) {
    w.commitSemicolon();
    w.inner.DecreaseIndent();
}

internal static void Clear(this ref trailingSemicolonDeferringWriter w) {
    w.hasPendingSemicolon = false;
    w.inner.Clear();
}

internal static @string String(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.String();
}

internal static void RawWrite(this ref trailingSemicolonDeferringWriter w, @string s) {
    w.commitSemicolon();
    w.inner.RawWrite(s);
}

internal static void WriteLiteral(this ref trailingSemicolonDeferringWriter w, @string s) {
    w.commitSemicolon();
    w.inner.WriteLiteral(s);
}

internal static nint GetTextPos(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.GetTextPos();
}

internal static nint GetLine(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.GetLine();
}

internal static core.UTF16Offset GetColumn(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.GetColumn();
}

internal static nint GetIndent(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.GetIndent();
}

internal static bool IsAtStartOfLine(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.IsAtStartOfLine();
}

internal static bool HasTrailingComment(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.HasTrailingComment();
}

internal static bool HasTrailingWhitespace(this ref trailingSemicolonDeferringWriter w) {
    return w.inner.HasTrailingWhitespace();
}

} // end printerᴛtrailingSemicolonDeferringWriter
