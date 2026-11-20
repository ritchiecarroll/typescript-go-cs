namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using static checker_package;

partial class checker_package {

internal static void _ᴛ2() {
    array<EmptyStruct> x = new(1);
    _ = x[SignatureKindCall - 0];
    _ = x[SignatureKindConstruct - 1];
}

internal static readonly @string _SignatureKind_name = "SignatureKindCallSignatureKindConstruct"u8;

internal static array<uint8> _SignatureKind_index = new uint8[]{0, 17, 39}.array();

} partial class checkerᴛSignatureKind {

public static @string String(this SignatureKind i) {
    nint idx = (nint)i - 0;
    if (i < 0 || idx >= len(_SignatureKind_index) - 1) {
        return "SignatureKind("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }
    return _SignatureKind_name.slice(_SignatureKind_index[idx], _SignatureKind_index[idx + 1]);
}

} // end checkerᴛSignatureKind
