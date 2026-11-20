// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using bytes = bytes_package;
using io = io_package;
using bits = go.math.bits_package;
using sync = sync_package;
using go.math;
using ꓸꓸꓸOptions = Span<Options>;

partial class jsontext_package {

internal static ж<sync.Pool> bufferedEncoderPool = Ꮡ(new sync.Pool(New: () => @new<Encoder>()));
internal static ж<sync.Pool> streamingEncoderPool = Ꮡ(new sync.Pool(New: () => @new<Encoder>()));
internal static ж<sync.Pool> bytesBufferEncoderPool = Ꮡ(new sync.Pool(New: () => @new<Encoder>()));

partial struct bufferStatistics {
    internal nint strikes;
    internal nint prevLen;
}

internal static ж<Encoder> getBufferedEncoder(params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    var e = bufferedEncoderPool.Get()._<ж<Encoder>>();
    if (e.s.Buf == nil) {
        nint n = ((nint)1).Lsh((int64)(bits.Len((nuint)(e.s.bufStats.prevLen | 63))));
        e.s.Buf = new slice<byte>(0, n);
    }
    e.Ꮡs.reset(e.s.Buf[..0], default, opts.ꓸꓸꓸ);
    return e;
}
internal static void putBufferedEncoder(ж<Encoder> Ꮡe) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (cap(e.s.availBuffer) > 64 << 10) {
        e.s.availBuffer = default;
    }

    switch (ᐧ) {
    case {} when cap(e.s.Buf) <= 4 << 10: {
        e.s.bufStats.strikes = 0;
        break;
    }
    case {} when cap(e.s.Buf) / 4 <= len(e.s.Buf): {
        e.s.bufStats.strikes = 0;
        break;
    }
    case {} when e.s.bufStats.strikes is < 4: {
        e.s.bufStats.strikes++;
        break;
    }
    default: {
        e.s.bufStats.strikes = 0;
        e.s.bufStats.prevLen = len(e.s.Buf);
        e.s.Buf = default;
        break;
    }}

    bufferedEncoderPool.Put(Ꮡe.OrTypedNil());
}

internal static ж<Encoder> getStreamingEncoder(io.Writer w, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    var (_, ok) = w._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        var e = bytesBufferEncoderPool.Get()._<ж<Encoder>>();
        e.Ꮡs.reset(default, w, opts.ꓸꓸꓸ);
        return e;
    } else {
        var e = streamingEncoderPool.Get()._<ж<Encoder>>();
        e.Ꮡs.reset(e.s.Buf[..0], w, opts.ꓸꓸꓸ);
        return e;
    }
}
internal static void putStreamingEncoder(ж<Encoder> Ꮡe) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (cap(e.s.availBuffer) > 64 << 10) {
        e.s.availBuffer = default;
    }
    var (_, ok) = e.s.wr._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        (e.s.wr, e.s.Buf) = (default, default);
        bytesBufferEncoderPool.Put(Ꮡe.OrTypedNil());
    } else {
        e.s.wr = default;
        if (cap(e.s.Buf) > 64 << 10) {
            e.s.Buf = default;
        }
        streamingEncoderPool.Put(Ꮡe.OrTypedNil());
    }
}

internal static ж<sync.Pool> bufferedDecoderPool = Ꮡ(new sync.Pool(New: () => @new<Decoder>()));
internal static ж<sync.Pool> streamingDecoderPool = Ꮡ(new sync.Pool(New: () => @new<Decoder>()));
internal static ж<sync.Pool> bytesBufferDecoderPool = bufferedDecoderPool;

internal static ж<Decoder> getBufferedDecoder(slice<byte> b, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    var d = bufferedDecoderPool.Get()._<ж<Decoder>>();
    d.Ꮡs.reset(b, default, opts.ꓸꓸꓸ);
    return d;
}
internal static void putBufferedDecoder(ж<Decoder> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    d.s.buf = default;
    bufferedDecoderPool.Put(Ꮡd.OrTypedNil());
}

internal static ж<Decoder> getStreamingDecoder(io.Reader r, params ꓸꓸꓸOptions optsʗp) {
    var opts = optsʗp.sslice();

    var (_, ok) = r._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        var d = bytesBufferDecoderPool.Get()._<ж<Decoder>>();
        d.Ꮡs.reset(default, r, opts.ꓸꓸꓸ);
        return d;
    } else {
        var d = streamingDecoderPool.Get()._<ж<Decoder>>();
        d.Ꮡs.reset(d.s.buf[..0], r, opts.ꓸꓸꓸ);
        return d;
    }
}
internal static void putStreamingDecoder(ж<Decoder> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    var (_, ok) = d.s.rd._<ж<bytes.Buffer>>(ᐧ); if (ok) {
        (d.s.rd, d.s.buf) = (default, default);
        bytesBufferDecoderPool.Put(Ꮡd.OrTypedNil());
    } else {
        d.s.rd = default;
        if (cap(d.s.buf) > 64 << 10) {
            d.s.buf = default;
        }
        streamingDecoderPool.Put(Ꮡd.OrTypedNil());
    }
}

} // end jsontext_package
