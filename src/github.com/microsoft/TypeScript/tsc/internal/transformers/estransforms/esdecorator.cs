namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using static estransforms_package;

partial class estransforms_package {

partial struct lexicalEntryKind /*num:nint*/;

internal static lexicalEntryKind lexicalEntryKindClass => /* iota */ 0;
internal static lexicalEntryKind lexicalEntryKindClassElement => 1;
internal static lexicalEntryKind lexicalEntryKindName => 2;
internal static lexicalEntryKind lexicalEntryKindOther => 3;

partial struct lexicalEntry {
    internal lexicalEntryKind kind;
    internal ж<lexicalEntry> next;
    internal ж<classInfo> classInfoData;
    internal slice<ж<astꓸExpression>> savedPendingExpressions;
    internal ж<astꓸIdentifierNode> classThisData;
    internal ж<astꓸIdentifierNode> classSuperData;
    internal nint depth;
}

partial struct memberInfo {
    internal ж<astꓸIdentifierNode> memberDecoratorsName;
    internal ж<astꓸIdentifierNode> memberInitializersName;
    internal ж<astꓸIdentifierNode> memberExtraInitializersName;
    internal ж<astꓸIdentifierNode> memberDescriptorName;
}

partial struct classInfo {
    internal ж<ast.Node> @class;
    internal ж<astꓸIdentifierNode> classDecoratorsName;
    internal ж<astꓸIdentifierNode> classDescriptorName;
    internal ж<astꓸIdentifierNode> classExtraInitializersName;
    internal ж<astꓸIdentifierNode> classThis;
    internal ж<astꓸIdentifierNode> classSuper;
    internal ж<astꓸIdentifierNode> metadataReference;
    internal collections.OrderedMap<ж<ast.Node>, ж<memberInfo>> memberInfos;
    internal ж<astꓸIdentifierNode> instanceMethodExtraInitializersName;
    internal ж<astꓸIdentifierNode> staticMethodExtraInitializersName;
    internal slice<ж<astꓸStatement>> staticNonFieldDecorationStatements;
    internal slice<ж<astꓸStatement>> nonStaticNonFieldDecorationStatements;
    internal slice<ж<astꓸStatement>> staticFieldDecorationStatements;
    internal slice<ж<astꓸStatement>> nonStaticFieldDecorationStatements;
    internal bool hasStaticInitializers;
    internal bool hasNonAmbientInstanceFields;
    internal bool hasStaticPrivateClassElements;
    internal slice<ж<astꓸExpression>> pendingStaticInitializers;
    internal slice<ж<astꓸExpression>> pendingInstanceInitializers;
}

partial struct esDecoratorTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ж<lexicalEntry> top;
    internal ж<classInfo> classInfoStack;
    internal ж<astꓸIdentifierNode> classThis;
    internal ж<astꓸIdentifierNode> classSuper;
    internal slice<ж<astꓸExpression>> pendingExpressions;
    internal ж<astꓸIdentifierNode> outerThis;
    internal bool shouldTransformPrivateStaticElementsInFile;
    internal ж<ast.NodeVisitor> outerThisVisitor;
    internal ж<ast.NodeVisitor> discardedVisitor;
    internal ж<ast.NodeVisitor> modifierVisitor;
    internal ж<ast.NodeVisitor> exportStrippingModifierVisitor;
    internal ж<ast.NodeVisitor> classElementVisitor;
    internal ж<ast.NodeVisitor> nonConstructorClassElementVisitor;
    internal ж<ast.NodeVisitor> constructorClassElementVisitor;
    internal ж<ast.NodeVisitor> arrayAssignmentVisitor;
    internal ж<ast.NodeVisitor> objectAssignmentVisitor;
    internal ж<ast.NodeVisitor> staticOnlyModifierVisitor;
    internal ж<ast.NodeVisitor> asyncOnlyModifierVisitor;
    internal ж<ast.NodeVisitor> accessorStrippingModifierVisitor;
}

