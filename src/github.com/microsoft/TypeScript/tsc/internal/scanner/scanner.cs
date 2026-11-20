namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using iter = iter_package;
using maps = maps_package;
using strconv = strconv_package;
using strings = strings_package;
using unicode = unicode_package;
using utf16 = go.unicode.utf16_package;
using utf8 = go.unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.unicode;
using ꓸꓸꓸany = Span<any>;
using ꓸꓸꓸbool = Span<bool>;
using ꓸꓸꓸstring = Span<@string>;
using static scanner_package;

partial class scanner_package {

partial struct identifierVariant /*num:int32*/;

internal static identifierVariant identifierVariantStandard => /* iota */ 0;
internal static identifierVariant identifierVariantJSX => 1;
internal static identifierVariant identifierVariantRegExpGroupName => 2;

partial struct EscapeSequenceScanningFlags /*num:int32*/;

public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsString => /* 1 << 0 */ 1;
public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsReportErrors => /* 1 << 1 */ 2;
public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsRegularExpression => /* 1 << 2 */ 4;
public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsAnnexB => /* 1 << 3 */ 8;
public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsAnyUnicodeMode => /* 1 << 4 */ 16;
public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsAtomEscape => /* 1 << 5 */ 32;
public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsReportInvalidEscapeErrors => /* EscapeSequenceScanningFlagsRegularExpression | EscapeSequenceScanningFlagsReportErrors */ 6;
public static EscapeSequenceScanningFlags EscapeSequenceScanningFlagsAllowExtendedUnicodeEscape => /* EscapeSequenceScanningFlagsString | EscapeSequenceScanningFlagsAnyUnicodeMode */ 17;

// type ErrorCallback is a methodless func type — rendered inline as its base delegate

[GoLazy] partial class lazyᴛtextToKeyword { internal static map<@string, ast.Kind> textToKeyword = new map<@string, ast.Kind>{
    ["abstract"] = ast.KindAbstractKeyword,
    ["accessor"] = ast.KindAccessorKeyword,
    ["any"] = ast.KindAnyKeyword,
    ["as"] = ast.KindAsKeyword,
    ["asserts"] = ast.KindAssertsKeyword,
    ["assert"] = ast.KindAssertKeyword,
    ["bigint"] = ast.KindBigIntKeyword,
    ["boolean"] = ast.KindBooleanKeyword,
    ["break"] = ast.KindBreakKeyword,
    ["case"] = ast.KindCaseKeyword,
    ["catch"] = ast.KindCatchKeyword,
    ["class"] = ast.KindClassKeyword,
    ["continue"] = ast.KindContinueKeyword,
    ["const"] = ast.KindConstKeyword,
    ["constructor"] = ast.KindConstructorKeyword,
    ["debugger"] = ast.KindDebuggerKeyword,
    ["declare"] = ast.KindDeclareKeyword,
    ["default"] = ast.KindDefaultKeyword,
    ["defer"] = ast.KindDeferKeyword,
    ["delete"] = ast.KindDeleteKeyword,
    ["do"] = ast.KindDoKeyword,
    ["else"] = ast.KindElseKeyword,
    ["enum"] = ast.KindEnumKeyword,
    ["export"] = ast.KindExportKeyword,
    ["extends"] = ast.KindExtendsKeyword,
    ["false"] = ast.KindFalseKeyword,
    ["finally"] = ast.KindFinallyKeyword,
    ["for"] = ast.KindForKeyword,
    ["from"] = ast.KindFromKeyword,
    ["function"] = ast.KindFunctionKeyword,
    ["get"] = ast.KindGetKeyword,
    ["if"] = ast.KindIfKeyword,
    ["immediate"] = ast.KindImmediateKeyword,
    ["implements"] = ast.KindImplementsKeyword,
    ["import"] = ast.KindImportKeyword,
    ["in"] = ast.KindInKeyword,
    ["infer"] = ast.KindInferKeyword,
    ["instanceof"] = ast.KindInstanceOfKeyword,
    ["interface"] = ast.KindInterfaceKeyword,
    ["intrinsic"] = ast.KindIntrinsicKeyword,
    ["is"] = ast.KindIsKeyword,
    ["keyof"] = ast.KindKeyOfKeyword,
    ["let"] = ast.KindLetKeyword,
    ["module"] = ast.KindModuleKeyword,
    ["namespace"] = ast.KindNamespaceKeyword,
    ["never"] = ast.KindNeverKeyword,
    ["new"] = ast.KindNewKeyword,
    ["null"] = ast.KindNullKeyword,
    ["number"] = ast.KindNumberKeyword,
    ["object"] = ast.KindObjectKeyword,
    ["package"] = ast.KindPackageKeyword,
    ["private"] = ast.KindPrivateKeyword,
    ["protected"] = ast.KindProtectedKeyword,
    ["public"] = ast.KindPublicKeyword,
    ["override"] = ast.KindOverrideKeyword,
    ["out"] = ast.KindOutKeyword,
    ["readonly"] = ast.KindReadonlyKeyword,
    ["require"] = ast.KindRequireKeyword,
    ["global"] = ast.KindGlobalKeyword,
    ["return"] = ast.KindReturnKeyword,
    ["satisfies"] = ast.KindSatisfiesKeyword,
    ["set"] = ast.KindSetKeyword,
    ["static"] = ast.KindStaticKeyword,
    ["string"] = ast.KindStringKeyword,
    ["super"] = ast.KindSuperKeyword,
    ["switch"] = ast.KindSwitchKeyword,
    ["symbol"] = ast.KindSymbolKeyword,
    ["this"] = ast.KindThisKeyword,
    ["throw"] = ast.KindThrowKeyword,
    ["true"] = ast.KindTrueKeyword,
    ["try"] = ast.KindTryKeyword,
    ["type"] = ast.KindTypeKeyword,
    ["typeof"] = ast.KindTypeOfKeyword,
    ["undefined"] = ast.KindUndefinedKeyword,
    ["unique"] = ast.KindUniqueKeyword,
    ["unknown"] = ast.KindUnknownKeyword,
    ["using"] = ast.KindUsingKeyword,
    ["var"] = ast.KindVarKeyword,
    ["void"] = ast.KindVoidKeyword,
    ["while"] = ast.KindWhileKeyword,
    ["with"] = ast.KindWithKeyword,
    ["yield"] = ast.KindYieldKeyword,
    ["async"] = ast.KindAsyncKeyword,
    ["await"] = ast.KindAwaitKeyword,
    ["of"] = ast.KindOfKeyword
}; }

internal static map<@string, ast.Kind> textToToken = func(map<@string, ast.Kind> () => {
    var m = new map<@string, ast.Kind>{
        ["{"] = ast.KindOpenBraceToken,
        ["}"] = ast.KindCloseBraceToken,
        ["("] = ast.KindOpenParenToken,
        [")"] = ast.KindCloseParenToken,
        ["["] = ast.KindOpenBracketToken,
        ["]"] = ast.KindCloseBracketToken,
        ["."] = ast.KindDotToken,
        ["..."] = ast.KindDotDotDotToken,
        [";"] = ast.KindSemicolonToken,
        [","] = ast.KindCommaToken,
        ["<"] = ast.KindLessThanToken,
        [">"] = ast.KindGreaterThanToken,
        ["<="] = ast.KindLessThanEqualsToken,
        [">="] = ast.KindGreaterThanEqualsToken,
        ["=="] = ast.KindEqualsEqualsToken,
        ["!="] = ast.KindExclamationEqualsToken,
        ["==="] = ast.KindEqualsEqualsEqualsToken,
        ["!=="] = ast.KindExclamationEqualsEqualsToken,
        ["=>"] = ast.KindEqualsGreaterThanToken,
        ["+"] = ast.KindPlusToken,
        ["-"] = ast.KindMinusToken,
        ["**"] = ast.KindAsteriskAsteriskToken,
        ["*"] = ast.KindAsteriskToken,
        ["/"] = ast.KindSlashToken,
        ["%"] = ast.KindPercentToken,
        ["++"] = ast.KindPlusPlusToken,
        ["--"] = ast.KindMinusMinusToken,
        ["<<"] = ast.KindLessThanLessThanToken,
        ["</"] = ast.KindLessThanSlashToken,
        [">>"] = ast.KindGreaterThanGreaterThanToken,
        [">>>"] = ast.KindGreaterThanGreaterThanGreaterThanToken,
        ["&"] = ast.KindAmpersandToken,
        ["|"] = ast.KindBarToken,
        ["^"] = ast.KindCaretToken,
        ["!"] = ast.KindExclamationToken,
        ["~"] = ast.KindTildeToken,
        ["&&"] = ast.KindAmpersandAmpersandToken,
        ["||"] = ast.KindBarBarToken,
        ["?"] = ast.KindQuestionToken,
        ["??"] = ast.KindQuestionQuestionToken,
        ["?."] = ast.KindQuestionDotToken,
        [":"] = ast.KindColonToken,
        ["="] = ast.KindEqualsToken,
        ["+="] = ast.KindPlusEqualsToken,
        ["-="] = ast.KindMinusEqualsToken,
        ["*="] = ast.KindAsteriskEqualsToken,
        ["**="] = ast.KindAsteriskAsteriskEqualsToken,
        ["/="] = ast.KindSlashEqualsToken,
        ["%="] = ast.KindPercentEqualsToken,
        ["<<="] = ast.KindLessThanLessThanEqualsToken,
        [">>="] = ast.KindGreaterThanGreaterThanEqualsToken,
        [">>>="] = ast.KindGreaterThanGreaterThanGreaterThanEqualsToken,
        ["&="] = ast.KindAmpersandEqualsToken,
        ["|="] = ast.KindBarEqualsToken,
        ["^="] = ast.KindCaretEqualsToken,
        ["||="] = ast.KindBarBarEqualsToken,
        ["&&="] = ast.KindAmpersandAmpersandEqualsToken,
        ["??="] = ast.KindQuestionQuestionEqualsToken,
        ["@"] = ast.KindAtToken,
        ["#"] = ast.KindHashToken,
        ["`"] = ast.KindBacktickToken
    };
    maps.Copy<map<@string, ast.Kind>, map<@string, ast.Kind>, @string, ast.Kind>(m, textToKeyword);
    return m;
})();

partial struct ScannerState {
    internal nint pos;
    internal nint fullStartPos;
    internal nint tokenStart;
    internal ast.Kind token;
    internal @string tokenValue;
    internal ast.TokenFlags tokenFlags;
    internal slice<ast.CommentDirective> commentDirectives;
    internal nint skipJSDocLeadingAsterisks;
}

partial struct Scanner {
    internal @string text;
    internal nint end;
    internal core.LanguageVariant languageVariant;
    internal core.ScriptTarget scriptTarget;
    internal Actionꓸꓸꓸ<ж<diagnostics.Message>, nint, nint, any> onError;
    internal bool skipTrivia;
    /*embed*/ public ScannerState ScannerState;
    internal map<@string, @string> numberCache;
    internal map<@string, @string> hexNumberCache;
    internal map<@string, @string> hexDigitCache;
}

internal static Scanner defaultScanner() {
    return new Scanner(skipTrivia: true);
}

public static ж<Scanner> NewScanner() {
    ref var s = ref heap<Scanner>(defaultScanner(), out var Ꮡs);
    return Ꮡs;
}

} partial class scannerᴛScanner {

public static void Reset(this ref Scanner s) {
    var numberCache = cleared<map<@string, @string>, @string, @string>(s.numberCache);
    var hexNumberCache = cleared<map<@string, @string>, @string, @string>(s.hexNumberCache);
    var hexDigitCache = cleared<map<@string, @string>, @string, @string>(s.hexDigitCache);
    s = defaultScanner();
    s.numberCache = numberCache;
    s.hexNumberCache = hexNumberCache;
    s.hexDigitCache = hexDigitCache;
}

} partial class scanner_package {

internal static M cleared<M, K, V>(M m)
    where M : /* ~map[K]V */ IMap<K, V>, ISupportMake<M>, new()
{
    clear(m);
    return m;
}

} partial class scannerᴛScanner {

public static @string Text(this ref Scanner s) {
    return s.text;
}

public static ast.Kind Token(this ref Scanner s) {
    return s.token;
}

public static ast.TokenFlags TokenFlags(this ref Scanner s) {
    return s.tokenFlags;
}

public static nint TokenFullStart(this ref Scanner s) {
    return s.fullStartPos;
}

public static nint TokenStart(this ref Scanner s) {
    return s.tokenStart;
}

public static nint TokenEnd(this ref Scanner s) {
    return s.pos;
}

public static @string TokenText(this ref Scanner s) {
    return s.text.slice(s.tokenStart, s.pos);
}

public static @string TokenValue(this ref Scanner s) {
    return s.tokenValue;
}

public static core.TextRange TokenRange(this ref Scanner s) {
    return core.NewTextRange(s.tokenStart, s.pos);
}

public static slice<ast.CommentDirective> CommentDirectives(this ref Scanner s) {
    return s.commentDirectives;
}

public static ScannerState Mark(this ref Scanner s) {
    return s.ScannerState;
}

public static void Rewind(this ref Scanner s, ScannerState state) {
    s.ScannerState = state;
}

public static void ResetPos(this ref Scanner s, nint pos) {
    if (pos < 0) {
        throw panic("Cannot reset token state to negative position");
    }
    s.pos = pos;
    s.fullStartPos = pos;
    s.tokenStart = pos;
}

public static void ResetTokenState(this ref Scanner s, nint pos) {
    s.ResetPos(pos);
    s.token = ast.KindUnknown;
    s.tokenValue = "";
    s.tokenFlags = ast.TokenFlagsNone;
}

public static void SetSkipJSDocLeadingAsterisks(this ref Scanner scanner, bool skip) {
    if (skip) {
        scanner.skipJSDocLeadingAsterisks += 1;
    } else {
        scanner.skipJSDocLeadingAsterisks += -1;
    }
}

public static void SetSkipTrivia(this ref Scanner scanner, bool skip) {
    scanner.skipTrivia = skip;
}

public static bool HasUnicodeEscape(this ref Scanner s) {
    return (ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsUnicodeEscape) != 0;
}

public static bool HasExtendedUnicodeEscape(this ref Scanner s) {
    return (ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsExtendedUnicodeEscape) != 0;
}

public static bool HasPrecedingLineBreak(this ref Scanner s) {
    return (ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingLineBreak) != 0;
}

public static bool HasPrecedingJSDocComment(this ref Scanner s) {
    return (ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingJSDocComment) != 0;
}

public static bool HasPrecedingJSDocLeadingAsterisks(this ref Scanner s) {
    return (ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingJSDocLeadingAsterisks) != 0;
}

public static bool HasPrecedingJSDocWithDeprecatedTag(this ref Scanner s) {
    return (ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingJSDocWithDeprecated) != 0;
}

public static bool HasPrecedingJSDocWithSeeOrLink(this ref Scanner s) {
    return (ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingJSDocWithSeeOrLink) != 0;
}

internal static void scanJSDocCommentForTags(this ref Scanner s, @string commentText) {
    for (;;) {
        nint i = strings.IndexByte(commentText, (byte)'@');
        if (i < 0) {
            return;
        }
        commentText = commentText.slice(i + 1);
        if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingJSDocWithDeprecated) == 0 && hasJSDocTag(commentText, "deprecated")) {
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingJSDocWithDeprecated);
        }
        if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingJSDocWithSeeOrLink) == 0 && hasJSDocTag(commentText, "see", "link", "linkcode", "linkplain")) {
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingJSDocWithSeeOrLink);
        }
        if ((ast.TokenFlags)(s.tokenFlags & ((ast.TokenFlags)(ast.TokenFlagsPrecedingJSDocWithDeprecated | ast.TokenFlagsPrecedingJSDocWithSeeOrLink))) == ((ast.TokenFlags)(ast.TokenFlagsPrecedingJSDocWithDeprecated | ast.TokenFlagsPrecedingJSDocWithSeeOrLink))) {
            return;
        }
    }
}

} partial class scanner_package {

internal static bool hasJSDocTag(@string text, params ꓸꓸꓸstring tagsʗp) {
    var tags = tagsʗp.sslice();

    foreach (var (_, tag) in range(tags)) {
        if (!strings.HasPrefix(text, tag)) {
            continue;
        }
        if (len(text) == len(tag)) {
            return true;
        }
        var ch = text[len(tag)];
        if (ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r' || ch == '}' || ch == '*') {
            return true;
        }
    }
    return false;
}

} partial class scannerᴛScanner {

public static void SetText(this ref Scanner s, @string text) {
    s.text = text;
    s.end = len(text);
    s.ScannerState = new ScannerState();
}

public static void SetOnError(this ref Scanner s, Actionꓸꓸꓸ<ж<diagnostics.Message>, nint, nint, any> errorCallback) {
    s.onError = errorCallback;
}

public static void SetLanguageVariant(this ref Scanner s, core.LanguageVariant languageVariant) {
    s.languageVariant = languageVariant;
}

public static void SetScriptTarget(this ref Scanner s, core.ScriptTarget scriptTarget) {
    s.scriptTarget = scriptTarget;
}

internal static core.ScriptTarget languageVersion(this ref Scanner s) {
    if (s.scriptTarget == core.ScriptTargetNone) {
        return core.ScriptTargetLatest;
    }
    return s.scriptTarget;
}

internal static void error(this ref Scanner s, ж<diagnostics.Message> diagnostic) {
    s.errorAt(diagnostic, s.pos, 0);
}

internal static void errorAt(this ref Scanner s, ж<diagnostics.Message> diagnostic, nint pos, nint length, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    if (s.onError != nil) {
        s.onError(diagnostic, pos, length, args.ꓸꓸꓸ);
    }
}

internal static rune @char(this ref Scanner s) {
    if (s.pos < s.end) {
        return (rune)s.text[s.pos];
    }
    return -1;
}

internal static rune charAt(this ref Scanner s, nint offset) {
    if (s.pos + offset < s.end) {
        return (rune)s.text[s.pos + offset];
    }
    return -1;
}

internal static (rune, nint) charAndSize(this ref Scanner s) {
    if (s.pos < s.end) {
        var b = s.text[s.pos]; if (b < utf8.RuneSelf) {
            return ((rune)b, 1);
        }
    }
    return utf8.DecodeRuneInString(s.text.slice(s.pos));
}

internal static void scanASCIIWhile(this ref Scanner s, Func<byte, bool> pred) {
    @string text = s.text.slice(s.pos, s.end);
    nint i = 0;
    while (i < len(text)) {
        var b = text[i];
        if (b >= utf8.RuneSelf || !pred(b)) {
            break;
        }
        i++;
    }
    s.pos += i;
}

public static ast.Kind Scan(this ж<Scanner> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    s.fullStartPos = s.pos;
    s.tokenFlags = ast.TokenFlagsNone;
    for (;;) {
        var ch = s.@char();
        s.tokenStart = s.pos;

        var matchᴛ1 = false;
        if (ch is '\t' or '\v' or '\f' or ' ') { matchᴛ1 = true;
            s.pos++;
            if (s.skipTrivia) {
                continue;
            }
            for (;;) {
                var (chΔ4, size) = s.charAndSize();
                if (!stringutil.IsWhiteSpaceSingleLine(chΔ4)) {
                    break;
                }
                s.pos += size;
            }
            s.token = ast.KindWhitespaceTrivia;
        }
        else if (ch is '\n' or '\r') { matchᴛ1 = true;
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingLineBreak);
            if (s.skipTrivia) {
                s.pos++;
                s.scanASCIIWhile((byte b) => b == ' ' || (b >= '\t' && b <= '\r'));
                continue;
            }
            if (ch == '\r' && s.charAt(1) == '\n') {
                s.pos += 2;
            } else {
                s.pos++;
            }
            s.token = ast.KindNewLineTrivia;
        }
        else if (ch is '!') { matchᴛ1 = true;
            if (s.charAt(1) == '=') {
                if (s.charAt(2) == '=') {
                    s.pos += 3;
                    s.token = ast.KindExclamationEqualsEqualsToken;
                } else {
                    s.pos += 2;
                    s.token = ast.KindExclamationEqualsToken;
                }
            } else {
                s.pos++;
                s.token = ast.KindExclamationToken;
            }
        }
        else if (ch is '"' or '\'') { matchᴛ1 = true;
            s.tokenValue = s.scanString(false);
            s.token = ast.KindStringLiteral;
        }
        else if (ch is '`') { matchᴛ1 = true;
            s.token = s.scanTemplateAndSetTokenValue(false);
        }
        else if (ch is '%') { matchᴛ1 = true;
            if (s.charAt(1) == '=') {
                s.pos += 2;
                s.token = ast.KindPercentEqualsToken;
            } else {
                s.pos++;
                s.token = ast.KindPercentToken;
            }
        }
        else if (ch is '&') { matchᴛ1 = true;
            var next = s.charAt(1);
            if (next == '&') {
                if (s.charAt(2) == '=') {
                    s.pos += 3;
                    s.token = ast.KindAmpersandAmpersandEqualsToken;
                } else {
                    s.pos += 2;
                    s.token = ast.KindAmpersandAmpersandToken;
                }
            } else if (next == '=') {
                s.pos += 2;
                s.token = ast.KindAmpersandEqualsToken;
            } else {
                s.pos++;
                s.token = ast.KindAmpersandToken;
            }
        }
        else if (ch is '(') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindOpenParenToken;
        }
        else if (ch is ')') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindCloseParenToken;
        }
        else if (ch is '*') { matchᴛ1 = true;
            var next = s.charAt(1);
            if (next == '=') {
                s.pos += 2;
                s.token = ast.KindAsteriskEqualsToken;
            } else if (next == '*') {
                if (s.charAt(2) == '=') {
                    s.pos += 3;
                    s.token = ast.KindAsteriskAsteriskEqualsToken;
                } else {
                    s.pos += 2;
                    s.token = ast.KindAsteriskAsteriskToken;
                }
            } else {
                s.pos++;
                if (s.skipJSDocLeadingAsterisks != 0 && ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingJSDocLeadingAsterisks)) == 0 && ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsPrecedingLineBreak)) != 0) {
                    s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingJSDocLeadingAsterisks);
                    continue;
                }
                s.token = ast.KindAsteriskToken;
            }
        }
        else if (ch is '+') { matchᴛ1 = true;
            var next = s.charAt(1);
            if (next == '=') {
                s.pos += 2;
                s.token = ast.KindPlusEqualsToken;
            } else if (next == '+') {
                s.pos += 2;
                s.token = ast.KindPlusPlusToken;
            } else {
                s.pos++;
                s.token = ast.KindPlusToken;
            }
        }
        else if (ch is ',') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindCommaToken;
        }
        else if (ch is '-') { matchᴛ1 = true;
            var next = s.charAt(1);
            if (next == '=') {
                s.pos += 2;
                s.token = ast.KindMinusEqualsToken;
            } else if (next == '-') {
                s.pos += 2;
                s.token = ast.KindMinusMinusToken;
            } else {
                s.pos++;
                s.token = ast.KindMinusToken;
            }
        }
        else if (ch is '.') { matchᴛ1 = true;
            var next = s.charAt(1);
            if (stringutil.IsDigit(next)) {
                s.token = s.scanNumber();
            } else if (next == '.' && s.charAt(2) == '.') {
                s.pos += 3;
                s.token = ast.KindDotDotDotToken;
            } else {
                s.pos++;
                s.token = ast.KindDotToken;
            }
        }
        else if (ch is '/') { matchᴛ1 = true;
            if (s.charAt(1) == '/') {
                s.pos += 2;

                for (;;) {
                    s.scanASCIIWhile((byte b) => b != '\n' && b != '\r');
                    var (ch1, size) = s.charAndSize();
                    if (size == 0 || stringutil.IsLineBreak(ch1)) {
                        break;
                    }
                    s.pos += size;
                }

                s.processCommentDirective(s.tokenStart, s.pos, false);

                if (s.skipTrivia) {
                    continue;
                }
                s.token = ast.KindSingleLineCommentTrivia;
                return s.token;
            }
            if (s.charAt(1) == '*') {
                s.pos += 2;
                var isJSDoc = s.@char() == '*' && s.charAt(1) != '/';

                var commentClosed = false;
                nint lastLineStart = s.tokenStart;
                for (;;) {
                    s.scanASCIIWhile((byte b) => b != '*' && b != '\n' && b != '\r');
                    var (ch1, size) = s.charAndSize();
                    if (size == 0) {
                        break;
                    }

                    if (ch1 == '*' && s.charAt(1) == '/') {
                        s.pos += 2;
                        commentClosed = true;
                        break;
                    }

                    s.pos += size;

                    if (stringutil.IsLineBreak(ch1)) {
                        lastLineStart = s.pos;
                        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingLineBreak);
                    }
                }

                if (isJSDoc) {
                    s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingJSDocComment);
                    s.scanJSDocCommentForTags(s.text.slice(s.tokenStart, s.pos));
                }

                s.processCommentDirective(lastLineStart, s.pos, true);

                if (!commentClosed) {
                    s.error(diagnostics.Asterisk_Slash_expected);
                }

                if (s.skipTrivia) {
                    continue;
                }

                if (!commentClosed) {
                    s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsUnterminated);
                }
                s.token = ast.KindMultiLineCommentTrivia;
                return s.token;
            }
            if (s.charAt(1) == '=') {
                s.pos += 2;
                s.token = ast.KindSlashEqualsToken;
            } else {
                s.pos++;
                s.token = ast.KindSlashToken;
            }
        }
        else if (ch is '0') { matchᴛ1 = true;
            do {
                if (s.charAt(1) == 'X' || s.charAt(1) == 'x') {
                    nint start = s.pos;
                    s.pos += 2;
                    @string digits = s.scanHexDigits(1, true, true);
                    if (digits == ""u8) {
                        s.error(diagnostics.Hexadecimal_digit_expected);
                        digits = "0";
                    }
                    if (s.hexNumberCache == nil) {
                        s.hexNumberCache = new map<@string, @string>();
                    }
                    var (cachedValue, ok) = s.hexNumberCache[digits, ꟷ]; if (ok) {
                        s.tokenValue = cachedValue;
                    } else {
                        @string rawText = s.text.slice(start, s.pos);
                        if (strings.HasPrefix(rawText, "0x") && rawText[2..] == digits) {
                            s.tokenValue = rawText;
                        } else {
                            s.tokenValue = "0x"u8 + digits;
                        }
                        s.hexNumberCache[digits] = s.tokenValue;
                    }
                    s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsHexSpecifier);
                    s.token = s.scanBigIntSuffix();
                    break;
                }
                if (s.charAt(1) == 'B' || s.charAt(1) == 'b') {
                    s.pos += 2;
                    @string digits = s.scanBinaryOrOctalDigits(2);
                    if (digits == ""u8) {
                        s.error(diagnostics.Binary_digit_expected);
                        digits = "0";
                    }
                    s.tokenValue = "0b"u8 + digits;
                    s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsBinarySpecifier);
                    s.token = s.scanBigIntSuffix();
                    break;
                }
                if (s.charAt(1) == 'O' || s.charAt(1) == 'o') {
                    s.pos += 2;
                    @string digits = s.scanBinaryOrOctalDigits(8);
                    if (digits == ""u8) {
                        s.error(diagnostics.Octal_digit_expected);
                        digits = "0";
                    }
                    s.tokenValue = "0o"u8 + digits;
                    s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsOctalSpecifier);
                    s.token = s.scanBigIntSuffix();
                    break;
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1 && (ch is '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')) { matchᴛ1 = true;
            s.token = s.scanNumber();
        }
        else if (ch is ':') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindColonToken;
        }
        else if (ch is ';') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindSemicolonToken;
        }
        else if (ch is '<') { matchᴛ1 = true;
            if (s.charAt(1) == '<' && isConflictMarkerTrivia(s.text, s.pos)) {
                s.pos = scanConflictMarkerTrivia(s.text, s.pos, Ꮡs.errorAt);
                if (s.skipTrivia) {
                    continue;
                } else {
                    s.token = ast.KindConflictMarkerTrivia;
                    return s.token;
                }
            }
            if (s.charAt(1) == '<') {
                if (s.charAt(2) == '=') {
                    s.pos += 3;
                    s.token = ast.KindLessThanLessThanEqualsToken;
                } else {
                    s.pos += 2;
                    s.token = ast.KindLessThanLessThanToken;
                }
            } else if (s.charAt(1) == '=') {
                s.pos += 2;
                s.token = ast.KindLessThanEqualsToken;
            } else if (s.languageVariant == core.LanguageVariantJSX && s.charAt(1) == '/' && s.charAt(2) != '*') {
                s.pos += 2;
                s.token = ast.KindLessThanSlashToken;
            } else {
                s.pos++;
                s.token = ast.KindLessThanToken;
            }
        }
        else if (ch is '=') { matchᴛ1 = true;
            if (s.charAt(1) == '=' && isConflictMarkerTrivia(s.text, s.pos)) {
                s.pos = scanConflictMarkerTrivia(s.text, s.pos, Ꮡs.errorAt);
                if (s.skipTrivia) {
                    continue;
                } else {
                    s.token = ast.KindConflictMarkerTrivia;
                    return s.token;
                }
            }
            if (s.charAt(1) == '=') {
                if (s.charAt(2) == '=') {
                    s.pos += 3;
                    s.token = ast.KindEqualsEqualsEqualsToken;
                } else {
                    s.pos += 2;
                    s.token = ast.KindEqualsEqualsToken;
                }
            } else if (s.charAt(1) == '>') {
                s.pos += 2;
                s.token = ast.KindEqualsGreaterThanToken;
            } else {
                s.pos++;
                s.token = ast.KindEqualsToken;
            }
        }
        else if (ch is '>') { matchᴛ1 = true;
            if (s.charAt(1) == '>' && isConflictMarkerTrivia(s.text, s.pos)) {
                s.pos = scanConflictMarkerTrivia(s.text, s.pos, Ꮡs.errorAt);
                if (s.skipTrivia) {
                    continue;
                } else {
                    s.token = ast.KindConflictMarkerTrivia;
                    return s.token;
                }
            }
            s.pos++;
            s.token = ast.KindGreaterThanToken;
        }
        else if (ch is '?') { matchᴛ1 = true;
            if (s.charAt(1) == '.' && !stringutil.IsDigit(s.charAt(2))) {
                s.pos += 2;
                s.token = ast.KindQuestionDotToken;
            } else if (s.charAt(1) == '?') {
                if (s.charAt(2) == '=') {
                    s.pos += 3;
                    s.token = ast.KindQuestionQuestionEqualsToken;
                } else {
                    s.pos += 2;
                    s.token = ast.KindQuestionQuestionToken;
                }
            } else {
                s.pos++;
                s.token = ast.KindQuestionToken;
            }
        }
        else if (ch is '[') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindOpenBracketToken;
        }
        else if (ch is ']') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindCloseBracketToken;
        }
        else if (ch is '^') { matchᴛ1 = true;
            if (s.charAt(1) == '=') {
                s.pos += 2;
                s.token = ast.KindCaretEqualsToken;
            } else {
                s.pos++;
                s.token = ast.KindCaretToken;
            }
        }
        else if (ch is '{') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindOpenBraceToken;
        }
        else if (ch is '|') { matchᴛ1 = true;
            if (s.charAt(1) == '|' && isConflictMarkerTrivia(s.text, s.pos)) {
                s.pos = scanConflictMarkerTrivia(s.text, s.pos, Ꮡs.errorAt);
                if (s.skipTrivia) {
                    continue;
                } else {
                    s.token = ast.KindConflictMarkerTrivia;
                    return s.token;
                }
            }
            if (s.charAt(1) == '|') {
                if (s.charAt(2) == '=') {
                    s.pos += 3;
                    s.token = ast.KindBarBarEqualsToken;
                } else {
                    s.pos += 2;
                    s.token = ast.KindBarBarToken;
                }
            } else if (s.charAt(1) == '=') {
                s.pos += 2;
                s.token = ast.KindBarEqualsToken;
            } else {
                s.pos++;
                s.token = ast.KindBarToken;
            }
        }
        else if (ch is '}') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindCloseBraceToken;
        }
        else if (ch is '~') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindTildeToken;
        }
        else if (ch is '@') { matchᴛ1 = true;
            s.pos++;
            s.token = ast.KindAtToken;
        }
        else if (ch is '\\') { matchᴛ1 = true;
            if (s.scanIdentifier(0, identifierVariantStandard)) {
                s.token = GetIdentifierToken(s.tokenValue);
            } else {
                s.scanInvalidCharacter();
            }
        }
        else if (ch is '#') {
            do {
                if (s.charAt(1) == '!') {
                    if (s.pos == 0) {
                        s.pos += 2;
                        for (var (chΔ5, size) = s.charAndSize(); size > 0 && !stringutil.IsLineBreak(chΔ5); (chΔ5, size) = s.charAndSize()) {
                            s.pos += size;
                        }
                        goto continueᴛ1;
                    }
                    s.errorAt(diagnostics.X_can_only_be_used_at_the_start_of_a_file, s.pos, 2);
                    s.pos += 2;
                    s.token = ast.KindUnknown;
                    break;
                }
                if (!s.scanIdentifier(1, identifierVariantStandard)) {
                    s.errorAt(diagnostics.Invalid_character, s.pos - 1, 1);
                    s.tokenValue = "#";
                }
                s.token = ast.KindPrivateIdentifier;
            } while (false);
        }
        else if (!matchᴛ1) { /* default: */
            do {
                if (ch < 0) {
                    s.token = ast.KindEndOfFile;
                    break;
                }
                if (s.scanIdentifier(0, identifierVariantStandard)) {
                    s.token = GetIdentifierToken(s.tokenValue);
                    break;
                }
                var (chΔ6, size) = s.charAndSize();
                if (chΔ6 == utf8.RuneError) {
                    s.errorAt(diagnostics.File_appears_to_be_binary, 0, 0);
                    s.pos = len(s.text);
                    s.token = ast.KindNonTextFileMarkerTrivia;
                    break;
                }
                if (stringutil.IsWhiteSpaceSingleLine(chΔ6)) {
                    s.pos += size;

                    if (chΔ6 == 0x0085 || s.skipTrivia) {
                        goto continueᴛ1;
                    }

                    for (;;) {
                        (chΔ6, size) = s.charAndSize();
                        if (!stringutil.IsWhiteSpaceSingleLine(chΔ6)) {
                            break;
                        }
                        s.pos += size;
                    }
                    s.token = ast.KindWhitespaceTrivia;
                    return s.token;
                }
                if (stringutil.IsLineBreak(chΔ6)) {
                    s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingLineBreak);
                    s.pos += size;
                    goto continueᴛ1;
                }
                s.scanInvalidCharacter();
            } while (false);
        }

        return s.token;
