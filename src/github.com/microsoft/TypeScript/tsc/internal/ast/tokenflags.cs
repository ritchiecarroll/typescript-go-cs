namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ast_package {

partial struct TokenFlags /*num:int32*/;

public static TokenFlags TokenFlagsNone => 0;
public static TokenFlags TokenFlagsPrecedingLineBreak => /* 1 << 0 */ 1;
public static TokenFlags TokenFlagsPrecedingJSDocComment => /* 1 << 1 */ 2;
public static TokenFlags TokenFlagsUnterminated => /* 1 << 2 */ 4;
public static TokenFlags TokenFlagsExtendedUnicodeEscape => /* 1 << 3 */ 8;
public static TokenFlags TokenFlagsScientific => /* 1 << 4 */ 16;
public static TokenFlags TokenFlagsOctal => /* 1 << 5 */ 32;
public static TokenFlags TokenFlagsHexSpecifier => /* 1 << 6 */ 64;
public static TokenFlags TokenFlagsBinarySpecifier => /* 1 << 7 */ 128;
public static TokenFlags TokenFlagsOctalSpecifier => /* 1 << 8 */ 256;
public static TokenFlags TokenFlagsContainsSeparator => /* 1 << 9 */ 512;
public static TokenFlags TokenFlagsUnicodeEscape => /* 1 << 10 */ 1024;
public static TokenFlags TokenFlagsContainsInvalidEscape => /* 1 << 11 */ 2048;
public static TokenFlags TokenFlagsHexEscape => /* 1 << 12 */ 4096;
public static TokenFlags TokenFlagsContainsLeadingZero => /* 1 << 13 */ 8192;
public static TokenFlags TokenFlagsContainsInvalidSeparator => /* 1 << 14 */ 16384;
public static TokenFlags TokenFlagsPrecedingJSDocLeadingAsterisks => /* 1 << 15 */ 32768;
public static TokenFlags TokenFlagsSingleQuote => /* 1 << 16 */ 65536;
public static TokenFlags TokenFlagsPrecedingJSDocWithDeprecated => /* 1 << 17 */ 131072;
public static TokenFlags TokenFlagsPrecedingJSDocWithSeeOrLink => /* 1 << 18 */ 262144;
public static TokenFlags TokenFlagsBinaryOrOctalSpecifier => /* TokenFlagsBinarySpecifier | TokenFlagsOctalSpecifier */ 384;
public static TokenFlags TokenFlagsWithSpecifier => /* TokenFlagsHexSpecifier | TokenFlagsBinaryOrOctalSpecifier */ 448;
public static TokenFlags TokenFlagsStringLiteralFlags => /* TokenFlagsUnterminated | TokenFlagsHexEscape | TokenFlagsUnicodeEscape | TokenFlagsExtendedUnicodeEscape | TokenFlagsContainsInvalidEscape | TokenFlagsSingleQuote */ 72716;
public static TokenFlags TokenFlagsNumericLiteralFlags => /* TokenFlagsScientific | TokenFlagsOctal | TokenFlagsContainsLeadingZero | TokenFlagsWithSpecifier | TokenFlagsContainsSeparator | TokenFlagsContainsInvalidSeparator */ 25584;
public static TokenFlags TokenFlagsTemplateLiteralLikeFlags => /* TokenFlagsUnterminated | TokenFlagsHexEscape | TokenFlagsUnicodeEscape | TokenFlagsExtendedUnicodeEscape | TokenFlagsContainsInvalidEscape */ 7180;
public static TokenFlags TokenFlagsRegularExpressionLiteralFlags => /* TokenFlagsUnterminated */ 4;
public static TokenFlags TokenFlagsIsInvalid => /* TokenFlagsOctal | TokenFlagsContainsLeadingZero | TokenFlagsContainsInvalidSeparator | TokenFlagsContainsInvalidEscape */ 26656;

} // end ast_package
