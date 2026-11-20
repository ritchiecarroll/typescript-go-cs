global using resolutionKindSpecificLoader = System.Func<go.github.com.microsoft.TypeScript.tsc.@internal.module_package.extensions, go.@string, go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.module_package.resolved>>;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using maps = maps_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using vfsmatch = go.github.com.microsoft.TypeScript.tsc.@internal.vfs.vfsmatch_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.vfs;
using iter = iter_package;
using time = time_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using ꓸꓸꓸany = Span<any>;
using static module_package;

partial class module_package {

partial struct resolved {
    internal @string path;
    internal @string extension;
    internal PackageId packageId;
    internal @string originalPath;
    internal bool resolvedUsingTsExtension;
    internal bool resolvedUsingExtraExtensions;
}

} partial class moduleᴛresolved {

internal static bool shouldContinueSearching(this ж<resolved> Ꮡr) {
    return Ꮡr == nil;
}

internal static bool isResolved(this ж<resolved> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    return Ꮡr != nil && r.path != ""u8;
}

} partial class module_package {

internal static ж<resolved> continueSearching() {
    return default;
}

internal static ж<resolved> unresolved() {
    return Ꮡ(new resolved());
}

partial struct tracer {
    internal slice<DiagAndArgs> traces;
}

partial struct DiagAndArgs {
    public ж<diagnostics.Message> Message;
    public slice<any> Args;
}

} partial class moduleᴛtracer {

internal static void write(this ж<tracer> Ꮡt, ж<diagnostics.Message> diag, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.slice();
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡt != nil) {
        t.traces = append(t.traces, new DiagAndArgs(Message: diag, Args: args));
    }
}

internal static slice<DiagAndArgs> getTraces(this ж<tracer> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡt != nil) {
        return t.traces;
    }
    return default;
}

} partial class module_package {

partial struct resolutionState {
    internal ж<Resolver> resolver;
    internal ж<tracer> tracer;
    internal @string name;
    internal @string containingDirectory;
    internal bool isConfigLookup;
    internal NodeResolutionFeatures features;
    internal bool esmMode;
    internal slice<@string> conditions;
    internal extensions extensions;
    internal ж<core.CompilerOptions> compilerOptions;
    internal bool resolvePackageDirectoryOnly;
    internal bool candidateEndingIsFromConfig;
    internal bool resolvedPackageDirectory;
    internal slice<ж<ast.Diagnostic>> diagnostics;
}

internal static ж<resolutionState> newResolutionState(@string name, @string containingDirectory, bool isTypeReferenceDirective, coreꓸResolutionMode resolutionMode, ж<core.CompilerOptions> ᏑcompilerOptions, ResolvedProjectReference redirectedReference, ж<Resolver> resolver, ж<tracer> traceBuilder) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    var state = Ꮡ(new resolutionState(
        name: name,
        containingDirectory: containingDirectory,
        compilerOptions: GetCompilerOptionsWithRedirect(ᏑcompilerOptions, redirectedReference),
        resolver: resolver,
        tracer: traceBuilder
    ));

    if (isTypeReferenceDirective) {
        state.extensions = extensionsDeclaration;
    } else if (compilerOptions.NoDtsResolution == core.TSTrue) {
        state.extensions = extensionsImplementationFiles;
    } else {
        state.extensions = (extensions)((extensions)(extensionsTypeScript | extensionsJavaScript) | extensionsDeclaration);
    }

    if (!isTypeReferenceDirective && compilerOptions.GetResolveJsonModule()) {
        state.extensions |= (extensions)(extensionsJson);
    }

    switch (compilerOptions.GetModuleResolutionKind()) {
    case var ᴛ when ᴛ == core.ModuleResolutionKindNode16: {
        state.features = NodeResolutionFeaturesNode16Default;
        state.esmMode = resolutionMode == core.ModuleKindESNext;
        state.conditions = GetConditions(ᏑcompilerOptions, resolutionMode);
        break;
    }
    case var ᴛ when ᴛ == core.ModuleResolutionKindNodeNext: {
        state.features = NodeResolutionFeaturesNodeNextDefault;
        state.esmMode = resolutionMode == core.ModuleKindESNext;
        state.conditions = GetConditions(ᏑcompilerOptions, resolutionMode);
        break;
    }
    case var ᴛ when ᴛ == core.ModuleResolutionKindBundler: {
        state.features = getNodeResolutionFeatures(ᏑcompilerOptions);
        state.conditions = GetConditions(ᏑcompilerOptions, resolutionMode);
        break;
    }}

    return state;
}

public static ж<core.CompilerOptions> GetCompilerOptionsWithRedirect(ж<core.CompilerOptions> compilerOptions, ResolvedProjectReference redirectedReference) {
    if (redirectedReference == nil) {
        return compilerOptions;
    }
    var optionsFromRedirect = redirectedReference.CompilerOptions(); if (optionsFromRedirect != nil) {
        return optionsFromRedirect;
    }
    return compilerOptions;
}

partial struct Resolver {
    /*embed*/ internal caches caches;
    internal ResolutionHost host;
    internal ж<core.CompilerOptions> compilerOptions;
    internal @string typingsLocation;
    internal @string projectName;
    internal slice<@string> extraExtensions;
}

partial struct ResolverOptions {
    public ж<packagejson.InfoCache> PackageJsonCache;
}

public static ж<Resolver> NewResolver(ResolutionHost host, ж<core.CompilerOptions> options, @string typingsLocation, @string projectName, slice<@string> extraExtensions) {
    return Ꮡ(new Resolver(
        host: host,
        caches: newCaches(host.GetCurrentDirectory(), host.FS().UseCaseSensitiveFileNames(), ref (options).DerefOrNull()),
        compilerOptions: options,
        typingsLocation: typingsLocation,
        projectName: projectName,
        extraExtensions: extraExtensions
    ));
}

public static ж<Resolver> NewResolverWithOptions(ResolutionHost host, ж<core.CompilerOptions> compilerOptions, @string typingsLocation, @string projectName, ResolverOptions opts) {
    var r = Ꮡ(new Resolver(
        host: host,
        compilerOptions: compilerOptions,
        typingsLocation: typingsLocation,
        projectName: projectName
    ));
    if (opts.PackageJsonCache != nil) {
        r.packageJsonInfoCache = opts.PackageJsonCache;
    } else {
        r.caches = newCaches(host.GetCurrentDirectory(), host.FS().UseCaseSensitiveFileNames(), ref (compilerOptions).DerefOrNull());
    }
    return r;
}

} partial class moduleᴛResolver {

internal static ж<tracer> newTraceBuilder(this ref Resolver r) {
    if (r.compilerOptions.TraceResolution == core.TSTrue) {
        return Ꮡ(new tracer());
    }
    return default;
}

public static ж<packagejson.InfoCacheEntry> GetPackageScopeForPath(this ж<Resolver> Ꮡr, @string directory) {
    ref var r = ref Ꮡr.DerefOrNull();

    return (Ꮡ(new resolutionState(compilerOptions: r.compilerOptions, resolver: Ꮡr))).getPackageScopeForPath(directory);
}

public static void PackageJsonCacheEntries(this ref Resolver r, Func<tspath.Path, ж<packagejson.InfoCacheEntry>, bool> f) {
    r.caches.packageJsonInfoCache.Range(f);
}

} partial class moduleᴛtracer {

internal static void traceResolutionUsingProjectReference(this ж<tracer> Ꮡr, ResolvedProjectReference redirectedReference) {
    if (redirectedReference != nil && redirectedReference.CompilerOptions() != nil) {
        Ꮡr.write(diagnostics.Using_compiler_options_of_project_reference_redirect_0, redirectedReference.ConfigName());
    }
}

} partial class moduleᴛResolver {

public static (ж<ResolvedTypeReferenceDirective>, slice<DiagAndArgs>) ResolveTypeReferenceDirective(this ж<Resolver> Ꮡr, @string typeReferenceDirectiveName, @string containingFile, coreꓸResolutionMode resolutionMode, ResolvedProjectReference redirectedReference) {
    ref var r = ref Ꮡr.DerefOrNull();

    @string containingDirectory = tspath.GetDirectoryPath(containingFile);
    var traceBuilder = r.newTraceBuilder();

    var fromInferredTypesContainingFile = strings.HasSuffix(containingFile, InferredTypesContainingFile);

    var cacheKey = new typeRefDirectiveResolutionCacheKey(
        containingDirectory: containingDirectory,
        typeReferenceName: typeReferenceDirectiveName,
        resolutionMode: resolutionMode,
        redirectConfigName: getRedirectConfigName(redirectedReference),
        fromInferredTypesContainingFile: fromInferredTypesContainingFile
    );

    if (traceBuilder == nil) {
        var (cached, ok) = Ꮡr.of(Resolver.ᏑtypeRefDirectiveResolutionCache).Get(cacheKey); if (ok) {
            return (cached, default);
        }
    }

    var compilerOptions = GetCompilerOptionsWithRedirect(r.compilerOptions, redirectedReference);

    var (typeRoots, fromConfig) = compilerOptions.GetEffectiveTypeRoots(r.host.GetCurrentDirectory());
    if (traceBuilder != nil) {
        traceBuilder.write(diagnostics.Resolving_type_reference_directive_0_containing_file_1_root_directory_2, typeReferenceDirectiveName, containingFile, strings.Join(typeRoots, ","));
        traceBuilder.traceResolutionUsingProjectReference(redirectedReference);
    }

    var state = newResolutionState(typeReferenceDirectiveName, containingDirectory, true, resolutionMode, compilerOptions, redirectedReference, Ꮡr, traceBuilder);
    var result = state.resolveTypeReferenceDirective(typeRoots, fromConfig, fromInferredTypesContainingFile);

    if (traceBuilder != nil) {
        traceBuilder.traceTypeReferenceDirectiveResult(typeReferenceDirectiveName, result);
    }

    Ꮡr.of(Resolver.ᏑtypeRefDirectiveResolutionCache).Set(cacheKey, result);

    return (result, traceBuilder.getTraces());
}

public static (ж<ResolvedModule>, slice<DiagAndArgs>) ResolveModuleName(this ж<Resolver> Ꮡr, @string moduleName, @string containingFile, coreꓸResolutionMode resolutionMode, ResolvedProjectReference redirectedReference) {
    ref var r = ref Ꮡr.DerefOrNull();

    @string containingDirectory = tspath.GetDirectoryPath(containingFile);
    var traceBuilder = r.newTraceBuilder();

    var cacheKey = new moduleResolutionCacheKey(
        containingDirectory: containingDirectory,
        moduleName: moduleName,
        resolutionMode: resolutionMode,
        redirectConfigName: getRedirectConfigName(redirectedReference)
    );

    if (traceBuilder == nil) {
        var (cached, ok) = Ꮡr.of(Resolver.ᏑmoduleResolutionCache).Get(cacheKey); if (ok) {
            return (cached, default);
        }
    }

    var compilerOptions = GetCompilerOptionsWithRedirect(r.compilerOptions, redirectedReference);
    if (traceBuilder != nil) {
        traceBuilder.write(diagnostics.Resolving_module_0_from_1, moduleName, containingFile);
        traceBuilder.traceResolutionUsingProjectReference(redirectedReference);
    }

    var moduleResolution = compilerOptions.GetModuleResolutionKind();
    if (compilerOptions.ModuleResolution != moduleResolution) {
        if (traceBuilder != nil) {
            traceBuilder.write(diagnostics.Module_resolution_kind_is_not_specified_using_0, moduleResolution.String());
        }
    } else {
        if (traceBuilder != nil) {
            traceBuilder.write(diagnostics.Explicitly_specified_module_resolution_kind_Colon_0, moduleResolution.String());
        }
    }

    ж<ResolvedModule> result = default;
    switch (moduleResolution) {
    case var ᴛ when ᴛ == core.ModuleResolutionKindNode16 || ᴛ == core.ModuleResolutionKindNodeNext || ᴛ == core.ModuleResolutionKindBundler: {
        var state = newResolutionState(moduleName, containingDirectory, false, resolutionMode, compilerOptions, redirectedReference, Ꮡr, traceBuilder);
        result = state.resolveNodeLike();
        break;
    }
    default: {
        throw panic(fmt.Sprintf("Unexpected moduleResolution: %d"u8, moduleResolution));
        break;
    }}

    if (traceBuilder != nil) {
        if (result.IsResolved()) {
            if (result.PackageId.Name != ""u8) {
                traceBuilder.write(diagnostics.Module_name_0_was_successfully_resolved_to_1_with_Package_ID_2, moduleName, result.ResolvedFileName, result.ᏑPackageId.String());
            } else {
                traceBuilder.write(diagnostics.Module_name_0_was_successfully_resolved_to_1, moduleName, result.ResolvedFileName);
            }
        } else {
            traceBuilder.write(diagnostics.Module_name_0_was_not_resolved, moduleName);
        }
    }

    var finalResult = Ꮡr.tryResolveFromTypingsLocation(moduleName, containingDirectory, result, traceBuilder);
    Ꮡr.of(Resolver.ᏑmoduleResolutionCache).Set(cacheKey, finalResult);

    return (finalResult, traceBuilder.getTraces());
}

public static ж<ResolvedModule> ResolvePackageDirectory(this ж<Resolver> Ꮡr, @string moduleName, @string containingFile, coreꓸResolutionMode resolutionMode, ResolvedProjectReference redirectedReference) {
    ref var r = ref Ꮡr.DerefOrNull();

    var compilerOptions = GetCompilerOptionsWithRedirect(r.compilerOptions, redirectedReference);
    @string containingDirectory = tspath.GetDirectoryPath(containingFile);
    var state = newResolutionState(moduleName, containingDirectory, false, resolutionMode, compilerOptions, redirectedReference, Ꮡr, nil);
    state.resolvePackageDirectoryOnly = true;
    var result = state.loadModuleFromNearestNodeModulesDirectory(false); if (result != nil && result.path != ""u8) {
        return state.createResolvedModuleHandlingSymlink(result);
    }
    return default;
}

internal static ж<ResolvedModule> tryResolveFromTypingsLocation(this ж<Resolver> Ꮡr, @string moduleName, @string containingDirectory, ж<ResolvedModule> ᏑoriginalResult, ж<tracer> traceBuilder) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var originalResult = ref ᏑoriginalResult.DerefOrNull();

    if (r.typingsLocation == ""u8 || tspath.IsExternalModuleNameRelative(moduleName) || (originalResult.ResolvedFileName != ""u8 && tspath.ExtensionIsOneOf(originalResult.Extension, tspath.SupportedTSExtensionsWithJsonFlat))) {
        return ᏑoriginalResult;
    }

    var state = newResolutionState(
        moduleName,
        containingDirectory,
        false,
        core.ModuleKindNone,
        r.compilerOptions,
        default,
        Ꮡr,
        traceBuilder);
    if (traceBuilder != nil) {
        traceBuilder.write(diagnostics.Auto_discovery_for_typings_is_enabled_in_project_0_Running_extra_resolution_pass_for_module_1_using_cache_location_2, r.projectName, moduleName, r.typingsLocation);
    }
    var globalResolved = state.loadModuleFromImmediateNodeModulesDirectory(extensionsDeclaration, r.typingsLocation, false);
    if (globalResolved == nil) {
        return ᏑoriginalResult;
    }
    var result = state.createResolvedModule(globalResolved, true);
    result.ResolutionDiagnostics = appendꓸꓸꓸ(originalResult.ResolutionDiagnostics, result.ResolutionDiagnostics);
    return result;
}