internal static ж<transformers.Transformer> newESDecoratorTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    if (opts.CompilerOptions.ExperimentalDecorators.IsTrue() || (opts.CompilerOptions.GetEmitScriptTarget() >= core.ScriptTargetESNext && opts.CompilerOptions.GetUseDefineForClassFields())) {
        return default;
    }
    var tx = Ꮡ(new esDecoratorTransformer(compilerOptions: opts.CompilerOptions));
    var result = tx.of(esDecoratorTransformer.ᏑTransformer).NewTransformer(tx.visit, opts.Context);
    var ec = tx.of(esDecoratorTransformer.ᏑTransformer).EmitContext();
    tx.outerThisVisitor = ec.NewNodeVisitor(tx.outerThisVisit);
    tx.discardedVisitor = ec.NewNodeVisitor(tx.discardedValueVisit);
    tx.modifierVisitor = ec.NewNodeVisitor(tx.modifierVisitorVisit);
    tx.exportStrippingModifierVisitor = ec.NewNodeVisitor(tx.exportStrippingModifierVisit);
    tx.classElementVisitor = ec.NewNodeVisitor(tx.classElementVisitorVisit);
    tx.nonConstructorClassElementVisitor = ec.NewNodeVisitor(tx.nonConstructorClassElementVisit);
    tx.constructorClassElementVisitor = ec.NewNodeVisitor(tx.constructorClassElementVisit);
    tx.arrayAssignmentVisitor = ec.NewNodeVisitor(tx.visitArrayAssignmentElement);
    tx.objectAssignmentVisitor = ec.NewNodeVisitor(tx.visitObjectAssignmentElement);
    tx.staticOnlyModifierVisitor = ec.NewNodeVisitor((ж<ast.Node> node) => {
        if (node.Kind == ast.KindStaticKeyword) {
            return node;
        }
        return default;
    });
    tx.asyncOnlyModifierVisitor = ec.NewNodeVisitor((ж<ast.Node> node) => {
        if (node.Kind == ast.KindAsyncKeyword) {
            return node;
        }
        return default;
    });
    tx.accessorStrippingModifierVisitor = ec.NewNodeVisitor((ж<ast.Node> node) => {
        if (node.Kind == ast.KindAccessorKeyword) {
            return default;
        }
        return node;
    });
    return result;
}

} partial class estransformsᴛesDecoratorTransformer {

internal static void updateState(this ref esDecoratorTransformer tx) {
    tx.classInfoStack = default;
    tx.classThis = default;
    tx.classSuper = default;
    if (tx.top == nil) {
        return;
    }
    switch (tx.top.kind) {
    case var ᴛ when ᴛ == lexicalEntryKindClass: {
        tx.classInfoStack = tx.top.classInfoData;
        break;
    }
    case var ᴛ when ᴛ == lexicalEntryKindClassElement: {
        tx.classInfoStack = tx.top.next.classInfoData;
        tx.classThis = tx.top.classThisData;
        tx.classSuper = tx.top.classSuperData;
        break;
    }
    case var ᴛ when ᴛ == lexicalEntryKindName: {
        var grandparent = tx.top.next.next.next;
        if (grandparent != nil && grandparent.kind == lexicalEntryKindClassElement) {
            tx.classInfoStack = grandparent.next.classInfoData;
            tx.classThis = grandparent.classThisData;
            tx.classSuper = grandparent.classSuperData;
        }
        break;
    }}

}

internal static void enterClass(this ref esDecoratorTransformer tx, ж<classInfo> ci) {
    tx.top = Ꮡ(new lexicalEntry(
        kind: lexicalEntryKindClass,
        next: tx.top,
        classInfoData: ci,
        savedPendingExpressions: tx.pendingExpressions
    ));
    tx.pendingExpressions = default;
    tx.updateState();
}

internal static void exitClass(this ref esDecoratorTransformer tx) {
    debug.Assert(tx.top != nil && tx.top.kind == lexicalEntryKindClass, "Incorrect value for top.kind. Expected top.kind to be 'class' but got '", tx.top.kind, "' instead.");
    tx.pendingExpressions = tx.top.savedPendingExpressions;
    tx.top = tx.top.next;
    tx.updateState();
}

internal static void enterClassElement(this ref esDecoratorTransformer tx, ж<ast.Node> node) {
    debug.Assert(tx.top != nil && tx.top.kind == lexicalEntryKindClass, "Incorrect value for top.kind. Expected top.kind to be 'class' but got '", tx.top.kind, "' instead.");
    tx.top = Ꮡ(new lexicalEntry(
        kind: lexicalEntryKindClassElement,
        next: tx.top
    ));
    if (ast.IsClassStaticBlockDeclaration(node) || ast.IsPropertyDeclaration(node) && ast.HasStaticModifier(node)) {
        if (tx.top.next.classInfoData != nil) {
            tx.top.classThisData = tx.top.next.classInfoData.classThis;
            tx.top.classSuperData = tx.top.next.classInfoData.classSuper;
        }
    }
    tx.updateState();
}

internal static void exitClassElement(this ref esDecoratorTransformer tx) {
    debug.Assert(tx.top != nil && tx.top.kind == lexicalEntryKindClassElement, "Incorrect value for top.kind. Expected top.kind to be 'class-element' but got '", tx.top.kind, "' instead.");
    debug.Assert(tx.top.next != nil && tx.top.next.kind == lexicalEntryKindClass, "Incorrect value for top.next.kind. Expected top.next.kind to be 'class' but got '", tx.top.next.kind, "' instead.");
    tx.top = tx.top.next;
    tx.updateState();
}

internal static void enterName(this ref esDecoratorTransformer tx) {
    debug.Assert(tx.top != nil && tx.top.kind == lexicalEntryKindClassElement, "Incorrect value for top.kind. Expected top.kind to be 'class-element' but got '", tx.top.kind, "' instead.");
    tx.top = Ꮡ(new lexicalEntry(
        kind: lexicalEntryKindName,
        next: tx.top
    ));
    tx.updateState();
}

internal static void exitName(this ref esDecoratorTransformer tx) {
    debug.Assert(tx.top != nil && tx.top.kind == lexicalEntryKindName, "Incorrect value for top.kind. Expected top.kind to be 'name' but got '", tx.top.kind, "' instead.");
    tx.top = tx.top.next;
    tx.updateState();
}

internal static void enterOther(this ref esDecoratorTransformer tx) {
    if (tx.top != nil && tx.top.kind == lexicalEntryKindOther) {
        debug.Assert(len(tx.pendingExpressions) == 0);
        tx.top.depth++;
    } else {
        tx.top = Ꮡ(new lexicalEntry(
            kind: lexicalEntryKindOther,
            next: tx.top,
            savedPendingExpressions: tx.pendingExpressions
        ));
        tx.pendingExpressions = default;
        tx.updateState();
    }
}

internal static void exitOther(this ref esDecoratorTransformer tx) {
    debug.Assert(tx.top != nil && tx.top.kind == lexicalEntryKindOther, "Incorrect value for top.kind. Expected top.kind to be 'other' but got '", tx.top.kind, "' instead.");
    if (tx.top.depth > 0) {
        debug.Assert(len(tx.pendingExpressions) == 0);
        tx.top.depth--;
    } else {
        tx.pendingExpressions = tx.top.savedPendingExpressions;
        tx.top = tx.top.next;
        tx.updateState();
    }
}

internal static ж<ast.Node> visitSourceFile(this ref esDecoratorTransformer tx, ж<ast.SourceFile> node) {
    tx.top = default;
    tx.shouldTransformPrivateStaticElementsInFile = false;
    var visited = tx.Transformer.Visitor().VisitEachChild(node.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    tx.Transformer.EmitContext().AddEmitHelper(visited, tx.Transformer.EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    if (tx.shouldTransformPrivateStaticElementsInFile) {
        tx.Transformer.EmitContext().AddEmitFlags(visited, printer.EFTransformPrivateStaticElements);
        tx.shouldTransformPrivateStaticElementsInFile = false;
    }
    return visited;
}

internal static ж<ast.Node> outerThisVisit(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    if ((ast.SubtreeFacts)(n.SubtreeFacts() & ast.SubtreeContainsLexicalThis) == 0 && n.Kind != ast.KindThisKeyword) {
        return Ꮡn;
    }
    if (n.Kind == ast.KindThisKeyword) {
        if (tx.outerThis == nil) {
            tx.outerThis = tx.Transformer.Factory().NewUniqueNameEx("_outerThis", new printer.AutoGenerateOptions(
                Flags: printer.GeneratedIdentifierFlagsOptimistic
            ));
        }
        return tx.outerThis;
    }
    return tx.outerThisVisitor.VisitEachChild(Ꮡn);
}

internal static bool shouldVisitNode(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsDecorators) != 0 || (tx.classThis != nil && (ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsLexicalThis) != 0) || (tx.classThis != nil && tx.classSuper != nil && (ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsLexicalSuper) != 0);
}

internal static ж<ast.Node> visit(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindSourceFile) {
        return tx.visitSourceFile(node.AsSourceFile());
    }
    if (!tx.shouldVisitNode(Ꮡnode)) {
        return Ꮡnode;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindDecorator: {
        return default;
    }
    case var ᴛ when ᴛ == ast.KindClassDeclaration: {
        return tx.visitClassDeclaration(node.AsClassDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindClassExpression: {
        return tx.visitClassExpression(node.AsClassExpression());
    }
    case var ᴛ when ᴛ == ast.KindConstructor || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindClassStaticBlockDeclaration: {
        debug.Fail("Not supported outside of a class. Use 'classElementVisitor' instead.");
        return default;
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        return tx.visitParameterDeclaration(node.AsParameterDeclaration());
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return tx.visitBinaryExpression(Ꮡnode, false);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindBindingElement: {
        return tx.visitNamedEvaluationSite(Ꮡnode, node.Initializer());
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return tx.visitExportAssignment(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindThisKeyword: {
        return tx.visitThisExpression(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindForStatement: {
        return tx.visitForStatement(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindExpressionStatement: {
        return tx.visitExpressionStatement(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return tx.visitParenthesizedExpression(Ꮡnode, false);
    }
    case var ᴛ when ᴛ == ast.KindPartiallyEmittedExpression: {
        return tx.visitPartiallyEmittedExpression(Ꮡnode, false);
    }
    case var ᴛ when ᴛ == ast.KindCallExpression: {
        return tx.visitCallExpression(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindTaggedTemplateExpression: {
        return tx.visitTaggedTemplateExpression(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression || ᴛ == ast.KindPostfixUnaryExpression: {
        return tx.visitPreOrPostfixUnaryExpression(Ꮡnode, false);
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        return tx.visitPropertyAccessExpression(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindElementAccessExpression: {
        return tx.visitElementAccessExpression(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindComputedPropertyName: {
        return tx.visitComputedPropertyName(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindFunctionDeclaration: {
        tx.enterOther();
        var result = tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
        tx.exitOther();
        return result;
    }
    default: {
        return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
    }}

}

internal static ж<ast.Node> modifierVisitorVisit(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindDecorator) {
        return default;
    }
    return Ꮡnode;
}

internal static ж<ast.Node> classElementVisitorVisit(this ж<esDecoratorTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindConstructor: {
        return tx.visitConstructorDeclaration(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        return Ꮡtx.visitMethodDeclaration(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        return Ꮡtx.visitGetAccessorDeclaration(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        return Ꮡtx.visitSetAccessorDeclaration(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return Ꮡtx.visitPropertyDeclaration(Ꮡnode);
    }
    case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration: {
        return tx.visitClassStaticBlockDeclaration(Ꮡnode);
    }
    default: {
        return tx.visit(Ꮡnode);
    }}

}

internal static ж<ast.Node> discardedValueVisit(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression || ᴛ == ast.KindPostfixUnaryExpression: {
        return tx.visitPreOrPostfixUnaryExpression(Ꮡnode, true);
    }
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        return tx.visitBinaryExpression(Ꮡnode, true);
    }
    case var ᴛ when ᴛ == ast.KindParenthesizedExpression: {
        return tx.visitParenthesizedExpression(Ꮡnode, true);
    }
    case var ᴛ when ᴛ == ast.KindPartiallyEmittedExpression: {
        return tx.visitPartiallyEmittedExpression(Ꮡnode, true);
    }
    default: {
        return tx.visit(Ꮡnode);
    }}

}

internal static ж<ast.Node> nonConstructorClassElementVisit(this ж<esDecoratorTransformer> Ꮡtx, ж<ast.Node> node) {
    if (ast.IsConstructorDeclaration(node)) {
        return node;
    }
    return Ꮡtx.classElementVisitorVisit(node);
}

internal static ж<ast.Node> constructorClassElementVisit(this ж<esDecoratorTransformer> Ꮡtx, ж<ast.Node> node) {
    if (ast.IsConstructorDeclaration(node)) {
        return Ꮡtx.classElementVisitorVisit(node);
    }
    return node;
}

internal static ж<ast.Node> exportStrippingModifierVisit(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindExportKeyword) {
        return default;
    }
    return tx.modifierVisitorVisit(Ꮡnode);
}

} partial class estransforms_package {

internal static @string getHelperVariableName(ж<printer.EmitContext> Ꮡec, ж<ast.Node> Ꮡnode) {
    ref var ec = ref Ꮡec.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    @string declarationName = "";
    switch (ᐧ) {
    case {} when name != nil && ast.IsIdentifier(name) && !transformers.IsGeneratedIdentifier(Ꮡec, name): {
        declarationName = name.Text();
        break;
    }
    case {} when name != nil && ast.IsPrivateIdentifier(name) && !ec.HasAutoGenerateInfo(name): {
        @string text = name.Text(); if (len(text) > 1) {
            declarationName = text[1..];
        }
        break;
    }
    case {} when name != nil && ast.IsStringLiteral(name) && scanner.IsIdentifierText(name.Text(), core.LanguageVariantStandard): {
        declarationName = name.Text();
        break;
    }
    case {} when ast.IsClassLike(Ꮡnode): {
        declarationName = "class";
        break;
    }
    default: {
        declarationName = "member";
        break;
    }}

    if (ast.IsGetAccessorDeclaration(Ꮡnode)) {
        declarationName = "get_"u8 + declarationName;
    }
    if (ast.IsSetAccessorDeclaration(Ꮡnode)) {
        declarationName = "set_"u8 + declarationName;
    }
    if (name != nil && ast.IsPrivateIdentifier(name)) {
        declarationName = "private_"u8 + declarationName;
    }
    if (ast.IsStatic(Ꮡnode)) {
        declarationName = "static_"u8 + declarationName;
    }
    return "_"u8 + declarationName;
}

} partial class estransformsᴛesDecoratorTransformer {

internal static ж<astꓸIdentifierNode> createHelperVariable(this ref esDecoratorTransformer tx, ж<ast.Node> node, @string suffix) {
    return tx.Transformer.Factory().NewUniqueNameEx(
        getHelperVariableName(tx.Transformer.EmitContext(), node) + "_"u8 + suffix,
        new printer.AutoGenerateOptions(Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsReservedInNestedScopes)));
}

internal static ж<astꓸStatement> createLet(this ref esDecoratorTransformer tx, ж<astꓸIdentifierNode> name, ж<astꓸExpression> initializer) {
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
            tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(name, nil, nil, initializer)
            ])),
            ast.NodeFlagsLet));
}

internal static ж<classInfo> createClassInfo(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var ci = Ꮡ(new classInfo(
        @class: Ꮡnode,
        metadataReference: f.NewUniqueNameEx("_metadata", new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
        ))
    ));

    if (ast.NodeIsDecorated(false, Ꮡnode, nil, nil)) {
        var needsUniqueClassThis = core.Some(node.Members(), (ж<ast.Node> member) => (ast.IsPrivateIdentifierClassElementDeclaration(member) || ast.IsAutoAccessorPropertyDeclaration(member)) && ast.HasStaticModifier(member));
        printer.GeneratedIdentifierFlags flags = (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel);
        if (needsUniqueClassThis) {
            flags = (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsReservedInNestedScopes);
        }
        ci.classThis = f.NewUniqueNameEx("_classThis", new printer.AutoGenerateOptions(Flags: flags));
    }

    foreach (var (_, member) in range(node.Members())) {
        if (ast.IsMethodOrAccessor(member) && ast.NodeOrChildIsDecorated(false, member, Ꮡnode, nil)) {
            if (ast.HasStaticModifier(member)) {
                if (ci.staticMethodExtraInitializersName == nil) {
                    ci.staticMethodExtraInitializersName = f.NewUniqueNameEx("_staticExtraInitializers", new printer.AutoGenerateOptions(
                        Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
                    ));
                    ж<ast.Node> renamedClassThis = default;
                    if (ci.classThis != nil) {
                        renamedClassThis = ci.classThis;
                    } else {
                        renamedClassThis = f.NewThisExpression();
                    }
                    var initializer = f.NewRunInitializersHelper(renamedClassThis, ci.staticMethodExtraInitializersName, nil);
                    var nameRange = node.Name();
                    if (nameRange != nil) {
                        tx.Transformer.EmitContext().SetSourceMapRange(initializer, nameRange.Loc);
                    } else {
                        tx.Transformer.EmitContext().SetSourceMapRange(initializer, transformers.MoveRangePastDecorators(Ꮡnode));
                    }
                    ci.pendingStaticInitializers = append(ci.pendingStaticInitializers, initializer);
                }
            } else {
                if (ci.instanceMethodExtraInitializersName == nil) {
                    ci.instanceMethodExtraInitializersName = f.NewUniqueNameEx("_instanceExtraInitializers", new printer.AutoGenerateOptions(
                        Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
                    ));
                    var initializer = f.NewRunInitializersHelper(f.NewThisExpression(), ci.instanceMethodExtraInitializersName, nil);
                    var nameRange = node.Name();
                    if (nameRange != nil) {
                        tx.Transformer.EmitContext().SetSourceMapRange(initializer, nameRange.Loc);
                    } else {
                        tx.Transformer.EmitContext().SetSourceMapRange(initializer, transformers.MoveRangePastDecorators(Ꮡnode));
                    }
                    ci.pendingInstanceInitializers = append(ci.pendingInstanceInitializers, initializer);
                }
            }
        }

        if (ast.IsClassStaticBlockDeclaration(member)) {
            if (!isClassNamedEvaluationHelperBlock(tx.Transformer.EmitContext(), member)) {
                ci.hasStaticInitializers = true;
            }
        } else if (ast.IsPropertyDeclaration(member)) {
            if (ast.HasStaticModifier(member)) {
                ci.hasStaticInitializers = ci.hasStaticInitializers || member.Initializer() != nil || ast.HasDecorators(member);
            } else {
                ci.hasNonAmbientInstanceFields = ci.hasNonAmbientInstanceFields || !ast.HasSyntacticModifier(member, ast.ModifierFlagsAmbient);
            }
        }

        if ((ast.IsPrivateIdentifierClassElementDeclaration(member) || ast.IsAutoAccessorPropertyDeclaration(member)) && ast.HasStaticModifier(member)) {
            ci.hasStaticPrivateClassElements = true;
        }

        if (ci.staticMethodExtraInitializersName != nil && ci.instanceMethodExtraInitializersName != nil && ci.hasStaticInitializers && ci.hasNonAmbientInstanceFields && ci.hasStaticPrivateClassElements) {
            break;
        }
    }

    return ci;
}

internal static ж<astꓸExpression> transformClassLike(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var ec = tx.Transformer.EmitContext();

    ec.StartVariableEnvironment();

    if (!classHasDeclaredOrExplicitlyAssignedName(ec, Ꮡnode) && ast.ClassOrConstructorParameterIsDecorated(false, Ꮡnode)) {
        Ꮡnode = injectClassNamedEvaluationHelperBlockIfMissing(ec, Ꮡnode, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("", 0), nil); node = ref Ꮡnode.DerefOrNull();
    }

    var classReference = f.GetLocalNameEx(Ꮡnode, new printer.AssignedNameOptions());
    var ci = tx.createClassInfo(Ꮡnode);
    var classDefinitionStatements = slice<ж<astꓸStatement>>([]);
    slice<ж<astꓸStatement>> leadingBlockStatements = default;
    slice<ж<astꓸStatement>> trailingBlockStatements = default;
    ж<ast.Node> syntheticConstructor = default;
    ж<ast.NodeList> heritageClauses = default;
    var shouldTransformPrivateStaticElementsInClass = false;

    var classDecorators = tx.transformAllDecoratorsOfDeclaration(node.Decorators());
    if (len(classDecorators) > 0) {
        debug.Assert(ci.classThis != nil);

        ci.classDecoratorsName = f.NewUniqueNameEx("_classDecorators", new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
        ));
        ci.classDescriptorName = f.NewUniqueNameEx("_classDescriptor", new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
        ));
        ci.classExtraInitializersName = f.NewUniqueNameEx("_classExtraInitializers", new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
        ));

        var decoratorsArray = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(classDecorators),
            false);
        classDefinitionStatements = append(
            classDefinitionStatements,
            tx.createLet(ci.classDecoratorsName, decoratorsArray),
            tx.createLet(ci.classDescriptorName, nil),
            tx.createLet(ci.classExtraInitializersName, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false)),
            tx.createLet(ci.classThis, nil));

        if (len(classDecorators) > 0 && ci.hasStaticPrivateClassElements) {
            shouldTransformPrivateStaticElementsInClass = true;
            tx.shouldTransformPrivateStaticElementsInFile = true;
        }
    }

    var extendsClause = ast.GetHeritageClause(Ꮡnode, ast.KindExtendsKeyword);
    ж<ast.Node> extendsElement = default;
    if (extendsClause != nil) {
        var hc = extendsClause.AsHeritageClause();
        if (hc.Types != nil && len(hc.Types.Nodes) > 0) {
            extendsElement = hc.Types.Nodes[0];
        }
    }
    ж<astꓸExpression> extendsExpression = default;
    if (extendsElement != nil) {
        extendsExpression = tx.Transformer.Visitor().VisitNode((~extendsElement.AsExpressionWithTypeArguments()).Expression);
    }

    if (extendsExpression != nil) {
        ci.classSuper = f.NewUniqueNameEx("_classSuper", new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
        ));

        var unwrapped = ast.SkipOuterExpressions(extendsExpression, ast.OEKAll);
        var safeExtendsExpression = extendsExpression;
        if ((ast.IsClassExpression(unwrapped) && unwrapped.Name() == nil) || (ast.IsFunctionExpression(unwrapped) && unwrapped.Name() == nil) || ast.IsArrowFunction(unwrapped)) {
            safeExtendsExpression = f.NewCommaExpression(
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral("0", 0),
                extendsExpression);
        }
        classDefinitionStatements = append(classDefinitionStatements, tx.createLet(ci.classSuper, safeExtendsExpression));

        var updatedExtendsElement = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateExpressionWithTypeArguments(extendsElement.AsExpressionWithTypeArguments(), ci.classSuper, nil);
        var hc = extendsClause.AsHeritageClause();
        var updatedExtendsClause = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateHeritageClause(hc, hc.Token, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([updatedExtendsElement]));
        heritageClauses = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([updatedExtendsClause]);
    }

    ж<ast.Node> renamedClassThis = default;
    if (ci.classThis != nil) {
        renamedClassThis = ci.classThis;
    } else {
        renamedClassThis = f.NewThisExpression();
    }

    tx.enterClass(ci);

    leadingBlockStatements = append(leadingBlockStatements, tx.createMetadata(ci.metadataReference, ci.classSuper));

    var members = tx.nonConstructorClassElementVisitor.VisitNodes(node.MemberList());
    members = tx.constructorClassElementVisitor.VisitNodes(members);

    if (len(tx.pendingExpressions) > 0) {
        tx.outerThis = default;
        foreach (var (_, vᴛ1) in range(tx.pendingExpressions)) {
            var expr = vᴛ1;

            if ((ast.SubtreeFacts)(expr.SubtreeFacts() & ast.SubtreeContainsLexicalThis) != 0) {
                expr = tx.outerThisVisitor.VisitNode(expr);
            }
            var statement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expr);
            leadingBlockStatements = append(leadingBlockStatements, statement);
        }
        if (tx.outerThis != nil) {
            classDefinitionStatements = appendꓸꓸꓸ(
                slice<ж<astꓸStatement>>([tx.createLet(tx.outerThis, f.NewThisExpression())]),
                classDefinitionStatements);
        }
        tx.pendingExpressions = default;
    }
    tx.exitClass();

    if (len(ci.pendingInstanceInitializers) > 0 && ast.GetFirstConstructorWithBody(Ꮡnode) == nil) {
        var initializerStatements = tx.prepareConstructor(ci);
        if (len(initializerStatements) > 0) {
            var isDerivedClass = extendsElement != nil && ast.SkipOuterExpressions((~extendsElement.AsExpressionWithTypeArguments()).Expression, ast.OEKAll).Kind != ast.KindNullKeyword;
            var constructorStatements = slice<ж<astꓸStatement>>([]);
            if (isDerivedClass) {
                var spreadArguments = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewSpreadElement(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("arguments"));
                var superCall = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindSuperKeyword), nil, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([spreadArguments]), ast.NodeFlagsNone);
                constructorStatements = append(constructorStatements, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(superCall));
            }
            constructorStatements = appendꓸꓸꓸ(constructorStatements, initializerStatements);
            var constructorBody = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(constructorStatements), true);
            syntheticConstructor = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewConstructorDeclaration(nil, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), nil, nil, constructorBody);
        }
    }

    if (ci.staticMethodExtraInitializersName != nil) {
        classDefinitionStatements = append(
            classDefinitionStatements,
            tx.createLet(ci.staticMethodExtraInitializersName, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false)));
    }

    if (ci.instanceMethodExtraInitializersName != nil) {
        classDefinitionStatements = append(
            classDefinitionStatements,
            tx.createLet(ci.instanceMethodExtraInitializersName, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false)));
    }

    if (ci.ᏑmemberInfos.Size() > 0) {
        classDefinitionStatements = appendꓸꓸꓸ(classDefinitionStatements, tx.emitMemberInfoDeclarations(ci, true));
        classDefinitionStatements = appendꓸꓸꓸ(classDefinitionStatements, tx.emitMemberInfoDeclarations(ci, false));
    }

    leadingBlockStatements = appendꓸꓸꓸ(leadingBlockStatements, ci.staticNonFieldDecorationStatements);

    leadingBlockStatements = appendꓸꓸꓸ(leadingBlockStatements, ci.nonStaticNonFieldDecorationStatements);

    leadingBlockStatements = appendꓸꓸꓸ(leadingBlockStatements, ci.staticFieldDecorationStatements);

    leadingBlockStatements = appendꓸꓸꓸ(leadingBlockStatements, ci.nonStaticFieldDecorationStatements);

    if (ci.classDescriptorName != nil && ci.classDecoratorsName != nil && ci.classExtraInitializersName != nil && ci.classThis != nil) {
        var valueProperty = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), nil, nil, renamedClassThis);
        var classDescriptor = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([valueProperty]), false);
        var classDescriptorAssignment = f.NewAssignmentExpression(ci.classDescriptorName, classDescriptor);
        var classNameReference = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(renamedClassThis, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("name"), ast.NodeFlagsNone);

        var contextObj = f.NewESDecorateClassContextObject(classNameReference, ci.metadataReference);

        var esDecorateHelper = f.NewESDecorateHelper(
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword),
            classDescriptorAssignment,
            ci.classDecoratorsName,
            contextObj,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword),
            ci.classExtraInitializersName);
        var esDecorateStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(esDecorateHelper);
        ec.SetSourceMapRange(esDecorateStatement, transformers.MoveRangePastDecorators(Ꮡnode));
        leadingBlockStatements = append(leadingBlockStatements, esDecorateStatement);

        var classDescriptorValueRef = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(ci.classDescriptorName, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), ast.NodeFlagsNone);
        var classThisAssignment = f.NewAssignmentExpression(ci.classThis, classDescriptorValueRef);
        var classReferenceAssignment = f.NewAssignmentExpression(classReference, classThisAssignment);
        leadingBlockStatements = append(leadingBlockStatements, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(classReferenceAssignment));
    }

    leadingBlockStatements = append(leadingBlockStatements, tx.createSymbolMetadata(renamedClassThis, ci.metadataReference));

    if (len(ci.pendingStaticInitializers) > 0) {
        foreach (var (_, initializer) in range(ci.pendingStaticInitializers)) {
            var initializerStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(initializer);
            ec.SetSourceMapRange(initializerStatement, ec.SourceMapRange(initializer));
            trailingBlockStatements = append(trailingBlockStatements, initializerStatement);
        }
        ci.pendingStaticInitializers = default;
    }

    if (ci.classExtraInitializersName != nil) {
        var runClassInitializersHelper = f.NewRunInitializersHelper(renamedClassThis, ci.classExtraInitializersName, nil);
        var runClassInitializersStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(runClassInitializersHelper);
        if (node.Name() != nil) {
            ec.SetSourceMapRange(runClassInitializersStatement, node.Name().Loc);
        } else {
            ec.SetSourceMapRange(runClassInitializersStatement, transformers.MoveRangePastDecorators(Ꮡnode));
        }
        trailingBlockStatements = append(trailingBlockStatements, runClassInitializersStatement);
    }

    if (len(leadingBlockStatements) > 0 && len(trailingBlockStatements) > 0 && !ci.hasStaticInitializers) {
        leadingBlockStatements = appendꓸꓸꓸ(leadingBlockStatements, trailingBlockStatements);
        trailingBlockStatements = default;
    }

    ж<ast.Node> leadingStaticBlock = default;
    if (len(leadingBlockStatements) > 0) {
        leadingStaticBlock = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(
            nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(leadingBlockStatements), true));
    }

    if (leadingStaticBlock != nil && shouldTransformPrivateStaticElementsInClass) {
        ec.SetEmitFlags(leadingStaticBlock, printer.EFTransformPrivateStaticElements);
    }

    ж<ast.Node> trailingStaticBlock = default;
    if (len(trailingBlockStatements) > 0) {
        trailingStaticBlock = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(
            nil,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(trailingBlockStatements), true));
    }

    if (leadingStaticBlock != nil || syntheticConstructor != nil || trailingStaticBlock != nil) {
        var newMembers = new slice<ж<ast.Node>>(0, len(members.Nodes) + 3);

        nint existingNamedEvaluationHelperBlockIndex = -1;
        foreach (var (i, m) in range(members.Nodes)) {
            if (isClassNamedEvaluationHelperBlock(ec, m)) {
                existingNamedEvaluationHelperBlockIndex = i;
                break;
            }
        }

        if (leadingStaticBlock != nil) {
            newMembers = appendꓸꓸꓸ(newMembers, members.Nodes.slice(0, existingNamedEvaluationHelperBlockIndex + 1));
            newMembers = append(newMembers, leadingStaticBlock);
            newMembers = appendꓸꓸꓸ(newMembers, members.Nodes.slice(existingNamedEvaluationHelperBlockIndex + 1));
        } else {
            newMembers = appendꓸꓸꓸ(newMembers, members.Nodes);
        }

        if (syntheticConstructor != nil) {
            newMembers = append(newMembers, syntheticConstructor);
        }

        if (trailingStaticBlock != nil) {
            newMembers = append(newMembers, trailingStaticBlock);
        }

        var membersList = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newMembers);
        membersList.Loc = members.Loc;
        members = membersList;
    }

    var lexicalEnvironment = ec.EndVariableEnvironment();

    ж<ast.Node> classExpression = default;
    if (len(classDecorators) > 0) {
        classExpression = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassExpression(nil, nil, nil, heritageClauses, members);
        ec.SetOriginal(classExpression, Ꮡnode);
        if (ci.classThis != nil) {
            classExpression = injectClassThisAssignmentIfMissing(ec, f, classExpression, ci.classThis);
        }

        var classReferenceDeclaration = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(classReference, nil, nil, classExpression);
        var classReferenceVarDeclList = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([classReferenceDeclaration]), ast.NodeFlagsNone);
        ж<astꓸExpression> returnExpr = default;
        if (ci.classThis != nil) {
            returnExpr = f.NewAssignmentExpression(classReference, ci.classThis);
        } else {
            returnExpr = classReference;
        }
        classDefinitionStatements = append(
            classDefinitionStatements,
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, classReferenceVarDeclList),
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(returnExpr));
    } else {
        classExpression = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassExpression(nil, node.Name(), nil, heritageClauses, members);
        ec.SetOriginal(classExpression, Ꮡnode);
        classDefinitionStatements = append(classDefinitionStatements, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(classExpression));
    }

    if (shouldTransformPrivateStaticElementsInClass) {
        ec.AddEmitFlags(classExpression, printer.EFTransformPrivateStaticElements);
        foreach (var (_, member) in range(classExpression.Members())) {
            if ((ast.IsPrivateIdentifierClassElementDeclaration(member) || ast.IsAutoAccessorPropertyDeclaration(member)) && ast.HasStaticModifier(member)) {
                ec.AddEmitFlags(member, printer.EFTransformPrivateStaticElements);
            }
        }
    }

    var mergedStatements = ec.MergeEnvironment(classDefinitionStatements, lexicalEnvironment);
    return f.NewImmediatelyInvokedArrowFunction(mergedStatements);
}

