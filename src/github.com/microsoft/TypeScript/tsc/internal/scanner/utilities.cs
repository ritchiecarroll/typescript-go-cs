namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using unicode = unicode_package;
using utf8 = go.unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.unicode;

partial class scanner_package {

internal static bool tokenIsIdentifierOrKeyword(ast.Kind token) {
    return token >= ast.KindIdentifier;
}

public static ast.Kind IdentifierToKeywordKind(ж<ast.Identifier> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return textToKeyword[node.Text];
}

public static @string GetSourceTextOfNodeFromSourceFile(ж<ast.SourceFile> ᏑsourceFile, ж<ast.Node> node, bool includeTrivia) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    return GetTextOfNodeFromSourceText(sourceFile.Text(), node, includeTrivia);
}

internal static bool isJSDocTypeExpressionOrChild(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsJSDocTypeExpression(Ꮡnode)) {
        return true;
    }
    if ((ast.NodeFlags)(node.Flags & ((ast.NodeFlags)(ast.NodeFlagsJSDoc | ast.NodeFlagsReparsed))) == 0) {
        return false;
    }
    for (var current = Ꮡnode; current != nil; current = current.Parent) {
        if (ast.IsTypeNode(current)) {
            return true;
        }
    }
    return false;
}

internal static @string normalizeJSDocTypeSourceText(@string text) {
    var lineStarts = core.ComputeECMALineStarts(text);
    if (len(lineStarts) == 1) {
        return stripLeadingJSDocComment(text);
    }

    ref var result = ref heap(new strings.Builder(), out var Ꮡresult);
    Ꮡresult.Grow(len(text));
    @string newLine = core.NewLineKindLF.GetNewLineCharacter();
    foreach (var (i, lineStart) in range<core.TextPos>(lineStarts)) {
        if (i > 0) {
            Ꮡresult.WriteString(newLine);
        }
        nint lineEnd = len(text);
        if (i + 1 < len(lineStarts)) {
            lineEnd = (nint)lineStarts[i + 1];
        }
        @string line = strings.TrimRightFunc(text.slice(lineStart, lineEnd), stringutil.IsLineBreak);
        Ꮡresult.WriteString(stripLeadingJSDocComment(line));
    }
    return result.String();
}

internal static @string stripLeadingJSDocComment(@string line) {
    line = strings.TrimLeftFunc(line, stringutil.IsWhiteSpaceLike);
    if (len(line) > 0 && line[0] == '*') {
        line = line[1..];
    }
    return strings.TrimLeftFunc(line, stringutil.IsWhiteSpaceLike);
}

public static @string GetTextOfNodeFromSourceText(@string sourceText, ж<ast.Node> Ꮡnode, bool includeTrivia) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.NodeIsMissing(Ꮡnode)) {
        return "";
    }
    nint pos = node.Pos();
    if (!includeTrivia) {
        pos = SkipTrivia(sourceText, pos);
    }
    @string text = sourceText.slice(pos, node.End());
    if (isJSDocTypeExpressionOrChild(Ꮡnode)) {
        text = normalizeJSDocTypeSourceText(text);
    }
    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsReparserTransformedLiteral) != 0) {
        if (ast.IsStringLiteral(Ꮡnode)) {
            if ((ast.TokenFlags)(node.AsStringLiteral().TokenFlags & ast.TokenFlagsSingleQuote) != 0) {
                return "'"u8 + text + "'"u8;
            }
            return "\""u8 + text + "\""u8;
        } else if (ast.IsIdentifier(Ꮡnode)) {
            return node.Text();
        }
        debug.FailBadSyntaxKind(new ast.NodeжFailBadSyntaxKind_node(Ꮡnode), "Unexpected reparser-transformed node kind");
    }
    return text;
}

public static @string GetTextOfNode(ж<ast.Node> node) {
    return GetSourceTextOfNodeFromSourceFile(ast.GetSourceFileOfNode(node), node, false);
}

public static @string GetTextOfJSDocComment(ж<ast.NodeList> Ꮡcomment) {
    ref var comment = ref Ꮡcomment.DerefOrNull();

    if (Ꮡcomment == nil) {
        return "";
    }
    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    foreach (var (_, n) in range(comment.Nodes)) {
        switch (n.Kind) {
        case var ᴛ when ᴛ == ast.KindJSDocText: {
            Ꮡb.WriteString(n.Text());
            break;
        }
        case var ᴛ when ᴛ == ast.KindJSDocLink || ᴛ == ast.KindJSDocLinkCode || ᴛ == ast.KindJSDocLinkPlain: {
            Ꮡb.WriteString(GetTextOfNode(n));
            break;
        }}

    }
    return strings.TrimRightFunc(b.String(), unicode.IsSpace);
}

public static @string DeclarationNameToString(ж<ast.Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (Ꮡname == nil || name.Pos() == name.End()) {
        return "(Missing)";
    }
    return GetTextOfNode(Ꮡname);
}

public static bool IsIdentifierText(@string name, core.LanguageVariant languageVariant) {
    var (ch, size) = utf8.DecodeRuneInString(name);
    if (!IsIdentifierStart(ch)) {
        return false;
    }
    for (nint i = size; i < len(name); ) {
        (ch, size) = utf8.DecodeRuneInString(name.slice(i));
        if (!IsIdentifierPartEx(ch, languageVariant)) {
            return false;
        }
        i += size;
    }
    return true;
}

public static bool IsIntrinsicJsxName(@string name) {
    return len(name) != 0 && (name[0] >= 'a' && name[0] <= 'z' || strings.ContainsRune(name, '-'));
}

} // end scanner_package
