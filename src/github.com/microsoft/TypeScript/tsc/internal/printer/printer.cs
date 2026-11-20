namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using slices = slices_package;
using strings = strings_package;
using utf8 = unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using sourcemap = go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using unicode;
using static printer_package;

partial class printer_package {

partial struct PrinterOptions {
    public bool RemoveComments;
    public core.NewLineKind NewLine;
    public bool OmitTrailingSemicolon;
    public bool NoEmitHelpers;
    public core.ScriptTarget Target;
    public bool SourceMap;
    public bool InlineSourceMap;
    public bool InlineSources;
    public bool OmitBraceSourceMapPositions;
    public bool OnlyPrintJSDocStyle;
    public bool NeverAsciiEscape;
    public bool PreserveSourceNewlines;
    public bool TerminateUnterminatedLiterals;
}

partial struct PrintHandlers {
    public Func<@string, bool> HasGlobalName;
    public Func<sourcemap.Source, nint, (sourcemap.Source mappedSource, nint mappedPos, bool ok)> MapSourcePosition;
    public Action<ж<ast.Node>> OnBeforeEmitNode;
    public Action<ж<ast.Node>> OnAfterEmitNode;
    public Action<ж<ast.NodeList>> OnBeforeEmitNodeList;
    public Action<ж<ast.NodeList>> OnAfterEmitNodeList;
    public Action<ж<astꓸTokenNode>> OnBeforeEmitToken;
    public Action<ж<astꓸTokenNode>> OnAfterEmitToken;
}

partial struct Printer {
    /*embed*/ public PrintHandlers PrintHandlers;
    public PrinterOptions Options;
    internal ж<EmitContext> emitContext;
    internal ж<ast.SourceFile> currentSourceFile;
    internal map<@string, ж<astꓸIdentifierNode>> uniqueHelperNames;
    internal ж<astꓸIdentifierNode> externalHelpersModuleName;
    internal nint nextListElementPos;
    internal EmitTextWriter writer;
    internal EmitTextWriter ownWriter;
    internal WriteKind writeKind;
    internal bool sourceMapsDisabled;
    internal ж<sourcemap.Generator> sourceMapGenerator;
    internal sourcemap.Source sourceMapSource;
    internal sourcemap.SourceIndex sourceMapSourceIndex;
    internal bool sourceMapSourceIsJson;
    internal ж<lineCharacterCache> sourceMapLineCharCache;
    internal sourcemap.Source mostRecentSourceMapSource;
    internal sourcemap.SourceIndex mostRecentSourceMapSourceIndex;
    internal nint containerPos;
    internal nint containerEnd;
    internal nint declarationListContainerEnd;
    internal core.Stack<detachedCommentsInfo> detachedCommentsInfo;
    internal bool commentsDisabled;
    internal bool inExtends;
    internal NameGenerator nameGenerator;
    internal Func<@string, @string> makeFileLevelOptimisticUniqueName;
    internal core.Arena<commentState> commentStateArena;
    internal core.Arena<sourceMapState> sourceMapStateArena;
    public map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> IdToSymbol;
}

partial struct detachedCommentsInfo {
    internal nint nodePos;
    internal nint detachedCommentEndPos;
}

partial struct commentState {
    internal EmitFlags emitFlags;
    internal core.TextRange commentRange;
    internal nint containerPos;
    internal nint containerEnd;
    internal nint declarationListContainerEnd;
}

partial struct sourceMapState {
    internal EmitFlags emitFlags;
    internal core.TextRange sourceMapRange;
    internal bool hasTokenSourceMapRange;
}

partial struct printerState {
    internal ж<commentState> commentState;
    internal ж<sourceMapState> sourceMapState;
}

public static ж<Printer> NewPrinter(PrinterOptions options, PrintHandlers handlers, ж<EmitContext> ᏑemitContext) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    var printer = Ꮡ(new Printer(
        PrintHandlers: handlers,
        Options: options,
        emitContext: ᏑemitContext
    ));
    if (printer.emitContext == nil) {
        printer.emitContext = NewEmitContext();
    }
    printer.nameGenerator.Context = printer.emitContext;
    var printerʗ1 = printer;
    printer.nameGenerator.GetTextOfNode = @string (ж<ast.Node> node) => printerʗ1.getTextOfNode(node, false);
    var printerʗ2 = printer;
        printer.nameGenerator.IsFileLevelUniqueNameInCurrentFile = (@string p1, bool p2) => printerʗ2.isFileLevelUniqueNameInCurrentFile(p1, p2);
    var printerʗ3 = printer;
    printer.makeFileLevelOptimisticUniqueName = @string (@string name) => printerʗ3.ᏑnameGenerator.MakeFileLevelOptimisticUniqueName(name);
    printer.containerPos = -1;
    printer.containerEnd = -1;
    printer.declarationListContainerEnd = -1;
    printer.commentsDisabled = options.RemoveComments;
    return printer;
}

} partial class printerᴛPrinter {

internal static @string getLiteralTextOfNode(this ж<Printer> Ꮡp, ж<astꓸLiteralLikeNode> node, ж<ast.SourceFile> sourceFile, getLiteralTextFlags flags) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (ast.IsStringLiteral(node)) {
        var (textSourceNode, ok) = p.emitContext.textSource[node, ꟷ]; if (ok && textSourceNode != nil) {
            @string text = default;
            switch (textSourceNode.Kind) {
            default: {
                return Ꮡp.getLiteralTextOfNode(textSourceNode, ast.GetSourceFileOfNode(textSourceNode), flags);
            }
            case var ᴛ when ᴛ == ast.KindNumericLiteral: {
                text = textSourceNode.Text();
                break;
            }
            case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindPrivateIdentifier || ᴛ == ast.KindJsxNamespacedName: {
                text = Ꮡp.getTextOfNode(textSourceNode, false);
                break;
            }}

            switch (ᐧ) {
            case {} when (getLiteralTextFlags)(flags & getLiteralTextFlagsJsxAttributeEscape) != 0: {
                return "\""u8 + escapeJsxAttributeString(text, QuoteCharDoubleQuote) + "\""u8;
            }
            case {} when (getLiteralTextFlags)(flags & getLiteralTextFlagsNeverAsciiEscape) != 0 || (EmitFlags)(p.emitContext.EmitFlags(node) & EFNoAsciiEscaping) != 0: {
                return "\""u8 + EscapeString(text, QuoteCharDoubleQuote) + "\""u8;
            }
            default: {
                return "\""u8 + escapeNonAsciiString(text, QuoteCharDoubleQuote) + "\""u8;
            }}

        }
    }
    if ((EmitFlags)(p.emitContext.EmitFlags(node) & EFNoAsciiEscaping) != 0) {
        flags |= (getLiteralTextFlags)(getLiteralTextFlagsNeverAsciiEscape);
    }
    if (p.Options.Target >= core.ScriptTargetES2021) {
        flags |= (getLiteralTextFlags)(getLiteralTextFlagsAllowNumericSeparator);
    }
    return getLiteralText(node, core.Coalesce<ast.SourceFile>(sourceFile, p.currentSourceFile), flags);
}

internal static @string getTextOfNode(this ж<Printer> Ꮡp, ж<ast.Node> Ꮡnode, bool includeTrivia) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsMemberName(Ꮡnode) && p.emitContext.autoGenerate[Ꮡnode] != nil) {
        return Ꮡp.ᏑnameGenerator.GenerateName(Ꮡnode);
    }

    if (ast.IsStringLiteral(Ꮡnode)) {
        var textSourceNode = p.emitContext.textSource[Ꮡnode]; if (textSourceNode != nil) {
            return Ꮡp.getTextOfNode(textSourceNode, includeTrivia);
        }
    }

    var canUseSourceFile = p.currentSourceFile != nil && node.Parent != nil && !ast.NodeIsSynthesized(Ꮡnode);

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindPrivateIdentifier || ᴛ == ast.KindJsxNamespacedName: {
        if (!canUseSourceFile || ast.GetSourceFileOfNode(Ꮡnode) != p.emitContext.MostOriginal(p.currentSourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).AsSourceFile()) {
            return node.Text();
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral || ᴛ == ast.KindNumericLiteral || ᴛ == ast.KindBigIntLiteral || ᴛ == ast.KindNoSubstitutionTemplateLiteral || ᴛ == ast.KindTemplateHead || ᴛ == ast.KindTemplateMiddle || ᴛ == ast.KindTemplateTail: {
        return Ꮡp.getLiteralTextOfNode(Ꮡnode, nil, getLiteralTextFlagsNone);
    }
    default: {
        throw panic(fmt.Sprintf("unexpected node: %v"u8, node.Kind));
        break;
    }}

    return scanner.GetSourceTextOfNodeFromSourceFile(p.currentSourceFile, Ꮡnode, includeTrivia);
}

} partial class printer_package {

partial struct WriteKind /*num:nint*/;

public static WriteKind WriteKindNone => /* iota */ 0;
public static WriteKind WriteKindKeyword => 1;
public static WriteKind WriteKindOperator => 2;
public static WriteKind WriteKindPunctuation => 3;
public static WriteKind WriteKindStringLiteral => 4;
public static WriteKind WriteKindParameter => 5;
public static WriteKind WriteKindProperty => 6;
public static WriteKind WriteKindComment => 7;
public static WriteKind WriteKindLiteral => 8;

} partial class printerᴛPrinter {

internal static void writeAs(this ref Printer p, @string text, WriteKind writeKind) {
    switch (writeKind) {
    case var ᴛ when ᴛ == WriteKindNone: {
        p.writer.Write(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindParameter: {
        p.writeParameter(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindKeyword: {
        p.writeKeyword(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindOperator: {
        p.writeOperator(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindProperty: {
        p.writeProperty(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindPunctuation: {
        p.writePunctuation(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindStringLiteral: {
        p.writer.WriteStringLiteral(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindComment: {
        p.writeComment(text);
        break;
    }
    case var ᴛ when ᴛ == WriteKindLiteral: {
        p.writeLiteral(text);
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected printer.WriteKind: %v"u8, writeKind));
        break;
    }}

}

internal static void write(this ref Printer p, @string text) {
    p.writeAs(text, p.writeKind);
}

internal static WriteKind setWriteKind(this ref Printer p, WriteKind kind) {
    WriteKind previous = p.writeKind;
    p.writeKind = kind;
    return previous;
}

internal static void writeSymbol(this ref Printer p, @string text, ж<ast.Symbol> optSymbol) {
    if (optSymbol == nil) {
        p.write(text);
    } else {
        p.writer.WriteSymbol(text, optSymbol);
    }
}

internal static void writeLiteral(this ref Printer p, @string text) {
    p.writer.WriteLiteral(text);
}

internal static void writePunctuation(this ref Printer p, @string text) {
    p.writer.WritePunctuation(text);
}

internal static void writeOperator(this ref Printer p, @string text) {
    p.writer.WriteOperator(text);
}

internal static void writeKeyword(this ref Printer p, @string text) {
    p.writer.WriteKeyword(text);
}

internal static void writeProperty(this ref Printer p, @string text) {
    p.writer.WriteProperty(text);
}

internal static void writeParameter(this ref Printer p, @string text) {
    p.writer.WriteParameter(text);
}

internal static void writeComment(this ref Printer p, @string text) {
    p.writer.WriteComment(text);
}

internal static void writeSpace(this ref Printer p) {
    p.writer.WriteSpace(" ");
}

internal static void writeLine(this ref Printer p) {
    p.writer.WriteLine();
}

internal static void writeLineRepeat(this ref Printer p, nint count) {
    foreach (var _ᴛ1 in range(count)) {
        p.writeLine();
    }
}

internal static void writeLines(this ref Printer p, @string text) {
    var lines = stringutil.SplitLines(text);
    nint indentation = stringutil.GuessIndentation(lines);
    foreach (var (_, vᴛ1) in range(lines)) {
        var line = vᴛ1;

        if (indentation > 0) {
            line = line.slice(indentation);
        }
        if (len(line) > 0) {
            p.writeLine();
            p.write(line);
        }
    }
}

internal static void writeTrailingSemicolon(this ref Printer p) {
    p.writer.WriteTrailingSemicolon(";");
}

internal static void increaseIndent(this ref Printer p) {
    p.writer.IncreaseIndent();
}

internal static void decreaseIndent(this ref Printer p) {
    p.writer.DecreaseIndent();
}

internal static void increaseIndentIf(this ref Printer p, bool indentRequested) {
    if (indentRequested) {
        p.increaseIndent();
    }
}

internal static void decreaseIndentIf(this ref Printer p, bool indentRequested) {
    if (indentRequested) {
        p.decreaseIndent();
    }
}

internal static void writeLineOrSpace(this ж<Printer> Ꮡp, ж<ast.Node> parentNode, ж<ast.Node> prevChildNode, ж<ast.Node> nextChildNode) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.shouldEmitOnSingleLine(parentNode)) {
        p.writeSpace();
    } else if (p.Options.PreserveSourceNewlines) {
        nint lines = Ꮡp.getLinesBetweenNodes(parentNode, prevChildNode, nextChildNode);
        if (lines > 0) {
            p.writeLineRepeat(lines);
        } else {
            p.writeSpace();
        }
    } else {
        p.writeLine();
    }
}

internal static void writeLinesAndIndent(this ref Printer p, nint lineCount, bool writeSpaceIfNotIndenting) {
    if (lineCount > 0) {
        p.increaseIndent();
        p.writeLineRepeat(lineCount);
    } else if (writeSpaceIfNotIndenting) {
        p.writeSpace();
    }
}

internal static bool writeLineSeparatorsAndIndentBefore(this ж<Printer> Ꮡp, ж<ast.Node> node, ж<ast.Node> parent) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.Options.PreserveSourceNewlines) {
        nint leadingNewlines = Ꮡp.getLeadingLineTerminatorCount(parent, node, LFNone);
        if (leadingNewlines > 0) {
            p.writeLinesAndIndent(leadingNewlines, false);
            return true;
        }
    }
    return false;
}

internal static void writeLineSeparatorsAfter(this ж<Printer> Ꮡp, ж<ast.Node> node, ж<ast.Node> parent) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.Options.PreserveSourceNewlines) {
        nint trailingNewlines = Ꮡp.getClosingLineTerminatorCount(parent, node, LFNone, core.NewTextRange(-1, -1));
        if (trailingNewlines > 0) {
            p.writeLineRepeat(trailingNewlines);
        }
    }
}

internal static nint getLinesBetweenNodes(this ж<Printer> Ꮡp, ж<ast.Node> Ꮡparent, ж<ast.Node> Ꮡnode1, ж<ast.Node> Ꮡnode2) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var parent = ref Ꮡparent.DerefOrNull();
    ref var node1 = ref Ꮡnode1.DerefOrNull();
    ref var node2 = ref Ꮡnode2.DerefOrNull();

    if (p.shouldElideIndentation(Ꮡparent)) {
        return 0;
    }

    Ꮡparent = skipSynthesizedParentheses(Ꮡparent); parent = ref Ꮡparent.DerefOrNull();
    Ꮡnode1 = skipSynthesizedParentheses(Ꮡnode1); node1 = ref Ꮡnode1.DerefOrNull();
    Ꮡnode2 = skipSynthesizedParentheses(Ꮡnode2); node2 = ref Ꮡnode2.DerefOrNull();

    if (p.shouldEmitOnNewLine(Ꮡnode2, LFNone)) {
        return 1;
    }

    if (p.currentSourceFile != nil && !ast.NodeIsSynthesized(Ꮡparent) && !ast.NodeIsSynthesized(Ꮡnode1) && !ast.NodeIsSynthesized(Ꮡnode2)) {
        if (p.Options.PreserveSourceNewlines) {
            return p.getEffectiveLines(
                (bool includeComments) => getLinesBetweenRangeEndAndRangeStart(
                        Ꮡnode1.Value.Loc,
                        Ꮡnode2.Value.Loc,
                        Ꮡp.Value.currentSourceFile,
                        includeComments));
        }
        return core.IfElse<nint>(rangeEndIsOnSameLineAsRangeStart(node1.Loc, node2.Loc, p.currentSourceFile), 0, 1);
    }

    return 0;
}

internal static nint getEffectiveLines(this ref Printer p, Func<bool, nint> getLineDifference) {
    if (!p.Options.PreserveSourceNewlines) {
        throw panic("Should not be called when preserveSourceNewlines is false");
    }
    nint lines = getLineDifference(true);
    if (lines == 0) {
        return getLineDifference(false);
    }
    return lines;
}

internal static nint getLeadingLineTerminatorCount(this ж<Printer> Ꮡp, ж<ast.Node> ᏑparentNode, ж<ast.Node> ᏑfirstChild, ListFormat format) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var parentNode = ref ᏑparentNode.DerefOrNull();
    ref var firstChild = ref ᏑfirstChild.DerefOrNull();

    if ((ListFormat)(format & LFPreserveLines) != 0 || p.Options.PreserveSourceNewlines) {
        if ((ListFormat)(format & LFPreferNewLine) != 0) {
            return 1;
        }

        if (ᏑfirstChild == nil) {
            return core.IfElse<nint>(ᏑparentNode == nil || p.currentSourceFile != nil && RangeIsOnSingleLine(parentNode.Loc, p.currentSourceFile), 0, 1);
        }
        if (p.nextListElementPos > 0 && firstChild.Pos() == p.nextListElementPos) {
            return 0;
        }
        if (firstChild.Kind == ast.KindJsxText) {
            return 0;
        }
        if (p.currentSourceFile != nil && ᏑparentNode != nil && !ast.PositionIsSynthesized(parentNode.Pos()) && !ast.NodeIsSynthesized(ᏑfirstChild) && (firstChild.Parent == nil)) {
            if (p.Options.PreserveSourceNewlines) {
                return p.getEffectiveLines(
                    (bool includeComments) => getLinesBetweenPositionAndPrecedingNonWhitespaceCharacter(
                            ᏑfirstChild.Value.Pos(),
                            ᏑparentNode.Value.Pos(),
                            Ꮡp.Value.currentSourceFile,
                            includeComments));
            }
            return core.IfElse<nint>(RangeStartPositionsAreOnSameLine(parentNode.Loc, firstChild.Loc, p.currentSourceFile), 0, 1);
        }
        if (p.shouldEmitOnNewLine(ᏑfirstChild, format)) {
            return 1;
        }
    }
    return core.IfElse<nint>((ListFormat)(format & LFMultiLine) != 0, 1, 0);
}

internal static nint getSeparatingLineTerminatorCount(this ж<Printer> Ꮡp, ж<ast.Node> ᏑpreviousNode, ж<ast.Node> ᏑnextNode, ListFormat format) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var previousNode = ref ᏑpreviousNode.DerefOrNull();
    ref var nextNode = ref ᏑnextNode.DerefOrNull();

    if ((ListFormat)(format & LFPreserveLines) != 0 || p.Options.PreserveSourceNewlines) {
        if (ᏑpreviousNode == nil || ᏑnextNode == nil) {
            return 0;
        }
        if (nextNode.Kind == ast.KindJsxText) {
            return 0;
        } else if (p.currentSourceFile != nil && !ast.NodeIsSynthesized(ᏑpreviousNode) && !ast.NodeIsSynthesized(ᏑnextNode)) {
            if (p.Options.PreserveSourceNewlines && siblingNodePositionsAreComparable(p.emitContext, ᏑpreviousNode, ᏑnextNode)) {
                return p.getEffectiveLines(
                    (bool includeComments) => getLinesBetweenRangeEndAndRangeStart(
                            ᏑpreviousNode.Value.Loc,
                            ᏑnextNode.Value.Loc,
                            Ꮡp.Value.currentSourceFile,
                            includeComments));
            } else if (!p.Options.PreserveSourceNewlines && originalNodesHaveSameParent(p.emitContext, ᏑpreviousNode, ᏑnextNode)) {
                return core.IfElse<nint>(rangeEndIsOnSameLineAsRangeStart(previousNode.Loc, nextNode.Loc, p.currentSourceFile), 0, 1);
            }
            return core.IfElse<nint>((ListFormat)(format & LFPreferNewLine) != 0, 1, 0);
        } else if (p.shouldEmitOnNewLine(ᏑpreviousNode, format) || p.shouldEmitOnNewLine(ᏑnextNode, format)) {
            return 1;
        }
    } else if (p.shouldEmitOnNewLine(ᏑnextNode, LFNone)) {
        return 1;
    }
    return core.IfElse<nint>((ListFormat)(format & LFMultiLine) != 0, 1, 0);
}

internal static nint getClosingLineTerminatorCount(this ж<Printer> Ꮡp, ж<ast.Node> ᏑparentNode, ж<ast.Node> ᏑlastChild, ListFormat format, core.TextRange childrenTextRange) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var parentNode = ref ᏑparentNode.DerefOrNull();
    ref var lastChild = ref ᏑlastChild.DerefOrNull();

    if ((ListFormat)(format & LFPreserveLines) != 0 || p.Options.PreserveSourceNewlines) {
        if ((ListFormat)(format & LFPreferNewLine) != 0) {
            return 1;
        }
        if (ᏑlastChild == nil) {
            return core.IfElse<nint>(ᏑparentNode == nil || p.currentSourceFile != nil && RangeIsOnSingleLine(parentNode.Loc, p.currentSourceFile), 0, 1);
        }
        if (p.currentSourceFile != nil && ᏑparentNode != nil && !ast.PositionIsSynthesized(parentNode.Pos()) && !ast.NodeIsSynthesized(ᏑlastChild) && (lastChild.Parent == nil || lastChild.Parent == ᏑparentNode)) {
            if (p.Options.PreserveSourceNewlines) {
                nint end = greatestEnd(lastChild.End(), new core_TextRangeᴠtryGetEnd_node(childrenTextRange));
                return p.getEffectiveLines(
                    (bool includeComments) => getLinesBetweenPositionAndNextNonWhitespaceCharacter(
                            end,
                            ᏑparentNode.Value.End(),
                            Ꮡp.Value.currentSourceFile,
                            includeComments));
            }
            return core.IfElse<nint>(rangeEndPositionsAreOnSameLine(parentNode.Loc, lastChild.Loc, p.currentSourceFile), 0, 1);
        }
        if (p.shouldEmitOnNewLine(ᏑlastChild, format)) {
            return 1;
        }
    }
    if ((ListFormat)(format & LFMultiLine) != 0 && (ListFormat)(format & LFNoTrailingNewLine) == 0) {
        return 1;
    }
    return 0;
}

internal static void writeCommentRange(this ref Printer p, ast.CommentRange comment) {
    if (p.currentSourceFile == nil) {
        return;
    }

    @string text = p.currentSourceFile.Text();
    var lineMap = p.currentSourceFile.ECMALineMap();
    p.writeCommentRangeWorker(text, lineMap, comment.Kind, comment.TextRange);
}

internal static void writeCommentRangeWorker(this ref Printer p, @string text, slice<core.TextPos> lineMap, ast.Kind kind, core.TextRange loc) {
    if (kind == ast.KindMultiLineCommentTrivia) {
        nint indentSize = GetDefaultIndentSize();
        nint firstLine = scanner.ComputeLineOfPosition(lineMap, loc.Pos());
        nint lineCount = len(lineMap);
        nint firstCommentLineIndent = -1;
        nint pos = loc.Pos();
        nint currentLine = firstLine;
        for (; pos < loc.End(); currentLine++) {
            nint nextLineStart = default;
            if (currentLine + 1 == lineCount) {
                nextLineStart = len(text) + 1;
            } else {
                nextLineStart = (nint)lineMap[currentLine + 1];
            }

            if (pos != loc.Pos()) {
                if (firstCommentLineIndent == -1) {
                    firstCommentLineIndent = calculateIndent(text, (nint)lineMap[firstLine], loc.Pos());
                }

                nint currentWriterIndentSpacing = p.writer.GetIndent() * indentSize;

                nint spacesToEmit = currentWriterIndentSpacing - firstCommentLineIndent + calculateIndent(text, pos, nextLineStart);
                if (spacesToEmit > 0) {
                    nint numberOfSingleSpacesToEmit = rem(spacesToEmit, indentSize);
                    @string indentSizeSpaceString = getIndentString(quo((spacesToEmit - numberOfSingleSpacesToEmit), indentSize), indentSize);

                    p.writer.RawWrite(indentSizeSpaceString);

                    while (numberOfSingleSpacesToEmit > 0) {
                        p.writer.RawWrite(" ");
                        numberOfSingleSpacesToEmit--;
                    }
                } else {
                    p.writer.RawWrite("");
                }
            }

            nint end = min(loc.End(), nextLineStart);
            for (nint scan = pos; scan < end; ) {
                var (ch, size) = utf8.DecodeRuneInString(text.slice(scan, end));
                if (size == 0) {
                    break;
                }
                if (stringutil.IsLineBreak(ch)) {
                    end = scan;
                    break;
                }
                scan += size;
            }
            @string currentLineText = strings.TrimSpace(text.slice(pos, end));
            if (len(currentLineText) > 0) {
                p.writeComment(currentLineText);
                if (end != loc.End()) {
                    p.writeLine();
                }
            } else {
                p.writer.WriteLineForce(true);
            }

            pos = nextLineStart;
        }
    } else {
        p.writeComment(text.slice(loc.Pos(), loc.End()));
    }
}

internal static bool shouldEmitComments(this ref Printer p, ж<ast.Node> node) {
    return !p.commentsDisabled && p.currentSourceFile != nil && !ast.IsSourceFile(node);
}

internal static bool shouldWriteComment(this ref Printer p, ast.CommentRange comment) {
    return !p.Options.OnlyPrintJSDocStyle || p.currentSourceFile != nil && isJSDocLikeText(p.currentSourceFile.Text(), comment) || p.currentSourceFile != nil && IsPinnedComment(p.currentSourceFile.Text(), comment);
}

internal static bool shouldEmitIndented(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFIndented) != 0;
}

internal static bool shouldElideIndentation(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFNoIndentation) != 0;
}

internal static bool shouldEmitOnSingleLine(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFSingleLine) != 0;
}

internal static bool shouldEmitOnMultipleLines(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFMultiLine) != 0;
}

internal static bool shouldEmitBlockFunctionBodyOnSingleLine(this ж<Printer> Ꮡp, ж<ast.Block> Ꮡbody) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var body = ref Ꮡbody.DerefOrNull();

    if (p.shouldEmitOnSingleLine(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return true;
    }

    if (body.MultiLine) {
        return false;
    }

    if (!ast.NodeIsSynthesized(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && p.currentSourceFile != nil && !RangeIsOnSingleLine(body.Loc, p.currentSourceFile)) {
        return false;
    }

    if (Ꮡp.getLeadingLineTerminatorCount(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.FirstOrNil(body.Statements.Nodes), LFPreserveLines) > 0 || Ꮡp.getClosingLineTerminatorCount(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.LastOrNil(body.Statements.Nodes), LFPreserveLines, body.Statements.Loc) > 0) {
        return false;
    }

    ж<astꓸStatement> previousStatement = default;
    foreach (var (_, statement) in range(body.Statements.Nodes)) {
        if (Ꮡp.getSeparatingLineTerminatorCount(previousStatement, statement, LFPreserveLines) > 0) {
            return false;
        }

        previousStatement = statement;
    }

    return true;
}

internal static bool shouldEmitOnNewLine(this ref Printer p, ж<ast.Node> node, ListFormat format) {
    if ((EmitFlags)(p.emitContext.EmitFlags(node) & EFStartOnNewLine) != 0) {
        return true;
    }
    return (ListFormat)(format & LFPreferNewLine) != 0;
}

internal static bool shouldEmitSourceMaps(this ref Printer p, ж<ast.Node> node) {
    return !p.sourceMapsDisabled && p.sourceMapSource != nil && !ast.IsSourceFile(node) && !ast.IsInJsonFile(node);
}

internal static bool shouldEmitTokenSourceMaps(this ref Printer p, ast.Kind token, nint pos, ж<ast.Node> contextNode, tokenEmitFlags flags) {
    return (tokenEmitFlags)(flags & tefNoSourceMaps) == 0 && p.shouldEmitSourceMaps(contextNode) && !p.Options.OmitBraceSourceMapPositions && (token == ast.KindOpenBraceToken || token == ast.KindCloseBraceToken);
}

internal static bool shouldEmitLeadingComments(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFNoLeadingComments) == 0;
}

internal static bool shouldEmitTrailingComments(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFNoTrailingComments) == 0;
}

internal static bool shouldEmitNestedComments(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFNoNestedComments) == 0;
}

internal static bool shouldEmitDetachedComments(this ref Printer p, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsSourceFile(Ꮡnode)) {
        return true;
    }

    var @file = node.AsSourceFile();

    return len((~@file).Statements.Nodes) == 0 || !ast.IsPrologueDirective((~@file).Statements.Nodes[0]) || ast.NodeIsSynthesized((~@file).Statements.Nodes[0]);
}

internal static bool hasCommentsAtPosition(this ref Printer p, nint pos) {
    if (p.currentSourceFile == nil) {
        return false;
    }

    foreach (var _ᴛ1 in range<ast.CommentRange>(scanner.GetTrailingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), p.currentSourceFile.Text(), pos + 1).Invoke)) {
        return true;
    }
    foreach (var _ᴛ2 in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), p.currentSourceFile.Text(), pos + 1).Invoke)) {
        return true;
    }
    return false;
}

internal static bool shouldEmitIndirectCall(this ref Printer p, ж<ast.Node> node) {
    return (EmitFlags)(p.emitContext.EmitFlags(node) & EFIndirectCall) != 0;
}

internal static bool shouldAllowTrailingComma(this ref Printer p, ж<ast.Node> Ꮡnode, ж<ast.NodeList> list) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (p.currentSourceFile == nil || p.currentSourceFile.ScriptKind == core.ScriptKindJSON) {
        return false;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindConstructor || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType || ᴛ == ast.KindCallSignature || ᴛ == ast.KindConstructSignature || ᴛ == ast.KindTaggedTemplateExpression || ᴛ == ast.KindObjectBindingPattern || ᴛ == ast.KindArrayBindingPattern || ᴛ == ast.KindNamedImports || ᴛ == ast.KindNamedExports || ᴛ == ast.KindImportAttributes: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindClassExpression || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindInterfaceDeclaration: {
        return list == node.TypeParameterList();
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindMethodDeclaration: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindNewExpression: {
        return true;
    }}

    return false;
}