internal static ж<ResolvedModule> resolveConfig(this ж<Resolver> Ꮡr, @string moduleName, @string containingFile) {
    ref var r = ref Ꮡr.DerefOrNull();

    @string containingDirectory = tspath.GetDirectoryPath(containingFile);
    var state = newResolutionState(moduleName, containingDirectory, false, core.ModuleKindCommonJS, r.compilerOptions, default, Ꮡr, nil);
    state.isConfigLookup = true;
    state.extensions = extensionsJson;
    return state.resolveNodeLike();
}

} partial class moduleᴛtracer {

internal static void traceTypeReferenceDirectiveResult(this ж<tracer> Ꮡr, @string typeReferenceDirectiveName, ж<ResolvedTypeReferenceDirective> Ꮡresult) {
    ref var result = ref Ꮡresult.DerefOrNull();

    if (!result.IsResolved()) {
        Ꮡr.write(diagnostics.Type_reference_directive_0_was_not_resolved, typeReferenceDirectiveName);
    } else if (result.PackageId.Name != ""u8) {
        Ꮡr.write(
            diagnostics.Type_reference_directive_0_was_successfully_resolved_to_1_with_Package_ID_2_primary_Colon_3,
            typeReferenceDirectiveName,
            result.ResolvedFileName,
            result.PackageId.String(),
            result.Primary);
    } else {
        Ꮡr.write(
            diagnostics.Type_reference_directive_0_was_successfully_resolved_to_1_primary_Colon_2,
            typeReferenceDirectiveName,
            result.ResolvedFileName,
            result.Primary);
    }
}

} partial class moduleᴛresolutionState {

internal static ж<ResolvedTypeReferenceDirective> resolveTypeReferenceDirective(this ж<resolutionState> Ꮡr, slice<@string> typeRoots, bool fromConfig, bool fromInferredTypesContainingFile) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (len(typeRoots) > 0) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Resolving_with_primary_search_path_0, strings.Join(typeRoots, ", "));
        }
        foreach (var (_, typeRoot) in range(typeRoots)) {
            @string candidate = r.getCandidateFromTypeRoot(typeRoot);
            var directoryExists = r.resolver.host.FS().DirectoryExists(typeRoot);
            if (!directoryExists) {
                if (r.tracer != nil) {
                    r.tracer.write(diagnostics.Directory_0_does_not_exist_skipping_all_lookups_in_it, typeRoot);
                }
                continue;
            }
            if (fromConfig) {
                var resolvedFromFile = r.loadModuleFromFile(extensionsDeclaration, candidate); if (!resolvedFromFile.shouldContinueSearching()) {
                    @string packageDirectory = ParseNodeModuleFromPath(resolvedFromFile.path, false);
                    if (packageDirectory != ""u8) {
                        resolvedFromFile.packageId = r.getPackageId(resolvedFromFile.path, r.getPackageJsonInfo(packageDirectory));
                    }
                    return r.createResolvedTypeReferenceDirective(resolvedFromFile, true);
                }
            }
            var resolvedFromDirectory = Ꮡr.loadNodeModuleFromDirectory(extensionsDeclaration, candidate, true); if (!resolvedFromDirectory.shouldContinueSearching()) {
                return r.createResolvedTypeReferenceDirective(resolvedFromDirectory, true);
            }
        }
    } else if (r.tracer != nil) {
        r.tracer.write(diagnostics.Root_directory_cannot_be_determined_skipping_primary_search_paths);
    }

    ж<resolved> resolved = default;
    if (!fromConfig || !fromInferredTypesContainingFile) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Looking_up_in_node_modules_folder_initial_location_0, r.containingDirectory);
        }
        if (!tspath.IsExternalModuleNameRelative(r.name)) {
            resolved = Ꮡr.loadModuleFromNearestNodeModulesDirectory(false);
        } else {
            @string candidate = normalizePathForCJSResolution(r.containingDirectory, r.name);
            resolved = Ꮡr.nodeLoadModuleByRelativeName(extensionsDeclaration, candidate, true);
        }
    } else if (r.tracer != nil) {
        r.tracer.write(diagnostics.Resolving_type_reference_directive_for_program_that_specifies_custom_typeRoots_skipping_lookup_in_node_modules_folder);
    }
    return r.createResolvedTypeReferenceDirective(resolved, false);
}

internal static @string getCandidateFromTypeRoot(this ref resolutionState r, @string typeRoot) {
    @string nameForLookup = r.name;
    if (strings.HasSuffix(typeRoot, "/node_modules/@types") || strings.HasSuffix(typeRoot, "/node_modules/@types/")) {
        nameForLookup = r.mangleScopedPackageName(r.name);
    }
    return tspath.CombinePaths(typeRoot, nameForLookup);
}

internal static @string mangleScopedPackageName(this ref resolutionState r, @string name) {
    @string mangled = MangleScopedPackageName(name);
    if (r.tracer != nil && mangled != name) {
        r.tracer.write(diagnostics.Scoped_package_detected_looking_in_0, mangled);
    }
    return mangled;
}

internal static ж<resolved> resolveFromTypeRoot(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (r.compilerOptions.TypeRoots == nil) {
        return default;
    }
    foreach (var (_, typeRoot) in range(r.compilerOptions.TypeRoots)) {
        @string candidate = r.getCandidateFromTypeRoot(typeRoot);
        var directoryExists = r.resolver.host.FS().DirectoryExists(typeRoot);
        if (!directoryExists) {
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Directory_0_does_not_exist_skipping_all_lookups_in_it, typeRoot);
            }
            continue;
        }
        var resolvedFromFile = r.loadModuleFromFile(extensionsDeclaration, candidate); if (!resolvedFromFile.shouldContinueSearching()) {
            @string packageDirectory = ParseNodeModuleFromPath(resolvedFromFile.path, false);
            if (packageDirectory != ""u8) {
                resolvedFromFile.packageId = r.getPackageId(resolvedFromFile.path, r.getPackageJsonInfo(packageDirectory));
            }
            return resolvedFromFile;
        }
        var resolved = Ꮡr.loadNodeModuleFromDirectory(extensionsDeclaration, candidate, true); if (!resolved.shouldContinueSearching()) {
            return resolved;
        }
    }
    return default;
}

internal static ж<packagejson.InfoCacheEntry> getPackageScopeForPath(this ж<resolutionState> Ꮡr, @string directory) {
    ref var r = ref Ꮡr.DerefOrNull();

    var result = tspath.ForEachAncestorDirectoryStoppingAtGlobalCache(
        r.resolver.typingsLocation,
        directory,
        (ж<packagejson.InfoCacheEntry>, bool) (@string directoryΔ1) => {
            var resultΔ1 = Ꮡr.Value.getPackageJsonInfo(directoryΔ1); if (resultΔ1 != nil) {
                return (resultΔ1, true);
            }
            return (default, false);
        });
    return result;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
private static readonly object esmˢ = (@string)"ESM"u8;
private static readonly object cjsˢ = (@string)"CJS"u8;
private static readonly @string importˢ = "import"u8;

internal static ж<ResolvedModule> resolveNodeLike(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (r.tracer != nil) {
        @string conditions = strings.Join(core.Map(r.conditions, @string (@string c) => @"'"u8 + c + @"'"u8), ", ");
        if (r.esmMode) {
            r.tracer.write(diagnostics.Resolving_in_0_mode_with_conditions_1, esmˢ, conditions);
        } else {
            r.tracer.write(diagnostics.Resolving_in_0_mode_with_conditions_1, cjsˢ, conditions);
        }
    }
    var result = Ꮡr.resolveNodeLikeWorker();
    if (r.resolvedPackageDirectory && !r.isConfigLookup && (NodeResolutionFeatures)(r.features & NodeResolutionFeaturesExports) != 0 && (extensions)(r.extensions & ((extensions)(extensionsTypeScript | extensionsDeclaration))) != 0 && !tspath.IsExternalModuleNameRelative(r.name) && result.IsResolved() && result.IsExternalLibraryImport && !extensionIsOk((extensions)(extensionsTypeScript | extensionsDeclaration), result.Extension) && slices.Contains(r.conditions, importˢ)) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Resolution_of_non_relative_name_failed_trying_with_modern_Node_resolution_features_disabled_to_see_if_npm_library_needs_configuration_update);
        }
        r.features = (NodeResolutionFeatures)(r.features & ~((NodeResolutionFeatures)NodeResolutionFeaturesExports));
        r.extensions = (extensions)(r.extensions & ((extensions)(extensionsTypeScript | extensionsDeclaration)));
        nint diagnosticsCount = len(r.diagnostics);
        var diagnosticResult = Ꮡr.resolveNodeLikeWorker(); if (diagnosticResult.IsResolved() && diagnosticResult.IsExternalLibraryImport) {
            result.AlternateResult = diagnosticResult.ResolvedFileName;
        }
        r.diagnostics = r.diagnostics.slice(0, diagnosticsCount);
    }
    return result;
}

internal static ж<ResolvedModule> resolveNodeLikeWorker(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    {
        var resolved = Ꮡr.tryLoadModuleUsingOptionalResolutionSettings(); if (!resolved.shouldContinueSearching()) {
            return r.createResolvedModuleHandlingSymlink(resolved);
        }
    }

    if (!tspath.IsExternalModuleNameRelative(r.name)) {
        if ((NodeResolutionFeatures)(r.features & NodeResolutionFeaturesImports) != 0 && strings.HasPrefix(r.name, "#")) {
            var resolved = Ꮡr.loadModuleFromImports(); if (!resolved.shouldContinueSearching()) {
                return r.createResolvedModuleHandlingSymlink(resolved);
            }
        }
        if ((NodeResolutionFeatures)(r.features & NodeResolutionFeaturesSelfName) != 0) {
            var resolved = Ꮡr.loadModuleFromSelfNameReference(); if (!resolved.shouldContinueSearching()) {
                return r.createResolvedModuleHandlingSymlink(resolved);
            }
        }
        if (strings.Contains(r.name, ":")) {
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Skipping_module_0_that_looks_like_an_absolute_URI_target_file_types_Colon_1, r.name, r.extensions.String());
            }
            return r.createResolvedModule(nil, false);
        }
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Loading_module_0_from_node_modules_folder_target_file_types_Colon_1, r.name, r.extensions.String());
        }
        {
            var resolved = Ꮡr.loadModuleFromNearestNodeModulesDirectory(false); if (!resolved.shouldContinueSearching()) {
                return r.createResolvedModuleHandlingSymlink(resolved);
            }
        }
        if ((extensions)(r.extensions & extensionsDeclaration) != 0) {
            var resolved = Ꮡr.resolveFromTypeRoot(); if (!resolved.shouldContinueSearching()) {
                return r.createResolvedModuleHandlingSymlink(resolved);
            }
        }
    } else {
        @string candidate = normalizePathForCJSResolution(r.containingDirectory, r.name);
        var resolved = Ꮡr.nodeLoadModuleByRelativeName(r.extensions, candidate, true);
        return r.createResolvedModule(
            resolved,
            resolved != nil && strings.Contains(resolved.path, "/node_modules/"));
    }
    return r.createResolvedModule(nil, false);
}

internal static ж<resolved> loadModuleFromSelfNameReference(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    @string directoryPath = tspath.GetNormalizedAbsolutePath(r.containingDirectory, r.resolver.host.GetCurrentDirectory());
    var scope = Ꮡr.getPackageScopeForPath(directoryPath);
    if (!scope.Exists() || scope.Contents.of(packagejson.PackageJson.ᏑExports).of(packagejson.ExportsOrImports.ᏑJSONValue).IsFalsy()) {
        return continueSearching();
    }
    var (name, ok) = scope.Contents.of(packagejson.PackageJson.ᏑName).GetValue();
    if (!ok) {
        return continueSearching();
    }
    var parts = tspath.GetPathComponents(r.name, "");
    var nameParts = tspath.GetPathComponents(name, "");
    if (len(parts) < len(nameParts) || !slices.Equal<slice<@string>, @string>(nameParts, parts.slice(0, len(nameParts)))) {
        return continueSearching();
    }
    var trailingParts = parts.slice(len(nameParts));
    @string subpath = default;
    if (len(trailingParts) > 0) {
        subpath = tspath.CombinePaths("."u8, trailingParts.ꓸꓸꓸ);
    } else {
        subpath = ".";
    }
    if (r.compilerOptions.GetAllowJS() && !strings.Contains(r.containingDirectory, "/node_modules/")) {
        return Ꮡr.loadModuleFromExports(scope, r.extensions, subpath);
    }
    var priorityExtensions = (extensions)(r.extensions & ((extensions)(extensionsTypeScript | extensionsDeclaration)));
    var secondaryExtensions = (extensions)(r.extensions & ~((extensions)((extensions)(extensionsTypeScript | extensionsDeclaration))));
    var resolved = Ꮡr.loadModuleFromExports(scope, priorityExtensions, subpath); if (!resolved.shouldContinueSearching()) {
        return resolved;
    }
    return Ꮡr.loadModuleFromExports(scope, secondaryExtensions, subpath);
}

