namespace go.github.com.microsoft.TypeScript.tsc.@internal.vfs;

using strconv = strconv_package;
using static vfsmatch_package;

partial class vfsmatch_package {

internal static void _ᴛ1() {
    array<EmptyStruct> x = new(1);
    _ = x[UsageFiles - 0];
    _ = x[UsageDirectories - 1];
    _ = x[UsageExclude - 2];
}

internal static readonly @string _Usage_name = "FilesDirectoriesExclude"u8;

internal static array<uint8> _Usage_index = new uint8[]{0, 5, 16, 23}.array();

} partial class vfsmatchᴛUsage {

public static @string String(this Usage i) {
    nint idx = (nint)i - 0;
    if (i < 0 || idx >= len(_Usage_index) - 1) {
        return "Usage("u8 + strconv.FormatInt((int64)i, 10) + ")"u8;
    }
    return _Usage_name.slice(_Usage_index[idx], _Usage_index[idx + 1]);
}

} // end vfsmatchᴛUsage