continueᴛ1:;
    }
}

internal static void processCommentDirective(this ref Scanner s, nint start, nint end, bool multiline) {
    nint pos = start;
    if (multiline) {
        while (pos < end && (s.text[pos] == ' ' || s.text[pos] == '\t')) {
            pos++;
        }
        while (pos < end && (s.text[pos] == '/' || s.text[pos] == '*')) {
            pos++;
        }
    } else {
        pos += 2;
        while (pos < end && s.text[pos] == '/') {
            pos++;
        }
    }
    while (pos < end && (s.text[pos] == ' ' || s.text[pos] == '\t')) {
        pos++;
    }
    if (!(pos < end && s.text[pos] == '@')) {
        return;
    }
    pos++;
    ast.CommentDirectiveKind kind = default;
    switch (ᐧ) {
    case {} when strings.HasPrefix(s.text.slice(pos), "ts-expect-error"): {
        kind = ast.CommentDirectiveKindExpectError;
        break;
    }
    case {} when strings.HasPrefix(s.text.slice(pos), "ts-ignore"): {
        kind = ast.CommentDirectiveKindIgnore;
        break;
    }
    default: {
        return;
    }}

    s.commentDirectives = append(s.commentDirectives, new ast.CommentDirective(Loc: core.NewTextRange(start, end), Kind: kind));
}

