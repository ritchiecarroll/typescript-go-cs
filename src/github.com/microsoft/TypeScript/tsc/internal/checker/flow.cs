namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using math = math_package;
using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using xxh3 = go.github.com.zeebo.xxh3_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.zeebo;
using checker = checker_package;
using static checker_package;

partial class checker_package {

partial struct FlowType {
    internal ж<Type> t;
    internal bool incomplete;
}

} partial class checkerᴛFlowType {

internal static bool isNil(this ref FlowType ft) {
    return ft.t == nil;
}

} partial class checkerᴛChecker {

internal static FlowType newFlowType(this ref Checker c, ж<Type> Ꮡt, bool incomplete) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (incomplete && (TypeFlags)(t.flags & TypeFlagsNever) != 0) {
        Ꮡt = c.silentNeverType; t = ref Ꮡt.DerefOrNull();
    }
    return new FlowType(t: Ꮡt, incomplete: incomplete);
}

} partial class checker_package {

partial struct SharedFlow {
    internal ж<ast.FlowNode> flow;
    internal FlowType flowType;
}

partial struct FlowState {
    internal ж<ast.Node> reference;
    internal ж<Type> declaredType;
    internal ж<Type> initialType;
    internal ж<ast.Node> flowContainer;
    internal CacheHashKey refKey;
    internal nint depth;
    internal nint sharedFlowStart;
    internal slice<ж<ast.FlowReduceLabelData>> reduceLabels;
    internal ж<FlowState> next;
}

} partial class checkerᴛChecker {

internal static ж<FlowState> getFlowState(this ref Checker c) {
    var f = c.freeFlowState;
    if (f == nil) {
        f = Ꮡ(new FlowState());
    }
    c.freeFlowState = f.next;
    return f;
}

internal static void putFlowState(this ref Checker c, ж<FlowState> Ꮡf) {
    ref var f = ref Ꮡf.DerefOrNull();

    f = new FlowState(
        reduceLabels: f.reduceLabels[..0],
        next: c.freeFlowState
    );
    c.freeFlowState = Ꮡf;
}

} partial class checker_package {

internal static ж<ast.FlowNode> getFlowNodeOfNode(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var flowNodeData = node.FlowNodeData();
    if (flowNodeData != nil) {
        return flowNodeData.FlowNode;
    }
    return default;
}

} partial class checkerᴛChecker {

internal static ж<Type> getFlowTypeOfReference(this ж<Checker> Ꮡc, ж<ast.Node> reference, ж<Type> declaredType) {
    return Ꮡc.getFlowTypeOfReferenceEx(reference, declaredType, declaredType, nil, nil);
}

internal static ж<Type> getFlowTypeOfReferenceEx(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡreference, ж<Type> ᏑdeclaredType, ж<Type> ᏑinitialType, ж<ast.Node> ᏑflowContainer, ж<ast.FlowNode> ᏑflowNode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var reference = ref Ꮡreference.DerefOrNull();
    ref var declaredType = ref ᏑdeclaredType.DerefOrNull();
    ref var initialType = ref ᏑinitialType.DerefOrNull();
    ref var flowContainer = ref ᏑflowContainer.DerefOrNull();
    ref var flowNode = ref ᏑflowNode.DerefOrNull();

    if (c.flowAnalysisDisabled) {
        return c.errorType;
    }
    if (ᏑflowNode == nil) {
        ᏑflowNode = getFlowNodeOfNode(Ꮡreference); flowNode = ref ᏑflowNode.DerefOrNull();
        if (ᏑflowNode == nil) {
            return ᏑdeclaredType;
        }
    }
    var f = c.getFlowState();
    f.reference = Ꮡreference;
    f.declaredType = ᏑdeclaredType;
    f.initialType = core.Coalesce<Type>(ᏑinitialType, ᏑdeclaredType);
    f.flowContainer = ᏑflowContainer;
    f.sharedFlowStart = len(c.sharedFlows);
    c.flowInvocationCount++;
    var evolvedType = Ꮡc.getTypeAtFlowNode(f, ᏑflowNode).t;
    c.sharedFlows = c.sharedFlows.slice(0, f.sharedFlowStart);
    c.putFlowState(f);
    ж<Type> resultType = default;
    if ((ObjectFlags)(evolvedType.objectFlags & ObjectFlagsEvolvingArray) != 0 && Ꮡc.isEvolvingArrayOperationTarget(Ꮡreference)) {
        resultType = c.autoArrayType;
    } else {
        resultType = Ꮡc.finalizeEvolvingArrayType(evolvedType);
    }
    if (resultType == c.unreachableNeverType || reference.Parent != nil && ast.IsNonNullExpression(reference.Parent) && (TypeFlags)(resultType.flags & TypeFlagsNever) == 0 && (TypeFlags)(Ꮡc.getTypeWithFacts(resultType, TypeFactsNEUndefinedOrNull).flags & TypeFlagsNever) != 0) {
        return ᏑdeclaredType;
    }
    return resultType;
}

internal static FlowType getTypeAtFlowNode(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    if (f.depth == 2000) {
        var tr = c.tracer; if (tr != nil) {
            tr.Instant(tracing.PhaseCheckTypes, "getTypeAtFlowNode_DepthLimit", new map<@string, any>{["depth"] = f.depth});
        }
        c.flowAnalysisDisabled = true;
        Ꮡc.reportFlowControlError(f.reference);
        return new FlowType(t: c.errorType);
    }
    f.depth++;
    ж<ast.FlowNode> sharedFlow = default;
    for (;;) {
        var flags = flow.Flags;
        if ((ast.FlowFlags)(flags & ast.FlowFlagsShared) != 0) {
            for (nint i = f.sharedFlowStart; i < len(c.sharedFlows); i++) {
                if (c.sharedFlows[i].flow == Ꮡflow) {
                    f.depth--;
                    return c.sharedFlows[i].flowType;
                }
            }
            sharedFlow = Ꮡflow;
        }
        FlowType t = default;
        switch (ᐧ) {
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsAssignment) != 0: {
            t = Ꮡc.getTypeAtFlowAssignment(Ꮡf, Ꮡflow);
            if (t.isNil()) {
                Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
                continue;
            }
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsCall) != 0: {
            t = Ꮡc.getTypeAtFlowCall(Ꮡf, Ꮡflow);
            if (t.isNil()) {
                Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
                continue;
            }
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsCondition) != 0: {
            t = Ꮡc.getTypeAtFlowCondition(Ꮡf, Ꮡflow);
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsSwitchClause) != 0: {
            t = Ꮡc.getTypeAtSwitchClause(Ꮡf, Ꮡflow);
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsBranchLabel) != 0: {
            var antecedents = getBranchLabelAntecedents(Ꮡflow, f.reduceLabels);
            if (antecedents.Next == nil) {
                Ꮡflow = antecedents.Flow; flow = ref Ꮡflow.DerefOrNull();
                continue;
            }
            t = Ꮡc.getTypeAtFlowBranchLabel(Ꮡf, Ꮡflow, antecedents);
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsLoopLabel) != 0: {
            if (flow.Antecedents.Next == nil) {
                Ꮡflow = flow.Antecedents.Flow; flow = ref Ꮡflow.DerefOrNull();
                continue;
            }
            t = Ꮡc.getTypeAtFlowLoopLabel(Ꮡf, Ꮡflow);
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsArrayMutation) != 0: {
            t = Ꮡc.getTypeAtFlowArrayMutation(Ꮡf, Ꮡflow);
            if (t.isNil()) {
                Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
                continue;
            }
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsReduceLabel) != 0: {
            f.reduceLabels = append(f.reduceLabels, flow.Node.AsFlowReduceLabelData());
            t = Ꮡc.getTypeAtFlowNode(Ꮡf, flow.Antecedent);
            f.reduceLabels = f.reduceLabels.slice(0, len(f.reduceLabels) - 1);
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsStart) != 0: {
            var container = flow.Node;
            if (container != nil && container != f.flowContainer && !ast.IsPropertyAccessExpression(f.reference) && !ast.IsElementAccessExpression(f.reference) && !(f.reference.Kind == ast.KindThisKeyword && !ast.IsArrowFunction(container))) {
                Ꮡflow = container.FlowNodeData().FlowNode; flow = ref Ꮡflow.DerefOrNull();
                continue;
            }
            t = new FlowType(t: f.initialType);
            break;
        }
        default: {
            t = new FlowType(t: c.convertAutoToAny(f.declaredType));
            break;
        }}

        if (sharedFlow != nil) {
            c.sharedFlows = append(c.sharedFlows, new SharedFlow(flow: sharedFlow, flowType: t));
        }
        f.depth--;
        return t;
    }
}

} partial class checker_package {

internal static ж<ast.FlowList> getBranchLabelAntecedents(ж<ast.FlowNode> Ꮡflow, slice<ж<ast.FlowReduceLabelData>> reduceLabels) {
    ref var flow = ref Ꮡflow.DerefOrNull();

    nint i = len(reduceLabels);
    while (i != 0) {
        i--;
        var data = reduceLabels[i];
        if (data.Target == Ꮡflow) {
            return data.Antecedents;
        }
    }
    return flow.Antecedents;
}

} partial class checkerᴛChecker {

internal static FlowType getTypeAtFlowAssignment(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    var node = flow.Node;
    if (Ꮡc.isMatchingReference(f.reference, node)) {
        if (!Ꮡc.isReachableFlowNode(Ꮡflow)) {
            return new FlowType(t: c.unreachableNeverType);
        }
        if (getAssignmentTargetKind(node) == AssignmentKindCompound) {
            var flowType = Ꮡc.getTypeAtFlowNode(Ꮡf, flow.Antecedent);
            return c.newFlowType(Ꮡc.getBaseTypeOfLiteralType(flowType.t), flowType.incomplete);
        }
        if (f.declaredType == c.autoType || f.declaredType == c.autoArrayType) {
            if (c.isEmptyArrayAssignment(node)) {
                return new FlowType(t: Ꮡc.getEvolvingArrayType(c.neverType));
            }
            var assignedType = Ꮡc.getWidenedLiteralType(Ꮡc.getInitialOrAssignedType(Ꮡf, Ꮡflow));
            if (Ꮡc.isTypeAssignableTo(assignedType, f.declaredType)) {
                return new FlowType(t: assignedType);
            }
            return new FlowType(t: c.anyArrayType);
        }
        var t = f.declaredType;
        if (isInCompoundLikeAssignment(node)) {
            t = Ꮡc.getBaseTypeOfLiteralType(t);
        }
        if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
            return new FlowType(t: Ꮡc.getAssignmentReducedType(t, Ꮡc.getInitialOrAssignedType(Ꮡf, Ꮡflow)));
        }
        return new FlowType(t: t);
    }
    if (Ꮡc.containsMatchingReference(f.reference, node)) {
        if (!Ꮡc.isReachableFlowNode(Ꮡflow)) {
            return new FlowType(t: c.unreachableNeverType);
        }
        if (ast.IsVariableDeclaration(node) && (ast.IsInJSFile(node) || ast.IsVarConstLike(node))) {
            var init = node.Initializer(); if (init != nil && ast.IsFunctionExpressionOrArrowFunction(init)) {
                return Ꮡc.getTypeAtFlowNode(Ꮡf, flow.Antecedent);
            }
        }
        return new FlowType(t: f.declaredType);
    }
    if (ast.IsVariableDeclaration(node) && ast.IsForInStatement(node.Parent.Parent) && (Ꮡc.isMatchingReference(f.reference, node.Parent.Parent.Expression()) || Ꮡc.optionalChainContainsReference(node.Parent.Parent.Expression(), f.reference))) {
        return new FlowType(t: Ꮡc.getNonNullableTypeIfNeeded(Ꮡc.finalizeEvolvingArrayType(Ꮡc.getTypeAtFlowNode(Ꮡf, flow.Antecedent).t)));
    }
    return new FlowType();
}

internal static ж<Type> getInitialOrAssignedType(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    if (ast.IsVariableDeclaration(flow.Node) || ast.IsBindingElement(flow.Node)) {
        return Ꮡc.getNarrowableTypeForReference(Ꮡc.getInitialType(flow.Node), f.reference, CheckModeNormal);
    }
    return Ꮡc.getNarrowableTypeForReference(Ꮡc.getAssignedType(flow.Node), f.reference, CheckModeNormal);
}

internal static bool isEmptyArrayAssignment(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsVariableDeclaration(Ꮡnode) && node.Initializer() != nil && isEmptyArrayLiteral(node.Initializer()) || !ast.IsBindingElement(Ꮡnode) && ast.IsBinaryExpression(node.Parent) && isEmptyArrayLiteral(node.Parent.AsBinaryExpression().Right);
}

internal static FlowType getTypeAtFlowCall(this ж<Checker> Ꮡc, ж<FlowState> f, ж<ast.FlowNode> Ꮡflow) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    var signature = Ꮡc.getEffectsSignature(flow.Node);
    if (signature != nil) {
        var predicate = Ꮡc.getTypePredicateOfSignature(signature);
        if (predicate != nil && (predicate.kind == TypePredicateKindAssertsThis || predicate.kind == TypePredicateKindAssertsIdentifier)) {
            var flowType = Ꮡc.getTypeAtFlowNode(f, flow.Antecedent);
            var t = Ꮡc.finalizeEvolvingArrayType(flowType.t);
            ж<Type> narrowedType = default;
            switch (ᐧ) {
            case {} when predicate.t != nil: {
                narrowedType = Ꮡc.narrowTypeByTypePredicate(f, t, predicate, flow.Node, true);
                break;
            }
            case {} when predicate.kind == TypePredicateKindAssertsIdentifier && predicate.parameterIndex >= 0 && (nint)predicate.parameterIndex < len(flow.Node.Arguments()): {
                narrowedType = Ꮡc.narrowTypeByAssertion(f, t, flow.Node.Arguments()[predicate.parameterIndex]);
                break;
            }
            default: {
                narrowedType = t;
                break;
            }}

            if (narrowedType == t) {
                return flowType;
            }
            return c.newFlowType(narrowedType, flowType.incomplete);
        }
        if ((TypeFlags)(Ꮡc.getReturnTypeOfSignature(signature).flags & TypeFlagsNever) != 0) {
            return new FlowType(t: c.unreachableNeverType);
        }
    }
    return new FlowType();
}

internal static ж<Type> narrowTypeByTypePredicate(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> Ꮡt, ж<TypePredicate> Ꮡpredicate, ж<ast.Node> callExpression, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    ref var predicate = ref Ꮡpredicate.DerefOrNull();

    if (predicate.t != nil && !(IsTypeAny(Ꮡt) && (predicate.t == c.globalObjectType || predicate.t == c.globalFunctionType))) {
        var predicateArgument = c.getTypePredicateArgument(Ꮡpredicate, callExpression);
        if (predicateArgument != nil) {
            if (Ꮡc.isMatchingReference(f.reference, predicateArgument)) {
                return Ꮡc.getNarrowedType(Ꮡt, predicate.t, assumeTrue, false);
            }
            if (c.strictNullChecks && Ꮡc.optionalChainContainsReference(predicateArgument, f.reference) && (assumeTrue && !Ꮡc.hasTypeFacts(predicate.t, TypeFactsEQUndefined) || !assumeTrue && everyType(predicate.t, Ꮡc.IsNullableType))) {
                Ꮡt = Ꮡc.getAdjustedTypeWithFacts(Ꮡt, TypeFactsNEUndefinedOrNull); t = ref Ꮡt.DerefOrNull();
            }
            var access = Ꮡc.getDiscriminantPropertyAccess(Ꮡf, predicateArgument, Ꮡt);
            if (access != nil) {
                return Ꮡc.narrowTypeByDiscriminant(Ꮡt, access, (ж<Type> tΔ1) => Ꮡc.getNarrowedType(tΔ1, Ꮡpredicate.Value.t, assumeTrue, false));
            }
        }
    }
    return Ꮡt;
}

internal static ж<Type> narrowTypeByAssertion(this ж<Checker> Ꮡc, ж<FlowState> f, ж<Type> t, ж<ast.Node> expr) {
    ref var c = ref Ꮡc.DerefOrNull();

    var node = ast.SkipParentheses(expr);
    if (node.Kind == ast.KindFalseKeyword) {
        return c.unreachableNeverType;
    }
    if (node.Kind == ast.KindBinaryExpression) {
        if (node.AsBinaryExpression().OperatorToken.Kind == ast.KindAmpersandAmpersandToken) {
            return Ꮡc.narrowTypeByAssertion(f, Ꮡc.narrowTypeByAssertion(f, t, node.AsBinaryExpression().Left), node.AsBinaryExpression().Right);
        }
        if (node.AsBinaryExpression().OperatorToken.Kind == ast.KindBarBarToken) {
            return Ꮡc.getUnionType(slice<ж<Type>>([Ꮡc.narrowTypeByAssertion(f, t, node.AsBinaryExpression().Left), Ꮡc.narrowTypeByAssertion(f, t, node.AsBinaryExpression().Right)]));
        }
    }
    return Ꮡc.narrowType(f, t, node, true);
}

internal static FlowType getTypeAtFlowCondition(this ж<Checker> Ꮡc, ж<FlowState> f, ж<ast.FlowNode> Ꮡflow) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    var flowType = Ꮡc.getTypeAtFlowNode(f, flow.Antecedent);
    if ((TypeFlags)(flowType.t.flags & TypeFlagsNever) != 0) {
        return flowType;
    }
    var assumeTrue = (ast.FlowFlags)(flow.Flags & ast.FlowFlagsTrueCondition) != 0;
    var nonEvolvingType = Ꮡc.finalizeEvolvingArrayType(flowType.t);
    var narrowedType = Ꮡc.narrowType(f, nonEvolvingType, flow.Node, assumeTrue);
    if (narrowedType == nonEvolvingType) {
        return flowType;
    }
    return c.newFlowType(narrowedType, flowType.incomplete);
}

