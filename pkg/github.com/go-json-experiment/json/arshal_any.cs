// Copyright 2022 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using cmp = cmp_package;
using errors = errors_package;
using math = math_package;
using reflect = reflect_package;
using slices = slices_package;
using strconv = strconv_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;

partial class json_package {

internal static error marshalValueAny(ж<jsontext.Encoder> enc, any val, ж<jsonopts.Struct> Ꮡmo) {
    ref var mo = ref Ꮡmo.DerefOrNull();

    switch (val.type()) {
    case null: {
        return enc.WriteToken(jsontext.Null);
    }
    case bool valΔ1: {
        return enc.WriteToken(jsontext.Bool(valΔ1));
    }
    case @string valΔ1: {
        return enc.WriteToken(jsontext.String(valΔ1));
    }
    case float64 valΔ1: {
        if (math.IsNaN(valΔ1) || math.IsInf(valΔ1, 0)) {
            break;
        }
        return enc.WriteToken(jsontext.Float(valΔ1));
    }
    case map<@string, any> valΔ1: {
        return marshalObjectAny(enc, valΔ1, Ꮡmo);
    }
    case slice<any> valΔ1: {
        return marshalArrayAny(enc, valΔ1, Ꮡmo);
    }}

    var v = newAddressableValue(reflect.TypeOf(val));
    v.Value.Set(reflect.ValueOf(val));
    var marshal = lookupArshaler(v.Value.Type()).marshal;
    if (mo.Marshalers != nil) {
        (marshal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshal, v.Value.Type());
    }
    return marshal(enc, v, Ꮡmo);
}

internal static (any, error) unmarshalValueAny(ж<jsontext.Decoder> dec, ref jsonopts.Struct uo) {
    var k = dec.PeekKind();
    switch (k) {
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'{'): {
        var (ᴛ1, ᴛ2) = unmarshalObjectAny(dec, ref uo);
        return (ᴛ1, ᴛ2);
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'['): {
        var (ᴛ1, ᴛ2) = unmarshalArrayAny(dec, ref uo);
        return (ᴛ1, ᴛ2);
    }
    default: {
        var xd = export.Decoder(dec);
        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        var (val, err) = xd.ReadValue(Ꮡflags);
        if (err != nil) {
            return (default, err);
        }
        switch (val.Kind()) {
        case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'n'): {
            return (default, default);
        }
        case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'f'): {
            return (false, default);
        }
        case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'t'): {
            return (true, default);
        }
        case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'"'): {
            val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
            if (xd.StringCache == nil) {
                xd.StringCache = Ꮡ(new stringCache(256));
            }
            return (makeString(xd.StringCache, val), default);
        }
        case var ᴛ2 when ᴛ2 == (jsontext.Kind)((rune)'0'): {
            if (uo.Flags.Get(jsonflags.UnmarshalAnyWithRawNumber)) {
                return (@internal.RawNumberOf(val), default);
            }
            var (fv, errΔ2) = strconv.ParseFloat((@string)(slice<byte>)val, 64);
            if (errΔ2 != nil) {
                return (fv, newUnmarshalErrorAfterWithValue(dec, float64Type, errors.Unwrap(errΔ2)));
            }
            return (fv, default);
        }
        default: {
            throw panic("BUG: invalid kind: " + k.String());
            break;
        }}

        break;
    }}

}

