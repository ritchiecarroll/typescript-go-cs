namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using unicode = unicode_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using static parser_package;

partial class parser_package {

[GoInit] internal static void init() {
    ast.SetParseJSDocForNode(parseJSDocForNode);
}

internal static slice<ж<ast.Node>> parseJSDocForNode(ж<ast.SourceFile> ᏑsourceFile, ж<ast.Node> Ꮡnode) {
    GoFrame ᒐ = default;
    try {
        ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var p = getParser();
        defer(putParser, p, ref ᒐ);
        p.initializeState(sourceFile.ParseOptions(), sourceFile.Text(), sourceFile.ScriptKind);
        var ranges = GetJSDocCommentRanges(p.Ꮡfactory, default, Ꮡnode, sourceFile.Text());
        if (len(ranges) == 0) {
            return default;
        }
        var jsdoc = new slice<ж<ast.Node>>(0, len(ranges));
        nint pos = node.Pos();
        foreach (var (_, comment) in range(ranges)) {
            var parsed = p.parseJSDocComment(Ꮡnode, comment.TextRange.Pos(), comment.TextRange.End(), pos); if (parsed != nil) {
                parsed.Parent = Ꮡnode;
                jsdoc = append(jsdoc, parsed);
                pos = parsed.End();
            }
        }
        return jsdoc;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

partial struct jsdocState /*num:int32*/;

internal static jsdocState jsdocStateBeginningOfLine => /* iota */ 0;
internal static jsdocState jsdocStateSawAsterisk => 1;
internal static jsdocState jsdocStateSavingComments => 2;
internal static jsdocState jsdocStateSavingBackticks => 3;

partial struct propertyLikeParse /*num:int32*/;

internal static propertyLikeParse propertyLikeParseProperty => /* 1 << iota */ 1;
internal static propertyLikeParse propertyLikeParseParameter => 2;
internal static propertyLikeParse propertyLikeParseCallbackParameter => 4;

} partial class parserᴛParser {

internal static slice<ж<ast.Node>> withJSDoc(this ж<Parser> Ꮡp, ж<ast.Node> Ꮡnode, jsdocScannerInfo info) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((jsdocScannerInfo)(info & jsdocScannerInfoHasJSDoc) == 0) {
        return default;
    }

    if (!p.isJavaScript()) {
        node.Flags |= (ast.NodeFlags)(ast.NodeFlagsHasJSDoc);
        if ((jsdocScannerInfo)(info & jsdocScannerInfoHasDeprecated) != 0) {
            node.Flags |= (ast.NodeFlags)(ast.NodeFlagsPossiblyContainsDeprecatedTag);
        }
        if ((jsdocScannerInfo)(info & jsdocScannerInfoHasSeeOrLink) == 0) {
            return default;
        }
    }

    var ranges = GetJSDocCommentRanges(Ꮡp.Ꮡfactory, p.jsdocCommentRangesSpace, Ꮡnode, p.sourceText);
    p.jsdocCommentRangesSpace = ranges[..0];

    p.hasDeprecatedTag = false;
    var jsdoc = p.nodeSliceArena.NewSlice(len(ranges))[..0];
    nint pos = node.Pos();
    foreach (var (_, comment) in range(ranges)) {
        var parsed = Ꮡp.parseJSDocComment(Ꮡnode, comment.TextRange.Pos(), comment.TextRange.End(), pos); if (parsed != nil) {
            parsed.Parent = Ꮡnode;
            jsdoc = append(jsdoc, parsed);
            pos = parsed.End();
        }
    }
    if (len(jsdoc) != 0) {
        if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsHasJSDoc) == 0) {
            node.Flags |= (ast.NodeFlags)(ast.NodeFlagsHasJSDoc);
        }
        if (p.hasDeprecatedTag) {
            p.hasDeprecatedTag = false;
            node.Flags |= (ast.NodeFlags)(ast.NodeFlagsPossiblyContainsDeprecatedTag);
        }
        if (p.isJavaScript()) {
            Ꮡp.reparseTags(Ꮡnode, jsdoc);
        }
        p.jsdocInfos = append(p.jsdocInfos, new JSDocInfo(parent: Ꮡnode, jsDocs: jsdoc));
        return jsdoc;
    }
    return default;
}

internal static ж<ast.Node> parseJSDocTypeExpression(this ж<Parser> Ꮡp, bool mayOmitBraces) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    bool hasBrace = default;
    if (mayOmitBraces) {
        hasBrace = p.parseOptional(ast.KindOpenBraceToken);
    } else {
        hasBrace = p.parseExpected(ast.KindOpenBraceToken);
    }
    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsJSDoc, true);
    var t = Ꮡp.parseJSDocType();
    p.contextFlags = saveContextFlags;
    if (hasBrace) {
        p.parseExpectedJSDoc(ast.KindCloseBraceToken);
    }

    return p.finishNode(p.factory.NewJSDocTypeExpression(t), pos);
}

internal static ж<ast.Node> parseJSDocNameReference(this ref Parser p) {
    nint pos = p.nodePos();
    var hasBrace = p.parseOptional(ast.KindOpenBraceToken);
    var entityName = p.parseJSDocLinkName();
    if (hasBrace) {
        p.parseExpectedJSDoc(ast.KindCloseBraceToken);
    }
    p.scanner.ResetPos(p.scanner.TokenFullStart());
    p.nextTokenJSDoc();
    return p.finishNode(p.factory.NewJSDocNameReference(entityName), pos);
}

internal static ж<ast.Node> parseJSDocComment(this ж<Parser> Ꮡp, ж<ast.Node> parent, nint start, nint end, nint fullStart) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (end == -1) {
        end = len(p.sourceText);
    }
    if (!isJSDocLikeText(p.sourceText.slice(start))) {
        return default;
    }

    @string saveSourceText = p.sourceText;
    var saveToken = p.token;
    var saveContextFlags = p.contextFlags;
    ParsingContexts saveParsingContexts = p.parsingContexts;
    var saveScannerState = p.scanner.Mark();
    nint saveDiagnosticsLength = len(p.diagnostics);
    var saveHasParseError = p.hasParseError;
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;

    nint initialIndent = start + 4 - (strings.LastIndex(p.sourceText.slice(0, start), "\n") + 1);
    p.sourceText = p.sourceText.slice(0, end - 2);
    p.scanner.SetText(p.sourceText);
    p.scanner.ResetPos(start + 3);
    p.setContextFlags(ast.NodeFlagsJSDoc, true);
    p.parsingContexts |= (ParsingContexts)((ParsingContexts)(nint)(1 << (int)(PCJSDocComment)));

    var comment = Ꮡp.parseJSDocCommentWorker(start, end, fullStart, initialIndent);
    if ((ast.NodeFlags)(p.contextFlags & ast.NodeFlagsJavaScriptFile) != 0) {
        p.jsdocDiagnostics = appendꓸꓸꓸ(p.jsdocDiagnostics, p.diagnostics.slice(saveDiagnosticsLength));
    }
    p.diagnostics = p.diagnostics.slice(0, saveDiagnosticsLength);

    p.sourceText = saveSourceText;
    p.scanner.SetText(p.sourceText);
    p.parsingContexts = saveParsingContexts;
    p.contextFlags = saveContextFlags;
    p.scanner.Rewind(saveScannerState);
    p.token = saveToken;
    p.hasParseError = saveHasParseError;
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;

    return comment;
}

