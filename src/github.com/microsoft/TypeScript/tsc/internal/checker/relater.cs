namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using ꓸꓸꓸany = Span<any>;
using static checker_package;

partial class checker_package {

partial struct SignatureCheckMode /*num:uint32*/;

public static SignatureCheckMode SignatureCheckModeNone => 0;
public static SignatureCheckMode SignatureCheckModeBivariantCallback => /* 1 << 0 */ 1;
public static SignatureCheckMode SignatureCheckModeStrictCallback => /* 1 << 1 */ 2;
public static SignatureCheckMode SignatureCheckModeIgnoreReturnTypes => /* 1 << 2 */ 4;
public static SignatureCheckMode SignatureCheckModeStrictArity => /* 1 << 3 */ 8;
public static SignatureCheckMode SignatureCheckModeStrictTopSignature => /* 1 << 4 */ 16;
public static SignatureCheckMode SignatureCheckModeCallback => /* SignatureCheckModeBivariantCallback | SignatureCheckModeStrictCallback */ 3;

partial struct MinArgumentCountFlags /*num:uint32*/;

public static MinArgumentCountFlags MinArgumentCountFlagsNone => 0;
public static MinArgumentCountFlags MinArgumentCountFlagsStrongArityForUntypedJS => /* 1 << 0 */ 1;
public static MinArgumentCountFlags MinArgumentCountFlagsVoidIsNonOptional => /* 1 << 1 */ 2;

partial struct IntersectionState /*num:uint32*/;

public static IntersectionState IntersectionStateNone => 0;
public static IntersectionState IntersectionStateSource => /* 1 << 0 */ 1;
public static IntersectionState IntersectionStateTarget => /* 1 << 1 */ 2;

partial struct RecursionFlags /*num:uint32*/;

public static RecursionFlags RecursionFlagsNone => 0;
public static RecursionFlags RecursionFlagsSource => /* 1 << 0 */ 1;
public static RecursionFlags RecursionFlagsTarget => /* 1 << 1 */ 2;
public static RecursionFlags RecursionFlagsBoth => /* RecursionFlagsSource | RecursionFlagsTarget */ 3;

partial struct ExpandingFlags /*num:uint8*/;

public static ExpandingFlags ExpandingFlagsNone => 0;
public static ExpandingFlags ExpandingFlagsSource => /* 1 << 0 */ 1;
public static ExpandingFlags ExpandingFlagsTarget => /* 1 << 1 */ 2;
public static ExpandingFlags ExpandingFlagsBoth => /* ExpandingFlagsSource | ExpandingFlagsTarget */ 3;

partial struct RelationComparisonResult /*num:uint32*/;

public static RelationComparisonResult RelationComparisonResultNone => 0;
public static RelationComparisonResult RelationComparisonResultSucceeded => /* 1 << 0 */ 1;
public static RelationComparisonResult RelationComparisonResultFailed => /* 1 << 1 */ 2;
public static RelationComparisonResult RelationComparisonResultReportsUnmeasurable => /* 1 << 3 */ 8;
public static RelationComparisonResult RelationComparisonResultReportsUnreliable => /* 1 << 4 */ 16;
public static RelationComparisonResult RelationComparisonResultComplexityOverflow => /* 1 << 5 */ 32;
public static RelationComparisonResult RelationComparisonResultReportsMask => /* RelationComparisonResultReportsUnmeasurable | RelationComparisonResultReportsUnreliable */ 24;
public static RelationComparisonResult RelationComparisonResultOverflow => /* RelationComparisonResultComplexityOverflow */ 32;

partial struct DiagnosticAndArguments {
    internal ж<diagnostics.Message> message;
    internal slice<any> arguments;
}

partial struct ErrorOutputContainer {
    internal slice<ж<ast.Diagnostic>> errors;
    internal bool skipLogging;
}

// type ErrorReporter is a methodless func type — rendered inline as its base delegate

partial struct RecursionId {
    internal any value;
}

internal static RecursionId asRecursionId<T>(T value) {
    return new RecursionId(value: (value).OrTypedNilOf());
}

partial struct Relation {
    internal map<CacheHashKey, RelationComparisonResult> results;
}

} partial class checkerᴛRelation {

internal static RelationComparisonResult get(this ref Relation r, CacheHashKey key) {
    return r.results[key];
}

internal static void set(this ref Relation r, CacheHashKey key, RelationComparisonResult result) {
    if (r.results == nil) {
        r.results = new map<CacheHashKey, RelationComparisonResult>();
    }
    r.results[key] = result;
}

internal static nint size(this ref Relation r) {
    return len(r.results);
}

} partial class checkerᴛChecker {

internal static bool isTypeIdenticalTo(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.isTypeRelatedTo(source, target, c.identityRelation);
}

internal static Ternary compareTypesIdentical(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (Ꮡc.isTypeRelatedTo(source, target, c.identityRelation)) {
        return TernaryTrue;
    }
    return TernaryFalse;
}

internal static Ternary compareTypesAssignableSimple(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (Ꮡc.isTypeRelatedTo(source, target, c.assignableRelation)) {
        return TernaryTrue;
    }
    return TernaryFalse;
}

internal static Ternary compareTypesAssignableWorker(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, bool reportErrors) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (Ꮡc.isTypeRelatedTo(source, target, c.assignableRelation)) {
        return TernaryTrue;
    }
    return TernaryFalse;
}

internal static Ternary compareTypesSubtypeOf(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (Ꮡc.isTypeRelatedTo(source, target, c.subtypeRelation)) {
        return TernaryTrue;
    }
    return TernaryFalse;
}

internal static bool isTypeAssignableTo(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.isTypeRelatedTo(source, target, c.assignableRelation);
}

internal static bool isTypeSubtypeOf(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.isTypeRelatedTo(source, target, c.subtypeRelation);
}

internal static bool isTypeStrictSubtypeOf(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.isTypeRelatedTo(source, target, c.strictSubtypeRelation);
}

internal static bool isTypeComparableTo(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.isTypeRelatedTo(source, target, c.comparableRelation);
}

internal static bool areTypesComparable(this ж<Checker> Ꮡc, ж<Type> type1, ж<Type> type2) {
    return Ꮡc.isTypeComparableTo(type1, type2) || Ꮡc.isTypeComparableTo(type2, type1);
}

internal static bool isTypeRelatedTo(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<Relation> Ꮡrelation) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var relation = ref Ꮡrelation.DerefOrNull();

    if (isFreshLiteralType(Ꮡsource)) {
        Ꮡsource = source.AsLiteralType().regularType; source = ref Ꮡsource.DerefOrNull();
    }
    if (isFreshLiteralType(Ꮡtarget)) {
        Ꮡtarget = target.AsLiteralType().regularType; target = ref Ꮡtarget.DerefOrNull();
    }
    if (Ꮡsource == Ꮡtarget) {
        return true;
    }
    if (Ꮡrelation != c.identityRelation) {
        if (Ꮡrelation == c.comparableRelation && (TypeFlags)(target.flags & TypeFlagsNever) == 0 && Ꮡc.isSimpleTypeRelatedTo(Ꮡtarget, Ꮡsource, Ꮡrelation, default) || Ꮡc.isSimpleTypeRelatedTo(Ꮡsource, Ꮡtarget, Ꮡrelation, default)) {
            return true;
        }
    } else if (!((TypeFlags)(((TypeFlags)(source.flags | target.flags)) & ((TypeFlags)((TypeFlags)((TypeFlags)(TypeFlagsUnionOrIntersection | TypeFlagsIndexedAccess) | TypeFlagsConditional) | TypeFlagsSubstitution))) != 0)) {
        if (source.flags != target.flags) {
            return false;
        }
        if ((TypeFlags)(source.flags & TypeFlagsSingleton) != 0) {
            return true;
        }
    }
    if ((TypeFlags)(source.flags & TypeFlagsObject) != 0 && (TypeFlags)(target.flags & TypeFlagsObject) != 0) {
        var (id, _) = getRelationKey(Ꮡsource, Ꮡtarget, IntersectionStateNone, Ꮡrelation == c.identityRelation, false);
        var related = relation.get(id);
        if (related != RelationComparisonResultNone) {
            return (RelationComparisonResult)(related & RelationComparisonResultSucceeded) != 0;
        }
    }
    if ((TypeFlags)(source.flags & TypeFlagsStructuredOrInstantiable) != 0 || (TypeFlags)(target.flags & TypeFlagsStructuredOrInstantiable) != 0) {
        return Ꮡc.checkTypeRelatedTo(Ꮡsource, Ꮡtarget, Ꮡrelation, nil);
    }
    return false;
}

internal static bool isSimpleTypeRelatedTo(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<Relation> relation, Actionꓸꓸꓸ<ж<diagnostics.Message>, any> errorReporter) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var s = source.flags;
    var t = target.flags;
    if ((TypeFlags)(t & TypeFlagsAny) != 0 || (TypeFlags)(s & TypeFlagsNever) != 0 || Ꮡsource == c.wildcardType) {
        return true;
    }
    if ((TypeFlags)(t & TypeFlagsUnknown) != 0 && !(relation == c.strictSubtypeRelation && (TypeFlags)(s & TypeFlagsAny) != 0)) {
        return true;
    }
    if ((TypeFlags)(t & TypeFlagsNever) != 0) {
        return false;
    }
    if ((TypeFlags)(s & TypeFlagsStringLike) != 0 && (TypeFlags)(t & TypeFlagsString) != 0) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsStringLiteral) != 0 && (TypeFlags)(s & TypeFlagsEnumLiteral) != 0 && (TypeFlags)(t & TypeFlagsStringLiteral) != 0 && (TypeFlags)(t & TypeFlagsEnumLiteral) == 0 && AreEqual(source.AsLiteralType().value, target.AsLiteralType().value)) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsNumberLike) != 0 && (TypeFlags)(t & TypeFlagsNumber) != 0) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsNumberLiteral) != 0 && (TypeFlags)(s & TypeFlagsEnumLiteral) != 0 && (TypeFlags)(t & TypeFlagsNumberLiteral) != 0 && (TypeFlags)(t & TypeFlagsEnumLiteral) == 0 && AreEqual(source.AsLiteralType().value, target.AsLiteralType().value)) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsBigIntLike) != 0 && (TypeFlags)(t & TypeFlagsBigInt) != 0) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsBooleanLike) != 0 && (TypeFlags)(t & TypeFlagsBoolean) != 0) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsESSymbolLike) != 0 && (TypeFlags)(t & TypeFlagsESSymbol) != 0) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsEnum) != 0 && (TypeFlags)(t & TypeFlagsEnum) != 0 && source.symbol.Name == target.symbol.Name && Ꮡc.isEnumTypeRelatedTo(source.symbol, target.symbol, errorReporter)) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsEnumLiteral) != 0 && (TypeFlags)(t & TypeFlagsEnumLiteral) != 0) {
        if ((TypeFlags)(s & TypeFlagsUnion) != 0 && (TypeFlags)(t & TypeFlagsUnion) != 0 && Ꮡc.isEnumTypeRelatedTo(source.symbol, target.symbol, errorReporter)) {
            return true;
        }
        if ((TypeFlags)(s & TypeFlagsLiteral) != 0 && (TypeFlags)(t & TypeFlagsLiteral) != 0 && AreEqual(source.AsLiteralType().value, target.AsLiteralType().value) && Ꮡc.isEnumTypeRelatedTo(source.symbol, target.symbol, errorReporter)) {
            return true;
        }
    }
    if ((TypeFlags)(s & TypeFlagsUndefined) != 0 && (!c.strictNullChecks && (TypeFlags)(t & TypeFlagsUnionOrIntersection) == 0 || (TypeFlags)(t & ((TypeFlags)(TypeFlagsUndefined | TypeFlagsVoid))) != 0)) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsNull) != 0 && (!c.strictNullChecks && (TypeFlags)(t & TypeFlagsUnionOrIntersection) == 0 || (TypeFlags)(t & TypeFlagsNull) != 0)) {
        return true;
    }
    if ((TypeFlags)(s & TypeFlagsObject) != 0 && (TypeFlags)(t & TypeFlagsNonPrimitive) != 0 && !(relation == c.strictSubtypeRelation && Ꮡc.IsEmptyAnonymousObjectType(Ꮡsource) && (ObjectFlags)(source.objectFlags & ObjectFlagsFreshLiteral) == 0)) {
        return true;
    }
    if (relation == c.assignableRelation || relation == c.comparableRelation) {
        if ((TypeFlags)(s & TypeFlagsAny) != 0) {
            return true;
        }
        if ((TypeFlags)(s & TypeFlagsNumber) != 0 && ((TypeFlags)(t & TypeFlagsEnum) != 0 || (TypeFlags)(t & TypeFlagsNumberLiteral) != 0 && (TypeFlags)(t & TypeFlagsEnumLiteral) != 0)) {
            return true;
        }
        if ((TypeFlags)(s & TypeFlagsNumberLiteral) != 0 && (TypeFlags)(s & TypeFlagsEnumLiteral) == 0 && ((TypeFlags)(t & TypeFlagsEnum) != 0 || (TypeFlags)(t & TypeFlagsNumberLiteral) != 0 && (TypeFlags)(t & TypeFlagsEnumLiteral) != 0 && AreEqual(source.AsLiteralType().value, target.AsLiteralType().value))) {
            return true;
        }
        if (Ꮡc.isUnknownLikeUnionType(Ꮡtarget)) {
            return true;
        }
    }
    return false;
}

internal static bool isEnumTypeRelatedTo(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsource, ж<ast.Symbol> Ꮡtarget, Actionꓸꓸꓸ<ж<diagnostics.Message>, any> errorReporter) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var sourceSymbol = core.IfElse((ast.SymbolFlags)(source.Flags & ast.SymbolFlagsEnumMember) != 0, Ꮡc.getParentOfSymbol(Ꮡsource), Ꮡsource);
    var targetSymbol = core.IfElse((ast.SymbolFlags)(target.Flags & ast.SymbolFlagsEnumMember) != 0, Ꮡc.getParentOfSymbol(Ꮡtarget), Ꮡtarget);
    if (sourceSymbol == targetSymbol) {
        return true;
    }
    if (sourceSymbol.Name != targetSymbol.Name || (ast.SymbolFlags)(sourceSymbol.Flags & ast.SymbolFlagsRegularEnum) == 0 || (ast.SymbolFlags)(targetSymbol.Flags & ast.SymbolFlagsRegularEnum) == 0) {
        return false;
    }
    var key = new EnumRelationKey(sourceId: ast.GetSymbolId(sourceSymbol), targetId: ast.GetSymbolId(targetSymbol));
    var entry = c.enumRelation[key]; if (entry != RelationComparisonResultNone && !((RelationComparisonResult)(entry & RelationComparisonResultFailed) != 0 && errorReporter != nil)) {
        return (RelationComparisonResult)(entry & RelationComparisonResultSucceeded) != 0;
    }
    var targetEnumType = Ꮡc.getTypeOfSymbol(targetSymbol);
    foreach (var (_, sourceProperty) in range(Ꮡc.getPropertiesOfType(Ꮡc.getTypeOfSymbol(sourceSymbol)))) {
        if ((ast.SymbolFlags)(sourceProperty.Flags & ast.SymbolFlagsEnumMember) != 0) {
            var targetProperty = Ꮡc.getPropertyOfType(targetEnumType, sourceProperty.Name);
            if (targetProperty == nil || (ast.SymbolFlags)(targetProperty.Flags & ast.SymbolFlagsEnumMember) == 0) {
                if (errorReporter != nil) {
                    errorReporter(diagnostics.Property_0_is_missing_in_type_1, Ꮡc.symbolToString(sourceProperty), Ꮡc.TypeToStringEx(Ꮡc.getDeclaredTypeOfSymbol(targetSymbol), nil, TypeFormatFlagsUseFullyQualifiedType, nil));
                }
                c.enumRelation[key] = RelationComparisonResultFailed;
                return false;
            }
            var sourceValue = Ꮡc.getEnumMemberValue(ast.GetDeclarationOfKind(sourceProperty, ast.KindEnumMember)).Value;
            var targetValue = Ꮡc.getEnumMemberValue(ast.GetDeclarationOfKind(targetProperty, ast.KindEnumMember)).Value;
            if (!AreEqual(sourceValue, targetValue)) {
                if (sourceValue != nil && targetValue != nil) {
                    if (errorReporter != nil) {
                        errorReporter(diagnostics.Each_declaration_of_0_1_differs_in_its_value_where_2_was_expected_but_3_was_given, Ꮡc.symbolToString(targetSymbol), Ꮡc.symbolToString(targetProperty), c.valueToString(targetValue), c.valueToString(sourceValue));
                    }
                    c.enumRelation[key] = RelationComparisonResultFailed;
                    return false;
                }
                var (_, sourceIsString) = sourceValue._<@string>(ᐧ);
                var (_, targetIsString) = targetValue._<@string>(ᐧ);
                if (sourceIsString || targetIsString) {
                    if (errorReporter != nil) {
                        var knownStringValue = core.OrElse(sourceValue, targetValue);
                        errorReporter(diagnostics.One_value_of_0_1_is_the_string_2_and_the_other_is_assumed_to_be_an_unknown_numeric_value, Ꮡc.symbolToString(targetSymbol), Ꮡc.symbolToString(targetProperty), c.valueToString(knownStringValue));
                    }
                    c.enumRelation[key] = RelationComparisonResultFailed;
                    return false;
                }
            }
        }
    }
    c.enumRelation[key] = RelationComparisonResultSucceeded;
    return true;
}

internal static bool checkTypeAssignableTo(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, ж<ast.Node> errorNode, ж<diagnostics.Message> headMessage) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.checkTypeRelatedToEx(source, target, c.assignableRelation, errorNode, headMessage, nil);
}

internal static bool checkTypeAssignableToEx(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, ж<ast.Node> errorNode, ж<diagnostics.Message> headMessage, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.checkTypeRelatedToEx(source, target, c.assignableRelation, errorNode, headMessage, diagnosticOutput);
}

internal static bool checkTypeComparableTo(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, ж<ast.Node> errorNode, ж<diagnostics.Message> headMessage) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.checkTypeRelatedToEx(source, target, c.comparableRelation, errorNode, headMessage, nil);
}

internal static bool checkTypeRelatedTo(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, ж<Relation> relation, ж<ast.Node> errorNode) {
    return Ꮡc.checkTypeRelatedToEx(source, target, relation, errorNode, nil, nil);
}

internal static bool checkTypeRelatedToEx(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<Relation> Ꮡrelation, ж<ast.Node> ᏑerrorNode, ж<diagnostics.Message> headMessage, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var relation = ref Ꮡrelation.DerefOrNull();
    ref var errorNode = ref ᏑerrorNode.DerefOrNull();

    var r = Ꮡc.getRelater();
    r.relation = Ꮡrelation;
    r.errorNode = ᏑerrorNode;
    r.relationCount = (16_000_000 - relation.size()) / 8;
    var result = r.isRelatedToEx(Ꮡsource, Ꮡtarget, RecursionFlagsBoth, ᏑerrorNode != nil, headMessage, IntersectionStateNone);
    if (r.overflow) {
        var (id, _) = getRelationKey(Ꮡsource, Ꮡtarget, IntersectionStateNone, Ꮡrelation == c.identityRelation, false);
        relation.set(id, (RelationComparisonResult)(RelationComparisonResultFailed | RelationComparisonResultComplexityOverflow));
        var tr = c.tracer; if (tr != nil) {
            tr.Instant(tracing.PhaseCheckTypes, "checkTypeRelatedTo_DepthLimit", new map<@string, any>{["sourceId"] = source.id, ["targetId"] = target.id, ["depth"] = len(r.sourceStack), ["targetDepth"] = len(r.targetStack)});
        }
        if (ᏑerrorNode == nil) {
            ᏑerrorNode = c.currentNode; errorNode = ref ᏑerrorNode.DerefOrNull();
        }
        Ꮡc.reportDiagnostic(NewDiagnosticForNode(ᏑerrorNode, diagnostics.Excessive_complexity_comparing_types_0_and_1, Ꮡc.TypeToString(Ꮡsource), Ꮡc.TypeToString(Ꮡtarget)), diagnosticOutput);
    } else if (r.errorChain != nil) {
        if (headMessage != nil && ᏑerrorNode != nil && result == TernaryFalse && source.symbol != nil && c.exportTypeLinks.Has(source.symbol)) {
            var links = c.exportTypeLinks.Get(source.symbol);
            if (links.originatingImport != nil && !ast.IsImportCall(links.originatingImport)) {
                var helpfulRetry = Ꮡc.checkTypeRelatedTo(Ꮡc.getTypeOfSymbol(links.target), Ꮡtarget, Ꮡrelation, nil);
                if (helpfulRetry) {
                    r.relatedInfo = append(r.relatedInfo, createDiagnosticForNode(links.originatingImport, diagnostics.Type_originates_at_this_import_A_namespace_style_import_cannot_be_called_or_constructed_and_will_cause_a_failure_at_runtime_Consider_using_a_default_import_or_import_require_here_instead));
                }
            }
        }
        Ꮡc.reportDiagnostic(createDiagnosticChainFromErrorChain(r.errorChain, r.errorNode, r.relatedInfo), diagnosticOutput);
    }
    Ꮡc.putRelater(r);
    return result != TernaryFalse;
}

} partial class checker_package {

internal static ж<ast.Diagnostic> createDiagnosticChainFromErrorChain(ж<ErrorChain> Ꮡchain, ж<ast.Node> errorNode, slice<ж<ast.Diagnostic>> relatedInfo) {
    ref var chain = ref Ꮡchain.DerefOrNull();

    while (Ꮡchain != nil && chain.message.ElidedInCompatibilityPyramid()) {
        Ꮡchain = chain.next; chain = ref Ꮡchain.DerefOrNull();
    }
    if (Ꮡchain == nil) {
        return default;
    }
    var next = createDiagnosticChainFromErrorChain(chain.next, errorNode, relatedInfo);
    if (next == nil) {
        return NewDiagnosticForNode(errorNode, chain.message, chain.args.ꓸꓸꓸ).SetRelatedInfo(relatedInfo);
    }
    return ast.NewDiagnosticChain(next, chain.message, chain.args.ꓸꓸꓸ);
}

} partial class checkerᴛChecker {

internal static void reportDiagnostic(this ж<Checker> Ꮡc, ж<ast.Diagnostic> diagnostic, ж<slice<ж<ast.Diagnostic>>> ᏑdiagnosticOutput) {
    ref var diagnosticOutput = ref ᏑdiagnosticOutput.DerefOrNull();

    if (diagnostic != nil) {
        if (ᏑdiagnosticOutput != nil) {
            diagnosticOutput = append(diagnosticOutput, diagnostic);
        } else {
            Ꮡc.addDiagnostic(diagnostic);
        }
    }
}

internal static bool checkTypeAssignableToAndOptionallyElaborate(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, ж<ast.Node> errorNode, ж<ast.Node> expr, ж<diagnostics.Message> headMessage, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.checkTypeRelatedToAndOptionallyElaborate(source, target, c.assignableRelation, errorNode, expr, headMessage, diagnosticOutput);
}

internal static bool checkTypeRelatedToAndOptionallyElaborate(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, ж<Relation> relation, ж<ast.Node> errorNode, ж<ast.Node> expr, ж<diagnostics.Message> headMessage, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    if (Ꮡc.isTypeRelatedTo(source, target, relation)) {
        return true;
    }
    if (errorNode != nil && !Ꮡc.elaborateError(expr, source, target, relation, headMessage, diagnosticOutput)) {
        return Ꮡc.checkTypeRelatedToEx(source, target, relation, errorNode, headMessage, diagnosticOutput);
    }
    return false;
}

internal static bool elaborateError(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> source, ж<Type> target, ж<Relation> relation, ж<diagnostics.Message> headMessage, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil || Ꮡc.isOrHasGenericConditional(target)) {
        return false;
    }
    if (c.compilerOptions.NoCheck.IsTrue()) {
        return false;
    }
    if (Ꮡc.elaborateDidYouMeanToCallOrConstruct(Ꮡnode, source, target, relation, SignatureKindConstruct, headMessage, diagnosticOutput) || Ꮡc.elaborateDidYouMeanToCallOrConstruct(Ꮡnode, source, target, relation, SignatureKindCall, headMessage, diagnosticOutput)) {
        return true;
    }
    var exprᴛ1 = node.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindAsExpression) { matchᴛ1 = true;
        do {
            if (!ast.IsConstAssertion(Ꮡnode)) {
                break;
            }
            fallthrough = true;
        } while (false);
    }
    if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindJsxExpression || exprᴛ1 == ast.KindParenthesizedExpression)) { matchᴛ1 = true;
        return Ꮡc.elaborateError(node.Expression(), source, target, relation, headMessage, diagnosticOutput);
    }
    else if (exprᴛ1 == ast.KindBinaryExpression) { matchᴛ1 = true;
        switch (node.AsBinaryExpression().OperatorToken.Kind) {
        case var ᴛ when ᴛ == ast.KindEqualsToken || ᴛ == ast.KindCommaToken: {
            return Ꮡc.elaborateError(node.AsBinaryExpression().Right, source, target, relation, headMessage, diagnosticOutput);
        }}

    }
    else if (exprᴛ1 == ast.KindObjectLiteralExpression) { matchᴛ1 = true;
        return Ꮡc.elaborateObjectLiteral(Ꮡnode, source, target, relation, diagnosticOutput);
    }
    else if (exprᴛ1 == ast.KindArrayLiteralExpression) { matchᴛ1 = true;
        return Ꮡc.elaborateArrayLiteral(Ꮡnode, source, target, relation, diagnosticOutput);
    }
    else if (exprᴛ1 == ast.KindArrowFunction) {
        return Ꮡc.elaborateArrowFunction(Ꮡnode, source, target, relation, diagnosticOutput);
    }
    else if (exprᴛ1 == ast.KindJsxAttributes) { matchᴛ1 = true;
        return Ꮡc.elaborateJsxComponents(Ꮡnode, source, target, relation, diagnosticOutput);
    }

    return false;
}

internal static bool isOrHasGenericConditional(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (TypeFlags)(t.flags & TypeFlagsConditional) != 0 || ((TypeFlags)(t.flags & TypeFlagsIntersection) != 0 && core.Some<ж<Type>>(t.Types(), Ꮡc.isOrHasGenericConditional));
}

internal static bool elaborateDidYouMeanToCallOrConstruct(this ж<Checker> Ꮡc, ж<ast.Node> node, ж<Type> source, ж<Type> target, ж<Relation> relation, SignatureKind kind, ж<diagnostics.Message> headMessage, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    if (core.Some(Ꮡc.getSignaturesOfType(source, kind), (ж<Signature> s) => {
        var returnType = Ꮡc.getReturnTypeOfSignature(s);
        return (TypeFlags)(returnType.flags & ((TypeFlags)(TypeFlagsAny | TypeFlagsNever))) == 0 && Ꮡc.checkTypeRelatedTo(returnType, target, relation, nil);
    })) {
        ref var diags = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡdiags);
        if (!Ꮡc.checkTypeRelatedToEx(source, target, relation, node, headMessage, Ꮡdiags)) {
            var diagnostic = diags[0];
            var message = core.IfElse(kind == SignatureKindConstruct,
                diagnostics.Did_you_mean_to_use_new_with_this_expression,
                diagnostics.Did_you_mean_to_call_this_expression);
            Ꮡc.reportDiagnostic(diagnostic.AddRelatedInfo(createDiagnosticForNode(node, message)), diagnosticOutput);
            return true;
        }
    }
    return false;
}

internal static bool elaborateObjectLiteral(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> source, ж<Type> Ꮡtarget, ж<Relation> relation, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(target.flags & ((TypeFlags)(TypeFlagsPrimitive | TypeFlagsNever))) != 0) {
        return false;
    }
    var reportedError = false;
    foreach (var (_, prop) in range(node.Properties())) {
        if (ast.IsSpreadAssignment(prop)) {
            continue;
        }
        var nameType = Ꮡc.getLiteralTypeFromProperty(Ꮡc.getSymbolOfDeclaration(prop), TypeFlagsStringOrNumberLiteralOrUnique, false);
        if (nameType == nil || (TypeFlags)(nameType.flags & TypeFlagsNever) != 0) {
            continue;
        }
        switch (prop.Kind) {
        case var ᴛ when ᴛ == ast.KindSetAccessor || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindShorthandPropertyAssignment: {
            reportedError = Ꮡc.elaborateElement(source, Ꮡtarget, relation, prop.Name(), nil, nameType, nil, default, diagnosticOutput) || reportedError;
            break;
        }
        case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
            var message = core.IfElse(ast.IsComputedNonLiteralName(prop.Name()), diagnostics.Type_of_computed_property_s_value_is_0_which_is_not_assignable_to_type_1, (ж<diagnostics.Message>)(nil));
            reportedError = Ꮡc.elaborateElement(source, Ꮡtarget, relation, prop.Name(), prop.Initializer(), nameType, message, default, diagnosticOutput) || reportedError;
            break;
        }}

    }
    return reportedError;
}

internal static bool elaborateArrayLiteral(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<Relation> relation, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(target.flags & ((TypeFlags)(TypeFlagsPrimitive | TypeFlagsNever))) != 0) {
        return false;
    }
    if (!Ꮡc.isTupleLikeType(Ꮡsource)) {
        c.pushContextualType(Ꮡnode, Ꮡtarget, false);
        Ꮡsource = Ꮡc.checkArrayLiteral(Ꮡnode, (CheckMode)(CheckModeContextual | CheckModeForceTuple)); source = ref Ꮡsource.DerefOrNull();
        c.popContextualType();
        if (!Ꮡc.isTupleLikeType(Ꮡsource)) {
            return false;
        }
    }
    var reportedError = false;
    foreach (var (i, element) in range(node.Elements())) {
        if (ast.IsOmittedExpression(element) || Ꮡc.isTupleLikeType(Ꮡtarget) && Ꮡc.getPropertyOfType(Ꮡtarget, ((jsnum.Number)(float64)i).String()) == nil) {
            continue;
        }
        var nameType = Ꮡc.getNumberLiteralType((jsnum.Number)(float64)i);
        var checkNode = c.getEffectiveCheckNode(element);
        reportedError = Ꮡc.elaborateElement(Ꮡsource, Ꮡtarget, relation, checkNode, checkNode, nameType, nil, default, diagnosticOutput) || reportedError;
    }
    return reportedError;
}

