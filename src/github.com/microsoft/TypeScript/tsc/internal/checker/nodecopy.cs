namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> reuseNode(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> node) {
    if (node == nil) {
        return node;
    }

    return Ꮡb.tryReuseExistingNodeHelper(node);
}

internal static ж<ast.Node> tryJSTypeNodeToTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> node) {
    return Ꮡb.reuseNode(node);
}

internal static ж<ast.Node> reuseName(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> node, bool isMethod) {
    ref var b = ref Ꮡb.DerefOrNull();

    var res = Ꮡb.reuseNode(node);
    if (res == nil) {
        return res;
    }

    var (text, ok) = ast.TryGetTextOfPropertyName(res);
    if (!ok) {
        return res;
    }

    propertyNameNodeKind kind = classifyPropertyName(text, ast.IsStringLiteral(res), isMethod);
    if (ast.IsIdentifier(res) && kind == propertyNameNodeKindIdentifier) {
        return res;
    }
    if (ast.IsStringLiteral(res) && kind == propertyNameNodeKindStringLiteral) {
        return res;
    }

    ж<ast.Node> renamed = default;
    switch (kind) {
    case var ᴛ when ᴛ == propertyNameNodeKindIdentifier: {
        renamed = b.newIdentifier(text, nil);
        break;
    }
    case var ᴛ when ᴛ == propertyNameNodeKindStringLiteral: {
        renamed = b.f.NewStringLiteral(text, ast.TokenFlagsNone);
        break;
    }
    default: {
        return res;
    }}

    b.e.SetOriginal(renamed, res);
    return b.setTextRange(renamed, res);
}

internal static ж<ast.Node> reuseTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> node) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (node == nil) {
        return node;
    }
    var r = Ꮡb.reuseNode(node);
    if (r != nil) {
        if (b.ctx.maxExpansionDepth >= 0 && !b.ctx.canIncreaseExpansionDepth) {
            Ꮡb.walkNodeForExpandability(node);
        }
        return r;
    }
    b.ctx.tracker.ReportInferenceFallback(node);
    var t = b.getTypeFromTypeNode(node, false);
    return Ꮡb.typeToTypeNode(t);
}

internal static void walkNodeForExpandability(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> Ꮡnode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (b.ctx.canIncreaseExpansionDepth || Ꮡnode == nil) {
        return;
    }
    if (ast.IsTypeReferenceNode(Ꮡnode) || ast.IsExpressionWithTypeArguments(Ꮡnode) || ast.IsTypePredicateNode(Ꮡnode) || ast.IsImportTypeNode(Ꮡnode)) {
        var t = b.getTypeFromTypeNode(Ꮡnode, false);
        if (t != nil) {
            Ꮡb.checkTypeExpandability(t);
            if (b.ctx.canIncreaseExpansionDepth) {
                return;
            }
        }
    }
    node.ForEachChild((ж<ast.Node> child) => {
        Ꮡb.walkNodeForExpandability(child);
        return Ꮡb.Value.ctx.canIncreaseExpansionDepth;
    });
}

} partial class checker_package {

partial struct recoveryBoundary {
    internal ж<NodeBuilderContext> ctx;
    internal bool hadError;
    internal slice<Action> deferredReports;
    internal nodebuilder.SymbolTracker oldTracker;
    internal slice<ж<TrackedSymbolArgs>> oldTrackedSymbols;
    internal slice<ж<TrackedSymbolArgs>> trackedSymbols;
    internal bool oldEncounteredError;
    internal nint oldApproximateLength;
}

} partial class checkerᴛrecoveryBoundary {

internal static void markError(this ref recoveryBoundary b, Action f) {
    b.hadError = true;
    if (f != nil) {
        b.deferredReports = append(b.deferredReports, f);
    }
}

} partial class checker_package {

partial struct originalRecoveryScopeState {
    internal nint trackedSymbolsTop;
    internal nint unreportedErrorsTop;
    internal bool hadError;
}

} partial class checkerᴛrecoveryBoundary {

internal static originalRecoveryScopeState startRecoveryScope(this ref recoveryBoundary b) {
    nint trackedSymbolsTop = len(b.ctx.trackedSymbols);
    nint unreportedErrorsTop = len(b.deferredReports);
    return new originalRecoveryScopeState(trackedSymbolsTop: trackedSymbolsTop, unreportedErrorsTop: unreportedErrorsTop, hadError: b.hadError);
}

internal static void endRecoveryScope(this ref recoveryBoundary b, originalRecoveryScopeState state) {
    b.hadError = state.hadError;
    b.ctx.trackedSymbols = b.ctx.trackedSymbols.slice(0, state.trackedSymbolsTop);
    b.deferredReports = b.deferredReports.slice(0, state.unreportedErrorsTop);
}

} partial class checker_package {

partial struct wrappingTracker {
    internal nodebuilder.SymbolTracker wrapped;
    internal ж<recoveryBoundary> bound;
}

} partial class checkerᴛwrappingTracker {

internal static void PopErrorFallbackNode(this ref wrappingTracker w) {
    w.wrapped.PopErrorFallbackNode();
}

internal static void PushErrorFallbackNode(this ref wrappingTracker w, ж<ast.Node> node) {
    w.wrapped.PushErrorFallbackNode(node);
}

internal static void ReportCyclicStructureError(this ref wrappingTracker w) {
    w.bound.markError(w.wrapped.ReportCyclicStructureError);
}

internal static void ReportInaccessibleThisError(this ref wrappingTracker w) {
    w.bound.markError(w.wrapped.ReportInaccessibleThisError);
}

internal static void ReportInaccessibleUniqueSymbolError(this ref wrappingTracker w) {
    w.bound.markError(w.wrapped.ReportInaccessibleUniqueSymbolError);
}

internal static void ReportInferenceFallback(this ref wrappingTracker w, ж<ast.Node> node) {
    w.wrapped.ReportInferenceFallback(node);
}

internal static void ReportLikelyUnsafeImportRequiredError(this ж<wrappingTracker> Ꮡw, @string specifier, @string symbolName) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.bound.markError(() => {
        Ꮡw.Value.wrapped.ReportLikelyUnsafeImportRequiredError(specifier, symbolName);
    });
}

