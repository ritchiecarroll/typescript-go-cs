// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

global using seenPointers = go.map<object, go.EmptyStruct>;

namespace go.github.com.go_json_experiment;

using bytes = bytes_package;
using cmp = cmp_package;
using encoding = encoding_package;
using base32 = go.encoding.base32_package;
using base64 = go.encoding.base64_package;
using hex = go.encoding.hex_package;
using errors = errors_package;
using fmt = fmt_package;
using math = math_package;
using reflect = reflect_package;
using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using sync = sync_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using @unsafe = unsafe_package;
using go.encoding;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using static json_package;

partial class json_package {

internal const bool optimizeCommon = true;

internal static reflect.Type anyType = reflect.TypeFor<any>();
internal static reflect.Type boolType = reflect.TypeFor<bool>();
internal static reflect.Type stringType = reflect.TypeFor<@string>();
internal static reflect.Type float64Type = reflect.TypeFor<float64>();
internal static reflect.Type mapStringAnyType = reflect.TypeFor<map<@string, any>>();
internal static reflect.Type sliceAnyType = reflect.TypeFor<slice<any>>();
internal static reflect.Type bytesType = reflect.TypeFor<slice<byte>>();
internal static reflect.Type emptyStructType = reflect.TypeFor<EmptyStruct>();

internal static UntypedInt startDetectingCyclesAfter => 1000;

partial struct typedPointer {
    internal reflect.Type typ;
    internal any ptr;
    internal nint len;
}

internal static error visitPointer(ref seenPointers m, reflect.Value v) {
    var p = new typedPointer(v.Type(), @unsafe.Pointer.OrTypedNil(v.UnsafePointer()), sliceLen(v));
    var (_, ok) = (m)[p, ꟷ]; if (ok) {
        return @internal.ErrCycle;
    }
    if (m == nil) {
        m = new seenPointers();
    }
    (m)[p] = new EmptyStruct();
    return default;
}
internal static void leavePointer(ref seenPointers m, reflect.Value v) {
    var p = new typedPointer(v.Type(), @unsafe.Pointer.OrTypedNil(v.UnsafePointer()), sliceLen(v));
    delete(m, p);
}

internal static nint sliceLen(reflect.Value v) {
    if (v.Kind() == reflect.Slice) {
        return v.Len();
    }
    return 0;
}

internal static int64 len64<Bytes>(Bytes @in)
    where Bytes : /* ~[]byte | ~string */ IByteSeq<Bytes, byte>, new()
{
    return (int64)len(@in);
}

internal static ж<arshaler> makeDefaultArshaler(reflect.Type t) {
    switch (t.Kind()) {
    case var ᴛ when ᴛ == reflect.Bool: {
        return makeBoolArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.String: {
        return makeStringArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.Int || ᴛ == reflect.Int8 || ᴛ == reflect.Int16 || ᴛ == reflect.Int32 || ᴛ == reflect.Int64: {
        return makeIntArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.Uint || ᴛ == reflect.Uint8 || ᴛ == reflect.Uint16 || ᴛ == reflect.Uint32 || ᴛ == reflect.Uint64 || ᴛ == reflect.Uintptr: {
        return makeUintArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.Float32 || ᴛ == reflect.Float64: {
        return makeFloatArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.Map: {
        return makeMapArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.Struct: {
        return makeStructArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.Slice: {
        var fncs = makeSliceArshaler(t);
        if (t.Elem().Kind() == reflect.Uint8) {
            return makeBytesArshaler(t, fncs);
        }
        return fncs;
    }
    case var ᴛ when ᴛ == reflect.Array: {
        var fncs = makeArrayArshaler(t);
        if (t.Elem().Kind() == reflect.Uint8) {
            return makeBytesArshaler(t, fncs);
        }
        return fncs;
    }
    case var ᴛ when ᴛ == reflect.Pointer: {
        return makePointerArshaler(t);
    }
    case var ᴛ when ᴛ == reflect.Interface: {
        return makeInterfaceArshaler(t);
    }
    default: {
        return makeInvalidArshaler(t);
    }}

}

internal static ж<arshaler> makeBoolArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        var xe = export.Encoder(enc);
        bool stringify = default;
        if (mo.Flags.Has(jsonflags.TagFlags)) {
            stringify = mo.Flags.Get(jsonflags.StringTag) && mo.Flags.Get(jsonflags.StringifyWithLegacySemantics);
            if (mo.Flags.Get(jsonflags.StringTag) && !mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyWithLegacySemantics | jsonflags.ReportErrorsWithLegacySemantics))) {
                return newMarshalErrorBefore(enc, t, errInvalidStringTag);
            }
            if (mo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
            }
        }

        if (optimizeCommon && !mo.Flags.Get(jsonflags.AnyWhitespace) && !stringify && !xe.Tokens.Last.NeedObjectName()) {
            xe.Buf = strconv.AppendBool(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'t'), va.Value.Bool());
            xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
            if (xe.NeedFlush()) {
                return xe.Flush();
            }
            return default;
        }

        if (stringify) {
            if (va.Value.Bool()) {
                return enc.WriteToken(jsontext.String("true"));
            } else {
                return enc.WriteToken(jsontext.String("false"));
            }
        }
        return enc.WriteToken(jsontext.Bool(va.Value.Bool()));
    };
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        bool stringify = default;
        if (uo.Flags.Has(jsonflags.TagFlags)) {
            stringify = uo.Flags.Get(jsonflags.StringTag) && uo.Flags.Get(jsonflags.StringifyWithLegacySemantics);
            if (uo.Flags.Get(jsonflags.StringTag) && !uo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyWithLegacySemantics | jsonflags.ReportErrorsWithLegacySemantics))) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
            }
        }
        var (tok, err) = dec.ReadToken();
        if (err != nil) {
            return err;
        }
        var k = tok.Kind();
        switch (k) {
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                va.Value.SetBool(false);
            }
            return default;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'t') || ᴛ == (jsontext.Kind)((rune)'f'): {
            if (!stringify) {
                va.Value.SetBool(tok.Bool());
                return default;
            }
            break;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'"'): {
            if (stringify) {
                var exprᴛ2 = tok.String();
                switch (ᐧ) {
                case {} when exprᴛ2 == "true"u8: {
                    va.Value.SetBool(true);
                    break;
                }
                case {} when exprᴛ2 == "false"u8: {
                    va.Value.SetBool(false);
                    break;
                }
                default: {
                    if (uo.Flags.Get(jsonflags.StringifyWithLegacySemantics) && tok.String() == "null"u8) {
                        if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                            va.Value.SetBool(false);
                        }
                        return default;
                    }
                    return newUnmarshalErrorAfterWithValue(dec, t, strconv.ErrSyntax);
                }}

                return default;
            }
            break;
        }}

        return newUnmarshalErrorAfterWithSkipping(dec, t, default);
    };
    return Ꮡfncs;
}

internal static ж<arshaler> makeStringArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        var xe = export.Encoder(enc);
        bool stringify = default;
        if (mo.Flags.Has(jsonflags.TagFlags)) {
            stringify = mo.Flags.Get(jsonflags.StringTag) && mo.Flags.Get(jsonflags.StringifyWithLegacySemantics);
            if (mo.Flags.Get(jsonflags.StringTag) && !mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyWithLegacySemantics | jsonflags.ReportErrorsWithLegacySemantics))) {
                return newMarshalErrorBefore(enc, t, errInvalidStringTag);
            }
            if (mo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
            }
        }

        @string s = va.Value.String();
        if (optimizeCommon && !mo.Flags.Get(jsonflags.AnyWhitespace) && !stringify && !xe.Tokens.Last.NeedObjectName()) {
            var b = xe.Buf;
            b = xe.Tokens.MayAppendDelim(b, (rune)'"');
            (b, var err) = jsonwire.AppendQuote(b, slice<byte>(s), mo.ᏑFlags);
            if (err == nil) {
                xe.Buf = b;
                xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
                if (xe.NeedFlush()) {
                    return xe.Flush();
                }
                return default;
            }
        }

        if (stringify) {
            var (b, err) = jsonwire.AppendQuote(default, slice<byte>(s), mo.ᏑFlags);
            if (err != nil) {
                return newMarshalErrorBefore(enc, t, new jsontext.SyntacticErrorжerror(Ꮡ(new jsontext.SyntacticError(Err: err))));
            }
            (var q, err) = jsontext.AppendQuote(default, b);
            if (err != nil) {
                throw panic("BUG: second AppendQuote should never fail: " + err.Error());
            }
            return enc.WriteValue(q);
        }
        return enc.WriteToken(jsontext.String(s));
    };
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        var xd = export.Decoder(dec);
        bool stringify = default;
        if (uo.Flags.Has(jsonflags.TagFlags)) {
            stringify = uo.Flags.Get(jsonflags.StringTag) && uo.Flags.Get(jsonflags.StringifyWithLegacySemantics);
            if (uo.Flags.Get(jsonflags.StringTag) && !uo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyWithLegacySemantics | jsonflags.ReportErrorsWithLegacySemantics))) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
            }
        }
        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        var (val, err) = xd.ReadValue(Ꮡflags);
        if (err != nil) {
            return err;
        }
        var k = val.Kind();
        switch (k) {
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                va.Value.SetString("");
            }
            return default;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'"'): {
            val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
            if (stringify) {
                (val, err) = jsontext.AppendUnquote(default, val);
                if (err != nil) {
                    return newUnmarshalErrorAfter(dec, t, err);
                }
                if (uo.Flags.Get(jsonflags.StringifyWithLegacySemantics) && ((@string)(slice<byte>)val) == "null"u8) {
                    if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                        va.Value.SetString("");
                    }
                    return default;
                }
            }
            if (xd.StringCache == nil) {
                xd.StringCache = Ꮡ(new stringCache(256));
            }
            @string str = makeString(xd.StringCache, val);
            va.Value.SetString(str);
            return default;
        }}

        return newUnmarshalErrorAfter(dec, t, default);
    };
    return Ꮡfncs;
}