internal static bool elaborateElement(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> Ꮡtarget, ж<Relation> relation, ж<ast.Node> prop, ж<ast.Node> next, ж<Type> ᏑnameType, ж<diagnostics.Message> errorMessage, Func<ж<ast.Node>, ж<ast.Diagnostic>> diagnosticFactory, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var nameType = ref ᏑnameType.DerefOrNull();

    var targetPropType = Ꮡc.getBestMatchIndexedAccessTypeOrUndefined(source, Ꮡtarget, ᏑnameType);
    if (targetPropType == nil || (TypeFlags)(targetPropType.flags & TypeFlagsIndexedAccess) != 0) {
        return false;
    }
    var sourcePropType = Ꮡc.getIndexedAccessTypeOrUndefined(source, ᏑnameType, AccessFlagsNone, nil, nil);
    if (sourcePropType == nil || Ꮡc.checkTypeRelatedTo(sourcePropType, targetPropType, relation, nil)) {
        return false;
    }
    if (next != nil && Ꮡc.elaborateError(next, sourcePropType, targetPropType, relation, nil, diagnosticOutput)) {
        return true;
    }
    ref var diags = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡdiags);
    var specificSource = sourcePropType;
    if (next != nil) {
        specificSource = Ꮡc.checkExpressionForMutableLocationWithContextualType(next, sourcePropType);
    }
    if (diagnosticFactory != nil) {
        diags = append(diags, diagnosticFactory(prop));
    } else if (c.exactOptionalPropertyTypes && c.isExactOptionalPropertyMismatch(specificSource, targetPropType)) {
        diags = append(diags, createDiagnosticForNode(prop, diagnostics.Type_0_is_not_assignable_to_type_1_with_exactOptionalPropertyTypes_Colon_true_Consider_adding_undefined_to_the_type_of_the_target, Ꮡc.TypeToString(specificSource), Ꮡc.TypeToString(targetPropType)));
    } else {
        @string propName = c.getPropertyNameFromIndex(ᏑnameType, nil);
        var targetIsOptional = (ast.SymbolFlags)(core.OrElse(Ꮡc.getPropertyOfType(Ꮡtarget, propName), c.unknownSymbol).Flags & ast.SymbolFlagsOptional) != 0;
        var sourceIsOptional = (ast.SymbolFlags)(core.OrElse(Ꮡc.getPropertyOfType(source, propName), c.unknownSymbol).Flags & ast.SymbolFlagsOptional) != 0;
        targetPropType = Ꮡc.removeMissingType(targetPropType, targetIsOptional);
        sourcePropType = Ꮡc.removeMissingType(sourcePropType, targetIsOptional && sourceIsOptional);
        var result = Ꮡc.checkTypeRelatedToEx(specificSource, targetPropType, relation, prop, errorMessage, Ꮡdiags);
        if (result && specificSource != sourcePropType) {
            Ꮡc.checkTypeRelatedToEx(sourcePropType, targetPropType, relation, prop, errorMessage, Ꮡdiags);
        }
    }
    if (len(diags) == 0) {
        return false;
    }
    var diagnostic = diags[0];
    @string propertyName = default;
    ж<ast.Symbol> targetProp = default;
    if (isTypeUsableAsPropertyName(ref (ᏑnameType).DerefOrNull())) {
        propertyName = getPropertyNameFromType(ᏑnameType);
        targetProp = Ꮡc.getPropertyOfType(Ꮡtarget, propertyName);
    }
    var issuedElaboration = false;
    if (targetProp == nil) {
        var indexInfo = Ꮡc.getApplicableIndexInfo(Ꮡtarget, ᏑnameType);
        if (indexInfo != nil && indexInfo.declaration != nil && !c.program.IsSourceFileDefaultLibrary(ast.GetSourceFileOfNode(indexInfo.declaration).Path())) {
            issuedElaboration = true;
            diagnostic.AddRelatedInfo(createDiagnosticForNode(indexInfo.declaration, diagnostics.The_expected_type_comes_from_this_index_signature));
        }
    }
    if (!issuedElaboration && (targetProp != nil && len(targetProp.Declarations) != 0 || target.symbol != nil && len(target.symbol.Declarations) != 0)) {
        ж<ast.Node> targetNode = default;
        if (targetProp != nil && len(targetProp.Declarations) != 0) {
            targetNode = targetProp.Declarations[0];
        } else {
            targetNode = target.symbol.Declarations[0];
        }
        if (propertyName == ""u8 || (TypeFlags)(nameType.flags & TypeFlagsUniqueESSymbol) != 0) {
            propertyName = Ꮡc.TypeToString(ᏑnameType);
        }
        if (!c.program.IsSourceFileDefaultLibrary(ast.GetSourceFileOfNode(targetNode).Path())) {
            diagnostic.AddRelatedInfo(createDiagnosticForNode(targetNode, diagnostics.The_expected_type_comes_from_property_0_which_is_declared_here_on_type_1, propertyName, Ꮡc.TypeToString(Ꮡtarget)));
        }
    }
    Ꮡc.reportDiagnostic(diagnostic, diagnosticOutput);
    return true;
}

internal static ж<Type> getBestMatchIndexedAccessTypeOrUndefined(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> Ꮡtarget, ж<Type> nameType) {
    ref var target = ref Ꮡtarget.DerefOrNull();

    var idx = Ꮡc.getIndexedAccessTypeOrUndefined(Ꮡtarget, nameType, AccessFlagsNone, nil, nil);
    if (idx != nil) {
        return idx;
    }
    if ((TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
        var best = Ꮡc.getBestMatchingType(source, Ꮡtarget, Ꮡc.compareTypesAssignableSimple);
        if (best != nil) {
            return Ꮡc.getIndexedAccessTypeOrUndefined(best, nameType, AccessFlagsNone, nil, nil);
        }
    }
    return default;
}

internal static ж<Type> checkExpressionForMutableLocationWithContextualType(this ж<Checker> Ꮡc, ж<ast.Node> next, ж<Type> sourcePropType) {
    ref var c = ref Ꮡc.DerefOrNull();

    c.pushContextualType(next, sourcePropType, false);
    var result = Ꮡc.checkExpressionForMutableLocation(next, CheckModeContextual);
    c.popContextualType();
    return result;
}

internal static bool elaborateArrowFunction(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> source, ж<Type> Ꮡtarget, ж<Relation> relation, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if (ast.IsBlock(node.Body()) || core.Some<ж<astꓸParameterDeclarationNode>>(node.Parameters(), hasType)) {
        return false;
    }
    var sourceSig = Ꮡc.getSingleCallSignature(source);
    if (sourceSig == nil) {
        return false;
    }
    var targetSignatures = Ꮡc.getSignaturesOfType(Ꮡtarget, SignatureKindCall);
    if (len(targetSignatures) == 0) {
        return false;
    }
    var returnExpression = node.Body();
    var sourceReturn = Ꮡc.getReturnTypeOfSignature(sourceSig);
    var targetReturn = Ꮡc.getUnionType(core.Map<ж<Signature>, ж<Type>>(targetSignatures, Ꮡc.getReturnTypeOfSignature));
    if (Ꮡc.checkTypeRelatedTo(sourceReturn, targetReturn, relation, nil)) {
        return false;
    }
    if (returnExpression != nil && Ꮡc.elaborateError(returnExpression, sourceReturn, targetReturn, relation, nil, diagnosticOutput)) {
        return true;
    }
    ref var diags = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡdiags);
    Ꮡc.checkTypeRelatedToEx(sourceReturn, targetReturn, relation, returnExpression, nil, Ꮡdiags);
    if (len(diags) != 0) {
        var diagnostic = diags[0];
        if (target.symbol != nil && len(target.symbol.Declarations) != 0) {
            diagnostic.AddRelatedInfo(createDiagnosticForNode(target.symbol.Declarations[0], diagnostics.The_expected_type_comes_from_the_return_type_of_this_signature));
        }
        if ((ast.FunctionFlags)(ast.GetFunctionFlags(Ꮡnode) & ast.FunctionFlagsAsync) == 0 && Ꮡc.getTypeOfPropertyOfType(sourceReturn, "then") == nil && Ꮡc.checkTypeRelatedTo(Ꮡc.createPromiseType(sourceReturn), targetReturn, relation, nil)) {
            diagnostic.AddRelatedInfo(createDiagnosticForNode(Ꮡnode, diagnostics.Did_you_mean_to_mark_this_function_as_async));
        }
        Ꮡc.reportDiagnostic(diagnostic, diagnosticOutput);
        return true;
    }
    return false;
}

internal static bool isWeakType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsObject) != 0) {
        var resolved = Ꮡc.resolveStructuredTypeMembers(Ꮡt);
        return len(resolved.signatures) == 0 && len(resolved.indexInfos) == 0 && len(resolved.properties) > 0 && core.Every(resolved.properties, (ж<ast.Symbol> p) => (ast.SymbolFlags)(p.Flags & ast.SymbolFlagsOptional) != 0);
    }
    if ((TypeFlags)(t.flags & TypeFlagsSubstitution) != 0) {
        return Ꮡc.isWeakType(t.AsSubstitutionType().baseType);
    }
    if ((TypeFlags)(t.flags & TypeFlagsIntersection) != 0) {
        return core.Every<ж<Type>>(t.Types(), Ꮡc.isWeakType);
    }
    return false;
}

internal static bool hasCommonProperties(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, bool isComparingJsxAttributes) {
    foreach (var (_, prop) in range(Ꮡc.getPropertiesOfType(source))) {
        if (Ꮡc.isKnownProperty(target, prop.Name, isComparingJsxAttributes)) {
            return true;
        }
    }
    return false;
}

internal static bool isKnownProperty(this ж<Checker> Ꮡc, ж<Type> ᏑtargetType, @string name, bool isComparingJsxAttributes) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var targetType = ref ᏑtargetType.DerefOrNull();

    if ((TypeFlags)(targetType.flags & TypeFlagsObject) != 0) {
        if (Ꮡc.getPropertyOfObjectType(ᏑtargetType, name) != nil || Ꮡc.getApplicableIndexInfoForName(ᏑtargetType, name) != nil || isLateBoundName(name) && Ꮡc.getIndexInfoOfType(ᏑtargetType, c.stringType) != nil || isComparingJsxAttributes && isHyphenatedJsxName(name)) {
            return true;
        }
    }
    if ((TypeFlags)(targetType.flags & TypeFlagsSubstitution) != 0) {
        return Ꮡc.isKnownProperty(targetType.AsSubstitutionType().baseType, name, isComparingJsxAttributes);
    }
    if ((TypeFlags)(targetType.flags & TypeFlagsUnionOrIntersection) != 0 && isExcessPropertyCheckTarget(ᏑtargetType)) {
        foreach (var (_, t) in range(targetType.Types())) {
            if (Ꮡc.isKnownProperty(t, name, isComparingJsxAttributes)) {
                return true;
            }
        }
    }
    return false;
}

} partial class checker_package {

internal static bool isHyphenatedJsxName(@string name) {
    return strings.Contains(name, "-");
}

internal static bool isExcessPropertyCheckTarget(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (TypeFlags)(t.flags & TypeFlagsObject) != 0 && (ObjectFlags)(t.objectFlags & ObjectFlagsObjectLiteralPatternWithComputedProperties) == 0 || (TypeFlags)(t.flags & TypeFlagsNonPrimitive) != 0 || (TypeFlags)(t.flags & TypeFlagsSubstitution) != 0 && isExcessPropertyCheckTarget(t.AsSubstitutionType().baseType) || (TypeFlags)(t.flags & TypeFlagsUnion) != 0 && core.Some<ж<Type>>(t.Types(), isExcessPropertyCheckTarget) || (TypeFlags)(t.flags & TypeFlagsIntersection) != 0 && core.Every<ж<Type>>(t.Types(), isExcessPropertyCheckTarget);
}

} partial class checkerᴛChecker {

internal static bool isDeeplyNestedType(this ref Checker c, ж<Type> t, slice<ж<Type>> stack, nint maxDepth) {
    if (len(stack) >= maxDepth) {
        var target = getRecursionIdentityTarget(t);
        if ((TypeFlags)(target.flags & TypeFlagsIntersection) != 0) {
            foreach (var (_, tΔ1) in range(target.Types())) {
                if (c.isDeeplyNestedType(tΔ1, stack, maxDepth)) {
                    return true;
                }
            }
        } else {
            var identity = getRecursionIdentityFromTarget(target);
            nint count = 0;
            var lastTypeId = (TypeId)0;
            foreach (var (_, tΔ2) in range(stack)) {
                if (hasMatchingRecursionIdentity(tΔ2, identity)) {
                    if (tΔ2.id >= lastTypeId) {
                        count++;
                        if (count >= maxDepth) {
                            return true;
                        }
                    }
                    lastTypeId = tΔ2.id;
                }
            }
        }
    }
    return false;
}

} partial class checker_package {

internal static bool hasMatchingRecursionIdentity(ж<Type> t, RecursionId identity) {
    var target = getRecursionIdentityTarget(t);
    if ((TypeFlags)(target.flags & TypeFlagsIntersection) != 0) {
        foreach (var (_, tΔ1) in range(target.Types())) {
            if (hasMatchingRecursionIdentity(tΔ1, identity)) {
                return true;
            }
        }
        return false;
    }
    return getRecursionIdentityFromTarget(target) == identity;
}

internal static RecursionId getRecursionIdentity(ж<Type> t) {
    return getRecursionIdentityFromTarget(getRecursionIdentityTarget(t));
}

internal static ж<Type> getRecursionIdentityTarget(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsIndexedAccess) != 0) {
        return getRecursionIdentityTarget(t.AsIndexedAccessType().objectType);
    }
    if ((ObjectFlags)(t.objectFlags & ObjectFlagsInstantiatedMapped) == ObjectFlagsInstantiatedMapped) {
        var target = t.checker.getModifiersTypeFromMappedType(Ꮡt);
        if (target != nil && (target.symbol != nil || (TypeFlags)(target.flags & TypeFlagsIntersection) != 0 && core.Some(target.Types(), (ж<Type> tΔ1) => tΔ1.symbol != nil))) {
            return getRecursionIdentityTarget(target);
        }
    }
    return Ꮡt;
}

internal static RecursionId getRecursionIdentityFromTarget(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsObject) != 0 && !isObjectOrArrayLiteralType(Ꮡt)) {
        if ((ObjectFlags)(t.objectFlags & ObjectFlagsReference) != 0 && t.AsTypeReference().node != nil) {
            return asRecursionId(t.AsTypeReference().node);
        }
        if (t.symbol != nil && !((ObjectFlags)(t.objectFlags & ObjectFlagsAnonymous) != 0 && (ast.SymbolFlags)(t.symbol.Flags & ast.SymbolFlagsClass) != 0) && (ObjectFlags)(t.objectFlags & (uint32)ObjectFlagsFromTypeNode) == 0) {
            return asRecursionId(t.symbol);
        }
        if (isTupleType(Ꮡt) && (ObjectFlags)(t.objectFlags & (uint32)ObjectFlagsFromTypeNode) == 0) {
            return asRecursionId(t.Target());
        }
    }
    if ((TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0 && t.symbol != nil) {
        return asRecursionId(t.symbol);
    }
    if ((TypeFlags)(t.flags & TypeFlagsConditional) != 0) {
        return asRecursionId(t.AsConditionalType().root.node.of(ast.ConditionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    return asRecursionId(Ꮡt);
}

} partial class checkerᴛChecker {

internal static ж<Type> getBestMatchingType(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, Func<ж<Type>, ж<Type>, Ternary> isRelatedTo) {
    ref var c = ref Ꮡc.DerefOrNull();

    {
        var t = Ꮡc.findMatchingDiscriminantType(source, target, isRelatedTo); if (t != nil) {
            return t;
        }
    }
    {
        var t = c.findMatchingTypeReferenceOrTypeAliasReference(source, target); if (t != nil) {
            return t;
        }
    }
    {
        var t = Ꮡc.findBestTypeForObjectLiteral(source, target); if (t != nil) {
            return t;
        }
    }
    {
        var t = Ꮡc.findBestTypeForInvokable(source, target, SignatureKindCall); if (t != nil) {
            return t;
        }
    }
    {
        var t = Ꮡc.findBestTypeForInvokable(source, target, SignatureKindConstruct); if (t != nil) {
            return t;
        }
    }
    return Ꮡc.findMostOverlappyType(source, target);
}

internal static ж<Type> findMatchingTypeReferenceOrTypeAliasReference(this ref Checker c, ж<Type> Ꮡsource, ж<Type> ᏑunionTarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var unionTarget = ref ᏑunionTarget.DerefOrNull();

    var sourceObjectFlags = source.objectFlags;
    if ((ObjectFlags)(sourceObjectFlags & ((ObjectFlags)(ObjectFlagsReference | ObjectFlagsAnonymous))) != 0 && (TypeFlags)(unionTarget.flags & TypeFlagsUnion) != 0) {
        foreach (var (_, target) in range(unionTarget.Types())) {
            if ((TypeFlags)(target.flags & TypeFlagsObject) != 0) {
                var overlapObjFlags = (ObjectFlags)(sourceObjectFlags & target.objectFlags);
                if ((ObjectFlags)(overlapObjFlags & ObjectFlagsReference) != 0 && source.Target() == target.Target()) {
                    return target;
                }
                if ((ObjectFlags)(overlapObjFlags & ObjectFlagsAnonymous) != 0 && source.alias != nil && target.alias != nil && source.alias.symbol == target.alias.symbol) {
                    return target;
                }
            }
        }
    }
    return default;
}

internal static ж<Type> findBestTypeForInvokable(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> ᏑunionTarget, SignatureKind kind) {
    ref var unionTarget = ref ᏑunionTarget.DerefOrNull();

    if (len(Ꮡc.getSignaturesOfType(source, kind)) != 0) {
        return core.Find(unionTarget.Types(), (ж<Type> t) => len(Ꮡc.getSignaturesOfType(t, kind)) != 0);
    }
    return default;
}

internal static ж<Type> findMostOverlappyType(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> ᏑunionTarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var unionTarget = ref ᏑunionTarget.DerefOrNull();

    ж<Type> bestMatch = default;
    if ((TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsPrimitive | TypeFlagsInstantiablePrimitive))) == 0) {
        nint matchingCount = 0;
        foreach (var (_, target) in range(unionTarget.Types())) {
            if ((TypeFlags)(target.flags & ((TypeFlags)(TypeFlagsPrimitive | TypeFlagsInstantiablePrimitive))) == 0) {
                var overlap = Ꮡc.getIntersectionType(slice<ж<Type>>([Ꮡc.getIndexType(Ꮡsource), Ꮡc.getIndexType(target)]));
                if ((TypeFlags)(overlap.flags & TypeFlagsIndex) != 0) {
                    return target;
                } else if (isUnitType(overlap) || (TypeFlags)(overlap.flags & TypeFlagsUnion) != 0) {
                    nint length = 1;
                    if ((TypeFlags)(overlap.flags & TypeFlagsUnion) != 0) {
                        length = core.CountWhere<ж<Type>>(overlap.Types(), isUnitType);
                    }
                    if (length >= matchingCount) {
                        bestMatch = target;
                        matchingCount = length;
                    }
                }
            }
        }
    }
    return bestMatch;
}

internal static ж<Type> findBestTypeForObjectLiteral(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> ᏑunionTarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var unionTarget = ref ᏑunionTarget.DerefOrNull();

    if ((ObjectFlags)(source.objectFlags & ObjectFlagsObjectLiteral) != 0 && someType(ᏑunionTarget, Ꮡc.isArrayLikeType)) {
        return core.Find(unionTarget.Types(), (ж<Type> t) => !Ꮡc.isArrayLikeType(t));
    }
    return default;
}

internal static bool shouldReportUnmatchedPropertyError(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    var typeCallSignatures = Ꮡc.getSignaturesOfStructuredType(source, SignatureKindCall);
    var typeConstructSignatures = Ꮡc.getSignaturesOfStructuredType(source, SignatureKindConstruct);
    var typeProperties = Ꮡc.getPropertiesOfObjectType(source);
    if ((len(typeCallSignatures) != 0 || len(typeConstructSignatures) != 0) && len(typeProperties) == 0) {
        if ((len(Ꮡc.getSignaturesOfType(target, SignatureKindCall)) != 0 && len(typeCallSignatures) != 0) || len(Ꮡc.getSignaturesOfType(target, SignatureKindConstruct)) != 0 && len(typeConstructSignatures) != 0) {
            return true;
        }
        return false;
    }
    return true;
}

internal static ж<ast.Symbol> getUnmatchedProperty(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, bool requireOptionalProperties, bool matchDiscriminantProperties) {
    return Ꮡc.getUnmatchedPropertiesWorker(source, target, requireOptionalProperties, matchDiscriminantProperties, nil);
}

internal static slice<ж<ast.Symbol>> getUnmatchedProperties(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, bool requireOptionalProperties, bool matchDiscriminantProperties) {
    ref var props = ref heap<slice<ж<ast.Symbol>>>(out var Ꮡprops);
    Ꮡc.getUnmatchedPropertiesWorker(source, target, requireOptionalProperties, matchDiscriminantProperties, Ꮡprops);
    return props;
}

internal static ж<ast.Symbol> getUnmatchedPropertiesWorker(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target, bool requireOptionalProperties, bool matchDiscriminantProperties, ж<slice<ж<ast.Symbol>>> ᏑpropsOut) {
    ref var propsOut = ref ᏑpropsOut.DerefOrNull();

    var properties = Ꮡc.getPropertiesOfType(target);
    foreach (var (_, targetProp) in range(properties)) {
        if (isStaticPrivateIdentifierProperty(ref (targetProp).DerefOrNull())) {
            continue;
        }
        if (requireOptionalProperties || (ast.SymbolFlags)(targetProp.Flags & ast.SymbolFlagsOptional) == 0 && (ast.CheckFlags)(targetProp.CheckFlags & ast.CheckFlagsPartial) == 0) {
            var sourceProp = Ꮡc.getPropertyOfType(source, targetProp.Name);
            if (sourceProp == nil) {
                if (ᏑpropsOut == nil) {
                    return targetProp;
                }
                propsOut = append(propsOut, targetProp);
            } else if (matchDiscriminantProperties) {
                var targetType = Ꮡc.getTypeOfSymbol(targetProp);
                if ((TypeFlags)(targetType.flags & TypeFlagsUnit) != 0) {
                    var sourceType = Ꮡc.getTypeOfSymbol(sourceProp);
                    if (!((TypeFlags)(sourceType.flags & TypeFlagsAny) != 0 || Ꮡc.getRegularTypeOfLiteralType(sourceType) == Ꮡc.getRegularTypeOfLiteralType(targetType))) {
                        if (ᏑpropsOut == nil) {
                            return targetProp;
                        }
                        propsOut = append(propsOut, targetProp);
                    }
                }
            }
        }
    }
    return default;
}

} partial class checker_package {

internal static slice<ж<ast.Symbol>> excludeProperties(slice<ж<ast.Symbol>> properties, collections.Set<@string> excludedPropertiesʗp) {
    ref var excludedProperties = ref heap(excludedPropertiesʗp, out var ᏑexcludedProperties);

    if (ᏑexcludedProperties.Len() == 0 || len(properties) == 0) {
        return properties;
    }
    slice<ж<ast.Symbol>> reduced = default;
    bool excluded = default;
    foreach (var (i, prop) in range(properties)) {
        if (!ᏑexcludedProperties.Has(prop.Name)) {
            if (excluded) {
                reduced = append(reduced, prop);
            }
        } else if (!excluded) {
            reduced = slices.Clip<slice<ж<ast.Symbol>>, ж<ast.Symbol>>(properties.slice(0, i));
            excluded = true;
        }
    }
    if (excluded) {
        return reduced;
    }
    return properties;
}

partial struct TypeDiscriminator {
    internal ж<Checker> c;
    internal slice<ж<ast.Symbol>> props;
    internal Func<ж<Type>, ж<Type>, Ternary> isRelatedTo;
}

} partial class checkerᴛTypeDiscriminator {

internal static nint len(this ref TypeDiscriminator d) {
    return builtin.len(d.props);
}

internal static @string name(this ref TypeDiscriminator d, nint index) {
    return d.props[index].Name;
}

internal static bool matches(this ref TypeDiscriminator d, nint index, ж<Type> t) {
    var propType = d.c.getTypeOfSymbol(d.props[index]);
    foreach (var (_, s) in range(propType.Distributed())) {
        if (d.isRelatedTo(s, t) != TernaryFalse) {
            return true;
        }
    }
    return false;
}

} partial class checkerᴛChecker {

internal static ж<Type> findMatchingDiscriminantType(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, Func<ж<Type>, ж<Type>, Ternary> isRelatedTo) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(target.flags & TypeFlagsUnion) != 0 && (TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsIntersection | TypeFlagsObject))) != 0) {
        var match = Ꮡc.getMatchingUnionConstituentForType(Ꮡtarget, Ꮡsource); if (match != nil) {
            return match;
        }
        var discriminantProperties = Ꮡc.findDiscriminantProperties(Ꮡc.getPropertiesOfType(Ꮡsource), Ꮡtarget); if (len(discriminantProperties) != 0) {
            var discriminator = Ꮡ(new TypeDiscriminator(c: Ꮡc, props: discriminantProperties, isRelatedTo: isRelatedTo));
            var discriminated = Ꮡc.discriminateTypeByDiscriminableItems(Ꮡtarget, new TypeDiscriminatorжDiscriminator(discriminator)); if (discriminated != Ꮡtarget) {
                return discriminated;
            }
        }
    }
    return default;
}

internal static slice<ж<ast.Symbol>> findDiscriminantProperties(this ж<Checker> Ꮡc, slice<ж<ast.Symbol>> sourceProperties, ж<Type> target) {
    slice<ж<ast.Symbol>> result = default;
    foreach (var (_, sourceProperty) in range(sourceProperties)) {
        if (Ꮡc.isDiscriminantProperty(target, sourceProperty.Name)) {
            result = append(result, sourceProperty);
        }
    }
    return result;
}

internal static bool isDiscriminantProperty(this ж<Checker> Ꮡc, ж<Type> Ꮡt, @string name) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡt != nil && (TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
        var prop = Ꮡc.getUnionOrIntersectionProperty(Ꮡt, name, false);
        if (prop != nil && (ast.CheckFlags)(prop.CheckFlags & ast.CheckFlagsSyntheticProperty) != 0) {
            if ((ast.CheckFlags)(prop.CheckFlags & ast.CheckFlagsIsDiscriminantComputed) == 0) {
                prop.CheckFlags |= (ast.CheckFlags)(ast.CheckFlagsIsDiscriminantComputed);
                if ((ast.CheckFlags)(prop.CheckFlags & ast.CheckFlagsNonUniformAndLiteral) == ast.CheckFlagsNonUniformAndLiteral && !Ꮡc.isGenericType(Ꮡc.getTypeOfSymbol(prop))) {
                    prop.CheckFlags |= (ast.CheckFlags)(ast.CheckFlagsIsDiscriminant);
                }
            }
            return (ast.CheckFlags)(prop.CheckFlags & ast.CheckFlagsIsDiscriminant) != 0;
        }
    }
    return false;
}

internal static ж<Type> getMatchingUnionConstituentForType(this ж<Checker> Ꮡc, ж<Type> unionType, ж<Type> t) {
    @string keyPropertyName = Ꮡc.getKeyPropertyName(unionType);
    if (keyPropertyName == ""u8) {
        return default;
    }
    var propType = Ꮡc.getTypeOfPropertyOfType(t, keyPropertyName);
    if (propType == nil) {
        return default;
    }
    return Ꮡc.getConstituentTypeForKeyType(unionType, propType);
}

internal static @string getKeyPropertyName(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    var u = t.AsUnionType();
    if (u.keyPropertyName == ""u8) {
        (u.keyPropertyName, u.constituentMap) = Ꮡc.computeKeyPropertyNameAndMap(Ꮡt);
    }
    if (u.keyPropertyName == ast.InternalSymbolNameMissing) {
        return "";
    }
    return u.keyPropertyName;
}

internal static ж<Type> getConstituentTypeForKeyType(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ж<Type> keyType) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    var result = t.AsUnionType().constituentMap[Ꮡc.getRegularTypeOfLiteralType(keyType)];
    if (result != c.unknownType) {
        return result;
    }
    return default;
}

internal static (@string, map<ж<Type>, ж<Type>>) computeKeyPropertyNameAndMap(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    var types = t.Types();
    if (len(types) < 10 || (ObjectFlags)(t.objectFlags & ObjectFlagsPrimitiveUnion) != 0 || core.CountWhere<ж<Type>>(types, isObjectOrInstantiableNonPrimitive) < 10) {
        return (ast.InternalSymbolNameMissing, default);
    }
    @string keyPropertyName = Ꮡc.getKeyPropertyCandidateName(types);
    if (keyPropertyName == ""u8) {
        return (ast.InternalSymbolNameMissing, default);
    }
    var mapByKeyProperty = Ꮡc.mapTypesByKeyProperty(types, keyPropertyName);
    if (mapByKeyProperty == nil) {
        return (ast.InternalSymbolNameMissing, default);
    }
    return (keyPropertyName, mapByKeyProperty);
}

} partial class checker_package {

internal static bool isObjectOrInstantiableNonPrimitive(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (TypeFlags)(t.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsInstantiableNonPrimitive))) != 0;
}

} partial class checkerᴛChecker {

internal static @string getKeyPropertyCandidateName(this ж<Checker> Ꮡc, slice<ж<Type>> types) {
    foreach (var (_, t) in range(types)) {
        if ((TypeFlags)(t.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsInstantiableNonPrimitive))) != 0) {
            foreach (var (_, p) in range(Ꮡc.getPropertiesOfType(t))) {
                if (isUnitType(Ꮡc.getTypeOfSymbol(p))) {
                    return p.Name;
                }
            }
        }
    }
    return "";
}

internal static map<ж<Type>, ж<Type>> mapTypesByKeyProperty(this ж<Checker> Ꮡc, slice<ж<Type>> types, @string keyPropertyName) {
    ref var c = ref Ꮡc.DerefOrNull();

    var typesByKey = new map<ж<Type>, ж<Type>>();
    nint count = 0;
    foreach (var (_, t) in range(types)) {
        if ((TypeFlags)(t.flags & ((TypeFlags)((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection) | TypeFlagsInstantiableNonPrimitive))) != 0) {
            var discriminant = Ꮡc.getTypeOfPropertyOfType(t, keyPropertyName);
            if (discriminant == nil || !isLiteralType(discriminant)) {
                return default;
            }
            var duplicate = false;
            foreach (var (_, d) in range(discriminant.Distributed())) {
                var key = Ꮡc.getRegularTypeOfLiteralType(d);
                var existing = typesByKey[key]; if (existing == nil) {
                    typesByKey[key] = t;
                } else if (existing != c.unknownType) {
                    typesByKey[key] = c.unknownType;
                    duplicate = true;
                }
            }
            if (!duplicate) {
                count++;
            }
        }
    }
    if (count >= 10 && count * 2 >= len(types)) {
        return typesByKey;
    }
    return default;
}

} partial class checker_package {

partial interface Discriminator {
    nint len();
    @string name(nint index);
    bool matches(nint index, ж<Type> t);
}

} partial class checkerᴛChecker {

internal static ж<Type> discriminateTypeByDiscriminableItems(this ж<Checker> Ꮡc, ж<Type> Ꮡtarget, Discriminator discriminator) {
    ref var target = ref Ꮡtarget.DerefOrNull();
    var filteredTypesᴷ = new stackAppend<ж<Type>>();

    var types = target.Types();
    var include = new slice<Ternary>(len(types));
    foreach (var (i, t) in range(types)) {
        if ((TypeFlags)(t.flags & TypeFlagsPrimitive) == 0 && (TypeFlags)(Ꮡc.getReducedType(t).flags & TypeFlagsNever) == 0) {
            include[i] = TernaryTrue;
        }
    }
    foreach (var n in range(discriminator.len())) {
        var matched = false;
        foreach (var i in range(len(types))) {
            if (include[i] != TernaryFalse) {
                var targetType = Ꮡc.getTypeOfPropertyOrIndexSignatureOfType(types[i], discriminator.name(n));
                if (targetType != nil) {
                    if (discriminator.matches(n, targetType)) {
                        matched = true;
                    } else {
                        include[i] = TernaryMaybe;
                    }
                }
            }
        }
        foreach (var i in range(len(types))) {
            if (include[i] == TernaryMaybe) {
                if (matched) {
                    include[i] = TernaryFalse;
                } else {
                    include[i] = TernaryTrue;
                }
            }
        }
    }
    if (slices.Contains(include, TernaryFalse)) {
        slice<ж<Type>> filteredTypes = default;
        foreach (var (i, t) in range(types)) {
            if (include[i] == TernaryTrue) {
                filteredTypes = append(filteredTypesᴷ, filteredTypes, t);
            }
        }
        var filtered = Ꮡc.getUnionTypeEx(filteredTypes, UnionReductionNone, nil, nil);
        if ((TypeFlags)(filtered.flags & TypeFlagsNever) == 0) {
            return filtered;
        }
    }
    return Ꮡtarget;
}

internal static ж<Type> filterPrimitivesIfContainsNonPrimitive(this ж<Checker> Ꮡc, ж<Type> unionType) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (c.maybeTypeOfKind(unionType, TypeFlagsNonPrimitive)) {
        var result = Ꮡc.filterType(unionType, isNonPrimitiveType);
        if ((TypeFlags)(result.flags & TypeFlagsNever) == 0) {
            return result;
        }
    }
    return unionType;
}

} partial class checker_package {

internal static bool isNonPrimitiveType(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (TypeFlags)(t.flags & TypeFlagsPrimitive) == 0;
}

} partial class checkerᴛChecker {

internal static (@string, @string) getTypeNamesForErrorDisplay(this ж<Checker> Ꮡc, ж<Type> Ꮡleft, ж<Type> Ꮡright) {
    ref var left = ref Ꮡleft.DerefOrNull();
    ref var right = ref Ꮡright.DerefOrNull();

    @string leftStr = default;
    if (Ꮡc.symbolValueDeclarationIsContextSensitive(left.symbol)) {
        leftStr = Ꮡc.typeToString(Ꮡleft, left.symbol.ValueDeclaration);
    } else {
        leftStr = Ꮡc.TypeToString(Ꮡleft);
    }
    @string rightStr = default;
    if (Ꮡc.symbolValueDeclarationIsContextSensitive(right.symbol)) {
        rightStr = Ꮡc.typeToString(Ꮡright, right.symbol.ValueDeclaration);
    } else {
        rightStr = Ꮡc.TypeToString(Ꮡright);
    }
    if (leftStr == rightStr) {
        leftStr = Ꮡc.getTypeNameForErrorDisplay(Ꮡleft);
        rightStr = Ꮡc.getTypeNameForErrorDisplay(Ꮡright);
    }
    return (leftStr, rightStr);
}

internal static @string getTypeNameForErrorDisplay(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.typeToStringEx(t, nil, TypeFormatFlagsUseFullyQualifiedType, nil);
}

internal static bool symbolValueDeclarationIsContextSensitive(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return Ꮡsymbol != nil && symbol.ValueDeclaration != nil && ast.IsExpression(symbol.ValueDeclaration) && !Ꮡc.isContextSensitive(symbol.ValueDeclaration);
}

internal static bool typeCouldHaveTopLevelSingletonTypes(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsBoolean) != 0) {
        return false;
    }
    if ((TypeFlags)(t.flags & TypeFlagsUnionOrIntersection) != 0) {
        return core.Some<ж<Type>>(t.Types(), Ꮡc.typeCouldHaveTopLevelSingletonTypes);
    }
    if ((TypeFlags)(t.flags & TypeFlagsInstantiable) != 0) {
        var constraint = Ꮡc.getConstraintOfType(Ꮡt);
        if (constraint != nil && constraint != Ꮡt) {
            return Ꮡc.typeCouldHaveTopLevelSingletonTypes(constraint);
        }
    }
    return isUnitType(Ꮡt) || (TypeFlags)(t.flags & TypeFlagsTemplateLiteral) != 0 || (TypeFlags)(t.flags & TypeFlagsStringMapping) != 0;
}

