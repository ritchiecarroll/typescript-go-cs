namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using iter = iter_package;
using math = math_package;
using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using parser = go.github.com.microsoft.TypeScript.tsc.@internal.parser_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using static checker_package;

partial class checker_package {

partial struct JsxFlags /*num:uint32*/;

public static JsxFlags JsxFlagsNone => 0;
public static JsxFlags JsxFlagsIntrinsicNamedElement => /* 1 << 0 */ 1;
public static JsxFlags JsxFlagsIntrinsicIndexedElement => /* 1 << 1 */ 2;
public static JsxFlags JsxFlagsIntrinsicElement => /* JsxFlagsIntrinsicNamedElement | JsxFlagsIntrinsicIndexedElement */ 3;

partial struct JsxReferenceKind /*num:int32*/;

public static JsxReferenceKind JsxReferenceKindComponent => /* iota */ 0;
public static JsxReferenceKind JsxReferenceKindFunction => 1;
public static JsxReferenceKind JsxReferenceKindMixed => 2;

partial struct JsxElementLinks {
    internal JsxFlags jsxFlags;
    internal ж<Type> resolvedJsxElementAttributesType;
    internal ж<ast.Symbol> jsxNamespace;
    internal ж<ast.Symbol> jsxImplicitImportContainer;
    internal ж<ast.Node> firstJSXTagInFile;
}


partial struct JsxNamesᴛ1 /*dyn*/ {
    public @string JSX;
    public @string IntrinsicElements;
    public @string ElementClass;
    public @string ElementAttributesPropertyNameContainer;
    public @string ElementChildrenAttributeNameContainer;
    public @string Element;
    public @string ElementType;
    public @string IntrinsicAttributes;
    public @string IntrinsicClassAttributes;
    public @string LibraryManagedAttributes;
}
[GoLazy] partial class lazyᴛJsxNames { public static readonly ж<JsxNamesᴛ1> ᏑJsxNames = new StaticBox<JsxNamesᴛ1>(new JsxNamesᴛ1(
    JSX: "JSX",
    IntrinsicElements: "IntrinsicElements",
    ElementClass: "ElementClass",
    ElementAttributesPropertyNameContainer: "ElementAttributesProperty",
    ElementChildrenAttributeNameContainer: "ElementChildrenAttribute",
    Element: "Element",
    ElementType: "ElementType",
    IntrinsicAttributes: "IntrinsicAttributes",
    IntrinsicClassAttributes: "IntrinsicClassAttributes",
    LibraryManagedAttributes: "LibraryManagedAttributes"
)); }
public static ref JsxNamesᴛ1 JsxNames => ref ((StaticBox<JsxNamesᴛ1>)ᏑJsxNames).ValueSlot;


partial struct ReactNamesᴛ1 /*dyn*/ {
    public @string Fragment;
}
public static readonly ж<ReactNamesᴛ1> ᏑReactNames = new StaticBox<ReactNamesᴛ1>(new ReactNamesᴛ1(
    Fragment: "Fragment"
));
public static ref ReactNamesᴛ1 ReactNames => ref ((StaticBox<ReactNamesᴛ1>)ᏑReactNames).ValueSlot;

} partial class checkerᴛChecker {

internal static ж<Type> checkJsxElement(this ж<Checker> Ꮡc, ж<ast.Node> node, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();

    c.checkNodeDeferred(node);
    return Ꮡc.getJsxElementTypeAt(node);
}

internal static void checkJsxElementDeferred(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var jsxElement = node.AsJsxElement();
    Ꮡc.checkJsxOpeningLikeElementOrOpeningFragment(jsxElement.OpeningElement);
    if (isJsxIntrinsicTagName(jsxElement.ClosingElement.TagName())) {
        Ꮡc.getIntrinsicTagSymbol(jsxElement.ClosingElement);
    } else {
        Ꮡc.checkExpression(jsxElement.ClosingElement.TagName());
    }
    Ꮡc.checkJsxChildren(Ꮡnode, CheckModeNormal);
}

internal static ж<Type> checkJsxExpression(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡc.checkGrammarJsxExpression(node.AsJsxExpression());
    if (node.Expression() == nil) {
        return c.errorType;
    }
    var t = Ꮡc.checkExpressionEx(node.Expression(), checkMode);
    if (node.AsJsxExpression().DotDotDotToken != nil && t != c.anyType && !c.isArrayType(t)) {
        Ꮡc.error(Ꮡnode, diagnostics.JSX_spread_child_must_be_an_array_type);
    }
    return t;
}

internal static ж<Type> checkJsxSelfClosingElement(this ж<Checker> Ꮡc, ж<ast.Node> node, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();

    c.checkNodeDeferred(node);
    return Ꮡc.getJsxElementTypeAt(node);
}

internal static void checkJsxSelfClosingElementDeferred(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    Ꮡc.checkJsxOpeningLikeElementOrOpeningFragment(node);
}

internal static ж<Type> checkJsxFragment(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡc.checkJsxOpeningLikeElementOrOpeningFragment(node.AsJsxFragment().OpeningFragment);
    var nodeSourceFile = ast.GetSourceFileOfNode(Ꮡnode);
    if (c.compilerOptions.GetJSXTransformEnabled() && (c.compilerOptions.JsxFactory != ""u8 || ast.GetPragmaFromSourceFile(nodeSourceFile, "jsx") != nil) && c.compilerOptions.JsxFragmentFactory == ""u8 && ast.GetPragmaFromSourceFile(nodeSourceFile, "jsxfrag") == nil) {
        var message = core.IfElse(c.compilerOptions.JsxFactory != ""u8,
            diagnostics.The_jsxFragmentFactory_compiler_option_must_be_provided_to_use_JSX_fragments_with_the_jsxFactory_compiler_option,
            diagnostics.An_jsxFrag_pragma_is_required_when_using_an_jsx_pragma_with_JSX_fragments);
        Ꮡc.error(Ꮡnode, message);
    }
    Ꮡc.checkJsxChildren(Ꮡnode, CheckModeNormal);
    var t = Ꮡc.getJsxElementTypeAt(Ꮡnode);
    return core.IfElse(c.isErrorType(t), c.anyType, t);
}

internal static ж<Type> checkJsxAttributes(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    c.checkNodeDeferred(Ꮡnode);
    return Ꮡc.createJsxAttributesTypeFromAttributesProperty(node.Parent, checkMode);
}

internal static void checkJsxOpeningLikeElementOrOpeningFragment(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var isNodeOpeningLikeElement = ast.IsJsxOpeningLikeElement(Ꮡnode);
    if (isNodeOpeningLikeElement) {
        Ꮡc.checkGrammarJsxElement(Ꮡnode);
    }
    Ꮡc.checkJsxPreconditions(Ꮡnode);
    Ꮡc.markJsxAliasReferenced(Ꮡnode);
    var sig = Ꮡc.getResolvedSignature(Ꮡnode, nil, CheckModeNormal);
    Ꮡc.checkDeprecatedSignature(sig, Ꮡnode);
    if (isNodeOpeningLikeElement) {
        var elementTypeConstraint = Ꮡc.getJsxElementTypeTypeAt(Ꮡnode);
        if (elementTypeConstraint != nil) {
            var tagName = node.TagName();
            ж<Type> tagType = default;
            if (isJsxIntrinsicTagName(tagName)) {
                tagType = Ꮡc.getStringLiteralType(tagName.Text());
            } else {
                tagType = Ꮡc.checkExpression(tagName);
            }
            ref var diags = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡdiags);
            if (!Ꮡc.checkTypeRelatedToEx(tagType, elementTypeConstraint, c.assignableRelation, tagName, diagnostics.Its_type_0_is_not_a_valid_JSX_element_type, Ꮡdiags)) {
                Ꮡc.addDiagnostic(ast.NewDiagnosticChain(diags[0], diagnostics.X_0_cannot_be_used_as_a_JSX_component, scanner.GetTextOfNode(tagName)));
            }
        } else {
            Ꮡc.checkJsxReturnAssignableToAppropriateBound(Ꮡc.getJsxReferenceKind(Ꮡnode), Ꮡc.getReturnTypeOfSignature(sig), Ꮡnode);
        }
    }
}

internal static void checkJsxPreconditions(this ж<Checker> Ꮡc, ж<ast.Node> errorNode) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (c.compilerOptions.Jsx == core.JsxEmitNone) {
        Ꮡc.error(errorNode, diagnostics.Cannot_use_JSX_unless_the_jsx_flag_is_provided);
    }
    if (c.noImplicitAny && Ꮡc.getJsxElementTypeAt(errorNode) == nil) {
        Ꮡc.error(errorNode, diagnostics.JSX_element_implicitly_has_type_any_because_the_global_type_JSX_Element_does_not_exist);
    }
}

internal static void checkJsxReturnAssignableToAppropriateBound(this ж<Checker> Ꮡc, JsxReferenceKind refKind, ж<Type> elemInstanceType, ж<ast.Node> ᏑopeningLikeElement) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var openingLikeElement = ref ᏑopeningLikeElement.DerefOrNull();

    ref var diags = ref heap<slice<ж<ast.Diagnostic>>>(out var Ꮡdiags);
    switch (refKind) {
    case var ᴛ when ᴛ == JsxReferenceKindFunction: {
        var sfcReturnConstraint = Ꮡc.getJsxStatelessElementTypeAt(ᏑopeningLikeElement);
        if (sfcReturnConstraint != nil) {
            Ꮡc.checkTypeRelatedToEx(elemInstanceType, sfcReturnConstraint, c.assignableRelation, openingLikeElement.TagName(), diagnostics.Its_return_type_0_is_not_a_valid_JSX_element, Ꮡdiags);
        }
        break;
    }
    case var ᴛ when ᴛ == JsxReferenceKindComponent: {
        var classConstraint = Ꮡc.getJsxElementClassTypeAt(ᏑopeningLikeElement);
        if (classConstraint != nil) {
            Ꮡc.checkTypeRelatedToEx(elemInstanceType, classConstraint, c.assignableRelation, openingLikeElement.TagName(), diagnostics.Its_instance_type_0_is_not_a_valid_JSX_element, Ꮡdiags);
        }
        break;
    }
    default: {
        var sfcReturnConstraint = Ꮡc.getJsxStatelessElementTypeAt(ᏑopeningLikeElement);
        var classConstraint = Ꮡc.getJsxElementClassTypeAt(ᏑopeningLikeElement);
        if (sfcReturnConstraint == nil || classConstraint == nil) {
            return;
        }
        var combined = Ꮡc.getUnionType([sfcReturnConstraint, classConstraint]);
        Ꮡc.checkTypeRelatedToEx(elemInstanceType, combined, c.assignableRelation, openingLikeElement.TagName(), diagnostics.Its_element_type_0_is_not_a_valid_JSX_element, Ꮡdiags);
        break;
    }}

    if (len(diags) != 0) {
        Ꮡc.addDiagnostic(ast.NewDiagnosticChain(diags[0], diagnostics.X_0_cannot_be_used_as_a_JSX_component, scanner.GetTextOfNode(openingLikeElement.TagName())));
    }
}

