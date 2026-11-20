namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

partial struct NodeBuilder {
    internal slice<ж<NodeBuilderContext>> ctxStack;
    internal Host host;
    internal ж<NodeBuilderImpl> impl;
    internal ж<VerbosityContext> verbosity;
}

partial struct VerbosityContext {
    public nint Level;
    public nint MaxTruncationLength;
    public bool CanIncreaseVerbosity;
    public bool Truncated;
}

} partial class checkerᴛNodeBuilder {

public static ж<printer.EmitContext> EmitContext(this ref NodeBuilder b) {
    return b.impl.e;
}

internal static void enterContext(this ref NodeBuilder b, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var verbosityLevel = ref heap<nint>(-1, out var ᏑverbosityLevel);
    ref var maxTruncationLength = ref heap<nint>(0, out var ᏑmaxTruncationLength);
    if (b.verbosity != nil) {
        verbosityLevel = b.verbosity.Level;
        maxTruncationLength = b.verbosity.MaxTruncationLength;
    }
    b.ctxStack = append(b.ctxStack, b.impl.ctx);
    b.impl.ctx = Ꮡ(new NodeBuilderContext(
        host: b.host,
        tracker: tracker,
        flags: flags,
        internalFlags: internalFlags,
        maxExpansionDepth: verbosityLevel,
        maxTruncationLength: maxTruncationLength,
        enclosingDeclaration: enclosingDeclaration,
        enclosingFile: ast.GetSourceFileOfNode(enclosingDeclaration),
        inferTypeParameters: new slice<ж<Type>>(0),
        symbolDepth: new map<CompositeSymbolIdentity, nint>(),
        trackedSymbols: new slice<ж<TrackedSymbolArgs>>(0),
        reverseMappedStack: new slice<ж<ast.Symbol>>(0),
        enclosingSymbolTypes: new map<ast.SymbolId, ж<Type>>(),
        remappedSymbolReferences: new map<ast.SymbolId, ж<ast.Symbol>>()
    ));
    tracker = new SymbolTrackerImplжSymbolTracker(NewSymbolTrackerImpl(b.impl.ctx, tracker));
    b.impl.ctx.tracker = tracker;
}

internal static void propagateVerbosityOut(this ref NodeBuilder b) {
    if (b.verbosity != nil) {
        if (b.impl.ctx.canIncreaseExpansionDepth) {
            b.verbosity.CanIncreaseVerbosity = true;
        }
        if (b.impl.ctx.expansionTruncated) {
            b.verbosity.Truncated = true;
        }
    }
}

internal static void popContext(this ref NodeBuilder b) {
    nint stackSize = len(b.ctxStack);
    if (stackSize == 0) {
        b.impl.ctx = default;
    } else {
        b.impl.ctx = b.ctxStack[stackSize - 1];
        b.ctxStack = b.ctxStack.slice(0, stackSize - 1);
    }
}

internal static ж<ast.Node> exitContext(this ж<NodeBuilder> Ꮡb, ж<ast.Node> result) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var b = ref Ꮡb.DerefOrNull();

        b.propagateVerbosityOut();
        b.exitContextCheck();
        ᒐd1 = true;
        if (b.impl.ctx.encounteredError) {
            return default;
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡb.DerefOrNull().popContext(); ᒐ.Run(); }
}

internal static slice<ж<ast.Node>> exitContextSlice(this ж<NodeBuilder> Ꮡb, slice<ж<ast.Node>> result) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var b = ref Ꮡb.DerefOrNull();

        b.propagateVerbosityOut();
        b.exitContextCheck();
        ᒐd1 = true;
        if (b.impl.ctx.encounteredError) {
            return default;
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡb.DerefOrNull().popContext(); ᒐ.Run(); }
}

internal static void exitContextCheck(this ref NodeBuilder b) {
    if (b.impl.ctx.truncating && (nodebuilder.Flags)(b.impl.ctx.flags & nodebuilder.FlagsNoTruncation) != 0) {
        b.impl.ctx.tracker.ReportTruncationError();
    }
}

public static ж<ast.Node> IndexInfoToIndexSignatureDeclaration(this ж<NodeBuilder> Ꮡb, ж<IndexInfo> info, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.indexInfoToIndexSignatureDeclarationHelper(info, nil));
}

public static ж<ast.Node> SerializeReturnTypeForSignature(this ж<NodeBuilder> Ꮡb, ж<ast.Node> signatureDeclaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    var signature = b.impl.ch.getSignatureFromDeclaration(signatureDeclaration);
    var (_, cleanup) = b.impl.enterSignatureScope(signature);
    var result = b.impl.serializeReturnTypeForSignature(signature, true);
    cleanup();
    return Ꮡb.exitContext(result);
}

