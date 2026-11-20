namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using language = golang.org.x.text.language_package;
using golang.org.x.text;
using static locale_package;

partial class locale_package {

partial struct contextKey /*num:nint*/;

partial struct Locale /*global::go.golang.org.x.text.language_package.Tag*/;

public static readonly ж<Locale> ᏑDefault = new StaticBox<Locale>(default(Locale));
public static ref Locale Default => ref ((StaticBox<Locale>)ᏑDefault).ValueSlot;

} partial class localeᴛLocale {

public static @string String(this Locale l) {
    if (l == Default) {
        return "";
    }
    return ((language.Tag)l).String();
}

} partial class locale_package {

public static context.Context WithLocale(context.Context ctx, Locale locale) {
    return context.WithValue(ctx, (contextKey)0, locale);
}

public static Locale FromContext(context.Context ctx) {
    var (locale, _) = ctx.Value((contextKey)0)._<Locale>(ᐧ);
    return locale;
}

public static bool HasLocale(context.Context ctx) {
    var (_, ok) = ctx.Value((contextKey)0)._<Locale>(ᐧ);
    return ok;
}

public static (Locale locale, bool ok) Parse(@string localeStr) {
    var (tag, err) = language.Parse(localeStr);
    return ((Locale)tag, err == nil);
}

} // end locale_package
