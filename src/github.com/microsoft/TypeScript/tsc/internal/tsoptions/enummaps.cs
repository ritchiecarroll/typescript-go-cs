namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;

partial class tsoptions_package {

public static ж<collections.OrderedMap<@string, any>> LibMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "es5", Value: (@string)"lib.es5.d.ts"),
    new(Key: "es6", Value: (@string)"lib.es2015.d.ts"),
    new(Key: "es2015", Value: (@string)"lib.es2015.d.ts"),
    new(Key: "es7", Value: (@string)"lib.es2016.d.ts"),
    new(Key: "es2016", Value: (@string)"lib.es2016.d.ts"),
    new(Key: "es2017", Value: (@string)"lib.es2017.d.ts"),
    new(Key: "es2018", Value: (@string)"lib.es2018.d.ts"),
    new(Key: "es2019", Value: (@string)"lib.es2019.d.ts"),
    new(Key: "es2020", Value: (@string)"lib.es2020.d.ts"),
    new(Key: "es2021", Value: (@string)"lib.es2021.d.ts"),
    new(Key: "es2022", Value: (@string)"lib.es2022.d.ts"),
    new(Key: "es2023", Value: (@string)"lib.es2023.d.ts"),
    new(Key: "es2024", Value: (@string)"lib.es2024.d.ts"),
    new(Key: "es2025", Value: (@string)"lib.es2025.d.ts"),
    new(Key: "esnext", Value: (@string)"lib.esnext.d.ts"),
    new(Key: "dom", Value: (@string)"lib.dom.d.ts"),
    new(Key: "dom.iterable", Value: (@string)"lib.dom.iterable.d.ts"),
    new(Key: "dom.asynciterable", Value: (@string)"lib.dom.asynciterable.d.ts"),
    new(Key: "webworker", Value: (@string)"lib.webworker.d.ts"),
    new(Key: "webworker.importscripts", Value: (@string)"lib.webworker.importscripts.d.ts"),
    new(Key: "webworker.iterable", Value: (@string)"lib.webworker.iterable.d.ts"),
    new(Key: "webworker.asynciterable", Value: (@string)"lib.webworker.asynciterable.d.ts"),
    new(Key: "scripthost", Value: (@string)"lib.scripthost.d.ts"),
    new(Key: "es2015.core", Value: (@string)"lib.es2015.core.d.ts"),
    new(Key: "es2015.collection", Value: (@string)"lib.es2015.collection.d.ts"),
    new(Key: "es2015.generator", Value: (@string)"lib.es2015.generator.d.ts"),
    new(Key: "es2015.iterable", Value: (@string)"lib.es2015.iterable.d.ts"),
    new(Key: "es2015.promise", Value: (@string)"lib.es2015.promise.d.ts"),
    new(Key: "es2015.proxy", Value: (@string)"lib.es2015.proxy.d.ts"),
    new(Key: "es2015.reflect", Value: (@string)"lib.es2015.reflect.d.ts"),
    new(Key: "es2015.symbol", Value: (@string)"lib.es2015.symbol.d.ts"),
    new(Key: "es2015.symbol.wellknown", Value: (@string)"lib.es2015.symbol.wellknown.d.ts"),
    new(Key: "es2016.array.include", Value: (@string)"lib.es2016.array.include.d.ts"),
    new(Key: "es2016.intl", Value: (@string)"lib.es2016.intl.d.ts"),
    new(Key: "es2017.arraybuffer", Value: (@string)"lib.es2017.arraybuffer.d.ts"),
    new(Key: "es2017.date", Value: (@string)"lib.es2017.date.d.ts"),
    new(Key: "es2017.object", Value: (@string)"lib.es2017.object.d.ts"),
    new(Key: "es2017.sharedmemory", Value: (@string)"lib.es2017.sharedmemory.d.ts"),
    new(Key: "es2017.string", Value: (@string)"lib.es2017.string.d.ts"),
    new(Key: "es2017.intl", Value: (@string)"lib.es2017.intl.d.ts"),
    new(Key: "es2017.typedarrays", Value: (@string)"lib.es2017.typedarrays.d.ts"),
    new(Key: "es2018.asyncgenerator", Value: (@string)"lib.es2018.asyncgenerator.d.ts"),
    new(Key: "es2018.asynciterable", Value: (@string)"lib.es2018.asynciterable.d.ts"),
    new(Key: "es2018.intl", Value: (@string)"lib.es2018.intl.d.ts"),
    new(Key: "es2018.promise", Value: (@string)"lib.es2018.promise.d.ts"),
    new(Key: "es2018.regexp", Value: (@string)"lib.es2018.regexp.d.ts"),
    new(Key: "es2019.array", Value: (@string)"lib.es2019.array.d.ts"),
    new(Key: "es2019.object", Value: (@string)"lib.es2019.object.d.ts"),
    new(Key: "es2019.string", Value: (@string)"lib.es2019.string.d.ts"),
    new(Key: "es2019.symbol", Value: (@string)"lib.es2019.symbol.d.ts"),
    new(Key: "es2019.intl", Value: (@string)"lib.es2019.intl.d.ts"),
    new(Key: "es2020.bigint", Value: (@string)"lib.es2020.bigint.d.ts"),
    new(Key: "es2020.date", Value: (@string)"lib.es2020.date.d.ts"),
    new(Key: "es2020.promise", Value: (@string)"lib.es2020.promise.d.ts"),
    new(Key: "es2020.sharedmemory", Value: (@string)"lib.es2020.sharedmemory.d.ts"),
    new(Key: "es2020.string", Value: (@string)"lib.es2020.string.d.ts"),
    new(Key: "es2020.symbol.wellknown", Value: (@string)"lib.es2020.symbol.wellknown.d.ts"),
    new(Key: "es2020.intl", Value: (@string)"lib.es2020.intl.d.ts"),
    new(Key: "es2020.number", Value: (@string)"lib.es2020.number.d.ts"),
    new(Key: "es2021.promise", Value: (@string)"lib.es2021.promise.d.ts"),
    new(Key: "es2021.string", Value: (@string)"lib.es2021.string.d.ts"),
    new(Key: "es2021.weakref", Value: (@string)"lib.es2021.weakref.d.ts"),
    new(Key: "es2021.intl", Value: (@string)"lib.es2021.intl.d.ts"),
    new(Key: "es2022.array", Value: (@string)"lib.es2022.array.d.ts"),
    new(Key: "es2022.error", Value: (@string)"lib.es2022.error.d.ts"),
    new(Key: "es2022.intl", Value: (@string)"lib.es2022.intl.d.ts"),
    new(Key: "es2022.object", Value: (@string)"lib.es2022.object.d.ts"),
    new(Key: "es2022.string", Value: (@string)"lib.es2022.string.d.ts"),
    new(Key: "es2022.regexp", Value: (@string)"lib.es2022.regexp.d.ts"),
    new(Key: "es2023.array", Value: (@string)"lib.es2023.array.d.ts"),
    new(Key: "es2023.collection", Value: (@string)"lib.es2023.collection.d.ts"),
    new(Key: "es2023.intl", Value: (@string)"lib.es2023.intl.d.ts"),
    new(Key: "es2024.arraybuffer", Value: (@string)"lib.es2024.arraybuffer.d.ts"),
    new(Key: "es2024.collection", Value: (@string)"lib.es2024.collection.d.ts"),
    new(Key: "es2024.object", Value: (@string)"lib.es2024.object.d.ts"),
    new(Key: "es2024.promise", Value: (@string)"lib.es2024.promise.d.ts"),
    new(Key: "es2024.regexp", Value: (@string)"lib.es2024.regexp.d.ts"),
    new(Key: "es2024.sharedmemory", Value: (@string)"lib.es2024.sharedmemory.d.ts"),
    new(Key: "es2024.string", Value: (@string)"lib.es2024.string.d.ts"),
    new(Key: "es2025.collection", Value: (@string)"lib.es2025.collection.d.ts"),
    new(Key: "es2025.float16", Value: (@string)"lib.es2025.float16.d.ts"),
    new(Key: "es2025.intl", Value: (@string)"lib.es2025.intl.d.ts"),
    new(Key: "es2025.iterator", Value: (@string)"lib.es2025.iterator.d.ts"),
    new(Key: "es2025.promise", Value: (@string)"lib.es2025.promise.d.ts"),
    new(Key: "es2025.regexp", Value: (@string)"lib.es2025.regexp.d.ts"),
    new(Key: "esnext.asynciterable", Value: (@string)"lib.es2018.asynciterable.d.ts"),
    new(Key: "esnext.symbol", Value: (@string)"lib.es2019.symbol.d.ts"),
    new(Key: "esnext.bigint", Value: (@string)"lib.es2020.bigint.d.ts"),
    new(Key: "esnext.weakref", Value: (@string)"lib.es2021.weakref.d.ts"),
    new(Key: "esnext.object", Value: (@string)"lib.es2024.object.d.ts"),
    new(Key: "esnext.regexp", Value: (@string)"lib.es2024.regexp.d.ts"),
    new(Key: "esnext.string", Value: (@string)"lib.es2024.string.d.ts"),
    new(Key: "esnext.float16", Value: (@string)"lib.es2025.float16.d.ts"),
    new(Key: "esnext.iterator", Value: (@string)"lib.es2025.iterator.d.ts"),
    new(Key: "esnext.promise", Value: (@string)"lib.es2025.promise.d.ts"),
    new(Key: "esnext.array", Value: (@string)"lib.esnext.array.d.ts"),
    new(Key: "esnext.collection", Value: (@string)"lib.esnext.collection.d.ts"),
    new(Key: "esnext.date", Value: (@string)"lib.esnext.date.d.ts"),
    new(Key: "esnext.decorators", Value: (@string)"lib.esnext.decorators.d.ts"),
    new(Key: "esnext.disposable", Value: (@string)"lib.esnext.disposable.d.ts"),
    new(Key: "esnext.error", Value: (@string)"lib.esnext.error.d.ts"),
    new(Key: "esnext.intl", Value: (@string)"lib.esnext.intl.d.ts"),
    new(Key: "esnext.sharedmemory", Value: (@string)"lib.esnext.sharedmemory.d.ts"),
    new(Key: "esnext.temporal", Value: (@string)"lib.esnext.temporal.d.ts"),
    new(Key: "esnext.typedarrays", Value: (@string)"lib.esnext.typedarrays.d.ts"),
    new(Key: "decorators", Value: (@string)"lib.decorators.d.ts"),
    new(Key: "decorators.legacy", Value: (@string)"lib.decorators.legacy.d.ts")
]));