internal static ж<ast.Node> parseJSDocCommentWorker(this ж<Parser> Ꮡp, nint start, nint end, nint fullStart, nint indent) {
    ref var p = ref Ꮡp.DerefOrNull();

    var tags = p.nodeSliceArena.NewSlice(1)[..0];
    nint tagsPos = -1;
    nint tagsEnd = -1;
    var state = jsdocStateSawAsterisk;
    nint backtickCount = 0;
    var inFencedCodeBlock = false;
    var commentParts = p.nodeSliceArena.NewSlice(1)[..0];
    ref var comments = ref heap<slice<@string>>(p.jsdocCommentsSpace, out var Ꮡcomments);
    nint commentsPos = -1;
    nint linkEnd = start;
    nint margin = -1;
    void pushComment(@string text) {
        if (margin == -1) {
            margin = indent;
        }
        Ꮡcomments.ValueSlot = append(Ꮡcomments.ValueSlot, text);
        indent += len(text);
    }

    p.nextTokenJSDoc();
    while (p.parseOptionalJsdoc(ast.KindWhitespaceTrivia)) {
    }
    if (p.parseOptionalJsdoc(ast.KindNewLineTrivia)) {
        state = jsdocStateBeginningOfLine;
        indent = 0;
    }
loop:
    for (;;) {
        if (p.token != ast.KindBacktickToken && backtickCount > 0) {
            if (backtickCount >= 3) {
                inFencedCodeBlock = !inFencedCodeBlock;
            }
            backtickCount = 0;
        }
        var exprᴛ1 = p.token;
        var matchᴛ1 = false;
        if (exprᴛ1 == ast.KindAtToken) { matchᴛ1 = true;
            do {
                if (inFencedCodeBlock || !p.scanner.CanFollowJSDocAt()) {
                    if (inFencedCodeBlock) {
                        state = jsdocStateSavingBackticks;
                    } else {
                        state = jsdocStateSavingComments;
                    }
                    pushComment(p.scanner.TokenText());
                    break;
                }
                comments = removeTrailingWhitespace(comments);
                if (commentsPos == -1) {
                    commentsPos = p.nodePos();
                }
                var tag = Ꮡp.parseTag(tags, indent);
                if (tagsPos == -1) {
                    tagsPos = tag.Pos();
                }
                tags = append(tags, tag);
                tagsEnd = tag.End();
                state = jsdocStateBeginningOfLine;
                margin = -1;
            } while (false);
        }
        else if (exprᴛ1 == ast.KindNewLineTrivia) { matchᴛ1 = true;
            comments = append(comments, p.scanner.TokenText());
            state = jsdocStateBeginningOfLine;
            indent = 0;
        }
        else if (exprᴛ1 == ast.KindAsteriskToken) { matchᴛ1 = true;
            @string asterisk = p.scanner.TokenText();
            if (state == jsdocStateSawAsterisk) {
                state = jsdocStateSavingComments;
                pushComment(asterisk);
            } else {
                if (state != jsdocStateBeginningOfLine) {
                    throw panic("state must be BeginningOfLine");
                }
                state = jsdocStateSawAsterisk;
                indent += len(asterisk);
            }
        }
        else if (exprᴛ1 == ast.KindWhitespaceTrivia) { matchᴛ1 = true;
            if (state == jsdocStateSavingComments || state == jsdocStateSavingBackticks) {
                throw panic("whitespace shouldn't come from the scanner while saving top-level comment text");
            }
            @string whitespace = p.scanner.TokenText();
            if (margin > -1 && indent + len(whitespace) > margin) {
                nint existingIndent = margin - indent;
                if (existingIndent < 0) {
                    existingIndent += len(whitespace);
                }
                if (existingIndent < 0) {
                    existingIndent = 0;
                }
                comments = append(comments, whitespace.slice(existingIndent));
            }
            indent += len(whitespace);
        }
        else if (exprᴛ1 == ast.KindEndOfFile) { matchᴛ1 = true;
            goto break_loop;
        }
        else if (exprᴛ1 == ast.KindJSDocCommentTextToken) { matchᴛ1 = true;
            if (state != jsdocStateSavingBackticks) {
                if (inFencedCodeBlock) {
                    state = jsdocStateSavingBackticks;
                } else {
                    state = jsdocStateSavingComments;
                }
            }
            pushComment(p.scanner.TokenValue());
        }
        else if (exprᴛ1 == ast.KindBacktickToken) { matchᴛ1 = true;
            backtickCount++;
            if (state == jsdocStateSavingBackticks) {
                state = jsdocStateSavingComments;
            } else {
                state = jsdocStateSavingBackticks;
            }
            pushComment(p.scanner.TokenText());
        }
        else if (exprᴛ1 == ast.KindOpenBraceToken) { matchᴛ1 = true;
            do {
                if (inFencedCodeBlock) {
                    state = jsdocStateSavingBackticks;
                    pushComment(p.scanner.TokenText());
                    break;
                }
                state = jsdocStateSavingComments;
                nint commentEnd = p.scanner.TokenFullStart();
                nint linkStart = p.scanner.TokenEnd() - 1;
                var link = Ꮡp.parseJSDocLink(linkStart);
                if (link != nil) {
                    if (linkEnd == start) {
                        comments = removeLeadingNewlines(comments);
                    }
                    var jsdocText = p.finishNodeWithEnd(p.factory.NewJSDocText(p.stringSliceArena.Clone(comments)), linkEnd, commentEnd);
                    commentParts = append(commentParts, jsdocText, link);
                    comments = comments[..0];
                    linkEnd = p.scanner.TokenEnd();
                    break;
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1) { /* default: */
            if (state != jsdocStateSavingBackticks) {
                if (inFencedCodeBlock) {
                    state = jsdocStateSavingBackticks;
                } else {
                    state = jsdocStateSavingComments;
                }
            }
            pushComment(p.scanner.TokenText());
        }

        if (state == jsdocStateSavingComments || state == jsdocStateSavingBackticks) {
            p.nextJSDocCommentTextToken(state == jsdocStateSavingBackticks);
        } else {
            p.nextTokenJSDoc();
        }
continue_loop:;
    }
break_loop:;

    p.jsdocCommentsSpace = comments[..0];
    if (commentsPos == -1) {
        commentsPos = p.scanner.TokenFullStart();
    }

    if (len(comments) > 0) {
        comments[len(comments) - 1] = strings.TrimRightFunc(comments[len(comments) - 1], unicode.IsSpace);
        var jsdocText = p.finishNodeWithEnd(p.factory.NewJSDocText(p.stringSliceArena.Clone(comments)), linkEnd, commentsPos);
        commentParts = append(commentParts, jsdocText);
    }

    if (len(commentParts) > 0 && len(tags) > 0 && commentsPos == -1) {
        throw panic("having parsed tags implies that the end of the comment span should be set");
    }

    ж<ast.NodeList> tagsNodeList = default;
    if (tagsPos != -1) {
        tagsNodeList = p.newNodeList(core.NewTextRange(tagsPos, tagsEnd), tags);
    }

    var jsdocComment = p.factory.NewJSDoc(
        p.newNodeList(core.NewTextRange(start, commentsPos), commentParts),
        tagsNodeList);
    return p.finishNodeWithEnd(jsdocComment, fullStart, end);
}

} partial class parser_package {

internal static slice<@string> removeLeadingNewlines(slice<@string> comments) {
    nint i = 0;
    while (i < len(comments) && strings.TrimLeft(comments[i], "\r\n") == ""u8) {
        i++;
    }
    return comments.slice(i);
}

internal static @string trimEnd(@string s) {
    return strings.TrimRightFunc(s, stringutil.IsWhiteSpaceLike);
}

internal static slice<@string> removeTrailingWhitespace(slice<@string> comments) {
    nint end = len(comments);
    for (nint i = len(comments) - 1; i >= 0; i--) {
        @string trimmed = trimEnd(comments[i]);
        if (trimmed == ""u8) {
            end = i;
        } else {
            comments[i] = trimmed;
            break;
        }
    }
    return comments.slice(0, end);
}

} partial class parserᴛParser {

internal static bool isNextNonwhitespaceTokenEndOfFile(this ref Parser p) {
    for (;;) {
        p.nextTokenJSDoc();
        if (p.token == ast.KindEndOfFile) {
            return true;
        }
        if (!(p.token == ast.KindWhitespaceTrivia || p.token == ast.KindNewLineTrivia)) {
            return false;
        }
    }
}

internal static void skipWhitespace(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindWhitespaceTrivia || p.token == ast.KindNewLineTrivia) {
        if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.isNextNonwhitespaceTokenEndOfFile)))) {
            return;
        }
    }
    while (p.token == ast.KindWhitespaceTrivia || p.token == ast.KindNewLineTrivia) {
        p.nextTokenJSDoc();
    }
}