internal static ж<Type> narrowType(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> t, ж<ast.Node> Ꮡexpr, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var expr = ref Ꮡexpr.DerefOrNull();

    if (ast.IsExpressionOfOptionalChainRoot(Ꮡexpr) || ast.IsBinaryExpression(expr.Parent) && (expr.Parent.AsBinaryExpression().OperatorToken.Kind == ast.KindQuestionQuestionToken || expr.Parent.AsBinaryExpression().OperatorToken.Kind == ast.KindQuestionQuestionEqualsToken) && expr.Parent.AsBinaryExpression().Left == Ꮡexpr) {
        return Ꮡc.narrowTypeByOptionality(Ꮡf, t, Ꮡexpr, assumeTrue);
    }
    var exprᴛ1 = expr.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindIdentifier) { matchᴛ1 = true;
        if (!Ꮡc.isMatchingReference(f.reference, Ꮡexpr) && c.inlineLevel < 5) {
            var symbol = c.getResolvedSymbol(Ꮡexpr);
            if (c.isConstantVariable(symbol)) {
                var declaration = symbol.ValueDeclaration;
                if (declaration != nil && ast.IsVariableDeclaration(declaration) && declaration.Type() == nil && declaration.Initializer() != nil && Ꮡc.isConstantReference(f.reference)) {
                    c.inlineLevel++;
                    var result = Ꮡc.narrowType(Ꮡf, t, declaration.Initializer(), assumeTrue);
                    c.inlineLevel--;
                    return result;
                }
            }
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindThisKeyword || exprᴛ1 == ast.KindSuperKeyword || exprᴛ1 == ast.KindPropertyAccessExpression || exprᴛ1 == ast.KindElementAccessExpression)) { matchᴛ1 = true;
        return Ꮡc.narrowTypeByTruthiness(Ꮡf, t, Ꮡexpr, assumeTrue);
    }
    if (exprᴛ1 == ast.KindCallExpression) { matchᴛ1 = true;
        return Ꮡc.narrowTypeByCallExpression(Ꮡf, t, Ꮡexpr, assumeTrue);
    }
    if (exprᴛ1 == ast.KindParenthesizedExpression || exprᴛ1 == ast.KindNonNullExpression || exprᴛ1 == ast.KindSatisfiesExpression) { matchᴛ1 = true;
        return Ꮡc.narrowType(Ꮡf, t, expr.Expression(), assumeTrue);
    }
    if (exprᴛ1 == ast.KindBinaryExpression) {
        return Ꮡc.narrowTypeByBinaryExpression(Ꮡf, t, expr.AsBinaryExpression(), assumeTrue);
    }
    if (exprᴛ1 == ast.KindPrefixUnaryExpression) { matchᴛ1 = true;
        if (expr.AsPrefixUnaryExpression().Operator == ast.KindExclamationToken) {
            return Ꮡc.narrowType(Ꮡf, t, expr.AsPrefixUnaryExpression().Operand, !assumeTrue);
        }
    }

    return t;
}

internal static ж<Type> narrowTypeByOptionality(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> t, ж<ast.Node> expr, bool assumePresent) {
    ref var f = ref Ꮡf.DerefOrNull();

    if (Ꮡc.isMatchingReference(f.reference, expr)) {
        return Ꮡc.getAdjustedTypeWithFacts(t, core.IfElse(assumePresent, TypeFactsNEUndefinedOrNull, TypeFactsEQUndefinedOrNull));
    }
    var access = Ꮡc.getDiscriminantPropertyAccess(Ꮡf, expr, t);
    if (access != nil) {
        return Ꮡc.narrowTypeByDiscriminant(t, access, (ж<Type> tΔ1) => Ꮡc.getTypeWithFacts(tΔ1, core.IfElse(assumePresent, TypeFactsNEUndefinedOrNull, TypeFactsEQUndefinedOrNull)));
    }
    return t;
}

internal static ж<Type> narrowTypeByTruthiness(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> Ꮡt, ж<ast.Node> expr, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡc.isMatchingReference(f.reference, expr)) {
        return Ꮡc.getAdjustedTypeWithFacts(Ꮡt, core.IfElse(assumeTrue, TypeFactsTruthy, TypeFactsFalsy));
    }
    if (c.strictNullChecks && assumeTrue && Ꮡc.optionalChainContainsReference(expr, f.reference)) {
        Ꮡt = Ꮡc.getAdjustedTypeWithFacts(Ꮡt, TypeFactsNEUndefinedOrNull); t = ref Ꮡt.DerefOrNull();
    }
    var access = Ꮡc.getDiscriminantPropertyAccess(Ꮡf, expr, Ꮡt);
    if (access != nil) {
        return Ꮡc.narrowTypeByDiscriminant(Ꮡt, access, (ж<Type> tΔ1) => Ꮡc.getTypeWithFacts(tΔ1, core.IfElse(assumeTrue, TypeFactsTruthy, TypeFactsFalsy)));
    }
    return Ꮡt;
}

internal static ж<Type> narrowTypeByCallExpression(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> t, ж<ast.Node> ᏑcallExpression, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var callExpression = ref ᏑcallExpression.DerefOrNull();

    if (Ꮡc.hasMatchingArgument(ᏑcallExpression, f.reference)) {
        ж<TypePredicate> predicate = default;
        if (assumeTrue || !isCallChain(ᏑcallExpression)) {
            var signature = Ꮡc.getEffectsSignature(ᏑcallExpression);
            if (signature != nil) {
                predicate = Ꮡc.getTypePredicateOfSignature(signature);
            }
        }
        if (predicate != nil && (predicate.kind == TypePredicateKindThis || predicate.kind == TypePredicateKindIdentifier)) {
            return Ꮡc.narrowTypeByTypePredicate(Ꮡf, t, predicate, ᏑcallExpression, assumeTrue);
        }
    }
    if (c.containsMissingType(t) && ast.IsAccessExpression(f.reference) && ast.IsPropertyAccessExpression(callExpression.Expression())) {
        var callAccess = callExpression.Expression();
        if (Ꮡc.isMatchingReference(f.reference.Expression(), c.getReferenceCandidate(callAccess.Expression())) && ast.IsIdentifier(callAccess.Name()) && callAccess.Name().Text() == "hasOwnProperty"u8 && len(callExpression.Arguments()) == 1) {
            var argument = callExpression.Arguments()[0];
            var (accessedName, ok) = Ꮡc.getAccessedPropertyName(f.reference); if (ok && ast.IsStringLiteralLike(argument) && accessedName == argument.Text()) {
                return Ꮡc.getTypeWithFacts(t, core.IfElse(assumeTrue, TypeFactsNEUndefined, TypeFactsEQUndefined));
            }
        }
    }
    return t;
}

internal static ж<Type> narrowTypeByBinaryExpression(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> Ꮡt, ж<ast.BinaryExpression> Ꮡexpr, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    ref var expr = ref Ꮡexpr.DerefOrNull();

    switch (expr.OperatorToken.Kind) {
    case var ᴛ when ᴛ == ast.KindEqualsToken || ᴛ == ast.KindBarBarEqualsToken || ᴛ == ast.KindAmpersandAmpersandEqualsToken || ᴛ == ast.KindQuestionQuestionEqualsToken: {
        return Ꮡc.narrowTypeByTruthiness(Ꮡf, Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Right, assumeTrue), expr.Left, assumeTrue);
    }
    case var ᴛ when ᴛ == ast.KindEqualsEqualsToken || ᴛ == ast.KindExclamationEqualsToken || ᴛ == ast.KindEqualsEqualsEqualsToken || ᴛ == ast.KindExclamationEqualsEqualsToken: {
        var @operator = expr.OperatorToken.Kind;
        var left = c.getReferenceCandidate(expr.Left);
        var right = c.getReferenceCandidate(expr.Right);
        if (left.Kind == ast.KindTypeOfExpression && ast.IsStringLiteralLike(right)) {
            return Ꮡc.narrowTypeByTypeof(Ꮡf, Ꮡt, left.AsTypeOfExpression(), @operator, right, assumeTrue);
        }
        if (right.Kind == ast.KindTypeOfExpression && ast.IsStringLiteralLike(left)) {
            return Ꮡc.narrowTypeByTypeof(Ꮡf, Ꮡt, right.AsTypeOfExpression(), @operator, left, assumeTrue);
        }
        if (Ꮡc.isMatchingReference(f.reference, left)) {
            return Ꮡc.narrowTypeByEquality(Ꮡt, @operator, right, assumeTrue);
        }
        if (Ꮡc.isMatchingReference(f.reference, right)) {
            return Ꮡc.narrowTypeByEquality(Ꮡt, @operator, left, assumeTrue);
        }
        if (c.strictNullChecks) {
            if (Ꮡc.optionalChainContainsReference(left, f.reference)) {
                Ꮡt = Ꮡc.narrowTypeByOptionalChainContainment(Ꮡf, Ꮡt, @operator, right, assumeTrue); t = ref Ꮡt.DerefOrNull();
            } else if (Ꮡc.optionalChainContainsReference(right, f.reference)) {
                Ꮡt = Ꮡc.narrowTypeByOptionalChainContainment(Ꮡf, Ꮡt, @operator, left, assumeTrue); t = ref Ꮡt.DerefOrNull();
            }
        }
        var leftAccess = Ꮡc.getDiscriminantPropertyAccess(Ꮡf, left, Ꮡt);
        if (leftAccess != nil) {
            return Ꮡc.narrowTypeByDiscriminantProperty(Ꮡt, leftAccess, @operator, right, assumeTrue);
        }
        var rightAccess = Ꮡc.getDiscriminantPropertyAccess(Ꮡf, right, Ꮡt);
        if (rightAccess != nil) {
            return Ꮡc.narrowTypeByDiscriminantProperty(Ꮡt, rightAccess, @operator, left, assumeTrue);
        }
        if (Ꮡc.isMatchingConstructorReference(Ꮡf, left)) {
            return Ꮡc.narrowTypeByConstructor(Ꮡt, @operator, right, assumeTrue);
        }
        if (Ꮡc.isMatchingConstructorReference(Ꮡf, right)) {
            return Ꮡc.narrowTypeByConstructor(Ꮡt, @operator, left, assumeTrue);
        }
        if (ast.IsBooleanLiteral(right) && !ast.IsAccessExpression(left)) {
            return Ꮡc.narrowTypeByBooleanComparison(Ꮡf, Ꮡt, left, right, @operator, assumeTrue);
        }
        if (ast.IsBooleanLiteral(left) && !ast.IsAccessExpression(right)) {
            return Ꮡc.narrowTypeByBooleanComparison(Ꮡf, Ꮡt, right, left, @operator, assumeTrue);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindInstanceOfKeyword: {
        return Ꮡc.narrowTypeByInstanceof(Ꮡf, Ꮡt, Ꮡexpr, assumeTrue);
    }
    case var ᴛ when ᴛ == ast.KindInKeyword: {
        if (ast.IsPrivateIdentifier(expr.Left)) {
            return Ꮡc.narrowTypeByPrivateIdentifierInInExpression(Ꮡf, Ꮡt, Ꮡexpr, assumeTrue);
        }
        var target = c.getReferenceCandidate(expr.Right);
        if (c.containsMissingType(Ꮡt) && ast.IsAccessExpression(f.reference) && Ꮡc.isMatchingReference(f.reference.Expression(), target)) {
            var leftType = Ꮡc.getTypeOfExpression(expr.Left);
            if (isTypeUsableAsPropertyName(ref (leftType).DerefOrNull())) {
                var (accessedName, ok) = Ꮡc.getAccessedPropertyName(f.reference); if (ok && accessedName == getPropertyNameFromType(leftType)) {
                    return Ꮡc.getTypeWithFacts(Ꮡt, core.IfElse(assumeTrue, TypeFactsNEUndefined, TypeFactsEQUndefined));
                }
            }
        }
        if (Ꮡc.isMatchingReference(f.reference, target)) {
            var leftType = Ꮡc.getTypeOfExpression(expr.Left);
            if (isTypeUsableAsPropertyName(ref (leftType).DerefOrNull())) {
                return Ꮡc.narrowTypeByInKeyword(Ꮡf, Ꮡt, leftType, assumeTrue);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindCommaToken: {
        return Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Right, assumeTrue);
    }
    case var ᴛ when ᴛ == ast.KindAmpersandAmpersandToken: {
        if (assumeTrue) {
            return Ꮡc.narrowType(Ꮡf, Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Left, true), expr.Right, true);
        }
        return Ꮡc.getUnionType(slice<ж<Type>>([Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Left, false), Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Right, false)]));
    }
    case var ᴛ when ᴛ == ast.KindBarBarToken: {
        if (assumeTrue) {
            return Ꮡc.getUnionType(slice<ж<Type>>([Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Left, true), Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Right, true)]));
        }
        return Ꮡc.narrowType(Ꮡf, Ꮡc.narrowType(Ꮡf, Ꮡt, expr.Left, false), expr.Right, false);
    }}

    return Ꮡt;
}

internal static ж<Type> narrowTypeByEquality(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ast.Kind @operator, ж<ast.Node> value, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsAny) != 0) {
        return Ꮡt;
    }
    if (@operator == ast.KindExclamationEqualsToken || @operator == ast.KindExclamationEqualsEqualsToken) {
        assumeTrue = !assumeTrue;
    }
    var valueType = Ꮡc.getTypeOfExpression(value);
    var doubleEquals = @operator == ast.KindEqualsEqualsToken || @operator == ast.KindExclamationEqualsToken;
    if ((TypeFlags)(valueType.flags & TypeFlagsNullable) != 0) {
        if (!c.strictNullChecks) {
            return Ꮡt;
        }
        TypeFacts facts = default;
        switch (ᐧ) {
        case {} when doubleEquals: {
            facts = core.IfElse(assumeTrue, TypeFactsEQUndefinedOrNull, TypeFactsNEUndefinedOrNull);
            break;
        }
        case {} when (TypeFlags)(valueType.flags & TypeFlagsNull) != 0: {
            facts = core.IfElse(assumeTrue, TypeFactsEQNull, TypeFactsNENull);
            break;
        }
        default: {
            facts = core.IfElse(assumeTrue, TypeFactsEQUndefined, TypeFactsNEUndefined);
            break;
        }}

        return Ꮡc.getAdjustedTypeWithFacts(Ꮡt, facts);
    }
    if (assumeTrue) {
        if (!doubleEquals && ((TypeFlags)(t.flags & TypeFlagsUnknown) != 0 || someType(Ꮡt, Ꮡc.IsEmptyAnonymousObjectType))) {
            if ((TypeFlags)(valueType.flags & ((TypeFlags)(TypeFlagsPrimitive | TypeFlagsNonPrimitive))) != 0 || Ꮡc.IsEmptyAnonymousObjectType(valueType)) {
                return valueType;
            }
            if ((TypeFlags)(valueType.flags & TypeFlagsObject) != 0) {
                return c.nonPrimitiveType;
            }
        }
        if (!doubleEquals && (TypeFlags)(valueType.flags & TypeFlagsPrimitive) != 0 && Ꮡc.isUniformUnionType(Ꮡt)) {
            var regularType = Ꮡc.getRegularTypeOfLiteralType(valueType);
            if (c.unionContainsType(Ꮡt, regularType, false)) {
                return regularType;
            }
        }
        var valueTypeʗ1 = valueType;
        var filteredType = Ꮡc.filterType(Ꮡt, (ж<Type> tΔ1) => Ꮡc.areTypesComparable(tΔ1, valueTypeʗ1) || doubleEquals && isCoercibleUnderDoubleEquals(ref (tΔ1).DerefOrNull(), ref (valueTypeʗ1).DerefOrNull()));
        return Ꮡc.replacePrimitivesWithLiterals(filteredType, valueType);
    }
    if (isUnitType(valueType)) {
        if (Ꮡc.isUniformUnionType(Ꮡt)) {
            var filteredType = Ꮡc.removeType(Ꮡt, Ꮡc.getRegularTypeOfLiteralType(valueType));
            if (filteredType != Ꮡt) {
                return filteredType;
            }
        }
        var valueTypeʗ2 = valueType;
        return Ꮡc.filterType(Ꮡt, (ж<Type> tΔ2) => !(Ꮡc.isUnitLikeType(tΔ2) && Ꮡc.areTypesComparable(tΔ2, valueTypeʗ2)));
    }
    return Ꮡt;
}

