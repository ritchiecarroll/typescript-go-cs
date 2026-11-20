namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using static collections_package;

partial class collections_package {

partial struct CopyOnWriteMap<K, V> {
    internal map<K, V> m;
    internal bool owned;
}

} partial class collectionsᴛCopyOnWriteMap {

public static (V, bool) Get<K, V>(this ref CopyOnWriteMap<K, V> c, K k) {
    var (v, ok) = c.m[k, ꟷ];
    return (v, ok);
}

public static bool Has<K, V>(this ref CopyOnWriteMap<K, V> c, K k) {
    var (_, ok) = c.m[k, ꟷ];
    return ok;
}

public static void Set<K, V>(this ref CopyOnWriteMap<K, V> c, K k, V v) {
    c.ensureOwned();
    c.m[k] = v;
}

internal static void ensureOwned<K, V>(this ref CopyOnWriteMap<K, V> c) {
    if (c.owned) {
        return;
    }
    if (c.m == nil) {
        c.m = new map<K, V>();
    } else {
        c.m = maps.Clone<map<K, V>, K, V>(c.m);
    }
    c.owned = true;
}

public static Action EnterScope<K, V>(this ж<CopyOnWriteMap<K, V>> Ꮡc) {
    ref var c = ref Ꮡc.DerefOrNull();

    ref var saved = ref heap<CopyOnWriteMap<K, V>>(c, out var Ꮡsaved);
    c.owned = false;
    var savedʗ1 = saved;
    return () => {
        Ꮡc.Value = savedʗ1;
    };
}

} partial class collections_package {

partial struct CopyOnWriteSet<K> {
    internal CopyOnWriteMap<K, EmptyStruct> m;
}

} partial class collectionsᴛCopyOnWriteSet {

public static bool Has<K>(this ref CopyOnWriteSet<K> c, K k) {
    var (_, ok) = c.m.Get(k);
    return ok;
}

public static void Add<K>(this ref CopyOnWriteSet<K> c, K k) {
    c.m.Set(k, new EmptyStruct());
}

public static Action EnterScope<K>(this ж<CopyOnWriteSet<K>> Ꮡc) {
    return Ꮡc.Ꮡm.EnterScope();
}

} // end collectionsᴛCopyOnWriteSet
