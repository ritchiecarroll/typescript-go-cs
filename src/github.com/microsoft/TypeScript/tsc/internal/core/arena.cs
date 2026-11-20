namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using static core_package;

partial class core_package {

partial struct Arena<T> {
    internal slice<T> data;
}

} partial class coreᴛArena {

public static ж<T> New<T>(this ref Arena<T> a) {
    if (len(a.data) == cap(a.data)) {
        nint nextSize = nextArenaSize(len(a.data));
        a.data = slices.Grow<slice<T>, T>(default, nextSize);
    }
    nint index = len(a.data);
    a.data = a.data.slice(0, index + 1);
    return Ꮡ(a.data, index);
}

public static slice<T> NewSlice<T>(this ref Arena<T> a, nint size) {
    if (size == 0) {
        return default;
    }
    if (len(a.data) + size > cap(a.data)) {
        nint nextSize = nextArenaSize(len(a.data));
        if (size > nextSize) {
            return new slice<T>(size);
        }
        a.data = slices.Grow<slice<T>, T>(default, nextSize);
    }
    nint newLen = len(a.data) + size;
    var Δslice = a.data.slice(len(a.data), newLen, newLen);
    a.data = a.data.slice(0, newLen);
    return Δslice;
}

public static slice<T> NewSlice1<T>(this ref Arena<T> a, T t) {
    var Δslice = a.NewSlice(1);
    Δslice[0] = t;
    return Δslice;
}

public static slice<T> Clone<T>(this ref Arena<T> a, slice<T> t) {
    if (len(t) == 0) {
        return default;
    }
    var Δslice = a.NewSlice(len(t));
    copy(Δslice, t);
    return Δslice;
}

} partial class core_package {

internal static nint nextArenaSize(nint size) {
    size = max(size, 1);
    size = min(size * 2, 256);
    return size;
}

} // end core_package
