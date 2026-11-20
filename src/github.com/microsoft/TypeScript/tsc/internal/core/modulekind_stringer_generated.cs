namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using static core_package;

partial class core_package {

internal static void _ᴛ2() {
    array<EmptyStruct> x = new(1);
    _ = x[ModuleKindNone - 0];
    _ = x[ModuleKindCommonJS - 1];
    _ = x[ModuleKindAMD - 2];
    _ = x[ModuleKindUMD - 3];
    _ = x[ModuleKindSystem - 4];
    _ = x[ModuleKindES2015 - 5];
    _ = x[ModuleKindES2020 - 6];
    _ = x[ModuleKindES2022 - 7];
    _ = x[ModuleKindESNext - 99];
    _ = x[ModuleKindNode16 - 100];
    _ = x[ModuleKindNode18 - 101];
    _ = x[ModuleKindNode20 - 102];
    _ = x[ModuleKindNodeNext - 199];
    _ = x[ModuleKindPreserve - 200];
}

internal static readonly @string _ModuleKind_name_0 = "NoneCommonJSAMDUMDSystemES2015ES2020ES2022"u8;
internal static readonly @string _ModuleKind_name_1 = "ESNextNode16Node18Node20"u8;
internal static readonly @string _ModuleKind_name_2 = "NodeNextPreserve"u8;

internal static array<uint8> _ModuleKind_index_0 = new uint8[]{0, 4, 12, 15, 18, 24, 30, 36, 42}.array();
internal static array<uint8> _ModuleKind_index_1 = new uint8[]{0, 6, 12, 18, 24}.array();
internal static array<uint8> _ModuleKind_index_2 = new uint8[]{0, 8, 16}.array();

} partial class coreᴛModuleKind {

public static @string String(this ModuleKind i) {
    switch (ᐧ) {
    case {} when 0 <= i && i <= 7: {
        return _ModuleKind_name_0.slice(_ModuleKind_index_0[i], _ModuleKind_index_0[i + 1]);
    }
    case {} when 99 <= i && i <= 102: {
        i -= 99;
        return _ModuleKind_name_1.slice(_ModuleKind_index_1[i], _ModuleKind_index_1[i + 1]);
    }
    case {} when 199 <= i && i <= 200: {
        i -= 199;
        return _ModuleKind_name_2.slice(_ModuleKind_index_2[i], _ModuleKind_index_2[i + 1]);
    }
    default: {
        return "ModuleKind("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }}

}

} // end coreᴛModuleKind
