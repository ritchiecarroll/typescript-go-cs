namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

partial struct InferenceKey {
    internal TypeId s;
    internal TypeId t;
}

partial struct InferenceState {
    internal slice<ж<InferenceInfo>> inferences;
    internal ж<Type> originalSource;
    internal ж<Type> originalTarget;
    internal InferencePriority priority;
    internal InferencePriority inferencePriority;
    internal bool contravariant;
    internal bool bivariant;
    internal ExpandingFlags expandingFlags;
    internal ж<Type> propagationType;
    internal map<InferenceKey, InferencePriority> visited;
    internal slice<ж<Type>> sourceStack;
    internal slice<ж<Type>> targetStack;
    internal ж<InferenceState> next;
}

} partial class checkerᴛChecker {

internal static ж<InferenceState> getInferenceState(this ref Checker c) {
    var n = c.freeinferenceState;
    if (n == nil) {
        n = Ꮡ(new InferenceState());
    }
    c.freeinferenceState = n.next;
    return n;
}

internal static void putInferenceState(this ref Checker c, ж<InferenceState> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    clear(n.visited);
    n = new InferenceState(
        inferences: n.inferences[..0],
        visited: n.visited,
        sourceStack: n.sourceStack[..0],
        targetStack: n.targetStack[..0],
        next: c.freeinferenceState
    );
    c.freeinferenceState = Ꮡn;
}

internal static void inferTypes(this ж<Checker> Ꮡc, slice<ж<InferenceInfo>> inferences, ж<Type> ᏑoriginalSource, ж<Type> ᏑoriginalTarget, InferencePriority priority, bool contravariant) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var originalSource = ref ᏑoriginalSource.DerefOrNull();
    ref var originalTarget = ref ᏑoriginalTarget.DerefOrNull();

    var n = c.getInferenceState();
    n.inferences = inferences;
    n.originalSource = ᏑoriginalSource;
    n.originalTarget = ᏑoriginalTarget;
    n.priority = priority;
    n.inferencePriority = InferencePriorityMaxValue;
    n.contravariant = contravariant;
    Ꮡc.inferFromTypes(n, ᏑoriginalSource, ᏑoriginalTarget);
    c.putInferenceState(n);
}

internal static void inferFromTypes(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var n = ref Ꮡn.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if (!c.couldContainTypeVariables(Ꮡtarget) || c.isNoInferType(Ꮡtarget)) {
        return;
    }
    if (Ꮡsource == c.wildcardType || Ꮡsource == c.blockedStringType) {
        var savePropagationType = n.propagationType;
        n.propagationType = Ꮡsource;
        Ꮡc.inferFromTypes(Ꮡn, Ꮡtarget, Ꮡtarget);
        n.propagationType = savePropagationType;
        return;
    }
    if (source.alias != nil && target.alias != nil && source.alias.symbol == target.alias.symbol) {
        if (len(source.alias.typeArguments) != 0 || len(target.alias.typeArguments) != 0) {
            var @params = c.typeAliasLinks.Get(source.alias.symbol).typeParameters;
            nint minParams = c.getMinTypeArgumentCount(@params);
            var nodeIsInJsFile = ast.IsInJSFile(source.alias.symbol.ValueDeclaration);
            var sourceTypes = Ꮡc.fillMissingTypeArguments(source.alias.typeArguments, @params, minParams, nodeIsInJsFile);
            var targetTypes = Ꮡc.fillMissingTypeArguments(target.alias.typeArguments, @params, minParams, nodeIsInJsFile);
            Ꮡc.inferFromTypeArguments(Ꮡn, sourceTypes, targetTypes, Ꮡc.getAliasVariances(source.alias.symbol));
        }
        return;
    }
    if (Ꮡsource == Ꮡtarget && (TypeFlags)(source.flags & TypeFlagsUnionOrIntersection) != 0) {
        foreach (var (_, t) in range(source.Types())) {
            Ꮡc.inferFromTypes(Ꮡn, t, t);
        }
        return;
    }
    if ((TypeFlags)(target.flags & TypeFlagsUnion) != 0) {
        slice<ж<Type>> sourceTypes = default;
        if ((TypeFlags)(source.flags & TypeFlagsUnion) != 0) {
            sourceTypes = source.Types();
        } else {
            sourceTypes = [Ꮡsource];
        }
        var (tempSources, tempTargets) = Ꮡc.inferFromMatchingTypes(Ꮡn, sourceTypes, target.Types(), ((Func<ж<Checker>, ж<Type>, ж<Type>, bool>)(checkerᴛChecker.isTypeOrBaseIdenticalTo)), false);
        var (sources, targets) = Ꮡc.inferFromMatchingTypes(Ꮡn, tempSources, tempTargets, ((Func<ж<Checker>, ж<Type>, ж<Type>, bool>)(checkerᴛChecker.isTypeCloselyMatchedBy)), true);
        if (len(targets) == 0) {
            return;
        }
        Ꮡtarget = Ꮡc.getUnionType(targets); target = ref Ꮡtarget.DerefOrNull();
        if (len(sources) == 0) {
            Ꮡc.inferWithPriority(Ꮡn, Ꮡsource, Ꮡtarget, InferencePriorityNakedTypeVariable);
            return;
        }
        Ꮡsource = Ꮡc.getUnionType(sources); source = ref Ꮡsource.DerefOrNull();
    } else if ((TypeFlags)(target.flags & TypeFlagsIntersection) != 0 && !core.Every<ж<Type>>(target.Types(), Ꮡc.isNonGenericObjectType)) {
        if ((TypeFlags)(source.flags & TypeFlagsUnion) == 0) {
            slice<ж<Type>> sourceTypes = default;
            if ((TypeFlags)(source.flags & TypeFlagsIntersection) != 0) {
                sourceTypes = source.Types();
            } else {
                sourceTypes = [Ꮡsource];
            }
            var (sources, targets) = Ꮡc.inferFromMatchingTypes(Ꮡn, sourceTypes, target.Types(), ((Func<ж<Checker>, ж<Type>, ж<Type>, bool>)(checkerᴛChecker.isTypeIdenticalTo)), false);
            if (len(sources) == 0 || len(targets) == 0) {
                return;
            }
            Ꮡsource = Ꮡc.getIntersectionType(sources); source = ref Ꮡsource.DerefOrNull();
            Ꮡtarget = Ꮡc.getIntersectionType(targets); target = ref Ꮡtarget.DerefOrNull();
        }
    }
    if ((TypeFlags)(target.flags & ((TypeFlags)(TypeFlagsIndexedAccess | TypeFlagsSubstitution))) != 0) {
        if (c.isNoInferType(Ꮡtarget)) {
            return;
        }
        Ꮡtarget = Ꮡc.getActualTypeVariable(Ꮡtarget); target = ref Ꮡtarget.DerefOrNull();
    }
    if ((TypeFlags)(target.flags & TypeFlagsTypeVariable) != 0) {
        if (Ꮡc.isFromInferenceBlockedSource(Ꮡsource)) {
            return;
        }
        var inference = getInferenceInfoForType(ref (Ꮡn).DerefOrNull(), Ꮡtarget);
        if (inference != nil) {
            if ((ObjectFlags)(source.objectFlags & ObjectFlagsNonInferrableType) != 0 || Ꮡsource == c.nonInferrableAnyType) {
                return;
            }
            if (!inference.isFixed) {
                var candidate = core.OrElse(n.propagationType, Ꮡsource);
                if (candidate == c.blockedStringType) {
                    return;
                }
                if (n.priority < inference.priority) {
                    inference.candidates = default;
                    inference.contraCandidates = default;
                    inference.topLevel = true;
                    inference.priority = n.priority;
                }
                if (n.priority == inference.priority) {
                    if (n.contravariant && !n.bivariant) {
                        if (!slices.Contains(inference.contraCandidates, candidate)) {
                            inference.contraCandidates = append(inference.contraCandidates, candidate);
                            clearCachedInferences(n.inferences);
                        }
                    } else if (!slices.Contains(inference.candidates, candidate)) {
                        inference.candidates = append(inference.candidates, candidate);
                        clearCachedInferences(n.inferences);
                    }
                }
                if ((InferencePriority)(n.priority & InferencePriorityReturnType) == 0 && (TypeFlags)(target.flags & TypeFlagsTypeParameter) != 0 && inference.topLevel && !Ꮡc.isTypeParameterAtTopLevel(n.originalTarget, Ꮡtarget, 0)) {
                    inference.topLevel = false;
                    clearCachedInferences(n.inferences);
                }
            }
            n.inferencePriority = min(n.inferencePriority, n.priority);
            return;
        }
        var simplified = Ꮡc.getSimplifiedType(Ꮡtarget, false);
        if (simplified != Ꮡtarget) {
            Ꮡc.inferFromTypes(Ꮡn, Ꮡsource, simplified);
        } else if ((TypeFlags)(target.flags & TypeFlagsIndexedAccess) != 0) {
            var indexType = Ꮡc.getSimplifiedType(target.AsIndexedAccessType().indexType, false);
            if ((TypeFlags)(indexType.flags & TypeFlagsInstantiable) != 0) {
                var simplifiedΔ1 = Ꮡc.distributeIndexOverObjectType(Ꮡc.getSimplifiedType(target.AsIndexedAccessType().objectType, false), indexType, false);
                if (simplifiedΔ1 != nil && simplifiedΔ1 != Ꮡtarget) {
                    Ꮡc.inferFromTypes(Ꮡn, Ꮡsource, simplifiedΔ1);
                }
            }
        }
    }
    switch (ᐧ) {
    case {} when (ObjectFlags)(source.objectFlags & ObjectFlagsReference) != 0 && (ObjectFlags)(target.objectFlags & ObjectFlagsReference) != 0 && (source.AsTypeReference().target == target.AsTypeReference().target || c.isArrayType(Ꮡsource) && c.isArrayType(Ꮡtarget)) && !(source.AsTypeReference().node != nil && target.AsTypeReference().node != nil): {
        Ꮡc.inferFromTypeArguments(Ꮡn, Ꮡc.getTypeArguments(Ꮡsource), Ꮡc.getTypeArguments(Ꮡtarget), Ꮡc.getVariances(source.AsTypeReference().target));
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsIndex) != 0 && (TypeFlags)(target.flags & TypeFlagsIndex) != 0: {
        Ꮡc.inferFromContravariantTypes(Ꮡn, source.AsIndexType().target, target.AsIndexType().target);
        break;
    }
    case {} when (isLiteralType(Ꮡsource) || (TypeFlags)(source.flags & TypeFlagsString) != 0) && (TypeFlags)(target.flags & TypeFlagsIndex) != 0: {
        var empty = Ꮡc.createEmptyObjectTypeFromStringLiteral(Ꮡsource);
        Ꮡc.inferFromContravariantTypesWithPriority(Ꮡn, empty, target.AsIndexType().target, InferencePriorityLiteralKeyof);
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsIndexedAccess) != 0 && (TypeFlags)(target.flags & TypeFlagsIndexedAccess) != 0: {
        Ꮡc.inferFromTypes(Ꮡn, source.AsIndexedAccessType().objectType, target.AsIndexedAccessType().objectType);
        Ꮡc.inferFromTypes(Ꮡn, source.AsIndexedAccessType().indexType, target.AsIndexedAccessType().indexType);
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsStringMapping) != 0 && (TypeFlags)(target.flags & TypeFlagsStringMapping) != 0: {
        if (source.symbol == target.symbol) {
            Ꮡc.inferFromTypes(Ꮡn, source.AsStringMappingType().target, target.AsStringMappingType().target);
        }
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsSubstitution) != 0: {
        Ꮡc.inferFromTypes(Ꮡn, source.AsSubstitutionType().baseType, Ꮡtarget);
        Ꮡc.inferWithPriority(Ꮡn, Ꮡc.getSubstitutionIntersection(Ꮡsource), Ꮡtarget, InferencePrioritySubstituteSource);
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsConditional) != 0: {
        Ꮡc.invokeOnce(Ꮡn, Ꮡsource, Ꮡtarget, ((Action<ж<Checker>, ж<InferenceState>, ж<Type>, ж<Type>>)(checkerᴛChecker.inferToConditionalType)));
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsUnionOrIntersection) != 0: {
        Ꮡc.inferToMultipleTypes(Ꮡn, Ꮡsource, target.Types(), target.flags);
        break;
    }
    case {} when (TypeFlags)(source.flags & TypeFlagsUnion) != 0: {
        foreach (var (_, sourceType) in range(source.Types())) {
            Ꮡc.inferFromTypes(Ꮡn, sourceType, Ꮡtarget);
        }
        break;
    }
    case {} when (TypeFlags)(target.flags & TypeFlagsTemplateLiteral) != 0: {
        Ꮡc.inferToTemplateLiteralType(Ꮡn, Ꮡsource, target.AsTemplateLiteralType());
        break;
    }
    default: {
        Ꮡsource = Ꮡc.getReducedType(Ꮡsource); source = ref Ꮡsource.DerefOrNull();
        if (Ꮡc.isGenericMappedType(Ꮡsource) && Ꮡc.isGenericMappedType(Ꮡtarget)) {
            Ꮡc.invokeOnce(Ꮡn, Ꮡsource, Ꮡtarget, ((Action<ж<Checker>, ж<InferenceState>, ж<Type>, ж<Type>>)(checkerᴛChecker.inferFromGenericMappedTypes)));
        }
        if (!((InferencePriority)(n.priority & InferencePriorityNoConstraints) != 0 && (TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsIntersection | TypeFlagsInstantiable))) != 0)) {
            var apparentSource = Ꮡc.getApparentType(Ꮡsource);
            if (apparentSource != Ꮡsource && (TypeFlags)(apparentSource.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection))) == 0) {
                Ꮡc.inferFromTypes(Ꮡn, apparentSource, Ꮡtarget);
                return;
            }
            Ꮡsource = apparentSource; source = ref Ꮡsource.DerefOrNull();
        }
        if ((TypeFlags)(source.flags & ((TypeFlags)(TypeFlagsObject | TypeFlagsIntersection))) != 0) {
            Ꮡc.invokeOnce(Ꮡn, Ꮡsource, Ꮡtarget, ((Action<ж<Checker>, ж<InferenceState>, ж<Type>, ж<Type>>)(checkerᴛChecker.inferFromObjectTypes)));
        }
        break;
    }}

}

