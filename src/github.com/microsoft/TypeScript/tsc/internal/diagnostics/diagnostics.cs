namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using regexp = regexp_package;
using strconv = strconv_package;
using strings = strings_package;
using sync = sync_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using language = golang.org.x.text.language_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using golang.org.x.text;
using ꓸꓸꓸany = Span<any>;
using ꓸꓸꓸstring = Span<@string>;
using diagnostics = diagnostics_package;
using static diagnostics_package;

partial class diagnostics_package {

partial struct Category /*num:int32*/;

public static Category CategoryWarning => /* iota */ 0;
public static Category CategoryError => 1;
public static Category CategorySuggestion => 2;
public static Category CategoryMessage => 3;

} partial class diagnosticsᴛCategory {

public static @string Name(this Category category) {
    switch (category) {
    case var ᴛ when ᴛ == CategoryWarning: {
        return "warning";
    }
    case var ᴛ when ᴛ == CategoryError: {
        return "error";
    }
    case var ᴛ when ᴛ == CategorySuggestion: {
        return "suggestion";
    }
    case var ᴛ when ᴛ == CategoryMessage: {
        return "message";
    }}

    throw panic("Unhandled diagnostic category");
}

} partial class diagnostics_package {

partial struct Key /*@string*/;

partial struct Message {
    internal int32 code;
    internal Category category;
    internal Key key;
    internal @string text;
    internal bool reportsUnnecessary;
    internal bool elidedInCompatibilityPyramid;
    internal bool reportsDeprecated;
}

} partial class diagnosticsᴛMessage {

public static int32 Code(this ref Message m) { return m.code; }
public static Category Category(this ref Message m) { return m.category; }
public static Key Key(this ref Message m) { return m.key; }
public static bool ReportsUnnecessary(this ref Message m) { return m.reportsUnnecessary; }
public static bool ElidedInCompatibilityPyramid(this ref Message m) { return m.elidedInCompatibilityPyramid; }
public static bool ReportsDeprecated(this ref Message m) { return m.reportsDeprecated; }

public static @string String(this ref Message m) {
    return m.text;
}

public static @string Localize(this ж<Message> Ꮡm, locale.Locale localeΔ1, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.slice();

    return diagnostics.Localize(localeΔ1, Ꮡm, ""u8, StringifyArgs(args).ꓸꓸꓸ);
}

} partial class diagnostics_package {

public static @string Localize(locale.Locale localeΔ1, ж<Message> Ꮡmessage, Key key, params ꓸꓸꓸstring argsʗp) {
    var args = argsʗp.slice();
    ref var message = ref Ꮡmessage.DerefOrNull();

    if (Ꮡmessage == nil) {
        Ꮡmessage = keyToMessage(key); message = ref Ꮡmessage.DerefOrNull();
    }
    if (Ꮡmessage == nil) {
        throw panic("Unknown diagnostic message: " + ((@string)key));
    }

    @string text = message.text;
    var (localized, ok) = getLocalizedMessages((language.Tag)localeΔ1)[message.key, ꟷ]; if (ok) {
        text = localized;
    }

    return Format(text, args);
}

internal static readonly ж<sync.Map> ᏑlocalizedMessagesCache = new StaticBox<sync.Map>(default(sync.Map));
internal static ref sync.Map localizedMessagesCache => ref ((StaticBox<sync.Map>)ᏑlocalizedMessagesCache).ValueSlot;

internal static map<Key, @string> getLocalizedMessages(language.Tag loc) {
    if (loc == language.Und) {
        return default;
    }

    var (cached, ok) = ᏑlocalizedMessagesCache.Load(loc); if (ok) {
        if (cached == nil) {
            return default;
        }
        return cached._<map<Key, @string>>();
    }

    map<Key, @string> messages = default;

    var (_, index, confidence) = matcher.Match(loc);
    if (confidence >= language.Low && index >= 0 && index < len(localeFuncs)) {
        var fn = localeFuncs[index]; if (fn != nil) {
            messages = fn();
        }
    }

    ᏑlocalizedMessagesCache.Store(loc, messages);
    return messages;
}

internal static ж<regexp.Regexp> placeholderRegexp = regexp.MustCompile(@"{(\d+)}");

public static @string Format(@string text, slice<@string> args) {
    if (len(args) == 0) {
        return text;
    }

    args = core.SameMap(args, @string (@string arg) => strings.ToValidUTF8(arg, "\uFFFD"));

    var argsʗ1 = args;
    return placeholderRegexp.ReplaceAllStringFunc(text, (@string match) => {
        var (index, err) = strconv.ParseInt(match.slice(1, len(match) - 1), 10, 0);
        if (err != nil || (nint)index >= len(argsʗ1)) {
            throw panic("Invalid formatting placeholder");
        }
        return argsʗ1[(nint)index];
    });
}

public static slice<@string> StringifyArgs(slice<any> args) {
    if (len(args) == 0) {
        return default;
    }

    var result = new slice<@string>(len(args));
    foreach (var (i, arg) in range(args)) {
        var (s, ok) = arg._<@string>(ᐧ); if (ok) {
            result[i] = s;
        } else {
            result[i] = fmt.Sprintf("%v"u8, arg);
        }
    }
    return result;
}

public static ж<Message> NewAdHocMessage(@string message) {
    return Ꮡ(new Message(
        code: -1,
        category: CategoryError,
        key: (@string)"-1",
        text: message
    ));
}

} // end diagnostics_package
