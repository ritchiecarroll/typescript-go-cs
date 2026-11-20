// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using unicode = unicode_package;
using utf8 = go.unicode.utf8_package;
using go.unicode;

partial class json_package {

internal static slice<byte> foldName(slice<byte> @in) {
    array<byte> arr = new(32);
    return appendFoldedName(arr[..0], @in);
}
internal static slice<byte> appendFoldedName(slice<byte> @out, slice<byte> @in) {
    for (nint i = 0; i < len(@in); ) {
        var c = @in[i]; if (c < utf8.RuneSelf) {
            if (c != '_' && c != '-') {
                if ('a' <= c && c <= 'z') {
                    c -= (byte)((rune)'a' - (rune)'A');
                }
                @out = append(@out, c);
            }
            i++;
            continue;
        }
        var (r, n) = utf8.DecodeRune(@in.slice(i));
        @out = utf8.AppendRune(@out, foldRune(r));
        i += n;
    }
    return @out;
}

internal static rune foldRune(rune r) {
    for (;;) {
        var r2 = unicode.SimpleFold(r);
        if (r2 <= r) {
            return r2;
        }
        r = r2;
    }
}

} // end json_package