public static slice<ж<ast.Node>> SerializeTypeParametersForSignature(this ж<NodeBuilder> Ꮡb, ж<ast.Node> signatureDeclaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    var symbol = b.impl.ch.getSymbolOfDeclaration(signatureDeclaration);
    var typeParams = Ꮡb.SymbolToTypeParameterDeclarations(symbol, enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContextSlice(typeParams);
}

public static ж<ast.Node> SerializeTypeForDeclaration(this ж<NodeBuilder> Ꮡb, ж<ast.Node> declaration, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.serializeTypeForDeclaration(declaration, nil, symbol, true));
}

public static ж<ast.Node> SerializeTypeForExpression(this ж<NodeBuilder> Ꮡb, ж<ast.Node> expr, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.serializeTypeForExpression(expr));
}

public static ж<ast.Node> SignatureToSignatureDeclaration(this ж<NodeBuilder> Ꮡb, ж<Signature> signature, ast.Kind kind, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.signatureToSignatureDeclarationHelper(signature, kind, nil));
}

public static slice<ж<ast.Node>> ExpandSymbolForHover(this ж<NodeBuilder> Ꮡb, ж<ast.Symbol> symbol, ast.SymbolFlags meaning) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(nil, (nodebuilder.Flags)((nodebuilder.Flags)(nodebuilder.FlagsIgnoreErrors | nodebuilder.FlagsMultilineObjectLiterals) | nodebuilder.FlagsUseAliasDefinedOutsideCurrentScope), nodebuilder.InternalFlagsNone, default);

    var declaredType = b.impl.ch.getDeclaredTypeOfSymbol(symbol);
    b.impl.ctx.typeStack = append(b.impl.ctx.typeStack, declaredType);
    b.impl.ctx.typeStack = append(b.impl.ctx.typeStack, (ж<Type>)(nil));

    var nodes = b.impl.expandSymbolForHover(symbol);

    b.impl.ctx.typeStack = b.impl.ctx.typeStack.slice(0, len(b.impl.ctx.typeStack) - 2);

    b.propagateVerbosityOut();

    var result = new slice<ж<ast.Node>>(0, len(nodes));
    foreach (var (_, node) in range(nodes)) {
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            result = append(result, simplifyClassDeclaration(b.impl.f, node, ref (symbol).DerefOrNull()));
            break;
        }
        case var ᴛ when ᴛ == ast.KindEnumDeclaration: {
            result = append(result, simplifyModifiers(b.impl.f, node, ast.IsEnumDeclaration, ref (symbol).DerefOrNull()));
            break;
        }
        case var ᴛ when ᴛ == ast.KindInterfaceDeclaration: {
            if ((ast.SymbolFlags)(meaning & ast.SymbolFlagsInterface) != 0) {
                result = append(result, simplifyModifiers(b.impl.f, node, ast.IsInterfaceDeclaration, ref (symbol).DerefOrNull()));
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindModuleDeclaration: {
            result = append(result, simplifyModifiers(b.impl.f, node, ast.IsModuleDeclaration, ref (symbol).DerefOrNull()));
            break;
        }}

    }

    return Ꮡb.exitContextSlice(result);
}

} partial class checker_package {

internal static ж<ast.Node> simplifyClassDeclaration(ж<ast.NodeFactory> Ꮡf, ж<ast.Node> ᏑclassDecl, ref ast.Symbol symbol) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var classDecl = ref ᏑclassDecl.DerefOrNull();

    var classDeclarations = core.Filter<ж<ast.Node>>(symbol.Declarations, ast.IsClassLike);
    ж<ast.Node> originalClassDecl = default;
    if (len(classDeclarations) > 0) {
        originalClassDecl = classDeclarations[0];
    } else {
        originalClassDecl = ᏑclassDecl;
    }
    var modifiers = (ast.ModifierFlags)(originalClassDecl.ModifierFlags() & ~((ast.ModifierFlags)((ast.ModifierFlags)(ast.ModifierFlagsExport | ast.ModifierFlagsAmbient))));
    var isAnonymous = ast.IsClassExpression(originalClassDecl);
    if (isAnonymous) {
        var cd = classDecl.AsClassDeclaration();
        ᏑclassDecl = f.UpdateClassDeclaration(
            cd,
            classDecl.Modifiers(),
            nil,
            (~cd).TypeParameters,
            cd.HeritageClauses,
            (~cd).Members); classDecl = ref ᏑclassDecl.DerefOrNull();
    }
    return ast.ReplaceModifiers(Ꮡf, ᏑclassDecl, f.NewModifierList(ast.CreateModifiersFromModifierFlags(modifiers, Ꮡf.NewModifier)));
}

