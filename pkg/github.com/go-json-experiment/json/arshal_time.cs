// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using bytes = bytes_package;
using cmp = cmp_package;
using errors = errors_package;
using fmt = fmt_package;
using math = math_package;
using bits = go.math.bits_package;
using reflect = reflect_package;
using strconv = strconv_package;
using strings = strings_package;
using time = time_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using go.math;
using static json_package;

partial class json_package {

internal static reflect.Type timeDurationType = reflect.TypeFor<time.Duration>();
internal static reflect.Type timeTimeType = reflect.TypeFor<time.Time>();

internal static ж<arshaler> makeTimeArshaler(ж<arshaler> Ꮡfncs, reflect.Type t) {
    ref var fncs = ref Ꮡfncs.DerefOrNull();

    switch (t) {
    case var ᴛ when AreEqual(ᴛ, timeDurationType): {
        fncs.nonDefault = true;
        var marshalNano = fncs.marshal;
        var marshalNanoʗ1 = marshalNano;
        fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
            var xe = export.Encoder(enc);
            ref var m = ref heap(new durationArshaler(), out var Ꮡm);
            if (mo.Flags.Has(jsonflags.FormatTag)) {
                if (!m.initFormat(mo.Format)) {
                    return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
                }
            } else if (mo.Flags.Get(jsonflags.FormatDurationAsNano)) {
                return marshalNanoʗ1(enc, va, mo);
            } else {
                @string workaround = default;
                if (mo.Flags.Get(jsonflags.FormatTagSupported)) {
                    workaround = "; specify an explicit format";
                }
                return newMarshalErrorBefore(enc, t, errors.New("no default representation"u8 + workaround));
            }
            if (mo.Flags.Get(jsonflags.StringTag) && !m.isNumeric() && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newMarshalErrorBefore(enc, t, errInvalidStringTag);
            }

            (m.td, _) = reflect.TypeAssert<time.Duration>(va.Value);
            var k = stringOrNumberKind(!m.isNumeric() || xe.Tokens.Last.NeedObjectName() || mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag)));
            {
                var err = xe.AppendRaw(k, true, Ꮡm.appendMarshal); if (err != nil) {
                    if (!isSyntacticError(err) && !export.IsIOError(err)) {
                        err = newMarshalErrorBefore(enc, t, err);
                    }
                    return err;
                }
            }
            return default;
        };
        var unmarshalNano = fncs.unmarshal;
        var unmarshalNanoʗ1 = unmarshalNano;
        fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
            var xd = export.Decoder(dec);
            durationArshaler u = default;
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                if (!u.initFormat(uo.Format)) {
                    return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
                }
            } else if (uo.Flags.Get(jsonflags.FormatDurationAsNano)) {
                return unmarshalNanoʗ1(dec, va, uo);
            } else {
                @string workaround = default;
                if (uo.Flags.Get(jsonflags.FormatTagSupported)) {
                    workaround = "; specify an explicit format";
                }
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errors.New("no default representation"u8 + workaround));
            }
            if (uo.Flags.Get(jsonflags.StringTag) && !u.isNumeric() && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }

            var stringify = !u.isNumeric() || xd.Tokens.Last.NeedObjectName() || uo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
            ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
            var (td, _) = reflect.TypeAssert<ж<time.Duration>>(va.Value.Addr());
            var (val, err) = xd.ReadValue(Ꮡflags);
            if (err != nil) {
                return err;
            }
            var k = val.Kind();
            switch (k) {
            case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'n'): {
                if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                    td.Value = (time.Duration)0;
                }
                return default;
            }
            case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'"'): {
                if (!stringify) {
                    break;
                }
                val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                var errΔ3 = u.unmarshal(val); if (errΔ3 != nil) {
                    return newUnmarshalErrorAfter(dec, t, errΔ3);
                }
                td.Value = u.td;
                return default;
            }
            case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'0'): {
                if (stringify) {
                    break;
                }
                var errΔ4 = u.unmarshal(val); if (errΔ4 != nil) {
                    return newUnmarshalErrorAfter(dec, t, errΔ4);
                }
                td.Value = u.td;
                return default;
            }}

            return newUnmarshalErrorAfter(dec, t, default);
        };
        break;
    }
    case var ᴛ when AreEqual(ᴛ, timeTimeType): {
        fncs.nonDefault = true;
        fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
            var xe = export.Encoder(enc);
            ref var m = ref heap(new timeArshaler(), out var Ꮡm);
            if (mo.Flags.Has(jsonflags.FormatTag)) {
                if (!m.initFormat(mo.Format)) {
                    return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
                }
            }
            if (mo.Flags.Get(jsonflags.StringTag) && !m.isNumeric() && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newMarshalErrorBefore(enc, t, errInvalidStringTag);
            }

            (m.tt, _) = reflect.TypeAssert<time.Time>(va.Value);
            var k = stringOrNumberKind(!m.isNumeric() || xe.Tokens.Last.NeedObjectName() || mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag)));
            {
                var errΔ1 = xe.AppendRaw(k, !m.hasCustomFormat(), Ꮡm.appendMarshal); if (errΔ1 != nil) {
                    if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return @internal.NewMarshalerError(va.Value.Addr().Interface(), errΔ1, "MarshalJSON");
                    }
                    if (!isSyntacticError(errΔ1) && !export.IsIOError(errΔ1)) {
                        errΔ1 = newMarshalErrorBefore(enc, t, errΔ1);
                    }
                    return errΔ1;
                }
            }
            return default;
        };
        fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
            error err = default;
            var xd = export.Decoder(dec);
            timeArshaler u = default;
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                if (!u.initFormat(uo.Format)) {
                    return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
                }
            } else if (uo.Flags.Get(jsonflags.ParseTimeWithLooseRFC3339)) {
                u.looseRFC3339 = true;
            }
            if (uo.Flags.Get(jsonflags.StringTag) && !u.isNumeric() && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }

            var stringify = !u.isNumeric() || xd.Tokens.Last.NeedObjectName() || uo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
            ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
            var (tt, _) = reflect.TypeAssert<ж<time.Time>>(va.Value.Addr());
            (var val, err) = xd.ReadValue(Ꮡflags);
            if (err != nil) {
                return err;
            }
            var k = val.Kind();
            switch (k) {
            case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'n'): {
                if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                    tt.Value = new time.Time();
                }
                return default;
            }
            case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'"'): {
                if (!stringify) {
                    break;
                }
                val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                var errΔ3 = u.unmarshal(val); if (errΔ3 != nil) {
                    if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return errΔ3;
                    }
                    return newUnmarshalErrorAfter(dec, t, errΔ3);
                }
                tt.Value = u.tt;
                return default;
            }
            case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'0'): {
                if (stringify) {
                    break;
                }
                var errΔ4 = u.unmarshal(val); if (errΔ4 != nil) {
                    if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return errΔ4;
                    }
                    return newUnmarshalErrorAfter(dec, t, errΔ4);
                }
                tt.Value = u.tt;
                return default;
            }}

            return newUnmarshalErrorAfter(dec, t, default);
        };
        break;
    }}

    return Ꮡfncs;
}

