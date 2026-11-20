namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tsoptions_package;

partial class tsoptions_package {

public static ж<NameMap> CompilerNameMap;
internal static void initᴛCompilerNameMap() { CompilerNameMap = GetNameMapFromList(OptionsDeclarations); }
public static ж<NameMap> BuildNameMap;
internal static void initᴛBuildNameMap() { BuildNameMap = GetNameMapFromList(BuildOpts); }
public static ж<NameMap> WatchNameMap;
internal static void initᴛWatchNameMap() { WatchNameMap = GetNameMapFromList(OptionsForWatch); }

public static ж<NameMap> GetNameMapFromList(slice<ж<CommandLineOption>> optDecls) {
    var optionsNames = collections.NewOrderedMapWithSizeHint<@string, ж<CommandLineOption>>(len(optDecls));
    var shortOptionNames = new map<@string, @string>{};
    foreach (var (_, option) in range(optDecls)) {
        optionsNames.Set(strings.ToLower(option.Name), option);
        if (option.ShortName != ""u8) {
            shortOptionNames[option.ShortName] = option.Name;
        }
    }
    return Ꮡ(new NameMap(
        optionsNames: optionsNames,
        shortOptionNames: shortOptionNames
    ));
}

partial struct NameMap {
    internal ж<collections.OrderedMap<@string, ж<CommandLineOption>>> optionsNames;
    internal map<@string, @string> shortOptionNames;
}

} partial class tsoptionsᴛNameMap {

public static ж<CommandLineOption> Get(this ref NameMap nm, @string name) {
    return nm.optionsNames.GetOrZero(strings.ToLower(name));
}

public static ж<CommandLineOption> GetFromShort(this ref NameMap nm, @string shortName) {
    var (name, ok) = nm.shortOptionNames[shortName, ꟷ];
    if (!ok) {
        return default;
    }
    return nm.Get(name);
}

public static ж<CommandLineOption> GetOptionDeclarationFromName(this ref NameMap nm, @string optionName, bool allowShort) {
    optionName = strings.ToLower(optionName);
    if (allowShort) {
        @string @short = nm.shortOptionNames[optionName];
        if (@short != ""u8) {
            optionName = @short;
        }
    }
    return nm.Get(optionName);
}

} // end tsoptionsᴛNameMap