public static ast.Kind ReScanLessThanToken(this ref Scanner s) {
    if (s.token == ast.KindLessThanLessThanToken) {
        s.pos = s.tokenStart + 1;
        s.token = ast.KindLessThanToken;
    }
    return s.token;
}

public static ast.Kind ReScanGreaterThanToken(this ref Scanner s) {
    if (s.token == ast.KindGreaterThanToken) {
        s.reScanGreaterThanTokenInner();
    }
    return s.token;
}

internal static void reScanGreaterThanTokenInner(this ref Scanner s) {
    s.pos = s.tokenStart + 1;
    if (s.@char() == '>') {
        if (s.charAt(1) == '>') {
            if (s.charAt(2) == '=') {
                s.pos += 3;
                s.token = ast.KindGreaterThanGreaterThanGreaterThanEqualsToken;
            } else {
                s.pos += 2;
                s.token = ast.KindGreaterThanGreaterThanGreaterThanToken;
            }
        } else if (s.charAt(1) == '=') {
            s.pos += 2;
            s.token = ast.KindGreaterThanGreaterThanEqualsToken;
        } else {
            s.pos++;
            s.token = ast.KindGreaterThanGreaterThanToken;
        }
    } else if (s.@char() == '=') {
        s.pos++;
        s.token = ast.KindGreaterThanEqualsToken;
    }
}

public static ast.Kind ReScanTemplateToken(this ref Scanner s, bool isTaggedTemplate) {
    s.pos = s.tokenStart;
    s.token = s.scanTemplateAndSetTokenValue(!isTaggedTemplate);
    return s.token;
}

public static ast.Kind ReScanAsteriskEqualsToken(this ref Scanner s) {
    if (s.token != ast.KindAsteriskEqualsToken) {
        throw panic("'ReScanAsteriskEqualsToken' should only be called on a '*='");
    }
    s.pos = s.tokenStart + 1;
    s.token = ast.KindEqualsToken;
    return s.token;
}

public static ast.Kind ReScanSlashToken(this ж<Scanner> Ꮡs, params ꓸꓸꓸbool reportErrorsʗp) {
    var reportErrors = reportErrorsʗp.sslice();
    ref var s = ref Ꮡs.DerefOrNull();

    var shouldReportErrors = len(reportErrors) > 0 && reportErrors[0];
    if (s.token == ast.KindSlashToken || s.token == ast.KindSlashEqualsToken) {
        nint startOfRegExpBody = s.tokenStart + 1;
        nint p = startOfRegExpBody;
        var inEscape = false;
        ref var namedCaptureGroups = ref heap<bool>(false, out var ᏑnamedCaptureGroups);
        var inCharacterClass = false;
loop:
        for (;;) {
            if (p >= s.end) {
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsUnterminated);
                goto break_loop;
            }
            var ch = (rune)s.text[p];
            switch (ᐧ) {
            case {} when stringutil.IsLineBreak(ch): {
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsUnterminated);
                goto break_loop;
                break;
            }
            case {} when inEscape: {
                inEscape = false;
                break;
            }
            case {} when ch == '/' && !inCharacterClass: {
                goto break_loop;
                break;
            }
            case {} when ch is '[': {
                inCharacterClass = true;
                break;
            }
            case {} when ch is '\\': {
                inEscape = true;
                break;
            }
            case {} when ch is ']': {
                inCharacterClass = false;
                break;
            }
            case {} when !inCharacterClass && ch == '(' && p + 1 < s.end && s.text[p + 1] == (byte)'?' && p + 2 < s.end && s.text[p + 2] == (byte)'<' && (p + 3 >= s.end || (s.text[p + 3] != (byte)'=' && s.text[p + 3] != (byte)'!')): {
                namedCaptureGroups = true;
                break;
            }}

            p++;
continue_loop:;
        }