internal static void inferFromTypeArguments(this ж<Checker> Ꮡc, ж<InferenceState> n, slice<ж<Type>> sourceTypes, slice<ж<Type>> targetTypes, slice<VarianceFlags> variances) {
    foreach (var i in range(min(len(sourceTypes), len(targetTypes)))) {
        if (i < len(variances) && (VarianceFlags)(variances[i] & VarianceFlagsVarianceMask) == VarianceFlagsContravariant) {
            Ꮡc.inferFromContravariantTypes(n, sourceTypes[i], targetTypes[i]);
        } else {
            Ꮡc.inferFromTypes(n, sourceTypes[i], targetTypes[i]);
        }
    }
}

internal static void inferWithPriority(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> source, ж<Type> target, InferencePriority newPriority) {
    ref var n = ref Ꮡn.DerefOrNull();

    var savePriority = n.priority;
    n.priority |= (InferencePriority)(newPriority);
    Ꮡc.inferFromTypes(Ꮡn, source, target);
    n.priority = savePriority;
}

internal static void inferFromContravariantTypesWithPriority(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> source, ж<Type> target, InferencePriority newPriority) {
    ref var n = ref Ꮡn.DerefOrNull();

    var savePriority = n.priority;
    n.priority |= (InferencePriority)(newPriority);
    Ꮡc.inferFromContravariantTypes(Ꮡn, source, target);
    n.priority = savePriority;
}

internal static void inferFromContravariantTypes(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> source, ж<Type> target) {
    ref var n = ref Ꮡn.DerefOrNull();

    n.contravariant = !n.contravariant;
    Ꮡc.inferFromTypes(Ꮡn, source, target);
    n.contravariant = !n.contravariant;
}

internal static void inferFromContravariantTypesIfStrictFunctionTypes(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> source, ж<Type> target) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var n = ref Ꮡn.DerefOrNull();

    if (c.strictFunctionTypes || (InferencePriority)(n.priority & InferencePriorityAlwaysStrict) != 0) {
        Ꮡc.inferFromContravariantTypes(Ꮡn, source, target);
    } else {
        Ꮡc.inferFromTypes(Ꮡn, source, target);
    }
}

internal static void invokeOnce(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, Action<ж<Checker>, ж<InferenceState>, ж<Type>, ж<Type>> action) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var n = ref Ꮡn.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var key = new InferenceKey(s: source.id, t: target.id);
    var (status, ok) = n.visited[key, ꟷ]; if (ok) {
        n.inferencePriority = min(n.inferencePriority, status);
        return;
    }
    if (n.visited == nil) {
        n.visited = new map<InferenceKey, InferencePriority>();
    }
    n.visited[key] = InferencePriorityCircularity;
    var saveInferencePriority = n.inferencePriority;
    n.inferencePriority = InferencePriorityMaxValue;
    var saveExpandingFlags = n.expandingFlags;
    n.sourceStack = append(n.sourceStack, Ꮡsource);
    n.targetStack = append(n.targetStack, Ꮡtarget);
    if (c.isDeeplyNestedType(Ꮡsource, n.sourceStack, 2)) {
        n.expandingFlags |= (ExpandingFlags)(ExpandingFlagsSource);
    }
    if (c.isDeeplyNestedType(Ꮡtarget, n.targetStack, 2)) {
        n.expandingFlags |= (ExpandingFlags)(ExpandingFlagsTarget);
    }
    if (n.expandingFlags != ExpandingFlagsBoth) {
        action(Ꮡc, Ꮡn, Ꮡsource, Ꮡtarget);
    } else {
        n.inferencePriority = InferencePriorityCircularity;
    }
    n.targetStack = n.targetStack.slice(0, len(n.targetStack) - 1);
    n.sourceStack = n.sourceStack.slice(0, len(n.sourceStack) - 1);
    n.expandingFlags = saveExpandingFlags;
    n.visited[key] = n.inferencePriority;
    n.inferencePriority = min(n.inferencePriority, saveInferencePriority);
}

internal static (slice<ж<Type>>, slice<ж<Type>>) inferFromMatchingTypes(this ж<Checker> Ꮡc, ж<InferenceState> n, slice<ж<Type>> sources, slice<ж<Type>> targets, Func<ж<Checker>, ж<Type>, ж<Type>, bool> matches, bool sort) {
    slice<ж<Type>> matchedSources = default;
    slice<ж<Type>> matchedTargets = default;
    foreach (var (_, t) in range(targets)) {
        foreach (var (_, s) in range(sources)) {
            if (matches(Ꮡc, s, t)) {
                if (!sort) {
                    Ꮡc.inferFromTypes(n, s, t);
                }
                matchedSources = core.AppendIfUnique(matchedSources, s);
                matchedTargets = core.AppendIfUnique(matchedTargets, t);
            }
        }
    }
    if (sort) {
        slices.SortFunc<slice<ж<Type>>, ж<Type>>(matchedTargets, compareTypesAndDepth);
        foreach (var (_, t) in range(matchedTargets)) {
            foreach (var (_, s) in range(matchedSources)) {
                if (matches(Ꮡc, s, t)) {
                    Ꮡc.inferFromTypes(n, s, t);
                }
            }
        }
    }
    if (len(matchedSources) != 0) {
        var matchedSourcesʗ1 = matchedSources;
        sources = core.Filter(sources, (ж<Type> t) => !slices.Contains(matchedSourcesʗ1, t));
    }
    if (len(matchedTargets) != 0) {
        var matchedTargetsʗ1 = matchedTargets;
        targets = core.Filter(targets, (ж<Type> t) => !slices.Contains(matchedTargetsʗ1, t));
    }
    return (sources, targets);
}

} partial class checker_package {

internal static nint compareTypesAndDepth(ж<Type> t1, ж<Type> t2) {
    nint d1 = getTypeDepth(t1, 3);
    nint d2 = getTypeDepth(t2, 3);
    if (d1 != d2) {
        return d2 - d1;
    }
    return CompareTypes(t1, t2);
}

internal static nint getTypeDepth(ж<Type> Ꮡt, nint maxDepth) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (maxDepth != 0) {
        if (t.alias != nil && len(t.alias.typeArguments) != 0) {
            return getTypeListDepth(t.alias.typeArguments, maxDepth - 1) + 1;
        }
        if ((ObjectFlags)(t.objectFlags & ObjectFlagsReference) != 0) {
            var typeArguments = t.checker.getTypeArguments(Ꮡt); if (len(typeArguments) != 0) {
                return getTypeListDepth(typeArguments, maxDepth - 1) + 1;
            }
        }
        if ((TypeFlags)(t.flags & TypeFlagsUnionOrIntersection) != 0) {
            return getTypeListDepth(t.Types(), maxDepth);
        }
    }
    return 0;
}

