namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using maps = maps_package;
using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using modulespecifiers = go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using pseudochecker = go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using static checker_package;

partial class checker_package {

partial struct CompositeSymbolIdentity {
    internal bool isConstructorNode;
    internal ast.SymbolId symbolId;
    internal ast.NodeId nodeId;
}

partial struct TrackedSymbolArgs {
    internal ж<ast.Symbol> symbol;
    internal ж<ast.Node> enclosingDeclaration;
    internal ast.SymbolFlags meaning;
}

partial struct SerializedTypeEntry {
    internal ж<ast.Node> node;
    internal bool truncating;
    internal nint addedLength;
    internal slice<ж<TrackedSymbolArgs>> trackedSymbols;
}

partial struct CompositeTypeCacheIdentity {
    internal TypeId typeId;
    internal nodebuilder.Flags flags;
    internal nodebuilder.InternalFlags internalFlags;
}

partial struct NodeBuilderLinks {
    internal map<CompositeTypeCacheIdentity, ж<SerializedTypeEntry>> serializedTypes;
    internal ж<@string> fakeScopeForSignatureDeclaration;
}

partial struct NodeBuilderSymbolLinks {
    internal module.ModeAwareCache<moduleSpecifierResult> specifierCache;
}

partial struct moduleSpecifierResult {
    internal @string specifier;
    internal ж<Type> importAttributesType;
}

partial struct NodeBuilderContext {
    internal Host host;
    internal nodebuilder.SymbolTracker tracker;
    internal nint approximateLength;
    internal nint maxTruncationLength;
    internal bool encounteredError;
    internal bool truncating;
    internal bool reportedDiagnostic;
    internal nodebuilder.Flags flags;
    internal nodebuilder.InternalFlags internalFlags;
    internal nint depth;
    internal nint maxExpansionDepth;
    internal slice<ж<Type>> typeStack;
    internal bool canIncreaseExpansionDepth;
    internal bool expansionTruncated;
    internal ж<ast.Node> enclosingDeclaration;
    internal ж<ast.SourceFile> enclosingFile;
    internal slice<ж<Type>> inferTypeParameters;
    internal collections.Set<TypeId> visitedTypes;
    internal map<CompositeSymbolIdentity, nint> symbolDepth;
    internal slice<ж<TrackedSymbolArgs>> trackedSymbols;
    internal ж<TypeMapper> mapper;
    internal slice<ж<ast.Symbol>> reverseMappedStack;
    internal map<ast.SymbolId, ж<Type>> enclosingSymbolTypes;
    internal bool suppressReportInferenceFallback;
    internal map<ast.SymbolId, ж<ast.Symbol>> remappedSymbolReferences;
    internal collections.CopyOnWriteMap<TypeId, ж<ast.Identifier>> typeParameterNames;
    internal collections.CopyOnWriteSet<@string> typeParameterNamesByText;
    internal collections.CopyOnWriteMap<@string, nint> typeParameterNamesByTextNextNameCount;
    internal collections.CopyOnWriteSet<ast.SymbolId> typeParameterSymbolList;
}

partial struct NodeBuilderImpl {
    internal ж<ast.NodeFactory> f;
    internal ж<Checker> ch;
    internal ж<printer.EmitContext> e;
    internal ж<pseudochecker.PseudoChecker> pc;
    internal core.LinkStore<ж<ast.Node>, NodeBuilderLinks> links;
    internal core.LinkStore<ж<ast.Symbol>, NodeBuilderSymbolLinks> symbolLinks;
    internal ж<NodeBuilderContext> ctx;
    internal ж<ast.NodeVisitor> cloneBindingNameVisitor;
    internal map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> idToSymbol;
}

internal static UntypedInt defaultMaximumTruncationLength => 160;
internal static UntypedInt noTruncationMaximumTruncationLength => 1_000_000;

internal static ж<NodeBuilderImpl> newNodeBuilderImpl(ж<Checker> Ꮡch, ж<printer.EmitContext> Ꮡe, map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> idToSymbol) {
    ref var ch = ref Ꮡch.DerefOrNull();
    ref var e = ref Ꮡe.DerefOrNull();

    if (idToSymbol == nil) {
        idToSymbol = new map<ж<astꓸIdentifierNode>, ж<ast.Symbol>>();
    }
    var b = Ꮡ(new NodeBuilderImpl(f: e.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory(), ch: Ꮡch, e: Ꮡe, idToSymbol: idToSymbol, pc: pseudochecker.NewPseudoChecker(ch.strictNullChecks, ch.exactOptionalPropertyTypes)));
    b.cloneBindingNameVisitor = ast.NewNodeVisitor(b.cloneBindingName, b.f, new ast.NodeVisitorHooks());
    return b;
}

} partial class checkerᴛNodeBuilderImpl {

internal static Action saveRestoreFlags(this ж<NodeBuilderImpl> Ꮡb) {
    ref var b = ref Ꮡb.DerefOrNull();

    var flags = b.ctx.flags;
    var internalFlags = b.ctx.internalFlags;
    nint depth = b.ctx.depth;

    return () => {
        Ꮡb.Value.ctx.flags = flags;
        Ꮡb.Value.ctx.internalFlags = internalFlags;
        Ꮡb.Value.ctx.depth = depth;
    };
}

internal static bool checkTruncationLength(this ref NodeBuilderImpl b) {
    if (b.ctx.truncating) {
        return b.ctx.truncating;
    }
    nint maxLength = default;
    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTruncation) != 0) {
        maxLength = noTruncationMaximumTruncationLength;
    } else if (b.ctx.maxTruncationLength > 0) {
        maxLength = b.ctx.maxTruncationLength;
    } else {
        maxLength = defaultMaximumTruncationLength;
    }
    b.ctx.truncating = b.ctx.approximateLength > maxLength;
    return b.ctx.truncating;
}

internal static bool checkTruncationLengthIfExpanding(this ref NodeBuilderImpl b) {
    if (b.ctx.maxExpansionDepth >= 0 && b.checkTruncationLength()) {
        b.ctx.expansionTruncated = true;
        return true;
    }
    return false;
}

internal static bool isExpandableType(this ref NodeBuilderImpl b, ж<Type> Ꮡt, bool isAlias) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (isAlias) {
        return !b.ch.IsLibSymbolForHoverVerbosity(t.alias.Symbol());
    }
    if (b.ch.IsLibTypeForHoverVerbosity(Ꮡt)) {
        return false;
    }
    var objectFlags = t.objectFlags;
    if ((TypeFlags)(t.flags & TypeFlagsEnumLike) != 0 || (ObjectFlags)(objectFlags & ObjectFlagsReference) != 0 || (ObjectFlags)(objectFlags & ObjectFlagsClassOrInterface) != 0) {
        return true;
    }
    if ((ObjectFlags)(objectFlags & ObjectFlagsAnonymous) != 0 && t.symbol != nil && (ast.SymbolFlags)(t.symbol.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsClass | ast.SymbolFlagsEnum) | ast.SymbolFlagsValueModule) | ast.SymbolFlagsFunction) | ast.SymbolFlagsMethod))) != 0) {
        return true;
    }
    return false;
}

internal static bool isTypeOnStack(this ref NodeBuilderImpl b, ж<Type> t) {
    foreach (var i in range(len(b.ctx.typeStack) - 1)) {
        if (b.ctx.typeStack[i] == t) {
            return true;
        }
    }
    return false;
}

internal static bool shouldExpandType(this ref NodeBuilderImpl b, ж<Type> t, bool isAlias) {
    if (b.ctx.maxExpansionDepth < 0) {
        return false;
    }
    if (!b.isExpandableType(t, isAlias)) {
        return false;
    }
    if (b.isTypeOnStack(t)) {
        return false;
    }
    if (b.ctx.depth < b.ctx.maxExpansionDepth) {
        return true;
    }
    b.ctx.canIncreaseExpansionDepth = true;
    return false;
}

internal static bool isActivelyExpanding(this ref NodeBuilderImpl b) {
    return b.ctx.maxExpansionDepth > 0 && b.ctx.depth < b.ctx.maxExpansionDepth;
}

internal static void checkTypeExpandability(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt) {
    GoFrame ᒐ = default;
    try {
        ref var b = ref Ꮡb.DerefOrNull();
        ref var t = ref Ꮡt.DerefOrNull();

        if (b.ctx.maxExpansionDepth < 0 || Ꮡt == nil || b.ctx.canIncreaseExpansionDepth) {
            return;
        }
        b.ctx.typeStack = append(b.ctx.typeStack, Ꮡt);
        defer(() => {
            Ꮡb.Value.ctx.typeStack = Ꮡb.Value.ctx.typeStack.slice(0, len(Ꮡb.Value.ctx.typeStack) - 1);
        }, ref ᒐ);
        if (b.isTypeOnStack(Ꮡt)) {
            return;
        }
        if (t.alias != nil) {
            b.shouldExpandType(Ꮡt, true);
        }
        if (!b.ctx.canIncreaseExpansionDepth) {
            b.shouldExpandType(Ꮡt, false);
        }
        if (b.ctx.canIncreaseExpansionDepth) {
            return;
        }
        if ((ObjectFlags)(t.objectFlags & ObjectFlagsReference) != 0) {
            foreach (var (_, arg) in range(b.ch.getTypeArguments(Ꮡt))) {
                Ꮡb.checkTypeExpandability(arg);
                if (b.ctx.canIncreaseExpansionDepth) {
                    return;
                }
            }
        }
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static ж<astꓸTypeNode> appendReferenceToType(this ref NodeBuilderImpl b, ж<astꓸTypeNode> Ꮡroot, ж<astꓸTypeNode> Ꮡref) {
    ref var root = ref Ꮡroot.DerefOrNull();
    ref var @ref = ref Ꮡref.DerefOrNull();

    if (ast.IsImportTypeNode(Ꮡroot)) {
        var imprt = root.AsImportTypeNode();
        var ids = getAccessStack(Ꮡref);
        var qualifier = root.AsImportTypeNode().Qualifier;
        foreach (var (_, id) in range(ids)) {
            if (qualifier != nil) {
                qualifier = b.f.NewQualifiedName(qualifier, id);
            } else {
                qualifier = id;
            }
        }
        return b.f.UpdateImportTypeNode(imprt, imprt.IsTypeOf, imprt.Argument, (~imprt).Attributes, qualifier, @ref.TypeArgumentList());
    } else if (ast.IsTypeReferenceNode(Ꮡroot)) {
        var typeRef = root.AsTypeReferenceNode();
        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseInstantiationExpressions) != 0 && (~typeRef).TypeArguments != nil && len((~typeRef).TypeArguments.Nodes) != 0) {
            var exprΔ1 = b.createExpressionWithTypeArguments(b.createAccessExpression(typeRef.TypeName), (~typeRef).TypeArguments);
            foreach (var (_, id) in range(getAccessStack(Ꮡref))) {
                exprΔ1 = b.f.NewPropertyAccessExpression(exprΔ1, nil, id, ast.NodeFlagsNone);
            }
            return exprΔ1;
        }
        ж<ast.Node> typeName = typeRef.TypeName;
        foreach (var (_, id) in range(getAccessStack(Ꮡref))) {
            typeName = b.f.NewQualifiedName(typeName, id);
        }
        return b.f.UpdateTypeReferenceNode(root.AsTypeReferenceNode(), typeName, @ref.TypeArgumentList());
    }
    var expr = b.createAccessExpression(Ꮡroot);
    foreach (var (_, id) in range(getAccessStack(Ꮡref))) {
        expr = b.f.NewPropertyAccessExpression(expr, nil, id, ast.NodeFlagsNone);
    }
    return expr;
}

} partial class checker_package {

internal static slice<ж<ast.Node>> getAccessStack(ж<ast.Node> Ꮡref) {
    ref var @ref = ref Ꮡref.DerefOrNull();

    ж<ast.Node> state = @ref.AsTypeReferenceNode().TypeName;
    var ids = slice<ж<ast.Node>>([]);
    while (!ast.IsIdentifier(state)) {
        var entity = state.AsQualifiedName();
        ids = appendꓸꓸꓸ(slice<ж<ast.Node>>([entity.Right]), ids);
        state = entity.Left;
    }
    ids = appendꓸꓸꓸ(slice<ж<ast.Node>>([state]), ids);
    return ids;
}

internal static bool isClassInstanceSide(ж<Checker> c, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return t.symbol != nil && (ast.SymbolFlags)(t.symbol.Flags & ast.SymbolFlagsClass) != 0 && (Ꮡt == c.getDeclaredTypeOfClassOrInterface(t.symbol) || ((TypeFlags)(t.flags & TypeFlagsObject) != 0 && (ObjectFlags)(t.objectFlags & (uint32)ObjectFlagsIsClassInstanceClone) != 0));
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<astꓸTypeNode> createElidedInformationPlaceholder(this ref NodeBuilderImpl b) {
    b.ctx.approximateLength += 3;
    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTruncation) == 0) {
        return b.f.NewTypeReferenceNode(b.f.NewIdentifier("..."), nil);
    }
    return b.e.AddSyntheticLeadingComment(b.f.NewKeywordTypeNode(ast.KindAnyKeyword), ast.KindMultiLineCommentTrivia, "elided", false);
}

} partial class checker_package {

internal partial struct mapToTypeNodes_seenName /*dyn*/ {
    internal ж<Type> t;
    internal nint i;
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.NodeList> mapToTypeNodes(this ж<NodeBuilderImpl> Ꮡb, slice<ж<Type>> list, bool isBareList) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (len(list) == 0) {
        return default;
    }

    if (b.checkTruncationLength()) {
        if (!isBareList) {
            ж<ast.Node> node = default;
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTruncation) != 0) {
                node = b.e.AddSyntheticLeadingComment(b.f.NewKeywordTypeNode(ast.KindAnyKeyword), ast.KindMultiLineCommentTrivia, "elided", false);
            } else {
                node = b.f.NewTypeReferenceNode(b.f.NewIdentifier("..."), nil);
            }
            return b.f.NewNodeList([node]);
        } else if (len(list) > 2) {
            var nodes = slice<ж<ast.Node>>([
                Ꮡb.typeToTypeNode(list[0]),
                default,
                Ꮡb.typeToTypeNode(list[len(list) - 1])
            ]);

            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTruncation) != 0) {
                nodes[1] = b.e.AddSyntheticLeadingComment(b.f.NewKeywordTypeNode(ast.KindAnyKeyword), ast.KindMultiLineCommentTrivia, fmt.Sprintf("... %d more elided ..."u8, len(list) - 2), false);
            } else {
                @string text = fmt.Sprintf("... %d more ..."u8, len(list) - 2);
                nodes[1] = b.f.NewTypeReferenceNode(b.f.NewIdentifier(text), nil);
            }
            return b.f.NewNodeList(nodes);
        }
    }

    var mayHaveNameCollisions = (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseFullyQualifiedType) == 0;
    ж<collections.MultiMap<@string, mapToTypeNodes_seenName>> seenNames = default;
    if (mayHaveNameCollisions) {
        seenNames = Ꮡ(new collections.MultiMap<@string, mapToTypeNodes_seenName>());
    }

    var result = new slice<ж<ast.Node>>(0, len(list));

    foreach (var (i, t) in range(list)) {
        nint displayIndex = i + 1;
        if (b.checkTruncationLength() && (displayIndex + 2 < len(list) - 1)) {
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTruncation) != 0) {
                result = append(result, b.e.AddSyntheticLeadingComment(b.f.NewKeywordTypeNode(ast.KindAnyKeyword), ast.KindMultiLineCommentTrivia, fmt.Sprintf("... %d more elided ..."u8, len(list) - displayIndex), false));
            } else {
                @string text = fmt.Sprintf("... %d more ..."u8, len(list) - displayIndex);
                result = append(result, b.f.NewTypeReferenceNode(b.f.NewIdentifier(text), nil));
            }
            var typeNodeΔ1 = Ꮡb.typeToTypeNode(list[len(list) - 1]);
            if (typeNodeΔ1 != nil) {
                result = append(result, typeNodeΔ1);
            }
            break;
        }
        b.ctx.approximateLength += 2;
        var typeNode = Ꮡb.typeToTypeNode(t);
        if (typeNode != nil) {
            result = append(result, typeNode);
            if (seenNames != nil && isIdentifierTypeReference(typeNode)) {
                seenNames.Add(typeNode.AsTypeReferenceNode().TypeName.Text(), new mapToTypeNodes_seenName(t, len(result) - 1));
            }
        }
    }

    if (seenNames != nil) {
        var restoreFlags = Ꮡb.saveRestoreFlags();
        b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsUseFullyQualifiedType);
        foreach (var types in range<slice<mapToTypeNodes_seenName>>(seenNames.Values().Invoke)) {
            if (!arrayIsHomogeneous(types, (mapToTypeNodes_seenName a, mapToTypeNodes_seenName bΔ1) => typesAreSameReference(a.t, bΔ1.t))) {
                foreach (var (_, seen) in range(types)) {
                    result[seen.i] = Ꮡb.typeToTypeNode(seen.t);
                }
            }
        }
        restoreFlags();
    }

    return b.f.NewNodeList(result);
}

internal static ж<ast.Node> serializeTypeName(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> node, bool isTypeOf, ж<ast.NodeList> typeArguments) {
    ref var b = ref Ꮡb.DerefOrNull();

    var meaning = ast.SymbolFlagsType;
    if (isTypeOf) {
        meaning = ast.SymbolFlagsValue;
    }
    var symbol = b.ch.resolveEntityName(node, meaning, true, false, node);
    if (symbol == nil) {
        return default;
    }

    var resolvedSymbol = symbol;
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAlias) != 0) {
        resolvedSymbol = b.ch.resolveAlias(symbol);
    }

    if (b.ch.IsSymbolAccessible(symbol, b.ctx.enclosingDeclaration, meaning, false).Accessibility != printer.SymbolAccessibilityAccessible) {
        return default;
    }
    return Ꮡb.symbolToTypeNode(resolvedSymbol, meaning, typeArguments);
}

} partial class checker_package {

internal static bool isIdentifierTypeReference(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsTypeReferenceNode(Ꮡnode) && ast.IsIdentifier(node.AsTypeReferenceNode().TypeName);
}

internal static bool arrayIsHomogeneous<T>(slice<T> Δarray, Func<T, T, bool> comparer) {
    if (len(Δarray) < 2) {
        return true;
    }
    var first = Δarray[0];
    for (nint i = 1; i < len(Δarray); i++) {
        var target = Δarray[i];
        if (!comparer(first, target)) {
            return false;
        }
    }
    return true;
}

internal static bool typesAreSameReference(ж<Type> Ꮡa, ж<Type> Ꮡb) {
    ref var a = ref Ꮡa.DerefOrNull();
    ref var b = ref Ꮡb.DerefOrNull();

    return Ꮡa == Ꮡb || a.symbol != nil && a.symbol == b.symbol || a.alias != nil && a.alias == b.alias;
}

} partial class checkerᴛNodeBuilderImpl {

internal static void setCommentRange(this ref NodeBuilderImpl b, ж<ast.Node> node, ж<ast.Node> range_) {
    if (range_ != nil && b.ctx.enclosingFile != nil && b.ctx.enclosingFile == ast.GetSourceFileOfNode(range_)) {
        b.e.AssignCommentRange(node, range_);
    }
}

internal static bool typeNodeIsEquivalentToType(this ref NodeBuilderImpl b, ж<ast.Node> annotatedDeclaration, ж<Type> t, ж<Type> typeFromTypeNode) {
    if (typeFromTypeNode == t) {
        return true;
    }
    if (annotatedDeclaration == nil) {
        return false;
    }
    if (isOptionalDeclaration(annotatedDeclaration)) {
        return b.ch.getTypeWithFacts(t, TypeFactsNEUndefined) == typeFromTypeNode;
    }
    return false;
}

internal static bool canReuseExistingJSTypeNode(this ref NodeBuilderImpl b, ж<astꓸTypeNode> existing, ж<Type> t) {
    return b.ch.getIntendedTypeFromJSDocTypeReference(existing) == nil && b.existingTypeNodeIsNotReferenceOrIsReferenceWithCompatibleTypeArgumentCount(existing, t);
}

internal static ж<ast.Symbol> tryGetResolvedSymbolFromTypeNode(this ref NodeBuilderImpl b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil || node.Parent == nil) {
        return default;
    }
    b.ch.getTypeFromTypeNode(Ꮡnode);
    var links = b.ch.ᏑsymbolNodeLinks.TryGet(Ꮡnode);
    if (links == nil) {
        return default;
    }
    return links.resolvedSymbol;
}

internal static bool existingTypeNodeIsNotReferenceOrIsReferenceWithCompatibleTypeArgumentCount(this ref NodeBuilderImpl b, ж<astꓸTypeNode> Ꮡexisting, ж<Type> Ꮡt) {
    ref var existing = ref Ꮡexisting.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if ((ObjectFlags)(t.objectFlags & ObjectFlagsReference) == 0) {
        return true;
    }
    if (!ast.IsTypeReferenceNode(Ꮡexisting)) {
        return true;
    }

    var symbol = b.tryGetResolvedSymbolFromTypeNode(Ꮡexisting);
    if (symbol == nil) {
        return true;
    }

    var existingTarget = b.ch.getDeclaredTypeOfSymbol(symbol);
    if (existingTarget == nil || existingTarget != t.AsTypeReference().target) {
        return true;
    }
    return len(existing.TypeArguments()) >= b.ch.getMinTypeArgumentCount(t.AsTypeReference().target.AsInterfaceType().TypeParameters());
}

internal static ж<astꓸTypeNode> tryReuseExistingNonParameterTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<astꓸTypeNode> existing, ж<Type> t, ж<ast.Node> Ꮡhost, ж<Type> ᏑannotationType) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var host = ref Ꮡhost.DerefOrNull();
    ref var annotationType = ref ᏑannotationType.DerefOrNull();

    if (Ꮡhost == nil) {
        Ꮡhost = b.ctx.enclosingDeclaration; host = ref Ꮡhost.DerefOrNull();
    }
    if (ᏑannotationType == nil) {
        ᏑannotationType = b.getTypeFromTypeNode(existing, true); annotationType = ref ᏑannotationType.DerefOrNull();
    }
    if (ᏑannotationType != nil && b.typeNodeIsEquivalentToType(Ꮡhost, t, ᏑannotationType) && b.canReuseExistingJSTypeNode(existing, t)) {
        var result = Ꮡb.tryReuseExistingNodeHelper(existing);
        if (result != nil) {
            return result;
        }
    }
    return default;
}

internal static ж<Type> getResolvedTypeWithoutAbstractConstructSignatures(this ref NodeBuilderImpl b, ж<StructuredType> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (len(t.ConstructSignatures()) == 0) {
        return Ꮡt.of(StructuredType.ᏑConstrainedType).of(ConstrainedType.ᏑTypeBase).AsType();
    }
    if (t.objectTypeWithoutAbstractConstructSignatures != nil) {
        return t.objectTypeWithoutAbstractConstructSignatures;
    }
    var constructSignatures = core.Filter(t.ConstructSignatures(), (ж<Signature> signature) => (SignatureFlags)(signature.flags & SignatureFlagsAbstract) == 0);
    if (len(constructSignatures) == len(t.ConstructSignatures())) {
        t.objectTypeWithoutAbstractConstructSignatures = Ꮡt.of(StructuredType.ᏑConstrainedType).of(ConstrainedType.ᏑTypeBase).AsType();
        return Ꮡt.of(StructuredType.ᏑConstrainedType).of(ConstrainedType.ᏑTypeBase).AsType();
    }
    var typeCopy = b.ch.newAnonymousType(t.symbol, t.members, t.CallSignatures(), core.IfElse(len(constructSignatures) > 0, constructSignatures, slice<ж<Signature>>([])), t.indexInfos);
    t.objectTypeWithoutAbstractConstructSignatures = typeCopy;
    typeCopy.AsStructuredType().objectTypeWithoutAbstractConstructSignatures = typeCopy;
    return typeCopy;
}

