// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using bytes = bytes_package;
using errors = errors_package;
using io = io_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using go.github.com.go_json_experiment.json.@internal;
using ꓸꓸꓸOptions = Span<Options>;
using static jsontext_package;

partial class jsontext_package {

partial struct Decoder {
    internal decoderState s;
}

public partial struct decoderState {
    /*embed*/ internal state state;
    /*embed*/ internal decodeBuffer decodeBuffer;
    /*embed*/ public go.github.com.go_json_experiment.json.@internal.jsonopts_package.Struct Struct;
    public /*[256]*/ ж<array<@string>> StringCache;
}

partial struct decodeBuffer {
    internal nint peekPos;
    internal error peekErr;
    internal slice<byte> buf;
    internal nint prevStart;
    internal nint prevEnd;
    internal int64 baseOffset;
    internal io.Reader rd;
}

public static ж<Decoder> NewDecoder(io.Reader r, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    var d = @new<Decoder>();
    d.Reset(r, opts.ꓸꓸꓸ);
    return d;
}

} partial class jsontextᴛDecoder {

public static void Reset(this ж<Decoder> Ꮡd, io.Reader r, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();
    ref var d = ref Ꮡd.DerefOrNull();

    switch (ᐧ) {
    case {} when Ꮡd == nil: {
        throw panic("jsontext: invalid nil Decoder");
        break;
    }
    case {} when r == nil: {
        throw panic("jsontext: invalid nil io.Reader");
        break;
    }
    case {} when d.s.Flags.Get(jsonflags.WithinArshalCall): {
        throw panic("jsontext: cannot reset Decoder passed to json.UnmarshalerFrom");
        break;
    }}

    var b = d.s.buf[..0];
    var (_, ok) = d.s.rd._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        b = default;
    }
    d.s.reset(b, r, opts.ꓸꓸꓸ);
}

} partial class jsontextᴛdecoderState {

internal static void reset(this ref decoderState d, slice<byte> b, io.Reader r, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    d.state.reset();
    d.decodeBuffer = new decodeBuffer(buf: b, rd: r);
    var opts2 = new jsonopts.Struct();
    opts2.Join(opts.ꓸꓸꓸ);
    d.Struct = opts2;
}

} partial class jsontextᴛDecoder {

public static Options Options(this ж<Decoder> Ꮡd) {
    return new jsonopts.StructжOptions(Ꮡd.Ꮡs.ᏑStruct);
}

} partial class jsontextᴛdecoderState {

internal static ж<jsonopts.Struct> options(this ж<decoderState> Ꮡd) { return Ꮡd.ᏑStruct; }

} partial class jsontext_package {

internal static error errBufferWriteAfterNext = errors.New("invalid bytes.Buffer.Write call after calling bytes.Buffer.Next");

} partial class jsontextᴛdecoderState {

internal static error fetch(this ref decoderState d) {
    if (d.rd == nil) {
        return io.ErrUnexpectedEOF;
    }

    d.Names.copyQuotedBuffer(d.buf);

    var (bb, ok) = d.rd._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        switch (ᐧ) {
        case {} when bb.Len() is 0: {
            return io.ErrUnexpectedEOF;
        }
        case {} when len(d.buf) is 0: {
            d.buf = bb.Next(bb.Len());
            return default;
        }
        default: {
            return new ioErrorжerror(Ꮡ(new ioError(action: "read", err: errBufferWriteAfterNext)));
        }}

    }

    if (cap(d.buf) == 0) {
        d.buf = new slice<byte>(0, 64);
    }

    UntypedInt maxBufferSize = /* 4 << 10 */ 4096;
    UntypedInt growthSizeFactor = 2;
    const int64 growthRateFactor = 2;
    var grow = cap(d.buf) <= (nint)(maxBufferSize / growthSizeFactor);
    grow = grow && (int64)cap(d.buf) < d.decodeBuffer.previousOffsetEnd() / growthRateFactor;
    grow = grow || (d.prevStart == 0 && len(d.buf) >= 3 * cap(d.buf) / 4);

    if (grow) {
        var buf = new slice<byte>(0, cap(d.buf) * (nint)growthSizeFactor);
        d.buf = appendꓸꓸꓸ(buf, d.buf.slice(d.prevStart));
    } else {
        nint n = copy(d.buf.slice(0, cap(d.buf)), d.buf.slice(d.prevStart));
        d.buf = d.buf.slice(0, n);
    }
    d.baseOffset += (int64)d.prevStart;
    d.prevEnd -= d.prevStart;
    d.prevStart = 0;

    for (;;) {
        var (n, err) = d.rd.Read(d.buf.slice(len(d.buf), cap(d.buf)));
        switch (ᐧ) {
        case {} when n is > 0: {
            d.buf = d.buf.slice(0, len(d.buf) + n);
            return default;
        }
        case {} when AreEqual(err, io.EOF): {
            return io.ErrUnexpectedEOF;
        }
        case {} when err != nil: {
            return new ioErrorжerror(Ꮡ(new ioError(action: "read", err: err)));
        }
        default: {
            continue;
            break;
        }}

    }
}

} partial class jsontext_package {

internal static UntypedInt invalidateBufferByte => /* '#' */ 35;

} partial class jsontextᴛdecodeBuffer {

internal static void invalidatePreviousRead(this ref decodeBuffer d) {
    bool isBytesBuffer(io.Reader r) {
        var (_, ok) = r._<ж<bytes.Buffer>>(ᐧ);
        return ok;
    }
    if (d.rd != nil && !isBytesBuffer(d.rd) && d.prevStart < d.prevEnd && (nuint)d.prevStart < (nuint)len(d.buf)) {
        d.buf[d.prevStart] = invalidateBufferByte;
        d.prevStart = d.prevEnd;
    }
}

internal static bool needMore(this ref decodeBuffer d, nint pos) {
    return pos == len(d.buf);
}

internal static int64 offsetAt(this ref decodeBuffer d, nint pos) { return d.baseOffset + (int64)pos; }
internal static int64 previousOffsetStart(this ref decodeBuffer d) { return d.baseOffset + (int64)d.prevStart; }
internal static int64 previousOffsetEnd(this ref decodeBuffer d) { return d.baseOffset + (int64)d.prevEnd; }
internal static slice<byte> previousBuffer(this ref decodeBuffer d) { return d.buf.slice(d.prevStart, d.prevEnd); }
internal static slice<byte> unreadBuffer(this ref decodeBuffer d) { return d.buf.slice(d.prevEnd, len(d.buf)); }

internal static slice<byte> PreviousTokenOrValue(this ref decodeBuffer d) {
    var b = d.previousBuffer();
    if (d.peekPos > 0 || len(b) > 0 && b[0] == invalidateBufferByte) {
        return default;
    }
    if (len(b) == 0) {
        b = d.buf.slice(0, d.prevEnd);
        foreach (var (_, tok) in range(slice<@string>(["null", "false", "true", "{", "}", "[", "]"]))) {
            if (len(b) >= len(tok) && ((sstring)(b.slice(len(b) - len(tok)))) == tok) {
                return b.slice(len(b) - len(tok));
            }
        }
    }
    return b;
}

} partial class jsontextᴛDecoder {

public static Kind PeekKind(this ж<Decoder> Ꮡd) {
    return Ꮡd.Ꮡs.PeekKind();
}

} partial class jsontextᴛdecoderState {

public static Kind PeekKind(this ж<decoderState> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    if (d.peekPos > 0) {
        return ((Kind)d.buf[d.peekPos]).normalize();
    }

    error err = default;
    Ꮡd.of(decoderState.ᏑdecodeBuffer).invalidatePreviousRead();
    nint pos = d.prevEnd;

    pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
    if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
        (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
            if (AreEqual(err, io.ErrUnexpectedEOF) && d.Tokens.Depth() == 1) {
                err = io.EOF;
            }
            (d.peekPos, d.peekErr) = (-1, wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, 0));
            return invalidKind;
        }
    }

    byte delim = default;
    var c = d.buf[pos]; if (c == ':' || c == ',') {
        delim = c;
        pos += 1;
        pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
        if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
            (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                err = wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, 0);
                (d.peekPos, d.peekErr) = (-1, Ꮡd.checkDelimBeforeIOError(delim, err));
                return invalidKind;
            }
        }
    }
    var next = ((Kind)d.buf[pos]).normalize();
    if (d.Tokens.needDelim(next) != delim) {
        (d.peekPos, d.peekErr) = (-1, Ꮡd.checkDelim(delim, next));
        return invalidKind;
    }

    (d.peekPos, d.peekErr) = (pos, default);
    return next;
}

