namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class tsoptions_package {

public static readonly ж<slice<ж<CommandLineOption>>> ᏑOptionsForWatch = new StaticBox<slice<ж<CommandLineOption>>>(default(slice<ж<CommandLineOption>>));
public static ref slice<ж<CommandLineOption>> OptionsForWatch => ref ((StaticBox<slice<ж<CommandLineOption>>>)ᏑOptionsForWatch).ValueSlot;
internal static void initᴛOptionsForWatch() { OptionsForWatch = [
    Ꮡ(new CommandLineOption(
        Name: "watchInterval",
        Kind: CommandLineOptionTypeNumber,
        Category: diagnostics.Watch_and_Build_Modes)),
    Ꮡ(new CommandLineOption(
        Name: "watchFile",
        Kind: CommandLineOptionTypeEnum,
        Category: diagnostics.Watch_and_Build_Modes,
        Description: diagnostics.Specify_how_the_TypeScript_watch_mode_works,
        DefaultValueDescription: core.WatchFileKindUseFsEvents)),
    Ꮡ(new CommandLineOption(
        Name: "watchDirectory",
        Kind: CommandLineOptionTypeEnum,
        Category: diagnostics.Watch_and_Build_Modes,
        Description: diagnostics.Specify_how_directories_are_watched_on_systems_that_lack_recursive_file_watching_functionality,
        DefaultValueDescription: core.WatchDirectoryKindUseFsEvents)),
    Ꮡ(new CommandLineOption(
        Name: "fallbackPolling",
        Kind: CommandLineOptionTypeEnum,
        Category: diagnostics.Watch_and_Build_Modes,
        Description: diagnostics.Specify_what_approach_the_watcher_should_use_if_the_system_runs_out_of_native_file_watchers,
        DefaultValueDescription: core.PollingKindPriorityInterval)),
    Ꮡ(new CommandLineOption(
        Name: "synchronousWatchDirectory",
        Kind: CommandLineOptionTypeBoolean,
        Category: diagnostics.Watch_and_Build_Modes,
        Description: diagnostics.Synchronously_call_callbacks_and_update_the_state_of_directory_watchers_on_platforms_that_don_t_support_recursive_watching_natively,
        DefaultValueDescription: false)),
    Ꮡ(new CommandLineOption(
        Name: "excludeDirectories",
        Kind: CommandLineOptionTypeList,
        allowConfigDirTemplateSubstitution: true,
        Category: diagnostics.Watch_and_Build_Modes,
        Description: diagnostics.Remove_a_list_of_directories_from_the_watch_process)),
    Ꮡ(new CommandLineOption(
        Name: "excludeFiles",
        Kind: CommandLineOptionTypeList,
        allowConfigDirTemplateSubstitution: true,
        Category: diagnostics.Watch_and_Build_Modes,
        Description: diagnostics.Remove_a_list_of_files_from_the_watch_mode_s_processing))
]; }

} // end tsoptions_package