internal static slice<ж<Type>> inferJsxTypeArguments(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Signature> signature, CheckMode checkMode, ж<InferenceContext> Ꮡcontext) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var context = ref Ꮡcontext.DerefOrNull();

    var paramType = Ꮡc.getEffectiveFirstArgumentForJsxSignature(signature, Ꮡnode);
    var checkAttrType = Ꮡc.checkExpressionWithContextualType(node.Attributes(), paramType, Ꮡcontext, checkMode);
    Ꮡc.inferTypes(context.inferences, checkAttrType, paramType, InferencePriorityNone, false);
    return Ꮡc.getInferredTypes(Ꮡcontext);
}

internal static ж<Type> getContextualTypeForJsxExpression(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ContextFlags contextFlags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (ᐧ) {
    case {} when ast.IsJsxAttributeLike(node.Parent): {
        return Ꮡc.getContextualType(Ꮡnode, contextFlags);
    }
    case {} when ast.IsJsxElement(node.Parent): {
        return Ꮡc.getContextualTypeForChildJsxExpression(node.Parent, Ꮡnode, contextFlags);
    }}

    return default;
}

internal static ж<Type> getContextualTypeForJsxAttribute(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡattribute, ContextFlags contextFlags) {
    ref var attribute = ref Ꮡattribute.DerefOrNull();

    if (ast.IsJsxAttribute(Ꮡattribute)) {
        var attributesType = Ꮡc.getApparentTypeOfContextualType(attribute.Parent, contextFlags);
        if (attributesType == nil || IsTypeAny(attributesType)) {
            return default;
        }
        return Ꮡc.getTypeOfPropertyOfContextualType(attributesType, attribute.Name().Text());
    }
    return Ꮡc.getContextualType(attribute.Parent, contextFlags);
}

internal static ж<Type> getContextualJsxElementAttributesType(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ContextFlags contextFlags) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsJsxOpeningElement(Ꮡnode) && contextFlags != ContextFlagsIgnoreNodeInferences) {
        nint index = c.findContextualNode(node.Parent, contextFlags == ContextFlagsNone);
        if (index >= 0) {
            return c.contextualInfos[index].t;
        }
    }
    return Ꮡc.getContextualTypeForArgumentAtIndex(Ꮡnode, 0);
}

internal static ж<Type> getContextualTypeForChildJsxExpression(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<astꓸJsxChild> child, ContextFlags contextFlags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var attributesType = Ꮡc.getApparentTypeOfContextualType(node.AsJsxElement().OpeningElement.Attributes(), contextFlags);
    @string jsxChildrenPropertyName = Ꮡc.getJsxElementChildrenPropertyName(Ꮡc.getJsxNamespaceAt(Ꮡnode));
    if (!(attributesType != nil && !IsTypeAny(attributesType) && jsxChildrenPropertyName != ast.InternalSymbolNameMissing && jsxChildrenPropertyName != ""u8)) {
        return default;
    }
    var realChildren = ast.GetSemanticJsxChildren(node.Children().Nodes);
    nint childIndex = slices.Index(realChildren, child);
    var childFieldType = Ꮡc.getTypeOfPropertyOfContextualType(attributesType, jsxChildrenPropertyName);
    if (childFieldType == nil) {
        return default;
    }
    if (len(realChildren) == 1) {
        return childFieldType;
    }
    return Ꮡc.mapTypeEx(childFieldType, (ж<Type> t) => {
        if (Ꮡc.isArrayLikeType(t)) {
            return Ꮡc.getIndexedAccessType(t, Ꮡc.getNumberLiteralType((jsnum.Number)(float64)childIndex));
        }
        return t;
    }, true);
}

internal static ж<Type> discriminateContextualTypeByJSXAttributes(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> ᏑcontextualType) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var contextualType = ref ᏑcontextualType.DerefOrNull();

    var key = new DiscriminatedContextualTypeKey(nodeId: ast.GetNodeId(Ꮡnode), typeId: contextualType.id);
    var discriminatedΔ1 = c.discriminatedContextualTypes[key]; if (discriminatedΔ1 != nil) {
        return discriminatedΔ1;
    }
    @string jsxChildrenPropertyName = Ꮡc.getJsxElementChildrenPropertyName(Ꮡc.getJsxNamespaceAt(Ꮡnode));
    var discriminantProperties = core.Filter(node.Properties(), (ж<ast.Node> p) => {
        var symbol = p.Symbol();
        if (symbol == nil || !ast.IsJsxAttribute(p)) {
            return false;
        }
        var initializer = p.Initializer();
        return (initializer == nil || Ꮡc.Value.isPossiblyDiscriminantValue(initializer)) && Ꮡc.isDiscriminantProperty(ᏑcontextualType, symbol.Name);
    });
    var discriminantMembers = core.Filter(Ꮡc.getPropertiesOfType(ᏑcontextualType), (ж<ast.Symbol> s) => {
        if ((ast.SymbolFlags)(s.Flags & ast.SymbolFlagsOptional) == 0 || Ꮡnode.Value.Symbol() == nil) {
            return false;
        }
        var element = Ꮡnode.Value.Parent.Parent;
        if (s.Name == jsxChildrenPropertyName && ast.IsJsxElement(element) && len(ast.GetSemanticJsxChildren(element.Children().Nodes)) != 0) {
            return false;
        }
        return Ꮡnode.Value.Symbol().Members[s.Name] == nil && Ꮡc.isDiscriminantProperty(ᏑcontextualType, s.Name);
    });
    var discriminator = Ꮡ(new ObjectLiteralDiscriminator(c: Ꮡc, props: discriminantProperties, members: discriminantMembers));
    var discriminated = Ꮡc.discriminateTypeByDiscriminableItems(ᏑcontextualType, new ObjectLiteralDiscriminatorжDiscriminator(discriminator));
    c.discriminatedContextualTypes[key] = discriminated;
    return discriminated;
}