internal static error checkDelimBeforeIOError(this ж<decoderState> Ꮡd, byte delim, error err) {
    ref var d = ref Ꮡd.DerefOrNull();

    Kind next = /* Kind('"') */ 34;
    if (d.Tokens.needDelim(next) != delim) {
        err = Ꮡd.checkDelim(delim, next);
    }
    return err;
}

public static nint CountNextDelimWhitespace(this ж<decoderState> Ꮡd) {
    Ꮡd.PeekKind();
    return len(Ꮡd.of(decoderState.ᏑdecodeBuffer).unreadBuffer()) - len(bytes.TrimLeft(Ꮡd.of(decoderState.ᏑdecodeBuffer).unreadBuffer(), ",: \n\r\t"));
}

internal static error checkDelim(this ж<decoderState> Ꮡd, byte delim, Kind next) {
    ref var d = ref Ꮡd.DerefOrNull();

    @string where = "at start of value";
    switch (d.Tokens.needDelim(next)) {
    case var ᴛ when ᴛ == delim: {
        return default;
    }
    case (byte)':': {
        where = "after object name (expecting ':')";
        break;
    }
    case (byte)',': {
        if (d.Tokens.Last.isObject()) {
            where = "after object value (expecting ',' or '}')";
        } else {
            where = "after array element (expecting ',' or ']')";
        }
        break;
    }}

    nint pos = d.prevEnd;
    pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
    var err = jsonwire.NewInvalidCharacterError(d.buf.slice(pos), where);
    return wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, 0);
}

} partial class jsontextᴛDecoder {

public static error SkipValue(this ж<Decoder> Ꮡd) {
    return Ꮡd.Ꮡs.SkipValue();
}

} partial class jsontextᴛdecoderState {

public static error SkipValue(this ж<decoderState> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    switch (Ꮡd.PeekKind()) {
    case var ᴛ when ᴛ == (Kind)((rune)'{') || ᴛ == (Kind)((rune)'['): {
        nint depth = d.Tokens.Depth();
        for (;;) {
            var (_, err) = Ꮡd.ReadToken(); if (err != nil) {
                return err;
            }
            if (depth >= d.Tokens.Depth()) {
                return default;
            }
        }
        break;
    }
    default: {
        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        {
            var (_, err) = Ꮡd.ReadValue(Ꮡflags); if (err != nil) {
                return err;
            }
        }
        return default;
    }}

}

public static error SkipValueRemainder(this ж<decoderState> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    if (d.Tokens.Depth() - 1 > 0 && d.Tokens.Last.Length() == 0) {
        for (nint n = d.Tokens.Depth(); d.Tokens.Depth() >= n; ) {
            var (_, err) = Ꮡd.ReadToken(); if (err != nil) {
                return err;
            }
        }
    }
    return default;
}

public static error SkipUntil(this ж<decoderState> Ꮡd, nint depth, int64 length) {
    ref var d = ref Ꮡd.DerefOrNull();

    while (d.Tokens.Depth() > depth || (d.Tokens.Depth() == depth && d.Tokens.Last.Length() < length)) {
        var (_, err) = Ꮡd.ReadToken(); if (err != nil) {
            return err;
        }
    }
    return default;
}

} partial class jsontextᴛDecoder {

public static (Token, error) ReadToken(this ж<Decoder> Ꮡd) {
    return Ꮡd.Ꮡs.ReadToken();
}

} partial class jsontextᴛdecoderState {

public static (Token, error) ReadToken(this ж<decoderState> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    error err = default;
    Kind next = default;
    nint pos = d.peekPos;
    if (pos != 0) {
        if (d.peekErr != nil) {
            var errΔ1 = d.peekErr;
            (d.peekPos, d.peekErr) = (0, default);
            return (new Token(), errΔ1);
        }
        next = ((Kind)d.buf[pos]).normalize();
        d.peekPos = 0;
    } else {
        Ꮡd.of(decoderState.ᏑdecodeBuffer).invalidatePreviousRead();
        pos = d.prevEnd;

        pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
        if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
            (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                if (AreEqual(err, io.ErrUnexpectedEOF) && d.Tokens.Depth() == 1) {
                    err = io.EOF;
                }
                return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, 0));
            }
        }

        byte delim = default;
        var c = d.buf[pos]; if (c == ':' || c == ',') {
            delim = c;
            pos += 1;
            pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
            if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
                (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                    err = wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, 0);
                    return (new Token(), Ꮡd.checkDelimBeforeIOError(delim, err));
                }
            }
        }
        next = ((Kind)d.buf[pos]).normalize();
        if (d.Tokens.needDelim(next) != delim) {
            return (new Token(), Ꮡd.checkDelim(delim, next));
        }
    }

    nint n = default;
    switch (next) {
    case var ᴛ when ᴛ == (Kind)((rune)'n'): {
        if (jsonwire.ConsumeNull(d.buf.slice(pos)) == 0) {
            (pos, err) = d.consumeLiteral(pos, "null");
            if (err != nil) {
                return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
            }
        } else {
            pos += len("null");
        }
        err = d.Tokens.appendLiteral(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos - len("null"), +1));
        }
        (d.prevStart, d.prevEnd) = (pos, pos);
        return (Null, default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'f'): {
        if (jsonwire.ConsumeFalse(d.buf.slice(pos)) == 0) {
            (pos, err) = d.consumeLiteral(pos, "false");
            if (err != nil) {
                return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
            }
        } else {
            pos += len("false");
        }
        err = d.Tokens.appendLiteral(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos - len("false"), +1));
        }
        (d.prevStart, d.prevEnd) = (pos, pos);
        return (False, default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'t'): {
        if (jsonwire.ConsumeTrue(d.buf.slice(pos)) == 0) {
            (pos, err) = d.consumeLiteral(pos, "true");
            if (err != nil) {
                return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
            }
        } else {
            pos += len("true");
        }
        err = d.Tokens.appendLiteral(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos - len("true"), +1));
        }
        (d.prevStart, d.prevEnd) = (pos, pos);
        return (True, default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'"'): {
        ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
        n = jsonwire.ConsumeSimpleString(d.buf.slice(pos)); if (n == 0) {
            var oldAbsPos = d.baseOffset + (int64)pos;
            (pos, err) = d.consumeString(Ꮡflags, pos);
            var newAbsPos = d.baseOffset + (int64)pos;
            n = (nint)(newAbsPos - oldAbsPos);
            if (err != nil) {
                return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
            }
        } else {
            pos += n;
        }
        if (d.Tokens.Last.NeedObjectName()) {
            if (!d.Flags.Get(jsonflags.AllowDuplicateNames)) {
                if (!d.Tokens.Last.isValidNamespace()) {
                    return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), errInvalidNamespace, pos - n, +1));
                }
                if (d.Tokens.Last.isActiveNamespace() && !d.Namespaces.Last().insertQuoted(d.buf.slice(pos - n, pos), flags.IsVerbatim())) {
                    err = wrapWithObjectName(ErrDuplicateName, d.buf.slice(pos - n, pos));
                    return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos - n, +1));
                }
            }
            d.Names.ReplaceLastQuotedOffset(pos - n);
        }
        err = d.Tokens.appendString(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos - n, +1));
        }
        (d.prevStart, d.prevEnd) = (pos - n, pos);
        return (new Token(raw: Ꮡd.ᏑdecodeBuffer, num: (uint64)Ꮡd.of(decoderState.ᏑdecodeBuffer).previousOffsetStart()), default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'0'): {
        n = jsonwire.ConsumeSimpleNumber(d.buf.slice(pos)); if (n == 0 || Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos + n)) {
            var oldAbsPos = d.baseOffset + (int64)pos;
            (pos, err) = d.consumeNumber(pos);
            var newAbsPos = d.baseOffset + (int64)pos;
            n = (nint)(newAbsPos - oldAbsPos);
            if (err != nil) {
                return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
            }
        } else {
            pos += n;
        }
        err = d.Tokens.appendNumber(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos - n, +1));
        }
        (d.prevStart, d.prevEnd) = (pos - n, pos);
        return (new Token(raw: Ꮡd.ᏑdecodeBuffer, num: (uint64)Ꮡd.of(decoderState.ᏑdecodeBuffer).previousOffsetStart()), default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'{'): {
        err = d.Tokens.pushObject(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
        }
        d.Names.push();
        if (!d.Flags.Get(jsonflags.AllowDuplicateNames)) {
            Ꮡd.of(decoderState.ᏑNamespaces).push();
        }
        d.Flags.Clear(jsonflags.TagFlags);
        pos += 1;
        (d.prevStart, d.prevEnd) = (pos, pos);
        return (BeginObject, default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'}'): {
        err = d.Tokens.popObject(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
        }
        d.Names.pop();
        if (!d.Flags.Get(jsonflags.AllowDuplicateNames)) {
            d.Namespaces.pop();
        }
        pos += 1;
        (d.prevStart, d.prevEnd) = (pos, pos);
        return (EndObject, default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'['): {
        err = d.Tokens.pushArray(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
        }
        d.Flags.Clear(jsonflags.TagFlags);
        pos += 1;
        (d.prevStart, d.prevEnd) = (pos, pos);
        return (BeginArray, default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)']'): {
        err = d.Tokens.popArray(); if (err != nil) {
            return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
        }
        pos += 1;
        (d.prevStart, d.prevEnd) = (pos, pos);
        return (EndArray, default);
    }
    default: {
        err = jsonwire.NewInvalidCharacterError(d.buf.slice(pos), "at start of value");
        return (new Token(), wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
    }}

}

} partial class jsontextᴛDecoder {

public static (Value, error) ReadValue(this ж<Decoder> Ꮡd) {
    ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
    return Ꮡd.Ꮡs.ReadValue(Ꮡflags);
}

} partial class jsontextᴛdecoderState {

public static (Value, error) ReadValue(this ж<decoderState> Ꮡd, ж<jsonwire.ValueFlags> flags) {
    ref var d = ref Ꮡd.DerefOrNull();

    error err = default;
    Kind next = default;
    nint pos = d.peekPos;
    if (pos != 0) {
        if (d.peekErr != nil) {
            var errΔ1 = d.peekErr;
            (d.peekPos, d.peekErr) = (0, default);
            return (default, errΔ1);
        }
        next = ((Kind)d.buf[pos]).normalize();
        d.peekPos = 0;
    } else {
        Ꮡd.of(decoderState.ᏑdecodeBuffer).invalidatePreviousRead();
        pos = d.prevEnd;

        pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
        if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
            (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                if (AreEqual(err, io.ErrUnexpectedEOF) && d.Tokens.Depth() == 1) {
                    err = io.EOF;
                }
                return (default, wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, 0));
            }
        }

        byte delim = default;
        var c = d.buf[pos]; if (c == ':' || c == ',') {
            delim = c;
            pos += 1;
            pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
            if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
                (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                    err = wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, 0);
                    return (default, Ꮡd.checkDelimBeforeIOError(delim, err));
                }
            }
        }
        next = ((Kind)d.buf[pos]).normalize();
        if (d.Tokens.needDelim(next) != delim) {
            return (default, Ꮡd.checkDelim(delim, next));
        }
    }

    var oldAbsPos = d.baseOffset + (int64)pos;
    (pos, err) = Ꮡd.consumeValue(flags, pos, d.Tokens.Depth());
    var newAbsPos = d.baseOffset + (int64)pos;
    nint n = (nint)(newAbsPos - oldAbsPos);
    if (err != nil) {
        return (default, wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos, +1));
    }
    switch (next) {
    case var ᴛ when ᴛ == (Kind)((rune)'n') || ᴛ == (Kind)((rune)'t') || ᴛ == (Kind)((rune)'f'): {
        err = d.Tokens.appendLiteral();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'"'): {
        if (d.Tokens.Last.NeedObjectName()) {
            if (!d.Flags.Get(jsonflags.AllowDuplicateNames)) {
                if (!d.Tokens.Last.isValidNamespace()) {
                    err = errInvalidNamespace;
                    break;
                }
                if (d.Tokens.Last.isActiveNamespace() && !d.Namespaces.Last().insertQuoted(d.buf.slice(pos - n, pos), flags.IsVerbatim())) {
                    err = wrapWithObjectName(ErrDuplicateName, d.buf.slice(pos - n, pos));
                    break;
                }
            }
            d.Names.ReplaceLastQuotedOffset(pos - n);
        }
        err = d.Tokens.appendString();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'0'): {
        err = d.Tokens.appendNumber();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'{'): {
        err = d.Tokens.pushObject(); if (err != nil) {
            break;
        }
        err = d.Tokens.popObject(); if (err != nil) {
            throw panic("BUG: popObject should never fail immediately after pushObject: " + err.Error());
        }
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'['): {
        err = d.Tokens.pushArray(); if (err != nil) {
            break;
        }
        err = d.Tokens.popArray(); if (err != nil) {
            throw panic("BUG: popArray should never fail immediately after pushArray: " + err.Error());
        }
        break;
    }}

    if (err != nil) {
        return (default, wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), err, pos - n, +1));
    }
    d.prevEnd = pos;
    d.prevStart = pos - n;
    return (d.buf.slice(pos - n, pos, pos), default);
}

