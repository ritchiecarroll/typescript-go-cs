// Copyright 2013 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text;

using compact = go.golang.org.x.text.@internal.language.compact_package;
using go.golang.org.x.text.@internal.language;
using static language_package;

partial class language_package {

public static Tag MustParse(@string s) {
    var (t, err) = Parse(s);
    if (err != nil) {
        throw panic(err);
    }
    return t;
}

} partial class languageᴛCanonType {

public static Tag MustParse(this CanonType c, @string s) {
    var (t, err) = c.Parse(s);
    if (err != nil) {
        throw panic(err);
    }
    return t;
}

} partial class language_package {

public static Base MustParseBase(@string s) {
    var (b, err) = ParseBase(s);
    if (err != nil) {
        throw panic(err);
    }
    return b;
}

public static Script MustParseScript(@string s) {
    var (scr, err) = ParseScript(s);
    if (err != nil) {
        throw panic(err);
    }
    return scr;
}

public static Region MustParseRegion(@string s) {
    var (r, err) = ParseRegion(s);
    if (err != nil) {
        throw panic(err);
    }
    return r;
}

internal static Tag und = new Tag(new compact.Tag());
public static readonly ж<Tag> ᏑUnd = new StaticBox<Tag>(new Tag(new compact.Tag()));
public static ref Tag Und => ref ((StaticBox<Tag>)ᏑUnd).ValueSlot;
public static readonly ж<Tag> ᏑAfrikaans = new StaticBox<Tag>(((Tag)compact.Afrikaans));
public static ref Tag Afrikaans => ref ((StaticBox<Tag>)ᏑAfrikaans).ValueSlot;
public static readonly ж<Tag> ᏑAmharic = new StaticBox<Tag>(((Tag)compact.Amharic));
public static ref Tag Amharic => ref ((StaticBox<Tag>)ᏑAmharic).ValueSlot;
public static readonly ж<Tag> ᏑArabic = new StaticBox<Tag>(((Tag)compact.Arabic));
public static ref Tag Arabic => ref ((StaticBox<Tag>)ᏑArabic).ValueSlot;
public static readonly ж<Tag> ᏑModernStandardArabic = new StaticBox<Tag>(((Tag)compact.ModernStandardArabic));
public static ref Tag ModernStandardArabic => ref ((StaticBox<Tag>)ᏑModernStandardArabic).ValueSlot;
public static readonly ж<Tag> ᏑAzerbaijani = new StaticBox<Tag>(((Tag)compact.Azerbaijani));
public static ref Tag Azerbaijani => ref ((StaticBox<Tag>)ᏑAzerbaijani).ValueSlot;
public static readonly ж<Tag> ᏑBulgarian = new StaticBox<Tag>(((Tag)compact.Bulgarian));
public static ref Tag Bulgarian => ref ((StaticBox<Tag>)ᏑBulgarian).ValueSlot;
public static readonly ж<Tag> ᏑBengali = new StaticBox<Tag>(((Tag)compact.Bengali));
public static ref Tag Bengali => ref ((StaticBox<Tag>)ᏑBengali).ValueSlot;
public static readonly ж<Tag> ᏑCatalan = new StaticBox<Tag>(((Tag)compact.Catalan));
public static ref Tag Catalan => ref ((StaticBox<Tag>)ᏑCatalan).ValueSlot;
public static readonly ж<Tag> ᏑCzech = new StaticBox<Tag>(((Tag)compact.Czech));
public static ref Tag Czech => ref ((StaticBox<Tag>)ᏑCzech).ValueSlot;
public static readonly ж<Tag> ᏑDanish = new StaticBox<Tag>(((Tag)compact.Danish));
public static ref Tag Danish => ref ((StaticBox<Tag>)ᏑDanish).ValueSlot;
public static readonly ж<Tag> ᏑGerman = new StaticBox<Tag>(((Tag)compact.German));
public static ref Tag German => ref ((StaticBox<Tag>)ᏑGerman).ValueSlot;
public static readonly ж<Tag> ᏑGreek = new StaticBox<Tag>(((Tag)compact.Greek));
public static ref Tag Greek => ref ((StaticBox<Tag>)ᏑGreek).ValueSlot;
public static readonly ж<Tag> ᏑEnglish = new StaticBox<Tag>(((Tag)compact.English));
public static ref Tag English => ref ((StaticBox<Tag>)ᏑEnglish).ValueSlot;
public static readonly ж<Tag> ᏑAmericanEnglish = new StaticBox<Tag>(((Tag)compact.AmericanEnglish));
public static ref Tag AmericanEnglish => ref ((StaticBox<Tag>)ᏑAmericanEnglish).ValueSlot;
public static readonly ж<Tag> ᏑBritishEnglish = new StaticBox<Tag>(((Tag)compact.BritishEnglish));
public static ref Tag BritishEnglish => ref ((StaticBox<Tag>)ᏑBritishEnglish).ValueSlot;
public static readonly ж<Tag> ᏑSpanish = new StaticBox<Tag>(((Tag)compact.Spanish));
public static ref Tag Spanish => ref ((StaticBox<Tag>)ᏑSpanish).ValueSlot;
public static readonly ж<Tag> ᏑEuropeanSpanish = new StaticBox<Tag>(((Tag)compact.EuropeanSpanish));
public static ref Tag EuropeanSpanish => ref ((StaticBox<Tag>)ᏑEuropeanSpanish).ValueSlot;
public static readonly ж<Tag> ᏑLatinAmericanSpanish = new StaticBox<Tag>(((Tag)compact.LatinAmericanSpanish));
public static ref Tag LatinAmericanSpanish => ref ((StaticBox<Tag>)ᏑLatinAmericanSpanish).ValueSlot;
public static readonly ж<Tag> ᏑEstonian = new StaticBox<Tag>(((Tag)compact.Estonian));
public static ref Tag Estonian => ref ((StaticBox<Tag>)ᏑEstonian).ValueSlot;
public static readonly ж<Tag> ᏑPersian = new StaticBox<Tag>(((Tag)compact.Persian));
public static ref Tag Persian => ref ((StaticBox<Tag>)ᏑPersian).ValueSlot;
public static readonly ж<Tag> ᏑFinnish = new StaticBox<Tag>(((Tag)compact.Finnish));
public static ref Tag Finnish => ref ((StaticBox<Tag>)ᏑFinnish).ValueSlot;
public static readonly ж<Tag> ᏑFilipino = new StaticBox<Tag>(((Tag)compact.Filipino));
public static ref Tag Filipino => ref ((StaticBox<Tag>)ᏑFilipino).ValueSlot;
public static readonly ж<Tag> ᏑFrench = new StaticBox<Tag>(((Tag)compact.French));
public static ref Tag French => ref ((StaticBox<Tag>)ᏑFrench).ValueSlot;
public static readonly ж<Tag> ᏑCanadianFrench = new StaticBox<Tag>(((Tag)compact.CanadianFrench));
public static ref Tag CanadianFrench => ref ((StaticBox<Tag>)ᏑCanadianFrench).ValueSlot;
public static readonly ж<Tag> ᏑGujarati = new StaticBox<Tag>(((Tag)compact.Gujarati));
public static ref Tag Gujarati => ref ((StaticBox<Tag>)ᏑGujarati).ValueSlot;
public static readonly ж<Tag> ᏑHebrew = new StaticBox<Tag>(((Tag)compact.Hebrew));
public static ref Tag Hebrew => ref ((StaticBox<Tag>)ᏑHebrew).ValueSlot;
public static readonly ж<Tag> ᏑHindi = new StaticBox<Tag>(((Tag)compact.Hindi));
public static ref Tag Hindi => ref ((StaticBox<Tag>)ᏑHindi).ValueSlot;
public static readonly ж<Tag> ᏑCroatian = new StaticBox<Tag>(((Tag)compact.Croatian));
public static ref Tag Croatian => ref ((StaticBox<Tag>)ᏑCroatian).ValueSlot;
public static readonly ж<Tag> ᏑHungarian = new StaticBox<Tag>(((Tag)compact.Hungarian));
public static ref Tag Hungarian => ref ((StaticBox<Tag>)ᏑHungarian).ValueSlot;
public static readonly ж<Tag> ᏑArmenian = new StaticBox<Tag>(((Tag)compact.Armenian));
public static ref Tag Armenian => ref ((StaticBox<Tag>)ᏑArmenian).ValueSlot;
public static readonly ж<Tag> ᏑIndonesian = new StaticBox<Tag>(((Tag)compact.Indonesian));
public static ref Tag Indonesian => ref ((StaticBox<Tag>)ᏑIndonesian).ValueSlot;
public static readonly ж<Tag> ᏑIcelandic = new StaticBox<Tag>(((Tag)compact.Icelandic));
public static ref Tag Icelandic => ref ((StaticBox<Tag>)ᏑIcelandic).ValueSlot;
public static readonly ж<Tag> ᏑItalian = new StaticBox<Tag>(((Tag)compact.Italian));
public static ref Tag Italian => ref ((StaticBox<Tag>)ᏑItalian).ValueSlot;
public static readonly ж<Tag> ᏑJapanese = new StaticBox<Tag>(((Tag)compact.Japanese));
public static ref Tag Japanese => ref ((StaticBox<Tag>)ᏑJapanese).ValueSlot;
public static readonly ж<Tag> ᏑGeorgian = new StaticBox<Tag>(((Tag)compact.Georgian));
public static ref Tag Georgian => ref ((StaticBox<Tag>)ᏑGeorgian).ValueSlot;
public static readonly ж<Tag> ᏑKazakh = new StaticBox<Tag>(((Tag)compact.Kazakh));
public static ref Tag Kazakh => ref ((StaticBox<Tag>)ᏑKazakh).ValueSlot;
public static readonly ж<Tag> ᏑKhmer = new StaticBox<Tag>(((Tag)compact.Khmer));
public static ref Tag Khmer => ref ((StaticBox<Tag>)ᏑKhmer).ValueSlot;
public static readonly ж<Tag> ᏑKannada = new StaticBox<Tag>(((Tag)compact.Kannada));
public static ref Tag Kannada => ref ((StaticBox<Tag>)ᏑKannada).ValueSlot;
public static readonly ж<Tag> ᏑKorean = new StaticBox<Tag>(((Tag)compact.Korean));
public static ref Tag Korean => ref ((StaticBox<Tag>)ᏑKorean).ValueSlot;
public static readonly ж<Tag> ᏑKirghiz = new StaticBox<Tag>(((Tag)compact.Kirghiz));
public static ref Tag Kirghiz => ref ((StaticBox<Tag>)ᏑKirghiz).ValueSlot;
public static readonly ж<Tag> ᏑLao = new StaticBox<Tag>(((Tag)compact.Lao));
public static ref Tag Lao => ref ((StaticBox<Tag>)ᏑLao).ValueSlot;
public static readonly ж<Tag> ᏑLithuanian = new StaticBox<Tag>(((Tag)compact.Lithuanian));
public static ref Tag Lithuanian => ref ((StaticBox<Tag>)ᏑLithuanian).ValueSlot;
public static readonly ж<Tag> ᏑLatvian = new StaticBox<Tag>(((Tag)compact.Latvian));
public static ref Tag Latvian => ref ((StaticBox<Tag>)ᏑLatvian).ValueSlot;
public static readonly ж<Tag> ᏑMacedonian = new StaticBox<Tag>(((Tag)compact.Macedonian));
public static ref Tag Macedonian => ref ((StaticBox<Tag>)ᏑMacedonian).ValueSlot;
public static readonly ж<Tag> ᏑMalayalam = new StaticBox<Tag>(((Tag)compact.Malayalam));
public static ref Tag Malayalam => ref ((StaticBox<Tag>)ᏑMalayalam).ValueSlot;
public static readonly ж<Tag> ᏑMongolian = new StaticBox<Tag>(((Tag)compact.Mongolian));
public static ref Tag Mongolian => ref ((StaticBox<Tag>)ᏑMongolian).ValueSlot;
public static readonly ж<Tag> ᏑMarathi = new StaticBox<Tag>(((Tag)compact.Marathi));
public static ref Tag Marathi => ref ((StaticBox<Tag>)ᏑMarathi).ValueSlot;
public static readonly ж<Tag> ᏑMalay = new StaticBox<Tag>(((Tag)compact.Malay));
public static ref Tag Malay => ref ((StaticBox<Tag>)ᏑMalay).ValueSlot;
public static readonly ж<Tag> ᏑBurmese = new StaticBox<Tag>(((Tag)compact.Burmese));
public static ref Tag Burmese => ref ((StaticBox<Tag>)ᏑBurmese).ValueSlot;
public static readonly ж<Tag> ᏑNepali = new StaticBox<Tag>(((Tag)compact.Nepali));
public static ref Tag Nepali => ref ((StaticBox<Tag>)ᏑNepali).ValueSlot;
public static readonly ж<Tag> ᏑDutch = new StaticBox<Tag>(((Tag)compact.Dutch));
public static ref Tag Dutch => ref ((StaticBox<Tag>)ᏑDutch).ValueSlot;
public static readonly ж<Tag> ᏑNorwegian = new StaticBox<Tag>(((Tag)compact.Norwegian));
public static ref Tag Norwegian => ref ((StaticBox<Tag>)ᏑNorwegian).ValueSlot;
public static readonly ж<Tag> ᏑPunjabi = new StaticBox<Tag>(((Tag)compact.Punjabi));
public static ref Tag Punjabi => ref ((StaticBox<Tag>)ᏑPunjabi).ValueSlot;
public static readonly ж<Tag> ᏑPolish = new StaticBox<Tag>(((Tag)compact.Polish));
public static ref Tag Polish => ref ((StaticBox<Tag>)ᏑPolish).ValueSlot;
public static readonly ж<Tag> ᏑPortuguese = new StaticBox<Tag>(((Tag)compact.Portuguese));
public static ref Tag Portuguese => ref ((StaticBox<Tag>)ᏑPortuguese).ValueSlot;
public static readonly ж<Tag> ᏑBrazilianPortuguese = new StaticBox<Tag>(((Tag)compact.BrazilianPortuguese));
public static ref Tag BrazilianPortuguese => ref ((StaticBox<Tag>)ᏑBrazilianPortuguese).ValueSlot;
public static readonly ж<Tag> ᏑEuropeanPortuguese = new StaticBox<Tag>(((Tag)compact.EuropeanPortuguese));
public static ref Tag EuropeanPortuguese => ref ((StaticBox<Tag>)ᏑEuropeanPortuguese).ValueSlot;
public static readonly ж<Tag> ᏑRomanian = new StaticBox<Tag>(((Tag)compact.Romanian));
public static ref Tag Romanian => ref ((StaticBox<Tag>)ᏑRomanian).ValueSlot;
public static readonly ж<Tag> ᏑRussian = new StaticBox<Tag>(((Tag)compact.Russian));
public static ref Tag Russian => ref ((StaticBox<Tag>)ᏑRussian).ValueSlot;
public static readonly ж<Tag> ᏑSinhala = new StaticBox<Tag>(((Tag)compact.Sinhala));
public static ref Tag Sinhala => ref ((StaticBox<Tag>)ᏑSinhala).ValueSlot;
public static readonly ж<Tag> ᏑSlovak = new StaticBox<Tag>(((Tag)compact.Slovak));
public static ref Tag Slovak => ref ((StaticBox<Tag>)ᏑSlovak).ValueSlot;
public static readonly ж<Tag> ᏑSlovenian = new StaticBox<Tag>(((Tag)compact.Slovenian));
public static ref Tag Slovenian => ref ((StaticBox<Tag>)ᏑSlovenian).ValueSlot;
public static readonly ж<Tag> ᏑAlbanian = new StaticBox<Tag>(((Tag)compact.Albanian));
public static ref Tag Albanian => ref ((StaticBox<Tag>)ᏑAlbanian).ValueSlot;
public static readonly ж<Tag> ᏑSerbian = new StaticBox<Tag>(((Tag)compact.Serbian));
public static ref Tag Serbian => ref ((StaticBox<Tag>)ᏑSerbian).ValueSlot;
public static readonly ж<Tag> ᏑSerbianLatin = new StaticBox<Tag>(((Tag)compact.SerbianLatin));
public static ref Tag SerbianLatin => ref ((StaticBox<Tag>)ᏑSerbianLatin).ValueSlot;
public static readonly ж<Tag> ᏑSwedish = new StaticBox<Tag>(((Tag)compact.Swedish));
public static ref Tag Swedish => ref ((StaticBox<Tag>)ᏑSwedish).ValueSlot;
public static readonly ж<Tag> ᏑSwahili = new StaticBox<Tag>(((Tag)compact.Swahili));
public static ref Tag Swahili => ref ((StaticBox<Tag>)ᏑSwahili).ValueSlot;
public static readonly ж<Tag> ᏑTamil = new StaticBox<Tag>(((Tag)compact.Tamil));
public static ref Tag Tamil => ref ((StaticBox<Tag>)ᏑTamil).ValueSlot;
public static readonly ж<Tag> ᏑTelugu = new StaticBox<Tag>(((Tag)compact.Telugu));
public static ref Tag Telugu => ref ((StaticBox<Tag>)ᏑTelugu).ValueSlot;
public static readonly ж<Tag> ᏑThai = new StaticBox<Tag>(((Tag)compact.Thai));
public static ref Tag Thai => ref ((StaticBox<Tag>)ᏑThai).ValueSlot;
public static readonly ж<Tag> ᏑTurkish = new StaticBox<Tag>(((Tag)compact.Turkish));
public static ref Tag Turkish => ref ((StaticBox<Tag>)ᏑTurkish).ValueSlot;
public static readonly ж<Tag> ᏑUkrainian = new StaticBox<Tag>(((Tag)compact.Ukrainian));
public static ref Tag Ukrainian => ref ((StaticBox<Tag>)ᏑUkrainian).ValueSlot;
public static readonly ж<Tag> ᏑUrdu = new StaticBox<Tag>(((Tag)compact.Urdu));
public static ref Tag Urdu => ref ((StaticBox<Tag>)ᏑUrdu).ValueSlot;
public static readonly ж<Tag> ᏑUzbek = new StaticBox<Tag>(((Tag)compact.Uzbek));
public static ref Tag Uzbek => ref ((StaticBox<Tag>)ᏑUzbek).ValueSlot;
public static readonly ж<Tag> ᏑVietnamese = new StaticBox<Tag>(((Tag)compact.Vietnamese));
public static ref Tag Vietnamese => ref ((StaticBox<Tag>)ᏑVietnamese).ValueSlot;
public static readonly ж<Tag> ᏑChinese = new StaticBox<Tag>(((Tag)compact.Chinese));
public static ref Tag Chinese => ref ((StaticBox<Tag>)ᏑChinese).ValueSlot;
public static readonly ж<Tag> ᏑSimplifiedChinese = new StaticBox<Tag>(((Tag)compact.SimplifiedChinese));
public static ref Tag SimplifiedChinese => ref ((StaticBox<Tag>)ᏑSimplifiedChinese).ValueSlot;
public static readonly ж<Tag> ᏑTraditionalChinese = new StaticBox<Tag>(((Tag)compact.TraditionalChinese));
public static ref Tag TraditionalChinese => ref ((StaticBox<Tag>)ᏑTraditionalChinese).ValueSlot;
public static readonly ж<Tag> ᏑZulu = new StaticBox<Tag>(((Tag)compact.Zulu));
public static ref Tag Zulu => ref ((StaticBox<Tag>)ᏑZulu).ValueSlot;

} // end language_package