internal static ж<resolved> loadModuleFromImports(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (r.name == "#"u8 || (strings.HasPrefix(r.name, "#/") && ((NodeResolutionFeatures)(r.features & NodeResolutionFeaturesImportsPatternRoot)) == 0)) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Invalid_import_specifier_0_has_no_possible_resolutions, r.name);
        }
        return continueSearching();
    }
    @string directoryPath = tspath.GetNormalizedAbsolutePath(r.containingDirectory, r.resolver.host.GetCurrentDirectory());
    var scope = Ꮡr.getPackageScopeForPath(directoryPath);
    if (!scope.Exists()) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Directory_0_has_no_containing_package_json_scope_Imports_will_not_resolve, directoryPath);
        }
        return continueSearching();
    }
    if (scope.Contents.Imports.Type != packagejson.JSONValueTypeObject) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.X_package_json_scope_0_has_no_imports_defined, scope.PackageDirectory);
        }
        return continueSearching();
    }

    var result = Ꮡr.loadModuleFromExportsOrImports(r.extensions, r.name, scope.Contents.Imports.AsObject(), scope, true); if (!result.shouldContinueSearching()) {
        return result;
    }

    if (r.tracer != nil) {
        r.tracer.write(diagnostics.Import_specifier_0_does_not_exist_in_package_json_scope_at_path_1, r.name, scope.PackageDirectory);
    }
    return continueSearching();
}

internal static ж<resolved> loadModuleFromExports(this ж<resolutionState> Ꮡr, ж<packagejson.InfoCacheEntry> ᏑpackageInfo, extensions ext, @string subpath) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var packageInfo = ref ᏑpackageInfo.DerefOrNull();

    if (!ᏑpackageInfo.Exists() || packageInfo.Contents.of(packagejson.PackageJson.ᏑExports).of(packagejson.ExportsOrImports.ᏑJSONValue).IsFalsy()) {
        return continueSearching();
    }

    if (subpath == "."u8) {
        packagejson.ExportsOrImports mainExport = new(nil);
        switch (packageInfo.Contents.Exports.Type) {
        case var ᴛ when ᴛ == packagejson.JSONValueTypeString || ᴛ == packagejson.JSONValueTypeArray: {
            mainExport = packageInfo.Contents.Exports;
            break;
        }
        case var ᴛ when ᴛ == packagejson.JSONValueTypeObject: {
            if (packageInfo.Contents.Exports.IsConditions()) {
                mainExport = packageInfo.Contents.Exports;
            } else if (packageInfo.Contents.Exports.AsObject().Get((@string)".") is var (dot, ok) && ok) {
                mainExport = dot;
            }
            break;
        }}

        if (mainExport.Type != packagejson.JSONValueTypeNotPresent) {
            return Ꮡr.loadModuleFromTargetExportOrImport(ext, subpath, ᏑpackageInfo, false, mainExport, "", false, ".");
        }
    } else if (packageInfo.Contents.Exports.Type == packagejson.JSONValueTypeObject && packageInfo.Contents.Exports.IsSubpaths()) {
        var result = Ꮡr.loadModuleFromExportsOrImports(ext, subpath, packageInfo.Contents.Exports.AsObject(), ᏑpackageInfo, false); if (!result.shouldContinueSearching()) {
            return result;
        }
    }

    if (r.tracer != nil) {
        r.tracer.write(diagnostics.Export_specifier_0_does_not_exist_in_package_json_scope_at_path_1, subpath, packageInfo.PackageDirectory);
    }
    return continueSearching();
}

internal static ж<resolved> loadModuleFromExportsOrImports(this ж<resolutionState> Ꮡr, extensions extensions, @string moduleName, ж<collections.OrderedMap<@string, packagejson.ExportsOrImports>> ᏑlookupTable, ж<packagejson.InfoCacheEntry> scope, bool isImports) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var lookupTable = ref ᏑlookupTable.DerefOrNull();

    if (!strings.HasSuffix(moduleName, "/") && !strings.Contains(moduleName, "*")) {
        var (target, ok) = lookupTable.Get(moduleName); if (ok) {
            return Ꮡr.loadModuleFromTargetExportOrImport(extensions, moduleName, scope, isImports, target, "", false, moduleName);
        }
    }

    var expandingKeys = new slice<@string>(0, ᏑlookupTable.Size());
    foreach (var key in range<@string>(ᏑlookupTable.Keys().Invoke)) {
        if (strings.Count(key, "*") == 1 || strings.HasSuffix(key, "/")) {
            expandingKeys = append(expandingKeys, key);
        }
    }
    slices.SortFunc<slice<@string>, @string>(expandingKeys, ComparePatternKeys);

    foreach (var (_, potentialTarget) in range(expandingKeys)) {
        if ((NodeResolutionFeatures)(r.features & NodeResolutionFeaturesExportsPatternTrailers) != 0 && matchesPatternWithTrailer(potentialTarget, moduleName)) {
            var (target, _) = lookupTable.Get(potentialTarget);
            nint starPos = strings.Index(potentialTarget, "*");
            @string subpath = moduleName.slice(len(potentialTarget.slice(0, starPos)), len(moduleName) - (len(potentialTarget) - 1 - starPos));
            return Ꮡr.loadModuleFromTargetExportOrImport(extensions, moduleName, scope, isImports, target, subpath, true, potentialTarget);
        } else if (strings.HasSuffix(potentialTarget, "*") && strings.HasPrefix(moduleName, potentialTarget.slice(0, len(potentialTarget) - 1))) {
            var (target, _) = lookupTable.Get(potentialTarget);
            @string subpath = moduleName.slice(len(potentialTarget) - 1);
            return Ꮡr.loadModuleFromTargetExportOrImport(extensions, moduleName, scope, isImports, target, subpath, true, potentialTarget);
        } else if (strings.HasPrefix(moduleName, potentialTarget)) {
            var (target, _) = lookupTable.Get(potentialTarget);
            @string subpath = moduleName.slice(len(potentialTarget));
            return Ꮡr.loadModuleFromTargetExportOrImport(extensions, moduleName, scope, isImports, target, subpath, false, potentialTarget);
        }
    }

    return continueSearching();
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
private static readonly @string importsˢ = "imports"u8;
private static readonly @string nodeModulesˢ = "node_modules"u8;
private static readonly @string exportsˢ = "exports"u8;