internal static slice<ж<astꓸStatement>> emitMemberInfoDeclarations(this ref esDecoratorTransformer tx, ж<classInfo> ci, bool isStatic) {
    var f = tx.Transformer.Factory();
    slice<ж<astꓸStatement>> stmts = default;
    foreach (var (member, mi) in range<ж<ast.Node>, ж<memberInfo>>(ci.ᏑmemberInfos.Entries().Invoke)) {
        if (ast.IsStatic(member) != isStatic) {
            continue;
        }
        stmts = append(stmts, tx.createLet(mi.memberDecoratorsName, nil));
        if (mi.memberInitializersName != nil) {
            stmts = append(stmts, tx.createLet(mi.memberInitializersName, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false)));
        }
        if (mi.memberExtraInitializersName != nil) {
            stmts = append(stmts, tx.createLet(mi.memberExtraInitializersName, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(default), false)));
        }
        if (mi.memberDescriptorName != nil) {
            stmts = append(stmts, tx.createLet(mi.memberDescriptorName, nil));
        }
    }
    return stmts;
}

} partial class estransforms_package {

internal static bool isDecoratedClassLike(ж<ast.Node> node) {
    return ast.ClassOrConstructorParameterIsDecorated(false, node) || ast.ChildIsDecorated(false, node, nil);
}

} partial class estransformsᴛesDecoratorTransformer {

internal static ж<ast.Node> visitClassDeclaration(this ref esDecoratorTransformer tx, ж<ast.ClassDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isDecoratedClassLike(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var f = tx.Transformer.Factory();
        var ec = tx.Transformer.EmitContext();
        var statements = slice<ж<astꓸStatement>>([]);

        var originalClass = ec.MostOriginal(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (!ast.IsClassLike(originalClass)) {
            originalClass = Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        }
        ж<astꓸExpression> className = default;
        if (originalClass.Name() != nil) {
            className = f.NewStringLiteralFromNode(originalClass.Name());
        } else {
            className = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral("default", 0);
        }

        var isExport = ast.HasSyntacticModifier(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsExport);
        var isDefault = ast.HasSyntacticModifier(Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ast.ModifierFlagsDefault);

        var classNode = Ꮡnode.of(ast.ClassDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
        if (node.Name() == nil) {
            classNode = injectClassNamedEvaluationHelperBlockIfMissing(ec, classNode, className, nil);
        }

        if (isExport && isDefault) {
            var iife = tx.transformClassLike(classNode);
            if (classNode.Name() != nil) {
                var varDecl = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(f.GetLocalName(classNode), nil, nil, iife);
                ec.SetOriginal(varDecl, classNode);
                var varDecls = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsLet);
                var varStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, varDecls);
                statements = append(statements, varStatement);

                var exportStatement = f.NewExportDefault(f.GetDeclarationName(classNode));
                ec.SetOriginal(exportStatement, classNode);
                ec.AssignCommentRange(exportStatement, classNode);
                ec.SetSourceMapRange(exportStatement, transformers.MoveRangePastDecorators(classNode));
                statements = append(statements, exportStatement);
            } else {
                var exportStatement = f.NewExportDefault(iife);
                ec.SetOriginal(exportStatement, classNode);
                ec.AssignCommentRange(exportStatement, classNode);
                ec.SetSourceMapRange(exportStatement, transformers.MoveRangePastDecorators(classNode));
                statements = append(statements, exportStatement);
            }
        } else {
            debug.Assert(classNode.Name() != nil, "A class declaration that is not a default export must have a name.");
            var iife = tx.transformClassLike(classNode);
            var modifiersΔ1 = tx.exportStrippingModifierVisitor.VisitModifiers(classNode.Modifiers());

            var declName = f.GetLocalNameEx(classNode, new printer.AssignedNameOptions(AllowSourceMaps: true));
            var varDecl = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(declName, nil, nil, iife);
            ec.SetOriginal(varDecl, classNode);
            var varDecls = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsLet);
            var varStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(modifiersΔ1, varDecls);
            ec.SetOriginal(varStatement, classNode);
            ec.AssignCommentRange(varStatement, classNode);
            statements = append(statements, varStatement);

            if (isExport) {
                var exportStatement = f.NewExternalModuleExport(declName);
                ec.SetOriginal(exportStatement, classNode);
                statements = append(statements, exportStatement);
            }
        }

        return transformers.SingleOrMany(statements, f);
    }

    var modifiers = tx.modifierVisitor.VisitModifiers(Ꮡnode.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers());
    var heritageClauses = tx.Transformer.Visitor().VisitNodes(node.HeritageClauses);
    tx.enterClass(nil);
    var members = tx.classElementVisitor.VisitNodes(node.Members);
    tx.exitClass();
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(Ꮡnode, modifiers, node.Name(), nil, heritageClauses, members);
}

