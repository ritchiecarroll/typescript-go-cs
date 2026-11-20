// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text;

using errors = errors_package;
using sort = sort_package;
using strconv = strconv_package;
using strings = strings_package;
using language = go.golang.org.x.text.@internal.language_package;
using compact = go.golang.org.x.text.@internal.language.compact_package;
using go.golang.org.x.text.@internal;
using ꓸꓸꓸany = Span<any>;
using static language_package;

partial class language_package {

partial interface ValueError :
    error
{
    @string Subtag();
}

public static (Tag t, error err) Parse(@string s) {
    return Default.Parse(s);
}

} partial class languageᴛCanonType {

public static (Tag t, error err) Parse(this CanonType c, @string s) {
    Tag t = default;
    heap<error>(out var Ꮡerr);
    GoFrame ᒐ = default;
    try {
        ref var err = ref Ꮡerr.ValueSlot;

        defer(() => {
            if (recover() != nil) {
                t = new Tag(new compact.Tag());
                Ꮡerr.ValueSlot = language.ErrSyntax;
            }
        }, ref ᒐ);

        ref var tt = ref heap<language.Tag>(out var Ꮡtt);
        (tt, err) = language.Parse(s);
        if (err != nil) {
            (t, err) = (makeTag(tt), err); goto ᒐdone;
        }
        (tt, var changed) = canonicalize(c, tt);
        if (changed) {
            Ꮡtt.RemakeString();
        }
        (t, err) = (makeTag(tt), default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (t, Ꮡerr.ValueSlot);
}

} partial class language_package {

public static (Tag t, error err) Compose(params ꓸꓸꓸany partʗp) {
    var part = partʗp.sslice();

    return Default.Compose(part.ꓸꓸꓸ);
}

} partial class languageᴛCanonType {

public static (Tag t, error err) Compose(this CanonType c, params ꓸꓸꓸany partʗp) {
    Tag t = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        var part = partʗp.sslice();

        defer(() => {
            if (recover() != nil) {
                t = new Tag(new compact.Tag());
                err = language.ErrSyntax;
            }
        }, ref ᒐ);

        ref var b = ref heap(new language.Builder(), out var Ꮡb);
        err = update(Ꮡb, part.ꓸꓸꓸ); if (err != nil) {
            (t, err) = (und, err); goto ᒐdone;
        }
        (b.Tag, _) = canonicalize(c, b.Tag);
        (t, err) = (makeTag(b.Make()), err);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (t, err);
}

} partial class language_package {

internal static error errInvalidArgument = errors.New("invalid Extension or Variant");

internal static error /*err*/ update(ж<language.Builder> Ꮡb, params ꓸꓸꓸany partʗp) {
    error err = default;
    var part = partʗp.sslice();
    ref var b = ref Ꮡb.DerefOrNull();

    foreach (var (_, x) in range(part)) {
        switch (x.type()) {
        case Tag vᴛ1: {
            ref var v = ref heap(vᴛ1, out var Ꮡv);
            b.SetTag(Ꮡv.tag());
            break;
        }
        case Base v: {
            b.Tag.LangID = v.langID;
            break;
        }
        case Script v: {
            b.Tag.ScriptID = v.scriptID;
            break;
        }
        case Region v: {
            b.Tag.RegionID = v.regionID;
            break;
        }
        case Variant v: {
            if (v.variant == ""u8) {
                err = errInvalidArgument;
                break;
            }
            b.AddVariant(v.variant);
            break;
        }
        case Extension v: {
            if (v.s == ""u8) {
                err = errInvalidArgument;
                break;
            }
            b.SetExt(v.s);
            break;
        }
        case slice<Variant> v: {
            b.ClearVariants();
            foreach (var (_, vΔ1) in range(v)) {
                b.AddVariant(vΔ1.variant);
            }
            break;
        }
        case slice<Extension> v: {
            b.ClearExtensions();
            foreach (var (_, e) in range(v)) {
                b.SetExt(e.s);
            }
            break;
        }
        case {} Δv when Δv._<error>(out var v): {
            if (v != nil) {
                err = v;
            }
            break;
        }}
    }
    return err;
}

internal static error errInvalidWeight = errors.New("ParseAcceptLanguage: invalid weight");
internal static error errTagListTooLarge = errors.New("tag list exceeds max length");

public static (slice<Tag> tag, slice<float32> q, error err) ParseAcceptLanguage(@string s) {
    slice<Tag> tag = default;
    slice<float32> q = default;
    error err = default;
    GoFrame ᒐ = default;
    try {
        defer(() => {
            if (recover() != nil) {
                tag = default;
                q = default;
                err = language.ErrSyntax;
            }
        }, ref ᒐ);

        if (strings.Count(s, "-") > 1000) {
            (tag, q, err) = (default, default, errTagListTooLarge); goto ᒐdone;
        }

        @string entry = default;
        while (s != ""u8) {
            (entry, s) = split(s, (byte)','); if (entry == ""u8) {
                continue;
            }

            var (entryΔ1, weight) = split(entry, (byte)';');

            var (t, errΔ1) = Parse(entryΔ1);
            if (errΔ1 != nil) {
                var (id, ok) = acceptFallback[entryΔ1, ꟷ];
                if (!ok) {
                    (tag, q, err) = (default, default, errΔ1); goto ᒐdone;
                }
                t = makeTag(new language.Tag(LangID: id));
            }

            var w = 1.0D;
            if (weight != ""u8) {
                weight = consume(weight, (byte)'q');
                weight = consume(weight, (byte)'=');
                (w, errΔ1) = strconv.ParseFloat(weight, 32); if (errΔ1 != nil) {
                    (tag, q, err) = (default, default, errInvalidWeight); goto ᒐdone;
                }
                if (w <= 0D) {
                    continue;
                }
            }

            tag = append(tag, t);
            q = append(q, (float32)w);
        }
        sort.Stable(new tagSortжInterface(Ꮡ(new tagSort(tag, q))));
        (tag, q, err) = (tag, q, default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
    ᒐdone: return (tag, q, err);
}

internal static @string consume(@string s, byte c) {
    if (s == ""u8 || s[0] != c) {
        return "";
    }
    return strings.TrimSpace(s[1..]);
}

internal static (@string head, @string tail) split(@string s, byte c) {
    nint i = strings.IndexByte(s, c); if (i >= 0) {
        return (strings.TrimSpace(s.slice(0, i)), strings.TrimSpace(s.slice(i + 1)));
    }
    return (strings.TrimSpace(s), "");
}

[GoLazy] partial class lazyᴛacceptFallback { internal static map<@string, language.Language> acceptFallback = new map<@string, language.Language>{
    ["english"] = _en,
    ["deutsch"] = _de,
    ["italian"] = _it,
    ["french"] = _fr,
    ["*"] = _mul
}; }

partial struct tagSort {
    internal slice<Tag> tag;
    internal slice<float32> q;
}

} partial class languageᴛtagSort {

internal static nint Len(this ref tagSort s) {
    return len(s.q);
}

internal static bool Less(this ref tagSort s, nint i, nint j) {
    return s.q[i] > s.q[j];
}

internal static void Swap(this ref tagSort s, nint i, nint j) {
    (s.tag[i], s.tag[j]) = (s.tag[j], s.tag[i]);
    (s.q[i], s.q[j]) = (s.q[j], s.q[i]);
}

} // end languageᴛtagSort
