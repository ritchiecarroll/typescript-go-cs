namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using math = math_package;
using strconv = strconv_package;
using strings = strings_package;
using utf16 = go.unicode.utf16_package;
using utf8 = go.unicode.utf8_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.unicode;
using iter = iter_package;
using ꓸꓸꓸany = Span<any>;
using static scanner_package;

partial class scanner_package {

partial struct regularExpressionFlags /*num:int32*/;

internal static regularExpressionFlags regularExpressionFlagsNone => 0;
internal static regularExpressionFlags regularExpressionFlagsHasIndices => /* 1 << 0 */ 1;
internal static regularExpressionFlags regularExpressionFlagsGlobal => /* 1 << 1 */ 2;
internal static regularExpressionFlags regularExpressionFlagsIgnoreCase => /* 1 << 2 */ 4;
internal static regularExpressionFlags regularExpressionFlagsMultiline => /* 1 << 3 */ 8;
internal static regularExpressionFlags regularExpressionFlagsDotAll => /* 1 << 4 */ 16;
internal static regularExpressionFlags regularExpressionFlagsUnicode => /* 1 << 5 */ 32;
internal static regularExpressionFlags regularExpressionFlagsUnicodeSets => /* 1 << 6 */ 64;
internal static regularExpressionFlags regularExpressionFlagsSticky => /* 1 << 7 */ 128;
internal static regularExpressionFlags regularExpressionFlagsAnyUnicodeMode => /* regularExpressionFlagsUnicode | regularExpressionFlagsUnicodeSets */ 96;
internal static regularExpressionFlags regularExpressionFlagsModifiers => /* regularExpressionFlagsIgnoreCase | regularExpressionFlagsMultiline | regularExpressionFlagsDotAll */ 28;

[GoLazy] partial class lazyᴛcharCodeToRegExpFlag { internal static map<rune, regularExpressionFlags> charCodeToRegExpFlag = new map<rune, regularExpressionFlags>{
    ['d'] = regularExpressionFlagsHasIndices,
    ['g'] = regularExpressionFlagsGlobal,
    ['i'] = regularExpressionFlagsIgnoreCase,
    ['m'] = regularExpressionFlagsMultiline,
    ['s'] = regularExpressionFlagsDotAll,
    ['u'] = regularExpressionFlagsUnicode,
    ['v'] = regularExpressionFlagsUnicodeSets,
    ['y'] = regularExpressionFlagsSticky
}; }

[GoLazy] partial class lazyᴛregExpFlagToFirstAvailableLanguageVersion { internal static map<regularExpressionFlags, core.ScriptTarget> regExpFlagToFirstAvailableLanguageVersion = new map<regularExpressionFlags, core.ScriptTarget>{
    [regularExpressionFlagsHasIndices] = core.ScriptTargetES2022,
    [regularExpressionFlagsDotAll] = core.ScriptTargetES2018,
    [regularExpressionFlagsUnicodeSets] = core.ScriptTargetES2024
}; }

} partial class scannerᴛScanner {

internal static void checkRegularExpressionFlagAvailability(this ref Scanner s, regularExpressionFlags flag, nint pos, nint size) {
    var (availableFrom, ok) = regExpFlagToFirstAvailableLanguageVersion[flag, ꟷ]; if (ok && s.languageVersion() < availableFrom) {
        s.errorAt(diagnostics.This_regular_expression_flag_is_only_available_when_targeting_0_or_later, pos, size, strings.ToLower(availableFrom.String()));
    }
}

} partial class scanner_package {

partial struct classSetExpressionType /*num:nint*/;

internal static classSetExpressionType classSetExpressionTypeUnknown => /* iota */ 0;
internal static classSetExpressionType classSetExpressionTypeClassUnion => 1;
internal static classSetExpressionType classSetExpressionTypeClassIntersection => 2;
internal static classSetExpressionType classSetExpressionTypeClassSubtraction => 3;

partial struct groupNameReference {
    internal nint pos;
    internal nint end;
    internal @string name;
}

partial struct decimalEscapeValue {
    internal nint pos;
    internal nint end;
    internal nint value;
}

partial struct regExpParser {
    internal ж<Scanner> scanner;
    internal nint end;
    internal regularExpressionFlags regExpFlags;
    internal bool anyUnicodeMode;
    internal bool unicodeSetsMode;
    internal bool annexB;
    internal bool anyUnicodeModeOrNonAnnexB;
    internal bool namedCaptureGroups;
    internal bool mayContainStrings;
    internal nint numberOfCapturingGroups;
    internal map<@string, bool> groupSpecifiers;
    internal slice<groupNameReference> groupNameReferences;
    internal slice<decimalEscapeValue> decimalEscapes;
    internal slice<map<@string, bool>> namedCapturingGroups;
    internal rune pendingLowSurrogate;
}

} partial class scannerᴛregExpParser {

internal static nint pos(this ref regExpParser p) {
    return p.scanner.pos;
}

internal static void setPos(this ref regExpParser p, nint v) {
    p.scanner.pos = v;
}

internal static void incPos(this ref regExpParser p, nint n) {
    p.scanner.pos += n;
}

internal static rune @char(this ref regExpParser p) {
    return p.scanner.@char();
}

internal static rune charAt(this ref regExpParser p, nint pos) {
    return p.scanner.charAt(pos - p.pos());
}

internal static void error(this ref regExpParser p, ж<diagnostics.Message> msg, nint pos, nint length, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    p.scanner.errorAt(msg, pos, length, args.ꓸꓸꓸ);
}

internal static @string text(this ref regExpParser p) {
    return p.scanner.text;
}

} partial class scanner_package {

internal static nint compareDecimalStrings(@string a, @string b) {
    a = strings.TrimLeft(a, "0");
    b = strings.TrimLeft(b, "0");
    if (a == ""u8) {
        a = "0";
    }
    if (b == ""u8) {
        b = "0";
    }
    if (len(a) != len(b)) {
        if (len(a) < len(b)) {
            return -1;
        }
        return 1;
    }
    return strings.Compare(a, b);
}

} partial class scannerᴛregExpParser {

internal static void scanDisjunction(this ref regExpParser p, bool isInGroup) {
    ref var disjunctionNames = ref heap(new collections.Set<@string>(), out var ᏑdisjunctionNames);
    for (;;) {
        p.namedCapturingGroups = append(p.namedCapturingGroups, new map<@string, bool>());
        p.scanAlternative(isInGroup);
        var alternativeNames = p.namedCapturingGroups[len(p.namedCapturingGroups) - 1];
        p.namedCapturingGroups = p.namedCapturingGroups.slice(0, len(p.namedCapturingGroups) - 1);
        foreach (var (name, _) in alternativeNames) {
            disjunctionNames.Add(name);
        }
        if (p.@char() != '|') {
            break;
        }
        p.incPos(1);
    }
    if (isInGroup && len(p.namedCapturingGroups) > 0) {
        var parentScope = p.namedCapturingGroups[len(p.namedCapturingGroups) - 1];
        foreach (var (name, _) in ᏑdisjunctionNames.Keys()) {
            parentScope[name] = true;
        }
    }
}

internal static void scanAlternative(this ref regExpParser p, bool isInGroup) {
    var isPreviousTermQuantifiable = false;
    while (p.pos() < p.end) {
        nint start = p.pos();
        var ch = p.@char();
        var matchᴛ1 = false;
        if (ch is '^' or '$') { matchᴛ1 = true;
            p.incPos(1);
            isPreviousTermQuantifiable = false;
        }
        else if (ch is '\\') { matchᴛ1 = true;
            p.incPos(1);
            switch (p.@char()) {
            case 'b' or 'B': {
                p.incPos(1);
                isPreviousTermQuantifiable = false;
                break;
            }
            default: {
                p.scanAtomEscape();
                isPreviousTermQuantifiable = true;
                break;
            }}

        }
        else if (ch is '(') { matchᴛ1 = true;
            p.incPos(1);
            if (p.@char() == '?') {
                p.incPos(1);
                switch (p.@char()) {
                case '=' or '!': {
                    p.incPos(1);
                    isPreviousTermQuantifiable = !p.anyUnicodeModeOrNonAnnexB;
                    break;
                }
                case '<': {
                    nint groupNameStart = p.pos();
                    p.incPos(1);
                    switch (p.@char()) {
                    case '=' or '!': {
                        p.incPos(1);
                        isPreviousTermQuantifiable = false;
                        break;
                    }
                    default: {
                        p.scanGroupName(false);
                        p.scanExpectedChar('>');
                        if (p.scanner.languageVersion() < core.ScriptTargetES2018) {
                            p.error(diagnostics.Named_capturing_groups_are_only_available_when_targeting_ES2018_or_later, groupNameStart, p.pos() - groupNameStart);
                        }
                        p.numberOfCapturingGroups++;
                        isPreviousTermQuantifiable = true;
                        break;
                    }}

                    break;
                }
                default: {
                    nint flagsStart = p.pos();
                    var setFlags = p.scanPatternModifiers(regularExpressionFlagsNone);
                    if (p.@char() == '-') {
                        p.incPos(1);
                        p.scanPatternModifiers(setFlags);
                        if (p.pos() == flagsStart + 1) {
                            p.error(diagnostics.Subpattern_flags_must_be_present_when_there_is_a_minus_sign, flagsStart, p.pos() - flagsStart);
                        }
                    }
                    if (p.pos() != flagsStart && p.scanner.languageVersion() < core.ScriptTargetES2025) {
                        p.error(diagnostics.Regular_expression_pattern_modifiers_are_only_available_when_targeting_0_or_later, flagsStart, p.pos() - flagsStart, strings.ToLower(core.ScriptTargetES2025.String()));
                    }
                    p.scanExpectedChar(':');
                    isPreviousTermQuantifiable = true;
                    break;
                }}

            } else {
                p.numberOfCapturingGroups++;
                isPreviousTermQuantifiable = true;
            }
            p.scanDisjunction(true);
            p.scanExpectedChar(')');
        }
        else if (ch is '{') { matchᴛ1 = true;
            p.incPos(1);
            nint digitsStart = p.pos();
            p.scanDigits();
            @string minStr = p.scanner.tokenValue;
            if (!p.anyUnicodeModeOrNonAnnexB && minStr == ""u8) {
                isPreviousTermQuantifiable = true;
                continue;
            }
            if (p.@char() == ',') {
                p.incPos(1);
                p.scanDigits();
                @string maxStr = p.scanner.tokenValue;
                if (minStr == ""u8) {
                    if (maxStr != ""u8 || p.@char() == '}') {
                        p.error(diagnostics.Incomplete_quantifier_Digit_expected, digitsStart, 0);
                    } else {
                        p.error(diagnostics.Unexpected_0_Did_you_mean_to_escape_it_with_backslash, start, 1, (@string)ch);
                        isPreviousTermQuantifiable = true;
                        continue;
                    }
                } else if (maxStr != ""u8) {
                    if (compareDecimalStrings(minStr, maxStr) > 0 && (p.anyUnicodeModeOrNonAnnexB || p.@char() == '}')) {
                        p.error(diagnostics.Numbers_out_of_order_in_quantifier, digitsStart, p.pos() - digitsStart);
                    }
                }
            } else if (minStr == ""u8) {
                if (p.anyUnicodeModeOrNonAnnexB) {
                    p.error(diagnostics.Unexpected_0_Did_you_mean_to_escape_it_with_backslash, start, 1, (@string)ch);
                }
                isPreviousTermQuantifiable = true;
                continue;
            }
            if (p.@char() != '}') {
                if (p.anyUnicodeModeOrNonAnnexB) {
                    p.error(diagnostics.X_0_expected, p.pos(), 0, (@string)"}");
                    p.incPos(-1);
                } else {
                    isPreviousTermQuantifiable = true;
                    continue;
                }
            }
            fallthrough = true;
        }
        if (fallthrough || !matchᴛ1 && (ch is '*' or '+' or '?')) { matchᴛ1 = true;
            p.incPos(1);
            if (p.@char() == '?') {
                p.incPos(1);
            }
            if (!isPreviousTermQuantifiable) {
                p.error(diagnostics.There_is_nothing_available_for_repetition, start, p.pos() - start);
            }
            isPreviousTermQuantifiable = false;
        }
        else if (ch is '.') { matchᴛ1 = true;
            p.incPos(1);
            isPreviousTermQuantifiable = true;
        }
        else if (ch is '[') { matchᴛ1 = true;
            p.incPos(1);
            if (p.unicodeSetsMode) {
                p.scanClassSetExpression();
            } else {
                p.scanClassRanges();
                p.pendingLowSurrogate = 0;
            }
            p.scanExpectedChar(']');
            isPreviousTermQuantifiable = true;
        }
        else if (ch is ')') { matchᴛ1 = true;
            if (isInGroup) {
                return;
            }
            fallthrough = true;
        }
        if (fallthrough || !matchᴛ1 && (ch is ']' or '}')) { matchᴛ1 = true;
            if (p.anyUnicodeModeOrNonAnnexB || ch == ')') {
                p.error(diagnostics.Unexpected_0_Did_you_mean_to_escape_it_with_backslash, p.pos(), 1, (@string)ch);
            }
            p.incPos(1);
            isPreviousTermQuantifiable = true;
        }
        else if (ch is '/' or '|') {
            return;
        }
        else if (!matchᴛ1) { /* default: */
            p.scanSourceCharacter();
            isPreviousTermQuantifiable = true;
        }

    }
}

internal static regularExpressionFlags scanPatternModifiers(this ref regExpParser p, regularExpressionFlags currFlags) {
    while (p.pos() < p.end) {
        var (ch, size) = utf8.DecodeRuneInString(p.text().slice(p.pos()));
        if (ch == utf8.RuneError || !IsIdentifierPart(ch)) {
            break;
        }
        var (flag, ok) = charCodeToRegExpFlag[ch, ꟷ];
        if (!ok) {
            p.error(diagnostics.Unknown_regular_expression_flag, p.pos(), size);
        } else if ((regularExpressionFlags)(currFlags & flag) != 0) {
            p.error(diagnostics.Duplicate_regular_expression_flag, p.pos(), size);
        } else if ((regularExpressionFlags)(flag & regularExpressionFlagsModifiers) == 0) {
            p.error(diagnostics.This_regular_expression_flag_cannot_be_toggled_within_a_subpattern, p.pos(), size);
        } else {
            currFlags |= (regularExpressionFlags)(flag);
        }
        p.incPos(size);
    }
    return currFlags;
}

internal static void scanAtomEscape(this ref regExpParser p) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '\\');
    var exprᴛ1 = p.@char();
    var matchᴛ1 = false;
    if (exprᴛ1 is 'k') { matchᴛ1 = true;
        p.incPos(1);
        if (p.@char() == '<') {
            p.incPos(1);
            p.scanGroupName(true);
            p.scanExpectedChar('>');
        } else if (p.anyUnicodeModeOrNonAnnexB || p.namedCaptureGroups) {
            p.error(diagnostics.X_k_must_be_followed_by_a_capturing_group_name_enclosed_in_angle_brackets, p.pos() - 2, 2);
        }
    }
    else if (exprᴛ1 is 'q') { matchᴛ1 = true;
        if (p.unicodeSetsMode) {
            p.incPos(1);
            p.error(diagnostics.X_q_is_only_available_inside_character_class, p.pos() - 2, 2);
            return;
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1) { /* default: */
        if (!p.scanCharacterClassEscape() && !p.scanDecimalEscape()) {
            debug.Assert(p.scanCharacterEscape(true) != ""u8);
        }
    }

}

