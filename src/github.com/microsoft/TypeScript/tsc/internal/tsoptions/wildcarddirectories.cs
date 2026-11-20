namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfsmatch = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.vfsmatch_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;

partial class tsoptions_package {

internal static map<@string, bool> getWildcardDirectories(slice<@string> include, slice<@string> exclude, tspath.ComparePathsOptions comparePathsOptions) {
    var recursiveKeysᴷ = new stackAppend<@string>();

    if (len(include) == 0) {
        return default;
    }

    var excludeMatcher = vfsmatch.NewSpecMatcher(exclude, comparePathsOptions.CurrentDirectory, vfsmatch.UsageExclude, comparePathsOptions.UseCaseSensitiveFileNames);

    var wildcardDirectories = new map<@string, bool>();
    var wildCardKeyToPath = new map<@string, @string>();

    slice<@string> recursiveKeys = default;

    foreach (var (_, @file) in range(include)) {
        @string spec = tspath.NormalizePath(tspath.CombinePaths(comparePathsOptions.CurrentDirectory, @file));
        if (excludeMatcher != nil && excludeMatcher.MatchString(spec)) {
            continue;
        }

        var match = getWildcardDirectoryFromSpec(spec, comparePathsOptions.UseCaseSensitiveFileNames);
        if (match != nil) {
            @string key = match.Key;
            @string path = match.Path;
            var recursive = match.Recursive;

            var (existingPath, existsPath) = wildCardKeyToPath[key, ꟷ];
            bool existingRecursive = default;

            if (existsPath) {
                existingRecursive = wildcardDirectories[existingPath];
            }

            if (!existsPath || (!existingRecursive && recursive)) {
                @string pathToUse = path;
                if (existsPath) {
                    pathToUse = existingPath;
                }
                wildcardDirectories[pathToUse] = recursive;

                if (!existsPath) {
                    wildCardKeyToPath[key] = path;
                }

                if (recursive) {
                    recursiveKeys = append(recursiveKeysᴷ, recursiveKeys, key);
                }
            }
        }

        foreach (var (path, _) in wildcardDirectories) {
            foreach (var (_, recursiveKey) in range(recursiveKeys)) {
                @string key = toCanonicalKey(path, comparePathsOptions.UseCaseSensitiveFileNames);
                if (key != recursiveKey && tspath.ContainsPath(recursiveKey, key, comparePathsOptions)) {
                    delete(wildcardDirectories, path);
                }
            }
        }
    }

    return wildcardDirectories;
}

internal static @string toCanonicalKey(@string path, bool useCaseSensitiveFileNames) {
    if (useCaseSensitiveFileNames) {
        return path;
    }
    return strings.ToLower(path);
}

partial struct wildcardDirectoryMatch {
    public @string Key;
    public @string Path;
    public bool Recursive;
}

internal static ж<wildcardDirectoryMatch> getWildcardDirectoryFromSpec(@string spec, bool useCaseSensitiveFileNames) {
    nint firstWildcard = strings.IndexAny(spec, "*?");
    if (firstWildcard != -1) {
        nint lastSepBeforeWildcard = strings.LastIndexByte(spec.slice(0, firstWildcard), tspath.DirectorySeparator);
        if (lastSepBeforeWildcard != -1) {
            ref var path = ref heap<@string>(spec.slice(0, lastSepBeforeWildcard), out var Ꮡpath);
            nint lastDirectorySeparatorIndex = strings.LastIndexByte(spec, tspath.DirectorySeparator);

            ref var recursive = ref heap<bool>(firstWildcard < lastDirectorySeparatorIndex, out var Ꮡrecursive);

            return Ꮡ(new wildcardDirectoryMatch(
                Key: toCanonicalKey(path, useCaseSensitiveFileNames),
                Path: path,
                Recursive: recursive
            ));
        }
    }

    nint lastSepIndex = strings.LastIndexByte(spec, tspath.DirectorySeparator); if (lastSepIndex != -1) {
        @string lastSegment = spec.slice(lastSepIndex + 1);
        if (vfsmatch.IsImplicitGlob(lastSegment)) {
            ref var path = ref heap<@string>(tspath.RemoveTrailingDirectorySeparator(spec), out var Ꮡpath);
            return Ꮡ(new wildcardDirectoryMatch(
                Key: toCanonicalKey(path, useCaseSensitiveFileNames),
                Path: path,
                Recursive: true
            ));
        }
    }

    return default;
}

} // end tsoptions_package
