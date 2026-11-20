namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strconv = strconv_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using static transformers_package;

partial class transformers_package {

partial struct FlattenLevel /*num:nint*/;

public static FlattenLevel FlattenLevelAll => /* iota */ 0;
public static FlattenLevel FlattenLevelObjectRest => 1;

// type CreateAssignmentCallback is a methodless func type — rendered inline as its base delegate

public static ж<astꓸExpression> FlattenDestructuringAssignment(ж<Transformer> tx, ж<ast.Node> node, bool needsValue, FlattenLevel level, Func<ж<astꓸIdentifierNode>, ж<astꓸExpression>, ж<core.TextRange>, ж<astꓸExpression>> createAssignmentCallback) {
    var f = newFlattener(tx, level);
    f.createAssignmentCallback = createAssignmentCallback;
    f.hoistTempVariables = true;
    f.emitBindingOrAssignment = ((Action<ж<flattener>, ж<ast.Node>, ж<ast.Node>, core.TextRange, ж<ast.Node>>)(transformersᴛflattener.emitAssignment));
    f.createArrayBindingOrAssignmentPattern = ((Func<ж<flattener>, slice<ж<ast.Node>>, ж<ast.Node>>)(transformersᴛflattener.createArrayAssignmentPattern));
    f.createObjectBindingOrAssignmentPattern = ((Func<ж<flattener>, slice<ж<ast.Node>>, ж<ast.Node>>)(transformersᴛflattener.createObjectAssignmentPattern));
    f.createArrayBindingOrAssignmentElement = ((Func<ж<flattener>, ж<ast.Node>, ж<ast.Node>>)(transformersᴛflattener.createArrayAssignmentElement));
    return f.flattenDestructuringAssignment(node, needsValue);
}

partial struct pendingDecl {
    internal slice<ж<ast.Node>> pendingExpressions;
    internal ж<ast.Node> name;
    internal ж<ast.Node> value;
    internal core.TextRange location;
    internal ж<ast.Node> original;
}

public static ж<ast.Node> FlattenDestructuringBinding(ж<Transformer> tx, ж<ast.Node> node, ж<ast.Node> rval, FlattenLevel level, bool hoistTempVariables, bool skipInitializer) {
    var f = newFlattener(tx, level);
    f.hoistTempVariables = hoistTempVariables;
    f.emitBindingOrAssignment = ((Action<ж<flattener>, ж<ast.Node>, ж<ast.Node>, core.TextRange, ж<ast.Node>>)(transformersᴛflattener.emitBinding));
    f.createArrayBindingOrAssignmentPattern = ((Func<ж<flattener>, slice<ж<ast.Node>>, ж<ast.Node>>)(transformersᴛflattener.createArrayBindingPattern));
    f.createObjectBindingOrAssignmentPattern = ((Func<ж<flattener>, slice<ж<ast.Node>>, ж<ast.Node>>)(transformersᴛflattener.createObjectBindingPattern));
    f.createArrayBindingOrAssignmentElement = ((Func<ж<flattener>, ж<ast.Node>, ж<ast.Node>>)(transformersᴛflattener.createArrayBindingElement));
    return f.flattenDestructuringBinding(node, rval, skipInitializer);
}

partial struct flattener {
    internal ж<Transformer> tx;
    internal FlattenLevel level;
    internal Func<ж<astꓸIdentifierNode>, ж<astꓸExpression>, ж<core.TextRange>, ж<astꓸExpression>> createAssignmentCallback;
    internal slice<ж<ast.Node>> expressions;
    internal slice<pendingDecl> declarations;
    internal bool hasTransformedPriorElement;
    internal bool hoistTempVariables;
    internal Action<ж<flattener>, ж<ast.Node>, ж<ast.Node>, core.TextRange, ж<ast.Node>> emitBindingOrAssignment;
    internal Func<ж<flattener>, slice<ж<ast.Node>>, ж<ast.Node>> createArrayBindingOrAssignmentPattern;
    internal Func<ж<flattener>, slice<ж<ast.Node>>, ж<ast.Node>> createObjectBindingOrAssignmentPattern;
    internal Func<ж<flattener>, ж<ast.Node>, ж<ast.Node>> createArrayBindingOrAssignmentElement;
}

internal static ж<flattener> newFlattener(ж<Transformer> tx, FlattenLevel level) {
    return Ꮡ(new flattener(
        tx: tx,
        level: level
    ));
}

} partial class transformersᴛflattener {

internal static ж<ast.Node> createArrayAssignmentPattern(this ref flattener f, slice<ж<ast.Node>> elements) {
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(elements), false);
}