internal static ж<ast.Node> symbolToNode(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, ast.SymbolFlags meaning) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if ((nodebuilder.InternalFlags)(b.ctx.internalFlags & nodebuilder.InternalFlagsWriteComputedProps) != 0) {
        if (symbol.ValueDeclaration != nil) {
            var name = ast.GetNameOfDeclaration(symbol.ValueDeclaration);
            if (name != nil && ast.IsComputedPropertyName(name)) {
                return name;
            }
        }
        if (b.ch.ᏑvalueSymbolLinks.Has(Ꮡsymbol)) {
            var nameType = b.ch.ᏑvalueSymbolLinks.Get(Ꮡsymbol).nameType;
            if (nameType != nil && (TypeFlags)(nameType.flags & ((TypeFlags)(TypeFlagsEnumLiteral | TypeFlagsUniqueESSymbol))) != 0) {
                var oldEnclosing = b.ctx.enclosingDeclaration;
                b.ctx.enclosingDeclaration = nameType.symbol.ValueDeclaration;
                var result = b.f.NewComputedPropertyName(Ꮡb.symbolToExpression(nameType.symbol, meaning));
                b.ctx.enclosingDeclaration = oldEnclosing;
                return result;
            }
        }
    }
    return Ꮡb.symbolToExpression(Ꮡsymbol, meaning);
}

internal static ж<ast.Node> symbolToName(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> symbol, ast.SymbolFlags meaning, bool expectsIdentifier) {
    ref var b = ref Ꮡb.DerefOrNull();

    var chain = Ꮡb.lookupSymbolChain(symbol, meaning, false);
    if (expectsIdentifier && len(chain) != 1 && !b.ctx.encounteredError && ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowQualifiedNameInPlaceOfIdentifier) != 0)) {
        b.ctx.encounteredError = true;
    }
    return b.createEntityNameFromSymbolChain(chain, len(chain) - 1);
}

internal static ж<ast.Node> createEntityNameFromSymbolChain(this ref NodeBuilderImpl b, slice<ж<ast.Symbol>> chain, nint index) {
    var symbol = chain[index];

    if (index == 0) {
        b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsInInitialEntityName);
    }
    @string symbolName = b.getNameOfSymbolAsWritten(symbol);
    if (index == 0) {
        b.ctx.flags ^= (nodebuilder.Flags)(nodebuilder.FlagsInInitialEntityName);
    }

    var identifier = b.newIdentifier(symbolName, symbol);
    b.e.AddEmitFlags(identifier, printer.EFNoAsciiEscaping);
    if (index > 0) {
        return b.f.NewQualifiedName(
            b.createEntityNameFromSymbolChain(chain, index - 1),
            identifier);
    }
    return identifier;
}

internal static ж<astꓸEntityName> symbolToEntityNameNode(this ref NodeBuilderImpl b, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var identifier = b.newIdentifier(symbol.Name, Ꮡsymbol);
    if (symbol.Parent != nil) {
        return b.f.NewQualifiedName(b.symbolToEntityNameNode(symbol.Parent), identifier);
    }
    return identifier;
}

internal static ж<astꓸTypeNode> symbolToTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, ast.SymbolFlags mask, ж<ast.NodeList> typeArguments) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var chain = Ꮡb.lookupSymbolChain(Ꮡsymbol, mask, ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseAliasDefinedOutsideCurrentScope) == 0));
    if (len(chain) == 0) {
        return default;
    }
    var isTypeOf = mask == ast.SymbolFlagsValue;
    if (core.Some<ж<ast.Node>>(chain[0].Declarations, hasNonGlobalAugmentationExternalModuleSymbol)) {
        ж<ast.Node> nonRootParts = default;
        if (len(chain) > 1) {
            nonRootParts = Ꮡb.createAccessFromSymbolChain(chain, len(chain) - 1, 1, typeArguments);
        }
        var typeParameterNodes = typeArguments;
        if (typeParameterNodes == nil) {
            typeParameterNodes = Ꮡb.lookupTypeParameterNodes(chain, 0);
        }
        var contextFile = ast.GetSourceFileOfNode(b.e.MostOriginal(b.ctx.enclosingDeclaration));
        var targetFile = ast.GetSourceFileOfModule(chain[0]);
        moduleSpecifierResult specifierResult = default;
        coreꓸResolutionMode importModeOverride = default;
        if (b.ch.compilerOptions.GetModuleResolutionKind() == core.ModuleResolutionKindNode16 || b.ch.compilerOptions.GetModuleResolutionKind() == core.ModuleResolutionKindNodeNext) {
            if (targetFile != nil && contextFile != nil && b.ch.program.GetEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(targetFile)) == core.ModuleKindESNext && b.ch.program.GetEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(targetFile)) != b.ch.program.GetEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(contextFile))) {
                specifierResult = Ꮡb.getSpecifierForModuleSymbol(chain[0], core.ModuleKindESNext);
                importModeOverride = core.ModuleKindESNext;
            }
        }
        if (len(specifierResult.specifier) == 0) {
            specifierResult = Ꮡb.getSpecifierForModuleSymbol(chain[0], core.ResolutionModeNone);
        }
        if (((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowNodeModulesRelativePaths) == 0) && strings.Contains(specifierResult.specifier, "/node_modules/")) {
            var oldSpecifierResult = specifierResult;

            if (b.ch.compilerOptions.GetModuleResolutionKind() == core.ModuleResolutionKindNode16 || b.ch.compilerOptions.GetModuleResolutionKind() == core.ModuleResolutionKindNodeNext) {
                var swappedMode = core.ModuleKindESNext;
                if (b.ch.program.GetEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(contextFile)) == core.ModuleKindESNext) {
                    swappedMode = core.ModuleKindCommonJS;
                }
                specifierResult = Ꮡb.getSpecifierForModuleSymbol(chain[0], swappedMode);

                if (strings.Contains(specifierResult.specifier, "/node_modules/")) {
                    specifierResult = oldSpecifierResult;
                } else {
                    importModeOverride = swappedMode;
                }
            }

            if (importModeOverride == core.ResolutionModeNone) {
                b.ctx.encounteredError = true;
                b.ctx.tracker.ReportLikelyUnsafeImportRequiredError(oldSpecifierResult.specifier, symbol.Name);
            }
        }

        var attributes = b.createImportAttributesForModuleSpecifier(specifierResult, importModeOverride);
        var lit = b.f.NewLiteralTypeNode(b.newStringLiteral(specifierResult.specifier));
        b.ctx.approximateLength += len(specifierResult.specifier) + 10;
        if (nonRootParts == nil || ast.IsEntityName(nonRootParts)) {
            if (nonRootParts != nil) {
            }
            return b.f.NewImportTypeNode(isTypeOf, lit, attributes, nonRootParts, typeParameterNodes);
        }

        var splitNode = getTopmostIndexedAccessType(nonRootParts.AsIndexedAccessTypeNode());
        var qualifier = splitNode.ObjectType.AsTypeReferenceNode().TypeName;
        return b.f.NewIndexedAccessTypeNode(
            b.f.NewImportTypeNode(isTypeOf, lit, attributes, qualifier, typeParameterNodes),
            splitNode.IndexType);
    }

    var entityName = Ꮡb.createAccessFromSymbolChain(chain, len(chain) - 1, 0, typeArguments);
    if (ast.IsIndexedAccessTypeNode(entityName)) {
        return entityName;
    }
    if (ast.IsEntityName(entityName)) {
        if (isTypeOf) {
            return b.f.NewTypeQueryNode(entityName, nil);
        }
        return b.f.NewTypeReferenceNode(entityName, typeArguments);
    }
    if (isTypeOf && ast.IsExpressionWithTypeArguments(entityName)) {
        var expr = entityName.AsExpressionWithTypeArguments();
        return b.f.NewTypeQueryNode(b.f.DeepCloneNode((~expr).Expression), (~expr).TypeArguments);
    }
    return entityName;
}

} partial class checker_package {

internal static ж<ast.IndexedAccessTypeNode> getTopmostIndexedAccessType(ж<ast.IndexedAccessTypeNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsIndexedAccessTypeNode(node.ObjectType)) {
        return getTopmostIndexedAccessType(node.ObjectType.AsIndexedAccessTypeNode());
    }
    return Ꮡnode;
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> createAccessFromSymbolChain(this ж<NodeBuilderImpl> Ꮡb, slice<ж<ast.Symbol>> chain, nint index, nint stopper, ж<ast.NodeList> overrideTypeArguments) {
    ref var b = ref Ꮡb.DerefOrNull();

    var typeParameterNodes = overrideTypeArguments;
    if (index != (len(chain) - 1)) {
        typeParameterNodes = Ꮡb.lookupTypeParameterNodes(chain, index);
    }
    var symbol = chain[index];
    ж<ast.Symbol> parent = default;
    if (index > 0) {
        parent = chain[index - 1];
    }

    @string symbolName = default;
    if (index == 0) {
        b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsInInitialEntityName);
        symbolName = b.getNameOfSymbolAsWritten(symbol);
        b.ctx.approximateLength += len(symbolName) + 1;
        b.ctx.flags ^= (nodebuilder.Flags)(nodebuilder.FlagsInInitialEntityName);
    } else {
        if (parent != nil) {
            var exports = b.ch.getExportsOfSymbol(parent);
            if (exports != nil) {
                var (res, ok) = exports[symbol.Name, ꟷ];
                if (symbol.Name != ast.InternalSymbolNameExportEquals && !isLateBoundName(symbol.Name) && ok && res != nil && b.ch.getSymbolIfSameReference(res, symbol) != nil) {
                    symbolName = symbol.Name;
                } else {
                    var results = new map<ж<ast.Symbol>, @string>(1);
                    foreach (var (name, ex) in exports) {
                        if (b.ch.getSymbolIfSameReference(ex, symbol) != nil && !isLateBoundName(name) && name != ast.InternalSymbolNameExportEquals) {
                            results[ex] = name;
                        }
                    }
                    var resultSymbols = slices.Collect(maps.Keys<map<ж<ast.Symbol>, @string>, ж<ast.Symbol>, @string>(results));
                    if (len(resultSymbols) > 0) {
                        b.ch.sortSymbols(resultSymbols);
                        symbolName = results[resultSymbols[0]];
                    }
                }
            }
        }
    }

    if (len(symbolName) == 0) {
        ж<ast.Node> name = default;
        foreach (var (_, d) in range(symbol.Declarations)) {
            name = ast.GetNameOfDeclaration(d);
            if (name != nil) {
                break;
            }
        }
        if (name != nil && ast.IsComputedPropertyName(name) && ast.IsEntityName(name.Expression())) {
            var lhs = Ꮡb.createAccessFromSymbolChain(chain, index - 1, stopper, overrideTypeArguments);
            if (ast.IsEntityName(lhs)) {
                return b.f.NewIndexedAccessTypeNode(
                    b.f.NewParenthesizedTypeNode(b.f.NewTypeQueryNode(lhs, nil)),
                    b.f.NewTypeQueryNode(name.Expression(), nil));
            }
            return lhs;
        }
        symbolName = b.getNameOfSymbolAsWritten(symbol);
    }
    b.ctx.approximateLength += len(symbolName) + 1;

    if (((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsForbidIndexedAccessSymbolReferences) == 0) && parent != nil && b.ch.getMembersOfSymbol(parent) != nil && b.ch.getMembersOfSymbol(parent)[symbol.Name] != nil && b.ch.getSymbolIfSameReference(b.ch.getMembersOfSymbol(parent)[symbol.Name], symbol) != nil) {
        var lhs = Ꮡb.createAccessFromSymbolChain(chain, index - 1, stopper, overrideTypeArguments);
        if (ast.IsIndexedAccessTypeNode(lhs)) {
            return b.f.NewIndexedAccessTypeNode(
                lhs,
                b.f.NewLiteralTypeNode(b.newStringLiteral(symbolName)));
        }
        return b.f.NewIndexedAccessTypeNode(
            b.f.NewTypeReferenceNode(lhs, typeParameterNodes),
            b.f.NewLiteralTypeNode(b.newStringLiteral(symbolName)));
    }

    var identifier = b.newIdentifier(symbolName, symbol);
    b.e.AddEmitFlags(identifier, printer.EFNoAsciiEscaping);

    if (index > stopper) {
        var lhs = Ꮡb.createAccessFromSymbolChain(chain, index - 1, stopper, overrideTypeArguments);
        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseInstantiationExpressions) == 0 || ast.IsEntityName(lhs) && (typeParameterNodes == nil || len(typeParameterNodes.Nodes) == 0)) {
            return b.f.NewQualifiedName(lhs, identifier);
        }
        return b.createExpressionWithTypeArguments(b.f.NewPropertyAccessExpression(b.createAccessExpression(lhs), nil, identifier, ast.NodeFlagsNone), typeParameterNodes);
    }
    return identifier;
}

internal static ж<astꓸExpression> symbolToExpression(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> symbol, ast.SymbolFlags mask) {
    var chain = Ꮡb.lookupSymbolChain(symbol, mask, false);
    return Ꮡb.createExpressionFromSymbolChain(chain, len(chain) - 1);
}

internal static ж<astꓸExpression> createExpressionFromSymbolChain(this ж<NodeBuilderImpl> Ꮡb, slice<ж<ast.Symbol>> chain, nint index) {
    ref var b = ref Ꮡb.DerefOrNull();

    var typeParameterNodes = Ꮡb.lookupExpressionChainTypeArgumentNodes(chain, index);
    var symbol = chain[index];

    if (index == 0) {
        b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsInInitialEntityName);
    }
    @string symbolName = b.getNameOfSymbolAsWritten(symbol);
    if (index == 0) {
        b.ctx.flags ^= (nodebuilder.Flags)(nodebuilder.FlagsInInitialEntityName);
    }

    if (startsWithSingleOrDoubleQuote(symbolName) && core.Some<ж<ast.Node>>(symbol.Declarations, hasNonGlobalAugmentationExternalModuleSymbol)) {
        var specifierResult = Ꮡb.getSpecifierForModuleSymbol(symbol, core.ResolutionModeNone);
        b.ctx.approximateLength += 2 + len(specifierResult.specifier);
        return b.newStringLiteral(specifierResult.specifier);
    }

    if (index == 0 || canUsePropertyAccess(symbolName)) {
        var identifier = b.newIdentifier(symbolName, symbol);
        b.e.AddEmitFlags(identifier, printer.EFNoAsciiEscaping);
        b.ctx.approximateLength += 1 + len(symbolName);
        if (index > 0) {
            var result = b.f.NewPropertyAccessExpression(Ꮡb.createExpressionFromSymbolChain(chain, index - 1), nil, identifier, ast.NodeFlagsNone);
            b.e.AddEmitFlags(result, printer.EFNoIndentation);
            return b.createExpressionWithTypeArguments(result, typeParameterNodes);
        }
        return b.createExpressionWithTypeArguments(identifier, typeParameterNodes);
    }

    if (startsWithSquareBracket(symbolName)) {
        symbolName = symbolName.slice(1, len(symbolName) - 1);
    }

    ж<astꓸExpression> expression = default;
    if (startsWithSingleOrDoubleQuote(symbolName) && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsEnumMember) == 0) {
        @string literalText = stringutil.UnquoteString(symbolName);
        b.ctx.approximateLength += len(literalText) + 2;
        expression = b.newStringLiteralEx(literalText, symbolName[0] == '\'');
    } else if (jsnum.FromString(symbolName).String() == symbolName) {
        b.ctx.approximateLength += len(symbolName);
        expression = b.f.NewNumericLiteral(symbolName, ast.TokenFlagsNone);
    }
    if (expression == nil) {
        b.ctx.approximateLength += len(symbolName);
        expression = b.newIdentifier(symbolName, symbol);
        b.e.AddEmitFlags(expression, printer.EFNoAsciiEscaping);
    }
    b.ctx.approximateLength += 2;
    return b.createExpressionWithTypeArguments(b.f.NewElementAccessExpression(Ꮡb.createExpressionFromSymbolChain(chain, index - 1), nil, expression, ast.NodeFlagsNone), typeParameterNodes);
}

} partial class checker_package {

internal static bool canUsePropertyAccess(@string name) {
    if (len(name) == 0) {
        return false;
    }
    if (strings.HasPrefix(name, "#")) {
        return len(name) > 1 && scanner.IsIdentifierText(name[1..], core.LanguageVariantStandard);
    }
    return scanner.IsIdentifierText(name, core.LanguageVariantStandard);
}

internal static bool startsWithSingleOrDoubleQuote(@string str) {
    return strings.HasPrefix(str, "'") || strings.HasPrefix(str, "\"");
}

internal static bool startsWithSquareBracket(@string str) {
    return strings.HasPrefix(str, "[");
}

internal static bool isDefaultBindingContext(ж<ast.Node> Ꮡlocation) {
    ref var location = ref Ꮡlocation.DerefOrNull();

    return location.Kind == ast.KindSourceFile || ast.IsAmbientModule(Ꮡlocation);
}

} partial class checkerᴛNodeBuilderImpl {

internal static @string getNameOfSymbolFromNameType(this ref NodeBuilderImpl b, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (b.ch.ᏑvalueSymbolLinks.Has(Ꮡsymbol)) {
        var nameType = b.ch.ᏑvalueSymbolLinks.Get(Ꮡsymbol).nameType;
        if (nameType == nil) {
            return "";
        }
        if ((TypeFlags)(nameType.flags & TypeFlagsStringOrNumberLiteral) != 0) {
            @string name = default;
            switch (nameType.AsLiteralType().value.type()) {
            case @string v: {
                name = v;
                break;
            }
            case jsnum.Number v: {
                name = v.String();
                break;
            }}
            if (!scanner.IsIdentifierText(name, core.LanguageVariantStandard) && !isNumericLiteralName(name)) {
                return b.ch.valueToString(nameType.AsLiteralType().value);
            }
            if (isNumericLiteralName(name) && strings.HasPrefix(name, "-")) {
                return fmt.Sprintf("[%s]"u8, name);
            }
            return name;
        }
        if ((TypeFlags)(nameType.flags & TypeFlagsUniqueESSymbol) != 0) {
            @string text = b.getNameOfSymbolAsWritten(nameType.AsUniqueESSymbolType().symbol);
            return fmt.Sprintf("[%s]"u8, text);
        }
    }
    return "";
}

internal static @string getNameOfSymbolAsWritten(this ref NodeBuilderImpl b, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var (result, ok) = b.ctx.remappedSymbolReferences[ast.GetSymbolId(Ꮡsymbol), ꟷ];
    if (ok) {
        Ꮡsymbol = result; symbol = ref Ꮡsymbol.DerefOrNull();
    }
    if (symbol.Name == ast.InternalSymbolNameDefault && ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseAliasDefinedOutsideCurrentScope) == 0) && (((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsInInitialEntityName) == 0) || len(symbol.Declarations) == 0 || (b.ctx.enclosingDeclaration != nil && ast.FindAncestor(symbol.Declarations[0], isDefaultBindingContext) != ast.FindAncestor(b.ctx.enclosingDeclaration, isDefaultBindingContext)))) {
        return "default";
    }
    if (len(symbol.Declarations) > 0) {
        var nameΔ1 = core.FirstNonNil<ж<ast.Node>, ж<ast.Node>>(symbol.Declarations, ast.GetNameOfDeclaration);
        if (nameΔ1 != nil) {
            if (ast.IsComputedPropertyName(nameΔ1) && (ast.CheckFlags)(symbol.CheckFlags & ast.CheckFlagsLate) == 0) {
                if (b.ch.ᏑvalueSymbolLinks.Has(Ꮡsymbol) && b.ch.ᏑvalueSymbolLinks.Get(Ꮡsymbol).nameType != nil && (TypeFlags)(b.ch.ᏑvalueSymbolLinks.Get(Ꮡsymbol).nameType.flags & TypeFlagsStringOrNumberLiteral) != 0) {
                    @string resultΔ1 = b.getNameOfSymbolFromNameType(Ꮡsymbol);
                    if (len(resultΔ1) > 0) {
                        return resultΔ1;
                    }
                }
            }
            return scanner.DeclarationNameToString(nameΔ1);
        }
        var declaration = symbol.Declarations[0];
        if (declaration.Parent != nil && declaration.Parent.Kind == ast.KindVariableDeclaration) {
            return scanner.DeclarationNameToString(declaration.Parent.AsVariableDeclaration().Name());
        }
        if (ast.IsClassExpression(declaration) || ast.IsFunctionExpression(declaration) || ast.IsArrowFunction(declaration)) {
            if (b.ctx != nil && !b.ctx.encounteredError && (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowAnonymousIdentifier) == 0) {
                b.ctx.encounteredError = true;
            }
            switch (declaration.Kind) {
            case var ᴛ when ᴛ == ast.KindClassExpression: {
                return "(Anonymous class)";
            }
            case var ᴛ when ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction: {
                return "(Anonymous function)";
            }}

        }
    }
    @string name = b.getNameOfSymbolFromNameType(Ꮡsymbol);
    if (len(name) > 0) {
        return name;
    }
    return ast.EscapeInternalSymbolName(symbol.Name);
}

internal static slice<ж<Type>> getTypeParametersOfClassOrInterface(this ref NodeBuilderImpl b, ж<ast.Symbol> symbol) {
    var result = new slice<ж<Type>>(0);
    result = appendꓸꓸꓸ(result, b.ch.getOuterTypeParametersOfClassOrInterface(symbol));
    result = appendꓸꓸꓸ(result, b.ch.getLocalTypeParametersOfClassOrInterfaceOrTypeAlias(symbol));
    return result;
}

internal static ж<astꓸTypeParameterList> lookupTypeParameterNodes(this ж<NodeBuilderImpl> Ꮡb, slice<ж<ast.Symbol>> chain, nint index) {
    ref var b = ref Ꮡb.DerefOrNull();

    debug.Assert(chain != nil && 0 <= index && index < len(chain));
    var symbol = chain[index];
    var symbolId = ast.GetSymbolId(symbol);
    if (b.ctx.ᏑtypeParameterSymbolList.Has(symbolId)) {
        return default;
    }
    b.ctx.ᏑtypeParameterSymbolList.Add(symbolId);

    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteTypeParametersInQualifiedName) != 0 && index < (len(chain) - 1)) {
        var typeArgumentNodes = Ꮡb.lookupInstantiatedTypeArgumentNodes(chain, index); if (typeArgumentNodes != nil) {
            return typeArgumentNodes;
        } else {
            var typeParameterNodes = Ꮡb.typeParametersToTypeParameterDeclarations(symbol);
            if (len(typeParameterNodes) > 0) {
                return b.f.NewNodeList(typeParameterNodes);
            }
            return default;
        }
    }

    return default;
}

internal static slice<ж<ast.Symbol>> lookupSymbolChain(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> symbol, ast.SymbolFlags meaning, bool yieldModuleSymbol) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.ctx.tracker.TrackSymbol(symbol, b.ctx.enclosingDeclaration, meaning);
    return Ꮡb.lookupSymbolChainWorker(symbol, meaning, yieldModuleSymbol);
}