internal static ж<Type> narrowTypeByTypeof(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> Ꮡt, ж<ast.TypeOfExpression> ᏑtypeOfExpr, ast.Kind @operator, ж<ast.Node> Ꮡliteral, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    ref var typeOfExpr = ref ᏑtypeOfExpr.DerefOrNull();
    ref var literal = ref Ꮡliteral.DerefOrNull();

    if (@operator == ast.KindExclamationEqualsToken || @operator == ast.KindExclamationEqualsEqualsToken) {
        assumeTrue = !assumeTrue;
    }
    var target = c.getReferenceCandidate(typeOfExpr.Expression);
    if (!Ꮡc.isMatchingReference(f.reference, target)) {
        if (c.strictNullChecks && Ꮡc.optionalChainContainsReference(target, f.reference) && assumeTrue == (literal.Text() != "undefined"u8)) {
            Ꮡt = Ꮡc.getAdjustedTypeWithFacts(Ꮡt, TypeFactsNEUndefinedOrNull); t = ref Ꮡt.DerefOrNull();
        }
        var propertyAccess = Ꮡc.getDiscriminantPropertyAccess(Ꮡf, target, Ꮡt);
        if (propertyAccess != nil) {
            return Ꮡc.narrowTypeByDiscriminant(Ꮡt, propertyAccess, (ж<Type> tΔ1) => Ꮡc.narrowTypeByLiteralExpression(tΔ1, Ꮡliteral, assumeTrue));
        }
        return Ꮡt;
    }
    return Ꮡc.narrowTypeByLiteralExpression(Ꮡt, Ꮡliteral, assumeTrue);
}

} partial class checker_package {

[GoLazy] partial class lazyᴛtypeofNEFacts { internal static map<@string, TypeFacts> typeofNEFacts = new map<@string, TypeFacts>{
    ["string"] = TypeFactsTypeofNEString,
    ["number"] = TypeFactsTypeofNENumber,
    ["bigint"] = TypeFactsTypeofNEBigInt,
    ["boolean"] = TypeFactsTypeofNEBoolean,
    ["symbol"] = TypeFactsTypeofNESymbol,
    ["undefined"] = TypeFactsNEUndefined,
    ["object"] = TypeFactsTypeofNEObject,
    ["function"] = TypeFactsTypeofNEFunction
}; }

} partial class checkerᴛChecker {

internal static ж<Type> narrowTypeByLiteralExpression(this ж<Checker> Ꮡc, ж<Type> t, ж<astꓸLiteralExpression> Ꮡliteral, bool assumeTrue) {
    ref var literal = ref Ꮡliteral.DerefOrNull();

    if (assumeTrue) {
        return Ꮡc.narrowTypeByTypeName(t, literal.Text());
    }
    var (facts, ok) = typeofNEFacts[literal.Text(), ꟷ];
    if (!ok) {
        facts = TypeFactsTypeofNEHostObject;
    }
    return Ꮡc.getAdjustedTypeWithFacts(t, facts);
}

internal static ж<Type> narrowTypeByTypeName(this ж<Checker> Ꮡc, ж<Type> Ꮡt, @string typeName) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    switch (ᐧ) {
    case {} when typeName == "string"u8: {
        return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.stringType, TypeFactsTypeofEQString);
    }
    case {} when typeName == "number"u8: {
        return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.numberType, TypeFactsTypeofEQNumber);
    }
    case {} when typeName == "bigint"u8: {
        return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.bigintType, TypeFactsTypeofEQBigInt);
    }
    case {} when typeName == "boolean"u8: {
        return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.booleanType, TypeFactsTypeofEQBoolean);
    }
    case {} when typeName == "symbol"u8: {
        return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.esSymbolType, TypeFactsTypeofEQSymbol);
    }
    case {} when typeName == "object"u8: {
        if ((TypeFlags)(t.flags & TypeFlagsAny) != 0) {
            return Ꮡt;
        }
        return Ꮡc.getUnionType(slice<ж<Type>>([Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.nonPrimitiveType, TypeFactsTypeofEQObject), Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.nullType, TypeFactsEQNull)]));
    }
    case {} when typeName == "function"u8: {
        if ((TypeFlags)(t.flags & TypeFlagsAny) != 0) {
            return Ꮡt;
        }
        return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.globalFunctionType, TypeFactsTypeofEQFunction);
    }
    case {} when typeName == "undefined"u8: {
        return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.undefinedType, TypeFactsEQUndefined);
    }}

    return Ꮡc.narrowTypeByTypeFacts(Ꮡt, c.nonPrimitiveType, TypeFactsTypeofEQHostObject);
}

internal static ж<Type> narrowTypeByTypeFacts(this ж<Checker> Ꮡc, ж<Type> t, ж<Type> impliedType, TypeFacts facts) {
    return Ꮡc.mapType(t, (ж<Type> tΔ1) => {
        switch (ᐧ) {
        case {} when Ꮡc.isTypeRelatedTo(tΔ1, impliedType, Ꮡc.Value.strictSubtypeRelation): {
            if (Ꮡc.hasTypeFacts(tΔ1, facts)) {
                return tΔ1;
            }
            return Ꮡc.Value.neverType;
        }
        case {} when Ꮡc.isTypeSubtypeOf(impliedType, tΔ1): {
            return impliedType;
        }
        case {} when Ꮡc.hasTypeFacts(tΔ1, facts): {
            return Ꮡc.getIntersectionType([tΔ1, impliedType]);
        }}

        return Ꮡc.Value.neverType;
    });
}

internal static ж<Type> narrowTypeByDiscriminantProperty(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ж<ast.Node> access, ast.Kind @operator, ж<ast.Node> value, bool assumeTrue) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((@operator == ast.KindEqualsEqualsEqualsToken || @operator == ast.KindExclamationEqualsEqualsToken) && (TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
        @string keyPropertyName = Ꮡc.getKeyPropertyName(Ꮡt);
        if (keyPropertyName != ""u8) {
            var (accessedName, ok) = Ꮡc.getAccessedPropertyName(access); if (ok && keyPropertyName == accessedName) {
                var candidate = Ꮡc.getConstituentTypeForKeyType(Ꮡt, Ꮡc.getTypeOfExpression(value));
                if (candidate != nil) {
                    if (assumeTrue && @operator == ast.KindEqualsEqualsEqualsToken || !assumeTrue && @operator == ast.KindExclamationEqualsEqualsToken) {
                        return candidate;
                    }
                    var propType = Ꮡc.getTypeOfPropertyOfType(candidate, keyPropertyName); if (propType != nil && isUnitType(propType)) {
                        return Ꮡc.removeType(Ꮡt, candidate);
                    }
                    return Ꮡt;
                }
            }
        }
    }
    return Ꮡc.narrowTypeByDiscriminant(Ꮡt, access, (ж<Type> tΔ1) => Ꮡc.narrowTypeByEquality(tΔ1, @operator, value, assumeTrue));
}

internal static ж<Type> narrowTypeByDiscriminant(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> access, Func<ж<Type>, ж<Type>> narrowType) {
    ref var c = ref Ꮡc.DerefOrNull();

    var (propName, ok) = Ꮡc.getAccessedPropertyName(access);
    if (!ok) {
        return t;
    }
    var optionalChain = ast.IsOptionalChain(access);
    var removeNullable = c.strictNullChecks && (optionalChain || isNonNullAccess(access)) && c.maybeTypeOfKind(t, TypeFlagsNullable);
    var nonNullType = t;
    if (removeNullable) {
        nonNullType = Ꮡc.getTypeWithFacts(t, TypeFactsNEUndefinedOrNull);
    }
    var propType = Ꮡc.getTypeOfPropertyOfType(nonNullType, propName);
    if (propType == nil) {
        return t;
    }
    if (removeNullable && optionalChain) {
        propType = Ꮡc.getOptionalType(propType, false);
    }
    var narrowedPropType = narrowType(propType);
    var narrowedPropTypeʗ1 = narrowedPropType;
    return Ꮡc.filterType(t, (ж<Type> tΔ1) => {
        var discriminantType = core.OrElse(Ꮡc.getTypeOfPropertyOrIndexSignatureOfType(tΔ1, propName), Ꮡc.Value.unknownType);
        return (TypeFlags)(discriminantType.flags & TypeFlagsNever) == 0 && (TypeFlags)(narrowedPropTypeʗ1.flags & TypeFlagsNever) == 0 && Ꮡc.areTypesComparable(narrowedPropTypeʗ1, discriminantType);
    });
}

internal static bool isMatchingConstructorReference(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.Node> Ꮡexpr) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var expr = ref Ꮡexpr.DerefOrNull();

    ж<ast.Node> name = default;
    if (ast.IsPropertyAccessExpression(Ꮡexpr)) {
        name = expr.AsPropertyAccessExpression().Name();
    } else if (ast.IsElementAccessExpression(Ꮡexpr) && ast.IsStringLiteralLike(expr.AsElementAccessExpression().ArgumentExpression)) {
        name = expr.AsElementAccessExpression().ArgumentExpression;
    }
    return name != nil && name.Text() == "constructor"u8 && Ꮡc.isMatchingReference(f.reference, expr.Expression());
}

internal static ж<Type> narrowTypeByConstructor(this ж<Checker> Ꮡc, ж<Type> t, ast.Kind @operator, ж<ast.Node> identifier, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (assumeTrue && @operator != ast.KindEqualsEqualsToken && @operator != ast.KindEqualsEqualsEqualsToken || !assumeTrue && @operator != ast.KindExclamationEqualsToken && @operator != ast.KindExclamationEqualsEqualsToken) {
        return t;
    }
    var identifierType = Ꮡc.getTypeOfExpression(identifier);
    if (!Ꮡc.isFunctionType(identifierType) && !Ꮡc.isConstructorType(identifierType)) {
        return t;
    }
    var prototypeProperty = Ꮡc.getPropertyOfType(identifierType, "prototype");
    if (prototypeProperty == nil) {
        return t;
    }
    var prototypeType = Ꮡc.getTypeOfSymbol(prototypeProperty);
    ж<Type> candidate = default;
    if (!IsTypeAny(prototypeType)) {
        candidate = prototypeType;
    }
    if (candidate == nil || candidate == c.globalObjectType || candidate == c.globalFunctionType) {
        return t;
    }
    if (IsTypeAny(t)) {
        return candidate;
    }
    var candidateʗ1 = candidate;
    return Ꮡc.filterType(t, (ж<Type> tΔ1) => Ꮡc.isConstructedBy(tΔ1, candidateʗ1));
}

internal static bool isConstructedBy(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsObject) != 0 && (ObjectFlags)(source.objectFlags & ObjectFlagsClass) != 0 || (TypeFlags)(target.flags & TypeFlagsObject) != 0 && (ObjectFlags)(target.objectFlags & ObjectFlagsClass) != 0) {
        return source.symbol == target.symbol;
    }
    return Ꮡc.isTypeSubtypeOf(Ꮡsource, Ꮡtarget);
}

internal static ж<Type> narrowTypeByBooleanComparison(this ж<Checker> Ꮡc, ж<FlowState> f, ж<Type> t, ж<ast.Node> expr, ж<ast.Node> ᏑboolValue, ast.Kind @operator, bool assumeTrue) {
    ref var boolValue = ref ᏑboolValue.DerefOrNull();

    assumeTrue = (assumeTrue != (boolValue.Kind == ast.KindTrueKeyword)) != (@operator != ast.KindExclamationEqualsEqualsToken && @operator != ast.KindExclamationEqualsToken);
    return Ꮡc.narrowType(f, t, expr, assumeTrue);
}

internal static ж<Type> narrowTypeByInstanceof(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> Ꮡt, ж<ast.BinaryExpression> Ꮡexpr, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    ref var expr = ref Ꮡexpr.DerefOrNull();

    var left = c.getReferenceCandidate(expr.Left);
    if (!Ꮡc.isMatchingReference(f.reference, left)) {
        if (assumeTrue && c.strictNullChecks && Ꮡc.optionalChainContainsReference(left, f.reference)) {
            return Ꮡc.getAdjustedTypeWithFacts(Ꮡt, TypeFactsNEUndefinedOrNull);
        }
        return Ꮡt;
    }
    var right = expr.Right;
    var rightType = Ꮡc.getTypeOfExpression(right);
    if (!Ꮡc.isTypeDerivedFrom(rightType, c.globalObjectType)) {
        return Ꮡt;
    }
    ж<TypePredicate> predicate = default;
    var signature = Ꮡc.getEffectsSignature(Ꮡexpr.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); if (signature != nil) {
        predicate = Ꮡc.getTypePredicateOfSignature(signature);
    }
    if (predicate != nil && predicate.kind == TypePredicateKindIdentifier && predicate.parameterIndex == 0) {
        return Ꮡc.getNarrowedType(Ꮡt, predicate.t, assumeTrue, true);
    }
    if (!Ꮡc.isTypeDerivedFrom(rightType, c.globalFunctionType)) {
        return Ꮡt;
    }
    var instanceType = Ꮡc.mapType(rightType, Ꮡc.getInstanceType);
    if (IsTypeAny(Ꮡt) && (instanceType == c.globalObjectType || instanceType == c.globalFunctionType) || !assumeTrue && !((TypeFlags)(instanceType.flags & TypeFlagsObject) != 0 && !Ꮡc.IsEmptyAnonymousObjectType(instanceType))) {
        return Ꮡt;
    }
    return Ꮡc.getNarrowedType(Ꮡt, instanceType, assumeTrue, true);
}

internal static ж<Type> getNarrowedType(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ж<Type> candidate, bool assumeTrue, bool checkDerived) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsUnion) == 0) {
        return Ꮡc.getNarrowedTypeWorker(Ꮡt, candidate, assumeTrue, checkDerived);
    }
    var key = new NarrowedTypeKey(Ꮡt, candidate, assumeTrue, checkDerived);
    var (narrowedTypeΔ1, ok) = c.narrowedTypes[key, ꟷ]; if (ok) {
        return narrowedTypeΔ1;
    }
    var narrowedType = Ꮡc.getNarrowedTypeWorker(Ꮡt, candidate, assumeTrue, checkDerived);
    c.narrowedTypes[key] = narrowedType;
    return narrowedType;
}

internal static ж<Type> getNarrowedTypeWorker(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ж<Type> candidate, bool assumeTrue, bool checkDerived) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if (!assumeTrue) {
        if (Ꮡt == candidate) {
            return c.neverType;
        }
        if (checkDerived) {
            return Ꮡc.filterType(Ꮡt, (ж<Type> tΔ1) => !Ꮡc.isTypeDerivedFrom(tΔ1, candidate));
        }
        if ((TypeFlags)(t.flags & TypeFlagsUnknown) != 0) {
            Ꮡt = c.unknownUnionType; t = ref Ꮡt.DerefOrNull();
        }
        var trueType = Ꮡc.getNarrowedType(Ꮡt, candidate, true, false);
        var trueTypeʗ1 = trueType;
        return c.recombineUnknownType(Ꮡc.filterType(Ꮡt, (ж<Type> tΔ2) => !Ꮡc.isTypeSubsetOf(tΔ2, trueTypeʗ1)));
    }
    if ((TypeFlags)(t.flags & TypeFlagsAnyOrUnknown) != 0) {
        return candidate;
    }
    if (Ꮡt == candidate) {
        return candidate;
    }
    @string keyPropertyName = default;
    if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
        keyPropertyName = Ꮡc.getKeyPropertyName(Ꮡt);
    }
    var narrowedType = Ꮡc.mapType(candidate, (ж<Type> n) => {
        var matching = Ꮡt;
        if (keyPropertyName != ""u8) {
            var discriminant = Ꮡc.getTypeOfPropertyOfType(n, keyPropertyName); if (discriminant != nil) {
                var constituent = Ꮡc.getConstituentTypeForKeyType(Ꮡt, discriminant); if (constituent != nil) {
                    matching = constituent;
                }
            }
        }
        Func<ж<Type>, ж<Type>> mapType = default;
        if (checkDerived) {
            mapType = (ж<Type> tΔ3) => {
                switch (ᐧ) {
                case {} when Ꮡc.isTypeDerivedFrom(tΔ3, n): {
                    return tΔ3;
                }
                case {} when Ꮡc.isTypeDerivedFrom(n, tΔ3): {
                    return n;
                }}

                return Ꮡc.Value.neverType;
            };
        } else {
            mapType = (ж<Type> tΔ4) => {
                switch (ᐧ) {
                case {} when Ꮡc.isTypeStrictSubtypeOf(tΔ4, n): {
                    return tΔ4;
                }
                case {} when Ꮡc.isTypeStrictSubtypeOf(n, tΔ4): {
                    return n;
                }
                case {} when Ꮡc.isTypeSubtypeOf(tΔ4, n): {
                    return tΔ4;
                }
                case {} when Ꮡc.isTypeSubtypeOf(n, tΔ4): {
                    return n;
                }}

                return Ꮡc.Value.neverType;
            };
        }
        var directlyRelated = Ꮡc.mapType(matching, mapType);
        if ((TypeFlags)(directlyRelated.flags & TypeFlagsNever) == 0) {
            return directlyRelated;
        }
        ref var isRelated = ref heap<Func<ж<Type>, ж<Type>, bool>>(out var ᏑisRelated);
        if (checkDerived) {
                        ᏑisRelated.ValueSlot = (ж<Type> p1, ж<Type> p2) => Ꮡc.isTypeDerivedFrom(p1, p2);
        } else {
                        ᏑisRelated.ValueSlot = (ж<Type> p1, ж<Type> p2) => Ꮡc.isTypeSubtypeOf(p1, p2);
        }
        return Ꮡc.mapType(Ꮡt, (ж<Type> tΔ5) => {
            if (Ꮡc.Value.maybeTypeOfKind(tΔ5, TypeFlagsInstantiable)) {
                var constraint = Ꮡc.getBaseConstraintOfType(tΔ5);
                if (constraint == nil || ᏑisRelated.ValueSlot(n, constraint)) {
                    return Ꮡc.getIntersectionType([tΔ5, n]);
                }
            }
            return Ꮡc.Value.neverType;
        });
    });
    switch (ᐧ) {
    case {} when (TypeFlags)(narrowedType.flags & TypeFlagsNever) == 0: {
        return narrowedType;
    }
    case {} when Ꮡc.isTypeSubtypeOf(candidate, Ꮡt): {
        return candidate;
    }
    case {} when Ꮡc.isTypeAssignableTo(Ꮡt, candidate): {
        return Ꮡt;
    }
    case {} when Ꮡc.isTypeAssignableTo(candidate, Ꮡt): {
        return candidate;
    }}

    return Ꮡc.getIntersectionType([Ꮡt, candidate]);
}