break_loop:;

        ref var endOfRegExpBody = ref heap<nint>(p, out var ᏑendOfRegExpBody);
        if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsUnterminated) != 0) {
            p = startOfRegExpBody;
            inEscape = false;
            nint characterClassDepth = 0;
            var inDecimalQuantifier = false;
            nint groupDepth = 0;
            while (p < endOfRegExpBody) {
                var ch = (rune)s.text[p];
                if (inEscape) {
                    inEscape = false;
                } else if (ch == '\\') {
                    inEscape = true;
                } else if (ch == '[') {
                    characterClassDepth++;
                } else if (ch == ']' && characterClassDepth != 0) {
                    characterClassDepth--;
                } else if (characterClassDepth == 0) {
                    if (ch == '{') {
                        inDecimalQuantifier = true;
                    } else if (ch == '}' && inDecimalQuantifier) {
                        inDecimalQuantifier = false;
                    } else if (!inDecimalQuantifier) {
                        if (ch == '(') {
                            groupDepth++;
                        } else if (ch == ')' && groupDepth != 0) {
                            groupDepth--;
                        } else if (ch == ')' || ch == ']' || ch == '}') {
                            break;
                        }
                    }
                }
                p++;
            }
            while (p > startOfRegExpBody) {
                var (ch, size) = utf8.DecodeLastRuneInString(s.text.slice(0, p));
                if (stringutil.IsWhiteSpaceLike(ch) || ch == ';') {
                    p -= size;
                } else {
                    break;
                }
            }
            s.errorAt(diagnostics.Unterminated_regular_expression_literal, s.tokenStart, p - s.tokenStart);
        } else {
            p++;
            ref var regExpFlags = ref heap(new regularExpressionFlags(), out var ᏑregExpFlags);
            while (p < s.end) {
                var (ch, size) = utf8.DecodeRuneInString(s.text.slice(p));
                if (ch == utf8.RuneError || !IsIdentifierPart(ch)) {
                    break;
                }
                if (shouldReportErrors) {
                    var (flag, ok) = charCodeToRegExpFlag[ch, ꟷ];
                    if (!ok) {
                        s.errorAt(diagnostics.Unknown_regular_expression_flag, p, size);
                    } else if ((regularExpressionFlags)(regExpFlags & flag) != 0) {
                        s.errorAt(diagnostics.Duplicate_regular_expression_flag, p, size);
                    } else if ((regularExpressionFlags)(((regularExpressionFlags)(regExpFlags | flag)) & regularExpressionFlagsAnyUnicodeMode) == regularExpressionFlagsAnyUnicodeMode) {
                        s.errorAt(diagnostics.The_Unicode_u_flag_and_the_Unicode_Sets_v_flag_cannot_be_set_simultaneously, p, size);
                    } else {
                        regExpFlags |= (regularExpressionFlags)(flag);
                        s.checkRegularExpressionFlagAvailability(flag, p, size);
                    }
                }
                p += size;
            }
            if (shouldReportErrors) {
                s.pos = startOfRegExpBody;
                nint saveEnd = s.end;
                nint saveTokenPos = s.tokenStart;
                var saveTokenFlags = s.tokenFlags;
                s.end = endOfRegExpBody;
                var parser = Ꮡ(new regExpParser(
                    scanner: Ꮡs,
                    end: endOfRegExpBody,
                    regExpFlags: regExpFlags,
                    anyUnicodeMode: (regularExpressionFlags)(regExpFlags & regularExpressionFlagsAnyUnicodeMode) != 0,
                    unicodeSetsMode: (regularExpressionFlags)(regExpFlags & regularExpressionFlagsUnicodeSets) != 0,
                    annexB: true,
                    namedCaptureGroups: namedCaptureGroups,
                    groupSpecifiers: new map<@string, bool>()
                ));
                parser.run();
                s.end = saveEnd;
                s.pos = p;
                s.tokenStart = saveTokenPos;
                s.tokenFlags = saveTokenFlags;
            } else {
                s.pos = p;
            }
        }

        s.pos = p;
        s.tokenValue = s.text.slice(s.tokenStart, s.pos);
        s.token = ast.KindRegularExpressionLiteral;
    }
    return s.token;
}

public static ast.Kind ReScanJsxToken(this ж<Scanner> Ꮡs, bool allowMultilineJsxText) {
    ref var s = ref Ꮡs.DerefOrNull();

    s.pos = s.fullStartPos;
    s.tokenStart = s.fullStartPos;
    s.token = Ꮡs.ScanJsxTokenEx(allowMultilineJsxText);
    return s.token;
}

public static ast.Kind ReScanHashToken(this ref Scanner s) {
    if (s.token == ast.KindPrivateIdentifier) {
        s.pos = s.tokenStart + 1;
        s.token = ast.KindHashToken;
    }
    return s.token;
}

public static ast.Kind ReScanQuestionToken(this ref Scanner s) {
    if (s.token != ast.KindQuestionQuestionToken) {
        throw panic("'reScanQuestionToken' should only be called on a '??'");
    }
    s.pos = s.tokenStart + 1;
    s.token = ast.KindQuestionToken;
    return s.token;
}

public static ast.Kind ScanJsxToken(this ж<Scanner> Ꮡs) {
    return Ꮡs.ScanJsxTokenEx(true);
}

public static ast.Kind ScanJsxTokenEx(this ж<Scanner> Ꮡs, bool allowMultilineJsxText) {
    ref var s = ref Ꮡs.DerefOrNull();

    s.fullStartPos = s.pos;
    s.tokenStart = s.pos;
    var ch = s.@char();
    switch (ᐧ) {
    case {} when ch is < 0: {
        s.token = ast.KindEndOfFile;
        break;
    }
    case {} when ch is '<': {
        if (s.charAt(1) == '/') {
            s.pos += 2;
            s.token = ast.KindLessThanSlashToken;
        } else {
            s.pos++;
            s.token = ast.KindLessThanToken;
        }
        break;
    }
    case {} when ch is '{': {
        s.pos++;
        s.token = ast.KindOpenBraceToken;
        break;
    }
    default: {
        nint firstNonWhitespace = 0;
        for (;;) {
            var (chΔ2, size) = s.charAndSize();
            if (size == 0 || chΔ2 == '{') {
                break;
            }
            if (chΔ2 == '<') {
                if (isConflictMarkerTrivia(s.text, s.pos)) {
                    s.pos = scanConflictMarkerTrivia(s.text, s.pos, Ꮡs.errorAt);
                    s.token = ast.KindConflictMarkerTrivia;
                    return s.token;
                }
                break;
            }
            if (chΔ2 == '>') {
                s.errorAt(diagnostics.Unexpected_token_Did_you_mean_or_gt, s.pos, 1);
            } else if (chΔ2 == '}') {
                s.errorAt(diagnostics.Unexpected_token_Did_you_mean_or_rbrace, s.pos, 1);
            }
            if (stringutil.IsLineBreak(chΔ2) && firstNonWhitespace == 0) {
                firstNonWhitespace = -1;
            } else if (!allowMultilineJsxText && stringutil.IsLineBreak(chΔ2) && firstNonWhitespace > 0) {
                break;
            } else if (!stringutil.IsWhiteSpaceLike(chΔ2)) {
                firstNonWhitespace = s.pos;
            }
            s.pos += size;
        }
        s.tokenValue = s.text.slice(s.fullStartPos, s.pos);
        s.token = ast.KindJsxText;
        if (firstNonWhitespace == -1) {
            s.token = ast.KindJsxTextAllWhiteSpaces;
        }
        break;
    }}

    return s.token;
}

public static ast.Kind ScanJsxIdentifier(this ref Scanner s) {
    if (tokenIsIdentifierOrKeyword(s.token)) {
        s.tokenValue += s.scanIdentifierParts(identifierVariantJSX);
        s.token = GetIdentifierToken(s.tokenValue);
    }
    return s.token;
}

public static ast.Kind ScanJsxAttributeValue(this ж<Scanner> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    s.fullStartPos = s.pos;
    for (var (ch, size) = s.charAndSize(); size > 0 && stringutil.IsWhiteSpaceLike(ch); (ch, size) = s.charAndSize()) {
        s.pos += size;
    }
    s.tokenStart = s.pos;
    switch (s.@char()) {
    case '"' or '\'': {
        s.tokenValue = s.scanString(true);
        s.token = ast.KindStringLiteral;
        return s.token;
    }
    default: {
        return Ꮡs.Scan();
    }}

}

public static ast.Kind ReScanJsxAttributeValue(this ж<Scanner> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    s.pos = s.fullStartPos;
    s.tokenStart = s.fullStartPos;
    return Ꮡs.ScanJsxAttributeValue();
}

public static ast.Kind ScanJSDocCommentTextToken(this ref Scanner s, bool inBackticks) {
    s.fullStartPos = s.pos;
    s.tokenFlags = ast.TokenFlagsNone;
    if (s.pos >= len(s.text)) {
        s.token = ast.KindEndOfFile;
        return s.token;
    }
    s.tokenStart = s.pos;
    for (var (ch, size) = s.charAndSize(); s.pos < len(s.text) && !stringutil.IsLineBreak(ch) && ch != '`'; (ch, size) = s.charAndSize()) {
        if (!inBackticks) {
            if (ch == '{') {
                break;
            } else if (ch == '@' && s.pos >= 0) {
                var (previous, _) = utf8.DecodeLastRuneInString(s.text.slice(0, s.pos));
                if (stringutil.IsWhiteSpaceSingleLine(previous)) {
                    var (next, _) = utf8.DecodeRuneInString(s.text.slice(s.pos + size));
                    if (IsIdentifierStart(next)) {
                        break;
                    }
                }
            }
        }
        s.pos += size;
    }
    if (s.pos == s.tokenStart) {
        return s.ScanJSDocToken();
    }
    s.tokenValue = s.text.slice(s.tokenStart, s.pos);
    s.token = ast.KindJSDocCommentTextToken;
    return s.token;
}

public static bool CanFollowJSDocAt(this ref Scanner s) {
    if (s.pos >= len(s.text)) {
        return true;
    }
    var (ch, _) = utf8.DecodeRuneInString(s.text.slice(s.pos));
    return IsIdentifierStart(ch) || stringutil.IsWhiteSpaceSingleLine(ch) || stringutil.IsLineBreak(ch);
}

public static ast.Kind ScanJSDocToken(this ref Scanner s) {
    s.fullStartPos = s.pos;
    s.tokenFlags = ast.TokenFlagsNone;
    if (s.pos >= len(s.text)) {
        s.token = ast.KindEndOfFile;
        return s.token;
    }

    s.tokenStart = s.pos;
    var (ch, size) = s.charAndSize();
    s.pos += size;
    var matchᴛ1 = false;
    if (ch is '\t' or '\v' or '\f' or ' ') { matchᴛ1 = true;
        for (var (ch2, size2) = s.charAndSize(); size2 > 0 && stringutil.IsWhiteSpaceSingleLine(ch2); (ch2, size2) = s.charAndSize()) {
            s.pos += size2;
        }
        s.token = ast.KindWhitespaceTrivia;
        return s.token;
    }
    if (ch is '@') { matchᴛ1 = true;
        s.token = ast.KindAtToken;
        return s.token;
    }
    if (ch is '\r') { matchᴛ1 = true;
        if (s.@char() == '\n') {
            s.pos++;
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && ch is '\n') { matchᴛ1 = true;
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsPrecedingLineBreak);
        s.token = ast.KindNewLineTrivia;
        return s.token;
    }
    if (ch is '*') { matchᴛ1 = true;
        s.token = ast.KindAsteriskToken;
        return s.token;
    }
    if (ch is '{') { matchᴛ1 = true;
        s.token = ast.KindOpenBraceToken;
        return s.token;
    }
    if (ch is '}') { matchᴛ1 = true;
        s.token = ast.KindCloseBraceToken;
        return s.token;
    }
    if (ch is '[') { matchᴛ1 = true;
        s.token = ast.KindOpenBracketToken;
        return s.token;
    }
    if (ch is ']') { matchᴛ1 = true;
        s.token = ast.KindCloseBracketToken;
        return s.token;
    }
    if (ch is '(') { matchᴛ1 = true;
        s.token = ast.KindOpenParenToken;
        return s.token;
    }
    if (ch is ')') { matchᴛ1 = true;
        s.token = ast.KindCloseParenToken;
        return s.token;
    }
    if (ch is '<') { matchᴛ1 = true;
        s.token = ast.KindLessThanToken;
        return s.token;
    }
    if (ch is '>') { matchᴛ1 = true;
        s.token = ast.KindGreaterThanToken;
        return s.token;
    }
    if (ch is '=') { matchᴛ1 = true;
        s.token = ast.KindEqualsToken;
        return s.token;
    }
    if (ch is ',') { matchᴛ1 = true;
        s.token = ast.KindCommaToken;
        return s.token;
    }
    if (ch is '.') { matchᴛ1 = true;
        s.token = ast.KindDotToken;
        return s.token;
    }
    if (ch is '`') {
        s.token = ast.KindBacktickToken;
        return s.token;
    }
    if (ch is '#') { matchᴛ1 = true;
        s.token = ast.KindHashToken;
        return s.token;
    }

    s.pos = s.tokenStart;
    if (s.scanIdentifier(0, identifierVariantJSX)) {
        s.token = GetIdentifierToken(s.tokenValue);
        return s.token;
    }
    s.pos = s.tokenStart + size;
    s.token = ast.KindUnknown;
    return s.token;
}