internal static ж<ast.Node> visitClassExpression(this ref esDecoratorTransformer tx, ж<ast.ClassExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isDecoratedClassLike(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        var iife = tx.transformClassLike(Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        tx.Transformer.EmitContext().SetOriginal(iife, Ꮡnode.of(ast.ClassExpression.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        return iife;
    }

    var modifiers = tx.modifierVisitor.VisitModifiers(Ꮡnode.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers());
    var heritageClauses = tx.Transformer.Visitor().VisitNodes(node.HeritageClauses);
    tx.enterClass(nil);
    var members = tx.classElementVisitor.VisitNodes(node.Members);
    tx.exitClass();
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(Ꮡnode, modifiers, node.Name(), nil, heritageClauses, members);
}

internal static slice<ж<astꓸStatement>> prepareConstructor(this ref esDecoratorTransformer tx, ж<classInfo> Ꮡci) {
    ref var ci = ref Ꮡci.DerefOrNull();

    if (len(ci.pendingInstanceInitializers) == 0) {
        return default;
    }
    var f = tx.Transformer.Factory();
    var statements = slice<ж<astꓸStatement>>([
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(f.InlineExpressions(ci.pendingInstanceInitializers))
    ]);
    ci.pendingInstanceInitializers = default;
    return statements;
}

internal static slice<ж<astꓸStatement>> transformConstructorBodyWorker(this ref esDecoratorTransformer tx, slice<ж<astꓸStatement>> statementsOut, slice<ж<astꓸStatement>> statementsIn, nint statementOffset, slice<nint> superPath, nint superPathDepth, slice<ж<astꓸStatement>> initializerStatements) {
    nint superStatementIndex = superPath[superPathDepth];
    if (superStatementIndex > statementOffset) {
        foreach (var (_, s) in range(statementsIn.slice(statementOffset, superStatementIndex))) {
            statementsOut = append(statementsOut, tx.Transformer.Visitor().VisitNode(s));
        }
    }

    var superStatement = statementsIn[superStatementIndex];
    if (ast.IsTryStatement(superStatement)) {
        var tryBlockNode = superStatement.AsTryStatement().TryBlock;
        var tryBlock = tryBlockNode.AsBlock();
        var tryBlockStatements = tx.transformConstructorBodyWorker(default, (~tryBlock).Statements.Nodes, 0, superPath, superPathDepth + 1, initializerStatements);

        var newTryBlock = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(tryBlockStatements), true);
        newTryBlock.Loc = tryBlockNode.Loc;

        ж<ast.Node> catchClause = default;
        if (superStatement.AsTryStatement().CatchClause != nil) {
            catchClause = tx.Transformer.Visitor().VisitNode(superStatement.AsTryStatement().CatchClause);
        }
        ж<ast.Node> finallyBlock = default;
        if (superStatement.AsTryStatement().FinallyBlock != nil) {
            finallyBlock = tx.Transformer.Visitor().VisitNode(superStatement.AsTryStatement().FinallyBlock);
        }
        var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTryStatement(superStatement.AsTryStatement(), newTryBlock, catchClause, finallyBlock);
        statementsOut = append(statementsOut, updated);
    } else {
        statementsOut = append(statementsOut, tx.Transformer.Visitor().VisitNode(superStatement));
        statementsOut = appendꓸꓸꓸ(statementsOut, initializerStatements);
    }

    if (superStatementIndex + 1 < len(statementsIn)) {
        foreach (var (_, s) in range(statementsIn.slice(superStatementIndex + 1))) {
            statementsOut = append(statementsOut, tx.Transformer.Visitor().VisitNode(s));
        }
    }
    return statementsOut;
}

internal static ж<ast.Node> visitConstructorDeclaration(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    tx.enterClassElement(Ꮡnode);
    var modifiers = tx.modifierVisitor.VisitModifiers(node.Modifiers());
    var parameters = tx.Transformer.Visitor().VisitNodes(node.ParameterList());

    ж<ast.Node> body = default;
    var ctor = node.AsConstructorDeclaration();
    if ((~ctor).Body != nil && tx.classInfoStack != nil) {
        var initializerStatements = tx.prepareConstructor(tx.classInfoStack);
        if (len(initializerStatements) > 0) {
            var stmts = slice<ж<astꓸStatement>>([]);
            var (prologue, rest) = tx.Transformer.Factory().SplitStandardPrologue((~(~ctor).Body.AsBlock()).Statements.Nodes);
            stmts = appendꓸꓸꓸ(stmts, prologue);

            var superStatementIndices = transformers.FindSuperStatementIndexPath(rest, 0);
            if (len(superStatementIndices) > 0) {
                stmts = tx.transformConstructorBodyWorker(stmts, rest, 0, superStatementIndices, 0, initializerStatements);
            } else {
                stmts = appendꓸꓸꓸ(stmts, initializerStatements);
                var (visited, _) = tx.Transformer.Visitor().VisitSlice(rest);
                stmts = appendꓸꓸꓸ(stmts, visited);
            }

            body = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(stmts), true);
            tx.Transformer.EmitContext().SetOriginal(body, (~ctor).Body.AsNode());
            body.Loc = ctor.Value.Body.Loc;
        }
    }

    if (body == nil) {
        body = tx.Transformer.Visitor().VisitNode((~ctor).Body.AsNode());
    }
    tx.exitClassElement();
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateConstructorDeclaration(ctor, modifiers, nil, parameters, nil, nil, body);
}

internal static ж<ast.Node> finishClassElement(this ref esDecoratorTransformer tx, ж<ast.Node> updated, ж<ast.Node> original) {
    if (updated != original) {
        tx.Transformer.EmitContext().AssignCommentRange(updated, original);
        tx.Transformer.EmitContext().SetSourceMapRange(updated, transformers.MoveRangePastDecorators(original));
    }
    return updated;
}

} partial class estransforms_package {

partial struct partialResult {
    internal ж<ast.ModifierList> modifiers;
    internal ж<astꓸExpression> referencedName;
    internal ж<ast.Node> name;
    internal ж<astꓸIdentifierNode> initializersName;
    internal ж<astꓸIdentifierNode> extraInitializersName;
    internal ж<astꓸIdentifierNode> descriptorName;
    internal ж<astꓸIdentifierNode> thisArg;
}

// type createDescriptorFunc is a methodless func type — rendered inline as its base delegate

} partial class estransformsᴛesDecoratorTransformer {

internal static partialResult partialTransformClassElement(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡmember, ж<classInfo> Ꮡci, Func<ж<ast.Node>, ж<ast.ModifierList>, ж<astꓸExpression>> createDescriptor) {
    ref var member = ref Ꮡmember.DerefOrNull();
    ref var ci = ref Ꮡci.DerefOrNull();

    var f = tx.Transformer.Factory();
    var ec = tx.Transformer.EmitContext();

    if (Ꮡci == nil) {
        var modifiersΔ1 = tx.modifierVisitor.VisitModifiers(member.Modifiers());
        tx.enterName();
        var name = tx.visitPropertyName(member.Name());
        tx.exitName();
        return new partialResult(modifiers: modifiersΔ1, name: name);
    }

    var savedClassThis = tx.classThis;
    tx.classThis = default;
    var memberDecorators = tx.transformAllDecoratorsOfDeclaration(member.Decorators());
    tx.classThis = savedClassThis;
    var modifiers = tx.modifierVisitor.VisitModifiers(member.Modifiers());

    partialResult result = default;
    result.modifiers = modifiers;

    if (len(memberDecorators) > 0) {
        var memberDecoratorsName = tx.createHelperVariable(Ꮡmember, "decorators");
        var memberDecoratorsArray = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(memberDecorators),
            false);
        var memberDecoratorsAssignment = f.NewAssignmentExpression(memberDecoratorsName, memberDecoratorsArray);
        var mi = Ꮡ(new memberInfo(memberDecoratorsName: memberDecoratorsName));
        ci.memberInfos.Set(Ꮡmember, mi);
        tx.pendingExpressions = append(tx.pendingExpressions, memberDecoratorsAssignment);

        @string kind = default;
        switch (ᐧ) {
        case {} when ast.IsGetAccessorDeclaration(Ꮡmember): {
            kind = "getter";
            break;
        }
        case {} when ast.IsSetAccessorDeclaration(Ꮡmember): {
            kind = "setter";
            break;
        }
        case {} when ast.IsMethodDeclaration(Ꮡmember): {
            kind = "method";
            break;
        }
        case {} when ast.IsAutoAccessorPropertyDeclaration(Ꮡmember): {
            kind = "accessor";
            break;
        }
        case {} when ast.IsPropertyDeclaration(Ꮡmember): {
            kind = "field";
            break;
        }
        default: {
            debug.Fail("Unexpected class element kind.");
            break;
        }}

        bool propertyNameComputed = default;
        ж<astꓸExpression> propertyNameExpr = default;
        if (member.Name() != nil && (ast.IsIdentifier(member.Name()) || ast.IsPrivateIdentifier(member.Name()))) {
            propertyNameComputed = false;
            propertyNameExpr = member.Name();
        } else if (member.Name() != nil && ast.IsPropertyNameLiteral(member.Name())) {
            propertyNameComputed = true;
            propertyNameExpr = f.NewStringLiteralFromNode(member.Name());
        } else if (member.Name() != nil && ast.IsComputedPropertyName(member.Name())) {
            var cpn = member.Name().AsComputedPropertyName();
            if (ast.IsPropertyNameLiteral((~cpn).Expression) && !ast.IsIdentifier((~cpn).Expression)) {
                propertyNameComputed = true;
                propertyNameExpr = f.NewStringLiteralFromNode((~cpn).Expression);
            } else {
                tx.enterName();
                (result.referencedName, result.name) = tx.visitReferencedPropertyName(member.Name());
                tx.exitName();
                propertyNameComputed = true;
                propertyNameExpr = result.referencedName;
            }
        }

        var contextObj = f.NewESDecorateClassElementContextObject(
            kind,
            propertyNameComputed,
            propertyNameExpr,
            ast.IsStatic(Ꮡmember),
            member.Name() != nil && ast.IsPrivateIdentifier(member.Name()),
            ast.IsPropertyDeclaration(Ꮡmember) || ast.IsGetAccessorDeclaration(Ꮡmember) || ast.IsMethodDeclaration(Ꮡmember),
            ast.IsPropertyDeclaration(Ꮡmember) || ast.IsSetAccessorDeclaration(Ꮡmember),
            ci.metadataReference);

        if (ast.IsMethodOrAccessor(Ꮡmember)) {
            var methodExtraInitializersName = ci.instanceMethodExtraInitializersName;
            if (ast.IsStatic(Ꮡmember)) {
                methodExtraInitializersName = ci.staticMethodExtraInitializersName;
            }
            debug.Assert(methodExtraInitializersName != nil, "methodExtraInitializersName should be defined");

            ж<astꓸExpression> descriptorArg = default;
            if (ast.IsPrivateIdentifierClassElementDeclaration(Ꮡmember) && createDescriptor != nil) {
                var asyncMods = tx.asyncOnlyModifierVisitor.VisitModifiers(modifiers);
                var descriptor = createDescriptor(Ꮡmember, asyncMods);
                mi.memberDescriptorName = tx.createHelperVariable(Ꮡmember, "descriptor");
                result.descriptorName = mi.memberDescriptorName;
                descriptorArg = f.NewAssignmentExpression(mi.memberDescriptorName, descriptor);
            } else {
                descriptorArg = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword);
            }

            var esDecorateExpr = f.NewESDecorateHelper(
                f.NewThisExpression(),
                descriptorArg,
                memberDecoratorsName,
                contextObj,
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword),
                methodExtraInitializersName);
            var esDecorateStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(esDecorateExpr);
            ec.SetSourceMapRange(esDecorateStatement, transformers.MoveRangePastDecorators(Ꮡmember));
            tx.appendDecorationStatement(Ꮡci, Ꮡmember, esDecorateStatement);
        } else if (ast.IsPropertyDeclaration(Ꮡmember)) {
            mi.memberInitializersName = tx.createHelperVariable(Ꮡmember, "initializers");
            mi.memberExtraInitializersName = tx.createHelperVariable(Ꮡmember, "extraInitializers");
            result.initializersName = mi.memberInitializersName;
            result.extraInitializersName = mi.memberExtraInitializersName;
            if (ast.IsStatic(Ꮡmember)) {
                result.thisArg = ci.classThis;
            }

            ж<ast.Node> ctorArg = default;
            if (ast.IsAutoAccessorPropertyDeclaration(Ꮡmember)) {
                ctorArg = f.NewThisExpression();
            } else {
                ctorArg = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword);
            }

            ж<astꓸExpression> descriptorArg = default;
            if (ast.IsPrivateIdentifierClassElementDeclaration(Ꮡmember) && ast.HasAccessorModifier(Ꮡmember) && createDescriptor != nil) {
                var descriptor = createDescriptor(Ꮡmember, nil);
                mi.memberDescriptorName = tx.createHelperVariable(Ꮡmember, "descriptor");
                result.descriptorName = mi.memberDescriptorName;
                descriptorArg = f.NewAssignmentExpression(mi.memberDescriptorName, descriptor);
            } else {
                descriptorArg = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword);
            }

            var esDecorateExpr = f.NewESDecorateHelper(
                ctorArg,
                descriptorArg,
                memberDecoratorsName,
                contextObj,
                mi.memberInitializersName,
                mi.memberExtraInitializersName);
            var esDecorateStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(esDecorateExpr);
            ec.SetSourceMapRange(esDecorateStatement, transformers.MoveRangePastDecorators(Ꮡmember));
            tx.appendDecorationStatement(Ꮡci, Ꮡmember, esDecorateStatement);
        }
    }

    if (result.name == nil) {
        tx.enterName();
        result.name = tx.visitPropertyName(member.Name());
        tx.exitName();
    }

    if ((modifiers == nil || len(modifiers.Nodes) == 0) && (ast.IsMethodDeclaration(Ꮡmember) || ast.IsPropertyDeclaration(Ꮡmember))) {
        ec.SetEmitFlags(result.name, printer.EFNoLeadingComments);
    }

    return result;
}

