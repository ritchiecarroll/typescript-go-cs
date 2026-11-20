// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text;

using strings = strings_package;
using language = go.golang.org.x.text.@internal.language_package;
using compact = go.golang.org.x.text.@internal.language.compact_package;
using go.golang.org.x.text.@internal;
using go.golang.org.x.text.@internal.language;
using static language_package;

partial class language_package {

partial struct Tag /*global::go.golang.org.x.text.@internal.language.compact_package.Tag*/;

internal static Tag /*tag*/ makeTag(language.Tag t) {
    return (Tag)compact.Make(t);
}

} partial class languageᴛTag {

internal static language.Tag tag(this ж<Tag> Ꮡt) {
    return (Ꮡt.Reinterpret<Tag, compact.Tag>()).Value.Tag();
}

internal static bool isCompact(this ж<Tag> Ꮡt) {
    return (Ꮡt.Reinterpret<Tag, compact.Tag>()).IsCompact();
}

internal static language.Language lang(this ж<Tag> Ꮡt) { return Ꮡt.tag().LangID; }
internal static language.Region region(this ж<Tag> Ꮡt) { return Ꮡt.tag().RegionID; }
internal static language.Script script(this ж<Tag> Ꮡt) { return Ꮡt.tag().ScriptID; }

} partial class language_package {

public static Tag Make(@string s) {
    return Default.Make(s);
}

} partial class languageᴛCanonType {

public static Tag Make(this CanonType c, @string s) {
    var (t, _) = c.Parse(s);
    return t;
}

} partial class languageᴛTag {

public static (Base b, Script s, Region r) Raw(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    var tt = Ꮡt.tag();
    return (new Base(tt.LangID), new Script(tt.ScriptID), new Region(tt.RegionID));
}

public static bool IsRoot(this Tag t) {
    return ((compact.Tag)t).IsRoot();
}

} partial class language_package {

partial struct CanonType /*num:nint*/;

public static CanonType DeprecatedBase => /* 1 << iota */ 1;
public static CanonType DeprecatedScript => 2;
public static CanonType DeprecatedRegion => 4;
public static CanonType SuppressScript => 8;
public static CanonType Legacy => 16;
public static CanonType Macro => 32;
public static CanonType CLDR => 64;
public static CanonType Raw => 0;
public static CanonType Deprecated => /* DeprecatedBase | DeprecatedScript | DeprecatedRegion */ 7;
public static CanonType BCP47 => /* Deprecated | SuppressScript */ 15;
public static CanonType All => /* BCP47 | Legacy | Macro */ 63;
public static CanonType Default => /* Deprecated | Legacy */ 23;
internal static CanonType canonLang => /* DeprecatedBase | Legacy | Macro */ 49;

internal static (language.Tag, bool) canonicalize(CanonType c, language.Tag t) {
    if (c == Raw) {
        return (t, false);
    }
    var changed = false;
    if ((CanonType)(c & SuppressScript) != 0) {
        if (t.LangID.SuppressScript() == t.ScriptID) {
            t.ScriptID = 0;
            changed = true;
        }
    }
    if ((CanonType)(c & canonLang) != 0) {
        for (;;) {
            var (l, aliasType) = t.LangID.Canonicalize(); if (l != t.LangID) {
                switch (aliasType) {
                case var ᴛ when ᴛ == language.Legacy: {
                    if ((CanonType)(c & Legacy) != 0) {
                        if (t.LangID == _sh && t.ScriptID == 0) {
                            t.ScriptID = _Latn;
                        }
                        t.LangID = l;
                        changed = true;
                    }
                    break;
                }
                case var ᴛ when ᴛ == language.Macro: {
                    if ((CanonType)(c & Macro) != 0) {
                        if ((CanonType)(c & CLDR) == 0 || t.LangID != _nb) {
                            changed = true;
                            t.LangID = l;
                        }
                    }
                    break;
                }
                case var ᴛ when ᴛ == language.Deprecated: {
                    if ((CanonType)(c & DeprecatedBase) != 0) {
                        if (t.LangID == _mo && t.RegionID == 0) {
                            t.RegionID = _MD;
                        }
                        t.LangID = l;
                        changed = true;
                        continue;
                    }
                    break;
                }}

            } else if ((CanonType)(c & Legacy) != 0 && t.LangID == _no && (CanonType)(c & CLDR) != 0) {
                t.LangID = _nb;
                changed = true;
            }
            break;
        }
    }
    if ((CanonType)(c & DeprecatedScript) != 0) {
        if (t.ScriptID == _Qaai) {
            changed = true;
            t.ScriptID = _Zinh;
        }
    }
    if ((CanonType)(c & DeprecatedRegion) != 0) {
        var r = t.RegionID.Canonicalize(); if (r != t.RegionID) {
            changed = true;
            t.RegionID = r;
        }
    }
    return (t, changed);
}

} partial class languageᴛCanonType {

public static (Tag, error) Canonicalize(this CanonType c, Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    if (Ꮡt.isCompact()) {
        var (_, changed) = canonicalize(c, ((compact.Tag)t).Tag()); if (!changed) {
            return (t, default);
        }
    }
    {
        ref var tag = ref heap<language.Tag>(out var Ꮡtag);
        (tag, var changed) = canonicalize(c, Ꮡt.tag()); if (changed) {
            Ꮡtag.RemakeString();
            return (makeTag(tag), default);
        }
    }
    return (t, default);
}

} partial class language_package {

partial struct Confidence /*num:nint*/;

public static Confidence No => /* iota */ 0;
public static Confidence Low => 1;
public static Confidence High => 2;
public static Confidence Exact => 3;

internal static slice<@string> confName = ["No", "Low", "High", "Exact"];

} partial class languageᴛConfidence {

public static @string String(this Confidence c) {
    return confName[c];
}

} partial class languageᴛTag {

public static @string String(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    return Ꮡt.tag().String();
}

public static (slice<byte> text, error err) MarshalText(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    return Ꮡt.tag().MarshalText();
}

public static error UnmarshalText(this ref Tag t, slice<byte> text) {
    language.Tag tag = default;
    var err = tag.UnmarshalText(text);
    t = makeTag(tag);
    return err;
}

public static (Base, Confidence) Base(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    var b = Ꮡt.lang(); if (b != 0) {
        return (new Base(b), Exact);
    }
    var tt = Ꮡt.tag();
    Confidence c = High;
    if (tt.ScriptID == 0 && !tt.RegionID.IsCountry()) {
        c = Low;
    }
    var (tag, err) = tt.Maximize(); if (err == nil && tag.LangID != 0) {
        return (new Base(tag.LangID), c);
    }
    return (new Base(0), No);
}

public static (Script, Confidence) Script(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    {
        var scr = Ꮡt.script(); if (scr != 0) {
            return (new Script(scr), Exact);
        }
    }
    var tt = Ꮡt.tag();
    var sc = (language.Script)_Zzzz;
    Confidence c = No;
    {
        var scr = tt.LangID.SuppressScript(); if (scr != 0) {
            if (tt.RegionID == 0) {
                return (new Script(scr), High);
            }
            (sc, c) = (scr, High);
        }
    }
    var (tag, err) = tt.Maximize(); if (err == nil) {
        if (tag.ScriptID != sc) {
            (sc, c) = (tag.ScriptID, Low);
        }
    } else {
        (tt, _) = canonicalize((CanonType)(Deprecated | Macro), tt);
        var (tagΔ1, errΔ1) = tt.Maximize(); if (errΔ1 == nil && tagΔ1.ScriptID != sc) {
            (sc, c) = (tagΔ1.ScriptID, Low);
        }
    }
    return (new Script(sc), c);
}

public static (Region, Confidence) Region(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    var r = Ꮡt.region(); if (r != 0) {
        return (new Region(r), Exact);
    }
    var tt = Ꮡt.tag();
    {
        var (ttΔ1, err) = tt.Maximize(); if (err == nil) {
            return (new Region(ttΔ1.RegionID), Low);
        }
    }
    (tt, _) = canonicalize((CanonType)(Deprecated | Macro), tt);
    {
        var (tag, err) = tt.Maximize(); if (err == nil) {
            return (new Region(tag.RegionID), Low);
        }
    }
    return (new Region(_ZZ), No);
}

public static slice<Variant> Variants(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);
    var vᴷ = new stackAppend<Variant>(moves: true);

    if (!((compact.Tag)t).MayHaveVariants()) {
        return default;
    }
    var v = slice<Variant>([]);
    @string x = "";
    @string str = Ꮡt.tag().Variants();
    while (str != ""u8) {
        (x, str) = nextToken(str);
        v = append(vᴷ, v, new Variant(x));
    }
    v = move2heap(vᴷ, v);
    return v;
}

