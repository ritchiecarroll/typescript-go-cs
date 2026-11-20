// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

global using Options = go.github.com.go_json_experiment.json.@internal.jsonopts_package.Options;

namespace go.github.com.go_json_experiment.json;

using strings = strings_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using go.github.com.go_json_experiment.json.@internal;

partial class jsontext_package {

public static Options AllowDuplicateNames(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.AllowDuplicateNames | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.AllowDuplicateNames | 0));
    }
}

public static Options AllowInvalidUTF8(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.AllowInvalidUTF8 | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.AllowInvalidUTF8 | 0));
    }
}

public static Options EscapeForHTML(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.EscapeForHTML | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.EscapeForHTML | 0));
    }
}

public static Options EscapeForJS(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.EscapeForJS | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.EscapeForJS | 0));
    }
}

public static Options PreserveRawStrings(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.PreserveRawStrings | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.PreserveRawStrings | 0));
    }
}

public static Options CanonicalizeRawInts(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.CanonicalizeRawInts | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.CanonicalizeRawInts | 0));
    }
}

public static Options CanonicalizeRawFloats(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.CanonicalizeRawFloats | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.CanonicalizeRawFloats | 0));
    }
}

public static Options ReorderRawObjects(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.ReorderRawObjects | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.ReorderRawObjects | 0));
    }
}

public static Options SpaceAfterColon(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.SpaceAfterColon | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.SpaceAfterColon | 0));
    }
}

public static Options SpaceAfterComma(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.SpaceAfterComma | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.SpaceAfterComma | 0));
    }
}

public static Options Multiline(bool v) {
    if (v) {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.Multiline | 1));
    } else {
        return new jsonflags_BoolsᴠOptions((jsonflags.Bools)(jsonflags.Multiline | 0));
    }
}

public static Options WithIndent(@string indent) {
    switch (ᐧ) {
    case {} when indent == "\t"u8: {
        return ((jsonopts.Indent)(@string)"\t");
    }
    case {} when indent == "    "u8: {
        return ((jsonopts.Indent)(@string)"    ");
    }
    case {} when indent == "   "u8: {
        return ((jsonopts.Indent)(@string)"   ");
    }
    case {} when indent == "  "u8: {
        return ((jsonopts.Indent)(@string)"  ");
    }
    case {} when indent == " "u8: {
        return ((jsonopts.Indent)(@string)" ");
    }
    case {} when indent == ""u8: {
        return ((jsonopts.Indent)(@string)""u8);
    }}

    @string s = strings.Trim(indent, " \t"); if (len(s) > 0) {
        throw panic("json: invalid character " + jsonwire.QuoteRune(slice<byte>(s)) + " in indent");
    }
    return ((jsonopts.Indent)indent);
}

public static Options WithIndentPrefix(@string prefix) {
    @string s = strings.Trim(prefix, " \t"); if (len(s) > 0) {
        throw panic("json: invalid character " + jsonwire.QuoteRune(slice<byte>(s)) + " in indent prefix");
    }
    return ((jsonopts.IndentPrefix)prefix);
}

} // end jsontext_package
