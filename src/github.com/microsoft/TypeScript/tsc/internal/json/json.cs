global using Value = go.github.com.go_json_experiment.json.jsontext_package.Value;
global using Kind = go.github.com.go_json_experiment.json.jsontext_package.Kind;
global using UnmarshalerFrom = go.github.com.go_json_experiment.json_package.UnmarshalerFrom;
global using MarshalerTo = go.github.com.go_json_experiment.json_package.MarshalerTo;
global using Decoder = go.github.com.go_json_experiment.json.jsontext_package.Decoder;
global using Encoder = go.github.com.go_json_experiment.json.jsontext_package.Encoder;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using io = io_package;
using slices = slices_package;
using json = go.github.com.go_json_experiment.json_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using go.github.com.go_json_experiment;
using go.github.com.go_json_experiment.json;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;

partial class json_package {

internal static slice<jsonopts.Options> allowInvalid = slices.Clip<slice<jsonopts.Options>, jsonopts.Options>(slice<jsonopts.Options>([jsontext.AllowInvalidUTF8(true)]));

public static (slice<byte> @out, error err) Marshal(any @in, params Span<jsonopts.Options> optsʗp) {
    var opts = optsʗp.slice();

    if (len(opts) == 0) {
        opts = allowInvalid;
    } else {
        opts = appendꓸꓸꓸ(allowInvalid, opts);
    }
    return json.Marshal(@in, opts.ꓸꓸꓸ);
}

public static error /*err*/ MarshalEncode(ж<jsontext.Encoder> @out, any @in, params Span<jsonopts.Options> optsʗp) {
    var opts = optsʗp.slice();

    if (len(opts) == 0) {
        opts = allowInvalid;
    } else {
        opts = appendꓸꓸꓸ(allowInvalid, opts);
    }
    return json.MarshalEncode(@out, @in, opts.ꓸꓸꓸ);
}

public static error /*err*/ MarshalWrite(io.Writer @out, any @in, params Span<jsonopts.Options> optsʗp) {
    var opts = optsʗp.slice();

    if (len(opts) == 0) {
        opts = allowInvalid;
    } else {
        opts = appendꓸꓸꓸ(allowInvalid, opts);
    }
    return json.MarshalWrite(@out, @in, opts.ꓸꓸꓸ);
}

public static (slice<byte> @out, error err) MarshalIndent(any @in, @string prefix, @string indent) {
    if (prefix == ""u8 && indent == ""u8) {
        return Marshal(@in);
    }
    return Marshal(@in, jsontext.WithIndentPrefix(prefix), jsontext.WithIndent(indent));
}

public static error /*err*/ MarshalIndentWrite(io.Writer @out, any @in, @string prefix, @string indent) {
    if (prefix == ""u8 && indent == ""u8) {
        return MarshalWrite(@out, @in);
    }
    return MarshalWrite(@out, @in, jsontext.WithIndentPrefix(prefix), jsontext.WithIndent(indent));
}

public static error /*err*/ Unmarshal(slice<byte> @in, any @out, params Span<jsonopts.Options> optsʗp) {
    var opts = optsʗp.sslice();

    return json.Unmarshal(@in, @out, opts.ꓸꓸꓸ);
}

public static error /*err*/ UnmarshalDecode(ж<jsontext.Decoder> @in, any @out, params Span<jsonopts.Options> optsʗp) {
    var opts = optsʗp.sslice();

    return json.UnmarshalDecode(@in, @out, opts.ꓸꓸꓸ);
}

public static error /*err*/ UnmarshalRead(io.Reader @in, any @out, params Span<jsonopts.Options> optsʗp) {
    var opts = optsʗp.sslice();

    return json.UnmarshalRead(@in, @out, opts.ꓸꓸꓸ);
}

public static jsonopts.Options AllowDuplicateNames(bool allow) {
    return jsontext.AllowDuplicateNames(allow);
}

public static jsonopts.Options Deterministic(bool v) {
    return json.Deterministic(v);
}

public static jsonopts.Options WithIndent(@string indent) {
    return jsontext.WithIndent(indent);
}

public static ж<jsontext.Decoder> NewDecoder(io.Reader r) {
    return jsontext.NewDecoder(r);
}

public static readonly ж<jsontext.Token> ᏑBeginObject = new StaticBox<jsontext.Token>(jsontext.BeginObject);
public static ref jsontext.Token BeginObject => ref ((StaticBox<jsontext.Token>)ᏑBeginObject).ValueSlot;
public static readonly ж<jsontext.Token> ᏑEndObject = new StaticBox<jsontext.Token>(jsontext.EndObject);
public static ref jsontext.Token EndObject => ref ((StaticBox<jsontext.Token>)ᏑEndObject).ValueSlot;
public static readonly ж<jsontext.Token> ᏑNull = new StaticBox<jsontext.Token>(jsontext.Null);
public static ref jsontext.Token Null => ref ((StaticBox<jsontext.Token>)ᏑNull).ValueSlot;
public static readonly ж<jsontext.Token> ᏑBeginArray = new StaticBox<jsontext.Token>(jsontext.BeginArray);
public static ref jsontext.Token BeginArray => ref ((StaticBox<jsontext.Token>)ᏑBeginArray).ValueSlot;
public static readonly ж<jsontext.Token> ᏑEndArray = new StaticBox<jsontext.Token>(jsontext.EndArray);
public static ref jsontext.Token EndArray => ref ((StaticBox<jsontext.Token>)ᏑEndArray).ValueSlot;

} // end json_package