internal static slice<VarianceFlags> getVariances(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡt == c.globalArrayType || Ꮡt == c.globalReadonlyArrayType || (ObjectFlags)(t.objectFlags & ObjectFlagsTuple) != 0) {
        return c.arrayVariances;
    }
    return Ꮡc.getVariancesWorker(t.symbol, t.AsInterfaceType().TypeParameters());
}

internal static slice<VarianceFlags> getAliasVariances(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.getVariancesWorker(symbol, c.typeAliasLinks.Get(symbol).typeParameters);
}

internal static slice<VarianceFlags> getVariancesWorker(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, slice<ж<Type>> typeParameters) {
    GoFrame ᒐ = default;
    try {
        ref var c = ref Ꮡc.DerefOrNull();
        ref var symbol = ref Ꮡsymbol.DerefOrNull();

        var links = c.varianceLinks.Get(Ꮡsymbol);
        if (links.variances == nil) {
            map<@string, any> traceArgs = default;
            {
                var tr = c.tracer; if (tr != nil) {
                    traceArgs = new map<@string, any>{["arity"] = len(typeParameters), ["id"] = Ꮡc.getDeclaredTypeOfSymbol(Ꮡsymbol).id};
                    var popFn = tr.Push(tracing.PhaseCheckTypes, "getVariancesWorker", traceArgs, true);
                    var linksʗ1 = links;
                    var popFnʗ1 = popFn;
                    var traceArgsʗ1 = traceArgs;
                    defer(() => {
                        var formatted = new slice<@string>(len(linksʗ1.variances));
                        foreach (var (i, v) in range(linksʗ1.variances)) {
                            formatted[i] = v.String();
                        }
                        traceArgsʗ1["variances"] = formatted;
                        popFnʗ1();
                    }, ref ᒐ);
                }
            }
            nint stackIndex = c.getVarianceStackIndex(Ꮡsymbol);
            if (stackIndex < 0) {
                nint saveResolutionStart = c.resolutionStart;
                if (len(c.varianceStack) == 0) {
                    c.resolutionStart = len(c.typeResolutions);
                }
                c.varianceStack = append(c.varianceStack, new VarianceStackEntry(Ꮡsymbol, typeParameters));
                var variances = new slice<VarianceFlags>(len(typeParameters));
                foreach (var (i, tp) in range(typeParameters)) {
                    var modifiers = c.getTypeParameterModifiers(tp);
                    VarianceFlags variance = default;
                    switch (ᐧ) {
                    case {} when (ast.ModifierFlags)(modifiers & ast.ModifierFlagsOut) != 0: {
                        if ((ast.ModifierFlags)(modifiers & ast.ModifierFlagsIn) != 0) {
                            variance = VarianceFlagsInvariant;
                        } else {
                            variance = VarianceFlagsCovariant;
                        }
                        break;
                    }
                    case {} when (ast.ModifierFlags)(modifiers & ast.ModifierFlagsIn) != 0: {
                        variance = VarianceFlagsContravariant;
                        break;
                    }
                    default: {
                        var saveReliabilityFlags = c.reliabilityFlags;
                        c.reliabilityFlags = 0;
                        var typeWithSuper = Ꮡc.createMarkerType(Ꮡsymbol, tp, c.markerSuperType);
                        var typeWithSub = Ꮡc.createMarkerType(Ꮡsymbol, tp, c.markerSubType);
                        variance = (VarianceFlags)(core.IfElse(Ꮡc.isTypeAssignableTo(typeWithSub, typeWithSuper), VarianceFlagsCovariant, (VarianceFlags)0) | core.IfElse(Ꮡc.isTypeAssignableTo(typeWithSuper, typeWithSub), VarianceFlagsContravariant, (VarianceFlags)0));
                        if (variance == VarianceFlagsBivariant && Ꮡc.isTypeAssignableTo(Ꮡc.createMarkerType(Ꮡsymbol, tp, c.markerOtherType), typeWithSuper)) {
                            variance = VarianceFlagsIndependent;
                        }
                        if ((RelationComparisonResult)(c.reliabilityFlags & RelationComparisonResultReportsUnmeasurable) != 0) {
                            variance |= (VarianceFlags)(VarianceFlagsUnmeasurable);
                        }
                        if ((RelationComparisonResult)(c.reliabilityFlags & RelationComparisonResultReportsUnreliable) != 0) {
                            variance |= (VarianceFlags)(VarianceFlagsUnreliable);
                        }
                        c.reliabilityFlags = saveReliabilityFlags;
                        break;
                    }}

                    if (len(links.variances) != 0) {
                        break;
                    }
                    variances[i] = variance;
                }
                if (len(links.variances) == 0) {
                    links.variances = variances;
                }
                c.varianceStack = c.varianceStack.slice(0, len(c.varianceStack) - 1);
                if (len(c.varianceStack) == 0) {
                    c.resolutionStart = saveResolutionStart;
                }
            } else {
                nint minIndex = stackIndex;
                for (nint i = stackIndex + 1; i < len(c.varianceStack); i++) {
                    if (c.compareSymbols(c.varianceStack[i].symbol, c.varianceStack[minIndex].symbol) < 0) {
                        minIndex = i;
                    }
                }
                if (minIndex > stackIndex) {
                    var saveVarianceStack = c.varianceStack;
                    c.varianceStack = default;
                    Ꮡc.getVariancesWorker(saveVarianceStack[minIndex].symbol, saveVarianceStack[minIndex].typeParameters);
                    c.varianceStack = saveVarianceStack;
                }
                if (len(links.variances) == 0) {
                    links.variances = [];
                }
            }
        }
        return links.variances;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static nint getVarianceStackIndex(this ref Checker c, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    foreach (var (i, entry) in range(c.varianceStack)) {
        if (entry.symbol == Ꮡsymbol) {
            return i;
        }
    }
    return -1;
}

internal static ж<Type> createMarkerType(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var mapper = newSimpleTypeMapper(source, target);
    var t = Ꮡc.getDeclaredTypeOfSymbol(Ꮡsymbol);
    if (c.isErrorType(t)) {
        return t;
    }
    ж<Type> result = default;
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTypeAlias) != 0) {
        result = Ꮡc.getTypeAliasInstantiation(Ꮡsymbol, Ꮡc.instantiateTypes(c.typeAliasLinks.Get(Ꮡsymbol).typeParameters, mapper), nil);
    } else {
        result = Ꮡc.createTypeReference(t, Ꮡc.instantiateTypes(t.AsInterfaceType().TypeParameters(), mapper));
    }
    c.markerTypes.Add(result);
    return result;
}

internal static bool isMarkerType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.ᏑmarkerTypes.Has(t);
}

internal static ast.ModifierFlags getTypeParameterModifiers(this ref Checker c, ж<Type> Ꮡtp) {
    ref var tp = ref Ꮡtp.DerefOrNull();

    ast.ModifierFlags flags = default;
    if (tp.symbol != nil) {
        foreach (var (_, d) in range(tp.symbol.Declarations)) {
            flags |= (ast.ModifierFlags)(d.ModifierFlags());
        }
    }
    return (ast.ModifierFlags)(flags & ((ast.ModifierFlags)((ast.ModifierFlags)(ast.ModifierFlagsIn | ast.ModifierFlagsOut) | ast.ModifierFlagsConst)));
}

internal static bool hasCovariantVoidArgument(this ref Checker c, slice<ж<Type>> typeArguments, slice<VarianceFlags> variances) {
    foreach (var (i, v) in range(variances)) {
        if ((VarianceFlags)(v & VarianceFlagsVarianceMask) == VarianceFlagsCovariant && (TypeFlags)(typeArguments[i].flags & TypeFlagsVoid) != 0) {
            return true;
        }
    }
    return false;
}

internal static bool isSignatureAssignableTo(this ж<Checker> Ꮡc, ж<Signature> source, ж<Signature> target, bool ignoreReturnTypes) {
    ref var c = ref Ꮡc.DerefOrNull();

    return Ꮡc.compareSignaturesRelated(source, target, core.IfElse(ignoreReturnTypes, SignatureCheckModeIgnoreReturnTypes, SignatureCheckModeNone), false, default, c.compareTypesAssignable, nil) != TernaryFalse;
}

internal static Ternary compareSignaturesRelated(this ж<Checker> Ꮡc, ж<Signature> Ꮡsource, ж<Signature> Ꮡtarget, SignatureCheckMode checkMode, bool reportErrors, Actionꓸꓸꓸ<ж<diagnostics.Message>, any> errorReporter, Func<ж<Type>, ж<Type>, bool, Ternary> compareTypes, ж<TypeMapper> reportUnreliableMarkers) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if (Ꮡsource == Ꮡtarget) {
        return TernaryTrue;
    }
    if (!((SignatureCheckMode)(checkMode & SignatureCheckModeStrictTopSignature) != 0 && Ꮡc.isTopSignature(Ꮡsource)) && Ꮡc.isTopSignature(Ꮡtarget)) {
        return TernaryTrue;
    }
    if ((SignatureCheckMode)(checkMode & SignatureCheckModeStrictTopSignature) != 0 && Ꮡc.isTopSignature(Ꮡsource) && !Ꮡc.isTopSignature(Ꮡtarget)) {
        return TernaryFalse;
    }
    nint targetCount = Ꮡc.getParameterCount(Ꮡtarget);
    bool sourceHasMoreParameters = default;
    if (!Ꮡc.hasEffectiveRestParameter(Ꮡtarget)) {
        if ((SignatureCheckMode)(checkMode & SignatureCheckModeStrictArity) != 0) {
            sourceHasMoreParameters = Ꮡc.hasEffectiveRestParameter(Ꮡsource) || Ꮡc.getParameterCount(Ꮡsource) > targetCount;
        } else {
            sourceHasMoreParameters = Ꮡc.getMinArgumentCount(Ꮡsource) > targetCount;
        }
    }
    if (sourceHasMoreParameters) {
        if (reportErrors && ((SignatureCheckMode)(checkMode & SignatureCheckModeStrictArity) == 0)) {
            errorReporter(diagnostics.Target_signature_provides_too_few_arguments_Expected_0_or_more_but_got_1, Ꮡc.getMinArgumentCount(Ꮡsource), targetCount);
        }
        return TernaryFalse;
    }
    if (len(source.typeParameters) != 0 && !core.Same(source.typeParameters, target.typeParameters)) {
        Ꮡtarget = Ꮡc.getCanonicalSignature(Ꮡtarget); target = ref Ꮡtarget.DerefOrNull();
        Ꮡsource = Ꮡc.instantiateSignatureInContextOf(Ꮡsource, Ꮡtarget, nil, compareTypes); source = ref Ꮡsource.DerefOrNull();
    }
    nint sourceCount = Ꮡc.getParameterCount(Ꮡsource);
    var sourceRestType = Ꮡc.getNonArrayRestType(Ꮡsource);
    var targetRestType = Ꮡc.getNonArrayRestType(Ꮡtarget);
    if (sourceRestType != nil || targetRestType != nil) {
        Ꮡc.instantiateType(core.IfElse(sourceRestType != nil, sourceRestType, targetRestType), reportUnreliableMarkers);
    }
    var kind = ast.KindUnknown;
    if (target.declaration != nil) {
        kind = target.declaration.Kind;
    }
    var strictVariance = (SignatureCheckMode)(checkMode & SignatureCheckModeCallback) == 0 && c.strictFunctionTypes && kind != ast.KindMethodDeclaration && kind != ast.KindMethodSignature && kind != ast.KindConstructor;
    var result = TernaryTrue;
    var sourceThisType = Ꮡc.getThisTypeOfSignature(Ꮡsource);
    if (sourceThisType != nil && sourceThisType != c.voidType) {
        var targetThisType = Ꮡc.getThisTypeOfSignature(Ꮡtarget);
        if (targetThisType != nil) {
            Ternary related = default;
            if (!strictVariance) {
                related = compareTypes(sourceThisType, targetThisType, false);
            }
            if (related == TernaryFalse) {
                related = compareTypes(targetThisType, sourceThisType, reportErrors);
            }
            if (related == TernaryFalse) {
                if (reportErrors) {
                    errorReporter(diagnostics.The_this_types_of_each_signature_are_incompatible);
                }
                return TernaryFalse;
            }
            result &= (Ternary)(related);
        }
    }
    nint paramCount = default;
    if (sourceRestType != nil || targetRestType != nil) {
        paramCount = min(sourceCount, targetCount);
    } else {
        paramCount = max(sourceCount, targetCount);
    }
    nint restIndex = default;
    if (sourceRestType != nil || targetRestType != nil) {
        restIndex = paramCount - 1;
    } else {
        restIndex = -1;
    }
    foreach (var i in range(paramCount)) {
        ж<Type> sourceType = default;
        if (i == restIndex) {
            sourceType = Ꮡc.getRestOrAnyTypeAtPosition(Ꮡsource, i);
        } else {
            sourceType = Ꮡc.tryGetTypeAtPosition(Ꮡsource, i);
        }
        ж<Type> targetType = default;
        if (i == restIndex) {
            targetType = Ꮡc.getRestOrAnyTypeAtPosition(Ꮡtarget, i);
        } else {
            targetType = Ꮡc.tryGetTypeAtPosition(Ꮡtarget, i);
        }
        if (sourceType != nil && targetType != nil && (sourceType != targetType || (SignatureCheckMode)(checkMode & SignatureCheckModeStrictArity) != 0)) {
            ж<Signature> sourceSig = default;
            ж<Signature> targetSig = default;
            if ((SignatureCheckMode)(checkMode & SignatureCheckModeCallback) == 0 && !Ꮡc.isInstantiatedGenericParameter(Ꮡsource, i)) {
                sourceSig = Ꮡc.getSingleCallSignature(Ꮡc.GetNonNullableType(sourceType));
            }
            if ((SignatureCheckMode)(checkMode & SignatureCheckModeCallback) == 0 && !Ꮡc.isInstantiatedGenericParameter(Ꮡtarget, i)) {
                targetSig = Ꮡc.getSingleCallSignature(Ꮡc.GetNonNullableType(targetType));
            }
            var callbacks = sourceSig != nil && targetSig != nil && Ꮡc.getTypePredicateOfSignature(sourceSig) == nil && Ꮡc.getTypePredicateOfSignature(targetSig) == nil && Ꮡc.getTypeFacts(sourceType, TypeFactsIsUndefinedOrNull) == Ꮡc.getTypeFacts(targetType, TypeFactsIsUndefinedOrNull);
            Ternary related = default;
            if (callbacks) {
                related = Ꮡc.compareSignaturesRelated(targetSig, sourceSig, (SignatureCheckMode)((SignatureCheckMode)(checkMode & SignatureCheckModeStrictArity) | core.IfElse(strictVariance, SignatureCheckModeStrictCallback, SignatureCheckModeBivariantCallback)), reportErrors, errorReporter, compareTypes, reportUnreliableMarkers);
            } else {
                if ((SignatureCheckMode)(checkMode & SignatureCheckModeCallback) == 0 && !strictVariance) {
                    related = compareTypes(sourceType, targetType, false);
                }
                if (related == TernaryFalse) {
                    related = compareTypes(targetType, sourceType, reportErrors);
                }
            }
            if (related != TernaryFalse && (SignatureCheckMode)(checkMode & SignatureCheckModeStrictArity) != 0 && i >= Ꮡc.getMinArgumentCount(Ꮡsource) && i < Ꮡc.getMinArgumentCount(Ꮡtarget) && compareTypes(sourceType, targetType, false) != TernaryFalse) {
                related = TernaryFalse;
            }
            if (related == TernaryFalse) {
                if (reportErrors) {
                    errorReporter(diagnostics.Types_of_parameters_0_and_1_are_incompatible, Ꮡc.getParameterNameAtPosition(Ꮡsource, i), Ꮡc.getParameterNameAtPosition(Ꮡtarget, i));
                }
                return TernaryFalse;
            }
            result &= (Ternary)(related);
        }
    }
    if ((SignatureCheckMode)(checkMode & SignatureCheckModeIgnoreReturnTypes) == 0) {
        var targetReturnType = Ꮡc.getNonCircularReturnTypeOfSignature(Ꮡtarget);
        if (targetReturnType == c.voidType || targetReturnType == c.anyType) {
            return result;
        }
        var sourceReturnType = Ꮡc.getNonCircularReturnTypeOfSignature(Ꮡsource);
        var targetTypePredicate = Ꮡc.getTypePredicateOfSignature(Ꮡtarget);
        if (targetTypePredicate != nil) {
            var sourceTypePredicate = Ꮡc.getTypePredicateOfSignature(Ꮡsource);
            if (sourceTypePredicate != nil) {
                result &= (Ternary)(Ꮡc.compareTypePredicateRelatedTo(sourceTypePredicate, targetTypePredicate, reportErrors, errorReporter, compareTypes));
            } else if (targetTypePredicate.kind == TypePredicateKindIdentifier || targetTypePredicate.kind == TypePredicateKindThis) {
                if (reportErrors) {
                    errorReporter(diagnostics.Signature_0_must_be_a_type_predicate, Ꮡc.signatureToString(Ꮡsource));
                }
                return TernaryFalse;
            }
        } else {
            Ternary related = default;
            if ((SignatureCheckMode)(checkMode & SignatureCheckModeBivariantCallback) != 0) {
                related = compareTypes(targetReturnType, sourceReturnType, false);
            }
            if (related == TernaryFalse) {
                related = compareTypes(sourceReturnType, targetReturnType, reportErrors);
            }
            result &= (Ternary)(related);
            if (result == TernaryFalse && reportErrors) {
                ж<diagnostics.Message> message = default;
                if (len(source.parameters) == 0 && len(target.parameters) == 0) {
                    message = core.IfElse((SignatureFlags)(source.flags & SignatureFlagsConstruct) != 0,
                        diagnostics.Construct_signatures_with_no_arguments_have_incompatible_return_types_0_and_1,
                        diagnostics.Call_signatures_with_no_arguments_have_incompatible_return_types_0_and_1);
                } else {
                    message = core.IfElse((SignatureFlags)(source.flags & SignatureFlagsConstruct) != 0,
                        diagnostics.Construct_signature_return_types_0_and_1_are_incompatible,
                        diagnostics.Call_signature_return_types_0_and_1_are_incompatible);
                }
                errorReporter(message, Ꮡc.TypeToString(sourceReturnType), Ꮡc.TypeToString(targetReturnType));
            }
        }
    }
    return result;
}

internal static Ternary compareTypePredicateRelatedTo(this ж<Checker> Ꮡc, ж<TypePredicate> Ꮡsource, ж<TypePredicate> Ꮡtarget, bool reportErrors, Actionꓸꓸꓸ<ж<diagnostics.Message>, any> errorReporter, Func<ж<Type>, ж<Type>, bool, Ternary> compareTypes) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if (source.kind != target.kind) {
        if (reportErrors) {
            errorReporter(diagnostics.A_this_based_type_guard_is_not_compatible_with_a_parameter_based_type_guard);
            errorReporter(diagnostics.Type_predicate_0_is_not_assignable_to_1, Ꮡc.typePredicateToString(Ꮡsource), Ꮡc.typePredicateToString(Ꮡtarget));
        }
        return TernaryFalse;
    }
    if (source.kind == TypePredicateKindIdentifier || source.kind == TypePredicateKindAssertsIdentifier) {
        if (source.parameterIndex != target.parameterIndex) {
            if (reportErrors) {
                errorReporter(diagnostics.Parameter_0_is_not_in_the_same_position_as_parameter_1, source.parameterName, target.parameterName);
                errorReporter(diagnostics.Type_predicate_0_is_not_assignable_to_1, Ꮡc.typePredicateToString(Ꮡsource), Ꮡc.typePredicateToString(Ꮡtarget));
            }
            return TernaryFalse;
        }
    }
    Ternary related = default;
    switch (ᐧ) {
    case {} when source.t == target.t: {
        related = TernaryTrue;
        break;
    }
    case {} when source.t != nil && target.t != nil: {
        related = compareTypes(source.t, target.t, reportErrors);
        break;
    }
    default: {
        related = TernaryFalse;
        break;
    }}

    if (related == TernaryFalse && reportErrors) {
        errorReporter(diagnostics.Type_predicate_0_is_not_assignable_to_1, Ꮡc.typePredicateToString(Ꮡsource), Ꮡc.typePredicateToString(Ꮡtarget));
    }
    return related;
}

internal static bool isTopSignature(this ж<Checker> Ꮡc, ж<Signature> Ꮡs) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var s = ref Ꮡs.DerefOrNull();

    if (len(s.typeParameters) == 0 && (s.thisParameter == nil || IsTypeAny(Ꮡc.getTypeOfParameter(s.thisParameter))) && len(s.parameters) == 1 && signatureHasRestParameter(ref (Ꮡs).DerefOrNull())) {
        var paramType = Ꮡc.getTypeOfParameter(s.parameters[0]);
        ж<Type> restType = default;
        if (c.isArrayType(paramType)) {
            restType = Ꮡc.getTypeArguments(paramType)[0];
        } else {
            restType = paramType;
        }
        return (TypeFlags)(restType.flags & ((TypeFlags)(TypeFlagsAny | TypeFlagsNever))) != 0 && (TypeFlags)(Ꮡc.getReturnTypeOfSignature(Ꮡs).flags & TypeFlagsAnyOrUnknown) != 0;
    }
    return false;
}

internal static nint getParameterCount(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    nint length = len(signature.parameters);
    if (signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull())) {
        var restType = Ꮡc.getTypeOfSymbol(signature.parameters[length - 1]);
        if (isTupleType(restType)) {
            return length + restType.TargetTupleType().fixedLength - core.IfElse<nint>((ElementFlags)(restType.TargetTupleType().combinedFlags & ElementFlagsVariable) != 0, 0, 1);
        }
    }
    return length;
}

internal static nint getMinArgumentCount(this ж<Checker> Ꮡc, ж<Signature> signature) {
    return Ꮡc.getMinArgumentCountEx(signature, MinArgumentCountFlagsNone);
}

internal static nint getMinArgumentCountEx(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature, MinArgumentCountFlags flags) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    var strongArityForUntypedJS = (MinArgumentCountFlags)(flags & MinArgumentCountFlagsStrongArityForUntypedJS);
    var voidIsNonOptional = (MinArgumentCountFlags)(flags & MinArgumentCountFlagsVoidIsNonOptional);
    if (voidIsNonOptional != 0 || signature.resolvedMinArgumentCount == -1) {
        nint minArgumentCount = -1;
        if (signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull())) {
            var restType = Ꮡc.getTypeOfSymbol(signature.parameters[len(signature.parameters) - 1]);
            if (isTupleType(restType)) {
                nint firstOptionalIndex = core.FindIndex(restType.TargetTupleType().elementInfos, (TupleElementInfo info) => (ElementFlags)(info.flags & ElementFlagsRequired) == 0);
                nint requiredCount = firstOptionalIndex;
                if (firstOptionalIndex < 0) {
                    requiredCount = restType.TargetTupleType().fixedLength;
                }
                if (requiredCount > 0) {
                    minArgumentCount = len(signature.parameters) - 1 + requiredCount;
                }
            }
        }
        if (minArgumentCount == -1) {
            if (strongArityForUntypedJS == 0 && (SignatureFlags)(signature.flags & SignatureFlagsIsUntypedSignatureInJSFile) != 0) {
                return 0;
            }
            minArgumentCount = (nint)signature.minArgumentCount;
        }
        if (voidIsNonOptional != 0) {
            return minArgumentCount;
        }
        for (nint i = minArgumentCount - 1; i >= 0; i--) {
            var t = Ꮡc.getTypeAtPosition(Ꮡsignature, i);
            if (!someType(t, (ж<Type> tΔ1) => (TypeFlags)(tΔ1.flags & TypeFlagsVoid) != 0)) {
                break;
            }
            minArgumentCount = i;
        }
        signature.resolvedMinArgumentCount = (int32)minArgumentCount;
    }
    return (nint)signature.resolvedMinArgumentCount;
}

internal static bool hasEffectiveRestParameter(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull())) {
        var restType = Ꮡc.getTypeOfSymbol(signature.parameters[len(signature.parameters) - 1]);
        return !isTupleType(restType) || (ElementFlags)(restType.TargetTupleType().combinedFlags & ElementFlagsVariable) != 0;
    }
    return false;
}

internal static ж<Type> getTypeAtPosition(this ж<Checker> Ꮡc, ж<Signature> signature, nint pos) {
    ref var c = ref Ꮡc.DerefOrNull();

    var t = Ꮡc.tryGetTypeAtPosition(signature, pos);
    if (t != nil) {
        return t;
    }
    return c.anyType;
}

internal static ж<Type> tryGetTypeAtPosition(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature, nint pos) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    nint paramCount = len(signature.parameters) - core.IfElse<nint>(signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull()), 1, 0);
    if (pos < paramCount) {
        return Ꮡc.getTypeOfParameter(signature.parameters[pos]);
    }
    if (signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull())) {
        var restType = Ꮡc.getTypeOfSymbol(signature.parameters[paramCount]);
        nint index = pos - paramCount;
        if (!isTupleType(restType) || (ElementFlags)(restType.TargetTupleType().combinedFlags & ElementFlagsVariable) != 0 || index < restType.TargetTupleType().fixedLength) {
            return Ꮡc.getIndexedAccessType(restType, Ꮡc.getNumberLiteralType((jsnum.Number)(float64)index));
        }
    }
    return default;
}

internal static ж<Type> getRestOrAnyTypeAtPosition(this ж<Checker> Ꮡc, ж<Signature> source, nint pos) {
    ref var c = ref Ꮡc.DerefOrNull();

    var restType = Ꮡc.getRestTypeAtPosition(source, pos, false);
    if (restType != nil) {
        var elementType = Ꮡc.getElementTypeOfArrayType(restType); if (elementType != nil && IsTypeAny(elementType)) {
            return c.anyType;
        }
    }
    return restType;
}

internal static ж<Type> getRestTypeAtPosition(this ж<Checker> Ꮡc, ж<Signature> source, nint pos, bool @readonly) {
    ref var c = ref Ꮡc.DerefOrNull();

    nint parameterCount = Ꮡc.getParameterCount(source);
    nint minArgumentCount = Ꮡc.getMinArgumentCount(source);
    var restType = Ꮡc.getEffectiveRestType(source);
    if (restType != nil && pos >= parameterCount - 1) {
        if (pos == parameterCount - 1) {
            return restType;
        } else {
            return Ꮡc.createArrayType(Ꮡc.getIndexedAccessType(restType, c.numberType));
        }
    }
    nint length = parameterCount - pos;
    if (length <= 0) {
        return Ꮡc.createTupleTypeEx(default, default, @readonly);
    }
    var types = new slice<ж<Type>>(length);
    var infos = new slice<TupleElementInfo>(length);
    foreach (var i in range(len(types))) {
        ElementFlags flags = default;
        if (restType == nil || i < len(types) - 1) {
            types[i] = Ꮡc.getTypeAtPosition(source, i + pos);
            flags = core.IfElse(i + pos < minArgumentCount, ElementFlagsRequired, ElementFlagsOptional);
        } else {
            types[i] = restType;
            flags = ElementFlagsVariadic;
        }
        infos[i] = new TupleElementInfo(flags: flags, labeledDeclaration: Ꮡc.getNameableDeclarationAtPosition(source, i + pos));
    }
    return Ꮡc.createTupleTypeEx(types, infos, @readonly);
}

internal static ж<ast.Node> getNameableDeclarationAtPosition(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature, nint pos) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();

    nint paramCount = len(signature.parameters) - core.IfElse<nint>(signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull()), 1, 0);
    if (pos < paramCount) {
        var decl = signature.parameters[pos].ValueDeclaration;
        if (decl != nil && c.isValidDeclarationForTupleLabel(decl)) {
            return decl;
        }
        return default;
    }
    if (signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull())) {
        var restParameter = signature.parameters[paramCount];
        var restType = Ꮡc.getTypeOfSymbol(restParameter);
        if (isTupleType(restType)) {
            var elementInfos = restType.TargetTupleType().elementInfos;
            nint index = pos - paramCount;
            if (index < len(elementInfos)) {
                return elementInfos[index].labeledDeclaration;
            }
            return default;
        }
        if (restParameter.ValueDeclaration != nil && c.isValidDeclarationForTupleLabel(restParameter.ValueDeclaration)) {
            return restParameter.ValueDeclaration;
        }
    }
    return default;
}

internal static bool isValidDeclarationForTupleLabel(this ref Checker c, ж<ast.Node> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    return ast.IsNamedTupleMember(Ꮡd) || ast.IsParameterDeclaration(Ꮡd) && d.Name() != nil && ast.IsIdentifier(d.Name());
}

internal static ж<Type> getNonArrayRestType(this ж<Checker> Ꮡc, ж<Signature> signature) {
    ref var c = ref Ꮡc.DerefOrNull();

    var restType = Ꮡc.getEffectiveRestType(signature);
    if (restType != nil && !c.isArrayType(restType) && !IsTypeAny(restType)) {
        return restType;
    }
    return default;
}

internal static ж<Type> getEffectiveRestType(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull())) {
        var restType = Ꮡc.getTypeOfSymbol(signature.parameters[len(signature.parameters) - 1]);
        if (!isTupleType(restType)) {
            if (IsTypeAny(restType)) {
                return c.anyArrayType;
            }
            return restType;
        }
        if ((ElementFlags)(restType.TargetTupleType().combinedFlags & ElementFlagsVariable) != 0) {
            return Ꮡc.sliceTupleType(restType, restType.TargetTupleType().fixedLength, 0);
        }
    }
    return default;
}

internal static ж<Type> sliceTupleType(this ж<Checker> Ꮡc, ж<Type> Ꮡt, nint index, nint endSkipCount) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    var target = t.TargetTupleType();
    nint endIndex = c.getTypeReferenceArity(Ꮡt) - max(endSkipCount, 0);
    if (index > target.fixedLength) {
        var restArrayType = Ꮡc.getRestArrayTypeOfTupleType(Ꮡt); if (restArrayType != nil) {
            return restArrayType;
        }
        return Ꮡc.createTupleType(default);
    }
    if (index >= endIndex) {
        return Ꮡc.createTupleType(default);
    }
    return Ꮡc.createTupleTypeEx(Ꮡc.getTypeArguments(Ꮡt).slice(index, endIndex), target.elementInfos.slice(index, endIndex), false);
}

