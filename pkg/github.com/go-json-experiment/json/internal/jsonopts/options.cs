// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json.@internal;

using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using ꓸꓸꓸOptions = Span<jsonopts_package.Options>;
using static jsonopts_package;

partial class jsonopts_package {

partial interface Options {
    void JSONOptions(@internal.NotForPublicUse _);
}

partial interface experimentalFormatTagSupporter {
    bool ExperimentalSupportFormatTag();
}

partial struct Struct {
    public jsonflags.Flags Flags;
    /*embed*/ public CoderValues CoderValues;
    /*embed*/ public ArshalValues ArshalValues;
}

partial struct CoderValues {
    public @string Indent;
    public @string IndentPrefix;
    public int64 ByteLimit;
    public nint DepthLimit;
}

partial struct ArshalValues {
    public any Marshalers;
    public any Unmarshalers;
    public @string Format;
}

[GoLazy] partial class lazyᴛDefaultOptionsV2 { public static readonly ж<Struct> ᏑDefaultOptionsV2 = new StaticBox<Struct>(new Struct(
    Flags: new jsonflags.Flags(
        Presence: (uint64)jsonflags.DefaultV1Flags,
        Values: (uint64)0
    )
)); }
public static ref Struct DefaultOptionsV2 => ref ((StaticBox<Struct>)ᏑDefaultOptionsV2).ValueSlot;

[GoLazy] partial class lazyᴛDefaultOptionsV1 { public static readonly ж<Struct> ᏑDefaultOptionsV1 = new StaticBox<Struct>(new Struct(
    Flags: new jsonflags.Flags(
        Presence: (uint64)jsonflags.DefaultV1Flags,
        Values: (uint64)jsonflags.DefaultV1Flags
    )
)); }
public static ref Struct DefaultOptionsV1 => ref ((StaticBox<Struct>)ᏑDefaultOptionsV1).ValueSlot;

} partial class jsonoptsᴛStruct {

public static void JSONOptions(this ref Struct _Δp0, @internal.NotForPublicUse _Δp1) { }

} partial class jsonopts_package {

public static readonly ж<Func<Struct, Options, (any, bool)>> ᏑGetUnknownOption = new StaticBox<Func<Struct, Options, (any, bool)>>((Struct _Δp0, Options _Δp1) => {
    throw panic("unknown option");
});
public static ref Func<Struct, Options, (any, bool)> GetUnknownOption => ref ((StaticBox<Func<Struct, Options, (any, bool)>>)ᏑGetUnknownOption).ValueSlot;

public static (T, bool) GetOption<T>(Options opts, Func<T, Options> setter) {
    var (structOpts, ok) = opts._<ж<Struct>>(ᐧ);
    if (!ok) {
        ref var structOpts2 = ref heap(new Struct(), out var ᏑstructOpts2);
        structOpts2.Join(opts);
        structOpts = ᏑstructOpts2;
    }

    T zero = GoZero<T>();
    var switchᴛ1 = setter(zero);
    switch (switchᴛ1.type()) {
    case jsonflags.Bools opt: {
        var v = structOpts.Flags.Get(opt);
        var okΔ1 = structOpts.Flags.Has(opt);
        if (!okΔ1 && opt == jsonflags.StringifyNumbers && structOpts.Flags.Get(jsonflags.StringTag)) {
            return (((any)true)._<T>(), true);
        }
        return (((any)v)._<T>(), okΔ1);
    }
    case {} Δopt when Δopt._<experimentalFormatTagSupporter>(out var opt): {
        var v = structOpts.Flags.Get(jsonflags.FormatTagSupported);
        var okΔ2 = structOpts.Flags.Has(jsonflags.FormatTagSupported);
        return (((any)v)._<T>(), okΔ2);
    }
    case Indent opt: {
        if (!structOpts.Flags.Has(jsonflags.Indent)) {
            return (zero, false);
        }
        return (((any)structOpts.Indent)._<T>(), true);
    }
    case IndentPrefix opt: {
        if (!structOpts.Flags.Has(jsonflags.IndentPrefix)) {
            return (zero, false);
        }
        return (((any)structOpts.IndentPrefix)._<T>(), true);
    }
    case ByteLimit opt: {
        if (!structOpts.Flags.Has(jsonflags.ByteLimit)) {
            return (zero, false);
        }
        return (((any)structOpts.ByteLimit)._<T>(), true);
    }
    case DepthLimit opt: {
        if (!structOpts.Flags.Has(jsonflags.DepthLimit)) {
            return (zero, false);
        }
        return (((any)structOpts.DepthLimit)._<T>(), true);
    }
    default: {
        var opt = switchᴛ1;
        var (v, okΔ3) = GetUnknownOption(structOpts.Value, opt);
        return (v._<T>(), okΔ3);
    }}
}

public static readonly ж<Func<Struct, Options, Struct>> ᏑJoinUnknownOption = new StaticBox<Func<Struct, Options, Struct>>((Struct _Δp0, Options _Δp1) => {
    throw panic("unknown option");
});
public static ref Func<Struct, Options, Struct> JoinUnknownOption => ref ((StaticBox<Func<Struct, Options, Struct>>)ᏑJoinUnknownOption).ValueSlot;

} partial class jsonoptsᴛStruct {

public static void Join(this ref Struct dst, params ꓸꓸꓸOptions srcsʗp) {
    var srcs = srcsʗp.sslice();

    foreach (var (_, src) in range(srcs)) {
        switch (src.type()) {
        case null: {
            continue;
            break;
        }
        case jsonflags.Bools srcΔ1: {
            dst.Flags.Set(srcΔ1);
            break;
        }
        case {} ΔsrcΔ1 when ΔsrcΔ1._<experimentalFormatTagSupporter>(out var srcΔ1): {
            if (srcΔ1.ExperimentalSupportFormatTag()) {
                dst.Flags.Set((jsonflags.Bools)(jsonflags.FormatTagSupported | 1));
            } else {
                dst.Flags.Set((jsonflags.Bools)(jsonflags.FormatTagSupported | 0));
            }
            break;
        }
        case Indent srcΔ1: {
            dst.Flags.Set((jsonflags.Bools)((jsonflags.Bools)(jsonflags.Multiline | jsonflags.Indent) | 1));
            dst.Indent = (@string)srcΔ1;
            break;
        }
        case IndentPrefix srcΔ1: {
            dst.Flags.Set((jsonflags.Bools)((jsonflags.Bools)(jsonflags.Multiline | jsonflags.IndentPrefix) | 1));
            dst.IndentPrefix = (@string)srcΔ1;
            break;
        }
        case ByteLimit srcΔ1: {
            dst.Flags.Set((jsonflags.Bools)(jsonflags.ByteLimit | 1));
            dst.ByteLimit = (int64)srcΔ1;
            break;
        }
        case DepthLimit srcΔ1: {
            dst.Flags.Set((jsonflags.Bools)(jsonflags.DepthLimit | 1));
            dst.DepthLimit = (nint)srcΔ1;
            break;
        }
        case ж<Struct> srcΔ1: {
            dst.Flags.Join(srcΔ1.Flags);
            if (srcΔ1.Flags.Has(jsonflags.NonBooleanFlags)) {
                if (srcΔ1.Flags.Has(jsonflags.Indent)) {
                    dst.Indent = srcΔ1.Indent;
                }
                if (srcΔ1.Flags.Has(jsonflags.IndentPrefix)) {
                    dst.IndentPrefix = srcΔ1.IndentPrefix;
                }
                if (srcΔ1.Flags.Has(jsonflags.ByteLimit)) {
                    dst.ByteLimit = srcΔ1.ByteLimit;
                }
                if (srcΔ1.Flags.Has(jsonflags.DepthLimit)) {
                    dst.DepthLimit = srcΔ1.DepthLimit;
                }
                if (srcΔ1.Flags.Has(jsonflags.Marshalers)) {
                    dst.Marshalers = srcΔ1.Marshalers;
                }
                if (srcΔ1.Flags.Has(jsonflags.Unmarshalers)) {
                    dst.Unmarshalers = srcΔ1.Unmarshalers;
                }
                if (srcΔ1.Flags.Has(jsonflags.FormatTag)) {
                    dst.Format = srcΔ1.Format;
                }
            }
            break;
        }
        default: {
            var srcΔ1 = src;
            dst = JoinUnknownOption(dst, srcΔ1);
            break;
        }}
    }
}

public static void InitializeMultiline(this ref Struct s) {
    if (!s.Flags.Has(jsonflags.SpaceAfterColon)) {
        s.Flags.Set((jsonflags.Bools)(jsonflags.SpaceAfterColon | 1));
    }
    if (!s.Flags.Has(jsonflags.SpaceAfterComma)) {
        s.Flags.Set((jsonflags.Bools)(jsonflags.SpaceAfterComma | 0));
    }
    if (!s.Flags.Has(jsonflags.Indent)) {
        s.Flags.Set((jsonflags.Bools)(jsonflags.Indent | 1));
        s.Indent = "\t";
    }
}

} partial class jsonopts_package {

public static bool ChangedWhitespace(Struct s1, Struct s2) {
    return s1.Flags.Get(jsonflags.Multiline) != s2.Flags.Get(jsonflags.Multiline) || s1.Flags.Get(jsonflags.SpaceAfterColon) != s2.Flags.Get(jsonflags.SpaceAfterColon) || s1.Flags.Get(jsonflags.SpaceAfterComma) != s2.Flags.Get(jsonflags.SpaceAfterComma) || (s2.Flags.Get(jsonflags.Multiline) && (s1.Indent != s2.Indent || s1.IndentPrefix != s2.IndentPrefix));
}

partial struct Indent /*@string*/;

partial struct IndentPrefix /*@string*/;

partial struct ByteLimit /*num:int64*/;

partial struct DepthLimit /*num:nint*/;

} partial class jsonoptsᴛIndent {

public static void JSONOptions(this Indent _Δp0, @internal.NotForPublicUse _Δp1) { }

} partial class jsonoptsᴛIndentPrefix {

public static void JSONOptions(this IndentPrefix _Δp0, @internal.NotForPublicUse _Δp1) { }

} partial class jsonoptsᴛByteLimit {

public static void JSONOptions(this ByteLimit _Δp0, @internal.NotForPublicUse _Δp1) { }

} partial class jsonoptsᴛDepthLimit {

public static void JSONOptions(this DepthLimit _Δp0, @internal.NotForPublicUse _Δp1) { }

} // end jsonoptsᴛDepthLimit
