// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

global using Marshalers = go.github.com.go_json_experiment.json_package.typedArshalers<go.github.com.go_json_experiment.json.jsontext_package.Encoder>;
global using Unmarshalers = go.github.com.go_json_experiment.json_package.typedArshalers<go.github.com.go_json_experiment.json.jsontext_package.Decoder>;
global using typedMarshalers = go.github.com.go_json_experiment.json_package.typedArshalers<go.github.com.go_json_experiment.json.jsontext_package.Encoder>;
global using typedUnmarshalers = go.github.com.go_json_experiment.json_package.typedArshalers<go.github.com.go_json_experiment.json.jsontext_package.Decoder>;
global using typedMarshaler = go.github.com.go_json_experiment.json_package.typedArshaler<go.github.com.go_json_experiment.json.jsontext_package.Encoder>;
global using typedUnmarshaler = go.github.com.go_json_experiment.json_package.typedArshaler<go.github.com.go_json_experiment.json.jsontext_package.Decoder>;

namespace go.github.com.go_json_experiment;

using errors = errors_package;
using fmt = fmt_package;
using io = io_package;
using reflect = reflect_package;
using sync = sync_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using ꓸꓸꓸжMarshalers = Span<ж<Marshalers>>;
using ꓸꓸꓸжUnmarshalers = Span<ж<Unmarshalers>>;
using static json_package;

partial class json_package {

internal static error errUnsupportedMutation = errors.New("unsupported calls must not read or write any tokens");
internal static error errNonSingularValue = errors.New("must read or write exactly one value");

public static ж<Marshalers> JoinMarshalers(params ꓸꓸꓸжMarshalers msʗp) {
    var ms = msʗp.sslice();

    return newMarshalers(ms.ꓸꓸꓸ);
}

public static ж<Unmarshalers> JoinUnmarshalers(params ꓸꓸꓸжUnmarshalers usʗp) {
    var us = usʗp.sslice();

    return newUnmarshalers(us.ꓸꓸꓸ);
}

public partial struct typedArshalers<Coder> {
    /*embed*/ internal nonComparable nonComparable;
    internal slice<typedArshaler<Coder>> fncVals;
    internal sync.Map fncCache;
    internal bool fromAny;
}

partial struct typedArshaler<Coder> {
    internal reflect.Type typ;
    internal Func<ж<Coder>, addressableValue, ж<jsonopts.Struct>, error> fnc;
    internal bool maySkip;
}

internal static ж<Marshalers> newMarshalers(params ꓸꓸꓸжMarshalers msʗp) {
    var ms = msʗp.sslice();

    return newTypedArshalers(ms.ꓸꓸꓸ);
}

internal static ж<Unmarshalers> newUnmarshalers(params ꓸꓸꓸжUnmarshalers usʗp) {
    var us = usʗp.sslice();

    return newTypedArshalers(us.ꓸꓸꓸ);
}

internal static ж<typedArshalers<Coder>> newTypedArshalers<Coder>(params Span<ж<typedArshalers<Coder>>> @asʗp) {
    var @as = @asʗp.sslice();

    ref var a = ref heap(new typedArshalers<Coder>(), out var Ꮡa);
    foreach (var (_, a2) in range(@as)) {
        if (a2 != nil) {
            a.fncVals = appendꓸꓸꓸ(a.fncVals, a2.fncVals);
            a.fromAny = a.fromAny || a2.fromAny;
        }
    }
    if (len(a.fncVals) == 0) {
        return default;
    }
    return Ꮡa;
}

} partial class jsonᴛtypedArshalers {

internal static (Func<ж<Coder>, addressableValue, ж<jsonopts.Struct>, error>, bool) lookup<Coder>(this ж<typedArshalers<Coder>> Ꮡa, Func<ж<Coder>, addressableValue, ж<jsonopts.Struct>, error> fnc, reflect.Type t) {
    ref var a = ref Ꮡa.DerefOrNull();

    if (Ꮡa == nil) {
        return (fnc, false);
    }
    var (vΔ1, ok) = Ꮡa.ᏑfncCache.Load(t); if (ok) {
        if (vΔ1 == nil) {
            return (fnc, false);
        }
        return (vΔ1._<Func<ж<Coder>, addressableValue, ж<jsonopts.Struct>, error>>(), true);
    }

    slice<Func<ж<Coder>, addressableValue, ж<jsonopts.Struct>, error>> fncs = default;
    foreach (var (_, fncVal) in range(a.fncVals)) {
        if (!castableTo(t, fncVal.typ)) {
            continue;
        }
        fncs = append(fncs, fncVal.fnc);
        if (!fncVal.maySkip) {
            break;
        }
    }

    if (len(fncs) == 0) {
        Ꮡa.ᏑfncCache.Store(t, default);
        return (fnc, false);
    }

    var fncDefault = fnc;
    var fncDefaultʗ1 = fncDefault;
    var fncsʗ1 = fncs;
    fnc = (ж<Coder> c, addressableValue vΔ2, ж<jsonopts.Struct> o) => {
        foreach (var (_, fncΔ1) in range(fncsʗ1)) {
            var err = fncΔ1(c, vΔ2, o); if (!errors.Is(err, errors.ErrUnsupported)) {
                return err;
            }
        }
        return fncDefaultʗ1(c, vΔ2, o);
    };

    var (v, _) = Ꮡa.ᏑfncCache.LoadOrStore(t, (fnc).OrTypedNilFunc());
    return (v._<Func<ж<Coder>, addressableValue, ж<jsonopts.Struct>, error>>(), true);
}

} partial class json_package {

public static ж<Marshalers> MarshalFunc<T, Tᴺ>(Func<T, (slice<byte>, error)> fn) {
    var t = reflect.TypeFor<Tᴺ>();
    assertCastableTo(t, true);
        var tʗ1 = t;
    var typFnc = new typedMarshaler(
        typ: t,
        fnc: error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
            var (v, _) = reflect.TypeAssert<T>(va.castTo(tʗ1));
            var (val, err) = fn(v);
            if (err != nil) {
                err = wrapErrUnsupported(err, "marshal function of type func(T) ([]byte, error)");
                if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return @internal.NewMarshalerError(va.Value.Addr().Interface(), err, "MarshalFunc");
                }
                err = newMarshalErrorBefore(enc, tʗ1, err);
                return collapseSemanticErrors(err);
            }
            var errΔ1 = enc.WriteValue(val); if (errΔ1 != nil) {
                if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return @internal.NewMarshalerError(va.Value.Addr().Interface(), errΔ1, "MarshalFunc");
                }
                if (isSyntacticError(errΔ1)) {
                    errΔ1 = newMarshalErrorBefore(enc, tʗ1, errΔ1);
                }
                return errΔ1;
            }
            return default;
        }
    );
    return Ꮡ(new Marshalers(fncVals: [typFnc], fromAny: castableToFromAny(t)));
}

