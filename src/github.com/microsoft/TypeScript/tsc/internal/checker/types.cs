global using StringLiteralType = go.github.com.microsoft.TypeScript.tsc.@internal.checker_package.Type;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using bits = go.math.bits_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.math;
using static checker_package;

partial class checker_package {

partial struct ParseFlags /*num:uint32*/;

public static ParseFlags ParseFlagsNone => 0;
public static ParseFlags ParseFlagsYield => /* 1 << 0 */ 1;
public static ParseFlags ParseFlagsAwait => /* 1 << 1 */ 2;
public static ParseFlags ParseFlagsType => /* 1 << 2 */ 4;
public static ParseFlags ParseFlagsIgnoreMissingOpenBrace => /* 1 << 4 */ 16;
public static ParseFlags ParseFlagsJSDoc => /* 1 << 5 */ 32;

partial struct SignatureKind /*num:int32*/;

public static SignatureKind SignatureKindCall => /* iota */ 0;
public static SignatureKind SignatureKindConstruct => 1;

partial struct IndexKind /*num:int32*/;

public static IndexKind IndexKindString => /* iota */ 0;
public static IndexKind IndexKindNumber => 1;

partial struct MemberOverrideStatus /*num:int32*/;

public static MemberOverrideStatus MemberOverrideStatusNone => /* iota */ 0;
public static MemberOverrideStatus MemberOverrideStatusNeedsOverride => 1;
public static MemberOverrideStatus MemberOverrideStatusHasInvalidOverride => 2;

partial struct ContextFlags /*num:uint32*/;

public static ContextFlags ContextFlagsNone => 0;
public static ContextFlags ContextFlagsSignature => /* 1 << 0 */ 1;
public static ContextFlags ContextFlagsNoConstraints => /* 1 << 1 */ 2;
public static ContextFlags ContextFlagsIgnoreNodeInferences => /* 1 << 2 */ 4;
public static ContextFlags ContextFlagsSkipBindingPatterns => /* 1 << 3 */ 8;

partial struct TypeFormatFlags /*num:uint32*/;

public static TypeFormatFlags TypeFormatFlagsNone => 0;
public static TypeFormatFlags TypeFormatFlagsNoTruncation => /* 1 << 0 */ 1;
public static TypeFormatFlags TypeFormatFlagsWriteArrayAsGenericType => /* 1 << 1 */ 2;
public static TypeFormatFlags TypeFormatFlagsGenerateNamesForShadowedTypeParams => /* 1 << 2 */ 4;
public static TypeFormatFlags TypeFormatFlagsUseStructuralFallback => /* 1 << 3 */ 8;
public static TypeFormatFlags TypeFormatFlagsWriteTypeArgumentsOfSignature => /* 1 << 5 */ 32;
public static TypeFormatFlags TypeFormatFlagsUseFullyQualifiedType => /* 1 << 6 */ 64;
public static TypeFormatFlags TypeFormatFlagsSuppressAnyReturnType => /* 1 << 8 */ 256;
public static TypeFormatFlags TypeFormatFlagsMultilineObjectLiterals => /* 1 << 10 */ 1024;
public static TypeFormatFlags TypeFormatFlagsWriteClassExpressionAsTypeLiteral => /* 1 << 11 */ 2048;
public static TypeFormatFlags TypeFormatFlagsUseTypeOfFunction => /* 1 << 12 */ 4096;
public static TypeFormatFlags TypeFormatFlagsOmitParameterModifiers => /* 1 << 13 */ 8192;
public static TypeFormatFlags TypeFormatFlagsUseAliasDefinedOutsideCurrentScope => /* 1 << 14 */ 16384;
public static TypeFormatFlags TypeFormatFlagsUseSingleQuotesForStringLiteralType => /* 1 << 28 */ 268435456;
public static TypeFormatFlags TypeFormatFlagsNoTypeReduction => /* 1 << 29 */ 536870912;
public static TypeFormatFlags TypeFormatFlagsUseInstantiationExpressions => /* 1 << 30 */ 1073741824;
public static TypeFormatFlags TypeFormatFlagsOmitThisParameter => /* 1 << 25 */ 33554432;
public static TypeFormatFlags TypeFormatFlagsWriteCallStyleSignature => /* 1 << 27 */ 134217728;
public static TypeFormatFlags TypeFormatFlagsAllowUniqueESSymbolType => /* 1 << 20 */ 1048576;
public static TypeFormatFlags TypeFormatFlagsAddUndefined => /* 1 << 17 */ 131072;
public static TypeFormatFlags TypeFormatFlagsWriteArrowStyleSignature => /* 1 << 18 */ 262144;
public static TypeFormatFlags TypeFormatFlagsInArrayType => /* 1 << 19 */ 524288;
public static TypeFormatFlags TypeFormatFlagsInElementType => /* 1 << 21 */ 2097152;
public static TypeFormatFlags TypeFormatFlagsInFirstTypeArgument => /* 1 << 22 */ 4194304;
public static TypeFormatFlags TypeFormatFlagsInTypeAlias => /* 1 << 23 */ 8388608;
public static TypeFormatFlags TypeFormatFlagsNodeBuilderFlagsMask => /* TypeFormatFlagsNoTruncation | TypeFormatFlagsWriteArrayAsGenericType | TypeFormatFlagsGenerateNamesForShadowedTypeParams | TypeFormatFlagsUseStructuralFallback | TypeFormatFlagsWriteTypeArgumentsOfSignature |
	TypeFormatFlagsUseFullyQualifiedType | TypeFormatFlagsSuppressAnyReturnType | TypeFormatFlagsMultilineObjectLiterals | TypeFormatFlagsWriteClassExpressionAsTypeLiteral |
	TypeFormatFlagsUseTypeOfFunction | TypeFormatFlagsOmitParameterModifiers | TypeFormatFlagsUseAliasDefinedOutsideCurrentScope | TypeFormatFlagsAllowUniqueESSymbolType | TypeFormatFlagsInTypeAlias |
	TypeFormatFlagsUseInstantiationExpressions | TypeFormatFlagsUseSingleQuotesForStringLiteralType | TypeFormatFlagsNoTypeReduction | TypeFormatFlagsOmitThisParameter */ 1922071919;

partial struct SymbolFormatFlags /*num:uint32*/;

public static SymbolFormatFlags SymbolFormatFlagsNone => 0;
public static SymbolFormatFlags SymbolFormatFlagsWriteTypeParametersOrArguments => /* 1 << 0 */ 1;
public static SymbolFormatFlags SymbolFormatFlagsUseOnlyExternalAliasing => /* 1 << 1 */ 2;
public static SymbolFormatFlags SymbolFormatFlagsAllowAnyNodeKind => /* 1 << 2 */ 4;
public static SymbolFormatFlags SymbolFormatFlagsUseAliasDefinedOutsideCurrentScope => /* 1 << 3 */ 8;
public static SymbolFormatFlags SymbolFormatFlagsWriteComputedProps => /* 1 << 4 */ 16;
public static SymbolFormatFlags SymbolFormatFlagsDoNotIncludeSymbolChain => /* 1 << 5 */ 32;

partial struct ExternalEmitHelpers /*num:uint32*/;

public static ExternalEmitHelpers ExternalEmitHelpersRest => /* 1 << iota */ 1;
public static ExternalEmitHelpers ExternalEmitHelpersDecorate => 2;
public static ExternalEmitHelpers ExternalEmitHelpersMetadata => 4;
public static ExternalEmitHelpers ExternalEmitHelpersParam => 8;
public static ExternalEmitHelpers ExternalEmitHelpersAwaiter => 16;
public static ExternalEmitHelpers ExternalEmitHelpersAwait => 32;
public static ExternalEmitHelpers ExternalEmitHelpersAsyncGenerator => 64;
public static ExternalEmitHelpers ExternalEmitHelpersAsyncDelegator => 128;
public static ExternalEmitHelpers ExternalEmitHelpersAsyncValues => 256;
public static ExternalEmitHelpers ExternalEmitHelpersExportStar => 512;
public static ExternalEmitHelpers ExternalEmitHelpersImportStar => 1024;
public static ExternalEmitHelpers ExternalEmitHelpersImportDefault => 2048;
public static ExternalEmitHelpers ExternalEmitHelpersMakeTemplateObject => 4096;
public static ExternalEmitHelpers ExternalEmitHelpersClassPrivateFieldGet => 8192;
public static ExternalEmitHelpers ExternalEmitHelpersClassPrivateFieldSet => 16384;
public static ExternalEmitHelpers ExternalEmitHelpersClassPrivateFieldIn => 32768;
public static ExternalEmitHelpers ExternalEmitHelpersSetFunctionName => 65536;
public static ExternalEmitHelpers ExternalEmitHelpersPropKey => 131072;
public static ExternalEmitHelpers ExternalEmitHelpersAddDisposableResourceAndDisposeResources => 262144;
public static ExternalEmitHelpers ExternalEmitHelpersRewriteRelativeImportExtension => 524288;
public static ExternalEmitHelpers ExternalEmitHelpersESDecorateAndRunInitializers => /* ExternalEmitHelpersDecorate */ 2;
public static ExternalEmitHelpers ExternalEmitHelpersFirstEmitHelper => /* ExternalEmitHelpersRest */ 1;
public static ExternalEmitHelpers ExternalEmitHelpersLastEmitHelper => /* ExternalEmitHelpersRewriteRelativeImportExtension */ 524288;
public static ExternalEmitHelpers ExternalEmitHelpersForAwaitOfIncludes => /* ExternalEmitHelpersAsyncValues */ 256;
public static ExternalEmitHelpers ExternalEmitHelpersAsyncGeneratorIncludes => /* ExternalEmitHelpersAwait | ExternalEmitHelpersAsyncGenerator */ 96;
public static ExternalEmitHelpers ExternalEmitHelpersAsyncDelegatorIncludes => /* ExternalEmitHelpersAwait | ExternalEmitHelpersAsyncDelegator | ExternalEmitHelpersAsyncValues */ 416;

internal static readonly @string externalHelpersModuleNameText = "tslib"u8;

partial struct TypeId /*num:uint32*/;

partial struct SignatureId /*num:uint32*/;

partial struct SymbolReferenceLinks {
    internal ast.SymbolFlags referenceKinds;
}

partial struct ValueSymbolLinks {
    internal ж<Type> resolvedType;
    internal ж<Type> writeType;
    internal ж<ast.Symbol> target;
    internal ж<TypeMapper> mapper;
    internal ж<Type> nameType;
    internal ж<Type> containingType;
    internal bool functionOrConstructorChecked;
}

partial struct MappedSymbolLinks {
    internal ж<Type> keyType;
    internal ж<ast.Symbol> syntheticOrigin;
}

partial struct DeferredSymbolLinks {
    internal ж<Type> parent;
    internal slice<ж<Type>> constituents;
    internal slice<ж<Type>> writeConstituents;
}

partial struct AliasSymbolLinks {
    internal ж<ast.Symbol> immediateTarget;
    internal ж<ast.Symbol> aliasTarget;
    internal bool referenced;
    internal ж<ast.Node> typeOnlyDeclaration;
}

partial struct ModuleSymbolLinks {
    internal ast.SymbolTable resolvedExports;
    internal map<@string, ж<ast.Node>> typeOnlyExportStarMap;
    internal bool exportsChecked;
}

partial struct ReverseMappedSymbolLinks {
    internal ж<Type> propertyType;
    internal ж<Type> mappedType;
    internal ж<Type> constraintType;
}

partial struct LateBoundLinks {
    internal ж<ast.Symbol> lateSymbol;
}

partial struct ExportTypeLinks {
    internal ж<ast.Symbol> target;
    internal ж<ast.Node> originatingImport;
}

partial struct TypeAliasLinks {
    internal ж<Type> declaredType;
    internal slice<ж<Type>> typeParameters;
    internal map<CacheHashKey, ж<Type>> instantiations;
    internal bool isConstructorDeclaredProperty;
}

partial struct DeclaredTypeLinks {
    internal ж<Type> declaredType;
    internal bool interfaceChecked;
    internal bool indexSignaturesChecked;
    internal bool typeParametersChecked;
    internal bool enumChecked;
}

partial struct ExhaustiveState /*num:byte*/;

public static ExhaustiveState ExhaustiveStateUnknown => /* iota */ 0;
public static ExhaustiveState ExhaustiveStateComputing => 1;
public static ExhaustiveState ExhaustiveStateFalse => 2;
public static ExhaustiveState ExhaustiveStateTrue => 3;

partial struct SwitchStatementLinks {
    internal ExhaustiveState exhaustiveState;
    internal bool switchTypesComputed;
    internal bool witnessesComputed;
    internal slice<ж<Type>> switchTypes;
    internal slice<@string> witnesses;
}

partial struct ArrayLiteralLinks {
    internal bool indicesComputed;
    internal nint firstSpreadIndex;
    internal nint lastSpreadIndex;
}

partial struct MembersOrExportsResolutionKind /*num:nint*/;

public static MembersOrExportsResolutionKind MembersOrExportsResolutionKindResolvedExports => 0;
public static MembersOrExportsResolutionKind MembersOrExportsResolutionKindResolvedMembers => 1;

partial struct MembersAndExportsLinks /*[2]global::go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SymbolTable*/;

partial struct SpreadLinks {
    internal ж<ast.Symbol> leftSpread;
    internal ж<ast.Symbol> rightSpread;
}

partial struct VarianceLinks {
    internal slice<VarianceFlags> variances;
}

partial struct VarianceFlags /*num:uint32*/;

public static VarianceFlags VarianceFlagsInvariant => 0;
public static VarianceFlags VarianceFlagsCovariant => /* 1 << 0 */ 1;
public static VarianceFlags VarianceFlagsContravariant => /* 1 << 1 */ 2;
public static VarianceFlags VarianceFlagsBivariant => /* VarianceFlagsCovariant | VarianceFlagsContravariant */ 3;
public static VarianceFlags VarianceFlagsIndependent => /* 1 << 2 */ 4;
public static VarianceFlags VarianceFlagsVarianceMask => /* VarianceFlagsInvariant | VarianceFlagsCovariant | VarianceFlagsContravariant | VarianceFlagsIndependent */ 7;
public static VarianceFlags VarianceFlagsUnmeasurable => /* 1 << 3 */ 8;
public static VarianceFlags VarianceFlagsUnreliable => /* 1 << 4 */ 16;
public static VarianceFlags VarianceFlagsAllowsStructuralFallback => /* VarianceFlagsUnmeasurable | VarianceFlagsUnreliable */ 24;

partial struct MarkedAssignmentSymbolLinks {
    internal int32 lastAssignmentPos;
    internal bool hasDefiniteAssignment;
}

partial struct accessibleChainCacheKey {
    internal bool useOnlyExternalAliasing;
    internal ж<ast.Node> location;
    internal ast.SymbolFlags meaning;
}

partial struct ContainingSymbolLinks {
    internal map<ast.NodeId, slice<ж<ast.Symbol>>> extendedContainersByFile;
    internal ж<slice<ж<ast.Symbol>>> extendedContainers;
    internal map<accessibleChainCacheKey, slice<ж<ast.Symbol>>> accessibleChainCache;
}

partial struct AccessFlags /*num:uint32*/;

public static AccessFlags AccessFlagsNone => 0;
public static AccessFlags AccessFlagsIncludeUndefined => /* 1 << 0 */ 1;
public static AccessFlags AccessFlagsNoIndexSignatures => /* 1 << 1 */ 2;
public static AccessFlags AccessFlagsWriting => /* 1 << 2 */ 4;
public static AccessFlags AccessFlagsCacheSymbol => /* 1 << 3 */ 8;
public static AccessFlags AccessFlagsAllowMissing => /* 1 << 4 */ 16;
public static AccessFlags AccessFlagsExpressionPosition => /* 1 << 5 */ 32;
public static AccessFlags AccessFlagsReportDeprecated => /* 1 << 6 */ 64;
public static AccessFlags AccessFlagsSuppressNoImplicitAnyError => /* 1 << 7 */ 128;
public static AccessFlags AccessFlagsContextual => /* 1 << 8 */ 256;
public static AccessFlags AccessFlagsPersistent => /* AccessFlagsIncludeUndefined */ 1;

partial struct NodeCheckFlags /*num:uint32*/;

public static NodeCheckFlags NodeCheckFlagsNone => 0;
public static NodeCheckFlags NodeCheckFlagsTypeChecked => /* 1 << 0 */ 1;
public static NodeCheckFlags NodeCheckFlagsContextChecked => /* 1 << 6 */ 64;
public static NodeCheckFlags NodeCheckFlagsEnumValuesComputed => /* 1 << 10 */ 1024;
public static NodeCheckFlags NodeCheckFlagsAssignmentsMarked => /* 1 << 17 */ 131072;
public static NodeCheckFlags NodeCheckFlagsContainsClassWithPrivateIdentifiers => /* 1 << 20 */ 1048576;
public static NodeCheckFlags NodeCheckFlagsContainsSuperPropertyInStaticInitializer => /* 1 << 21 */ 2097152;
public static NodeCheckFlags NodeCheckFlagsInCheckIdentifier => /* 1 << 22 */ 4194304;
public static NodeCheckFlags NodeCheckFlagsInitializerIsUndefined => /* 1 << 24 */ 16777216;
public static NodeCheckFlags NodeCheckFlagsInitializerIsUndefinedComputed => /* 1 << 25 */ 33554432;

partial struct NodeLinks {
    internal NodeCheckFlags flags;
    internal core.Tristate declarationRequiresScopeChange;
    internal bool hasReportedStatementInAmbientContext;
}

partial struct SymbolNodeLinks {
    internal ж<ast.Symbol> resolvedSymbol;
}

partial struct TypeNodeLinks {
    internal ж<Type> resolvedType;
    internal slice<ж<Type>> outerTypeParameters;
}

partial struct ComputedNameNodeLinks {
    internal ж<bool> hasName;
    internal @string name;
}

partial struct EnumMemberLinks {
    internal evaluator.Result value;
}

partial struct AssertionLinks {
    internal ж<Type> exprType;
}

partial struct SourceFileLinks {
    internal bool typeChecked;
    internal bool unusedChecked;
    internal ж<ast.Symbol> externalHelpersModule;
    internal ExternalEmitHelpers requestedExternalEmitHelpers;
    internal collections.OrderedSet<ж<ast.Node>> deferredNodes;
    internal slice<ж<ast.Node>> identifierCheckNodes;
    internal @string localJsxNamespace;
    internal @string localJsxFragmentNamespace;
    internal ж<astꓸEntityName> localJsxFactory;
    internal ж<astꓸEntityName> localJsxFragmentFactory;
    internal ж<Type> jsxFragmentType;
}

partial struct SignatureLinks {
    internal ж<Signature> resolvedSignature;
    internal ж<Signature> effectsSignature;
    internal ж<Signature> decoratorSignature;
}

partial struct TypeFlags /*num:uint32*/;

public static TypeFlags TypeFlagsNone => 0;
public static TypeFlags TypeFlagsAny => /* 1 << 0 */ 1;
public static TypeFlags TypeFlagsUnknown => /* 1 << 1 */ 2;
public static TypeFlags TypeFlagsUndefined => /* 1 << 2 */ 4;
public static TypeFlags TypeFlagsNull => /* 1 << 3 */ 8;
public static TypeFlags TypeFlagsVoid => /* 1 << 4 */ 16;
public static TypeFlags TypeFlagsString => /* 1 << 5 */ 32;
public static TypeFlags TypeFlagsNumber => /* 1 << 6 */ 64;
public static TypeFlags TypeFlagsBigInt => /* 1 << 7 */ 128;
public static TypeFlags TypeFlagsBoolean => /* 1 << 8 */ 256;
public static TypeFlags TypeFlagsESSymbol => /* 1 << 9 */ 512;
public static TypeFlags TypeFlagsStringLiteral => /* 1 << 10 */ 1024;
public static TypeFlags TypeFlagsNumberLiteral => /* 1 << 11 */ 2048;
public static TypeFlags TypeFlagsBigIntLiteral => /* 1 << 12 */ 4096;
public static TypeFlags TypeFlagsBooleanLiteral => /* 1 << 13 */ 8192;
public static TypeFlags TypeFlagsUniqueESSymbol => /* 1 << 14 */ 16384;
public static TypeFlags TypeFlagsEnumLiteral => /* 1 << 15 */ 32768;
public static TypeFlags TypeFlagsEnum => /* 1 << 16 */ 65536;
public static TypeFlags TypeFlagsNonPrimitive => /* 1 << 17 */ 131072;
public static TypeFlags TypeFlagsNever => /* 1 << 18 */ 262144;
public static TypeFlags TypeFlagsTypeParameter => /* 1 << 19 */ 524288;
public static TypeFlags TypeFlagsObject => /* 1 << 20 */ 1048576;
public static TypeFlags TypeFlagsIndex => /* 1 << 21 */ 2097152;
public static TypeFlags TypeFlagsTemplateLiteral => /* 1 << 22 */ 4194304;
public static TypeFlags TypeFlagsStringMapping => /* 1 << 23 */ 8388608;
public static TypeFlags TypeFlagsSubstitution => /* 1 << 24 */ 16777216;
public static TypeFlags TypeFlagsIndexedAccess => /* 1 << 25 */ 33554432;
public static TypeFlags TypeFlagsConditional => /* 1 << 26 */ 67108864;
public static TypeFlags TypeFlagsUnion => /* 1 << 27 */ 134217728;
public static TypeFlags TypeFlagsIntersection => /* 1 << 28 */ 268435456;
public static TypeFlags TypeFlagsReserved1 => /* 1 << 29 */ 536870912;
public static TypeFlags TypeFlagsReserved2 => /* 1 << 30 */ 1073741824;
public static TypeFlags TypeFlagsReserved3 => /* 1 << 31 */ 2147483648;
public static TypeFlags TypeFlagsAnyOrUnknown => /* TypeFlagsAny | TypeFlagsUnknown */ 3;
public static TypeFlags TypeFlagsNullable => /* TypeFlagsUndefined | TypeFlagsNull */ 12;
public static TypeFlags TypeFlagsLiteral => /* TypeFlagsStringLiteral | TypeFlagsNumberLiteral | TypeFlagsBigIntLiteral | TypeFlagsBooleanLiteral */ 15360;
public static TypeFlags TypeFlagsUnit => /* TypeFlagsEnum | TypeFlagsLiteral | TypeFlagsUniqueESSymbol | TypeFlagsNullable */ 97292;
public static TypeFlags TypeFlagsFreshable => /* TypeFlagsEnum | TypeFlagsLiteral */ 80896;
public static TypeFlags TypeFlagsStringOrNumberLiteral => /* TypeFlagsStringLiteral | TypeFlagsNumberLiteral */ 3072;
public static TypeFlags TypeFlagsStringOrNumberLiteralOrUnique => /* TypeFlagsStringLiteral | TypeFlagsNumberLiteral | TypeFlagsUniqueESSymbol */ 19456;
public static TypeFlags TypeFlagsDefinitelyFalsy => /* TypeFlagsStringLiteral | TypeFlagsNumberLiteral | TypeFlagsBigIntLiteral | TypeFlagsBooleanLiteral | TypeFlagsVoid | TypeFlagsUndefined | TypeFlagsNull */ 15388;
public static TypeFlags TypeFlagsPossiblyFalsy => /* TypeFlagsDefinitelyFalsy | TypeFlagsString | TypeFlagsNumber | TypeFlagsBigInt | TypeFlagsBoolean */ 15868;
public static TypeFlags TypeFlagsIntrinsic => /* TypeFlagsAny | TypeFlagsUnknown | TypeFlagsString | TypeFlagsNumber | TypeFlagsBigInt | TypeFlagsESSymbol | TypeFlagsVoid | TypeFlagsUndefined | TypeFlagsNull | TypeFlagsNever | TypeFlagsNonPrimitive */ 393983;
public static TypeFlags TypeFlagsStringLike => /* TypeFlagsString | TypeFlagsStringLiteral | TypeFlagsTemplateLiteral | TypeFlagsStringMapping */ 12583968;
public static TypeFlags TypeFlagsNumberLike => /* TypeFlagsNumber | TypeFlagsNumberLiteral | TypeFlagsEnum */ 67648;
public static TypeFlags TypeFlagsBigIntLike => /* TypeFlagsBigInt | TypeFlagsBigIntLiteral */ 4224;
public static TypeFlags TypeFlagsBooleanLike => /* TypeFlagsBoolean | TypeFlagsBooleanLiteral */ 8448;
public static TypeFlags TypeFlagsEnumLike => /* TypeFlagsEnum | TypeFlagsEnumLiteral */ 98304;
public static TypeFlags TypeFlagsESSymbolLike => /* TypeFlagsESSymbol | TypeFlagsUniqueESSymbol */ 16896;
public static TypeFlags TypeFlagsVoidLike => /* TypeFlagsVoid | TypeFlagsUndefined */ 20;
public static TypeFlags TypeFlagsPrimitive => /* TypeFlagsStringLike | TypeFlagsNumberLike | TypeFlagsBigIntLike | TypeFlagsBooleanLike | TypeFlagsEnumLike | TypeFlagsESSymbolLike | TypeFlagsVoidLike | TypeFlagsNull */ 12713980;
public static TypeFlags TypeFlagsDefinitelyNonNullable => /* TypeFlagsStringLike | TypeFlagsNumberLike | TypeFlagsBigIntLike | TypeFlagsBooleanLike | TypeFlagsEnumLike | TypeFlagsESSymbolLike | TypeFlagsObject | TypeFlagsNonPrimitive */ 13893600;
public static TypeFlags TypeFlagsDisjointDomains => /* TypeFlagsNonPrimitive | TypeFlagsStringLike | TypeFlagsNumberLike | TypeFlagsBigIntLike | TypeFlagsBooleanLike | TypeFlagsESSymbolLike | TypeFlagsVoidLike | TypeFlagsNull */ 12812284;
public static TypeFlags TypeFlagsUnionOrIntersection => /* TypeFlagsUnion | TypeFlagsIntersection */ 402653184;
public static TypeFlags TypeFlagsStructuredType => /* TypeFlagsObject | TypeFlagsUnion | TypeFlagsIntersection */ 403701760;
public static TypeFlags TypeFlagsTypeVariable => /* TypeFlagsTypeParameter | TypeFlagsIndexedAccess */ 34078720;
public static TypeFlags TypeFlagsInstantiableNonPrimitive => /* TypeFlagsTypeVariable | TypeFlagsConditional | TypeFlagsSubstitution */ 117964800;
public static TypeFlags TypeFlagsInstantiablePrimitive => /* TypeFlagsIndex | TypeFlagsTemplateLiteral | TypeFlagsStringMapping */ 14680064;
public static TypeFlags TypeFlagsInstantiable => /* TypeFlagsInstantiableNonPrimitive | TypeFlagsInstantiablePrimitive */ 132644864;
public static TypeFlags TypeFlagsStructuredOrInstantiable => /* TypeFlagsStructuredType | TypeFlagsInstantiable */ 536346624;
public static TypeFlags TypeFlagsObjectFlagsType => /* TypeFlagsAny | TypeFlagsNullable | TypeFlagsNever | TypeFlagsObject | TypeFlagsUnion | TypeFlagsIntersection */ 403963917;
public static TypeFlags TypeFlagsSimplifiable => /* TypeFlagsIndexedAccess | TypeFlagsConditional | TypeFlagsIndex */ 102760448;
public static TypeFlags TypeFlagsSingleton => /* TypeFlagsAny | TypeFlagsUnknown | TypeFlagsString | TypeFlagsNumber | TypeFlagsBoolean | TypeFlagsBigInt | TypeFlagsESSymbol | TypeFlagsVoid | TypeFlagsUndefined | TypeFlagsNull | TypeFlagsNever | TypeFlagsNonPrimitive */ 394239;
public static TypeFlags TypeFlagsNarrowable => /* TypeFlagsAny | TypeFlagsUnknown | TypeFlagsStructuredOrInstantiable | TypeFlagsStringLike | TypeFlagsNumberLike | TypeFlagsBigIntLike | TypeFlagsBooleanLike | TypeFlagsESSymbol | TypeFlagsUniqueESSymbol | TypeFlagsNonPrimitive */ 536575971;
public static TypeFlags TypeFlagsIncludesMask => /* TypeFlagsAny | TypeFlagsUnknown | TypeFlagsPrimitive | TypeFlagsNever | TypeFlagsObject | TypeFlagsUnion | TypeFlagsIntersection | TypeFlagsNonPrimitive | TypeFlagsTemplateLiteral | TypeFlagsStringMapping */ 416808959;
public static TypeFlags TypeFlagsIncludesMissingType => /* TypeFlagsTypeParameter */ 524288;
public static TypeFlags TypeFlagsIncludesNonWideningType => /* TypeFlagsIndex */ 2097152;
public static TypeFlags TypeFlagsIncludesWildcard => /* TypeFlagsIndexedAccess */ 33554432;
public static TypeFlags TypeFlagsIncludesEmptyObject => /* TypeFlagsConditional */ 67108864;
public static TypeFlags TypeFlagsIncludesInstantiable => /* TypeFlagsSubstitution */ 16777216;
public static TypeFlags TypeFlagsIncludesConstrainedTypeVariable => /* TypeFlagsReserved1 */ 536870912;
public static TypeFlags TypeFlagsIncludesError => /* TypeFlagsReserved2 */ 1073741824;
public static TypeFlags TypeFlagsNotPrimitiveUnion => /* TypeFlagsAny | TypeFlagsUnknown | TypeFlagsVoid | TypeFlagsNever | TypeFlagsObject | TypeFlagsIntersection | TypeFlagsIncludesInstantiable */ 286523411;


partial struct typeFlagNamesᴛ1 /*dyn*/ {
    internal TypeFlags flag;
    internal @string name;
}
[GoLazy] partial class lazyᴛtypeFlagNames { internal static array<typeFlagNamesᴛ1> typeFlagNames = new typeFlagNamesᴛ1[]{
    new(TypeFlagsAny, "Any"),
    new(TypeFlagsUnknown, "Unknown"),
    new(TypeFlagsUndefined, "Undefined"),
    new(TypeFlagsNull, "Null"),
    new(TypeFlagsVoid, "Void"),
    new(TypeFlagsString, "String"),
    new(TypeFlagsNumber, "Number"),
    new(TypeFlagsBigInt, "BigInt"),
    new(TypeFlagsBoolean, "Boolean"),
    new(TypeFlagsESSymbol, "ESSymbol"),
    new(TypeFlagsStringLiteral, "StringLiteral"),
    new(TypeFlagsNumberLiteral, "NumberLiteral"),
    new(TypeFlagsBigIntLiteral, "BigIntLiteral"),
    new(TypeFlagsBooleanLiteral, "BooleanLiteral"),
    new(TypeFlagsUniqueESSymbol, "UniqueESSymbol"),
    new(TypeFlagsEnumLiteral, "EnumLiteral"),
    new(TypeFlagsEnum, "Enum"),
    new(TypeFlagsNonPrimitive, "NonPrimitive"),
    new(TypeFlagsNever, "Never"),
    new(TypeFlagsTypeParameter, "TypeParameter"),
    new(TypeFlagsObject, "Object"),
    new(TypeFlagsIndex, "Index"),
    new(TypeFlagsTemplateLiteral, "TemplateLiteral"),
    new(TypeFlagsStringMapping, "StringMapping"),
    new(TypeFlagsSubstitution, "Substitution"),
    new(TypeFlagsIndexedAccess, "IndexedAccess"),
    new(TypeFlagsConditional, "Conditional"),
    new(TypeFlagsUnion, "Union"),
    new(TypeFlagsIntersection, "Intersection")
}.array(); }

public static slice<@string> FormatTypeFlags(TypeFlags flags) {
    var result = new slice<@string>(0, bits.OnesCount32((uint32)flags));
    foreach (var (_, fn) in range(typeFlagNames.ΔRangeSnapshot())) {
        if ((TypeFlags)(flags & fn.flag) != 0) {
            result = append(result, fn.name);
        }
    }
    if (len(result) == 0) {
        result = append(result, (@string)"None");
    }
    return result;
}

} partial class checkerᴛTypeFlags {

public static @string String(this TypeFlags f) {
    return strings.Join(FormatTypeFlags(f), "|");
}

} partial class checkerᴛVarianceFlags {

public static @string String(this VarianceFlags v) {
    var variance = (VarianceFlags)(v & VarianceFlagsVarianceMask);
    @string result = default;
    switch (variance) {
    case var ᴛ when ᴛ == VarianceFlagsInvariant: {
        result = "in out";
        break;
    }
    case var ᴛ when ᴛ == VarianceFlagsBivariant: {
        result = "[bivariant]";
        break;
    }
    case var ᴛ when ᴛ == VarianceFlagsContravariant: {
        result = "in";
        break;
    }
    case var ᴛ when ᴛ == VarianceFlagsCovariant: {
        result = "out";
        break;
    }
    case var ᴛ when ᴛ == VarianceFlagsIndependent: {
        result = "[independent]";
        break;
    }
    default: {
        result = "";
        break;
    }}

    if ((VarianceFlags)(v & VarianceFlagsUnmeasurable) != 0) {
        result += " (unmeasurable)"u8;
    } else if ((VarianceFlags)(v & VarianceFlagsUnreliable) != 0) {
        result += " (unreliable)"u8;
    }
    return result;
}

} partial class checker_package {

partial struct ObjectFlags /*num:uint32*/;

public static ObjectFlags ObjectFlagsNone => 0;
public static ObjectFlags ObjectFlagsClass => /* 1 << 0 */ 1;
public static ObjectFlags ObjectFlagsInterface => /* 1 << 1 */ 2;
public static ObjectFlags ObjectFlagsReference => /* 1 << 2 */ 4;
public static ObjectFlags ObjectFlagsTuple => /* 1 << 3 */ 8;
public static ObjectFlags ObjectFlagsAnonymous => /* 1 << 4 */ 16;
public static ObjectFlags ObjectFlagsMapped => /* 1 << 5 */ 32;
public static ObjectFlags ObjectFlagsInstantiated => /* 1 << 6 */ 64;
public static ObjectFlags ObjectFlagsObjectLiteral => /* 1 << 7 */ 128;
public static ObjectFlags ObjectFlagsEvolvingArray => /* 1 << 8 */ 256;
public static ObjectFlags ObjectFlagsObjectLiteralPatternWithComputedProperties => /* 1 << 9 */ 512;
public static ObjectFlags ObjectFlagsReverseMapped => /* 1 << 10 */ 1024;
public static ObjectFlags ObjectFlagsJsxAttributes => /* 1 << 11 */ 2048;
public static ObjectFlags ObjectFlagsJSLiteral => /* 1 << 12 */ 4096;
public static ObjectFlags ObjectFlagsFreshLiteral => /* 1 << 13 */ 8192;
public static ObjectFlags ObjectFlagsArrayLiteral => /* 1 << 14 */ 16384;
public static ObjectFlags ObjectFlagsPrimitiveUnion => /* 1 << 15 */ 32768;
public static ObjectFlags ObjectFlagsContainsWideningType => /* 1 << 16 */ 65536;
public static ObjectFlags ObjectFlagsContainsObjectOrArrayLiteral => /* 1 << 17 */ 131072;
public static ObjectFlags ObjectFlagsNonInferrableType => /* 1 << 18 */ 262144;
public static ObjectFlags ObjectFlagsCouldContainTypeVariablesComputed => /* 1 << 19 */ 524288;
public static ObjectFlags ObjectFlagsCouldContainTypeVariables => /* 1 << 20 */ 1048576;
public static ObjectFlags ObjectFlagsMembersResolved => /* 1 << 21 */ 2097152;
public static ObjectFlags ObjectFlagsClassOrInterface => /* ObjectFlagsClass | ObjectFlagsInterface */ 3;
public static ObjectFlags ObjectFlagsRequiresWidening => /* ObjectFlagsContainsWideningType | ObjectFlagsContainsObjectOrArrayLiteral */ 196608;
public static ObjectFlags ObjectFlagsPropagatingFlags => /* ObjectFlagsContainsWideningType | ObjectFlagsContainsObjectOrArrayLiteral | ObjectFlagsNonInferrableType */ 458752;
public static ObjectFlags ObjectFlagsInstantiatedMapped => /* ObjectFlagsMapped | ObjectFlagsInstantiated */ 96;
public static ObjectFlags ObjectFlagsObjectTypeKindMask => /* ObjectFlagsClassOrInterface | ObjectFlagsReference | ObjectFlagsTuple | ObjectFlagsAnonymous | ObjectFlagsMapped | ObjectFlagsReverseMapped | ObjectFlagsEvolvingArray | ObjectFlagsInstantiationExpressionType | ObjectFlagsSingleSignatureType */ 50332991;
public static UntypedInt ObjectFlagsContainsSpread => /* 1 << 22 */ 4194304;
public static UntypedInt ObjectFlagsObjectRestType => /* 1 << 23 */ 8388608;
public static UntypedInt ObjectFlagsInstantiationExpressionType => /* 1 << 24 */ 16777216;
public static UntypedInt ObjectFlagsSingleSignatureType => /* 1 << 25 */ 33554432;
public static UntypedInt ObjectFlagsIsClassInstanceClone => /* 1 << 26 */ 67108864;
public static UntypedInt ObjectFlagsIdenticalBaseTypeCalculated => /* 1 << 27 */ 134217728;
public static UntypedInt ObjectFlagsIdenticalBaseTypeExists => /* 1 << 28 */ 268435456;
public static UntypedInt ObjectFlagsUnresolvedMembers => /* 1 << 29 */ 536870912;
public static UntypedInt ObjectFlagsFromTypeNode => /* 1 << 30 */ 1073741824;
public static UntypedInt ObjectFlagsIsGenericTypeComputed => /* 1 << 22 */ 4194304;
public static UntypedInt ObjectFlagsIsGenericObjectType => /* 1 << 23 */ 8388608;
public static UntypedInt ObjectFlagsIsGenericIndexType => /* 1 << 24 */ 16777216;
public static UntypedInt ObjectFlagsIsGenericType => /* ObjectFlagsIsGenericObjectType | ObjectFlagsIsGenericIndexType */ 25165824;
public static UntypedInt ObjectFlagsContainsIntersections => /* 1 << 25 */ 33554432;
public static UntypedInt ObjectFlagsIsUnknownLikeUnionComputed => /* 1 << 26 */ 67108864;
public static UntypedInt ObjectFlagsIsUnknownLikeUnion => /* 1 << 27 */ 134217728;
public static UntypedInt ObjectFlagsIsUniformEnumComputed => /* 1 << 28 */ 268435456;
public static UntypedInt ObjectFlagsIsUniformEnum => /* 1 << 29 */ 536870912;
public static UntypedInt ObjectFlagsIsNeverIntersectionComputed => /* 1 << 25 */ 33554432;
public static UntypedInt ObjectFlagsIsNeverIntersection => /* 1 << 26 */ 67108864;
public static UntypedInt ObjectFlagsIsConstrainedTypeVariable => /* 1 << 27 */ 134217728;

partial struct TypeAlias {
    internal ж<ast.Symbol> symbol;
    internal slice<ж<Type>> typeArguments;
}

} partial class checkerᴛTypeAlias {

public static ж<ast.Symbol> Symbol(this ж<TypeAlias> Ꮡa) {
    ref var a = ref Ꮡa.DerefOrNull();

    if (Ꮡa == nil) {
        return default;
    }
    return a.symbol;
}

public static slice<ж<Type>> TypeArguments(this ж<TypeAlias> Ꮡa) {
    ref var a = ref Ꮡa.DerefOrNull();

    if (Ꮡa == nil) {
        return default;
    }
    return a.typeArguments;
}

} partial class checker_package {

partial struct Type {
    internal TypeFlags flags;
    internal ObjectFlags objectFlags;
    internal TypeId id;
    internal ж<ast.Symbol> symbol;
    internal ж<TypeAlias> alias;
    internal ж<Checker> checker;
    internal TypeData data;
}

} partial class checkerᴛType {

public static TypeId Id(this ref Type t) {
    return t.id;
}

public static TypeFlags Flags(this ref Type t) {
    return t.flags;
}

public static ObjectFlags ObjectFlags(this ref Type t) {
    return t.objectFlags;
}

public static ж<IntrinsicType> AsIntrinsicType(this ref Type t) { return t.data._<ж<IntrinsicType>>(); }
public static ж<LiteralType> AsLiteralType(this ref Type t) { return t.data._<ж<LiteralType>>(); }
public static ж<UniqueESSymbolType> AsUniqueESSymbolType(this ref Type t) { return t.data._<ж<UniqueESSymbolType>>(); }
public static ж<TupleType> AsTupleType(this ref Type t) { return t.data._<ж<TupleType>>(); }
public static ж<InstantiationExpressionType> AsInstantiationExpressionType(this ref Type t) {
    return t.data._<ж<InstantiationExpressionType>>();
}
public static ж<MappedType> AsMappedType(this ref Type t) { return t.data._<ж<MappedType>>(); }
public static ж<ReverseMappedType> AsReverseMappedType(this ref Type t) { return t.data._<ж<ReverseMappedType>>(); }
public static ж<EvolvingArrayType> AsEvolvingArrayType(this ref Type t) { return t.data._<ж<EvolvingArrayType>>(); }
public static ж<TypeParameter> AsTypeParameter(this ref Type t) { return t.data._<ж<TypeParameter>>(); }
public static ж<UnionType> AsUnionType(this ref Type t) { return t.data._<ж<UnionType>>(); }
public static ж<IntersectionType> AsIntersectionType(this ref Type t) { return t.data._<ж<IntersectionType>>(); }
public static ж<IndexType> AsIndexType(this ref Type t) { return t.data._<ж<IndexType>>(); }
public static ж<IndexedAccessType> AsIndexedAccessType(this ref Type t) { return t.data._<ж<IndexedAccessType>>(); }
public static ж<TemplateLiteralType> AsTemplateLiteralType(this ref Type t) { return t.data._<ж<TemplateLiteralType>>(); }
public static ж<StringMappingType> AsStringMappingType(this ref Type t) { return t.data._<ж<StringMappingType>>(); }
public static ж<SubstitutionType> AsSubstitutionType(this ref Type t) { return t.data._<ж<SubstitutionType>>(); }
public static ж<ConditionalType> AsConditionalType(this ref Type t) { return t.data._<ж<ConditionalType>>(); }

public static ж<ConstrainedType> AsConstrainedType(this ref Type t) { return t.data.AsConstrainedType(); }
public static ж<StructuredType> AsStructuredType(this ref Type t) { return t.data.AsStructuredType(); }
public static ж<ObjectType> AsObjectType(this ref Type t) { return t.data.AsObjectType(); }
public static ж<TypeReference> AsTypeReference(this ref Type t) { return t.data.AsTypeReference(); }
public static ж<InterfaceType> AsInterfaceType(this ref Type t) { return t.data.AsInterfaceType(); }
public static ж<UnionOrIntersectionType> AsUnionOrIntersectionType(this ref Type t) {
    return t.data.AsUnionOrIntersectionType();
}

public static slice<ж<Type>> Distributed(this ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(t.flags & TypeFlagsUnion) != 0: {
        return t.AsUnionType().types;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsNever) != 0: {
        return default;
    }}

    return [Ꮡt];
}