internal static slice<ж<ast.Symbol>> lookupSymbolChainWorker(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, ast.SymbolFlags meaning, bool yieldModuleSymbol) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    slice<ж<ast.Symbol>> chain = default;
    var isTypeParameter = (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTypeParameter) != 0;
    if (!isTypeParameter && (b.ctx.enclosingDeclaration != nil || (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseFullyQualifiedType) != 0) && ((nodebuilder.InternalFlags)(b.ctx.internalFlags & nodebuilder.InternalFlagsDoNotIncludeSymbolChain) == 0)) {
        var res = Ꮡb.getSymbolChain(Ꮡsymbol, meaning, true, yieldModuleSymbol);
        chain = res;
        debug.Assert(chain != nil);
        debug.Assert(len(chain) > 0);
    } else {
        chain = append(chain, Ꮡsymbol);
    }
    return chain;
}

} partial class checker_package {

partial struct sortedSymbolNamePair {
    internal ж<ast.Symbol> sym;
    internal @string name;
}

} partial class checkerᴛNodeBuilderImpl {

internal static slice<ж<ast.Symbol>> getSymbolChain(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, ast.SymbolFlags meaning, bool endOfChain, bool yieldModuleSymbol) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    var nextSymsᴷ = new stackAppend<ж<ast.Symbol>>();

    var accessibleSymbolChain = b.ch.getAccessibleSymbolChain(Ꮡsymbol, b.ctx.enclosingDeclaration, meaning, (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseOnlyExternalAliasing) != 0);
    var qualifierMeaning = meaning;
    if (len(accessibleSymbolChain) > 1) {
        qualifierMeaning = getQualifiedLeftMeaning(meaning);
    }
    if (len(accessibleSymbolChain) == 0 || b.ch.needsQualification(accessibleSymbolChain[0], b.ctx.enclosingDeclaration, qualifierMeaning)) {
        var root = Ꮡsymbol;
        if (len(accessibleSymbolChain) > 0) {
            root = accessibleSymbolChain[0];
        }
        var parents = b.ch.getContainersOfSymbol(root, b.ctx.enclosingDeclaration, meaning);
        if (len(parents) > 0) {
            var parentSpecifiers = core.Map(parents, sortedSymbolNamePair (ж<ast.Symbol> symbolΔ1) => {
                if (core.Some<ж<ast.Node>>(symbolΔ1.Declarations, hasNonGlobalAugmentationExternalModuleSymbol)) {
                    return new sortedSymbolNamePair(symbolΔ1, Ꮡb.getSpecifierForModuleSymbol(symbolΔ1, core.ResolutionModeNone).specifier);
                }
                return new sortedSymbolNamePair(symbolΔ1, "");
            });
            slices.SortStableFunc<slice<sortedSymbolNamePair>, sortedSymbolNamePair>(parentSpecifiers, Ꮡb.sortByBestName);
            foreach (var (_, pair) in range(parentSpecifiers)) {
                var parent = pair.sym;
                var parentChain = Ꮡb.getSymbolChain(parent, getQualifiedLeftMeaning(meaning), false, yieldModuleSymbol);
                if (len(parentChain) > 0) {
                    if (parent.Exports != nil) {
                        var (exported, ok) = parent.Exports[ast.InternalSymbolNameExportEquals, ꟷ];
                        if (ok && b.ch.getSymbolIfSameReference(exported, Ꮡsymbol) != nil) {
                            accessibleSymbolChain = parentChain;
                            break;
                        }
                    }
                    var nextSyms = accessibleSymbolChain;
                    if (len(nextSyms) == 0) {
                        var fallback = b.ch.getAliasForSymbolInContainer(parent, Ꮡsymbol);
                        if (fallback == nil) {
                            fallback = Ꮡsymbol;
                        }
                        nextSyms = append(nextSymsᴷ, nextSyms, fallback);
                    }
                    accessibleSymbolChain = appendꓸꓸꓸ(parentChain, nextSyms);
                    break;
                }
            }
        }
    }
    if (len(accessibleSymbolChain) > 0) {
        return accessibleSymbolChain;
    }
    if (endOfChain || ((ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsTypeLiteral | ast.SymbolFlagsObjectLiteral))) == 0)) {
        if (!endOfChain && !yieldModuleSymbol && core.Some<ж<ast.Node>>(symbol.Declarations, hasNonGlobalAugmentationExternalModuleSymbol)) {
            return default;
        }
        return [Ꮡsymbol];
    }
    return default;
}

internal static nint sortByBestName(this ref NodeBuilderImpl b_, sortedSymbolNamePair a, sortedSymbolNamePair b) {
    @string specifierA = a.name;
    @string specifierB = b.name;
    if (len(specifierA) > 0 && len(specifierB) > 0) {
        var isBRelative = tspath.PathIsRelative(specifierB);
        if (tspath.PathIsRelative(specifierA) == isBRelative) {
            return modulespecifiers.CountPathComponents(specifierA) - modulespecifiers.CountPathComponents(specifierB);
        }
        if (isBRelative) {
            return -1;
        }
        return 1;
    }
    return b_.ch.compareSymbols(a.sym, b.sym);
}

} partial class checker_package {

internal static bool canHaveModuleSpecifier(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return false;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindBindingElement || ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindExportDeclaration || ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindImportClause || ᴛ == ast.KindNamespaceExport || ᴛ == ast.KindNamespaceImport || ᴛ == ast.KindExportSpecifier || ᴛ == ast.KindImportSpecifier || ᴛ == ast.KindImportType: {
        return true;
    }}

    return false;
}

public static ж<ast.Node> TryGetModuleSpecifierFromDeclaration(ж<ast.Node> node) {
    var res = tryGetModuleSpecifierFromDeclarationWorker(node);
    if (res == nil || !ast.IsStringLiteral(res)) {
        return default;
    }
    return res;
}

internal static ж<ast.Node> tryGetModuleSpecifierFromDeclarationWorker(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindBindingElement: {
        var moduleCall = ast.FindAncestor(node.Initializer(), (ж<ast.Node> nodeΔ2) => ast.IsRequireCall(nodeΔ2, true) || ast.IsImportCall(nodeΔ2));
        if (moduleCall == nil) {
            return default;
        }
        return moduleCall.Arguments()[0];
    }
    case var ᴛ when ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindExportDeclaration || ᴛ == ast.KindJSDocImportTag: {
        return node.ModuleSpecifier();
    }
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
        var @ref = node.AsImportEqualsDeclaration().ModuleReference;
        if (@ref.Kind != ast.KindExternalModuleReference) {
            return default;
        }
        return @ref.Expression();
    }
    case var ᴛ when ᴛ == ast.KindImportClause: {
        if (ast.IsImportDeclaration(node.Parent)) {
            return node.Parent.ModuleSpecifier();
        }
        return node.Parent.ModuleSpecifier();
    }
    case var ᴛ when ᴛ == ast.KindNamespaceExport: {
        return node.Parent.ModuleSpecifier();
    }
    case var ᴛ when ᴛ == ast.KindNamespaceImport: {
        if (ast.IsImportDeclaration(node.Parent.Parent)) {
            return node.Parent.Parent.ModuleSpecifier();
        }
        return node.Parent.Parent.ModuleSpecifier();
    }
    case var ᴛ when ᴛ == ast.KindExportSpecifier: {
        return node.Parent.Parent.ModuleSpecifier();
    }
    case var ᴛ when ᴛ == ast.KindImportSpecifier: {
        if (ast.IsImportDeclaration(node.Parent.Parent.Parent)) {
            return node.Parent.Parent.Parent.ModuleSpecifier();
        }
        return node.Parent.Parent.Parent.ModuleSpecifier();
    }
    case var ᴛ when ᴛ == ast.KindImportType: {
        if (ast.IsLiteralImportTypeNode(Ꮡnode)) {
            return node.AsImportTypeNode().Argument.AsLiteralTypeNode().Literal;
        }
        return default;
    }
    default: {
        debug.AssertNever(Ꮡnode.OrTypedNil());
        return default;
    }}

}

} partial class checkerᴛNodeBuilderImpl {

internal static moduleSpecifierResult getSpecifierForModuleSymbol(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, coreꓸResolutionMode overrideImportMode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var enclosingDeclaration = b.e.MostOriginal(b.ctx.enclosingDeclaration);
    ж<ast.Node> originalModuleSpecifier = default;
    if (canHaveModuleSpecifier(enclosingDeclaration)) {
        originalModuleSpecifier = TryGetModuleSpecifierFromDeclaration(enclosingDeclaration);
    }
    ж<Type> originalImportAttributesType = default;
    if (originalModuleSpecifier != nil) {
        originalImportAttributesType = b.ch.getImportAttributesTypeForModuleSpecifier(originalModuleSpecifier);
    }

    var @file = ast.GetDeclarationOfKind(Ꮡsymbol, ast.KindSourceFile);
    if (@file == nil) {
        var equivalentSymbol = core.FirstNonNil(symbol.Declarations, ж<ast.Symbol> (ж<ast.Node> d) => Ꮡb.Value.ch.getFileSymbolIfFileSymbolExportEqualsContainer(d, Ꮡsymbol));
        if (equivalentSymbol != nil) {
            @file = ast.GetDeclarationOfKind(equivalentSymbol, ast.KindSourceFile);
        }
    }

    if (@file == nil) {
        var declaration = core.Find<ж<ast.Node>>(symbol.Declarations, ast.IsModuleWithStringLiteralName); if (declaration != nil) {
            @string specifier = declaration.Name().Text();
            if (originalImportAttributesType != nil && b.moduleSpecifierResolvesToSymbol(specifier, originalImportAttributesType, Ꮡsymbol)) {
                return new moduleSpecifierResult(specifier: specifier, importAttributesType: originalImportAttributesType);
            }
            return new moduleSpecifierResult(specifier: specifier, importAttributesType: b.ch.getTypeOfModuleImportAttributes(Ꮡsymbol));
        }
        {
            var (specifier, okΔ1) = ast.TryGetAmbientModuleNameFromSymbolName(symbol.Name); if (okΔ1) {
                return new moduleSpecifierResult(specifier: specifier);
            }
        }
    }
    if (b.ctx.enclosingFile == nil) {
        var (specifier, okΔ2) = ast.TryGetAmbientModuleNameFromSymbolName(symbol.Name); if (okΔ2) {
            return new moduleSpecifierResult(specifier: specifier);
        }
        return new moduleSpecifierResult(specifier: ast.GetSourceFileOfModule(Ꮡsymbol).FileName());
    }

    var contextFile = b.ctx.enclosingFile;
    var resolutionMode = overrideImportMode;
    if (resolutionMode == core.ResolutionModeNone && originalModuleSpecifier != nil) {
        resolutionMode = b.ch.program.GetModeForUsageLocation(new ast.SourceFileжHasFileName(contextFile), originalModuleSpecifier);
    } else if (resolutionMode == core.ResolutionModeNone && contextFile != nil) {
        resolutionMode = b.ch.program.GetDefaultResolutionModeForFile(new ast.SourceFileжHasFileName(contextFile));
    }
    var cacheKey = new module.ModeAwareCacheKey(Name: ((@string)contextFile.Path()), Mode: resolutionMode);
    var links = b.symbolLinks.Get(Ꮡsymbol);
    if (links.specifierCache == nil) {
        links.specifierCache = new module.ModeAwareCache<moduleSpecifierResult>(0);
    }
    var (result, ok) = links.specifierCache[cacheKey, ꟷ];
    if (ok) {
        return b.moduleSpecifierResultForSymbol(result, originalImportAttributesType, Ꮡsymbol);
    }
    var host = b.ctx.host;
    var specifierCompilerOptions = b.ch.compilerOptions;
    modulespecifiers.ImportModuleSpecifierPreference specifierPref = modulespecifiers.ImportModuleSpecifierPreferenceProjectRelative;
    modulespecifiers.ImportModuleSpecifierEndingPreference endingPref = modulespecifiers.ImportModuleSpecifierEndingPreferenceNone;
    if (resolutionMode == core.ResolutionModeESM) {
        endingPref = modulespecifiers.ImportModuleSpecifierEndingPreferenceJs;
    }
    var moduleSpecifiersResult = modulespecifiers.GetModuleSpecifiers(
        Ꮡsymbol,
        new CheckerжCheckerShape(b.ch),
        specifierCompilerOptions,
        new ast_SourceFileжSourceFileForSpecifierGeneration(contextFile),
        host,
        new modulespecifiers.UserPreferences(
            ImportModuleSpecifierPreference: specifierPref,
            ImportModuleSpecifierEnding: endingPref
        ),
        new modulespecifiers.ModuleSpecifierOptions(
            OverrideImportMode: overrideImportMode
        ),
        false);
    debug.Assert(len(moduleSpecifiersResult.Specifiers) > 0);
    result = new moduleSpecifierResult(specifier: moduleSpecifiersResult.Specifiers[0]);
    if (moduleSpecifiersResult.AmbientModuleSymbol != nil) {
        result.importAttributesType = b.ch.getTypeOfModuleImportAttributes(moduleSpecifiersResult.AmbientModuleSymbol);
    }
    links.specifierCache[cacheKey] = result;
    return b.moduleSpecifierResultForSymbol(result, originalImportAttributesType, Ꮡsymbol);
}

internal static moduleSpecifierResult moduleSpecifierResultForSymbol(this ref NodeBuilderImpl b, moduleSpecifierResult result, ж<Type> ᏑimportAttributesType, ж<ast.Symbol> symbol) {
    ref var importAttributesType = ref ᏑimportAttributesType.DerefOrNull();

    if (ᏑimportAttributesType != nil && b.moduleSpecifierResolvesToSymbol(result.specifier, ᏑimportAttributesType, symbol)) {
        result.importAttributesType = ᏑimportAttributesType;
    }
    return result;
}

internal static bool moduleSpecifierResolvesToSymbol(this ref NodeBuilderImpl b, @string specifier, ж<Type> importAttributesType, ж<ast.Symbol> symbol) {
    var location = b.ctx.enclosingDeclaration;
    if (location == nil && b.ctx.enclosingFile != nil) {
        location = b.ctx.enclosingFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    if (location == nil) {
        return false;
    }
    var resolved = b.ch.resolveExternalModule(location, specifier, nil, nil, false, importAttributesType);
    return resolved != nil && b.ch.getMergedSymbol(resolved) == b.ch.getMergedSymbol(symbol);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string importˢ = "import"u8;
internal static readonly @string requireˢ = "require"u8;

internal static ж<ast.Node> createImportAttributesForModuleSpecifier(this ref NodeBuilderImpl b, moduleSpecifierResult result, coreꓸResolutionMode importModeOverride) {
    var isEmptyAttributesType = result.importAttributesType == nil || b.ch.isEmptyObjectType(result.importAttributesType);
    if (isEmptyAttributesType && importModeOverride == core.ResolutionModeNone) {
        return default;
    }

    slice<ж<ast.Symbol>> properties = default;
    if (!isEmptyAttributesType) {
        properties = slices.Clone<slice<ж<ast.Symbol>>, ж<ast.Symbol>>(b.ch.getPropertiesOfType(result.importAttributesType));
    }
    slices.SortFunc(properties, (ж<ast.Symbol> a, ж<ast.Symbol> bΔ1) => strings.Compare(a.Name, bΔ1.Name));
    var attributes = new slice<ж<ast.Node>>(0, len(properties) + 1);
    @string resolutionMode = "";
    if (importModeOverride != core.ResolutionModeNone) {
        resolutionMode = core.IfElse(importModeOverride == core.ResolutionModeESM, importˢ, requireˢ);
        attributes = append(attributes, b.f.NewImportAttribute(b.newStringLiteral("resolution-mode"), b.newStringLiteral(resolutionMode)));
        b.ctx.approximateLength += len("resolution-mode") + len(resolutionMode) + 6;
    }
    foreach (var (_, property) in range(properties)) {
        @string name = property.Name;
        var propertyType = b.ch.getTypeOfSymbol(property);
        if ((TypeFlags)(propertyType.flags & TypeFlagsStringLiteral) == 0) {
            continue;
        }
        @string value = getStringLiteralValue(propertyType);
        ж<ast.Node> nameNode = default;
        if (scanner.IsIdentifierText(name, core.LanguageVariantStandard)) {
            nameNode = b.f.NewIdentifier(name);
        } else {
            nameNode = b.newStringLiteral(name);
            b.ctx.approximateLength += 2;
        }
        attributes = append(attributes, b.f.NewImportAttribute(nameNode, b.newStringLiteral(value)));
        b.ctx.approximateLength += len(name) + len(value) + 4;
    }
    if (len(attributes) == 0) {
        return default;
    }
    b.ctx.approximateLength += 16 + 2 * (len(attributes) - 1);
    return b.f.NewImportAttributes(ast.KindWithKeyword, b.f.NewNodeList(attributes), false);
}

internal static ж<astꓸTypeParameterDeclarationNode> typeParameterToDeclarationWithConstraint(this ж<NodeBuilderImpl> Ꮡb, ж<Type> typeParameter, ж<astꓸTypeNode> constraintNode) {
    ref var b = ref Ꮡb.DerefOrNull();

    var restoreFlags = Ꮡb.saveRestoreFlags();
    b.ctx.flags &= unchecked((nodebuilder.Flags)~(nodebuilder.Flags)(nodebuilder.FlagsWriteTypeParametersInQualifiedName));
    var modifiers = ast.CreateModifiersFromModifierFlags(b.ch.getTypeParameterModifiers(typeParameter), b.f.NewModifier);
    ж<ast.ModifierList> modifiersList = default;
    if (len(modifiers) > 0) {
        modifiersList = b.f.NewModifierList(modifiers);
    }
    var name = Ꮡb.typeParameterToName(typeParameter);
    var defaultParameter = b.ch.getDefaultFromTypeParameter(typeParameter);
    ж<ast.Node> defaultParameterDeclarationNode = default;
    if (defaultParameter != nil) {
        defaultParameterDeclarationNode = Ꮡb.typeToTypeNode(defaultParameter);
    }
    restoreFlags();
    return b.f.NewTypeParameterDeclaration(
        modifiersList,
        name.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(),
        constraintNode,
        nil,
        defaultParameterDeclarationNode);
}

internal static ж<ast.Node> setTextRange(this ref NodeBuilderImpl b, ж<ast.Node> Ꮡrange_, ж<ast.Node> Ꮡlocation) {
    ref var range_ = ref Ꮡrange_.DerefOrNull();
    ref var location = ref Ꮡlocation.DerefOrNull();

    if (Ꮡrange_ == nil) {
        return Ꮡrange_;
    }
    if (!ast.NodeIsSynthesized(Ꮡrange_) || ((ast.NodeFlags)(range_.Flags & ast.NodeFlagsSynthesized) == 0) || b.ctx.enclosingFile == nil || b.ctx.enclosingFile != ast.GetSourceFileOfNode(b.e.MostOriginal(Ꮡrange_))) {
        var originalΔ1 = Ꮡrange_;
        Ꮡrange_ = range_.Clone(new ast.NodeFactoryжNodeFactoryCoercible(b.f)); range_ = ref Ꮡrange_.DerefOrNull();
        range_.Loc = core.NewTextRange(-1, -1);
        var (symbol, ok) = b.idToSymbol[originalΔ1, ꟷ]; if (ok) {
            b.idToSymbol[Ꮡrange_] = symbol;
        }
    }
    if (Ꮡrange_ == Ꮡlocation || Ꮡlocation == nil) {
        return Ꮡrange_;
    }
    var original = b.e.Original(Ꮡrange_);
    while (original != nil && original != Ꮡlocation) {
        original = b.e.Original(original);
    }
    if (original == nil) {
        b.e.SetOriginalEx(Ꮡrange_, Ꮡlocation, true);
    }

    if (b.ctx.enclosingFile != nil && b.ctx.enclosingFile == ast.GetSourceFileOfNode(b.e.MostOriginal(Ꮡlocation))) {
        range_.Loc = location.Loc;
        return Ꮡrange_;
    } else {
        range_.Loc = core.NewTextRange(-1, -1);
    }
    return Ꮡrange_;
}

internal static bool typeParameterShadowsOtherTypeParameterInScope(this ref NodeBuilderImpl b, @string name, ж<Type> ᏑtypeParameter) {
    ref var typeParameter = ref ᏑtypeParameter.DerefOrNull();

    var result = b.ch.resolveName(b.ctx.enclosingDeclaration, name, ast.SymbolFlagsType, nil, false, false);
    if (result != nil && (ast.SymbolFlags)(result.Flags & ast.SymbolFlagsTypeParameter) != 0) {
        return result != typeParameter.symbol;
    }
    return false;
}

internal static ж<ast.Identifier> typeParameterToName(this ж<NodeBuilderImpl> Ꮡb, ж<Type> ᏑtypeParameter) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var typeParameter = ref ᏑtypeParameter.DerefOrNull();

    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0) {
        var (cached, ok) = b.ctx.ᏑtypeParameterNames.Get(typeParameter.id); if (ok) {
            return cached;
        }
    }
    var result = Ꮡb.symbolToName(typeParameter.symbol, ast.SymbolFlagsType, true);
    if (!ast.IsIdentifier(result)) {
        return b.f.NewIdentifier("(Missing type parameter)").AsIdentifier();
    }
    if (typeParameter.symbol != nil && len(typeParameter.symbol.Declarations) > 0) {
        var decl = typeParameter.symbol.Declarations[0];
        if (decl != nil && ast.IsTypeParameterDeclaration(decl)) {
            result = b.setTextRange(result, decl.Name());
        }
    }
    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0) {
        @string rawText = result.Text();
        var (i, _) = b.ctx.ᏑtypeParameterNamesByTextNextNameCount.Get(rawText);
        @string text = rawText;

        while (true) {
            if (!b.ctx.ᏑtypeParameterNamesByText.Has(text) && !b.typeParameterShadowsOtherTypeParameterInScope(text, ᏑtypeParameter)) {
                break;
            }
            i++;
            text = fmt.Sprintf("%s_%d"u8, rawText, i);
        }

        if (text != rawText) {
            result = b.newIdentifier(text, typeParameter.symbol);
        }

        b.ctx.ᏑtypeParameterNamesByTextNextNameCount.Set(rawText, i);
        b.ctx.ᏑtypeParameterNames.Set(typeParameter.id, result.AsIdentifier());
        b.ctx.ᏑtypeParameterNamesByText.Add(text);
    }

    return result.AsIdentifier();
}

internal static bool isMappedTypeHomomorphic(this ref NodeBuilderImpl b, ж<Type> mapped) {
    return b.ch.getHomomorphicTypeVariable(mapped) != nil;
}

internal static bool isHomomorphicMappedTypeWithNonHomomorphicInstantiation(this ref NodeBuilderImpl b, ж<MappedType> Ꮡmapped) {
    ref var mapped = ref Ꮡmapped.DerefOrNull();

    return mapped.target != nil && !b.isMappedTypeHomomorphic(Ꮡmapped.of(MappedType.ᏑObjectType).of(ObjectType.ᏑStructuredType).of(StructuredType.ᏑConstrainedType).of(ConstrainedType.ᏑTypeBase).AsType()) && b.isMappedTypeHomomorphic(mapped.target);
}