public static error CheckNextValue(this ж<decoderState> Ꮡd, bool last) {
    ref var d = ref Ꮡd.DerefOrNull();

    Ꮡd.PeekKind();
    nint pos = d.peekPos;
    var err = d.peekErr;
    (d.peekPos, d.peekErr) = (0, default);
    if (err != nil) {
        return err;
    }

    ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
    var (posΔ1, errΔ1) = Ꮡd.consumeValue(Ꮡflags, pos, d.Tokens.Depth()); if (errΔ1 != nil) {
        return wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), errΔ1, posΔ1, +1);
    } else if (last) {
        return Ꮡd.checkEOF(posΔ1);
    }
    return default;
}

public static bool AtEOF(this ref decoderState d) {
    var (_, err) = d.consumeWhitespace(d.prevEnd);
    return AreEqual(err, io.ErrUnexpectedEOF);
}

public static error CheckEOF(this ж<decoderState> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    return Ꮡd.checkEOF(d.prevEnd);
}
internal static error checkEOF(this ж<decoderState> Ꮡd, nint pos) {
    ref var d = ref Ꮡd.DerefOrNull();

    var (posΔ1, err) = d.consumeWhitespace(pos);
    switch (err) {
    case var ᴛ when AreEqual(ᴛ, default): {
        var errΔ2 = jsonwire.NewInvalidCharacterError(d.buf.slice(posΔ1), "after top-level value");
        return wrapSyntacticError(new decoderStateжwrapSyntacticError_state(Ꮡd), errΔ2, posΔ1, 0);
    }
    case var ᴛ when AreEqual(ᴛ, io.ErrUnexpectedEOF): {
        return default;
    }
    default: {
        return err;
    }}

}

