namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using sync = sync_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using semver = go.github.com.microsoft.TypeScript.tsc.@internal.semver_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using static packagejson_package;

partial class packagejson_package {

internal static readonly ж<semver.Version> ᏑtypeScriptVersion = new StaticBox<semver.Version>(semver.MustParse(core.Version()));
internal static ref semver.Version typeScriptVersion => ref ((StaticBox<semver.Version>)ᏑtypeScriptVersion).ValueSlot;

partial struct PackageJson {
    /*embed*/ public Fields Fields;
    public bool Parseable;
    internal VersionPaths versionPaths;
    internal slice<diagnosticAndArgs> versionTraces;
    internal sync.Once once;
}

partial struct diagnosticAndArgs {
    internal ж<diagnostics.Message> message;
    internal slice<any> args;
}

} partial class packagejsonᴛPackageJson {

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
private static readonly object typesVersionsˢ = (@string)"typesVersions"u8;
private static readonly object objectˢ = (@string)"object"u8;

public static VersionPaths GetVersionPaths(this ж<PackageJson> Ꮡp, Actionꓸꓸꓸ<ж<diagnostics.Message>, any> trace) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.Ꮡonce.Do(() => {
        if (Ꮡp.Value.Fields.TypesVersions.Type == JSONValueTypeNotPresent) {
            Ꮡp.Value.versionTraces = append(Ꮡp.Value.versionTraces, new diagnosticAndArgs(
                diagnostics.X_package_json_does_not_have_a_0_field,
                [typesVersionsˢ]
            ));
            return;
        }
        if (Ꮡp.Value.Fields.TypesVersions.Type != JSONValueTypeObject) {
            Ꮡp.Value.versionTraces = append(Ꮡp.Value.versionTraces, new diagnosticAndArgs(
                diagnostics.Expected_type_of_0_field_in_package_json_to_be_1_got_2,
                slice<any>([typesVersionsˢ, objectˢ, Ꮡp.Value.Fields.TypesVersions.Type.String()])
            ));
            return;
        }

        Ꮡp.Value.versionTraces = append(Ꮡp.Value.versionTraces, new diagnosticAndArgs(
            diagnostics.X_package_json_has_a_typesVersions_field_with_version_specific_path_mappings,
            [typesVersionsˢ]
        ));

        foreach (var (key, value) in range<@string, JSONValue>(Ꮡp.Value.Fields.TypesVersions.AsObject().Entries().Invoke)) {
            var (keyRange, ok) = semver.TryParseVersionRange(key);
            if (!ok) {
                Ꮡp.Value.versionTraces = append(Ꮡp.Value.versionTraces, new diagnosticAndArgs(
                    diagnostics.X_package_json_has_a_typesVersions_entry_0_that_is_not_a_valid_semver_range,
                    [key]
                ));
                continue;
            }
            if (keyRange.Test(ᏑtypeScriptVersion)) {
                if (value.Type != JSONValueTypeObject) {
                    Ꮡp.Value.versionTraces = append(Ꮡp.Value.versionTraces, new diagnosticAndArgs(
                        diagnostics.Expected_type_of_0_field_in_package_json_to_be_1_got_2,
                        slice<any>(["typesVersions['" + key + "']", objectˢ, value.Type.String()])
                    ));
                    return;
                }
                Ꮡp.Value.versionPaths = new VersionPaths(
                    Version: key,
                    pathsJSON: value.AsObject()
                );
                return;
            }
        }

        Ꮡp.Value.versionTraces = append(Ꮡp.Value.versionTraces, new diagnosticAndArgs(
            diagnostics.X_package_json_does_not_have_a_typesVersions_entry_that_matches_version_0,
            slice<any>([core.VersionMajorMinor()])
        ));
    });
    if (trace != nil) {
        foreach (var (_, msg) in range(p.versionTraces)) {
            trace(msg.message, msg.args.ꓸꓸꓸ);
        }
    }
    return p.versionPaths;
}

} partial class packagejson_package {

partial struct VersionPaths {
    public @string Version;
    internal ж<collections.OrderedMap<@string, JSONValue>> pathsJSON;
    internal ж<collections.OrderedMap<@string, slice<@string>>> paths;
}

} partial class packagejsonᴛVersionPaths {

public static bool Exists(this ж<VersionPaths> Ꮡv) {
    ref var v = ref Ꮡv.DerefOrNull();

    return Ꮡv != nil && v.Version != ""u8 && v.pathsJSON != nil;
}

public static ж<collections.OrderedMap<@string, slice<@string>>> GetPaths(this ж<VersionPaths> Ꮡv) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (!Ꮡv.Exists()) {
        return default;
    }
    if (v.paths != nil) {
        return v.paths;
    }
    var paths = collections.NewOrderedMapWithSizeHint<@string, slice<@string>>(v.pathsJSON.Size());
    foreach (var (key, value) in range<@string, JSONValue>(v.pathsJSON.Entries().Invoke)) {
        if (value.Type != JSONValueTypeArray) {
            continue;
        }
        var Δslice = new slice<@string>(len(value.AsArray()));
        foreach (var (i, path) in range(value.AsArray())) {
            if (path.Type != JSONValueTypeString) {
                continue;
            }
            Δslice[i] = path.Value._<@string>();
        }
        paths.Set(key, Δslice);
    }
    v.paths = paths;
    return v.paths;
}

} partial class packagejson_package {

partial struct InfoCacheEntry {
    public @string PackageDirectory;
    public bool DirectoryExists;
    public ж<PackageJson> Contents;
}

} partial class packagejsonᴛInfoCacheEntry {

public static bool Exists(this ж<InfoCacheEntry> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return Ꮡp != nil && p.Contents != nil;
}

public static ж<PackageJson> GetContents(this ж<InfoCacheEntry> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp == nil || p.Contents == nil) {
        return default;
    }
    return p.Contents;
}

