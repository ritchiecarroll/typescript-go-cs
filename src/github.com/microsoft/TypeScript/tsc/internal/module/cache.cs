namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static module_package;

partial class module_package {

partial struct ModeAwareCache<T> /*map[ModeAwareCacheKey, T]*/;

partial struct moduleResolutionCacheKey {
    internal @string containingDirectory;
    internal @string moduleName;
    internal coreꓸResolutionMode resolutionMode;
    internal @string redirectConfigName;
}

partial struct moduleResolutionCache {
    internal collections.SyncMap<moduleResolutionCacheKey, ж<ResolvedModule>> cache;
}

} partial class moduleᴛmoduleResolutionCache {

internal static (ж<ResolvedModule>, bool) Get(this ж<moduleResolutionCache> Ꮡc, moduleResolutionCacheKey key) {
    return Ꮡc.Ꮡcache.Load(key);
}

internal static void Set(this ж<moduleResolutionCache> Ꮡc, moduleResolutionCacheKey key, ж<ResolvedModule> value) {
    Ꮡc.Ꮡcache.LoadOrStore(key, value);
}

} partial class module_package {

partial struct typeRefDirectiveResolutionCacheKey {
    internal @string containingDirectory;
    internal @string typeReferenceName;
    internal coreꓸResolutionMode resolutionMode;
    internal @string redirectConfigName;
    internal bool fromInferredTypesContainingFile;
}

partial struct typeRefDirectiveResolutionCache {
    internal collections.SyncMap<typeRefDirectiveResolutionCacheKey, ж<ResolvedTypeReferenceDirective>> cache;
}

} partial class moduleᴛtypeRefDirectiveResolutionCache {

internal static (ж<ResolvedTypeReferenceDirective>, bool) Get(this ж<typeRefDirectiveResolutionCache> Ꮡc, typeRefDirectiveResolutionCacheKey key) {
    return Ꮡc.Ꮡcache.Load(key);
}

internal static void Set(this ж<typeRefDirectiveResolutionCache> Ꮡc, typeRefDirectiveResolutionCacheKey key, ж<ResolvedTypeReferenceDirective> value) {
    Ꮡc.Ꮡcache.Store(key, value);
}

} partial class module_package {

partial struct parsedPatternsCache {
    internal collections.SyncMap<ж<collections.OrderedMap<@string, slice<@string>>>, ж<ParsedPatterns>> cache;
}

} partial class moduleᴛparsedPatternsCache {

internal static ж<ParsedPatterns> Get(this ж<parsedPatternsCache> Ꮡc, ж<collections.OrderedMap<@string, slice<@string>>> pathMappings) {
    var (patterns, ok) = Ꮡc.Ꮡcache.Load(pathMappings);
    if (!ok) {
        (patterns, _) = Ꮡc.Ꮡcache.LoadOrStore(pathMappings, TryParsePatterns(pathMappings));
    }
    return patterns;
}

} partial class module_package {

partial struct caches {
    internal ж<packagejson.InfoCache> packageJsonInfoCache;
    internal moduleResolutionCache moduleResolutionCache;
    internal typeRefDirectiveResolutionCache typeRefDirectiveResolutionCache;
    internal parsedPatternsCache parsedPatternsForPaths;
}

internal static caches newCaches(@string currentDirectory, bool useCaseSensitiveFileNames, ref core.CompilerOptions options) {
    return new caches(
        packageJsonInfoCache: packagejson.NewInfoCache(currentDirectory, useCaseSensitiveFileNames)
    );
}

internal static @string getRedirectConfigName(ResolvedProjectReference redirect) {
    if (redirect == nil) {
        return "";
    }
    return redirect.ConfigName();
}

} // end module_package