internal static ж<Type> getKnownKeysOfTupleType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    nint fixedLength = t.TargetTupleType().fixedLength;
    var keys = new slice<ж<Type>>(fixedLength + 1);
    foreach (var i in range(fixedLength)) {
        keys[i] = Ꮡc.getStringLiteralType(strconv.Itoa(i));
    }
    keys[fixedLength] = Ꮡc.getIndexType(core.IfElse(t.TargetTupleType().@readonly, c.globalReadonlyArrayType, c.globalArrayType));
    return Ꮡc.getUnionType(keys);
}

internal static ж<Type> getRestArrayTypeOfTupleType(this ж<Checker> Ꮡc, ж<Type> t) {
    var restType = Ꮡc.getRestTypeOfTupleType(t); if (restType != nil) {
        return Ꮡc.createArrayType(restType);
    }
    return default;
}

internal static ж<Type> getThisTypeOfSignature(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (signature.thisParameter != nil) {
        return Ꮡc.getTypeOfSymbol(signature.thisParameter);
    }
    return default;
}

internal static bool isInstantiatedGenericParameter(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature, nint pos) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (signature.target == nil) {
        return false;
    }
    var t = Ꮡc.tryGetTypeAtPosition(signature.target, pos);
    return t != nil && Ꮡc.isGenericType(t);
}

internal static @string getParameterNameAtPosition(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature, nint pos) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();

    nint paramCount = len(signature.parameters) - core.IfElse<nint>(signatureHasRestParameter(ref (Ꮡsignature).DerefOrNull()), 1, 0);
    if (pos < paramCount) {
        return signature.parameters[pos].Name;
    }
    var restParameter = signature.parameters[paramCount];
    var restType = Ꮡc.getTypeOfSymbol(restParameter);
    if (isTupleType(restType)) {
        nint index = pos - paramCount;
        return c.getTupleElementLabel(restType.TargetTupleType().elementInfos[index], restParameter, index);
    }
    return restParameter.Name;
}

internal static @string getTupleElementLabel(this ref Checker c, TupleElementInfo elementInfo, ж<ast.Symbol> ᏑrestSymbol, nint index) {
    ref var restSymbol = ref ᏑrestSymbol.DerefOrNull();

    if (elementInfo.labeledDeclaration != nil) {
        return elementInfo.labeledDeclaration.Name().Text();
    }
    if (ᏑrestSymbol != nil && restSymbol.ValueDeclaration != nil && ast.IsParameterDeclaration(restSymbol.ValueDeclaration)) {
        return c.getTupleElementLabelFromBindingElement(restSymbol.ValueDeclaration, index, elementInfo.flags);
    }
    @string rootName = default;
    if (ᏑrestSymbol != nil) {
        rootName = restSymbol.Name;
    } else {
        rootName = "arg";
    }
    return rootName + "_"u8 + strconv.Itoa(index);
}

internal static @string getTupleElementLabelFromBindingElement(this ref Checker c, ж<ast.Node> Ꮡnode, nint index, ElementFlags elementFlags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Name() != nil) {
        switch (node.Name().Kind) {
        case var ᴛ when ᴛ == ast.KindIdentifier: {
            @string name = node.Name().Text();
            if (hasDotDotDotToken(Ꮡnode)) {
                if ((ElementFlags)(elementFlags & ElementFlagsVariable) != 0) {
                    return name;
                }
                return name + "_"u8 + strconv.Itoa(index);
            }

            if ((ElementFlags)(elementFlags & ElementFlagsFixed) != 0) {
                return name;
            }
            return name + "_n"u8;
        }
        case var ᴛ when ᴛ == ast.KindArrayBindingPattern: {
            if (hasDotDotDotToken(Ꮡnode)) {
                var elements = node.Name().Elements();
                var lastElement = core.LastOrNil(elements);
                var lastElementIsBindingElementRest = lastElement != nil && ast.IsBindingElement(lastElement) && hasDotDotDotToken(lastElement);
                nint elementCount = len(elements) - core.IfElse<nint>(lastElementIsBindingElementRest, 1, 0);
                if (index < elementCount) {
                    var element = elements[index];
                    if (ast.IsBindingElement(element)) {
                        return c.getTupleElementLabelFromBindingElement(element, index, elementFlags);
                    }
                } else if (lastElementIsBindingElementRest) {
                    return c.getTupleElementLabelFromBindingElement(lastElement, index - elementCount, elementFlags);
                }
            }
            break;
        }}

    }
    return "arg_"u8 + strconv.Itoa(index);
}

internal static ж<TypePredicate> getTypePredicateOfSignature(this ж<Checker> Ꮡc, ж<Signature> Ꮡsig) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var sig = ref Ꮡsig.DerefOrNull();

    if (sig.resolvedTypePredicate == nil) {
        switch (ᐧ) {
        case {} when sig.target != nil: {
            var targetTypePredicate = Ꮡc.getTypePredicateOfSignature(sig.target);
            if (targetTypePredicate != nil) {
                sig.resolvedTypePredicate = Ꮡc.instantiateTypePredicate(targetTypePredicate, sig.mapper);
            }
            break;
        }
        case {} when sig.composite != nil: {
            sig.resolvedTypePredicate = Ꮡc.getUnionOrIntersectionTypePredicate(sig.composite.signatures, sig.composite.isUnion);
            break;
        }
        default: {
            if (sig.declaration != nil) {
                var typeNode = sig.declaration.Type();
                switch (ᐧ) {
                case {} when typeNode != nil: {
                    if (ast.IsTypePredicateNode(typeNode)) {
                        sig.resolvedTypePredicate = Ꮡc.createTypePredicateFromTypePredicateNode(typeNode, Ꮡsig);
                    }
                    break;
                }
                case {} when ast.IsFunctionLikeDeclaration(sig.declaration) && (sig.resolvedReturnType == nil || (TypeFlags)(sig.resolvedReturnType.flags & TypeFlagsBoolean) != 0) && Ꮡc.getParameterCount(Ꮡsig) > 0: {
                    sig.resolvedTypePredicate = c.noTypePredicate;
                    sig.resolvedTypePredicate = Ꮡc.getTypePredicateFromBody(sig.declaration);
                    break;
                }}

            }
            break;
        }}

        if (sig.resolvedTypePredicate == nil) {
            sig.resolvedTypePredicate = c.noTypePredicate;
        }
    }
    if (sig.resolvedTypePredicate == c.noTypePredicate) {
        return default;
    }
    return sig.resolvedTypePredicate;
}

internal static ж<TypePredicate> getUnionOrIntersectionTypePredicate(this ж<Checker> Ꮡc, slice<ж<Signature>> signatures, bool isUnion) {
    ref var c = ref Ꮡc.DerefOrNull();
    var typesᴷ = new stackAppend<ж<Type>>();

    ж<TypePredicate> last = default;
    slice<ж<Type>> types = default;
    foreach (var (_, sig) in range(signatures)) {
        var pred = Ꮡc.getTypePredicateOfSignature(sig);
        if (pred != nil) {
            if (pred.kind != TypePredicateKindThis && pred.kind != TypePredicateKindIdentifier || last != nil && !c.typePredicateKindsMatch(last, pred)) {
                return default;
            }
            last = pred;
            types = append(typesᴷ, types, pred.t);
        } else {
            ж<Type> returnType = default;
            if (isUnion) {
                returnType = Ꮡc.getReturnTypeOfSignature(sig);
            }
            if (returnType != c.falseType && returnType != c.regularFalseType) {
                return default;
            }
        }
    }
    if (last == nil) {
        return default;
    }
    var compositeType = Ꮡc.getUnionOrIntersectionType(types, isUnion, UnionReductionLiteral);
    return c.newTypePredicate(last.kind, last.parameterName, last.parameterIndex, compositeType);
}

internal static bool typePredicateKindsMatch(this ref Checker c, ж<TypePredicate> Ꮡa, ж<TypePredicate> Ꮡb) {
    ref var a = ref Ꮡa.DerefOrNull();
    ref var b = ref Ꮡb.DerefOrNull();

    return a.kind == b.kind && a.parameterIndex == b.parameterIndex;
}

internal static ж<TypePredicate> createTypePredicateFromTypePredicateNode(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Signature> Ꮡsignature) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();

    var predicateNode = node.AsTypePredicateNode();
    ж<Type> t = default;
    if ((~predicateNode).Type != nil) {
        t = Ꮡc.getTypeFromTypeNode((~predicateNode).Type);
    }
    if (ast.IsThisTypeNode(predicateNode.ParameterName)) {
        var kindΔ1 = core.IfElse(predicateNode.AssertsModifier != nil, TypePredicateKindAssertsThis, TypePredicateKindThis);
        return c.newTypePredicate(kindΔ1, "", 0, t);
    }
    var kind = core.IfElse(predicateNode.AssertsModifier != nil, TypePredicateKindAssertsIdentifier, TypePredicateKindIdentifier);
    @string name = predicateNode.ParameterName.Text();
    nint index = core.FindIndex(signature.parameters, (ж<ast.Symbol> p) => p.Name == name);
    return c.newTypePredicate(kind, name, (int32)index, t);
}

internal static ж<TypePredicate> instantiateTypePredicate(this ж<Checker> Ꮡc, ж<TypePredicate> Ꮡpredicate, ж<TypeMapper> mapper) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var predicate = ref Ꮡpredicate.DerefOrNull();

    var t = Ꮡc.instantiateType(predicate.t, mapper);
    if (t == predicate.t) {
        return Ꮡpredicate;
    }
    return c.newTypePredicate(predicate.kind, predicate.parameterName, predicate.parameterIndex, t);
}

internal static ж<TypePredicate> newTypePredicate(this ref Checker c, TypePredicateKind kind, @string parameterName, int32 parameterIndex, ж<Type> t) {
    return Ꮡ(new TypePredicate(kind: kind, parameterIndex: parameterIndex, parameterName: parameterName, t: t));
}

internal static bool isResolvingReturnTypeOfSignature(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (signature.composite != nil && core.Some<ж<Signature>>(signature.composite.signatures, Ꮡc.isResolvingReturnTypeOfSignature)) {
        return true;
    }
    return signature.resolvedReturnType == nil && c.findResolutionCycleStartIndex(Ꮡsignature.OrTypedNil(), TypeSystemPropertyNameResolvedReturnType) >= 0;
}

internal static slice<ж<Signature>> findMatchingSignatures(this ж<Checker> Ꮡc, slice<slice<ж<Signature>>> signatureLists, ж<Signature> Ꮡsignature, nint listIndex) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (len(signature.typeParameters) != 0) {
        if (listIndex > 0) {
            return default;
        }
        for (nint i = 1; i < len(signatureLists); i++) {
            if (Ꮡc.findMatchingSignature(signatureLists[i], Ꮡsignature, false, false, false) == nil) {
                return default;
            }
        }
        return [Ꮡsignature];
    }
    slice<ж<Signature>> result = default;
    foreach (var i in range(len(signatureLists))) {
        ж<Signature> match = default;
        if (i == listIndex) {
            match = Ꮡsignature;
        } else {
            match = Ꮡc.findMatchingSignature(signatureLists[i], Ꮡsignature, false, false, true);
            if (match == nil) {
                match = Ꮡc.findMatchingSignature(signatureLists[i], Ꮡsignature, true, false, true);
            }
        }
        if (match == nil) {
            return default;
        }
        result = core.AppendIfUnique(result, match);
    }
    return result;
}

internal static ж<Signature> findMatchingSignature(this ж<Checker> Ꮡc, slice<ж<Signature>> signatureList, ж<Signature> signature, bool partialMatch, bool ignoreThisTypes, bool ignoreReturnTypes) {
    var compareTypes = core.IfElse<Func<ж<Type>, ж<Type>, Ternary>>(partialMatch, Ꮡc.compareTypesSubtypeOf, Ꮡc.compareTypesIdentical);
    foreach (var (_, s) in range(signatureList)) {
        if (Ꮡc.compareSignaturesIdentical(s, signature, partialMatch, ignoreThisTypes, ignoreReturnTypes, compareTypes) != 0) {
            return s;
        }
    }
    return default;
}

internal static Ternary compareSignaturesIdentical(this ж<Checker> Ꮡc, ж<Signature> Ꮡsource, ж<Signature> Ꮡtarget, bool partialMatch, bool ignoreThisTypes, bool ignoreReturnTypes, Func<ж<Type>, ж<Type>, Ternary> compareTypes) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if (Ꮡsource == Ꮡtarget) {
        return TernaryTrue;
    }
    if (!Ꮡc.isMatchingSignature(Ꮡsource, Ꮡtarget, partialMatch)) {
        return TernaryFalse;
    }
    if (len(source.typeParameters) != len(target.typeParameters)) {
        return TernaryFalse;
    }
    if (len(target.typeParameters) != 0) {
        var mapper = newTypeMapper(source.typeParameters, target.typeParameters);
        foreach (var i in range(len(target.typeParameters))) {
            var s = source.typeParameters[i];
            var t = target.typeParameters[i];
            if (!(s == t || compareTypes(Ꮡc.instantiateType(Ꮡc.getConstraintOrUnknownFromTypeParameter(s), mapper), Ꮡc.getConstraintOrUnknownFromTypeParameter(t)) != TernaryFalse && compareTypes(Ꮡc.instantiateType(Ꮡc.getDefaultOrUnknownFromTypeParameter(s), mapper), Ꮡc.getDefaultOrUnknownFromTypeParameter(t)) != TernaryFalse)) {
                return TernaryFalse;
            }
        }
        Ꮡsource = Ꮡc.instantiateSignatureEx(Ꮡsource, mapper, true); source = ref Ꮡsource.DerefOrNull();
    }
    var result = TernaryTrue;
    if (!ignoreThisTypes) {
        var sourceThisType = Ꮡc.getThisTypeOfSignature(Ꮡsource);
        if (sourceThisType != nil) {
            var targetThisType = Ꮡc.getThisTypeOfSignature(Ꮡtarget);
            if (targetThisType != nil) {
                var related = compareTypes(sourceThisType, targetThisType);
                if (related == TernaryFalse) {
                    return TernaryFalse;
                }
                result &= (Ternary)(related);
            }
        }
    }
    foreach (var i in range(Ꮡc.getParameterCount(Ꮡtarget))) {
        var s = Ꮡc.getTypeAtPosition(Ꮡsource, i);
        var t = Ꮡc.getTypeAtPosition(Ꮡtarget, i);
        var related = compareTypes(t, s);
        if (related == TernaryFalse) {
            return TernaryFalse;
        }
        result &= (Ternary)(related);
    }
    if (!ignoreReturnTypes) {
        var sourceTypePredicate = Ꮡc.getTypePredicateOfSignature(Ꮡsource);
        var targetTypePredicate = Ꮡc.getTypePredicateOfSignature(Ꮡtarget);
        if (sourceTypePredicate != nil || targetTypePredicate != nil) {
            result &= (Ternary)(c.compareTypePredicatesIdentical(sourceTypePredicate, targetTypePredicate, compareTypes));
        } else {
            result &= (Ternary)(compareTypes(Ꮡc.getReturnTypeOfSignature(Ꮡsource), Ꮡc.getReturnTypeOfSignature(Ꮡtarget)));
        }
    }
    return result;
}

internal static bool isMatchingSignature(this ж<Checker> Ꮡc, ж<Signature> source, ж<Signature> target, bool partialMatch) {
    nint sourceParameterCount = Ꮡc.getParameterCount(source);
    nint targetParameterCount = Ꮡc.getParameterCount(target);
    nint sourceMinArgumentCount = Ꮡc.getMinArgumentCount(source);
    nint targetMinArgumentCount = Ꮡc.getMinArgumentCount(target);
    var sourceHasRestParameter = Ꮡc.hasEffectiveRestParameter(source);
    var targetHasRestParameter = Ꮡc.hasEffectiveRestParameter(target);
    if (sourceParameterCount == targetParameterCount && sourceMinArgumentCount == targetMinArgumentCount && sourceHasRestParameter == targetHasRestParameter) {
        return true;
    }
    if (partialMatch && sourceMinArgumentCount <= targetMinArgumentCount) {
        return true;
    }
    return false;
}

internal static bool compareTypeParametersIdentical(this ж<Checker> Ꮡc, slice<ж<Type>> sourceParams, slice<ж<Type>> targetParams) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (len(sourceParams) != len(targetParams)) {
        return false;
    }
    var mapper = newTypeMapper(targetParams, sourceParams);
    foreach (var i in range(len(sourceParams))) {
        var source = sourceParams[i];
        var target = targetParams[i];
        if (source == target) {
            continue;
        }
        if (!Ꮡc.isTypeIdenticalTo(core.OrElse(Ꮡc.getConstraintFromTypeParameter(source), c.unknownType), Ꮡc.instantiateType(core.OrElse(Ꮡc.getConstraintFromTypeParameter(target), c.unknownType), mapper))) {
            return false;
        }
    }
    return true;
}

internal static Ternary compareTypePredicatesIdentical(this ref Checker c, ж<TypePredicate> Ꮡsource, ж<TypePredicate> Ꮡtarget, Func<ж<Type>, ж<Type>, Ternary> compareTypes) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    switch (ᐧ) {
    case {} when Ꮡsource == nil || Ꮡtarget == nil || !c.typePredicateKindsMatch(Ꮡsource, Ꮡtarget): {
        return TernaryFalse;
    }
    case {} when source.t == target.t: {
        return TernaryTrue;
    }
    case {} when source.t != nil && target.t != nil: {
        return compareTypes(source.t, target.t);
    }}

    return TernaryFalse;
}

internal static ж<Type> getEffectiveConstraintOfIntersection(this ж<Checker> Ꮡc, slice<ж<Type>> types, bool targetIsUnion) {
    var constraintsᴷ = new stackAppend<ж<Type>>();

    slice<ж<Type>> constraints = default;
    var hasDisjointDomainType = false;
    foreach (var (_, t) in range(types)) {
        if ((TypeFlags)(t.flags & TypeFlagsInstantiable) != 0) {
            var constraint = Ꮡc.getConstraintOfType(t);
            while (constraint != nil && (TypeFlags)(constraint.flags & ((TypeFlags)((TypeFlags)(TypeFlagsTypeParameter | TypeFlagsIndex) | TypeFlagsConditional))) != 0) {
                constraint = Ꮡc.getConstraintOfType(constraint);
            }
            if (constraint != nil) {
                constraints = append(constraintsᴷ, constraints, constraint);
                if (targetIsUnion) {
                    constraints = append(constraints, t);
                }
            }
        } else if ((TypeFlags)(t.flags & TypeFlagsDisjointDomains) != 0 || Ꮡc.IsEmptyAnonymousObjectType(t)) {
            hasDisjointDomainType = true;
        }
    }
    if (constraints != nil && (targetIsUnion || hasDisjointDomainType)) {
        if (hasDisjointDomainType) {
            foreach (var (_, t) in range(types)) {
                if ((TypeFlags)(t.flags & TypeFlagsDisjointDomains) != 0 || Ꮡc.IsEmptyAnonymousObjectType(t)) {
                    constraints = append(constraints, t);
                }
            }
        }
        return Ꮡc.getNormalizedType(Ꮡc.getIntersectionTypeEx(constraints, IntersectionFlagsNoConstraintReduction, nil), false);
    }
    return default;
}

internal static bool templateLiteralTypesDefinitelyUnrelated(this ref Checker c, ж<TemplateLiteralType> Ꮡsource, ж<TemplateLiteralType> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    @string sourceStart = source.texts[0];
    @string targetStart = target.texts[0];
    @string sourceEnd = source.texts[len(source.texts) - 1];
    @string targetEnd = target.texts[len(target.texts) - 1];
    nint startLen = min(len(sourceStart), len(targetStart));
    nint endLen = min(len(sourceEnd), len(targetEnd));
    return sourceStart.slice(0, startLen) != targetStart.slice(0, startLen) || sourceEnd.slice(len(sourceEnd) - endLen) != targetEnd.slice(len(targetEnd) - endLen);
}

internal static bool isTypeMatchedByTemplateLiteralType(this ж<Checker> Ꮡc, ж<Type> source, ж<TemplateLiteralType> Ꮡtarget, Func<ж<Type>, ж<Type>, bool, Ternary> compareTypes) {
    ref var target = ref Ꮡtarget.DerefOrNull();

    var inferences = Ꮡc.inferTypesFromTemplateLiteralType(source, Ꮡtarget, compareTypes);
    if (inferences != nil) {
        foreach (var (i, inference) in range(inferences)) {
            if (!Ꮡc.isValidTypeForTemplateLiteralPlaceholder(inference, target.types[i], compareTypes)) {
                return false;
            }
        }
        return true;
    }
    return false;
}

internal static slice<ж<Type>> inferTypesFromTemplateLiteralType(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<TemplateLiteralType> Ꮡtarget, Func<ж<Type>, ж<Type>, bool, Ternary> compareTypes) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(source.flags & TypeFlagsStringLiteral) != 0: {
        return Ꮡc.inferFromLiteralPartsToTemplateLiteral(slice<@string>([getStringLiteralValue(Ꮡsource)]), default, Ꮡtarget);
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsTemplateLiteral) != 0: {
        if (slices.Equal<slice<@string>, @string>(source.AsTemplateLiteralType().texts, target.texts)) {
            return core.MapIndex(source.AsTemplateLiteralType().types, ж<Type> (ж<Type> s, nint i) => {
                if (compareTypes(Ꮡc.getBaseConstraintOrType(s), Ꮡc.getBaseConstraintOrType(Ꮡtarget.Value.types[i]), false) != TernaryFalse) {
                    return s;
                }
                return Ꮡc.getStringLikeTypeForType(s);
            });
        }
        return Ꮡc.inferFromLiteralPartsToTemplateLiteral(source.AsTemplateLiteralType().texts, source.AsTemplateLiteralType().types, Ꮡtarget);
    }
    default: {
        return default;
    }}

}

internal static slice<ж<Type>> inferFromLiteralPartsToTemplateLiteral(this ж<Checker> Ꮡc, slice<@string> sourceTexts, slice<ж<Type>> sourceTypes, ж<TemplateLiteralType> Ꮡtarget) {
    ref var target = ref Ꮡtarget.DerefOrNull();

    nint lastSourceIndex = len(sourceTexts) - 1;
    @string sourceStartText = sourceTexts[0];
    @string sourceEndText = sourceTexts[lastSourceIndex];
    var targetTexts = target.texts;
    nint lastTargetIndex = len(targetTexts) - 1;
    @string targetStartText = targetTexts[0];
    @string targetEndText = targetTexts[lastTargetIndex];
    if (lastSourceIndex == 0 && len(sourceStartText) < len(targetStartText) + len(targetEndText) || !strings.HasPrefix(sourceStartText, targetStartText) || !strings.HasSuffix(sourceEndText, targetEndText)) {
        return default;
    }
    @string remainingEndText = sourceEndText.slice(0, len(sourceEndText) - len(targetEndText));
    nint seg = 0;
    nint pos = len(targetStartText);
    ref var matches = ref heap<slice<ж<Type>>>(out var Ꮡmatches);
    var sourceTextsʗ1 = sourceTexts;
    @string getSourceText(nint index) {
        if (index < lastSourceIndex) {
            return sourceTextsʗ1[index];
        }
        return remainingEndText;
    }
    var getSourceTextʗ1 = getSourceText;
    var sourceTextsʗ2 = sourceTexts;
    var sourceTypesʗ1 = sourceTypes;
    void addMatch(nint s, nint p) {
        ж<Type> matchType = default;
        if (s == seg) {
            matchType = Ꮡc.getStringLiteralType(stringutil.CombineSurrogatePairs(getSourceTextʗ1(s).slice(pos, p)));
        } else {
            var matchTexts = new slice<@string>(s - seg + 1);
            matchTexts[0] = sourceTextsʗ2[seg].slice(pos);
            copy(matchTexts[1..], sourceTextsʗ2.slice(seg + 1, s));
            matchTexts[s - seg] = getSourceTextʗ1(s).slice(0, p);
            matchType = Ꮡc.getTemplateLiteralType(matchTexts, sourceTypesʗ1.slice(seg, s));
        }
        Ꮡmatches.ValueSlot = append(Ꮡmatches.ValueSlot, matchType);
        seg = s;
        pos = p;
    }
    for (nint i = 1; i < lastTargetIndex; i++) {
        @string delim = targetTexts[i];
        if (len(delim) > 0) {
            nint s = seg;
            nint p = pos;
            for (;;) {
                nint d = strings.Index(getSourceText(s).slice(p), delim);
                if (d >= 0) {
                    p += d;
                    break;
                }
                s++;
                if (s == len(sourceTexts)) {
                    return default;
                }
                p = 0;
            }
            addMatch(s, p);
            pos += len(delim);
        } else {
            @string sourceText = getSourceText(seg); if (pos < len(sourceText)) {
                var (_, size) = stringutil.DecodeJSStringRune(sourceText.slice(pos));
                addMatch(seg, pos + size);
            } else if (seg < lastSourceIndex) {
                addMatch(seg + 1, 0);
            } else {
                return default;
            }
        }
    }
    addMatch(lastSourceIndex, len(getSourceText(lastSourceIndex)));
    return matches;
}

internal static ж<Type> getStringLikeTypeForType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & ((TypeFlags)(TypeFlagsAny | TypeFlagsStringLike))) != 0) {
        return Ꮡt;
    }
    return Ꮡc.getTemplateLiteralType(["", ""], [Ꮡt]);
}

internal static bool isValidTypeForTemplateLiteralPlaceholder(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, Func<ж<Type>, ж<Type>, bool, Ternary> compareTypes) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(target.flags & TypeFlagsIntersection) != 0: {
        return core.Every(target.Types(), (ж<Type> t) => t == Ꮡc.Value.emptyTypeLiteralType || Ꮡc.isValidTypeForTemplateLiteralPlaceholder(Ꮡsource, t, compareTypes));
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsString) != 0 || compareTypes(Ꮡsource, Ꮡtarget, false) != TernaryFalse: {
        return true;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsStringLiteral) != 0: {
        @string value = getStringLiteralValue(Ꮡsource);
        return (TypeFlags)(target.flags & TypeFlagsNumber) != 0 && isValidNumberString(value, false) || (TypeFlags)(target.flags & TypeFlagsBigInt) != 0 && isValidBigIntString(value, false) || (TypeFlags)(target.flags & ((TypeFlags)(TypeFlagsBooleanLiteral | TypeFlagsNullable))) != 0 && value == target.AsIntrinsicType().intrinsicName || (TypeFlags)(target.flags & TypeFlagsStringMapping) != 0 && Ꮡc.isMemberOfStringMapping(Ꮡsource, Ꮡtarget) || (TypeFlags)(target.flags & TypeFlagsTemplateLiteral) != 0 && Ꮡc.isTypeMatchedByTemplateLiteralType(Ꮡsource, target.AsTemplateLiteralType(), compareTypes);
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsTemplateLiteral) != 0: {
        var texts = source.AsTemplateLiteralType().texts;
        return len(texts) == 2 && texts[0] == "" && texts[1] == "" && compareTypes(source.AsTemplateLiteralType().types[0], Ꮡtarget, false) != TernaryFalse;
    }}

    return false;
}

internal static bool isMemberOfStringMapping(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> Ꮡtarget) {
    ref var target = ref Ꮡtarget.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(target.flags & TypeFlagsAny) != 0: {
        return true;
    }
    case {} when (TypeFlags)(target.flags & ((TypeFlags)(TypeFlagsString | TypeFlagsTemplateLiteral))) != 0: {
        return Ꮡc.isTypeAssignableTo(source, Ꮡtarget);
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsStringMapping) != 0: {
        var (mapped, inner) = Ꮡc.applyTargetStringMappingToSource(source, Ꮡtarget);
        return mapped == source && Ꮡc.isMemberOfStringMapping(source, inner);
    }}

    return false;
}

