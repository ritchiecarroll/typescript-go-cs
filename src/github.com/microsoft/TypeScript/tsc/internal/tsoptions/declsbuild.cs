namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class tsoptions_package {

public static readonly ж<CommandLineOption> ᏑTscBuildOption = new StaticBox<CommandLineOption>(new CommandLineOption(
    Name: "build",
    Kind: (@string)"boolean",
    ShortName: "b",
    ShowInSimplifiedHelpView: true,
    Category: diagnostics.Command_line_Options,
    Description: diagnostics.Build_one_or_more_projects_and_their_dependencies_if_out_of_date,
    DefaultValueDescription: false
));
public static ref CommandLineOption TscBuildOption => ref ((StaticBox<CommandLineOption>)ᏑTscBuildOption).ValueSlot;

public static readonly ж<slice<ж<CommandLineOption>>> ᏑOptionsForBuild = new StaticBox<slice<ж<CommandLineOption>>>(default(slice<ж<CommandLineOption>>));
public static ref slice<ж<CommandLineOption>> OptionsForBuild => ref ((StaticBox<slice<ж<CommandLineOption>>>)ᏑOptionsForBuild).ValueSlot;
internal static void initᴛOptionsForBuild() { OptionsForBuild = [
    ᏑTscBuildOption,
    Ꮡ(new CommandLineOption(
        Name: "verbose",
        ShortName: "v",
        Category: diagnostics.Command_line_Options,
        Description: diagnostics.Enable_verbose_logging,
        Kind: (@string)"boolean",
        DefaultValueDescription: false)),
    Ꮡ(new CommandLineOption(
        Name: "dry",
        ShortName: "d",
        Category: diagnostics.Command_line_Options,
        Description: diagnostics.Show_what_would_be_built_or_deleted_if_specified_with_clean,
        Kind: (@string)"boolean",
        DefaultValueDescription: false)),
    Ꮡ(new CommandLineOption(
        Name: "force",
        ShortName: "f",
        Category: diagnostics.Command_line_Options,
        Description: diagnostics.Build_all_projects_including_those_that_appear_to_be_up_to_date,
        Kind: (@string)"boolean",
        DefaultValueDescription: false)),
    Ꮡ(new CommandLineOption(
        Name: "clean",
        Category: diagnostics.Command_line_Options,
        Description: diagnostics.Delete_the_outputs_of_all_projects,
        Kind: (@string)"boolean",
        DefaultValueDescription: false)),
    Ꮡ(new CommandLineOption(
        Name: "builders",
        Kind: CommandLineOptionTypeNumber,
        Category: diagnostics.Command_line_Options,
        Description: diagnostics.Set_the_number_of_projects_to_build_concurrently,
        DefaultValueDescription: diagnostics.X_4_unless_singleThreaded_is_passed.OrTypedNil(),
        minValue: 1)),
    Ꮡ(new CommandLineOption(
        Name: "stopBuildOnErrors",
        Category: diagnostics.Command_line_Options,
        Description: diagnostics.Skip_building_downstream_projects_on_error_in_upstream_project,
        Kind: (@string)"boolean",
        DefaultValueDescription: false))
]; }

public static readonly ж<slice<ж<CommandLineOption>>> ᏑBuildOpts = new StaticBox<slice<ж<CommandLineOption>>>(default(slice<ж<CommandLineOption>>));
public static ref slice<ж<CommandLineOption>> BuildOpts => ref ((StaticBox<slice<ж<CommandLineOption>>>)ᏑBuildOpts).ValueSlot;
internal static void initᴛBuildOpts() { BuildOpts = slices.Concat<slice<ж<CommandLineOption>>, ж<CommandLineOption>>(commonOptionsWithBuild, OptionsForBuild); }

} // end tsoptions_package