public static readonly ж<slice<@string>> ᏑLibs = new StaticBox<slice<@string>>(slices.Collect(LibMap.Keys()));
public static ref slice<@string> Libs => ref ((StaticBox<slice<@string>>)ᏑLibs).ValueSlot;
public static ж<collections.Set<@string>> LibFilesSet = collections.NewSetFromItems(core.Map(slices.Collect(LibMap.Values()), @string (any s) => s._<@string>()).ꓸꓸꓸ);

public static (@string, bool) GetLibFileName(@string libName) {
    libName = tspath.ToFileNameLowerCase(libName);
    if (LibFilesSet.Has(libName)) {
        return (libName, true);
    }
    var (lib, ok) = LibMap.Get(libName);
    if (!ok) {
        return ("", false);
    }
    return (lib._<@string>(), true);
}

internal static ж<collections.OrderedMap<@string, any>> moduleResolutionOptionMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "node16", Value: core.ModuleResolutionKindNode16),
    new(Key: "nodenext", Value: core.ModuleResolutionKindNodeNext),
    new(Key: "bundler", Value: core.ModuleResolutionKindBundler),
    new(Key: "classic", Value: core.ModuleResolutionKindClassic),
    new(Key: "node", Value: core.ModuleResolutionKindNode10),
    new(Key: "node10", Value: core.ModuleResolutionKindNode10)
]));