internal static ж<ast.Node> createObjectAssignmentPattern(this ref flattener f, slice<ж<ast.Node>> elements) {
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(elements), false);
}

internal static ж<ast.Node> createArrayAssignmentElement(this ref flattener f, ж<ast.Node> expr) {
    return expr;
}

internal static void emitAssignment(this ref flattener f, ж<ast.Node> target, ж<ast.Node> value, core.TextRange locationʗp, ж<ast.Node> original) {
    ref var location = ref heap(locationʗp, out var Ꮡlocation);

    ж<astꓸExpression> expression = default;
    if (f.createAssignmentCallback != nil && ast.IsIdentifier(target)) {
        expression = f.createAssignmentCallback(target, value, Ꮡlocation);
    } else {
        expression = f.tx.Factory().NewAssignmentExpression(f.tx.Visitor().VisitNode(target), value);
        expression.Loc = location;
    }
    f.tx.EmitContext().SetOriginal(expression, original);
    f.emitExpression(expression);
}

internal static ж<ast.Node> createArrayBindingPattern(this ref flattener f, slice<ж<ast.Node>> elements) {
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBindingPattern(ast.KindArrayBindingPattern, f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(elements));
}

internal static ж<ast.Node> createObjectBindingPattern(this ref flattener f, slice<ж<ast.Node>> elements) {
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBindingPattern(ast.KindObjectBindingPattern, f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(elements));
}

internal static ж<ast.Node> createArrayBindingElement(this ref flattener f, ж<ast.Node> expr) {
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBindingElement(nil, nil, expr, nil);
}

internal static void emitBinding(this ref flattener f, ж<ast.Node> target, ж<ast.Node> Ꮡvalue, core.TextRange location, ж<ast.Node> original) {
    ref var value = ref Ꮡvalue.DerefOrNull();

    if (len(f.expressions) > 0) {
        Ꮡvalue = f.tx.Factory().InlineExpressions(append(f.expressions, Ꮡvalue)); value = ref Ꮡvalue.DerefOrNull();
        f.expressions = default;
    }
    f.declarations = append(f.declarations, new pendingDecl(
        name: target,
        value: Ꮡvalue,
        location: location,
        original: original
    ));
}

internal static void emitExpression(this ref flattener f, ж<ast.Node> expr) {
    f.expressions = append(f.expressions, expr);
}

internal static ж<ast.Node> ensureIdentifier(this ж<flattener> Ꮡf, ж<ast.Node> value, bool reuseIdentifierExpressions, core.TextRange location) {
    ref var f = ref Ꮡf.DerefOrNull();

    if (reuseIdentifierExpressions && ast.IsIdentifier(value)) {
        return value;
    }
    var temp = f.tx.Factory().NewTempVariable();
    if (f.hoistTempVariables) {
        f.tx.EmitContext().AddVariableDeclaration(temp);
        var assign = f.tx.Factory().NewAssignmentExpression(temp, value);
        assign.Loc = location;
        f.emitExpression(assign);
    } else {
        f.emitBindingOrAssignment(Ꮡf, temp, value, location, nil);
    }
    return temp;
}