internal static (ж<Type>, ж<Type>) applyTargetStringMappingToSource(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var inner = target.AsStringMappingType().target;
    if ((TypeFlags)(inner.flags & TypeFlagsStringMapping) != 0) {
        (Ꮡsource, inner) = Ꮡc.applyTargetStringMappingToSource(Ꮡsource, inner); source = ref Ꮡsource.DerefOrNull();
    }
    return (Ꮡc.getStringMappingType(target.symbol, Ꮡsource), inner);
}

} partial class checker_package {

internal static @string visibilityToString(ast.ModifierFlags flags) {
    if (flags == ast.ModifierFlagsPrivate) {
        return "private";
    }
    if (flags == ast.ModifierFlagsProtected) {
        return "protected";
    }
    return "public";
}

partial struct errorState {
    internal ж<ErrorChain> errorChain;
    internal slice<ж<ast.Diagnostic>> relatedInfo;
}

partial struct ErrorChain {
    internal ж<ErrorChain> next;
    internal ж<diagnostics.Message> message;
    internal slice<any> args;
}

partial struct Relater {
    internal ж<Checker> c;
    internal ж<Relation> relation;
    internal ж<ast.Node> errorNode;
    internal ж<ErrorChain> errorChain;
    internal slice<ж<ast.Diagnostic>> relatedInfo;
    internal slice<CacheHashKey> maybeKeys;
    internal collections.Set<CacheHashKey> maybeKeysSet;
    internal slice<ж<Type>> sourceStack;
    internal slice<ж<Type>> targetStack;
    internal nint maybeCount;
    internal nint sourceDepth;
    internal nint targetDepth;
    internal ExpandingFlags expandingFlags;
    internal bool overflow;
    internal nint relationCount;
    internal ж<Relater> next;
}

} partial class checkerᴛChecker {

internal static ж<Relater> getRelater(this ж<Checker> Ꮡc) {
    ref var c = ref Ꮡc.DerefOrNull();

    var r = c.freeRelater;
    if (r == nil) {
        r = Ꮡ(new Relater(c: Ꮡc));
    }
    c.freeRelater = r.next;
    return r;
}

internal static void putRelater(this ж<Checker> Ꮡc, ж<Relater> Ꮡr) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var r = ref Ꮡr.DerefOrNull();

    Ꮡr.ᏑmaybeKeysSet.Clear();
    r = new Relater(
        c: Ꮡc,
        maybeKeys: r.maybeKeys[..0],
        maybeKeysSet: r.maybeKeysSet,
        sourceStack: r.sourceStack[..0],
        targetStack: r.targetStack[..0],
        next: c.freeRelater
    );
    c.freeRelater = Ꮡr;
}

} partial class checkerᴛRelater {

internal static Ternary isRelatedToSimple(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target) {
    return Ꮡr.isRelatedToEx(source, target, RecursionFlagsBoth, false, nil, IntersectionStateNone);
}

internal static Ternary isRelatedToWorker(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target, bool reportErrors) {
    return Ꮡr.isRelatedToEx(source, target, RecursionFlagsBoth, reportErrors, nil, IntersectionStateNone);
}

internal static Ternary isRelatedTo(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target, RecursionFlags recursionFlags, bool reportErrors) {
    return Ꮡr.isRelatedToEx(source, target, recursionFlags, reportErrors, nil, IntersectionStateNone);
}

internal static Ternary isRelatedToEx(this ж<Relater> Ꮡr, ж<Type> ᏑoriginalSource, ж<Type> ᏑoriginalTarget, RecursionFlags recursionFlags, bool reportErrors, ж<diagnostics.Message> headMessage, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var originalSource = ref ᏑoriginalSource.DerefOrNull();
    ref var originalTarget = ref ᏑoriginalTarget.DerefOrNull();

    if (ᏑoriginalSource == ᏑoriginalTarget) {
        return TernaryTrue;
    }
    if ((TypeFlags)(originalSource.flags & TypeFlagsObject) != 0 && (TypeFlags)(originalTarget.flags & TypeFlagsPrimitive) != 0) {
        if (r.relation == r.c.comparableRelation && (TypeFlags)(originalTarget.flags & TypeFlagsNever) == 0 && r.c.isSimpleTypeRelatedTo(ᏑoriginalTarget, ᏑoriginalSource, r.relation, default) || r.c.isSimpleTypeRelatedTo(ᏑoriginalSource, ᏑoriginalTarget, r.relation, core.IfElse<Actionꓸꓸꓸ<ж<diagnostics.Message>, any>>(reportErrors, Ꮡr.reportError, (Actionꓸꓸꓸ<ж<diagnostics.Message>, any>)(default)))) {
            return TernaryTrue;
        }
        if (reportErrors) {
            r.reportErrorResults(ᏑoriginalSource, ᏑoriginalTarget, ᏑoriginalSource, ᏑoriginalTarget, headMessage);
        }
        return TernaryFalse;
    }
    var source = r.c.getNormalizedType(ᏑoriginalSource, false);
    var target = r.c.getNormalizedType(ᏑoriginalTarget, true);
    if (source == target) {
        return TernaryTrue;
    }
    if (r.relation == r.c.identityRelation) {
        if (source.flags != target.flags) {
            return TernaryFalse;
        }
        if ((TypeFlags)(source.flags & TypeFlagsSingleton) != 0) {
            return TernaryTrue;
        }
        r.traceUnionsOrIntersectionsTooLarge(source, target);
        return Ꮡr.recursiveTypeRelatedTo(source, target, false, IntersectionStateNone, recursionFlags);
    }
    if ((TypeFlags)(source.flags & TypeFlagsTypeParameter) != 0 && r.c.getConstraintOfType(source) == target) {
        return TernaryTrue;
    }
    if ((TypeFlags)(source.flags & TypeFlagsDefinitelyNonNullable) != 0 && (TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
        var types = target.Types();
        ж<Type> candidate = default;
        switch (ᐧ) {
        case {} when len(types) == 2 && (TypeFlags)(types[0].flags & TypeFlagsNullable) != 0: {
            candidate = types[1];
            break;
        }
        case {} when len(types) == 3 && (TypeFlags)(types[0].flags & TypeFlagsNullable) != 0 && (TypeFlags)(types[1].flags & TypeFlagsNullable) != 0: {
            candidate = types[2];
            break;
        }}

        if (candidate != nil && (TypeFlags)(candidate.flags & TypeFlagsNullable) == 0) {
            target = r.c.getNormalizedType(candidate, true);
            if (source == target) {
                return TernaryTrue;
            }
        }
    }
    if (r.relation == r.c.comparableRelation && (TypeFlags)(target.flags & TypeFlagsNever) == 0 && r.c.isSimpleTypeRelatedTo(target, source, r.relation, default) || r.c.isSimpleTypeRelatedTo(source, target, r.relation, core.IfElse<Actionꓸꓸꓸ<ж<diagnostics.Message>, any>>(reportErrors, Ꮡr.reportError, (Actionꓸꓸꓸ<ж<diagnostics.Message>, any>)(default)))) {
        return TernaryTrue;
    }
    if ((TypeFlags)(source.flags & TypeFlagsStructuredOrInstantiable) != 0 || (TypeFlags)(target.flags & TypeFlagsStructuredOrInstantiable) != 0) {
        var isPerformingExcessPropertyChecks = (IntersectionState)(intersectionState & IntersectionStateTarget) == 0 && isObjectLiteralType(ref (source).DerefOrNull()) && (ObjectFlags)(source.objectFlags & ObjectFlagsFreshLiteral) != 0;
        if (isPerformingExcessPropertyChecks) {
            if (Ꮡr.hasExcessProperties(source, target, reportErrors)) {
                if (reportErrors) {
                    r.reportRelationError(headMessage, source, core.IfElse(originalTarget.alias != nil, ᏑoriginalTarget, target));
                }
                return TernaryFalse;
            }
        }
        var isPerformingCommonPropertyChecks = (r.relation != r.c.comparableRelation || isUnitType(source)) && (IntersectionState)(intersectionState & IntersectionStateTarget) == 0 && (TypeFlags)(source.flags & ((TypeFlags)((TypeFlags)(TypeFlagsPrimitive | TypeFlagsObject) | TypeFlagsIntersection))) != 0 && source != r.c.globalObjectType && (TypeFlags)(target.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection))) != 0 && r.c.isWeakType(target) && (len(r.c.getPropertiesOfType(source)) > 0 || r.c.typeHasCallOrConstructSignatures(source));
        var isComparingJsxAttributes = (ObjectFlags)(source.objectFlags & ObjectFlagsJsxAttributes) != 0;
        if (isPerformingCommonPropertyChecks && !r.c.hasCommonProperties(source, target, isComparingJsxAttributes)) {
            if (reportErrors) {
                @string sourceString = r.c.TypeToString(core.IfElse(originalSource.alias != nil, ᏑoriginalSource, source));
                @string targetString = r.c.TypeToString(core.IfElse(originalTarget.alias != nil, ᏑoriginalTarget, target));
                var calls = r.c.getSignaturesOfType(source, SignatureKindCall);
                var constructs = r.c.getSignaturesOfType(source, SignatureKindConstruct);
                if (len(calls) > 0 && Ꮡr.isRelatedTo(r.c.getReturnTypeOfSignature(calls[0]), target, RecursionFlagsSource, false) != TernaryFalse || len(constructs) > 0 && Ꮡr.isRelatedTo(r.c.getReturnTypeOfSignature(constructs[0]), target, RecursionFlagsSource, false) != TernaryFalse) {
                    r.reportError(diagnostics.Value_of_type_0_has_no_properties_in_common_with_type_1_Did_you_mean_to_call_it, sourceString, targetString);
                } else {
                    r.reportError(diagnostics.Type_0_has_no_properties_in_common_with_type_1, sourceString, targetString);
                }
            }
            return TernaryFalse;
        }
        r.traceUnionsOrIntersectionsTooLarge(source, target);
        var skipCaching = (TypeFlags)(source.flags & TypeFlagsUnion) != 0 && len(source.Types()) < 4 && (TypeFlags)(target.flags & TypeFlagsUnion) == 0 || (TypeFlags)(target.flags & TypeFlagsUnion) != 0 && len(target.Types()) < 4 && (TypeFlags)(source.flags & TypeFlagsStructuredOrInstantiable) == 0;
        Ternary result = default;
        if (skipCaching) {
            result = Ꮡr.unionOrIntersectionRelatedTo(source, target, reportErrors, intersectionState);
        } else {
            result = Ꮡr.recursiveTypeRelatedTo(source, target, reportErrors, intersectionState, recursionFlags);
        }
        if (result != TernaryFalse) {
            return result;
        }
    }
    if (reportErrors) {
        r.reportErrorResults(ᏑoriginalSource, ᏑoriginalTarget, source, target, headMessage);
    }
    return TernaryFalse;
}

internal static bool hasExcessProperties(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, bool reportErrors) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if (!isExcessPropertyCheckTarget(Ꮡtarget) || !r.c.noImplicitAny && (ObjectFlags)(target.objectFlags & ObjectFlagsJSLiteral) != 0) {
        return false;
    }
    var isComparingJsxAttributes = (ObjectFlags)(source.objectFlags & ObjectFlagsJsxAttributes) != 0;
    if ((r.relation == r.c.assignableRelation || r.relation == r.c.comparableRelation) && (r.c.isTypeSubsetOf(r.c.globalObjectType, Ꮡtarget) || (!isComparingJsxAttributes && r.c.isEmptyObjectType(Ꮡtarget)))) {
        return false;
    }
    var reducedTarget = Ꮡtarget;
    slice<ж<Type>> checkTypes = default;
    if ((TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
        reducedTarget = r.c.findMatchingDiscriminantType(Ꮡsource, Ꮡtarget, Ꮡr.isRelatedToSimple);
        if (reducedTarget == nil) {
            reducedTarget = r.c.filterPrimitivesIfContainsNonPrimitive(Ꮡtarget);
        }
        checkTypes = reducedTarget.Distributed();
    }
    foreach (var (_, prop) in range(r.c.getPropertiesOfType(Ꮡsource))) {
        if (shouldCheckAsExcessProperty(ref (prop).DerefOrNull(), ref (source.symbol).DerefOrNull()) && !isIgnoredJsxProperty(ref (Ꮡsource).DerefOrNull(), ref (prop).DerefOrNull())) {
            if (!r.c.isKnownProperty(reducedTarget, prop.Name, isComparingJsxAttributes)) {
                if (reportErrors) {
                    var errorTarget = r.c.filterType(reducedTarget, isExcessPropertyCheckTarget);
                    if (r.errorNode == nil) {
                        throw panic("No errorNode in hasExcessProperties");
                    }
                    if (ast.IsJsxAttributes(r.errorNode) || ast.IsJsxOpeningLikeElement(r.errorNode) || ast.IsJsxOpeningLikeElement(r.errorNode.Parent)) {
                        if (prop.ValueDeclaration != nil && ast.IsJsxAttribute(prop.ValueDeclaration) && ast.GetSourceFileOfNode(r.errorNode) == ast.GetSourceFileOfNode(prop.ValueDeclaration.Name())) {
                            r.errorNode = prop.ValueDeclaration.Name();
                        }
                        @string propName = r.c.symbolToString(prop);
                        var suggestionSymbol = r.c.getSuggestedSymbolForNonexistentJSXAttribute(propName, errorTarget);
                        if (suggestionSymbol != nil) {
                            r.reportError(diagnostics.Property_0_does_not_exist_on_type_1_Did_you_mean_2, propName, r.c.TypeToString(errorTarget), r.c.symbolToString(suggestionSymbol));
                        } else {
                            r.reportError(diagnostics.Property_0_does_not_exist_on_type_1, propName, r.c.TypeToString(errorTarget));
                        }
                    } else {
                        ref var objectLiteralDeclaration = ref heap<ж<ast.Node>>(out var ᏑobjectLiteralDeclaration);
                        if (source.symbol != nil) {
                            objectLiteralDeclaration = core.FirstOrNil(source.symbol.Declarations);
                        }
                        @string suggestion = default;
                        if (prop.ValueDeclaration != nil && ast.IsObjectLiteralElement(prop.ValueDeclaration) && ast.FindAncestor(prop.ValueDeclaration, (ж<ast.Node> d) => d == ᏑobjectLiteralDeclaration.ValueSlot) != nil && ast.GetSourceFileOfNode(objectLiteralDeclaration) == ast.GetSourceFileOfNode(r.errorNode)) {
                            var name = prop.ValueDeclaration.Name();
                            r.errorNode = name;
                            if (ast.IsIdentifier(name)) {
                                suggestion = r.c.getSuggestionForNonexistentProperty(name.Text(), errorTarget);
                            }
                        }
                        if (suggestion != ""u8) {
                            r.reportError(diagnostics.Object_literal_may_only_specify_known_properties_but_0_does_not_exist_in_type_1_Did_you_mean_to_write_2, r.c.symbolToString(prop), r.c.TypeToString(errorTarget), suggestion);
                        } else {
                            r.reportError(diagnostics.Object_literal_may_only_specify_known_properties_and_0_does_not_exist_in_type_1, r.c.symbolToString(prop), r.c.TypeToString(errorTarget));
                        }
                    }
                }
                return true;
            }
            if (checkTypes != nil && Ꮡr.isRelatedTo(r.c.getTypeOfSymbol(prop), r.c.getTypeOfPropertyInTypes(checkTypes, prop.Name), RecursionFlagsBoth, reportErrors) == TernaryFalse) {
                if (reportErrors) {
                    r.reportError(diagnostics.Types_of_property_0_are_incompatible, r.c.symbolToString(prop));
                }
                return true;
            }
        }
    }
    return false;
}

} partial class checkerᴛChecker {

internal static ж<Type> getTypeOfPropertyInTypes(this ж<Checker> Ꮡc, slice<ж<Type>> types, @string name) {
    var propTypesᴷ = new stackAppend<ж<Type>>();

    slice<ж<Type>> propTypes = default;
    foreach (var (_, t) in range(types)) {
        propTypes = append(propTypesᴷ, propTypes, Ꮡc.getTypeOfPropertyInType(t, name));
    }
    return Ꮡc.getUnionType(propTypes);
}

internal static ж<Type> getTypeOfPropertyInType(this ж<Checker> Ꮡc, ж<Type> Ꮡt, @string name) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    Ꮡt = Ꮡc.getApparentType(Ꮡt); t = ref Ꮡt.DerefOrNull();
    ж<ast.Symbol> prop = default;
    if ((TypeFlags)(t.flags & TypeFlagsUnionOrIntersection) != 0) {
        prop = Ꮡc.getPropertyOfUnionOrIntersectionType(Ꮡt, name, false);
    } else {
        prop = Ꮡc.getPropertyOfObjectType(Ꮡt, name);
    }
    if (prop != nil) {
        return Ꮡc.getTypeOfSymbol(prop);
    }
    var indexInfo = Ꮡc.getApplicableIndexInfoForName(Ꮡt, name);
    if (indexInfo != nil) {
        return indexInfo.valueType;
    }
    return c.undefinedType;
}

} partial class checker_package {

internal static bool shouldCheckAsExcessProperty(ref ast.Symbol prop, ref ast.Symbol container) {
    return prop.ValueDeclaration != nil && container.ValueDeclaration != nil && prop.ValueDeclaration.Parent == container.ValueDeclaration;
}

internal static bool isIgnoredJsxProperty(ref Type source, ref ast.Symbol sourceProp) {
    return (ObjectFlags)(source.objectFlags & ObjectFlagsJsxAttributes) != 0 && isHyphenatedJsxName(sourceProp.Name);
}

} partial class checkerᴛChecker {

internal static bool isTypeSubsetOf(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    return Ꮡsource == Ꮡtarget || (TypeFlags)(source.flags & TypeFlagsNever) != 0 || (TypeFlags)(target.flags & TypeFlagsUnion) != 0 && Ꮡc.isTypeSubsetOfUnion(Ꮡsource, Ꮡtarget);
}

internal static bool isTypeSubsetOfUnion(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsUnion) != 0) {
        foreach (var (_, t) in range(source.Types())) {
            if (!containsType(target.Types(), t)) {
                return false;
            }
        }
        return true;
    }
    if ((TypeFlags)(source.flags & TypeFlagsEnumLike) != 0 && Ꮡc.getBaseTypeOfEnumLikeType(Ꮡsource) == Ꮡtarget) {
        return true;
    }
    return containsType(target.Types(), Ꮡsource);
}

} partial class checkerᴛRelater {

internal static Ternary unionOrIntersectionRelatedTo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsUnion) != 0) {
        if ((TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
            var sourceOrigin = source.AsUnionType().origin;
            if (sourceOrigin != nil && (TypeFlags)(sourceOrigin.flags & TypeFlagsIntersection) != 0 && target.alias != nil && slices.Contains(sourceOrigin.Types(), Ꮡtarget)) {
                return TernaryTrue;
            }
            var targetOrigin = target.AsUnionType().origin;
            if (targetOrigin != nil && (TypeFlags)(targetOrigin.flags & TypeFlagsUnion) != 0 && source.alias != nil && slices.Contains(targetOrigin.Types(), Ꮡsource)) {
                return TernaryTrue;
            }
        }
        if (r.relation == r.c.comparableRelation) {
            return Ꮡr.someTypeRelatedToType(Ꮡsource, Ꮡtarget, reportErrors && (TypeFlags)(source.flags & TypeFlagsPrimitive) == 0, intersectionState);
        }
        return Ꮡr.eachTypeRelatedToType(Ꮡsource, Ꮡtarget, reportErrors && (TypeFlags)(source.flags & TypeFlagsPrimitive) == 0, intersectionState);
    }
    if ((TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
        return Ꮡr.typeRelatedToSomeType(r.c.getRegularTypeOfObjectLiteral(Ꮡsource), Ꮡtarget, reportErrors && (TypeFlags)(source.flags & TypeFlagsPrimitive) == 0 && (TypeFlags)(target.flags & TypeFlagsPrimitive) == 0, intersectionState);
    }
    if ((TypeFlags)(target.flags & TypeFlagsIntersection) != 0) {
        return Ꮡr.typeRelatedToEachType(Ꮡsource, Ꮡtarget, reportErrors, IntersectionStateTarget);
    }
    if (r.relation == r.c.comparableRelation && (TypeFlags)(target.flags & TypeFlagsPrimitive) != 0) {
        var constraints = core.SameMap(source.Types(), ж<Type> (ж<Type> t) => {
            if ((TypeFlags)(t.flags & TypeFlagsInstantiable) != 0) {
                var constraint = Ꮡr.Value.c.getBaseConstraintOfType(t);
                if (constraint != nil) {
                    return constraint;
                }
                return Ꮡr.Value.c.unknownType;
            }
            return t;
        });
        if (!core.Same(constraints, source.Types())) {
            Ꮡsource = r.c.getIntersectionType(constraints); source = ref Ꮡsource.DerefOrNull();
            if ((TypeFlags)(source.flags & TypeFlagsNever) != 0) {
                return TernaryFalse;
            }
            if ((TypeFlags)(source.flags & TypeFlagsIntersection) == 0) {
                var result = Ꮡr.isRelatedTo(Ꮡsource, Ꮡtarget, RecursionFlagsSource, false);
                if (result != TernaryFalse) {
                    return result;
                }
                return Ꮡr.isRelatedTo(Ꮡtarget, Ꮡsource, RecursionFlagsSource, false);
            }
        }
    }
    return Ꮡr.someTypeRelatedToType(Ꮡsource, Ꮡtarget, false, IntersectionStateSource);
}

internal static Ternary someTypeRelatedToType(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> target, bool reportErrors, IntersectionState intersectionState) {
    ref var source = ref Ꮡsource.DerefOrNull();

    var sourceTypes = source.Types();
    if ((TypeFlags)(source.flags & TypeFlagsUnion) != 0 && containsType(sourceTypes, target)) {
        return TernaryTrue;
    }
    foreach (var (i, t) in range(sourceTypes)) {
        var related = Ꮡr.isRelatedToEx(t, target, RecursionFlagsSource, reportErrors && i == len(sourceTypes) - 1, nil, intersectionState);
        if (related != TernaryFalse) {
            return related;
        }
    }
    return TernaryFalse;
}

internal static Ternary eachTypeRelatedToType(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> target, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();

    var result = TernaryTrue;
    var sourceTypes = source.Types();
    var strippedTarget = r.getUndefinedStrippedTargetIfNeeded(Ꮡsource, target);
    slice<ж<Type>> strippedTypes = default;
    if ((TypeFlags)(strippedTarget.flags & TypeFlagsUnion) != 0) {
        strippedTypes = strippedTarget.Types();
    }
    foreach (var (i, sourceType) in range(sourceTypes)) {
        if ((TypeFlags)(strippedTarget.flags & TypeFlagsUnion) != 0 && len(sourceTypes) >= len(strippedTypes) && len(sourceTypes) % len(strippedTypes) == 0) {
            var relatedΔ1 = Ꮡr.isRelatedToEx(sourceType, strippedTypes[i % len(strippedTypes)], RecursionFlagsBoth, false, nil, intersectionState);
            if (relatedΔ1 != TernaryFalse) {
                result &= (Ternary)(relatedΔ1);
                continue;
            }
        }
        var related = Ꮡr.isRelatedToEx(sourceType, target, RecursionFlagsSource, reportErrors, nil, intersectionState);
        if (related == TernaryFalse) {
            return TernaryFalse;
        }
        result &= (Ternary)(related);
    }
    return result;
}

internal static ж<Type> getUndefinedStrippedTargetIfNeeded(this ref Relater r, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsUnion) != 0 && (TypeFlags)(target.flags & TypeFlagsUnion) != 0 && (TypeFlags)(source.Types()[0].flags & TypeFlagsUndefined) == 0 && (TypeFlags)(target.Types()[0].flags & TypeFlagsUndefined) != 0) {
        return r.c.extractTypesOfKind(Ꮡtarget, ~((TypeFlags)TypeFlagsUndefined));
    }
    return Ꮡtarget;
}

internal static Ternary typeRelatedToSomeType(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var targetTypes = target.Types();
    if ((TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
        if (containsType(targetTypes, Ꮡsource)) {
            return TernaryTrue;
        }
        if (r.relation != r.c.comparableRelation && (ObjectFlags)(target.objectFlags & ObjectFlagsPrimitiveUnion) != 0 && (TypeFlags)(source.flags & TypeFlagsEnumLiteral) == 0 && ((TypeFlags)(source.flags & ((TypeFlags)((TypeFlags)(TypeFlagsStringLiteral | TypeFlagsBooleanLiteral) | TypeFlagsBigIntLiteral))) != 0 || (r.relation == r.c.subtypeRelation || r.relation == r.c.strictSubtypeRelation) && (TypeFlags)(source.flags & TypeFlagsNumberLiteral) != 0)) {
            ж<Type> alternateForm = default;
            if (Ꮡsource == source.AsLiteralType().regularType) {
                alternateForm = source.AsLiteralType().freshType;
            } else {
                alternateForm = source.AsLiteralType().regularType;
            }
            ж<Type> primitive = default;
            switch (ᐧ) {
            case {} when (TypeFlags)(source.flags & TypeFlagsStringLiteral) != 0: {
                primitive = r.c.stringType;
                break;
            }
            case {} when (TypeFlags)(source.flags & TypeFlagsNumberLiteral) != 0: {
                primitive = r.c.numberType;
                break;
            }
            case {} when (TypeFlags)(source.flags & TypeFlagsBigIntLiteral) != 0: {
                primitive = r.c.bigintType;
                break;
            }}

            if (primitive != nil && containsType(targetTypes, primitive) || alternateForm != nil && containsType(targetTypes, alternateForm)) {
                return TernaryTrue;
            }
            return TernaryFalse;
        }
        var match = r.c.getMatchingUnionConstituentForType(Ꮡtarget, Ꮡsource);
        if (match != nil) {
            var related = Ꮡr.isRelatedToEx(Ꮡsource, match, RecursionFlagsTarget, false, nil, intersectionState);
            if (related != TernaryFalse) {
                return related;
            }
        }
    }
    foreach (var (_, t) in range(targetTypes)) {
        var related = Ꮡr.isRelatedToEx(Ꮡsource, t, RecursionFlagsTarget, false, nil, intersectionState);
        if (related != TernaryFalse) {
            return related;
        }
    }
    if (reportErrors) {
        var bestMatchingType = r.c.getBestMatchingType(Ꮡsource, Ꮡtarget, Ꮡr.isRelatedToSimple);
        if (bestMatchingType != nil) {
            Ꮡr.isRelatedToEx(Ꮡsource, bestMatchingType, RecursionFlagsTarget, true, nil, intersectionState);
        }
    }
    return TernaryFalse;
}

internal static Ternary typeRelatedToEachType(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> Ꮡtarget, bool reportErrors, IntersectionState intersectionState) {
    ref var target = ref Ꮡtarget.DerefOrNull();

    var result = TernaryTrue;
    var targetTypes = target.Types();
    foreach (var (_, targetType) in range(targetTypes)) {
        var related = Ꮡr.isRelatedToEx(source, targetType, RecursionFlagsTarget, reportErrors, nil, intersectionState);
        if (related == TernaryFalse) {
            return TernaryFalse;
        }
        result &= (Ternary)(related);
    }
    return result;
}

internal static Ternary eachTypeRelatedToSomeType(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> target) {
    ref var source = ref Ꮡsource.DerefOrNull();

    var result = TernaryTrue;
    var sourceTypes = source.Types();
    foreach (var (_, sourceType) in range(sourceTypes)) {
        var related = Ꮡr.typeRelatedToSomeType(sourceType, target, false, IntersectionStateNone);
        if (related == TernaryFalse) {
            return TernaryFalse;
        }
        result &= (Ternary)(related);
    }
    return result;
}

internal static Ternary recursiveTypeRelatedTo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, bool reportErrors, IntersectionState intersectionState, RecursionFlags recursionFlags) {
    GoFrame ᒐ = default;
    try {
        ref var r = ref Ꮡr.DerefOrNull();
        ref var source = ref Ꮡsource.DerefOrNull();
        ref var target = ref Ꮡtarget.DerefOrNull();

        if (r.overflow) {
            return TernaryFalse;
        }
        var (id, constrained) = getRelationKey(Ꮡsource, Ꮡtarget, intersectionState, r.relation == r.c.identityRelation, false);
        var entry = r.relation.get(id); if (entry != RelationComparisonResultNone) {
            if (reportErrors && (RelationComparisonResult)(entry & RelationComparisonResultFailed) != 0 && (RelationComparisonResult)(entry & RelationComparisonResultOverflow) == 0) {
            } else {
                r.c.reliabilityFlags |= (RelationComparisonResult)((RelationComparisonResult)(entry & ((RelationComparisonResult)(RelationComparisonResultReportsUnmeasurable | RelationComparisonResultReportsUnreliable))));
                if (reportErrors && (RelationComparisonResult)(entry & RelationComparisonResultOverflow) != 0) {
                    r.reportError(diagnostics.Excessive_complexity_comparing_types_0_and_1, r.c.TypeToString(Ꮡsource), r.c.TypeToString(Ꮡtarget));
                }
                if ((RelationComparisonResult)(entry & RelationComparisonResultSucceeded) != 0) {
                    return TernaryTrue;
                }
                return TernaryFalse;
            }
        }
        if (r.relationCount <= 0) {
            r.overflow = true;
            return TernaryFalse;
        }
        if (Ꮡr.ᏑmaybeKeysSet.Has(id)) {
            return TernaryMaybe;
        }
        if (constrained) {
            var (broadestEquivalentId, _) = getRelationKey(Ꮡsource, Ꮡtarget, intersectionState, r.relation == r.c.identityRelation, true);
            if (Ꮡr.ᏑmaybeKeysSet.Has(broadestEquivalentId)) {
                return TernaryMaybe;
            }
        }
        if (len(r.sourceStack) == 100 || len(r.targetStack) == 100) {
            return TernaryMaybe;
        }
        nint maybeStart = len(r.maybeKeys);
        r.maybeKeys = append(r.maybeKeys, id);
        r.maybeKeysSet.Add(id);
        var saveExpandingFlags = r.expandingFlags;
        if ((RecursionFlags)(recursionFlags & RecursionFlagsSource) != 0) {
            r.sourceStack = append(r.sourceStack, Ꮡsource);
            if ((ExpandingFlags)(r.expandingFlags & ExpandingFlagsSource) == 0 && r.c.isDeeplyNestedType(Ꮡsource, r.sourceStack, 3)) {
                r.expandingFlags |= (ExpandingFlags)(ExpandingFlagsSource);
            }
        }
        if ((RecursionFlags)(recursionFlags & RecursionFlagsTarget) != 0) {
            r.targetStack = append(r.targetStack, Ꮡtarget);
            if ((ExpandingFlags)(r.expandingFlags & ExpandingFlagsTarget) == 0 && r.c.isDeeplyNestedType(Ꮡtarget, r.targetStack, 3)) {
                r.expandingFlags |= (ExpandingFlags)(ExpandingFlagsTarget);
            }
        }
        var saveReliabilityFlags = r.c.reliabilityFlags;
        r.c.reliabilityFlags = 0;
        Ternary result = default;
        if (r.expandingFlags == ExpandingFlagsBoth) {
            var tr = r.c.tracer; if (tr != nil) {
                tr.Instant(tracing.PhaseCheckTypes, "recursiveTypeRelatedTo_DepthLimit", new map<@string, any>{["sourceId"] = source.id, ["targetId"] = target.id, ["depth"] = len(r.sourceStack), ["targetDepth"] = len(r.targetStack)});
            }
            result = TernaryMaybe;
        } else {
            {
                var tr = r.c.tracer; if (tr != nil) {
                    var trʗ1 = tr;
                    defer(trʗ1.Push(tracing.PhaseCheckTypes, "structuredTypeRelatedTo", new map<@string, any>{["sourceId"] = Ꮡsource.Value.id, ["targetId"] = Ꮡtarget.Value.id}, false), ref ᒐ);
                }
            }
            result = Ꮡr.structuredTypeRelatedTo(Ꮡsource, Ꮡtarget, reportErrors, intersectionState);
        }
        var propagatingVarianceFlags = r.c.reliabilityFlags;
        r.c.reliabilityFlags |= (RelationComparisonResult)(saveReliabilityFlags);
        if ((RecursionFlags)(recursionFlags & RecursionFlagsSource) != 0) {
            r.sourceStack = r.sourceStack.slice(0, len(r.sourceStack) - 1);
        }
        if ((RecursionFlags)(recursionFlags & RecursionFlagsTarget) != 0) {
            r.targetStack = r.targetStack.slice(0, len(r.targetStack) - 1);
        }
        r.expandingFlags = saveExpandingFlags;
        if (result != TernaryFalse) {
            if (result == TernaryTrue || (len(r.sourceStack) == 0 && len(r.targetStack) == 0)) {
                if (result == TernaryTrue || result == TernaryMaybe) {
                    r.resetMaybeStack(maybeStart, propagatingVarianceFlags, true);
                } else {
                    r.resetMaybeStack(maybeStart, propagatingVarianceFlags, false);
                }
            }
        } else {
            r.relation.set(id, (RelationComparisonResult)(RelationComparisonResultFailed | propagatingVarianceFlags));
            r.relationCount--;
            r.resetMaybeStack(maybeStart, propagatingVarianceFlags, false);
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static void resetMaybeStack(this ref Relater r, nint maybeStart, RelationComparisonResult propagatingVarianceFlags, bool markAllAsSucceeded) {
    for (nint i = maybeStart; i < len(r.maybeKeys); i++) {
        r.maybeKeysSet.Delete(r.maybeKeys[i]);
        if (markAllAsSucceeded) {
            r.relation.set(r.maybeKeys[i], (RelationComparisonResult)(RelationComparisonResultSucceeded | propagatingVarianceFlags));
            r.relationCount--;
        }
    }
    r.maybeKeys = r.maybeKeys.slice(0, maybeStart);
}

internal static errorState getErrorState(this ref Relater r) {
    return new errorState(
        errorChain: r.errorChain,
        relatedInfo: r.relatedInfo
    );
}

internal static void restoreErrorState(this ref Relater r, errorState e) {
    r.errorChain = e.errorChain;
    r.relatedInfo = e.relatedInfo;
}

internal static Ternary structuredTypeRelatedTo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var saveErrorState = r.getErrorState();
    var result = Ꮡr.structuredTypeRelatedToWorker(Ꮡsource, Ꮡtarget, reportErrors, intersectionState);
    if (r.relation != r.c.identityRelation) {
        if (result == TernaryFalse && ((TypeFlags)(source.flags & TypeFlagsIntersection) != 0 || (TypeFlags)(source.flags & TypeFlagsTypeParameter) != 0 && (TypeFlags)(target.flags & TypeFlagsUnion) != 0)) {
            slice<ж<Type>> sourceTypes = default;
            if ((TypeFlags)(source.flags & TypeFlagsIntersection) != 0) {
                sourceTypes = source.Types();
            } else {
                sourceTypes = [Ꮡsource];
            }
            var constraint = r.c.getEffectiveConstraintOfIntersection(sourceTypes, (TypeFlags)(target.flags & TypeFlagsUnion) != 0);
            if (constraint != nil && everyType(constraint, (ж<Type> c) => c != Ꮡsource)) {
                result = Ꮡr.isRelatedToEx(constraint, Ꮡtarget, RecursionFlagsSource, false, nil, intersectionState);
            }
        }
        switch (ᐧ) {
        case {} when result != TernaryFalse && (IntersectionState)(intersectionState & IntersectionStateTarget) == 0 && (TypeFlags)(target.flags & TypeFlagsIntersection) != 0 && !r.c.isGenericObjectType(Ꮡtarget) && (TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection))) != 0: {
            result &= (Ternary)(Ꮡr.propertiesRelatedTo(Ꮡsource, Ꮡtarget, reportErrors, new collections.Set<@string>(), false, IntersectionStateNone));
            if (result != 0 && isObjectLiteralType(ref (Ꮡsource).DerefOrNull()) && (ObjectFlags)(source.objectFlags & ObjectFlagsFreshLiteral) != 0) {
                result &= (Ternary)(Ꮡr.indexSignaturesRelatedTo(Ꮡsource, Ꮡtarget, false, reportErrors, IntersectionStateNone));
            }
            break;
        }
        case {} when result != 0 && r.c.isNonGenericObjectType(Ꮡtarget) && !r.c.isArrayOrTupleType(Ꮡtarget) && r.isSourceIntersectionNeedingExtraCheck(Ꮡsource, Ꮡtarget): {
            result &= (Ternary)(Ꮡr.propertiesRelatedTo(Ꮡsource, Ꮡtarget, reportErrors, new collections.Set<@string>(), true, intersectionState));
            break;
        }}

    }
    if (result != TernaryFalse) {
        r.restoreErrorState(saveErrorState);
    }
    return result;
}

