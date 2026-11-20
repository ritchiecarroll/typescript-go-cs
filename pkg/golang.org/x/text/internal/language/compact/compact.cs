// Copyright 2018 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal.language;

using sort = sort_package;
using strings = strings_package;
using language = go.golang.org.x.text.@internal.language_package;
using go.golang.org.x.text.@internal;
using static compact_package;

partial class compact_package {

partial struct ID /*num:uint16*/;

internal static (ID id, bool ok) getCoreIndex(language.Tag t) {
    bool ok = default;

    (var cci, ok) = language.GetCompactCore(t);
    if (!ok) {
        return (0, false);
    }
    nint i = sort.Search(len(coreTags), (nint iΔ1) => cci <= coreTags[iΔ1]);
    if (i == len(coreTags) || coreTags[i] != cci) {
        return (0, false);
    }
    return ((ID)(uint16)i, true);
}

} partial class compactᴛID {

public static ID Parent(this ID id) {
    return parents[id];
}

public static language.Tag Tag(this ID id) {
    if ((nint)id >= len(coreTags)) {
        return specialTags[(nint)id - len(coreTags)];
    }
    return coreTags[id].Tag();
}

} partial class compact_package {

internal static slice<language.Tag> specialTags;

[GoInit] internal static void init() {
    var tags = strings.Split(specialTagsStr, " ");
    specialTags = new slice<language.Tag>(len(tags));
    foreach (var (i, t) in range(tags)) {
        specialTags[i] = language.MustParse(t);
    }
}

} // end compact_package
