namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using utf8 = go.unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.unicode;
using iter = iter_package;
using ꓸꓸꓸany = Span<any>;
using static parser_package;

partial class parser_package {

partial struct ParsingContext /*num:nint*/;

public static ParsingContext PCSourceElements => /* iota */ 0;
public static ParsingContext PCBlockStatements => 1;
public static ParsingContext PCSwitchClauses => 2;
public static ParsingContext PCSwitchClauseStatements => 3;
public static ParsingContext PCTypeMembers => 4;
public static ParsingContext PCClassMembers => 5;
public static ParsingContext PCEnumMembers => 6;
public static ParsingContext PCHeritageClauseElement => 7;
public static ParsingContext PCVariableDeclarations => 8;
public static ParsingContext PCObjectBindingElements => 9;
public static ParsingContext PCArrayBindingElements => 10;
public static ParsingContext PCArgumentExpressions => 11;
public static ParsingContext PCObjectLiteralMembers => 12;
public static ParsingContext PCJsxAttributes => 13;
public static ParsingContext PCJsxChildren => 14;
public static ParsingContext PCArrayLiteralMembers => 15;
public static ParsingContext PCParameters => 16;
public static ParsingContext PCJSDocParameters => 17;
public static ParsingContext PCRestProperties => 18;
public static ParsingContext PCTypeParameters => 19;
public static ParsingContext PCTypeArguments => 20;
public static ParsingContext PCTupleElementTypes => 21;
public static ParsingContext PCHeritageClauses => 22;
public static ParsingContext PCImportOrExportSpecifiers => 23;
public static ParsingContext PCImportAttributes => 24;
public static ParsingContext PCJSDocComment => 25;
public static ParsingContext PCCount => 26;

partial struct ParsingContexts /*num:nint*/;

partial struct JSDocInfo {
    internal ж<ast.Node> parent;
    internal slice<ж<ast.Node>> jsDocs;
}

partial struct jsdocScannerInfo /*num:uint8*/;

internal static jsdocScannerInfo jsdocScannerInfoHasJSDoc => /* 1 << iota */ 1;
internal static jsdocScannerInfo jsdocScannerInfoHasDeprecated => 2;
internal static jsdocScannerInfo jsdocScannerInfoHasSeeOrLink => 4;

partial struct Parser {
    internal ж<scanner.Scanner> scanner;
    internal ast.NodeFactory factory;
    internal ast.SourceFileParseOptions opts;
    internal @string sourceText;
    internal core.ScriptKind scriptKind;
    internal core.LanguageVariant languageVariant;
    internal slice<ж<ast.Diagnostic>> diagnostics;
    internal slice<ж<ast.Diagnostic>> jsDiagnostics;
    internal slice<ж<ast.Diagnostic>> jsdocDiagnostics;
    internal ast.Kind token;
    internal ast.NodeFlags sourceFlags;
    internal ast.NodeFlags contextFlags;
    internal ParsingContexts parsingContexts;
    internal bool statementHasAwaitIdentifier;
    internal bool hasDeprecatedTag;
    internal bool hasParseError;
    internal nint identifierCount;
    internal collections.Set<nint> notParenthesizedArrow;
    internal core.Arena<ж<ast.Node>> nodeSliceArena;
    internal core.Arena<@string> stringSliceArena;
    internal slice<JSDocInfo> jsdocInfos;
    internal slice<nint> possibleAwaitSpans;
    internal slice<@string> jsdocCommentsSpace;
    internal slice<ast.CommentRange> jsdocCommentRangesSpace;
    internal slice<@string> jsdocTagCommentsSpace;
    internal slice<ж<ast.Node>> jsdocTagCommentsPartsSpace;
    internal slice<ж<ast.Node>> reparseList;
    internal ж<ast.Node> currentParent;
    internal Func<ж<ast.Node>, bool> setParentFromContext;
    internal slice<ж<ast.Node>> reparsedClones;
}

internal static ж<Parser> newParser() {
    var res = Ꮡ(new Parser());
    res.initializeClosures();
    return res;
}

internal static slice<@string> viableKeywordSuggestions = scanner.GetViableKeywordSuggestions();

internal static slice<ж<ast.Node>> missingListNodes = new slice<ж<ast.Node>>(0, 1);

internal static bool isMissingNodeList(ж<ast.NodeList> Ꮡlist) {
    ref var list = ref Ꮡlist.DerefOrNull();

    return Ꮡlist != nil && cap(list.Nodes) == 1 && Ꮡelem(list.Nodes[..1], 0) == Ꮡelem(missingListNodes[..1], 0);
}

internal static readonly ж<sync.Pool> ᏑparserPool = new StaticBox<sync.Pool>(new sync.Pool(
    New: () => newParser().OrTypedNil()
));
internal static ref sync.Pool parserPool => ref ((StaticBox<sync.Pool>)ᏑparserPool).ValueSlot;

internal static ж<Parser> getParser() {
    return ᏑparserPool.Get()._<ж<Parser>>();
}

internal static void putParser(ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p = new Parser(scanner: p.scanner, setParentFromContext: p.setParentFromContext);
    ᏑparserPool.Put(Ꮡp.OrTypedNil());
}

public static ж<ast.SourceFile> ParseSourceFile(ast.SourceFileParseOptions opts, @string sourceText, core.ScriptKind scriptKind) {
    GoFrame ᒐ = default;
    try {
        var p = getParser();
        defer(putParser, p, ref ᒐ);
        p.initializeState(opts, sourceText, scriptKind);
        p.nextToken();
        if (p.scriptKind == core.ScriptKindJSON) {
            return p.parseJSONText();
        }
        return p.parseSourceFileWorker();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class parserᴛParser {

internal static void initializeClosures(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.setParentFromContext = (ж<ast.Node> n) => {
        n.Parent = Ꮡp.Value.currentParent;
        return false;
    };
}

internal static bool isJavaScript(this ref Parser p) {
    return p.scriptKind == core.ScriptKindJS || p.scriptKind == core.ScriptKindJSX;
}

internal static ж<ast.SourceFile> parseJSONText(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    ж<ast.NodeList> statements = default;
    ж<astꓸTokenNode> eof = default;

    if (p.token == ast.KindEndOfFile) {
        statements = p.newNodeList(core.NewTextRange(pos, p.nodePos()), default);
        eof = p.parseTokenNode();
    } else {
        any expressions = default;

        while (p.token != ast.KindEndOfFile) {
            ж<astꓸExpression> expressionΔ1 = default;
            var exprᴛ1 = p.token;
            var matchᴛ1 = false;
            if (exprᴛ1 == ast.KindOpenBracketToken) { matchᴛ1 = true;
                expressionΔ1 = Ꮡp.parseArrayLiteralExpression();
            }
            else if (exprᴛ1 == ast.KindTrueKeyword || exprᴛ1 == ast.KindFalseKeyword || exprᴛ1 == ast.KindNullKeyword) { matchᴛ1 = true;
                expressionΔ1 = p.parseTokenNode();
            }
            else if (exprᴛ1 == ast.KindMinusToken) { matchᴛ1 = true;
                if (Ꮡp.lookAhead((ж<Parser> pΔ3) => pΔ3.nextToken() == ast.KindNumericLiteral && pΔ3.nextToken() != ast.KindColonToken)) {
                    expressionΔ1 = Ꮡp.parsePrefixUnaryExpression();
                } else {
                    expressionΔ1 = Ꮡp.parseObjectLiteralExpression();
                }
            }
            else if (exprᴛ1 == ast.KindNumericLiteral || exprᴛ1 == ast.KindStringLiteral) { matchᴛ1 = true;
                do {
                    if (Ꮡp.lookAhead((ж<Parser> pΔ4) => pΔ4.nextToken() != ast.KindColonToken)) {
                        expressionΔ1 = p.parseLiteralExpression();
                        break;
                    }
                    fallthrough = true;
                } while (false);
            }
            if (fallthrough || !matchᴛ1) { /* default: */
                expressionΔ1 = Ꮡp.parseObjectLiteralExpression();
            }

            if (expressions != nil) {
                var (es, ok) = expressions._<slice<ж<astꓸExpression>>>(ᐧ); if (ok) {
                    expressions = append(es, expressionΔ1);
                } else {
                    expressions = slice<ж<astꓸExpression>>([expressions._<ж<astꓸExpression>>(), expressionΔ1]);
                }
            } else {
                expressions = expressionΔ1.OrTypedNil();
                if (p.token != ast.KindEndOfFile) {
                    p.parseErrorAtCurrentToken(diagnostics.Unexpected_token);
                }
            }
        }

        ж<astꓸExpression> expression = default;
        {
            var (es, ok) = expressions._<slice<ж<astꓸExpression>>>(ᐧ); if (ok) {
                expression = p.finishNode(p.factory.NewArrayLiteralExpression(p.newNodeList(core.NewTextRange(pos, p.nodePos()), es), false), pos);
            } else {
                expression = expressions._<ж<astꓸExpression>>();
            }
        }
        var statement = p.finishNode(p.factory.NewExpressionStatement(expression), pos);
        statements = p.newNodeList(core.NewTextRange(pos, p.nodePos()), [statement]);
        eof = p.parseExpectedToken(ast.KindEndOfFile);
    }
    var node = p.finishNode(p.factory.NewSourceFile(p.opts, p.sourceText, statements, eof), pos);
    var result = node.AsSourceFile();
    if (len((~result).Statements.Nodes) > 0) {
        p.validateJsonValue(result, (~result).Statements.Nodes[0].Expression());
    }
    Ꮡp.finishSourceFile(result, false);
    return result;
}

} partial class parser_package {

internal static core.TextRange getErrorSpanForNode(@string sourceText, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    nint pos = node.Pos();
    if (!ast.NodeIsMissing(Ꮡnode)) {
        pos = scanner.SkipTrivia(sourceText, pos);
    }
    return core.NewTextRange(pos, node.End());
}

} partial class parserᴛParser {

internal static void validateJsonValue(this ref Parser p, ж<ast.SourceFile> sourceFile, ж<astꓸExpression> ᏑvalueExpression) {
    ref var valueExpression = ref ᏑvalueExpression.DerefOrNull();

    if (ᏑvalueExpression == nil) {
        return;
    }
    switch (valueExpression.Kind) {
    case var ᴛ when ᴛ == ast.KindTrueKeyword || ᴛ == ast.KindFalseKeyword || ᴛ == ast.KindNullKeyword || ᴛ == ast.KindNumericLiteral: {
        return;
    }
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        if (!isDoubleQuotedString(ᏑvalueExpression)) {
            p.diagnostics = append(p.diagnostics, ast.NewDiagnostic(sourceFile, getErrorSpanForNode(p.sourceText, ᏑvalueExpression), diagnostics.String_literal_with_double_quotes_expected));
        }
        return;
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
        if (valueExpression.AsPrefixUnaryExpression().Operator != ast.KindMinusToken || valueExpression.AsPrefixUnaryExpression().Operand.Kind != ast.KindNumericLiteral) {
            break;
        }
        return;
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        p.validateJsonObjectLiteral(sourceFile, valueExpression.AsObjectLiteralExpression());
        return;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        foreach (var (_, element) in range(valueExpression.Elements())) {
            p.validateJsonValue(sourceFile, element);
        }
        return;
    }}

    p.diagnostics = append(p.diagnostics, ast.NewDiagnostic(sourceFile, getErrorSpanForNode(p.sourceText, ᏑvalueExpression), diagnostics.Property_value_can_only_be_string_literal_numeric_literal_true_false_null_object_literal_or_array_literal));
}

} partial class parser_package {

internal static bool isDoubleQuotedString(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsStringLiteral(Ꮡnode) && (ast.TokenFlags)(node.AsStringLiteral().TokenFlags & ast.TokenFlagsSingleQuote) == 0;
}

} partial class parserᴛParser {

internal static void validateJsonObjectLiteral(this ref Parser p, ж<ast.SourceFile> sourceFile, ж<ast.ObjectLiteralExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    foreach (var (_, element) in range(node.Properties.Nodes)) {
        if (element.Kind != ast.KindPropertyAssignment) {
            p.diagnostics = append(p.diagnostics, ast.NewDiagnostic(sourceFile, getErrorSpanForNode(p.sourceText, element), diagnostics.Property_assignment_expected));
            continue;
        }
        if (element.Name() != nil && !isDoubleQuotedString(element.Name())) {
            p.diagnostics = append(p.diagnostics, ast.NewDiagnostic(sourceFile, getErrorSpanForNode(p.sourceText, element.Name()), diagnostics.String_literal_with_double_quotes_expected));
        }
        p.validateJsonValue(sourceFile, (~element.AsPropertyAssignment()).Initializer);
    }
}

} partial class parser_package {

public static ж<astꓸEntityName> ParseIsolatedEntityName(@string text) {
    GoFrame ᒐ = default;
    try {
        var p = getParser();
        defer(putParser, p, ref ᒐ);
        p.initializeState(new ast.SourceFileParseOptions(), text, core.ScriptKindJS);
        p.nextToken();
        var entityName = p.parseEntityName(true, false, nil);
        return core.IfElse(p.token == ast.KindEndOfFile && len(p.diagnostics) == 0, entityName, (ж<ast.Node>)(nil));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class parserᴛParser {

internal static void initializeState(this ж<Parser> Ꮡp, ast.SourceFileParseOptions opts, @string sourceText, core.ScriptKind scriptKind) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (scriptKind == core.ScriptKindUnknown) {
        throw panic("ScriptKind must be specified when parsing source file: " + opts.FileName);
    }

    if (p.scanner == nil) {
        p.scanner = scanner.NewScanner();
    } else {
        p.scanner.Reset();
    }
    p.opts = opts;
    p.sourceText = sourceText;
    p.scriptKind = scriptKind;
    p.languageVariant = getLanguageVariant(p.scriptKind);
    switch (p.scriptKind) {
    case var ᴛ when ᴛ == core.ScriptKindJS || ᴛ == core.ScriptKindJSX: {
        p.contextFlags = ast.NodeFlagsJavaScriptFile;
        break;
    }
    case var ᴛ when ᴛ == core.ScriptKindJSON: {
        p.contextFlags = (ast.NodeFlags)(ast.NodeFlagsJavaScriptFile | ast.NodeFlagsJsonFile);
        break;
    }
    default: {
        p.contextFlags = ast.NodeFlagsNone;
        break;
    }}

    p.scanner.SetText(p.sourceText);
    p.scanner.SetOnError(new Actionꓸꓸꓸ<ж<diagnostics.Message>, nint, nint, any>(Ꮡp.scanError));
    p.scanner.SetLanguageVariant(p.languageVariant);
}

internal static void scanError(this ref Parser p, ж<diagnostics.Message> message, nint pos, nint length, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    p.parseErrorAtRange(core.NewTextRange(pos, pos + length), message, args.ꓸꓸꓸ);
}

internal static ж<ast.Diagnostic> parseErrorAt(this ref Parser p, nint pos, nint end, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    return p.parseErrorAtRange(core.NewTextRange(pos, end), message, args.ꓸꓸꓸ);
}

internal static ж<ast.Diagnostic> parseErrorAtCurrentToken(this ref Parser p, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    return p.parseErrorAtRange(p.scanner.TokenRange(), message, args.ꓸꓸꓸ);
}

internal static ж<ast.Diagnostic> parseErrorAtRange(this ref Parser p, core.TextRange loc, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    ж<ast.Diagnostic> result = default;
    if (len(p.diagnostics) == 0 || p.diagnostics[len(p.diagnostics) - 1].Pos() != loc.Pos()) {
        result = ast.NewDiagnostic(nil, loc, message, args.ꓸꓸꓸ);
        p.diagnostics = append(p.diagnostics, result);
    }
    p.hasParseError = true;
    return result;
}

} partial class parser_package {

partial struct ParserState {
    internal scanner.ScannerState scannerState;
    internal ast.NodeFlags contextFlags;
    internal nint diagnosticsLen;
    internal nint jsDiagnosticsLen;
    internal nint jsdocInfosLen;
    internal nint reparsedClonesLen;
    internal bool statementHasAwaitIdentifier;
    internal bool hasParseError;
}

} partial class parserᴛParser {

internal static ParserState mark(this ref Parser p) {
    return new ParserState(
        scannerState: p.scanner.Mark(),
        contextFlags: p.contextFlags,
        diagnosticsLen: len(p.diagnostics),
        jsDiagnosticsLen: len(p.jsDiagnostics),
        jsdocInfosLen: len(p.jsdocInfos),
        reparsedClonesLen: len(p.reparsedClones),
        statementHasAwaitIdentifier: p.statementHasAwaitIdentifier,
        hasParseError: p.hasParseError
    );
}

internal static void rewind(this ref Parser p, ParserState state) {
    p.scanner.Rewind(state.scannerState);
    p.token = p.scanner.Token();
    p.contextFlags = state.contextFlags;
    p.diagnostics = p.diagnostics.slice(0, state.diagnosticsLen);
    p.jsDiagnostics = p.jsDiagnostics.slice(0, state.jsDiagnosticsLen);
    p.jsdocInfos = p.jsdocInfos.slice(0, state.jsdocInfosLen);
    p.reparsedClones = p.reparsedClones.slice(0, state.reparsedClonesLen);
    p.statementHasAwaitIdentifier = state.statementHasAwaitIdentifier;
    p.hasParseError = state.hasParseError;
}

internal static bool lookAhead(this ж<Parser> Ꮡp, Func<ж<Parser>, bool> callback) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.mark();
    var result = callback(Ꮡp);
    p.rewind(state);
    return result;
}

internal static ast.Kind nextToken(this ref Parser p) {
    if (ast.IsKeyword(p.token) && (p.scanner.HasUnicodeEscape() || p.scanner.HasExtendedUnicodeEscape())) {
        p.parseErrorAtCurrentToken(diagnostics.Keywords_cannot_contain_escape_characters);
    }
    p.token = p.scanner.Scan();
    return p.token;
}

internal static ast.Kind nextTokenWithoutCheck(this ref Parser p) {
    p.token = p.scanner.Scan();
    return p.token;
}

internal static ast.Kind nextTokenJSDoc(this ref Parser p) {
    p.token = p.scanner.ScanJSDocToken();
    return p.token;
}

internal static ast.Kind nextJSDocCommentTextToken(this ref Parser p, bool inBackticks) {
    p.token = p.scanner.ScanJSDocCommentTextToken(inBackticks);
    return p.token;
}

internal static nint nodePos(this ref Parser p) {
    return p.scanner.TokenFullStart();
}

internal static bool hasPrecedingLineBreak(this ref Parser p) {
    return p.scanner.HasPrecedingLineBreak();
}

internal static jsdocScannerInfo jsdocScannerInfo(this ref Parser p) {
    if (!p.scanner.HasPrecedingJSDocComment()) {
        return 0;
    }
    var info = jsdocScannerInfoHasJSDoc;
    if (p.scanner.HasPrecedingJSDocWithDeprecatedTag()) {
        info |= (jsdocScannerInfo)(jsdocScannerInfoHasDeprecated);
    }
    if (p.scanner.HasPrecedingJSDocWithSeeOrLink()) {
        info |= (jsdocScannerInfo)(jsdocScannerInfoHasSeeOrLink);
    }
    return info;
}

internal static ж<ast.SourceFile> parseSourceFileWorker(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var isDeclarationFile = tspath.IsDeclarationFileName(p.opts.FileName);
    if (isDeclarationFile) {
        p.contextFlags |= (ast.NodeFlags)(ast.NodeFlagsAmbient);
    }
    nint pos = p.nodePos();
    var statements = Ꮡp.parseListIndex(PCSourceElements, ((Func<ж<Parser>, nint, ж<ast.Node>>)(parserᴛParser.parseToplevelStatement)));
    nint end = p.nodePos();
    var endJSDoc = p.jsdocScannerInfo();
    var eof = p.parseTokenNode();
    Ꮡp.withJSDoc(eof, endJSDoc);
    if (eof.Kind != ast.KindEndOfFile) {
        throw panic("Expected end of file token from scanner.");
    }
    if (len(p.reparseList) != 0) {
        statements = appendꓸꓸꓸ(statements, p.reparseList);
        p.reparseList = default;
    }
    var node = p.finishNode(p.factory.NewSourceFile(p.opts, p.sourceText, p.newNodeList(core.NewTextRange(pos, end), statements), eof), pos);
    var result = node.AsSourceFile();
    Ꮡp.finishSourceFile(result, isDeclarationFile);
    if (!result.IsDeclarationFile && result.ExternalModuleIndicator != nil && len(p.possibleAwaitSpans) > 0) {
        var reparse = p.finishNode(Ꮡp.reparseTopLevelAwait(result), pos);
        if (node != reparse) {
            result = reparse.AsSourceFile();
            Ꮡp.finishSourceFile(result, isDeclarationFile);
        }
    }
    collectExternalModuleReferences(result);
    if (ast.IsInJSFile(node)) {
        result.SetJSDiagnostics(attachFileToDiagnostics(p.jsDiagnostics, result));
    }
    return result;
}

internal static void finishSourceFile(this ж<Parser> Ꮡp, ж<ast.SourceFile> Ꮡresult, bool isDeclarationFile) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var result = ref Ꮡresult.DerefOrNull();

    result.CommentDirectives = p.scanner.CommentDirectives();
    result.Pragmas = getCommentPragmas(Ꮡp.Ꮡfactory, p.sourceText);
    p.processPragmasIntoFields(Ꮡresult);
    result.SetDiagnostics(attachFileToDiagnostics(p.diagnostics, Ꮡresult));
    result.SetJSDocDiagnostics(attachFileToDiagnostics(p.jsdocDiagnostics, Ꮡresult));
    result.IsDeclarationFile = isDeclarationFile;
    result.LanguageVariant = p.languageVariant;
    result.ScriptKind = p.scriptKind;
    result.Flags |= (ast.NodeFlags)(p.sourceFlags);
    result.NodeCount = p.factory.NodeCount();
    result.TextCount = p.factory.TextCount();
    result.IdentifierCount = p.identifierCount;
    result.SetJSDocCache(p.createJSDocCache());
    if (!p.isJavaScript()) {
        result.SetHasLazyJSDoc(true);
    }
    slices.SortFunc<slice<ж<ast.Node>>, ж<ast.Node>>(p.reparsedClones, ast.CompareNodePositions);
    result.ReparsedClones = slices.Clone<slice<ж<ast.Node>>, ж<ast.Node>>(p.reparsedClones);
    ast.SetExternalModuleIndicator(Ꮡresult, p.opts.ExternalModuleIndicatorOptions);
}

internal static map<ж<ast.Node>, slice<ж<ast.Node>>> createJSDocCache(this ref Parser p) {
    if (len(p.jsdocInfos) == 0) {
        return default;
    }
    var result = new map<ж<ast.Node>, slice<ж<ast.Node>>>(len(p.jsdocInfos));
    foreach (var (_, info) in range(p.jsdocInfos)) {
        result[info.parent] = info.jsDocs;
    }
    return result;
}

internal static ж<ast.Node> parseToplevelStatement(this ж<Parser> Ꮡp, nint i) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.statementHasAwaitIdentifier = false;
    var statement = Ꮡp.parseStatement();
    i += len(p.reparseList);
    if (p.statementHasAwaitIdentifier && (ast.NodeFlags)(statement.Flags & ast.NodeFlagsAwaitContext) == 0) {
        if (len(p.possibleAwaitSpans) == 0 || p.possibleAwaitSpans[len(p.possibleAwaitSpans) - 1] != i) {
            p.possibleAwaitSpans = append(p.possibleAwaitSpans, i, i + 1);
        } else {
            p.possibleAwaitSpans[len(p.possibleAwaitSpans) - 1] = i + 1;
        }
    }
    return statement;
}

internal static ж<ast.Node> reparseTopLevelAwait(this ж<Parser> Ꮡp, ж<ast.SourceFile> ᏑsourceFile) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (len(p.possibleAwaitSpans) % 2 == 1) {
        throw panic("possibleAwaitSpans malformed: odd number of indices, not paired into spans.");
    }
    var statements = slice<ж<astꓸStatement>>([]);
    var savedParseDiagnostics = p.diagnostics;
    p.diagnostics = [];

    nint afterAwaitStatement = 0;
    for (nint i = 0; i < len(p.possibleAwaitSpans); i += 2) {
        nint nextAwaitStatement = p.possibleAwaitSpans[i];
        var prevStatement = sourceFile.Statements.Nodes[afterAwaitStatement];
        var nextStatement = sourceFile.Statements.Nodes[nextAwaitStatement];
        statements = appendꓸꓸꓸ(statements, sourceFile.Statements.Nodes.slice(afterAwaitStatement, nextAwaitStatement));

        var prevStatementʗ1 = prevStatement;
        nint diagnosticStart = core.FindIndex(savedParseDiagnostics, (ж<ast.Diagnostic> diagnostic) => diagnostic.Pos() >= prevStatementʗ1.Pos());
        nint diagnosticEnd = default;
        if (diagnosticStart >= 0) {
            var nextStatementʗ1 = nextStatement;
            diagnosticEnd = core.FindIndex(savedParseDiagnostics.slice(diagnosticStart), (ж<ast.Diagnostic> diagnostic) => diagnostic.Pos() >= nextStatementʗ1.Pos());
        } else {
            diagnosticEnd = -1;
        }
        if (diagnosticStart >= 0) {
            slice<ж<ast.Diagnostic>> Δslice = default;
            if (diagnosticEnd >= 0) {
                Δslice = savedParseDiagnostics.slice(diagnosticStart, diagnosticStart + diagnosticEnd);
            } else {
                Δslice = savedParseDiagnostics.slice(diagnosticStart);
            }
            p.diagnostics = appendꓸꓸꓸ(p.diagnostics, Δslice);
        }

        var state = p.mark();
        p.contextFlags |= (ast.NodeFlags)(ast.NodeFlagsAwaitContext);
        p.scanner.ResetPos(nextStatement.Pos());
        p.nextToken();

        afterAwaitStatement = p.possibleAwaitSpans[i + 1];
        while (p.token != ast.KindEndOfFile) {
            nint startPos = p.scanner.TokenFullStart();
            var statement = Ꮡp.parseStatement();
            statements = append(statements, statement);
            if (startPos == p.scanner.TokenFullStart()) {
                p.nextToken();
            }
            if (afterAwaitStatement < len(sourceFile.Statements.Nodes)) {
                var lastAwaitStatement = sourceFile.Statements.Nodes[afterAwaitStatement - 1];
                if (statement.End() == lastAwaitStatement.End()) {
                    break;
                }
                if (statement.End() > lastAwaitStatement.End()) {
                    i += 2;
                    if (i < len(p.possibleAwaitSpans)) {
                        afterAwaitStatement = p.possibleAwaitSpans[i + 1];
                    } else {
                        afterAwaitStatement = len(sourceFile.Statements.Nodes);
                    }
                }
            }
        }

        state.diagnosticsLen = len(p.diagnostics);
        p.rewind(state);
    }

    if (afterAwaitStatement < len(sourceFile.Statements.Nodes)) {
        var prevStatement = sourceFile.Statements.Nodes[afterAwaitStatement];
        statements = appendꓸꓸꓸ(statements, sourceFile.Statements.Nodes.slice(afterAwaitStatement));

        var prevStatementʗ2 = prevStatement;
        nint diagnosticStart = core.FindIndex(savedParseDiagnostics, (ж<ast.Diagnostic> diagnostic) => diagnostic.Pos() >= prevStatementʗ2.Pos());
        if (diagnosticStart >= 0) {
            p.diagnostics = appendꓸꓸꓸ(p.diagnostics, savedParseDiagnostics.slice(diagnosticStart));
        }
    }

    var result = p.factory.NewSourceFile(sourceFile.ParseOptions(), p.sourceText, p.newNodeList(sourceFile.Statements.Loc, statements), sourceFile.EndOfFileToken);
    foreach (var (_, s) in range(statements)) {
        s.Parent = result.AsNode();
    }
    return result;
}

internal static slice<ж<ast.Node>> parseListIndex(this ж<Parser> Ꮡp, ParsingContext kind, Func<ж<Parser>, nint, ж<ast.Node>> parseElement) {
    ref var p = ref Ꮡp.DerefOrNull();
    var listᴷ = new stackAppend<ж<ast.Node>>();

    ParsingContexts saveParsingContexts = p.parsingContexts;
    p.parsingContexts |= (ParsingContexts)((ParsingContexts)(nint)(1 << (int)(kind)));
    var outerReparseList = p.reparseList;
    p.reparseList = default;
    var list = new slice<ж<ast.Node>>(0, 16);
    for (nint i = 0; !Ꮡp.isListTerminator(kind); i++) {
        if (Ꮡp.isListElement(kind, false)) {
            var elt = parseElement(Ꮡp, len(list));
            if (len(p.reparseList) != 0) {
                foreach (var (_, e) in range(p.reparseList)) {
                    if ((ast.IsJSTypeAliasDeclaration(e) || ast.IsJSImportDeclaration(e)) && kind != PCSourceElements && kind != PCBlockStatements) {
                        outerReparseList = append(outerReparseList, e);
                    } else {
                        list = append(listᴷ, list, e);
                    }
                }
                p.reparseList = default;
            }
            list = append(list, elt);
            continue;
        }
        if (Ꮡp.abortParsingListOrMoveToNextToken(kind)) {
            break;
        }
    }
    p.reparseList = outerReparseList;
    p.parsingContexts = saveParsingContexts;
    return p.nodeSliceArena.Clone(list);
}

internal static ж<ast.NodeList> parseList(this ж<Parser> Ꮡp, ParsingContext kind, Func<ж<Parser>, ж<ast.Node>> parseElement) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var nodes = Ꮡp.parseListIndex(kind, (ж<Parser> pΔ1, nint _) => parseElement(pΔ1));
    return p.newNodeList(core.NewTextRange(pos, p.nodePos()), nodes);
}

internal static ж<ast.NodeList> parseDelimitedList(this ж<Parser> Ꮡp, ParsingContext kind, Func<ж<Parser>, ж<ast.Node>> parseElement) {
    ref var p = ref Ꮡp.DerefOrNull();
    var listᴷ = new stackAppend<ж<ast.Node>>();

    nint pos = p.nodePos();
    ParsingContexts saveParsingContexts = p.parsingContexts;
    p.parsingContexts |= (ParsingContexts)((ParsingContexts)(nint)(1 << (int)(kind)));
    var list = new slice<ж<ast.Node>>(0, 16);
    for (;;) {
        if (Ꮡp.isListElement(kind, false)) {
            nint startPos = p.nodePos();
            var element = parseElement(Ꮡp);
            if (element == nil) {
                p.parsingContexts = saveParsingContexts;
                return default;
            }
            list = append(listᴷ, list, element);
            if (p.parseOptional(ast.KindCommaToken)) {
                continue;
            }
            if (Ꮡp.isListTerminator(kind)) {
                break;
            }
            if (p.token != ast.KindCommaToken && kind == PCEnumMembers) {
                p.parseErrorAtCurrentToken(diagnostics.An_enum_member_name_must_be_followed_by_a_or);
            } else {
                p.parseExpected(ast.KindCommaToken);
            }
            if ((kind == PCObjectLiteralMembers || kind == PCImportAttributes) && p.token == ast.KindSemicolonToken && !p.hasPrecedingLineBreak()) {
                p.nextToken();
            }
            if (startPos == p.nodePos()) {
                p.nextToken();
            }
            continue;
        }
        if (Ꮡp.isListTerminator(kind)) {
            break;
        }
        if (Ꮡp.abortParsingListOrMoveToNextToken(kind)) {
            break;
        }
    }
    p.parsingContexts = saveParsingContexts;
    return p.newNodeList(core.NewTextRange(pos, p.nodePos()), p.nodeSliceArena.Clone(list));
}

internal static ж<ast.NodeList> parseBracketedList(this ж<Parser> Ꮡp, ParsingContext kind, Func<ж<Parser>, ж<ast.Node>> parseElement, ast.Kind opening, ast.Kind closing) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.parseExpected(opening)) {
        var result = Ꮡp.parseDelimitedList(kind, parseElement);
        p.parseExpected(closing);
        return result;
    }
    return p.createMissingList();
}

internal static ж<ast.NodeList> parseEmptyNodeList(this ref Parser p) {
    return p.newNodeList(core.NewTextRange(p.nodePos(), p.nodePos()), default);
}

internal static ж<ast.NodeList> createMissingList(this ref Parser p) {
    var result = p.parseEmptyNodeList();
    result.Nodes = missingListNodes;
    return result;
}

internal static bool abortParsingListOrMoveToNextToken(this ж<Parser> Ꮡp, ParsingContext kind) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parsingContextErrors(kind);
    if (Ꮡp.isInSomeParsingContext()) {
        return true;
    }
    p.nextToken();
    return false;
}

internal static bool isInSomeParsingContext(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    debug.Assert(p.parsingContexts != 0, "Missing parsing context");
    foreach (var kind in range(PCCount)) {
        if ((ParsingContexts)(p.parsingContexts & ((ParsingContexts)(nint)(1 << (int)(kind)))) != 0) {
            if (Ꮡp.isListElement(kind, true) || Ꮡp.isListTerminator(kind)) {
                return true;
            }
        }
    }
    return false;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
private static readonly object exportˢ = (@string)"export"u8;

internal static void parsingContextErrors(this ref Parser p, ParsingContext context) {
    switch (context) {
    case var ᴛ when ᴛ == PCSourceElements: {
        if (p.token == ast.KindDefaultKeyword) {
            p.parseErrorAtCurrentToken(diagnostics.X_0_expected, exportˢ);
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Declaration_or_statement_expected);
        }
        break;
    }
    case var ᴛ when ᴛ == PCBlockStatements: {
        p.parseErrorAtCurrentToken(diagnostics.Declaration_or_statement_expected);
        break;
    }
    case var ᴛ when ᴛ == PCSwitchClauses: {
        p.parseErrorAtCurrentToken(diagnostics.X_case_or_default_expected);
        break;
    }
    case var ᴛ when ᴛ == PCSwitchClauseStatements: {
        p.parseErrorAtCurrentToken(diagnostics.Statement_expected);
        break;
    }
    case var ᴛ when ᴛ == PCRestProperties || ᴛ == PCTypeMembers: {
        p.parseErrorAtCurrentToken(diagnostics.Property_or_signature_expected);
        break;
    }
    case var ᴛ when ᴛ == PCClassMembers: {
        p.parseErrorAtCurrentToken(diagnostics.Unexpected_token_A_constructor_method_accessor_or_property_was_expected);
        break;
    }
    case var ᴛ when ᴛ == PCEnumMembers: {
        p.parseErrorAtCurrentToken(diagnostics.Enum_member_expected);
        break;
    }
    case var ᴛ when ᴛ == PCHeritageClauseElement: {
        p.parseErrorAtCurrentToken(diagnostics.Expression_expected);
        break;
    }
    case var ᴛ when ᴛ == PCVariableDeclarations: {
        if (ast.IsKeyword(p.token)) {
            p.parseErrorAtCurrentToken(diagnostics.X_0_is_not_allowed_as_a_variable_declaration_name, scanner.TokenToString(p.token));
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Variable_declaration_expected);
        }
        break;
    }
    case var ᴛ when ᴛ == PCObjectBindingElements: {
        p.parseErrorAtCurrentToken(diagnostics.Property_destructuring_pattern_expected);
        break;
    }
    case var ᴛ when ᴛ == PCArrayBindingElements: {
        p.parseErrorAtCurrentToken(diagnostics.Array_element_destructuring_pattern_expected);
        break;
    }
    case var ᴛ when ᴛ == PCArgumentExpressions: {
        p.parseErrorAtCurrentToken(diagnostics.Argument_expression_expected);
        break;
    }
    case var ᴛ when ᴛ == PCObjectLiteralMembers: {
        p.parseErrorAtCurrentToken(diagnostics.Property_assignment_expected);
        break;
    }
    case var ᴛ when ᴛ == PCArrayLiteralMembers: {
        p.parseErrorAtCurrentToken(diagnostics.Expression_or_comma_expected);
        break;
    }
    case var ᴛ when ᴛ == PCJSDocParameters: {
        p.parseErrorAtCurrentToken(diagnostics.Parameter_declaration_expected);
        break;
    }
    case var ᴛ when ᴛ == PCParameters: {
        if (ast.IsKeyword(p.token)) {
            p.parseErrorAtCurrentToken(diagnostics.X_0_is_not_allowed_as_a_parameter_name, scanner.TokenToString(p.token));
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Parameter_declaration_expected);
        }
        break;
    }
    case var ᴛ when ᴛ == PCTypeParameters: {
        p.parseErrorAtCurrentToken(diagnostics.Type_parameter_declaration_expected);
        break;
    }
    case var ᴛ when ᴛ == PCTypeArguments: {
        p.parseErrorAtCurrentToken(diagnostics.Type_argument_expected);
        break;
    }
    case var ᴛ when ᴛ == PCTupleElementTypes: {
        p.parseErrorAtCurrentToken(diagnostics.Type_expected);
        break;
    }
    case var ᴛ when ᴛ == PCHeritageClauses: {
        p.parseErrorAtCurrentToken(diagnostics.Unexpected_token_expected);
        break;
    }
    case var ᴛ when ᴛ == PCImportOrExportSpecifiers: {
        if (p.token == ast.KindFromKeyword) {
            p.parseErrorAtCurrentToken(diagnostics.X_0_expected, (@string)"}");
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Identifier_expected);
        }
        break;
    }
    case var ᴛ when ᴛ == PCJsxAttributes || ᴛ == PCJsxChildren || ᴛ == PCJSDocComment: {
        p.parseErrorAtCurrentToken(diagnostics.Identifier_expected);
        break;
    }
    case var ᴛ when ᴛ == PCImportAttributes: {
        p.parseErrorAtCurrentToken(diagnostics.Identifier_or_string_literal_expected);
        break;
    }
    default: {
        throw panic("Unhandled case in parsingContextErrors");
        break;
    }}

}