internal static bool scanDecimalEscape(this ref regExpParser p) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '\\');
    var ch = p.@char();
    if (ch >= '1' && ch <= '9') {
        nint start = p.pos();
        p.scanDigits();
        var (val, err) = strconv.Atoi(p.scanner.tokenValue);
        if (err != nil) {
            val = math.MaxInt;
        }
        p.decimalEscapes = append(p.decimalEscapes, new decimalEscapeValue(pos: start, end: p.pos(), value: val));
        return true;
    }
    return false;
}

internal static @string scanCharacterEscape(this ref regExpParser p, bool atomEscape) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '\\');
    var ch = p.@char();
    switch (ch) {
    case var ᴛ when ᴛ == -1: {
        p.error(diagnostics.Undetermined_character_escape, p.pos() - 1, 1);
        return "\\";
    }
    case 'c': {
        p.incPos(1);
        ch = p.@char();
        if (stringutil.IsASCIILetter(ch)) {
            p.incPos(1);
            return (@string)(ch & 0x1f);
        }
        if (p.anyUnicodeModeOrNonAnnexB) {
            p.error(diagnostics.X_c_must_be_followed_by_an_ASCII_letter, p.pos() - 2, 2);
        } else if (atomEscape) {
            p.incPos(-1);
            return "\\";
        }
        return (@string)ch;
    }
    case '^' or '$' or '/' or '\\' or '.' or '*' or '+' or '?' or '(' or ')' or '[' or ']' or '{' or '}' or '|': {
        p.incPos(1);
        return (@string)ch;
    }
    default: {
        p.incPos(-1);
        var flags = EscapeSequenceScanningFlagsRegularExpression;
        if (p.annexB) {
            flags |= (EscapeSequenceScanningFlags)(EscapeSequenceScanningFlagsAnnexB);
        }
        if (p.anyUnicodeMode) {
            flags |= (EscapeSequenceScanningFlags)(EscapeSequenceScanningFlagsAnyUnicodeMode);
        }
        if (atomEscape) {
            flags |= (EscapeSequenceScanningFlags)(EscapeSequenceScanningFlagsAtomEscape);
        }
        return p.scanner.scanEscapeSequence(flags);
    }}

}