internal static bool elaborateJsxComponents(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Type> source, ж<Type> target, ж<Relation> relation, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var reportedError = false;
    foreach (var (_, prop) in range(node.Properties())) {
        if (!ast.IsJsxSpreadAttribute(prop) && !isHyphenatedJsxName(prop.Name().Text())) {
            var nameType = Ꮡc.getStringLiteralType(prop.Name().Text());
            if (nameType != nil && (TypeFlags)(nameType.flags & TypeFlagsNever) == 0) {
                reportedError = Ꮡc.elaborateElement(source, target, relation, prop.Name(), prop.Initializer(), nameType, nil, default, diagnosticOutput) || reportedError;
            }
        }
    }
    if (ast.IsJsxOpeningElement(node.Parent) && ast.IsJsxElement(node.Parent.Parent)) {
        var containingElement = node.Parent.Parent;
        @string childrenPropName = Ꮡc.getJsxElementChildrenPropertyName(Ꮡc.getJsxNamespaceAt(Ꮡnode));
        if (childrenPropName == ast.InternalSymbolNameMissing) {
            childrenPropName = "children";
        }
        var childrenNameType = Ꮡc.getStringLiteralType(childrenPropName);
        var childrenTargetType = Ꮡc.getIndexedAccessType(target, childrenNameType);
        var validChildren = ast.GetSemanticJsxChildren(containingElement.Children().Nodes);
        if (len(validChildren) == 0) {
            return reportedError;
        }
        var moreThanOneRealChildren = len(validChildren) > 1;
        ж<Type> arrayLikeTargetParts = default;
        ж<Type> nonArrayLikeTargetParts = default;
        var iterableType = c.getGlobalIterableType();
        if (iterableType != c.emptyGenericType) {
            var anyIterable = Ꮡc.createIterableType(c.anyType);
            var anyIterableʗ1 = anyIterable;
            arrayLikeTargetParts = Ꮡc.filterType(childrenTargetType, (ж<Type> t) => Ꮡc.isTypeAssignableTo(t, anyIterableʗ1));
            var anyIterableʗ2 = anyIterable;
            nonArrayLikeTargetParts = Ꮡc.filterType(childrenTargetType, (ж<Type> t) => !Ꮡc.isTypeAssignableTo(t, anyIterableʗ2));
        } else {
            arrayLikeTargetParts = Ꮡc.filterType(childrenTargetType, Ꮡc.isArrayOrTupleLikeType);
            nonArrayLikeTargetParts = Ꮡc.filterType(childrenTargetType, (ж<Type> t) => !Ꮡc.isArrayOrTupleLikeType(t));
        }
        ref var invalidTextDiagnostic = ref heap<ж<diagnostics.Message>>(out var ᏑinvalidTextDiagnostic);
        ref var invalidTextDiagnosticArgs = ref heap<slice<any>>(out var ᏑinvalidTextDiagnosticArgs);
        var childrenTargetTypeʗ1 = childrenTargetType;
        var getInvalidTextualChildDiagnostic = () => {
            if (ᏑinvalidTextDiagnostic.ValueSlot == nil) {
                @string tagNameText = scanner.GetTextOfNode(Ꮡnode.Value.Parent.TagName());
                ᏑinvalidTextDiagnostic.ValueSlot = diagnostics.X_0_components_don_t_accept_text_as_child_elements_Text_in_JSX_has_the_type_string_but_the_expected_type_of_1_is_2;
                ᏑinvalidTextDiagnosticArgs.ValueSlot = slice<any>([tagNameText, childrenPropName, Ꮡc.TypeToString(childrenTargetTypeʗ1)]);
            }
            return (ᏑinvalidTextDiagnostic.ValueSlot, ᏑinvalidTextDiagnosticArgs.ValueSlot);
        };
        if (moreThanOneRealChildren) {
            if (arrayLikeTargetParts != c.neverType) {
                var realSource = Ꮡc.createTupleType(Ꮡc.checkJsxChildren(containingElement, CheckModeNormal));
                var children = Ꮡc.generateJsxChildren(containingElement, getInvalidTextualChildDiagnostic);
                reportedError = Ꮡc.elaborateIterableOrArrayLikeTargetElementwise(children, realSource, arrayLikeTargetParts, relation, diagnosticOutput) || reportedError;
            } else if (!Ꮡc.isTypeRelatedTo(Ꮡc.getIndexedAccessType(source, childrenNameType), childrenTargetType, relation)) {
                var diag = Ꮡc.error(containingElement.AsJsxElement().OpeningElement.TagName(), diagnostics.This_JSX_tag_s_0_prop_expects_a_single_child_of_type_1_but_multiple_children_were_provided, childrenPropName, Ꮡc.TypeToString(childrenTargetType));
                Ꮡc.reportDiagnostic(diag, diagnosticOutput);
                reportedError = true;
            }
        } else {
            if (nonArrayLikeTargetParts != c.neverType) {
                var child = validChildren[0];
                var e = c.getElaborationElementForJsxChild(child, childrenNameType, getInvalidTextualChildDiagnostic);
                if (e.errorNode != nil) {
                    reportedError = Ꮡc.elaborateElement(source, target, relation, e.errorNode, e.innerExpression, e.nameType, nil, e.createDiagnostic, diagnosticOutput) || reportedError;
                }
            } else if (!Ꮡc.isTypeRelatedTo(Ꮡc.getIndexedAccessType(source, childrenNameType), childrenTargetType, relation)) {
                var diag = Ꮡc.error(containingElement.AsJsxElement().OpeningElement.TagName(), diagnostics.This_JSX_tag_s_0_prop_expects_type_1_which_requires_multiple_children_but_only_a_single_child_was_provided, childrenPropName, Ꮡc.TypeToString(childrenTargetType));
                Ꮡc.reportDiagnostic(diag, diagnosticOutput);
                reportedError = true;
            }
        }
    }
    return reportedError;
}

} partial class checker_package {

partial struct JsxElaborationElement {
    internal ж<ast.Node> errorNode;
    internal ж<ast.Node> innerExpression;
    internal ж<Type> nameType;
    internal Func<ж<ast.Node>, ж<ast.Diagnostic>> createDiagnostic;
}

} partial class checkerᴛChecker {

internal static iter.Seq<JsxElaborationElement> generateJsxChildren(this ж<Checker> Ꮡc, ж<ast.Node> node, Func<(ж<diagnostics.Message>, slice<any>)> getInvalidTextDiagnostic) {
    return (Func<JsxElaborationElement, bool> yield) => {
        nint memberOffset = 0;
        foreach (var (i, child) in range(node.Value.Children().Nodes)) {
            var nameType = Ꮡc.getNumberLiteralType((jsnum.Number)(float64)(i - memberOffset));
            var e = Ꮡc.Value.getElaborationElementForJsxChild(child, nameType, getInvalidTextDiagnostic);
            if (e.errorNode != nil) {
                if (!yield(e)) {
                    return;
                }
            } else {
                memberOffset++;
            }
        }
    };
}

internal static JsxElaborationElement getElaborationElementForJsxChild(this ref Checker c, ж<ast.Node> Ꮡchild, ж<Type> nameType, Func<(ж<diagnostics.Message>, slice<any>)> getInvalidTextDiagnostic) {
    ref var child = ref Ꮡchild.DerefOrNull();

    switch (child.Kind) {
    case var ᴛ when ᴛ == ast.KindJsxExpression: {
        return new JsxElaborationElement(errorNode: Ꮡchild, innerExpression: child.Expression(), nameType: nameType);
    }
    case var ᴛ when ᴛ == ast.KindJsxText: {
        if (child.AsJsxText().ContainsOnlyTriviaWhiteSpaces) {
            return new JsxElaborationElement();
        }
        return new JsxElaborationElement(
            errorNode: Ꮡchild,
            innerExpression: nil,
            nameType: nameType,
            createDiagnostic: (ж<ast.Node> prop) => {
                var (errorMessage, errorArgs) = getInvalidTextDiagnostic();
                return NewDiagnosticForNode(prop, errorMessage, errorArgs.ꓸꓸꓸ);
            }
        );
    }
    case var ᴛ when ᴛ == ast.KindJsxElement || ᴛ == ast.KindJsxSelfClosingElement || ᴛ == ast.KindJsxFragment: {
        return new JsxElaborationElement(errorNode: Ꮡchild, innerExpression: Ꮡchild, nameType: nameType);
    }}

    throw panic("Unhandled case in getElaborationElementForJsxChild");
}

internal static bool elaborateIterableOrArrayLikeTargetElementwise(this ж<Checker> Ꮡc, iter.Seq<JsxElaborationElement> iterator, ж<Type> source, ж<Type> target, ж<Relation> relation, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var c = ref Ꮡc.DerefOrNull();

    var tupleOrArrayLikeTargetParts = Ꮡc.filterType(target, Ꮡc.isArrayOrTupleLikeType);
    var nonTupleOrArrayLikeTargetParts = Ꮡc.filterType(target, (ж<Type> t) => !Ꮡc.isArrayOrTupleLikeType(t));
    ж<Type> iterationType = default;
    if (nonTupleOrArrayLikeTargetParts != c.neverType) {
        iterationType = Ꮡc.getIterationTypeOfIterable(IterationUseForOf, IterationTypeKindYield, nonTupleOrArrayLikeTargetParts, nil);
    }
    var reportedError = false;
    foreach (var e in range<JsxElaborationElement>(iterator.Invoke)) {
        var prop = e.errorNode;
        var next = e.innerExpression;
        var nameType = e.nameType;
        var targetPropType = iterationType;
        ж<Type> targetIndexedPropType = default;
        if (tupleOrArrayLikeTargetParts != c.neverType) {
            targetIndexedPropType = Ꮡc.getBestMatchIndexedAccessTypeOrUndefined(source, tupleOrArrayLikeTargetParts, nameType);
        }
        if (targetIndexedPropType != nil && (TypeFlags)(targetIndexedPropType.flags & TypeFlagsIndexedAccess) == 0) {
            if (iterationType != nil) {
                targetPropType = Ꮡc.getUnionType([iterationType, targetIndexedPropType]);
            } else {
                targetPropType = targetIndexedPropType;
            }
        }
        if (targetPropType == nil) {
            continue;
        }
        var sourcePropType = Ꮡc.getIndexedAccessTypeOrUndefined(source, nameType, AccessFlagsNone, nil, nil);
        if (sourcePropType == nil) {
            continue;
        }
        @string propName = c.getPropertyNameFromIndex(nameType, nil);
        if (!Ꮡc.checkTypeRelatedTo(sourcePropType, targetPropType, relation, nil)) {
            var elaborated = next != nil && Ꮡc.elaborateError(next, sourcePropType, targetPropType, relation, nil, diagnosticOutput);
            reportedError = true;
            if (!elaborated) {
                var specificSource = sourcePropType;
                if (next != nil) {
                    specificSource = Ꮡc.checkExpressionForMutableLocationWithContextualType(next, sourcePropType);
                }
                if (e.createDiagnostic != nil) {
                    Ꮡc.reportDiagnostic(e.createDiagnostic(prop), diagnosticOutput);
                } else if (c.exactOptionalPropertyTypes && c.isExactOptionalPropertyMismatch(specificSource, targetPropType)) {
                    var diag = createDiagnosticForNode(prop, diagnostics.Type_0_is_not_assignable_to_type_1_with_exactOptionalPropertyTypes_Colon_true_Consider_adding_undefined_to_the_type_of_the_target, Ꮡc.TypeToString(specificSource), Ꮡc.TypeToString(targetPropType));
                    Ꮡc.reportDiagnostic(diag, diagnosticOutput);
                } else {
                    var targetIsOptional = propName != ast.InternalSymbolNameMissing && (ast.SymbolFlags)(core.OrElse(Ꮡc.getPropertyOfType(tupleOrArrayLikeTargetParts, propName), c.unknownSymbol).Flags & ast.SymbolFlagsOptional) != 0;
                    var sourceIsOptional = propName != ast.InternalSymbolNameMissing && (ast.SymbolFlags)(core.OrElse(Ꮡc.getPropertyOfType(source, propName), c.unknownSymbol).Flags & ast.SymbolFlagsOptional) != 0;
                    targetPropType = Ꮡc.removeMissingType(targetPropType, targetIsOptional);
                    sourcePropType = Ꮡc.removeMissingType(sourcePropType, targetIsOptional && sourceIsOptional);
                    var result = Ꮡc.checkTypeRelatedToEx(specificSource, targetPropType, relation, prop, nil, diagnosticOutput);
                    if (result && specificSource != sourcePropType) {
                        Ꮡc.checkTypeRelatedToEx(sourcePropType, targetPropType, relation, prop, nil, diagnosticOutput);
                    }
                }
            }
        }
    }
    return reportedError;
}

internal static ж<ast.Symbol> getSuggestedSymbolForNonexistentJSXAttribute(this ж<Checker> Ꮡc, @string name, ж<Type> containingType) {
    var properties = Ꮡc.getPropertiesOfType(containingType);
    ж<ast.Symbol> jsxSpecific = default;
    switch (ᐧ) {
    case {} when name == "for"u8: {
        jsxSpecific = core.Find(properties, (ж<ast.Symbol> x) => ast.SymbolName(x) == "htmlFor"u8);
        break;
    }
    case {} when name == "class"u8: {
        jsxSpecific = core.Find(properties, (ж<ast.Symbol> x) => ast.SymbolName(x) == "className"u8);
        break;
    }}

    if (jsxSpecific != nil) {
        return jsxSpecific;
    }
    return Ꮡc.getSpellingSuggestionForName(name, slices.Values<slice<ж<ast.Symbol>>, ж<ast.Symbol>>(properties), ast.SymbolFlagsValue);
}

internal static ж<Type> getJSXFragmentType(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    var links = c.sourceFileLinks.Get(ast.GetSourceFileOfNode(node));
    if (links.jsxFragmentType != nil) {
        return links.jsxFragmentType;
    }
    @string jsxFragmentFactoryName = c.getJsxNamespace(node);
    var shouldResolveFactoryReference = (c.compilerOptions.Jsx == core.JsxEmitReact || c.compilerOptions.JsxFragmentFactory != ""u8) && jsxFragmentFactoryName != "null"u8;
    if (!shouldResolveFactoryReference) {
        links.jsxFragmentType = c.anyType;
        return links.jsxFragmentType;
    }
    var jsxFactorySymbol = Ꮡc.getJsxNamespaceContainerForImplicitImport(node);
    if (jsxFactorySymbol == nil) {
        var shouldModuleRefErr = c.compilerOptions.Jsx != core.JsxEmitPreserve && c.compilerOptions.Jsx != core.JsxEmitReactNative;
        var flags = ast.SymbolFlagsValue;
        if (!shouldModuleRefErr) {
            flags &= unchecked((ast.SymbolFlags)~(ast.SymbolFlags)(ast.SymbolFlagsEnum));
        }
        jsxFactorySymbol = c.resolveName(node, jsxFragmentFactoryName, flags, diagnostics.Using_JSX_fragments_requires_fragment_factory_0_to_be_in_scope_but_it_could_not_be_found, true, false);
    }
    if (jsxFactorySymbol == nil) {
        links.jsxFragmentType = c.errorType;
        return links.jsxFragmentType;
    }
    if (jsxFactorySymbol.Name == ReactNames.Fragment) {
        links.jsxFragmentType = Ꮡc.getTypeOfSymbol(jsxFactorySymbol);
        return links.jsxFragmentType;
    }
    var resolvedAlias = jsxFactorySymbol;
    if ((ast.SymbolFlags)(jsxFactorySymbol.Flags & ast.SymbolFlagsAlias) != 0) {
        resolvedAlias = Ꮡc.resolveAlias(jsxFactorySymbol);
    }

    var reactExports = Ꮡc.getExportsOfSymbol(resolvedAlias);
    var typeSymbol = Ꮡc.getSymbol(reactExports, ReactNames.Fragment, ast.SymbolFlagsBlockScopedVariable);
    if (typeSymbol != nil) {
        links.jsxFragmentType = Ꮡc.getTypeOfSymbol(typeSymbol);
    } else {
        links.jsxFragmentType = c.errorType;
    }
    return links.jsxFragmentType;
}

internal static ж<Signature> resolveJsxOpeningLikeElement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<slice<ж<Signature>>> candidatesOutArray, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var isJsxOpenFragment = ast.IsJsxOpeningFragment(Ꮡnode);
    ж<Type> exprTypes = default;
    if (!isJsxOpenFragment) {
        if (isJsxIntrinsicTagName(node.TagName())) {
            var result = Ꮡc.getIntrinsicAttributesTypeFromJsxOpeningLikeElement(Ꮡnode);
            var fakeSignature = Ꮡc.createSignatureForJSXIntrinsic(Ꮡnode, result);
            Ꮡc.checkTypeAssignableToAndOptionallyElaborate(Ꮡc.checkExpressionWithContextualType(node.Attributes(), Ꮡc.getEffectiveFirstArgumentForJsxSignature(fakeSignature, Ꮡnode), nil, CheckModeNormal), result, node.TagName(), node.Attributes(), nil, nil);
            var typeArguments = node.TypeArguments();
            if (len(typeArguments) != 0) {
                Ꮡc.checkSourceElements(typeArguments);
                var sourceFile = ast.GetSourceFileOfNode(Ꮡnode);
                var typeArgumentList = node.TypeArgumentList();
                var loc = core.NewTextRange(scanner.SkipTrivia(sourceFile.Text(), typeArgumentList.Loc.Pos()), typeArgumentList.Loc.End());
                Ꮡc.addDiagnostic(ast.NewDiagnostic(sourceFile, loc, diagnostics.Expected_0_type_arguments_but_got_1, (nint)0, len(typeArguments)));
            }
            return fakeSignature;
        }
        exprTypes = Ꮡc.checkExpression(node.TagName());
    } else {
        exprTypes = Ꮡc.getJSXFragmentType(Ꮡnode);
    }
    var apparentType = Ꮡc.getApparentType(exprTypes);
    if (c.isErrorType(apparentType)) {
        return Ꮡc.resolveErrorCall(Ꮡnode);
    }
    var signatures = Ꮡc.getUninstantiatedJsxSignaturesOfType(exprTypes, Ꮡnode);
    if (Ꮡc.isUntypedFunctionCall(exprTypes, apparentType, len(signatures), 0)) {
        return Ꮡc.resolveUntypedCall(Ꮡnode);
    }
    if (len(signatures) == 0) {
        if (isJsxOpenFragment) {
            Ꮡc.error(Ꮡnode, diagnostics.JSX_element_type_0_does_not_have_any_construct_or_call_signatures, scanner.GetTextOfNode(Ꮡnode));
        } else {
            Ꮡc.error(node.TagName(), diagnostics.JSX_element_type_0_does_not_have_any_construct_or_call_signatures, scanner.GetTextOfNode(node.TagName()));
        }
        return Ꮡc.resolveErrorCall(Ꮡnode);
    }
    return Ꮡc.resolveCall(Ꮡnode, signatures, candidatesOutArray, checkMode, SignatureFlagsNone, nil);
}