internal static nint getTypeListDepth(slice<ж<Type>> types, nint maxDepth) {
    nint depth = 0;
    foreach (var (_, t) in range(types)) {
        depth = max(depth, getTypeDepth(t, maxDepth));
    }
    return depth;
}

} partial class checkerᴛChecker {

internal static void inferToMultipleTypes(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> Ꮡsource, slice<ж<Type>> targets, TypeFlags targetFlags) {
    ref var n = ref Ꮡn.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    var unmatchedᴷ = new stackAppend<ж<Type>>();

    nint typeVariableCount = 0;
    if ((TypeFlags)(targetFlags & TypeFlagsUnion) != 0) {
        ж<Type> nakedTypeVariable = default;
        slice<ж<Type>> sources = default;
        if ((TypeFlags)(source.flags & TypeFlagsUnion) != 0) {
            sources = source.Types();
        } else {
            sources = [Ꮡsource];
        }
        var matched = new slice<bool>(len(sources));
        var inferenceCircularity = false;
        foreach (var (_, t) in range(targets)) {
            if (getInferenceInfoForType(ref (Ꮡn).DerefOrNull(), t) != nil) {
                nakedTypeVariable = t;
                typeVariableCount++;
            } else {
                foreach (var i in range(len(sources))) {
                    var saveInferencePriority = n.inferencePriority;
                    n.inferencePriority = InferencePriorityMaxValue;
                    Ꮡc.inferFromTypes(Ꮡn, sources[i], t);
                    if (n.inferencePriority == n.priority) {
                        matched[i] = true;
                    }
                    inferenceCircularity = inferenceCircularity || n.inferencePriority == InferencePriorityCircularity;
                    n.inferencePriority = min(n.inferencePriority, saveInferencePriority);
                }
            }
        }
        if (typeVariableCount == 0) {
            var intersectionTypeVariable = getSingleTypeVariableFromIntersectionTypes(Ꮡn, targets);
            if (intersectionTypeVariable != nil) {
                Ꮡc.inferWithPriority(Ꮡn, Ꮡsource, intersectionTypeVariable, InferencePriorityNakedTypeVariable);
            }
            return;
        }
        if (typeVariableCount == 1 && !inferenceCircularity) {
            slice<ж<Type>> unmatched = default;
            foreach (var (i, s) in range(sources)) {
                if (!matched[i]) {
                    unmatched = append(unmatchedᴷ, unmatched, s);
                }
            }
            if (len(unmatched) != 0) {
                Ꮡc.inferFromTypes(Ꮡn, Ꮡc.getUnionType(unmatched), nakedTypeVariable);
                return;
            }
        }
    } else {
        foreach (var (_, t) in range(targets)) {
            if (getInferenceInfoForType(ref (Ꮡn).DerefOrNull(), t) != nil) {
                typeVariableCount++;
            } else {
                Ꮡc.inferFromTypes(Ꮡn, Ꮡsource, t);
            }
        }
    }
    if ((TypeFlags)(targetFlags & TypeFlagsIntersection) != 0 && typeVariableCount == 1 || (TypeFlags)(targetFlags & TypeFlagsIntersection) == 0 && typeVariableCount > 0) {
        foreach (var (_, t) in range(targets)) {
            if (getInferenceInfoForType(ref (Ꮡn).DerefOrNull(), t) != nil) {
                Ꮡc.inferWithPriority(Ꮡn, Ꮡsource, t, InferencePriorityNakedTypeVariable);
            }
        }
    }
}

} partial class checker_package {

internal static ж<Type> getSingleTypeVariableFromIntersectionTypes(ж<InferenceState> n, slice<ж<Type>> types) {
    ж<Type> typeVariable = default;
    foreach (var (_, t) in range(types)) {
        if ((TypeFlags)(t.flags & TypeFlagsIntersection) == 0) {
            return default;
        }
        var v = core.Find(t.Types(), (ж<Type> tΔ1) => getInferenceInfoForType(ref (n).DerefOrNull(), tΔ1) != nil);
        if (v == nil || typeVariable != nil && v != typeVariable) {
            return default;
        }
        typeVariable = v;
    }
    return typeVariable;
}

} partial class checkerᴛChecker {

internal static void inferToMultipleTypesWithPriority(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> source, slice<ж<Type>> targets, TypeFlags targetFlags, InferencePriority newPriority) {
    ref var n = ref Ꮡn.DerefOrNull();

    var savePriority = n.priority;
    n.priority |= (InferencePriority)(newPriority);
    Ꮡc.inferToMultipleTypes(Ꮡn, source, targets, targetFlags);
    n.priority = savePriority;
}

internal static void inferToConditionalType(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var n = ref Ꮡn.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsConditional) != 0) {
        Ꮡc.inferFromTypes(Ꮡn, getNonDistributedTypeParameter(source.AsConditionalType().checkType), target.AsConditionalType().checkType);
        Ꮡc.inferFromTypes(Ꮡn, getNonDistributedTypeParameter(source.AsConditionalType().extendsType), target.AsConditionalType().extendsType);
        Ꮡc.inferFromTypes(Ꮡn, getNonDistributedTypeParameter(Ꮡc.getTrueTypeFromConditionalType(Ꮡsource)), Ꮡc.getTrueTypeFromConditionalType(Ꮡtarget));
        Ꮡc.inferFromTypes(Ꮡn, getNonDistributedTypeParameter(Ꮡc.getFalseTypeFromConditionalType(Ꮡsource)), Ꮡc.getFalseTypeFromConditionalType(Ꮡtarget));
    } else {
        var targetTypes = slice<ж<Type>>([Ꮡc.getTrueTypeFromConditionalType(Ꮡtarget), Ꮡc.getFalseTypeFromConditionalType(Ꮡtarget)]);
        Ꮡc.inferToMultipleTypesWithPriority(Ꮡn, Ꮡsource, targetTypes, target.flags, core.IfElse(n.contravariant, InferencePriorityContravariantConditional, (InferencePriority)0));
    }
}

internal static void inferToTemplateLiteralType(this ж<Checker> Ꮡc, ж<InferenceState> n, ж<Type> source, ж<TemplateLiteralType> Ꮡtarget) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var matches = Ꮡc.inferTypesFromTemplateLiteralType(source, Ꮡtarget, c.compareTypesAssignable);
    var types = target.types;
    if (len(matches) != 0 || core.Every(target.texts, (@string s) => s == ""u8)) {
        foreach (var (i, targetΔ1) in range(types)) {
            ref var sourceΔ1 = ref heap<ж<Type>>(out var ᏑsourceΔ1);
            if (len(matches) != 0) {
                sourceΔ1 = matches[i];
            } else {
                sourceΔ1 = c.neverType;
            }
            if ((TypeFlags)(sourceΔ1.flags & TypeFlagsStringLiteral) != 0 && (TypeFlags)(targetΔ1.flags & TypeFlagsTypeVariable) != 0) {
                {
                    var inferenceContext = getInferenceInfoForType(ref (n).DerefOrNull(), targetΔ1); if (inferenceContext != nil) {
                        {
                            var constraint = Ꮡc.getBaseConstraintOfType(inferenceContext.typeParameter); if (constraint != nil && !IsTypeAny(constraint)) {
                                var allTypeFlags = TypeFlagsNone;
                                foreach (var (_, t) in range(constraint.Distributed())) {
                                    allTypeFlags |= (TypeFlags)(t.flags);
                                }
                                if ((TypeFlags)(allTypeFlags & TypeFlagsString) == 0) {
                                    @string str = getStringLiteralValue(sourceΔ1);
                                    if ((TypeFlags)(allTypeFlags & TypeFlagsNumberLike) != 0 && !isValidNumberString(str, true)) {
                                        allTypeFlags &= unchecked((TypeFlags)~(TypeFlags)(TypeFlagsNumberLike));
                                    }
                                    if ((TypeFlags)(allTypeFlags & TypeFlagsBigIntLike) != 0 && !isValidBigIntString(str, true)) {
                                        allTypeFlags &= unchecked((TypeFlags)~(TypeFlags)(TypeFlagsBigIntLike));
                                    }
                                    ж<Type> choose(ж<Type> left, ж<Type> right) {
                                        switch (ᐧ) {
                                        case {} when (TypeFlags)(right.flags & allTypeFlags) == 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsString) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsString) != 0: {
                                            return ᏑsourceΔ1.ValueSlot;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsTemplateLiteral) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsTemplateLiteral) != 0 && Ꮡc.isTypeMatchedByTemplateLiteralType(ᏑsourceΔ1.ValueSlot, right.AsTemplateLiteralType(), Ꮡc.Value.compareTypesAssignable): {
                                            return ᏑsourceΔ1.ValueSlot;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsStringMapping) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsStringMapping) != 0 && str == applyStringMapping(ref (right.symbol).DerefOrNull(), str): {
                                            return ᏑsourceΔ1.ValueSlot;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsStringLiteral) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsStringLiteral) != 0 && getStringLiteralValue(right) == str: {
                                            return right;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsNumber) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsNumber) != 0: {
                                            return Ꮡc.getNumberLiteralType(jsnum.FromString(str));
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsEnum) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsEnum) != 0: {
                                            return Ꮡc.getNumberLiteralType(jsnum.FromString(str));
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsNumberLiteral) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsNumberLiteral) != 0 && getNumberLiteralValue(right) == jsnum.FromString(str): {
                                            return right;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsBigInt) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsBigInt) != 0: {
                                            return Ꮡc.parseBigIntLiteralType(str);
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsBigIntLiteral) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsBigIntLiteral) != 0 && pseudoBigIntToString(getBigIntLiteralValue(right)) == str: {
                                            return right;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsBoolean) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsBoolean) != 0: {
                                            switch (ᐧ) {
                                            case {} when str == "true"u8: {
                                                return Ꮡc.Value.trueType;
                                            }
                                            case {} when str == "false"u8: {
                                                return Ꮡc.Value.falseType;
                                            }
                                            default: {
                                                return Ꮡc.Value.booleanType;
                                            }}

                                            break;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsBooleanLiteral) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsBooleanLiteral) != 0 && core.IfElse(getBooleanLiteralValue(right), trueˢ, falseˢ) == str: {
                                            return right;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsUndefined) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsUndefined) != 0 && right.AsIntrinsicType().intrinsicName == str: {
                                            return right;
                                        }
                                        case {} when (TypeFlags)(left.flags & TypeFlagsNull) != 0: {
                                            return left;
                                        }
                                        case {} when (TypeFlags)(right.flags & TypeFlagsNull) != 0 && right.AsIntrinsicType().intrinsicName == str: {
                                            return right;
                                        }
                                        default: {
                                            return left;
                                        }}

                                    }
                                    var matchingType = c.neverType;
                                    foreach (var (_, t) in range(constraint.Distributed())) {
                                        matchingType = choose(matchingType, t);
                                    }
                                    if ((TypeFlags)(matchingType.flags & TypeFlagsNever) == 0) {
                                        Ꮡc.inferFromTypes(n, matchingType, targetΔ1);
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Ꮡc.inferFromTypes(n, sourceΔ1, targetΔ1);
        }
    }
}

internal static void inferFromGenericMappedTypes(this ж<Checker> Ꮡc, ж<InferenceState> n, ж<Type> source, ж<Type> target) {
    Ꮡc.inferFromTypes(n, Ꮡc.getConstraintTypeFromMappedType(source), Ꮡc.getConstraintTypeFromMappedType(target));
    Ꮡc.inferFromTypes(n, Ꮡc.getTemplateTypeFromMappedType(source), Ꮡc.getTemplateTypeFromMappedType(target));
    var sourceNameType = Ꮡc.getNameTypeFromMappedType(source);
    var targetNameType = Ꮡc.getNameTypeFromMappedType(target);
    if (sourceNameType != nil && targetNameType != nil) {
        Ꮡc.inferFromTypes(n, sourceNameType, targetNameType);
    }
}

