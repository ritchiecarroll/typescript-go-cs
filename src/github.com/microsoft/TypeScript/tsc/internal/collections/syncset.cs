namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using iter = iter_package;
using static collections_package;

partial class collections_package {

partial struct SyncSet<T> {
    internal SyncMap<T, EmptyStruct> m;
}

} partial class collectionsᴛSyncSet {

public static bool Has<T>(this ж<SyncSet<T>> Ꮡs, T key) {
    var (_, ok) = Ꮡs.Ꮡm.Load(key);
    return ok;
}

public static void Add<T>(this ж<SyncSet<T>> Ꮡs, T key) {
    Ꮡs.AddIfAbsent(key);
}

public static bool AddIfAbsent<T>(this ж<SyncSet<T>> Ꮡs, T key) {
    var (_, loaded) = Ꮡs.Ꮡm.LoadOrStore(key, new EmptyStruct());
    return !loaded;
}

public static void Delete<T>(this ж<SyncSet<T>> Ꮡs, T key) {
    Ꮡs.Ꮡm.Delete(key);
}

public static void Range<T>(this ж<SyncSet<T>> Ꮡs, Func<T, bool> fn) {
    Ꮡs.Ꮡm.Range((T key, EmptyStruct value) => fn(key));
}

public static nint Size<T>(this ж<SyncSet<T>> Ꮡs) {
    nint count = 0;
    Ꮡs.Ꮡm.Range((T _Δp0, EmptyStruct _Δp1) => {
        count++;
        return true;
    });
    return count;
}

public static bool IsEmpty<T>(this ж<SyncSet<T>> Ꮡs) {
    var empty = true;
    Ꮡs.Ꮡm.Range((T _Δp0, EmptyStruct _Δp1) => {
        empty = false;
        return false;
    });

    return empty;
}

public static slice<T> ToSlice<T>(this ж<SyncSet<T>> Ꮡs) {
    ref var arr = ref heap<slice<T>>(out var Ꮡarr);
    arr = new slice<T>(0, Ꮡs.Ꮡm.Size());
    Ꮡs.Ꮡm.Range((T key, EmptyStruct value) => {
        Ꮡarr.ValueSlot = append(Ꮡarr.ValueSlot, key);
        return true;
    });
    return arr;
}

public static iter.Seq<T> Keys<T>(this ж<SyncSet<T>> Ꮡs) {
    return (Func<T, bool> yield) => {
        Ꮡs.Ꮡm.Range((T key, EmptyStruct value) => {
            if (!yield(key)) {
                return false;
            }
            return true;
        });
    };
}

} // end collectionsᴛSyncSet
