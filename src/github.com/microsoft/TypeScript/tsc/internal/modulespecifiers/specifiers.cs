namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using outputpaths = go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using symlinks = go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;

partial class modulespecifiers_package {

public static ModuleSpecifiersResult GetModuleSpecifiers(ж<ast.Symbol> moduleSymbol, CheckerShape checker, ж<core.CompilerOptions> compilerOptions, SourceFileForSpecifierGeneration importingSourceFile, ModuleSpecifierGenerationHost host, UserPreferences userPreferences, ModuleSpecifierOptions options, bool forAutoImports) {
    return GetModuleSpecifiersWithInfo(
        moduleSymbol,
        checker,
        compilerOptions,
        importingSourceFile,
        host,
        userPreferences,
        options,
        forAutoImports);
}

public static ModuleSpecifiersResult GetModuleSpecifiersWithInfo(ж<ast.Symbol> moduleSymbol, CheckerShape checker, ж<core.CompilerOptions> compilerOptions, SourceFileForSpecifierGeneration importingSourceFile, ModuleSpecifierGenerationHost host, UserPreferences userPreferences, ModuleSpecifierOptions options, bool forAutoImports) {
    var ambient = tryGetModuleNameFromAmbientModule(moduleSymbol, checker);
    if (len(ambient.name) > 0) {
        if (forAutoImports && IsExcludedByRegex(ambient.name, userPreferences.AutoImportSpecifierExcludeRegexes)) {
            return new ModuleSpecifiersResult(Kind: ResultKindAmbient, AmbientModuleSymbol: ambient.symbol);
        }
        return new ModuleSpecifiersResult(Specifiers: [ambient.name], Kind: ResultKindAmbient, AmbientModuleSymbol: ambient.symbol);
    }

    var moduleSourceFile = ast.GetSourceFileOfModule(moduleSymbol);
    if (moduleSourceFile == nil) {
        return new ModuleSpecifiersResult();
    }

    @string moduleFileName = host.GetSourceOfProjectReferenceIfOutputIncluded(new ast.SourceFileжHasFileName(moduleSourceFile));

    var (specifiers, kind) = GetModuleSpecifiersForFileWithInfo(
        importingSourceFile,
        moduleFileName,
        compilerOptions,
        host,
        userPreferences,
        options,
        forAutoImports);
    return new ModuleSpecifiersResult(Specifiers: specifiers, Kind: kind);
}

public static (slice<@string>, ResultKind) GetModuleSpecifiersForFileWithInfo(SourceFileForSpecifierGeneration importingSourceFile, @string moduleFileName, ж<core.CompilerOptions> compilerOptions, ModuleSpecifierGenerationHost host, UserPreferences userPreferences, ModuleSpecifierOptions options, bool forAutoImports) {
    var modulePaths = getAllModulePathsWorker(
        getInfo(host.GetSourceOfProjectReferenceIfOutputIncluded(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile)), host),
        moduleFileName,
        host,
        ref (compilerOptions).DerefOrNull(),
        options);

    return computeModuleSpecifiers(
        modulePaths,
        compilerOptions,
        importingSourceFile,
        host,
        userPreferences,
        options,
        forAutoImports);
}

partial struct ambientModuleInfo {
    internal @string name;
    internal ж<ast.Symbol> symbol;
}

internal static ambientModuleInfo tryGetModuleNameFromAmbientModule(ж<ast.Symbol> ᏑmoduleSymbol, CheckerShape checker) {
    ref var moduleSymbol = ref ᏑmoduleSymbol.DerefOrNull();

    foreach (var (_, decl) in range(moduleSymbol.Declarations)) {
        if (ast.IsModuleWithStringLiteralName(decl) && (!ast.IsModuleAugmentationExternal(decl) || !tspath.IsExternalModuleNameRelative(decl.Name().Text()))) {
            return new ambientModuleInfo(name: decl.Name().Text(), symbol: ᏑmoduleSymbol);
        }
    }

    foreach (var (_, d) in range(moduleSymbol.Declarations)) {
        if (!ast.IsModuleDeclaration(d)) {
            continue;
        }

        var possibleContainer = ast.FindAncestor(d, ast.IsModuleWithStringLiteralName);
        if (possibleContainer == nil || possibleContainer.Parent == nil || !ast.IsSourceFile(possibleContainer.Parent)) {
            continue;
        }

        var (sym, ok) = possibleContainer.Symbol().Exports[ast.InternalSymbolNameExportEquals, ꟷ];
        if (!ok || sym == nil) {
            continue;
        }
        var exportAssignmentDecl = sym.ValueDeclaration;
        if (exportAssignmentDecl == nil || exportAssignmentDecl.Kind != ast.KindExportAssignment) {
            continue;
        }
        var exportSymbol = checker.GetSymbolAtLocation(exportAssignmentDecl.Expression());
        if (exportSymbol == nil) {
            continue;
        }
        if ((ast.SymbolFlags)(exportSymbol.Flags & ast.SymbolFlagsAlias) != 0) {
            exportSymbol = checker.GetAliasedSymbol(exportSymbol);
        }
        if (exportSymbol == d.Symbol()) {
            return new ambientModuleInfo(name: possibleContainer.Name().Text(), symbol: possibleContainer.Symbol());
        }
    }
    return new ambientModuleInfo();
}

partial struct Info {
    public bool UseCaseSensitiveFileNames;
    public @string ImportingSourceFileName;
    public @string SourceDirectory;
}

internal static Info getInfo(@string importingSourceFileName, ModuleSpecifierGenerationHost host) {
    @string sourceDirectory = tspath.GetDirectoryPath(importingSourceFileName);
    return new Info(
        ImportingSourceFileName: importingSourceFileName,
        SourceDirectory: sourceDirectory,
        UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames()
    );
}

internal static slice<ModulePath> getAllModulePaths(Info info, @string importedFileName, ModuleSpecifierGenerationHost host, ref core.CompilerOptions compilerOptions, UserPreferences preferences, ModuleSpecifierOptions options) {
    var modulePaths = getAllModulePathsWorker(info, importedFileName, host, ref compilerOptions, options);
    return modulePaths;
}

