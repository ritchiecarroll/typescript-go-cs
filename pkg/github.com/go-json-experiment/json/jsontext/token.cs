// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using bytes = bytes_package;
using errors = errors_package;
using math = math_package;
using strconv = strconv_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using go.github.com.go_json_experiment.json.@internal;
using static jsontext_package;

partial class jsontext_package {

internal static UntypedInt maxInt64 => /* math.MaxInt64 */ 9223372036854775807;
internal static UntypedInt minInt64 => /* math.MinInt64 */ -9223372036854775808;
internal static UntypedInt maxUint64 => /* math.MaxUint64 */ 18446744073709551615;
internal static UntypedInt minUint64 => 0;
internal static readonly @string invalidTokenPanic = "invalid jsontext.Token; it has been voided by a subsequent json.Decoder call"u8;

internal static error errInvalidToken = errors.New("invalid jsontext.Token");

partial struct Token {
    /*embed*/ internal nonComparable nonComparable;
    internal ж<decodeBuffer> raw;
    internal @string str;
    internal uint64 num;
}

public static readonly ж<Token> ᏑNull = new StaticBox<Token>(rawToken("null"));
public static ref Token Null => ref ((StaticBox<Token>)ᏑNull).ValueSlot;
public static readonly ж<Token> ᏑFalse = new StaticBox<Token>(rawToken("false"));
public static ref Token False => ref ((StaticBox<Token>)ᏑFalse).ValueSlot;
public static readonly ж<Token> ᏑTrue = new StaticBox<Token>(rawToken("true"));
public static ref Token True => ref ((StaticBox<Token>)ᏑTrue).ValueSlot;
public static readonly ж<Token> ᏑBeginObject = new StaticBox<Token>(rawToken("{"));
public static ref Token BeginObject => ref ((StaticBox<Token>)ᏑBeginObject).ValueSlot;
public static readonly ж<Token> ᏑEndObject = new StaticBox<Token>(rawToken("}"));
public static ref Token EndObject => ref ((StaticBox<Token>)ᏑEndObject).ValueSlot;
public static readonly ж<Token> ᏑBeginArray = new StaticBox<Token>(rawToken("["));
public static ref Token BeginArray => ref ((StaticBox<Token>)ᏑBeginArray).ValueSlot;
public static readonly ж<Token> ᏑEndArray = new StaticBox<Token>(rawToken("]"));
public static ref Token EndArray => ref ((StaticBox<Token>)ᏑEndArray).ValueSlot;
internal static Token zeroString = rawToken(@"""""");
internal static Token zeroNumber = rawToken(@"0");
internal static Token nanString = String("NaN");
internal static Token pinfString = String("Infinity");
internal static Token ninfString = String("-Infinity");

internal static Token rawToken(@string s) {
    return new Token(raw: Ꮡ(new decodeBuffer(buf: slice<byte>(s), prevStart: 0, prevEnd: len(s))));
}

public static Token Bool(bool b) {
    if (b) {
        return True;
    }
    return False;
}

public static Token String(@string s) {
    if (len(s) == 0) {
        return zeroString;
    }
    return new Token(str: s);
}

public static Token Float32(float32 n) {
    if (n != 0F && !math.IsNaN((float64)n) && !math.IsInf((float64)n, 0)) {
        return new Token(str: "F", num: (uint64)math.Float32bits(n));
    }
    return Float((float64)n);
}

public static Token Float(float64 n) {
    switch (ᐧ) {
    case {} when math.Float64bits(n) is 0: {
        return zeroNumber;
    }
    case {} when math.IsNaN(n): {
        return nanString;
    }
    case {} when math.IsInf(n, +1): {
        return pinfString;
    }
    case {} when math.IsInf(n, -1): {
        return ninfString;
    }}

    return new Token(str: "f", num: math.Float64bits(n));
}

public static Token Int(int64 n) {
    if (n == 0) {
        return zeroNumber;
    }
    return new Token(str: "i", num: (uint64)n);
}

public static Token Uint(uint64 n) {
    if (n == 0) {
        return zeroNumber;
    }
    return new Token(str: "u", num: (uint64)n);
}

} partial class jsontextᴛToken {

public static Token Clone(this Token t) {
    var raw = t.raw; if (raw != nil) {
        if (t.raw.prevStart == 0) {
            switch (t.raw) {
            case var ᴛ when ᴛ == Null.raw: {
                return Null;
            }
            case var ᴛ when ᴛ == False.raw: {
                return False;
            }
            case var ᴛ when ᴛ == True.raw: {
                return True;
            }
            case var ᴛ when ᴛ == BeginObject.raw: {
                return BeginObject;
            }
            case var ᴛ when ᴛ == EndObject.raw: {
                return EndObject;
            }
            case var ᴛ when ᴛ == BeginArray.raw: {
                return BeginArray;
            }
            case var ᴛ when ᴛ == EndArray.raw: {
                return EndArray;
            }}

        }

        if ((uint64)raw.previousOffsetStart() != t.num) {
            throw panic(invalidTokenPanic);
        }
        var buf = bytes.Clone(raw.previousBuffer());
        return new Token(raw: Ꮡ(new decodeBuffer(buf: buf, prevStart: 0, prevEnd: len(buf))));
    }
    return t;
}

public static bool Bool(this Token t) {
    switch (t.raw) {
    case var ᴛ when ᴛ == True.raw: {
        return true;
    }
    case var ᴛ when ᴛ == False.raw: {
        return false;
    }
    default: {
        throw panic("invalid JSON token kind: " + t.Kind().String());
        break;
    }}

}

internal static (slice<byte>, error) appendString(this Token t, slice<byte> dst, ж<jsonflags.Flags> flags) {
    var raw = t.raw; if (raw != nil) {
        var buf = raw.previousBuffer();
        if (((Kind)buf[0]) == (rune)'"') {
            if (jsonwire.ConsumeSimpleString(buf) == len(buf)) {
                return (appendꓸꓸꓸ(dst, buf), default);
            }
            var (dstΔ1, _, err) = jsonwire.ReformatString(dst, buf, flags);
            return (dstΔ1, err);
        }
    } else if (len(t.str) != 0 && t.num == 0) {
        return jsonwire.AppendQuote(dst, slice<byte>(t.str), flags);
    }

    throw panic("invalid JSON token kind: " + t.Kind().String());
}

public static @string String(this Token t) {
    var (s, b) = t.@string();
    if (len(b) > 0) {
        return (@string)b;
    }
    return s;
}
internal static (@string, slice<byte>) @string(this Token t) {
    var raw = t.raw; if (raw != nil) {
        if ((uint64)raw.previousOffsetStart() != t.num) {
            throw panic(invalidTokenPanic);
        }
        var buf = raw.previousBuffer();
        if (buf[0] == '"') {
            var isVerbatim = jsonwire.ConsumeSimpleString(buf) == len(buf);
            return ("", jsonwire.UnquoteMayCopy(buf, isVerbatim));
        }
        return ("", buf);
    }
    if (len(t.str) != 0 && t.num == 0) {
        return (t.str, default);
    }
    if (t.num > 0) {
        switch (t.str[0]) {
        case (byte)'F': {
            return ((@string)jsonwire.AppendFloat(default, (float64)math.Float32frombits((uint32)t.num), 32), default);
        }
        case (byte)'f': {
            return ((@string)jsonwire.AppendFloat(default, (float64)math.Float64frombits((uint64)t.num), 64), default);
        }
        case (byte)'i': {
            return (strconv.FormatInt((int64)t.num, 10), default);
        }
        case (byte)'u': {
            return (strconv.FormatUint((uint64)t.num, 10), default);
        }}

    }
    return ("<invalid jsontext.Token>", default);
}

internal static (slice<byte>, error) appendNumber(this Token t, slice<byte> dst, ж<jsonflags.Flags> flags) {
    var raw = t.raw; if (raw != nil) {
        var buf = raw.previousBuffer();
        if (((Kind)buf[0]).normalize() == (rune)'0') {
            var (dstΔ1, _, err) = jsonwire.ReformatNumber(dst, buf, flags);
            return (dstΔ1, err);
        }
    } else if (t.num != 0) {
        switch (t.str[0]) {
        case (byte)'F': {
            return (jsonwire.AppendFloat(dst, (float64)math.Float32frombits((uint32)t.num), 32), default);
        }
        case (byte)'f': {
            return (jsonwire.AppendFloat(dst, (float64)math.Float64frombits((uint64)t.num), 64), default);
        }
        case (byte)'i': {
            return (strconv.AppendInt(dst, (int64)t.num, 10), default);
        }
        case (byte)'u': {
            return (strconv.AppendUint(dst, (uint64)t.num, 10), default);
        }}

    }

    throw panic("invalid JSON token kind: " + t.Kind().String());
}

public static (float32, error) Float32(this Token t) {
    var (f, err) = t.@float(32);
    return ((float32)f, err);
}

public static (float64, error) Float(this Token t) {
    var (f, err) = t.@float(64);
    return ((float64)f, err);
}

internal static (float64, error) @float(this Token t, nint bits) {
    var raw = t.raw; if (raw != nil) {
        if ((uint64)raw.previousOffsetStart() != t.num) {
            throw panic(invalidTokenPanic);
        }
        var buf = raw.previousBuffer();
        if (((Kind)buf[0]).normalize() == (rune)'0') {
            var (fv, err) = strconv.ParseFloat((@string)buf, bits);
            if (err != nil) {
                err = new numErrorжerror(Ꮡ(new numError(accessor: "Float", value: t.String(), err: errors.Unwrap(err))));
            }
            return (fv, err);
        }
    } else if (t.num != 0) {
        switch (t.str[0]) {
        case (byte)'F': {
            return ((float64)math.Float32frombits((uint32)t.num), default);
        }
        case (byte)'f': {
            var f64 = (float64)math.Float64frombits((uint64)t.num);
            if (bits == 32 && !math.IsInf(f64, 0) && math.IsInf((float64)(float32)f64, 0)) {
                return (f64, new numErrorжerror(Ꮡ(new numError(accessor: "Float", value: t.String(), err: strconv.ErrRange))));
            }
            return (f64, default);
        }
        case (byte)'i': {
            return ((float64)(int64)t.num, default);
        }
        case (byte)'u': {
            return ((float64)(uint64)t.num, default);
        }}

    }

    if (t.Kind() == (rune)'"') {
        var exprᴛ1 = t.String();
        switch (ᐧ) {
        case {} when exprᴛ1 == "NaN"u8: {
            return (math.NaN(), default);
        }
        case {} when exprᴛ1 == "Infinity"u8: {
            return (math.Inf(+1), default);
        }
        case {} when exprᴛ1 == "-Infinity"u8: {
            return (math.Inf(-1), default);
        }}

    }

    throw panic("invalid JSON token kind: " + t.Kind().String());
}

public static (int64, error) Int(this Token t) {
    var raw = t.raw; if (raw != nil) {
        if ((uint64)raw.previousOffsetStart() != t.num) {
            throw panic(invalidTokenPanic);
        }
        var buf = raw.previousBuffer();
        if (len(buf) > 0 && buf[0] == '-') {
            var (abs, ok) = jsonwire.ParseUint(buf[(int)(len("-"))..]);
            switch (ᐧ) {
            case {} when abs > (uint64)(-minInt64): {
                return (minInt64, new numErrorжerror(Ꮡ(new numError(accessor: "Int", value: t.String(), err: strconv.ErrRange))));
            }
            case {} when ok: {
                return (-1 * (int64)abs, default);
            }}

        } else {
            var (abs, ok) = jsonwire.ParseUint(buf);
            switch (ᐧ) {
            case {} when abs > (uint64)(+maxInt64): {
                return (maxInt64, new numErrorжerror(Ꮡ(new numError(accessor: "Int", value: t.String(), err: strconv.ErrRange))));
            }
            case {} when ok: {
                return (+1 * (int64)abs, default);
            }}

        }
        if (((Kind)buf[0]).normalize() == (rune)'0') {
            var (f64, _) = strconv.ParseFloat((@string)buf, 64);
            return (f64toi64(f64), new numErrorжerror(Ꮡ(new numError(accessor: "Int", value: t.String(), err: strconv.ErrSyntax))));
        }
    } else if (t.num != 0) {
        switch (t.str[0]) {
        case (byte)'i': {
            return ((int64)t.num, default);
        }
        case (byte)'u': {
            if (t.num > maxInt64) {
                return (maxInt64, new numErrorжerror(Ꮡ(new numError(accessor: "Int", value: t.String(), err: strconv.ErrRange))));
            }
            return ((int64)t.num, default);
        }
        case (byte)'f' or (byte)'F': {
            var f64 = (float64)math.Float64frombits((uint64)t.num);
            if (t.str[0] == 'F') {
                f64 = (float64)math.Float32frombits((uint32)t.num);
            }
            var i64 = f64toi64(f64);
            switch (ᐧ) {
            case {} when (math.IsNaN(f64)) || (math.Trunc(f64) != f64): {
                return (i64, new numErrorжerror(Ꮡ(new numError(accessor: "Int", value: t.String(), err: strconv.ErrSyntax))));
            }
            case {} when (i64 == minInt64 && f64 < minInt64) || (i64 == maxInt64 && f64 > maxInt64): {
                return (i64, new numErrorжerror(Ꮡ(new numError(accessor: "Int", value: t.String(), err: strconv.ErrRange))));
            }
            default: {
                return (i64, default);
            }}

            break;
        }}

    }

    throw panic("invalid JSON token kind: " + t.Kind().String());
}

} partial class jsontext_package {

internal static int64 f64toi64(float64 f64) {
    switch (ᐧ) {
    case {} when math.IsNaN(f64): {
        return 0;
    }
    case {} when f64 >= /* maxInt64 + 1 */ 9.223372036854776e+18D: {
        return maxInt64;
    }
    case {} when f64 < minInt64: {
        return minInt64;
    }
    default: {
        return (int64)f64;
    }}

}

} partial class jsontextᴛToken {

public static (uint64, error) Uint(this Token t) {
    var raw = t.raw; if (raw != nil) {
        if ((uint64)raw.previousOffsetStart() != t.num) {
            throw panic(invalidTokenPanic);
        }
        var buf = raw.previousBuffer();
        var (abs, ok) = jsonwire.ParseUint(buf);
        switch (ᐧ) {
        case {} when ok: {
            return (abs, default);
        }
        case {} when abs == maxUint64: {
            return (maxUint64, new numErrorжerror(Ꮡ(new numError(accessor: "Uint", value: t.String(), err: strconv.ErrRange))));
        }}

        if (((Kind)buf[0]).normalize() == (rune)'0') {
            var (f64, _) = strconv.ParseFloat((@string)buf, 64);
            return (f64tou64(f64), new numErrorжerror(Ꮡ(new numError(accessor: "Uint", value: t.String(), err: strconv.ErrSyntax))));
        }
    } else if (t.num != 0) {
        switch (t.str[0]) {
        case (byte)'u': {
            return (t.num, default);
        }
        case (byte)'i': {
            if ((int64)t.num < minUint64) {
                return (minUint64, new numErrorжerror(Ꮡ(new numError(accessor: "Uint", value: t.String(), err: strconv.ErrSyntax))));
            }
            return ((uint64)(int64)t.num, default);
        }
        case (byte)'f' or (byte)'F': {
            var f64 = (float64)math.Float64frombits((uint64)t.num);
            if (t.str[0] == 'F') {
                f64 = (float64)math.Float32frombits((uint32)t.num);
            }
            var u64 = f64tou64(f64);
            switch (ᐧ) {
            case {} when (math.IsNaN(f64)) || (math.Trunc(f64) != f64) || (math.Signbit(f64)): {
                return (u64, new numErrorжerror(Ꮡ(new numError(accessor: "Uint", value: t.String(), err: strconv.ErrSyntax))));
            }
            case {} when (u64 == minUint64 && f64 < minUint64) || (u64 == maxUint64 && f64 > maxUint64): {
                return (u64, new numErrorжerror(Ꮡ(new numError(accessor: "Uint", value: t.String(), err: strconv.ErrRange))));
            }
            default: {
                return (u64, default);
            }}

            break;
        }}

    }

    throw panic("invalid JSON token kind: " + t.Kind().String());
}

} partial class jsontext_package {

internal static uint64 f64tou64(float64 f64) {
    switch (ᐧ) {
    case {} when math.IsNaN(f64): {
        return 0;
    }
    case {} when f64 >= /* maxUint64 + 1 */ 1.8446744073709552e+19D: {
        return maxUint64;
    }
    case {} when f64 < minUint64: {
        return minUint64;
    }
    default: {
        return (uint64)f64;
    }}

}

} partial class jsontextᴛToken {

public static Kind Kind(this Token t) {
    switch (ᐧ) {
    case {} when t.raw != nil: {
        var raw = t.raw;
        if ((uint64)raw.previousOffsetStart() != t.num) {
            throw panic(invalidTokenPanic);
        }
        return ((Kind)t.raw.buf[raw.prevStart]).normalize();
    }
    case {} when t.num is not 0: {
        return (rune)'0';
    }
    case {} when len(t.str) is not 0: {
        return (rune)'"';
    }
    default: {
        return invalidKind;
    }}

}

} partial class jsontext_package {

partial struct Kind /*num:byte*/;

public static Kind KindInvalid => 0;
public static Kind KindNull => /* 'n' */ 110;
public static Kind KindFalse => /* 'f' */ 102;
public static Kind KindTrue => /* 't' */ 116;
public static Kind KindString => /* '"' */ 34;
public static Kind KindNumber => /* '0' */ 48;
public static Kind KindBeginObject => /* '{' */ 123;
public static Kind KindEndObject => /* '}' */ 125;
public static Kind KindBeginArray => /* '[' */ 91;
public static Kind KindEndArray => /* ']' */ 93;

internal static Kind invalidKind => 0;

} partial class jsontextᴛKind {

public static @string String(this Kind k) {
    switch (k) {
    case var ᴛ when ᴛ == (Kind)(0): {
        return "invalid";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'n'): {
        return "null";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'f'): {
        return "false";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'t'): {
        return "true";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'"'): {
        return "string";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'0'): {
        return "number";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'{'): {
        return "{";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'}'): {
        return "}";
    }
    case var ᴛ when ᴛ == (Kind)((rune)'['): {
        return "[";
    }
    case var ᴛ when ᴛ == (Kind)((rune)']'): {
        return "]";
    }
    default: {
        return "<invalid jsontext.Kind: "u8 + jsonwire.QuoteRune([(byte)k]) + ">"u8;
    }}

}

} partial class jsontext_package {

[GoLazy] partial class lazyᴛnormKind { internal static array<Kind> normKind = new array<Kind>(256){
    ['n'] = (rune)'n',
    ['f'] = (rune)'f',
    ['t'] = (rune)'t',
    ['"'] = (rune)'"',
    ['{'] = (rune)'{',
    ['}'] = (rune)'}',
    ['['] = (rune)'[',
    [']'] = (rune)']',
    ['-'] = (rune)'0',
    ['0'] = (rune)'0',
    ['1'] = (rune)'0',
    ['2'] = (rune)'0',
    ['3'] = (rune)'0',
    ['4'] = (rune)'0',
    ['5'] = (rune)'0',
    ['6'] = (rune)'0',
    ['7'] = (rune)'0',
    ['8'] = (rune)'0',
    ['9'] = (rune)'0'
}; }

} partial class jsontextᴛKind {

internal static Kind normalize(this Kind k) {
    return normKind[k];
}

} // end jsontextᴛKind