internal static void scanGroupName(this ref regExpParser p, bool isReference) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '<');
    p.scanner.tokenStart = p.pos();
    if (!p.scanner.scanIdentifier(0, identifierVariantRegExpGroupName)) {
        p.error(diagnostics.Expected_a_capturing_group_name, p.pos(), 0);
    } else if (isReference) {
        p.groupNameReferences = append(p.groupNameReferences, new groupNameReference(pos: p.scanner.tokenStart, end: p.pos(), name: p.scanner.tokenValue));
    } else if (p.namedCapturingGroupsContains(p.scanner.tokenValue)) {
        p.error(diagnostics.Named_capturing_groups_with_the_same_name_must_be_mutually_exclusive_to_each_other, p.scanner.tokenStart, p.pos() - p.scanner.tokenStart);
    } else {
        if (p.groupSpecifiers[p.scanner.tokenValue] && p.scanner.languageVersion() >= core.ScriptTargetES2018 && p.scanner.languageVersion() < core.ScriptTargetES2025) {
            p.error(diagnostics.Duplicate_named_capturing_groups_are_only_available_when_targeting_0_or_later, p.scanner.tokenStart, p.pos() - p.scanner.tokenStart, strings.ToLower(core.ScriptTargetES2025.String()));
        }
        if (len(p.namedCapturingGroups) > 0) {
            p.namedCapturingGroups[len(p.namedCapturingGroups) - 1].Set(p.scanner.tokenValue, true);
        }
        p.groupSpecifiers[p.scanner.tokenValue] = true;
    }
}

