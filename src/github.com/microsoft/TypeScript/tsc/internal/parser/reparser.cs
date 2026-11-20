namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strconv = strconv_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static parser_package;

partial class parser_package {

} partial class parserᴛParser {

internal static void finishReparsedNode(this ref Parser p, ж<ast.Node> Ꮡnode, ж<ast.Node> ᏑlocationNode) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var locationNode = ref ᏑlocationNode.DerefOrNull();

    node.Flags = (ast.NodeFlags)(p.contextFlags | ast.NodeFlagsReparsed);
    node.Loc = locationNode.Loc;
    p.overrideParentInImmediateChildren(Ꮡnode);
}

internal static void finishMutatedNode(this ref Parser p, ж<ast.Node> node) {
    p.overrideParentInImmediateChildren(node);
}

internal static ж<ast.Node> addDeepCloneReparse(this ж<Parser> Ꮡp, ж<ast.Node> node) {
    ref var p = ref Ꮡp.DerefOrNull();

    var clone = Ꮡp.Ꮡfactory.DeepCloneReparse(node);
    if (clone != nil) {
        p.reparsedClones = append(p.reparsedClones, clone);
    }
    return clone;
}

internal static ж<ast.Node> addTransformedReparse(this ref Parser p, ж<ast.Node> ᏑnewNode, ж<ast.Node> old) {
    ref var newNode = ref ᏑnewNode.DerefOrNull();

    p.finishReparsedNode(ᏑnewNode, old);
    newNode.Flags |= (ast.NodeFlags)(ast.NodeFlagsReparserTransformedLiteral);
    p.reparsedClones = append(p.reparsedClones, ᏑnewNode);
    return ᏑnewNode;
}

internal static ж<ast.Node> checkNonIdentifierName(this ref Parser p, ж<ast.Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (Ꮡname == nil) {
        return default;
    }
    if (ast.IsIdentifier(Ꮡname) && !scanner.IsValidIdentifier((~name.AsIdentifier()).Text)) {
        var errLoc = name.Loc;
        if (errLoc.Len() == 0) {
            errLoc = core.NewTextRange(name.Loc.Pos() - 1, name.Loc.Pos());
        }
        p.parseErrorAtRange(errLoc, diagnostics.Identifier_expected);
    }
    return Ꮡname;
}

internal static void reparseTags(this ж<Parser> Ꮡp, ж<ast.Node> parent, slice<ж<ast.Node>> jsDoc) {
    foreach (var (_, j) in range(jsDoc)) {
        var isLast = j == jsDoc[len(jsDoc) - 1];
        var tags = j.AsJSDoc().Tags;
        if (tags == nil) {
            continue;
        }
        foreach (var (_, tag) in range(tags.Nodes)) {
            Ꮡp.reparseUnhosted(tag, parent, j);
            if (isLast) {
                Ꮡp.reparseHosted(tag, parent, j);
            }
        }
    }
}

