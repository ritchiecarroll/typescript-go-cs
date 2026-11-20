namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using go.github.com.go_json_experiment.json;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using static packagejson_package;

partial class packagejson_package {

partial struct JSONValueType /*num:int8*/;

public static JSONValueType JSONValueTypeNotPresent => /* iota */ 0;
public static JSONValueType JSONValueTypeNull => 1;
public static JSONValueType JSONValueTypeString => 2;
public static JSONValueType JSONValueTypeNumber => 3;
public static JSONValueType JSONValueTypeBoolean => 4;
public static JSONValueType JSONValueTypeArray => 5;
public static JSONValueType JSONValueTypeObject => 6;

} partial class packagejsonᴛJSONValueType {

public static @string String(this JSONValueType t) {
    switch (t) {
    case var ᴛ when ᴛ == JSONValueTypeNull: {
        return "null";
    }
    case var ᴛ when ᴛ == JSONValueTypeString: {
        return "string";
    }
    case var ᴛ when ᴛ == JSONValueTypeNumber: {
        return "number";
    }
    case var ᴛ when ᴛ == JSONValueTypeBoolean: {
        return "boolean";
    }
    case var ᴛ when ᴛ == JSONValueTypeArray: {
        return "array";
    }
    case var ᴛ when ᴛ == JSONValueTypeObject: {
        return "object";
    }
    default: {
        return fmt.Sprintf("unknown(%d)"u8, t);
    }}

}

} partial class packagejson_package {

partial struct JSONValue {
    public JSONValueType Type;
    public any Value;
}

} partial class packagejsonᴛJSONValue {

public static bool IsPresent(this ref JSONValue v) {
    return v.Type != JSONValueTypeNotPresent;
}

public static bool IsFalsy(this ref JSONValue v) {
    switch (v.Type) {
    case var ᴛ when ᴛ == JSONValueTypeNotPresent || ᴛ == JSONValueTypeNull: {
        return true;
    }
    case var ᴛ when ᴛ == JSONValueTypeString: {
        return AreEqual(v.Value, (@string)(""));
    }
    case var ᴛ when ᴛ == JSONValueTypeNumber: {
        return AreEqual(v.Value, (nint)0);
    }
    case var ᴛ when ᴛ == JSONValueTypeBoolean: {
        return !v.Value._<bool>();
    }
    default: {
        return false;
    }}

}

public static ж<collections.OrderedMap<@string, JSONValue>> AsObject(this JSONValue v) {
    if (v.Type != JSONValueTypeObject) {
        throw panic(fmt.Sprintf("expected object, got %v"u8, v.Type));
    }
    return v.Value._<ж<collections.OrderedMap<@string, JSONValue>>>();
}

public static slice<JSONValue> AsArray(this JSONValue v) {
    if (v.Type != JSONValueTypeArray) {
        throw panic(fmt.Sprintf("expected array, got %v"u8, v.Type));
    }
    return v.Value._<slice<JSONValue>>();
}

public static @string AsString(this JSONValue v) {
    if (v.Type != JSONValueTypeString) {
        throw panic(fmt.Sprintf("expected string, got %v"u8, v.Type));
    }
    return v.Value._<@string>();
}

} partial class packagejson_package {

internal static jsonꓸUnmarshalerFrom _ᴛ2ʗ = new JSONValueжUnmarshalerFrom(((ж<JSONValue>)nil));

} partial class packagejsonᴛJSONValue {

public static error UnmarshalJSONFrom(this ж<JSONValue> Ꮡv, ж<jsontext.Decoder> dec) {
    return unmarshalJSONValueV2<JSONValue>(Ꮡv, dec);
}

} partial class packagejson_package {

internal static error unmarshalJSONValue<T>(ж<JSONValue> Ꮡv, slice<byte> data) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (((sstring)data) == "null"u8) {
        v = new JSONValue(Type: JSONValueTypeNull);
    } else if (data[0] == '"') {
        v.Type = JSONValueTypeString;
        return json.Unmarshal(data, Ꮡv.of(JSONValue.ᏑValue));
    } else if (data[0] == '[') {
        ref var elements = ref heap<slice<T>>(out var Ꮡelements);
        var err = json.Unmarshal(data, Ꮡelements); if (err != nil) {
            return err;
        }
        v.Type = JSONValueTypeArray;
        v.Value = elements;
    } else if (data[0] == '{') {
        ref var @object = ref heap(new collections.OrderedMap<@string, T>(), out var Ꮡobject);
        var err = json.Unmarshal(data, Ꮡobject); if (err != nil) {
            return err;
        }
        v.Type = JSONValueTypeObject;
        v.Value = Ꮡobject;
    } else if (((sstring)data) == "true"u8) {
        v.Type = JSONValueTypeBoolean;
        v.Value = true;
    } else if (((sstring)data) == "false"u8) {
        v.Type = JSONValueTypeBoolean;
        v.Value = false;
    } else {
        v.Type = JSONValueTypeNumber;
        return json.Unmarshal(data, Ꮡv.of(JSONValue.ᏑValue));
    }
    return default;
}

internal static error unmarshalJSONValueV2<T>(ж<JSONValue> Ꮡv, ж<jsontext.Decoder> dec) {
    ref var v = ref Ꮡv.DerefOrNull();

    switch (dec.PeekKind()) {
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'n'): {
        {
            var (_, err) = dec.ReadToken(); if (err != nil) {
                return err;
            }
        }
        v.Value = default;
        v.Type = JSONValueTypeNull;
        return default;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'"'): {
        v.Type = JSONValueTypeString;
        {
            var err = json.UnmarshalDecode(dec, Ꮡv.of(JSONValue.ᏑValue)); if (err != nil) {
                return err;
            }
        }
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'['): {
        {
            var (_, err) = dec.ReadToken(); if (err != nil) {
                return err;
            }
        }
        slice<T> elements = default;
        while (dec.PeekKind() != json.EndArray.Kind()) {
            ref var element = ref heap<T>(out var Ꮡelement);
            var err = json.UnmarshalDecode(dec, Ꮡelement); if (err != nil) {
                return err;
            }
            elements = append(elements, element);
        }
        {
            var (_, err) = dec.ReadToken(); if (err != nil) {
                return err;
            }
        }
        v.Type = JSONValueTypeArray;
        v.Value = elements;
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'{'): {
        ref var @object = ref heap(new collections.OrderedMap<@string, T>(), out var Ꮡobject);
        {
            var err = json.UnmarshalDecode(dec, Ꮡobject); if (err != nil) {
                return err;
            }
        }
        v.Type = JSONValueTypeObject;
        v.Value = Ꮡobject;
        break;
    }
    case var ᴛ when ᴛ == (jsontext.Kind)((rune)'t') || ᴛ == (jsontext.Kind)((rune)'f'): {
        v.Type = JSONValueTypeBoolean;
        {
            var err = json.UnmarshalDecode(dec, Ꮡv.of(JSONValue.ᏑValue)); if (err != nil) {
                return err;
            }
        }
        break;
    }
    default: {
        v.Type = JSONValueTypeNumber;
        {
            var err = json.UnmarshalDecode(dec, Ꮡv.of(JSONValue.ᏑValue)); if (err != nil) {
                return err;
            }
        }
        break;
    }}

    return default;
}

} // end packagejson_package
