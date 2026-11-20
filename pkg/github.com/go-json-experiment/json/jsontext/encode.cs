// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using bytes = bytes_package;
using io = io_package;
using bits = go.math.bits_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using go.github.com.go_json_experiment.json.@internal;
using go.math;
using ꓸꓸꓸOptions = Span<Options>;
using static jsontext_package;

partial class jsontext_package {

partial struct Encoder {
    internal encoderState s;
}

public partial struct encoderState {
    /*embed*/ internal state state;
    /*embed*/ internal encodeBuffer encodeBuffer;
    /*embed*/ public go.github.com.go_json_experiment.json.@internal.jsonopts_package.Struct Struct;
    public map<any, EmptyStruct> SeenPointers;
}

partial struct encodeBuffer {
    public slice<byte> Buf;
    internal int64 baseOffset;
    internal io.Writer wr;
    internal nint maxValue;
    internal slice<byte> availBuffer;
    internal bufferStatistics bufStats;
}

public static ж<Encoder> NewEncoder(io.Writer w, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    var e = @new<Encoder>();
    e.Reset(w, opts.ꓸꓸꓸ);
    return e;
}

} partial class jsontextᴛEncoder {

public static void Reset(this ж<Encoder> Ꮡe, io.Writer w, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();
    ref var e = ref Ꮡe.DerefOrNull();

    switch (ᐧ) {
    case {} when Ꮡe == nil: {
        throw panic("jsontext: invalid nil Encoder");
        break;
    }
    case {} when w == nil: {
        throw panic("jsontext: invalid nil io.Writer");
        break;
    }
    case {} when e.s.Flags.Get(jsonflags.WithinArshalCall): {
        throw panic("jsontext: cannot reset Encoder passed to json.MarshalerTo");
        break;
    }}

    var b = e.s.Buf[..0];
    var (_, ok) = e.s.wr._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        b = default;
    }
    e.s.reset(b, w, opts.ꓸꓸꓸ);
}

} partial class jsontextᴛencoderState {

internal static void reset(this ref encoderState e, slice<byte> b, io.Writer w, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    e.state.reset();
    e.encodeBuffer = new encodeBuffer(Buf: b, wr: w, availBuffer: e.availBuffer, bufStats: e.bufStats);
    var (bb, ok) = w._<ж<bytes.Buffer>>(ᐧ); if (ok && bb != nil) {
        e.Buf = bb.AvailableBuffer();
    }
    var opts2 = new jsonopts.Struct();
    opts2.Join(opts.ꓸꓸꓸ);
    e.Struct = opts2;
    if (e.Struct.Flags.Get(jsonflags.Multiline)) {
        e.Struct.InitializeMultiline();
    }
}

} partial class jsontextᴛEncoder {

public static Options Options(this ж<Encoder> Ꮡe) {
    return new jsonopts.StructжOptions(Ꮡe.Ꮡs.ᏑStruct);
}

} partial class jsontextᴛencoderState {

internal static ж<jsonopts.Struct> options(this ж<encoderState> Ꮡe) { return Ꮡe.ᏑStruct; }

public static bool NeedFlush(this ref encoderState e) {
    return e.wr != nil && (e.Tokens.Depth() == 1 || len(e.Buf) > 3 * cap(e.Buf) / 4);
}

public static error Flush(this ref encoderState e) {
    if (e.wr == nil || e.avoidFlush()) {
        return default;
    }

    if (e.Tokens.Depth() == 1 && !e.Flags.Get(jsonflags.OmitTopLevelNewline)) {
        e.Buf = append(e.Buf, (byte)'\n');
    }

    e.Names.copyQuotedBuffer(e.Buf);

    var (bb, ok) = e.wr._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        var (nΔ1, _) = bb.Write(e.Buf);
        e.baseOffset += (int64)nΔ1;

        nint avail = bb.Available(); if (avail < bb.Len() / 4) {
            bb.Grow(avail + 1);
        }

        e.Buf = bb.AvailableBuffer();
        return default;
    }

    var (n, err) = e.wr.Write(e.Buf);
    e.baseOffset += (int64)n;
    if (err != nil) {
        if (n > 0) {
            e.Buf = e.Buf.slice(0, copy(e.Buf, e.Buf.slice(n)));
        }
        return new ioErrorжerror(Ꮡ(new ioError(action: "write", err: err)));
    }
    e.Buf = e.Buf[..0];

    UntypedInt maxBufferSize = /* 4 << 10 */ 4096;
    UntypedInt growthSizeFactor = 2;
    const int64 growthRateFactor = 2;
    var grow = cap(e.Buf) <= (nint)(maxBufferSize / growthSizeFactor);
    grow = grow && (int64)cap(e.Buf) < e.encodeBuffer.previousOffsetEnd() / growthRateFactor;
    if (grow) {
        e.Buf = new slice<byte>(0, cap(e.Buf) * (nint)growthSizeFactor);
    }

    return default;
}

} partial class jsontextᴛencodeBuffer {

internal static int64 offsetAt(this ref encodeBuffer e, nint pos) { return e.baseOffset + (int64)pos; }
internal static int64 previousOffsetEnd(this ref encodeBuffer e) { return e.baseOffset + (int64)len(e.Buf); }
internal static slice<byte> unflushedBuffer(this ref encodeBuffer e) { return e.Buf; }

} partial class jsontextᴛencoderState {

internal static bool avoidFlush(this ref encoderState e) {
    switch (ᐧ) {
    case {} when e.Tokens.Last.Length() is 0: {
        return true;
    }
    case {} when e.Tokens.Last.needObjectValue(): {
        return true;
    }
    case {} when e.Tokens.Last.NeedObjectName() && len(e.Buf) >= 2: {
        var exprᴛ1 = ((sstring)(e.Buf.slice(len(e.Buf) - 2)));
        switch (ᐧ) {
        case {} when exprᴛ1 == @"ll"u8 || exprᴛ1 == @""""""u8 || exprᴛ1 == @"{}"u8 || exprᴛ1 == @"[]"u8: {
            return true;
        }}

        break;
    }}

    return false;
}

public static bool UnwriteEmptyObjectMember(this ref encoderState e, ж<@string> ᏑprevName) {
    ref var prevName = ref ᏑprevName.DerefOrNull();

    var last = e.Tokens.Last; if (!last.isObject() || !last.NeedObjectName() || last.Length() == 0) {
        throw panic("BUG: must be called on an object after writing a value");
    }

    var b = e.encodeBuffer.unflushedBuffer();

    nint n = default;
    if (len(b) >= 3) {
        var exprᴛ1 = ((sstring)(b.slice(len(b) - 2)));
        switch (ᐧ) {
        case {} when exprᴛ1 == "ll"u8: {
            n = len(@"null");
            break;
        }
        case {} when exprᴛ1 == @""""""u8: {
            if (b[len(b) - 3] == '\\') {
                return false;
            }
            n = len(@"""""");
            break;
        }
        case {} when exprᴛ1 == @"{}"u8: {
            n = len(@"{}");
            break;
        }
        case {} when exprᴛ1 == @"[]"u8: {
            n = len(@"[]");
            break;
        }}

    }
    if (n == 0) {
        return false;
    }

    b = b.slice(0, len(b) - n);
    b = jsonwire.TrimSuffixWhitespace(b);
    b = jsonwire.TrimSuffixByte(b, (byte)':');
    b = jsonwire.TrimSuffixString(b);
    b = jsonwire.TrimSuffixWhitespace(b);
    b = jsonwire.TrimSuffixByte(b, (byte)',');
    e.Buf = b;

    e.Tokens.Last.decrement();
    e.Tokens.Last.decrement();
    if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
        if (e.Tokens.Last.isActiveNamespace()) {
            e.Namespaces.Last().removeLast();
        }
    }
    e.Names.clearLast();
    if (ᏑprevName != nil) {
        e.Names.copyQuotedBuffer(e.Buf);
        e.Names.replaceLastUnquotedName(prevName);
    }
    return true;
}

