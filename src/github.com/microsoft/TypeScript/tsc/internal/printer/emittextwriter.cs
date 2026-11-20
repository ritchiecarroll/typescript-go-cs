namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using fmt = fmt_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class printer_package {

partial interface EmitTextWriter :
    fmt.Stringer
{
    void Write(@string s);
    void WriteTrailingSemicolon(@string text);
    void WriteComment(@string text);
    void WriteKeyword(@string text);
    void WriteOperator(@string text);
    void WritePunctuation(@string text);
    void WriteSpace(@string text);
    void WriteStringLiteral(@string text);
    void WriteParameter(@string text);
    void WriteProperty(@string text);
    void WriteSymbol(@string text, ж<ast.Symbol> symbol);
    void WriteLine();
    void WriteLineForce(bool force);
    void IncreaseIndent();
    void DecreaseIndent();
    void Clear();
    void RawWrite(@string s);
    void WriteLiteral(@string s);
    nint GetTextPos();
    nint GetLine();
    core.UTF16Offset GetColumn();
    nint GetIndent();
    bool IsAtStartOfLine();
    bool HasTrailingComment();
    bool HasTrailingWhitespace();
}

} // end printer_package
