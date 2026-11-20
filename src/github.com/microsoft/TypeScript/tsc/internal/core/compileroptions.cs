global using ResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using reflect = reflect_package;
using slices = slices_package;
using strings = strings_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static core_package;

partial class core_package {

partial struct CompilerOptions {
    internal noCopy _;
    public Tristate AllowJs; /*`json:"allowJs,omitzero"`*/
    public Tristate AllowArbitraryExtensions; /*`json:"allowArbitraryExtensions,omitzero"`*/
    public Tristate AllowImportingTsExtensions; /*`json:"allowImportingTsExtensions,omitzero"`*/
    public Tristate AllowNonTsExtensions; /*`json:"allowNonTsExtensions,omitzero"`*/
    public Tristate AllowUmdGlobalAccess; /*`json:"allowUmdGlobalAccess,omitzero"`*/
    public Tristate AllowUnreachableCode; /*`json:"allowUnreachableCode,omitzero"`*/
    public Tristate AllowUnusedLabels; /*`json:"allowUnusedLabels,omitzero"`*/
    public Tristate AssumeChangesOnlyAffectDirectDependencies; /*`json:"assumeChangesOnlyAffectDirectDependencies,omitzero"`*/
    public Tristate CheckJs; /*`json:"checkJs,omitzero"`*/
    public slice<@string> CustomConditions; /*`json:"customConditions,omitzero"`*/
    public Tristate Composite; /*`json:"composite,omitzero"`*/
    public Tristate EmitDeclarationOnly; /*`json:"emitDeclarationOnly,omitzero"`*/
    public Tristate EmitBOM; /*`json:"emitBOM,omitzero"`*/
    public Tristate EmitDecoratorMetadata; /*`json:"emitDecoratorMetadata,omitzero"`*/
    public Tristate Declaration; /*`json:"declaration,omitzero"`*/
    public @string DeclarationDir; /*`json:"declarationDir,omitzero"`*/
    public Tristate DeclarationMap; /*`json:"declarationMap,omitzero"`*/
    public Tristate DeduplicatePackages; /*`json:"deduplicatePackages,omitzero"`*/
    public Tristate DisableSizeLimit; /*`json:"disableSizeLimit,omitzero"`*/
    public Tristate DisableSourceOfProjectReferenceRedirect; /*`json:"disableSourceOfProjectReferenceRedirect,omitzero"`*/
    public Tristate DisableSolutionSearching; /*`json:"disableSolutionSearching,omitzero"`*/
    public Tristate DisableReferencedProjectLoad; /*`json:"disableReferencedProjectLoad,omitzero"`*/
    public Tristate ErasableSyntaxOnly; /*`json:"erasableSyntaxOnly,omitzero"`*/
    public Tristate ExactOptionalPropertyTypes; /*`json:"exactOptionalPropertyTypes,omitzero"`*/
    public Tristate ExperimentalDecorators; /*`json:"experimentalDecorators,omitzero"`*/
    public Tristate ForceConsistentCasingInFileNames; /*`json:"forceConsistentCasingInFileNames,omitzero"`*/
    public Tristate IsolatedModules; /*`json:"isolatedModules,omitzero"`*/
    public Tristate IsolatedDeclarations; /*`json:"isolatedDeclarations,omitzero"`*/
    public Tristate IgnoreConfig; /*`json:"ignoreConfig,omitzero"`*/
    public @string IgnoreDeprecations; /*`json:"ignoreDeprecations,omitzero"`*/
    public Tristate ImportHelpers; /*`json:"importHelpers,omitzero"`*/
    public Tristate InlineSourceMap; /*`json:"inlineSourceMap,omitzero"`*/
    public Tristate InlineSources; /*`json:"inlineSources,omitzero"`*/
    public Tristate Init; /*`json:"init,omitzero"`*/
    public Tristate Incremental; /*`json:"incremental,omitzero"`*/
    public JsxEmit Jsx; /*`json:"jsx,omitzero"`*/
    public @string JsxFactory; /*`json:"jsxFactory,omitzero"`*/
    public @string JsxFragmentFactory; /*`json:"jsxFragmentFactory,omitzero"`*/
    public @string JsxImportSource; /*`json:"jsxImportSource,omitzero"`*/
    public slice<@string> Lib; /*`json:"lib,omitzero"`*/
    public Tristate LibReplacement; /*`json:"libReplacement,omitzero"`*/
    public @string Locale; /*`json:"locale,omitzero"`*/
    public @string MapRoot; /*`json:"mapRoot,omitzero"`*/
    public ModuleKind Module; /*`json:"module,omitzero"`*/
    public ModuleResolutionKind ModuleResolution; /*`json:"moduleResolution,omitzero"`*/
    public slice<@string> ModuleSuffixes; /*`json:"moduleSuffixes,omitzero"`*/
    public ModuleDetectionKind ModuleDetection; /*`json:"moduleDetection,omitzero"`*/
    public NewLineKind NewLine; /*`json:"newLine,omitzero"`*/
    public Tristate NoEmit; /*`json:"noEmit,omitzero"`*/
    public Tristate NoCheck; /*`json:"noCheck,omitzero"`*/
    public Tristate NoErrorTruncation; /*`json:"noErrorTruncation,omitzero"`*/
    public Tristate NoFallthroughCasesInSwitch; /*`json:"noFallthroughCasesInSwitch,omitzero"`*/
    public Tristate NoImplicitAny; /*`json:"noImplicitAny,omitzero"`*/
    public Tristate NoImplicitThis; /*`json:"noImplicitThis,omitzero"`*/
    public Tristate NoImplicitReturns; /*`json:"noImplicitReturns,omitzero"`*/
    public Tristate NoEmitHelpers; /*`json:"noEmitHelpers,omitzero"`*/
    public Tristate NoLib; /*`json:"noLib,omitzero"`*/
    public Tristate NoPropertyAccessFromIndexSignature; /*`json:"noPropertyAccessFromIndexSignature,omitzero"`*/
    public Tristate NoUncheckedIndexedAccess; /*`json:"noUncheckedIndexedAccess,omitzero"`*/
    public Tristate NoEmitOnError; /*`json:"noEmitOnError,omitzero"`*/
    public Tristate NoUnusedLocals; /*`json:"noUnusedLocals,omitzero"`*/
    public Tristate NoUnusedParameters; /*`json:"noUnusedParameters,omitzero"`*/
    public Tristate NoResolve; /*`json:"noResolve,omitzero"`*/
    public Tristate NoImplicitOverride; /*`json:"noImplicitOverride,omitzero"`*/
    public Tristate NoUncheckedSideEffectImports; /*`json:"noUncheckedSideEffectImports,omitzero"`*/
    public @string OutDir; /*`json:"outDir,omitzero"`*/
    public ж<collections.OrderedMap<@string, slice<@string>>> Paths; /*`json:"paths,omitzero"`*/
    public Tristate PreserveConstEnums; /*`json:"preserveConstEnums,omitzero"`*/
    public Tristate PreserveSymlinks; /*`json:"preserveSymlinks,omitzero"`*/
    public @string Project; /*`json:"project,omitzero"`*/
    public Tristate ResolveJsonModule; /*`json:"resolveJsonModule,omitzero"`*/
    public Tristate ResolvePackageJsonExports; /*`json:"resolvePackageJsonExports,omitzero"`*/
    public Tristate ResolvePackageJsonImports; /*`json:"resolvePackageJsonImports,omitzero"`*/
    public Tristate RemoveComments; /*`json:"removeComments,omitzero"`*/
    public Tristate RewriteRelativeImportExtensions; /*`json:"rewriteRelativeImportExtensions,omitzero"`*/
    public @string ReactNamespace; /*`json:"reactNamespace,omitzero"`*/
    public @string RootDir; /*`json:"rootDir,omitzero"`*/
    public slice<@string> RootDirs; /*`json:"rootDirs,omitzero"`*/
    public Tristate SkipLibCheck; /*`json:"skipLibCheck,omitzero"`*/
    public Tristate StableTypeOrdering; /*`json:"stableTypeOrdering,omitzero"`*/
    public Tristate Strict; /*`json:"strict,omitzero"`*/
    public Tristate StrictBindCallApply; /*`json:"strictBindCallApply,omitzero"`*/
    public Tristate StrictBuiltinIteratorReturn; /*`json:"strictBuiltinIteratorReturn,omitzero"`*/
    public Tristate StrictFunctionTypes; /*`json:"strictFunctionTypes,omitzero"`*/
    public Tristate StrictNullChecks; /*`json:"strictNullChecks,omitzero"`*/
    public Tristate StrictPropertyInitialization; /*`json:"strictPropertyInitialization,omitzero"`*/
    public Tristate StripInternal; /*`json:"stripInternal,omitzero"`*/
    public Tristate SkipDefaultLibCheck; /*`json:"skipDefaultLibCheck,omitzero"`*/
    public Tristate SourceMap; /*`json:"sourceMap,omitzero"`*/
    public @string SourceRoot; /*`json:"sourceRoot,omitzero"`*/
    public Tristate SuppressOutputPathCheck; /*`json:"suppressOutputPathCheck,omitzero"`*/
    public ScriptTarget Target; /*`json:"target,omitzero"`*/
    public Tristate TraceResolution; /*`json:"traceResolution,omitzero"`*/
    public @string TsBuildInfoFile; /*`json:"tsBuildInfoFile,omitzero"`*/
    public slice<@string> TypeRoots; /*`json:"typeRoots,omitzero"`*/
    public slice<@string> Types; /*`json:"types,omitzero"`*/
    public Tristate UseDefineForClassFields; /*`json:"useDefineForClassFields,omitzero"`*/
    public Tristate UseUnknownInCatchVariables; /*`json:"useUnknownInCatchVariables,omitzero"`*/
    public Tristate VerbatimModuleSyntax; /*`json:"verbatimModuleSyntax,omitzero"`*/
    public ж<nint> MaxNodeModuleJsDepth; /*`json:"maxNodeModuleJsDepth,omitzero"`*/
    public Tristate AllowSyntheticDefaultImports; /*`json:"allowSyntheticDefaultImports,omitzero" deprecated:"true"`*/
    public Tristate AlwaysStrict; /*`json:"alwaysStrict,omitzero" deprecated:"true"`*/
    public @string BaseUrl; /*`json:"baseUrl,omitzero" deprecated:"true"`*/
    public Tristate DownlevelIteration; /*`json:"downlevelIteration,omitzero" deprecated:"true"`*/
    public Tristate ESModuleInterop; /*`json:"esModuleInterop,omitzero" deprecated:"true"`*/
    public @string OutFile; /*`json:"outFile,omitzero" deprecated:"true"`*/
    public @string ConfigFilePath; /*`json:"configFilePath,omitzero"`*/
    public Tristate NoDtsResolution; /*`json:"noDtsResolution,omitzero" internal:"true"`*/
    public @string PathsBasePath; /*`json:"pathsBasePath,omitzero" internal:"true"`*/
    public Tristate Diagnostics; /*`json:"diagnostics,omitzero" internal:"true"`*/
    public Tristate ExtendedDiagnostics; /*`json:"extendedDiagnostics,omitzero" internal:"true"`*/
    public @string GenerateCpuProfile; /*`json:"generateCpuProfile,omitzero" internal:"true"`*/
    public @string GenerateTrace; /*`json:"generateTrace,omitzero" internal:"true"`*/
    public Tristate ListEmittedFiles; /*`json:"listEmittedFiles,omitzero" internal:"true"`*/
    public Tristate ListFiles; /*`json:"listFiles,omitzero" internal:"true"`*/
    public Tristate ExplainFiles; /*`json:"explainFiles,omitzero" internal:"true"`*/
    public Tristate ListFilesOnly; /*`json:"listFilesOnly,omitzero" internal:"true"`*/
    public Tristate NoEmitForJsFiles; /*`json:"noEmitForJsFiles,omitzero" internal:"true"`*/
    public Tristate PreserveWatchOutput; /*`json:"preserveWatchOutput,omitzero" internal:"true"`*/
    public Tristate Pretty; /*`json:"pretty,omitzero" internal:"true"`*/
    public Tristate Version; /*`json:"version,omitzero" internal:"true"`*/
    public Tristate Watch; /*`json:"watch,omitzero" internal:"true"`*/
    public Tristate ShowConfig; /*`json:"showConfig,omitzero" internal:"true"`*/
    public Tristate Build; /*`json:"build,omitzero" internal:"true"`*/
    public Tristate Help; /*`json:"help,omitzero" internal:"true"`*/
    public Tristate All; /*`json:"all,omitzero" internal:"true"`*/
    public Tristate RunExternalCode; /*`json:"runExternalCode,omitzero" internal:"true"`*/
    public @string PprofDir; /*`json:"pprofDir,omitzero"  internal:"true"`*/
    public Tristate SingleThreaded; /*`json:"singleThreaded,omitzero" internal:"true"`*/
    public Tristate Quiet; /*`json:"quiet,omitzero" internal:"true"`*/
    public ж<nint> Checkers; /*`json:"checkers,omitzero" internal:"true"`*/
}

partial struct noCopy {
}

} partial class coreᴛnoCopy {

internal static void Lock(this ref noCopy _) { }
internal static void Unlock(this ref noCopy _) { }

} partial class core_package {

public static ж<CompilerOptions> EmptyCompilerOptions = Ꮡ(new CompilerOptions());

internal static reflect.Type optionsType = reflect.TypeFor<CompilerOptions>();

} partial class coreᴛCompilerOptions {

public static ж<CompilerOptions> Clone(this ж<CompilerOptions> Ꮡoptions) {
    var target = Ꮡ(new CompilerOptions());

    var sourceValue = reflect.ValueOf(Ꮡoptions.OrTypedNil()).Elem();
    var targetValue = reflect.ValueOf(target.OrTypedNil()).Elem();

    foreach (var i in range(sourceValue.NumField())) {
        if (optionsType.Field(i).IsExported()) {
            targetValue.Field(i).Set(sourceValue.Field(i));
        }
    }

    return target;
}

public static ScriptTarget GetEmitScriptTarget(this ref CompilerOptions options) {
    if (options.Target != ScriptTargetNone) {
        return options.Target;
    }
    return ScriptTargetLatestStandard;
}

public static ModuleKind GetEmitModuleKind(this ref CompilerOptions options) {
    if (options.Module != ModuleKindNone) {
        return options.Module;
    }

    var target = options.GetEmitScriptTarget();
    if (target == ScriptTargetESNext) {
        return ModuleKindESNext;
    }
    if (target >= ScriptTargetES2022) {
        return ModuleKindES2022;
    }
    if (target >= ScriptTargetES2020) {
        return ModuleKindES2020;
    }
    if (target >= ScriptTargetES2015) {
        return ModuleKindES2015;
    }
    return ModuleKindCommonJS;
}

public static ModuleResolutionKind GetModuleResolutionKind(this ref CompilerOptions options) {
    switch (options.ModuleResolution) {
    case var ᴛ when ᴛ == ModuleResolutionKindUnknown || ᴛ == ModuleResolutionKindClassic || ᴛ == ModuleResolutionKindNode10: {
        switch (options.GetEmitModuleKind()) {
        case var ᴛ2 when ᴛ2 == ModuleKindNode16 || ᴛ2 == ModuleKindNode18 || ᴛ2 == ModuleKindNode20: {
            return ModuleResolutionKindNode16;
        }
        case var ᴛ2 when ᴛ2 == ModuleKindNodeNext: {
            return ModuleResolutionKindNodeNext;
        }
        default: {
            return ModuleResolutionKindBundler;
        }}

        break;
    }
    default: {
        return options.ModuleResolution;
    }}

}

public static ModuleDetectionKind GetEmitModuleDetectionKind(this ref CompilerOptions options) {
    if (options.ModuleDetection != ModuleDetectionKindNone) {
        return options.ModuleDetection;
    }
    var moduleKind = options.GetEmitModuleKind();
    if (ModuleKindNode16 <= moduleKind && moduleKind <= ModuleKindNodeNext) {
        return ModuleDetectionKindForce;
    }
    return ModuleDetectionKindAuto;
}

public static bool GetResolvePackageJsonExports(this ref CompilerOptions options) {
    return options.ResolvePackageJsonExports.IsTrueOrUnknown();
}

public static bool GetResolvePackageJsonImports(this ref CompilerOptions options) {
    return options.ResolvePackageJsonImports.IsTrueOrUnknown();
}

public static bool GetAllowImportingTsExtensions(this ref CompilerOptions options) {
    return options.AllowImportingTsExtensions.IsTrue() || options.RewriteRelativeImportExtensions.IsTrue();
}

public static bool AllowImportingTsExtensionsFrom(this ref CompilerOptions options, @string fileName) {
    return options.GetAllowImportingTsExtensions() || tspath.IsDeclarationFileName(fileName);
}

public static bool GetResolveJsonModule(this ref CompilerOptions options) {
    if (options.ResolveJsonModule != TSUnknown) {
        return options.ResolveJsonModule == TSTrue;
    }
    switch (options.GetEmitModuleKind()) {
    case var ᴛ when ᴛ == ModuleKindNode20 || ᴛ == ModuleKindNodeNext: {
        return true;
    }}

    return options.GetModuleResolutionKind() == ModuleResolutionKindBundler;
}

public static bool ShouldPreserveConstEnums(this ref CompilerOptions options) {
    return options.PreserveConstEnums == TSTrue || options.GetIsolatedModules();
}

public static bool GetAllowJS(this ref CompilerOptions options) {
    if (options.AllowJs != TSUnknown) {
        return options.AllowJs == TSTrue;
    }
    return options.CheckJs == TSTrue;
}

public static bool GetJSXTransformEnabled(this ref CompilerOptions options) {
    var jsx = options.Jsx;
    return jsx == JsxEmitReact || jsx == JsxEmitReactJSX || jsx == JsxEmitReactJSXDev;
}

public static bool GetStrictOptionValue(this ref CompilerOptions options, Tristate value) {
    if (value != TSUnknown) {
        return value == TSTrue;
    }
    return options.Strict != TSFalse;
}

public static (slice<@string> result, bool fromConfig) GetEffectiveTypeRoots(this ref CompilerOptions options, @string currentDirectory) {
    if (options.TypeRoots != nil) {
        return (options.TypeRoots, true);
    }
    @string baseDir = default;
    if (options.ConfigFilePath != ""u8) {
        baseDir = tspath.GetDirectoryPath(options.ConfigFilePath);
    } else {
        baseDir = currentDirectory;
        if (baseDir == ""u8) {
            throw panic("cannot get effective type roots without a config file path or current directory");
        }
    }

    ref var typeRoots = ref heap<slice<@string>>(new slice<@string>(0, strings.Count(baseDir, "/")), out var ᏑtypeRoots);
    tspath.ForEachAncestorDirectory(baseDir, (any, bool) (@string dir) => {
        ᏑtypeRoots.ValueSlot = append(ᏑtypeRoots.ValueSlot, tspath.CombinePaths(dir, "node_modules", "@types"));
        return (default, false);
    });
    return (typeRoots, false);
}

public static bool UsesWildcardTypes(this ref CompilerOptions options) {
    return slices.Contains(options.Types, (@string)"*");
}

public static bool GetIsolatedModules(this ref CompilerOptions options) {
    return options.IsolatedModules == TSTrue || options.VerbatimModuleSyntax == TSTrue;
}

public static bool IsIncremental(this ref CompilerOptions options) {
    return options.Incremental.IsTrue() || options.Composite.IsTrue();
}

public static bool GetEmitStandardClassFields(this ref CompilerOptions options) {
    return options.UseDefineForClassFields != TSFalse && options.GetEmitScriptTarget() >= ScriptTargetES2022;
}

public static bool GetUseDefineForClassFields(this ref CompilerOptions options) {
    if (options.UseDefineForClassFields == TSUnknown) {
        return options.GetEmitScriptTarget() >= ScriptTargetES2022;
    }
    return options.UseDefineForClassFields == TSTrue;
}

public static bool GetEmitDeclarations(this ref CompilerOptions options) {
    return options.Declaration.IsTrue() || options.Composite.IsTrue();
}

public static bool GetAreDeclarationMapsEnabled(this ref CompilerOptions options) {
    return options.DeclarationMap == TSTrue && options.GetEmitDeclarations();
}

public static bool HasJsonModuleEmitEnabled(this ref CompilerOptions options) {
    switch (options.GetEmitModuleKind()) {
    case var ᴛ when ᴛ == ModuleKindSystem || ᴛ == ModuleKindUMD: {
        return false;
    }}

    return true;
}

public static @string GetPathsBasePath(this ref CompilerOptions options, @string currentDirectory) {
    if (options.Paths.Size() == 0) {
        return "";
    }
    if (options.PathsBasePath != ""u8) {
        return options.PathsBasePath;
    }
    return currentDirectory;
}

} partial class core_package {

partial struct ModuleDetectionKind /*num:int32*/;

public static ModuleDetectionKind ModuleDetectionKindNone => 0;
public static ModuleDetectionKind ModuleDetectionKindAuto => 1;
public static ModuleDetectionKind ModuleDetectionKindLegacy => 2;
public static ModuleDetectionKind ModuleDetectionKindForce => 3;

partial struct ModuleKind /*num:int32*/;

public static ModuleKind ModuleKindNone => 0;
public static ModuleKind ModuleKindCommonJS => 1;
public static ModuleKind ModuleKindAMD => 2;
public static ModuleKind ModuleKindUMD => 3;
public static ModuleKind ModuleKindSystem => 4;
public static ModuleKind ModuleKindES2015 => 5;
public static ModuleKind ModuleKindES2020 => 6;
public static ModuleKind ModuleKindES2022 => 7;
public static ModuleKind ModuleKindESNext => 99;
public static ModuleKind ModuleKindNode16 => 100;
public static ModuleKind ModuleKindNode18 => 101;
public static ModuleKind ModuleKindNode20 => 102;
public static ModuleKind ModuleKindNodeNext => 199;
public static ModuleKind ModuleKindPreserve => 200;

} partial class coreᴛModuleKind {

public static bool IsNonNodeESM(this ModuleKind moduleKind) {
    return moduleKind >= ModuleKindES2015 && moduleKind <= ModuleKindESNext;
}

public static bool SupportsImportAttributes(this ModuleKind moduleKind) {
    return ModuleKindNode18 <= moduleKind && moduleKind <= ModuleKindNodeNext || moduleKind == ModuleKindPreserve || moduleKind == ModuleKindESNext;
}

} partial class core_package {

public static ModuleKind ResolutionModeNone => /* ModuleKindNone */ 0;
public static ModuleKind ResolutionModeCommonJS => /* ModuleKindCommonJS */ 1;
public static ModuleKind ResolutionModeESM => /* ModuleKindESNext */ 99;

partial struct ModuleResolutionKind /*num:int32*/;

public static ModuleResolutionKind ModuleResolutionKindUnknown => 0;
public static ModuleResolutionKind ModuleResolutionKindClassic => 1;
public static ModuleResolutionKind ModuleResolutionKindNode10 => 2;
public static ModuleResolutionKind ModuleResolutionKindNode16 => 3;
public static ModuleResolutionKind ModuleResolutionKindNodeNext => 99;
public static ModuleResolutionKind ModuleResolutionKindBundler => 100;

public static readonly ж<map<ModuleKind, ModuleResolutionKind>> ᏑModuleKindToModuleResolutionKind = new StaticBox<map<ModuleKind, ModuleResolutionKind>>(new map<ModuleKind, ModuleResolutionKind>{
    [ModuleKindNode16] = ModuleResolutionKindNode16,
    [ModuleKindNodeNext] = ModuleResolutionKindNodeNext
});
public static ref map<ModuleKind, ModuleResolutionKind> ModuleKindToModuleResolutionKind => ref ((StaticBox<map<ModuleKind, ModuleResolutionKind>>)ᏑModuleKindToModuleResolutionKind).ValueSlot;

} partial class coreᴛModuleResolutionKind {

public static @string String(this ModuleResolutionKind m) {
    switch (m) {
    case var ᴛ when ᴛ == ModuleResolutionKindUnknown: {
        throw panic("should not use zero value of ModuleResolutionKind");
        break;
    }
    case var ᴛ when ᴛ == ModuleResolutionKindClassic: {
        return "Classic";
    }
    case var ᴛ when ᴛ == ModuleResolutionKindNode10: {
        return "Node10";
    }
    case var ᴛ when ᴛ == ModuleResolutionKindNode16: {
        return "Node16";
    }
    case var ᴛ when ᴛ == ModuleResolutionKindNodeNext: {
        return "NodeNext";
    }
    case var ᴛ when ᴛ == ModuleResolutionKindBundler: {
        return "Bundler";
    }
    default: {
        throw panic("unhandled case in ModuleResolutionKind.String");
        break;
    }}

}

} partial class core_package {

partial struct NewLineKind /*num:int32*/;

public static NewLineKind NewLineKindNone => 0;
public static NewLineKind NewLineKindCRLF => 1;
public static NewLineKind NewLineKindLF => 2;

public static NewLineKind GetNewLineKind(@string s) {
    switch (ᐧ) {
    case {} when s == "\r\n"u8: {
        return NewLineKindCRLF;
    }
    case {} when s == "\n"u8: {
        return NewLineKindLF;
    }
    default: {
        return NewLineKindNone;
    }}

}

} partial class coreᴛNewLineKind {

public static @string GetNewLineCharacter(this NewLineKind newLine) {
    switch (newLine) {
    case var ᴛ when ᴛ == NewLineKindCRLF: {
        return "\r\n";
    }
    default: {
        return "\n";
    }}

}

} partial class core_package {

partial struct ScriptTarget /*num:int32*/;

public static ScriptTarget ScriptTargetNone => 0;
public static ScriptTarget ScriptTargetES5 => 1;
public static ScriptTarget ScriptTargetES2015 => 2;
public static ScriptTarget ScriptTargetES2016 => 3;
public static ScriptTarget ScriptTargetES2017 => 4;
public static ScriptTarget ScriptTargetES2018 => 5;
public static ScriptTarget ScriptTargetES2019 => 6;
public static ScriptTarget ScriptTargetES2020 => 7;
public static ScriptTarget ScriptTargetES2021 => 8;
public static ScriptTarget ScriptTargetES2022 => 9;
public static ScriptTarget ScriptTargetES2023 => 10;
public static ScriptTarget ScriptTargetES2024 => 11;
public static ScriptTarget ScriptTargetES2025 => 12;
public static ScriptTarget ScriptTargetESNext => 99;
public static ScriptTarget ScriptTargetJSON => 100;
public static ScriptTarget ScriptTargetLatest => /* ScriptTargetESNext */ 99;
public static ScriptTarget ScriptTargetLatestStandard => /* ScriptTargetES2025 */ 12;

partial struct JsxEmit /*num:int32*/;

public static JsxEmit JsxEmitNone => 0;
public static JsxEmit JsxEmitPreserve => 1;
public static JsxEmit JsxEmitReact => 2;
public static JsxEmit JsxEmitReactNative => 3;
public static JsxEmit JsxEmitReactJSX => 4;
public static JsxEmit JsxEmitReactJSXDev => 5;

} partial class coreᴛJsxEmit {

public static @string String(this JsxEmit j) {
    switch (j) {
    case var ᴛ when ᴛ == JsxEmitNone: {
        throw panic("should not use zero value of JsxEmit");
        break;
    }
    case var ᴛ when ᴛ == JsxEmitPreserve: {
        return "preserve";
    }
    case var ᴛ when ᴛ == JsxEmitReactNative: {
        return "react-native";
    }
    case var ᴛ when ᴛ == JsxEmitReact: {
        return "react";
    }
    case var ᴛ when ᴛ == JsxEmitReactJSX: {
        return "react-jsx";
    }
    case var ᴛ when ᴛ == JsxEmitReactJSXDev: {
        return "react-jsxdev";
    }
    default: {
        throw panic("unhandled case in JsxEmit.String");
        break;
    }}

}

} // end coreᴛJsxEmit