internal static void inferFromObjectTypes(this ж<Checker> Ꮡc, ж<InferenceState> n, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((ObjectFlags)(source.objectFlags & ObjectFlagsReference) != 0 && (ObjectFlags)(target.objectFlags & ObjectFlagsReference) != 0 && (source.Target() == target.Target() || c.isArrayType(Ꮡsource) && c.isArrayType(Ꮡtarget))) {
        Ꮡc.inferFromTypeArguments(n, Ꮡc.getTypeArguments(Ꮡsource), Ꮡc.getTypeArguments(Ꮡtarget), Ꮡc.getVariances(source.Target()));
        return;
    }
    if (Ꮡc.isGenericMappedType(Ꮡsource) && Ꮡc.isGenericMappedType(Ꮡtarget)) {
        Ꮡc.inferFromGenericMappedTypes(n, Ꮡsource, Ꮡtarget);
    }
    if ((ObjectFlags)(target.objectFlags & ObjectFlagsMapped) != 0 && target.AsMappedType().declaration.NameType == nil) {
        var constraintType = Ꮡc.getConstraintTypeFromMappedType(Ꮡtarget);
        if (Ꮡc.inferToMappedType(n, Ꮡsource, Ꮡtarget, constraintType)) {
            return;
        }
    }
    if (Ꮡc.typesDefinitelyUnrelated(Ꮡsource, Ꮡtarget)) {
        return;
    }
    if (c.isArrayOrTupleType(Ꮡsource)) {
        if (isTupleType(Ꮡtarget)) {
            nint sourceArity = c.getTypeReferenceArity(Ꮡsource);
            nint targetArity = c.getTypeReferenceArity(Ꮡtarget);
            var elementTypes = Ꮡc.getTypeArguments(Ꮡtarget);
            var elementInfos = target.TargetTupleType().elementInfos;
            if (isTupleType(Ꮡsource) && c.isTupleTypeStructureMatching(Ꮡsource, Ꮡtarget)) {
                foreach (var i in range(targetArity)) {
                    Ꮡc.inferFromTypes(n, Ꮡc.getTypeArguments(Ꮡsource)[i], elementTypes[i]);
                }
                return;
            }
            nint startLength = 0;
            nint endLength = 0;
            if (isTupleType(Ꮡsource)) {
                startLength = min(source.TargetTupleType().fixedLength, target.TargetTupleType().fixedLength);
                if ((ElementFlags)(target.TargetTupleType().combinedFlags & ElementFlagsVariable) != 0) {
                    endLength = min(getEndElementCount(ref (source.TargetTupleType()).DerefOrNull(), ElementFlagsFixed), getEndElementCount(ref (target.TargetTupleType()).DerefOrNull(), ElementFlagsFixed));
                }
            }
            foreach (var i in range(startLength)) {
                Ꮡc.inferFromTypes(n, Ꮡc.getTypeArguments(Ꮡsource)[i], elementTypes[i]);
            }
            if (!isTupleType(Ꮡsource) || sourceArity - startLength - endLength == 1 && (ElementFlags)(source.TargetTupleType().elementInfos[startLength].flags & ElementFlagsRest) != 0) {
                var restType = Ꮡc.getTypeArguments(Ꮡsource)[startLength];
                for (nint i = startLength; i < targetArity - endLength; i++) {
                    var t = restType;
                    if ((ElementFlags)(elementInfos[i].flags & ElementFlagsVariadic) != 0) {
                        t = Ꮡc.createArrayType(t);
                    }
                    Ꮡc.inferFromTypes(n, t, elementTypes[i]);
                }
            } else {
                nint middleLength = targetArity - startLength - endLength;
                if (middleLength == 2) {
                    if ((ElementFlags)((ElementFlags)(elementInfos[startLength].flags & elementInfos[startLength + 1].flags) & ElementFlagsVariadic) != 0) {
                        var targetInfo = getInferenceInfoForType(ref (n).DerefOrNull(), elementTypes[startLength]);
                        if (targetInfo != nil && targetInfo.impliedArity >= 0) {
                            Ꮡc.inferFromTypes(n, Ꮡc.sliceTupleType(Ꮡsource, startLength, endLength + sourceArity - targetInfo.impliedArity), elementTypes[startLength]);
                            Ꮡc.inferFromTypes(n, Ꮡc.sliceTupleType(Ꮡsource, startLength + targetInfo.impliedArity, endLength), elementTypes[startLength + 1]);
                        }
                    } else if ((ElementFlags)(elementInfos[startLength].flags & ElementFlagsVariadic) != 0 && (ElementFlags)(elementInfos[startLength + 1].flags & ElementFlagsRest) != 0) {
                        var info = getInferenceInfoForType(ref (n).DerefOrNull(), elementTypes[startLength]); if (info != nil) {
                            var constraint = Ꮡc.getBaseConstraintOfType(info.typeParameter);
                            if (constraint != nil && isTupleType(constraint) && (ElementFlags)(constraint.TargetTupleType().combinedFlags & ElementFlagsVariable) == 0) {
                                nint impliedArity = constraint.TargetTupleType().fixedLength;
                                Ꮡc.inferFromTypes(n, Ꮡc.sliceTupleType(Ꮡsource, startLength, sourceArity - (startLength + impliedArity)), elementTypes[startLength]);
                                var restType = Ꮡc.getElementTypeOfSliceOfTupleType(Ꮡsource, startLength + impliedArity, endLength, false, false); if (restType != nil) {
                                    Ꮡc.inferFromTypes(n, restType, elementTypes[startLength + 1]);
                                }
                            }
                        }
                    } else if ((ElementFlags)(elementInfos[startLength].flags & ElementFlagsRest) != 0 && (ElementFlags)(elementInfos[startLength + 1].flags & ElementFlagsVariadic) != 0) {
                        var info = getInferenceInfoForType(ref (n).DerefOrNull(), elementTypes[startLength + 1]); if (info != nil) {
                            var constraint = Ꮡc.getBaseConstraintOfType(info.typeParameter);
                            if (constraint != nil && isTupleType(constraint) && (ElementFlags)(constraint.TargetTupleType().combinedFlags & ElementFlagsVariable) == 0) {
                                nint impliedArity = constraint.TargetTupleType().fixedLength;
                                nint endIndex = sourceArity - getEndElementCount(ref (target.TargetTupleType()).DerefOrNull(), ElementFlagsFixed);
                                nint startIndex = endIndex - impliedArity;
                                if (startIndex >= startLength) {
                                    var trailingSlice = Ꮡc.createTupleTypeEx(Ꮡc.getTypeArguments(Ꮡsource).slice(startIndex, endIndex), source.TargetTupleType().elementInfos.slice(startIndex, endIndex), false);
                                    var restType = Ꮡc.getElementTypeOfSliceOfTupleType(Ꮡsource, startLength, endLength + impliedArity, false, false); if (restType != nil) {
                                        Ꮡc.inferFromTypes(n, restType, elementTypes[startLength]);
                                    }
                                    Ꮡc.inferFromTypes(n, trailingSlice, elementTypes[startLength + 1]);
                                }
                            }
                        }
                    }
                } else if (middleLength == 1 && (ElementFlags)(elementInfos[startLength].flags & ElementFlagsVariadic) != 0) {
                    var priority = core.IfElse((ElementFlags)(elementInfos[targetArity - 1].flags & ElementFlagsOptional) != 0, InferencePrioritySpeculativeTuple, (InferencePriority)0);
                    var sourceSlice = Ꮡc.sliceTupleType(Ꮡsource, startLength, endLength);
                    Ꮡc.inferWithPriority(n, sourceSlice, elementTypes[startLength], priority);
                } else if (middleLength == 1 && (ElementFlags)(elementInfos[startLength].flags & ElementFlagsRest) != 0) {
                    var restType = Ꮡc.getElementTypeOfSliceOfTupleType(Ꮡsource, startLength, endLength, false, false);
                    if (restType != nil) {
                        Ꮡc.inferFromTypes(n, restType, elementTypes[startLength]);
                    }
                }
            }
            foreach (var i in range(endLength)) {
                Ꮡc.inferFromTypes(n, Ꮡc.getTypeArguments(Ꮡsource)[sourceArity - i - 1], elementTypes[targetArity - i - 1]);
            }
            return;
        }
        if (c.isArrayType(Ꮡtarget)) {
            Ꮡc.inferFromIndexTypes(n, Ꮡsource, Ꮡtarget);
            return;
        }
    }
    Ꮡc.inferFromProperties(n, Ꮡsource, Ꮡtarget);
    Ꮡc.inferFromSignatures(n, Ꮡsource, Ꮡtarget, SignatureKindCall);
    Ꮡc.inferFromSignatures(n, Ꮡsource, Ꮡtarget, SignatureKindConstruct);
    Ꮡc.inferFromIndexTypes(n, Ꮡsource, Ꮡtarget);
}

internal static void inferFromProperties(this ж<Checker> Ꮡc, ж<InferenceState> n, ж<Type> source, ж<Type> target) {
    var properties = Ꮡc.getPropertiesOfObjectType(target);
    foreach (var (_, targetProp) in range(properties)) {
        var sourceProp = Ꮡc.getPropertyOfType(source, targetProp.Name);
        if (sourceProp != nil && !core.Some<ж<ast.Node>>(sourceProp.Declarations, Ꮡc.isSkipDirectInferenceNode)) {
            Ꮡc.inferFromTypes(n, Ꮡc.removeMissingType(Ꮡc.getTypeOfSymbol(sourceProp), (ast.SymbolFlags)(sourceProp.Flags & ast.SymbolFlagsOptional) != 0), Ꮡc.removeMissingType(Ꮡc.getTypeOfSymbol(targetProp), (ast.SymbolFlags)(targetProp.Flags & ast.SymbolFlagsOptional) != 0));
        }
    }
}

internal static void inferFromSignatures(this ж<Checker> Ꮡc, ж<InferenceState> n, ж<Type> source, ж<Type> target, SignatureKind kind) {
    var sourceSignatures = Ꮡc.getSignaturesOfType(source, kind);
    nint sourceLen = len(sourceSignatures);
    if (sourceLen > 0) {
        var targetSignatures = Ꮡc.getSignaturesOfType(target, kind);
        nint targetLen = len(targetSignatures);
        foreach (var i in range(targetLen)) {
            nint sourceIndex = max(sourceLen - targetLen + i, 0);
            Ꮡc.inferFromSignature(n, Ꮡc.getBaseSignature(sourceSignatures[sourceIndex]), Ꮡc.getErasedSignature(targetSignatures[i]));
        }
    }
}

internal static void inferFromSignature(this ж<Checker> Ꮡc, ж<InferenceState> Ꮡn, ж<Signature> Ꮡsource, ж<Signature> Ꮡtarget) {
    ref var n = ref Ꮡn.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((SignatureFlags)(source.flags & SignatureFlagsIsNonInferrable) == 0) {
        var saveBivariant = n.bivariant;
        var kind = ast.KindUnknown;
        if (target.declaration != nil) {
            kind = target.declaration.Kind;
        }
        n.bivariant = n.bivariant || kind == ast.KindMethodDeclaration || kind == ast.KindMethodSignature || kind == ast.KindConstructor;
        Ꮡc.applyToParameterTypes(Ꮡsource, Ꮡtarget, (ж<Type> s, ж<Type> t) => {
            Ꮡc.inferFromContravariantTypesIfStrictFunctionTypes(Ꮡn, s, t);
        });
        n.bivariant = saveBivariant;
    }
    Ꮡc.applyToReturnTypes(Ꮡsource, Ꮡtarget, (ж<Type> s, ж<Type> t) => {
        Ꮡc.inferFromTypes(Ꮡn, s, t);
    });
}