internal static Func<slice<byte>, slice<byte>, slice<byte>> appendEncodeBase16 = hex.AppendEncode;
internal static Func<slice<byte>, slice<byte>, slice<byte>> appendEncodeBase32 = base32.StdEncoding.AppendEncode;
internal static Func<slice<byte>, slice<byte>, slice<byte>> appendEncodeBase32Hex = base32.HexEncoding.AppendEncode;
internal static Func<slice<byte>, slice<byte>, slice<byte>> appendEncodeBase64 = base64.StdEncoding.AppendEncode;
internal static Func<slice<byte>, slice<byte>, slice<byte>> appendEncodeBase64URL = base64.URLEncoding.AppendEncode;
internal static Func<nint, nint> encodedLenBase16 = hex.EncodedLen;
internal static Func<nint, nint> encodedLenBase32 = base32.StdEncoding.EncodedLen;
internal static Func<nint, nint> encodedLenBase32Hex = base32.HexEncoding.EncodedLen;
internal static Func<nint, nint> encodedLenBase64 = base64.StdEncoding.EncodedLen;
internal static Func<nint, nint> encodedLenBase64URL = base64.URLEncoding.EncodedLen;
internal static Func<slice<byte>, slice<byte>, (slice<byte>, error)> appendDecodeBase16 = hex.AppendDecode;
internal static Func<slice<byte>, slice<byte>, (slice<byte>, error)> appendDecodeBase32 = base32.StdEncoding.AppendDecode;
internal static Func<slice<byte>, slice<byte>, (slice<byte>, error)> appendDecodeBase32Hex = base32.HexEncoding.AppendDecode;
internal static Func<slice<byte>, slice<byte>, (slice<byte>, error)> appendDecodeBase64 = base64.StdEncoding.AppendDecode;
internal static Func<slice<byte>, slice<byte>, (slice<byte>, error)> appendDecodeBase64URL = base64.URLEncoding.AppendDecode;

internal static ж<arshaler> makeBytesArshaler(reflect.Type t, ж<arshaler> Ꮡfncs) {
    ref var fncs = ref Ꮡfncs.DerefOrNull();

    var marshalArray = fncs.marshal;
    var isNamedByte = t.Elem().PkgPath() != ""u8;
    var hasMarshaler = implementsAny(t.Elem(), allMarshalerTypes.ꓸꓸꓸ);
    var marshalArrayʗ1 = marshalArray;
    fncs.marshal = (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        if (!mo.Flags.Get(jsonflags.FormatBytesWithLegacySemantics) && isNamedByte) {
            return marshalArrayʗ1(enc, va, mo);
        }
        var xe = export.Encoder(enc);
        ref var appendEncode = ref heap<Func<slice<byte>, slice<byte>, slice<byte>>>(appendEncodeBase64, out var ᏑappendEncode);
        if (mo.Flags.Has((jsonflags.Bools)((jsonflags.Bools)((jsonflags.Bools)(jsonflags.TagFlags | jsonflags.FormatByteArrayAsArray) | jsonflags.FormatBytesWithLegacySemantics) | jsonflags.FormatNilSliceAsNull))) {
            switch (ᐧ) {
            case {} when mo.Flags.Get(jsonflags.StringTag) && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics): {
                return newMarshalErrorBefore(enc, t, errInvalidStringTag);
            }
            case {} when mo.Flags.Has(jsonflags.FormatTag): {
                var exprᴛ1 = mo.Format;
                switch (ᐧ) {
                case {} when exprᴛ1 == "base64"u8: {
                    ᏑappendEncode.ValueSlot = appendEncodeBase64;
                    break;
                }
                case {} when exprᴛ1 == "base64url"u8: {
                    ᏑappendEncode.ValueSlot = appendEncodeBase64URL;
                    break;
                }
                case {} when exprᴛ1 == "base32"u8: {
                    ᏑappendEncode.ValueSlot = appendEncodeBase32;
                    break;
                }
                case {} when exprᴛ1 == "base32hex"u8: {
                    ᏑappendEncode.ValueSlot = appendEncodeBase32Hex;
                    break;
                }
                case {} when exprᴛ1 == "base16"u8 || exprᴛ1 == "hex"u8: {
                    ᏑappendEncode.ValueSlot = appendEncodeBase16;
                    break;
                }
                case {} when exprᴛ1 == "array"u8: {
                    mo.ᏑFlags.Clear(jsonflags.FormatTag);
                    return marshalArrayʗ1(enc, va, mo);
                }
                default: {
                    return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
                }}

                break;
            }
            case {} when mo.Flags.Get(jsonflags.FormatByteArrayAsArray) && va.Value.Kind() == reflect.Array: {
                return marshalArrayʗ1(enc, va, mo);
            }
            case {} when mo.Flags.Get(jsonflags.FormatBytesWithLegacySemantics) && hasMarshaler: {
                return marshalArrayʗ1(enc, va, mo);
            }}

            if (mo.Flags.Get(jsonflags.FormatNilSliceAsNull) && va.Value.Kind() == reflect.Slice && va.Value.IsNil()) {
                return enc.WriteToken(jsontext.Null);
            }
        }
        var vaʗ1 = va;
        return xe.AppendRaw((rune)'"', true, (slice<byte> b) => (ᏑappendEncode.ValueSlot(b, vaʗ1.Value.Bytes()), default));
    };
    var unmarshalArray = fncs.unmarshal;
    var unmarshalArrayʗ1 = unmarshalArray;
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        if (!uo.Flags.Get(jsonflags.FormatBytesWithLegacySemantics) && isNamedByte) {
            return unmarshalArrayʗ1(dec, va, uo);
        }
        var xd = export.Decoder(dec);
        var (appendDecode, encodedLen) = (appendDecodeBase64, encodedLenBase64);
        if (uo.Flags.Has((jsonflags.Bools)((jsonflags.Bools)(jsonflags.TagFlags | jsonflags.FormatByteArrayAsArray) | jsonflags.FormatBytesWithLegacySemantics))) {
            switch (ᐧ) {
            case {} when uo.Flags.Get(jsonflags.StringTag) && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics): {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            case {} when uo.Flags.Has(jsonflags.FormatTag): {
                var exprᴛ2 = uo.Format;
                switch (ᐧ) {
                case {} when exprᴛ2 == "base64"u8: {
                    (appendDecode, encodedLen) = (appendDecodeBase64, encodedLenBase64);
                    break;
                }
                case {} when exprᴛ2 == "base64url"u8: {
                    (appendDecode, encodedLen) = (appendDecodeBase64URL, encodedLenBase64URL);
                    break;
                }
                case {} when exprᴛ2 == "base32"u8: {
                    (appendDecode, encodedLen) = (appendDecodeBase32, encodedLenBase32);
                    break;
                }
                case {} when exprᴛ2 == "base32hex"u8: {
                    (appendDecode, encodedLen) = (appendDecodeBase32Hex, encodedLenBase32Hex);
                    break;
                }
                case {} when exprᴛ2 == "base16"u8 || exprᴛ2 == "hex"u8: {
                    (appendDecode, encodedLen) = (appendDecodeBase16, encodedLenBase16);
                    break;
                }
                case {} when exprᴛ2 == "array"u8: {
                    uo.ᏑFlags.Clear(jsonflags.FormatTag);
                    return unmarshalArrayʗ1(dec, va, uo);
                }
                default: {
                    return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
                }}

                break;
            }
            case {} when uo.Flags.Get(jsonflags.FormatByteArrayAsArray) && va.Value.Kind() == reflect.Array: {
                return unmarshalArrayʗ1(dec, va, uo);
            }
            case {} when uo.Flags.Get(jsonflags.FormatBytesWithLegacySemantics) && dec.PeekKind() == (rune)'[': {
                return unmarshalArrayʗ1(dec, va, uo);
            }}

        }
        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        var (val, err) = xd.ReadValue(Ꮡflags);
        if (err != nil) {
            return err;
        }
        var k = val.Kind();
        switch (k) {
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics) || va.Value.Kind() != reflect.Array) {
                va.Value.SetZero();
            }
            return default;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'"'): {
            val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
            var (b, errΔ4) = appendDecode(va.Value.Bytes()[..0], val);
            if (errΔ4 != nil) {
                return newUnmarshalErrorAfter(dec, t, errΔ4);
            }
            if (len(val) != encodedLen(len(b)) && !uo.Flags.Get(jsonflags.ParseBytesWithLooseRFC4648)) {
                nint i = bytes.IndexAny(val, "\r\n");
                var errΔ5 = fmt.Errorf("illegal character %s at offset %d"u8, jsonwire.QuoteRune(val.slice(i)), i);
                return newUnmarshalErrorAfter(dec, t, errΔ5);
            }

            if (va.Value.Kind() == reflect.Array) {
                var dst = va.Value.Bytes();
                clear(dst.slice(copy(dst, b)));
                if (len(b) != len(dst) && !uo.Flags.Get(jsonflags.UnmarshalArrayFromAnyLength)) {
                    var errΔ6 = fmt.Errorf("decoded length of %d mismatches array length of %d"u8, len(b), len(dst));
                    return newUnmarshalErrorAfter(dec, t, errΔ6);
                }
            } else {
                if (b == nil) {
                    b = [];
                }
                va.Value.SetBytes(b);
            }
            return default;
        }}

        return newUnmarshalErrorAfter(dec, t, default);
    };
    return Ꮡfncs;
}