internal static slice<ModulePath> getAllModulePathsWorker(Info info, @string importedFileName, ModuleSpecifierGenerationHost host, ref core.CompilerOptions compilerOptions, ModuleSpecifierOptions options) {
    var pathsInDirectoryᴷ = new stackAppend<ModulePath>();

    var allFileNames = new map<@string, ModulePath>();
    var paths = GetEachFileNameOfModule(info.ImportingSourceFileName, importedFileName, host, true);
    foreach (var (_, p) in range(paths)) {
        allFileNames[p.FileName] = p;
    }

    var useCaseSensitiveFileNames = info.UseCaseSensitiveFileNames;
    var comparePaths = (ModulePath a, ModulePath b) => comparePathsByRedirect(a, b, useCaseSensitiveFileNames);

    var sortedPaths = new slice<ModulePath>(0, len(paths));
    for (@string directory = info.SourceDirectory; len(allFileNames) != 0; ) {
        @string directoryStart = tspath.EnsureTrailingDirectorySeparator(directory);
        slice<ModulePath> pathsInDirectory = default;
        foreach (var (fileName, p) in allFileNames) {
            if (strings.HasPrefix(fileName, directoryStart)) {
                pathsInDirectory = append(pathsInDirectoryᴷ, pathsInDirectory, p);
                delete(allFileNames, fileName);
            }
        }
        if (len(pathsInDirectory) > 0) {
            slices.SortFunc(pathsInDirectory, comparePaths);
            sortedPaths = appendꓸꓸꓸ(sortedPaths, pathsInDirectory);
        }
        @string newDirectory = tspath.GetDirectoryPath(directory);
        if (newDirectory == directory) {
            break;
        }
        directory = newDirectory;
    }
    if (len(allFileNames) > 0) {
        var remainingPaths = slices.Collect(maps.Values<map<@string, ModulePath>, @string, ModulePath>(allFileNames));
        slices.SortFunc(remainingPaths, comparePaths);
        sortedPaths = appendꓸꓸꓸ(sortedPaths, remainingPaths);
    }
    return sortedPaths;
}

internal static bool containsIgnoredPath(@string s) {
    return strings.Contains(s, "/node_modules/.") || strings.Contains(s, "/.git") || strings.Contains(s, ".#");
}

public static bool ContainsNodeModules(@string s) {
    return strings.Contains(s, "/node_modules/");
}

public static slice<ModulePath> GetEachFileNameOfModule(@string importingFileName, @string importedFileName, ModuleSpecifierGenerationHost host, bool preferSymlinks) {
    var importedFileNamesᴷ = new stackAppend<@string>();

    @string cwd = host.GetCurrentDirectory();
    tspath.Path importedPath = tspath.ToPath(importedFileName, cwd, host.UseCaseSensitiveFileNames());
    @string referenceRedirect = default;
    var outputAndReference = host.GetProjectReferenceFromSource(importedPath);
    if (outputAndReference != nil && outputAndReference.OutputDts != ""u8) {
        referenceRedirect = outputAndReference.OutputDts;
    }

    var redirects = host.GetRedirectTargets(importedPath);
    var importedFileNames = new slice<@string>(0, 2 + len(redirects));
    if (len(referenceRedirect) > 0) {
        importedFileNames = append(importedFileNamesᴷ, importedFileNames, referenceRedirect);
    }
    importedFileNames = append(importedFileNames, importedFileName);
    importedFileNames = appendꓸꓸꓸ(importedFileNames, redirects);
    var targets = core.Map(importedFileNames, @string (@string f) => tspath.GetNormalizedAbsolutePath(f, cwd));
    var shouldFilterIgnoredPaths = !core.Every<@string>(targets, containsIgnoredPath);

    ref var results = ref heap<slice<ModulePath>>(new slice<ModulePath>(0, 2), out var Ꮡresults);
    if (!preferSymlinks) {
        foreach (var (_, p) in range(targets)) {
            if (!(shouldFilterIgnoredPaths && containsIgnoredPath(p))) {
                results = append(results, new ModulePath(
                    FileName: p,
                    IsInNodeModules: ContainsNodeModules(p),
                    IsRedirect: referenceRedirect == p
                ));
            }
        }
    }

    var symlinkCache = host.GetSymlinkCache();
    @string fullImportedFileName = tspath.GetNormalizedAbsolutePath(importedFileName, cwd);
    if (symlinkCache != nil) {
            var symlinkCacheʗ1 = symlinkCache;
            var targetsʗ1 = targets;
        tspath.ForEachAncestorDirectoryStoppingAtGlobalCache(
            host.GetGlobalTypingsCacheLocation(),
            tspath.GetDirectoryPath(fullImportedFileName),
            (bool, bool) (@string realPathDirectory) => {
                var (symlinkSet, ok) = symlinkCacheʗ1.DirectoriesByRealpath().Load(tspath.ToPath(realPathDirectory, cwd, host.UseCaseSensitiveFileNames()).EnsureTrailingDirectorySeparator());
                if (!ok) {
                    return (false, false);
                }

                if (tspath.StartsWithDirectory(importingFileName, realPathDirectory, host.UseCaseSensitiveFileNames())) {
                    return (false, true);
                }

                foreach (var (_, target) in range(targetsʗ1)) {
                    if (!tspath.StartsWithDirectory(target, realPathDirectory, host.UseCaseSensitiveFileNames())) {
                        continue;
                    }

                    @string relative = tspath.GetRelativePathFromDirectory(
                        realPathDirectory,
                        target,
                        new tspath.ComparePathsOptions(
                            UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
                            CurrentDirectory: cwd
                        ));
                    symlinkSet.Range((@string symlinkDirectory) => {
                        @string option = tspath.ResolvePath(symlinkDirectory, relative);
                        Ꮡresults.ValueSlot = append(Ꮡresults.ValueSlot, new ModulePath(
                            FileName: option,
                            IsInNodeModules: ContainsNodeModules(option),
                            IsRedirect: target == referenceRedirect
                        ));
                        shouldFilterIgnoredPaths = true;
                        return true;
                    });
                }

                return (false, false);
            });
    }

    if (preferSymlinks) {
        foreach (var (_, p) in range(targets)) {
            if (!(shouldFilterIgnoredPaths && containsIgnoredPath(p))) {
                results = append(results, new ModulePath(
                    FileName: p,
                    IsInNodeModules: ContainsNodeModules(p),
                    IsRedirect: referenceRedirect == p
                ));
            }
        }
    }

    return results;
}

