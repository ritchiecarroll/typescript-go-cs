namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using encoding = encoding_package;
using errors = errors_package;
using iter = iter_package;
using maps = maps_package;
using reflect = reflect_package;
using slices = slices_package;
using strconv = strconv_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using go.github.com.go_json_experiment.json;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using static collections_package;

partial class collections_package {

partial struct OrderedMap<K, V> {
    internal noCopy _;
    internal slice<K> keys;
    internal map<K, V> mp;
}

partial struct noCopy {
}

} partial class collectionsᴛnoCopy {

internal static void Lock(this ref noCopy _) { }
internal static void Unlock(this ref noCopy _) { }

} partial class collections_package {

public static ж<OrderedMap<K, V>> NewOrderedMapWithSizeHint<K, V>(nint hint) {
    ref var m = ref heap<OrderedMap<K, V>>(newMapWithSizeHint<K, V>(hint), out var Ꮡm);
    return Ꮡm;
}

internal static OrderedMap<K, V> newMapWithSizeHint<K, V>(nint hint) {
    return new OrderedMap<K, V>(
        keys: new slice<K>(0, hint),
        mp: new map<K, V>(hint)
    );
}

partial struct MapEntry<K, V> {
    public K Key;
    public V Value;
}

public static ж<OrderedMap<K, V>> NewOrderedMapFromList<K, V>(slice<MapEntry<K, V>> items) {
    var mp = NewOrderedMapWithSizeHint<K, V>(len(items));
    foreach (var (_, item) in range(items)) {
        mp.Set(item.Key, item.Value);
    }
    return mp;
}

} partial class collectionsᴛOrderedMap {

public static void Set<K, V>(this ref OrderedMap<K, V> m, K key, V value) {
    if (m.mp == nil) {
        m.mp = new map<K, V>();
    }

    var (_, ok) = m.mp[key, ꟷ]; if (!ok) {
        m.keys = append(m.keys, key);
    }
    m.mp[key] = value;
}

public static (V, bool) Get<K, V>(this ref OrderedMap<K, V> m, K key) {
    var (v, ok) = m.mp[key, ꟷ];
    return (v, ok);
}

public static V GetOrZero<K, V>(this ref OrderedMap<K, V> m, K key) {
    return m.mp[key];
}

public static (K, V, bool) EntryAt<K, V>(this ref OrderedMap<K, V> m, nint index) {
    if (index < 0 || index >= len(m.keys)) {
        K zero = GoZero<K>();
        V zeroV = GoZero<V>();
        return (zero, zeroV, false);
    }

    var key = m.keys[index];
    var value = m.mp[key];
    return (key, value, true);
}

public static bool Has<K, V>(this ref OrderedMap<K, V> m, K key) {
    var (_, ok) = m.mp[key, ꟷ];
    return ok;
}

public static (V, bool) Delete<K, V>(this ref OrderedMap<K, V> m, K key) {
    var (v, ok) = m.mp[key, ꟷ];
    if (!ok) {
        V zero = GoZero<V>();
        return (zero, false);
    }

    delete(m.mp, key);
    nint i = slices.Index(m.keys, key);
    if (i == 0) {
        K zero = GoZero<K>();
        m.keys[0] = zero;
        m.keys = m.keys[1..];
    } else {
        nint end = len(m.keys) - 1; if (i == end) {
            K zero = GoZero<K>();
            m.keys[end] = zero;
            m.keys = m.keys.slice(0, end);
        } else {
            m.keys = slices.Delete<slice<K>, K>(m.keys, i, i + 1);
        }
    }

    return (v, true);
}

public static iter.Seq<K> Keys<K, V>(this ж<OrderedMap<K, V>> Ꮡm) {
    return (Func<K, bool> yield) => {
        if (Ꮡm == nil) {
            return;
        }

        for (nint i = 0; i < len(Ꮡm.Value.keys); i++) {
            if (!yield(Ꮡm.Value.keys[i])) {
                break;
            }
        }
    };
}

public static iter.Seq<V> Values<K, V>(this ж<OrderedMap<K, V>> Ꮡm) {
    return (Func<V, bool> yield) => {
        if (Ꮡm == nil) {
            return;
        }

        for (nint i = 0; i < len(Ꮡm.Value.keys); i++) {
            if (!yield(Ꮡm.Value.mp[Ꮡm.Value.keys[i]])) {
                break;
            }
        }
    };
}

public static iter.Seq2<K, V> Entries<K, V>(this ж<OrderedMap<K, V>> Ꮡm) {
    return (Func<K, V, bool> yield) => {
        if (Ꮡm == nil) {
            return;
        }

        for (nint i = 0; i < len(Ꮡm.Value.keys); i++) {
            var key = Ꮡm.Value.keys[i];
            if (!yield(key, Ꮡm.Value.mp[key])) {
                break;
            }
        }
    };
}

public static void Clear<K, V>(this ref OrderedMap<K, V> m) {
    clear(m.keys);
    m.keys = m.keys[..0];
    clear(m.mp);
}

public static nint Size<K, V>(this ж<OrderedMap<K, V>> Ꮡm) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return 0;
    }

    return len(m.keys);
}

public static ж<OrderedMap<K, V>> Clone<K, V>(this ж<OrderedMap<K, V>> Ꮡm) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return default;
    }

    ref var m2 = ref heap<OrderedMap<K, V>>(m.clone(), out var Ꮡm2);
    return Ꮡm2;
}

