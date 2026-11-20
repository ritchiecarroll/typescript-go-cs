namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;

partial class tspath_package {

internal static slice<@string> ignoredPaths = [
    "/node_modules/.",
    "/.git",
    ".#"
];

public static bool ContainsIgnoredPath(@string path) {
    foreach (var (_, pattern) in range(ignoredPaths)) {
        if (strings.Contains(path, pattern)) {
            return true;
        }
    }
    return false;
}

} // end tspath_package