internal static bool namedCapturingGroupsContains(this ref regExpParser p, @string name) {
    foreach (var (_, group) in range(p.namedCapturingGroups)) {
        if (group[name]) {
            return true;
        }
    }
    return false;
}

internal static bool isClassContentExit(this ref regExpParser p, rune ch) {
    return ch == ']' || p.pos() >= p.end;
}

internal static void scanClassRanges(this ref regExpParser p) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '[');
    p.pendingLowSurrogate = 0;
    if (p.@char() == '^') {
        p.incPos(1);
    }
    while (p.pos() < p.end) {
        var ch = p.@char();
        if (p.isClassContentExit(ch)) {
            return;
        }
        nint minStart = p.pos();
        @string minCharacter = p.scanClassAtom();
        if (p.@char() == '-') {
            p.incPos(1);
            ch = p.@char();
            if (p.isClassContentExit(ch)) {
                return;
            }
            if (minCharacter == ""u8 && p.anyUnicodeModeOrNonAnnexB) {
                p.error(diagnostics.A_character_class_range_must_not_be_bounded_by_another_character_class, minStart, p.pos() - 1 - minStart);
            }
            nint maxStart = p.pos();
            @string maxCharacter = p.scanClassAtom();
            if (maxCharacter == ""u8 && p.anyUnicodeModeOrNonAnnexB) {
                p.error(diagnostics.A_character_class_range_must_not_be_bounded_by_another_character_class, maxStart, p.pos() - maxStart);
                continue;
            }
            if (minCharacter == ""u8) {
                continue;
            }
            var (minCharacterValue, minSize) = stringutil.DecodeJSStringRune(minCharacter);
            var (maxCharacterValue, maxSize) = stringutil.DecodeJSStringRune(maxCharacter);
            if (len(minCharacter) == minSize && len(maxCharacter) == maxSize && minCharacterValue > maxCharacterValue) {
                p.error(diagnostics.Range_out_of_order_in_character_class, minStart, p.pos() - minStart);
            }
        }
    }
}