public static @string UnwriteOnlyObjectMemberName(this ref encoderState e) {
    var last = e.Tokens.Last; if (!last.isObject() || last.Length() != 1) {
        throw panic("BUG: must be called on an object after writing first name");
    }

    var b = jsonwire.TrimSuffixString(e.Buf);
    var isVerbatim = bytes.IndexByte(e.Buf.slice(len(b)), (byte)'\\') < 0;
    @string name = (@string)jsonwire.UnquoteMayCopy(e.Buf.slice(len(b)), isVerbatim);
    e.Buf = jsonwire.TrimSuffixWhitespace(b);

    e.Tokens.Last.decrement();
    if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
        if (e.Tokens.Last.isActiveNamespace()) {
            e.Namespaces.Last().removeLast();
        }
    }
    e.Names.clearLast();
    return name;
}

} partial class jsontextᴛEncoder {

public static error WriteToken(this ж<Encoder> Ꮡe, Token t) {
    return Ꮡe.Ꮡs.WriteToken(t);
}

} partial class jsontextᴛencoderState {

public static error WriteToken(this ж<encoderState> Ꮡe, Token t) {
    ref var e = ref Ꮡe.DerefOrNull();

    var k = t.Kind();
    var b = e.Buf;

    b = e.Tokens.MayAppendDelim(b, k);
    if (e.Flags.Get(jsonflags.AnyWhitespace)) {
        b = e.appendWhitespace(b, k);
    }
    nint pos = len(b);

    error err = default;
    switch (k) {
    case var ᴛ when ᴛ == (Kind)((rune)'n'): {
        b = append(b, ((@string)"null").ꓸꓸꓸ);
        err = e.Tokens.appendLiteral();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'f'): {
        b = append(b, ((@string)"false").ꓸꓸꓸ);
        err = e.Tokens.appendLiteral();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'t'): {
        b = append(b, ((@string)"true").ꓸꓸꓸ);
        err = e.Tokens.appendLiteral();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'"'): {
        (b, err) = t.appendString(b, Ꮡe.of(encoderState.ᏑFlags)); if (err != nil) {
            break;
        }
        if (e.Tokens.Last.NeedObjectName()) {
            if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
                if (!e.Tokens.Last.isValidNamespace()) {
                    err = errInvalidNamespace;
                    break;
                }
                if (e.Tokens.Last.isActiveNamespace() && !e.Namespaces.Last().insertQuoted(b.slice(pos), false)) {
                    err = wrapWithObjectName(ErrDuplicateName, b.slice(pos));
                    break;
                }
            }
            e.Names.ReplaceLastQuotedOffset(pos);
        }
        err = e.Tokens.appendString();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'0'): {
        (b, err) = t.appendNumber(b, Ꮡe.of(encoderState.ᏑFlags)); if (err != nil) {
            break;
        }
        err = e.Tokens.appendNumber();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'{'): {
        b = append(b, (byte)'{');
        err = e.Tokens.pushObject(); if (err != nil) {
            break;
        }
        e.Names.push();
        if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
            Ꮡe.of(encoderState.ᏑNamespaces).push();
        }
        e.Flags.Clear(jsonflags.TagFlags);
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'}'): {
        b = append(b, (byte)'}');
        err = e.Tokens.popObject(); if (err != nil) {
            break;
        }
        e.Names.pop();
        if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
            e.Namespaces.pop();
        }
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'['): {
        b = append(b, (byte)'[');
        err = e.Tokens.pushArray();
        e.Flags.Clear(jsonflags.TagFlags);
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)']'): {
        b = append(b, (byte)']');
        err = e.Tokens.popArray();
        break;
    }
    default: {
        err = errInvalidToken;
        break;
    }}

    if (err != nil) {
        return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), err, pos, +1);
    }

    e.Buf = b;
    if (e.NeedFlush()) {
        return e.Flush();
    }
    return default;
}