internal static (nint newPos, error err) consumeWhitespace(this ref decoderState d, nint pos) {
    error err = default;

    for (;;) {
        pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
        if (d.decodeBuffer.needMore(pos)) {
            var absPos = d.baseOffset + (int64)pos;
            err = d.fetch();
            pos = (nint)(absPos - d.baseOffset);
            if (err != nil) {
                return (pos, err);
            }
            continue;
        }
        return (pos, default);
    }
}

internal static (nint newPos, error err) consumeValue(this ж<decoderState> Ꮡd, ж<jsonwire.ValueFlags> flags, nint pos, nint depth) {
    ref var d = ref Ꮡd.DerefOrNull();

    for (;;) {
        nint n = default;
        error errΔ1 = default;
        var next = ((Kind)d.buf[pos]).normalize();
        switch (next) {
        case var ᴛ when ᴛ == (Kind)((rune)'n'): {
            n = jsonwire.ConsumeNull(d.buf.slice(pos)); if (n == 0) {
                (n, errΔ1) = jsonwire.ConsumeLiteral(d.buf.slice(pos), "null");
            }
            break;
        }
        case var ᴛ when ᴛ == (Kind)((rune)'f'): {
            n = jsonwire.ConsumeFalse(d.buf.slice(pos)); if (n == 0) {
                (n, errΔ1) = jsonwire.ConsumeLiteral(d.buf.slice(pos), "false");
            }
            break;
        }
        case var ᴛ when ᴛ == (Kind)((rune)'t'): {
            n = jsonwire.ConsumeTrue(d.buf.slice(pos)); if (n == 0) {
                (n, errΔ1) = jsonwire.ConsumeLiteral(d.buf.slice(pos), "true");
            }
            break;
        }
        case var ᴛ when ᴛ == (Kind)((rune)'"'): {
            n = jsonwire.ConsumeSimpleString(d.buf.slice(pos)); if (n == 0) {
                return d.consumeString(flags, pos);
            }
            break;
        }
        case var ᴛ when ᴛ == (Kind)((rune)'0'): {
            n = jsonwire.ConsumeSimpleNumber(d.buf.slice(pos)); if (n == 0 || Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos + n)) {
                return d.consumeNumber(pos);
            }
            break;
        }
        case var ᴛ when ᴛ == (Kind)((rune)'{'): {
            return Ꮡd.consumeObject(flags, pos, depth);
        }
        case var ᴛ when ᴛ == (Kind)((rune)'['): {
            return Ꮡd.consumeArray(flags, pos, depth);
        }
        default: {
            if ((d.Tokens.Last.isObject() && next == (rune)']') || (d.Tokens.Last.isArray() && next == (rune)'}')) {
                return (pos, errMismatchDelim);
            }
            return (pos, jsonwire.NewInvalidCharacterError(d.buf.slice(pos), "at start of value"));
        }}

        if (AreEqual(errΔ1, io.ErrUnexpectedEOF)) {
            var absPos = d.baseOffset + (int64)pos;
            errΔ1 = d.fetch();
            pos = (nint)(absPos - d.baseOffset);
            if (errΔ1 != nil) {
                return (pos + n, errΔ1);
            }
            continue;
        }
        return (pos + n, errΔ1);
    }
}

