namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

partial struct nodeLinkStore<V> {
    internal core.PagedLinkStore<V> store;
}

} partial class checkerᴛnodeLinkStore {

internal static ж<V> Get<V>(this ref nodeLinkStore<V> s, ж<ast.Node> node) {
    return s.store.Get((uint64)ast.GetNodeId(node));
}

internal static bool Has<V>(this ref nodeLinkStore<V> s, ж<ast.Node> node) {
    return s.store.Has((uint64)ast.GetNodeId(node));
}

internal static ж<V> TryGet<V>(this ref nodeLinkStore<V> s, ж<ast.Node> node) {
    return s.store.TryGet((uint64)ast.GetNodeId(node));
}

} partial class checker_package {

partial struct symbolArenaLinkStore<V> {
    internal core.PagedLinkStore<ж<V>> store;
    internal core.Arena<V> arena;
}

} partial class checkerᴛsymbolArenaLinkStore {

internal static ж<V> Get<V>(this ref symbolArenaLinkStore<V> s, ж<ast.Symbol> symbol) {
    var link = s.store.Get((uint64)ast.GetSymbolId(symbol));
    if (link.ValueSlot == nil) {
        link.ValueSlot = s.arena.New();
    }
    return link.ValueSlot;
}

internal static bool Has<V>(this ref symbolArenaLinkStore<V> s, ж<ast.Symbol> symbol) {
    return s.TryGet(symbol) != nil;
}

internal static ж<V> TryGet<V>(this ref symbolArenaLinkStore<V> s, ж<ast.Symbol> symbol) {
    var link = s.store.TryGet((uint64)ast.GetSymbolId(symbol)); if (link != nil) {
        return link.ValueSlot;
    }
    return default;
}

} // end checkerᴛsymbolArenaLinkStore
