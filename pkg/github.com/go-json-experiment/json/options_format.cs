// Copyright 2026 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using static json_package;

partial class json_package {

partial struct supportFormatTag {
    /*embed*/ public Options Options;
    internal bool v;
}

internal static readonly ж<supportFormatTag> ᏑenableFormatTag = new StaticBox<supportFormatTag>(new supportFormatTag(v: true));
internal static ref supportFormatTag enableFormatTag => ref ((StaticBox<supportFormatTag>)ᏑenableFormatTag).ValueSlot;
internal static readonly ж<supportFormatTag> ᏑdisableFormatTag = new StaticBox<supportFormatTag>(new supportFormatTag(v: false));
internal static ref supportFormatTag disableFormatTag => ref ((StaticBox<supportFormatTag>)ᏑdisableFormatTag).ValueSlot;

} partial class jsonᴛsupportFormatTag {

internal static bool ExperimentalSupportFormatTag(this ref supportFormatTag o) {
    return o.v;
}

} partial class json_package {

public static Options ExperimentalSupportFormatTag(bool v) {
    if (v) {
        return new supportFormatTagжOptions(ᏑenableFormatTag);
    } else {
        return new supportFormatTagжOptions(ᏑdisableFormatTag);
    }
}

} // end json_package