internal static void applyToParameterTypes(this ж<Checker> Ꮡc, ж<Signature> source, ж<Signature> target, Action<ж<Type>, ж<Type>> callback) {
    nint sourceCount = Ꮡc.getParameterCount(source);
    nint targetCount = Ꮡc.getParameterCount(target);
    var sourceRestType = Ꮡc.getEffectiveRestType(source);
    var targetRestType = Ꮡc.getEffectiveRestType(target);
    nint targetNonRestCount = targetCount;
    if (targetRestType != nil) {
        targetNonRestCount--;
    }
    nint paramCount = targetNonRestCount;
    if (sourceRestType == nil) {
        paramCount = min(sourceCount, targetNonRestCount);
    }
    var sourceThisType = Ꮡc.getThisTypeOfSignature(source);
    if (sourceThisType != nil) {
        var targetThisType = Ꮡc.getThisTypeOfSignature(target);
        if (targetThisType != nil) {
            callback(sourceThisType, targetThisType);
        }
    }
    foreach (var i in range(paramCount)) {
        callback(Ꮡc.getTypeAtPosition(source, i), Ꮡc.getTypeAtPosition(target, i));
    }
    if (targetRestType != nil) {
        callback(Ꮡc.getRestTypeAtPosition(source, paramCount, Ꮡc.isConstTypeVariable(targetRestType, 0) && !someType(targetRestType, Ꮡc.isMutableArrayLikeType)), targetRestType);
    }
}

internal static void applyToReturnTypes(this ж<Checker> Ꮡc, ж<Signature> source, ж<Signature> target, Action<ж<Type>, ж<Type>> callback) {
    ref var c = ref Ꮡc.DerefOrNull();

    var targetTypePredicate = Ꮡc.getTypePredicateOfSignature(target);
    if (targetTypePredicate != nil) {
        var sourceTypePredicate = Ꮡc.getTypePredicateOfSignature(source);
        if (sourceTypePredicate != nil && c.typePredicateKindsMatch(sourceTypePredicate, targetTypePredicate) && sourceTypePredicate.t != nil && targetTypePredicate.t != nil) {
            callback(sourceTypePredicate.t, targetTypePredicate.t);
            return;
        }
    }
    var targetReturnType = Ꮡc.getReturnTypeOfSignature(target);
    if (c.couldContainTypeVariables(targetReturnType)) {
        callback(Ꮡc.getReturnTypeOfSignature(source), targetReturnType);
    }
}

internal static void inferFromIndexTypes(this ж<Checker> Ꮡc, ж<InferenceState> n, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    var propTypesᴷ = new stackAppend<ж<Type>>();

    var priority = InferencePriorityNone;
    if ((ObjectFlags)((ObjectFlags)(source.objectFlags & target.objectFlags) & ObjectFlagsMapped) != 0) {
        priority = InferencePriorityHomomorphicMappedType;
    }
    var indexInfos = Ꮡc.getIndexInfosOfType(Ꮡtarget);
    if (Ꮡc.isObjectTypeWithInferableIndex(Ꮡsource)) {
        foreach (var (_, targetInfo) in range(indexInfos)) {
            slice<ж<Type>> propTypes = default;
            foreach (var (_, prop) in range(Ꮡc.getPropertiesOfType(Ꮡsource))) {
                if (Ꮡc.isApplicableIndexType(Ꮡc.getLiteralTypeFromProperty(prop, TypeFlagsStringOrNumberLiteralOrUnique, false), targetInfo.keyType)) {
                    var propType = Ꮡc.getTypeOfSymbol(prop);
                    if ((ast.SymbolFlags)(prop.Flags & ast.SymbolFlagsOptional) != 0) {
                        propType = Ꮡc.removeMissingOrUndefinedType(propType);
                    }
                    propTypes = append(propTypesᴷ, propTypes, propType);
                }
            }
            foreach (var (_, info) in range(Ꮡc.getIndexInfosOfType(Ꮡsource))) {
                if (Ꮡc.isApplicableIndexType(info.keyType, targetInfo.keyType)) {
                    propTypes = append(propTypes, info.valueType);
                }
            }
            if (len(propTypes) != 0) {
                Ꮡc.inferWithPriority(n, Ꮡc.getUnionType(propTypes), targetInfo.valueType, priority);
            }
        }
    }
    foreach (var (_, targetInfo) in range(indexInfos)) {
        var sourceInfo = Ꮡc.getApplicableIndexInfo(Ꮡsource, targetInfo.keyType);
        if (sourceInfo != nil) {
            Ꮡc.inferWithPriority(n, sourceInfo.valueType, targetInfo.valueType, priority);
        }
    }
}

internal static bool inferToMappedType(this ж<Checker> Ꮡc, ж<InferenceState> n, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<Type> ᏑconstraintType) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var constraintType = ref ᏑconstraintType.DerefOrNull();

    if ((TypeFlags)(constraintType.flags & TypeFlagsUnion) != 0 || (TypeFlags)(constraintType.flags & TypeFlagsIntersection) != 0) {
        var result = false;
        foreach (var (_, t) in range(constraintType.Types())) {
            result = core.OrElse(Ꮡc.inferToMappedType(n, Ꮡsource, Ꮡtarget, t), result);
        }
        return result;
    }
    if ((TypeFlags)(constraintType.flags & TypeFlagsIndex) != 0) {
        var inference = getInferenceInfoForType(ref (n).DerefOrNull(), constraintType.AsIndexType().target);
        if (inference != nil && !inference.isFixed && !Ꮡc.isFromInferenceBlockedSource(Ꮡsource)) {
            var inferredType = Ꮡc.inferTypeForHomomorphicMappedType(Ꮡsource, Ꮡtarget, ᏑconstraintType);
            if (inferredType != nil) {
                Ꮡc.inferWithPriority(n, inferredType, inference.typeParameter, core.IfElse((ObjectFlags)(source.objectFlags & ObjectFlagsNonInferrableType) != 0, InferencePriorityPartialHomomorphicMappedType, InferencePriorityHomomorphicMappedType));
            }
        }
        return true;
    }
    if ((TypeFlags)(constraintType.flags & TypeFlagsTypeParameter) != 0) {
        Ꮡc.inferWithPriority(n, Ꮡc.getIndexTypeEx(Ꮡsource, core.IfElse(c.patternForType[Ꮡsource] != nil, IndexFlagsNoIndexSignatures, IndexFlagsNone)), ᏑconstraintType, InferencePriorityMappedTypeConstraint);
        var extendedConstraint = Ꮡc.getConstraintOfType(ᏑconstraintType);
        if (extendedConstraint != nil && Ꮡc.inferToMappedType(n, Ꮡsource, Ꮡtarget, extendedConstraint)) {
            return true;
        }
        var propTypes = core.Map<ж<ast.Symbol>, ж<Type>>(Ꮡc.getPropertiesOfType(Ꮡsource), Ꮡc.getTypeOfSymbol);
        var indexTypes = core.Map(Ꮡc.getIndexInfosOfType(Ꮡsource), ж<Type> (ж<IndexInfo> info) => {
            if (info != Ꮡc.Value.enumNumberIndexInfo) {
                return info.valueType;
            }
            return Ꮡc.Value.neverType;
        });
        Ꮡc.inferFromTypes(n, Ꮡc.getUnionType(core.Concatenate(propTypes, indexTypes)), Ꮡc.getTemplateTypeFromMappedType(Ꮡtarget));
        return true;
    }
    return false;
}

internal static ж<Type> inferTypeForHomomorphicMappedType(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<Type> Ꮡconstraint) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var constraint = ref Ꮡconstraint.DerefOrNull();

    var key = new ReverseMappedTypeKey(sourceId: source.id, targetId: target.id, constraintId: constraint.id);
    var cached = c.reverseHomomorphicMappedCache[key]; if (cached != nil) {
        return cached;
    }
    var t = Ꮡc.createReverseMappedType(Ꮡsource, Ꮡtarget, Ꮡconstraint);
    c.reverseHomomorphicMappedCache[key] = t;
    return t;
}

internal static ж<Type> createReverseMappedType(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> target, ж<Type> constraint) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();

    if (!(Ꮡc.getIndexInfoOfType(Ꮡsource, c.stringType) != nil || len(Ꮡc.getPropertiesOfType(Ꮡsource)) != 0 && Ꮡc.isPartiallyInferableType(Ꮡsource))) {
        return default;
    }
    if (c.isArrayType(Ꮡsource)) {
        var elementType = Ꮡc.inferReverseMappedType(Ꮡc.getTypeArguments(Ꮡsource)[0], target, constraint);
        if (elementType == nil) {
            return default;
        }
        return Ꮡc.createArrayTypeEx(elementType, c.isReadonlyArrayType(Ꮡsource));
    }
    if (isTupleType(Ꮡsource)) {
        var elementTypes = core.Map(Ꮡc.getElementTypes(Ꮡsource), ж<Type> (ж<Type> t) => Ꮡc.inferReverseMappedType(t, target, constraint));
        if (!core.Every(elementTypes, (ж<Type> t) => t != nil)) {
            return default;
        }
        var elementInfos = source.TargetTupleType().elementInfos;
        if ((MappedTypeModifiers)(getMappedTypeModifiers(target) & MappedTypeModifiersIncludeOptional) != 0) {
            elementInfos = core.SameMap(elementInfos, TupleElementInfo (TupleElementInfo info) => {
                if ((ElementFlags)(info.flags & ElementFlagsOptional) != 0) {
                    return new TupleElementInfo(flags: ElementFlagsRequired, labeledDeclaration: info.labeledDeclaration);
                }
                return info;
            });
        }
        return Ꮡc.createTupleTypeEx(elementTypes, elementInfos, source.TargetTupleType().@readonly);
    }
    var reversed = Ꮡc.newObjectType((ObjectFlags)(ObjectFlagsReverseMapped | ObjectFlagsAnonymous), nil);
    reversed.AsReverseMappedType().source = Ꮡsource;
    reversed.AsReverseMappedType().mappedType = target;
    reversed.AsReverseMappedType().constraintType = constraint;
    return reversed;
}

internal static bool isPartiallyInferableType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (ObjectFlags)(t.objectFlags & ObjectFlagsNonInferrableType) == 0 || isObjectLiteralType(ref (Ꮡt).DerefOrNull()) && core.Some(Ꮡc.getPropertiesOfType(Ꮡt), (ж<ast.Symbol> prop) => Ꮡc.isPartiallyInferableType(Ꮡc.getTypeOfSymbol(prop))) || isTupleType(Ꮡt) && core.Some<ж<Type>>(Ꮡc.getElementTypes(Ꮡt), Ꮡc.isPartiallyInferableType);
}

