// Copyright 2026 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using atomic = go.sync.atomic_package;
using go.sync;

partial class json_package {

internal static readonly ж<atomic.Bool> ᏑglobalEnableFormatTag = new StaticBox<atomic.Bool>(default(atomic.Bool));
internal static ref atomic.Bool globalEnableFormatTag => ref ((StaticBox<atomic.Bool>)ᏑglobalEnableFormatTag).ValueSlot;

public static void ExperimentalGlobalSupportFormatTag(bool v) {
    ᏑglobalEnableFormatTag.Store(v);
}

internal static slice<Options> mayAppendSupportFormatTag(slice<Options> opts) {
    if (ᏑglobalEnableFormatTag.Load()) {
        array<Options> optsArr = new(4);
        opts = append(appendꓸꓸꓸ(optsArr[..0], opts), (Options)(new supportFormatTagжOptions(ᏑenableFormatTag)));
    }
    return opts;
}

} // end json_package