internal static ж<resolved> loadModuleFromTargetExportOrImport(this ж<resolutionState> Ꮡr, extensions extensions, @string moduleName, ж<packagejson.InfoCacheEntry> Ꮡscope, bool isImports, packagejson.ExportsOrImports target, @string subpath, bool isPattern, @string key) {
    GoFrame ᒐ = default;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var scope = ref Ꮡscope.DerefOrNull();

        switch (target.Type) {
        case var ᴛ when ᴛ == packagejson.JSONValueTypeString: {
            var (targetString, _) = target.Value._<@string>(ᐧ);
            if (!isPattern && len(subpath) > 0 && !strings.HasSuffix(targetString, "/")) {
                if (r.tracer != nil) {
                    r.tracer.write(diagnostics.X_package_json_scope_0_has_invalid_type_for_target_of_specifier_1, scope.PackageDirectory, moduleName);
                }
                return continueSearching();
            }
            if (!strings.HasPrefix(targetString, "./")) {
                if (isImports && !strings.HasPrefix(targetString, "../") && !strings.HasPrefix(targetString, "/") && !tspath.IsRootedDiskPath(targetString)) {
                    @string combinedLookup = targetString + subpath;
                    if (isPattern) {
                        combinedLookup = strings.ReplaceAll(targetString, "*", subpath);
                    }
                    @string scopeContainingDirectory = tspath.EnsureTrailingDirectorySeparator(scope.PackageDirectory);
                    if (r.tracer != nil) {
                        r.tracer.write(diagnostics.Using_0_subpath_1_with_target_2, importsˢ, key, combinedLookup);
                        r.tracer.write(diagnostics.Resolving_module_0_from_1, combinedLookup, scopeContainingDirectory);
                    }
                    @string name = r.name;
                    @string containingDirectory = r.containingDirectory;
                    (r.name, r.containingDirectory) = (combinedLookup, scopeContainingDirectory);
                    defer(() => {
                        (Ꮡr.Value.name, Ꮡr.Value.containingDirectory) = (name, containingDirectory);
                    }, ref ᒐ);
                    var result = Ꮡr.resolveNodeLike(); if (result.IsResolved()) {
                        return Ꮡ(new resolved(
                            path: result.ResolvedFileName,
                            extension: result.Extension,
                            packageId: result.PackageId,
                            originalPath: result.OriginalPath,
                            resolvedUsingTsExtension: result.ResolvedUsingTsExtension
                        ));
                    }
                    return continueSearching();
                }
                if (r.tracer != nil) {
                    r.tracer.write(diagnostics.X_package_json_scope_0_has_invalid_type_for_target_of_specifier_1, scope.PackageDirectory, moduleName);
                }
                return continueSearching();
            }
            slice<@string> parts = default;
            if (tspath.PathIsRelative(targetString)) {
                parts = tspath.GetPathComponents(targetString, "")[1..];
            } else {
                parts = tspath.GetPathComponents(targetString, "");
            }
            var partsAfterFirst = parts[1..];
            if (slices.Contains(partsAfterFirst, (@string)"..") || slices.Contains(partsAfterFirst, (@string)".") || slices.Contains(partsAfterFirst, nodeModulesˢ)) {
                if (r.tracer != nil) {
                    r.tracer.write(diagnostics.X_package_json_scope_0_has_invalid_type_for_target_of_specifier_1, scope.PackageDirectory, moduleName);
                }
                return continueSearching();
            }
            @string resolvedTarget = tspath.CombinePaths(scope.PackageDirectory, targetString);
            var subpathParts = tspath.GetPathComponents(subpath, "");
            if (slices.Contains(subpathParts, (@string)"..") || slices.Contains(subpathParts, (@string)".") || slices.Contains(subpathParts, nodeModulesˢ)) {
                if (r.tracer != nil) {
                    r.tracer.write(diagnostics.X_package_json_scope_0_has_invalid_type_for_target_of_specifier_1, scope.PackageDirectory, moduleName);
                }
                return continueSearching();
            }

            if (r.tracer != nil) {
                @string messageTarget = default;
                if (isPattern) {
                    messageTarget = strings.ReplaceAll(targetString, "*", subpath);
                } else {
                    messageTarget = targetString + subpath;
                }
                r.tracer.write(diagnostics.Using_0_subpath_1_with_target_2, core.IfElse(isImports, importsˢ, exportsˢ), key, messageTarget);
            }
            @string finalPath = default;
            if (isPattern) {
                finalPath = tspath.GetNormalizedAbsolutePath(strings.ReplaceAll(resolvedTarget, "*", subpath), r.resolver.host.GetCurrentDirectory());
            } else {
                finalPath = tspath.GetNormalizedAbsolutePath(resolvedTarget + subpath, r.resolver.host.GetCurrentDirectory());
            }
            var inputLink = r.tryLoadInputFileForPath(finalPath, subpath, tspath.CombinePaths(scope.PackageDirectory, "package.json"), isImports); if (!inputLink.shouldContinueSearching()) {
                inputLink.packageId = r.getPackageId(inputLink.path, Ꮡscope);
                return inputLink;
            }
            {
                var result = r.loadFileNameFromPackageJSONField(extensions, finalPath, targetString); if (!result.shouldContinueSearching()) {
                    result.packageId = r.getPackageId(result.path, Ꮡscope);
                    return result;
                }
            }
            return continueSearching();
        }
        case var ᴛ when ᴛ == packagejson.JSONValueTypeObject: {
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Entering_conditional_exports);
            }
            foreach (var condition in range<@string>(target.AsObject().Keys().Invoke)) {
                if (r.conditionMatches(condition)) {
                    if (r.tracer != nil) {
                        r.tracer.write(diagnostics.Matched_0_condition_1, core.IfElse(isImports, importsˢ, exportsˢ), condition);
                    }
                    var (subTarget, _) = target.AsObject().Get(condition);
                    var result = Ꮡr.loadModuleFromTargetExportOrImport(extensions, moduleName, Ꮡscope, isImports, subTarget, subpath, isPattern, key); if (!result.shouldContinueSearching()) {
                        if (result.isResolved() && r.tracer != nil) {
                            r.tracer.write(diagnostics.Resolved_under_condition_0, condition);
                        }
                        if (r.tracer != nil) {
                            r.tracer.write(diagnostics.Exiting_conditional_exports);
                        }
                        return result;
                    } else if (r.tracer != nil) {
                        r.tracer.write(diagnostics.Failed_to_resolve_under_condition_0, condition);
                    }
                } else {
                    if (r.tracer != nil) {
                        r.tracer.write(diagnostics.Saw_non_matching_condition_0, condition);
                    }
                }
            }
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Exiting_conditional_exports);
            }
            return continueSearching();
        }
        case var ᴛ when ᴛ == packagejson.JSONValueTypeArray: {
            if (len(target.AsArray()) == 0) {
                if (r.tracer != nil) {
                    r.tracer.write(diagnostics.X_package_json_scope_0_has_invalid_type_for_target_of_specifier_1, scope.PackageDirectory, moduleName);
                }
                return continueSearching();
            }
            foreach (var (_, elem) in range(target.AsArray())) {
                var result = Ꮡr.loadModuleFromTargetExportOrImport(extensions, moduleName, Ꮡscope, isImports, elem, subpath, isPattern, key); if (!result.shouldContinueSearching()) {
                    return result;
                }
            }
            break;
        }
        case var ᴛ when ᴛ == packagejson.JSONValueTypeNull: {
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.X_package_json_scope_0_explicitly_maps_specifier_1_to_null, scope.PackageDirectory, moduleName);
            }
            return unresolved();
        }}

        if (r.tracer != nil) {
            r.tracer.write(diagnostics.X_package_json_scope_0_has_invalid_type_for_target_of_specifier_1, scope.PackageDirectory, moduleName);
        }
        return continueSearching();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<resolved> tryLoadInputFileForPath(this ref resolutionState r, @string finalPath, @string entry, @string packagePath, bool isImports) {
    if (!r.isConfigLookup && (r.compilerOptions.DeclarationDir != ""u8 || r.compilerOptions.OutDir != ""u8) && !strings.Contains(finalPath, "/node_modules/") && (r.compilerOptions.ConfigFilePath == ""u8 || tspath.ContainsPath(
        tspath.GetDirectoryPath(packagePath),
        r.compilerOptions.ConfigFilePath,
        new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: r.resolver.host.FS().UseCaseSensitiveFileNames(),
            CurrentDirectory: r.resolver.host.GetCurrentDirectory()
        )))) {
        @string rootDir = default;
        if (r.compilerOptions.RootDir != ""u8) {
            rootDir = r.compilerOptions.RootDir;
        } else if (r.compilerOptions.ConfigFilePath != ""u8) {
            rootDir = tspath.GetDirectoryPath(r.compilerOptions.ConfigFilePath);
        } else {
            var diagnostic = ast.NewDiagnostic(
                nil,
                new core.TextRange(),
                core.IfElse(
                    isImports,
                    diagnostics.The_project_root_is_ambiguous_but_is_required_to_resolve_import_map_entry_0_in_file_1_Supply_the_rootDir_compiler_option_to_disambiguate,
                    diagnostics.The_project_root_is_ambiguous_but_is_required_to_resolve_export_map_entry_0_in_file_1_Supply_the_rootDir_compiler_option_to_disambiguate),
                core.IfElse(entry == ""u8, (@string)".", entry),
                packagePath);
            r.diagnostics = append(r.diagnostics, diagnostic);
            return unresolved();
        }

        var candidateDirectories = r.getOutputDirectoriesForBaseDirectory(rootDir);
        foreach (var (_, candidateDir) in range(candidateDirectories)) {
            if (tspath.ContainsPath(candidateDir, finalPath, new tspath.ComparePathsOptions(
                UseCaseSensitiveFileNames: r.resolver.host.FS().UseCaseSensitiveFileNames(),
                CurrentDirectory: r.resolver.host.GetCurrentDirectory()
            ))) {
                @string pathFragment = default;
                if (len(finalPath) > len(candidateDir)) {
                    pathFragment = finalPath.slice(len(candidateDir) + 1);
                }
                @string possibleInputBase = tspath.CombinePaths(rootDir, pathFragment);
                var jsAndDtsExtensions = slice<@string>([tspath.ExtensionMjs, tspath.ExtensionCjs, tspath.ExtensionJs, tspath.ExtensionJson, tspath.ExtensionDmts, tspath.ExtensionDcts, tspath.ExtensionDts]);
                foreach (var (_, ext) in range(jsAndDtsExtensions)) {
                    if (tspath.FileExtensionIs(possibleInputBase, ext)) {
                        var inputExts = tspath.GetPossibleOriginalInputExtensionForExtension(possibleInputBase);
                        foreach (var (_, possibleExt) in range(inputExts)) {
                            if (!extensionIsOk(r.extensions, possibleExt)) {
                                continue;
                            }
                            @string possibleInputWithInputExtension = tspath.ChangeExtension(possibleInputBase, possibleExt);
                            if (r.resolver.host.FS().FileExists(possibleInputWithInputExtension)) {
                                var resolved = r.loadFileNameFromPackageJSONField(r.extensions, possibleInputWithInputExtension, "");
                                if (!resolved.shouldContinueSearching()) {
                                    return resolved;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return continueSearching();
}

internal static slice<@string> getOutputDirectoriesForBaseDirectory(this ref resolutionState r, @string commonSourceDirGuess) {
    @string currentDir = core.IfElse(r.compilerOptions.ConfigFilePath != ""u8, r.resolver.host.GetCurrentDirectory(), commonSourceDirGuess);
    slice<@string> candidateDirectories = default;
    if (r.compilerOptions.DeclarationDir != ""u8) {
        candidateDirectories = append(candidateDirectories, tspath.GetNormalizedAbsolutePath(tspath.CombinePaths(currentDir, r.compilerOptions.DeclarationDir), r.resolver.host.GetCurrentDirectory()));
    }
    if (r.compilerOptions.OutDir != ""u8 && r.compilerOptions.OutDir != r.compilerOptions.DeclarationDir) {
        candidateDirectories = append(candidateDirectories, tspath.GetNormalizedAbsolutePath(tspath.CombinePaths(currentDir, r.compilerOptions.OutDir), r.resolver.host.GetCurrentDirectory()));
    }
    return candidateDirectories;
}

internal static ж<resolved> loadModuleFromNearestNodeModulesDirectory(this ж<resolutionState> Ꮡr, bool typesScopeOnly) {
    ref var r = ref Ꮡr.DerefOrNull();

    var mode = core.ResolutionModeCommonJS;
    if (r.esmMode || r.conditionMatches("import")) {
        mode = core.ResolutionModeESM;
    }
    var priorityExtensions = (extensions)(r.extensions & ((extensions)(extensionsTypeScript | extensionsDeclaration)));
    var secondaryExtensions = (extensions)(r.extensions & ~((extensions)((extensions)(extensionsTypeScript | extensionsDeclaration))));
    if (priorityExtensions != 0) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Searching_all_ancestor_node_modules_directories_for_preferred_extensions_Colon_0, priorityExtensions.String());
        }
        var result = Ꮡr.loadModuleFromNearestNodeModulesDirectoryWorker(priorityExtensions, mode, typesScopeOnly); if (!result.shouldContinueSearching()) {
            return result;
        }
    }
    if (secondaryExtensions != 0 && !typesScopeOnly) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Searching_all_ancestor_node_modules_directories_for_fallback_extensions_Colon_0, secondaryExtensions.String());
        }
        return Ꮡr.loadModuleFromNearestNodeModulesDirectoryWorker(secondaryExtensions, mode, typesScopeOnly);
    }
    return continueSearching();
}

internal static ж<resolved> loadModuleFromNearestNodeModulesDirectoryWorker(this ж<resolutionState> Ꮡr, extensions ext, coreꓸResolutionMode mode, bool typesScopeOnly) {
    ref var r = ref Ꮡr.DerefOrNull();

    var (result, _) = tspath.ForEachAncestorDirectory(
        r.containingDirectory,
        (ж<resolved> result, bool stop) (@string directory) => {
            if (tspath.GetBaseFileName(directory) != "node_modules"u8) {
                var resultΔ2 = Ꮡr.loadModuleFromImmediateNodeModulesDirectory(ext, directory, typesScopeOnly);
                return (resultΔ2, !resultΔ2.shouldContinueSearching());
            }
            return (continueSearching(), false);
        });
    return result;
}

internal static ж<resolved> loadModuleFromImmediateNodeModulesDirectory(this ж<resolutionState> Ꮡr, extensions extensions, @string directory, bool typesScopeOnly) {
    ref var r = ref Ꮡr.DerefOrNull();

    @string nodeModulesFolder = tspath.CombinePaths(directory, "node_modules");
    if (!r.resolver.host.FS().DirectoryExists(nodeModulesFolder)) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Directory_0_does_not_exist_skipping_all_lookups_in_it, nodeModulesFolder);
        }
        return continueSearching();
    }

    if (!typesScopeOnly) {
        var packageResult = Ꮡr.loadModuleFromSpecificNodeModulesDirectory(extensions, r.name, nodeModulesFolder); if (!packageResult.shouldContinueSearching()) {
            return packageResult;
        }
    }

    if ((extensions)(extensions & extensionsDeclaration) != 0) {
        @string nodeModulesAtTypes = tspath.CombinePaths(nodeModulesFolder, "@types");
        if (!r.resolver.host.FS().DirectoryExists(nodeModulesAtTypes)) {
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Directory_0_does_not_exist_skipping_all_lookups_in_it, nodeModulesAtTypes);
            }
            return continueSearching();
        }
        return Ꮡr.loadModuleFromSpecificNodeModulesDirectory(extensionsDeclaration, r.mangleScopedPackageName(r.name), nodeModulesAtTypes);
    }

    return continueSearching();
}

internal static ж<resolved> loadModuleFromSpecificNodeModulesDirectory(this ж<resolutionState> Ꮡr, extensions ext, @string moduleName, @string nodeModulesDirectory) {
    ref var r = ref Ꮡr.DerefOrNull();

    @string candidate = tspath.RemoveTrailingDirectorySeparator(tspath.NormalizePath(tspath.CombinePaths(nodeModulesDirectory, moduleName)));
    var (packageName, rest) = ParsePackageName(moduleName);
    ref var packageDirectory = ref heap<@string>(tspath.CombinePaths(nodeModulesDirectory, packageName), out var ᏑpackageDirectory);
    if (packageName == ""u8) {
        packageDirectory = candidate;
    }

    if (r.resolvePackageDirectoryOnly) {
        if (r.resolver.host.FS().DirectoryExists(packageDirectory)) {
            return Ꮡ(new resolved(path: packageDirectory));
        }
        return continueSearching();
    }

    ж<packagejson.InfoCacheEntry> rootPackageInfo = default;
    ref var packageInfo = ref heap<ж<packagejson.InfoCacheEntry>>(r.getPackageJsonInfo(candidate), out var ᏑpackageInfo);
    if (rest != ""u8 && packageInfo.Exists()) {
        if ((NodeResolutionFeatures)(r.features & NodeResolutionFeaturesExports) != 0) {
            rootPackageInfo = r.getPackageJsonInfo(packageDirectory);
        }
        if (!rootPackageInfo.Exists() || rootPackageInfo.Contents.Exports.Type == packagejson.JSONValueTypeNotPresent) {
            var fromFile = r.loadModuleFromFile(ext, candidate); if (!fromFile.shouldContinueSearching()) {
                return fromFile;
            }

            var fromDirectory = Ꮡr.loadNodeModuleFromDirectoryWorker(ext, candidate, packageInfo); if (!fromDirectory.shouldContinueSearching()) {
                fromDirectory.packageId = r.getPackageId(fromDirectory.path, packageInfo);
                return fromDirectory;
            }
        }
    }

    var loader = (extensions extensions, @string candidateΔ1) => {
        if (rest != ""u8 || !Ꮡr.Value.esmMode) {
            var fromFile = Ꮡr.Value.loadModuleFromFile(extensions, candidateΔ1); if (!fromFile.shouldContinueSearching()) {
                fromFile.packageId = Ꮡr.Value.getPackageId(fromFile.path, ᏑpackageInfo.ValueSlot);
                return fromFile;
            }
        }
        var fromDirectory = Ꮡr.loadNodeModuleFromDirectoryWorker(extensions, candidateΔ1, ᏑpackageInfo.ValueSlot); if (!fromDirectory.shouldContinueSearching()) {
            fromDirectory.packageId = Ꮡr.Value.getPackageId(fromDirectory.path, ᏑpackageInfo.ValueSlot);
            return fromDirectory;
        }
        if (rest == ""u8 && ᏑpackageInfo.ValueSlot.Exists() && (ᏑpackageInfo.ValueSlot.Contents.Exports.Type == packagejson.JSONValueTypeNotPresent || ᏑpackageInfo.ValueSlot.Contents.Exports.Type == packagejson.JSONValueTypeNull) && Ꮡr.Value.esmMode) {
            var indexResult = Ꮡr.Value.loadModuleFromFile(extensions, tspath.CombinePaths(candidateΔ1, "index.js")); if (!indexResult.shouldContinueSearching()) {
                indexResult.packageId = Ꮡr.Value.getPackageId(indexResult.path, ᏑpackageInfo.ValueSlot);
                return indexResult;
            }
        }
        return continueSearching();
    };

    if (rest != ""u8) {
        packageInfo = rootPackageInfo;
        if (packageInfo == nil) {
            packageInfo = r.getPackageJsonInfo(packageDirectory);
        }
    }
    if (packageInfo != nil) {
        r.resolvedPackageDirectory = true;
        if ((NodeResolutionFeatures)(r.features & NodeResolutionFeaturesExports) != 0 && packageInfo.Exists() && !packageInfo.Contents.of(packagejson.PackageJson.ᏑExports).of(packagejson.ExportsOrImports.ᏑJSONValue).IsFalsy()) {
            return Ꮡr.loadModuleFromExports(packageInfo, ext, tspath.CombinePaths(".", rest));
        }
        if (rest != ""u8 && packageInfo.Exists()) {
            ref var versionPaths = ref heap<packagejson.VersionPaths>(packageInfo.Contents.GetVersionPaths(r.getTraceFunc()), out var ᏑversionPaths);
            if (ᏑversionPaths.Exists()) {
                if (r.tracer != nil) {
                    r.tracer.write(diagnostics.X_package_json_has_a_typesVersions_entry_0_that_matches_compiler_version_1_looking_for_a_pattern_to_match_module_name_2, versionPaths.Version, core.Version(), rest);
                }
                var pathPatterns = TryParsePatterns(ᏑversionPaths.GetPaths());
                var fromPaths = r.tryLoadModuleUsingPaths(ext, rest, packageDirectory, ᏑversionPaths.GetPaths(), pathPatterns, loader); if (!fromPaths.shouldContinueSearching()) {
                    return fromPaths;
                }
            }
        }
    }
    return loader(ext, candidate);
}