internal static ж<arshaler> makeIntArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    nint bits = t.Bits();
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        var xe = export.Encoder(enc);
        var stringify = xe.Tokens.Last.NeedObjectName() || mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
        if (mo.Flags.Has(jsonflags.FormatTag)) {
            return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
        }

        if (optimizeCommon && !mo.Flags.Get(jsonflags.AnyWhitespace) && !stringify) {
            xe.Buf = strconv.AppendInt(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'0'), va.Value.Int(), 10);
            xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
            if (xe.NeedFlush()) {
                return xe.Flush();
            }
            return default;
        }

        var k = stringOrNumberKind(stringify);
        var vaʗ1 = va;
        return xe.AppendRaw(k, true, (slice<byte> b) => (strconv.AppendInt(b, vaʗ1.Value.Int(), 10), default));
    };
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        var xd = export.Decoder(dec);
        var stringify = xd.Tokens.Last.NeedObjectName() || uo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
        if (uo.Flags.Has(jsonflags.FormatTag)) {
            return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
        }
        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        var (val, err) = xd.ReadValue(Ꮡflags);
        if (err != nil) {
            return err;
        }
        var k = val.Kind();
        var matchᴛ1 = false;
        if (k == (jsontext.Kind)((rune)'n')) { matchᴛ1 = true;
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                va.Value.SetInt(0);
            }
            return default;
        }
        if (k == (jsontext.Kind)((rune)'"')) {
            do {
                if (!stringify) {
                    break;
                }
                val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                if (uo.Flags.Get(jsonflags.StringifyWithLegacySemantics)) {
                    var (n, errΔ2) = strconv.ParseInt((@string)(slice<byte>)val, 10, bits);
                    if (errΔ2 != nil) {
                        if (((@string)(slice<byte>)val) == "null"u8) {
                            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                                va.Value.SetInt(0);
                            }
                            return default;
                        }
                        return newUnmarshalErrorAfterWithValue(dec, t, errors.Unwrap(errΔ2));
                    }
                    va.Value.SetInt(n);
                    return default;
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1 && k == (jsontext.Kind)((rune)'0')) { matchᴛ1 = true;
            do {
                if (stringify && k == (rune)'0') {
                    break;
                }
                nint negOffset = default;
                var neg = len(val) > 0 && val[0] == '-';
                if (neg) {
                    negOffset = 1;
                }
                var (n, ok) = jsonwire.ParseUint(val.slice(negOffset));
                var maxInt = ((uint64)1).Lsh((int64)(bits - 1));
                var overflow = (neg && n > maxInt) || (!neg && n > maxInt - 1);
                if (!ok) {
                    if (n != math.MaxUint64) {
                        return newUnmarshalErrorAfterWithValue(dec, t, strconv.ErrSyntax);
                    }
                    overflow = true;
                }
                if (overflow) {
                    return newUnmarshalErrorAfterWithValue(dec, t, strconv.ErrRange);
                }
                if (neg) {
                    va.Value.SetInt((int64)(((uint64)0 - n)));
                } else {
                    va.Value.SetInt((int64)(+n));
                }
                return default;
            } while (false);
        }

        return newUnmarshalErrorAfter(dec, t, default);
    };
    return Ꮡfncs;
}

internal static ж<arshaler> makeUintArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    nint bits = t.Bits();
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        var xe = export.Encoder(enc);
        var stringify = xe.Tokens.Last.NeedObjectName() || mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
        if (mo.Flags.Has(jsonflags.FormatTag)) {
            return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
        }

        if (optimizeCommon && !mo.Flags.Get(jsonflags.AnyWhitespace) && !stringify) {
            xe.Buf = strconv.AppendUint(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'0'), va.Value.Uint(), 10);
            xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
            if (xe.NeedFlush()) {
                return xe.Flush();
            }
            return default;
        }

        var k = stringOrNumberKind(stringify);
        var vaʗ1 = va;
        return xe.AppendRaw(k, true, (slice<byte> b) => (strconv.AppendUint(b, vaʗ1.Value.Uint(), 10), default));
    };
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        var xd = export.Decoder(dec);
        var stringify = xd.Tokens.Last.NeedObjectName() || uo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
        if (uo.Flags.Has(jsonflags.FormatTag)) {
            return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
        }
        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        var (val, err) = xd.ReadValue(Ꮡflags);
        if (err != nil) {
            return err;
        }
        var k = val.Kind();
        var matchᴛ1 = false;
        if (k == (jsontext.Kind)((rune)'n')) { matchᴛ1 = true;
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                va.Value.SetUint(0);
            }
            return default;
        }
        if (k == (jsontext.Kind)((rune)'"')) {
            do {
                if (!stringify) {
                    break;
                }
                val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                if (uo.Flags.Get(jsonflags.StringifyWithLegacySemantics)) {
                    var (n, errΔ2) = strconv.ParseUint((@string)(slice<byte>)val, 10, bits);
                    if (errΔ2 != nil) {
                        if (((@string)(slice<byte>)val) == "null"u8) {
                            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                                va.Value.SetUint(0);
                            }
                            return default;
                        }
                        return newUnmarshalErrorAfterWithValue(dec, t, errors.Unwrap(errΔ2));
                    }
                    va.Value.SetUint(n);
                    return default;
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1 && k == (jsontext.Kind)((rune)'0')) { matchᴛ1 = true;
            do {
                if (stringify && k == (rune)'0') {
                    break;
                }
                var (n, ok) = jsonwire.ParseUint(val);
                var maxUint = ((uint64)1).Lsh((int64)(bits));
                var overflow = n > maxUint - 1;
                if (!ok) {
                    if (n != math.MaxUint64) {
                        return newUnmarshalErrorAfterWithValue(dec, t, strconv.ErrSyntax);
                    }
                    overflow = true;
                }
                if (overflow) {
                    return newUnmarshalErrorAfterWithValue(dec, t, strconv.ErrRange);
                }
                va.Value.SetUint(n);
                return default;
            } while (false);
        }

        return newUnmarshalErrorAfter(dec, t, default);
    };
    return Ꮡfncs;
}

internal static ж<arshaler> makeFloatArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    nint bits = t.Bits();
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        var xe = export.Encoder(enc);
        var stringify = xe.Tokens.Last.NeedObjectName() || mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
        bool allowNonFinite = default;
        if (mo.Flags.Has(jsonflags.FormatTag)) {
            if (mo.Format == "nonfinite"u8) {
                allowNonFinite = true;
            } else {
                return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
            }
        }

        var fv = va.Value.Float();
        if (math.IsNaN(fv) || math.IsInf(fv, 0)) {
            if (!allowNonFinite) {
                var err = fmt.Errorf("unsupported value: %v"u8, fv);
                return newMarshalErrorBefore(enc, t, err);
            }
            return enc.WriteToken(jsontext.Float(fv));
        }

        if (optimizeCommon && !mo.Flags.Get(jsonflags.AnyWhitespace) && !stringify) {
            xe.Buf = jsonwire.AppendFloat(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'0'), fv, bits);
            xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
            if (xe.NeedFlush()) {
                return xe.Flush();
            }
            return default;
        }

        var k = stringOrNumberKind(stringify);
        var vaʗ1 = va;
        return xe.AppendRaw(k, true, (slice<byte> b) => (jsonwire.AppendFloat(b, vaʗ1.Value.Float(), bits), default));
    };
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        var xd = export.Decoder(dec);
        var stringify = xd.Tokens.Last.NeedObjectName() || uo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.StringTag));
        bool allowNonFinite = default;
        if (uo.Flags.Has(jsonflags.FormatTag)) {
            if (uo.Format == "nonfinite"u8) {
                allowNonFinite = true;
            } else {
                return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
            }
        }

        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        var (val, err) = xd.ReadValue(Ꮡflags);
        if (err != nil) {
            return err;
        }
        var k = val.Kind();
        var matchᴛ1 = false;
        if (k == (jsontext.Kind)((rune)'n')) { matchᴛ1 = true;
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                va.Value.SetFloat(0D);
            }
            return default;
        }
        if (k == (jsontext.Kind)((rune)'"')) {
            do {
                val = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                if (allowNonFinite) {
                    var exprᴛ2 = ((@string)(slice<byte>)val);
                    switch (ᐧ) {
                    case {} when exprᴛ2 == "NaN"u8: {
                        va.Value.SetFloat(math.NaN());
                        return default;
                    }
                    case {} when exprᴛ2 == "Infinity"u8: {
                        va.Value.SetFloat(math.Inf(+1));
                        return default;
                    }
                    case {} when exprᴛ2 == "-Infinity"u8: {
                        va.Value.SetFloat(math.Inf(-1));
                        return default;
                    }}

                }
                if (!stringify) {
                    break;
                }
                if (uo.Flags.Get(jsonflags.StringifyWithLegacySemantics)) {
                    var (n, errΔ4) = strconv.ParseFloat((@string)(slice<byte>)val, bits);
                    if (errΔ4 != nil) {
                        if (((@string)(slice<byte>)val) == "null"u8) {
                            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                                va.Value.SetFloat(0D);
                            }
                            return default;
                        }
                        return newUnmarshalErrorAfterWithValue(dec, t, errors.Unwrap(errΔ4));
                    }
                    va.Value.SetFloat(n);
                    return default;
                }
                {
                    var (n, errΔ5) = jsonwire.ConsumeNumber(val); if (n != len(val) || errΔ5 != nil) {
                        return newUnmarshalErrorAfterWithValue(dec, t, strconv.ErrSyntax);
                    }
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1 && k == (jsontext.Kind)((rune)'0')) { matchᴛ1 = true;
            do {
                if (stringify && k == (rune)'0') {
                    break;
                }
                var (fv, errΔ6) = strconv.ParseFloat((@string)(slice<byte>)val, bits);
                va.Value.SetFloat(fv);
                if (errΔ6 != nil) {
                    return newUnmarshalErrorAfterWithValue(dec, t, errors.Unwrap(errΔ6));
                }
                return default;
            } while (false);
        }

        return newUnmarshalErrorAfter(dec, t, default);
    };
    return Ꮡfncs;
}