internal static void ReportNonSerializableProperty(this ж<wrappingTracker> Ꮡw, @string propertyName) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.bound.markError(() => {
        Ꮡw.Value.wrapped.ReportNonSerializableProperty(propertyName);
    });
}

internal static void ReportNonlocalAugmentation(this ref wrappingTracker w, ж<ast.SourceFile> containingFile, ж<ast.Symbol> parentSymbol, ж<ast.Symbol> augmentingSymbol) {
    w.wrapped.ReportNonlocalAugmentation(containingFile, parentSymbol, augmentingSymbol);
}

internal static void ReportPrivateInBaseOfClassExpression(this ж<wrappingTracker> Ꮡw, @string propertyName) {
    ref var w = ref Ꮡw.DerefOrNull();

    w.bound.markError(() => {
        Ꮡw.Value.wrapped.ReportPrivateInBaseOfClassExpression(propertyName);
    });
}

internal static void ReportTruncationError(this ref wrappingTracker w) {
    w.wrapped.ReportTruncationError();
}

internal static bool TrackSymbol(this ref wrappingTracker w, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning) {
    w.bound.trackedSymbols = append(w.bound.trackedSymbols, Ꮡ(new TrackedSymbolArgs(symbol, enclosingDeclaration, meaning)));
    return false;
}

} partial class checker_package {

internal static ж<wrappingTracker> newWrappingTracker(nodebuilder.SymbolTracker inner, ж<recoveryBoundary> bound) {
    return Ꮡ(new wrappingTracker(
        wrapped: inner,
        bound: bound
    ));
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<recoveryBoundary> createRecoveryBoundary(this ref NodeBuilderImpl b) {
    b.ch.checkNotCanceled();
    var bound = Ꮡ(new recoveryBoundary(ctx: b.ctx, oldTracker: b.ctx.tracker, oldTrackedSymbols: b.ctx.trackedSymbols, oldEncounteredError: b.ctx.encounteredError, oldApproximateLength: b.ctx.approximateLength));
    var newTracker = NewSymbolTrackerImpl(b.ctx, new wrappingTrackerжSymbolTracker(newWrappingTracker(b.ctx.tracker, bound)));
    b.ctx.tracker = new SymbolTrackerImplжSymbolTracker(newTracker);
    b.ctx.trackedSymbols = default;
    return bound;
}

internal static bool finalizeBoundary(this ref NodeBuilderImpl b, ж<recoveryBoundary> Ꮡbound) {
    ref var bound = ref Ꮡbound.DerefOrNull();

    b.ctx.tracker = bound.oldTracker;
    b.ctx.trackedSymbols = bound.oldTrackedSymbols;
    b.ctx.encounteredError = bound.oldEncounteredError;
    b.ctx.approximateLength = bound.oldApproximateLength;

    foreach (var (_, f) in range(bound.deferredReports)) {
        f();
    }
    if (bound.hadError) {
        return false;
    }
    foreach (var (_, a) in range(bound.trackedSymbols)) {
        b.ctx.tracker.TrackSymbol(a.symbol, a.enclosingDeclaration, a.meaning);
    }
    return true;
}

internal static ж<astꓸTypeNode> tryReuseExistingNodeHelper(this ж<NodeBuilderImpl> Ꮡb, ж<astꓸTypeNode> Ꮡexisting) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var existing = ref Ꮡexisting.DerefOrNull();

    var bound = b.createRecoveryBoundary();
    ж<ast.Node> transformed = default;
    var v = getExistingNodeTreeVisitor(Ꮡb, bound);
    transformed = v.VisitNode(Ꮡexisting);
    if (!b.finalizeBoundary(bound)) {
        return default;
    }
    b.ctx.approximateLength += existing.Loc.End() - existing.Loc.Pos();
    return transformed;
}

internal static @string getModuleSpecifierOverride(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> Ꮡparent, ж<ast.Node> Ꮡlit) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var parent = ref Ꮡparent.DerefOrNull();
    ref var lit = ref Ꮡlit.DerefOrNull();

    if (b.ctx.enclosingFile != ast.GetSourceFileOfNode(Ꮡlit)) {
        var mode = core.ResolutionModeNone;
        if ((~parent.AsImportTypeNode()).Attributes != nil) {
            (mode, _) = (~parent.AsImportTypeNode()).Attributes.AsImportAttributes().of(ast.ImportAttributes.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).GetResolutionModeOverride(default);
        }
        @string name = lit.Text();
        @string originalName = name;
        var nodeSymbol = b.tryGetResolvedSymbolFromTypeNode(Ꮡparent);
        var meaning = ast.SymbolFlagsType;
        if (parent.AsImportTypeNode().IsTypeOf) {
            meaning = ast.SymbolFlagsValue;
        }
        ж<ast.Symbol> parentSymbol = default;
        if (nodeSymbol != nil && b.ch.IsSymbolAccessible(nodeSymbol, b.ctx.enclosingDeclaration, meaning, false).Accessibility == printer.SymbolAccessibilityAccessible) {
            parentSymbol = Ꮡb.lookupSymbolChain(nodeSymbol, meaning, true)[0];
        }
        if (parentSymbol != nil && IsExternalModuleSymbol(parentSymbol)) {
            name = Ꮡb.getSpecifierForModuleSymbol(parentSymbol, mode).specifier;
        } else {
            var targetFile = b.ch.getExternalModuleFileFromDeclaration(Ꮡparent);
            if (targetFile != nil) {
                name = Ꮡb.getSpecifierForModuleSymbol((~targetFile).Symbol, mode).specifier;
            }
        }
        if (len(name) > 0 && strings.Contains(name, "/node_modules/")) {
            b.ctx.encounteredError = true;
            b.ctx.tracker.ReportLikelyUnsafeImportRequiredError(name, "");
        }
        if (name != originalName) {
            return name;
        }
    }
    return "";
}

