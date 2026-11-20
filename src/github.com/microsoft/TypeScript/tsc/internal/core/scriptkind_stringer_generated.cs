namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using static core_package;

partial class core_package {

internal static void _ᴛ3() {
    array<EmptyStruct> x = new(1);
    _ = x[ScriptKindUnknown - 0];
    _ = x[ScriptKindJS - 1];
    _ = x[ScriptKindJSX - 2];
    _ = x[ScriptKindTS - 3];
    _ = x[ScriptKindTSX - 4];
    _ = x[ScriptKindJSON - 6];
}

internal static readonly @string _ScriptKind_name_0 = "ScriptKindUnknownScriptKindJSScriptKindJSXScriptKindTSScriptKindTSX"u8;
internal static readonly @string _ScriptKind_name_1 = "ScriptKindJSON"u8;

internal static array<uint8> _ScriptKind_index_0 = new uint8[]{0, 17, 29, 42, 54, 67}.array();

} partial class coreᴛScriptKind {

public static @string String(this ScriptKind i) {
    switch (ᐧ) {
    case {} when 0 <= i && i <= 4: {
        return _ScriptKind_name_0.slice(_ScriptKind_index_0[i], _ScriptKind_index_0[i + 1]);
    }
    case {} when i == 6: {
        return _ScriptKind_name_1;
    }
    default: {
        return "ScriptKind("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }}

}

} // end coreᴛScriptKind