internal static bool checkApplicableSignatureForJsxCallLikeElement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<Signature> signature, ж<Relation> relation, CheckMode checkMode, bool reportErrors, ж<slice<ж<ast.Diagnostic>>> diagnosticOutput) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var paramType = Ꮡc.getEffectiveFirstArgumentForJsxSignature(signature, Ꮡnode);
    ж<Type> attributesType = default;
    if (ast.IsJsxOpeningFragment(Ꮡnode)) {
        attributesType = Ꮡc.createJsxAttributesTypeFromAttributesProperty(Ꮡnode, CheckModeNormal);
    } else {
        attributesType = Ꮡc.checkExpressionWithContextualType(node.Attributes(), paramType, nil, checkMode);
    }
    ж<Type> checkAttributesType = default;
    bool checkTagNameDoesNotExpectTooManyArguments() {
        if (Ꮡc.getJsxNamespaceContainerForImplicitImport(Ꮡnode) != nil) {
            return true;
        }
        ж<Type> tagType = default;
        if ((ast.IsJsxOpeningElement(Ꮡnode) || ast.IsJsxSelfClosingElement(Ꮡnode)) && !(isJsxIntrinsicTagName(Ꮡnode.Value.TagName()) || ast.IsJsxNamespacedName(Ꮡnode.Value.TagName()))) {
            tagType = Ꮡc.checkExpression(Ꮡnode.Value.TagName());
        }
        if (tagType == nil) {
            return true;
        }
        var tagCallSignatures = Ꮡc.getSignaturesOfType(tagType, SignatureKindCall);
        if (len(tagCallSignatures) == 0) {
            return true;
        }
        var factory = Ꮡc.Value.getJsxFactoryEntity(Ꮡnode);
        if (factory == nil) {
            return true;
        }
        var factorySymbol = Ꮡc.resolveEntityName(factory, ast.SymbolFlagsValue, true, false, Ꮡnode);
        if (factorySymbol == nil) {
            return true;
        }

        var factoryType = Ꮡc.getTypeOfSymbol(factorySymbol);
        var callSignatures = Ꮡc.getSignaturesOfType(factoryType, SignatureKindCall);
        if (len(callSignatures) == 0) {
            return true;
        }
        var hasFirstParamSignatures = false;
        nint maxParamCount = 0;
        foreach (var (_, sig) in range(callSignatures)) {
            var firstparam = Ꮡc.getTypeAtPosition(sig, 0);
            var signaturesOfParam = Ꮡc.getSignaturesOfType(firstparam, SignatureKindCall);
            if (len(signaturesOfParam) == 0) {
                continue;
            }
            foreach (var (_, paramSig) in range(signaturesOfParam)) {
                hasFirstParamSignatures = true;
                if (Ꮡc.hasEffectiveRestParameter(paramSig)) {
                    return true;
                }
                nint paramCount = Ꮡc.getParameterCount(paramSig);
                if (paramCount > maxParamCount) {
                    maxParamCount = paramCount;
                }
            }
        }
        if (!hasFirstParamSignatures) {
            return true;
        }
        nint absoluteMinArgCount = math.MaxInt;
        foreach (var (_, tagSig) in range(tagCallSignatures)) {
            nint tagRequiredArgCount = Ꮡc.getMinArgumentCount(tagSig);
            if (tagRequiredArgCount < absoluteMinArgCount) {
                absoluteMinArgCount = tagRequiredArgCount;
            }
        }
        if (absoluteMinArgCount <= maxParamCount) {
            return true;
        }
        if (reportErrors) {
            var tagName = Ꮡnode.Value.TagName();
            var diag = NewDiagnosticForNode(tagName, diagnostics.Tag_0_expects_at_least_1_arguments_but_the_JSX_factory_2_provides_at_most_3, entityNameToString(tagName), absoluteMinArgCount, entityNameToString(factory), maxParamCount);
            var tagNameSymbol = Ꮡc.getSymbolAtLocation(tagName, false);
            if (tagNameSymbol != nil && tagNameSymbol.ValueDeclaration != nil) {
                diag.AddRelatedInfo(NewDiagnosticForNode(tagNameSymbol.ValueDeclaration, diagnostics.X_0_is_declared_here, entityNameToString(tagName)));
            }
            Ꮡc.reportDiagnostic(diag, diagnosticOutput);
        }
        return false;
    }
    if ((CheckMode)(checkMode & CheckModeSkipContextSensitive) != 0) {
        checkAttributesType = Ꮡc.getRegularTypeOfObjectLiteral(attributesType);
    } else {
        checkAttributesType = attributesType;
    }
    if (!checkTagNameDoesNotExpectTooManyArguments()) {
        return false;
    }
    ж<ast.Node> errorNode = default;
    if (reportErrors) {
        if (ast.IsJsxOpeningFragment(Ꮡnode)) {
            errorNode = Ꮡnode;
        } else {
            errorNode = node.TagName();
        }
    }
    ж<ast.Node> attributes = default;
    if (!ast.IsJsxOpeningFragment(Ꮡnode)) {
        attributes = node.Attributes();
    }
    return Ꮡc.checkTypeRelatedToAndOptionallyElaborate(checkAttributesType, paramType, relation, errorNode, attributes, nil, diagnosticOutput);
}