internal static (slice<@string>, ResultKind) computeModuleSpecifiers(slice<ModulePath> modulePaths, ж<core.CompilerOptions> compilerOptions, SourceFileForSpecifierGeneration importingSourceFile, ModuleSpecifierGenerationHost host, UserPreferences userPreferences, ModuleSpecifierOptions options, bool forAutoImport) {
    var redirectPathsSpecifiersᴷ = new stackAppend<@string>(moves: true);
    var pathsSpecifiersᴷ = new stackAppend<@string>(moves: true);

    var info = getInfo(importingSourceFile.FileName(), host);
    var preferences = getModuleSpecifierPreferences(userPreferences, host, compilerOptions, importingSourceFile, "");

    @string existingSpecifier = default;
    foreach (var (_, modulePath) in range(modulePaths)) {
        tspath.Path targetPath = tspath.ToPath(modulePath.FileName, host.GetCurrentDirectory(), info.UseCaseSensitiveFileNames);
        ж<astꓸStringLiteralLike> existingImport = default;
        foreach (var (_, importSpecifier) in range(importingSourceFile.Imports())) {
            var resolvedModule = host.GetResolvedModuleFromModuleSpecifier(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile), importSpecifier);
            if (resolvedModule.IsResolved() && tspath.ToPath(resolvedModule.ResolvedFileName, host.GetCurrentDirectory(), info.UseCaseSensitiveFileNames) == targetPath) {
                existingImport = importSpecifier;
                break;
            }
        }
        if (existingImport != nil) {
            if (preferences.relativePreference == RelativePreferenceNonRelative && tspath.PathIsRelative(existingImport.Text())) {
                continue;
            }
            var existingMode = host.GetModeForUsageLocation(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile), existingImport);
            var targetMode = options.OverrideImportMode;
            if (targetMode == core.ResolutionModeNone) {
                targetMode = host.GetDefaultResolutionModeForFile(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile));
            }
            if (existingMode != targetMode && existingMode != core.ResolutionModeNone && targetMode != core.ResolutionModeNone) {
                continue;
            }
            existingSpecifier = existingImport.Text();
            break;
        }
    }

    if (existingSpecifier != ""u8) {
        return (slice<@string>([existingSpecifier]), ResultKindNone);
    }

    var importedFileIsInNodeModules = core.Some(modulePaths, (ModulePath p) => p.IsInNodeModules);

    slice<@string> pathsSpecifiers = default;
    slice<@string> redirectPathsSpecifiers = default;
    slice<@string> nodeModulesSpecifiers = default;
    slice<@string> relativeSpecifiers = default;

    foreach (var (_, modulePath) in range(modulePaths)) {
        @string specifier = default;
        if (modulePath.IsInNodeModules) {
            specifier = tryGetModuleNameAsNodeModule(modulePath, info, importingSourceFile, host, compilerOptions, userPreferences, false, options.OverrideImportMode);
        }
        if (len(specifier) > 0 && !(forAutoImport && IsExcludedByRegex(specifier, preferences.excludeRegexes))) {
            nodeModulesSpecifiers = append(nodeModulesSpecifiers, specifier);
            if (modulePath.IsRedirect) {
                return (nodeModulesSpecifiers, ResultKindNodeModules);
            }
        }

        var importMode = options.OverrideImportMode;
        if (importMode == core.ResolutionModeNone) {
            importMode = host.GetDefaultResolutionModeForFile(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile));
        }
        @string local = getLocalModuleSpecifier(
            modulePath.FileName,
            info,
            compilerOptions,
            host,
            importMode,
            preferences,
            modulePath.IsRedirect || len(specifier) > 0);
        if (len(local) == 0 || forAutoImport && IsExcludedByRegex(local, preferences.excludeRegexes)) {
            continue;
        }
        if (modulePath.IsRedirect) {
            redirectPathsSpecifiers = append(redirectPathsSpecifiersᴷ, redirectPathsSpecifiers, local);
        } else if (PathIsBareSpecifier(local)) {
            if (ContainsNodeModules(local)) {
                relativeSpecifiers = append(relativeSpecifiers, local);
            } else {
                pathsSpecifiers = append(pathsSpecifiersᴷ, pathsSpecifiers, local);
            }
        } else if (forAutoImport || !importedFileIsInNodeModules || modulePath.IsInNodeModules) {
            relativeSpecifiers = append(relativeSpecifiers, local);
        }
    }

    if (len(pathsSpecifiers) > 0) {
        pathsSpecifiers = move2heap(pathsSpecifiersᴷ, pathsSpecifiers);
        return (pathsSpecifiers, ResultKindPaths);
    }
    if (len(redirectPathsSpecifiers) > 0) {
        redirectPathsSpecifiers = move2heap(redirectPathsSpecifiersᴷ, redirectPathsSpecifiers);
        return (redirectPathsSpecifiers, ResultKindRedirect);
    }
    if (len(nodeModulesSpecifiers) > 0) {
        return (nodeModulesSpecifiers, ResultKindNodeModules);
    }
    return (relativeSpecifiers, ResultKindRelative);
}

