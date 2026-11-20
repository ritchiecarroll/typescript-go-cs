namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using cmp = cmp_package;
using fmt = fmt_package;
using regexp = regexp_package;
using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using io = io_package;
using static semver_package;

partial class semver_package {

internal static ж<regexp.Regexp> versionRegexp = regexp.MustCompile(@"(?i)^(0|[1-9]\d*)(?:\.(0|[1-9]\d*)(?:\.(0|[1-9]\d*)(?:-([a-z0-9-.]+))?(?:\+([a-z0-9-.]+))?)?)?$");

internal static ж<regexp.Regexp> prereleaseRegexp = regexp.MustCompile(@"(?i)^(?:0|[1-9]\d*|[a-z-][a-z0-9-]*)(?:\.(?:0|[1-9]\d*|[a-zA-Z-][a-zA-Z0-9-]*))*$");
internal static ж<regexp.Regexp> prereleasePartRegexp = regexp.MustCompile(@"(?i)^(?:0|[1-9]\d*|[a-z-][a-z0-9-]*)$");

internal static ж<regexp.Regexp> buildRegExp = regexp.MustCompile(@"(?i)^[a-z0-9-]+(?:\.[a-z0-9-]+)*$");
internal static ж<regexp.Regexp> buildPartRegExp = regexp.MustCompile(@"(?i)^[a-z0-9-]+$");

internal static ж<regexp.Regexp> numericIdentifierRegExp = regexp.MustCompile(@"^(?:0|[1-9]\d*)$");

partial struct Version {
    internal uint32 major;
    internal uint32 minor;
    internal uint32 patch;
    internal slice<@string> prerelease;
    internal slice<@string> build;
}

internal static Version versionZero = new Version(
    prerelease: ["0"]
);

} partial class semverᴛVersion {

internal static Version incrementMajor(this ref Version v) {
    return new Version(
        major: v.major + 1
    );
}

internal static Version incrementMinor(this ref Version v) {
    return new Version(
        major: v.major,
        minor: v.minor + 1
    );
}

internal static Version incrementPatch(this ref Version v) {
    return new Version(
        major: v.major,
        minor: v.minor,
        patch: v.patch + 1
    );
}

} partial class semver_package {

internal static UntypedInt comparisonLessThan => -1;
internal static UntypedInt comparisonEqualTo => 0;
internal static UntypedInt comparisonGreaterThan => 1;

} partial class semverᴛVersion {

public static nint Compare(this ж<Version> Ꮡa, ж<Version> Ꮡb) {
    ref var a = ref Ꮡa.DerefOrNull();
    ref var b = ref Ꮡb.DerefOrNull();

    switch (ᐧ) {
    case {} when Ꮡa == Ꮡb: {
        return comparisonEqualTo;
    }
    case {} when Ꮡa == nil: {
        return comparisonLessThan;
    }
    case {} when Ꮡb == nil: {
        return comparisonGreaterThan;
    }}

    nint r = cmp.Compare(a.major, b.major);
    if (r != 0) {
        return r;
    }

    r = cmp.Compare(a.minor, b.minor);
    if (r != 0) {
        return r;
    }

    r = cmp.Compare(a.patch, b.patch);
    if (r != 0) {
        return r;
    }

    return comparePreReleaseIdentifiers(a.prerelease, b.prerelease);
}

} partial class semver_package {

internal static nint comparePreReleaseIdentifiers(slice<@string> left, slice<@string> right) {
    if (len(left) == 0) {
        if (len(right) == 0) {
            return comparisonEqualTo;
        }
        return comparisonGreaterThan;
    } else if (len(right) == 0) {
        return comparisonLessThan;
    }

    return slices.CompareFunc<slice<@string>, slice<@string>, @string, @string>(left, right, comparePreReleaseIdentifier);
}

internal static nint comparePreReleaseIdentifier(@string left, @string right) {
    nint compareResult = strings.Compare(left, right);
    if (compareResult == 0) {
        return compareResult;
    }

    var leftIsNumeric = numericIdentifierRegExp.MatchString(left);
    var rightIsNumeric = numericIdentifierRegExp.MatchString(right);

    if (leftIsNumeric || rightIsNumeric) {
        if (!rightIsNumeric) {
            return comparisonLessThan;
        }
        if (!leftIsNumeric) {
            return comparisonGreaterThan;
        }

        var (leftAsNumber, leftErr) = getUintComponent(left);
        var (rightAsNumber, rightErr) = getUintComponent(right);
        if (leftErr != nil || rightErr != nil) {
            nint leftLen = len(left);
            nint rightLen = len(right);
            nint lenCompare = cmp.Compare(leftLen, rightLen);
            if (lenCompare == 0) {
                return compareResult;
            } else {
                return lenCompare;
            }
        }
        return cmp.Compare(leftAsNumber, rightAsNumber);
    }

    return compareResult;
}

} partial class semverᴛVersion {

public static @string String(this ref Version v) {
    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    fmt.Fprintf(new strings_BuilderжWriter(Ꮡsb), "%d.%d.%d"u8, v.major, v.minor, v.patch);
    if (len(v.prerelease) > 0) {
        fmt.Fprintf(new strings_BuilderжWriter(Ꮡsb), "-%s"u8, strings.Join(v.prerelease, "."));
    }
    if (len(v.build) > 0) {
        fmt.Fprintf(new strings_BuilderжWriter(Ꮡsb), "+%s"u8, strings.Join(v.build, "."));
    }
    return sb.String();
}

} partial class semver_package {

partial struct SemverParseError {
    internal @string origInput;
}

} partial class semverᴛSemverParseError {

public static @string Error(this ref SemverParseError e) {
    return fmt.Sprintf("Could not parse version string from %q"u8, e.origInput);
}

} partial class semver_package {

public static (Version, error) TryParseVersion(@string text) {
    Version result = default;

    var match = versionRegexp.FindStringSubmatch(text);
    if (match == nil) {
        return (result, new SemverParseErrorжerror(Ꮡ(new SemverParseError(origInput: text))));
    }

    @string majorStr = match[1];
    @string minorStr = match[2];
    @string patchStr = match[3];
    @string prereleaseStr = match[4];
    @string buildStr = match[5];

    error err = default;

    (result.major, err) = getUintComponent(majorStr);
    if (err != nil) {
        return (result, err);
    }

    if (minorStr != ""u8) {
        (result.minor, err) = getUintComponent(minorStr);
        if (err != nil) {
            return (result, err);
        }
    }

    if (patchStr != ""u8) {
        (result.patch, err) = getUintComponent(patchStr);
        if (err != nil) {
            return (result, err);
        }
    }

    if (prereleaseStr != ""u8) {
        if (!prereleaseRegexp.MatchString(prereleaseStr)) {
            return (result, new SemverParseErrorжerror(Ꮡ(new SemverParseError(origInput: text))));
        }

        result.prerelease = strings.Split(prereleaseStr, ".");
    }
    if (buildStr != ""u8) {
        if (!buildRegExp.MatchString(buildStr)) {
            return (result, new SemverParseErrorжerror(Ꮡ(new SemverParseError(origInput: text))));
        }

        result.build = strings.Split(buildStr, ".");
    }

    return (result, default);
}

public static Version MustParse(@string text) {
    var (v, err) = TryParseVersion(text);
    if (err != nil) {
        throw panic(err);
    }
    return v;
}

internal static (uint32, error) getUintComponent(@string text) {
    var (r, err) = strconv.ParseUint(text, 10, 32);
    return ((uint32)r, err);
}

} // end semver_package