internal static @string skipWhitespaceOrAsterisk(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();
    var indentsᴷ = new stackAppend<@string>();

    if (p.token == ast.KindWhitespaceTrivia || p.token == ast.KindNewLineTrivia) {
        if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.isNextNonwhitespaceTokenEndOfFile)))) {
            return "";
        }
    }

    var precedingLineBreak = p.scanner.HasPrecedingLineBreak();
    var seenLineBreak = false;
    var indents = new slice<@string>(0, 4);
    while ((precedingLineBreak && p.token == ast.KindAsteriskToken) || p.token == ast.KindWhitespaceTrivia || p.token == ast.KindNewLineTrivia) {
        indents = append(indentsᴷ, indents, p.scanner.TokenText());
        if (p.token == ast.KindNewLineTrivia) {
            precedingLineBreak = true;
            seenLineBreak = true;
            indents = indents[..0];
        } else if (p.token == ast.KindAsteriskToken) {
            precedingLineBreak = false;
        }
        p.nextTokenJSDoc();
    }
    if (seenLineBreak) {
        return strings.Join(indents, "");
    } else {
        return "";
    }
}

internal static ж<ast.Node> parseTag(this ж<Parser> Ꮡp, slice<ж<ast.Node>> tags, nint margin) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token != ast.KindAtToken) {
        throw panic("should be called only at the start of a tag");
    }
    nint start = p.scanner.TokenStart();
    p.nextTokenJSDoc();

    var tagName = p.parseJSDocIdentifierName(diagnostics.Identifier_expected);
    @string indentText = Ꮡp.skipWhitespaceOrAsterisk();

    ж<ast.Node> tag = default;
    var exprᴛ1 = tagName.Text();
    switch (ᐧ) {
    case {} when exprᴛ1 == "implements"u8: {
        tag = Ꮡp.parseImplementsTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "augments"u8 || exprᴛ1 == "extends"u8: {
        tag = Ꮡp.parseAugmentsTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "public"u8: {
        tag = Ꮡp.parseSimpleTag(start, (ж<astꓸIdentifierNode> tagNameΔ7, ж<ast.NodeList> comments) => Ꮡp.Value.factory.NewJSDocPublicTag(tagNameΔ7, comments), tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "private"u8: {
        tag = Ꮡp.parseSimpleTag(start, (ж<astꓸIdentifierNode> tagNameΔ8, ж<ast.NodeList> comments) => Ꮡp.Value.factory.NewJSDocPrivateTag(tagNameΔ8, comments), tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "protected"u8: {
        tag = Ꮡp.parseSimpleTag(start, (ж<astꓸIdentifierNode> tagNameΔ9, ж<ast.NodeList> comments) => Ꮡp.Value.factory.NewJSDocProtectedTag(tagNameΔ9, comments), tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "readonly"u8: {
        tag = Ꮡp.parseSimpleTag(start, (ж<astꓸIdentifierNode> tagNameΔ10, ж<ast.NodeList> comments) => Ꮡp.Value.factory.NewJSDocReadonlyTag(tagNameΔ10, comments), tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "override"u8: {
        tag = Ꮡp.parseSimpleTag(start, (ж<astꓸIdentifierNode> tagNameΔ11, ж<ast.NodeList> comments) => Ꮡp.Value.factory.NewJSDocOverrideTag(tagNameΔ11, comments), tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "deprecated"u8: {
        p.hasDeprecatedTag = true;
        tag = Ꮡp.parseSimpleTag(start, (ж<astꓸIdentifierNode> tagNameΔ12, ж<ast.NodeList> comments) => Ꮡp.Value.factory.NewJSDocDeprecatedTag(tagNameΔ12, comments), tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "this"u8: {
        tag = Ꮡp.parseThisTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "arg"u8 || exprᴛ1 == "argument"u8 || exprᴛ1 == "param"u8: {
        tag = Ꮡp.parseParameterOrPropertyTag(start, tagName, propertyLikeParseParameter, margin);
        break;
    }
    case {} when exprᴛ1 == "return"u8 || exprᴛ1 == "returns"u8: {
        tag = Ꮡp.parseReturnTag(tags, start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "template"u8: {
        tag = Ꮡp.parseTemplateTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "type"u8: {
        tag = Ꮡp.parseTypeTag(tags, start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "typedef"u8: {
        tag = Ꮡp.parseTypedefTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "callback"u8: {
        tag = Ꮡp.parseCallbackTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "overload"u8: {
        tag = Ꮡp.parseOverloadTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "satisfies"u8: {
        tag = Ꮡp.parseSatisfiesTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "see"u8: {
        tag = Ꮡp.parseSeeTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "exception"u8 || exprᴛ1 == "throws"u8: {
        tag = Ꮡp.parseThrowsTag(start, tagName, margin, indentText);
        break;
    }
    case {} when exprᴛ1 == "import"u8: {
        tag = Ꮡp.parseImportTag(start, tagName, margin, indentText);
        break;
    }
    default: {
        tag = Ꮡp.parseUnknownTag(start, tagName, margin, indentText);
        break;
    }}

    if (tag == nil) {
        throw panic("tag should not be nil");
    }
    return tag;
}

internal static ж<ast.NodeList> parseTrailingTagComments(this ж<Parser> Ꮡp, nint pos, nint end, nint margin, @string indentText) {
    if (len(indentText) == 0) {
        margin += end - pos;
    }
    ref var initialMargin = ref heap(new @string(), out var ᏑinitialMargin);
    if (margin < len(indentText)) {
        initialMargin = indentText.slice(margin);
    }
    return Ꮡp.parseTagComments(margin, ᏑinitialMargin);
}

internal static ж<ast.NodeList> parseTagComments(this ж<Parser> Ꮡp, nint indent, ж<@string> ᏑinitialMargin) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var initialMargin = ref ᏑinitialMargin.DerefOrNull();

    nint commentsPos = p.nodePos();
    ref var comments = ref heap<slice<@string>>(p.jsdocTagCommentsSpace, out var Ꮡcomments);
    p.jsdocTagCommentsSpace = default;
    var parts = p.jsdocTagCommentsPartsSpace;
    p.jsdocTagCommentsPartsSpace = default;
    nint linkEnd = -1;
    var state = jsdocStateBeginningOfLine;
    nint backtickCount = 0;
    var inFencedCodeBlock = false;
    if (indent < 0) {
        throw panic("indent must be a natural number");
    }
    nint margin = -1;
    void pushComment(@string text) {
        if (margin == -1) {
            margin = indent;
        }
        Ꮡcomments.ValueSlot = append(Ꮡcomments.ValueSlot, text);
        indent += len(text);
    }

    if (ᏑinitialMargin != nil) {
        if (initialMargin != ""u8) {
            pushComment(initialMargin);
        }
        state = jsdocStateSawAsterisk;
    }
    var tok = p.token;
loop:
    for (;;) {
        if (tok != ast.KindBacktickToken && backtickCount > 0) {
            if (backtickCount >= 3) {
                inFencedCodeBlock = !inFencedCodeBlock;
            }
            backtickCount = 0;
        }
        var matchᴛ1 = false;
        if (tok == ast.KindNewLineTrivia) { matchᴛ1 = true;
            state = jsdocStateBeginningOfLine;
            comments = append(comments, p.scanner.TokenText());
            indent = 0;
        }
        else if (tok == ast.KindAtToken) { matchᴛ1 = true;
            if (!inFencedCodeBlock && p.scanner.CanFollowJSDocAt()) {
                p.scanner.ResetPos(p.scanner.TokenEnd() - 1);
                goto break_loop;
            }
            if (inFencedCodeBlock) {
                state = jsdocStateSavingBackticks;
            } else {
                state = jsdocStateSavingComments;
            }
            pushComment(p.scanner.TokenText());
        }
        else if (tok == ast.KindEndOfFile) { matchᴛ1 = true;
            goto break_loop;
        }
        else if (tok == ast.KindWhitespaceTrivia) { matchᴛ1 = true;
            if (state == jsdocStateSavingComments || state == jsdocStateSavingBackticks) {
                throw panic("whitespace shouldn't come from the scanner while saving comment text");
            }
            @string whitespace = p.scanner.TokenText();
            if (margin > -1 && indent + len(whitespace) > margin) {
                comments = append(comments, whitespace.slice(max(margin - indent, 0)));
                if (inFencedCodeBlock) {
                    state = jsdocStateSavingBackticks;
                } else {
                    state = jsdocStateSavingComments;
                }
            }
            indent += len(whitespace);
        }
        else if (tok == ast.KindOpenBraceToken) { matchᴛ1 = true;
            do {
                if (inFencedCodeBlock) {
                    state = jsdocStateSavingBackticks;
                    pushComment(p.scanner.TokenText());
                    break;
                }
                state = jsdocStateSavingComments;
                nint commentEnd = p.scanner.TokenFullStart();
                nint linkStart = p.scanner.TokenEnd() - 1;
                var link = Ꮡp.parseJSDocLink(linkStart);
                if (link != nil) {
                    nint commentStart = default;
                    if (linkEnd > -1) {
                        commentStart = linkEnd;
                    } else {
                        commentStart = commentsPos;
                    }
                    var text = p.finishNodeWithEnd(p.factory.NewJSDocText(p.stringSliceArena.Clone(comments)), commentStart, commentEnd);
                    parts = append(parts, text);
                    parts = append(parts, link);
                    comments = comments[..0];
                    linkEnd = p.scanner.TokenEnd();
                } else {
                    pushComment(p.scanner.TokenText());
                }
            } while (false);
        }
        else if (tok == ast.KindBacktickToken) { matchᴛ1 = true;
            backtickCount++;
            if (state == jsdocStateSavingBackticks) {
                state = jsdocStateSavingComments;
            } else {
                state = jsdocStateSavingBackticks;
            }
            pushComment(p.scanner.TokenText());
        }
        else if (tok == ast.KindJSDocCommentTextToken) { matchᴛ1 = true;
            if (state != jsdocStateSavingBackticks) {
                if (inFencedCodeBlock) {
                    state = jsdocStateSavingBackticks;
                } else {
                    state = jsdocStateSavingComments;
                }
            }
            pushComment(p.scanner.TokenValue());
        }
        else if (tok == ast.KindAsteriskToken) { matchᴛ1 = true;
            do {
                if (state == jsdocStateBeginningOfLine) {
                    state = jsdocStateSawAsterisk;
                    indent += 1;
                    break;
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1) { /* default: */
            if (state != jsdocStateSavingBackticks) {
                if (inFencedCodeBlock) {
                    state = jsdocStateSavingBackticks;
                } else {
                    state = jsdocStateSavingComments;
                }
            }
            pushComment(p.scanner.TokenText());
        }

        if (state == jsdocStateSavingComments || state == jsdocStateSavingBackticks) {
            tok = p.nextJSDocCommentTextToken(state == jsdocStateSavingBackticks);
        } else {
            tok = p.nextTokenJSDoc();
        }
continue_loop:;
    }
break_loop:;

    p.jsdocTagCommentsSpace = comments[..0];

    comments = removeLeadingNewlines(comments);
    comments = removeTrailingWhitespace(comments);
    if (len(comments) > 0) {
        nint commentStart = default;
        if (linkEnd > -1) {
            commentStart = linkEnd;
        } else {
            commentStart = commentsPos;
        }
        var text = p.finishNode(p.factory.NewJSDocText(p.stringSliceArena.Clone(comments)), commentStart);
        parts = append(parts, text);
    }

    p.jsdocTagCommentsPartsSpace = parts[..0];

    if (len(parts) > 0) {
        return p.newNodeList(core.NewTextRange(commentsPos, p.scanner.TokenEnd()), p.nodeSliceArena.Clone(parts));
    }
    return default;
}

internal static ж<ast.Node> parseJSDocLink(this ж<Parser> Ꮡp, nint start) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.mark();
    var (linkType, ok) = Ꮡp.parseJSDocLinkPrefix();
    if (!ok) {
        p.rewind(state);
        return default;
    }
    p.nextTokenJSDoc();
    Ꮡp.skipWhitespace();
    var name = p.parseJSDocLinkName();
    slice<@string> text = default;
    while (p.token != ast.KindCloseBraceToken && p.token != ast.KindNewLineTrivia && p.token != ast.KindEndOfFile) {
        text = append(text, p.scanner.TokenText());
        p.nextTokenJSDoc();
    }
    ж<ast.Node> create = default;
    switch (ᐧ) {
    case {} when linkType == "link"u8: {
        create = p.factory.NewJSDocLink(name, text);
        break;
    }
    case {} when linkType == "linkcode"u8: {
        create = p.factory.NewJSDocLinkCode(name, text);
        break;
    }
    default: {
        create = p.factory.NewJSDocLinkPlain(name, text);
        break;
    }}

    return p.finishNodeWithEnd(create, start, p.scanner.TokenEnd());
}

internal static ж<ast.Node> parseJSDocLinkName(this ref Parser p) {
    if (tokenIsIdentifierOrKeyword(p.token)) {
        nint pos = p.nodePos();
        var name = p.parseIdentifierName();
        while (p.parseOptional(ast.KindDotToken)) {
            ж<astꓸIdentifierNode> right = default;
            if (p.token == ast.KindPrivateIdentifier) {
                right = p.createMissingIdentifier();
            } else {
                right = p.parseIdentifierName();
            }
            name = p.finishNode(p.factory.NewQualifiedName(name, right), pos);
        }
        while (p.token == ast.KindPrivateIdentifier) {
            p.scanner.ReScanHashToken();
            p.nextTokenJSDoc();
            name = p.finishNode(p.factory.NewQualifiedName(name, p.parseIdentifier()), pos);
        }
        return name;
    }
    return default;
}

internal static (@string, bool) parseJSDocLinkPrefix(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.skipWhitespaceOrAsterisk();
    if (p.token == ast.KindOpenBraceToken && p.nextTokenJSDoc() == ast.KindAtToken && tokenIsIdentifierOrKeyword(p.nextTokenJSDoc())) {
        @string kind = p.scanner.TokenValue();
        if (isJSDocLinkTag(kind)) {
            return (kind, true);
        }
    }
    return ("NONE", false);
}

} partial class parser_package {

internal static bool isJSDocLinkTag(@string kind) {
    return kind == "link"u8 || kind == "linkcode"u8 || kind == "linkplain"u8;
}

} partial class parserᴛParser {

internal static ж<ast.Node> parseUnknownTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.finishNode(p.factory.NewJSDocUnknownTag(tagName, Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText)), start);
}

internal static ж<ast.Node> tryParseTypeExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.skipWhitespaceOrAsterisk();
    if (p.token == ast.KindOpenBraceToken) {
        return Ꮡp.parseJSDocTypeExpression(false);
    } else {
        return default;
    }
}

internal static (ж<astꓸEntityName> name, bool isBracketed) parseBracketNameInPropertyAndParamTag(this ж<Parser> Ꮡp, propertyLikeParse target) {
    ж<astꓸEntityName> name = default;
    bool isBracketed = default;
    ref var p = ref Ꮡp.DerefOrNull();

    isBracketed = p.parseOptionalJsdoc(ast.KindOpenBracketToken);
    if (isBracketed) {
        Ꮡp.skipWhitespace();
    }
    var isBackquoted = p.parseOptionalJsdoc(ast.KindBacktickToken);
    name = p.parseJSDocEntityName(core.IfElse(target == propertyLikeParseParameter, (ж<diagnostics.Message>)(nil), diagnostics.Identifier_expected));
    if (isBackquoted) {
        p.parseExpectedTokenJSDoc(ast.KindBacktickToken);
    }
    if (isBracketed) {
        Ꮡp.skipWhitespace();
        if (p.parseOptionalToken(ast.KindEqualsToken) != nil) {
            Ꮡp.parseExpression();
        }

        p.parseExpected(ast.KindCloseBracketToken);
    }

    return (name, isBracketed);
}

} partial class parser_package {

internal static bool isObjectOrObjectArrayTypeReference(ж<astꓸTypeNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindObjectKeyword: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindArrayType: {
        return isObjectOrObjectArrayTypeReference(node.AsArrayTypeNode().ElementType);
    }
    default: {
        if (ast.IsTypeReferenceNode(Ꮡnode)) {
            var @ref = node.AsTypeReferenceNode();
            return ast.IsIdentifier(@ref.TypeName) && @ref.TypeName.Text() == "Object"u8 && (~@ref).TypeArguments == nil;
        }
        return false;
    }}

}

} partial class parserᴛParser {

internal static ж<ast.Node> parseParameterOrPropertyTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, propertyLikeParse target, nint indent) {
    ref var p = ref Ꮡp.DerefOrNull();

    var typeExpression = Ꮡp.tryParseTypeExpression();
    var isNameFirst = typeExpression == nil;
    Ꮡp.skipWhitespaceOrAsterisk();

    var (name, isBracketed) = Ꮡp.parseBracketNameInPropertyAndParamTag(target);
    @string indentText = Ꮡp.skipWhitespaceOrAsterisk();

    if (isNameFirst && Ꮡp.lookAhead((ж<Parser> pΔ1) => {
        var (_, ok) = pΔ1.parseJSDocLinkPrefix();
        return !ok;
    })) {
        typeExpression = Ꮡp.tryParseTypeExpression();
    }

    var comment = Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText);

    var nestedTypeLiteral = Ꮡp.parseNestedTypeLiteral(typeExpression, name, target, indent);
    if (nestedTypeLiteral != nil) {
        typeExpression = nestedTypeLiteral;
        isNameFirst = true;
    }
    ж<ast.Node> result = default;
    var kind = core.IfElse(target == propertyLikeParseProperty, ast.KindJSDocPropertyTag, ast.KindJSDocParameterTag);
    result = p.factory.NewJSDocParameterOrPropertyTag(kind, tagName, name, isBracketed, typeExpression, isNameFirst, comment);
    return p.finishNode(result, start);
}

internal static ж<ast.Node> parseNestedTypeLiteral(this ж<Parser> Ꮡp, ж<ast.Node> ᏑtypeExpression, ж<astꓸEntityName> name, propertyLikeParse target, nint indent) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var typeExpression = ref ᏑtypeExpression.DerefOrNull();

    if (ᏑtypeExpression != nil && isObjectOrObjectArrayTypeReference(typeExpression.Type())) {
        nint pos = p.nodePos();
        slice<ж<ast.Node>> children = default;
        for (;;) {
            var state = p.mark();
            var child = Ꮡp.parseChildParameterOrPropertyTag(target, indent, name);
            if (child == nil) {
                p.rewind(state);
                break;
            }
            switch (child.Kind) {
            case var ᴛ when ᴛ == ast.KindJSDocParameterTag || ᴛ == ast.KindJSDocPropertyTag: {
                children = append(children, child);
                break;
            }
            case var ᴛ when ᴛ == ast.KindJSDocTemplateTag: {
                p.parseErrorAtRange(child.TagName().Loc, diagnostics.A_JSDoc_template_tag_may_not_follow_a_typedef_callback_or_overload_tag);
                break;
            }}

        }
        if (children != nil) {
            var literal = p.finishNode(p.factory.NewJSDocTypeLiteral(children, typeExpression.Type().Kind == ast.KindArrayType), pos);
            return p.finishNode(p.factory.NewJSDocTypeExpression(literal), pos);
        }
    }
    return default;
}

internal static ж<ast.Node> parseReturnTag(this ж<Parser> Ꮡp, slice<ж<ast.Node>> previousTags, nint start, ж<astꓸIdentifierNode> ᏑtagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var tagName = ref ᏑtagName.DerefOrNull();

    if (core.Some<ж<ast.Node>>(previousTags, ast.IsJSDocReturnTag)) {
        p.parseErrorAt(tagName.Pos(), p.scanner.TokenStart(), diagnostics.X_0_tag_already_specified, tagName.Text());
    }

    var typeExpression = Ꮡp.tryParseTypeExpression();
    return p.finishNode(p.factory.NewJSDocReturnTag(ᏑtagName, typeExpression, Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText)), start);
}

internal static ж<ast.Node> parseTypeTag(this ж<Parser> Ꮡp, slice<ж<ast.Node>> previousTags, nint start, ж<astꓸIdentifierNode> ᏑtagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var tagName = ref ᏑtagName.DerefOrNull();

    if (core.Some<ж<ast.Node>>(previousTags, ast.IsJSDocTypeTag)) {
        p.parseErrorAt(tagName.Pos(), p.scanner.TokenStart(), diagnostics.X_0_tag_already_specified, tagName.Text());
    }

    var typeExpression = Ꮡp.parseJSDocTypeExpression(true);
    ж<ast.NodeList> comments = default;
    if (indent != -1) {
        comments = Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText);
    }
    return p.finishNode(p.factory.NewJSDocTypeTag(ᏑtagName, typeExpression, comments), start);
}

internal static ж<ast.Node> parseSeeTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    var hasNameReference = p.isIdentifier() && !strings.HasPrefix(p.sourceText.slice(p.scanner.TokenEnd()), "://") || p.token == ast.KindOpenBraceToken && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeyword)));
    ж<ast.Node> nameExpression = default;
    if (hasNameReference) {
        nameExpression = p.parseJSDocNameReference();
    }
    var comments = Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText);
    return p.finishNode(p.factory.NewJSDocSeeTag(tagName, nameExpression, comments), start);
}

internal static ж<ast.Node> parseImplementsTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint margin, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    var className = Ꮡp.parseExpressionWithTypeArgumentsForAugments();
    return p.finishNode(p.factory.NewJSDocImplementsTag(tagName, className, Ꮡp.parseTrailingTagComments(start, p.nodePos(), margin, indentText)), start);
}

internal static ж<ast.Node> parseAugmentsTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint margin, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    var className = Ꮡp.parseExpressionWithTypeArgumentsForAugments();
    return p.finishNode(p.factory.NewJSDocAugmentsTag(tagName, className, Ꮡp.parseTrailingTagComments(start, p.nodePos(), margin, indentText)), start);
}

internal static ж<ast.Node> parseSatisfiesTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint margin, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    var typeExpression = Ꮡp.parseJSDocTypeExpression(false);
    var comments = Ꮡp.parseTrailingTagComments(start, p.nodePos(), margin, indentText);
    return p.finishNode(p.factory.NewJSDocSatisfiesTag(tagName, typeExpression, comments), start);
}

internal static ж<ast.Node> parseThrowsTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint margin, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    var typeExpression = Ꮡp.tryParseTypeExpression();
    var comment = Ꮡp.parseTrailingTagComments(start, p.nodePos(), margin, indentText);
    return p.finishNode(p.factory.NewJSDocThrowsTag(tagName, typeExpression, comment), start);
}

internal static ж<ast.Node> parseImportTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint margin, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint afterImportTagPos = p.scanner.TokenFullStart();

    ж<astꓸIdentifierNode> identifier = default;
    if (p.isIdentifier()) {
        identifier = p.parseIdentifier();
    }

    var importClause = Ꮡp.tryParseImportClause(identifier, afterImportTagPos, ast.KindTypeKeyword, true);
    var moduleSpecifier = Ꮡp.parseModuleSpecifier();
    var attributes = Ꮡp.tryParseImportAttributes();

    var comments = Ꮡp.parseTrailingTagComments(start, p.nodePos(), margin, indentText);
    return p.finishNode(p.factory.NewJSDocImportTag(tagName, importClause, moduleSpecifier, attributes, comments), start);
}

internal static ж<ast.Node> parseExpressionWithTypeArgumentsForAugments(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var usedBrace = p.parseOptional(ast.KindOpenBraceToken);
    nint pos = p.nodePos();
    var expression = p.parsePropertyAccessEntityNameExpression();
    p.scanner.SetSkipJSDocLeadingAsterisks(true);
    var typeArguments = Ꮡp.parseTypeArguments();
    p.scanner.SetSkipJSDocLeadingAsterisks(false);
    var node = p.finishNode(p.factory.NewExpressionWithTypeArguments(expression, typeArguments), pos);
    if (usedBrace) {
        Ꮡp.skipWhitespace();
        p.parseExpected(ast.KindCloseBraceToken);
    }
    return node;
}

internal static ж<ast.Node> parsePropertyAccessEntityNameExpression(this ref Parser p) {
    nint pos = p.nodePos();
    var node = p.parseJSDocIdentifierName(diagnostics.Identifier_expected);
    while (p.parseOptional(ast.KindDotToken)) {
        var name = p.parseJSDocIdentifierName(diagnostics.Identifier_expected);
        node = p.finishNode(p.factory.NewPropertyAccessExpression(node, nil, name, ast.NodeFlagsNone), pos);
    }
    return node;
}

internal static ж<ast.Node> parseSimpleTag(this ж<Parser> Ꮡp, nint start, Func<ж<astꓸIdentifierNode>, ж<ast.NodeList>, ж<ast.Node>> createTag, ж<astꓸIdentifierNode> tagName, nint margin, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.finishNode(createTag(tagName, Ꮡp.parseTrailingTagComments(start, p.nodePos(), margin, indentText)), start);
}

internal static ж<ast.Node> parseThisTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint margin, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    var typeExpression = Ꮡp.parseJSDocTypeExpression(true);
    Ꮡp.skipWhitespace();
    var result = p.factory.NewJSDocThisTag(tagName, typeExpression, Ꮡp.parseTrailingTagComments(start, p.nodePos(), margin, indentText));
    return p.finishNode(result, start);
}

internal static ж<ast.Node> parseJSDocTypeNameWithNamespace(this ref Parser p, bool nested) {
    nint start = p.scanner.TokenStart();
    if (!tokenIsIdentifierOrKeyword(p.token)) {
        return default;
    }
    var typeNameOrNamespaceName = p.parseJSDocIdentifierName(nil);
    if (p.parseOptionalJsdoc(ast.KindDotToken)) {
        var body = p.parseJSDocTypeNameWithNamespace(true);
        var jsDocNamespaceNode = p.factory.NewModuleDeclaration(
            nil,
            ast.KindNamespaceKeyword,
            typeNameOrNamespaceName,
            nil,
            body);
        if (nested) {
            jsDocNamespaceNode.Flags |= (ast.NodeFlags)(ast.NodeFlagsNestedNamespace);
        }
        return p.finishNode(jsDocNamespaceNode, start);
    }
    if (nested) {
        typeNameOrNamespaceName.Flags |= (ast.NodeFlags)(ast.NodeFlagsIdentifierIsInJSDocNamespace);
    }
    return typeNameOrNamespaceName;
}

internal static ж<ast.Node> parseTypedefTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> ᏑtagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var tagName = ref ᏑtagName.DerefOrNull();

    var typeExpression = Ꮡp.tryParseTypeExpression();
    Ꮡp.skipWhitespaceOrAsterisk();
    var fullName = p.parseJSDocTypeNameWithNamespace(false);
    if (fullName == nil) {
        fullName = p.parseJSDocIdentifierName(diagnostics.Identifier_expected);
    }
    Ꮡp.skipWhitespace();
    var comment = Ꮡp.parseTagComments(indent, nil);

    nint end = -1;
    var hasChildren = false;
    if (typeExpression == nil || isObjectOrObjectArrayTypeReference(typeExpression.Type())) {
        ж<ast.Node> child = default;
        ж<ast.JSDocTypeTag> childTypeTag = default;
        slice<ж<ast.Node>> jsdocPropertyTags = default;
        for (;;) {
            var state = p.mark();
            child = Ꮡp.parseChildPropertyTag(indent);
            if (child == nil) {
                p.rewind(state);
                break;
            }
            hasChildren = true;
            switch (child.Kind) {
            case var ᴛ when ᴛ == ast.KindJSDocTemplateTag: {
                p.parseErrorAtRange(child.TagName().Loc, diagnostics.A_JSDoc_template_tag_may_not_follow_a_typedef_callback_or_overload_tag);
                break;
            }
            case var ᴛ when ᴛ == ast.KindJSDocTypeTag: {
                if (childTypeTag == nil) {
                    childTypeTag = child.AsJSDocTypeTag();
                } else {
                    var lastError = p.parseErrorAtCurrentToken(diagnostics.A_JSDoc_typedef_comment_may_not_contain_multiple_type_tags);
                    if (lastError != nil) {
                        var related = ast.NewDiagnostic(nil, core.NewTextRange(0, 0), diagnostics.The_tag_was_first_specified_here);
                        lastError.AddRelatedInfo(related);
                    }
                }
                break;
            }
            default: {
                jsdocPropertyTags = append(jsdocPropertyTags, child);
                break;
            }}

        }
        if (hasChildren) {
            var isArrayType = typeExpression != nil && typeExpression.Type().Kind == ast.KindArrayType;
            var jsdocTypeLiteral = p.factory.NewJSDocTypeLiteral(jsdocPropertyTags, isArrayType);
            if (childTypeTag != nil && (~childTypeTag).TypeExpression != nil && !isObjectOrObjectArrayTypeReference((~childTypeTag).TypeExpression.Type())) {
                typeExpression = childTypeTag.Value.TypeExpression;
            } else {
                nint pos = start;
                if (len(jsdocPropertyTags) > 0) {
                    pos = jsdocPropertyTags[0].Pos();
                }
                typeExpression = p.finishNode(jsdocTypeLiteral, pos);
            }
            end = typeExpression.End();
        }
    }

    if (end == -1) {
        if (hasChildren && typeExpression != nil) {
            end = typeExpression.End();
        } else if (comment != nil) {
            end = p.nodePos();
        } else if (fullName != nil) {
            end = fullName.End();
        } else if (typeExpression != nil) {
            end = typeExpression.End();
        } else {
            end = tagName.End();
        }
    }

    if (comment == nil) {
        comment = Ꮡp.parseTrailingTagComments(start, end, indent, indentText);
    }

    var typedefTag = p.finishNodeWithEnd(p.factory.NewJSDocTypedefTag(ᏑtagName, typeExpression, fullName, comment), start, end);
    if (typeExpression != nil) {
        typeExpression.Parent = typedefTag;
    }
    return typedefTag;
}

internal static ж<ast.NodeList> parseCallbackTagParameters(this ж<Parser> Ꮡp, nint indent) {
    ref var p = ref Ꮡp.DerefOrNull();

    ж<ast.Node> child = default;
    slice<ж<ast.Node>> parameters = default;
    nint pos = p.nodePos();
    for (;;) {
        var state = p.mark();
        child = Ꮡp.parseChildParameterOrPropertyTag(propertyLikeParseCallbackParameter, indent, nil);
        if (child == nil) {
            p.rewind(state);
            break;
        }
        if (child.Kind == ast.KindJSDocTemplateTag) {
            p.parseErrorAtRange(child.TagName().Loc, diagnostics.A_JSDoc_template_tag_may_not_follow_a_typedef_callback_or_overload_tag);
        } else {
            parameters = append(parameters, child);
        }
    }
    return p.newNodeList(core.NewTextRange(pos, p.nodePos()), parameters);
}

internal static ж<ast.Node> parseJSDocSignature(this ж<Parser> Ꮡp, nint start, nint indent) {
    ref var p = ref Ꮡp.DerefOrNull();

    var parameters = Ꮡp.parseCallbackTagParameters(indent);
    ж<ast.Node> returnTag = default;
    var state = p.mark();
    if (p.parseOptionalJsdoc(ast.KindAtToken)) {
        var tag = Ꮡp.parseTag(default, indent);
        if (tag.Kind == ast.KindJSDocReturnTag) {
            returnTag = tag;
        }
    }
    if (returnTag == nil) {
        p.rewind(state);
    }
    return p.finishNode(p.factory.NewJSDocSignature(nil, parameters, returnTag), start);
}

internal static ж<ast.Node> parseCallbackTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    var fullName = p.parseJSDocTypeNameWithNamespace(false);
    if (fullName == nil) {
        fullName = p.parseJSDocIdentifierName(diagnostics.Identifier_expected);
    }
    Ꮡp.skipWhitespace();
    var comment = Ꮡp.parseTagComments(indent, nil);
    var typeExpression = Ꮡp.parseJSDocSignature(p.nodePos(), indent);
    if (comment == nil) {
        comment = Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText);
    }
    nint end = default;
    if (comment != nil) {
        end = p.nodePos();
    } else {
        end = typeExpression.End();
    }
    return p.finishNodeWithEnd(p.factory.NewJSDocCallbackTag(tagName, typeExpression, fullName, comment), start, end);
}

internal static ж<ast.Node> parseOverloadTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.skipWhitespace();
    var comment = Ꮡp.parseTagComments(indent, nil);
    var typeExpression = Ꮡp.parseJSDocSignature(start, indent);
    if (comment == nil) {
        comment = Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText);
    }
    nint end = default;
    if (comment != nil) {
        end = p.nodePos();
    } else {
        end = typeExpression.End();
    }
    return p.finishNodeWithEnd(p.factory.NewJSDocOverloadTag(tagName, typeExpression, comment), start, end);
}

} partial class parser_package {

internal static bool textsEqual(ж<astꓸEntityName> Ꮡa, ж<astꓸEntityName> Ꮡb) {
    ref var a = ref Ꮡa.DerefOrNull();
    ref var b = ref Ꮡb.DerefOrNull();

    while (!ast.IsIdentifier(Ꮡa) || !ast.IsIdentifier(Ꮡb)) {
        if (!ast.IsIdentifier(Ꮡa) && !ast.IsIdentifier(Ꮡb) && a.AsQualifiedName().Right.Text() == b.AsQualifiedName().Right.Text()) {
            Ꮡa = a.AsQualifiedName().Left; a = ref Ꮡa.DerefOrNull();
            Ꮡb = b.AsQualifiedName().Left; b = ref Ꮡb.DerefOrNull();
        } else {
            return false;
        }
    }
    return a.Text() == b.Text();
}

} partial class parserᴛParser {

internal static ж<ast.Node> parseChildPropertyTag(this ж<Parser> Ꮡp, nint indent) {
    return Ꮡp.parseChildParameterOrPropertyTag(propertyLikeParseProperty, indent, nil);
}

internal static ж<ast.Node> parseChildParameterOrPropertyTag(this ж<Parser> Ꮡp, propertyLikeParse target, nint indent, ж<astꓸEntityName> name) {
    ref var p = ref Ꮡp.DerefOrNull();

    var canParseTag = true;
    var seenAsterisk = false;
    for (;;) {
        switch (p.nextTokenJSDoc()) {
        case var ᴛ when ᴛ == ast.KindAtToken: {
            if (canParseTag && p.scanner.CanFollowJSDocAt()) {
                var child = Ꮡp.tryParseChildTag(target, indent);
                if (child != nil && name != nil && (child.Kind == ast.KindJSDocParameterTag || child.Kind == ast.KindJSDocPropertyTag) && (ast.IsIdentifier(child.Name()) || !textsEqual(name, child.Name().AsQualifiedName().Left))) {
                    return default;
                }
                return child;
            }
            seenAsterisk = false;
            break;
        }
        case var ᴛ when ᴛ == ast.KindNewLineTrivia: {
            canParseTag = true;
            seenAsterisk = false;
            break;
        }
        case var ᴛ when ᴛ == ast.KindAsteriskToken: {
            if (seenAsterisk) {
                canParseTag = false;
            }
            seenAsterisk = true;
            break;
        }
        case var ᴛ when ᴛ == ast.KindIdentifier: {
            canParseTag = false;
            break;
        }
        case var ᴛ when ᴛ == ast.KindEndOfFile: {
            return default;
        }}

    }
}

internal static ж<ast.Node> tryParseChildTag(this ж<Parser> Ꮡp, propertyLikeParse target, nint indent) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token != ast.KindAtToken) {
        throw panic("should only be called when at @");
    }
    nint start = p.scanner.TokenFullStart();
    p.nextTokenJSDoc();

    var tagName = p.parseJSDocIdentifierName(diagnostics.Identifier_expected);
    @string indentText = Ꮡp.skipWhitespaceOrAsterisk();
    propertyLikeParse t = default;
    var exprᴛ1 = tagName.Text();
    switch (ᐧ) {
    case {} when exprᴛ1 == "type"u8: {
        if (target == propertyLikeParseProperty) {
            return Ꮡp.parseTypeTag(default, start, tagName, -1, "");
        }
        break;
    }
    case {} when exprᴛ1 == "prop"u8 || exprᴛ1 == "property"u8: {
        t = propertyLikeParseProperty;
        break;
    }
    case {} when exprᴛ1 == "arg"u8 || exprᴛ1 == "argument"u8 || exprᴛ1 == "param"u8: {
        t = (propertyLikeParse)(propertyLikeParseParameter | propertyLikeParseCallbackParameter);
        break;
    }
    case {} when exprᴛ1 == "template"u8: {
        return Ꮡp.parseTemplateTag(start, tagName, indent, indentText);
    }
    case {} when exprᴛ1 == "this"u8: {
        return Ꮡp.parseThisTag(start, tagName, indent, indentText);
    }
    default: {
        return default;
    }}

    if (((propertyLikeParse)(target & t)) == 0) {
        return default;
    }
    return Ꮡp.parseParameterOrPropertyTag(start, tagName, target, indent);
}