internal static ж<ResolvedModule> createResolvedModuleHandlingSymlink(this ref resolutionState r, ж<resolved> Ꮡresolved) {
    ref var resolved = ref Ꮡresolved.DerefOrNull();

    var isExternalLibraryImport = Ꮡresolved != nil && strings.Contains(resolved.path, "/node_modules/");
    if (r.compilerOptions.PreserveSymlinks != core.TSTrue && isExternalLibraryImport && resolved.originalPath == ""u8 && !tspath.IsExternalModuleNameRelative(r.name)) {
        var (originalPath, resolvedFileName) = r.getOriginalAndResolvedFileName(resolved.path);
        if (originalPath != ""u8) {
            resolved.path = resolvedFileName;
            resolved.originalPath = originalPath;
        }
    }
    return r.createResolvedModule(Ꮡresolved, isExternalLibraryImport);
}

internal static ж<ResolvedModule> createResolvedModule(this ref resolutionState r, ж<resolved> Ꮡresolved, bool isExternalLibraryImport) {
    ref var resolved = ref Ꮡresolved.DerefOrNull();

    ref var resolvedModule = ref heap(new ResolvedModule(), out var ᏑresolvedModule);
    resolvedModule.ResolutionDiagnostics = r.diagnostics;

    if (Ꮡresolved != nil) {
        resolvedModule.ResolvedFileName = resolved.path;
        resolvedModule.OriginalPath = resolved.originalPath;
        resolvedModule.IsExternalLibraryImport = isExternalLibraryImport;
        resolvedModule.ResolvedUsingTsExtension = resolved.resolvedUsingTsExtension;
        resolvedModule.ResolvedUsingExtraExtensions = resolved.resolvedUsingExtraExtensions;
        resolvedModule.Extension = resolved.extension;
        resolvedModule.PackageId = resolved.packageId;
    }
    return ᏑresolvedModule;
}

internal static ж<ResolvedTypeReferenceDirective> createResolvedTypeReferenceDirective(this ref resolutionState r, ж<resolved> Ꮡresolved, bool primary) {
    ref var resolved = ref Ꮡresolved.DerefOrNull();

    ref var resolvedTypeReferenceDirective = ref heap(new ResolvedTypeReferenceDirective(), out var ᏑresolvedTypeReferenceDirective);
    resolvedTypeReferenceDirective.ResolutionDiagnostics = r.diagnostics;

    if (Ꮡresolved.isResolved()) {
        if (!tspath.ExtensionIsTs(resolved.extension)) {
            throw panic("expected a TypeScript file extension");
        }
        resolvedTypeReferenceDirective.ResolvedFileName = resolved.path;
        resolvedTypeReferenceDirective.Primary = primary;
        resolvedTypeReferenceDirective.PackageId = resolved.packageId;
        resolvedTypeReferenceDirective.IsExternalLibraryImport = strings.Contains(resolved.path, "/node_modules/");

        if (r.compilerOptions.PreserveSymlinks != core.TSTrue) {
            var (originalPath, resolvedFileName) = r.getOriginalAndResolvedFileName(resolved.path);
            if (originalPath != ""u8) {
                resolvedTypeReferenceDirective.ResolvedFileName = resolvedFileName;
                resolvedTypeReferenceDirective.OriginalPath = originalPath;
            }
        }
    }
    return ᏑresolvedTypeReferenceDirective;
}

internal static (@string, @string) getOriginalAndResolvedFileName(this ref resolutionState r, @string fileName) {
    @string resolvedFileName = r.realPath(fileName);
    var comparePathsOptions = new tspath.ComparePathsOptions(
        UseCaseSensitiveFileNames: r.resolver.host.FS().UseCaseSensitiveFileNames(),
        CurrentDirectory: r.resolver.host.GetCurrentDirectory()
    );
    if (tspath.ComparePaths(fileName, resolvedFileName, comparePathsOptions) == 0) {
        return ("", fileName);
    }
    return (fileName, resolvedFileName);
}

internal static ж<resolved> tryLoadModuleUsingOptionalResolutionSettings(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    var resolved = Ꮡr.tryLoadModuleUsingPathsIfEligible(); if (!resolved.shouldContinueSearching()) {
        return resolved;
    }

    if (!tspath.IsExternalModuleNameRelative(r.name)) {
        return continueSearching();
    } else {
        return Ꮡr.tryLoadModuleUsingRootDirs();
    }
}

internal static ж<ParsedPatterns> getParsedPatternsForPaths(this ref resolutionState r) {
    return r.resolver.getParsedPatternsForPaths(r.compilerOptions);
}

internal static ж<resolved> tryLoadModuleUsingPathsIfEligible(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (r.compilerOptions.Paths.Size() > 0 && !tspath.PathIsRelative(r.name)) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.X_paths_option_is_specified_looking_for_a_pattern_to_match_module_name_0, r.name);
        }
    } else {
        return continueSearching();
    }
    @string baseDirectory = r.compilerOptions.GetPathsBasePath(r.resolver.host.GetCurrentDirectory());
    var pathPatterns = r.getParsedPatternsForPaths();
    return r.tryLoadModuleUsingPaths(
        r.extensions,
        r.name,
        baseDirectory,
        r.compilerOptions.Paths,
        pathPatterns,
        (extensions extensions, @string candidate) => Ꮡr.nodeLoadModuleByRelativeName(extensions, candidate, true));
}

internal static ж<resolved> tryLoadModuleUsingPaths(this ref resolutionState r, extensions extensions, @string moduleName, @string containingDirectory, ж<collections.OrderedMap<@string, slice<@string>>> Ꮡpaths, ж<ParsedPatterns> pathPatterns, resolutionKindSpecificLoader loader) {
    ref var paths = ref Ꮡpaths.DerefOrNull();

    var matchedPattern = MatchPatternOrExact(pathPatterns, moduleName); if (matchedPattern.IsValid()) {
        @string matchedStar = matchedPattern.MatchedText(moduleName);
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Module_name_0_matched_pattern_1, moduleName, matchedPattern.Text);
        }
        foreach (var (_, subst) in range(paths.GetOrZero(matchedPattern.Text))) {
            @string path = strings.Replace(subst, "*", matchedStar, 1);
            @string candidate = tspath.NormalizePath(tspath.CombinePaths(containingDirectory, path));
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Trying_substitution_0_candidate_module_location_Colon_1, subst, path);
            }
            ref var extensionFromSubst = ref heap<@string>(tspath.TryGetExtensionFromPath(subst), out var ᏑextensionFromSubst);
            if (extensionFromSubst != ""u8) {
                ref var pathΔ1 = ref heap<@string>(out var ᏑpathΔ1);
                (pathΔ1, var ok) = r.tryFile(candidate); if (ok) {
                    return Ꮡ(new resolved(
                        path: pathΔ1,
                        extension: extensionFromSubst
                    ));
                }
            }
            var saveCandidateEndingIsFromConfig = r.candidateEndingIsFromConfig;
            if (extensionFromSubst != ""u8) {
                r.candidateEndingIsFromConfig = true;
            }
            var resolved = loader(extensions, candidate);
            r.candidateEndingIsFromConfig = saveCandidateEndingIsFromConfig;
            if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
    }
    return continueSearching();
}

internal static ж<resolved> tryLoadModuleUsingRootDirs(this ж<resolutionState> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (len(r.compilerOptions.RootDirs) == 0) {
        return continueSearching();
    }

    if (r.tracer != nil) {
        r.tracer.write(diagnostics.X_rootDirs_option_is_set_using_it_to_resolve_relative_module_name_0, r.name);
    }

    @string candidate = tspath.NormalizePath(tspath.CombinePaths(r.containingDirectory, r.name));

    @string matchedRootDir = default;
    @string matchedNormalizedPrefix = default;
    foreach (var (_, rootDir) in range(r.compilerOptions.RootDirs)) {
        @string normalizedRoot = tspath.NormalizePath(rootDir);
        if (!strings.HasSuffix(normalizedRoot, "/")) {
            normalizedRoot += "/"u8;
        }
        var isLongestMatchingPrefix = strings.HasPrefix(candidate, normalizedRoot) && (matchedNormalizedPrefix == ""u8 || len(matchedNormalizedPrefix) < len(normalizedRoot));

        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Checking_if_0_is_the_longest_matching_prefix_for_1_2, normalizedRoot, candidate, isLongestMatchingPrefix);
        }

        if (isLongestMatchingPrefix) {
            matchedNormalizedPrefix = normalizedRoot;
            matchedRootDir = rootDir;
        }
    }

    if (matchedNormalizedPrefix != ""u8) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Longest_matching_prefix_for_0_is_1, candidate, matchedNormalizedPrefix);
        }
        @string suffix = candidate.slice(len(matchedNormalizedPrefix));

        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Loading_0_from_the_root_dir_1_candidate_location_2, suffix, matchedNormalizedPrefix, candidate);
        }
        ж<resolved> loader(extensions extensions, @string candidateΔ1) => Ꮡr.nodeLoadModuleByRelativeName(extensions, candidateΔ1, true);
        {
            var resolvedFileName = loader(r.extensions, candidate); if (!resolvedFileName.shouldContinueSearching()) {
                return resolvedFileName;
            }
        }

        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Trying_other_entries_in_rootDirs);
        }
        foreach (var (_, rootDir) in range(r.compilerOptions.RootDirs)) {
            if (rootDir == matchedRootDir) {
                continue;
            }
            @string candidateΔ2 = tspath.CombinePaths(tspath.NormalizePath(rootDir), suffix);
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Loading_0_from_the_root_dir_1_candidate_location_2, suffix, rootDir, candidateΔ2);
            }
            var resolvedFileName = loader(r.extensions, candidateΔ2); if (!resolvedFileName.shouldContinueSearching()) {
                return resolvedFileName;
            }
        }
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Module_resolution_using_rootDirs_has_failed);
        }
    }
    return continueSearching();
}

internal static ж<resolved> nodeLoadModuleByRelativeName(this ж<resolutionState> Ꮡr, extensions extensions, @string candidate, bool considerPackageJson) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (r.tracer != nil) {
        r.tracer.write(diagnostics.Loading_module_as_file_Slash_folder_candidate_module_location_0_target_file_types_Colon_1, candidate, extensions.String());
    }
    if (!tspath.HasTrailingDirectorySeparator(candidate)) {
        @string parentOfCandidate = tspath.GetDirectoryPath(candidate);
        if (!r.resolver.host.FS().DirectoryExists(parentOfCandidate)) {
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Directory_0_does_not_exist_skipping_all_lookups_in_it, parentOfCandidate);
            }
            return continueSearching();
        }
        var resolvedFromFile = r.loadModuleFromFile(extensions, candidate);
        if (resolvedFromFile != nil) {
            if (considerPackageJson) {
                @string packageDirectory = ParseNodeModuleFromPath(resolvedFromFile.path, false); if (packageDirectory != ""u8) {
                    resolvedFromFile.packageId = r.getPackageId(resolvedFromFile.path, r.getPackageJsonInfo(packageDirectory));
                }
            }
            return resolvedFromFile;
        }
    }
    if (!r.resolver.host.FS().DirectoryExists(candidate)) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Directory_0_does_not_exist_skipping_all_lookups_in_it, candidate);
        }
        return continueSearching();
    }
    if (!r.esmMode) {
        return Ꮡr.loadNodeModuleFromDirectory(extensions, candidate, considerPackageJson);
    }
    return continueSearching();
}

internal static ж<resolved> loadModuleFromFile(this ref resolutionState r, extensions extensions, @string candidate) {
    var resolvedByReplacingExtension = r.loadModuleFromFileNoImplicitExtensions(extensions, candidate);
    if (resolvedByReplacingExtension != nil) {
        return resolvedByReplacingExtension;
    }

    if (!r.esmMode) {
        return r.tryAddingExtensions(candidate, extensions, "");
    }

    return continueSearching();
}

internal static ж<resolved> loadModuleFromFileNoImplicitExtensions(this ref resolutionState r, extensions extensions, @string candidate) {
    @string @base = tspath.GetBaseFileName(candidate);
    if (!strings.Contains(@base, ".")) {
        return continueSearching();
    }
    @string extensionless = tspath.RemoveFileExtension(candidate);
    if (extensionless == candidate) {
        @string extensionΔ1 = tspath.GetLongestExtensionFromPath(candidate, r.resolver.extraExtensions, false);
        if (extensionΔ1 == ""u8) {
            extensionΔ1 = candidate.slice(strings.LastIndex(candidate, "."));
        }
        extensionless = tspath.RemoveExtension(candidate, extensionΔ1);
    }

    @string extension = candidate.slice(len(extensionless));
    if (r.tracer != nil) {
        r.tracer.write(diagnostics.File_name_0_has_a_1_extension_stripping_it, candidate, extension);
    }
    return r.tryAddingExtensions(extensionless, extensions, extension);
}

