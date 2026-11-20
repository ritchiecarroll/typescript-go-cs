// Copyright 2021 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment;

using cmp = cmp_package;
using errors = errors_package;
using fmt = fmt_package;
using io = io_package;
using reflect = reflect_package;
using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using unicode = unicode_package;
using utf8 = go.unicode.utf8_package;
using jsonflags = go.github.com.go_json_experiment.json.@internal.jsonflags_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using go.github.com.go_json_experiment.json.@internal;
using go.unicode;
using ꓸꓸꓸany = Span<any>;
using static json_package;

partial class json_package {

public partial interface isZeroer {
    bool IsZero();
}

internal static reflect.Type isZeroerType = reflect.TypeFor<isZeroer>();

partial struct structFields {
    internal slice<structField> flattened;
    internal map<@string, ж<structField>> byActualName;
    internal map<@string, slice<ж<structField>>> byFoldedName;
    internal ж<structField> embeddedFallback;
    internal ж<SemanticError> errUnsupportedFormat;
}

} partial class jsonᴛstructFields {

internal static void reindex(this ref structFields sf) {
    void reindex(ж<structField> f) {
        f.index0 = f.index[0];
        f.index = f.index[1..];
        if (len(f.index) == 0) {
            f.index = default;
        }
    }
    foreach (var i in range(len(sf.flattened))) {
        reindex(Ꮡ(sf.flattened, i));
    }
    if (sf.embeddedFallback != nil) {
        reindex(sf.embeddedFallback);
    }
}

internal static slice<ж<structField>> lookupByFoldedName(this ref structFields fs, slice<byte> name) {
    return fs.byFoldedName[tmpstring(foldName(name))];
}

} partial class json_package {

partial struct structField {
    internal nint id;
    internal nint index0;
    internal slice<nint> index;
    internal reflect.Type typ;
    internal ж<arshaler> fncs;
    internal Func<addressableValue, bool> isZero;
    internal Func<addressableValue, bool> isEmpty;
    /*embed*/ internal fieldOptions fieldOptions;
}

internal static error errNoExportedFields = errors.New("Go struct has no exported fields");

internal partial struct makeStructFields_queueEntry /*dyn*/ {
    internal reflect.Type typ;
    internal slice<nint> index;
    internal bool visitChildren;
}

internal static (structFields fs, ж<SemanticError> serr) makeStructFields(reflect.Type root) {
    structFields fs = default;
    ж<SemanticError> serr = default;

    ж<SemanticError> orErrorf(ж<SemanticError> serrΔ1, reflect.Type t, @string f, params ꓸꓸꓸany aʗp) {
        var a = aʗp.sslice();
        return cmp.Or(serrΔ1, Ꮡ(new SemanticError(ΔGoType: t, Err: fmt.Errorf(f, a.ꓸꓸꓸ))));
    }

    nint queueIndex = default;
    ref var queue = ref heap<slice<makeStructFields_queueEntry>>(slice<makeStructFields_queueEntry>([new(root, default, true)]), out var Ꮡqueue);
    var seen = new map<reflect.Type, bool>{[root] = true};

    ref var allFields = ref heap<slice<structField>>(out var ᏑallFields);
    ref var embeddedFallbacks = ref heap<slice<structField>>(out var ᏑembeddedFallbacks);
    while (queueIndex < len(queue)) {
        ref var qe = ref heap<makeStructFields_queueEntry>(queue[queueIndex], out var Ꮡqe);
        queueIndex++;

        var t = qe.typ;
        nint embeddedFallbackIndex = -1;
        var namesIndex = new map<@string, nint>();
        bool hasAnyJSONTag = default;
        bool hasAnyJSONField = default;
        foreach (var i in range(t.NumField())) {
            ref var sf = ref heap<reflect.StructField>(t.Field(i), out var Ꮡsf);
            var (_, hasTag) = sf.Tag.Lookup("json");
            hasAnyJSONTag = hasAnyJSONTag || hasTag;
            var (options, ignored, err) = parseFieldOptions(sf);
            if (err != nil) {
                serr = cmp.Or(serr, Ꮡ(new SemanticError(ΔGoType: t, Err: err)));
            }
            if (ignored) {
                continue;
            }
            hasAnyJSONField = true;
            ref var f = ref heap<structField>(new structField(
                index: append(appendꓸꓸꓸ(new slice<nint>(0, len(qe.index) + 1), qe.index), i),
                typ: sf.Type,
                fieldOptions: options
            ), out var Ꮡf);
            if (sf.Anonymous && !f.hasName) {
                if (indirectType(f.typ).Kind() != reflect.Struct) {
                    serr = orErrorf(serr, t, "embedded Go struct field %s of non-struct type must be explicitly given a JSON name", sf.Name);
                } else {
                    f.embed = true;
                }
            }

            Action handleEmbed = default;
            ref var handleField = ref heap<Action>(out var ᏑhandleField);
            var orErrorfʗ1 = orErrorf;
            var qeʗ1 = qe;
            var seenʗ1 = seen;
            var sfʗ1 = sf;
            var tʗ1 = t;
            handleEmbed = () => {
                if (Ꮡf.Value.fieldOptions != (new fieldOptions(name: Ꮡf.Value.name, quotedName: Ꮡf.Value.quotedName, embed: true))) {
                    serr = orErrorfʗ1(serr, tʗ1, "Go struct field %s cannot have any options other than `embed` specified", sfʗ1.Name);
                    if (Ꮡf.Value.hasName) {
                        ᏑhandleField.ValueSlot();
                        return;
                    }
                    Ꮡf.Value.fieldOptions = new fieldOptions(name: Ꮡf.Value.name, quotedName: Ꮡf.Value.quotedName, embed: Ꮡf.Value.embed);
                }

                var tf = indirectType(Ꮡf.Value.typ);
                if (implementsAny(tf, allMethodTypes.ꓸꓸꓸ) && !AreEqual(tf, jsontextValueType)) {
                    serr = orErrorfʗ1(serr, tʗ1, "embedded Go struct field %s of type %s must not implement marshal or unmarshal methods", sfʗ1.Name, tf);
                }

                if (tf.Kind() == reflect.Struct) {
                    if (qeʗ1.visitChildren) {
                        Ꮡqueue.ValueSlot = append(Ꮡqueue.ValueSlot, new makeStructFields_queueEntry(tf, Ꮡf.Value.index, !seenʗ1[tf]));
                    }
                    seenʗ1[tf] = true;
                    return;
                } else if (!sfʗ1.IsExported()) {
                    serr = orErrorfʗ1(serr, tʗ1, "embedded Go struct field %s is not exported", sfʗ1.Name);
                    return;
                }

                switch (ᐧ) {
                case {} when AreEqual(tf, jsontextValueType): {
                    Ꮡf.Value.fncs = default;
                    break;
                }
                case {} when tf.Kind() == reflect.Map && tf.Key().Kind() == reflect.String: {
                    if (implementsAny(tf.Key(), allMethodTypes.ꓸꓸꓸ)) {
                        serr = orErrorfʗ1(serr, tʗ1, "embedded map field %s of type %s must have a string key that does not implement marshal or unmarshal methods", sfʗ1.Name, tf);
                        ᏑhandleField.ValueSlot();
                        return;
                    }
                    Ꮡf.Value.fncs = lookupArshaler(tf.Elem());
                    break;
                }
                default: {
                    serr = orErrorfʗ1(serr, tʗ1, "embedded Go struct field %s of type %s must be a Go struct, Go map of string key, or jsontext.Value", sfʗ1.Name, tf);
                    ᏑhandleField.ValueSlot();
                    return;
                }}

                if (embeddedFallbackIndex >= 0) {
                    serr = orErrorfʗ1(serr, tʗ1, "embedded Go struct fields %s and %s cannot both be a Go map or jsontext.Value", tʗ1.Field(embeddedFallbackIndex).Name, sfʗ1.Name);
                }
                embeddedFallbackIndex = i;

                ᏑembeddedFallbacks.ValueSlot = append(ᏑembeddedFallbacks.ValueSlot, Ꮡf.Value);
            };
            var namesIndexʗ1 = namesIndex;
            var orErrorfʗ2 = orErrorf;
            var sfʗ2 = sf;
            var tʗ2 = t;
            handleField = () => {
                if (!sfʗ2.IsExported()) {
                    var tf = indirectType(Ꮡf.Value.typ);
                    if (!(sfʗ2.Anonymous && tf.Kind() == reflect.Struct)) {
                        serr = orErrorfʗ2(serr, tʗ2, "Go struct field %s is not exported", sfʗ2.Name);
                        return;
                    }
                    if (implementsAny(tf, allMethodTypes.ꓸꓸꓸ) || (Ꮡf.Value.omitzero && implementsAny(tf, isZeroerType))) {
                        serr = orErrorfʗ2(serr, tʗ2, "Go struct field %s is not exported for method calls", sfʗ2.Name);
                        return;
                    }
                }

                switch (ᐧ) {
                case {} when sfʗ2.Type.Kind() == reflect.Interface && sfʗ2.Type.Implements(isZeroerType): {
                    Ꮡf.Value.isZero = (addressableValue va) => va.Value.IsNil() || (va.Value.Elem().Kind() == reflect.Pointer && va.Value.Elem().IsNil()) || va.Value.Interface()._<isZeroer>().IsZero();
                    break;
                }
                case {} when sfʗ2.Type.Kind() == reflect.Pointer && sfʗ2.Type.Implements(isZeroerType): {
                    Ꮡf.Value.isZero = (addressableValue va) => va.Value.IsNil() || va.Value.Interface()._<isZeroer>().IsZero();
                    break;
                }
                case {} when sfʗ2.Type.Implements(isZeroerType): {
                    Ꮡf.Value.isZero = (addressableValue va) => va.Value.Interface()._<isZeroer>().IsZero();
                    break;
                }
                case {} when reflect.PointerTo(sfʗ2.Type).Implements(isZeroerType): {
                    Ꮡf.Value.isZero = (addressableValue va) => va.Value.Addr().Interface()._<isZeroer>().IsZero();
                    break;
                }}

                switch (sfʗ2.Type.Kind()) {
                case var ᴛ when ᴛ == reflect.String || ᴛ == reflect.Map || ᴛ == reflect.Array || ᴛ == reflect.Slice: {
                    Ꮡf.Value.isEmpty = (addressableValue va) => va.Value.Len() == 0;
                    break;
                }
                case var ᴛ when ᴛ == reflect.Pointer || ᴛ == reflect.Interface: {
                    Ꮡf.Value.isEmpty = (addressableValue va) => va.Value.IsNil();
                    break;
                }}

                var (j, ok) = namesIndexʗ1[Ꮡf.Value.name, ꟷ]; if (ok) {
                    serr = orErrorfʗ2(serr, tʗ2, "Go struct fields %s and %s conflict over JSON object name %q", tʗ2.Field(j).Name, sfʗ2.Name, Ꮡf.Value.name);
                }
                namesIndexʗ1[Ꮡf.Value.name] = i;

                Ꮡf.Value.id = len(ᏑallFields.ValueSlot);
                Ꮡf.Value.fncs = lookupArshaler(sfʗ2.Type);
                ᏑallFields.ValueSlot = append(ᏑallFields.ValueSlot, Ꮡf.Value);
                if (Ꮡf.Value.format != ""u8 && fs.errUnsupportedFormat == nil) {
                    fs.errUnsupportedFormat = Ꮡ(new SemanticError(ΔGoType: tʗ2, Err: fmt.Errorf("Go struct field %s has unsupported `format` tag option"u8, sfʗ2.Name)));
                }
            };

            if (f.embed) {
                handleEmbed();
            } else {
                handleField();
            }
        }

        var isEmptyStruct = t.NumField() == 0;
        if (!isEmptyStruct && !hasAnyJSONTag && !hasAnyJSONField) {
            serr = cmp.Or(serr, Ꮡ(new SemanticError(ΔGoType: t, Err: errNoExportedFields)));
        }
    }

    var flattened = allFields[..0];
    slices.SortStableFunc(allFields, (structField x, structField y) => cmp.Or(
            strings.Compare(x.name, y.name),
            cmp.Compare(len(x.index), len(y.index)),
            boolsCompare(!x.hasName, !y.hasName)));
    while (len(allFields) > 0) {
        nint n = 1;
        while (n < len(allFields) && allFields[n - 1].name == allFields[n].name) {
            n++;
        }
        if (n == 1 || len(allFields[0].index) != len(allFields[1].index) || allFields[0].hasName != allFields[1].hasName) {
            flattened = append(flattened, allFields[0]);
        }
        allFields = allFields.slice(n);
    }

    slices.SortFunc(flattened, (structField x, structField y) => cmp.Compare(x.id, y.id));
    foreach (var i in range(len(flattened))) {
        flattened[i].id = i;
    }

    slices.SortFunc(flattened, (structField x, structField y) => slices.Compare<slice<nint>, nint>(x.index, y.index));

    fs = new structFields(
        flattened: flattened,
        byActualName: new map<@string, ж<structField>>(len(flattened)),
        byFoldedName: new map<@string, slice<ж<structField>>>(len(flattened)),

        errUnsupportedFormat: fs.errUnsupportedFormat
    );
    foreach (var (i, f) in range(fs.flattened)) {
        @string foldedName = (@string)foldName(slice<byte>(f.name));
        fs.byActualName[f.name] = Ꮡ(fs.flattened, i);
        fs.byFoldedName[foldedName] = append(fs.byFoldedName[foldedName], Ꮡ(fs.flattened, i));
    }
    foreach (var (foldedName, fields) in fs.byFoldedName) {
        if (len(fields) > 1) {
            slices.SortFunc(fields, (ж<structField> x, ж<structField> y) => cmp.Compare(x.id, y.id));
            fs.byFoldedName[foldedName] = fields;
        }
    }
    {
        nint n = len(embeddedFallbacks); if (n == 1 || (n > 1 && len(embeddedFallbacks[0].index) != len(embeddedFallbacks[1].index))) {
            fs.embeddedFallback = Ꮡ(embeddedFallbacks, 0);
        }
    }
    fs.reindex();
    return (fs, serr);
}

internal static reflect.Type indirectType(reflect.Type t) {
    if (t.Kind() == reflect.Pointer && t.Name() == ""u8) {
        t = t.Elem();
    }
    return t;
}

} partial class jsonᴛstructField {

internal static bool matchFoldedName(this ref structField f, slice<byte> name, ж<jsonflags.Flags> flags) {
    if (f.casing == caseIgnore || (flags.Get(jsonflags.MatchCaseInsensitiveNames) && f.casing != caseStrict)) {
        if (!flags.Get(jsonflags.MatchCaseSensitiveDelimiter) || strings.EqualFold((@string)name, f.name)) {
            return true;
        }
    }
    return false;
}

} partial class json_package {

internal static UntypedInt caseIgnore => 1;
internal static UntypedInt caseStrict => 2;

partial struct fieldOptions {
    internal @string name;
    internal @string quotedName;
    internal bool hasName;
    internal bool nameNeedEscape;
    internal int8 casing;
    internal bool embed;
    internal bool omitzero;
    internal bool omitempty;
    internal bool @string;
    internal @string format;
}

internal static (fieldOptions @out, bool ignored, error err) parseFieldOptions(reflect.StructField sf) {
    fieldOptions @out = default;
    error err = default;

    var (tag, hasTag) = sf.Tag.Lookup("json");

    if (tag == "-"u8) {
        return (new fieldOptions(), true, default);
    }

    if (!sf.IsExported() && !sf.Anonymous) {
        if (hasTag) {
            err = cmp.Or(err, fmt.Errorf("unexported Go struct field %s cannot have non-ignored `json:%q` tag"u8, sf.Name, tag));
        }
        return (new fieldOptions(), true, err);
    }

    @out.name = sf.Name;
    if (len(tag) > 0 && !strings.HasPrefix(tag, ",")) {
        nint n = len(tag) - len(strings.TrimLeftFunc(tag, (rune r) => !strings.ContainsRune(",\\'\"`", r)));
        @string name = tag.slice(0, n);

        error err2 = default;
        if (!strings.HasPrefix(tag.slice(n), ",") && len(name) != len(tag)) {
            (name, n, err2) = consumeTagOption(tag, false);
            if (err2 != nil) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s has malformed `json` tag: %v"u8, sf.Name, err2));
            }
        }
        if (!utf8.ValidString(name)) {
            err = cmp.Or(err, fmt.Errorf("Go struct field %s has JSON object name %q with invalid UTF-8"u8, sf.Name, name));
            name = (@string)slice<rune>(name);
        }
        if (err2 == nil) {
            @out.hasName = true;
            @out.name = name;
        }
        tag = tag.slice(n);
    }
    var (b, _) = jsonwire.AppendQuote(default, slice<byte>(@out.name), Ꮡ(new jsonflags.Flags()));
    @out.quotedName = (@string)b;
    @out.nameNeedEscape = jsonwire.NeedEscape(slice<byte>(@out.name));

    bool wasFormat = default;
    var seenOpts = new map<@string, bool>();
    while (len(tag) > 0) {
        if (tag[0] != ',') {
            err = cmp.Or(err, fmt.Errorf("Go struct field %s has malformed `json` tag: invalid character %q before next option (expecting ',')"u8, sf.Name, tag[0]));
        } else {
            tag = tag[(int)(len(","))..];
            if (len(tag) == 0) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s has malformed `json` tag: invalid trailing ',' character"u8, sf.Name));
                break;
            }
        }

        var (opt, n, err2) = consumeTagOption(tag, false);
        if (err2 != nil) {
            err = cmp.Or(err, fmt.Errorf("Go struct field %s has malformed `json` tag: %v"u8, sf.Name, err2));
        }
        @string rawOpt = tag.slice(0, n);
        tag = tag.slice(n);
        switch (ᐧ) {
        case {} when wasFormat: {
            err = cmp.Or(err, fmt.Errorf("Go struct field %s has `format` tag option that was not specified last"u8, sf.Name));
            break;
        }
        case {} when strings.HasPrefix(rawOpt, "'") && strings.TrimFunc(opt, isLetterOrDigit) == ""u8: {
            err = cmp.Or(err, fmt.Errorf("Go struct field %s has unnecessarily quoted appearance of `%s` tag option; specify `%s` instead"u8, sf.Name, rawOpt, opt));
            break;
        }}

        switch (ᐧ) {
        case {} when opt == "case"u8: {
            if (!strings.HasPrefix(tag, ":")) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s is missing value for `case` tag option; specify `case:ignore` or `case:strict` instead"u8, sf.Name));
                break;
            }
            tag = tag[(int)(len(":"))..];
            var (optΔ2, nΔ2, err2Δ2) = consumeTagOption(tag, false);
            if (err2Δ2 != nil) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s has malformed value for `case` tag option: %v"u8, sf.Name, err2Δ2));
                break;
            }
            @string rawOptΔ2 = tag.slice(0, nΔ2);
            tag = tag.slice(nΔ2);
            if (strings.HasPrefix(rawOptΔ2, "'")) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s has unnecessarily quoted appearance of `case:%s` tag option; specify `case:%s` instead"u8, sf.Name, rawOptΔ2, optΔ2));
            }
            switch (ᐧ) {
            case {} when optΔ2 == "ignore"u8: {
                @out.casing |= (int8)(caseIgnore);
                break;
            }
            case {} when optΔ2 == "strict"u8: {
                @out.casing |= (int8)(caseStrict);
                break;
            }
            default: {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s has unknown `case:%s` tag value"u8, sf.Name, rawOptΔ2));
                break;
            }}

            break;
        }
        case {} when opt == "embed"u8: {
            @out.embed = true;
            break;
        }
        case {} when opt == "omitzero"u8: {
            @out.omitzero = true;
            break;
        }
        case {} when opt == "omitempty"u8: {
            @out.omitempty = true;
            break;
        }
        case {} when opt == "string"u8: {
            @out.@string = true;
            break;
        }
        case {} when opt == "format"u8: {
            if (!strings.HasPrefix(tag, ":")) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s is missing value for `format` tag option"u8, sf.Name));
                break;
            }
            tag = tag[(int)(len(":"))..];
            var (optΔ3, nΔ3, err2Δ3) = consumeTagOption(tag, true);
            if (err2Δ3 != nil) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s has malformed value for `format` tag option: %v"u8, sf.Name, err2Δ3));
                break;
            } else if (optΔ3 == ""u8) {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s cannot have empty value for `format` tag option"u8, sf.Name));
                break;
            }
            tag = tag.slice(nΔ3);
            @out.format = optΔ3;
            wasFormat = true;
            break;
        }
        default: {
            @string normOpt = strings.ReplaceAll(strings.ToLower(opt), "_", "");
            switch (ᐧ) {
            case {} when normOpt == "case"u8 || normOpt == "embed"u8 || normOpt == "omitzero"u8 || normOpt == "omitempty"u8 || normOpt == "string"u8 || normOpt == "format"u8: {
                err = cmp.Or(err, fmt.Errorf("Go struct field %s has invalid appearance of `%s` tag option; specify `%s` instead"u8, sf.Name, opt, normOpt));
                break;
            }}

            break;
        }}

        switch (ᐧ) {
        case {} when @out.casing == (int8)((int8)caseIgnore | (int8)caseStrict): {
            err = cmp.Or(err, fmt.Errorf("Go struct field %s cannot have both `case:ignore` and `case:strict` tag options"u8, sf.Name));
            break;
        }
        case {} when seenOpts[opt]: {
            err = cmp.Or(err, fmt.Errorf("Go struct field %s has duplicate appearance of `%s` tag option"u8, sf.Name, rawOpt));
            break;
        }}

        seenOpts[opt] = true;
    }
    return (@out, false, err);
}

