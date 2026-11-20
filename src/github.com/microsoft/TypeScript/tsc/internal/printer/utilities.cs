namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using utf8 = unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using sourcemap = go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using unicode;
using ꓸꓸꓸtryGetEnd_node = Span<printer_package.tryGetEnd_node>;
using static printer_package;

partial class printer_package {

partial struct getLiteralTextFlags /*num:nint*/;

internal static getLiteralTextFlags getLiteralTextFlagsNone => 0;
internal static getLiteralTextFlags getLiteralTextFlagsNeverAsciiEscape => /* 1 << 0 */ 1;
internal static getLiteralTextFlags getLiteralTextFlagsJsxAttributeEscape => /* 1 << 1 */ 2;
internal static getLiteralTextFlags getLiteralTextFlagsTerminateUnterminatedLiterals => /* 1 << 2 */ 4;
internal static getLiteralTextFlags getLiteralTextFlagsAllowNumericSeparator => /* 1 << 3 */ 8;

partial struct QuoteChar /*num:rune*/;

public static QuoteChar QuoteCharSingleQuote => /* '\'' */ 39;
public static QuoteChar QuoteCharDoubleQuote => /* '"' */ 34;
public static QuoteChar QuoteCharBacktick => /* '`' */ 96;

internal static map<rune, @string> jsxEscapedCharsMap = new map<rune, @string>{
    ['"'] = "&quot;",
    ['\''] = "&apos;"
};

[GoLazy] partial class lazyᴛescapedCharsMap { internal static map<rune, @string> escapedCharsMap = new map<rune, @string>{
    ['\t'] = @"\t",
    ['\v'] = @"\v",
    ['\f'] = @"\f",
    ['\b'] = @"\b",
    ['\r'] = @"\r",
    ['\n'] = @"\n",
    ['\\'] = @"\\",
    ['"'] = @"\""",
    ['\''] = @"\'",
    ['`'] = "\\`",
    ['$'] = @"\$",
    ['\u2028'] = @"\u2028",
    ['\u2029'] = @"\u2029",
    ['\u0085'] = @"\u0085"
}; }

internal static void encodeJsxCharacterEntity(ж<strings.Builder> b, rune charCode) {
    @string hexCharCode = strings.ToUpper(strconv.FormatUint((uint64)charCode, 16));
    b.WriteString("&#x");
    b.WriteString(hexCharCode);
    b.WriteByte((byte)';');
}

internal static void encodeUtf16EscapeSequence(ж<strings.Builder> b, rune charCode) {
    @string hexCharCode = strings.ToUpper(strconv.FormatUint((uint64)charCode, 16));
    b.WriteString(@"\u");
    for (nint i = len(hexCharCode); i < 4; i++) {
        b.WriteByte((byte)'0');
    }
    b.WriteString(hexCharCode);
}

internal static void escapeStringWorker(@string s, QuoteChar quoteChar, getLiteralTextFlags flags, ж<strings.Builder> b) {
    nint pos = 0;
    nint i = 0;
    while (i < len(s)) {
        var (ch, size) = stringutil.DecodeJSStringRune(s.slice(i));

        var escape = false;
        if (ch >= 0xD800 && ch <= 0xDFFF) {
            escape = true;
        } else if (ch == utf8.RuneError && size == 1) {
            escape = true;
        }

        switch (ch) {
        case '\\': {
            if ((getLiteralTextFlags)(flags & getLiteralTextFlagsJsxAttributeEscape) == 0) {
                escape = true;
            }
            break;
        }
        case '$': {
            if (quoteChar == QuoteCharBacktick && i + 1 < len(s) && s[i + 1] == '{') {
                escape = true;
            }
            break;
        }
        case var ᴛ when ᴛ == (rune)quoteChar || ᴛ == '\u2028' || ᴛ == '\u2029' || ᴛ == '\u0085' || ᴛ == '\r': {
            escape = true;
            break;
        }
        case '\n': {
            if (quoteChar != QuoteCharBacktick) {
                escape = true;
            }
            break;
        }
        default: {
            if (ch <= '\u001f' || (getLiteralTextFlags)(flags & getLiteralTextFlagsNeverAsciiEscape) == 0 && ch > '\u007f') {
                escape = true;
            }
            break;
        }}

        if (escape) {
            if (pos < i) {
                b.WriteString(s.slice(pos, i));
            }

            switch (ᐧ) {
            case {} when (getLiteralTextFlags)(flags & getLiteralTextFlagsJsxAttributeEscape) != 0: {
                if (ch == 0) {
                    b.WriteString("&#0;");
                } else if (jsxEscapedCharsMap[ch, ꟷ] is (var match, true)) {
                    b.WriteString(match);
                } else {
                    encodeJsxCharacterEntity(b, ch);
                }
                break;
            }
            default: {
                if (ch == '\r' && quoteChar == QuoteCharBacktick && i + 1 < len(s) && s[i + 1] == '\n') {
                    size++;
                    b.WriteString(@"\r\n");
                } else if (ch > 0xffff) {
                    ch -= 0x10000;
                    encodeUtf16EscapeSequence(b, ((ch & 0b11111111110000000000) >> 10) + 0xD800);
                    encodeUtf16EscapeSequence(b, (ch & 0b00000000001111111111) + 0xDC00);
                } else if (ch >= 0xD800 && ch <= 0xDFFF) {
                    encodeUtf16EscapeSequence(b, ch);
                } else if (ch == 0) {
                    if (i + 1 < len(s) && stringutil.IsDigit((rune)s[i + 1])) {
                        b.WriteString(@"\x00");
                    } else {
                        b.WriteString(@"\0");
                    }
                } else {
                    var (match, ok) = escapedCharsMap[ch, ꟷ]; if (ok) {
                        b.WriteString(match);
                    } else {
                        encodeUtf16EscapeSequence(b, ch);
                    }
                }
                break;
            }}

            pos = i + size;
        }

        i += size;
    }

    if (pos < i) {
        b.WriteString(s.slice(pos));
    }
}

public static @string EscapeString(@string s, QuoteChar quoteChar) {
    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    Ꮡb.Grow(len(s) + 2);
    escapeStringWorker(s, quoteChar, getLiteralTextFlagsNeverAsciiEscape, Ꮡb);
    return b.String();
}

internal static @string escapeNonAsciiString(@string s, QuoteChar quoteChar) {
    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    Ꮡb.Grow(len(s) + 2);
    escapeStringWorker(s, quoteChar, getLiteralTextFlagsNone, Ꮡb);
    return b.String();
}

internal static @string escapeJsxAttributeString(@string s, QuoteChar quoteChar) {
    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    Ꮡb.Grow(len(s) + 2);
    escapeStringWorker(s, quoteChar, (getLiteralTextFlags)(getLiteralTextFlagsJsxAttributeEscape | getLiteralTextFlagsNeverAsciiEscape), Ꮡb);
    return b.String();
}

internal static bool canUseOriginalText(ж<astꓸLiteralLikeNode> Ꮡnode, getLiteralTextFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.NodeIsSynthesized(Ꮡnode) || node.Parent == nil || (getLiteralTextFlags)(flags & getLiteralTextFlagsTerminateUnterminatedLiterals) != 0 && ast.IsUnterminatedLiteral(Ꮡnode)) {
        return false;
    }

