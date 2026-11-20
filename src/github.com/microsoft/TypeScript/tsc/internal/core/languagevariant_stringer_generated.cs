namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using static core_package;

partial class core_package {

internal static void _ᴛ1() {
    array<EmptyStruct> x = new(1);
    _ = x[LanguageVariantStandard - 0];
    _ = x[LanguageVariantJSX - 1];
}

internal static readonly @string _LanguageVariant_name = "LanguageVariantStandardLanguageVariantJSX"u8;

internal static array<uint8> _LanguageVariant_index = new uint8[]{0, 23, 41}.array();

} partial class coreᴛLanguageVariant {

public static @string String(this LanguageVariant i) {
    nint idx = (nint)i - 0;
    if (i < 0 || idx >= len(_LanguageVariant_index) - 1) {
        return "LanguageVariant("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }
    return _LanguageVariant_name.slice(_LanguageVariant_index[idx], _LanguageVariant_index[idx + 1]);
}

} // end coreᴛLanguageVariant