internal static ж<astꓸTypeNode> createMappedTypeNodeFromType(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    debug.Assert((TypeFlags)(t.Flags() & TypeFlagsObject) != 0);
    var mapped = t.AsMappedType();
    ж<ast.Node> readonlyToken = default;
    if (mapped.declaration.ReadonlyToken != nil) {
        readonlyToken = b.f.NewToken(mapped.declaration.ReadonlyToken.Kind);
    }
    ж<ast.Node> questionToken = default;
    if ((~mapped.declaration).QuestionToken != nil) {
        questionToken = b.f.NewToken((~mapped.declaration).QuestionToken.Kind);
    }
    ж<ast.Node> appropriateConstraintTypeNode = default;
    ж<ast.Node> newTypeVariable = default;
    var templateType = b.ch.getTemplateTypeFromMappedType(Ꮡt);
    var typeParameter = b.ch.getTypeParameterFromMappedType(Ꮡt);

    var needsModifierPreservingWrapper = !b.ch.isMappedTypeWithKeyofConstraintDeclaration(Ꮡt) && (TypeFlags)(b.ch.getModifiersTypeFromMappedType(Ꮡt).flags & TypeFlagsUnknown) == 0 && (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0 && !((TypeFlags)(b.ch.getConstraintTypeFromMappedType(Ꮡt).flags & TypeFlagsTypeParameter) != 0 && (TypeFlags)(b.ch.getConstraintOfTypeParameter(b.ch.getConstraintTypeFromMappedType(Ꮡt)).flags & TypeFlagsIndex) != 0);

    if (b.ch.isMappedTypeWithKeyofConstraintDeclaration(Ꮡt)) {
        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0 && b.isHomomorphicMappedTypeWithNonHomomorphicInstantiation(mapped)) {
            var newConstraintParam = b.ch.newTypeParameter(
                b.ch.newSymbol(ast.SymbolFlagsTypeParameter, "T"));
            var name = Ꮡb.typeParameterToName(newConstraintParam);
            var target = t.Target();
            newTypeVariable = b.f.NewTypeReferenceNode(name.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil);
            templateType = b.ch.instantiateType(b.ch.getTemplateTypeFromMappedType(target), newTypeMapper(slice<ж<Type>>([b.ch.getTypeParameterFromMappedType(target), b.ch.getModifiersTypeFromMappedType(target)]), [typeParameter, newConstraintParam]));
        }
        var indexTarget = newTypeVariable;
        if (indexTarget == nil) {
            indexTarget = Ꮡb.typeToTypeNode(b.ch.getModifiersTypeFromMappedType(Ꮡt));
        }
        appropriateConstraintTypeNode = b.f.NewTypeOperatorNode(ast.KindKeyOfKeyword, indexTarget);
    } else if (needsModifierPreservingWrapper) {
        var newParam = b.ch.newTypeParameter(
            b.ch.newSymbol(ast.SymbolFlagsTypeParameter, "T"));
        var name = Ꮡb.typeParameterToName(newParam);
        newTypeVariable = b.f.NewTypeReferenceNode(name.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil);
        appropriateConstraintTypeNode = newTypeVariable;
    } else {
        appropriateConstraintTypeNode = Ꮡb.typeToTypeNode(b.ch.getConstraintTypeFromMappedType(Ꮡt));
    }

    var cleanup = Ꮡb.enterNewScope(mapped.declaration.of(ast.MappedTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), default, slice<ж<Type>>([b.ch.getTypeParameterFromMappedType(Ꮡt)]), default, nil);
    var typeParameterDeclarationNode = Ꮡb.typeParameterToDeclarationWithConstraint(typeParameter, appropriateConstraintTypeNode);
    ж<ast.Node> nameTypeNode = default;
    if (mapped.declaration.NameType != nil) {
        nameTypeNode = Ꮡb.typeToTypeNode(b.ch.getNameTypeFromMappedType(Ꮡt));
    }
    var templateTypeNode = Ꮡb.typeToTypeNode(b.ch.removeMissingType(
        templateType,
        (MappedTypeModifiers)(getMappedTypeModifiers(Ꮡt) & MappedTypeModifiersIncludeOptional) != 0));
    cleanup();
    var result = b.f.NewMappedTypeNode(
        readonlyToken,
        typeParameterDeclarationNode,
        nameTypeNode,
        questionToken,
        templateTypeNode,
        nil);
    b.ctx.approximateLength += 10;
    b.e.AddEmitFlags(result, printer.EFSingleLine);

    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0 && b.isHomomorphicMappedTypeWithNonHomomorphicInstantiation(mapped)) {
        var rawConstraintTypeFromDeclaration = b.getTypeFromTypeNode(mapped.declaration.TypeParameter.AsTypeParameterDeclaration().Constraint.Type(), false);
        if (rawConstraintTypeFromDeclaration != nil) {
            rawConstraintTypeFromDeclaration = b.ch.getConstraintOfTypeParameter(rawConstraintTypeFromDeclaration);
        }
        if (rawConstraintTypeFromDeclaration == nil) {
            rawConstraintTypeFromDeclaration = b.ch.unknownType;
        }
        var originalConstraint = b.ch.instantiateType(rawConstraintTypeFromDeclaration, mapped.mapper);

        ж<ast.Node> originalConstraintNode = default;
        if ((TypeFlags)(originalConstraint.flags & TypeFlagsUnknown) == 0) {
            originalConstraintNode = Ꮡb.typeToTypeNode(originalConstraint);
        }

        return b.f.NewConditionalTypeNode(
            Ꮡb.typeToTypeNode(b.ch.getModifiersTypeFromMappedType(Ꮡt)),
            b.f.NewInferTypeNode(b.f.NewTypeParameterDeclaration(nil, newTypeVariable.AsTypeReferenceNode().TypeName.Clone(new ast.NodeFactoryжNodeFactoryCoercible(b.f)), originalConstraintNode, nil, nil)),
            result,
            b.f.NewKeywordTypeNode(ast.KindNeverKeyword));
    } else if (needsModifierPreservingWrapper) {
        return b.f.NewConditionalTypeNode(
            Ꮡb.typeToTypeNode(b.ch.getConstraintTypeFromMappedType(Ꮡt)),
            b.f.NewInferTypeNode(b.f.NewTypeParameterDeclaration(nil, newTypeVariable.AsTypeReferenceNode().TypeName.Clone(new ast.NodeFactoryжNodeFactoryCoercible(b.f)), b.f.NewTypeOperatorNode(ast.KindKeyOfKeyword, Ꮡb.typeToTypeNode(b.ch.getModifiersTypeFromMappedType(Ꮡt))), nil, nil)),
            result,
            b.f.NewKeywordTypeNode(ast.KindNeverKeyword));
    }

    return result;
}

internal static ж<ast.Node> typePredicateToTypePredicateNode(this ж<NodeBuilderImpl> Ꮡb, ж<TypePredicate> Ꮡpredicate) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var predicate = ref Ꮡpredicate.DerefOrNull();

    ж<ast.Node> assertsModifier = default;
    if (predicate.kind == TypePredicateKindAssertsIdentifier || predicate.kind == TypePredicateKindAssertsThis) {
        assertsModifier = b.f.NewToken(ast.KindAssertsKeyword);
    }
    ж<ast.Node> parameterName = default;
    if (predicate.kind == TypePredicateKindIdentifier || predicate.kind == TypePredicateKindAssertsIdentifier) {
        parameterName = b.f.NewIdentifier(predicate.parameterName);
        b.e.AddEmitFlags(parameterName, printer.EFNoAsciiEscaping);
    } else {
        parameterName = b.f.NewThisTypeNode();
    }
    ж<ast.Node> typeNode = default;
    if (predicate.t != nil) {
        typeNode = Ꮡb.typeToTypeNode(predicate.t);
    }
    return b.f.NewTypePredicateNode(
        assertsModifier,
        parameterName,
        typeNode);
}

internal static ж<astꓸTypeNode> typeToTypeNodeHelperWithPossibleReusableTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<Type> t, ж<astꓸTypeNode> typeNode) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (t == nil) {
        return b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
    }
    if (!b.isActivelyExpanding() && typeNode != nil && b.getTypeFromTypeNode(typeNode, false) == t) {
        var reused = Ꮡb.tryReuseExistingNodeHelper(typeNode);
        if (reused != nil) {
            Ꮡb.checkTypeExpandability(t);
            return reused;
        }
    }
    return Ꮡb.typeToTypeNode(t);
}

internal static ж<ast.Node> typeParameterToDeclaration(this ж<NodeBuilderImpl> Ꮡb, ж<Type> parameter) {
    ref var b = ref Ꮡb.DerefOrNull();

    var constraint = b.ch.getConstraintOfTypeParameter(parameter);
    ж<ast.Node> constraintNode = default;
    if (constraint != nil) {
        constraintNode = Ꮡb.typeToTypeNodeHelperWithPossibleReusableTypeNode(constraint, b.ch.getConstraintDeclaration(parameter));
    }
    return Ꮡb.typeParameterToDeclarationWithConstraint(parameter, constraintNode);
}

internal static slice<ж<ast.Node>> symbolToTypeParameterDeclarations(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> symbol) {
    return Ꮡb.typeParametersToTypeParameterDeclarations(symbol);
}

internal static slice<ж<ast.Node>> typeParametersToTypeParameterDeclarations(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    var resultsᴷ = new stackAppend<ж<ast.Node>>(moves: true);
    var resultsᴷΔ1 = new stackAppend<ж<ast.Node>>(moves: true);

    var targetSymbol = b.ch.getTargetSymbol(Ꮡsymbol);
    if ((ast.SymbolFlags)(targetSymbol.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsClass | ast.SymbolFlagsInterface) | ast.SymbolFlagsAlias))) != 0) {
        slice<ж<ast.Node>> results = default;
        var @params = b.ch.getLocalTypeParametersOfClassOrInterfaceOrTypeAlias(Ꮡsymbol);
        foreach (var (_, param) in range(@params)) {
            results = append(resultsᴷ, results, Ꮡb.typeParameterToDeclaration(param));
        }
        results = move2heap(resultsᴷ, results);
        return results;
    } else if ((ast.SymbolFlags)(targetSymbol.Flags & ast.SymbolFlagsFunction) != 0) {
        slice<ж<ast.Node>> results = default;
        foreach (var (_, param) in range(b.ch.getTypeParametersFromDeclaration(symbol.ValueDeclaration))) {
            results = append(resultsᴷΔ1, results, Ꮡb.typeParameterToDeclaration(param));
        }
        results = move2heap(resultsᴷΔ1, results);
        return results;
    }
    return default;
}

} partial class checker_package {

internal static ж<ast.Node> getEffectiveParameterDeclaration(ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var parameterDeclaration = ast.GetDeclarationOfKind(Ꮡsymbol, ast.KindParameter);
    if (parameterDeclaration != nil) {
        return parameterDeclaration;
    }
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTransient) == 0) {
        return ast.GetDeclarationOfKind(Ꮡsymbol, ast.KindJSDocParameterTag);
    }
    return default;
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> symbolToParameterDeclaration(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> ᏑparameterSymbol, bool preserveModifierFlags) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var parameterSymbol = ref ᏑparameterSymbol.DerefOrNull();

    var parameterDeclaration = getEffectiveParameterDeclaration(ᏑparameterSymbol);

    var parameterType = b.ch.getTypeOfSymbol(ᏑparameterSymbol);
    var parameterTypeNode = Ꮡb.serializeTypeForDeclaration(parameterDeclaration, parameterType, ᏑparameterSymbol, true);
    ж<ast.ModifierList> modifiers = default;
    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsOmitParameterModifiers) == 0 && preserveModifierFlags && parameterDeclaration != nil && ast.CanHaveModifiers(parameterDeclaration)) {
        var originals = core.Filter<ж<ast.Node>>(parameterDeclaration.ModifierNodes(), ast.IsModifier);
        var clones = core.Map(originals, ж<ast.Node> (ж<ast.Node> node) => node.Clone(new ast.NodeFactoryжNodeFactoryCoercible(Ꮡb.Value.f)));
        if (len(clones) > 0) {
            modifiers = b.f.NewModifierList(clones);
        }
    }
    var isRest = parameterDeclaration != nil && isRestParameter(parameterDeclaration) || (ast.CheckFlags)(parameterSymbol.CheckFlags & ast.CheckFlagsRestParameter) != 0;
    ж<ast.Node> dotDotDotToken = default;
    if (isRest) {
        dotDotDotToken = b.f.NewToken(ast.KindDotDotDotToken);
    }
    var name = b.parameterToParameterDeclarationName(ᏑparameterSymbol, parameterDeclaration);
    var isOptional = parameterDeclaration != nil && b.ch.isOptionalParameter(parameterDeclaration) || (ast.CheckFlags)(parameterSymbol.CheckFlags & ast.CheckFlagsOptionalParameter) != 0;
    ж<ast.Node> questionToken = default;
    if (isOptional) {
        questionToken = b.f.NewToken(ast.KindQuestionToken);
    }

    var parameterNode = b.f.NewParameterDeclaration(
        modifiers,
        dotDotDotToken,
        name,
        questionToken,
        parameterTypeNode,
        nil);
    b.ctx.approximateLength += len(parameterSymbol.Name) + 3;
    return parameterNode;
}

internal static ж<ast.Node> parameterToParameterDeclarationName(this ref NodeBuilderImpl b, ж<ast.Symbol> ᏑparameterSymbol, ж<ast.Node> ᏑparameterDeclaration) {
    ref var parameterSymbol = ref ᏑparameterSymbol.DerefOrNull();
    ref var parameterDeclaration = ref ᏑparameterDeclaration.DerefOrNull();

    if (ᏑparameterDeclaration == nil || parameterDeclaration.Name() == nil) {
        return b.newIdentifier(parameterSymbol.Name, ᏑparameterSymbol);
    }

    var name = parameterDeclaration.Name();
    switch (name.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        var cloned = b.f.DeepCloneNode(name);
        b.e.SetEmitFlags(cloned, printer.EFNoAsciiEscaping);
        b.idToSymbol[cloned] = ᏑparameterSymbol;
        return cloned;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        var cloned = b.f.DeepCloneNode(name.AsQualifiedName().Right);
        b.e.SetEmitFlags(cloned, printer.EFNoAsciiEscaping);
        b.idToSymbol[cloned] = ᏑparameterSymbol;
        return cloned;
    }
    default: {
        return b.cloneBindingName(name);
    }}

}

internal static ж<ast.Node> cloneBindingName(this ref NodeBuilderImpl b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsComputedPropertyName(Ꮡnode) && b.ch.isLateBindableName(Ꮡnode)) {
        b.trackComputedName(node.Expression(), b.ctx.enclosingDeclaration);
    }

    var visited = b.cloneBindingNameVisitor.VisitEachChild(Ꮡnode);

    if (ast.IsBindingElement(visited)) {
        var bindingElement = visited.AsBindingElement();
        visited = b.f.UpdateBindingElement(
            bindingElement,
            bindingElement.DotDotDotToken,
            (~bindingElement).PropertyName,
            bindingElement.Name(),
            nil);
    }

    if (!ast.NodeIsSynthesized(visited)) {
        visited = b.f.DeepCloneNode(visited);
    }

    b.e.SetEmitFlags(visited, (printer.EmitFlags)(printer.EFSingleLine | printer.EFNoAsciiEscaping));
    return visited;
}

internal static ж<ast.Node> serializeTypeForExpression(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> expr) {
    ref var b = ref Ꮡb.DerefOrNull();

    var t = b.ch.instantiateType(b.ch.getWidenedType(b.ch.getRegularTypeOfExpression(expr)), b.ctx.mapper);
    return Ꮡb.typeToTypeNode(t);
}

internal static ж<ast.Node> serializeInferredReturnTypeForSignature(this ж<NodeBuilderImpl> Ꮡb, ж<Signature> signature, ж<Type> returnType) {
    ref var b = ref Ꮡb.DerefOrNull();

    var oldSuppressReportInferenceFallback = b.ctx.suppressReportInferenceFallback;
    b.ctx.suppressReportInferenceFallback = true;
    var typePredicate = b.ch.getTypePredicateOfSignature(signature);
    ж<ast.Node> returnTypeNode = default;
    if (typePredicate != nil) {
        ж<TypePredicate> predicate = default;
        if (b.ctx.mapper != nil) {
            predicate = b.ch.instantiateTypePredicate(typePredicate, b.ctx.mapper);
        } else {
            predicate = typePredicate;
        }
        returnTypeNode = Ꮡb.typePredicateToTypePredicateNodeHelper(predicate);
    } else {
        returnTypeNode = Ꮡb.typeToTypeNode(returnType);
    }
    b.ctx.suppressReportInferenceFallback = oldSuppressReportInferenceFallback;
    return returnTypeNode;
}