internal static bool scanIdentifier(this ref Scanner s, nint prefixLength, identifierVariant variant) {
    nint start = s.pos;
    s.pos += prefixLength;
    nint identifierStart = s.pos;
    var ch = s.@char();
    if (variant != identifierVariantJSX && (stringutil.IsASCIILetter(ch) || ch == '_' || ch == '$')) {
        s.pos++;
        s.scanASCIIWhile((byte b) => (b >= 'a' && b <= 'z') || (b >= 'A' && b <= 'Z') || (b >= '0' && b <= '9') || b == '_' || b == '$');
        ch = s.@char();
        if (ch < utf8.RuneSelf && ch != '\\') {
            s.tokenValue = s.text.slice(start, s.pos);
            return true;
        }
        s.pos = identifierStart;
    }
    (ch, var size) = s.charAndSize();
    if (IsIdentifierStart(ch)) {
        var languageVariant = core.LanguageVariantStandard;
        if (variant == identifierVariantJSX) {
            languageVariant = core.LanguageVariantJSX;
        }
        for (;;) {
            s.pos += size;
            (ch, size) = s.charAndSize();
            if (!IsIdentifierPartEx(ch, languageVariant)) {
                break;
            }
        }
        s.tokenValue = s.text.slice(start, s.pos);
        if (ch == '\\') {
            s.tokenValue += s.scanIdentifierParts(variant);
        }
        return true;
    }
    if (ch == '\\') {
        var (escaped, ok) = s.scanIdentifierEscape(IsIdentifierStart, variant == identifierVariantRegExpGroupName); if (ok) {
            s.tokenValue = s.text.slice(start, identifierStart) + ((@string)escaped) + s.scanIdentifierParts(variant);
            return true;
        }
    }
    return false;
}

internal static @string scanIdentifierParts(this ref Scanner s, identifierVariant variant) {
    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    nint start = s.pos;
    var languageVariant = core.LanguageVariantStandard;
    if (variant == identifierVariantJSX) {
        languageVariant = core.LanguageVariantJSX;
    }
    for (;;) {
        var (ch, size) = s.charAndSize();
        if (IsIdentifierPartEx(ch, languageVariant)) {
            s.pos += size;
            continue;
        }
        if (ch == '\\') {
            nint escapeStart = s.pos;
            {
                var (escaped, ok) = s.scanIdentifierEscape((rune chΔ1) => IsIdentifierPartEx(chΔ1, languageVariant), variant == identifierVariantRegExpGroupName); if (ok) {
                    Ꮡsb.WriteString(s.text.slice(start, escapeStart));
                    Ꮡsb.WriteRune(escaped);
                    start = s.pos;
                    continue;
                }
            }
        }
        break;
    }
    Ꮡsb.WriteString(s.text.slice(start, s.pos));
    return sb.String();
}

internal static (rune, bool) scanIdentifierEscape(this ref Scanner s, Func<rune, bool> isValid, bool allowSurrogatePairEscape) {
    var escaped = s.peekUnicodeEscape();
    if (escaped >= 0 && isValid(escaped)) {
        return (s.scanUnicodeEscape(true), true);
    }
    if (allowSurrogatePairEscape && s.charAt(2) != '{' && stringutil.IsHighSurrogate(escaped)) {
        nint savedPos = s.pos;
        var savedTokenFlags = s.tokenFlags;
        s.scanUnicodeEscape(false);
        if (s.charAt(2) != '{') {
            var (codePoint, ok) = s.scanLowSurrogateEscape(escaped); if (ok && isValid(codePoint)) {
                return (codePoint, true);
            }
        }
        s.pos = savedPos;
        s.tokenFlags = savedTokenFlags;
    }
    return (0, false);
}

internal static @string scanString(this ref Scanner s, bool jsxAttributeString) {
    var quote = s.@char();
    if (quote == '\'') {
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsSingleQuote);
    }
    s.pos++;
    nint strLen = strings.IndexByte(s.text.slice(s.pos), (byte)quote);
    if (strLen == 0) {
        s.pos++;
        return "";
    }
    if (strLen > 0) {
        @string str = s.text.slice(s.pos, s.pos + strLen);
        if (jsxAttributeString || strings.IndexByte(str, (byte)'\\') < 0 && strings.IndexByte(str, (byte)'\r') < 0 && strings.IndexByte(str, (byte)'\n') < 0) {
            s.pos += strLen + 1;
            return str;
        }
    }
    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    nint start = s.pos;
    for (;;) {
        var ch = s.@char();
        if (ch < 0) {
            Ꮡsb.WriteString(s.text.slice(start, s.pos));
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsUnterminated);
            s.error(diagnostics.Unterminated_string_literal);
            break;
        }
        if (ch == quote) {
            Ꮡsb.WriteString(s.text.slice(start, s.pos));
            s.pos++;
            break;
        }
        if (ch == '\\' && !jsxAttributeString) {
            Ꮡsb.WriteString(s.text.slice(start, s.pos));
            Ꮡsb.WriteString(s.scanEscapeSequence((EscapeSequenceScanningFlags)(EscapeSequenceScanningFlagsString | EscapeSequenceScanningFlagsReportErrors)));
            start = s.pos;
            continue;
        }
        if ((ch == '\n' || ch == '\r') && !jsxAttributeString) {
            Ꮡsb.WriteString(s.text.slice(start, s.pos));
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsUnterminated);
            s.error(diagnostics.Unterminated_string_literal);
            break;
        }
        s.pos++;
    }
    return sb.String();
}

internal static ast.Kind scanTemplateAndSetTokenValue(this ref Scanner s, bool shouldEmitInvalidEscapeError) {
    var partsᴷ = new stackAppend<@string>();

    var startedWithBacktick = s.@char() == '`';
    s.pos++;
    nint start = s.pos;
    var parts = new slice<@string>(0, 4);
    ast.Kind token = default;
    for (;;) {
        s.scanASCIIWhile((byte b) => b != '`' && b != '$' && b != '\\' && b != '\r');
        var ch = s.@char();
        if (ch < 0 || ch == '`') {
            parts = append(partsᴷ, parts, s.text.slice(start, s.pos));
            if (ch == '`') {
                s.pos++;
            } else {
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsUnterminated);
                s.error(diagnostics.Unterminated_template_literal);
            }
            token = core.IfElse(startedWithBacktick, ast.KindNoSubstitutionTemplateLiteral, ast.KindTemplateTail);
            break;
        }
        if (ch == '$' && s.charAt(1) == '{') {
            parts = append(parts, s.text.slice(start, s.pos));
            s.pos += 2;
            token = core.IfElse(startedWithBacktick, ast.KindTemplateHead, ast.KindTemplateMiddle);
            break;
        }
        if (ch == '\\') {
            parts = append(parts, s.text.slice(start, s.pos));
            parts = append(parts, s.scanEscapeSequence((EscapeSequenceScanningFlags)(EscapeSequenceScanningFlagsString | core.IfElse(shouldEmitInvalidEscapeError, EscapeSequenceScanningFlagsReportErrors, (EscapeSequenceScanningFlags)0))));
            start = s.pos;
            continue;
        }
        if (ch == '\r') {
            parts = append(parts, s.text.slice(start, s.pos));
            s.pos++;
            if (s.@char() == '\n') {
                s.pos++;
            }
            parts = append(parts, (@string)"\n");
            start = s.pos;
            continue;
        }
        s.pos++;
    }
    s.tokenValue = strings.Join(parts, "");
    return token;
}

internal static @string scanEscapeSequence(this ref Scanner s, EscapeSequenceScanningFlags flags) {
    nint start = s.pos;
    s.pos++;
    var ch = s.@char();
    if (ch < 0) {
        s.error(diagnostics.Unexpected_end_of_text);
        return "";
    }
    s.pos++;
    var chᴛ1 = ch;
    var matchᴛ1 = false;
    if (chᴛ1 is '0') { matchᴛ1 = true;
        if (!stringutil.IsDigit(s.@char())) {
            return "\x00";
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && (chᴛ1 is '1' or '2' or '3')) { matchᴛ1 = true;
        if (stringutil.IsOctalDigit(s.@char())) {
            s.pos++;
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && (chᴛ1 is '4' or '5' or '6' or '7')) { matchᴛ1 = true;
        if (stringutil.IsOctalDigit(s.@char())) {
            s.pos++;
        }
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidEscape);
        if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsReportInvalidEscapeErrors) != 0) {
            var (code, _) = strconv.ParseInt(s.text.slice(start + 1, s.pos), 8, 32);
            if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsRegularExpression) != 0 && (EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsAtomEscape) == 0 && ch != '0') {
                s.errorAt(diagnostics.Octal_escape_sequences_and_backreferences_are_not_allowed_in_a_character_class_If_this_was_intended_as_an_escape_sequence_use_the_syntax_0_instead, start, s.pos - start, fmt.Sprintf("\\x%02x"u8, code));
            } else {
                s.errorAt(diagnostics.Octal_escape_sequences_are_not_allowed_Use_the_syntax_0, start, s.pos - start, fmt.Sprintf("\\x%02x"u8, code));
            }
            return (@string)(rune)code;
        }
        return s.text.slice(start, s.pos);
    }
    if (chᴛ1 is '8' or '9') { matchᴛ1 = true;
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidEscape);
        if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsReportInvalidEscapeErrors) != 0) {
            if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsRegularExpression) != 0 && (EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsAtomEscape) == 0) {
                s.errorAt(diagnostics.Decimal_escape_sequences_and_backreferences_are_not_allowed_in_a_character_class, start, s.pos - start);
            } else {
                s.errorAt(diagnostics.Escape_sequence_0_is_not_allowed, start, s.pos - start, s.text.slice(start, s.pos));
            }
            return (@string)ch;
        }
        return s.text.slice(start, s.pos);
    }
    if (chᴛ1 is 'b') { matchᴛ1 = true;
        return "\b";
    }
    if (chᴛ1 is 't') { matchᴛ1 = true;
        return "\t";
    }
    if (chᴛ1 is 'n') { matchᴛ1 = true;
        return "\n";
    }
    if (chᴛ1 is 'v') { matchᴛ1 = true;
        return "\v";
    }
    if (chᴛ1 is 'f') { matchᴛ1 = true;
        return "\f";
    }
    if (chᴛ1 is 'r') { matchᴛ1 = true;
        return "\r";
    }
    if (chᴛ1 is '\'') { matchᴛ1 = true;
        return "'";
    }
    if (chᴛ1 is '"') { matchᴛ1 = true;
        return "\"";
    }
    if (chᴛ1 is 'u') { matchᴛ1 = true;
        var extended = s.@char() == '{';
        s.pos -= 2;
        var codePoint = s.scanUnicodeEscape((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsReportInvalidEscapeErrors) != 0);
        if (extended) {
            if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsAllowExtendedUnicodeEscape) == 0) {
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidEscape);
                if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsReportInvalidEscapeErrors) != 0) {
                    s.errorAt(diagnostics.Unicode_escape_sequences_are_only_available_when_the_Unicode_u_flag_or_the_Unicode_Sets_v_flag_is_set, start, s.pos - start);
                }
            }
            if (codePoint < 0) {
                return s.text.slice(start, s.pos);
            }
            if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsRegularExpression) == 0 && stringutil.IsHighSurrogate(codePoint)) {
                var (combined, ok) = s.scanLowSurrogateEscape(codePoint); if (ok) {
                    return (@string)combined;
                }
            }
            return stringutil.EncodeJSStringRune(codePoint);
        }
        if (codePoint < 0) {
            return s.text.slice(start, s.pos);
        } else if (stringutil.IsHighSurrogate(codePoint)) {
            if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsRegularExpression) == 0) {
                var (combined, ok) = s.scanLowSurrogateEscape(codePoint); if (ok) {
                    return (@string)combined;
                }
            } else if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsAnyUnicodeMode) != 0 && s.@char() == '\\' && s.charAt(1) == 'u' && s.charAt(2) != '{') {
                nint savedPos = s.pos;
                var nextCodePoint = s.scanUnicodeEscape((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsReportInvalidEscapeErrors) != 0);
                if (stringutil.IsLowSurrogate(nextCodePoint)) {
                    return (@string)stringutil.SurrogatePairToCodePoint(codePoint, nextCodePoint);
                }
                s.pos = savedPos;
            }
        }
        return stringutil.EncodeJSStringRune(codePoint);
    }
    if (chᴛ1 is 'x') { matchᴛ1 = true;
        for (; s.pos < start + 4; s.pos++) {
            if (!stringutil.IsHexDigit(s.@char())) {
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidEscape);
                if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsReportInvalidEscapeErrors) != 0) {
                    s.error(diagnostics.Hexadecimal_digit_expected);
                }
                return s.text.slice(start, s.pos);
            }
        }
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsHexEscape);
        var (escapedValue, _) = strconv.ParseInt(s.text.slice(start + 2, s.pos), 16, 32);
        return (@string)(rune)escapedValue;
    }
    if (chᴛ1 is '\r') { matchᴛ1 = true;
        if (s.@char() == '\n') {
            s.pos++;
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && chᴛ1 is '\n') {
        return "";
    }
    if (!matchᴛ1) { /* default: */
        if (ch >= utf8.RuneSelf) {
            s.pos--;
            nint size = default;
            (ch, size) = utf8.DecodeRuneInString(s.text.slice(s.pos));
            s.pos += size;
        }
        if (ch == '\u2028' || ch == '\u2029') {
            return "";
        }
        if ((EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsAnyUnicodeMode) != 0 || (EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsRegularExpression) != 0 && (EscapeSequenceScanningFlags)(flags & EscapeSequenceScanningFlagsAnnexB) == 0 && IsIdentifierPart(ch)) {
            s.errorAt(diagnostics.This_character_cannot_be_escaped_in_a_regular_expression, start, s.pos - start);
        }
        return (@string)ch;
    }
    return default;

}