internal static (@string, nint, error) consumeTagOption(@string @in, bool allowQuoted) {
    var bᴷ = new stackAppend<byte>();

    nint i = strings.IndexByte(@in, (byte)',');
    if (i < 0) {
        i = len(@in);
    }

    var (r, _) = utf8.DecodeRuneInString(@in);
    switch (ᐧ) {
    case {} when r == '_' || unicode.IsLetter(r): {
        nint n = len(@in) - len(strings.TrimLeftFunc(@in, isLetterOrDigit));
        return (@in.slice(0, n), n, default);
    }
    case {} when r is '\'': {
        if (!allowQuoted) {
            return (@in.slice(0, i), i, fmt.Errorf("invalid character %q at start of option (expecting Unicode letter)"u8, r));
        }

        bool inEscape = default;
        var b = slice<byte>([(byte)'"']);
        nint n = len(@"'");
        while (len(@in) > n) {
            var (rΔ2, rn) = utf8.DecodeRuneInString(@in.slice(n));
            switch (ᐧ) {
            case {} when inEscape: {
                if (rΔ2 == '\'') {
                    b = b.slice(0, len(b) - 1);
                }
                inEscape = false;
                break;
            }
            case {} when rΔ2 is '\\': {
                inEscape = true;
                break;
            }
            case {} when rΔ2 is '"': {
                b = append(bᴷ, b, (byte)'\\');
                break;
            }
            case {} when rΔ2 is '\'': {
                b = append(b, (byte)'"');
                n += len(@"'");
                var (@out, err) = strconv.Unquote((@string)b);
                if (err != nil) {
                    return (@in.slice(0, i), i, fmt.Errorf("invalid single-quoted string: %s"u8, @in.slice(0, n)));
                }
                return (@out, n, default);
            }}

            b = append(b, @in.slice(n).slice(0, rn).ꓸꓸꓸ);
            n += rn;
        }
        if (n > 10) {
            n = 10;
        }
        return (@in.slice(0, i), i, fmt.Errorf("single-quoted string not terminated: %s..."u8, @in.slice(0, n)));
    }
    case {} when len(@in) is 0: {
        return (@in.slice(0, i), i, io.ErrUnexpectedEOF);
    }
    default: {
        if (!allowQuoted) {
            return (@in.slice(0, i), i, fmt.Errorf("invalid character %q at start of option (expecting Unicode letter)"u8, r));
        }
        return (@in.slice(0, i), i, fmt.Errorf("invalid character %q at start of option (expecting Unicode letter or single quote)"u8, r));
    }}

}

internal static bool isLetterOrDigit(rune r) {
    return r == '_' || unicode.IsLetter(r) || unicode.IsNumber(r);
}

internal static nint boolsCompare(bool x, bool y) {
    switch (ᐧ) {
    case {} when !x && y: {
        return -1;
    }
    default: {
        return 0;
    }
    case {} when x && !y: {
        return +1;
    }}

}

} // end json_package