public static ж<Marshalers> MarshalToFunc<T, Tᴺ>(Func<ж<jsontext.Encoder>, T, error> fn) {
    var t = reflect.TypeFor<Tᴺ>();
    assertCastableTo(t, true);
        var tʗ1 = t;
    var typFnc = new typedMarshaler(
        typ: t,
        fnc: error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
            var xe = export.Encoder(enc);
            var (prevDepth, prevLength) = xe.Tokens.DepthLength();
            xe.of(jsontext.encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 1));
            var (v, _) = reflect.TypeAssert<T>(va.castTo(tʗ1));
            var err = fn(enc, v);
            xe.of(jsontext.encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 0));
            var (currDepth, currLength) = xe.Tokens.DepthLength();
            if (err == nil && (prevDepth != currDepth || prevLength + 1 != currLength)) {
                err = errNonSingularValue;
            }
            if (err != nil) {
                if (errors.Is(err, errors.ErrUnsupported)) {
                    if (prevDepth == currDepth && prevLength == currLength) {
                        return err;
                    }
                    err = errUnsupportedMutation;
                }
                if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return @internal.NewMarshalerError(va.Value.Addr().Interface(), err, "MarshalToFunc");
                }
                if (!export.IsIOError(err)) {
                    err = newSemanticErrorWithPosition(new jsontext_Encoderжcoder(enc), tʗ1, prevDepth, prevLength, err);
                }
                return err;
            }
            return default;
        },
        maySkip: true
    );
    return Ꮡ(new Marshalers(fncVals: [typFnc], fromAny: castableToFromAny(t)));
}