public static ж<Type> Target(this ref Type t) {
    switch (ᐧ) {
    case {} when (TypeFlags)(t.flags & TypeFlagsObject) != 0: {
        return t.AsObjectType().target;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0: {
        return t.AsTypeParameter().target;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsIndex) != 0: {
        return t.AsIndexType().target;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsStringMapping) != 0: {
        return t.AsStringMappingType().target;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsObject) != 0 && (ObjectFlags)(t.objectFlags & ObjectFlagsMapped) != 0: {
        return t.AsMappedType().target;
    }}

    throw panic("Unhandled case in Type.Target");
}

public static ж<TypeMapper> Mapper(this ref Type t) {
    switch (ᐧ) {
    case {} when (TypeFlags)(t.flags & TypeFlagsObject) != 0: {
        return t.AsObjectType().mapper;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0: {
        return t.AsTypeParameter().mapper;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsConditional) != 0: {
        return t.AsConditionalType().mapper;
    }}

    throw panic("Unhandled case in Type.Mapper");
}

public static slice<ж<Type>> Types(this ref Type t) {
    switch (ᐧ) {
    case {} when (TypeFlags)(t.flags & TypeFlagsUnionOrIntersection) != 0: {
        return t.AsUnionOrIntersectionType().types;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsTemplateLiteral) != 0: {
        return t.AsTemplateLiteralType().types;
    }}

    throw panic("Unhandled case in Type.Types");
}

public static ж<InterfaceType> TargetInterfaceType(this ref Type t) {
    return t.AsTypeReference().target.AsInterfaceType();
}

public static ж<TupleType> TargetTupleType(this ref Type t) {
    return t.AsTypeReference().target.AsTupleType();
}

public static ж<ast.Symbol> Symbol(this ref Type t) {
    return t.symbol;
}

public static ж<TypeAlias> Alias(this ref Type t) {
    return t.alias;
}

public static bool IsUnion(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsUnion) != 0;
}

public static bool IsString(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsString) != 0;
}