internal static bool isListElement(this ж<Parser> Ꮡp, ParsingContext parsingContext, bool inErrorRecovery) {
    ref var p = ref Ꮡp.DerefOrNull();

    var matchᴛ1 = false;
    if (parsingContext == PCSourceElements || parsingContext == PCBlockStatements || parsingContext == PCSwitchClauseStatements) { matchᴛ1 = true;
        return !(p.token == ast.KindSemicolonToken && inErrorRecovery) && Ꮡp.isStartOfStatement();
    }
    if (parsingContext == PCSwitchClauses) { matchᴛ1 = true;
        return p.token == ast.KindCaseKeyword || p.token == ast.KindDefaultKeyword;
    }
    if (parsingContext == PCTypeMembers) { matchᴛ1 = true;
        return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.scanTypeMemberStart)));
    }
    if (parsingContext == PCClassMembers) { matchᴛ1 = true;
        return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.scanClassMemberStart))) || p.token == ast.KindSemicolonToken && !inErrorRecovery;
    }
    if (parsingContext == PCEnumMembers) { matchᴛ1 = true;
        return p.token == ast.KindOpenBracketToken || p.isLiteralPropertyName();
    }
    if (parsingContext == PCObjectLiteralMembers) { matchᴛ1 = true;
        switch (p.token) {
        case var ᴛ when ᴛ == ast.KindOpenBracketToken || ᴛ == ast.KindAsteriskToken || ᴛ == ast.KindDotDotDotToken || ᴛ == ast.KindDotToken: {
            return true;
        }
        default: {
            return p.isLiteralPropertyName();
        }}

    }
    else if (parsingContext == PCRestProperties) { matchᴛ1 = true;
        return p.isLiteralPropertyName();
    }
    else if (parsingContext == PCObjectBindingElements) { matchᴛ1 = true;
        return p.token == ast.KindOpenBracketToken || p.token == ast.KindDotDotDotToken || p.isLiteralPropertyName();
    }
    else if (parsingContext == PCImportAttributes) { matchᴛ1 = true;
        return p.isImportAttributeName();
    }
    else if (parsingContext == PCHeritageClauseElement) { matchᴛ1 = true;
        if (p.token == ast.KindOpenBraceToken) {
            return Ꮡp.isValidHeritageClauseObjectLiteral();
        }
        if (!inErrorRecovery) {
            return Ꮡp.isStartOfLeftHandSideExpression() && !Ꮡp.isHeritageClauseExtendsOrImplementsKeyword();
        }
        return p.isIdentifier() && !Ꮡp.isHeritageClauseExtendsOrImplementsKeyword();
    }
    else if (parsingContext == PCVariableDeclarations) { matchᴛ1 = true;
        return p.isBindingIdentifierOrPrivateIdentifierOrPattern();
    }
    else if (parsingContext == PCArrayBindingElements) { matchᴛ1 = true;
        return p.token == ast.KindCommaToken || p.token == ast.KindDotDotDotToken || p.isBindingIdentifierOrPrivateIdentifierOrPattern();
    }
    else if (parsingContext == PCTypeParameters) { matchᴛ1 = true;
        return p.token == ast.KindInKeyword || p.token == ast.KindConstKeyword || p.isIdentifier();
    }
    else if (parsingContext == PCArrayLiteralMembers) { matchᴛ1 = true;
        if (p.token == ast.KindCommaToken || p.token == ast.KindDotToken) {
            return true;
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && parsingContext == PCArgumentExpressions) { matchᴛ1 = true;
        return p.token == ast.KindDotDotDotToken || Ꮡp.isStartOfExpression();
    }
    else if (parsingContext == PCParameters) { matchᴛ1 = true;
        return Ꮡp.isStartOfParameter(false);
    }
    else if (parsingContext == PCJSDocParameters) { matchᴛ1 = true;
        return Ꮡp.isStartOfParameter(true);
    }
    else if (parsingContext == PCTypeArguments || parsingContext == PCTupleElementTypes) { matchᴛ1 = true;
        return p.token == ast.KindCommaToken || Ꮡp.isStartOfType(false);
    }
    else if (parsingContext == PCHeritageClauses) { matchᴛ1 = true;
        return p.isHeritageClause();
    }
    else if (parsingContext == PCImportOrExportSpecifiers) { matchᴛ1 = true;
        if (p.token == ast.KindFromKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsTokenStringLiteral)))) {
            return false;
        }
        if (p.token == ast.KindStringLiteral) {
            return true;
        }
        return tokenIsIdentifierOrKeyword(p.token);
    }
    else if (parsingContext == PCJsxAttributes) { matchᴛ1 = true;
        return tokenIsIdentifierOrKeyword(p.token) || p.token == ast.KindOpenBraceToken;
    }
    else if (parsingContext == PCJsxChildren) {
        return true;
    }
    else if (parsingContext == PCJSDocComment) { matchᴛ1 = true;
        return true;
    }

    throw panic("Unhandled case in isListElement");
}

internal static bool isListTerminator(this ж<Parser> Ꮡp, ParsingContext kind) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindEndOfFile) {
        return true;
    }
    switch (kind) {
    case var ᴛ when ᴛ == PCBlockStatements || ᴛ == PCSwitchClauses || ᴛ == PCTypeMembers || ᴛ == PCClassMembers || ᴛ == PCEnumMembers || ᴛ == PCObjectLiteralMembers || ᴛ == PCObjectBindingElements || ᴛ == PCImportOrExportSpecifiers || ᴛ == PCImportAttributes: {
        return p.token == ast.KindCloseBraceToken;
    }
    case var ᴛ when ᴛ == PCSwitchClauseStatements: {
        return p.token == ast.KindCloseBraceToken || p.token == ast.KindCaseKeyword || p.token == ast.KindDefaultKeyword;
    }
    case var ᴛ when ᴛ == PCHeritageClauseElement: {
        return p.token == ast.KindOpenBraceToken || p.token == ast.KindExtendsKeyword || p.token == ast.KindImplementsKeyword;
    }
    case var ᴛ when ᴛ == PCVariableDeclarations: {
        return p.canParseSemicolon() || p.token == ast.KindInKeyword || p.token == ast.KindOfKeyword || p.token == ast.KindEqualsGreaterThanToken;
    }
    case var ᴛ when ᴛ == PCTypeParameters: {
        return p.token == ast.KindGreaterThanToken || p.token == ast.KindOpenParenToken || p.token == ast.KindOpenBraceToken || p.token == ast.KindExtendsKeyword || p.token == ast.KindImplementsKeyword;
    }
    case var ᴛ when ᴛ == PCArgumentExpressions: {
        return p.token == ast.KindCloseParenToken || p.token == ast.KindSemicolonToken;
    }
    case var ᴛ when ᴛ == PCArrayLiteralMembers || ᴛ == PCTupleElementTypes || ᴛ == PCArrayBindingElements: {
        return p.token == ast.KindCloseBracketToken;
    }
    case var ᴛ when ᴛ == PCJSDocParameters || ᴛ == PCParameters || ᴛ == PCRestProperties: {
        return p.token == ast.KindCloseParenToken || p.token == ast.KindCloseBracketToken;
    }
    case var ᴛ when ᴛ == PCTypeArguments: {
        return p.token != ast.KindCommaToken;
    }
    case var ᴛ when ᴛ == PCHeritageClauses: {
        return p.token == ast.KindOpenBraceToken || p.token == ast.KindCloseBraceToken;
    }
    case var ᴛ when ᴛ == PCJsxAttributes: {
        return p.token == ast.KindGreaterThanToken || p.token == ast.KindSlashToken;
    }
    case var ᴛ when ᴛ == PCJsxChildren: {
        return p.token == ast.KindLessThanToken && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsSlash)));
    }}

    return false;
}

internal static bool parseExpectedJSDoc(this ref Parser p, ast.Kind kind) {
    if (p.token == kind) {
        p.nextTokenJSDoc();
        return true;
    }
    if (!isKeywordOrPunctuation(kind)) {
        throw panic("Invalid JSDoc kind: expected keyword or punctuation");
    }
    p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(kind));
    return false;
}

internal static void parseExpectedMatchingBrackets(this ref Parser p, ast.Kind openKind, ast.Kind closeKind, bool openParsed, nint openPosition) {
    if (p.token == closeKind) {
        p.nextToken();
        return;
    }
    var lastError = p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(closeKind));
    if (!openParsed) {
        return;
    }
    if (lastError != nil) {
        var related = ast.NewDiagnostic(nil, core.NewTextRange(openPosition, openPosition), diagnostics.The_parser_expected_to_find_a_1_to_match_the_0_token_here, scanner.TokenToString(openKind), scanner.TokenToString(closeKind));
        lastError.AddRelatedInfo(related);
    }
}

internal static bool parseOptional(this ref Parser p, ast.Kind token) {
    if (p.token == token) {
        p.nextToken();
        return true;
    }
    return false;
}

internal static bool parseExpected(this ref Parser p, ast.Kind kind) {
    return p.parseExpectedWithDiagnostic(kind, nil, true);
}

internal static bool parseExpectedWithoutAdvancing(this ref Parser p, ast.Kind kind) {
    return p.parseExpectedWithDiagnostic(kind, nil, false);
}

internal static bool parseExpectedWithDiagnostic(this ref Parser p, ast.Kind kind, ж<diagnostics.Message> message, bool shouldAdvance) {
    if (p.token == kind) {
        if (shouldAdvance) {
            p.nextToken();
        }
        return true;
    }
    if (message != nil) {
        p.parseErrorAtCurrentToken(message);
    } else {
        p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(kind));
    }
    return false;
}

internal static ж<ast.Node> parseTokenNode(this ref Parser p) {
    nint pos = p.nodePos();
    var kind = p.token;
    p.nextToken();
    return p.finishNode(p.factory.NewToken(kind), pos);
}

internal static ж<ast.Node> parseExpectedToken(this ref Parser p, ast.Kind kind) {
    var token = p.parseOptionalToken(kind);
    if (token == nil) {
        p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(kind));
        token = p.finishNode(p.factory.NewToken(kind), p.nodePos());
    }
    return token;
}

internal static ж<ast.Node> parseOptionalToken(this ref Parser p, ast.Kind kind) {
    if (p.token == kind) {
        return p.parseTokenNode();
    }
    return default;
}

internal static ж<ast.Node> parseExpectedTokenJSDoc(this ref Parser p, ast.Kind kind) {
    var optional = p.parseOptionalTokenJSDoc(kind);
    if (optional == nil) {
        if (!isKeywordOrPunctuation(kind)) {
            throw panic("expected keyword or punctuation");
        }
        p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(kind));
        optional = p.finishNode(p.factory.NewToken(kind), p.nodePos());
    }
    return optional;
}

internal static ж<ast.Node> parseOptionalTokenJSDoc(this ref Parser p, ast.Kind kind) {
    if (p.token == kind) {
        return p.parseTokenNode();
    }
    return default;
}

internal static ж<astꓸStatement> parseStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindSemicolonToken: {
        return Ꮡp.parseEmptyStatement();
    }
    case var ᴛ when ᴛ == ast.KindOpenBraceToken: {
        return Ꮡp.parseBlock(false, nil);
    }
    case var ᴛ when ᴛ == ast.KindVarKeyword: {
        return Ꮡp.parseVariableStatement(p.nodePos(), p.jsdocScannerInfo(), nil);
    }
    case var ᴛ when ᴛ == ast.KindLetKeyword: {
        if (Ꮡp.isLetDeclaration()) {
            return Ꮡp.parseVariableStatement(p.nodePos(), p.jsdocScannerInfo(), nil);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindAwaitKeyword: {
        if (Ꮡp.isAwaitUsingDeclaration()) {
            return Ꮡp.parseVariableStatement(p.nodePos(), p.jsdocScannerInfo(), nil);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindUsingKeyword: {
        if (Ꮡp.isUsingDeclaration()) {
            return Ꮡp.parseVariableStatement(p.nodePos(), p.jsdocScannerInfo(), nil);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionKeyword: {
        return Ꮡp.parseFunctionDeclaration(p.nodePos(), p.jsdocScannerInfo(), nil);
    }
    case var ᴛ when ᴛ == ast.KindClassKeyword: {
        return Ꮡp.parseClassDeclaration(p.nodePos(), p.jsdocScannerInfo(), nil);
    }
    case var ᴛ when ᴛ == ast.KindIfKeyword: {
        return Ꮡp.parseIfStatement();
    }
    case var ᴛ when ᴛ == ast.KindDoKeyword: {
        return Ꮡp.parseDoStatement();
    }
    case var ᴛ when ᴛ == ast.KindWhileKeyword: {
        return Ꮡp.parseWhileStatement();
    }
    case var ᴛ when ᴛ == ast.KindForKeyword: {
        return Ꮡp.parseForOrForInOrForOfStatement();
    }
    case var ᴛ when ᴛ == ast.KindContinueKeyword: {
        return Ꮡp.parseContinueStatement();
    }
    case var ᴛ when ᴛ == ast.KindBreakKeyword: {
        return Ꮡp.parseBreakStatement();
    }
    case var ᴛ when ᴛ == ast.KindReturnKeyword: {
        return Ꮡp.parseReturnStatement();
    }
    case var ᴛ when ᴛ == ast.KindWithKeyword: {
        return Ꮡp.parseWithStatement();
    }
    case var ᴛ when ᴛ == ast.KindSwitchKeyword: {
        return Ꮡp.parseSwitchStatement();
    }
    case var ᴛ when ᴛ == ast.KindThrowKeyword: {
        return Ꮡp.parseThrowStatement();
    }
    case var ᴛ when ᴛ == ast.KindTryKeyword || ᴛ == ast.KindCatchKeyword || ᴛ == ast.KindFinallyKeyword: {
        return Ꮡp.parseTryStatement();
    }
    case var ᴛ when ᴛ == ast.KindDebuggerKeyword: {
        return Ꮡp.parseDebuggerStatement();
    }
    case var ᴛ when ᴛ == ast.KindAtToken: {
        return Ꮡp.parseDeclaration();
    }
    case var ᴛ when ᴛ == ast.KindAsyncKeyword || ᴛ == ast.KindInterfaceKeyword || ᴛ == ast.KindTypeKeyword || ᴛ == ast.KindModuleKeyword || ᴛ == ast.KindNamespaceKeyword || ᴛ == ast.KindDeclareKeyword || ᴛ == ast.KindConstKeyword || ᴛ == ast.KindEnumKeyword || ᴛ == ast.KindExportKeyword || ᴛ == ast.KindImportKeyword || ᴛ == ast.KindPrivateKeyword || ᴛ == ast.KindProtectedKeyword || ᴛ == ast.KindPublicKeyword || ᴛ == ast.KindAbstractKeyword || ᴛ == ast.KindAccessorKeyword || ᴛ == ast.KindStaticKeyword || ᴛ == ast.KindReadonlyKeyword || ᴛ == ast.KindGlobalKeyword: {
        if (Ꮡp.isStartOfDeclaration()) {
            return Ꮡp.parseDeclaration();
        }
        break;
    }}

    return Ꮡp.parseExpressionOrLabeledStatement();
}

internal static ж<astꓸStatement> parseDeclaration(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var modifiers = Ꮡp.parseModifiersEx(true, false, false);
    var isAmbient = modifiers != nil && core.Some<ж<ast.Node>>(modifiers.Nodes, isDeclareModifier);
    if (isAmbient) {
        foreach (var (_, m) in range(modifiers.Nodes)) {
            m.Flags |= (ast.NodeFlags)(ast.NodeFlagsAmbient);
        }
        var saveContextFlags = p.contextFlags;
        p.setContextFlags(ast.NodeFlagsAmbient, true);
        var result = Ꮡp.parseDeclarationWorker(pos, jsdoc, modifiers);
        p.contextFlags = saveContextFlags;
        return result;
    } else {
        return Ꮡp.parseDeclarationWorker(pos, jsdoc, modifiers);
    }
}

internal static ж<astꓸStatement> parseDeclarationWorker(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindVarKeyword || ᴛ == ast.KindLetKeyword || ᴛ == ast.KindConstKeyword || ᴛ == ast.KindUsingKeyword: {
        return Ꮡp.parseVariableStatement(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindAwaitKeyword: {
        if (Ꮡp.isAwaitUsingDeclaration()) {
            return Ꮡp.parseVariableStatement(pos, jsdoc, modifiers);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionKeyword: {
        return Ꮡp.parseFunctionDeclaration(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindClassKeyword: {
        return Ꮡp.parseClassDeclaration(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindInterfaceKeyword: {
        return Ꮡp.parseInterfaceDeclaration(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindTypeKeyword: {
        return Ꮡp.parseTypeAliasDeclaration(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindEnumKeyword: {
        return Ꮡp.parseEnumDeclaration(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindGlobalKeyword || ᴛ == ast.KindModuleKeyword || ᴛ == ast.KindNamespaceKeyword: {
        return Ꮡp.parseModuleDeclaration(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindImportKeyword: {
        return Ꮡp.parseImportDeclarationOrImportEqualsDeclaration(pos, jsdoc, modifiers);
    }
    case var ᴛ when ᴛ == ast.KindExportKeyword: {
        p.nextToken();
        switch (p.token) {
        case var ᴛ2 when ᴛ2 == ast.KindDefaultKeyword || ᴛ2 == ast.KindEqualsToken: {
            return Ꮡp.parseExportAssignment(pos, jsdoc, modifiers);
        }
        case var ᴛ2 when ᴛ2 == ast.KindAsKeyword: {
            return Ꮡp.parseNamespaceExportDeclaration(pos, jsdoc, modifiers);
        }
        default: {
            return Ꮡp.parseExportDeclaration(pos, jsdoc, modifiers);
        }}

        break;
    }}

    if (modifiers != nil) {
        p.parseErrorAt(p.nodePos(), p.nodePos(), diagnostics.Declaration_expected);
        return p.finishNode(p.factory.NewMissingDeclaration(modifiers), pos);
    }
    throw panic("Unhandled case in parseDeclarationWorker");
}

} partial class parser_package {

internal static bool isDeclareModifier(ж<ast.Node> Ꮡmodifier) {
    ref var modifier = ref Ꮡmodifier.DerefOrNull();

    return modifier.Kind == ast.KindDeclareKeyword;
}

} partial class parserᴛParser {

internal static bool isLetDeclaration(this ж<Parser> Ꮡp) {
    return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsBindingIdentifierOrStartOfDestructuring)));
}

internal static bool nextTokenIsBindingIdentifierOrStartOfDestructuring(this ref Parser p) {
    p.nextToken();
    return p.isBindingIdentifier() || p.token == ast.KindOpenBraceToken || p.token == ast.KindOpenBracketToken;
}

internal static ж<ast.Node> parseBlock(this ж<Parser> Ꮡp, bool ignoreMissingOpenBrace, ж<diagnostics.Message> diagnosticMessage) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    nint openBracePosition = p.scanner.TokenStart();
    var openBraceParsed = p.parseExpectedWithDiagnostic(ast.KindOpenBraceToken, diagnosticMessage, true);
    var multiline = false;
    if (openBraceParsed || ignoreMissingOpenBrace) {
        multiline = p.hasPrecedingLineBreak();
        var statements = Ꮡp.parseList(PCBlockStatements, ((Func<ж<Parser>, ж<astꓸStatement>>)(parserᴛParser.parseStatement)));
        p.parseExpectedMatchingBrackets(ast.KindOpenBraceToken, ast.KindCloseBraceToken, openBraceParsed, openBracePosition);
        var resultΔ1 = p.finishNode(p.factory.NewBlock(statements, multiline), pos);
        Ꮡp.withJSDoc(resultΔ1, jsdoc);
        if (p.token == ast.KindEqualsToken) {
            p.parseErrorAtCurrentToken(diagnostics.Declaration_or_statement_expected_This_follows_a_block_of_statements_so_if_you_intended_to_write_a_destructuring_assignment_you_might_need_to_wrap_the_whole_assignment_in_parentheses);
            p.nextToken();
        }
        return resultΔ1;
    }
    var result = p.finishNode(p.factory.NewBlock(p.createMissingList(), multiline), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseEmptyStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindSemicolonToken);
    var result = p.finishNode(p.factory.NewEmptyStatement(), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseIfStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindIfKeyword);
    nint openParenPosition = p.scanner.TokenStart();
    var openParenParsed = p.parseExpected(ast.KindOpenParenToken);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpectedMatchingBrackets(ast.KindOpenParenToken, ast.KindCloseParenToken, openParenParsed, openParenPosition);
    var thenStatement = Ꮡp.parseStatement();
    ж<astꓸStatement> elseStatement = default;
    if (p.parseOptional(ast.KindElseKeyword)) {
        elseStatement = Ꮡp.parseStatement();
    }
    var result = p.finishNode(p.factory.NewIfStatement(expression, thenStatement, elseStatement), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseDoStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindDoKeyword);
    var statement = Ꮡp.parseStatement();
    p.parseExpected(ast.KindWhileKeyword);
    nint openParenPosition = p.scanner.TokenStart();
    var openParenParsed = p.parseExpected(ast.KindOpenParenToken);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpectedMatchingBrackets(ast.KindOpenParenToken, ast.KindCloseParenToken, openParenParsed, openParenPosition);
    p.parseOptional(ast.KindSemicolonToken);
    var result = p.finishNode(p.factory.NewDoStatement(statement, expression), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseWhileStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindWhileKeyword);
    nint openParenPosition = p.scanner.TokenStart();
    var openParenParsed = p.parseExpected(ast.KindOpenParenToken);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpectedMatchingBrackets(ast.KindOpenParenToken, ast.KindCloseParenToken, openParenParsed, openParenPosition);
    var statement = Ꮡp.parseStatement();
    var result = p.finishNode(p.factory.NewWhileStatement(expression, statement), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseForOrForInOrForOfStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindForKeyword);
    var awaitToken = p.parseOptionalToken(ast.KindAwaitKeyword);
    p.parseExpected(ast.KindOpenParenToken);
    ж<astꓸForInitializer> initializer = default;
    if (p.token != ast.KindSemicolonToken) {
        if (p.token == ast.KindVarKeyword || p.token == ast.KindLetKeyword || p.token == ast.KindConstKeyword || p.token == ast.KindUsingKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsBindingIdentifierOrStartOfDestructuringOnSameLineDisallowOf))) || p.token == ast.KindAwaitKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsUsingKeywordThenBindingIdentifierOrStartOfObjectDestructuringOnSameLine)))) {
            initializer = Ꮡp.parseVariableDeclarationList(true);
        } else {
            initializer = doInContext<ж<astꓸExpression>>(Ꮡp, ast.NodeFlagsDisallowInContext, true, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseExpression)));
        }
    }
    ж<astꓸStatement> result = default;
    switch (ᐧ) {
    case {} when awaitToken != nil && p.parseExpected(ast.KindOfKeyword) || awaitToken == nil && p.parseOptional(ast.KindOfKeyword): {
        var expression = doInContext<ж<astꓸExpression>>(Ꮡp, ast.NodeFlagsDisallowInContext, false, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseAssignmentExpressionOrHigher)));
        p.parseExpected(ast.KindCloseParenToken);
        result = p.factory.NewForInOrOfStatement(ast.KindForOfStatement, awaitToken, initializer, expression, Ꮡp.parseStatement());
        break;
    }
    case {} when p.parseOptional(ast.KindInKeyword): {
        var expression = Ꮡp.parseExpressionAllowIn();
        p.parseExpected(ast.KindCloseParenToken);
        result = p.factory.NewForInOrOfStatement(ast.KindForInStatement, nil, initializer, expression, Ꮡp.parseStatement());
        break;
    }
    default: {
        p.parseExpected(ast.KindSemicolonToken);
        ж<astꓸExpression> condition = default;
        if (p.token != ast.KindSemicolonToken && p.token != ast.KindCloseParenToken) {
            condition = Ꮡp.parseExpressionAllowIn();
        }
        p.parseExpected(ast.KindSemicolonToken);
        ж<astꓸExpression> incrementor = default;
        if (p.token != ast.KindCloseParenToken) {
            incrementor = Ꮡp.parseExpressionAllowIn();
        }
        p.parseExpected(ast.KindCloseParenToken);
        result = p.factory.NewForStatement(initializer, condition, incrementor, Ꮡp.parseStatement());
        break;
    }}

    p.finishNode(result, pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseBreakStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindBreakKeyword);
    var label = p.parseIdentifierUnlessAtSemicolon();
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewBreakStatement(label), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseContinueStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindContinueKeyword);
    var label = p.parseIdentifierUnlessAtSemicolon();
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewContinueStatement(label), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseIdentifierUnlessAtSemicolon(this ref Parser p) {
    if (!p.canParseSemicolon()) {
        return p.parseIdentifier();
    }
    return default;
}

internal static ж<ast.Node> parseReturnStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindReturnKeyword);
    ж<astꓸExpression> expression = default;
    if (!p.canParseSemicolon()) {
        expression = Ꮡp.parseExpressionAllowIn();
    }
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewReturnStatement(expression), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseWithStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindWithKeyword);
    nint openParenPosition = p.scanner.TokenStart();
    var openParenParsed = p.parseExpected(ast.KindOpenParenToken);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpectedMatchingBrackets(ast.KindOpenParenToken, ast.KindCloseParenToken, openParenParsed, openParenPosition);
    var statement = doInContext<ж<astꓸStatement>>(Ꮡp, ast.NodeFlagsInWithStatement, true, ((Func<ж<Parser>, ж<astꓸStatement>>)(parserᴛParser.parseStatement)));
    var result = p.finishNode(p.factory.NewWithStatement(expression, statement), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseCaseClause(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindCaseKeyword);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpected(ast.KindColonToken);
    var statements = Ꮡp.parseList(PCSwitchClauseStatements, ((Func<ж<Parser>, ж<astꓸStatement>>)(parserᴛParser.parseStatement)));
    var result = p.finishNode(p.factory.NewCaseOrDefaultClause(ast.KindCaseClause, expression, statements), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseDefaultClause(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindDefaultKeyword);
    p.parseExpected(ast.KindColonToken);
    var statements = Ꮡp.parseList(PCSwitchClauseStatements, ((Func<ж<Parser>, ж<astꓸStatement>>)(parserᴛParser.parseStatement)));
    var result = p.finishNode(p.factory.NewCaseOrDefaultClause(ast.KindDefaultClause, nil, statements), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseCaseOrDefaultClause(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindCaseKeyword) {
        return Ꮡp.parseCaseClause();
    }
    return Ꮡp.parseDefaultClause();
}

internal static ж<ast.Node> parseCaseBlock(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindOpenBraceToken);
    var clauses = Ꮡp.parseList(PCSwitchClauses, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseCaseOrDefaultClause)));
    p.parseExpected(ast.KindCloseBraceToken);
    var result = p.finishNode(p.factory.NewCaseBlock(clauses), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseSwitchStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindSwitchKeyword);
    p.parseExpected(ast.KindOpenParenToken);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpected(ast.KindCloseParenToken);
    var caseBlock = Ꮡp.parseCaseBlock();
    var result = p.finishNode(p.factory.NewSwitchStatement(expression, caseBlock), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseThrowStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindThrowKeyword);
    ж<astꓸExpression> expression = default;
    if (!p.hasPrecedingLineBreak()) {
        expression = Ꮡp.parseExpressionAllowIn();
    } else {
        expression = p.createMissingIdentifier();
    }
    if (!p.tryParseSemicolon()) {
        p.parseErrorForMissingSemicolonAfter(expression);
    }
    var result = p.finishNode(p.factory.NewThrowStatement(expression), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseTryStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindTryKeyword);
    var tryBlock = Ꮡp.parseBlock(false, nil);
    ж<ast.Node> catchClause = default;
    if (p.token == ast.KindCatchKeyword) {
        catchClause = Ꮡp.parseCatchClause();
    }
    ж<ast.Node> finallyBlock = default;
    if (catchClause == nil || p.token == ast.KindFinallyKeyword) {
        p.parseExpectedWithDiagnostic(ast.KindFinallyKeyword, diagnostics.X_catch_or_finally_expected, true);
        finallyBlock = Ꮡp.parseBlock(false, nil);
    }
    var result = p.finishNode(p.factory.NewTryStatement(tryBlock, catchClause, finallyBlock), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseCatchClause(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindCatchKeyword);
    ж<ast.Node> variableDeclaration = default;
    if (p.parseOptional(ast.KindOpenParenToken)) {
        variableDeclaration = Ꮡp.parseVariableDeclaration();
        p.parseExpected(ast.KindCloseParenToken);
    }
    var block = Ꮡp.parseBlock(false, nil);
    var result = p.finishNode(p.factory.NewCatchClause(variableDeclaration, block), pos);
    return result;
}

internal static ж<ast.Node> parseDebuggerStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindDebuggerKeyword);
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewDebuggerStatement(), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<astꓸStatement> parseExpressionOrLabeledStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var hasParen = p.token == ast.KindOpenParenToken;
    var expression = Ꮡp.parseExpression();

    if (expression.Kind == ast.KindIdentifier && p.parseOptional(ast.KindColonToken)) {
        var resultΔ1 = p.finishNode(p.factory.NewLabeledStatement(expression, Ꮡp.parseStatement()), pos);
        Ꮡp.withJSDoc(resultΔ1, jsdoc);
        return resultΔ1;
    }

    if (!p.tryParseSemicolon()) {
        p.parseErrorForMissingSemicolonAfter(expression);
    }
    var result = p.finishNode(p.factory.NewExpressionStatement(expression), pos);
    if (hasParen) {
        jsdoc &= unchecked((jsdocScannerInfo)~(jsdocScannerInfo)(jsdocScannerInfoHasJSDoc));
    }
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseVariableStatement(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var declarationList = Ꮡp.parseVariableDeclarationList(false);
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewVariableStatement(modifiers, declarationList), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.Node> parseVariableDeclarationList(this ж<Parser> Ꮡp, bool inForStatementInitializer) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    ast.NodeFlags flags = default;
    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindVarKeyword: {
        flags = ast.NodeFlagsNone;
        break;
    }
    case var ᴛ when ᴛ == ast.KindLetKeyword: {
        flags = ast.NodeFlagsLet;
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstKeyword: {
        flags = ast.NodeFlagsConst;
        break;
    }
    case var ᴛ when ᴛ == ast.KindUsingKeyword: {
        flags = ast.NodeFlagsUsing;
        break;
    }
    case var ᴛ when ᴛ == ast.KindAwaitKeyword: {
        if (!Ꮡp.isAwaitUsingDeclaration()) {
            break;
        }
        flags = ast.NodeFlagsAwaitUsing;
        p.nextToken();
        break;
    }
    default: {
        throw panic("Unhandled case in parseVariableDeclarationList");
        break;
    }}

    p.nextToken();
    ж<ast.NodeList> declarations = default;
    if (p.token == ast.KindOfKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsIdentifierAndCloseParen)))) {
        declarations = p.createMissingList();
    } else {
        var saveContextFlags = p.contextFlags;
        p.setContextFlags(ast.NodeFlagsDisallowInContext, inForStatementInitializer);
        declarations = Ꮡp.parseDelimitedList(PCVariableDeclarations, core.IfElse<Func<ж<Parser>, ж<ast.Node>>>(inForStatementInitializer, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseVariableDeclaration)), ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseVariableDeclarationAllowExclamation))));
        p.contextFlags = saveContextFlags;
    }
    var result = p.finishNode(p.factory.NewVariableDeclarationList(declarations, flags), pos);
    return result;
}

internal static bool nextIsIdentifierAndCloseParen(this ref Parser p) {
    return p.nextTokenIsIdentifier() && p.nextToken() == ast.KindCloseParenToken;
}

internal static bool nextTokenIsIdentifier(this ref Parser p) {
    p.nextToken();
    return p.isIdentifier();
}

internal static ж<ast.Node> parseVariableDeclaration(this ж<Parser> Ꮡp) {
    return Ꮡp.parseVariableDeclarationWorker(false);
}

internal static ж<ast.Node> parseVariableDeclarationAllowExclamation(this ж<Parser> Ꮡp) {
    return Ꮡp.parseVariableDeclarationWorker(true);
}

internal static ж<ast.Node> parseVariableDeclarationWorker(this ж<Parser> Ꮡp, bool allowExclamation) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var name = Ꮡp.parseIdentifierOrPatternWithDiagnostic(diagnostics.Private_identifiers_are_not_allowed_in_variable_declarations);
    ж<ast.Node> exclamationToken = default;
    if (allowExclamation && name.Kind == ast.KindIdentifier && p.token == ast.KindExclamationToken && !p.hasPrecedingLineBreak()) {
        exclamationToken = p.parseTokenNode();
    }
    var typeNode = Ꮡp.parseTypeAnnotation();
    ж<astꓸExpression> initializer = default;
    if (p.token != ast.KindInKeyword && p.token != ast.KindOfKeyword) {
        initializer = Ꮡp.parseInitializer();
    }
    var result = p.finishNode(p.factory.NewVariableDeclaration(name, exclamationToken, typeNode, initializer), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.Node> parseIdentifierOrPattern(this ж<Parser> Ꮡp) {
    return Ꮡp.parseIdentifierOrPatternWithDiagnostic(nil);
}

internal static ж<ast.Node> parseIdentifierOrPatternWithDiagnostic(this ж<Parser> Ꮡp, ж<diagnostics.Message> privateIdentifierDiagnosticMessage) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindOpenBracketToken) {
        return Ꮡp.parseArrayBindingPattern();
    }
    if (p.token == ast.KindOpenBraceToken) {
        return Ꮡp.parseObjectBindingPattern();
    }
    return p.parseBindingIdentifierWithDiagnostic(privateIdentifierDiagnosticMessage);
}

internal static ж<ast.Node> parseArrayBindingPattern(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindOpenBracketToken);
    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsDisallowInContext, false);
    var elements = Ꮡp.parseDelimitedList(PCArrayBindingElements, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseArrayBindingElement)));
    p.contextFlags = saveContextFlags;
    p.parseExpected(ast.KindCloseBracketToken);
    return p.finishNode(p.factory.NewBindingPattern(ast.KindArrayBindingPattern, elements), pos);
}

