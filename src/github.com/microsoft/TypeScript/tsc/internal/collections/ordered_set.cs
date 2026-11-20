namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using iter = iter_package;
using static collections_package;

partial class collections_package {

partial struct OrderedSet<T> {
    internal OrderedMap<T, EmptyStruct> m;
}

public static ж<OrderedSet<T>> NewOrderedSetWithSizeHint<T>(nint hint) {
    return Ꮡ(new OrderedSet<T>(
        m: newMapWithSizeHint<T, EmptyStruct>(hint)
    ));
}

} partial class collectionsᴛOrderedSet {

public static void Add<T>(this ref OrderedSet<T> s, T value) {
    s.m.Set(value, new EmptyStruct());
}

public static bool Has<T>(this ref OrderedSet<T> s, T value) {
    return s.m.Has(value);
}

public static bool Delete<T>(this ref OrderedSet<T> s, T value) {
    var (_, ok) = s.m.Delete(value);
    return ok;
}

public static iter.Seq<T> Values<T>(this ж<OrderedSet<T>> Ꮡs) {
    return Ꮡs.Ꮡm.Keys();
}

public static void Clear<T>(this ref OrderedSet<T> s) {
    s.m.Clear();
}

public static nint Size<T>(this ж<OrderedSet<T>> Ꮡs) {
    return Ꮡs.Ꮡm.Size();
}

public static ж<OrderedSet<T>> Clone<T>(this ref OrderedSet<T> s) {
    return Ꮡ(new OrderedSet<T>(
        m: s.m.clone()
    ));
}

} // end collectionsᴛOrderedSet
