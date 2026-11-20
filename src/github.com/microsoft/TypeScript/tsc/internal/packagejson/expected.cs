namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using reflect = reflect_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static packagejson_package;

partial class packagejson_package {

partial struct Expected<T> {
    internal @string actualJSONType;
    public bool Null;
    public bool Valid;
    public T Value;
}

} partial class packagejsonᴛExpected {

public static error UnmarshalJSON<T>(this ж<Expected<T>> Ꮡe, slice<byte> data) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (((sstring)data) == "null"u8) {
        e = new Expected<T>(Null: true, actualJSONType: "null");
        return default;
    }
    if (json.Unmarshal(data, Ꮡe.of(Expected<T>.ᏑValue)) == nil) {
        e.Valid = true;
    }
    switch (data[0]) {
    case (byte)'"': {
        e.actualJSONType = "string";
        break;
    }
    case (byte)'t' or (byte)'f': {
        e.actualJSONType = "boolean";
        break;
    }
    case (byte)'[': {
        e.actualJSONType = "array";
        break;
    }
    case (byte)'{': {
        e.actualJSONType = "object";
        break;
    }
    default: {
        e.actualJSONType = "number";
        break;
    }}

    return default;
}

public static bool IsPresent<T>(this ref Expected<T> e) {
    return e.actualJSONType != ""u8;
}

public static (T value, bool ok) GetValue<T>(this ref Expected<T> e) {
    return (e.Value, e.Valid);
}

public static bool IsValid<T>(this ref Expected<T> e) {
    return e.Valid;
}

public static @string ExpectedJSONType<T>(this ref Expected<T> e) {
    switch (reflect.TypeFor<T>().Kind()) {
    case var ᴛ when ᴛ == reflect.String: {
        return "string";
    }
    case var ᴛ when ᴛ == reflect.Bool: {
        return "boolean";
    }
    case var ᴛ when ᴛ == reflect.Slice || ᴛ == reflect.Array: {
        return "array";
    }
    case var ᴛ when ᴛ == reflect.Map: {
        return "object";
    }
    case var ᴛ when ᴛ == reflect.Int || ᴛ == reflect.Int8 || ᴛ == reflect.Int16 || ᴛ == reflect.Int32 || ᴛ == reflect.Int64 || ᴛ == reflect.Uint || ᴛ == reflect.Uint8 || ᴛ == reflect.Uint16 || ᴛ == reflect.Uint32 || ᴛ == reflect.Uint64: {
        return "number";
    }
    default: {
        return "unknown";
    }}

}

public static @string ActualJSONType<T>(this ref Expected<T> e) {
    return e.actualJSONType;
}

} partial class packagejson_package {

public static Expected<T> ExpectedOf<T>(T value) {
    return new Expected<T>(Value: value, Valid: true, actualJSONType: (((ж<Expected<T>>)nil)).ExpectedJSONType());
}

} // end packagejson_package
