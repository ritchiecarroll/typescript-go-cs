namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using reflect = reflect_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static tsoptions_package;

partial class tsoptions_package {

public static core.Tristate ParseTristate(any value) {
    if (value == nil) {
        return core.TSUnknown;
    }
    var (v, ok) = value._<core.Tristate>(ᐧ); if (ok) {
        return v;
    }
    if (AreEqual(value, true)) {
        return core.TSTrue;
    } else {
        return core.TSFalse;
    }
}

public static slice<@string> ParseStringArray(any value) {
    var (arr, ok) = value._<slice<any>>(ᐧ); if (ok) {
        if (arr == nil) {
            return default;
        }
        var result = new slice<@string>(0, len(arr));
        foreach (var (_, v) in range(arr)) {
            var (str, okΔ1) = v._<@string>(ᐧ); if (okΔ1) {
                result = append(result, str);
            }
        }
        return result;
    }
    return default;
}

internal static ж<collections.OrderedMap<@string, slice<@string>>> parseStringMap(any value) {
    {
        var (m, ok) = value._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok) {
            var result = collections.NewOrderedMapWithSizeHint<@string, slice<@string>>(m.Size());
            foreach (var (k, v) in range<@string, any>(m.Entries().Invoke)) {
                result.Set(k, ParseStringArray(v));
            }
            return result;
        }
    }
    return default;
}

public static @string ParseString(any value) {
    var (str, ok) = value._<@string>(ᐧ); if (ok) {
        return str;
    }
    return "";
}

internal static ж<nint> parseNumber(any value) {
    {
        ref var num = ref heap<nint>(out var Ꮡnum);
        (num, var ok) = value._<nint>(ᐧ); if (ok) {
            return Ꮡnum;
        }
    }
    {
        var (num, ok) = value._<float64>(ᐧ); if (ok) {
            ref var n = ref heap<nint>((nint)num, out var Ꮡn);
            return Ꮡn;
        }
    }
    return default;
}

partial struct projectReferenceParseResult {
    internal core.ProjectReference reference;
    internal bool hasPath;
    internal bool pathValid;
    internal bool hasCircular;
    internal bool circularValid;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string pathˢ = "path"u8;
internal static readonly @string circularˢ = "circular"u8;

internal static ж<projectReferenceParseResult> parseProjectReference(any json) {
    var (v, ok) = json._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok) {
        var result = Ꮡ(new projectReferenceParseResult());
        {
            var (value, okΔ1) = v.Get(pathˢ); if (okΔ1) {
                result.hasPath = true;
                var (path, okΔ2) = value._<@string>(ᐧ); if (okΔ2) {
                    result.reference.Path = path;
                    result.pathValid = true;
                }
            }
        }
        {
            var (value, okΔ3) = v.Get(circularˢ); if (okΔ3) {
                result.hasCircular = true;
                var (circular, okΔ4) = value._<bool>(ᐧ); if (okΔ4) {
                    result.reference.Circular = circular;
                    result.circularValid = true;
                }
            }
        }
        return result;
    }
    return default;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string packageˢ = "package"u8;
internal static readonly @string extensionsˢ = "extensions"u8;
internal static readonly @string optionsˢ = "options"u8;

internal static (ж<contentmapper.Mapper>, slice<ж<ast.Diagnostic>>) parseContentMapper(any value) {
    var (v, ok) = value._<ж<collections.OrderedMap<@string, any>>>(ᐧ);
    if (!ok) {
        return (default, default);
    }
    slice<ж<ast.Diagnostic>> errors = default;
    var mapper = Ꮡ(new contentmapper.Mapper());
    var (pkg, okΔ1) = v.Get(packageˢ); if (okΔ1) {
        var (str, isString) = pkg._<@string>(ᐧ); if (isString && str != ""u8) {
            mapper.Package = str;
        } else {
            errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Compiler_option_0_requires_a_value_of_type_1, "contentMapper.package", "string"));
        }
    } else {
        errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Compiler_option_0_requires_a_value_of_type_1, "contentMapper.package", "string"));
    }
    var (extensions, okΔ2) = v.Get(extensionsˢ); if (okΔ2) {
        var (strs, isStringArray) = parseStringArrayStrict(extensions); if (isStringArray) {
            mapper.Definition.Extensions = strs;
        } else {
            errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Compiler_option_0_requires_a_value_of_type_1, "contentMapper.extensions", "string[]"));
        }
    } else {
        errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Compiler_option_0_requires_a_value_of_type_1, "contentMapper.extensions", "string[]"));
    }
    var (options, okΔ3) = v.Get(optionsˢ); if (okΔ3) {
        var (_, isObject) = options._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (!isObject) {
            errors = append(errors, ast.NewCompilerDiagnostic(diagnostics.Compiler_option_0_requires_a_value_of_type_1, "contentMapper.options", "object"));
        } else {
            (mapper.Options, _) = json.Marshal(options);
        }
    }
    if (len(errors) != 0) {
        return (default, errors);
    }
    return (mapper, errors);
}

