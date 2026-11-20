namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using static core_package;

partial class core_package {

partial struct TypeAcquisition {
    public Tristate Enable; /*`json:"enable,omitzero"`*/
    public slice<@string> Include; /*`json:"include,omitzero"`*/
    public slice<@string> Exclude; /*`json:"exclude,omitzero"`*/
    public Tristate DisableFilenameBasedTypeAcquisition; /*`json:"disableFilenameBasedTypeAcquisition,omitzero"`*/
}

} partial class coreᴛTypeAcquisition {

public static bool ΔEquals(this ж<TypeAcquisition> Ꮡta, ж<TypeAcquisition> Ꮡother) {
    ref var ta = ref Ꮡta.DerefOrNull();
    ref var other = ref Ꮡother.DerefOrNull();

    if (Ꮡta == Ꮡother) {
        return true;
    }
    if (Ꮡta == nil || Ꮡother == nil) {
        return false;
    }

    return (ta.Enable == other.Enable && slices.Equal<slice<@string>, @string>(ta.Include, other.Include) && slices.Equal<slice<@string>, @string>(ta.Exclude, other.Exclude) && ta.DisableFilenameBasedTypeAcquisition == other.DisableFilenameBasedTypeAcquisition);
}

} // end coreᴛTypeAcquisition