internal static nint writeTokenText(this ref Printer p, ast.Kind token, WriteKind writeKind, nint pos) {
    @string tokenString = scanner.TokenToString(token);
    p.writeAs(tokenString, writeKind);
    if (ast.PositionIsSynthesized(pos)) {
        return pos;
    } else {
        return pos + len(tokenString);
    }
}

internal static nint emitToken(this ref Printer p, ast.Kind token, nint pos, WriteKind writeKind, ж<ast.Node> contextNode) {
    return p.emitTokenEx(token, pos, writeKind, contextNode, tefNone);
}

internal static nint emitTokenEx(this ref Printer p, ast.Kind token, nint pos, WriteKind writeKind, ж<ast.Node> contextNode, tokenEmitFlags flags) {
    (var state, pos) = p.enterToken(token, pos, contextNode, flags);
    pos = p.writeTokenText(token, writeKind, pos);
    p.exitToken(token, pos, contextNode, state);
    return pos;
}

internal static void emitKeywordNode(this ref Printer p, ж<astꓸTokenNode> node) {
    p.emitKeywordNodeEx(node, tefNone);
}

internal static void emitKeywordNodeEx(this ref Printer p, ж<astꓸTokenNode> Ꮡnode, tokenEmitFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    var state = p.enterTokenNode(Ꮡnode, flags);
    p.writeTokenText(node.Kind, WriteKindKeyword, node.Pos());
    p.exitTokenNode(Ꮡnode, state);
}

internal static void emitPunctuationNode(this ref Printer p, ж<astꓸTokenNode> node) {
    p.emitPunctuationNodeEx(node, tefNone);
}

internal static void emitPunctuationNodeEx(this ref Printer p, ж<astꓸTokenNode> Ꮡnode, tokenEmitFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    var state = p.enterTokenNode(Ꮡnode, flags);
    p.writeTokenText(node.Kind, WriteKindPunctuation, node.Pos());
    p.exitTokenNode(Ꮡnode, state);
}

internal static void emitTokenNode(this ref Printer p, ж<astꓸTokenNode> node) {
    p.emitTokenNodeEx(node, tefNone);
}

