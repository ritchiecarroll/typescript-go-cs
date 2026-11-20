// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using cmp = cmp_package;
using errors = errors_package;
using fmt = fmt_package;
using io = io_package;
using reflect = reflect_package;
using strconv = strconv_package;
using strings = strings_package;
using sync = sync_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using static json_package;

partial class json_package {

public static error ErrUnknownName = errors.New("unknown object member name");

internal static error errAmbiguousName = errors.New("ambiguous object member name");

internal static readonly @string errorPrefix = "json: "u8;

internal static bool isSemanticError(error err) {
    var (_, ok) = err._<ж<SemanticError>>(ᐧ);
    return ok;
}

internal static bool isSyntacticError(error err) {
    var (_, ok) = err._<ж<jsontext.SyntacticError>>(ᐧ);
    return ok;
}

internal static bool isFatalError(error err, jsonflags.Flags flags) {
    return !flags.Get(jsonflags.ReportErrorsWithLegacySemantics) || isSyntacticError(err) || export.IsIOError(err);
}

partial struct SemanticError {
    /*embed*/ internal requireKeyedLiterals requireKeyedLiterals;
    /*embed*/ internal nonComparable nonComparable;
    internal @string action;
    public int64 ByteOffset;
    public jsontext.Pointer JSONPointer;
    public jsontext.Kind JSONKind;
    public jsontext.Value JSONValue;
    public reflect.Type ΔGoType;
    public error Err;
}

partial interface coder {
    jsontext.Pointer StackPointer();
    Options Options();
}

internal static error newInvalidFormatError(coder c, reflect.Type t) {
    var err = fmt.Errorf("invalid format flag %q"u8, c.Options()._<ж<jsonopts.Struct>>().Format);
    switch (c.type()) {
    case ж<jsontext.Encoder> cΔ1: {
        err = newMarshalErrorBefore(cΔ1, t, err);
        break;
    }
    case ж<jsontext.Decoder> cΔ1: {
        err = newUnmarshalErrorBeforeWithSkipping(cΔ1, t, err);
        break;
    }}
    return err;
}

internal static error newMarshalErrorBefore(ж<jsontext.Encoder> Ꮡe, reflect.Type t, error err) {
    ref var e = ref Ꮡe.DerefOrNull();

    return new SemanticErrorжerror(Ꮡ(new SemanticError(action: "marshal", ΔGoType: t, Err: toUnexpectedEOF(err),
        ByteOffset: e.OutputOffset() + (int64)export.Encoder(Ꮡe).CountNextDelimWhitespace(),
        JSONPointer: ((jsontext.Pointer)export.Encoder(Ꮡe).AppendStackPointer(default, +1)))));
}

internal static error newUnmarshalErrorBefore(ж<jsontext.Decoder> Ꮡd, reflect.Type t, error err) {
    ref var d = ref Ꮡd.DerefOrNull();

    ref var k = ref heap(new jsontext.Kind(), out var Ꮡk);
    if (export.Decoder(Ꮡd).Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
        k = Ꮡd.PeekKind();
    }
    return new SemanticErrorжerror(Ꮡ(new SemanticError(action: "unmarshal", ΔGoType: t, Err: toUnexpectedEOF(err),
        ByteOffset: d.InputOffset() + (int64)export.Decoder(Ꮡd).CountNextDelimWhitespace(),
        JSONPointer: ((jsontext.Pointer)export.Decoder(Ꮡd).AppendStackPointer(default, +1)),
        JSONKind: k)));
}

internal static error newUnmarshalErrorBeforeWithSkipping(ж<jsontext.Decoder> d, reflect.Type t, error err) {
    err = newUnmarshalErrorBefore(d, t, err);
    if (export.Decoder(d).Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
        var err2 = export.Decoder(d).SkipValue(); if (err2 != nil) {
            return err2;
        }
    }
    return err;
}

internal static error newUnmarshalErrorAfter(ж<jsontext.Decoder> Ꮡd, reflect.Type t, error err) {
    ref var d = ref Ꮡd.DerefOrNull();

    var tokOrVal = export.Decoder(Ꮡd).PreviousTokenOrValue();
    ref var byteOffset = ref heap<int64>(d.InputOffset() - (int64)len(tokOrVal), out var ᏑbyteOffset);
    if (export.Decoder(Ꮡd).Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
        var k = ((jsontext.Value)tokOrVal).Kind(); if (k == (rune)'[' || k == (rune)'{') {
            byteOffset++;
        } else {
            byteOffset += (int64)len(tokOrVal);
        }
    }
    return new SemanticErrorжerror(Ꮡ(new SemanticError(action: "unmarshal", ΔGoType: t, Err: toUnexpectedEOF(err),
        ByteOffset: byteOffset,
        JSONPointer: ((jsontext.Pointer)export.Decoder(Ꮡd).AppendStackPointer(default, -1)),
        JSONKind: ((jsontext.Value)tokOrVal).Kind())));
}

internal static error newUnmarshalErrorAfterWithValue(ж<jsontext.Decoder> d, reflect.Type t, error err) {
    var serr = newUnmarshalErrorAfter(d, t, err)._<ж<SemanticError>>();
    if (serr.JSONKind == (rune)'"' || serr.JSONKind == (rune)'0') {
        serr.JSONValue = ((jsontext.Value)export.Decoder(d).PreviousTokenOrValue()).Clone();
    }
    return new SemanticErrorжerror(serr);
}

internal static error newUnmarshalErrorAfterWithSkipping(ж<jsontext.Decoder> d, reflect.Type t, error err) {
    err = newUnmarshalErrorAfter(d, t, err);
    if (export.Decoder(d).Flags.Get(jsonflags.ReportErrorsWithLegacySemantics)) {
        var err2 = export.Decoder(d).SkipValueRemainder(); if (err2 != nil) {
            return err2;
        }
    }
    return err;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string marshalˢ = "marshal"u8;
internal static readonly @string unmarshalˢ = "unmarshal"u8;

internal partial interface newSemanticErrorWithPosition_coderState /*dyn*/ {
    slice<byte> AppendStackPointer(slice<byte> _Δp0, nint _Δp1);
}

internal static error newSemanticErrorWithPosition(coder c, reflect.Type t, nint prevDepth, int64 prevLength, error err) {
    var (serr, _) = err._<ж<SemanticError>>(ᐧ);
    if (serr == nil) {
        serr = Ꮡ(new SemanticError(Err: err));
    }
    serr.Err = toUnexpectedEOF(serr.Err);
    nint currDepth = default;
    int64 currLength = default;
    newSemanticErrorWithPosition_coderState coderState = default;
    int64 offset = default;
    switch (c.type()) {
    case ж<jsontext.Encoder> cΔ1: {
        var e = export.Encoder(cΔ1);
        serr.action = cmp.Or(serr.action, marshalˢ);
        (currDepth, currLength) = e.Tokens.DepthLength();
        offset = cΔ1.OutputOffset() + (int64)export.Encoder(cΔ1).CountNextDelimWhitespace();
        coderState = new jsontext_encoderStateжnewSemanticErrorWithPosition_coderState(e);
        break;
    }
    case ж<jsontext.Decoder> cΔ1: {
        var d = export.Decoder(cΔ1);
        serr.action = cmp.Or(serr.action, unmarshalˢ);
        (currDepth, currLength) = d.Tokens.DepthLength();
        var tokOrVal = d.PreviousTokenOrValue();
        offset = cΔ1.InputOffset() - (int64)len(tokOrVal);
        if ((prevDepth == currDepth && prevLength == currLength) || len(tokOrVal) == 0) {
            offset = cΔ1.InputOffset() + (int64)export.Decoder(cΔ1).CountNextDelimWhitespace();
        }
        coderState = new jsontext_decoderStateжnewSemanticErrorWithPosition_coderState(d);
        break;
    }}
    serr.ByteOffset = cmp.Or(serr.ByteOffset, offset);
    if (serr.JSONPointer == ""u8) {
        nint where = 0;
        switch (ᐧ) {
        case {} when prevDepth == currDepth && prevLength + 0 == currLength: {
            where = +1;
            break;
        }
        case {} when prevDepth == currDepth && prevLength + 1 == currLength: {
            where = -1;
            break;
        }}

        serr.JSONPointer = (jsontext.Pointer)coderState.AppendStackPointer(default, where);
    }
    serr.ΔGoType = cmp.Or(serr.ΔGoType, t);
    return new SemanticErrorжerror(serr);
}

internal static error collapseSemanticErrors(error err) {
    var (serr1, ok) = err._<ж<SemanticError>>(ᐧ); if (ok) {
        var (serr2, okΔ1) = serr1.Err._<ж<SemanticError>>(ᐧ); if (okΔ1) {
            serr2.ByteOffset = serr1.ByteOffset + serr2.ByteOffset;
            serr2.JSONPointer = serr1.JSONPointer + serr2.JSONPointer;
            serr1.Value = serr2.Value;
        }
    }
    return err;
}

internal static error wrapErrUnsupported(error err, @string what) {
    if (errors.Is(err, errors.ErrUnsupported)) {
        return errors.New(what + " may not return errors.ErrUnsupported"u8);
    }
    return err;
}

internal static Func<@string> errorModalVerb = sync.OnceValue(@string () => {
    foreach (var (phrase, _) in new map<@string, EmptyStruct>{["cannot"] = new(), ["unable to"] = new()}) {
        return phrase;
    }
    return "";
});

} partial class jsonᴛSemanticError {

public static @string Error(this ref SemanticError e) {
    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    Ꮡsb.WriteString(errorPrefix);
    Ꮡsb.WriteString(errorModalVerb());

    @string preposition = default;
    var exprᴛ1 = e.action;
    switch (ᐧ) {
    case {} when exprᴛ1 == "marshal"u8: {
        Ꮡsb.WriteString(" marshal");
        preposition = " from";
        break;
    }
    case {} when exprᴛ1 == "unmarshal"u8: {
        Ꮡsb.WriteString(" unmarshal");
        preposition = " into";
        break;
    }
    default: {
        Ꮡsb.WriteString(" handle");
        preposition = " with";
        break;
    }}

    switch (e.JSONKind) {
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
        Ꮡsb.WriteString(" JSON null");
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'f') || ᴛ == (jsontext.Kind)((rune)'t'): {
        Ꮡsb.WriteString(" JSON boolean");
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'"'): {
        Ꮡsb.WriteString(" JSON string");
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'0'): {
        Ꮡsb.WriteString(" JSON number");
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'{') || ᴛ == (jsontext.Kind)((rune)'}'): {
        Ꮡsb.WriteString(" JSON object");
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'[') || ᴛ == (jsontext.Kind)((rune)']'): {
        Ꮡsb.WriteString(" JSON array");
        break;
    }
    default: {
        if (e.action == ""u8) {
            preposition = "";
        }
        break;
    }}

    if (len(e.JSONValue) > 0 && len(e.JSONValue) < 100) {
        Ꮡsb.WriteByte((byte)' ');
        Ꮡsb.Write(e.JSONValue);
    }

    if (e.ΔGoType != nil) {
        @string typeString = e.ΔGoType.String();
        if (len(typeString) > 100) {
            typeString = e.ΔGoType.Kind().String();
            if (e.ΔGoType.Kind() == reflect.Struct && e.ΔGoType.Name() == ""u8) {
                foreach (var i in range(e.ΔGoType.NumField())) {
                    @string pkgPath = e.ΔGoType.Field(i).PkgPath; if (pkgPath != ""u8) {
                        typeString = pkgPath.slice(strings.LastIndexByte(pkgPath, (byte)'/') + len("/")) + ".struct";
                        break;
                    }
                }
            }
        }
        Ꮡsb.WriteString(preposition);
        Ꮡsb.WriteString(" Go ");
        Ꮡsb.WriteString(typeString);
    }

    if (AreEqual(e.Err, ErrUnknownName) || AreEqual(e.Err, errAmbiguousName)) {
        Ꮡsb.WriteString(": ");
        Ꮡsb.WriteString(e.Err.Error());
        Ꮡsb.WriteString(" ");
        Ꮡsb.WriteString(strconv.Quote(e.JSONPointer.LastToken()));
        jsontext.Pointer parent = e.JSONPointer.Parent(); if (parent != ""u8) {
            Ꮡsb.WriteString(" within ");
            Ꮡsb.WriteString(strconv.Quote(jsonwire.TruncatePointer((@string)parent, 100)));
        }
        return sb.String();
    }

    var (serr, _) = e.Err._<ж<jsontext.SyntacticError>>(ᐧ);
    switch (ᐧ) {
    case {} when e.JSONPointer != ""u8: {
        if (serr == nil || !e.JSONPointer.Contains(serr.JSONPointer)) {
            Ꮡsb.WriteString(" within ");
            Ꮡsb.WriteString(strconv.Quote(jsonwire.TruncatePointer((@string)e.JSONPointer, 100)));
        }
        break;
    }
    case {} when e.ByteOffset is > 0: {
        if (serr == nil || !(e.ByteOffset <= serr.ByteOffset)) {
            Ꮡsb.WriteString(" after offset ");
            Ꮡsb.WriteString(strconv.FormatInt(e.ByteOffset, 10));
        }
        break;
    }}

    if (e.Err != nil) {
        @string errString = e.Err.Error();
        if (isSyntacticError(e.Err)) {
            errString = strings.TrimPrefix(errString, "jsontext: ");
        }
        Ꮡsb.WriteString(": ");
        Ꮡsb.WriteString(errString);
    }

    return sb.String();
}

public static error Unwrap(this ref SemanticError e) {
    return e.Err;
}

} partial class json_package {

internal static error newDuplicateNameError(jsontext.Pointer ptr, slice<byte> quotedName, int64 offset) {
    if (quotedName != nil) {
        var (name, _) = jsonwire.AppendUnquote(default, quotedName);
        ptr = ptr.AppendToken((@string)name);
    }
    return new jsontext.SyntacticErrorжerror(Ꮡ(new jsontext.SyntacticError(
        ByteOffset: offset,
        JSONPointer: ptr,
        Err: jsontext.ErrDuplicateName
    )));
}

internal static error toUnexpectedEOF(error err) {
    if (AreEqual(err, io.EOF)) {
        return io.ErrUnexpectedEOF;
    }
    return err;
}

} // end json_package