internal partial struct makeMapArshaler_member /*dyn*/ {
    internal @string name;
    internal addressableValue key;
    internal addressableValue val;
}

internal static ж<arshaler> makeMapArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    ref var once = ref heap(new sync.Once(), out var Ꮡonce);
    ref var keyFncs = ref heap<ж<arshaler>>(out var ᏑkeyFncs);
    ref var valFncs = ref heap<ж<arshaler>>(out var ᏑvalFncs);
    var init = () => {
        ᏑkeyFncs.ValueSlot = lookupArshaler(t.Key());
        ᏑvalFncs.ValueSlot = lookupArshaler(t.Elem());
    };
    var nillableLegacyKey = t.Key().Kind() == reflect.Pointer && implementsAny(t.Key(), textMarshalerType, textAppenderType);
    var initʗ1 = init;
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        GoFrame ᒐ = default;
        try {
            var xe = export.Encoder(enc);
            if (xe.Tokens.Depth() > startDetectingCyclesAfter) {
                var err = visitPointer(ref (xe.ᏑSeenPointers).DerefOrNull(), va.Value); if (err != nil) {
                    return newMarshalErrorBefore(enc, t, err);
                }
                defer((ᴛ1, ᴛ2) => leavePointer(ref ᴛ1.DerefOrNull(), ᴛ2), xe.ᏑSeenPointers, va.Value, ref ᒐ);
            }

            var emitNull = mo.Flags.Get(jsonflags.FormatNilMapAsNull);
            if (mo.Flags.Has(jsonflags.TagFlags)) {
                if (mo.Flags.Get(jsonflags.StringTag) && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return newMarshalErrorBefore(enc, t, errInvalidStringTag);
                }
                if (mo.Flags.Has(jsonflags.FormatTag)) {
                    var exprᴛ1 = mo.Format;
                    switch (ᐧ) {
                    case {} when exprᴛ1 == "emitnull"u8: {
                        emitNull = true;
                        break;
                    }
                    case {} when exprᴛ1 == "emitempty"u8: {
                        emitNull = false;
                        break;
                    }
                    default: {
                        return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
                    }}

                }
            }

            nint n = va.Value.Len();
            if (n == 0) {
                if (emitNull && va.Value.IsNil()) {
                    return enc.WriteToken(jsontext.Null);
                }
                if (optimizeCommon && !mo.Flags.Get(jsonflags.AnyWhitespace) && !xe.Tokens.Last.NeedObjectName()) {
                    xe.Buf = append(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'{'), ((@string)"{}").ꓸꓸꓸ);
                    xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
                    if (xe.NeedFlush()) {
                        return xe.Flush();
                    }
                    return default;
                }
            }

            Ꮡonce.Do(initʗ1);
            {
                var err = enc.WriteToken(jsontext.BeginObject); if (err != nil) {
                    return err;
                }
            }
            if (n > 0) {
                var nonDefaultKey = ᏑkeyFncs.ValueSlot.nonDefault;
                var marshalKey = ᏑkeyFncs.ValueSlot.marshal;
                var marshalVal = ᏑvalFncs.ValueSlot.marshal;
                if (mo.Marshalers != nil) {
                    bool ok = default;
                    (marshalKey, ok) = mo.Marshalers._<ж<Marshalers>>().lookup(marshalKey, t.Key());
                    (marshalVal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshalVal, t.Elem());
                    nonDefaultKey = nonDefaultKey || ok;
                }
                var k = newAddressableValue(t.Key());
                var v = newAddressableValue(t.Elem());

                if (!nonDefaultKey && mapKeyWithUniqueRepresentation(k.Value.Kind(), mo.Flags.Get(jsonflags.AllowInvalidUTF8))) {
                    xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.DisableNamespace();
                }

                switch (ᐧ) {
                case {} when !mo.Flags.Get(jsonflags.Deterministic) || n <= 1: {
                    for (var iter = va.Value.MapRange(); iter.Next(); ) {
                        k.Value.SetIterKey(iter);
                        var err = marshalKey(enc, k, mo);
                        if (err != nil) {
                            if (mo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && errors.Is(err, jsontext.ErrNonStringName) && nillableLegacyKey && k.Value.IsNil()) {
                                err = enc.WriteToken(jsontext.String(""));
                            }
                            if (err != nil) {
                                var (serr, ok) = err._<ж<jsontext.SyntacticError>>(ᐧ); if (ok && AreEqual(serr.Err, jsontext.ErrNonStringName)) {
                                    err = newMarshalErrorBefore(enc, k.Value.Type(), err);
                                }
                                return err;
                            }
                        }
                        v.Value.SetIterValue(iter);
                        var errΔ1 = marshalVal(enc, v, mo); if (errΔ1 != nil) {
                            return errΔ1;
                        }
                    }
                    break;
                }
                case {} when !nonDefaultKey && t.Key().Kind() == reflect.String: {
                    var names = getStrings(n);
                    for ((nint i, var iter) = (0, va.Value.MapRange()); i < n && iter.Next(); i++) {
                        k.Value.SetIterKey(iter);
                        (names.ValueSlot)[i] = k.Value.String();
                    }
                    slices.Sort<stringSlice, @string>(names.ValueSlot);
                    foreach (var (_, name) in range<@string>(names.ValueSlot)) {
                        {
                            var err = enc.WriteToken(jsontext.String(name)); if (err != nil) {
                                return err;
                            }
                        }
                        k.Value.SetString(name);
                        v.Value.Set(va.Value.MapIndex(k.Value));
                        {
                            var err = marshalVal(enc, v, mo); if (err != nil) {
                                return err;
                            }
                        }
                    }
                    putStrings(names);
                    break;
                }
                default: {
                    var members = new slice<makeMapArshaler_member>(n, () => new());
                    var keys = reflect.MakeSlice(reflect.SliceOf(t.Key()), n, n);
                    var vals = reflect.MakeSlice(reflect.SliceOf(t.Elem()), n, n);
                    for ((nint i, var iter) = (0, va.Value.MapRange()); i < n && iter.Next(); i++) {
                        var kΔ2 = new addressableValue(keys.Index(i), true);
                        kΔ2.Value.SetIterKey(iter);
                        var vΔ2 = new addressableValue(vals.Index(i), true);
                        vΔ2.Value.SetIterValue(iter);
                        var err = marshalKey(enc, kΔ2, mo);
                        if (err != nil) {
                            if (mo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && errors.Is(err, jsontext.ErrNonStringName) && nillableLegacyKey && kΔ2.Value.IsNil()) {
                                err = enc.WriteToken(jsontext.String(""));
                            }
                            if (err != nil) {
                                var (serr, ok) = err._<ж<jsontext.SyntacticError>>(ᐧ); if (ok && AreEqual(serr.Err, jsontext.ErrNonStringName)) {
                                    err = newMarshalErrorBefore(enc, kΔ2.Value.Type(), err);
                                }
                                return err;
                            }
                        }
                        @string name = xe.UnwriteOnlyObjectMemberName();
                        members[i] = new makeMapArshaler_member(name, kΔ2, vΔ2);
                    }
                    slices.SortFunc(members, (makeMapArshaler_member x, makeMapArshaler_member y) => strings.Compare(x.name, y.name));
                    foreach (var (_, member) in range(members)) {
                        {
                            var err = enc.WriteToken(jsontext.String(member.name)); if (err != nil) {
                                return err;
                            }
                        }
                        {
                            var err = marshalVal(enc, member.val, mo); if (err != nil) {
                                return err;
                            }
                        }
                    }
                    break;
                }}

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
    };
    var initʗ2 = init;
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        var xd = export.Decoder(dec);
        if (uo.Flags.Has(jsonflags.TagFlags)) {
            if (uo.Flags.Get(jsonflags.StringTag) && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                var exprᴛ2 = uo.Format;
                switch (ᐧ) {
                case {} when exprᴛ2 == "emitnull"u8 || exprᴛ2 == "emitempty"u8: {
                    break;
                }
                default: {
                    return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
                }}

            }
        }
        var (tok, err) = dec.ReadToken();
        if (err != nil) {
            return err;
        }
        var k = tok.Kind();
        switch (k) {
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
            va.Value.SetZero();
            return default;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'{'): {
            Ꮡonce.Do(initʗ2);
            if (va.Value.IsNil()) {
                va.Value.Set(reflect.MakeMap(t));
            }

            var nonDefaultKey = ᏑkeyFncs.ValueSlot.nonDefault;
            var unmarshalKey = ᏑkeyFncs.ValueSlot.unmarshal;
            var unmarshalVal = ᏑvalFncs.ValueSlot.unmarshal;
            if (uo.Unmarshalers != nil) {
                bool ok = default;
                (unmarshalKey, ok) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshalKey, t.Key());
                (unmarshalVal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshalVal, t.Elem());
                nonDefaultKey = nonDefaultKey || ok;
            }
            var kΔ2 = newAddressableValue(t.Key());
            var v = newAddressableValue(t.Elem());

            if (!nonDefaultKey && mapKeyWithUniqueRepresentation(kΔ2.Value.Kind(), uo.Flags.Get(jsonflags.AllowInvalidUTF8))) {
                xd.of(jsontext.decoderState.ᏑTokens).ᏑLast.DisableNamespace();
            }

            reflect.Value seen = new(nil);
            if (!uo.Flags.Get(jsonflags.AllowDuplicateNames) && va.Value.Len() > 0) {
                seen = reflect.MakeMap(reflect.MapOf(kΔ2.Value.Type(), emptyStructType));
            }

            error errUnmarshal = default;
            while (dec.PeekKind() != (rune)'}') {
                kΔ2.Value.SetZero();
                var errΔ5 = unmarshalKey(dec, kΔ2, uo);
                if (errΔ5 != nil) {
                    if (isFatalError(errΔ5, uo.Flags)) {
                        return errΔ5;
                    }
                    var errΔ6 = dec.SkipValue(); if (errΔ6 != nil) {
                        return errΔ6;
                    }
                    errUnmarshal = cmp.Or(errUnmarshal, errΔ5);
                    continue;
                }
                if (kΔ2.Value.Kind() == reflect.Interface && !kΔ2.Value.IsNil() && !kΔ2.Value.Elem().Type().Comparable()) {
                    var errΔ7 = newUnmarshalErrorAfter(dec, t, fmt.Errorf("invalid incomparable key type %v"u8, kΔ2.Value.Elem().Type()));
                    if (!uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return errΔ7;
                    }
                    var err2 = dec.SkipValue(); if (err2 != nil) {
                        return err2;
                    }
                    errUnmarshal = cmp.Or(errUnmarshal, errΔ7);
                    continue;
                }

                var v2 = va.Value.MapIndex(kΔ2.Value); if (v2.IsValid()) {
                    if (!uo.Flags.Get(jsonflags.AllowDuplicateNames) && (!seen.IsValid() || seen.MapIndex(kΔ2.Value).IsValid())) {
                        var name = xd.PreviousTokenOrValue();
                        return newDuplicateNameError(dec.StackPointer(), default, dec.InputOffset() - len64(name));
                    }
                    if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                        v.Value.Set(v2);
                    } else {
                        v.Value.SetZero();
                    }
                } else {
                    v.Value.SetZero();
                }

                errΔ5 = unmarshalVal(dec, v, uo);
                va.Value.SetMapIndex(kΔ2.Value, v.Value);
                if (seen.IsValid()) {
                    seen.SetMapIndex(kΔ2.Value, reflect.Zero(emptyStructType));
                }
                if (errΔ5 != nil) {
                    if (isFatalError(errΔ5, uo.Flags)) {
                        return errΔ5;
                    }
                    errUnmarshal = cmp.Or(errUnmarshal, errΔ5);
                }
            }
            var (_, errΔ8) = dec.ReadToken(); if (errΔ8 != nil) {
                return errΔ8;
            }
            return errUnmarshal;
        }}

        return newUnmarshalErrorAfterWithSkipping(dec, t, default);
    };
    return Ꮡfncs;
}

