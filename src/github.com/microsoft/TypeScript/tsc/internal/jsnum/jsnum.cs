namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using math = math_package;
using big = go.math.big_package;
using go.math;
using static jsnum_package;

partial class jsnum_package {

public static Number MaxSafeInteger => /* 1<<53 - 1 */ 9.007199254740991e+15;
public static Number MinSafeInteger => /* -MaxSafeInteger */ -9.007199254740991e+15;

partial struct Number /*num:float64*/;

public static Number NaN() {
    return (Number)math.NaN();
}

} partial class jsnumᴛNumber {

public static bool IsNaN(this Number n) {
    return math.IsNaN((float64)n);
}

} partial class jsnum_package {

public static Number Inf(nint sign) {
    return (Number)math.Inf(sign);
}

} partial class jsnumᴛNumber {

public static bool IsInf(this Number n) {
    return math.IsInf((float64)n, 0);
}

} partial class jsnum_package {

internal static bool isNonFinite(float64 x) {
    const uint64 mask = 0x7FF0000000000000;
    return (uint64)(math.Float64bits(x) & mask) == mask;
}

} partial class jsnumᴛNumber {

internal static uint32 toUint32(this Number x) {
    return (uint32)x.toInt32();
}

internal static int32 toInt32(this Number n) {
    var x = (float64)n;

    var smi = (int32)x; if ((float64)smi == x) {
        return smi;
    }

    if (isNonFinite(x)) {
        return 0;
    }

    x = math.Trunc(x);
    x = math.Mod(x, 4294967296D);
    return (int32)(int64)x;
}

internal static uint32 toShiftCount(this Number x) {
    return x.toUint32() & 31;
}

public static Number SignedRightShift(this Number x, Number y) {
    return (Number)(float64)(x.toInt32().Rsh((uint64)(y.toShiftCount())));
}

public static Number UnsignedRightShift(this Number x, Number y) {
    return (Number)(float64)(x.toUint32().Rsh((uint64)(y.toShiftCount())));
}

public static Number LeftShift(this Number x, Number y) {
    return (Number)(float64)(x.toInt32().Lsh((uint64)(y.toShiftCount())));
}

public static Number BitwiseNOT(this Number x) {
    return (Number)(float64)(~x.toInt32());
}

public static Number BitwiseOR(this Number x, Number y) {
    return (Number)(float64)((x.toInt32() | y.toInt32()));
}

public static Number BitwiseAND(this Number x, Number y) {
    return (Number)(float64)((x.toInt32() & y.toInt32()));
}

public static Number BitwiseXOR(this Number x, Number y) {
    return (Number)(float64)((x.toInt32() ^ y.toInt32()));
}

internal static Number trunc(this Number x) {
    return (Number)math.Trunc((float64)x);
}

public static Number Floor(this Number x) {
    return (Number)math.Floor((float64)x);
}

public static Number Abs(this Number x) {
    return (Number)math.Abs((float64)x);
}

} partial class jsnum_package {

internal static Number negativeZero = ((Number)math.Copysign(0D, -1D));

} partial class jsnumᴛNumber {

public static Number Remainder(this Number n, Number d) {
    switch (ᐧ) {
    case {} when n.IsNaN() || d.IsNaN(): {
        return NaN();
    }
    case {} when n.IsInf(): {
        return NaN();
    }
    case {} when d.IsInf(): {
        return n;
    }
    case {} when d == 0D: {
        return NaN();
    }
    case {} when n == 0D: {
        return n;
    }}

    return (Number)math.Mod((float64)n, (float64)d);
}

public static Number Exponentiate(this Number @base, Number exponent) {
    switch (ᐧ) {
    case {} when (@base == 1D || @base == -1D) && exponent.IsInf(): {
        return NaN();
    }
    case {} when @base == 1D && exponent.IsNaN(): {
        return NaN();
    }}

    var b = (float64)@base;
    var e = (float64)exponent;

    if (b >= math.MinInt64 && b <= math.MaxInt64 && b == math.Trunc(b) && e >= 0D && e <= math.MaxInt64 && e == math.Trunc(e) && !math.IsInf(e, 0)) {
        var magnitude = e * math.Log2(math.Abs(b));
        if (magnitude > 53D && magnitude <= math.Log2(math.MaxFloat64)) {
            var ri = @new<big.Int>().Exp(big.NewInt((int64)b), big.NewInt((int64)e), nil);
            var (result, _) = @new<big.Float>().SetPrec(256).SetInt(ri).Float64();
            return (Number)result;
        }
    }

    return (Number)math.Pow(b, e);
}

} // end jsnumᴛNumber