internal static ж<ast.Node> typePredicateToTypePredicateNodeHelper(this ж<NodeBuilderImpl> Ꮡb, ж<TypePredicate> ᏑtypePredicate) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var typePredicate = ref ᏑtypePredicate.DerefOrNull();

    ж<ast.Node> assertsModifier = default;
    if (typePredicate.kind == TypePredicateKindAssertsThis || typePredicate.kind == TypePredicateKindAssertsIdentifier) {
        assertsModifier = b.f.NewToken(ast.KindAssertsKeyword);
    } else {
        assertsModifier = default;
    }
    ж<ast.Node> parameterName = default;
    if (typePredicate.kind == TypePredicateKindIdentifier || typePredicate.kind == TypePredicateKindAssertsIdentifier) {
        parameterName = b.newIdentifier(typePredicate.parameterName, nil);
        b.e.SetEmitFlags(parameterName, printer.EFNoAsciiEscaping);
    } else {
        parameterName = b.f.NewThisTypeNode();
    }
    ж<ast.Node> typeNode = default;
    if (typePredicate.t != nil) {
        typeNode = Ꮡb.typeToTypeNode(typePredicate.t);
    }
    return b.f.NewTypePredicateNode(assertsModifier, parameterName, typeNode);
}

} partial class checker_package {

partial struct SignatureToSignatureDeclarationOptions {
    internal slice<ж<ast.Node>> modifiers;
    internal ж<astꓸPropertyName> name;
    internal ж<ast.Node> questionToken;
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> signatureToSignatureDeclarationHelper(this ж<NodeBuilderImpl> Ꮡb, ж<Signature> Ꮡsignature, ast.Kind kind, ж<SignatureToSignatureDeclarationOptions> Ꮡoptions) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();
    ref var options = ref Ꮡoptions.DerefOrNull();

    slice<ж<ast.Node>> typeParameters = default;

    var (expandedParams, cleanup) = Ꮡb.enterSignatureScope(Ꮡsignature);
    b.ctx.approximateLength += 3;

    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteTypeArgumentsOfSignature) != 0 && signature.target != nil && signature.mapper != nil && len(signature.target.typeParameters) != 0) {
        foreach (var (_, parameter) in range(signature.target.typeParameters)) {
            typeParameters = append(typeParameters, Ꮡb.typeToTypeNode(b.ch.instantiateType(parameter, signature.mapper)));
        }
    } else {
        foreach (var (_, parameter) in range(signature.typeParameters)) {
            typeParameters = append(typeParameters, Ꮡb.typeParameterToDeclaration(parameter));
        }
    }

    var restoreFlags = Ꮡb.saveRestoreFlags();
    b.ctx.flags &= unchecked((nodebuilder.Flags)~(nodebuilder.Flags)(nodebuilder.FlagsSuppressAnyReturnType));
    var expandedParamsʗ1 = expandedParams;

    var parameters = core.Map(core.IfElse(core.Some(expandedParams, (ж<ast.Symbol> p) => p != expandedParamsʗ1[len(expandedParamsʗ1) - 1] && (ast.CheckFlags)(p.CheckFlags & ast.CheckFlagsRestParameter) != 0), signature.parameters, expandedParams), ж<ast.Node> (ж<ast.Symbol> parameter) => Ꮡb.symbolToParameterDeclaration(parameter, kind == ast.KindConstructor));
    ж<ast.Node> thisParameter = default;
    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsOmitThisParameter) != 0) {
        thisParameter = default;
    } else {
        thisParameter = Ꮡb.tryGetThisParameterDeclaration(Ꮡsignature);
    }
    if (thisParameter != nil) {
        parameters = appendꓸꓸꓸ(slice<ж<ast.Node>>([thisParameter]), parameters);
    }
    restoreFlags();

    var returnTypeNode = Ꮡb.serializeReturnTypeForSignature(Ꮡsignature, true);

    slice<ж<ast.Node>> modifiers = default;
    if (Ꮡoptions != nil) {
        modifiers = options.modifiers;
    }
    if ((kind == ast.KindConstructorType) && (SignatureFlags)(signature.flags & SignatureFlagsAbstract) != 0) {
        var flags = ast.ModifiersToFlags(modifiers);
        modifiers = ast.CreateModifiersFromModifierFlags((ast.ModifierFlags)(flags | ast.ModifierFlagsAbstract), b.f.NewModifier);
    }

    var paramList = b.f.NewNodeList(parameters);
    ж<ast.NodeList> typeParamList = default;
    if (len(typeParameters) != 0) {
        typeParamList = b.f.NewNodeList(typeParameters);
    }
    ж<ast.ModifierList> modifierList = default;
    if (len(modifiers) > 0) {
        modifierList = b.f.NewModifierList(modifiers);
    }
    ж<ast.Node> name = default;
    if (Ꮡoptions != nil) {
        name = options.name;
    }
    if (name == nil) {
        name = b.f.NewIdentifier("");
    }

    ж<ast.Node> node = default;
    switch (ᐧ) {
    case {} when kind == ast.KindCallSignature: {
        node = b.f.NewCallSignatureDeclaration(typeParamList, paramList, returnTypeNode);
        break;
    }
    case {} when kind == ast.KindConstructSignature: {
        node = b.f.NewConstructSignatureDeclaration(typeParamList, paramList, returnTypeNode);
        break;
    }
    case {} when kind == ast.KindMethodSignature: {
        ж<ast.Node> questionToken = default;
        if (Ꮡoptions != nil) {
            questionToken = options.questionToken;
        }
        node = b.f.NewMethodSignatureDeclaration(modifierList, name, questionToken, typeParamList, paramList, returnTypeNode);
        break;
    }
    case {} when kind == ast.KindMethodDeclaration: {
        node = b.f.NewMethodDeclaration(modifierList, nil, name, nil, typeParamList, paramList, returnTypeNode, nil, nil);
        break;
    }
    case {} when kind == ast.KindConstructor: {
        node = b.f.NewConstructorDeclaration(modifierList, nil, paramList, nil, nil, nil);
        break;
    }
    case {} when kind == ast.KindGetAccessor: {
        node = b.f.NewGetAccessorDeclaration(modifierList, name, nil, paramList, returnTypeNode, nil, nil);
        break;
    }
    case {} when kind == ast.KindSetAccessor: {
        node = b.f.NewSetAccessorDeclaration(modifierList, name, nil, paramList, nil, nil, nil);
        break;
    }
    case {} when kind == ast.KindIndexSignature: {
        node = b.f.NewIndexSignatureDeclaration(modifierList, paramList, returnTypeNode);
        break;
    }
    case {} when kind == ast.KindFunctionType: {
        if (returnTypeNode == nil) {
            returnTypeNode = b.f.NewTypeReferenceNode(b.f.NewIdentifier(""), nil);
        }
        node = b.f.NewFunctionTypeNode(typeParamList, paramList, returnTypeNode);
        break;
    }
    case {} when kind == ast.KindConstructorType: {
        if (returnTypeNode == nil) {
            returnTypeNode = b.f.NewTypeReferenceNode(b.f.NewIdentifier(""), nil);
        }
        node = b.f.NewConstructorTypeNode(modifierList, typeParamList, paramList, returnTypeNode);
        break;
    }
    case {} when kind == ast.KindFunctionDeclaration: {
        node = b.f.NewFunctionDeclaration(modifierList, nil, name, typeParamList, paramList, returnTypeNode, nil, nil);
        break;
    }
    case {} when kind == ast.KindFunctionExpression: {
        node = b.f.NewFunctionExpression(modifierList, nil, name, typeParamList, paramList, returnTypeNode, nil, b.f.NewBlock(b.f.NewNodeList([]), false));
        break;
    }
    case {} when kind == ast.KindArrowFunction: {
        node = b.f.NewArrowFunction(modifierList, typeParamList, paramList, returnTypeNode, nil, nil, b.f.NewBlock(b.f.NewNodeList([]), false));
        break;
    }
    default: {
        throw panic("Unhandled kind in signatureToSignatureDeclarationHelper");
        break;
    }}

    cleanup();
    return node;
}

} partial class checkerᴛChecker {

internal static slice<slice<ж<ast.Symbol>>> getExpandedParameters(this ж<Checker> Ꮡc, ж<Signature> Ꮡsig, bool skipUnionExpanding) {
    ref var sig = ref Ꮡsig.DerefOrNull();

    if (signatureHasRestParameter(ref (Ꮡsig).DerefOrNull())) {
        nint restIndex = len(sig.parameters) - 1;
        var restSymbol = sig.parameters[restIndex];
        var restType = Ꮡc.getTypeOfSymbol(restSymbol);
        slice<@string> getUniqAssociatedNamesFromTupleType(ж<Type> t, ж<ast.Symbol> restSymbolΔ1) {
            var names = core.MapIndex(t.Target().AsTupleType().elementInfos, @string (TupleElementInfo info, nint i) => Ꮡc.Value.getTupleElementLabel(info, restSymbolΔ1, i));
            if (len(names) > 0) {
                var duplicates = slice<nint>([]);
                var uniqueNames = new map<@string, bool>();
                foreach (var (i, name) in range(names)) {
                    var (_, ok) = uniqueNames[name, ꟷ];
                    if (ok) {
                        duplicates = append(duplicates, i);
                    } else {
                        uniqueNames[name] = true;
                    }
                }
                var counters = new map<@string, nint>();
                foreach (var (_, i) in range(duplicates)) {
                    var (counter, ok) = counters[names[i], ꟷ];
                    if (!ok) {
                        counter = 1;
                    }
                    @string name = default;
                    while (true) {
                        name = fmt.Sprintf("%s_%d"u8, names[i], counter);
                        var (_, okΔ1) = uniqueNames[name, ꟷ];
                        if (okΔ1) {
                            counter++;
                            continue;
                        } else {
                            uniqueNames[name] = true;
                            break;
                        }
                    }
                    names[i] = name;
                    counters[names[i]] = counter + 1;
                }
            }
            return names;
        }
        var getUniqAssociatedNamesFromTupleTypeʗ1 = getUniqAssociatedNamesFromTupleType;
        slice<ж<ast.Symbol>> expandSignatureParametersWithTupleMembers(ж<Type> restTypeΔ1, nint restIndexΔ1, ж<ast.Symbol> restSymbolΔ2) {
            var elementTypes = Ꮡc.getTypeArguments(restTypeΔ1);
            var associatedNames = getUniqAssociatedNamesFromTupleTypeʗ1(restTypeΔ1, restSymbolΔ2);
            var associatedNamesʗ1 = associatedNames;
            var restParams = core.MapIndex(elementTypes, ж<ast.Symbol> (ж<Type> t, nint i) => {
                @string name = associatedNamesʗ1[i];
                var flags = restTypeΔ1.Target().AsTupleType().elementInfos[i].flags;
                ast.CheckFlags checkFlags = default;
                switch (ᐧ) {
                case {} when (ElementFlags)(flags & ElementFlagsVariable) != 0: {
                    checkFlags = ast.CheckFlagsRestParameter;
                    break;
                }
                case {} when (ElementFlags)(flags & ElementFlagsOptional) != 0: {
                    checkFlags = ast.CheckFlagsOptionalParameter;
                    break;
                }}

                var symbol = Ꮡc.Value.newSymbolEx(ast.SymbolFlagsFunctionScopedVariable, name, checkFlags);
                var links = Ꮡc.Value.valueSymbolLinks.Get(symbol);
                if ((ElementFlags)(flags & ElementFlagsRest) != 0) {
                    links.resolvedType = Ꮡc.createArrayType(t);
                } else {
                    links.resolvedType = t;
                }
                return symbol;
            });
            return core.Concatenate(Ꮡsig.Value.parameters.slice(0, restIndexΔ1), restParams);
        }

        if (isTupleType(restType)) {
            return slice<slice<ж<ast.Symbol>>>([expandSignatureParametersWithTupleMembers(restType, restIndex, restSymbol)]);
        } else if (!skipUnionExpanding && (TypeFlags)(restType.flags & TypeFlagsUnion) != 0 && core.Every<ж<Type>>(restType.AsUnionType().types, isTupleType)) {
            var expandSignatureParametersWithTupleMembersʗ1 = expandSignatureParametersWithTupleMembers;
            var restSymbolʗ1 = restSymbol;
            return core.Map(restType.AsUnionType().types, slice<ж<ast.Symbol>> (ж<Type> t) => expandSignatureParametersWithTupleMembersʗ1(t, restIndex, restSymbolʗ1));
        }
    }
    return [sig.parameters];
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> tryGetThisParameterDeclaration(this ж<NodeBuilderImpl> Ꮡb, ж<Signature> Ꮡsignature) {
    ref var signature = ref Ꮡsignature.DerefOrNull();

    if (signature.thisParameter != nil) {
        return Ꮡb.symbolToParameterDeclaration(signature.thisParameter, false);
    }
    if (signature.declaration != nil && ast.IsInJSFile(signature.declaration)) {
    }
    return default;
}

internal static ж<ast.Node> serializeReturnTypeForSignature(this ж<NodeBuilderImpl> Ꮡb, ж<Signature> Ꮡsignature, bool tryReuse) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();

    var suppressAny = (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsSuppressAnyReturnType) != 0;
    var restoreFlags = Ꮡb.saveRestoreFlags();
    if (suppressAny) {
        b.ctx.flags &= unchecked((nodebuilder.Flags)~(nodebuilder.Flags)(nodebuilder.FlagsSuppressAnyReturnType));
    }
    ж<ast.Node> returnTypeNode = default;

    ж<Type> returnType = default;
    if (signature.declaration != nil && !ast.NodeIsSynthesized(signature.declaration)) {
        var symbol = b.ch.getSymbolOfDeclaration(signature.declaration);
        bool ok = default;
        (returnType, ok) = b.ctx.enclosingSymbolTypes[ast.GetSymbolId(symbol), ꟷ];
        if (!ok || returnType == nil) {
            returnType = b.ch.instantiateType(b.ch.getReturnTypeOfSignature(Ꮡsignature), b.ctx.mapper);
        }
    } else {
        returnType = b.ch.getReturnTypeOfSignature(Ꮡsignature);
    }
    if (!(suppressAny && IsTypeAny(returnType))) {
        if (!b.isActivelyExpanding() && tryReuse && b.ctx.enclosingDeclaration != nil && signature.declaration != nil && !ast.NodeIsSynthesized(signature.declaration)) {
            var declarationSymbol = b.ch.getSymbolOfDeclaration(signature.declaration);
            var restore = Ꮡb.addSymbolTypeToContext(declarationSymbol, returnType);
            var pt = b.pc.GetReturnTypeOfSignature(signature.declaration);
            if (b.pseudoTypeEquivalentToType(pt, returnType, false, !b.ctx.suppressReportInferenceFallback)) {
                var typePredicate = b.ch.getTypePredicateOfSignature(Ꮡsignature);
                if (typePredicate != nil && !b.pseudoReturnTypeMatchesPredicate(pt, typePredicate)) {
                    if (!b.ctx.suppressReportInferenceFallback) {
                        b.ctx.tracker.ReportInferenceFallback(signature.declaration);
                    }
                    pt = default;
                }
                if (pt != nil) {
                    returnTypeNode = Ꮡb.pseudoTypeToNodeWithCheckerFallback(pt, returnType);
                }
            }
            restore();
        }
        if (returnTypeNode == nil) {
            returnTypeNode = Ꮡb.serializeInferredReturnTypeForSignature(Ꮡsignature, returnType);
        }
    }

    if (returnTypeNode == nil && !suppressAny) {
        returnTypeNode = b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
    }
    restoreFlags();
    return returnTypeNode;
}

internal static bool isTriviallySerializableComputedName(this ref NodeBuilderImpl b, ж<ast.Node> Ꮡe) {
    ref var e = ref Ꮡe.DerefOrNull();

    var shapeGood = Ꮡe != nil && e.Name() != nil && ast.IsComputedPropertyName(e.Name()) && ast.IsEntityNameExpression(e.Name().Expression());
    if (!shapeGood) {
        return false;
    }
    return b.ch.GetEmitResolver().isEntityNameVisible(e.Name().Expression(), b.ctx.enclosingDeclaration, false).Accessibility == printer.SymbolAccessibilityAccessible;
}

internal static slice<ж<ast.Node>> indexInfoToObjectComputedNamesOrSignatureDeclaration(this ж<NodeBuilderImpl> Ꮡb, ж<IndexInfo> ᏑindexInfo, ж<astꓸTypeNode> typeNode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var indexInfo = ref ᏑindexInfo.DerefOrNull();

    if (len(indexInfo.components) > 0) {
        var allComponentComputedNamesSerializable = b.ctx.enclosingDeclaration != nil && core.Every<ж<ast.Node>>(indexInfo.components, Ꮡb.isTriviallySerializableComputedName);
        if (allComponentComputedNamesSerializable) {
            var newComponents = core.Filter(indexInfo.components, (ж<ast.Node> c) => !Ꮡb.Value.ch.hasLateBindableName(c));
            var bailed = false;
            var results = core.Map(newComponents, ж<ast.Node> (ж<ast.Node> e) => {
                var name = Ꮡb.reuseNode(e.Name());
                if (name != nil) {
                    Ꮡb.Value.trackComputedName(e.Name().Expression(), Ꮡb.Value.ctx.enclosingDeclaration);
                    ж<ast.ModifierList> mods = default;
                    if (ᏑindexInfo.Value.isReadonly) {
                        mods = Ꮡb.Value.f.NewModifierList(slice<ж<ast.Node>>([Ꮡb.Value.f.NewModifier(ast.KindReadonlyKeyword)]));
                    }
                    ж<ast.Node> postfixToken = default;
                    if (e.PostfixToken() != nil) {
                        postfixToken = e.PostfixToken().Clone(new ast.NodeFactoryжNodeFactoryCoercible(Ꮡb.Value.f));
                    }
                    ж<astꓸTypeNode> currentTypeNode = default;
                    if (typeNode != nil) {
                        currentTypeNode = Ꮡb.Value.f.DeepCloneNode(typeNode);
                    } else {
                        currentTypeNode = Ꮡb.typeToTypeNode(Ꮡb.Value.ch.getTypeOfSymbol(e.Symbol()));
                    }
                    var sig = Ꮡb.Value.f.NewPropertySignatureDeclaration(
                        mods,
                        name,
                        postfixToken,
                        currentTypeNode,
                        nil);
                    sig.Loc = e.Loc;
                    return sig;
                }
                bailed = true;
                return default;
            });
            if (!bailed) {
                return results;
            }
        }
    }
    return slice<ж<ast.Node>>([Ꮡb.indexInfoToIndexSignatureDeclarationHelper(ᏑindexInfo, typeNode)]);
}

internal static ж<ast.Node> indexInfoToIndexSignatureDeclarationHelper(this ж<NodeBuilderImpl> Ꮡb, ж<IndexInfo> ᏑindexInfo, ж<astꓸTypeNode> ᏑtypeNode) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var indexInfo = ref ᏑindexInfo.DerefOrNull();
    ref var typeNode = ref ᏑtypeNode.DerefOrNull();

    @string name = getNameFromIndexInfo(ref (ᏑindexInfo).DerefOrNull());
    var indexerTypeNode = Ꮡb.typeToTypeNode(indexInfo.keyType);

    var indexingParameter = b.f.NewParameterDeclaration(nil, nil, b.newIdentifier(name, nil), nil, indexerTypeNode, nil);
    if (ᏑtypeNode == nil) {
        if (indexInfo.valueType == nil) {
            ᏑtypeNode = b.f.NewKeywordTypeNode(ast.KindAnyKeyword); typeNode = ref ᏑtypeNode.DerefOrNull();
        } else {
            ᏑtypeNode = Ꮡb.typeToTypeNode(indexInfo.valueType); typeNode = ref ᏑtypeNode.DerefOrNull();
        }
    }
    if (indexInfo.valueType == nil && (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowEmptyIndexInfoType) == 0) {
        b.ctx.encounteredError = true;
    }
    b.ctx.approximateLength += len(name) + 4;
    ж<ast.ModifierList> modifiers = default;
    if (indexInfo.isReadonly) {
        b.ctx.approximateLength += 9;
        modifiers = b.f.NewModifierList(slice<ж<ast.Node>>([b.f.NewModifier(ast.KindReadonlyKeyword)]));
    }
    return b.f.NewIndexSignatureDeclaration(modifiers, b.f.NewNodeList([indexingParameter]), ᏑtypeNode);
}

} partial class checker_package {

internal static bool hasTypeAnnotation(ж<astꓸDeclaration> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    if (Ꮡdeclaration == nil || declaration.Type() == nil) {
        return false;
    }
    if (ast.IsTypeAliasDeclaration(Ꮡdeclaration) || ast.IsJSTypeAliasDeclaration(Ꮡdeclaration)) {
        return false;
    }
    return true;
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> serializeTypeForDeclaration(this ж<NodeBuilderImpl> Ꮡb, ж<astꓸDeclaration> Ꮡdeclaration, ж<Type> Ꮡt, ж<ast.Symbol> Ꮡsymbol, bool tryReuse) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡdeclaration == nil) {
        if (Ꮡsymbol != nil) {
            Ꮡdeclaration = symbol.ValueDeclaration; declaration = ref Ꮡdeclaration.DerefOrNull();
            if (Ꮡdeclaration == nil) {
                Ꮡdeclaration = core.FirstOrNil(symbol.Declarations); declaration = ref Ꮡdeclaration.DerefOrNull();
            }
        }
    }
    if (Ꮡsymbol == nil) {
        Ꮡsymbol = b.ch.getSymbolOfDeclaration(Ꮡdeclaration); symbol = ref Ꮡsymbol.DerefOrNull();
    }
    if (Ꮡt == nil) {
        if (Ꮡsymbol == nil) {
            if (ast.IsVariableLike(Ꮡdeclaration)) {
                Ꮡt = b.ch.getTypeForVariableLikeDeclaration(Ꮡdeclaration, false, CheckModeNormal); t = ref Ꮡt.DerefOrNull();
            } else {
                Ꮡt = b.ch.errorType; t = ref Ꮡt.DerefOrNull();
            }
        } else {
            Ꮡt = b.ctx.enclosingSymbolTypes[ast.GetSymbolId(Ꮡsymbol)]; t = ref Ꮡt.DerefOrNull();
            if (Ꮡt == nil) {
                if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAccessor) != 0 && declaration.Kind == ast.KindSetAccessor) {
                    Ꮡt = b.ch.instantiateType(b.ch.getWriteTypeOfSymbol(Ꮡsymbol), b.ctx.mapper); t = ref Ꮡt.DerefOrNull();
                } else if (Ꮡsymbol != nil && ((ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsTypeLiteral | ast.SymbolFlagsSignature))) == 0)) {
                    Ꮡt = b.ch.instantiateType(b.ch.getWidenedLiteralType(b.ch.getTypeOfSymbol(Ꮡsymbol)), b.ctx.mapper); t = ref Ꮡt.DerefOrNull();
                } else {
                    Ꮡt = b.ch.errorType; t = ref Ꮡt.DerefOrNull();
                }
            }
        }
    }

    var requiresAddingUndefined = Ꮡdeclaration != nil && (ast.IsParameterDeclaration(Ꮡdeclaration) || ast.IsPropertySignatureDeclaration(Ꮡdeclaration) || ast.IsPropertyDeclaration(Ꮡdeclaration)) && b.ch.GetEmitResolver().requiresAddingImplicitUndefined(Ꮡdeclaration, Ꮡsymbol, b.ctx.enclosingDeclaration);
    var addUndefinedForParameter = requiresAddingUndefined && (ast.IsParameterDeclaration(Ꮡdeclaration));
    if (addUndefinedForParameter) {
        Ꮡt = b.ch.getOptionalType(Ꮡt, false); t = ref Ꮡt.DerefOrNull();
    }

    var restoreFlags = Ꮡb.saveRestoreFlags();
    if ((TypeFlags)(t.flags & TypeFlagsUniqueESSymbol) != 0 && t.symbol == Ꮡsymbol && (b.ctx.enclosingDeclaration == nil || core.Some(symbol.Declarations, (ж<astꓸDeclaration> d) => ast.GetSourceFileOfNode(d) == Ꮡb.Value.ctx.enclosingFile))) {
        b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsAllowUniqueESSymbolType);
    }
    ж<ast.Node> result = default;
    bool reportedInferenceFallback = default;
    if (!b.isActivelyExpanding() && tryReuse && b.ctx.enclosingDeclaration != nil && Ꮡdeclaration != nil && (ast.IsAccessor(Ꮡdeclaration) || (ast.HasInferredType(Ꮡdeclaration) && !ast.NodeIsSynthesized(Ꮡdeclaration) && ((ObjectFlags)(t.ObjectFlags() & ObjectFlagsRequiresWidening)) == 0))) {
        Action remove = default;
        if (Ꮡsymbol != nil) {
            remove = Ꮡb.addSymbolTypeToContext(Ꮡsymbol, Ꮡt);
        }
        ж<pseudochecker.PseudoType> pt = default;
        if (ast.IsAccessor(Ꮡdeclaration)) {
            pt = b.pc.GetTypeOfAccessor(Ꮡdeclaration);
        } else {
            pt = b.pc.GetTypeOfDeclaration(Ꮡdeclaration);
        }
        if ((pt == nil || pt.Kind == pseudochecker.PseudoTypeKindNoResult) && ast.IsBinaryExpression(Ꮡdeclaration) && Ꮡsymbol != nil) {
            var decl = core.Find<ж<ast.Node>>(symbol.Declarations, hasTypeAnnotation); if (decl != nil) {
                pt = b.pc.GetTypeOfDeclaration(decl);
            }
        }
        var reportErrors = !b.ctx.suppressReportInferenceFallback;
        if (b.pseudoTypeEquivalentToType(pt, Ꮡt, !requiresAddingUndefined && (ast.IsParameterDeclaration(Ꮡdeclaration) || ast.IsPropertySignatureDeclaration(Ꮡdeclaration) || ast.IsPropertyDeclaration(Ꮡdeclaration)) && isOptionalDeclaration(Ꮡdeclaration), reportErrors)) {
            var ptt = b.pseudoTypeToType(pt);
            if (ptt != nil && requiresAddingUndefined && containsNonMissingUndefinedType(ref (b.ch).DerefOrNull(), Ꮡt) && !containsNonMissingUndefinedType(ref (b.ch).DerefOrNull(), ptt)) {
                pt = pseudochecker.NewPseudoTypeUnion([pt, pseudochecker.PseudoTypeUndefined]);
            }
            result = Ꮡb.pseudoTypeToNodeWithCheckerFallback(pt, Ꮡt);
        } else {
            reportedInferenceFallback = reportErrors && pt.Kind == pseudochecker.PseudoTypeKindInferred && len(pt.AsPseudoTypeInferred().ErrorNodes) > 0;
            var shouldAddUndefined = false;
            if (requiresAddingUndefined) {
                var ptt = b.pseudoTypeToType(pt); if (ptt != nil) {
                    shouldAddUndefined = !containsNonMissingUndefinedType(ref (b.ch).DerefOrNull(), ptt);
                } else {
                    shouldAddUndefined = !pseudochecker.CouldAlreadyReferToUndefinedType(pt);
                }
            }
            if (shouldAddUndefined) {
                pt = pseudochecker.NewPseudoTypeUnion([pt, pseudochecker.PseudoTypeUndefined]);
                if (b.pseudoTypeEquivalentToType(pt, Ꮡt, false, reportErrors)) {
                    result = Ꮡb.pseudoTypeToNodeWithCheckerFallback(pt, Ꮡt);
                    reportedInferenceFallback = false;
                }
            }
        }
        if (remove != nil) {
            remove();
        }
    }
    if (result == nil) {
        if (reportedInferenceFallback) {
            var oldSuppress = b.ctx.suppressReportInferenceFallback;
            b.ctx.suppressReportInferenceFallback = true;
            result = Ꮡb.typeToTypeNode(Ꮡt);
            b.ctx.suppressReportInferenceFallback = oldSuppress;
        } else {
            result = Ꮡb.typeToTypeNode(Ꮡt);
        }
    }
    restoreFlags();
    if (result == nil) {
        return b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
    }
    return result;
}

} partial class checker_package {

public static UntypedInt MAX_REVERSE_MAPPED_NESTING_INSPECTION_DEPTH => 3;

} partial class checkerᴛNodeBuilderImpl {

internal static bool shouldUsePlaceholderForProperty(this ref NodeBuilderImpl b, ж<ast.Symbol> ᏑpropertySymbol) {
    ref var propertySymbol = ref ᏑpropertySymbol.DerefOrNull();

    if ((ast.CheckFlags)(propertySymbol.CheckFlags & ast.CheckFlagsReverseMapped) == 0) {
        return false;
    }
    if (slices.Contains(b.ctx.reverseMappedStack, ᏑpropertySymbol)) {
        return true;
    }
    if (len(b.ctx.reverseMappedStack) > 0) {
        var last = b.ctx.reverseMappedStack[len(b.ctx.reverseMappedStack) - 1];
        if (b.ch.ᏑReverseMappedSymbolLinks.Has(last)) {
            var links = b.ch.ᏑReverseMappedSymbolLinks.TryGet(last);
            var propertyType = links.propertyType;
            if (propertyType != nil && (ObjectFlags)(propertyType.objectFlags & ObjectFlagsAnonymous) == 0) {
                return true;
            }
        }
    }
    if (len(b.ctx.reverseMappedStack) < MAX_REVERSE_MAPPED_NESTING_INSPECTION_DEPTH) {
        return false;
    }
    if (!b.ch.ᏑReverseMappedSymbolLinks.Has(ᏑpropertySymbol)) {
        return false;
    }
    var propertyLinks = b.ch.ᏑReverseMappedSymbolLinks.TryGet(ᏑpropertySymbol);
    var propMappedType = propertyLinks.mappedType;
    if (propMappedType == nil || propMappedType.symbol == nil) {
        return false;
    }
    foreach (var i in range(len(b.ctx.reverseMappedStack))) {
        if (i > MAX_REVERSE_MAPPED_NESTING_INSPECTION_DEPTH) {
            break;
        }
        var prop = b.ctx.reverseMappedStack[len(b.ctx.reverseMappedStack) - 1 - i];
        if (b.ch.ᏑReverseMappedSymbolLinks.Has(prop)) {
            var links = b.ch.ᏑReverseMappedSymbolLinks.TryGet(prop);
            var mappedType = links.mappedType;
            if (mappedType != nil && mappedType.symbol == propMappedType.symbol) {
                return true;
            }
        }
    }
    return false;
}

internal static void trackComputedName(this ref NodeBuilderImpl b, ж<ast.Node> accessExpression, ж<ast.Node> enclosingDeclaration) {
    var firstIdentifier = ast.GetFirstIdentifier(accessExpression);
    var name = b.ch.resolveName(enclosingDeclaration, firstIdentifier.Text(), (ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsExportValue), nil, true, false);
    if (name != nil) {
        b.ctx.tracker.TrackSymbol(name, enclosingDeclaration, ast.SymbolFlagsValue);
    } else {
        var fallback = b.ch.resolveName(firstIdentifier, firstIdentifier.Text(), (ast.SymbolFlags)(ast.SymbolFlagsValue | ast.SymbolFlagsExportValue), nil, true, false);
        if (fallback != nil) {
            b.ctx.tracker.TrackSymbol(fallback, enclosingDeclaration, ast.SymbolFlagsValue);
        }
    }
}

} partial class checker_package {

partial struct propertyNameNodeKind /*num:nint*/;

internal static propertyNameNodeKind propertyNameNodeKindIdentifier => /* iota */ 0;
internal static propertyNameNodeKind propertyNameNodeKindNumericLiteral => 1;
internal static propertyNameNodeKind propertyNameNodeKindStringLiteral => 2;

internal static propertyNameNodeKind classifyPropertyName(@string name, bool stringNamed, bool isMethod) {
    if (isMethod && name == "new"u8) {
        return propertyNameNodeKindStringLiteral;
    }
    if (scanner.IsIdentifierText(name, core.LanguageVariantStandard)) {
        return propertyNameNodeKindIdentifier;
    }
    return core.IfElse(!stringNamed && isNumericLiteralName(name) && jsnum.FromString(name) >= 0D, propertyNameNodeKindNumericLiteral, propertyNameNodeKindStringLiteral);
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> createPropertyNameNodeForIdentifierOrLiteral(this ref NodeBuilderImpl b, @string name, bool singleQuote, bool stringNamed, bool isMethod, ж<ast.Symbol> symbol) {
    switch (classifyPropertyName(name, stringNamed, isMethod)) {
    case var ᴛ when ᴛ == propertyNameNodeKindIdentifier: {
        return b.newIdentifier(name, symbol);
    }
    case var ᴛ when ᴛ == propertyNameNodeKindNumericLiteral: {
        return b.f.NewNumericLiteral(name, ast.TokenFlagsNone);
    }
    default: {
        return b.f.NewStringLiteral(name, core.IfElse(singleQuote, ast.TokenFlagsSingleQuote, ast.TokenFlagsNone));
    }}

}

internal static bool isStringNamed(this ref NodeBuilderImpl b, ж<astꓸDeclaration> d) {
    var name = ast.GetNameOfDeclaration(d);
    if (name == nil) {
        return false;
    }
    if (ast.IsComputedPropertyName(name)) {
        var t = b.ch.checkExpression(name.Expression());
        return (TypeFlags)(t.flags & TypeFlagsStringLike) != 0;
    }
    if (ast.IsElementAccessExpression(name)) {
        var t = b.ch.checkExpression(name.AsElementAccessExpression().ArgumentExpression);
        return (TypeFlags)(t.flags & TypeFlagsStringLike) != 0;
    }
    return ast.IsStringLiteral(name);
}

internal static bool isSingleQuotedStringNamed(this ref NodeBuilderImpl b, ж<astꓸDeclaration> d) {
    var name = ast.GetNameOfDeclaration(d);
    return name != nil && ast.IsStringLiteral(name) && (ast.TokenFlags)(name.AsStringLiteral().TokenFlags & ast.TokenFlagsSingleQuote) != 0;
}

internal static ж<ast.Node> getPropertyNameNodeForSymbol(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> enclosingDeclaration) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (symbol.ValueDeclaration != nil) {
        var declName = symbol.ValueDeclaration.Name();
        if (declName != nil && ast.IsPrivateIdentifier(declName)) {
            return b.f.DeepCloneNode(declName);
        }
    }
    var stringNamed = len(symbol.Declarations) != 0 && core.Every<ж<ast.Node>>(symbol.Declarations, Ꮡb.isStringNamed);
    var singleQuote = len(symbol.Declarations) != 0 && core.Every<ж<ast.Node>>(symbol.Declarations, Ꮡb.isSingleQuotedStringNamed);
    var isMethod = (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsMethod) != 0;
    var fromNameType = Ꮡb.getPropertyNameNodeForSymbolFromNameType(Ꮡsymbol, enclosingDeclaration, singleQuote, stringNamed, isMethod);
    if (fromNameType != nil) {
        return fromNameType;
    }

    @string name = symbol.Name;
    @string privateNamePrefix = ((@string)(new byte[]{0xfe, 0x23}));
    if (strings.HasPrefix(name, privateNamePrefix)) {
        name = name[(int)(len(privateNamePrefix))..];
        name = strings.TrimLeftFunc(name, stringutil.IsDigit);
        name = "__#private"u8 + name;
    }

    return b.createPropertyNameNodeForIdentifierOrLiteral(name, singleQuote, stringNamed, isMethod, Ꮡsymbol);
}

internal static ж<ast.Node> getPropertyNameNodeForSymbolFromNameType(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> ᏑenclosingDeclaration, bool singleQuote, bool stringNamed, bool isMethod) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    ref var enclosingDeclaration = ref ᏑenclosingDeclaration.DerefOrNull();

    if (!b.ch.ᏑvalueSymbolLinks.Has(Ꮡsymbol)) {
        return default;
    }
    var nameType = b.ch.ᏑvalueSymbolLinks.TryGet(Ꮡsymbol).nameType;
    if (nameType == nil) {
        return default;
    }
    var enumEnclosingDeclaration = ᏑenclosingDeclaration;
    if (enumEnclosingDeclaration == nil && b.ctx.enclosingFile != nil) {
        enumEnclosingDeclaration = b.ctx.enclosingFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    if ((TypeFlags)(nameType.flags & TypeFlagsEnumLiteral) != 0) {
        var enumSymbol = nameType.symbol.Parent;
        if (enumSymbol == nil) {
            enumSymbol = nameType.symbol;
        }
        if (enumEnclosingDeclaration != nil && b.ch.IsSymbolAccessibleByFlags(enumSymbol, enumEnclosingDeclaration, ast.SymbolFlagsValue)) {
            var saveEnclosingDeclaration = b.ctx.enclosingDeclaration;
            b.ctx.enclosingDeclaration = enumEnclosingDeclaration;
            var result = b.f.NewComputedPropertyName(Ꮡb.symbolToExpression(nameType.symbol, ast.SymbolFlagsValue));
            b.ctx.enclosingDeclaration = saveEnclosingDeclaration;
            return result;
        }
    }
    if ((TypeFlags)(nameType.flags & TypeFlagsStringOrNumberLiteral) != 0) {
        @string name = default;
        switch (nameType.AsLiteralType().value.type()) {
        case jsnum.Number: {
            name = nameType.AsLiteralType().value._<jsnum.Number>().String();
            break;
        }
        case @string: {
            name = nameType.AsLiteralType().value._<@string>();
            break;
        }}

        if (!scanner.IsIdentifierText(name, core.LanguageVariantStandard) && (stringNamed || !isNumericLiteralName(name))) {
            var node = b.f.NewStringLiteral(name, core.IfElse(singleQuote, ast.TokenFlagsSingleQuote, ast.TokenFlagsNone));
            return node;
        }
        if (isNumericLiteralName(name) && name[0] == '-') {
            return b.f.NewComputedPropertyName(b.f.NewPrefixUnaryExpression(ast.KindMinusToken, b.f.NewNumericLiteral(name[1..], ast.TokenFlagsNone)));
        }
        return b.createPropertyNameNodeForIdentifierOrLiteral(name, singleQuote, stringNamed, isMethod, Ꮡsymbol);
    }
    if ((TypeFlags)(nameType.flags & TypeFlagsUniqueESSymbol) != 0) {
        return b.f.NewComputedPropertyName(Ꮡb.symbolToExpression(nameType.AsUniqueESSymbolType().symbol, ast.SymbolFlagsValue));
    }
    return default;
}

internal static slice<ж<astꓸTypeElement>> addPropertyToElementList(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> ᏑpropertySymbol, slice<ж<astꓸTypeElement>> typeElements) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var propertySymbol = ref ᏑpropertySymbol.DerefOrNull();

    var propertyIsReverseMapped = (ast.CheckFlags)(propertySymbol.CheckFlags & ast.CheckFlagsReverseMapped) != 0;
    ж<Type> propertyType = default;
    if (b.shouldUsePlaceholderForProperty(ᏑpropertySymbol)) {
        propertyType = b.ch.anyType;
    } else {
        propertyType = b.ch.getNonMissingTypeOfSymbol(ᏑpropertySymbol);
    }
    var saveEnclosingDeclaration = b.ctx.enclosingDeclaration;
    b.ctx.enclosingDeclaration = default;
    if (isLateBoundName(propertySymbol.Name)) {
        if (len(propertySymbol.Declarations) > 0) {
            var decl = propertySymbol.Declarations[0];
            if (b.ch.hasLateBindableName(decl)) {
                if (ast.IsBinaryExpression(decl)) {
                    var name = ast.GetNameOfDeclaration(decl);
                    if (name != nil && ast.IsElementAccessExpression(name) && ast.IsPropertyAccessEntityNameExpression(name.AsElementAccessExpression().ArgumentExpression, false)) {
                        b.trackComputedName(name.AsElementAccessExpression().ArgumentExpression, saveEnclosingDeclaration);
                    }
                } else {
                    b.trackComputedName(decl.Name().Expression(), saveEnclosingDeclaration);
                }
            }
        } else {
            b.ctx.tracker.ReportNonSerializableProperty(b.ch.symbolToString(ᏑpropertySymbol));
        }
    }
    if (propertySymbol.ValueDeclaration != nil) {
        b.ctx.enclosingDeclaration = propertySymbol.ValueDeclaration;
    } else if (len(propertySymbol.Declarations) > 0 && propertySymbol.Declarations[0] != nil) {
        b.ctx.enclosingDeclaration = propertySymbol.Declarations[0];
    } else {
        b.ctx.enclosingDeclaration = saveEnclosingDeclaration;
    }
    var propertyName = Ꮡb.getPropertyNameNodeForSymbol(ᏑpropertySymbol, saveEnclosingDeclaration);
    b.ctx.enclosingDeclaration = saveEnclosingDeclaration;
    b.ctx.approximateLength += len(ast.SymbolName(ᏑpropertySymbol)) + 1;

    if ((ast.SymbolFlags)(propertySymbol.Flags & ast.SymbolFlagsAccessor) != 0) {
        var writeType = b.ch.getWriteTypeOfSymbol(ᏑpropertySymbol);
        if (!b.ch.isErrorType(propertyType) && !b.ch.isErrorType(writeType)) {
            var propDeclaration = ast.GetDeclarationOfKind(ᏑpropertySymbol, ast.KindPropertyDeclaration);
            if (propertyType != writeType || propertySymbol.Parent != nil && (ast.SymbolFlags)(propertySymbol.Parent.Flags & ast.SymbolFlagsClass) != 0 && propDeclaration == nil) {
                var symbolMapper = b.ch.ᏑvalueSymbolLinks.Get(ᏑpropertySymbol).mapper;
                var getterDeclaration = ast.GetDeclarationOfKind(ᏑpropertySymbol, ast.KindGetAccessor); if (getterDeclaration != nil) {
                    var getterSignature = b.ch.getSignatureFromDeclaration(getterDeclaration);
                    if (symbolMapper != nil) {
                        getterSignature = b.ch.instantiateSignature(getterSignature, symbolMapper);
                    }
                    var getter = Ꮡb.signatureToSignatureDeclarationHelper(getterSignature, ast.KindGetAccessor, Ꮡ(new SignatureToSignatureDeclarationOptions(
                        name: propertyName
                    )));
                    b.setCommentRange(getter, getterDeclaration);
                    typeElements = append(typeElements, getter);
                }
                var setterDeclaration = ast.GetDeclarationOfKind(ᏑpropertySymbol, ast.KindSetAccessor); if (setterDeclaration != nil) {
                    var setterSignature = b.ch.getSignatureFromDeclaration(setterDeclaration);
                    if (symbolMapper != nil) {
                        setterSignature = b.ch.instantiateSignature(setterSignature, symbolMapper);
                    }
                    var setter = Ꮡb.signatureToSignatureDeclarationHelper(setterSignature, ast.KindSetAccessor, Ꮡ(new SignatureToSignatureDeclarationOptions(
                        name: propertyName
                    )));
                    b.setCommentRange(setter, setterDeclaration);
                    typeElements = append(typeElements, setter);
                }
                return typeElements;
            } else if (propertySymbol.Parent != nil && (ast.SymbolFlags)(propertySymbol.Parent.Flags & ast.SymbolFlagsClass) != 0 && propDeclaration != nil && core.Find(propDeclaration.ModifierNodes(), (ж<ast.Node> m) => m.Kind == ast.KindAccessorKeyword) != nil) {
                var fakeGetterSignature = b.ch.newSignature(SignatureFlagsNone, nil, default, nil, default, propertyType, nil, 0);
                var fakeGetterDeclaration = Ꮡb.signatureToSignatureDeclarationHelper(fakeGetterSignature, ast.KindGetAccessor, Ꮡ(new SignatureToSignatureDeclarationOptions(
                    name: propertyName
                )));
                b.setCommentRange(fakeGetterDeclaration, propDeclaration);
                typeElements = append(typeElements, fakeGetterDeclaration);

                var setterParam = b.ch.newSymbol(ast.SymbolFlagsFunctionScopedVariable, "arg");
                b.ch.ᏑvalueSymbolLinks.Get(setterParam).resolvedType = writeType;
                var fakeSetterSignature = b.ch.newSignature(SignatureFlagsNone, nil, default, nil, [setterParam], b.ch.voidType, nil, 0);
                var fakeSetterDeclaration = Ꮡb.signatureToSignatureDeclarationHelper(fakeSetterSignature, ast.KindSetAccessor, Ꮡ(new SignatureToSignatureDeclarationOptions(
                    name: propertyName
                )));
                typeElements = append(typeElements, fakeSetterDeclaration);
                return typeElements;
            }
        }
    }

    ж<ast.Node> optionalToken = default;
    if ((ast.SymbolFlags)(propertySymbol.Flags & ast.SymbolFlagsOptional) != 0) {
        optionalToken = b.f.NewToken(ast.KindQuestionToken);
    } else {
        optionalToken = default;
    }
    if ((ast.SymbolFlags)(propertySymbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsFunction | ast.SymbolFlagsMethod))) != 0 && len(b.ch.getPropertiesOfObjectType(propertyType)) == 0 && !b.ch.isReadonlySymbol(ᏑpropertySymbol)) {
        var signatures = b.ch.getSignaturesOfType(b.ch.filterType(propertyType, (ж<Type> t) => (TypeFlags)(t.flags & TypeFlagsUndefined) == 0), SignatureKindCall);
        foreach (var (_, signature) in range(signatures)) {
            var methodDeclaration = Ꮡb.signatureToSignatureDeclarationHelper(signature, ast.KindMethodSignature, Ꮡ(new SignatureToSignatureDeclarationOptions(
                name: propertyName,
                questionToken: optionalToken
            )));
            b.setCommentRange(methodDeclaration, core.Coalesce<ast.Node>(signature.declaration, propertySymbol.ValueDeclaration));
            typeElements = append(typeElements, methodDeclaration);
        }
        if (len(signatures) != 0 || optionalToken == nil) {
            return typeElements;
        }
    }
    ж<astꓸTypeNode> propertyTypeNode = default;
    if (b.shouldUsePlaceholderForProperty(ᏑpropertySymbol)) {
        propertyTypeNode = b.createElidedInformationPlaceholder();
    } else {
        if (propertyIsReverseMapped) {
            b.ctx.reverseMappedStack = append(b.ctx.reverseMappedStack, ᏑpropertySymbol);
        }
        if (propertyType != nil) {
            propertyTypeNode = Ꮡb.serializeTypeForDeclaration(nil, propertyType, ᏑpropertySymbol, true);
        } else {
            propertyTypeNode = b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
        }
        if (propertyIsReverseMapped) {
            b.ctx.reverseMappedStack = b.ctx.reverseMappedStack.slice(0, len(b.ctx.reverseMappedStack) - 1);
        }
    }

    ж<ast.ModifierList> modifiers = default;
    if (b.ch.isReadonlySymbol(ᏑpropertySymbol)) {
        modifiers = b.f.NewModifierList(slice<ж<ast.Node>>([b.f.NewModifier(ast.KindReadonlyKeyword)]));
        b.ctx.approximateLength += 9;
    }
    var propertySignature = b.f.NewPropertySignatureDeclaration(modifiers, propertyName, optionalToken, propertyTypeNode, nil);

    b.setCommentRange(propertySignature, propertySymbol.ValueDeclaration);
    typeElements = append(typeElements, propertySignature);

    return typeElements;
}