internal static @string getLocalModuleSpecifier(@string moduleFileName, Info info, ж<core.CompilerOptions> ᏑcompilerOptions, ModuleSpecifierGenerationHost host, coreꓸResolutionMode importMode, ModuleSpecifierPreferences preferences, bool pathsOnly) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    var paths = compilerOptions.Paths;
    var rootDirs = compilerOptions.RootDirs;

    if (pathsOnly && paths == nil) {
        return "";
    }

    @string sourceDirectory = info.SourceDirectory;

    var allowedEndings = preferences.getAllowedEndingsInPreferredOrder(importMode);
    @string relativePath = default;
    if (len(rootDirs) > 0) {
        relativePath = tryGetModuleNameFromRootDirs(rootDirs, moduleFileName, sourceDirectory, allowedEndings, ᏑcompilerOptions, host);
    }
    if (len(relativePath) == 0) {
        relativePath = processEnding(ensurePathIsNonModuleName(tspath.GetRelativePathFromDirectory(sourceDirectory, moduleFileName, new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
            CurrentDirectory: host.GetCurrentDirectory()
        ))), allowedEndings, ᏑcompilerOptions, host);
    }

    if ((paths == nil && !compilerOptions.GetResolvePackageJsonImports()) || preferences.relativePreference == RelativePreferenceRelative) {
        if (pathsOnly) {
            return "";
        }
        return relativePath;
    }

    @string root = compilerOptions.GetPathsBasePath(host.GetCurrentDirectory());
    @string baseDirectory = tspath.GetNormalizedAbsolutePath(root, host.GetCurrentDirectory());
    @string relativeToBaseUrl = getRelativePathIfInSameVolume(moduleFileName, baseDirectory, host.UseCaseSensitiveFileNames());
    if (len(relativeToBaseUrl) == 0) {
        if (pathsOnly) {
            return "";
        }
        return relativePath;
    }

    @string fromPackageJsonImports = default;
    if (!pathsOnly) {
        fromPackageJsonImports = tryGetModuleNameFromPackageJsonImports(
            moduleFileName,
            sourceDirectory,
            ᏑcompilerOptions,
            host,
            importMode,
            prefersTsExtension(allowedEndings));
    }

    @string fromPaths = default;
    if ((pathsOnly || len(fromPackageJsonImports) == 0) && paths != nil) {
        fromPaths = tryGetModuleNameFromPaths(
            relativeToBaseUrl,
            paths,
            allowedEndings,
            baseDirectory,
            host,
            ᏑcompilerOptions);
    }

    if (pathsOnly) {
        return fromPaths;
    }

    @string maybeNonRelative = default;
    if (len(fromPackageJsonImports) > 0) {
        maybeNonRelative = fromPackageJsonImports;
    } else {
        maybeNonRelative = fromPaths;
    }
    if (len(maybeNonRelative) == 0) {
        return relativePath;
    }

    var relativeIsExcluded = IsExcludedByRegex(relativePath, preferences.excludeRegexes);
    var nonRelativeIsExcluded = IsExcludedByRegex(maybeNonRelative, preferences.excludeRegexes);
    if (!relativeIsExcluded && nonRelativeIsExcluded) {
        return relativePath;
    }
    if (relativeIsExcluded && !nonRelativeIsExcluded) {
        return maybeNonRelative;
    }

    if (preferences.relativePreference == RelativePreferenceNonRelative && !tspath.PathIsRelative(maybeNonRelative)) {
        return maybeNonRelative;
    }

    if (preferences.relativePreference == RelativePreferenceExternalNonRelative && !tspath.PathIsRelative(maybeNonRelative)) {
        tspath.Path projectDirectory = default;
        if (len(compilerOptions.ConfigFilePath) > 0) {
            projectDirectory = tspath.ToPath(tspath.GetDirectoryPath(compilerOptions.ConfigFilePath), host.GetCurrentDirectory(), host.UseCaseSensitiveFileNames());
        } else {
            projectDirectory = tspath.ToPath(host.GetCurrentDirectory(), host.GetCurrentDirectory(), host.UseCaseSensitiveFileNames());
        }
        tspath.Path canonicalSourceDirectory = tspath.ToPath(sourceDirectory, host.GetCurrentDirectory(), host.UseCaseSensitiveFileNames());
        tspath.Path modulePath = tspath.ToPath(moduleFileName, (@string)projectDirectory, host.UseCaseSensitiveFileNames());

        var sourceIsInternal = projectDirectory.ContainsPath(canonicalSourceDirectory);
        var targetIsInternal = projectDirectory.ContainsPath(modulePath);
        if (sourceIsInternal && !targetIsInternal || !sourceIsInternal && targetIsInternal) {
            return maybeNonRelative;
        }

        @string nearestTargetPackageJson = host.GetNearestAncestorDirectoryWithPackageJson(tspath.GetDirectoryPath((@string)modulePath));
        @string nearestSourcePackageJson = host.GetNearestAncestorDirectoryWithPackageJson(sourceDirectory);

        if (!packageJsonPathsAreEqual(nearestTargetPackageJson, nearestSourcePackageJson, new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
            CurrentDirectory: host.GetCurrentDirectory()
        ))) {
            return maybeNonRelative;
        }

        return relativePath;
    }

    if (isPathRelativeToParent(maybeNonRelative) || CountPathComponents(relativePath) < CountPathComponents(maybeNonRelative)) {
        return relativePath;
    }
    return maybeNonRelative;
}

internal static @string processEnding(@string fileName, slice<ModuleSpecifierEnding> allowedEndings, ж<core.CompilerOptions> options, ModuleSpecifierGenerationHost host) {
    if (tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionJson, tspath.ExtensionMjs, tspath.ExtensionCjs])) {
        return fileName;
    }

    @string noExtension = tspath.RemoveFileExtension(fileName);
    if (fileName == noExtension) {
        return fileName;
    }

    nint jsPriority = slices.Index(allowedEndings, ModuleSpecifierEndingJsExtension);
    nint tsPriority = slices.Index(allowedEndings, ModuleSpecifierEndingTsExtension);
    if (tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionMts, tspath.ExtensionCts]) && tsPriority != -1 && tsPriority < jsPriority) {
        return fileName;
    }
    if (tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionDmts, tspath.ExtensionDcts])) {
        @string inputExt = tspath.GetDeclarationFileExtension(fileName);
        @string ext = GetJSExtensionForDeclarationFileExtension(inputExt);
        return tspath.RemoveExtension(fileName, inputExt) + ext;
    }
    if (tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionMts, tspath.ExtensionCts])) {
        return noExtension + getJSExtensionForFile(fileName, options);
    }
    if (!tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionDts]) && tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionTs]) && strings.Contains(fileName, ".d.")) {
        @string result = TryGetRealFileNameForNonJSDeclarationFileName(fileName); if (result != ""u8) {
            return result;
        }
    }

    switch (allowedEndings[0]) {
    case var ᴛ when ᴛ == ModuleSpecifierEndingMinimal: {
        @string withoutIndex = strings.TrimSuffix(noExtension, "/index");
        if (host != nil && withoutIndex != noExtension && tryGetAnyFileFromPath(host, withoutIndex)) {
            return noExtension;
        }
        return withoutIndex;
    }
    case var ᴛ when ᴛ == ModuleSpecifierEndingIndex: {
        return noExtension;
    }
    case var ᴛ when ᴛ == ModuleSpecifierEndingJsExtension: {
        return noExtension + getJSExtensionForFile(fileName, options);
    }
    case var ᴛ when ᴛ == ModuleSpecifierEndingTsExtension: {
        if (tspath.IsDeclarationFileName(fileName)) {
            nint extensionlessPriority = -1;
            foreach (var (i, e) in range(allowedEndings)) {
                if (e == ModuleSpecifierEndingMinimal || e == ModuleSpecifierEndingIndex) {
                    extensionlessPriority = i;
                    break;
                }
            }
            if (extensionlessPriority != -1 && extensionlessPriority < jsPriority) {
                return noExtension;
            }
            return noExtension + getJSExtensionForFile(fileName, options);
        }
        return fileName;
    }
    default: {
        debug.AssertNever(allowedEndings[0]);
        return "";
    }}

}

internal static @string tryGetModuleNameFromRootDirs(slice<@string> rootDirs, @string moduleFileName, @string sourceDirectory, slice<ModuleSpecifierEnding> allowedEndings, ж<core.CompilerOptions> compilerOptions, ModuleSpecifierGenerationHost host) {
    var normalizedTargetPaths = getPathsRelativeToRootDirs(moduleFileName, rootDirs, host.UseCaseSensitiveFileNames());
    if (len(normalizedTargetPaths) == 0) {
        return "";
    }

    var normalizedSourcePaths = getPathsRelativeToRootDirs(sourceDirectory, rootDirs, host.UseCaseSensitiveFileNames());
    @string shortest = default;
    nint shortestSepCount = default;
    foreach (var (_, sourcePath) in range(normalizedSourcePaths)) {
        foreach (var (_, targetPath) in range(normalizedTargetPaths)) {
            @string candidate = ensurePathIsNonModuleName(tspath.GetRelativePathFromDirectory(sourcePath, targetPath, new tspath.ComparePathsOptions(
                UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
                CurrentDirectory: host.GetCurrentDirectory()
            )));
            nint candidateSepCount = strings.Count(candidate, "/");
            if (len(shortest) == 0 || candidateSepCount < shortestSepCount) {
                shortest = candidate;
                shortestSepCount = candidateSepCount;
            }
        }
    }

    if (len(shortest) == 0) {
        return "";
    }
    return processEnding(shortest, allowedEndings, compilerOptions, host);
}

