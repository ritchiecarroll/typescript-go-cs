namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using sync = sync_package;

partial class core_package {

[GoLazy] partial class lazyᴛUnprefixedNodeCoreModules { public static readonly ж<map<@string, bool>> ᏑUnprefixedNodeCoreModules = new StaticBox<map<@string, bool>>(new map<@string, bool>{
    ["assert"] = true,
    ["assert/strict"] = true,
    ["async_hooks"] = true,
    ["buffer"] = true,
    ["child_process"] = true,
    ["cluster"] = true,
    ["console"] = true,
    ["constants"] = true,
    ["crypto"] = true,
    ["dgram"] = true,
    ["diagnostics_channel"] = true,
    ["dns"] = true,
    ["dns/promises"] = true,
    ["domain"] = true,
    ["events"] = true,
    ["fs"] = true,
    ["fs/promises"] = true,
    ["http"] = true,
    ["http2"] = true,
    ["https"] = true,
    ["inspector"] = true,
    ["inspector/promises"] = true,
    ["module"] = true,
    ["net"] = true,
    ["os"] = true,
    ["path"] = true,
    ["path/posix"] = true,
    ["path/win32"] = true,
    ["perf_hooks"] = true,
    ["process"] = true,
    ["punycode"] = true,
    ["querystring"] = true,
    ["readline"] = true,
    ["readline/promises"] = true,
    ["repl"] = true,
    ["stream"] = true,
    ["stream/consumers"] = true,
    ["stream/promises"] = true,
    ["stream/web"] = true,
    ["string_decoder"] = true,
    ["sys"] = true,
    ["timers"] = true,
    ["timers/promises"] = true,
    ["tls"] = true,
    ["trace_events"] = true,
    ["tty"] = true,
    ["url"] = true,
    ["util"] = true,
    ["util/types"] = true,
    ["v8"] = true,
    ["vm"] = true,
    ["wasi"] = true,
    ["worker_threads"] = true,
    ["zlib"] = true
}); }
public static ref map<@string, bool> UnprefixedNodeCoreModules => ref ((StaticBox<map<@string, bool>>)ᏑUnprefixedNodeCoreModules).ValueSlot;

public static readonly ж<map<@string, bool>> ᏑExclusivelyPrefixedNodeCoreModules = new StaticBox<map<@string, bool>>(new map<@string, bool>{
    ["node:quic"] = true,
    ["node:sea"] = true,
    ["node:sqlite"] = true,
    ["node:test"] = true,
    ["node:test/reporters"] = true
});
public static ref map<@string, bool> ExclusivelyPrefixedNodeCoreModules => ref ((StaticBox<map<@string, bool>>)ᏑExclusivelyPrefixedNodeCoreModules).ValueSlot;

public static readonly ж<Func<map<@string, bool>>> ᏑNodeCoreModules = new StaticBox<Func<map<@string, bool>>>(sync.OnceValue(map<@string, bool> () => {
    var nodeCoreModules = new map<@string, bool>(len(UnprefixedNodeCoreModules) * 2 + len(ExclusivelyPrefixedNodeCoreModules));
    foreach (var (unprefixed, _) in UnprefixedNodeCoreModules) {
        nodeCoreModules[unprefixed] = true;
        nodeCoreModules["node:"u8 + unprefixed] = true;
    }
    maps.Copy<map<@string, bool>, map<@string, bool>, @string, bool>(nodeCoreModules, ExclusivelyPrefixedNodeCoreModules);
    return nodeCoreModules;
}));
public static ref Func<map<@string, bool>> NodeCoreModules => ref ((StaticBox<Func<map<@string, bool>>>)ᏑNodeCoreModules).ValueSlot;

public static @string NonRelativeModuleNameForTypingCache(@string moduleName) {
    if (NodeCoreModules()[moduleName]) {
        return "node";
    }
    return moduleName;
}

} // end core_package
