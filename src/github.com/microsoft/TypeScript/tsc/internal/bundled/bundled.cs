namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using filepath = path.filepath_package;
using runtime = runtime_package;
using sync = sync_package;
using testing = testing_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using System.Runtime.CompilerServices;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using path;
using time = time_package;

partial class bundled_package {

public const bool Embedded = /* embedded */ true;

public static vfs.FS WrapFS(vfs.FS fs) {
    return wrapFS(fs);
}

public static @string LibPath() {
    return libPath();
}

internal static Func<@string> bundledSourceDir = sync.OnceValue([MethodImpl(MethodImplOptions.NoInlining)] @string () => {
    var (_, filename, _, ok) = runtime.Caller(0);
    if (!ok) {
        throw panic("bundled: could not get current filename");
    }
    return filepath.Dir(filepath.FromSlash(filename));
});

internal static Func<@string> testingLibPath = sync.OnceValue(@string () => {
    if (!testing.Testing()) {
        throw panic("bundled: TestingLibPath should only be called during tests");
    }
    return tspath.NormalizeSlashes(filepath.Join(bundledSourceDir(), "libs"));
});

public static @string TestingLibPath() {
    return testingLibPath();
}

} // end bundled_package