internal static error marshalObjectAny(ж<jsontext.Encoder> enc, map<@string, any> obj, ж<jsonopts.Struct> Ꮡmo) {
    GoFrame ᒐ = default;
    try {
        ref var mo = ref Ꮡmo.DerefOrNull();

        var xe = export.Encoder(enc);
        if (xe.Tokens.Depth() > startDetectingCyclesAfter) {
            ref var v = ref heap<reflect.Value>(reflect.ValueOf(obj), out var Ꮡv);
            var err = visitPointer(ref (xe.ᏑSeenPointers).DerefOrNull(), v); if (err != nil) {
                return newMarshalErrorBefore(enc, mapStringAnyType, err);
            }
            defer((ᴛ1, ᴛ2) => leavePointer(ref ᴛ1.DerefOrNull(), ᴛ2), xe.ᏑSeenPointers, v, ref ᒐ);
        }

        if (len(obj) == 0) {
            if (mo.Flags.Get(jsonflags.FormatNilMapAsNull) && obj == nil) {
                return enc.WriteToken(jsontext.Null);
            }
            if (!mo.Flags.Get(jsonflags.AnyWhitespace) && !xe.Tokens.Last.NeedObjectName()) {
                xe.Buf = append(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'{'), ((@string)"{}").ꓸꓸꓸ);
                xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
                if (xe.NeedFlush()) {
                    return xe.Flush();
                }
                return default;
            }
        }

        {
            var err = enc.WriteToken(jsontext.BeginObject); if (err != nil) {
                return err;
            }
        }
        if (!mo.Flags.Get(jsonflags.AllowInvalidUTF8)) {
            xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.DisableNamespace();
        }
        if (!mo.Flags.Get(jsonflags.Deterministic) || len(obj) <= 1) {
            foreach (var (name, val) in obj) {
                {
                    var err = enc.WriteToken(jsontext.String(name)); if (err != nil) {
                        return err;
                    }
                }
                {
                    var err = marshalValueAny(enc, val, Ꮡmo); if (err != nil) {
                        return err;
                    }
                }
            }
        } else {
            var names = getStrings(len(obj));
            nint i = default;
            foreach (var (name, _) in obj) {
                (names.ValueSlot)[i] = name;
                i++;
            }
            slices.Sort<stringSlice, @string>(names.ValueSlot);
            foreach (var (_, name) in range<@string>(names.ValueSlot)) {
                {
                    var err = enc.WriteToken(jsontext.String(name)); if (err != nil) {
                        return err;
                    }
                }
                {
                    var err = marshalValueAny(enc, obj[name], Ꮡmo); if (err != nil) {
                        return err;
                    }
                }
            }
            putStrings(names);
        }
        {
            var err = enc.WriteToken(jsontext.EndObject); if (err != nil) {
                return err;
            }
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (map<@string, any>, error) unmarshalObjectAny(ж<jsontext.Decoder> Ꮡdec, ref jsonopts.Struct uo) {
    ref var dec = ref Ꮡdec.DerefOrNull();

    {
        var (tok, err) = Ꮡdec.ReadToken();
        switch (ᐧ) {
        case {} when err != nil: {
            return (default, err);
        }
        case {} when tok.Kind() != (rune)'{': {
            throw panic("BUG: invalid kind: " + tok.Kind().String());
            break;
        }}
    }

    var obj = new map<@string, any>();
    if (!uo.Flags.Get(jsonflags.AllowInvalidUTF8)) {
        Ꮡ(export.Decoder(Ꮡdec).Tokens).ᏑLast.DisableNamespace();
    }
    error errUnmarshal = default;
    while (Ꮡdec.PeekKind() != (rune)'}') {
        var (tok, err) = Ꮡdec.ReadToken();
        if (err != nil) {
            return (obj, err);
        }
        @string name = tok.String();

        var (_, ok) = obj[name, ꟷ]; if (ok) {
            var nameΔ1 = export.Decoder(Ꮡdec).PreviousTokenOrValue();
            var errΔ1 = newDuplicateNameError(dec.StackPointer(), default, dec.InputOffset() - len64(nameΔ1));
            return (obj, errΔ1);
        }

        (var val, err) = unmarshalValueAny(Ꮡdec, ref uo);
        obj[name] = val;
        if (err != nil) {
            if (isFatalError(err, uo.Flags)) {
                return (obj, err);
            }
            errUnmarshal = cmp.Or(err, errUnmarshal);
        }
    }
    {
        var (_, err) = Ꮡdec.ReadToken(); if (err != nil) {
            return (obj, err);
        }
    }
    return (obj, errUnmarshal);
}

internal static error marshalArrayAny(ж<jsontext.Encoder> enc, slice<any> arr, ж<jsonopts.Struct> Ꮡmo) {
    GoFrame ᒐ = default;
    try {
        ref var mo = ref Ꮡmo.DerefOrNull();

        var xe = export.Encoder(enc);
        if (xe.Tokens.Depth() > startDetectingCyclesAfter) {
            ref var v = ref heap<reflect.Value>(reflect.ValueOf(arr), out var Ꮡv);
            var err = visitPointer(ref (xe.ᏑSeenPointers).DerefOrNull(), v); if (err != nil) {
                return newMarshalErrorBefore(enc, sliceAnyType, err);
            }
            defer((ᴛ1, ᴛ2) => leavePointer(ref ᴛ1.DerefOrNull(), ᴛ2), xe.ᏑSeenPointers, v, ref ᒐ);
        }

        if (len(arr) == 0) {
            if (mo.Flags.Get(jsonflags.FormatNilSliceAsNull) && arr == nil) {
                return enc.WriteToken(jsontext.Null);
            }
            if (!mo.Flags.Get(jsonflags.AnyWhitespace) && !xe.Tokens.Last.NeedObjectName()) {
                xe.Buf = append(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'['), ((@string)"[]").ꓸꓸꓸ);
                xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
                if (xe.NeedFlush()) {
                    return xe.Flush();
                }
                return default;
            }
        }

        {
            var err = enc.WriteToken(jsontext.BeginArray); if (err != nil) {
                return err;
            }
        }
        foreach (var (_, val) in range(arr)) {
            var err = marshalValueAny(enc, val, Ꮡmo); if (err != nil) {
                return err;
            }
        }
        {
            var err = enc.WriteToken(jsontext.EndArray); if (err != nil) {
                return err;
            }
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (slice<any>, error) unmarshalArrayAny(ж<jsontext.Decoder> dec, ref jsonopts.Struct uo) {
    {
        var (tok, err) = dec.ReadToken();
        switch (ᐧ) {
        case {} when err != nil: {
            return (default, err);
        }
        case {} when tok.Kind() != (rune)'[': {
            throw panic("BUG: invalid kind: " + tok.Kind().String());
            break;
        }}
    }

    var arr = slice<any>([]);
    error errUnmarshal = default;
    while (dec.PeekKind() != (rune)']') {
        var (val, err) = unmarshalValueAny(dec, ref uo);
        arr = append(arr, val);
        if (err != nil) {
            if (isFatalError(err, uo.Flags)) {
                return (arr, err);
            }
            errUnmarshal = cmp.Or(errUnmarshal, err);
        }
    }
    {
        var (_, err) = dec.ReadToken(); if (err != nil) {
            return (arr, err);
        }
    }
    return (arr, errUnmarshal);
}

} // end json_package
