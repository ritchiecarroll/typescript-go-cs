namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;

partial class parser_package {

internal static core.LanguageVariant getLanguageVariant(core.ScriptKind scriptKind) {
    switch (scriptKind) {
    case var ᴛ when ᴛ == core.ScriptKindTSX || ᴛ == core.ScriptKindJSX || ᴛ == core.ScriptKindJS || ᴛ == core.ScriptKindJSON: {
        return core.LanguageVariantJSX;
    }}

    return core.LanguageVariantStandard;
}

internal static bool tokenIsIdentifierOrKeyword(ast.Kind token) {
    return token >= ast.KindIdentifier;
}

internal static bool tokenIsIdentifierOrKeywordOrGreaterThan(ast.Kind token) {
    return token == ast.KindGreaterThanToken || tokenIsIdentifierOrKeyword(token);
}

public static slice<ast.CommentRange> GetJSDocCommentRanges(ж<ast.NodeFactory> f, slice<ast.CommentRange> commentRanges, ж<ast.Node> Ꮡnode, @string text) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindParameter || ᴛ == ast.KindTypeParameter || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindParenthesizedExpression || ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindExportSpecifier: {
        foreach (var commentRange in range<ast.CommentRange>(scanner.GetTrailingCommentRanges(f, text, node.Pos()).Invoke)) {
            commentRanges = append(commentRanges, commentRange);
        }
        foreach (var commentRange in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(f, text, node.Pos()).Invoke)) {
            commentRanges = append(commentRanges, commentRange);
        }
        break;
    }
    default: {
        foreach (var commentRange in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(f, text, node.Pos()).Invoke)) {
            commentRanges = append(commentRanges, commentRange);
        }
        break;
    }}

    return slices.DeleteFunc(commentRanges, (ast.CommentRange comment) => {
        nint commentStart = comment.TextRange.Pos();
        nint commentLen = comment.TextRange.End() - commentStart;
        return comment.TextRange.End() > Ꮡnode.Value.End() || commentLen < 4 || text[commentStart + 1] != '*' || text[commentStart + 2] != '*' || text[commentStart + 3] == '/';
    });
}

internal static bool isKeywordOrPunctuation(ast.Kind token) {
    return ast.IsKeywordKind(token) || ast.IsPunctuationKind(token);
}

internal static bool isJSDocLikeText(@string text) {
    return len(text) >= 4 && text[1] == '*' && text[2] == '*' && text[3] != '/';
}

} // end parser_package