internal static ж<Type> createJsxAttributesTypeFromAttributesProperty(this ж<Checker> Ꮡc, ж<ast.Node> ᏑopeningLikeElement, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var openingLikeElement = ref ᏑopeningLikeElement.DerefOrNull();

    ast.SymbolTable allAttributesTable = default;
    if (c.strictNullChecks) {
        allAttributesTable = new ast.SymbolTable(0);
    }
    ref var attributesTable = ref heap<ast.SymbolTable>(new ast.SymbolTable(0), out var ᏑattributesTable);
    ref var attributesSymbol = ref heap<ж<ast.Symbol>>(out var ᏑattributesSymbol);
    var attributeParent = ᏑopeningLikeElement;
    var spread = c.emptyJsxObjectType;
    bool hasSpreadAnyType = default;
    ж<Type> typeToIntersect = default;
    bool explicitlySpecifyChildrenAttribute = default;
    var objectFlags = ObjectFlagsJsxAttributes;
    ж<Type> createJsxAttributesType() {
        objectFlags |= (ObjectFlags)(ObjectFlagsFreshLiteral);
        var result = Ꮡc.newAnonymousType(ᏑattributesSymbol.ValueSlot, ᏑattributesTable.ValueSlot, default, default, default);
        result.objectFlags |= (ObjectFlags)((ObjectFlags)((ObjectFlags)(objectFlags | ObjectFlagsObjectLiteral) | ObjectFlagsContainsObjectOrArrayLiteral));
        return result;
    }
    @string jsxChildrenPropertyName = Ꮡc.getJsxElementChildrenPropertyName(Ꮡc.getJsxNamespaceAt(ᏑopeningLikeElement));
    var isJsxOpenFragment = ast.IsJsxOpeningFragment(ᏑopeningLikeElement);
    if (!isJsxOpenFragment) {
        var attributes = openingLikeElement.Attributes();
        attributesSymbol = attributes.Symbol();
        attributeParent = attributes;
        var contextualType = Ꮡc.getContextualType(attributes, ContextFlagsNone);
        foreach (var (_, attributeDecl) in range(attributes.Properties())) {
            var member = attributeDecl.Symbol();
            if (ast.IsJsxAttribute(attributeDecl)) {
                var exprType = Ꮡc.checkJsxAttribute(attributeDecl, checkMode);
                objectFlags |= (ObjectFlags)((ObjectFlags)(exprType.objectFlags & ObjectFlagsPropagatingFlags));
                var attributeSymbol = c.newSymbol((ast.SymbolFlags)(ast.SymbolFlagsProperty | member.Flags), member.Name);
                attributeSymbol.Declarations = member.Declarations;
                attributeSymbol.Parent = member.Parent;
                if (member.ValueDeclaration != nil) {
                    attributeSymbol.ValueDeclaration = member.ValueDeclaration;
                }
                var links = c.valueSymbolLinks.Get(attributeSymbol);
                links.resolvedType = exprType;
                links.target = member;
                attributesTable[attributeSymbol.Name] = attributeSymbol;
                if (allAttributesTable != nil) {
                    allAttributesTable[attributeSymbol.Name] = attributeSymbol;
                }
                if (attributeDecl.Name().Text() == jsxChildrenPropertyName) {
                    explicitlySpecifyChildrenAttribute = true;
                }
                if (contextualType != nil && (CheckMode)(checkMode & CheckModeInferential) != 0 && (CheckMode)(checkMode & CheckModeSkipContextSensitive) == 0 && Ꮡc.isContextSensitive(attributeDecl)) {
                    var inferenceContext = c.getInferenceContext(attributes);
                    debug.Assert(inferenceContext != nil);
                    var inferenceNode = attributeDecl.Initializer().Expression();
                    c.addIntraExpressionInferenceSite(inferenceContext, inferenceNode, exprType);
                }
            } else {
                debug.Assert(attributeDecl.Kind == ast.KindJsxSpreadAttribute);
                if (len(attributesTable) != 0) {
                    spread = Ꮡc.getSpreadType(spread, createJsxAttributesType(), attributesSymbol, objectFlags, false);
                    attributesTable = new ast.SymbolTable(0);
                }
                var exprType = Ꮡc.getReducedType(Ꮡc.checkExpressionEx(attributeDecl.Expression(), (CheckMode)(checkMode & CheckModeInferential)));
                if (IsTypeAny(exprType)) {
                    hasSpreadAnyType = true;
                }
                if (Ꮡc.isValidSpreadType(exprType)) {
                    spread = Ꮡc.getSpreadType(spread, exprType, attributesSymbol, objectFlags, false);
                    if (allAttributesTable != nil) {
                        Ꮡc.checkSpreadPropOverrides(exprType, allAttributesTable, attributeDecl);
                    }
                } else {
                    Ꮡc.error(attributeDecl.Expression(), diagnostics.Spread_types_may_only_be_created_from_object_types);
                    if (typeToIntersect != nil) {
                        typeToIntersect = Ꮡc.getIntersectionType([typeToIntersect, exprType]);
                    } else {
                        typeToIntersect = exprType;
                    }
                }
            }
        }
        if (!hasSpreadAnyType) {
            if (len(attributesTable) != 0) {
                spread = Ꮡc.getSpreadType(spread, createJsxAttributesType(), attributesSymbol, objectFlags, false);
            }
        }
    }
    bool parentHasSemanticJsxChildren(ж<ast.Node> openingLikeElementΔ1) {
        var parent = openingLikeElementΔ1.Parent;
        if (parent == nil) {
            return false;
        }
        slice<ж<ast.Node>> children = default;

        switch (ᐧ) {
        case {} when ast.IsJsxElement(parent): {
            if (parent.AsJsxElement().OpeningElement == openingLikeElementΔ1) {
                children = parent.Children().Nodes;
            }
            break;
        }
        case {} when ast.IsJsxFragment(parent): {
            if (parent.AsJsxFragment().OpeningFragment == openingLikeElementΔ1) {
                children = parent.Children().Nodes;
            }
            break;
        }}

        return len(ast.GetSemanticJsxChildren(children)) != 0;
    }
    if (parentHasSemanticJsxChildren(ᏑopeningLikeElement)) {
        slice<ж<Type>> childTypes = Ꮡc.checkJsxChildren(openingLikeElement.Parent, checkMode);
        if (!hasSpreadAnyType && jsxChildrenPropertyName != ast.InternalSymbolNameMissing && jsxChildrenPropertyName != ""u8) {
            if (explicitlySpecifyChildrenAttribute) {
                Ꮡc.error(attributeParent, diagnostics.X_0_are_specified_twice_The_attribute_named_0_will_be_overwritten, jsxChildrenPropertyName);
            }
            ж<Type> childrenContextualType = default;
            if (ast.IsJsxOpeningElement(ᏑopeningLikeElement)) {
                var contextualType = Ꮡc.getApparentTypeOfContextualType(openingLikeElement.Attributes(), ContextFlagsNone); if (contextualType != nil) {
                    childrenContextualType = Ꮡc.getTypeOfPropertyOfContextualType(contextualType, jsxChildrenPropertyName);
                }
            }
            var childrenPropSymbol = c.newSymbol(ast.SymbolFlagsProperty, jsxChildrenPropertyName);
            var links = c.valueSymbolLinks.Get(childrenPropSymbol);
            switch (ᐧ) {
            case {} when len(childTypes) is 1: {
                links.resolvedType = childTypes[0];
                break;
            }
            case {} when childrenContextualType != nil && someType(childrenContextualType, Ꮡc.isTupleLikeType): {
                links.resolvedType = Ꮡc.createTupleType(childTypes);
                break;
            }
            default: {
                links.resolvedType = Ꮡc.createArrayType(Ꮡc.getUnionType(childTypes));
                break;
            }}

            childrenPropSymbol.ValueDeclaration = c.factory.NewPropertySignatureDeclaration(nil, c.factory.NewIdentifier(jsxChildrenPropertyName), nil, nil, nil);
            childrenPropSymbol.ValueDeclaration.Parent = attributeParent;
            childrenPropSymbol.ValueDeclaration.AsPropertySignatureDeclaration().Value.Symbol = childrenPropSymbol;
            var childPropMap = new ast.SymbolTable(0);
            childPropMap[jsxChildrenPropertyName] = childrenPropSymbol;
            spread = Ꮡc.getSpreadType(spread, Ꮡc.newAnonymousType(attributesSymbol, childPropMap, default, default, default), attributesSymbol, (ObjectFlags)(objectFlags | c.getPropagatingFlagsOfTypes(childTypes, TypeFlagsNone)), false);
        }
    }
    if (hasSpreadAnyType) {
        return c.anyType;
    }
    if (typeToIntersect != nil) {
        if (spread != c.emptyJsxObjectType) {
            return Ꮡc.getIntersectionType([typeToIntersect, spread]);
        }
        return typeToIntersect;
    }
    if (spread == c.emptyJsxObjectType) {
        return createJsxAttributesType();
    }
    return spread;
}

internal static ж<Type> checkJsxAttribute(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Initializer() != nil) {
        return Ꮡc.checkExpressionForMutableLocation(node.Initializer(), checkMode);
    }
    return c.trueType;
}

internal static slice<ж<Type>> checkJsxChildren(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, CheckMode checkMode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    slice<ж<Type>> childTypes = default;
    foreach (var (_, child) in range(node.Children().Nodes)) {
        if (ast.IsJsxText(child)) {
            if (!child.AsJsxText().ContainsOnlyTriviaWhiteSpaces) {
                childTypes = append(childTypes, c.stringType);
            }
        } else if (ast.IsJsxExpression(child) && child.Expression() == nil) {
            continue;
        } else {
            childTypes = append(childTypes, Ꮡc.checkExpressionForMutableLocation(child, checkMode));
        }
    }
    return childTypes;
}

