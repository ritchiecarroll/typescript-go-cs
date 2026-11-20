// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using bytes = bytes_package;
using io = io_package;
using strconv = strconv_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using go.github.com.go_json_experiment.json.@internal;
using static jsontext_package;

partial class jsontext_package {

internal static readonly @string errorPrefix = "jsontext: "u8;

partial struct ioError {
    internal @string action;
    internal error err;
}

} partial class jsontextᴛioError {

internal static @string Error(this ref ioError e) {
    return errorPrefix + e.action + " error: "u8 + e.err.Error();
}
internal static error Unwrap(this ref ioError e) {
    return e.err;
}

} partial class jsontext_package {

partial struct numError {
    internal @string accessor;
    internal @string value;
    internal error err;
}

} partial class jsontextᴛnumError {

internal static @string Error(this ref numError e) {
    return "jsontext.Token("u8 + e.value + ")."u8 + e.accessor + " error: "u8 + e.err.Error();
}
internal static error Unwrap(this ref numError e) {
    return e.err;
}

} partial class jsontext_package {

partial struct SyntacticError {
    /*embed*/ internal requireKeyedLiterals requireKeyedLiterals;
    /*embed*/ internal nonComparable nonComparable;
    public int64 ByteOffset;
    public Pointer JSONPointer;
    public error Err;
}

internal partial interface wrapSyntacticError_state /*dyn*/ {
    ж<jsonopts.Struct> options();
    int64 offsetAt(nint pos);
    slice<byte> AppendStackPointer(slice<byte> b, nint where);
}

internal static error wrapSyntacticError(wrapSyntacticError_state state, error err, nint pos, nint where) {
    {
        var (_, ok) = err._<ж<ioError>>(ᐧ); if (AreEqual(err, io.EOF) || ok) {
            return err;
        }
    }
    ref var offset = ref heap<int64>(state.offsetAt(pos), out var Ꮡoffset);
    var ptr = state.AppendStackPointer(default, where);
    if (err._<ж<pointerSuffixError>>(out var serr)) {
        ptr = serr.appendPointer(ptr);
        err = serr.error;
    }
    {
        var (d, ok) = state._<ж<decoderState>>(ᐧ); if (ok && AreEqual(err, errMismatchDelim)) {
            @string whereΔ1 = "at start of value";
            if (len(d.Tokens.Stack) > 0 && d.Tokens.Last.Length() > 0) {
                switch (ᐧ) {
                case {} when d.Tokens.Last.isArray(): {
                    whereΔ1 = "after array element (expecting ',' or ']')";
                    ptr = slice<byte>((@string)(((Pointer)ptr).Parent()));
                    break;
                }
                case {} when d.Tokens.Last.isObject(): {
                    whereΔ1 = "after object value (expecting ',' or '}')";
                    ptr = slice<byte>((@string)(((Pointer)ptr).Parent()));
                    break;
                }}

            }
            err = jsonwire.NewInvalidCharacterError(d.buf.slice(pos), whereΔ1);
        }
    }
    if (state.options().Flags.Get(jsonflags.ReportErrorsWithLegacySemantics) && !AreEqual(err, io.ErrUnexpectedEOF)) {
        var (werr, ok) = err._<ж<jsonwire.InvalidTextError>>(ᐧ); if (ok && werr != nil) {
            offset += (int64)len(werr.What);
        } else {
            offset++;
        }
    }
    return new SyntacticErrorжerror(Ꮡ(new SyntacticError(ByteOffset: offset, JSONPointer: ((Pointer)ptr), Err: err)));
}

} partial class jsontextᴛSyntacticError {

public static @string Error(this ref SyntacticError e) {
    var bᴷ = new stackAppend<byte>();

    Pointer pointer = e.JSONPointer;
    var offset = e.ByteOffset;
    var b = slice<byte>(errorPrefix);
    if (e.Err != nil) {
        b = append(b, e.Err.Error().ꓸꓸꓸ);
        if (AreEqual(e.Err, ErrDuplicateName)) {
            b = strconv.AppendQuote(append(bᴷ, b, (byte)' '), pointer.LastToken());
            pointer = pointer.Parent();
            offset = 0;
        }
    } else {
        b = append(b, ((@string)"syntactic error").ꓸꓸꓸ);
    }
    if (pointer != ""u8) {
        b = strconv.AppendQuote(append(b, ((@string)" within ").ꓸꓸꓸ), jsonwire.TruncatePointer((@string)pointer, 100));
    }
    if (offset > 0) {
        b = strconv.AppendInt(append(b, ((@string)" after offset ").ꓸꓸꓸ), offset, 10);
    }
    return (@string)b;
}

public static error Unwrap(this ref SyntacticError e) {
    return e.Err;
}

} partial class jsontext_package {

partial struct pointerSuffixError {
    /*embed*/ internal error error;
    internal slice<byte> reversePointer;
}

internal static error wrapWithObjectName(error err, slice<byte> quotedName) {
    var (serr, _) = err._<ж<pointerSuffixError>>(ᐧ);
    if (serr == nil) {
        serr = Ꮡ(new pointerSuffixError(error: err));
    }
    var name = jsonwire.UnquoteMayCopy(quotedName, false);
    serr.reversePointer = appendEscapePointerName(append(serr.reversePointer, (byte)'/'), name);
    return new pointerSuffixErrorжerror(serr);
}

internal static error wrapWithArrayIndex(error err, int64 index) {
    var (serr, _) = err._<ж<pointerSuffixError>>(ᐧ);
    if (serr == nil) {
        serr = Ꮡ(new pointerSuffixError(error: err));
    }
    serr.reversePointer = strconv.AppendUint(append(serr.reversePointer, (byte)'/'), (uint64)index, 10);
    return new pointerSuffixErrorжerror(serr);
}

} partial class jsontextᴛpointerSuffixError {

internal static slice<byte> appendPointer(this ref pointerSuffixError e, slice<byte> pointer) {
    var (bi, bo) = (e.reversePointer, pointer);
    while (len(bi) > 0) {
        nint i = bytes.LastIndexByte(bi, (byte)'/');
        (bi, bo) = (bi.slice(0, i), appendꓸꓸꓸ(bo, bi.slice(i)));
    }
    return bo;
}

} // end jsontextᴛpointerSuffixError