internal static (nint newPos, error err) consumeLiteral(this ref decoderState d, nint pos, @string lit) {
    for (;;) {
        var (n, errΔ1) = jsonwire.ConsumeLiteral(d.buf.slice(pos), lit);
        if (AreEqual(errΔ1, io.ErrUnexpectedEOF)) {
            var absPos = d.baseOffset + (int64)pos;
            errΔ1 = d.fetch();
            pos = (nint)(absPos - d.baseOffset);
            if (errΔ1 != nil) {
                return (pos + n, errΔ1);
            }
            continue;
        }
        return (pos + n, errΔ1);
    }
}

internal static (nint newPos, error err) consumeString(this ref decoderState d, ж<jsonwire.ValueFlags> flags, nint pos) {
    error err = default;

    nint n = default;
    for (;;) {
        (n, err) = jsonwire.ConsumeStringResumable(flags, d.buf.slice(pos), n, !d.Flags.Get(jsonflags.AllowInvalidUTF8));
        if (AreEqual(err, io.ErrUnexpectedEOF)) {
            var absPos = d.baseOffset + (int64)pos;
            err = d.fetch();
            pos = (nint)(absPos - d.baseOffset);
            if (err != nil) {
                return (pos + n, err);
            }
            continue;
        }
        return (pos + n, err);
    }
}