internal static slice<ж<Signature>> getUninstantiatedJsxSignaturesOfType(this ж<Checker> Ꮡc, ж<Type> ᏑelementType, ж<ast.Node> caller) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var elementType = ref ᏑelementType.DerefOrNull();

    if ((TypeFlags)(elementType.flags & TypeFlagsString) != 0) {
        return [c.anySignature];
    }
    if ((TypeFlags)(elementType.flags & TypeFlagsStringLiteral) != 0) {
        var intrinsicType = Ꮡc.getIntrinsicAttributesTypeFromStringLiteralType(ᏑelementType, caller);
        if (intrinsicType == nil) {
            Ꮡc.error(caller, diagnostics.Property_0_does_not_exist_on_type_1, getStringLiteralValue(ᏑelementType), "JSX." + JsxNames.IntrinsicElements);
            return default;
        }
        var fakeSignature = Ꮡc.createSignatureForJSXIntrinsic(caller, intrinsicType);
        return [fakeSignature];
    }
    var apparentElemType = Ꮡc.getApparentType(ᏑelementType);
    var signatures = Ꮡc.getSignaturesOfType(apparentElemType, SignatureKindConstruct);
    if (len(signatures) == 0) {
        signatures = Ꮡc.getSignaturesOfType(apparentElemType, SignatureKindCall);
    }
    if (len(signatures) == 0 && (TypeFlags)(apparentElemType.flags & TypeFlagsUnion) != 0) {
        signatures = Ꮡc.getUnionSignatures(core.Map(apparentElemType.Types(), slice<ж<Signature>> (ж<Type> t) => Ꮡc.getUninstantiatedJsxSignaturesOfType(t, caller)));
    }
    return signatures;
}

internal static ж<Type> getEffectiveFirstArgumentForJsxSignature(this ж<Checker> Ꮡc, ж<Signature> signature, ж<ast.Node> node) {
    if (ast.IsJsxOpeningFragment(node) || Ꮡc.getJsxReferenceKind(node) != JsxReferenceKindComponent) {
        return Ꮡc.getJsxPropsTypeFromCallSignature(signature, node);
    }
    return Ꮡc.getJsxPropsTypeFromClassType(signature, node);
}

internal static ж<Type> getJsxPropsTypeFromCallSignature(this ж<Checker> Ꮡc, ж<Signature> sig, ж<ast.Node> context) {
    ref var c = ref Ꮡc.DerefOrNull();

    var propsType = Ꮡc.getTypeOfFirstParameterOfSignatureWithFallback(sig, c.unknownType);
    propsType = Ꮡc.getJsxManagedAttributesFromLocatedAttributes(context, Ꮡc.getJsxNamespaceAt(context), propsType);
    var intrinsicAttribs = Ꮡc.getJsxType(JsxNames.IntrinsicAttributes, context);
    if (!c.isErrorType(intrinsicAttribs)) {
        propsType = Ꮡc.intersectTypes(intrinsicAttribs, propsType);
    }
    return propsType;
}

internal static ж<Type> getJsxPropsTypeFromClassType(this ж<Checker> Ꮡc, ж<Signature> sig, ж<ast.Node> Ꮡcontext) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var context = ref Ꮡcontext.DerefOrNull();

    var ns = Ꮡc.getJsxNamespaceAt(Ꮡcontext);
    @string forcedLookupLocation = Ꮡc.getJsxElementPropertiesName(ns);
    ж<Type> attributesType = default;
    switch (ᐧ) {
    case {} when forcedLookupLocation == ast.InternalSymbolNameMissing: {
        attributesType = Ꮡc.getTypeOfFirstParameterOfSignatureWithFallback(sig, c.unknownType);
        break;
    }
    case {} when forcedLookupLocation == ""u8: {
        attributesType = Ꮡc.getReturnTypeOfSignature(sig);
        break;
    }
    default: {
        attributesType = Ꮡc.getJsxPropsTypeForSignatureFromMember(sig, forcedLookupLocation);
        if (attributesType == nil && len(context.Attributes().Properties()) != 0) {
            Ꮡc.error(Ꮡcontext, diagnostics.JSX_element_class_does_not_support_attributes_because_it_does_not_have_a_0_property, forcedLookupLocation);
        }
        break;
    }}

    if (attributesType == nil) {
        return c.unknownType;
    }
    attributesType = Ꮡc.getJsxManagedAttributesFromLocatedAttributes(Ꮡcontext, ns, attributesType);
    if (IsTypeAny(attributesType)) {
        return attributesType;
    }
    var apparentAttributesType = attributesType;
    var intrinsicClassAttribs = Ꮡc.getJsxType(JsxNames.IntrinsicClassAttributes, Ꮡcontext);
    if (!c.isErrorType(intrinsicClassAttribs)) {
        var typeParams = Ꮡc.getLocalTypeParametersOfClassOrInterfaceOrTypeAlias(intrinsicClassAttribs.symbol);
        var hostClassType = Ꮡc.getReturnTypeOfSignature(sig);
        ж<Type> libraryManagedAttributeType = default;
        if (typeParams != nil) {
            var inferredArgs = Ꮡc.fillMissingTypeArguments([hostClassType], typeParams, c.getMinTypeArgumentCount(typeParams), ast.IsInJSFile(Ꮡcontext));
            libraryManagedAttributeType = Ꮡc.instantiateType(intrinsicClassAttribs, newTypeMapper(typeParams, inferredArgs));
        } else {
            libraryManagedAttributeType = intrinsicClassAttribs;
        }
        apparentAttributesType = Ꮡc.intersectTypes(libraryManagedAttributeType, apparentAttributesType);
    }
    var intrinsicAttribs = Ꮡc.getJsxType(JsxNames.IntrinsicAttributes, Ꮡcontext);
    if (!c.isErrorType(intrinsicAttribs)) {
        apparentAttributesType = Ꮡc.intersectTypes(intrinsicAttribs, apparentAttributesType);
    }
    return apparentAttributesType;
}

internal static ж<Type> getJsxPropsTypeForSignatureFromMember(this ж<Checker> Ꮡc, ж<Signature> Ꮡsig, @string forcedLookupLocation) {
    ref var sig = ref Ꮡsig.DerefOrNull();
    var resultsᴷ = new stackAppend<ж<Type>>();

    if (sig.composite != nil) {
        slice<ж<Type>> results = default;
        foreach (var (_, signature) in range(sig.composite.signatures)) {
            var instance = Ꮡc.getReturnTypeOfSignature(signature);
            if (IsTypeAny(instance)) {
                return instance;
            }
            var propType = Ꮡc.getTypeOfPropertyOfType(instance, forcedLookupLocation);
            if (propType == nil) {
                return default;
            }
            results = append(resultsᴷ, results, propType);
        }
        return Ꮡc.getIntersectionType(results);
    }
    var instanceType = Ꮡc.getReturnTypeOfSignature(Ꮡsig);
    if (IsTypeAny(instanceType)) {
        return instanceType;
    }
    return Ꮡc.getTypeOfPropertyOfType(instanceType, forcedLookupLocation);
}

internal static ж<Type> getJsxManagedAttributesFromLocatedAttributes(this ж<Checker> Ꮡc, ж<ast.Node> context, ж<ast.Symbol> ns, ж<Type> attributesType) {
    var managedSym = Ꮡc.getJsxLibraryManagedAttributes(ns);
    if (managedSym != nil) {
        var ctorType = Ꮡc.getStaticTypeOfReferencedJsxConstructor(context);
        var result = Ꮡc.instantiateAliasOrInterfaceWithDefaults(managedSym, [ctorType, attributesType], ast.IsInJSFile(context));
        if (result != nil) {
            return result;
        }
    }
    return attributesType;
}

internal static ж<Type> instantiateAliasOrInterfaceWithDefaults(this ж<Checker> Ꮡc, ж<ast.Symbol> ᏑmanagedSym, slice<ж<Type>> typeArguments, bool inJavaScript) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var managedSym = ref ᏑmanagedSym.DerefOrNull();

    var declaredManagedType = Ꮡc.getDeclaredTypeOfSymbol(ᏑmanagedSym);
    if ((ast.SymbolFlags)(managedSym.Flags & ast.SymbolFlagsTypeAlias) != 0) {
        var @params = c.typeAliasLinks.Get(ᏑmanagedSym).typeParameters;
        if (len(@params) >= len(typeArguments)) {
            var args = Ꮡc.fillMissingTypeArguments(typeArguments, @params, len(typeArguments), inJavaScript);
            if (len(args) == 0) {
                return declaredManagedType;
            }
            return Ꮡc.getTypeAliasInstantiation(ᏑmanagedSym, args, nil);
        }
    }
    if ((ObjectFlags)(declaredManagedType.objectFlags & ObjectFlagsClassOrInterface) != 0 && len(declaredManagedType.AsInterfaceType().TypeParameters()) >= len(typeArguments)) {
        var args = Ꮡc.fillMissingTypeArguments(typeArguments, declaredManagedType.AsInterfaceType().TypeParameters(), len(typeArguments), inJavaScript);
        return Ꮡc.createTypeReference(declaredManagedType, args);
    }
    return default;
}

internal static ж<ast.Symbol> getJsxLibraryManagedAttributes(this ж<Checker> Ꮡc, ж<ast.Symbol> ᏑjsxNamespace) {
    ref var jsxNamespace = ref ᏑjsxNamespace.DerefOrNull();

    if (ᏑjsxNamespace != nil) {
        return Ꮡc.getSymbol(jsxNamespace.Exports, JsxNames.LibraryManagedAttributes, ast.SymbolFlagsType);
    }
    return default;
}

internal static ж<ast.Symbol> getJsxElementTypeSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> ᏑjsxNamespace) {
    ref var jsxNamespace = ref ᏑjsxNamespace.DerefOrNull();

    if (ᏑjsxNamespace != nil) {
        return Ꮡc.getSymbol(jsxNamespace.Exports, JsxNames.ElementType, ast.SymbolFlagsType);
    }
    return default;
}

internal static @string getJsxElementPropertiesName(this ж<Checker> Ꮡc, ж<ast.Symbol> jsxNamespace) {
    return Ꮡc.getNameFromJsxElementAttributesContainer(JsxNames.ElementAttributesPropertyNameContainer, jsxNamespace);
}

