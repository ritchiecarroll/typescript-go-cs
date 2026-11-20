namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ast_package {

partial struct NodeFlags /*num:uint32*/;

public static NodeFlags NodeFlagsNone => 0;
public static NodeFlags NodeFlagsLet => /* 1 << 0 */ 1;
public static NodeFlags NodeFlagsConst => /* 1 << 1 */ 2;
public static NodeFlags NodeFlagsUsing => /* 1 << 2 */ 4;
public static NodeFlags NodeFlagsReparsed => /* 1 << 3 */ 8;
public static NodeFlags NodeFlagsSynthesized => /* 1 << 4 */ 16;
public static NodeFlags NodeFlagsOptionalChain => /* 1 << 5 */ 32;
public static NodeFlags NodeFlagsExportContext => /* 1 << 6 */ 64;
public static NodeFlags NodeFlagsContainsThis => /* 1 << 7 */ 128;
public static NodeFlags NodeFlagsHasImplicitReturn => /* 1 << 8 */ 256;
public static NodeFlags NodeFlagsHasExplicitReturn => /* 1 << 9 */ 512;
public static NodeFlags NodeFlagsDisallowInContext => /* 1 << 10 */ 1024;
public static NodeFlags NodeFlagsYieldContext => /* 1 << 11 */ 2048;
public static NodeFlags NodeFlagsDecoratorContext => /* 1 << 12 */ 4096;
public static NodeFlags NodeFlagsAwaitContext => /* 1 << 13 */ 8192;
public static NodeFlags NodeFlagsDisallowConditionalTypesContext => /* 1 << 14 */ 16384;
public static NodeFlags NodeFlagsThisNodeHasError => /* 1 << 15 */ 32768;
public static NodeFlags NodeFlagsJavaScriptFile => /* 1 << 16 */ 65536;
public static NodeFlags NodeFlagsThisNodeOrAnySubNodesHasError => /* 1 << 17 */ 131072;
public static NodeFlags NodeFlagsHasAsyncFunctions => /* 1 << 18 */ 262144;
public static NodeFlags NodeFlagsPossiblyContainsDynamicImport => /* 1 << 19 */ 524288;
public static NodeFlags NodeFlagsPossiblyContainsImportMeta => /* 1 << 20 */ 1048576;
public static NodeFlags NodeFlagsHasJSDoc => /* 1 << 21 */ 2097152;
public static NodeFlags NodeFlagsJSDoc => /* 1 << 22 */ 4194304;
public static NodeFlags NodeFlagsAmbient => /* 1 << 23 */ 8388608;
public static NodeFlags NodeFlagsInWithStatement => /* 1 << 24 */ 16777216;
public static NodeFlags NodeFlagsJsonFile => /* 1 << 25 */ 33554432;
public static NodeFlags NodeFlagsPossiblyContainsDeprecatedTag => /* 1 << 26 */ 67108864;
public static NodeFlags NodeFlagsUnreachable => /* 1 << 27 */ 134217728;
public static NodeFlags NodeFlagsReparserTransformedLiteral => /* 1 << 28 */ 268435456;
public static NodeFlags NodeFlagsBlockScoped => /* NodeFlagsLet | NodeFlagsConst | NodeFlagsUsing */ 7;
public static NodeFlags NodeFlagsConstant => /* NodeFlagsConst | NodeFlagsUsing */ 6;
public static NodeFlags NodeFlagsAwaitUsing => /* NodeFlagsConst | NodeFlagsUsing */ 6;
public static NodeFlags NodeFlagsReachabilityCheckFlags => /* NodeFlagsHasImplicitReturn | NodeFlagsHasExplicitReturn */ 768;
public static NodeFlags NodeFlagsReachabilityAndEmitFlags => /* NodeFlagsReachabilityCheckFlags | NodeFlagsHasAsyncFunctions */ 262912;
public static NodeFlags NodeFlagsContextFlags => /* NodeFlagsDisallowInContext | NodeFlagsDisallowConditionalTypesContext | NodeFlagsYieldContext | NodeFlagsDecoratorContext | NodeFlagsAwaitContext | NodeFlagsJavaScriptFile | NodeFlagsInWithStatement | NodeFlagsAmbient */ 25263104;
public static NodeFlags NodeFlagsTypeExcludesFlags => /* NodeFlagsYieldContext | NodeFlagsAwaitContext */ 10240;
public static NodeFlags NodeFlagsPermanentlySetIncrementalFlags => /* NodeFlagsPossiblyContainsDynamicImport | NodeFlagsPossiblyContainsImportMeta */ 1572864;
public static NodeFlags NodeFlagsIdentifierHasExtendedUnicodeEscape => /* NodeFlagsContainsThis */ 128;
public static NodeFlags NodeFlagsIdentifierIsInJSDocNamespace => /* NodeFlagsHasAsyncFunctions */ 262144;
public static NodeFlags NodeFlagsNestedNamespace => /* NodeFlagsOptionalChain */ 32;

} // end ast_package
