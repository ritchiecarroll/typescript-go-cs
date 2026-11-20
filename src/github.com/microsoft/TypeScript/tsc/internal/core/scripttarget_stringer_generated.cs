namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using static core_package;

partial class core_package {

internal static void _ᴛ4() {
    array<EmptyStruct> x = new(1);
    _ = x[ScriptTargetNone - 0];
    _ = x[ScriptTargetES5 - 1];
    _ = x[ScriptTargetES2015 - 2];
    _ = x[ScriptTargetES2016 - 3];
    _ = x[ScriptTargetES2017 - 4];
    _ = x[ScriptTargetES2018 - 5];
    _ = x[ScriptTargetES2019 - 6];
    _ = x[ScriptTargetES2020 - 7];
    _ = x[ScriptTargetES2021 - 8];
    _ = x[ScriptTargetES2022 - 9];
    _ = x[ScriptTargetES2023 - 10];
    _ = x[ScriptTargetES2024 - 11];
    _ = x[ScriptTargetES2025 - 12];
    _ = x[ScriptTargetESNext - 99];
    _ = x[ScriptTargetJSON - 100];
    _ = x[ScriptTargetLatest - 99];
    _ = x[ScriptTargetLatestStandard - 12];
}

internal static readonly @string _ScriptTarget_name_0 = "NoneES5ES2015ES2016ES2017ES2018ES2019ES2020ES2021ES2022ES2023ES2024ES2025"u8;
internal static readonly @string _ScriptTarget_name_1 = "ESNextJSON"u8;

internal static array<uint8> _ScriptTarget_index_0 = new uint8[]{0, 4, 7, 13, 19, 25, 31, 37, 43, 49, 55, 61, 67, 73}.array();
internal static array<uint8> _ScriptTarget_index_1 = new uint8[]{0, 6, 10}.array();

} partial class coreᴛScriptTarget {

public static @string String(this ScriptTarget i) {
    switch (ᐧ) {
    case {} when 0 <= i && i <= 12: {
        return _ScriptTarget_name_0.slice(_ScriptTarget_index_0[i], _ScriptTarget_index_0[i + 1]);
    }
    case {} when 99 <= i && i <= 100: {
        i -= 99;
        return _ScriptTarget_name_1.slice(_ScriptTarget_index_1[i], _ScriptTarget_index_1[i + 1]);
    }
    default: {
        return "ScriptTarget("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }}

}

} // end coreᴛScriptTarget