internal static @string getJsxElementChildrenPropertyName(this ж<Checker> Ꮡc, ж<ast.Symbol> jsxNamespace) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (c.compilerOptions.Jsx == core.JsxEmitReactJSX || c.compilerOptions.Jsx == core.JsxEmitReactJSXDev) {
        return "children";
    }
    return Ꮡc.getNameFromJsxElementAttributesContainer(JsxNames.ElementChildrenAttributeNameContainer, jsxNamespace);
}

internal static @string getNameFromJsxElementAttributesContainer(this ж<Checker> Ꮡc, @string nameOfAttribPropContainer, ж<ast.Symbol> ᏑjsxNamespace) {
    ref var jsxNamespace = ref ᏑjsxNamespace.DerefOrNull();

    if (ᏑjsxNamespace != nil) {
        var jsxElementAttribPropInterfaceSym = Ꮡc.getSymbol(jsxNamespace.Exports, nameOfAttribPropContainer, ast.SymbolFlagsType);
        if (jsxElementAttribPropInterfaceSym != nil) {
            var jsxElementAttribPropInterfaceType = Ꮡc.getDeclaredTypeOfSymbol(jsxElementAttribPropInterfaceSym);
            var propertiesOfJsxElementAttribPropInterface = Ꮡc.getPropertiesOfType(jsxElementAttribPropInterfaceType);
            if (len(propertiesOfJsxElementAttribPropInterface) == 0) {
                return "";
            }
            if (len(propertiesOfJsxElementAttribPropInterface) == 1) {
                return propertiesOfJsxElementAttribPropInterface[0].Name;
            }
            if (len(propertiesOfJsxElementAttribPropInterface) > 1 && len(jsxElementAttribPropInterfaceSym.Declarations) != 0) {
                Ꮡc.error(jsxElementAttribPropInterfaceSym.Declarations[0], diagnostics.The_global_type_JSX_0_may_not_have_more_than_one_property, nameOfAttribPropContainer);
            }
        }
    }
    return ast.InternalSymbolNameMissing;
}

internal static ж<Type> getStaticTypeOfReferencedJsxConstructor(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡcontext) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var context = ref Ꮡcontext.DerefOrNull();

    if (ast.IsJsxOpeningFragment(Ꮡcontext)) {
        return Ꮡc.getJSXFragmentType(Ꮡcontext);
    }
    if (isJsxIntrinsicTagName(context.TagName())) {
        var result = Ꮡc.getIntrinsicAttributesTypeFromJsxOpeningLikeElement(Ꮡcontext);
        var fakeSignature = Ꮡc.createSignatureForJSXIntrinsic(Ꮡcontext, result);
        return Ꮡc.getOrCreateTypeFromSignature(fakeSignature);
    }
    var tagType = Ꮡc.checkExpressionCached(context.TagName());
    if ((TypeFlags)(tagType.flags & TypeFlagsStringLiteral) != 0) {
        var result = Ꮡc.getIntrinsicAttributesTypeFromStringLiteralType(tagType, Ꮡcontext);
        if (result == nil) {
            return c.errorType;
        }
        var fakeSignature = Ꮡc.createSignatureForJSXIntrinsic(Ꮡcontext, result);
        return Ꮡc.getOrCreateTypeFromSignature(fakeSignature);
    }
    return tagType;
}

internal static ж<Type> getIntrinsicAttributesTypeFromStringLiteralType(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> location) {
    ref var c = ref Ꮡc.DerefOrNull();

    var intrinsicElementsType = Ꮡc.getJsxType(JsxNames.IntrinsicElements, location);
    if (!c.isErrorType(intrinsicElementsType)) {
        @string stringLiteralTypeName = getStringLiteralValue(t);
        var intrinsicProp = Ꮡc.getPropertyOfType(intrinsicElementsType, stringLiteralTypeName);
        if (intrinsicProp != nil) {
            return Ꮡc.getTypeOfSymbol(intrinsicProp);
        }
        var indexSignatureType = Ꮡc.getIndexTypeOfType(intrinsicElementsType, c.stringType);
        if (indexSignatureType != nil) {
            return indexSignatureType;
        }
        return default;
    }
    return c.anyType;
}

internal static JsxReferenceKind getJsxReferenceKind(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isJsxIntrinsicTagName(node.TagName())) {
        return JsxReferenceKindMixed;
    }
    var tagType = Ꮡc.getApparentType(Ꮡc.checkExpression(node.TagName()));
    if (len(Ꮡc.getSignaturesOfType(tagType, SignatureKindConstruct)) != 0) {
        return JsxReferenceKindComponent;
    }
    if (len(Ꮡc.getSignaturesOfType(tagType, SignatureKindCall)) != 0) {
        return JsxReferenceKindFunction;
    }
    return JsxReferenceKindMixed;
}

internal static ж<Signature> createSignatureForJSXIntrinsic(this ж<Checker> Ꮡc, ж<ast.Node> node, ж<Type> result) {
    ref var c = ref Ꮡc.DerefOrNull();

    var elementType = c.errorType;
    var @namespace = Ꮡc.getJsxNamespaceAt(node); if (@namespace != nil) {
        var typeSymbol = Ꮡc.getSymbol(Ꮡc.getExportsOfSymbol(@namespace), JsxNames.Element, ast.SymbolFlagsType); if (typeSymbol != nil) {
            elementType = Ꮡc.getDeclaredTypeOfSymbol(typeSymbol);
        }
    }
    var parameterSymbol = c.newSymbol(ast.SymbolFlagsFunctionScopedVariable, "props");
    c.valueSymbolLinks.Get(parameterSymbol).resolvedType = result;
    return c.newSignature(SignatureFlagsNone, nil, default, nil, [parameterSymbol], elementType, nil, 1);
}

internal static ж<Type> getIntrinsicAttributesTypeFromJsxOpeningLikeElement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    debug.Assert(isJsxIntrinsicTagName(node.TagName()));
    var links = c.jsxElementLinks.Get(Ꮡnode);
    if (links.resolvedJsxElementAttributesType != nil) {
        return links.resolvedJsxElementAttributesType;
    }
    var symbol = Ꮡc.getIntrinsicTagSymbol(Ꮡnode);
    if ((JsxFlags)(links.jsxFlags & JsxFlagsIntrinsicNamedElement) != 0) {
        links.resolvedJsxElementAttributesType = core.OrElse(Ꮡc.getTypeOfSymbol(symbol), c.errorType);
        return links.resolvedJsxElementAttributesType;
    }
    if ((JsxFlags)(links.jsxFlags & JsxFlagsIntrinsicIndexedElement) != 0) {
        var indexInfo = Ꮡc.getApplicableIndexInfoForName(Ꮡc.getJsxType(JsxNames.IntrinsicElements, Ꮡnode), node.TagName().Text());
        if (indexInfo != nil) {
            links.resolvedJsxElementAttributesType = indexInfo.valueType;
            return links.resolvedJsxElementAttributesType;
        }
    }
    links.resolvedJsxElementAttributesType = c.errorType;
    return links.resolvedJsxElementAttributesType;
}

internal static ж<ast.Symbol> getIntrinsicTagSymbol(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var links = c.symbolNodeLinks.Get(Ꮡnode);
    if (links.resolvedSymbol != nil) {
        return links.resolvedSymbol;
    }
    var intrinsicElementsType = Ꮡc.getJsxType(JsxNames.IntrinsicElements, Ꮡnode);
    if (!c.isErrorType(intrinsicElementsType)) {
        var tagName = node.TagName();
        if (!ast.IsIdentifier(tagName) && !ast.IsJsxNamespacedName(tagName)) {
            throw panic("Invalid tag name");
        }
        @string propName = tagName.Text();
        var intrinsicProp = Ꮡc.getPropertyOfType(intrinsicElementsType, propName);
        if (intrinsicProp != nil) {
            c.jsxElementLinks.Get(Ꮡnode).jsxFlags |= (JsxFlags)(JsxFlagsIntrinsicNamedElement);
            links.resolvedSymbol = intrinsicProp;
            return links.resolvedSymbol;
        }
        var indexSymbol = Ꮡc.getApplicableIndexSymbol(intrinsicElementsType, Ꮡc.getStringLiteralType(propName));
        if (indexSymbol != nil) {
            c.jsxElementLinks.Get(Ꮡnode).jsxFlags |= (JsxFlags)(JsxFlagsIntrinsicIndexedElement);
            links.resolvedSymbol = indexSymbol;
            return links.resolvedSymbol;
        }
        if (Ꮡc.getTypeOfPropertyOrIndexSignatureOfType(intrinsicElementsType, propName) != nil) {
            c.jsxElementLinks.Get(Ꮡnode).jsxFlags |= (JsxFlags)(JsxFlagsIntrinsicIndexedElement);
            links.resolvedSymbol = intrinsicElementsType.symbol;
            return links.resolvedSymbol;
        }
        Ꮡc.error(Ꮡnode, diagnostics.Property_0_does_not_exist_on_type_1, tagName.Text(), "JSX." + JsxNames.IntrinsicElements);
        links.resolvedSymbol = c.unknownSymbol;
        return links.resolvedSymbol;
    }
    if (c.noImplicitAny) {
        Ꮡc.error(Ꮡnode, diagnostics.JSX_element_implicitly_has_type_any_because_no_interface_JSX_0_exists, JsxNames.IntrinsicElements);
    }
    links.resolvedSymbol = c.unknownSymbol;
    return links.resolvedSymbol;
}

internal static ж<Type> getJsxStatelessElementTypeAt(this ж<Checker> Ꮡc, ж<ast.Node> location) {
    ref var c = ref Ꮡc.DerefOrNull();

    var jsxElementType = Ꮡc.getJsxElementTypeAt(location);
    if (jsxElementType == nil) {
        return default;
    }
    return Ꮡc.getUnionType([jsxElementType, c.nullType]);
}

internal static ж<Type> getJsxElementClassTypeAt(this ж<Checker> Ꮡc, ж<ast.Node> location) {
    ref var c = ref Ꮡc.DerefOrNull();

    var t = Ꮡc.getJsxType(JsxNames.ElementClass, location);
    if (c.isErrorType(t)) {
        return default;
    }
    return t;
}

internal static ж<Type> getJsxElementTypeAt(this ж<Checker> Ꮡc, ж<ast.Node> location) {
    return Ꮡc.getJsxType(JsxNames.Element, location);
}

