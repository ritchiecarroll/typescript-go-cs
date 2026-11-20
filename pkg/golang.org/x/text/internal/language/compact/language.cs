// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal.language;

using strings = strings_package;
using language = go.golang.org.x.text.@internal.language_package;
using go.golang.org.x.text.@internal;
using static compact_package;

partial class compact_package {

partial struct Tag {
    internal ID language;
    internal ID locale;
    internal fullTag full;
}

internal static UntypedInt _und => 0;

partial interface fullTag {
    bool IsRoot();
    language.Tag Parent();
}

public static Tag /*tag*/ Make(language.Tag t) {
    Tag tag = default;

    @string region = t.TypeForKey("rg"); if (len(region) == 6 && region[2..] == "zzzz") {
        var (r, err) = language.ParseRegion(region[..2]); if (err == nil) {
            var tFull = t;
            (t, _) = t.SetTypeForKey("rg", "");
            bool exact1 = default;
            bool exact2 = default;
            (tag.language, exact1) = FromTag(t);
            t.RegionID = r;
            (tag.locale, exact2) = FromTag(t);
            if (!exact1 || !exact2) {
                tag.full = new language_TagᴠfullTag(tFull);
            }
            return tag;
        }
    }
    var (lang, ok) = FromTag(t);
    tag.language = lang;
    tag.locale = lang;
    if (!ok) {
        tag.full = new language_TagᴠfullTag(t);
    }
    return tag;
}

} partial class compactᴛTag {

public static language.Tag Tag(this Tag t) {
    if (t.full != nil) {
        return t.full._<fullTag, language.Tag>();
    }
    var tag = t.language.Tag();
    if (t.language != t.locale) {
        var loc = t.locale.Tag();
        (tag, _) = tag.SetTypeForKey("rg", strings.ToLower(loc.RegionID.String()) + "zzzz"u8);
    }
    return tag;
}

public static bool IsCompact(this ref Tag t) {
    return t.full == nil;
}

public static bool MayHaveVariants(this Tag t) {
    return t.full != nil || (nint)t.language >= len(coreTags);
}

public static bool MayHaveExtensions(this Tag t) {
    return t.full != nil || (nint)t.language >= len(coreTags) || t.language != t.locale;
}

public static bool IsRoot(this Tag t) {
    if (t.full != nil) {
        return t.full.IsRoot();
    }
    return t.language == _und;
}

public static Tag Parent(this Tag t) {
    if (t.full != nil) {
        return Make(t.full.Parent());
    }
    if (t.language != t.locale) {
        return new Tag(language: t.language, locale: t.language);
    }
    var (lang, _) = FromTag(t.language.Tag().Parent());
    return new Tag(language: lang, locale: lang);
}

} partial class compact_package {

internal static (@string t, @string tail) nextToken(@string s) {
    nint p = strings.Index(s[1..], "-");
    if (p == -1) {
        return (s[1..], "");
    }
    p++;
    return (s.slice(1, p), s.slice(p));
}

public static (ID id, bool exact) LanguageID(Tag t) {
    return (t.language, t.full == nil);
}

public static (ID id, bool exact) RegionalID(Tag t) {
    return (t.locale, t.full == nil);
}

} partial class compactᴛTag {

public static Tag LanguageTag(this Tag t) {
    if (t.full == nil) {
        return new Tag(language: t.language, locale: t.language);
    }
    var tt = t.Tag();
    tt.SetTypeForKey("rg", "");
    tt.SetTypeForKey("va", "");
    return Make(tt);
}

public static Tag RegionalTag(this Tag t) {
    var rt = new Tag(language: t.locale, locale: t.locale);
    if (t.full == nil) {
        return rt;
    }
    var b = new language.Builder();
    var tag = t.Tag();
    b.SetTag(t.locale.Tag());
    @string v = tag.Variants(); if (v != ""u8) {
        foreach (var (_, vΔ1) in range(strings.Split(v, "-"))) {
            b.AddVariant(vΔ1);
        }
    }
    foreach (var (_, e) in range(tag.Extensions())) {
        b.AddExt(e);
    }
    return t;
}

} partial class compact_package {

public static (ID id, bool exact) FromTag(language.Tag t) {
    bool exact = default;

    exact = true;

    var (b, s, r) = t.Raw();
    if (t.HasString()) {
        if (t.IsPrivateUse()) {
            return (0, false);
        }
        var hasExtra = false;
        if (t.HasVariants()) {
            if (t.HasExtensions()) {
                var build = new language.Builder();
                build.SetTag(new language.Tag(LangID: b, ScriptID: s, RegionID: r));
                build.AddVariant(t.Variants());
                exact = false;
                t = build.Make();
            }
            hasExtra = true;
        } else if (t.Extension((byte)'u') is var (_, ok) && ok) {
            var old = t;
            @string variant = t.TypeForKey("va");
            t = new language.Tag(LangID: b, ScriptID: s, RegionID: r);
            if (variant != ""u8) {
                (t, _) = t.SetTypeForKey("va", variant);
                hasExtra = true;
            }
            exact = old == t;
        } else {
            exact = false;
        }
        if (hasExtra) {
            foreach (var (i, sΔ1) in range(specialTags)) {
                if (sΔ1 == t) {
                    return ((ID)(uint16)(i + len(coreTags)), exact);
                }
            }
            exact = false;
        }
    }
    {
        var (x, ok) = getCoreIndex(t); if (ok) {
            return (x, exact);
        }
    }
    exact = false;
    if (r != 0 && s == 0) {
        var (tΔ1, _) = t.Maximize();
        var (x, ok) = getCoreIndex(tΔ1); if (ok) {
            return (x, exact);
        }
    }
    for (t = t.Parent(); t != root; t = t.Parent()) {
        var (x, ok) = getCoreIndex(t); if (ok) {
            return (x, exact);
        }
    }
    return (0, exact);
}

internal static language.Tag root = new language.Tag();

} // end compact_package
