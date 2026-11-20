// Copyright 2014 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

partial class language_package {

public static slice<Language> BaseLanguages() {
    var @base = new slice<Language>(0, NumLanguages);
    for (nint iΔ1 = 0; iΔ1 < langNoIndexOffset; iΔ1++) {
        if (iΔ1 != nonCanonicalUnd) {
            @base = append(@base, (Language)(uint16)iΔ1);
        }
    }
    nint i = langNoIndexOffset;
    foreach (var (_, vᴛ1) in range(langNoIndex.ΔRangeSnapshot())) {
        var v = vᴛ1;

        for (nint k = 0; k < 8; k++) {
            if ((uint8)(v & 1) == 1) {
                @base = append(@base, (Language)(uint16)i);
            }
            v >>= 1;
            i++;
        }
    }
    return @base;
}

} // end language_package