internal static ж<ast.Node> parseArrayBindingElement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    ж<ast.Node> dotDotDotToken = default;
    ж<ast.Node> name = default;
    ж<astꓸExpression> initializer = default;
    if (p.token != ast.KindCommaToken) {
        dotDotDotToken = p.parseOptionalToken(ast.KindDotDotDotToken);
        name = Ꮡp.parseIdentifierOrPattern();
        initializer = Ꮡp.parseInitializer();
    }
    return p.finishNode(p.factory.NewBindingElement(dotDotDotToken, nil, name, initializer), pos);
}

internal static ж<ast.Node> parseObjectBindingPattern(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindOpenBraceToken);
    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsDisallowInContext, false);
    var elements = Ꮡp.parseDelimitedList(PCObjectBindingElements, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseObjectBindingElement)));
    p.contextFlags = saveContextFlags;
    p.parseExpected(ast.KindCloseBraceToken);
    return p.finishNode(p.factory.NewBindingPattern(ast.KindObjectBindingPattern, elements), pos);
}

internal static ж<ast.Node> parseObjectBindingElement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var dotDotDotToken = p.parseOptionalToken(ast.KindDotDotDotToken);
    var tokenIsIdentifier = p.isBindingIdentifier();
    var propertyName = Ꮡp.parsePropertyName();
    ж<ast.Node> name = default;
    if (tokenIsIdentifier && p.token != ast.KindColonToken) {
        name = propertyName;
        propertyName = default;
    } else {
        p.parseExpected(ast.KindColonToken);
        name = Ꮡp.parseIdentifierOrPattern();
    }
    var initializer = Ꮡp.parseInitializer();
    return p.finishNode(p.factory.NewBindingElement(dotDotDotToken, propertyName, name, initializer), pos);
}

internal static ж<astꓸExpression> parseInitializer(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.parseOptional(ast.KindEqualsToken)) {
        return Ꮡp.parseAssignmentExpressionOrHigher();
    }
    return default;
}

internal static ж<astꓸTypeNode> parseTypeAnnotation(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.parseOptional(ast.KindColonToken)) {
        return Ꮡp.parseType();
    }
    return default;
}

internal static ж<ast.Node> parseFunctionDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> Ꮡmodifiers) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    p.parseExpected(ast.KindFunctionKeyword);
    var asteriskToken = p.parseOptionalToken(ast.KindAsteriskToken);
    ж<ast.Node> name = default;
    if (Ꮡmodifiers == nil || (ast.ModifierFlags)(modifiers.ModifierFlags & ast.ModifierFlagsDefault) == 0 || p.isBindingIdentifier()) {
        name = p.parseBindingIdentifier();
    }
    var signatureFlags = (ParseFlags)(core.IfElse(asteriskToken != nil, ParseFlagsYield, ParseFlagsNone) | core.IfElse(Ꮡmodifiers != nil && (ast.ModifierFlags)(modifiers.ModifierFlags & ast.ModifierFlagsAsync) != 0, ParseFlagsAwait, ParseFlagsNone));
    var typeParameters = Ꮡp.parseTypeParameters();
    var saveContextFlags = p.contextFlags;
    if (Ꮡmodifiers != nil && (ast.ModifierFlags)(modifiers.ModifierFlags & ast.ModifierFlagsExport) != 0) {
        p.setContextFlags(ast.NodeFlagsAwaitContext, true);
    }
    var parameters = Ꮡp.parseParameters(signatureFlags);
    var returnType = Ꮡp.parseReturnType(ast.KindColonToken, false);
    var body = Ꮡp.parseFunctionBlockOrSemicolon(signatureFlags, diagnostics.X_or_expected);
    p.contextFlags = saveContextFlags;
    var result = p.finishNode(p.factory.NewFunctionDeclaration(Ꮡmodifiers, asteriskToken, name, typeParameters, parameters, returnType, nil, body), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.Node> parseClassDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    return Ꮡp.parseClassDeclarationOrExpression(pos, jsdoc, modifiers, ast.KindClassDeclaration);
}

internal static ж<ast.Node> parseClassExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return Ꮡp.parseClassDeclarationOrExpression(p.nodePos(), p.jsdocScannerInfo(), nil, ast.KindClassExpression);
}

internal static ж<ast.Node> parseClassDeclarationOrExpression(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> Ꮡmodifiers, ast.Kind kind) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    p.parseExpected(ast.KindClassKeyword);
    var name = Ꮡp.parseNameOfClassDeclarationOrExpression();
    var typeParameters = Ꮡp.parseTypeParameters();
    if (Ꮡmodifiers != nil && (ParsingContexts)(p.parsingContexts & ((ParsingContexts)(nint)(1 << (int)(PCSourceElements)))) != 0 && (ParsingContexts)(p.parsingContexts & ((ParsingContexts)((1 << (int)(PCBlockStatements)) | (1 << (int)(PCSwitchClauseStatements))))) == 0 && core.Some<ж<ast.Node>>(modifiers.Nodes, isExportModifier)) {
        p.setContextFlags(ast.NodeFlagsAwaitContext, true);
    }
    var heritageClauses = Ꮡp.parseHeritageClauses(false);
    ж<ast.NodeList> members = default;
    if (p.parseExpected(ast.KindOpenBraceToken)) {
        members = Ꮡp.parseList(PCClassMembers, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseClassElement)));
        p.parseExpected(ast.KindCloseBraceToken);
    } else {
        members = p.createMissingList();
    }
    p.contextFlags = saveContextFlags;
    ж<ast.Node> result = default;
    if (Ꮡmodifiers != nil && (ast.ModifierFlags)(ast.ModifiersToFlags(modifiers.Nodes) & ast.ModifierFlagsAmbient) != 0) {
        p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    }
    if (kind == ast.KindClassDeclaration) {
        result = p.factory.NewClassDeclaration(Ꮡmodifiers, name, typeParameters, heritageClauses, members);
    } else {
        result = p.factory.NewClassExpression(Ꮡmodifiers, name, typeParameters, heritageClauses, members);
    }
    p.finishNode(result, pos);
    Ꮡp.withJSDoc(result, jsdoc);
    if ((ast.NodeFlags)(result.Flags & ast.NodeFlagsJavaScriptFile) != 0) {
        p.checkJSSyntax(result);
        if (heritageClauses != nil) {
            foreach (var (_, clause) in range(heritageClauses.Nodes)) {
                if (clause.AsHeritageClause().Token == ast.KindExtendsKeyword) {
                    foreach (var (_, expr) in range(clause.AsHeritageClause().Types.Nodes)) {
                        p.checkJSSyntax(expr);
                    }
                }
            }
        }
    }
    return result;
}

internal static ж<ast.Node> parseNameOfClassDeclarationOrExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.isBindingIdentifier() && !Ꮡp.isImplementsClause()) {
        var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
        var id = p.createIdentifier(p.isBindingIdentifier());
        p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
        return id;
    }
    return default;
}

internal static bool isImplementsClause(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.token == ast.KindImplementsKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeyword)));
}

} partial class parser_package {

internal static bool isExportModifier(ж<ast.Node> Ꮡmodifier) {
    ref var modifier = ref Ꮡmodifier.DerefOrNull();

    return modifier.Kind == ast.KindExportKeyword;
}

internal static bool isAsyncModifier(ж<ast.Node> Ꮡmodifier) {
    ref var modifier = ref Ꮡmodifier.DerefOrNull();

    return modifier.Kind == ast.KindAsyncKeyword;
}

} partial class parserᴛParser {

internal static ж<ast.NodeList> parseHeritageClauses(this ж<Parser> Ꮡp, bool isInterface) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.isHeritageClause()) {
        return Ꮡp.parseList(PCHeritageClauses, (ж<Parser> pΔ1) => pΔ1.parseHeritageClause(isInterface));
    }
    return default;
}

internal static ж<ast.Node> parseHeritageClause(this ж<Parser> Ꮡp, bool isInterface) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var kind = p.token;
    p.nextToken();
    var parseElement = ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseExpressionWithTypeArguments));
    if (isTypeHeritageClause(isInterface, kind)) {
        parseElement = ((Func<ж<Parser>, ж<astꓸHeritageClauseElement>>)(parserᴛParser.parseTypeHeritageClauseElement));
    }
    var types = Ꮡp.parseDelimitedList(PCHeritageClauseElement, parseElement);
    return p.checkJSSyntax(p.finishNode(p.factory.NewHeritageClause(kind, types), pos));
}

} partial class parser_package {

internal static bool isTypeHeritageClause(bool isInterface, ast.Kind token) {
    return isInterface && token == ast.KindExtendsKeyword || !isInterface && token == ast.KindImplementsKeyword;
}

} partial class parserᴛParser {

internal static ж<astꓸHeritageClauseElement> parseTypeHeritageClauseElement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var expressionWithTypeArguments = Ꮡp.parseExpressionWithTypeArguments().AsExpressionWithTypeArguments();
    if (!isValidHeritageTypeReferenceExpression((~expressionWithTypeArguments).Expression)) {
        return expressionWithTypeArguments.of(ast.ExpressionWithTypeArguments.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    var typeName = p.convertEntityNameExpressionToEntityName((~expressionWithTypeArguments).Expression);
    return p.finishNode(p.factory.NewTypeReferenceNode(typeName, (~expressionWithTypeArguments).TypeArguments), pos);
}

} partial class parser_package {

internal static bool isValidHeritageTypeReferenceExpression(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsIdentifier(Ꮡnode)) {
        return ast.NodeIsPresent(Ꮡnode);
    }
    return ast.IsPropertyAccessExpression(Ꮡnode) && !ast.IsOptionalChain(Ꮡnode) && ast.NodeIsPresent(node.Name()) && isValidHeritageTypeReferenceExpression(node.Expression());
}

} partial class parserᴛParser {

internal static ж<ast.Node> convertEntityNameExpressionToEntityName(this ref Parser p, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsIdentifier(Ꮡnode)) {
        return Ꮡnode;
    }
    var propertyAccess = node.AsPropertyAccessExpression();
    var result = p.factory.NewQualifiedName(
        p.convertEntityNameExpressionToEntityName((~propertyAccess).Expression),
        propertyAccess.Name());
    return p.finishNodeWithEnd(result, node.Pos(), node.End());
}

internal static ж<ast.Node> parseExpressionWithTypeArguments(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var expression = Ꮡp.parseLeftHandSideExpressionOrHigher();
    if (ast.IsExpressionWithTypeArguments(expression)) {
        return expression;
    }
    var typeArguments = Ꮡp.parseTypeArguments();
    return p.finishNode(p.factory.NewExpressionWithTypeArguments(expression, typeArguments), pos);
}

internal static ж<ast.Node> parseClassElement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    if (p.token == ast.KindSemicolonToken) {
        p.nextToken();
        var result = p.finishNode(p.factory.NewSemicolonClassElement(), pos);
        Ꮡp.withJSDoc(result, jsdoc);
        return result;
    }
    var modifiers = Ꮡp.parseModifiersEx(true, true, true);
    if (p.token == ast.KindStaticKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOpenBrace)))) {
        return Ꮡp.parseClassStaticBlockDeclaration(pos, jsdoc, modifiers);
    }
    if (Ꮡp.parseContextualModifier(ast.KindGetKeyword)) {
        return Ꮡp.parseAccessorDeclaration(pos, jsdoc, modifiers, ast.KindGetAccessor, ParseFlagsNone);
    }
    if (Ꮡp.parseContextualModifier(ast.KindSetKeyword)) {
        return Ꮡp.parseAccessorDeclaration(pos, jsdoc, modifiers, ast.KindSetAccessor, ParseFlagsNone);
    }
    if (p.token == ast.KindConstructorKeyword || p.token == ast.KindStringLiteral) {
        var constructorDeclaration = Ꮡp.tryParseConstructorDeclaration(pos, jsdoc, modifiers);
        if (constructorDeclaration != nil) {
            return constructorDeclaration;
        }
    }
    if (Ꮡp.isIndexSignature()) {
        return p.checkJSSyntax(Ꮡp.parseIndexSignatureDeclaration(pos, jsdoc, modifiers));
    }
    if (tokenIsIdentifierOrKeyword(p.token) || p.token == ast.KindStringLiteral || p.token == ast.KindNumericLiteral || p.token == ast.KindBigIntLiteral || p.token == ast.KindAsteriskToken || p.token == ast.KindOpenBracketToken) {
        var isAmbient = modifiers != nil && core.Some<ж<ast.Node>>(modifiers.Nodes, isDeclareModifier);
        if (isAmbient) {
            foreach (var (_, m) in range(modifiers.Nodes)) {
                m.Flags |= (ast.NodeFlags)(ast.NodeFlagsAmbient);
            }
            var saveContextFlags = p.contextFlags;
            p.setContextFlags(ast.NodeFlagsAmbient, true);
            var result = Ꮡp.parsePropertyOrMethodDeclaration(pos, jsdoc, modifiers);
            p.contextFlags = saveContextFlags;
            return result;
        } else {
            return Ꮡp.parsePropertyOrMethodDeclaration(pos, jsdoc, modifiers);
        }
    }
    if (modifiers != nil) {
        p.parseErrorAt(p.nodePos(), p.nodePos(), diagnostics.Declaration_expected);
        var name = p.createMissingIdentifier();
        return Ꮡp.parsePropertyDeclaration(pos, jsdoc, modifiers, name, nil);
    }
    throw panic("Should not have attempted to parse class member declaration.");
}

internal static ж<ast.Node> parseClassStaticBlockDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parseExpectedToken(ast.KindStaticKeyword);
    var body = Ꮡp.parseClassStaticBlockBody();
    var result = p.finishNode(p.factory.NewClassStaticBlockDeclaration(modifiers, body), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseClassStaticBlockBody(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsYieldContext, false);
    p.setContextFlags(ast.NodeFlagsAwaitContext, true);
    var body = Ꮡp.parseBlock(false, nil);
    p.contextFlags = saveContextFlags;
    return body;
}

internal static ж<ast.Node> tryParseConstructorDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.mark();
    if (p.token == ast.KindConstructorKeyword || p.token == ast.KindStringLiteral && p.scanner.TokenValue() == "constructor"u8 && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOpenParen)))) {
        p.nextToken();
        var typeParameters = Ꮡp.parseTypeParameters();
        var parameters = Ꮡp.parseParameters(ParseFlagsNone);
        var returnType = Ꮡp.parseReturnType(ast.KindColonToken, false);
        var body = Ꮡp.parseFunctionBlockOrSemicolon(ParseFlagsNone, diagnostics.X_or_expected);
        var result = p.finishNode(p.factory.NewConstructorDeclaration(modifiers, typeParameters, parameters, returnType, nil, body), pos);
        Ꮡp.withJSDoc(result, jsdoc);
        p.checkJSSyntax(result);
        return result;
    }
    p.rewind(state);
    return default;
}

internal static bool nextTokenIsOpenParen(this ref Parser p) {
    return p.nextToken() == ast.KindOpenParenToken;
}

internal static ж<ast.Node> parsePropertyOrMethodDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var asteriskToken = p.parseOptionalToken(ast.KindAsteriskToken);
    var name = Ꮡp.parsePropertyName();
    var questionToken = p.parseOptionalToken(ast.KindQuestionToken);
    if (asteriskToken != nil || p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken) {
        return Ꮡp.parseMethodDeclaration(pos, jsdoc, modifiers, asteriskToken, name, questionToken, diagnostics.X_or_expected);
    }
    return Ꮡp.parsePropertyDeclaration(pos, jsdoc, modifiers, name, questionToken);
}

internal static ж<ast.Node> parseMethodDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers, ж<ast.Node> asteriskToken, ж<ast.Node> name, ж<ast.Node> questionToken, ж<diagnostics.Message> diagnosticMessage) {
    ref var p = ref Ꮡp.DerefOrNull();

    var signatureFlags = (ParseFlags)(core.IfElse(asteriskToken != nil, ParseFlagsYield, ParseFlagsNone) | core.IfElse(modifierListHasAsync(modifiers), ParseFlagsAwait, ParseFlagsNone));
    var typeParameters = Ꮡp.parseTypeParameters();
    var parameters = Ꮡp.parseParameters(signatureFlags);
    var typeNode = Ꮡp.parseReturnType(ast.KindColonToken, false);
    var body = Ꮡp.parseFunctionBlockOrSemicolon(signatureFlags, diagnosticMessage);
    var result = p.finishNode(p.factory.NewMethodDeclaration(modifiers, asteriskToken, name, questionToken, typeParameters, parameters, typeNode, nil, body), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

} partial class parser_package {

internal static bool modifierListHasAsync(ж<ast.ModifierList> Ꮡmodifiers) {
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    return Ꮡmodifiers != nil && core.Some<ж<ast.Node>>(modifiers.Nodes, isAsyncModifier);
}

} partial class parserᴛParser {

internal static ж<ast.Node> parsePropertyDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers, ж<ast.Node> name, ж<ast.Node> questionToken) {
    ref var p = ref Ꮡp.DerefOrNull();

    var postfixToken = questionToken;
    if (postfixToken == nil && !p.hasPrecedingLineBreak()) {
        postfixToken = p.parseOptionalToken(ast.KindExclamationToken);
    }
    var typeNode = Ꮡp.parseTypeAnnotation();
    var initializer = doInContext<ж<astꓸExpression>>(Ꮡp, (ast.NodeFlags)((ast.NodeFlags)(ast.NodeFlagsYieldContext | ast.NodeFlagsAwaitContext) | ast.NodeFlagsDisallowInContext), false, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseInitializer)));
    p.parseSemicolonAfterPropertyName(name, typeNode, initializer);
    var result = p.finishNode(p.factory.NewPropertyDeclaration(modifiers, name, postfixToken, typeNode, initializer), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static void parseSemicolonAfterPropertyName(this ref Parser p, ж<ast.Node> name, ж<astꓸTypeNode> typeNode, ж<astꓸExpression> initializer) {
    if (p.token == ast.KindAtToken && !p.hasPrecedingLineBreak()) {
        p.parseErrorAtCurrentToken(diagnostics.Decorators_must_precede_the_name_and_all_keywords_of_property_declarations);
        return;
    }
    if (p.token == ast.KindOpenParenToken) {
        p.parseErrorAtCurrentToken(diagnostics.Cannot_start_a_function_call_in_a_type_annotation);
        p.nextToken();
        return;
    }
    if (typeNode != nil && !p.canParseSemicolon()) {
        if (initializer != nil) {
            p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(ast.KindSemicolonToken));
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Expected_for_property_initializer);
        }
        return;
    }
    if (p.tryParseSemicolon()) {
        return;
    }
    if (initializer != nil) {
        p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(ast.KindSemicolonToken));
        return;
    }
    p.parseErrorForMissingSemicolonAfter(name);
}

internal static void parseErrorForMissingSemicolonAfter(this ref Parser p, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindTaggedTemplateExpression) {
        p.parseErrorAtRange(p.skipRangeTrivia(node.AsTaggedTemplateExpression().Template.Loc), diagnostics.Module_declaration_names_may_only_use_or_quoted_strings);
        return;
    }
    @string expressionText = default;
    if (node.Kind == ast.KindIdentifier) {
        expressionText = node.Text();
    }
    if (expressionText == ""u8) {
        p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(ast.KindSemicolonToken));
        return;
    }
    nint pos = scanner.SkipTrivia(p.sourceText, node.Pos());
    switch (ᐧ) {
    case {} when expressionText == "const"u8 || expressionText == "let"u8 || expressionText == "var"u8: {
        p.parseErrorAt(pos, node.End(), diagnostics.Variable_declaration_not_allowed_at_this_location);
        return;
    }
    case {} when expressionText == "declare"u8: {
        return;
    }
    case {} when expressionText == "interface"u8: {
        p.parseErrorForInvalidName(diagnostics.Interface_name_cannot_be_0, diagnostics.Interface_must_be_given_a_name, ast.KindOpenBraceToken);
        return;
    }
    case {} when expressionText == "is"u8: {
        p.parseErrorAt(pos, p.scanner.TokenStart(), diagnostics.A_type_predicate_is_only_allowed_in_return_type_position_for_functions_and_methods);
        return;
    }
    case {} when expressionText == "module"u8 || expressionText == "namespace"u8: {
        p.parseErrorForInvalidName(diagnostics.Namespace_name_cannot_be_0, diagnostics.Namespace_must_be_given_a_name, ast.KindOpenBraceToken);
        return;
    }
    case {} when expressionText == "type"u8: {
        p.parseErrorForInvalidName(diagnostics.Type_alias_name_cannot_be_0, diagnostics.Type_alias_must_be_given_a_name, ast.KindEqualsToken);
        return;
    }}

    @string suggestion = core.GetSpellingSuggestionForStrings(expressionText, slices.Values<slice<@string>, @string>(viableKeywordSuggestions));
    if (suggestion == ""u8) {
        suggestion = getSpaceSuggestion(expressionText);
    }
    if (suggestion != ""u8) {
        p.parseErrorAt(pos, node.End(), diagnostics.Unknown_keyword_or_identifier_Did_you_mean_0, suggestion);
        return;
    }
    if (p.token == ast.KindUnknown) {
        return;
    }
    p.parseErrorAt(pos, node.End(), diagnostics.Unexpected_keyword_or_identifier);
}

} partial class parser_package {

internal static @string getSpaceSuggestion(@string expressionText) {
    foreach (var (_, keyword) in range(viableKeywordSuggestions)) {
        if (len(expressionText) > len(keyword) + 2 && strings.HasPrefix(expressionText, keyword)) {
            return keyword + " " + expressionText.slice(len(keyword));
        }
    }
    return "";
}

} partial class parserᴛParser {

internal static void parseErrorForInvalidName(this ref Parser p, ж<diagnostics.Message> nameDiagnostic, ж<diagnostics.Message> blankDiagnostic, ast.Kind tokenIfBlankName) {
    if (p.token == tokenIfBlankName) {
        p.parseErrorAtCurrentToken(blankDiagnostic);
    } else {
        p.parseErrorAtCurrentToken(nameDiagnostic, p.scanner.TokenValue());
    }
}

internal static ж<ast.Node> parseInterfaceDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parseExpected(ast.KindInterfaceKeyword);
    var name = p.parseIdentifier();
    var typeParameters = Ꮡp.parseTypeParameters();
    var heritageClauses = Ꮡp.parseHeritageClauses(true);
    var members = Ꮡp.parseObjectTypeMembers();
    var result = p.finishNode(p.factory.NewInterfaceDeclaration(modifiers, name, typeParameters, heritageClauses, members), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.Node> parseTypeAliasDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parseExpected(ast.KindTypeKeyword);
    if (p.hasPrecedingLineBreak()) {
        p.parseErrorAtCurrentToken(diagnostics.Line_break_not_permitted_here);
    }
    var name = p.parseIdentifier();
    var typeParameters = Ꮡp.parseTypeParameters();
    p.parseExpected(ast.KindEqualsToken);
    ж<astꓸTypeNode> typeNode = default;
    if (p.token == ast.KindIntrinsicKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsNotDot)))) {
        typeNode = p.parseKeywordTypeNode();
    } else {
        typeNode = Ꮡp.parseType();
    }
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewTypeAliasDeclaration(modifiers, name, typeParameters, typeNode), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static bool nextIsNotDot(this ref Parser p) {
    return p.nextToken() != ast.KindDotToken;
}

internal static ж<ast.Node> parseEnumMember(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var name = Ꮡp.parsePropertyName();
    var initializer = doInContext<ж<astꓸExpression>>(Ꮡp, ast.NodeFlagsDisallowInContext, false, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseInitializer)));
    var result = p.finishNode(p.factory.NewEnumMember(name, initializer), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseEnumDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    p.parseExpected(ast.KindEnumKeyword);
    var name = p.parseIdentifier();
    ж<ast.NodeList> members = default;
    if (p.parseExpected(ast.KindOpenBraceToken)) {
        var saveContextFlags = p.contextFlags;
        p.setContextFlags((ast.NodeFlags)(ast.NodeFlagsYieldContext | ast.NodeFlagsAwaitContext), false);
        members = Ꮡp.parseDelimitedList(PCEnumMembers, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseEnumMember)));
        p.contextFlags = saveContextFlags;
        p.parseExpected(ast.KindCloseBraceToken);
    } else {
        members = p.createMissingList();
    }
    var result = p.finishNode(p.factory.NewEnumDeclaration(modifiers, name, members), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return result;
}

internal static ж<astꓸStatement> parseModuleDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var keyword = ast.KindModuleKeyword;
    if (p.token == ast.KindGlobalKeyword) {
        return Ꮡp.parseAmbientExternalModuleDeclaration(pos, jsdoc, modifiers);
    } else if (p.parseOptional(ast.KindNamespaceKeyword)) {
        keyword = ast.KindNamespaceKeyword;
    } else {
        p.parseExpected(ast.KindModuleKeyword);
        if (p.token == ast.KindStringLiteral) {
            return Ꮡp.parseAmbientExternalModuleDeclaration(pos, jsdoc, modifiers);
        }
    }
    return Ꮡp.parseModuleOrNamespaceDeclaration(pos, jsdoc, modifiers, false, keyword);
}

internal static ж<ast.Node> parseAmbientExternalModuleDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    ж<ast.Node> name = default;
    var keyword = ast.KindModuleKeyword;
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    if (p.token == ast.KindGlobalKeyword) {
        name = p.parseIdentifier();
        keyword = ast.KindGlobalKeyword;
    } else {
        name = p.parseLiteralExpression();
    }
    ж<astꓸTypeLiteralNodeNode> attributes = default;
    if (keyword == ast.KindModuleKeyword && p.parseOptional(ast.KindWithKeyword)) {
        attributes = Ꮡp.parseTypeLiteral();
    }
    ж<ast.Node> body = default;
    if (p.token == ast.KindOpenBraceToken) {
        body = Ꮡp.parseModuleBlock();
    } else {
        p.parseSemicolon();
    }
    var result = p.finishNode(p.factory.NewModuleDeclaration(modifiers, keyword, name, attributes, body), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return result;
}

internal static ж<ast.Node> parseModuleBlock(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    ж<ast.NodeList> statements = default;
    if (p.parseExpected(ast.KindOpenBraceToken)) {
        statements = Ꮡp.parseList(PCBlockStatements, ((Func<ж<Parser>, ж<astꓸStatement>>)(parserᴛParser.parseStatement)));
        p.parseExpected(ast.KindCloseBraceToken);
    } else {
        statements = p.createMissingList();
    }
    return p.finishNode(p.factory.NewModuleBlock(statements), pos);
}

internal static ж<ast.Node> parseModuleOrNamespaceDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers, bool nested, ast.Kind keyword) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    ж<ast.Node> name = default;
    if (nested) {
        name = p.parseIdentifierName();
    } else {
        name = p.parseIdentifier();
    }
    ж<ast.Node> body = default;
    if (p.parseOptional(ast.KindDotToken)) {
        var implicitExport = p.factory.NewModifier(ast.KindExportKeyword);
        implicitExport.Loc = core.NewTextRange(p.nodePos(), p.nodePos());
        implicitExport.Flags = ast.NodeFlagsReparsed;
        var implicitModifiers = p.newModifierList(implicitExport.Loc, p.nodeSliceArena.NewSlice1(implicitExport));
        body = Ꮡp.parseModuleOrNamespaceDeclaration(p.nodePos(), 0, implicitModifiers, true, keyword);
    } else {
        body = Ꮡp.parseModuleBlock();
    }
    var result = p.finishNode(p.factory.NewModuleDeclaration(modifiers, keyword, name, nil, body), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return result;
}

internal static ж<astꓸStatement> parseImportDeclarationOrImportEqualsDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parseExpected(ast.KindImportKeyword);
    nint afterImportPos = p.nodePos();
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    ж<ast.Node> identifier = default;
    if (p.isIdentifier()) {
        identifier = p.parseIdentifier();
    }
    var phaseModifier = ast.KindUnknown;
    if (identifier != nil && identifier.Text() == "type"u8 && (p.token != ast.KindFromKeyword || p.isIdentifier() && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsFromKeywordOrEqualsToken)))) && (p.isIdentifier() || p.tokenAfterImportDefinitelyProducesImportDeclaration())) {
        phaseModifier = ast.KindTypeKeyword;
        identifier = default;
        if (p.isIdentifier()) {
            identifier = p.parseIdentifier();
        }
    } else if (identifier != nil && identifier.Text() == "defer"u8) {
        bool shouldParseAsDeferModifier = default;
        if (p.token == ast.KindFromKeyword) {
            shouldParseAsDeferModifier = !Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsTokenStringLiteral)));
        } else {
            shouldParseAsDeferModifier = p.token != ast.KindCommaToken && p.token != ast.KindEqualsToken;
        }
        if (shouldParseAsDeferModifier) {
            phaseModifier = ast.KindDeferKeyword;
            identifier = default;
            if (p.isIdentifier()) {
                identifier = p.parseIdentifier();
            }
        }
    }
    if (identifier != nil && !p.tokenAfterImportedIdentifierDefinitelyProducesImportDeclaration() && phaseModifier != ast.KindDeferKeyword) {
        var importEquals = p.checkJSSyntax(Ꮡp.parseImportEqualsDeclaration(pos, jsdoc, modifiers, identifier, phaseModifier == ast.KindTypeKeyword));
        p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
        return importEquals;
    }
    var importClause = Ꮡp.tryParseImportClause(identifier, afterImportPos, phaseModifier, false);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    var moduleSpecifier = Ꮡp.parseModuleSpecifier();
    var attributes = Ꮡp.tryParseImportAttributes();
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewImportDeclaration(modifiers, importClause, moduleSpecifier, attributes), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static bool nextTokenIsFromKeywordOrEqualsToken(this ref Parser p) {
    p.nextToken();
    return p.token == ast.KindFromKeyword || p.token == ast.KindEqualsToken;
}

internal static bool tokenAfterImportDefinitelyProducesImportDeclaration(this ref Parser p) {
    return p.token == ast.KindAsteriskToken || p.token == ast.KindOpenBraceToken;
}

internal static bool tokenAfterImportedIdentifierDefinitelyProducesImportDeclaration(this ref Parser p) {
    return p.token == ast.KindCommaToken || p.token == ast.KindFromKeyword;
}

internal static ж<ast.Node> parseImportEqualsDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers, ж<ast.Node> identifier, bool isTypeOnly) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parseExpected(ast.KindEqualsToken);
    var moduleReference = Ꮡp.parseModuleReference();
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewImportEqualsDeclaration(modifiers, isTypeOnly, identifier, moduleReference), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseModuleReference(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindRequireKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOpenParen)))) {
        return Ꮡp.parseExternalModuleReference();
    }
    return Ꮡp.parseEntityName(false, false, nil);
}

internal static ж<ast.Node> parseExternalModuleReference(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    nint pos = p.nodePos();
    p.parseExpected(ast.KindRequireKeyword);
    p.parseExpected(ast.KindOpenParenToken);
    var expression = Ꮡp.parseModuleSpecifier();
    p.parseExpected(ast.KindCloseParenToken);
    var result = p.finishNode(p.factory.NewExternalModuleReference(expression), pos);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return result;
}

internal static ж<astꓸExpression> parseModuleSpecifier(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindStringLiteral) {
        return p.parseLiteralExpression();
    }
    return Ꮡp.parseExpression();
}

internal static ж<ast.Node> tryParseImportClause(this ж<Parser> Ꮡp, ж<ast.Node> identifier, nint pos, ast.Kind phaseModifier, bool skipJSDocLeadingAsterisks) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (identifier != nil || p.token == ast.KindAsteriskToken || p.token == ast.KindOpenBraceToken) {
        var importClause = Ꮡp.parseImportClause(identifier, pos, phaseModifier, skipJSDocLeadingAsterisks);
        p.parseExpected(ast.KindFromKeyword);
        return importClause;
    }
    return default;
}

internal static ж<ast.Node> parseImportClause(this ж<Parser> Ꮡp, ж<ast.Node> identifier, nint pos, ast.Kind phaseModifier, bool skipJSDocLeadingAsterisks) {
    ref var p = ref Ꮡp.DerefOrNull();

    ж<ast.Node> namedBindings = default;
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    if (identifier == nil || p.parseOptional(ast.KindCommaToken)) {
        if (skipJSDocLeadingAsterisks) {
            p.scanner.SetSkipJSDocLeadingAsterisks(true);
        }
        if (p.token == ast.KindAsteriskToken) {
            namedBindings = p.parseNamespaceImport();
        } else {
            namedBindings = Ꮡp.parseNamedImports();
        }
        if (skipJSDocLeadingAsterisks) {
            p.scanner.SetSkipJSDocLeadingAsterisks(false);
        }
    }
    var result = p.finishNode(p.factory.NewImportClause(phaseModifier, identifier, namedBindings), pos);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return result;
}

internal static ж<ast.Node> parseNamespaceImport(this ref Parser p) {
    nint pos = p.nodePos();
    p.parseExpected(ast.KindAsteriskToken);
    p.parseExpected(ast.KindAsKeyword);
    var name = p.parseIdentifier();
    return p.finishNode(p.factory.NewNamespaceImport(name), pos);
}

internal static ж<ast.Node> parseNamedImports(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var imports = Ꮡp.parseBracketedList(PCImportOrExportSpecifiers, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseImportSpecifier)), ast.KindOpenBraceToken, ast.KindCloseBraceToken);
    return p.finishNode(p.factory.NewNamedImports(imports), pos);
}

internal static ж<ast.Node> parseImportSpecifier(this ref Parser p) {
    nint pos = p.nodePos();
    var (isTypeOnly, propertyName, name) = p.parseImportOrExportSpecifier(ast.KindImportSpecifier);
    ж<ast.Node> identifierName = default;
    if (name.Kind == ast.KindIdentifier) {
        identifierName = name;
    } else {
        p.parseErrorAtRange(p.skipRangeTrivia(name.Loc), diagnostics.Identifier_expected);
        identifierName = p.newIdentifier("");
        p.finishNode(identifierName, name.Pos());
    }
    var result = p.checkJSSyntax(p.finishNode(p.factory.NewImportSpecifier(isTypeOnly, propertyName, identifierName), pos));
    return result;
}

internal static (bool isTypeOnly, ж<ast.Node> propertyName, ж<ast.Node> name) parseImportOrExportSpecifier(this ref Parser p, ast.Kind kind) {
    bool isTypeOnly = default;
    ж<ast.Node> propertyName = default;
    ж<ast.Node> name = default;

    var canParseAsKeyword = true;
    var disallowKeywords = kind == ast.KindImportSpecifier;
    bool nameOk = default;
    (name, nameOk) = p.parseModuleExportName(disallowKeywords);
    if (name.Kind == ast.KindIdentifier && name.Text() == "type"u8) {
        if (p.token == ast.KindAsKeyword) {
            var firstAs = p.parseIdentifierName();
            if (p.token == ast.KindAsKeyword) {
                var secondAs = p.parseIdentifierName();
                if (p.canParseModuleExportName()) {
                    isTypeOnly = true;
                    propertyName = firstAs;
                    (name, nameOk) = p.parseModuleExportName(disallowKeywords);
                    canParseAsKeyword = false;
                } else {
                    propertyName = name;
                    name = secondAs;
                    canParseAsKeyword = false;
                }
            } else if (p.canParseModuleExportName()) {
                propertyName = name;
                canParseAsKeyword = false;
                (name, nameOk) = p.parseModuleExportName(disallowKeywords);
            } else {
                isTypeOnly = true;
                name = firstAs;
            }
        } else if (p.canParseModuleExportName()) {
            isTypeOnly = true;
            (name, nameOk) = p.parseModuleExportName(disallowKeywords);
        }
    }
    if (canParseAsKeyword && p.token == ast.KindAsKeyword) {
        propertyName = name;
        p.parseExpected(ast.KindAsKeyword);
        (name, nameOk) = p.parseModuleExportName(disallowKeywords);
    }

    if (!nameOk) {
        p.parseErrorAtRange(p.skipRangeTrivia(name.Loc), diagnostics.Identifier_expected);
    }

    return (isTypeOnly, propertyName, name);
}

internal static bool canParseModuleExportName(this ref Parser p) {
    return tokenIsIdentifierOrKeyword(p.token) || p.token == ast.KindStringLiteral;
}

