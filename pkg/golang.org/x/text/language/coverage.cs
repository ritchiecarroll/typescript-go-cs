// Copyright 2014 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text;

using fmt = fmt_package;
using sort = sort_package;
using language = go.golang.org.x.text.@internal.language_package;
using go.golang.org.x.text.@internal;
using ꓸꓸꓸany = Span<any>;
using static language_package;

partial class language_package {

partial interface Coverage {
    slice<Tag> Tags();
    slice<Base> BaseLanguages();
    slice<Script> Scripts();
    slice<Region> Regions();
}

public static Coverage Supported = new allSubtags();

partial struct allSubtags {
}

} partial class languageᴛallSubtags {

internal static slice<Region> Regions(this allSubtags s) {
    var reg = new slice<Region>(language.NumRegions);
    foreach (var i in range(len(reg))) {
        reg[i] = new Region((language.Region)(uint16)(i + 1));
    }
    return reg;
}

internal static slice<Script> Scripts(this allSubtags s) {
    var scr = new slice<Script>(language.NumScripts);
    foreach (var i in range(len(scr))) {
        scr[i] = new Script((language.Script)(uint16)(i + 1));
    }
    return scr;
}

internal static slice<Base> BaseLanguages(this allSubtags s) {
    var bs = language.BaseLanguages();
    var @base = new slice<Base>(len(bs));
    foreach (var (i, b) in range(bs)) {
        @base[i] = new Base(b);
    }
    return @base;
}

internal static slice<Tag> Tags(this allSubtags s) {
    return default;
}

} partial class language_package {

partial struct coverage {
    internal Func<slice<Tag>> tags;
    internal Func<slice<Base>> bases;
    internal Func<slice<Script>> scripts;
    internal Func<slice<Region>> regions;
}

} partial class languageᴛcoverage {

internal static slice<Tag> Tags(this ref coverage s) {
    if (s.tags == nil) {
        return default;
    }
    return s.tags();
}

} partial class language_package {

partial struct bases /*[]Base*/;

} partial class languageᴛbases {

internal static nint Len(this bases b) {
    return len(b);
}

internal static void Swap(this bases b, nint i, nint j) {
    (b[i], b[j]) = (b[j], b[i]);
}

internal static bool Less(this bases b, nint i, nint j) {
    return b[i].langID < b[j].langID;
}

} partial class languageᴛcoverage {

internal static slice<Base> BaseLanguages(this ref coverage s) {
    if (s.bases == nil) {
        var tags = s.Tags();
        if (len(tags) == 0) {
            return default;
        }
        var a = new slice<Base>(len(tags));
        foreach (var (i, vᴛ1) in range(tags)) {
            ref var t = ref heap(new Tag(), out var Ꮡt);
            t = vᴛ1;

            a[i] = new Base(Ꮡt.lang());
        }
        sort.Sort((bases)a);
        nint k = 0;
        for (nint i = 1; i < len(a); i++) {
            if (a[k] != a[i]) {
                k++;
                a[k] = a[i];
            }
        }
        return a.slice(0, k + 1);
    }
    return s.bases();
}

internal static slice<Script> Scripts(this ref coverage s) {
    if (s.scripts == nil) {
        return default;
    }
    return s.scripts();
}

internal static slice<Region> Regions(this ref coverage s) {
    if (s.regions == nil) {
        return default;
    }
    return s.regions();
}

} partial class language_package {

public static Coverage NewCoverage(params ꓸꓸꓸany listʗp) {
    var list = listʗp.sslice();

    var s = Ꮡ(new coverage());
    foreach (var (_, x) in range(list)) {
        switch (x.type()) {
        case {} Δv when Δv._<Func<slice<Base>>>(out var v): {
            s.bases = v;
            break;
        }
        case {} Δv when Δv._<Func<slice<Script>>>(out var v): {
            s.scripts = v;
            break;
        }
        case {} Δv when Δv._<Func<slice<Region>>>(out var v): {
            s.regions = v;
            break;
        }
        case {} Δv when Δv._<Func<slice<Tag>>>(out var v): {
            s.tags = v;
            break;
        }
        case slice<Base> v: {
            var vʗ1 = v;
            s.bases = () => vʗ1;
            break;
        }
        case slice<Script> v: {
            var vʗ2 = v;
            s.scripts = () => vʗ2;
            break;
        }
        case slice<Region> v: {
            var vʗ3 = v;
            s.regions = () => vʗ3;
            break;
        }
        case slice<Tag> v: {
            var vʗ4 = v;
            s.tags = () => vʗ4;
            break;
        }
        default: {
            var v = x;
            throw panic(fmt.Sprintf("language: unsupported set type %T"u8, v));
            break;
        }}
    }
    return new coverageжCoverage(s);
}

} // end language_package