internal static bool isSourceIntersectionNeedingExtraCheck(this ref Relater r, ж<Type> Ꮡsource, ж<Type> target) {
    ref var source = ref Ꮡsource.DerefOrNull();

    return (TypeFlags)(source.flags & TypeFlagsIntersection) != 0 && (TypeFlags)(r.c.getApparentType(Ꮡsource).flags & TypeFlagsStructuredType) != 0 && !core.Some(source.Types(), (ж<Type> t) => t == target || (ObjectFlags)(t.objectFlags & ObjectFlagsNonInferrableType) != 0);
}

internal static Ternary structuredTypeRelatedToWorker(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    Ternary result = default;
    bool varianceCheckFailed = default;
    ref var originalErrorChain = ref heap<ж<ErrorChain>>(out var ᏑoriginalErrorChain);
    ref var saveErrorState = ref heap<errorState>(r.getErrorState(), out var ᏑsaveErrorState);
    var saveErrorStateʗ1 = saveErrorState;
    (Ternary, bool) relateVariances(slice<ж<Type>> sourceTypeArguments, slice<ж<Type>> targetTypeArguments, slice<VarianceFlags> variances, IntersectionState intersectionStateΔ1) {
        result = Ꮡr.typeArgumentsRelatedTo(sourceTypeArguments, targetTypeArguments, variances, reportErrors, intersectionStateΔ1); if (result != TernaryFalse) {
            return (result, true);
        }
        if (core.Some(variances, (VarianceFlags v) => (VarianceFlags)(v & VarianceFlagsAllowsStructuralFallback) != 0)) {
            ᏑoriginalErrorChain.ValueSlot = default;
            Ꮡr.Value.restoreErrorState(saveErrorStateʗ1);
            return (TernaryFalse, false);
        }
        var allowStructuralFallback = Ꮡr.Value.c.hasCovariantVoidArgument(targetTypeArguments, variances);
        varianceCheckFailed = !allowStructuralFallback;
        if (len(variances) != 0 && !allowStructuralFallback) {
            if (varianceCheckFailed && !(reportErrors && core.Some(variances, (VarianceFlags v) => ((VarianceFlags)(v & VarianceFlagsVarianceMask)) == VarianceFlagsInvariant))) {
                return (TernaryFalse, true);
            }
            ᏑoriginalErrorChain.ValueSlot = Ꮡr.Value.errorChain;
            Ꮡr.Value.restoreErrorState(saveErrorStateʗ1);
        }
        return (TernaryFalse, false);
    }
    switch (ᐧ) {
    case {} when r.relation == r.c.identityRelation: {
        switch (ᐧ) {
        case {} when (TypeFlags)(source.flags & TypeFlagsUnionOrIntersection) != 0: {
            var resultΔ4 = Ꮡr.eachTypeRelatedToSomeType(Ꮡsource, Ꮡtarget);
            if (resultΔ4 != TernaryFalse) {
                resultΔ4 &= (Ternary)(Ꮡr.eachTypeRelatedToSomeType(Ꮡtarget, Ꮡsource));
            }
            return resultΔ4;
        }
        case {} when (TypeFlags)(source.flags & TypeFlagsIndex) != 0: {
            return Ꮡr.isRelatedTo(source.Target(), target.Target(), RecursionFlagsBoth, false);
        }
        case {} when (TypeFlags)(source.flags & TypeFlagsIndexedAccess) != 0: {
            result = Ꮡr.isRelatedTo(source.AsIndexedAccessType().objectType, target.AsIndexedAccessType().objectType, RecursionFlagsBoth, false);
            if (result != TernaryFalse) {
                result &= (Ternary)(Ꮡr.isRelatedTo(source.AsIndexedAccessType().indexType, target.AsIndexedAccessType().indexType, RecursionFlagsBoth, false));
                if (result != TernaryFalse) {
                    return result;
                }
            }
            break;
        }
        case {} when (TypeFlags)(source.flags & TypeFlagsConditional) != 0: {
            if (source.AsConditionalType().root.isDistributive == target.AsConditionalType().root.isDistributive) {
                result = Ꮡr.isRelatedTo(source.AsConditionalType().checkType, target.AsConditionalType().checkType, RecursionFlagsBoth, false);
                if (result != TernaryFalse) {
                    result &= (Ternary)(Ꮡr.isRelatedTo(source.AsConditionalType().extendsType, target.AsConditionalType().extendsType, RecursionFlagsBoth, false));
                    if (result != TernaryFalse) {
                        result &= (Ternary)(Ꮡr.isRelatedTo(r.c.getTrueTypeFromConditionalType(Ꮡsource), r.c.getTrueTypeFromConditionalType(Ꮡtarget), RecursionFlagsBoth, false));
                        if (result != TernaryFalse) {
                            result &= (Ternary)(Ꮡr.isRelatedTo(r.c.getFalseTypeFromConditionalType(Ꮡsource), r.c.getFalseTypeFromConditionalType(Ꮡtarget), RecursionFlagsBoth, false));
                            if (result != TernaryFalse) {
                                return result;
                            }
                        }
                    }
                }
            }
            break;
        }
        case {} when (TypeFlags)(source.flags & TypeFlagsSubstitution) != 0: {
            result = Ꮡr.isRelatedTo(source.AsSubstitutionType().baseType, target.AsSubstitutionType().baseType, RecursionFlagsBoth, false);
            if (result != TernaryFalse) {
                result &= (Ternary)(Ꮡr.isRelatedTo(source.AsSubstitutionType().constraint, target.AsSubstitutionType().constraint, RecursionFlagsBoth, false));
                if (result != TernaryFalse) {
                    return result;
                }
            }
            break;
        }
        case {} when (TypeFlags)(source.flags & TypeFlagsTemplateLiteral) != 0: {
            if (slices.Equal<slice<@string>, @string>(source.AsTemplateLiteralType().texts, target.AsTemplateLiteralType().texts)) {
                result = TernaryTrue;
                foreach (var (i, sourceType) in range(source.AsTemplateLiteralType().types)) {
                    var targetType = target.AsTemplateLiteralType().types[i];
                    result &= (Ternary)(Ꮡr.isRelatedTo(sourceType, targetType, RecursionFlagsBoth, false));
                    if (result == TernaryFalse) {
                        return result;
                    }
                }
                return result;
            }
            break;
        }
        case {} when (TypeFlags)(source.flags & TypeFlagsStringMapping) != 0: {
            if (source.AsStringMappingType().of(StringMappingType.ᏑConstrainedType).of(ConstrainedType.ᏑTypeBase).of(TypeBase.ᏑType).Symbol() == target.AsStringMappingType().of(StringMappingType.ᏑConstrainedType).of(ConstrainedType.ᏑTypeBase).of(TypeBase.ᏑType).Symbol()) {
                return Ꮡr.isRelatedTo(source.AsStringMappingType().target, target.AsStringMappingType().target, RecursionFlagsBoth, false);
            }
            break;
        }}

        if ((TypeFlags)(source.flags & TypeFlagsObject) == 0) {
            return TernaryFalse;
        }
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsUnionOrIntersection) != 0 || (TypeFlags)(target.flags & TypeFlagsUnionOrIntersection) != 0: {
        result = Ꮡr.unionOrIntersectionRelatedTo(Ꮡsource, Ꮡtarget, reportErrors, intersectionState);
        if (result != TernaryFalse) {
            return result;
        }
        if (!((TypeFlags)(source.flags & TypeFlagsInstantiable) != 0 || (TypeFlags)(source.flags & TypeFlagsObject) != 0 && (TypeFlags)(target.flags & TypeFlagsUnion) != 0 || (TypeFlags)(source.flags & TypeFlagsIntersection) != 0 && (TypeFlags)(target.flags & ((TypeFlags)((TypeFlags)(TypeFlagsObject | TypeFlagsUnion) | TypeFlagsInstantiable))) != 0)) {
            return TernaryFalse;
        }
        break;
    }}

    if ((TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsConditional))) != 0 && source.alias != nil && len(source.alias.typeArguments) != 0 && target.alias != nil && source.alias.symbol == target.alias.symbol && !(r.c.isMarkerType(Ꮡsource) || r.c.isMarkerType(Ꮡtarget))) {
        var variances = r.c.getAliasVariances(source.alias.symbol);
        if (len(variances) == 0) {
            return TernaryUnknown;
        }
        var @params = r.c.ᏑtypeAliasLinks.Get(source.alias.symbol).typeParameters;
        nint minParams = r.c.getMinTypeArgumentCount(@params);
        var nodeIsInJsFile = ast.IsInJSFile(source.alias.symbol.ValueDeclaration);
        var sourceTypes = r.c.fillMissingTypeArguments(source.alias.typeArguments, @params, minParams, nodeIsInJsFile);
        var targetTypes = r.c.fillMissingTypeArguments(target.alias.typeArguments, @params, minParams, nodeIsInJsFile);
        var (varianceResult, ok) = relateVariances(sourceTypes, targetTypes, variances, intersectionState);
        if (ok) {
            return varianceResult;
        }
    }
    if (isSingleElementGenericTupleType(Ꮡsource) && !source.TargetTupleType().@readonly) {
        result = Ꮡr.isRelatedTo(r.c.getTypeArguments(Ꮡsource)[0], Ꮡtarget, RecursionFlagsSource, false);
        if (result != TernaryFalse) {
            return result;
        }
    }
    if (isSingleElementGenericTupleType(Ꮡtarget) && (target.TargetTupleType().@readonly || r.c.isMutableArrayOrTuple(r.c.getBaseConstraintOrType(Ꮡsource)))) {
        result = Ꮡr.isRelatedTo(Ꮡsource, r.c.getTypeArguments(Ꮡtarget)[0], RecursionFlagsTarget, false);
        if (result != TernaryFalse) {
            return result;
        }
    }
    switch (ᐧ) {
    case {} when (TypeFlags)(target.flags & TypeFlagsTypeParameter) != 0: {
        if ((ObjectFlags)(source.objectFlags & ObjectFlagsMapped) != 0 && source.AsMappedType().declaration.NameType == nil && Ꮡr.isRelatedTo(r.c.getIndexType(Ꮡtarget), r.c.getConstraintTypeFromMappedType(Ꮡsource), RecursionFlagsBoth, false) != TernaryFalse) {
            if ((MappedTypeModifiers)(getMappedTypeModifiers(Ꮡsource) & MappedTypeModifiersIncludeOptional) == 0) {
                var templateType = r.c.getTemplateTypeFromMappedType(Ꮡsource);
                var indexedAccessType = r.c.getIndexedAccessType(Ꮡtarget, r.c.getTypeParameterFromMappedType(Ꮡsource));
                result = Ꮡr.isRelatedTo(templateType, indexedAccessType, RecursionFlagsBoth, reportErrors);
                if (result != TernaryFalse) {
                    return result;
                }
            }
        }
        if (r.relation == r.c.comparableRelation && (TypeFlags)(source.flags & TypeFlagsTypeParameter) != 0) {
            {
                var constraint = r.c.getConstraintOfTypeParameter(Ꮡsource);
                if (constraint != nil && someType(constraint, (ж<Type> c) => (TypeFlags)(c.flags & TypeFlagsTypeParameter) != 0)) {
                    return Ꮡr.isRelatedTo(constraint, Ꮡtarget, RecursionFlagsSource, false);
                }
            }
            return TernaryFalse;
        }
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsIndexedAccess) != 0: {
        if ((TypeFlags)(source.flags & TypeFlagsIndexedAccess) != 0) {
            result = Ꮡr.isRelatedTo(source.AsIndexedAccessType().objectType, target.AsIndexedAccessType().objectType, RecursionFlagsBoth, reportErrors);
            if (result != TernaryFalse) {
                result &= (Ternary)(Ꮡr.isRelatedTo(source.AsIndexedAccessType().indexType, target.AsIndexedAccessType().indexType, RecursionFlagsBoth, reportErrors));
            }
            if (result != TernaryFalse) {
                return result;
            }
            if (reportErrors) {
                originalErrorChain = r.errorChain;
            }
        }
        if (r.relation == r.c.assignableRelation || r.relation == r.c.comparableRelation) {
            var objectType = target.AsIndexedAccessType().objectType;
            var indexType = target.AsIndexedAccessType().indexType;
            var baseObjectType = r.c.getBaseConstraintOrType(objectType);
            var baseIndexType = r.c.getBaseConstraintOrType(indexType);
            if (!r.c.isGenericObjectType(baseObjectType) && !r.c.isGenericIndexType(baseIndexType)) {
                var accessFlags = (AccessFlags)(AccessFlagsWriting | core.IfElse(baseObjectType != objectType, AccessFlagsNoIndexSignatures, (AccessFlags)0));
                var constraint = r.c.getIndexedAccessTypeOrUndefined(baseObjectType, baseIndexType, accessFlags, nil, nil);
                if (constraint != nil) {
                    if (reportErrors && originalErrorChain != nil) {
                        r.restoreErrorState(saveErrorState);
                    }
                    result = Ꮡr.isRelatedToEx(Ꮡsource, constraint, RecursionFlagsTarget, reportErrors, nil, intersectionState);
                    if (result != TernaryFalse) {
                        return result;
                    }
                    if (reportErrors && originalErrorChain != nil && r.errorChain != nil) {
                        if (chainDepth(originalErrorChain) <= chainDepth(r.errorChain)) {
                            r.errorChain = originalErrorChain;
                        }
                    }
                }
            }
        }
        if (reportErrors) {
            originalErrorChain = default;
        }
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsIndex) != 0: {
        var targetType = target.AsIndexType().target;
        if ((TypeFlags)(source.flags & TypeFlagsIndex) != 0) {
            result = Ꮡr.isRelatedTo(targetType, source.AsIndexType().target, RecursionFlagsBoth, false);
            if (result != TernaryFalse) {
                return result;
            }
        }
        if (isTupleType(targetType)) {
            result = Ꮡr.isRelatedTo(Ꮡsource, r.c.getKnownKeysOfTupleType(targetType), RecursionFlagsTarget, reportErrors);
            if (result != TernaryFalse) {
                return result;
            }
        } else {
            var constraint = r.c.getSimplifiedTypeOrConstraint(targetType);
            if (constraint != nil) {
                if (Ꮡr.isRelatedTo(Ꮡsource, r.c.getIndexTypeEx(constraint, (IndexFlags)(target.AsIndexType().indexFlags | IndexFlagsNoReducibleCheck)), RecursionFlagsTarget, reportErrors) == TernaryTrue) {
                    return TernaryTrue;
                }
            } else if (r.c.isGenericMappedType(targetType)) {
                var nameType = r.c.getNameTypeFromMappedType(targetType);
                var constraintType = r.c.getConstraintTypeFromMappedType(targetType);
                ж<Type> targetKeys = default;
                if (nameType != nil && r.c.isMappedTypeWithKeyofConstraintDeclaration(targetType)) {
                    var mappedKeys = r.c.getApparentMappedTypeKeys(nameType, targetType);
                    targetKeys = r.c.getUnionType([mappedKeys, nameType]);
                } else if (nameType != nil) {
                    targetKeys = nameType;
                } else {
                    targetKeys = constraintType;
                }
                if (Ꮡr.isRelatedTo(Ꮡsource, targetKeys, RecursionFlagsTarget, reportErrors) == TernaryTrue) {
                    return TernaryTrue;
                }
            }
        }
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsConditional) != 0: {
        if (r.c.isDeeplyNestedType(Ꮡtarget, r.targetStack, 10)) {
            return TernaryMaybe;
        }
        var c = target.AsConditionalType();
        if (c.root.inferTypeParameters == nil && !r.c.isDistributionDependent(c.root) && !((TypeFlags)(source.flags & TypeFlagsConditional) != 0 && source.AsConditionalType().root == c.root)) {
            var skipTrue = !r.c.isTypeAssignableTo(r.c.getPermissiveInstantiation(c.checkType), r.c.getPermissiveInstantiation(c.extendsType));
            var skipFalse = !skipTrue && r.c.isTypeAssignableTo(r.c.getRestrictiveInstantiation(c.checkType), r.c.getRestrictiveInstantiation(c.extendsType));
            if (skipTrue) {
                result = TernaryTrue;
            } else {
                result = Ꮡr.isRelatedToEx(Ꮡsource, r.c.getTrueTypeFromConditionalType(Ꮡtarget), RecursionFlagsTarget, false, nil, intersectionState);
            }
            if (result != TernaryFalse) {
                if (skipFalse) {
                    result &= (Ternary)(TernaryTrue);
                } else {
                    result &= (Ternary)(Ꮡr.isRelatedToEx(Ꮡsource, r.c.getFalseTypeFromConditionalType(Ꮡtarget), RecursionFlagsTarget, false, nil, intersectionState));
                }
                if (result != TernaryFalse) {
                    return result;
                }
            }
        }
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsTemplateLiteral) != 0: {
        if ((TypeFlags)(source.flags & TypeFlagsTemplateLiteral) != 0) {
            if (r.relation == r.c.comparableRelation) {
                if (r.c.templateLiteralTypesDefinitelyUnrelated(source.AsTemplateLiteralType(), target.AsTemplateLiteralType())) {
                    return TernaryFalse;
                }
                return TernaryTrue;
            }
            r.c.instantiateType(Ꮡsource, r.c.reportUnreliableMapper);
        }
        if (r.c.isTypeMatchedByTemplateLiteralType(Ꮡsource, target.AsTemplateLiteralType(), new Func<ж<Type>, ж<Type>, bool, Ternary>(Ꮡr.isRelatedToWorker))) {
            return TernaryTrue;
        }
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsStringMapping) != 0: {
        if ((TypeFlags)(source.flags & TypeFlagsStringMapping) == 0) {
            if (r.c.isMemberOfStringMapping(Ꮡsource, Ꮡtarget)) {
                return TernaryTrue;
            }
        }
        break;
    }
    case {} when r.c.isGenericMappedType(Ꮡtarget) && r.relation != r.c.identityRelation: {
        var keysRemapped = target.AsMappedType().declaration.NameType != nil;
        var templateType = r.c.getTemplateTypeFromMappedType(Ꮡtarget);
        var modifiers = getMappedTypeModifiers(Ꮡtarget);
        if ((MappedTypeModifiers)(modifiers & MappedTypeModifiersExcludeOptional) == 0) {
            if (!keysRemapped && (TypeFlags)(templateType.flags & TypeFlagsIndexedAccess) != 0 && templateType.AsIndexedAccessType().objectType == Ꮡsource && templateType.AsIndexedAccessType().indexType == r.c.getTypeParameterFromMappedType(Ꮡtarget)) {
                return TernaryTrue;
            }
            if (!r.c.isGenericMappedType(Ꮡsource)) {
                ж<Type> targetKeys = default;
                if (keysRemapped) {
                    targetKeys = r.c.getNameTypeFromMappedType(Ꮡtarget);
                } else {
                    targetKeys = r.c.getConstraintTypeFromMappedType(Ꮡtarget);
                }
                var sourceKeys = r.c.getIndexTypeEx(Ꮡsource, IndexFlagsNoIndexSignatures);
                var includeOptional = (MappedTypeModifiers)(modifiers & MappedTypeModifiersIncludeOptional) != 0;
                ж<Type> filteredByApplicability = default;
                if (includeOptional) {
                    filteredByApplicability = r.c.intersectTypes(targetKeys, sourceKeys);
                }
                if (includeOptional && (TypeFlags)(filteredByApplicability.flags & TypeFlagsNever) == 0 || !includeOptional && Ꮡr.isRelatedTo(targetKeys, sourceKeys, RecursionFlagsBoth, false) != TernaryFalse) {
                    var templateTypeΔ1 = r.c.getTemplateTypeFromMappedType(Ꮡtarget);
                    var typeParameter = r.c.getTypeParameterFromMappedType(Ꮡtarget);
                    var nonNullComponent = r.c.extractTypesOfKind(templateTypeΔ1, ~((TypeFlags)TypeFlagsNullable));
                    if (!keysRemapped && (TypeFlags)(nonNullComponent.flags & TypeFlagsIndexedAccess) != 0 && nonNullComponent.AsIndexedAccessType().indexType == typeParameter) {
                        result = Ꮡr.isRelatedTo(Ꮡsource, nonNullComponent.AsIndexedAccessType().objectType, RecursionFlagsTarget, reportErrors);
                        if (result != TernaryFalse) {
                            return result;
                        }
                    } else {
                        var indexingType = typeParameter;
                        switch (ᐧ) {
                        case {} when keysRemapped: {
                            indexingType = core.OrElse(filteredByApplicability, targetKeys);
                            break;
                        }
                        case {} when filteredByApplicability != nil: {
                            indexingType = r.c.getIntersectionType([filteredByApplicability, typeParameter]);
                            break;
                        }}

                        var indexedAccessType = r.c.getIndexedAccessType(Ꮡsource, indexingType);
                        result = Ꮡr.isRelatedTo(indexedAccessType, templateTypeΔ1, RecursionFlagsBoth, reportErrors);
                        if (result != TernaryFalse) {
                            return result;
                        }
                    }
                }
                originalErrorChain = r.errorChain;
                r.restoreErrorState(saveErrorState);
            }
        }
        break;
    }}

    switch (ᐧ) {
    case {} when (TypeFlags)(source.flags & TypeFlagsTypeVariable) != 0: {
        if ((TypeFlags)(source.flags & TypeFlagsIndexedAccess) == 0 || (TypeFlags)(target.flags & TypeFlagsIndexedAccess) == 0) {
            var constraint = r.c.getConstraintOfType(Ꮡsource);
            if (constraint == nil) {
                constraint = r.c.unknownType;
            }
            result = Ꮡr.isRelatedToEx(constraint, Ꮡtarget, RecursionFlagsSource, false, nil, intersectionState);
            if (result != TernaryFalse) {
                return result;
            }
            var constraintWithThis = r.c.getTypeWithThisArgument(constraint, Ꮡsource, false);
            result = Ꮡr.isRelatedToEx(constraintWithThis, Ꮡtarget, RecursionFlagsSource, reportErrors && constraint != r.c.unknownType && (TypeFlags)((TypeFlags)(target.flags & source.flags) & TypeFlagsTypeParameter) == 0, nil, intersectionState);
            if (result != TernaryFalse) {
                return result;
            }
            if (r.c.isMappedTypeGenericIndexedAccess(Ꮡsource)) {
                var indexConstraint = r.c.getConstraintOfType(source.AsIndexedAccessType().indexType);
                if (indexConstraint != nil) {
                    result = Ꮡr.isRelatedTo(r.c.getIndexedAccessType(source.AsIndexedAccessType().objectType, indexConstraint), Ꮡtarget, RecursionFlagsSource, reportErrors);
                    if (result != TernaryFalse) {
                        return result;
                    }
                }
            }
        }
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsIndex) != 0: {
        var isDeferredMappedIndex = r.c.shouldDeferIndexType(source.AsIndexType().target, source.AsIndexType().indexFlags) && (ObjectFlags)(source.AsIndexType().target.objectFlags & ObjectFlagsMapped) != 0;
        result = Ꮡr.isRelatedTo(r.c.stringNumberSymbolType, Ꮡtarget, RecursionFlagsSource, reportErrors && !isDeferredMappedIndex);
        if (result != TernaryFalse) {
            return result;
        }
        if (isDeferredMappedIndex) {
            var mappedType = source.AsIndexType().target;
            var nameType = r.c.getNameTypeFromMappedType(mappedType);
            ж<Type> sourceMappedKeys = default;
            if (nameType != nil && r.c.isMappedTypeWithKeyofConstraintDeclaration(mappedType)) {
                sourceMappedKeys = r.c.getApparentMappedTypeKeys(nameType, mappedType);
            } else if (nameType != nil) {
                sourceMappedKeys = nameType;
            } else {
                sourceMappedKeys = r.c.getConstraintTypeFromMappedType(mappedType);
            }
            result = Ꮡr.isRelatedTo(sourceMappedKeys, Ꮡtarget, RecursionFlagsSource, reportErrors);
            if (result != TernaryFalse) {
                return result;
            }
        }
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsConditional) != 0: {
        if (r.c.isDeeplyNestedType(Ꮡsource, r.sourceStack, 10)) {
            return TernaryMaybe;
        }
        if ((TypeFlags)(target.flags & TypeFlagsConditional) != 0) {
            var sourceParams = source.AsConditionalType().root.inferTypeParameters;
            var sourceExtends = source.AsConditionalType().extendsType;
            ж<TypeMapper> mapper = default;
            if (len(sourceParams) != 0) {
                var ctx = r.c.newInferenceContext(sourceParams, nil, InferenceFlagsNone, new Func<ж<Type>, ж<Type>, bool, Ternary>(Ꮡr.isRelatedToWorker));
                r.c.inferTypes(ctx.inferences, target.AsConditionalType().extendsType, sourceExtends, (InferencePriority)(InferencePriorityNoConstraints | InferencePriorityAlwaysStrict), false);
                sourceExtends = r.c.instantiateType(sourceExtends, ctx.mapper);
                mapper = ctx.mapper;
            }
            if (r.c.isTypeIdenticalTo(sourceExtends, target.AsConditionalType().extendsType) && (Ꮡr.isRelatedTo(source.AsConditionalType().checkType, target.AsConditionalType().checkType, RecursionFlagsBoth, false) != 0 || Ꮡr.isRelatedTo(target.AsConditionalType().checkType, source.AsConditionalType().checkType, RecursionFlagsBoth, false) != 0)) {
                result = Ꮡr.isRelatedTo(r.c.instantiateType(r.c.getTrueTypeFromConditionalType(Ꮡsource), mapper), r.c.getTrueTypeFromConditionalType(Ꮡtarget), RecursionFlagsBoth, reportErrors);
                if (result != TernaryFalse) {
                    result &= (Ternary)(Ꮡr.isRelatedTo(r.c.getFalseTypeFromConditionalType(Ꮡsource), r.c.getFalseTypeFromConditionalType(Ꮡtarget), RecursionFlagsBoth, reportErrors));
                }
                if (result != TernaryFalse) {
                    return result;
                }
            }
        }
        var defaultConstraint = r.c.getDefaultConstraintOfConditionalType(Ꮡsource);
        if (defaultConstraint != nil) {
            result = Ꮡr.isRelatedTo(defaultConstraint, Ꮡtarget, RecursionFlagsSource, reportErrors);
            if (result != TernaryFalse) {
                return result;
            }
        }
        if ((TypeFlags)(target.flags & TypeFlagsConditional) == 0 && r.c.hasNonCircularBaseConstraint(Ꮡsource)) {
            var distributiveConstraint = r.c.getConstraintOfDistributiveConditionalType(Ꮡsource);
            if (distributiveConstraint != nil) {
                r.restoreErrorState(saveErrorState);
                result = Ꮡr.isRelatedTo(distributiveConstraint, Ꮡtarget, RecursionFlagsSource, reportErrors);
                if (result != TernaryFalse) {
                    return result;
                }
            }
        }
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsTemplateLiteral) != 0 && (TypeFlags)(target.flags & TypeFlagsObject) == 0: {
        if ((TypeFlags)(target.flags & TypeFlagsTemplateLiteral) == 0) {
            var constraint = r.c.getBaseConstraintOfType(Ꮡsource);
            if (constraint != nil && constraint != Ꮡsource) {
                result = Ꮡr.isRelatedTo(constraint, Ꮡtarget, RecursionFlagsSource, reportErrors);
                if (result != TernaryFalse) {
                    return result;
                }
            }
        }
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsStringMapping) != 0: {
        if ((TypeFlags)(target.flags & TypeFlagsStringMapping) != 0) {
            if (source.AsStringMappingType().symbol != target.AsStringMappingType().symbol) {
                return TernaryFalse;
            }
            result = Ꮡr.isRelatedTo(source.AsStringMappingType().target, target.AsStringMappingType().target, RecursionFlagsBoth, reportErrors);
            if (result != TernaryFalse) {
                return result;
            }
        } else {
            var constraint = r.c.getBaseConstraintOfType(Ꮡsource);
            if (constraint != nil) {
                result = Ꮡr.isRelatedTo(constraint, Ꮡtarget, RecursionFlagsSource, reportErrors);
                if (result != TernaryFalse) {
                    return result;
                }
            }
        }
        break;
    }
    default: {
        if (r.relation != r.c.subtypeRelation && r.relation != r.c.strictSubtypeRelation && isPartialMappedType(Ꮡtarget) && r.c.isEmptyObjectType(Ꮡsource)) {
            return TernaryTrue;
        }
        if (r.c.isGenericMappedType(Ꮡtarget)) {
            if (r.c.isGenericMappedType(Ꮡsource)) {
                result = Ꮡr.mappedTypeRelatedTo(Ꮡsource, Ꮡtarget, reportErrors);
                if (result != TernaryFalse) {
                    return result;
                }
            }
            return TernaryFalse;
        }
        var sourceIsPrimitive = (TypeFlags)(source.flags & TypeFlagsPrimitive) != 0;
        if (r.relation != r.c.identityRelation) {
            Ꮡsource = r.c.getApparentType(Ꮡsource); source = ref Ꮡsource.DerefOrNull();
        } else if (r.c.isGenericMappedType(Ꮡsource)) {
            return TernaryFalse;
        }
        switch (ᐧ) {
        case {} when (ObjectFlags)(source.objectFlags & ObjectFlagsReference) != 0 && (ObjectFlags)(target.objectFlags & ObjectFlagsReference) != 0 && source.Target() == target.Target() && !isTupleType(Ꮡsource) && !r.c.isMarkerType(Ꮡsource) && !r.c.isMarkerType(Ꮡtarget): {
            if (r.c.isEmptyArrayLiteralType(Ꮡsource)) {
                return TernaryTrue;
            }
            var variances = r.c.getVariances(source.Target());
            if (len(variances) == 0) {
                return TernaryUnknown;
            }
            var (varianceResult, ok) = relateVariances(r.c.getTypeArguments(Ꮡsource), r.c.getTypeArguments(Ꮡtarget), variances, intersectionState);
            if (ok) {
                return varianceResult;
            }
            break;
        }
        case {} when r.c.isArrayType(Ꮡtarget) && (r.c.isReadonlyArrayType(Ꮡtarget) && everyType(Ꮡsource, r.c.isArrayOrTupleType) || everyType(Ꮡsource, isMutableTupleType)): {
            if (r.relation != r.c.identityRelation) {
                return Ꮡr.isRelatedTo(r.c.getIndexTypeOfTypeEx(Ꮡsource, r.c.numberType, r.c.anyType), r.c.getIndexTypeOfTypeEx(Ꮡtarget, r.c.numberType, r.c.anyType), RecursionFlagsBoth, reportErrors);
            }
            return TernaryFalse;
        }
        case {} when r.c.isGenericTupleType(Ꮡsource) && isTupleType(Ꮡtarget) && !r.c.isGenericTupleType(Ꮡtarget): {
            var constraint = r.c.getBaseConstraintOrType(Ꮡsource);
            if (constraint != Ꮡsource) {
                return Ꮡr.isRelatedTo(constraint, Ꮡtarget, RecursionFlagsSource, reportErrors);
            }
            break;
        }
        case {} when (r.relation == r.c.subtypeRelation || r.relation == r.c.strictSubtypeRelation) && r.c.isEmptyObjectType(Ꮡtarget) && (ObjectFlags)(target.objectFlags & ObjectFlagsFreshLiteral) != 0 && !r.c.isEmptyObjectType(Ꮡsource): {
            return TernaryFalse;
        }}

        if ((TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection))) != 0 && (TypeFlags)(target.flags & TypeFlagsObject) != 0) {
            var reportStructuralErrors = reportErrors && r.errorChain == saveErrorState.errorChain && !sourceIsPrimitive;
            result = Ꮡr.propertiesRelatedTo(Ꮡsource, Ꮡtarget, reportStructuralErrors, new collections.Set<@string>(), false, intersectionState);
            if (result != TernaryFalse) {
                result &= (Ternary)(Ꮡr.signaturesRelatedTo(Ꮡsource, Ꮡtarget, SignatureKindCall, reportStructuralErrors, intersectionState));
                if (result != TernaryFalse) {
                    result &= (Ternary)(Ꮡr.signaturesRelatedTo(Ꮡsource, Ꮡtarget, SignatureKindConstruct, reportStructuralErrors, intersectionState));
                    if (result != TernaryFalse) {
                        result &= (Ternary)(Ꮡr.indexSignaturesRelatedTo(Ꮡsource, Ꮡtarget, sourceIsPrimitive, reportStructuralErrors, intersectionState));
                    }
                }
            }
            if (result != TernaryFalse) {
                if (!varianceCheckFailed) {
                    return result;
                }
                if (originalErrorChain != nil) {
                    r.errorChain = originalErrorChain;
                } else if (r.errorChain == nil) {
                    r.errorChain = saveErrorState.errorChain;
                }
            }
        }
        if ((TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection))) != 0 && (TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
            var objectOnlyTarget = r.c.extractTypesOfKind(Ꮡtarget, (TypeFlags)((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection) | TypeFlagsSubstitution));
            if ((TypeFlags)(objectOnlyTarget.flags & TypeFlagsUnion) != 0) {
                var resultΔ6 = Ꮡr.typeRelatedToDiscriminatedType(Ꮡsource, objectOnlyTarget);
                if (resultΔ6 != TernaryFalse) {
                    return resultΔ6;
                }
            }
        }
        break;
    }}

    return TernaryFalse;
}

