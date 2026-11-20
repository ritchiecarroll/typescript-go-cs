namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;

partial class core_package {

internal static @string version = "7.1.0-dev";

public static @string Version() {
    return version;
}

internal static @string versionMajorMinor = func(@string () => {
    var seenMajor = false;
    nint i = strings.IndexFunc(version, (rune r) => {
        if (r == '.') {
            if (seenMajor) {
                return true;
            }
            seenMajor = true;
        }
        return false;
    });
    if (i == -1) {
        throw panic("invalid version string: " + version);
    }
    return version.slice(0, i);
})();

public static @string VersionMajorMinor() {
    return versionMajorMinor;
}

} // end core_package