internal static bool mapKeyWithUniqueRepresentation(reflect.Kind k, bool allowInvalidUTF8) {
    switch (k) {
    case var ᴛ when ᴛ == reflect.Bool || ᴛ == reflect.Int || ᴛ == reflect.Int8 || ᴛ == reflect.Int16 || ᴛ == reflect.Int32 || ᴛ == reflect.Int64 || ᴛ == reflect.Uint || ᴛ == reflect.Uint8 || ᴛ == reflect.Uint16 || ᴛ == reflect.Uint32 || ᴛ == reflect.Uint64 || ᴛ == reflect.Uintptr: {
        return true;
    }
    case var ᴛ when ᴛ == reflect.String: {
        return !allowInvalidUTF8;
    }
    default: {
        return false;
    }}

}

internal static error errNilField = errors.New("cannot set embedded pointer to unexported struct type");
internal static error errInvalidStringTag = errors.New("invalid use of `string` tag option");

internal static ж<arshaler> makeStructArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    ref var once = ref heap(new sync.Once(), out var Ꮡonce);
    ref var fields = ref heap(new structFields(), out var Ꮡfields);
    ref var errInit = ref heap<ж<SemanticError>>(out var ᏑerrInit);
    var init = () => {
        (Ꮡfields.Value, ᏑerrInit.ValueSlot) = makeStructFields(t);
    };
    var initʗ1 = init;
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        var xe = export.Encoder(enc);
        if (mo.Flags.Has(jsonflags.TagFlags)) {
            if (mo.Flags.Get(jsonflags.StringTag) && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newMarshalErrorBefore(enc, t, errInvalidStringTag);
            }
            if (mo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
            }
        }
        Ꮡonce.Do(initʗ1);
        if (ᏑerrInit.ValueSlot != nil && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            return newMarshalErrorBefore(enc, ᏑerrInit.ValueSlot.ΔGoType, ᏑerrInit.ValueSlot.Err);
        } else if (Ꮡfields.Value.errUnsupportedFormat != nil && !mo.Flags.Get(jsonflags.FormatTagSupported)) {
            return newMarshalErrorBefore(enc, Ꮡfields.Value.errUnsupportedFormat.ΔGoType, Ꮡfields.Value.errUnsupportedFormat.Err);
        }

        {
            var err = enc.WriteToken(jsontext.BeginObject); if (err != nil) {
                return err;
            }
        }
        uintSet seenIdxs = default;
        nint prevIdx = -1;
        xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.DisableNamespace();
        foreach (var i in range(len(Ꮡfields.Value.flattened))) {
            var f = Ꮡ(Ꮡfields.Value.flattened, i);
            var v = new addressableValue(va.Value.Field(f.index0), va.forcedAddr);
            if (len(f.index) > 0) {
                v = v.fieldByIndex(f.index, false);
                if (!v.Value.IsValid()) {
                    continue;
                }
            }

            if ((f.omitzero || mo.Flags.Get(jsonflags.OmitZeroStructFields)) && ((f.isZero == nil && v.Value.IsZero()) || (f.isZero != nil && f.isZero(v)))) {
                continue;
            }

            if (f.omitempty && mo.Flags.Get(jsonflags.OmitEmptyWithLegacySemantics) && isLegacyEmpty(v)) {
                continue;
            }

            var marshal = f.fncs.marshal;
            var nonDefault = f.fncs.nonDefault;
            if (mo.Marshalers != nil) {
                bool ok = default;
                (marshal, ok) = mo.Marshalers._<ж<Marshalers>>().lookup(marshal, f.typ);
                nonDefault = nonDefault || ok;
            }

            if (f.omitempty && !mo.Flags.Get(jsonflags.OmitEmptyWithLegacySemantics) && !nonDefault && f.isEmpty != nil && f.isEmpty(v)) {
                continue;
            }

            if (optimizeCommon) {
                var b = xe.Buf;
                if (xe.Tokens.Last.Length() > 0) {
                    b = append(b, (byte)',');
                    if (mo.Flags.Get(jsonflags.SpaceAfterComma)) {
                        b = append(b, (byte)' ');
                    }
                }
                if (mo.Flags.Get(jsonflags.Multiline)) {
                    b = xe.AppendIndent(b, xe.Tokens.NeedIndent((rune)'"'));
                }

                nint n0 = len(b);
                if (!f.nameNeedEscape) {
                    b = append(b, f.quotedName.ꓸꓸꓸ);
                } else {
                    (b, _) = jsonwire.AppendQuote(b, slice<byte>(f.name), mo.ᏑFlags);
                }
                xe.Buf = b;
                xe.of(jsontext.encoderState.ᏑNames).ReplaceLastQuotedOffset(n0);
                xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
            } else {
                var errΔ1 = enc.WriteToken(jsontext.String(f.name)); if (errΔ1 != nil) {
                    return errΔ1;
                }
            }

            var flagsOriginal = mo.Flags;
            if (f.@string) {
                mo.ᏑFlags.Set((jsonflags.Bools)(jsonflags.StringTag | 1));
            }
            if (f.format != ""u8) {
                mo.ᏑFlags.Set((jsonflags.Bools)(jsonflags.FormatTag | 1));
                mo.Format = f.format;
            }
            var err = marshal(enc, v, mo);
            mo.Flags = flagsOriginal;
            mo.Format = "";
            if (err != nil) {
                return err;
            }

            if (f.omitempty && !mo.Flags.Get(jsonflags.OmitEmptyWithLegacySemantics)) {
                ж<@string> prevName = default;
                if (prevIdx >= 0) {
                    prevName = Ꮡ(Ꮡfields.Value.flattened, prevIdx).of(structField.Ꮡname);
                }
                if (xe.UnwriteEmptyObjectMember(prevName)) {
                    continue;
                }
            }

            if (!mo.Flags.Get(jsonflags.AllowDuplicateNames) && Ꮡfields.Value.embeddedFallback != nil) {
                seenIdxs.insert((nuint)f.id);
            }
            prevIdx = f.id;
        }
        if (Ꮡfields.Value.embeddedFallback != nil) {
            Func<slice<byte>, bool> insertUnquotedName = default;
            if (!mo.Flags.Get(jsonflags.AllowDuplicateNames)) {
                var xeʗ1 = xe;
                insertUnquotedName = (slice<byte> name) => {
                    var foldedFields = Ꮡfields.Value.lookupByFoldedName(name); if (len(foldedFields) > 0) {
                        {
                            var f = Ꮡfields.Value.byActualName[tmpstring(name)]; if (f != nil) {
                                return seenIdxs.insert((nuint)f.id);
                            }
                        }
                        foreach (var (_, f) in range(foldedFields)) {
                            if (f.matchFoldedName(name, mo.ᏑFlags)) {
                                return seenIdxs.insert((nuint)f.id);
                            }
                        }
                    }

                    return xeʗ1.Namespaces.Last().InsertUnquoted(name);
                };
            }
            var err = marshalEmbeddedFallbackAll(enc, va, mo, ref (Ꮡfields.Value.embeddedFallback).DerefOrNull(), insertUnquotedName); if (err != nil) {
                return err;
            }
        }
        {
            var err = enc.WriteToken(jsontext.EndObject); if (err != nil) {
                return err;
            }
        }
        return default;
    };
    var initʗ2 = init;
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        var xd = export.Decoder(dec);
        if (uo.Flags.Has(jsonflags.TagFlags)) {
            if (uo.Flags.Get(jsonflags.StringTag) && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
            }
        }
        var (tok, err) = dec.ReadToken();
        if (err != nil) {
            return err;
        }
        var k = tok.Kind();
        switch (k) {
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                va.Value.SetZero();
            }
            return default;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'{'): {
            Ꮡonce.Do(initʗ2);
            if (ᏑerrInit.ValueSlot != nil && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorAfter(dec, ᏑerrInit.ValueSlot.ΔGoType, ᏑerrInit.ValueSlot.Err);
            } else if (Ꮡfields.Value.errUnsupportedFormat != nil && !uo.Flags.Get(jsonflags.FormatTagSupported)) {
                return newUnmarshalErrorAfter(dec, Ꮡfields.Value.errUnsupportedFormat.ΔGoType, Ꮡfields.Value.errUnsupportedFormat.Err);
            }

            uintSet seenIdxs = default;
            xd.of(jsontext.decoderState.ᏑTokens).ᏑLast.DisableNamespace();
            error errUnmarshal = default;
            while (dec.PeekKind() != (rune)'}') {
                ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
                var (val, errΔ7) = xd.ReadValue(Ꮡflags);
                if (errΔ7 != nil) {
                    return errΔ7;
                }
                var name = jsonwire.UnquoteMayCopy(val, flags.IsVerbatim());
                var f = Ꮡfields.Value.byActualName[tmpstring(name)];
                if (f == nil) {
                    nint numMatch = default;
                    foreach (var (_, f2) in range(Ꮡfields.Value.lookupByFoldedName(name))) {
                        if (f2.matchFoldedName(name, uo.ᏑFlags)) {
                            if (f == nil) {
                                f = f2;
                            }
                            numMatch++;
                        }
                    }
                    if (numMatch > 1 && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                        return newUnmarshalErrorAfter(dec, t, errAmbiguousName);
                    }
                    if (f == nil) {
                        if (uo.Flags.Get(jsonflags.RejectUnknownMembers) && Ꮡfields.Value.embeddedFallback == nil) {
                            var errΔ8 = newUnmarshalErrorAfter(dec, t, ErrUnknownName);
                            if (!uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                                return errΔ8;
                            }
                            errUnmarshal = cmp.Or(errUnmarshal, errΔ8);
                        }
                        if (!uo.Flags.Get(jsonflags.AllowDuplicateNames) && !xd.Namespaces.Last().InsertUnquoted(name)) {
                            return newDuplicateNameError(dec.StackPointer(), default, dec.InputOffset() - len64(val));
                        }

                        if (Ꮡfields.Value.embeddedFallback == nil) {
                            var errΔ9 = dec.SkipValue(); if (errΔ9 != nil) {
                                return errΔ9;
                            }
                        } else {
                            var errΔ10 = unmarshalEmbeddedFallbackNext(dec, va, uo, ref (Ꮡfields.Value.embeddedFallback).DerefOrNull(), val, name); if (errΔ10 != nil) {
                                if (isFatalError(errΔ10, uo.Flags)) {
                                    return errΔ10;
                                }
                                errUnmarshal = cmp.Or(errUnmarshal, errΔ10);
                            }
                        }
                        continue;
                    }
                }
                if (!uo.Flags.Get(jsonflags.AllowDuplicateNames) && !seenIdxs.insert((nuint)f.id)) {
                    return newDuplicateNameError(dec.StackPointer(), default, dec.InputOffset() - len64(val));
                }

                var unmarshal = f.fncs.unmarshal;
                if (uo.Unmarshalers != nil) {
                    (unmarshal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshal, f.typ);
                }
                var flagsOriginal = uo.Flags;
                if (f.@string) {
                    uo.ᏑFlags.Set((jsonflags.Bools)(jsonflags.StringTag | 1));
                }
                if (f.format != ""u8) {
                    uo.ᏑFlags.Set((jsonflags.Bools)(jsonflags.FormatTag | 1));
                    uo.Format = f.format;
                }
                var v = new addressableValue(va.Value.Field(f.index0), va.forcedAddr);
                if (len(f.index) > 0) {
                    v = v.fieldByIndex(f.index, true);
                    if (!v.Value.IsValid()) {
                        var errΔ11 = newUnmarshalErrorBefore(dec, t, errNilField);
                        if (!uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                            return errΔ11;
                        }
                        errUnmarshal = cmp.Or(errUnmarshal, errΔ11);
                        unmarshal = (ж<jsontext.Decoder> decΔ2, addressableValue _Δp1, ж<jsonopts.Struct> _Δp2) => decΔ2.SkipValue();
                    }
                }
                errΔ7 = unmarshal(dec, v, uo);
                uo.Flags = flagsOriginal;
                uo.Format = "";
                if (errΔ7 != nil) {
                    if (isFatalError(errΔ7, uo.Flags)) {
                        return errΔ7;
                    }
                    errUnmarshal = cmp.Or(errUnmarshal, errΔ7);
                }
            }
            var (_, errΔ12) = dec.ReadToken(); if (errΔ12 != nil) {
                return errΔ12;
            }
            return errUnmarshal;
        }}

        return newUnmarshalErrorAfterWithSkipping(dec, t, default);
    };
    return Ꮡfncs;
}

} partial class jsonᴛaddressableValue {

internal static addressableValue fieldByIndex(this addressableValue va, slice<nint> index, bool mayAlloc) {
    foreach (var (_, i) in range(index)) {
        va = va.indirect(mayAlloc);
        if (!va.Value.IsValid()) {
            return va;
        }
        va = new addressableValue(va.Value.Field(i), va.forcedAddr);
    }
    return va;
}

internal static addressableValue indirect(this addressableValue va, bool mayAlloc) {
    if (va.Value.Kind() == reflect.Pointer) {
        if (va.Value.IsNil()) {
            if (!mayAlloc || !va.Value.CanSet()) {
                return new addressableValue();
            }
            va.Value.Set(reflect.New(va.Value.Type().Elem()));
        }
        va = new addressableValue(va.Value.Elem(), false);
    }
    return va;
}

} partial class json_package {

internal static bool isLegacyEmpty(addressableValue v) {
    switch (v.Value.Kind()) {
    case var ᴛ when ᴛ == reflect.Bool: {
        return v.Value.Bool() == false;
    }
    case var ᴛ when ᴛ == reflect.Int || ᴛ == reflect.Int8 || ᴛ == reflect.Int16 || ᴛ == reflect.Int32 || ᴛ == reflect.Int64: {
        return v.Value.Int() == 0;
    }
    case var ᴛ when ᴛ == reflect.Uint || ᴛ == reflect.Uint8 || ᴛ == reflect.Uint16 || ᴛ == reflect.Uint32 || ᴛ == reflect.Uint64 || ᴛ == reflect.Uintptr: {
        return v.Value.Uint() == 0;
    }
    case var ᴛ when ᴛ == reflect.Float32 || ᴛ == reflect.Float64: {
        return v.Value.Float() == 0D;
    }
    case var ᴛ when ᴛ == reflect.String || ᴛ == reflect.Map || ᴛ == reflect.Slice || ᴛ == reflect.Array: {
        return v.Value.Len() == 0;
    }
    case var ᴛ when ᴛ == reflect.Pointer || ᴛ == reflect.Interface: {
        return v.Value.IsNil();
    }}

    return false;
}

internal static ж<arshaler> makeSliceArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    ref var once = ref heap(new sync.Once(), out var Ꮡonce);
    ref var valFncs = ref heap<ж<arshaler>>(out var ᏑvalFncs);
    var init = () => {
        ᏑvalFncs.ValueSlot = lookupArshaler(t.Elem());
    };
    var initʗ1 = init;
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        GoFrame ᒐ = default;
        try {
            var xe = export.Encoder(enc);
            if (xe.Tokens.Depth() > startDetectingCyclesAfter) {
                var err = visitPointer(ref (xe.ᏑSeenPointers).DerefOrNull(), va.Value); if (err != nil) {
                    return newMarshalErrorBefore(enc, t, err);
                }
                defer((ᴛ1, ᴛ2) => leavePointer(ref ᴛ1.DerefOrNull(), ᴛ2), xe.ᏑSeenPointers, va.Value, ref ᒐ);
            }

            var emitNull = mo.Flags.Get(jsonflags.FormatNilSliceAsNull);
            if (mo.Flags.Has(jsonflags.TagFlags)) {
                if (mo.Flags.Get(jsonflags.StringTag) && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return newMarshalErrorBefore(enc, t, errInvalidStringTag);
                }
                if (mo.Flags.Has(jsonflags.FormatTag)) {
                    var exprᴛ1 = mo.Format;
                    switch (ᐧ) {
                    case {} when exprᴛ1 == "emitnull"u8: {
                        emitNull = true;
                        break;
                    }
                    case {} when exprᴛ1 == "emitempty"u8: {
                        emitNull = false;
                        break;
                    }
                    default: {
                        return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
                    }}

                }
            }

            nint n = va.Value.Len();
            if (n == 0) {
                if (emitNull && va.Value.IsNil()) {
                    return enc.WriteToken(jsontext.Null);
                }
                if (optimizeCommon && !mo.Flags.Get(jsonflags.AnyWhitespace) && !xe.Tokens.Last.NeedObjectName()) {
                    xe.Buf = append(xe.Tokens.MayAppendDelim(xe.Buf, (rune)'['), ((@string)"[]").ꓸꓸꓸ);
                    xe.of(jsontext.encoderState.ᏑTokens).ᏑLast.Increment();
                    if (xe.NeedFlush()) {
                        return xe.Flush();
                    }
                    return default;
                }
            }

            Ꮡonce.Do(initʗ1);
            {
                var err = enc.WriteToken(jsontext.BeginArray); if (err != nil) {
                    return err;
                }
            }
            var marshal = ᏑvalFncs.ValueSlot.marshal;
            if (mo.Marshalers != nil) {
                (marshal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshal, t.Elem());
            }
            foreach (var i in range(n)) {
                var v = new addressableValue(va.Value.Index(i), false);
                var err = marshal(enc, v, mo); if (err != nil) {
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
    };
    ref var emptySlice = ref heap<reflect.Value>(reflect.MakeSlice(t, 0, 0), out var ᏑemptySlice);
    var emptySliceʗ1 = emptySlice;
    var initʗ2 = init;
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        if (uo.Flags.Has(jsonflags.TagFlags)) {
            if (uo.Flags.Get(jsonflags.StringTag) && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                var exprᴛ2 = uo.Format;
                switch (ᐧ) {
                case {} when exprᴛ2 == "emitnull"u8 || exprᴛ2 == "emitempty"u8: {
                    break;
                }
                default: {
                    return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
                }}

            }
        }

        var (tok, err) = dec.ReadToken();
        if (err != nil) {
            return err;
        }
        var k = tok.Kind();
        switch (k) {
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
            va.Value.SetZero();
            return default;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'['): {
            Ꮡonce.Do(initʗ2);
            var unmarshal = ᏑvalFncs.ValueSlot.unmarshal;
            if (uo.Unmarshalers != nil) {
                (unmarshal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshal, t.Elem());
            }
            var mustZero = true;
            nint cap = va.Value.Cap();
            if (cap > 0) {
                va.Value.SetLen(cap);
            }
            nint i = default;
            error errUnmarshal = default;
            while (dec.PeekKind() != (rune)']') {
                if (i == cap) {
                    va.Value.Grow(1);
                    cap = va.Value.Cap();
                    va.Value.SetLen(cap);
                    mustZero = false;
                }
                var v = new addressableValue(va.Value.Index(i), false);
                i++;
                if (mustZero && !uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                    v.Value.SetZero();
                }
                var errΔ3 = unmarshal(dec, v, uo); if (errΔ3 != nil) {
                    if (isFatalError(errΔ3, uo.Flags)) {
                        va.Value.SetLen(i);
                        return errΔ3;
                    }
                    errUnmarshal = cmp.Or(errUnmarshal, errΔ3);
                }
            }
            if (i == 0) {
                va.Value.Set(emptySliceʗ1);
            } else {
                va.Value.SetLen(i);
            }
            var (_, errΔ4) = dec.ReadToken(); if (errΔ4 != nil) {
                return errΔ4;
            }
            return errUnmarshal;
        }}

        return newUnmarshalErrorAfterWithSkipping(dec, t, default);
    };
    return Ꮡfncs;
}