internal static @string tryGetModuleNameAsNodeModule(ModulePath pathObj, Info info, SourceFileForSpecifierGeneration importingSourceFile, ModuleSpecifierGenerationHost host, ж<core.CompilerOptions> options, UserPreferences userPreferences, bool packageNameOnly, coreꓸResolutionMode overrideMode) {
    var parts = GetNodeModulePathParts(pathObj.FileName);
    if (parts == nil) {
        return "";
    }

    var preferences = getModuleSpecifierPreferences(userPreferences, host, options, importingSourceFile, "");
    var allowedEndings = preferences.getAllowedEndingsInPreferredOrder(core.ResolutionModeNone);

    var caseSensitive = host.UseCaseSensitiveFileNames();
    @string moduleSpecifier = pathObj.FileName;
    var isPackageRootPath = false;
    if (!packageNameOnly) {
        nint packageRootIndex = parts.PackageRootIndex;
        @string moduleFileName = default;
        while (true) {
            var pkgJsonResults = tryDirectoryWithPackageJson(
                parts.Value,
                pathObj,
                importingSourceFile,
                host,
                overrideMode,
                options,
                allowedEndings);
            @string moduleFileToTry = pkgJsonResults.moduleFileToTry;
            @string packageRootPath = pkgJsonResults.packageRootPath;
            var blockedByExports = pkgJsonResults.blockedByExports;
            var verbatimFromExports = pkgJsonResults.verbatimFromExports;
            if (blockedByExports) {
                return "";
            }
            if (verbatimFromExports) {
                return moduleFileToTry;
            }
            if (len(packageRootPath) > 0) {
                moduleSpecifier = packageRootPath;
                isPackageRootPath = true;
                break;
            }
            if (len(moduleFileName) == 0) {
                moduleFileName = moduleFileToTry;
            }
            packageRootIndex = core.IndexAfter(pathObj.FileName, "/", packageRootIndex + 1);
            if (packageRootIndex == -1) {
                moduleSpecifier = processEnding(moduleFileName, allowedEndings, options, host);
                break;
            }
        }
    }

    if (pathObj.IsRedirect && !isPackageRootPath) {
        return "";
    }

    @string globalTypingsCacheLocation = host.GetGlobalTypingsCacheLocation();
    @string pathToTopLevelNodeModules = moduleSpecifier.slice(0, parts.TopLevelNodeModulesIndex);

    if (!stringutil.HasPrefix(info.SourceDirectory, pathToTopLevelNodeModules, caseSensitive) || len(globalTypingsCacheLocation) > 0 && stringutil.HasPrefix(globalTypingsCacheLocation, pathToTopLevelNodeModules, caseSensitive)) {
        return "";
    }

    @string nodeModulesDirectoryName = moduleSpecifier.slice(parts.TopLevelPackageNameIndex + 1);
    return module.GetPackageNameFromTypesPackageName(nodeModulesDirectoryName);
}

partial struct pkgJsonDirAttemptResult {
    internal @string moduleFileToTry;
    internal @string packageRootPath;
    internal bool blockedByExports;
    internal bool verbatimFromExports;
}