internal static rune scanUnicodeEscape(this ref Scanner s, bool shouldEmitInvalidEscapeError) {
    s.pos += 2;
    nint start = s.pos;
    var extended = s.@char() == '{';
    @string hexDigits = default;
    if (extended) {
        s.pos++;
        hexDigits = s.scanHexDigits(1, true, false);
    } else {
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsUnicodeEscape);
        hexDigits = s.scanHexDigits(4, false, false);
    }
    if (hexDigits == ""u8) {
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidEscape);
        if (shouldEmitInvalidEscapeError) {
            s.error(diagnostics.Hexadecimal_digit_expected);
        }
        return -1;
    }
    var (hexValue, _) = strconv.ParseInt(hexDigits, 16, 32);
    if (extended) {
        var isInvalidExtendedEscape = false;
        if (hexValue > 0x10FFFF) {
            if (shouldEmitInvalidEscapeError) {
                s.errorAt(diagnostics.An_extended_Unicode_escape_value_must_be_between_0x0_and_0x10FFFF_inclusive, start + 1, s.pos - start - 1);
            }
            isInvalidExtendedEscape = true;
        }
        if (s.pos >= s.end) {
            if (shouldEmitInvalidEscapeError) {
                s.error(diagnostics.Unexpected_end_of_text);
            }
            isInvalidExtendedEscape = true;
        } else if (s.@char() == '}') {
            s.pos++;
        } else {
            if (shouldEmitInvalidEscapeError) {
                s.error(diagnostics.Unterminated_Unicode_escape_sequence);
            }
            isInvalidExtendedEscape = true;
        }
        if (isInvalidExtendedEscape) {
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidEscape);
            return -1;
        }
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsExtendedUnicodeEscape);
    }
    return (rune)hexValue;
}

internal static (rune, bool) scanLowSurrogateEscape(this ref Scanner s, rune high) {
    if (s.@char() != '\\' || s.charAt(1) != 'u') {
        return (0, false);
    }
    nint savedPos = s.pos;
    var savedTokenFlags = s.tokenFlags;
    var low = s.scanUnicodeEscape(false);
    if (stringutil.IsLowSurrogate(low)) {
        return (stringutil.SurrogatePairToCodePoint(high, low), true);
    }
    s.pos = savedPos;
    s.tokenFlags = savedTokenFlags;
    return (0, false);
}

internal static rune peekUnicodeEscape(this ref Scanner s) {
    if (s.charAt(1) == 'u') {
        nint savePos = s.pos;
        var saveTokenFlags = s.tokenFlags;
        var codePoint = s.scanUnicodeEscape(false);
        s.pos = savePos;
        s.tokenFlags = saveTokenFlags;
        return codePoint;
    }
    return -1;
}

internal static ast.Kind scanNumber(this ref Scanner s) {
    nint start = s.pos;
    @string fixedPart = default;
    if (s.@char() == '0') {
        s.pos++;
        if (s.@char() == '_') {
            s.tokenFlags |= (ast.TokenFlags)((ast.TokenFlags)(ast.TokenFlagsContainsSeparator | ast.TokenFlagsContainsInvalidSeparator));
            s.errorAt(diagnostics.Numeric_separators_are_not_allowed_here, s.pos, 1);
            s.pos = start;
            fixedPart = s.scanNumberFragment();
        } else {
            var (digits, isOctal) = s.scanDigits();
            if (digits == ""u8) {
                fixedPart = "0";
            } else if (!isOctal) {
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsLeadingZero);
                fixedPart = digits;
            } else {
                var (val, _) = strconv.ParseInt(digits, 8, 64);
                s.tokenValue = strconv.FormatInt(val, 10);
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsOctal);
                var withMinus = s.token == ast.KindMinusToken;
                @string literal = core.IfElse(withMinus, (@string)"-", (@string)"") + "0o"u8 + strconv.FormatInt(val, 8);
                if (withMinus) {
                    start--;
                }
                s.errorAt(diagnostics.Octal_literals_are_not_allowed_Use_the_syntax_0, start, s.pos - start, literal);
                return ast.KindNumericLiteral;
            }
        }
    } else {
        fixedPart = s.scanNumberFragment();
    }
    nint fixedPartEnd = s.pos;
    @string fractionalPart = "";
    @string exponentPreamble = "";
    @string exponentPart = "";
    if (s.@char() == '.') {
        s.pos++;
        fractionalPart = s.scanNumberFragment();
    }
    nint end = s.pos;
    if (s.@char() == 'E' || s.@char() == 'e') {
        s.pos++;
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsScientific);
        if (s.@char() == '+' || s.@char() == '-') {
            s.pos++;
        }
        nint startNumericPart = s.pos;
        exponentPart = s.scanNumberFragment();
        if (exponentPart == ""u8) {
            s.error(diagnostics.Digit_expected);
        } else {
            exponentPreamble = s.text.slice(end, startNumericPart);
            end = s.pos;
        }
    }
    if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsContainsSeparator) != 0) {
        s.tokenValue = fixedPart;
        if (fractionalPart != ""u8) {
            s.tokenValue += "."u8 + fractionalPart;
        }
        if (exponentPart != ""u8) {
            s.tokenValue += exponentPreamble + exponentPart;
        }
    } else {
        s.tokenValue = s.text.slice(start, end);
    }
    if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsContainsLeadingZero) != 0) {
        s.errorAt(diagnostics.Decimals_with_leading_zeros_are_not_allowed, start, s.pos - start);
        s.tokenValue = jsnum.FromString(s.tokenValue).String();
        return ast.KindNumericLiteral;
    }
    ast.Kind result = default;
    if (fixedPartEnd == s.pos) {
        result = s.scanBigIntSuffix();
    } else {
        s.tokenValue = jsnum.FromString(s.tokenValue).String();
        result = ast.KindNumericLiteral;
    }
    var (ch, _) = s.charAndSize();
    if (IsIdentifierStart(ch)) {
        nint idStart = s.pos;
        @string id = s.scanIdentifierParts(identifierVariantStandard);
        if (result != ast.KindBigIntLiteral && len(id) == 1 && s.text[idStart] == 'n') {
            if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsScientific) != 0) {
                s.errorAt(diagnostics.A_bigint_literal_cannot_use_exponential_notation, start, s.pos - start);
                return result;
            }
            if (fixedPartEnd < idStart) {
                s.errorAt(diagnostics.A_bigint_literal_must_be_an_integer, start, s.pos - start);
                return result;
            }
        }
        s.errorAt(diagnostics.An_identifier_or_keyword_cannot_immediately_follow_a_numeric_literal, idStart, s.pos - idStart);
        s.pos = idStart;
    }
    return result;
}

internal static @string scanNumberFragment(this ref Scanner s) {
    nint start = s.pos;
    var allowSeparator = false;
    var isPreviousTokenSeparator = false;
    ref var result = ref heap(new strings.Builder(), out var Ꮡresult);
    for (;;) {
        nint before = s.pos;
        s.scanASCIIWhile((byte b) => b >= '0' && b <= '9');
        if (s.pos > before) {
            allowSeparator = true;
            isPreviousTokenSeparator = false;
        }
        var ch = s.@char();
        if (ch == '_') {
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsSeparator);
            if (allowSeparator) {
                allowSeparator = false;
                isPreviousTokenSeparator = true;
                Ꮡresult.WriteString(s.text.slice(start, s.pos));
            } else {
                s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidSeparator);
                if (isPreviousTokenSeparator) {
                    s.errorAt(diagnostics.Multiple_consecutive_numeric_separators_are_not_permitted, s.pos, 1);
                } else {
                    s.errorAt(diagnostics.Numeric_separators_are_not_allowed_here, s.pos, 1);
                }
            }
            s.pos++;
            start = s.pos;
            continue;
        }
        break;
    }
    if (isPreviousTokenSeparator) {
        s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsInvalidSeparator);
        s.errorAt(diagnostics.Numeric_separators_are_not_allowed_here, s.pos - 1, 1);
    }
    if (result.Len() == 0) {
        return s.text.slice(start, s.pos);
    }
    Ꮡresult.WriteString(s.text.slice(start, s.pos));
    return result.String();
}

internal static (@string, bool) scanDigits(this ref Scanner s) {
    nint start = s.pos;
    var isOctal = true;
    while (stringutil.IsDigit(s.@char())) {
        if (!stringutil.IsOctalDigit(s.@char())) {
            isOctal = false;
        }
        s.pos++;
    }
    return (s.text.slice(start, s.pos), isOctal);
}

internal static @string scanHexDigits(this ref Scanner s, nint minCount, bool scanAsManyAsPossible, bool canHaveSeparators) {
    nint digitCount = 0;
    nint start = s.pos;
    var allowSeparator = false;
    var isPreviousTokenSeparator = false;
    while (digitCount < minCount || scanAsManyAsPossible) {
        var ch = s.@char();
        if (stringutil.IsHexDigit(ch)) {
            allowSeparator = canHaveSeparators;
            isPreviousTokenSeparator = false;
            digitCount++;
        } else if (canHaveSeparators && ch == '_') {
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsSeparator);
            if (allowSeparator) {
                allowSeparator = false;
                isPreviousTokenSeparator = true;
            } else if (isPreviousTokenSeparator) {
                s.errorAt(diagnostics.Multiple_consecutive_numeric_separators_are_not_permitted, s.pos, 1);
            } else {
                s.errorAt(diagnostics.Numeric_separators_are_not_allowed_here, s.pos, 1);
            }
        } else {
            break;
        }
        s.pos++;
    }
    if (isPreviousTokenSeparator) {
        s.errorAt(diagnostics.Numeric_separators_are_not_allowed_here, s.pos - 1, 1);
    }
    if (digitCount < minCount) {
        return "";
    }
    @string digits = s.text.slice(start, s.pos);
    if (s.hexDigitCache == nil) {
        s.hexDigitCache = new map<@string, @string>();
    }
    var (cached, ok) = s.hexDigitCache[digits, ꟷ]; if (ok) {
        return cached;
    } else {
        @string original = digits;
        if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsContainsSeparator) != 0) {
            digits = strings.ReplaceAll(digits, "_", "");
        }
        digits = strings.ToLower(digits);
        s.hexDigitCache[original] = digits;
        return digits;
    }
}

internal static @string scanBinaryOrOctalDigits(this ref Scanner s, int32 @base) {
    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    var allowSeparator = false;
    var isPreviousTokenSeparator = false;
    for (;;) {
        var ch = s.@char();
        if (stringutil.IsDigit(ch) && ch - '0' < @base) {
            Ꮡsb.WriteByte((byte)ch);
            allowSeparator = true;
            isPreviousTokenSeparator = false;
        } else if (ch == '_') {
            s.tokenFlags |= (ast.TokenFlags)(ast.TokenFlagsContainsSeparator);
            if (allowSeparator) {
                allowSeparator = false;
                isPreviousTokenSeparator = true;
            } else if (isPreviousTokenSeparator) {
                s.errorAt(diagnostics.Multiple_consecutive_numeric_separators_are_not_permitted, s.pos, 1);
            } else {
                s.errorAt(diagnostics.Numeric_separators_are_not_allowed_here, s.pos, 1);
            }
        } else {
            break;
        }
        s.pos++;
    }
    if (isPreviousTokenSeparator) {
        s.errorAt(diagnostics.Numeric_separators_are_not_allowed_here, s.pos - 1, 1);
    }
    return sb.String();
}