internal static ж<ast.Node> parseTemplateTagTypeParameter(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint typeParameterPos = p.nodePos();
    var isBracketed = p.parseOptionalJsdoc(ast.KindOpenBracketToken);
    if (isBracketed) {
        Ꮡp.skipWhitespace();
    }

    var modifiers = Ꮡp.parseModifiersEx(false, true, false);
    var name = p.parseJSDocIdentifierName(diagnostics.Unexpected_token_A_type_parameter_name_was_expected_without_curly_braces);
    ж<ast.Node> defaultType = default;
    if (isBracketed) {
        Ꮡp.skipWhitespace();
        p.parseExpected(ast.KindEqualsToken);
        var saveContextFlags = p.contextFlags;
        p.setContextFlags(ast.NodeFlagsJSDoc, true);
        defaultType = Ꮡp.parseJSDocType();
        p.contextFlags = saveContextFlags;
        p.parseExpected(ast.KindCloseBracketToken);
    }

    if (ast.NodeIsMissing(name)) {
        return default;
    }
    return p.finishNode(p.factory.NewTypeParameterDeclaration(modifiers, name, nil, nil, defaultType), typeParameterPos);
}

internal static ж<astꓸTypeParameterList> parseTemplateTagTypeParameters(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    ref var typeParameters = ref heap<astꓸTypeParameterList>(new astꓸTypeParameterList(), out var ᏑtypeParameters);
    for (var ok = true; ok; ok = p.parseOptionalJsdoc(ast.KindCommaToken)) {
        Ꮡp.skipWhitespace();
        var node = Ꮡp.parseTemplateTagTypeParameter();
        if (node != nil) {
            typeParameters.Nodes = append(typeParameters.Nodes, node);
        }
        Ꮡp.skipWhitespaceOrAsterisk();
    }
    return ᏑtypeParameters;
}