internal static (nint newPos, error err) consumeNumber(this ref decoderState d, nint pos) {
    error err = default;

    nint n = default;
    jsonwire.ConsumeNumberState state = default;
    for (;;) {
        (n, state, err) = jsonwire.ConsumeNumberResumable(d.buf.slice(pos), n, state);
        if (AreEqual(err, io.ErrUnexpectedEOF) || d.decodeBuffer.needMore(pos + n)) {
            var mayTerminate = err == nil;
            var absPos = d.baseOffset + (int64)pos;
            err = d.fetch();
            pos = (nint)(absPos - d.baseOffset);
            if (err != nil) {
                if (mayTerminate && AreEqual(err, io.ErrUnexpectedEOF)) {
                    return (pos + n, default);
                }
                return (pos, err);
            }
            continue;
        }
        return (pos + n, err);
    }
}

internal static (nint newPos, error err) consumeObject(this ж<decoderState> Ꮡd, ж<jsonwire.ValueFlags> Ꮡflags, nint pos, nint depth) {
    nint newPos = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        ref var d = ref Ꮡd.DerefOrNull();
        ref var flags = ref Ꮡflags.DerefOrNull();

        nint n = default;
        ж<objectNamespace> names = default;
        if (!d.Flags.Get(jsonflags.AllowDuplicateNames)) {
            Ꮡd.of(decoderState.ᏑNamespaces).push();
            defer(Ꮡd.of(decoderState.ᏑNamespaces).pop, ref ᒐ);
            names = d.Namespaces.Last();
        }

        if ((nuint)pos >= (nuint)len(d.buf) || d.buf[pos] != '{') {
            throw panic("BUG: consumeObject must be called with a buffer that starts with '{'");
        } else if (depth == (nint)(maxNestingDepth + 1)) {
            (newPos, err) = (pos, errMaxDepth); goto ᒐdone;
        }
        pos++;

        pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
        if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
            (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                (newPos, err) = (pos, err); goto ᒐdone;
            }
        }
        if (d.buf[pos] == '}') {
            pos++;
            (newPos, err) = (pos, default); goto ᒐdone;
        }

        depth++;
        for (;;) {
            pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
            if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
                (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                    (newPos, err) = (pos, err); goto ᒐdone;
                }
            }
            ref var flags2 = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags2);
            n = jsonwire.ConsumeSimpleString(d.buf.slice(pos)); if (n == 0) {
                var oldAbsPos = d.baseOffset + (int64)pos;
                (pos, err) = d.consumeString(Ꮡflags2, pos);
                var newAbsPos = d.baseOffset + (int64)pos;
                n = (nint)(newAbsPos - oldAbsPos);
                flags.Join(flags2);
                if (err != nil) {
                    (newPos, err) = (pos, err); goto ᒐdone;
                }
            } else {
                pos += n;
            }
            var quotedName = d.buf.slice(pos - n, pos);
            if (!d.Flags.Get(jsonflags.AllowDuplicateNames) && !names.insertQuoted(quotedName, flags2.IsVerbatim())) {
                (newPos, err) = (pos - n, wrapWithObjectName(ErrDuplicateName, quotedName)); goto ᒐdone;
            }

            pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
            if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
                (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                    (newPos, err) = (pos, wrapWithObjectName(err, quotedName)); goto ᒐdone;
                }
            }
            if (d.buf[pos] != ':') {
                var errΔ1 = jsonwire.NewInvalidCharacterError(d.buf.slice(pos), "after object name (expecting ':')");
                (newPos, err) = (pos, wrapWithObjectName(errΔ1, quotedName)); goto ᒐdone;
            }
            pos++;

            pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
            if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
                (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                    (newPos, err) = (pos, wrapWithObjectName(err, quotedName)); goto ᒐdone;
                }
            }
            (pos, err) = Ꮡd.consumeValue(Ꮡflags, pos, depth);
            if (err != nil) {
                (newPos, err) = (pos, wrapWithObjectName(err, quotedName)); goto ᒐdone;
            }

            pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
            if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
                (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                    (newPos, err) = (pos, err); goto ᒐdone;
                }
            }
            switch (d.buf[pos]) {
            case (byte)',': {
                pos++;
                continue;
                break;
            }
            case (byte)'}': {
                pos++;
                (newPos, err) = (pos, default); goto ᒐdone;
            }
            default: {
                (newPos, err) = (pos, jsonwire.NewInvalidCharacterError(d.buf.slice(pos), "after object value (expecting ',' or '}')")); goto ᒐdone;
            }}

        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (newPos, err);
}