internal static ast.Kind scanBigIntSuffix(this ref Scanner s) {
    if (s.@char() == 'n') {
        s.tokenValue += "n"u8;
        if ((ast.TokenFlags)(s.tokenFlags & ast.TokenFlagsBinaryOrOctalSpecifier) != 0) {
            s.tokenValue = jsnum.ParsePseudoBigInt(s.tokenValue) + "n"u8;
        }
        s.pos++;
        return ast.KindBigIntLiteral;
    }
    if (s.numberCache == nil) {
        s.numberCache = new map<@string, @string>();
    }
    var (cached, ok) = s.numberCache[s.tokenValue, ꟷ]; if (ok) {
        s.tokenValue = cached;
    } else {
        @string tokenValue = jsnum.FromString(s.tokenValue).String();
        if (tokenValue == s.tokenValue) {
            tokenValue = s.tokenValue;
        }
        s.numberCache[s.tokenValue] = tokenValue;
        s.tokenValue = tokenValue;
    }
    return ast.KindNumericLiteral;
}

internal static void scanInvalidCharacter(this ref Scanner s) {
    var (_, size) = s.charAndSize();
    s.errorAt(diagnostics.Invalid_character, s.pos, size);
    s.pos += size;
    s.token = ast.KindUnknown;
}

} partial class scanner_package {

public static ast.Kind GetIdentifierToken(@string str) {
    if (len(str) >= 2 && len(str) <= 12 && str[0] >= 'a' && str[0] <= 'z') {
        var keyword = textToKeyword[str];
        if (keyword != ast.KindUnknown) {
            return keyword;
        }
    }
    return ast.KindIdentifier;
}

public static bool IsValidIdentifier(@string s) {
    if (len(s) == 0) {
        return false;
    }
    foreach (var (i, ch) in s) {
        if (i == 0 && !IsIdentifierStart(ch) || i != 0 && !IsIdentifierPart(ch)) {
            return false;
        }
    }
    return true;
}

internal static bool isWordCharacter(rune ch) {
    return stringutil.IsASCIILetter(ch) || stringutil.IsDigit(ch) || ch == '_';
}

public static bool IsIdentifierStart(rune ch) {
    return stringutil.IsASCIILetter(ch) || ch == '_' || ch == '$' || ch >= utf8.RuneSelf && stringutil.IsUnicodeIdentifierStart(ch);
}

public static bool IsIdentifierPart(rune ch) {
    return IsIdentifierPartEx(ch, core.LanguageVariantStandard);
}

public static bool IsIdentifierPartEx(rune ch, core.LanguageVariant languageVariant) {
    return isWordCharacter(ch) || ch == '$' || ch >= utf8.RuneSelf && stringutil.IsUnicodeIdentifierPart(ch) || languageVariant == core.LanguageVariantJSX && ch == '-';
}

internal static array<@string> tokenToText = func(array<@string> () => {
    array<@string> result = new(351); /* ast.KindCount */
    foreach (var (text, kind) in textToToken) {
        result[kind] = text;
    }
    return result.Clone();
})();

public static @string TokenToString(ast.Kind token) {
    return tokenToText[token];
}

public static ast.Kind StringToToken(@string s) {
    var (kind, ok) = textToToken[s, ꟷ];
    if (ok) {
        return kind;
    }
    return ast.KindUnknown;
}

public static slice<@string> GetViableKeywordSuggestions() {
    var result = new slice<@string>(0, len(textToKeyword));
    foreach (var (text, _) in textToKeyword) {
        if (len(text) > 2) {
            result = append(result, text);
        }
    }
    return result;
}

internal static bool couldStartTrivia(@string text, nint pos) {
    var ch = text[pos];
    switch (ch) {
    case (byte)'\r' or (byte)'\n' or (byte)'\t' or (byte)'\v' or (byte)'\f' or (byte)' ' or (byte)'/' or (byte)'<' or (byte)'|' or (byte)'=' or (byte)'>': {
        return true;
    }
    case (byte)'#': {
        return pos == 0;
    }
    default: {
        return ch > maxAsciiCharacter;
    }}

}

partial struct SkipTriviaOptions {
    public bool StopAfterLineBreak;
    public bool StopAtComments;
    public bool InJSDoc;
}

public static nint SkipTrivia(@string text, nint pos) {
    return SkipTriviaEx(text, pos, nil);
}

public static nint SkipTriviaEx(@string text, nint pos, ж<SkipTriviaOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (ast.PositionIsSynthesized(pos)) {
        return pos;
    }
    if (Ꮡoptions == nil) {
        Ꮡoptions = Ꮡ(new SkipTriviaOptions()); options = ref Ꮡoptions.DerefOrNull();
    }

    nint textLen = len(text);
    var canConsumeStar = false;
    for (;;) {
        if (pos >= textLen) {
            return pos;
        }
        var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
        var matchᴛ1 = false;
        if (ch is '\r') { matchᴛ1 = true;
            if (pos + 1 < textLen && text[pos + 1] == '\n') {
                pos++;
            }
            fallthrough = true;
        }
        if (fallthrough || !matchᴛ1 && ch is '\n') { matchᴛ1 = true;
            pos++;
            if (options.StopAfterLineBreak) {
                return pos;
            }
            canConsumeStar = options.InJSDoc;
            continue;
        }
        else if (ch is '\t' or '\v' or '\f' or ' ') { matchᴛ1 = true;
            pos++;
            continue;
        }
        else if (ch is '/') { matchᴛ1 = true;
            do {
                if (options.StopAtComments) {
                    break;
                }
                if (pos + 1 < textLen) {
                    if (text[pos + 1] == '/') {
                        pos += 2;
                        while (pos < textLen) {
                            var (chΔ2, sizeΔ3) = utf8.DecodeRuneInString(text.slice(pos));
                            if (stringutil.IsLineBreak(chΔ2)) {
                                break;
                            }
                            pos += sizeΔ3;
                        }
                        canConsumeStar = false;
                        goto continueᴛ1;
                    }
                    if (text[pos + 1] == '*') {
                        pos += 2;
                        while (pos < textLen) {
                            if (text[pos] == '*' && (pos + 1 < textLen) && text[pos + 1] == '/') {
                                pos += 2;
                                break;
                            }
                            var (_, sizeΔ4) = utf8.DecodeRuneInString(text.slice(pos));
                            pos += sizeΔ4;
                        }
                        canConsumeStar = false;
                        goto continueᴛ1;
                    }
                }
            } while (false);
        }
        else if (ch is '<' or '|' or '=' or '>') { matchᴛ1 = true;
            if (isConflictMarkerTrivia(text, pos)) {
                pos = scanConflictMarkerTrivia(text, pos, default);
                canConsumeStar = false;
                continue;
            }
        }
        else if (ch is '#') { matchᴛ1 = true;
            if (pos == 0 && isShebangTrivia(text, pos)) {
                pos = scanShebangTrivia(text, pos);
                canConsumeStar = false;
                continue;
            }
        }
        else if (ch is '*') {
            if (canConsumeStar) {
                pos++;
                canConsumeStar = false;
                continue;
            }
        }
        else if (!matchᴛ1) { /* default: */
            if (ch > (rune)maxAsciiCharacter && stringutil.IsWhiteSpaceLike(ch)) {
                pos += size;
                continue;
            }
        }

        return pos;
continueᴛ1:;
    }
}

internal static nint mergeConflictMarkerLength = len("<<<<<<<");
internal static byte maxAsciiCharacter = 127;

internal static bool isConflictMarkerTrivia(@string text, nint pos) {
    if (pos < 0) {
        throw panic("pos < 0");
    }

    if (pos + 1 >= len(text) || text[pos + 1] != text[pos]) {
        return false;
    }

    var atLineStart = pos == 0 || stringutil.IsLineBreak((rune)text[pos - 1]);
    if (!atLineStart && pos >= 2) {
        var (prev, _) = utf8.DecodeLastRuneInString(text.slice(0, pos - 2));
        atLineStart = stringutil.IsLineBreak(prev);
    }
    if (atLineStart) {
        var ch = text[pos];

        if ((pos + mergeConflictMarkerLength) < len(text)) {
            foreach (var i in range(mergeConflictMarkerLength)) {
                if (text[pos + i] != ch) {
                    return false;
                }
            }

            return ch == '=' || text[pos + mergeConflictMarkerLength] == ' ';
        }
    }

    return false;
}

internal static nint scanConflictMarkerTrivia(@string text, nint pos, Actionꓸꓸꓸ<ж<diagnostics.Message>, nint, nint, any> reportError) {
    if (reportError != nil) {
        reportError(diagnostics.Merge_conflict_marker_encountered, pos, mergeConflictMarkerLength);
    }
    var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
    nint length = len(text);

    if (ch == '<' || ch == '>') {
        while (pos < length && !stringutil.IsLineBreak(ch)) {
            pos += size;
            (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
        }
    } else {
        if (ch != '|' && ch != '=') {
            throw panic("Assertion failed: ch must be either '|' or '='");
        }
        while (pos < length) {
            var currentChar = text[pos];
            if ((currentChar == '=' || currentChar == '>') && (rune)currentChar != ch && isConflictMarkerTrivia(text, pos)) {
                break;
            }

            pos++;
        }
    }

    return pos;
}

internal static bool isShebangTrivia(@string text, nint pos) {
    if (len(text) < 2) {
        return false;
    }
    if (pos != 0) {
        throw panic("Shebangs check must only be done at the start of the file");
    }
    return text[0] == '#' && text[1] == '!';
}

internal static nint scanShebangTrivia(@string text, nint pos) {
    pos += 2;
    while (pos < len(text)) {
        var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
        if (stringutil.IsLineBreak(ch)) {
            break;
        }
        pos += size;
    }
    return pos;
}

public static @string GetShebang(@string text) {
    if (!isShebangTrivia(text, 0)) {
        return "";
    }

    nint end = scanShebangTrivia(text, 0);
    return text.slice(0, end);
}

public static ж<Scanner> GetScannerForSourceFile(ж<ast.SourceFile> ᏑsourceFile, nint pos) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    var s = NewScanner();
    s.text = sourceFile.Text();
    s.pos = pos;
    s.end = len(s.text);
    s.languageVariant = sourceFile.LanguageVariant;
    s.Scan();
    return s;
}

public static ast.Kind ScanTokenAtPosition(ж<ast.SourceFile> sourceFile, nint pos) {
    var s = GetScannerForSourceFile(sourceFile, pos);
    return s.token;
}

public static core.TextRange GetRangeOfTokenAtPosition(ж<ast.SourceFile> sourceFile, nint pos) {
    var s = GetScannerForSourceFile(sourceFile, pos);
    return core.NewTextRange(s.tokenStart, s.pos);
}

public static nint GetTokenPosOfNode(ж<ast.Node> Ꮡnode, ж<ast.SourceFile> ᏑsourceFile, bool includeJSDoc) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (ast.NodeIsMissing(Ꮡnode)) {
        return node.Pos();
    }
    if (ast.IsJSDocNode(Ꮡnode) || node.Kind == ast.KindJsxText) {
        return SkipTriviaEx(sourceFile.Text(), node.Pos(), Ꮡ(new SkipTriviaOptions(StopAtComments: true)));
    }
    if (includeJSDoc && len(Ꮡnode.JSDoc(ᏑsourceFile)) > 0) {
        return GetTokenPosOfNode(Ꮡnode.JSDoc(ᏑsourceFile)[0], ᏑsourceFile, false);
    }
    return SkipTriviaEx(sourceFile.Text(), node.Pos(), Ꮡ(new SkipTriviaOptions(InJSDoc: (ast.NodeFlags)(node.Flags & ast.NodeFlagsJSDoc) != 0)));
}

internal static core.TextRange getErrorRangeForArrowFunction(ж<ast.SourceFile> ᏑsourceFile, ж<ast.Node> Ꮡnode) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    nint pos = SkipTrivia(sourceFile.Text(), node.Pos());
    var body = node.Body();
    if (body != nil && body.Kind == ast.KindBlock) {
        nint startLine = GetECMALineOfPosition(new ast.SourceFileжSourceFileLike(ᏑsourceFile), body.Pos());
        nint endLine = GetECMALineOfPosition(new ast.SourceFileжSourceFileLike(ᏑsourceFile), body.End());
        if (startLine < endLine) {
            return core.NewTextRange(pos, GetECMAEndLinePosition(ᏑsourceFile, startLine) + 1);
        }
    }
    return core.NewTextRange(pos, node.End());
}

internal static ж<ast.Node> findOriginatingJSDocSatisfiesTag(ж<ast.SourceFile> sourceFile, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var targetType = node.AsSatisfiesExpression().Value.Type;
    if ((ast.NodeFlags)(targetType.Flags & ast.NodeFlagsReparsed) == 0) {
        return default;
    }
    for (var current = node.Parent; current != nil; current = current.Parent) {
        if ((ast.NodeFlags)(current.Flags & ast.NodeFlagsHasJSDoc) == 0) {
            continue;
        }
        ж<ast.Node> firstSatisfiesTag = default;
        foreach (var (_, jsDoc) in range(current.EagerJSDoc(sourceFile))) {
            var tags = jsDoc.AsJSDoc().Tags; if (tags != nil) {
                foreach (var (_, tag) in range(tags.Nodes)) {
                    if (!ast.IsJSDocSatisfiesTag(tag)) {
                        continue;
                    }
                    if (firstSatisfiesTag == nil) {
                        firstSatisfiesTag = tag;
                    }
                    var typeExpr = tag.AsJSDocSatisfiesTag().Value.TypeExpression; if (typeExpr != nil) {
                        var t = typeExpr.Type(); if (t != nil && t.Loc == targetType.Loc) {
                            return tag;
                        }
                    }
                }
            }
        }
        return firstSatisfiesTag;
    }
    return default;
}