internal static ж<collections.OrderedMap<@string, any>> targetOptionMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "es5", Value: core.ScriptTargetES5),
    new(Key: "es6", Value: core.ScriptTargetES2015),
    new(Key: "es2015", Value: core.ScriptTargetES2015),
    new(Key: "es2016", Value: core.ScriptTargetES2016),
    new(Key: "es2017", Value: core.ScriptTargetES2017),
    new(Key: "es2018", Value: core.ScriptTargetES2018),
    new(Key: "es2019", Value: core.ScriptTargetES2019),
    new(Key: "es2020", Value: core.ScriptTargetES2020),
    new(Key: "es2021", Value: core.ScriptTargetES2021),
    new(Key: "es2022", Value: core.ScriptTargetES2022),
    new(Key: "es2023", Value: core.ScriptTargetES2023),
    new(Key: "es2024", Value: core.ScriptTargetES2024),
    new(Key: "es2025", Value: core.ScriptTargetES2025),
    new(Key: "esnext", Value: core.ScriptTargetESNext)
]));

internal static ж<collections.OrderedMap<@string, any>> moduleOptionMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "commonjs", Value: core.ModuleKindCommonJS),
    new(Key: "amd", Value: core.ModuleKindAMD),
    new(Key: "system", Value: core.ModuleKindSystem),
    new(Key: "umd", Value: core.ModuleKindUMD),
    new(Key: "es6", Value: core.ModuleKindES2015),
    new(Key: "es2015", Value: core.ModuleKindES2015),
    new(Key: "es2020", Value: core.ModuleKindES2020),
    new(Key: "es2022", Value: core.ModuleKindES2022),
    new(Key: "esnext", Value: core.ModuleKindESNext),
    new(Key: "node16", Value: core.ModuleKindNode16),
    new(Key: "node18", Value: core.ModuleKindNode18),
    new(Key: "node20", Value: core.ModuleKindNode20),
    new(Key: "nodenext", Value: core.ModuleKindNodeNext),
    new(Key: "preserve", Value: core.ModuleKindPreserve)
]));

