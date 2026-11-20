namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using reflect = reflect_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;

partial class tsoptions_package {

internal static Func<ж<core.CompilerOptions>, any> computeFn<T>(Func<ж<core.CompilerOptions>, T> fn) {
    return (ж<core.CompilerOptions> opts) => (fn(opts)).OrTypedNilOf();
}

partial struct impliedOption {
    internal @string name;
    internal slice<@string> dependencies;
    internal Func<ж<core.CompilerOptions>, any> compute;
}

internal static slice<impliedOption> impliedOptions = slice<impliedOption>([
    new(name: "Module", dependencies: ["Target"], compute: computeFn<core.ModuleKind>(((Func<ж<core.CompilerOptions>, core.ModuleKind>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetEmitModuleKind)))),
    new(name: "ModuleResolution", dependencies: ["Module", "Target"], compute: computeFn<core.ModuleResolutionKind>(((Func<ж<core.CompilerOptions>, core.ModuleResolutionKind>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetModuleResolutionKind)))),
    new(name: "ModuleDetection", dependencies: ["Module", "Target"], compute: computeFn<core.ModuleDetectionKind>(((Func<ж<core.CompilerOptions>, core.ModuleDetectionKind>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetEmitModuleDetectionKind)))),
    new(name: "IsolatedModules", dependencies: ["VerbatimModuleSyntax"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetIsolatedModules)))),
    new(name: "PreserveConstEnums", dependencies: ["IsolatedModules", "VerbatimModuleSyntax"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.ShouldPreserveConstEnums)))),
    new(name: "Declaration", dependencies: ["Composite"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetEmitDeclarations)))),
    new(name: "DeclarationMap", dependencies: ["Declaration", "Composite"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetAreDeclarationMapsEnabled)))),
    new(name: "Incremental", dependencies: ["Composite"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.IsIncremental)))),
    new(name: "UseDefineForClassFields", dependencies: ["Target", "Module"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetUseDefineForClassFields)))),
    new(name: "ResolvePackageJsonExports", dependencies: ["ModuleResolution", "Module", "Target"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetResolvePackageJsonExports)))),
    new(name: "ResolvePackageJsonImports", dependencies: ["ModuleResolution", "ResolvePackageJsonExports", "Module", "Target"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetResolvePackageJsonImports)))),
    new(name: "ResolveJsonModule", dependencies: ["ModuleResolution", "Module", "Target"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetResolveJsonModule)))),
    new(name: "AllowJs", dependencies: ["CheckJs"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetAllowJS)))),
    new(name: "AllowImportingTsExtensions", dependencies: ["RewriteRelativeImportExtensions"], compute: computeFn<bool>(((Func<ж<core.CompilerOptions>, bool>)(global::go.github.com.microsoft.TypeScript.tsc.@internal.coreᴛCompilerOptions.GetAllowImportingTsExtensions))))
]);

partial struct TSConfig {
    public ж<collections.OrderedMap<@string, any>> CompilerOptions; /*`json:"compilerOptions"`*/
    public slice<any> References; /*`json:"references,omitzero"`*/
    public slice<@string> Files; /*`json:"files,omitzero"`*/
    public slice<@string> Include; /*`json:"include,omitzero"`*/
    public slice<@string> Exclude; /*`json:"exclude,omitzero"`*/
    public ж<bool> CompileOnSave; /*`json:"compileOnSave,omitzero"`*/
}