internal static (ж<ast.Node> node, bool nameOk) parseModuleExportName(this ref Parser p, bool disallowKeywords) {
    bool nameOk = default;

    nameOk = true;

    if (p.token == ast.KindStringLiteral) {
        return (p.parseLiteralExpression(), nameOk);
    }
    if (disallowKeywords && ast.IsKeyword(p.token) && !p.isIdentifier()) {
        nameOk = false;
    }
    return (p.parseIdentifierName(), nameOk);
}

internal static ж<ast.Node> tryParseImportAttributes(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindWithKeyword || (p.token == ast.KindAssertKeyword && !p.hasPrecedingLineBreak())) {
        if (p.token == ast.KindAssertKeyword) {
            p.parseErrorAtCurrentToken(diagnostics.Import_assertions_have_been_replaced_by_import_attributes_Use_with_instead_of_assert);
        }
        return Ꮡp.parseImportAttributes(p.token, false);
    }
    return default;
}

internal static ж<ast.Node> parseExportAssignment(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    p.setContextFlags(ast.NodeFlagsAwaitContext, true);
    var isExportEquals = false;
    if (p.parseOptional(ast.KindEqualsToken)) {
        isExportEquals = true;
    } else {
        p.parseExpected(ast.KindDefaultKeyword);
    }
    var expression = Ꮡp.parseAssignmentExpressionOrHigher();
    p.parseSemicolon();
    p.contextFlags = saveContextFlags;
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    var result = p.finishNode(p.factory.NewExportAssignment(modifiers, isExportEquals, nil, expression), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.Node> parseNamespaceExportDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parseExpected(ast.KindAsKeyword);
    p.parseExpected(ast.KindNamespaceKeyword);
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    var name = p.parseIdentifier();
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    p.parseSemicolon();
    var result = p.finishNode(p.factory.NewNamespaceExportDeclaration(modifiers, name), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parseExportDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    p.setContextFlags(ast.NodeFlagsAwaitContext, true);
    ж<ast.Node> exportClause = default;
    ж<astꓸExpression> moduleSpecifier = default;
    ж<ast.Node> attributes = default;
    var isTypeOnly = p.parseOptional(ast.KindTypeKeyword);
    nint namespaceExportPos = p.nodePos();
    if (p.parseOptional(ast.KindAsteriskToken)) {
        if (p.parseOptional(ast.KindAsKeyword)) {
            exportClause = p.parseNamespaceExport(namespaceExportPos);
        }
        p.parseExpected(ast.KindFromKeyword);
        moduleSpecifier = Ꮡp.parseModuleSpecifier();
    } else {
        exportClause = Ꮡp.parseNamedExports();
        if (p.token == ast.KindFromKeyword || (p.token == ast.KindStringLiteral && !p.hasPrecedingLineBreak())) {
            p.parseExpected(ast.KindFromKeyword);
            moduleSpecifier = Ꮡp.parseModuleSpecifier();
        }
    }
    if (moduleSpecifier != nil && (p.token == ast.KindWithKeyword || p.token == ast.KindAssertKeyword) && !p.hasPrecedingLineBreak()) {
        if (p.token == ast.KindAssertKeyword) {
            p.parseErrorAtCurrentToken(diagnostics.Import_assertions_have_been_replaced_by_import_attributes_Use_with_instead_of_assert);
        }
        attributes = Ꮡp.parseImportAttributes(p.token, false);
    }
    p.parseSemicolon();
    p.contextFlags = saveContextFlags;
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    var result = p.finishNode(p.factory.NewExportDeclaration(modifiers, isTypeOnly, exportClause, moduleSpecifier, attributes), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.Node> parseNamespaceExport(this ref Parser p, nint pos) {
    var (exportName, _) = p.parseModuleExportName(false);
    return p.finishNode(p.factory.NewNamespaceExport(exportName), pos);
}

internal static ж<ast.Node> parseNamedExports(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var exports = Ꮡp.parseBracketedList(PCImportOrExportSpecifiers, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseExportSpecifier)), ast.KindOpenBraceToken, ast.KindCloseBraceToken);
    return p.finishNode(p.factory.NewNamedExports(exports), pos);
}

internal static ж<ast.Node> parseExportSpecifier(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var (isTypeOnly, propertyName, name) = p.parseImportOrExportSpecifier(ast.KindExportSpecifier);
    var result = p.finishNode(p.factory.NewExportSpecifier(isTypeOnly, propertyName, name), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<astꓸTypeNode> parseType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsTypeExcludesFlags, false);
    ж<astꓸTypeNode> typeNode = default;
    if (Ꮡp.isStartOfFunctionTypeOrConstructorType()) {
        typeNode = Ꮡp.parseFunctionOrConstructorType();
    } else {
        nint pos = p.nodePos();
        typeNode = Ꮡp.parseUnionTypeOrHigher();
        if (!p.inDisallowConditionalTypesContext() && !p.hasPrecedingLineBreak() && p.parseOptional(ast.KindExtendsKeyword)) {
            var extendsType = doInContext<ж<astꓸTypeNode>>(Ꮡp, ast.NodeFlagsDisallowConditionalTypesContext, true, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseType)));
            p.parseExpected(ast.KindQuestionToken);
            var trueType = doInContext<ж<astꓸTypeNode>>(Ꮡp, ast.NodeFlagsDisallowConditionalTypesContext, false, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseType)));
            p.parseExpected(ast.KindColonToken);
            var falseType = doInContext<ж<astꓸTypeNode>>(Ꮡp, ast.NodeFlagsDisallowConditionalTypesContext, false, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseType)));
            var conditionalType = p.factory.NewConditionalTypeNode(typeNode, extendsType, trueType, falseType);
            p.finishNode(conditionalType, pos);
            typeNode = conditionalType;
        }
    }
    p.contextFlags = saveContextFlags;
    return typeNode;
}

internal static ж<astꓸTypeNode> parseUnionTypeOrHigher(this ж<Parser> Ꮡp) {
    return Ꮡp.parseUnionOrIntersectionType(ast.KindBarToken, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseIntersectionTypeOrHigher)));
}

internal static ж<astꓸTypeNode> parseIntersectionTypeOrHigher(this ж<Parser> Ꮡp) {
    return Ꮡp.parseUnionOrIntersectionType(ast.KindAmpersandToken, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseTypeOperatorOrHigher)));
}

internal static ж<astꓸTypeNode> parseUnionOrIntersectionType(this ж<Parser> Ꮡp, ast.Kind @operator, Func<ж<Parser>, ж<astꓸTypeNode>> parseConstituentType) {
    ref var p = ref Ꮡp.DerefOrNull();
    var typesᴷ = new stackAppend<ж<ast.Node>>();

    nint pos = p.nodePos();
    var isUnionType = @operator == ast.KindBarToken;
    var hasLeadingOperator = p.parseOptional(@operator);
    ж<astꓸTypeNode> typeNode = default;
    if (hasLeadingOperator) {
        typeNode = Ꮡp.parseFunctionOrConstructorTypeToError(isUnionType, parseConstituentType);
    } else {
        typeNode = parseConstituentType(Ꮡp);
    }
    if (p.token == @operator || hasLeadingOperator) {
        var types = new slice<ж<ast.Node>>(1, 8);
        types[0] = typeNode;
        while (p.parseOptional(@operator)) {
            types = append(typesᴷ, types, Ꮡp.parseFunctionOrConstructorTypeToError(isUnionType, parseConstituentType));
        }
        typeNode = p.createUnionOrIntersectionTypeNode(@operator, p.newNodeList(core.NewTextRange(pos, p.nodePos()), p.nodeSliceArena.Clone(types)));
        p.finishNode(typeNode, pos);
    }
    return typeNode;
}

internal static ж<ast.Node> createUnionOrIntersectionTypeNode(this ref Parser p, ast.Kind @operator, ж<ast.NodeList> types) {
    switch (@operator) {
    case var ᴛ when ᴛ == ast.KindBarToken: {
        return p.factory.NewUnionTypeNode(types);
    }
    case var ᴛ when ᴛ == ast.KindAmpersandToken: {
        return p.factory.NewIntersectionTypeNode(types);
    }
    default: {
        throw panic("Unhandled case in createUnionOrIntersectionType");
        break;
    }}

}

internal static ж<astꓸTypeNode> parseTypeOperatorOrHigher(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var @operator = p.token;
    switch (@operator) {
    case var ᴛ when ᴛ == ast.KindKeyOfKeyword || ᴛ == ast.KindUniqueKeyword || ᴛ == ast.KindReadonlyKeyword: {
        return Ꮡp.parseTypeOperator(@operator);
    }
    case var ᴛ when ᴛ == ast.KindInferKeyword: {
        return Ꮡp.parseInferType();
    }}

    return doInContext<ж<ast.Node>>(Ꮡp, ast.NodeFlagsDisallowConditionalTypesContext, false, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parsePostfixTypeOrHigher)));
}

internal static ж<ast.Node> parseTypeOperator(this ж<Parser> Ꮡp, ast.Kind @operator) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(@operator);
    return p.finishNode(p.factory.NewTypeOperatorNode(@operator, Ꮡp.parseTypeOperatorOrHigher()), pos);
}

internal static ж<ast.Node> parseInferType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindInferKeyword);
    return p.finishNode(p.factory.NewInferTypeNode(Ꮡp.parseTypeParameterOfInferType()), pos);
}

internal static ж<ast.Node> parseTypeParameterOfInferType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var name = p.parseIdentifier();
    var constraint = Ꮡp.tryParseConstraintOfInferType();
    return p.finishNode(p.factory.NewTypeParameterDeclaration(nil, name, constraint, nil, nil), pos);
}

internal static ж<ast.Node> tryParseConstraintOfInferType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.mark();
    if (p.parseOptional(ast.KindExtendsKeyword)) {
        var constraint = doInContext<ж<astꓸTypeNode>>(Ꮡp, ast.NodeFlagsDisallowConditionalTypesContext, true, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseType)));
        if (p.inDisallowConditionalTypesContext() || p.token != ast.KindQuestionToken) {
            return constraint;
        }
    }
    p.rewind(state);
    return default;
}

internal static ж<ast.Node> parsePostfixTypeOrHigher(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var typeNode = Ꮡp.parseNonArrayType();
    while (!p.hasPrecedingLineBreak()) {
        switch (p.token) {
        case var ᴛ when ᴛ == ast.KindExclamationToken: {
            p.nextToken();
            typeNode = p.finishNode(p.factory.NewJSDocNonNullableType(typeNode), pos);
            break;
        }
        case var ᴛ when ᴛ == ast.KindQuestionToken: {
            if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsStartOfType)))) {
                return typeNode;
            }
            p.nextToken();
            typeNode = p.finishNode(p.factory.NewJSDocNullableType(typeNode), pos);
            break;
        }
        case var ᴛ when ᴛ == ast.KindOpenBracketToken: {
            p.parseExpected(ast.KindOpenBracketToken);
            if (Ꮡp.isStartOfType(false)) {
                var indexType = Ꮡp.parseType();
                p.parseExpected(ast.KindCloseBracketToken);
                typeNode = p.finishNode(p.factory.NewIndexedAccessTypeNode(typeNode, indexType), pos);
            } else {
                p.parseExpected(ast.KindCloseBracketToken);
                typeNode = p.finishNode(p.factory.NewArrayTypeNode(typeNode), pos);
            }
            break;
        }
        default: {
            return typeNode;
        }}

    }
    return typeNode;
}

internal static bool nextIsStartOfType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.nextToken();
    return Ꮡp.isStartOfType(false);
}

internal static ж<ast.Node> parseNonArrayType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var exprᴛ1 = p.token;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindAnyKeyword || exprᴛ1 == ast.KindUnknownKeyword || exprᴛ1 == ast.KindStringKeyword || exprᴛ1 == ast.KindNumberKeyword || exprᴛ1 == ast.KindBigIntKeyword || exprᴛ1 == ast.KindSymbolKeyword || exprᴛ1 == ast.KindBooleanKeyword || exprᴛ1 == ast.KindUndefinedKeyword || exprᴛ1 == ast.KindNeverKeyword || exprᴛ1 == ast.KindObjectKeyword) { matchᴛ1 = true;
        var state = p.mark();
        var keywordTypeNode = p.parseKeywordTypeNode();
        if (p.token != ast.KindDotToken) {
            return keywordTypeNode;
        }
        p.rewind(state);
        return Ꮡp.parseTypeReference();
    }
    if (exprᴛ1 == ast.KindAsteriskEqualsToken) { matchᴛ1 = true;
        p.scanner.ReScanAsteriskEqualsToken();
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && exprᴛ1 == ast.KindAsteriskToken) { matchᴛ1 = true;
        return p.parseJSDocAllType();
    }
    if (exprᴛ1 == ast.KindQuestionQuestionToken) { matchᴛ1 = true;
        p.scanner.ReScanQuestionToken();
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && exprᴛ1 == ast.KindQuestionToken) { matchᴛ1 = true;
        return Ꮡp.parseJSDocNullableType();
    }
    if (exprᴛ1 == ast.KindExclamationToken) { matchᴛ1 = true;
        return Ꮡp.parseJSDocNonNullableType();
    }
    if (exprᴛ1 == ast.KindNoSubstitutionTemplateLiteral || exprᴛ1 == ast.KindStringLiteral || exprᴛ1 == ast.KindNumericLiteral || exprᴛ1 == ast.KindBigIntLiteral || exprᴛ1 == ast.KindTrueKeyword || exprᴛ1 == ast.KindFalseKeyword || exprᴛ1 == ast.KindNullKeyword) { matchᴛ1 = true;
        return p.parseLiteralTypeNode(false);
    }
    if (exprᴛ1 == ast.KindMinusToken) { matchᴛ1 = true;
        if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsNumericOrBigIntLiteral)))) {
            return p.parseLiteralTypeNode(true);
        }
        return Ꮡp.parseTypeReference();
    }
    if (exprᴛ1 == ast.KindVoidKeyword) { matchᴛ1 = true;
        return p.parseKeywordTypeNode();
    }
    if (exprᴛ1 == ast.KindThisKeyword) { matchᴛ1 = true;
        var thisKeyword = p.parseThisTypeNode();
        if (p.token == ast.KindIsKeyword && !p.hasPrecedingLineBreak()) {
            return Ꮡp.parseThisTypePredicate(thisKeyword);
        }
        return thisKeyword;
    }
    if (exprᴛ1 == ast.KindTypeOfKeyword) { matchᴛ1 = true;
        if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsStartOfTypeOfImportType)))) {
            return Ꮡp.parseImportType();
        }
        return Ꮡp.parseTypeQuery();
    }
    if (exprᴛ1 == ast.KindOpenBraceToken) { matchᴛ1 = true;
        if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsStartOfMappedType)))) {
            return Ꮡp.parseMappedType();
        }
        return Ꮡp.parseTypeLiteral();
    }
    if (exprᴛ1 == ast.KindOpenBracketToken) { matchᴛ1 = true;
        return Ꮡp.parseTupleType();
    }
    if (exprᴛ1 == ast.KindOpenParenToken) { matchᴛ1 = true;
        return Ꮡp.parseParenthesizedType();
    }
    if (exprᴛ1 == ast.KindImportKeyword) { matchᴛ1 = true;
        return Ꮡp.parseImportType();
    }
    if (exprᴛ1 == ast.KindAssertsKeyword) { matchᴛ1 = true;
        if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeywordOnSameLine)))) {
            return Ꮡp.parseAssertsTypePredicate();
        }
        return Ꮡp.parseTypeReference();
    }
    if (exprᴛ1 == ast.KindTemplateHead) {
        return Ꮡp.parseTemplateType();
    }
    if (!matchᴛ1) { /* default: */
        return Ꮡp.parseTypeReference();
    }
    return default;

}

internal static ж<ast.Node> parseKeywordTypeNode(this ref Parser p) {
    nint pos = p.nodePos();
    var result = p.factory.NewKeywordTypeNode(p.token);
    p.nextToken();
    return p.finishNode(result, pos);
}

internal static ж<ast.Node> parseThisTypeNode(this ref Parser p) {
    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewThisTypeNode(), pos);
}

internal static ж<ast.Node> parseThisTypePredicate(this ж<Parser> Ꮡp, ж<ast.Node> Ꮡlhs) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var lhs = ref Ꮡlhs.DerefOrNull();

    p.nextToken();
    return p.finishNode(p.factory.NewTypePredicateNode(nil, Ꮡlhs, Ꮡp.parseType()), lhs.Pos());
}

internal static ж<ast.Node> parseJSDocAllType(this ref Parser p) {
    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewJSDocAllType(), pos);
}

internal static ж<astꓸTypeNode> parseJSDocNonNullableType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewJSDocNonNullableType(Ꮡp.parseTypeOperatorOrHigher()), pos);
}

internal static ж<ast.Node> parseJSDocNullableType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewJSDocNullableType(Ꮡp.parseTypeOperatorOrHigher()), pos);
}

internal static ж<astꓸTypeNode> parseJSDocType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.scanner.SetSkipJSDocLeadingAsterisks(true);
    nint pos = p.nodePos();

    var hasDotDotDot = p.parseOptional(ast.KindDotDotDotToken);
    var t = Ꮡp.parseTypeOrTypePredicate();
    p.scanner.SetSkipJSDocLeadingAsterisks(false);
    if (hasDotDotDot) {
        t = p.finishNode(p.factory.NewJSDocVariadicType(t), pos);
    }
    if (p.token == ast.KindEqualsToken) {
        p.nextToken();
        return p.finishNode(p.factory.NewJSDocOptionalType(t), pos);
    }
    return t;
}

internal static ж<ast.Node> parseLiteralTypeNode(this ref Parser p, bool negative) {
    nint pos = p.nodePos();
    if (negative) {
        p.nextToken();
    }
    ж<astꓸExpression> expression = default;
    if (p.token == ast.KindTrueKeyword || p.token == ast.KindFalseKeyword || p.token == ast.KindNullKeyword) {
        expression = p.parseKeywordExpression();
    } else {
        expression = p.parseLiteralExpression();
    }
    if (negative) {
        expression = p.finishNode(p.factory.NewPrefixUnaryExpression(ast.KindMinusToken, expression), pos);
    }
    return p.finishNode(p.factory.NewLiteralTypeNode(expression), pos);
}

internal static ж<ast.Node> parseTypeReference(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    return p.finishNode(p.factory.NewTypeReferenceNode(Ꮡp.parseEntityNameOfTypeReference(), Ꮡp.parseTypeArgumentsOfTypeReference()), pos);
}

internal static ж<ast.Node> parseEntityNameOfTypeReference(this ж<Parser> Ꮡp) {
    return Ꮡp.parseEntityName(true, false, diagnostics.Type_expected);
}

internal static ж<ast.Node> parseEntityName(this ж<Parser> Ꮡp, bool allowReservedWords, bool allowPrivateName, ж<diagnostics.Message> diagnosticMessage) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    ж<ast.Node> entity = default;
    if (allowReservedWords) {
        entity = p.parseIdentifierNameWithDiagnostic(diagnosticMessage);
    } else {
        entity = p.parseIdentifierWithDiagnostic(diagnosticMessage, nil);
    }
    while (p.parseOptional(ast.KindDotToken)) {
        if (p.token == ast.KindLessThanToken) {
            break;
        }
        entity = p.finishNode(p.factory.NewQualifiedName(entity, Ꮡp.parseRightSideOfDot(allowReservedWords, allowPrivateName, true)), pos);
    }
    return entity;
}

internal static ж<ast.Node> parseRightSideOfDot(this ж<Parser> Ꮡp, bool allowIdentifierNames, bool allowPrivateIdentifiers, bool allowUnicodeEscapeSequenceInIdentifierName) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.hasPrecedingLineBreak() && tokenIsIdentifierOrKeyword(p.token) && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeywordOnSameLine)))) {
        p.parseErrorAt(p.nodePos(), p.nodePos(), diagnostics.Identifier_expected);
        return p.createMissingIdentifier();
    }
    if (p.token == ast.KindPrivateIdentifier) {
        var node = p.parsePrivateIdentifier();
        if (allowPrivateIdentifiers) {
            return node;
        }
        p.parseErrorAt(p.nodePos(), p.nodePos(), diagnostics.Identifier_expected);
        return p.createMissingIdentifier();
    }
    if (allowIdentifierNames) {
        if (allowUnicodeEscapeSequenceInIdentifierName) {
            return p.parseIdentifierName();
        }
        return p.parseIdentifierNameErrorOnUnicodeEscapeSequence();
    }
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    var id = p.parseIdentifier();
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return id;
}

internal static ж<ast.Node> newIdentifier(this ref Parser p, @string text) {
    p.identifierCount++;
    var id = p.factory.NewIdentifier(text);
    if (text == "await"u8) {
        p.statementHasAwaitIdentifier = true;
    }
    return id;
}

internal static ж<ast.Node> createMissingIdentifier(this ref Parser p) {
    return p.finishNode(p.newIdentifier(""), p.nodePos());
}

internal static ж<ast.Node> parsePrivateIdentifier(this ref Parser p) {
    nint pos = p.nodePos();
    @string text = p.scanner.TokenValue();
    p.nextToken();
    return p.finishNode(p.factory.NewPrivateIdentifier(text), pos);
}

internal static ast.Kind reScanLessThanToken(this ref Parser p) {
    p.token = p.scanner.ReScanLessThanToken();
    return p.token;
}

internal static ast.Kind reScanGreaterThanToken(this ref Parser p) {
    p.token = p.scanner.ReScanGreaterThanToken();
    return p.token;
}

internal static ast.Kind reScanSlashToken(this ref Parser p) {
    p.token = p.scanner.ReScanSlashToken();
    return p.token;
}

internal static ast.Kind reScanTemplateToken(this ref Parser p, bool isTaggedTemplate) {
    p.token = p.scanner.ReScanTemplateToken(isTaggedTemplate);
    return p.token;
}

internal static ж<ast.NodeList> parseTypeArgumentsOfTypeReference(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (!p.hasPrecedingLineBreak() && p.reScanLessThanToken() == ast.KindLessThanToken) {
        return Ꮡp.parseTypeArguments();
    }
    return default;
}

internal static ж<ast.NodeList> parseTypeArguments(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindLessThanToken) {
        return Ꮡp.parseBracketedList(PCTypeArguments, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseType)), ast.KindLessThanToken, ast.KindGreaterThanToken);
    }
    return default;
}

internal static bool nextIsStartOfTypeOfImportType(this ref Parser p) {
    p.nextToken();
    return p.token == ast.KindImportKeyword;
}

internal static ж<ast.Node> parseImportType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.sourceFlags |= (ast.NodeFlags)(ast.NodeFlagsPossiblyContainsDynamicImport);
    nint pos = p.nodePos();
    var isTypeOf = p.parseOptional(ast.KindTypeOfKeyword);
    p.parseExpected(ast.KindImportKeyword);
    p.parseExpected(ast.KindOpenParenToken);
    var typeNode = Ꮡp.parseType();
    ж<ast.Node> attributes = default;
    if (p.parseOptional(ast.KindCommaToken)) {
        nint openBracePosition = p.scanner.TokenStart();
        p.parseExpected(ast.KindOpenBraceToken);
        var currentToken = p.token;
        if (currentToken == ast.KindWithKeyword || currentToken == ast.KindAssertKeyword) {
            if (currentToken == ast.KindAssertKeyword) {
                p.parseErrorAtCurrentToken(diagnostics.Import_assertions_have_been_replaced_by_import_attributes_Use_with_instead_of_assert);
            }
            p.nextToken();
        } else {
            p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(ast.KindWithKeyword));
        }
        p.parseExpected(ast.KindColonToken);
        attributes = Ꮡp.parseImportAttributes(currentToken, true);
        p.parseOptional(ast.KindCommaToken);
        if (!p.parseExpected(ast.KindCloseBraceToken)) {
            if (len(p.diagnostics) != 0) {
                var lastDiagnostic = p.diagnostics[len(p.diagnostics) - 1];
                if (lastDiagnostic.Code() == diagnostics.X_0_expected.Code()) {
                    var related = ast.NewDiagnostic(nil, core.NewTextRange(openBracePosition, openBracePosition), diagnostics.The_parser_expected_to_find_a_1_to_match_the_0_token_here, "{", "}");
                    lastDiagnostic.AddRelatedInfo(related);
                }
            }
        }
    }
    p.parseExpected(ast.KindCloseParenToken);
    ж<ast.Node> qualifier = default;
    if (p.parseOptional(ast.KindDotToken)) {
        qualifier = Ꮡp.parseEntityNameOfTypeReference();
    }
    var typeArguments = Ꮡp.parseTypeArgumentsOfTypeReference();
    return p.finishNode(p.factory.NewImportTypeNode(isTypeOf, typeNode, attributes, qualifier, typeArguments), pos);
}

internal static ж<ast.Node> parseImportAttribute(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    ж<ast.Node> name = default;
    if (tokenIsIdentifierOrKeyword(p.token)) {
        name = p.parseIdentifierName();
    } else if (p.token == ast.KindStringLiteral) {
        name = p.parseLiteralExpression();
    }
    if (name != nil) {
        p.parseExpected(ast.KindColonToken);
    } else {
        p.parseErrorAtCurrentToken(diagnostics.Identifier_or_string_literal_expected);
    }
    var value = Ꮡp.parseAssignmentExpressionOrHigher();
    return p.finishNode(p.factory.NewImportAttribute(name, value), pos);
}

internal static ж<ast.Node> parseImportAttributes(this ж<Parser> Ꮡp, ast.Kind token, bool skipKeyword) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    if (!skipKeyword) {
        p.parseExpected(token);
    }
    ж<ast.NodeList> elements = default;
    bool multiLine = default;
    nint openBracePosition = p.scanner.TokenStart();
    if (p.parseExpected(ast.KindOpenBraceToken)) {
        multiLine = p.hasPrecedingLineBreak();
        elements = Ꮡp.parseDelimitedList(PCImportAttributes, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseImportAttribute)));
        if (!p.parseExpected(ast.KindCloseBraceToken)) {
            if (len(p.diagnostics) != 0) {
                var lastDiagnostic = p.diagnostics[len(p.diagnostics) - 1];
                if (lastDiagnostic.Code() == diagnostics.X_0_expected.Code()) {
                    var related = ast.NewDiagnostic(nil, core.NewTextRange(openBracePosition, openBracePosition), diagnostics.The_parser_expected_to_find_a_1_to_match_the_0_token_here, "{", "}");
                    lastDiagnostic.AddRelatedInfo(related);
                }
            }
        }
    } else {
        elements = p.parseEmptyNodeList();
    }
    return p.finishNode(p.factory.NewImportAttributes(token, elements, multiLine), pos);
}

internal static ж<ast.Node> parseTypeQuery(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindTypeOfKeyword);
    var entityName = Ꮡp.parseEntityName(true, true, nil);
    ж<ast.NodeList> typeArguments = default;
    if (!p.hasPrecedingLineBreak()) {
        typeArguments = Ꮡp.parseTypeArguments();
    }
    return p.finishNode(p.factory.NewTypeQueryNode(entityName, typeArguments), pos);
}

internal static bool nextIsStartOfMappedType(this ref Parser p) {
    p.nextToken();
    if (p.token == ast.KindPlusToken || p.token == ast.KindMinusToken) {
        return p.nextToken() == ast.KindReadonlyKeyword;
    }
    if (p.token == ast.KindReadonlyKeyword) {
        p.nextToken();
    }
    return p.token == ast.KindOpenBracketToken && p.nextTokenIsIdentifier() && p.nextToken() == ast.KindInKeyword;
}

internal static ж<ast.Node> parseMappedType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindOpenBraceToken);
    ж<ast.Node> readonlyToken = default;
    if (p.token == ast.KindReadonlyKeyword || p.token == ast.KindPlusToken || p.token == ast.KindMinusToken) {
        readonlyToken = p.parseTokenNode();
        if (readonlyToken.Kind != ast.KindReadonlyKeyword) {
            p.parseExpected(ast.KindReadonlyKeyword);
        }
    }
    p.parseExpected(ast.KindOpenBracketToken);
    var typeParameter = Ꮡp.parseMappedTypeParameter();
    ж<astꓸTypeNode> nameType = default;
    if (p.parseOptional(ast.KindAsKeyword)) {
        nameType = Ꮡp.parseType();
    }
    p.parseExpected(ast.KindCloseBracketToken);
    ж<ast.Node> questionToken = default;
    if (p.token == ast.KindQuestionToken || p.token == ast.KindPlusToken || p.token == ast.KindMinusToken) {
        questionToken = p.parseTokenNode();
        if (questionToken.Kind != ast.KindQuestionToken) {
            p.parseExpected(ast.KindQuestionToken);
        }
    }
    var typeNode = Ꮡp.parseTypeAnnotation();
    p.parseSemicolon();
    var members = Ꮡp.parseList(PCTypeMembers, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseTypeMember)));
    p.parseExpected(ast.KindCloseBraceToken);
    return p.finishNode(p.factory.NewMappedTypeNode(readonlyToken, typeParameter, nameType, questionToken, typeNode, members), pos);
}

internal static ж<ast.Node> parseMappedTypeParameter(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var name = p.parseIdentifierName();
    p.parseExpected(ast.KindInKeyword);
    var typeNode = Ꮡp.parseType();
    return p.finishNode(p.factory.NewTypeParameterDeclaration(nil, name, typeNode, nil, nil), pos);
}

internal static ж<ast.Node> parseTypeMember(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken) {
        return Ꮡp.parseSignatureMember(ast.KindCallSignature);
    }
    if (p.token == ast.KindNewKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOpenParenOrLessThan)))) {
        return Ꮡp.parseSignatureMember(ast.KindConstructSignature);
    }
    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var modifiers = Ꮡp.parseModifiers();
    if (Ꮡp.parseContextualModifier(ast.KindGetKeyword)) {
        return Ꮡp.parseAccessorDeclaration(pos, jsdoc, modifiers, ast.KindGetAccessor, ParseFlagsType);
    }
    if (Ꮡp.parseContextualModifier(ast.KindSetKeyword)) {
        return Ꮡp.parseAccessorDeclaration(pos, jsdoc, modifiers, ast.KindSetAccessor, ParseFlagsType);
    }
    if (Ꮡp.isIndexSignature()) {
        return Ꮡp.parseIndexSignatureDeclaration(pos, jsdoc, modifiers);
    }
    return Ꮡp.parsePropertyOrMethodSignature(pos, jsdoc, modifiers);
}

internal static bool nextTokenIsOpenParenOrLessThan(this ref Parser p) {
    p.nextToken();
    return p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken;
}

internal static ж<ast.Node> parseSignatureMember(this ж<Parser> Ꮡp, ast.Kind kind) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    if (kind == ast.KindConstructSignature) {
        p.parseExpected(ast.KindNewKeyword);
    }
    var typeParameters = Ꮡp.parseTypeParameters();
    var parameters = Ꮡp.parseParameters(ParseFlagsType);
    var typeNode = Ꮡp.parseReturnType(ast.KindColonToken, true);
    p.parseTypeMemberSemicolon();
    ж<ast.Node> result = default;
    if (kind == ast.KindCallSignature) {
        result = p.factory.NewCallSignatureDeclaration(typeParameters, parameters, typeNode);
    } else {
        result = p.factory.NewConstructSignatureDeclaration(typeParameters, parameters, typeNode);
    }
    p.finishNode(result, pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.NodeList> parseTypeParameters(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindLessThanToken) {
        return Ꮡp.parseBracketedList(PCTypeParameters, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseTypeParameter)), ast.KindLessThanToken, ast.KindGreaterThanToken);
    }
    return default;
}

internal static ж<ast.Node> parseTypeParameter(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var modifiers = Ꮡp.parseModifiersEx(false, true, false);
    var name = p.parseIdentifier();
    ж<astꓸTypeNode> constraint = default;
    ж<astꓸExpression> expression = default;
    if (p.parseOptional(ast.KindExtendsKeyword)) {
        if (Ꮡp.isStartOfType(false) || !Ꮡp.isStartOfExpression()) {
            constraint = Ꮡp.parseType();
        } else {
            expression = Ꮡp.parseUnaryExpressionOrHigher();
        }
    }
    ж<astꓸTypeNode> defaultType = default;
    if (p.parseOptional(ast.KindEqualsToken)) {
        defaultType = Ꮡp.parseType();
    }
    var result = p.factory.NewTypeParameterDeclaration(modifiers, name, constraint, expression, defaultType);
    return p.finishNode(result, pos);
}

internal static ж<ast.NodeList> parseParameters(this ж<Parser> Ꮡp, ParseFlags flags) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.parseExpected(ast.KindOpenParenToken)) {
        var parameters = Ꮡp.parseParametersWorker(flags, true);
        p.parseExpected(ast.KindCloseParenToken);
        return parameters;
    }
    return p.createMissingList();
}

internal static ж<ast.NodeList> parseParametersWorker(this ж<Parser> Ꮡp, ParseFlags flags, bool allowAmbiguity) {
    ref var p = ref Ꮡp.DerefOrNull();

    var inAwaitContext = (ast.NodeFlags)(p.contextFlags & ast.NodeFlagsAwaitContext) != 0;
    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsYieldContext, (ParseFlags)(flags & ParseFlagsYield) != 0);
    p.setContextFlags(ast.NodeFlagsAwaitContext, (ParseFlags)(flags & ParseFlagsAwait) != 0);
    var parameters = Ꮡp.parseDelimitedList(PCParameters, (ж<Parser> pΔ1) => {
        var parameter = pΔ1.parseParameterEx(inAwaitContext, allowAmbiguity);
        if (parameter != nil && (ParseFlags)(flags & ParseFlagsType) == 0) {
            pΔ1.checkJSSyntax(parameter);
        }
        return parameter;
    });
    p.contextFlags = saveContextFlags;
    return parameters;
}

internal static ж<ast.Node> parseParameter(this ж<Parser> Ꮡp) {
    return Ꮡp.parseParameterEx(false, true);
}

internal static ж<ast.Node> parseParameterEx(this ж<Parser> Ꮡp, bool inOuterAwaitContext, bool allowAmbiguity) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsAwaitContext, inOuterAwaitContext);
    var modifiers = Ꮡp.parseModifiersEx(true, false, false);
    p.contextFlags = saveContextFlags;
    if (p.token == ast.KindThisKeyword) {
        var resultΔ1 = p.factory.NewParameterDeclaration(
            modifiers,
            nil,
            p.createIdentifier(true),
            nil,
            Ꮡp.parseTypeAnnotation(),
            nil);
        if (modifiers != nil) {
            p.parseErrorAtRange(modifiers.Nodes[0].Loc, diagnostics.Neither_decorators_nor_modifiers_may_be_applied_to_this_parameters);
        }
        Ꮡp.withJSDoc(p.finishNode(resultΔ1, pos), jsdoc);
        return resultΔ1;
    }
    var dotDotDotToken = p.parseOptionalToken(ast.KindDotDotDotToken);
    if (!allowAmbiguity && !p.isParameterNameStart()) {
        return default;
    }
    var result = p.factory.NewParameterDeclaration(
        modifiers,
        dotDotDotToken,
        Ꮡp.parseNameOfParameter(modifiers),
        p.parseOptionalToken(ast.KindQuestionToken),
        Ꮡp.parseTypeAnnotation(),
        Ꮡp.parseInitializer());
    Ꮡp.withJSDoc(p.finishNode(result, pos), jsdoc);
    return result;
}

internal static bool isParameterNameStart(this ref Parser p) {
    return p.isBindingIdentifier() || p.token == ast.KindOpenBracketToken || p.token == ast.KindOpenBraceToken;
}

internal static ж<ast.Node> parseNameOfParameter(this ж<Parser> Ꮡp, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var name = Ꮡp.parseIdentifierOrPatternWithDiagnostic(diagnostics.Private_identifiers_cannot_be_used_as_parameters);
    if (name.Loc.Len() == 0 && modifiers == nil && ast.IsModifierKind(p.token)) {
        p.nextToken();
    }
    return name;
}