internal static ж<Type> getInstanceType(this ж<Checker> Ꮡc, ж<Type> constructorType) {
    ref var c = ref Ꮡc.DerefOrNull();

    var prototypePropertyType = Ꮡc.getTypeOfPropertyOfType(constructorType, "prototype");
    if (prototypePropertyType != nil && !IsTypeAny(prototypePropertyType)) {
        return prototypePropertyType;
    }
    var constructSignatures = Ꮡc.getSignaturesOfType(constructorType, SignatureKindConstruct);
    if (len(constructSignatures) != 0) {
        return Ꮡc.getUnionType(core.Map(constructSignatures, ж<Type> (ж<Signature> signature) => Ꮡc.getReturnTypeOfSignature(Ꮡc.getErasedSignature(signature))));
    }
    return c.emptyObjectType;
}

internal static ж<Type> narrowTypeByPrivateIdentifierInInExpression(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<Type> t, ж<ast.BinaryExpression> Ꮡexpr, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var expr = ref Ꮡexpr.DerefOrNull();

    var target = c.getReferenceCandidate(expr.Right);
    if (!Ꮡc.isMatchingReference(f.reference, target)) {
        return t;
    }
    var symbol = c.getSymbolForPrivateIdentifierExpression(expr.Left);
    if (symbol == nil) {
        return t;
    }
    var classSymbol = symbol.Parent;
    ж<Type> targetType = default;
    if (ast.HasStaticModifier(symbol.ValueDeclaration)) {
        targetType = Ꮡc.getTypeOfSymbol(classSymbol);
    } else {
        targetType = Ꮡc.getDeclaredTypeOfSymbol(classSymbol);
    }
    return Ꮡc.getNarrowedType(t, targetType, assumeTrue, true);
}

internal static ж<Type> narrowTypeByInKeyword(this ж<Checker> Ꮡc, ж<FlowState> f, ж<Type> t, ж<Type> nameType, bool assumeTrue) {
    ref var c = ref Ꮡc.DerefOrNull();

    @string name = getPropertyNameFromType(nameType);
    var isKnownProperty = someType(t, (ж<Type> tΔ1) => Ꮡc.isTypePresencePossible(tΔ1, name, true));
    if (isKnownProperty) {
        return Ꮡc.filterType(t, (ж<Type> tΔ2) => Ꮡc.isTypePresencePossible(tΔ2, name, assumeTrue));
    }
    if (assumeTrue) {
        var recordSymbol = c.getGlobalRecordSymbol();
        if (recordSymbol != nil) {
            return Ꮡc.getIntersectionType(slice<ж<Type>>([t, Ꮡc.getTypeAliasInstantiation(recordSymbol, [nameType, c.unknownType], nil)]));
        }
    }
    return t;
}

internal static bool isTypePresencePossible(this ж<Checker> Ꮡc, ж<Type> t, @string propName, bool assumeTrue) {
    var prop = Ꮡc.getPropertyOfType(t, propName);
    if (prop != nil) {
        return (ast.SymbolFlags)(prop.Flags & ast.SymbolFlagsOptional) != 0 || (ast.CheckFlags)(prop.CheckFlags & ast.CheckFlagsPartial) != 0 || assumeTrue;
    }
    return Ꮡc.getApplicableIndexInfoForName(t, propName) != nil || !assumeTrue;
}

internal static ж<Type> narrowTypeByOptionalChainContainment(this ж<Checker> Ꮡc, ж<FlowState> f, ж<Type> t, ast.Kind @operator, ж<ast.Node> value, bool assumeTrue) {
    var equalsOperator = @operator == ast.KindEqualsEqualsToken || @operator == ast.KindEqualsEqualsEqualsToken;
    TypeFlags nullableFlags = default;
    if (@operator == ast.KindEqualsEqualsToken || @operator == ast.KindExclamationEqualsToken) {
        nullableFlags = TypeFlagsNullable;
    } else {
        nullableFlags = TypeFlagsUndefined;
    }
    var valueType = Ꮡc.getTypeOfExpression(value);
    var removeNullable = equalsOperator != assumeTrue && everyType(valueType, (ж<Type> tΔ1) => (TypeFlags)(tΔ1.flags & nullableFlags) != 0) || equalsOperator == assumeTrue && everyType(valueType, (ж<Type> tΔ2) => (TypeFlags)(tΔ2.flags & ((TypeFlags)(TypeFlagsAnyOrUnknown | nullableFlags))) == 0);
    if (removeNullable) {
        return Ꮡc.getAdjustedTypeWithFacts(t, TypeFactsNEUndefinedOrNull);
    }
    return t;
}

internal static FlowType getTypeAtSwitchClause(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    var data = flow.Node.AsFlowSwitchClauseData();
    var expr = ast.SkipParentheses(data.SwitchStatement.Expression());
    var flowType = Ꮡc.getTypeAtFlowNode(Ꮡf, flow.Antecedent);
    var t = flowType.t;
    switch (ᐧ) {
    case {} when Ꮡc.isMatchingReference(f.reference, expr): {
        t = Ꮡc.narrowTypeBySwitchOnDiscriminant(t, data);
        break;
    }
    case {} when expr.Kind == ast.KindTypeOfExpression && Ꮡc.isMatchingReference(f.reference, expr.Expression()): {
        t = Ꮡc.narrowTypeBySwitchOnTypeOf(t, data);
        break;
    }
    case {} when expr.Kind == ast.KindTrueKeyword: {
        t = Ꮡc.narrowTypeBySwitchOnTrue(Ꮡf, t, data);
        break;
    }
    default: {
        if (c.strictNullChecks) {
            if (Ꮡc.optionalChainContainsReference(expr, f.reference)) {
                t = Ꮡc.narrowTypeBySwitchOptionalChainContainment(t, data, (ж<Type> tΔ3) => (TypeFlags)(tΔ3.flags & ((TypeFlags)(TypeFlagsUndefined | TypeFlagsNever))) == 0);
            } else if (ast.IsTypeOfExpression(expr) && Ꮡc.optionalChainContainsReference(expr.Expression(), f.reference)) {
                t = Ꮡc.narrowTypeBySwitchOptionalChainContainment(t, data, (ж<Type> tΔ4) => !((TypeFlags)(tΔ4.flags & TypeFlagsNever) != 0 || (TypeFlags)(tΔ4.flags & TypeFlagsStringLiteral) != 0 && getStringLiteralValue(tΔ4) == "undefined"u8));
            }
        }
        var access = Ꮡc.getDiscriminantPropertyAccess(Ꮡf, expr, t);
        if (access != nil) {
            t = Ꮡc.narrowTypeBySwitchOnDiscriminantProperty(t, access, data);
        }
        break;
    }}

    return c.newFlowType(t, flowType.incomplete);
}

internal static ж<Type> narrowTypeBySwitchOnDiscriminant(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ж<ast.FlowSwitchClauseData> Ꮡdata) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    ref var data = ref Ꮡdata.DerefOrNull();
    var groundClauseTypesᴷ = new stackAppend<ж<Type>>();

    var switchTypes = Ꮡc.getSwitchClauseTypes(data.SwitchStatement);
    if (len(switchTypes) == 0) {
        return Ꮡt;
    }
    var clauseTypes = switchTypes.slice(data.ClauseStart, data.ClauseEnd);
    var hasDefaultClause = data.ClauseStart == data.ClauseEnd || slices.Contains(clauseTypes, c.neverType);
    if (((TypeFlags)(t.flags & TypeFlagsUnknown) != 0) && !hasDefaultClause) {
        slice<ж<Type>> groundClauseTypes = default;
        foreach (var (i, s) in range(clauseTypes)) {
            if ((TypeFlags)(s.flags & ((TypeFlags)(TypeFlagsPrimitive | TypeFlagsNonPrimitive))) != 0) {
                if (groundClauseTypes != nil) {
                    groundClauseTypes = append(groundClauseTypesᴷ, groundClauseTypes, s);
                }
            } else if ((TypeFlags)(s.flags & TypeFlagsObject) != 0) {
                if (groundClauseTypes == nil) {
                    groundClauseTypes = clauseTypes.slice(0, i, i);
                }
                groundClauseTypes = append(groundClauseTypes, c.nonPrimitiveType);
            } else {
                return Ꮡt;
            }
        }
        return Ꮡc.getUnionType(core.IfElse(groundClauseTypes == nil, clauseTypes, groundClauseTypes));
    }
    var discriminantType = Ꮡc.getUnionType(clauseTypes);
    ж<Type> caseType = default;
    if ((TypeFlags)(discriminantType.flags & TypeFlagsNever) != 0) {
        caseType = c.neverType;
    } else {
        if ((TypeFlags)(discriminantType.flags & TypeFlagsPrimitive) != 0 && Ꮡc.isUniformUnionType(Ꮡt)) {
            var regularType = Ꮡc.getRegularTypeOfLiteralType(discriminantType);
            if (c.unionContainsType(Ꮡt, regularType, false)) {
                caseType = regularType;
            }
        }
        if (caseType == nil) {
            var discriminantTypeʗ1 = discriminantType;
            var filtered = Ꮡc.filterType(Ꮡt, (ж<Type> tΔ1) => Ꮡc.areTypesComparable(discriminantTypeʗ1, tΔ1));
            caseType = Ꮡc.replacePrimitivesWithLiterals(filtered, discriminantType);
        }
    }
    if (!hasDefaultClause) {
        return caseType;
    }
    var switchTypesʗ1 = switchTypes;
    var defaultType = Ꮡc.filterType(Ꮡt, (ж<Type> tΔ2) => {
        if (!Ꮡc.isUnitLikeType(tΔ2)) {
            return true;
        }
        ref var u = ref heap<ж<Type>>(Ꮡc.Value.undefinedType, out var Ꮡu);
        if ((TypeFlags)(tΔ2.flags & TypeFlagsUndefined) == 0) {
            Ꮡu.ValueSlot = Ꮡc.getRegularTypeOfLiteralType(Ꮡc.Value.extractUnitType(tΔ2));
        }
        return !slices.ContainsFunc(switchTypesʗ1, (ж<Type> st) => isUnitType(st) && Ꮡc.areTypesComparable(st, Ꮡu.ValueSlot));
    });
    if ((TypeFlags)(caseType.flags & TypeFlagsNever) != 0) {
        return defaultType;
    }
    return Ꮡc.getUnionType([caseType, defaultType]);
}

internal static ж<Type> narrowTypeBySwitchOnTypeOf(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.FlowSwitchClauseData> Ꮡdata) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var data = ref Ꮡdata.DerefOrNull();

    var witnesses = c.getSwitchClauseTypeOfWitnesses(data.SwitchStatement);
    if (witnesses == nil) {
        return t;
    }
    var clauses = data.SwitchStatement.AsSwitchStatement().CaseBlock.AsCaseBlock().Clauses.Nodes;
    nint defaultIndex = core.FindIndex(clauses, (ж<ast.Node> clause) => clause.Kind == ast.KindDefaultClause);
    nint clauseStart = (nint)data.ClauseStart;
    nint clauseEnd = (nint)data.ClauseEnd;
    var hasDefaultClause = clauseStart == clauseEnd || (defaultIndex >= clauseStart && defaultIndex < clauseEnd);
    if (hasDefaultClause) {
        var notEqualFacts = c.getNotEqualFactsFromTypeofSwitch(clauseStart, clauseEnd, witnesses);
        return Ꮡc.filterType(t, (ж<Type> tΔ1) => Ꮡc.getTypeFacts(tΔ1, notEqualFacts) == notEqualFacts);
    }
    var clauseWitnesses = witnesses.slice(clauseStart, clauseEnd);
    return Ꮡc.getUnionType(core.Map(clauseWitnesses, ж<Type> (@string text) => {
        if (text != ""u8) {
            return Ꮡc.narrowTypeByTypeName(t, text);
        }
        return Ꮡc.Value.neverType;
    }));
}

internal static ж<Type> narrowTypeBySwitchOnTrue(this ж<Checker> Ꮡc, ж<FlowState> f, ж<Type> Ꮡt, ж<ast.FlowSwitchClauseData> Ꮡdata) {
    ref var t = ref Ꮡt.DerefOrNull();
    ref var data = ref Ꮡdata.DerefOrNull();

    var clauses = data.SwitchStatement.AsSwitchStatement().CaseBlock.AsCaseBlock().Clauses.Nodes;
    nint defaultIndex = core.FindIndex(clauses, (ж<ast.Node> clause) => clause.Kind == ast.KindDefaultClause);
    nint clauseStart = (nint)data.ClauseStart;
    nint clauseEnd = (nint)data.ClauseEnd;
    var hasDefaultClause = clauseStart == clauseEnd || (defaultIndex >= clauseStart && defaultIndex < clauseEnd);
    foreach (var i in range(clauseStart)) {
        var clause = clauses[i];
        if (clause.Kind == ast.KindCaseClause) {
            Ꮡt = Ꮡc.narrowType(f, Ꮡt, clause.Expression(), false); t = ref Ꮡt.DerefOrNull();
        }
    }
    if (hasDefaultClause) {
        for (nint i = clauseEnd; i < len(clauses); i++) {
            var clause = clauses[i];
            if (clause.Kind == ast.KindCaseClause) {
                Ꮡt = Ꮡc.narrowType(f, Ꮡt, clause.Expression(), false); t = ref Ꮡt.DerefOrNull();
            }
        }
        return Ꮡt;
    }
    return Ꮡc.getUnionType(core.Map(clauses.slice(clauseStart, clauseEnd), ж<Type> (ж<ast.Node> clause) => {
        if (clause.Kind == ast.KindCaseClause) {
            return Ꮡc.narrowType(f, Ꮡt, clause.Expression(), true);
        }
        return Ꮡc.Value.neverType;
    }));
}

internal static ж<Type> narrowTypeBySwitchOptionalChainContainment(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.FlowSwitchClauseData> Ꮡdata, Func<ж<Type>, bool> clauseCheck) {
    ref var data = ref Ꮡdata.DerefOrNull();

    var everyClauseChecks = data.ClauseStart != data.ClauseEnd && core.Every(Ꮡc.getSwitchClauseTypes(data.SwitchStatement).slice(data.ClauseStart, data.ClauseEnd), clauseCheck);
    if (everyClauseChecks) {
        return Ꮡc.getTypeWithFacts(t, TypeFactsNEUndefinedOrNull);
    }
    return t;
}

internal static ж<Type> narrowTypeBySwitchOnDiscriminantProperty(this ж<Checker> Ꮡc, ж<Type> Ꮡt, ж<ast.Node> access, ж<ast.FlowSwitchClauseData> Ꮡdata) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    ref var data = ref Ꮡdata.DerefOrNull();

    if (data.ClauseStart < data.ClauseEnd && (TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
        var (accessedName, _) = Ꮡc.getAccessedPropertyName(access);
        if (accessedName != ""u8 && Ꮡc.getKeyPropertyName(Ꮡt) == accessedName) {
            var clauseTypes = Ꮡc.getSwitchClauseTypes(data.SwitchStatement).slice(data.ClauseStart, data.ClauseEnd);
            var candidate = Ꮡc.getUnionType(core.Map(clauseTypes, ж<Type> (ж<Type> s) => {
                var result = Ꮡc.getConstituentTypeForKeyType(Ꮡt, s);
                if (result != nil) {
                    return result;
                }
                return Ꮡc.Value.unknownType;
            }));
            if (candidate != c.unknownType) {
                return candidate;
            }
        }
    }
    return Ꮡc.narrowTypeByDiscriminant(Ꮡt, access, (ж<Type> tΔ1) => Ꮡc.narrowTypeBySwitchOnDiscriminant(tΔ1, Ꮡdata));
}

internal static FlowType getTypeAtFlowBranchLabel(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> flow, ж<ast.FlowList> antecedents) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();

    nint antecedentStart = len(c.antecedentTypes);
    var subtypeReduction = false;
    var seenIncomplete = false;
    ж<ast.FlowNode> bypassFlow = default;
    for (var list = antecedents; list != nil; list = list.Next) {
        var antecedent = list.Flow;
        if (bypassFlow == nil && (ast.FlowFlags)(antecedent.Flags & ast.FlowFlagsSwitchClause) != 0 && antecedent.Node.AsFlowSwitchClauseData().IsEmpty()) {
            bypassFlow = antecedent;
            continue;
        }
        var flowType = Ꮡc.getTypeAtFlowNode(Ꮡf, antecedent);
        if (flowType.t == f.declaredType && f.declaredType == f.initialType) {
            c.antecedentTypes = c.antecedentTypes.slice(0, antecedentStart);
            return new FlowType(t: flowType.t);
        }
        if (!slices.Contains(c.antecedentTypes.slice(antecedentStart), flowType.t)) {
            c.antecedentTypes = append(c.antecedentTypes, flowType.t);
        }
        if (!Ꮡc.isTypeSubsetOf(flowType.t, f.initialType)) {
            subtypeReduction = true;
        }
        if (flowType.incomplete) {
            seenIncomplete = true;
        }
    }
    if (bypassFlow != nil) {
        var flowType = Ꮡc.getTypeAtFlowNode(Ꮡf, bypassFlow);
        if ((TypeFlags)(flowType.t.flags & TypeFlagsNever) == 0 && !slices.Contains(c.antecedentTypes.slice(antecedentStart), flowType.t) && !Ꮡc.isExhaustiveSwitchStatement(bypassFlow.Node.AsFlowSwitchClauseData().SwitchStatement)) {
            if (flowType.t == f.declaredType && f.declaredType == f.initialType) {
                c.antecedentTypes = c.antecedentTypes.slice(0, antecedentStart);
                return new FlowType(t: flowType.t);
            }
            c.antecedentTypes = append(c.antecedentTypes, flowType.t);
            if (!Ꮡc.isTypeSubsetOf(flowType.t, f.initialType)) {
                subtypeReduction = true;
            }
            if (flowType.incomplete) {
                seenIncomplete = true;
            }
        }
    }
    var result = c.newFlowType(Ꮡc.getUnionOrEvolvingArrayType(Ꮡf, c.antecedentTypes.slice(antecedentStart), core.IfElse(subtypeReduction, UnionReductionSubtype, UnionReductionLiteral)), seenIncomplete);
    c.antecedentTypes = c.antecedentTypes.slice(0, antecedentStart);
    return result;
}

