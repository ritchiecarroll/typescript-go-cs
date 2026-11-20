namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using static collections_package;

partial class collections_package {

partial struct Set<T> {
    public map<T, EmptyStruct> M;
}

public static ж<Set<T>> NewSetWithSizeHint<T>(nint hint) {
    return Ꮡ(new Set<T>(
        M: new map<T, EmptyStruct>(hint)
    ));
}

} partial class collectionsᴛSet {

public static bool Has<T>(this ж<Set<T>> Ꮡs, T key) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs == nil) {
        return false;
    }
    var (_, ok) = s.M[key, ꟷ];
    return ok;
}

public static void Add<T>(this ref Set<T> s, T key) {
    if (s.M == nil) {
        s.M = new map<T, EmptyStruct>();
    }
    s.M[key] = new EmptyStruct();
}

public static void Delete<T>(this ref Set<T> s, T key) {
    delete(s.M, key);
}

public static nint Len<T>(this ж<Set<T>> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs == nil) {
        return 0;
    }
    return len(s.M);
}

public static map<T, EmptyStruct> Keys<T>(this ж<Set<T>> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs == nil) {
        return default;
    }
    return s.M;
}

public static void Clear<T>(this ж<Set<T>> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs == nil) {
        return;
    }
    clear(s.M);
}

public static bool AddIfAbsent<T>(this ж<Set<T>> Ꮡs, T key) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs.Has(key)) {
        return false;
    }
    s.Add(key);
    return true;
}

public static ж<Set<T>> Clone<T>(this ж<Set<T>> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs == nil) {
        return default;
    }
    var clone = Ꮡ(new Set<T>(M: maps.Clone<map<T, EmptyStruct>, T, EmptyStruct>(s.M)));
    return clone;
}

public static void Union<T>(this ж<Set<T>> Ꮡs, ж<Set<T>> Ꮡother) {
    ref var s = ref Ꮡs.DerefOrNull();
    ref var other = ref Ꮡother.DerefOrNull();

    if (Ꮡs.Len() == 0 && Ꮡother.Len() == 0) {
        return;
    }
    if (Ꮡs == nil) {
        throw panic("cannot modify nil Set");
    }
    if (s.M == nil) {
        s.M = maps.Clone<map<T, EmptyStruct>, T, EmptyStruct>(other.M);
        return;
    }
    maps.Copy<map<T, EmptyStruct>, map<T, EmptyStruct>, T, EmptyStruct>(s.M, other.M);
}

public static ж<Set<T>> UnionedWith<T>(this ж<Set<T>> Ꮡs, ж<Set<T>> Ꮡother) {
    ref var other = ref Ꮡother.DerefOrNull();

    if (Ꮡs == nil && Ꮡother == nil) {
        return default;
    }
    var result = Ꮡs.Clone();
    if (Ꮡother != nil) {
        if (result == nil) {
            result = Ꮡ(new Set<T>());
        }
        if (result.M == nil) {
            result.M = new map<T, EmptyStruct>(len(other.M));
        }
        maps.Copy<map<T, EmptyStruct>, map<T, EmptyStruct>, T, EmptyStruct>(result.M, other.M);
    }
    return result;
}

public static bool ΔEquals<T>(this ж<Set<T>> Ꮡs, ж<Set<T>> Ꮡother) {
    ref var s = ref Ꮡs.DerefOrNull();
    ref var other = ref Ꮡother.DerefOrNull();

    if (Ꮡs == Ꮡother) {
        return true;
    }
    if (Ꮡs == nil || Ꮡother == nil) {
        return false;
    }
    return maps.Equal<map<T, EmptyStruct>, map<T, EmptyStruct>, T, EmptyStruct>(s.M, other.M);
}

public static bool IsSubsetOf<T>(this ж<Set<T>> Ꮡs, ж<Set<T>> other) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs == nil) {
        return true;
    }
    foreach (var (key, _) in s.M) {
        if (!other.Has(key)) {
            return false;
        }
    }
    return true;
}

public static bool Intersects<T>(this ж<Set<T>> Ꮡs, ж<Set<T>> other) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (Ꮡs == nil || other == nil) {
        return false;
    }
    foreach (var (key, _) in s.M) {
        if (other.Has(key)) {
            return true;
        }
    }
    return false;
}

} partial class collections_package {

public static ж<Set<T>> NewSetFromItems<T>(params Span<T> itemsʗp) {
    var items = itemsʗp.sslice();

    var s = Ꮡ(new Set<T>());
    foreach (var (_, item) in range(items)) {
        s.Add(item);
    }
    return s;
}

} // end collections_package
