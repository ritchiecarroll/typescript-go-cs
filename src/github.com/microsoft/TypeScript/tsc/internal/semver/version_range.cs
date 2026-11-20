namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using regexp = regexp_package;
using strings = strings_package;
using static semver_package;

partial class semver_package {

internal static ж<regexp.Regexp> logicalOrRegExp = regexp.MustCompile(@"\|\|");
internal static ж<regexp.Regexp> whitespaceRegExp = regexp.MustCompile(@"\s+");

internal static ж<regexp.Regexp> partialRegExp = regexp.MustCompile(@"(?i)^([x*0]|[1-9]\d*)(?:\.([x*0]|[1-9]\d*)(?:\.([x*0]|[1-9]\d*)(?:-([a-z0-9-.]+))?(?:\+([a-z0-9-.]+))?)?)?$");

internal static ж<regexp.Regexp> hyphenRegExp = regexp.MustCompile(@"(?i)^\s*([a-z0-9-+.*]+)\s+-\s+([a-z0-9-+.*]+)\s*$");

internal static ж<regexp.Regexp> rangeRegExp = regexp.MustCompile(@"(?i)^([~^<>=]|<=|>=)?\s*([a-z0-9-+.*]+)$");

partial struct VersionRange {
    internal slice<slice<versionComparator>> alternatives;
}

partial struct versionComparator {
    internal comparatorOperator @operator;
    internal Version operand;
}

partial struct comparatorOperator /*@string*/;

internal static readonly comparatorOperator rangeLessThan = "<"u8;
internal static readonly comparatorOperator rangeLessThanEqual = "<="u8;
internal static readonly comparatorOperator rangeEqual = "="u8;
internal static readonly comparatorOperator rangeGreaterThanEqual = ">="u8;
internal static readonly comparatorOperator rangeGreaterThan = ">"u8;

} partial class semverᴛVersionRange {

public static @string String(this ref VersionRange v) {
    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    formatDisjunction(Ꮡsb, v.alternatives);
    return sb.String();
}

} partial class semver_package {

internal static void formatDisjunction(ж<strings.Builder> Ꮡsb, slice<slice<versionComparator>> alternatives) {
    ref var sb = ref Ꮡsb.DerefOrNull();

    nint origLen = sb.Len();

    foreach (var (i, alternative) in range(alternatives)) {
        if (i > 0) {
            Ꮡsb.WriteString(" || ");
        }
        formatAlternative(Ꮡsb, alternative);
    }

    if (sb.Len() == origLen) {
        Ꮡsb.WriteString("*");
    }
}

internal static void formatAlternative(ж<strings.Builder> sb, slice<versionComparator> comparators) {
    foreach (var (i, comparator) in range(comparators)) {
        if (i > 0) {
            sb.WriteByte((byte)' ');
        }
        formatComparator(sb, comparator);
    }
}

internal static void formatComparator(ж<strings.Builder> sb, versionComparator comparator) {
    sb.WriteString((@string)comparator.@operator);
    sb.WriteString(comparator.operand.String());
}

} partial class semverᴛVersionRange {

public static bool Test(this ref VersionRange v, ж<Version> version) {
    return testDisjunction(v.alternatives, version);
}

} partial class semver_package {

internal static bool testDisjunction(slice<slice<versionComparator>> alternatives, ж<Version> version) {
    if (len(alternatives) == 0) {
        return true;
    }

    foreach (var (_, alternative) in range(alternatives)) {
        if (testAlternative(alternative, version)) {
            return true;
        }
    }

    return false;
}

internal static bool testAlternative(slice<versionComparator> alternative, ж<Version> version) {
    foreach (var (_, comparator) in range(alternative)) {
        if (!testComparator(comparator, version)) {
            return false;
        }
    }
    return true;
}

internal static bool testComparator(versionComparator comparatorʗp, ж<Version> version) {
    ref var comparator = ref heap(comparatorʗp, out var Ꮡcomparator);

    nint cmp = version.Compare(Ꮡcomparator.Ꮡoperand);
    var exprᴛ1 = comparator.@operator;
    switch (ᐧ) {
    case {} when exprᴛ1 == rangeLessThan: {
        return cmp < 0;
    }
    case {} when exprᴛ1 == rangeLessThanEqual: {
        return cmp <= 0;
    }
    case {} when exprᴛ1 == rangeEqual: {
        return cmp == 0;
    }
    case {} when exprᴛ1 == rangeGreaterThanEqual: {
        return cmp >= 0;
    }
    case {} when exprᴛ1 == rangeGreaterThan: {
        return cmp > 0;
    }
    default: {
        throw panic("Unexpected operator: "u8 + comparator.@operator);
        break;
    }}

}

public static (VersionRange, bool) TryParseVersionRange(@string text) {
    var (alternatives, ok) = parseAlternatives(text);
    return (new VersionRange(alternatives: alternatives), ok);
}

internal static (slice<slice<versionComparator>>, bool) parseAlternatives(@string text) {
    var alternativesᴷ = new stackAppend<slice<versionComparator>>(moves: true);

    slice<slice<versionComparator>> alternatives = default;

    text = strings.TrimSpace(text);
    var ranges = logicalOrRegExp.Split(text, -1);
    foreach (var (_, vᴛ1) in range(ranges)) {
        var r = vᴛ1;

        r = strings.TrimSpace(r);
        if (r == ""u8) {
            continue;
        }

        slice<versionComparator> comparators = default;

        var hyphenMatch = hyphenRegExp.FindStringSubmatch(r); if (hyphenMatch != nil) {
            var (parsedComparators, ok) = parseHyphen(hyphenMatch[1], hyphenMatch[2]); if (ok) {
                comparators = appendꓸꓸꓸ(comparators, parsedComparators);
            } else {
                return (default, false);
            }
        } else {
            foreach (var (_, simple) in range(whitespaceRegExp.Split(r, -1))) {
                var match = rangeRegExp.FindStringSubmatch(strings.TrimSpace(simple));
                if (match == nil) {
                    return (default, false);
                }

                var (parsedComparators, ok) = parseComparator(match[1], match[2]); if (ok) {
                    comparators = appendꓸꓸꓸ(comparators, parsedComparators);
                } else {
                    return (default, false);
                }
            }
        }

        alternatives = append(alternativesᴷ, alternatives, comparators);
    }

    alternatives = move2heap(alternativesᴷ, alternatives);
    return (alternatives, true);
}

internal static (slice<versionComparator>, bool) parseHyphen(@string left, @string right) {
    var (leftResult, leftOk) = parsePartial(left);
    if (!leftOk) {
        return (default, false);
    }

    var (rightResult, rightOk) = parsePartial(right);
    if (!rightOk) {
        return (default, false);
    }

    slice<versionComparator> comparators = default;
    if (!isWildcard(leftResult.majorStr)) {
        comparators = append(comparators, new versionComparator(
            @operator: rangeGreaterThanEqual,
            operand: leftResult.version
        ));
    }

    if (!isWildcard(rightResult.majorStr)) {
        comparatorOperator @operator = default;
        var operand = rightResult.version;

        switch (ᐧ) {
        case {} when isWildcard(rightResult.minorStr): {
            operand = operand.incrementMajor();
            @operator = rangeLessThan;
            break;
        }
        case {} when isWildcard(rightResult.patchStr): {
            operand = operand.incrementMinor();
            @operator = rangeLessThan;
            break;
        }
        default: {
            @operator = rangeLessThanEqual;
            break;
        }}

        comparators = append(comparators, new versionComparator(
            @operator: @operator,
            operand: operand
        ));
    }

    return (comparators, true);
}

partial struct partialVersion {
    internal Version version;
    internal @string majorStr;
    internal @string minorStr;
    internal @string patchStr;
}

internal static (partialVersion, bool) parsePartial(@string text) {
    var match = partialRegExp.FindStringSubmatch(text);
    if (match == nil) {
        return (new partialVersion(), false);
    }

    @string majorStr = match[1];
    @string minorStr = match[2];
    @string patchStr = match[3];
    @string prereleaseStr = match[4];
    @string buildStr = match[5];

    if (minorStr == ""u8) {
        minorStr = "*";
    }
    if (patchStr == ""u8) {
        patchStr = "*";
    }

    uint32 majorNumeric = default;
    uint32 minorNumeric = default;
    uint32 patchNumeric = default;
    error err = default;

    if (isWildcard(majorStr)) {
        majorNumeric = 0;
        minorNumeric = 0;
        patchNumeric = 0;
    } else {
        (majorNumeric, err) = getUintComponent(majorStr);
        if (err != nil) {
            return (new partialVersion(), false);
        }

        if (isWildcard(minorStr)) {
            minorNumeric = 0;
            patchNumeric = 0;
        } else {
            (minorNumeric, err) = getUintComponent(minorStr);
            if (err != nil) {
                return (new partialVersion(), false);
            }

            if (isWildcard(patchStr)) {
                patchNumeric = 0;
            } else {
                (patchNumeric, err) = getUintComponent(patchStr);
                if (err != nil) {
                    return (new partialVersion(), false);
                }
            }
        }
    }

    slice<@string> prerelease = default;
    if (prereleaseStr != ""u8) {
        prerelease = strings.Split(prereleaseStr, ".");
    }

    slice<@string> build = default;
    if (buildStr != ""u8) {
        build = strings.Split(buildStr, ".");
    }

    var result = new partialVersion(
        version: new Version(
            major: majorNumeric,
            minor: minorNumeric,
            patch: patchNumeric,
            prerelease: prerelease,
            build: build
        ),
        majorStr: majorStr,
        minorStr: minorStr,
        patchStr: patchStr
    );

    return (result, true);
}

internal static (slice<versionComparator>, bool) parseComparator(@string op, @string text) {
    comparatorOperator @operator = (comparatorOperator)op;

    var (result, ok) = parsePartial(text);
    if (!ok) {
        return (default, false);
    }

    slice<versionComparator> comparatorsResult = default;

    if (!isWildcard(result.majorStr)) {
        switch (ᐧ) {
        case {} when @operator == (comparatorOperator)("~"u8): {
            var first = new versionComparator(rangeGreaterThanEqual, result.version);

            Version secondVersion = default;
            if (isWildcard(result.minorStr)) {
                secondVersion = result.version.incrementMajor();
            } else {
                secondVersion = result.version.incrementMinor();
            }

            var second = new versionComparator(rangeLessThan, secondVersion);
            comparatorsResult = [first, second];
            break;
        }
        case {} when @operator == (comparatorOperator)("^"u8): {
            var first = new versionComparator(rangeGreaterThanEqual, result.version);

            Version secondVersion = default;
            if (result.version.major > 0 || isWildcard(result.minorStr)) {
                secondVersion = result.version.incrementMajor();
            } else if (result.version.minor > 0 || isWildcard(result.patchStr)) {
                secondVersion = result.version.incrementMinor();
            } else {
                secondVersion = result.version.incrementPatch();
            }
            var second = new versionComparator(rangeLessThan, secondVersion);
            comparatorsResult = [first, second];
            break;
        }
        case {} when @operator == (comparatorOperator)("<"u8) || @operator == (comparatorOperator)(">="u8): {
            var version = result.version;
            if (isWildcard(result.minorStr) || isWildcard(result.patchStr)) {
                version.prerelease = ["0"];
            }
            comparatorsResult = [
                new(@operator, version)
            ];
            break;
        }
        case {} when @operator == (comparatorOperator)("<="u8) || @operator == (comparatorOperator)(">"u8): {
            var version = result.version;
            if (isWildcard(result.minorStr)) {
                if (@operator == rangeLessThanEqual) {
                    @operator = rangeLessThan;
                } else {
                    @operator = rangeGreaterThanEqual;
                }

                version = version.incrementMajor();
                version.prerelease = ["0"];
            } else if (isWildcard(result.patchStr)) {
                if (@operator == rangeLessThanEqual) {
                    @operator = rangeLessThan;
                } else {
                    @operator = rangeGreaterThanEqual;
                }

                version = version.incrementMinor();
                version.prerelease = ["0"];
            }

            comparatorsResult = [
                new(@operator, version)
            ];
            break;
        }
        case {} when @operator == (comparatorOperator)("="u8) || @operator == (comparatorOperator)(""u8): {
            @operator = rangeEqual;

            if (isWildcard(result.minorStr) || isWildcard(result.patchStr)) {
                var originalVersion = result.version;

                var firstVersion = originalVersion;
                firstVersion.prerelease = ["0"];

                Version secondVersion = default;
                if (isWildcard(result.minorStr)) {
                    secondVersion = originalVersion.incrementMajor();
                } else {
                    secondVersion = originalVersion.incrementMinor();
                }
                secondVersion.prerelease = ["0"];

                comparatorsResult = [
                    new(rangeGreaterThanEqual, firstVersion),
                    new(rangeLessThan, secondVersion)
                ];
            } else {
                comparatorsResult = [
                    new(@operator, result.version)
                ];
            }
            break;
        }
        default: {
            throw panic("Unexpected operator: "u8 + @operator);
            break;
        }}

    } else {
        if (@operator == "<"u8 || @operator == ">"u8) {
            comparatorsResult = [
                new(rangeLessThan, versionZero)
            ];
        }
    }

    return (comparatorsResult, true);
}

internal static bool isWildcard(@string text) {
    return text == "*"u8 || text == "x"u8 || text == "X"u8;
}

} // end semver_package