internal static void appendDecorationStatement(this ref esDecoratorTransformer tx, ж<classInfo> Ꮡci, ж<ast.Node> member, ж<astꓸStatement> stmt) {
    ref var ci = ref Ꮡci.DerefOrNull();

    if (ast.IsMethodOrAccessor(member) || ast.IsAutoAccessorPropertyDeclaration(member)) {
        if (ast.IsStatic(member)) {
            ci.staticNonFieldDecorationStatements = append(ci.staticNonFieldDecorationStatements, stmt);
        } else {
            ci.nonStaticNonFieldDecorationStatements = append(ci.nonStaticNonFieldDecorationStatements, stmt);
        }
    } else if (ast.IsPropertyDeclaration(member) && !ast.IsAutoAccessorPropertyDeclaration(member)) {
        if (ast.IsStatic(member)) {
            ci.staticFieldDecorationStatements = append(ci.staticFieldDecorationStatements, stmt);
        } else {
            ci.nonStaticFieldDecorationStatements = append(ci.nonStaticFieldDecorationStatements, stmt);
        }
    } else {
        debug.Fail("Unexpected class element kind.");
    }
}

internal static ж<ast.Node> visitMethodDeclaration(this ж<esDecoratorTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    tx.enterClassElement(Ꮡnode);
    var result = tx.partialTransformClassElement(Ꮡnode, tx.classInfoStack, new Func<ж<ast.Node>, ж<ast.ModifierList>, ж<astꓸExpression>>(Ꮡtx.createMethodDescriptorObject));
    if (result.descriptorName != nil) {
        tx.exitClassElement();
        return tx.finishClassElement(tx.createMethodDescriptorForwarder(result.modifiers, result.name, result.descriptorName), Ꮡnode);
    }
    var parameters = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNodes(node.ParameterList());
    var body = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNode(node.Body());
    tx.exitClassElement();
    var method = node.AsMethodDeclaration();
    return tx.finishClassElement(
        Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateMethodDeclaration(method, result.modifiers, method.AsteriskToken, result.name, nil, nil, parameters, nil, nil, body),
        Ꮡnode);
}

internal static ж<ast.Node> visitGetAccessorDeclaration(this ж<esDecoratorTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    tx.enterClassElement(Ꮡnode);
    var result = tx.partialTransformClassElement(Ꮡnode, tx.classInfoStack, new Func<ж<ast.Node>, ж<ast.ModifierList>, ж<astꓸExpression>>(Ꮡtx.createGetAccessorDescriptorObject));
    if (result.descriptorName != nil) {
        tx.exitClassElement();
        return tx.finishClassElement(tx.createGetAccessorDescriptorForwarder(result.modifiers, result.name, result.descriptorName), Ꮡnode);
    }
    var parameters = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNodes(node.ParameterList());
    var body = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNode(node.Body());
    tx.exitClassElement();
    var accessor = node.AsGetAccessorDeclaration();
    return tx.finishClassElement(
        Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateGetAccessorDeclaration(accessor, result.modifiers, result.name, nil, parameters, nil, nil, body),
        Ꮡnode);
}

internal static ж<ast.Node> visitSetAccessorDeclaration(this ж<esDecoratorTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    tx.enterClassElement(Ꮡnode);
    var result = tx.partialTransformClassElement(Ꮡnode, tx.classInfoStack, new Func<ж<ast.Node>, ж<ast.ModifierList>, ж<astꓸExpression>>(Ꮡtx.createSetAccessorDescriptorObject));
    if (result.descriptorName != nil) {
        tx.exitClassElement();
        return tx.finishClassElement(tx.createSetAccessorDescriptorForwarder(result.modifiers, result.name, result.descriptorName), Ꮡnode);
    }
    var parameters = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNodes(node.ParameterList());
    var body = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNode(node.Body());
    tx.exitClassElement();
    var accessor = node.AsSetAccessorDeclaration();
    return tx.finishClassElement(
        Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSetAccessorDeclaration(accessor, result.modifiers, result.name, nil, parameters, nil, nil, body),
        Ꮡnode);
}

internal static ж<ast.Node> visitClassStaticBlockDeclaration(this ref esDecoratorTransformer tx, ж<ast.Node> node) {
    tx.enterClassElement(node);
    var f = tx.Transformer.Factory();

    ж<ast.Node> result = default;
    if (isClassNamedEvaluationHelperBlock(tx.Transformer.EmitContext(), node)) {
        result = tx.Transformer.Visitor().VisitEachChild(node);
        var assignedName = tx.Transformer.EmitContext().AssignedName(node); if (assignedName != nil && result != node) {
            tx.Transformer.EmitContext().SetAssignedName(result, assignedName);
        }
    } else if (isClassThisAssignmentBlock(tx.Transformer.EmitContext(), node)) {
        var savedClassThis = tx.classThis;
        tx.classThis = default;
        result = tx.Transformer.Visitor().VisitEachChild(node);
        tx.classThis = savedClassThis;
    } else {
        var ec = tx.Transformer.EmitContext();
        ec.StartVariableEnvironment();
        result = tx.Transformer.Visitor().VisitEachChild(node);
        var varStatements = ec.EndVariableEnvironment();
        if (len(varStatements) > 0) {
            var blockBody = (~result.AsClassStaticBlockDeclaration()).Body.AsBlock();
            var newStmts = new slice<ж<astꓸStatement>>(0, len(varStatements) + len((~blockBody).Statements.Nodes));
            newStmts = appendꓸꓸꓸ(newStmts, varStatements);
            newStmts = appendꓸꓸꓸ(newStmts, (~blockBody).Statements.Nodes);
            result = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newStmts), blockBody.MultiLine));
        }
        if (tx.classInfoStack != nil) {
            tx.classInfoStack.hasStaticInitializers = true;
            if (len(tx.classInfoStack.pendingStaticInitializers) > 0) {
                var stmts = slice<ж<ast.Node>>([]);
                foreach (var (_, init) in range(tx.classInfoStack.pendingStaticInitializers)) {
                    var initStmt = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(init);
                    tx.Transformer.EmitContext().SetSourceMapRange(initStmt, tx.Transformer.EmitContext().SourceMapRange(init));
                    stmts = append(stmts, initStmt);
                }
                var body = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(stmts), true);
                var staticBlock = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(nil, body);
                tx.classInfoStack.pendingStaticInitializers = default;
                tx.exitClassElement();
                return transformers.SingleOrMany([staticBlock, result], tx.Transformer.Factory());
            }
        }
    }

    tx.exitClassElement();
    return result;
}

internal static ж<ast.Node> visitPropertyDeclaration(this ж<esDecoratorTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).EmitContext(), Ꮡnode, isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).EmitContext(), Ꮡnode, canIgnoreEmptyStringLiteralInAssignedName(node.Initializer()), ""); node = ref Ꮡnode.DerefOrNull();
    }

    tx.enterClassElement(Ꮡnode);

    debug.Assert(!ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsAmbient), "Not yet implemented.");

    var f = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Factory();
    var ec = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).EmitContext();

    Func<ж<ast.Node>, ж<ast.ModifierList>, ж<astꓸExpression>> createDescriptor = default;
    if (ast.HasAccessorModifier(Ꮡnode)) {
        createDescriptor = (ж<ast.Node> p1, ж<ast.ModifierList> p2) => Ꮡtx.createAccessorPropertyDescriptorObject(p1, p2);
    }
    var result = tx.partialTransformClassElement(Ꮡnode, tx.classInfoStack, createDescriptor);

    ec.StartVariableEnvironment();

    var initializer = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNode(node.Initializer());
    if (result.initializersName != nil) {
        ж<ast.Node> thisArg = default;
        if (result.thisArg != nil) {
            thisArg = result.thisArg;
        } else {
            thisArg = f.NewThisExpression();
        }
        if (initializer == nil) {
            initializer = f.NewVoidZeroExpression();
        }
        initializer = f.NewRunInitializersHelper(thisArg, result.initializersName, initializer);
    }

    if (ast.IsStatic(Ꮡnode) && tx.classInfoStack != nil && initializer != nil) {
        tx.classInfoStack.hasStaticInitializers = true;
    }

    var declarations = ec.EndVariableEnvironment();
    if (len(declarations) > 0) {
        var stmts = new slice<ж<astꓸStatement>>(len(declarations) + 1);
        copy(stmts, declarations);
        stmts[len(declarations)] = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(initializer);
        initializer = f.NewImmediatelyInvokedArrowFunction(stmts);
    }

    if (tx.classInfoStack != nil) {
        if (ast.IsStatic(Ꮡnode)) {
            initializer = tx.injectPendingInitializers(tx.classInfoStack, true, initializer);
            if (result.extraInitializersName != nil) {
                ж<ast.Node> thisArg = default;
                if (tx.classInfoStack.classThis != nil) {
                    thisArg = tx.classInfoStack.classThis;
                } else {
                    thisArg = f.NewThisExpression();
                }
                tx.classInfoStack.pendingStaticInitializers = append(
                    tx.classInfoStack.pendingStaticInitializers,
                    f.NewRunInitializersHelper(thisArg, result.extraInitializersName, nil));
            }
        } else {
            initializer = tx.injectPendingInitializers(tx.classInfoStack, false, initializer);
            if (result.extraInitializersName != nil) {
                tx.classInfoStack.pendingInstanceInitializers = append(
                    tx.classInfoStack.pendingInstanceInitializers,
                    f.NewRunInitializersHelper(f.NewThisExpression(), result.extraInitializersName, nil));
            }
        }
    }

    tx.exitClassElement();

    if (ast.HasAccessorModifier(Ꮡnode) && result.descriptorName != nil) {
        var commentRange = ec.CommentRange(Ꮡnode);
        var sourceMapRange = ec.SourceMapRange(Ꮡnode);

        var propName = node.Name();
        var getterName = result.name;
        var setterName = result.name;
        if (ast.IsComputedPropertyName(propName) && !transformers.IsSimpleInlineableExpression(propName.Expression())) {
            var cacheAssignment = findComputedPropertyNameCacheAssignment(ref (ec).DerefOrNull(), propName);
            if (cacheAssignment != nil) {
                getterName = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(propName.AsComputedPropertyName(), Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNode(propName.Expression()));
                setterName = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(propName.AsComputedPropertyName(), cacheAssignment.Left);
            } else {
                var temp = f.NewTempVariable();
                ec.SetSourceMapRange(temp, propName.Expression().Loc);
                ec.AddVariableDeclaration(temp);
                var expression = Ꮡtx.of(esDecoratorTransformer.ᏑTransformer).Visitor().VisitNode(propName.Expression());
                var assignment = f.NewAssignmentExpression(temp, expression);
                ec.SetSourceMapRange(assignment, propName.Expression().Loc);
                getterName = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(propName.AsComputedPropertyName(), assignment);
                setterName = f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(propName.AsComputedPropertyName(), temp);
            }
        }

        var modifiersWithoutAccessor = tx.accessorStrippingModifierVisitor.VisitModifiers(result.modifiers);

        var backingField = createAccessorPropertyBackingField(f, node.AsPropertyDeclaration(), modifiersWithoutAccessor, initializer);
        ec.SetOriginal(backingField, Ꮡnode);
        ec.SetEmitFlags(backingField, printer.EFNoComments);
        ec.SetSourceMapRange(backingField, sourceMapRange);
        ec.SetSourceMapRange(backingField.AsPropertyDeclaration().Name(), ec.SourceMapRange(node.Name()));

        var getter = tx.createGetAccessorDescriptorForwarder(modifiersWithoutAccessor, getterName, result.descriptorName);
        ec.SetOriginal(getter, Ꮡnode);
        ec.SetCommentRange(getter, commentRange);
        ec.SetSourceMapRange(getter, sourceMapRange);

        var setter = tx.createSetAccessorDescriptorForwarder(modifiersWithoutAccessor, setterName, result.descriptorName);
        ec.SetOriginal(setter, Ꮡnode);
        ec.SetEmitFlags(setter, printer.EFNoComments);
        ec.SetSourceMapRange(setter, sourceMapRange);

        return transformers.SingleOrMany([backingField, getter, setter], f);
    }

    var prop = node.AsPropertyDeclaration();
    return tx.finishClassElement(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyDeclaration(prop, result.modifiers, result.name, nil, nil, initializer),
        Ꮡnode);
}

