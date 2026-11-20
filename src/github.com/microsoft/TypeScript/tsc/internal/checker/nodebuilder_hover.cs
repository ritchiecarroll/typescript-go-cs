namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

internal static bool isExpanding(ref NodeBuilderContext ctx) {
    return ctx.maxExpansionDepth != -1;
}

} partial class checkerᴛNodeBuilderImpl {

internal static slice<ж<ast.Node>> expandSymbolForHover(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    slice<ж<ast.Node>> results = default;
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsEnum) != 0) {
        var node = b.expandEnumDecl(Ꮡsymbol); if (node != nil) {
            results = append(results, node);
        }
    }
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsClass) != 0) {
        var node = Ꮡb.expandClassDecl(Ꮡsymbol); if (node != nil) {
            results = append(results, node);
        }
    }
    if ((ast.SymbolFlags)(symbol.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsValueModule | ast.SymbolFlagsNamespaceModule))) != 0) {
        var node = Ꮡb.expandModuleDecl(Ꮡsymbol); if (node != nil) {
            results = append(results, node);
        }
    }
    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsInterface) != 0 && (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsClass) == 0) {
        var node = Ꮡb.expandInterfaceDecl(Ꮡsymbol); if (node != nil) {
            results = append(results, node);
        }
    }
    return results;
}

internal static ж<ast.Node> expandEnumDecl(this ref NodeBuilderImpl b, ж<ast.Symbol> symbol) {
    @string name = ast.SymbolName(symbol);
    b.ctx.approximateLength += 9 + len(name);
    var memberProps = core.Filter(b.ch.getPropertiesOfType(b.ch.getTypeOfSymbol(symbol)), (ж<ast.Symbol> p) => (ast.SymbolFlags)(p.Flags & ast.SymbolFlagsEnumMember) != 0);
    slice<ж<ast.Node>> members = default;
    foreach (var (i, p) in range(memberProps)) {
        if (b.checkTruncationLengthIfExpanding() && i + 3 < len(memberProps) - 1) {
            b.ctx.expansionTruncated = true;
            members = append(members, b.f.NewEnumMember(b.f.NewStringLiteral(fmt.Sprintf(" ... %d more ... "u8, len(memberProps) - i - 1), 0), nil));
            var last = memberProps[len(memberProps) - 1];
            members = append(members, b.f.NewEnumMember(b.f.NewIdentifier(last.Name), b.enumMemberInitializer(last)));
            break;
        }
        var memberDecl = core.Find<ж<ast.Node>>(p.Declarations, ast.IsEnumMember);
        ж<ast.Node> initializer = default;
        if (memberDecl != nil && (~memberDecl.AsEnumMember()).Initializer != nil) {
            initializer = b.f.DeepCloneNode((~memberDecl.AsEnumMember()).Initializer);
        } else {
            initializer = b.enumMemberInitializer(p);
        }
        b.ctx.approximateLength += 4 + len(p.Name);
        if (initializer != nil) {
            b.ctx.approximateLength += 5;
        }
        members = append(members, b.f.NewEnumMember(b.f.NewIdentifier(p.Name), initializer));
    }

    var constModifier = ast.ModifierFlagsNone;
    if (isConstEnumSymbol(ref (symbol).DerefOrNull())) {
        constModifier = ast.ModifierFlagsConst;
    }
    ж<ast.ModifierList> mods = default;
    if (constModifier != 0) {
        mods = b.f.NewModifierList(ast.CreateModifiersFromModifierFlags(constModifier, b.f.NewModifier));
    }
    return b.f.NewEnumDeclaration(mods, b.f.NewIdentifier(name), b.f.NewNodeList(members));
}

internal static ж<ast.Node> enumMemberInitializer(this ref NodeBuilderImpl b, ж<ast.Symbol> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    var memberDecl = core.Find<ж<ast.Node>>(p.Declarations, ast.IsEnumMember);
    if (memberDecl == nil) {
        return default;
    }
    var val = b.ch.GetConstantValue(memberDecl);
    if (val == nil) {
        return default;
    }
    switch (val.type()) {
    case @string v: {
        return b.f.NewStringLiteral(v, 0);
    }
    case jsnum.Number v: {
        return b.f.NewNumericLiteral(v.String(), 0);
    }}
    return default;
}