internal static ж<resolved> tryAddingExtensions(this ref resolutionState r, @string extensionless, extensions extensions, @string originalExtension) {
    @string directory = tspath.GetDirectoryPath(extensionless);
    if (directory != ""u8 && !r.resolver.host.FS().DirectoryExists(directory)) {
        return continueSearching();
    }

    switch (ᐧ) {
    case {} when originalExtension == tspath.ExtensionMjs || originalExtension == tspath.ExtensionMts || originalExtension == tspath.ExtensionDmts: {
        if ((extensions)(extensions & extensionsTypeScript) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionMts, extensionless, originalExtension == tspath.ExtensionMts || originalExtension == tspath.ExtensionDmts); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsDeclaration) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionDmts, extensionless, originalExtension == tspath.ExtensionMts || originalExtension == tspath.ExtensionDmts); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsJavaScript) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionMjs, extensionless, false); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        return continueSearching();
    }
    case {} when originalExtension == tspath.ExtensionCjs || originalExtension == tspath.ExtensionCts || originalExtension == tspath.ExtensionDcts: {
        if ((extensions)(extensions & extensionsTypeScript) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionCts, extensionless, originalExtension == tspath.ExtensionCts || originalExtension == tspath.ExtensionDcts); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsDeclaration) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionDcts, extensionless, originalExtension == tspath.ExtensionCts || originalExtension == tspath.ExtensionDcts); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsJavaScript) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionCjs, extensionless, false); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        return continueSearching();
    }
    case {} when originalExtension == tspath.ExtensionJson: {
        if ((extensions)(extensions & extensionsDeclaration) != 0) {
            var resolved = r.tryExtension(".d.json.ts", extensionless, false); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsJson) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionJson, extensionless, false); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        return continueSearching();
    }
    case {} when originalExtension == tspath.ExtensionTsx || originalExtension == tspath.ExtensionJsx: {
        if ((extensions)(extensions & extensionsTypeScript) != 0) {
            {
                var resolved = r.tryExtension(tspath.ExtensionTsx, extensionless, originalExtension == tspath.ExtensionTsx); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
            {
                var resolved = r.tryExtension(tspath.ExtensionTs, extensionless, originalExtension == tspath.ExtensionTsx); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
        }
        if ((extensions)(extensions & extensionsDeclaration) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionDts, extensionless, originalExtension == tspath.ExtensionTsx); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsJavaScript) != 0) {
            {
                var resolved = r.tryExtension(tspath.ExtensionJsx, extensionless, false); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
            {
                var resolved = r.tryExtension(tspath.ExtensionJs, extensionless, false); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
        }
        return continueSearching();
    }
    case {} when originalExtension == tspath.ExtensionTs || originalExtension == tspath.ExtensionDts || originalExtension == tspath.ExtensionJs || originalExtension == ""u8: {
        if ((extensions)(extensions & extensionsTypeScript) != 0) {
            {
                var resolved = r.tryExtension(tspath.ExtensionTs, extensionless, originalExtension == tspath.ExtensionTs || originalExtension == tspath.ExtensionDts); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
            {
                var resolved = r.tryExtension(tspath.ExtensionTsx, extensionless, originalExtension == tspath.ExtensionTs || originalExtension == tspath.ExtensionDts); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
        }
        if ((extensions)(extensions & extensionsDeclaration) != 0) {
            var resolved = r.tryExtension(tspath.ExtensionDts, extensionless, originalExtension == tspath.ExtensionTs || originalExtension == tspath.ExtensionDts); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsJavaScript) != 0) {
            {
                var resolved = r.tryExtension(tspath.ExtensionJs, extensionless, false); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
            {
                var resolved = r.tryExtension(tspath.ExtensionJsx, extensionless, false); if (!resolved.shouldContinueSearching()) {
                    return resolved;
                }
            }
        }
        if (r.isConfigLookup) {
            var resolved = r.tryExtension(tspath.ExtensionJson, extensionless, false); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        return continueSearching();
    }
    default: {
        if (slices.Contains(r.resolver.extraExtensions, originalExtension)) {
            var resolved = r.tryExtension(originalExtension, extensionless, false); if (!resolved.shouldContinueSearching()) {
                resolved.resolvedUsingExtraExtensions = true;
                return resolved;
            }
        }
        if ((extensions)(extensions & extensionsDeclaration) != 0 && !tspath.IsDeclarationFileName(extensionless + originalExtension)) {
            var resolved = r.tryExtension(".d"u8 + originalExtension + ".ts"u8, extensionless, false); if (!resolved.shouldContinueSearching()) {
                return resolved;
            }
        }
        return continueSearching();
    }}

}

internal static ж<resolved> tryExtension(this ref resolutionState r, @string extension, @string extensionless, bool resolvedUsingTsExtension) {
    @string fileName = extensionless + extension;
    ref var path = ref heap<@string>(out var Ꮡpath);
    (path, var ok) = r.tryFile(fileName); if (ok) {
        return Ꮡ(new resolved(
            path: path,
            extension: extension,
            resolvedUsingTsExtension: !r.candidateEndingIsFromConfig && resolvedUsingTsExtension
        ));
    }
    return continueSearching();
}

internal static (@string, bool) tryFile(this ref resolutionState r, @string fileName) {
    if (len(r.compilerOptions.ModuleSuffixes) == 0) {
        return (fileName, r.tryFileLookup(fileName));
    }

    @string ext = tspath.TryGetExtensionFromPath(fileName);
    @string fileNameNoExtension = tspath.RemoveExtension(fileName, ext);
    foreach (var (_, suffix) in range(r.compilerOptions.ModuleSuffixes)) {
        @string path = fileNameNoExtension + suffix + ext;
        if (r.tryFileLookup(path)) {
            return (path, true);
        }
    }
    return (fileName, false);
}

internal static bool tryFileLookup(this ref resolutionState r, @string fileName) {
    if (r.resolver.host.FS().FileExists(fileName)) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.File_0_exists_use_it_as_a_name_resolution_result, fileName);
        }
        return true;
    } else if (r.tracer != nil) {
        r.tracer.write(diagnostics.File_0_does_not_exist, fileName);
    }
    return false;
}

internal static ж<resolved> loadNodeModuleFromDirectory(this ж<resolutionState> Ꮡr, extensions extensions, @string candidate, bool considerPackageJson) {
    ref var r = ref Ꮡr.DerefOrNull();

    ж<packagejson.InfoCacheEntry> packageInfo = default;
    if (considerPackageJson) {
        packageInfo = r.getPackageJsonInfo(candidate);
    }

    return Ꮡr.loadNodeModuleFromDirectoryWorker(extensions, candidate, packageInfo);
}

internal static ж<resolved> loadNodeModuleFromDirectoryWorker(this ж<resolutionState> Ꮡr, extensions ext, @string candidate, ж<packagejson.InfoCacheEntry> ᏑpackageInfo) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var packageInfo = ref ᏑpackageInfo.DerefOrNull();

    @string packageFile = default;
    ref var versionPaths = ref heap(new packagejson.VersionPaths(), out var ᏑversionPaths);
    if (ᏑpackageInfo.Exists()) {
        versionPaths = packageInfo.Contents.GetVersionPaths(r.getTraceFunc());
        if (tspath.ComparePaths(candidate, packageInfo.PackageDirectory, new tspath.ComparePathsOptions(UseCaseSensitiveFileNames: r.resolver.host.FS().UseCaseSensitiveFileNames())) == 0) {
            var (@file, ok) = r.getPackageFile(ext, ᏑpackageInfo); if (ok) {
                packageFile = @file;
            }
        }
    }

    var loader = (extensions extensions, @string candidateΔ1) => {
        var fromFile = Ꮡr.Value.loadFileNameFromPackageJSONField(extensions, candidateΔ1, packageFile); if (!fromFile.shouldContinueSearching()) {
            return fromFile;
        }

        var expandedExtensions = extensions;
        if (extensions == extensionsDeclaration) {
            expandedExtensions = (extensions)(extensionsTypeScript | extensionsDeclaration);
        }

        var saveESMMode = Ꮡr.Value.esmMode;
        var saveCandidateEndingIsFromConfig = Ꮡr.Value.candidateEndingIsFromConfig;
        Ꮡr.Value.candidateEndingIsFromConfig = true;
        if (ᏑpackageInfo.Exists() && ᏑpackageInfo.Value.Contents.Type.Value != "module"u8) {
            Ꮡr.Value.esmMode = false;
        }
        var result = Ꮡr.nodeLoadModuleByRelativeName(expandedExtensions, candidateΔ1, false);
        Ꮡr.Value.esmMode = saveESMMode;
        Ꮡr.Value.candidateEndingIsFromConfig = saveCandidateEndingIsFromConfig;
        return result;
    };

    @string indexPath = default;
    if (r.isConfigLookup) {
        indexPath = tspath.CombinePaths(candidate, "tsconfig");
    } else {
        indexPath = tspath.CombinePaths(candidate, "index");
    }

    if (ᏑversionPaths.Exists() && (packageFile == ""u8 || tspath.ContainsPath(candidate, packageFile, new tspath.ComparePathsOptions()))) {
        @string moduleName = default;
        if (packageFile != ""u8) {
            moduleName = tspath.GetRelativePathFromDirectory(candidate, packageFile, new tspath.ComparePathsOptions());
        } else {
            moduleName = tspath.GetRelativePathFromDirectory(candidate, indexPath, new tspath.ComparePathsOptions());
        }
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.X_package_json_has_a_typesVersions_entry_0_that_matches_compiler_version_1_looking_for_a_pattern_to_match_module_name_2, versionPaths.Version, core.Version(), moduleName);
        }
        var pathPatterns = TryParsePatterns(ᏑversionPaths.GetPaths());
        var result = r.tryLoadModuleUsingPaths(ext, moduleName, candidate, ᏑversionPaths.GetPaths(), pathPatterns, loader); if (!result.shouldContinueSearching()) {
            if (result.packageId.Name != ""u8) {
                throw panic("expected packageId to be empty");
            }
            return result;
        }
    }

    if (packageFile != ""u8) {
        var packageFileResult = loader(ext, packageFile); if (!packageFileResult.shouldContinueSearching()) {
            if (packageFileResult.packageId.Name != ""u8) {
                throw panic("expected packageId to be empty");
            }
            return packageFileResult;
        }
    }

    if (!r.esmMode) {
        if (!r.resolver.host.FS().DirectoryExists(candidate)) {
            return continueSearching();
        }
        return r.loadModuleFromFile(ext, indexPath);
    }
    return continueSearching();
}

internal static ж<resolved> loadFileNameFromPackageJSONField(this ref resolutionState r, extensions extensions, @string candidate, @string packageJSONValue) {
    if ((extensions)(extensions & extensionsTypeScript) != 0 && tspath.HasImplementationTSFileExtension(candidate) || (extensions)(extensions & extensionsDeclaration) != 0 && tspath.IsDeclarationFileName(candidate)) {
        ref var path = ref heap<@string>(out var Ꮡpath);
        (path, var ok) = r.tryFile(candidate); if (ok) {
            ref var extension = ref heap<@string>(tspath.TryExtractTSExtension(path), out var Ꮡextension);
            ref var resolvedUsingTsExtension = ref heap<bool>(strings.HasSuffix(packageJSONValue, "*") && extension != ""u8, out var ᏑresolvedUsingTsExtension);
            return Ꮡ(new resolved(
                path: path,
                extension: extension,
                resolvedUsingTsExtension: resolvedUsingTsExtension
            ));
        }
        return continueSearching();
    }

    if (r.isConfigLookup && (extensions)(extensions & extensionsJson) != 0 && tspath.FileExtensionIs(candidate, tspath.ExtensionJson)) {
        ref var path = ref heap<@string>(out var Ꮡpath);
        (path, var ok) = r.tryFile(candidate); if (ok) {
            return Ꮡ(new resolved(
                path: path,
                extension: tspath.ExtensionJson
            ));
        }
    }

    return r.loadModuleFromFileNoImplicitExtensions(extensions, candidate);
}

internal static (@string, bool) getPackageFile(this ref resolutionState r, extensions extensions, ж<packagejson.InfoCacheEntry> ᏑpackageInfo) {
    ref var packageInfo = ref ᏑpackageInfo.DerefOrNull();

    if (!ᏑpackageInfo.Exists()) {
        return ("", false);
    }
    if (r.isConfigLookup) {
        return r.getPackageJSONPathField("tsconfig", packageInfo.Contents.of(packagejson.PackageJson.ᏑTSConfig), packageInfo.PackageDirectory);
    }
    if ((extensions)(extensions & extensionsDeclaration) != 0) {
        {
            var (packageFile, ok) = r.getPackageJSONPathField("typings", packageInfo.Contents.of(packagejson.PackageJson.ᏑTypings), packageInfo.PackageDirectory); if (ok) {
                return (packageFile, ok);
            }
        }
        {
            var (packageFile, ok) = r.getPackageJSONPathField("types", packageInfo.Contents.of(packagejson.PackageJson.ᏑTypes), packageInfo.PackageDirectory); if (ok) {
                return (packageFile, ok);
            }
        }
    }
    if ((extensions)(extensions & ((extensions)(extensionsImplementationFiles | extensionsDeclaration))) != 0) {
        return r.getPackageJSONPathField("main", packageInfo.Contents.of(packagejson.PackageJson.ᏑMain), packageInfo.PackageDirectory);
    }
    return ("", false);
}

