// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

partial class language_package {

public static Tag MustParse(@string s) {
    var (t, err) = Parse(s);
    if (err != nil) {
        throw panic(err);
    }
    return t;
}

public static Language MustParseBase(@string s) {
    var (b, err) = ParseBase(s);
    if (err != nil) {
        throw panic(err);
    }
    return b;
}

public static Script MustParseScript(@string s) {
    var (scr, err) = ParseScript(s);
    if (err != nil) {
        throw panic(err);
    }
    return scr;
}

public static Region MustParseRegion(@string s) {
    var (r, err) = ParseRegion(s);
    if (err != nil) {
        throw panic(err);
    }
    return r;
}

public static readonly ж<Tag> ᏑUnd = new StaticBox<Tag>(default(Tag));
public static ref Tag Und => ref ((StaticBox<Tag>)ᏑUnd).ValueSlot;

} // end language_package