internal static ж<ast.Node> visitThisExpression(this ref esDecoratorTransformer tx, ж<ast.Node> node) {
    if (tx.classThis != nil) {
        return tx.classThis;
    }
    return node;
}

internal static ж<ast.Node> visitCallExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var call = node.AsCallExpression();
    if (ast.IsSuperProperty((~call).Expression) && tx.classThis != nil) {
        var expression = tx.Transformer.Visitor().VisitNode((~call).Expression);
        var argumentsList = tx.Transformer.Visitor().VisitNodes((~call).Arguments);
        var invocation = tx.Transformer.Factory().NewFunctionCallCall(expression, tx.classThis, argumentsList.Nodes);
        tx.Transformer.EmitContext().SetOriginal(invocation, Ꮡnode);
        invocation.Loc = node.Loc;
        return invocation;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitTaggedTemplateExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var tte = node.AsTaggedTemplateExpression();
    if (ast.IsSuperProperty(tte.Tag) && tx.classThis != nil) {
        var tag = tx.Transformer.Visitor().VisitNode(tte.Tag);
        var boundTag = tx.Transformer.Factory().NewFunctionBindCall(tag, tx.classThis, []);
        tx.Transformer.EmitContext().SetOriginal(boundTag, Ꮡnode);
        boundTag.Loc = node.Loc;
        var template = tx.Transformer.Visitor().VisitNode(tte.Template);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateTaggedTemplateExpression(tte, boundTag, nil, nil, template, tte.Flags);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitPropertyAccessExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var pa = node.AsPropertyAccessExpression();
    if (ast.IsSuperProperty(Ꮡnode) && ast.IsIdentifier(pa.Name()) && tx.classThis != nil && tx.classSuper != nil) {
        var propertyName = tx.Transformer.Factory().NewStringLiteralFromNode(pa.Name());
        var superProperty = tx.Transformer.Factory().NewReflectGetCall(tx.classSuper, propertyName, tx.classThis);
        tx.Transformer.EmitContext().SetOriginal(superProperty, (~pa).Expression);
        superProperty.Loc = pa.Value.Expression.Loc;
        return superProperty;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitElementAccessExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var ea = node.AsElementAccessExpression();
    if (ast.IsSuperProperty(Ꮡnode) && tx.classThis != nil && tx.classSuper != nil) {
        var propertyName = tx.Transformer.Visitor().VisitNode(ea.ArgumentExpression);
        var superProperty = tx.Transformer.Factory().NewReflectGetCall(tx.classSuper, propertyName, tx.classThis);
        tx.Transformer.EmitContext().SetOriginal(superProperty, (~ea).Expression);
        superProperty.Loc = ea.Value.Expression.Loc;
        return superProperty;
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitParameterDeclaration(this ref esDecoratorTransformer tx, ж<ast.ParameterDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var paramNode = Ꮡnode.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), paramNode, isAnonymousClassNeedingAssignedName)) {
        paramNode = transformNamedEvaluation(tx.Transformer.EmitContext(), paramNode, canIgnoreEmptyStringLiteralInAssignedName(paramNode.Initializer()), "");
        Ꮡnode = paramNode.AsParameterDeclaration(); node = ref Ꮡnode.DerefOrNull();
    }

    var updated = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParameterDeclaration(
        Ꮡnode,
        nil,
        node.DotDotDotToken,
        tx.Transformer.Visitor().VisitNode(node.Name()),
        nil,
        nil,
        tx.Transformer.Visitor().VisitNode(node.Initializer));
    if (updated != paramNode) {
        tx.Transformer.EmitContext().SetCommentRange(updated, paramNode.Loc);
        var newLoc = transformers.MoveRangePastModifiers(paramNode);
        updated.Loc = newLoc;
        tx.Transformer.EmitContext().SetSourceMapRange(updated, newLoc);
        tx.Transformer.EmitContext().SetEmitFlags(updated.Name(), printer.EFNoTrailingSourceMap);
    }
    return updated;
}

internal static ж<ast.Node> visitNamedEvaluationSite(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode, ж<ast.Node> classExpr) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode, isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode, canIgnoreEmptyStringLiteralInAssignedName(classExpr), ""); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

} partial class estransforms_package {

internal static bool isAnonymousClassNeedingAssignedName(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsClassExpression(Ꮡnode) && node.Name() == nil && isDecoratedClassLike(Ꮡnode);
}

internal static bool canIgnoreEmptyStringLiteralInAssignedName(ж<ast.Node> node) {
    if (node == nil) {
        return false;
    }
    var innerExpression = ast.SkipOuterExpressions(node, ast.OEKAll);
    return ast.IsClassExpression(innerExpression) && innerExpression.Name() == nil && !ast.ClassOrConstructorParameterIsDecorated(false, innerExpression);
}

} partial class estransformsᴛesDecoratorTransformer {

internal static ж<ast.Node> visitForStatement(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var forStmt = node.AsForStatement();
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateForStatement(
        forStmt,
        tx.discardedVisitor.VisitNode((~forStmt).Initializer),
        tx.Transformer.Visitor().VisitNode(forStmt.Condition),
        tx.discardedVisitor.VisitNode(forStmt.Incrementor),
        tx.Transformer.EmitContext().VisitIterationBody((~forStmt).Statement, tx.Transformer.Visitor()));
}

internal static ж<ast.Node> visitExpressionStatement(this ref esDecoratorTransformer tx, ж<ast.Node> node) {
    return tx.discardedVisitor.VisitEachChild(node);
}

internal static ж<ast.Node> visitBinaryExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode, bool discarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var ec = tx.Transformer.EmitContext();
    var bin = node.AsBinaryExpression();

    if (ast.IsDestructuringAssignment(Ꮡnode)) {
        var left = tx.visitAssignmentPattern(bin.Left);
        var right = tx.Transformer.Visitor().VisitNode(bin.Right);
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(bin, nil, left, nil, bin.OperatorToken, right);
    }

    if (ast.IsAssignmentExpression(Ꮡnode, false)) {
        if (isNamedEvaluationAnd(ec, Ꮡnode, isAnonymousClassNeedingAssignedName)) {
            Ꮡnode = transformNamedEvaluation(ec, Ꮡnode, canIgnoreEmptyStringLiteralInAssignedName(bin.Right), ""); node = ref Ꮡnode.DerefOrNull();
            return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
        }

        if (ast.IsSuperProperty(bin.Left) && tx.classThis != nil && tx.classSuper != nil) {
            ж<astꓸExpression> setterName = default;
            if (ast.IsElementAccessExpression(bin.Left)) {
                setterName = tx.Transformer.Visitor().VisitNode(bin.Left.AsElementAccessExpression().ArgumentExpression);
            } else if (ast.IsPropertyAccessExpression(bin.Left) && ast.IsIdentifier(bin.Left.AsPropertyAccessExpression().Name())) {
                setterName = f.NewStringLiteralFromNode(bin.Left.AsPropertyAccessExpression().Name());
            }
            if (setterName != nil) {
                var expression = tx.Transformer.Visitor().VisitNode(bin.Right);
                if (ast.IsCompoundAssignment(bin.OperatorToken.Kind)) {
                    var getterName = setterName;
                    if (!transformers.IsSimpleInlineableExpression(setterName)) {
                        getterName = f.NewTempVariable();
                        ec.AddVariableDeclaration(getterName);
                        setterName = f.NewAssignmentExpression(getterName, setterName);
                    }
                    var superPropertyGet = f.NewReflectGetCall(tx.classSuper, getterName, tx.classThis);
                    ec.SetOriginal(superPropertyGet, bin.Left);
                    superPropertyGet.Loc = bin.Left.Loc;
                    expression = f.of(printer.NodeFactory.ᏑΔNodeFactory).AsNodeFactory().NewBinaryExpression(
                        nil,
                        superPropertyGet,
                        nil,
                        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(transformers.GetNonAssignmentOperatorForCompoundAssignment(bin.OperatorToken.Kind)),
                        expression);
                    expression.Loc = node.Loc;
                }
                ж<astꓸExpression> temp = default;
                if (!discarded) {
                    temp = f.NewTempVariable();
                    ec.AddVariableDeclaration(temp);
                }
                if (temp != nil) {
                    expression = f.NewAssignmentExpression(temp, expression);
                    expression.Loc = node.Loc;
                }
                expression = f.NewReflectSetCall(tx.classSuper, setterName, expression, tx.classThis);
                ec.SetOriginal(expression, Ꮡnode);
                expression.Loc = node.Loc;
                if (temp != nil) {
                    expression = f.NewCommaExpression(expression, temp);
                    expression.Loc = node.Loc;
                }
                return expression;
            }
        }
    }

    if (bin.OperatorToken.Kind == ast.KindCommaToken) {
        var left = tx.discardedVisitor.VisitNode(bin.Left);
        ж<ast.Node> right = default;
        if (discarded) {
            right = tx.discardedVisitor.VisitNode(bin.Right);
        } else {
            right = tx.Transformer.Visitor().VisitNode(bin.Right);
        }
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(bin, nil, left, nil, bin.OperatorToken, right);
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitPreOrPostfixUnaryExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode, bool discarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var ec = tx.Transformer.EmitContext();

    ast.Kind @operator = default;
    ж<ast.Node> operandNode = default;
    if (ast.IsPrefixUnaryExpression(Ꮡnode)) {
        @operator = node.AsPrefixUnaryExpression().Operator;
        operandNode = node.AsPrefixUnaryExpression().Operand;
    } else {
        @operator = node.AsPostfixUnaryExpression().Operator;
        operandNode = node.AsPostfixUnaryExpression().Operand;
    }

    if (@operator == ast.KindPlusPlusToken || @operator == ast.KindMinusMinusToken) {
        var operand = ast.SkipParentheses(operandNode);
        if (ast.IsSuperProperty(operand) && tx.classThis != nil && tx.classSuper != nil) {
            ж<astꓸExpression> setterName = default;
            if (ast.IsElementAccessExpression(operand)) {
                setterName = tx.Transformer.Visitor().VisitNode(operand.AsElementAccessExpression().ArgumentExpression);
            } else if (ast.IsPropertyAccessExpression(operand) && ast.IsIdentifier(operand.AsPropertyAccessExpression().Name())) {
                setterName = f.NewStringLiteralFromNode(operand.AsPropertyAccessExpression().Name());
            }
            if (setterName != nil) {
                var getterName = setterName;
                if (!transformers.IsSimpleInlineableExpression(setterName)) {
                    getterName = f.NewTempVariable();
                    ec.AddVariableDeclaration(getterName);
                    setterName = f.NewAssignmentExpression(getterName, setterName);
                }

                var expression = f.NewReflectGetCall(tx.classSuper, getterName, tx.classThis);
                ec.SetOriginal(expression, Ꮡnode);
                expression.Loc = node.Loc;

                ж<astꓸIdentifierNode> temp = default;
                if (!discarded) {
                    temp = f.NewTempVariable();
                    ec.AddVariableDeclaration(temp);
                }

                expression = expandPreOrPostfixIncrementOrDecrementExpression(f, ec, Ꮡnode, expression, temp);

                expression = f.NewReflectSetCall(tx.classSuper, setterName, expression, tx.classThis);
                ec.SetOriginal(expression, Ꮡnode);
                expression.Loc = node.Loc;

                if (temp != nil) {
                    expression = f.NewCommaExpression(expression, temp);
                    expression.Loc = node.Loc;
                }

                return expression;
            }
        }
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static (ж<astꓸExpression>, ж<ast.Node>) visitReferencedPropertyName(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPropertyNameLiteral(Ꮡnode) || ast.IsPrivateIdentifier(Ꮡnode)) {
        return (tx.Transformer.Factory().NewStringLiteralFromNode(Ꮡnode), tx.Transformer.Visitor().VisitNode(Ꮡnode));
    }

    var cpn = node.AsComputedPropertyName();
    if (ast.IsPropertyNameLiteral((~cpn).Expression) && !ast.IsIdentifier((~cpn).Expression)) {
        return (tx.Transformer.Factory().NewStringLiteralFromNode((~cpn).Expression), tx.Transformer.Visitor().VisitNode(Ꮡnode));
    }

    var referencedName = tx.Transformer.Factory().NewGeneratedNameForNode(Ꮡnode);
    tx.Transformer.EmitContext().AddVariableDeclaration(referencedName);

    var key = tx.Transformer.Factory().NewPropKeyHelper(tx.Transformer.Visitor().VisitNode((~cpn).Expression));
    var assignment = tx.Transformer.Factory().NewAssignmentExpression(referencedName, key);
    var updatedName = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(cpn, tx.injectPendingExpressions(assignment));
    return (referencedName, updatedName);
}

internal static ж<ast.Node> visitPropertyName(this ref esDecoratorTransformer tx, ж<ast.Node> node) {
    if (ast.IsComputedPropertyName(node)) {
        return tx.visitComputedPropertyName(node);
    }
    return tx.Transformer.Visitor().VisitNode(node);
}

internal static ж<ast.Node> visitComputedPropertyName(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var cpn = node.AsComputedPropertyName();
    var expression = tx.Transformer.Visitor().VisitNode((~cpn).Expression);
    if (!transformers.IsSimpleInlineableExpression(expression)) {
        expression = tx.injectPendingExpressions(expression);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateComputedPropertyName(cpn, expression);
}

internal static ж<ast.Node> visitDestructuringAssignmentTarget(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsObjectLiteralExpression(Ꮡnode) || ast.IsArrayLiteralExpression(Ꮡnode)) {
        return tx.visitAssignmentPattern(Ꮡnode);
    }

    if (ast.IsSuperProperty(Ꮡnode) && tx.classThis != nil && tx.classSuper != nil) {
        var f = tx.Transformer.Factory();
        var ec = tx.Transformer.EmitContext();
        ж<astꓸExpression> propertyName = default;
        if (ast.IsElementAccessExpression(Ꮡnode)) {
            propertyName = tx.Transformer.Visitor().VisitNode(node.AsElementAccessExpression().ArgumentExpression);
        } else if (ast.IsPropertyAccessExpression(Ꮡnode) && ast.IsIdentifier(node.AsPropertyAccessExpression().Name())) {
            propertyName = f.NewStringLiteralFromNode(node.AsPropertyAccessExpression().Name());
        }
        if (propertyName != nil) {
            var paramName = f.NewTempVariable();
            var expression = f.NewAssignmentTargetWrapper(
                paramName,
                f.NewReflectSetCall(
                    tx.classSuper,
                    propertyName,
                    paramName,
                    tx.classThis));
            ec.SetOriginal(expression, Ꮡnode);
            expression.Loc = node.Loc;
            return expression;
        }
    }

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitAssignmentElement(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsAssignmentExpression(Ꮡnode, true)) {
        var f = tx.Transformer.Factory();
        var bin = node.AsBinaryExpression();
        if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode, isAnonymousClassNeedingAssignedName)) {
            Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode, canIgnoreEmptyStringLiteralInAssignedName(bin.Right), ""); node = ref Ꮡnode.DerefOrNull();
            bin = node.AsBinaryExpression();
        }
        var assignmentTarget = tx.visitDestructuringAssignmentTarget(bin.Left);
        var initializer = tx.Transformer.Visitor().VisitNode(bin.Right);
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateBinaryExpression(bin, nil, assignmentTarget, nil, bin.OperatorToken, initializer);
    }
    return tx.visitDestructuringAssignmentTarget(Ꮡnode);
}