internal static ж<Type> getUnionOrEvolvingArrayType(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, slice<ж<Type>> types, UnionReduction subtypeReduction) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();

    if (isEvolvingArrayTypeList(types)) {
        return Ꮡc.getEvolvingArrayType(Ꮡc.getUnionType(core.Map<ж<Type>, ж<Type>>(types, Ꮡc.getElementTypeOfEvolvingArrayType)));
    }
    var result = c.recombineUnknownType(Ꮡc.getUnionTypeEx(core.SameMap<ж<Type>>(types, Ꮡc.finalizeEvolvingArrayType), subtypeReduction, nil, nil));
    if (result != f.declaredType && (TypeFlags)((TypeFlags)(result.flags & f.declaredType.flags) & TypeFlagsUnion) != 0 && slices.Equal<slice<ж<Type>>, ж<Type>>(result.AsUnionType().types, f.declaredType.AsUnionType().types)) {
        return f.declaredType;
    }
    return result;
}

internal static FlowType getTypeAtFlowLoopLabel(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    if (f.refKey.IsZero()) {
        f.refKey = Ꮡc.getFlowReferenceKey(Ꮡf);
    }
    if (f.refKey == nonDottedNameCacheKey) {
        return new FlowType(t: f.declaredType);
    }
    var key = new FlowLoopKey(flowNode: Ꮡflow, refKey: f.refKey);
    {
        var cached = c.flowLoopCache[key]; if (cached != nil) {
            return new FlowType(t: cached);
        }
    }
    foreach (var (_, loopInfo) in range(c.flowLoopStack)) {
        if (loopInfo.key == key && len(loopInfo.types) != 0) {
            return c.newFlowType(Ꮡc.getUnionOrEvolvingArrayType(Ꮡf, loopInfo.types, UnionReductionLiteral), true);
        }
    }
    var antecedentTypes = new slice<ж<Type>>(0, 4);
    var subtypeReduction = false;
    FlowType firstAntecedentType = default;
    for (var list = flow.Antecedents; list != nil; list = list.Next) {
        FlowType flowType = default;
        if (firstAntecedentType.isNil()) {
            firstAntecedentType = Ꮡc.getTypeAtFlowNode(Ꮡf, list.Flow);
            flowType = firstAntecedentType;
        } else {
            c.flowLoopStack = append(c.flowLoopStack, new FlowLoopInfo(key: key, types: antecedentTypes));
            var saveFlowTypeCache = c.flowTypeCache;
            c.flowTypeCache = default;
            flowType = Ꮡc.getTypeAtFlowNode(Ꮡf, list.Flow);
            c.flowTypeCache = saveFlowTypeCache;
            c.flowLoopStack = c.flowLoopStack.slice(0, len(c.flowLoopStack) - 1);
            var cached = c.flowLoopCache[key]; if (cached != nil) {
                return new FlowType(t: cached);
            }
        }
        antecedentTypes = core.AppendIfUnique(antecedentTypes, flowType.t);
        if (!Ꮡc.isTypeSubsetOf(flowType.t, f.initialType)) {
            subtypeReduction = true;
        }
        if (flowType.t == f.declaredType) {
            break;
        }
    }
    var result = Ꮡc.getUnionOrEvolvingArrayType(Ꮡf, antecedentTypes, core.IfElse(subtypeReduction, UnionReductionSubtype, UnionReductionLiteral));
    if (firstAntecedentType.incomplete) {
        return c.newFlowType(result, true);
    }
    c.flowLoopCache[key] = result;
    return new FlowType(t: result);
}

internal static FlowType getTypeAtFlowArrayMutation(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    if (f.declaredType == c.autoType || f.declaredType == c.autoArrayType) {
        var node = flow.Node;
        ж<ast.Node> expr = default;
        if (ast.IsCallExpression(node)) {
            expr = node.Expression().Expression();
        } else {
            expr = node.AsBinaryExpression().Left.Expression();
        }
        if (Ꮡc.isMatchingReference(f.reference, c.getReferenceCandidate(expr))) {
            var flowType = Ꮡc.getTypeAtFlowNode(Ꮡf, flow.Antecedent);
            if ((ObjectFlags)(flowType.t.objectFlags & ObjectFlagsEvolvingArray) != 0) {
                var evolvedType = flowType.t;
                if (ast.IsCallExpression(node)) {
                    foreach (var (_, arg) in range(node.Arguments())) {
                        evolvedType = Ꮡc.addEvolvingArrayElementType(evolvedType, arg);
                    }
                } else {
                    var indexType = Ꮡc.getContextFreeTypeOfExpression(node.AsBinaryExpression().Left.AsElementAccessExpression().ArgumentExpression);
                    if (Ꮡc.isTypeAssignableToKind(indexType, TypeFlagsNumberLike)) {
                        evolvedType = Ꮡc.addEvolvingArrayElementType(evolvedType, node.AsBinaryExpression().Right);
                    }
                }
                return c.newFlowType(evolvedType, flowType.incomplete);
            }
            return flowType;
        }
    }
    return new FlowType();
}

internal static ж<ast.Node> getDiscriminantPropertyAccess(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.Node> expr, ж<Type> ᏑcomputedType) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var computedType = ref ᏑcomputedType.DerefOrNull();

    if ((TypeFlags)(f.declaredType.flags & TypeFlagsUnion) != 0 || (TypeFlags)(computedType.flags & TypeFlagsUnion) != 0) {
        var access = Ꮡc.getCandidateDiscriminantPropertyAccess(Ꮡf, expr);
        if (access != nil) {
            var (name, ok) = Ꮡc.getAccessedPropertyName(access); if (ok) {
                var t = ᏑcomputedType;
                if ((TypeFlags)(f.declaredType.flags & TypeFlagsUnion) != 0 && Ꮡc.isTypeSubsetOf(ᏑcomputedType, f.declaredType)) {
                    t = f.declaredType;
                }
                if (Ꮡc.isDiscriminantProperty(t, name)) {
                    return access;
                }
            }
        }
    }
    return default;
}

internal static ж<ast.Node> getCandidateDiscriminantPropertyAccess(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.Node> Ꮡexpr) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var expr = ref Ꮡexpr.DerefOrNull();

    switch (ᐧ) {
    case {} when ast.IsBindingPattern(f.reference) || ast.IsFunctionExpressionOrArrowFunction(f.reference) || ast.IsObjectLiteralMethod(f.reference): {
        if (ast.IsIdentifier(Ꮡexpr)) {
            var symbol = c.getResolvedSymbol(Ꮡexpr);
            var declaration = c.getExportSymbolOfValueSymbolIfExported(symbol).ValueDeclaration;
            if (declaration != nil && (ast.IsBindingElement(declaration) || ast.IsParameterDeclaration(declaration)) && f.reference == declaration.Parent && declaration.Initializer() == nil && !hasDotDotDotToken(declaration)) {
                return declaration;
            }
        }
        break;
    }
    case {} when ast.IsAccessExpression(Ꮡexpr): {
        if (Ꮡc.isMatchingReference(f.reference, expr.Expression())) {
            return Ꮡexpr;
        }
        break;
    }
    case {} when ast.IsIdentifier(Ꮡexpr): {
        var symbol = c.getResolvedSymbol(Ꮡexpr);
        if (c.isConstantVariable(symbol)) {
            var declaration = symbol.ValueDeclaration;
            var initializer = getCandidateVariableDeclarationInitializer(declaration);
            if (initializer != nil && ast.IsAccessExpression(initializer) && Ꮡc.isMatchingReference(f.reference, initializer.Expression())) {
                return initializer;
            }
            if (ast.IsBindingElement(declaration) && declaration.Initializer() == nil) {
                initializer = getCandidateVariableDeclarationInitializer(declaration.Parent.Parent);
                if (initializer != nil && (ast.IsIdentifier(initializer) || ast.IsAccessExpression(initializer)) && Ꮡc.isMatchingReference(f.reference, initializer)) {
                    return declaration;
                }
            }
        }
        break;
    }}

    return default;
}

} partial class checker_package {

internal static ж<ast.Node> getCandidateVariableDeclarationInitializer(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsVariableDeclaration(Ꮡnode) && node.Type() == nil) {
        var initializer = node.Initializer(); if (initializer != nil) {
            return ast.SkipParentheses(initializer);
        }
    }
    return default;
}

} partial class checkerᴛChecker {

internal static ж<Type> getEvolvingArrayType(this ж<Checker> Ꮡc, ж<Type> ᏑelementType) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var elementType = ref ᏑelementType.DerefOrNull();

    var key = new CachedTypeKey(kind: CachedTypeKindEvolvingArrayType, typeId: elementType.id);
    var result = c.cachedTypes[key];
    if (result == nil) {
        result = Ꮡc.newObjectType(ObjectFlagsEvolvingArray, nil);
        result.AsEvolvingArrayType().elementType = ᏑelementType;
        c.cachedTypes[key] = result;
    }
    return result;
}

internal static ж<Type> getElementTypeOfEvolvingArrayType(this ref Checker c, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((ObjectFlags)(t.objectFlags & ObjectFlagsEvolvingArray) != 0) {
        return t.AsEvolvingArrayType().elementType;
    }
    return c.neverType;
}

} partial class checker_package {

internal static bool isEvolvingArrayTypeList(slice<ж<Type>> types) {
    var hasEvolvingArrayType = false;
    foreach (var (_, t) in range(types)) {
        if ((TypeFlags)(t.flags & TypeFlagsNever) == 0) {
            if ((ObjectFlags)(t.objectFlags & ObjectFlagsEvolvingArray) == 0) {
                return false;
            }
            hasEvolvingArrayType = true;
        }
    }
    return hasEvolvingArrayType;
}

} partial class checkerᴛChecker {

internal static bool isEvolvingArrayOperationTarget(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    var root = c.getReferenceRoot(node);
    var parent = root.Parent;
    var isLengthPushOrUnshift = ast.IsPropertyAccessExpression(parent) && (parent.Name().Text() == "length"u8 || ast.IsCallExpression(parent.Parent) && ast.IsIdentifier(parent.Name()) && ast.IsPushOrUnshiftIdentifier(parent.Name()));
    var isElementAssignment = ast.IsElementAccessExpression(parent) && parent.Expression() == root && ast.IsBinaryExpression(parent.Parent) && parent.Parent.AsBinaryExpression().OperatorToken.Kind == ast.KindEqualsToken && parent.Parent.AsBinaryExpression().Left == parent && !ast.IsAssignmentTarget(parent.Parent) && Ꮡc.isTypeAssignableToKind(Ꮡc.getTypeOfExpression(parent.AsElementAccessExpression().ArgumentExpression), TypeFlagsNumberLike);
    return isLengthPushOrUnshift || isElementAssignment;
}

internal static ж<Type> addEvolvingArrayElementType(this ж<Checker> Ꮡc, ж<Type> ᏑevolvingArrayType, ж<ast.Node> node) {
    ref var evolvingArrayType = ref ᏑevolvingArrayType.DerefOrNull();

    var newElementType = Ꮡc.getRegularTypeOfObjectLiteral(Ꮡc.getBaseTypeOfLiteralType(Ꮡc.getContextFreeTypeOfExpression(node)));
    var elementType = evolvingArrayType.AsEvolvingArrayType().elementType;
    if (Ꮡc.isTypeSubsetOf(newElementType, elementType)) {
        return ᏑevolvingArrayType;
    }
    return Ꮡc.getEvolvingArrayType(Ꮡc.getUnionType([elementType, newElementType]));
}

internal static ж<Type> finalizeEvolvingArrayType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if ((ObjectFlags)(t.objectFlags & ObjectFlagsEvolvingArray) != 0) {
        return Ꮡc.getFinalArrayType(t.AsEvolvingArrayType());
    }
    return Ꮡt;
}

internal static ж<Type> getFinalArrayType(this ж<Checker> Ꮡc, ж<EvolvingArrayType> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (t.finalArrayType == nil) {
        t.finalArrayType = Ꮡc.createFinalArrayType(t.elementType);
    }
    return t.finalArrayType;
}

internal static ж<Type> createFinalArrayType(this ж<Checker> Ꮡc, ж<Type> ᏑelementType) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var elementType = ref ᏑelementType.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(elementType.flags & TypeFlagsNever) != 0: {
        return c.autoArrayType;
    }
    case {} when (TypeFlags)(elementType.flags & TypeFlagsUnion) != 0: {
        return Ꮡc.createArrayType(Ꮡc.getUnionTypeEx(elementType.Types(), UnionReductionSubtype, nil, nil));
    }}

    return Ꮡc.createArrayType(ᏑelementType);
}

internal static void reportFlowControlError(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    var block = ast.FindAncestor(node, ast.IsFunctionOrModuleBlock);
    var sourceFile = ast.GetSourceFileOfNode(node);
    var span = scanner.GetRangeOfTokenAtPosition(sourceFile, block.StatementList().Pos());
    Ꮡc.addDiagnostic(ast.NewDiagnostic(sourceFile, span, diagnostics.The_containing_function_or_module_body_is_too_large_for_control_flow_analysis));
}