public static error AppendRaw(this ж<encoderState> Ꮡe, Kind k, bool safeASCII, Func<slice<byte>, (slice<byte>, error)> appendFn) {
    ref var e = ref Ꮡe.DerefOrNull();

    var b = e.Buf;

    b = e.Tokens.MayAppendDelim(b, k);
    if (e.Flags.Get(jsonflags.AnyWhitespace)) {
        b = e.appendWhitespace(b, k);
    }
    nint pos = len(b);

    error err = default;
    switch (k) {
    case var ᴛ when ᴛ == (Kind)((rune)'"'): {
        b = append(b, (byte)'"');
        (b, err) = appendFn(b); if (err != nil) {
            return err;
        }
        b = append(b, (byte)'"');

        var isVerbatim = safeASCII || !jsonwire.NeedEscape(b.slice(pos + len(@""""), len(b) - len(@"""")));
        if (!isVerbatim) {
            error errΔ4 = default;
            var b2 = appendꓸꓸꓸ(e.availBuffer, b.slice(pos + len(@""""), len(b) - len(@"""")));
            (b, errΔ4) = jsonwire.AppendQuote(b.slice(0, pos), b2, Ꮡe.of(encoderState.ᏑFlags));
            e.availBuffer = b2[..0];
            if (errΔ4 != nil) {
                return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), errΔ4, pos, +1);
            }
        }

        if (e.Tokens.Last.NeedObjectName()) {
            if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
                if (!e.Tokens.Last.isValidNamespace()) {
                    return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), errInvalidNamespace, pos, +1);
                }
                if (e.Tokens.Last.isActiveNamespace() && !e.Namespaces.Last().insertQuoted(b.slice(pos), isVerbatim)) {
                    err = wrapWithObjectName(ErrDuplicateName, b.slice(pos));
                    return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), err, pos, +1);
                }
            }
            e.Names.ReplaceLastQuotedOffset(pos);
        }
        var errΔ5 = e.Tokens.appendString(); if (errΔ5 != nil) {
            return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), errΔ5, pos, +1);
        }
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'0'): {
        (b, err) = appendFn(b); if (err != nil) {
            return err;
        }
        var errΔ6 = e.Tokens.appendNumber(); if (errΔ6 != nil) {
            return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), errΔ6, pos, +1);
        }
        break;
    }
    default: {
        throw panic("BUG: invalid kind");
        break;
    }}

    e.Buf = b;
    if (e.NeedFlush()) {
        return e.Flush();
    }
    return default;
}

} partial class jsontextᴛEncoder {

public static error WriteValue(this ж<Encoder> Ꮡe, Value v) {
    return Ꮡe.Ꮡs.WriteValue(v);
}

} partial class jsontextᴛencoderState {

public static error WriteValue(this ж<encoderState> Ꮡe, Value v) {
    ref var e = ref Ꮡe.DerefOrNull();

    e.maxValue |= (nint)(len(v));

    var k = v.Kind();
    var b = e.Buf;

    b = e.Tokens.MayAppendDelim(b, k);
    if (e.Flags.Get(jsonflags.AnyWhitespace)) {
        b = e.appendWhitespace(b, k);
    }
    nint pos = len(b);

    nint n = default;
    n += jsonwire.ConsumeWhitespace(v.slice(n));
    (b, var m, var err) = Ꮡe.reformatValue(b, v.slice(n), e.Tokens.Depth());
    if (err != nil) {
        return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), err, pos + n + m, +1);
    }
    n += m;
    n += jsonwire.ConsumeWhitespace(v.slice(n));
    if (len(v) > n) {
        err = jsonwire.NewInvalidCharacterError(v.slice(n), "after top-level value");
        return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), err, pos + n, 0);
    }

    switch (k) {
    case var ᴛ when ᴛ == (Kind)((rune)'n') || ᴛ == (Kind)((rune)'f') || ᴛ == (Kind)((rune)'t'): {
        err = e.Tokens.appendLiteral();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'"'): {
        if (e.Tokens.Last.NeedObjectName()) {
            if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
                if (!e.Tokens.Last.isValidNamespace()) {
                    err = errInvalidNamespace;
                    break;
                }
                if (e.Tokens.Last.isActiveNamespace() && !e.Namespaces.Last().insertQuoted(b.slice(pos), false)) {
                    err = wrapWithObjectName(ErrDuplicateName, b.slice(pos));
                    break;
                }
            }
            e.Names.ReplaceLastQuotedOffset(pos);
        }
        err = e.Tokens.appendString();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'0'): {
        err = e.Tokens.appendNumber();
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'{'): {
        err = e.Tokens.pushObject(); if (err != nil) {
            break;
        }
        err = e.Tokens.popObject(); if (err != nil) {
            throw panic("BUG: popObject should never fail immediately after pushObject: " + err.Error());
        }
        if (e.Flags.Get(jsonflags.ReorderRawObjects)) {
            mustReorderObjects(b.slice(pos));
        }
        break;
    }
    case var ᴛ when ᴛ == (Kind)((rune)'['): {
        err = e.Tokens.pushArray(); if (err != nil) {
            break;
        }
        err = e.Tokens.popArray(); if (err != nil) {
            throw panic("BUG: popArray should never fail immediately after pushArray: " + err.Error());
        }
        if (e.Flags.Get(jsonflags.ReorderRawObjects)) {
            mustReorderObjects(b.slice(pos));
        }
        break;
    }}

    if (err != nil) {
        return wrapSyntacticError(new encoderStateжwrapSyntacticError_state(Ꮡe), err, pos, +1);
    }

    e.Buf = b;
    if (e.NeedFlush()) {
        return e.Flush();
    }
    return default;
}