internal static error errArrayUnderflow = errors.New("too few array elements");
internal static error errArrayOverflow = errors.New("too many array elements");

internal static ж<arshaler> makeArrayArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    ref var once = ref heap(new sync.Once(), out var Ꮡonce);
    ref var valFncs = ref heap<ж<arshaler>>(out var ᏑvalFncs);
    var init = () => {
        ᏑvalFncs.ValueSlot = lookupArshaler(t.Elem());
    };
    nint n = t.Len();
    var initʗ1 = init;
    fncs.marshal = error (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        if (mo.Flags.Has(jsonflags.TagFlags)) {
            if (mo.Flags.Get(jsonflags.StringTag) && !mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newMarshalErrorBefore(enc, t, errInvalidStringTag);
            }
            if (mo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
            }
        }
        Ꮡonce.Do(initʗ1);
        {
            var err = enc.WriteToken(jsontext.BeginArray); if (err != nil) {
                return err;
            }
        }
        var marshal = ᏑvalFncs.ValueSlot.marshal;
        if (mo.Marshalers != nil) {
            (marshal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshal, t.Elem());
        }
        foreach (var i in range(n)) {
            var v = new addressableValue(va.Value.Index(i), va.forcedAddr);
            var err = marshal(enc, v, mo); if (err != nil) {
                return err;
            }
        }
        {
            var err = enc.WriteToken(jsontext.EndArray); if (err != nil) {
                return err;
            }
        }
        return default;
    };
    var initʗ2 = init;
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        if (uo.Flags.Has(jsonflags.TagFlags)) {
            if (uo.Flags.Get(jsonflags.StringTag) && !uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
            }
        }
        var (tok, err) = dec.ReadToken();
        if (err != nil) {
            return err;
        }
        var k = tok.Kind();
        switch (k) {
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
            if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                va.Value.SetZero();
            }
            return default;
        }
        case var ᴛ when ᴛ == (jsontext.Kind)((rune)'['): {
            Ꮡonce.Do(initʗ2);
            var unmarshal = ᏑvalFncs.ValueSlot.unmarshal;
            if (uo.Unmarshalers != nil) {
                (unmarshal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshal, t.Elem());
            }
            nint i = default;
            error errUnmarshal = default;
            while (dec.PeekKind() != (rune)']') {
                if (i >= n) {
                    var errΔ4 = dec.SkipValue(); if (errΔ4 != nil) {
                        return errΔ4;
                    }
                    err = errArrayOverflow;
                    continue;
                }
                var v = new addressableValue(va.Value.Index(i), va.forcedAddr);
                if (!uo.Flags.Get(jsonflags.MergeWithLegacySemantics)) {
                    v.Value.SetZero();
                }
                var errΔ5 = unmarshal(dec, v, uo); if (errΔ5 != nil) {
                    if (isFatalError(errΔ5, uo.Flags)) {
                        return errΔ5;
                    }
                    errUnmarshal = cmp.Or(errUnmarshal, errΔ5);
                }
                i++;
            }
            for (; i < n; i++) {
                va.Value.Index(i).SetZero();
                err = errArrayUnderflow;
            }
            var (_, errΔ6) = dec.ReadToken(); if (errΔ6 != nil) {
                return errΔ6;
            }
            if (err != nil && !uo.Flags.Get(jsonflags.UnmarshalArrayFromAnyLength)) {
                return newUnmarshalErrorAfter(dec, t, err);
            }
            return errUnmarshal;
        }}

        return newUnmarshalErrorAfterWithSkipping(dec, t, default);
    };
    return Ꮡfncs;
}