internal static bool isMatchingReference(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡsource, ж<ast.Node> Ꮡtarget) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    switch (target.Kind) {
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression || ᴛ == ast.KindNonNullExpression: {
        return Ꮡc.isMatchingReference(Ꮡsource, target.Expression());
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return ast.IsAssignmentExpression(Ꮡtarget, false) && Ꮡc.isMatchingReference(Ꮡsource, target.AsBinaryExpression().Left) || ast.IsBinaryExpression(Ꮡtarget) && target.AsBinaryExpression().OperatorToken.Kind == ast.KindCommaToken && Ꮡc.isMatchingReference(Ꮡsource, target.AsBinaryExpression().Right);
    }}

    switch (source.Kind) {
    case var ᴛ when ᴛ == ast.KindMetaProperty: {
        return ast.IsMetaProperty(Ꮡtarget) && source.AsMetaProperty().KeywordToken == target.AsMetaProperty().KeywordToken && source.Name().Text() == target.Name().Text();
    }
    case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindPrivateIdentifier: {
        if (ast.IsThisInTypeQuery(Ꮡsource)) {
            return target.Kind == ast.KindThisKeyword;
        }
        return ast.IsIdentifier(Ꮡtarget) && c.getResolvedSymbol(Ꮡsource) == c.getResolvedSymbol(Ꮡtarget) || (ast.IsVariableDeclaration(Ꮡtarget) || ast.IsBindingElement(Ꮡtarget)) && c.getExportSymbolOfValueSymbolIfExported(c.getResolvedSymbol(Ꮡsource)) == Ꮡc.getSymbolOfDeclaration(Ꮡtarget);
    }
    case var ᴛ when ᴛ == ast.KindThisKeyword: {
        return target.Kind == ast.KindThisKeyword;
    }
    case var ᴛ when ᴛ == ast.KindSuperKeyword: {
        return target.Kind == ast.KindSuperKeyword;
    }
    case var ᴛ when ᴛ == ast.KindNonNullExpression || ᴛ == ast.KindParenthesizedExpression || ᴛ == ast.KindSatisfiesExpression: {
        return Ꮡc.isMatchingReference(source.Expression(), Ꮡtarget);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        {
            var (sourcePropertyName, ok) = Ꮡc.getAccessedPropertyName(Ꮡsource); if (ok) {
                if (ast.IsAccessExpression(Ꮡtarget)) {
                    var (targetPropertyName, okΔ1) = Ꮡc.getAccessedPropertyName(Ꮡtarget); if (okΔ1) {
                        return targetPropertyName == sourcePropertyName && Ꮡc.isMatchingReference(source.Expression(), target.Expression());
                    }
                }
            }
        }
        if (ast.IsElementAccessExpression(Ꮡsource) && ast.IsElementAccessExpression(Ꮡtarget)) {
            var sourceArg = source.AsElementAccessExpression().ArgumentExpression;
            var targetArg = target.AsElementAccessExpression().ArgumentExpression;
            if (ast.IsIdentifier(sourceArg) && ast.IsIdentifier(targetArg)) {
                var symbol = c.getResolvedSymbol(sourceArg);
                if (symbol == c.getResolvedSymbol(targetArg) && (c.isConstantVariable(symbol) || c.isParameterOrMutableLocalVariable(symbol) && !Ꮡc.isSymbolAssigned(symbol))) {
                    return Ꮡc.isMatchingReference(source.Expression(), target.Expression());
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        if (ast.IsAccessExpression(Ꮡtarget)) {
            var (targetPropertyName, ok) = Ꮡc.getAccessedPropertyName(Ꮡtarget); if (ok) {
                return source.AsQualifiedName().Right.Text() == targetPropertyName && Ꮡc.isMatchingReference(source.AsQualifiedName().Left, target.Expression());
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return ast.IsBinaryExpression(Ꮡsource) && source.AsBinaryExpression().OperatorToken.Kind == ast.KindCommaToken && Ꮡc.isMatchingReference(source.AsBinaryExpression().Right, Ꮡtarget);
    }}

    return false;
}

} partial class checker_package {

internal static CacheHashKey nonDottedNameCacheKey = ((CacheHashKey)xxh3.HashString128("?"));

} partial class checkerᴛChecker {

internal static CacheHashKey getFlowReferenceKey(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf) {
    ref var f = ref Ꮡf.DerefOrNull();

    ref var b = ref heap(new keyBuilder(), out var Ꮡb);
    if (Ꮡc.writeFlowCacheKey(Ꮡb, f.reference, f.declaredType, f.initialType, f.flowContainer)) {
        return b.hash();
    }
    return nonDottedNameCacheKey;
}

internal static bool writeFlowCacheKey(this ж<Checker> Ꮡc, ж<keyBuilder> Ꮡb, ж<ast.Node> Ꮡnode, ж<Type> declaredType, ж<Type> initialType, ж<ast.Node> flowContainer) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var exprᴛ1 = node.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == ast.KindIdentifier) { matchᴛ1 = true;
        if (!ast.IsThisInTypeQuery(Ꮡnode)) {
            var symbol = c.getResolvedSymbol(Ꮡnode);
            if (symbol == c.unknownSymbol) {
                return false;
            }
            b.writeSymbol(symbol);
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && exprᴛ1 == ast.KindThisKeyword) { matchᴛ1 = true;
        b.writeByte((byte)':');
        b.writeType(declaredType);
        if (initialType != declaredType) {
            b.writeByte((byte)'=');
            b.writeType(initialType);
        }
        if (flowContainer != nil) {
            b.writeByte((byte)'@');
            b.writeNode(flowContainer);
        }
        return true;
    }
    if (exprᴛ1 == ast.KindNonNullExpression || exprᴛ1 == ast.KindParenthesizedExpression) { matchᴛ1 = true;
        return Ꮡc.writeFlowCacheKey(Ꮡb, node.Expression(), declaredType, initialType, flowContainer);
    }
    if (exprᴛ1 == ast.KindQualifiedName) { matchᴛ1 = true;
        if (!Ꮡc.writeFlowCacheKey(Ꮡb, node.AsQualifiedName().Left, declaredType, initialType, flowContainer)) {
            return false;
        }
        b.writeByte((byte)'.');
        b.writeString(node.AsQualifiedName().Right.Text());
        return true;
    }
    if (exprᴛ1 == ast.KindPropertyAccessExpression || exprᴛ1 == ast.KindElementAccessExpression) {
        var (propName, ok) = Ꮡc.getAccessedPropertyName(Ꮡnode); if (ok) {
            if (!Ꮡc.writeFlowCacheKey(Ꮡb, node.Expression(), declaredType, initialType, flowContainer)) {
                return false;
            }
            b.writeByte((byte)'.');
            b.writeString(propName);
            return true;
        }
        if (ast.IsElementAccessExpression(Ꮡnode) && ast.IsIdentifier(node.AsElementAccessExpression().ArgumentExpression)) {
            var symbol = c.getResolvedSymbol(node.AsElementAccessExpression().ArgumentExpression);
            if (c.isConstantVariable(symbol) || c.isParameterOrMutableLocalVariable(symbol) && !Ꮡc.isSymbolAssigned(symbol)) {
                if (!Ꮡc.writeFlowCacheKey(Ꮡb, node.Expression(), declaredType, initialType, flowContainer)) {
                    return false;
                }
                b.writeString(".@");
                b.writeSymbol(symbol);
                return true;
            }
        }
    }
    else if (exprᴛ1 == ast.KindObjectBindingPattern || exprᴛ1 == ast.KindArrayBindingPattern || exprᴛ1 == ast.KindFunctionDeclaration || exprᴛ1 == ast.KindFunctionExpression || exprᴛ1 == ast.KindArrowFunction || exprᴛ1 == ast.KindMethodDeclaration) { matchᴛ1 = true;
        b.writeNode(Ꮡnode);
        b.writeByte((byte)'#');
        b.writeType(declaredType);
        return true;
    }

    return false;
}

internal static (@string, bool) getAccessedPropertyName(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡaccess) {
    ref var access = ref Ꮡaccess.DerefOrNull();

    if (ast.IsPropertyAccessExpression(Ꮡaccess)) {
        return (access.Name().Text(), true);
    }
    if (ast.IsElementAccessExpression(Ꮡaccess)) {
        return Ꮡc.tryGetElementAccessExpressionName(access.AsElementAccessExpression());
    }
    if (ast.IsBindingElement(Ꮡaccess)) {
        return Ꮡc.getDestructuringPropertyName(Ꮡaccess);
    }
    if (ast.IsParameterDeclaration(Ꮡaccess)) {
        return (strconv.Itoa(slices.Index(access.Parent.Parameters(), Ꮡaccess)), true);
    }
    return ("", false);
}

internal static (@string, bool) tryGetElementAccessExpressionName(this ж<Checker> Ꮡc, ж<ast.ElementAccessExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (ᐧ) {
    case {} when ast.IsStringOrNumericLiteralLike(node.ArgumentExpression): {
        return (node.ArgumentExpression.Text(), true);
    }
    case {} when ast.IsEntityNameExpression(node.ArgumentExpression): {
        return Ꮡc.tryGetNameFromEntityNameExpression(node.ArgumentExpression);
    }}

    return ("", false);
}

internal static (@string, bool) tryGetNameFromEntityNameExpression(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    var symbol = Ꮡc.resolveEntityName(node, ast.SymbolFlagsValue, true, false, nil);
    if (symbol == nil || !(c.isConstantVariable(symbol) || ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsEnumMember) != 0))) {
        return ("", false);
    }
    var declaration = symbol.ValueDeclaration;
    if (declaration == nil) {
        return ("", false);
    }
    var t = Ꮡc.tryGetTypeFromTypeNode(declaration);
    if (t != nil) {
        var (name, ok) = checker.tryGetNameFromType(t); if (ok) {
            return (name, true);
        }
    }
    if (hasOnlyExpressionInitializer(ref (declaration).DerefOrNull()) && !ast.IsBindingElement(declaration) && Ꮡc.isBlockScopedNameDeclaredBeforeUse(declaration, node)) {
        var initializer = declaration.Initializer(); if (initializer != nil) {
            var initializerType = Ꮡc.getTypeOfExpression(initializer); if (initializerType != nil) {
                return checker.tryGetNameFromType(initializerType);
            }
        } else if (ast.IsEnumMember(declaration)) {
            return ast.TryGetTextOfPropertyName(declaration.Name());
        }
    }
    return ("", false);
}

} partial class checker_package {

internal static (@string, bool) tryGetNameFromType(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(t.flags & TypeFlagsUniqueESSymbol) != 0: {
        return (t.AsUniqueESSymbolType().name, true);
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsStringOrNumberLiteral) != 0: {
        return (evaluator.AnyToString(t.AsLiteralType().value), true);
    }}

    return ("", false);
}

} partial class checkerᴛChecker {

internal static (@string, bool) getDestructuringPropertyName(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (ast.IsBindingElement(Ꮡnode) && ast.IsObjectBindingPattern(parent)) {
        return Ꮡc.getLiteralPropertyNameText(getBindingElementPropertyName(Ꮡnode));
    }
    if (ast.IsPropertyAssignment(Ꮡnode) || ast.IsShorthandPropertyAssignment(Ꮡnode)) {
        return Ꮡc.getLiteralPropertyNameText(node.Name());
    }
    if (ast.IsArrayLiteralExpression(parent) || ast.IsArrayBindingPattern(parent)) {
        return (strconv.Itoa(slices.Index(parent.Elements(), Ꮡnode)), true);
    }
    return ("", false);
}

internal static (@string, bool) getLiteralPropertyNameText(this ж<Checker> Ꮡc, ж<ast.Node> name) {
    var t = Ꮡc.getLiteralTypeFromPropertyName(name);
    if ((TypeFlags)(t.flags & ((TypeFlags)(TypeFlagsStringLiteral | TypeFlagsNumberLiteral))) != 0) {
        return (evaluator.AnyToString(t.AsLiteralType().value), true);
    }
    return ("", false);
}

internal static bool isConstantReference(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindThisKeyword: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        if (!ast.IsThisInTypeQuery(Ꮡnode)) {
            var symbol = c.getResolvedSymbol(Ꮡnode);
            return c.isConstantVariable(symbol) || c.isParameterOrMutableLocalVariable(symbol) && !Ꮡc.isSymbolAssigned(symbol) || symbol.ValueDeclaration != nil && ast.IsFunctionExpression(symbol.ValueDeclaration);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        if (Ꮡc.isConstantReference(node.Expression())) {
            var symbol = c.getResolvedSymbolOrNil(Ꮡnode);
            if (symbol != nil) {
                return Ꮡc.isReadonlySymbol(symbol);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindObjectBindingPattern || ᴛ == ast.KindArrayBindingPattern: {
        var rootDeclaration = ast.GetRootDeclaration(node.Parent);
        if (ast.IsParameterDeclaration(rootDeclaration) || ast.IsVariableDeclaration(rootDeclaration) && ast.IsCatchClause(rootDeclaration.Parent)) {
            return !Ꮡc.isSomeSymbolAssigned(rootDeclaration);
        }
        return ast.IsVariableDeclaration(rootDeclaration) && c.isVarConstLike(rootDeclaration);
    }}

    return false;
}

internal static bool containsMatchingReference(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡsource, ж<ast.Node> target) {
    ref var source = ref Ꮡsource.DerefOrNull();

    while (ast.IsAccessExpression(Ꮡsource)) {
        Ꮡsource = source.Expression(); source = ref Ꮡsource.DerefOrNull();
        if (Ꮡc.isMatchingReference(Ꮡsource, target)) {
            return true;
        }
    }
    return false;
}

internal static bool optionalChainContainsReference(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡsource, ж<ast.Node> target) {
    ref var source = ref Ꮡsource.DerefOrNull();

    while (ast.IsOptionalChain(Ꮡsource)) {
        Ꮡsource = source.Expression(); source = ref Ꮡsource.DerefOrNull();
        if (Ꮡc.isMatchingReference(Ꮡsource, target)) {
            return true;
        }
    }
    return false;
}

internal static ж<ast.Node> getReferenceCandidate(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return c.getReferenceCandidate(node.Expression());
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        switch (node.AsBinaryExpression().OperatorToken.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindEqualsToken || ᴛ2 == ast.KindBarBarEqualsToken || ᴛ2 == ast.KindAmpersandAmpersandEqualsToken || ᴛ2 == ast.KindQuestionQuestionEqualsToken: {
            return c.getReferenceCandidate(node.AsBinaryExpression().Left);
        }
        case var ᴛ2 when ᴛ2 == ast.KindCommaToken: {
            return c.getReferenceCandidate(node.AsBinaryExpression().Right);
        }}

        break;
    }}

    return Ꮡnode;
}

internal static ж<ast.Node> getReferenceRoot(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (ast.IsParenthesizedExpression(parent) || ast.IsBinaryExpression(parent) && parent.AsBinaryExpression().OperatorToken.Kind == ast.KindEqualsToken && parent.AsBinaryExpression().Left == Ꮡnode || ast.IsBinaryExpression(parent) && parent.AsBinaryExpression().OperatorToken.Kind == ast.KindCommaToken && parent.AsBinaryExpression().Right == Ꮡnode) {
        return c.getReferenceRoot(parent);
    }
    return Ꮡnode;
}

internal static bool hasMatchingArgument(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡexpression, ж<ast.Node> reference) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    foreach (var (_, argument) in range(expression.Arguments())) {
        if (Ꮡc.isOrContainsMatchingReference(reference, argument) || Ꮡc.optionalChainContainsReference(argument, reference)) {
            return true;
        }
    }
    if (ast.IsPropertyAccessExpression(expression.Expression()) && Ꮡc.isOrContainsMatchingReference(reference, expression.Expression().Expression())) {
        return true;
    }
    return false;
}

internal static bool isOrContainsMatchingReference(this ж<Checker> Ꮡc, ж<ast.Node> source, ж<ast.Node> target) {
    return Ꮡc.isMatchingReference(source, target) || Ꮡc.containsMatchingReference(source, target);
}

internal static ж<Type> replacePrimitivesWithLiterals(this ж<Checker> Ꮡc, ж<Type> typeWithPrimitives, ж<Type> typeWithLiterals) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (c.maybeTypeOfKind(typeWithPrimitives, (TypeFlags)((TypeFlags)((TypeFlags)(TypeFlagsString | TypeFlagsTemplateLiteral) | TypeFlagsNumber) | TypeFlagsBigInt)) && c.maybeTypeOfKind(typeWithLiterals, (TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)(TypeFlagsStringLiteral | TypeFlagsTemplateLiteral) | TypeFlagsStringMapping) | TypeFlagsNumberLiteral) | TypeFlagsBigIntLiteral))) {
        return Ꮡc.mapType(typeWithPrimitives, (ж<Type> t) => {
            switch (ᐧ) {
            case {} when (TypeFlags)(t.flags & TypeFlagsString) != 0: {
                return Ꮡc.extractTypesOfKind(typeWithLiterals, (TypeFlags)((TypeFlags)((TypeFlags)(TypeFlagsString | TypeFlagsStringLiteral) | TypeFlagsTemplateLiteral) | TypeFlagsStringMapping));
            }
            case {} when Ꮡc.isPatternLiteralType(t) && !Ꮡc.Value.maybeTypeOfKind(typeWithLiterals, (TypeFlags)((TypeFlags)(TypeFlagsString | TypeFlagsTemplateLiteral) | TypeFlagsStringMapping)): {
                return Ꮡc.extractTypesOfKind(typeWithLiterals, TypeFlagsStringLiteral);
            }
            case {} when (TypeFlags)(t.flags & TypeFlagsNumber) != 0: {
                return Ꮡc.extractTypesOfKind(typeWithLiterals, (TypeFlags)(TypeFlagsNumber | TypeFlagsNumberLiteral));
            }
            case {} when (TypeFlags)(t.flags & TypeFlagsBigInt) != 0: {
                return Ꮡc.extractTypesOfKind(typeWithLiterals, (TypeFlags)(TypeFlagsBigInt | TypeFlagsBigIntLiteral));
            }
            default: {
                return t;
            }}

        });
    }
    return typeWithPrimitives;
}

} partial class checker_package {

internal static bool isCoercibleUnderDoubleEquals(ref Type source, ref Type target) {
    return (TypeFlags)(source.flags & ((TypeFlags)((TypeFlags)(TypeFlagsNumber | TypeFlagsString) | TypeFlagsBooleanLiteral))) != 0 && (TypeFlags)(target.flags & ((TypeFlags)((TypeFlags)(TypeFlagsNumber | TypeFlagsString) | TypeFlagsBoolean))) != 0;
}

} partial class checkerᴛChecker {

internal static bool isExhaustiveSwitchStatement(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    var links = c.switchStatementLinks.Get(node);
    if (links.exhaustiveState == ExhaustiveStateUnknown) {
        links.exhaustiveState = ExhaustiveStateComputing;
        var isExhaustive = Ꮡc.computeExhaustiveSwitchStatement(node);
        if (links.exhaustiveState == ExhaustiveStateComputing) {
            links.exhaustiveState = core.IfElse(isExhaustive, ExhaustiveStateTrue, ExhaustiveStateFalse);
        }
    } else if (links.exhaustiveState == ExhaustiveStateComputing) {
        links.exhaustiveState = ExhaustiveStateFalse;
    }
    return links.exhaustiveState == ExhaustiveStateTrue;
}

internal static bool computeExhaustiveSwitchStatement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsTypeOfExpression(node.Expression())) {
        var witnesses = c.getSwitchClauseTypeOfWitnesses(Ꮡnode);
        if (witnesses == nil) {
            return false;
        }
        var operandConstraint = Ꮡc.getBaseConstraintOrType(Ꮡc.checkExpressionCached(node.Expression().Expression()));
        var notEqualFacts = c.getNotEqualFactsFromTypeofSwitch(0, 0, witnesses);
        if ((TypeFlags)(operandConstraint.flags & TypeFlagsAnyOrUnknown) != 0) {
            return (TypeFacts)(TypeFactsAllTypeofNE & notEqualFacts) == TypeFactsAllTypeofNE;
        }
        return !someType(operandConstraint, (ж<Type> tΔ1) => Ꮡc.getTypeFacts(tΔ1, notEqualFacts) == notEqualFacts);
    }
    var t = Ꮡc.getBaseConstraintOrType(Ꮡc.checkExpressionCached(node.Expression()));
    if (!isLiteralType(t)) {
        return false;
    }
    var switchTypes = Ꮡc.getSwitchClauseTypes(Ꮡnode);
    if (len(switchTypes) == 0 || core.Some<ж<Type>>(switchTypes, isNeitherUnitTypeNorNever)) {
        return false;
    }
    return c.eachTypeContainedIn(Ꮡc.mapType(t, Ꮡc.getRegularTypeOfLiteralType), switchTypes);
}

internal static bool eachTypeContainedIn(this ref Checker c, ж<Type> Ꮡsource, slice<ж<Type>> types) {
    ref var source = ref Ꮡsource.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsUnion) != 0) {
        var typesʗ1 = types;
        return !core.Some(source.AsUnionType().types, (ж<Type> t) => !slices.Contains(typesʗ1, t));
    }
    return slices.Contains(types, Ꮡsource);
}

internal static slice<@string> getSwitchClauseTypeOfWitnesses(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var links = c.switchStatementLinks.Get(Ꮡnode);
    if (!links.witnessesComputed) {
        var clauses = node.AsSwitchStatement().CaseBlock.AsCaseBlock().Clauses.Nodes;
        var witnesses = new slice<@string>(len(clauses));
        foreach (var (i, clause) in range(clauses)) {
            if (clause.Kind == ast.KindCaseClause) {
                if (!ast.IsStringLiteralLike(clause.Expression())) {
                    witnesses = default;
                    break;
                }
                @string text = clause.Expression().Text(); if (!slices.Contains(witnesses, text)) {
                    witnesses[i] = text;
                }
            }
        }
        links.witnesses = witnesses;
        links.witnessesComputed = true;
    }
    return links.witnesses;
}

