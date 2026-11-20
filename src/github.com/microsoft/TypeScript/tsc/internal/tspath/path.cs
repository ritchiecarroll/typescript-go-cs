namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using cmp = cmp_package;
using slices = slices_package;
using strings = strings_package;
using unicode = unicode_package;
using utf8 = go.unicode.utf8_package;
using @unsafe = unsafe_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.unicode;
using ꓸꓸꓸstring = Span<@string>;
using tspath = tspath_package;
using static tspath_package;

partial class tspath_package {

partial struct Path /*@string*/;

public static UntypedInt DirectorySeparator => /* '/' */ 47;
internal static readonly @string urlSchemeSeparator = "://"u8;

internal static bool isAnyDirectorySeparator(byte @char) {
    return @char == '/' || @char == '\\';
}

public static bool IsUrl(@string path) {
    return GetEncodedRootLength(path) < 0;
}

public static bool IsRootedDiskPath(@string path) {
    return GetEncodedRootLength(path) > 0;
}

public static bool IsDiskPathRoot(@string path) {
    nint rootLength = GetEncodedRootLength(path);
    return rootLength > 0 && rootLength == len(path);
}

public static bool IsDynamicFileName(@string fileName) {
    return strings.HasPrefix(fileName, "^/");
}

public static bool PathIsAbsolute(@string path) {
    return GetEncodedRootLength(path) != 0;
}

public static bool HasTrailingDirectorySeparator(@string path) {
    return len(path) > 0 && isAnyDirectorySeparator(path[len(path) - 1]);
}

public static @string CombinePaths(@string firstPath, params ꓸꓸꓸstring pathsʗp) {
    var paths = pathsʗp.sslice();

    firstPath = NormalizeSlashes(firstPath);

    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    nint size = len(firstPath) + len(paths);
    foreach (var (_, p) in range(paths)) {
        size += len(p);
    }
    Ꮡb.Grow(size);

    Ꮡb.WriteString(firstPath);

    nint start = 0;
    @string result() => Ꮡb.Value.String().slice(start);
    void setResult(@string value) {
        start = Ꮡb.Value.Len();
        Ꮡb.WriteString(value);
    }

    foreach (var (_, vᴛ1) in range(paths)) {
        var trailingPath = vᴛ1;

        if (trailingPath == ""u8) {
            continue;
        }
        trailingPath = NormalizeSlashes(trailingPath);
        if (result() == ""u8 || GetRootLength(trailingPath) != 0) {
            setResult(trailingPath);
        } else {
            if (!HasTrailingDirectorySeparator(result())) {
                Ꮡb.WriteByte(DirectorySeparator);
            }
            Ꮡb.WriteString(trailingPath);
        }
    }
    return result();
}

public static slice<@string> GetPathComponents(@string path, @string currentDirectory) {
    path = CombinePaths(currentDirectory, path);
    return pathComponents(path, GetRootLength(path));
}

internal static slice<@string> pathComponents(@string path, nint rootLength) {
    @string root = path.slice(0, rootLength);
    var rest = strings.Split(path.slice(rootLength), "/");
    if (len(rest) > 0 && rest[len(rest) - 1] == "") {
        rest = rest.slice(0, len(rest) - 1);
    }
    return appendꓸꓸꓸ(slice<@string>([root]), rest);
}

public static bool IsVolumeCharacter(byte @char) {
    return @char >= 'a' && @char <= 'z' || @char >= 'A' && @char <= 'Z';
}

internal static nint getFileUrlVolumeSeparatorEnd(@string url, nint start) {
    if (len(url) <= start) {
        return -1;
    }
    var ch0 = url[start];
    if (ch0 == ':') {
        return start + 1;
    }
    if (ch0 == '%' && len(url) > start + 2 && url[start + 1] == '3') {
        var ch2 = url[start + 2];
        if (ch2 == 'a' || ch2 == 'A') {
            return start + 3;
        }
    }
    return -1;
}

public static nint GetEncodedRootLength(@string path) {
    nint ln = len(path);
    if (ln == 0) {
        return 0;
    }
    var ch0 = path[0];

    if (ch0 == '/' || ch0 == '\\') {
        if (ln == 1 || path[1] != ch0) {
            return 1;
        }

        nint offset = 2;
        nint p1 = strings.IndexByte(path.slice(offset), ch0);
        if (p1 < 0) {
            return ln;
        }

        return p1 + offset + 1;
    }

    if (IsVolumeCharacter(ch0) && ln > 1 && path[1] == ':') {
        if (ln == 2) {
            return 2;
        }
        var ch2 = path[2];
        if (ch2 == '/' || ch2 == '\\') {
            return 3;
        }
    }

    if (ch0 == '^' && ln > 1 && path[1] == '/') {
        return 2;
    }

    nint schemeEnd = strings.Index(path, urlSchemeSeparator);
    if (schemeEnd != -1) {
        nint authorityStart = schemeEnd + len(urlSchemeSeparator);
        nint authorityLength = strings.Index(path.slice(authorityStart), "/");
        if (authorityLength != -1) {
            nint authorityEnd = authorityStart + authorityLength;

            @string scheme = path.slice(0, schemeEnd);
            @string authority = path.slice(authorityStart, authorityEnd);
            if (scheme == "file"u8 && (authority == ""u8 || authority == "localhost"u8) && (len(path) > authorityEnd + 2) && IsVolumeCharacter(path[authorityEnd + 1])) {
                nint volumeSeparatorEnd = getFileUrlVolumeSeparatorEnd(path, authorityEnd + 2);
                if (volumeSeparatorEnd != -1) {
                    if (volumeSeparatorEnd == len(path)) {
                        return ~volumeSeparatorEnd;
                    }
                    if (path[volumeSeparatorEnd] == '/') {
                        return ~(volumeSeparatorEnd + 1);
                    }
                }
            }
            return ~(authorityEnd + 1);
        }
        return ~ln;
    }

    return 0;
}

public static nint GetRootLength(@string path) {
    nint rootLength = GetEncodedRootLength(path);
    if (rootLength < 0) {
        return ~rootLength;
    }
    return rootLength;
}

public static @string GetDirectoryPath(@string path) {
    path = NormalizeSlashes(path);

    nint rootLength = GetRootLength(path);
    if (rootLength == len(path)) {
        return path;
    }

    path = RemoveTrailingDirectorySeparator(path);
    return path.slice(0, max(rootLength, strings.LastIndex(path, "/")));
}

} partial class tspathᴛPath {

public static Path GetDirectoryPath(this Path p) {
    return (Path)tspath.GetDirectoryPath((@string)p);
}

} partial class tspath_package {

public static @string GetPathFromPathComponents(slice<@string> pathComponents) {
    if (len(pathComponents) == 0) {
        return "";
    }

    @string root = pathComponents[0];
    if (root != ""u8) {
        root = EnsureTrailingDirectorySeparator(root);
    }

    return root + strings.Join(pathComponents[1..], "/");
}

public static @string NormalizeSlashes(@string path) {
    return strings.ReplaceAll(path, "\\", "/");
}

internal static slice<@string> reducePathComponents(slice<@string> components) {
    var reducedᴷ = new stackAppend<@string>(moves: true, capacityUsed: true);

    if (len(components) == 0) {
        return [];
    }
    var reduced = slice<@string>([components[0]]);
    for (nint i = 1; i < len(components); i++) {
        @string component = components[i];
        if (component == ""u8) {
            continue;
        }
        if (component == "."u8) {
            continue;
        }
        if (component == ".."u8) {
            if (len(reduced) > 1) {
                if (reduced[len(reduced) - 1] != "..") {
                    reduced = reduced.slice(0, len(reduced) - 1);
                    continue;
                }
            } else if (reduced[0] != "") {
                continue;
            }
        }
        reduced = append(reducedᴷ, reduced, component);
    }
    reduced = move2heap(reducedᴷ, reduced);
    return reduced;
}

public static @string ResolvePath(@string path, params ꓸꓸꓸstring pathsʗp) {
    var paths = pathsʗp.sslice();

    @string combinedPath = default;
    if (len(paths) > 0) {
        combinedPath = CombinePaths(path, paths.ꓸꓸꓸ);
    } else {
        combinedPath = NormalizeSlashes(path);
    }
    return NormalizePath(combinedPath);
}

public static @string ResolveTripleslashReference(@string moduleName, @string containingFile) {
    @string basePath = GetDirectoryPath(containingFile);
    if (IsRootedDiskPath(moduleName)) {
        return NormalizePath(moduleName);
    }
    return NormalizePath(CombinePaths(basePath, moduleName));
}

public static slice<@string> GetNormalizedPathComponents(@string path, @string currentDirectory) {
    @string combined = CombinePaths(currentDirectory, path);
    return getNormalizedPathComponentsFromCombined(combined);
}

internal static slice<@string> getNormalizedPathComponentsFromCombined(@string path) {
    nint rootLength = GetRootLength(path);
    var components = new slice<@string>(1, 8);
    components[0] = path.slice(0, rootLength);

    for (nint i = rootLength; i < len(path); ) {
        while (i < len(path) && path[i] == '/') {
            i++;
        }
        if (i >= len(path)) {
            break;
        }

        nint start = i;
        while (i < len(path) && path[i] != '/') {
            i++;
        }
        @string component = path.slice(start, i);

        if (component == ""u8 || component == "."u8) {
            continue;
        }
        if (component == ".."u8) {
            if (len(components) > 1) {
                if (components[len(components) - 1] != "..") {
                    components = components.slice(0, len(components) - 1);
                    continue;
                }
            } else if (components[0] != "") {
                continue;
            }
        }

        components = append(components, component);
    }

    return components;
}

public static @string GetNormalizedAbsolutePathWithoutRoot(@string fileName, @string currentDirectory) {
    @string absolutePath = GetNormalizedAbsolutePath(fileName, currentDirectory);
    nint rootLength = GetRootLength(absolutePath);
    return absolutePath.slice(rootLength);
}

public static @string GetNormalizedAbsolutePath(@string fileName, @string currentDirectory) {
    nint rootLength = GetRootLength(fileName);
    if (rootLength == 0 && currentDirectory != ""u8) {
        fileName = CombinePaths(currentDirectory, fileName);
    } else {
        fileName = NormalizeSlashes(fileName);
    }
    rootLength = GetRootLength(fileName);

    var (simpleNormalized, ok) = simpleNormalizePath(fileName); if (ok) {
        nint lengthΔ1 = len(simpleNormalized);
        if (lengthΔ1 > rootLength) {
            return RemoveTrailingDirectorySeparator(simpleNormalized);
        }
        if (lengthΔ1 == rootLength && rootLength != 0) {
            return EnsureTrailingDirectorySeparator(simpleNormalized);
        }
        return simpleNormalized;
    }

    nint length = len(fileName);
    @string root = fileName.slice(0, rootLength);
    bool changed = default;
    @string normalized = default;
    nint segmentStart = default;
    nint index = rootLength;
    nint normalizedUpTo = index;
    var seenNonDotDotSegment = rootLength != 0;
    while (index < length) {
        segmentStart = index;
        var ch = fileName[index];
        while (ch == '/') {
            index++;
            if (index < length) {
                ch = fileName[index];
            } else {
                break;
            }
        }
        if (index > segmentStart) {
            if (!changed) {
                normalized = fileName.slice(0, max(rootLength, segmentStart - 1));
                changed = true;
            }
            if (index == length) {
                break;
            }
            segmentStart = index;
        }
        nint segmentEnd = strings.IndexByte(fileName.slice(index + 1), (byte)'/');
        if (segmentEnd == -1) {
            segmentEnd = length;
        } else {
            segmentEnd += index + 1;
        }
        nint segmentLength = segmentEnd - segmentStart;
        if (segmentLength == 1 && fileName[index] == '.') {
            if (!changed) {
                normalized = fileName.slice(0, normalizedUpTo);
                changed = true;
            }
        } else if (segmentLength == 2 && fileName[index] == '.' && fileName[index + 1] == '.') {
            if (!seenNonDotDotSegment) {
                if (changed) {
                    if (len(normalized) == rootLength) {
                        normalized += ".."u8;
                    } else {
                        normalized += "/.."u8;
                    }
                } else {
                    normalizedUpTo = index + 2;
                }
            } else if (!changed) {
                if (normalizedUpTo - 1 >= 0) {
                    normalized = fileName.slice(0, max(rootLength, strings.LastIndexByte(fileName.slice(0, normalizedUpTo - 1), (byte)'/')));
                } else {
                    normalized = fileName.slice(0, normalizedUpTo);
                }
                changed = true;
                seenNonDotDotSegment = (len(normalized) != rootLength || rootLength != 0) && normalized != ".."u8 && !strings.HasSuffix(normalized, "/..");
            } else {
                nint lastSlash = strings.LastIndexByte(normalized, (byte)'/');
                if (lastSlash != -1) {
                    normalized = normalized.slice(0, max(rootLength, lastSlash));
                } else {
                    normalized = root;
                }
                seenNonDotDotSegment = (len(normalized) != rootLength || rootLength != 0) && normalized != ".."u8 && !strings.HasSuffix(normalized, "/..");
            }
        } else if (changed) {
            if (len(normalized) != rootLength) {
                normalized += "/"u8;
            }
            seenNonDotDotSegment = true;
            normalized += fileName.slice(segmentStart, segmentEnd);
        } else {
            seenNonDotDotSegment = true;
            normalizedUpTo = segmentEnd;
        }
        index = segmentEnd + 1;
    }
    if (changed) {
        return normalized;
    }
    if (length > rootLength) {
        return RemoveTrailingDirectorySeparators(fileName);
    }
    if (length == rootLength) {
        return EnsureTrailingDirectorySeparator(fileName);
    }
    return fileName;
}

internal static (@string, bool) simpleNormalizePath(@string path) {
    if (!hasRelativePathSegment(path)) {
        return (path, true);
    }
    @string simplified = strings.ReplaceAll(path, "/./", "/");
    @string trimmed = strings.TrimPrefix(simplified, "./");
    if (trimmed != path && !hasRelativePathSegment(trimmed) && !(trimmed != simplified && strings.HasPrefix(trimmed, "/"))) {
        path = trimmed;
        return (path, true);
    }
    return ("", false);
}

internal static bool hasRelativePathSegment(@string p) {
    nint n = len(p);
    if (n == 0) {
        return false;
    }

    if (p == "."u8 || p == ".."u8) {
        return true;
    }

    if (p[0] == '.') {
        if (n >= 2 && p[1] == '/') {
            return true;
        }
        if (n >= 3 && p[1] == '.' && p[2] == '/') {
            return true;
        }
    }
    if (p[n - 1] == '.') {
        if (n >= 2 && p[n - 2] == '/') {
            return true;
        }
        if (n >= 3 && p[n - 2] == '.' && p[n - 3] == '/') {
            return true;
        }
    }

    var prevSlash = false;
    nint segLen = 0;
    nint dotCount = 0;

    foreach (var i in range(n)) {
        var c = p[i];
        if (c == '/') {
            if (prevSlash) {
                return true;
            }
            if ((segLen == 1 && dotCount == 1) || (segLen == 2 && dotCount == 2)) {
                return true;
            }
            prevSlash = true;
            segLen = 0;
            dotCount = 0;
            continue;
        }

        if (c == '.') {
            if (dotCount >= 0) {
                dotCount++;
            }
        } else {
            dotCount = -1;
        }
        segLen++;
        prevSlash = false;
    }

    return (segLen == 1 && dotCount == 1) || (segLen == 2 && dotCount == 2);
}

public static @string NormalizePath(@string path) {
    path = NormalizeSlashes(path);
    var (normalizedΔ1, ok) = simpleNormalizePath(path); if (ok) {
        return normalizedΔ1;
    }
    @string normalized = GetNormalizedAbsolutePath(path, "");
    if (normalized != ""u8 && HasTrailingDirectorySeparator(path)) {
        normalized = EnsureTrailingDirectorySeparator(normalized);
    }
    return normalized;
}

public static @string GetCanonicalFileName(@string fileName, bool useCaseSensitiveFileNames) {
    if (useCaseSensitiveFileNames) {
        return fileName;
    }
    return ToFileNameLowerCase(fileName);
}

public static (@string, bool) TrimFilePathPrefix(@string path, @string prefix, bool useCaseSensitiveFileNames) {
    if (useCaseSensitiveFileNames) {
        return strings.CutPrefix(path, prefix);
    }
    @string canonicalPrefix = GetCanonicalFileName(prefix, false);
    if (!strings.HasPrefix(GetCanonicalFileName(path, false), canonicalPrefix)) {
        return (path, false);
    }
    return (trimRuneCount(path, utf8.RuneCountInString(canonicalPrefix)), true);
}

internal static @string trimRuneCount(@string s, nint runeCount) {
    nint i = 0;
    foreach (var _ᴛ1 in range(runeCount)) {
        if (i >= len(s)) {
            break;
        }
        var (_, size) = utf8.DecodeRuneInString(s.slice(i));
        i += size;
    }
    return s.slice(i);
}

public static @string ToFileNameLowerCase(@string fileName) {
    const rune IWithDot = /* '\u0130' */ 304;

    var ascii = true;
    var needsLower = false;
    nint fileNameLen = len(fileName);
    foreach (var i in range(fileNameLen)) {
        var c = fileName[i];
        if (c >= 0x80) {
            ascii = false;
            break;
        }
        if ('A' <= c && c <= 'Z') {
            needsLower = true;
        }
    }
    if (ascii) {
        if (!needsLower) {
            return fileName;
        }
        var b = new slice<byte>(fileNameLen);
        foreach (var i in range(fileNameLen)) {
            var c = fileName[i];
            if ('A' <= c && c <= 'Z') {
                c += (byte)((rune)'a' - (rune)'A');
            }
            b[i] = c;
        }
        return @unsafe.String(Ꮡ(b, 0), len(b));
    }

    return strings.Map((rune r) => {
        if (r == IWithDot) {
            return r;
        }
        return unicode.ToLower(r);
    }, fileName);
}

public static Path ToPath(@string fileName, @string basePath, bool useCaseSensitiveFileNames) {
    @string nonCanonicalizedPath = default;
    if (IsRootedDiskPath(fileName)) {
        nonCanonicalizedPath = NormalizePath(fileName);
    } else {
        nonCanonicalizedPath = GetNormalizedAbsolutePath(fileName, basePath);
    }
    return (Path)GetCanonicalFileName(nonCanonicalizedPath, useCaseSensitiveFileNames);
}

public static @string RemoveTrailingDirectorySeparator(@string path) {
    if (HasTrailingDirectorySeparator(path)) {
        return path.slice(0, len(path) - 1);
    }
    return path;
}

} partial class tspathᴛPath {

public static Path RemoveTrailingDirectorySeparator(this Path p) {
    return (Path)tspath.RemoveTrailingDirectorySeparator((@string)p);
}

} partial class tspath_package {

public static @string RemoveTrailingDirectorySeparators(@string path) {
    while (HasTrailingDirectorySeparator(path)) {
        path = RemoveTrailingDirectorySeparator(path);
    }
    return path;
}

public static @string EnsureTrailingDirectorySeparator(@string path) {
    if (!HasTrailingDirectorySeparator(path)) {
        return path + "/"u8;
    }

    return path;
}

} partial class tspathᴛPath {

public static Path EnsureTrailingDirectorySeparator(this Path p) {
    return (Path)tspath.EnsureTrailingDirectorySeparator((@string)p);
}

} partial class tspath_package {

public static slice<@string> GetPathComponentsRelativeTo(@string from, @string to, ComparePathsOptions options) {
    var fromComponents = reducePathComponents(GetPathComponents(from, options.CurrentDirectory));
    var toComponents = reducePathComponents(GetPathComponents(to, options.CurrentDirectory));

    nint start = 0;
    nint maxCommonComponents = min(len(fromComponents), len(toComponents));
    var stringEqualer = options.getEqualityComparer();
    for (; start < maxCommonComponents; start++) {
        @string fromComponent = fromComponents[start];
        @string toComponent = toComponents[start];
        if (start == 0) {
            if (!stringutil.EquateStringCaseInsensitive(fromComponent, toComponent)) {
                break;
            }
        } else {
            if (!stringEqualer(fromComponent, toComponent)) {
                break;
            }
        }
    }

    if (start == 0) {
        return toComponents;
    }

    nint numDotDotSlashes = len(fromComponents) - start;
    var result = new slice<@string>(1 + numDotDotSlashes + len(toComponents) - start);

    result[0] = "";
    nint i = 1;
    foreach (var _ᴛ1 in range(numDotDotSlashes)) {
        result[i] = "..";
        i++;
    }
    foreach (var (_, component) in range(toComponents.slice(start))) {
        result[i] = component;
        i++;
    }

    return result;
}

public static @string GetRelativePathFromDirectory(@string fromDirectory, @string to, ComparePathsOptions options) {
    if ((GetRootLength(fromDirectory) > 0) != (GetRootLength(to) > 0)) {
        throw panic("paths must either both be absolute or both be relative");
    }
    var pathComponents = GetPathComponentsRelativeTo(fromDirectory, to, options);
    return GetPathFromPathComponents(pathComponents);
}

public static @string GetRelativePathFromFile(@string from, @string to, ComparePathsOptions options) {
    return EnsurePathIsNonModuleName(GetRelativePathFromDirectory(GetDirectoryPath(from), to, options));
}

public static @string ConvertToRelativePath(@string absoluteOrRelativePath, ComparePathsOptions options) {
    if (!IsRootedDiskPath(absoluteOrRelativePath)) {
        return absoluteOrRelativePath;
    }

    return GetRelativePathToDirectoryOrUrl(options.CurrentDirectory, absoluteOrRelativePath, false, options);
}

public static @string GetRelativePathToDirectoryOrUrl(@string directoryPathOrUrl, @string relativeOrAbsolutePath, bool isAbsolutePathAnUrl, ComparePathsOptions options) {
    var pathComponents = GetPathComponentsRelativeTo(
        directoryPathOrUrl,
        relativeOrAbsolutePath,
        options);

    @string firstComponent = pathComponents[0];
    if (isAbsolutePathAnUrl && IsRootedDiskPath(firstComponent)) {
        @string prefix = default;
        if (firstComponent[0] == DirectorySeparator) {
            prefix = "file://";
        } else {
            prefix = "file:///";
        }
        pathComponents[0] = prefix + firstComponent;
    }

    return GetPathFromPathComponents(pathComponents);
}

public static @string GetBaseFileName(@string path) {
    path = NormalizeSlashes(path);

    nint rootLength = GetRootLength(path);
    if (rootLength == len(path)) {
        return "";
    }

    path = RemoveTrailingDirectorySeparator(path);
    return path.slice(max(GetRootLength(path), strings.LastIndex(path, (@string)(rune)DirectorySeparator) + 1));
}

public static @string GetAnyExtensionFromPath(@string path, slice<@string> extensions, bool ignoreCase) {
    if (len(extensions) > 0) {
        return getAnyExtensionFromPathWorker(RemoveTrailingDirectorySeparator(path), extensions, stringutil.GetStringEqualityComparer(ignoreCase));
    }

    @string baseFileName = GetBaseFileName(path);
    nint extensionIndex = strings.LastIndex(baseFileName, ".");
    if (extensionIndex >= 0) {
        return baseFileName.slice(extensionIndex);
    }
    return "";
}

public static @string GetLongestExtensionFromPath(@string path, slice<@string> extensions, bool ignoreCase) {
    path = RemoveTrailingDirectorySeparator(path);
    var comparer = stringutil.GetStringEqualityComparer(ignoreCase);
    @string longest = "";
    foreach (var (_, extension) in range(extensions)) {
        if (len(extension) > len(longest)) {
            @string matched = tryGetExtensionFromPath(path, extension, comparer); if (matched != ""u8) {
                longest = matched;
            }
        }
    }
    return longest;
}

internal static @string getAnyExtensionFromPathWorker(@string path, slice<@string> extensions, Func<@string, @string, bool> stringEqualityComparer) {
    foreach (var (_, extension) in range(extensions)) {
        @string result = tryGetExtensionFromPath(path, extension, stringEqualityComparer);
        if (result != ""u8) {
            return result;
        }
    }
    return "";
}

internal static @string tryGetExtensionFromPath(@string path, @string extension, Func<@string, @string, bool> stringEqualityComparer) {
    if (!strings.HasPrefix(extension, ".")) {
        extension = "."u8 + extension;
    }
    if (len(path) >= len(extension) && path[len(path) - len(extension)] == '.') {
        @string pathExtension = path.slice(len(path) - len(extension));
        if (stringEqualityComparer(pathExtension, extension)) {
            return pathExtension;
        }
    }
    return "";
}

public static bool PathIsRelative(@string path) {
    if (path == "."u8 || path == ".."u8) {
        return true;
    }

    if (len(path) >= 2 && path[0] == '.' && (path[1] == '/' || path[1] == '\\')) {
        return true;
    }

    if (len(path) >= 3 && path[0] == '.' && path[1] == '.' && (path[2] == '/' || path[2] == '\\')) {
        return true;
    }

    return false;
}

public static @string EnsurePathIsNonModuleName(@string path) {
    if (!PathIsAbsolute(path) && !PathIsRelative(path)) {
        return "./"u8 + path;
    }
    return path;
}

public static bool IsExternalModuleNameRelative(@string moduleName) {
    return PathIsRelative(moduleName) || IsRootedDiskPath(moduleName);
}

partial struct ComparePathsOptions {
    public bool UseCaseSensitiveFileNames;
    public @string CurrentDirectory;
}

} partial class tspathᴛComparePathsOptions {

public static Func<@string, @string, nint> GetComparer(this ComparePathsOptions o) {
    return stringutil.GetStringComparer(!o.UseCaseSensitiveFileNames);
}

internal static Func<@string, @string, bool> getEqualityComparer(this ComparePathsOptions o) {
    return stringutil.GetStringEqualityComparer(!o.UseCaseSensitiveFileNames);
}

} partial class tspath_package {

public static nint ComparePaths(@string a, @string b, ComparePathsOptions options) {
    a = CombinePaths(options.CurrentDirectory, a);
    b = CombinePaths(options.CurrentDirectory, b);

    if (a == b) {
        return 0;
    }
    if (a == ""u8) {
        return -1;
    }
    if (b == ""u8) {
        return 1;
    }

    @string aRoot = a.slice(0, GetRootLength(a));
    @string bRoot = b.slice(0, GetRootLength(b));
    stringutilꓸComparison result = stringutil.CompareStringsCaseInsensitive(aRoot, bRoot);
    if (result != 0) {
        return result;
    }

    @string aRest = a.slice(len(aRoot));
    @string bRest = b.slice(len(bRoot));
    if (!hasRelativePathSegment(aRest) && !hasRelativePathSegment(bRest)) {
        return options.GetComparer()(aRest, bRest);
    }

    var aComponents = reducePathComponents(GetPathComponents(a, ""));
    var bComponents = reducePathComponents(GetPathComponents(b, ""));
    nint sharedLength = min(len(aComponents), len(bComponents));
    for (nint i = 1; i < sharedLength; i++) {
        nint resultΔ1 = options.GetComparer()(aComponents[i], bComponents[i]);
        if (resultΔ1 != 0) {
            return resultΔ1;
        }
    }
    return cmp.Compare(len(aComponents), len(bComponents));
}

public static nint ComparePathsCaseSensitive(@string a, @string b, @string currentDirectory) {
    return ComparePaths(a, b, new ComparePathsOptions(UseCaseSensitiveFileNames: true, CurrentDirectory: currentDirectory));
}

public static nint ComparePathsCaseInsensitive(@string a, @string b, @string currentDirectory) {
    return ComparePaths(a, b, new ComparePathsOptions(UseCaseSensitiveFileNames: false, CurrentDirectory: currentDirectory));
}

public static bool ContainsPath(@string parent, @string child, ComparePathsOptions options) {
    parent = CombinePaths(options.CurrentDirectory, parent);
    child = CombinePaths(options.CurrentDirectory, child);
    if (parent == ""u8 || child == ""u8) {
        return false;
    }
    if (parent == child) {
        return true;
    }
    var parentComponents = reducePathComponents(GetPathComponents(parent, ""));
    var childComponents = reducePathComponents(GetPathComponents(child, ""));
    if (len(childComponents) < len(parentComponents)) {
        return false;
    }

    var componentComparer = options.getEqualityComparer();
    foreach (var (i, parentComponent) in range(parentComponents)) {
        Func<@string, @string, bool> comparer = default;
        if (i == 0) {
            comparer = stringutil.EquateStringCaseInsensitive;
        } else {
            comparer = componentComparer;
        }
        if (!comparer(parentComponent, childComponents[i])) {
            return false;
        }
    }

    return true;
}

} partial class tspathᴛPath {

public static bool ContainsPath(this Path p, Path child) {
    if (len(p) == 0) {
        return false;
    }
    return p == child || len(child) > len(p) && strings.HasPrefix((@string)child, (@string)p) && (p[len(p) - 1] == '/' || child[len(p)] == '/');
}

} partial class tspath_package {

public static bool FileExtensionIs(@string path, @string extension) {
    return len(path) > len(extension) && strings.HasSuffix(path, extension);
}

public static T ForEachAncestorDirectoryStoppingAtGlobalCache<T>(@string globalCacheLocation, @string directory, Func<@string, (T, bool)> callback) {
    var (result, _) = ForEachAncestorDirectory(directory, (T, bool) (@string ancestorDirectory) => {
        var (resultΔ1, stop) = callback(ancestorDirectory);
        if (stop || ancestorDirectory == globalCacheLocation) {
            return (resultΔ1, true);
        }
        return (resultΔ1, false);
    });
    return result;
}

public static (T result, bool ok) ForEachAncestorDirectory<T>(@string directory, Func<@string, (T, bool)> callback) {
    for (;;) {
        var (resultΔ1, stop) = callback(directory);
        if (stop) {
            return (resultΔ1, true);
        }

        @string parentPath = GetDirectoryPath(directory);
        if (parentPath == directory) {
            T zero = GoZero<T>();
            return (zero, false);
        }

        directory = parentPath;
    }
}

public static (T result, bool ok) ForEachAncestorDirectoryPath<T>(Path directory, Func<Path, (T, bool)> callback) {
    return ForEachAncestorDirectory((@string)directory, (T, bool) (@string directoryΔ1) => callback((Path)directoryΔ1));
}

public static bool HasExtension(@string fileName) {
    return strings.Contains(GetBaseFileName(fileName), ".");
}

public static (@string volume, @string rest, bool ok) SplitVolumePath(@string path) {
    if (len(path) >= 2 && IsVolumeCharacter(path[0]) && path[1] == ':') {
        return (strings.ToLower(path[0..2]), path[2..], true);
    }
    return ("", path, false);
}

public static (slice<@string> parents, map<@string, EmptyStruct> ignored) GetCommonParents(slice<@string> paths, nint minComponents, Func<@string, @string, slice<@string>> getPathComponents, ComparePathsOptions options) {
    map<@string, EmptyStruct> ignored = default;
    var pathComponentsᴷ = new stackAppend<slice<@string>>();

    if (minComponents < 1) {
        throw panic("minComponents must be at least 1");
    }
    if (len(paths) == 0) {
        return (default, default);
    }
    if (len(paths) == 1) {
        if (len(reducePathComponents(getPathComponents(paths[0], options.CurrentDirectory))) < minComponents) {
            return (default, new map<@string, EmptyStruct>{[paths[0]] = new()});
        }
        return (paths, default);
    }

    ignored = new map<@string, EmptyStruct>();
    var pathComponents = new slice<slice<@string>>(0, len(paths));
    foreach (var (_, path) in range(paths)) {
        var components = reducePathComponents(getPathComponents(path, options.CurrentDirectory));
        if (len(components) < minComponents) {
            ignored[path] = new EmptyStruct();
        } else {
            pathComponents = append(pathComponentsᴷ, pathComponents, components);
        }
    }

    var results = getCommonParentsWorker(pathComponents, minComponents, options);
    var resultPaths = new slice<@string>(len(results));
    foreach (var (i, comps) in range(results)) {
        resultPaths[i] = GetPathFromPathComponents(comps);
    }

    return (resultPaths, ignored);
}

internal partial struct getCommonParentsWorker_type /*dyn*/ {
    internal slice<@string> head;
    internal slice<slice<@string>> tails;
}

internal static slice<slice<@string>> getCommonParentsWorker(slice<slice<@string>> componentGroups, nint minComponents, ComparePathsOptions options) {
    var orderedGroupsᴷ = new stackAppend<Path>();

    if (len(componentGroups) == 0) {
        return default;
    }
    nint maxDepth = len(componentGroups[0]);
    foreach (var (_, comps) in range(componentGroups[1..])) {
        nint l = len(comps); if (l < maxDepth) {
            maxDepth = l;
        }
    }

    var equality = options.getEqualityComparer();
    foreach (var lastCommonIndex in range(maxDepth)) {
        @string candidate = componentGroups[0][lastCommonIndex];
        foreach (var (j, comps) in range(componentGroups[1..])) {
            if (!equality(candidate, comps[lastCommonIndex])) {
                if (lastCommonIndex < minComponents) {
                    var orderedGroups = new slice<Path>(0, len(componentGroups) - j);
                    var newGroups = new map<Path, getCommonParentsWorker_type>();
                    foreach (var (_, g) in range(componentGroups)) {
                        Path key = ToPath(g[lastCommonIndex], options.CurrentDirectory, options.UseCaseSensitiveFileNames);
                        var (_, ok) = newGroups[key, ꟷ]; if (!ok) {
                            orderedGroups = append(orderedGroupsᴷ, orderedGroups, key);
                        }
                        newGroups[key] = new getCommonParentsWorker_type(
                            head: g.slice(0, lastCommonIndex + 1),
                            tails: append(newGroups[key].tails, g.slice(lastCommonIndex + 1))
                        );
                    }
                    slices.Sort<slice<Path>, Path>(orderedGroups);
                    var result = new slice<slice<@string>>(0, len(newGroups));
                    foreach (var (_, key) in range(orderedGroups)) {
                        var group = newGroups[key];
                        var subResults = getCommonParentsWorker(group.tails, minComponents - (lastCommonIndex + 1), options);
                        foreach (var (_, sr) in range(subResults)) {
                            result = append(result, appendꓸꓸꓸ(group.head, sr));
                        }
                    }
                    return result;
                }
                return [componentGroups[0].slice(0, lastCommonIndex)];
            }
        }
    }

    return [componentGroups[0].slice(0, maxDepth)];
}

public static bool StartsWithDirectory(@string fileName, @string directoryName, bool useCaseSensitiveFileNames) {
    if (directoryName == ""u8) {
        return false;
    }

    @string canonicalFileName = GetCanonicalFileName(fileName, useCaseSensitiveFileNames);
    @string canonicalDirectoryName = GetCanonicalFileName(directoryName, useCaseSensitiveFileNames);
    canonicalDirectoryName = strings.TrimSuffix(canonicalDirectoryName, "/");
    canonicalDirectoryName = strings.TrimSuffix(canonicalDirectoryName, "\\");

    return strings.HasPrefix(canonicalFileName, canonicalDirectoryName + "/"u8) || strings.HasPrefix(canonicalFileName, canonicalDirectoryName + "\\"u8);
}

public static nint CompareNumberOfDirectorySeparators(@string path1, @string path2) {
    return cmp.Compare(strings.Count(path1, "/"), strings.Count(path2, "/"));
}

} // end tspath_package
