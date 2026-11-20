namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static tstransforms_package;

partial class tstransforms_package {

partial struct LegacyDecoratorsTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal core.ScriptTarget languageVersion;
    internal binder.ReferenceResolver referenceResolver;
    internal map<ж<ast.Node>, ж<ast.Node>> classAliases;
    internal slice<ж<ast.ClassDeclaration>> enclosingClasses;
}

public static ж<transformers.Transformer> NewLegacyDecoratorsTransformer(ж<transformers.TransformOptions> Ꮡopt) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    var tx = Ꮡ(new LegacyDecoratorsTransformer(languageVersion: opt.CompilerOptions.GetEmitScriptTarget(), referenceResolver: opt.Resolver));
    return tx.of(LegacyDecoratorsTransformer.ᏑTransformer).NewTransformer(tx.visit, opt.Context);
}

} partial class tstransformsᴛLegacyDecoratorsTransformer {

internal static ж<ast.Node> visit(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsDecorators)) == 0 && len(tx.enclosingClasses) == 0) {
        return Ꮡnode;
    }

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        return tx.visitIdentifier(node.AsIdentifier());
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        return tx.visitPropertyAccessExpression(node.AsPropertyAccessExpression());
    }
    case var ᴛ when ᴛ == ast.KindDecorator: {
        return default;
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration: {
        return Ꮡtx.visitClassDeclaration(node.AsClassDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindClassExpression: {
        return tx.visitClassExpression(node.AsClassExpression());
    }
    case var ᴛ when ᴛ == ast.KindConstructor: {
        return tx.visitConstructorDeclaration(node.AsConstructorDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        return tx.visitMethodDeclaration(node.AsMethodDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        return tx.visitSetAccessorDeclaration(node.AsSetAccessorDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        return tx.visitGetAccessorDeclaration(node.AsGetAccessorDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return tx.visitPropertyDeclaration(node.AsPropertyDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        return tx.visitParamerDeclaration(node.AsParameterDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        tx.classAliases = new map<ж<ast.Node>, ж<ast.Node>>();
        tx.enclosingClasses = default;
        var result = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
        Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().AddEmitHelper(result, Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
        tx.classAliases = default;
        tx.enclosingClasses = default;
        return result;
    }
    default: {
        return Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> visitIdentifier(this ref LegacyDecoratorsTransformer tx, ж<ast.Identifier> node) {
    foreach (var (_, d) in range(tx.enclosingClasses)) {
        var (_, ok) = tx.classAliases[d.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ꟷ]; if (ok && tx.referenceResolver.GetReferencedValueDeclaration(tx.Transformer.EmitContext().MostOriginal(node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) == tx.Transformer.EmitContext().MostOriginal(d.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            return tx.classAliases[d.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()];
        }
    }
    return node.of(ast.Identifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

internal static ж<ast.Node> visitPropertyAccessExpression(this ref LegacyDecoratorsTransformer tx, ж<ast.PropertyAccessExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var expression = tx.Transformer.Visitor().VisitNode(node.Expression);
    if (expression != node.Expression) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAccessExpression(Ꮡnode, expression, node.QuestionDotToken, node.Name(), node.Flags);
    }
    return Ꮡnode.of(ast.PropertyAccessExpression.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
}

} partial class tstransforms_package {

internal static ж<ast.NodeList> elideNodes(ж<printer.NodeFactory> f, ж<ast.NodeList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil) {
        return default;
    }
    if (len(nodes.Nodes) == 0) {
        return Ꮡnodes;
    }
    var replacement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]);
    replacement.Loc = nodes.Loc;
    return replacement;
}

internal static ж<ast.ModifierList> elideModifiers(ж<printer.NodeFactory> f, ж<ast.ModifierList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil) {
        return default;
    }
    if (len(nodes.Nodes) == 0) {
        return Ꮡnodes;
    }
    var replacement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList([]);
    replacement.Loc = nodes.Loc;
    return replacement;
}

} partial class tstransformsᴛLegacyDecoratorsTransformer {

internal static ж<ast.Node> finishClassElement(this ref LegacyDecoratorsTransformer tx, ж<ast.Node> updated, ж<ast.Node> Ꮡoriginal) {
    ref var original = ref Ꮡoriginal.DerefOrNull();

    if (updated != Ꮡoriginal) {
        tx.Transformer.EmitContext().SetCommentRange(updated, original.Loc);
        tx.Transformer.EmitContext().SetSourceMapRange(updated, transformers.MoveRangePastModifiers(Ꮡoriginal));
    }
    return updated;
}

internal static ж<ast.Node> visitParamerDeclaration(this ref LegacyDecoratorsTransformer tx, ж<ast.ParameterDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
        Ꮡnode,
        elideModifiers(tx.Transformer.Factory(), Ꮡnode.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers()),
        node.DotDotDotToken,
        tx.Transformer.Visitor().VisitNode(node.Name()),
        nil,
        nil,
        tx.Transformer.Visitor().VisitNode(node.Initializer));
    if (updated != Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) {
        tx.Transformer.EmitContext().SetCommentRange(updated, node.Loc);
        var newLoc = transformers.MoveRangePastModifiers(Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        updated.Loc = newLoc;
        tx.Transformer.EmitContext().SetSourceMapRange(updated, newLoc);
        tx.Transformer.EmitContext().SetEmitFlags(updated.Name(), printer.EFNoTrailingSourceMap);
    }
    return updated;
}

internal static ж<ast.Node> visitPropertyNameOfClassElement(this ref LegacyDecoratorsTransformer tx, ж<ast.Node> Ꮡmember) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var name = member.Name();
    if (ast.IsComputedPropertyName(name) && ast.HasDecorators(Ꮡmember)) {
        var expression = tx.Transformer.Visitor().VisitNode((~name.AsComputedPropertyName()).Expression);
        var innerExpression = ast.SkipPartiallyEmittedExpressions(expression);
        if (!transformers.IsSimpleInlineableExpression(innerExpression)) {
            var generatedName = tx.Transformer.Factory().NewGeneratedNameForNode(name);
            tx.Transformer.EmitContext().AddVariableDeclaration(generatedName);
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(name.AsComputedPropertyName(), tx.Transformer.Factory().NewAssignmentExpression(generatedName.AsNode(), expression));
        }
    }
    return tx.Transformer.Visitor().VisitNode(name);
}

internal static ж<ast.Node> visitPropertyDeclaration(this ref LegacyDecoratorsTransformer tx, ж<ast.PropertyDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient)) != 0) {
        return default;
    }
    if (ast.HasSyntacticModifier(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), (ast.ModifierFlags)(ast.ModifierFlagsAmbient | ast.ModifierFlagsAbstract))) {
        return default;
    }

    return tx.finishClassElement(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(
            Ꮡnode,
            tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.PropertyDeclaration.ᏑNamedMemberBase).Modifiers()),
            tx.visitPropertyNameOfClassElement(Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Initializer)),
        Ꮡnode.of(ast.PropertyDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitGetAccessorDeclaration(this ref LegacyDecoratorsTransformer tx, ж<ast.GetAccessorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.finishClassElement(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(
            Ꮡnode,
            tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()),
            tx.visitPropertyNameOfClassElement(Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            tx.Transformer.Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Body)),
        Ꮡnode.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitSetAccessorDeclaration(this ref LegacyDecoratorsTransformer tx, ж<ast.SetAccessorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.finishClassElement(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(
            Ꮡnode,
            tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNamedMemberBase).Modifiers()),
            tx.visitPropertyNameOfClassElement(Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            tx.Transformer.Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Body)),
        Ꮡnode.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitMethodDeclaration(this ref LegacyDecoratorsTransformer tx, ж<ast.MethodDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.finishClassElement(
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(
            Ꮡnode,
            tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.MethodDeclaration.ᏑNamedMemberBase).Modifiers()),
            node.AsteriskToken,
            tx.visitPropertyNameOfClassElement(Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
            nil,
            nil,
            tx.Transformer.Visitor().VisitNodes(node.Parameters),
            nil,
            nil,
            tx.Transformer.Visitor().VisitNode(node.Body)),
        Ꮡnode.of(ast.MethodDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<ast.Node> visitConstructorDeclaration(this ref LegacyDecoratorsTransformer tx, ж<ast.ConstructorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(
        Ꮡnode,
        tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.ConstructorDeclaration.ᏑModifiersBase).Modifiers()),
        nil,
        tx.Transformer.Visitor().VisitNodes(node.Parameters),
        nil,
        nil,
        tx.Transformer.Visitor().VisitNode(node.Body));
}

internal static ж<ast.Node> visitClassExpression(this ref LegacyDecoratorsTransformer tx, ж<ast.ClassExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(
        Ꮡnode,
        tx.Transformer.Visitor().VisitModifiers(Ꮡnode.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers()),
        node.Name(),
        nil,
        tx.Transformer.Visitor().VisitNodes(node.HeritageClauses),
        tx.Transformer.Visitor().VisitNodes(node.Members));
}

internal static ж<ast.Node> visitClassDeclaration(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decorated = ast.ClassOrConstructorParameterIsDecorated(true, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (!(decorated || ast.ChildIsDecorated(true, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil))) {
        return Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    if (decorated) {
        return Ꮡtx.transformClassDeclarationWithClassDecorators(Ꮡnode, node.Name());
    }
    return Ꮡtx.transformClassDeclarationWithoutClassDecorators(Ꮡnode, node.Name());
}

internal static ж<ast.Node> transformClassDeclarationWithoutClassDecorators(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> Ꮡnode, ж<astꓸDeclarationName> Ꮡname) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    var modifiers = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitModifiers(Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers());
    var heritageClauses = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitNodes(node.HeritageClauses);
    var initialMembers = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitNodes(node.Members);
    var (members, decorationStatements) = Ꮡtx.transformDecoratorsOfClassElements(Ꮡnode, initialMembers);

    if (Ꮡname == nil && len(decorationStatements) > 0) {
        Ꮡname = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewGeneratedNameForNode(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); name = ref Ꮡname.DerefOrNull();
    }

    var updated = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(
        Ꮡnode,
        modifiers,
        Ꮡname,
        nil,
        heritageClauses,
        members);

    if (len(decorationStatements) == 0) {
        return updated;
    }
    return Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(appendꓸꓸꓸ(slice<ж<ast.Node>>([updated]), decorationStatements));
}

internal static void popEnclosingClass(this ref LegacyDecoratorsTransformer tx) {
    tx.enclosingClasses = tx.enclosingClasses.slice(0, len(tx.enclosingClasses) - 1);
}

internal static void pushEnclosingClass(this ref LegacyDecoratorsTransformer tx, ж<ast.ClassDeclaration> cls) {
    tx.enclosingClasses = append(tx.enclosingClasses, cls);
}

internal static ж<ast.Node> transformClassDeclarationWithClassDecorators(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> Ꮡnode, ж<astꓸDeclarationName> name) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();
        ref var node = ref Ꮡnode.DerefOrNull();

        var isExport = ast.HasSyntacticModifier(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport);
        var isDefault = ast.HasSyntacticModifier(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsDefault);
        ж<ast.ModifierList> modifiers = default;
        if (Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers() != nil && len(Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers().Nodes) > 0) {
            var modifierNodes = core.Filter<ж<ast.Node>>(Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers().Nodes, isNotExportOrDefaultOrDecorator);
            if (len(modifierNodes) != len(Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers().Nodes)) {
                modifiers = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewModifierList(modifierNodes);
                modifiers.Loc = Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers().Loc;
            } else {
                modifiers = Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers();
            }
        }

        var location = transformers.MoveRangePastModifiers(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        var classAlias = Ꮡtx.getClassAliasIfNeeded(Ꮡnode);
        if (classAlias != nil) {
            tx.pushEnclosingClass(Ꮡnode);
            ᒐd1 = true;
        }

        var declName = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().GetLocalNameEx(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), new printer.AssignedNameOptions(AllowComments: false, AllowSourceMaps: true));

        var heritageClauses = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitNodes(node.HeritageClauses);
        var members = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Visitor().VisitNodes(node.Members);

        (members, var decorationStatements) = Ꮡtx.transformDecoratorsOfClassElements(Ꮡnode, members);

        var assignClassAliasInStaticBlock = tx.languageVersion >= core.ScriptTargetES2022 && classAlias != nil && members != nil && len(members.Nodes) > 0 && core.Some<ж<ast.Node>>(members.Nodes, isClassStaticBlockDeclarationOrStaticProperty);
        if (assignClassAliasInStaticBlock) {
            var memberList = slice<ж<ast.Node>>([]);
            memberList = append(memberList, Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(nil, Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(
                Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                    Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(classAlias, Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindThisKeyword)))
                ])),
                false)));
            memberList = appendꓸꓸꓸ(memberList, members.Nodes);
            var newList = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(memberList);
            newList.Loc = members.Loc;
            members = newList;
        }

        var exprName = name;
        if (name != nil && transformers.IsGeneratedIdentifier(Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext(), name)) {
            exprName = default;
        }
        var classExpression = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewClassExpression(
            modifiers,
            exprName,
            nil,
            heritageClauses,
            members);

        Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetOriginal(classExpression, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        classExpression.Loc = location;

        var varInitializer = classExpression;
        if (classAlias != nil && !assignClassAliasInStaticBlock) {
            varInitializer = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(classAlias, classExpression);
        }
        var varDecl = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
            declName,
            nil,
            nil,
            varInitializer);
        Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetOriginal(varDecl, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

        var varDeclList = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsLet);
        var varStatement = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, varDeclList);
        Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetOriginal(varStatement, Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        varStatement.Loc = location;
        Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetCommentRange(varStatement, node.Loc);

        var statements = slice<ж<ast.Node>>([varStatement]);
        statements = appendꓸꓸꓸ(statements, decorationStatements);
        statements = append(statements, Ꮡtx.getConstructorDecorationStatement(Ꮡnode));

        if (isExport) {
            ж<ast.Node> exportStatement = default;
            if (isDefault) {
                exportStatement = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewExportDefault(declName);
            } else {
                exportStatement = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewExternalModuleExport(Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().GetDeclarationName(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
            }
            statements = append(statements, exportStatement);
        }

        if (len(statements) == 1) {
            return statements[0];
        }
        return Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSyntaxList(statements);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡtx.DerefOrNull().popEnclosingClass(); ᒐ.Run(); }
}

internal static bool hasInternalStaticReference(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var classNode = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().MostOriginal(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    ref var isOrContainsStaticSelfReference = ref heap<Func<ж<ast.Node>, bool>>(out var ᏑisOrContainsStaticSelfReference);
    var classNodeʗ1 = classNode;
    isOrContainsStaticSelfReference = (ж<ast.Node> n) => {
        if (ast.IsIdentifier(n) && Ꮡtx.Value.referenceResolver.GetReferencedValueDeclaration(Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().MostOriginal(n)) == classNodeʗ1) {
            return true;
        }
        if (ast.IsPropertyAccessExpression(n)) {
            return ᏑisOrContainsStaticSelfReference.ValueSlot(n.Expression());
        }
        return n.ForEachChild(ᏑisOrContainsStaticSelfReference.ValueSlot);
    };
    foreach (var (_, member) in range(node.Members.Nodes)) {
        if (member.ForEachChild(isOrContainsStaticSelfReference)) {
            return true;
        }
    }
    return false;
}

internal static ж<ast.Node> getClassAliasIfNeeded(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!Ꮡtx.hasInternalStaticReference(Ꮡnode)) {
        return default;
    }
    @string nameText = "default";
    if (node.Name() != nil && !transformers.IsGeneratedIdentifier(Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext(), node.Name())) {
        nameText = node.Name().Text();
    }

    var classAlias = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewUniqueName(nameText);
    Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().AddVariableDeclaration(classAlias);
    tx.classAliases[Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()] = classAlias;

    return classAlias;
}

internal static ж<ast.Node> getConstructorDecorationStatement(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> node) {
    var expression = Ꮡtx.generateConstructorDecorationExpression(node);
    if (expression != nil) {
        var result = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
        Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetOriginal(result, node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return result;
    }
    return default;
}

internal static ж<ast.Node> generateConstructorDecorationExpression(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> node) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    var allDecorators = getAllDecoratorsOfClass(node, true);
    var hasAlias = len(tx.enclosingClasses) > 0 && tx.enclosingClasses[len(tx.enclosingClasses) - 1] == node;
    if (hasAlias) {
        tx.popEnclosingClass();
    }
    var decoratorExpressions = Ꮡtx.transformAllDecoratorsOfDeclaration(allDecorators);
    if (hasAlias) {
        tx.pushEnclosingClass(node);
    }
    if (len(decoratorExpressions) == 0) {
        return default;
    }

    ж<ast.Node> classAlias = default;
    if (tx.classAliases != nil) {
        (classAlias, _) = tx.classAliases[node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ꟷ];
    }

    var localName = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().GetDeclarationNameEx(node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), new printer.NameOptions(AllowComments: false, AllowSourceMaps: true));
    var decorate = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewDecorateHelper(decoratorExpressions, localName, nil, nil);
    var assignmentTarget = decorate;
    if (classAlias != nil) {
        assignmentTarget = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(classAlias, decorate);
    }
    var expression = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewAssignmentExpression(localName, assignmentTarget);
    Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetEmitFlags(expression, printer.EFNoComments);
    Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetSourceMapRange(expression, transformers.MoveRangePastModifiers(node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    return expression;
}

} partial class tstransforms_package {

internal static bool isClassStaticBlockDeclarationOrStaticProperty(ж<ast.Node> node) {
    return ast.IsClassStaticBlockDeclaration(node) || (ast.IsPropertyDeclaration(node) && ast.HasStaticModifier(node));
}

internal static bool isNotExportOrDefaultOrDecorator(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return !(ast.IsDecorator(Ꮡnode) || node.Kind == ast.KindExportKeyword || node.Kind == ast.KindDefaultKeyword);
}

internal static bool decoratorContainsPrivateIdentifierInExpression(ж<ast.Node> Ꮡdecorator) {
    ref var decorator = ref Ꮡdecorator.DerefOrNull();

    return ((ast.SubtreeFacts)(decorator.SubtreeFacts() & ast.SubtreeContainsPrivateIdentifierInExpression)) != 0;
}

internal static bool parameterDecoratorsContainPrivateIdentifierInExpression(slice<ж<ast.Node>> parameterDecorators) {
    return core.Some<ж<ast.Node>>(parameterDecorators, decoratorContainsPrivateIdentifierInExpression);
}

internal static bool hasClassElementWithDecoratorContainingPrivateIdentifierInExpression(ref ast.ClassDeclaration node) {
    if (node.Members == nil || len(node.Members.Nodes) == 0) {
        return false;
    }
    foreach (var (_, member) in range(node.Members.Nodes)) {
        if (!ast.CanHaveDecorators(member)) {
            continue;
        }
        var allDecorators = getAllDecoratorsOfClassElement(member, ref node, true);
        if (allDecorators == nil) {
            continue;
        }
        if (core.Some<ж<ast.Node>>(allDecorators.decorators, decoratorContainsPrivateIdentifierInExpression)) {
            return true;
        }
        if (core.Some<slice<ж<ast.Node>>>(allDecorators.parameters, parameterDecoratorsContainPrivateIdentifierInExpression)) {
            return true;
        }
    }
    return false;
}

partial struct allDecorators {
    internal slice<ж<ast.Node>> decorators;
    internal slice<slice<ж<ast.Node>>> parameters;
}

internal static ж<allDecorators> getAllDecoratorsOfClass(ж<ast.ClassDeclaration> node, bool useLegacyDecorators) {
    var decorators = node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Decorators();
    slice<slice<ж<ast.Node>>> parameters = default;
    if (useLegacyDecorators) {
        parameters = getDecoratorsOfParameters(ast.GetFirstConstructorWithBody(node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    }
    if (len(decorators) == 0 && len(parameters) == 0) {
        return default;
    }
    return Ꮡ(new allDecorators(decorators: decorators, parameters: parameters));
}

internal static ж<allDecorators> getAllDecoratorsOfClassElement(ж<ast.Node> Ꮡmember, ref ast.ClassDeclaration parent, bool useLegacyDecorators) {
    ref var member = ref Ꮡmember.DerefOrNull();

    switch (member.Kind) {
    case var ᴛ when ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
        if (!useLegacyDecorators) {
            return getAllDecoratorsOfMethod(Ꮡmember, false);
        }
        return getAllDecoratorsOfAccessors(Ꮡmember, ref parent, true);
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        return getAllDecoratorsOfMethod(Ꮡmember, useLegacyDecorators);
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return getAllDecoratorsOfProperty(Ꮡmember);
    }
    default: {
        return default;
    }}

}

internal static ж<allDecorators> getAllDecoratorsOfAccessors(ж<ast.Node> Ꮡaccessor, ref ast.ClassDeclaration parent, bool useLegacyDecorators) {
    ref var accessor = ref Ꮡaccessor.DerefOrNull();

    if (accessor.Body() == nil) {
        return default;
    }
    var decls = ast.GetAllAccessorDeclarations(parent.Members.Nodes, Ꮡaccessor);
    ж<ast.Node> firstAccessorWithDecorators = default;
    if (ast.HasDecorators(decls.FirstAccessor)) {
        firstAccessorWithDecorators = decls.FirstAccessor;
    } else if (decls.SecondAccessor != nil && ast.HasDecorators(decls.SecondAccessor)) {
        firstAccessorWithDecorators = decls.SecondAccessor;
    }

    if (firstAccessorWithDecorators == nil || Ꮡaccessor != firstAccessorWithDecorators) {
        return default;
    }

    var decorators = firstAccessorWithDecorators.Decorators();
    slice<slice<ж<ast.Node>>> parameters = default;
    if (useLegacyDecorators && decls.SetAccessor != nil) {
        parameters = getDecoratorsOfParameters(decls.SetAccessor.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    if (len(decorators) == 0 && len(parameters) == 0) {
        return default;
    }

    return Ꮡ(new allDecorators(
        decorators: decorators,
        parameters: parameters
    ));
}

internal static ж<allDecorators> getAllDecoratorsOfProperty(ж<ast.Node> Ꮡproperty) {
    ref var property = ref Ꮡproperty.DerefOrNull();

    var decorators = property.Decorators();
    if (len(decorators) == 0) {
        return default;
    }
    return Ꮡ(new allDecorators(decorators: decorators));
}

internal static ж<allDecorators> getAllDecoratorsOfMethod(ж<ast.Node> Ꮡmethod, bool useLegacyDecorators) {
    ref var method = ref Ꮡmethod.DerefOrNull();

    if (method.Body() == nil) {
        return default;
    }
    var decorators = method.Decorators();
    slice<slice<ж<ast.Node>>> parameters = default;
    if (useLegacyDecorators) {
        parameters = getDecoratorsOfParameters(Ꮡmethod);
    }
    if (len(decorators) == 0 && len(parameters) == 0) {
        return default;
    }
    return Ꮡ(new allDecorators(decorators: decorators, parameters: parameters));
}

internal static slice<slice<ж<ast.Node>>> getDecoratorsOfParameters(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    slice<slice<ж<ast.Node>>> decorators = default;
    if (Ꮡnode != nil) {
        var parameters = node.Parameters();
        var firstParameterIsThis = len(parameters) > 0 && ast.IsThisParameter(parameters[0]);
        nint firstParameterOffset = 0;
        nint numParameters = len(parameters);
        if (firstParameterIsThis) {
            firstParameterOffset = 1;
            numParameters = numParameters - 1;
        }
        foreach (var i in range(numParameters)) {
            var p = parameters[i + firstParameterOffset];
            if (len(decorators) > 0 || ast.HasDecorators(p)) {
                if (len(decorators) == 0) {
                    decorators = new slice<slice<ж<ast.Node>>>(numParameters);
                }
                decorators[i] = p.Decorators();
            }
        }
    }
    return decorators;
}

} partial class tstransformsᴛLegacyDecoratorsTransformer {

internal static (ж<ast.NodeList>, slice<ж<ast.Node>>) transformDecoratorsOfClassElements(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> node, ж<ast.NodeList> Ꮡmembers) {
    ref var members = ref Ꮡmembers.DerefOrNull();

    slice<ж<ast.Node>> decorationStatements = default;
    decorationStatements = appendꓸꓸꓸ(decorationStatements, Ꮡtx.getClassElementDecorationStatements(node, false));
    decorationStatements = appendꓸꓸꓸ(decorationStatements, Ꮡtx.getClassElementDecorationStatements(node, true));
    if (hasClassElementWithDecoratorContainingPrivateIdentifierInExpression(ref (node).DerefOrNull())) {
        slice<ж<ast.Node>> memberNodes = default;
        if (Ꮡmembers != nil && len(members.Nodes) > 0) {
            memberNodes = members.Nodes;
        }
        Ꮡmembers = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(
            append(
                appendꓸꓸꓸ(slice<ж<ast.Node>>([]), memberNodes),
                Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(nil, Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(decorationStatements), true)))); members = ref Ꮡmembers.DerefOrNull();
        decorationStatements = default;
    }

    return (Ꮡmembers, decorationStatements);
}

internal static slice<ж<ast.Node>> getClassElementDecorationStatements(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> node, bool isStatic) {
    var statementsᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    var exprs = Ꮡtx.generateClassElementDecorationExpressions(node, isStatic);
    slice<ж<ast.Node>> statements = default;
    foreach (var (_, e) in range(exprs)) {
        statements = append(statementsᴷ, statements, Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(e));
    }
    statements = move2heap(statementsᴷ, statements);
    return statements;
}

} partial class tstransforms_package {

internal static bool isDecoratedClassElement(ж<ast.Node> member, bool isStaticElement, ж<ast.ClassDeclaration> parent) {
    return isStaticElement == ast.IsStatic(member) && ast.NodeOrChildIsDecorated(true, member, parent.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil);
}

internal static slice<ж<ast.Node>> getDecoratedClassElements(ж<ast.ClassDeclaration> Ꮡnode, bool isStatic) {
    ref var node = ref Ꮡnode.DerefOrNull();
    var membersᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    if (node.Members == nil || len(node.Members.Nodes) == 0) {
        return default;
    }
    slice<ж<ast.Node>> members = default;
    foreach (var (_, member) in range(node.Members.Nodes)) {
        if (isDecoratedClassElement(member, isStatic, Ꮡnode)) {
            members = append(membersᴷ, members, member);
        }
    }
    members = move2heap(membersᴷ, members);
    return members;
}

} partial class tstransformsᴛLegacyDecoratorsTransformer {

internal static slice<ж<ast.Node>> generateClassElementDecorationExpressions(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> node, bool isStatic) {
    var expressionsᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    var members = getDecoratedClassElements(node, isStatic);
    slice<ж<ast.Node>> expressions = default;
    foreach (var (_, member) in range(members)) {
        var expr = Ꮡtx.generateClassElementDecorationExpression(node, member);
        if (expr != nil) {
            expressions = append(expressionsᴷ, expressions, expr);
        }
    }
    expressions = move2heap(expressionsᴷ, expressions);
    return expressions;
}

internal static ж<ast.Node> generateClassElementDecorationExpression(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<ast.ClassDeclaration> node, ж<ast.Node> Ꮡmember) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var member = ref Ꮡmember.DerefOrNull();

    var allDecorators = getAllDecoratorsOfClassElement(Ꮡmember, ref (node).DerefOrNull(), true);
    var decoratorExpressions = Ꮡtx.transformAllDecoratorsOfDeclaration(allDecorators);
    if (len(decoratorExpressions) == 0) {
        return default;
    }

    var prefix = tx.getClassMemberPrefix(node, Ꮡmember);
    var memberName = tx.getExpressionForPropertyName(Ꮡmember, (ast.NodeFlags)(member.Flags & ast.NodeFlagsAmbient) == 0);
    ж<ast.Node> descriptor = default;
    if (ast.IsPropertyDeclaration(Ꮡmember) && !ast.HasAccessorModifier(Ꮡmember)) {
        descriptor = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewVoidZeroExpression();
    } else {
        descriptor = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindNullKeyword);
    }

    var helper = Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).Factory().NewDecorateHelper(
        decoratorExpressions,
        prefix,
        memberName,
        descriptor);

    Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetEmitFlags(helper, printer.EFNoComments);
    Ꮡtx.of(LegacyDecoratorsTransformer.ᏑTransformer).EmitContext().SetSourceMapRange(helper, transformers.MoveRangePastModifiers(Ꮡmember));
    return helper;
}

internal static bool isSyntheticMetadataDecorator(this ref LegacyDecoratorsTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.Transformer.EmitContext().IsCallToHelper(node.Expression(), "__metadata");
}

internal static slice<ж<ast.Node>> transformAllDecoratorsOfDeclaration(this ж<LegacyDecoratorsTransformer> Ꮡtx, ж<allDecorators> ᏑallDecorators) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var allDecorators = ref ᏑallDecorators.DerefOrNull();

    if (ᏑallDecorators == nil) {
        return default;
    }

    var mm = collections.GroupBy<bool, ж<ast.Node>>(allDecorators.decorators, Ꮡtx.isSyntheticMetadataDecorator);
    var metadata = mm.Get(true);
    var decorators = mm.Get(false);

    slice<ж<ast.Node>> decoratorExpressions = default;
    decoratorExpressions = appendꓸꓸꓸ(decoratorExpressions, tx.transformDecorators(decorators));
    decoratorExpressions = appendꓸꓸꓸ(decoratorExpressions, tx.transformDecoratorsOfParameters(allDecorators.parameters));
    decoratorExpressions = appendꓸꓸꓸ(decoratorExpressions, tx.transformDecorators(metadata));
    return decoratorExpressions;
}

internal static slice<ж<ast.Node>> transformDecoratorsOfParameters(this ref LegacyDecoratorsTransformer tx, slice<slice<ж<ast.Node>>> parameters) {
    var resultsᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    slice<ж<ast.Node>> results = default;
    foreach (var (i, decorators) in range(parameters)) {
        if (len(decorators) > 0) {
            foreach (var (_, decorator) in range(decorators)) {
                var helper = tx.Transformer.Factory().NewParamHelper(
                    tx.Transformer.Visitor().VisitNode(decorator.Expression()),
                    i,
                    decorator.Expression().Loc);
                tx.Transformer.EmitContext().SetEmitFlags(helper, printer.EFNoComments);
                results = append(resultsᴷ, results, helper);
            }
        }
    }
    results = move2heap(resultsᴷ, results);
    return results;
}

internal static slice<ж<ast.Node>> transformDecorators(this ref LegacyDecoratorsTransformer tx, slice<ж<ast.Node>> decorators) {
    var resultsᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    slice<ж<ast.Node>> results = default;
    foreach (var (_, d) in range(decorators)) {
        results = append(resultsᴷ, results, tx.Transformer.Visitor().VisitNode(d.Expression()));
    }
    results = move2heap(resultsᴷ, results);
    return results;
}

internal static ж<ast.Node> getClassMemberPrefix(this ref LegacyDecoratorsTransformer tx, ж<ast.ClassDeclaration> node, ж<ast.Node> member) {
    if (ast.IsStatic(member)) {
        return tx.Transformer.Factory().GetDeclarationName(node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }
    return tx.getClassPrototype(node);
}

internal static ж<ast.Node> getClassPrototype(this ref LegacyDecoratorsTransformer tx, ж<ast.ClassDeclaration> node) {
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
        tx.Transformer.Factory().GetDeclarationName(node.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()),
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("prototype"),
        ast.NodeFlagsNone);
}

internal static ж<ast.Node> getExpressionForPropertyName(this ref LegacyDecoratorsTransformer tx, ж<ast.Node> Ꮡmember, bool generateNameForComputedPropertyName) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var name = member.Name();
    if (ast.IsPrivateIdentifier(name)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("");
    } else if (ast.IsComputedPropertyName(name)) {
        if (generateNameForComputedPropertyName && !transformers.IsSimpleInlineableExpression((~name.AsComputedPropertyName()).Expression)) {
            return tx.Transformer.Factory().NewGeneratedNameForNode(name);
        }
        return (~name.AsComputedPropertyName()).Expression;
    } else if (ast.IsIdentifier(name)) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(name.Text(), ast.TokenFlagsNone);
    } else {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).DeepCloneNode(name);
    }
}

} // end tstransformsᴛLegacyDecoratorsTransformer
