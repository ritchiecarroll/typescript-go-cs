namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class modulespecifiers_package {

internal static bool shouldAllowImportingTsExtension(ж<core.CompilerOptions> ᏑcompilerOptions, @string fromFileName) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    return compilerOptions.GetAllowImportingTsExtensions() || len(fromFileName) > 0 && tspath.IsDeclarationFileName(fromFileName);
}

internal static bool usesExtensionsOnImports(SourceFileForSpecifierGeneration @file) {
    foreach (var (_, @ref) in range(@file.Imports())) {
        @string text = @ref.Text();
        if (tspath.PathIsRelative(text) && !tspath.FileExtensionIsOneOf(text, tspath.ExtensionsNotSupportingExtensionlessResolution)) {
            return tspath.HasTSFileExtension(text) || tspath.HasJSFileExtension(text);
        }
    }
    return false;
}

internal static ModuleSpecifierEnding inferPreference(coreꓸResolutionMode resolutionMode, SourceFileForSpecifierGeneration sourceFile, bool moduleResolutionIsNodeNext) {
    var usesJsExtensions = false;
    slice<ж<astꓸLiteralLikeNode>> specifiers = default;
    if (sourceFile != nil && len(sourceFile.Imports()) > 0) {
        specifiers = sourceFile.Imports();
    } else if (sourceFile != nil && sourceFile.IsJS()) {
    }

    foreach (var (_, specifier) in range(specifiers)) {
        @string path = specifier.Text();
        if (tspath.PathIsRelative(path)) {
            if (moduleResolutionIsNodeNext && resolutionMode == core.ResolutionModeCommonJS) {
                continue;
            }
            if (tspath.FileExtensionIsOneOf(path, tspath.ExtensionsNotSupportingExtensionlessResolution)) {
                continue;
            }
            if (tspath.HasTSFileExtension(path)) {
                return ModuleSpecifierEndingTsExtension;
            }
            if (tspath.HasJSFileExtension(path)) {
                usesJsExtensions = true;
            }
        }
    }

    if (usesJsExtensions) {
        return ModuleSpecifierEndingJsExtension;
    }
    return ModuleSpecifierEndingMinimal;
}

internal static ModuleSpecifierEnding getModuleSpecifierEndingPreference(ImportModuleSpecifierEndingPreference pref, coreꓸResolutionMode resolutionMode, ж<core.CompilerOptions> ᏑcompilerOptions, SourceFileForSpecifierGeneration sourceFile) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    var moduleResolution = compilerOptions.GetModuleResolutionKind();
    var moduleResolutionIsNodeNext = core.ModuleResolutionKindNode16 <= moduleResolution && moduleResolution <= core.ModuleResolutionKindNodeNext;

    if (pref == ImportModuleSpecifierEndingPreferenceJs || resolutionMode == core.ResolutionModeESM && moduleResolutionIsNodeNext) {
        if (!shouldAllowImportingTsExtension(ᏑcompilerOptions, "")) {
            return ModuleSpecifierEndingJsExtension;
        }
        if (inferPreference(resolutionMode, sourceFile, moduleResolutionIsNodeNext) != ModuleSpecifierEndingJsExtension) {
            return ModuleSpecifierEndingTsExtension;
        }
        return ModuleSpecifierEndingJsExtension;
    }

    if (pref == ImportModuleSpecifierEndingPreferenceMinimal) {
        return ModuleSpecifierEndingMinimal;
    }

    if (pref == ImportModuleSpecifierEndingPreferenceIndex) {
        return ModuleSpecifierEndingIndex;
    }

    if (!shouldAllowImportingTsExtension(ᏑcompilerOptions, "")) {
        if (sourceFile != nil && usesExtensionsOnImports(sourceFile)) {
            return ModuleSpecifierEndingJsExtension;
        }
        return ModuleSpecifierEndingMinimal;
    }

    return inferPreference(resolutionMode, sourceFile, moduleResolutionIsNodeNext);
}

internal static ModuleSpecifierEnding getPreferredEnding(UserPreferences prefs, ModuleSpecifierGenerationHost host, ж<core.CompilerOptions> compilerOptions, SourceFileForSpecifierGeneration importingSourceFile, @string oldImportSpecifier, coreꓸResolutionMode resolutionMode) {
    if (len(oldImportSpecifier) > 0) {
        if (tspath.HasJSFileExtension(oldImportSpecifier)) {
            return ModuleSpecifierEndingJsExtension;
        }
        if (strings.HasSuffix(oldImportSpecifier, "/index")) {
            return ModuleSpecifierEndingIndex;
        }
    }
    if (resolutionMode == core.ResolutionModeNone) {
        resolutionMode = host.GetDefaultResolutionModeForFile(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile));
    }
    return getModuleSpecifierEndingPreference(
        prefs.ImportModuleSpecifierEnding,
        resolutionMode,
        compilerOptions,
        importingSourceFile);
}

partial struct ModuleSpecifierPreferences {
    internal RelativePreferenceKind relativePreference;
    internal Func<coreꓸResolutionMode, slice<ModuleSpecifierEnding>> getAllowedEndingsInPreferredOrder;
    internal slice<@string> excludeRegexes;
}