internal static OrderedMap<K, V> clone<K, V>(this ref OrderedMap<K, V> m) {
    return new OrderedMap<K, V>(
        keys: slices.Clone<slice<K>, K>(m.keys),
        mp: maps.Clone<map<K, V>, K, V>(m.mp)
    );
}

} partial class collections_package {

internal static jsonꓸMarshalerTo _ᴛ1ʗ = new OrderedMapжMarshalerTo<@string, @string>(((ж<OrderedMap<@string, @string>>)nil));

} partial class collectionsᴛOrderedMap {

public static error MarshalJSONTo<K, V>(this ref OrderedMap<K, V> m, ж<jsontext.Encoder> enc) {
    {
        var err = enc.WriteToken(json.BeginObject); if (err != nil) {
            return err;
        }
    }

    foreach (var (_, k) in range(m.keys)) {
        var (keyString, err) = resolveKeyName(reflect.ValueOf((k).OrTypedNilOf()));
        if (err != nil) {
            return err;
        }

        var errΔ1 = json.MarshalEncode(enc, keyString); if (errΔ1 != nil) {
            return errΔ1;
        }

        var errΔ2 = json.MarshalEncode(enc, (m.mp[k]).OrTypedNilOf()); if (errΔ2 != nil) {
            return errΔ2;
        }
    }

    return enc.WriteToken(json.EndObject);
}

} partial class collections_package {

internal static (@string, error) resolveKeyName(reflect.Value k) {
    if (k.Kind() == reflect.String) {
        return (k.String(), default);
    }
    var (tm, ok) = reflect.TypeAssert<encoding.TextMarshaler>(k); if (ok) {
        if (k.Kind() == reflect.Pointer && k.IsNil()) {
            return ("", default);
        }
        var (buf, err) = tm.MarshalText();
        return ((@string)buf, err);
    }
    switch (k.Kind()) {
    case var ᴛ when ᴛ == reflect.Int || ᴛ == reflect.Int8 || ᴛ == reflect.Int16 || ᴛ == reflect.Int32 || ᴛ == reflect.Int64: {
        return (strconv.FormatInt(k.Int(), 10), default);
    }
    case var ᴛ when ᴛ == reflect.Uint || ᴛ == reflect.Uint8 || ᴛ == reflect.Uint16 || ᴛ == reflect.Uint32 || ᴛ == reflect.Uint64 || ᴛ == reflect.Uintptr: {
        return (strconv.FormatUint(k.Uint(), 10), default);
    }}

    throw panic("unexpected map key type");
}

internal static jsonꓸUnmarshalerFrom _ᴛ2ʗ = new OrderedMapжUnmarshalerFrom<@string, @string>(((ж<OrderedMap<@string, @string>>)nil));

} partial class collectionsᴛOrderedMap {

public static error UnmarshalJSONFrom<K, V>(this ref OrderedMap<K, V> m, ж<jsontext.Decoder> dec) {
    var (token, err) = dec.ReadToken();
    if (err != nil) {
        return err;
    }
    if (token.Kind() == (rune)'n') {
        return default;
    }
    if (token.Kind() != (rune)'{') {
        return errors.New("cannot unmarshal non-object JSON value into Map");
    }
    while (dec.PeekKind() != (rune)'}') {
        ref var key = ref heap<K>(out var Ꮡkey);
        ref var value = ref heap<V>(out var Ꮡvalue);
        var errΔ1 = json.UnmarshalDecode(dec, Ꮡkey); if (errΔ1 != nil) {
            return errΔ1;
        }
        var errΔ2 = json.UnmarshalDecode(dec, Ꮡvalue); if (errΔ2 != nil) {
            return errΔ2;
        }
        m.Set(key, value);
    }
    var (_, errΔ3) = dec.ReadToken(); if (errΔ3 != nil) {
        return errΔ3;
    }
    return default;
}

} partial class collections_package {

public static void DiffOrderedMaps<K, V>(ж<OrderedMap<K, V>> m1, ж<OrderedMap<K, V>> m2, Action<K, V> onAdded, Action<K, V> onRemoved, Action<K, V, V> onModified) {
    DiffOrderedMapsFunc(m1, m2, (V a, V b) => AreEqual(a, b), onAdded, onRemoved, onModified);
}

public static void DiffOrderedMapsFunc<K, V>(ж<OrderedMap<K, V>> Ꮡm1, ж<OrderedMap<K, V>> Ꮡm2, Func<V, V, bool> equalValues, Action<K, V> onAdded, Action<K, V> onRemoved, Action<K, V, V> onModified) {
    ref var m1 = ref Ꮡm1.DerefOrNull();
    ref var m2 = ref Ꮡm2.DerefOrNull();

    foreach (var (k, v2) in range<K, V>(Ꮡm2.Entries().Invoke)) {
        var (_, ok) = m1.Get(k); if (!ok) {
            onAdded(k, v2);
        }
    }
    foreach (var (k, v1) in range<K, V>(Ꮡm1.Entries().Invoke)) {
        var (v2, ok) = m2.Get(k); if (ok) {
            if (!equalValues(v1, v2)) {
                onModified(k, v1, v2);
            }
        } else {
            onRemoved(k, v1);
        }
    }
}

} // end collections_package
