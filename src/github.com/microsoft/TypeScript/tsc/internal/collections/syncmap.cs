namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using iter = iter_package;
using sync = sync_package;
using static collections_package;

partial class collections_package {

partial struct SyncMap<K, V> {
    internal array<K> _ = new(0);
    internal array<V> __ = new(0);
    internal sync.Map m;
}

} partial class collectionsᴛSyncMap {

public static (V value, bool ok) Load<K, V>(this ж<SyncMap<K, V>> Ꮡs, K key) {
    V value = GoZero<V>();
    bool ok = default;

    (var val, ok) = Ꮡs.Ꮡm.Load((key).OrTypedNilOf());
    if (!ok || val == nil) {
        return (value, ok);
    }
    return (val._<V>(), true);
}

public static void Store<K, V>(this ж<SyncMap<K, V>> Ꮡs, K key, V value) {
    Ꮡs.Ꮡm.Store((key).OrTypedNilOf(), (value).OrTypedNilOf());
}

public static (V actual, bool loaded) LoadOrStore<K, V>(this ж<SyncMap<K, V>> Ꮡs, K key, V value) {
    V actual = GoZero<V>();
    bool loaded = default;

    (var actualAny, loaded) = Ꮡs.Ꮡm.LoadOrStore((key).OrTypedNilOf(), (value).OrTypedNilOf());
    if (actualAny == nil) {
        return (actual, loaded);
    }

    return (actualAny._<V>(), loaded);
}

public static void Delete<K, V>(this ж<SyncMap<K, V>> Ꮡs, K key) {
    Ꮡs.Ꮡm.Delete((key).OrTypedNilOf());
}

public static void Clear<K, V>(this ж<SyncMap<K, V>> Ꮡs) {
    Ꮡs.Ꮡm.Clear();
}

public static void Range<K, V>(this ж<SyncMap<K, V>> Ꮡs, Func<K, V, bool> f) {
    Ꮡs.Ꮡm.Range((any key, any value) => {
        K k = GoZero<K>();
        if (key != nil) {
            k = key._<K>();
        }

        V v = GoZero<V>();
        if (value != nil) {
            v = value._<V>();
        }

        return f(k, v);
    });
}

public static nint Size<K, V>(this ж<SyncMap<K, V>> Ꮡs) {
    nint count = 0;
    Ꮡs.Ꮡm.Range((any _Δp0, any _Δp1) => {
        count++;
        return true;
    });
    return count;
}

public static map<K, V> ToMap<K, V>(this ж<SyncMap<K, V>> Ꮡs) {
    var m = new map<K, V>(Ꮡs.Size());
    var mʗ1 = m;
    Ꮡs.Ꮡm.Range((any key, any value) => {
        mʗ1[key._<K>()] = value._<V>();
        return true;
    });
    return m;
}

public static iter.Seq<K> Keys<K, V>(this ж<SyncMap<K, V>> Ꮡs) {
    return (Func<K, bool> yield) => {
        Ꮡs.Ꮡm.Range((any key, any value) => {
            if (!yield(key._<K>())) {
                return false;
            }
            return true;
        });
    };
}

public static ж<SyncMap<K, V>> Clone<K, V>(this ж<SyncMap<K, V>> Ꮡs) {
    var clone = Ꮡ(new SyncMap<K, V>());
    var cloneʗ1 = clone;
    Ꮡs.Ꮡm.Range((any key, any value) => {
        cloneʗ1.Ꮡm.Store(key, value);
        return true;
    });
    return clone;
}

} // end collectionsᴛSyncMap