internal static ж<ast.NodeList> createTypeNodesFromResolvedType(this ж<NodeBuilderImpl> Ꮡb, ж<StructuredType> ᏑresolvedType) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var resolvedType = ref ᏑresolvedType.DerefOrNull();

    if (b.checkTruncationLength()) {
        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTruncation) != 0) {
            var elem = b.f.NewNotEmittedTypeElement();
            return b.f.NewNodeList(slice<ж<astꓸTypeElement>>([b.e.AddSyntheticTrailingComment(elem, ast.KindMultiLineCommentTrivia, "elided", false)]));
        }
        return b.f.NewNodeList(slice<ж<ast.Node>>([b.f.NewPropertySignatureDeclaration(nil, b.f.NewIdentifier("..."), nil, nil, nil)]));
    }
    slice<ж<astꓸTypeElement>> typeElements = default;
    foreach (var (_, signature) in range(resolvedType.CallSignatures())) {
        typeElements = append(typeElements, Ꮡb.signatureToSignatureDeclarationHelper(signature, ast.KindCallSignature, nil));
    }
    foreach (var (_, signature) in range(resolvedType.ConstructSignatures())) {
        if ((SignatureFlags)(signature.flags & SignatureFlagsAbstract) != 0) {
            continue;
        }
        typeElements = append(typeElements, Ꮡb.signatureToSignatureDeclarationHelper(signature, ast.KindConstructSignature, nil));
    }
    foreach (var (_, info) in range(resolvedType.indexInfos)) {
        typeElements = slices.Concat<slice<ж<astꓸTypeElement>>, ж<astꓸTypeElement>>(typeElements, Ꮡb.indexInfoToObjectComputedNamesOrSignatureDeclaration(info, core.IfElse((ObjectFlags)(resolvedType.objectFlags & ObjectFlagsReverseMapped) != 0, b.createElidedInformationPlaceholder(), (ж<astꓸTypeNode>)(nil))));
    }

    var properties = resolvedType.properties;
    if (len(properties) == 0) {
        return b.f.NewNodeList(typeElements);
    }

    nint i = 0;
    foreach (var (_, propertySymbol) in range(properties)) {
        if (isExpanding(ref (b.ctx).DerefOrNull()) && (ast.SymbolFlags)(propertySymbol.Flags & ast.SymbolFlagsPrototype) != 0) {
            continue;
        }
        i++;
        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteClassExpressionAsTypeLiteral) != 0) {
            if ((ast.SymbolFlags)(propertySymbol.Flags & ast.SymbolFlagsPrototype) != 0) {
                continue;
            }
            if ((ast.ModifierFlags)(getDeclarationModifierFlagsFromSymbol(ref (propertySymbol).DerefOrNull()) & ((ast.ModifierFlags)(ast.ModifierFlagsPrivate | ast.ModifierFlagsProtected))) != 0) {
                b.ctx.tracker.ReportPrivateInBaseOfClassExpression(propertySymbol.Name);
            }
            if (IsPrivateIdentifierSymbol(propertySymbol)) {
                b.ctx.tracker.ReportPrivateInBaseOfClassExpression(ast.SymbolName(propertySymbol));
            }
        }
        if (b.checkTruncationLength() && (i + 2 < len(properties) - 1)) {
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTruncation) != 0) {
                typeElements[len(typeElements) - 1] = b.e.AddSyntheticTrailingComment(typeElements[len(typeElements) - 1], ast.KindMultiLineCommentTrivia, fmt.Sprintf("... %d more elided ..."u8, len(properties) - i), false);
            } else {
                @string text = fmt.Sprintf("... %d more ..."u8, len(properties) - i);
                typeElements = append(typeElements, b.f.NewPropertySignatureDeclaration(nil, b.f.NewIdentifier(text), nil, nil, nil));
            }
            typeElements = Ꮡb.addPropertyToElementList(properties[len(properties) - 1], typeElements);
            break;
        }
        typeElements = Ꮡb.addPropertyToElementList(propertySymbol, typeElements);
    }
    if (len(typeElements) != 0) {
        return b.f.NewNodeList(typeElements);
    } else {
        return default;
    }
}

internal static ж<astꓸTypeNode> createTypeNodeFromObjectType(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();
    var typesᴷ = new stackAppend<ж<Type>>();

    if (b.ch.isGenericMappedType(Ꮡt) || ((ObjectFlags)(t.objectFlags & ObjectFlagsMapped) != 0 && t.AsMappedType().containsError)) {
        return Ꮡb.createMappedTypeNodeFromType(Ꮡt);
    }

    var resolved = b.ch.resolveStructuredTypeMembers(Ꮡt);
    var callSigs = resolved.CallSignatures();
    var ctorSigs = resolved.ConstructSignatures();
    if (len(resolved.properties) == 0 && len(resolved.indexInfos) == 0) {
        if (len(callSigs) == 0 && len(ctorSigs) == 0) {
            b.ctx.approximateLength += 2;
            var result = b.f.NewTypeLiteralNode(b.f.NewNodeList([]));
            b.e.SetEmitFlags(result, printer.EFSingleLine);
            return result;
        }

        if (len(callSigs) == 1 && len(ctorSigs) == 0) {
            var signature = callSigs[0];
            var signatureNode = Ꮡb.signatureToSignatureDeclarationHelper(signature, ast.KindFunctionType, nil);
            return signatureNode;
        }

        if (len(ctorSigs) == 1 && len(callSigs) == 0) {
            var signature = ctorSigs[0];
            var signatureNode = Ꮡb.signatureToSignatureDeclarationHelper(signature, ast.KindConstructorType, nil);
            return signatureNode;
        }
    }

    var abstractSignatures = core.Filter(ctorSigs, (ж<Signature> signature) => (SignatureFlags)(signature.flags & SignatureFlagsAbstract) != 0);
    if (len(abstractSignatures) > 0) {
        var types = core.Map(abstractSignatures, ж<Type> (ж<Signature> s) => Ꮡb.Value.ch.getOrCreateTypeFromSignature(s));
        nint typeElementCount = len(callSigs) + (len(ctorSigs) - len(abstractSignatures)) + len(resolved.indexInfos) + core.IfElse((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteClassExpressionAsTypeLiteral) != 0, core.CountWhere(resolved.properties, (ж<ast.Symbol> p) => (ast.SymbolFlags)(p.Flags & ast.SymbolFlagsPrototype) == 0), len(resolved.properties));
        if (typeElementCount != 0) {
            types = append(typesᴷ, types, b.getResolvedTypeWithoutAbstractConstructSignatures(resolved));
        }
        return Ꮡb.typeToTypeNode(b.ch.getIntersectionType(types));
    }

    var restoreFlags = Ꮡb.saveRestoreFlags();
    b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsInObjectTypeLiteral);
    var members = Ꮡb.createTypeNodesFromResolvedType(resolved);
    restoreFlags();
    var typeLiteralNode = b.f.NewTypeLiteralNode(members);
    b.ctx.approximateLength += 2;
    b.e.SetEmitFlags(typeLiteralNode, core.IfElse(((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsMultilineObjectLiterals) != 0), (printer.EmitFlags)0, printer.EFSingleLine));
    return typeLiteralNode;
}

} partial class checker_package {

internal static ж<ast.Symbol> getTypeAliasForTypeLiteral(ж<Checker> c, ref Type t) {
    if (t.symbol != nil && (ast.SymbolFlags)(t.symbol.Flags & ast.SymbolFlagsTypeLiteral) != 0 && t.symbol.Declarations != nil) {
        var node = ast.WalkUpParenthesizedTypes(t.symbol.Declarations[0].Parent);
        if (ast.IsTypeAliasDeclaration(node)) {
            return c.getSymbolOfDeclaration(node);
        }
    }
    return default;
}

} partial class checkerᴛNodeBuilderImpl {

internal static (bool, ж<ast.Symbol>) shouldWriteTypeOfFunctionSymbol(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol, TypeId typeId) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    var isStaticMethodSymbol = (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsMethod) != 0 && scanner.IsIdentifierText(symbol.Name, core.LanguageVariantStandard) && core.Some(symbol.Declarations, (ж<ast.Node> declaration) => ast.IsStatic(declaration) && !Ꮡb.Value.ch.isLateBindableIndexSignature(ast.GetNameOfDeclaration(declaration)));
    var isNonLocalFunctionSymbol = false;
    var isFunctionExpressionSymbol = false;
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsFunction) != 0) {
        if (symbol.Parent != nil) {
            isNonLocalFunctionSymbol = true;
        } else {
            foreach (var (_, declaration) in range(symbol.Declarations)) {
                if (declaration.Parent.Kind == ast.KindSourceFile || declaration.Parent.Kind == ast.KindModuleBlock) {
                    isNonLocalFunctionSymbol = true;
                    break;
                }
                if (ast.IsFunctionExpressionOrArrowFunction(declaration) && ast.IsVariableDeclaration(declaration.Parent) && ast.IsVariableDeclarationList(declaration.Parent.Parent) && ast.IsVariableStatement(declaration.Parent.Parent.Parent) && declaration.Parent.Parent.Parent.Parent != nil && (declaration.Parent.Parent.Parent.Parent.Kind == ast.KindSourceFile || declaration.Parent.Parent.Parent.Parent.Kind == ast.KindModuleBlock)) {
                    isNonLocalFunctionSymbol = true;
                    isFunctionExpressionSymbol = true;
                    break;
                }
            }
        }
    }
    if (isStaticMethodSymbol || isNonLocalFunctionSymbol) {
        if (isFunctionExpressionSymbol && symbol.ValueDeclaration != nil && symbol.ValueDeclaration.Parent != nil && symbol.ValueDeclaration.Parent != b.ctx.enclosingDeclaration) {
            Ꮡsymbol = b.ch.getMergedSymbol(symbol.ValueDeclaration.Parent.Symbol()); symbol = ref Ꮡsymbol.DerefOrNull();
        }
        return (((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseTypeOfFunction) != 0 || b.ctx.ᏑvisitedTypes.Has(typeId)) && ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseStructuralFallback) == 0 || b.ch.IsValueSymbolAccessible(Ꮡsymbol, b.ctx.enclosingDeclaration)), Ꮡsymbol);
    }
    return (false, Ꮡsymbol);
}