public static nint /*n*/ CountNextDelimWhitespace(this ref encoderState e) {
    nint n = default;

    Kind next = /* Kind('"') */ 34;
    var delim = e.Tokens.needDelim(next);
    if (delim > 0) {
        n += (nint)((nint)len(",") | (nint)len(":"));
    }
    if (delim == ':') {
        if (e.Flags.Get(jsonflags.SpaceAfterColon)) {
            n += len(" ");
        }
    } else {
        if (delim == ',' && e.Flags.Get(jsonflags.SpaceAfterComma)) {
            n += len(" ");
        }
        if (e.Flags.Get(jsonflags.Multiline)) {
            nint m = e.Tokens.NeedIndent(next); if (m > 0) {
                n += len("\n") + len(e.IndentPrefix) + (m - 1) * len(e.Indent);
            }
        }
    }
    return n;
}

internal static slice<byte> appendWhitespace(this ref encoderState e, slice<byte> b, Kind next) {
    var delim = e.Tokens.needDelim(next); if (delim == ':') {
        if (e.Flags.Get(jsonflags.SpaceAfterColon)) {
            b = append(b, (byte)' ');
        }
    } else {
        if (delim == ',' && e.Flags.Get(jsonflags.SpaceAfterComma)) {
            b = append(b, (byte)' ');
        }
        if (e.Flags.Get(jsonflags.Multiline)) {
            b = e.AppendIndent(b, e.Tokens.NeedIndent(next));
        }
    }
    return b;
}