internal static void emitTokenNodeEx(this ref Printer p, ж<astꓸTokenNode> Ꮡnode, tokenEmitFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    switch (ᐧ) {
    case {} when ast.IsKeywordKind(node.Kind): {
        p.emitKeywordNodeEx(Ꮡnode, flags);
        break;
    }
    case {} when ast.IsPunctuationKind(node.Kind): {
        p.emitPunctuationNodeEx(Ꮡnode, flags);
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected TokenNode: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitLiteral(this ж<Printer> Ꮡp, ж<astꓸLiteralLikeNode> node, getLiteralTextFlags flags) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.Options.NeverAsciiEscape) {
        flags |= (getLiteralTextFlags)(getLiteralTextFlagsNeverAsciiEscape);
    }
    if (p.Options.TerminateUnterminatedLiterals) {
        flags |= (getLiteralTextFlags)(getLiteralTextFlagsTerminateUnterminatedLiterals);
    }

    @string text = Ꮡp.getLiteralTextOfNode(node, nil, flags);

    p.writer.WriteStringLiteral(text);
}

internal static void emitNumericLiteral(this ж<Printer> Ꮡp, ж<ast.NumericLiteral> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.NumericLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.NumericLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.NumericLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitBigIntLiteral(this ж<Printer> Ꮡp, ж<ast.BigIntLiteral> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.BigIntLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.BigIntLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.BigIntLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitStringLiteral(this ж<Printer> Ꮡp, ж<ast.StringLiteral> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.StringLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.StringLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.StringLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNoSubstitutionTemplateLiteral(this ж<Printer> Ꮡp, ж<ast.NoSubstitutionTemplateLiteral> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.NoSubstitutionTemplateLiteral.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.NoSubstitutionTemplateLiteral.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.NoSubstitutionTemplateLiteral.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitRegularExpressionLiteral(this ж<Printer> Ꮡp, ж<ast.RegularExpressionLiteral> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.RegularExpressionLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.RegularExpressionLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.RegularExpressionLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateHead(this ж<Printer> Ꮡp, ж<ast.TemplateHead> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.TemplateHead.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.TemplateHead.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.TemplateHead.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateMiddle(this ж<Printer> Ꮡp, ж<ast.TemplateMiddle> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.TemplateMiddle.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.TemplateMiddle.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.TemplateMiddle.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateTail(this ж<Printer> Ꮡp, ж<ast.TemplateTail> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.TemplateTail.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLiteral(node.of(ast.TemplateTail.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getLiteralTextFlagsNone);
    p.exitNode(node.of(ast.TemplateTail.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateMiddleTail(this ж<Printer> Ꮡp, ж<astꓸTemplateMiddleOrTail> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindTemplateMiddle: {
        Ꮡp.emitTemplateMiddle(node.AsTemplateMiddle());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateTail: {
        Ꮡp.emitTemplateTail(node.AsTemplateTail());
        break;
    }}

}

internal static void emitSnippetNode(this ref Printer p, ж<ast.Node> node, ж<SnippetElement> ᏑsnippetElement) {
    ref var snippetElement = ref ᏑsnippetElement.DerefOrNull();

    switch (snippetElement.Kind) {
    case var ᴛ when ᴛ == SnippetKindTabStop: {
        p.emitTabStop(node, ᏑsnippetElement);
        break;
    }
    default: {
        throw panic(fmt.Sprintf("Unhandled snippet element kind: %v"u8, snippetElement.Kind));
        break;
    }}

}

internal static void emitTabStop(this ref Printer p, ж<ast.Node> Ꮡnode, ж<SnippetElement> ᏑsnippetElement) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var snippetElement = ref ᏑsnippetElement.DerefOrNull();

    debug.Assert(node.Kind == ast.KindEmptyStatement, "Snippet tab stops can only be emitted on empty statements");
    p.writer.RawWrite(fmt.Sprintf("$%d"u8, snippetElement.Order));
}

internal static void emitIdentifierText(this ж<Printer> Ꮡp, ж<ast.Identifier> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var f = ast.GetSourceFileOfNode(node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    debug.Assert(f == nil || p.currentSourceFile == nil || f.FileName() == p.currentSourceFile.FileName());
    @string text = Ꮡp.getTextOfNode(node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false);

    if (p.IdToSymbol != nil) {
        var (symbol, ok) = p.IdToSymbol[node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ꟷ]; if (ok) {
            p.writeSymbol(text, symbol);
            return;
        }
    }
    p.write(text);
}

internal static void emitIdentifierName(this ж<Printer> Ꮡp, ж<ast.Identifier> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitIdentifierText(node);
    p.exitNode(node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitIdentifierNameNode(this ж<Printer> Ꮡp, ж<astꓸIdentifierNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }
    Ꮡp.emitIdentifierName(node.AsIdentifier());
}

internal static ж<astꓸIdentifierNode> getUniqueHelperName(this ж<Printer> Ꮡp, @string name) {
    ref var p = ref Ꮡp.DerefOrNull();

    var helperName = p.uniqueHelperNames[name];
    if (helperName == nil) {
        var helperNameΔ1 = p.emitContext.Factory.NewUniqueNameEx(name, new AutoGenerateOptions(Flags: (GeneratedIdentifierFlags)((nint)GeneratedIdentifierFlagsFileLevel | (nint)GeneratedIdentifierFlagsOptimistic)));
        Ꮡp.generateName(helperNameΔ1);
        p.uniqueHelperNames[name] = helperNameΔ1;
        return helperNameΔ1;
    }
    return helperName.Clone(new NodeFactoryжNodeFactoryCoercible(p.emitContext.Factory));
}

internal static void emitIdentifierReference(this ж<Printer> Ꮡp, ж<ast.Identifier> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((p.externalHelpersModuleName != nil || p.uniqueHelperNames != nil) && (EmitFlags)(p.emitContext.EmitFlags(Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) & EFHelperName) != 0) {
        if (p.externalHelpersModuleName != nil) {
            var helper = p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
                p.externalHelpersModuleName.Clone(new NodeFactoryжNodeFactoryCoercible(p.emitContext.Factory)),
                nil,
                Ꮡnode.Clone(new NodeFactoryжNodeFactoryCoercible(p.emitContext.Factory)),
                ast.NodeFlagsNone);
            p.emitContext.AssignCommentAndSourceMapRanges(helper, Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            Ꮡp.emitPropertyAccessExpression(helper.AsPropertyAccessExpression());
            return;
        }
        if (p.uniqueHelperNames != nil) {
            var helperName = Ꮡp.getUniqueHelperName(node.Text);
            p.emitContext.AssignCommentAndSourceMapRanges(helperName, Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            Ꮡnode = helperName.AsIdentifier(); node = ref Ꮡnode.DerefOrNull();
        }
    }

    var state = p.enterNode(Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitIdentifierText(Ꮡnode);
    p.exitNode(Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitBindingIdentifier(this ж<Printer> Ꮡp, ж<ast.Identifier> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (p.uniqueHelperNames != nil && (EmitFlags)(p.emitContext.EmitFlags(Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) & EFHelperName) != 0) {
        var helperName = Ꮡp.getUniqueHelperName(node.Text);
        p.emitContext.AssignCommentAndSourceMapRanges(helperName, Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        Ꮡnode = helperName.AsIdentifier(); node = ref Ꮡnode.DerefOrNull();
    }

    var state = p.enterNode(Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitIdentifierText(Ꮡnode);
    p.exitNode(Ꮡnode.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitLabelIdentifier(this ж<Printer> Ꮡp, ж<ast.Identifier> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitIdentifierText(node);
    p.exitNode(node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitPrivateIdentifier(this ж<Printer> Ꮡp, ж<ast.PrivateIdentifier> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.PrivateIdentifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.write(Ꮡp.getTextOfNode(node.of(ast.PrivateIdentifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), false));
    p.exitNode(node.of(ast.PrivateIdentifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitQualifiedName(this ж<Printer> Ꮡp, ж<ast.QualifiedName> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.QualifiedName.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEntityName(node.Left);
    p.writePunctuation(".");
    Ꮡp.emitMemberName(node.Right);
    p.exitNode(Ꮡnode.of(ast.QualifiedName.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitComputedPropertyName(this ж<Printer> Ꮡp, ж<ast.ComputedPropertyName> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ComputedPropertyName.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("[");
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceDisallowComma);
    p.writePunctuation("]");
    p.exitNode(Ꮡnode.of(ast.ComputedPropertyName.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitEntityName(this ж<Printer> Ꮡp, ж<astꓸEntityName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierReference(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        Ꮡp.emitQualifiedName(node.AsQualifiedName());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        Ꮡp.emitExpression(Ꮡnode, ast.OperatorPrecedenceDisallowComma);
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected EntityName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitBindingName(this ж<Printer> Ꮡp, ж<astꓸBindingName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitBindingIdentifier(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectBindingPattern: {
        Ꮡp.emitObjectBindingPattern(node.AsBindingPattern());
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrayBindingPattern: {
        Ꮡp.emitArrayBindingPattern(node.AsBindingPattern());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected BindingName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitPropertyName(this ж<Printer> Ꮡp, ж<astꓸPropertyName> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    WriteKind savedWriteKind = p.writeKind;
    p.writeKind = WriteKindProperty;

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierName(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrivateIdentifier: {
        Ꮡp.emitPrivateIdentifier(node.AsPrivateIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        Ꮡp.emitStringLiteral(node.AsStringLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNoSubstitutionTemplateLiteral: {
        Ꮡp.emitNoSubstitutionTemplateLiteral(node.AsNoSubstitutionTemplateLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral: {
        Ꮡp.emitNumericLiteral(node.AsNumericLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindBigIntLiteral: {
        Ꮡp.emitBigIntLiteral(node.AsBigIntLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindComputedPropertyName: {
        Ꮡp.emitComputedPropertyName(node.AsComputedPropertyName());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected PropertyName: %v"u8, node.Kind));
        break;
    }}

    p.writeKind = savedWriteKind;
}

internal static void emitMemberName(this ж<Printer> Ꮡp, ж<astꓸMemberName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierName(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrivateIdentifier: {
        Ꮡp.emitPrivateIdentifier(node.AsPrivateIdentifier());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected MemberName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitModuleName(this ж<Printer> Ꮡp, ж<astꓸModuleName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitBindingIdentifier(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        Ꮡp.emitStringLiteral(node.AsStringLiteral());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected ModuleName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitModuleExportName(this ж<Printer> Ꮡp, ж<astꓸModuleExportName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierName(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        Ꮡp.emitStringLiteral(node.AsStringLiteral());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected ModuleExportName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitImportAttributeName(this ж<Printer> Ꮡp, ж<astꓸImportAttributeName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierName(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        Ꮡp.emitStringLiteral(node.AsStringLiteral());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected ImportAttributeName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitNestedModuleName(this ж<Printer> Ꮡp, ж<astꓸModuleName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierName(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        Ꮡp.emitStringLiteral(node.AsStringLiteral());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected ModuleName: %v"u8, node.Kind));
        break;
    }}

}

} partial class printer_package {

internal partial struct emitModifierList_Mode /*num:nint*/;

} partial class printerᴛPrinter {

internal static nint emitModifierList(this ж<Printer> Ꮡp, ж<ast.Node> ᏑparentNode, ж<ast.ModifierList> Ꮡmodifiers, bool allowDecorators) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var parentNode = ref ᏑparentNode.DerefOrNull();
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    if (Ꮡmodifiers == nil || len(modifiers.Nodes) == 0) {
        return parentNode.Pos();
    }

    if (core.Every<ж<ast.Node>>(modifiers.Nodes, ast.IsModifier)) {
        Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTokenNode>>)(printerᴛPrinter.emitKeywordNode)), ᏑparentNode, Ꮡmodifiers.ᏑNodeList, LFModifiers);
    } else if (core.Every<ж<ast.Node>>(modifiers.Nodes, ast.IsDecorator)) {
        if (!allowDecorators) {
            return parentNode.Pos();
        }

        Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸModifierLike>>)(printerᴛPrinter.emitModifierLike)), ᏑparentNode, Ꮡmodifiers.ᏑNodeList, LFDecorators);
    } else {
        if (p.OnBeforeEmitNodeList != nil) {
            p.OnBeforeEmitNodeList(Ꮡmodifiers.ᏑNodeList);
        }
        emitModifierList_Mode ModeNone = /* iota */ 0;
        emitModifierList_Mode ModeModifiers = 1;
        emitModifierList_Mode ModeDecorators = 2;

        emitModifierList_Mode lastMode = ModeNone;
        emitModifierList_Mode mode = ModeNone;
        nint start = 0;
        nint pos = 0;

        ж<astꓸModifierLike> lastModifier = default;
        while (start < len(modifiers.Nodes)) {
            while (pos < len(modifiers.Nodes)) {
                lastModifier = modifiers.Nodes[pos];
                if (ast.IsDecorator(lastModifier)) {
                    mode = ModeDecorators;
                } else {
                    mode = ModeModifiers;
                }
                if (lastMode == ModeNone) {
                    lastMode = mode;
                } else if (mode != lastMode) {
                    break;
                }
                pos++;
            }

            var textRange = core.NewTextRange(-1, -1);
            if (start == 0) {
                textRange = core.NewTextRange(Ꮡmodifiers.of(ast.ModifierList.ᏑNodeList).Pos(), textRange.End());
            }
            if (pos == len(modifiers.Nodes) - 1) {
                textRange = core.NewTextRange(textRange.Pos(), Ꮡmodifiers.of(ast.ModifierList.ᏑNodeList).End());
            }
            if (allowDecorators || lastMode == ModeModifiers) {
                Ꮡp.emitListItems(
                    ((Action<ж<Printer>, ж<astꓸModifierLike>>)(printerᴛPrinter.emitModifierLike)),
                    ᏑparentNode,
                    modifiers.Nodes.slice(start, pos),
                    core.IfElse(lastMode == ModeModifiers, LFModifiers, LFDecorators),
                    false,
                    textRange);
            }
            start = pos;
            lastMode = mode;
            pos++;
        }

        if (p.OnAfterEmitNodeList != nil) {
            p.OnAfterEmitNodeList(Ꮡmodifiers.ᏑNodeList);
        }
    }

    return greatestEnd(parentNode.Pos(), new ast_NodeжtryGetEnd_node(core.LastOrNil(modifiers.Nodes)));
}

internal static void emitTypeParameter(this ж<Printer> Ꮡp, ж<ast.TypeParameterDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.TypeParameterDeclaration.ᏑModifiersBase).Modifiers(), false);
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    if (node.Constraint != nil) {
        p.writeSpace();
        p.writeKeyword("extends");
        p.writeSpace();
        Ꮡp.emitTypeNodeOutsideExtends(node.Constraint);
    }
    if (node.DefaultType != nil) {
        p.writeSpace();
        p.writeOperator("=");
        p.writeSpace();
        Ꮡp.emitTypeNodeOutsideExtends(node.DefaultType);
    }
    p.exitNode(Ꮡnode.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeParameterDeclarationNode(this ж<Printer> Ꮡp, ж<astꓸTypeParameterDeclarationNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsTypeParameterDeclaration(Ꮡnode)) {
        Ꮡp.emitTypeParameter(node.AsTypeParameterDeclaration());
    } else {
        Ꮡp.emitTypeArgument(Ꮡnode);
    }
}

internal static void emitParameterName(this ж<Printer> Ꮡp, ж<astꓸBindingName> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    WriteKind savedWriteKind = p.writeKind;
    p.writeKind = WriteKindParameter;
    Ꮡp.emitBindingName(node);
    p.writeKind = savedWriteKind;
}

internal static void emitParameter(this ж<Printer> Ꮡp, ж<ast.ParameterDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers(), true);
    p.emitTokenNode(node.DotDotDotToken);
    Ꮡp.emitParameterName(node.Name());
    p.emitTokenNode(node.QuestionToken);

    Ꮡp.emitTypeAnnotation(node.Type);

    Ꮡp.emitInitializer(node.Initializer, greatestEnd(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), new ast_NodeжtryGetEnd_node(node.Type), new ast_NodeжtryGetEnd_node(node.QuestionToken), new ast_NodeжtryGetEnd_node(node.Name()), new ast_ModifierListжtryGetEnd_node(Ꮡnode.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers())), Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitParameterDeclarationNode(this ж<Printer> Ꮡp, ж<astꓸParameterDeclarationNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitParameter(node.AsParameterDeclaration());
}

internal static void emitDecorator(this ж<Printer> Ꮡp, ж<ast.Decorator> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.Decorator.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("@");
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLeftHandSide);
    p.exitNode(Ꮡnode.of(ast.Decorator.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitModifierLike(this ж<Printer> Ꮡp, ж<astꓸModifierLike> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (ᐧ) {
    case {} when ast.IsDecorator(Ꮡnode): {
        Ꮡp.emitDecorator(node.AsDecorator());
        break;
    }
    case {} when ast.IsModifier(Ꮡnode): {
        p.emitKeywordNode(Ꮡnode);
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled ModifierLike: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitTypeParameters(this ж<Printer> Ꮡp, ж<ast.Node> parentNode, ж<astꓸTypeParameterList> nodes) {
    if (nodes == nil) {
        return;
    }
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTypeParameterDeclarationNode>>)(printerᴛPrinter.emitTypeParameterDeclarationNode)), parentNode, nodes, (ListFormat)(LFTypeParameters | core.IfElse(ast.IsArrowFunction(parentNode), LFAllowTrailingComma, LFNone)));
}

internal static void emitTypeAnnotation(this ж<Printer> Ꮡp, ж<astꓸTypeNode> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (node == nil) {
        return;
    }

    p.writePunctuation(":");
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node);
}

internal static void emitInitializer(this ж<Printer> Ꮡp, ж<astꓸExpression> node, nint equalTokenPos, ж<ast.Node> contextNode) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (node == nil) {
        return;
    }

    p.writeSpace();
    p.emitToken(ast.KindEqualsToken, equalTokenPos, WriteKindOperator, contextNode);
    p.writeSpace();
    Ꮡp.emitExpression(node, ast.OperatorPrecedenceDisallowComma);
}

internal static void emitParameters(this ж<Printer> Ꮡp, ж<ast.Node> parentNode, ж<astꓸParameterList> parameters) {
    Ꮡp.generateAllNames(parameters);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸParameterDeclarationNode>>)(printerᴛPrinter.emitParameterDeclarationNode)), parentNode, parameters, LFParameters);
}

} partial class printer_package {

internal static bool canEmitSimpleArrowHead(ж<ast.Node> ᏑparentNode, ж<astꓸParameterList> Ꮡparameters) {
    ref var parentNode = ref ᏑparentNode.DerefOrNull();
    ref var parameters = ref Ꮡparameters.DerefOrNull();

    if (!ast.IsArrowFunction(ᏑparentNode) || len(parameters.Nodes) != 1) {
        return false;
    }

    var parent = parentNode.AsArrowFunction();
    var parameter = parameters.Nodes[0].AsParameterDeclaration();

    return parameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos() == parent.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos() && (~parent).TypeParameters == nil && (~parent).Type == nil && (parent.of(ast.ArrowFunction.ᏑModifiersBase).Modifiers() == nil || len(parent.of(ast.ArrowFunction.ᏑModifiersBase).Modifiers().Nodes) == 0) && !parameters.HasTrailingComma() && parameter.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers() == nil && parameter.DotDotDotToken == nil && (~parameter).QuestionToken == nil && (~parameter).Type == nil && (~parameter).Initializer == nil && ast.IsIdentifier(parameter.Name());
}

} partial class printerᴛPrinter {

internal static void emitParametersForArrow(this ж<Printer> Ꮡp, ж<ast.Node> parentNode, ж<astꓸParameterList> parameters) {
    if (canEmitSimpleArrowHead(parentNode, parameters)) {
        Ꮡp.generateAllNames(parameters);
        Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸParameterDeclarationNode>>)(printerᴛPrinter.emitParameterDeclarationNode)), parentNode, parameters, LFSingleArrowParameter);
    } else {
        Ꮡp.emitParameters(parentNode, parameters);
    }
}

internal static void emitParametersForIndexSignature(this ж<Printer> Ꮡp, ж<ast.Node> parentNode, ж<astꓸParameterList> parameters) {
    Ꮡp.generateAllNames(parameters);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸParameterDeclarationNode>>)(printerᴛPrinter.emitParameterDeclarationNode)), parentNode, parameters, LFIndexSignatureParameters);
}

internal static void emitSignature(this ж<Printer> Ꮡp, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var n = node.FunctionLikeData();

    Ꮡp.emitTypeParameters(Ꮡnode, n.TypeParameters);

    Ꮡp.emitParameters(Ꮡnode, n.Parameters);
    Ꮡp.emitTypeAnnotation(n.Type);
}

internal static void emitFunctionBody(this ж<Printer> Ꮡp, ж<ast.Block> Ꮡbody) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var body = ref Ꮡbody.DerefOrNull();

    p.emitContext.AddEmitFlags(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), EFNoSourceMap);

    if (p.OnBeforeEmitNode != nil) {
        p.OnBeforeEmitNode(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    Ꮡp.generateNames(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    p.writePunctuation("{");

    p.increaseIndent();
    var detachedState = p.emitDetachedCommentsBeforeStatementList(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), body.Statements.Loc);
    nint statementOffset = Ꮡp.emitPrologueDirectives(body.Statements);
    nint pos = p.writer.GetTextPos();
    p.emitHelpers(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    if (Ꮡp.shouldEmitBlockFunctionBodyOnSingleLine(Ꮡbody) && statementOffset == 0 && pos == p.writer.GetTextPos()) {
        p.decreaseIndent();
        Ꮡp.emitListRange(((Action<ж<Printer>, ж<astꓸStatement>>)(printerᴛPrinter.emitStatement)), Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), body.Statements, LFSingleLineFunctionBodyStatements, statementOffset, -1);
        p.increaseIndent();
    } else {
        Ꮡp.emitListRange(((Action<ж<Printer>, ж<astꓸStatement>>)(printerᴛPrinter.emitStatement)), Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), body.Statements, LFMultiLineFunctionBodyStatements, statementOffset, -1);
    }

    p.emitDetachedCommentsAfterStatementList(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), body.Statements.Loc, detachedState);
    p.decreaseIndent();

    p.emitTokenEx(ast.KindCloseBraceToken, body.Statements.End(), WriteKindPunctuation, Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tefNoComments);

    if (p.OnAfterEmitNode != nil) {
        p.OnAfterEmitNode(Ꮡbody.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
}

internal static void emitFunctionBodyNode(this ж<Printer> Ꮡp, ж<astꓸBlockNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        p.writeTrailingSemicolon();
        return;
    }

    p.writeSpace();
    Ꮡp.emitFunctionBody(node.AsBlock());
}

internal static void emitPropertySignature(this ж<Printer> Ꮡp, ж<ast.PropertySignatureDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.PropertySignatureDeclaration.ᏑNamedMemberBase).Modifiers(), false);
    Ꮡp.emitPropertyName(node.Name());
    p.emitTokenNode(node.PostfixToken);
    Ꮡp.emitTypeAnnotation(node.Type);
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitPropertyDeclaration(this ж<Printer> Ꮡp, ж<ast.PropertyDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers(), true);
    Ꮡp.emitPropertyName(node.Name());
    p.emitTokenNode(node.PostfixToken);
    Ꮡp.emitTypeAnnotation(node.Type);
    Ꮡp.emitInitializer(node.Initializer, greatestEnd(node.Name().End(), new ast_NodeжtryGetEnd_node(node.Type), new ast_NodeжtryGetEnd_node(node.PostfixToken)), Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitMethodSignature(this ж<Printer> Ꮡp, ж<ast.MethodSignatureDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNamedMemberBase).Modifiers(), false);
    Ꮡp.emitPropertyName(node.Name());
    p.emitTokenNode(node.PostfixToken);
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeTrailingSemicolon();
    p.popNameGenerationScope(Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitMethodDeclaration(this ж<Printer> Ꮡp, ж<ast.MethodDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers(), true);
    p.emitTokenNode(node.AsteriskToken);
    Ꮡp.emitPropertyName(node.Name());
    p.emitTokenNode(node.PostfixToken);
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitFunctionBodyNode(node.Body);
    p.popNameGenerationScope(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitClassStaticBlockDeclaration(this ж<Printer> Ꮡp, ж<ast.ClassStaticBlockDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ClassStaticBlockDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeKeyword("static");
    p.pushNameGenerationScope(Ꮡnode.of(ast.ClassStaticBlockDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitFunctionBodyNode(node.Body);
    p.popNameGenerationScope(Ꮡnode.of(ast.ClassStaticBlockDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.ClassStaticBlockDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitConstructor(this ж<Printer> Ꮡp, ж<ast.ConstructorDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ConstructorDeclaration.ᏑModifiersBase).Modifiers(), false);
    p.writeKeyword("constructor");
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitFunctionBodyNode(node.Body);
    p.popNameGenerationScope(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitAccessorDeclaration(this ж<Printer> Ꮡp, ast.Kind token, ж<ast.AccessorDeclarationBase> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = Ꮡp.emitModifierList(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers(), true);
    p.emitToken(token, pos, WriteKindKeyword, Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitPropertyName(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Name());
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitFunctionBodyNode(node.Body);
    p.popNameGenerationScope(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitGetAccessorDeclaration(this ж<Printer> Ꮡp, ж<ast.GetAccessorDeclaration> node) {
    Ꮡp.emitAccessorDeclaration(ast.KindGetKeyword, node.ᏑAccessorDeclarationBase);
}

internal static void emitSetAccessorDeclaration(this ж<Printer> Ꮡp, ж<ast.SetAccessorDeclaration> node) {
    Ꮡp.emitAccessorDeclaration(ast.KindSetKeyword, node.ᏑAccessorDeclarationBase);
}

internal static void emitCallSignature(this ж<Printer> Ꮡp, ж<ast.CallSignatureDeclaration> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var indented = p.shouldEmitIndented(node.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(node.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(node.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeTrailingSemicolon();
    p.popNameGenerationScope(node.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(node.of(ast.CallSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitConstructSignature(this ж<Printer> Ꮡp, ж<ast.ConstructSignatureDeclaration> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeKeyword("new");
    p.writeSpace();
    var indented = p.shouldEmitIndented(node.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(node.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(node.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeTrailingSemicolon();
    p.popNameGenerationScope(node.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(node.of(ast.ConstructSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitIndexSignature(this ж<Printer> Ꮡp, ж<ast.IndexSignatureDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑModifiersBase).Modifiers(), false);
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitParametersForIndexSignature(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Parameters);
    Ꮡp.emitTypeAnnotation(node.Type);
    p.writeTrailingSemicolon();
    p.popNameGenerationScope(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitClassElement(this ж<Printer> Ꮡp, ж<astꓸClassElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        Ꮡp.emitPropertyDeclaration(node.AsPropertyDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        Ꮡp.emitMethodDeclaration(node.AsMethodDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration: {
        Ꮡp.emitClassStaticBlockDeclaration(node.AsClassStaticBlockDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructor: {
        Ꮡp.emitConstructor(node.AsConstructorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        Ꮡp.emitGetAccessorDeclaration(node.AsGetAccessorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        Ꮡp.emitSetAccessorDeclaration(node.AsSetAccessorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIndexSignature: {
        Ꮡp.emitIndexSignature(node.AsIndexSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSemicolonClassElement: {
        p.emitSemicolonClassElement(node.AsSemicolonClassElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNotEmittedStatement: {
        p.emitNotEmittedStatement(node.AsNotEmittedStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSTypeAliasDeclaration: {
        Ꮡp.emitTypeAliasDeclaration(node.AsTypeAliasDeclaration());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected ClassElement: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitTypeElement(this ж<Printer> Ꮡp, ж<astꓸTypeElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertySignature: {
        Ꮡp.emitPropertySignature(node.AsPropertySignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMethodSignature: {
        Ꮡp.emitMethodSignature(node.AsMethodSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallSignature: {
        Ꮡp.emitCallSignature(node.AsCallSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructSignature: {
        Ꮡp.emitConstructSignature(node.AsConstructSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        Ꮡp.emitGetAccessorDeclaration(node.AsGetAccessorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        Ꮡp.emitSetAccessorDeclaration(node.AsSetAccessorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIndexSignature: {
        Ꮡp.emitIndexSignature(node.AsIndexSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNotEmittedTypeElement: {
        p.emitNotEmittedTypeElement(node.AsNotEmittedTypeElement());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected TypeElement: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitObjectLiteralElement(this ж<Printer> Ꮡp, ж<astꓸObjectLiteralElement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
        Ꮡp.emitPropertyAssignment(node.AsPropertyAssignment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        Ꮡp.emitShorthandPropertyAssignment(node.AsShorthandPropertyAssignment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSpreadAssignment: {
        Ꮡp.emitSpreadAssignment(node.AsSpreadAssignment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        Ꮡp.emitMethodDeclaration(node.AsMethodDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        Ꮡp.emitGetAccessorDeclaration(node.AsGetAccessorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        Ꮡp.emitSetAccessorDeclaration(node.AsSetAccessorDeclaration());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled ObjectLiteralElement: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitKeywordTypeNode(this ref Printer p, ж<ast.KeywordTypeNode> node) {
    p.emitKeywordNode(node.of(ast.KeywordTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static void emitTypePredicateParameterName(this ж<Printer> Ꮡp, ж<astꓸTypePredicateParameterName> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierReference(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindThisType: {
        p.emitThisType(node.AsThisTypeNode());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected TypePredicateParameterName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitTypePredicate(this ж<Printer> Ꮡp, ж<ast.TypePredicateNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypePredicateNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.AssertsModifier != nil) {
        p.emitTokenNode(node.AssertsModifier);
        p.writeSpace();
    }
    Ꮡp.emitTypePredicateParameterName(node.ParameterName);
    if (node.Type != nil) {
        p.writeSpace();
        p.writeKeyword("is");
        p.writeSpace();
        Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    }
    p.exitNode(Ꮡnode.of(ast.TypePredicateNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeArgument(this ж<Printer> Ꮡp, ж<astꓸTypeNode> node) {
    Ꮡp.emitTypeNodeOutsideExtends(node);
}

internal static void emitTypeArguments(this ж<Printer> Ꮡp, ж<ast.Node> parentNode, ж<astꓸTypeArgumentList> nodes) {
    if (nodes == nil) {
        return;
    }
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTypeParameterDeclarationNode>>)(printerᴛPrinter.emitTypeParameterDeclarationNode)), parentNode, nodes, LFTypeArguments);
}

internal static void emitTypeReference(this ж<Printer> Ꮡp, ж<ast.TypeReferenceNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeReferenceNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEntityName(node.TypeName);
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.TypeReferenceNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    p.exitNode(Ꮡnode.of(ast.TypeReferenceNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitReturnType(this ж<Printer> Ꮡp, ж<astꓸTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }
    p.writePunctuation("=>");
    p.writeSpace();
    if (p.inExtends && node.Kind == ast.KindInferType && node.AsInferTypeNode().TypeParameter.AsTypeParameterDeclaration().Constraint != nil) {
        Ꮡp.emitTypeNodePreservingExtends(Ꮡnode, ast.TypePrecedenceHighest);
    } else {
        Ꮡp.emitTypeNodePreservingExtends(Ꮡnode, ast.TypePrecedenceLowest);
    }
}

internal static void emitFunctionType(this ж<Printer> Ꮡp, ж<ast.FunctionTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTypeParameters(Ꮡnode.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeParameters);
    Ꮡp.emitParameters(Ꮡnode.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Parameters);
    p.writeSpace();
    Ꮡp.emitReturnType(node.Type);
    p.popNameGenerationScope(Ꮡnode.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.FunctionTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitConstructorType(this ж<Printer> Ꮡp, ж<ast.ConstructorTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑModifiersBase).Modifiers(), false);
    p.writeKeyword("new");
    p.writeSpace();
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTypeParameters(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeParameters);
    Ꮡp.emitParameters(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Parameters);
    p.writeSpace();
    Ꮡp.emitReturnType(node.Type);
    p.popNameGenerationScope(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.ConstructorTypeNode.ᏑFunctionOrConstructorTypeNodeBase).of(ast.FunctionOrConstructorTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeQuery(this ж<Printer> Ꮡp, ж<ast.TypeQueryNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeQueryNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeKeyword("typeof");
    p.writeSpace();
    Ꮡp.emitEntityName(node.ExprName);
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.TypeQueryNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    p.exitNode(Ꮡnode.of(ast.TypeQueryNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeLiteral(this ж<Printer> Ꮡp, ж<ast.TypeLiteralNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeLiteralNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.pushNameGenerationScope(Ꮡnode.of(ast.TypeLiteralNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateAllMemberNames(node.Members);
    p.writePunctuation("{");
    ListFormat flags = core.IfElse(p.shouldEmitOnSingleLine(Ꮡnode.of(ast.TypeLiteralNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), LFSingleLineTypeLiteralMembers, LFMultiLineTypeLiteralMembers);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTypeElement>>)(printerᴛPrinter.emitTypeElement)), Ꮡnode.of(ast.TypeLiteralNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Members, (ListFormat)(flags | LFNoSpaceIfEmpty));
    p.writePunctuation("}");
    p.popNameGenerationScope(Ꮡnode.of(ast.TypeLiteralNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.TypeLiteralNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitArrayType(this ж<Printer> Ꮡp, ж<ast.ArrayTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ArrayTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitPostfixTypeOperand(node.ElementType, Ꮡnode.of(ast.ArrayTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("[");
    p.writePunctuation("]");
    p.exitNode(Ꮡnode.of(ast.ArrayTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitPostfixTypeOperand(this ж<Printer> Ꮡp, ж<astꓸTypeNode> Ꮡoperand, ж<ast.Node> parent) {
    ref var operand = ref Ꮡoperand.DerefOrNull();

    if (ast.IsParseTreeNode(parent) && operand.Kind == ast.KindTypeQuery) {
        Ꮡp.emitTypeNode(Ꮡoperand, ast.TypePrecedenceTypeOperator);
        return;
    }
    Ꮡp.emitTypeNode(Ꮡoperand, ast.TypePrecedencePostfix);
}

internal static void emitTupleElementType(this ж<Printer> Ꮡp, ж<ast.Node> node) {
    Ꮡp.emitTypeNodeOutsideExtends(node);
}

internal static void emitTupleType(this ж<Printer> Ꮡp, ж<ast.TupleTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TupleTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindOpenBracketToken, Ꮡnode.of(ast.TupleTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.TupleTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    ListFormat flags = core.IfElse(p.shouldEmitOnSingleLine(Ꮡnode.of(ast.TupleTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), LFSingleLineTupleTypeElements, LFMultiLineTupleTypeElements);
    Ꮡp.emitList(((Action<ж<Printer>, ж<ast.Node>>)(printerᴛPrinter.emitTupleElementType)), Ꮡnode.of(ast.TupleTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Elements, (ListFormat)(flags | LFNoSpaceIfEmpty));
    p.emitToken(ast.KindCloseBracketToken, node.Elements.End(), WriteKindPunctuation, Ꮡnode.of(ast.TupleTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.TupleTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitRestType(this ж<Printer> Ꮡp, ж<ast.RestTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.RestTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("...");
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    p.exitNode(Ꮡnode.of(ast.RestTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitOptionalType(this ж<Printer> Ꮡp, ж<ast.OptionalTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.OptionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitPostfixTypeOperand(node.Type, Ꮡnode.of(ast.OptionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("?");
    p.exitNode(Ꮡnode.of(ast.OptionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNamedTupleMember(this ж<Printer> Ꮡp, ж<ast.NamedTupleMember> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NamedTupleMember.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitPunctuationNode(node.DotDotDotToken);
    Ꮡp.emitIdentifierName(node.Name().AsIdentifier());
    p.emitPunctuationNode(node.QuestionToken);
    p.emitToken(ast.KindColonToken, greatestEnd(node.Name().End(), new ast_NodeжtryGetEnd_node(node.QuestionToken)), WriteKindPunctuation, Ꮡnode.of(ast.NamedTupleMember.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    p.exitNode(Ꮡnode.of(ast.NamedTupleMember.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitUnionTypeConstituent(this ж<Printer> Ꮡp, ж<astꓸTypeNode> node) {
    Ꮡp.emitTypeNode(node, ast.TypePrecedenceTypeOperator);
}

internal static void emitUnionType(this ж<Printer> Ꮡp, ж<ast.UnionTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.UnionTypeNode.ᏑUnionOrIntersectionTypeNodeBase).of(ast.UnionOrIntersectionTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTypeNode>>)(printerᴛPrinter.emitUnionTypeConstituent)), Ꮡnode.of(ast.UnionTypeNode.ᏑUnionOrIntersectionTypeNodeBase).of(ast.UnionOrIntersectionTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Types, LFUnionTypeConstituents);
    p.exitNode(Ꮡnode.of(ast.UnionTypeNode.ᏑUnionOrIntersectionTypeNodeBase).of(ast.UnionOrIntersectionTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitIntersectionTypeConstituent(this ж<Printer> Ꮡp, ж<astꓸTypeNode> node) {
    Ꮡp.emitTypeNode(node, ast.TypePrecedenceTypeOperator);
}

internal static void emitIntersectionType(this ж<Printer> Ꮡp, ж<ast.IntersectionTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.IntersectionTypeNode.ᏑUnionOrIntersectionTypeNodeBase).of(ast.UnionOrIntersectionTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTypeNode>>)(printerᴛPrinter.emitIntersectionTypeConstituent)), Ꮡnode.of(ast.IntersectionTypeNode.ᏑUnionOrIntersectionTypeNodeBase).of(ast.UnionOrIntersectionTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Types, LFIntersectionTypeConstituents);
    p.exitNode(Ꮡnode.of(ast.IntersectionTypeNode.ᏑUnionOrIntersectionTypeNodeBase).of(ast.UnionOrIntersectionTypeNodeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitConditionalType(this ж<Printer> Ꮡp, ж<ast.ConditionalTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ConditionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTypeNode(node.CheckType, ast.TypePrecedenceUnion);
    p.writeSpace();
    p.writeKeyword("extends");
    p.writeSpace();
    Ꮡp.emitTypeNodeInExtends(node.ExtendsType);
    p.writeSpace();
    p.writePunctuation("?");
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node.TrueType);
    p.writeSpace();
    p.writePunctuation(":");
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node.FalseType);
    p.exitNode(Ꮡnode.of(ast.ConditionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitInferTypeParameter(this ж<Printer> Ꮡp, ж<ast.TypeParameterDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    if (node.Constraint != nil) {
        p.writeSpace();
        p.writeKeyword("extends");
        p.writeSpace();
        Ꮡp.emitTypeNodeInExtends(node.Constraint);
    }
    p.exitNode(Ꮡnode.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitInferType(this ж<Printer> Ꮡp, ж<ast.InferTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.InferTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeKeyword("infer");
    p.writeSpace();
    Ꮡp.emitInferTypeParameter(node.TypeParameter.AsTypeParameterDeclaration());
    p.exitNode(Ꮡnode.of(ast.InferTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitParenthesizedType(this ж<Printer> Ꮡp, ж<ast.ParenthesizedTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ParenthesizedTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("(");
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    p.writePunctuation(")");
    p.exitNode(Ꮡnode.of(ast.ParenthesizedTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitThisType(this ref Printer p, ж<ast.ThisTypeNode> node) {
    var state = p.enterNode(node.of(ast.ThisTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeKeyword("this");
    p.exitNode(node.of(ast.ThisTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeOperator(this ж<Printer> Ꮡp, ж<ast.TypeOperatorNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(node.Operator, Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitTypeNode(node.Type, core.IfElse(node.Operator == ast.KindReadonlyKeyword, ast.TypePrecedencePostfix, ast.TypePrecedenceTypeOperator));
    p.exitNode(Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitIndexedAccessType(this ж<Printer> Ꮡp, ж<ast.IndexedAccessTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.IndexedAccessTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitPostfixTypeOperand(node.ObjectType, Ꮡnode.of(ast.IndexedAccessTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("[");
    Ꮡp.emitTypeNodeOutsideExtends(node.IndexType);
    p.writePunctuation("]");
    p.exitNode(Ꮡnode.of(ast.IndexedAccessTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitMappedTypeParameter(this ж<Printer> Ꮡp, ж<ast.TypeParameterDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    p.writeSpace();
    p.writeKeyword("in");
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node.Constraint);
    p.exitNode(Ꮡnode.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitMappedType(this ж<Printer> Ꮡp, ж<ast.MappedTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.MappedTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var singleLine = p.shouldEmitOnSingleLine(Ꮡnode.of(ast.MappedTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("{");
    if (singleLine) {
        p.writeSpace();
    } else {
        p.writeLine();
        p.increaseIndent();
    }
    if (node.ReadonlyToken != nil) {
        p.emitTokenNode(node.ReadonlyToken);
        if (node.ReadonlyToken.Kind != ast.KindReadonlyKeyword) {
            p.writeKeyword("readonly");
        }
        p.writeSpace();
    }
    p.writePunctuation("[");
    Ꮡp.emitMappedTypeParameter(node.TypeParameter.AsTypeParameterDeclaration());
    if (node.NameType != nil) {
        p.writeSpace();
        p.writeKeyword("as");
        p.writeSpace();
        Ꮡp.emitTypeNodeOutsideExtends(node.NameType);
    }
    p.writePunctuation("]");
    if (node.QuestionToken != nil) {
        p.emitPunctuationNode(node.QuestionToken);
        if (node.QuestionToken.Kind != ast.KindQuestionToken) {
            p.writePunctuation("?");
        }
    }
    if (node.Type != nil) {
        p.writePunctuation(":");
        p.writeSpace();
        Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    }
    p.writeTrailingSemicolon();
    if (node.Members != nil) {
        if (len(node.Members.Nodes) > 0) {
            if (singleLine) {
                p.writeSpace();
            } else {
                p.writeLine();
            }
            Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTypeElement>>)(printerᴛPrinter.emitTypeElement)), Ꮡnode.of(ast.MappedTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Members, LFPreserveLines);
        }
    }
    if (singleLine) {
        p.writeSpace();
    } else {
        p.writeLine();
        p.decreaseIndent();
    }
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.MappedTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitLiteralType(this ж<Printer> Ꮡp, ж<ast.LiteralTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.LiteralTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Literal, ast.OperatorPrecedenceComma);
    p.exitNode(Ꮡnode.of(ast.LiteralTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateTypeSpan(this ж<Printer> Ꮡp, ж<ast.TemplateLiteralTypeSpan> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TemplateLiteralTypeSpan.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    Ꮡp.emitTemplateMiddleTail(node.Literal);
    p.exitNode(Ꮡnode.of(ast.TemplateLiteralTypeSpan.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateTypeSpanNode(this ж<Printer> Ꮡp, ж<astꓸTemplateLiteralTypeSpanNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitTemplateTypeSpan(node.AsTemplateLiteralTypeSpan());
}

internal static void emitTemplateType(this ж<Printer> Ꮡp, ж<ast.TemplateLiteralTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TemplateLiteralTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTemplateHead(node.Head.AsTemplateHead());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTemplateLiteralTypeSpanNode>>)(printerᴛPrinter.emitTemplateTypeSpanNode)), Ꮡnode.of(ast.TemplateLiteralTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TemplateSpans, LFTemplateExpressionSpans);
    p.exitNode(Ꮡnode.of(ast.TemplateLiteralTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string assertˢ = "assert"u8;
internal static readonly @string withˢ = "with"u8;

internal static void emitImportTypeNodeAttributes(this ж<Printer> Ꮡp, ж<ast.ImportAttributes> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("{");
    p.writeSpace();
    p.writeKeyword(core.IfElse(node.Token == ast.KindAssertKeyword, assertˢ, withˢ));
    p.writePunctuation(":");
    p.writeSpace();
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸImportAttributeNode>>)(printerᴛPrinter.emitImportAttributeNode)), Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Attributes, LFImportAttributes);
    p.writeSpace();
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitImportTypeNode(this ж<Printer> Ꮡp, ж<ast.ImportTypeNode> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportTypeNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.IsTypeOf) {
        p.writeKeyword("typeof");
        p.writeSpace();
    }
    p.writeKeyword("import");
    p.writePunctuation("(");
    Ꮡp.emitTypeNodeOutsideExtends(node.Argument);
    if (node.Attributes != nil) {
        p.writePunctuation(",");
        p.writeSpace();
        Ꮡp.emitImportTypeNodeAttributes(node.Attributes.AsImportAttributes());
    }
    p.writePunctuation(")");
    if (node.Qualifier != nil) {
        p.writePunctuation(".");
        Ꮡp.emitEntityName(node.Qualifier);
    }
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.ImportTypeNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    p.exitNode(Ꮡnode.of(ast.ImportTypeNode.ᏑNodeWithTypeArgumentsBase).of(ast.NodeWithTypeArgumentsBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeNodeInExtends(this ж<Printer> Ꮡp, ж<astꓸTypeNode> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var savedInExtends = p.inExtends;
    p.inExtends = true;
    Ꮡp.emitTypeNodePreservingExtends(node, ast.TypePrecedenceLowest);
    p.inExtends = savedInExtends;
}

internal static void emitTypeNodeOutsideExtends(this ж<Printer> Ꮡp, ж<astꓸTypeNode> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var savedInExtends = p.inExtends;
    p.inExtends = false;
    Ꮡp.emitTypeNodePreservingExtends(node, ast.TypePrecedenceLowest);
    p.inExtends = savedInExtends;
}

internal static void emitTypeNodePreservingExtends(this ж<Printer> Ꮡp, ж<astꓸTypeNode> node, ast.TypePrecedence precedence) {
    Ꮡp.emitTypeNode(node, precedence);
}

internal static void emitTypeNode(this ж<Printer> Ꮡp, ж<astꓸTypeNode> Ꮡnode, ast.TypePrecedence precedence) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (p.inExtends && precedence <= ast.TypePrecedenceConditional) {
        precedence = ast.TypePrecedenceFunction;
    }

    var savedInExtends = p.inExtends;
    var parens = ast.GetTypeNodePrecedence(Ꮡnode) < precedence;
    if (parens) {
        p.inExtends = false;
        p.writePunctuation("(");
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindAnyKeyword || ᴛ == ast.KindUnknownKeyword || ᴛ == ast.KindNumberKeyword || ᴛ == ast.KindBigIntKeyword || ᴛ == ast.KindObjectKeyword || ᴛ == ast.KindBooleanKeyword || ᴛ == ast.KindStringKeyword || ᴛ == ast.KindSymbolKeyword || ᴛ == ast.KindVoidKeyword || ᴛ == ast.KindUndefinedKeyword || ᴛ == ast.KindNeverKeyword || ᴛ == ast.KindIntrinsicKeyword: {
        p.emitKeywordTypeNode(node.AsKeywordTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypePredicate: {
        Ꮡp.emitTypePredicate(node.AsTypePredicateNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeReference: {
        Ꮡp.emitTypeReference(node.AsTypeReferenceNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionType: {
        Ꮡp.emitFunctionType(node.AsFunctionTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructorType: {
        Ꮡp.emitConstructorType(node.AsConstructorTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeQuery: {
        Ꮡp.emitTypeQuery(node.AsTypeQueryNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeLiteral: {
        Ꮡp.emitTypeLiteral(node.AsTypeLiteralNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrayType: {
        Ꮡp.emitArrayType(node.AsArrayTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTupleType: {
        Ꮡp.emitTupleType(node.AsTupleTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindOptionalType: {
        Ꮡp.emitOptionalType(node.AsOptionalTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindRestType: {
        Ꮡp.emitRestType(node.AsRestTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindUnionType: {
        Ꮡp.emitUnionType(node.AsUnionTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIntersectionType: {
        Ꮡp.emitIntersectionType(node.AsIntersectionTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindConditionalType: {
        Ꮡp.emitConditionalType(node.AsConditionalTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindInferType: {
        Ꮡp.emitInferType(node.AsInferTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedType: {
        Ꮡp.emitParenthesizedType(node.AsParenthesizedTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindThisType: {
        p.emitThisType(node.AsThisTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeOperator: {
        Ꮡp.emitTypeOperator(node.AsTypeOperatorNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIndexedAccessType: {
        Ꮡp.emitIndexedAccessType(node.AsIndexedAccessTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMappedType: {
        Ꮡp.emitMappedType(node.AsMappedTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindLiteralType: {
        Ꮡp.emitLiteralType(node.AsLiteralTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamedTupleMember: {
        Ꮡp.emitNamedTupleMember(node.AsNamedTupleMember());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateLiteralType: {
        Ꮡp.emitTemplateType(node.AsTemplateLiteralTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateLiteralTypeSpan: {
        Ꮡp.emitTemplateTypeSpan(node.AsTemplateLiteralTypeSpan());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportType: {
        Ꮡp.emitImportTypeNode(node.AsImportTypeNode());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        Ꮡp.emitPropertyAccessExpression(node.AsPropertyAccessExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExpressionWithTypeArguments: {
        Ꮡp.emitExpressionWithTypeArguments(node.AsExpressionWithTypeArguments());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocAllType: {
        p.emitJSDocAllType(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocNonNullableType: {
        Ꮡp.emitJSDocNonNullableType(node.AsJSDocNonNullableType());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocNullableType: {
        Ꮡp.emitJSDocNullableType(node.AsJSDocNullableType());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocOptionalType: {
        Ꮡp.emitJSDocOptionalType(node.AsJSDocOptionalType());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocVariadicType: {
        Ꮡp.emitJSDocVariadicType(node.AsJSDocVariadicType());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled TypeNode: %v"u8, node.Kind));
        break;
    }}

    if (parens) {
        p.writePunctuation(")");
    }

    p.inExtends = savedInExtends;
}

internal static void emitObjectBindingPattern(this ж<Printer> Ꮡp, ж<ast.BindingPattern> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("{");
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸBindingElementNode>>)(printerᴛPrinter.emitBindingElementNode)), Ꮡnode.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Elements, LFObjectBindingPatternElements);
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitArrayBindingPattern(this ж<Printer> Ꮡp, ж<ast.BindingPattern> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("[");
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸBindingElementNode>>)(printerᴛPrinter.emitBindingElementNode)), Ꮡnode.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Elements, LFArrayBindingPatternElements);
    p.writePunctuation("]");
    p.exitNode(Ꮡnode.of(ast.BindingPattern.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitBindingElement(this ж<Printer> Ꮡp, ж<ast.BindingElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitTokenNode(node.DotDotDotToken);
    if (node.PropertyName != nil) {
        Ꮡp.emitPropertyName(node.PropertyName);
        p.writePunctuation(":");
        p.writeSpace();
    }
    var name = node.Name(); if (name != nil) {
        Ꮡp.emitBindingName(name);
        Ꮡp.emitInitializer(node.Initializer, node.Name().End(), Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    p.exitNode(Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitBindingElementNode(this ж<Printer> Ꮡp, ж<astꓸBindingElementNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitBindingElement(node.AsBindingElement());
}

internal static void emitJSDocAllType(this ref Printer p, ж<ast.Node> node) {
    p.emitKeywordNode(node);
}

internal static void emitJSDocNonNullableType(this ж<Printer> Ꮡp, ж<ast.JSDocNonNullableType> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JSDocNonNullableType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("!");
    Ꮡp.emitTypeNode(node.Type, ast.TypePrecedenceNonArray);
    p.exitNode(Ꮡnode.of(ast.JSDocNonNullableType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJSDocNullableType(this ж<Printer> Ꮡp, ж<ast.JSDocNullableType> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JSDocNullableType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("?");
    Ꮡp.emitTypeNode(node.Type, ast.TypePrecedenceNonArray);
    p.exitNode(Ꮡnode.of(ast.JSDocNullableType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJSDocOptionalType(this ж<Printer> Ꮡp, ж<ast.JSDocOptionalType> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JSDocOptionalType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTypeNode(node.Type, ast.TypePrecedenceJSDoc);
    p.writePunctuation("=");
    p.exitNode(Ꮡnode.of(ast.JSDocOptionalType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJSDocVariadicType(this ж<Printer> Ꮡp, ж<ast.JSDocVariadicType> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JSDocVariadicType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("...");
    Ꮡp.emitTypeNode(node.Type, ast.TypePrecedenceJSDoc);
    p.exitNode(Ꮡnode.of(ast.JSDocVariadicType.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitKeywordExpression(this ref Printer p, ж<ast.KeywordExpression> node) {
    p.emitKeywordNode(node.of(ast.KeywordExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static void emitArrayLiteralExpressionElement(this ж<Printer> Ꮡp, ж<astꓸExpression> node) {
    Ꮡp.emitExpression(node, ast.OperatorPrecedenceSpread);
}

internal static void emitArrayLiteralExpression(this ж<Printer> Ꮡp, ж<ast.ArrayLiteralExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸExpression>>)(printerᴛPrinter.emitArrayLiteralExpressionElement)), Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Elements, (ListFormat)(LFArrayLiteralExpressionElements | core.IfElse(node.MultiLine, LFPreferNewLine, LFNone)));
    p.exitNode(Ꮡnode.of(ast.ArrayLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitObjectLiteralExpression(this ж<Printer> Ꮡp, ж<ast.ObjectLiteralExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateAllMemberNames(node.Properties);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸObjectLiteralElement>>)(printerᴛPrinter.emitObjectLiteralElement)), Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Properties, (ListFormat)((ListFormat)(LFObjectLiteralExpressionProperties | core.IfElse(node.MultiLine, LFPreferNewLine, LFNone)) | core.IfElse(p.shouldAllowTrailingComma(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Properties), LFAllowTrailingComma, LFNone)));
    p.popNameGenerationScope(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.ObjectLiteralExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static bool mayNeedDotDotForPropertyAccess(this ж<Printer> Ꮡp, ж<astꓸExpression> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    Ꮡexpression = ast.SkipPartiallyEmittedExpressions(Ꮡexpression); expression = ref Ꮡexpression.DerefOrNull();
    if (ast.IsNumericLiteral(Ꮡexpression)) {
        @string text = Ꮡp.getLiteralTextOfNode(Ꮡexpression, nil, getLiteralTextFlagsNeverAsciiEscape);
        return (ast.TokenFlags)(expression.AsNumericLiteral().TokenFlags & ast.TokenFlagsWithSpecifier) == 0 && !strings.Contains(text, scanner.TokenToString(ast.KindDotToken)) && !strings.Contains(text, "E") && !strings.Contains(text, "e");
    }
    return false;
}

internal static void emitPropertyAccessExpression(this ж<Printer> Ꮡp, ж<ast.PropertyAccessExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, core.IfElse(ast.IsOptionalChain(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), ast.OperatorPrecedenceOptionalChain, ast.OperatorPrecedenceMember));
    var token = node.QuestionDotToken;
    if (token == nil) {
        token = p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindDotToken);
        token.Loc = core.NewTextRange(node.Expression.End(), node.Name().Pos());
        p.emitContext.AddEmitFlags(token, EFNoSourceMap);
    }
    nint linesBeforeDot = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Expression, token);
    p.writeLineRepeat(linesBeforeDot);
    p.increaseIndentIf(linesBeforeDot > 0);
    var shouldEmitDotDot = token.Kind != ast.KindQuestionDotToken && Ꮡp.mayNeedDotDotForPropertyAccess(node.Expression) && !p.writer.HasTrailingComment() && !p.writer.HasTrailingWhitespace();
    if (shouldEmitDotDot) {
        p.writePunctuation(".");
    }
    if (node.QuestionDotToken != nil) {
        p.emitTokenNode(token);
    } else {
        p.emitToken(ast.KindDotToken, node.Expression.End(), WriteKindPunctuation, Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    nint linesAfterDot = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), token, node.Name());
    p.writeLineRepeat(linesAfterDot);
    p.increaseIndentIf(linesAfterDot > 0);
    Ꮡp.emitMemberName(node.Name());
    p.decreaseIndentIf(linesAfterDot > 0);
    p.decreaseIndentIf(linesBeforeDot > 0);
    p.exitNode(Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitElementAccessExpression(this ж<Printer> Ꮡp, ж<ast.ElementAccessExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, core.IfElse(ast.IsOptionalChain(Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()), ast.OperatorPrecedenceOptionalChain, ast.OperatorPrecedenceMember));
    p.emitTokenNode(node.QuestionDotToken);
    p.emitToken(ast.KindOpenBracketToken, greatestEnd(-1, new ast_NodeжtryGetEnd_node(node.Expression), new ast_NodeжtryGetEnd_node(node.QuestionDotToken)), WriteKindPunctuation, Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.ArgumentExpression, ast.OperatorPrecedenceComma);
    p.emitToken(ast.KindCloseBracketToken, node.ArgumentExpression.End(), WriteKindPunctuation, Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.ElementAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitArgument(this ж<Printer> Ꮡp, ж<astꓸExpression> node) {
    Ꮡp.emitExpression(node, ast.OperatorPrecedenceSpread);
}

internal static void emitCallee(this ж<Printer> Ꮡp, ж<astꓸExpression> callee, ж<ast.Node> ᏑparentNode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var parentNode = ref ᏑparentNode.DerefOrNull();

    if (p.shouldEmitIndirectCall(ᏑparentNode)) {
        p.writePunctuation("(");
        p.writeLiteral("0");
        p.writePunctuation(",");
        p.writeSpace();
        Ꮡp.emitExpression(callee, ast.OperatorPrecedenceComma);
        p.writePunctuation(")");
    } else if (parentNode.Kind == ast.KindCallExpression && isNewExpressionWithoutArguments(ast.SkipPartiallyEmittedExpressions(callee))) {
        Ꮡp.emitExpression(callee, ast.OperatorPrecedenceParentheses);
    } else {
        Ꮡp.emitExpression(callee, core.IfElse(ast.IsOptionalChain(ᏑparentNode), ast.OperatorPrecedenceOptionalChain, ast.OperatorPrecedenceMember));
    }
}

internal static void emitCallExpression(this ж<Printer> Ꮡp, ж<ast.CallExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitCallee(node.Expression, Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitTokenNode(node.QuestionDotToken);
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸExpression>>)(printerᴛPrinter.emitArgument)), Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Arguments, LFCallExpressionArguments);
    p.exitNode(Ꮡnode.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNewExpression(this ж<Printer> Ꮡp, ж<ast.NewExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NewExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindNewKeyword, Ꮡnode.of(ast.NewExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.NewExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    if (ast.SkipPartiallyEmittedExpressions(node.Expression).Kind == ast.KindCallExpression) {
        Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceParentheses);
    } else {
        Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceMember);
    }
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.NewExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸExpression>>)(printerᴛPrinter.emitArgument)), Ꮡnode.of(ast.NewExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Arguments, LFNewExpressionArguments);
    p.exitNode(Ꮡnode.of(ast.NewExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateLiteral(this ж<Printer> Ꮡp, ж<astꓸTemplateLiteral> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindNoSubstitutionTemplateLiteral: {
        Ꮡp.emitNoSubstitutionTemplateLiteral(node.AsNoSubstitutionTemplateLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateExpression: {
        Ꮡp.emitTemplateExpression(node.AsTemplateExpression());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled TemplateLiteral: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitTaggedTemplateExpression(this ж<Printer> Ꮡp, ж<ast.TaggedTemplateExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitCallee(node.Tag, Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    p.writeSpace();
    Ꮡp.emitTemplateLiteral(node.Template);
    p.exitNode(Ꮡnode.of(ast.TaggedTemplateExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeAssertionExpression(this ж<Printer> Ꮡp, ж<ast.TypeAssertion> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeAssertion.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("<");
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    p.writePunctuation(">");
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceUpdate);
    p.exitNode(Ꮡnode.of(ast.TypeAssertion.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitParenthesizedExpression(this ж<Printer> Ꮡp, ж<ast.ParenthesizedExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint openParenPos = p.emitToken(ast.KindOpenParenToken, Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var indented = Ꮡp.writeLineSeparatorsAndIndentBefore(node.Expression, Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceComma);
    Ꮡp.writeLineSeparatorsAfter(node.Expression, Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    nint closeParenPos = openParenPos;
    if (node.Expression != nil) {
        closeParenPos = node.Expression.End();
    }
    p.emitToken(ast.KindCloseParenToken, closeParenPos, WriteKindPunctuation, Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.ParenthesizedExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitFunctionExpression(this ж<Printer> Ꮡp, ж<ast.FunctionExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateNameIfNeeded(node.Name());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.FunctionExpression.ᏑModifiersBase).Modifiers(), false);
    p.writeKeyword("function");
    p.emitTokenNode(node.AsteriskToken);
    p.writeSpace();
    Ꮡp.emitIdentifierNameNode(node.Name());
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitFunctionBodyNode(node.Body);
    p.popNameGenerationScope(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.FunctionExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitConciseBody(this ж<Printer> Ꮡp, ж<astꓸBlockOrExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (ᐧ) {
    case {} when ast.IsBlock(Ꮡnode): {
        Ꮡp.emitFunctionBody(node.AsBlock());
        break;
    }
    case {} when ast.IsObjectLiteralExpression(ast.GetLeftmostExpression(Ꮡnode, false)): {
        var paren = p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression(Ꮡnode);
        paren.Loc = node.Loc;
        Ꮡp.emitExpression(paren, ast.OperatorPrecedenceLowest);
        break;
    }
    case {} when ast.IsExpression(Ꮡnode): {
        Ꮡp.emitExpression(Ꮡnode, ast.OperatorPrecedenceYield);
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected ConciseBody: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitArrowFunction(this ж<Printer> Ꮡp, ж<ast.ArrowFunction> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ArrowFunction.ᏑModifiersBase).Modifiers(), false);
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTypeParameters(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeParameters);
    Ꮡp.emitParametersForArrow(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Parameters);
    Ꮡp.emitTypeAnnotation(node.Type);
    p.writeSpace();
    p.emitTokenNode(node.EqualsGreaterThanToken);
    p.writeSpace();
    Ꮡp.emitConciseBody(node.Body);
    p.popNameGenerationScope(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.ArrowFunction.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitDeleteExpression(this ж<Printer> Ꮡp, ж<ast.DeleteExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.DeleteExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindDeleteKeyword, Ꮡnode.of(ast.DeleteExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.DeleteExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceUnary);
    p.exitNode(Ꮡnode.of(ast.DeleteExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeOfExpression(this ж<Printer> Ꮡp, ж<ast.TypeOfExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeOfExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindTypeOfKeyword, Ꮡnode.of(ast.TypeOfExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.TypeOfExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceUnary);
    p.exitNode(Ꮡnode.of(ast.TypeOfExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitVoidExpression(this ж<Printer> Ꮡp, ж<ast.VoidExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.VoidExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindVoidKeyword, Ꮡnode.of(ast.VoidExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.VoidExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceUnary);
    p.exitNode(Ꮡnode.of(ast.VoidExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitAwaitExpression(this ж<Printer> Ꮡp, ж<ast.AwaitExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindAwaitKeyword, Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceUnary);
    p.exitNode(Ꮡnode.of(ast.AwaitExpression.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitPrefixUnaryExpression(this ж<Printer> Ꮡp, ж<ast.PrefixUnaryExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var @operator = node.Operator;
    var operand = node.Operand;
    p.emitToken(@operator, Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindOperator, Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    if (operand.Kind == ast.KindPrefixUnaryExpression) {
        var inner = operand.AsPrefixUnaryExpression().Operator;
        if ((@operator == ast.KindPlusToken && (inner == ast.KindPlusToken || inner == ast.KindPlusPlusToken)) || (@operator == ast.KindMinusToken && (inner == ast.KindMinusToken || inner == ast.KindMinusMinusToken))) {
            p.writeSpace();
        }
    }

    Ꮡp.emitExpression(node.Operand, ast.OperatorPrecedenceUnary);
    p.exitNode(Ꮡnode.of(ast.PrefixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitPostfixUnaryExpression(this ж<Printer> Ꮡp, ж<ast.PostfixUnaryExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Operand, ast.OperatorPrecedenceLeftHandSide);
    p.emitToken(node.Operator, node.Operand.End(), WriteKindOperator, Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.PostfixUnaryExpression.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static ast.Kind getLiteralKindOfBinaryPlusOperand(this ref Printer p, ж<astꓸExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = ast.SkipPartiallyEmittedExpressions(Ꮡnode); node = ref Ꮡnode.DerefOrNull();

    if (ast.IsLiteralKind(node.Kind)) {
        return node.Kind;
    }

    if (node.Kind == ast.KindBinaryExpression) {
        var n = node.AsBinaryExpression(); if (n.OperatorToken.Kind == ast.KindPlusToken) {
            var leftKind = p.getLiteralKindOfBinaryPlusOperand(n.Left);
            var literalKind = ast.KindUnknown;
            if (ast.IsLiteralKind(leftKind) && leftKind == p.getLiteralKindOfBinaryPlusOperand(n.Right)) {
                literalKind = leftKind;
            }

            return literalKind;
        }
    }

    return ast.KindUnknown;
}

internal static (ast.OperatorPrecedence leftPrec, ast.OperatorPrecedence rightPrec) getBinaryExpressionPrecedence(this ref Printer p, ж<ast.BinaryExpression> Ꮡnode) {
    ast.OperatorPrecedence leftPrec = default;
    ast.OperatorPrecedence rightPrec = default;
    ref var node = ref Ꮡnode.DerefOrNull();

    ast.OperatorPrecedence precedence = ast.GetExpressionPrecedence(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    leftPrec = precedence;
    rightPrec = precedence;
    switch (precedence) {
    case var ᴛ when ᴛ == ast.OperatorPrecedenceComma: {
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceAssignment: {
        leftPrec = ast.OperatorPrecedenceConditional;
        rightPrec = ast.OperatorPrecedenceYield;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceLogicalOR: {
        rightPrec = ast.OperatorPrecedenceLogicalAND;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceLogicalAND: {
        rightPrec = ast.OperatorPrecedenceBitwiseOR;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceBitwiseOR: {
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceBitwiseXOR: {
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceBitwiseAND: {
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceEquality: {
        rightPrec = ast.OperatorPrecedenceRelational;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceRelational: {
        rightPrec = ast.OperatorPrecedenceShift;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceShift: {
        rightPrec = ast.OperatorPrecedenceAdditive;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceAdditive: {
        if (node.OperatorToken.Kind == ast.KindPlusToken && isBinaryOperation(node.Right, ast.KindPlusToken)) {
            var leftKind = p.getLiteralKindOfBinaryPlusOperand(node.Left);
            if (ast.IsLiteralKind(leftKind) && leftKind == p.getLiteralKindOfBinaryPlusOperand(node.Right)) {
                break;
            }
        }
        rightPrec = ast.OperatorPrecedenceMultiplicative;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceMultiplicative: {
        if (node.OperatorToken.Kind == ast.KindAsteriskToken && isBinaryOperation(node.Right, ast.KindAsteriskToken)) {
            break;
        }
        rightPrec = ast.OperatorPrecedenceExponentiation;
        break;
    }
    case var ᴛ when ᴛ == ast.OperatorPrecedenceExponentiation: {
        leftPrec = ast.OperatorPrecedenceUpdate;
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled precedence: %v"u8, precedence));
        break;
    }}

    return (leftPrec, rightPrec);
}

internal static void emitBinaryExpression(this ж<Printer> Ꮡp, ж<ast.BinaryExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var (leftPrec, rightPrec) = p.getBinaryExpressionPrecedence(Ꮡnode);
    var emittedLeft = ast.SkipPartiallyEmittedExpressions(node.Left); if (ast.NodeIsSynthesized(emittedLeft) && emittedLeft.Kind == ast.KindBinaryExpression && mixingBinaryOperatorsRequiresParentheses(node.OperatorToken.Kind, emittedLeft.AsBinaryExpression().OperatorToken.Kind)) {
        leftPrec = ast.OperatorPrecedenceHighest;
    }
    var emittedRight = ast.SkipPartiallyEmittedExpressions(node.Right); if (ast.NodeIsSynthesized(emittedRight) && emittedRight.Kind == ast.KindBinaryExpression && mixingBinaryOperatorsRequiresParentheses(node.OperatorToken.Kind, emittedRight.AsBinaryExpression().OperatorToken.Kind)) {
        rightPrec = ast.OperatorPrecedenceHighest;
    }
    var state = p.enterNode(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Left, leftPrec);
    nint linesBeforeOperator = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Left, node.OperatorToken);
    nint linesAfterOperator = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.OperatorToken, node.Right);
    p.writeLinesAndIndent(linesBeforeOperator, node.OperatorToken.Kind != ast.KindCommaToken);
    p.emitTokenNodeEx(node.OperatorToken, tefNoSourceMaps);
    p.writeLinesAndIndent(linesAfterOperator, true);
    Ꮡp.emitExpression(node.Right, rightPrec);
    p.decreaseIndentIf(linesAfterOperator > 0);
    p.decreaseIndentIf(linesBeforeOperator > 0);
    p.exitNode(Ꮡnode.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitShortCircuitExpression(this ж<Printer> Ꮡp, ж<astꓸExpression> node) {
    if (isBinaryOperation(ast.SkipPartiallyEmittedExpressions(node), ast.KindQuestionQuestionToken)) {
        Ꮡp.emitExpression(node, ast.OperatorPrecedenceCoalesce);
    } else {
        Ꮡp.emitExpression(node, ast.OperatorPrecedenceLogicalOR);
    }
}

internal static void emitConditionalExpression(this ж<Printer> Ꮡp, ж<ast.ConditionalExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ConditionalExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint linesBeforeQuestion = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.ConditionalExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Condition, node.QuestionToken);
    nint linesAfterQuestion = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.ConditionalExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.QuestionToken, node.WhenTrue);
    nint linesBeforeColon = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.ConditionalExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.WhenTrue, node.ColonToken);
    nint linesAfterColon = Ꮡp.getLinesBetweenNodes(Ꮡnode.of(ast.ConditionalExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.ColonToken, node.WhenFalse);
    Ꮡp.emitShortCircuitExpression(node.Condition);
    p.writeLinesAndIndent(linesBeforeQuestion, true);
    p.emitPunctuationNode(node.QuestionToken);
    p.writeLinesAndIndent(linesAfterQuestion, true);
    Ꮡp.emitExpression(node.WhenTrue, ast.OperatorPrecedenceYield);
    p.decreaseIndentIf(linesAfterQuestion > 0);
    p.decreaseIndentIf(linesBeforeQuestion > 0);
    p.writeLinesAndIndent(linesBeforeColon, true);
    p.emitPunctuationNode(node.ColonToken);
    p.writeLinesAndIndent(linesAfterColon, true);
    Ꮡp.emitExpression(node.WhenFalse, ast.OperatorPrecedenceYield);
    p.decreaseIndentIf(linesAfterColon > 0);
    p.decreaseIndentIf(linesBeforeColon > 0);
    p.exitNode(Ꮡnode.of(ast.ConditionalExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateExpression(this ж<Printer> Ꮡp, ж<ast.TemplateExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TemplateExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitTemplateHead(node.Head.AsTemplateHead());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTemplateSpanNode>>)(printerᴛPrinter.emitTemplateSpanNode)), Ꮡnode.of(ast.TemplateExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TemplateSpans, LFTemplateExpressionSpans);
    p.exitNode(Ꮡnode.of(ast.TemplateExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitYieldExpression(this ж<Printer> Ꮡp, ж<ast.YieldExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.YieldExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindYieldKeyword, Ꮡnode.of(ast.YieldExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.YieldExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitPunctuationNode(node.AsteriskToken);
    if (node.Expression != nil) {
        p.writeSpace();
        Ꮡp.emitExpressionNoASI(node.Expression, ast.OperatorPrecedenceDisallowComma);
    }
    p.exitNode(Ꮡnode.of(ast.YieldExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitSpreadElement(this ж<Printer> Ꮡp, ж<ast.SpreadElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.SpreadElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindDotDotDotToken, Ꮡnode.of(ast.SpreadElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.SpreadElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceDisallowComma);
    p.exitNode(Ꮡnode.of(ast.SpreadElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitClassExpression(this ж<Printer> Ꮡp, ж<ast.ClassExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateNameIfNeeded(node.Name());

    nint pos = Ꮡp.emitModifierList(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), true);
    p.emitToken(ast.KindClassKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    if (node.Name() != nil) {
        p.writeSpace();
        Ꮡp.emitIdentifierName(node.Name().AsIdentifier());
    }

    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);

    Ꮡp.emitTypeParameters(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeParameters);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸHeritageClauseNode>>)(printerᴛPrinter.emitHeritageClauseNode)), Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.HeritageClauses, LFClassHeritageClauses);
    p.writeSpace();
    p.writePunctuation("{");
    p.pushNameGenerationScope(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateAllMemberNames(node.Members);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸClassElement>>)(printerᴛPrinter.emitClassElement)), Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Members, LFClassMembers);
    p.popNameGenerationScope(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("}");

    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitOmittedExpression(this ref Printer p, ж<ast.Node> node) {
    p.exitNode(node, p.enterNode(node));
}

internal static void emitExpressionWithTypeArguments(this ж<Printer> Ꮡp, ж<ast.ExpressionWithTypeArguments> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ExpressionWithTypeArguments.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceMember);
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.ExpressionWithTypeArguments.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    p.exitNode(Ꮡnode.of(ast.ExpressionWithTypeArguments.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitAsExpression(this ж<Printer> Ꮡp, ж<ast.AsExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.AsExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceRelational);
    p.writeSpace();
    p.writeKeyword("as");
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    p.exitNode(Ꮡnode.of(ast.AsExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitSatisfiesExpression(this ж<Printer> Ꮡp, ж<ast.SatisfiesExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.SatisfiesExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceRelational);
    p.writeSpace();
    p.writeKeyword("satisfies");
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    p.exitNode(Ꮡnode.of(ast.SatisfiesExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNonNullExpression(this ж<Printer> Ꮡp, ж<ast.NonNullExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NonNullExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceMember);
    p.writeOperator("!");
    p.exitNode(Ꮡnode.of(ast.NonNullExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitMetaProperty(this ж<Printer> Ꮡp, ж<ast.MetaProperty> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.MetaProperty.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(node.KeywordToken, Ꮡnode.of(ast.MetaProperty.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.MetaProperty.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation(".");
    Ꮡp.emitIdentifierName(node.Name().AsIdentifier());
    p.exitNode(Ꮡnode.of(ast.MetaProperty.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

} partial class printer_package {

internal partial struct emitPartiallyEmittedExpression_entry /*dyn*/ {
    internal ж<ast.PartiallyEmittedExpression> node;
    internal printerState state;
}

} partial class printerᴛPrinter {

internal static void emitPartiallyEmittedExpression(this ж<Printer> Ꮡp, ж<ast.PartiallyEmittedExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    core.Stack<emitPartiallyEmittedExpression_entry> stack = default;
    for (;;) {
        var state = p.enterNode(Ꮡnode.of(ast.PartiallyEmittedExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var emitFlags = p.emitContext.EmitFlags(Ꮡnode.of(ast.PartiallyEmittedExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if ((EmitFlags)(emitFlags & EFNoLeadingComments) == 0 && Ꮡnode.of(ast.PartiallyEmittedExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos() != node.Expression.Pos()) {
            p.emitTrailingCommentsOfPosition(node.Expression.Pos(), false, false);
        }
        stack.Push(new emitPartiallyEmittedExpression_entry(Ꮡnode, state));
        if (!ast.IsPartiallyEmittedExpression(node.Expression)) {
            break;
        }
        Ꮡnode = node.Expression.AsPartiallyEmittedExpression(); node = ref Ꮡnode.DerefOrNull();
    }

    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);

    while (stack.Len() > 0) {
        var entry = stack.Pop();
        var emitFlags = p.emitContext.EmitFlags(Ꮡnode.of(ast.PartiallyEmittedExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if ((EmitFlags)(emitFlags & EFNoTrailingComments) == 0 && Ꮡnode.of(ast.PartiallyEmittedExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).End() != node.Expression.End()) {
            p.emitLeadingCommentsOfPosition(node.Expression.End());
        }
        p.exitNode(Ꮡnode.of(ast.PartiallyEmittedExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), entry.state);
        Ꮡnode = entry.node; node = ref Ꮡnode.DerefOrNull();
    }
}

internal static bool commentWillEmitNewLine(this ref Printer p, ast.CommentRange comment) {
    return comment.Kind == ast.KindSingleLineCommentTrivia || comment.HasTrailingNewLine;
}

internal static bool syntheticCommentWillEmitNewLine(this ref Printer p, SynthesizedComment comment) {
    return comment.Kind == ast.KindSingleLineCommentTrivia || comment.HasTrailingNewLine;
}

internal static bool willEmitLeadingNewLine(this ж<Printer> Ꮡp, ж<astꓸExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (p.currentSourceFile == nil) {
        return false;
    }
    var hasLeadingCommentRanges = false;
    var hasNewLineComment = false;
    foreach (var comment in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), p.currentSourceFile.Text(), node.Pos()).Invoke)) {
        hasLeadingCommentRanges = true;
        if (p.commentWillEmitNewLine(comment)) {
            hasNewLineComment = true;
        }
    }
    if (hasLeadingCommentRanges) {
        var parseNode = p.emitContext.ParseNode(Ꮡnode);
        if (parseNode != nil && ast.IsParenthesizedExpression(parseNode.Parent)) {
            return true;
        }
    }
    if (hasNewLineComment) {
        return true;
    }
    if (slices.ContainsFunc<slice<SynthesizedComment>, SynthesizedComment>(p.emitContext.GetSyntheticLeadingComments(Ꮡnode), Ꮡp.syntheticCommentWillEmitNewLine)) {
        return true;
    }
    if (ast.IsPartiallyEmittedExpression(Ꮡnode)) {
        var pee = node.AsPartiallyEmittedExpression();
        if (node.Pos() != (~pee).Expression.Pos()) {
            foreach (var comment in range<ast.CommentRange>(scanner.GetTrailingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), p.currentSourceFile.Text(), (~pee).Expression.Pos()).Invoke)) {
                if (p.commentWillEmitNewLine(comment)) {
                    return true;
                }
            }
        }
        return Ꮡp.willEmitLeadingNewLine((~pee).Expression);
    }
    return false;
}

internal static ж<astꓸExpression> parenthesizeExpressionForNoAsi(this ж<Printer> Ꮡp, ж<astꓸExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!p.commentsDisabled) {
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindPartiallyEmittedExpression: {
            if (Ꮡp.willEmitLeadingNewLine(Ꮡnode)) {
                var peeΔ1 = node.AsPartiallyEmittedExpression();
                var parseNode = p.emitContext.ParseNode(Ꮡnode);
                if (parseNode != nil && ast.IsParenthesizedExpression(parseNode)) {
                    var parens = p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression((~peeΔ1).Expression);
                    p.emitContext.SetOriginal(parens, Ꮡnode);
                    parens.Loc = parseNode.Loc;
                    return parens;
                }
                return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).NewParenthesizedExpression(Ꮡnode);
            }
            var pee = node.AsPartiallyEmittedExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdatePartiallyEmittedExpression(
                pee,
                Ꮡp.parenthesizeExpressionForNoAsi((~pee).Expression));
        }
        case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
            var pae = node.AsPropertyAccessExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdatePropertyAccessExpression(
                pae,
                Ꮡp.parenthesizeExpressionForNoAsi((~pae).Expression),
                (~pae).QuestionDotToken,
                pae.Name(),
                pae.Flags);
        }
        case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
            var eae = node.AsElementAccessExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateElementAccessExpression(
                eae,
                Ꮡp.parenthesizeExpressionForNoAsi((~eae).Expression),
                (~eae).QuestionDotToken,
                eae.ArgumentExpression,
                eae.Flags);
        }
        case var ᴛ when ᴛ == ast.KindCallExpression: {
            var ce = node.AsCallExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateCallExpression(
                ce,
                Ꮡp.parenthesizeExpressionForNoAsi((~ce).Expression),
                (~ce).QuestionDotToken,
                (~ce).TypeArguments,
                (~ce).Arguments,
                ce.Flags);
        }
        case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
            var tte = node.AsTaggedTemplateExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateTaggedTemplateExpression(
                tte,
                Ꮡp.parenthesizeExpressionForNoAsi(tte.Tag),
                (~tte).QuestionDotToken,
                (~tte).TypeArguments,
                tte.Template,
                tte.Flags);
        }
        case var ᴛ when ᴛ == ast.KindPostfixUnaryExpression: {
            var pue = node.AsPostfixUnaryExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdatePostfixUnaryExpression(
                pue,
                Ꮡp.parenthesizeExpressionForNoAsi(pue.Operand),
                pue.Operator);
        }
        case var ᴛ when ᴛ == ast.KindBinaryExpression: {
            var be = node.AsBinaryExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(
                be,
                be.of(ast.BinaryExpression.ᏑModifiersBase).Modifiers(),
                Ꮡp.parenthesizeExpressionForNoAsi(be.Left),
                (~be).Type,
                be.OperatorToken,
                be.Right);
        }
        case var ᴛ when ᴛ == ast.KindConditionalExpression: {
            var ce = node.AsConditionalExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateConditionalExpression(
                ce,
                Ꮡp.parenthesizeExpressionForNoAsi(ce.Condition),
                (~ce).QuestionToken,
                ce.WhenTrue,
                ce.ColonToken,
                ce.WhenFalse);
        }
        case var ᴛ when ᴛ == ast.KindAsExpression: {
            var ae = node.AsAsExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateAsExpression(
                ae,
                Ꮡp.parenthesizeExpressionForNoAsi((~ae).Expression),
                (~ae).Type);
        }
        case var ᴛ when ᴛ == ast.KindSatisfiesExpression: {
            var se = node.AsSatisfiesExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateSatisfiesExpression(
                se,
                Ꮡp.parenthesizeExpressionForNoAsi((~se).Expression),
                (~se).Type);
        }
        case var ᴛ when ᴛ == ast.KindNonNullExpression: {
            var nne = node.AsNonNullExpression();
            return p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).UpdateNonNullExpression(
                nne,
                Ꮡp.parenthesizeExpressionForNoAsi((~nne).Expression),
                nne.Flags);
        }}

    }
    return Ꮡnode;
}

internal static void emitExpressionNoASI(this ж<Printer> Ꮡp, ж<astꓸExpression> Ꮡnode, ast.OperatorPrecedence precedence) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = Ꮡp.parenthesizeExpressionForNoAsi(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    Ꮡp.emitExpression(Ꮡnode, precedence);
}

internal static void emitExpression(this ж<Printer> Ꮡp, ж<astꓸExpression> Ꮡnode, ast.OperatorPrecedence precedence) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var parens = ast.GetExpressionPrecedence(ast.SkipPartiallyEmittedExpressions(Ꮡnode)) < precedence;
    if (parens) {
        p.writePunctuation("(");
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindTrueKeyword || ᴛ == ast.KindFalseKeyword || ᴛ == ast.KindNullKeyword: {
        p.emitTokenNode(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindThisKeyword || ᴛ == ast.KindSuperKeyword || ᴛ == ast.KindImportKeyword: {
        p.emitKeywordExpression(node.AsKeywordExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral: {
        Ꮡp.emitNumericLiteral(node.AsNumericLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindBigIntLiteral: {
        Ꮡp.emitBigIntLiteral(node.AsBigIntLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        Ꮡp.emitStringLiteral(node.AsStringLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindRegularExpressionLiteral: {
        Ꮡp.emitRegularExpressionLiteral(node.AsRegularExpressionLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNoSubstitutionTemplateLiteral: {
        Ꮡp.emitNoSubstitutionTemplateLiteral(node.AsNoSubstitutionTemplateLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierReference(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrivateIdentifier: {
        Ꮡp.emitPrivateIdentifier(node.AsPrivateIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        Ꮡp.emitArrayLiteralExpression(node.AsArrayLiteralExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        Ꮡp.emitObjectLiteralExpression(node.AsObjectLiteralExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        Ꮡp.emitPropertyAccessExpression(node.AsPropertyAccessExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
        Ꮡp.emitElementAccessExpression(node.AsElementAccessExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        Ꮡp.emitCallExpression(node.AsCallExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNewExpression: {
        Ꮡp.emitNewExpression(node.AsNewExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
        Ꮡp.emitTaggedTemplateExpression(node.AsTaggedTemplateExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeAssertionExpression: {
        Ꮡp.emitTypeAssertionExpression(node.AsTypeAssertion());
        break;
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        Ꮡp.emitParenthesizedExpression(node.AsParenthesizedExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionExpression: {
        Ꮡp.emitFunctionExpression(node.AsFunctionExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrowFunction: {
        Ꮡp.emitArrowFunction(node.AsArrowFunction());
        break;
    }
    case var ᴛ when ᴛ == ast.KindDeleteExpression: {
        Ꮡp.emitDeleteExpression(node.AsDeleteExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeOfExpression: {
        Ꮡp.emitTypeOfExpression(node.AsTypeOfExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindVoidExpression: {
        Ꮡp.emitVoidExpression(node.AsVoidExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindAwaitExpression: {
        Ꮡp.emitAwaitExpression(node.AsAwaitExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        Ꮡp.emitPrefixUnaryExpression(node.AsPrefixUnaryExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPostfixUnaryExpression: {
        Ꮡp.emitPostfixUnaryExpression(node.AsPostfixUnaryExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        Ꮡp.emitBinaryExpression(node.AsBinaryExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindConditionalExpression: {
        Ꮡp.emitConditionalExpression(node.AsConditionalExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateExpression: {
        Ꮡp.emitTemplateExpression(node.AsTemplateExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindYieldExpression: {
        Ꮡp.emitYieldExpression(node.AsYieldExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSpreadElement: {
        Ꮡp.emitSpreadElement(node.AsSpreadElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassExpression: {
        Ꮡp.emitClassExpression(node.AsClassExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindOmittedExpression: {
        p.emitOmittedExpression(Ꮡnode);
        break;
    }
    case var ᴛ when ᴛ == ast.KindAsExpression: {
        Ꮡp.emitAsExpression(node.AsAsExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNonNullExpression: {
        Ꮡp.emitNonNullExpression(node.AsNonNullExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExpressionWithTypeArguments: {
        Ꮡp.emitExpressionWithTypeArguments(node.AsExpressionWithTypeArguments());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSatisfiesExpression: {
        Ꮡp.emitSatisfiesExpression(node.AsSatisfiesExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMetaProperty: {
        Ꮡp.emitMetaProperty(node.AsMetaProperty());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSyntheticExpression: {
        throw panic("SyntheticExpression should never be printed.");
        break;
    }
    case var ᴛ when ᴛ == ast.KindMissingDeclaration: {
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxElement: {
        Ꮡp.emitJsxElement(node.AsJsxElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxSelfClosingElement: {
        Ꮡp.emitJsxSelfClosingElement(node.AsJsxSelfClosingElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxFragment: {
        Ꮡp.emitJsxFragment(node.AsJsxFragment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSyntaxList: {
        throw panic("SyntaxList should not be printed");
        break;
    }
    case var ᴛ when ᴛ == ast.KindNotEmittedStatement: {
        return;
    }
    case var ᴛ when ᴛ == ast.KindPartiallyEmittedExpression: {
        Ꮡp.emitPartiallyEmittedExpression(node.AsPartiallyEmittedExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSyntheticReferenceExpression: {
        throw panic("SyntheticReferenceExpression should not be printed");
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unexpected Expression: %v"u8, node.Kind));
        break;
    }}

    if (parens) {
        p.writePunctuation(")");
    }
}

internal static void emitTemplateSpan(this ж<Printer> Ꮡp, ж<ast.TemplateSpan> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TemplateSpan.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceComma);
    Ꮡp.emitTemplateMiddleTail(node.Literal);
    p.exitNode(Ꮡnode.of(ast.TemplateSpan.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTemplateSpanNode(this ж<Printer> Ꮡp, ж<astꓸTemplateSpanNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitTemplateSpan(node.AsTemplateSpan());
}

internal static void emitSemicolonClassElement(this ref Printer p, ж<ast.SemicolonClassElement> node) {
    var state = p.enterNode(node.of(ast.SemicolonClassElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeTrailingSemicolon();
    p.exitNode(node.of(ast.SemicolonClassElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static bool isEmptyBlock(this ref Printer p, ж<ast.Node> Ꮡblock, ж<astꓸStatementList> Ꮡstatements) {
    ref var block = ref Ꮡblock.DerefOrNull();
    ref var statements = ref Ꮡstatements.DerefOrNull();

    return len(statements.Nodes) == 0 && (p.currentSourceFile == nil || rangeEndIsOnSameLineAsRangeStart(block.Loc, block.Loc, p.currentSourceFile));
}

internal static void emitBlock(this ж<Printer> Ꮡp, ж<ast.Block> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateNames(Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindOpenBraceToken, Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    ListFormat format = core.IfElse(!node.MultiLine && p.isEmptyBlock(Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements) || p.shouldEmitOnSingleLine(Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        LFSingleLineBlockStatements,
        LFMultiLineBlockStatements);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸStatement>>)(printerᴛPrinter.emitStatement)), Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements, format);

    p.emitTokenEx(ast.KindCloseBraceToken, node.Statements.End(), WriteKindPunctuation, Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.IfElse((ListFormat)(format & LFMultiLine) != 0, tefIndentLeadingComments, tefNone));
    p.exitNode(Ꮡnode.of(ast.Block.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitVariableStatement(this ж<Printer> Ꮡp, ж<ast.VariableStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.VariableStatement.ᏑModifiersBase).Modifiers(), false);
    Ꮡp.emitVariableDeclarationList(node.DeclarationList.AsVariableDeclarationList());
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitEmptyStatement(this ref Printer p, ж<ast.EmptyStatement> node, bool isEmbeddedStatement) {
    var state = p.enterNode(node.of(ast.EmptyStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    if (isEmbeddedStatement) {
        p.writePunctuation(";");
    } else {
        p.writeTrailingSemicolon();
    }
    p.exitNode(node.of(ast.EmptyStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitExpressionStatement(this ж<Printer> Ꮡp, ж<ast.ExpressionStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ExpressionStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    if (p.currentSourceFile != nil && p.currentSourceFile.ScriptKind == core.ScriptKindJSON) {
        Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceComma);
    } else if (isImmediatelyInvokedFunctionExpressionOrArrowFunction(node.Expression)) {
        Ꮡp.emitIIFEWithParenthesizedCallee(node.Expression);
    } else {
        switch (ast.GetLeftmostExpression(node.Expression, false).Kind) {
        case var ᴛ when ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindObjectLiteralExpression: {
            Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceParentheses);
            break;
        }
        default: {
            Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceComma);
            break;
        }}

    }

    if (p.currentSourceFile == nil || p.currentSourceFile.ScriptKind != core.ScriptKindJSON || ast.NodeIsSynthesized(node.Expression)) {
        p.writeTrailingSemicolon();
    }

    p.exitNode(Ꮡnode.of(ast.ExpressionStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitIIFEWithParenthesizedCallee(this ж<Printer> Ꮡp, ж<astꓸExpression> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var call = ast.SkipPartiallyEmittedExpressions(node).AsCallExpression();
    var state = p.enterNode(call.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("(");
    Ꮡp.emitExpression((~call).Expression, ast.OperatorPrecedenceLowest);
    p.writePunctuation(")");
    p.emitTokenNode((~call).QuestionDotToken);
    Ꮡp.emitTypeArguments(call.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), (~call).TypeArguments);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸExpression>>)(printerᴛPrinter.emitArgument)), call.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), (~call).Arguments, LFCallExpressionArguments);
    p.exitNode(call.of(ast.CallExpression.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitIfStatement(this ж<Printer> Ꮡp, ж<ast.IfStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindIfKeyword, Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(ast.KindOpenParenToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);
    p.emitToken(ast.KindCloseParenToken, node.Expression.End(), WriteKindPunctuation, Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.ThenStatement);
    if (node.ElseStatement != nil) {
        Ꮡp.writeLineOrSpace(Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.ThenStatement, node.ElseStatement);
        p.emitToken(ast.KindElseKeyword, node.ThenStatement.End(), WriteKindKeyword, Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (node.ElseStatement.Kind == ast.KindIfStatement) {
            p.writeSpace();
            Ꮡp.emitIfStatement(node.ElseStatement.AsIfStatement());
        } else {
            Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.ElseStatement);
        }
    }
    p.exitNode(Ꮡnode.of(ast.IfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitWhileClause(this ж<Printer> Ꮡp, ж<ast.Node> node, ж<astꓸExpression> Ꮡexpression, nint startPos) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var expression = ref Ꮡexpression.DerefOrNull();

    nint pos = p.emitToken(ast.KindWhileKeyword, startPos, WriteKindKeyword, node);
    p.writeSpace();
    p.emitToken(ast.KindOpenParenToken, pos, WriteKindPunctuation, node);
    Ꮡp.emitExpression(Ꮡexpression, ast.OperatorPrecedenceLowest);
    p.emitToken(ast.KindCloseParenToken, expression.End(), WriteKindPunctuation, node);
}

internal static void emitDoStatement(this ж<Printer> Ꮡp, ж<ast.DoStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.DoStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindDoKeyword, Ꮡnode.of(ast.DoStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.DoStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.DoStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statement);
    if (ast.IsBlock(node.Statement) && !p.Options.PreserveSourceNewlines) {
        p.writeSpace();
    } else {
        Ꮡp.writeLineOrSpace(Ꮡnode.of(ast.DoStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statement, node.Expression);
    }

    Ꮡp.emitWhileClause(Ꮡnode.of(ast.DoStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Expression, node.Statement.End());
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.DoStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitWhileStatement(this ж<Printer> Ꮡp, ж<ast.WhileStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.WhileStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitWhileClause(Ꮡnode.of(ast.WhileStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Expression, Ꮡnode.of(ast.WhileStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos());
    Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.WhileStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statement);
    p.exitNode(Ꮡnode.of(ast.WhileStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitForInitializer(this ж<Printer> Ꮡp, ж<astꓸForInitializer> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindVariableDeclarationList) {
        Ꮡp.emitVariableDeclarationList(node.AsVariableDeclarationList());
    } else {
        Ꮡp.emitExpression(Ꮡnode, ast.OperatorPrecedenceLowest);
    }
}

internal static void emitForStatement(this ж<Printer> Ꮡp, ж<ast.ForStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindForKeyword, Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    pos = p.emitToken(ast.KindOpenParenToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Initializer != nil) {
        Ꮡp.emitForInitializer(node.Initializer);
        pos = node.Initializer.End();
    }
    pos = p.emitToken(ast.KindSemicolonToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Condition != nil) {
        p.writeSpace();
        Ꮡp.emitExpression(node.Condition, ast.OperatorPrecedenceLowest);
        pos = node.Condition.End();
    }
    pos = p.emitToken(ast.KindSemicolonToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Incrementor != nil) {
        p.writeSpace();
        Ꮡp.emitExpression(node.Incrementor, ast.OperatorPrecedenceLowest);
        pos = node.Incrementor.End();
    }
    p.emitToken(ast.KindCloseParenToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statement);
    p.exitNode(Ꮡnode.of(ast.ForStatement.ᏑIterationStatementBase).of(ast.IterationStatementBase.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitForInStatement(this ж<Printer> Ꮡp, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindForKeyword, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(ast.KindOpenParenToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitForInitializer(node.Initializer);
    p.writeSpace();
    p.emitToken(ast.KindInKeyword, node.Initializer.End(), WriteKindKeyword, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);
    p.emitToken(ast.KindCloseParenToken, node.Expression.End(), WriteKindPunctuation, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statement);
    p.exitNode(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitForOfStatement(this ж<Printer> Ꮡp, ж<ast.ForInOrOfStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint openParenPos = p.emitToken(ast.KindForKeyword, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    if (node.AwaitModifier != nil) {
        p.emitKeywordNode(node.AwaitModifier);
        p.writeSpace();
    }
    p.emitToken(ast.KindOpenParenToken, openParenPos, WriteKindPunctuation, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitForInitializer(node.Initializer);
    p.writeSpace();
    p.emitToken(ast.KindOfKeyword, node.Initializer.End(), WriteKindKeyword, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);
    p.emitToken(ast.KindCloseParenToken, node.Expression.End(), WriteKindPunctuation, Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statement);
    p.exitNode(Ꮡnode.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitContinueStatement(this ж<Printer> Ꮡp, ж<ast.ContinueStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ContinueStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindContinueKeyword, Ꮡnode.of(ast.ContinueStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ContinueStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Label != nil) {
        p.writeSpace();
        Ꮡp.emitLabelIdentifier(node.Label.AsIdentifier());
    }
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.ContinueStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitBreakStatement(this ж<Printer> Ꮡp, ж<ast.BreakStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.BreakStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindBreakKeyword, Ꮡnode.of(ast.BreakStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.BreakStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Label != nil) {
        p.writeSpace();
        Ꮡp.emitLabelIdentifier(node.Label.AsIdentifier());
    }
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.BreakStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitReturnStatement(this ж<Printer> Ꮡp, ж<ast.ReturnStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ReturnStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindReturnKeyword, Ꮡnode.of(ast.ReturnStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ReturnStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Expression != nil) {
        p.writeSpace();
        Ꮡp.emitExpressionNoASI(node.Expression, ast.OperatorPrecedenceLowest);
    }
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.ReturnStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitWithStatement(this ж<Printer> Ꮡp, ж<ast.WithStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.WithStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindWithKeyword, Ꮡnode.of(ast.WithStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.WithStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(ast.KindOpenParenToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.WithStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);
    p.emitToken(ast.KindCloseParenToken, node.Expression.End(), WriteKindPunctuation, Ꮡnode.of(ast.WithStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitEmbeddedStatement(Ꮡnode.of(ast.WithStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statement);
    p.exitNode(Ꮡnode.of(ast.WithStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitSwitchStatement(this ж<Printer> Ꮡp, ж<ast.SwitchStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.SwitchStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindSwitchKeyword, Ꮡnode.of(ast.SwitchStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.SwitchStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(ast.KindOpenParenToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.SwitchStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);
    p.emitToken(ast.KindCloseParenToken, node.Expression.End(), WriteKindPunctuation, Ꮡnode.of(ast.SwitchStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitCaseBlock(node.CaseBlock.AsCaseBlock());
    p.exitNode(Ꮡnode.of(ast.SwitchStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitLabeledStatement(this ж<Printer> Ꮡp, ж<ast.LabeledStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.LabeledStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitLabelIdentifier(node.Label.AsIdentifier());
    p.emitToken(ast.KindColonToken, node.Label.End(), WriteKindPunctuation, Ꮡnode.of(ast.LabeledStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    p.writeSpace();
    Ꮡp.emitStatement(node.Statement);

    p.exitNode(Ꮡnode.of(ast.LabeledStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitThrowStatement(this ж<Printer> Ꮡp, ж<ast.ThrowStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ThrowStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindThrowKeyword, Ꮡnode.of(ast.ThrowStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ThrowStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpressionNoASI(node.Expression, ast.OperatorPrecedenceLowest);
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.ThrowStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTryStatement(this ж<Printer> Ꮡp, ж<ast.TryStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindTryKeyword, Ꮡnode.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitBlock(node.TryBlock.AsBlock());
    if (node.CatchClause != nil) {
        Ꮡp.writeLineOrSpace(Ꮡnode.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TryBlock, node.CatchClause);
        Ꮡp.emitCatchClause(node.CatchClause.AsCatchClause());
    }
    if (node.FinallyBlock != nil) {
        Ꮡp.writeLineOrSpace(Ꮡnode.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.Coalesce<astꓸCatchClauseNode>(node.CatchClause, node.TryBlock), node.FinallyBlock);
        p.emitToken(ast.KindFinallyKeyword, core.Coalesce<astꓸCatchClauseNode>(node.CatchClause, node.TryBlock).End(), WriteKindKeyword, Ꮡnode.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
        Ꮡp.emitBlock(node.FinallyBlock.AsBlock());
    }
    p.exitNode(Ꮡnode.of(ast.TryStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitDebuggerStatement(this ref Printer p, ж<ast.DebuggerStatement> node) {
    var state = p.enterNode(node.of(ast.DebuggerStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindDebuggerKeyword, node.of(ast.DebuggerStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, node.of(ast.DebuggerStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeTrailingSemicolon();
    p.exitNode(node.of(ast.DebuggerStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNotEmittedStatement(this ref Printer p, ж<ast.NotEmittedStatement> node) {
    p.exitNode(node.of(ast.NotEmittedStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), p.enterNode(node.of(ast.NotEmittedStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
}

internal static void emitNotEmittedTypeElement(this ref Printer p, ж<ast.NotEmittedTypeElement> node) {
    p.exitNode(node.of(ast.NotEmittedTypeElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), p.enterNode(node.of(ast.NotEmittedTypeElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
}

internal static void emitVariableDeclaration(this ж<Printer> Ꮡp, ж<ast.VariableDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitBindingName(node.Name());
    p.emitPunctuationNode(node.ExclamationToken);
    Ꮡp.emitTypeAnnotation(node.Type);
    Ꮡp.emitInitializer(node.Initializer, greatestEnd(node.Name().End(), new ast_NodeжtryGetEnd_node(node.Type), new ast_NodeжtryGetEnd_node(p.emitContext.GetTypeNode(node.Name()))), Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitVariableDeclarationNode(this ж<Printer> Ꮡp, ж<astꓸVariableDeclarationNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitVariableDeclaration(node.AsVariableDeclaration());
}

internal static void emitVariableDeclarationList(this ж<Printer> Ꮡp, ж<ast.VariableDeclarationList> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    switch (ᐧ) {
    case {} when ast.IsVarLet(Ꮡnode.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()): {
        p.writeKeyword("let");
        break;
    }
    case {} when ast.IsVarConst(Ꮡnode.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()): {
        p.writeKeyword("const");
        break;
    }
    case {} when ast.IsVarUsing(Ꮡnode.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()): {
        p.writeKeyword("using");
        break;
    }
    case {} when ast.IsVarAwaitUsing(Ꮡnode.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()): {
        p.writeKeyword("await");
        p.writeSpace();
        p.writeKeyword("using");
        break;
    }
    default: {
        p.writeKeyword("var");
        break;
    }}

    p.writeSpace();
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸVariableDeclarationNode>>)(printerᴛPrinter.emitVariableDeclarationNode)), Ꮡnode.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Declarations, LFVariableDeclarationList);
    p.exitNode(Ꮡnode.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitFunctionDeclaration(this ж<Printer> Ꮡp, ж<ast.FunctionDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateNameIfNeeded(node.Name());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.FunctionDeclaration.ᏑModifiersBase).Modifiers(), false);
    p.writeKeyword("function");
    p.emitTokenNode(node.AsteriskToken);
    p.writeSpace();
    var name = node.Name(); if (name != nil) {
        Ꮡp.emitIdentifierName(name.AsIdentifier());
    }
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    p.pushNameGenerationScope(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitSignature(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitFunctionBodyNode(node.Body);
    p.popNameGenerationScope(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitClassDeclaration(this ж<Printer> Ꮡp, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateNameIfNeeded(node.Name());
    nint pos = Ꮡp.emitModifierList(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), true);
    p.emitToken(ast.KindClassKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Name() != nil) {
        p.writeSpace();
        Ꮡp.emitIdentifierName(node.Name().AsIdentifier());
    }
    var indented = p.shouldEmitIndented(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.increaseIndentIf(indented);
    Ꮡp.emitTypeParameters(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeParameters);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸHeritageClauseNode>>)(printerᴛPrinter.emitHeritageClauseNode)), Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.HeritageClauses, LFClassHeritageClauses);
    p.writeSpace();
    p.writePunctuation("{");
    p.pushNameGenerationScope(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateAllMemberNames(node.Members);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸClassElement>>)(printerᴛPrinter.emitClassElement)), Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Members, LFClassMembers);
    p.popNameGenerationScope(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("}");
    p.decreaseIndentIf(indented);
    p.exitNode(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitInterfaceDeclaration(this ж<Printer> Ꮡp, ж<ast.InterfaceDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.InterfaceDeclaration.ᏑModifiersBase).Modifiers(), false);
    p.writeKeyword("interface");
    p.writeSpace();
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    Ꮡp.emitTypeParameters(Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeParameters);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸHeritageClauseNode>>)(printerᴛPrinter.emitHeritageClauseNode)), Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.HeritageClauses, LFHeritageClauses);
    p.writeSpace();
    p.writePunctuation("{");
    p.pushNameGenerationScope(Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateAllMemberNames(node.Members);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸTypeElement>>)(printerᴛPrinter.emitTypeElement)), Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Members, LFInterfaceMembers);
    p.popNameGenerationScope(Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.InterfaceDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitTypeAliasDeclaration(this ж<Printer> Ꮡp, ж<ast.TypeAliasDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.TypeAliasDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.TypeAliasDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.TypeAliasDeclaration.ᏑModifiersBase).Modifiers(), false);
    p.writeKeyword("type");
    p.writeSpace();
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    Ꮡp.emitTypeParameters(Ꮡnode.of(ast.TypeAliasDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeParameters);
    p.writeSpace();
    p.writePunctuation("=");
    p.writeSpace();
    Ꮡp.emitTypeNodeOutsideExtends(node.Type);
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.TypeAliasDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitEnumDeclaration(this ж<Printer> Ꮡp, ж<ast.EnumDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.EnumDeclaration.ᏑModifiersBase).Modifiers(), false);
    p.writeKeyword("enum");
    p.writeSpace();
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    p.writeSpace();
    p.writePunctuation("{");
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸEnumMemberNode>>)(printerᴛPrinter.emitEnumMemberNode)), Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Members, LFEnumMembers);
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.EnumDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string namespaceˢ = "namespace"u8;
internal static readonly @string moduleˢ = "module"u8;

internal static void emitModuleDeclaration(this ж<Printer> Ꮡp, ж<ast.ModuleDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ModuleDeclaration.ᏑModifiersBase).Modifiers(), false);
    if (node.Keyword != ast.KindGlobalKeyword) {
        p.writeKeyword(core.IfElse(node.Keyword == ast.KindNamespaceKeyword, namespaceˢ, moduleˢ));
        p.writeSpace();
    }
    Ꮡp.emitModuleName(node.Name());
    var body = node.Body;
    while (body != nil && ast.IsModuleDeclaration(body)) {
        var module = body.AsModuleDeclaration();
        p.writePunctuation(".");
        Ꮡp.emitNestedModuleName(module.Name());
        body = module.Value.Body;
    }
    if (node.Attributes != nil) {
        p.writeSpace();
        p.writeKeyword("with");
        p.writeSpace();
        Ꮡp.emitTypeNode(node.Attributes, ast.TypePrecedenceNonArray);
    }
    if (body == nil) {
        p.writeTrailingSemicolon();
    } else {
        p.writeSpace();
        Ꮡp.emitModuleBlock(body.AsModuleBlock());
    }
    p.exitNode(Ꮡnode.of(ast.ModuleDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitModuleBlock(this ж<Printer> Ꮡp, ж<ast.ModuleBlock> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateNames(Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindOpenBraceToken, Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    ListFormat format = core.IfElse(p.isEmptyBlock(Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements) || p.shouldEmitOnSingleLine(Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        LFSingleLineBlockStatements,
        LFMultiLineBlockStatements);
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸStatement>>)(printerᴛPrinter.emitStatement)), Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements, format);
    p.emitTokenEx(ast.KindCloseBraceToken, node.Statements.End(), WriteKindPunctuation, Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.IfElse((ListFormat)(format & LFMultiLine) != 0, tefIndentLeadingComments, tefNone));
    p.exitNode(Ꮡnode.of(ast.ModuleBlock.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitCaseBlock(this ж<Printer> Ꮡp, ж<ast.CaseBlock> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.CaseBlock.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindOpenBraceToken, Ꮡnode.of(ast.CaseBlock.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.CaseBlock.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸCaseOrDefaultClauseNode>>)(printerᴛPrinter.emitCaseOrDefaultClauseNode)), Ꮡnode.of(ast.CaseBlock.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Clauses, LFCaseBlockClauses);
    p.emitTokenEx(ast.KindCloseBraceToken, node.Clauses.End(), WriteKindPunctuation, Ꮡnode.of(ast.CaseBlock.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), tefIndentLeadingComments);
    p.exitNode(Ꮡnode.of(ast.CaseBlock.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitImportEqualsDeclaration(this ж<Printer> Ꮡp, ж<ast.ImportEqualsDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑModifiersBase).Modifiers(), false);
    nint pos = p.emitToken(ast.KindImportKeyword, greatestEnd(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), new ast_ModifierListжtryGetEnd_node(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑModifiersBase).Modifiers())), WriteKindKeyword, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    if (node.IsTypeOnly) {
        p.emitToken(ast.KindTypeKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
    }
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    p.writeSpace();
    p.emitToken(ast.KindEqualsToken, node.Name().End(), WriteKindPunctuation, Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitModuleReference(node.ModuleReference);
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.ImportEqualsDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitModuleReference(this ж<Printer> Ꮡp, ж<astꓸModuleReference> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierReference(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        Ꮡp.emitQualifiedName(node.AsQualifiedName());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExternalModuleReference: {
        Ꮡp.emitExternalModuleReference(node.AsExternalModuleReference());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled ModuleReference: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitImportDeclaration(this ж<Printer> Ꮡp, ж<ast.ImportDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers(), false);
    p.emitToken(ast.KindImportKeyword, greatestEnd(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), new ast_ModifierListжtryGetEnd_node(Ꮡnode.of(ast.ImportDeclaration.ᏑModifiersBase).Modifiers())), WriteKindKeyword, Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    if (node.ImportClause != nil) {
        Ꮡp.emitImportClause(node.ImportClause.AsImportClause());
        p.writeSpace();
        p.emitToken(ast.KindFromKeyword, node.ImportClause.End(), WriteKindKeyword, Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
    }
    Ꮡp.emitExpression(node.ModuleSpecifier, ast.OperatorPrecedenceLowest);
    if (node.Attributes != nil) {
        p.writeSpace();
        Ꮡp.emitImportAttributes(node.Attributes.AsImportAttributes());
    }
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitImportClause(this ж<Printer> Ꮡp, ж<ast.ImportClause> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.PhaseModifier != ast.KindUnknown) {
        p.emitToken(node.PhaseModifier, Ꮡnode.of(ast.ImportClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ImportClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
    }
    var name = node.Name(); if (name != nil) {
        Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
        if (node.NamedBindings != nil) {
            p.emitToken(ast.KindCommaToken, name.End(), WriteKindPunctuation, Ꮡnode.of(ast.ImportClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            p.writeSpace();
        }
    }
    Ꮡp.emitNamedImportBindings(node.NamedBindings);
    p.exitNode(Ꮡnode.of(ast.ImportClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNamespaceImport(this ж<Printer> Ꮡp, ж<ast.NamespaceImport> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NamespaceImport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindAsteriskToken, Ꮡnode.of(ast.NamespaceImport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.NamespaceImport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(ast.KindAsKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.NamespaceImport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    p.exitNode(Ꮡnode.of(ast.NamespaceImport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNamedImports(this ж<Printer> Ꮡp, ж<ast.NamedImports> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NamedImports.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("{");
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸImportSpecifierNode>>)(printerᴛPrinter.emitImportSpecifierNode)), Ꮡnode.of(ast.NamedImports.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Elements, LFNamedImportsOrExportsElements);
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.NamedImports.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNamedImportBindings(this ж<Printer> Ꮡp, ж<astꓸNamedImportBindings> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindNamespaceImport: {
        Ꮡp.emitNamespaceImport(node.AsNamespaceImport());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamedImports: {
        Ꮡp.emitNamedImports(node.AsNamedImports());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled NamedImportBindings: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitImportSpecifier(this ж<Printer> Ꮡp, ж<ast.ImportSpecifier> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.IsTypeOnly) {
        p.writeKeyword("type");
        p.writeSpace();
    }
    if (node.PropertyName != nil) {
        Ꮡp.emitModuleExportName(node.PropertyName);
        p.writeSpace();
        p.emitToken(ast.KindAsKeyword, node.PropertyName.End(), WriteKindKeyword, Ꮡnode.of(ast.ImportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
    }
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    p.exitNode(Ꮡnode.of(ast.ImportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitImportSpecifierNode(this ж<Printer> Ꮡp, ж<astꓸImportSpecifierNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitImportSpecifier(node.AsImportSpecifier());
}

internal static void emitExportAssignment(this ж<Printer> Ꮡp, ж<ast.ExportAssignment> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint nextPos = p.emitToken(ast.KindExportKeyword, Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    if (node.IsExportEquals) {
        p.emitToken(ast.KindEqualsToken, nextPos, WriteKindOperator, Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    } else {
        p.emitToken(ast.KindDefaultKeyword, nextPos, WriteKindKeyword, Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    p.writeSpace();
    if (node.IsExportEquals) {
        Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceAssignment);
    } else {
        var expr = ast.GetLeftmostExpression(node.Expression, false);
        if (ast.IsClassExpression(expr) || ast.IsFunctionExpression(expr)) {
            Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceParentheses);
        } else {
            Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceAssignment);
        }
    }
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.ExportAssignment.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitExportDeclaration(this ж<Printer> Ꮡp, ж<ast.ExportDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitModifierList(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.ExportDeclaration.ᏑModifiersBase).Modifiers(), false);
    nint pos = p.emitToken(ast.KindExportKeyword, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    if (node.IsTypeOnly) {
        pos = p.emitToken(ast.KindTypeKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
    }
    if (node.ExportClause != nil) {
        Ꮡp.emitNamedExportBindings(node.ExportClause);
    } else {
        pos = p.emitToken(ast.KindAsteriskToken, pos, WriteKindPunctuation, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    if (node.ModuleSpecifier != nil) {
        p.writeSpace();
        p.emitToken(ast.KindFromKeyword, greatestEnd(pos, new ast_NodeжtryGetEnd_node(node.ExportClause)), WriteKindKeyword, Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
        Ꮡp.emitExpression(node.ModuleSpecifier, ast.OperatorPrecedenceLowest);
    }
    if (node.Attributes != nil) {
        p.writeSpace();
        Ꮡp.emitImportAttributes(node.Attributes.AsImportAttributes());
    }
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitImportAttributes(this ж<Printer> Ꮡp, ж<ast.ImportAttributes> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(node.Token, Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸImportAttributeNode>>)(printerᴛPrinter.emitImportAttributeNode)), Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Attributes, LFImportAttributes);
    p.exitNode(Ꮡnode.of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitImportAttribute(this ж<Printer> Ꮡp, ж<ast.ImportAttribute> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ImportAttribute.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitImportAttributeName(node.Name());
    p.writePunctuation(":");
    p.writeSpace();
    var value = node.Value;
    if ((EmitFlags)(p.emitContext.EmitFlags(node.Value) & EFNoLeadingComments) == 0) {
        var commentRange = p.emitContext.CommentRange(value);
        p.emitTrailingComments(commentRange.Pos(), commentSeparatorAfter);
    }
    Ꮡp.emitExpression(value, ast.OperatorPrecedenceDisallowComma);
    p.exitNode(Ꮡnode.of(ast.ImportAttribute.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitImportAttributeNode(this ж<Printer> Ꮡp, ж<astꓸImportAttributeNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitImportAttribute(node.AsImportAttribute());
}

internal static void emitNamespaceExportDeclaration(this ж<Printer> Ꮡp, ж<ast.NamespaceExportDeclaration> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NamespaceExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindExportKeyword, Ꮡnode.of(ast.NamespaceExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.NamespaceExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    pos = p.emitToken(ast.KindAsKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.NamespaceExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(ast.KindNamespaceKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.NamespaceExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitBindingIdentifier(node.Name().AsIdentifier());
    p.writeTrailingSemicolon();
    p.exitNode(Ꮡnode.of(ast.NamespaceExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNamespaceExport(this ж<Printer> Ꮡp, ж<ast.NamespaceExport> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NamespaceExport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindAsteriskToken, Ꮡnode.of(ast.NamespaceExport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.NamespaceExport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(ast.KindAsKeyword, pos, WriteKindKeyword, Ꮡnode.of(ast.NamespaceExport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitModuleExportName(node.Name());
    p.exitNode(Ꮡnode.of(ast.NamespaceExport.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNamedExports(this ж<Printer> Ꮡp, ж<ast.NamedExports> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.NamedExports.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("{");
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸExportSpecifierNode>>)(printerᴛPrinter.emitExportSpecifierNode)), Ꮡnode.of(ast.NamedExports.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Elements, LFNamedImportsOrExportsElements);
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.NamedExports.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitNamedExportBindings(this ж<Printer> Ꮡp, ж<astꓸNamedExportBindings> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindNamespaceExport: {
        Ꮡp.emitNamespaceExport(node.AsNamespaceExport());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamedExports: {
        Ꮡp.emitNamedExports(node.AsNamedExports());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled NamedExportBindings: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitExportSpecifier(this ж<Printer> Ꮡp, ж<ast.ExportSpecifier> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ExportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.IsTypeOnly) {
        p.writeKeyword("type");
        p.writeSpace();
    }
    if (node.PropertyName != nil) {
        Ꮡp.emitModuleExportName(node.PropertyName);
        p.writeSpace();
        p.emitToken(ast.KindAsKeyword, node.PropertyName.End(), WriteKindKeyword, Ꮡnode.of(ast.ExportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
    }
    Ꮡp.emitModuleExportName(node.Name());
    p.exitNode(Ꮡnode.of(ast.ExportSpecifier.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitExportSpecifierNode(this ж<Printer> Ꮡp, ж<astꓸExportSpecifierNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitExportSpecifier(node.AsExportSpecifier());
}

internal static void emitEmbeddedStatement(this ж<Printer> Ꮡp, ж<ast.Node> parentNode, ж<astꓸStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBlock(Ꮡnode) || p.shouldEmitOnSingleLine(parentNode) || p.Options.PreserveSourceNewlines && Ꮡp.getLeadingLineTerminatorCount(parentNode, Ꮡnode, LFNone) == 0) {
        p.writeSpace();
        Ꮡp.emitStatement(Ꮡnode);
    } else {
        p.writeLine();
        p.increaseIndent();
        if (node.Kind == ast.KindEmptyStatement) {
            p.emitEmptyStatement(node.AsEmptyStatement(), true);
        } else {
            Ꮡp.emitStatement(Ꮡnode);
        }
        p.decreaseIndent();
    }
}

internal static void emitStatement(this ж<Printer> Ꮡp, ж<astꓸStatement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var snippetElement = p.emitContext.SnippetElement(Ꮡnode); if (snippetElement != nil) {
        p.emitSnippetNode(Ꮡnode, snippetElement);
        return;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindBlock: {
        Ꮡp.emitBlock(node.AsBlock());
        break;
    }
    case var ᴛ when ᴛ == ast.KindEmptyStatement: {
        p.emitEmptyStatement(node.AsEmptyStatement(), false);
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableStatement: {
        Ꮡp.emitVariableStatement(node.AsVariableStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExpressionStatement: {
        Ꮡp.emitExpressionStatement(node.AsExpressionStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIfStatement: {
        Ꮡp.emitIfStatement(node.AsIfStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindDoStatement: {
        Ꮡp.emitDoStatement(node.AsDoStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindWhileStatement: {
        Ꮡp.emitWhileStatement(node.AsWhileStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        Ꮡp.emitForStatement(node.AsForStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindForInStatement: {
        Ꮡp.emitForInStatement(node.AsForInOrOfStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindForOfStatement: {
        Ꮡp.emitForOfStatement(node.AsForInOrOfStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindContinueStatement: {
        Ꮡp.emitContinueStatement(node.AsContinueStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindBreakStatement: {
        Ꮡp.emitBreakStatement(node.AsBreakStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindReturnStatement: {
        Ꮡp.emitReturnStatement(node.AsReturnStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindWithStatement: {
        Ꮡp.emitWithStatement(node.AsWithStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSwitchStatement: {
        Ꮡp.emitSwitchStatement(node.AsSwitchStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindLabeledStatement: {
        Ꮡp.emitLabeledStatement(node.AsLabeledStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindThrowStatement: {
        Ꮡp.emitThrowStatement(node.AsThrowStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTryStatement: {
        Ꮡp.emitTryStatement(node.AsTryStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindDebuggerStatement: {
        p.emitDebuggerStatement(node.AsDebuggerStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNotEmittedStatement: {
        p.emitNotEmittedStatement(node.AsNotEmittedStatement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        Ꮡp.emitFunctionDeclaration(node.AsFunctionDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration: {
        Ꮡp.emitClassDeclaration(node.AsClassDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindInterfaceDeclaration: {
        Ꮡp.emitInterfaceDeclaration(node.AsInterfaceDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration: {
        Ꮡp.emitTypeAliasDeclaration(node.AsTypeAliasDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
        Ꮡp.emitEnumDeclaration(node.AsEnumDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
        Ꮡp.emitModuleDeclaration(node.AsModuleDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMissingDeclaration: {
        break;
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamespaceExportDeclaration: {
        Ꮡp.emitNamespaceExportDeclaration(node.AsNamespaceExportDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
        Ꮡp.emitImportEqualsDeclaration(node.AsImportEqualsDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportDeclaration: {
        Ꮡp.emitImportDeclaration(node.AsImportDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        Ꮡp.emitExportAssignment(node.AsExportAssignment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportDeclaration: {
        Ꮡp.emitExportDeclaration(node.AsExportDeclaration());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled statement: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitExternalModuleReference(this ж<Printer> Ꮡp, ж<ast.ExternalModuleReference> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ExternalModuleReference.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeKeyword("require");
    p.writePunctuation("(");
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceDisallowComma);
    p.writePunctuation(")");
    p.exitNode(Ꮡnode.of(ast.ExternalModuleReference.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxElement(this ж<Printer> Ꮡp, ж<ast.JsxElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitJsxOpeningElement(node.OpeningElement.AsJsxOpeningElement());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸJsxChild>>)(printerᴛPrinter.emitJsxChild)), Ꮡnode.of(ast.JsxElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Children, LFJsxElementOrFragmentChildren);
    Ꮡp.emitJsxClosingElement(node.ClosingElement.AsJsxClosingElement());
    p.exitNode(Ꮡnode.of(ast.JsxElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxSelfClosingElement(this ж<Printer> Ꮡp, ж<ast.JsxSelfClosingElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxSelfClosingElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("<");
    Ꮡp.emitJsxTagName(node.TagName);
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.JsxSelfClosingElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    p.writeSpace();
    Ꮡp.emitJsxAttributes(node.Attributes.AsJsxAttributes());
    p.writePunctuation("/>");
    p.exitNode(Ꮡnode.of(ast.JsxSelfClosingElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxFragment(this ж<Printer> Ꮡp, ж<ast.JsxFragment> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxFragment.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitJsxOpeningFragment(node.OpeningFragment.AsJsxOpeningFragment());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸJsxChild>>)(printerᴛPrinter.emitJsxChild)), Ꮡnode.of(ast.JsxFragment.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Children, LFJsxElementOrFragmentChildren);
    p.emitJsxClosingFragment(node.ClosingFragment.AsJsxClosingFragment());
    p.exitNode(Ꮡnode.of(ast.JsxFragment.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxOpeningElement(this ж<Printer> Ꮡp, ж<ast.JsxOpeningElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxOpeningElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("<");
    var indented = Ꮡp.writeLineSeparatorsAndIndentBefore(node.TagName, Ꮡnode.of(ast.JsxOpeningElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitJsxTagName(node.TagName);
    Ꮡp.emitTypeArguments(Ꮡnode.of(ast.JsxOpeningElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.TypeArguments);
    if (len(node.Attributes.Properties()) > 0) {
        p.writeSpace();
    }
    Ꮡp.emitJsxAttributes(node.Attributes.AsJsxAttributes());
    Ꮡp.writeLineSeparatorsAfter(node.Attributes, Ꮡnode.of(ast.JsxOpeningElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.decreaseIndentIf(indented);
    p.writePunctuation(">");
    p.exitNode(Ꮡnode.of(ast.JsxOpeningElement.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxClosingElement(this ж<Printer> Ꮡp, ж<ast.JsxClosingElement> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxClosingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("</");
    Ꮡp.emitJsxTagName(node.TagName);
    p.writePunctuation(">");
    p.exitNode(Ꮡnode.of(ast.JsxClosingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxOpeningFragment(this ref Printer p, ж<ast.JsxOpeningFragment> node) {
    var state = p.enterNode(node.of(ast.JsxOpeningFragment.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("<");
    p.writePunctuation(">");
    p.exitNode(node.of(ast.JsxOpeningFragment.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxClosingFragment(this ref Printer p, ж<ast.JsxClosingFragment> node) {
    var state = p.enterNode(node.of(ast.JsxClosingFragment.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("</");
    p.writePunctuation(">");
    p.exitNode(node.of(ast.JsxClosingFragment.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxText(this ref Printer p, ж<ast.JsxText> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxText.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeLiteral(node.Text);
    p.exitNode(Ꮡnode.of(ast.JsxText.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxAttributes(this ж<Printer> Ꮡp, ж<ast.JsxAttributes> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxAttributes.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸJsxAttributeLike>>)(printerᴛPrinter.emitJsxAttributeLike)), Ꮡnode.of(ast.JsxAttributes.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Properties, LFJsxElementAttributes);
    p.exitNode(Ꮡnode.of(ast.JsxAttributes.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxAttribute(this ж<Printer> Ꮡp, ж<ast.JsxAttribute> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxAttribute.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitJsxAttributeName(node.Name());
    if (node.Initializer != nil) {
        p.writePunctuation("=");
        Ꮡp.emitJsxAttributeValue(node.Initializer);
    }
    p.exitNode(Ꮡnode.of(ast.JsxAttribute.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxSpreadAttribute(this ж<Printer> Ꮡp, ж<ast.JsxSpreadAttribute> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxSpreadAttribute.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writePunctuation("{...");
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);
    p.writePunctuation("}");
    p.exitNode(Ꮡnode.of(ast.JsxSpreadAttribute.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxAttributeLike(this ж<Printer> Ꮡp, ж<astꓸJsxAttributeLike> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindJsxAttribute: {
        Ꮡp.emitJsxAttribute(node.AsJsxAttribute());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxSpreadAttribute: {
        Ꮡp.emitJsxSpreadAttribute(node.AsJsxSpreadAttribute());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled JsxAttributeLike: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitJsxExpression(this ж<Printer> Ꮡp, ж<ast.JsxExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Expression != nil || !p.commentsDisabled && !ast.NodeIsSynthesized(Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && p.hasCommentsAtPosition(Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos())) {
        var indented = p.currentSourceFile != nil && !ast.NodeIsSynthesized(Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) && GetLinesBetweenPositions(p.currentSourceFile, Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).End()) != 0;
        p.increaseIndentIf(indented);
        nint end = p.emitToken(ast.KindOpenBraceToken, Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.emitTokenNode(node.DotDotDotToken);
        if (node.Expression != nil) {
            Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceDisallowComma);
        }
        p.emitToken(ast.KindCloseBraceToken, greatestEnd(end, new ast_NodeжtryGetEnd_node(node.Expression), new ast_NodeжtryGetEnd_node(node.DotDotDotToken)), WriteKindPunctuation, Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.decreaseIndentIf(indented);
    }
    p.exitNode(Ꮡnode.of(ast.JsxExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxNamespacedName(this ж<Printer> Ꮡp, ж<ast.JsxNamespacedName> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.JsxNamespacedName.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitIdentifierName(node.Namespace.AsIdentifier());
    p.writePunctuation(":");
    Ꮡp.emitIdentifierName(node.Name().AsIdentifier());
    p.exitNode(Ꮡnode.of(ast.JsxNamespacedName.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitJsxChild(this ж<Printer> Ꮡp, ж<astꓸJsxChild> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindJsxText: {
        p.emitJsxText(node.AsJsxText());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxExpression: {
        Ꮡp.emitJsxExpression(node.AsJsxExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxElement: {
        Ꮡp.emitJsxElement(node.AsJsxElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxSelfClosingElement: {
        Ꮡp.emitJsxSelfClosingElement(node.AsJsxSelfClosingElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxFragment: {
        Ꮡp.emitJsxFragment(node.AsJsxFragment());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled JsxChild: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitJsxTagName(this ж<Printer> Ꮡp, ж<astꓸJsxTagNameExpression> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierReference(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindThisKeyword: {
        p.emitKeywordExpression(node.AsKeywordExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxNamespacedName: {
        Ꮡp.emitJsxNamespacedName(node.AsJsxNamespacedName());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        Ꮡp.emitPropertyAccessExpression(node.AsPropertyAccessExpression());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled JsxTagName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitJsxAttributeName(this ж<Printer> Ꮡp, ж<astꓸJsxAttributeName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierName(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxNamespacedName: {
        Ꮡp.emitJsxNamespacedName(node.AsJsxNamespacedName());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled JsxAttributeName: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitJsxAttributeValue(this ж<Printer> Ꮡp, ж<astꓸJsxAttributeValue> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        Ꮡp.emitStringLiteral(node.AsStringLiteral());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxExpression: {
        Ꮡp.emitJsxExpression(node.AsJsxExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxElement: {
        Ꮡp.emitJsxElement(node.AsJsxElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxSelfClosingElement: {
        Ꮡp.emitJsxSelfClosingElement(node.AsJsxSelfClosingElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxFragment: {
        Ꮡp.emitJsxFragment(node.AsJsxFragment());
        break;
    }
    default: {
        Ꮡp.emitExpression(Ꮡnode, ast.OperatorPrecedenceLowest);
        break;
    }}

}

internal static void emitCaseOrDefaultClauseStatements(this ж<Printer> Ꮡp, ж<ast.CaseOrDefaultClause> Ꮡnode, nint colonPos) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var emitAsSingleStatement = len(node.Statements.Nodes) == 1 && (p.currentSourceFile == nil || ast.NodeIsSynthesized(Ꮡnode.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) || ast.NodeIsSynthesized(node.Statements.Nodes[0]) || RangeStartPositionsAreOnSameLine(node.Loc, node.Statements.Nodes[0].Loc, p.currentSourceFile));

    ListFormat format = LFCaseOrDefaultClauseStatements;
    if (emitAsSingleStatement) {
        p.writeTokenText(ast.KindColonToken, WriteKindPunctuation, colonPos);
        p.writeSpace();
        format &= ~(ListFormat)((ListFormat)(LFMultiLine | LFIndented));
    } else {
        p.emitToken(ast.KindColonToken, colonPos, WriteKindPunctuation, Ꮡnode.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸStatement>>)(printerᴛPrinter.emitStatement)), Ꮡnode.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements, format);
}

internal static void emitCaseClause(this ж<Printer> Ꮡp, ж<ast.CaseOrDefaultClause> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitToken(ast.KindCaseKeyword, Ꮡnode.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceLowest);
    Ꮡp.emitCaseOrDefaultClauseStatements(Ꮡnode, node.Expression.End());
    p.exitNode(Ꮡnode.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitDefaultClause(this ж<Printer> Ꮡp, ж<ast.CaseOrDefaultClause> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.enterNode(node.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint pos = p.emitToken(ast.KindDefaultKeyword, node.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, node.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitCaseOrDefaultClauseStatements(node, pos);
    p.exitNode(node.of(ast.CaseOrDefaultClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitCaseOrDefaultClauseNode(this ж<Printer> Ꮡp, ж<astꓸCaseOrDefaultClauseNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindCaseClause: {
        Ꮡp.emitCaseClause(node.AsCaseOrDefaultClause());
        break;
    }
    case var ᴛ when ᴛ == ast.KindDefaultClause: {
        Ꮡp.emitDefaultClause(node.AsCaseOrDefaultClause());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled CaseOrDefaultClause: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitHeritageClause(this ж<Printer> Ꮡp, ж<ast.HeritageClause> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.HeritageClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    p.emitToken(node.Token, Ꮡnode.of(ast.HeritageClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.HeritageClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();
    Ꮡp.emitList(((Action<ж<Printer>, ж<astꓸHeritageClauseElement>>)(printerᴛPrinter.emitHeritageClauseElement)), Ꮡnode.of(ast.HeritageClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Types, LFHeritageClauseTypes);
    p.exitNode(Ꮡnode.of(ast.HeritageClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitHeritageClauseElement(this ж<Printer> Ꮡp, ж<astꓸHeritageClauseElement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindExpressionWithTypeArguments: {
        Ꮡp.emitExpressionWithTypeArguments(node.AsExpressionWithTypeArguments());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeReference: {
        Ꮡp.emitTypeReference(node.AsTypeReferenceNode());
        break;
    }
    default: {
        throw panic(fmt.Sprintf("unhandled HeritageClauseElement: %v"u8, node.Kind));
        break;
    }}

}

internal static void emitHeritageClauseNode(this ж<Printer> Ꮡp, ж<astꓸHeritageClauseNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitHeritageClause(node.AsHeritageClause());
}

internal static void emitCatchClause(this ж<Printer> Ꮡp, ж<ast.CatchClause> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    nint openParenPos = p.emitToken(ast.KindCatchKeyword, Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindKeyword, Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.writeSpace();

    if (node.VariableDeclaration != nil) {
        p.emitToken(ast.KindOpenParenToken, openParenPos, WriteKindPunctuation, Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        Ꮡp.emitVariableDeclaration(node.VariableDeclaration.AsVariableDeclaration());
        p.emitToken(ast.KindCloseParenToken, node.VariableDeclaration.End(), WriteKindPunctuation, Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        p.writeSpace();
    }

    Ꮡp.emitBlock(node.Block.AsBlock());
    p.exitNode(Ꮡnode.of(ast.CatchClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitPropertyAssignment(this ж<Printer> Ꮡp, ж<ast.PropertyAssignment> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.PropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitPropertyName(node.Name());
    p.writePunctuation(":");
    p.writeSpace();
    var initializer = node.Initializer;
    if ((EmitFlags)(p.emitContext.EmitFlags(initializer) & EFNoLeadingComments) == 0) {
        var commentRange = p.emitContext.CommentRange(initializer);
        p.emitTrailingComments(commentRange.Pos(), commentSeparatorAfter);
    }
    Ꮡp.emitExpression(initializer, ast.OperatorPrecedenceDisallowComma);
    p.exitNode(Ꮡnode.of(ast.PropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitShorthandPropertyAssignment(this ж<Printer> Ꮡp, ж<ast.ShorthandPropertyAssignment> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.ShorthandPropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitPropertyName(node.Name());
    if (node.ObjectAssignmentInitializer != nil) {
        p.writeSpace();
        p.writePunctuation("=");
        p.writeSpace();
        Ꮡp.emitExpression(node.ObjectAssignmentInitializer, ast.OperatorPrecedenceDisallowComma);
    }
    p.exitNode(Ꮡnode.of(ast.ShorthandPropertyAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitSpreadAssignment(this ж<Printer> Ꮡp, ж<ast.SpreadAssignment> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.SpreadAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (node.Expression != nil) {
        p.emitToken(ast.KindDotDotDotToken, Ꮡnode.of(ast.SpreadAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos(), WriteKindPunctuation, Ꮡnode.of(ast.SpreadAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        Ꮡp.emitExpression(node.Expression, ast.OperatorPrecedenceDisallowComma);
    }
    p.exitNode(Ꮡnode.of(ast.SpreadAssignment.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitEnumMember(this ж<Printer> Ꮡp, ж<ast.EnumMember> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var state = p.enterNode(Ꮡnode.of(ast.EnumMember.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.emitPropertyName(node.Name());
    Ꮡp.emitInitializer(node.Initializer, node.Name().End(), Ꮡnode.of(ast.EnumMember.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.exitNode(Ꮡnode.of(ast.EnumMember.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), state);
}

internal static void emitEnumMemberNode(this ж<Printer> Ꮡp, ж<astꓸEnumMemberNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡp.emitEnumMember(node.AsEnumMember());
}

internal static void emitJSDocNode(this ref Printer p, ж<ast.Node> node) {
    throw panic("not implemented");
}

internal static void emitShebangIfNeeded(this ref Printer p, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.NodeIsSynthesized(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        return;
    }
    @string shebang = scanner.GetShebang(node.Text());
    if (shebang != ""u8) {
        p.writeComment(shebang);
        p.writeLine();
    }
}

internal static nint emitPrologueDirectives(this ж<Printer> Ꮡp, ж<astꓸStatementList> Ꮡstatements) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var statements = ref Ꮡstatements.DerefOrNull();

    foreach (var (i, statement) in range(statements.Nodes)) {
        if (ast.IsPrologueDirective(statement)) {
            p.writeLine();
            Ꮡp.emitStatement(statement);
        } else {
            return i;
        }
    }
    return len(statements.Nodes);
}

internal static bool emitHelpers(this ref Printer p, ж<ast.Node> node) {
    var helpersEmitted = false;
    var sourceFile = p.currentSourceFile;
    var shouldSkip = p.Options.NoEmitHelpers || (sourceFile != nil && p.emitContext.HasRecordedExternalHelpers(sourceFile));
    var helpers = slices.Clone<slice<ж<EmitHelper>>, ж<EmitHelper>>(p.emitContext.GetEmitHelpers(node));
    if (len(helpers) > 0) {
        slices.SortStableFunc<slice<ж<EmitHelper>>, ж<EmitHelper>>(helpers, compareEmitHelpers);
        foreach (var (_, helper) in range(helpers)) {
            if (!helper.Scoped) {
                if (shouldSkip) {
                    continue;
                }
            }
            if (helper.TextCallback != nil) {
                p.writeLines(helper.TextCallback(p.makeFileLevelOptimisticUniqueName));
            } else {
                p.writeLines(helper.Text);
            }
            helpersEmitted = true;
        }
    }

    return helpersEmitted;
}

internal static void emitSourceFile(this ж<Printer> Ꮡp, ж<ast.SourceFile> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var savedCurrentSourceFile = p.currentSourceFile;
    var savedCommentsDisabled = p.commentsDisabled;
    p.currentSourceFile = Ꮡnode;

    p.writeLine();

    p.pushNameGenerationScope(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡp.generateAllNames(node.Statements);

    nint index = 0;
    ж<commentState> state = default;
    if (node.ScriptKind != core.ScriptKindJSON) {
        p.emitShebangIfNeeded(Ꮡnode);
        index = Ꮡp.emitPrologueDirectives(node.Statements);
        if (!p.writer.IsAtStartOfLine()) {
            p.writeLine();
        }
        state = p.emitDetachedCommentsBeforeStatementList(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements.Loc);
        p.emitHelpers(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (node.IsDeclarationFile) {
            p.emitTripleSlashDirectives(Ꮡnode);
        }
    } else {
        state = p.emitDetachedCommentsBeforeStatementList(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements.Loc);
    }

    Ꮡp.emitListRange(
        ((Action<ж<Printer>, ж<astꓸStatement>>)(printerᴛPrinter.emitStatement)),
        Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(),
        node.Statements,
        LFMultiLine,
        index,
        -1);
    p.popNameGenerationScope(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    p.emitDetachedCommentsAfterStatementList(Ꮡnode.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Statements.Loc, state);
    p.currentSourceFile = savedCurrentSourceFile;
    p.commentsDisabled = savedCommentsDisabled;
}

internal static void emitTripleSlashDirectives(this ref Printer p, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    p.emitDirective("path", node.ReferencedFiles);
    p.emitDirective("types", node.TypeReferenceDirectives);
    p.emitDirective("lib", node.LibReferenceDirectives);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string importˢ = "import"u8;
internal static readonly @string requireˢ = "require"u8;
internal static readonly @string preserveTrueˢ = @"preserve=""true"" "u8;

internal static void emitDirective(this ref Printer p, @string kind, slice<ж<ast.FileReference>> refs) {
    foreach (var (_, @ref) in range(refs)) {
        @string resolutionMode = default;
        if (@ref.ResolutionMode != core.ResolutionModeNone) {
            resolutionMode = fmt.Sprintf(@"resolution-mode=""%s"" "u8, core.IfElse(@ref.ResolutionMode == core.ResolutionModeESM, importˢ, requireˢ));
        }
        p.writeComment(fmt.Sprintf("/// <reference %s=\"%s\" %s%s/>"u8, kind, @ref.FileName, resolutionMode, core.IfElse(@ref.Preserve, preserveTrueˢ, (@string)"")));
        p.writeLine();
    }
}

internal static void emitList(this ж<Printer> Ꮡp, Action<ж<Printer>, ж<ast.Node>> emit, ж<ast.Node> parentNode, ж<ast.NodeList> children, ListFormat format) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.shouldEmitOnMultipleLines(parentNode)) {
        format |= (ListFormat)((ListFormat)(LFPreferNewLine | LFIndented));
    }

    Ꮡp.emitListRange(emit, parentNode, children, format, -1, -1);
}

internal static void emitListRange(this ж<Printer> Ꮡp, Action<ж<Printer>, ж<ast.Node>> emit, ж<ast.Node> ᏑparentNode, ж<ast.NodeList> Ꮡchildren, ListFormat format, nint start, nint count) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var parentNode = ref ᏑparentNode.DerefOrNull();
    ref var children = ref Ꮡchildren.DerefOrNull();

    var isNil = Ꮡchildren == nil;

    nint length = 0;
    if (!isNil) {
        length = len(children.Nodes);
    }

    if (start < 0) {
        start = 0;
    }

    if (count < 0) {
        count = length - start;
    }

    if (isNil && (ListFormat)(format & LFOptionalIfNil) != 0) {
        return;
    }

    var isEmpty = isNil || start >= length || count <= 0;
    if (isEmpty && (ListFormat)(format & LFOptionalIfEmpty) != 0) {
        if (p.OnBeforeEmitNodeList != nil) {
            p.OnBeforeEmitNodeList(Ꮡchildren);
        }
        if (p.OnAfterEmitNodeList != nil) {
            p.OnAfterEmitNodeList(Ꮡchildren);
        }
        return;
    }

    if ((ListFormat)(format & LFBracketsMask) != 0) {
        p.writePunctuation(getOpeningBracket(format));
        if (isEmpty && !isNil) {
            p.emitTrailingComments(children.Pos(), commentSeparatorBefore);
        }
    }

    if (p.OnBeforeEmitNodeList != nil) {
        p.OnBeforeEmitNodeList(Ꮡchildren);
    }

    if (isEmpty) {
        if ((ListFormat)(format & LFMultiLine) != 0 && !(p.Options.PreserveSourceNewlines && (ᏑparentNode == nil || p.currentSourceFile != nil && RangeIsOnSingleLine(parentNode.Loc, p.currentSourceFile)))) {
            p.writeLine();
        } else if ((ListFormat)(format & LFSpaceBetweenBraces) != 0 && (ListFormat)(format & LFNoSpaceIfEmpty) == 0) {
            p.writeSpace();
        }
    } else {
        nint end = min(start + count, length);

        Ꮡp.emitListItems(emit, ᏑparentNode, children.Nodes.slice(start, end), format, p.hasTrailingComma(ᏑparentNode, Ꮡchildren), children.Loc);
    }

    if (p.OnAfterEmitNodeList != nil) {
        p.OnAfterEmitNodeList(Ꮡchildren);
    }

    if ((ListFormat)(format & LFBracketsMask) != 0) {
        if (isEmpty && !isNil) {
            p.emitLeadingComments(children.End(), false);
        }
        p.writePunctuation(getClosingBracket(format));
    }
}

internal static bool hasTrailingComma(this ref Printer p, ж<ast.Node> ᏑparentNode, ж<ast.NodeList> Ꮡchildren) {
    ref var parentNode = ref ᏑparentNode.DerefOrNull();
    ref var children = ref Ꮡchildren.DerefOrNull();

    if (!children.HasTrailingComma()) {
        return false;
    }

    var originalParent = p.emitContext.MostOriginal(ᏑparentNode);
    if (originalParent == ᏑparentNode) {
        return true;
    }

    if (originalParent.Kind != parentNode.Kind) {
        return false;
    }

    var originalList = Ꮡchildren;
    switch (originalParent.Kind) {
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        originalList = originalParent.PropertyList();
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        originalList = originalParent.ElementList();
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallExpression || ᴛ == ast.KindNewExpression: {
        switch (Ꮡchildren) {
        case var ᴛ2 when ᴛ2 == parentNode.TypeArgumentList(): {
            originalList = originalParent.TypeArgumentList();
            break;
        }
        case var ᴛ2 when ᴛ2 == parentNode.ArgumentList(): {
            originalList = originalParent.ArgumentList();
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructor || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType || ᴛ == ast.KindCallSignature || ᴛ == ast.KindConstructSignature: {
        switch (Ꮡchildren) {
        case var ᴛ2 when ᴛ2 == parentNode.TypeParameterList(): {
            originalList = originalParent.TypeParameterList();
            break;
        }
        case var ᴛ2 when ᴛ2 == parentNode.ParameterList(): {
            originalList = originalParent.ParameterList();
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression || ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration: {
        switch (Ꮡchildren) {
        case var ᴛ2 when ᴛ2 == parentNode.TypeParameterList(): {
            originalList = originalParent.TypeParameterList();
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectBindingPattern || ᴛ == ast.KindArrayBindingPattern: {
        switch (Ꮡchildren) {
        case var ᴛ2 when ᴛ2 == parentNode.ElementList(): {
            originalList = originalParent.ElementList();
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindNamedImports || ᴛ == ast.KindNamedExports: {
        originalList = originalParent.ElementList();
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportAttributes: {
        originalList = originalParent.AsImportAttributes().Value.Attributes;
        break;
    }}

    if (originalList != nil) {
        return originalList.HasTrailingComma();
    }

    return false;
}

internal static void writeDelimiter(this ref Printer p, ListFormat format) {
    switch ((ListFormat)(format & LFDelimitersMask)) {
    case var ᴛ when ᴛ == LFNone: {
        break;
        break;
    }
    case var ᴛ when ᴛ == LFCommaDelimited: {
        p.writePunctuation(",");
        break;
    }
    case var ᴛ when ᴛ == LFBarDelimited: {
        p.writeSpace();
        p.writePunctuation("|");
        break;
    }
    case var ᴛ when ᴛ == LFAsteriskDelimited: {
        p.writeSpace();
        p.writePunctuation("*");
        p.writeSpace();
        break;
    }
    case var ᴛ when ᴛ == LFAmpersandDelimited: {
        p.writeSpace();
        p.writePunctuation("&");
        break;
    }}

}

internal static void emitListItems(this ж<Printer> Ꮡp, Action<ж<Printer>, ж<ast.Node>> emit, ж<ast.Node> parentNode, slice<ж<ast.Node>> children, ListFormat format, bool hasTrailingComma, core.TextRange childrenTextRange) {
    ref var p = ref Ꮡp.DerefOrNull();

    var mayEmitInterveningComments = (ListFormat)(format & LFNoInterveningComments) == 0;
    var shouldEmitInterveningComments = mayEmitInterveningComments;

    nint leadingLineTerminatorCount = 0;
    if (len(children) > 0) {
        leadingLineTerminatorCount = Ꮡp.getLeadingLineTerminatorCount(parentNode, children[0], format);
    }
    if (leadingLineTerminatorCount > 0) {
        foreach (var _ᴛ1 in range(leadingLineTerminatorCount)) {
            p.writeLine();
        }
        shouldEmitInterveningComments = false;
    } else if ((ListFormat)(format & LFSpaceBetweenBraces) != 0) {
        p.writeSpace();
    }

    if ((ListFormat)(format & LFIndented) != 0) {
        p.increaseIndent();
    }

    nint parentEnd = greatestEnd(-1, new ast_NodeжtryGetEnd_node(parentNode));

    ж<ast.Node> previousSibling = default;
    var shouldDecreaseIndentAfterEmit = false;
    foreach (var (_, child) in range(children)) {
        if ((ListFormat)(format & LFAsteriskDelimited) != 0) {
            p.writeLine();
            p.writeDelimiter(format);
        } else if (previousSibling != nil) {
            if ((ListFormat)(format & LFDelimitersMask) != 0 && previousSibling.End() != parentEnd) {
                if (!p.commentsDisabled && p.shouldEmitTrailingComments(previousSibling)) {
                    p.emitLeadingComments(previousSibling.End(), false);
                }
            }

            p.writeDelimiter(format);

            nint separatingLineTerminatorCount = Ꮡp.getSeparatingLineTerminatorCount(previousSibling, child, format);
            if (separatingLineTerminatorCount > 0) {
                if ((ListFormat)(format & ((ListFormat)(LFLinesMask | LFIndented))) == LFSingleLine) {
                    p.increaseIndent();
                    shouldDecreaseIndentAfterEmit = true;
                }

                if (shouldEmitInterveningComments && (ListFormat)(format & LFDelimitersMask) != 0 && !ast.PositionIsSynthesized(child.Pos()) && p.shouldEmitLeadingComments(child)) {
                    var commentRange = p.emitContext.CommentRange(child);
                    p.emitTrailingCommentsOfPosition(commentRange.Pos(), (ListFormat)(format & LFSpaceBetweenSiblings) != 0, true);
                }

                foreach (var _ᴛ2 in range(separatingLineTerminatorCount)) {
                    p.writeLine();
                }

                shouldEmitInterveningComments = false;
            } else if ((ListFormat)(format & LFSpaceBetweenSiblings) != 0) {
                p.writeSpace();
            }
        }

        if (shouldEmitInterveningComments && p.shouldEmitLeadingComments(child)) {
            var commentRange = p.emitContext.CommentRange(child);
            p.emitTrailingCommentsOfPosition(commentRange.Pos(), false, false);
        } else {
            shouldEmitInterveningComments = mayEmitInterveningComments;
        }

        p.nextListElementPos = child.Pos();
        emit(Ꮡp, child);

        if (shouldDecreaseIndentAfterEmit) {
            p.decreaseIndent();
            shouldDecreaseIndentAfterEmit = false;
        }

        previousSibling = child;
    }

    var skipTrailingComments = p.commentsDisabled || !p.shouldEmitTrailingComments(previousSibling);
    var emitTrailingComma = hasTrailingComma && (ListFormat)(format & LFAllowTrailingComma) != 0 && (ListFormat)(format & LFCommaDelimited) != 0;
    if (emitTrailingComma) {
        if (previousSibling != nil && !skipTrailingComments) {
            p.emitToken(ast.KindCommaToken, previousSibling.End(), WriteKindPunctuation, previousSibling);
        } else {
            p.writePunctuation(",");
        }
    }

    if (previousSibling != nil && parentEnd != previousSibling.End() && (ListFormat)(format & LFDelimitersMask) != 0 && !skipTrailingComments) {
        nint commentsPos = default;
        if (emitTrailingComma && childrenTextRange.End() > 0) {
            commentsPos = childrenTextRange.End();
        } else {
            commentsPos = previousSibling.End();
        }
        p.emitLeadingComments(commentsPos, false);
    }

    if ((ListFormat)(format & LFIndented) != 0) {
        p.decreaseIndent();
    }

    nint closingLineTerminatorCount = Ꮡp.getClosingLineTerminatorCount(parentNode, core.LastOrNil(children), format, childrenTextRange);
    if (closingLineTerminatorCount > 0) {
        foreach (var _ᴛ3 in range(closingLineTerminatorCount)) {
            p.writeLine();
        }
    } else if ((ListFormat)(format & ((ListFormat)(LFSpaceAfterList | LFSpaceBetweenBraces))) != 0) {
        p.writeSpace();
    }
}

public static @string Emit(this ж<Printer> Ꮡp, ж<ast.Node> node, ж<ast.SourceFile> sourceFile) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.ownWriter == nil) {
        p.ownWriter = NewTextWriter(p.Options.NewLine.GetNewLineCharacter(), 0);
    }

    Ꮡp.Write(node, sourceFile, p.ownWriter, nil);
    @string text = p.ownWriter.String();

    p.ownWriter.Clear();
    return text;
}

public static @string EmitSourceFile(this ж<Printer> Ꮡp, ж<ast.SourceFile> sourceFile) {
    return Ꮡp.Emit(sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), sourceFile);
}

internal static void setSourceFile(this ref Printer p, ж<ast.SourceFile> sourceFile) {
    p.currentSourceFile = sourceFile;
    p.uniqueHelperNames = default;
    p.externalHelpersModuleName = default;
    if (sourceFile != nil) {
        if ((EmitFlags)(p.emitContext.EmitFlags(p.emitContext.MostOriginal(sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) & EFExternalHelpers) != 0) {
            p.uniqueHelperNames = new map<@string, ж<astꓸIdentifierNode>>();
        }
        p.externalHelpersModuleName = p.emitContext.GetExternalHelpersModuleName(sourceFile);
        p.setSourceMapSource(new ast_SourceFileжSource(sourceFile));
    }
}

} partial class printer_package {

internal partial interface Write_type /*dyn*/ {
    void Grow(nint n);
}

} partial class printerᴛPrinter {

public static void Write(this ж<Printer> Ꮡp, ж<ast.Node> Ꮡnode, ж<ast.SourceFile> ᏑsourceFile, EmitTextWriter writer, ж<sourcemap.Generator> ᏑsourceMapGenerator) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var sourceMapGenerator = ref ᏑsourceMapGenerator.DerefOrNull();

    var savedCurrentSourceFile = p.currentSourceFile;
    var savedWriter = p.writer;
    var savedUniqueHelperNames = p.uniqueHelperNames;
    var savedSourceMapsDisabled = p.sourceMapsDisabled;
    var savedSourceMapGenerator = p.sourceMapGenerator;
    var savedSourceMapSource = p.sourceMapSource;
    sourcemap.SourceIndex savedSourceMapSourceIndex = p.sourceMapSourceIndex;
    var savedSourceMapLineCharCache = p.sourceMapLineCharCache;

    p.sourceMapsDisabled = ᏑsourceMapGenerator == nil;
    p.sourceMapGenerator = ᏑsourceMapGenerator;
    p.sourceMapSource = default;
    p.sourceMapSourceIndex = -1;
    p.sourceMapLineCharCache = default;

    p.setSourceFile(ᏑsourceFile);
    if (p.Options.OmitTrailingSemicolon) {
        writer = getTrailingSemicolonDeferringWriter(writer);
    }
    p.writer = writer;
    p.writer.Clear();
    if (ᏑsourceFile != nil) {
        var (grower, ok) = p.writer._<Write_type>(ᐧ); if (ok) {
            grower.Grow(len(sourceFile.Text()));
        }
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindTemplateHead: {
        Ꮡp.emitTemplateHead(node.AsTemplateHead());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateMiddle: {
        Ꮡp.emitTemplateMiddle(node.AsTemplateMiddle());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateTail: {
        Ꮡp.emitTemplateTail(node.AsTemplateTail());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        Ꮡp.emitIdentifierName(node.AsIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPrivateIdentifier: {
        Ꮡp.emitPrivateIdentifier(node.AsPrivateIdentifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        Ꮡp.emitQualifiedName(node.AsQualifiedName());
        break;
    }
    case var ᴛ when ᴛ == ast.KindComputedPropertyName: {
        Ꮡp.emitComputedPropertyName(node.AsComputedPropertyName());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTypeParameter: {
        Ꮡp.emitTypeParameter(node.AsTypeParameterDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        Ꮡp.emitParameter(node.AsParameterDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindDecorator: {
        Ꮡp.emitDecorator(node.AsDecorator());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertySignature: {
        Ꮡp.emitPropertySignature(node.AsPropertySignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        Ꮡp.emitPropertyDeclaration(node.AsPropertyDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMethodSignature: {
        Ꮡp.emitMethodSignature(node.AsMethodSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        Ꮡp.emitMethodDeclaration(node.AsMethodDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration: {
        Ꮡp.emitClassStaticBlockDeclaration(node.AsClassStaticBlockDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructor: {
        Ꮡp.emitConstructor(node.AsConstructorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        Ꮡp.emitGetAccessorDeclaration(node.AsGetAccessorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        Ꮡp.emitSetAccessorDeclaration(node.AsSetAccessorDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindCallSignature: {
        Ꮡp.emitCallSignature(node.AsCallSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructSignature: {
        Ꮡp.emitConstructSignature(node.AsConstructSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIndexSignature: {
        Ꮡp.emitIndexSignature(node.AsIndexSignatureDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectBindingPattern: {
        Ꮡp.emitObjectBindingPattern(node.AsBindingPattern());
        break;
    }
    case var ᴛ when ᴛ == ast.KindArrayBindingPattern: {
        Ꮡp.emitArrayBindingPattern(node.AsBindingPattern());
        break;
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        Ꮡp.emitBindingElement(node.AsBindingElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindTemplateSpan: {
        Ꮡp.emitTemplateSpan(node.AsTemplateSpan());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSemicolonClassElement: {
        p.emitSemicolonClassElement(node.AsSemicolonClassElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        Ꮡp.emitVariableDeclaration(node.AsVariableDeclaration());
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclarationList: {
        Ꮡp.emitVariableDeclarationList(node.AsVariableDeclarationList());
        break;
    }
    case var ᴛ when ᴛ == ast.KindModuleBlock: {
        Ꮡp.emitModuleBlock(node.AsModuleBlock());
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseBlock: {
        Ꮡp.emitCaseBlock(node.AsCaseBlock());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportClause: {
        Ꮡp.emitImportClause(node.AsImportClause());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamespaceImport: {
        Ꮡp.emitNamespaceImport(node.AsNamespaceImport());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamespaceExport: {
        Ꮡp.emitNamespaceExport(node.AsNamespaceExport());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamedImports: {
        Ꮡp.emitNamedImports(node.AsNamedImports());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportSpecifier: {
        Ꮡp.emitImportSpecifier(node.AsImportSpecifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamedExports: {
        Ꮡp.emitNamedExports(node.AsNamedExports());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportSpecifier: {
        Ꮡp.emitExportSpecifier(node.AsExportSpecifier());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportAttributes: {
        Ꮡp.emitImportAttributes(node.AsImportAttributes());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportAttribute: {
        Ꮡp.emitImportAttribute(node.AsImportAttribute());
        break;
    }
    case var ᴛ when ᴛ == ast.KindExternalModuleReference: {
        Ꮡp.emitExternalModuleReference(node.AsExternalModuleReference());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxText: {
        p.emitJsxText(node.AsJsxText());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxOpeningElement: {
        Ꮡp.emitJsxOpeningElement(node.AsJsxOpeningElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxOpeningFragment: {
        p.emitJsxOpeningFragment(node.AsJsxOpeningFragment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxClosingElement: {
        Ꮡp.emitJsxClosingElement(node.AsJsxClosingElement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxClosingFragment: {
        p.emitJsxClosingFragment(node.AsJsxClosingFragment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxAttribute: {
        Ꮡp.emitJsxAttribute(node.AsJsxAttribute());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxAttributes: {
        Ꮡp.emitJsxAttributes(node.AsJsxAttributes());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxSpreadAttribute: {
        Ꮡp.emitJsxSpreadAttribute(node.AsJsxSpreadAttribute());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxExpression: {
        Ꮡp.emitJsxExpression(node.AsJsxExpression());
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxNamespacedName: {
        Ꮡp.emitJsxNamespacedName(node.AsJsxNamespacedName());
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseClause: {
        Ꮡp.emitCaseClause(node.AsCaseOrDefaultClause());
        break;
    }
    case var ᴛ when ᴛ == ast.KindDefaultClause: {
        Ꮡp.emitDefaultClause(node.AsCaseOrDefaultClause());
        break;
    }
    case var ᴛ when ᴛ == ast.KindHeritageClause: {
        Ꮡp.emitHeritageClause(node.AsHeritageClause());
        break;
    }
    case var ᴛ when ᴛ == ast.KindCatchClause: {
        Ꮡp.emitCatchClause(node.AsCatchClause());
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
        Ꮡp.emitPropertyAssignment(node.AsPropertyAssignment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        Ꮡp.emitShorthandPropertyAssignment(node.AsShorthandPropertyAssignment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSpreadAssignment: {
        Ꮡp.emitSpreadAssignment(node.AsSpreadAssignment());
        break;
    }
    case var ᴛ when ᴛ == ast.KindEnumMember: {
        Ꮡp.emitEnumMember(node.AsEnumMember());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        Ꮡp.emitSourceFile(node.AsSourceFile());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNotEmittedTypeElement: {
        p.emitNotEmittedTypeElement(node.AsNotEmittedTypeElement());
        break;
    }
    default: {
        switch (ᐧ) {
        case {} when ast.IsTypeNode(Ꮡnode): {
            Ꮡp.emitTypeNodeOutsideExtends(Ꮡnode);
            break;
        }
        case {} when ast.IsStatement(Ꮡnode): {
            Ꮡp.emitStatement(Ꮡnode);
            break;
        }
        case {} when ast.IsExpression(Ꮡnode): {
            Ꮡp.emitExpression(Ꮡnode, ast.OperatorPrecedenceLowest);
            break;
        }
        case {} when ast.IsKeywordKind(node.Kind): {
            p.emitKeywordNode(Ꮡnode);
            break;
        }
        case {} when ast.IsPunctuationKind(node.Kind): {
            p.emitPunctuationNode(Ꮡnode);
            break;
        }
        case {} when ast.IsJSDocKind(node.Kind): {
            p.emitJSDocNode(Ꮡnode);
            break;
        }
        default: {
            throw panic(fmt.Sprintf("unhandled Node: %v"u8, node.Kind));
            break;
        }}

        break;
    }}

    p.currentSourceFile = savedCurrentSourceFile;
    p.writer = savedWriter;
    p.uniqueHelperNames = savedUniqueHelperNames;
    p.sourceMapsDisabled = savedSourceMapsDisabled;
    p.sourceMapGenerator = savedSourceMapGenerator;
    p.sourceMapSource = savedSourceMapSource;
    p.sourceMapSourceIndex = savedSourceMapSourceIndex;
    p.sourceMapLineCharCache = savedSourceMapLineCharCache;
}

internal static ж<commentState> emitCommentsBeforeNode(this ref Printer p, ж<ast.Node> node) {
    if (!p.shouldEmitComments(node)) {
        return default;
    }

    var emitFlags = p.emitContext.EmitFlags(node);
    var commentRange = p.emitContext.CommentRange(node);
    nint containerPos = p.containerPos;
    nint containerEnd = p.containerEnd;
    nint declarationListContainerEnd = p.declarationListContainerEnd;

    p.emitLeadingCommentsOfNode(node, emitFlags, commentRange);
    p.emitLeadingSyntheticCommentsOfNode(node, emitFlags);
    if ((EmitFlags)(emitFlags & EFNoNestedComments) != 0) {
        p.commentsDisabled = true;
    }

    var c = p.commentStateArena.New();
    c.Value = new commentState(emitFlags, commentRange, containerPos, containerEnd, declarationListContainerEnd);
    return c;
}

internal static void emitCommentsAfterNode(this ref Printer p, ж<ast.Node> node, ж<commentState> Ꮡstate) {
    ref var state = ref Ꮡstate.DerefOrNull();

    if (Ꮡstate == nil) {
        return;
    }

    var emitFlags = state.emitFlags;
    var commentRange = state.commentRange;
    nint containerPos = state.containerPos;
    nint containerEnd = state.containerEnd;
    nint declarationListContainerEnd = state.declarationListContainerEnd;

    if ((EmitFlags)(emitFlags & EFNoNestedComments) != 0) {
        p.commentsDisabled = false;
    }

    p.emitTrailingSyntheticCommentsOfNode(node, emitFlags);
    p.emitTrailingCommentsOfNode(node, emitFlags, commentRange, containerPos, containerEnd, declarationListContainerEnd);

    var typeNode = p.emitContext.GetTypeNode(node); if (typeNode != nil) {
        p.emitTrailingCommentsOfNode(node, emitFlags, typeNode.Loc, containerPos, containerEnd, declarationListContainerEnd);
    }
}

internal static (ж<commentState>, nint) emitCommentsBeforeToken(this ref Printer p, ast.Kind token, nint pos, ж<ast.Node> ᏑcontextNode, tokenEmitFlags flags) {
    ref var contextNode = ref ᏑcontextNode.DerefOrNull();

    if ((tokenEmitFlags)(flags & tefNoComments) != 0 || p.commentsDisabled) {
        if (p.currentSourceFile != nil && !ast.PositionIsSynthesized(pos)) {
            pos = scanner.SkipTrivia(p.currentSourceFile.Text(), pos);
        }
        return (default, pos);
    }

    nint startPos = pos;
    if (p.currentSourceFile != nil) {
        pos = scanner.SkipTrivia(p.currentSourceFile.Text(), startPos);
    }

    var node = p.emitContext.ParseNode(ᏑcontextNode);
    var isSimilarNode = node != nil && node.Kind == contextNode.Kind;
    if (!isSimilarNode) {
        return (default, pos);
    }

    if (contextNode.Pos() != startPos) {
        var indentLeading = (tokenEmitFlags)(flags & tefIndentLeadingComments) != 0;
        var needsIndent = indentLeading && p.currentSourceFile != nil && !PositionsAreOnSameLine(startPos, pos, p.currentSourceFile);
        p.increaseIndentIf(needsIndent);
        p.emitLeadingComments(startPos, false);
        p.decreaseIndentIf(needsIndent);
    }

    return (p.commentStateArena.New(), pos);
}

internal static void emitCommentsAfterToken(this ref Printer p, ast.Kind token, nint pos, ж<ast.Node> ᏑcontextNode, ж<commentState> state) {
    ref var contextNode = ref ᏑcontextNode.DerefOrNull();

    if (state == nil) {
        return;
    }

    if (contextNode.End() != pos) {
        var isJsxExprContext = contextNode.Kind == ast.KindJsxExpression;
        p.emitTrailingComments(pos, core.IfElse(isJsxExprContext, commentSeparatorNone, commentSeparatorBefore));
    }
}

internal static ж<commentState> emitDetachedCommentsBeforeStatementList(this ref Printer p, ж<ast.Node> node, core.TextRange detachedRange) {
    if (!p.shouldEmitDetachedComments(node)) {
        return default;
    }

    ref var emitFlags = ref heap<EmitFlags>(p.emitContext.EmitFlags(node), out var ᏑemitFlags);
    ref var containerPos = ref heap<nint>(p.containerPos, out var ᏑcontainerPos);
    ref var containerEnd = ref heap<nint>(p.containerEnd, out var ᏑcontainerEnd);
    ref var declarationListContainerEnd = ref heap<nint>(p.declarationListContainerEnd, out var ᏑdeclarationListContainerEnd);
    var skipLeadingComments = ast.PositionIsSynthesized(detachedRange.Pos()) || (EmitFlags)(emitFlags & EFNoLeadingComments) != 0;

    if (!skipLeadingComments) {
        p.emitDetachedCommentsAndUpdateCommentsInfo(detachedRange);
    }

    if ((EmitFlags)(emitFlags & EFNoNestedComments) != 0) {
        p.commentsDisabled = true;
    }

    return Ꮡ(new commentState(emitFlags, detachedRange, containerPos, containerEnd, declarationListContainerEnd));
}

internal static void emitDetachedCommentsAfterStatementList(this ref Printer p, ж<ast.Node> node, core.TextRange detachedRange, ж<commentState> Ꮡstate) {
    ref var state = ref Ꮡstate.DerefOrNull();

    if (Ꮡstate == nil) {
        return;
    }

    var emitFlags = state.emitFlags;
    var skipTrailingComments = p.commentsDisabled || ast.PositionIsSynthesized(detachedRange.End()) || (EmitFlags)(emitFlags & EFNoTrailingComments) != 0;

    if (!skipTrailingComments) {
        var hasWrittenComment = p.emitLeadingComments(detachedRange.End(), false);
        if (hasWrittenComment && !p.writer.IsAtStartOfLine()) {
            p.writeLine();
        }
    }
}

internal static void emitLeadingCommentsOfNode(this ref Printer p, ж<ast.Node> Ꮡnode, EmitFlags emitFlags, core.TextRange commentRange) {
    ref var node = ref Ꮡnode.DerefOrNull();

    nint pos = commentRange.Pos();
    nint end = commentRange.End();

    if ((!ast.PositionIsSynthesized(pos) || !ast.PositionIsSynthesized(end)) && pos != end) {
        var skipLeadingComments = ast.PositionIsSynthesized(pos) || (EmitFlags)(emitFlags & EFNoLeadingComments) != 0 || node.Kind == ast.KindJsxText;
        var skipTrailingComments = ast.PositionIsSynthesized(end) || (EmitFlags)(emitFlags & EFNoTrailingComments) != 0 || node.Kind == ast.KindJsxText;

        if (!skipLeadingComments) {
            p.emitLeadingComments(pos, node.Kind == ast.KindNotEmittedStatement);
        }

        if (!skipLeadingComments || (pos >= 0 && ((EmitFlags)(emitFlags & EFNoLeadingComments)) != 0)) {
            p.containerPos = pos;
        }

        if (!skipTrailingComments || (end >= 0 && ((EmitFlags)(emitFlags & EFNoTrailingComments)) != 0)) {
            p.containerEnd = end;

            if (node.Kind == ast.KindVariableDeclarationList) {
                p.declarationListContainerEnd = end;
            }
        }
    }
}

internal static void emitTrailingCommentsOfNode(this ref Printer p, ж<ast.Node> Ꮡnode, EmitFlags emitFlags, core.TextRange commentRange, nint containerPos, nint containerEnd, nint declarationListContainerEnd) {
    ref var node = ref Ꮡnode.DerefOrNull();

    nint pos = commentRange.Pos();
    nint end = commentRange.End();
    var skipTrailingComments = end < 0 || ((EmitFlags)(emitFlags & EFNoTrailingComments)) != 0 || node.Kind == ast.KindJsxText;
    if ((!ast.PositionIsSynthesized(pos) || !ast.PositionIsSynthesized(end)) && pos != end) {
        p.containerPos = containerPos;
        p.containerEnd = containerEnd;
        p.declarationListContainerEnd = declarationListContainerEnd;

        if (!skipTrailingComments && node.Kind != ast.KindNotEmittedStatement) {
            p.emitTrailingComments(end, commentSeparatorBefore);
        }
    }
}

internal static void emitLeadingSyntheticCommentsOfNode(this ref Printer p, ж<ast.Node> node, EmitFlags emitFlags) {
    if ((EmitFlags)(emitFlags & EFNoLeadingComments) != 0) {
        return;
    }
    var synth = p.emitContext.GetSyntheticLeadingComments(node);
    foreach (var (_, c) in range(synth)) {
        p.emitLeadingSynthesizedComment(c);
    }
}

internal static void emitLeadingSynthesizedComment(this ref Printer p, SynthesizedComment comment) {
    if (comment.HasLeadingNewLine || comment.Kind == ast.KindSingleLineCommentTrivia) {
        p.writer.WriteLine();
    }
    p.writeSynthesizedComment(comment);
    if (comment.HasTrailingNewLine || comment.Kind == ast.KindSingleLineCommentTrivia) {
        p.writer.WriteLine();
    } else {
        p.writer.WriteSpace(" ");
    }
}

internal static void emitTrailingSyntheticCommentsOfNode(this ref Printer p, ж<ast.Node> node, EmitFlags emitFlags) {
    if ((EmitFlags)(emitFlags & EFNoTrailingComments) != 0) {
        return;
    }
    var synth = p.emitContext.GetSyntheticTrailingComments(node);
    foreach (var (_, c) in range(synth)) {
        p.emitTrailingSynthesizedComment(c);
    }
}

internal static void emitTrailingSynthesizedComment(this ref Printer p, SynthesizedComment comment) {
    if (!p.writer.IsAtStartOfLine()) {
        p.writer.WriteSpace(" ");
    }
    p.writeSynthesizedComment(comment);
    if (comment.HasTrailingNewLine) {
        p.writer.WriteLine();
    }
}

} partial class printer_package {

internal static @string formatSynthesizedComment(SynthesizedComment comment) {
    if (comment.Kind == ast.KindMultiLineCommentTrivia) {
        return "/*"u8 + comment.Text + "*/"u8;
    }
    return "//"u8 + comment.Text;
}

} partial class printerᴛPrinter {

internal static void writeSynthesizedComment(this ref Printer p, SynthesizedComment comment) {
    @string text = formatSynthesizedComment(comment);
    slice<core.TextPos> lineMap = default;
    if (comment.Kind == ast.KindMultiLineCommentTrivia) {
        lineMap = core.ComputeECMALineStarts(text);
    }
    p.writeCommentRangeWorker(text, lineMap, comment.Kind, core.NewTextRange(0, len(text)));
}

internal static bool emitLeadingComments(this ref Printer p, nint pos, bool elided) {
    if (p.commentsDisabled || p.currentSourceFile == nil || ast.PositionIsSynthesized(pos) || pos == p.containerPos) {
        return false;
    }

    var tripleSlash = core.TSUnknown;
    if (!elided) {
        if (pos == 0 && p.currentSourceFile != nil && p.currentSourceFile.IsDeclarationFile) {
            tripleSlash = core.TSFalse;
        }
    } else if (pos == 0) {
        tripleSlash = core.TSTrue;
    } else {
        return false;
    }

    if (p.detachedCommentsInfo.Len() > 0) {
        var info = p.detachedCommentsInfo.Peek(); if (info.nodePos == pos) {
            pos = p.detachedCommentsInfo.Pop().detachedCommentEndPos;
        }
    }

    slice<ast.CommentRange> comments = default;
    foreach (var comment in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), p.currentSourceFile.Text(), pos).Invoke)) {
        if (p.shouldWriteComment(comment) && p.shouldEmitCommentIfTripleSlash(comment, tripleSlash)) {
            comments = append(comments, comment);
        }
    }

    if (len(comments) > 0 && p.shouldEmitNewLineBeforeLeadingCommentOfPosition(pos, comments[0].TextRange.Pos())) {
        p.writeLine();
    }

    return p.emitComments(comments, commentSeparatorAfter);
}

internal static bool shouldEmitCommentIfTripleSlash(this ref Printer p, ast.CommentRange comment, core.Tristate tripleSlash) {
    switch (tripleSlash) {
    case var ᴛ when ᴛ == core.TSTrue: {
        return p.isTripleSlashComment(comment);
    }
    case var ᴛ when ᴛ == core.TSFalse: {
        return !p.isTripleSlashComment(comment);
    }
    default: {
        return true;
    }}

}

internal static bool shouldEmitNewLineBeforeLeadingCommentOfPosition(this ref Printer p, nint pos, nint commentPos) {
    return p.currentSourceFile != nil && pos != commentPos && scanner.ComputeLineOfPosition(p.currentSourceFile.ECMALineMap(), pos) != scanner.ComputeLineOfPosition(p.currentSourceFile.ECMALineMap(), commentPos);
}

internal static void emitLeadingCommentsOfPosition(this ref Printer p, nint pos) {
    if (p.commentsDisabled || pos == -1) {
        return;
    }

    p.emitLeadingComments(pos, false);
}

internal static void emitTrailingComments(this ref Printer p, nint pos, commentSeparator commentSeparator) {
    if (p.commentsDisabled) {
        return;
    }
    if (p.commentsDisabled || p.currentSourceFile == nil || p.containerEnd != -1 && (pos == p.containerEnd || pos == p.declarationListContainerEnd)) {
        return;
    }

    slice<ast.CommentRange> comments = default;
    foreach (var comment in range<ast.CommentRange>(scanner.GetTrailingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), p.currentSourceFile.Text(), pos).Invoke)) {
        if (p.shouldWriteComment(comment)) {
            comments = append(comments, comment);
        }
    }

    p.emitComments(comments, commentSeparator);
}

internal static void emitTrailingCommentsOfPosition(this ref Printer p, nint pos, bool prefixSpace, bool forceNoNewline) {
    if (p.commentsDisabled || p.currentSourceFile == nil) {
        return;
    }
    if (p.containerEnd != -1 && (pos == p.containerEnd || pos == p.declarationListContainerEnd)) {
        return;
    }

    slice<ast.CommentRange> comments = default;
    foreach (var comment in range<ast.CommentRange>(scanner.GetTrailingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), p.currentSourceFile.Text(), pos).Invoke)) {
        comments = append(comments, comment);
    }
    if (len(comments) == 0) {
        return;
    }

    foreach (var (_, comment) in range(comments)) {
        if (prefixSpace) {
            if (!p.shouldWriteComment(comment)) {
                continue;
            }
            if (!p.writer.IsAtStartOfLine()) {
                p.writeSpace();
            }
            p.emitComment(comment);
            if (comment.HasTrailingNewLine) {
                p.writeLine();
            }
            continue;
        }

        p.emitComment(comment);
        switch (ᐧ) {
        case {} when forceNoNewline: {
            if (comment.Kind == ast.KindSingleLineCommentTrivia) {
                p.writeLine();
            }
            break;
        }
        case {} when comment.HasTrailingNewLine: {
            p.writeLine();
            break;
        }
        default: {
            p.writeSpace();
            break;
        }}

    }
}

internal static void emitDetachedCommentsAndUpdateCommentsInfo(this ref Printer p, core.TextRange textRange) {
    if (p.currentSourceFile == nil) {
        return;
    }
    var (currentDetachedCommentInfo, ok) = p.emitDetachedComments(textRange); if (ok) {
        p.detachedCommentsInfo.Push(currentDetachedCommentInfo);
    }
}

internal static (detachedCommentsInfo result, bool hasResult) emitDetachedComments(this ref Printer p, core.TextRange textRange) {
    detachedCommentsInfo result = default;
    bool hasResult = default;
    var detachedCommentsᴷ = new stackAppend<ast.CommentRange>();
    var commentsToEmitᴷ = new stackAppend<ast.CommentRange>();

    if (p.currentSourceFile == nil) {
        return (result, hasResult);
    }

    @string text = p.currentSourceFile.Text();
    var lineMap = p.currentSourceFile.ECMALineMap();

    slice<ast.CommentRange> leadingComments = default;
    if (p.commentsDisabled) {
        if (textRange.Pos() == 0) {
            foreach (var comment in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), text, textRange.Pos()).Invoke)) {
                if (IsPinnedComment(text, comment)) {
                    leadingComments = append(leadingComments, comment);
                }
            }
        }
    } else {
        leadingComments = slices.Collect(scanner.GetLeadingCommentRanges(p.emitContext.Factory.of(NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), text, textRange.Pos()));
    }

    if (len(leadingComments) > 0) {
        slice<ast.CommentRange> detachedComments = default;
        ast.CommentRange lastComment = new(nil);
        foreach (var (i, comment) in range(leadingComments)) {
            if (i > 0) {
                nint lastCommentLine = scanner.ComputeLineOfPosition(lineMap, lastComment.TextRange.End());
                nint commentLine = scanner.ComputeLineOfPosition(lineMap, comment.TextRange.Pos());

                if (commentLine >= lastCommentLine + 2) {
                    break;
                }
            }

            detachedComments = append(detachedCommentsᴷ, detachedComments, comment);
            lastComment = comment;
        }

        if (len(detachedComments) > 0) {
            nint lastCommentLine = scanner.ComputeLineOfPosition(lineMap, core.LastOrNil(detachedComments).TextRange.End());
            nint nodeLine = scanner.ComputeLineOfPosition(lineMap, scanner.SkipTrivia(text, textRange.Pos()));
            if (nodeLine >= lastCommentLine + 2) {
                slice<ast.CommentRange> commentsToEmit = default;
                foreach (var (_, comment) in range(detachedComments)) {
                    if (p.shouldWriteComment(comment)) {
                        commentsToEmit = append(commentsToEmitᴷ, commentsToEmit, comment);
                    }
                }

                if (len(commentsToEmit) > 0) {
                    if (p.shouldEmitNewLineBeforeLeadingCommentOfPosition(textRange.Pos(), commentsToEmit[0].TextRange.Pos())) {
                        p.writeLine();
                    }

                    p.emitComments(commentsToEmit, commentSeparatorAfter);
                }
                result = new detachedCommentsInfo(nodePos: textRange.Pos(), detachedCommentEndPos: core.LastOrNil(detachedComments).TextRange.End());
                hasResult = true;
            }
        }
    }
    return (result, hasResult);
}

} partial class printer_package {

partial struct commentSeparator /*num:uint32*/;

internal static commentSeparator commentSeparatorNone => /* iota */ 0;
internal static commentSeparator commentSeparatorBefore => 1;
internal static commentSeparator commentSeparatorAfter => 2;

} partial class printerᴛPrinter {

internal static bool emitComments(this ref Printer p, slice<ast.CommentRange> comments, commentSeparator commentSeparator) {
    var interveningSeparator = false;
    if (len(comments) == 0) {
        return false;
    }

    if (commentSeparator == commentSeparatorBefore) {
        p.writeSpace();
    }

    foreach (var (_, comment) in range(comments)) {
        if (interveningSeparator) {
            p.writeSpace();
            interveningSeparator = false;
        }

        p.emitComment(comment);

        if (comment.Kind == ast.KindSingleLineCommentTrivia || comment.HasTrailingNewLine && commentSeparator != commentSeparatorNone) {
            p.writeLine();
        } else {
            interveningSeparator = commentSeparator != commentSeparatorNone;
        }
    }

    if (interveningSeparator && commentSeparator == commentSeparatorAfter) {
        p.writeSpace();
    }

    return true;
}

internal static void emitComment(this ref Printer p, ast.CommentRange comment) {
    p.emitPos(comment.TextRange.Pos());
    p.writeCommentRange(comment);
    p.emitPos(comment.TextRange.End());
}

internal static bool isTripleSlashComment(this ref Printer p, ast.CommentRange comment) {
    return p.currentSourceFile != nil && IsRecognizedTripleSlashComment(p.currentSourceFile.Text(), comment);
}

internal static void setSourceMapSource(this ref Printer p, sourcemap.Source source) {
    if (p.sourceMapsDisabled) {
        return;
    }

    p.sourceMapSource = source;
    p.sourceMapLineCharCache = newLineCharacterCache(source);
    if (AreEqual(p.mostRecentSourceMapSource, source)) {
        p.sourceMapSourceIndex = p.mostRecentSourceMapSourceIndex;
        return;
    }

    p.sourceMapSourceIsJson = tspath.FileExtensionIs(source.FileName(), tspath.ExtensionJson);
    if (p.sourceMapSourceIsJson) {
        return;
    }

    p.sourceMapSourceIndex = p.sourceMapGenerator.AddSource(source.FileName());
    if (p.Options.InlineSources) {
        var err = p.sourceMapGenerator.SetSourceContent(p.sourceMapSourceIndex, source.Text()); if (err != nil) {
            throw panic(err);
        }
    }

    p.mostRecentSourceMapSource = source;
    p.mostRecentSourceMapSourceIndex = p.sourceMapSourceIndex;
}

internal static void emitPos(this ref Printer p, nint pos) {
    if (p.sourceMapsDisabled || p.sourceMapSource == nil || p.sourceMapGenerator == nil || p.sourceMapSourceIsJson || ast.PositionIsSynthesized(pos)) {
        return;
    }

    var source = p.sourceMapSource;
    sourcemap.SourceIndex sourceIndex = p.sourceMapSourceIndex;
    var lineCharCache = p.sourceMapLineCharCache;
    if (p.MapSourcePosition != nil) {
        var (mappedSource, mappedPos, ok) = p.MapSourcePosition(source, pos);
        if (!ok) {
            var err = p.sourceMapGenerator.AddGeneratedMapping(p.writer.GetLine(), p.writer.GetColumn()); if (err != nil) {
                throw panic(err);
            }
            return;
        }
        pos = mappedPos;
        if (!AreEqual(mappedSource, source)) {
            var savedSource = p.sourceMapSource;
            sourcemap.SourceIndex savedSourceIndex = p.sourceMapSourceIndex;
            var savedSourceIsJson = p.sourceMapSourceIsJson;
            var savedLineCharCache = p.sourceMapLineCharCache;
            p.setSourceMapSource(mappedSource);
            sourceIndex = p.sourceMapSourceIndex;
            lineCharCache = p.sourceMapLineCharCache;
            p.sourceMapSource = savedSource;
            p.sourceMapSourceIndex = savedSourceIndex;
            p.sourceMapSourceIsJson = savedSourceIsJson;
            p.sourceMapLineCharCache = savedLineCharCache;
        }
    }

    var (sourceLine, sourceCharacter) = lineCharCache.getLineAndCharacter(pos);
    {
        var err = p.sourceMapGenerator.AddSourceMapping(
            p.writer.GetLine(),
            p.writer.GetColumn(),
            sourceIndex,
            sourceLine,
            sourceCharacter); if (err != nil) {
            throw panic(err);
        }
    }
}

internal static void emitSourcePos(this ref Printer p, sourcemap.Source source, nint pos) {
    if (!AreEqual(source, p.sourceMapSource)) {
        var savedSourceMapSource = p.sourceMapSource;
        sourcemap.SourceIndex savedSourceMapSourceIndex = p.sourceMapSourceIndex;
        var savedSourceMapLineCharCache = p.sourceMapLineCharCache;
        p.setSourceMapSource(source);
        p.emitPos(pos);
        p.sourceMapSource = savedSourceMapSource;
        p.sourceMapSourceIndex = savedSourceMapSourceIndex;
        p.sourceMapLineCharCache = savedSourceMapLineCharCache;
    } else {
        p.emitPos(pos);
    }
}

internal static ж<sourceMapState> emitSourceMapsBeforeNode(this ref Printer p, ж<ast.Node> node) {
    if (!p.shouldEmitSourceMaps(node)) {
        return default;
    }

    var emitFlags = p.emitContext.EmitFlags(node);
    var loc = p.emitContext.SourceMapRange(node);

    if (!ast.IsNotEmittedStatement(node) && (EmitFlags)(emitFlags & EFNoLeadingSourceMap) == 0 && p.currentSourceFile != nil && !ast.PositionIsSynthesized(loc.Pos())) {
        p.emitSourcePos(p.sourceMapSource, scanner.SkipTrivia(p.currentSourceFile.Text(), loc.Pos()));
    }

    if ((EmitFlags)(emitFlags & EFNoNestedSourceMaps) != 0) {
        p.sourceMapsDisabled = true;
    }

    var state = p.sourceMapStateArena.New();
    state.Value = new sourceMapState(emitFlags, loc, false);
    return state;
}

internal static void emitSourceMapsAfterNode(this ref Printer p, ж<ast.Node> node, ж<sourceMapState> ᏑpreviousState) {
    ref var previousState = ref ᏑpreviousState.DerefOrNull();

    if (ᏑpreviousState == nil) {
        return;
    }

    var emitFlags = previousState.emitFlags;
    var loc = previousState.sourceMapRange;

    if ((EmitFlags)(emitFlags & EFNoNestedSourceMaps) != 0) {
        p.sourceMapsDisabled = false;
    }

    if (!ast.IsNotEmittedStatement(node) && (EmitFlags)(emitFlags & EFNoTrailingSourceMap) == 0 && !ast.PositionIsSynthesized(loc.End())) {
        p.emitSourcePos(p.sourceMapSource, loc.End());
    }
}

internal static ж<sourceMapState> emitSourceMapsBeforeToken(this ref Printer p, ast.Kind token, nint pos, ж<ast.Node> contextNode, tokenEmitFlags flags) {
    if (!p.shouldEmitTokenSourceMaps(token, pos, contextNode, flags)) {
        return default;
    }

    var emitFlags = p.emitContext.EmitFlags(contextNode);
    var (loc, hasLoc) = p.emitContext.TokenSourceMapRange(contextNode, token);
    if (hasLoc) {
        pos = loc.Pos();
    }
    if (pos >= 0 && p.currentSourceFile != nil) {
        pos = scanner.SkipTrivia(p.currentSourceFile.Text(), pos);
    }
    if ((EmitFlags)(emitFlags & EFNoTokenLeadingSourceMaps) == 0 && pos >= 0) {
        p.emitSourcePos(p.sourceMapSource, pos);
    }

    var state = p.sourceMapStateArena.New();
    state.Value = new sourceMapState(emitFlags, loc, hasLoc);
    return state;
}

internal static void emitSourceMapsAfterToken(this ref Printer p, ast.Kind token, nint pos, ж<ast.Node> contextNode, ж<sourceMapState> ᏑpreviousState) {
    ref var previousState = ref ᏑpreviousState.DerefOrNull();

    if (ᏑpreviousState == nil) {
        return;
    }

    var emitFlags = previousState.emitFlags;
    var loc = previousState.sourceMapRange;
    var hasLoc = previousState.hasTokenSourceMapRange;
    if ((EmitFlags)(emitFlags & EFNoTokenTrailingSourceMaps) == 0) {
        if (hasLoc) {
            pos = loc.End();
        }
        if (pos >= 0) {
            p.emitSourcePos(p.sourceMapSource, pos);
        }
    }
}

internal static bool shouldReuseTempVariableScope(this ref Printer p, ж<ast.Node> node) {
    return node != nil && (EmitFlags)(p.emitContext.EmitFlags(node) & EFReuseTempVariableScope) != 0;
}

internal static void pushNameGenerationScope(this ref Printer p, ж<ast.Node> node) {
    p.nameGenerator.PushScope(p.shouldReuseTempVariableScope(node));
}

internal static void popNameGenerationScope(this ref Printer p, ж<ast.Node> node) {
    p.nameGenerator.PopScope(p.shouldReuseTempVariableScope(node));
}

internal static void generateAllNames(this ж<Printer> Ꮡp, ж<ast.NodeList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil) {
        return;
    }
    foreach (var (_, node) in range(nodes.Nodes)) {
        Ꮡp.generateNames(node);
    }
}

internal static void generateNames(this ж<Printer> Ꮡp, ж<ast.Node> Ꮡnode) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindBlock || ᴛ == ast.KindCaseClause || ᴛ == ast.KindDefaultClause: {
        Ꮡp.generateAllNames(node.StatementList());
        break;
    }
    case var ᴛ when ᴛ == ast.KindLabeledStatement || ᴛ == ast.KindWithStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement: {
        Ꮡp.generateNames(node.Statement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindIfStatement: {
        Ꮡp.generateNames(node.AsIfStatement().ThenStatement);
        Ꮡp.generateNames(node.AsIfStatement().ElseStatement);
        break;
    }
    case var ᴛ when ᴛ == ast.KindForStatement || ᴛ == ast.KindForOfStatement || ᴛ == ast.KindForInStatement: {
        Ꮡp.generateNames(node.Initializer());
        Ꮡp.generateNames(node.Statement());
        break;
    }
    case var ᴛ when ᴛ == ast.KindSwitchStatement: {
        Ꮡp.generateNames(node.AsSwitchStatement().CaseBlock);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCaseBlock: {
        Ꮡp.generateAllNames(node.AsCaseBlock().Clauses);
        break;
    }
    case var ᴛ when ᴛ == ast.KindTryStatement: {
        Ꮡp.generateNames(node.AsTryStatement().TryBlock);
        Ꮡp.generateNames(node.AsTryStatement().CatchClause);
        Ꮡp.generateNames(node.AsTryStatement().FinallyBlock);
        break;
    }
    case var ᴛ when ᴛ == ast.KindCatchClause: {
        Ꮡp.generateNames(node.AsCatchClause().VariableDeclaration);
        Ꮡp.generateNames(node.AsCatchClause().Block);
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableStatement: {
        Ꮡp.generateNames(node.AsVariableStatement().DeclarationList);
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclarationList: {
        Ꮡp.generateAllNames(node.AsVariableDeclarationList().Declarations);
        break;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindParameter || ᴛ == ast.KindBindingElement || ᴛ == ast.KindClassDeclaration: {
        Ꮡp.generateNameIfNeeded(node.Name());
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        Ꮡp.generateNameIfNeeded(node.Name());
        if (p.shouldReuseTempVariableScope(Ꮡnode)) {
            Ꮡp.generateAllNames((~node.AsFunctionDeclaration()).Parameters);
            Ꮡp.generateNames((~node.AsFunctionDeclaration()).Body);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectBindingPattern || ᴛ == ast.KindArrayBindingPattern: {
        Ꮡp.generateAllNames(node.ElementList());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindJSImportDeclaration: {
        Ꮡp.generateNames((~node.AsImportDeclaration()).ImportClause);
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportClause: {
        Ꮡp.generateNameIfNeeded(node.AsImportClause().Name());
        Ꮡp.generateNames(node.AsImportClause().NamedBindings);
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamespaceImport || ᴛ == ast.KindNamespaceExport: {
        Ꮡp.generateNameIfNeeded(node.Name());
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamedImports: {
        Ꮡp.generateAllNames(node.ElementList());
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportSpecifier: {
        var n = node.AsImportSpecifier();
        if ((~n).PropertyName != nil) {
            Ꮡp.generateNameIfNeeded((~n).PropertyName);
        } else {
            Ꮡp.generateNameIfNeeded(n.Name());
        }
        break;
    }}

}

internal static void generateAllMemberNames(this ж<Printer> Ꮡp, ж<ast.NodeList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil) {
        return;
    }
    foreach (var (_, node) in range(nodes.Nodes)) {
        Ꮡp.generateMemberNames(node);
    }
}

internal static void generateMemberNames(this ж<Printer> Ꮡp, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindShorthandPropertyAssignment || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertySignature || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
        Ꮡp.generateNameIfNeeded(node.Name());
        break;
    }}

}

internal static void generateNameIfNeeded(this ж<Printer> Ꮡp, ж<astꓸDeclarationName> name) {
    if (name != nil) {
        if (ast.IsMemberName(name)) {
            Ꮡp.generateName(name);
        } else if (ast.IsBindingPattern(name)) {
            Ꮡp.generateNames(name);
        }
    }
}

internal static void generateName(this ж<Printer> Ꮡp, ж<astꓸMemberName> name) {
    _ = Ꮡp.ᏑnameGenerator.GenerateName(name);
}

internal static bool isFileLevelUniqueNameInCurrentFile(this ref Printer p, @string name, bool _) {
    if (p.currentSourceFile != nil) {
        return p.emitContext.IsFileLevelUniqueName(p.currentSourceFile, name, p.HasGlobalName);
    } else {
        return true;
    }
}

internal static printerState enterNode(this ref Printer p, ж<ast.Node> node) {
    var state = new printerState();

    if (p.OnBeforeEmitNode != nil) {
        p.OnBeforeEmitNode(node);
    }

    state.commentState = p.emitCommentsBeforeNode(node);
    state.sourceMapState = p.emitSourceMapsBeforeNode(node);
    return state;
}

internal static void exitNode(this ref Printer p, ж<ast.Node> node, printerState previousState) {
    p.emitSourceMapsAfterNode(node, previousState.sourceMapState);
    p.emitCommentsAfterNode(node, previousState.commentState);

    if (p.OnAfterEmitNode != nil) {
        p.OnAfterEmitNode(node);
    }
}

internal static printerState enterTokenNode(this ref Printer p, ж<ast.Node> node, tokenEmitFlags flags) {
    var state = new printerState();

    if (p.OnBeforeEmitToken != nil) {
        p.OnBeforeEmitToken(node);
    }

    if ((tokenEmitFlags)(flags & tefNoComments) == 0) {
        state.commentState = p.emitCommentsBeforeNode(node);
    }
    if ((tokenEmitFlags)(flags & tefNoSourceMaps) == 0) {
        state.sourceMapState = p.emitSourceMapsBeforeNode(node);
    }
    return state;
}

internal static void exitTokenNode(this ref Printer p, ж<ast.Node> node, printerState previousState) {
    p.emitSourceMapsAfterNode(node, previousState.sourceMapState);
    p.emitCommentsAfterNode(node, previousState.commentState);

    if (p.OnAfterEmitToken != nil) {
        p.OnAfterEmitToken(node);
    }
}

} partial class printer_package {

partial struct tokenEmitFlags /*num:uint32*/;

internal static tokenEmitFlags tefNoComments => /* 1 << iota */ 1;
internal static tokenEmitFlags tefIndentLeadingComments => 2;
internal static tokenEmitFlags tefNoSourceMaps => 4;
internal static tokenEmitFlags tefNone => 0;

} partial class printerᴛPrinter {

internal static (printerState, nint) enterToken(this ref Printer p, ast.Kind token, nint pos, ж<ast.Node> contextNode, tokenEmitFlags flags) {
    var state = new printerState();
    (state.commentState, pos) = p.emitCommentsBeforeToken(token, pos, contextNode, flags);
    state.sourceMapState = p.emitSourceMapsBeforeToken(token, pos, contextNode, flags);
    return (state, pos);
}

internal static void exitToken(this ref Printer p, ast.Kind token, nint pos, ж<ast.Node> contextNode, printerState previousState) {
    p.emitSourceMapsAfterToken(token, pos, contextNode, previousState.sourceMapState);
    p.emitCommentsAfterToken(token, pos, contextNode, previousState.commentState);
}

} partial class printer_package {

partial struct ListFormat /*num:nint*/;

public static ListFormat LFNone => 0;
public static ListFormat LFSingleLine => 0;
public static ListFormat LFMultiLine => /* 1 << 0 */ 1;
public static ListFormat LFPreserveLines => /* 1 << 1 */ 2;
public static ListFormat LFLinesMask => /* LFSingleLine | LFMultiLine | LFPreserveLines */ 3;
public static ListFormat LFNotDelimited => 0;
public static ListFormat LFBarDelimited => /* 1 << 2 */ 4;
public static ListFormat LFAmpersandDelimited => /* 1 << 3 */ 8;
public static ListFormat LFCommaDelimited => /* 1 << 4 */ 16;
public static ListFormat LFAsteriskDelimited => /* 1 << 5 */ 32;
public static ListFormat LFDelimitersMask => /* LFBarDelimited | LFAmpersandDelimited | LFCommaDelimited | LFAsteriskDelimited */ 60;
public static ListFormat LFAllowTrailingComma => /* 1 << 6 */ 64;
public static ListFormat LFIndented => /* 1 << 7 */ 128;
public static ListFormat LFSpaceBetweenBraces => /* 1 << 8 */ 256;
public static ListFormat LFSpaceBetweenSiblings => /* 1 << 9 */ 512;
public static ListFormat LFBraces => /* 1 << 10 */ 1024;
public static ListFormat LFParenthesis => /* 1 << 11 */ 2048;
public static ListFormat LFAngleBrackets => /* 1 << 12 */ 4096;
public static ListFormat LFSquareBrackets => /* 1 << 13 */ 8192;
public static ListFormat LFBracketsMask => /* LFBraces | LFParenthesis | LFAngleBrackets | LFSquareBrackets */ 15360;
public static ListFormat LFOptionalIfNil => /* 1 << 14 */ 16384;
public static ListFormat LFOptionalIfEmpty => /* 1 << 15 */ 32768;
public static ListFormat LFOptional => /* LFOptionalIfNil | LFOptionalIfEmpty */ 49152;
public static ListFormat LFPreferNewLine => /* 1 << 16 */ 65536;
public static ListFormat LFNoTrailingNewLine => /* 1 << 17 */ 131072;
public static ListFormat LFNoInterveningComments => /* 1 << 18 */ 262144;
public static ListFormat LFNoSpaceIfEmpty => /* 1 << 19 */ 524288;
public static ListFormat LFSingleElement => /* 1 << 20 */ 1048576;
public static ListFormat LFSpaceAfterList => /* 1 << 21 */ 2097152;
public static ListFormat LFModifiers => /* LFSingleLine | LFSpaceBetweenSiblings | LFNoInterveningComments | LFSpaceAfterList */ 2359808;
public static ListFormat LFHeritageClauses => /* LFSingleLine | LFSpaceBetweenSiblings */ 512;
public static ListFormat LFSingleLineTypeLiteralMembers => /* LFSingleLine | LFSpaceBetweenBraces | LFSpaceBetweenSiblings */ 768;
public static ListFormat LFMultiLineTypeLiteralMembers => /* LFMultiLine | LFIndented | LFOptionalIfEmpty */ 32897;
public static ListFormat LFSingleLineTupleTypeElements => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine */ 528;
public static ListFormat LFMultiLineTupleTypeElements => /* LFCommaDelimited | LFIndented | LFSpaceBetweenSiblings | LFMultiLine */ 657;
public static ListFormat LFUnionTypeConstituents => /* LFBarDelimited | LFSpaceBetweenSiblings | LFSingleLine */ 516;
public static ListFormat LFIntersectionTypeConstituents => /* LFAmpersandDelimited | LFSpaceBetweenSiblings | LFSingleLine */ 520;
public static ListFormat LFObjectBindingPatternElements => /* LFSingleLine | LFAllowTrailingComma | LFSpaceBetweenBraces | LFCommaDelimited | LFSpaceBetweenSiblings | LFNoSpaceIfEmpty */ 525136;
public static ListFormat LFArrayBindingPatternElements => /* LFSingleLine | LFAllowTrailingComma | LFCommaDelimited | LFSpaceBetweenSiblings | LFNoSpaceIfEmpty */ 524880;
public static ListFormat LFObjectLiteralExpressionProperties => /* LFPreserveLines | LFCommaDelimited | LFSpaceBetweenSiblings | LFSpaceBetweenBraces | LFIndented | LFBraces | LFNoSpaceIfEmpty */ 526226;
public static ListFormat LFImportAttributes => /* LFPreserveLines | LFCommaDelimited | LFSpaceBetweenSiblings | LFSpaceBetweenBraces | LFIndented | LFBraces | LFNoSpaceIfEmpty */ 526226;
public static ListFormat LFArrayLiteralExpressionElements => /* LFPreserveLines | LFCommaDelimited | LFSpaceBetweenSiblings | LFAllowTrailingComma | LFIndented | LFSquareBrackets */ 8914;
public static ListFormat LFCommaListElements => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine */ 528;
public static ListFormat LFCallExpressionArguments => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine | LFParenthesis */ 2576;
public static ListFormat LFNewExpressionArguments => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine | LFParenthesis | LFOptionalIfNil */ 18960;
public static ListFormat LFTemplateExpressionSpans => /* LFSingleLine | LFNoInterveningComments */ 262144;
public static ListFormat LFSingleLineBlockStatements => /* LFSpaceBetweenBraces | LFSpaceBetweenSiblings | LFSingleLine */ 768;
public static ListFormat LFMultiLineBlockStatements => /* LFIndented | LFMultiLine */ 129;
public static ListFormat LFVariableDeclarationList => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine */ 528;
public static ListFormat LFSingleLineFunctionBodyStatements => /* LFSingleLine | LFSpaceBetweenSiblings | LFSpaceBetweenBraces */ 768;
public static ListFormat LFMultiLineFunctionBodyStatements => /* LFMultiLine */ 1;
public static ListFormat LFClassHeritageClauses => /* LFSingleLine */ 0;
public static ListFormat LFClassMembers => /* LFIndented | LFMultiLine */ 129;
public static ListFormat LFInterfaceMembers => /* LFIndented | LFMultiLine */ 129;
public static ListFormat LFEnumMembers => /* LFCommaDelimited | LFIndented | LFMultiLine */ 145;
public static ListFormat LFCaseBlockClauses => /* LFIndented | LFMultiLine */ 129;
public static ListFormat LFNamedImportsOrExportsElements => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFAllowTrailingComma | LFSingleLine | LFSpaceBetweenBraces | LFNoSpaceIfEmpty */ 525136;
public static ListFormat LFJsxElementOrFragmentChildren => /* LFSingleLine | LFNoInterveningComments */ 262144;
public static ListFormat LFJsxElementAttributes => /* LFSingleLine | LFSpaceBetweenSiblings | LFNoInterveningComments */ 262656;
public static ListFormat LFCaseOrDefaultClauseStatements => /* LFIndented | LFMultiLine | LFNoTrailingNewLine | LFOptionalIfEmpty */ 163969;
public static ListFormat LFHeritageClauseTypes => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine */ 528;
public static ListFormat LFSourceFileStatements => /* LFMultiLine | LFNoTrailingNewLine */ 131073;
public static ListFormat LFDecorators => /* LFMultiLine | LFOptional | LFSpaceAfterList */ 2146305;
public static ListFormat LFTypeArguments => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine | LFAngleBrackets | LFOptional */ 53776;
public static ListFormat LFTypeParameters => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine | LFAngleBrackets | LFOptional */ 53776;
public static ListFormat LFParameters => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine | LFParenthesis */ 2576;
public static ListFormat LFSingleArrowParameter => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine */ 528;
public static ListFormat LFIndexSignatureParameters => /* LFCommaDelimited | LFSpaceBetweenSiblings | LFSingleLine | LFIndented | LFSquareBrackets */ 8848;
public static ListFormat LFJSDocComment => /* LFMultiLine | LFAsteriskDelimited */ 33;
public static ListFormat LFImportClauseEntries => /* LFImportAttributes */ 526226;

internal static @string getOpeningBracket(ListFormat format) {
    switch ((ListFormat)(format & LFBracketsMask)) {
    case var ᴛ when ᴛ == LFBraces: {
        return "{";
    }
    case var ᴛ when ᴛ == LFParenthesis: {
        return "(";
    }
    case var ᴛ when ᴛ == LFAngleBrackets: {
        return "<";
    }
    case var ᴛ when ᴛ == LFSquareBrackets: {
        return "[";
    }
    default: {
        throw panic(fmt.Sprintf("Unexpected bracket: %v"u8, (ListFormat)(format & LFBracketsMask)));
        break;
    }}

}

internal static @string getClosingBracket(ListFormat format) {
    switch ((ListFormat)(format & LFBracketsMask)) {
    case var ᴛ when ᴛ == LFBraces: {
        return "}";
    }
    case var ᴛ when ᴛ == LFParenthesis: {
        return ")";
    }
    case var ᴛ when ᴛ == LFAngleBrackets: {
        return ">";
    }
    case var ᴛ when ᴛ == LFSquareBrackets: {
        return "]";
    }
    default: {
        throw panic(fmt.Sprintf("Unexpected bracket: %v"u8, (ListFormat)(format & LFBracketsMask)));
        break;
    }}

}

} // end printer_package