public static Tag Parent(this Tag t) {
    return (Tag)((compact.Tag)t).Parent();
}

} partial class language_package {

internal static (@string t, @string tail) nextToken(@string s) {
    nint p = strings.Index(s[1..], "-");
    if (p == -1) {
        return (s[1..], "");
    }
    p++;
    return (s.slice(1, p), s.slice(p));
}

partial struct Extension {
    internal @string s;
}

} partial class languageᴛExtension {

public static @string String(this Extension e) {
    return e.s;
}

} partial class language_package {

public static (Extension e, error err) ParseExtension(@string s) {
    error err = default;

    (var ext, err) = language.ParseExtension(s);
    return (new Extension(ext), err);
}

} partial class languageᴛExtension {

public static byte Type(this Extension e) {
    if (e.s == ""u8) {
        return 0;
    }
    return e.s[0];
}

public static slice<@string> Tokens(this Extension e) {
    return strings.Split(e.s, "-");
}

} partial class languageᴛTag {

public static (Extension ext, bool ok) Extension(this Tag tʗp, byte x) {
    bool ok = default;
    ref var t = ref heap(tʗp, out var Ꮡt);

    if (!((compact.Tag)t).MayHaveExtensions()) {
        return (new Extension(), false);
    }
    (var e, ok) = Ꮡt.tag().Extension(x);
    return (new Extension(e), ok);
}

public static slice<Extension> Extensions(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);
    var eᴷ = new stackAppend<Extension>(moves: true);

    if (!((compact.Tag)t).MayHaveExtensions()) {
        return default;
    }
    var e = slice<Extension>([]);
    foreach (var (_, ext) in range(Ꮡt.tag().Extensions())) {
        e = append(eᴷ, e, new Extension(ext));
    }
    e = move2heap(eᴷ, e);
    return e;
}