internal static ж<astꓸTypeNode> parseReturnType(this ж<Parser> Ꮡp, ast.Kind returnToken, bool isType) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.shouldParseReturnType(returnToken, isType)) {
        return doInContext<ж<astꓸTypeNode>>(Ꮡp, ast.NodeFlagsDisallowConditionalTypesContext, false, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseTypeOrTypePredicate)));
    }
    return default;
}

internal static bool shouldParseReturnType(this ref Parser p, ast.Kind returnToken, bool isType) {
    if (returnToken == ast.KindEqualsGreaterThanToken) {
        p.parseExpected(returnToken);
        return true;
    } else if (p.parseOptional(ast.KindColonToken)) {
        return true;
    } else if (isType && p.token == ast.KindEqualsGreaterThanToken) {
        p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(ast.KindColonToken));
        p.nextToken();
        return true;
    }
    return false;
}

internal static ж<astꓸTypeNode> parseTypeOrTypePredicate(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.isIdentifier()) {
        var state = p.mark();
        nint pos = p.nodePos();
        var id = p.parseIdentifier();
        if (p.token == ast.KindIsKeyword && !p.hasPrecedingLineBreak()) {
            p.nextToken();
            return p.finishNode(p.factory.NewTypePredicateNode(nil, id, Ꮡp.parseType()), pos);
        }
        p.rewind(state);
    }
    return Ꮡp.parseType();
}

internal static void parseTypeMemberSemicolon(this ref Parser p) {
    if (p.parseOptional(ast.KindCommaToken)) {
        return;
    }
    p.parseSemicolon();
}

internal static ж<ast.Node> parseAccessorDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers, ast.Kind kind, ParseFlags flags) {
    ref var p = ref Ꮡp.DerefOrNull();

    var name = Ꮡp.parsePropertyName();
    var typeParameters = Ꮡp.parseTypeParameters();
    var parameters = Ꮡp.parseParameters(ParseFlagsNone);
    var returnType = Ꮡp.parseReturnType(ast.KindColonToken, false);
    var body = Ꮡp.parseFunctionBlockOrSemicolon(flags, nil);
    ж<ast.Node> result = default;
    if (kind == ast.KindGetAccessor) {
        result = p.factory.NewGetAccessorDeclaration(modifiers, name, typeParameters, parameters, returnType, nil, body);
    } else {
        result = p.factory.NewSetAccessorDeclaration(modifiers, name, typeParameters, parameters, returnType, nil, body);
    }
    Ꮡp.withJSDoc(p.finishNode(result, pos), jsdoc);
    if ((ParseFlags)(flags & ParseFlagsType) == 0) {
        p.checkJSSyntax(result);
    }
    return result;
}

internal static ж<ast.Node> parsePropertyName(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    var prop = Ꮡp.parsePropertyNameWorker(true);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return prop;
}

internal static ж<ast.Node> parsePropertyNameWorker(this ж<Parser> Ꮡp, bool allowComputedPropertyNames) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindStringLiteral || p.token == ast.KindNumericLiteral || p.token == ast.KindBigIntLiteral) {
        return p.parseLiteralExpression();
    }
    if (allowComputedPropertyNames && p.token == ast.KindOpenBracketToken) {
        return Ꮡp.parseComputedPropertyName();
    }
    if (p.token == ast.KindPrivateIdentifier) {
        return p.parsePrivateIdentifier();
    }
    return p.parseIdentifierName();
}

internal static ж<ast.Node> parseComputedPropertyName(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindOpenBracketToken);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpected(ast.KindCloseBracketToken);
    return p.finishNode(p.factory.NewComputedPropertyName(expression), pos);
}

internal static ж<ast.Node> parseFunctionBlockOrSemicolon(this ж<Parser> Ꮡp, ParseFlags flags, ж<diagnostics.Message> diagnosticMessage) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token != ast.KindOpenBraceToken) {
        if ((ParseFlags)(flags & ParseFlagsType) != 0) {
            p.parseTypeMemberSemicolon();
            return default;
        }
        if (p.canParseSemicolon()) {
            p.parseSemicolon();
            return default;
        }
    }
    return Ꮡp.parseFunctionBlock(flags, diagnosticMessage);
}

internal static ж<ast.Node> parseFunctionBlock(this ж<Parser> Ꮡp, ParseFlags flags, ж<diagnostics.Message> diagnosticMessage) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    p.setContextFlags(ast.NodeFlagsYieldContext, (ParseFlags)(flags & ParseFlagsYield) != 0);
    p.setContextFlags(ast.NodeFlagsAwaitContext, (ParseFlags)(flags & ParseFlagsAwait) != 0);
    p.setContextFlags(ast.NodeFlagsDecoratorContext, false);
    var block = Ꮡp.parseBlock((ParseFlags)(flags & ParseFlagsIgnoreMissingOpenBrace) != 0, diagnosticMessage);
    p.contextFlags = saveContextFlags;
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return block;
}

internal static bool isIndexSignature(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.token == ast.KindOpenBracketToken && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsUnambiguouslyIndexSignature)));
}

internal static bool nextIsUnambiguouslyIndexSignature(this ref Parser p) {
    p.nextToken();
    if (p.token == ast.KindDotDotDotToken || p.token == ast.KindCloseBracketToken) {
        return true;
    }
    if (ast.IsModifierKind(p.token)) {
        p.nextToken();
        if (p.isIdentifier()) {
            return true;
        }
    } else if (!p.isIdentifier()) {
        return false;
    } else {
        p.nextToken();
    }
    if (p.token == ast.KindColonToken || p.token == ast.KindCommaToken) {
        return true;
    }
    if (p.token != ast.KindQuestionToken) {
        return false;
    }
    p.nextToken();
    return p.token == ast.KindColonToken || p.token == ast.KindCommaToken || p.token == ast.KindCloseBracketToken;
}

internal static ж<ast.Node> parseIndexSignatureDeclaration(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var parameters = Ꮡp.parseBracketedList(PCParameters, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseParameter)), ast.KindOpenBracketToken, ast.KindCloseBracketToken);
    var typeNode = Ꮡp.parseTypeAnnotation();
    p.parseTypeMemberSemicolon();
    var result = p.finishNode(p.factory.NewIndexSignatureDeclaration(modifiers, parameters, typeNode), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.Node> parsePropertyOrMethodSignature(this ж<Parser> Ꮡp, nint pos, jsdocScannerInfo jsdoc, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();

    var name = Ꮡp.parsePropertyName();
    var questionToken = p.parseOptionalToken(ast.KindQuestionToken);
    ж<ast.Node> result = default;
    if (p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken) {
        var typeParameters = Ꮡp.parseTypeParameters();
        var parameters = Ꮡp.parseParameters(ParseFlagsType);
        var returnType = Ꮡp.parseReturnType(ast.KindColonToken, true);
        result = p.factory.NewMethodSignatureDeclaration(modifiers, name, questionToken, typeParameters, parameters, returnType);
    } else {
        var typeNode = Ꮡp.parseTypeAnnotation();
        ж<astꓸExpression> initializer = default;
        if (p.token == ast.KindEqualsToken) {
            initializer = Ꮡp.parseInitializer();
        }
        result = p.factory.NewPropertySignatureDeclaration(modifiers, name, questionToken, typeNode, initializer);
    }
    p.parseTypeMemberSemicolon();
    Ꮡp.withJSDoc(p.finishNode(result, pos), jsdoc);
    return result;
}

internal static ж<ast.Node> parseTypeLiteral(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var result = p.finishNode(p.factory.NewTypeLiteralNode(Ꮡp.parseObjectTypeMembers()), pos);
    return result;
}

internal static ж<ast.NodeList> parseObjectTypeMembers(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.parseExpected(ast.KindOpenBraceToken)) {
        var members = Ꮡp.parseList(PCTypeMembers, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseTypeMember)));
        p.parseExpected(ast.KindCloseBraceToken);
        return members;
    }
    return p.createMissingList();
}

internal static ж<ast.Node> parseTupleType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    return p.finishNode(p.factory.NewTupleTypeNode(Ꮡp.parseBracketedList(PCTupleElementTypes, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseTupleElementNameOrTupleElementType)), ast.KindOpenBracketToken, ast.KindCloseBracketToken)), pos);
}

internal static ж<ast.Node> parseTupleElementNameOrTupleElementType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.scanStartOfNamedTupleElement)))) {
        nint pos = p.nodePos();
        var jsdoc = p.jsdocScannerInfo();
        var dotDotDotToken = p.parseOptionalToken(ast.KindDotDotDotToken);
        var name = p.parseIdentifierName();
        var questionToken = p.parseOptionalToken(ast.KindQuestionToken);
        p.parseExpected(ast.KindColonToken);
        var typeNode = Ꮡp.parseTupleElementType();
        var result = p.finishNode(p.factory.NewNamedTupleMember(dotDotDotToken, name, questionToken, typeNode), pos);
        Ꮡp.withJSDoc(result, jsdoc);
        return result;
    }
    return Ꮡp.parseTupleElementType();
}

internal static bool scanStartOfNamedTupleElement(this ref Parser p) {
    if (p.token == ast.KindDotDotDotToken) {
        return tokenIsIdentifierOrKeyword(p.nextToken()) && p.nextTokenIsColonOrQuestionColon();
    }
    return tokenIsIdentifierOrKeyword(p.token) && p.nextTokenIsColonOrQuestionColon();
}

internal static bool nextTokenIsColonOrQuestionColon(this ref Parser p) {
    return p.nextToken() == ast.KindColonToken || p.token == ast.KindQuestionToken && p.nextToken() == ast.KindColonToken;
}

internal static ж<astꓸTypeNode> parseTupleElementType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    if (p.parseOptional(ast.KindDotDotDotToken)) {
        return p.finishNode(p.factory.NewRestTypeNode(Ꮡp.parseType()), pos);
    }
    var typeNode = Ꮡp.parseType();
    if (ast.IsJSDocNullableType(typeNode) && typeNode.Pos() == typeNode.Type().Pos()) {
        var node = p.factory.NewOptionalTypeNode(typeNode.Type());
        node.Flags = typeNode.Flags;
        node.Loc = typeNode.Loc;
        typeNode.Type().Parent = node;
        return node;
    }
    return typeNode;
}

internal static ж<ast.Node> parseParenthesizedType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindOpenParenToken);
    var typeNode = Ꮡp.parseType();
    p.parseExpected(ast.KindCloseParenToken);
    return p.finishNode(p.factory.NewParenthesizedTypeNode(typeNode), pos);
}

internal static ж<astꓸTypeNode> parseAssertsTypePredicate(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var assertsModifier = p.parseExpectedToken(ast.KindAssertsKeyword);
    ж<ast.Node> parameterName = default;
    if (p.token == ast.KindThisKeyword) {
        parameterName = p.parseThisTypeNode();
    } else {
        parameterName = p.parseIdentifier();
    }
    ж<astꓸTypeNode> typeNode = default;
    if (p.parseOptional(ast.KindIsKeyword)) {
        typeNode = Ꮡp.parseType();
    }
    return p.finishNode(p.factory.NewTypePredicateNode(assertsModifier, parameterName, typeNode), pos);
}

internal static ж<ast.Node> parseTemplateType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    return p.finishNode(p.factory.NewTemplateLiteralTypeNode(p.parseTemplateHead(false), Ꮡp.parseTemplateTypeSpans()), pos);
}

internal static ж<ast.Node> parseTemplateHead(this ref Parser p, bool isTaggedTemplate) {
    if (!isTaggedTemplate && (ast.TokenFlags)(p.scanner.TokenFlags() & ast.TokenFlagsIsInvalid) != 0) {
        p.reScanTemplateToken(false);
    }
    nint pos = p.nodePos();
    var result = p.factory.NewTemplateHead(p.scanner.TokenValue(), p.getTemplateLiteralRawText(2), p.scanner.TokenFlags());
    p.nextToken();
    return p.finishNode(result, pos);
}

internal static @string getTemplateLiteralRawText(this ref Parser p, nint endLength) {
    @string tokenText = p.scanner.TokenText();
    if ((ast.TokenFlags)(p.scanner.TokenFlags() & ast.TokenFlagsUnterminated) != 0) {
        endLength = 0;
    }
    return tokenText.slice(1, len(tokenText) - endLength);
}

internal static ж<ast.NodeList> parseTemplateTypeSpans(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    slice<ж<ast.Node>> list = default;
    for (;;) {
        var span = Ꮡp.parseTemplateTypeSpan();
        list = append(list, span);
        if (span.AsTemplateLiteralTypeSpan().Literal.Kind != ast.KindTemplateMiddle) {
            break;
        }
    }
    return p.newNodeList(core.NewTextRange(pos, p.nodePos()), list);
}

internal static ж<ast.Node> parseTemplateTypeSpan(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    return p.finishNode(p.factory.NewTemplateLiteralTypeSpan(Ꮡp.parseType(), p.parseLiteralOfTemplateSpan(false)), pos);
}

internal static ж<ast.Node> parseLiteralOfTemplateSpan(this ref Parser p, bool isTaggedTemplate) {
    if (p.token == ast.KindCloseBraceToken) {
        p.reScanTemplateToken(isTaggedTemplate);
        return p.parseTemplateMiddleOrTail();
    }
    p.parseErrorAtCurrentToken(diagnostics.X_0_expected, scanner.TokenToString(ast.KindCloseBraceToken));
    return p.finishNode(p.factory.NewTemplateTail("", "", ast.TokenFlagsNone), p.nodePos());
}

internal static ж<ast.Node> parseTemplateMiddleOrTail(this ref Parser p) {
    nint pos = p.nodePos();
    ж<ast.Node> result = default;
    if (p.token == ast.KindTemplateMiddle) {
        result = p.factory.NewTemplateMiddle(p.scanner.TokenValue(), p.getTemplateLiteralRawText(2), p.scanner.TokenFlags());
    } else {
        result = p.factory.NewTemplateTail(p.scanner.TokenValue(), p.getTemplateLiteralRawText(1), p.scanner.TokenFlags());
    }
    p.nextToken();
    return p.finishNode(result, pos);
}

internal static ж<astꓸTypeNode> parseFunctionOrConstructorTypeToError(this ж<Parser> Ꮡp, bool isInUnionType, Func<ж<Parser>, ж<astꓸTypeNode>> parseConstituentType) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp.isStartOfFunctionTypeOrConstructorType()) {
        var typeNode = Ꮡp.parseFunctionOrConstructorType();
        ж<diagnostics.Message> diagnostic = default;
        if (typeNode.Kind == ast.KindFunctionType) {
            diagnostic = core.IfElse(isInUnionType,
                diagnostics.Function_type_notation_must_be_parenthesized_when_used_in_a_union_type,
                diagnostics.Function_type_notation_must_be_parenthesized_when_used_in_an_intersection_type);
        } else {
            diagnostic = core.IfElse(isInUnionType,
                diagnostics.Constructor_type_notation_must_be_parenthesized_when_used_in_a_union_type,
                diagnostics.Constructor_type_notation_must_be_parenthesized_when_used_in_an_intersection_type);
        }
        p.parseErrorAtRange(typeNode.Loc, diagnostic);
        return typeNode;
    }
    return parseConstituentType(Ꮡp);
}

internal static bool isStartOfFunctionTypeOrConstructorType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.token == ast.KindLessThanToken || p.token == ast.KindOpenParenToken && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsUnambiguouslyStartOfFunctionType))) || p.token == ast.KindNewKeyword || p.token == ast.KindAbstractKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsNewKeyword)));
}

internal static ж<astꓸTypeNode> parseFunctionOrConstructorType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var modifiers = p.parseModifiersForConstructorType();
    var isConstructorType = p.parseOptional(ast.KindNewKeyword);
    debug.Assert(modifiers == nil || isConstructorType, "Per isStartOfFunctionOrConstructorType, a function type cannot have modifiers.");
    var typeParameters = Ꮡp.parseTypeParameters();
    var parameters = Ꮡp.parseParameters(ParseFlagsType);
    var returnType = Ꮡp.parseReturnType(ast.KindEqualsGreaterThanToken, false);
    ж<astꓸTypeNode> result = default;
    if (isConstructorType) {
        result = p.factory.NewConstructorTypeNode(modifiers, typeParameters, parameters, returnType);
    } else {
        result = p.factory.NewFunctionTypeNode(typeParameters, parameters, returnType);
    }
    p.finishNode(result, pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<ast.ModifierList> parseModifiersForConstructorType(this ref Parser p) {
    if (p.token == ast.KindAbstractKeyword) {
        nint pos = p.nodePos();
        var modifier = p.factory.NewModifier(p.token);
        p.nextToken();
        p.finishNode(modifier, pos);
        return p.newModifierList(modifier.Loc, p.nodeSliceArena.NewSlice1(modifier));
    }
    return default;
}

internal static bool nextTokenIsNewKeyword(this ref Parser p) {
    return p.nextToken() == ast.KindNewKeyword;
}

internal static bool nextIsUnambiguouslyStartOfFunctionType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.nextToken();
    if (p.token == ast.KindCloseParenToken || p.token == ast.KindDotDotDotToken) {
        return true;
    }
    if (Ꮡp.skipParameterStart()) {
        if (p.token == ast.KindColonToken || p.token == ast.KindCommaToken || p.token == ast.KindQuestionToken || p.token == ast.KindEqualsToken) {
            return true;
        }
        if (p.token == ast.KindCloseParenToken && p.nextToken() == ast.KindEqualsGreaterThanToken) {
            return true;
        }
    }
    return false;
}

internal static bool skipParameterStart(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (ast.IsModifierKind(p.token)) {
        Ꮡp.parseModifiers();
    }
    p.parseOptional(ast.KindDotDotDotToken);
    if (p.isIdentifier() || p.token == ast.KindThisKeyword) {
        p.nextToken();
        return true;
    }
    if (p.token == ast.KindOpenBracketToken || p.token == ast.KindOpenBraceToken) {
        nint previousErrorCount = len(p.diagnostics);
        Ꮡp.parseIdentifierOrPattern();
        return previousErrorCount == len(p.diagnostics);
    }
    return false;
}

internal static ж<ast.ModifierList> parseModifiers(this ж<Parser> Ꮡp) {
    return Ꮡp.parseModifiersEx(false, false, false);
}

internal static ж<ast.ModifierList> parseModifiersEx(this ж<Parser> Ꮡp, bool allowDecorators, bool permitConstAsModifier, bool stopOnStartOfClassStaticBlock) {
    ref var p = ref Ꮡp.DerefOrNull();
    var listᴷ = new stackAppend<ж<ast.Node>>();

    bool hasLeadingModifier = default;
    bool hasTrailingDecorator = default;
    bool hasTrailingModifier = default;
    bool hasStaticModifier = default;
    nint pos = p.nodePos();
    var list = new slice<ж<ast.Node>>(0, 16);
    for (;;) {
        if (allowDecorators && p.token == ast.KindAtToken && !hasTrailingModifier) {
            var decorator = Ꮡp.parseDecorator();
            list = append(listᴷ, list, decorator);
            if (hasLeadingModifier) {
                hasTrailingDecorator = true;
            }
        } else {
            var modifier = Ꮡp.tryParseModifier(hasStaticModifier, permitConstAsModifier, stopOnStartOfClassStaticBlock);
            if (modifier == nil) {
                break;
            }
            if (modifier.Kind == ast.KindStaticKeyword) {
                hasStaticModifier = true;
            }
            list = append(list, modifier);
            if (hasTrailingDecorator) {
                hasTrailingModifier = true;
            } else {
                hasLeadingModifier = true;
            }
        }
    }
    if (len(list) != 0) {
        return p.newModifierList(core.NewTextRange(pos, p.nodePos()), p.nodeSliceArena.Clone(list));
    }
    return default;
}

internal static ж<ast.Node> parseDecorator(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindAtToken);
    var expression = doInContext<ж<astꓸExpression>>(Ꮡp, ast.NodeFlagsDecoratorContext, true, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseDecoratorExpression)));
    return p.finishNode(p.factory.NewDecorator(expression), pos);
}

internal static ж<astꓸExpression> parseDecoratorExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.inAwaitContext() && p.token == ast.KindAwaitKeyword) {
        nint pos = p.nodePos();
        var awaitExpression = p.parseIdentifierWithDiagnostic(diagnostics.Expression_expected, nil);
        p.nextToken();
        var memberExpression = Ꮡp.parseMemberExpressionRest(pos, awaitExpression, true);
        return Ꮡp.parseCallExpressionRest(pos, memberExpression);
    }
    return Ꮡp.parseLeftHandSideExpressionOrHigher();
}

internal static ж<ast.Node> tryParseModifier(this ж<Parser> Ꮡp, bool hasSeenStaticModifier, bool permitConstAsModifier, bool stopOnStartOfClassStaticBlock) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var kind = p.token;
    if (p.token == ast.KindConstKeyword && permitConstAsModifier) {
        if (!Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOnSameLineAndCanFollowModifier)))) {
            return default;
        } else {
            p.nextToken();
        }
    } else if (stopOnStartOfClassStaticBlock && p.token == ast.KindStaticKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOpenBrace)))) {
        return default;
    } else if (hasSeenStaticModifier && p.token == ast.KindStaticKeyword) {
        return default;
    } else {
        if (!Ꮡp.parseAnyContextualModifier()) {
            return default;
        }
    }
    return p.finishNode(p.factory.NewModifier(kind), pos);
}

internal static bool parseContextualModifier(this ж<Parser> Ꮡp, ast.Kind t) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.mark();
    if (p.token == t && Ꮡp.nextTokenCanFollowModifier()) {
        return true;
    }
    p.rewind(state);
    return false;
}

internal static bool parseAnyContextualModifier(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var state = p.mark();
    if (ast.IsModifierKind(p.token) && Ꮡp.nextTokenCanFollowModifier()) {
        return true;
    }
    p.rewind(state);
    return false;
}

internal static bool nextTokenCanFollowModifier(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindConstKeyword: {
        return p.nextToken() == ast.KindEnumKeyword;
    }
    case var ᴛ when ᴛ == ast.KindExportKeyword: {
        p.nextToken();
        if (p.token == ast.KindDefaultKeyword) {
            return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenCanFollowDefaultKeyword)));
        }
        if (p.token == ast.KindTypeKeyword) {
            return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenCanFollowExportModifier)));
        }
        return p.canFollowExportModifier();
    }
    case var ᴛ when ᴛ == ast.KindDefaultKeyword: {
        return Ꮡp.nextTokenCanFollowDefaultKeyword();
    }
    case var ᴛ when ᴛ == ast.KindStaticKeyword: {
        p.nextToken();
        return p.canFollowModifier();
    }
    case var ᴛ when ᴛ == ast.KindGetKeyword || ᴛ == ast.KindSetKeyword: {
        p.nextToken();
        return p.canFollowGetOrSetKeyword();
    }
    default: {
        return p.nextTokenIsOnSameLineAndCanFollowModifier();
    }}

}

internal static bool nextTokenCanFollowDefaultKeyword(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.nextToken()) {
    case var ᴛ when ᴛ == ast.KindClassKeyword || ᴛ == ast.KindFunctionKeyword || ᴛ == ast.KindInterfaceKeyword || ᴛ == ast.KindAtToken: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindAbstractKeyword: {
        return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsClassKeywordOnSameLine)));
    }
    case var ᴛ when ᴛ == ast.KindAsyncKeyword: {
        return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsFunctionKeywordOnSameLine)));
    }}

    return false;
}

internal static bool nextTokenIsIdentifierOrKeyword(this ref Parser p) {
    return tokenIsIdentifierOrKeyword(p.nextToken());
}

internal static bool nextTokenIsIdentifierOrKeywordOrGreaterThan(this ref Parser p) {
    return tokenIsIdentifierOrKeywordOrGreaterThan(p.nextToken());
}

internal static bool nextTokenIsIdentifierOrKeywordOnSameLine(this ref Parser p) {
    return p.nextTokenIsIdentifierOrKeyword() && !p.hasPrecedingLineBreak();
}

internal static bool nextTokenIsIdentifierOrKeywordOrLiteralOnSameLine(this ref Parser p) {
    return (p.nextTokenIsIdentifierOrKeyword() || p.token == ast.KindNumericLiteral || p.token == ast.KindBigIntLiteral || p.token == ast.KindStringLiteral) && !p.hasPrecedingLineBreak();
}

internal static bool nextTokenIsClassKeywordOnSameLine(this ref Parser p) {
    return p.nextToken() == ast.KindClassKeyword && !p.hasPrecedingLineBreak();
}

internal static bool nextTokenIsFunctionKeywordOnSameLine(this ref Parser p) {
    return p.nextToken() == ast.KindFunctionKeyword && !p.hasPrecedingLineBreak();
}

internal static bool nextTokenCanFollowExportModifier(this ref Parser p) {
    p.nextToken();
    return p.canFollowExportModifier();
}

internal static bool canFollowExportModifier(this ref Parser p) {
    return p.token == ast.KindAtToken || p.token != ast.KindAsteriskToken && p.token != ast.KindAsKeyword && p.token != ast.KindOpenBraceToken && p.canFollowModifier();
}

internal static bool canFollowModifier(this ref Parser p) {
    return p.token == ast.KindOpenBracketToken || p.token == ast.KindOpenBraceToken || p.token == ast.KindAsteriskToken || p.token == ast.KindDotDotDotToken || p.isLiteralPropertyName();
}

internal static bool canFollowGetOrSetKeyword(this ref Parser p) {
    return p.token == ast.KindOpenBracketToken || p.isLiteralPropertyName();
}

internal static bool nextTokenIsOnSameLineAndCanFollowModifier(this ref Parser p) {
    p.nextToken();
    if (p.hasPrecedingLineBreak()) {
        return false;
    }
    return p.canFollowModifier();
}

internal static bool nextTokenIsOpenBrace(this ref Parser p) {
    return p.nextToken() == ast.KindOpenBraceToken;
}

internal static ж<astꓸExpression> parseExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    p.contextFlags &= unchecked((ast.NodeFlags)~(ast.NodeFlags)(ast.NodeFlagsDecoratorContext));
    nint pos = p.nodePos();
    var expr = Ꮡp.parseAssignmentExpressionOrHigher();
    for (;;) {
        var operatorToken = p.parseOptionalToken(ast.KindCommaToken);
        if (operatorToken == nil) {
            break;
        }
        expr = p.makeBinaryExpression(expr, operatorToken, Ꮡp.parseAssignmentExpressionOrHigher(), pos);
    }
    p.contextFlags = saveContextFlags;
    return expr;
}

internal static ж<astꓸExpression> parseExpressionAllowIn(this ж<Parser> Ꮡp) {
    return doInContext<ж<astꓸExpression>>(Ꮡp, ast.NodeFlagsDisallowInContext, false, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseExpression)));
}

internal static ж<astꓸExpression> parseAssignmentExpressionOrHigher(this ж<Parser> Ꮡp) {
    return Ꮡp.parseAssignmentExpressionOrHigherWorker(true);
}

internal static ж<astꓸExpression> parseAssignmentExpressionOrHigherWorker(this ж<Parser> Ꮡp, bool allowReturnTypeInArrowFunction) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp.isYieldExpression()) {
        return Ꮡp.parseYieldExpression();
    }
    var arrowExpression = Ꮡp.tryParseParenthesizedArrowFunctionExpression(allowReturnTypeInArrowFunction);
    if (arrowExpression != nil) {
        return arrowExpression;
    }
    arrowExpression = Ꮡp.tryParseAsyncSimpleArrowFunctionExpression(allowReturnTypeInArrowFunction);
    if (arrowExpression != nil) {
        return arrowExpression;
    }
    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var expr = Ꮡp.parseBinaryExpressionOrHigher(ast.OperatorPrecedenceLowest);
    if (expr.Kind == ast.KindIdentifier && p.token == ast.KindEqualsGreaterThanToken) {
        return Ꮡp.parseSimpleArrowFunctionExpression(pos, expr, allowReturnTypeInArrowFunction, jsdoc, nil);
    }
    if (ast.IsLeftHandSideExpression(expr) && ast.IsAssignmentOperator(p.reScanGreaterThanToken())) {
        return p.makeBinaryExpression(expr, p.parseTokenNode(), Ꮡp.parseAssignmentExpressionOrHigherWorker(allowReturnTypeInArrowFunction), pos);
    }
    return Ꮡp.parseConditionalExpressionRest(expr, pos, allowReturnTypeInArrowFunction);
}

internal static bool isYieldExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindYieldKeyword) {
        if (p.inYieldContext()) {
            return true;
        }

        return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeywordOrLiteralOnSameLine)));
    }
    return false;
}

internal static ж<ast.Node> parseYieldExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.nextToken();
    ж<ast.Node> result = default;
    if (!p.hasPrecedingLineBreak() && (p.token == ast.KindAsteriskToken || Ꮡp.isStartOfExpression())) {
        result = p.factory.NewYieldExpression(p.parseOptionalToken(ast.KindAsteriskToken), Ꮡp.parseAssignmentExpressionOrHigher());
    } else {
        result = p.factory.NewYieldExpression(nil, nil);
    }
    return p.finishNode(result, pos);
}

internal static core.Tristate isParenthesizedArrowFunctionExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken || p.token == ast.KindAsyncKeyword) {
        var state = p.mark();
        var result = Ꮡp.nextIsParenthesizedArrowFunctionExpression();
        p.rewind(state);
        return result;
    }
    if (p.token == ast.KindEqualsGreaterThanToken) {
        return core.TSTrue;
    }
    return core.TSFalse;
}

internal static core.Tristate nextIsParenthesizedArrowFunctionExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindAsyncKeyword) {
        p.nextToken();
        if (p.hasPrecedingLineBreak()) {
            return core.TSFalse;
        }
        if (p.token != ast.KindOpenParenToken && p.token != ast.KindLessThanToken) {
            return core.TSFalse;
        }
    }
    var first = p.token;
    var second = p.nextToken();
    if (first == ast.KindOpenParenToken) {
        if (second == ast.KindCloseParenToken) {
            var third = p.nextToken();
            switch (third) {
            case var ᴛ when ᴛ == ast.KindEqualsGreaterThanToken || ᴛ == ast.KindColonToken || ᴛ == ast.KindOpenBraceToken: {
                return core.TSTrue;
            }}

            return core.TSFalse;
        }
        if (second == ast.KindOpenBracketToken || second == ast.KindOpenBraceToken) {
            return core.TSUnknown;
        }
        if (second == ast.KindDotDotDotToken) {
            return core.TSTrue;
        }
        if (ast.IsModifierKind(second) && second != ast.KindAsyncKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifier)))) {
            if (p.nextToken() == ast.KindAsKeyword) {
                return core.TSFalse;
            }
            return core.TSTrue;
        }
        if (!p.isIdentifier() && second != ast.KindThisKeyword) {
            return core.TSFalse;
        }
        switch (p.nextToken()) {
        case var ᴛ when ᴛ == ast.KindColonToken: {
            return core.TSTrue;
        }
        case var ᴛ when ᴛ == ast.KindQuestionToken: {
            p.nextToken();
            if (p.token == ast.KindColonToken || p.token == ast.KindCommaToken || p.token == ast.KindEqualsToken || p.token == ast.KindCloseParenToken) {
                return core.TSTrue;
            }
            return core.TSFalse;
        }
        case var ᴛ when ᴛ == ast.KindCommaToken || ᴛ == ast.KindEqualsToken || ᴛ == ast.KindCloseParenToken: {
            return core.TSUnknown;
        }}

        return core.TSFalse;
    } else {
        debug.Assert(first == ast.KindLessThanToken);
        if (!p.isIdentifier() && p.token != ast.KindConstKeyword) {
            return core.TSFalse;
        }
        if (p.languageVariant == core.LanguageVariantJSX) {
            var isArrowFunctionInJsx = Ꮡp.lookAhead((ж<Parser> pΔ1) => {
                pΔ1.parseOptional(ast.KindConstKeyword);
                var third = pΔ1.nextToken();
                if (third == ast.KindExtendsKeyword) {
                    var fourth = pΔ1.nextToken();
                    switch (fourth) {
                    case var ᴛ when ᴛ == ast.KindEqualsToken || ᴛ == ast.KindGreaterThanToken || ᴛ == ast.KindSlashToken: {
                        return false;
                    }}

                    return true;
                } else if (third == ast.KindCommaToken || third == ast.KindEqualsToken) {
                    return true;
                }
                return false;
            });
            if (isArrowFunctionInJsx) {
                return core.TSTrue;
            }
            return core.TSFalse;
        }
        return core.TSUnknown;
    }
}

internal static ж<ast.Node> tryParseParenthesizedArrowFunctionExpression(this ж<Parser> Ꮡp, bool allowReturnTypeInArrowFunction) {
    ref var p = ref Ꮡp.DerefOrNull();

    var tristate = Ꮡp.isParenthesizedArrowFunctionExpression();
    if (tristate == core.TSFalse) {
        return default;
    }
    if (tristate == core.TSTrue) {
        return Ꮡp.parseParenthesizedArrowFunctionExpression(true, true);
    }
    var state = p.mark();
    var result = Ꮡp.parsePossibleParenthesizedArrowFunctionExpression(allowReturnTypeInArrowFunction);
    if (result == nil) {
        p.rewind(state);
    }
    return result;
}

internal static ж<ast.Node> parseParenthesizedArrowFunctionExpression(this ж<Parser> Ꮡp, bool allowAmbiguity, bool allowReturnTypeInArrowFunction) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var modifiers = p.parseModifiersForArrowFunction();
    var isAsync = modifierListHasAsync(modifiers);
    var signatureFlags = core.IfElse(isAsync, ParseFlagsAwait, ParseFlagsNone);
    var typeParameters = Ꮡp.parseTypeParameters();
    ж<ast.NodeList> parameters = default;
    if (!p.parseExpected(ast.KindOpenParenToken)) {
        if (!allowAmbiguity) {
            return default;
        }
        parameters = p.createMissingList();
    } else {
        if (!allowAmbiguity) {
            var maybeParameters = Ꮡp.parseParametersWorker(signatureFlags, allowAmbiguity);
            if (maybeParameters == nil) {
                return default;
            }
            parameters = maybeParameters;
        } else {
            parameters = Ꮡp.parseParametersWorker(signatureFlags, allowAmbiguity);
        }
        if (!p.parseExpected(ast.KindCloseParenToken) && !allowAmbiguity) {
            return default;
        }
    }
    var hasReturnColon = p.token == ast.KindColonToken;
    var returnType = Ꮡp.parseReturnType(ast.KindColonToken, false);
    if (returnType != nil && !allowAmbiguity && typeHasArrowFunctionBlockingParseError(returnType)) {
        return default;
    }
    var unwrappedType = returnType;
    while (unwrappedType != nil && unwrappedType.Kind == ast.KindParenthesizedType) {
        unwrappedType = unwrappedType.Type();
    }
    if (!allowAmbiguity && p.token != ast.KindEqualsGreaterThanToken && p.token != ast.KindOpenBraceToken) {
        return default;
    }
    var lastToken = p.token;
    var equalsGreaterThanToken = p.parseExpectedToken(ast.KindEqualsGreaterThanToken);
    ж<ast.Node> body = default;
    if (lastToken == ast.KindEqualsGreaterThanToken || lastToken == ast.KindOpenBraceToken) {
        body = Ꮡp.parseArrowFunctionExpressionBody(isAsync, allowReturnTypeInArrowFunction);
    } else {
        body = p.parseIdentifier();
    }
    if (!allowReturnTypeInArrowFunction && hasReturnColon) {
        if (p.token != ast.KindColonToken) {
            return default;
        }
    }
    var result = p.finishNode(p.factory.NewArrowFunction(modifiers, typeParameters, parameters, returnType, nil, equalsGreaterThanToken, body), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.ModifierList> parseModifiersForArrowFunction(this ref Parser p) {
    if (p.token == ast.KindAsyncKeyword) {
        nint pos = p.nodePos();
        p.nextToken();
        var modifier = p.finishNode(p.factory.NewModifier(ast.KindAsyncKeyword), pos);
        return p.newModifierList(modifier.Loc, p.nodeSliceArena.NewSlice1(modifier));
    }
    return default;
}

} partial class parser_package {

internal static bool typeHasArrowFunctionBlockingParseError(ж<astꓸTypeNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindTypeReference: {
        return ast.NodeIsMissing(node.AsTypeReferenceNode().TypeName);
    }
    case var ᴛ when ᴛ == ast.KindFunctionType || ᴛ == ast.KindConstructorType: {
        return isMissingNodeList(node.FunctionLikeData().Parameters) || typeHasArrowFunctionBlockingParseError(node.Type());
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedType: {
        return typeHasArrowFunctionBlockingParseError(node.Type());
    }}

    return false;
}

} partial class parserᴛParser {

internal static ж<ast.Node> parseArrowFunctionExpressionBody(this ж<Parser> Ꮡp, bool isAsync, bool allowReturnTypeInArrowFunction) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindOpenBraceToken) {
        return Ꮡp.parseFunctionBlock(core.IfElse(isAsync, ParseFlagsAwait, ParseFlagsNone), nil);
    }
    if (p.token != ast.KindSemicolonToken && p.token != ast.KindFunctionKeyword && p.token != ast.KindClassKeyword && Ꮡp.isStartOfStatement() && !Ꮡp.isStartOfExpressionStatement()) {
        return Ꮡp.parseFunctionBlock((ParseFlags)(ParseFlagsIgnoreMissingOpenBrace | core.IfElse(isAsync, ParseFlagsAwait, ParseFlagsNone)), nil);
    }
    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsAwaitContext, isAsync);
    p.setContextFlags(ast.NodeFlagsYieldContext, false);
    var node = Ꮡp.parseAssignmentExpressionOrHigherWorker(allowReturnTypeInArrowFunction);
    p.contextFlags = saveContextFlags;
    return node;
}