public static @string GetDirectory(this ж<InfoCacheEntry> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (Ꮡp == nil) {
        return "";
    }
    return p.PackageDirectory;
}

public static ж<InfoCacheEntry> WithPackageDirectory(this ж<InfoCacheEntry> Ꮡp, @string packageDirectory) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (p.PackageDirectory == packageDirectory) {
        return Ꮡp;
    }
    return Ꮡ(new InfoCacheEntry(
        PackageDirectory: packageDirectory,
        DirectoryExists: p.DirectoryExists,
        Contents: p.Contents
    ));
}

} partial class packagejson_package {

partial struct InfoCache {
    internal collections.SyncMap<tspath.Path, ж<InfoCacheEntry>> cache;
    internal @string currentDirectory;
    internal bool useCaseSensitiveFileNames;
}

public static ж<InfoCache> NewInfoCache(@string currentDirectory, bool useCaseSensitiveFileNames) {
    return Ꮡ(new InfoCache(
        currentDirectory: currentDirectory,
        useCaseSensitiveFileNames: useCaseSensitiveFileNames
    ));
}

} partial class packagejsonᴛInfoCache {

public static ж<InfoCacheEntry> Get(this ж<InfoCache> Ꮡp, @string packageJsonPath) {
    ref var p = ref Ꮡp.DerefOrNull();

    tspath.Path key = tspath.ToPath(packageJsonPath, p.currentDirectory, p.useCaseSensitiveFileNames);
    var (value, ok) = Ꮡp.Ꮡcache.Load(key); if (ok) {
        return value;
    }
    return default;
}

public static ж<InfoCacheEntry> Set(this ж<InfoCache> Ꮡp, @string packageJsonPath, ж<InfoCacheEntry> info) {
    ref var p = ref Ꮡp.DerefOrNull();

    tspath.Path key = tspath.ToPath(packageJsonPath, p.currentDirectory, p.useCaseSensitiveFileNames);
    var (actual, _) = Ꮡp.Ꮡcache.LoadOrStore(key, info);
    return actual;
}

public static void Range(this ж<InfoCache> Ꮡp, Func<tspath.Path, ж<InfoCacheEntry>, bool> f) {
    Ꮡp.Ꮡcache.Range(f);
}

} // end packagejsonᴛInfoCache
