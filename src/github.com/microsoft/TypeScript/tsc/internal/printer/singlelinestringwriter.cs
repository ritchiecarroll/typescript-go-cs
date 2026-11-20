namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using sync = sync_package;
using utf8 = unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using unicode;
using static printer_package;

partial class printer_package {

internal static readonly ж<sync.Pool> ᏑsingleLineStringWriterPool = new StaticBox<sync.Pool>(new sync.Pool(
    New: () => Ꮡ(new singleLineStringWriter())
));
internal static ref sync.Pool singleLineStringWriterPool => ref ((StaticBox<sync.Pool>)ᏑsingleLineStringWriterPool).ValueSlot;

internal static EmitTextWriter _ᴛ1ʗ = new singleLineStringWriterжEmitTextWriter(Ꮡ(new singleLineStringWriter()));

public static (EmitTextWriter, Action) GetSingleLineStringWriter() {
    var w = ᏑsingleLineStringWriterPool.Get()._<ж<singleLineStringWriter>>();
    w.Clear();
    var wʗ1 = w;
    return (new singleLineStringWriterжEmitTextWriter(w), () => {
        ᏑsingleLineStringWriterPool.Put(wʗ1.OrTypedNil());
    });
}

partial struct singleLineStringWriter {
    internal strings.Builder builder;
    internal @string lastWritten;
}

} partial class printerᴛsingleLineStringWriter {

internal static void Clear(this ref singleLineStringWriter w) {
    w.lastWritten = "";
    w.builder.Reset();
}

internal static void DecreaseIndent(this singleLineStringWriter w) {
}

internal static core.UTF16Offset GetColumn(this singleLineStringWriter w) {
    return 0;
}

internal static nint GetIndent(this singleLineStringWriter w) {
    return 0;
}

internal static nint GetLine(this singleLineStringWriter w) {
    return 0;
}

internal static @string String(this singleLineStringWriter w) {
    return w.builder.String();
}

internal static nint GetTextPos(this singleLineStringWriter w) {
    return w.builder.Len();
}

internal static bool HasTrailingComment(this singleLineStringWriter w) {
    return false;
}

internal static bool HasTrailingWhitespace(this singleLineStringWriter w) {
    if (w.builder.Len() == 0) {
        return false;
    }
    var (ch, _) = utf8.DecodeLastRuneInString(w.lastWritten);
    if (ch == utf8.RuneError) {
        return false;
    }
    return stringutil.IsWhiteSpaceLike(ch);
}

internal static void IncreaseIndent(this singleLineStringWriter w) {
}

internal static bool IsAtStartOfLine(this singleLineStringWriter w) {
    return false;
}

internal static void RawWrite(this ж<singleLineStringWriter> Ꮡw, @string s) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = s;
    Ꮡw.Ꮡbuilder.WriteString(s);
}

internal static void Write(this ж<singleLineStringWriter> Ꮡw, @string s) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = s;
    Ꮡw.Ꮡbuilder.WriteString(s);
}

internal static void WriteComment(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteKeyword(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteLine(this ж<singleLineStringWriter> Ꮡw) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = " ";
    Ꮡw.Ꮡbuilder.WriteString(" ");
}

internal static void WriteLineForce(this ж<singleLineStringWriter> Ꮡw, bool force) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = " ";
    Ꮡw.Ꮡbuilder.WriteString(" ");
}

internal static void WriteLiteral(this ж<singleLineStringWriter> Ꮡw, @string s) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = s;
    Ꮡw.Ꮡbuilder.WriteString(s);
}

internal static void WriteOperator(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteParameter(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteProperty(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WritePunctuation(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteSpace(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteStringLiteral(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteSymbol(this ж<singleLineStringWriter> Ꮡw, @string text, ж<ast.Symbol> symbol) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

internal static void WriteTrailingSemicolon(this ж<singleLineStringWriter> Ꮡw, @string text) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.lastWritten = text;
    Ꮡw.Ꮡbuilder.WriteString(text);
}

} // end printerᴛsingleLineStringWriter
