namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using regexp = regexp_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using packagejson = go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;

partial class modulespecifiers_package {

partial struct regexPatternCacheKey {
    internal @string pattern;
    internal bool caseInsensitive;
}

internal static readonly ж<sync.RWMutex> ᏑregexPatternCacheMu = new StaticBox<sync.RWMutex>(default(sync.RWMutex));
internal static ref sync.RWMutex regexPatternCacheMu => ref ((StaticBox<sync.RWMutex>)ᏑregexPatternCacheMu).ValueSlot;
internal static map<regexPatternCacheKey, ж<regexp.Regexp>> regexPatternCache = new map<regexPatternCacheKey, ж<regexp.Regexp>>();

internal static nint comparePathsByRedirect(ModulePath a, ModulePath b, bool useCaseSensitiveFileNames) {
    {
        nint c = core.CompareBooleans(b.IsRedirect, a.IsRedirect); if (c != 0) {
            return c;
        }
    }
    {
        nint c = tspath.CompareNumberOfDirectorySeparators(a.FileName, b.FileName); if (c != 0) {
            return c;
        }
    }
    return tspath.ComparePaths(a.FileName, b.FileName, new tspath.ComparePathsOptions(UseCaseSensitiveFileNames: useCaseSensitiveFileNames));
}

public static bool PathIsBareSpecifier(@string path) {
    return !tspath.PathIsAbsolute(path) && !tspath.PathIsRelative(path);
}

public static bool IsExcludedByRegex(@string moduleSpecifier, slice<@string> excludes) {
    foreach (var (_, pattern) in range(excludes)) {
        var re = stringToRegex(pattern);
        if (re == nil) {
            continue;
        }
        if (re.MatchString(moduleSpecifier)) {
            return true;
        }
    }
    return false;
}

internal static ж<regexp.Regexp> stringToRegex(@string pattern) {
    GoFrame ᒐ = default;
    try {
        var caseInsensitive = false;

        if (len(pattern) > 2 && pattern[0] == '/') {
            nint lastSlash = strings.LastIndex(pattern, "/");
            if (lastSlash > 0) {
                var hasUnescapedMiddleSlash = false;
                for (nint i = 1; i < lastSlash; i++) {
                    if (pattern[i] == '/' && (i == 0 || pattern[i - 1] != '\\')) {
                        hasUnescapedMiddleSlash = true;
                        break;
                    }
                }

                if (!hasUnescapedMiddleSlash) {
                    @string flags = pattern.slice(lastSlash + 1);
                    pattern = pattern.slice(1, lastSlash);

                    foreach (var (_, flag) in flags) {
                        switch (flag) {
                        case 'i': {
                            caseInsensitive = true;
                            break;
                        }}

                    }
                }
            }
        }
        var key = new regexPatternCacheKey(pattern, caseInsensitive);

        ᏑregexPatternCacheMu.RLock();
        var (re, ok) = regexPatternCache[key, ꟷ];
        ᏑregexPatternCacheMu.RUnlock();
        if (ok) {
            return re;
        }

        ᏑregexPatternCacheMu.Lock();
        defer(ᏑregexPatternCacheMu.Unlock, ref ᒐ);

        (re, ok) = regexPatternCache[key, ꟷ];
        if (ok) {
            return re;
        }

        if (len(regexPatternCache) > 1000) {
            clear(regexPatternCache);
        }

        pattern = strings.Clone(pattern);
        key.pattern = pattern;

        @string compilePattern = pattern;
        if (caseInsensitive) {
            compilePattern = "(?i:"u8 + pattern + ")"u8;
        }

        var (compiled, err) = regexp.Compile(compilePattern);
        if (err != nil) {
            regexPatternCache[key] = default;
            return default;
        }
        regexPatternCache[key] = compiled;
        return compiled;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static @string ensurePathIsNonModuleName(@string path) {
    if (PathIsBareSpecifier(path)) {
        return "./"u8 + path;
    }
    return path;
}

public static @string GetJSExtensionForDeclarationFileExtension(@string ext) {
    switch (ᐧ) {
    case {} when ext == tspath.ExtensionDts: {
        return tspath.ExtensionJs;
    }
    case {} when ext == tspath.ExtensionDmts: {
        return tspath.ExtensionMjs;
    }
    case {} when ext == tspath.ExtensionDcts: {
        return tspath.ExtensionCjs;
    }
    default: {
        return ext.slice(len(".d"), len(ext) - len(tspath.ExtensionTs));
    }}

}

public static @string TryGetRealFileNameForNonJSDeclarationFileName(@string fileName) {
    @string baseName = tspath.GetBaseFileName(fileName);
    if (!strings.HasSuffix(fileName, tspath.ExtensionTs) || !strings.Contains(baseName, ".d.") || strings.HasSuffix(baseName, tspath.ExtensionDts)) {
        return "";
    }
    @string noExtension = tspath.RemoveExtension(fileName, tspath.ExtensionTs);
    nint lastDotIndex = strings.LastIndex(noExtension, ".");
    @string ext = noExtension.slice(lastDotIndex);
    var (before, _, _) = strings.Cut(noExtension, ".d.");
    return before + ext;
}

internal static @string getJSExtensionForFile(@string fileName, ж<core.CompilerOptions> options) {
    @string result = module.TryGetJSExtensionForFile(fileName, options);
    if (len(result) == 0) {
        throw panic(fmt.Sprintf("Extension %s is unsupported:: FileName:: %s"u8, extensionFromPath(fileName), fileName));
    }
    return result;
}

internal static @string extensionFromPath(@string path) {
    @string ext = tspath.TryGetExtensionFromPath(path);
    if (len(ext) == 0) {
        throw panic(fmt.Sprintf("File %s has unknown extension."u8, path));
    }
    return ext;
}

internal static bool tryGetAnyFileFromPath(ModuleSpecifierGenerationHost host, @string path) {
    var extGroups = tsoptions.GetSupportedExtensions(
        Ꮡ(new core.CompilerOptions(
            AllowJs: core.TSTrue
        )),
        [".node", ".json"]);
    foreach (var (_, exts) in range(extGroups)) {
        foreach (var (_, e) in range(exts)) {
            @string fullPath = path + e;
            if (host.FileExists(tspath.GetNormalizedAbsolutePath(fullPath, host.GetCurrentDirectory()))) {
                return true;
            }
        }
    }
    return false;
}

internal static slice<@string> getPathsRelativeToRootDirs(@string path, slice<@string> rootDirs, bool useCaseSensitiveFileNames) {
    var resultsᴷ = new stackAppend<@string>(moves: true);

    slice<@string> results = default;
    foreach (var (_, rootDir) in range(rootDirs)) {
        @string relativePath = getRelativePathIfInSameVolume(path, rootDir, useCaseSensitiveFileNames);
        if (!isPathRelativeToParent(relativePath)) {
            results = append(resultsᴷ, results, relativePath);
        }
    }
    results = move2heap(resultsᴷ, results);
    return results;
}

internal static bool isPathRelativeToParent(@string path) {
    return strings.HasPrefix(path, "..");
}

internal static @string getRelativePathIfInSameVolume(@string path, @string directoryPath, bool useCaseSensitiveFileNames) {
    @string relativePath = tspath.GetRelativePathToDirectoryOrUrl(directoryPath, path, false, new tspath.ComparePathsOptions(
        UseCaseSensitiveFileNames: useCaseSensitiveFileNames,
        CurrentDirectory: directoryPath
    ));
    if (tspath.IsRootedDiskPath(relativePath)) {
        return "";
    }
    return relativePath;
}

internal static bool packageJsonPathsAreEqual(@string a, @string b, tspath.ComparePathsOptions options) {
    if (a == b) {
        return true;
    }
    if (len(a) == 0 || len(b) == 0) {
        return false;
    }
    return tspath.ComparePaths(a, b, options) == 0;
}

internal static bool prefersTsExtension(slice<ModuleSpecifierEnding> allowedEndings) {
    nint jsPriority = slices.Index(allowedEndings, ModuleSpecifierEndingJsExtension);
    nint tsPriority = slices.Index(allowedEndings, ModuleSpecifierEndingTsExtension);
    if (tsPriority > -1) {
        return tsPriority < jsPriority;
    }
    return false;
}

internal static @string replaceFirstStar(@string s, @string replacement) {
    return strings.Replace(s, "*", replacement, 1);
}

partial struct NodeModulePathParts {
    public nint TopLevelNodeModulesIndex;
    public nint TopLevelPackageNameIndex;
    public nint PackageRootIndex;
    public nint FileNameIndex;
}

partial struct nodeModulesPathParseState /*num:uint8*/;

internal static nodeModulesPathParseState nodeModulesPathParseStateBeforeNodeModules => /* iota */ 0;
internal static nodeModulesPathParseState nodeModulesPathParseStateNodeModules => 1;
internal static nodeModulesPathParseState nodeModulesPathParseStateScope => 2;
internal static nodeModulesPathParseState nodeModulesPathParseStatePackageContent => 3;

public static ж<NodeModulePathParts> GetNodeModulePathParts(@string fullPath) {
    ref var topLevelNodeModulesIndex = ref heap<nint>(0, out var ᏑtopLevelNodeModulesIndex);
    ref var topLevelPackageNameIndex = ref heap<nint>(0, out var ᏑtopLevelPackageNameIndex);
    ref var packageRootIndex = ref heap<nint>(0, out var ᏑpackageRootIndex);
    ref var fileNameIndex = ref heap<nint>(0, out var ᏑfileNameIndex);

    nint partStart = 0;
    nint partEnd = 0;
    var state = nodeModulesPathParseStateBeforeNodeModules;

    while (partEnd >= 0) {
        partStart = partEnd;
        partEnd = core.IndexAfter(fullPath, "/", partStart + 1);
        switch (state) {
        case var ᴛ when ᴛ == nodeModulesPathParseStateBeforeNodeModules: {
            if (strings.Index(fullPath.slice(partStart), "/node_modules/") == 0) {
                topLevelNodeModulesIndex = partStart;
                topLevelPackageNameIndex = partEnd;
                state = nodeModulesPathParseStateNodeModules;
            }
            break;
        }
        case var ᴛ when ᴛ == nodeModulesPathParseStateNodeModules || ᴛ == nodeModulesPathParseStateScope: {
            if (state == nodeModulesPathParseStateNodeModules && fullPath[partStart + 1] == '@') {
                state = nodeModulesPathParseStateScope;
            } else {
                packageRootIndex = partEnd;
                state = nodeModulesPathParseStatePackageContent;
            }
            break;
        }
        case var ᴛ when ᴛ == nodeModulesPathParseStatePackageContent: {
            if (strings.Index(fullPath.slice(partStart), "/node_modules/") == 0) {
                state = nodeModulesPathParseStateNodeModules;
            } else {
                state = nodeModulesPathParseStatePackageContent;
            }
            break;
        }}

    }

    fileNameIndex = partStart;

    if (state > nodeModulesPathParseStateNodeModules) {
        return Ꮡ(new NodeModulePathParts(
            TopLevelNodeModulesIndex: topLevelNodeModulesIndex,
            TopLevelPackageNameIndex: topLevelPackageNameIndex,
            PackageRootIndex: packageRootIndex,
            FileNameIndex: fileNameIndex
        ));
    }
    return default;
}

public static @string GetNodeModulesPackageName(ж<core.CompilerOptions> compilerOptions, ж<ast.SourceFile> ᏑimportingSourceFile, @string nodeModulesFileName, ModuleSpecifierGenerationHost host, UserPreferences preferences, ModuleSpecifierOptions options) {
    ref var importingSourceFile = ref ᏑimportingSourceFile.DerefOrNull();

    var info = getInfo(importingSourceFile.FileName(), host);
    var modulePaths = getAllModulePaths(info, nodeModulesFileName, host, ref (compilerOptions).DerefOrNull(), preferences, options);
    foreach (var (_, modulePath) in range(modulePaths)) {
        @string result = tryGetModuleNameAsNodeModule(modulePath, info, new ast_SourceFileжSourceFileForSpecifierGeneration(ᏑimportingSourceFile), host, compilerOptions, preferences, true, options.OverrideImportMode); if (len(result) > 0) {
            return result;
        }
    }
    return "";
}

internal static bool allKeysStartWithDot(ж<collections.OrderedMap<@string, packagejson.ExportsOrImports>> obj) {
    foreach (var k in range<@string>(obj.Keys().Invoke)) {
        if (!strings.HasPrefix(k, ".")) {
            return false;
        }
    }
    return true;
}

public static @string GetPackageNameFromDirectory(@string fileOrDirectoryPath) {
    nint idx = strings.LastIndex(fileOrDirectoryPath, "/node_modules/");
    if (idx == -1) {
        return "";
    }

    @string basename = fileOrDirectoryPath.slice(idx + len("/node_modules/"));
    if (basename[0] == '.') {
        return "";
    }

    nint nextSlash = strings.Index(basename, "/");
    if (nextSlash == -1) {
        return basename;
    }

    if (basename[0] != '@' || nextSlash == len(basename) - 1) {
        return basename.slice(0, nextSlash);
    }

    nint secondSlash = strings.Index(basename.slice(nextSlash + 1), "/");
    if (secondSlash == -1) {
        return basename;
    }

    return basename.slice(0, nextSlash + 1 + secondSlash);
}

public static @string ProcessEntrypointEnding(ж<module.ResolvedEntrypoint> Ꮡentrypoint, UserPreferences prefs, ModuleSpecifierGenerationHost host, ж<core.CompilerOptions> options, SourceFileForSpecifierGeneration importingSourceFile, slice<ModuleSpecifierEnding> allowedEndings) {
    ref var entrypoint = ref Ꮡentrypoint.DerefOrNull();

    @string specifier = entrypoint.ModuleSpecifier;
    if (entrypoint.Ending == module.EndingFixed) {
        return specifier;
    }

    if (len(allowedEndings) == 0) {
        allowedEndings = GetAllowedEndingsInPreferredOrder(
            prefs,
            host,
            options,
            importingSourceFile,
            "",
            host.GetDefaultResolutionModeForFile(importingSourceFile));
    }

    var preferredEnding = allowedEndings[0];

    @string dtsExtension = tspath.GetDeclarationFileExtension(specifier);
    if (dtsExtension != ""u8) {
        switch (preferredEnding) {
        case var ᴛ when ᴛ == ModuleSpecifierEndingTsExtension || ᴛ == ModuleSpecifierEndingJsExtension: {
            @string jsExtension = GetJSExtensionForDeclarationFileExtension(dtsExtension);
            return tspath.ChangeAnyExtension(specifier, jsExtension, [dtsExtension], false);
        }
        case var ᴛ when ᴛ == ModuleSpecifierEndingMinimal || ᴛ == ModuleSpecifierEndingIndex: {
            if (entrypoint.Ending == module.EndingChangeable) {
                if (dtsExtension == tspath.ExtensionDts) {
                    specifier = tspath.RemoveExtension(specifier, dtsExtension);
                    if (preferredEnding == ModuleSpecifierEndingMinimal) {
                        specifier = strings.TrimSuffix(specifier, "/index");
                    }
                    return specifier;
                }
                @string jsExtensionΔ1 = GetJSExtensionForDeclarationFileExtension(dtsExtension);
                return tspath.ChangeAnyExtension(specifier, jsExtensionΔ1, [dtsExtension], false);
            }
            @string jsExtension = GetJSExtensionForDeclarationFileExtension(dtsExtension);
            return tspath.ChangeAnyExtension(specifier, jsExtension, [dtsExtension], false);
        }}

        return specifier;
    }

    if (tspath.FileExtensionIsOneOf(specifier, [tspath.ExtensionTs, tspath.ExtensionTsx, tspath.ExtensionMts, tspath.ExtensionCts])) {
        switch (preferredEnding) {
        case var ᴛ when ᴛ == ModuleSpecifierEndingTsExtension: {
            return specifier;
        }
        case var ᴛ when ᴛ == ModuleSpecifierEndingJsExtension: {
            {
                @string jsExtension = module.TryGetJSExtensionForFile(specifier, options); if (jsExtension != ""u8) {
                    return tspath.RemoveFileExtension(specifier) + jsExtension;
                }
            }
            return specifier;
        }
        case var ᴛ when ᴛ == ModuleSpecifierEndingMinimal || ᴛ == ModuleSpecifierEndingIndex: {
            if (entrypoint.Ending == module.EndingChangeable) {
                specifier = tspath.RemoveFileExtension(specifier);
                if (preferredEnding == ModuleSpecifierEndingMinimal) {
                    specifier = strings.TrimSuffix(specifier, "/index");
                }
                return specifier;
            }
            {
                @string jsExtension = module.TryGetJSExtensionForFile(specifier, options); if (jsExtension != ""u8) {
                    return tspath.RemoveFileExtension(specifier) + jsExtension;
                }
            }
            return specifier;
        }}

        return specifier;
    }

    if (tspath.FileExtensionIsOneOf(specifier, [tspath.ExtensionJs, tspath.ExtensionJsx, tspath.ExtensionMjs, tspath.ExtensionCjs])) {
        switch (preferredEnding) {
        case var ᴛ when ᴛ == ModuleSpecifierEndingTsExtension || ᴛ == ModuleSpecifierEndingJsExtension: {
            return specifier;
        }
        case var ᴛ when ᴛ == ModuleSpecifierEndingMinimal || ᴛ == ModuleSpecifierEndingIndex: {
            if (entrypoint.Ending == module.EndingChangeable) {
                specifier = tspath.RemoveFileExtension(specifier);
                if (preferredEnding == ModuleSpecifierEndingMinimal) {
                    specifier = strings.TrimSuffix(specifier, "/index");
                }
                return specifier;
            }
            return specifier;
        }}

        return specifier;
    }

    return specifier;
}

} // end modulespecifiers_package
