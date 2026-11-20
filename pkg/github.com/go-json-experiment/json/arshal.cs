// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

global using marshaler = System.Func<go.ж<go.github.com.go_json_experiment.json.jsontext_package.Encoder>, go.github.com.go_json_experiment.json_package.addressableValue, go.ж<go.github.com.go_json_experiment.json.@internal.jsonopts_package.Struct>, go.error>;
global using unmarshaler = System.Func<go.ж<go.github.com.go_json_experiment.json.jsontext_package.Decoder>, go.github.com.go_json_experiment.json_package.addressableValue, go.ж<go.github.com.go_json_experiment.json.@internal.jsonopts_package.Struct>, go.error>;

namespace go.github.com.go_json_experiment;

using bytes = bytes_package;
using encoding = encoding_package;
using errors = errors_package;
using io = io_package;
using reflect = reflect_package;
using sync = sync_package;
using time = time_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using ꓸꓸꓸOptions = Span<Options>;

partial class json_package {

internal static encoding.TextMarshaler _ᴛ1ʗ;
internal static encoding.TextAppender _ᴛ2ʗ;
internal static encoding.TextUnmarshaler _ᴛ3ʗ;
internal static time.Time _ᴛ4ʗ;
internal static time.Duration _ᴛ5ʗ;

internal static error errChangingDuplicateNames = errors.New("cannot change duplicate name checks after a JSON object has already begun processing");
internal static error errChangingInvalidUTF8 = errors.New("cannot change UTF-8 checks after a JSON object has already begun processing");
internal static error errChangingWhitespace = errors.New("cannot change whitespace formatting within a MarshalEncode call");

internal static jsontext.export export = jsontext.Internal.Export(@internal.ᏑAllowInternalUse);

public static (slice<byte> @out, error err) Marshal(any @in, params ꓸꓸꓸOptions optsʗp) {
    slice<byte> @out = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        var opts = optsʗp.slice();

        opts = mayAppendSupportFormatTag(opts);
        var enc = export.GetBufferedEncoder(opts.ꓸꓸꓸ);
        defer((ᴛ0, ᴛ1) => ᴛ0.PutBufferedEncoder(ᴛ1), export, enc, ref ᒐ);
        var xe = export.Encoder(enc);
        xe.of(jsontext.encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.OmitTopLevelNewline | 1));
        err = marshalEncode(enc, @in, xe.ᏑStruct);
        if (err != nil && xe.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            (@out, err) = (default, @internal.TransformMarshalError(@in, err)); goto ᒐdone;
        }
        (@out, err) = (bytes.Clone(xe.Buf), err);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (@out, err);
}