internal static void scanClassSetExpression(this ref regExpParser p) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '[');
    var isCharacterComplement = false;
    if (p.@char() == '^') {
        p.incPos(1);
        isCharacterComplement = true;
    }
    var expressionMayContainStrings = false;
    var ch = p.@char();
    if (p.isClassContentExit(ch)) {
        return;
    }
    nint start = p.pos();
    @string operand = default;
    @string twoChars = "";
    if (p.pos() + 1 < p.end) {
        twoChars = p.text().slice(p.pos(), p.pos() + 2);
    }
    switch (ᐧ) {
    case {} when twoChars == "--"u8 || twoChars == "&&"u8: {
        p.error(diagnostics.Expected_a_class_set_operand, p.pos(), 0);
        p.mayContainStrings = false;
        break;
    }
    default: {
        operand = p.scanClassSetOperand();
        break;
    }}

    switch (p.@char()) {
    case '-': {
        if (p.pos() + 1 < p.end && p.charAt(p.pos() + 1) == '-') {
            if (isCharacterComplement && p.mayContainStrings) {
                p.error(diagnostics.Anything_that_would_possibly_match_more_than_a_single_character_is_invalid_inside_a_negated_character_class, start, p.pos() - start);
            }
            expressionMayContainStrings = p.mayContainStrings;
            p.scanClassSetSubExpression(classSetExpressionTypeClassSubtraction);
            p.mayContainStrings = !isCharacterComplement && expressionMayContainStrings;
            return;
        }
        break;
    }
    case '&': {
        if (p.pos() + 1 < p.end && p.charAt(p.pos() + 1) == '&') {
            p.scanClassSetSubExpression(classSetExpressionTypeClassIntersection);
            if (isCharacterComplement && p.mayContainStrings) {
                p.error(diagnostics.Anything_that_would_possibly_match_more_than_a_single_character_is_invalid_inside_a_negated_character_class, start, p.pos() - start);
            }
            expressionMayContainStrings = p.mayContainStrings;
            p.mayContainStrings = !isCharacterComplement && expressionMayContainStrings;
            return;
        }
        break;
    }
    default: {
        if (isCharacterComplement && p.mayContainStrings) {
            p.error(diagnostics.Anything_that_would_possibly_match_more_than_a_single_character_is_invalid_inside_a_negated_character_class, start, p.pos() - start);
        }
        expressionMayContainStrings = p.mayContainStrings;
        break;
    }}

    while (p.pos() < p.end) {
        ch = p.@char();
        switch (ch) {
        case '-': {
            p.incPos(1);
            ch = p.@char();
            if (p.isClassContentExit(ch)) {
                p.mayContainStrings = !isCharacterComplement && expressionMayContainStrings;
                return;
            }
            if (ch == '-') {
                p.incPos(1);
                p.error(diagnostics.Operators_must_not_be_mixed_within_a_character_class_Wrap_it_in_a_nested_class_instead, p.pos() - 2, 2);
                start = p.pos() - 2;
                operand = p.text().slice(start, p.pos());
                continue;
            } else {
                if (operand == ""u8) {
                    p.error(diagnostics.A_character_class_range_must_not_be_bounded_by_another_character_class, start, p.pos() - 1 - start);
                }
                nint secondStart = p.pos();
                @string secondOperand = p.scanClassSetOperand();
                if (isCharacterComplement && p.mayContainStrings) {
                    p.error(diagnostics.Anything_that_would_possibly_match_more_than_a_single_character_is_invalid_inside_a_negated_character_class, secondStart, p.pos() - secondStart);
                }
                expressionMayContainStrings = expressionMayContainStrings || p.mayContainStrings;
                if (secondOperand == ""u8) {
                    p.error(diagnostics.A_character_class_range_must_not_be_bounded_by_another_character_class, secondStart, p.pos() - secondStart);
                } else if (operand != ""u8) {
                    var (minCharacterValue, minSize) = stringutil.DecodeJSStringRune(operand);
                    var (maxCharacterValue, maxSize) = stringutil.DecodeJSStringRune(secondOperand);
                    if (len(operand) == minSize && len(secondOperand) == maxSize && minCharacterValue > maxCharacterValue) {
                        p.error(diagnostics.Range_out_of_order_in_character_class, start, p.pos() - start);
                    }
                }
            }
            break;
        }
        case '&': {
            if (p.pos() + 1 < p.end && p.charAt(p.pos() + 1) == '&') {
                start = p.pos();
                p.incPos(2);
                p.error(diagnostics.Operators_must_not_be_mixed_within_a_character_class_Wrap_it_in_a_nested_class_instead, p.pos() - 2, 2);
                if (p.@char() == '&') {
                    p.error(diagnostics.Unexpected_0_Did_you_mean_to_escape_it_with_backslash, p.pos(), 1, (@string)ch);
                    p.incPos(1);
                }
                operand = p.text().slice(start, p.pos());
                continue;
            }
            break;
        }}

        if (p.isClassContentExit(p.@char())) {
            break;
        }
        start = p.pos();
        twoChars = "";
        if (p.pos() + 1 < p.end) {
            twoChars = p.text().slice(p.pos(), p.pos() + 2);
        }
        switch (ᐧ) {
        case {} when twoChars == "--"u8 || twoChars == "&&"u8: {
            p.error(diagnostics.Operators_must_not_be_mixed_within_a_character_class_Wrap_it_in_a_nested_class_instead, p.pos(), 2);
            p.incPos(2);
            operand = p.text().slice(start, p.pos());
            break;
        }
        default: {
            operand = p.scanClassSetOperand();
            if (isCharacterComplement && p.mayContainStrings) {
                p.error(diagnostics.Anything_that_would_possibly_match_more_than_a_single_character_is_invalid_inside_a_negated_character_class, start, p.pos() - start);
            }
            expressionMayContainStrings = expressionMayContainStrings || p.mayContainStrings;
            break;
        }}

    }
    p.mayContainStrings = !isCharacterComplement && expressionMayContainStrings;
}

