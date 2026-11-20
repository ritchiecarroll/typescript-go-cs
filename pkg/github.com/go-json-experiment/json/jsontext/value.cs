// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using bytes = bytes_package;
using errors = errors_package;
using io = io_package;
using slices = slices_package;
using sync = sync_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using go.github.com.go_json_experiment.json.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using ꓸꓸꓸOptions = Span<Options>;
using static jsontext_package;

partial class jsontext_package {

public static slice<byte> AppendFloat(slice<byte> dst, float64 src, nint bits) {
    if (bits != 32 && bits != 64) {
        throw panic("illegal AppendFloat bit size");
    }
    return jsonwire.AppendFloat(dst, src, bits);
}

public static (slice<byte>, error) AppendFormat<Bytes>(slice<byte> dst, Bytes src, params ꓸꓸꓸOptions optsʗp)
    where Bytes : /* ~[]byte | ~string */ IByteSeq<Bytes, byte>, new() {
    GoFrame ᒐ = default;
    try
    {
        var opts = optsʗp.sslice();

        var e = getBufferedEncoder(opts.ꓸꓸꓸ);
        defer(putBufferedEncoder, e, ref ᒐ);
        e.Ꮡs.of(encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.OmitTopLevelNewline | 1));
        var err = e.Ꮡs.WriteValue((Value)src.ToSlice()); if (err != nil) {
            return (append(dst, src.ꓸꓸꓸ), err);
        }
        return (appendꓸꓸꓸ(dst, e.s.Buf), default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

partial struct Value /*[]byte*/;

} partial class jsontextᴛValue {

public static Value Clone(this Value v) {
    return bytes.Clone(v);
}

public static @string String(this Value v) {
    if (v == nil) {
        return "null";
    }
    return (@string)(slice<byte>)v;
}

public static bool IsValid(this Value v, params ꓸꓸꓸOptions optsʗp) {
    GoFrame ᒐ = default;
    try {
        var opts = optsʗp.sslice();

        var d = getBufferedDecoder(v, opts.ꓸꓸꓸ);
        defer(putBufferedDecoder, d, ref ᒐ);
        var (_, errVal) = d.ReadValue();
        var (_, errEOF) = d.ReadToken();
        return errVal == nil && AreEqual(errEOF, io.EOF);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static error Format(this ж<Value> Ꮡv, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.slice();

    return Ꮡv.format(opts, default);
}

internal static error format(this ж<Value> Ꮡv, slice<Options> opts1, slice<Options> opts2) {
    GoFrame ᒐ = default;
    try {
        ref var v = ref Ꮡv.DerefOrNull();

        var e = getBufferedEncoder(opts1.ꓸꓸꓸ);
        defer(putBufferedEncoder, e, ref ᒐ);
        e.Ꮡs.of(encoderState.ᏑStruct).Join(opts2.ꓸꓸꓸ);
        e.Ꮡs.of(encoderState.ᏑFlags).Set((jsonflags.Bools)(jsonflags.OmitTopLevelNewline | 1));
        var err = e.Ꮡs.WriteValue(v); if (err != nil) {
            return err;
        }
        if (!bytes.Equal(v, e.s.Buf)) {
            v = appendꓸꓸꓸ((v)[..0], e.s.Buf);
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static error Compact(this ж<Value> Ꮡv, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.slice();

    return Ꮡv.format(slice<Options>([AllowDuplicateNames(true), AllowInvalidUTF8(true), PreserveRawStrings(true)
    ]), opts);
}

public static error Indent(this ж<Value> Ꮡv, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.slice();

    return Ꮡv.format(slice<Options>([AllowDuplicateNames(true), AllowInvalidUTF8(true), PreserveRawStrings(true), Multiline(true)
    ]), opts);
}

public static error Canonicalize(this ж<Value> Ꮡv, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.slice();

    return Ꮡv.format(slice<Options>([CanonicalizeRawInts(true), CanonicalizeRawFloats(true), ReorderRawObjects(true)
    ]), opts);
}

public static (slice<byte>, error) MarshalJSON(this Value v) {
    if (v == nil) {
        return (slice<byte>("null"u8), default);
    }
    return (v, default);
}

public static error UnmarshalJSON(this ж<Value> Ꮡv, slice<byte> b) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (Ꮡv == nil) {
        return errors.New("jsontext.Value: UnmarshalJSON on nil pointer");
    }
    v = appendꓸꓸꓸ((v)[..0], b);
    return default;
}

public static Kind Kind(this Value v) {
    var vΔ1 = v.slice(jsonwire.ConsumeWhitespace(v)); if (len(vΔ1) > 0) {
        return ((Kind)vΔ1[0]).normalize();
    }
    return invalidKind;
}

} partial class jsontext_package {

internal static readonly @string commaAndWhitespace = ", \n\r\t"u8;

partial struct objectMember {
    internal slice<byte> name;
    internal slice<byte> buffer;
}

} partial class jsontextᴛobjectMember {

internal static nint Compare(this objectMember x, objectMember y) {
    nint c = jsonwire.CompareUTF16(x.name, y.name); if (c != 0) {
        return c;
    }
    return jsonwire.CompareUTF16(
        bytes.TrimLeft(x.buffer, commaAndWhitespace),
        bytes.TrimLeft(y.buffer, commaAndWhitespace));
}

} partial class jsontext_package {

internal static readonly ж<sync.Pool> ᏑobjectMemberPool = new StaticBox<sync.Pool>(new sync.Pool(New: () => @new<slice<objectMember>>()));
internal static ref sync.Pool objectMemberPool => ref ((StaticBox<sync.Pool>)ᏑobjectMemberPool).ValueSlot;

internal static ж<slice<objectMember>> getObjectMembers() {
    var ns = ᏑobjectMemberPool.Get()._<ж<slice<objectMember>>>();
    ns.ValueSlot = (ns.ValueSlot)[..0];
    return ns;
}
internal static void putObjectMembers(ж<slice<objectMember>> Ꮡns) {
    ref var ns = ref Ꮡns.DerefOrNull();

    if (cap(ns) < 1 << 10) {
        clear(ns);
        ᏑobjectMemberPool.Put(Ꮡns.OrTypedNil());
    }
}

internal static void mustReorderObjects(slice<byte> b) {
    GoFrame ᒐ = default;
    try {
        var e2 = getBufferedEncoder();
        defer(putBufferedEncoder, e2, ref ᒐ);

        var d = getBufferedDecoder(b);
        defer(putBufferedDecoder, d, ref ᒐ);
        d.Ꮡs.of(decoderState.ᏑFlags).Set((jsonflags.Bools)((jsonflags.Bools)(jsonflags.AllowDuplicateNames | jsonflags.AllowInvalidUTF8) | 1));
        mustReorderObjectsFromDecoder(d, ref (e2.Ꮡs.of(encoderState.ᏑBuf)).DerefOrNull());
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static void mustReorderObjectsFromDecoder(ж<Decoder> Ꮡd, ref slice<byte> scratch) {
    GoFrame ᒐ = default;
    try {
        ref var d = ref Ꮡd.DerefOrNull();

        {
            var (tok, err) = Ꮡd.ReadToken();
            switch (tok.Kind()) {
            case var ᴛ when ᴛ == (Kind)((rune)'{'): {
                var members = getObjectMembers();
                defer(putObjectMembers, members, ref ᒐ);
                objectMember prevMember = default;
                var isSorted = true;

                var beforeBody = d.InputOffset();
                while (Ꮡd.PeekKind() != (rune)'}') {
                    var beforeName = d.InputOffset();
                    ref var flags = ref heap(new jsonwire.ValueFlags(), out var Ꮡflags);
                    var (name, _) = Ꮡd.Ꮡs.ReadValue(Ꮡflags);
                    name = jsonwire.UnquoteMayCopy(name, flags.IsVerbatim());
                    mustReorderObjectsFromDecoder(Ꮡd, ref scratch);
                    var afterValue = d.InputOffset();

                    var currMember = new objectMember(name, d.s.buf.slice((nint)(beforeName), (nint)(afterValue)));
                    if (isSorted && len(members.ValueSlot) > 0) {
                        isSorted = ((Func<objectMember, objectMember, nint>)(jsontextᴛobjectMember.Compare))(prevMember, currMember) < 0;
                    }
                    members.ValueSlot = append(members.ValueSlot, currMember);
                    prevMember = currMember;
                }
                var afterBody = d.InputOffset();
                Ꮡd.ReadToken();

                if (isSorted) {
                    return;
                }
                var firstBufferBeforeSorting = (members.ValueSlot)[0].buffer;
                slices.SortFunc<slice<objectMember>, objectMember>(members.ValueSlot, ((Func<objectMember, objectMember, nint>)(jsontextᴛobjectMember.Compare)));
                var firstBufferAfterSorting = (members.ValueSlot)[0].buffer;

                slice<byte> commaAndWhitespacePrefix(slice<byte> b) => b.slice(0, len(b) - len(bytes.TrimLeft(b, commaAndWhitespace)));
                var sorted = (scratch)[..0];
                foreach (var (i, member) in range(members.ValueSlot)) {
                    switch (ᐧ) {
                    case {} when i == 0 && Ꮡelem(member.buffer, 0) != Ꮡelem(firstBufferBeforeSorting, 0): {
                        sorted = appendꓸꓸꓸ(sorted, commaAndWhitespacePrefix(firstBufferBeforeSorting));
                        sorted = appendꓸꓸꓸ(sorted, bytes.TrimLeft(member.buffer, commaAndWhitespace));
                        break;
                    }
                    case {} when i != 0 && Ꮡelem(member.buffer, 0) == Ꮡelem(firstBufferBeforeSorting, 0): {
                        sorted = appendꓸꓸꓸ(sorted, commaAndWhitespacePrefix(firstBufferAfterSorting));
                        sorted = appendꓸꓸꓸ(sorted, bytes.TrimLeft(member.buffer, commaAndWhitespace));
                        break;
                    }
                    default: {
                        sorted = appendꓸꓸꓸ(sorted, member.buffer);
                        break;
                    }}

                }
                if ((nint)(afterBody - beforeBody) != len(sorted)) {
                    throw panic("BUG: length invariant violated");
                }
                copy(d.s.buf.slice((nint)(beforeBody), (nint)(afterBody)), sorted);

                if (len(sorted) > len(scratch)) {
                    scratch = sorted;
                }
                break;
            }
            case var ᴛ when ᴛ == (Kind)((rune)'['): {
                while (Ꮡd.PeekKind() != (rune)']') {
                    mustReorderObjectsFromDecoder(Ꮡd, ref scratch);
                }
                Ꮡd.ReadToken();
                break;
            }
            default: {
                if (err != nil) {
                    throw panic("BUG: " + err.Error());
                }
                break;
            }}
        }

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} // end jsontext_package