internal static ж<ast.Node> expandClassDecl(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol) {
    GoFrame ᒐ = default;
    try {
        ref var b = ref Ꮡb.DerefOrNull();
        ref var symbol = ref Ꮡsymbol.DerefOrNull();

        @string name = ast.SymbolName(Ꮡsymbol);
        b.ctx.approximateLength += 9 + len(name);

        var classLikeDeclarations = core.Filter<ж<ast.Node>>(symbol.Declarations, ast.IsClassLike);
        var originalDecl = core.FirstOrNil(classLikeDeclarations);
        var oldEnclosing = b.ctx.enclosingDeclaration;
        if (originalDecl != nil) {
            b.ctx.enclosingDeclaration = originalDecl;
        }
        var oldEnclosingʗ1 = oldEnclosing;
        defer(() => {
            Ꮡb.Value.ctx.enclosingDeclaration = oldEnclosingʗ1;
        }, ref ᒐ);

        var localParams = b.ch.getLocalTypeParametersOfClassOrInterfaceOrTypeAlias(Ꮡsymbol);
        var typeParamDecls = core.Map(localParams, ж<ast.Node> (ж<Type> p) => Ꮡb.typeParameterToDeclaration(p));

        var declaredType = b.ch.getDeclaredTypeOfClassOrInterface(Ꮡsymbol);
        var classType = b.ch.getTypeWithThisArgument(declaredType, nil, false);
        var baseTypes = b.ch.getBaseTypes(b.ch.getTargetType(classType));
        var staticType = b.ch.getTypeOfSymbol(Ꮡsymbol);
        var isClass = staticType.symbol != nil && staticType.symbol.ValueDeclaration != nil && ast.IsClassLike(staticType.symbol.ValueDeclaration);
        ж<Type> staticBaseType = default;
        if (isClass) {
            staticBaseType = b.ch.getBaseConstructorTypeOfClass(declaredType);
        } else {
            staticBaseType = b.ch.anyType;
        }

        var heritageClauses = b.hoverHeritageClauses(classLikeDeclarations);

        var allProps = b.ch.getPropertiesOfType(classType);
        var symbolProps = b.filterInheritedProperties(classType, baseTypes, allProps);
        var publicProps = core.Filter(symbolProps, (ж<ast.Symbol> s) => !isHashPrivate(s));
        var hasPrivate = core.Some<ж<ast.Symbol>>(symbolProps, isHashPrivate);

        slice<ж<ast.Node>> instanceMembers = default;
        instanceMembers = Ꮡb.serializePropertiesWithTruncation(publicProps, instanceMembers);
        instanceMembers = typeElementsToClassElements(b.f, instanceMembers);
        instanceMembers = b.addClassModifiers(instanceMembers, false);

        var staticProps = core.Filter(b.ch.getPropertiesOfType(staticType), (ж<ast.Symbol> p) => (ast.SymbolFlags)(p.Flags & ast.SymbolFlagsPrototype) == 0 && p.Name != "prototype"u8 && !Ꮡb.Value.isNamespaceMember(p));
        slice<ж<ast.Node>> staticMembers = default;
        staticMembers = Ꮡb.serializePropertiesWithTruncation(staticProps, staticMembers);
        staticMembers = typeElementsToClassElements(b.f, staticMembers);
        staticMembers = b.addClassModifiers(staticMembers, true);

        slice<ж<ast.Node>> privateMembers = default;
        if (hasPrivate) {
            privateMembers = Ꮡb.serializePropertiesWithTruncation(core.Filter<ж<ast.Symbol>>(symbolProps, isHashPrivate), privateMembers);
            privateMembers = typeElementsToClassElements(b.f, privateMembers);
        }

        var constructors = Ꮡb.serializeConstructors(staticType, staticBaseType, isClass, Ꮡsymbol);

        var indexSigs = Ꮡb.serializeIndexSignaturesOfType(classType, core.FirstOrNil(baseTypes));

        var allMembers = new slice<ж<ast.Node>>(0, len(indexSigs) + len(staticMembers) + len(constructors) + len(instanceMembers) + len(privateMembers));
        allMembers = appendꓸꓸꓸ(allMembers, indexSigs);
        allMembers = appendꓸꓸꓸ(allMembers, staticMembers);
        allMembers = appendꓸꓸꓸ(allMembers, constructors);
        allMembers = appendꓸꓸꓸ(allMembers, instanceMembers);
        allMembers = appendꓸꓸꓸ(allMembers, privateMembers);

        return b.f.NewClassDeclaration(nil, b.f.NewIdentifier(name), b.f.NewNodeList(typeParamDecls), b.f.NewNodeList(heritageClauses), b.f.NewNodeList(allMembers));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static slice<ж<ast.Node>> addClassModifiers(this ref NodeBuilderImpl b, slice<ж<ast.Node>> members, bool isStatic) {
    foreach (var (i, m) in range(members)) {
        ж<ast.Symbol> memberSymbol = default;
        var memberName = m.Name();
        if (memberName != nil) {
            var (sym, ok) = b.idToSymbol[memberName, ꟷ]; if (ok) {
                memberSymbol = sym;
            }
        }
        if (memberSymbol == nil) {
            continue;
        }
        var modFlags = (ast.ModifierFlags)(getDeclarationModifierFlagsFromSymbol(ref (memberSymbol).DerefOrNull()) & ~ast.ModifierFlagsAsync);
        if (isStatic) {
            modFlags |= (ast.ModifierFlags)(ast.ModifierFlagsStatic);
        }
        if (modFlags != 0 && ast.CanHaveModifiers(m)) {
            var existing = m.ModifierFlags();
            if (modFlags != existing) {
                members[i] = ast.ReplaceModifiers(b.f, m, b.f.NewModifierList(ast.CreateModifiersFromModifierFlags((ast.ModifierFlags)(modFlags | existing), b.f.NewModifier)));
            }
        }
    }
    return members;
}

} partial class checker_package {

internal static slice<ж<ast.Node>> typeElementsToClassElements(ж<ast.NodeFactory> Ꮡf, slice<ж<ast.Node>> members) {
    ref var f = ref Ꮡf.DerefOrNull();

    foreach (var (i, m) in range(members)) {
        switch (m.Kind) {
        case var ᴛ when ᴛ == ast.KindPropertySignature: {
            var ps = m.AsPropertySignatureDeclaration();
            members[i] = f.NewPropertyDeclaration(m.Modifiers(), ps.Name(), ps.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).QuestionToken(), (~ps).Type, nil);
            break;
        }
        case var ᴛ when ᴛ == ast.KindMethodSignature: {
            var ms = m.AsMethodSignatureDeclaration();
            members[i] = f.NewMethodDeclaration(m.Modifiers(), nil, ms.Name(), ms.of(ast.MethodSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).QuestionToken(), (~ms).TypeParameters, (~ms).Parameters, (~ms).Type, nil, nil);
            break;
        }}

    }
    return members;
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> expandInterfaceDecl(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    @string name = ast.SymbolName(Ꮡsymbol);
    b.ctx.approximateLength += 14 + len(name);

    var interfaceType = b.ch.getDeclaredTypeOfClassOrInterface(Ꮡsymbol);
    var interfaceDeclarations = core.Filter<ж<ast.Node>>(symbol.Declarations, ast.IsInterfaceDeclaration);
    var localParams = b.ch.getLocalTypeParametersOfClassOrInterfaceOrTypeAlias(Ꮡsymbol);
    var typeParamDecls = core.Map(localParams, ж<ast.Node> (ж<Type> p) => Ꮡb.typeParameterToDeclaration(p));
    var baseTypes = b.ch.getBaseTypes(interfaceType);
    ж<Type> baseType = default;
    if (len(baseTypes) > 0) {
        baseType = b.ch.getIntersectionType(baseTypes);
    }

    var resolved = b.ch.resolveStructuredTypeMembers(interfaceType);
    slice<ж<ast.Node>> members = default;

    members = appendꓸꓸꓸ(members, Ꮡb.serializeIndexSignaturesOfType(interfaceType, baseType));
    foreach (var (_, sig) in range(resolved.ConstructSignatures())) {
        if ((SignatureFlags)(sig.flags & SignatureFlagsAbstract) != 0) {
            continue;
        }
        members = append(members, Ꮡb.signatureToSignatureDeclarationHelper(sig, ast.KindConstructSignature, nil));
    }
    foreach (var (_, sig) in range(resolved.CallSignatures())) {
        members = append(members, Ꮡb.signatureToSignatureDeclarationHelper(sig, ast.KindCallSignature, nil));
    }
    var filteredProps = b.filterInheritedProperties(interfaceType, baseTypes, resolved.properties);
    members = Ꮡb.serializePropertiesWithTruncation(filteredProps, members);

    var heritageClauses = b.hoverHeritageClauses(interfaceDeclarations);

    return b.f.NewInterfaceDeclaration(nil, b.f.NewIdentifier(name), b.f.NewNodeList(typeParamDecls), b.f.NewNodeList(heritageClauses), b.f.NewNodeList(members));
}

internal static slice<ж<ast.Node>> hoverHeritageClauses(this ref NodeBuilderImpl b, slice<ж<ast.Node>> declarations) {
    slice<ж<ast.Node>> extendsTypes = default;
    slice<ж<ast.Node>> implementsTypes = default;
    foreach (var (_, declaration) in range(declarations)) {
        foreach (var (_, heritageElement) in range(ast.GetExtendsHeritageClauseElements(declaration))) {
            extendsTypes = append(extendsTypes, b.f.DeepCloneNode(heritageElement.AsNode()));
        }
        foreach (var (_, heritageElement) in range(ast.GetImplementsHeritageClauseElements(declaration))) {
            implementsTypes = append(implementsTypes, b.f.DeepCloneNode(heritageElement.AsNode()));
        }
    }

    slice<ж<ast.Node>> heritageClauses = default;
    if (len(extendsTypes) > 0) {
        heritageClauses = append(heritageClauses, b.f.NewHeritageClause(ast.KindExtendsKeyword, b.f.NewNodeList(extendsTypes)));
    }
    if (len(implementsTypes) > 0) {
        heritageClauses = append(heritageClauses, b.f.NewHeritageClause(ast.KindImplementsKeyword, b.f.NewNodeList(implementsTypes)));
    }
    return heritageClauses;
}

internal static slice<ж<ast.Node>> serializePropertiesWithTruncation(this ж<NodeBuilderImpl> Ꮡb, slice<ж<ast.Symbol>> properties, slice<ж<ast.Node>> elements) {
    ref var b = ref Ꮡb.DerefOrNull();

    properties = core.Filter(properties, (ж<ast.Symbol> p) => (ast.SymbolFlags)(p.Flags & ast.SymbolFlagsPrototype) == 0);
    foreach (var (i, p) in range(properties)) {
        if (b.checkTruncationLengthIfExpanding() && (i + 3 < len(properties) - 1)) {
            b.ctx.expansionTruncated = true;
            @string text = fmt.Sprintf("... %d more ..."u8, len(properties) - i - 1);
            elements = append(elements, b.f.NewPropertySignatureDeclaration(nil, b.f.NewIdentifier(text), nil, nil, nil));
            elements = Ꮡb.addPropertyToElementList(properties[len(properties) - 1], elements);
            break;
        }
        elements = Ꮡb.addPropertyToElementList(p, elements);
    }
    return elements;
}

internal static slice<ж<ast.Node>> serializeConstructors(this ж<NodeBuilderImpl> Ꮡb, ж<Type> staticType, ж<Type> staticBaseType, bool isClass, ж<ast.Symbol> Ꮡsymbol) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var symbol = ref Ꮡsymbol.DerefOrNull();
    var resultᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    var isNonConstructable = !isClass && symbol.ValueDeclaration != nil && ast.IsInJSFile(symbol.ValueDeclaration) && len(b.ch.getSignaturesOfType(staticType, SignatureKindConstruct)) == 0;
    if (isNonConstructable) {
        b.ctx.approximateLength += 21;
        var modifiers = ast.CreateModifiersFromModifierFlags(ast.ModifierFlagsPrivate, b.f.NewModifier);
        return slice<ж<ast.Node>>([b.f.NewConstructorDeclaration(b.f.NewModifierList(modifiers), nil, b.f.NewNodeList(default), nil, nil, nil)]);
    }
    var signatures = b.ch.getSignaturesOfType(staticType, SignatureKindConstruct);
    if (staticBaseType != nil) {
        var baseSigs = b.ch.getSignaturesOfType(staticBaseType, SignatureKindConstruct);
        if (len(baseSigs) == 0 && core.Every(signatures, (ж<Signature> sig) => len(sig.parameters) == 0)) {
            return default;
        }
        if (len(baseSigs) == len(signatures)) {
            var allMatch = true;
            foreach (var i in range(len(baseSigs))) {
                if (b.ch.compareSignaturesIdentical(signatures[i], baseSigs[i], false, false, true, b.ch.compareTypesIdentical) != TernaryTrue) {
                    allMatch = false;
                    break;
                }
            }
            if (allMatch) {
                return default;
            }
        }
        ast.ModifierFlags privateProtected = default;
        foreach (var (_, sig) in range(signatures)) {
            if (sig.declaration != nil) {
                privateProtected |= (ast.ModifierFlags)((ast.ModifierFlags)(sig.declaration.ModifierFlags() & ((ast.ModifierFlags)(ast.ModifierFlagsPrivate | ast.ModifierFlagsProtected))));
            }
        }
        if (privateProtected != 0) {
            return slice<ж<ast.Node>>([b.f.NewConstructorDeclaration(
                b.f.NewModifierList(ast.CreateModifiersFromModifierFlags(privateProtected, b.f.NewModifier)),
                nil, b.f.NewNodeList(default), nil, nil, nil)
            ]);
        }
    } else if (core.Every(signatures, (ж<Signature> sig) => len(sig.parameters) == 0)) {
        return default;
    }
    slice<ж<ast.Node>> result = default;
    foreach (var (_, sig) in range(signatures)) {
        b.ctx.approximateLength++;
        result = append(resultᴷ, result, Ꮡb.signatureToSignatureDeclarationHelper(sig, ast.KindConstructor, nil));
    }
    result = move2heap(resultᴷ, result);
    return result;
}

internal static slice<ж<ast.Node>> serializeIndexSignaturesOfType(this ж<NodeBuilderImpl> Ꮡb, ж<Type> input, ж<Type> baseType) {
    ref var b = ref Ꮡb.DerefOrNull();
    var resultᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    slice<ж<ast.Node>> result = default;
    foreach (var (_, info) in range(b.ch.getIndexInfosOfType(input))) {
        if (baseType != nil) {
            var baseInfo = b.ch.getIndexInfoOfType(baseType, info.keyType);
            if (baseInfo != nil && b.ch.isTypeIdenticalTo(info.valueType, baseInfo.valueType)) {
                continue;
            }
        }
        result = append(resultᴷ, result, Ꮡb.indexInfoToIndexSignatureDeclarationHelper(info, nil));
    }
    result = move2heap(resultᴷ, result);
    return result;
}

internal static ж<ast.Node> serializeNamespaceMember(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡresolved, @string name) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var resolved = ref Ꮡresolved.DerefOrNull();

    switch (ᐧ) {
    case {} when (ast.SymbolFlags)(resolved.Flags & ast.SymbolFlagsTypeAlias) != 0: {
        return Ꮡb.serializeTypeAliasForNamespace(Ꮡresolved, name);
    }
    case {} when (ast.SymbolFlags)(resolved.Flags & ast.SymbolFlagsEnum) != 0: {
        return b.expandEnumDecl(Ꮡresolved);
    }
    case {} when (ast.SymbolFlags)(resolved.Flags & ast.SymbolFlagsClass) != 0: {
        return Ꮡb.expandClassDecl(Ꮡresolved);
    }
    case {} when (ast.SymbolFlags)(resolved.Flags & ast.SymbolFlagsInterface) != 0: {
        return Ꮡb.expandInterfaceDecl(Ꮡresolved);
    }
    case {} when (ast.SymbolFlags)(resolved.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsValueModule | ast.SymbolFlagsNamespaceModule))) != 0: {
        return Ꮡb.expandModuleDecl(Ꮡresolved);
    }
    default: {
        var t = b.ch.getWidenedType(b.ch.getTypeOfSymbol(Ꮡresolved));
        b.ctx.approximateLength += len(name) + 5;
        return b.f.NewVariableStatement(
            nil,
            b.f.NewVariableDeclarationList(
                b.f.NewNodeList(slice<ж<ast.Node>>([
                    b.f.NewVariableDeclaration(b.f.NewIdentifier(name), nil, Ꮡb.serializeTypeForDeclaration(nil, t, Ꮡresolved, true), nil)
                ])),
                ast.NodeFlagsLet));
    }}

}

} partial class checker_package {

internal partial struct expandModuleDecl_hoverStatement /*dyn*/ {
    internal ж<ast.Node> node;
    internal bool isLocal;
}

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> expandModuleDecl(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> Ꮡsymbol) {
    GoFrame ᒐ = default;
    try {
        ref var b = ref Ꮡb.DerefOrNull();
        ref var symbol = ref Ꮡsymbol.DerefOrNull();
        var membersᴷ = new stackAppend<ж<ast.Symbol>>();
        var bodyStmtsᴷ = new stackAppend<expandModuleDecl_hoverStatement>();

        var exports = b.ch.getExportsOfSymbol(Ꮡsymbol);
        slice<ж<ast.Symbol>> members = default;
        foreach (var (_, sym) in exports) {
            if (!b.isNamespaceMember(sym)) {
                continue;
            }
            if (!scanner.IsIdentifierText(sym.Name, core.LanguageVariantStandard)) {
                continue;
            }
            members = append(membersᴷ, members, sym);
        }
        b.ch.sortSymbols(members);
        b.ctx.approximateLength += 14;

        var oldFlags = b.ctx.flags;
        defer(() => {
            Ꮡb.Value.ctx.flags = oldFlags;
        }, ref ᒐ);
        b.ctx.flags |= (nodebuilder.Flags)((nodebuilder.Flags)(nodebuilder.FlagsWriteTypeParametersInQualifiedName | ((nodebuilder.Flags)(uint32)SymbolFormatFlagsUseOnlyExternalAliasing)));
        var localName = Ꮡb.symbolToNode(Ꮡsymbol, ast.SymbolFlagsAll);
        b.ctx.flags = oldFlags;
        slice<expandModuleDecl_hoverStatement> bodyStmts = default;
        ref var emittedLocals = ref heap(new collections.Set<ж<ast.Symbol>>(), out var ᏑemittedLocals);
        for (nint i = 0; i < len(members); i++) {
            var m = members[i];
            if (b.checkTruncationLengthIfExpanding() && i + 3 < len(members) - 1) {
                b.ctx.expansionTruncated = true;
                bodyStmts = append(bodyStmtsᴷ, bodyStmts, new expandModuleDecl_hoverStatement(node: b.f.NewExpressionStatement(b.f.NewIdentifier(fmt.Sprintf("... (%d more) ..."u8, len(members) - i - 1)))));
                i = len(members) - 2;
                continue;
            }

            if ((ast.SymbolFlags)(m.Flags & ast.SymbolFlagsAlias) != 0) {
                var aliasDecl = b.ch.getDeclarationOfAliasSymbol(m);
                var target = b.ch.getMergedSymbol(b.ch.getTargetOfAliasDeclaration(aliasDecl));
                if (target != nil) {
                    if ((ast.SymbolFlags)(target.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsBlockScopedVariable | ast.SymbolFlagsFunctionScopedVariable) | ast.SymbolFlagsProperty))) != 0) {
                        if (ᏑemittedLocals.AddIfAbsent(target)) {
                            var localType = b.ch.getWidenedType(b.ch.getTypeOfSymbol(target));
                            b.ctx.approximateLength += len(target.Name) + 5;
                            var localStmt = b.f.NewVariableStatement(nil,
                                b.f.NewVariableDeclarationList(b.f.NewNodeList(slice<ж<ast.Node>>([
                                    b.f.NewVariableDeclaration(b.f.NewIdentifier(target.Name), nil, Ꮡb.serializeTypeForDeclaration(nil, localType, target, true), nil)
                                ])), ast.NodeFlagsLet));
                            bodyStmts = append(bodyStmts, new expandModuleDecl_hoverStatement(node: localStmt, isLocal: true));
                        }
                    }
                    @string targetName = target.Name;
                    b.ctx.approximateLength += 16 + len(m.Name);
                    ж<ast.Node> propertyName = default;
                    if (m.Name != targetName) {
                        propertyName = b.f.NewIdentifier(targetName);
                    }
                    var stmt = b.f.NewExportDeclaration(
                        nil, false,
                        b.f.NewNamedExports(b.f.NewNodeList(slice<ж<ast.Node>>([
                            b.f.NewExportSpecifier(false, propertyName, b.f.NewIdentifier(m.Name))
                        ]))),
                        nil, nil);
                    bodyStmts = append(bodyStmts, new expandModuleDecl_hoverStatement(node: stmt));
                    continue;
                }
            }

            var resolved = b.ch.resolveSymbol(m);

            if ((ast.SymbolFlags)(resolved.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsFunction | ast.SymbolFlagsMethod))) != 0) {
                var t = b.ch.getTypeOfSymbol(resolved);
                var sigs = b.ch.getSignaturesOfType(t, SignatureKindCall);
                foreach (var (_, sig) in range(sigs)) {
                    b.ctx.approximateLength++;
                    var decl = Ꮡb.signatureToSignatureDeclarationHelper(sig, ast.KindFunctionDeclaration, Ꮡ(new SignatureToSignatureDeclarationOptions(
                        name: b.f.NewIdentifier(m.Name)
                    )));
                    bodyStmts = append(bodyStmts, new expandModuleDecl_hoverStatement(node: decl));
                }
                var merged = b.ch.getMergedSymbol(resolved);
                var hasModuleExports = (ast.SymbolFlags)(merged.Flags & ((ast.SymbolFlags)(ast.SymbolFlagsValueModule | ast.SymbolFlagsNamespaceModule))) != 0 && merged.Exports != nil && len(merged.Exports) != 0;
                if (!hasModuleExports) {
                    bodyStmts = append(bodyStmts, new expandModuleDecl_hoverStatement(node: b.f.NewModuleDeclaration(nil, ast.KindNamespaceKeyword, b.f.NewIdentifier(m.Name), nil, b.f.NewModuleBlock(b.f.NewNodeList(default)))));
                }
                continue;
            }

            var node = Ꮡb.serializeNamespaceMember(resolved, m.Name); if (node != nil) {
                bodyStmts = append(bodyStmts, new expandModuleDecl_hoverStatement(node: node));
            }
        }

        foreach (var i in range(len(bodyStmts))) {
            var s = Ꮡ(bodyStmts, i);
            if (s.isLocal || ast.IsExportDeclaration(s.node)) {
                continue;
            }
            if (ast.CanHaveModifiers(s.node)) {
                var mf = (ast.ModifierFlags)(s.node.ModifierFlags() | ast.ModifierFlagsExport);
                s.node = ast.ReplaceModifiers(b.f, s.node, b.f.NewModifierList(ast.CreateModifiersFromModifierFlags(mf, b.f.NewModifier)));
            }
        }

        var bodyStatements = new slice<ж<ast.Node>>(len(bodyStmts));
        foreach (var i in range(len(bodyStmts))) {
            bodyStatements[i] = bodyStmts[i].node;
        }
        var allExported = len(bodyStatements) > 0 && core.Every(bodyStatements, (ж<ast.Node> d) => ast.HasSyntacticModifier(d, ast.ModifierFlagsExport));
        if (allExported) {
            foreach (var (i, stmt) in range(bodyStatements)) {
                if (ast.CanHaveModifiers(stmt)) {
                    var mf = (ast.ModifierFlags)(stmt.ModifierFlags() & ~ast.ModifierFlagsExport);
                    bodyStatements[i] = ast.ReplaceModifiers(b.f, stmt, b.f.NewModifierList(ast.CreateModifiersFromModifierFlags(mf, b.f.NewModifier)));
                }
            }
        }

        var keyword = ast.KindNamespaceKeyword;
        if (!ast.IsIdentifier(localName)) {
            keyword = ast.KindModuleKeyword;
        }
        ж<astꓸTypeLiteralNodeNode> attributes = default;
        {
            var declaration = core.Find(symbol.Declarations, (ж<ast.Node> declarationΔ1) => ast.IsModuleDeclaration(declarationΔ1) && (~declarationΔ1.AsModuleDeclaration()).Attributes != nil); if (declaration != nil) {
                attributes = b.f.DeepCloneNode((~declaration.AsModuleDeclaration()).Attributes);
                b.e.SetEmitFlags(attributes, printer.EFSingleLine);
            }
        }
        return b.f.NewModuleDeclaration(nil, keyword, localName, attributes, b.f.NewModuleBlock(b.f.NewNodeList(bodyStatements)));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> serializeTypeAliasForNamespace(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> symbol, @string name) {
    ref var b = ref Ꮡb.DerefOrNull();

    var aliasType = b.ch.getDeclaredTypeOfTypeAlias(symbol);
    var typeParams = b.ch.getLocalTypeParametersOfClassOrInterfaceOrTypeAlias(symbol);
    var typeParamDecls = core.Map(typeParams, ж<ast.Node> (ж<Type> p) => Ꮡb.typeParameterToDeclaration(p));
    var restoreFlags = Ꮡb.saveRestoreFlags();
    b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsInTypeAlias);
    var typeNode = Ꮡb.typeToTypeNode(aliasType);
    restoreFlags();
    b.ctx.approximateLength += 8 + len(name);
    return b.f.NewTypeAliasDeclaration(nil, b.f.NewIdentifier(name), b.f.NewNodeList(typeParamDecls), typeNode);
}