public static ж<TSConfig> ConvertToTSConfig(ж<ParsedCommandLine> ᏑconfigParseResult, @string configFileName) {
    ref var configParseResult = ref ᏑconfigParseResult.DerefOrNull();
    var filesᴷ = new stackAppend<@string>(moves: true);
    var referencesᴷ = new stackAppend<any>(moves: true);

    if (configFileName == ""u8) {
        configFileName = "tsconfig.json";
    }
    @string normalizedConfigPath = tspath.GetNormalizedAbsolutePath(configFileName, configParseResult.GetCurrentDirectory());
    var comparePathsOptions = new tspath.ComparePathsOptions(
        CurrentDirectory: configParseResult.GetCurrentDirectory(),
        UseCaseSensitiveFileNames: configParseResult.UseCaseSensitiveFileNames()
    );

    slice<@string> files = default;
    foreach (var (_, f) in range(configParseResult.FileNames())) {
        @string normalizedFilePath = tspath.GetNormalizedAbsolutePath(f, configParseResult.GetCurrentDirectory());
        @string relativePath = tspath.GetRelativePathFromFile(normalizedConfigPath, normalizedFilePath, comparePathsOptions);
        files = append(filesᴷ, files, relativePath);
    }

    var optionMap = serializeCompilerOptions(ᏑconfigParseResult.CompilerOptions(), normalizedConfigPath, comparePathsOptions);

    foreach (var (_, name) in range(slice<@string>([
        "showConfig", "configFile", "configFilePath", "help", "init",
        "listFilesOnly", "listEmittedFiles", "project", "build", "version"
    ]))) {
        optionMap.Delete(name);
    }

    addImpliedOptions(optionMap, ᏑconfigParseResult.CompilerOptions(), normalizedConfigPath, comparePathsOptions);

    var config = Ꮡ(new TSConfig(
        CompilerOptions: optionMap
    ));

    var refs = configParseResult.ProjectReferences(); if (len(refs) > 0) {
        slice<any> references = default;
        foreach (var (_, r) in range(refs)) {
            var @ref = Ꮡ(new collections.OrderedMap<@string, any>());
            @ref.Set(pathˢ, r.OriginalPath);
            if (r.Circular) {
                @ref.Set(circularˢ, true);
            }
            references = append(referencesᴷ, references, (any)(@ref.OrTypedNil()));
        }
        references = move2heap(referencesᴷ, references);
        config.References = references;
    }

    if (len(files) > 0) {
        files = move2heap(filesᴷ, files);
        config.Files = files;
    }

    if (configParseResult.ConfigFile != nil && configParseResult.ConfigFile.configFileSpecs != nil) {
        var specs = configParseResult.ConfigFile.configFileSpecs;
        var include = filterSameAsDefaultInclude(specs.validatedIncludeSpecs);
        if (len(include) > 0) {
            config.Include = include;
        }
        config.Exclude = specs.validatedExcludeSpecs;
    }

    if (configParseResult.CompileOnSave != nil && configParseResult.CompileOnSave.Value) {
        ref var t = ref heap<bool>(true, out var Ꮡt);
        config.CompileOnSave = Ꮡt;
    }

    return config;
}

internal static slice<@string> filterSameAsDefaultInclude(slice<@string> specs) {
    if (len(specs) == 0) {
        return default;
    }
    if (len(specs) == 1 && specs[0] == defaultIncludeSpec) {
        return default;
    }
    return specs;
}

internal static @string getNameOfCompilerOptionValue(any value, ж<collections.OrderedMap<@string, any>> enumMap) {
    foreach (var (k, v) in range<@string, any>(enumMap.Entries().Invoke)) {
        if (AreEqual(v, value)) {
            return k;
        }
    }
    return "";
}

internal static ж<collections.OrderedMap<@string, any>> serializeCompilerOptions(ж<core.CompilerOptions> options, @string configFilePath, tspath.ComparePathsOptions comparePathsOptions) {
    var result = collections.NewOrderedMapWithSizeHint<@string, any>(32);
    @string configDir = tspath.GetDirectoryPath(configFilePath);

    var optionsValue = reflect.ValueOf(options.OrTypedNil()).Elem();
    var optionsTypeInfo = reflect.TypeFor<core.CompilerOptions>();

    foreach (var i in range(optionsValue.NumField())) {
        var field = optionsTypeInfo.Field(i);
        if (!field.IsExported()) {
            continue;
        }

        var optionDecl = CommandLineCompilerOptionsMap.Get(field.Name);
        if (optionDecl == nil) {
            continue;
        }

        if (optionDecl.Category == diagnostics.Command_line_Options || optionDecl.Category == diagnostics.Output_Formatting) {
            continue;
        }

        var fieldValue = optionsValue.Field(i);

        if (fieldValue.IsZero()) {
            continue;
        }

        @string name = optionDecl.Name;
        var value = fieldValue.Interface();

        var enumMap = optionDecl.EnumMap();
        if (enumMap != nil) {
            @string serialized = serializeEnumValue(value, enumMap);
            if (serialized != ""u8) {
                result.Set(name, serialized);
            }
            continue;
        }

        var exprᴛ1 = optionDecl.Kind;
        switch (ᐧ) {
        case {} when exprᴛ1 == CommandLineOptionTypeListOrElement: {
            debug.Assert(false, "listOrElement option should not reach serialization");
            break;
        }
        case {} when exprᴛ1 == CommandLineOptionTypeList: {
            var elem = optionDecl.Elements();
            if (elem != nil && elem.IsFilePath) {
                var (strs, ok) = value._<slice<@string>>(ᐧ); if (ok) {
                    var relPaths = new slice<@string>(len(strs));
                    foreach (var (j, s) in range(strs)) {
                        @string absPath = tspath.GetNormalizedAbsolutePath(s, configDir);
                        relPaths[j] = tspath.GetRelativePathFromFile(configFilePath, absPath, comparePathsOptions);
                    }
                    result.Set(name, relPaths);
                    continue;
                }
            }
            if (elem != nil && elem.EnumMap() != nil) {
                var elemMap = elem.EnumMap();
                var (strs, ok) = value._<slice<@string>>(ᐧ); if (ok) {
                    var serialized = new slice<@string>(0, len(strs));
                    foreach (var (_, s) in range(strs)) {
                        @string found = getNameOfCompilerOptionValue(s, elemMap);
                        if (found != ""u8) {
                            serialized = append(serialized, found);
                        } else {
                            serialized = append(serialized, s);
                        }
                    }
                    result.Set(name, serialized);
                    continue;
                }
            }
            result.Set(name, value);
            break;
        }
        case {} when exprᴛ1 == CommandLineOptionTypeString: {
            if (optionDecl.IsFilePath) {
                var (s, ok) = value._<@string>(ᐧ); if (ok && s != ""u8) {
                    @string absPath = tspath.GetNormalizedAbsolutePath(s, configDir);
                    result.Set(name, tspath.GetRelativePathFromFile(configFilePath, absPath, comparePathsOptions));
                    continue;
                }
            }
            result.Set(name, value);
            break;
        }
        case {} when exprᴛ1 == CommandLineOptionTypeBoolean: {
            if (value._<core.Tristate>(out var t)) {
                if (t.IsTrue()) {
                    result.Set(name, true);
                } else if (t.IsFalse()) {
                    result.Set(name, false);
                }
            } else {
                result.Set(name, value);
            }
            break;
        }
        case {} when exprᴛ1 == CommandLineOptionTypeNumber: {
            result.Set(name, value);
            break;
        }
        default: {
            result.Set(name, value);
            break;
        }}

    }

    return result;
}