internal static ж<Type> inferReverseMappedType(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget, ж<Type> Ꮡconstraint) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var constraint = ref Ꮡconstraint.DerefOrNull();

    var key = new ReverseMappedTypeKey(sourceId: source.id, targetId: target.id, constraintId: constraint.id);
    var (cached, ok) = c.reverseMappedCache[key, ꟷ]; if (ok) {
        return core.OrElse(cached, c.unknownType);
    }
    c.reverseMappedSourceStack = append(c.reverseMappedSourceStack, Ꮡsource);
    c.reverseMappedTargetStack = append(c.reverseMappedTargetStack, Ꮡtarget);
    var saveExpandingFlags = c.reverseExpandingFlags;
    if (c.isDeeplyNestedType(Ꮡsource, c.reverseMappedSourceStack, 2)) {
        c.reverseExpandingFlags |= (ExpandingFlags)(ExpandingFlagsSource);
    }
    if (c.isDeeplyNestedType(Ꮡtarget, c.reverseMappedTargetStack, 2)) {
        c.reverseExpandingFlags |= (ExpandingFlags)(ExpandingFlagsTarget);
    }
    ж<Type> t = default;
    if (c.reverseExpandingFlags != ExpandingFlagsBoth) {
        t = Ꮡc.inferReverseMappedTypeWorker(Ꮡsource, Ꮡtarget, Ꮡconstraint);
    }
    c.reverseMappedSourceStack = c.reverseMappedSourceStack.slice(0, len(c.reverseMappedSourceStack) - 1);
    c.reverseMappedTargetStack = c.reverseMappedTargetStack.slice(0, len(c.reverseMappedTargetStack) - 1);
    c.reverseExpandingFlags = saveExpandingFlags;
    c.reverseMappedCache[key] = t;
    return t;
}

internal static ж<Type> inferReverseMappedTypeWorker(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> Ꮡtarget, ж<Type> Ꮡconstraint) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();
    ref var constraint = ref Ꮡconstraint.DerefOrNull();

    var typeParameter = Ꮡc.getIndexedAccessType(constraint.AsIndexType().target, Ꮡc.getTypeParameterFromMappedType(Ꮡtarget));
    var templateType = Ꮡc.getTemplateTypeFromMappedType(Ꮡtarget);
    var inference = newInferenceInfo(typeParameter);
    Ꮡc.inferTypes([inference], source, templateType, InferencePriorityNone, false);
    return Ꮡc.getWidenedType(core.OrElse(Ꮡc.getTypeFromInference(inference), c.unknownType));
}

internal static void resolveReverseMappedTypeMembers(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    var r = t.AsReverseMappedType();
    var indexInfo = Ꮡc.getIndexInfoOfType(r.source, c.stringType);
    var modifiers = getMappedTypeModifiers(r.mappedType);
    var readonlyMask = (MappedTypeModifiers)(modifiers & MappedTypeModifiersIncludeReadonly) == 0;
    var optionalMask = core.IfElse((MappedTypeModifiers)(modifiers & MappedTypeModifiersIncludeOptional) != 0, (ast.SymbolFlags)0, ast.SymbolFlagsOptional);
    slice<ж<IndexInfo>> indexInfos = default;
    if (indexInfo != nil) {
        indexInfos = slice<ж<IndexInfo>>([c.newIndexInfo(c.stringType, core.OrElse(Ꮡc.inferReverseMappedType(indexInfo.valueType, r.mappedType, r.constraintType), c.unknownType), readonlyMask && indexInfo.isReadonly, nil, default)]);
    }
    var members = new ast.SymbolTable(0);
    var limitedConstraint = Ꮡc.getLimitedConstraint(Ꮡt);
    foreach (var (_, prop) in range(Ꮡc.getPropertiesOfType(r.source))) {
        if (limitedConstraint != nil) {
            var propertyNameType = Ꮡc.getLiteralTypeFromProperty(prop, TypeFlagsStringOrNumberLiteralOrUnique, false);
            if (!Ꮡc.isTypeAssignableTo(propertyNameType, limitedConstraint)) {
                continue;
            }
        }
        var checkFlags = (ast.CheckFlags)(ast.CheckFlagsReverseMapped | core.IfElse(readonlyMask && Ꮡc.isReadonlySymbol(prop), ast.CheckFlagsReadonly, (ast.CheckFlags)0));
        var inferredProp = c.newSymbolEx((ast.SymbolFlags)(ast.SymbolFlagsProperty | (ast.SymbolFlags)(prop.Flags & optionalMask)), prop.Name, checkFlags);
        inferredProp.Declarations = prop.Declarations;
        c.valueSymbolLinks.Get(inferredProp).nameType = c.valueSymbolLinks.Get(prop).nameType;
        var links = c.ReverseMappedSymbolLinks.Get(inferredProp);
        links.propertyType = Ꮡc.getTypeOfSymbol(prop);
        var constraintTarget = r.constraintType.AsIndexType().target;
        if ((TypeFlags)(constraintTarget.flags & TypeFlagsIndexedAccess) != 0 && (TypeFlags)(constraintTarget.AsIndexedAccessType().objectType.flags & TypeFlagsTypeParameter) != 0 && (TypeFlags)(constraintTarget.AsIndexedAccessType().indexType.flags & TypeFlagsTypeParameter) != 0) {
            var newTypeParam = constraintTarget.AsIndexedAccessType().objectType;
            var newMappedType = Ꮡc.replaceIndexedAccess(r.mappedType, constraintTarget, newTypeParam);
            links.mappedType = newMappedType;
            links.constraintType = Ꮡc.getIndexType(newTypeParam);
        } else {
            links.mappedType = r.mappedType;
            links.constraintType = r.constraintType;
        }
        members[prop.Name] = inferredProp;
    }
    Ꮡc.setStructuredTypeMembers(Ꮡt, members, default, default, indexInfos);
}

internal static ж<Type> getTypeOfReverseMappedSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    ref var c = ref Ꮡc.DerefOrNull();

    var links = c.valueSymbolLinks.Get(symbol);
    if (links.resolvedType == nil) {
        var reverseLinks = c.ReverseMappedSymbolLinks.Get(symbol);
        links.resolvedType = core.OrElse(Ꮡc.inferReverseMappedType(reverseLinks.propertyType, reverseLinks.mappedType, reverseLinks.constraintType), c.unknownType);
    }
    return links.resolvedType;
}

internal static ж<Type> getLimitedConstraint(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    var constraint = Ꮡc.getConstraintTypeFromMappedType(t.AsReverseMappedType().mappedType);
    if (!((TypeFlags)(constraint.flags & TypeFlagsUnion) != 0 || (TypeFlags)(constraint.flags & TypeFlagsIntersection) != 0)) {
        return default;
    }
    var origin = constraint;
    if ((TypeFlags)(constraint.flags & TypeFlagsUnion) != 0) {
        origin = constraint.AsUnionType().origin;
    }
    if (origin == nil || (TypeFlags)(origin.flags & TypeFlagsIntersection) == 0) {
        return default;
    }
    var constraintType = t.AsReverseMappedType().constraintType;
    var constraintTypeʗ1 = constraintType;
    var limitedConstraint = Ꮡc.getIntersectionType(core.Filter(origin.Types(), (ж<Type> tΔ1) => tΔ1 != constraintTypeʗ1));
    if (limitedConstraint != c.neverType) {
        return limitedConstraint;
    }
    return default;
}

internal static ж<Type> replaceIndexedAccess(this ж<Checker> Ꮡc, ж<Type> instantiable, ж<Type> Ꮡt, ж<Type> replacement) {
    ref var t = ref Ꮡt.DerefOrNull();

    return Ꮡc.instantiateType(instantiable, newTypeMapper(slice<ж<Type>>([t.AsIndexedAccessType().indexType, t.AsIndexedAccessType().objectType]), slice<ж<Type>>([Ꮡc.getNumberLiteralType(0D), Ꮡc.createTupleType([replacement])])));
}

internal static bool typesDefinitelyUnrelated(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    if (isTupleType(source) && isTupleType(target)) {
        return tupleTypesDefinitelyUnrelated(source, target);
    }
    return Ꮡc.getUnmatchedProperty(source, target, false, true) != nil && Ꮡc.getUnmatchedProperty(target, source, false, false) != nil;
}

} partial class checker_package {

internal static bool tupleTypesDefinitelyUnrelated(ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var s = source.TargetTupleType();
    var t = target.TargetTupleType();
    return (ElementFlags)(t.combinedFlags & ElementFlagsVariadic) == 0 && t.minLength > s.minLength || (ElementFlags)(t.combinedFlags & ElementFlagsVariable) == 0 && ((ElementFlags)(s.combinedFlags & ElementFlagsVariable) != 0 || t.fixedLength < s.fixedLength);
}

} partial class checkerᴛChecker {

internal static bool isTupleTypeStructureMatching(this ref Checker c, ж<Type> Ꮡt1, ж<Type> Ꮡt2) {
    ref var t1 = ref Ꮡt1.DerefOrNull();
    ref var t2 = ref Ꮡt2.DerefOrNull();

    if (c.getTypeReferenceArity(Ꮡt1) != c.getTypeReferenceArity(Ꮡt2)) {
        return false;
    }
    foreach (var (i, e) in range(t1.TargetTupleType().elementInfos)) {
        if ((ElementFlags)(e.flags & ElementFlagsVariable) != (ElementFlags)(t2.TargetTupleType().elementInfos[i].flags & ElementFlagsVariable)) {
            return false;
        }
    }
    return true;
}

internal static bool isTypeOrBaseIdenticalTo(this ж<Checker> Ꮡc, ж<Type> Ꮡs, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var s = ref Ꮡs.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡt == c.missingType) {
        return Ꮡs == Ꮡt;
    }
    return Ꮡc.isTypeIdenticalTo(Ꮡs, Ꮡt) || (TypeFlags)(t.flags & TypeFlagsString) != 0 && (TypeFlags)(s.flags & TypeFlagsStringLiteral) != 0 || (TypeFlags)(t.flags & TypeFlagsNumber) != 0 && (TypeFlags)(s.flags & TypeFlagsNumberLiteral) != 0;
}

internal static bool isTypeCloselyMatchedBy(this ref Checker c, ж<Type> Ꮡs, ж<Type> Ꮡt) {
    ref var s = ref Ꮡs.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    return (TypeFlags)(s.flags & TypeFlagsObject) != 0 && (TypeFlags)(t.flags & TypeFlagsObject) != 0 && s.symbol != nil && s.symbol == t.symbol || s.alias != nil && t.alias != nil && len(s.alias.typeArguments) != 0 && s.alias.symbol == t.alias.symbol;
}