internal static (nint newPos, error err) consumeArray(this ж<decoderState> Ꮡd, ж<jsonwire.ValueFlags> flags, nint pos, nint depth) {
    error err = default;
    ref var d = ref Ꮡd.DerefOrNull();

    if ((nuint)pos >= (nuint)len(d.buf) || d.buf[pos] != '[') {
        throw panic("BUG: consumeArray must be called with a buffer that starts with '['");
    } else if (depth == (nint)(maxNestingDepth + 1)) {
        return (pos, errMaxDepth);
    }
    pos++;

    pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
    if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
        (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
            return (pos, err);
        }
    }
    if (d.buf[pos] == ']') {
        pos++;
        return (pos, default);
    }

    int64 idx = default;
    depth++;
    for (;;) {
        pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
        if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
            (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                return (pos, err);
            }
        }
        (pos, err) = Ꮡd.consumeValue(flags, pos, depth);
        if (err != nil) {
            return (pos, wrapWithArrayIndex(err, idx));
        }

        pos += jsonwire.ConsumeWhitespace(d.buf.slice(pos));
        if (Ꮡd.of(decoderState.ᏑdecodeBuffer).needMore(pos)) {
            (pos, err) = d.consumeWhitespace(pos); if (err != nil) {
                return (pos, err);
            }
        }
        switch (d.buf[pos]) {
        case (byte)',': {
            pos++;
            idx++;
            continue;
            break;
        }
        case (byte)']': {
            pos++;
            return (pos, default);
        }
        default: {
            return (pos, jsonwire.NewInvalidCharacterError(d.buf.slice(pos), "after array element (expecting ',' or ']')"));
        }}

    }
}

} partial class jsontextᴛDecoder {

public static int64 InputOffset(this ref Decoder d) {
    return d.s.decodeBuffer.previousOffsetEnd();
}

public static slice<byte> UnreadBuffer(this ref Decoder d) {
    return d.s.decodeBuffer.unreadBuffer();
}

public static nint StackDepth(this ref Decoder d) {
    return d.s.Tokens.Depth() - 1;
}

public static (Kind, int64) StackIndex(this ж<Decoder> Ꮡd, nint i) {
    var s = Ꮡd.Ꮡs.of(decoderState.ᏑTokens).index(i);
    switch (ᐧ) {
    case {} when i > 0 && s.isObject(): {
        return ((rune)'{', s.Length());
    }
    case {} when i > 0 && s.isArray(): {
        return ((rune)'[', s.Length());
    }
    default: {
        return (0, s.Length());
    }}

}

public static Pointer StackPointer(this ref Decoder d) {
    return (Pointer)d.s.AppendStackPointer(default, -1);
}

} partial class jsontextᴛdecoderState {

public static slice<byte> AppendStackPointer(this ref decoderState d, slice<byte> b, nint where) {
    d.Names.copyQuotedBuffer(d.buf);
    return d.state.appendStackPointer(b, where);
}

} // end jsontextᴛdecoderState