public static bool IsIntersection(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsIntersection) != 0;
}

public static bool IsStringLiteral(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsStringLiteral) != 0;
}

public static bool IsNumberLiteral(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsNumberLiteral) != 0;
}

public static bool IsBigIntLiteral(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsBigIntLiteral) != 0;
}

public static bool IsEnumLiteral(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsEnumLiteral) != 0;
}

public static bool IsBooleanLike(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsBooleanLike) != 0;
}

public static bool IsStringLike(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsStringLike) != 0;
}

public static bool IsClass(this ref Type t) {
    return (ObjectFlags)(t.objectFlags & ObjectFlagsClass) != 0;
}

public static bool IsTypeParameter(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0;
}

public static bool IsIndex(this ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsIndex) != 0;
}

public static bool IsTupleType(this ж<Type> Ꮡt) {
    return isTupleType(Ꮡt);
}

} partial class checker_package {

partial interface TypeData {
    ж<Type> AsType();
    ж<ConstrainedType> AsConstrainedType();
    ж<StructuredType> AsStructuredType();
    ж<ObjectType> AsObjectType();
    ж<TypeReference> AsTypeReference();
    ж<InterfaceType> AsInterfaceType();
    ж<UnionOrIntersectionType> AsUnionOrIntersectionType();
}

partial struct TypeBase {
    /*embed*/ public Type Type;
}

} partial class checkerᴛTypeBase {

public static ж<Type> AsType(this ж<TypeBase> Ꮡt) { return Ꮡt.ᏑType; }
public static ж<ConstrainedType> AsConstrainedType(this ref TypeBase t) { return default; }
public static ж<StructuredType> AsStructuredType(this ref TypeBase t) { return default; }
public static ж<ObjectType> AsObjectType(this ref TypeBase t) { return default; }
public static ж<TypeReference> AsTypeReference(this ref TypeBase t) { return default; }
public static ж<InterfaceType> AsInterfaceType(this ref TypeBase t) { return default; }
public static ж<UnionOrIntersectionType> AsUnionOrIntersectionType(this ref TypeBase t) { return default; }

} partial class checker_package {

partial struct IntrinsicType {
    /*embed*/ public TypeBase TypeBase;
    internal @string intrinsicName;
}

} partial class checkerᴛIntrinsicType {

public static @string IntrinsicName(this ref IntrinsicType t) { return t.intrinsicName; }

} partial class checker_package {

partial struct LiteralType {
    /*embed*/ public TypeBase TypeBase;
    internal any value;
    internal ж<Type> freshType;
    internal ж<Type> regularType;
}

} partial class checkerᴛLiteralType {

public static any Value(this ref LiteralType t) {
    return t.value;
}

public static ж<Type> FreshType(this ref LiteralType t) {
    return t.freshType;
}

public static ж<Type> RegularType(this ref LiteralType t) {
    return t.regularType;
}

public static @string String(this ref LiteralType t) {
    return ValueToString(t.value);
}

} partial class checker_package {

partial struct UniqueESSymbolType {
    /*embed*/ public TypeBase TypeBase;
    internal @string name;
}

partial struct ConstrainedType {
    /*embed*/ public TypeBase TypeBase;
    internal ж<Type> resolvedBaseConstraint;
}

} partial class checkerᴛConstrainedType {

public static ж<ConstrainedType> AsConstrainedType(this ж<ConstrainedType> Ꮡt) { return Ꮡt; }

} partial class checker_package {

partial struct StructuredType {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal ast.SymbolTable members;
    internal slice<ж<ast.Symbol>> properties;
    internal slice<ж<Signature>> signatures;
    internal nint callSignatureCount;
    internal slice<ж<IndexInfo>> indexInfos;
    internal ж<Type> objectTypeWithoutAbstractConstructSignatures;
}

} partial class checkerᴛStructuredType {

public static ж<StructuredType> AsStructuredType(this ж<StructuredType> Ꮡt) { return Ꮡt; }

public static slice<ж<Signature>> CallSignatures(this ref StructuredType t) {
    return slices.Clip<slice<ж<Signature>>, ж<Signature>>(t.signatures.slice(0, t.callSignatureCount));
}

public static slice<ж<Signature>> ConstructSignatures(this ref StructuredType t) {
    return slices.Clip<slice<ж<Signature>>, ж<Signature>>(t.signatures.slice(t.callSignatureCount));
}

public static slice<ж<ast.Symbol>> Properties(this ref StructuredType t) {
    return t.properties;
}

} partial class checker_package {

partial struct ObjectType {
    /*embed*/ public StructuredType StructuredType;
    internal ж<Type> target;
    internal ж<TypeMapper> mapper;
    internal map<CacheHashKey, ж<Type>> instantiations;
}

} partial class checkerᴛObjectType {

public static ж<ObjectType> AsObjectType(this ж<ObjectType> Ꮡt) { return Ꮡt; }

} partial class checker_package {

partial struct TypeReference {
    /*embed*/ public ObjectType ObjectType;
    internal ж<ast.Node> node;
    internal slice<ж<Type>> resolvedTypeArguments;
}

} partial class checkerᴛTypeReference {

public static ж<TypeReference> AsTypeReference(this ж<TypeReference> Ꮡt) { return Ꮡt; }

} partial class checker_package {

partial struct InterfaceType {
    /*embed*/ public TypeReference TypeReference;
    internal slice<ж<Type>> allTypeParameters;
    internal nint outerTypeParameterCount;
    internal ж<Type> thisType;
    internal bool baseTypesResolved;
    internal bool declaredMembersResolved;
    internal ж<Type> resolvedBaseConstructorType;
    internal slice<ж<Type>> resolvedBaseTypes;
    internal ast.SymbolTable declaredMembers;
    internal slice<ж<Signature>> declaredCallSignatures;
    internal slice<ж<Signature>> declaredConstructSignatures;
    internal slice<ж<IndexInfo>> declaredIndexInfos;
}

} partial class checkerᴛInterfaceType {

public static ж<InterfaceType> AsInterfaceType(this ж<InterfaceType> Ꮡt) { return Ꮡt; }

public static slice<ж<Type>> OuterTypeParameters(this ref InterfaceType t) {
    if (len(t.allTypeParameters) == 0) {
        return default;
    }
    return slices.Clip<slice<ж<Type>>, ж<Type>>(t.allTypeParameters.slice(0, t.outerTypeParameterCount));
}

public static slice<ж<Type>> LocalTypeParameters(this ref InterfaceType t) {
    if (len(t.allTypeParameters) == 0) {
        return default;
    }
    return slices.Clip<slice<ж<Type>>, ж<Type>>(t.allTypeParameters.slice(t.outerTypeParameterCount, len(t.allTypeParameters) - 1));
}

public static slice<ж<Type>> TypeParameters(this ref InterfaceType t) {
    if (len(t.allTypeParameters) == 0) {
        return default;
    }
    return slices.Clip<slice<ж<Type>>, ж<Type>>(t.allTypeParameters.slice(0, len(t.allTypeParameters) - 1));
}

public static ж<Type> ThisType(this ref InterfaceType t) {
    return t.thisType;
}

} partial class checker_package {

partial struct ElementFlags /*num:uint32*/;

public static ElementFlags ElementFlagsNone => 0;
public static ElementFlags ElementFlagsRequired => /* 1 << 0 */ 1;
public static ElementFlags ElementFlagsOptional => /* 1 << 1 */ 2;
public static ElementFlags ElementFlagsRest => /* 1 << 2 */ 4;
public static ElementFlags ElementFlagsVariadic => /* 1 << 3 */ 8;
public static ElementFlags ElementFlagsFixed => /* ElementFlagsRequired | ElementFlagsOptional */ 3;
public static ElementFlags ElementFlagsVariable => /* ElementFlagsRest | ElementFlagsVariadic */ 12;
public static ElementFlags ElementFlagsNonRequired => /* ElementFlagsOptional | ElementFlagsRest | ElementFlagsVariadic */ 14;
public static ElementFlags ElementFlagsNonRest => /* ElementFlagsRequired | ElementFlagsOptional | ElementFlagsVariadic */ 11;

partial struct TupleElementInfo {
    internal ElementFlags flags;
    internal ж<ast.Node> labeledDeclaration;
}

} partial class checkerᴛTupleElementInfo {

public static ElementFlags TupleElementFlags(this ref TupleElementInfo t) { return t.flags; }
public static ж<ast.Node> LabeledDeclaration(this ref TupleElementInfo t) { return t.labeledDeclaration; }

} partial class checker_package {

partial struct TupleType {
    /*embed*/ public InterfaceType InterfaceType;
    internal slice<TupleElementInfo> elementInfos;
    internal nint minLength;
    internal nint fixedLength;
    internal ElementFlags combinedFlags;
    internal bool @readonly;
}

} partial class checkerᴛTupleType {

public static nint FixedLength(this ref TupleType t) { return t.fixedLength; }
public static bool IsReadonly(this ref TupleType t) { return t.@readonly; }
public static slice<ElementFlags> ElementFlags(this ref TupleType t) {
    var elementFlags = new slice<ElementFlags>(len(t.elementInfos));
    foreach (var (i, info) in range(t.elementInfos)) {
        elementFlags[i] = info.flags;
    }
    return elementFlags;
}
public static slice<TupleElementInfo> ElementInfos(this ref TupleType t) { return t.elementInfos; }

} partial class checker_package {

partial struct InstantiationExpressionType {
    /*embed*/ public ObjectType ObjectType;
    internal ж<ast.Node> node;
}

partial struct MappedType {
    /*embed*/ public ObjectType ObjectType;
    internal ж<ast.MappedTypeNode> declaration;
    internal ж<Type> typeParameter;
    internal ж<Type> constraintType;
    internal ж<Type> nameType;
    internal ж<Type> templateType;
    internal ж<Type> modifiersType;
    internal ж<Type> resolvedApparentType;
    internal bool containsError;
}

partial struct ReverseMappedType {
    /*embed*/ public ObjectType ObjectType;
    internal ж<Type> source;
    internal ж<Type> mappedType;
    internal ж<Type> constraintType;
}

partial struct EvolvingArrayType {
    /*embed*/ public ObjectType ObjectType;
    internal ж<Type> elementType;
    internal ж<Type> finalArrayType;
}

partial struct UnionOrIntersectionType {
    /*embed*/ public StructuredType StructuredType;
    internal slice<ж<Type>> types;
    internal ast.SymbolTable propertyCache;
    internal ast.SymbolTable propertyCacheWithoutFunctionPropertyAugment;
    internal slice<ж<ast.Symbol>> resolvedProperties;
}

} partial class checkerᴛUnionOrIntersectionType {

public static ж<UnionOrIntersectionType> AsUnionOrIntersectionType(this ж<UnionOrIntersectionType> Ꮡt) { return Ꮡt; }

public static slice<ж<Type>> Types(this ref UnionOrIntersectionType t) {
    return t.types;
}

} partial class checker_package {

partial struct UnionType {
    /*embed*/ public UnionOrIntersectionType UnionOrIntersectionType;
    internal ж<Type> resolvedReducedType;
    internal ж<Type> regularType;
    internal ж<Type> origin;
    internal @string keyPropertyName;
    internal map<ж<Type>, ж<Type>> constituentMap;
}

partial struct IntersectionType {
    /*embed*/ public UnionOrIntersectionType UnionOrIntersectionType;
    internal ж<Type> resolvedApparentType;
    internal ж<Type> uniqueLiteralFilledInstantiation;
}

partial struct TypeParameter {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal ж<Type> constraint;
    internal ж<Type> target;
    internal ж<TypeMapper> mapper;
    internal bool isThisType;
    internal bool isDistributed;
    internal ж<Type> resolvedDefaultType;
    internal ж<Type> distributedType;
}

} partial class checkerᴛTypeParameter {

public static bool IsThisType(this ref TypeParameter t) { return t.isThisType; }

} partial class checker_package {

partial struct IndexFlags /*num:uint32*/;

public static IndexFlags IndexFlagsNone => 0;
public static IndexFlags IndexFlagsStringsOnly => /* 1 << 0 */ 1;
public static IndexFlags IndexFlagsNoIndexSignatures => /* 1 << 1 */ 2;
public static IndexFlags IndexFlagsNoReducibleCheck => /* 1 << 2 */ 4;

partial struct IndexType {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal ж<Type> target;
    internal IndexFlags indexFlags;
}

} partial class checkerᴛIndexType {

public static ж<Type> Target(this ref IndexType t) { return t.target; }

} partial class checker_package {

partial struct IndexedAccessType {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal ж<Type> objectType;
    internal ж<Type> indexType;
    internal AccessFlags accessFlags;
}

} partial class checkerᴛIndexedAccessType {

public static ж<Type> ObjectType(this ref IndexedAccessType t) { return t.objectType; }
public static ж<Type> IndexType(this ref IndexedAccessType t) { return t.indexType; }

} partial class checker_package {

partial struct TemplateLiteralType {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal slice<@string> texts;
    internal slice<ж<Type>> types;
}

} partial class checkerᴛTemplateLiteralType {

public static slice<@string> Texts(this ref TemplateLiteralType t) { return t.texts; }
public static slice<ж<Type>> Types(this ref TemplateLiteralType t) { return t.types; }

} partial class checker_package {

partial struct StringMappingType {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal ж<Type> target;
}

} partial class checkerᴛStringMappingType {

public static ж<Type> Target(this ref StringMappingType t) { return t.target; }

} partial class checker_package {

partial struct SubstitutionType {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal ж<Type> baseType;
    internal ж<Type> constraint;
}

} partial class checkerᴛSubstitutionType {

public static ж<Type> BaseType(this ref SubstitutionType t) { return t.baseType; }
public static ж<Type> SubstConstraint(this ref SubstitutionType t) { return t.constraint; }

} partial class checker_package {

partial struct ConditionalRoot {
    internal ж<ast.ConditionalTypeNode> node;
    internal ж<Type> checkType;
    internal ж<Type> extendsType;
    internal bool isDistributive;
    internal slice<ж<Type>> inferTypeParameters;
    internal slice<ж<Type>> outerTypeParameters;
    internal map<CacheHashKey, ж<Type>> instantiations;
    internal ж<TypeAlias> alias;
}

partial struct ConditionalType {
    /*embed*/ public ConstrainedType ConstrainedType;
    internal ж<ConditionalRoot> root;
    internal ж<Type> checkType;
    internal ж<Type> extendsType;
    internal ж<Type> resolvedTrueType;
    internal ж<Type> resolvedFalseType;
    internal ж<Type> resolvedInferredTrueType;
    internal ж<Type> resolvedDefaultConstraint;
    internal ж<Type> resolvedConstraintOfDistributive;
    internal ж<TypeMapper> mapper;
    internal ж<TypeMapper> combinedMapper;
}

} partial class checkerᴛConditionalType {

public static ж<Type> CheckType(this ref ConditionalType t) { return t.checkType; }
public static ж<Type> ExtendsType(this ref ConditionalType t) { return t.extendsType; }

} partial class checker_package {

partial struct SignatureFlags /*num:uint32*/;

public static SignatureFlags SignatureFlagsNone => 0;
public static SignatureFlags SignatureFlagsHasRestParameter => /* 1 << 0 */ 1;
public static SignatureFlags SignatureFlagsHasLiteralTypes => /* 1 << 1 */ 2;
public static SignatureFlags SignatureFlagsConstruct => /* 1 << 2 */ 4;
public static SignatureFlags SignatureFlagsAbstract => /* 1 << 3 */ 8;
public static SignatureFlags SignatureFlagsIsInnerCallChain => /* 1 << 4 */ 16;
public static SignatureFlags SignatureFlagsIsOuterCallChain => /* 1 << 5 */ 32;
public static SignatureFlags SignatureFlagsIsUntypedSignatureInJSFile => /* 1 << 6 */ 64;
public static SignatureFlags SignatureFlagsIsNonInferrable => /* 1 << 7 */ 128;
public static SignatureFlags SignatureFlagsIsSignatureCandidateForOverloadFailure => /* 1 << 8 */ 256;
public static SignatureFlags SignatureFlagsPropagatingFlags => /* SignatureFlagsHasRestParameter | SignatureFlagsHasLiteralTypes | SignatureFlagsConstruct | SignatureFlagsAbstract | SignatureFlagsIsUntypedSignatureInJSFile | SignatureFlagsIsSignatureCandidateForOverloadFailure */ 335;
public static SignatureFlags SignatureFlagsCallChainFlags => /* SignatureFlagsIsInnerCallChain | SignatureFlagsIsOuterCallChain */ 48;

partial struct Signature {
    internal SignatureId id;
    internal SignatureFlags flags;
    internal int32 minArgumentCount;
    internal int32 resolvedMinArgumentCount;
    internal ж<ast.Node> declaration;
    internal slice<ж<Type>> typeParameters;
    internal slice<ж<ast.Symbol>> parameters;
    internal ж<ast.Symbol> thisParameter;
    internal ж<Type> resolvedReturnType;
    internal ж<TypePredicate> resolvedTypePredicate;
    internal ж<Signature> target;
    internal ж<TypeMapper> mapper;
    internal ж<Type> isolatedSignatureType;
    internal ж<CompositeSignature> composite;
}

} partial class checkerᴛSignature {

public static SignatureId Id(this ref Signature s) {
    return s.id;
}

public static SignatureFlags Flags(this ref Signature s) {
    return s.flags;
}

public static slice<ж<Type>> TypeParameters(this ref Signature s) {
    return s.typeParameters;
}

public static ж<ast.Node> Declaration(this ref Signature s) {
    return s.declaration;
}

public static ж<Signature> Target(this ref Signature s) {
    return s.target;
}

public static ж<ast.Symbol> ThisParameter(this ref Signature s) {
    return s.thisParameter;
}

public static slice<ж<ast.Symbol>> Parameters(this ref Signature s) {
    return s.parameters;
}

public static bool HasRestParameter(this ref Signature s) {
    return (SignatureFlags)(s.flags & SignatureFlagsHasRestParameter) != 0;
}

public static nint MinArgumentCount(this ref Signature s) {
    return (nint)s.minArgumentCount;
}

} partial class checker_package {

partial struct CompositeSignature {
    internal bool isUnion;
    internal slice<ж<Signature>> signatures;
}

partial struct TypePredicateKind /*num:int32*/;

public static TypePredicateKind TypePredicateKindThis => /* iota */ 0;
public static TypePredicateKind TypePredicateKindIdentifier => 1;
public static TypePredicateKind TypePredicateKindAssertsThis => 2;
public static TypePredicateKind TypePredicateKindAssertsIdentifier => 3;

partial struct TypePredicate {
    internal TypePredicateKind kind;
    internal int32 parameterIndex;
    internal @string parameterName;
    internal ж<Type> t;
}

} partial class checkerᴛTypePredicate {

public static ж<Type> Type(this ref TypePredicate typePredicate) {
    return typePredicate.t;
}

public static TypePredicateKind Kind(this ref TypePredicate typePredicate) {
    return typePredicate.kind;
}

public static int32 ParameterIndex(this ref TypePredicate typePredicate) {
    return typePredicate.parameterIndex;
}

public static @string ParameterName(this ref TypePredicate typePredicate) {
    return typePredicate.parameterName;
}

} partial class checker_package {

partial struct IndexInfo {
    internal ж<Type> keyType;
    internal ж<Type> valueType;
    internal bool isReadonly;
    internal ж<ast.Node> declaration;
    internal ж<ast.Symbol> indexSymbol;
    internal slice<ж<ast.Node>> components;
}

} partial class checkerᴛIndexInfo {

public static ж<Type> KeyType(this ref IndexInfo info) {
    return info.keyType;
}

public static ж<Type> ValueType(this ref IndexInfo info) {
    return info.valueType;
}

public static bool IsReadonly(this ref IndexInfo info) {
    return info.isReadonly;
}

public static ж<ast.Node> Declaration(this ref IndexInfo info) {
    return info.declaration;
}

} partial class checker_package {

partial struct Ternary /*num:int8*/;

public static Ternary TernaryFalse => 0;
public static Ternary TernaryUnknown => 1;
public static Ternary TernaryMaybe => 3;
public static Ternary TernaryTrue => -1;

// type TypeComparer is a methodless func type — rendered inline as its base delegate

partial struct LanguageFeatureMinimumTargetMap {
    public core.ScriptTarget Exponentiation;
    public core.ScriptTarget AsyncFunctions;
    public core.ScriptTarget ForAwaitOf;
    public core.ScriptTarget AsyncGenerators;
    public core.ScriptTarget AsyncIteration;
    public core.ScriptTarget ObjectSpreadRest;
    public core.ScriptTarget RegularExpressionFlagsDotAll;
    public core.ScriptTarget BindinglessCatch;
    public core.ScriptTarget BigInt;
    public core.ScriptTarget NullishCoalesce;
    public core.ScriptTarget OptionalChaining;
    public core.ScriptTarget LogicalAssignment;
    public core.ScriptTarget TopLevelAwait;
    public core.ScriptTarget ClassFields;
    public core.ScriptTarget PrivateNamesAndClassStaticBlocks;
    public core.ScriptTarget RegularExpressionFlagsHasIndices;
    public core.ScriptTarget ShebangComments;
    public core.ScriptTarget UsingAndAwaitUsing;
    public core.ScriptTarget ClassAndClassElementDecorators;
    public core.ScriptTarget RegularExpressionFlagsUnicodeSets;
}

[GoLazy] partial class lazyᴛLanguageFeatureMinimumTarget { public static readonly ж<LanguageFeatureMinimumTargetMap> ᏑLanguageFeatureMinimumTarget = new StaticBox<LanguageFeatureMinimumTargetMap>(new LanguageFeatureMinimumTargetMap(
    Exponentiation: core.ScriptTargetES2016,
    AsyncFunctions: core.ScriptTargetES2017,
    ForAwaitOf: core.ScriptTargetES2018,
    AsyncGenerators: core.ScriptTargetES2018,
    AsyncIteration: core.ScriptTargetES2018,
    ObjectSpreadRest: core.ScriptTargetES2018,
    RegularExpressionFlagsDotAll: core.ScriptTargetES2018,
    BindinglessCatch: core.ScriptTargetES2019,
    BigInt: core.ScriptTargetES2020,
    NullishCoalesce: core.ScriptTargetES2020,
    OptionalChaining: core.ScriptTargetES2020,
    LogicalAssignment: core.ScriptTargetES2021,
    TopLevelAwait: core.ScriptTargetES2022,
    ClassFields: core.ScriptTargetES2022,
    PrivateNamesAndClassStaticBlocks: core.ScriptTargetES2022,
    RegularExpressionFlagsHasIndices: core.ScriptTargetES2022,
    ShebangComments: core.ScriptTargetESNext,
    UsingAndAwaitUsing: core.ScriptTargetESNext,
    ClassAndClassElementDecorators: core.ScriptTargetESNext,
    RegularExpressionFlagsUnicodeSets: core.ScriptTargetESNext
)); }
public static ref LanguageFeatureMinimumTargetMap LanguageFeatureMinimumTarget => ref ((StaticBox<LanguageFeatureMinimumTargetMap>)ᏑLanguageFeatureMinimumTarget).ValueSlot;

} // end checker_package
