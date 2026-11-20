namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using static packagejson_package;

partial class packagejson_package {

partial struct objectKind /*num:int8*/;

internal static objectKind objectKindUnknown => /* iota */ 0;
internal static objectKind objectKindSubpaths => 1;
internal static objectKind objectKindConditions => 2;
internal static objectKind objectKindImports => 3;
internal static objectKind objectKindInvalid => 4;

partial struct ExportsOrImports {
    /*embed*/ public JSONValue JSONValue;
    internal objectKind objectKind;
}

internal static jsonꓸUnmarshalerFrom _ᴛ1ʗ = new ExportsOrImportsжUnmarshalerFrom(((ж<ExportsOrImports>)nil));

} partial class packagejsonᴛExportsOrImports {

public static error UnmarshalJSONFrom(this ж<ExportsOrImports> Ꮡe, ж<jsontext.Decoder> dec) {
    return unmarshalJSONValueV2<ExportsOrImports>(Ꮡe.ᏑJSONValue, dec);
}

public static ж<collections.OrderedMap<@string, ExportsOrImports>> AsObject(this ExportsOrImports e) {
    if (e.Type != JSONValueTypeObject) {
        throw panic("expected object");
    }
    return e.Value._<ж<collections.OrderedMap<@string, ExportsOrImports>>>();
}

public static slice<ExportsOrImports> AsArray(this ExportsOrImports e) {
    if (e.Type != JSONValueTypeArray) {
        throw panic("expected array");
    }
    return e.Value._<slice<ExportsOrImports>>();
}

public static bool IsSubpaths(this ExportsOrImports eʗp) {
    ref var e = ref heap(eʗp, out var Ꮡe);

    Ꮡe.initObjectKind();
    return e.objectKind == objectKindSubpaths;
}

public static bool IsImports(this ExportsOrImports eʗp) {
    ref var e = ref heap(eʗp, out var Ꮡe);

    Ꮡe.initObjectKind();
    return e.objectKind == objectKindImports;
}

public static bool IsConditions(this ExportsOrImports eʗp) {
    ref var e = ref heap(eʗp, out var Ꮡe);

    Ꮡe.initObjectKind();
    return e.objectKind == objectKindConditions;
}

internal static void initObjectKind(this ж<ExportsOrImports> Ꮡe) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (e.objectKind == objectKindUnknown && e.Type == JSONValueTypeObject) {
        {
            var obj = Ꮡe.AsObject(); if (obj.Size() > 0) {
                var (seenDot, seenHash, seenOther) = (false, false, false);
                foreach (var k in range<@string>(obj.Keys().Invoke)) {
                    if (len(k) > 0) {
                        seenDot = seenDot || k[0] == '.';
                        seenHash = seenHash || k[0] == '#';
                        seenOther = seenOther || (k[0] != '.' && k[0] != '#');
                        if (seenOther && (seenDot || seenHash)) {
                            e.objectKind = objectKindInvalid;
                            return;
                        }
                    }
                }
                if (seenDot) {
                    e.objectKind = objectKindSubpaths;
                    return;
                }
                if (seenHash) {
                    e.objectKind = objectKindImports;
                    return;
                }
            }
        }
        e.objectKind = objectKindConditions;
    }
}

} // end packagejsonᴛExportsOrImports
