// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

global using Options = go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options;

namespace go.github.com.go_json_experiment;

using fmt = fmt_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using go.github.com.go_json_experiment.json;
using go.github.com.go_json_experiment.json.@internal;
using ꓸꓸꓸOptions = Span<Options>;
using static json_package;

partial class json_package {

public static Options JoinOptions(params ꓸꓸꓸOptions srcsʗp) {
    var srcs = srcsʗp.sslice();

    ref var dst = ref heap(new jsonopts.Struct(), out var Ꮡdst);
    dst.Join(srcs.ꓸꓸꓸ);
    return new jsonopts.StructжOptions(Ꮡdst);
}

public static (T, bool) GetOption<T>(Options opts, Func<T, Options> setter) {
    return jsonopts.GetOption(opts, setter);
}

public static Options DefaultOptionsV2() {
    return new jsonopts.StructжOptions(jsonopts.ᏑDefaultOptionsV2);
}

public static Options StringifyNumbers(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.StringifyNumbers | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.StringifyNumbers | 0));
    }
}

public static Options Deterministic(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.Deterministic | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.Deterministic | 0));
    }
}

public static Options FormatNilSliceAsNull(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.FormatNilSliceAsNull | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.FormatNilSliceAsNull | 0));
    }
}

public static Options FormatNilMapAsNull(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.FormatNilMapAsNull | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.FormatNilMapAsNull | 0));
    }
}

public static Options OmitZeroStructFields(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.OmitZeroStructFields | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.OmitZeroStructFields | 0));
    }
}

public static Options MatchCaseInsensitiveNames(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.MatchCaseInsensitiveNames | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.MatchCaseInsensitiveNames | 0));
    }
}

public static Options RejectUnknownMembers(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.RejectUnknownMembers | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.RejectUnknownMembers | 0));
    }
}

public static Options WithMarshalers(ж<Marshalers> v) {
    return new marshalersOptionжOptions(v.Reinterpret<Marshalers, marshalersOption>());
}

public static Options WithUnmarshalers(ж<Unmarshalers> v) {
    return new unmarshalersOptionжOptions(v.Reinterpret<Unmarshalers, unmarshalersOption>());
}

partial struct marshalersOption /*typedArshalers<global::go.github.com.go_json_experiment.json.jsontext_package.Encoder>*/;

partial struct unmarshalersOption /*typedArshalers<global::go.github.com.go_json_experiment.json.jsontext_package.Decoder>*/;

} partial class jsonᴛmarshalersOption {

internal static void JSONOptions(this ref marshalersOption _Δp0, @internal.NotForPublicUse _Δp1) { }

} partial class jsonᴛunmarshalersOption {

internal static void JSONOptions(this ref unmarshalersOption _Δp0, @internal.NotForPublicUse _Δp1) { }

} partial class json_package {

[GoInit] internal static void init() {
    jsonopts.GetUnknownOption = (jsonopts.Struct src, jsonopts.Options zero) => {
        switch (zero.type()) {
        case ж<marshalersOption>: {
            if (!src.Flags.Has(jsonflags.Marshalers)) {
                return ((ж<Marshalers>)nil, false);
            }
            return (src.Marshalers._<ж<Marshalers>>().OrTypedNil(), true);
        }
        case ж<unmarshalersOption>: {
            if (!src.Flags.Has(jsonflags.Unmarshalers)) {
                return ((ж<Unmarshalers>)nil, false);
            }
            return (src.Unmarshalers._<ж<Unmarshalers>>().OrTypedNil(), true);
        }
        default: {
            throw panic(fmt.Sprintf("unknown option %T"u8, zero));
            break;
        }}

    };
    jsonopts.JoinUnknownOption = (jsonopts.Struct dst, jsonopts.Options src) => {
        switch (src.type()) {
        case ж<marshalersOption> srcΔ1: {
            dst.Flags.Set((jsonflags.Bools)(jsonflags.Marshalers | 1));
            dst.Marshalers = srcΔ1.Reinterpret<marshalersOption, Marshalers>().OrTypedNil();
            break;
        }
        case ж<unmarshalersOption> srcΔ1: {
            dst.Flags.Set((jsonflags.Bools)(jsonflags.Unmarshalers | 1));
            dst.Unmarshalers = srcΔ1.Reinterpret<unmarshalersOption, Unmarshalers>().OrTypedNil();
            break;
        }
        default: {
            var srcΔ1 = src;
            throw panic(fmt.Sprintf("unknown option %T"u8, srcΔ1));
            break;
        }}
        return dst;
    };
}

} // end json_package