internal static (slice<@string>, bool) parseStringArrayStrict(any value) {
    var (arr, ok) = value._<slice<any>>(ᐧ);
    if (!ok) {
        return (default, false);
    }
    var result = new slice<@string>(0, len(arr));
    foreach (var (_, v) in range(arr)) {
        var (str, okΔ1) = v._<@string>(ᐧ);
        if (!okΔ1) {
            return (default, false);
        }
        result = append(result, str);
    }
    return (result, true);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string includeˢ = "include"u8;
internal static readonly @string excludeˢ = "exclude"u8;
internal static readonly @string filesˢ = "files"u8;
internal static readonly @string referencesˢ = "references"u8;
internal static readonly @string contentMappersˢ = "contentMappers"u8;
internal static readonly @string extendsˢ = "extends"u8;
internal static readonly @string compilerOptionsˢ = "compilerOptions"u8;
internal static readonly @string excludesˢ = "excludes"u8;
internal static readonly @string typeAcquisitionˢ = "typeAcquisition"u8;

internal static ж<collections.OrderedMap<@string, any>> parseJsonToStringKey(any json) {
    var result = collections.NewOrderedMapWithSizeHint<@string, any>(6);
    var (m, ok) = json._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok) {
        {
            var (v, okΔ1) = m.Get(includeˢ); if (okΔ1) {
                result.Set(includeˢ, v);
            }
        }
        {
            var (v, okΔ2) = m.Get(excludeˢ); if (okΔ2) {
                result.Set(excludeˢ, v);
            }
        }
        {
            var (v, okΔ3) = m.Get(filesˢ); if (okΔ3) {
                result.Set(filesˢ, v);
            }
        }
        {
            var (v, okΔ4) = m.Get(referencesˢ); if (okΔ4) {
                result.Set(referencesˢ, v);
            }
        }
        {
            var (v, okΔ5) = m.Get(contentMappersˢ); if (okΔ5) {
                result.Set(contentMappersˢ, v);
            }
        }
        {
            var (v, okΔ6) = m.Get(extendsˢ); if (okΔ6) {
                var (str, okΔ7) = v._<@string>(ᐧ); if (okΔ7) {
                    result.Set(extendsˢ, slice<any>([str]));
                }
                result.Set(extendsˢ, v);
            }
        }
        {
            var (v, okΔ8) = m.Get(compilerOptionsˢ); if (okΔ8) {
                result.Set(compilerOptionsˢ, v);
            }
        }
        {
            var (v, okΔ9) = m.Get(excludesˢ); if (okΔ9) {
                result.Set(excludesˢ, v);
            }
        }
        {
            var (v, okΔ10) = m.Get(typeAcquisitionˢ); if (okΔ10) {
                result.Set(typeAcquisitionˢ, v);
            }
        }
    }
    return result;
}

partial interface optionParser {
    slice<ж<ast.Diagnostic>> ParseOption(@string key, any value);
    ж<diagnostics.Message> UnknownOptionDiagnostic();
    ж<diagnostics.Message> UnknownDidYouMeanDiagnostic();
}