internal static ж<Type> createEmptyObjectTypeFromStringLiteral(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    var members = new ast.SymbolTable(0);
    foreach (var (_, tΔ1) in range(Ꮡt.Distributed())) {
        if ((TypeFlags)(tΔ1.flags & TypeFlagsStringLiteral) == 0) {
            continue;
        }
        @string name = getStringLiteralValue(tΔ1);
        var literalProp = c.newSymbol(ast.SymbolFlagsProperty, name);
        c.valueSymbolLinks.Get(literalProp).resolvedType = c.anyType;
        if (tΔ1.symbol != nil) {
            literalProp.Declarations = tΔ1.symbol.Declarations;
            literalProp.ValueDeclaration = tΔ1.symbol.ValueDeclaration;
        }
        members[name] = literalProp;
    }
    slice<ж<IndexInfo>> indexInfos = default;
    if ((TypeFlags)(t.flags & TypeFlagsString) != 0) {
        indexInfos = slice<ж<IndexInfo>>([c.newIndexInfo(c.stringType, c.emptyObjectType, false, nil, default)]);
    }
    return Ꮡc.newAnonymousType(nil, members, default, default, indexInfos);
}

internal static ж<InferenceContext> newInferenceContext(this ж<Checker> Ꮡc, slice<ж<Type>> typeParameters, ж<Signature> signature, InferenceFlags flags, Func<ж<Type>, ж<Type>, bool, Ternary> compareTypes) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (compareTypes == nil) {
        compareTypes = c.compareTypesAssignable;
    }
    return Ꮡc.newInferenceContextWorker(core.Map<ж<Type>, ж<InferenceInfo>>(typeParameters, newInferenceInfo), signature, flags, compareTypes);
}

internal static ж<InferenceContext> cloneInferenceContext(this ж<Checker> Ꮡc, ж<InferenceContext> Ꮡn, InferenceFlags extraFlags) {
    ref var n = ref Ꮡn.DerefOrNull();

    if (Ꮡn == nil) {
        return default;
    }
    return Ꮡc.newInferenceContextWorker(core.Map<ж<InferenceInfo>, ж<InferenceInfo>>(n.inferences, cloneInferenceInfo), n.signature, (InferenceFlags)(n.flags | extraFlags), n.compareTypes);
}

internal static ж<InferenceContext> cloneInferredPartOfContext(this ж<Checker> Ꮡc, ж<InferenceContext> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    var inferences = core.Filter<ж<InferenceInfo>>(n.inferences, hasInferenceCandidates);
    if (len(inferences) == 0) {
        return default;
    }
    return Ꮡc.newInferenceContextWorker(core.Map<ж<InferenceInfo>, ж<InferenceInfo>>(inferences, cloneInferenceInfo), n.signature, n.flags, n.compareTypes);
}

internal static ж<InferenceContext> newInferenceContextWorker(this ж<Checker> Ꮡc, slice<ж<InferenceInfo>> inferences, ж<Signature> signature, InferenceFlags flags, Func<ж<Type>, ж<Type>, bool, Ternary> compareTypes) {
    var n = Ꮡ(new InferenceContext(
        inferences: inferences,
        signature: signature,
        flags: flags,
        compareTypes: compareTypes
    ));
    n.mapper = Ꮡc.newInferenceTypeMapper(n, true);
    n.nonFixingMapper = Ꮡc.newInferenceTypeMapper(n, false);
    return n;
}

internal static void addIntraExpressionInferenceSite(this ref Checker c, ж<InferenceContext> Ꮡn, ж<ast.Node> node, ж<Type> t) {
    ref var n = ref Ꮡn.DerefOrNull();

    n.intraExpressionInferenceSites = append(n.intraExpressionInferenceSites, new IntraExpressionInferenceSite(node: node, t: t));
}

internal static void inferFromIntraExpressionSites(this ж<Checker> Ꮡc, ж<InferenceContext> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    foreach (var (_, site) in range(n.intraExpressionInferenceSites)) {
        ж<Type> contextualType = default;
        if (ast.IsMethodDeclaration(site.node)) {
            contextualType = Ꮡc.getContextualTypeForObjectLiteralMethod(site.node, ContextFlagsNoConstraints);
        } else {
            contextualType = Ꮡc.getContextualType(site.node, ContextFlagsNoConstraints);
        }
        if (contextualType != nil) {
            Ꮡc.inferTypes(n.inferences, site.t, contextualType, InferencePriorityNone, false);
        }
    }
    n.intraExpressionInferenceSites = default;
}

internal static ж<Type> getInferredType(this ж<Checker> Ꮡc, ж<InferenceContext> Ꮡn, nint index) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var n = ref Ꮡn.DerefOrNull();

    var inference = n.inferences[index];
    if (inference.inferredType == nil) {
        if (inference.typeParameter == c.errorType) {
            return inference.typeParameter;
        }
        ж<Type> inferredType = default;
        ж<Type> fallbackType = default;
        if (n.signature != nil) {
            ж<Type> inferredCovariantType = default;
            if (len(inference.candidates) != 0) {
                inferredCovariantType = Ꮡc.getCovariantInference(inference, n.signature);
            }
            ж<Type> inferredContravariantType = default;
            if (len(inference.contraCandidates) != 0) {
                inferredContravariantType = Ꮡc.getContravariantInference(inference);
            }
            if (inferredCovariantType != nil || inferredContravariantType != nil) {
                var inferredCovariantTypeʗ1 = inferredCovariantType;

                var inferenceʗ1 = inference;
                var inferredCovariantTypeʗ2 = inferredCovariantType;
                var preferCovariantType = inferredCovariantType != nil && (inferredContravariantType == nil || (TypeFlags)(inferredCovariantType.flags & ((TypeFlags)(TypeFlagsNever | TypeFlagsAny))) == 0 && core.Some(inference.contraCandidates, (ж<Type> t) => Ꮡc.isTypeAssignableTo(inferredCovariantTypeʗ1, t)) && core.Every(n.inferences, (ж<InferenceInfo> other) => {
                    var inferredCovariantTypeʗ3 = inferredCovariantTypeʗ2;
                    return other != inferenceʗ1 && Ꮡc.getConstraintOfTypeParameter(other.typeParameter) != inferenceʗ1.typeParameter || core.Every(other.candidates, (ж<Type> t) => Ꮡc.isTypeAssignableTo(t, inferredCovariantTypeʗ3));
                }));
                if (preferCovariantType) {
                    inferredType = inferredCovariantType;
                    fallbackType = inferredContravariantType;
                } else {
                    inferredType = inferredContravariantType;
                    fallbackType = inferredCovariantType;
                }
            } else if ((InferenceFlags)(n.flags & InferenceFlagsNoDefault) != 0) {
                inferredType = c.silentNeverType;
            } else {
                var defaultType = Ꮡc.getDefaultFromTypeParameter(inference.typeParameter);
                if (defaultType != nil) {
                    inferredType = Ꮡc.instantiateType(defaultType, mergeTypeMappers(c.newBackreferenceMapper(Ꮡn, index), n.nonFixingMapper));
                }
            }
        } else {
            inferredType = Ꮡc.getTypeFromInference(inference);
        }
        inference.inferredType = inferredType;
        if (inference.inferredType == nil) {
            inference.inferredType = core.IfElse((InferenceFlags)(n.flags & InferenceFlagsAnyDefault) != 0, c.anyType, c.unknownType);
        }
        var constraint = Ꮡc.getConstraintOfTypeParameter(inference.typeParameter);
        if (constraint != nil) {
            var instantiatedConstraint = Ꮡc.instantiateType(constraint, n.nonFixingMapper);
            if (inferredType != nil) {
                var constraintWithThis = Ꮡc.getTypeWithThisArgument(instantiatedConstraint, inferredType, false);
                if (n.compareTypes(inferredType, constraintWithThis, false) == TernaryFalse) {
                    ж<Type> filteredByConstraint = default;
                    if (inference.priority == InferencePriorityReturnType) {
                        var constraintWithThisʗ1 = constraintWithThis;
                        filteredByConstraint = Ꮡc.mapType(inferredType, (ж<Type> t) => core.IfElse(Ꮡn.Value.compareTypes(t, constraintWithThisʗ1, false) != TernaryFalse, t, Ꮡc.Value.neverType));
                    }
                    inferredType = core.IfElse(filteredByConstraint != nil && (TypeFlags)(filteredByConstraint.flags & TypeFlagsNever) == 0, filteredByConstraint, (ж<Type>)(nil));
                }
            }
            if (inferredType == nil) {
                inferredType = core.IfElse(fallbackType != nil && n.compareTypes(fallbackType, Ꮡc.getTypeWithThisArgument(instantiatedConstraint, fallbackType, false), false) != TernaryFalse, fallbackType, instantiatedConstraint);
            }
            inference.inferredType = inferredType;
        }
        c.clearActiveMapperCaches();
    }
    return inference.inferredType;
}

internal static slice<ж<Type>> getInferredTypes(this ж<Checker> Ꮡc, ж<InferenceContext> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    var result = new slice<ж<Type>>(len(n.inferences));
    foreach (var i in range(len(n.inferences))) {
        result[i] = Ꮡc.getInferredType(Ꮡn, i);
    }
    return result;
}

internal static ж<TypeMapper> getMapperFromContext(this ref Checker c, ж<InferenceContext> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    if (Ꮡn == nil) {
        return default;
    }
    return n.mapper;
}

internal static ж<TypeMapper> createOuterReturnMapper(this ж<Checker> Ꮡc, ж<InferenceContext> Ꮡcontext) {
    ref var context = ref Ꮡcontext.DerefOrNull();

    if (context.outerReturnMapper == nil) {
        var mapper = Ꮡc.cloneInferenceContext(Ꮡcontext, InferenceFlagsNone).mapper;
        if (context.returnMapper != nil) {
            mapper = newMergedTypeMapper(context.returnMapper, mapper);
        }
        context.outerReturnMapper = mapper;
    }
    return context.outerReturnMapper;
}

internal static ж<Type> getCovariantInference(this ж<Checker> Ꮡc, ж<InferenceInfo> Ꮡinference, ж<Signature> signature) {
    ref var inference = ref Ꮡinference.DerefOrNull();

    var candidates = Ꮡc.unionObjectAndArrayLiteralCandidates(inference.candidates);
    var primitiveConstraint = Ꮡc.hasPrimitiveConstraint(inference.typeParameter) || Ꮡc.isConstTypeVariable(inference.typeParameter, 0);
    var widenLiteralTypes = !primitiveConstraint && inference.topLevel && (inference.isFixed || !Ꮡc.isTypeParameterAtTopLevelInReturnType(signature, inference.typeParameter));
    slice<ж<Type>> baseCandidates = default;
    switch (ᐧ) {
    case {} when primitiveConstraint: {
        baseCandidates = core.SameMap<ж<Type>>(candidates, Ꮡc.getRegularTypeOfLiteralType);
        break;
    }
    case {} when widenLiteralTypes: {
        baseCandidates = core.SameMap<ж<Type>>(candidates, Ꮡc.getWidenedLiteralType);
        break;
    }
    default: {
        baseCandidates = candidates;
        break;
    }}

    ж<Type> unwidenedType = default;
    if ((InferencePriority)(inference.priority & InferencePriorityPriorityImpliesCombination) != 0) {
        unwidenedType = Ꮡc.getUnionTypeEx(baseCandidates, UnionReductionSubtype, nil, nil);
    } else {
        unwidenedType = Ꮡc.getCommonSupertype(baseCandidates);
    }
    return Ꮡc.getWidenedType(unwidenedType);
}

