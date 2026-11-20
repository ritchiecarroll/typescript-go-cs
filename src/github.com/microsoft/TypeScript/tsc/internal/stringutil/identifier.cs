namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using unicode = unicode_package;

partial class stringutil_package {

public static bool IsUnicodeIdentifierStart(rune ch) {
    return unicode.Is(unicodeESNextIdentifierStart, ch);
}

public static bool IsUnicodeIdentifierPart(rune ch) {
    return unicode.Is(unicodeESNextIdentifierPart, ch);
}

} // end stringutil_package