internal static ж<collections.OrderedMap<@string, any>> moduleDetectionOptionMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "auto", Value: core.ModuleDetectionKindAuto),
    new(Key: "legacy", Value: core.ModuleDetectionKindLegacy),
    new(Key: "force", Value: core.ModuleDetectionKindForce)
]));

internal static ж<collections.OrderedMap<@string, any>> jsxOptionMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "preserve", Value: core.JsxEmitPreserve),
    new(Key: "react-native", Value: core.JsxEmitReactNative),
    new(Key: "react-jsx", Value: core.JsxEmitReactJSX),
    new(Key: "react-jsxdev", Value: core.JsxEmitReactJSXDev),
    new(Key: "react", Value: core.JsxEmitReact)
]));

internal static ж<collections.OrderedMap<@string, any>> newLineOptionMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "crlf", Value: core.NewLineKindCRLF),
    new(Key: "lf", Value: core.NewLineKindLF)
]));

[GoLazy] partial class lazyᴛtargetToLibMap { internal static map<core.ScriptTarget, @string> targetToLibMap = new map<core.ScriptTarget, @string>{
    [core.ScriptTargetESNext] = "lib.esnext.full.d.ts",
    [core.ScriptTargetES2025] = "lib.es2025.full.d.ts",
    [core.ScriptTargetES2024] = "lib.es2024.full.d.ts",
    [core.ScriptTargetES2023] = "lib.es2023.full.d.ts",
    [core.ScriptTargetES2022] = "lib.es2022.full.d.ts",
    [core.ScriptTargetES2021] = "lib.es2021.full.d.ts",
    [core.ScriptTargetES2020] = "lib.es2020.full.d.ts",
    [core.ScriptTargetES2019] = "lib.es2019.full.d.ts",
    [core.ScriptTargetES2018] = "lib.es2018.full.d.ts",
    [core.ScriptTargetES2017] = "lib.es2017.full.d.ts",
    [core.ScriptTargetES2016] = "lib.es2016.full.d.ts",
    [core.ScriptTargetES2015] = "lib.es6.d.ts"
}; }

public static map<core.ScriptTarget, @string> TargetToLibMap() {
    return targetToLibMap;
}

public static @string GetDefaultLibFileName(ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    var (name, ok) = targetToLibMap[options.GetEmitScriptTarget(), ꟷ];
    if (!ok) {
        return "lib.d.ts";
    }
    return name;
}

internal static ж<collections.OrderedMap<@string, any>> watchFileEnumMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "fixedpollinginterval", Value: core.WatchFileKindFixedPollingInterval),
    new(Key: "prioritypollinginterval", Value: core.WatchFileKindPriorityPollingInterval),
    new(Key: "dynamicprioritypolling", Value: core.WatchFileKindDynamicPriorityPolling),
    new(Key: "fixedchunksizepolling", Value: core.WatchFileKindFixedChunkSizePolling),
    new(Key: "usefsevents", Value: core.WatchFileKindUseFsEvents),
    new(Key: "usefseventsonparentdirectory", Value: core.WatchFileKindUseFsEventsOnParentDirectory)
]));

internal static ж<collections.OrderedMap<@string, any>> watchDirectoryEnumMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "usefsevents", Value: core.WatchDirectoryKindUseFsEvents),
    new(Key: "fixedpollinginterval", Value: core.WatchDirectoryKindFixedPollingInterval),
    new(Key: "dynamicprioritypolling", Value: core.WatchDirectoryKindDynamicPriorityPolling),
    new(Key: "fixedchunksizepolling", Value: core.WatchDirectoryKindFixedChunkSizePolling)
]));

internal static ж<collections.OrderedMap<@string, any>> fallbackEnumMap = collections.NewOrderedMapFromList(slice<collections.MapEntry<@string, any>>([
    new(Key: "fixedinterval", Value: core.PollingKindFixedInterval),
    new(Key: "priorityinterval", Value: core.PollingKindPriorityInterval),
    new(Key: "dynamicpriority", Value: core.PollingKindDynamicPriority),
    new(Key: "fixedchunksize", Value: core.PollingKindFixedChunkSize)
]));

} // end tsoptions_package
