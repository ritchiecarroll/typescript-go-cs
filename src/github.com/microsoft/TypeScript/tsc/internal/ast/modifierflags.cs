namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ast_package {

partial struct ModifierFlags /*num:uint32*/;

public static ModifierFlags ModifierFlagsNone => 0;
public static ModifierFlags ModifierFlagsPublic => /* 1 << 0 */ 1;
public static ModifierFlags ModifierFlagsPrivate => /* 1 << 1 */ 2;
public static ModifierFlags ModifierFlagsProtected => /* 1 << 2 */ 4;
public static ModifierFlags ModifierFlagsReadonly => /* 1 << 3 */ 8;
public static ModifierFlags ModifierFlagsOverride => /* 1 << 4 */ 16;
public static ModifierFlags ModifierFlagsExport => /* 1 << 5 */ 32;
public static ModifierFlags ModifierFlagsAbstract => /* 1 << 6 */ 64;
public static ModifierFlags ModifierFlagsAmbient => /* 1 << 7 */ 128;
public static ModifierFlags ModifierFlagsStatic => /* 1 << 8 */ 256;
public static ModifierFlags ModifierFlagsAccessor => /* 1 << 9 */ 512;
public static ModifierFlags ModifierFlagsAsync => /* 1 << 10 */ 1024;
public static ModifierFlags ModifierFlagsDefault => /* 1 << 11 */ 2048;
public static ModifierFlags ModifierFlagsConst => /* 1 << 12 */ 4096;
public static ModifierFlags ModifierFlagsIn => /* 1 << 13 */ 8192;
public static ModifierFlags ModifierFlagsOut => /* 1 << 14 */ 16384;
public static ModifierFlags ModifierFlagsDecorator => /* 1 << 15 */ 32768;
public static ModifierFlags ModifierFlagsDeprecated => /* 1 << 16 */ 65536;
public static ModifierFlags ModifierFlagsJSDocPublic => /* 1 << 23 */ 8388608;
public static ModifierFlags ModifierFlagsJSDocPrivate => /* 1 << 24 */ 16777216;
public static ModifierFlags ModifierFlagsJSDocProtected => /* 1 << 25 */ 33554432;
public static ModifierFlags ModifierFlagsJSDocReadonly => /* 1 << 26 */ 67108864;
public static ModifierFlags ModifierFlagsJSDocOverride => /* 1 << 27 */ 134217728;
public static ModifierFlags ModifierFlagsHasComputedJSDocModifiers => /* 1 << 28 */ 268435456;
public static ModifierFlags ModifierFlagsHasComputedFlags => /* 1 << 29 */ 536870912;
public static ModifierFlags ModifierFlagsSyntacticOrJSDocModifiers => /* ModifierFlagsPublic | ModifierFlagsPrivate | ModifierFlagsProtected | ModifierFlagsReadonly | ModifierFlagsOverride */ 31;
public static ModifierFlags ModifierFlagsSyntacticOnlyModifiers => /* ModifierFlagsExport | ModifierFlagsAmbient | ModifierFlagsAbstract | ModifierFlagsStatic | ModifierFlagsAccessor | ModifierFlagsAsync | ModifierFlagsDefault | ModifierFlagsConst | ModifierFlagsIn | ModifierFlagsOut | ModifierFlagsDecorator */ 65504;
public static ModifierFlags ModifierFlagsSyntacticModifiers => /* ModifierFlagsSyntacticOrJSDocModifiers | ModifierFlagsSyntacticOnlyModifiers */ 65535;
public static ModifierFlags ModifierFlagsJSDocCacheOnlyModifiers => /* ModifierFlagsJSDocPublic | ModifierFlagsJSDocPrivate | ModifierFlagsJSDocProtected | ModifierFlagsJSDocReadonly | ModifierFlagsJSDocOverride */ 260046848;
public static ModifierFlags ModifierFlagsJSDocOnlyModifiers => /* ModifierFlagsDeprecated */ 65536;
public static ModifierFlags ModifierFlagsNonCacheOnlyModifiers => /* ModifierFlagsSyntacticOrJSDocModifiers | ModifierFlagsSyntacticOnlyModifiers | ModifierFlagsJSDocOnlyModifiers */ 131071;
public static ModifierFlags ModifierFlagsAccessibilityModifier => /* ModifierFlagsPublic | ModifierFlagsPrivate | ModifierFlagsProtected */ 7;
public static ModifierFlags ModifierFlagsParameterPropertyModifier => /* ModifierFlagsAccessibilityModifier | ModifierFlagsReadonly | ModifierFlagsOverride */ 31;
public static ModifierFlags ModifierFlagsNonPublicAccessibilityModifier => /* ModifierFlagsPrivate | ModifierFlagsProtected */ 6;
public static ModifierFlags ModifierFlagsTypeScriptModifier => /* ModifierFlagsAmbient | ModifierFlagsPublic | ModifierFlagsPrivate | ModifierFlagsProtected | ModifierFlagsReadonly | ModifierFlagsAbstract | ModifierFlagsConst | ModifierFlagsOverride | ModifierFlagsIn | ModifierFlagsOut */ 28895;
public static ModifierFlags ModifierFlagsExportDefault => /* ModifierFlagsExport | ModifierFlagsDefault */ 2080;
public static ModifierFlags ModifierFlagsAll => /* ModifierFlagsExport | ModifierFlagsAmbient | ModifierFlagsPublic | ModifierFlagsPrivate | ModifierFlagsProtected | ModifierFlagsStatic | ModifierFlagsReadonly | ModifierFlagsAbstract | ModifierFlagsAccessor | ModifierFlagsAsync | ModifierFlagsDefault | ModifierFlagsConst | ModifierFlagsDeprecated | ModifierFlagsOverride | ModifierFlagsIn | ModifierFlagsOut | ModifierFlagsDecorator */ 131071;
public static ModifierFlags ModifierFlagsModifier => /* ModifierFlagsAll & ^ModifierFlagsDecorator */ 98303;
public static ModifierFlags ModifierFlagsJavaScript => /* ModifierFlagsExport | ModifierFlagsStatic | ModifierFlagsAccessor | ModifierFlagsAsync | ModifierFlagsDefault */ 3872;

} // end ast_package
