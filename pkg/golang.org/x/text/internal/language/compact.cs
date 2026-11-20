// Copyright 2018 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

using static language_package;

partial class language_package {

partial struct CompactCoreInfo /*num:uint32*/;

public static (CompactCoreInfo cci, bool ok) GetCompactCore(Tag t) {
    CompactCoreInfo cci = default;

    if (t.LangID > langNoIndexOffset) {
        return (0, false);
    }
    cci |= (CompactCoreInfo)((((CompactCoreInfo)(uint32)t.LangID) << (int)(8 + 12)));
    cci |= (CompactCoreInfo)((((CompactCoreInfo)(uint32)t.ScriptID) << 12));
    cci |= (CompactCoreInfo)(((CompactCoreInfo)(uint32)t.RegionID));
    return (cci, true);
}

} partial class languageᴛCompactCoreInfo {

public static Tag Tag(this CompactCoreInfo c) {
    return new Tag(
        LangID: ((Language)(uint16)((c >> 20))),
        RegionID: ((Region)(uint16)((CompactCoreInfo)(c & 0x3ff))),
        ScriptID: (Script)(((Script)(uint16)((c >> 12))) & 0xff)
    );
}

} // end languageᴛCompactCoreInfo