internal static ж<ast.Node> simplifyModifiers(ж<ast.NodeFactory> Ꮡf, ж<ast.Node> newDecl, Func<ж<ast.Node>, bool> isDeclKind, ref ast.Symbol symbol) {
    ref var f = ref Ꮡf.DerefOrNull();

    var decls = core.Filter(symbol.Declarations, isDeclKind);
    ж<ast.Node> declWithModifiers = default;
    if (len(decls) > 0) {
        declWithModifiers = decls[0];
    } else {
        declWithModifiers = newDecl;
    }
    var modifiers = (ast.ModifierFlags)(declWithModifiers.ModifierFlags() & ~((ast.ModifierFlags)((ast.ModifierFlags)(ast.ModifierFlagsExport | ast.ModifierFlagsAmbient))));
    return ast.ReplaceModifiers(Ꮡf, newDecl, f.NewModifierList(ast.CreateModifiersFromModifierFlags(modifiers, Ꮡf.NewModifier)));
}

} partial class checkerᴛNodeBuilder {

public static ж<ast.Node> SymbolToEntityName(this ж<NodeBuilder> Ꮡb, ж<ast.Symbol> symbol, ast.SymbolFlags meaning, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.symbolToName(symbol, meaning, false));
}

public static ж<ast.Node> SymbolToExpression(this ж<NodeBuilder> Ꮡb, ж<ast.Symbol> symbol, ast.SymbolFlags meaning, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.symbolToExpression(symbol, meaning));
}

public static ж<ast.Node> SymbolToNode(this ж<NodeBuilder> Ꮡb, ж<ast.Symbol> symbol, ast.SymbolFlags meaning, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.symbolToNode(symbol, meaning));
}

public static ж<ast.Node> SymbolToParameterDeclaration(this NodeBuilder bʗp, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref heap(bʗp, out var Ꮡb);

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.symbolToParameterDeclaration(symbol, false));
}

public static slice<ж<ast.Node>> SymbolToTypeParameterDeclarations(this ж<NodeBuilder> Ꮡb, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContextSlice(b.impl.symbolToTypeParameterDeclarations(symbol));
}

public static ж<ast.Node> TypeParameterToDeclaration(this ж<NodeBuilder> Ꮡb, ж<Type> parameter, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.typeParameterToDeclaration(parameter));
}

public static ж<ast.Node> TypePredicateToTypePredicateNode(this ж<NodeBuilder> Ꮡb, ж<TypePredicate> predicate, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.typePredicateToTypePredicateNode(predicate));
}

public static ж<ast.Node> TypeToTypeNode(this ж<NodeBuilder> Ꮡb, ж<Type> typ, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.typeToTypeNode(typ));
}

public static ж<ast.Node> TryJSTypeNodeToTypeNode(this ж<NodeBuilder> Ꮡb, ж<ast.Node> node, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker) {
    ref var b = ref Ꮡb.DerefOrNull();

    b.enterContext(enclosingDeclaration, flags, internalFlags, tracker);
    return Ꮡb.exitContext(b.impl.tryJSTypeNodeToTypeNode(node));
}

} partial class checker_package {

public static ж<NodeBuilder> NewNodeBuilder(ж<Checker> ch, ж<printer.EmitContext> e) {
    return NewNodeBuilderEx(ch, e, default);
}

public static ж<NodeBuilder> NewNodeBuilderEx(ж<Checker> Ꮡch, ж<printer.EmitContext> e, map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> idToSymbol) {
    ref var ch = ref Ꮡch.DerefOrNull();

    var impl = newNodeBuilderImpl(Ꮡch, e, idToSymbol);
    return Ꮡ(new NodeBuilder(impl: impl, ctxStack: new slice<ж<NodeBuilderContext>>(0, 1), host: ch.program));
}

} partial class checkerᴛChecker {

internal static (ж<NodeBuilder>, Action) getNodeBuilder(this ж<Checker> Ꮡc) {
    ref var c = ref Ꮡc.DerefOrNull();

    var releaseNodes = () => {
        Ꮡc.Value.typeToStringNodebuilder.EmitContext().Factory.of(printer.NodeFactory.ᏑΔNodeFactory).ReleaseArenas();
    };
    if (c.typeToStringNodebuilder != nil) {
        return (c.typeToStringNodebuilder, releaseNodes);
    }
    c.typeToStringNodebuilder = Ꮡc.getNodeBuilderEx(default);
    return (c.typeToStringNodebuilder, releaseNodes);
}

internal static ж<NodeBuilder> getNodeBuilderEx(this ж<Checker> Ꮡc, map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> idToSymbol) {
    var b = NewNodeBuilderEx(Ꮡc, printer.NewEmitContext(), idToSymbol);
    return b;
}

} // end checkerᴛChecker