internal static ж<ast.Node> rewriteModuleSpecifier(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> parent, ж<ast.Node> lit) {
    ref var b = ref Ꮡb.DerefOrNull();

    @string newName = Ꮡb.getModuleSpecifierOverride(parent, lit);
    if (len(newName) == 0) {
        return lit;
    }
    var res = b.f.NewStringLiteral(newName, ast.TokenFlagsNone);
    b.e.SetOriginal(res, lit);
    return res;
}

internal static ж<ast.Node> getEnclosingDeclarationIgnoringFakeScope(this ref NodeBuilderImpl b) {
    var enc = b.ctx.enclosingDeclaration;
    while (enc != nil && b.links.Get(enc).fakeScopeForSignatureDeclaration != nil) {
        enc = enc.Parent;
    }
    return enc;
}

} partial class checker_package {

internal static ж<ast.NodeVisitor> getExistingNodeTreeVisitor(ж<NodeBuilderImpl> Ꮡb, ж<recoveryBoundary> bound) {
    ref var b = ref Ꮡb.DerefOrNull();

    ref var visitor = ref heap<ж<ast.NodeVisitor>>(out var Ꮡvisitor);
    ж<ast.Node> attachSymbolToLeftmostIdentifier(ж<ast.Node> leftmost, ж<ast.Node> node, ж<ast.Symbol> sym) {
        ref var vis = ref heap<ж<ast.NodeVisitor>>(out var Ꮡvis);
        var visitorFunc = (ж<ast.Node> nodeΔ1) => {
            if (nodeΔ1 == leftmost) {
                ж<Type> type_ = default;
                ж<ast.Node> name = default;
                if (sym != nil) {
                    type_ = Ꮡb.Value.ch.getDeclaredTypeOfSymbol(sym);
                    if ((ast.SymbolFlags)(sym.Flags & ast.SymbolFlagsTypeParameter) != 0) {
                        name = Ꮡb.typeParameterToName(type_).of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
                    }
                }
                if (name == nil) {
                    name = Ꮡb.Value.newIdentifier(nodeΔ1.Text(), sym);
                }
                name = Ꮡb.Value.setTextRange(name, nodeΔ1);
                Ꮡb.Value.e.AddEmitFlags(name, printer.EFNoAsciiEscaping);
                return name;
            }
            return Ꮡb.Value.setTextRange(nodeΔ1.VisitEachChild(Ꮡvis.ValueSlot), nodeΔ1);
        };
        Ꮡvis.ValueSlot = ast.NewNodeVisitor(visitorFunc, Ꮡb.Value.f, new ast.NodeVisitorHooks());
        return visitorFunc(node);
    }
    var attachSymbolToLeftmostIdentifierʗ1 = attachSymbolToLeftmostIdentifier;
    (bool, ж<ast.Node>, ж<ast.Symbol>) trackExistingEntityName(ж<ast.Node> node, ж<ast.Node> overrideEnclosing) {
        var enclosingDeclaration = Ꮡb.Value.ctx.enclosingDeclaration;
        if (overrideEnclosing != nil) {
            enclosingDeclaration = overrideEnclosing;
        }
        var introducesError = false;
        var leftmost = ast.GetFirstIdentifier(node);
        if (ast.IsInJSFile(node) && (ast.IsExportsIdentifier(leftmost) || ast.IsModuleExportsAccessExpression(leftmost.Parent) || (ast.IsQualifiedName(leftmost.Parent) && ast.IsModuleIdentifier(leftmost.Parent.AsQualifiedName().Left) && ast.IsExportsIdentifier(leftmost.Parent.AsQualifiedName().Right)))) {
            introducesError = true;
            return (introducesError, Ꮡb.Value.setTextRange(Ꮡb.Value.f.DeepCloneNode(node), node), default);
        }
        var meaning = getMeaningOfEntityNameReference(node);
        ж<ast.Symbol> sym = default;
        if (ast.IsThisIdentifier(leftmost)) {
            sym = Ꮡb.Value.ch.getSymbolOfDeclaration(Ꮡb.Value.ch.getThisContainer(leftmost, false, false));
            if (Ꮡb.Value.ch.IsSymbolAccessible(sym, leftmost, meaning, false).Accessibility != printer.SymbolAccessibilityAccessible) {
                introducesError = true;
                Ꮡb.Value.ctx.tracker.ReportInaccessibleThisError();
            }
            return (introducesError, attachSymbolToLeftmostIdentifierʗ1(leftmost, node, sym), default);
        }
        sym = Ꮡb.Value.ch.resolveEntityName(leftmost, meaning, true, true, nil);
        if (Ꮡb.Value.ctx.enclosingDeclaration != nil && !(sym != nil && (ast.SymbolFlags)(sym.Flags & ast.SymbolFlagsTypeParameter) != 0)) {
            sym = Ꮡb.Value.ch.getExportSymbolOfValueSymbolIfExported(sym);
            var symAtLocation = Ꮡb.Value.ch.resolveEntityName(leftmost, meaning, true, true, Ꮡb.Value.ctx.enclosingDeclaration);
            if (symAtLocation == Ꮡb.Value.ch.unknownSymbol || (symAtLocation == nil && sym != nil) || (symAtLocation != nil && sym != nil && Ꮡb.Value.ch.getSymbolIfSameReference(Ꮡb.Value.ch.getExportSymbolOfValueSymbolIfExported(symAtLocation), sym) == nil)) {
                if (symAtLocation != Ꮡb.Value.ch.unknownSymbol) {
                    Ꮡb.Value.ctx.tracker.ReportInferenceFallback(node);
                }
                introducesError = true;
                return (introducesError, Ꮡb.Value.setTextRange(Ꮡb.Value.f.DeepCloneNode(node), node), sym);
            } else {
                sym = symAtLocation;
            }
        }

        if (sym != nil) {
            if ((ast.SymbolFlags)(sym.Flags & ast.SymbolFlagsFunctionScopedVariable) != 0 && sym.ValueDeclaration != nil) {
                if (ast.IsPartOfParameterDeclaration(sym.ValueDeclaration) || ast.IsJSDocParameterTag(sym.ValueDeclaration)) {
                    return (introducesError, attachSymbolToLeftmostIdentifierʗ1(leftmost, node, sym), default);
                }
            }
            if ((ast.SymbolFlags)(sym.Flags & ast.SymbolFlagsTypeParameter) == 0 && !ast.IsDeclarationName(node) && Ꮡb.Value.ch.IsSymbolAccessible(sym, enclosingDeclaration, meaning, false).Accessibility != printer.SymbolAccessibilityAccessible) {
                Ꮡb.Value.ctx.tracker.ReportInferenceFallback(node);
                introducesError = true;
            } else {
                Ꮡb.Value.ctx.tracker.TrackSymbol(sym, enclosingDeclaration, meaning);
            }
            return (introducesError, attachSymbolToLeftmostIdentifierʗ1(leftmost, node, sym), default);
        }
        return (introducesError, Ꮡb.Value.setTextRange(Ꮡb.Value.f.DeepCloneNode(node), node), default);
    }
    ref var tryVisitSimpleTypeNode = ref heap<Func<ж<ast.Node>, ж<ast.Node>>>(out var ᏑtryVisitSimpleTypeNode);
    ж<ast.Node> tryVisitIndexedAccess(ж<ast.Node> node) {
        var resultObjectType = ᏑtryVisitSimpleTypeNode.ValueSlot(node.AsIndexedAccessTypeNode().ObjectType);
        if (resultObjectType == nil) {
            return default;
        }
        return Ꮡb.Value.setTextRange(Ꮡb.Value.f.UpdateIndexedAccessTypeNode(node.AsIndexedAccessTypeNode(), resultObjectType, Ꮡvisitor.ValueSlot.VisitNode(node.AsIndexedAccessTypeNode().IndexType)), node);
    }
    ж<ast.Node> tryVisitKeyOf(ж<ast.Node> node) {
        var to = node.AsTypeOperatorNode();
        var t = ᏑtryVisitSimpleTypeNode.ValueSlot((~to).Type);
        if (t == nil) {
            return default;
        }
        return Ꮡb.Value.setTextRange(Ꮡb.Value.f.UpdateTypeOperatorNode(to, to.Operator, t), node);
    }
    var trackExistingEntityNameʗ1 = trackExistingEntityName;
    ж<ast.Node> tryVisitTypeQuery(ж<ast.Node> node) {
        var (introducesError, exprName, _) = trackExistingEntityNameʗ1(node.AsTypeQueryNode().ExprName, nil);
        if (!introducesError) {
            return Ꮡb.Value.setTextRange(Ꮡb.Value.f.UpdateTypeQueryNode(
                node.AsTypeQueryNode(),
                exprName,
                Ꮡvisitor.ValueSlot.VisitNodes((~node.AsTypeQueryNode()).TypeArguments)), node);
        }

        var serializedName = Ꮡb.serializeTypeName(node.AsTypeQueryNode().ExprName, true, Ꮡvisitor.ValueSlot.VisitNodes((~node.AsTypeQueryNode()).TypeArguments));
        if (serializedName != nil) {
            return Ꮡb.Value.setTextRange(serializedName, node.AsTypeQueryNode().ExprName);
        }
        return default;
    }
    var trackExistingEntityNameʗ2 = trackExistingEntityName;
    ж<ast.Node> tryVisitTypeReference(ж<ast.Node> node) {
        if (ast.IsConstTypeReference(node)) {
            return default;
        }
        var s = Ꮡb.Value.tryGetResolvedSymbolFromTypeNode(node);
        if (s == nil) {
            return default;
        }
        if ((ast.SymbolFlags)(s.Flags & ast.SymbolFlagsTypeParameter) != 0) {
            var declaredType = Ꮡb.Value.ch.getDeclaredTypeOfSymbol(s);
            if (Ꮡb.Value.ctx.mapper != nil && getMappedType(declaredType, Ꮡb.Value.ctx.mapper) != declaredType) {
                return default;
            }
        }
        if (!Ꮡb.Value.canReuseExistingJSTypeNode(node, Ꮡb.Value.getTypeFromTypeNode(node, false))) {
            return default;
        }
        var (introducesError, newName, _) = trackExistingEntityNameʗ2(node.AsTypeReferenceNode().TypeName, nil);
        if (!introducesError) {
            var typeArguments = Ꮡvisitor.ValueSlot.VisitNodes((~node.AsTypeReferenceNode()).TypeArguments);
            return Ꮡb.Value.setTextRange(Ꮡb.Value.f.UpdateTypeReferenceNode(
                node.AsTypeReferenceNode(),
                newName,
                typeArguments), node);
        } else {
            var serializedName = Ꮡb.serializeTypeName(node.AsTypeReferenceNode().TypeName, false, Ꮡvisitor.ValueSlot.VisitNodes((~node.AsTypeReferenceNode()).TypeArguments));
            if (serializedName != nil) {
                return Ꮡb.Value.setTextRange(serializedName, node.AsTypeReferenceNode().TypeName);
            }
            return default;
        }
    }
    var tryVisitIndexedAccessʗ1 = tryVisitIndexedAccess;
    var tryVisitKeyOfʗ1 = tryVisitKeyOf;
    var tryVisitTypeQueryʗ1 = tryVisitTypeQuery;
    var tryVisitTypeReferenceʗ1 = tryVisitTypeReference;
    tryVisitSimpleTypeNode = (ж<ast.Node> node) => {
        var innerNode = ast.SkipParentheses(node);
        switch (innerNode.Kind) {
        case var ᴛ when ᴛ == ast.KindTypeReference: {
            return tryVisitTypeReferenceʗ1(innerNode);
        }
        case var ᴛ when ᴛ == ast.KindTypeQuery: {
            return tryVisitTypeQueryʗ1(innerNode);
        }
        case var ᴛ when ᴛ == ast.KindIndexedAccessType: {
            return tryVisitIndexedAccessʗ1(innerNode);
        }
        case var ᴛ when ᴛ == ast.KindTypeOperator: {
            if (innerNode.AsTypeOperatorNode().Operator == ast.KindKeyOfKeyword) {
                return tryVisitKeyOfʗ1(innerNode);
            }
            break;
        }}

        return Ꮡvisitor.ValueSlot.VisitNode(node);
    };
    var trackExistingEntityNameʗ3 = trackExistingEntityName;
    var tryVisitIndexedAccessʗ2 = tryVisitIndexedAccess;
    var tryVisitKeyOfʗ2 = tryVisitKeyOf;
    var tryVisitTypeQueryʗ2 = tryVisitTypeQuery;
    var tryVisitTypeReferenceʗ2 = tryVisitTypeReference;
    ж<ast.Node> visitExistingNodeTreeSymbolsWorker(ж<ast.Node> node) {
        var factory = Ꮡb.Value.f;
        if (node.Kind == ast.KindJSDocTypeExpression) {
            return Ꮡvisitor.ValueSlot.VisitNode((~node.AsJSDocTypeExpression()).Type);
        }
        if (node.Kind == ast.KindJSDocAllType) {
            return factory.NewKeywordTypeNode(ast.KindAnyKeyword);
        }
        if (node.Kind == ast.KindJSDocNullableType) {
            var unionMembers = slice<ж<ast.Node>>([
                Ꮡvisitor.ValueSlot.VisitNode((~node.AsJSDocNullableType()).Type),
                factory.NewLiteralTypeNode(factory.NewKeywordExpression(ast.KindNullKeyword))
            ]);
            return factory.NewUnionTypeNode(factory.NewNodeList(unionMembers));
        }
        if (node.Kind == ast.KindJSDocOptionalType) {
            var unionMembers = slice<ж<ast.Node>>([
                Ꮡvisitor.ValueSlot.VisitNode((~node.AsJSDocOptionalType()).Type),
                factory.NewKeywordTypeNode(ast.KindUndefinedKeyword)
            ]);
            return factory.NewUnionTypeNode(factory.NewNodeList(unionMembers));
        }
        if (node.Kind == ast.KindJSDocNonNullableType) {
            return Ꮡvisitor.ValueSlot.VisitNode((~node.AsJSDocNonNullableType()).Type);
        }
        if (node.Kind == ast.KindJSDocVariadicType) {
            return factory.NewArrayTypeNode(Ꮡvisitor.ValueSlot.VisitNode((~node.AsJSDocVariadicType()).Type));
        }
        if (node.Kind == ast.KindJSDocTypeLiteral) {
            slice<ж<ast.Node>> members = default;
            foreach (var (_, t) in range(node.AsJSDocTypeLiteral().JSDocPropertyTags)) {
                if (t.Kind != ast.KindJSDocPropertyTag && t.Kind != ast.KindJSDocParameterTag) {
                    continue;
                }
                var n = t.Name();
                ж<ast.Node> targetName = default;
                if (ast.IsIdentifier(n)) {
                    targetName = n;
                } else {
                    targetName = n.AsQualifiedName().Right;
                }
                var name = Ꮡvisitor.ValueSlot.VisitNode(targetName);
                var shouldBeOptional = t.AsJSDocParameterOrPropertyTag().IsBracketed || (t.TypeExpression() != nil && t.TypeExpression().Kind == ast.KindJSDocOptionalType);
                ж<ast.Node> question = default;
                if (shouldBeOptional) {
                    question = factory.NewToken(ast.KindQuestionToken);
                }
                var ty = Ꮡvisitor.ValueSlot.VisitNode(t.TypeExpression());

                members = append(members, factory.NewPropertySignatureDeclaration(nil, name, question, ty, nil));
            }
            return factory.NewTypeLiteralNode(factory.NewNodeList(members));
        }

        if (ast.IsTypeReferenceNode(node) && ast.IsIdentifier(node.AsTypeReferenceNode().TypeName) && (~node.AsTypeReferenceNode().TypeName.AsIdentifier()).Text == ""u8) {
            var replacement = factory.NewKeywordTypeNode(ast.KindAnyKeyword);
            Ꮡb.Value.e.SetOriginal(replacement, node);
            return replacement;
        }
        if (ast.IsThisTypeNode(node)) {
            return node;
        }
        if (ast.IsTypeParameterDeclaration(node)) {
            var (_, newName, _) = trackExistingEntityNameʗ3(node.Name(), nil);
            return factory.UpdateTypeParameterDeclaration(
                node.AsTypeParameterDeclaration(),
                Ꮡvisitor.ValueSlot.VisitModifiers(node.Modifiers()),
                newName,
                Ꮡvisitor.ValueSlot.VisitNode(node.AsTypeParameterDeclaration().Constraint),
                Ꮡvisitor.ValueSlot.VisitNode((~node.AsTypeParameterDeclaration()).Expression),
                Ꮡvisitor.ValueSlot.VisitNode(node.AsTypeParameterDeclaration().DefaultType));
        }
        if (ast.IsIndexedAccessTypeNode(node)) {
            var result = tryVisitIndexedAccessʗ2(node);
            if (result != nil) {
                return result;
            }
            bound.Value.markError(default);
            return node;
        }
        if (ast.IsTypeReferenceNode(node)) {
            var result = tryVisitTypeReferenceʗ2(node);
            if (result != nil) {
                return result;
            }
            bound.Value.markError(default);
            return node;
        }
        if (ast.IsTypeQueryNode(node)) {
            var result = tryVisitTypeQueryʗ2(node);
            if (result != nil) {
                return result;
            }
            bound.Value.markError(default);
            return node;
        }
        if (ast.IsTypeOperatorNode(node)) {
            if (node.AsTypeOperatorNode().Operator == ast.KindUniqueKeyword && (~node.AsTypeOperatorNode()).Type.Kind == ast.KindSymbolKeyword) {
                var nonFakeEnclosing = Ꮡb.Value.getEnclosingDeclarationIgnoringFakeScope();
                var nonFakeEnclosingʗ1 = nonFakeEnclosing;
                var sameScope = ast.FindAncestor(node, (ж<ast.Node> a) => a == nonFakeEnclosingʗ1);
                if (sameScope == nil) {
                    bound.Value.markError(default);
                    return node;
                }
            } else if (node.AsTypeOperatorNode().Operator == ast.KindKeyOfKeyword) {
                var result = tryVisitKeyOfʗ2(node);
                if (result != nil) {
                    return result;
                }
                bound.Value.markError(default);
                return node;
            }
        }
        if (ast.IsLiteralImportTypeNode(node)) {
            if ((~node.AsImportTypeNode()).Attributes != nil && (~node.AsImportTypeNode()).Attributes.AsImportAttributes().Token == ast.KindAssertKeyword) {
                bound.Value.markError(default);
                return node;
            }
            var t = Ꮡb.Value.getTypeFromTypeNode(node, true);
            if (t == nil) {
                bound.Value.markError(default);
                return node;
            }
            if (ast.IsInJSFile(node)) {
            }
            var originalSpec = node.AsImportTypeNode().Argument.AsLiteralTypeNode().Literal;
            var specifier = Ꮡb.rewriteModuleSpecifier(node, originalSpec);
            if (originalSpec == specifier) {
                specifier = Ꮡvisitor.ValueSlot.VisitNode(specifier);
            }
            var arg = node.AsImportTypeNode().Argument;
            if (specifier != originalSpec) {
                arg = factory.NewLiteralTypeNode(specifier);
            }
            return factory.UpdateImportTypeNode(
                node.AsImportTypeNode(),
                node.AsImportTypeNode().IsTypeOf,
                arg,
                Ꮡvisitor.ValueSlot.VisitNode((~node.AsImportTypeNode()).Attributes),
                Ꮡvisitor.ValueSlot.VisitNode(node.AsImportTypeNode().Qualifier),
                Ꮡvisitor.ValueSlot.VisitNodes((~node.AsImportTypeNode()).TypeArguments));
        }
        if (node.Name() != nil && node.Name().Kind == ast.KindComputedPropertyName && !Ꮡb.Value.ch.hasLateBindableName(node)) {
            if (!ast.HasDynamicName(node)) {
                return Ꮡvisitor.ValueSlot.VisitEachChild(node);
            }
            var shouldRemoveDeclaration = !(((nodebuilder.InternalFlags)(Ꮡb.Value.ctx.internalFlags & nodebuilder.InternalFlagsAllowUnresolvedNames) != 0) && ast.IsEntityNameExpression((~node.Name().AsComputedPropertyName()).Expression) && ((TypeFlags)(Ꮡb.Value.ch.checkComputedPropertyName(node.Name()).flags & TypeFlagsAny) != 0));
            if (shouldRemoveDeclaration) {
                return default;
            }
        }
        if ((ast.IsFunctionLike(node) && node.Type() == nil) || (ast.IsPropertyDeclaration(node) && node.Type() == nil && node.Initializer() == nil) || (ast.IsPropertySignatureDeclaration(node) && node.Type() == nil && node.Initializer() == nil) || (ast.IsParameterDeclaration(node) && node.Type() == nil && node.Initializer() == nil)) {
            var visited = Ꮡvisitor.ValueSlot.VisitEachChild(node);
            if (visited == node) {
                visited = Ꮡb.Value.setTextRange(node.Clone(new ast.NodeFactoryжNodeFactoryCoercible(factory)), node);
            }
            node = visited;
            var newType = factory.NewKeywordTypeNode(ast.KindAnyKeyword);
            switch (node.Kind) {
            case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
                return factory.UpdatePropertyDeclaration(
                    node.AsPropertyDeclaration(),
                    node.Modifiers(),
                    node.Name(),
                    node.PostfixToken(),
                    newType,
                    nil);
            }
            case var ᴛ when ᴛ == ast.KindPropertySignature: {
                return factory.UpdatePropertySignatureDeclaration(
                    node.AsPropertySignatureDeclaration(),
                    node.Modifiers(),
                    node.Name(),
                    node.PostfixToken(),
                    newType,
                    nil);
            }
            case var ᴛ when ᴛ == ast.KindParameter: {
                return factory.UpdateParameterDeclaration(
                    node.AsParameterDeclaration(),
                    nil,
                    node.AsParameterDeclaration().DotDotDotToken,
                    node.Name(),
                    (~node.AsParameterDeclaration()).QuestionToken,
                    newType,
                    nil);
            }
            case var ᴛ when ᴛ == ast.KindMethodSignature: {
                return factory.UpdateMethodSignatureDeclaration(
                    node.AsMethodSignatureDeclaration(),
                    node.Modifiers(),
                    node.Name(),
                    (~node.AsMethodSignatureDeclaration()).PostfixToken,
                    (~node.AsMethodSignatureDeclaration()).TypeParameters,
                    (~node.AsMethodSignatureDeclaration()).Parameters,
                    newType);
            }
            case var ᴛ when ᴛ == ast.KindCallSignature: {
                return factory.UpdateCallSignatureDeclaration(
                    node.AsCallSignatureDeclaration(),
                    (~node.AsCallSignatureDeclaration()).TypeParameters,
                    (~node.AsCallSignatureDeclaration()).Parameters,
                    newType);
            }
            case var ᴛ when ᴛ == ast.KindJSDocSignature: {
                return factory.UpdateJSDocSignature(
                    node.AsJSDocSignature(),
                    (~node.AsJSDocSignature()).TypeParameters,
                    (~node.AsJSDocSignature()).Parameters,
                    newType);
            }
            case var ᴛ when ᴛ == ast.KindConstructSignature: {
                return factory.UpdateConstructSignatureDeclaration(
                    node.AsConstructSignatureDeclaration(),
                    (~node.AsConstructSignatureDeclaration()).TypeParameters,
                    (~node.AsConstructSignatureDeclaration()).Parameters,
                    newType);
            }
            case var ᴛ when ᴛ == ast.KindIndexSignature: {
                return factory.UpdateIndexSignatureDeclaration(
                    node.AsIndexSignatureDeclaration(),
                    node.Modifiers(),
                    (~node.AsIndexSignatureDeclaration()).Parameters,
                    newType);
            }
            case var ᴛ when ᴛ == ast.KindFunctionType: {
                return factory.UpdateFunctionTypeNode(
                    node.AsFunctionTypeNode(),
                    (~node.AsFunctionTypeNode()).TypeParameters,
                    (~node.AsFunctionTypeNode()).Parameters,
                    newType);
            }
            case var ᴛ when ᴛ == ast.KindConstructorType: {
                return factory.UpdateConstructorTypeNode(
                    node.AsConstructorTypeNode(),
                    node.Modifiers(),
                    (~node.AsConstructorTypeNode()).TypeParameters,
                    (~node.AsConstructorTypeNode()).Parameters,
                    newType);
            }}

        }
        if (ast.IsComputedPropertyName(node) && ast.IsEntityNameExpression((~node.AsComputedPropertyName()).Expression)) {
            var (introducesError, result, _) = trackExistingEntityNameʗ3((~node.AsComputedPropertyName()).Expression, nil);
            if (!introducesError) {
                return factory.UpdateComputedPropertyName(node.AsComputedPropertyName(), result);
            } else {
                bound.Value.markError(default);
                return Ꮡvisitor.ValueSlot.VisitEachChild(node);
            }
        }
        if (ast.IsTypePredicateNode(node)) {
            ж<ast.Node> parameterName = default;
            if (ast.IsIdentifier(node.AsTypePredicateNode().ParameterName)) {
                var (introducesError, result, _) = trackExistingEntityNameʗ3(node.AsTypePredicateNode().ParameterName, nil);
                if (introducesError) {
                    bound.Value.markError(default);
                }
                parameterName = result;
            } else {
                parameterName = node.AsTypePredicateNode().ParameterName.Clone(new ast.NodeFactoryжNodeFactoryCoercible(factory));
            }
            return factory.UpdateTypePredicateNode(
                node.AsTypePredicateNode(),
                Ꮡvisitor.ValueSlot.VisitNode(node.AsTypePredicateNode().AssertsModifier),
                parameterName,
                Ꮡvisitor.ValueSlot.VisitNode((~node.AsTypePredicateNode()).Type));
        }
        if (ast.IsConditionalTypeNode(node)) {
            var checkType = Ꮡvisitor.ValueSlot.VisitNode(node.AsConditionalTypeNode().CheckType);
            var dispose = Ꮡb.enterNewScope(node, default, Ꮡb.Value.ch.getInferTypeParameters(node), default, nil);
            var extendsType = Ꮡvisitor.ValueSlot.VisitNode(node.AsConditionalTypeNode().ExtendsType);
            var trueType = Ꮡvisitor.ValueSlot.VisitNode(node.AsConditionalTypeNode().TrueType);
            dispose();
            var falseType = Ꮡvisitor.ValueSlot.VisitNode(node.AsConditionalTypeNode().FalseType);
            return factory.UpdateConditionalTypeNode(
                node.AsConditionalTypeNode(),
                checkType,
                extendsType,
                trueType,
                falseType);
        }

        if (ast.IsTupleTypeNode(node) || ((nodebuilder.Flags)(Ꮡb.Value.ctx.flags & nodebuilder.FlagsMultilineObjectLiterals) == 0 && ast.IsTypeLiteralNode(node)) || ast.IsMappedTypeNode(node)) {
            var res = Ꮡvisitor.ValueSlot.VisitEachChild(node);
            if (res == node) {
                res = res.Clone(new ast.NodeFactoryжNodeFactoryCoercible(factory));
                res = Ꮡb.Value.setTextRange(res, node);
            }
            Ꮡb.Value.e.AddEmitFlags(res, printer.EFSingleLine);
            return res;
        }

        if (ast.IsStringLiteralLike(node)) {
            var c = node.Clone(new ast.NodeFactoryжNodeFactoryCoercible(Ꮡb.Value.f));
            if (ast.IsStringLiteral(node) && (nodebuilder.Flags)(Ꮡb.Value.ctx.flags & nodebuilder.FlagsUseSingleQuotesForStringLiteralType) != 0 && (ast.TokenFlags)(node.AsStringLiteral().TokenFlags & ast.TokenFlagsSingleQuote) == 0) {
                c.AsStringLiteral().TokenFlags ^= (ast.TokenFlags)(ast.TokenFlagsSingleQuote);
            }
            Ꮡb.Value.e.AddEmitFlags(c, printer.EFNoAsciiEscaping);
            return c;
        }

        return Ꮡvisitor.ValueSlot.VisitEachChild(node);
    }
    var nonLocalNode = true;
    var visitExistingNodeTreeSymbolsWorkerʗ1 = visitExistingNodeTreeSymbolsWorker;


    visitor = ast.NewNodeVisitor((ж<ast.Node> node) => {
        if (bound.Value.hadError) {
            return node;
        }
        var recover_ = bound.Value.startRecoveryScope();
        var introducesNewScope = ast.IsFunctionLike(node) || ast.IsMappedTypeNode(node);
        Action exit = default;
        if (introducesNewScope) {
            slice<ж<ast.Symbol>> @params = default;
            slice<ж<Type>> typeParams = default;
            if (ast.IsFunctionLike(node)) {
                var sig = Ꮡb.Value.ch.getSignatureFromDeclaration(node);
                @params = sig.parameters;
                typeParams = sig.typeParameters;
            } else if (ast.IsConditionalTypeNode(node)) {
                typeParams = Ꮡb.Value.ch.getInferTypeParameters(node);
            } else if (ast.IsMappedTypeNode(node)) {
                typeParams = slice<ж<Type>>([Ꮡb.Value.ch.getDeclaredTypeOfTypeParameter(Ꮡb.Value.ch.getSymbolOfDeclaration(node.AsMappedTypeNode().TypeParameter))]);
            }
            exit = Ꮡb.enterNewScope(node, @params, typeParams, default, nil);
        }
        var result = visitExistingNodeTreeSymbolsWorkerʗ1(node);
        if (exit != nil) {
            exit();
        }

        if (result == node && !ast.NodeIsSynthesized(node)) {
            result = Ꮡb.Value.f.DeepCloneNode(node);
        }

        result = Ꮡb.Value.setTextRange(result, node);

        if (bound.Value.hadError) {
            if (ast.IsTypeNode(node) && !ast.IsTypePredicateNode(node)) {
                bound.Value.endRecoveryScope(recover_);
                var t = Ꮡb.Value.getTypeFromTypeNode(node, false);
                return Ꮡb.typeToTypeNode(t);
            }
            return Ꮡb.Value.setTextRange(node.Clone(new ast.NodeFactoryжNodeFactoryCoercible(Ꮡb.Value.f)), node);
        }

        return result;
    }, b.f, new ast.NodeVisitorHooks(
        VisitNodes: (ж<ast.NodeList> nodes, ж<ast.NodeVisitor> v) => {
            var res = v.VisitNodes(nodes);
            if (nonLocalNode && res != nil) {
                if (res == nodes) {
                    res = nodes.Clone(new ast.NodeFactoryжNodeFactoryCoercible(Ꮡb.Value.f));
                }
                res.Loc = core.NewTextRange(-1, -1);
            }
            return res;
        },
        VisitNode: (ж<ast.Node> node, ж<ast.NodeVisitor> v) => {
            var oldNonLocalNode = nonLocalNode;
            nonLocalNode = Ꮡb.Value.ctx.enclosingFile == nil || Ꮡb.Value.ctx.enclosingFile != ast.GetSourceFileOfNode(Ꮡb.Value.e.MostOriginal(node));
            var res = v.VisitNode(node);
            nonLocalNode = oldNonLocalNode;
            return res;
        }
    ));
    return visitor;
}

} // end checker_package