partial struct durationArshaler {
    internal time.Duration td;
    internal uint64 @base;
}

} partial class jsonᴛdurationArshaler {

internal static bool /*ok*/ initFormat(this ref durationArshaler a, @string format) {
    switch (ᐧ) {
    case {} when format == "units"u8: {
        a.@base = 0;
        break;
    }
    case {} when format == "sec"u8: {
        a.@base = 1000000000;
        break;
    }
    case {} when format == "milli"u8: {
        a.@base = 1000000;
        break;
    }
    case {} when format == "micro"u8: {
        a.@base = 1000;
        break;
    }
    case {} when format == "nano"u8: {
        a.@base = 1;
        break;
    }
    case {} when format == "iso8601"u8: {
        a.@base = 8601;
        break;
    }
    default: {
        return false;
    }}

    return true;
}

internal static bool isNumeric(this ref durationArshaler a) {
    return a.@base != 0 && a.@base != 8601;
}

internal static (slice<byte>, error) appendMarshal(this ref durationArshaler a, slice<byte> b) {
    switch (a.@base) {
    case 0: {
        return (append(b, a.td.String().ꓸꓸꓸ), default);
    }
    case 8601: {
        return (appendDurationISO8601(b, a.td), default);
    }
    default: {
        return (appendDurationBase10(b, a.td, a.@base), default);
    }}

}

internal static error /*err*/ unmarshal(this ref durationArshaler a, slice<byte> b) {
    error err = default;

    switch (a.@base) {
    case 0: {
        (a.td, err) = time.ParseDuration((@string)b);
        break;
    }
    case 8601: {
        (a.td, err) = parseDurationISO8601(b);
        break;
    }
    default: {
        (a.td, err) = parseDurationBase10(b, a.@base);
        break;
    }}

    return err;
}

} partial class json_package {

partial struct timeArshaler {
    internal time.Time tt;
    internal uint64 @base;
    internal @string format;
    internal bool looseRFC3339;
}

} partial class jsonᴛtimeArshaler {

internal static bool initFormat(this ref timeArshaler a, @string format) {
    if (len(format) == 0) {
        return false;
    }
    a.@base = math.MaxUint;
    var c = format[0]; if (!('a' <= c && c <= 'z') && !('A' <= c && c <= 'Z')) {
        a.format = format;
        return true;
    }
    switch (ᐧ) {
    case {} when format == "ANSIC"u8: {
        a.format = time.ANSIC;
        break;
    }
    case {} when format == "UnixDate"u8: {
        a.format = time.UnixDate;
        break;
    }
    case {} when format == "RubyDate"u8: {
        a.format = time.RubyDate;
        break;
    }
    case {} when format == "RFC822"u8: {
        a.format = time.RFC822;
        break;
    }
    case {} when format == "RFC822Z"u8: {
        a.format = time.RFC822Z;
        break;
    }
    case {} when format == "RFC850"u8: {
        a.format = time.RFC850;
        break;
    }
    case {} when format == "RFC1123"u8: {
        a.format = time.RFC1123;
        break;
    }
    case {} when format == "RFC1123Z"u8: {
        a.format = time.RFC1123Z;
        break;
    }
    case {} when format == "RFC3339"u8: {
        a.@base = 0;
        a.format = time.RFC3339;
        break;
    }
    case {} when format == "RFC3339Nano"u8: {
        a.@base = 0;
        a.format = time.RFC3339Nano;
        break;
    }
    case {} when format == "Kitchen"u8: {
        a.format = time.Kitchen;
        break;
    }
    case {} when format == "Stamp"u8: {
        a.format = time.Stamp;
        break;
    }
    case {} when format == "StampMilli"u8: {
        a.format = time.StampMilli;
        break;
    }
    case {} when format == "StampMicro"u8: {
        a.format = time.StampMicro;
        break;
    }
    case {} when format == "StampNano"u8: {
        a.format = time.StampNano;
        break;
    }
    case {} when format == "DateTime"u8: {
        a.format = time.DateTime;
        break;
    }
    case {} when format == "DateOnly"u8: {
        a.format = time.DateOnly;
        break;
    }
    case {} when format == "TimeOnly"u8: {
        a.format = time.TimeOnly;
        break;
    }
    case {} when format == "unix"u8: {
        a.@base = 1;
        break;
    }
    case {} when format == "unixmilli"u8: {
        a.@base = 1000;
        break;
    }
    case {} when format == "unixmicro"u8: {
        a.@base = 1000000;
        break;
    }
    case {} when format == "unixnano"u8: {
        a.@base = 1000000000;
        break;
    }
    default: {
        if (strings.TrimFunc(format, isLetterOrDigit) == ""u8) {
            return false;
        }
        a.format = format;
        break;
    }}

    return true;
}

internal static bool isNumeric(this ref timeArshaler a) {
    return (nint)a.@base > 0;
}

internal static bool hasCustomFormat(this ref timeArshaler a) {
    return a.@base == math.MaxUint;
}

internal static (slice<byte>, error) appendMarshal(this ref timeArshaler a, slice<byte> b) {
    switch (a.@base) {
    case 0: {
        @string format = cmp.Or(a.format, time.RFC3339Nano);
        nint n0 = len(b);
        b = a.tt.AppendFormat(b, format);
        var bΔ2 = b.slice(n0);
        switch (ᐧ) {
        case {} when bΔ2[len("9999")] != (byte)'-': {
            return (bΔ2, errors.New("year outside of range [0,9999]"));
        }
        case {} when bΔ2[len(bΔ2) - 1] is not (byte)'Z': {
            var c = bΔ2[len(bΔ2) - len("Z07:00")];
            if (('0' <= c && c <= '9') || parseDec2(bΔ2.slice(len(bΔ2) - len("07:00"))) >= 24) {
                return (bΔ2, errors.New("timezone hour outside of range [0,23]"));
            }
            break;
        }}

        return (b, default);
    }
    case var ᴛ when ᴛ == math.MaxUint: {
        return (a.tt.AppendFormat(b, a.format), default);
    }
    default: {
        return (appendTimeUnix(b, a.tt, a.@base), default);
    }}

}

internal static error /*err*/ unmarshal(this ref timeArshaler a, slice<byte> b) {
    error err = default;

    switch (a.@base) {
    case 0: {
        var errΔ2 = a.tt.UnmarshalText(b); if (errΔ2 != nil) {
            return errΔ2;
        }
        error newParseError(@string layout, @string value, @string layoutElem, @string valueElem, @string message) => new time.ParseErrorжerror(Ꮡ(new time.ParseError(Layout: layout, Value: value, LayoutElem: layoutElem, ValueElem: valueElem, Message: message)));
        switch (ᐧ) {
        case {} when a.looseRFC3339: {
            return default;
        }
        case {} when b[len("2006-01-02T") + 1] == (byte)':': {
            return newParseError(time.RFC3339, (@string)b, "15", (@string)(b[(int)(len("2006-01-02T"))..][..1]), "");
        }
        case {} when b[len("2006-01-02T15:04:05")] == (byte)',': {
            return newParseError(time.RFC3339, (@string)b, ".", ",", "");
        }
        case {} when b[len(b) - 1] is not (byte)'Z': {
            switch (ᐧ) {
            case {} when parseDec2(b.slice(len(b) - len("07:00"))) >= 24: {
                return newParseError(time.RFC3339, (@string)b, "Z07:00", (@string)(b.slice(len(b) - len("Z07:00"))), ": timezone hour out of range");
            }
            case {} when parseDec2(b.slice(len(b) - len("00"))) >= 60: {
                return newParseError(time.RFC3339, (@string)b, "Z07:00", (@string)(b.slice(len(b) - len("Z07:00"))), ": timezone minute out of range");
            }}

            break;
        }}

        return default;
    }
    case var ᴛ when ᴛ == math.MaxUint: {
        (a.tt, err) = time.Parse(a.format, (@string)b);
        return err;
    }
    default: {
        (a.tt, err) = parseTimeUnix(b, a.@base);
        return err;
    }}

}

} partial class json_package {

internal static slice<byte> appendDurationBase10(slice<byte> b, time.Duration d, uint64 pow10) {
    (b, var n) = mayAppendDurationSign(b, d);
    var (whole, frac) = bits.Div64(0, n, (uint64)pow10);
    b = strconv.AppendUint(b, whole, 10);
    return appendFracBase10(b, frac, pow10);
}

internal static (time.Duration, error) parseDurationBase10(slice<byte> b, uint64 pow10) {
    var (suffix, neg) = consumeSign(b, false);
    var (wholeBytes, fracBytes) = bytesCutByte(suffix, (byte)'.', true);
    var (whole, okWhole) = jsonwire.ParseUint(wholeBytes);
    var (frac, okFrac) = parseFracBase10(fracBytes, pow10);
    var (hi, lo) = bits.Mul64(whole, (uint64)pow10);
    var (sum, co) = bits.Add64(lo, (uint64)frac, 0);
    var d = mayApplyDurationSign(sum, neg);
    switch (ᐧ) {
    case {} when (!okWhole && whole != math.MaxUint64) || !okFrac: {
        return (0, fmt.Errorf("invalid duration %q: %w"u8, b, strconv.ErrSyntax));
    }
    case {} when !okWhole || hi > 0 || co > 0 || neg != (d < 0): {
        return (0, fmt.Errorf("invalid duration %q: %w"u8, b, strconv.ErrRange));
    }
    default: {
        return (d, default);
    }}

}

internal static slice<byte> appendDurationISO8601(slice<byte> b, time.Duration d) {
    if (d == 0) {
        return append(b, ((@string)"PT0S").ꓸꓸꓸ);
    }
    (b, var n) = mayAppendDurationSign(b, d);
    b = append(b, ((@string)"PT").ꓸꓸꓸ);
    (n, var nsec) = bits.Div64(0, n, 1000000000);
    (n, var sec) = bits.Div64(0, n, 60);
    var (hour, min) = bits.Div64(0, n, 60);
    if (hour > 0) {
        b = append(strconv.AppendUint(b, hour, 10), (byte)'H');
    }
    if (min > 0) {
        b = append(strconv.AppendUint(b, min, 10), (byte)'M');
    }
    if (sec > 0 || nsec > 0) {
        b = append(appendFracBase10(strconv.AppendUint(b, sec, 10), nsec, 1000000000), (byte)'S');
    }
    return b;
}

internal static UntypedFloat daysPerYear => 365.2425;

internal static error errInaccurateDateUnits = errors.New("inaccurate year, month, week, or day units");

internal static (time.Duration, error) parseDurationISO8601(slice<byte> b) {
    bool invalid = default;
    bool overflow = default;
    bool inaccurate = default;
    bool sawFrac = default;
    uint64 sumNanos = default;
    uint64 n = default;
    uint64 co = default;

    (slice<byte> prefix, slice<byte> suffix, bool ok) cutBytes(slice<byte> bΔ1, byte c0, byte c1) {
        foreach (var (i, c) in range(bΔ1)) {
            if (c == c0 || c == c1) {
                return (bΔ1.slice(0, i), bΔ1.slice(i + 1), true);
            }
        }
        return (bΔ1, default, false);
    }

    var cutBytesʗ1 = cutBytes;
    slice<byte> mayParseUnit(slice<byte> bΔ2, byte desHi, byte desLo, time.Duration unit) {
        var (number, suffixΔ2, ok) = cutBytesʗ1(bΔ2, desHi, desLo);
        if (!ok || sawFrac) {
            return bΔ2;
        }

        (var whole, var frac, ok) = cutBytesʗ1(number, (byte)'.', (byte)',');
        if (ok) {
            sawFrac = true;
            invalid = invalid || len(frac) == len("") || unit > time.Hour;
            if (unit == time.Second) {
                (n, ok) = parsePaddedBase10(frac, (uint64)time.Second);
                invalid = invalid || !ok;
            } else {
                var (f, err) = strconv.ParseFloat("0."u8 + ((sstring)frac), 64);
                invalid = invalid || err != nil || len(bytes.Trim(frac[(int)(len("."))..], "0123456789")) > 0;
                n = (uint64)math.Round(f * (float64)unit);
            }
            (sumNanos, co) = bits.Add64(sumNanos, n, 0);
            overflow = overflow || co > 0;
        }
        while (len(whole) > 1 && whole[0] == '0') {
            whole = whole[(int)(len("0"))..];
        }
        (var nΔ1, ok) = jsonwire.ParseUint(whole);
        var (hi, lo) = bits.Mul64(nΔ1, (uint64)unit);
        (sumNanos, co) = bits.Add64(sumNanos, lo, 0);
        invalid = invalid || (!ok && nΔ1 != math.MaxUint64);
        overflow = overflow || (!ok && nΔ1 == math.MaxUint64) || hi > 0 || co > 0;
        inaccurate = inaccurate || unit > time.Hour;
        return suffixΔ2;
    }

    var (suffix, neg) = consumeSign(b, true);
    (var prefix, suffix, var okP) = cutBytes(suffix, (byte)'P', (byte)'p');
    var (durDate, durTime, okT) = cutBytes(suffix, (byte)'T', (byte)'t');
    invalid = invalid || len(prefix) > 0 || !okP || (okT && len(durTime) == 0) || len(durDate) + len(durTime) == 0;
    if (len(durDate) > 0) {
        durDate = mayParseUnit(durDate, (byte)'Y', (byte)'y', (time.Duration)(31556952000000000L));
        durDate = mayParseUnit(durDate, (byte)'M', (byte)'m', (time.Duration)(2629746000000000L));
        durDate = mayParseUnit(durDate, (byte)'W', (byte)'w', (time.Duration)(604800000000000L));
        durDate = mayParseUnit(durDate, (byte)'D', (byte)'d', (time.Duration)(86400000000000L));
        invalid = invalid || len(durDate) > 0;
    }
    if (len(durTime) > 0) {
        durTime = mayParseUnit(durTime, (byte)'H', (byte)'h', (time.Duration)(3600000000000L));
        durTime = mayParseUnit(durTime, (byte)'M', (byte)'m', (time.Duration)(60000000000L));
        durTime = mayParseUnit(durTime, (byte)'S', (byte)'s', (time.Duration)1000000000);
        invalid = invalid || len(durTime) > 0;
    }
    var d = mayApplyDurationSign(sumNanos, neg);
    overflow = overflow || (neg != (d < 0) && d != 0);

    switch (ᐧ) {
    case {} when invalid: {
        return (0, fmt.Errorf("invalid ISO 8601 duration %q: %w"u8, b, strconv.ErrSyntax));
    }
    case {} when overflow: {
        return (0, fmt.Errorf("invalid ISO 8601 duration %q: %w"u8, b, strconv.ErrRange));
    }
    case {} when inaccurate: {
        return (d, fmt.Errorf("invalid ISO 8601 duration %q: %w"u8, b, errInaccurateDateUnits));
    }
    default: {
        return (d, default);
    }}

}

internal static (slice<byte>, uint64) mayAppendDurationSign(slice<byte> b, time.Duration d) {
    if (d < 0) {
        b = append(b, (byte)'-');
        d *= -1;
    }
    return (b, (uint64)d);
}

internal static time.Duration mayApplyDurationSign(uint64 n, bool neg) {
    if (neg) {
        return -1 * ((time.Duration)(int64)n);
    } else {
        return +1 * ((time.Duration)(int64)n);
    }
}

internal static slice<byte> appendTimeUnix(slice<byte> b, time.Time t, uint64 pow10) {
    var (sec, nsec) = (t.Unix(), (int64)t.Nanosecond());
    if (sec < 0) {
        b = append(b, (byte)'-');
        (sec, nsec) = negateSecNano(sec, nsec);
    }
    switch (ᐧ) {
    case {} when pow10 is 1: {
        b = strconv.AppendUint(b, (uint64)sec, 10);
        return appendFracBase10(b, (uint64)nsec, 1000000000);
    }
    case {} when (uint64)sec is < 1000000000: {
        b = strconv.AppendUint(b, (uint64)sec * (uint64)pow10 + (uint64)((uint64)nsec / (1000000000 / pow10)), 10);
        return appendFracBase10(b, ((uint64)nsec * pow10) % 1000000000, 1000000000);
    }
    default: {
        b = strconv.AppendUint(b, (uint64)sec, 10);
        b = appendPaddedBase10(b, (uint64)nsec / (1000000000 / pow10), pow10);
        return appendFracBase10(b, ((uint64)nsec * pow10) % 1000000000, 1000000000);
    }}

}

internal static (time.Time, error) parseTimeUnix(slice<byte> b, uint64 pow10) {
    var (suffix, neg) = consumeSign(b, false);
    var (wholeBytes, fracBytes) = bytesCutByte(suffix, (byte)'.', true);
    var (whole, okWhole) = jsonwire.ParseUint(wholeBytes);
    var (frac, okFrac) = parseFracBase10(fracBytes, 1000000000 / pow10);
    int64 sec = default;
    int64 nsec = default;
    switch (ᐧ) {
    case {} when pow10 is 1: {
        sec = (int64)whole;
        nsec = (int64)frac;
        break;
    }
    case {} when okWhole: {
        sec = (int64)(whole / pow10);
        nsec = (int64)((whole % pow10) * (1000000000 / pow10) + frac);
        break;
    }
    case {} when !okWhole && whole == math.MaxUint64: {
        nint width = (nint)math.Log10((float64)pow10);
        (whole, okWhole) = jsonwire.ParseUint(wholeBytes.slice(0, len(wholeBytes) - width));
        var (mid, _) = parsePaddedBase10(wholeBytes.slice(len(wholeBytes) - width), pow10);
        sec = (int64)whole;
        nsec = (int64)(mid * (1000000000 / pow10) + frac);
        break;
    }}

    if (neg) {
        (sec, nsec) = negateSecNano(sec, nsec);
    }
    var t = time.Unix(sec, nsec).UTC();
    switch (ᐧ) {
    case {} when (!okWhole && whole != math.MaxUint64) || !okFrac: {
        return (new time.Time(), fmt.Errorf("invalid time %q: %w"u8, b, strconv.ErrSyntax));
    }
    case {} when !okWhole || neg != (t.Unix() < 0): {
        return (new time.Time(), fmt.Errorf("invalid time %q: %w"u8, b, strconv.ErrRange));
    }
    default: {
        return (t, default);
    }}

}

internal static (int64, int64) negateSecNano(int64 sec, int64 nsec) {
    sec = ~sec;
    nsec = -nsec + 1000000000;
    sec += (int64)(nsec / 1000000000);
    nsec %= 1000000000;
    return (sec, nsec);
}

internal static slice<byte> appendFracBase10(slice<byte> b, uint64 n, uint64 max10) {
    if (n == 0) {
        return b;
    }
    return bytes.TrimRight(appendPaddedBase10(append(b, (byte)'.'), n, max10), "0");
}

internal static (uint64 n, bool ok) parseFracBase10(slice<byte> b, uint64 max10) {
    switch (ᐧ) {
    case {} when len(b) is 0: {
        return (0, true);
    }
    case {} when len(b) < len(".0") || b[0] != (byte)'.': {
        return (0, false);
    }}

    return parsePaddedBase10(b[(int)(len("."))..], max10);
}

internal static slice<byte> appendPaddedBase10(slice<byte> b, uint64 n, uint64 max10) {
    if (n < max10 / 10) {
        nint i = len(b);
        b = strconv.AppendUint(b, n + max10 / 10, 10);
        b[i]--;
        return b;
    }
    return strconv.AppendUint(b, n, 10);
}

internal static (uint64 n, bool ok) parsePaddedBase10(slice<byte> b, uint64 max10) {
    uint64 n = default;

    var pow10 = (uint64)1;
    while (pow10 < max10) {
        n *= 10;
        if (len(b) > 0) {
            if (b[0] < '0' || '9' < b[0]) {
                return (n, false);
            }
            n += (uint64)((byte)(b[0] - '0'));
            b = b[1..];
        }
        pow10 *= 10;
    }
    if (len(b) > 0 && len(bytes.TrimRight(b, "0123456789")) > 0) {
        return (n, false);
    }
    return (n, true);
}

internal static (slice<byte>, bool) consumeSign(slice<byte> b, bool allowPlus) {
    if (len(b) > 0) {
        if (b[0] == '-') {
            return (b[(int)(len("-"))..], true);
        } else if (b[0] == '+' && allowPlus) {
            return (b[(int)(len("+"))..], false);
        }
    }
    return (b, false);
}

internal static (slice<byte>, slice<byte>) bytesCutByte(slice<byte> b, byte c, bool include) {
    nint i = bytes.IndexByte(b, c); if (i >= 0) {
        if (include) {
            return (b.slice(0, i), b.slice(i));
        }
        return (b.slice(0, i), b.slice(i + 1));
    }
    return (b, default);
}

internal static byte parseDec2(slice<byte> b) {
    if (len(b) < 2) {
        return 0;
    }
    return (byte)(10 * (b[0] - '0') + (b[1] - '0'));
}

} // end json_package