internal static ж<ast.Node> parseTemplateTag(this ж<Parser> Ꮡp, nint start, ж<astꓸIdentifierNode> tagName, nint indent, @string indentText) {
    ref var p = ref Ꮡp.DerefOrNull();

    ж<ast.Node> constraint = default;
    if (p.token == ast.KindOpenBraceToken) {
        constraint = Ꮡp.parseJSDocTypeExpression(false);
    }
    var typeParameters = Ꮡp.parseTemplateTagTypeParameters();
    var result = p.factory.NewJSDocTemplateTag(tagName, constraint, typeParameters, Ꮡp.parseTrailingTagComments(start, p.nodePos(), indent, indentText));
    return p.finishNode(result, start);
}

internal static bool parseOptionalJsdoc(this ref Parser p, ast.Kind t) {
    if (p.token == t) {
        p.nextTokenJSDoc();
        return true;
    }
    return false;
}

internal static ж<astꓸEntityName> parseJSDocEntityName(this ref Parser p, ж<diagnostics.Message> diagnosticMessage) {
    ж<astꓸEntityName> entity = p.parseJSDocIdentifierName(diagnosticMessage);
    if (p.parseOptional(ast.KindOpenBracketToken)) {
        p.parseExpected(ast.KindCloseBracketToken);
    }
    while (p.parseOptional(ast.KindDotToken)) {
        var name = p.parseJSDocIdentifierName(diagnostics.Identifier_expected);
        if (p.parseOptional(ast.KindOpenBracketToken)) {
            p.parseExpected(ast.KindCloseBracketToken);
        }
        nint pos = entity.Pos();
        entity = p.finishNode(p.factory.NewQualifiedName(entity, name), pos);
    }
    return entity;
}

internal static ж<astꓸIdentifierNode> parseJSDocIdentifierName(this ref Parser p, ж<diagnostics.Message> diagnosticMessage) {
    if (!tokenIsIdentifierOrKeyword(p.token)) {
        if (diagnosticMessage != nil) {
            p.parseErrorAtCurrentToken(diagnosticMessage);
        } else if (isReservedWord(p.token)) {
            p.parseErrorAtCurrentToken(diagnostics.Identifier_expected_0_is_a_reserved_word_that_cannot_be_used_here, p.scanner.TokenText());
        }
        return p.finishNode(p.newIdentifier(""), p.nodePos());
    }
    nint pos = p.scanner.TokenStart();
    nint end = p.scanner.TokenEnd();
    @string text = p.scanner.TokenValue();
    p.nextTokenJSDoc();
    return p.finishNodeWithEnd(p.newIdentifier(text), pos, end);
}

} // end parserᴛParser
