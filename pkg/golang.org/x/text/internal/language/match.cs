// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

using errors = errors_package;
using static language_package;

partial class language_package {

partial struct scriptRegionFlags /*num:uint8*/;

internal static UntypedInt isList => /* 1 << iota */ 1;
internal static UntypedInt scriptInFrom => 2;
internal static UntypedInt regionInFrom => 4;

} partial class languageᴛTag {

internal static void setUndefinedLang(this ref Tag t, Language id) {
    if (t.LangID == 0) {
        t.LangID = id;
    }
}

internal static void setUndefinedScript(this ref Tag t, Script id) {
    if (t.ScriptID == 0) {
        t.ScriptID = id;
    }
}

internal static void setUndefinedRegion(this ref Tag t, Region id) {
    if (t.RegionID == 0 || t.RegionID.Contains(id)) {
        t.RegionID = id;
    }
}

} partial class language_package {

public static error ErrMissingLikelyTagsData = errors.New("missing likely tags data");

} partial class languageᴛTag {

internal static (Tag, error) addLikelySubtags(this Tag t) {
    ref var id = ref heap<Tag>(out var Ꮡid);
    (id, var err) = addTags(t);
    if (err != nil) {
        return (t, err);
    } else if (id.equalTags(t)) {
        return (t, default);
    }
    Ꮡid.RemakeString();
    return (id, default);
}

} partial class language_package {

internal static bool specializeRegion(ref Tag t) {
    var i = regionInclusion[t.RegionID]; if (i < nRegionGroups) {
        var x = likelyRegionGroup[i];
        if (((Language)x.lang) == t.LangID && ((Script)x.script) == t.ScriptID) {
            t.RegionID = (Region)x.region;
        }
        return true;
    }
    return false;
}

} partial class languageᴛTag {

public static (Tag, error) Maximize(this Tag t) {
    return addTags(t);
}

} partial class language_package {

internal static (Tag, error) addTags(Tag t) {
    if (t.IsPrivateUse()) {
        return (t, default);
    }
    if (t.ScriptID != 0 && t.RegionID != 0) {
        if (t.LangID != 0) {
            specializeRegion(ref t);
            return (t, default);
        }
        var list = likelyRegion.slice(t.RegionID, t.RegionID + 1);
        {
            var x = list[0]; if ((uint8)(x.flags & (uint8)isList) != 0) {
                list = likelyRegionList.slice(x.lang, (uint16)(x.lang + (uint16)x.script));
            }
        }
        foreach (var (_, x) in range(list)) {
            if (((Script)x.script) == t.ScriptID) {
                t.setUndefinedLang((Language)x.lang);
                return (t, default);
            }
        }
    }
    if (t.LangID != 0) {
        if (t.LangID < langNoIndexOffset) {
            var x = likelyLang[t.LangID];
            if ((uint8)(x.flags & (uint8)isList) != 0) {
                var list = likelyLangList.slice(x.region, (uint16)(x.region + (uint16)x.script));
                if (t.ScriptID != 0) {
                    foreach (var (_, xΔ1) in range(list)) {
                        if (((Script)xΔ1.script) == t.ScriptID && (uint8)(xΔ1.flags & (uint8)scriptInFrom) != 0) {
                            t.setUndefinedRegion((Region)xΔ1.region);
                            return (t, default);
                        }
                    }
                } else if (t.RegionID != 0) {
                    nint count = 0;
                    var goodScript = true;
                    var tt = t;
                    foreach (var (_, xΔ2) in range(list)) {
                        if ((uint8)(xΔ2.flags & (uint8)scriptInFrom) == 0 && t.RegionID.Contains((Region)xΔ2.region)) {
                            tt.RegionID = (Region)xΔ2.region;
                            tt.setUndefinedScript((Script)xΔ2.script);
                            goodScript = goodScript && tt.ScriptID == ((Script)xΔ2.script);
                            count++;
                        }
                    }
                    if (count == 1) {
                        return (tt, default);
                    }
                    if (goodScript) {
                        t.ScriptID = tt.ScriptID;
                    }
                }
            }
        }
    } else {
        if (t.ScriptID != 0) {
            var x = likelyScript[t.ScriptID];
            if (x.region != 0) {
                t.setUndefinedRegion((Region)x.region);
                t.setUndefinedLang((Language)x.lang);
                return (t, default);
            }
        }
        if (t.RegionID != 0) {
            var i = regionInclusion[t.RegionID]; if (i < nRegionGroups) {
                var x = likelyRegionGroup[i];
                if (x.region != 0) {
                    t.setUndefinedLang((Language)x.lang);
                    t.setUndefinedScript((Script)x.script);
                    t.RegionID = (Region)x.region;
                }
            } else {
                var x = likelyRegion[t.RegionID];
                if ((uint8)(x.flags & (uint8)isList) != 0) {
                    x = likelyRegionList[x.lang];
                }
                if (x.script != 0 && x.flags != scriptInFrom) {
                    t.setUndefinedLang((Language)x.lang);
                    t.setUndefinedScript((Script)x.script);
                    return (t, default);
                }
            }
        }
    }

    if (t.LangID < langNoIndexOffset) {
        var x = likelyLang[t.LangID];
        if ((uint8)(x.flags & (uint8)isList) != 0) {
            x = likelyLangList[x.region];
        }
        if (x.region != 0) {
            t.setUndefinedScript((Script)x.script);
            t.setUndefinedRegion((Region)x.region);
        }
        specializeRegion(ref t);
        if (t.LangID == 0) {
            t.LangID = _en;
        }
        return (t, default);
    }
    return (t, ErrMissingLikelyTagsData);
}

} partial class languageᴛTag {

internal static void setTagsFrom(this ref Tag t, Tag id) {
    t.LangID = id.LangID;
    t.ScriptID = id.ScriptID;
    t.RegionID = id.RegionID;
}

internal static (Tag, error) minimize(this Tag tʗp) {
    ref var t = ref heap(tʗp, out var Ꮡt);

    (t, var err) = minimizeTags(t);
    if (err != nil) {
        return (t, err);
    }
    Ꮡt.RemakeString();
    return (t, default);
}

} partial class language_package {

internal static (Tag, error) minimizeTags(Tag t) {
    if (t.equalTags(Und)) {
        return (t, default);
    }
    var (max, err) = addTags(t);
    if (err != nil) {
        return (t, err);
    }
    foreach (var (_, id) in range(new Tag[]{
        new(LangID: t.LangID),
        new(LangID: t.LangID, RegionID: t.RegionID),
        new(LangID: t.LangID, ScriptID: t.ScriptID)
    }.array())) {
        var (x, errΔ1) = addTags(id); if (errΔ1 == nil && max.equalTags(x)) {
            t.setTagsFrom(id);
            break;
        }
    }
    return (t, default);
}

} // end language_package