internal static ж<ast.Node> createDefaultValueCheck(this ж<flattener> Ꮡf, ж<astꓸExpression> Ꮡvalue, ж<astꓸExpression> defaultValue, core.TextRange location) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var value = ref Ꮡvalue.DerefOrNull();

    Ꮡvalue = Ꮡf.ensureIdentifier(Ꮡvalue, true, location); value = ref Ꮡvalue.DerefOrNull();
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
        f.tx.Factory().NewTypeCheck(Ꮡvalue, "undefined"),
        f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
        defaultValue,
        f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
        Ꮡvalue);
}

internal static ж<ast.Node> createDestructuringPropertyAccess(this ж<flattener> Ꮡf, ж<ast.Node> value, ж<ast.Node> ᏑpropertyName) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var propertyName = ref ᏑpropertyName.DerefOrNull();

    if (ast.IsComputedPropertyName(ᏑpropertyName)) {
        var argumentExpression = Ꮡf.ensureIdentifier(f.tx.Visitor().VisitNode(propertyName.Expression()), false, propertyName.Loc);
        return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(value, nil, argumentExpression, ast.NodeFlagsNone);
    } else if (ast.IsStringOrNumericLiteralLike(ᏑpropertyName) || ast.IsBigIntLiteral(ᏑpropertyName)) {
        var argumentExpression = propertyName.Clone(new printer.NodeFactoryжNodeFactoryCoercible(f.tx.Factory()));
        return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(value, nil, argumentExpression, ast.NodeFlagsNone);
    } else {
        var name = f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(propertyName.Text());
        return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(value, nil, name, ast.NodeFlagsNone);
    }
}

internal static ж<astꓸExpression> flattenDestructuringAssignment(this ж<flattener> Ꮡf, ж<ast.Node> Ꮡnode, bool needsValue) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var location = node.Loc;
    ж<ast.Node> value = default;
    if (ast.IsDestructuringAssignment(Ꮡnode)) {
        value = node.AsBinaryExpression().Right;
        while (ast.IsEmptyArrayLiteral(node.AsBinaryExpression().Left) || ast.IsEmptyObjectLiteral(node.AsBinaryExpression().Left)) {
            if (ast.IsDestructuringAssignment(value)) {
                Ꮡnode = value; node = ref Ꮡnode.DerefOrNull();
                location = node.Loc;
                value = node.AsBinaryExpression().Right;
            } else {
                return f.tx.Visitor().VisitNode(value);
            }
        }
    }

    if (value != nil) {
        value = f.tx.Visitor().VisitNode(value);
        if (ast.IsIdentifier(value) && BindingOrAssignmentElementAssignsToName(Ꮡnode, value.Text()) || BindingOrAssignmentElementContainsNonLiteralComputedName(Ꮡnode)) {
            value = Ꮡf.ensureIdentifier(value, false, location);
        } else if (needsValue) {
            value = Ꮡf.ensureIdentifier(value, true, location);
        } else if (ast.NodeIsSynthesized(Ꮡnode)) {
            location = value.Loc;
        }
    }

    Ꮡf.flattenBindingOrAssignmentElement(Ꮡnode, value, location, ast.IsDestructuringAssignment(Ꮡnode));

    if (value != nil && needsValue) {
        if (len(f.expressions) == 0) {
            return value;
        }
        f.expressions = append(f.expressions, value);
    }

    var res = f.tx.Factory().InlineExpressions(f.expressions);
    if (res != nil) {
        return res;
    }
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewOmittedExpression();
}

