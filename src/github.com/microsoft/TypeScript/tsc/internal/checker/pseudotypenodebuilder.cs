namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using pseudochecker = go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

} partial class checkerᴛNodeBuilderImpl {

internal static ж<ast.Node> pseudoTypeToNodeWithCheckerFallback(this ж<NodeBuilderImpl> Ꮡb, ж<pseudochecker.PseudoType> Ꮡt, ж<Type> checkerType) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if (t.Kind == pseudochecker.PseudoTypeKindInferred) {
        if (!b.ctx.suppressReportInferenceFallback) {
            var errorNodes = t.AsPseudoTypeInferred().ErrorNodes; if (len(errorNodes) > 0) {
                foreach (var (_, n) in range(errorNodes)) {
                    b.ctx.tracker.ReportInferenceFallback(n);
                }
            } else {
                b.ctx.tracker.ReportInferenceFallback(t.AsPseudoTypeInferred().Expression);
            }
        }
        var oldSuppress = b.ctx.suppressReportInferenceFallback;
        b.ctx.suppressReportInferenceFallback = true;
        var result = Ꮡb.typeToTypeNode(checkerType);
        b.ctx.suppressReportInferenceFallback = oldSuppress;
        return result;
    } else if (t.Kind == pseudochecker.PseudoTypeKindDirect) {
        var existing = t.AsPseudoTypeDirect().TypeNode;
        if (!b.canReuseExistingJSTypeNode(existing, checkerType)) {
            if (!b.ctx.suppressReportInferenceFallback) {
                b.ctx.tracker.ReportInferenceFallback(existing);
            }
            var oldSuppress = b.ctx.suppressReportInferenceFallback;
            b.ctx.suppressReportInferenceFallback = true;
            var result = Ꮡb.typeToTypeNode(checkerType);
            b.ctx.suppressReportInferenceFallback = oldSuppress;
            return result;
        }
    }
    return Ꮡb.pseudoTypeToNode(Ꮡt);
}