internal static Ternary typeArgumentsRelatedTo(this ж<Relater> Ꮡr, slice<ж<Type>> sources, slice<ж<Type>> targets, slice<VarianceFlags> variances, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (len(sources) != len(targets) && r.relation == r.c.identityRelation) {
        return TernaryFalse;
    }
    nint length = min(len(sources), len(targets));
    var result = TernaryTrue;
    foreach (var i in range(length)) {
        var varianceFlags = VarianceFlagsCovariant;
        if (i < len(variances)) {
            varianceFlags = variances[i];
        }
        var variance = (VarianceFlags)(varianceFlags & VarianceFlagsVarianceMask);
        if (variance != VarianceFlagsIndependent) {
            var s = sources[i];
            var t = targets[i];
            Ternary related = default;
            if ((VarianceFlags)(varianceFlags & VarianceFlagsUnmeasurable) != 0) {
                if (r.relation == r.c.identityRelation) {
                    related = Ꮡr.isRelatedTo(s, t, RecursionFlagsBoth, false);
                } else {
                    related = r.c.compareTypesIdentical(s, t);
                }
            } else {
                if (len(r.c.varianceStack) != 0 && (VarianceFlags)(varianceFlags & VarianceFlagsUnreliable) != 0) {
                    r.c.instantiateType(s, r.c.reportUnreliableMapper);
                }
                if (variance == VarianceFlagsCovariant) {
                    related = Ꮡr.isRelatedToEx(s, t, RecursionFlagsBoth, reportErrors, nil, intersectionState);
                } else if (variance == VarianceFlagsContravariant) {
                    related = Ꮡr.isRelatedToEx(t, s, RecursionFlagsBoth, reportErrors, nil, intersectionState);
                } else if (variance == VarianceFlagsBivariant) {
                    related = Ꮡr.isRelatedTo(t, s, RecursionFlagsBoth, false);
                    if (related == TernaryFalse) {
                        related = Ꮡr.isRelatedToEx(s, t, RecursionFlagsBoth, reportErrors, nil, intersectionState);
                    }
                } else {
                    related = Ꮡr.isRelatedToEx(s, t, RecursionFlagsBoth, reportErrors, nil, intersectionState);
                    if (related != TernaryFalse) {
                        related &= (Ternary)(Ꮡr.isRelatedToEx(t, s, RecursionFlagsBoth, reportErrors, nil, intersectionState));
                    }
                }
            }
            if (related == TernaryFalse) {
                return TernaryFalse;
            }
            result &= (Ternary)(related);
        }
    }
    return result;
}

internal static Ternary mappedTypeRelatedTo(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target, bool reportErrors) {
    ref var r = ref Ꮡr.DerefOrNull();

    var modifiersRelated = r.relation == r.c.comparableRelation || r.relation == r.c.identityRelation && getMappedTypeModifiers(source) == getMappedTypeModifiers(target) || r.relation != r.c.identityRelation && r.c.getCombinedMappedTypeOptionality(source) <= r.c.getCombinedMappedTypeOptionality(target);
    if (modifiersRelated) {
        var targetConstraint = r.c.getConstraintTypeFromMappedType(target);
        var sourceConstraint = r.c.instantiateType(r.c.getConstraintTypeFromMappedType(source), core.IfElse(r.c.getCombinedMappedTypeOptionality(source) < 0, r.c.reportUnmeasurableMapper, r.c.reportUnreliableMapper));
        var result = Ꮡr.isRelatedTo(targetConstraint, sourceConstraint, RecursionFlagsBoth, reportErrors); if (result != TernaryFalse) {
            var mapper = newSimpleTypeMapper(r.c.getTypeParameterFromMappedType(source), r.c.getTypeParameterFromMappedType(target));
            if (r.c.instantiateType(r.c.getNameTypeFromMappedType(source), mapper) == r.c.instantiateType(r.c.getNameTypeFromMappedType(target), mapper)) {
                return (Ternary)(result & Ꮡr.isRelatedTo(r.c.instantiateType(r.c.getTemplateTypeFromMappedType(source), mapper), r.c.getTemplateTypeFromMappedType(target), RecursionFlagsBoth, reportErrors));
            }
        }
    }
    return TernaryFalse;
}

internal static Ternary typeRelatedToDiscriminatedType(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var sourceProperties = r.c.getPropertiesOfType(Ꮡsource);
    var sourcePropertiesFiltered = r.c.findDiscriminantProperties(sourceProperties, Ꮡtarget);
    if (len(sourcePropertiesFiltered) == 0) {
        return TernaryFalse;
    }
    nint numCombinations = 1;
    foreach (var (_, sourceProperty) in range(sourcePropertiesFiltered)) {
        numCombinations *= countTypes(r.c.getNonMissingTypeOfSymbol(sourceProperty));
        if (numCombinations > 25) {
            var tr = r.c.tracer; if (tr != nil) {
                tr.Instant(tracing.PhaseCheckTypes, "typeRelatedToDiscriminatedType_DepthLimit", new map<@string, any>{["sourceId"] = source.id, ["targetId"] = target.id, ["numCombinations"] = numCombinations});
            }
            return TernaryFalse;
        }
        if (numCombinations == 0) {
            return TernaryFalse;
        }
    }
    var sourceDiscriminantTypes = new slice<slice<ж<Type>>>(len(sourcePropertiesFiltered));
    collections.Set<@string> excludedProperties = default;
    foreach (var (i, sourceProperty) in range(sourcePropertiesFiltered)) {
        var sourcePropertyType = r.c.getNonMissingTypeOfSymbol(sourceProperty);
        sourceDiscriminantTypes[i] = sourcePropertyType.Distributed();
        excludedProperties.Add(sourceProperty.Name);
    }
    var discriminantCombinations = new slice<slice<ж<Type>>>(numCombinations);
    foreach (var i in range(numCombinations)) {
        var combination = new slice<ж<Type>>(len(sourceDiscriminantTypes));
        nint n = i;
        for (nint j = len(sourceDiscriminantTypes) - 1; j >= 0; j--) {
            var sourceTypes = sourceDiscriminantTypes[j];
            nint length = len(sourceTypes);
            combination[j] = sourceTypes[rem(n, length)];
            n = quo(n, length);
        }
        discriminantCombinations[i] = combination;
    }
    slice<ж<Type>> matchingTypes = default;
    foreach (var (_, combination) in range(discriminantCombinations)) {
        var hasMatch = false;
outer:
        foreach (var (_, t) in range(target.Types())) {
            foreach (var i in range(len(sourcePropertiesFiltered))) {
                var sourceProperty = sourcePropertiesFiltered[i];
                var targetProperty = r.c.getPropertyOfType(t, sourceProperty.Name);
                if (targetProperty == nil) {
                    goto continue_outer;
                }
                if (sourceProperty == targetProperty) {
                    continue;
                }
                var combinationʗ1 = combination;
                var related = Ꮡr.propertyRelatedTo(Ꮡsource, Ꮡtarget, sourceProperty, targetProperty, (ж<ast.Symbol> _) => combinationʗ1[i],
                    false, IntersectionStateNone, r.c.strictNullChecks || r.relation == r.c.comparableRelation);
                if (related == TernaryFalse) {
                    goto continue_outer;
                }
            }
            matchingTypes = core.AppendIfUnique(matchingTypes, t);
            hasMatch = true;
continue_outer:;
        }
break_outer:;
        if (!hasMatch) {
            return TernaryFalse;
        }
    }
    var result = TernaryTrue;
    foreach (var (_, t) in range(matchingTypes)) {
        result &= (Ternary)(Ꮡr.propertiesRelatedTo(Ꮡsource, t, false, excludedProperties, false, IntersectionStateNone));
        if (result != TernaryFalse) {
            result &= (Ternary)(Ꮡr.signaturesRelatedTo(Ꮡsource, t, SignatureKindCall, false, IntersectionStateNone));
            if (result != TernaryFalse) {
                result &= (Ternary)(Ꮡr.signaturesRelatedTo(Ꮡsource, t, SignatureKindConstruct, false, IntersectionStateNone));
                if (result != TernaryFalse && !(isTupleType(Ꮡsource) && isTupleType(t))) {
                    result &= (Ternary)(Ꮡr.indexSignaturesRelatedTo(Ꮡsource, t, false, false, IntersectionStateNone));
                }
            }
        }
        if (result == TernaryFalse) {
            return result;
        }
    }
    return result;
}

internal static Ternary propertiesRelatedTo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, bool reportErrors, collections.Set<@string> excludedPropertiesʗp, bool optionalsOnly, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var excludedProperties = ref heap(excludedPropertiesʗp, out var ᏑexcludedProperties);

    if (r.relation == r.c.identityRelation) {
        return Ꮡr.propertiesIdenticalTo(Ꮡsource, Ꮡtarget, excludedProperties);
    }
    var result = TernaryTrue;
    if (isTupleType(Ꮡtarget)) {
        if (r.c.isArrayOrTupleType(Ꮡsource)) {
            if (!target.TargetTupleType().@readonly && (r.c.isReadonlyArrayType(Ꮡsource) || isTupleType(Ꮡsource) && source.TargetTupleType().@readonly)) {
                return TernaryFalse;
            }
            nint sourceArity = r.c.getTypeReferenceArity(Ꮡsource);
            nint targetArity = r.c.getTypeReferenceArity(Ꮡtarget);
            bool sourceRest = default;
            if (isTupleType(Ꮡsource)) {
                sourceRest = (ElementFlags)(source.TargetTupleType().combinedFlags & ElementFlagsRest) != 0;
            } else {
                sourceRest = true;
            }
            var targetHasRestElement = (ElementFlags)(target.TargetTupleType().combinedFlags & ElementFlagsRest) != 0;
            var targetHasVariableElement = (ElementFlags)(target.TargetTupleType().combinedFlags & ElementFlagsVariable) != 0;
            nint sourceMinLength = default;
            if (isTupleType(Ꮡsource)) {
                sourceMinLength = source.TargetTupleType().minLength;
            } else {
                sourceMinLength = 0;
            }
            nint targetMinLength = target.TargetTupleType().minLength;
            if (!sourceRest && sourceArity < targetMinLength) {
                if (reportErrors) {
                    r.reportError(diagnostics.Source_has_0_element_s_but_target_requires_1, sourceArity, targetMinLength);
                }
                return TernaryFalse;
            }
            if (!targetHasVariableElement && targetArity < sourceMinLength) {
                if (reportErrors) {
                    r.reportError(diagnostics.Source_has_0_element_s_but_target_allows_only_1, sourceMinLength, targetArity);
                }
                return TernaryFalse;
            }
            if (!targetHasVariableElement && (sourceRest || targetArity < sourceArity)) {
                if (reportErrors) {
                    if (sourceMinLength < targetMinLength) {
                        r.reportError(diagnostics.Target_requires_0_element_s_but_source_may_have_fewer, targetMinLength);
                    } else {
                        r.reportError(diagnostics.Target_allows_only_0_element_s_but_source_may_have_more, targetArity);
                    }
                }
                return TernaryFalse;
            }
            var sourceTypeArguments = r.c.getTypeArguments(Ꮡsource);
            var targetTypeArguments = r.c.getTypeArguments(Ꮡtarget);
            nint targetStartCount = getStartElementCount(ref (target.TargetTupleType()).DerefOrNull(), ElementFlagsNonRest);
            nint targetEndCount = getEndElementCount(ref (target.TargetTupleType()).DerefOrNull(), ElementFlagsNonRest);
            var canExcludeDiscriminants = ᏑexcludedProperties.Len() != 0;
            foreach (var sourcePosition in range(sourceArity)) {
                ElementFlags sourceFlags = default;
                if (isTupleType(Ꮡsource)) {
                    sourceFlags = source.TargetTupleType().elementInfos[sourcePosition].flags;
                } else {
                    sourceFlags = ElementFlagsRest;
                }
                nint sourcePositionFromEnd = sourceArity - 1 - sourcePosition;
                nint targetPosition = default;
                if (targetHasRestElement && sourcePosition >= targetStartCount) {
                    targetPosition = targetArity - 1 - min(sourcePositionFromEnd, targetEndCount);
                } else {
                    if (sourcePosition >= targetArity) {
                        if (reportErrors) {
                            r.reportError(diagnostics.Target_allows_only_0_element_s_but_source_may_have_more, targetArity);
                        }
                        return TernaryFalse;
                    }
                    targetPosition = sourcePosition;
                }
                var targetFlags = ElementFlagsNone;
                if (targetPosition >= 0) {
                    targetFlags = target.TargetTupleType().elementInfos[targetPosition].flags;
                }
                if ((ElementFlags)(targetFlags & ElementFlagsVariadic) != 0 && (ElementFlags)(sourceFlags & ElementFlagsVariadic) == 0) {
                    if (reportErrors) {
                        r.reportError(diagnostics.Source_provides_no_match_for_variadic_element_at_position_0_in_target, targetPosition);
                    }
                    return TernaryFalse;
                }
                if ((ElementFlags)(sourceFlags & ElementFlagsVariadic) != 0 && (ElementFlags)(targetFlags & ElementFlagsVariable) == 0) {
                    if (reportErrors) {
                        r.reportError(diagnostics.Variadic_element_at_position_0_in_source_does_not_match_element_at_position_1_in_target, sourcePosition, targetPosition);
                    }
                    return TernaryFalse;
                }
                if ((ElementFlags)(targetFlags & ElementFlagsRequired) != 0 && (ElementFlags)(sourceFlags & ElementFlagsRequired) == 0) {
                    if (reportErrors) {
                        r.reportError(diagnostics.Source_provides_no_match_for_required_element_at_position_0_in_target, targetPosition);
                    }
                    return TernaryFalse;
                }
                if (canExcludeDiscriminants) {
                    if ((ElementFlags)(sourceFlags & ElementFlagsVariable) != 0 || (ElementFlags)(targetFlags & ElementFlagsVariable) != 0) {
                        canExcludeDiscriminants = false;
                    }
                    if (canExcludeDiscriminants && ᏑexcludedProperties.Has(strconv.Itoa(sourcePosition))) {
                        continue;
                    }
                }
                var sourceType = r.c.removeMissingType(sourceTypeArguments[sourcePosition], (ElementFlags)((ElementFlags)(sourceFlags & targetFlags) & ElementFlagsOptional) != 0);
                var targetType = targetTypeArguments[targetPosition];
                ж<Type> targetCheckType = default;
                if ((ElementFlags)(sourceFlags & ElementFlagsVariadic) != 0 && (ElementFlags)(targetFlags & ElementFlagsRest) != 0) {
                    targetCheckType = r.c.createArrayType(targetType);
                } else {
                    targetCheckType = r.c.removeMissingType(targetType, (ElementFlags)(targetFlags & ElementFlagsOptional) != 0);
                }
                var related = Ꮡr.isRelatedToEx(sourceType, targetCheckType, RecursionFlagsBoth, reportErrors, nil, intersectionState);
                if (related == TernaryFalse) {
                    if (reportErrors && (targetArity > 1 || sourceArity > 1)) {
                        if (targetHasRestElement && sourcePosition >= targetStartCount && sourcePositionFromEnd >= targetEndCount && targetStartCount != sourceArity - targetEndCount - 1) {
                            r.reportError(diagnostics.Type_at_positions_0_through_1_in_source_is_not_compatible_with_type_at_position_2_in_target, targetStartCount, sourceArity - targetEndCount - 1, targetPosition);
                        } else {
                            r.reportError(diagnostics.Type_at_position_0_in_source_is_not_compatible_with_type_at_position_1_in_target, sourcePosition, targetPosition);
                        }
                    }
                    return TernaryFalse;
                }
                result &= (Ternary)(related);
            }
            return result;
        }
        if ((ElementFlags)(target.TargetTupleType().combinedFlags & ElementFlagsVariable) != 0) {
            return TernaryFalse;
        }
    }
    var requireOptionalProperties = (r.relation == r.c.subtypeRelation || r.relation == r.c.strictSubtypeRelation) && !isObjectLiteralType(ref (Ꮡsource).DerefOrNull()) && !r.c.isEmptyArrayLiteralType(Ꮡsource) && !isTupleType(Ꮡsource);
    var unmatchedProperty = r.c.getUnmatchedProperty(Ꮡsource, Ꮡtarget, requireOptionalProperties, false);
    if (unmatchedProperty != nil) {
        if (reportErrors && r.c.shouldReportUnmatchedPropertyError(Ꮡsource, Ꮡtarget)) {
            r.reportUnmatchedProperty(Ꮡsource, Ꮡtarget, unmatchedProperty, requireOptionalProperties);
        }
        return TernaryFalse;
    }
    if (isObjectLiteralType(ref (Ꮡtarget).DerefOrNull())) {
        foreach (var (_, sourceProp) in range(excludeProperties(r.c.getPropertiesOfType(Ꮡsource), excludedProperties))) {
            if (r.c.getPropertyOfObjectType(Ꮡtarget, sourceProp.Name) == nil) {
                if (reportErrors) {
                    r.reportError(diagnostics.Property_0_does_not_exist_on_type_1, r.c.symbolToString(sourceProp), r.c.TypeToString(Ꮡtarget));
                }
                return TernaryFalse;
            }
        }
    }
    var properties = r.c.getPropertiesOfType(Ꮡtarget);
    var numericNamesOnly = isTupleType(Ꮡsource) && isTupleType(Ꮡtarget);
    foreach (var (_, targetProp) in range(excludeProperties(properties, excludedProperties))) {
        @string name = targetProp.Name;
        if ((ast.SymbolFlags)(targetProp.Flags & ast.SymbolFlagsPrototype) == 0 && (!numericNamesOnly || isNumericLiteralName(name) || name == "length"u8) && (!optionalsOnly || (ast.SymbolFlags)(targetProp.Flags & ast.SymbolFlagsOptional) != 0)) {
            var sourceProp = r.c.getPropertyOfType(Ꮡsource, name);
            if (sourceProp != nil && sourceProp != targetProp) {
                var related = Ꮡr.propertyRelatedTo(Ꮡsource, Ꮡtarget, sourceProp, targetProp, r.c.getNonMissingTypeOfSymbol, reportErrors, intersectionState, r.relation == r.c.comparableRelation);
                if (related == TernaryFalse) {
                    return TernaryFalse;
                }
                result &= (Ternary)(related);
            }
        }
    }
    return result;
}

internal static Ternary propertyRelatedTo(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target, ж<ast.Symbol> ᏑsourceProp, ж<ast.Symbol> ᏑtargetProp, Func<ж<ast.Symbol>, ж<Type>> getTypeOfSourceProperty, bool reportErrors, IntersectionState intersectionState, bool skipOptional) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var sourceProp = ref ᏑsourceProp.DerefOrNull();
    ref var targetProp = ref ᏑtargetProp.DerefOrNull();

    var sourcePropFlags = getDeclarationModifierFlagsFromSymbol(ref (ᏑsourceProp).DerefOrNull());
    var targetPropFlags = getDeclarationModifierFlagsFromSymbol(ref (ᏑtargetProp).DerefOrNull());
    switch (ᐧ) {
    case {} when (ast.ModifierFlags)(sourcePropFlags & ast.ModifierFlagsPrivate) != 0 || (ast.ModifierFlags)(targetPropFlags & ast.ModifierFlagsPrivate) != 0: {
        if (sourceProp.ValueDeclaration != targetProp.ValueDeclaration) {
            if (reportErrors) {
                if ((ast.ModifierFlags)(sourcePropFlags & ast.ModifierFlagsPrivate) != 0 && (ast.ModifierFlags)(targetPropFlags & ast.ModifierFlagsPrivate) != 0) {
                    r.reportError(diagnostics.Types_have_separate_declarations_of_a_private_property_0, r.c.symbolToString(ᏑtargetProp));
                } else {
                    r.reportError(diagnostics.Property_0_is_private_in_type_1_but_not_in_type_2, r.c.symbolToString(ᏑtargetProp), r.c.TypeToString(core.IfElse((ast.ModifierFlags)(sourcePropFlags & ast.ModifierFlagsPrivate) != 0, source, target)), r.c.TypeToString(core.IfElse((ast.ModifierFlags)(sourcePropFlags & ast.ModifierFlagsPrivate) != 0, target, source)));
                }
            }
            return TernaryFalse;
        }
        break;
    }
    case {} when (ast.ModifierFlags)(targetPropFlags & ast.ModifierFlagsProtected) != 0: {
        if (!r.c.isValidOverrideOf(ᏑsourceProp, ᏑtargetProp)) {
            if (reportErrors) {
                var sourceType = core.OrElse(r.c.getDeclaringClass(ᏑsourceProp), source);
                var targetType = core.OrElse(r.c.getDeclaringClass(ᏑtargetProp), target);
                r.reportError(diagnostics.Property_0_is_protected_but_type_1_is_not_a_class_derived_from_2, r.c.symbolToString(ᏑtargetProp), r.c.TypeToString(sourceType), r.c.TypeToString(targetType));
            }
            return TernaryFalse;
        }
        break;
    }
    case {} when (ast.ModifierFlags)(sourcePropFlags & ast.ModifierFlagsProtected) != 0: {
        if (reportErrors) {
            r.reportError(diagnostics.Property_0_is_protected_in_type_1_but_public_in_type_2, r.c.symbolToString(ᏑtargetProp), r.c.TypeToString(source), r.c.TypeToString(target));
        }
        return TernaryFalse;
    }}

    if (r.relation == r.c.strictSubtypeRelation && r.c.isReadonlySymbol(ᏑsourceProp) && !r.c.isReadonlySymbol(ᏑtargetProp)) {
        return TernaryFalse;
    }
    var related = Ꮡr.isPropertySymbolTypeRelated(ᏑsourceProp, ᏑtargetProp, getTypeOfSourceProperty, reportErrors, intersectionState);
    if (related == TernaryFalse) {
        if (reportErrors) {
            r.reportError(diagnostics.Types_of_property_0_are_incompatible, r.c.symbolToString(ᏑtargetProp));
        }
        return TernaryFalse;
    }
    if (!skipOptional && (ast.SymbolFlags)(sourceProp.Flags & ast.SymbolFlagsOptional) != 0 && (ast.SymbolFlags)(targetProp.Flags & ast.SymbolFlagsClassMember) != 0 && (ast.SymbolFlags)(targetProp.Flags & ast.SymbolFlagsOptional) == 0) {
        if (reportErrors) {
            r.reportError(diagnostics.Property_0_is_optional_in_type_1_but_required_in_type_2, r.c.symbolToString(ᏑtargetProp), r.c.TypeToString(source), r.c.TypeToString(target));
        }
        return TernaryFalse;
    }
    return related;
}

internal static Ternary isPropertySymbolTypeRelated(this ж<Relater> Ꮡr, ж<ast.Symbol> sourceProp, ж<ast.Symbol> ᏑtargetProp, Func<ж<ast.Symbol>, ж<Type>> getTypeOfSourceProperty, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var targetProp = ref ᏑtargetProp.DerefOrNull();

    var targetIsOptional = r.c.strictNullChecks && (ast.CheckFlags)(targetProp.CheckFlags & ast.CheckFlagsPartial) != 0;
    var effectiveTarget = r.c.addOptionalityEx(r.c.getNonMissingTypeOfSymbol(ᏑtargetProp), false, targetIsOptional);
    if ((TypeFlags)(effectiveTarget.flags & core.IfElse(r.relation == r.c.strictSubtypeRelation, TypeFlagsAny, TypeFlagsAnyOrUnknown)) != 0) {
        return TernaryTrue;
    }
    var effectiveSource = getTypeOfSourceProperty(sourceProp);
    return Ꮡr.isRelatedToEx(effectiveSource, effectiveTarget, RecursionFlagsBoth, reportErrors, nil, intersectionState);
}

internal static void reportUnmatchedProperty(this ref Relater r, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<ast.Symbol> ᏑunmatchedProperty, bool requireOptionalProperties) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var unmatchedProperty = ref ᏑunmatchedProperty.DerefOrNull();

    if (unmatchedProperty.ValueDeclaration != nil && unmatchedProperty.ValueDeclaration.Name() != nil && ast.IsPrivateIdentifier(unmatchedProperty.ValueDeclaration.Name()) && source.symbol != nil && (ast.SymbolFlags)(source.symbol.Flags & ast.SymbolFlagsClass) != 0) {
        @string privateIdentifierDescription = unmatchedProperty.ValueDeclaration.Name().Text();
        @string symbolTableKey = binder.GetSymbolNameForPrivateIdentifier(source.symbol, privateIdentifierDescription);
        if (r.c.getPropertyOfType(Ꮡsource, symbolTableKey) != nil) {
            r.reportError(diagnostics.Property_0_in_type_1_refers_to_a_different_member_that_cannot_be_accessed_from_within_type_2, privateIdentifierDescription, r.c.SymbolToString(source.symbol), r.c.SymbolToString(target.symbol));
            return;
        }
    }
    var props = r.c.getUnmatchedProperties(Ꮡsource, Ꮡtarget, requireOptionalProperties, false);
    if (len(props) == 1) {
        var (sourceType, targetType) = r.c.getTypeNamesForErrorDisplay(Ꮡsource, Ꮡtarget);
        @string propName = r.c.symbolToString(ᏑunmatchedProperty);
        r.reportError(diagnostics.Property_0_is_missing_in_type_1_but_required_in_type_2, propName, sourceType, targetType);
        if (len(unmatchedProperty.Declarations) != 0) {
            r.relatedInfo = append(r.relatedInfo, createDiagnosticForNode(unmatchedProperty.Declarations[0], diagnostics.X_0_is_declared_here, propName));
        }
    } else if (r.tryElaborateArrayLikeErrors(Ꮡsource, Ꮡtarget, false)) {
        var (sourceType, targetType) = r.c.getTypeNamesForErrorDisplay(Ꮡsource, Ꮡtarget);
        if (len(props) > 5) {
            @string propNames = strings.Join(core.Map<ж<ast.Symbol>, @string>(props[..4], r.c.symbolToString), ", ");
            r.reportError(diagnostics.Type_0_is_missing_the_following_properties_from_type_1_Colon_2_and_3_more, sourceType, targetType, propNames, len(props) - 4);
        } else {
            @string propNames = strings.Join(core.Map<ж<ast.Symbol>, @string>(props, r.c.symbolToString), ", ");
            r.reportError(diagnostics.Type_0_is_missing_the_following_properties_from_type_1_Colon_2, sourceType, targetType, propNames);
        }
    }
}

internal static bool tryElaborateArrayLikeErrors(this ref Relater r, ж<Type> Ꮡsource, ж<Type> target, bool reportErrors) {
    ref var source = ref Ꮡsource.DerefOrNull();

    if (isTupleType(Ꮡsource)) {
        if (source.TargetTupleType().@readonly && r.c.isMutableArrayOrTuple(target)) {
            if (reportErrors) {
                r.reportError(diagnostics.The_type_0_is_readonly_and_cannot_be_assigned_to_the_mutable_type_1, r.c.TypeToString(Ꮡsource), r.c.TypeToString(target));
            }
            return false;
        }
        return r.c.isArrayOrTupleType(target);
    }
    if (r.c.isReadonlyArrayType(Ꮡsource) && r.c.isMutableArrayOrTuple(target)) {
        if (reportErrors) {
            r.reportError(diagnostics.The_type_0_is_readonly_and_cannot_be_assigned_to_the_mutable_type_1, r.c.TypeToString(Ꮡsource), r.c.TypeToString(target));
        }
        return false;
    }
    if (isTupleType(target)) {
        return r.c.isArrayType(Ꮡsource);
    }
    return true;
}

internal static void tryElaborateErrorsForPrimitivesAndObjects(this ref Relater r, ж<Type> source, ж<Type> target) {
    if ((source == r.c.globalStringType && target == r.c.stringType) || (source == r.c.globalNumberType && target == r.c.numberType) || (source == r.c.globalBooleanType && target == r.c.booleanType) || (source == r.c.getGlobalESSymbolType() && target == r.c.esSymbolType)) {
        r.reportError(diagnostics.X_0_is_a_primitive_but_1_is_a_wrapper_object_Prefer_using_0_when_possible, r.c.TypeToString(target), r.c.TypeToString(source));
    }
}

internal static Ternary propertiesIdenticalTo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, collections.Set<@string> excludedProperties) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsObject) == 0 || (TypeFlags)(target.flags & TypeFlagsObject) == 0) {
        return TernaryFalse;
    }
    var sourceProperties = excludeProperties(r.c.getPropertiesOfObjectType(Ꮡsource), excludedProperties);
    var targetProperties = excludeProperties(r.c.getPropertiesOfObjectType(Ꮡtarget), excludedProperties);
    if (len(sourceProperties) != len(targetProperties)) {
        return TernaryFalse;
    }
    var result = TernaryTrue;
    foreach (var (_, sourceProp) in range(sourceProperties)) {
        var targetProp = r.c.getPropertyOfObjectType(Ꮡtarget, sourceProp.Name);
        if (targetProp == nil) {
            return TernaryFalse;
        }
        var related = r.c.compareProperties(sourceProp, targetProp, Ꮡr.isRelatedToSimple);
        if (related == TernaryFalse) {
            return TernaryFalse;
        }
        result &= (Ternary)(related);
    }
    return result;
}

internal static Ternary signaturesRelatedTo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, SignatureKind kind, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if (r.relation == r.c.identityRelation) {
        return Ꮡr.signaturesIdenticalTo(Ꮡsource, Ꮡtarget, kind);
    }
    if (Ꮡsource == r.c.anyFunctionType) {
        return TernaryTrue;
    }
    if (Ꮡtarget == r.c.anyFunctionType) {
        return TernaryFalse;
    }
    var sourceSignatures = r.c.getSignaturesOfType(Ꮡsource, kind);
    var targetSignatures = r.c.getSignaturesOfType(Ꮡtarget, kind);
    if (kind == SignatureKindConstruct && len(sourceSignatures) != 0 && len(targetSignatures) != 0) {
        var sourceIsAbstract = (SignatureFlags)(sourceSignatures[0].flags & SignatureFlagsAbstract) != 0;
        var targetIsAbstract = (SignatureFlags)(targetSignatures[0].flags & SignatureFlagsAbstract) != 0;
        if (sourceIsAbstract && !targetIsAbstract) {
            if (reportErrors) {
                r.reportError(diagnostics.Cannot_assign_an_abstract_constructor_type_to_a_non_abstract_constructor_type);
            }
            return TernaryFalse;
        }
        if (!r.constructorVisibilitiesAreCompatible(sourceSignatures[0], targetSignatures[0], reportErrors)) {
            return TernaryFalse;
        }
    }
    var result = TernaryTrue;
    switch (ᐧ) {
    case {} when (ObjectFlags)(source.objectFlags & ObjectFlagsInstantiated) != 0 && (ObjectFlags)(target.objectFlags & ObjectFlagsInstantiated) != 0 && source.symbol == target.symbol || (ObjectFlags)(source.objectFlags & ObjectFlagsReference) != 0 && (ObjectFlags)(target.objectFlags & ObjectFlagsReference) != 0 && source.Target() == target.Target(): {
        foreach (var i in range(len(targetSignatures))) {
            var related = Ꮡr.signatureRelatedTo(sourceSignatures[i], targetSignatures[i], true, reportErrors, intersectionState);
            if (related == TernaryFalse) {
                return TernaryFalse;
            }
            result &= (Ternary)(related);
        }
        break;
    }
    case {} when len(sourceSignatures) == 1 && len(targetSignatures) == 1: {
        var eraseGenerics = r.relation == r.c.comparableRelation;
        result = Ꮡr.signatureRelatedTo(sourceSignatures[0], targetSignatures[0], eraseGenerics, reportErrors, intersectionState);
        break;
    }
    default: {
outer:
        foreach (var (_, t) in range(targetSignatures)) {
            var saveErrorState = r.getErrorState();
            var shouldElaborateErrors = reportErrors;
            foreach (var (_, s) in range(sourceSignatures)) {
                var related = Ꮡr.signatureRelatedTo(s, t, true, shouldElaborateErrors, intersectionState);
                if (related != TernaryFalse) {
                    result &= (Ternary)(related);
                    r.restoreErrorState(saveErrorState);
                    goto continue_outer;
                }
                shouldElaborateErrors = false;
            }
            if (shouldElaborateErrors) {
                r.reportError(diagnostics.Type_0_provides_no_match_for_the_signature_1, r.c.TypeToString(Ꮡsource), r.c.signatureToString(t));
            }
            return TernaryFalse;
continue_outer:;
        }
break_outer:;
        break;
    }}

    return result;
}

