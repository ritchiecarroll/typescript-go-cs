// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using bytes = bytes_package;
using errors = errors_package;
using io = io_package;
using reflect = reflect_package;
using slices = slices_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;

partial class json_package {

internal static error errRawEmbedNotObject = errors.New("embedded raw value must be a JSON object");

internal static reflect.Type jsontextValueType = reflect.TypeFor<jsontext.Value>();

internal static error marshalEmbeddedFallbackAll(ж<jsontext.Encoder> Ꮡenc, addressableValue va, ж<jsonopts.Struct> Ꮡmo, ref structField f, Func<slice<byte>, bool> insertUnquotedName) {
    GoFrame ᒐ = default;
    try {
        ref var enc = ref Ꮡenc.DerefOrNull();
        ref var mo = ref Ꮡmo.DerefOrNull();

        var v = new addressableValue(va.Value.Field(f.index0), va.forcedAddr);
        if (len(f.index) > 0) {
            v = v.fieldByIndex(f.index, false);
            if (!v.Value.IsValid()) {
                return default;
            }
        }
        v = v.indirect(false);
        if (!v.Value.IsValid()) {
            return default;
        }

        if (AreEqual(v.Value.Type(), jsontextValueType)) {
            var (b, _) = reflect.TypeAssert<jsontext.Value>(v.Value);
            if (len(b) == 0) {
                return default;
            }

            var dec = export.GetBufferedDecoder(b);
            defer((ᴛ0, ᴛ1) => ᴛ0.PutBufferedDecoder(ᴛ1), export, dec, ref ᒐ);
            var xd = export.Decoder(dec);
            xd.of(jsontext.decoderState.ᏑFlags).Set((jsonflags.Bools)((jsonflags.Bools)(jsonflags.AllowDuplicateNames | jsonflags.AllowInvalidUTF8) | 1));

            var (tok, err) = dec.ReadToken();
            if (err != nil) {
                if (AreEqual(err, io.EOF)) {
                    err = io.ErrUnexpectedEOF;
                }
                return newMarshalErrorBefore(Ꮡenc, v.Value.Type(), err);
            }
            if (tok.Kind() != (rune)'{') {
                return newMarshalErrorBefore(Ꮡenc, v.Value.Type(), errRawEmbedNotObject);
            }
            while (dec.PeekKind() != (rune)'}') {
                ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
                var (val, errΔ1) = xd.ReadValue(Ꮡflags);
                if (errΔ1 != nil) {
                    return newMarshalErrorBefore(Ꮡenc, v.Value.Type(), errΔ1);
                }
                if (insertUnquotedName != nil) {
                    var name = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                    if (!insertUnquotedName(name)) {
                        return newDuplicateNameError(enc.StackPointer().Parent(), val, enc.OutputOffset());
                    }
                }
                var errΔ2 = Ꮡenc.WriteValue(val); if (errΔ2 != nil) {
                    return errΔ2;
                }

                (val, errΔ1) = xd.ReadValue(Ꮡflags);
                if (errΔ1 != nil) {
                    return newMarshalErrorBefore(Ꮡenc, v.Value.Type(), errΔ1);
                }
                var errΔ3 = Ꮡenc.WriteValue(val); if (errΔ3 != nil) {
                    return errΔ3;
                }
            }
            var (_, errΔ4) = dec.ReadToken(); if (errΔ4 != nil) {
                return newMarshalErrorBefore(Ꮡenc, v.Value.Type(), errΔ4);
            }
            var errΔ5 = xd.CheckEOF(); if (errΔ5 != nil) {
                return newMarshalErrorBefore(Ꮡenc, v.Value.Type(), errΔ5);
            }
            return default;
        } else {
            ref var m = ref heap<addressableValue>(v, out var Ꮡm);
            nint n = m.Value.Len();
            if (n == 0) {
                return default;
            }
            var mk = newAddressableValue(m.Value.Type().Key());
            var mv = newAddressableValue(m.Value.Type().Elem());
            var mʗ1 = m;
            error marshalKey(addressableValue mkΔ1) {
                var (b, err) = jsonwire.AppendQuote(Ꮡenc.Value.AvailableBuffer(), slice<byte>(mkΔ1.Value.String()), Ꮡmo.ᏑFlags);
                if (err != nil) {
                    return newMarshalErrorBefore(Ꮡenc, mʗ1.Value.Type().Key(), err);
                }
                if (insertUnquotedName != nil) {
                    var isVerbatim = bytes.IndexByte(b, (byte)'\\') < 0;
                    var name = jsonwire.UnquoteMayCopy(b, isVerbatim);
                    if (!insertUnquotedName(name)) {
                        return newDuplicateNameError(Ꮡenc.Value.StackPointer().Parent(), b, Ꮡenc.Value.OutputOffset());
                    }
                }
                return Ꮡenc.WriteValue(b);
            }
            var marshalVal = f.fncs.marshal;
            if (mo.Marshalers != nil) {
                (marshalVal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshalVal, mv.Value.Type());
            }
            if (!mo.Flags.Get(jsonflags.Deterministic) || n <= 1) {
                for (var iter = m.Value.MapRange(); iter.Next(); ) {
                    mk.Value.SetIterKey(iter);
                    {
                        var err = marshalKey(mk); if (err != nil) {
                            return err;
                        }
                    }
                    mv.Value.Set(iter.Value());
                    {
                        var err = marshalVal(Ꮡenc, mv, Ꮡmo); if (err != nil) {
                            return err;
                        }
                    }
                }
            } else {
                var names = getStrings(n);
                for ((nint i, var iter) = (0, m.Value.MapRange()); i < n && iter.Next(); i++) {
                    mk.Value.SetIterKey(iter);
                    (names.ValueSlot)[i] = mk.Value.String();
                }
                slices.Sort<stringSlice, @string>(names.ValueSlot);
                foreach (var (_, name) in range<@string>(names.ValueSlot)) {
                    mk.Value.SetString(name);
                    {
                        var err = marshalKey(mk); if (err != nil) {
                            return err;
                        }
                    }
                    mv.Value.Set(m.Value.MapIndex(mk.Value));
                    {
                        var err = marshalVal(Ꮡenc, mv, Ꮡmo); if (err != nil) {
                            return err;
                        }
                    }
                }
                putStrings(names);
            }
            return default;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static error unmarshalEmbeddedFallbackNext(ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> Ꮡuo, ref structField f, slice<byte> quotedName, slice<byte> unquotedName) {
    ref var uo = ref Ꮡuo.DerefOrNull();

    var v = new addressableValue(va.Value.Field(f.index0), va.forcedAddr);
    if (len(f.index) > 0) {
        v = v.fieldByIndex(f.index, true);
    }
    v = v.indirect(true);

    if (AreEqual(v.Value.Type(), jsontextValueType)) {
        var (b, _) = reflect.TypeAssert<ж<jsontext.Value>>(v.Value.Addr());
        if (len(b.ValueSlot) == 0) {
            b.ValueSlot = append(b.ValueSlot, (byte)'{');
        } else {
            b.ValueSlot = jsonwire.TrimSuffixWhitespace(b.ValueSlot);
            if (jsonwire.HasSuffixByte(b.ValueSlot, (byte)'}')) {
                b.ValueSlot = jsonwire.TrimSuffixByte(b.ValueSlot, (byte)'}');
                b.ValueSlot = jsonwire.TrimSuffixWhitespace(b.ValueSlot);
                if (!jsonwire.HasSuffixByte(b.ValueSlot, (byte)',') && !jsonwire.HasSuffixByte(b.ValueSlot, (byte)'{')) {
                    b.ValueSlot = append(b.ValueSlot, (byte)',');
                }
            } else {
                return newUnmarshalErrorAfterWithSkipping(dec, v.Value.Type(), errRawEmbedNotObject);
            }
        }
        b.ValueSlot = appendꓸꓸꓸ(b.ValueSlot, quotedName);
        b.ValueSlot = append(b.ValueSlot, (byte)':');
        var (val, err) = dec.ReadValue();
        if (err != nil) {
            return err;
        }
        b.ValueSlot = appendꓸꓸꓸ(b.ValueSlot, val);
        b.ValueSlot = append(b.ValueSlot, (byte)'}');
        return default;
    } else {
        @string name = (@string)unquotedName;

        var m = v;
        if (m.Value.IsNil()) {
            m.Value.Set(reflect.MakeMap(m.Value.Type()));
        }
        var mk = reflect.ValueOf(name);
        var mkt = m.Value.Type().Key(); if (!AreEqual(mkt, stringType)) {
            mk = mk.Convert(mkt);
        }
        var mv = newAddressableValue(m.Value.Type().Elem());
        var v2 = m.Value.MapIndex(mk); if (v2.IsValid()) {
            mv.Value.Set(v2);
        }

        var unmarshal = f.fncs.unmarshal;
        if (uo.Unmarshalers != nil) {
            (unmarshal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshal, mv.Value.Type());
        }
        var err = unmarshal(dec, mv, Ꮡuo);
        m.Value.SetMapIndex(mk, mv.Value);
        if (err != nil) {
            return err;
        }
        return default;
    }
}

} // end json_package