internal static ж<ast.Node> pseudoTypeToNode(this ж<NodeBuilderImpl> Ꮡb, ж<pseudochecker.PseudoType> Ꮡt) {
    GoFrame ᒐ = default;
    try {
        ref var b = ref Ꮡb.DerefOrNull();
        ref var t = ref Ꮡt.DerefOrNull();

        debug.Assert(Ꮡt != nil, "Attempted to serialize nil pseudotype");
        switch (t.Kind) {
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindDirect: {
            return Ꮡb.reuseTypeNode(t.AsPseudoTypeDirect().TypeNode);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindInferred: {
            var inferred = t.AsPseudoTypeInferred();
            var node = inferred.Expression;
            var errorNodes = inferred.ErrorNodes; if (len(errorNodes) > 0) {
                foreach (var (_, n) in range(errorNodes)) {
                    b.ctx.tracker.ReportInferenceFallback(n);
                }
            } else if (ast.IsEntityNameExpression(node) && ast.IsDeclaration(node.Parent)) {
                b.ctx.tracker.ReportInferenceFallback(node.Parent);
            } else {
                b.ctx.tracker.ReportInferenceFallback(node);
            }
            if (inferred.IsSignatureReturn) {
                return Ꮡb.serializeReturnTypeForSignature(b.ch.getSignatureFromDeclaration(node), false);
            }
            if (ast.IsReturnStatement(node.Parent)) {
                var enclosing = ast.GetContainingFunction(node);
                if (ast.IsAccessor(enclosing)) {
                    return Ꮡb.serializeTypeForDeclaration(enclosing, nil, nil, false);
                }
                return Ꮡb.serializeReturnTypeForSignature(b.ch.getSignatureFromDeclaration(enclosing), false);
            }
            if (ast.IsArrowFunction(node.Parent) && (~node.Parent.AsArrowFunction()).Body == node) {
                return Ꮡb.serializeReturnTypeForSignature(b.ch.getSignatureFromDeclaration(node.Parent), false);
            }
            if (ast.IsDeclaration(node.Parent)) {
                return Ꮡb.serializeTypeForDeclaration(node.Parent, nil, nil, false);
            }
            var ty = b.ch.getTypeOfExpression(node);
            return Ꮡb.typeToTypeNode(ty);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindNoResult: {
            var node = t.AsPseudoTypeNoResult().Declaration;
            b.ctx.tracker.ReportInferenceFallback(node);
            if (ast.IsFunctionLike(node) && !ast.IsAccessor(node)) {
                return Ꮡb.serializeReturnTypeForSignature(b.ch.getSignatureFromDeclaration(node), false);
            }
            return Ꮡb.serializeTypeForDeclaration(node, nil, nil, false);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindMaybeConstLocation: {
            var d = t.AsPseudoTypeMaybeConstLocation();
            var isInConstContext = b.ch.isConstContext(d.Node);
            if (!isInConstContext && pseudochecker.IsInConstContext(d.Node)) {
                var contextualType = b.ch.getContextualType(d.Node, ContextFlagsNone);
                var tΔ2 = b.pseudoTypeToType(d.ConstType);
                if (tΔ2 != nil && b.ch.isLiteralOfContextualType(tΔ2, b.ch.instantiateContextualType(contextualType, d.Node, ContextFlagsNone))) {
                    isInConstContext = true;
                }
            }
            if (isInConstContext) {
                return Ꮡb.pseudoTypeToNode(d.ConstType);
            } else {
                return Ꮡb.pseudoTypeToNode(d.RegularType);
            }
            break;
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindUnion: {
            ref var res = ref heap<slice<ж<ast.Node>>>(out var Ꮡres);
            bool hasElidedType = default;
            bool hasUndefined = default;
            var members = t.AsPseudoTypeUnion().Types;
            ref var appendTypeNode = ref heap<Action<ж<ast.Node>>>(out var ᏑappendTypeNode);
            appendTypeNode = (ж<ast.Node> node) => {
                if (ast.IsUnionTypeNode(node)) {
                    foreach (var (_, nodeΔ1) in range(node.AsUnionTypeNode().Types.Nodes)) {
                        ᏑappendTypeNode.ValueSlot(nodeΔ1);
                    }
                    return;
                }
                if (node.Kind == ast.KindUndefinedKeyword) {
                    if (hasUndefined) {
                        return;
                    }
                    hasUndefined = true;
                }
                Ꮡres.ValueSlot = append(Ꮡres.ValueSlot, node);
            };
            foreach (var (_, m) in range(members)) {
                if (!b.ch.strictNullChecks) {
                    if (m.Kind == pseudochecker.PseudoTypeKindUndefined || m.Kind == pseudochecker.PseudoTypeKindNull) {
                        hasElidedType = true;
                        continue;
                    }
                }
                appendTypeNode(Ꮡb.pseudoTypeToNode(m));
            }
            if (len(res) == 1) {
                return res[0];
            }
            if (len(res) == 0) {
                if (hasElidedType) {
                    return b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
                }
                return b.f.NewKeywordTypeNode(ast.KindNeverKeyword);
            }
            return b.f.NewUnionTypeNode(b.f.NewNodeList(res));
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindUndefined: {
            if (!b.ch.strictNullChecks) {
                return b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
            }
            return b.f.NewKeywordTypeNode(ast.KindUndefinedKeyword);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindNull: {
            if (!b.ch.strictNullChecks) {
                return b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
            }
            return b.f.NewLiteralTypeNode(b.f.NewKeywordExpression(ast.KindNullKeyword));
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindAny: {
            return b.f.NewKeywordTypeNode(ast.KindAnyKeyword);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindString: {
            return b.f.NewKeywordTypeNode(ast.KindStringKeyword);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindNumber: {
            return b.f.NewKeywordTypeNode(ast.KindNumberKeyword);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindBigInt: {
            return b.f.NewKeywordTypeNode(ast.KindBigIntKeyword);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindBoolean: {
            return b.f.NewKeywordTypeNode(ast.KindBooleanKeyword);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindFalse: {
            return b.f.NewLiteralTypeNode(b.f.NewKeywordExpression(ast.KindFalseKeyword));
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindTrue: {
            return b.f.NewLiteralTypeNode(b.f.NewKeywordExpression(ast.KindTrueKeyword));
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindSingleCallSignature: {
            var d = t.AsPseudoTypeSingleCallSignature();
            var signature = b.ch.getSignatureFromDeclaration(d.Signature);
            var expandedParams = b.ch.getExpandedParameters(signature, true)[0];
            var cleanup = Ꮡb.enterNewScope(d.Signature, expandedParams, signature.typeParameters, signature.parameters, signature.mapper);
            var cleanupʗ1 = cleanup;
            defer(cleanupʗ1, ref ᒐ);
            ж<ast.NodeList> typeParams = default;
            if (len(d.TypeParameters) > 0) {
                var res = new slice<ж<ast.Node>>(0, len(d.TypeParameters));
                foreach (var (_, tp) in range(d.TypeParameters)) {
                    res = append(res, Ꮡb.reuseNode(tp.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
                }
                typeParams = b.f.NewNodeList(res);
            }
            var @params = Ꮡb.pseudoParametersToNodeList(d.Parameters);
            var returnType = Ꮡb.pseudoTypeToNode(d.ReturnType);
            return b.f.NewFunctionTypeNode(typeParams, @params, returnType);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindTuple: {
            slice<ж<ast.Node>> res = default;
            var elements = t.AsPseudoTypeTuple().Elements;
            foreach (var (_, e) in range(elements)) {
                res = append(res, Ꮡb.pseudoTypeToNode(e));
            }
            var result = b.f.NewTupleTypeNode(b.f.NewNodeList(res));
            b.e.AddEmitFlags(result, printer.EFSingleLine);
            return b.f.NewTypeOperatorNode(ast.KindReadonlyKeyword, result);
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindObjectLiteral: {
            var elements = t.AsPseudoTypeObjectLiteral().Elements;
            if (len(elements) == 0) {
                var resultΔ1 = b.f.NewTypeLiteralNode(b.f.NewNodeList(default));
                b.e.AddEmitFlags(resultΔ1, printer.EFSingleLine);
                return resultΔ1;
            }
            var isConst = b.ch.isConstContext(elements[0].Name.Parent.Parent);
            var newElements = new slice<ж<ast.Node>>(0, len(elements));

            var restoreObjectLiteralFlags = Ꮡb.saveRestoreFlags();
            b.ctx.flags |= (nodebuilder.Flags)(nodebuilder.FlagsInObjectTypeLiteral);

            foreach (var (_, e) in range(elements)) {
                ж<ast.ModifierList> modifiers = default;
                if (isConst || (e.Kind == pseudochecker.PseudoObjectElementKindPropertyAssignment && e.AsPseudoPropertyAssignment().Readonly)) {
                    modifiers = b.f.NewModifierList(slice<ж<ast.Node>>([b.f.NewModifier(ast.KindReadonlyKeyword)]));
                }
                Action cleanup = default;
                if (e.Kind != pseudochecker.PseudoObjectElementKindPropertyAssignment) {
                    var signature = b.ch.getSignatureFromDeclaration(e.Signature());
                    var expandedParams = b.ch.getExpandedParameters(signature, true)[0];
                    cleanup = Ꮡb.enterNewScope(e.Signature(), expandedParams, signature.typeParameters, signature.parameters, signature.mapper);
                }
                ж<ast.Node> newProp = default;
                switch (e.Kind) {
                case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindMethod: {
                    var d = e.AsPseudoObjectMethod();
                    ж<ast.NodeList> typeParams = default;
                    if (len(d.TypeParameters) > 0) {
                        var res = new slice<ж<ast.Node>>(0, len(d.TypeParameters));
                        foreach (var (_, tp) in range(d.TypeParameters)) {
                            res = append(res, Ꮡb.reuseNode(tp.of(ast.TypeParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
                        }
                        typeParams = b.f.NewNodeList(res);
                    }
                    if (isConst) {
                        newProp = b.f.NewPropertySignatureDeclaration(
                            modifiers,
                            Ꮡb.reuseName(e.Name, false),
                            nil,
                            b.f.NewFunctionTypeNode(
                                typeParams,
                                Ꮡb.pseudoParametersToNodeList(d.Parameters),
                                Ꮡb.pseudoTypeToNode(d.ReturnType)),
                            nil);
                        break;
                    }
                    newProp = b.f.NewMethodSignatureDeclaration(
                        modifiers,
                        Ꮡb.reuseName(e.Name, true),
                        nil,
                        typeParams,
                        Ꮡb.pseudoParametersToNodeList(d.Parameters),
                        Ꮡb.pseudoTypeToNode(d.ReturnType));
                    break;
                }
                case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindPropertyAssignment: {
                    var d = e.AsPseudoPropertyAssignment();
                    newProp = b.f.NewPropertySignatureDeclaration(
                        modifiers,
                        Ꮡb.reuseName(e.Name, false),
                        nil,
                        Ꮡb.pseudoTypeToNode(d.Type),
                        nil);
                    break;
                }
                case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindSetAccessor: {
                    var d = e.AsPseudoSetAccessor();
                    newProp = b.f.NewSetAccessorDeclaration(
                        nil,
                        Ꮡb.reuseName(e.Name, false),
                        nil,
                        b.f.NewNodeList(slice<ж<ast.Node>>([Ꮡb.pseudoParameterToNode(d.Parameter)])),
                        nil,
                        nil,
                        nil);
                    break;
                }
                case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindGetAccessor: {
                    var d = e.AsPseudoGetAccessor();
                    newProp = b.f.NewGetAccessorDeclaration(
                        nil,
                        Ꮡb.reuseName(e.Name, false),
                        nil,
                        nil,
                        Ꮡb.pseudoTypeToNode(d.Type),
                        nil,
                        nil);
                    break;
                }}

                if (b.ctx.enclosingFile == ast.GetSourceFileOfNode(e.Name)) {
                    b.e.SetCommentRange(newProp, e.Name.Parent.Loc);
                }
                newElements = append(newElements, newProp);
                if (cleanup != nil) {
                    cleanup();
                }
            }
            restoreObjectLiteralFlags();
            var result = b.f.NewTypeLiteralNode(b.f.NewNodeList(newElements));
            if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsMultilineObjectLiterals) == 0) {
                b.e.AddEmitFlags(result, printer.EFSingleLine);
            }
            return result;
        }
        case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindStringLiteral || ᴛ == pseudochecker.PseudoTypeKindNumericLiteral || ᴛ == pseudochecker.PseudoTypeKindBigIntLiteral: {
            var source = t.AsPseudoTypeLiteral().Node;
            return b.f.NewLiteralTypeNode(Ꮡb.reuseNode(source));
        }
        default: {
            debug.AssertNever(t.Kind, "Unhandled pseudotype kind in pseudotype node construction");
            return default;
        }}

    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.NodeList> pseudoParametersToNodeList(this ж<NodeBuilderImpl> Ꮡb, slice<ж<pseudochecker.PseudoParameter>> @params) {
    ref var b = ref Ꮡb.DerefOrNull();

    var res = new slice<ж<ast.Node>>(0, len(@params));
    foreach (var (_, p) in range(@params)) {
        res = append(res, Ꮡb.pseudoParameterToNode(p));
    }
    return b.f.NewNodeList(res);
}

internal static ж<ast.Node> pseudoParameterToNode(this ж<NodeBuilderImpl> Ꮡb, ж<pseudochecker.PseudoParameter> Ꮡp) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var p = ref Ꮡp.DerefOrNull();

    ж<ast.Node> dotDotDot = default;
    ж<ast.Node> questionMark = default;
    if (p.Rest) {
        dotDotDot = b.f.NewToken(ast.KindDotDotDotToken);
    }
    if (p.Optional) {
        questionMark = b.f.NewToken(ast.KindQuestionToken);
    }
    var parameter = b.f.NewParameterDeclaration(
        nil,
        dotDotDot,
        b.parameterToParameterDeclarationName(p.Name.Parent.Symbol(), p.Name.Parent),
        questionMark,
        Ꮡb.pseudoTypeToNode(p.Type),
        nil);
    var original = p.Name.Parent; if (ast.IsParameterDeclaration(original)) {
        b.setCommentRange(parameter, original);
    }
    return parameter;
}

internal static bool pseudoTypeEquivalentToType(this ref NodeBuilderImpl b, ж<pseudochecker.PseudoType> Ꮡt, ж<Type> Ꮡtype_, bool isOptionalAnnotated, bool reportErrors) {
    ref var t = ref Ꮡt.DerefOrNull();
    ref var type_ = ref Ꮡtype_.DerefOrNull();

    if (Ꮡtype_ != nil && b.ch.isErrorType(Ꮡtype_)) {
        return true;
    }
    var typeFromPseudo = b.pseudoTypeToType(Ꮡt);
    if (typeFromPseudo == Ꮡtype_) {
        return true;
    }
    var undefinedStripped = Ꮡtype_;
    if (isOptionalAnnotated) {
        undefinedStripped = b.ch.getTypeWithFacts(Ꮡtype_, TypeFactsNEUndefined);
    }
    if (typeFromPseudo != nil && Ꮡtype_ != nil) {
        if (isOptionalAnnotated) {
            if (undefinedStripped == typeFromPseudo) {
                return true;
            }
            if ((TypeFlags)(typeFromPseudo.flags & TypeFlagsUnion) != 0 && (TypeFlags)(undefinedStripped.flags & TypeFlagsUnion) != 0) {
                if (b.ch.compareTypesIdentical(typeFromPseudo, undefinedStripped) == TernaryTrue) {
                    return true;
                }
            }
        }
        if (b.ch.getRegularTypeOfLiteralType(typeFromPseudo) == b.ch.getRegularTypeOfLiteralType(Ꮡtype_)) {
            return true;
        }
        if ((TypeFlags)(typeFromPseudo.flags & TypeFlagsUnion) != 0 && (TypeFlags)(type_.flags & TypeFlagsUnion) != 0) {
            if (b.ch.compareTypesIdentical(typeFromPseudo, Ꮡtype_) == TernaryTrue) {
                return true;
            }
        }
    }
    switch (t.Kind) {
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindInferred: {
        var errorNodes = t.AsPseudoTypeInferred().ErrorNodes; if (len(errorNodes) > 0) {
            if (reportErrors) {
                foreach (var (_, n) in range(errorNodes)) {
                    b.ctx.tracker.ReportInferenceFallback(n);
                }
            }
            return false;
        }
        if (reportErrors) {
            b.ctx.tracker.ReportInferenceFallback(t.AsPseudoTypeInferred().Expression);
        }
        return false;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindObjectLiteral: {
        var pt = t.AsPseudoTypeObjectLiteral();
        if (Ꮡtype_ == nil) {
            return false;
        }
        var targetProps = b.ch.getPropertiesOfType(undefinedStripped);
        nint targetDeclCount = 0;
        foreach (var (_, prop) in range(targetProps)) {
            targetDeclCount += len(prop.Declarations);
        }
        if (len(pt.Elements) != targetDeclCount) {
            return false;
        }
        foreach (var (_, e) in range(pt.Elements)) {
            ж<ast.Symbol> targetProp = default;
            var elemSymbol = e.Name.Parent.Symbol();
            if (elemSymbol != nil) {
                targetProp = b.ch.getPropertyOfType(undefinedStripped, elemSymbol.Name);
            }
            if (targetProp == nil) {
                foreach (var (_, prop) in range(targetProps)) {
                    if (prop.ValueDeclaration != nil && prop.ValueDeclaration.Name() == e.Name) {
                        targetProp = prop;
                        break;
                    }
                }
                if (targetProp == nil) {
                    if (reportErrors) {
                        b.ctx.tracker.ReportInferenceFallback(e.Name.Parent);
                    }
                    return false;
                }
            }
            var targetIsOptional = (ast.SymbolFlags)(targetProp.Flags & ast.SymbolFlagsOptional) != 0;
            if (e.Optional != targetIsOptional) {
                if (reportErrors) {
                    b.ctx.tracker.ReportInferenceFallback(e.Name.Parent);
                }
                return false;
            }
            var propType = b.ch.getTypeOfSymbol(targetProp);
            propType = b.ch.removeMissingType(propType, targetIsOptional);
            switch (e.Kind) {
            case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindPropertyAssignment: {
                var d = e.AsPseudoPropertyAssignment();
                if (!b.pseudoTypeEquivalentToType(d.Type, propType, e.Optional, false)) {
                    if (reportErrors) {
                        if (d.Type.Kind == pseudochecker.PseudoTypeKindInferred && len(d.Type.AsPseudoTypeInferred().ErrorNodes) > 0) {
                            foreach (var (_, n) in range(d.Type.AsPseudoTypeInferred().ErrorNodes)) {
                                b.ctx.tracker.ReportInferenceFallback(n);
                            }
                        } else if (!isStructuralPseudoType(d.Type)) {
                            b.ctx.tracker.ReportInferenceFallback(e.Name.Parent);
                        }
                    }
                    return false;
                }
                break;
            }
            case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindMethod: {
                var d = e.AsPseudoObjectMethod();
                var targetSig = b.ch.getSingleCallSignature(propType);
                if (targetSig == nil) {
                    continue;
                }
                var paramEq = b.pseudoParametersEquivalentToParameters(d.Parameters, targetSig, reportErrors, e.Name.Parent);
                if (!paramEq) {
                    return false;
                }
                var targetPredicate = b.ch.getTypePredicateOfSignature(targetSig);
                if (targetPredicate != nil) {
                    if (!b.pseudoReturnTypeMatchesPredicate(d.ReturnType, targetPredicate)) {
                        if (reportErrors) {
                            b.ctx.tracker.ReportInferenceFallback(e.Name.Parent);
                        }
                        return false;
                    }
                } else if (!b.pseudoTypeEquivalentToType(d.ReturnType, b.ch.getReturnTypeOfSignature(targetSig), false, false)) {
                    if (reportErrors) {
                        b.ctx.tracker.ReportInferenceFallback(e.Name.Parent);
                    }
                    return false;
                }
                break;
            }
            case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindGetAccessor: {
                var d = e.AsPseudoGetAccessor();
                if (!b.pseudoTypeEquivalentToType(d.Type, propType, false, false)) {
                    if (reportErrors) {
                        b.ctx.tracker.ReportInferenceFallback(e.Name.Parent);
                    }
                    return false;
                }
                break;
            }
            case var ᴛ2 when ᴛ2 == pseudochecker.PseudoObjectElementKindSetAccessor: {
                var d = e.AsPseudoSetAccessor();
                var writeType = b.ch.getWriteTypeOfSymbol(targetProp);
                if (!b.pseudoTypeEquivalentToType(d.Parameter.Type, writeType, false, false)) {
                    if (reportErrors) {
                        b.ctx.tracker.ReportInferenceFallback(e.Name.Parent);
                    }
                    return false;
                }
                break;
            }}

        }
        return true;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindTuple: {
        var pt = t.AsPseudoTypeTuple();
        if (undefinedStripped == nil || !isTupleType(undefinedStripped)) {
            return false;
        }
        var tupleTarget = undefinedStripped.TargetTupleType();
        if ((ElementFlags)(tupleTarget.combinedFlags & ElementFlagsNonRequired) != 0) {
            return false;
        }
        var elementTypes = b.ch.getTypeArguments(undefinedStripped);
        if (len(pt.Elements) != len(elementTypes)) {
            return false;
        }
        foreach (var (i, elem) in range(pt.Elements)) {
            if (!b.pseudoTypeEquivalentToType(elem, elementTypes[i], false, reportErrors)) {
                return false;
            }
        }
        return true;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindSingleCallSignature: {
        var targetSig = b.ch.getSingleCallSignature(undefinedStripped);
        if (targetSig == nil) {
            return false;
        }
        var pt = t.AsPseudoTypeSingleCallSignature();
        if (len(targetSig.typeParameters) != len(pt.TypeParameters)) {
            if (reportErrors) {
                b.ctx.tracker.ReportInferenceFallback(pt.Signature);
            }
            return false;
        }
        var paramEq = b.pseudoParametersEquivalentToParameters(pt.Parameters, targetSig, reportErrors, pt.Signature);
        if (!paramEq) {
            return false;
        }
        var targetPredicate = b.ch.getTypePredicateOfSignature(targetSig);
        if (targetPredicate != nil) {
            if (!b.pseudoReturnTypeMatchesPredicate(pt.ReturnType, targetPredicate)) {
                if (reportErrors) {
                    b.ctx.tracker.ReportInferenceFallback(pt.Signature);
                }
                return false;
            }
        } else if (!b.pseudoTypeEquivalentToType(pt.ReturnType, b.ch.getReturnTypeOfSignature(targetSig), false, reportErrors)) {
            return false;
        }
        return true;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindNoResult: {
        if (reportErrors) {
            b.ctx.tracker.ReportInferenceFallback(t.AsPseudoTypeNoResult().Declaration);
        }
        return false;
    }
    default: {
        return false;
    }}

}

internal static bool pseudoParametersEquivalentToParameters(this ref NodeBuilderImpl b, slice<ж<pseudochecker.PseudoParameter>> @params, ж<Signature> ᏑtargetSig, bool reportErrors, ж<ast.Node> nonParamErrorLocation) {
    ref var targetSig = ref ᏑtargetSig.DerefOrNull();

    if (targetSig.thisParameter != nil && len(@params) == 0) {
        if (reportErrors) {
            b.ctx.tracker.ReportInferenceFallback(nonParamErrorLocation);
        }
        return false;
    } else if (targetSig.thisParameter != nil && ast.IsThisIdentifier(@params[0].Name)) {
        var targetParam = targetSig.thisParameter;
        var paramType = b.ch.getTypeOfParameter(targetParam);
        if (!b.pseudoTypeEquivalentToType(@params[0].Type, paramType, @params[0].Optional, false)) {
            if (reportErrors) {
                b.ctx.tracker.ReportInferenceFallback(@params[0].Name.Parent);
            }
            return false;
        }
        @params = @params[1..];
    } else if (targetSig.thisParameter != nil) {
        if (reportErrors) {
            b.ctx.tracker.ReportInferenceFallback(nonParamErrorLocation);
        }
        return false;
    }
    if (len(targetSig.parameters) != len(@params)) {
        if (reportErrors) {
            b.ctx.tracker.ReportInferenceFallback(nonParamErrorLocation);
        }
        return false;
    }
    foreach (var (i, p) in range(@params)) {
        var targetParam = targetSig.parameters[i];
        if (p.Optional != b.ch.isOptionalParameter(targetParam.ValueDeclaration)) {
            if (reportErrors) {
                b.ctx.tracker.ReportInferenceFallback(p.Name.Parent);
            }
            return false;
        }
        var paramType = b.ch.getTypeOfParameter(targetParam);
        if (!b.pseudoTypeEquivalentToType(p.Type, paramType, p.Optional, false)) {
            if (reportErrors) {
                b.ctx.tracker.ReportInferenceFallback(p.Name.Parent);
            }
            return false;
        }
    }
    return true;
}

} partial class checker_package {

internal static bool isStructuralPseudoType(ж<pseudochecker.PseudoType> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    switch (t.Kind) {
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindObjectLiteral || ᴛ == pseudochecker.PseudoTypeKindTuple || ᴛ == pseudochecker.PseudoTypeKindSingleCallSignature: {
        return true;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindMaybeConstLocation: {
        var d = t.AsPseudoTypeMaybeConstLocation();
        return isStructuralPseudoType(d.ConstType) || isStructuralPseudoType(d.RegularType);
    }}

    return false;
}

} partial class checkerᴛNodeBuilderImpl {

internal static bool pseudoReturnTypeMatchesPredicate(this ref NodeBuilderImpl b, ж<pseudochecker.PseudoType> Ꮡrt, ж<TypePredicate> Ꮡpredicate) {
    ref var rt = ref Ꮡrt.DerefOrNull();
    ref var predicate = ref Ꮡpredicate.DerefOrNull();

    if (rt.Kind != pseudochecker.PseudoTypeKindDirect) {
        return false;
    }
    var node = rt.AsPseudoTypeDirect().TypeNode;
    if (!ast.IsTypePredicateNode(node)) {
        return false;
    }
    var tp = node.AsTypePredicateNode();
    var isAsserts = tp.AssertsModifier != nil;
    var predicateIsAsserts = predicate.kind == TypePredicateKindAssertsThis || predicate.kind == TypePredicateKindAssertsIdentifier;
    if (isAsserts != predicateIsAsserts) {
        return false;
    }
    var isThis = ast.IsThisTypeNode(tp.ParameterName);
    var predicateIsThis = predicate.kind == TypePredicateKindThis || predicate.kind == TypePredicateKindAssertsThis;
    if (isThis != predicateIsThis) {
        return false;
    }
    if (!isThis) {
        if (tp.ParameterName.Text() != predicate.parameterName) {
            return false;
        }
    }
    if (predicate.t != nil) {
        if ((~tp).Type == nil) {
            return false;
        }
        var predicateTypeFromNode = b.ch.getTypeFromTypeNode((~tp).Type);
        if (predicateTypeFromNode != predicate.t) {
            if (b.ch.compareTypesIdentical(predicateTypeFromNode, predicate.t) != TernaryTrue) {
                return false;
            }
        }
    } else if ((~tp).Type != nil) {
        return false;
    }
    return true;
}

internal static ж<Type> pseudoTypeToType(this ref NodeBuilderImpl b, ж<pseudochecker.PseudoType> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();
    var resᴷ = new stackAppend<ж<Type>>();

    debug.Assert(Ꮡt != nil, "Attempted to realize nil pseudotype");
    switch (t.Kind) {
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindDirect: {
        return b.ch.getTypeFromTypeNode(t.AsPseudoTypeDirect().TypeNode);
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindInferred: {
        var node = t.AsPseudoTypeInferred().Expression;
        if (t.AsPseudoTypeInferred().IsSignatureReturn) {
            return b.ch.getReturnTypeOfSignature(b.ch.getSignatureFromDeclaration(node));
        }
        var ty = b.ch.getWidenedType(b.ch.getRegularTypeOfExpression(node));
        return ty;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindNoResult: {
        return default;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindMaybeConstLocation: {
        var d = t.AsPseudoTypeMaybeConstLocation();
        if (b.ch.isConstContext(d.Node)) {
            return b.pseudoTypeToType(d.ConstType);
        }
        return b.pseudoTypeToType(d.RegularType);
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindUnion: {
        slice<ж<Type>> res = default;
        bool hasElidedType = default;
        var members = t.AsPseudoTypeUnion().Types;
        foreach (var (_, m) in range(members)) {
            if (!b.ch.strictNullChecks) {
                if (m.Kind == pseudochecker.PseudoTypeKindUndefined || m.Kind == pseudochecker.PseudoTypeKindNull) {
                    hasElidedType = true;
                    continue;
                }
            }
            var tΔ2 = b.pseudoTypeToType(m);
            if (tΔ2 == nil) {
                return default;
            }
            res = append(resᴷ, res, tΔ2);
        }
        if (len(res) == 1) {
            return res[0];
        }
        if (len(res) == 0) {
            if (hasElidedType) {
                return b.ch.anyType;
            }
            return b.ch.neverType;
        }
        return b.ch.getUnionType(res);
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindUndefined: {
        return b.ch.undefinedWideningType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindNull: {
        return b.ch.nullWideningType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindAny: {
        return b.ch.anyType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindString: {
        return b.ch.stringType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindNumber: {
        return b.ch.numberType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindBigInt: {
        return b.ch.bigintType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindBoolean: {
        return b.ch.booleanType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindFalse: {
        return b.ch.falseType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindTrue: {
        return b.ch.trueType;
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindStringLiteral || ᴛ == pseudochecker.PseudoTypeKindNumericLiteral || ᴛ == pseudochecker.PseudoTypeKindBigIntLiteral: {
        var source = t.AsPseudoTypeLiteral().Node;
        return b.ch.getRegularTypeOfExpression(source);
    }
    case var ᴛ when ᴛ == pseudochecker.PseudoTypeKindObjectLiteral || ᴛ == pseudochecker.PseudoTypeKindSingleCallSignature || ᴛ == pseudochecker.PseudoTypeKindTuple: {
        return default;
    }
    default: {
        debug.Fail("Unhandled pseudochecker.PseudoTypeKind in pseudoTypeToType");
        return default;
    }}

}

} // end checkerᴛNodeBuilderImpl