public static slice<byte> AppendIndent(this ref encoderState e, slice<byte> b, nint n) {
    if (n == 0) {
        return b;
    }
    b = append(b, (byte)'\n');
    b = append(b, e.IndentPrefix.ꓸꓸꓸ);
    for (; n > 1; n--) {
        b = append(b, e.Indent.ꓸꓸꓸ);
    }
    return b;
}

internal static (slice<byte>, nint, error) reformatValue(this ж<encoderState> Ꮡe, slice<byte> dst, Value src, nint depth) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (len(src) == 0) {
        return (dst, 0, io.ErrUnexpectedEOF);
    }
    var k = ((Kind)src[0]).normalize();
    switch (k) {
    case var ᴛ when ᴛ == (Kind)((rune)'n'): {
        if (jsonwire.ConsumeNull(src) == 0) {
            var (n, err) = jsonwire.ConsumeLiteral(src, "null");
            return (dst, n, err);
        }
        return (append(dst, ((@string)"null").ꓸꓸꓸ), len("null"), default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'f'): {
        if (jsonwire.ConsumeFalse(src) == 0) {
            var (n, err) = jsonwire.ConsumeLiteral(src, "false");
            return (dst, n, err);
        }
        return (append(dst, ((@string)"false").ꓸꓸꓸ), len("false"), default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'t'): {
        if (jsonwire.ConsumeTrue(src) == 0) {
            var (n, err) = jsonwire.ConsumeLiteral(src, "true");
            return (dst, n, err);
        }
        return (append(dst, ((@string)"true").ꓸꓸꓸ), len("true"), default);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'"'): {
        {
            nint n = jsonwire.ConsumeSimpleString(src); if (n > 0) {
                dst = appendꓸꓸꓸ(dst, src.slice(0, n));
                return (dst, n, default);
            }
        }
        return jsonwire.ReformatString(dst, src, Ꮡe.of(encoderState.ᏑFlags));
    }
    case var ᴛ when ᴛ == (Kind)((rune)'0'): {
        {
            nint n = jsonwire.ConsumeSimpleNumber(src); if (n > 0 && !e.Flags.Get(jsonflags.CanonicalizeNumbers)) {
                dst = appendꓸꓸꓸ(dst, src.slice(0, n));
                return (dst, n, default);
            }
        }
        return jsonwire.ReformatNumber(dst, src, Ꮡe.of(encoderState.ᏑFlags));
    }
    case var ᴛ when ᴛ == (Kind)((rune)'{'): {
        return Ꮡe.reformatObject(dst, src, depth);
    }
    case var ᴛ when ᴛ == (Kind)((rune)'['): {
        return Ꮡe.reformatArray(dst, src, depth);
    }
    default: {
        return (dst, 0, jsonwire.NewInvalidCharacterError(src, "at start of value"));
    }}

}

internal static (slice<byte>, nint, error) reformatObject(this ж<encoderState> Ꮡe, slice<byte> dst, Value src, nint depth) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var e = ref Ꮡe.DerefOrNull();

        if (len(src) == 0 || src[0] != '{') {
            throw panic("BUG: reformatObject must be called with a buffer that starts with '{'");
        } else if (depth == (nint)(maxNestingDepth + 1)) {
            return (dst, 0, errMaxDepth);
        }
        dst = append(dst, (byte)'{');
        nint n = len("{");

        n += jsonwire.ConsumeWhitespace(src.slice(n));
        if ((nuint)len(src) <= (nuint)n) {
            return (dst, n, io.ErrUnexpectedEOF);
        }
        if (src[n] == '}') {
            dst = append(dst, (byte)'}');
            n += len("}");
            return (dst, n, default);
        }

        error err = default;
        ж<objectNamespace> names = default;
        if (!e.Flags.Get(jsonflags.AllowDuplicateNames)) {
            Ꮡe.of(encoderState.ᏑNamespaces).push();
            ᒐd1 = true;
            names = e.Namespaces.Last();
        }
        depth++;
        for (;;) {
            if (e.Flags.Get(jsonflags.Multiline)) {
                dst = e.AppendIndent(dst, depth);
            }

            n += jsonwire.ConsumeWhitespace(src.slice(n));
            if ((nuint)len(src) <= (nuint)n) {
                return (dst, n, io.ErrUnexpectedEOF);
            }
            nint m = jsonwire.ConsumeSimpleString(src.slice(n));
            var isVerbatim = m > 0;
            if (isVerbatim) {
                dst = appendꓸꓸꓸ(dst, src.slice(n, n + m));
            } else {
                (dst, m, err) = jsonwire.ReformatString(dst, src.slice(n), Ꮡe.of(encoderState.ᏑFlags));
                if (err != nil) {
                    return (dst, n + m, err);
                }
            }
            var quotedName = src.slice(n, n + m);
            if (!e.Flags.Get(jsonflags.AllowDuplicateNames) && !names.insertQuoted(quotedName, isVerbatim)) {
                return (dst, n, wrapWithObjectName(ErrDuplicateName, quotedName));
            }
            n += m;

            n += jsonwire.ConsumeWhitespace(src.slice(n));
            if ((nuint)len(src) <= (nuint)n) {
                return (dst, n, wrapWithObjectName(io.ErrUnexpectedEOF, quotedName));
            }
            if (src[n] != ':') {
                err = jsonwire.NewInvalidCharacterError(src.slice(n), "after object name (expecting ':')");
                return (dst, n, wrapWithObjectName(err, quotedName));
            }
            dst = append(dst, (byte)':');
            n += len(":");
            if (e.Flags.Get(jsonflags.SpaceAfterColon)) {
                dst = append(dst, (byte)' ');
            }

            n += jsonwire.ConsumeWhitespace(src.slice(n));
            if ((nuint)len(src) <= (nuint)n) {
                return (dst, n, wrapWithObjectName(io.ErrUnexpectedEOF, quotedName));
            }
            (dst, m, err) = Ꮡe.reformatValue(dst, src.slice(n), depth);
            if (err != nil) {
                return (dst, n + m, wrapWithObjectName(err, quotedName));
            }
            n += m;

            n += jsonwire.ConsumeWhitespace(src.slice(n));
            if ((nuint)len(src) <= (nuint)n) {
                return (dst, n, io.ErrUnexpectedEOF);
            }
            switch (src[n]) {
            case (byte)',': {
                dst = append(dst, (byte)',');
                if (e.Flags.Get(jsonflags.SpaceAfterComma)) {
                    dst = append(dst, (byte)' ');
                }
                n += len(",");
                continue;
                break;
            }
            case (byte)'}': {
                if (e.Flags.Get(jsonflags.Multiline)) {
                    dst = e.AppendIndent(dst, depth - 1);
                }
                dst = append(dst, (byte)'}');
                n += len("}");
                return (dst, n, default);
            }
            default: {
                return (dst, n, jsonwire.NewInvalidCharacterError(src.slice(n), "after object value (expecting ',' or '}')"));
            }}

        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡe.DerefOrNull().Namespaces.pop(); ᒐ.Run(); }
}

