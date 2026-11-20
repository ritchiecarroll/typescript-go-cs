// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using io = io_package;
using Δinternal = go.github.com.go_json_experiment.json.internal_package;
using go.github.com.go_json_experiment.json;
using ꓸꓸꓸOptions = Span<Options>;
using static jsontext_package;

partial class jsontext_package {

public static readonly ж<exporter> ᏑInternal = new StaticBox<exporter>(default(exporter));
public static ref exporter Internal => ref ((StaticBox<exporter>)ᏑInternal).ValueSlot;

public partial struct exporter {
}

} partial class jsontextᴛexporter {

public static export Export(this exporter _, ж<Δinternal.NotForPublicUse> p) {
    if (p != Δinternal.ᏑAllowInternalUse) {
        throw panic("unauthorized call to Export");
    }
    return new export();
}

} partial class jsontext_package {

public partial struct export {
}

} partial class jsontextᴛexport {

public static ж<encoderState> Encoder(this export _, ж<Encoder> e) { return e.Ꮡs; }

public static ж<decoderState> Decoder(this export _, ж<Decoder> d) { return d.Ꮡs; }

public static ж<Encoder> GetBufferedEncoder(this export _, params ꓸꓸꓸOptions oʗp) {
    var o = oʗp.sslice();

    return getBufferedEncoder(o.ꓸꓸꓸ);
}
public static void PutBufferedEncoder(this export _, ж<Encoder> e) {
    putBufferedEncoder(e);
}

public static ж<Encoder> GetStreamingEncoder(this export _, io.Writer w, params ꓸꓸꓸOptions oʗp) {
    var o = oʗp.sslice();

    return getStreamingEncoder(w, o.ꓸꓸꓸ);
}
public static void PutStreamingEncoder(this export _, ж<Encoder> e) {
    putStreamingEncoder(e);
}

public static ж<Decoder> GetBufferedDecoder(this export _, slice<byte> b, params ꓸꓸꓸOptions oʗp) {
    var o = oʗp.sslice();

    return getBufferedDecoder(b, o.ꓸꓸꓸ);
}
public static void PutBufferedDecoder(this export _, ж<Decoder> d) {
    putBufferedDecoder(d);
}

public static ж<Decoder> GetStreamingDecoder(this export _, io.Reader r, params ꓸꓸꓸOptions oʗp) {
    var o = oʗp.sslice();

    return getStreamingDecoder(r, o.ꓸꓸꓸ);
}
public static void PutStreamingDecoder(this export _, ж<Decoder> d) {
    putStreamingDecoder(d);
}

public static bool IsIOError(this export _Δp0, error err) {
    var (_, ok) = err._<ж<ioError>>(ᐧ);
    return ok;
}

} // end jsontextᴛexport