internal static pkgJsonDirAttemptResult tryDirectoryWithPackageJson(NodeModulePathParts parts, ModulePath pathObj, SourceFileForSpecifierGeneration importingSourceFile, ModuleSpecifierGenerationHost host, coreꓸResolutionMode overrideMode, ж<core.CompilerOptions> Ꮡoptions, slice<ModuleSpecifierEnding> allowedEndings) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    nint rootIdx = parts.PackageRootIndex;
    if (rootIdx == -1) {
        rootIdx = len(pathObj.FileName);
    }
    @string packageRootPath = pathObj.FileName.slice(0, rootIdx);
    @string packageJsonPath = tspath.CombinePaths(packageRootPath, "package.json");
    @string moduleFileToTry = pathObj.FileName;
    var maybeBlockedByTypesVersions = false;
    var packageJson = host.GetPackageJsonInfo(packageJsonPath);
    if (packageJson == nil) {
        @string fileName = moduleFileToTry.slice(parts.PackageRootIndex + 1);
        if (fileName == "index.d.ts"u8 || fileName == "index.js"u8 || fileName == "index.ts"u8 || fileName == "index.tsx"u8) {
            return new pkgJsonDirAttemptResult(moduleFileToTry: moduleFileToTry, packageRootPath: packageRootPath);
        } else {
            return new pkgJsonDirAttemptResult(moduleFileToTry: moduleFileToTry);
        }
    }

    var importMode = overrideMode;
    if (importMode == core.ResolutionModeNone) {
        importMode = host.GetDefaultResolutionModeForFile(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile));
    }

    var packageJsonContent = packageJson.GetContents();
    if (options.GetResolvePackageJsonExports()) {
        @string nodeModulesDirectoryName = packageRootPath.slice(parts.TopLevelPackageNameIndex + 1);
        @string packageName = module.GetPackageNameFromTypesPackageName(nodeModulesDirectoryName);

        if (tspath.FileExtensionIsOneOf(pathObj.FileName, [tspath.ExtensionCjs, tspath.ExtensionCts, tspath.ExtensionDcts])) {
            importMode = core.ResolutionModeCommonJS;
        } else if (tspath.FileExtensionIsOneOf(pathObj.FileName, [tspath.ExtensionMjs, tspath.ExtensionMts, tspath.ExtensionDmts])) {
            importMode = core.ResolutionModeESM;
        }

        var conditions = module.GetConditions(Ꮡoptions, importMode);

        @string fromExports = default;
        if (packageJsonContent != nil && packageJsonContent.Fields.Exports.Type != packagejson.JSONValueTypeNotPresent) {
            fromExports = tryGetModuleNameFromExports(
                Ꮡoptions,
                host,
                pathObj.FileName,
                packageRootPath,
                packageName,
                packageJsonContent.Fields.Exports,
                conditions);
        }
        if (len(fromExports) > 0) {
            return new pkgJsonDirAttemptResult(
                moduleFileToTry: fromExports,
                verbatimFromExports: true
            );
        }
        if (packageJsonContent != nil && packageJsonContent.Fields.Exports.Type != packagejson.JSONValueTypeNotPresent) {
            return new pkgJsonDirAttemptResult(
                moduleFileToTry: pathObj.FileName,
                blockedByExports: true
            );
        }
    }

    ref var versionPaths = ref heap(new packagejson.VersionPaths(), out var ᏑversionPaths);
    if (packageJsonContent != nil && packageJsonContent.TypesVersions.Type == packagejson.JSONValueTypeObject) {
        versionPaths = packageJsonContent.GetVersionPaths(default);
    }
    if (ᏑversionPaths.GetPaths() != nil) {
        @string subModuleName = pathObj.FileName.slice(len(packageRootPath) + 1);
        @string fromPaths = tryGetModuleNameFromPaths(
            subModuleName,
            ᏑversionPaths.GetPaths(),
            allowedEndings,
            packageRootPath,
            host,
            Ꮡoptions);
        if (len(fromPaths) == 0) {
            maybeBlockedByTypesVersions = true;
        } else {
            moduleFileToTry = tspath.CombinePaths(packageRootPath, fromPaths);
        }
    }
    @string mainFileRelative = "index.js";
    if (packageJsonContent != nil) {
        if (packageJsonContent.Typings.Valid) {
            mainFileRelative = packageJsonContent.Typings.Value;
        } else if (packageJsonContent.Types.Valid) {
            mainFileRelative = packageJsonContent.Types.Value;
        } else if (packageJsonContent.Main.Valid) {
            mainFileRelative = packageJsonContent.Main.Value;
        }
    }

    if (len(mainFileRelative) > 0 && !(maybeBlockedByTypesVersions && module.MatchPatternOrExact(module.TryParsePatterns(ᏑversionPaths.GetPaths()), mainFileRelative) != new core.Pattern())) {
        tspath.Path mainExportFile = tspath.ToPath(mainFileRelative, packageRootPath, host.UseCaseSensitiveFileNames());
        var compareOpt = new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
            CurrentDirectory: host.GetCurrentDirectory()
        );
        if (tspath.ComparePaths(tspath.RemoveFileExtension((@string)mainExportFile), tspath.RemoveFileExtension(moduleFileToTry), compareOpt) == 0) {
            return new pkgJsonDirAttemptResult(packageRootPath: packageRootPath, moduleFileToTry: moduleFileToTry);
        } else if (packageJsonContent == nil || packageJsonContent.Type.Value != "module"u8 && !tspath.FileExtensionIsOneOf(moduleFileToTry, tspath.ExtensionsNotSupportingExtensionlessResolution) && stringutil.HasPrefix(moduleFileToTry, (@string)mainExportFile, host.UseCaseSensitiveFileNames()) && tspath.ComparePaths(tspath.GetDirectoryPath(moduleFileToTry), tspath.RemoveTrailingDirectorySeparator((@string)mainExportFile), compareOpt) == 0 && tspath.RemoveFileExtension(tspath.GetBaseFileName(moduleFileToTry)) == "index"u8) {
            return new pkgJsonDirAttemptResult(packageRootPath: packageRootPath, moduleFileToTry: moduleFileToTry);
        }
    }

    return new pkgJsonDirAttemptResult(moduleFileToTry: moduleFileToTry);
}

internal static @string tryGetModuleNameFromExports(ж<core.CompilerOptions> options, ModuleSpecifierGenerationHost host, @string targetFilePath, @string packageDirectory, @string packageName, packagejson.ExportsOrImports exports, slice<@string> conditions) {
    if (exports.IsSubpaths()) {
        foreach (var (k, subk) in range<@string, packagejson.ExportsOrImports>(exports.AsObject().Entries().Invoke)) {
            @string subPackageName = tspath.GetNormalizedAbsolutePath(tspath.CombinePaths(packageName, k), "");
            var mode = MatchingModeExact;
            if (strings.HasSuffix(k, "/")) {
                mode = MatchingModeDirectory;
            } else if (strings.Contains(k, "*")) {
                mode = MatchingModePattern;
            }
            @string result = tryGetModuleNameFromExportsOrImports(options, host, targetFilePath, packageDirectory, subPackageName, subk, conditions, mode, false, false);
            if (len(result) > 0) {
                return result;
            }
        }
    }
    return tryGetModuleNameFromExportsOrImports(
        options,
        host,
        targetFilePath,
        packageDirectory,
        packageName,
        exports,
        conditions,
        MatchingModeExact,
        false,
        false);
}

internal static @string tryGetModuleNameFromPackageJsonImports(@string moduleFileName, @string sourceDirectory, ж<core.CompilerOptions> Ꮡoptions, ModuleSpecifierGenerationHost host, coreꓸResolutionMode importMode, bool preferTsExtension) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (!options.GetResolvePackageJsonImports()) {
        return "";
    }

    @string ancestorDirectoryWithPackageJson = host.GetNearestAncestorDirectoryWithPackageJson(sourceDirectory);
    if (len(ancestorDirectoryWithPackageJson) == 0) {
        return "";
    }
    @string packageJsonPath = tspath.CombinePaths(ancestorDirectoryWithPackageJson, "package.json");

    var info = host.GetPackageJsonInfo(packageJsonPath);
    if (info == nil) {
        return "";
    }

    var imports = info.GetContents().Fields.Imports;
    switch (imports.Type) {
    case var ᴛ when ᴛ == packagejson.JSONValueTypeNotPresent || ᴛ == packagejson.JSONValueTypeArray || ᴛ == packagejson.JSONValueTypeString: {
        return "";
    }
    case var ᴛ when ᴛ == packagejson.JSONValueTypeObject: {
        var conditions = module.GetConditions(Ꮡoptions, importMode);
        var top = imports.AsObject();
        var entries = top.Entries();
        foreach (var (k, value) in range<@string, packagejson.ExportsOrImports>(entries.Invoke)) {
            if (k == "#"u8 || k == "#/"u8 || !strings.HasPrefix(k, "#")) {
                continue;
            }
            if (strings.HasPrefix(k, "#/") && options.GetModuleResolutionKind() != core.ModuleResolutionKindNodeNext && options.GetModuleResolutionKind() != core.ModuleResolutionKindBundler) {
                continue;
            }
            var mode = MatchingModeExact;
            if (strings.HasSuffix(k, "/")) {
                mode = MatchingModeDirectory;
            } else if (strings.Contains(k, "*")) {
                mode = MatchingModePattern;
            }
            @string result = tryGetModuleNameFromExportsOrImports(
                Ꮡoptions,
                host,
                moduleFileName,
                ancestorDirectoryWithPackageJson,
                k,
                value,
                conditions,
                mode,
                true,
                preferTsExtension);
            if (len(result) > 0) {
                return result;
            }
        }
        break;
    }}

    return "";
}