internal static bool isStartOfExpressionStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.token != ast.KindOpenBraceToken && p.token != ast.KindFunctionKeyword && p.token != ast.KindClassKeyword && p.token != ast.KindAtToken && Ꮡp.isStartOfExpression();
}

internal static ж<ast.Node> parsePossibleParenthesizedArrowFunctionExpression(this ж<Parser> Ꮡp, bool allowReturnTypeInArrowFunction) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint tokenPos = p.scanner.TokenStart();
    if (Ꮡp.ᏑnotParenthesizedArrow.Has(tokenPos)) {
        return default;
    }
    var result = Ꮡp.parseParenthesizedArrowFunctionExpression(false, allowReturnTypeInArrowFunction);
    if (result == nil) {
        p.notParenthesizedArrow.Add(tokenPos);
    }
    return result;
}

internal static ж<ast.Node> tryParseAsyncSimpleArrowFunctionExpression(this ж<Parser> Ꮡp, bool allowReturnTypeInArrowFunction) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindAsyncKeyword && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsUnParenthesizedAsyncArrowFunction)))) {
        nint pos = p.nodePos();
        var jsdoc = p.jsdocScannerInfo();
        var asyncModifier = p.parseModifiersForArrowFunction();
        var expr = Ꮡp.parseBinaryExpressionOrHigher(ast.OperatorPrecedenceLowest);
        return Ꮡp.parseSimpleArrowFunctionExpression(pos, expr, allowReturnTypeInArrowFunction, jsdoc, asyncModifier);
    }
    return default;
}

internal static bool nextIsUnParenthesizedAsyncArrowFunction(this ref Parser p) {
    if (p.token == ast.KindAsyncKeyword) {
        p.nextToken();
        if (p.hasPrecedingLineBreak() || p.token == ast.KindEqualsGreaterThanToken) {
            return false;
        }
        if (!p.isIdentifier()) {
            return false;
        }
        p.nextTokenWithoutCheck();
        return !p.hasPrecedingLineBreak() && p.token == ast.KindEqualsGreaterThanToken;
    }
    return false;
}

internal static ж<ast.Node> parseSimpleArrowFunctionExpression(this ж<Parser> Ꮡp, nint pos, ж<ast.Node> Ꮡidentifier, bool allowReturnTypeInArrowFunction, jsdocScannerInfo jsdoc, ж<ast.ModifierList> asyncModifier) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var identifier = ref Ꮡidentifier.DerefOrNull();

    debug.Assert(p.token == ast.KindEqualsGreaterThanToken, "parseSimpleArrowFunctionExpression should only have been called if we had a =>");
    var parameter = p.finishNode(p.factory.NewParameterDeclaration(nil, nil, Ꮡidentifier, nil, nil, nil), identifier.Pos());
    var parameters = p.newNodeList(parameter.Loc, [parameter]);
    var equalsGreaterThanToken = p.parseExpectedToken(ast.KindEqualsGreaterThanToken);
    var body = Ꮡp.parseArrowFunctionExpressionBody(asyncModifier != nil, allowReturnTypeInArrowFunction);
    var result = p.finishNode(p.factory.NewArrowFunction(asyncModifier, nil, parameters, nil, nil, equalsGreaterThanToken, body), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<astꓸExpression> parseConditionalExpressionRest(this ж<Parser> Ꮡp, ж<astꓸExpression> leftOperand, nint pos, bool allowReturnTypeInArrowFunction) {
    ref var p = ref Ꮡp.DerefOrNull();

    var questionToken = p.parseOptionalToken(ast.KindQuestionToken);
    if (questionToken == nil) {
        return leftOperand;
    }
    var saveContextFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsDisallowInContext, false);
    var trueExpression = Ꮡp.parseAssignmentExpressionOrHigherWorker(false);
    p.contextFlags = saveContextFlags;
    var colonToken = p.parseExpectedToken(ast.KindColonToken);
    ж<astꓸExpression> falseExpression = default;
    if (ast.NodeIsPresent(colonToken)) {
        falseExpression = Ꮡp.parseAssignmentExpressionOrHigherWorker(allowReturnTypeInArrowFunction);
    } else {
        falseExpression = p.createMissingIdentifier();
    }
    return p.finishNode(p.factory.NewConditionalExpression(leftOperand, questionToken, trueExpression, colonToken, falseExpression), pos);
}

internal static ж<astꓸExpression> parseBinaryExpressionOrHigher(this ж<Parser> Ꮡp, ast.OperatorPrecedence precedence) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var leftOperand = Ꮡp.parseUnaryExpressionOrHigher();
    return Ꮡp.parseBinaryExpressionRest(precedence, leftOperand, pos);
}

} partial class parser_package {

internal static bool shouldConsumeBinaryOperator(ast.Kind @operator, ast.OperatorPrecedence operatorPrecedence, ast.OperatorPrecedence currentPrecedence) {
    if (operatorPrecedence > currentPrecedence) {
        return true;
    }
    return operatorPrecedence == currentPrecedence && @operator == ast.KindAsteriskAsteriskToken;
}

} partial class parserᴛParser {

internal static ж<astꓸExpression> parseBinaryExpressionRest(this ж<Parser> Ꮡp, ast.OperatorPrecedence precedence, ж<astꓸExpression> ᏑleftOperand, nint pos) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var leftOperand = ref ᏑleftOperand.DerefOrNull();

    var lastOperand = ᏑleftOperand;
    for (;;) {
        var @operator = p.reScanGreaterThanToken();
        ast.OperatorPrecedence newPrecedence = ast.GetBinaryOperatorPrecedence(@operator);
        if (!shouldConsumeBinaryOperator(@operator, newPrecedence, precedence)) {
            break;
        }
        if (@operator == ast.KindInKeyword && p.inDisallowInContext()) {
            break;
        }
        if (@operator == ast.KindAsKeyword || @operator == ast.KindSatisfiesKeyword) {
            if (p.hasPrecedingLineBreak()) {
                break;
            } else {
                p.nextToken();
                ast.OperatorPrecedence lastPrecedence = ast.OperatorPrecedenceHighest;
                if (ast.IsBinaryExpression(lastOperand)) {
                    lastPrecedence = ast.GetBinaryOperatorPrecedence(lastOperand.AsBinaryExpression().OperatorToken.Kind);
                }
                if (@operator == ast.KindSatisfiesKeyword) {
                    ᏑleftOperand = p.makeSatisfiesExpression(ᏑleftOperand, Ꮡp.parseType()); leftOperand = ref ᏑleftOperand.DerefOrNull();
                } else {
                    ᏑleftOperand = p.makeAsExpression(ᏑleftOperand, Ꮡp.parseType()); leftOperand = ref ᏑleftOperand.DerefOrNull();
                }
                var nextOperator = p.reScanGreaterThanToken();
                ast.OperatorPrecedence nextPrecedence = ast.GetBinaryOperatorPrecedence(nextOperator);
                if (shouldConsumeBinaryOperator(nextOperator, nextPrecedence, lastPrecedence)) {
                    break;
                }
            }
        } else {
            ᏑleftOperand = p.makeBinaryExpression(ᏑleftOperand, p.parseTokenNode(), Ꮡp.parseBinaryExpressionOrHigher(newPrecedence), pos); leftOperand = ref ᏑleftOperand.DerefOrNull();
            lastOperand = ᏑleftOperand;
        }
    }
    return ᏑleftOperand;
}

internal static ж<ast.Node> makeSatisfiesExpression(this ref Parser p, ж<astꓸExpression> Ꮡexpression, ж<astꓸTypeNode> typeNode) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    return p.checkJSSyntax(p.finishNode(p.factory.NewSatisfiesExpression(Ꮡexpression, typeNode), expression.Pos()));
}

internal static ж<ast.Node> makeAsExpression(this ref Parser p, ж<astꓸExpression> Ꮡleft, ж<astꓸTypeNode> right) {
    ref var left = ref Ꮡleft.DerefOrNull();

    return p.checkJSSyntax(p.finishNode(p.factory.NewAsExpression(Ꮡleft, right), left.Pos()));
}

internal static ж<ast.Node> makeBinaryExpression(this ref Parser p, ж<astꓸExpression> left, ж<ast.Node> operatorToken, ж<astꓸExpression> right, nint pos) {
    return p.finishNode(p.factory.NewBinaryExpression(nil, left, nil, operatorToken, right), pos);
}

internal static ж<astꓸExpression> parseUnaryExpressionOrHigher(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.isUpdateExpression()) {
        nint pos = p.nodePos();
        var updateExpression = Ꮡp.parseUpdateExpression();
        if (p.token == ast.KindAsteriskAsteriskToken) {
            return Ꮡp.parseBinaryExpressionRest(ast.GetBinaryOperatorPrecedence(p.token), updateExpression, pos);
        }
        return updateExpression;
    }
    var unaryOperator = p.token;
    var simpleUnaryExpression = Ꮡp.parseSimpleUnaryExpression();
    if (p.token == ast.KindAsteriskAsteriskToken) {
        nint pos = scanner.SkipTrivia(p.sourceText, simpleUnaryExpression.Pos());
        nint end = simpleUnaryExpression.End();
        if (simpleUnaryExpression.Kind == ast.KindTypeAssertionExpression) {
            p.parseErrorAt(pos, end, diagnostics.A_type_assertion_expression_is_not_allowed_in_the_left_hand_side_of_an_exponentiation_expression_Consider_enclosing_the_expression_in_parentheses);
        } else {
            debug.Assert(isKeywordOrPunctuation(unaryOperator));
            p.parseErrorAt(pos, end, diagnostics.An_unary_expression_with_the_0_operator_is_not_allowed_in_the_left_hand_side_of_an_exponentiation_expression_Consider_enclosing_the_expression_in_parentheses, scanner.TokenToString(unaryOperator));
        }
    }
    return simpleUnaryExpression;
}

internal static bool isUpdateExpression(this ref Parser p) {
    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindPlusToken || ᴛ == ast.KindMinusToken || ᴛ == ast.KindTildeToken || ᴛ == ast.KindExclamationToken || ᴛ == ast.KindDeleteKeyword || ᴛ == ast.KindTypeOfKeyword || ᴛ == ast.KindVoidKeyword || ᴛ == ast.KindAwaitKeyword: {
        return false;
    }
    case var ᴛ when ᴛ == ast.KindLessThanToken: {
        return p.languageVariant == core.LanguageVariantJSX;
    }}

    return true;
}

internal static ж<astꓸExpression> parseUpdateExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    if (p.token == ast.KindPlusPlusToken || p.token == ast.KindMinusMinusToken) {
        var @operator = p.token;
        p.nextToken();
        return p.finishNode(p.factory.NewPrefixUnaryExpression(@operator, Ꮡp.parseLeftHandSideExpressionOrHigher()), pos);
    } else if (p.languageVariant == core.LanguageVariantJSX && p.token == ast.KindLessThanToken && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeywordOrGreaterThan)))) {
        return Ꮡp.parseJsxElementOrSelfClosingElementOrFragment(true, -1, nil, false);
    }
    var expression = Ꮡp.parseLeftHandSideExpressionOrHigher();
    if ((p.token == ast.KindPlusPlusToken || p.token == ast.KindMinusMinusToken) && !p.hasPrecedingLineBreak()) {
        var @operator = p.token;
        p.nextToken();
        return p.finishNode(p.factory.NewPostfixUnaryExpression(expression, @operator), pos);
    }
    return expression;
}

internal static ж<astꓸExpression> parseJsxElementOrSelfClosingElementOrFragment(this ж<Parser> Ꮡp, bool inExpressionContext, nint topInvalidNodePosition, ж<ast.Node> ᏑopeningTag, bool mustBeUnary) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var openingTag = ref ᏑopeningTag.DerefOrNull();

    nint pos = p.nodePos();
    var opening = Ꮡp.parseJsxOpeningOrSelfClosingElementOrOpeningFragment(inExpressionContext);
    ж<astꓸExpression> result = default;
    switch (opening.Kind) {
    case var ᴛ when ᴛ == ast.KindJsxOpeningElement: {
        var children = Ꮡp.parseJsxChildren(opening);
        ж<ast.Node> closingElement = default;
        var lastChild = core.LastOrNil(children.Nodes);
        if (lastChild != nil && lastChild.Kind == ast.KindJsxElement && !ast.TagNamesAreEquivalent(lastChild.AsJsxElement().OpeningElement.TagName(), lastChild.AsJsxElement().ClosingElement.TagName()) && ast.TagNamesAreEquivalent(opening.TagName(), lastChild.AsJsxElement().ClosingElement.TagName())) {
            nint end = lastChild.Children().End();
            var missingIdentifier = p.finishNodeWithEnd(p.newIdentifier(""), end, end);
            var newClosingElement = p.finishNodeWithEnd(p.factory.NewJsxClosingElement(missingIdentifier), end, end);
            var newLast = p.finishNodeWithEnd(
                p.factory.NewJsxElement(lastChild.AsJsxElement().OpeningElement, lastChild.Children(), newClosingElement),
                lastChild.AsJsxElement().OpeningElement.Pos(),
                end);
            if (lastChild.AsJsxElement().OpeningElement != nil) {
                lastChild.AsJsxElement().OpeningElement.Parent = newLast;
            }
            if (lastChild.Children() != nil) {
                foreach (var (_, c) in range(lastChild.Children().Nodes)) {
                    c.Parent = newLast;
                }
            }
            newClosingElement.Parent = newLast;
            children = p.newNodeList(core.NewTextRange(children.Pos(), newLast.End()), append(children.Nodes.slice(0, len(children.Nodes) - 1), newLast));
            closingElement = lastChild.AsJsxElement().ClosingElement;
        } else {
            closingElement = Ꮡp.parseJsxClosingElement(opening, inExpressionContext);
            if (!ast.TagNamesAreEquivalent(opening.TagName(), closingElement.TagName())) {
                if (ᏑopeningTag != nil && ast.IsJsxOpeningElement(ᏑopeningTag) && ast.TagNamesAreEquivalent(closingElement.TagName(), openingTag.TagName())) {
                    p.parseErrorAtRange(opening.TagName().Loc, diagnostics.JSX_element_0_has_no_corresponding_closing_tag, scanner.GetTextOfNodeFromSourceText(p.sourceText, opening.TagName(), false));
                } else {
                    p.parseErrorAtRange(closingElement.TagName().Loc, diagnostics.Expected_corresponding_JSX_closing_tag_for_0, scanner.GetTextOfNodeFromSourceText(p.sourceText, opening.TagName(), false));
                }
            }
        }
        result = p.finishNode(p.factory.NewJsxElement(opening, children, closingElement), pos);
        closingElement.Parent = result;
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxOpeningFragment: {
        result = p.finishNode(p.factory.NewJsxFragment(opening, Ꮡp.parseJsxChildren(opening), p.parseJsxClosingFragment(inExpressionContext)), pos);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxSelfClosingElement: {
        result = opening;
        break;
    }
    default: {
        throw panic("Unhandled case in parseJsxElementOrSelfClosingElementOrFragment");
        break;
    }}

    if (!mustBeUnary && inExpressionContext && p.token == ast.KindLessThanToken) {
        nint topBadPos = topInvalidNodePosition;
        if (topBadPos < 0) {
            topBadPos = result.Pos();
        }
        var invalidElement = Ꮡp.parseJsxElementOrSelfClosingElementOrFragment(true, topBadPos, nil, false);
        var operatorToken = p.factory.NewToken(ast.KindCommaToken);
        operatorToken.Loc = core.NewTextRange(invalidElement.Pos(), invalidElement.Pos());
        p.parseErrorAt(scanner.SkipTrivia(p.sourceText, topBadPos), invalidElement.End(), diagnostics.JSX_expressions_must_have_one_parent_element);
        result = p.finishNode(p.factory.NewBinaryExpression(nil, result, nil, operatorToken, invalidElement), pos);
    }
    return result;
}

internal static ж<ast.NodeList> parseJsxChildren(this ж<Parser> Ꮡp, ж<astꓸExpression> ᏑopeningTag) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var openingTag = ref ᏑopeningTag.DerefOrNull();

    nint pos = p.nodePos();
    ParsingContexts saveParsingContexts = p.parsingContexts;
    p.parsingContexts |= (ParsingContexts)((ParsingContexts)(nint)(1 << (int)(PCJsxChildren)));
    slice<ж<ast.Node>> list = default;
    for (;;) {
        var currentToken = p.scanner.ReScanJsxToken(true);
        var child = Ꮡp.parseJsxChild(ᏑopeningTag, currentToken);
        if (child == nil) {
            break;
        }
        list = append(list, child);
        if (ast.IsJsxOpeningElement(ᏑopeningTag) && child.Kind == ast.KindJsxElement && !ast.TagNamesAreEquivalent(child.AsJsxElement().OpeningElement.TagName(), child.AsJsxElement().ClosingElement.TagName()) && ast.TagNamesAreEquivalent(openingTag.TagName(), child.AsJsxElement().ClosingElement.TagName())) {
            break;
        }
    }
    p.parsingContexts = saveParsingContexts;
    return p.newNodeList(core.NewTextRange(pos, p.nodePos()), list);
}

internal static ж<astꓸExpression> parseJsxChild(this ж<Parser> Ꮡp, ж<ast.Node> ᏑopeningTag, ast.Kind token) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var openingTag = ref ᏑopeningTag.DerefOrNull();

    switch (token) {
    case var ᴛ when ᴛ == ast.KindEndOfFile: {
        if (ast.IsJsxOpeningFragment(ᏑopeningTag)) {
            p.parseErrorAtRange(openingTag.Loc, diagnostics.JSX_fragment_has_no_corresponding_closing_tag);
        } else {
            var tag = openingTag.TagName();
            nint start = min(scanner.SkipTrivia(p.sourceText, tag.Pos()), tag.End());
            p.parseErrorAt(start, tag.End(), diagnostics.JSX_element_0_has_no_corresponding_closing_tag,
                scanner.GetTextOfNodeFromSourceText(p.sourceText, openingTag.TagName(), false));
        }
        return default;
    }
    case var ᴛ when ᴛ == ast.KindLessThanSlashToken || ᴛ == ast.KindConflictMarkerTrivia: {
        return default;
    }
    case var ᴛ when ᴛ == ast.KindJsxText || ᴛ == ast.KindJsxTextAllWhiteSpaces: {
        return p.parseJsxText();
    }
    case var ᴛ when ᴛ == ast.KindOpenBraceToken: {
        return Ꮡp.parseJsxExpression(false);
    }
    case var ᴛ when ᴛ == ast.KindLessThanToken: {
        return Ꮡp.parseJsxElementOrSelfClosingElementOrFragment(false, -1, ᏑopeningTag, false);
    }}

    throw panic("Unhandled case in parseJsxChild");
}

internal static ж<ast.Node> parseJsxText(this ref Parser p) {
    nint pos = p.nodePos();
    var result = p.factory.NewJsxText(p.scanner.TokenValue(), p.token == ast.KindJsxTextAllWhiteSpaces);
    p.scanJsxText();
    return p.finishNode(result, pos);
}

internal static ж<ast.Node> parseJsxExpression(this ж<Parser> Ꮡp, bool inExpressionContext) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    if (!p.parseExpected(ast.KindOpenBraceToken)) {
        return default;
    }
    ж<ast.Node> dotDotDotToken = default;
    ж<astꓸExpression> expression = default;
    if (p.token != ast.KindCloseBraceToken) {
        if (!inExpressionContext) {
            dotDotDotToken = p.parseOptionalToken(ast.KindDotDotDotToken);
        }
        expression = Ꮡp.parseExpression();
    }
    if (inExpressionContext) {
        p.parseExpected(ast.KindCloseBraceToken);
    } else if (p.parseExpectedWithoutAdvancing(ast.KindCloseBraceToken)) {
        p.scanJsxText();
    }
    return p.finishNode(p.factory.NewJsxExpression(dotDotDotToken, expression), pos);
}

internal static ast.Kind scanJsxText(this ref Parser p) {
    p.token = p.scanner.ScanJsxToken();
    return p.token;
}

internal static ast.Kind scanJsxIdentifier(this ref Parser p) {
    p.token = p.scanner.ScanJsxIdentifier();
    return p.token;
}

internal static ast.Kind scanJsxAttributeValue(this ref Parser p) {
    p.token = p.scanner.ScanJsxAttributeValue();
    return p.token;
}

internal static ж<ast.Node> parseJsxClosingElement(this ж<Parser> Ꮡp, ж<ast.Node> Ꮡopen, bool inExpressionContext) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var open = ref Ꮡopen.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindLessThanSlashToken);
    var tagName = Ꮡp.parseJsxElementName();
    if (p.parseExpectedWithDiagnostic(ast.KindGreaterThanToken, nil, false)) {
        if (inExpressionContext || !ast.TagNamesAreEquivalent(open.TagName(), tagName)) {
            p.nextToken();
        } else {
            p.scanJsxText();
        }
    }
    return p.finishNode(p.factory.NewJsxClosingElement(tagName), pos);
}

internal static ж<astꓸExpression> parseJsxOpeningOrSelfClosingElementOrOpeningFragment(this ж<Parser> Ꮡp, bool inExpressionContext) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindLessThanToken);
    if (p.token == ast.KindGreaterThanToken) {
        p.scanJsxText();
        return p.finishNode(p.factory.NewJsxOpeningFragment(), pos);
    }
    var tagName = Ꮡp.parseJsxElementName();
    ж<ast.NodeList> typeArguments = default;
    if ((ast.NodeFlags)(p.contextFlags & ast.NodeFlagsJavaScriptFile) == 0) {
        typeArguments = Ꮡp.parseTypeArguments();
    }
    var attributes = Ꮡp.parseJsxAttributes();
    ж<astꓸExpression> result = default;
    if (p.token == ast.KindGreaterThanToken) {
        p.scanJsxText();
        result = p.factory.NewJsxOpeningElement(tagName, typeArguments, attributes);
    } else {
        p.parseExpected(ast.KindSlashToken);
        if (p.parseExpectedWithoutAdvancing(ast.KindGreaterThanToken)) {
            if (inExpressionContext) {
                p.nextToken();
            } else {
                p.scanJsxText();
            }
        }
        result = p.factory.NewJsxSelfClosingElement(tagName, typeArguments, attributes);
    }
    return p.finishNode(result, pos);
}

internal static ж<astꓸExpression> parseJsxElementName(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var initialExpression = p.parseJsxTagName();
    if (ast.IsJsxNamespacedName(initialExpression)) {
        return initialExpression;
    }
    var expression = initialExpression;
    while (p.parseOptional(ast.KindDotToken)) {
        expression = p.finishNode(p.factory.NewPropertyAccessExpression(expression, nil, Ꮡp.parseRightSideOfDot(true, false, false), ast.NodeFlagsNone), pos);
    }
    return expression;
}

internal static ж<astꓸExpression> parseJsxTagName(this ref Parser p) {
    nint pos = p.nodePos();
    p.scanJsxIdentifier();
    var isThis = p.token == ast.KindThisKeyword;
    var tagName = p.parseIdentifierNameErrorOnUnicodeEscapeSequence();
    if (p.parseOptional(ast.KindColonToken)) {
        p.scanJsxIdentifier();
        return p.finishNode(p.factory.NewJsxNamespacedName(tagName, p.parseIdentifierNameErrorOnUnicodeEscapeSequence()), pos);
    }
    if (isThis) {
        var result = p.factory.NewKeywordExpression(ast.KindThisKeyword);
        return p.finishNode(result, pos);
    }
    return tagName;
}

internal static ж<ast.Node> parseJsxAttributes(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    return p.finishNode(p.factory.NewJsxAttributes(Ꮡp.parseList(PCJsxAttributes, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseJsxAttribute)))), pos);
}

internal static ж<ast.Node> parseJsxAttribute(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindOpenBraceToken) {
        return Ꮡp.parseJsxSpreadAttribute();
    }
    nint pos = p.nodePos();
    return p.finishNode(p.factory.NewJsxAttribute(p.parseJsxAttributeName(), Ꮡp.parseJsxAttributeValue()), pos);
}

internal static ж<ast.Node> parseJsxSpreadAttribute(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindOpenBraceToken);
    p.parseExpected(ast.KindDotDotDotToken);
    var expression = Ꮡp.parseExpression();
    p.parseExpected(ast.KindCloseBraceToken);
    return p.finishNode(p.factory.NewJsxSpreadAttribute(expression), pos);
}

internal static ж<ast.Node> parseJsxAttributeName(this ref Parser p) {
    nint pos = p.nodePos();
    p.scanJsxIdentifier();
    var attrName = p.parseIdentifierNameErrorOnUnicodeEscapeSequence();
    if (p.parseOptional(ast.KindColonToken)) {
        p.scanJsxIdentifier();
        return p.finishNode(p.factory.NewJsxNamespacedName(attrName, p.parseIdentifierNameErrorOnUnicodeEscapeSequence()), pos);
    }
    return attrName;
}

internal static ж<astꓸExpression> parseJsxAttributeValue(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindEqualsToken) {
        if (p.scanJsxAttributeValue() == ast.KindStringLiteral) {
            return p.parseLiteralExpression();
        }
        if (p.token == ast.KindOpenBraceToken) {
            return Ꮡp.parseJsxExpression(true);
        }
        if (p.token == ast.KindLessThanToken) {
            return Ꮡp.parseJsxElementOrSelfClosingElementOrFragment(true, -1, nil, false);
        }
        p.parseErrorAtCurrentToken(diagnostics.X_or_JSX_element_expected);
    }
    return default;
}

internal static ж<ast.Node> parseJsxClosingFragment(this ref Parser p, bool inExpressionContext) {
    nint pos = p.nodePos();
    p.parseExpected(ast.KindLessThanSlashToken);
    if (p.parseExpectedWithDiagnostic(ast.KindGreaterThanToken, diagnostics.Expected_corresponding_closing_tag_for_JSX_fragment, false)) {
        if (inExpressionContext) {
            p.nextToken();
        } else {
            p.scanJsxText();
        }
    }
    return p.finishNode(p.factory.NewJsxClosingFragment(), pos);
}

internal static ж<astꓸExpression> parseSimpleUnaryExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var exprᴛ1 = p.token;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindPlusToken || exprᴛ1 == ast.KindMinusToken || exprᴛ1 == ast.KindTildeToken || exprᴛ1 == ast.KindExclamationToken) { matchᴛ1 = true;
        return Ꮡp.parsePrefixUnaryExpression();
    }
    if (exprᴛ1 == ast.KindDeleteKeyword) { matchᴛ1 = true;
        return Ꮡp.parseDeleteExpression();
    }
    if (exprᴛ1 == ast.KindTypeOfKeyword) { matchᴛ1 = true;
        return Ꮡp.parseTypeOfExpression();
    }
    if (exprᴛ1 == ast.KindVoidKeyword) { matchᴛ1 = true;
        return Ꮡp.parseVoidExpression();
    }
    if (exprᴛ1 == ast.KindLessThanToken) { matchᴛ1 = true;
        if (p.languageVariant == core.LanguageVariantJSX) {
            return Ꮡp.parseJsxElementOrSelfClosingElementOrFragment(true, -1, nil, true);
        }
        return Ꮡp.parseTypeAssertion();
    }
    if (exprᴛ1 == ast.KindAwaitKeyword) { matchᴛ1 = true;
        if (Ꮡp.isAwaitExpression()) {
            return Ꮡp.parseAwaitExpression();
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1) { /* default: */
        return Ꮡp.parseUpdateExpression();
    }
    return default;

}

internal static ж<ast.Node> parsePrefixUnaryExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var @operator = p.token;
    p.nextToken();
    return p.finishNode(p.factory.NewPrefixUnaryExpression(@operator, Ꮡp.parseSimpleUnaryExpression()), pos);
}

internal static ж<ast.Node> parseDeleteExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewDeleteExpression(Ꮡp.parseSimpleUnaryExpression()), pos);
}

internal static ж<ast.Node> parseTypeOfExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewTypeOfExpression(Ꮡp.parseSimpleUnaryExpression()), pos);
}

internal static ж<ast.Node> parseVoidExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewVoidExpression(Ꮡp.parseSimpleUnaryExpression()), pos);
}

internal static bool isAwaitExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.token == ast.KindAwaitKeyword) {
        if (p.inAwaitContext()) {
            return true;
        }
        return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeywordOrLiteralOnSameLine)));
    }
    return false;
}

internal static ж<ast.Node> parseAwaitExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.nextToken();
    return p.finishNode(p.factory.NewAwaitExpression(Ꮡp.parseSimpleUnaryExpression()), pos);
}

internal static ж<ast.Node> parseTypeAssertion(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    debug.Assert(p.languageVariant != core.LanguageVariantJSX, "Type assertions should never be parsed in JSX; they should be parsed as comparisons or JSX elements/fragments.");
    nint pos = p.nodePos();
    p.parseExpected(ast.KindLessThanToken);
    var typeNode = Ꮡp.parseType();
    p.parseExpected(ast.KindGreaterThanToken);
    var expression = Ꮡp.parseSimpleUnaryExpression();
    return p.finishNode(p.factory.NewTypeAssertion(typeNode, expression), pos);
}

internal static ж<astꓸExpression> parseLeftHandSideExpressionOrHigher(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    ж<astꓸExpression> expression = default;
    if (p.token == ast.KindImportKeyword) {
        if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOpenParenOrLessThan)))) {
            p.sourceFlags |= (ast.NodeFlags)(ast.NodeFlagsPossiblyContainsDynamicImport);
            expression = p.parseKeywordExpression();
        } else if (Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsDot)))) {
            p.nextToken();
            p.nextToken();
            expression = p.finishNode(p.factory.NewMetaProperty(ast.KindImportKeyword, p.parseIdentifierName()), pos);
            if (expression.Text() == "defer"u8) {
                if (p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken) {
                    p.sourceFlags |= (ast.NodeFlags)(ast.NodeFlagsPossiblyContainsDynamicImport);
                }
            } else {
                p.sourceFlags |= (ast.NodeFlags)(ast.NodeFlagsPossiblyContainsImportMeta);
            }
        } else {
            expression = Ꮡp.parseMemberExpressionOrHigher();
        }
    } else if (p.token == ast.KindSuperKeyword) {
        expression = Ꮡp.parseSuperExpression();
    } else {
        expression = Ꮡp.parseMemberExpressionOrHigher();
    }
    return Ꮡp.parseCallExpressionRest(pos, expression);
}

internal static bool nextTokenIsDot(this ref Parser p) {
    return p.nextToken() == ast.KindDotToken;
}

internal static ж<astꓸExpression> parseSuperExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var expression = p.parseKeywordExpression();
    if (p.token == ast.KindLessThanToken) {
        nint startPos = p.nodePos();
        var typeArguments = Ꮡp.tryParseTypeArgumentsInExpression();
        if (typeArguments != nil) {
            p.parseErrorAt(startPos, p.nodePos(), diagnostics.X_super_may_not_use_type_arguments);
            if (!p.isTemplateStartOfTaggedTemplate()) {
                expression = p.finishNode(p.factory.NewExpressionWithTypeArguments(expression, typeArguments), pos);
            }
        }
    }
    if (p.token == ast.KindOpenParenToken || p.token == ast.KindDotToken || p.token == ast.KindOpenBracketToken) {
        return expression;
    }
    p.parseErrorAtCurrentToken(diagnostics.X_super_must_be_followed_by_an_argument_list_or_member_access);
    return p.finishNode(p.factory.NewPropertyAccessExpression(expression, nil, Ꮡp.parseRightSideOfDot(true, true, true), ast.NodeFlagsNone), pos);
}

internal static bool isTemplateStartOfTaggedTemplate(this ref Parser p) {
    return p.token == ast.KindNoSubstitutionTemplateLiteral || p.token == ast.KindTemplateHead;
}

internal static ж<ast.NodeList> tryParseTypeArgumentsInExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if ((ast.NodeFlags)(p.contextFlags & ast.NodeFlagsJavaScriptFile) != 0 || (p.token != ast.KindLessThanToken && p.token != ast.KindLessThanLessThanToken)) {
        return default;
    }
    var state = p.mark();
    if (p.reScanLessThanToken() == ast.KindLessThanToken) {
        p.nextToken();
        var typeArguments = Ꮡp.parseDelimitedList(PCTypeArguments, ((Func<ж<Parser>, ж<astꓸTypeNode>>)(parserᴛParser.parseType)));
        if (p.reScanGreaterThanToken() == ast.KindGreaterThanToken) {
            p.nextToken();
            if (Ꮡp.canFollowTypeArgumentsInExpression()) {
                return typeArguments;
            }
        }
    }
    p.rewind(state);
    return default;
}

internal static bool canFollowTypeArgumentsInExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindOpenParenToken || ᴛ == ast.KindNoSubstitutionTemplateLiteral || ᴛ == ast.KindTemplateHead: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindLessThanToken || ᴛ == ast.KindGreaterThanToken || ᴛ == ast.KindPlusToken || ᴛ == ast.KindMinusToken: {
        return false;
    }}

    return p.hasPrecedingLineBreak() || p.isBinaryOperator() || !Ꮡp.isStartOfExpression();
}

internal static ж<ast.Node> parseMemberExpressionOrHigher(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var expression = Ꮡp.parsePrimaryExpression();
    return Ꮡp.parseMemberExpressionRest(pos, expression, true);
}