internal static void scanClassSetSubExpression(this ref regExpParser p, classSetExpressionType expressionType) {
    var expressionMayContainStrings = p.mayContainStrings;
    while (p.pos() < p.end) {
        var ch = p.@char();
        if (p.isClassContentExit(ch)) {
            break;
        }
        switch (ch) {
        case '-': {
            p.incPos(1);
            if (p.@char() == '-') {
                p.incPos(1);
                if (expressionType != classSetExpressionTypeClassSubtraction) {
                    p.error(diagnostics.Operators_must_not_be_mixed_within_a_character_class_Wrap_it_in_a_nested_class_instead, p.pos() - 2, 2);
                }
            } else {
                p.error(diagnostics.Operators_must_not_be_mixed_within_a_character_class_Wrap_it_in_a_nested_class_instead, p.pos() - 1, 1);
            }
            break;
        }
        case '&': {
            p.incPos(1);
            if (p.@char() == '&') {
                p.incPos(1);
                if (expressionType != classSetExpressionTypeClassIntersection) {
                    p.error(diagnostics.Operators_must_not_be_mixed_within_a_character_class_Wrap_it_in_a_nested_class_instead, p.pos() - 2, 2);
                }
                if (p.@char() == '&') {
                    p.error(diagnostics.Unexpected_0_Did_you_mean_to_escape_it_with_backslash, p.pos(), 1, (@string)ch);
                    p.incPos(1);
                }
            } else {
                p.error(diagnostics.Unexpected_0_Did_you_mean_to_escape_it_with_backslash, p.pos() - 1, 1, (@string)ch);
            }
            break;
        }
        default: {
            switch (expressionType) {
            case var ᴛ when ᴛ == classSetExpressionTypeClassSubtraction: {
                p.error(diagnostics.X_0_expected, p.pos(), 0, (@string)"--");
                break;
            }
            case var ᴛ when ᴛ == classSetExpressionTypeClassIntersection: {
                p.error(diagnostics.X_0_expected, p.pos(), 0, (@string)"&&");
                break;
            }}

            break;
        }}

        ch = p.@char();
        if (p.isClassContentExit(ch)) {
            p.error(diagnostics.Expected_a_class_set_operand, p.pos(), 0);
            break;
        }
        p.scanClassSetOperand();
        if (expressionType == classSetExpressionTypeClassIntersection) {
            expressionMayContainStrings = expressionMayContainStrings && p.mayContainStrings;
        }
    }
    p.mayContainStrings = expressionMayContainStrings;
}

internal static @string scanClassSetOperand(this ref regExpParser p) {
    p.mayContainStrings = false;
    var exprᴛ1 = p.@char();
    var matchᴛ1 = false;
    if (exprᴛ1 is '[') { matchᴛ1 = true;
        p.incPos(1);
        p.scanClassSetExpression();
        p.scanExpectedChar(']');
        return "";
    }
    if (exprᴛ1 is '\\') { matchᴛ1 = true;
        p.incPos(1);
        if (p.scanCharacterClassEscape()) {
            return "";
        } else if (p.@char() == 'q') {
            p.incPos(1);
            if (p.@char() == '{') {
                p.incPos(1);
                p.scanClassStringDisjunctionContents();
                p.scanExpectedChar('}');
                return "";
            } else {
                p.error(diagnostics.X_q_must_be_followed_by_string_alternatives_enclosed_in_braces, p.pos() - 2, 2);
                return "q";
            }
        }
        p.incPos(-1);
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1) { /* default: */
        return p.scanClassSetCharacter();
    }
    return default;

}

internal static void scanClassStringDisjunctionContents(this ref regExpParser p) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '{');
    nint characterCount = 0;
    while (p.pos() < p.end) {
        var ch = p.@char();
        switch (ch) {
        case '}': {
            if (characterCount != 1) {
                p.mayContainStrings = true;
            }
            return;
        }
        case '|': {
            if (characterCount != 1) {
                p.mayContainStrings = true;
            }
            p.incPos(1);
            characterCount = 0;
            break;
        }
        default: {
            p.scanClassSetCharacter();
            characterCount++;
            break;
        }}

    }
}

internal static @string scanClassSetCharacter(this ref regExpParser p) {
    var ch = p.@char();
    if (ch == '\\') {
        p.incPos(1);
        var innerCh = p.@char();
        switch (innerCh) {
        case 'b': {
            p.incPos(1);
            return "\b";
        }
        case '&' or '-' or '!' or '#' or '%' or ',' or ':' or ';' or '<' or '=' or '>' or '@' or '`' or '~': {
            p.incPos(1);
            return (@string)innerCh;
        }
        default: {
            return p.scanCharacterEscape(false);
        }}

    } else if (p.pos() + 1 < p.end && ch == p.charAt(p.pos() + 1)) {
        switch (ch) {
        case '&' or '!' or '#' or '%' or '*' or '+' or ',' or '.' or ':' or ';' or '<' or '=' or '>' or '?' or '@' or '`' or '~': {
            p.error(diagnostics.A_character_class_must_not_contain_a_reserved_double_punctuator_Did_you_mean_to_escape_it_with_backslash, p.pos(), 2);
            p.incPos(2);
            return p.text().slice(p.pos() - 2, p.pos());
        }}

    }
    switch (ch) {
    case '/' or '(' or ')' or '[' or ']' or '{' or '}' or '-' or '|': {
        p.error(diagnostics.Unexpected_0_Did_you_mean_to_escape_it_with_backslash, p.pos(), 1, (@string)ch);
        p.incPos(1);
        return (@string)ch;
    }}

    return p.scanSourceCharacter();
}

