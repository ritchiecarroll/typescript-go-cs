namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using semver = go.github.com.microsoft.TypeScript.tsc.@internal.semver_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class module_package {

internal static readonly ж<semver.Version> ᏑtypeScriptVersion = new StaticBox<semver.Version>(semver.MustParse(core.Version()));
internal static ref semver.Version typeScriptVersion => ref ((StaticBox<semver.Version>)ᏑtypeScriptVersion).ValueSlot;

public static readonly @string InferredTypesContainingFile = "__inferred type names__.ts"u8;

public static bool IsApplicableVersionedTypesKey(@string key) {
    if (!strings.HasPrefix(key, "types@")) {
        return false;
    }
    var (range_, ok) = semver.TryParseVersionRange(key[(int)(len("types@"))..]);
    if (!ok) {
        return false;
    }
    return range_.Test(ᏑtypeScriptVersion);
}

public static @string ParseNodeModuleFromPath(@string resolved, bool isFolder) {
    @string path = tspath.NormalizePath(resolved);
    nint idx = strings.LastIndex(path, "/node_modules/");
    if (idx == -1) {
        return "";
    }

    nint indexAfterNodeModules = idx + len("/node_modules/");
    nint indexAfterPackageName = moveToNextDirectorySeparatorIfAvailable(path, indexAfterNodeModules, isFolder);
    if (path[indexAfterNodeModules] == '@') {
        indexAfterPackageName = moveToNextDirectorySeparatorIfAvailable(path, indexAfterPackageName, isFolder);
    }
    return path.slice(0, indexAfterPackageName);
}

public static (@string packageName, @string rest) ParsePackageName(@string moduleName) {
    nint idx = strings.Index(moduleName, "/");
    if (len(moduleName) > 0 && moduleName[0] == '@') {
        nint offset = idx + 1;
        idx = strings.Index(moduleName.slice(offset), "/");
        if (idx != -1) {
            idx += offset;
        }
    }
    if (idx == -1) {
        return (moduleName, "");
    }
    return (moduleName.slice(0, idx), moduleName.slice(idx + 1));
}

public static @string MangleScopedPackageName(@string packageName) {
    if (len(packageName) > 0 && packageName[0] == '@') {
        nint idx = strings.Index(packageName, "/");
        if (idx == -1) {
            return packageName;
        }
        return packageName.slice(1, idx) + "__" + packageName.slice(idx + 1);
    }
    return packageName;
}

public static @string UnmangleScopedPackageName(@string packageName) {
    var (before, after, ok) = strings.Cut(packageName, "__");
    if (ok) {
        return "@"u8 + before + "/"u8 + after;
    }
    return packageName;
}

public static @string GetTypesPackageName(@string packageName) {
    return "@types/"u8 + MangleScopedPackageName(packageName);
}

public static @string GetPackageNameFromTypesPackageName(@string mangledName) {
    @string withoutAtTypePrefix = strings.TrimPrefix(mangledName, "@types/");
    if (withoutAtTypePrefix != mangledName) {
        return UnmangleScopedPackageName(withoutAtTypePrefix);
    }
    return mangledName;
}

public static nint ComparePatternKeys(@string a, @string b) {
    nint aPatternIndex = strings.Index(a, "*");
    nint bPatternIndex = strings.Index(b, "*");
    nint baseLenA = len(a);
    if (aPatternIndex != -1) {
        baseLenA = aPatternIndex + 1;
    }
    nint baseLenB = len(b);
    if (bPatternIndex != -1) {
        baseLenB = bPatternIndex + 1;
    }

    if (baseLenA > baseLenB) {
        return -1;
    }
    if (baseLenB > baseLenA) {
        return 1;
    }
    if (aPatternIndex == -1) {
        return 1;
    }
    if (bPatternIndex == -1) {
        return -1;
    }
    if (len(a) > len(b)) {
        return -1;
    }
    if (len(b) > len(a)) {
        return 1;
    }
    return 0;
}

public static ж<diagnostics.Message> GetResolutionDiagnostic(ж<core.CompilerOptions> options, ж<ResolvedModule> ᏑresolvedModule, ж<ast.SourceFile> @file) {
    ref var resolvedModule = ref ᏑresolvedModule.DerefOrNull();

    ж<diagnostics.Message> needJsx() {
        if (options.Value.Jsx != core.JsxEmitNone) {
            return default;
        }
        return diagnostics.Module_0_was_resolved_to_1_but_jsx_is_not_set;
    }

    ж<diagnostics.Message> needAllowJs() {
        if (options.Value.GetAllowJS() || !options.Value.NoImplicitAny.DefaultIfUnknown(options.Value.Strict).IsTrue()) {
            return default;
        }
        return diagnostics.Could_not_find_a_declaration_file_for_module_0_1_implicitly_has_an_any_type;
    }

    ж<diagnostics.Message> needResolveJsonModule() {
        if (options.Value.GetResolveJsonModule()) {
            return default;
        }
        return diagnostics.Module_0_was_resolved_to_1_but_resolveJsonModule_is_not_used;
    }

    ж<diagnostics.Message> needAllowArbitraryExtensions() {
        if (@file.Value.IsDeclarationFile || options.Value.AllowArbitraryExtensions.IsTrue()) {
            return default;
        }
        return diagnostics.Module_0_was_resolved_to_1_but_allowArbitraryExtensions_is_not_set;
    }

    if (resolvedModule.ResolvedUsingExtraExtensions) {
        return default;
    }

    var exprᴛ1 = resolvedModule.Extension;
    switch (ᐧ) {
    case {} when exprᴛ1 == tspath.ExtensionTs || exprᴛ1 == tspath.ExtensionDts || exprᴛ1 == tspath.ExtensionMts || exprᴛ1 == tspath.ExtensionDmts || exprᴛ1 == tspath.ExtensionCts || exprᴛ1 == tspath.ExtensionDcts: {
        return default;
    }
    case {} when exprᴛ1 == tspath.ExtensionTsx: {
        return needJsx();
    }
    case {} when exprᴛ1 == tspath.ExtensionJsx: {
        var message = needJsx(); if (message != nil) {
            return message;
        }
        return needAllowJs();
    }
    case {} when exprᴛ1 == tspath.ExtensionJs || exprᴛ1 == tspath.ExtensionMjs || exprᴛ1 == tspath.ExtensionCjs: {
        return needAllowJs();
    }
    case {} when exprᴛ1 == tspath.ExtensionJson: {
        return needResolveJsonModule();
    }
    default: {
        return needAllowArbitraryExtensions();
    }}

}

public static @string TryGetJSExtensionForFile(@string fileName, ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    @string ext = tspath.TryGetExtensionFromPath(fileName);
    switch (ᐧ) {
    case {} when ext == tspath.ExtensionTs || ext == tspath.ExtensionDts: {
        return tspath.ExtensionJs;
    }
    case {} when ext == tspath.ExtensionTsx: {
        if (options.Jsx == core.JsxEmitPreserve) {
            return tspath.ExtensionJsx;
        }
        return tspath.ExtensionJs;
    }
    case {} when ext == tspath.ExtensionJs || ext == tspath.ExtensionJsx || ext == tspath.ExtensionJson: {
        return ext;
    }
    case {} when ext == tspath.ExtensionDmts || ext == tspath.ExtensionMts || ext == tspath.ExtensionMjs: {
        return tspath.ExtensionMjs;
    }
    case {} when ext == tspath.ExtensionDcts || ext == tspath.ExtensionCts || ext == tspath.ExtensionCjs: {
        return tspath.ExtensionCjs;
    }
    default: {
        return "";
    }}

}

} // end module_package