internal static ж<arshaler> makePointerArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    ref var once = ref heap(new sync.Once(), out var Ꮡonce);
    ref var valFncs = ref heap<ж<arshaler>>(out var ᏑvalFncs);
    var init = () => {
        ᏑvalFncs.ValueSlot = lookupArshaler(t.Elem());
    };
    var initʗ1 = init;
    fncs.marshal = (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        GoFrame ᒐ = default;
        try {
            var xe = export.Encoder(enc);
            if (xe.Tokens.Depth() > startDetectingCyclesAfter) {
                var err = visitPointer(ref (xe.ᏑSeenPointers).DerefOrNull(), va.Value); if (err != nil) {
                    return newMarshalErrorBefore(enc, t, err);
                }
                defer((ᴛ1, ᴛ2) => leavePointer(ref ᴛ1.DerefOrNull(), ᴛ2), xe.ᏑSeenPointers, va.Value, ref ᒐ);
            }

            if (mo.Flags.Get(jsonflags.StringTag) && mo.Flags.Get(jsonflags.StringifyWithLegacySemantics) && t.Elem().Kind() == reflect.Pointer) {
                if (!mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return newMarshalErrorBefore(enc, t, errInvalidStringTag);
                }
                mo.ᏑFlags.Clear(jsonflags.StringTag);
            }
            if (va.Value.IsNil()) {
                return enc.WriteToken(jsontext.Null);
            }
            Ꮡonce.Do(initʗ1);
            var marshal = ᏑvalFncs.ValueSlot.marshal;
            if (mo.Marshalers != nil) {
                (marshal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshal, t.Elem());
            }
            var v = new addressableValue(va.Value.Elem(), false);
            return marshal(enc, v, mo);
        }
        catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
        finally { ᒐ.Run(); }
    };
    var initʗ2 = init;
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        if (dec.PeekKind() == (rune)'n') {
            var (_, err) = dec.ReadToken(); if (err != nil) {
                return err;
            }
            va.Value.SetZero();
            return default;
        }
        if (uo.Flags.Get(jsonflags.StringTag) && uo.Flags.Get(jsonflags.StringifyWithLegacySemantics) && t.Elem().Kind() == reflect.Pointer) {
            if (!uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
            }
            uo.ᏑFlags.Clear(jsonflags.StringTag);
        }
        Ꮡonce.Do(initʗ2);
        var unmarshal = ᏑvalFncs.ValueSlot.unmarshal;
        if (uo.Unmarshalers != nil) {
            (unmarshal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshal, t.Elem());
        }
        if (va.Value.IsNil()) {
            va.Value.Set(reflect.New(t.Elem()));
        }
        var v = new addressableValue(va.Value.Elem(), false);
        {
            var err = unmarshal(dec, v, uo); if (err != nil) {
                return err;
            }
        }
        if (uo.Flags.Get(jsonflags.StringTag) && uo.Flags.Get(jsonflags.StringifyWithLegacySemantics)) {
            if (((sstring)export.Decoder(dec).PreviousTokenOrValue()) == @"""null"""u8) {
                va.Value.SetZero();
            }
        }
        return default;
    };
    return Ꮡfncs;
}

internal partial struct makeInterfaceArshaler_i /*dyn*/ {
    /*embed*/ public MarshalerTo MarshalerTo;
}

internal partial struct makeInterfaceArshaler_iᴛ1 /*dyn*/ {
    /*embed*/ public Marshaler Marshaler;
}

internal partial struct makeInterfaceArshaler_iᴛ2 /*dyn*/ {
    /*embed*/ public encoding_package.TextAppender TextAppender;
}

