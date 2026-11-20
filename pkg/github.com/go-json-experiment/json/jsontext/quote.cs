// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using go.github.com.go_json_experiment.json.@internal;

partial class jsontext_package {

public static (slice<byte>, error) AppendQuote<Bytes>(slice<byte> dst, Bytes src)
    where Bytes : /* ~[]byte | ~string */ IByteSeq<Bytes, byte>, new()
{
    (dst, var err) = jsonwire.AppendQuote(dst, src.ToSlice(), Ꮡ(new jsonflags.Flags()));
    if (err != nil) {
        err = new SyntacticErrorжerror(Ꮡ(new SyntacticError(Err: err)));
    }
    return (dst, err);
}

public static (slice<byte>, error) AppendUnquote<Bytes>(slice<byte> dst, Bytes src)
    where Bytes : /* ~[]byte | ~string */ IByteSeq<Bytes, byte>, new()
{
    (dst, var err) = jsonwire.AppendUnquote(dst, src.ToSlice());
    if (err != nil) {
        err = new SyntacticErrorжerror(Ꮡ(new SyntacticError(Err: err)));
    }
    return (dst, err);
}

} // end jsontext_package
