namespace go.github.com.microsoft.TypeScript.tsc.@internal.vfs;

using math = math_package;
using slices = slices_package;
using strings = strings_package;
using utf8 = unicode.utf8_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using time = time_package;
using unicode;
using static vfsmatch_package;

partial class vfsmatch_package {

partial struct Usage /*num:int8*/;

public static Usage UsageFiles => /* iota */ 0;
public static Usage UsageDirectories => 1;
public static Usage UsageExclude => 2;

public static UntypedInt UnlimitedDepth => /* math.MaxInt */ 9223372036854775807;

public static slice<@string> ReadDirectory(vfs.FS host, @string currentDir, @string path, slice<@string> extensions, slice<@string> excludes, slice<@string> includes, nint depth) {
    return matchFiles(path, extensions, excludes, includes, host.UseCaseSensitiveFileNames(), currentDir, depth, host);
}

public static bool IsImplicitGlob(@string lastPathComponent) {
    return !strings.ContainsAny(lastPathComponent, ".*?");
}

internal static slice<rune> wildcardCharCodes = ['*', '?'];

internal static @string getIncludeBasePath(@string absolute) {
    nint wildcardOffset = strings.IndexAny(absolute, (@string)wildcardCharCodes);
    if (wildcardOffset < 0) {
        if (!tspath.HasExtension(absolute)) {
            return absolute;
        } else {
            return tspath.RemoveTrailingDirectorySeparator(tspath.GetDirectoryPath(absolute));
        }
    }
    return absolute.slice(0, max(strings.LastIndex(absolute.slice(0, wildcardOffset), (@string)(rune)tspath.DirectorySeparator), 0));
}

internal static slice<@string> getBasePaths(@string path, slice<@string> includes, bool useCaseSensitiveFileNames) {
    var includeBasePathsᴷ = new stackAppend<@string>();

    var basePaths = slice<@string>([path]);

    if (len(includes) > 0) {
        ref var comparePathsOptions = ref heap<tspath.ComparePathsOptions>(new tspath.ComparePathsOptions(CurrentDirectory: path, UseCaseSensitiveFileNames: useCaseSensitiveFileNames), out var ᏑcomparePathsOptions);
        var stringComparer = comparePathsOptions.GetComparer();

        var includeBasePaths = slice<@string>([]);
        foreach (var (_, include) in range(includes)) {
            @string absolute = default;
            if (tspath.IsRootedDiskPath(include)) {
                absolute = include;
            } else {
                absolute = tspath.NormalizePath(tspath.CombinePaths(path, include));
            }
            includeBasePaths = append(includeBasePathsᴷ, includeBasePaths, getIncludeBasePath(absolute));
        }

        slices.SortStableFunc(includeBasePaths, stringComparer);

        foreach (var (_, includeBasePath) in range(includeBasePaths)) {
            var comparePathsOptionsʗ1 = comparePathsOptions;
            if (core.Every(basePaths, (@string basepath) => !tspath.ContainsPath(basepath, includeBasePath, comparePathsOptionsʗ1))) {
                basePaths = append(basePaths, includeBasePath);
            }
        }
    }

    return basePaths;
}

partial struct globPattern {
    internal slice<component> components;
    internal bool isExclude;
    internal bool caseSensitive;
    internal bool excludeMinJs;
}

partial struct component {
    internal componentKind kind;
    internal @string literal;
    internal slice<segment> segments;
    internal bool skipPackageFolders;
}

partial struct componentKind /*num:nint*/;

internal static componentKind kindLiteral => /* iota */ 0;
internal static componentKind kindWildcard => 1;
internal static componentKind kindDoubleAsterisk => 2;

partial struct segment {
    internal segmentKind kind;
    internal @string literal;
}

partial struct segmentKind /*num:nint*/;

internal static segmentKind segLiteral => /* iota */ 0;
internal static segmentKind segStar => 1;
internal static segmentKind segQuestion => 2;

internal static (globPattern, bool) compileGlobPattern(@string spec, @string basePath, Usage usage, bool caseSensitive) {
    var partsᴷ = new stackAppend<@string>();

    var parts = tspath.GetNormalizedPathComponents(spec, basePath);

    if (usage != UsageExclude && core.LastOrNil(parts) == "**"u8) {
        return (new globPattern(), false);
    }

    parts[0] = tspath.RemoveTrailingDirectorySeparator(parts[0]);

    if (IsImplicitGlob(core.LastOrNil(parts))) {
        parts = append(partsᴷ, parts, (@string)"**", (@string)"*");
    }

    var p = new globPattern(
        isExclude: usage == UsageExclude,
        caseSensitive: caseSensitive,
        excludeMinJs: usage == UsageFiles,
        components: new slice<component>(0, len(parts))
    );

    foreach (var (_, part) in range(parts)) {
        p.components = append(p.components, parseComponent(part, usage != UsageExclude));
    }
    return (p, true);
}

internal static component parseComponent(@string s, bool isInclude) {
    if (s == "**"u8) {
        return new component(kind: kindDoubleAsterisk);
    }
    if (!strings.ContainsAny(s, "*?")) {
        return new component(kind: kindLiteral, literal: s);
    }
    return new component(
        kind: kindWildcard,
        segments: parseSegments(s),
        skipPackageFolders: isInclude
    );
}

internal static slice<segment> parseSegments(@string s) {
    nint wildcards = 0;
    foreach (var i in range(len(s))) {
        if (s[i] == '*' || s[i] == '?') {
            wildcards++;
        }
    }
    var result = new slice<segment>(0, 2 * wildcards + 1);
    nint start = 0;
    foreach (var i in range(len(s))) {
        switch (s[i]) {
        case (byte)'*' or (byte)'?': {
            if (i > start) {
                result = append(result, new segment(kind: segLiteral, literal: s.slice(start, i)));
            }
            if (s[i] == '*') {
                result = append(result, new segment(kind: segStar));
            } else {
                result = append(result, new segment(kind: segQuestion));
            }
            start = i + 1;
            break;
        }}

    }
    if (start < len(s)) {
        result = append(result, new segment(kind: segLiteral, literal: s.slice(start)));
    }
    return result;
}

} partial class vfsmatchᴛglobPattern {

internal static bool matches(this ref globPattern p, @string path) {
    return p.matchPathParts(path, "", 0, 0, false);
}

internal static bool matchesParts(this ref globPattern p, @string prefix, @string suffix) {
    return p.matchPathParts(prefix, suffix, 0, 0, false);
}

internal static bool matchesPrefixParts(this ref globPattern p, @string prefix, @string suffix) {
    return p.matchPathParts(prefix, suffix, 0, 0, true);
}

internal static bool matchPathParts(this ref globPattern p, @string prefix, @string suffix, nint pathOffset, nint compIdx, bool prefixOnly) {
    for (;;) {
        var (pathPart, nextOffset, ok) = nextPathPartParts(prefix, suffix, pathOffset);
        if (!ok) {
            if (prefixOnly) {
                return true;
            }
            return p.patternSatisfied(compIdx);
        }

        if (compIdx >= len(p.components)) {
            return p.isExclude && !prefixOnly;
        }

        var comp = p.components[compIdx];
        switch (comp.kind) {
        case var ᴛ when ᴛ == kindDoubleAsterisk: {
            if (p.matchPathParts(prefix, suffix, pathOffset, compIdx + 1, prefixOnly)) {
                return true;
            }
            if (!p.isExclude && (isHiddenPath(pathPart) || isPackageFolder(pathPart))) {
                return false;
            }
            pathOffset = nextOffset;
            continue;
            break;
        }
        case var ᴛ when ᴛ == kindLiteral: {
            if (comp.skipPackageFolders && isPackageFolder(pathPart)) {
                throw panic("unreachable: literal components never have skipPackageFolders");
            }
            if (!p.stringsEqual(comp.literal, pathPart)) {
                return false;
            }
            break;
        }
        case var ᴛ when ᴛ == kindWildcard: {
            if (comp.skipPackageFolders && isPackageFolder(pathPart)) {
                return false;
            }
            if (!p.matchWildcard(comp.segments, pathPart)) {
                return false;
            }
            break;
        }}

        pathOffset = nextOffset;
        compIdx++;
    }
}

internal static bool patternSatisfied(this ref globPattern p, nint compIdx) {
    foreach (var (_, c) in range(p.components.slice(compIdx))) {
        if (c.kind != kindDoubleAsterisk) {
            return false;
        }
    }
    return true;
}

} partial class vfsmatch_package {

internal static (@string part, nint nextOffset, bool ok) nextPathPartSingle(@string s, nint offset) {
    if (offset >= len(s)) {
        return ("", offset, false);
    }
    if (offset == 0 && len(s) > 0 && s[0] == '/') {
        return ("", 1, true);
    }
    while (offset < len(s) && s[offset] == '/') {
        offset++;
    }
    if (offset >= len(s)) {
        return ("", offset, false);
    }
    @string rest = s.slice(offset);
    nint idx = strings.IndexByte(rest, (byte)'/'); if (idx >= 0) {
        return (rest.slice(0, idx), offset + idx, true);
    }
    return (rest, len(s), true);
}

internal static (@string part, nint nextOffset, bool ok) nextPathPartParts(@string prefix, @string suffix, nint offset) {
    if (len(suffix) == 0) {
        return nextPathPartSingle(prefix, offset);
    }
    if (len(prefix) == 0) {
        return nextPathPartSingle(suffix, offset);
    }

    nint totalLen = len(prefix) + len(suffix);
    if (offset >= totalLen) {
        return ("", offset, false);
    }

    if (offset == 0 && prefix[0] == '/') {
        return ("", 1, true);
    }

    if (offset < len(prefix)) {
        while (offset < len(prefix) && prefix[offset] == '/') {
            offset++;
        }
        if (offset < len(prefix)) {
            @string rest = prefix.slice(offset);
            nint idx = strings.IndexByte(rest, (byte)'/');
            return (rest.slice(0, idx), offset + idx, true);
        }
    }

    nint sOff = offset - len(prefix);
    if (sOff >= len(suffix)) {
        return ("", offset, false);
    }
    return (suffix.slice(sOff), totalLen, true);
}

} partial class vfsmatchᴛglobPattern {

internal static bool matchWildcard(this ref globPattern p, slice<segment> segs, @string s) {
    if (!p.isExclude && len(segs) > 0 && isHiddenPath(s) && (segs[0].kind == segStar || segs[0].kind == segQuestion)) {
        return false;
    }

    if (len(segs) == 2 && segs[0].kind == segStar && segs[1].kind == segLiteral) {
        @string suffix = segs[1].literal;
        if (len(s) < len(suffix) || !p.stringsEqual(suffix, s.slice(len(s) - len(suffix)))) {
            return false;
        }
        return p.shouldIncludeMinJs(s, segs);
    }

    return p.matchSegments(segs, s) && p.shouldIncludeMinJs(s, segs);
}

internal static bool matchSegments(this ref globPattern p, slice<segment> segs, @string s) {
    nint segIdx = 0;
    nint sIdx = 0;
    nint starSegIdx = -1;
    nint starSIdx = 0;

    while (sIdx < len(s)) {
        if (segIdx < len(segs)) {
            var seg = segs[segIdx];
            switch (seg.kind) {
            case var ᴛ when ᴛ == segLiteral: {
                nint end = sIdx + len(seg.literal);
                if (end <= len(s) && p.stringsEqual(seg.literal, s.slice(sIdx, end))) {
                    sIdx = end;
                    segIdx++;
                    continue;
                }
                break;
            }
            case var ᴛ when ᴛ == segQuestion: {
                if (s[sIdx] != '/') {
                    var (_, size) = utf8.DecodeRuneInString(s.slice(sIdx));
                    sIdx += size;
                    segIdx++;
                    continue;
                }
                break;
            }
            case var ᴛ when ᴛ == segStar: {
                starSegIdx = segIdx;
                starSIdx = sIdx;
                segIdx++;
                continue;
                break;
            }}

        }

        if (starSegIdx >= 0 && starSIdx < len(s) && s[starSIdx] != '/') {
            var (_, size) = utf8.DecodeRuneInString(s.slice(starSIdx));
            starSIdx += size;
            sIdx = starSIdx;
            segIdx = starSegIdx + 1;
            continue;
        }

        return false;
    }

    while (segIdx < len(segs) && segs[segIdx].kind == segStar) {
        segIdx++;
    }
    return segIdx >= len(segs);
}

internal static bool shouldIncludeMinJs(this ref globPattern p, @string filename, slice<segment> segs) {
    if (!p.excludeMinJs) {
        return true;
    }

    if (!p.hasMinJsSuffix(filename)) {
        return true;
    }
    if (p.patternMentionsMinSuffix(segs)) {
        return true;
    }
    return false;
}

internal static bool hasMinJsSuffix(this ref globPattern p, @string filename) {
    if (p.caseSensitive) {
        return strings.HasSuffix(filename, ".min.js");
    }
    @string minJs = ".min.js";
    if (len(filename) < len(minJs)) {
        return false;
    }
    return strings.EqualFold(filename.slice(len(filename) - len(minJs)), minJs);
}

internal static bool patternMentionsMinSuffix(this ref globPattern p, slice<segment> segs) {
    foreach (var (_, seg) in range(segs)) {
        if (seg.kind != segLiteral) {
            continue;
        }
        @string lit = seg.literal;
        if (!p.caseSensitive) {
            lit = strings.ToLower(lit);
        }
        if (strings.Contains(lit, ".min.js") || strings.Contains(lit, ".min.")) {
            return true;
        }
    }
    return false;
}

internal static bool stringsEqual(this ref globPattern p, @string a, @string b) {
    if (p.caseSensitive) {
        return a == b;
    }
    return strings.EqualFold(a, b);
}

} partial class vfsmatch_package {

internal static bool isHiddenPath(@string name) {
    return len(name) > 0 && name[0] == '.';
}

internal static bool isPackageFolder(@string name) {
    switch (len(name)) {
    case var ᴛ when ᴛ == len("node_modules"): {
        return strings.EqualFold(name, "node_modules");
    }
    case var ᴛ when ᴛ == len("jspm_packages"): {
        return strings.EqualFold(name, "jspm_packages");
    }
    case var ᴛ when ᴛ == len("bower_components"): {
        return strings.EqualFold(name, "bower_components");
    }}

    return false;
}

internal static @string ensureTrailingSlash(@string s) {
    if (len(s) > 0 && s[len(s) - 1] != '/') {
        return s + "/"u8;
    }
    return s;
}

partial struct globMatcher {
    internal slice<globPattern> includes;
    internal slice<globPattern> excludes;
    internal bool hadIncludes;
}

internal static ж<globMatcher> newGlobMatcher(slice<@string> includeSpecs, slice<@string> excludeSpecs, @string basePath, bool caseSensitive, Usage usage) {
    var m = Ꮡ(new globMatcher(
        hadIncludes: len(includeSpecs) > 0,
        includes: new slice<globPattern>(0, len(includeSpecs)),
        excludes: new slice<globPattern>(0, len(excludeSpecs))
    ));

    foreach (var (_, spec) in range(includeSpecs)) {
        var (p, ok) = compileGlobPattern(spec, basePath, usage, caseSensitive); if (ok) {
            m.includes = append(m.includes, p);
        }
    }
    foreach (var (_, spec) in range(excludeSpecs)) {
        var (p, ok) = compileGlobPattern(spec, basePath, UsageExclude, caseSensitive); if (ok) {
            m.excludes = append(m.excludes, p);
        }
    }
    return m;
}

} partial class vfsmatchᴛglobMatcher {

internal static (nint, bool) matchesFileParts(this ref globMatcher m, @string prefix, @string suffix) {
    foreach (var i in range(len(m.excludes))) {
        if (m.excludes[i].matchesParts(prefix, suffix)) {
            return (0, false);
        }
    }
    if (len(m.includes) == 0) {
        if (m.hadIncludes) {
            return (0, false);
        }
        return (0, true);
    }
    foreach (var i in range(len(m.includes))) {
        if (m.includes[i].matchesParts(prefix, suffix)) {
            return (i, true);
        }
    }
    return (0, false);
}

internal static bool matchesDirectoryParts(this ref globMatcher m, @string prefix, @string suffix) {
    foreach (var i in range(len(m.excludes))) {
        if (m.excludes[i].matchesParts(prefix, suffix)) {
            return false;
        }
    }
    if (len(m.includes) == 0) {
        return !m.hadIncludes;
    }
    foreach (var i in range(len(m.includes))) {
        if (m.includes[i].matchesPrefixParts(prefix, suffix)) {
            return true;
        }
    }
    return false;
}

} partial class vfsmatch_package {

partial struct globVisitor {
    internal vfs.FS host;
    internal ж<globMatcher> fileMatcher;
    internal ж<globMatcher> directoryMatcher;
    internal slice<@string> extensions;
    internal bool useCaseSensitiveFileNames;
    internal collections.Set<@string> visited;
    internal slice<slice<@string>> results;
}

} partial class vfsmatchᴛglobVisitor {

internal static void visit(this ж<globVisitor> Ꮡv, @string path, @string absolutePath, nint depth, @string resolvedRealPath) {
    ref var v = ref Ꮡv.DerefOrNull();

    @string realPath = default;
    if (resolvedRealPath != ""u8) {
        realPath = resolvedRealPath;
    } else {
        realPath = v.host.Realpath(absolutePath);
    }
    @string canonicalPath = tspath.GetCanonicalFileName(realPath, v.useCaseSensitiveFileNames);
    if (Ꮡv.Ꮡvisited.Has(canonicalPath)) {
        return;
    }
    v.visited.Add(canonicalPath);

    var entries = v.host.GetAccessibleEntries(absolutePath);

    @string pathPrefix = ensureTrailingSlash(path);
    @string absPrefix = ensureTrailingSlash(absolutePath);

    foreach (var (_, @file) in range(entries.Files)) {
        if (len(v.extensions) > 0 && !tspath.FileExtensionIsOneOf(@file, v.extensions)) {
            continue;
        }
        var (idx, ok) = v.fileMatcher.matchesFileParts(absPrefix, @file); if (ok) {
            v.results[idx] = append(v.results[idx], pathPrefix + @file);
        }
    }

    if (depth != UnlimitedDepth) {
        depth--;
        if (depth == 0) {
            return;
        }
    }

    foreach (var (_, dir) in range(entries.Directories)) {
        if (!v.directoryMatcher.matchesDirectoryParts(absPrefix, dir)) {
            continue;
        }
        @string absDir = absPrefix + dir;
        @string childRealPath = default;
        if (entries.Symlinks != nil) {
            var (_, isSymlink) = entries.Symlinks[dir, ꟷ]; if (!isSymlink) {
                childRealPath = tspath.CombinePaths(realPath, dir);
            }
        }
        Ꮡv.visit(pathPrefix + dir, absDir, depth, childRealPath);
    }
}

} partial class vfsmatch_package {

internal static slice<@string> matchFiles(@string path, slice<@string> extensions, slice<@string> excludes, slice<@string> includes, bool useCaseSensitiveFileNames, @string currentDirectory, nint depth, vfs.FS host) {
    path = tspath.NormalizePath(path);
    currentDirectory = tspath.NormalizePath(currentDirectory);
    @string absolutePath = tspath.CombinePaths(currentDirectory, path);

    var fileMatcher = newGlobMatcher(includes, excludes, absolutePath, useCaseSensitiveFileNames, UsageFiles);
    var directoryMatcher = newGlobMatcher(includes, excludes, absolutePath, useCaseSensitiveFileNames, UsageDirectories);

    ref var v = ref heap<globVisitor>(new globVisitor(
        host: host,
        fileMatcher: fileMatcher,
        directoryMatcher: directoryMatcher,
        extensions: extensions,
        useCaseSensitiveFileNames: useCaseSensitiveFileNames,
        results: new slice<slice<@string>>(max(len(fileMatcher.includes), 1))
    ), out var Ꮡv);

    foreach (var (_, basePath) in range(getBasePaths(path, includes, useCaseSensitiveFileNames))) {
        Ꮡv.visit(basePath, tspath.CombinePaths(currentDirectory, basePath), depth, "");
    }

    if (len(v.results) == 1) {
        return v.results[0];
    }
    return core.Flatten(v.results);
}

partial struct SpecMatcher {
    internal slice<globPattern> patterns;
}

} partial class vfsmatchᴛSpecMatcher {

public static bool MatchString(this ref SpecMatcher m, @string path) {
    foreach (var i in range(len(m.patterns))) {
        if (m.patterns[i].matches(path)) {
            return true;
        }
    }
    return false;
}

public static nint MatchIndex(this ref SpecMatcher m, @string path) {
    foreach (var i in range(len(m.patterns))) {
        if (m.patterns[i].matches(path)) {
            return i;
        }
    }
    return -1;
}

} partial class vfsmatch_package {

public static ж<SpecMatcher> NewSpecMatcher(slice<@string> specs, @string basePath, Usage usage, bool useCaseSensitiveFileNames) {
    if (len(specs) == 0) {
        return default;
    }
    var patterns = new slice<globPattern>(0, len(specs));
    foreach (var (_, spec) in range(specs)) {
        var (p, ok) = compileGlobPattern(spec, basePath, usage, useCaseSensitiveFileNames); if (ok) {
            patterns = append(patterns, p);
        }
    }
    if (len(patterns) == 0) {
        return default;
    }
    return Ꮡ(new SpecMatcher(patterns: patterns));
}

} // end vfsmatch_package