internal static ж<packagejson.InfoCacheEntry> getPackageJsonInfo(this ref resolutionState r, @string packageDirectory) {
    @string packageJsonPath = tspath.CombinePaths(packageDirectory, "package.json");

    var existing = r.resolver.packageJsonInfoCache.Get(packageJsonPath); if (existing != nil) {
        if (existing.Contents != nil) {
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.File_0_exists_according_to_earlier_cached_lookups, packageJsonPath);
            }
            return existing.WithPackageDirectory(packageDirectory);
        } else {
            if (existing.DirectoryExists && r.tracer != nil) {
                r.tracer.write(diagnostics.File_0_does_not_exist_according_to_earlier_cached_lookups, packageJsonPath);
            }
            return default;
        }
    }

    ref var directoryExists = ref heap<bool>(r.resolver.host.FS().DirectoryExists(packageDirectory), out var ᏑdirectoryExists);
    if (directoryExists && r.resolver.host.FS().FileExists(packageJsonPath)) {
        var (contents, _) = r.resolver.host.FS().ReadFile(packageJsonPath);
        ref var packageJsonContent = ref heap<packagejson.Fields>(out var ᏑpackageJsonContent);
        (packageJsonContent, var err) = packagejson.Parse(slice<byte>(contents));
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Found_package_json_at_0, packageJsonPath);
        }
        var result = Ꮡ(new packagejson.InfoCacheEntry(
            PackageDirectory: packageDirectory,
            DirectoryExists: true,
            Contents: Ꮡ(new packagejson.PackageJson(
                Fields: packageJsonContent,
                Parseable: err == nil
            ))
        ));
        result = r.resolver.packageJsonInfoCache.Set(packageJsonPath, result);
        return result.WithPackageDirectory(packageDirectory);
    } else {
        if (directoryExists && r.tracer != nil) {
            r.tracer.write(diagnostics.File_0_does_not_exist, packageJsonPath);
        }
        _ = r.resolver.packageJsonInfoCache.Set(packageJsonPath, Ꮡ(new packagejson.InfoCacheEntry(
            PackageDirectory: packageDirectory,
            DirectoryExists: directoryExists
        )));
    }
    return default;
}

internal static PackageId getPackageId(this ref resolutionState r, @string resolvedFileName, ж<packagejson.InfoCacheEntry> ᏑpackageInfo) {
    ref var packageInfo = ref ᏑpackageInfo.DerefOrNull();

    if (ᏑpackageInfo.Exists()) {
        var packageJsonContent = packageInfo.Contents;
        var (name, ok) = packageJsonContent.of(packagejson.PackageJson.ᏑName).GetValue(); if (ok) {
            var (version, okΔ1) = packageJsonContent.of(packagejson.PackageJson.ᏑVersion).GetValue(); if (okΔ1) {
                @string subModuleName = default;
                if (len(resolvedFileName) > len(packageInfo.PackageDirectory)) {
                    subModuleName = resolvedFileName.slice(len(packageInfo.PackageDirectory) + 1);
                }
                return new PackageId(
                    Name: name,
                    Version: version,
                    SubModuleName: subModuleName,
                    PeerDependencies: r.readPackageJsonPeerDependencies(ᏑpackageInfo)
                );
            }
        }
    }
    return new PackageId();
}

internal static @string readPackageJsonPeerDependencies(this ref resolutionState r, ж<packagejson.InfoCacheEntry> ᏑpackageJsonInfo) {
    ref var packageJsonInfo = ref ᏑpackageJsonInfo.DerefOrNull();

    ref var peerDependencies = ref heap<packagejson.Expected<map<@string, @string>>>(packageJsonInfo.Contents.PeerDependencies, out var ᏑpeerDependencies);
    var ok = r.validatePackageJSONField("peerDependencies", new packagejson_ExpectedжTypeValidatedField<map<@string, @string>>(ᏑpeerDependencies));
    if (!ok || len(peerDependencies.Value) == 0) {
        return "";
    }
    if (r.tracer != nil) {
        r.tracer.write(diagnostics.X_package_json_has_a_peerDependencies_field);
    }
    @string packageDirectory = r.realPath(packageJsonInfo.PackageDirectory);
    nint nodeModulesIndex = strings.LastIndex(packageDirectory, "/node_modules");
    if (nodeModulesIndex == -1) {
        return "";
    }
    @string nodeModules = packageDirectory.slice(0, nodeModulesIndex + len("/node_modules")) + "/";
    var names = slices.AppendSeq(new slice<@string>(0, len(peerDependencies.Value)), maps.Keys<map<@string, @string>, @string, @string>(peerDependencies.Value));
    slices.Sort<slice<@string>, @string>(names);
    ref var builder = ref heap<strings.Builder>(new strings.Builder(), out var Ꮡbuilder);
    foreach (var (_, name) in range(names)) {
        var peerPackageJson = r.getPackageJsonInfo(nodeModules + name);
        if (peerPackageJson.Exists()) {
            @string version = peerPackageJson.Contents.Version.Value;
            Ꮡbuilder.WriteString("+");
            Ꮡbuilder.WriteString(name);
            Ꮡbuilder.WriteString("@");
            Ꮡbuilder.WriteString(version);
            if (r.tracer != nil) {
                r.tracer.write(diagnostics.Found_peerDependency_0_with_1_version, name, version);
            }
        } else if (r.tracer != nil) {
            r.tracer.write(diagnostics.Failed_to_find_peerDependency_0, name);
        }
    }
    return builder.String();
}

internal static @string realPath(this ref resolutionState r, @string path) {
    @string rp = tspath.NormalizePath(r.resolver.host.FS().Realpath(path));
    if (r.tracer != nil) {
        r.tracer.write(diagnostics.Resolving_real_path_for_0_result_1, path, rp);
    }
    return rp;
}

internal static bool validatePackageJSONField(this ref resolutionState r, @string fieldName, packagejson.TypeValidatedField field) {
    if (field.IsPresent()) {
        if (field.IsValid()) {
            return true;
        }
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.Expected_type_of_0_field_in_package_json_to_be_1_got_2, fieldName, field.ExpectedJSONType(), field.ActualJSONType());
        }
    }
    if (r.tracer != nil) {
        r.tracer.write(diagnostics.X_package_json_does_not_have_a_0_field, fieldName);
    }
    return false;
}

internal static (@string, bool) getPackageJSONPathField(this ref resolutionState r, @string fieldName, ж<packagejson.Expected<@string>> Ꮡfield, @string directory) {
    ref var field = ref Ꮡfield.DerefOrNull();

    if (!r.validatePackageJSONField(fieldName, new packagejson_ExpectedжTypeValidatedField<@string>(Ꮡfield))) {
        return ("", false);
    }
    if (field.Value == ""u8) {
        if (r.tracer != nil) {
            r.tracer.write(diagnostics.X_package_json_had_a_falsy_0_field, fieldName);
        }
        return ("", false);
    }
    @string path = tspath.NormalizePath(tspath.CombinePaths(directory, field.Value));
    if (r.tracer != nil) {
        r.tracer.write(diagnostics.X_package_json_has_0_field_1_that_references_2, fieldName, field.Value, path);
    }
    return (path, true);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
private static readonly @string typesˢ = "types"u8;

internal static bool conditionMatches(this ref resolutionState r, @string condition) {
    if (condition == "default"u8 || slices.Contains(r.conditions, condition)) {
        return true;
    }
    if (!slices.Contains(r.conditions, typesˢ)) {
        return false;
    }
    return IsApplicableVersionedTypesKey(condition);
}

internal static Actionꓸꓸꓸ<ж<diagnostics.Message>, any> getTraceFunc(this ref resolutionState r) {
    if (r.tracer != nil) {
        return r.tracer.write;
    }
    return default;
}

} partial class module_package {

public static slice<@string> GetConditions(ж<core.CompilerOptions> Ꮡoptions, coreꓸResolutionMode resolutionMode) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    var moduleResolution = options.GetModuleResolutionKind();
    if (resolutionMode == core.ModuleKindNone && moduleResolution == core.ModuleResolutionKindBundler) {
        resolutionMode = core.ModuleKindESNext;
    }
    var conditions = new slice<@string>(0, 3 + len(options.CustomConditions));
    if (resolutionMode == core.ModuleKindESNext) {
        conditions = append(conditions, (@string)"import");
    } else {
        conditions = append(conditions, (@string)"require");
    }

    if (options.NoDtsResolution != core.TSTrue) {
        conditions = append(conditions, (@string)"types");
    }
    if (moduleResolution != core.ModuleResolutionKindBundler) {
        conditions = append(conditions, (@string)"node");
    }
    conditions = core.Concatenate(conditions, options.CustomConditions);
    return conditions;
}

internal static NodeResolutionFeatures getNodeResolutionFeatures(ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    var features = NodeResolutionFeaturesNone;

    switch (options.GetModuleResolutionKind()) {
    case var ᴛ when ᴛ == core.ModuleResolutionKindNode16: {
        features = NodeResolutionFeaturesNode16Default;
        break;
    }
    case var ᴛ when ᴛ == core.ModuleResolutionKindNodeNext: {
        features = NodeResolutionFeaturesNodeNextDefault;
        break;
    }
    case var ᴛ when ᴛ == core.ModuleResolutionKindBundler: {
        features = NodeResolutionFeaturesBundlerDefault;
        break;
    }}

    if (options.ResolvePackageJsonExports == core.TSTrue) {
        features |= (NodeResolutionFeatures)(NodeResolutionFeaturesExports);
    } else if (options.ResolvePackageJsonExports == core.TSFalse) {
        features &= ~(NodeResolutionFeatures)(NodeResolutionFeaturesExports);
    }
    if (options.ResolvePackageJsonImports == core.TSTrue) {
        features |= (NodeResolutionFeatures)(NodeResolutionFeaturesImports);
    } else if (options.ResolvePackageJsonImports == core.TSFalse) {
        features &= ~(NodeResolutionFeatures)(NodeResolutionFeaturesImports);
    }
    return features;
}

internal static nint moveToNextDirectorySeparatorIfAvailable(@string path, nint prevSeparatorIndex, bool isFolder) {
    nint offset = prevSeparatorIndex + 1;
    nint nextSeparatorIndex = -1;
    if (offset <= len(path)) {
        nextSeparatorIndex = strings.Index(path.slice(offset), "/");
    }
    if (nextSeparatorIndex == -1) {
        if (isFolder) {
            return len(path);
        }
        return prevSeparatorIndex;
    }
    return nextSeparatorIndex + offset;
}