internal static ж<ast.Node> flattenDestructuringBinding(this ж<flattener> Ꮡf, ж<ast.Node> Ꮡnode, ж<ast.Node> rval, bool skipInitializer) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsVariableDeclaration(Ꮡnode)) {
        var initializer = GetInitializerOfBindingOrAssignmentElement(Ꮡnode);
        if (initializer != nil && (ast.IsIdentifier(initializer) && BindingOrAssignmentElementAssignsToName(Ꮡnode, initializer.Text()) || BindingOrAssignmentElementContainsNonLiteralComputedName(Ꮡnode))) {
            initializer = Ꮡf.ensureIdentifier(f.tx.Visitor().VisitNode(initializer), false, initializer.Loc);
            Ꮡnode = f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateVariableDeclaration(node.AsVariableDeclaration(), node.Name(), nil, nil, initializer); node = ref Ꮡnode.DerefOrNull();
        }
    }

    Ꮡf.flattenBindingOrAssignmentElement(Ꮡnode, rval, node.Loc, skipInitializer);

    if (len(f.expressions) > 0) {
        var temp = f.tx.Factory().NewTempVariable();
        if (f.hoistTempVariables) {
            var value = f.tx.Factory().InlineExpressions(f.expressions);
            f.expressions = default;
            f.emitBindingOrAssignment(Ꮡf, temp, value, new core.TextRange(), nil);
        } else {
            f.tx.EmitContext().AddVariableDeclaration(temp);
            var last = Ꮡ(f.declarations, len(f.declarations) - 1);
            last.pendingExpressions = append(last.pendingExpressions, f.tx.Factory().NewAssignmentExpression(temp, last.value));
            last.pendingExpressions = appendꓸꓸꓸ(last.pendingExpressions, f.expressions);
            last.value = temp;
        }
    }

    var decls = new slice<ж<ast.Node>>(0, len(f.declarations));
    foreach (var (_, pending) in range(f.declarations)) {
        var expr = pending.value;
        if (len(pending.pendingExpressions) > 0) {
            expr = f.tx.Factory().InlineExpressions(append(pending.pendingExpressions, pending.value));
        }
        var decl = f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(pending.name, nil, nil, expr);
        decl.Loc = pending.location;
        if (pending.original != nil) {
            f.tx.EmitContext().SetOriginal(decl, pending.original);
        }
        decls = append(decls, decl);
    }

    if (len(decls) == 1) {
        return decls[0];
    }
    if (len(decls) == 0) {
        return default;
    }
    return f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(decls);
}

internal static void flattenBindingOrAssignmentElement(this ж<flattener> Ꮡf, ж<ast.Node> element, ж<ast.Node> Ꮡvalue, core.TextRange location, bool skipInitializer) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var value = ref Ꮡvalue.DerefOrNull();

    var bindingTarget = ast.GetTargetOfBindingOrAssignmentElement(element);
    if (bindingTarget == nil) {
        return;
    }
    if (!skipInitializer) {
        var initializer = f.tx.Visitor().VisitNode(GetInitializerOfBindingOrAssignmentElement(element));
        if (initializer != nil) {
            if (Ꮡvalue != nil) {
                Ꮡvalue = Ꮡf.createDefaultValueCheck(Ꮡvalue, initializer, location); value = ref Ꮡvalue.DerefOrNull();
                if (!IsSimpleCopiableExpression(initializer) && (ast.IsBindingPattern(bindingTarget) || ast.IsAssignmentPattern(bindingTarget))) {
                    Ꮡvalue = Ꮡf.ensureIdentifier(Ꮡvalue, true, location); value = ref Ꮡvalue.DerefOrNull();
                }
            } else {
                Ꮡvalue = initializer; value = ref Ꮡvalue.DerefOrNull();
            }
        } else if (Ꮡvalue == nil) {
            Ꮡvalue = f.tx.Factory().NewVoidZeroExpression(); value = ref Ꮡvalue.DerefOrNull();
        }
    }

    if (isObjectBindingOrAssignmentPattern(bindingTarget)) {
        Ꮡf.flattenObjectBindingOrAssignmentPattern(element, bindingTarget, Ꮡvalue, location);
    } else if (isArrayBindingOrAssignmentPattern(bindingTarget)) {
        Ꮡf.flattenArrayBindingOrAssignmentPattern(element, bindingTarget, Ꮡvalue, location);
    } else {
        f.emitBindingOrAssignment(Ꮡf, bindingTarget, Ꮡvalue, location, element);
    }
}