public static @string TypeForKey(this Tag tʗp, @string key) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    if (!((compact.Tag)t).MayHaveExtensions()) {
        if (key != "rg"u8 && key != "va"u8) {
            return "";
        }
    }
    return Ꮡt.tag().TypeForKey(key);
}

public static (Tag, error) SetTypeForKey(this Tag tʗp, @string key, @string value) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    var (tt, err) = Ꮡt.tag().SetTypeForKey(key, value);
    return (makeTag(tt), err);
}

} partial class language_package {

public static UntypedInt NumCompactTags => /* compact.NumCompactTags */ 775;

public static (nint index, bool exact) CompactIndex(Tag t) {
    bool exact = default;

    (var id, exact) = compact.LanguageID((compact.Tag)t);
    return ((nint)id, exact);
}

internal static language.Tag root = new language.Tag();

partial struct Base {
    internal language.Language langID;
}

public static (Base, error) ParseBase(@string s) {
    var (l, err) = language.ParseBase(s);
    return (new Base(l), err);
}

} partial class languageᴛBase {

public static @string String(this Base b) {
    return b.langID.String();
}

public static @string ISO3(this Base b) {
    return b.langID.ISO3();
}

public static bool IsPrivateUse(this Base b) {
    return b.langID.IsPrivateUse();
}

} partial class language_package {

partial struct Script {
    internal language.Script scriptID;
}

public static (Script, error) ParseScript(@string s) {
    var (sc, err) = language.ParseScript(s);
    return (new Script(sc), err);
}

} partial class languageᴛScript {

public static @string String(this Script s) {
    return s.scriptID.String();
}

public static bool IsPrivateUse(this Script s) {
    return s.scriptID.IsPrivateUse();
}

} partial class language_package {

partial struct Region {
    internal language.Region regionID;
}

public static (Region, error) EncodeM49(nint r) {
    var (rid, err) = language.EncodeM49(r);
    return (new Region(rid), err);
}

public static (Region, error) ParseRegion(@string s) {
    var (r, err) = language.ParseRegion(s);
    return (new Region(r), err);
}

} partial class languageᴛRegion {

public static @string String(this Region r) {
    return r.regionID.String();
}

public static @string ISO3(this Region r) {
    return r.regionID.ISO3();
}

public static nint M49(this Region r) {
    return r.regionID.M49();
}

public static bool IsPrivateUse(this Region r) {
    return r.regionID.IsPrivateUse();
}

public static bool IsCountry(this Region r) {
    return r.regionID.IsCountry();
}

public static bool IsGroup(this Region r) {
    return r.regionID.IsGroup();
}

public static bool Contains(this Region r, Region c) {
    return r.regionID.Contains(c.regionID);
}

public static (Region, error) TLD(this Region r) {
    var (tld, err) = r.regionID.TLD();
    return (new Region(tld), err);
}

public static Region Canonicalize(this Region r) {
    return new Region(r.regionID.Canonicalize());
}

} partial class language_package {

partial struct Variant {
    internal @string variant;
}

public static (Variant, error) ParseVariant(@string s) {
    var (v, err) = language.ParseVariant(s);
    return (new Variant(v.String()), err);
}

} partial class languageᴛVariant {

public static @string String(this Variant v) {
    return v.variant;
}

} // end languageᴛVariant