internal static slice<ж<ast.Symbol>> filterInheritedProperties(this ref NodeBuilderImpl b, ж<Type> t, slice<ж<Type>> baseTypes, slice<ж<ast.Symbol>> properties) {
    if (len(baseTypes) == 0) {
        return properties;
    }
    var propsByName = new map<@string, ж<ast.Symbol>>(len(properties));
    foreach (var (_, p) in range(properties)) {
        propsByName[p.Name] = p;
    }
    ref var inherited = ref heap(new collections.Set<@string>(), out var Ꮡinherited);
    foreach (var (_, @base) in range(baseTypes)) {
        var baseWithThis = b.ch.getTypeWithThisArgument(@base, b.ch.getTargetType(t).AsInterfaceType().thisType, false);
        foreach (var (_, prop) in range(b.ch.getPropertiesOfType(baseWithThis))) {
            var (existing, ok) = propsByName[prop.Name, ꟷ]; if (ok && prop.Parent == existing.Parent) {
                inherited.Add(prop.Name);
            }
        }
    }
    if (Ꮡinherited.Len() == 0) {
        return properties;
    }
    return core.Filter(properties, (ж<ast.Symbol> p) => !Ꮡinherited.Has(p.Name));
}

internal static bool isNamespaceMember(this ref NodeBuilderImpl b, ж<ast.Symbol> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    return (ast.SymbolFlags)(p.Flags & ((ast.SymbolFlags)((ast.SymbolFlags)(ast.SymbolFlagsType | ast.SymbolFlagsNamespace) | ast.SymbolFlagsAlias))) != 0 || !((ast.SymbolFlags)(p.Flags & ast.SymbolFlagsPrototype) != 0 || p.Name == "prototype"u8 || (p.ValueDeclaration != nil && ast.HasStaticModifier(p.ValueDeclaration) && ast.IsClassLike(p.ValueDeclaration.Parent)));
}

} partial class checker_package {

internal static bool isHashPrivate(ж<ast.Symbol> Ꮡs) {
    ref var s = ref Ꮡs.DerefOrNull();

    return s.ValueDeclaration != nil && s.ValueDeclaration.Name() != nil && ast.IsPrivateIdentifier(s.ValueDeclaration.Name());
}

} // end checker_package