internal static ж<ast.Node> visitAssignmentRestElement(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var se = node.AsSpreadElement();
    if (ast.IsLeftHandSideExpression((~se).Expression)) {
        var f = tx.Transformer.Factory();
        var expression = tx.visitDestructuringAssignmentTarget((~se).Expression);
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSpreadElement(se, expression);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitArrayAssignmentElement(this ref esDecoratorTransformer tx, ж<ast.Node> node) {
    debug.Assert(ast.IsArrayBindingOrAssignmentElement(node));
    if (ast.IsSpreadElement(node)) {
        return tx.visitAssignmentRestElement(node);
    }
    if (!ast.IsOmittedExpression(node)) {
        return tx.visitAssignmentElement(node);
    }
    return tx.Transformer.Visitor().VisitEachChild(node);
}

internal static ж<ast.Node> visitAssignmentPropertyNode(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var pa = node.AsPropertyAssignment();
    var name = tx.Transformer.Visitor().VisitNode(pa.Name());
    if (ast.IsAssignmentExpression((~pa).Initializer, true)) {
        var assignmentElement = tx.visitAssignmentElement((~pa).Initializer);
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAssignment(pa, nil, name, nil, nil, assignmentElement);
    }
    if (ast.IsLeftHandSideExpression((~pa).Initializer)) {
        var assignmentElement = tx.visitDestructuringAssignmentTarget((~pa).Initializer);
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePropertyAssignment(pa, nil, name, nil, nil, assignmentElement);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitShorthandAssignmentProperty(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (isNamedEvaluationAnd(tx.Transformer.EmitContext(), Ꮡnode, isAnonymousClassNeedingAssignedName)) {
        Ꮡnode = transformNamedEvaluation(tx.Transformer.EmitContext(), Ꮡnode, canIgnoreEmptyStringLiteralInAssignedName(node.AsShorthandPropertyAssignment().ObjectAssignmentInitializer), ""); node = ref Ꮡnode.DerefOrNull();
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitAssignmentRestProperty(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var sa = node.AsSpreadAssignment();
    if (ast.IsLeftHandSideExpression((~sa).Expression)) {
        var f = tx.Transformer.Factory();
        var expression = tx.visitDestructuringAssignmentTarget((~sa).Expression);
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSpreadAssignment(sa, expression);
    }
    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

internal static ж<ast.Node> visitObjectAssignmentElement(this ref esDecoratorTransformer tx, ж<ast.Node> node) {
    debug.Assert(ast.IsObjectBindingOrAssignmentElement(node));
    if (ast.IsSpreadAssignment(node)) {
        return tx.visitAssignmentRestProperty(node);
    }
    if (ast.IsShorthandPropertyAssignment(node)) {
        return tx.visitShorthandAssignmentProperty(node);
    }
    if (ast.IsPropertyAssignment(node)) {
        return tx.visitAssignmentPropertyNode(node);
    }
    return tx.Transformer.Visitor().VisitEachChild(node);
}

internal static ж<ast.Node> visitAssignmentPattern(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    if (ast.IsArrayLiteralExpression(Ꮡnode)) {
        var ale = node.AsArrayLiteralExpression();
        var elements = tx.arrayAssignmentVisitor.VisitNodes((~ale).Elements);
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateArrayLiteralExpression(ale, elements, ale.MultiLine);
    }
    var ole = node.AsObjectLiteralExpression();
    var properties = tx.objectAssignmentVisitor.VisitNodes((~ole).Properties);
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateObjectLiteralExpression(ole, properties, ole.MultiLine);
}

internal static ж<ast.Node> visitExportAssignment(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return tx.visitNamedEvaluationSite(Ꮡnode, node.Expression());
}

internal static ж<ast.Node> visitParenthesizedExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode, bool discarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var f = tx.Transformer.Factory();
    var pe = node.AsParenthesizedExpression();
    ж<ast.Node> expression = default;
    if (discarded) {
        expression = tx.discardedVisitor.VisitNode((~pe).Expression);
    } else {
        expression = tx.Transformer.Visitor().VisitNode((~pe).Expression);
    }
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(pe, expression);
}

internal static ж<ast.Node> visitPartiallyEmittedExpression(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡnode, bool discarded) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var pe = node.AsPartiallyEmittedExpression();
    ж<ast.Node> expression = default;
    if (discarded) {
        expression = tx.discardedVisitor.VisitNode((~pe).Expression);
    } else {
        expression = tx.Transformer.Visitor().VisitNode((~pe).Expression);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdatePartiallyEmittedExpression(pe, expression);
}

internal static ж<astꓸExpression> prependExpressions(this ref esDecoratorTransformer tx, slice<ж<astꓸExpression>> pending, ж<astꓸExpression> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    var f = tx.Transformer.Factory();
    if (len(pending) == 0) {
        return Ꮡexpression;
    }
    if (Ꮡexpression == nil) {
        return f.InlineExpressions(pending);
    }
    if (ast.IsParenthesizedExpression(Ꮡexpression)) {
        var pe = expression.AsParenthesizedExpression();
        var exprsΔ1 = new slice<ж<astꓸExpression>>(len(pending) + 1);
        copy(exprsΔ1, pending);
        exprsΔ1[len(pending)] = pe.Value.Expression;
        return f.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateParenthesizedExpression(pe, f.InlineExpressions(exprsΔ1));
    }
    var exprs = new slice<ж<astꓸExpression>>(len(pending) + 1);
    copy(exprs, pending);
    exprs[len(pending)] = Ꮡexpression;
    return f.InlineExpressions(exprs);
}

internal static ж<astꓸExpression> injectPendingExpressions(this ref esDecoratorTransformer tx, ж<astꓸExpression> expression) {
    var result = tx.prependExpressions(tx.pendingExpressions, expression);
    debug.Assert(result != nil);
    if (result != expression) {
        tx.pendingExpressions = default;
    }
    return result;
}

internal static ж<astꓸExpression> injectPendingInitializers(this ref esDecoratorTransformer tx, ж<classInfo> ci, bool isStatic, ж<astꓸExpression> expression) {
    ж<slice<ж<astꓸExpression>>> pending = default;
    if (isStatic) {
        pending = ci.ᏑpendingStaticInitializers;
    } else {
        pending = ci.ᏑpendingInstanceInitializers;
    }
    var result = tx.prependExpressions(pending.ValueSlot, expression);
    if (result != expression) {
        pending.ValueSlot = default;
    }
    return result;
}

internal static slice<ж<astꓸExpression>> transformAllDecoratorsOfDeclaration(this ref esDecoratorTransformer tx, slice<ж<ast.Node>> decorators) {
    if (len(decorators) == 0) {
        return default;
    }
    var result = new slice<ж<astꓸExpression>>(0, len(decorators));
    foreach (var (_, d) in range(decorators)) {
        result = append(result, tx.transformDecorator(d));
    }
    return result;
}

internal static ж<astꓸExpression> transformDecorator(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡdecorator) {
    ref var decorator = ref Ꮡdecorator.DerefOrNull();

    var expression = tx.Transformer.Visitor().VisitNode((~decorator.AsDecorator()).Expression);
    tx.Transformer.EmitContext().SetEmitFlags(expression, printer.EFNoComments);

    var innerExpression = ast.SkipOuterExpressions(expression, ast.OEKAll);
    if (ast.IsAccessExpression(innerExpression)) {
        var (target, thisArg) = tx.createCallBinding(expression);
        var bindCall = tx.Transformer.Factory().NewFunctionBindCall(target, thisArg, default);
        return tx.Transformer.Factory().RestoreOuterExpressions(expression, bindCall, ast.OEKAll);
    }
    return expression;
}

internal static (ж<astꓸExpression>, ж<astꓸExpression>) createCallBinding(this ref esDecoratorTransformer tx, ж<astꓸExpression> expression) {
    var f = tx.Transformer.Factory();
    var callee = ast.SkipOuterExpressions(expression, ast.OEKAll);
    if (ast.IsSuperProperty(callee)) {
        return (callee, f.NewThisExpression());
    }
    if (callee.Kind == ast.KindSuperKeyword) {
        return (callee, f.NewThisExpression());
    }
    if ((printer.EmitFlags)(tx.Transformer.EmitContext().EmitFlags(callee) & printer.EFHelperName) != 0) {
        return (callee, f.NewVoidZeroExpression());
    }
    if (ast.IsPropertyAccessExpression(callee)) {
        var pa = callee.AsPropertyAccessExpression();
        if (tx.shouldBeCapturedInTempVariable((~pa).Expression)) {
            var thisArg = f.NewTempVariable();
            tx.Transformer.EmitContext().AddVariableDeclaration(thisArg);
            var assign = f.NewAssignmentExpression(thisArg, (~pa).Expression);
            assign.Loc = pa.Value.Expression.Loc;
            var target = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(assign, nil, pa.Name(), ast.NodeFlagsNone);
            target.Loc = callee.Loc;
            return (target, thisArg);
        }
        return (callee, (~pa).Expression);
    }
    if (ast.IsElementAccessExpression(callee)) {
        var ea = callee.AsElementAccessExpression();
        if (tx.shouldBeCapturedInTempVariable((~ea).Expression)) {
            var thisArg = f.NewTempVariable();
            tx.Transformer.EmitContext().AddVariableDeclaration(thisArg);
            var assign = f.NewAssignmentExpression(thisArg, (~ea).Expression);
            assign.Loc = ea.Value.Expression.Loc;
            var target = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(assign, nil, ea.ArgumentExpression, ast.NodeFlagsNone);
            target.Loc = callee.Loc;
            return (target, thisArg);
        }
        return (callee, (~ea).Expression);
    }
    return (expression, f.NewVoidZeroExpression());
}

internal static bool shouldBeCapturedInTempVariable(this ref esDecoratorTransformer tx, ж<astꓸExpression> node) {
    var target = ast.SkipParentheses(node);
    switch (target.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        return true;
    }
    case var ᴛ when ᴛ == ast.KindThisKeyword || ᴛ == ast.KindNumericLiteral || ᴛ == ast.KindBigIntLiteral || ᴛ == ast.KindStringLiteral: {
        return false;
    }
    default: {
        return true;
    }}

}

internal static ж<ast.Node> createDescriptorMethod(this ref esDecoratorTransformer tx, ж<ast.Node> original, ж<ast.Node> name, ж<ast.ModifierList> modifiers, ж<astꓸTokenNode> asteriskToken, @string kind, ж<ast.NodeList> parameters, ж<ast.Node> Ꮡbody) {
    ref var body = ref Ꮡbody.DerefOrNull();

    var f = tx.Transformer.Factory();
    var ec = tx.Transformer.EmitContext();

    if (Ꮡbody == nil) {
        Ꮡbody = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), false); body = ref Ꮡbody.DerefOrNull();
    }

    var funcExpr = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewFunctionExpression(
        modifiers,
        asteriskToken,
        nil,
        nil,
        parameters,
        nil,
        nil,
        Ꮡbody);
    ec.SetOriginal(funcExpr, original);
    ec.SetSourceMapRange(funcExpr, transformers.MoveRangePastDecorators(original));
    ec.SetEmitFlags(funcExpr, printer.EFNoComments);

    @string prefix = default;
    if (kind == "get"u8 || kind == "set"u8) {
        prefix = kind;
    }
    var functionName = f.NewStringLiteralFromNode(name);
    var namedFunction = f.NewSetFunctionNameHelper(funcExpr, functionName, prefix);

    var method = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(kind), nil, nil, namedFunction);
    ec.SetOriginal(method, original);
    ec.SetSourceMapRange(method, transformers.MoveRangePastDecorators(original));
    ec.SetEmitFlags(method, printer.EFNoComments);
    return method;
}

internal static ж<astꓸExpression> createMethodDescriptorObject(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡmember, ж<ast.ModifierList> modifiers) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var f = tx.Transformer.Factory();
    var parameters = tx.Transformer.Visitor().VisitNodes(member.ParameterList());
    var body = tx.Transformer.Visitor().VisitNode(member.Body());
    var method = member.AsMethodDeclaration();
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            tx.createDescriptorMethod(Ꮡmember, member.Name(), modifiers, method.AsteriskToken, "value", parameters, body)
        ])),
        false);
}