    if (node.Kind == ast.KindNumericLiteral) {
        var tokenFlags = node.AsNumericLiteral().TokenFlags;
        if ((ast.TokenFlags)(tokenFlags & ast.TokenFlagsIsInvalid) != 0) {
            return false;
        }
        if ((ast.TokenFlags)(tokenFlags & ast.TokenFlagsContainsSeparator) != 0) {
            return (getLiteralTextFlags)(flags & getLiteralTextFlagsAllowNumericSeparator) != 0;
        }
    }

    return node.Kind != ast.KindBigIntLiteral;
}

internal static @string getLiteralText(ж<astꓸLiteralLikeNode> Ꮡnode, ж<ast.SourceFile> sourceFile, getLiteralTextFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (sourceFile != nil && canUseOriginalText(Ꮡnode, flags)) {
        return scanner.GetSourceTextOfNodeFromSourceFile(sourceFile, Ꮡnode, false);
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        ref var b = ref heap(new strings.Builder(), out var Ꮡb);
        QuoteChar quoteChar = default;
        if ((ast.TokenFlags)(node.AsStringLiteral().TokenFlags & ast.TokenFlagsSingleQuote) != 0) {
            quoteChar = QuoteCharSingleQuote;
        } else {
            quoteChar = QuoteCharDoubleQuote;
        }

        @string text = node.Text();

        Ꮡb.Grow(len(text) + 2);
        Ꮡb.WriteRune((rune)quoteChar);

        escapeStringWorker(text, quoteChar, flags, Ꮡb);

        Ꮡb.WriteRune((rune)quoteChar);
        return b.String();
    }
    case var ᴛ when ᴛ == ast.KindNoSubstitutionTemplateLiteral || ᴛ == ast.KindTemplateHead || ᴛ == ast.KindTemplateMiddle || ᴛ == ast.KindTemplateTail: {
        ref var b = ref heap(new strings.Builder(), out var Ꮡb);
        @string text = node.Text();
        @string rawText = node.TemplateLiteralLikeData().RawText;
        var raw = len(rawText) > 0 || len(text) == 0;

        nint textLen = default;
        if (raw) {
            textLen = len(rawText);
        } else {
            textLen = len(text);
        }

        switch (node.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindNoSubstitutionTemplateLiteral: {
            Ꮡb.Grow(2 + textLen);
            Ꮡb.WriteRune('`');
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindTemplateHead: {
            Ꮡb.Grow(3 + textLen);
            Ꮡb.WriteRune('`');
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindTemplateMiddle: {
            Ꮡb.Grow(3 + textLen);
            Ꮡb.WriteRune('}');
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindTemplateTail: {
            Ꮡb.Grow(2 + textLen);
            Ꮡb.WriteRune('}');
            break;
        }}

        switch (ᐧ) {
        case {} when len(rawText) > 0 || len(text) == 0: {
            Ꮡb.WriteString(rawText);
            break;
        }
        default: {
            escapeStringWorker(text, QuoteCharBacktick, flags, Ꮡb);
            break;
        }}

        switch (node.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindNoSubstitutionTemplateLiteral: {
            Ꮡb.WriteRune('`');
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindTemplateHead: {
            Ꮡb.WriteString("${");
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindTemplateMiddle: {
            Ꮡb.WriteString("${");
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindTemplateTail: {
            Ꮡb.WriteRune('`');
            break;
        }}

        return b.String();
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral || ᴛ == ast.KindBigIntLiteral: {
        return node.Text();
    }
    case var ᴛ when ᴛ == ast.KindRegularExpressionLiteral: {
        if ((getLiteralTextFlags)(flags & getLiteralTextFlagsTerminateUnterminatedLiterals) != 0 && ast.IsUnterminatedLiteral(Ꮡnode)) {
            ref var b = ref heap(new strings.Builder(), out var Ꮡb);
            @string text = node.Text();
            if (len(text) > 0 && text[len(text) - 1] == '\\') {
                Ꮡb.Grow(2 + len(text));
                Ꮡb.WriteString(text);
                Ꮡb.WriteString(" /");
            } else {
                Ꮡb.Grow(1 + len(text));
                Ꮡb.WriteString(text);
                Ꮡb.WriteString("/");
            }
            return b.String();
        }
        return node.Text();
    }
    default: {
        throw panic("Unsupported LiteralLikeNode");
        break;
    }}

}

internal static bool isNotPrologueDirective(ж<ast.Node> node) {
    return !ast.IsPrologueDirective(node);
}

public static bool RangeIsOnSingleLine(core.TextRange r, ж<ast.SourceFile> sourceFile) {
    return rangeStartIsOnSameLineAsRangeEnd(r, r, sourceFile);
}

public static bool RangeStartPositionsAreOnSameLine(core.TextRange range1, core.TextRange range2, ж<ast.SourceFile> sourceFile) {
    return PositionsAreOnSameLine(
        getStartPositionOfRange(range1, sourceFile, false),
        getStartPositionOfRange(range2, sourceFile, false),
        sourceFile);
}

internal static bool rangeEndPositionsAreOnSameLine(core.TextRange range1, core.TextRange range2, ж<ast.SourceFile> sourceFile) {
    return PositionsAreOnSameLine(range1.End(), range2.End(), sourceFile);
}

internal static bool rangeStartIsOnSameLineAsRangeEnd(core.TextRange range1, core.TextRange range2, ж<ast.SourceFile> sourceFile) {
    return PositionsAreOnSameLine(getStartPositionOfRange(range1, sourceFile, false), range2.End(), sourceFile);
}

internal static bool rangeEndIsOnSameLineAsRangeStart(core.TextRange range1, core.TextRange range2, ж<ast.SourceFile> sourceFile) {
    return PositionsAreOnSameLine(range1.End(), getStartPositionOfRange(range2, sourceFile, false), sourceFile);
}

internal static nint getStartPositionOfRange(core.TextRange r, ж<ast.SourceFile> ᏑsourceFile, bool includeComments) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (ast.PositionIsSynthesized(r.Pos())) {
        return -1;
    }
    return scanner.SkipTriviaEx(sourceFile.Text(), r.Pos(), Ꮡ(new scanner.SkipTriviaOptions(StopAtComments: includeComments)));
}

public static bool PositionsAreOnSameLine(nint pos1, nint pos2, ж<ast.SourceFile> sourceFile) {
    return GetLinesBetweenPositions(sourceFile, pos1, pos2) == 0;
}

public static nint GetLinesBetweenPositions(ж<ast.SourceFile> sourceFile, nint pos1, nint pos2) {
    if (pos1 == pos2) {
        return 0;
    }
    var lineStarts = scanner.GetECMALineStarts(new ast.SourceFileжSourceFileLike(sourceFile));
    nint lower = core.IfElse(pos1 < pos2, pos1, pos2);
    var isNegative = lower == pos2;
    nint upper = core.IfElse(isNegative, pos1, pos2);
    nint lowerLine = scanner.ComputeLineOfPosition(lineStarts, lower);
    nint upperLine = lowerLine + scanner.ComputeLineOfPosition(lineStarts.slice(lowerLine), upper);
    if (isNegative) {
        return lowerLine - upperLine;
    } else {
        return upperLine - lowerLine;
    }
}

internal static nint getLinesBetweenRangeEndAndRangeStart(core.TextRange range1, core.TextRange range2, ж<ast.SourceFile> sourceFile, bool includeSecondRangeComments) {
    nint range2Start = getStartPositionOfRange(range2, sourceFile, includeSecondRangeComments);
    return GetLinesBetweenPositions(sourceFile, range1.End(), range2Start);
}

internal static nint getLinesBetweenPositionAndPrecedingNonWhitespaceCharacter(nint pos, nint stopPos, ж<ast.SourceFile> ᏑsourceFile, bool includeComments) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    nint startPos = scanner.SkipTriviaEx(sourceFile.Text(), pos, Ꮡ(new scanner.SkipTriviaOptions(StopAtComments: includeComments)));
    nint prevPos = getPreviousNonWhitespacePosition(startPos, stopPos, ᏑsourceFile);
    return GetLinesBetweenPositions(ᏑsourceFile, core.IfElse(prevPos >= 0, prevPos, stopPos), startPos);
}

internal static nint getLinesBetweenPositionAndNextNonWhitespaceCharacter(nint pos, nint stopPos, ж<ast.SourceFile> ᏑsourceFile, bool includeComments) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    nint nextPos = scanner.SkipTriviaEx(sourceFile.Text(), pos, Ꮡ(new scanner.SkipTriviaOptions(StopAtComments: includeComments)));
    return GetLinesBetweenPositions(ᏑsourceFile, pos, core.IfElse(stopPos < nextPos, stopPos, nextPos));
}

internal static nint getPreviousNonWhitespacePosition(nint pos, nint stopPos, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    for (; pos >= stopPos; pos--) {
        if (!stringutil.IsWhiteSpaceLike((rune)sourceFile.Text()[pos])) {
            return pos;
        }
    }
    return -1;
}

internal static bool siblingNodePositionsAreComparable(ж<EmitContext> ᏑemitContext, ж<ast.Node> ᏑpreviousNode, ж<ast.Node> ᏑnextNode) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var previousNode = ref ᏑpreviousNode.DerefOrNull();
    ref var nextNode = ref ᏑnextNode.DerefOrNull();

    if (nextNode.Pos() < previousNode.End()) {
        return false;
    }

    ᏑpreviousNode = emitContext.MostOriginal(ᏑpreviousNode); previousNode = ref ᏑpreviousNode.DerefOrNull();
    ᏑnextNode = emitContext.MostOriginal(ᏑnextNode); nextNode = ref ᏑnextNode.DerefOrNull();
    var parent = previousNode.Parent;
    if (parent == nil || parent != nextNode.Parent) {
        return false;
    }

    var parentNodeArray = getContainingNodeArray(ᏑpreviousNode);
    if (parentNodeArray != nil) {
        nint prevNodeIndex = slices.Index(parentNodeArray.Nodes, ᏑpreviousNode);
        return prevNodeIndex >= 0 && slices.Index(parentNodeArray.Nodes, ᏑnextNode) == prevNodeIndex + 1;
    }

    return false;
}

internal static ж<ast.NodeList> getContainingNodeArray(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (parent == nil) {
        return default;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindTypeParameter: {
        switch (ᐧ) {
        case {} when ast.IsFunctionLike(parent) || ast.IsClassLike(parent) || ast.IsInterfaceDeclaration(parent) || ast.IsTypeOrJSTypeAliasDeclaration(parent): {
            return parent.TypeParameterList();
        }
        case {} when ast.IsInferTypeNode(parent): {
            break;
            break;
        }
        default: {
            throw panic(fmt.Sprintf("Unexpected TypeParameter parent: %#v"u8, parent.Kind));
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        return node.Parent.FunctionLikeData().Parameters;
    }
    case var ᴛ when ᴛ == ast.KindTemplateLiteralTypeSpan: {
        return node.Parent.AsTemplateLiteralTypeNode().TemplateSpans;
    }
    case var ᴛ when ᴛ == ast.KindTemplateSpan: {
        return node.Parent.AsTemplateExpression().TemplateSpans;
    }
    case var ᴛ when ᴛ == ast.KindDecorator: {
        if (canHaveDecorators(ref (node.Parent).DerefOrNull())) {
            var modifiers = node.Parent.Modifiers(); if (modifiers != nil) {
                return modifiers.ᏑNodeList;
            }
        }
        return default;
    }
    case var ᴛ when ᴛ == ast.KindHeritageClause: {
        if (ast.IsClassLike(node.Parent)) {
            return node.Parent.ClassLikeData().HeritageClauses;
        } else {
            return node.Parent.AsInterfaceDeclaration().HeritageClauses;
        }
        break;
    }}

    switch (parent.Kind) {
    case var ᴛ when ᴛ == ast.KindTypeLiteral || ᴛ == ast.KindInterfaceDeclaration: {
        if (ast.IsTypeElement(Ꮡnode)) {
            return parent.MemberList();
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindUnionType: {
        return parent.AsUnionTypeNode().Types;
    }
    case var ᴛ when ᴛ == ast.KindIntersectionType: {
        return parent.AsIntersectionTypeNode().Types;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression || ᴛ == ast.KindTupleType || ᴛ == ast.KindNamedImports || ᴛ == ast.KindNamedExports: {
        return parent.ElementList();
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindJsxAttributes: {
        return parent.PropertyList();
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        var p = parent.AsCallExpression();
        switch (ᐧ) {
        case {} when ast.IsTypeNode(Ꮡnode): {
            return (~p).TypeArguments;
        }
        case {} when Ꮡnode != (~p).Expression: {
            return (~p).Arguments;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindNewExpression: {
        var p = parent.AsNewExpression();
        switch (ᐧ) {
        case {} when ast.IsTypeNode(Ꮡnode): {
            return (~p).TypeArguments;
        }
        case {} when Ꮡnode != (~p).Expression: {
            return (~p).Arguments;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxElement || ᴛ == ast.KindJsxFragment: {
        if (ast.IsJsxChild(Ꮡnode)) {
            return parent.Children();
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxOpeningElement || ᴛ == ast.KindJsxSelfClosingElement: {
        if (ast.IsTypeNode(Ꮡnode)) {
            return parent.TypeArgumentList();
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindBlock || ᴛ == ast.KindModuleBlock || ᴛ == ast.KindCaseClause || ᴛ == ast.KindDefaultClause: {
        return parent.StatementList();
    }
    case var ᴛ when ᴛ == ast.KindCaseBlock: {
        return parent.AsCaseBlock().Clauses;
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression: {
        if (ast.IsClassElement(Ꮡnode)) {
            return parent.MemberList();
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
        if (ast.IsEnumMember(Ꮡnode)) {
            return parent.MemberList();
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        if (ast.IsStatement(Ꮡnode)) {
            return parent.StatementList();
        }
        break;
    }}

    if (ast.IsModifier(Ꮡnode)) {
        var modifiers = parent.Modifiers(); if (modifiers != nil) {
            return modifiers.ᏑNodeList;
        }
    }

    return default;
}

internal static bool canHaveDecorators(ref ast.Node node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindParameter || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindClassExpression || ᴛ == ast.KindClassDeclaration: {
        return true;
    }}

    return false;
}

internal static bool originalNodesHaveSameParent(ж<EmitContext> ᏑemitContext, ж<ast.Node> ᏑnodeA, ж<ast.Node> ᏑnodeB) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var nodeA = ref ᏑnodeA.DerefOrNull();
    ref var nodeB = ref ᏑnodeB.DerefOrNull();

    ᏑnodeA = emitContext.MostOriginal(ᏑnodeA); nodeA = ref ᏑnodeA.DerefOrNull();
    if (nodeA.Parent != nil) {
        ᏑnodeB = emitContext.MostOriginal(ᏑnodeB); nodeB = ref ᏑnodeB.DerefOrNull();
        return nodeA.Parent == nodeB.Parent;
    }
    return false;
}

internal partial interface tryGetEnd_node /*dyn*/ {
    nint End();
}

internal static (nint, bool) tryGetEnd(tryGetEnd_node node) {
    switch (node.type()) {
    case ж<ast.Node> v: {
        if (v != nil) {
            return (v.End(), true);
        }
        break;
    }
    case ж<ast.NodeList> v: {
        if (v != nil) {
            return (v.End(), true);
        }
        break;
    }
    case ж<ast.ModifierList> v: {
        if (v != nil) {
            return (v.of(ast.ModifierList.ᏑNodeList).End(), true);
        }
        break;
    }
    case ж<core.TextRange> v: {
        if (v != nil) {
            return (v.End(), true);
        }
        break;
    }
    case core.TextRange v: {
        return (v.End(), true);
    }
    default: {
        var v = node;
        throw panic(fmt.Sprintf("unhandled type: %T"u8, node));
        break;
    }}
    return (0, false);
}

internal static nint greatestEnd(nint end, params ꓸꓸꓸtryGetEnd_node nodesʗp) {
    var nodes = nodesʗp.sslice();

    for (nint i = len(nodes) - 1; i >= 0; i--) {
        var node = nodes[i];
        var (nodeEnd, ok) = tryGetEnd(node); if (ok && end < nodeEnd) {
            end = nodeEnd;
        }
    }
    return end;
}

internal static ж<ast.Node> skipSynthesizedParentheses(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (node.Kind == ast.KindParenthesizedExpression && ast.NodeIsSynthesized(Ꮡnode)) {
        Ꮡnode = node.Expression(); node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

internal static bool isNewExpressionWithoutArguments(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == ast.KindNewExpression && node.ArgumentList() == nil;
}

internal static bool isBinaryOperation(ж<ast.Node> Ꮡnode, ast.Kind token) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = ast.SkipPartiallyEmittedExpressions(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    return node.Kind == ast.KindBinaryExpression && node.AsBinaryExpression().OperatorToken.Kind == token;
}

internal static bool mixingBinaryOperatorsRequiresParentheses(ast.Kind a, ast.Kind b) {
    if (a == ast.KindQuestionQuestionToken) {
        return b == ast.KindAmpersandAmpersandToken || b == ast.KindBarBarToken;
    }
    if (b == ast.KindQuestionQuestionToken) {
        return a == ast.KindAmpersandAmpersandToken || a == ast.KindBarBarToken;
    }
    return false;
}

internal static bool isImmediatelyInvokedFunctionExpressionOrArrowFunction(ж<astꓸExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡnode = ast.SkipPartiallyEmittedExpressions(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
    if (!ast.IsCallExpression(Ꮡnode)) {
        return false;
    }
    Ꮡnode = ast.SkipPartiallyEmittedExpressions(node.Expression()); node = ref Ꮡnode.DerefOrNull();
    return ast.IsFunctionExpression(Ꮡnode) || ast.IsArrowFunction(Ꮡnode);
}

internal static bool hasLeadingHash(@string text) {
    return len(text) > 0 && text[0] == '#';
}

internal static @string removeLeadingHash(@string text) {
    if (hasLeadingHash(text)) {
        return text[1..];
    } else {
        return text;
    }
}

internal static @string ensureLeadingHash(@string text) {
    if (hasLeadingHash(text)) {
        return text;
    } else {
        return "#"u8 + text;
    }
}

public static @string FormatGeneratedName(bool privateName, @string prefix, @string @base, @string suffix) {
    @string name = removeLeadingHash(prefix) + removeLeadingHash(@base) + removeLeadingHash(suffix);
    if (privateName) {
        return ensureLeadingHash(name);
    }
    return name;
}

internal static bool isASCIIWordCharacter(rune ch) {
    return stringutil.IsASCIILetter(ch) || stringutil.IsDigit(ch) || ch == '_';
}

internal static @string makeIdentifierFromModuleName(@string moduleName) {
    moduleName = tspath.GetBaseFileName(moduleName);
    ref var builder = ref heap(new strings.Builder(), out var Ꮡbuilder);
    nint start = 0;
    nint pos = 0;
    while (pos < len(moduleName)) {
        var ch = (rune)moduleName[pos];
        if (pos == 0 && stringutil.IsDigit(ch)) {
            Ꮡbuilder.WriteByte((byte)'_');
        } else if (!isASCIIWordCharacter(ch)) {
            if (start < pos) {
                Ꮡbuilder.WriteString(moduleName.slice(start, pos));
            }
            Ꮡbuilder.WriteByte((byte)'_');
            start = pos + 1;
        }
        pos++;
    }
    if (start < pos) {
        Ꮡbuilder.WriteString(moduleName.slice(start, pos));
    }
    return builder.String();
}

internal static nint findSpanEndWithEmitContext<T>(ж<EmitContext> c, slice<T> Δarray, Func<ж<EmitContext>, T, bool> test, nint start) {
    nint i = start;
    while (i < len(Δarray) && test(c, Δarray[i])) {
        i++;
    }
    return i;
}

internal static nint findSpanEnd<T>(slice<T> Δarray, Func<T, bool> test, nint start) {
    nint i = start;
    while (i < len(Δarray) && test(Δarray[i])) {
        i++;
    }
    return i;
}

internal static void skipWhiteSpaceSingleLine(@string text, ref nint pos) {
    while (pos < len(text)) {
        var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
        if (!stringutil.IsWhiteSpaceSingleLine(ch)) {
            break;
        }
        pos += size;
    }
}

internal static bool matchWhiteSpaceSingleLine(@string text, ref nint pos) {
    nint startPos = pos;
    skipWhiteSpaceSingleLine(text, ref pos);
    return pos != startPos;
}

internal static bool matchRune(@string text, ref nint pos, rune expected) {
    var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
    if (ch == expected) {
        pos += size;
        return true;
    }
    return false;
}

internal static bool matchString(@string text, ref nint pos, @string expected) {
    nint textPos = pos;
    nint expectedPos = 0;
    while (expectedPos < len(expected)) {
        if (textPos >= len(text)) {
            return false;
        }

        var (expectedRune, expectedSize) = utf8.DecodeRuneInString(expected.slice(expectedPos));
        if (!matchRune(text, ref textPos, expectedRune)) {
            return false;
        }

        expectedPos += expectedSize;
    }

    pos = textPos;
    return true;
}

internal static bool matchQuotedString(@string text, ref nint pos) {
    nint textPos = pos;
    rune quoteChar = default;
    switch (ᐧ) {
    case {} when matchRune(text, ref textPos, '\''): {
        quoteChar = '\'';
        break;
    }
    case {} when matchRune(text, ref textPos, '"'): {
        quoteChar = '"';
        break;
    }
    default: {
        return false;
    }}

    while (textPos < len(text)) {
        var (ch, size) = utf8.DecodeRuneInString(text.slice(textPos));
        textPos += size;
        if (ch == quoteChar) {
            pos = textPos;
            return true;
        }
    }
    return false;
}

public static bool IsRecognizedTripleSlashComment(@string text, ast.CommentRange commentRange) {
    if (commentRange.Kind == ast.KindSingleLineCommentTrivia && commentRange.TextRange.Len() > 2 && text[commentRange.TextRange.Pos() + 1] == '/' && text[commentRange.TextRange.Pos() + 2] == '/') {
        text = text.slice(commentRange.TextRange.Pos() + 3, commentRange.TextRange.End());
        nint pos = 0;
        skipWhiteSpaceSingleLine(text, ref pos);
        if (!matchRune(text, ref pos, '<')) {
            return false;
        }
        switch (ᐧ) {
        case {} when matchString(text, ref pos, "reference"): {
            if (!matchWhiteSpaceSingleLine(text, ref pos)) {
                return false;
            }
            if (!matchString(text, ref pos, "path") && !matchString(text, ref pos, "types") && !matchString(text, ref pos, "lib") && !matchString(text, ref pos, "no-default-lib")) {
                return false;
            }
            skipWhiteSpaceSingleLine(text, ref pos);
            if (!matchRune(text, ref pos, '=')) {
                return false;
            }
            skipWhiteSpaceSingleLine(text, ref pos);
            if (!matchQuotedString(text, ref pos)) {
                return false;
            }
            break;
        }
        case {} when matchString(text, ref pos, "amd-dependency"): {
            if (!matchWhiteSpaceSingleLine(text, ref pos)) {
                return false;
            }
            if (!matchString(text, ref pos, "path")) {
                return false;
            }
            skipWhiteSpaceSingleLine(text, ref pos);
            if (!matchRune(text, ref pos, '=')) {
                return false;
            }
            skipWhiteSpaceSingleLine(text, ref pos);
            if (!matchQuotedString(text, ref pos)) {
                return false;
            }
            break;
        }
        case {} when matchString(text, ref pos, "amd-module"): {
            skipWhiteSpaceSingleLine(text, ref pos);
            break;
        }
        default: {
            return false;
        }}

        nint index = strings.Index(text.slice(pos), "/>");
        return index != -1;
    }

    return false;
}

internal static bool isJSDocLikeText(@string text, ast.CommentRange comment) {
    return comment.Kind == ast.KindMultiLineCommentTrivia && comment.TextRange.Len() >= 5 && text[comment.TextRange.Pos() + 2] == '*' && text[comment.TextRange.Pos() + 3] != '/';
}

public static bool IsPinnedComment(@string text, ast.CommentRange comment) {
    return comment.Kind == ast.KindMultiLineCommentTrivia && comment.TextRange.Len() > 5 && text[comment.TextRange.Pos() + 2] == '!';
}

internal static nint calculateIndent(@string text, nint pos, nint end) {
    nint currentLineIndent = 0;
    nint indentSize = GetDefaultIndentSize();
    while (pos < end) {
        var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
        if (!stringutil.IsWhiteSpaceSingleLine(ch)) {
            break;
        }
        if (ch == '\t') {
            currentLineIndent += indentSize - (rem(currentLineIndent, indentSize));
        } else {
            currentLineIndent++;
        }
        pos += size;
    }

    return currentLineIndent;
}

partial struct lineCharacterCache {
    internal slice<core.TextPos> lineMap;
    internal @string text;
    internal nint cachedLine;
    internal nint cachedPos;
    internal core.UTF16Offset cachedChar;
    internal bool hasCached;
}

internal static ж<lineCharacterCache> newLineCharacterCache(sourcemap.Source source) {
    return Ꮡ(new lineCharacterCache(
        lineMap: source.ECMALineMap(),
        text: source.Text()
    ));
}

} partial class printerᴛlineCharacterCache {

internal static (nint line, core.UTF16Offset character) getLineAndCharacter(this ref lineCharacterCache c, nint pos) {
    nint line = default;
    core.UTF16Offset character = default;

    line = scanner.ComputeLineOfPosition(c.lineMap, pos);
    nint lineStart = (nint)c.lineMap[line];
    nint endPos = min(pos, len(c.text));
    if (c.hasCached && line == c.cachedLine && endPos >= c.cachedPos) {
        character = c.cachedChar + core.UTF16Len(c.text.slice(c.cachedPos, endPos));
    } else {
        character = core.UTF16Len(c.text.slice(lineStart, endPos));
    }
    core.UTF16Offset cachedChar = character;
    character += ((core.UTF16Offset)(pos - endPos));
    c.cachedLine = line;
    c.cachedPos = endPos;
    c.cachedChar = cachedChar;
    c.hasCached = true;
    return (line, character);
}

} // end printerᴛlineCharacterCache
