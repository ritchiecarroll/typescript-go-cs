namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using cmp = cmp_package;
using fmt = fmt_package;
using big = go.math.big_package;
using strings = strings_package;
using go.math;
using static jsnum_package;

partial class jsnum_package {

partial struct PseudoBigInt {
    public bool Negative;
    public @string Base10Value;
}

public static PseudoBigInt NewPseudoBigInt(@string value, bool negative) {
    value = strings.TrimLeft(value, "0");
    return new PseudoBigInt(Negative: negative && len(value) != 0, Base10Value: value);
}

} partial class jsnumᴛPseudoBigInt {

public static @string String(this PseudoBigInt value) {
    if (len(value.Base10Value) == 0) {
        return "0";
    }
    if (value.Negative) {
        return "-"u8 + value.Base10Value;
    }
    return value.Base10Value;
}

public static nint Sign(this PseudoBigInt value) {
    if (len(value.Base10Value) == 0) {
        return 0;
    }
    if (value.Negative) {
        return -1;
    }
    return 1;
}

public static nint Compare(this PseudoBigInt value, PseudoBigInt other) {
    nint cΔ1 = cmp.Compare(value.Sign(), other.Sign()); if (cΔ1 != 0) {
        return cΔ1;
    }
    nint c = cmp.Compare(len(value.Base10Value), len(other.Base10Value));
    if (c == 0) {
        c = strings.Compare(value.Base10Value, other.Base10Value);
    }
    if (value.Negative) {
        c = -c;
    }
    return c;
}

} partial class jsnum_package {

public static PseudoBigInt ParseValidBigInt(@string text) {
    (text, var negative) = strings.CutPrefix(text, "-");
    return NewPseudoBigInt(ParsePseudoBigInt(text), negative);
}

public static @string ParsePseudoBigInt(@string stringValue) {
    stringValue = strings.TrimSuffix(stringValue, "n");
    byte b1 = default;
    if (len(stringValue) > 1) {
        b1 = stringValue[1];
    }
    switch (b1) {
    case (byte)'b' or (byte)'B' or (byte)'o' or (byte)'O' or (byte)'x' or (byte)'X': {
        break;
    }
    default: {
        stringValue = strings.TrimLeft(stringValue, "0");
        if (stringValue == ""u8) {
            return "0";
        }
        return stringValue;
    }}

    var (bi, ok) = @new<big.Int>().SetString(stringValue, 0);
    if (!ok) {
        throw panic(fmt.Sprintf("Failed to parse big int: %q"u8, stringValue));
    }
    return bi.String();
}

} // end jsnum_package