public static ж<Unmarshalers> UnmarshalFunc<T, Tᴺ>(Func<slice<byte>, T, error> fn) {
    var t = reflect.TypeFor<Tᴺ>();
    assertCastableTo(t, false);
        var tʗ1 = t;
    var typFnc = new typedUnmarshaler(
        typ: t,
        fnc: error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
            var (val, err) = dec.ReadValue();
            if (err != nil) {
                return err;
            }
            var (v, _) = reflect.TypeAssert<T>(va.castTo(tʗ1));
            err = fn(val, v);
            if (err != nil) {
                err = wrapErrUnsupported(err, "unmarshal function of type func([]byte, T) error");
                if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return err;
                }
                err = newUnmarshalErrorAfter(dec, tʗ1, err);
                return collapseSemanticErrors(err);
            }
            return default;
        }
    );
    return Ꮡ(new Unmarshalers(fncVals: [typFnc], fromAny: castableToFromAny(t)));
}

public static ж<Unmarshalers> UnmarshalFromFunc<T, Tᴺ>(Func<ж<jsontext.Decoder>, T, error> fn) {
    var t = reflect.TypeFor<Tᴺ>();
    assertCastableTo(t, false);
        var tʗ1 = t;
    var typFnc = new typedUnmarshaler(
        typ: t,
        fnc: error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
            var xd = export.Decoder(dec);
            var (prevDepth, prevLength) = xd.Tokens.DepthLength();
            if (prevDepth == 1 && xd.AtEOF()) {
                return io.EOF;
            }
            xd.of(jsontext.decoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 1));
            var (v, _) = reflect.TypeAssert<T>(va.castTo(tʗ1));
            var err = fn(dec, v);
            xd.of(jsontext.decoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 0));
            var (currDepth, currLength) = xd.Tokens.DepthLength();
            if (err == nil && (prevDepth != currDepth || prevLength + 1 != currLength)) {
                err = errNonSingularValue;
            }
            if (err != nil) {
                if (errors.Is(err, errors.ErrUnsupported)) {
                    if (prevDepth == currDepth && prevLength == currLength) {
                        return err;
                    }
                    err = errUnsupportedMutation;
                }
                if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    var err2 = xd.SkipUntil(prevDepth, prevLength + 1); if (err2 != nil) {
                        return err2;
                    }
                    return err;
                }
                if (!isSyntacticError(err) && !export.IsIOError(err)) {
                    err = newSemanticErrorWithPosition(new jsontext_Decoderжcoder(dec), tʗ1, prevDepth, prevLength, err);
                }
                return err;
            }
            return default;
        },
        maySkip: true
    );
    return Ꮡ(new Unmarshalers(fncVals: [typFnc], fromAny: castableToFromAny(t)));
}

internal static void assertCastableTo(reflect.Type to, bool marshal) {
    switch (to.Kind()) {
    case var ᴛ when ᴛ == reflect.Interface: {
        return;
    }
    case var ᴛ when ᴛ == reflect.Pointer: {
        if (to.Name() == ""u8) {
            return;
        }
        break;
    }
    default: {
        if (marshal) {
            return;
        }
        break;
    }}

    if (marshal) {
        throw panic(fmt.Sprintf("input type %v must be an interface type, an unnamed pointer type, or a non-pointer type"u8, to));
    } else {
        throw panic(fmt.Sprintf("input type %v must be an interface type or an unnamed pointer type"u8, to));
    }
}

internal static bool castableTo(reflect.Type from, reflect.Type to) {
    switch (to.Kind()) {
    case var ᴛ when ᴛ == reflect.Interface: {
        return reflect.PointerTo(from).Implements(to);
    }
    case var ᴛ when ᴛ == reflect.Pointer: {
        return AreEqual(reflect.PointerTo(from), to);
    }
    default: {
        return AreEqual(from, to);
    }}

}

} partial class jsonᴛaddressableValue {

internal static reflect.Value castTo(this addressableValue va, reflect.Type to) {
    switch (to.Kind()) {
    case var ᴛ when ᴛ == reflect.Interface: {
        return va.Value.Addr().Convert(to);
    }
    case var ᴛ when ᴛ == reflect.Pointer: {
        return va.Value.Addr();
    }
    default: {
        return va.Value;
    }}

}

} partial class json_package {

internal static bool castableToFromAny(reflect.Type to) {
    foreach (var (_, from) in range(slice<reflect.Type>([anyType, boolType, stringType, float64Type, mapStringAnyType, sliceAnyType]))) {
        if (castableTo(from, to)) {
            return true;
        }
    }
    return false;
}

} // end json_package