internal static ж<Type> getContravariantInference(this ж<Checker> Ꮡc, ж<InferenceInfo> Ꮡinference) {
    ref var inference = ref Ꮡinference.DerefOrNull();

    if ((InferencePriority)(inference.priority & InferencePriorityPriorityImpliesCombination) != 0) {
        return Ꮡc.getIntersectionType(inference.contraCandidates);
    }
    return Ꮡc.getCommonSubtype(inference.contraCandidates);
}

internal static slice<ж<Type>> unionObjectAndArrayLiteralCandidates(this ж<Checker> Ꮡc, slice<ж<Type>> candidates) {
    if (len(candidates) > 1) {
        var objectLiterals = core.Filter<ж<Type>>(candidates, isObjectOrArrayLiteralType);
        if (len(objectLiterals) != 0) {
            var literalsType = Ꮡc.getUnionTypeEx(objectLiterals, UnionReductionSubtype, nil, nil);
            var nonLiteralTypes = core.Filter(candidates, (ж<Type> t) => !isObjectOrArrayLiteralType(t));
            return core.Concatenate(nonLiteralTypes, slice<ж<Type>>([literalsType]));
        }
    }
    return candidates;
}

internal static bool hasPrimitiveConstraint(this ж<Checker> Ꮡc, ж<Type> t) {
    ref var c = ref Ꮡc.DerefOrNull();

    var constraint = Ꮡc.getConstraintOfTypeParameter(t);
    if (constraint != nil) {
        if ((TypeFlags)(constraint.flags & TypeFlagsConditional) != 0) {
            constraint = Ꮡc.getDefaultConstraintOfConditionalType(constraint);
        }
        return c.maybeTypeOfKind(constraint, (TypeFlags)((TypeFlags)((TypeFlags)(TypeFlagsPrimitive | TypeFlagsIndex) | TypeFlagsTemplateLiteral) | TypeFlagsStringMapping));
    }
    return false;
}

internal static bool isTypeParameterAtTopLevel(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ж<Type> tp, nint depth) {
    ref var t = ref Ꮡt.DerefOrNull();

    return Ꮡt == tp || (TypeFlags)(t.flags & TypeFlagsUnionOrIntersection) != 0 && core.Some(t.Types(), (ж<Type> tΔ1) => Ꮡc.isTypeParameterAtTopLevel(tΔ1, tp, depth)) || depth < 3 && (TypeFlags)(t.flags & TypeFlagsConditional) != 0 && (Ꮡc.isTypeParameterAtTopLevel(Ꮡc.getTrueTypeFromConditionalType(Ꮡt), tp, depth + 1) || Ꮡc.isTypeParameterAtTopLevel(Ꮡc.getFalseTypeFromConditionalType(Ꮡt), tp, depth + 1));
}

internal static bool isTypeParameterAtTopLevelInReturnType(this ж<Checker> Ꮡc, ж<Signature> signature, ж<Type> typeParameter) {
    var typePredicate = Ꮡc.getTypePredicateOfSignature(signature);
    if (typePredicate != nil) {
        return typePredicate.t != nil && Ꮡc.isTypeParameterAtTopLevel(typePredicate.t, typeParameter, 0);
    }
    return Ꮡc.isTypeParameterAtTopLevel(Ꮡc.getReturnTypeOfSignature(signature), typeParameter, 0);
}

internal static ж<Type> getTypeFromInference(this ж<Checker> Ꮡc, ж<InferenceInfo> Ꮡinference) {
    ref var inference = ref Ꮡinference.DerefOrNull();

    switch (ᐧ) {
    case {} when len(inference.candidates) is not 0: {
        return Ꮡc.getUnionTypeEx(inference.candidates, UnionReductionSubtype, nil, nil);
    }
    case {} when len(inference.contraCandidates) is not 0: {
        return Ꮡc.getIntersectionType(inference.contraCandidates);
    }}

    return default;
}

} partial class checker_package {

internal static ж<InferenceInfo> getInferenceInfoForType(ref InferenceState n, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsTypeVariable) != 0) {
        Ꮡt = getNonDistributedTypeParameter(Ꮡt); t = ref Ꮡt.DerefOrNull();
        foreach (var (_, inference) in range(n.inferences)) {
            if (Ꮡt == inference.typeParameter) {
                return inference;
            }
        }
    }
    return default;
}

} partial class checkerᴛChecker {

internal static ж<Type> getCommonSupertype(this ж<Checker> Ꮡc, slice<ж<Type>> types) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (len(types) == 1) {
        return types[0];
    }
    var primaryTypes = types;
    if (c.strictNullChecks) {
        primaryTypes = core.SameMap(types, ж<Type> (ж<Type> t) => Ꮡc.filterType(t, (ж<Type> u) => (TypeFlags)(u.flags & TypeFlagsNullable) == 0));
    }
    ж<Type> supertype = default;
    if (Ꮡc.literalTypesWithSameBaseType(primaryTypes)) {
        supertype = Ꮡc.getUnionType(primaryTypes);
    } else {
        supertype = Ꮡc.getSingleCommonSupertype(primaryTypes);
    }
    if (core.Same(primaryTypes, types)) {
        return supertype;
    }
    return Ꮡc.getNullableType(supertype, (TypeFlags)(c.getCombinedTypeFlags(types) & TypeFlagsNullable));
}

internal static ж<Type> getSingleCommonSupertype(this ж<Checker> Ꮡc, slice<ж<Type>> types) {
    var candidate = Ꮡc.findLeftmostType(types, ((Func<ж<Checker>, ж<Type>, ж<Type>, bool>)(checkerᴛChecker.isTypeStrictSubtypeOf)));
    var candidateʗ1 = candidate;
    if (core.Every(types, (ж<Type> t) => t == candidateʗ1 || Ꮡc.isTypeStrictSubtypeOf(t, candidateʗ1))) {
        return candidate;
    }
    return Ꮡc.findLeftmostType(types, ((Func<ж<Checker>, ж<Type>, ж<Type>, bool>)(checkerᴛChecker.isTypeSubtypeOf)));
}

internal static ж<Type> findLeftmostType(this ж<Checker> Ꮡc, slice<ж<Type>> types, Func<ж<Checker>, ж<Type>, ж<Type>, bool> f) {
    ж<Type> candidate = default;
    foreach (var (_, t) in range(types)) {
        if (candidate == nil || f(Ꮡc, candidate, t)) {
            candidate = t;
        }
    }
    return candidate;
}

internal static ж<Type> getCommonSubtype(this ж<Checker> Ꮡc, slice<ж<Type>> types) {
    ж<Type> subtype = default;
    foreach (var (_, t) in range(types)) {
        if (subtype == nil || Ꮡc.isTypeSubtypeOf(t, subtype)) {
            subtype = t;
        }
    }
    return subtype;
}

internal static TypeFlags getCombinedTypeFlags(this ref Checker c, slice<ж<Type>> types) {
    var flags = TypeFlagsNone;
    foreach (var (_, t) in range(types)) {
        if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
            flags |= (TypeFlags)(c.getCombinedTypeFlags(t.Types()));
        } else {
            flags |= (TypeFlags)(t.flags);
        }
    }
    return flags;
}

internal static bool literalTypesWithSameBaseType(this ж<Checker> Ꮡc, slice<ж<Type>> types) {
    ж<Type> commonBaseType = default;
    foreach (var (_, t) in range(types)) {
        if ((TypeFlags)(t.flags & TypeFlagsNever) == 0) {
            var baseType = Ꮡc.getBaseTypeOfLiteralType(t);
            if (commonBaseType == nil) {
                commonBaseType = baseType;
            }
            if (baseType == t || baseType != commonBaseType) {
                return false;
            }
        }
    }
    return true;
}

internal static bool isFromInferenceBlockedSource(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return t.symbol != nil && core.Some<ж<ast.Node>>(t.symbol.Declarations, Ꮡc.isSkipDirectInferenceNode);
}

internal static bool isSkipDirectInferenceNode(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    return Ꮡc.ᏑskipDirectInferenceNodes.Has(node);
}

} partial class checker_package {

internal static ж<InferenceInfo> newInferenceInfo(ж<Type> typeParameter) {
    return Ꮡ(new InferenceInfo(typeParameter: typeParameter, priority: InferencePriorityMaxValue, topLevel: true, impliedArity: -1));
}

internal static ж<InferenceInfo> cloneInferenceInfo(ж<InferenceInfo> Ꮡinfo) {
    ref var info = ref Ꮡinfo.DerefOrNull();

    return Ꮡ(new InferenceInfo(
        typeParameter: info.typeParameter,
        candidates: slices.Clone<slice<ж<Type>>, ж<Type>>(info.candidates),
        contraCandidates: slices.Clone<slice<ж<Type>>, ж<Type>>(info.contraCandidates),
        inferredType: info.inferredType,
        priority: info.priority,
        topLevel: info.topLevel,
        isFixed: info.isFixed,
        impliedArity: info.impliedArity
    ));
}

internal static void clearCachedInferences(slice<ж<InferenceInfo>> inferences) {
    foreach (var (_, inference) in range(inferences)) {
        if (!inference.isFixed) {
            inference.inferredType = default;
        }
    }
}

internal static bool hasInferenceCandidates(ж<InferenceInfo> Ꮡinfo) {
    ref var info = ref Ꮡinfo.DerefOrNull();

    return len(info.candidates) != 0 || len(info.contraCandidates) != 0;
}

internal static bool hasInferenceCandidatesOrDefault(ж<InferenceInfo> Ꮡinfo) {
    ref var info = ref Ꮡinfo.DerefOrNull();

    return hasInferenceCandidates(Ꮡinfo) || hasTypeParameterDefault(ref (info.typeParameter).DerefOrNull());
}

internal static bool hasTypeParameterDefault(ref Type tp) {
    if (tp.symbol != nil) {
        foreach (var (_, d) in range(tp.symbol.Declarations)) {
            if (ast.IsTypeParameterDeclaration(d) && d.AsTypeParameterDeclaration().DefaultType != nil) {
                return true;
            }
        }
    }
    return false;
}

internal static bool hasOverlappingInferences(slice<ж<InferenceInfo>> a, slice<ж<InferenceInfo>> b) {
    foreach (var i in range(len(a))) {
        if (hasInferenceCandidates(a[i]) && hasInferenceCandidates(b[i])) {
            return true;
        }
    }
    return false;
}

} partial class checkerᴛChecker {

internal static void mergeInferences(this ref Checker c, slice<ж<InferenceInfo>> target, slice<ж<InferenceInfo>> source) {
    foreach (var i in range(len(target))) {
        if (!hasInferenceCandidates(target[i]) && hasInferenceCandidates(source[i])) {
            target[i] = source[i];
        }
    }
}

} // end checkerᴛChecker
