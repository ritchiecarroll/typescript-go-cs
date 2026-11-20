namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class nodebuilder_package {

partial interface SymbolTracker {
    bool TrackSymbol(ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning);
    void ReportInaccessibleThisError();
    void ReportPrivateInBaseOfClassExpression(@string propertyName);
    void ReportInaccessibleUniqueSymbolError();
    void ReportCyclicStructureError();
    void ReportLikelyUnsafeImportRequiredError(@string specifier, @string symbolName);
    void ReportTruncationError();
    void ReportNonlocalAugmentation(ж<ast.SourceFile> containingFile, ж<ast.Symbol> parentSymbol, ж<ast.Symbol> augmentingSymbol);
    void ReportNonSerializableProperty(@string propertyName);
    void ReportInferenceFallback(ж<ast.Node> node);
    void PushErrorFallbackNode(ж<ast.Node> node);
    void PopErrorFallbackNode();
}

partial struct Flags /*num:uint32*/;

public static Flags FlagsNone => 0;
public static Flags FlagsNoTruncation => /* 1 << 0 */ 1;
public static Flags FlagsWriteArrayAsGenericType => /* 1 << 1 */ 2;
public static Flags FlagsGenerateNamesForShadowedTypeParams => /* 1 << 2 */ 4;
public static Flags FlagsUseStructuralFallback => /* 1 << 3 */ 8;
public static Flags FlagsForbidIndexedAccessSymbolReferences => /* 1 << 4 */ 16;
public static Flags FlagsWriteTypeArgumentsOfSignature => /* 1 << 5 */ 32;
public static Flags FlagsUseFullyQualifiedType => /* 1 << 6 */ 64;
public static Flags FlagsUseOnlyExternalAliasing => /* 1 << 7 */ 128;
public static Flags FlagsSuppressAnyReturnType => /* 1 << 8 */ 256;
public static Flags FlagsWriteTypeParametersInQualifiedName => /* 1 << 9 */ 512;
public static Flags FlagsMultilineObjectLiterals => /* 1 << 10 */ 1024;
public static Flags FlagsWriteClassExpressionAsTypeLiteral => /* 1 << 11 */ 2048;
public static Flags FlagsUseTypeOfFunction => /* 1 << 12 */ 4096;
public static Flags FlagsOmitParameterModifiers => /* 1 << 13 */ 8192;
public static Flags FlagsUseAliasDefinedOutsideCurrentScope => /* 1 << 14 */ 16384;
public static Flags FlagsUseSingleQuotesForStringLiteralType => /* 1 << 28 */ 268435456;
public static Flags FlagsNoTypeReduction => /* 1 << 29 */ 536870912;
public static Flags FlagsUseInstantiationExpressions => /* 1 << 30 */ 1073741824;
public static Flags FlagsOmitThisParameter => /* 1 << 25 */ 33554432;
public static Flags FlagsWriteCallStyleSignature => /* 1 << 27 */ 134217728;
public static Flags FlagsAllowThisInObjectLiteral => /* 1 << 15 */ 32768;
public static Flags FlagsAllowQualifiedNameInPlaceOfIdentifier => /* 1 << 16 */ 65536;
public static Flags FlagsAllowAnonymousIdentifier => /* 1 << 17 */ 131072;
public static Flags FlagsAllowEmptyUnionOrIntersection => /* 1 << 18 */ 262144;
public static Flags FlagsAllowEmptyTuple => /* 1 << 19 */ 524288;
public static Flags FlagsAllowUniqueESSymbolType => /* 1 << 20 */ 1048576;
public static Flags FlagsAllowEmptyIndexInfoType => /* 1 << 21 */ 2097152;
public static Flags FlagsAllowNodeModulesRelativePaths => /* 1 << 26 */ 67108864;
public static Flags FlagsIgnoreErrors => /* FlagsAllowThisInObjectLiteral | FlagsAllowQualifiedNameInPlaceOfIdentifier | FlagsAllowAnonymousIdentifier | FlagsAllowEmptyUnionOrIntersection | FlagsAllowEmptyTuple | FlagsAllowEmptyIndexInfoType | FlagsAllowNodeModulesRelativePaths */ 70221824;
public static Flags FlagsInObjectTypeLiteral => /* 1 << 22 */ 4194304;
public static Flags FlagsInTypeAlias => /* 1 << 23 */ 8388608;
public static Flags FlagsInInitialEntityName => /* 1 << 24 */ 16777216;

partial struct InternalFlags /*num:int32*/;

public static InternalFlags InternalFlagsNone => 0;
public static InternalFlags InternalFlagsWriteComputedProps => /* 1 << 0 */ 1;
public static InternalFlags InternalFlagsNoSyntacticPrinter => /* 1 << 1 */ 2;
public static InternalFlags InternalFlagsDoNotIncludeSymbolChain => /* 1 << 2 */ 4;
public static InternalFlags InternalFlagsAllowUnresolvedNames => /* 1 << 3 */ 8;

} // end nodebuilder_package
