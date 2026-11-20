namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using static core_package;

partial class core_package {

partial struct LinkStore<K, V> {
    internal map<K, ж<V>> entries;
    internal Arena<V> arena;
}

} partial class coreᴛLinkStore {

public static ж<V> Get<K, V>(this ref LinkStore<K, V> s, K key) {
    var value = s.entries[key];
    if (value != nil) {
        return value;
    }
    if (s.entries == nil) {
        s.entries = new map<K, ж<V>>();
    }
    value = s.arena.New();
    s.entries[key] = value;
    return value;
}

public static bool Has<K, V>(this ref LinkStore<K, V> s, K key) {
    var (_, ok) = s.entries[key, ꟷ];
    return ok;
}

public static ж<V> TryGet<K, V>(this ref LinkStore<K, V> s, K key) {
    return s.entries[key];
}

} partial class core_package {

internal static UntypedInt pageShift => 8;
internal static UntypedInt pageSize => /* 1 << pageShift */ 256;
internal static UntypedInt pageMask => /* pageSize - 1 */ 255;
internal static UntypedInt maxPageCount => 65536;

partial struct PagedLinkStore<V> {
    internal map<uint64, ж<array<V>>> pageMap;
    internal /*[256]*/ slice<ж<array<V>>> pageList;
}

} partial class coreᴛPagedLinkStore {

public static ж<V> Get<V>(this ref PagedLinkStore<V> s, uint64 key) {
    ж<array<V>> page = default;
    var pageIndex = key >> (int)(pageShift);
    if (pageIndex < maxPageCount) {
        if ((nint)pageIndex >= len(s.pageList)) {
            s.pageList = slices.Grow<slice<ж<array<V>>>, ж<array<V>>>(s.pageList, (nint)pageIndex - len(s.pageList) + 1).slice(0, (nint)(pageIndex + 1));
        }
        page = s.pageList[pageIndex];
        if (page == nil) {
            page = Ꮡ(new array<V>(256));
            s.pageList[pageIndex] = page;
        }
    } else {
        page = s.pageMap[pageIndex];
        if (page == nil) {
            page = Ꮡ(new array<V>(256));
            if (s.pageMap == nil) {
                s.pageMap = new map<uint64, ж<array<V>>>();
            }
            s.pageMap[pageIndex] = page;
        }
    }
    return page.at<V>((ulong)((key & (uint64)pageMask)));
}

public static bool Has<V>(this ref PagedLinkStore<V> s, uint64 key) {
    return s.TryGet(key) != nil;
}

public static ж<V> TryGet<V>(this ref PagedLinkStore<V> s, uint64 key) {
    ж<array<V>> page = default;
    var pageIndex = key >> (int)(pageShift);
    if (pageIndex < maxPageCount) {
        if ((nint)pageIndex < len(s.pageList)) {
            page = s.pageList[pageIndex];
        }
    } else {
        page = s.pageMap[pageIndex];
    }
    if (page != nil) {
        return page.at<V>((ulong)((key & (uint64)pageMask)));
    }
    return default;
}

} // end coreᴛPagedLinkStore