partial struct compilerOptionsParser {
    /*embed*/ public ж<github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions> CompilerOptions;
}

} partial class tsoptionsᴛcompilerOptionsParser {

internal static slice<ж<ast.Diagnostic>> ParseOption(this ref compilerOptionsParser o, @string key, any value) {
    return ParseCompilerOptions(key, value, o.CompilerOptions);
}

internal static ж<diagnostics.Message> UnknownOptionDiagnostic(this ref compilerOptionsParser o) {
    return extraKeyDiagnostics("compilerOptions");
}

internal static ж<diagnostics.Message> UnknownDidYouMeanDiagnostic(this ref compilerOptionsParser o) {
    return extraKeyDidYouMeanDiagnostics("compilerOptions");
}

} partial class tsoptions_package {

partial struct watchOptionsParser {
    /*embed*/ public ж<github.com.microsoft.TypeScript.tsc.@internal.core_package.WatchOptions> WatchOptions;
}

} partial class tsoptionsᴛwatchOptionsParser {

internal static slice<ж<ast.Diagnostic>> ParseOption(this ref watchOptionsParser o, @string key, any value) {
    return ParseWatchOptions(key, value, o.WatchOptions);
}

internal static ж<diagnostics.Message> UnknownOptionDiagnostic(this ref watchOptionsParser o) {
    return extraKeyDiagnostics("watchOptions");
}

internal static ж<diagnostics.Message> UnknownDidYouMeanDiagnostic(this ref watchOptionsParser o) {
    return extraKeyDidYouMeanDiagnostics("watchOptions");
}

} partial class tsoptions_package {

partial struct typeAcquisitionParser {
    /*embed*/ public ж<github.com.microsoft.TypeScript.tsc.@internal.core_package.TypeAcquisition> TypeAcquisition;
}

} partial class tsoptionsᴛtypeAcquisitionParser {

internal static slice<ж<ast.Diagnostic>> ParseOption(this ref typeAcquisitionParser o, @string key, any value) {
    return ParseTypeAcquisition(key, value, o.TypeAcquisition);
}

internal static ж<diagnostics.Message> UnknownOptionDiagnostic(this ref typeAcquisitionParser o) {
    return extraKeyDiagnostics("typeAcquisition");
}

internal static ж<diagnostics.Message> UnknownDidYouMeanDiagnostic(this ref typeAcquisitionParser o) {
    return extraKeyDidYouMeanDiagnostics("typeAcquisition");
}

} partial class tsoptions_package {

partial struct buildOptionsParser {
    /*embed*/ public ж<github.com.microsoft.TypeScript.tsc.@internal.core_package.BuildOptions> BuildOptions;
}

} partial class tsoptionsᴛbuildOptionsParser {

internal static slice<ж<ast.Diagnostic>> ParseOption(this ref buildOptionsParser o, @string key, any value) {
    return ParseBuildOptions(key, value, o.BuildOptions);
}

internal static ж<diagnostics.Message> UnknownOptionDiagnostic(this ref buildOptionsParser o) {
    return extraKeyDiagnostics("buildOptions");
}

internal static ж<diagnostics.Message> UnknownDidYouMeanDiagnostic(this ref buildOptionsParser o) {
    return extraKeyDidYouMeanDiagnostics("buildOptions");
}

} partial class tsoptions_package {

public static slice<ж<ast.Diagnostic>> ParseCompilerOptions(@string key, any value, ж<core.CompilerOptions> allOptions) {
    if (value == nil) {
        return default;
    }
    if (allOptions == nil) {
        return default;
    }
    parseCompilerOptions(key, value, ref (allOptions).DerefOrNull());
    return default;
}

internal static bool /*foundKey*/ parseCompilerOptions(@string key, any value, ref core.CompilerOptions allOptions) {
    var option = CommandLineCompilerOptionsMap.Get(key);
    if (option != nil) {
        key = option.Name;
    }
    switch (ᐧ) {
    case {} when key == "allowJs"u8: {
        allOptions.AllowJs = ParseTristate(value);
        break;
    }
    case {} when key == "allowImportingTsExtensions"u8: {
        allOptions.AllowImportingTsExtensions = ParseTristate(value);
        break;
    }
    case {} when key == "allowSyntheticDefaultImports"u8: {
        allOptions.AllowSyntheticDefaultImports = ParseTristate(value);
        break;
    }
    case {} when key == "allowNonTsExtensions"u8: {
        allOptions.AllowNonTsExtensions = ParseTristate(value);
        break;
    }
    case {} when key == "allowUmdGlobalAccess"u8: {
        allOptions.AllowUmdGlobalAccess = ParseTristate(value);
        break;
    }
    case {} when key == "allowUnreachableCode"u8: {
        allOptions.AllowUnreachableCode = ParseTristate(value);
        break;
    }
    case {} when key == "allowUnusedLabels"u8: {
        allOptions.AllowUnusedLabels = ParseTristate(value);
        break;
    }
    case {} when key == "allowArbitraryExtensions"u8: {
        allOptions.AllowArbitraryExtensions = ParseTristate(value);
        break;
    }
    case {} when key == "alwaysStrict"u8: {
        allOptions.AlwaysStrict = ParseTristate(value);
        break;
    }
    case {} when key == "assumeChangesOnlyAffectDirectDependencies"u8: {
        allOptions.AssumeChangesOnlyAffectDirectDependencies = ParseTristate(value);
        break;
    }
    case {} when key == "baseUrl"u8: {
        allOptions.BaseUrl = ParseString(value);
        break;
    }
    case {} when key == "build"u8: {
        allOptions.Build = ParseTristate(value);
        break;
    }
    case {} when key == "checkJs"u8: {
        allOptions.CheckJs = ParseTristate(value);
        break;
    }
    case {} when key == "customConditions"u8: {
        allOptions.CustomConditions = ParseStringArray(value);
        break;
    }
    case {} when key == "composite"u8: {
        allOptions.Composite = ParseTristate(value);
        break;
    }
    case {} when key == "declarationDir"u8: {
        allOptions.DeclarationDir = ParseString(value);
        break;
    }
    case {} when key == "deduplicatePackages"u8: {
        allOptions.DeduplicatePackages = ParseTristate(value);
        break;
    }
    case {} when key == "diagnostics"u8: {
        allOptions.Diagnostics = ParseTristate(value);
        break;
    }
    case {} when key == "disableSizeLimit"u8: {
        allOptions.DisableSizeLimit = ParseTristate(value);
        break;
    }
    case {} when key == "disableSourceOfProjectReferenceRedirect"u8: {
        allOptions.DisableSourceOfProjectReferenceRedirect = ParseTristate(value);
        break;
    }
    case {} when key == "disableSolutionSearching"u8: {
        allOptions.DisableSolutionSearching = ParseTristate(value);
        break;
    }
    case {} when key == "disableReferencedProjectLoad"u8: {
        allOptions.DisableReferencedProjectLoad = ParseTristate(value);
        break;
    }
    case {} when key == "declarationMap"u8: {
        allOptions.DeclarationMap = ParseTristate(value);
        break;
    }
    case {} when key == "declaration"u8: {
        allOptions.Declaration = ParseTristate(value);
        break;
    }
    case {} when key == "downlevelIteration"u8: {
        allOptions.DownlevelIteration = ParseTristate(value);
        break;
    }
    case {} when key == "erasableSyntaxOnly"u8: {
        allOptions.ErasableSyntaxOnly = ParseTristate(value);
        break;
    }
    case {} when key == "emitDeclarationOnly"u8: {
        allOptions.EmitDeclarationOnly = ParseTristate(value);
        break;
    }
    case {} when key == "extendedDiagnostics"u8: {
        allOptions.ExtendedDiagnostics = ParseTristate(value);
        break;
    }
    case {} when key == "emitDecoratorMetadata"u8: {
        allOptions.EmitDecoratorMetadata = ParseTristate(value);
        break;
    }
    case {} when key == "emitBOM"u8: {
        allOptions.EmitBOM = ParseTristate(value);
        break;
    }
    case {} when key == "esModuleInterop"u8: {
        allOptions.ESModuleInterop = ParseTristate(value);
        break;
    }
    case {} when key == "exactOptionalPropertyTypes"u8: {
        allOptions.ExactOptionalPropertyTypes = ParseTristate(value);
        break;
    }
    case {} when key == "explainFiles"u8: {
        allOptions.ExplainFiles = ParseTristate(value);
        break;
    }
    case {} when key == "experimentalDecorators"u8: {
        allOptions.ExperimentalDecorators = ParseTristate(value);
        break;
    }
    case {} when key == "forceConsistentCasingInFileNames"u8: {
        allOptions.ForceConsistentCasingInFileNames = ParseTristate(value);
        break;
    }
    case {} when key == "generateCpuProfile"u8: {
        allOptions.GenerateCpuProfile = ParseString(value);
        break;
    }
    case {} when key == "generateTrace"u8: {
        allOptions.GenerateTrace = ParseString(value);
        break;
    }
    case {} when key == "isolatedModules"u8: {
        allOptions.IsolatedModules = ParseTristate(value);
        break;
    }
    case {} when key == "ignoreConfig"u8: {
        allOptions.IgnoreConfig = ParseTristate(value);
        break;
    }
    case {} when key == "ignoreDeprecations"u8: {
        allOptions.IgnoreDeprecations = ParseString(value);
        break;
    }
    case {} when key == "importHelpers"u8: {
        allOptions.ImportHelpers = ParseTristate(value);
        break;
    }
    case {} when key == "incremental"u8: {
        allOptions.Incremental = ParseTristate(value);
        break;
    }
    case {} when key == "init"u8: {
        allOptions.Init = ParseTristate(value);
        break;
    }
    case {} when key == "inlineSourceMap"u8: {
        allOptions.InlineSourceMap = ParseTristate(value);
        break;
    }
    case {} when key == "inlineSources"u8: {
        allOptions.InlineSources = ParseTristate(value);
        break;
    }
    case {} when key == "isolatedDeclarations"u8: {
        allOptions.IsolatedDeclarations = ParseTristate(value);
        break;
    }
    case {} when key == "jsx"u8: {
        allOptions.Jsx = floatOrInt32ToFlag<core.JsxEmit>(value);
        break;
    }
    case {} when key == "jsxFactory"u8: {
        allOptions.JsxFactory = ParseString(value);
        break;
    }
    case {} when key == "jsxFragmentFactory"u8: {
        allOptions.JsxFragmentFactory = ParseString(value);
        break;
    }
    case {} when key == "jsxImportSource"u8: {
        allOptions.JsxImportSource = ParseString(value);
        break;
    }
    case {} when key == "lib"u8: {
        var (_, ok) = value._<slice<@string>>(ᐧ); if (ok) {
            allOptions.Lib = value._<slice<@string>>();
        } else {
            allOptions.Lib = ParseStringArray(value);
        }
        break;
    }
    case {} when key == "libReplacement"u8: {
        allOptions.LibReplacement = ParseTristate(value);
        break;
    }
    case {} when key == "listEmittedFiles"u8: {
        allOptions.ListEmittedFiles = ParseTristate(value);
        break;
    }
    case {} when key == "listFiles"u8: {
        allOptions.ListFiles = ParseTristate(value);
        break;
    }
    case {} when key == "listFilesOnly"u8: {
        allOptions.ListFilesOnly = ParseTristate(value);
        break;
    }
    case {} when key == "locale"u8: {
        allOptions.Locale = ParseString(value);
        break;
    }
    case {} when key == "mapRoot"u8: {
        allOptions.MapRoot = ParseString(value);
        break;
    }
    case {} when key == "module"u8: {
        allOptions.Module = floatOrInt32ToFlag<core.ModuleKind>(value);
        break;
    }
    case {} when key == "moduleDetectionKind"u8: {
        allOptions.ModuleDetection = floatOrInt32ToFlag<core.ModuleDetectionKind>(value);
        break;
    }
    case {} when key == "moduleResolution"u8: {
        allOptions.ModuleResolution = floatOrInt32ToFlag<core.ModuleResolutionKind>(value);
        break;
    }
    case {} when key == "moduleSuffixes"u8: {
        allOptions.ModuleSuffixes = ParseStringArray(value);
        break;
    }
    case {} when key == "moduleDetection"u8: {
        allOptions.ModuleDetection = floatOrInt32ToFlag<core.ModuleDetectionKind>(value);
        break;
    }
    case {} when key == "noCheck"u8: {
        allOptions.NoCheck = ParseTristate(value);
        break;
    }
    case {} when key == "noFallthroughCasesInSwitch"u8: {
        allOptions.NoFallthroughCasesInSwitch = ParseTristate(value);
        break;
    }
    case {} when key == "noEmitForJsFiles"u8: {
        allOptions.NoEmitForJsFiles = ParseTristate(value);
        break;
    }
    case {} when key == "noErrorTruncation"u8: {
        allOptions.NoErrorTruncation = ParseTristate(value);
        break;
    }
    case {} when key == "noImplicitAny"u8: {
        allOptions.NoImplicitAny = ParseTristate(value);
        break;
    }
    case {} when key == "noImplicitThis"u8: {
        allOptions.NoImplicitThis = ParseTristate(value);
        break;
    }
    case {} when key == "noLib"u8: {
        allOptions.NoLib = ParseTristate(value);
        break;
    }
    case {} when key == "noPropertyAccessFromIndexSignature"u8: {
        allOptions.NoPropertyAccessFromIndexSignature = ParseTristate(value);
        break;
    }
    case {} when key == "noUncheckedIndexedAccess"u8: {
        allOptions.NoUncheckedIndexedAccess = ParseTristate(value);
        break;
    }
    case {} when key == "noEmitHelpers"u8: {
        allOptions.NoEmitHelpers = ParseTristate(value);
        break;
    }
    case {} when key == "noEmitOnError"u8: {
        allOptions.NoEmitOnError = ParseTristate(value);
        break;
    }
    case {} when key == "noImplicitReturns"u8: {
        allOptions.NoImplicitReturns = ParseTristate(value);
        break;
    }
    case {} when key == "noUnusedLocals"u8: {
        allOptions.NoUnusedLocals = ParseTristate(value);
        break;
    }
    case {} when key == "noUnusedParameters"u8: {
        allOptions.NoUnusedParameters = ParseTristate(value);
        break;
    }
    case {} when key == "noImplicitOverride"u8: {
        allOptions.NoImplicitOverride = ParseTristate(value);
        break;
    }
    case {} when key == "noUncheckedSideEffectImports"u8: {
        allOptions.NoUncheckedSideEffectImports = ParseTristate(value);
        break;
    }
    case {} when key == "outFile"u8: {
        allOptions.OutFile = ParseString(value);
        break;
    }
    case {} when key == "noResolve"u8: {
        allOptions.NoResolve = ParseTristate(value);
        break;
    }
    case {} when key == "paths"u8: {
        allOptions.Paths = parseStringMap(value);
        break;
    }
    case {} when key == "preserveWatchOutput"u8: {
        allOptions.PreserveWatchOutput = ParseTristate(value);
        break;
    }
    case {} when key == "preserveConstEnums"u8: {
        allOptions.PreserveConstEnums = ParseTristate(value);
        break;
    }
    case {} when key == "preserveSymlinks"u8: {
        allOptions.PreserveSymlinks = ParseTristate(value);
        break;
    }
    case {} when key == "project"u8: {
        allOptions.Project = ParseString(value);
        break;
    }
    case {} when key == "pretty"u8: {
        allOptions.Pretty = ParseTristate(value);
        break;
    }
    case {} when key == "resolveJsonModule"u8: {
        allOptions.ResolveJsonModule = ParseTristate(value);
        break;
    }
    case {} when key == "resolvePackageJsonExports"u8: {
        allOptions.ResolvePackageJsonExports = ParseTristate(value);
        break;
    }
    case {} when key == "resolvePackageJsonImports"u8: {
        allOptions.ResolvePackageJsonImports = ParseTristate(value);
        break;
    }
    case {} when key == "reactNamespace"u8: {
        allOptions.ReactNamespace = ParseString(value);
        break;
    }
    case {} when key == "rewriteRelativeImportExtensions"u8: {
        allOptions.RewriteRelativeImportExtensions = ParseTristate(value);
        break;
    }
    case {} when key == "rootDir"u8: {
        allOptions.RootDir = ParseString(value);
        break;
    }
    case {} when key == "rootDirs"u8: {
        allOptions.RootDirs = ParseStringArray(value);
        break;
    }
    case {} when key == "removeComments"u8: {
        allOptions.RemoveComments = ParseTristate(value);
        break;
    }
    case {} when key == "stableTypeOrdering"u8: {
        allOptions.StableTypeOrdering = ParseTristate(value);
        break;
    }
    case {} when key == "strict"u8: {
        allOptions.Strict = ParseTristate(value);
        break;
    }
    case {} when key == "strictBindCallApply"u8: {
        allOptions.StrictBindCallApply = ParseTristate(value);
        break;
    }
    case {} when key == "strictBuiltinIteratorReturn"u8: {
        allOptions.StrictBuiltinIteratorReturn = ParseTristate(value);
        break;
    }
    case {} when key == "strictFunctionTypes"u8: {
        allOptions.StrictFunctionTypes = ParseTristate(value);
        break;
    }
    case {} when key == "strictNullChecks"u8: {
        allOptions.StrictNullChecks = ParseTristate(value);
        break;
    }
    case {} when key == "strictPropertyInitialization"u8: {
        allOptions.StrictPropertyInitialization = ParseTristate(value);
        break;
    }
    case {} when key == "skipDefaultLibCheck"u8: {
        allOptions.SkipDefaultLibCheck = ParseTristate(value);
        break;
    }
    case {} when key == "sourceMap"u8: {
        allOptions.SourceMap = ParseTristate(value);
        break;
    }
    case {} when key == "sourceRoot"u8: {
        allOptions.SourceRoot = ParseString(value);
        break;
    }
    case {} when key == "stripInternal"u8: {
        allOptions.StripInternal = ParseTristate(value);
        break;
    }
    case {} when key == "suppressOutputPathCheck"u8: {
        allOptions.SuppressOutputPathCheck = ParseTristate(value);
        break;
    }
    case {} when key == "target"u8: {
        allOptions.Target = floatOrInt32ToFlag<core.ScriptTarget>(value);
        break;
    }
    case {} when key == "traceResolution"u8: {
        allOptions.TraceResolution = ParseTristate(value);
        break;
    }
    case {} when key == "tsBuildInfoFile"u8: {
        allOptions.TsBuildInfoFile = ParseString(value);
        break;
    }
    case {} when key == "typeRoots"u8: {
        allOptions.TypeRoots = ParseStringArray(value);
        break;
    }
    case {} when key == "types"u8: {
        allOptions.Types = ParseStringArray(value);
        break;
    }
    case {} when key == "useDefineForClassFields"u8: {
        allOptions.UseDefineForClassFields = ParseTristate(value);
        break;
    }
    case {} when key == "useUnknownInCatchVariables"u8: {
        allOptions.UseUnknownInCatchVariables = ParseTristate(value);
        break;
    }
    case {} when key == "verbatimModuleSyntax"u8: {
        allOptions.VerbatimModuleSyntax = ParseTristate(value);
        break;
    }
    case {} when key == "version"u8: {
        allOptions.Version = ParseTristate(value);
        break;
    }
    case {} when key == "help"u8: {
        allOptions.Help = ParseTristate(value);
        break;
    }
    case {} when key == "all"u8: {
        allOptions.All = ParseTristate(value);
        break;
    }
    case {} when key == "maxNodeModuleJsDepth"u8: {
        allOptions.MaxNodeModuleJsDepth = parseNumber(value);
        break;
    }
    case {} when key == "skipLibCheck"u8: {
        allOptions.SkipLibCheck = ParseTristate(value);
        break;
    }
    case {} when key == "noEmit"u8: {
        allOptions.NoEmit = ParseTristate(value);
        break;
    }
    case {} when key == "showConfig"u8: {
        allOptions.ShowConfig = ParseTristate(value);
        break;
    }
    case {} when key == "configFilePath"u8: {
        allOptions.ConfigFilePath = ParseString(value);
        break;
    }
    case {} when key == "noDtsResolution"u8: {
        allOptions.NoDtsResolution = ParseTristate(value);
        break;
    }
    case {} when key == "pathsBasePath"u8: {
        allOptions.PathsBasePath = ParseString(value);
        break;
    }
    case {} when key == "outDir"u8: {
        allOptions.OutDir = ParseString(value);
        break;
    }
    case {} when key == "newLine"u8: {
        allOptions.NewLine = floatOrInt32ToFlag<core.NewLineKind>(value);
        break;
    }
    case {} when key == "watch"u8: {
        allOptions.Watch = ParseTristate(value);
        break;
    }
    case {} when key == "pprofDir"u8: {
        allOptions.PprofDir = ParseString(value);
        break;
    }
    case {} when key == "singleThreaded"u8: {
        allOptions.SingleThreaded = ParseTristate(value);
        break;
    }
    case {} when key == "quiet"u8: {
        allOptions.Quiet = ParseTristate(value);
        break;
    }
    case {} when key == "checkers"u8: {
        allOptions.Checkers = parseNumber(value);
        break;
    }
    case {} when key == "runExternalCode"u8: {
        allOptions.RunExternalCode = ParseTristate(value);
        break;
    }
    default: {
        return false;
    }}

    return true;
}

internal static T floatOrInt32ToFlag<T>(any value)
    where T : /* ~int32 */ IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>, IIncrementOperators<T>, IDecrementOperators<T>, IUnaryNegationOperators<T, T>, IModulusOperators<T, T, T>, IBitwiseOperators<T, T, T>, IShiftOperators<T, int, T>, IEqualityOperators<T, T, bool>, IComparisonOperators<T, T, bool>, new()
{
    var (v, ok) = value._<T>(ᐧ); if (ok) {
        return v;
    }
    return ConvertToType<T>(value._<float64>());
}

public static slice<ж<ast.Diagnostic>> ParseWatchOptions(@string key, any value, ж<core.WatchOptions> ᏑallOptions) {
    ref var allOptions = ref ᏑallOptions.DerefOrNull();

    if (ᏑallOptions == nil) {
        return default;
    }
    switch (ᐧ) {
    case {} when key == "watchInterval"u8: {
        allOptions.Interval = parseNumber(value);
        break;
    }
    case {} when key == "watchFile"u8: {
        if (value != nil) {
            allOptions.FileKind = value._<core.WatchFileKind>();
        }
        break;
    }
    case {} when key == "watchDirectory"u8: {
        if (value != nil) {
            allOptions.DirectoryKind = value._<core.WatchDirectoryKind>();
        }
        break;
    }
    case {} when key == "fallbackPolling"u8: {
        if (value != nil) {
            allOptions.FallbackPolling = value._<core.PollingKind>();
        }
        break;
    }
    case {} when key == "synchronousWatchDirectory"u8: {
        allOptions.SyncWatchDir = ParseTristate(value);
        break;
    }
    case {} when key == "excludeDirectories"u8: {
        allOptions.ExcludeDir = ParseStringArray(value);
        break;
    }
    case {} when key == "excludeFiles"u8: {
        allOptions.ExcludeFiles = ParseStringArray(value);
        break;
    }}

    return default;
}

public static slice<ж<ast.Diagnostic>> ParseTypeAcquisition(@string key, any value, ж<core.TypeAcquisition> ᏑallOptions) {
    ref var allOptions = ref ᏑallOptions.DerefOrNull();

    if (value == nil) {
        return default;
    }
    if (ᏑallOptions == nil) {
        return default;
    }
    switch (ᐧ) {
    case {} when key == "enable"u8: {
        allOptions.Enable = ParseTristate(value);
        break;
    }
    case {} when key == "include"u8: {
        allOptions.Include = ParseStringArray(value);
        break;
    }
    case {} when key == "exclude"u8: {
        allOptions.Exclude = ParseStringArray(value);
        break;
    }
    case {} when key == "disableFilenameBasedTypeAcquisition"u8: {
        allOptions.DisableFilenameBasedTypeAcquisition = ParseTristate(value);
        break;
    }}

    return default;
}

public static slice<ж<ast.Diagnostic>> ParseBuildOptions(@string key, any value, ж<core.BuildOptions> ᏑallOptions) {
    ref var allOptions = ref ᏑallOptions.DerefOrNull();

    if (value == nil) {
        return default;
    }
    if (ᏑallOptions == nil) {
        return default;
    }
    var option = BuildNameMap.Get(key);
    if (option != nil) {
        key = option.Name;
    }
    switch (ᐧ) {
    case {} when key == "clean"u8: {
        allOptions.Clean = ParseTristate(value);
        break;
    }
    case {} when key == "dry"u8: {
        allOptions.Dry = ParseTristate(value);
        break;
    }
    case {} when key == "force"u8: {
        allOptions.Force = ParseTristate(value);
        break;
    }
    case {} when key == "builders"u8: {
        allOptions.Builders = parseNumber(value);
        break;
    }
    case {} when key == "stopBuildOnErrors"u8: {
        allOptions.StopBuildOnErrors = ParseTristate(value);
        break;
    }
    case {} when key == "verbose"u8: {
        allOptions.Verbose = ParseTristate(value);
        break;
    }}

    return default;
}

internal static ж<core.CompilerOptions> mergeCompilerOptions(ж<core.CompilerOptions> targetOptions, ж<core.CompilerOptions> sourceOptions, any rawSource) {
    if (sourceOptions == nil) {
        return targetOptions;
    }

    ref var explicitNullFields = ref heap(new collections.Set<@string>(), out var ᏑexplicitNullFields);
    if (rawSource != nil) {
        {
            var (rawMap, ok) = rawSource._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (ok && rawMap != nil) {
                {
                    var (compilerOptionsRaw, exists) = rawMap.Get(compilerOptionsˢ); if (exists) {
                        {
                            var (compilerOptionsMap, okΔ1) = compilerOptionsRaw._<ж<collections.OrderedMap<@string, any>>>(ᐧ); if (okΔ1) {
                                foreach (var (key, value) in range<@string, any>(compilerOptionsMap.Entries().Invoke)) {
                                    if (value == nil) {
                                        explicitNullFields.Add(key);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    var targetValue = reflect.ValueOf(targetOptions.OrTypedNil()).Elem();
    var sourceValue = reflect.ValueOf(sourceOptions.OrTypedNil()).Elem();
    var targetType = targetValue.Type();

    foreach (var i in range(targetValue.NumField())) {
        var targetField = targetValue.Field(i);
        var sourceField = sourceValue.Field(i);

        @string jsonTag = targetType.Field(i).Tag.Get("json"); if (jsonTag != ""u8) {
            var (jsonFieldName, _, _) = strings.Cut(jsonTag, ","); if (jsonFieldName != ""u8 && ᏑexplicitNullFields.Has(jsonFieldName)) {
                targetField.SetZero();
                continue;
            }
        }

        if (!sourceField.IsZero()) {
            targetField.Set(sourceField);
        }
    }

    return targetOptions;
}

internal static ж<collections.OrderedMap<@string, any>> convertToOptionsWithAbsolutePaths(ж<collections.OrderedMap<@string, any>> ᏑoptionsBase, CommandLineOptionNameMap optionMap, @string cwd) {
    ref var optionsBase = ref ᏑoptionsBase.DerefOrNull();

    if (ᏑoptionsBase == nil) {
        return default;
    }
    foreach (var (o, v) in range<@string, any>(ᏑoptionsBase.Entries().Invoke)) {
        var (result, ok) = ConvertOptionToAbsolutePath(o, v, optionMap, cwd);
        if (ok) {
            optionsBase.Set(o, result);
        }
    }
    return ᏑoptionsBase;
}

public static (any, bool) ConvertOptionToAbsolutePath(@string o, any v, CommandLineOptionNameMap optionMap, @string cwd) {
    var option = optionMap.Get(o);
    if (option == nil) {
        return (default, false);
    }
    if (option.Kind == "list"u8) {
        if (option.Elements().IsFilePath) {
            {
                var (arr, ok) = v._<slice<@string>>(ᐧ); if (ok) {
                    return (core.Map(arr, @string (@string item) => tspath.GetNormalizedAbsolutePath(item, cwd)), true);
                }
            }
            {
                var (arr, ok) = v._<slice<any>>(ᐧ); if (ok) {
                    return (core.Map(arr, any (any item) => {
                        var (s, isStr) = item._<@string>(ᐧ); if (isStr) {
                            return tspath.GetNormalizedAbsolutePath(s, cwd);
                        }
                        return item;
                    }), true);
                }
            }
        }
    } else if (option.IsFilePath) {
        var (value, ok) = v._<@string>(ᐧ); if (ok) {
            return (tspath.GetNormalizedAbsolutePath(value, cwd), true);
        }
    }
    return (default, false);
}

} // end tsoptions_package