partial struct ParsedPatterns {
    internal collections.Set<@string> matchableStringSet;
    internal slice<core.Pattern> patterns;
}

} partial class moduleᴛResolver {

internal static ж<ParsedPatterns> getParsedPatternsForPaths(this ж<Resolver> Ꮡr, ж<core.CompilerOptions> ᏑcompilerOptions) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    return Ꮡr.of(Resolver.ᏑparsedPatternsForPaths).Get(compilerOptions.Paths);
}

} partial class module_package {

public static ж<ParsedPatterns> TryParsePatterns(ж<collections.OrderedMap<@string, slice<@string>>> pathMappings) {
    var paths = pathMappings.Keys();

    nint numPatterns = 0;
    nint numMatchables = 0;
    foreach (var path in range<@string>(paths.Invoke)) {
        var pattern = core.TryParsePattern(path); if (pattern.IsValid()) {
            if (pattern.StarIndex == -1) {
                numMatchables++;
            } else {
                numPatterns++;
            }
        }
    }

    slice<core.Pattern> patterns = default;
    collections.Set<@string> matchableStringSet = default;
    if (numPatterns != 0) {
        patterns = new slice<core.Pattern>(0, numPatterns);
    }
    if (numMatchables != 0) {
        matchableStringSet = collections.NewSetWithSizeHint<@string>(numMatchables).Value;
    }

    foreach (var path in range<@string>(paths.Invoke)) {
        var pattern = core.TryParsePattern(path); if (pattern.IsValid()) {
            if (pattern.StarIndex == -1) {
                matchableStringSet.Add(path);
            } else {
                patterns = append(patterns, pattern);
            }
        }
    }
    return Ꮡ(new ParsedPatterns(
        matchableStringSet: matchableStringSet,
        patterns: patterns
    ));
}

public static core.Pattern MatchPatternOrExact(ж<ParsedPatterns> Ꮡpatterns, @string candidate) {
    ref var patterns = ref Ꮡpatterns.DerefOrNull();

    if (Ꮡpatterns.ᏑmatchableStringSet.Has(candidate)) {
        return new core.Pattern(
            Text: candidate,
            StarIndex: -1
        );
    }
    if (len(patterns.patterns) == 0) {
        return new core.Pattern();
    }
    return core.FindBestPatternMatch<core.Pattern>(patterns.patterns, core.Identity<core.Pattern>, candidate);
}

internal static @string normalizePathForCJSResolution(@string containingDirectory, @string moduleName) {
    @string combined = tspath.CombinePaths(containingDirectory, moduleName);
    var parts = tspath.GetPathComponents(combined, "");
    @string lastPart = parts[len(parts) - 1];
    if (lastPart == "."u8 || lastPart == ".."u8) {
        return tspath.EnsureTrailingDirectorySeparator(tspath.NormalizePath(combined));
    }
    return tspath.NormalizePath(combined);
}

internal static bool matchesPatternWithTrailer(@string target, @string name) {
    if (strings.HasSuffix(target, "*")) {
        return false;
    }
    var (before, after, ok) = strings.Cut(target, "*");
    if (!ok) {
        return false;
    }
    return strings.HasPrefix(name, before) && strings.HasSuffix(name, after);
}

internal static bool extensionIsOk(extensions extensions, @string extension) {
    return ((extensions)(extensions & extensionsJavaScript) != 0 && (extension == tspath.ExtensionJs || extension == tspath.ExtensionJsx || extension == tspath.ExtensionMjs || extension == tspath.ExtensionCjs) || ((extensions)(extensions & extensionsTypeScript) != 0 && (extension == tspath.ExtensionTs || extension == tspath.ExtensionTsx || extension == tspath.ExtensionMts || extension == tspath.ExtensionCts)) || ((extensions)(extensions & extensionsDeclaration) != 0 && (extension == tspath.ExtensionDts || extension == tspath.ExtensionDmts || extension == tspath.ExtensionDcts)) || ((extensions)(extensions & extensionsJson) != 0 && extension == tspath.ExtensionJson));
}

public static ж<ResolvedModule> ResolveConfig(@string moduleName, @string containingFile, ResolutionHost host) {
    var resolver = NewResolver(host, Ꮡ(new core.CompilerOptions(ModuleResolution: core.ModuleResolutionKindNodeNext)), "", "", default);
    return resolver.resolveConfig(moduleName, containingFile);
}

public static slice<@string> GetAutomaticTypeDirectiveNames(ж<core.CompilerOptions> Ꮡoptions, ResolutionHost host) {
    ref var options = ref Ꮡoptions.DerefOrNull();
    var wildcardMatchesᴷ = new stackAppend<@string>();

    if (!options.UsesWildcardTypes()) {
        if (options.Types != nil) {
            return options.Types;
        }
        return [];
    }

    slice<@string> wildcardMatches = default;
    var (typeRoots, _) = options.GetEffectiveTypeRoots(host.GetCurrentDirectory());
    foreach (var (_, root) in range(typeRoots)) {
        if (host.FS().DirectoryExists(root)) {
            foreach (var (_, typeDirectivePath) in range(host.FS().GetAccessibleEntries(root).Directories)) {
                @string normalized = tspath.NormalizePath(typeDirectivePath);
                @string packageJsonPath = tspath.CombinePaths(root, normalized, "package.json");
                var isNotNeededPackage = false;
                if (host.FS().FileExists(packageJsonPath)) {
                    var (contents, _) = host.FS().ReadFile(packageJsonPath);
                    var (packageJsonContent, _) = packagejson.Parse(slice<byte>(contents));
                    isNotNeededPackage = packageJsonContent.Typings.Null;
                }
                if (!isNotNeededPackage) {
                    @string baseFileName = tspath.GetBaseFileName(normalized);
                    if (!strings.HasPrefix(baseFileName, ".")) {
                        wildcardMatches = append(wildcardMatchesᴷ, wildcardMatches, baseFileName);
                    }
                }
            }
        }
    }

    slice<@string> result = default;
    foreach (var (_, t) in range(options.Types)) {
        if (t == "*"u8) {
            result = appendꓸꓸꓸ(result, wildcardMatches);
        } else {
            result = append(result, t);
        }
    }
    return core.Deduplicate(result);
}

partial struct Ending /*num:nint*/;

public static Ending EndingFixed => /* iota */ 0;
public static Ending EndingExtensionChangeable => 1;
public static Ending EndingChangeable => 2;

partial struct ResolvedEntrypoint {
    public @string OriginalFileName;
    public @string ResolvedFileName;
    public @string ModuleSpecifier;
    public Ending Ending;
    public ж<collections.Set<@string>> IncludeConditions;
    public ж<collections.Set<@string>> ExcludeConditions;
}

} partial class moduleᴛResolvedEntrypoint {

public static @string SymlinkOrRealpath(this ref ResolvedEntrypoint e) {
    if (e.OriginalFileName != ""u8) {
        return e.OriginalFileName;
    }
    return e.ResolvedFileName;
}

} partial class moduleᴛResolver {

public static slice<ж<ResolvedEntrypoint>> GetEntrypointsFromPackageJsonInfo(this ж<Resolver> Ꮡr, ж<packagejson.InfoCacheEntry> ᏑpackageJson, @string packageName, bool enableDirectorySearch) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var packageJson = ref ᏑpackageJson.DerefOrNull();

    ref var extensions = ref heap<extensions>((extensions)(extensionsTypeScript | extensionsDeclaration), out var Ꮡextensions);
    ref var features = ref heap<NodeResolutionFeatures>(NodeResolutionFeaturesAll, out var Ꮡfeatures);
    var state = Ꮡ(new resolutionState(resolver: Ꮡr, extensions: extensions, features: features, compilerOptions: r.compilerOptions));
    if (ᏑpackageJson.Exists() && packageJson.Contents.of(packagejson.PackageJson.ᏑExports).of(packagejson.ExportsOrImports.ᏑJSONValue).IsPresent()) {
        var entrypoints = state.loadEntrypointsFromExportMap(ᏑpackageJson, packageName, packageJson.Contents.Exports);
        return entrypoints;
    }

    slice<ж<ResolvedEntrypoint>> result = default;
    var mainResolution = state.loadNodeModuleFromDirectoryWorker(
        extensions,
        packageJson.PackageDirectory,
        ᏑpackageJson);

    if (mainResolution.isResolved()) {
        result = append(result, r.createResolvedEntrypointHandlingSymlink(
            mainResolution.path,
            packageName,
            nil,
            nil,
            EndingFixed));
    }

    if (enableDirectorySearch) {
        var otherFiles = vfsmatch.ReadDirectory(
            r.host.FS(),
            r.host.GetCurrentDirectory(),
            packageJson.PackageDirectory,
            extensions.Array(),
            ["node_modules"],
            ["**/*"],
            vfsmatch.UnlimitedDepth);

        var comparePathsOptions = new tspath.ComparePathsOptions(UseCaseSensitiveFileNames: r.host.FS().UseCaseSensitiveFileNames());
        foreach (var (_, @file) in range(otherFiles)) {
            if (mainResolution.isResolved() && tspath.ComparePaths(@file, mainResolution.path, comparePathsOptions) == 0) {
                continue;
            }

            result = append(result, r.createResolvedEntrypointHandlingSymlink(
                @file,
                tspath.ResolvePath(packageName, tspath.GetRelativePathFromDirectory(packageJson.PackageDirectory, @file, comparePathsOptions)),
                nil,
                nil,
                EndingChangeable));
        }
    }

    if (len(result) > 0) {
        return result;
    }
    return default;
}

internal static ж<ResolvedEntrypoint> createResolvedEntrypointHandlingSymlink(this ref Resolver r, @string fileName, @string moduleSpecifier, ж<collections.Set<@string>> includeConditions, ж<collections.Set<@string>> excludeConditions, Ending ending) {
    ref var originalFileName = ref heap(new @string(), out var ᏑoriginalFileName);
    ref var resolvedFileName = ref heap<@string>(fileName, out var ᏑresolvedFileName);
    @string realPath = r.host.FS().Realpath(fileName); if (realPath != fileName) {
        originalFileName = fileName;
        resolvedFileName = realPath;
    }
    return Ꮡ(new ResolvedEntrypoint(
        OriginalFileName: originalFileName,
        ResolvedFileName: resolvedFileName,
        ModuleSpecifier: moduleSpecifier,
        IncludeConditions: includeConditions,
        ExcludeConditions: excludeConditions,
        Ending: ending
    ));
}

} partial class moduleᴛresolutionState {

internal static slice<ж<ResolvedEntrypoint>> loadEntrypointsFromExportMap(this ж<resolutionState> Ꮡr, ж<packagejson.InfoCacheEntry> packageJson, @string packageName, packagejson.ExportsOrImports exports) {
    ref var loadEntrypointsFromTargetExports = ref heap<Action<@string, ж<collections.Set<@string>>, ж<collections.Set<@string>>, packagejson.ExportsOrImports>>(out var ᏑloadEntrypointsFromTargetExports);
    ref var entrypoints = ref heap<slice<ж<ResolvedEntrypoint>>>(out var Ꮡentrypoints);

    loadEntrypointsFromTargetExports = (@string subpath, ж<collections.Set<@string>> includeConditions, ж<collections.Set<@string>> excludeConditions, packagejson.ExportsOrImports exportsΔ1) => {
        if (exportsΔ1.Type == packagejson.JSONValueTypeString && strings.HasPrefix(exportsΔ1.AsString(), "./")) {
            if (strings.ContainsRune(exportsΔ1.AsString(), '*')) {
                if (strings.IndexByte(exportsΔ1.AsString(), (byte)'*') != strings.LastIndexByte(exportsΔ1.AsString(), (byte)'*')) {
                    return;
                }
                @string patternPath = tspath.ResolvePath(packageJson.Value.PackageDirectory, exportsΔ1.AsString());
                var (leadingSlice, trailingSlice, _) = strings.Cut(patternPath, "*");
                var caseSensitive = Ꮡr.Value.resolver.host.FS().UseCaseSensitiveFileNames();
                var files = vfsmatch.ReadDirectory(
                    Ꮡr.Value.resolver.host.FS(),
                    Ꮡr.Value.resolver.host.GetCurrentDirectory(),
                    packageJson.Value.PackageDirectory,
                    Ꮡr.Value.extensions.Array(),
                    default,
                    slice<@string>([
                        tspath.ChangeFullExtension(strings.Replace(exportsΔ1.AsString(), "*", "**/*", 1), ".*")
                    ]),
                    vfsmatch.UnlimitedDepth);
                foreach (var (_, @file) in range(files)) {
                    var (matchedStar, ok) = Ꮡr.Value.getMatchedStarForPatternEntrypoint(@file, leadingSlice, trailingSlice, caseSensitive);
                    if (!ok) {
                        continue;
                    }
                    @string moduleSpecifier = tspath.ResolvePath(packageName, strings.Replace(subpath, "*", matchedStar, 1));
                    Ꮡentrypoints.ValueSlot = append(Ꮡentrypoints.ValueSlot, Ꮡr.Value.resolver.createResolvedEntrypointHandlingSymlink(
                        @file,
                        moduleSpecifier,
                        includeConditions,
                        excludeConditions,
                        core.IfElse(strings.HasSuffix(exportsΔ1.AsString(), "*"), EndingExtensionChangeable, EndingFixed)));
                }
            } else {
                var partsAfterFirst = tspath.GetPathComponents(exportsΔ1.AsString(), "")[2..];
                if (slices.Contains(partsAfterFirst, (@string)"..") || slices.Contains(partsAfterFirst, (@string)".") || slices.Contains(partsAfterFirst, nodeModulesˢ)) {
                    return;
                }
                @string resolvedTarget = tspath.ResolvePath(packageJson.Value.PackageDirectory, exportsΔ1.AsString());
                var result = Ꮡr.Value.loadFileNameFromPackageJSONField(Ꮡr.Value.extensions, resolvedTarget, exportsΔ1.AsString()); if (result.isResolved()) {
                    Ꮡentrypoints.ValueSlot = append(Ꮡentrypoints.ValueSlot, Ꮡr.Value.resolver.createResolvedEntrypointHandlingSymlink(
                        result.path,
                        tspath.ResolvePath(packageName, subpath),
                        includeConditions,
                        excludeConditions,
                        core.IfElse(strings.HasSuffix(exportsΔ1.AsString(), "*"), EndingExtensionChangeable, EndingFixed)));
                }
            }
        } else if (exportsΔ1.Type == packagejson.JSONValueTypeArray) {
            foreach (var (_, element) in range(exportsΔ1.AsArray())) {
                ᏑloadEntrypointsFromTargetExports.ValueSlot(subpath, includeConditions, excludeConditions, element);
            }
        } else if (exportsΔ1.Type == packagejson.JSONValueTypeObject) {
            slice<@string> prevConditions = default;
            foreach (var (condition, export) in range<@string, packagejson.ExportsOrImports>(exportsΔ1.AsObject().Entries().Invoke)) {
                if (excludeConditions != nil && excludeConditions.Has(condition)) {
                    continue;
                }

                var conditionAlwaysMatches = condition == "default"u8 || condition == "types"u8 || IsApplicableVersionedTypesKey(condition);
                var newIncludeConditions = includeConditions;
                if (!conditionAlwaysMatches) {
                    newIncludeConditions = includeConditions.Clone();
                    excludeConditions = excludeConditions.Clone();
                    if (newIncludeConditions == nil) {
                        newIncludeConditions = Ꮡ(new collections.Set<@string>());
                    }
                    newIncludeConditions.Add(condition);
                    foreach (var (_, prevCondition) in range(prevConditions)) {
                        if (excludeConditions == nil) {
                            excludeConditions = Ꮡ(new collections.Set<@string>());
                        }
                        excludeConditions.Add(prevCondition);
                    }
                }

                prevConditions = append(prevConditions, condition);
                ᏑloadEntrypointsFromTargetExports.ValueSlot(subpath, newIncludeConditions, excludeConditions, export);
                if (conditionAlwaysMatches) {
                    break;
                }
            }
        }
    };

    switch (exports.Type) {
    case var ᴛ when ᴛ == packagejson.JSONValueTypeArray: {
        foreach (var (_, element) in range(exports.AsArray())) {
            loadEntrypointsFromTargetExports(".", nil, nil, element);
        }
        break;
    }
    case var ᴛ when ᴛ == packagejson.JSONValueTypeObject: {
        if (exports.IsSubpaths()) {
            foreach (var (subpath, export) in range<@string, packagejson.ExportsOrImports>(exports.AsObject().Entries().Invoke)) {
                loadEntrypointsFromTargetExports(subpath, nil, nil, export);
            }
        } else {
            loadEntrypointsFromTargetExports(".", nil, nil, exports);
        }
        break;
    }
    default: {
        loadEntrypointsFromTargetExports(".", nil, nil, exports);
        break;
    }}

    return entrypoints;
}

internal static (@string, bool) getMatchedStarForPatternEntrypoint(this ref resolutionState r, @string @file, @string leadingSlice, @string trailingSlice, bool caseSensitive) {
    if (stringutil.HasPrefixAndSuffixWithoutOverlap(@file, leadingSlice, trailingSlice, caseSensitive)) {
        return (@file.slice(len(leadingSlice), len(@file) - len(trailingSlice)), true);
    }

    @string jsExtension = TryGetJSExtensionForFile(@file, r.compilerOptions); if (len(jsExtension) > 0) {
        @string swapped = tspath.ChangeFullExtension(@file, jsExtension);
        if (stringutil.HasPrefixAndSuffixWithoutOverlap(swapped, leadingSlice, trailingSlice, caseSensitive)) {
            return (swapped.slice(len(leadingSlice), len(swapped) - len(trailingSlice)), true);
        }
    }

    return ("", false);
}

} // end moduleᴛresolutionState