internal static ж<Type> getJsxElementTypeTypeAt(this ж<Checker> Ꮡc, ж<ast.Node> location) {
    ref var c = ref Ꮡc.DerefOrNull();

    var ns = Ꮡc.getJsxNamespaceAt(location);
    if (ns == nil) {
        return default;
    }
    var sym = Ꮡc.getJsxElementTypeSymbol(ns);
    if (sym == nil) {
        return default;
    }
    var t = Ꮡc.instantiateAliasOrInterfaceWithDefaults(sym, default, ast.IsInJSFile(location));
    if (t == nil || c.isErrorType(t)) {
        return default;
    }
    return t;
}

internal static ж<Type> getJsxType(this ж<Checker> Ꮡc, @string name, ж<ast.Node> location) {
    ref var c = ref Ꮡc.DerefOrNull();

    var @namespace = Ꮡc.getJsxNamespaceAt(location); if (@namespace != nil) {
        var exports = Ꮡc.getExportsOfSymbol(@namespace); if (exports != nil) {
            var typeSymbol = Ꮡc.getSymbol(exports, name, ast.SymbolFlagsType); if (typeSymbol != nil) {
                return Ꮡc.getDeclaredTypeOfSymbol(typeSymbol);
            }
        }
    }
    return c.errorType;
}

internal static ж<ast.Symbol> getJsxNamespaceAt(this ж<Checker> Ꮡc, ж<ast.Node> location) {
    ref var c = ref Ꮡc.DerefOrNull();

    ж<JsxElementLinks> links = default;
    if (location != nil) {
        links = c.jsxElementLinks.Get(location);
    }
    if (links != nil && links.jsxNamespace != nil && links.jsxNamespace != c.unknownSymbol) {
        return links.jsxNamespace;
    }
    if (links == nil || links.jsxNamespace != c.unknownSymbol) {
        var resolvedNamespace = Ꮡc.getJsxNamespaceContainerForImplicitImport(location);
        if (resolvedNamespace == nil || resolvedNamespace == c.unknownSymbol) {
            @string namespaceName = c.getJsxNamespace(location);
            resolvedNamespace = c.resolveName(location, namespaceName, ast.SymbolFlagsNamespace, nil, false, false);
        }
        if (resolvedNamespace != nil) {
            var candidate = Ꮡc.resolveSymbol(Ꮡc.getSymbol(Ꮡc.getExportsOfSymbol(Ꮡc.resolveSymbol(resolvedNamespace)), JsxNames.JSX, ast.SymbolFlagsNamespace));
            if (candidate != nil && candidate != c.unknownSymbol) {
                if (links != nil) {
                    links.jsxNamespace = candidate;
                }
                return candidate;
            }
        }
        if (links != nil) {
            links.jsxNamespace = c.unknownSymbol;
        }
    }
    var s = Ꮡc.resolveSymbol(c.getGlobalSymbol(JsxNames.JSX, ast.SymbolFlagsNamespace, nil));
    if (s == c.unknownSymbol) {
        return default;
    }
    return s;
}

internal static @string getJsxNamespace(this ref Checker c, ж<ast.Node> location) {
    if (location != nil) {
        var @file = ast.GetSourceFileOfNode(location);
        if (@file != nil) {
            var links = c.sourceFileLinks.Get(@file);
            if (ast.IsJsxOpeningFragment(location)) {
                if (links.localJsxFragmentNamespace != ""u8) {
                    return links.localJsxFragmentNamespace;
                }
                var jsxFragmentPragma = ast.GetPragmaFromSourceFile(@file, "jsxfrag");
                if (jsxFragmentPragma != nil) {
                    links.localJsxFragmentFactory = c.parseIsolatedEntityName(jsxFragmentPragma.Args["factory"].Value);
                    if (links.localJsxFragmentFactory != nil) {
                        links.localJsxFragmentNamespace = ast.GetFirstIdentifier(links.localJsxFragmentFactory).Text();
                        return links.localJsxFragmentNamespace;
                    }
                }
                var entity = c.getJsxFragmentFactoryEntity(location);
                if (entity != nil) {
                    links.localJsxFragmentFactory = entity;
                    links.localJsxFragmentNamespace = ast.GetFirstIdentifier(entity).Text();
                    return links.localJsxFragmentNamespace;
                }
            } else {
                @string localJsxNamespace = c.getLocalJsxNamespace(@file);
                if (localJsxNamespace != ""u8) {
                    links.localJsxNamespace = localJsxNamespace;
                    return links.localJsxNamespace;
                }
            }
        }
    }
    if (c._jsxNamespace == ""u8) {
        c._jsxNamespace = "React";
        if (c.compilerOptions.JsxFactory != ""u8) {
            c._jsxFactoryEntity = c.parseIsolatedEntityName(c.compilerOptions.JsxFactory);
            if (c._jsxFactoryEntity != nil) {
                c._jsxNamespace = ast.GetFirstIdentifier(c._jsxFactoryEntity).Text();
            }
        } else if (c.compilerOptions.ReactNamespace != ""u8) {
            c._jsxNamespace = c.compilerOptions.ReactNamespace;
        }
    }
    if (c._jsxFactoryEntity == nil) {
        c._jsxFactoryEntity = c.factory.NewQualifiedName(c.factory.NewIdentifier(c._jsxNamespace), c.factory.NewIdentifier("createElement"));
    }
    return c._jsxNamespace;
}

internal static @string getLocalJsxNamespace(this ref Checker c, ж<ast.SourceFile> @file) {
    var links = c.sourceFileLinks.Get(@file);
    if (links.localJsxNamespace != ""u8) {
        return links.localJsxNamespace;
    }
    var jsxPragma = ast.GetPragmaFromSourceFile(@file, "jsx");
    if (jsxPragma != nil) {
        links.localJsxFactory = c.parseIsolatedEntityName(jsxPragma.Args["factory"].Value);
        if (links.localJsxFactory != nil) {
            links.localJsxNamespace = ast.GetFirstIdentifier(links.localJsxFactory).Text();
            return links.localJsxNamespace;
        }
    }
    return "";
}

internal static ж<ast.Node> getJsxFactoryEntity(this ref Checker c, ж<ast.Node> location) {
    if (location != nil) {
        c.getJsxNamespace(location);
        var localJsxFactory = c.sourceFileLinks.Get(ast.GetSourceFileOfNode(location)).localJsxFactory; if (localJsxFactory != nil) {
            return localJsxFactory;
        }
    }
    return c._jsxFactoryEntity;
}

internal static ж<astꓸEntityName> getJsxFragmentFactoryEntity(this ref Checker c, ж<ast.Node> location) {
    if (location != nil) {
        var @file = ast.GetSourceFileOfNode(location);
        if (@file != nil) {
            var links = c.sourceFileLinks.Get(@file);
            if (links.localJsxFragmentFactory != nil) {
                return links.localJsxFragmentFactory;
            }
            var jsxFragPragma = ast.GetPragmaFromSourceFile(@file, "jsxfrag");
            if (jsxFragPragma != nil) {
                links.localJsxFragmentFactory = c.parseIsolatedEntityName(jsxFragPragma.Args["factory"].Value);
                return links.localJsxFragmentFactory;
            }
        }
    }
    if (c.compilerOptions.JsxFragmentFactory != ""u8) {
        return c.parseIsolatedEntityName(c.compilerOptions.JsxFragmentFactory);
    }
    return default;
}

internal static ж<ast.Node> parseIsolatedEntityName(this ref Checker c, @string name) {
    var result = parser.ParseIsolatedEntityName(name);
    if (result != nil) {
        markAsSynthetic(result);
    }
    return result;
}

} partial class checker_package {

internal static bool markAsSynthetic(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    node.Loc = core.NewTextRange(-1, -1);
    node.ForEachChild(new Func<ж<ast.Node>, bool>(markAsSynthetic));
    return false;
}

} partial class checkerᴛChecker {

internal static ж<ast.Symbol> getJsxNamespaceContainerForImplicitImport(this ж<Checker> Ꮡc, ж<ast.Node> location) {
    ref var c = ref Ꮡc.DerefOrNull();

    var @file = ast.GetSourceFileOfNode(location);
    var links = c.jsxElementLinks.Get(@file.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (links.jsxImplicitImportContainer != nil) {
        return core.IfElse(links.jsxImplicitImportContainer == c.unknownSymbol, (ж<ast.Symbol>)(nil), links.jsxImplicitImportContainer);
    }
    var canonicalErrorTag = links.firstJSXTagInFile;
    if (canonicalErrorTag == nil) {
        ref var visit = ref heap<Func<ж<ast.Node>, bool>>(out var Ꮡvisit);
        var linksʗ1 = links;
        visit = (ж<ast.Node> node) => {
            if (ast.IsJsxElement(node) || ast.IsJsxSelfClosingElement(node)) {
                linksʗ1.firstJSXTagInFile = node;
                return true;
            }
            if (ast.IsJsxFragment(node)) {
                linksʗ1.firstJSXTagInFile = node.AsJsxFragment().OpeningFragment;
                return true;
            }
            return node.ForEachChild(Ꮡvisit.ValueSlot);
        };
        @file.ForEachChild(visit);
        canonicalErrorTag = links.firstJSXTagInFile;
    }
    var (moduleReference, specifier) = c.getJSXRuntimeImportSpecifier(@file);
    if (moduleReference == ""u8) {
        return default;
    }
    var errorMessage = diagnostics.This_JSX_tag_requires_the_module_path_0_to_exist_but_none_could_be_found_Make_sure_you_have_types_for_the_appropriate_package_installed;
    var mod = Ꮡc.resolveExternalModule(core.OrElse(specifier, canonicalErrorTag), moduleReference, errorMessage, canonicalErrorTag, false, nil);
    ж<ast.Symbol> result = default;
    if (mod != nil && mod != c.unknownSymbol) {
        result = c.getMergedSymbol(Ꮡc.resolveSymbol(mod));
    }
    links.jsxImplicitImportContainer = core.OrElse(result, c.unknownSymbol);
    return result;
}

internal static (@string moduleReference, ж<ast.Node> specifier) getJSXRuntimeImportSpecifier(this ref Checker c, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    return c.program.GetJSXRuntimeImportSpecifier(@file.Path());
}

} // end checkerᴛChecker
