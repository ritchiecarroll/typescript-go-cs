// Copyright 2018 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

using sort = sort_package;
using strings = strings_package;
using ꓸꓸꓸstring = Span<@string>;
using static language_package;

partial class language_package {

partial struct Builder {
    public Tag Tag;
    internal @string @private;
    internal slice<@string> variants;
    internal slice<@string> extensions;
}

} partial class languageᴛBuilder {

public static Tag Make(this ref Builder b) {
    ref var t = ref heap<Tag>(b.Tag, out var Ꮡt);

    if (len(b.extensions) > 0 || len(b.variants) > 0) {
        sort.Sort((sortVariants)b.variants);
        sort.Strings(b.extensions);

        if (b.@private != ""u8) {
            b.extensions = append(b.extensions, b.@private);
        }
        nint n = (nint)maxCoreSize + tokenLen(b.variants.ꓸꓸꓸ) + tokenLen(b.extensions.ꓸꓸꓸ);
        var buf = new slice<byte>(n);
        nint p = t.genCoreBytes(buf);
        t.pVariant = (byte)p;
        p += appendTokens(buf.slice(p), b.variants.ꓸꓸꓸ);
        t.pExt = (uint16)p;
        p += appendTokens(buf.slice(p), b.extensions.ꓸꓸꓸ);
        t.str = (@string)(buf.slice(0, p));
        ref var scan = ref heap<scanner>(makeScanner(buf.slice(0, p)), out var Ꮡscan);
        (t, _) = parse(Ꮡscan, "");
        return t;
    } else if (b.@private != ""u8) {
        t.str = b.@private;
        Ꮡt.RemakeString();
    }
    return t;
}

public static void SetTag(this ref Builder b, Tag t) {
    b.Tag.LangID = t.LangID;
    b.Tag.RegionID = t.RegionID;
    b.Tag.ScriptID = t.ScriptID;
    b.variants = b.variants[..0];
    @string variants = t.Variants(); if (variants != ""u8) {
        foreach (var (_, vr) in range(strings.Split(variants[1..], "-"))) {
            b.variants = append(b.variants, vr);
        }
    }
    (b.extensions, b.@private) = (b.extensions[..0], "");
    foreach (var (_, e) in range(t.Extensions())) {
        b.AddExt(e);
    }
}

public static void AddExt(this ref Builder b, @string e) {
    if (e[0] == 'x') {
        if (b.@private == ""u8) {
            b.@private = e;
        }
        return;
    }
    foreach (var (i, s) in range(b.extensions)) {
        if (s[0] == e[0]) {
            if (e[0] == 'u') {
                b.extensions[i] += e[1..];
            }
            return;
        }
    }
    b.extensions = append(b.extensions, e);
}

public static void SetExt(this ref Builder b, @string e) {
    if (e[0] == 'x') {
        b.@private = e;
        return;
    }
    foreach (var (i, s) in range(b.extensions)) {
        if (s[0] == e[0]) {
            if (e[0] == 'u') {
                b.extensions[i] = e + s[1..];
            } else {
                b.extensions[i] = e;
            }
            return;
        }
    }
    b.extensions = append(b.extensions, e);
}

public static void AddVariant(this ref Builder b, params ꓸꓸꓸstring vʗp) {
    var v = vʗp.sslice();

    foreach (var (_, vΔ1) in range(v)) {
        if (vΔ1 != ""u8) {
            b.variants = append(b.variants, vΔ1);
        }
    }
}

public static void ClearVariants(this ref Builder b) {
    b.variants = b.variants[..0];
}

public static void ClearExtensions(this ref Builder b) {
    b.@private = "";
    b.extensions = b.extensions[..0];
}

} partial class language_package {

internal static nint /*n*/ tokenLen(params ꓸꓸꓸstring tokenʗp) {
    nint n = default;
    var token = tokenʗp.sslice();

    foreach (var (_, t) in range(token)) {
        n += len(t) + 1;
    }
    return n;
}

internal static nint appendTokens(slice<byte> b, params ꓸꓸꓸstring tokenʗp) {
    var token = tokenʗp.sslice();

    nint p = 0;
    foreach (var (_, t) in range(token)) {
        b[p] = (byte)'-';
        copy(b.slice(p + 1), t);
        p += 1 + len(t);
    }
    return p;
}

partial struct sortVariants /*[]@string*/;

} partial class languageᴛsortVariants {

internal static nint Len(this sortVariants s) {
    return len(s);
}

internal static void Swap(this sortVariants s, nint i, nint j) {
    (s[j], s[i]) = (s[i], s[j]);
}

internal static bool Less(this sortVariants s, nint i, nint j) {
    return variantIndex[s[i]] < variantIndex[s[j]];
}

} // end languageᴛsortVariants