internal static void flattenObjectBindingOrAssignmentPattern(this ж<flattener> Ꮡf, ж<ast.Node> parent, ж<ast.Node> Ꮡpattern, ж<ast.Node> Ꮡvalue, core.TextRange location) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var pattern = ref Ꮡpattern.DerefOrNull();
    ref var value = ref Ꮡvalue.DerefOrNull();
    var computedTempVariablesᴷ = new stackAppend<ж<ast.Node>>();

    var elements = ast.GetElementsOfBindingOrAssignmentPattern(Ꮡpattern);
    nint numElements = len(elements);
    if (numElements != 1) {
        var reuseIdentifierExpressions = !ast.IsDeclarationBindingElement(parent) || numElements != 0;
        Ꮡvalue = Ꮡf.ensureIdentifier(Ꮡvalue, reuseIdentifierExpressions, location); value = ref Ꮡvalue.DerefOrNull();
    }
    slice<ж<ast.Node>> bindingElements = default;
    slice<ж<ast.Node>> computedTempVariables = default;
    foreach (var (i, element) in range(elements)) {
        if (ast.GetRestIndicatorOfBindingOrAssignmentElement(element) == nil) {
            var propertyName = ast.TryGetPropertyNameOfBindingOrAssignmentElement(element);
            if (f.level >= FlattenLevelObjectRest && (ast.SubtreeFacts)(element.SubtreeFacts() & ((ast.SubtreeFacts)(ast.SubtreeContainsRestOrSpread | ast.SubtreeContainsObjectRestOrSpread))) == 0 && (ast.SubtreeFacts)(ast.GetTargetOfBindingOrAssignmentElement(element).SubtreeFacts() & ((ast.SubtreeFacts)(ast.SubtreeContainsRestOrSpread | ast.SubtreeContainsObjectRestOrSpread))) == 0 && !ast.IsComputedPropertyName(propertyName)) {
                bindingElements = append(bindingElements, f.tx.Visitor().VisitNode(element));
            } else {
                if (len(bindingElements) > 0) {
                    f.emitBindingOrAssignment(Ꮡf, f.createObjectBindingOrAssignmentPattern(Ꮡf, bindingElements), Ꮡvalue, location, Ꮡpattern);
                    bindingElements = default;
                }
                var rhsValue = Ꮡf.createDestructuringPropertyAccess(Ꮡvalue, propertyName);
                if (ast.IsComputedPropertyName(propertyName)) {
                    computedTempVariables = append(computedTempVariablesᴷ, computedTempVariables, rhsValue.AsElementAccessExpression().ArgumentExpression);
                }
                Ꮡf.flattenBindingOrAssignmentElement(element, rhsValue, element.Loc, false);
            }
        } else if (i == numElements - 1) {
            if (len(bindingElements) > 0) {
                f.emitBindingOrAssignment(Ꮡf, f.createObjectBindingOrAssignmentPattern(Ꮡf, bindingElements), Ꮡvalue, location, Ꮡpattern);
                bindingElements = default;
            }
            var rhsValue = f.tx.Factory().NewRestHelper(Ꮡvalue, elements, computedTempVariables, pattern.Loc);
            Ꮡf.flattenBindingOrAssignmentElement(element, rhsValue, element.Loc, false);
        }
    }
    if (len(bindingElements) > 0) {
        f.emitBindingOrAssignment(Ꮡf, f.createObjectBindingOrAssignmentPattern(Ꮡf, bindingElements), Ꮡvalue, location, Ꮡpattern);
    }
}

} partial class transformers_package {

partial struct restIdElemPair {
    internal ж<ast.Node> id;
    internal ж<ast.Node> element;
}

} partial class transformersᴛflattener {

internal static void flattenArrayBindingOrAssignmentPattern(this ж<flattener> Ꮡf, ж<ast.Node> parent, ж<ast.Node> pattern, ж<ast.Node> Ꮡvalue, core.TextRange location) {
    ref var f = ref Ꮡf.DerefOrNull();
    ref var value = ref Ꮡvalue.DerefOrNull();
    var restContainingElementsᴷ = new stackAppend<restIdElemPair>(moves: true);

    var elements = ast.GetElementsOfBindingOrAssignmentPattern(pattern);
    nint numElements = len(elements);
    if (numElements != 1 && (f.level < FlattenLevelObjectRest || numElements == 0) || core.Every<ж<ast.Node>>(elements, ast.IsOmittedExpression)) {
        var reuseIdentifierExpressions = !ast.IsDeclarationBindingElement(parent) || numElements != 0;
        Ꮡvalue = Ꮡf.ensureIdentifier(Ꮡvalue, reuseIdentifierExpressions, location); value = ref Ꮡvalue.DerefOrNull();
    }
    slice<ж<ast.Node>> bindingElements = default;
    slice<restIdElemPair> restContainingElements = default;
    foreach (var (i, element) in range(elements)) {
        if (f.level >= FlattenLevelObjectRest) {
            if ((ast.SubtreeFacts)(element.SubtreeFacts() & ast.SubtreeContainsObjectRestOrSpread) != 0 || f.hasTransformedPriorElement && !isSimpleBindingOrAssignmentElement(element)) {
                f.hasTransformedPriorElement = true;
                var temp = f.tx.Factory().NewTempVariable();
                if (f.hoistTempVariables) {
                    f.tx.EmitContext().AddVariableDeclaration(temp);
                }
                restContainingElements = append(restContainingElementsᴷ, restContainingElements, new restIdElemPair(temp, element));
                bindingElements = append(bindingElements, f.createArrayBindingOrAssignmentElement(Ꮡf, temp));
            } else {
                bindingElements = append(bindingElements, element);
            }
        } else if (ast.IsOmittedExpression(element)) {
            continue;
        } else if (ast.GetRestIndicatorOfBindingOrAssignmentElement(element) == nil) {
            var rhsValue = f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(Ꮡvalue, nil, f.tx.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(strconv.Itoa(i), ast.TokenFlagsNone), ast.NodeFlagsNone);
            Ꮡf.flattenBindingOrAssignmentElement(element, rhsValue, element.Loc, false);
        } else if (i == numElements - 1) {
            var rhsValue = f.tx.Factory().NewArraySliceCall(Ꮡvalue, i);
            Ꮡf.flattenBindingOrAssignmentElement(element, rhsValue, element.Loc, false);
        }
    }
    if (len(bindingElements) > 0) {
        f.emitBindingOrAssignment(Ꮡf, f.createArrayBindingOrAssignmentPattern(Ꮡf, bindingElements), Ꮡvalue, location, pattern);
    }
    if (len(restContainingElements) > 0) {
        restContainingElements = move2heap(restContainingElementsᴷ, restContainingElements);
        foreach (var (_, pair) in range(restContainingElements)) {
            Ꮡf.flattenBindingOrAssignmentElement(pair.element, pair.id, pair.element.Loc, false);
        }
    }
}

} partial class transformers_package {

public static bool BindingOrAssignmentElementAssignsToName(ж<ast.Node> element, @string name) {
    var target = ast.GetTargetOfBindingOrAssignmentElement(element);
    if (target == nil) {
        return false;
    }
    if (ast.IsBindingPattern(target) || ast.IsAssignmentPattern(target)) {
        return bindingOrAssignmentPatternAssignsToName(target, name);
    } else if (ast.IsIdentifier(target)) {
        return target.Text() == name;
    }
    return false;
}

internal static bool bindingOrAssignmentPatternAssignsToName(ж<ast.Node> pattern, @string name) {
    var elements = ast.GetElementsOfBindingOrAssignmentPattern(pattern);
    foreach (var (_, element) in range(elements)) {
        if (BindingOrAssignmentElementAssignsToName(element, name)) {
            return true;
        }
    }
    return false;
}

public static bool BindingOrAssignmentElementContainsNonLiteralComputedName(ж<ast.Node> element) {
    var propertyName = ast.TryGetPropertyNameOfBindingOrAssignmentElement(element);
    if (propertyName != nil && ast.IsComputedPropertyName(propertyName) && !ast.IsLiteralExpression(propertyName.Expression())) {
        return true;
    }
    var target = ast.GetTargetOfBindingOrAssignmentElement(element);
    return target != nil && (ast.IsBindingPattern(target) || ast.IsAssignmentPattern(target)) && bindingOrAssignmentPatternContainsNonLiteralComputedName(target);
}

internal static bool bindingOrAssignmentPatternContainsNonLiteralComputedName(ж<ast.Node> pattern) {
    var elements = ast.GetElementsOfBindingOrAssignmentPattern(pattern);
    return slices.ContainsFunc<slice<ж<ast.Node>>, ж<ast.Node>>(elements, BindingOrAssignmentElementContainsNonLiteralComputedName);
}

public static ж<ast.Node> GetInitializerOfBindingOrAssignmentElement(ж<ast.Node> ᏑbindingElement) {
    ref var bindingElement = ref ᏑbindingElement.DerefOrNull();

    if (ᏑbindingElement == nil) {
        return default;
    }
    if (ast.IsDeclarationBindingElement(ᏑbindingElement)) {
        return bindingElement.Initializer();
    }
    if (ast.IsPropertyAssignment(ᏑbindingElement)) {
        var initializer = bindingElement.Initializer();
        if (ast.IsAssignmentExpression(initializer, true)) {
            return initializer.AsBinaryExpression().Right;
        }
        return default;
    }
    if (ast.IsShorthandPropertyAssignment(ᏑbindingElement)) {
        return bindingElement.AsShorthandPropertyAssignment().ObjectAssignmentInitializer;
    }
    if (ast.IsAssignmentExpression(ᏑbindingElement, true)) {
        return bindingElement.AsBinaryExpression().Right;
    }
    if (ast.IsSpreadElement(ᏑbindingElement)) {
        return GetInitializerOfBindingOrAssignmentElement(bindingElement.Expression());
    }
    return default;
}

internal static bool isObjectBindingOrAssignmentPattern(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && (node.Kind == ast.KindObjectBindingPattern || node.Kind == ast.KindObjectLiteralExpression);
}

internal static bool isArrayBindingOrAssignmentPattern(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && (node.Kind == ast.KindArrayBindingPattern || node.Kind == ast.KindArrayLiteralExpression);
}

internal static bool isSimpleBindingOrAssignmentElement(ж<ast.Node> element) {
    var target = ast.GetTargetOfBindingOrAssignmentElement(element);
    if (target == nil || ast.IsOmittedExpression(target)) {
        return true;
    }
    var propertyName = ast.TryGetPropertyNameOfBindingOrAssignmentElement(element);
    if (propertyName != nil && !ast.IsPropertyNameLiteral(propertyName)) {
        return false;
    }
    var initializer = GetInitializerOfBindingOrAssignmentElement(element);
    if (initializer != nil && !IsSimpleInlineableExpression(initializer)) {
        return false;
    }
    if (ast.IsBindingPattern(target) || ast.IsAssignmentPattern(target)) {
        return core.Every<ж<ast.Node>>(ast.GetElementsOfBindingOrAssignmentPattern(target), isSimpleBindingOrAssignmentElement);
    }
    return ast.IsIdentifier(target);
}

} // end transformers_package