internal static (slice<byte>, nint, error) reformatArray(this ж<encoderState> Ꮡe, slice<byte> dst, Value src, nint depth) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (len(src) == 0 || src[0] != '[') {
        throw panic("BUG: reformatArray must be called with a buffer that starts with '['");
    } else if (depth == (nint)(maxNestingDepth + 1)) {
        return (dst, 0, errMaxDepth);
    }
    dst = append(dst, (byte)'[');
    nint n = len("[");

    n += jsonwire.ConsumeWhitespace(src.slice(n));
    if ((nuint)len(src) <= (nuint)n) {
        return (dst, n, io.ErrUnexpectedEOF);
    }
    if (src[n] == ']') {
        dst = append(dst, (byte)']');
        n += len("]");
        return (dst, n, default);
    }

    int64 idx = default;
    error err = default;
    depth++;
    for (;;) {
        if (e.Flags.Get(jsonflags.Multiline)) {
            dst = e.AppendIndent(dst, depth);
        }

        n += jsonwire.ConsumeWhitespace(src.slice(n));
        if ((nuint)len(src) <= (nuint)n) {
            return (dst, n, io.ErrUnexpectedEOF);
        }
        nint m = default;
        (dst, m, err) = Ꮡe.reformatValue(dst, src.slice(n), depth);
        if (err != nil) {
            return (dst, n + m, wrapWithArrayIndex(err, idx));
        }
        n += m;

        n += jsonwire.ConsumeWhitespace(src.slice(n));
        if ((nuint)len(src) <= (nuint)n) {
            return (dst, n, io.ErrUnexpectedEOF);
        }
        switch (src[n]) {
        case (byte)',': {
            dst = append(dst, (byte)',');
            if (e.Flags.Get(jsonflags.SpaceAfterComma)) {
                dst = append(dst, (byte)' ');
            }
            n += len(",");
            idx++;
            continue;
            break;
        }
        case (byte)']': {
            if (e.Flags.Get(jsonflags.Multiline)) {
                dst = e.AppendIndent(dst, depth - 1);
            }
            dst = append(dst, (byte)']');
            n += len("]");
            return (dst, n, default);
        }
        default: {
            return (dst, n, jsonwire.NewInvalidCharacterError(src.slice(n), "after array value (expecting ',' or ']')"));
        }}

    }
}

} partial class jsontextᴛEncoder {

public static int64 OutputOffset(this ref Encoder e) {
    return e.s.encodeBuffer.previousOffsetEnd();
}

public static slice<byte> AvailableBuffer(this ref Encoder e) {
    nint n = ((nint)1).Lsh((int64)(bits.Len((nuint)(e.s.maxValue | 63))));
    if (cap(e.s.availBuffer) < n) {
        e.s.availBuffer = new slice<byte>(0, n);
    }
    return e.s.availBuffer;
}

public static nint StackDepth(this ref Encoder e) {
    return e.s.Tokens.Depth() - 1;
}

public static (Kind, int64) StackIndex(this ж<Encoder> Ꮡe, nint i) {
    var s = Ꮡe.Ꮡs.of(encoderState.ᏑTokens).index(i);
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

public static Pointer StackPointer(this ref Encoder e) {
    return (Pointer)e.s.AppendStackPointer(default, -1);
}

} partial class jsontextᴛencoderState {

public static slice<byte> AppendStackPointer(this ref encoderState e, slice<byte> b, nint where) {
    e.Names.copyQuotedBuffer(e.Buf);
    return e.state.appendStackPointer(b, where);
}

} // end jsontextᴛencoderState