internal static ж<astꓸExpression> createGetAccessorDescriptorObject(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡmember, ж<ast.ModifierList> modifiers) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var f = tx.Transformer.Factory();
    var body = tx.Transformer.Visitor().VisitNode(member.Body());
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            tx.createDescriptorMethod(Ꮡmember, member.Name(), modifiers, nil, "get", f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), body)
        ])),
        false);
}

internal static ж<astꓸExpression> createSetAccessorDescriptorObject(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡmember, ж<ast.ModifierList> modifiers) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var f = tx.Transformer.Factory();
    var parameters = tx.Transformer.Visitor().VisitNodes(member.ParameterList());
    var body = tx.Transformer.Visitor().VisitNode(member.Body());
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            tx.createDescriptorMethod(Ꮡmember, member.Name(), modifiers, nil, "set", parameters, body)
        ])),
        false);
}

internal static ж<astꓸExpression> createAccessorPropertyDescriptorObject(this ref esDecoratorTransformer tx, ж<ast.Node> Ꮡmember, ж<ast.ModifierList> _) {
    ref var member = ref Ꮡmember.DerefOrNull();

    var f = tx.Transformer.Factory();
    var backingFieldName = f.NewGeneratedPrivateNameForNodeEx(member.Name(), new printer.AutoGenerateOptions(Suffix: "_accessor_storage"));
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            tx.createDescriptorMethod(
                Ꮡmember, member.Name(), nil, nil, "get",
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(
                        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(f.NewThisExpression(), nil, backingFieldName, ast.NodeFlagsNone))
                ])), false)),
            tx.createDescriptorMethod(
                Ꮡmember, member.Name(), nil, nil, "set",
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    f.of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(nil, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), nil, nil, nil)
                ])),
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                    f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(
                        f.NewAssignmentExpression(
                            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(f.NewThisExpression(), nil, backingFieldName, ast.NodeFlagsNone),
                            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value")))
                ])), false))
        ])),
        false);
}

internal static ж<ast.Node> createMethodDescriptorForwarder(this ref esDecoratorTransformer tx, ж<ast.ModifierList> modifiers, ж<ast.Node> name, ж<astꓸIdentifierNode> descriptorName) {
    var f = tx.Transformer.Factory();
    var staticOnly = tx.staticOnlyModifierVisitor.VisitModifiers(modifiers);
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewGetAccessorDeclaration(
        staticOnly,
        name,
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
        nil,
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(
                f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(descriptorName, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), ast.NodeFlagsNone))
        ])), false));
}

internal static ж<ast.Node> createGetAccessorDescriptorForwarder(this ref esDecoratorTransformer tx, ж<ast.ModifierList> modifiers, ж<ast.Node> name, ж<astꓸIdentifierNode> descriptorName) {
    var f = tx.Transformer.Factory();
    var staticOnly = tx.staticOnlyModifierVisitor.VisitModifiers(modifiers);
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewGetAccessorDeclaration(
        staticOnly,
        name,
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]),
        nil,
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(
                f.NewFunctionCallCall(
                    f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(descriptorName, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("get"), ast.NodeFlagsNone),
                    f.NewThisExpression(),
                    default))
        ])), false));
}

internal static ж<ast.Node> createSetAccessorDescriptorForwarder(this ref esDecoratorTransformer tx, ж<ast.ModifierList> modifiers, ж<ast.Node> name, ж<astꓸIdentifierNode> descriptorName) {
    var f = tx.Transformer.Factory();
    var staticOnly = tx.staticOnlyModifierVisitor.VisitModifiers(modifiers);
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewSetAccessorDeclaration(
        staticOnly,
        name,
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewParameterDeclaration(nil, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), nil, nil, nil)
        ])),
        nil,
        nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
            f.of(printer.NodeFactory.ᏑΔNodeFactory).NewReturnStatement(
                f.NewFunctionCallCall(
                    f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(descriptorName, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("set"), ast.NodeFlagsNone),
                    f.NewThisExpression(),
                    slice<ж<ast.Node>>([f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value")])))
        ])), false));
}

internal static ж<astꓸStatement> createMetadata(this ref esDecoratorTransformer tx, ж<astꓸIdentifierNode> name, ж<astꓸIdentifierNode> classSuper) {
    var f = tx.Transformer.Factory();

    ж<astꓸExpression> superMetadata = default;
    if (classSuper != nil) {
        superMetadata = tx.createSymbolMetadataReference(classSuper);
    } else {
        superMetadata = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword);
    }

    var objectCreate = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object"), nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("create"), ast.NodeFlagsNone),
        nil, nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([superMetadata]),
        ast.NodeFlagsNone);

    var symbolCheck = f.NewLogicalANDExpression(
        f.NewTypeCheck(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Symbol"), "function"),
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Symbol"), nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("metadata"), ast.NodeFlagsNone));

    var conditional = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewConditionalExpression(
        symbolCheck,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionToken),
        objectCreate,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindColonToken),
        f.NewVoidZeroExpression());

    var varDecl = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(name, nil, nil, conditional);
    var varDeclList = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([varDecl]), ast.NodeFlagsConst);
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, varDeclList);
}

internal static ж<astꓸStatement> createSymbolMetadata(this ref esDecoratorTransformer tx, ж<astꓸExpression> target, ж<astꓸIdentifierNode> value) {
    var f = tx.Transformer.Factory();

    var symbolMetadata = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Symbol"), nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("metadata"), ast.NodeFlagsNone);

    var descriptorProps = slice<ж<ast.Node>>([
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("enumerable"), nil, nil, f.NewTrueExpression()),
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("configurable"), nil, nil, f.NewTrueExpression()),
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("writable"), nil, nil, f.NewTrueExpression()),
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("value"), nil, nil, value)
    ]);
    var descriptor = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(descriptorProps), false);

    var defineProperty = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Object"), nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("defineProperty"), ast.NodeFlagsNone),
        nil, nil,
        f.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([target, symbolMetadata, descriptor]),
        ast.NodeFlagsNone);

    var ifStatement = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIfStatement(value, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(defineProperty), nil);
    tx.Transformer.EmitContext().SetEmitFlags(ifStatement, printer.EFSingleLine);
    return ifStatement;
}

internal static ж<astꓸExpression> createSymbolMetadataReference(this ref esDecoratorTransformer tx, ж<astꓸIdentifierNode> classSuper) {
    var f = tx.Transformer.Factory();
    var symbolMetadata = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Symbol"), nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("metadata"), ast.NodeFlagsNone);
    var elementAccess = f.of(printer.NodeFactory.ᏑΔNodeFactory).NewElementAccessExpression(classSuper, nil, symbolMetadata, ast.NodeFlagsNone);
    return f.of(printer.NodeFactory.ᏑΔNodeFactory).NewBinaryExpression(nil, elementAccess, nil, f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindQuestionQuestionToken), f.of(printer.NodeFactory.ᏑΔNodeFactory).NewToken(ast.KindNullKeyword));
}

} partial class estransforms_package {

internal static ж<ast.Node> injectClassThisAssignmentIfMissing(ж<printer.EmitContext> Ꮡec, ж<printer.NodeFactory> Ꮡf, ж<ast.Node> Ꮡnode, ж<astꓸIdentifierNode> classThis) {
    ref var ec = ref Ꮡec.DerefOrNull();
    ref var f = ref Ꮡf.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (classHasClassThisAssignment(Ꮡec, Ꮡnode)) {
        return Ꮡnode;
    }

    var expression = f.NewAssignmentExpression(classThis, f.NewThisExpression());
    var statement = Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).NewExpressionStatement(expression);
    var body = Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).NewBlock(Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([statement]), false);
    var staticBlock = Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).NewClassStaticBlockDeclaration(nil, body);
    ec.SetClassThis(staticBlock, classThis);

    if (node.Name() != nil) {
        ec.SetSourceMapRange(statement, node.Name().Loc);
    }

    var newMembers = new slice<ж<ast.Node>>(0, 1 + len(node.Members()));
    newMembers = append(newMembers, staticBlock);
    newMembers = appendꓸꓸꓸ(newMembers, node.Members());
    var membersList = Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(newMembers);
    membersList.Loc = node.MemberList().Loc;

    ж<ast.Node> updatedNode = default;
    if (ast.IsClassDeclaration(Ꮡnode)) {
        var cd = node.AsClassDeclaration();
        updatedNode = Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassDeclaration(cd, cd.of(ast.ClassDeclaration.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), cd.Name(), nil, cd.HeritageClauses, membersList);
    } else {
        var ce = node.AsClassExpression();
        updatedNode = Ꮡf.of(printer.NodeFactory.ᏑΔNodeFactory).UpdateClassExpression(ce, ce.of(ast.ClassExpression.ᏑClassLikeBase).of(ast.ClassLikeBase.ᏑModifiersBase).Modifiers(), ce.Name(), nil, ce.HeritageClauses, membersList);
    }
    ec.SetClassThis(updatedNode, classThis);
    return updatedNode;
}

} // end estransforms_package