public static slice<ModuleSpecifierEnding> GetAllowedEndingsInPreferredOrder(UserPreferences prefs, ModuleSpecifierGenerationHost host, ж<core.CompilerOptions> ᏑcompilerOptions, SourceFileForSpecifierGeneration importingSourceFile, @string oldImportSpecifier, coreꓸResolutionMode syntaxImpliedNodeFormat) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    var preferredEnding = getPreferredEnding(
        prefs,
        host,
        ᏑcompilerOptions,
        importingSourceFile,
        oldImportSpecifier,
        core.ResolutionModeNone);
    var resolutionMode = host.GetDefaultResolutionModeForFile(new SourceFileForSpecifierGenerationᴠHasFileName(importingSourceFile));
    if (resolutionMode != syntaxImpliedNodeFormat) {
        preferredEnding = getPreferredEnding(
            prefs,
            host,
            ᏑcompilerOptions,
            importingSourceFile,
            oldImportSpecifier,
            syntaxImpliedNodeFormat);
    }
    var moduleResolution = compilerOptions.GetModuleResolutionKind();
    var moduleResolutionIsNodeNext = core.ModuleResolutionKindNode16 <= moduleResolution && moduleResolution <= core.ModuleResolutionKindNodeNext;
    var allowImportingTsExtension = shouldAllowImportingTsExtension(ᏑcompilerOptions, importingSourceFile.FileName());
    var effectiveSyntaxMode = syntaxImpliedNodeFormat;
    if (effectiveSyntaxMode == core.ResolutionModeNone) {
        effectiveSyntaxMode = resolutionMode;
    }
    if (effectiveSyntaxMode == core.ResolutionModeESM && moduleResolutionIsNodeNext) {
        if (allowImportingTsExtension) {
            return [ModuleSpecifierEndingTsExtension, ModuleSpecifierEndingJsExtension];
        }
        return [ModuleSpecifierEndingJsExtension];
    }
    switch (preferredEnding) {
    case var ᴛ when ᴛ == ModuleSpecifierEndingJsExtension: {
        if (allowImportingTsExtension) {
            return [ModuleSpecifierEndingJsExtension, ModuleSpecifierEndingTsExtension, ModuleSpecifierEndingMinimal, ModuleSpecifierEndingIndex];
        }
        return [ModuleSpecifierEndingJsExtension, ModuleSpecifierEndingMinimal, ModuleSpecifierEndingIndex];
    }
    case var ᴛ when ᴛ == ModuleSpecifierEndingTsExtension: {
        return [ModuleSpecifierEndingTsExtension, ModuleSpecifierEndingMinimal, ModuleSpecifierEndingJsExtension, ModuleSpecifierEndingIndex];
    }
    case var ᴛ when ᴛ == ModuleSpecifierEndingIndex: {
        if (allowImportingTsExtension) {
            return [ModuleSpecifierEndingIndex, ModuleSpecifierEndingMinimal, ModuleSpecifierEndingTsExtension, ModuleSpecifierEndingJsExtension];
        }
        return [ModuleSpecifierEndingIndex, ModuleSpecifierEndingMinimal, ModuleSpecifierEndingJsExtension];
    }
    case var ᴛ when ᴛ == ModuleSpecifierEndingMinimal: {
        if (allowImportingTsExtension) {
            return [ModuleSpecifierEndingMinimal, ModuleSpecifierEndingIndex, ModuleSpecifierEndingTsExtension, ModuleSpecifierEndingJsExtension];
        }
        return [ModuleSpecifierEndingMinimal, ModuleSpecifierEndingIndex, ModuleSpecifierEndingJsExtension];
    }
    default: {
        debug.AssertNever(preferredEnding);
        break;
    }}

    return [ModuleSpecifierEndingMinimal];
}

internal static ModuleSpecifierPreferences getModuleSpecifierPreferences(UserPreferences prefs, ModuleSpecifierGenerationHost host, ж<core.CompilerOptions> compilerOptions, SourceFileForSpecifierGeneration importingSourceFile, @string oldImportSpecifier) {
    var excludes = prefs.AutoImportSpecifierExcludeRegexes;
    var relativePreference = RelativePreferenceShortest;
    if (len(oldImportSpecifier) > 0) {
        if (tspath.IsExternalModuleNameRelative(oldImportSpecifier)) {
            relativePreference = RelativePreferenceRelative;
        } else {
            relativePreference = RelativePreferenceNonRelative;
        }
    } else {
        var exprᴛ1 = prefs.ImportModuleSpecifierPreference;
        switch (ᐧ) {
        case {} when exprᴛ1 == ImportModuleSpecifierPreferenceRelative: {
            relativePreference = RelativePreferenceRelative;
            break;
        }
        case {} when exprᴛ1 == ImportModuleSpecifierPreferenceNonRelative: {
            relativePreference = RelativePreferenceNonRelative;
            break;
        }
        case {} when exprᴛ1 == ImportModuleSpecifierPreferenceProjectRelative: {
            relativePreference = RelativePreferenceExternalNonRelative;
            break;
        }}

    }

    var prefsʗ1 = prefs;
    var getAllowedEndingsInPreferredOrder = (coreꓸResolutionMode syntaxImpliedNodeFormat) => GetAllowedEndingsInPreferredOrder(
            prefsʗ1,
            host,
            compilerOptions,
            importingSourceFile,
            oldImportSpecifier,
            syntaxImpliedNodeFormat);

    return new ModuleSpecifierPreferences(
        excludeRegexes: excludes,
        relativePreference: relativePreference,
        getAllowedEndingsInPreferredOrder: getAllowedEndingsInPreferredOrder
    );
}

} // end modulespecifiers_package