internal static ж<astꓸExpression> parseMemberExpressionRest(this ж<Parser> Ꮡp, nint pos, ж<astꓸExpression> Ꮡexpression, bool allowOptionalChain) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var expression = ref Ꮡexpression.DerefOrNull();

    for (;;) {
        ж<ast.Node> questionDotToken = default;
        var isPropertyAccess = false;
        if (allowOptionalChain && Ꮡp.isStartOfOptionalPropertyOrElementAccessChain()) {
            questionDotToken = p.parseExpectedToken(ast.KindQuestionDotToken);
            isPropertyAccess = tokenIsIdentifierOrKeyword(p.token);
        } else {
            isPropertyAccess = p.parseOptional(ast.KindDotToken);
        }
        if (isPropertyAccess) {
            Ꮡexpression = Ꮡp.parsePropertyAccessExpressionRest(pos, Ꮡexpression, questionDotToken); expression = ref Ꮡexpression.DerefOrNull();
            continue;
        }
        if ((questionDotToken != nil || !p.inDecoratorContext()) && p.parseOptional(ast.KindOpenBracketToken)) {
            Ꮡexpression = Ꮡp.parseElementAccessExpressionRest(pos, Ꮡexpression, questionDotToken); expression = ref Ꮡexpression.DerefOrNull();
            continue;
        }
        if (p.isTemplateStartOfTaggedTemplate()) {
            if (questionDotToken == nil && ast.IsExpressionWithTypeArguments(Ꮡexpression)) {
                var original = expression.AsExpressionWithTypeArguments();
                Ꮡexpression = Ꮡp.parseTaggedTemplateRest(pos, (~original).Expression, questionDotToken, (~original).TypeArguments); expression = ref Ꮡexpression.DerefOrNull();
                p.unparseExpressionWithTypeArguments((~original).Expression, (~original).TypeArguments, Ꮡexpression);
            } else {
                Ꮡexpression = Ꮡp.parseTaggedTemplateRest(pos, Ꮡexpression, questionDotToken, nil); expression = ref Ꮡexpression.DerefOrNull();
            }
            continue;
        }
        if (questionDotToken == nil) {
            if (p.token == ast.KindExclamationToken && !p.hasPrecedingLineBreak()) {
                p.nextToken();
                Ꮡexpression = p.checkJSSyntax(p.finishNode(p.factory.NewNonNullExpression(Ꮡexpression, ast.NodeFlagsNone), pos)); expression = ref Ꮡexpression.DerefOrNull();
                continue;
            }
            var typeArguments = Ꮡp.tryParseTypeArgumentsInExpression();
            if (typeArguments != nil) {
                Ꮡexpression = p.finishNode(p.factory.NewExpressionWithTypeArguments(Ꮡexpression, typeArguments), pos); expression = ref Ꮡexpression.DerefOrNull();
                continue;
            }
        }
        return Ꮡexpression;
    }
}

internal static bool isStartOfOptionalPropertyOrElementAccessChain(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.token == ast.KindQuestionDotToken && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeywordOrOpenBracketOrTemplate)));
}

internal static bool nextTokenIsIdentifierOrKeywordOrOpenBracketOrTemplate(this ref Parser p) {
    p.nextToken();
    return tokenIsIdentifierOrKeyword(p.token) || p.token == ast.KindOpenBracketToken || p.isTemplateStartOfTaggedTemplate();
}

internal static ж<ast.Node> parsePropertyAccessExpressionRest(this ж<Parser> Ꮡp, nint pos, ж<astꓸExpression> Ꮡexpression, ж<ast.Node> questionDotToken) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var expression = ref Ꮡexpression.DerefOrNull();

    var name = Ꮡp.parseRightSideOfDot(true, true, true);
    var isOptionalChain = questionDotToken != nil || p.tryReparseOptionalChain(Ꮡexpression);
    var propertyAccess = p.factory.NewPropertyAccessExpression(Ꮡexpression, questionDotToken, name, core.IfElse(isOptionalChain, ast.NodeFlagsOptionalChain, ast.NodeFlagsNone));
    if (isOptionalChain && ast.IsPrivateIdentifier(name)) {
        p.parseErrorAtRange(p.skipRangeTrivia(name.Loc), diagnostics.An_optional_chain_cannot_contain_private_identifiers);
    }
    if (ast.IsExpressionWithTypeArguments(Ꮡexpression)) {
        var typeArguments = expression.TypeArgumentList();
        if (typeArguments != nil) {
            var loc = core.NewTextRange(typeArguments.Pos() - 1, scanner.SkipTrivia(p.sourceText, typeArguments.End()) + 1);
            p.parseErrorAtRange(loc, diagnostics.An_instantiation_expression_cannot_be_followed_by_a_property_access);
        }
    }
    return p.finishNode(propertyAccess, pos);
}

internal static bool tryReparseOptionalChain(this ref Parser p, ж<astꓸExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0) {
        return true;
    }
    if (ast.IsNonNullExpression(Ꮡnode)) {
        var expr = node.Expression();
        while (ast.IsNonNullExpression(expr) && (ast.NodeFlags)(expr.Flags & ast.NodeFlagsOptionalChain) == 0) {
            expr = expr.Expression();
        }
        if ((ast.NodeFlags)(expr.Flags & ast.NodeFlagsOptionalChain) != 0) {
            while (ast.IsNonNullExpression(Ꮡnode)) {
                node.Flags |= (ast.NodeFlags)(ast.NodeFlagsOptionalChain);
                Ꮡnode = node.Expression(); node = ref Ꮡnode.DerefOrNull();
            }
            return true;
        }
    }
    return false;
}

internal static ж<ast.Node> parseElementAccessExpressionRest(this ж<Parser> Ꮡp, nint pos, ж<astꓸExpression> expression, ж<ast.Node> questionDotToken) {
    ref var p = ref Ꮡp.DerefOrNull();

    var argumentExpression = p.createMissingIdentifier();
    if (p.token == ast.KindCloseBracketToken) {
        p.parseErrorAt(p.nodePos(), p.nodePos(), diagnostics.An_element_access_expression_should_take_an_argument);
    } else {
        argumentExpression = Ꮡp.parseExpressionAllowIn();
    }
    p.parseExpected(ast.KindCloseBracketToken);
    var isOptionalChain = questionDotToken != nil || p.tryReparseOptionalChain(expression);
    return p.finishNode(p.factory.NewElementAccessExpression(expression, questionDotToken, argumentExpression, core.IfElse(isOptionalChain, ast.NodeFlagsOptionalChain, ast.NodeFlagsNone)), pos);
}

internal static ж<astꓸExpression> parseCallExpressionRest(this ж<Parser> Ꮡp, nint pos, ж<astꓸExpression> Ꮡexpression) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var expression = ref Ꮡexpression.DerefOrNull();

    for (;;) {
        Ꮡexpression = Ꮡp.parseMemberExpressionRest(pos, Ꮡexpression, true); expression = ref Ꮡexpression.DerefOrNull();
        ж<ast.NodeList> typeArguments = default;
        var questionDotToken = p.parseOptionalToken(ast.KindQuestionDotToken);
        if (questionDotToken != nil) {
            typeArguments = Ꮡp.tryParseTypeArgumentsInExpression();
            if (p.isTemplateStartOfTaggedTemplate()) {
                Ꮡexpression = Ꮡp.parseTaggedTemplateRest(pos, Ꮡexpression, questionDotToken, typeArguments); expression = ref Ꮡexpression.DerefOrNull();
                continue;
            }
        }
        if (typeArguments != nil || p.token == ast.KindOpenParenToken) {
            if (questionDotToken == nil && expression.Kind == ast.KindExpressionWithTypeArguments) {
                typeArguments = expression.TypeArgumentList();
                Ꮡexpression = expression.AsExpressionWithTypeArguments().Value.Expression; expression = ref Ꮡexpression.DerefOrNull();
            }
            var inner = Ꮡexpression;
            var argumentList = Ꮡp.parseArgumentList();
            var isOptionalChain = questionDotToken != nil || p.tryReparseOptionalChain(Ꮡexpression);
            Ꮡexpression = p.checkJSSyntax(p.finishNode(p.factory.NewCallExpression(Ꮡexpression, questionDotToken, typeArguments, argumentList, core.IfElse(isOptionalChain, ast.NodeFlagsOptionalChain, ast.NodeFlagsNone)), pos)); expression = ref Ꮡexpression.DerefOrNull();
            p.unparseExpressionWithTypeArguments(inner, typeArguments, Ꮡexpression);
            continue;
        }
        if (questionDotToken != nil) {
            p.parseErrorAtCurrentToken(diagnostics.Identifier_expected);
            var name = p.createMissingIdentifier();
            Ꮡexpression = p.finishNode(p.factory.NewPropertyAccessExpression(Ꮡexpression, questionDotToken, name, ast.NodeFlagsOptionalChain), pos); expression = ref Ꮡexpression.DerefOrNull();
        }
        break;
    }
    return Ꮡexpression;
}

internal static ж<ast.NodeList> parseArgumentList(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.parseExpected(ast.KindOpenParenToken);
    var result = Ꮡp.parseDelimitedList(PCArgumentExpressions, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseArgumentExpression)));
    p.parseExpected(ast.KindCloseParenToken);
    return result;
}

internal static ж<astꓸExpression> parseArgumentExpression(this ж<Parser> Ꮡp) {
    return doInContext<ж<astꓸExpression>>(Ꮡp, (ast.NodeFlags)(ast.NodeFlagsDisallowInContext | ast.NodeFlagsDecoratorContext), false, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseArgumentOrArrayLiteralElement)));
}

internal static ж<astꓸExpression> parseArgumentOrArrayLiteralElement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindDotDotDotToken: {
        return Ꮡp.parseSpreadElement();
    }
    case var ᴛ when ᴛ == ast.KindCommaToken: {
        return p.finishNode(p.factory.NewOmittedExpression(), p.nodePos());
    }}

    return Ꮡp.parseAssignmentExpressionOrHigher();
}

internal static ж<ast.Node> parseSpreadElement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindDotDotDotToken);
    var expression = Ꮡp.parseAssignmentExpressionOrHigher();
    return p.finishNode(p.factory.NewSpreadElement(expression), pos);
}

internal static ж<ast.Node> parseTaggedTemplateRest(this ж<Parser> Ꮡp, nint pos, ж<astꓸExpression> Ꮡtag, ж<ast.Node> questionDotToken, ж<ast.NodeList> typeArguments) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var tag = ref Ꮡtag.DerefOrNull();

    ж<astꓸExpression> template = default;
    if (p.token == ast.KindNoSubstitutionTemplateLiteral) {
        p.reScanTemplateToken(true);
        template = p.parseLiteralExpression();
    } else {
        template = Ꮡp.parseTemplateExpression(true);
    }
    var isOptionalChain = questionDotToken != nil || (ast.NodeFlags)(tag.Flags & ast.NodeFlagsOptionalChain) != 0;
    return p.checkJSSyntax(p.finishNode(p.factory.NewTaggedTemplateExpression(Ꮡtag, questionDotToken, typeArguments, template, core.IfElse(isOptionalChain, ast.NodeFlagsOptionalChain, ast.NodeFlagsNone)), pos));
}

internal static ж<astꓸExpression> parseTemplateExpression(this ж<Parser> Ꮡp, bool isTaggedTemplate) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    return p.finishNode(p.factory.NewTemplateExpression(p.parseTemplateHead(isTaggedTemplate), Ꮡp.parseTemplateSpans(isTaggedTemplate)), pos);
}

internal static ж<ast.NodeList> parseTemplateSpans(this ж<Parser> Ꮡp, bool isTaggedTemplate) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    slice<ж<ast.Node>> list = default;
    for (;;) {
        var span = Ꮡp.parseTemplateSpan(isTaggedTemplate);
        list = append(list, span);
        if (span.AsTemplateSpan().Literal.Kind != ast.KindTemplateMiddle) {
            break;
        }
    }
    return p.newNodeList(core.NewTextRange(pos, p.nodePos()), list);
}

internal static ж<ast.Node> parseTemplateSpan(this ж<Parser> Ꮡp, bool isTaggedTemplate) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var expression = Ꮡp.parseExpressionAllowIn();
    var literal = p.parseLiteralOfTemplateSpan(isTaggedTemplate);
    return p.finishNode(p.factory.NewTemplateSpan(expression, literal), pos);
}

internal static ж<astꓸExpression> parsePrimaryExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var exprᴛ1 = p.token;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindNoSubstitutionTemplateLiteral) { matchᴛ1 = true;
        if ((ast.TokenFlags)(p.scanner.TokenFlags() & ast.TokenFlagsIsInvalid) != 0) {
            p.reScanTemplateToken(false);
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindNumericLiteral || exprᴛ1 == ast.KindBigIntLiteral || exprᴛ1 == ast.KindStringLiteral)) { matchᴛ1 = true;
        return p.parseLiteralExpression();
    }
    if (exprᴛ1 == ast.KindThisKeyword || exprᴛ1 == ast.KindSuperKeyword || exprᴛ1 == ast.KindNullKeyword || exprᴛ1 == ast.KindTrueKeyword || exprᴛ1 == ast.KindFalseKeyword) { matchᴛ1 = true;
        return p.parseKeywordExpression();
    }
    if (exprᴛ1 == ast.KindOpenParenToken) { matchᴛ1 = true;
        return Ꮡp.parseParenthesizedExpression();
    }
    if (exprᴛ1 == ast.KindOpenBracketToken) { matchᴛ1 = true;
        return Ꮡp.parseArrayLiteralExpression();
    }
    if (exprᴛ1 == ast.KindOpenBraceToken) { matchᴛ1 = true;
        return Ꮡp.parseObjectLiteralExpression();
    }
    if (exprᴛ1 == ast.KindAsyncKeyword) { matchᴛ1 = true;
        do {
            if (!Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsFunctionKeywordOnSameLine)))) {
                break;
            }
            return Ꮡp.parseFunctionExpression();
        } while (false);
    }
    else if (exprᴛ1 == ast.KindAtToken) { matchᴛ1 = true;
        return Ꮡp.parseDecoratedExpression();
    }
    else if (exprᴛ1 == ast.KindClassKeyword) { matchᴛ1 = true;
        return Ꮡp.parseClassExpression();
    }
    else if (exprᴛ1 == ast.KindFunctionKeyword) { matchᴛ1 = true;
        return Ꮡp.parseFunctionExpression();
    }
    else if (exprᴛ1 == ast.KindNewKeyword) { matchᴛ1 = true;
        return Ꮡp.parseNewExpressionOrNewDotTarget();
    }
    else if (exprᴛ1 == ast.KindSlashToken || exprᴛ1 == ast.KindSlashEqualsToken) { matchᴛ1 = true;
        if (p.reScanSlashToken() == ast.KindRegularExpressionLiteral) {
            return p.parseLiteralExpression();
        }
    }
    else if (exprᴛ1 == ast.KindTemplateHead) {
        return Ꮡp.parseTemplateExpression(false);
    }
    else if (exprᴛ1 == ast.KindPrivateIdentifier) { matchᴛ1 = true;
        return p.parsePrivateIdentifier();
    }

    return p.parseIdentifierWithDiagnostic(diagnostics.Expression_expected, nil);
}

internal static ж<astꓸExpression> parseParenthesizedExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    p.parseExpected(ast.KindOpenParenToken);
    var expression = Ꮡp.parseExpressionAllowIn();
    p.parseExpected(ast.KindCloseParenToken);
    var result = p.finishNode(p.factory.NewParenthesizedExpression(expression), pos);
    Ꮡp.withJSDoc(result, jsdoc);
    return result;
}

internal static ж<astꓸExpression> parseArrayLiteralExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    nint openBracketPosition = p.scanner.TokenStart();
    var openBracketParsed = p.parseExpected(ast.KindOpenBracketToken);
    var multiLine = p.hasPrecedingLineBreak();
    var elements = Ꮡp.parseDelimitedList(PCArrayLiteralMembers, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseArgumentOrArrayLiteralElement)));
    p.parseExpectedMatchingBrackets(ast.KindOpenBracketToken, ast.KindCloseBracketToken, openBracketParsed, openBracketPosition);
    return p.finishNode(p.factory.NewArrayLiteralExpression(elements, multiLine), pos);
}

internal static ж<astꓸExpression> parseObjectLiteralExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    nint openBracePosition = p.scanner.TokenStart();
    var openBraceParsed = p.parseExpected(ast.KindOpenBraceToken);
    var multiLine = p.hasPrecedingLineBreak();
    var properties = Ꮡp.parseDelimitedList(PCObjectLiteralMembers, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseObjectLiteralElement)));
    p.parseExpectedMatchingBrackets(ast.KindOpenBraceToken, ast.KindCloseBraceToken, openBraceParsed, openBracePosition);
    return p.finishNode(p.factory.NewObjectLiteralExpression(properties, multiLine), pos);
}

internal static ж<ast.Node> parseObjectLiteralElement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    if (p.parseOptional(ast.KindDotDotDotToken)) {
        var expression = Ꮡp.parseAssignmentExpressionOrHigher();
        var result = p.finishNode(p.factory.NewSpreadAssignment(expression), pos);
        Ꮡp.withJSDoc(result, jsdoc);
        return result;
    }
    var modifiers = Ꮡp.parseModifiersEx(true, false, false);
    if (Ꮡp.parseContextualModifier(ast.KindGetKeyword)) {
        return Ꮡp.parseAccessorDeclaration(pos, jsdoc, modifiers, ast.KindGetAccessor, ParseFlagsNone);
    }
    if (Ꮡp.parseContextualModifier(ast.KindSetKeyword)) {
        return Ꮡp.parseAccessorDeclaration(pos, jsdoc, modifiers, ast.KindSetAccessor, ParseFlagsNone);
    }
    var asteriskToken = p.parseOptionalToken(ast.KindAsteriskToken);
    var tokenIsIdentifier = p.isIdentifier();
    var name = Ꮡp.parsePropertyName();
    var postfixToken = p.parseOptionalToken(ast.KindQuestionToken);
    if (postfixToken == nil) {
        postfixToken = p.parseOptionalToken(ast.KindExclamationToken);
    }
    if (asteriskToken != nil || p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken) {
        return Ꮡp.parseMethodDeclaration(pos, jsdoc, modifiers, asteriskToken, name, postfixToken, nil);
    }
    ж<ast.Node> node = default;
    var isShorthandPropertyAssignment = tokenIsIdentifier && p.token != ast.KindColonToken;
    if (isShorthandPropertyAssignment) {
        var equalsToken = p.parseOptionalToken(ast.KindEqualsToken);
        ж<astꓸExpression> initializer = default;
        if (equalsToken != nil) {
            initializer = doInContext<ж<astꓸExpression>>(Ꮡp, ast.NodeFlagsDisallowInContext, false, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseAssignmentExpressionOrHigher)));
        }
        node = p.factory.NewShorthandPropertyAssignment(modifiers, name, postfixToken, nil, equalsToken, initializer);
    } else {
        p.parseExpected(ast.KindColonToken);
        var initializer = doInContext<ж<astꓸExpression>>(Ꮡp, ast.NodeFlagsDisallowInContext, false, ((Func<ж<Parser>, ж<astꓸExpression>>)(parserᴛParser.parseAssignmentExpressionOrHigher)));
        node = p.factory.NewPropertyAssignment(modifiers, name, postfixToken, nil, initializer);
    }
    p.finishNode(node, pos);
    Ꮡp.withJSDoc(node, jsdoc);
    return node;
}

internal static ж<astꓸExpression> parseFunctionExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContexFlags = p.contextFlags;
    p.setContextFlags(ast.NodeFlagsDecoratorContext, false);
    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var modifiers = Ꮡp.parseModifiers();
    p.parseExpected(ast.KindFunctionKeyword);
    var asteriskToken = p.parseOptionalToken(ast.KindAsteriskToken);
    var isGenerator = asteriskToken != nil;
    var isAsync = modifierListHasAsync(modifiers);
    var signatureFlags = (ParseFlags)(core.IfElse(isGenerator, ParseFlagsYield, ParseFlagsNone) | core.IfElse(isAsync, ParseFlagsAwait, ParseFlagsNone));
    ж<ast.Node> name = default;
    switch (ᐧ) {
    case {} when isGenerator && isAsync: {
        name = doInContext<ж<ast.Node>>(Ꮡp, (ast.NodeFlags)(ast.NodeFlagsYieldContext | ast.NodeFlagsAwaitContext), true, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseOptionalBindingIdentifier)));
        break;
    }
    case {} when isGenerator: {
        name = doInContext<ж<ast.Node>>(Ꮡp, ast.NodeFlagsYieldContext, true, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseOptionalBindingIdentifier)));
        break;
    }
    case {} when isAsync: {
        name = doInContext<ж<ast.Node>>(Ꮡp, ast.NodeFlagsAwaitContext, true, ((Func<ж<Parser>, ж<ast.Node>>)(parserᴛParser.parseOptionalBindingIdentifier)));
        break;
    }
    default: {
        name = p.parseOptionalBindingIdentifier();
        break;
    }}

    var typeParameters = Ꮡp.parseTypeParameters();
    var parameters = Ꮡp.parseParameters(signatureFlags);
    var returnType = Ꮡp.parseReturnType(ast.KindColonToken, false);
    var body = Ꮡp.parseFunctionBlock(signatureFlags, nil);
    p.contextFlags = saveContexFlags;
    var result = p.factory.NewFunctionExpression(modifiers, asteriskToken, name, typeParameters, parameters, returnType, nil, body);
    p.finishNode(result, pos);
    Ꮡp.withJSDoc(result, jsdoc);
    p.checkJSSyntax(result);
    return result;
}

internal static ж<ast.Node> parseOptionalBindingIdentifier(this ref Parser p) {
    if (p.isBindingIdentifier()) {
        return p.parseBindingIdentifier();
    }
    return default;
}

internal static ж<astꓸExpression> parseDecoratedExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    var jsdoc = p.jsdocScannerInfo();
    var modifiers = Ꮡp.parseModifiersEx(true, false, false);
    if (p.token == ast.KindClassKeyword) {
        return Ꮡp.parseClassDeclarationOrExpression(pos, jsdoc, modifiers, ast.KindClassExpression);
    }
    p.parseErrorAt(p.nodePos(), p.nodePos(), diagnostics.Expression_expected);
    return p.finishNode(p.factory.NewMissingDeclaration(modifiers), pos);
}

internal static void unparseExpressionWithTypeArguments(this ref Parser p, ж<ast.Node> Ꮡexpression, ж<ast.NodeList> ᏑtypeArguments, ж<ast.Node> result) {
    ref var expression = ref Ꮡexpression.DerefOrNull();
    ref var typeArguments = ref ᏑtypeArguments.DerefOrNull();

    if (Ꮡexpression != nil) {
        expression.Parent = result;
    }
    if (ᏑtypeArguments != nil) {
        foreach (var (_, a) in range(typeArguments.Nodes)) {
            a.Parent = result;
        }
    }
}

internal static ж<ast.Node> parseNewExpressionOrNewDotTarget(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    nint pos = p.nodePos();
    p.parseExpected(ast.KindNewKeyword);
    if (p.parseOptional(ast.KindDotToken)) {
        var name = p.parseIdentifierName();
        return p.finishNode(p.factory.NewMetaProperty(ast.KindNewKeyword, name), pos);
    }
    nint expressionPos = p.nodePos();
    var expression = Ꮡp.parseMemberExpressionRest(expressionPos, Ꮡp.parsePrimaryExpression(), false);
    ж<ast.NodeList> typeArguments = default;
    if (expression.Kind == ast.KindExpressionWithTypeArguments) {
        typeArguments = expression.TypeArgumentList();
        expression = expression.AsExpressionWithTypeArguments().Value.Expression;
    }
    if (p.token == ast.KindQuestionDotToken) {
        p.parseErrorAtCurrentToken(diagnostics.Invalid_optional_chain_from_new_expression_Did_you_mean_to_call_0, scanner.GetTextOfNodeFromSourceText(p.sourceText, expression, false));
    }
    ж<ast.NodeList> argumentList = default;
    if (p.token == ast.KindOpenParenToken) {
        argumentList = Ꮡp.parseArgumentList();
    }
    var result = p.checkJSSyntax(p.finishNode(p.factory.NewNewExpression(expression, typeArguments, argumentList), pos));
    p.unparseExpressionWithTypeArguments(expression, typeArguments, result);
    return result;
}

internal static ж<ast.Node> parseKeywordExpression(this ref Parser p) {
    nint pos = p.nodePos();
    var result = p.factory.NewKeywordExpression(p.token);
    p.nextToken();
    return p.finishNode(result, pos);
}

internal static ж<ast.Node> parseLiteralExpression(this ref Parser p) {
    nint pos = p.nodePos();
    @string text = p.scanner.TokenValue();
    var tokenFlags = p.scanner.TokenFlags();
    ж<ast.Node> result = default;
    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindStringLiteral: {
        result = p.factory.NewStringLiteral(text, tokenFlags);
        break;
    }
    case var ᴛ when ᴛ == ast.KindNumericLiteral: {
        result = p.factory.NewNumericLiteral(text, tokenFlags);
        break;
    }
    case var ᴛ when ᴛ == ast.KindBigIntLiteral: {
        result = p.factory.NewBigIntLiteral(text, tokenFlags);
        break;
    }
    case var ᴛ when ᴛ == ast.KindRegularExpressionLiteral: {
        result = p.factory.NewRegularExpressionLiteral(text, tokenFlags);
        break;
    }
    case var ᴛ when ᴛ == ast.KindNoSubstitutionTemplateLiteral: {
        result = p.factory.NewNoSubstitutionTemplateLiteral(text, tokenFlags);
        break;
    }
    default: {
        throw panic("Unhandled case in parseLiteralExpression");
        break;
    }}

    p.nextToken();
    return p.finishNode(result, pos);
}

internal static ж<ast.Node> parseIdentifierNameErrorOnUnicodeEscapeSequence(this ref Parser p) {
    if (p.scanner.HasUnicodeEscape() || p.scanner.HasExtendedUnicodeEscape()) {
        p.parseErrorAtCurrentToken(diagnostics.Unicode_escape_sequence_cannot_appear_here);
    }
    return p.createIdentifier(tokenIsIdentifierOrKeyword(p.token));
}

internal static ж<ast.Node> parseBindingIdentifier(this ref Parser p) {
    return p.parseBindingIdentifierWithDiagnostic(nil);
}

internal static ж<ast.Node> parseBindingIdentifierWithDiagnostic(this ref Parser p, ж<diagnostics.Message> privateIdentifierDiagnosticMessage) {
    var saveHasAwaitIdentifier = p.statementHasAwaitIdentifier;
    var id = p.createIdentifierWithDiagnostic(p.isBindingIdentifier(), nil, privateIdentifierDiagnosticMessage);
    p.statementHasAwaitIdentifier = saveHasAwaitIdentifier;
    return id;
}

internal static ж<ast.Node> parseIdentifierName(this ref Parser p) {
    return p.parseIdentifierNameWithDiagnostic(nil);
}

internal static ж<ast.Node> parseIdentifierNameWithDiagnostic(this ref Parser p, ж<diagnostics.Message> diagnosticMessage) {
    return p.createIdentifierWithDiagnostic(tokenIsIdentifierOrKeyword(p.token), diagnosticMessage, nil);
}

internal static ж<ast.Node> parseIdentifier(this ref Parser p) {
    return p.parseIdentifierWithDiagnostic(nil, nil);
}

internal static ж<ast.Node> parseIdentifierWithDiagnostic(this ref Parser p, ж<diagnostics.Message> diagnosticMessage, ж<diagnostics.Message> privateIdentifierDiagnosticMessage) {
    return p.createIdentifierWithDiagnostic(p.isIdentifier(), diagnosticMessage, privateIdentifierDiagnosticMessage);
}

internal static ж<ast.Node> createIdentifier(this ref Parser p, bool isIdentifier) {
    return p.createIdentifierWithDiagnostic(isIdentifier, nil, nil);
}

internal static ж<ast.Node> createIdentifierWithDiagnostic(this ref Parser p, bool isIdentifier, ж<diagnostics.Message> diagnosticMessage, ж<diagnostics.Message> privateIdentifierDiagnosticMessage) {
    if (isIdentifier) {
        nint pos = default;
        if (p.scanner.HasPrecedingJSDocLeadingAsterisks()) {
            pos = p.scanner.TokenStart();
        } else {
            pos = p.nodePos();
        }
        @string text = p.scanner.TokenValue();
        p.nextTokenWithoutCheck();
        return p.finishNode(p.newIdentifier(text), pos);
    }
    if (p.token == ast.KindPrivateIdentifier) {
        if (privateIdentifierDiagnosticMessage != nil) {
            p.parseErrorAtCurrentToken(privateIdentifierDiagnosticMessage);
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Private_identifiers_are_not_allowed_outside_class_bodies);
        }
        return p.createIdentifier(true);
    }
    var reportAtCurrentPosition = p.token == ast.KindEndOfFile;
    if (diagnosticMessage != nil) {
        if (reportAtCurrentPosition) {
            nint pos = p.scanner.TokenFullStart();
            p.parseErrorAt(pos, pos, diagnosticMessage);
        } else {
            p.parseErrorAtCurrentToken(diagnosticMessage);
        }
    } else if (isReservedWord(p.token)) {
        if (reportAtCurrentPosition) {
            nint pos = p.scanner.TokenFullStart();
            p.parseErrorAt(pos, pos, diagnostics.Identifier_expected_0_is_a_reserved_word_that_cannot_be_used_here, p.scanner.TokenText());
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Identifier_expected_0_is_a_reserved_word_that_cannot_be_used_here, p.scanner.TokenText());
        }
    } else {
        if (reportAtCurrentPosition) {
            nint pos = p.scanner.TokenFullStart();
            p.parseErrorAt(pos, pos, diagnostics.Identifier_expected);
        } else {
            p.parseErrorAtCurrentToken(diagnostics.Identifier_expected);
        }
    }
    return p.createMissingIdentifier();
}

internal static ж<ast.NodeList> newNodeList(this ref Parser p, core.TextRange loc, slice<ж<ast.Node>> nodes) {
    var list = p.factory.NewNodeList(nodes);
    list.Loc = loc;
    return list;
}

internal static ж<ast.ModifierList> newModifierList(this ref Parser p, core.TextRange loc, slice<ж<ast.Node>> nodes) {
    var list = p.factory.NewModifierList(nodes);
    list.Loc = loc;
    return list;
}

internal static ж<ast.Node> finishNode(this ref Parser p, ж<ast.Node> node, nint pos) {
    return p.finishNodeWithEnd(node, pos, p.nodePos());
}

internal static ж<ast.Node> finishNodeWithEnd(this ref Parser p, ж<ast.Node> Ꮡnode, nint pos, nint end) {
    ref var node = ref Ꮡnode.DerefOrNull();

    node.Loc = core.NewTextRange(pos, end);
    node.Flags |= (ast.NodeFlags)(p.contextFlags);
    if (p.hasParseError) {
        node.Flags |= (ast.NodeFlags)(ast.NodeFlagsThisNodeHasError);
        p.hasParseError = false;
    }
    p.overrideParentInImmediateChildren(Ꮡnode);
    return Ꮡnode;
}

internal static void overrideParentInImmediateChildren(this ref Parser p, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    p.currentParent = Ꮡnode;
    node.ForEachChild(p.setParentFromContext);
    p.currentParent = default;
}

internal static bool nextTokenIsSlash(this ref Parser p) {
    return p.nextToken() == ast.KindSlashToken;
}

internal static bool scanTypeMemberStart(this ref Parser p) {
    if (p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken || p.token == ast.KindGetKeyword || p.token == ast.KindSetKeyword) {
        return true;
    }
    var idToken = false;
    while (ast.IsModifierKind(p.token)) {
        idToken = true;
        p.nextToken();
    }
    if (p.token == ast.KindOpenBracketToken) {
        return true;
    }
    if (p.isLiteralPropertyName()) {
        idToken = true;
        p.nextToken();
    }
    if (idToken) {
        return p.token == ast.KindOpenParenToken || p.token == ast.KindLessThanToken || p.token == ast.KindQuestionToken || p.token == ast.KindColonToken || p.token == ast.KindCommaToken || p.canParseSemicolon();
    }
    return false;
}

internal static bool scanClassMemberStart(this ref Parser p) {
    var idToken = ast.KindUnknown;
    if (p.token == ast.KindAtToken) {
        return true;
    }
    while (ast.IsModifierKind(p.token)) {
        idToken = p.token;
        if (ast.IsClassMemberModifier(idToken)) {
            return true;
        }
        p.nextToken();
    }
    if (p.token == ast.KindAsteriskToken) {
        return true;
    }
    if (p.isLiteralPropertyName()) {
        idToken = p.token;
        p.nextToken();
    }
    if (p.token == ast.KindOpenBracketToken) {
        return true;
    }
    if (idToken != ast.KindUnknown) {
        if (!ast.IsKeyword(idToken) || idToken == ast.KindSetKeyword || idToken == ast.KindGetKeyword) {
            return true;
        }
        switch (p.token) {
        case var ᴛ when ᴛ == ast.KindOpenParenToken || ᴛ == ast.KindLessThanToken || ᴛ == ast.KindExclamationToken || ᴛ == ast.KindColonToken || ᴛ == ast.KindEqualsToken || ᴛ == ast.KindQuestionToken: {
            return true;
        }}

        return p.canParseSemicolon();
    }
    return false;
}

internal static bool canParseSemicolon(this ref Parser p) {
    return p.token == ast.KindSemicolonToken || p.token == ast.KindCloseBraceToken || p.token == ast.KindEndOfFile || p.hasPrecedingLineBreak();
}

internal static bool tryParseSemicolon(this ref Parser p) {
    if (!p.canParseSemicolon()) {
        return false;
    }
    if (p.token == ast.KindSemicolonToken) {
        p.nextToken();
    }
    return true;
}

internal static bool parseSemicolon(this ref Parser p) {
    return p.tryParseSemicolon() || p.parseExpected(ast.KindSemicolonToken);
}

internal static bool isLiteralPropertyName(this ref Parser p) {
    return tokenIsIdentifierOrKeyword(p.token) || p.token == ast.KindStringLiteral || p.token == ast.KindNumericLiteral || p.token == ast.KindBigIntLiteral;
}

internal static bool isStartOfStatement(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindAtToken || ᴛ == ast.KindSemicolonToken || ᴛ == ast.KindOpenBraceToken || ᴛ == ast.KindVarKeyword || ᴛ == ast.KindLetKeyword || ᴛ == ast.KindUsingKeyword || ᴛ == ast.KindFunctionKeyword || ᴛ == ast.KindClassKeyword || ᴛ == ast.KindEnumKeyword || ᴛ == ast.KindIfKeyword || ᴛ == ast.KindDoKeyword || ᴛ == ast.KindWhileKeyword || ᴛ == ast.KindForKeyword || ᴛ == ast.KindContinueKeyword || ᴛ == ast.KindBreakKeyword || ᴛ == ast.KindReturnKeyword || ᴛ == ast.KindWithKeyword || ᴛ == ast.KindSwitchKeyword || ᴛ == ast.KindThrowKeyword || ᴛ == ast.KindTryKeyword || ᴛ == ast.KindDebuggerKeyword || ᴛ == ast.KindCatchKeyword || ᴛ == ast.KindFinallyKeyword: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindImportKeyword: {
        return Ꮡp.isStartOfDeclaration() || Ꮡp.isNextTokenOpenParenOrLessThanOrDot();
    }
    case var ᴛ when ᴛ == ast.KindConstKeyword || ᴛ == ast.KindExportKeyword: {
        return Ꮡp.isStartOfDeclaration();
    }
    case var ᴛ when ᴛ == ast.KindAsyncKeyword || ᴛ == ast.KindDeclareKeyword || ᴛ == ast.KindInterfaceKeyword || ᴛ == ast.KindModuleKeyword || ᴛ == ast.KindNamespaceKeyword || ᴛ == ast.KindTypeKeyword || ᴛ == ast.KindGlobalKeyword || ᴛ == ast.KindDeferKeyword: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindAccessorKeyword || ᴛ == ast.KindPublicKeyword || ᴛ == ast.KindPrivateKeyword || ᴛ == ast.KindProtectedKeyword || ᴛ == ast.KindStaticKeyword || ᴛ == ast.KindReadonlyKeyword: {
        return Ꮡp.isStartOfDeclaration() || !Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsIdentifierOrKeywordOnSameLine)));
    }
    default: {
        return Ꮡp.isStartOfExpression();
    }}

}

internal static bool isStartOfDeclaration(this ж<Parser> Ꮡp) {
    return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.scanStartOfDeclaration)));
}

