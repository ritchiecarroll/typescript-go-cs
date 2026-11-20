namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using static core_package;

partial class core_package {

internal static void _ᴛ7() {
    array<EmptyStruct> x = new(1);
    _ = x[TSUnknown - 0];
    _ = x[TSFalse - 1];
    _ = x[TSTrue - 2];
}

internal static readonly @string _Tristate_name = "TSUnknownTSFalseTSTrue"u8;

internal static array<uint8> _Tristate_index = new uint8[]{0, 9, 16, 22}.array();

} partial class coreᴛTristate {

public static @string String(this Tristate i) {
    nint idx = (nint)i - 0;
    if (i < 0 || idx >= len(_Tristate_index) - 1) {
        return "Tristate("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }
    return _Tristate_name.slice(_Tristate_index[idx], _Tristate_index[idx + 1]);
}

} // end coreᴛTristate