internal partial struct makeInterfaceArshaler_iᴛ3 /*dyn*/ {
    /*embed*/ public encoding_package.TextMarshaler TextMarshaler;
}

internal static ж<arshaler> makeInterfaceArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    reflect.Type whichMarshaler = default;
    foreach (var (_, iface) in range(allMarshalerTypes)) {
        if (t.Implements(iface)) {
            whichMarshaler = t;
            break;
        }
    }
    var whichMarshalerʗ1 = whichMarshaler;
    fncs.marshal = (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => {
        if (mo.Flags.Has(jsonflags.TagFlags)) {
            if (mo.Flags.Get(jsonflags.StringTag)) {
                if (!mo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return newMarshalErrorBefore(enc, t, errInvalidStringTag);
                }
                if (mo.Flags.Get(jsonflags.StringifyWithLegacySemantics)) {
                    mo.ᏑFlags.Clear(jsonflags.StringTag);
                }
            }
            if (mo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Encoderжcoder(enc), t);
            }
        }
        if (va.Value.IsNil()) {
            return enc.WriteToken(jsontext.Null);
        } else if (mo.Flags.Get(jsonflags.CallMethodsWithLegacySemantics) && whichMarshalerʗ1 != nil) {
            if (va.Value.Elem().Kind() == reflect.Pointer && va.Value.Elem().IsNil()) {
                var v2 = newAddressableValue(whichMarshalerʗ1);
                switch (whichMarshalerʗ1) {
                case var ᴛ when AreEqual(ᴛ, jsonMarshalerToType): {
                    v2.Value.Set(reflect.ValueOf(new makeInterfaceArshaler_i(va.Value.Elem().Interface()._<MarshalerTo>())));
                    break;
                }
                case var ᴛ when AreEqual(ᴛ, jsonMarshalerType): {
                    v2.Value.Set(reflect.ValueOf(new makeInterfaceArshaler_iᴛ1(va.Value.Elem().Interface()._<Marshaler>())));
                    break;
                }
                case var ᴛ when AreEqual(ᴛ, textAppenderType): {
                    v2.Value.Set(reflect.ValueOf(new makeInterfaceArshaler_iᴛ2(va.Value.Elem().Interface()._<encoding.TextAppender>())));
                    break;
                }
                case var ᴛ when AreEqual(ᴛ, textMarshalerType): {
                    v2.Value.Set(reflect.ValueOf(new makeInterfaceArshaler_iᴛ3(va.Value.Elem().Interface()._<encoding.TextMarshaler>())));
                    break;
                }}

                va = v2;
            }
        }
        var v = newAddressableValue(va.Value.Elem().Type());
        v.Value.Set(va.Value.Elem());
        var marshal = lookupArshaler(v.Value.Type()).marshal;
        if (mo.Marshalers != nil) {
            (marshal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshal, v.Value.Type());
        }
        if (optimizeCommon && AreEqual(t, anyType) && !mo.Flags.Get((jsonflags.Bools)(jsonflags.StringifyNumbers | jsonflags.TagFlags)) && (mo.Marshalers == nil || !mo.Marshalers._<ж<Marshalers>>().fromAny)) {
            return marshalValueAny(enc, va.Value.Elem().Interface(), mo);
        }
        return marshal(enc, v, mo);
    };
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        if (uo.Flags.Has(jsonflags.TagFlags)) {
            if (uo.Flags.Get(jsonflags.StringTag)) {
                if (!uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
                    return newUnmarshalErrorBeforeWithSkipping(dec, t, errInvalidStringTag);
                }
                if (uo.Flags.Get(jsonflags.StringifyWithLegacySemantics)) {
                    uo.ᏑFlags.Clear(jsonflags.StringTag);
                }
            }
            if (uo.Flags.Has(jsonflags.FormatTag)) {
                return newInvalidFormatError(new jsontext_Decoderжcoder(dec), t);
            }
        }
        if (uo.Flags.Get(jsonflags.MergeWithLegacySemantics) && !va.Value.IsNil()) {
            var e = va.Value.Elem();
            if (e.Kind() == reflect.Pointer && !e.IsNil()) {
                if (dec.PeekKind() == (rune)'n' && e.Elem().Kind() == reflect.Pointer) {
                    var (_, errΔ1) = dec.ReadToken(); if (errΔ1 != nil) {
                        return errΔ1;
                    }
                    va.Value.Elem().Elem().SetZero();
                    return default;
                }
            } else {
                va.Value.SetZero();
            }
        }
        if (dec.PeekKind() == (rune)'n') {
            var (_, errΔ2) = dec.ReadToken(); if (errΔ2 != nil) {
                return errΔ2;
            }
            va.Value.SetZero();
            return default;
        }
        addressableValue v = new(nil);
        if (va.Value.IsNil()) {
            if (optimizeCommon && AreEqual(t, anyType) && !uo.Flags.Get((jsonflags.Bools)(jsonflags.AllowDuplicateNames | jsonflags.FormatTag)) && (uo.Unmarshalers == nil || !uo.Unmarshalers._<ж<Unmarshalers>>().fromAny)) {
                var (vΔ1, errΔ3) = unmarshalValueAny(dec, ref (uo).DerefOrNull());
                if (vΔ1 != nil) {
                    va.Value.Set(reflect.ValueOf(vΔ1));
                }
                return errΔ3;
            }

            var k = dec.PeekKind();
            if (!isAnyType(t)) {
                return newUnmarshalErrorBeforeWithSkipping(dec, t, @internal.ErrNilInterface);
            }
            switch (k) {
            case var ᴛ when ᴛ == (jsontext.Kind)((rune)'f') || ᴛ == (jsontext.Kind)((rune)'t'): {
                v = newAddressableValue(boolType);
                break;
            }
            case var ᴛ when ᴛ == (jsontext.Kind)((rune)'"'): {
                v = newAddressableValue(stringType);
                break;
            }
            case var ᴛ when ᴛ == (jsontext.Kind)((rune)'0'): {
                if (uo.Flags.Get(jsonflags.UnmarshalAnyWithRawNumber)) {
                    v = new addressableValue(reflect.ValueOf(@internal.NewRawNumber()).Elem(), true);
                } else {
                    v = newAddressableValue(float64Type);
                }
                break;
            }
            case var ᴛ when ᴛ == (jsontext.Kind)((rune)'{'): {
                v = newAddressableValue(mapStringAnyType);
                break;
            }
            case var ᴛ when ᴛ == (jsontext.Kind)((rune)'['): {
                v = newAddressableValue(sliceAnyType);
                break;
            }
            default: {
                var (_, errΔ5) = dec.ReadValue();
                return errΔ5;
            }}

        } else {
            v = newAddressableValue(va.Value.Elem().Type());
            v.Value.Set(va.Value.Elem());
        }
        var unmarshal = lookupArshaler(v.Value.Type()).unmarshal;
        if (uo.Unmarshalers != nil) {
            (unmarshal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshal, v.Value.Type());
        }
        var err = unmarshal(dec, v, uo);
        va.Value.Set(v.Value);
        return err;
    };
    return Ꮡfncs;
}

internal static bool isAnyType(reflect.Type t) {
    return AreEqual(t, anyType) || anyType.Implements(t);
}

internal static ж<arshaler> makeInvalidArshaler(reflect.Type t) {
    ref var fncs = ref heap(new arshaler(), out var Ꮡfncs);
    fncs.marshal = (ж<jsontext.Encoder> enc, addressableValue va, ж<jsonopts.Struct> mo) => newMarshalErrorBefore(enc, t, default);
    fncs.unmarshal = error (ж<jsontext.Decoder> dec, addressableValue va, ж<jsonopts.Struct> uo) => {
        if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            var (val, err) = dec.ReadValue();
            switch (ᐧ) {
            case {} when err != nil: {
                return err;
            }
            case {} when val.Kind() == (rune)'n': {
                return default;
            }
            default: {
                return newUnmarshalErrorAfter(dec, t, default);
            }}

        }
        return newUnmarshalErrorBefore(dec, t, default);
    };
    return Ꮡfncs;
}

internal static jsontext.Kind stringOrNumberKind(bool isString) {
    if (isString) {
        return (rune)'"';
    } else {
        return (rune)'0';
    }
}

partial struct uintSet64 /*num:uint64*/;

} partial class jsonᴛuintSet64 {

internal static bool has(this uintSet64 s, nuint i) { return (uintSet64)(s & ((uintSet64)((uint64)1 << (int)(i)))) > 0; }
internal static void set(this ref uintSet64 s, nuint i) { s |= (uintSet64)((uintSet64)((uint64)1 << (int)(i))); }

} partial class json_package {

partial struct uintSet {
    internal uintSet64 lo;
    internal slice<uintSet64> hi;
}

} partial class jsonᴛuintSet {

internal static bool has(this ref uintSet s, nuint i) {
    if (i < 64) {
        return s.lo.has(i);
    } else {
        i -= 64;
        nint iHi = (nint)(i / 64);
        nuint iLo = i % 64;
        return iHi < len(s.hi) && s.hi[iHi].has(iLo);
    }
}

internal static bool insert(this ref uintSet s, nuint i) {
    if (i < 64) {
        var has = s.lo.has(i);
        s.lo.set(i);
        return !has;
    } else {
        i -= 64;
        nint iHi = (nint)(i / 64);
        nuint iLo = i % 64;
        if (iHi >= len(s.hi)) {
            s.hi = appendꓸꓸꓸ(s.hi, makeꓸꓸꓸ<uintSet64>(iHi + 1 - len(s.hi)));
            s.hi = s.hi.slice(0, cap(s.hi));
        }
        var has = s.hi[iHi].has(iLo);
        s.hi[iHi].set(iLo);
        return !has;
    }
}

} // end jsonᴛuintSet
