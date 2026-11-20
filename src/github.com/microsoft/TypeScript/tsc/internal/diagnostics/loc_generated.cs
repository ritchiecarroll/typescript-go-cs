namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using gzip = compress.gzip_package;
// blank import: embed_package (side effects only; no using emitted — a `using _` alias hijacks C# discards)
using strings = strings_package;
using sync = sync_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using language = golang.org.x.text.language_package;
using compress;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using golang.org.x.text;
using io = io_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;

partial class diagnostics_package {

internal static language.Matcher matcher = language.NewMatcher(slice<language.Tag>([
    language.English,
    language.MustParse("zh-CN"),
    language.MustParse("zh-TW"),
    language.MustParse("cs-CZ"),
    language.MustParse("de-DE"),
    language.MustParse("es-ES"),
    language.MustParse("fr-FR"),
    language.MustParse("it-IT"),
    language.MustParse("ja-JP"),
    language.MustParse("ko-KR"),
    language.MustParse("pl-PL"),
    language.MustParse("pt-BR"),
    language.MustParse("ru-RU"),
    language.MustParse("tr-TR")
]));

internal static slice<Func<map<Key, @string>>> localeFuncs;
internal static void initᴛlocaleFuncs() { localeFuncs = [
    default,
    zhCN,
    zhTW,
    csCZ,
    deDE,
    esES,
    frFR,
    itIT,
    jaJP,
    koKR,
    plPL,
    ptBR,
    ruRU,
    trTR
]; }

internal static map<Key, @string> loadLocaleData(@string data) {
    GoFrame ᒐ = default;
    try {
        var (gr, err) = gzip.NewReader(new strings_ReaderжReader(strings.NewReader(data)));
        if (err != nil) {
            throw panic("failed to create gzip reader: " + err.Error());
        }
        var grʗ1 = gr;
        defer(() => grʗ1.Close(), ref ᒐ);
        ref var result = ref heap<map<Key, @string>>(out var Ꮡresult);
        var errΔ1 = json.UnmarshalRead(new gzip_ReaderжReader(gr), Ꮡresult); if (errΔ1 != nil) {
            throw panic("failed to unmarshal locale data: " + errΔ1.Error());
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static @string zhCNData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/zh-CN.json.gz");

internal static Func<map<Key, @string>> zhCN = sync.OnceValue(map<Key, @string> () => loadLocaleData(zhCNData));

internal static @string zhTWData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/zh-TW.json.gz");

internal static Func<map<Key, @string>> zhTW = sync.OnceValue(map<Key, @string> () => loadLocaleData(zhTWData));

internal static @string csCZData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/cs-CZ.json.gz");

internal static Func<map<Key, @string>> csCZ = sync.OnceValue(map<Key, @string> () => loadLocaleData(csCZData));

internal static @string deDEData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/de-DE.json.gz");

internal static Func<map<Key, @string>> deDE = sync.OnceValue(map<Key, @string> () => loadLocaleData(deDEData));

internal static @string esESData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/es-ES.json.gz");

internal static Func<map<Key, @string>> esES = sync.OnceValue(map<Key, @string> () => loadLocaleData(esESData));

internal static @string frFRData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/fr-FR.json.gz");

internal static Func<map<Key, @string>> frFR = sync.OnceValue(map<Key, @string> () => loadLocaleData(frFRData));

internal static @string itITData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/it-IT.json.gz");

internal static Func<map<Key, @string>> itIT = sync.OnceValue(map<Key, @string> () => loadLocaleData(itITData));

internal static @string jaJPData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/ja-JP.json.gz");

internal static Func<map<Key, @string>> jaJP = sync.OnceValue(map<Key, @string> () => loadLocaleData(jaJPData));

internal static @string koKRData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/ko-KR.json.gz");

internal static Func<map<Key, @string>> koKR = sync.OnceValue(map<Key, @string> () => loadLocaleData(koKRData));

internal static @string plPLData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/pl-PL.json.gz");

internal static Func<map<Key, @string>> plPL = sync.OnceValue(map<Key, @string> () => loadLocaleData(plPLData));

internal static @string ptBRData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/pt-BR.json.gz");

internal static Func<map<Key, @string>> ptBR = sync.OnceValue(map<Key, @string> () => loadLocaleData(ptBRData));

internal static @string ruRUData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/ru-RU.json.gz");

internal static Func<map<Key, @string>> ruRU = sync.OnceValue(map<Key, @string> () => loadLocaleData(ruRUData));

internal static @string trTRData = go.embed_package.ΔEmbedString(typeof(diagnostics_package).Assembly, "go.embed/github.com/microsoft/TypeScript/tsc/internal/diagnostics/", "loc/tr-TR.json.gz");

internal static Func<map<Key, @string>> trTR = sync.OnceValue(map<Key, @string> () => loadLocaleData(trTRData));

} // end diagnostics_package
