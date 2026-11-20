// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using encoding = encoding_package;
using errors = errors_package;
using io = io_package;
using reflect = reflect_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using ꓸꓸꓸreflectꓸType = Span<reflect_package.Type>;

partial class json_package {

internal static error errNonStringValue = errors.New("JSON value must be string type");

internal static reflect.Type jsonMarshalerType = reflect.TypeFor<Marshaler>();
internal static reflect.Type jsonMarshalerToType = reflect.TypeFor<MarshalerTo>();
internal static reflect.Type jsonUnmarshalerType = reflect.TypeFor<Unmarshaler>();
internal static reflect.Type jsonUnmarshalerFromType = reflect.TypeFor<UnmarshalerFrom>();
internal static reflect.Type textAppenderType = reflect.TypeFor<encoding.TextAppender>();
internal static reflect.Type textMarshalerType = reflect.TypeFor<encoding.TextMarshaler>();
internal static reflect.Type textUnmarshalerType = reflect.TypeFor<encoding.TextUnmarshaler>();
internal static slice<reflect.Type> allMarshalerTypes = [jsonMarshalerToType, jsonMarshalerType, textAppenderType, textMarshalerType];
internal static slice<reflect.Type> allUnmarshalerTypes = [jsonUnmarshalerFromType, jsonUnmarshalerType, textUnmarshalerType];
internal static slice<reflect.Type> allMethodTypes = appendꓸꓸꓸ(allMarshalerTypes, allUnmarshalerTypes);

partial interface Marshaler {
    (slice<byte>, error) MarshalJSON();
}

partial interface MarshalerTo {
    error MarshalJSONTo(ж<jsontext.Encoder> _);
}

partial interface Unmarshaler {
    error UnmarshalJSON(slice<byte> _);
}

partial interface UnmarshalerFrom {
    error UnmarshalJSONFrom(ж<jsontext.Decoder> _);
}

internal static ж<arshaler> makeMethodArshaler(ж<arshaler> Ꮡfncs, reflect.Type t) {
    ref var fncs = ref Ꮡfncs.DerefOrNull();

    if (t.Kind() == reflect.Pointer || t.Kind() == reflect.Interface) {
        return Ꮡfncs;
    }

    {
        var (needAddr, ok) = implements(t, textMarshalerType); if (ok) {
            fncs.nonDefault = true;
            var prevMarshal = fncs.marshal;
            var prevMarshalʗ1 = prevMarshal;
            fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
                if (mo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && (needAddr && va.forcedAddr)) {
                    return prevMarshalʗ1(enc, va, mo);
                }
                var (marshaler, _) = reflect.TypeAssert<encoding.TextMarshaler>(va.Value.Addr());
                {
                    var marshalerʗ1 = marshaler;
                    var err = export.Encoder(enc).AppendRaw((rune)'"', false, (slice<byte> b) => {
                        var (b2, errΔ1) = marshalerʗ1.MarshalText();
                        return (appendꓸꓸꓸ(b, b2), errΔ1);
                    }); if (err != nil) {
                        err = wrapErrUnsupported(err, "MarshalText method");
                        if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                            return @internal.NewMarshalerError(va.Value.Addr().Interface(), err, "MarshalText");
                        }
                        if (!isSemanticError(err) && !export.IsIOError(err)) {
                            err = newMarshalErrorBefore(enc, t, err);
                        }
                        return err;
                    }
                }
                return default;
            };
        }
    }

    {
        var (needAddr, ok) = implements(t, textAppenderType); if (ok) {
            fncs.nonDefault = true;
            var prevMarshal = fncs.marshal;
            var prevMarshalʗ2 = prevMarshal;
            fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
                if (mo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && (needAddr && va.forcedAddr)) {
                    return prevMarshalʗ2(enc, va, mo);
                }
                var (appender, _) = reflect.TypeAssert<encoding.TextAppender>(va.Value.Addr());
                {
                    var errΔ1 = export.Encoder(enc).AppendRaw((rune)'"', false, appender.AppendText); if (errΔ1 != nil) {
                        errΔ1 = wrapErrUnsupported(errΔ1, "AppendText method");
                        if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                            return @internal.NewMarshalerError(va.Value.Addr().Interface(), errΔ1, "AppendText");
                        }
                        if (!isSemanticError(errΔ1) && !export.IsIOError(errΔ1)) {
                            errΔ1 = newMarshalErrorBefore(enc, t, errΔ1);
                        }
                        return errΔ1;
                    }
                }
                return default;
            };
        }
    }

    {
        var (needAddr, ok) = implements(t, jsonMarshalerType); if (ok) {
            fncs.nonDefault = true;
            var prevMarshal = fncs.marshal;
            var prevMarshalʗ3 = prevMarshal;
            fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
                if (mo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && ((needAddr && va.forcedAddr) || export.Encoder(enc).Tokens.Last.NeedObjectName())) {
                    return prevMarshalʗ3(enc, va, mo);
                }
                var (marshaler, _) = reflect.TypeAssert<Marshaler>(va.Value.Addr());
                var (val, err) = marshaler.MarshalJSON();
                if (err != nil) {
                    err = wrapErrUnsupported(err, "MarshalJSON method");
                    if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return @internal.NewMarshalerError(va.Value.Addr().Interface(), err, "MarshalJSON");
                    }
                    err = newMarshalErrorBefore(enc, t, err);
                    return collapseSemanticErrors(err);
                }
                var errΔ1 = enc.WriteValue(val); if (errΔ1 != nil) {
                    if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return @internal.NewMarshalerError(va.Value.Addr().Interface(), errΔ1, "MarshalJSON");
                    }
                    if (isSyntacticError(errΔ1)) {
                        errΔ1 = newMarshalErrorBefore(enc, t, errΔ1);
                    }
                    return errΔ1;
                }
                return default;
            };
        }
    }

    {
        var (needAddr, ok) = implements(t, jsonMarshalerToType); if (ok) {
            fncs.nonDefault = true;
            var prevMarshal = fncs.marshal;
            var prevMarshalʗ4 = prevMarshal;
            fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
                if (mo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && ((needAddr && va.forcedAddr) || export.Encoder(enc).Tokens.Last.NeedObjectName())) {
                    return prevMarshalʗ4(enc, va, mo);
                }
                var xe = export.Encoder(enc);
                var (prevDepth, prevLength) = xe.Tokens.DepthLength();
                xe.of(jsontext.encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 1));
                var (marshaler, _) = reflect.TypeAssert<MarshalerTo>(va.Value.Addr());
                var err = marshaler.MarshalJSONTo(enc);
                xe.of(jsontext.encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 0));
                var (currDepth, currLength) = xe.Tokens.DepthLength();
                if ((prevDepth != currDepth || prevLength + 1 != currLength) && err == nil) {
                    err = errNonSingularValue;
                }
                if (err != nil) {
                    if (errors.Is(err, errors.ErrUnsupported)) {
                        if (prevDepth == currDepth && prevLength == currLength) {
                            return prevMarshalʗ4(enc, va, mo);
                        }
                        err = errUnsupportedMutation;
                    }
                    if (mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return @internal.NewMarshalerError(va.Value.Addr().Interface(), err, "MarshalJSONTo");
                    }
                    if (!export.IsIOError(err)) {
                        err = newSemanticErrorWithPosition(new jsontext_Encoderжcoder(enc), t, prevDepth, prevLength, err);
                    }
                    return err;
                }
                return default;
            };
        }
    }

    {
        var (_, ok) = implements(t, textUnmarshalerType); if (ok) {
            fncs.nonDefault = true;
            fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
                var xd = export.Decoder(dec);
                ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
                var (val, err) = xd.ReadValue(Ꮡflags);
                if (err != nil) {
                    return err;
                }
                if (val.Kind() == (rune)'n') {
                    if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                        va.Value.SetZero();
                    }
                    return default;
                }
                if (val.Kind() != (rune)'"') {
                    return newUnmarshalErrorAfter(dec, t, errNonStringValue);
                }
                var s = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                var (unmarshaler, _) = reflect.TypeAssert<encoding.TextUnmarshaler>(va.Value.Addr());
                var errΔ1 = unmarshaler.UnmarshalText(s); if (errΔ1 != nil) {
                    errΔ1 = wrapErrUnsupported(errΔ1, "UnmarshalText method");
                    if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return errΔ1;
                    }
                    if (!isSemanticError(errΔ1) && !isSyntacticError(errΔ1) && !export.IsIOError(errΔ1)) {
                        errΔ1 = newUnmarshalErrorAfter(dec, t, errΔ1);
                    }
                    return errΔ1;
                }
                return default;
            };
        }
    }

    {
        var (_, ok) = implements(t, jsonUnmarshalerType); if (ok) {
            fncs.nonDefault = true;
            var prevUnmarshal = fncs.unmarshal;
            var prevUnmarshalʗ1 = prevUnmarshal;
            fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
                if (uo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && export.Decoder(dec).Tokens.Last.NeedObjectName()) {
                    return prevUnmarshalʗ1(dec, va, uo);
                }
                var (val, err) = dec.ReadValue();
                if (err != nil) {
                    return err;
                }
                var (unmarshaler, _) = reflect.TypeAssert<Unmarshaler>(va.Value.Addr());
                var errΔ1 = unmarshaler.UnmarshalJSON(val); if (errΔ1 != nil) {
                    errΔ1 = wrapErrUnsupported(errΔ1, "UnmarshalJSON method");
                    if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return errΔ1;
                    }
                    errΔ1 = newUnmarshalErrorAfter(dec, t, errΔ1);
                    return collapseSemanticErrors(errΔ1);
                }
                return default;
            };
        }
    }

    {
        var (_, ok) = implements(t, jsonUnmarshalerFromType); if (ok) {
            fncs.nonDefault = true;
            var prevUnmarshal = fncs.unmarshal;
            var prevUnmarshalʗ2 = prevUnmarshal;
            fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
                if (uo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && export.Decoder(dec).Tokens.Last.NeedObjectName()) {
                    return prevUnmarshalʗ2(dec, va, uo);
                }
                var xd = export.Decoder(dec);
                var (prevDepth, prevLength) = xd.Tokens.DepthLength();
                if (prevDepth == 1 && xd.AtEOF()) {
                    return io.EOF;
                }
                xd.of(jsontext.decoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 1));
                var (unmarshaler, _) = reflect.TypeAssert<UnmarshalerFrom>(va.Value.Addr());
                var err = unmarshaler.UnmarshalJSONFrom(dec);
                xd.of(jsontext.decoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.WithinArshalCall | 0));
                var (currDepth, currLength) = xd.Tokens.DepthLength();
                if ((prevDepth != currDepth || prevLength + 1 != currLength) && err == nil) {
                    err = errNonSingularValue;
                }
                if (err != nil) {
                    if (errors.Is(err, errors.ErrUnsupported)) {
                        if (prevDepth == currDepth && prevLength == currLength) {
                            return prevUnmarshalʗ2(dec, va, uo);
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
                        err = newSemanticErrorWithPosition(new jsontext_Decoderжcoder(dec), t, prevDepth, prevLength, err);
                    }
                    return err;
                }
                return default;
            };
        }
    }

    return Ꮡfncs;
}

internal static bool implementsAny(reflect.Type t, params ꓸꓸꓸreflectꓸType ifaceTypesʗp) {
    var ifaceTypes = ifaceTypesʗp.sslice();

    foreach (var (_, ifaceType) in range(ifaceTypes)) {
        var (_, ok) = implements(t, ifaceType); if (ok) {
            return true;
        }
    }
    return false;
}

internal static (bool needAddr, bool ok) implements(reflect.Type t, reflect.Type ifaceType) {
    switch (ᐧ) {
    case {} when t.Implements(ifaceType): {
        return (false, true);
    }
    case {} when reflect.PointerTo(t).Implements(ifaceType): {
        return (true, true);
    }
    default: {
        return (false, false);
    }}

}

} // end json_package