partial struct specPair {
    internal ModuleSpecifierEnding ending;
    internal @string value;
}

internal static @string tryGetModuleNameFromPaths(@string relativeToBaseUrl, ж<collections.OrderedMap<@string, slice<@string>>> paths, slice<ModuleSpecifierEnding> allowedEndings, @string baseDirectory, ModuleSpecifierGenerationHost host, ж<core.CompilerOptions> compilerOptions) {
    var candidatesᴷ = new stackAppend<specPair>();

    var caseSensitive = host.UseCaseSensitiveFileNames();
    foreach (var (key, values) in range<@string, slice<@string>>(paths.Entries().Invoke)) {
        foreach (var (_, patternText) in range(values)) {
            @string normalized = tspath.NormalizePath(patternText);
            @string pattern = getRelativePathIfInSameVolume(normalized, baseDirectory, caseSensitive);
            if (len(pattern) == 0) {
                pattern = normalized;
            }
            var (prefix, suffix, ok) = strings.Cut(pattern, "*");

            slice<specPair> candidates = default;
            foreach (var (_, ending) in range(allowedEndings)) {
                @string result = processEnding(
                    relativeToBaseUrl,
                    [ending],
                    compilerOptions,
                    host);
                candidates = append(candidatesᴷ, candidates, new specPair(
                    ending: ending,
                    value: result
                ));
            }
            if (len(tspath.TryGetExtensionFromPath(pattern)) > 0) {
                candidates = append(candidates, new specPair(
                    ending: ModuleSpecifierEndingJsExtension,
                    value: relativeToBaseUrl
                ));
            }

            if (ok) {
                foreach (var (_, c) in range(candidates)) {
                    @string value = c.value;
                    if (len(value) >= len(prefix) + len(suffix) && stringutil.HasPrefix(value, prefix, caseSensitive) && stringutil.HasSuffix(value, suffix, caseSensitive) && validateEnding(c, relativeToBaseUrl, compilerOptions, host)) {
                        @string matchedStar = value.slice(len(prefix), len(value) - len(suffix));
                        if (!tspath.PathIsRelative(matchedStar)) {
                            return replaceFirstStar(key, matchedStar);
                        }
                    }
                }
            } else if (core.Some(candidates, (specPair c) => c.ending != ModuleSpecifierEndingMinimal && pattern == c.value) || core.Some(candidates, (specPair c) => c.ending == ModuleSpecifierEndingMinimal && pattern == c.value && validateEnding(c, relativeToBaseUrl, compilerOptions, host))) {
                return key;
            }
        }
    }
    return "";
}