internal static ж<astꓸTypeNode> createAnonymousTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<Type> t) {
    return Ꮡb.createAnonymousTypeNodeEx(t, false, false);
}

internal static (bool, ж<ast.Symbol>) shouldEmitTypeOfSymbol(this ж<NodeBuilderImpl> Ꮡb, bool forceExpansion, bool forceClassExpansion, ast.SymbolFlags isInstanceType, ж<ast.Symbol> Ꮡsymbol, TypeId typeId) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (forceExpansion) {
        return (false, Ꮡsymbol);
    }
    var nonFunctionResult = (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsClass) != 0 && !forceClassExpansion && b.ch.getBaseTypeVariableOfClass(Ꮡsymbol) == nil && !(symbol.ValueDeclaration != nil && ast.IsClassLike(symbol.ValueDeclaration) && (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteClassExpressionAsTypeLiteral) != 0 && (!ast.IsClassDeclaration(symbol.ValueDeclaration) || b.ch.IsSymbolAccessible(Ꮡsymbol, b.ctx.enclosingDeclaration, isInstanceType, false).Accessibility != printer.SymbolAccessibilityAccessible)) || (ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsEnum | ast.SymbolFlagsValueModule))) != 0;
    if (nonFunctionResult) {
        return (true, Ꮡsymbol);
    }
    return Ꮡb.shouldWriteTypeOfFunctionSymbol(Ꮡsymbol, typeId);
}

internal static ж<astꓸTypeNode> createAnonymousTypeNodeEx(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt, bool forceClassExpansion, bool forceExpansion) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    var typeId = t.id;
    var symbol = t.symbol;
    if (symbol != nil) {
        var isInstantiationExpressionType = (ObjectFlags)(t.objectFlags & (uint32)ObjectFlagsInstantiationExpressionType) != 0;
        if (isInstantiationExpressionType) {
            var instantiationExpressionType = t.AsInstantiationExpressionType();
            var existing = instantiationExpressionType.node;
            if (ast.IsTypeQueryNode(existing) && b.getTypeFromTypeNode(existing, false) == Ꮡt) {
                if (b.ctx.ᏑvisitedTypes.Has(typeId)) {
                    return b.createElidedInformationPlaceholder();
                }
                b.ctx.ᏑvisitedTypes.Add(typeId);
                var typeNode = Ꮡb.tryReuseExistingNonParameterTypeNode(existing, Ꮡt, nil, nil);
                b.ctx.ᏑvisitedTypes.Delete(typeId);
                if (typeNode != nil) {
                    return typeNode;
                }
            }
            if (b.ctx.ᏑvisitedTypes.Has(typeId)) {
                return b.createElidedInformationPlaceholder();
            }
            return Ꮡb.visitAndTransformType(Ꮡt, ((Func<ж<NodeBuilderImpl>, ж<Type>, ж<astꓸTypeNode>>)(checkerᴛNodeBuilderImpl.createTypeNodeFromObjectType)));
        }
        ast.SymbolFlags isInstanceType = default;
        if (isClassInstanceSide(b.ch, Ꮡt)) {
            isInstanceType = ast.SymbolFlagsType;
        } else {
            isInstanceType = ast.SymbolFlagsValue;
        }

        var (ok, symbolΔ1) = Ꮡb.shouldEmitTypeOfSymbol(forceExpansion, forceClassExpansion, isInstanceType, symbol, typeId); if (ok) {
            if (b.shouldExpandType(Ꮡt, false)) {
                b.ctx.depth++;
            } else {
                return Ꮡb.symbolToTypeNode(symbolΔ1, isInstanceType, nil);
            }
        }
        if (b.ctx.ᏑvisitedTypes.Has(typeId)) {
            var typeAlias = getTypeAliasForTypeLiteral(b.ch, ref (Ꮡt).DerefOrNull());
            if (typeAlias != nil) {
                return Ꮡb.symbolToTypeNode(typeAlias, ast.SymbolFlagsType, nil);
            } else {
                return b.createElidedInformationPlaceholder();
            }
        } else {
            return Ꮡb.visitAndTransformType(Ꮡt, ((Func<ж<NodeBuilderImpl>, ж<Type>, ж<astꓸTypeNode>>)(checkerᴛNodeBuilderImpl.createTypeNodeFromObjectType)));
        }
    } else {
        return Ꮡb.createTypeNodeFromObjectType(Ꮡt);
    }
}

internal static ж<Type> getTypeFromTypeNode(this ref NodeBuilderImpl b, ж<astꓸTypeNode> Ꮡnode, bool noMappedTypes) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Parent == nil) {
        return b.ch.errorType;
    }
    var t = b.ch.getTypeFromTypeNode(Ꮡnode);
    if (b.ctx.mapper == nil) {
        return t;
    }

    var instantiated = b.ch.instantiateType(t, b.ctx.mapper);
    if (noMappedTypes && instantiated != t) {
        return default;
    }
    return instantiated;
}

internal static ж<astꓸTypeNode> typeToTypeNodeOrCircularityElision(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
        if (b.ctx.ᏑvisitedTypes.Has(t.id)) {
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowAnonymousIdentifier) == 0) {
                b.ctx.encounteredError = true;
                b.ctx.tracker.ReportCyclicStructureError();
            }
            return b.createElidedInformationPlaceholder();
        }
        return Ꮡb.visitAndTransformType(Ꮡt, ((Func<ж<NodeBuilderImpl>, ж<Type>, ж<astꓸTypeNode>>)(checkerᴛNodeBuilderImpl.typeToTypeNode)));
    }
    return Ꮡb.typeToTypeNode(Ꮡt);
}

internal static ж<astꓸTypeNode> conditionalTypeToTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡ_t) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var _t = ref Ꮡ_t.DerefOrNull();

    if (b.checkTruncationLength()) {
        return b.createElidedInformationPlaceholder();
    }
    var t = _t.AsConditionalType();
    var checkTypeNode = Ꮡb.typeToTypeNode(t.checkType);
    b.ctx.approximateLength += 15;
    if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0 && t.root.isDistributive && (TypeFlags)(t.checkType.flags & TypeFlagsTypeParameter) == 0) {
        var newParam = b.ch.newTypeParameter(b.ch.newSymbol(ast.SymbolFlagsTypeParameter, "T"));
        var name = Ꮡb.typeParameterToName(newParam);
        var newTypeVariable = b.f.NewTypeReferenceNode(name.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil);
        b.ctx.approximateLength += 37;
        var newMapper = prependTypeMapping(t.root.checkType, newParam, t.mapper);
        var saveInferTypeParametersΔ1 = b.ctx.inferTypeParameters;
        b.ctx.inferTypeParameters = t.root.inferTypeParameters;
        var extendsTypeNodeΔ1 = Ꮡb.typeToTypeNode(b.ch.instantiateType(t.root.extendsType, newMapper));
        b.ctx.inferTypeParameters = saveInferTypeParametersΔ1;
        var trueTypeNodeΔ1 = Ꮡb.typeToTypeNodeOrCircularityElision(b.ch.instantiateType(b.getTypeFromTypeNode(t.root.node.TrueType, false), newMapper));
        var falseTypeNodeΔ1 = Ꮡb.typeToTypeNodeOrCircularityElision(b.ch.instantiateType(b.getTypeFromTypeNode(t.root.node.FalseType, false), newMapper));

        var newId = newTypeVariable.AsTypeReferenceNode().TypeName.AsIdentifier().Clone(new ast.NodeFactoryжNodeFactoryCoercible(b.f));
        var syntheticExtendsNode = b.f.NewInferTypeNode(b.f.NewTypeParameterDeclaration(nil, newId, nil, nil, nil));
        var innerCheckConditionalNode = b.f.NewConditionalTypeNode(newTypeVariable, extendsTypeNodeΔ1, trueTypeNodeΔ1, falseTypeNodeΔ1);
        var syntheticTrueNode = b.f.NewConditionalTypeNode(b.f.NewTypeReferenceNode(name.Clone(new ast.NodeFactoryжNodeFactoryCoercible(b.f)), nil), b.f.DeepCloneNode(checkTypeNode), innerCheckConditionalNode, b.f.NewKeywordTypeNode(ast.KindNeverKeyword));
        return b.f.NewConditionalTypeNode(checkTypeNode, syntheticExtendsNode, syntheticTrueNode, b.f.NewKeywordTypeNode(ast.KindNeverKeyword));
    }
    var saveInferTypeParameters = b.ctx.inferTypeParameters;
    b.ctx.inferTypeParameters = t.root.inferTypeParameters;
    var extendsTypeNode = Ꮡb.typeToTypeNode(t.extendsType);
    b.ctx.inferTypeParameters = saveInferTypeParameters;
    var trueTypeNode = Ꮡb.typeToTypeNodeOrCircularityElision(b.ch.getTrueTypeFromConditionalType(Ꮡ_t));
    var falseTypeNode = Ꮡb.typeToTypeNodeOrCircularityElision(b.ch.getFalseTypeFromConditionalType(Ꮡ_t));
    return b.f.NewConditionalTypeNode(checkTypeNode, extendsTypeNode, trueTypeNode, falseTypeNode);
}

internal static ж<ast.Symbol> getParentSymbolOfTypeParameter(this ref NodeBuilderImpl b, ж<TypeParameter> ᏑtypeParameter) {
    ref var typeParameter = ref ᏑtypeParameter.DerefOrNull();

    var tp = ast.GetDeclarationOfKind(typeParameter.symbol, ast.KindTypeParameter);
    ж<ast.Node> host = default;
    host = tp.Parent;
    if (host == nil) {
        return default;
    }
    return b.ch.getSymbolOfNode(host);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string arrayˢ = "Array"u8;
internal static readonly @string readonlyArrayˢ = "ReadonlyArray"u8;

internal static ж<astꓸTypeNode> typeReferenceToTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    slice<ж<Type>> typeArguments = b.ch.getTypeArguments(Ꮡt);
    if (t.Target() == b.ch.globalArrayType || t.Target() == b.ch.globalReadonlyArrayType) {
        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteArrayAsGenericType) != 0) {
            var typeArgumentNode = Ꮡb.typeToTypeNode(typeArguments[0]);
            return b.f.NewTypeReferenceNode(
                b.newIdentifier(core.IfElse(t.Target() == b.ch.globalArrayType, arrayˢ, readonlyArrayˢ), t.Target().symbol),
                b.f.NewNodeList([typeArgumentNode]));
        }
        var elementType = Ꮡb.typeToTypeNode(typeArguments[0]);
        var arrayType = b.f.NewArrayTypeNode(elementType);
        if (t.Target() == b.ch.globalArrayType) {
            return arrayType;
        } else {
            return b.f.NewTypeOperatorNode(ast.KindReadonlyKeyword, arrayType);
        }
    } else if ((ObjectFlags)(t.Target().objectFlags & ObjectFlagsTuple) != 0) {
        typeArguments = core.SameMapIndex(typeArguments, ж<Type> (ж<Type> arg, nint i) => {
            var isOptional = false;
            if (i < len(Ꮡt.Value.Target().AsTupleType().elementInfos)) {
                isOptional = (ElementFlags)(Ꮡt.Value.Target().AsTupleType().elementInfos[i].flags & ElementFlagsOptional) != 0;
            }
            return Ꮡb.Value.ch.removeMissingType(arg, isOptional);
        });
        if (len(typeArguments) > 0) {
            nint arity = b.ch.getTypeReferenceArity(Ꮡt);
            var tupleConstituentNodes = Ꮡb.mapToTypeNodes(typeArguments.slice(0, arity), false);
            if (tupleConstituentNodes != nil) {
                for (nint i = 0; i < len(tupleConstituentNodes.Nodes); i++) {
                    var flags = t.Target().AsTupleType().elementInfos[i].flags;
                    var labeledElementDeclaration = t.Target().AsTupleType().elementInfos[i].labeledDeclaration;

                    if (labeledElementDeclaration != nil) {
                        tupleConstituentNodes.Nodes[i] = b.f.NewNamedTupleMember(
                            core.IfElse((ElementFlags)(flags & ElementFlagsVariable) != 0, b.f.NewToken(ast.KindDotDotDotToken), (ж<ast.Node>)(nil)),
                            b.newIdentifier(b.ch.getTupleElementLabel(t.Target().AsTupleType().elementInfos[i], nil, i), nil),
                            core.IfElse((ElementFlags)(flags & ElementFlagsOptional) != 0, b.f.NewToken(ast.KindQuestionToken), (ж<ast.Node>)(nil)),
                            core.IfElse((ElementFlags)(flags & ElementFlagsRest) != 0, b.f.NewArrayTypeNode(tupleConstituentNodes.Nodes[i]), tupleConstituentNodes.Nodes[i]));
                    } else {
                        switch (ᐧ) {
                        case {} when (ElementFlags)(flags & ElementFlagsVariable) != 0: {
                            tupleConstituentNodes.Nodes[i] = b.f.NewRestTypeNode(core.IfElse((ElementFlags)(flags & ElementFlagsRest) != 0, b.f.NewArrayTypeNode(tupleConstituentNodes.Nodes[i]), tupleConstituentNodes.Nodes[i]));
                            break;
                        }
                        case {} when (ElementFlags)(flags & ElementFlagsOptional) != 0: {
                            tupleConstituentNodes.Nodes[i] = b.f.NewOptionalTypeNode(tupleConstituentNodes.Nodes[i]);
                            break;
                        }}

                    }
                }
                var tupleTypeNode = b.f.NewTupleTypeNode(tupleConstituentNodes);
                b.e.SetEmitFlags(tupleTypeNode, printer.EFSingleLine);
                if (t.Target().AsTupleType().@readonly) {
                    return b.f.NewTypeOperatorNode(ast.KindReadonlyKeyword, tupleTypeNode);
                } else {
                    return tupleTypeNode;
                }
            }
        }
        if (b.ctx.encounteredError || ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowEmptyTuple) != 0)) {
            var tupleTypeNode = b.f.NewTupleTypeNode(b.f.NewNodeList([]));
            b.e.SetEmitFlags(tupleTypeNode, printer.EFSingleLine);
            if (t.Target().AsTupleType().@readonly) {
                return b.f.NewTypeOperatorNode(ast.KindReadonlyKeyword, tupleTypeNode);
            } else {
                return tupleTypeNode;
            }
        }
        b.ctx.encounteredError = true;
        return default;
    } else if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteClassExpressionAsTypeLiteral) != 0 && t.symbol.ValueDeclaration != nil && ast.IsClassLike(t.symbol.ValueDeclaration) && !b.ch.IsValueSymbolAccessible(t.symbol, b.ctx.enclosingDeclaration)) {
        return Ꮡb.createAnonymousTypeNode(Ꮡt);
    } else {
        var outerTypeParameters = t.Target().AsInterfaceType().OuterTypeParameters();
        nint i = 0;
        ж<astꓸTypeNode> resultType = default;
        if (outerTypeParameters != nil) {
            nint length = len(outerTypeParameters);
            while (i < length) {
                nint start = i;
                var parent = b.getParentSymbolOfTypeParameter(outerTypeParameters[i].AsTypeParameter());
                for (var ok = true; ok; ok = i < length && b.getParentSymbolOfTypeParameter(outerTypeParameters[i].AsTypeParameter()) == parent) {
                    i++;
                }

                if (!slices.Equal<slice<ж<Type>>, ж<Type>>(outerTypeParameters.slice(start, i), typeArguments.slice(start, i))) {
                    var typeArgumentSlice = Ꮡb.mapToTypeNodes(typeArguments.slice(start, i), false);
                    var restoreFlagsΔ1 = Ꮡb.saveRestoreFlags();
                    b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsForbidIndexedAccessSymbolReferences);
                    var @ref = Ꮡb.symbolToTypeNode(parent, ast.SymbolFlagsType, typeArgumentSlice);
                    restoreFlagsΔ1();
                    if (resultType == nil) {
                        resultType = @ref;
                    } else {
                        resultType = b.appendReferenceToType(resultType, @ref);
                    }
                }
            }
        }
        ж<ast.NodeList> typeArgumentNodes = default;
        if (len(typeArguments) > 0) {
            nint typeParameterCount = 0;
            var typeParams = t.Target().AsInterfaceType().TypeParameters();
            if (typeParams != nil) {
                typeParameterCount = min(len(typeParams), len(typeArguments));

                if (b.ch.isReferenceToType(Ꮡt, b.ch.getGlobalIterableType()) || b.ch.isReferenceToType(Ꮡt, b.ch.getGlobalIterableIteratorType()) || b.ch.isReferenceToType(Ꮡt, b.ch.getGlobalAsyncIterableType()) || b.ch.isReferenceToType(Ꮡt, b.ch.getGlobalAsyncIterableIteratorType())) {
                    if (t.AsTypeReference().node == nil || !ast.IsTypeReferenceNode(t.AsTypeReference().node) || t.AsTypeReference().node.TypeArguments() == nil || len(t.AsTypeReference().node.TypeArguments()) < typeParameterCount) {
                        while (typeParameterCount > 0) {
                            var typeArgument = typeArguments[typeParameterCount - 1];
                            var typeParameter = t.Target().AsInterfaceType().TypeParameters()[typeParameterCount - 1];
                            var defaultType = b.ch.getDefaultFromTypeParameter(typeParameter);
                            if (defaultType == nil || !b.ch.isTypeIdenticalTo(typeArgument, defaultType)) {
                                break;
                            }
                            typeParameterCount--;
                        }
                    }
                }
            }

            typeArgumentNodes = Ꮡb.mapToTypeNodes(typeArguments.slice(i, typeParameterCount), false);
        }
        var restoreFlags = Ꮡb.saveRestoreFlags();
        b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsForbidIndexedAccessSymbolReferences);
        var finalRef = Ꮡb.symbolToTypeNode(t.symbol, ast.SymbolFlagsType, typeArgumentNodes);
        restoreFlags();
        if (resultType == nil) {
            return finalRef;
        } else {
            return b.appendReferenceToType(resultType, finalRef);
        }
    }
}

