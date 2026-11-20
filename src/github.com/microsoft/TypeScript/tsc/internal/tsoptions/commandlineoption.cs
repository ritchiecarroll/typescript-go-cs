global using CompilerOptionsValue = object;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tsoptions_package;

partial class tsoptions_package {

partial struct CommandLineOptionKind /*@string*/;

public static readonly CommandLineOptionKind CommandLineOptionTypeString = "string"u8;
public static readonly CommandLineOptionKind CommandLineOptionTypeNumber = "number"u8;
public static readonly CommandLineOptionKind CommandLineOptionTypeBoolean = "boolean"u8;
public static readonly CommandLineOptionKind CommandLineOptionTypeObject = "object"u8;
public static readonly CommandLineOptionKind CommandLineOptionTypeList = "list"u8;
public static readonly CommandLineOptionKind CommandLineOptionTypeListOrElement = "listOrElement"u8;
public static readonly CommandLineOptionKind CommandLineOptionTypeEnum = "enum"u8;

partial struct CommandLineOption {
    public @string Name, ShortName;
    public CommandLineOptionKind Kind;
    public bool IsFilePath;
    public bool IsTSConfigOnly;
    public bool IsCommandLineOnly;
    public ж<diagnostics.Message> Description;
    public any DefaultValueDescription;
    public bool ShowInSimplifiedHelpView;
    public ж<diagnostics.Message> Category;
    internal extraValidation extraValidation;
    internal nint minValue;
    internal bool allowConfigDirTemplateSubstitution;
    public bool AffectsDeclarationPath;
    public bool AffectsProgramStructure;
    public bool AffectsSemanticDiagnostics;
    public bool AffectsBuildInfo;
    public bool AffectsBindDiagnostics;
    public bool AffectsSourceFile;
    public bool AffectsModuleResolution;
    public bool AffectsEmit;
    internal bool allowJsFlag;
    internal bool strictFlag;
    internal core.Tristate transpileOptionValue;
    internal bool listPreserveFalsyValues;
    public CommandLineOptionNameMap ElementOptions;
}

partial struct extraValidation /*@string*/;

internal static readonly extraValidation extraValidationNone = ""u8;
internal static readonly extraValidation extraValidationSpec = "spec"u8;
internal static readonly extraValidation extraValidationLocale = "locale"u8;

} partial class tsoptionsᴛCommandLineOption {

public static ж<collections.Set<@string>> DeprecatedKeys(this ref CommandLineOption o) {
    if (o.Kind != CommandLineOptionTypeEnum) {
        return default;
    }
    return commandLineOptionDeprecated[o.Name];
}

public static ж<collections.OrderedMap<@string, any>> EnumMap(this ref CommandLineOption o) {
    if (o.Kind != CommandLineOptionTypeEnum) {
        return default;
    }
    return commandLineOptionEnumMap[o.Name];
}

public static ж<CommandLineOption> Elements(this ref CommandLineOption o) {
    if (o.Kind != CommandLineOptionTypeList && o.Kind != CommandLineOptionTypeListOrElement) {
        return default;
    }
    return commandLineOptionElements[o.Name];
}

public static bool DisallowNullOrUndefined(this ref CommandLineOption o) {
    return o.Name == "extends"u8;
}

} partial class tsoptions_package {

[GoLazy] partial class lazyᴛcommandLineOptionElements { internal static map<@string, ж<CommandLineOption>> commandLineOptionElements = new map<@string, ж<CommandLineOption>>{
    ["lib"] = Ꮡ(new CommandLineOption(
        Name: "lib",
        Kind: CommandLineOptionTypeEnum,
        DefaultValueDescription: core.TSUnknown)),
    ["rootDirs"] = Ꮡ(new CommandLineOption(
        Name: "rootDirs",
        Kind: CommandLineOptionTypeString,
        IsFilePath: true)),
    ["typeRoots"] = Ꮡ(new CommandLineOption(
        Name: "typeRoots",
        Kind: CommandLineOptionTypeString,
        IsFilePath: true)),
    ["types"] = Ꮡ(new CommandLineOption(
        Name: "types",
        Kind: CommandLineOptionTypeString)),
    ["moduleSuffixes"] = Ꮡ(new CommandLineOption(
        Name: "moduleSuffixes",
        Kind: CommandLineOptionTypeString)),
    ["customConditions"] = Ꮡ(new CommandLineOption(
        Name: "condition",
        Kind: CommandLineOptionTypeString)),
    ["plugins"] = Ꮡ(new CommandLineOption(
        Name: "plugin",
        Kind: CommandLineOptionTypeObject)),
    ["references"] = Ꮡ(new CommandLineOption(
        Name: "references",
        Kind: CommandLineOptionTypeObject)),
    ["contentMappers"] = Ꮡ(new CommandLineOption(
        Name: "contentMappers",
        Kind: CommandLineOptionTypeObject)),
    ["files"] = Ꮡ(new CommandLineOption(
        Name: "files",
        Kind: CommandLineOptionTypeString)),
    ["include"] = Ꮡ(new CommandLineOption(
        Name: "include",
        Kind: CommandLineOptionTypeString)),
    ["exclude"] = Ꮡ(new CommandLineOption(
        Name: "exclude",
        Kind: CommandLineOptionTypeString)),
    ["extends"] = Ꮡ(new CommandLineOption(
        Name: "extends",
        Kind: CommandLineOptionTypeString)),
    ["excludeDirectories"] = Ꮡ(new CommandLineOption(
        Name: "excludeDirectory",
        Kind: CommandLineOptionTypeString,
        IsFilePath: true,
        extraValidation: extraValidationSpec)),
    ["excludeFiles"] = Ꮡ(new CommandLineOption(
        Name: "excludeFile",
        Kind: CommandLineOptionTypeString,
        IsFilePath: true,
        extraValidation: extraValidationSpec)),
    ["libFiles"] = Ꮡ(new CommandLineOption(
        Name: "libFiles",
        Kind: CommandLineOptionTypeString))
}; }

internal static map<@string, ж<collections.OrderedMap<@string, any>>> commandLineOptionEnumMap;
internal static void initᴛcommandLineOptionEnumMap() { commandLineOptionEnumMap = new map<@string, ж<collections.OrderedMap<@string, any>>>{
    ["lib"] = LibMap,
    ["moduleResolution"] = moduleResolutionOptionMap,
    ["module"] = moduleOptionMap,
    ["target"] = targetOptionMap,
    ["moduleDetection"] = moduleDetectionOptionMap,
    ["jsx"] = jsxOptionMap,
    ["newLine"] = newLineOptionMap,
    ["watchFile"] = watchFileEnumMap,
    ["watchDirectory"] = watchDirectoryEnumMap,
    ["fallbackPolling"] = fallbackEnumMap
}; }

internal static map<@string, ж<collections.Set<@string>>> commandLineOptionDeprecated = new map<@string, ж<collections.Set<@string>>>{
    ["module"] = collections.NewSetFromItems((@string)"none", (@string)"amd", (@string)"system", (@string)"umd"),
    ["moduleResolution"] = collections.NewSetFromItems((@string)"node", (@string)"classic", (@string)"node10"),
    ["target"] = collections.NewSetFromItems((@string)"es5")
};

} // end tsoptions_package
