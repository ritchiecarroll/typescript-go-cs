namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using iter = iter_package;
using maps = maps_package;
using slices = slices_package;
using static collections_package;

partial class collections_package {

partial struct MultiMap<K, V> {
    public map<K, slice<V>> M;
}

public static ж<MultiMap<K, V>> NewMultiMapWithSizeHint<K, V>(nint hint) {
    return Ꮡ(new MultiMap<K, V>(
        M: new map<K, slice<V>>(hint)
    ));
}

public static ж<MultiMap<K, V>> GroupBy<K, V>(slice<V> items, Func<V, K> groupId) {
    var m = Ꮡ(new MultiMap<K, V>());
    foreach (var (_, item) in range(items)) {
        m.Add(groupId(item), item);
    }
    return m;
}

} partial class collectionsᴛMultiMap {

public static bool Has<K, V>(this ref MultiMap<K, V> s, K key) {
    var (_, ok) = s.M[key, ꟷ];
    return ok;
}

public static slice<V> Get<K, V>(this ref MultiMap<K, V> s, K key) {
    return s.M[key];
}

public static void Add<K, V>(this ref MultiMap<K, V> s, K key, V value) {
    if (s.M == nil) {
        s.M = new map<K, slice<V>>();
    }
    s.M[key] = append(s.M[key], value);
}

public static void Remove<K, V>(this ref MultiMap<K, V> s, K key, V value) {
    var (values, ok) = s.M[key, ꟷ]; if (ok) {
        nint i = slices.Index(values, value);
        if (i >= 0) {
            if (len(values) == 1) {
                delete(s.M, key);
            } else {
                values = appendꓸꓸꓸ(values.slice(0, i), values.slice(i + 1));
                s.M[key] = values;
            }
        }
    }
}

public static void RemoveAll<K, V>(this ref MultiMap<K, V> s, K key) {
    delete(s.M, key);
}

public static nint Len<K, V>(this ref MultiMap<K, V> s) {
    return len(s.M);
}

public static iter.Seq<K> Keys<K, V>(this ref MultiMap<K, V> s) {
    return maps.Keys<map<K, slice<V>>, K, slice<V>>(s.M);
}

public static iter.Seq<slice<V>> Values<K, V>(this ref MultiMap<K, V> s) {
    return maps.Values<map<K, slice<V>>, K, slice<V>>(s.M);
}

public static void Clear<K, V>(this ref MultiMap<K, V> s) {
    clear(s.M);
}

} // end collectionsᴛMultiMap