internal static bool validateEnding(specPair c, @string relativeToBaseUrl, ж<core.CompilerOptions> compilerOptions, ModuleSpecifierGenerationHost host) {
    return c.ending != ModuleSpecifierEndingMinimal || c.value == processEnding(relativeToBaseUrl, [c.ending], compilerOptions, host);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string typesˢ = "types"u8;

internal static @string tryGetModuleNameFromExportsOrImports(ж<core.CompilerOptions> options, ModuleSpecifierGenerationHost host, @string targetFilePath, @string packageDirectory, @string packageName, packagejson.ExportsOrImports exports, slice<@string> conditions, MatchingMode mode, bool isImports, bool preferTsExtension) {
    switch (exports.Type) {
    case var ᴛ when ᴛ == packagejson.JSONValueTypeNotPresent: {
        return "";
    }
    case var ᴛ when ᴛ == packagejson.JSONValueTypeString: {
        @string strValue = exports.Value._<@string>();

        @string outputFile = default;
        @string declarationFile = default;
        if (isImports) {
            outputFile = outputpaths.GetOutputJSFileNameWorker(targetFilePath, options, new ModuleSpecifierGenerationHostᴠOutputPathsHost(host));
            declarationFile = outputpaths.GetOutputDeclarationFileNameWorker(targetFilePath, options, new ModuleSpecifierGenerationHostᴠOutputPathsHost(host));
        }

        @string pathOrPattern = tspath.GetNormalizedAbsolutePath(tspath.CombinePaths(packageDirectory, strValue), "");
        @string extensionSwappedTarget = default;
        if (tspath.HasTSFileExtension(targetFilePath)) {
            extensionSwappedTarget = tspath.RemoveFileExtension(targetFilePath) + module.TryGetJSExtensionForFile(targetFilePath, options);
        }
        var canTryTsExtension = preferTsExtension && tspath.HasImplementationTSFileExtension(targetFilePath);

        var compareOpts = new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
            CurrentDirectory: host.GetCurrentDirectory()
        );

        switch (mode) {
        case var ᴛ2 when ᴛ2 == MatchingModeExact: {
            if (len(extensionSwappedTarget) > 0 && tspath.ComparePaths(extensionSwappedTarget, pathOrPattern, compareOpts) == 0 || tspath.ComparePaths(targetFilePath, pathOrPattern, compareOpts) == 0 || len(outputFile) > 0 && tspath.ComparePaths(outputFile, pathOrPattern, compareOpts) == 0 || len(declarationFile) > 0 && tspath.ComparePaths(declarationFile, pathOrPattern, compareOpts) == 0) {
                return packageName;
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == MatchingModeDirectory: {
            if (canTryTsExtension && tspath.ContainsPath(targetFilePath, pathOrPattern, compareOpts)) {
                @string fragment = tspath.GetRelativePathFromDirectory(pathOrPattern, targetFilePath, compareOpts);
                return tspath.GetNormalizedAbsolutePath(tspath.CombinePaths(tspath.CombinePaths(packageName, strValue), fragment), "");
            }
            if (len(extensionSwappedTarget) > 0 && tspath.ContainsPath(pathOrPattern, extensionSwappedTarget, compareOpts)) {
                @string fragment = tspath.GetRelativePathFromDirectory(pathOrPattern, extensionSwappedTarget, compareOpts);
                return tspath.GetNormalizedAbsolutePath(tspath.CombinePaths(tspath.CombinePaths(packageName, strValue), fragment), "");
            }
            if (!canTryTsExtension && tspath.ContainsPath(pathOrPattern, targetFilePath, compareOpts)) {
                @string fragment = tspath.GetRelativePathFromDirectory(pathOrPattern, targetFilePath, compareOpts);
                return tspath.GetNormalizedAbsolutePath(tspath.CombinePaths(tspath.CombinePaths(packageName, strValue), fragment), "");
            }
            if (len(outputFile) > 0 && tspath.ContainsPath(pathOrPattern, outputFile, compareOpts)) {
                @string fragment = tspath.GetRelativePathFromDirectory(pathOrPattern, outputFile, compareOpts);
                return tspath.CombinePaths(packageName, fragment);
            }
            if (len(declarationFile) > 0 && tspath.ContainsPath(pathOrPattern, declarationFile, compareOpts)) {
                @string fragment = tspath.GetRelativePathFromDirectory(pathOrPattern, declarationFile, compareOpts);
                @string jsExtension = getJSExtensionForFile(declarationFile, options);
                @string fragmentWithJsExtension = tspath.ChangeExtension(fragment, jsExtension);
                return tspath.CombinePaths(packageName, fragmentWithJsExtension);
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == MatchingModePattern: {
            var (leadingSlice, trailingSlice, _) = strings.Cut(pathOrPattern, "*");
            var caseSensitive = host.UseCaseSensitiveFileNames();
            if (canTryTsExtension && stringutil.HasPrefixAndSuffixWithoutOverlap(targetFilePath, leadingSlice, trailingSlice, caseSensitive)) {
                @string starReplacement = targetFilePath.slice(len(leadingSlice), len(targetFilePath) - len(trailingSlice));
                return replaceFirstStar(packageName, starReplacement);
            }
            if (len(extensionSwappedTarget) > 0 && stringutil.HasPrefixAndSuffixWithoutOverlap(extensionSwappedTarget, leadingSlice, trailingSlice, caseSensitive)) {
                @string starReplacement = extensionSwappedTarget.slice(len(leadingSlice), len(extensionSwappedTarget) - len(trailingSlice));
                return replaceFirstStar(packageName, starReplacement);
            }
            if (!canTryTsExtension && stringutil.HasPrefixAndSuffixWithoutOverlap(targetFilePath, leadingSlice, trailingSlice, caseSensitive)) {
                @string starReplacement = targetFilePath.slice(len(leadingSlice), len(targetFilePath) - len(trailingSlice));
                return replaceFirstStar(packageName, starReplacement);
            }
            if (len(outputFile) > 0 && stringutil.HasPrefixAndSuffixWithoutOverlap(outputFile, leadingSlice, trailingSlice, caseSensitive)) {
                @string starReplacement = outputFile.slice(len(leadingSlice), len(outputFile) - len(trailingSlice));
                return replaceFirstStar(packageName, starReplacement);
            }
            if (len(declarationFile) > 0 && stringutil.HasPrefixAndSuffixWithoutOverlap(declarationFile, leadingSlice, trailingSlice, caseSensitive)) {
                @string starReplacement = declarationFile.slice(len(leadingSlice), len(declarationFile) - len(trailingSlice));
                @string substituted = replaceFirstStar(packageName, starReplacement);
                @string jsExtension = module.TryGetJSExtensionForFile(declarationFile, options);
                if (len(jsExtension) > 0) {
                    return tspath.ChangeFullExtension(substituted, jsExtension);
                }
            }
            break;
        }}

        return "";
    }
    case var ᴛ when ᴛ == packagejson.JSONValueTypeArray: {
        var arr = exports.AsArray();
        foreach (var (_, e) in range(arr)) {
            @string result = tryGetModuleNameFromExportsOrImports(options, host, targetFilePath, packageDirectory, packageName, e, conditions, mode, isImports, preferTsExtension);
            if (len(result) > 0) {
                return result;
            }
        }
        break;
    }
    case var ᴛ when ᴛ == packagejson.JSONValueTypeObject: {
        var obj = exports.AsObject();
        foreach (var (key, value) in range<@string, packagejson.ExportsOrImports>(obj.Entries().Invoke)) {
            if (key == "default"u8 || slices.Contains(conditions, key) || slices.Contains(conditions, typesˢ) && module.IsApplicableVersionedTypesKey(key)) {
                @string result = tryGetModuleNameFromExportsOrImports(options, host, targetFilePath, packageDirectory, packageName, value, conditions, mode, isImports, preferTsExtension);
                if (len(result) > 0) {
                    return result;
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == packagejson.JSONValueTypeNull: {
        return "";
    }}

    return "";
}

public static @string GetModuleSpecifier(ж<core.CompilerOptions> compilerOptions, ModuleSpecifierGenerationHost host, ж<ast.SourceFile> importingSourceFile, @string importingSourceFileName, @string oldImportSpecifier, @string toFileName, ModuleSpecifierOptions options) {
    return getModuleSpecifierWithPreferences(
        compilerOptions,
        host,
        importingSourceFile,
        importingSourceFileName,
        oldImportSpecifier,
        toFileName,
        new UserPreferences(),
        options);
}

public static @string UpdateModuleSpecifier(ж<core.CompilerOptions> compilerOptions, ModuleSpecifierGenerationHost host, ж<ast.SourceFile> importingSourceFile, @string importingSourceFileName, @string oldImportSpecifier, @string toFileName, UserPreferences userPreferences, ModuleSpecifierOptions options) {
    return getModuleSpecifierWithPreferences(
        compilerOptions,
        host,
        importingSourceFile,
        importingSourceFileName,
        oldImportSpecifier,
        toFileName,
        userPreferences,
        options);
}

internal static @string getModuleSpecifierWithPreferences(ж<core.CompilerOptions> compilerOptions, ModuleSpecifierGenerationHost host, ж<ast.SourceFile> importingSourceFile, @string importingSourceFileName, @string oldImportSpecifier, @string toFileName, UserPreferences userPreferences, ModuleSpecifierOptions options) {
    var info = getInfo(importingSourceFileName, host);
    var modulePaths = getAllModulePaths(info, toFileName, host, ref (compilerOptions).DerefOrNull(), userPreferences, options);
    var preferences = getModuleSpecifierPreferences(userPreferences, host, compilerOptions, new ast_SourceFileжSourceFileForSpecifierGeneration(importingSourceFile), oldImportSpecifier);

    var resolutionMode = options.OverrideImportMode;
    if (resolutionMode == core.ResolutionModeNone) {
        resolutionMode = host.GetDefaultResolutionModeForFile(new ast.SourceFileжHasFileName(importingSourceFile));
    }

    foreach (var (_, modulePath) in range(modulePaths)) {
        @string firstDefined = tryGetModuleNameAsNodeModule(modulePath, info, new ast_SourceFileжSourceFileForSpecifierGeneration(importingSourceFile), host, compilerOptions, userPreferences, false, options.OverrideImportMode); if (len(firstDefined) > 0) {
            return firstDefined;
        }
    }

    return getLocalModuleSpecifier(toFileName, info, compilerOptions, host, resolutionMode, preferences, false);
}

} // end modulespecifiers_package