internal static @string scanClassAtom(this ref regExpParser p) {
    if (p.@char() == '\\') {
        p.incPos(1);
        var ch = p.@char();
        switch (ch) {
        case 'b': {
            p.incPos(1);
            return "\b";
        }
        case '-': {
            p.incPos(1);
            return (@string)ch;
        }
        default: {
            if (p.scanCharacterClassEscape()) {
                return "";
            }
            return p.scanCharacterEscape(false);
        }}

    } else {
        return p.scanSourceCharacter();
    }
}

internal static bool scanCharacterClassEscape(this ref regExpParser p) {
    debug.Assert(p.pos() > 0 && p.text()[p.pos() - 1] == '\\');
    var isCharacterComplement = false;
    nint start = p.pos() - 1;
    var ch = p.@char();
    var matchᴛ1 = false;
    if (ch is 'd' or 'D' or 's' or 'S' or 'w' or 'W') { matchᴛ1 = true;
        p.incPos(1);
        return true;
    }
    if (ch is 'P') {
        isCharacterComplement = true;
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && ch is 'p') { matchᴛ1 = true;
        p.incPos(1);
        if (p.@char() == '{') {
            p.incPos(1);
            nint propertyNameOrValueStart = p.pos();
            @string propertyNameOrValue = p.scanWordCharacters();
            if (p.@char() == '=') {
                @string propertyName = nonBinaryUnicodeProperties[propertyNameOrValue];
                if (p.pos() == propertyNameOrValueStart) {
                    p.error(diagnostics.Expected_a_Unicode_property_name, p.pos(), 0);
                } else if (propertyName == ""u8) {
                    p.error(diagnostics.Unknown_Unicode_property_name, propertyNameOrValueStart, p.pos() - propertyNameOrValueStart);
                    @string suggestion = p.getSpellingSuggestionForUnicodePropertyName(propertyNameOrValue);
                    if (suggestion != ""u8) {
                        p.error(diagnostics.Did_you_mean_0, propertyNameOrValueStart, p.pos() - propertyNameOrValueStart, suggestion);
                    }
                }
                p.incPos(1);
                nint propertyValueStart = p.pos();
                @string propertyValue = p.scanWordCharacters();
                if (p.pos() == propertyValueStart) {
                    p.error(diagnostics.Expected_a_Unicode_property_value, p.pos(), 0);
                } else if (propertyName != ""u8) {
                    var values = valuesOfNonBinaryUnicodeProperties[propertyName];
                    if (values != nil && !values.Has(propertyValue)) {
                        p.error(diagnostics.Unknown_Unicode_property_value, propertyValueStart, p.pos() - propertyValueStart);
                        @string suggestion = p.getSpellingSuggestionForUnicodePropertyValue(propertyName, propertyValue);
                        if (suggestion != ""u8) {
                            p.error(diagnostics.Did_you_mean_0, propertyValueStart, p.pos() - propertyValueStart, suggestion);
                        }
                    }
                }
            } else {
                if (p.pos() == propertyNameOrValueStart) {
                    p.error(diagnostics.Expected_a_Unicode_property_name_or_value, p.pos(), 0);
                } else if (binaryUnicodePropertiesOfStrings.Has(propertyNameOrValue)) {
                    if (!p.unicodeSetsMode) {
                        p.error(diagnostics.Any_Unicode_property_that_would_possibly_match_more_than_a_single_character_is_only_available_when_the_Unicode_Sets_v_flag_is_set, propertyNameOrValueStart, p.pos() - propertyNameOrValueStart);
                    } else if (isCharacterComplement) {
                        p.error(diagnostics.Anything_that_would_possibly_match_more_than_a_single_character_is_invalid_inside_a_negated_character_class, propertyNameOrValueStart, p.pos() - propertyNameOrValueStart);
                    } else {
                        p.mayContainStrings = true;
                    }
                } else if (!valuesOfNonBinaryUnicodeProperties["General_Category"].Has(propertyNameOrValue) && !binaryUnicodeProperties.Has(propertyNameOrValue)) {
                    p.error(diagnostics.Unknown_Unicode_property_name_or_value, propertyNameOrValueStart, p.pos() - propertyNameOrValueStart);
                    @string suggestion = p.getSpellingSuggestionForUnicodePropertyNameOrValue(propertyNameOrValue);
                    if (suggestion != ""u8) {
                        p.error(diagnostics.Did_you_mean_0, propertyNameOrValueStart, p.pos() - propertyNameOrValueStart, suggestion);
                    }
                }
            }
            p.scanExpectedChar('}');
            if (!p.anyUnicodeMode) {
                p.error(diagnostics.Unicode_property_value_expressions_are_only_available_when_the_Unicode_u_flag_or_the_Unicode_Sets_v_flag_is_set, start, p.pos() - start);
            }
        } else if (p.anyUnicodeModeOrNonAnnexB) {
            p.error(diagnostics.X_0_must_be_followed_by_a_Unicode_property_value_expression_enclosed_in_braces, p.pos() - 2, 2, (@string)ch);
        } else {
            p.incPos(-1);
            return false;
        }
        return true;
    }

    return false;
}

