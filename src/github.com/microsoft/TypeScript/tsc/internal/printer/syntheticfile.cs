namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using sourcemap = go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package;

partial class printer_package {

public static (@string text, ж<ast.Node> positioned) PrintAndPositionNode(ж<ast.NodeFactory> factory, ж<ast.Node> node, ж<ast.SourceFile> sourceFile, @string newLine, nint indentSize, ж<EmitContext> emitContext) {
    @string text = default;
    ж<ast.Node> positioned = default;

    var writer = NewChangeTrackerWriter(newLine, indentSize);
    NewPrinter(
        new PrinterOptions(
            NewLine: core.GetNewLineKind(newLine),
            NeverAsciiEscape: true,
            PreserveSourceNewlines: true,
            TerminateUnterminatedLiterals: true
        ),
        writer.GetPrintHandlers(),
        emitContext).Write(node, sourceFile, new ChangeTrackerWriterжEmitTextWriter(writer), nil);

    text = writer.String();
    text = strings.TrimSuffix(text, newLine);
    positioned = writer.AssignPositionsToNode(node, factory);
    return (text, positioned);
}

public static ж<ast.SourceFile> CreateSyntheticSourceFile(ж<ast.NodeFactory> Ꮡfactory, ж<ast.Node> Ꮡnode, @string text, ast.SourceFileParseOptions parseOptions) {
    ref var factory = ref Ꮡfactory.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var eof = factory.NewToken(ast.KindEndOfFile);
    eof.Loc = core.NewTextRange(len(text), len(text));
    var statements = factory.NewNodeList([Ꮡnode]);
    statements.Loc = core.NewTextRange(node.Pos(), node.End());
    var syntheticFile = factory.NewSourceFile(
        parseOptions,
        text,
        statements,
        eof);
    syntheticFile.Loc = core.NewTextRange(0, len(text));
    ast.SetParentInChildren(syntheticFile);
    return syntheticFile.AsSourceFile();
}

} // end printer_package
