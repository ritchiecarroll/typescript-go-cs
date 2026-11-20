namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ast_package {

partial struct CheckFlags /*num:uint32*/;

public static CheckFlags CheckFlagsNone => 0;
public static CheckFlags CheckFlagsInstantiated => /* 1 << 0 */ 1;
public static CheckFlags CheckFlagsSyntheticProperty => /* 1 << 1 */ 2;
public static CheckFlags CheckFlagsSyntheticMethod => /* 1 << 2 */ 4;
public static CheckFlags CheckFlagsReadonly => /* 1 << 3 */ 8;
public static CheckFlags CheckFlagsReadPartial => /* 1 << 4 */ 16;
public static CheckFlags CheckFlagsWritePartial => /* 1 << 5 */ 32;
public static CheckFlags CheckFlagsHasNonUniformType => /* 1 << 6 */ 64;
public static CheckFlags CheckFlagsHasLiteralType => /* 1 << 7 */ 128;
public static CheckFlags CheckFlagsContainsPublic => /* 1 << 8 */ 256;
public static CheckFlags CheckFlagsContainsProtected => /* 1 << 9 */ 512;
public static CheckFlags CheckFlagsContainsPrivate => /* 1 << 10 */ 1024;
public static CheckFlags CheckFlagsContainsWritePublic => /* 1 << 11 */ 2048;
public static CheckFlags CheckFlagsContainsWriteProtected => /* 1 << 12 */ 4096;
public static CheckFlags CheckFlagsContainsWritePrivate => /* 1 << 13 */ 8192;
public static CheckFlags CheckFlagsContainsStatic => /* 1 << 14 */ 16384;
public static CheckFlags CheckFlagsLate => /* 1 << 15 */ 32768;
public static CheckFlags CheckFlagsReverseMapped => /* 1 << 16 */ 65536;
public static CheckFlags CheckFlagsOptionalParameter => /* 1 << 17 */ 131072;
public static CheckFlags CheckFlagsRestParameter => /* 1 << 18 */ 262144;
public static CheckFlags CheckFlagsDeferredType => /* 1 << 19 */ 524288;
public static CheckFlags CheckFlagsHasNeverType => /* 1 << 20 */ 1048576;
public static CheckFlags CheckFlagsMapped => /* 1 << 21 */ 2097152;
public static CheckFlags CheckFlagsStripOptional => /* 1 << 22 */ 4194304;
public static CheckFlags CheckFlagsUnresolved => /* 1 << 23 */ 8388608;
public static CheckFlags CheckFlagsIsDiscriminantComputed => /* 1 << 24 */ 16777216;
public static CheckFlags CheckFlagsIsDiscriminant => /* 1 << 25 */ 33554432;
public static CheckFlags CheckFlagsIndexSymbol => /* 1 << 26 */ 67108864;
public static CheckFlags CheckFlagsSynthetic => /* CheckFlagsSyntheticProperty | CheckFlagsSyntheticMethod */ 6;
public static CheckFlags CheckFlagsNonUniformAndLiteral => /* CheckFlagsHasNonUniformType | CheckFlagsHasLiteralType */ 192;
public static CheckFlags CheckFlagsPartial => /* CheckFlagsReadPartial | CheckFlagsWritePartial */ 48;

} // end ast_package