internal static TypeFacts getNotEqualFactsFromTypeofSwitch(this ref Checker c, nint start, nint end, slice<@string> witnesses) {
    TypeFacts facts = TypeFactsNone;
    foreach (var (i, witness) in range(witnesses)) {
        if ((i < start || i >= end) && witness != ""u8) {
            var (f, ok) = typeofNEFacts[witness, ꟷ];
            if (!ok) {
                f = TypeFactsTypeofNEHostObject;
            }
            facts |= (TypeFacts)(f);
        }
    }
    return facts;
}

internal static slice<ж<Type>> getSwitchClauseTypes(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var links = c.switchStatementLinks.Get(Ꮡnode);
    if (!links.switchTypesComputed) {
        var clauses = node.AsSwitchStatement().CaseBlock.AsCaseBlock().Clauses.Nodes;
        var types = new slice<ж<Type>>(len(clauses));
        foreach (var (i, clause) in range(clauses)) {
            types[i] = Ꮡc.getTypeOfSwitchClause(clause);
        }
        links.switchTypes = types;
        links.switchTypesComputed = true;
    }
    return links.switchTypes;
}

internal static ж<Type> getTypeOfSwitchClause(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡclause) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var clause = ref Ꮡclause.DerefOrNull();

    if (clause.Kind == ast.KindCaseClause) {
        return Ꮡc.getRegularTypeOfLiteralType(Ꮡc.getTypeOfExpression(clause.Expression()));
    }
    return c.neverType;
}

internal static ж<Signature> getEffectsSignature(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var links = c.signatureLinks.Get(Ꮡnode);
    var signature = links.effectsSignature;
    if (signature == nil) {
        ж<Type> funcType = default;
        if (ast.IsBinaryExpression(Ꮡnode)) {
            var rightType = Ꮡc.checkNonNullExpression(node.AsBinaryExpression().Right);
            funcType = Ꮡc.getSymbolHasInstanceMethodOfObjectType(rightType);
        } else if (ast.IsExpressionStatement(node.Parent)) {
            funcType = Ꮡc.getTypeOfDottedName(node.Expression(), nil);
        } else if (node.Expression().Kind != ast.KindSuperKeyword) {
            if (ast.IsOptionalChain(Ꮡnode)) {
                funcType = Ꮡc.checkNonNullType(Ꮡc.getOptionalExpressionType(Ꮡc.checkExpression(node.Expression()), node.Expression()), node.Expression());
            } else {
                funcType = Ꮡc.checkNonNullExpression(node.Expression());
            }
        }
        ж<Type> apparentType = default;
        if (funcType != nil) {
            apparentType = Ꮡc.getApparentType(funcType);
        }
        var signatures = Ꮡc.getSignaturesOfType(core.OrElse(apparentType, c.unknownType), SignatureKindCall);
        switch (ᐧ) {
        case {} when len(signatures) == 1 && len(signatures[0].typeParameters) == 0: {
            signature = signatures[0];
            break;
        }
        case {} when core.Some<ж<Signature>>(signatures, Ꮡc.hasTypePredicateOrNeverReturnType): {
            signature = Ꮡc.getResolvedSignature(Ꮡnode, nil, CheckModeNormal);
            break;
        }}

        if (!(signature != nil && Ꮡc.hasTypePredicateOrNeverReturnType(signature))) {
            signature = c.unknownSignature;
        }
        links.effectsSignature = signature;
    }
    if (signature == c.unknownSignature) {
        return default;
    }
    return signature;
}

internal static ж<Type> getSymbolHasInstanceMethodOfObjectType(this ж<Checker> Ꮡc, ж<Type> t) {
    @string hasInstancePropertyName = Ꮡc.getPropertyNameForKnownSymbolName("hasInstance");
    if (Ꮡc.allTypesAssignableToKind(t, TypeFlagsNonPrimitive)) {
        var hasInstanceProperty = Ꮡc.getPropertyOfType(t, hasInstancePropertyName);
        if (hasInstanceProperty != nil) {
            var hasInstancePropertyType = Ꮡc.getTypeOfSymbol(hasInstanceProperty);
            if (hasInstancePropertyType != nil && len(Ꮡc.getSignaturesOfType(hasInstancePropertyType, SignatureKindCall)) != 0) {
                return hasInstancePropertyType;
            }
        }
    }
    return default;
}

internal static @string getPropertyNameForKnownSymbolName(this ж<Checker> Ꮡc, @string symbolName) {
    ref var c = ref Ꮡc.DerefOrNull();

    var ctorType = c.getGlobalESSymbolConstructorSymbolOrNil();
    if (ctorType != nil) {
        var uniqueType = Ꮡc.getTypeOfPropertyOfType(Ꮡc.getTypeOfSymbol(ctorType), symbolName);
        if (uniqueType != nil && isTypeUsableAsPropertyName(ref (uniqueType).DerefOrNull())) {
            return getPropertyNameFromType(uniqueType);
        }
    }
    return ast.InternalSymbolNamePrefix + "@" + symbolName;
}

internal static ж<Type> getTypeOfDottedName(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<ast.Diagnostic> diagnostic) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsInWithStatement) == 0) {
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindIdentifier: {
            var symbol = c.getExportSymbolOfValueSymbolIfExported(c.getResolvedSymbol(Ꮡnode));
            return Ꮡc.getExplicitTypeOfSymbol(symbol, diagnostic);
        }
        case var ᴛ when ᴛ == ast.KindThisKeyword: {
            return Ꮡc.getExplicitThisType(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindSuperKeyword: {
            return Ꮡc.checkSuperExpression(Ꮡnode);
        }
        case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
            var t = Ꮡc.getTypeOfDottedName(node.Expression(), diagnostic);
            if (t != nil) {
                var name = node.Name();
                ж<ast.Symbol> prop = default;
                if (ast.IsPrivateIdentifier(name)) {
                    if (t.symbol != nil) {
                        prop = Ꮡc.getPropertyOfType(t, binder.GetSymbolNameForPrivateIdentifier(t.symbol, name.Text()));
                    }
                } else {
                    prop = Ꮡc.getPropertyOfType(t, name.Text());
                }
                if (prop != nil) {
                    return Ꮡc.getExplicitTypeOfSymbol(prop, diagnostic);
                }
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
            return Ꮡc.getTypeOfDottedName(node.Expression(), diagnostic);
        }}

    }
    return default;
}

internal static ж<Type> getExplicitTypeOfSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, ж<ast.Diagnostic> diagnostic) {
    GoFrame ᒐ = default;
    try {
        ref var c = ref Ꮡc.DerefOrNull();
        ref var symbol = ref Ꮡsymbol.DerefOrNull();

        Ꮡsymbol = Ꮡc.resolveSymbol(Ꮡsymbol); symbol = ref Ꮡsymbol.DerefOrNull();
        if (!Ꮡc.ᏑresolvingExplicitTypeOfSymbol.AddIfAbsent(Ꮡsymbol)) {
            return default;
        }
        defer(Ꮡc.ᏑresolvingExplicitTypeOfSymbol.Delete, Ꮡsymbol, ref ᒐ);
        if ((ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsFunction | ast.SymbolFlagsMethod) | ast.SymbolFlagsClass) | ast.SymbolFlagsValueModule))) != 0) {
            return Ꮡc.getTypeOfSymbol(Ꮡsymbol);
        }
        if ((ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsVariable | ast.SymbolFlagsProperty))) != 0) {
            if ((ast.CheckFlags)(symbol.CheckFlags & ast.CheckFlagsMapped) != 0) {
                var origin = c.mappedSymbolLinks.Get(Ꮡsymbol).syntheticOrigin;
                if (origin != nil && Ꮡc.getExplicitTypeOfSymbol(origin, diagnostic) != nil) {
                    return Ꮡc.getTypeOfSymbol(Ꮡsymbol);
                }
            }
            var declaration = symbol.ValueDeclaration;
            if (declaration != nil) {
                if (c.isDeclarationWithExplicitTypeAnnotation(declaration)) {
                    return Ꮡc.getTypeOfSymbol(Ꮡsymbol);
                }
                if (ast.IsVariableDeclaration(declaration) && ast.IsForOfStatement(declaration.Parent.Parent)) {
                    var statement = declaration.Parent.Parent;
                    var expressionType = Ꮡc.getTypeOfDottedName(statement.Expression(), nil);
                    if (expressionType != nil) {
                        IterationUse use = default;
                        if (statement.AsForInOrOfStatement().AwaitModifier != nil) {
                            use = IterationUseForAwaitOf;
                        } else {
                            use = IterationUseForOf;
                        }
                        return Ꮡc.checkIteratedTypeOrElementType(use, expressionType, c.undefinedType, nil);
                    }
                }
                if (diagnostic != nil) {
                    diagnostic.AddRelatedInfo(createDiagnosticForNode(declaration, diagnostics.X_0_needs_an_explicit_type_annotation, Ꮡc.symbolToString(Ꮡsymbol)));
                }
            }
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static bool isDeclarationWithExplicitTypeAnnotation(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.IsVariableDeclaration(Ꮡnode) || ast.IsPropertyDeclaration(Ꮡnode) || ast.IsPropertySignatureDeclaration(Ꮡnode) || ast.IsParameterDeclaration(Ꮡnode)) && node.Type() != nil || c.isExpandoPropertyFunctionWithReturnTypeAnnotation(Ꮡnode);
}

internal static bool isExpandoPropertyFunctionWithReturnTypeAnnotation(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsBinaryExpression(Ꮡnode)) {
        var expr = node.AsBinaryExpression().Right; if (ast.IsFunctionLike(expr) && expr.Type() != nil) {
            return true;
        }
    }
    return false;
}

internal static bool hasTypePredicateOrNeverReturnType(this ж<Checker> Ꮡc, ж<Signature> Ꮡsig) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var sig = ref Ꮡsig.DerefOrNull();

    return Ꮡc.getTypePredicateOfSignature(Ꮡsig) != nil || sig.declaration != nil && (TypeFlags)(core.OrElse(Ꮡc.getReturnTypeFromAnnotation(sig.declaration), c.unknownType).flags & TypeFlagsNever) != 0;
}

internal static ж<Type> getExplicitThisType(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    var container = ast.GetThisContainer(node, false, false);
    if (ast.IsFunctionLike(container)) {
        var signature = Ꮡc.getSignatureFromDeclaration(container);
        if (signature.thisParameter != nil) {
            return Ꮡc.getExplicitTypeOfSymbol(signature.thisParameter, nil);
        }
    }
    if (container.Parent != nil && ast.IsClassLike(container.Parent)) {
        var symbol = Ꮡc.getSymbolOfDeclaration(container.Parent);
        if (ast.IsStatic(container)) {
            return Ꮡc.getTypeOfSymbol(symbol);
        } else {
            return Ꮡc.getDeclaredTypeOfSymbol(symbol).AsInterfaceType().thisType;
        }
    }
    return default;
}

internal static ж<Type> getInitialType(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        return Ꮡc.getInitialTypeOfVariableDeclaration(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        return Ꮡc.getInitialTypeOfBindingElement(Ꮡnode);
    }}

    throw panic("Unhandled case in getInitialType");
}

internal static ж<Type> getInitialTypeOfVariableDeclaration(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Initializer() != nil) {
        return Ꮡc.getTypeOfInitializer(node.Initializer());
    }
    if (ast.IsForInStatement(node.Parent.Parent)) {
        return c.stringType;
    }
    if (ast.IsForOfStatement(node.Parent.Parent)) {
        var t = Ꮡc.checkRightHandSideOfForOf(node.Parent.Parent);
        if (t != nil) {
            return t;
        }
    }
    return c.errorType;
}

internal static ж<Type> getTypeOfInitializer(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (c.typeNodeLinks.Has(node)) {
        var t = c.typeNodeLinks.Get(node).resolvedType;
        if (t != nil) {
            return t;
        }
    }
    return Ꮡc.getTypeOfExpression(node);
}

internal static ж<Type> getInitialTypeOfBindingElement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var pattern = node.Parent;
    var parentType = Ꮡc.getInitialType(pattern.Parent);
    ж<Type> t = default;
    switch (ᐧ) {
    case {} when ast.IsObjectBindingPattern(pattern): {
        t = Ꮡc.getTypeOfDestructuredProperty(parentType, getBindingElementPropertyName(Ꮡnode));
        break;
    }
    case {} when !hasDotDotDotToken(Ꮡnode): {
        t = Ꮡc.getTypeOfDestructuredArrayElement(parentType, slices.Index(pattern.Elements(), Ꮡnode));
        break;
    }
    default: {
        t = Ꮡc.getTypeOfDestructuredSpreadExpression(parentType);
        break;
    }}

    return Ꮡc.getTypeWithDefault(t, node.Initializer());
}

internal static ж<Type> getAssignedType(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    switch (parent.Kind) {
    case var ᴛ when ᴛ == ast.KindForInStatement: {
        return c.stringType;
    }
    case var ᴛ when ᴛ == ast.KindForOfStatement: {
        var t = Ꮡc.checkRightHandSideOfForOf(parent);
        if (t != nil) {
            return t;
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return Ꮡc.getAssignedTypeOfBinaryExpression(parent);
    }
    case var ᴛ when ᴛ == ast.KindDeleteExpression: {
        return c.undefinedType;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        return Ꮡc.getAssignedTypeOfArrayLiteralElement(parent, Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindSpreadElement: {
        return Ꮡc.getAssignedTypeOfSpreadExpression(parent);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
        return Ꮡc.getAssignedTypeOfPropertyAssignment(parent);
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        return Ꮡc.getAssignedTypeOfShorthandPropertyAssignment(parent);
    }}

    return c.errorType;
}

internal static ж<Type> getAssignedTypeOfBinaryExpression(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var isDestructuringDefaultAssignment = ast.IsArrayLiteralExpression(node.Parent) && c.isDestructuringAssignmentTarget(node.Parent) || ast.IsPropertyAssignment(node.Parent) && c.isDestructuringAssignmentTarget(node.Parent.Parent);
    if (isDestructuringDefaultAssignment) {
        return Ꮡc.getTypeWithDefault(Ꮡc.getAssignedType(Ꮡnode), node.AsBinaryExpression().Right);
    }
    return Ꮡc.getTypeOfExpression(node.AsBinaryExpression().Right);
}

internal static ж<Type> getAssignedTypeOfArrayLiteralElement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<ast.Node> element) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡc.getTypeOfDestructuredArrayElement(Ꮡc.getAssignedType(Ꮡnode), slices.Index(node.Elements(), element));
}

internal static ж<Type> getTypeOfDestructuredArrayElement(this ж<Checker> Ꮡc, ж<Type> t, nint index) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (everyType(t, Ꮡc.isTupleLikeType)) {
        var elementType = Ꮡc.getTupleElementType(t, index); if (elementType != nil) {
            return elementType;
        }
    }
    {
        var elementType = Ꮡc.checkIteratedTypeOrElementType(IterationUseDestructuring, t, c.undefinedType, nil); if (elementType != nil) {
            return Ꮡc.includeUndefinedInIndexSignature(elementType);
        }
    }
    return c.errorType;
}

internal static ж<Type> includeUndefinedInIndexSignature(this ж<Checker> Ꮡc, ж<Type> t) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (t == nil) {
        return default;
    }
    if (c.compilerOptions.NoUncheckedIndexedAccess == core.TSTrue) {
        return Ꮡc.getUnionType([t, c.missingType]);
    }
    return t;
}

internal static ж<Type> getAssignedTypeOfSpreadExpression(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡc.getTypeOfDestructuredSpreadExpression(Ꮡc.getAssignedType(node.Parent));
}

internal static ж<Type> getTypeOfDestructuredSpreadExpression(this ж<Checker> Ꮡc, ж<Type> t) {
    ref var c = ref Ꮡc.DerefOrNull();

    var elementType = Ꮡc.checkIteratedTypeOrElementType(IterationUseDestructuring, t, c.undefinedType, nil);
    if (elementType == nil) {
        elementType = c.errorType;
    }
    return Ꮡc.createArrayType(elementType);
}

internal static ж<Type> getAssignedTypeOfPropertyAssignment(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡc.getTypeOfDestructuredProperty(Ꮡc.getAssignedType(node.Parent), node.Name());
}

internal static ж<Type> getTypeOfDestructuredProperty(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> name) {
    ref var c = ref Ꮡc.DerefOrNull();

    var nameType = Ꮡc.getLiteralTypeFromPropertyName(name);
    if (!isTypeUsableAsPropertyName(ref (nameType).DerefOrNull())) {
        return c.errorType;
    }
    @string text = getPropertyNameFromType(nameType);
    var propType = Ꮡc.getTypeOfPropertyOfType(t, text); if (propType != nil) {
        return propType;
    }
    var indexInfo = Ꮡc.getApplicableIndexInfoForName(t, text); if (indexInfo != nil) {
        return Ꮡc.includeUndefinedInIndexSignature(indexInfo.valueType);
    }
    return c.errorType;
}

internal static ж<Type> getAssignedTypeOfShorthandPropertyAssignment(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡc.getTypeWithDefault(Ꮡc.getAssignedTypeOfPropertyAssignment(Ꮡnode), node.AsShorthandPropertyAssignment().ObjectAssignmentInitializer);
}

internal static bool isDestructuringAssignmentTarget(this ref Checker c, ж<ast.Node> Ꮡparent) {
    ref var parent = ref Ꮡparent.DerefOrNull();

    return ast.IsBinaryExpression(parent.Parent) && parent.Parent.AsBinaryExpression().Left == Ꮡparent || ast.IsForOfStatement(parent.Parent) && parent.Parent.Initializer() == Ꮡparent;
}

internal static ж<Type> getTypeWithDefault(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> defaultExpression) {
    if (defaultExpression != nil) {
        return Ꮡc.getUnionType(slice<ж<Type>>([Ꮡc.getNonUndefinedType(t), Ꮡc.getTypeOfExpression(defaultExpression)]));
    }
    return t;
}