internal static ж<astꓸTypeNode> visitAndTransformType(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt, Func<ж<NodeBuilderImpl>, ж<Type>, ж<astꓸTypeNode>> transform) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    var typeId = t.id;
    ref var isConstructorObject = ref heap<bool>((ObjectFlags)(t.objectFlags & ObjectFlagsAnonymous) != 0 && t.symbol != nil && (ast.SymbolFlags)(t.symbol.Flags & ast.SymbolFlagsClass) != 0, out var ᏑisConstructorObject);
    ж<CompositeSymbolIdentity> id = default;
    switch (ᐧ) {
    case {} when (ObjectFlags)(t.objectFlags & ObjectFlagsReference) != 0 && t.AsTypeReference().node != nil: {
        id = Ꮡ(new CompositeSymbolIdentity(false, 0, ast.GetNodeId(t.AsTypeReference().node)));
        break;
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsConditional) != 0: {
        id = Ꮡ(new CompositeSymbolIdentity(false, 0, ast.GetNodeId(t.AsConditionalType().root.node.of(ast.ConditionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())));
        break;
    }
    case {} when t.symbol != nil: {
        id = Ꮡ(new CompositeSymbolIdentity(isConstructorObject, ast.GetSymbolId(t.symbol), 0));
        break;
    }
    default: {
        id = default;
        break;
    }}

    var key = new CompositeTypeCacheIdentity(typeId, b.ctx.flags, b.ctx.internalFlags);
    var canUseCache = b.ctx.maxExpansionDepth < 0;
    if (canUseCache && b.ctx.enclosingDeclaration != nil && b.links.Has(b.ctx.enclosingDeclaration)) {
        var links = b.links.Get(b.ctx.enclosingDeclaration);
        var (cachedResult, ok) = links.serializedTypes[key, ꟷ];
        if (ok) {
            foreach (var (_, arg) in range(cachedResult.trackedSymbols)) {
                b.ctx.tracker.TrackSymbol(arg.symbol, arg.enclosingDeclaration, arg.meaning);
            }
            if (cachedResult.truncating) {
                b.ctx.truncating = true;
            }
            b.ctx.approximateLength += cachedResult.addedLength;
            return b.f.DeepCloneNode(cachedResult.node);
        }
    }

    nint depth = default;
    if (id != nil) {
        depth = b.ctx.symbolDepth[id.Value];
        if (depth > 10) {
            return b.createElidedInformationPlaceholder();
        }
        b.ctx.symbolDepth[id.Value] = depth + 1;
    }
    b.ctx.ᏑvisitedTypes.Add(typeId);
    var prevTrackedSymbols = b.ctx.trackedSymbols;
    b.ctx.trackedSymbols = default;
    nint startLength = b.ctx.approximateLength;
    var result = transform(Ꮡb, Ꮡt);
    ref var addedLength = ref heap<nint>(b.ctx.approximateLength - startLength, out var ᏑaddedLength);
    if (canUseCache && !b.ctx.reportedDiagnostic && !b.ctx.encounteredError) {
        var links = b.links.Get(b.ctx.enclosingDeclaration);
        if (links.serializedTypes == nil) {
            links.serializedTypes = new map<CompositeTypeCacheIdentity, ж<SerializedTypeEntry>>();
        }
        links.serializedTypes[key] = Ꮡ(new SerializedTypeEntry(
            node: result,
            truncating: b.ctx.truncating,
            addedLength: addedLength,
            trackedSymbols: b.ctx.trackedSymbols
        ));
    }
    b.ctx.ᏑvisitedTypes.Delete(typeId);
    if (id != nil) {
        b.ctx.symbolDepth[id.Value] = depth;
    }
    b.ctx.trackedSymbols = prevTrackedSymbols;
    return result;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string subˢ = "sub-"u8;
internal static readonly @string superˢ = "super-"u8;

internal static ж<astꓸTypeNode> typeToTypeNode(this ж<NodeBuilderImpl> Ꮡb, ж<Type> Ꮡt) {
    GoFrame ᒐ = default;
    try {
        ref var b = ref Ꮡb.DerefOrNull();
        ref var t = ref Ꮡt.DerefOrNull();

        var inTypeAlias = (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsInTypeAlias);
        b.ctx.flags &= unchecked((nodebuilder.Flags)~(nodebuilder.Flags)(nodebuilder.FlagsInTypeAlias));

        if (Ꮡt == nil) {
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowEmptyUnionOrIntersection) == 0) {
                b.ctx.encounteredError = true;
                return default;
            }
            b.ctx.approximateLength += 3;
            return b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
        }

        Ꮡt = getNonDistributedTypeParameter(Ꮡt); t = ref Ꮡt.DerefOrNull();

        if (b.ctx.maxExpansionDepth >= 0) {
            b.ctx.typeStack = append(b.ctx.typeStack, Ꮡt);
            defer(() => {
                Ꮡb.Value.ctx.typeStack = Ꮡb.Value.ctx.typeStack.slice(0, len(Ꮡb.Value.ctx.typeStack) - 1);
            }, ref ᒐ);
        }

        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsNoTypeReduction) == 0) {
            Ꮡt = b.ch.getReducedType(Ꮡt); t = ref Ꮡt.DerefOrNull();
        }

        if ((TypeFlags)(t.flags & TypeFlagsAny) != 0) {
            if (t.alias != nil) {
                return t.alias.ToTypeReferenceNode(Ꮡb);
            }
            if (Ꮡt == b.ch.unresolvedType) {
                return b.e.AddSyntheticLeadingComment(b.f.NewKeywordTypeNode(ast.KindAnyKeyword), ast.KindMultiLineCommentTrivia, "unresolved", false);
            }
            b.ctx.approximateLength += 3;
            return b.f.NewKeywordTypeNode(core.IfElse(Ꮡt == b.ch.intrinsicMarkerType, ast.KindIntrinsicKeyword, ast.KindAnyKeyword));
        }
        if ((TypeFlags)(t.flags & TypeFlagsUnknown) != 0) {
            return b.f.NewKeywordTypeNode(ast.KindUnknownKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsString) != 0) {
            b.ctx.approximateLength += 6;
            return b.f.NewKeywordTypeNode(ast.KindStringKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsNumber) != 0) {
            b.ctx.approximateLength += 6;
            return b.f.NewKeywordTypeNode(ast.KindNumberKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsBigInt) != 0) {
            b.ctx.approximateLength += 6;
            return b.f.NewKeywordTypeNode(ast.KindBigIntKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsBoolean) != 0 && t.alias == nil) {
            b.ctx.approximateLength += 7;
            return b.f.NewKeywordTypeNode(ast.KindBooleanKeyword);
        }
        var expandingEnum = false;
        if ((TypeFlags)(t.flags & TypeFlagsEnumLike) != 0) {
            if ((ast.SymbolFlags)(t.symbol.Flags & ast.SymbolFlagsEnumMember) != 0) {
                var parentSymbol = b.ch.getParentOfSymbol(t.symbol);
                var parentName = Ꮡb.symbolToTypeNode(parentSymbol, ast.SymbolFlagsType, nil);
                if (b.ch.getDeclaredTypeOfSymbol(parentSymbol) == Ꮡt) {
                    return parentName;
                }
                @string memberName = ast.SymbolName(t.symbol);
                if (scanner.IsIdentifierText(memberName, core.LanguageVariantStandard)) {
                    return b.appendReferenceToType(parentName, b.f.NewTypeReferenceNode(b.f.NewIdentifier(memberName), nil));
                }
                if (ast.IsImportTypeNode(parentName)) {
                    parentName.AsImportTypeNode().IsTypeOf = true;
                    return b.f.NewIndexedAccessTypeNode(parentName, b.f.NewLiteralTypeNode(b.newStringLiteral(memberName)));
                } else if (ast.IsTypeReferenceNode(parentName)) {
                    return b.f.NewIndexedAccessTypeNode(b.f.NewTypeQueryNode(parentName.AsTypeReferenceNode().TypeName, nil), b.f.NewLiteralTypeNode(b.newStringLiteral(memberName)));
                } else {
                    throw panic("Unhandled type node kind returned from `symbolToTypeNode`.");
                }
            }
            if ((TypeFlags)(t.flags & TypeFlagsUnion) == 0 || !b.shouldExpandType(Ꮡt, false)) {
                return Ꮡb.symbolToTypeNode(t.symbol, ast.SymbolFlagsType, nil);
            }
            expandingEnum = true;
        }
        if ((TypeFlags)(t.flags & TypeFlagsStringLiteral) != 0) {
            b.ctx.approximateLength += len(t.AsLiteralType().value._<@string>()) + 2;
            var lit = b.newStringLiteral(t.AsLiteralType().value._<@string>());
            b.e.AddEmitFlags(lit, printer.EFNoAsciiEscaping);
            return b.f.NewLiteralTypeNode(lit);
        }
        if ((TypeFlags)(t.flags & TypeFlagsNumberLiteral) != 0) {
            var value = t.AsLiteralType().value._<jsnum.Number>();
            b.ctx.approximateLength += len(value.String());
            if (value < 0D) {
                return b.f.NewLiteralTypeNode(b.f.NewPrefixUnaryExpression(ast.KindMinusToken, b.f.NewNumericLiteral(value.String()[1..], ast.TokenFlagsNone)));
            } else {
                return b.f.NewLiteralTypeNode(b.f.NewNumericLiteral(value.String(), ast.TokenFlagsNone));
            }
        }
        if ((TypeFlags)(t.flags & TypeFlagsBigIntLiteral) != 0) {
            b.ctx.approximateLength += len(pseudoBigIntToString(getBigIntLiteralValue(Ꮡt))) + 1;
            return b.f.NewLiteralTypeNode(b.f.NewBigIntLiteral(pseudoBigIntToString(getBigIntLiteralValue(Ꮡt)) + "n"u8, ast.TokenFlagsNone));
        }
        if ((TypeFlags)(t.flags & TypeFlagsBooleanLiteral) != 0) {
            if (t.AsLiteralType().value._<bool>()) {
                b.ctx.approximateLength += 4;
                return b.f.NewLiteralTypeNode(b.f.NewKeywordExpression(ast.KindTrueKeyword));
            } else {
                b.ctx.approximateLength += 5;
                return b.f.NewLiteralTypeNode(b.f.NewKeywordExpression(ast.KindFalseKeyword));
            }
        }
        if ((TypeFlags)(t.flags & TypeFlagsUniqueESSymbol) != 0) {
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowUniqueESSymbolType) == 0) {
                if (b.ch.IsValueSymbolAccessible(t.symbol, b.ctx.enclosingDeclaration)) {
                    b.ctx.approximateLength += 6;
                    return Ꮡb.symbolToTypeNode(t.symbol, ast.SymbolFlagsValue, nil);
                }
                b.ctx.tracker.ReportInaccessibleUniqueSymbolError();
            }
            b.ctx.approximateLength += 13;
            return b.f.NewTypeOperatorNode(ast.KindUniqueKeyword, b.f.NewKeywordTypeNode(ast.KindSymbolKeyword));
        }
        if ((TypeFlags)(t.flags & TypeFlagsVoid) != 0) {
            b.ctx.approximateLength += 4;
            return b.f.NewKeywordTypeNode(ast.KindVoidKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsUndefined) != 0) {
            b.ctx.approximateLength += 9;
            return b.f.NewKeywordTypeNode(ast.KindUndefinedKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsNull) != 0) {
            b.ctx.approximateLength += 4;
            return b.f.NewLiteralTypeNode(b.f.NewKeywordExpression(ast.KindNullKeyword));
        }
        if ((TypeFlags)(t.flags & TypeFlagsNever) != 0) {
            b.ctx.approximateLength += 5;
            return b.f.NewKeywordTypeNode(ast.KindNeverKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsESSymbol) != 0) {
            b.ctx.approximateLength += 6;
            return b.f.NewKeywordTypeNode(ast.KindSymbolKeyword);
        }
        if ((TypeFlags)(t.flags & TypeFlagsNonPrimitive) != 0) {
            b.ctx.approximateLength += 6;
            return b.f.NewKeywordTypeNode(ast.KindObjectKeyword);
        }
        if (isThisTypeParameter(Ꮡt)) {
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsInObjectTypeLiteral) != 0) {
                if (!b.ctx.encounteredError && (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowThisInObjectLiteral) == 0) {
                    b.ctx.encounteredError = true;
                }
                b.ctx.tracker.ReportInaccessibleThisError();
            }
            b.ctx.approximateLength += 4;
            return b.f.NewThisTypeNode();
        }

        if (inTypeAlias == 0 && t.alias != nil && ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseAliasDefinedOutsideCurrentScope) != 0 || b.ch.IsTypeSymbolAccessible(t.alias.Symbol(), b.ctx.enclosingDeclaration))) {
            if (!b.shouldExpandType(Ꮡt, true)) {
                var sym = t.alias.Symbol();
                var typeArgumentNodes = Ꮡb.mapToTypeNodes(t.alias.TypeArguments(), false);
                if (isReservedMemberName(sym.Name) && (ast.SymbolFlags)(sym.Flags & ast.SymbolFlagsClass) == 0) {
                    return b.f.NewTypeReferenceNode(b.f.NewIdentifier(""), typeArgumentNodes);
                }
                if (typeArgumentNodes != nil && len(typeArgumentNodes.Nodes) == 1 && sym == b.ch.globalArrayType.symbol) {
                    return b.f.NewArrayTypeNode(typeArgumentNodes.Nodes[0]);
                }
                return Ꮡb.symbolToTypeNode(sym, ast.SymbolFlagsType, typeArgumentNodes);
            }
            b.ctx.depth++;
            defer(() => {
                Ꮡb.Value.ctx.depth--;
            }, ref ᒐ);
        }

        var objectFlags = t.objectFlags;

        if ((ObjectFlags)(objectFlags & ObjectFlagsReference) != 0) {
            debug.Assert((TypeFlags)(t.Flags() & TypeFlagsObject) != 0);
            if (b.shouldExpandType(Ꮡt, false)) {
                b.ctx.depth++;
                var result = Ꮡb.createAnonymousTypeNodeEx(Ꮡt, true, true);
                b.ctx.depth--;
                return result;
            }
            if (t.AsTypeReference().node != nil) {
                return Ꮡb.visitAndTransformType(Ꮡt, ((Func<ж<NodeBuilderImpl>, ж<Type>, ж<astꓸTypeNode>>)(checkerᴛNodeBuilderImpl.typeReferenceToTypeNode)));
            } else {
                return Ꮡb.typeReferenceToTypeNode(Ꮡt);
            }
        }
        if ((TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0 || (ObjectFlags)(objectFlags & ObjectFlagsClassOrInterface) != 0) {
            if ((ObjectFlags)(objectFlags & ObjectFlagsClassOrInterface) != 0 && b.shouldExpandType(Ꮡt, false)) {
                b.ctx.depth++;
                var result = Ꮡb.createAnonymousTypeNodeEx(Ꮡt, true, true);
                b.ctx.depth--;
                return result;
            }
            if ((TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0 && slices.Contains(b.ctx.inferTypeParameters, Ꮡt)) {
                b.ctx.approximateLength += len(ast.SymbolName(t.symbol)) + 6;
                ж<astꓸTypeNode> constraintNode = default;
                var constraint = b.ch.getConstraintOfTypeParameter(Ꮡt);
                if (constraint != nil) {
                    var inferredConstraint = b.ch.getInferredTypeParameterConstraint(Ꮡt, true);
                    if (!(inferredConstraint != nil && b.ch.isTypeIdenticalTo(constraint, inferredConstraint))) {
                        b.ctx.approximateLength += 9;
                        constraintNode = Ꮡb.typeToTypeNode(constraint);
                    }
                }
                return b.f.NewInferTypeNode(Ꮡb.typeParameterToDeclarationWithConstraint(Ꮡt, constraintNode));
            }
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0 && (TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0) {
                var nameΔ1 = Ꮡb.typeParameterToName(Ꮡt);
                b.ctx.approximateLength += len((~nameΔ1).Text);
                return b.f.NewTypeReferenceNode(b.newIdentifier((~nameΔ1).Text, t.symbol), nil);
            }
            if (t.symbol != nil) {
                return Ꮡb.symbolToTypeNode(t.symbol, ast.SymbolFlagsType, nil);
            }
            @string name = default;
            if ((Ꮡt == b.ch.markerSuperTypeForCheck || Ꮡt == b.ch.markerSubTypeForCheck) && b.ch.varianceTypeParameter != nil && b.ch.varianceTypeParameter.symbol != nil) {
                name = core.IfElse(Ꮡt == b.ch.markerSubTypeForCheck, subˢ, superˢ) + ast.SymbolName(b.ch.varianceTypeParameter.symbol);
            } else {
                name = "?";
            }
            return b.f.NewTypeReferenceNode(b.newIdentifier(name, nil), nil);
        }
        if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0 && t.AsUnionType().origin != nil) {
            Ꮡt = t.AsUnionType().origin; t = ref Ꮡt.DerefOrNull();
        }
        if ((TypeFlags)(t.flags & ((TypeFlags)(TypeFlagsUnion | TypeFlagsIntersection))) != 0) {
            slice<ж<Type>> types = default;
            if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
                types = b.ch.formatUnionTypes(t.AsUnionType().types, expandingEnum);
            } else {
                types = t.AsIntersectionType().types;
            }
            if (len(types) == 1) {
                return Ꮡb.typeToTypeNode(types[0]);
            }
            var typeNodes = Ꮡb.mapToTypeNodes(types, true);
            if (typeNodes != nil && len(typeNodes.Nodes) > 0) {
                if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
                    return b.f.NewUnionTypeNode(typeNodes);
                } else {
                    return b.f.NewIntersectionTypeNode(typeNodes);
                }
            } else {
                if (!b.ctx.encounteredError && (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsAllowEmptyUnionOrIntersection) == 0) {
                    b.ctx.encounteredError = true;
                }
                return default;
            }
        }
        if ((ObjectFlags)(objectFlags & ((ObjectFlags)(ObjectFlagsAnonymous | ObjectFlagsMapped))) != 0) {
            debug.Assert((TypeFlags)(t.Flags() & TypeFlagsObject) != 0);
            return Ꮡb.createAnonymousTypeNode(Ꮡt);
        }
        if ((TypeFlags)(t.flags & TypeFlagsIndex) != 0) {
            var indexedType = t.Target();
            b.ctx.approximateLength += 6;
            var indexTypeNode = Ꮡb.typeToTypeNode(indexedType);
            return b.f.NewTypeOperatorNode(ast.KindKeyOfKeyword, indexTypeNode);
        }
        if ((TypeFlags)(t.flags & TypeFlagsTemplateLiteral) != 0) {
            var texts = t.AsTemplateLiteralType().texts;
            var types = t.AsTemplateLiteralType().types;
            var templateHead = b.f.NewTemplateHead(texts[0], "", ast.TokenFlagsNone);
            b.e.AddEmitFlags(templateHead, printer.EFNoAsciiEscaping);
            var textsʗ1 = texts;
            var typesʗ1 = types;
            var templateSpans = b.f.NewNodeList(core.MapIndex(types, ж<ast.Node> (ж<Type> tΔ1, nint i) => {
                ж<astꓸTemplateMiddleOrTail> res = default;
                if (i < len(typesʗ1) - 1) {
                    res = Ꮡb.Value.f.NewTemplateMiddle(textsʗ1[i + 1], "", ast.TokenFlagsNone);
                } else {
                    res = Ꮡb.Value.f.NewTemplateTail(textsʗ1[i + 1], "", ast.TokenFlagsNone);
                }
                Ꮡb.Value.e.AddEmitFlags(res, printer.EFNoAsciiEscaping);
                return Ꮡb.Value.f.NewTemplateLiteralTypeSpan(Ꮡb.typeToTypeNode(tΔ1), res);
            }));
            b.ctx.approximateLength += 2;
            return b.f.NewTemplateLiteralTypeNode(templateHead, templateSpans);
        }
        if ((TypeFlags)(t.flags & TypeFlagsStringMapping) != 0) {
            var typeNode = Ꮡb.typeToTypeNode(t.Target());
            return Ꮡb.symbolToTypeNode(t.AsStringMappingType().symbol, ast.SymbolFlagsType, b.f.NewNodeList([typeNode]));
        }
        if ((TypeFlags)(t.flags & TypeFlagsIndexedAccess) != 0) {
            var objectTypeNode = Ꮡb.typeToTypeNode(t.AsIndexedAccessType().objectType);
            var indexTypeNode = Ꮡb.typeToTypeNode(t.AsIndexedAccessType().indexType);
            b.ctx.approximateLength += 2;
            return b.f.NewIndexedAccessTypeNode(objectTypeNode, indexTypeNode);
        }
        if ((TypeFlags)(t.flags & TypeFlagsConditional) != 0) {
            return Ꮡb.visitAndTransformType(Ꮡt, ((Func<ж<NodeBuilderImpl>, ж<Type>, ж<astꓸTypeNode>>)(checkerᴛNodeBuilderImpl.conditionalTypeToTypeNode)));
        }
        if ((TypeFlags)(t.flags & TypeFlagsSubstitution) != 0) {
            var typeNode = Ꮡb.typeToTypeNode(t.AsSubstitutionType().baseType);
            if (!b.ch.isNoInferType(Ꮡt)) {
                return typeNode;
            }
            var noInferSymbol = b.ch.getGlobalTypeAliasSymbol("NoInfer", 1, false);
            if (noInferSymbol != nil) {
                return Ꮡb.symbolToTypeNode(noInferSymbol, ast.SymbolFlagsType, b.f.NewNodeList([typeNode]));
            } else {
                return typeNode;
            }
        }

        throw panic("Should be unreachable.");
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> newStringLiteral(this ref NodeBuilderImpl b, @string text) {
    return b.newStringLiteralEx(text, false);
}

internal static ж<ast.Node> newStringLiteralEx(this ref NodeBuilderImpl b, @string text, bool isSingleQuote) {
    var flags = ast.TokenFlagsNone;
    if (isSingleQuote || (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsUseSingleQuotesForStringLiteralType) != 0) {
        flags |= (ast.TokenFlags)(ast.TokenFlagsSingleQuote);
    }
    var node = b.f.NewStringLiteral(text, flags);
    return node;
}

} partial class checkerᴛTypeAlias {

public static ж<ast.Node> ToTypeReferenceNode(this ж<TypeAlias> Ꮡt, ж<NodeBuilderImpl> Ꮡb) {
    ref var b = ref Ꮡb.DerefOrNull();

    return b.f.NewTypeReferenceNode(b.symbolToEntityNameNode(Ꮡt.Symbol()), Ꮡb.mapToTypeNodes(Ꮡt.TypeArguments(), false));
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> newIdentifier(this ref NodeBuilderImpl b, @string text, ж<ast.Symbol> symbol) {
    var id = b.f.NewIdentifier(text);
    if (symbol != nil) {
        b.idToSymbol[id] = symbol;
    }
    return id;
}

internal static ж<astꓸExpression> createAccessExpression(this ref NodeBuilderImpl b, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (ᐧ) {
    case {} when ast.IsQualifiedName(Ꮡnode): {
        return b.f.NewPropertyAccessExpression(b.createAccessExpression(node.AsQualifiedName().Left), nil, b.f.DeepCloneNode(node.AsQualifiedName().Right), ast.NodeFlagsNone);
    }
    case {} when (ast.IsIdentifier(Ꮡnode)) || (ast.IsPropertyAccessExpression(Ꮡnode)) || (ast.IsExpressionWithTypeArguments(Ꮡnode)): {
        return b.f.DeepCloneNode(Ꮡnode);
    }
    default: {
        throw panic("unexpected access node kind: " + node.Kind.String());
        break;
    }}

}

internal static ж<astꓸExpression> createExpressionWithTypeArguments(this ref NodeBuilderImpl b, ж<astꓸExpression> expr, ж<ast.NodeList> ᏑtypeArguments) {
    ref var typeArguments = ref ᏑtypeArguments.DerefOrNull();

    if (ᏑtypeArguments == nil || len(typeArguments.Nodes) == 0) {
        return expr;
    }
    return b.f.NewExpressionWithTypeArguments(expr, ᏑtypeArguments);
}

internal static ж<astꓸTypeParameterList> lookupInstantiatedTypeArgumentNodes(this ж<NodeBuilderImpl> Ꮡb, slice<ж<ast.Symbol>> chain, nint index) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (b.shouldWriteTypeParametersInQualifiedName(chain, index)) {
        var symbol = chain[index];
        var nextSymbol = chain[index + 1];
        if ((ast.CheckFlags)(nextSymbol.CheckFlags & ast.CheckFlagsInstantiated) == 0) {
            return default;
        }

        var targetSymbol = symbol;
        if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAlias) != 0 && !b.ch.canGetTypeParametersOfClassOrInterface(symbol)) {
            targetSymbol = b.ch.resolveAlias(symbol);
        }

        if (!b.ch.canGetTypeParametersOfClassOrInterface(targetSymbol)) {
            return default;
        }

        var @params = b.getTypeParametersOfClassOrInterface(targetSymbol);
        var targetMapper = b.ch.ᏑvalueSymbolLinks.Get(nextSymbol).mapper;
        if (targetMapper != nil) {
            @params = core.Map<ж<Type>, ж<Type>>(@params, targetMapper.Map);
        }
        return Ꮡb.mapToTypeNodes(@params, false);
    }
    return default;
}

internal static ж<astꓸTypeParameterList> lookupExpressionChainTypeArgumentNodes(this ж<NodeBuilderImpl> Ꮡb, slice<ж<ast.Symbol>> chain, nint index) {
    ref var b = ref Ꮡb.DerefOrNull();

    if (b.shouldWriteTypeParametersInQualifiedName(chain, index)) {
        var symbol = chain[index];
        var symbolId = ast.GetSymbolId(symbol);
        if (b.ctx.ᏑtypeParameterSymbolList.Has(symbolId)) {
            return default;
        }

        b.ctx.ᏑtypeParameterSymbolList.Add(symbolId);
        var typeArgumentNodes = Ꮡb.lookupInstantiatedTypeArgumentNodes(chain, index); if (typeArgumentNodes != nil) {
            return typeArgumentNodes;
        }
        var typeParameterNodes = Ꮡb.typeParametersToTypeParameterDeclarations(symbol);
        if (len(typeParameterNodes) > 0) {
            return b.f.NewNodeList(typeParameterNodes);
        }
    }
    return default;
}

internal static bool shouldWriteTypeParametersInQualifiedName(this ref NodeBuilderImpl b, slice<ж<ast.Symbol>> chain, nint index) {
    return (nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsWriteTypeParametersInQualifiedName) != 0 && index < len(chain) - 1;
}

} // end checkerᴛNodeBuilderImpl