internal static bool scanStartOfDeclaration(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    for (;;) {
        switch (p.token) {
        case var ᴛ when ᴛ == ast.KindVarKeyword || ᴛ == ast.KindLetKeyword || ᴛ == ast.KindConstKeyword || ᴛ == ast.KindFunctionKeyword || ᴛ == ast.KindClassKeyword || ᴛ == ast.KindEnumKeyword: {
            return true;
        }
        case var ᴛ when ᴛ == ast.KindUsingKeyword: {
            return Ꮡp.isUsingDeclaration();
        }
        case var ᴛ when ᴛ == ast.KindAwaitKeyword: {
            return Ꮡp.isAwaitUsingDeclaration();
        }
        case var ᴛ when ᴛ == ast.KindInterfaceKeyword || ᴛ == ast.KindTypeKeyword || ᴛ == ast.KindDeferKeyword: {
            return p.nextTokenIsIdentifierOnSameLine();
        }
        case var ᴛ when ᴛ == ast.KindModuleKeyword || ᴛ == ast.KindNamespaceKeyword: {
            return p.nextTokenIsIdentifierOrStringLiteralOnSameLine();
        }
        case var ᴛ when ᴛ == ast.KindAbstractKeyword || ᴛ == ast.KindAccessorKeyword || ᴛ == ast.KindAsyncKeyword || ᴛ == ast.KindDeclareKeyword || ᴛ == ast.KindPrivateKeyword || ᴛ == ast.KindProtectedKeyword || ᴛ == ast.KindPublicKeyword || ᴛ == ast.KindReadonlyKeyword: {
            var previousToken = p.token;
            p.nextToken();
            if (p.hasPrecedingLineBreak()) {
                return false;
            }
            if (previousToken == ast.KindDeclareKeyword && p.token == ast.KindTypeKeyword) {
                return true;
            }
            continue;
            break;
        }
        case var ᴛ when ᴛ == ast.KindGlobalKeyword: {
            p.nextToken();
            return p.token == ast.KindOpenBraceToken || p.token == ast.KindIdentifier || p.token == ast.KindExportKeyword;
        }
        case var ᴛ when ᴛ == ast.KindImportKeyword: {
            p.nextToken();
            return p.token == ast.KindDeferKeyword || p.token == ast.KindStringLiteral || p.token == ast.KindAsteriskToken || p.token == ast.KindOpenBraceToken || tokenIsIdentifierOrKeyword(p.token);
        }
        case var ᴛ when ᴛ == ast.KindExportKeyword: {
            p.nextToken();
            if (p.token == ast.KindEqualsToken || p.token == ast.KindAsteriskToken || p.token == ast.KindOpenBraceToken || p.token == ast.KindDefaultKeyword || p.token == ast.KindAsKeyword || p.token == ast.KindAtToken) {
                return true;
            }
            if (p.token == ast.KindTypeKeyword) {
                p.nextToken();
                return p.token == ast.KindAsteriskToken || p.token == ast.KindOpenBraceToken || p.isIdentifier() && !p.hasPrecedingLineBreak();
            }
            continue;
            break;
        }
        case var ᴛ when ᴛ == ast.KindStaticKeyword: {
            p.nextToken();
            continue;
            break;
        }}

        return false;
    }
}

internal static bool isStartOfExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp.isStartOfLeftHandSideExpression()) {
        return true;
    }
    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindPlusToken || ᴛ == ast.KindMinusToken || ᴛ == ast.KindTildeToken || ᴛ == ast.KindExclamationToken || ᴛ == ast.KindDeleteKeyword || ᴛ == ast.KindTypeOfKeyword || ᴛ == ast.KindVoidKeyword || ᴛ == ast.KindPlusPlusToken || ᴛ == ast.KindMinusMinusToken || ᴛ == ast.KindLessThanToken || ᴛ == ast.KindAwaitKeyword || ᴛ == ast.KindYieldKeyword || ᴛ == ast.KindPrivateIdentifier || ᴛ == ast.KindAtToken: {
        return true;
    }}

    if (p.isBinaryOperator()) {
        return true;
    }
    return p.isIdentifier();
}

internal static bool isStartOfLeftHandSideExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindThisKeyword || ᴛ == ast.KindSuperKeyword || ᴛ == ast.KindNullKeyword || ᴛ == ast.KindTrueKeyword || ᴛ == ast.KindFalseKeyword || ᴛ == ast.KindNumericLiteral || ᴛ == ast.KindBigIntLiteral || ᴛ == ast.KindStringLiteral || ᴛ == ast.KindNoSubstitutionTemplateLiteral || ᴛ == ast.KindTemplateHead || ᴛ == ast.KindOpenParenToken || ᴛ == ast.KindOpenBracketToken || ᴛ == ast.KindOpenBraceToken || ᴛ == ast.KindFunctionKeyword || ᴛ == ast.KindClassKeyword || ᴛ == ast.KindNewKeyword || ᴛ == ast.KindSlashToken || ᴛ == ast.KindSlashEqualsToken || ᴛ == ast.KindIdentifier: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindImportKeyword: {
        return Ꮡp.isNextTokenOpenParenOrLessThanOrDot();
    }}

    return p.isIdentifier();
}

internal static bool isStartOfType(this ж<Parser> Ꮡp, bool inStartOfParameter) {
    ref var p = ref Ꮡp.DerefOrNull();

    switch (p.token) {
    case var ᴛ when ᴛ == ast.KindAnyKeyword || ᴛ == ast.KindUnknownKeyword || ᴛ == ast.KindStringKeyword || ᴛ == ast.KindNumberKeyword || ᴛ == ast.KindBigIntKeyword || ᴛ == ast.KindBooleanKeyword || ᴛ == ast.KindReadonlyKeyword || ᴛ == ast.KindSymbolKeyword || ᴛ == ast.KindUniqueKeyword || ᴛ == ast.KindVoidKeyword || ᴛ == ast.KindUndefinedKeyword || ᴛ == ast.KindNullKeyword || ᴛ == ast.KindThisKeyword || ᴛ == ast.KindTypeOfKeyword || ᴛ == ast.KindNeverKeyword || ᴛ == ast.KindOpenBraceToken || ᴛ == ast.KindOpenBracketToken || ᴛ == ast.KindLessThanToken || ᴛ == ast.KindBarToken || ᴛ == ast.KindAmpersandToken || ᴛ == ast.KindNewKeyword || ᴛ == ast.KindStringLiteral || ᴛ == ast.KindNumericLiteral || ᴛ == ast.KindBigIntLiteral || ᴛ == ast.KindTrueKeyword || ᴛ == ast.KindFalseKeyword || ᴛ == ast.KindObjectKeyword || ᴛ == ast.KindAsteriskToken || ᴛ == ast.KindQuestionToken || ᴛ == ast.KindExclamationToken || ᴛ == ast.KindDotDotDotToken || ᴛ == ast.KindInferKeyword || ᴛ == ast.KindImportKeyword || ᴛ == ast.KindAssertsKeyword || ᴛ == ast.KindNoSubstitutionTemplateLiteral || ᴛ == ast.KindTemplateHead: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindFunctionKeyword: {
        return !inStartOfParameter;
    }
    case var ᴛ when ᴛ == ast.KindMinusToken: {
        return !inStartOfParameter && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsNumericOrBigIntLiteral)));
    }
    case var ᴛ when ᴛ == ast.KindOpenParenToken: {
        return !inStartOfParameter && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsParenthesizedOrFunctionType)));
    }}

    return p.isIdentifier();
}

internal static bool nextTokenIsNumericOrBigIntLiteral(this ref Parser p) {
    p.nextToken();
    return p.token == ast.KindNumericLiteral || p.token == ast.KindBigIntLiteral;
}

internal static bool nextIsParenthesizedOrFunctionType(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.nextToken();
    return p.token == ast.KindCloseParenToken || Ꮡp.isStartOfParameter(false) || Ꮡp.isStartOfType(false);
}

internal static bool isStartOfParameter(this ж<Parser> Ꮡp, bool isJSDocParameter) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.token == ast.KindDotDotDotToken || p.isBindingIdentifierOrPrivateIdentifierOrPattern() || ast.IsModifierKind(p.token) || p.token == ast.KindAtToken || Ꮡp.isStartOfType(!isJSDocParameter);
}

internal static bool isBindingIdentifierOrPrivateIdentifierOrPattern(this ref Parser p) {
    return p.token == ast.KindOpenBraceToken || p.token == ast.KindOpenBracketToken || p.token == ast.KindPrivateIdentifier || p.isBindingIdentifier();
}

internal static bool isNextTokenOpenParenOrLessThanOrDot(this ж<Parser> Ꮡp) {
    return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsOpenParenOrLessThanOrDot)));
}

internal static bool nextTokenIsOpenParenOrLessThanOrDot(this ref Parser p) {
    switch (p.nextToken()) {
    case var ᴛ when ᴛ == ast.KindOpenParenToken || ᴛ == ast.KindLessThanToken || ᴛ == ast.KindDotToken: {
        return true;
    }}

    return false;
}

internal static bool nextTokenIsIdentifierOnSameLine(this ref Parser p) {
    p.nextToken();
    return p.isIdentifier() && !p.hasPrecedingLineBreak();
}

internal static bool nextTokenIsIdentifierOrStringLiteralOnSameLine(this ref Parser p) {
    p.nextToken();
    return (p.isIdentifier() || p.token == ast.KindStringLiteral) && !p.hasPrecedingLineBreak();
}

internal static bool isIdentifier(this ref Parser p) {
    if (p.token == ast.KindIdentifier) {
        return true;
    }
    if (p.token == ast.KindYieldKeyword && p.inYieldContext() || p.token == ast.KindAwaitKeyword && p.inAwaitContext()) {
        return false;
    }
    return p.token > ast.KindLastReservedWord;
}

internal static bool isBindingIdentifier(this ref Parser p) {
    return p.token == ast.KindIdentifier || p.token > ast.KindLastReservedWord;
}

internal static bool isImportAttributeName(this ref Parser p) {
    return tokenIsIdentifierOrKeyword(p.token) || p.token == ast.KindStringLiteral;
}

internal static bool isBinaryOperator(this ref Parser p) {
    if (p.inDisallowInContext() && p.token == ast.KindInKeyword) {
        return false;
    }
    return ast.GetBinaryOperatorPrecedence(p.token) != ast.OperatorPrecedenceInvalid;
}

internal static bool isValidHeritageClauseObjectLiteral(this ж<Parser> Ꮡp) {
    return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsValidHeritageClauseObjectLiteral)));
}

internal static bool nextIsValidHeritageClauseObjectLiteral(this ref Parser p) {
    if (p.nextToken() == ast.KindCloseBraceToken) {
        var next = p.nextToken();
        return next == ast.KindCommaToken || next == ast.KindOpenBraceToken || next == ast.KindExtendsKeyword || next == ast.KindImplementsKeyword;
    }
    return true;
}

internal static bool isHeritageClause(this ref Parser p) {
    return p.token == ast.KindExtendsKeyword || p.token == ast.KindImplementsKeyword;
}

internal static bool isHeritageClauseExtendsOrImplementsKeyword(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.isHeritageClause() && Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsStartOfExpression)));
}

internal static bool nextIsStartOfExpression(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.nextToken();
    return Ꮡp.isStartOfExpression();
}

internal static bool isUsingDeclaration(this ж<Parser> Ꮡp) {
    return Ꮡp.lookAhead((ж<Parser> pΔ1) => pΔ1.nextTokenIsBindingIdentifierOrStartOfDestructuringOnSameLine(false));
}

internal static bool nextTokenIsEqualsOrSemicolonOrColonToken(this ref Parser p) {
    p.nextToken();
    return p.token == ast.KindEqualsToken || p.token == ast.KindSemicolonToken || p.token == ast.KindColonToken;
}

internal static bool nextTokenIsBindingIdentifierOrStartOfDestructuringOnSameLine(this ж<Parser> Ꮡp, bool disallowOf) {
    ref var p = ref Ꮡp.DerefOrNull();

    p.nextToken();
    if (disallowOf && p.token == ast.KindOfKeyword) {
        return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextTokenIsEqualsOrSemicolonOrColonToken)));
    }
    return (p.isBindingIdentifier() || p.token == ast.KindOpenBraceToken) && !p.hasPrecedingLineBreak();
}

internal static bool nextTokenIsBindingIdentifierOrStartOfDestructuringOnSameLineDisallowOf(this ж<Parser> Ꮡp) {
    return Ꮡp.nextTokenIsBindingIdentifierOrStartOfDestructuringOnSameLine(true);
}

internal static bool isAwaitUsingDeclaration(this ж<Parser> Ꮡp) {
    return Ꮡp.lookAhead(((Func<ж<Parser>, bool>)(parserᴛParser.nextIsUsingKeywordThenBindingIdentifierOrStartOfObjectDestructuringOnSameLine)));
}

internal static bool nextIsUsingKeywordThenBindingIdentifierOrStartOfObjectDestructuringOnSameLine(this ж<Parser> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return p.nextToken() == ast.KindUsingKeyword && Ꮡp.nextTokenIsBindingIdentifierOrStartOfDestructuringOnSameLine(false);
}

internal static bool nextTokenIsTokenStringLiteral(this ref Parser p) {
    return p.nextToken() == ast.KindStringLiteral;
}

internal static void setContextFlags(this ref Parser p, ast.NodeFlags flags, bool value) {
    if (value) {
        p.contextFlags |= (ast.NodeFlags)(flags);
    } else {
        p.contextFlags &= unchecked((ast.NodeFlags)~(ast.NodeFlags)(flags));
    }
}

} partial class parser_package {

internal static T doInContext<T>(ж<Parser> Ꮡp, ast.NodeFlags flags, bool value, Func<ж<Parser>, T> f) {
    ref var p = ref Ꮡp.DerefOrNull();

    var saveContextFlags = p.contextFlags;
    p.setContextFlags(flags, value);
    var result = f(Ꮡp);
    p.contextFlags = saveContextFlags;
    return result;
}

} partial class parserᴛParser {

internal static bool inYieldContext(this ref Parser p) {
    return (ast.NodeFlags)(p.contextFlags & ast.NodeFlagsYieldContext) != 0;
}

internal static bool inDisallowInContext(this ref Parser p) {
    return (ast.NodeFlags)(p.contextFlags & ast.NodeFlagsDisallowInContext) != 0;
}

internal static bool inDisallowConditionalTypesContext(this ref Parser p) {
    return (ast.NodeFlags)(p.contextFlags & ast.NodeFlagsDisallowConditionalTypesContext) != 0;
}

internal static bool inDecoratorContext(this ref Parser p) {
    return (ast.NodeFlags)(p.contextFlags & ast.NodeFlagsDecoratorContext) != 0;
}

internal static bool inAwaitContext(this ref Parser p) {
    return (ast.NodeFlags)(p.contextFlags & ast.NodeFlagsAwaitContext) != 0;
}

internal static core.TextRange skipRangeTrivia(this ref Parser p, core.TextRange textRange) {
    return core.NewTextRange(scanner.SkipTrivia(p.sourceText, textRange.Pos()), textRange.End());
}

} partial class parser_package {

internal static bool isReservedWord(ast.Kind token) {
    return ast.KindFirstReservedWord <= token && token <= ast.KindLastReservedWord;
}

internal static slice<ж<ast.Diagnostic>> attachFileToDiagnostics(slice<ж<ast.Diagnostic>> diagnostics, ж<ast.SourceFile> @file) {
    foreach (var (_, d) in range(diagnostics)) {
        d.SetFile(@file);
        foreach (var (_, r) in range(d.RelatedInformation())) {
            r.SetFile(@file);
        }
    }
    return diagnostics;
}

internal static slice<ast.Pragma> /*pragmas*/ getCommentPragmas(ж<ast.NodeFactory> f, @string sourceText) {
    slice<ast.Pragma> pragmas = default;

    foreach (var commentRange in range<ast.CommentRange>(scanner.GetLeadingCommentRanges(f, sourceText, 0).Invoke)) {
        @string comment = sourceText.slice(commentRange.TextRange.Pos(), commentRange.TextRange.End());
        pragmas = appendꓸꓸꓸ(pragmas, extractPragmas(commentRange, comment));
    }
    return pragmas;
}

internal static slice<ast.Pragma> extractPragmas(ast.CommentRange commentRange, @string text) {
    if (commentRange.Kind == ast.KindSingleLineCommentTrivia) {
        nint pos = 2;
        var tripleSlash = match(text, pos, "/");
        if (tripleSlash) {
            pos++;
        }
        pos = skipBlanks(text, pos);
        if (tripleSlash && match(text, pos, "<")) {
            @string tagName = extractName(text, pos + 1);
            if (tagName != "reference"u8) {
                return default;
            }
            pos += 10;
            var args = new map<@string, ast.PragmaArgument>();
            for (;;) {
                pos = skipBlanks(text, pos);
                if (match(text, pos, "/>")) {
                    break;
                }
                @string argName = extractName(text, pos);
                if (argName == ""u8) {
                    break;
                }
                pos = skipBlanks(text, pos + len(argName));
                if (!match(text, pos, "=")) {
                    break;
                }
                pos = skipBlanks(text, pos + 1);
                var (value, ok) = extractQuotedString(text, pos);
                if (!ok) {
                    break;
                }
                args[argName] = new ast.PragmaArgument(
                    Name: argName,
                    Value: value,
                    TextRange: core.NewTextRange(commentRange.TextRange.Pos() + pos + 1, commentRange.TextRange.Pos() + pos + 1 + len(value))
                );
                pos += len(value) + 2;
            }
            return [new(
                CommentRange: commentRange,
                Name: "reference",
                Args: args
            )
            ];
        }
        if (match(text, pos, "@")) {
            pos++;
            @string pragmaName = extractName(text, pos);
            if (!(pragmaName == "ts-check"u8 || pragmaName == "ts-nocheck"u8)) {
                return default;
            }
            return [new(
                CommentRange: commentRange,
                Name: pragmaName
            )
            ];
        }
    }
    if (commentRange.Kind == ast.KindMultiLineCommentTrivia) {
        text = strings.TrimSuffix(text, "*/");
        nint pos = 2;
        slice<ast.Pragma> pragmas = default;
        for (;;) {
            pos = skipTo(text, pos, "@"); if (pos < 0) {
                break;
            }
            nint namePos = pos + 1;
            nint nameEnd = skipNonBlanks(text, namePos);
            if (nameEnd == namePos) {
                pos++;
                continue;
            }
            nint lineEnd = lineEndPos(text, pos);
            @string pragmaName = strings.ToLower(text.slice(namePos, nameEnd));
            if (pragmaName == "jsx"u8 || pragmaName == "jsxfrag"u8 || pragmaName == "jsximportsource"u8 || pragmaName == "jsxruntime"u8) {
                nint start = skipBlanks(text, nameEnd);
                nint argEnd = skipNonBlanks(text, start);
                if (argEnd != start) {
                    var args = new map<@string, ast.PragmaArgument>(1);
                    args["factory"] = new ast.PragmaArgument(
                        Name: "factory",
                        Value: text.slice(start, argEnd),
                        TextRange: core.NewTextRange(commentRange.TextRange.Pos() + start, commentRange.TextRange.Pos() + argEnd)
                    );
                    pragmas = append(pragmas, new ast.Pragma(
                        CommentRange: commentRange,
                        Name: pragmaName,
                        Args: args
                    ));
                }
            }
            pos = lineEnd;
        }
        return pragmas;
    }
    return default;
}

internal static bool match(@string text, nint pos, @string s) {
    return strings.HasPrefix(text.slice(pos), s);
}

internal static nint skipBlanks(@string text, nint pos) {
    while (pos < len(text) && (text[pos] == ' ' || text[pos] == '\t')) {
        pos++;
    }
    return pos;
}

internal static nint skipNonBlanks(@string text, nint pos) {
    while (pos < len(text) && (text[pos] != ' ' && text[pos] != '\t' && text[pos] != '\r' && text[pos] != '\n')) {
        pos++;
    }
    return pos;
}

internal static nint skipTo(@string text, nint pos, @string s) {
    if (pos >= len(text)) {
        return -1;
    }
    nint i = strings.Index(text.slice(pos), s);
    if (i < 0) {
        return -1;
    }
    return pos + i;
}

internal static nint lineEndPos(@string text, nint pos) {
    while (pos < len(text)) {
        var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
        if (stringutil.IsLineBreak(ch)) {
            return pos;
        }
        pos += size;
    }
    return len(text);
}

internal static @string extractName(@string text, nint pos) {
    nint start = pos;
    while (pos < len(text) && (text[pos] >= 'A' && text[pos] <= 'Z' || text[pos] >= 'a' && text[pos] <= 'z' || text[pos] == '-')) {
        pos++;
    }
    return strings.ToLower(text.slice(start, pos));
}

internal static (@string, bool) extractQuotedString(@string text, nint pos) {
    if (pos == len(text)) {
        return ("", false);
    }
    var quote = text[pos];
    if (quote != '\'' && quote != '"') {
        return ("", false);
    }
    pos++;
    nint start = pos;
    while (pos < len(text) && text[pos] != quote) {
        pos++;
    }
    if (pos == len(text)) {
        return ("", false);
    }
    return (text.slice(start, pos), true);
}

} partial class parserᴛParser {

internal static void processPragmasIntoFields(this ref Parser p, ж<ast.SourceFile> Ꮡcontext) {
    ref var context = ref Ꮡcontext.DerefOrNull();

    context.CheckJsDirective = default;
    context.ReferencedFiles = default;
    context.TypeReferenceDirectives = default;
    context.LibReferenceDirectives = default;
    foreach (var (_, pragma) in range(context.Pragmas)) {
        var exprᴛ1 = pragma.Name;
        switch (ᐧ) {
        case {} when exprᴛ1 == "reference"u8: {
            var (types, typesOk) = pragma.Args["types", ꟷ];
            var (lib, libOk) = pragma.Args["lib", ꟷ];
            var (path, pathOk) = pragma.Args["path", ꟷ];
            var (resolutionMode, resolutionModeOk) = pragma.Args["resolution-mode", ꟷ];
            var (preserve, preserveOk) = pragma.Args["preserve", ꟷ];
            var (noDefaultLib, noDefaultLibOk) = pragma.Args["no-default-lib", ꟷ];
            switch (ᐧ) {
            case {} when noDefaultLibOk && noDefaultLib.Value == "true"u8: {
                break;
            }
            case {} when typesOk: {
                ref var parsed = ref heap(new coreꓸResolutionMode(), out var Ꮡparsed);
                if (resolutionModeOk) {
                    parsed = p.parseResolutionMode(resolutionMode.Value, resolutionMode.TextRange.Pos(), resolutionMode.TextRange.End());
                }
                context.TypeReferenceDirectives = append(context.TypeReferenceDirectives, Ꮡ(new ast.FileReference(
                    TextRange: types.TextRange,
                    FileName: types.Value,
                    ResolutionMode: parsed,
                    Preserve: preserveOk && preserve.Value == "true"u8
                )));
                break;
            }
            case {} when libOk: {
                context.LibReferenceDirectives = append(context.LibReferenceDirectives, Ꮡ(new ast.FileReference(
                    TextRange: lib.TextRange,
                    FileName: lib.Value,
                    Preserve: preserveOk && preserve.Value == "true"u8
                )));
                break;
            }
            case {} when pathOk: {
                context.ReferencedFiles = append(context.ReferencedFiles, Ꮡ(new ast.FileReference(
                    TextRange: path.TextRange,
                    FileName: path.Value,
                    Preserve: preserveOk && preserve.Value == "true"u8
                )));
                break;
            }
            default: {
                p.parseErrorAtRange(pragma.TextRange, diagnostics.Invalid_reference_directive_syntax);
                break;
            }}

            break;
        }
        case {} when exprᴛ1 == "ts-check"u8 || exprᴛ1 == "ts-nocheck"u8: {
            if (context.CheckJsDirective == nil || pragma.TextRange.Pos() > context.CheckJsDirective.Range.TextRange.Pos()) {
                context.CheckJsDirective = Ꮡ(new ast.CheckJsDirective(
                    Enabled: pragma.Name == "ts-check"u8,
                    Range: pragma.CommentRange
                ));
            }
            break;
        }
        case {} when exprᴛ1 == "jsx"u8 || exprᴛ1 == "jsxfrag"u8 || exprᴛ1 == "jsximportsource"u8 || exprᴛ1 == "jsxruntime"u8: {
            break;
        }
        default: {
            throw panic("Unhandled pragma kind: " + pragma.Name);
            break;
        }}

    }
}

internal static coreꓸResolutionMode /*resolutionKind*/ parseResolutionMode(this ref Parser p, @string mode, nint pos, nint end) {
    coreꓸResolutionMode resolutionKind = default;

    if (mode == "import"u8) {
        resolutionKind = core.ModuleKindESNext;
        return resolutionKind;
    }
    if (mode == "require"u8) {
        resolutionKind = core.ModuleKindCommonJS;
        return resolutionKind;
    }
    p.parseErrorAt(pos, end, diagnostics.X_resolution_mode_should_be_either_require_or_import);
    return resolutionKind;
}

internal static void jsErrorAtRange(this ref Parser p, core.TextRange loc, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    p.jsDiagnostics = append(p.jsDiagnostics, ast.NewDiagnostic(nil, core.NewTextRange(scanner.SkipTrivia(p.sourceText, loc.Pos()), loc.End()), message, args.ꓸꓸꓸ));
}

internal static void checkJSDecoratorSyntax(this ref Parser p, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var modifiers = node.ModifierNodes();
    if (len(modifiers) == 0) {
        return;
    }

    if (ast.CanHaveIllegalDecorators(Ꮡnode)) {
        foreach (var (_, modifier) in range(modifiers)) {
            if (ast.IsDecorator(modifier)) {
                p.jsErrorAtRange(modifier.Loc, diagnostics.Decorators_are_not_valid_here);
                break;
            }
        }
    } else if (ast.CanHaveDecorators(Ꮡnode)) {
        nint decoratorIndex = core.FindIndex<ж<ast.Node>>(modifiers, ast.IsDecorator);
        if (decoratorIndex >= 0) {
            if (ast.IsClassDeclaration(Ꮡnode)) {
                nint exportIndex = core.FindIndex<ж<ast.Node>>(modifiers, isExportModifier);
                if (exportIndex >= 0) {
                    nint defaultIndex = core.FindIndex(modifiers, (ж<ast.Node> m) => m.Kind == ast.KindDefaultKeyword);
                    if (decoratorIndex > exportIndex && defaultIndex >= 0 && decoratorIndex < defaultIndex) {
                        p.jsErrorAtRange(modifiers[decoratorIndex].Loc, diagnostics.Decorators_are_not_valid_here);
                    } else if (decoratorIndex < exportIndex) {
                        nint trailingDecoratorIndex = -1;
                        for (nint i = exportIndex; i < len(modifiers); i++) {
                            if (ast.IsDecorator(modifiers[i])) {
                                trailingDecoratorIndex = i;
                                break;
                            }
                        }
                        if (trailingDecoratorIndex >= 0) {
                            var diag = ast.NewDiagnostic(
                                nil,
                                core.NewTextRange(scanner.SkipTrivia(p.sourceText, modifiers[trailingDecoratorIndex].Loc.Pos()), modifiers[trailingDecoratorIndex].Loc.End()),
                                diagnostics.Decorators_may_not_appear_after_export_or_export_default_if_they_also_appear_before_export);
                            diag.AddRelatedInfo(ast.NewDiagnostic(
                                nil,
                                core.NewTextRange(scanner.SkipTrivia(p.sourceText, modifiers[decoratorIndex].Loc.Pos()), modifiers[decoratorIndex].Loc.End()),
                                diagnostics.Decorator_used_before_export_here));
                            p.jsDiagnostics = append(p.jsDiagnostics, diag);
                        }
                    }
                }
            }
        }
    }
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
private static readonly object importTypeˢ = (@string)"import type"u8;
private static readonly object exportTypeˢ = (@string)"export type"u8;
private static readonly object importTypeˢ2 = (@string)"import...type"u8;
private static readonly object exportTypeˢ2 = (@string)"export...type"u8;
private static readonly object interfaceˢ = (@string)"interface"u8;
private static readonly object enumˢ = (@string)"enum"u8;

internal static ж<ast.Node> checkJSSyntax(this ref Parser p, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsJavaScriptFile) == 0 || (ast.NodeFlags)(node.Flags & ((ast.NodeFlags)(ast.NodeFlagsJSDoc | ast.NodeFlagsReparsed))) != 0) {
        return Ꮡnode;
    }
    var exprᴛ1 = node.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindParameter || exprᴛ1 == ast.KindPropertyDeclaration || exprᴛ1 == ast.KindMethodDeclaration) { matchᴛ1 = true;
        var token = node.QuestionToken(); if (token != nil && (ast.NodeFlags)(token.Flags & ast.NodeFlagsReparsed) == 0 && ast.IsQuestionToken(token)) {
            p.jsErrorAtRange(token.Loc, diagnostics.The_0_modifier_can_only_be_used_in_TypeScript_files, (@string)"?");
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindMethodSignature || exprᴛ1 == ast.KindConstructor || exprᴛ1 == ast.KindGetAccessor || exprᴛ1 == ast.KindSetAccessor || exprᴛ1 == ast.KindFunctionExpression || exprᴛ1 == ast.KindFunctionDeclaration || exprᴛ1 == ast.KindArrowFunction || exprᴛ1 == ast.KindVariableDeclaration || exprᴛ1 == ast.KindIndexSignature)) { matchᴛ1 = true;
        if (ast.IsFunctionLike(Ꮡnode) && node.Body() == nil) {
            p.jsErrorAtRange(node.Loc, diagnostics.Signature_declarations_can_only_be_used_in_TypeScript_files);
        } else if (node.Type() is var t && t != nil && (ast.NodeFlags)(t.Flags & ast.NodeFlagsReparsed) == 0) {
            p.jsErrorAtRange(t.Loc, diagnostics.Type_annotations_can_only_be_used_in_TypeScript_files);
        }
    }
    else if (exprᴛ1 == ast.KindImportDeclaration) { matchᴛ1 = true;
        var clause = node.ImportClause(); if (clause != nil && clause.IsTypeOnly()) {
            p.jsErrorAtRange(node.Loc, diagnostics.X_0_declarations_can_only_be_used_in_TypeScript_files, importTypeˢ);
        }
    }
    else if (exprᴛ1 == ast.KindExportDeclaration) { matchᴛ1 = true;
        if (node.IsTypeOnly()) {
            p.jsErrorAtRange(node.Loc, diagnostics.X_0_declarations_can_only_be_used_in_TypeScript_files, exportTypeˢ);
        }
    }
    else if (exprᴛ1 == ast.KindImportSpecifier) { matchᴛ1 = true;
        if (node.IsTypeOnly()) {
            p.jsErrorAtRange(node.Loc, diagnostics.X_0_declarations_can_only_be_used_in_TypeScript_files, importTypeˢ2);
        }
    }
    else if (exprᴛ1 == ast.KindExportSpecifier) { matchᴛ1 = true;
        if (node.IsTypeOnly()) {
            p.jsErrorAtRange(node.Loc, diagnostics.X_0_declarations_can_only_be_used_in_TypeScript_files, exportTypeˢ2);
        }
    }
    else if (exprᴛ1 == ast.KindImportEqualsDeclaration) { matchᴛ1 = true;
        p.jsErrorAtRange(node.Loc, diagnostics.X_import_can_only_be_used_in_TypeScript_files);
    }
    else if (exprᴛ1 == ast.KindExportAssignment) { matchᴛ1 = true;
        if (node.AsExportAssignment().IsExportEquals) {
            p.jsErrorAtRange(node.Loc, diagnostics.X_export_can_only_be_used_in_TypeScript_files);
        }
    }
    else if (exprᴛ1 == ast.KindHeritageClause) { matchᴛ1 = true;
        if (node.AsHeritageClause().Token == ast.KindImplementsKeyword) {
            p.jsErrorAtRange(node.Loc, diagnostics.X_implements_clauses_can_only_be_used_in_TypeScript_files);
        }
    }
    else if (exprᴛ1 == ast.KindInterfaceDeclaration) { matchᴛ1 = true;
        p.jsErrorAtRange(node.Name().Loc, diagnostics.X_0_declarations_can_only_be_used_in_TypeScript_files, interfaceˢ);
    }
    else if (exprᴛ1 == ast.KindModuleDeclaration) { matchᴛ1 = true;
        p.jsErrorAtRange(node.Name().Loc, diagnostics.X_0_declarations_can_only_be_used_in_TypeScript_files, scanner.TokenToString(node.AsModuleDeclaration().Keyword));
    }
    else if (exprᴛ1 == ast.KindTypeAliasDeclaration) { matchᴛ1 = true;
        p.jsErrorAtRange(node.Name().Loc, diagnostics.Type_aliases_can_only_be_used_in_TypeScript_files);
    }
    else if (exprᴛ1 == ast.KindEnumDeclaration) { matchᴛ1 = true;
        p.jsErrorAtRange(node.Name().Loc, diagnostics.X_0_declarations_can_only_be_used_in_TypeScript_files, enumˢ);
    }
    else if (exprᴛ1 == ast.KindNonNullExpression) { matchᴛ1 = true;
        p.jsErrorAtRange(node.Loc, diagnostics.Non_null_assertions_can_only_be_used_in_TypeScript_files);
    }
    else if (exprᴛ1 == ast.KindAsExpression) {
        p.jsErrorAtRange(node.Type().Loc, diagnostics.Type_assertion_expressions_can_only_be_used_in_TypeScript_files);
    }
    else if (exprᴛ1 == ast.KindSatisfiesExpression) { matchᴛ1 = true;
        p.jsErrorAtRange(node.Type().Loc, diagnostics.Type_satisfaction_expressions_can_only_be_used_in_TypeScript_files);
    }

    p.checkJSDecoratorSyntax(Ꮡnode);
    var exprᴛ2 = node.Kind;
    var matchᴛ2 = false;
    if (exprᴛ2 == ast.KindClassDeclaration || exprᴛ2 == ast.KindClassExpression || exprᴛ2 == ast.KindMethodDeclaration || exprᴛ2 == ast.KindConstructor || exprᴛ2 == ast.KindGetAccessor || exprᴛ2 == ast.KindSetAccessor || exprᴛ2 == ast.KindFunctionExpression || exprᴛ2 == ast.KindFunctionDeclaration || exprᴛ2 == ast.KindArrowFunction) { matchᴛ2 = true;
        {
            var list = node.TypeParameterList(); if (list != nil && core.Some(list.Nodes, (ж<ast.Node> n) => (ast.NodeFlags)(n.Flags & ast.NodeFlagsReparsed) == 0)) {
                p.jsErrorAtRange(list.Loc, diagnostics.Type_parameter_declarations_can_only_be_used_in_TypeScript_files);
            }
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ2 && (exprᴛ2 == ast.KindVariableStatement || exprᴛ2 == ast.KindPropertyDeclaration)) { matchᴛ2 = true;
        foreach (var (_, modifier) in range(node.ModifierNodes())) {
            if ((ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0 && modifier.Kind != ast.KindDecorator && (ast.ModifierFlags)(ast.ModifierToFlag(modifier.Kind) & ast.ModifierFlagsJavaScript) == 0) {
                p.jsErrorAtRange(modifier.Loc, diagnostics.The_0_modifier_can_only_be_used_in_TypeScript_files, scanner.TokenToString(modifier.Kind));
            }
        }
    }
    else if (exprᴛ2 == ast.KindParameter) {
        if (core.Some<ж<ast.Node>>(node.ModifierNodes(), ast.IsModifier)) {
            p.jsErrorAtRange(node.Modifiers().Loc, diagnostics.Parameter_modifiers_can_only_be_used_in_TypeScript_files);
        }
    }
    else if (exprᴛ2 == ast.KindCallExpression || exprᴛ2 == ast.KindNewExpression || exprᴛ2 == ast.KindExpressionWithTypeArguments || exprᴛ2 == ast.KindJsxSelfClosingElement || exprᴛ2 == ast.KindJsxOpeningElement || exprᴛ2 == ast.KindTaggedTemplateExpression) { matchᴛ2 = true;
        {
            var list = node.TypeArgumentList(); if (list != nil && core.Some(list.Nodes, (ж<ast.Node> n) => (ast.NodeFlags)(n.Flags & ast.NodeFlagsReparsed) == 0)) {
                p.jsErrorAtRange(list.Loc, diagnostics.Type_arguments_can_only_be_used_in_TypeScript_files);
            }
        }
    }

    return Ꮡnode;
}

} // end parserᴛParser