public static error /*err*/ MarshalWrite(io.Writer @out, any @in, params ꓸꓸꓸOptions optsʗp) {
    error err = default;
    GoFrame ᒐ = default;
    try {
        var opts = optsʗp.slice();

        opts = mayAppendSupportFormatTag(opts);
        var enc = export.GetStreamingEncoder(@out, opts.ꓸꓸꓸ);
        defer((ᴛ0, ᴛ1) => ᴛ0.PutStreamingEncoder(ᴛ1), export, enc, ref ᒐ);
        var xe = export.Encoder(enc);
        xe.of(jsontext.encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.OmitTopLevelNewline | 1));
        err = marshalEncode(enc, @in, xe.ᏑStruct);
        if (err != nil && xe.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            err = @internal.TransformMarshalError(@in, err); goto ᒐdone;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return err;
}

public static error /*err*/ MarshalEncode(ж<jsontext.Encoder> @out, any @in, params ꓸꓸꓸOptions optsʗp) {
    error err = default;
    GoFrame ᒐ = default;
    try {
        var opts = optsʗp.slice();

        opts = mayAppendSupportFormatTag(opts);
        var xe = export.Encoder(@out);
        if (len(opts) > 0) {
            ref var optsOriginal = ref heap<jsonopts.Struct>(xe.Struct, out var ᏑoptsOriginal);
            var optsOriginalʗ1 = optsOriginal;
            var xeʗ1 = xe;
            defer(() => {
                xeʗ1.Struct = optsOriginalʗ1;
            }, ref ᒐ);
            xe.ᏑStruct.Join(opts.ꓸꓸꓸ);
            if (xe.Tokens.Last.NeedObjectName()) {
                if (optsOriginal.Flags.Get(jsonflags.AllowDuplicateNames) != xe.Struct.Flags.Get(jsonflags.AllowDuplicateNames)) {
                    err = newMarshalErrorBefore(@out, reflect.TypeOf(@in), errChangingDuplicateNames); goto ᒐdone;
                }
                if (optsOriginal.Flags.Get(jsonflags.AllowInvalidUTF8) != xe.Struct.Flags.Get(jsonflags.AllowInvalidUTF8)) {
                    err = newMarshalErrorBefore(@out, reflect.TypeOf(@in), errChangingInvalidUTF8); goto ᒐdone;
                }
            }
            if (xe.Struct.Flags.Has(jsonflags.AnyWhitespace)) {
                if (xe.Struct.Flags.Get(jsonflags.Multiline)) {
                    xe.ᏑStruct.InitializeMultiline();
                }
                if (jsonopts.ChangedWhitespace(optsOriginal, xe.Struct)) {
                    err = newMarshalErrorBefore(@out, reflect.TypeOf(@in), errChangingWhitespace); goto ᒐdone;
                }
            }
        }
        err = marshalEncode(@out, @in, xe.ᏑStruct);
        if (err != nil && xe.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            err = @internal.TransformMarshalError(@in, err); goto ᒐdone;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return err;
}

internal static error /*err*/ marshalEncode(ж<jsontext.Encoder> @out, any @in, ж<jsonopts.Struct> Ꮡmo) {
    ref var mo = ref Ꮡmo.DerefOrNull();

    var v = reflect.ValueOf(@in);
    if (!v.IsValid() || (v.Kind() == reflect.Pointer && v.IsNil())) {
        return @out.WriteToken(jsontext.Null);
    }
    var forceAddr = v.Kind() != reflect.Pointer;
    if (forceAddr) {
        var v2 = reflect.New(v.Type());
        v2.Elem().Set(v);
        v = v2;
    }
    var va = new addressableValue(v.Elem(), forceAddr);
    var t = va.Value.Type();

    var marshal = lookupArshaler(t).marshal;
    if (mo.Marshalers != nil) {
        (marshal, _) = mo.Marshalers._<ж<Marshalers>>().lookup(marshal, t);
    }
    var errΔ1 = marshal(@out, va, Ꮡmo); if (errΔ1 != nil) {
        if (!mo.Flags.Get(jsonflags.AllowDuplicateNames)) {
            export.Encoder(@out).of(jsontext.encoderState.ᏑTokens).InvalidateDisabledNamespaces();
        }
        return errΔ1;
    }
    return default;
}

public static error /*err*/ Unmarshal(slice<byte> @in, any @out, params ꓸꓸꓸOptions optsʗp) {
    error err = default;
    GoFrame ᒐ = default;
    try {
        var opts = optsʗp.slice();

        opts = mayAppendSupportFormatTag(opts);
        var dec = export.GetBufferedDecoder(@in, opts.ꓸꓸꓸ);
        defer((ᴛ0, ᴛ1) => ᴛ0.PutBufferedDecoder(ᴛ1), export, dec, ref ᒐ);
        var xd = export.Decoder(dec);
        err = unmarshalDecode(dec, @out, xd.ᏑStruct, true);
        if (err != nil && xd.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            err = @internal.TransformUnmarshalError(@out, err); goto ᒐdone;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return err;
}

public static error /*err*/ UnmarshalRead(io.Reader @in, any @out, params ꓸꓸꓸOptions optsʗp) {
    error err = default;
    GoFrame ᒐ = default;
    try {
        var opts = optsʗp.slice();

        opts = mayAppendSupportFormatTag(opts);
        var dec = export.GetStreamingDecoder(@in, opts.ꓸꓸꓸ);
        defer((ᴛ0, ᴛ1) => ᴛ0.PutStreamingDecoder(ᴛ1), export, dec, ref ᒐ);
        var xd = export.Decoder(dec);
        err = unmarshalDecode(dec, @out, xd.ᏑStruct, true);
        if (err != nil && xd.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            err = @internal.TransformUnmarshalError(@out, err); goto ᒐdone;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return err;
}

public static error /*err*/ UnmarshalDecode(ж<jsontext.Decoder> @in, any @out, params ꓸꓸꓸOptions optsʗp) {
    error err = default;
    GoFrame ᒐ = default;
    try {
        var opts = optsʗp.slice();

        opts = mayAppendSupportFormatTag(opts);
        var xd = export.Decoder(@in);
        if (len(opts) > 0) {
            ref var optsOriginal = ref heap<jsonopts.Struct>(xd.Struct, out var ᏑoptsOriginal);
            var optsOriginalʗ1 = optsOriginal;
            var xdʗ1 = xd;
            defer(() => {
                xdʗ1.Struct = optsOriginalʗ1;
            }, ref ᒐ);
            xd.ᏑStruct.Join(opts.ꓸꓸꓸ);
            if (xd.Tokens.Last.NeedObjectName()) {
                if (optsOriginal.Flags.Get(jsonflags.AllowDuplicateNames) != xd.Struct.Flags.Get(jsonflags.AllowDuplicateNames)) {
                    err = newUnmarshalErrorBefore(@in, reflect.TypeOf(@out), errChangingDuplicateNames); goto ᒐdone;
                }
                if (optsOriginal.Flags.Get(jsonflags.AllowInvalidUTF8) != xd.Struct.Flags.Get(jsonflags.AllowInvalidUTF8)) {
                    err = newUnmarshalErrorBefore(@in, reflect.TypeOf(@out), errChangingInvalidUTF8); goto ᒐdone;
                }
            }
        }
        err = unmarshalDecode(@in, @out, xd.ᏑStruct, false);
        if (err != nil && xd.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
            err = @internal.TransformUnmarshalError(@out, err); goto ᒐdone;
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return err;
}

internal static error /*err*/ unmarshalDecode(ж<jsontext.Decoder> Ꮡin, any @out, ж<jsonopts.Struct> Ꮡuo, bool last) {
    ref var @in = ref Ꮡin.DerefOrNull();
    ref var uo = ref Ꮡuo.DerefOrNull();

    var v = reflect.ValueOf(@out);
    if (v.Kind() != reflect.Pointer || v.IsNil()) {
        return new SemanticErrorжerror(Ꮡ(new SemanticError(action: "unmarshal", ΔGoType: reflect.TypeOf(@out), Err: @internal.ErrNonNilReference)));
    }
    var va = new addressableValue(v.Elem(), false);
    var t = va.Value.Type();

    if (uo.Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
        var errΔ1 = export.Decoder(Ꮡin).CheckNextValue(last); if (errΔ1 != nil) {
            if (AreEqual(errΔ1, io.EOF) && last) {
                ref var offset = ref heap<int64>(@in.InputOffset() + (int64)len(@in.UnreadBuffer()), out var Ꮡoffset);
                return new jsontext.SyntacticErrorжerror(Ꮡ(new jsontext.SyntacticError(ByteOffset: offset, Err: io.ErrUnexpectedEOF)));
            }
            return errΔ1;
        }
    }

    var unmarshal = lookupArshaler(t).unmarshal;
    if (uo.Unmarshalers != nil) {
        (unmarshal, _) = uo.Unmarshalers._<ж<Unmarshalers>>().lookup(unmarshal, t);
    }
    var errΔ2 = unmarshal(Ꮡin, va, Ꮡuo); if (errΔ2 != nil) {
        if (!uo.Flags.Get(jsonflags.AllowDuplicateNames)) {
            export.Decoder(Ꮡin).of(jsontext.decoderState.ᏑTokens).InvalidateDisabledNamespaces();
        }
        if (AreEqual(errΔ2, io.EOF) && last) {
            ref var offset = ref heap<int64>(@in.InputOffset() + (int64)len(@in.UnreadBuffer()), out var Ꮡoffset);
            return new jsontext.SyntacticErrorжerror(Ꮡ(new jsontext.SyntacticError(ByteOffset: offset, Err: io.ErrUnexpectedEOF)));
        }
        return errΔ2;
    }
    if (last) {
        return export.Decoder(Ꮡin).CheckEOF();
    }
    return default;
}

partial struct addressableValue {
    /*embed*/ public reflect_package.Value Value;
    internal bool forcedAddr;
}

internal static addressableValue newAddressableValue(reflect.Type t) {
    return new addressableValue(reflect.New(t).Elem(), true);
}

partial struct arshaler {
    internal marshaler marshal;
    internal unmarshaler unmarshal;
    internal bool nonDefault;
}

internal static readonly ж<sync.Map> ᏑlookupArshalerCache = new StaticBox<sync.Map>(default(sync.Map));
internal static ref sync.Map lookupArshalerCache => ref ((StaticBox<sync.Map>)ᏑlookupArshalerCache).ValueSlot;

internal static ж<arshaler> lookupArshaler(reflect.Type t) {
    var (vΔ1, ok) = ᏑlookupArshalerCache.Load(t); if (ok) {
        return vΔ1._<ж<arshaler>>();
    }

    var fncs = makeDefaultArshaler(t);
    fncs = makeMethodArshaler(fncs, t);
    fncs = makeTimeArshaler(fncs, t);

    var (v, _) = ᏑlookupArshalerCache.LoadOrStore(t, fncs.OrTypedNil());
    return v._<ж<arshaler>>();
}

internal static ж<sync.Pool> stringsPools = Ꮡ(new sync.Pool(New: () => @new<stringSlice>()));

partial struct stringSlice /*[]@string*/;

internal static ж<stringSlice> getStrings(nint n) {
    var s = stringsPools.Get()._<ж<stringSlice>>();
    if (cap(s.ValueSlot) < n) {
        s.ValueSlot = new slice<@string>(n);
    }
    s.ValueSlot = (s.ValueSlot).slice(0, n);
    return s;
}

internal static void putStrings(ж<stringSlice> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (cap(s) > 1 << 10) {
        s = default;
    }
    clear(s);
    stringsPools.Put(Ꮡs.OrTypedNil());
}

} // end json_package