internal static ж<Type> getAssignmentReducedType(this ж<Checker> Ꮡc, ж<Type> ᏑdeclaredType, ж<Type> ᏑassignedType) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var declaredType = ref ᏑdeclaredType.DerefOrNull();
    ref var assignedType = ref ᏑassignedType.DerefOrNull();

    if (ᏑdeclaredType == ᏑassignedType) {
        return ᏑdeclaredType;
    }
    if ((TypeFlags)(assignedType.flags & TypeFlagsNever) != 0) {
        return ᏑassignedType;
    }
    var key = new AssignmentReducedKey(id1: declaredType.id, id2: assignedType.id);
    var result = c.assignmentReducedTypes[key];
    if (result == nil) {
        result = Ꮡc.getAssignmentReducedTypeWorker(ᏑdeclaredType, ᏑassignedType);
        c.assignmentReducedTypes[key] = result;
    }
    return result;
}

internal static ж<Type> getAssignmentReducedTypeWorker(this ж<Checker> Ꮡc, ж<Type> declaredType, ж<Type> ᏑassignedType) {
    ref var assignedType = ref ᏑassignedType.DerefOrNull();

    var filteredType = Ꮡc.filterType(declaredType, (ж<Type> t) => Ꮡc.typeMaybeAssignableTo(ᏑassignedType, t));
    var reducedType = filteredType;
    if ((TypeFlags)(assignedType.flags & TypeFlagsBooleanLiteral) != 0 && isFreshLiteralType(ᏑassignedType)) {
        reducedType = Ꮡc.mapType(filteredType, Ꮡc.getFreshTypeOfLiteralType);
    }
    if (Ꮡc.isTypeAssignableTo(ᏑassignedType, reducedType)) {
        return reducedType;
    }
    return declaredType;
}

internal static bool typeMaybeAssignableTo(this ж<Checker> Ꮡc, ж<Type> Ꮡsource, ж<Type> target) {
    ref var source = ref Ꮡsource.DerefOrNull();

    if ((TypeFlags)(source.flags & TypeFlagsUnion) == 0) {
        return Ꮡc.isTypeAssignableTo(Ꮡsource, target);
    }
    if (containsType(source.Types(), target)) {
        return true;
    }
    foreach (var (_, t) in range(source.Types())) {
        if (Ꮡc.isTypeAssignableTo(t, target)) {
            return true;
        }
    }
    return false;
}

internal static ж<ast.Node> getTypePredicateArgument(this ref Checker c, ж<TypePredicate> Ꮡpredicate, ж<ast.Node> ᏑcallExpression) {
    ref var predicate = ref Ꮡpredicate.DerefOrNull();
    ref var callExpression = ref ᏑcallExpression.DerefOrNull();

    if (predicate.kind == TypePredicateKindIdentifier || predicate.kind == TypePredicateKindAssertsIdentifier) {
        var arguments = callExpression.Arguments();
        if (predicate.parameterIndex >= 0 && (nint)predicate.parameterIndex < len(arguments)) {
            return arguments[predicate.parameterIndex];
        }
    } else {
        var invokedExpression = ast.SkipParentheses(callExpression.Expression());
        if (ast.IsAccessExpression(invokedExpression)) {
            return ast.SkipParentheses(invokedExpression.Expression());
        }
    }
    return default;
}

internal static ж<Type> getFlowTypeInConstructor(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> Ꮡconstructor) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    ref var constructor = ref Ꮡconstructor.DerefOrNull();

    ж<ast.Node> accessName = default;
    if (strings.HasPrefix(symbol.Name, ast.InternalSymbolNamePrefix + "#")) {
        accessName = c.factory.NewPrivateIdentifier(symbol.Name.slice(strings.Index(symbol.Name, "@") + 1));
    } else {
        accessName = c.factory.NewIdentifier(symbol.Name);
    }
    var reference = c.factory.NewPropertyAccessExpression(c.factory.NewKeywordExpression(ast.KindThisKeyword), nil, accessName, ast.NodeFlagsNone);
    reference.Expression().Parent = reference;
    reference.Parent = Ꮡconstructor;
    reference.FlowNodeData().FlowNode = constructor.AsConstructorDeclaration().ReturnFlowNode;
    var flowType = Ꮡc.getFlowTypeOfProperty(reference, Ꮡsymbol);
    if (c.noImplicitAny && (flowType == c.autoType || flowType == c.autoArrayType)) {
        Ꮡc.error(symbol.ValueDeclaration, diagnostics.Member_0_implicitly_has_an_1_type, Ꮡc.symbolToString(Ꮡsymbol), Ꮡc.TypeToString(flowType));
    }
    if (everyType(flowType, Ꮡc.IsNullableType)) {
        return default;
    }
    return c.convertAutoToAny(flowType);
}

internal static ж<Type> getFlowTypeInStaticBlocks(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, slice<ж<ast.Node>> staticBlocks) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    ж<ast.Node> accessName = default;
    if (strings.HasPrefix(symbol.Name, ast.InternalSymbolNamePrefix + "#")) {
        accessName = c.factory.NewPrivateIdentifier(symbol.Name.slice(strings.Index(symbol.Name, "@") + 1));
    } else {
        accessName = c.factory.NewIdentifier(symbol.Name);
    }
    foreach (var (_, staticBlock) in range(staticBlocks)) {
        var reference = c.factory.NewPropertyAccessExpression(c.factory.NewKeywordExpression(ast.KindThisKeyword), nil, accessName, ast.NodeFlagsNone);
        reference.Expression().Parent = reference;
        reference.Parent = staticBlock;
        reference.FlowNodeData().FlowNode = staticBlock.AsClassStaticBlockDeclaration().ReturnFlowNode;
        var flowType = Ꮡc.getFlowTypeOfProperty(reference, Ꮡsymbol);
        if (c.noImplicitAny && (flowType == c.autoType || flowType == c.autoArrayType)) {
            Ꮡc.error(symbol.ValueDeclaration, diagnostics.Member_0_implicitly_has_an_1_type, Ꮡc.symbolToString(Ꮡsymbol), Ꮡc.TypeToString(flowType));
        }
        if (everyType(flowType, Ꮡc.IsNullableType)) {
            continue;
        }
        return c.convertAutoToAny(flowType);
    }
    return default;
}

internal static bool isReachableFlowNode(this ж<Checker> Ꮡc, ж<ast.FlowNode> flow) {
    ref var c = ref Ꮡc.DerefOrNull();

    var f = c.getFlowState();
    var result = Ꮡc.isReachableFlowNodeWorker(f, flow, false);
    c.putFlowState(f);
    c.lastFlowNode = flow;
    c.lastFlowNodeReachable = result;
    return result;
}

internal static bool isReachableFlowNodeWorker(this ж<Checker> Ꮡc, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow, bool noCacheCheck) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    for (;;) {
        if (Ꮡflow == c.lastFlowNode) {
            return c.lastFlowNodeReachable;
        }
        var flags = flow.Flags;
        if ((ast.FlowFlags)(flags & ast.FlowFlagsShared) != 0) {
            if (!noCacheCheck && len(f.reduceLabels) == 0) {
                var (reachableΔ1, ok) = c.flowNodeReachable[Ꮡflow, ꟷ]; if (ok) {
                    return reachableΔ1;
                }
                var reachable = Ꮡc.isReachableFlowNodeWorker(Ꮡf, Ꮡflow, true);
                c.flowNodeReachable[Ꮡflow] = reachable;
                return reachable;
            }
            noCacheCheck = false;
        }
        switch (ᐧ) {
        case {} when (ast.FlowFlags)(flags & ((ast.FlowFlags)((ast.FlowFlags)(ast.FlowFlagsAssignment | ast.FlowFlagsCondition) | ast.FlowFlagsArrayMutation))) != 0: {
            Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsCall) != 0: {
            var signature = Ꮡc.getEffectsSignature(flow.Node); if (signature != nil) {
                var predicate = Ꮡc.getTypePredicateOfSignature(signature); if (predicate != nil && predicate.kind == TypePredicateKindAssertsIdentifier && predicate.t == nil) {
                    var arguments = flow.Node.Arguments(); if (predicate.parameterIndex >= 0 && (nint)predicate.parameterIndex < len(arguments) && c.isFalseExpression(arguments[predicate.parameterIndex])) {
                        return false;
                    }
                }
                if ((TypeFlags)(Ꮡc.getReturnTypeOfSignature(signature).flags & TypeFlagsNever) != 0) {
                    return false;
                }
            }
            Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsBranchLabel) != 0: {
            for (var list = getBranchLabelAntecedents(Ꮡflow, f.reduceLabels); list != nil; list = list.Next) {
                if (Ꮡc.isReachableFlowNodeWorker(Ꮡf, list.Flow, false)) {
                    return true;
                }
            }
            return false;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsLoopLabel) != 0: {
            if (flow.Antecedents == nil) {
                return false;
            }
            Ꮡflow = flow.Antecedents.Flow; flow = ref Ꮡflow.DerefOrNull();
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsSwitchClause) != 0: {
            var data = flow.Node.AsFlowSwitchClauseData();
            if (data.ClauseStart == data.ClauseEnd && Ꮡc.isExhaustiveSwitchStatement(data.SwitchStatement)) {
                return false;
            }
            Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsReduceLabel) != 0: {
            c.lastFlowNode = default;
            f.reduceLabels = append(f.reduceLabels, flow.Node.AsFlowReduceLabelData());
            var result = Ꮡc.isReachableFlowNodeWorker(Ꮡf, flow.Antecedent, false);
            f.reduceLabels = f.reduceLabels.slice(0, len(f.reduceLabels) - 1);
            return result;
        }
        default: {
            return (ast.FlowFlags)(flags & ast.FlowFlagsUnreachable) == 0;
        }}

    }
}

internal static bool isFalseExpression(this ref Checker c, ж<ast.Node> expr) {
    var node = ast.SkipParentheses(expr);
    if (node.Kind == ast.KindFalseKeyword) {
        return true;
    }
    if (ast.IsBinaryExpression(node)) {
        var binary = node.AsBinaryExpression();
        return binary.OperatorToken.Kind == ast.KindAmpersandAmpersandToken && (c.isFalseExpression(binary.Left) || c.isFalseExpression(binary.Right)) || binary.OperatorToken.Kind == ast.KindBarBarToken && c.isFalseExpression(binary.Left) && c.isFalseExpression(binary.Right);
    }
    return false;
}

internal static bool isPostSuperFlowNode(this ref Checker c, ж<ast.FlowNode> flow, bool noCacheCheck) {
    var f = c.getFlowState();
    var result = c.isPostSuperFlowNodeWorker(f, flow, noCacheCheck);
    c.putFlowState(f);
    return result;
}

internal static bool isPostSuperFlowNodeWorker(this ref Checker c, ж<FlowState> Ꮡf, ж<ast.FlowNode> Ꮡflow, bool noCacheCheck) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var flow = ref Ꮡflow.DerefOrNull();

    for (;;) {
        var flags = flow.Flags;
        if ((ast.FlowFlags)(flags & ast.FlowFlagsShared) != 0) {
            if (!noCacheCheck) {
                var (postSuperΔ1, ok) = c.flowNodePostSuper[Ꮡflow, ꟷ]; if (ok) {
                    return postSuperΔ1;
                }
                var postSuper = c.isPostSuperFlowNodeWorker(Ꮡf, Ꮡflow, true);
                c.flowNodePostSuper[Ꮡflow] = postSuper;
            }
            noCacheCheck = false;
        }
        switch (ᐧ) {
        case {} when (ast.FlowFlags)(flags & ((ast.FlowFlags)((ast.FlowFlags)((ast.FlowFlags)(ast.FlowFlagsAssignment | ast.FlowFlagsCondition) | ast.FlowFlagsArrayMutation) | ast.FlowFlagsSwitchClause))) != 0: {
            Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsCall) != 0: {
            if (flow.Node.Expression().Kind == ast.KindSuperKeyword) {
                return true;
            }
            Ꮡflow = flow.Antecedent; flow = ref Ꮡflow.DerefOrNull();
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsBranchLabel) != 0: {
            for (var list = getBranchLabelAntecedents(Ꮡflow, f.reduceLabels); list != nil; list = list.Next) {
                if (!c.isPostSuperFlowNodeWorker(Ꮡf, list.Flow, false)) {
                    return false;
                }
            }
            return true;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsLoopLabel) != 0: {
            Ꮡflow = flow.Antecedents.Flow; flow = ref Ꮡflow.DerefOrNull();
            break;
        }
        case {} when (ast.FlowFlags)(flags & ast.FlowFlagsReduceLabel) != 0: {
            f.reduceLabels = append(f.reduceLabels, flow.Node.AsFlowReduceLabelData());
            var result = c.isPostSuperFlowNodeWorker(Ꮡf, flow.Antecedent, false);
            f.reduceLabels = f.reduceLabels.slice(0, len(f.reduceLabels) - 1);
            return result;
        }
        default: {
            return (ast.FlowFlags)(flags & ast.FlowFlagsUnreachable) != 0;
        }}

    }
}

internal static bool isSymbolAssignedDefinitely(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    ref var c = ref Ꮡc.DerefOrNull();

    Ꮡc.ensureAssignmentsMarked(symbol);
    return c.markedAssignmentSymbolLinks.Get(symbol).hasDefiniteAssignment;
}

internal static bool isSymbolAssigned(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    ref var c = ref Ꮡc.DerefOrNull();

    Ꮡc.ensureAssignmentsMarked(symbol);
    return c.markedAssignmentSymbolLinks.Get(symbol).lastAssignmentPos != 0;
}

internal static bool isPastLastAssignment(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> Ꮡlocation) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var location = ref Ꮡlocation.DerefOrNull();

    Ꮡc.ensureAssignmentsMarked(symbol);
    var lastAssignmentPos = c.markedAssignmentSymbolLinks.Get(symbol).lastAssignmentPos;
    return lastAssignmentPos == 0 || Ꮡlocation != nil && (nint)lastAssignmentPos < location.Pos();
}

internal static void ensureAssignmentsMarked(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var parent = ast.FindAncestor(symbol.ValueDeclaration, ast.IsFunctionOrSourceFile);
    if (parent == nil) {
        return;
    }
    var links = c.nodeLinks.Get(parent);
    if ((NodeCheckFlags)(links.flags & NodeCheckFlagsAssignmentsMarked) == 0) {
        links.flags |= (NodeCheckFlags)(NodeCheckFlagsAssignmentsMarked);
        if (!Ꮡc.hasParentWithAssignmentsMarked(parent)) {
            c.markNodeAssignments(parent);
        }
    }
}

internal static bool hasParentWithAssignmentsMarked(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.FindAncestor(node.Parent, (ж<ast.Node> nodeΔ1) => ast.IsFunctionOrSourceFile(nodeΔ1) && (NodeCheckFlags)(Ꮡc.Value.nodeLinks.Get(nodeΔ1).flags & NodeCheckFlagsAssignmentsMarked) != 0) != nil;
}

internal static bool markNodeAssignmentsWorker(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        var assignmentKind = getAssignmentTargetKind(Ꮡnode);
        if (assignmentKind != AssignmentKindNone) {
            var symbol = c.getResolvedSymbol(Ꮡnode);
            if (c.isParameterOrMutableLocalVariable(symbol)) {
                var links = c.markedAssignmentSymbolLinks.Get(symbol);
                var pos = links.lastAssignmentPos; if (pos == 0 || pos != math.MaxInt32) {
                    var referencingFunction = ast.FindAncestor(Ꮡnode, ast.IsFunctionOrSourceFile);
                    var declaringFunction = ast.FindAncestor(symbol.ValueDeclaration, ast.IsFunctionOrSourceFile);
                    if (referencingFunction == declaringFunction) {
                        links.lastAssignmentPos = (int32)c.extendAssignmentPosition(Ꮡnode, symbol.ValueDeclaration);
                    } else {
                        links.lastAssignmentPos = math.MaxInt32;
                    }
                }
                if (assignmentKind == AssignmentKindDefinite) {
                    links.hasDefiniteAssignment = true;
                }
            }
        }
        return false;
    }
    case var ᴛ when ᴛ == ast.KindExportSpecifier: {
        var exportDeclaration = node.AsExportSpecifier().Parent.Parent.AsExportDeclaration();
        var name = node.PropertyNameOrName();
        if (!node.IsTypeOnly() && !(~exportDeclaration).IsTypeOnly && (~exportDeclaration).ModuleSpecifier == nil && !ast.IsStringLiteral(name)) {
            var symbol = Ꮡc.resolveEntityName(name, ast.SymbolFlagsValue, true, true, nil);
            if (symbol != nil && c.isParameterOrMutableLocalVariable(symbol)) {
                var links = c.markedAssignmentSymbolLinks.Get(symbol);
                links.lastAssignmentPos = math.MaxInt32;
            }
        }
        return false;
    }
    case var ᴛ when ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindEnumDeclaration: {
        return false;
    }}

    if (ast.IsTypeNode(Ꮡnode)) {
        return false;
    }
    return node.ForEachChild(c.markNodeAssignments);
}

internal static nint extendAssignmentPosition(this ref Checker c, ж<ast.Node> Ꮡnode, ж<ast.Node> Ꮡdeclaration) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    nint pos = node.Pos();
    while (Ꮡnode != nil && node.Pos() > declaration.Pos()) {
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindVariableStatement || ᴛ == ast.KindExpressionStatement || ᴛ == ast.KindIfStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindForStatement || ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement || ᴛ == ast.KindWithStatement || ᴛ == ast.KindSwitchStatement || ᴛ == ast.KindTryStatement || ᴛ == ast.KindClassDeclaration: {
            pos = node.End();
            break;
        }}

        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return pos;
}

} // end checkerᴛChecker