internal static @string getSpellingSuggestionForUnicodePropertyName(this ref regExpParser p, @string name) {
    return core.GetSpellingSuggestionForStrings(name, maps.Keys<map<@string, @string>, @string, @string>(nonBinaryUnicodeProperties));
}

internal static @string getSpellingSuggestionForUnicodePropertyValue(this ref regExpParser p, @string propertyName, @string value) {
    var values = valuesOfNonBinaryUnicodeProperties[propertyName];
    if (values == nil) {
        return "";
    }
    return core.GetSpellingSuggestionForStrings(value, maps.Keys<map<@string, EmptyStruct>, @string, EmptyStruct>(values.Keys()));
}

internal static @string getSpellingSuggestionForUnicodePropertyNameOrValue(this ref regExpParser p, @string name) {
    return core.GetSpellingSuggestionForStrings(name, core.ConcatenateSeq<@string>(
        maps.Keys<map<@string, EmptyStruct>, @string, EmptyStruct>(valuesOfNonBinaryUnicodeProperties["General_Category"].Keys()),
        maps.Keys<map<@string, EmptyStruct>, @string, EmptyStruct>(binaryUnicodeProperties.Keys()),
        maps.Keys<map<@string, EmptyStruct>, @string, EmptyStruct>(binaryUnicodePropertiesOfStrings.Keys())));
}

internal static @string scanWordCharacters(this ref regExpParser p) {
    nint start = p.pos();
    while (p.pos() < p.end) {
        var ch = p.@char();
        if (!isWordCharacter(ch)) {
            break;
        }
        p.incPos(1);
    }
    return p.text().slice(start, p.pos());
}

internal static @string scanSourceCharacter(this ref regExpParser p) {
    if (p.pos() >= p.end) {
        return "";
    }
    if (!p.anyUnicodeMode) {
        if (p.pendingLowSurrogate != 0) {
            var (_, sizeΔ1) = utf8.DecodeRuneInString(p.text().slice(p.pos()));
            p.incPos(sizeΔ1);
            var low = p.pendingLowSurrogate;
            p.pendingLowSurrogate = 0;
            return stringutil.EncodeJSStringRune(low);
        }
        var (chΔ1, sizeΔ2) = utf8.DecodeRuneInString(p.text().slice(p.pos()));
        if (chΔ1 == utf8.RuneError || sizeΔ2 == 0) {
            p.incPos(1);
            return (@string)p.text()[p.pos() - 1];
        }
        if (utf16.RuneLen(chΔ1) == 2) {
            var (high, low) = stringutil.CodePointToSurrogatePair(chΔ1);
            p.pendingLowSurrogate = low;
            return stringutil.EncodeJSStringRune(high);
        }
        p.incPos(sizeΔ2);
        return (@string)chΔ1;
    }
    var (ch, size) = utf8.DecodeRuneInString(p.text().slice(p.pos()));
    if (size == 0) {
        return "";
    }
    if (ch == utf8.RuneError) {
        p.incPos(size);
        return "";
    }
    p.incPos(size);
    return (@string)ch;
}

internal static void scanExpectedChar(this ref regExpParser p, rune ch) {
    if (p.@char() == ch) {
        p.incPos(1);
    } else {
        p.error(diagnostics.X_0_expected, p.pos(), 0, (@string)ch);
    }
}

internal static void scanDigits(this ref regExpParser p) {
    nint start = p.pos();
    while (p.pos() < p.end && stringutil.IsDigit(p.@char())) {
        p.incPos(1);
    }
    p.scanner.tokenValue = p.text().slice(start, p.pos());
}

internal static void run(this ref regExpParser p) {
    p.anyUnicodeModeOrNonAnnexB = p.anyUnicodeMode || !p.annexB;

    p.scanDisjunction(false);

    foreach (var (_, reference) in range(p.groupNameReferences)) {
        if (!p.groupSpecifiers[reference.name]) {
            p.error(diagnostics.There_is_no_capturing_group_named_0_in_this_regular_expression, reference.pos, reference.end - reference.pos, reference.name);
            if (len(p.groupSpecifiers) > 0) {
                @string suggestion = core.GetSpellingSuggestionForStrings(reference.name, maps.Keys<map<@string, bool>, @string, bool>(p.groupSpecifiers));
                if (suggestion != ""u8) {
                    p.error(diagnostics.Did_you_mean_0, reference.pos, reference.end - reference.pos, suggestion);
                }
            }
        }
    }
    foreach (var (_, escape) in range(p.decimalEscapes)) {
        if (escape.value > p.numberOfCapturingGroups) {
            if (p.numberOfCapturingGroups > 0) {
                p.error(diagnostics.This_backreference_refers_to_a_group_that_does_not_exist_There_are_only_0_capturing_groups_in_this_regular_expression, escape.pos, escape.end - escape.pos, p.numberOfCapturingGroups);
            } else {
                p.error(diagnostics.This_backreference_refers_to_a_group_that_does_not_exist_There_are_no_capturing_groups_in_this_regular_expression, escape.pos, escape.end - escape.pos);
            }
        }
    }
}

} // end scannerᴛregExpParser