internal static @string serializeEnumValue(any value, ж<collections.OrderedMap<@string, any>> enumMap) {
    var rv = reflect.ValueOf(value);
    if (rv.CanInt()) {
        var intVal = rv.Int();
        foreach (var (k, v) in range<@string, any>(enumMap.Entries().Invoke)) {
            var ev = reflect.ValueOf(v);
            if (ev.CanInt() && ev.Int() == intVal) {
                return k;
            }
        }
    }
    return getNameOfCompilerOptionValue(value, enumMap);
}

internal static void addImpliedOptions(ж<collections.OrderedMap<@string, any>> ᏑoptionMap, ж<core.CompilerOptions> options, @string _Δp2, tspath.ComparePathsOptions _Δp3) {
    ref var optionMap = ref ᏑoptionMap.DerefOrNull();

    var provided = new map<@string, bool>(ᏑoptionMap.Size());
    foreach (var k in range<@string>(ᏑoptionMap.Keys().Invoke)) {
        provided[k] = true;
    }

    var defaultOpts = Ꮡ(new core.CompilerOptions());

    foreach (var (_, entry) in range(impliedOptions)) {
        var optionDecl = CommandLineCompilerOptionsMap.Get(entry.name);
        if (optionDecl == nil) {
            continue;
        }

        if (provided[optionDecl.Name]) {
            continue;
        }

        if (!anyDependencyProvided(entry.dependencies, provided)) {
            continue;
        }

        var implied = entry.compute(options);
        var defaultVal = entry.compute(defaultOpts);

        if (reflect.DeepEqual(implied, defaultVal)) {
            continue;
        }

        var serialized = serializeImpliedOptionValue(optionDecl, implied);
        if (serialized == nil) {
            continue;
        }
        optionMap.Set(optionDecl.Name, serialized);
    }
}

internal static bool anyDependencyProvided(slice<@string> dependencies, map<@string, bool> provided) {
    foreach (var (_, dep) in range(dependencies)) {
        var depDecl = CommandLineCompilerOptionsMap.Get(dep);
        if (depDecl != nil && provided[depDecl.Name]) {
            return true;
        }
    }
    return false;
}

internal static any serializeImpliedOptionValue(ж<CommandLineOption> ᏑoptionDecl, any value) {
    ref var optionDecl = ref ᏑoptionDecl.DerefOrNull();

    if (value == nil) {
        return default;
    }
    var enumMap = optionDecl.EnumMap();
    if (enumMap != nil) {
        @string s = serializeEnumValue(value, enumMap);
        if (s != ""u8) {
            return s;
        }
        return default;
    }
    switch (value.type()) {
    case bool v: {
        return v;
    }
    case core.Tristate v: {
        if (v.IsTrue()) {
            return true;
        } else if (v.IsFalse()) {
            return false;
        }
        return default;
    }}
    return value;
}

} // end tsoptions_package