public static core.TextRange GetErrorRangeForNode(ж<ast.SourceFile> ᏑsourceFile, ж<ast.Node> Ꮡnode) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var errorNode = Ꮡnode;
    var exprᴛ1 = node.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindSourceFile) { matchᴛ1 = true;
        nint posΔ3 = SkipTrivia(sourceFile.Text(), 0);
        if (posΔ3 == len(sourceFile.Text())) {
            return core.NewTextRange(0, 0);
        }
        return GetRangeOfTokenAtPosition(ᏑsourceFile, posΔ3);
    }
    if (exprᴛ1 == ast.KindFunctionDeclaration || exprᴛ1 == ast.KindMethodDeclaration) { matchᴛ1 = true;
        do {
            if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
                errorNode = Ꮡnode;
                break;
            }
            fallthrough = true;
        } while (false);
    }
    if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindVariableDeclaration || exprᴛ1 == ast.KindBindingElement || exprᴛ1 == ast.KindClassDeclaration || exprᴛ1 == ast.KindInterfaceDeclaration || exprᴛ1 == ast.KindModuleDeclaration || exprᴛ1 == ast.KindEnumDeclaration || exprᴛ1 == ast.KindEnumMember || exprᴛ1 == ast.KindFunctionExpression || exprᴛ1 == ast.KindGetAccessor || exprᴛ1 == ast.KindSetAccessor || exprᴛ1 == ast.KindTypeAliasDeclaration || exprᴛ1 == ast.KindJSTypeAliasDeclaration || exprᴛ1 == ast.KindPropertyDeclaration || exprᴛ1 == ast.KindPropertySignature || exprᴛ1 == ast.KindNamespaceImport)) { matchᴛ1 = true;
        errorNode = ast.GetNameOfDeclaration(Ꮡnode);
    }
    else if (exprᴛ1 == ast.KindClassExpression) { matchᴛ1 = true;
        errorNode = node.Name();
    }
    else if (exprᴛ1 == ast.KindArrowFunction) { matchᴛ1 = true;
        return getErrorRangeForArrowFunction(ᏑsourceFile, Ꮡnode);
    }
    else if (exprᴛ1 == ast.KindCaseClause || exprᴛ1 == ast.KindDefaultClause) { matchᴛ1 = true;
        nint start = SkipTrivia(sourceFile.Text(), node.Pos());
        nint end = node.End();
        var statements = node.Statements();
        if (len(statements) != 0) {
            end = statements[0].Pos();
        }
        return core.NewTextRange(start, end);
    }
    else if (exprᴛ1 == ast.KindReturnStatement || exprᴛ1 == ast.KindYieldExpression) { matchᴛ1 = true;
        nint posΔ4 = SkipTrivia(sourceFile.Text(), node.Pos());
        return GetRangeOfTokenAtPosition(ᏑsourceFile, posΔ4);
    }
    else if (exprᴛ1 == ast.KindSatisfiesExpression) {
        var jsDocSatisfiesTag = findOriginatingJSDocSatisfiesTag(ᏑsourceFile, Ꮡnode); if (jsDocSatisfiesTag != nil) {
            nint posΔ5 = SkipTrivia(sourceFile.Text(), jsDocSatisfiesTag.TagName().Pos());
            return GetRangeOfTokenAtPosition(ᏑsourceFile, posΔ5);
        }
        nint posΔ6 = SkipTrivia(sourceFile.Text(), (~node.AsSatisfiesExpression()).Expression.End());
        return GetRangeOfTokenAtPosition(ᏑsourceFile, posΔ6);
    }
    else if (exprᴛ1 == ast.KindConstructor) { matchᴛ1 = true;
        do {
            if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
                errorNode = Ꮡnode;
                break;
            }
            var scanner = GetScannerForSourceFile(ᏑsourceFile, node.Pos());
            nint start = scanner.TokenStart();
            while (scanner.Token() != ast.KindConstructorKeyword && scanner.Token() != ast.KindStringLiteral && scanner.Token() != ast.KindEndOfFile) {
                scanner.Scan();
            }
            return core.NewTextRange(start, scanner.TokenEnd());
        } while (false);
    }

    if (errorNode == nil) {
        return GetRangeOfTokenAtPosition(ᏑsourceFile, node.Pos());
    }
    nint pos = errorNode.Pos();
    if (!ast.NodeIsMissing(errorNode) && !ast.IsJsxText(errorNode)) {
        pos = SkipTrivia(sourceFile.Text(), pos);
    }
    return core.NewTextRange(pos, errorNode.End());
}

public static nint ComputeLineOfPosition(slice<core.TextPos> lineStarts, nint pos) {
    nint low = 0;
    nint high = len(lineStarts) - 1;
    while (low <= high) {
        nint middle = low + ((high - low) >> 1);
        nint value = (nint)lineStarts[middle];
        if (value < pos) {
            low = middle + 1;
        } else if (value > pos) {
            high = middle - 1;
        } else {
            return middle;
        }
    }
    return low - 1;
}

public static slice<core.TextPos> GetECMALineStarts(ast.SourceFileLike sourceFile) {
    return sourceFile.ECMALineMap();
}

public static nint GetECMALineOfPosition(ast.SourceFileLike sourceFile, nint pos) {
    var lineMap = GetECMALineStarts(sourceFile);
    return ComputeLineOfPosition(lineMap, pos);
}

public static (nint line, core.UTF16Offset character) GetECMALineAndUTF16CharacterOfPosition(ast.SourceFileLike sourceFile, nint pos) {
    nint line = default;
    core.UTF16Offset character = default;

    var lineMap = GetECMALineStarts(sourceFile);
    line = ComputeLineOfPosition(lineMap, pos);
    character = core.UTF16Len(sourceFile.Text().slice(lineMap[line], pos));
    return (line, character);
}

public static (nint line, nint byteOffset) GetECMALineAndByteOffsetOfPosition(ast.SourceFileLike sourceFile, nint pos) {
    nint line = default;
    nint byteOffset = default;

    var lineMap = GetECMALineStarts(sourceFile);
    line = ComputeLineOfPosition(lineMap, pos);
    byteOffset = pos - (nint)lineMap[line];
    return (line, byteOffset);
}

public static nint GetECMAEndLinePosition(ж<ast.SourceFile> ᏑsourceFile, nint line) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    nint pos = (nint)GetECMALineStarts(new ast.SourceFileжSourceFileLike(ᏑsourceFile))[line];
    for (;;) {
        var (ch, size) = utf8.DecodeRuneInString(sourceFile.Text().slice(pos));
        if (size == 0 || stringutil.IsLineBreak(ch)) {
            return pos - 1;
        }
        pos += size;
    }
}

public static nint GetECMAPositionOfLineAndUTF16Character(ast.SourceFileLike sourceFile, nint line, core.UTF16Offset character) {
    var lineStarts = GetECMALineStarts(sourceFile);
    return ComputePositionOfLineAndUTF16Character(lineStarts, line, character, sourceFile.Text(), false);
}

public static nint GetECMAPositionOfLineAndByteOffset(ast.SourceFileLike sourceFile, nint line, nint byteOffset) {
    return ComputePositionOfLineAndByteOffset(GetECMALineStarts(sourceFile), line, byteOffset);
}

public static nint ComputePositionOfLineAndByteOffset(slice<core.TextPos> lineStarts, nint line, nint byteOffset) {
    if (line < 0 || line >= len(lineStarts)) {
        throw panic(fmt.Sprintf("Bad line number. Line: %d, lineStarts.length: %d."u8, line, len(lineStarts)));
    }
    return (nint)lineStarts[line] + byteOffset;
}

public static nint ComputePositionOfLineAndUTF16Character(slice<core.TextPos> lineStarts, nint line, core.UTF16Offset character, @string text, bool allowEdits) {
    if (line < 0 || line >= len(lineStarts)) {
        if (allowEdits) {
            if (line < 0) {
                line = 0;
            } else if (line >= len(lineStarts)) {
                line = len(lineStarts) - 1;
            }
        } else {
            throw panic(fmt.Sprintf("Bad line number. Line: %d, lineStarts.length: %d."u8, line, len(lineStarts)));
        }
    }

    nint lineStart = (nint)lineStarts[line];

    if (character > 0) {
        nint lineEnd = len(text);
        if (line + 1 < len(lineStarts)) {
            lineEnd = (nint)lineStarts[line + 1];
        }
        core.UTF16Offset utf16Count = (core.UTF16Offset)0;
        nint pos = lineStart;
        while (pos < lineEnd) {
            if (utf16Count >= character) {
                break;
            }
            var (r, size) = utf8.DecodeRuneInString(text.slice(pos));
            utf16Count += ((core.UTF16Offset)utf16.RuneLen(r));
            pos += size;
        }
        if (!allowEdits) {
            if (pos == lineEnd && utf16Count < character) {
                throw panic(fmt.Sprintf("Bad UTF-16 character offset. Line: %d, character: %d."u8, line, character));
            }
            debug.Assert(pos <= len(text));
            return pos;
        }
        if (pos > len(text)) {
            return len(text);
        }
        return pos;
    }

    nint res = lineStart;

    if (allowEdits) {
        if (res > len(text)) {
            return len(text);
        }
        return res;
    }
    debug.Assert(res <= len(text));
    return res;
}

public static iter.Seq<ast.CommentRange> GetLeadingCommentRanges(ж<ast.NodeFactory> f, @string text, nint pos) {
    return iterateCommentRanges(f, text, pos, false);
}

public static iter.Seq<ast.CommentRange> GetTrailingCommentRanges(ж<ast.NodeFactory> f, @string text, nint pos) {
    return iterateCommentRanges(f, text, pos, true);
}

internal static iter.Seq<ast.CommentRange> iterateCommentRanges(ж<ast.NodeFactory> f, @string text, nint pos, bool trailing) {
    return (Func<ast.CommentRange, bool> yield) => {
        nint pendingPos = default;
        nint pendingEnd = default;
        ast.Kind pendingKind = default;
        bool pendingHasTrailingNewLine = default;
        var hasPendingCommentRange = false;
        var collecting = trailing;
        if (pos == 0) {
            collecting = true;
            if (isShebangTrivia(text, pos)) {
                pos = scanShebangTrivia(text, pos);
            }
        }
scan:
        while (pos >= 0 && pos < len(text)) {
            var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
            var matchᴛ1 = false;
            if (ch is '\r') { matchᴛ1 = true;
                if (pos + 1 < len(text) && text[pos + 1] == '\n') {
                    pos++;
                }
                fallthrough = true;
            }
            if (fallthrough || !matchᴛ1 && ch is '\n') { matchᴛ1 = true;
                pos++;
                if (trailing) {
                    goto break_scan;
                }

                collecting = true;
                if (hasPendingCommentRange) {
                    pendingHasTrailingNewLine = true;
                }

                continue;
            }
            else if (ch is '\t' or '\v' or '\f' or ' ') { matchᴛ1 = true;
                pos++;
                continue;
            }
            else if (ch is '/') {
                byte nextChar = default;
                if (pos + 1 < len(text)) {
                    nextChar = text[pos + 1];
                }
                var hasTrailingNewLine = false;
                if (nextChar == '/' || nextChar == '*') {
                    ast.Kind kind = default;
                    if (nextChar == '/') {
                        kind = ast.KindSingleLineCommentTrivia;
                    } else {
                        kind = ast.KindMultiLineCommentTrivia;
                    }

                    nint startPos = pos;
                    pos += 2;
                    if (nextChar == '/') {
                        while (pos < len(text)) {
                            var (c, s) = utf8.DecodeRuneInString(text.slice(pos));
                            if (stringutil.IsLineBreak(c)) {
                                hasTrailingNewLine = true;
                                break;
                            }
                            pos += s;
                        }
                    } else {
                        nint i = strings.Index(text.slice(pos), "*/"); if (i >= 0) {
                            pos += i + 2;
                        } else {
                            pos = len(text);
                        }
                    }

                    if (collecting) {
                        if (hasPendingCommentRange) {
                            if (!yield(f.Value.NewCommentRange(pendingKind, pendingPos, pendingEnd, pendingHasTrailingNewLine))) {
                                return;
                            }
                        }

                        pendingPos = startPos;
                        pendingEnd = pos;
                        pendingKind = kind;
                        pendingHasTrailingNewLine = hasTrailingNewLine;
                        hasPendingCommentRange = true;
                    }

                    continue;
                }
                goto break_scan;
            }
            else if (!matchᴛ1) { /* default: */
                if (ch > unicode.MaxASCII && stringutil.IsWhiteSpaceLike(ch)) {
                    if (hasPendingCommentRange && stringutil.IsLineBreak(ch)) {
                        pendingHasTrailingNewLine = true;
                    }
                    pos += size;
                    continue;
                }
                goto break_scan;
            }

continue_scan:;
        }
break_scan:;

        if (hasPendingCommentRange) {
            yield(f.Value.NewCommentRange(pendingKind, pendingPos, pendingEnd, pendingHasTrailingNewLine));
        }
    };
}

} // end scanner_package
