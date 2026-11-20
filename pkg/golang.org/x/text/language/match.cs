// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text;

using errors = errors_package;
using strings = strings_package;
using language = go.golang.org.x.text.@internal.language_package;
using go.golang.org.x.text.@internal;
using ꓸꓸꓸTag = Span<language_package.Tag>;
using ꓸꓸꓸstring = Span<@string>;
using static language_package;

partial class language_package {

// type MatchOption is a methodless func type — rendered inline as its base delegate

public static Action<ж<matcher>> PreferSameScript(bool preferSame) {
    return (ж<matcher> m) => {
        m.preferSameScript = preferSame;
    };
}

public static (Tag tag, nint index) MatchStrings(Matcher m, params ꓸꓸꓸstring langʗp) {
    Tag tag = default;
    nint index = default;
    var lang = langʗp.sslice();

    foreach (var (_, accept) in range(lang)) {
        var (desired, _, err) = ParseAcceptLanguage(accept);
        if (err != nil) {
            continue;
        }
        var (tagΔ1, indexΔ1, conf) = m.Match(desired.ꓸꓸꓸ); if (conf != No) {
            return (tagΔ1, indexΔ1);
        }
    }
    (tag, index, _) = m.Match();
    return (tag, index);
}

partial interface Matcher {
    (Tag tag, nint index, Confidence c) Match(params ꓸꓸꓸTag tʗp);
}

public static Confidence Comprehends(Tag speaker, Tag alternative) {
    var (_, _, c) = NewMatcher([alternative]).Match(speaker);
    return c;
}

public static Matcher NewMatcher(slice<Tag> t, params Span<Action<ж<matcher>>> optionsʗp) {
    var options = optionsʗp.slice();

    return new matcherжMatcher(newMatcher(t, options));
}

} partial class languageᴛmatcher {

public static (Tag t, nint index, Confidence c) Match(this ref matcher m, params ꓸꓸꓸTag wantʗp) {
    nint index = default;
    Confidence c = default;
    var want = wantʗp.sslice();

    ref var tt = ref heap(new language.Tag(), out var Ꮡtt);
    (var match, var w, c) = m.getBest(want.ꓸꓸꓸ);
    if (match != nil) {
        (tt, index) = (match.tag, match.index);
    } else {
        tt = m.default_.tag;
        if (m.preferSameScript) {
outer:
            foreach (var (_, wΔ1) in range(want)) {
                var (script, _) = wΔ1.Script();
                if (script.scriptID == 0) {
                    continue;
                }
                foreach (var (i, h) in range(m.supported)) {
                    if (script.scriptID == h.maxScript) {
                        (tt, index) = (h.tag, i);
                        goto break_outer;
                    }
                }
continue_outer:;
            }
break_outer:;
        }
    }
    if (w.RegionID != tt.RegionID && w.RegionID != 0) {
        if (w.RegionID != 0 && tt.RegionID != 0 && tt.RegionID.Contains(w.RegionID)) {
            tt.RegionID = w.RegionID;
            Ꮡtt.RemakeString();
        } else {
            @string r = w.RegionID.String(); if (len(r) == 2) {
                (tt, _) = tt.SetTypeForKey("rg", strings.ToLower(r) + "zzzz"u8);
            }
        }
    }
    var e = w.Extensions(); if (len(e) > 0) {
        var b = new language.Builder();
        b.SetTag(tt);
        foreach (var (_, eΔ1) in range(e)) {
            b.AddExt(eΔ1);
        }
        tt = b.Make();
    }
    return (makeTag(tt), index, c);
}

} partial class language_package {

public static error ErrMissingLikelyTagsData = errors.New("missing likely tags data");

public partial struct matcher {
    internal ж<haveTag> default_;
    internal slice<ж<haveTag>> supported;
    internal map<language.Language, ж<matchHeader>> index;
    internal bool passSettings;
    internal bool preferSameScript;
}

partial struct matchHeader {
    internal slice<ж<haveTag>> haveTags;
    internal bool original;
}

partial struct haveTag {
    internal language.Tag tag;
    internal nint index;
    internal Confidence conf;
    internal language.Region maxRegion;
    internal language.Script maxScript;
    internal language.Script altScript;
    internal uint16 nextMax;
}

internal static (haveTag, language.Language) makeHaveTag(language.Tag tag, nint index) {
    ref var max = ref heap<language.Tag>(tag, out var Ꮡmax);
    if (tag.LangID != 0 || tag.RegionID != 0 || tag.ScriptID != 0) {
        (max, _) = canonicalize(All, max);
        (max, _) = max.Maximize();
        Ꮡmax.RemakeString();
    }
    return (new haveTag(tag, index, Exact, max.RegionID, max.ScriptID, altScript(max.LangID, max.ScriptID), 0), max.LangID);
}

internal static language.Script altScript(language.Language l, language.Script s) {
    foreach (var (_, alt) in range(matchScript)) {
        if ((((language.Language)alt.wantLang) == l || ((language.Language)alt.haveLang) == l) && ((language.Script)(uint16)alt.haveScript) == s) {
            return (language.Script)(uint16)alt.wantScript;
        }
    }
    return 0;
}

} partial class languageᴛmatchHeader {

internal static void addIfNew(this ref matchHeader h, haveTag nʗp, bool exact) {
    ref var n = ref heap(nʗp, out var Ꮡn);

    h.original = h.original || exact;
    foreach (var (_, v) in range(h.haveTags)) {
        if (equalsRest(v.tag, n.tag)) {
            return;
        }
    }
    foreach (var (iᴛ1, v) in range(h.haveTags)) {
        var i = iᴛ1;

        if (v.maxScript == n.maxScript && v.maxRegion == n.maxRegion && v.tag.VariantOrPrivateUseTags() == n.tag.VariantOrPrivateUseTags()) {
            while (h.haveTags[i].nextMax != 0) {
                i = (nint)h.haveTags[i].nextMax;
            }
            h.haveTags[i].nextMax = (uint16)len(h.haveTags);
            break;
        }
    }
    h.haveTags = append(h.haveTags, Ꮡn);
}

} partial class languageᴛmatcher {

internal static ж<matchHeader> header(this ref matcher m, language.Language l) {
    var hΔ1 = m.index[l]; if (hΔ1 != nil) {
        return hΔ1;
    }
    var h = Ꮡ(new matchHeader());
    m.index[l] = h;
    return h;
}

} partial class language_package {

internal static Confidence toConf(uint8 d) {
    if (d <= 10) {
        return High;
    }
    if (d < 30) {
        return Low;
    }
    return No;
}

internal static ж<matcher> newMatcher(slice<Tag> supported, slice<Action<ж<matcher>>> options) {
    var m = Ꮡ(new matcher(
        index: new map<language.Language, ж<matchHeader>>(),
        preferSameScript: true
    ));
    foreach (var (_, o) in range(options)) {
        o(m);
    }
    if (len(supported) == 0) {
        m.default_ = Ꮡ(new haveTag());
        return m;
    }
    foreach (var (i, vᴛ1) in range(supported)) {
        ref var tag = ref heap(new Tag(), out var Ꮡtag);
        tag = vᴛ1;

        var tt = Ꮡtag.tag();
        ref var pair = ref heap<haveTag>(out var Ꮡpair);
        (pair, _) = makeHaveTag(tt, i);
        m.header(tt.LangID).addIfNew(pair, true);
        m.supported = append(m.supported, Ꮡpair);
    }
    m.default_ = m.header(Ꮡ(supported, 0).lang()).haveTags[0];
    foreach (var (i, vᴛ2) in range(supported)) {
        ref var tag = ref heap(new Tag(), out var Ꮡtag);
        tag = vᴛ2;

        var tt = Ꮡtag.tag();
        var (pair, max) = makeHaveTag(tt, i);
        if (max != tt.LangID) {
            m.header(max).addIfNew(pair, true);
        }
    }

    var mʗ1 = m;
    void update(uint16 want, uint16 have, Confidence conf) {
        var hh = mʗ1.index[((language.Language)have)]; if (hh != nil) {
            if (!hh.original) {
                return;
            }
            var hw = mʗ1.header((language.Language)want);
            foreach (var (_, ht) in range(hh.haveTags)) {
                var v = ht.Value;
                if (conf < v.conf) {
                    v.conf = conf;
                }
                v.nextMax = 0;
                if (v.altScript != 0) {
                    v.altScript = altScript((language.Language)want, v.maxScript);
                }
                hw.addIfNew(v, conf == Exact && hh.original);
            }
        }
    }

    foreach (var (_, ml) in range(matchLang)) {
        update(ml.want, ml.have, toConf(ml.distance));
        if (!ml.oneway) {
            update(ml.have, ml.want, toConf(ml.distance));
        }
    }

    foreach (var (i, lm) in range(language.AliasMap.ΔRangeSnapshot())) {
        Confidence conf = Exact;
        if (language.AliasTypes[i] != language.Macro) {
            if (!isExactEquivalent((language.Language)lm.From)) {
                conf = High;
            }
            update(lm.To, lm.From, conf);
        }
        update(lm.From, lm.To, conf);
    }
    return m;
}

} partial class languageᴛmatcher {

internal static (ж<haveTag> got, language.Tag orig, Confidence c) getBest(this ref matcher m, params ꓸꓸꓸTag wantʗp) {
    var want = wantʗp.slice();

    var best = new bestMatch();
    foreach (var (i, vᴛ1) in range(want)) {
        ref var ww = ref heap(new Tag(), out var Ꮡww);
        ww = vᴛ1;

        var w = Ꮡww.tag();
        language.Tag max = default;
        var h = m.index[w.LangID];
        if (w.LangID != 0) {
            if (h == nil) {
                continue;
            }
            (max, _) = canonicalize((CanonType)((CanonType)(Legacy | Deprecated) | Macro), w);
            if (w.RegionID != max.RegionID) {
                w.RegionID = max.RegionID;
            }
            (max, _) = max.Maximize();
        } else {
            if (h != nil) {
                foreach (var iΔ1 in range(len(h.haveTags))) {
                    var have = h.haveTags[iΔ1];
                    if (equalsRest(have.tag, w)) {
                        return (have, w, Exact);
                    }
                }
            }
            if (w.ScriptID == 0 && w.RegionID == 0) {
                continue;
            }
            (max, _) = w.Maximize();
            h = m.index[max.LangID]; if (h == nil) {
                continue;
            }
        }
        var pin = true;
        foreach (var (_, vᴛ2) in range(want.slice(i + 1))) {
            ref var t = ref heap(new Tag(), out var Ꮡt);
            t = vᴛ2;

            if (w.LangID == Ꮡt.lang()) {
                pin = false;
                break;
            }
        }
        foreach (var iΔ2 in range(len(h.haveTags))) {
            var have = h.haveTags[iΔ2];
            best.update(have, w, max.ScriptID, max.RegionID, pin);
            if (best.conf == Exact) {
                while (have.nextMax != 0) {
                    have = h.haveTags[have.nextMax];
                    best.update(have, w, max.ScriptID, max.RegionID, pin);
                }
                return (best.have, best.want, best.conf);
            }
        }
    }
    if (best.conf <= No) {
        if (len(want) != 0) {
            return (default, Ꮡ(want, 0).tag(), No);
        }
        return (default, new language.Tag(), No);
    }
    return (best.have, best.want, best.conf);
}

} partial class language_package {

partial struct bestMatch {
    internal ж<haveTag> have;
    internal language.Tag want;
    internal Confidence conf;
    internal language.Region pinnedRegion;
    internal bool pinLanguage;
    internal bool sameRegionGroup;
    internal bool origLang;
    internal bool origReg;
    internal bool paradigmReg;
    internal uint8 regGroupDist;
    internal bool origScript;
}

} partial class languageᴛbestMatch {

internal static void update(this ref bestMatch m, ж<haveTag> Ꮡhave, language.Tag tag, language.Script maxScript, language.Region maxRegion, bool pin) {
    ref var have = ref Ꮡhave.DerefOrNull();

    Confidence c = have.conf;
    if (c < m.conf) {
        return;
    }
    if (m.pinLanguage && tag.LangID != m.want.LangID) {
        return;
    }
    if (tag.LangID == m.want.LangID && m.sameRegionGroup) {
        var (_, sameGroupΔ1) = regionGroupDist(m.pinnedRegion, have.maxRegion, have.maxScript, m.want.LangID);
        if (!sameGroupΔ1) {
            return;
        }
    }
    if (c == Exact && have.maxScript == maxScript) {
        m.pinLanguage = pin;
    }
    if (equalsRest(have.tag, tag)) {
    } else if (have.maxScript != maxScript) {
        if (Low < m.conf || have.altScript != maxScript) {
            return;
        }
        c = Low;
    } else if (have.maxRegion != maxRegion) {
        if (High < c) {
            c = High;
        }
    }

    var beaten = false;
    if (c != m.conf) {
        if (c < m.conf) {
            return;
        }
        beaten = true;
    }

    var origLang = have.tag.LangID == tag.LangID && tag.LangID != 0;
    if (!beaten && m.origLang != origLang) {
        if (m.origLang) {
            return;
        }
        beaten = true;
    }

    var origReg = have.tag.RegionID == tag.RegionID && tag.RegionID != 0;
    if (!beaten && m.origReg != origReg) {
        if (m.origReg) {
            return;
        }
        beaten = true;
    }

    var (regGroupDist, sameGroup) = regionGroupDist(have.maxRegion, maxRegion, maxScript, tag.LangID);
    if (!beaten && m.regGroupDist != regGroupDist) {
        if (regGroupDist > m.regGroupDist) {
            return;
        }
        beaten = true;
    }

    var paradigmReg = isParadigmLocale(tag.LangID, have.maxRegion);
    if (!beaten && m.paradigmReg != paradigmReg) {
        if (!paradigmReg) {
            return;
        }
        beaten = true;
    }

    var origScript = have.tag.ScriptID == tag.ScriptID && tag.ScriptID != 0;
    if (!beaten && m.origScript != origScript) {
        if (m.origScript) {
            return;
        }
        beaten = true;
    }

    if (beaten) {
        m.have = Ꮡhave;
        m.want = tag;
        m.conf = c;
        m.pinnedRegion = maxRegion;
        m.sameRegionGroup = sameGroup;
        m.origLang = origLang;
        m.origReg = origReg;
        m.paradigmReg = paradigmReg;
        m.origScript = origScript;
        m.regGroupDist = regGroupDist;
    }
}

} partial class language_package {

internal static bool isParadigmLocale(language.Language lang, language.Region r) {
    foreach (var (_, vᴛ1) in range(paradigmLocales)) {
        var e = vᴛ1.Clone();

        if (((language.Language)e[0]) == lang && (r == ((language.Region)e[1]) || r == ((language.Region)e[2]))) {
            return true;
        }
    }
    return false;
}

internal static (uint8 dist, bool same) regionGroupDist(language.Region a, language.Region b, language.Script script, language.Language lang) {
    const uint8 defaultDistance = 4;

    nuint aGroup = (nuint)regionToGroups[a] << 1;
    nuint bGroup = (nuint)regionToGroups[b] << 1;
    foreach (var (_, ri) in range(matchRegion)) {
        if (((language.Language)ri.lang) == lang && (ri.script == 0 || ((language.Script)(uint16)ri.script) == script)) {
            nuint group = (nuint)(((nuint)1).Lsh((uint64)((uint8)(ri.group & ~0x80))));
            if ((uint8)(0x80 & ri.group) == 0) {
                if ((aGroup & bGroup & group) != 0) {
                    return (ri.distance, ri.distance == defaultDistance);
                }
            } else {
                if (((aGroup | bGroup) & group) == 0) {
                    return (ri.distance, ri.distance == defaultDistance);
                }
            }
        }
    }
    return (defaultDistance, true);
}

internal static bool equalsRest(language.Tag a, language.Tag b) {
    return a.ScriptID == b.ScriptID && a.RegionID == b.RegionID && a.VariantOrPrivateUseTags() == b.VariantOrPrivateUseTags();
}

internal static bool isExactEquivalent(language.Language l) {
    foreach (var (_, o) in range(notEquivalent)) {
        if (o == l) {
            return false;
        }
    }
    return true;
}

internal static slice<language.Language> notEquivalent;

[GoInit] internal static void init() {
    foreach (var (_, lm) in range(language.AliasMap.ΔRangeSnapshot())) {
        var tag = new language.Tag(LangID: ((language.Language)lm.From));
        (tag, _) = canonicalize(All, tag); if (tag.ScriptID != 0 || tag.RegionID != 0) {
            notEquivalent = append(notEquivalent, (language.Language)lm.From);
        }
    }
    foreach (var (i, vᴛ1) in range(paradigmLocales)) {
        var v = vᴛ1.Clone();

        var t = new language.Tag(LangID: ((language.Language)v[0]));
        var (max, _) = t.Maximize();
        if (v[1] == 0) {
            paradigmLocales[i][1] = (uint16)max.RegionID;
        }
        if (v[2] == 0) {
            paradigmLocales[i][2] = (uint16)max.RegionID;
        }
    }
}

} // end language_package
