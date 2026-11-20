namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using static diagnostics_package;

partial class diagnostics_package {

internal static void _ᴛ1() {
    array<EmptyStruct> x = new(1);
    _ = x[CategoryWarning - 0];
    _ = x[CategoryError - 1];
    _ = x[CategorySuggestion - 2];
    _ = x[CategoryMessage - 3];
}

internal static readonly @string _Category_name = "CategoryWarningCategoryErrorCategorySuggestionCategoryMessage"u8;

internal static array<uint8> _Category_index = new uint8[]{0, 15, 28, 46, 61}.array();

} partial class diagnosticsᴛCategory {

public static @string String(this Category i) {
    nint idx = (nint)i - 0;
    if (i < 0 || idx >= len(_Category_index) - 1) {
        return "Category("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }
    return _Category_name.slice(_Category_index[idx], _Category_index[idx + 1]);
}

} // end diagnosticsᴛCategory