internal static bool constructorVisibilitiesAreCompatible(this ref Relater r, ж<Signature> ᏑsourceSignature, ж<Signature> ᏑtargetSignature, bool reportErrors) {
    ref var sourceSignature = ref ᏑsourceSignature.DerefOrNull();
    ref var targetSignature = ref ᏑtargetSignature.DerefOrNull();

    if (sourceSignature.declaration == nil || targetSignature.declaration == nil) {
        return true;
    }
    var sourceAccessibility = (ast.ModifierFlags)(sourceSignature.declaration.ModifierFlags() & ast.ModifierFlagsNonPublicAccessibilityModifier);
    var targetAccessibility = (ast.ModifierFlags)(targetSignature.declaration.ModifierFlags() & ast.ModifierFlagsNonPublicAccessibilityModifier);
    if (targetAccessibility == ast.ModifierFlagsPrivate) {
        return true;
    }
    if (targetAccessibility == ast.ModifierFlagsProtected && sourceAccessibility != ast.ModifierFlagsPrivate) {
        return true;
    }
    if (targetAccessibility != ast.ModifierFlagsProtected && sourceAccessibility == 0) {
        return true;
    }
    if (reportErrors) {
        r.reportError(diagnostics.Cannot_assign_a_0_constructor_type_to_a_1_constructor_type, visibilityToString(sourceAccessibility), visibilityToString(targetAccessibility));
    }
    return false;
}

internal static Ternary signatureRelatedTo(this ж<Relater> Ꮡr, ж<Signature> Ꮡsource, ж<Signature> Ꮡtarget, bool erase, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var checkMode = SignatureCheckModeNone;
    switch (ᐧ) {
    case {} when r.relation == r.c.subtypeRelation: {
        checkMode = SignatureCheckModeStrictTopSignature;
        break;
    }
    case {} when r.relation == r.c.strictSubtypeRelation: {
        checkMode = (SignatureCheckMode)(SignatureCheckModeStrictTopSignature | SignatureCheckModeStrictArity);
        break;
    }}

    if (erase) {
        Ꮡsource = r.c.getErasedSignature(Ꮡsource); source = ref Ꮡsource.DerefOrNull();
        Ꮡtarget = r.c.getErasedSignature(Ꮡtarget); target = ref Ꮡtarget.DerefOrNull();
    }
    var isRelatedToWorker = (ж<Type> sourceΔ1, ж<Type> targetΔ1, bool reportErrorsΔ1) => Ꮡr.isRelatedToEx(sourceΔ1, targetΔ1, RecursionFlagsBoth, reportErrorsΔ1, nil, intersectionState);
    return r.c.compareSignaturesRelated(Ꮡsource, Ꮡtarget, checkMode, reportErrors, new Actionꓸꓸꓸ<ж<diagnostics.Message>, any>(Ꮡr.reportError), isRelatedToWorker, r.c.reportUnreliableMapper);
}

internal static Ternary signaturesIdenticalTo(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target, SignatureKind kind) {
    ref var r = ref Ꮡr.DerefOrNull();

    var sourceSignatures = r.c.getSignaturesOfType(source, kind);
    var targetSignatures = r.c.getSignaturesOfType(target, kind);
    if (len(sourceSignatures) != len(targetSignatures)) {
        return TernaryFalse;
    }
    var result = TernaryTrue;
    foreach (var i in range(len(sourceSignatures))) {
        var related = r.c.compareSignaturesIdentical(sourceSignatures[i], targetSignatures[i], false, false, false, Ꮡr.isRelatedToSimple);
        if (related == 0) {
            return TernaryFalse;
        }
        result &= (Ternary)(related);
    }
    return result;
}

internal static Ternary indexSignaturesRelatedTo(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target, bool sourceIsPrimitive, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (r.relation == r.c.identityRelation) {
        return Ꮡr.indexSignaturesIdenticalTo(source, target);
    }
    var indexInfos = r.c.getIndexInfosOfType(target);
    var targetHasStringIndex = core.Some(indexInfos, (ж<IndexInfo> info) => info.keyType == Ꮡr.Value.c.stringType);
    var result = TernaryTrue;
    foreach (var (_, targetInfo) in range(indexInfos)) {
        Ternary related = default;
        switch (ᐧ) {
        case {} when r.relation != r.c.strictSubtypeRelation && !sourceIsPrimitive && targetHasStringIndex && (TypeFlags)(targetInfo.valueType.flags & TypeFlagsAny) != 0: {
            related = TernaryTrue;
            break;
        }
        case {} when r.c.isGenericMappedType(source) && targetHasStringIndex: {
            related = Ꮡr.isRelatedTo(r.c.getTemplateTypeFromMappedType(source), targetInfo.valueType, RecursionFlagsBoth, reportErrors);
            break;
        }
        default: {
            related = Ꮡr.typeRelatedToIndexInfo(source, targetInfo, reportErrors, intersectionState);
            break;
        }}

        if (related == TernaryFalse) {
            return TernaryFalse;
        }
        result &= (Ternary)(related);
    }
    return result;
}

internal static Ternary typeRelatedToIndexInfo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<IndexInfo> ᏑtargetInfo, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var targetInfo = ref ᏑtargetInfo.DerefOrNull();

    var sourceInfo = r.c.getApplicableIndexInfo(Ꮡsource, targetInfo.keyType);
    if (sourceInfo != nil) {
        return Ꮡr.indexInfoRelatedTo(sourceInfo, ᏑtargetInfo, reportErrors, intersectionState);
    }
    if ((IntersectionState)(intersectionState & IntersectionStateSource) == 0 && (r.relation != r.c.strictSubtypeRelation || (ObjectFlags)(source.objectFlags & ObjectFlagsFreshLiteral) != 0) && r.c.isObjectTypeWithInferableIndex(Ꮡsource)) {
        return Ꮡr.membersRelatedToIndexInfo(Ꮡsource, ᏑtargetInfo, reportErrors, intersectionState);
    }
    if (reportErrors) {
        r.reportError(diagnostics.Index_signature_for_type_0_is_missing_in_type_1, r.c.TypeToString(targetInfo.keyType), r.c.TypeToString(Ꮡsource));
    }
    return TernaryFalse;
}

} partial class checkerᴛChecker {

internal static bool isObjectTypeWithInferableIndex(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsIntersection) != 0) {
        return core.Every<ж<Type>>(t.Types(), Ꮡc.isObjectTypeWithInferableIndex);
    }
    return t.symbol != nil && (ast.SymbolFlags)(t.symbol.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsObjectLiteral | ast.SymbolFlagsTypeLiteral) | ast.SymbolFlagsEnum) | ast.SymbolFlagsValueModule))) != 0 && (ast.SymbolFlags)(t.symbol.Flags & ast.SymbolFlagsClass) == 0 && !Ꮡc.typeHasCallOrConstructSignatures(Ꮡt) || (ObjectFlags)(t.objectFlags & ((ObjectFlags)(ObjectFlagsJSLiteral | (uint32)ObjectFlagsObjectRestType))) != 0 || (ObjectFlags)(t.objectFlags & ObjectFlagsReverseMapped) != 0 && Ꮡc.isObjectTypeWithInferableIndex(t.AsReverseMappedType().source);
}

} partial class checkerᴛRelater {

internal static Ternary membersRelatedToIndexInfo(this ж<Relater> Ꮡr, ж<Type> Ꮡsource, ж<IndexInfo> ᏑtargetInfo, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var targetInfo = ref ᏑtargetInfo.DerefOrNull();

    var result = TernaryTrue;
    var keyType = targetInfo.keyType;
    slice<ж<ast.Symbol>> props = default;
    if ((TypeFlags)(source.flags & TypeFlagsIntersection) != 0) {
        props = r.c.getPropertiesOfUnionOrIntersectionType(Ꮡsource);
    } else {
        props = r.c.getPropertiesOfObjectType(Ꮡsource);
    }
    foreach (var (_, prop) in range(props)) {
        if (isIgnoredJsxProperty(ref (Ꮡsource).DerefOrNull(), ref (prop).DerefOrNull())) {
            continue;
        }
        if (r.c.isApplicableIndexType(r.c.getLiteralTypeFromProperty(prop, TypeFlagsStringOrNumberLiteralOrUnique, false), keyType)) {
            var propType = r.c.getNonMissingTypeOfSymbol(prop);
            ж<Type> t = default;
            if (r.c.exactOptionalPropertyTypes || (TypeFlags)(propType.flags & TypeFlagsUndefined) != 0 || keyType == r.c.numberType || (ast.SymbolFlags)(prop.Flags & ast.SymbolFlagsOptional) == 0) {
                t = propType;
            } else {
                t = r.c.getTypeWithFacts(propType, TypeFactsNEUndefined);
            }
            var related = Ꮡr.isRelatedToEx(t, targetInfo.valueType, RecursionFlagsBoth, reportErrors, nil, intersectionState);
            if (related == TernaryFalse) {
                if (reportErrors) {
                    r.reportError(diagnostics.Property_0_is_incompatible_with_index_signature, r.c.symbolToString(prop));
                }
                return TernaryFalse;
            }
            result &= (Ternary)(related);
        }
    }
    foreach (var (_, info) in range(r.c.getIndexInfosOfType(Ꮡsource))) {
        if (r.c.isApplicableIndexType(info.keyType, keyType)) {
            var related = Ꮡr.indexInfoRelatedTo(info, ᏑtargetInfo, reportErrors, intersectionState);
            if (!(related != 0)) {
                return TernaryFalse;
            }
            result &= (Ternary)(related);
        }
    }
    return result;
}

internal static Ternary indexInfoRelatedTo(this ж<Relater> Ꮡr, ж<IndexInfo> ᏑsourceInfo, ж<IndexInfo> ᏑtargetInfo, bool reportErrors, IntersectionState intersectionState) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var sourceInfo = ref ᏑsourceInfo.DerefOrNull();
    ref var targetInfo = ref ᏑtargetInfo.DerefOrNull();

    var related = Ꮡr.isRelatedToEx(sourceInfo.valueType, targetInfo.valueType, RecursionFlagsBoth, reportErrors, nil, intersectionState);
    if (related == TernaryFalse && reportErrors) {
        if (sourceInfo.keyType == targetInfo.keyType) {
            r.reportError(diagnostics.X_0_index_signatures_are_incompatible, r.c.TypeToString(sourceInfo.keyType));
        } else {
            r.reportError(diagnostics.X_0_and_1_index_signatures_are_incompatible, r.c.TypeToString(sourceInfo.keyType), r.c.TypeToString(targetInfo.keyType));
        }
    }
    return related;
}

internal static Ternary indexSignaturesIdenticalTo(this ж<Relater> Ꮡr, ж<Type> source, ж<Type> target) {
    ref var r = ref Ꮡr.DerefOrNull();

    var sourceInfos = r.c.getIndexInfosOfType(source);
    var targetInfos = r.c.getIndexInfosOfType(target);
    if (len(sourceInfos) != len(targetInfos)) {
        return TernaryFalse;
    }
    foreach (var (_, targetInfo) in range(targetInfos)) {
        var sourceInfo = r.c.getIndexInfoOfType(source, targetInfo.keyType);
        if (!(sourceInfo != nil && Ꮡr.isRelatedTo(sourceInfo.valueType, targetInfo.valueType, RecursionFlagsBoth, false) != TernaryFalse && sourceInfo.isReadonly == targetInfo.isReadonly)) {
            return TernaryFalse;
        }
    }
    return TernaryTrue;
}

internal static void reportErrorResults(this ref Relater r, ж<Type> ᏑoriginalSource, ж<Type> ᏑoriginalTarget, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<diagnostics.Message> headMessage) {
    ref var originalSource = ref ᏑoriginalSource.DerefOrNull();
    ref var originalTarget = ref ᏑoriginalTarget.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var sourceHasBase = r.c.getSingleBaseForNonAugmentingSubtype(ᏑoriginalSource) != nil;
    var targetHasBase = r.c.getSingleBaseForNonAugmentingSubtype(ᏑoriginalTarget) != nil;
    if (originalSource.alias != nil || sourceHasBase) {
        Ꮡsource = ᏑoriginalSource; source = ref Ꮡsource.DerefOrNull();
    }
    if (originalTarget.alias != nil || targetHasBase) {
        Ꮡtarget = ᏑoriginalTarget; target = ref Ꮡtarget.DerefOrNull();
    }
    if ((TypeFlags)(source.flags & TypeFlagsObject) != 0 && (TypeFlags)(target.flags & TypeFlagsObject) != 0) {
        r.tryElaborateArrayLikeErrors(Ꮡsource, Ꮡtarget, true);
    }
    switch (ᐧ) {
    case {} when (TypeFlags)(source.flags & TypeFlagsObject) != 0 && (TypeFlags)(target.flags & TypeFlagsPrimitive) != 0: {
        r.tryElaborateErrorsForPrimitivesAndObjects(Ꮡsource, Ꮡtarget);
        break;
    }
    case {} when source.symbol != nil && (TypeFlags)(source.flags & TypeFlagsObject) != 0 && r.c.globalObjectType == Ꮡsource: {
        r.reportError(diagnostics.The_Object_type_is_assignable_to_very_few_other_types_Did_you_mean_to_use_the_any_type_instead);
        break;
    }
    case {} when (ObjectFlags)(source.objectFlags & ObjectFlagsJsxAttributes) != 0 && (TypeFlags)(target.flags & TypeFlagsIntersection) != 0: {
        var targetTypes = target.Types();
        var intrinsicAttributes = r.c.getJsxType(JsxNames.IntrinsicAttributes, r.errorNode);
        var intrinsicClassAttributes = r.c.getJsxType(JsxNames.IntrinsicClassAttributes, r.errorNode);
        if (!r.c.isErrorType(intrinsicAttributes) && !r.c.isErrorType(intrinsicClassAttributes) && (slices.Contains(targetTypes, intrinsicAttributes) || slices.Contains(targetTypes, intrinsicClassAttributes))) {
            return;
        }
        break;
    }
    case {} when (TypeFlags)(originalTarget.flags & TypeFlagsIntersection) != 0 && (ObjectFlags)(originalTarget.objectFlags & (uint32)ObjectFlagsIsNeverIntersection) != 0: {
        var message = diagnostics.The_intersection_0_was_reduced_to_never_because_property_1_has_conflicting_types_in_some_constituents;
        var prop = core.Find<ж<ast.Symbol>>(r.c.getPropertiesOfUnionOrIntersectionType(ᏑoriginalTarget), r.c.isDiscriminantWithNeverType);
        if (prop == nil) {
            message = diagnostics.The_intersection_0_was_reduced_to_never_because_property_1_exists_in_multiple_constituents_and_is_private_in_some;
            prop = core.Find<ж<ast.Symbol>>(r.c.getPropertiesOfUnionOrIntersectionType(ᏑoriginalTarget), isConflictingPrivateProperty);
        }
        if (prop != nil) {
            r.reportError(message, r.c.typeToStringEx(ᏑoriginalTarget, nil, TypeFormatFlagsNoTypeReduction, nil), r.c.symbolToString(prop));
        }
        break;
    }}

    r.reportRelationError(headMessage, Ꮡsource, Ꮡtarget);
    if ((TypeFlags)(source.flags & TypeFlagsTypeParameter) != 0 && source.symbol != nil && len(source.symbol.Declarations) != 0 && r.c.getConstraintOfType(Ꮡsource) == nil) {
        var syntheticParam = r.c.cloneTypeParameter(Ꮡsource);
        syntheticParam.AsTypeParameter().constraint = r.c.instantiateType(Ꮡtarget, newSimpleTypeMapper(Ꮡsource, syntheticParam));
        if (r.c.hasNonCircularBaseConstraint(syntheticParam)) {
            @string targetConstraintString = r.c.TypeToString(Ꮡtarget);
            r.relatedInfo = append(r.relatedInfo, NewDiagnosticForNode(source.symbol.Declarations[0], diagnostics.This_type_parameter_might_need_an_extends_0_constraint, targetConstraintString));
        }
    }
}

internal static void reportRelationError(this ref Relater r, ж<diagnostics.Message> Ꮡmessage, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var message = ref Ꮡmessage.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var (sourceType, targetType) = r.c.getTypeNamesForErrorDisplay(Ꮡsource, Ꮡtarget);
    var generalizedSource = Ꮡsource;
    @string generalizedSourceType = sourceType;
    if ((TypeFlags)(target.flags & TypeFlagsNever) == 0 && isLiteralType(Ꮡsource) && !r.c.typeCouldHaveTopLevelSingletonTypes(Ꮡtarget)) {
        generalizedSource = r.c.getBaseTypeOfLiteralType(Ꮡsource);
        generalizedSourceType = r.c.getTypeNameForErrorDisplay(generalizedSource);
    }
    TypeFlags targetFlags = default;
    if ((TypeFlags)(target.flags & TypeFlagsIndexedAccess) != 0 && (TypeFlags)(source.flags & TypeFlagsIndexedAccess) == 0) {
        targetFlags = target.AsIndexedAccessType().objectType.flags;
    } else {
        targetFlags = target.flags;
    }
    if ((TypeFlags)(targetFlags & TypeFlagsTypeParameter) != 0 && Ꮡtarget != r.c.markerSuperTypeForCheck && Ꮡtarget != r.c.markerSubTypeForCheck) {
        var constraint = r.c.getBaseConstraintOfType(Ꮡtarget);
        switch (ᐧ) {
        case {} when IsDistributedTypeParameter(Ꮡtarget) && r.c.isTypeAssignableTo(generalizedSource, target.AsTypeParameter().constraint): {
            r.reportError(diagnostics.X_0_is_only_assignable_to_the_non_distributed_1_but_1_has_been_distributed_here, generalizedSourceType, targetType);
            break;
        }
        case {} when constraint != nil && r.c.isTypeAssignableTo(generalizedSource, constraint): {
            r.reportError(diagnostics.X_0_is_assignable_to_the_constraint_of_type_1_but_1_could_be_instantiated_with_a_different_subtype_of_constraint_2, generalizedSourceType, targetType, r.c.TypeToString(constraint));
            break;
        }
        case {} when constraint != nil && r.c.isTypeAssignableTo(Ꮡsource, constraint): {
            r.reportError(diagnostics.X_0_is_assignable_to_the_constraint_of_type_1_but_1_could_be_instantiated_with_a_different_subtype_of_constraint_2, sourceType, targetType, r.c.TypeToString(constraint));
            break;
        }
        default: {
            r.errorChain = default;
            r.reportError(diagnostics.X_0_could_be_instantiated_with_an_arbitrary_type_which_could_be_unrelated_to_1, targetType, generalizedSourceType);
            break;
        }}

    }
    if (Ꮡmessage == nil) {
        switch (ᐧ) {
        case {} when r.relation == r.c.comparableRelation: {
            Ꮡmessage = diagnostics.Type_0_is_not_comparable_to_type_1; message = ref Ꮡmessage.DerefOrNull();
            break;
        }
        case {} when sourceType == targetType: {
            Ꮡmessage = diagnostics.Type_0_is_not_assignable_to_type_1_Two_different_types_with_this_name_exist_but_they_are_unrelated; message = ref Ꮡmessage.DerefOrNull();
            break;
        }
        case {} when r.c.exactOptionalPropertyTypes && len(r.c.getExactOptionalUnassignableProperties(Ꮡsource, Ꮡtarget)) != 0: {
            Ꮡmessage = diagnostics.Type_0_is_not_assignable_to_type_1_with_exactOptionalPropertyTypes_Colon_true_Consider_adding_undefined_to_the_types_of_the_target_s_properties; message = ref Ꮡmessage.DerefOrNull();
            break;
        }
        default: {
            if ((TypeFlags)(source.flags & TypeFlagsStringLiteral) != 0 && (TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
                var suggestedType = r.c.getSuggestedTypeForNonexistentStringLiteralType(Ꮡsource, Ꮡtarget);
                if (suggestedType != nil) {
                    r.reportError(diagnostics.Type_0_is_not_assignable_to_type_1_Did_you_mean_2, generalizedSourceType, targetType, r.c.TypeToString(suggestedType));
                    return;
                }
            }
            Ꮡmessage = diagnostics.Type_0_is_not_assignable_to_type_1; message = ref Ꮡmessage.DerefOrNull();
            break;
        }}

    } else if (Ꮡmessage == diagnostics.Argument_of_type_0_is_not_assignable_to_parameter_of_type_1 && r.c.exactOptionalPropertyTypes && len(r.c.getExactOptionalUnassignableProperties(Ꮡsource, Ꮡtarget)) > 0) {
        Ꮡmessage = diagnostics.Argument_of_type_0_is_not_assignable_to_parameter_of_type_1_with_exactOptionalPropertyTypes_Colon_true_Consider_adding_undefined_to_the_types_of_the_target_s_properties; message = ref Ꮡmessage.DerefOrNull();
    }
    switch (r.getChainMessage(0)) {
    case var ᴛ when ᴛ == diagnostics.Object_literal_may_only_specify_known_properties_and_0_does_not_exist_in_type_1 || ᴛ == diagnostics.Object_literal_may_only_specify_known_properties_but_0_does_not_exist_in_type_1_Did_you_mean_to_write_2: {
        return;
    }
    case var ᴛ when ᴛ == diagnostics.Excessive_complexity_comparing_types_0_and_1 || ᴛ == diagnostics.The_type_0_is_readonly_and_cannot_be_assigned_to_the_mutable_type_1: {
        if (r.chainArgsMatch(generalizedSourceType, targetType)) {
            return;
        }
        break;
    }
    case var ᴛ when ᴛ == diagnostics.Property_0_is_missing_in_type_1_but_required_in_type_2: {
        if (!isConversionOrInterfaceImplementationMessage(Ꮡmessage) && r.chainArgsMatch((any)(default), generalizedSourceType, targetType)) {
            return;
        }
        break;
    }
    case var ᴛ when ᴛ == diagnostics.Type_0_is_missing_the_following_properties_from_type_1_Colon_2_and_3_more || ᴛ == diagnostics.Type_0_is_missing_the_following_properties_from_type_1_Colon_2: {
        if (!isConversionOrInterfaceImplementationMessage(Ꮡmessage) && r.chainArgsMatch(generalizedSourceType, targetType)) {
            return;
        }
        break;
    }}

    r.reportError(Ꮡmessage, generalizedSourceType, targetType);
}

internal static void reportError(this ref Relater r, ж<diagnostics.Message> Ꮡmessage, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.slice();
    ref var message = ref Ꮡmessage.DerefOrNull();

    if (Ꮡmessage == diagnostics.Types_of_property_0_are_incompatible) {
        switch (r.getChainMessage(0)) {
        case var ᴛ when ᴛ == diagnostics.Object_literal_may_only_specify_known_properties_and_0_does_not_exist_in_type_1 || ᴛ == diagnostics.Object_literal_may_only_specify_known_properties_but_0_does_not_exist_in_type_1_Did_you_mean_to_write_2: {
            return;
        }}

        @string arg = default;
        switch (r.getChainMessage(1)) {
        case var ᴛ when ᴛ == diagnostics.Call_signatures_with_no_arguments_have_incompatible_return_types_0_and_1: {
            arg = getPropertyNameArg(args[0]) + "()"u8;
            break;
        }
        case var ᴛ when ᴛ == diagnostics.Construct_signatures_with_no_arguments_have_incompatible_return_types_0_and_1: {
            arg = "new "u8 + getPropertyNameArg(args[0]) + "()"u8;
            break;
        }
        case var ᴛ when ᴛ == diagnostics.Call_signature_return_types_0_and_1_are_incompatible: {
            arg = getPropertyNameArg(args[0]) + "(...)"u8;
            break;
        }
        case var ᴛ when ᴛ == diagnostics.Construct_signature_return_types_0_and_1_are_incompatible: {
            arg = "new "u8 + getPropertyNameArg(args[0]) + "(...)"u8;
            break;
        }}

        if (arg != ""u8) {
            Ꮡmessage = diagnostics.The_types_returned_by_0_are_incompatible_between_these_types; message = ref Ꮡmessage.DerefOrNull();
            args[0] = arg;
            r.errorChain = r.errorChain.next.next;
        }
        switch (r.getChainMessage(1)) {
        case var ᴛ when ᴛ == diagnostics.Types_of_property_0_are_incompatible || ᴛ == diagnostics.The_types_of_0_are_incompatible_between_these_types || ᴛ == diagnostics.The_types_returned_by_0_are_incompatible_between_these_types: {
            @string head = getPropertyNameArg(args[0]);
            @string tail = getPropertyNameArg(r.errorChain.next.args[0]);
            @string argΔ2 = addToDottedName(head, tail);
            r.errorChain = r.errorChain.next.next;
            if (Ꮡmessage == diagnostics.Types_of_property_0_are_incompatible) {
                Ꮡmessage = diagnostics.The_types_of_0_are_incompatible_between_these_types; message = ref Ꮡmessage.DerefOrNull();
            }
            r.reportError(Ꮡmessage, argΔ2);
            return;
        }}

    }
    r.errorChain = Ꮡ(new ErrorChain(next: r.errorChain, message: Ꮡmessage, args: args));
}

} partial class checker_package {

internal static @string addToDottedName(@string head, @string tail) {
    if (strings.HasPrefix(head, "new ")) {
        head = "("u8 + head + ")"u8;
    }
    nint pos = 0;
    for (;;) {
        if (strings.HasPrefix(tail.slice(pos), "(")) {
            pos++;
        } else if (strings.HasPrefix(tail.slice(pos), "new ")) {
            pos += 4;
        } else {
            break;
        }
    }
    @string prefix = tail.slice(0, pos);
    @string suffix = tail.slice(pos);
    if (strings.HasPrefix(suffix, "[")) {
        return prefix + head + suffix;
    }
    return prefix + head + "."u8 + suffix;
}

} partial class checkerᴛRelater {

internal static ж<diagnostics.Message> getChainMessage(this ref Relater r, nint index) {
    var e = r.errorChain;
    for (;;) {
        if (e == nil) {
            return default;
        }
        if (index == 0) {
            return e.message;
        }
        e = e.next;
        index--;
    }
}

internal static bool chainArgsMatch(this ref Relater r, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    foreach (var (i, a) in range(args)) {
        if (a != nil && !AreEqual(a, r.errorChain.args[i])) {
            return false;
        }
    }
    return true;
}

} partial class checker_package {

internal static @string getPropertyNameArg(any arg) {
    @string s = arg._<@string>();
    if (len(s) != 0 && (s[0] == '"' || s[0] == '\'' || s[0] == '`')) {
        return "["u8 + s + "]"u8;
    }
    return s;
}

internal static bool isConversionOrInterfaceImplementationMessage(ж<diagnostics.Message> message) {
    return message == diagnostics.Class_0_incorrectly_implements_interface_1 || message == diagnostics.Class_0_incorrectly_implements_class_1_Did_you_mean_to_extend_1_and_inherit_its_members_as_a_subclass || message == diagnostics.Conversion_of_type_0_to_type_1_may_be_a_mistake_because_neither_type_sufficiently_overlaps_with_the_other_If_this_was_intentional_convert_the_expression_to_unknown_first || message == diagnostics.Its_instance_type_0_is_not_a_valid_JSX_element || message == diagnostics.Its_return_type_0_is_not_a_valid_JSX_element || message == diagnostics.Its_element_type_0_is_not_a_valid_JSX_element;
}

internal static nint chainDepth(ж<ErrorChain> Ꮡchain) {
    ref var chain = ref Ꮡchain.DerefOrNull();

    nint depth = 0;
    while (Ꮡchain != nil) {
        depth++;
        Ꮡchain = chain.next; chain = ref Ꮡchain.DerefOrNull();
    }
    return depth;
}

} partial class checkerᴛChecker {

internal static bool isTypeDerivedFrom(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(source.flags & TypeFlagsUnion) != 0: {
        return core.Every(source.AsUnionType().types, (ж<Type> t) => Ꮡc.isTypeDerivedFrom(t, Ꮡtarget));
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsUnion) != 0: {
        return core.Some(target.AsUnionType().types, (ж<Type> t) => Ꮡc.isTypeDerivedFrom(Ꮡsource, t));
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsIntersection) != 0: {
        return core.Some(source.AsIntersectionType().types, (ж<Type> t) => Ꮡc.isTypeDerivedFrom(t, Ꮡtarget));
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsInstantiableNonPrimitive) != 0: {
        var constraint = Ꮡc.getBaseConstraintOfType(Ꮡsource);
        if (constraint == nil) {
            constraint = c.unknownType;
        }
        return Ꮡc.isTypeDerivedFrom(constraint, Ꮡtarget);
    }
    case {} when Ꮡc.IsEmptyAnonymousObjectType(Ꮡtarget): {
        return (TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsNonPrimitive))) != 0;
    }
    case {} when Ꮡtarget == c.globalObjectType: {
        return (TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsNonPrimitive))) != 0 && !Ꮡc.IsEmptyAnonymousObjectType(Ꮡsource);
    }
    case {} when Ꮡtarget == c.globalFunctionType: {
        return (TypeFlags)(source.flags & TypeFlagsObject) != 0 && Ꮡc.isFunctionObjectType(Ꮡsource);
    }
    default: {
        return Ꮡc.hasBaseType(Ꮡsource, c.getTargetType(Ꮡtarget)) || (c.isArrayType(Ꮡtarget) && !c.isReadonlyArrayType(Ꮡtarget) && Ꮡc.isTypeDerivedFrom(Ꮡsource, c.globalReadonlyArrayType));
    }}

}

internal static bool isDistributionDependent(this ж<Checker> Ꮡc, ж<ConditionalRoot> Ꮡroot) {
    ref var root = ref Ꮡroot.DerefOrNull();

    return root.isDistributive && (Ꮡc.isTypeParameterPossiblyReferenced(root.checkType, root.node.TrueType) || Ꮡc.isTypeParameterPossiblyReferenced(root.checkType, root.node.FalseType));
}

} partial class checkerᴛRelater {

internal static void traceUnionsOrIntersectionsTooLarge(this ref Relater r, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var tr = r.c.tracer;
    if (tr == nil) {
        return;
    }
    if ((TypeFlags)(source.flags & TypeFlagsUnionOrIntersection) != 0 && (TypeFlags)(target.flags & TypeFlagsUnionOrIntersection) != 0) {
        if ((ObjectFlags)((ObjectFlags)(source.objectFlags & target.objectFlags) & ObjectFlagsPrimitiveUnion) != 0) {
            return;
        }
        nint sourceSize = len(source.Types());
        nint targetSize = len(target.Types());
        if (sourceSize * targetSize > 1_000_000) {
            tr.Instant(tracing.PhaseCheckTypes, "traceUnionsOrIntersectionsTooLarge_DepthLimit", new map<@string, any>{["sourceId"] = source.id, ["sourceSize"] = sourceSize, ["targetId"] = target.id, ["targetSize"] = targetSize});
        }
    }
}

} // end checkerᴛRelater
