namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class tsoptions_package {

internal static ж<CommandLineOption> typeAcquisitionDeclaration;
internal static void initᴛtypeAcquisitionDeclaration() { typeAcquisitionDeclaration = Ꮡ(new CommandLineOption(
    Name: "typeAcquisition",
    Kind: CommandLineOptionTypeObject,
    ElementOptions: commandLineOptionsToMap(typeAcquisitionDecls)
)); }

[GoLazy] partial class lazyᴛtypeAcquisitionDecls { internal static slice<ж<CommandLineOption>> typeAcquisitionDecls = [
    Ꮡ(new CommandLineOption(
        Name: "enable",
        Kind: CommandLineOptionTypeBoolean,
        DefaultValueDescription: false)),
    Ꮡ(new CommandLineOption(
        Name: "include",
        Kind: CommandLineOptionTypeList)),
    Ꮡ(new CommandLineOption(
        Name: "exclude",
        Kind: CommandLineOptionTypeList)),
    Ꮡ(new CommandLineOption(
        Name: "disableFilenameBasedTypeAcquisition",
        Kind: CommandLineOptionTypeBoolean,
        DefaultValueDescription: false))
]; }

} // end tsoptions_package
