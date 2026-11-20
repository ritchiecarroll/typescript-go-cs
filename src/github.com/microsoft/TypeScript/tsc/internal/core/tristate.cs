namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using static core_package;

partial class core_package {

partial struct Tristate /*num:byte*/;

public static Tristate TSUnknown => /* iota */ 0;
public static Tristate TSFalse => 1;
public static Tristate TSTrue => 2;

} partial class coreᴛTristate {

public static bool IsTrue(this Tristate t) {
    return t == TSTrue;
}

public static bool IsTrueOrUnknown(this Tristate t) {
    return t == TSTrue || t == TSUnknown;
}

public static bool IsFalse(this Tristate t) {
    return t == TSFalse;
}

public static bool IsFalseOrUnknown(this Tristate t) {
    return t == TSFalse || t == TSUnknown;
}

public static bool IsUnknown(this Tristate t) {
    return t == TSUnknown;
}

public static Tristate DefaultIfUnknown(this Tristate t, Tristate value) {
    if (t == TSUnknown) {
        return value;
    }
    return t;
}

public static error UnmarshalJSON(this ref Tristate t, slice<byte> data) {
    var exprᴛ1 = ((sstring)data);
    switch (ᐧ) {
    case {} when exprᴛ1 == "true"u8: {
        t = TSTrue;
        break;
    }
    case {} when exprᴛ1 == "false"u8: {
        t = TSFalse;
        break;
    }
    default: {
        t = TSUnknown;
        break;
    }}

    return default;
}

public static (slice<byte>, error) MarshalJSON(this Tristate t) {
    switch (t) {
    case var ᴛ when ᴛ == TSTrue: {
        return (slice<byte>("true"u8), default);
    }
    case var ᴛ when ᴛ == TSFalse: {
        return (slice<byte>("false"u8), default);
    }
    default: {
        return (slice<byte>("null"u8), default);
    }}

}

} partial class core_package {

public static Tristate BoolToTristate(bool b) {
    if (b) {
        return TSTrue;
    }
    return TSFalse;
}

} // end core_package