internal static void reparseUnhosted(this ж<Parser> Ꮡp, ж<ast.Node> Ꮡtag, ж<ast.Node> Ꮡparent, ж<ast.Node> jsDoc) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var tag = ref Ꮡtag.DerefOrNull();
    ref var parent = ref Ꮡparent.DerefOrNull();

    switch (tag.Kind) {
    case var ᴛ when ᴛ == ast.KindJSDocTypedefTag: {
        var typeExpression = tag.TypeExpression();
        if (typeExpression == nil) {
            break;
        }
        var fullName = tag.Name();
        var isNamespace = fullName != nil && ast.IsModuleDeclaration(fullName);
        ж<ast.ModifierList> modifiers = default;
        if (isNamespace) {
            modifiers = p.createExportModifier(Ꮡtag);
        }
        var typeAlias = p.factory.NewJSTypeAliasDeclaration(modifiers, Ꮡp.addDeepCloneReparse(p.checkNonIdentifierName(p.getInnermostNameOfJSDocNamespace(fullName))), nil, nil);
        typeAlias.AsTypeAliasDeclaration().Value.TypeParameters = Ꮡp.gatherTypeParameters(jsDoc, true);
        ж<ast.Node> t = default;
        switch (typeExpression.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindJSDocTypeExpression: {
            t = Ꮡp.addDeepCloneReparse(typeExpression.Type());
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindJSDocTypeLiteral: {
            t = Ꮡp.reparseJSDocTypeLiteral(typeExpression);
            break;
        }
        default: {
            throw panic("typedef tag type expression should be a name reference or a type expression" + typeExpression.Kind.String());
            break;
        }}

        typeAlias.AsTypeAliasDeclaration().Value.Type = t;
        p.finishReparsedNode(typeAlias, Ꮡtag);
        p.jsdocInfos = append(p.jsdocInfos, new JSDocInfo(parent: typeAlias, jsDocs: [jsDoc]));
        typeAlias.Flags |= (ast.NodeFlags)(ast.NodeFlagsHasJSDoc);
        var result = Ꮡp.wrapInJSDocNamespace(fullName, typeAlias, false);
        p.reparseList = append(p.reparseList, result);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocCallbackTag: {
        var typeExpression = tag.TypeExpression();
        if (typeExpression == nil) {
            break;
        }
        var fullName = tag.Name();
        var isNamespace = fullName != nil && ast.IsModuleDeclaration(fullName);
        ж<ast.ModifierList> modifiers = default;
        if (isNamespace) {
            modifiers = p.createExportModifier(Ꮡtag);
        }
        var functionType = Ꮡp.reparseJSDocSignature(typeExpression, Ꮡtag, jsDoc, Ꮡtag, nil);
        var typeAlias = p.factory.NewJSTypeAliasDeclaration(modifiers, Ꮡp.addDeepCloneReparse(p.getInnermostNameOfJSDocNamespace(fullName)), nil, functionType);
        typeAlias.AsTypeAliasDeclaration().Value.TypeParameters = Ꮡp.gatherTypeParameters(jsDoc, true);
        p.finishReparsedNode(typeAlias, Ꮡtag);
        p.jsdocInfos = append(p.jsdocInfos, new JSDocInfo(parent: typeAlias, jsDocs: [jsDoc]));
        typeAlias.Flags |= (ast.NodeFlags)(ast.NodeFlagsHasJSDoc);
        var result = Ꮡp.wrapInJSDocNamespace(fullName, typeAlias, false);
        p.reparseList = append(p.reparseList, result);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocImportTag: {
        var importTag = tag.AsJSDocImportTag();
        if ((~importTag).ImportClause == nil) {
            break;
        }
        var importClause = Ꮡp.addDeepCloneReparse((~importTag).ImportClause);
        importClause.AsImportClause().PhaseModifier = ast.KindTypeKeyword;
        var importDeclaration = p.factory.NewJSImportDeclaration(
            Ꮡp.Ꮡfactory.DeepCloneReparseModifiers(importTag.of(ast.JSDocImportTag.ᏑJSDocTagBase).of(ast.JSDocTagBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).Modifiers()),
            importClause,
            Ꮡp.addDeepCloneReparse((~importTag).ModuleSpecifier),
            Ꮡp.addDeepCloneReparse((~importTag).Attributes));
        p.finishReparsedNode(importDeclaration, Ꮡtag);
        p.reparseList = append(p.reparseList, importDeclaration);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocOverloadTag: {
        if ((ast.IsFunctionDeclaration(Ꮡparent) || ast.IsMethodDeclaration(Ꮡparent) || ast.IsConstructorDeclaration(Ꮡparent)) && (ParsingContexts)(p.parsingContexts & ((ParsingContexts)(nint)(1 << (int)(PCObjectLiteralMembers)))) == 0) {
            p.reparseList = append(p.reparseList, Ꮡp.reparseJSDocSignature((~tag.AsJSDocOverloadTag()).TypeExpression, Ꮡparent, jsDoc, Ꮡtag, parent.Modifiers()));
        }
        break;
    }}

}

internal static ж<ast.Node> reparseJSDocSignature(this ж<Parser> Ꮡp, ж<ast.Node> ᏑjsSignature, ж<ast.Node> Ꮡfun, ж<ast.Node> jsDoc, ж<ast.Node> Ꮡtag, ж<ast.ModifierList> modifiers) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var jsSignature = ref ᏑjsSignature.DerefOrNull();
    ref var fun = ref Ꮡfun.DerefOrNull();
    ref var tag = ref Ꮡtag.DerefOrNull();

    ж<ast.Node> signature = default;
    var clonedModifiers = Ꮡp.Ꮡfactory.DeepCloneReparseModifiers(modifiers);
    switch (fun.Kind) {
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        signature = p.factory.NewFunctionDeclaration(clonedModifiers, nil, Ꮡp.Ꮡfactory.DeepCloneReparse(p.checkNonIdentifierName(fun.Name())), nil, nil, nil, nil, nil);
        break;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        signature = p.factory.NewMethodDeclaration(clonedModifiers, nil, Ꮡp.Ꮡfactory.DeepCloneReparse(p.checkNonIdentifierName(fun.Name())), nil, nil, nil, nil, nil, nil);
        break;
    }
    case var ᴛ when ᴛ == ast.KindConstructor: {
        signature = p.factory.NewConstructorDeclaration(clonedModifiers, nil, nil, nil, nil, nil);
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocCallbackTag: {
        signature = p.factory.NewFunctionTypeNode(nil, nil, p.factory.NewKeywordTypeNode(ast.KindAnyKeyword));
        break;
    }
    default: {
        throw panic("Unexpected kind " + fun.Kind.String());
        break;
    }}

    if (tag.Kind != ast.KindJSDocCallbackTag) {
        signature.FunctionLikeData().TypeParameters = Ꮡp.gatherTypeParameters(jsDoc, false);
    }
    var parameters = p.nodeSliceArena.NewSlice(0);
    foreach (var (pi, param) in range(jsSignature.Parameters())) {
        ж<ast.Node> parameter = default;
        if (param.Kind == ast.KindJSDocThisTag) {
            var thisTag = param.AsJSDocThisTag();
            var thisIdent = p.factory.NewIdentifier("this");
            thisIdent.Loc = thisTag.Loc;
            thisIdent.Flags = (ast.NodeFlags)(p.contextFlags | ast.NodeFlagsReparsed);
            parameter = p.factory.NewParameterDeclaration(nil, nil, thisIdent, nil, nil, nil);
            if ((~thisTag).TypeExpression != nil) {
                parameter.AsParameterDeclaration().Value.Type = Ꮡp.addDeepCloneReparse((~thisTag).TypeExpression.Type());
            }
        } else if (param.Kind == ast.KindJSDocParameterTag || param.Kind == ast.KindJSDocPropertyTag) {
            var jsparam = param.AsJSDocParameterOrPropertyTag();
            if (ast.IsQualifiedName(jsparam.Name())) {
                continue;
            }
            ж<ast.Node> dotDotDotToken = default;
            ж<astꓸTypeNode> paramType = default;

            if ((~jsparam).TypeExpression != nil) {
                if ((~jsparam).TypeExpression.Type().Kind == ast.KindJSDocVariadicType) {
                    dotDotDotToken = p.factory.NewToken(ast.KindDotDotDotToken);
                    dotDotDotToken.Loc = jsparam.Loc;
                    dotDotDotToken.Flags = (ast.NodeFlags)(p.contextFlags | ast.NodeFlagsReparsed);

                    var variadicType = (~jsparam).TypeExpression.Type().AsJSDocVariadicType();
                    paramType = Ꮡp.reparseJSDocTypeLiteral((~variadicType).Type);
                } else {
                    paramType = Ꮡp.reparseJSDocTypeLiteral((~jsparam).TypeExpression.Type());
                }
            }
            var name = jsparam.Name();
            if (ast.IsIdentifier(name) && !scanner.IsValidIdentifier((~name.AsIdentifier()).Text)) {
                ref var result = ref heap<strings.Builder>(new strings.Builder(), out var Ꮡresult);
                foreach (var (i, ch) in (~name.AsIdentifier()).Text) {
                    if (i == 0) {
                        if (!scanner.IsIdentifierStart(ch)) {
                            Ꮡresult.WriteRune('_');
                        } else {
                            Ꮡresult.WriteRune(ch);
                        }
                        continue;
                    } else if (!scanner.IsIdentifierPart(ch)) {
                        Ꮡresult.WriteRune('_');
                    } else {
                        Ꮡresult.WriteRune(ch);
                    }
                }
                if (result.Len() == 0) {
                    Ꮡresult.WriteRune('_');
                    Ꮡresult.WriteString(strconv.Itoa(pi));
                }
                name = p.addTransformedReparse(p.factory.NewIdentifier(result.String()), name);
            } else {
                name = Ꮡp.addDeepCloneReparse(name);
            }
            parameter = p.factory.NewParameterDeclaration(nil, dotDotDotToken, name, p.makeQuestionIfOptional(jsparam), paramType, nil);
        }
        p.finishReparsedNode(parameter, param);
        parameters = append(parameters, parameter);
        Ꮡp.reparseJSDocComment(parameter, param);
    }
    signature.FunctionLikeData().Parameters = p.newNodeList((~jsSignature.AsJSDocSignature()).Parameters.Loc, parameters);

    if (jsSignature.Type() != nil && jsSignature.Type().TypeExpression() != nil) {
        signature.FunctionLikeData().Type = Ꮡp.addDeepCloneReparse(jsSignature.Type().TypeExpression().Type());
    }
    var loc = ᏑjsSignature;
    if (tag.Kind == ast.KindJSDocOverloadTag) {
        loc = tag.TagName();
    }
    p.finishReparsedNode(signature, loc);
    return signature;
}

internal static ж<ast.Node> reparseJSDocTypeLiteral(this ж<Parser> Ꮡp, ж<astꓸTypeNode> Ꮡt) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡt == nil) {
        return default;
    }
    if (t.Kind == ast.KindJSDocTypeLiteral) {
        var jstypeliteral = t.AsJSDocTypeLiteral();
        var isArrayType = jstypeliteral.IsArrayType;
        var properties = p.nodeSliceArena.NewSlice(0);
        foreach (var (_, prop) in range(jstypeliteral.JSDocPropertyTags)) {
            if (prop.Kind != ast.KindJSDocPropertyTag && prop.Kind != ast.KindJSDocParameterTag) {
                continue;
            }
            var jsprop = prop.AsJSDocParameterOrPropertyTag();
            var name = prop.Name();
            if (name.Kind == ast.KindQualifiedName) {
                name = name.AsQualifiedName().Right;
            }
            if (ast.IsIdentifier(name) && !scanner.IsValidIdentifier((~name.AsIdentifier()).Text)) {
                name = p.addTransformedReparse(p.factory.NewStringLiteral((~name.AsIdentifier()).Text, ast.TokenFlagsNone), name);
            } else {
                name = Ꮡp.addDeepCloneReparse(name);
            }
            var property = p.factory.NewPropertySignatureDeclaration(nil, name, p.makeQuestionIfOptional(jsprop), nil, nil);
            if ((~jsprop).TypeExpression != nil) {
                property.AsPropertySignatureDeclaration().Value.Type = Ꮡp.reparseJSDocTypeLiteral((~jsprop).TypeExpression.Type());
            }
            p.finishReparsedNode(property, prop);
            properties = append(properties, property);
            Ꮡp.reparseJSDocComment(property, prop);
        }
        Ꮡt = p.factory.NewTypeLiteralNode(p.newNodeList(jstypeliteral.Loc, properties)); t = ref Ꮡt.DerefOrNull();
        if (isArrayType) {
            p.finishReparsedNode(Ꮡt, jstypeliteral.of(ast.JSDocTypeLiteral.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            Ꮡt = p.factory.NewArrayTypeNode(Ꮡt); t = ref Ꮡt.DerefOrNull();
        }
        p.finishReparsedNode(Ꮡt, jstypeliteral.of(ast.JSDocTypeLiteral.ᏑJSDocTypeBase).of(ast.JSDocTypeBase.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return Ꮡt;
    }
    return Ꮡp.addDeepCloneReparse(Ꮡt);
}

internal static void reparseJSDocComment(this ж<Parser> Ꮡp, ж<ast.Node> Ꮡnode, ж<ast.Node> Ꮡtag) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var tag = ref Ꮡtag.DerefOrNull();

    {
        var comment = tag.CommentList(); if (comment != nil) {
            var newComment = p.factory.NewNodeList(core.Map<ж<ast.Node>, ж<ast.Node>>(comment.Nodes, Ꮡp.Ꮡfactory.DeepCloneReparse));
            newComment.Loc = comment.Loc;
            var propJSDoc = p.factory.NewJSDoc(newComment, nil);
            p.finishReparsedNode(propJSDoc, Ꮡtag);
            propJSDoc.Parent = Ꮡnode;
            p.jsdocInfos = append(p.jsdocInfos, new JSDocInfo(parent: Ꮡnode, jsDocs: [propJSDoc]));
            node.Flags |= (ast.NodeFlags)(ast.NodeFlagsHasJSDoc);
        }
    }
}

internal static ж<ast.NodeList> gatherTypeParameters(this ж<Parser> Ꮡp, ж<ast.Node> Ꮡj, bool typedefOrCallback) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var j = ref Ꮡj.DerefOrNull();

    slice<ж<ast.Node>> typeParameters = default;
    nint pos = -1;
    nint endPos = -1;
    var firstTemplate = true;
    foreach (var (_, tag) in range(j.AsJSDoc().Tags.Nodes)) {
        if (!typedefOrCallback && (ast.IsJSDocTypedefTag(tag) || ast.IsJSDocCallbackTag(tag))) {
            return default;
        }
        if (!ast.IsJSDocTemplateTag(tag)) {
            continue;
        }
        if (firstTemplate) {
            pos = tag.Pos();
            firstTemplate = false;
        }
        endPos = tag.End();
        var constraint = tag.AsJSDocTemplateTag().Constraint;
        var firstTypeParameter = true;
        foreach (var (_, tp) in range(tag.TypeParameters())) {
            ж<ast.Node> reparse = default;
            if (constraint != nil && firstTypeParameter) {
                reparse = p.factory.NewTypeParameterDeclaration(
                    Ꮡp.Ꮡfactory.DeepCloneReparseModifiers(tp.Modifiers()),
                    Ꮡp.addDeepCloneReparse(p.checkNonIdentifierName(tp.Name())),
                    Ꮡp.addDeepCloneReparse(constraint.Type()),
                    nil,
                    Ꮡp.addDeepCloneReparse(tp.AsTypeParameterDeclaration().DefaultType));
                p.finishReparsedNode(reparse, tp);
            } else {
                reparse = Ꮡp.addDeepCloneReparse(tp);
            }
            if (typeParameters == nil) {
                typeParameters = p.nodeSliceArena.NewSlice(0);
            }
            typeParameters = append(typeParameters, reparse);
            firstTypeParameter = false;
        }
    }
    if (len(typeParameters) == 0) {
        return default;
    } else {
        return p.newNodeList(core.NewTextRange(pos, endPos), typeParameters);
    }
}

internal static void reparseHosted(this ж<Parser> Ꮡp, ж<ast.Node> Ꮡtag, ж<ast.Node> Ꮡparent, ж<ast.Node> jsDoc) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var tag = ref Ꮡtag.DerefOrNull();
    ref var parent = ref Ꮡparent.DerefOrNull();

    switch (tag.Kind) {
    case var ᴛ when ᴛ == ast.KindJSDocTypeTag: {
        switch (parent.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindVariableStatement: {
            if (parent.AsVariableStatement().DeclarationList != nil) {
                foreach (var (_, declaration) in range(parent.AsVariableStatement().DeclarationList.AsVariableDeclarationList().Declarations.Nodes)) {
                    if (declaration.Type() == nil && tag.TypeExpression() != nil) {
                        declaration.AsMutable().SetType(Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()));
                        p.finishMutatedNode(declaration);
                        return;
                    }
                }
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindVariableDeclaration || ᴛ2 == ast.KindExportAssignment || ᴛ2 == ast.KindPropertyDeclaration || ᴛ2 == ast.KindPropertyAssignment || ᴛ2 == ast.KindShorthandPropertyAssignment || ᴛ2 == ast.KindGetAccessor: {
            if (parent.Type() == nil && tag.TypeExpression() != nil) {
                Ꮡparent.AsMutable().SetType(Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()));
                p.finishMutatedNode(Ꮡparent);
                return;
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindParameter: {
            if (parent.Type() == nil && tag.TypeExpression() != nil) {
                Ꮡparent.AsMutable().SetType(Ꮡp.reparseJSDocTypeLiteral(tag.TypeExpression().Type()));
                p.finishMutatedNode(Ꮡparent);
                return;
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindExpressionStatement: {
            if (parent.Expression().Kind == ast.KindBinaryExpression) {
                var bin = parent.Expression().AsBinaryExpression();
                ast.JSDeclarationKind kind = ast.GetAssignmentDeclarationKind(bin.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); if (kind != ast.JSDeclarationKindNone && tag.TypeExpression() != nil) {
                    bin.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).AsMutable().SetType(Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()));
                    p.finishMutatedNode(bin.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                    return;
                }
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindReturnStatement || ᴛ2 == ast.KindParenthesizedExpression: {
            if (parent.Expression() != nil && tag.TypeExpression() != nil) {
                Ꮡparent.AsMutable().SetExpression(p.makeNewCast(
                    Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()),
                    parent.Expression(),
                    true));
                p.finishMutatedNode(Ꮡparent);
                return;
            }
            break;
        }}

        {
            var fun = getFunctionLikeHost(Ꮡparent); if (fun != nil) {
                var noTypedParams = core.Every(fun.Parameters(), (ж<ast.Node> param) => param.Type() == nil);
                if (fun.TypeParameterList() == nil && fun.Type() == nil && noTypedParams && tag.TypeExpression() != nil) {
                    fun.FunctionLikeData().FullSignature = Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type());
                    p.finishMutatedNode(fun);
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocSatisfiesTag: {
        switch (parent.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindVariableStatement: {
            if (parent.AsVariableStatement().DeclarationList != nil) {
                foreach (var (_, declaration) in range(parent.AsVariableStatement().DeclarationList.AsVariableDeclarationList().Declarations.Nodes)) {
                    if (declaration.Initializer() != nil && tag.TypeExpression() != nil) {
                        declaration.AsMutable().SetInitializer(p.makeNewCast(
                            Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()),
                            declaration.Initializer(),
                            false));
                        p.finishMutatedNode(declaration);
                        break;
                    }
                }
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindVariableDeclaration || ᴛ2 == ast.KindPropertyDeclaration || ᴛ2 == ast.KindPropertyAssignment: {
            if (parent.Initializer() != nil && tag.TypeExpression() != nil) {
                Ꮡparent.AsMutable().SetInitializer(p.makeNewCast(
                    Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()),
                    parent.Initializer(),
                    false));
                p.finishMutatedNode(Ꮡparent);
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindShorthandPropertyAssignment: {
            var shorthand = parent.AsShorthandPropertyAssignment();
            if (shorthand.ObjectAssignmentInitializer != nil && (~tag.AsJSDocSatisfiesTag()).TypeExpression != nil) {
                shorthand.ObjectAssignmentInitializer = p.makeNewCast(
                    Ꮡp.addDeepCloneReparse((~tag.AsJSDocSatisfiesTag()).TypeExpression.Type()),
                    shorthand.ObjectAssignmentInitializer,
                    false);
                p.finishMutatedNode(Ꮡparent);
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindReturnStatement || ᴛ2 == ast.KindParenthesizedExpression || ᴛ2 == ast.KindExportAssignment: {
            if (parent.Expression() != nil && tag.TypeExpression() != nil) {
                Ꮡparent.AsMutable().SetExpression(p.makeNewCast(
                    Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()),
                    parent.Expression(),
                    false));
                p.finishMutatedNode(Ꮡparent);
            }
            break;
        }
        case var ᴛ2 when ᴛ2 == ast.KindExpressionStatement: {
            if (parent.Expression().Kind == ast.KindBinaryExpression) {
                var bin = parent.Expression().AsBinaryExpression();
                ast.JSDeclarationKind kind = ast.GetAssignmentDeclarationKind(bin.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); if (kind != ast.JSDeclarationKindNone && tag.TypeExpression() != nil) {
                    bin.Right = p.makeNewCast(
                        Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type()),
                        bin.Right,
                        false);
                    p.finishMutatedNode(bin.of(ast.BinaryExpression.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                }
            }
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocTemplateTag: {
        {
            var fun = getFunctionLikeHost(Ꮡparent); if (fun != nil) {
                if (fun.TypeParameters() == nil && fun.FunctionLikeData().FullSignature == nil) {
                    fun.FunctionLikeData().TypeParameters = Ꮡp.gatherTypeParameters(jsDoc, false);
                    p.finishMutatedNode(fun);
                }
            } else if (parent.Kind == ast.KindClassDeclaration) {
                var @class = parent.AsClassDeclaration();
                if ((~@class).TypeParameters == nil) {
                    @class.Value.TypeParameters = Ꮡp.gatherTypeParameters(jsDoc, false);
                    p.finishMutatedNode(Ꮡparent);
                }
            } else if (parent.Kind == ast.KindClassExpression) {
                var @class = parent.AsClassExpression();
                if ((~@class).TypeParameters == nil) {
                    @class.Value.TypeParameters = Ꮡp.gatherTypeParameters(jsDoc, false);
                    p.finishMutatedNode(Ꮡparent);
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocParameterTag: {
        {
            var fun = getFunctionLikeHost(Ꮡparent); if (fun != nil && fun.FunctionLikeData().FullSignature == nil) {
                var parameterTag = tag.AsJSDocParameterOrPropertyTag();
                var (param, ok) = findMatchingParameter(fun, parameterTag, jsDoc); if (ok) {
                    if ((~param).Type == nil && (~parameterTag).TypeExpression != nil) {
                        param.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).AsParameterDeclaration().Value.Type = Ꮡp.reparseJSDocTypeLiteral((~parameterTag).TypeExpression.Type());
                    }
                    if ((~param).QuestionToken == nil) {
                        var question = p.makeQuestionIfOptional(parameterTag); if (question != nil) {
                            param.Value.QuestionToken = question;
                        }
                    }
                    p.finishMutatedNode(param.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocThisTag: {
        {
            var fun = getFunctionLikeHost(Ꮡparent); if (fun != nil) {
                var @params = fun.Parameters();
                if (len(@params) == 0 || (@params[0].Name().Kind != ast.KindThisKeyword && !ast.IsThisIdentifier(@params[0].Name()))) {
                    var thisParam = p.factory.NewParameterDeclaration(
                        nil,
                        nil,
                        p.factory.NewIdentifier("this"),
                        nil,
                        nil,
                        nil);
                    if ((~tag.AsJSDocThisTag()).TypeExpression != nil) {
                        thisParam.AsParameterDeclaration().Value.Type = Ꮡp.addDeepCloneReparse((~tag.AsJSDocThisTag()).TypeExpression.Type());
                    }
                    p.finishReparsedNode(thisParam, tag.TagName());

                    var newParams = p.nodeSliceArena.NewSlice(len(@params) + 1);
                    newParams[0] = thisParam;
                    foreach (var (i, param) in range(@params)) {
                        newParams[i + 1] = param;
                    }

                    fun.FunctionLikeData().Parameters = p.newNodeList(fun.ParameterList().Loc, newParams);
                    p.finishMutatedNode(fun);
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocReturnTag: {
        {
            var fun = getFunctionLikeHost(Ꮡparent); if (fun != nil && fun.FunctionLikeData().FullSignature == nil) {
                if (fun.Type() == nil && tag.TypeExpression() != nil) {
                    fun.FunctionLikeData().Type = Ꮡp.addDeepCloneReparse(tag.TypeExpression().Type());
                    p.finishMutatedNode(fun);
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocReadonlyTag || ᴛ == ast.KindJSDocPrivateTag || ᴛ == ast.KindJSDocPublicTag || ᴛ == ast.KindJSDocProtectedTag || ᴛ == ast.KindJSDocOverrideTag: {
        if (parent.Kind == ast.KindExpressionStatement) {
            Ꮡparent = parent.Expression(); parent = ref Ꮡparent.DerefOrNull();
        }
        var exprᴛ4 = parent.Kind;
        var matchᴛ1 = false;
        if (exprᴛ4 == ast.KindMethodDeclaration || exprᴛ4 == ast.KindGetAccessor || exprᴛ4 == ast.KindSetAccessor) {
            if ((ParsingContexts)(p.parsingContexts & ((ParsingContexts)(nint)(1 << (int)(PCObjectLiteralMembers)))) != 0) {
                return;
            }
            fallthrough = true;
        }
        if (fallthrough || !matchᴛ1 && (exprᴛ4 == ast.KindPropertyDeclaration || exprᴛ4 == ast.KindConstructor || exprᴛ4 == ast.KindBinaryExpression)) { matchᴛ1 = true;
            ast.Kind keyword = default;
            switch (tag.Kind) {
            case var ᴛ2 when ᴛ2 == ast.KindJSDocReadonlyTag: {
                keyword = ast.KindReadonlyKeyword;
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindJSDocPrivateTag: {
                keyword = ast.KindPrivateKeyword;
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindJSDocPublicTag: {
                keyword = ast.KindPublicKeyword;
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindJSDocProtectedTag: {
                keyword = ast.KindProtectedKeyword;
                break;
            }
            case var ᴛ2 when ᴛ2 == ast.KindJSDocOverrideTag: {
                keyword = ast.KindOverrideKeyword;
                break;
            }}

            var modifier = p.factory.NewModifier(keyword);
            modifier.Loc = tag.Loc;
            modifier.Flags = (ast.NodeFlags)(p.contextFlags | ast.NodeFlagsReparsed);
            slice<ж<ast.Node>> nodes = default;
            core.TextRange loc = default;
            if (parent.Modifiers() == nil) {
                nodes = p.nodeSliceArena.NewSlice(1);
                nodes[0] = modifier;
                loc = tag.Loc;
            } else {
                nodes = append(parent.ModifierNodes(), modifier);
                loc = parent.Modifiers().Loc;
            }
            Ꮡparent.AsMutable().SetModifiers(p.newModifierList(loc, nodes));
            p.finishMutatedNode(Ꮡparent);
        }

        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocImplementsTag: {
        {
            var @class = getClassLikeData(Ꮡparent); if (@class != nil) {
                var implementsTag = tag.AsJSDocImplementsTag();

                if (@class.HeritageClauses != nil) {
                    {
                        var implementsClause = core.Find(@class.HeritageClauses.Nodes, (ж<ast.Node> node) => node.AsHeritageClause().Token == ast.KindImplementsKeyword); if (implementsClause != nil) {
                            implementsClause.AsHeritageClause().Types.Nodes = append(implementsClause.AsHeritageClause().Types.Nodes, Ꮡp.addDeepCloneReparse((~implementsTag).ClassName));
                            p.finishMutatedNode(implementsClause);
                            return;
                        }
                    }
                }
                var typesList = p.newNodeList((~implementsTag).ClassName.Loc, p.nodeSliceArena.NewSlice1(Ꮡp.addDeepCloneReparse((~implementsTag).ClassName)));

                var heritageClause = p.factory.NewHeritageClause(ast.KindImplementsKeyword, typesList);
                p.finishReparsedNode(heritageClause, (~implementsTag).ClassName);

                if (@class.HeritageClauses == nil) {
                    var heritageClauses = p.newNodeList((~implementsTag).ClassName.Loc, p.nodeSliceArena.NewSlice1(heritageClause));
                    @class.HeritageClauses = heritageClauses;
                } else {
                    @class.HeritageClauses.Nodes = append(@class.HeritageClauses.Nodes, heritageClause);
                }
                p.finishMutatedNode(Ꮡparent);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindJSDocAugmentsTag: {
        {
            var @class = getClassLikeData(Ꮡparent); if (@class != nil && @class.HeritageClauses != nil) {
                {
                    var extendsClause = core.Find(@class.HeritageClauses.Nodes, (ж<ast.Node> node) => node.AsHeritageClause().Token == ast.KindExtendsKeyword); if (extendsClause != nil && len(extendsClause.AsHeritageClause().Types.Nodes) == 1) {
                        var target = extendsClause.AsHeritageClause().Types.Nodes[0].AsExpressionWithTypeArguments();
                        var source = tag.ClassName().AsExpressionWithTypeArguments();
                        if (ast.HasSamePropertyAccessName((~target).Expression, (~source).Expression)) {
                            if ((~target).TypeArguments == nil && (~source).TypeArguments != nil) {
                                var newArguments = p.nodeSliceArena.NewSlice(len((~source).TypeArguments.Nodes));
                                foreach (var (i, arg) in range((~source).TypeArguments.Nodes)) {
                                    newArguments[i] = Ꮡp.addDeepCloneReparse(arg);
                                }
                                target.Value.TypeArguments = p.newNodeList((~source).TypeArguments.Loc, newArguments);
                                p.finishMutatedNode(target.of(ast.ExpressionWithTypeArguments.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                            }
                        }
                    }
                }
            }
        }
        break;
    }}

}

internal static ж<ast.Node> makeQuestionIfOptional(this ref Parser p, ж<ast.JSDocParameterOrPropertyTag> Ꮡparameter) {
    ref var parameter = ref Ꮡparameter.DerefOrNull();

    ж<ast.Node> questionToken = default;
    if (parameter.IsBracketed || parameter.TypeExpression != nil && parameter.TypeExpression.Type().Kind == ast.KindJSDocOptionalType) {
        questionToken = p.factory.NewToken(ast.KindQuestionToken);
        questionToken.Loc = parameter.Loc;
        questionToken.Flags = (ast.NodeFlags)(p.contextFlags | ast.NodeFlagsReparsed);
    }
    return questionToken;
}

} partial class parser_package {

internal static (ж<ast.ParameterDeclaration>, bool) findMatchingParameter(ж<ast.Node> Ꮡfun, ж<ast.JSDocParameterOrPropertyTag> ᏑparameterTag, ж<ast.Node> ᏑjsDoc) {
    ref var fun = ref Ꮡfun.DerefOrNull();
    ref var parameterTag = ref ᏑparameterTag.DerefOrNull();
    ref var jsDoc = ref ᏑjsDoc.DerefOrNull();

    nint tagIndex = -1;
    nint paramCount = -1;
    foreach (var (_, tag) in range(jsDoc.AsJSDoc().Tags.Nodes)) {
        if (tag.Kind == ast.KindJSDocParameterTag) {
            paramCount++;
            if (tag.AsJSDocParameterOrPropertyTag() == ᏑparameterTag) {
                tagIndex = paramCount;
                break;
            }
        }
    }
    foreach (var (parameterIndex, parameter) in range(fun.Parameters())) {
        if (parameter.Name().Kind == ast.KindIdentifier) {
            if (parameterTag.Name().Kind == ast.KindIdentifier && ((parameter.Name().Text() == parameterTag.Name().Text()) || (parameterIndex == tagIndex && len(parameterTag.Name().Text()) == 0))) {
                return (parameter.AsParameterDeclaration(), true);
            }
        } else if (parameterIndex == tagIndex) {
            return (parameter.AsParameterDeclaration(), true);
        }
    }
    return (default, false);
}

internal static ж<ast.Node> skipSatisfiesExpressions(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil && node.Kind == ast.KindSatisfiesExpression) {
        Ꮡnode = node.Expression(); node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

internal static ж<ast.Node> getFunctionLikeHost(ж<ast.Node> Ꮡhost) {
    ref var host = ref Ꮡhost.DerefOrNull();

    var fun = Ꮡhost;
    switch (host.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableStatement: {
        var nodes = host.AsVariableStatement().DeclarationList.AsVariableDeclarationList().Declarations.Nodes; if (len(nodes) != 0) {
            fun = nodes[0].Initializer();
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindPropertyDeclaration: {
        fun = host.Initializer();
        break;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment || ᴛ == ast.KindReturnStatement: {
        fun = host.Expression();
        break;
    }
    case var ᴛ when ᴛ == ast.KindExpressionStatement: {
        fun = ast.GetRightMostAssignedExpression(host.Expression());
        break;
    }}

    fun = skipSatisfiesExpressions(fun);
    if (ast.IsFunctionLike(fun)) {
        return fun;
    }
    return default;
}

} partial class parserᴛParser {

internal static ж<ast.Node> makeNewCast(this ref Parser p, ж<astꓸTypeNode> t, ж<ast.Node> Ꮡe, bool isAssertion) {
    ref var e = ref Ꮡe.DerefOrNull();

    ж<ast.Node> assert = default;
    if (isAssertion) {
        assert = p.factory.NewAsExpression(Ꮡe, t);
    } else {
        assert = p.factory.NewSatisfiesExpression(Ꮡe, t);
    }
    p.finishNodeWithEnd(assert, e.Pos(), e.End());
    return assert;
}

} partial class parser_package {

internal static ж<ast.ClassLikeBase> getClassLikeData(ж<ast.Node> Ꮡparent) {
    ref var parent = ref Ꮡparent.DerefOrNull();

    ж<ast.ClassLikeBase> @class = default;
    switch (parent.Kind) {
    case var ᴛ when ᴛ == ast.KindClassDeclaration: {
        @class = parent.AsClassDeclaration().of(ast.ClassDeclaration.ᏑClassLikeBase).ClassLikeData();
        break;
    }
    case var ᴛ when ᴛ == ast.KindClassExpression: {
        @class = parent.AsClassExpression().of(ast.ClassExpression.ᏑClassLikeBase).ClassLikeData();
        break;
    }}

    return @class;
}

} partial class parserᴛParser {

internal static ж<ast.ModifierList> createExportModifier(this ref Parser p, ж<ast.Node> ᏑlocationNode) {
    ref var locationNode = ref ᏑlocationNode.DerefOrNull();

    var exportModifier = p.factory.NewModifier(ast.KindExportKeyword);
    exportModifier.Loc = locationNode.Loc;
    exportModifier.Flags = (ast.NodeFlags)(p.contextFlags | ast.NodeFlagsReparsed);
    var nodes = p.nodeSliceArena.NewSlice1(exportModifier);
    return p.newModifierList(locationNode.Loc, nodes);
}

internal static ж<ast.Node> getInnermostNameOfJSDocNamespace(this ref Parser p, ж<ast.Node> ᏑfullName) {
    ref var fullName = ref ᏑfullName.DerefOrNull();

    if (ᏑfullName == nil) {
        return default;
    }
    while (fullName.Kind == ast.KindModuleDeclaration) {
        var body = fullName.AsModuleDeclaration().Value.Body;
        if (body == nil) {
            return fullName.Name();
        }
        ᏑfullName = body; fullName = ref ᏑfullName.DerefOrNull();
    }
    return ᏑfullName;
}

internal static ж<ast.Node> wrapInJSDocNamespace(this ж<Parser> Ꮡp, ж<ast.Node> ᏑfullName, ж<ast.Node> statement, bool nested) {
    ref var p = ref Ꮡp.DerefOrNull();
    ref var fullName = ref ᏑfullName.DerefOrNull();

    if (ᏑfullName == nil || !ast.IsModuleDeclaration(ᏑfullName)) {
        return statement;
    }
    var wrapped = Ꮡp.wrapInJSDocNamespace(fullName.Body(), statement, true);
    var block = p.factory.NewModuleBlock(p.newNodeList(fullName.Loc, p.nodeSliceArena.NewSlice1(wrapped)));
    p.finishReparsedNode(block, ᏑfullName);
    ж<ast.ModifierList> modifiers = default;
    if (nested) {
        modifiers = p.createExportModifier(ᏑfullName);
    }
    var result = p.factory.NewModuleDeclaration(modifiers, ast.KindNamespaceKeyword, Ꮡp.addDeepCloneReparse(fullName.Name()), nil, block);
    p.finishReparsedNode(result, ᏑfullName);
    p.reparsedClones = append(p.reparsedClones, result);
    return result;
}

} // end parserᴛParser
