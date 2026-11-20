namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using maps = maps_package;
using slices = slices_package;
using strconv = strconv_package;
using strings = strings_package;
using utf8 = unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using unicode;
using static jsxtransforms_package;

partial class jsxtransforms_package {

partial struct JSXTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal printer.EmitResolver emitResolver;
    internal @string importSpecifier;
    internal ж<ast.Node> filenameDeclaration;
    internal collections.OrderedMap<@string, map<@string, ж<ast.Node>>> utilizedImplicitRuntimeImports;
    internal bool inJsxChild;
    internal ж<ast.SourceFile> currentSourceFile;
}

public static ж<transformers.Transformer> NewJSXTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var compilerOptions = opts.CompilerOptions;
    var emitContext = opts.Context;
    var tx = Ꮡ(new JSXTransformer(
        compilerOptions: compilerOptions,
        emitResolver: opts.EmitResolver
    ));
    return tx.of(JSXTransformer.ᏑTransformer).NewTransformer(tx.visit, emitContext);
}

} partial class jsxtransformsᴛJSXTransformer {

internal static ж<ast.Node> getCurrentFileNameExpression(this ref JSXTransformer tx) {
    if (tx.filenameDeclaration != nil) {
        return tx.filenameDeclaration.AsVariableDeclaration().Name();
    }
    var d = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
        tx.Transformer.Factory().NewUniqueNameEx("_jsxFileName", new printer.AutoGenerateOptions(
            Flags: (printer.GeneratedIdentifierFlags)((nint)printer.GeneratedIdentifierFlagsOptimistic | (nint)printer.GeneratedIdentifierFlagsFileLevel)
        )),
        nil,
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(tx.currentSourceFile.FileName(), ast.TokenFlagsNone));
    tx.filenameDeclaration = d;
    return d.AsVariableDeclaration().Name();
}

internal static @string getJsxFactoryCalleePrimitive(this ref JSXTransformer tx, bool isStaticChildren) {
    if (tx.compilerOptions.Jsx == core.JsxEmitReactJSXDev) {
        return "jsxDEV";
    }
    if (isStaticChildren) {
        return "jsxs";
    }
    return "jsx";
}

internal static ж<ast.Node> getJsxFactoryCallee(this ref JSXTransformer tx, bool isStaticChildren) {
    @string t = tx.getJsxFactoryCalleePrimitive(isStaticChildren);
    return tx.getImplicitImportForName(t);
}

internal static ж<ast.Node> getImplicitJsxFragmentReference(this ref JSXTransformer tx) {
    return tx.getImplicitImportForName("Fragment");
}

internal static ж<ast.Node> getImplicitImportForName(this ref JSXTransformer tx, @string name) {
    @string importSource = tx.importSpecifier;
    if (name != "createElement"u8) {
        importSource = ast.GetJSXRuntimeImport(importSource, tx.compilerOptions);
    }
    var (existing, ok) = tx.utilizedImplicitRuntimeImports.Get(importSource);
    if (ok) {
        var (elem, okΔ1) = existing[name, ꟷ];
        if (okΔ1) {
            return elem.AsImportSpecifier().Name();
        }
    } else {
        existing = new map<@string, ж<ast.Node>>();
        tx.utilizedImplicitRuntimeImports.Set(importSource, existing);
    }

    var generatedName = tx.Transformer.Factory().NewUniqueNameEx("_"u8 + name, new printer.AutoGenerateOptions(
        Flags: (printer.GeneratedIdentifierFlags)((UntypedInt)(printer.GeneratedIdentifierFlagsOptimistic | printer.GeneratedIdentifierFlagsFileLevel) | (nint)printer.GeneratedIdentifierFlagsAllowNameSubstitution)
    ));
    var specifier = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportSpecifier(false, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(name), generatedName);
    tx.emitResolver.SetReferencedImportDeclaration(generatedName, specifier);
    existing[name] = specifier;
    return specifier.Name();
}

internal static void setInChild(this ref JSXTransformer tx, bool v) {
    tx.inJsxChild = v;
}

internal static ж<ast.Node> visit(this ж<JSXTransformer> Ꮡtx, ж<ast.Node> Ꮡnode) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return default;
    }
    if ((ast.SubtreeFacts)(node.SubtreeFacts() & ast.SubtreeContainsJsx) == 0) {
        return Ꮡnode;
    }
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindSourceFile: {
        tx.setInChild(false);
        return Ꮡtx.visitSourceFile(node.AsSourceFile());
    }
    case var ᴛ when ᴛ == ast.KindJsxElement: {
        return Ꮡtx.visitJsxElement(node.AsJsxElement());
    }
    case var ᴛ when ᴛ == ast.KindJsxSelfClosingElement: {
        return Ꮡtx.visitJsxSelfClosingElement(node.AsJsxSelfClosingElement());
    }
    case var ᴛ when ᴛ == ast.KindJsxFragment: {
        return Ꮡtx.visitJsxFragment(node.AsJsxFragment());
    }
    case var ᴛ when ᴛ == ast.KindJsxOpeningElement: {
        throw panic("JsxOpeningElement should not be visited, handled in visitJsxElement");
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxOpeningFragment: {
        throw panic("JsxOpeningFragment should not be visited, handled in visitJsxFragment");
        break;
    }
    case var ᴛ when ᴛ == ast.KindJsxText: {
        tx.setInChild(false);
        return tx.visitJsxText(node.AsJsxText());
    }
    case var ᴛ when ᴛ == ast.KindJsxExpression: {
        tx.setInChild(false);
        return tx.visitJsxExpression(node.AsJsxExpression());
    }}

    tx.setInChild(false);
    return Ꮡtx.of(JSXTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡnode);
}

} partial class jsxtransforms_package {

internal static bool hasKeyAfterPropsSpread(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var spread = false;
    var opener = Ꮡnode;
    if (node.Kind == ast.KindJsxElement) {
        opener = node.AsJsxElement().OpeningElement;
    }
    foreach (var (_, elem) in range(opener.Attributes().Properties())) {
        if (ast.IsJsxSpreadAttribute(elem) && (!ast.IsObjectLiteralExpression(elem.Expression()) || core.Some<ж<ast.Node>>(elem.Expression().Properties(), ast.IsSpreadAssignment))) {
            spread = true;
        } else if (spread && ast.IsJsxAttribute(elem) && ast.IsIdentifier(elem.Name()) && elem.Name().Text() == "key"u8) {
            return true;
        }
    }
    return false;
}

} partial class jsxtransformsᴛJSXTransformer {

internal static bool shouldUseCreateElement(this ref JSXTransformer tx, ж<ast.Node> node) {
    return len(tx.importSpecifier) == 0 || hasKeyAfterPropsSpread(node);
}

} partial class jsxtransforms_package {

internal static slice<ж<ast.Node>> insertStatementAfterPrologue<T>(slice<ж<ast.Node>> to, ж<ast.Node> statement, Func<T, ж<ast.Node>, bool> isPrologueDirective, T callee) {
    if (statement == nil) {
        return to;
    }
    nint statementIdx = 0;
    for (; statementIdx < len(to); statementIdx++) {
        if (!isPrologueDirective(callee, to[statementIdx])) {
            break;
        }
    }
    return slices.Insert(to, statementIdx, statement);
}

} partial class jsxtransformsᴛJSXTransformer {

internal static bool isAnyPrologueDirective(this ref JSXTransformer tx, ж<ast.Node> node) {
    return ast.IsPrologueDirective(node) || ((printer.EmitFlags)(tx.Transformer.EmitContext().EmitFlags(node) & printer.EFCustomPrologue) != 0);
}

internal static slice<ж<ast.Node>> insertStatementAfterCustomPrologue(this ж<JSXTransformer> Ꮡtx, slice<ж<ast.Node>> to, ж<ast.Node> statement) {
    return insertStatementAfterPrologue<ж<JSXTransformer>>(to, statement, ((Func<ж<JSXTransformer>, ж<ast.Node>, bool>)(jsxtransformsᴛJSXTransformer.isAnyPrologueDirective)), Ꮡtx);
}

} partial class jsxtransforms_package {

internal static nint sortImportSpecifiers(ж<ast.Node> Ꮡa, ж<ast.Node> Ꮡb) {
    ref var a = ref Ꮡa.DerefOrNull();
    ref var b = ref Ꮡb.DerefOrNull();

    stringutilꓸComparison res = stringutil.CompareStringsCaseSensitive(a.PropertyName().Text(), b.PropertyName().Text());
    if (res != 0) {
        return res;
    }
    return stringutil.CompareStringsCaseSensitive(a.AsImportSpecifier().Name().Text(), b.AsImportSpecifier().Name().Text());
}

internal static slice<ж<ast.Node>> getSortedSpecifiers(map<@string, ж<ast.Node>> m) {
    var res = slices.Collect(maps.Values<map<@string, ж<ast.Node>>, @string, ж<ast.Node>>(m));
    slices.SortFunc<slice<ж<ast.Node>>, ж<ast.Node>>(res, sortImportSpecifiers);
    return res;
}

} partial class jsxtransformsᴛJSXTransformer {

internal static ж<ast.Node> visitSourceFile(this ж<JSXTransformer> Ꮡtx, ж<ast.SourceFile> Ꮡfile) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (@file.IsDeclarationFile) {
        return Ꮡfile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }

    tx.currentSourceFile = Ꮡfile;
    tx.importSpecifier = ast.GetJSXImplicitImportBase(tx.compilerOptions, Ꮡfile);
    tx.filenameDeclaration = default;
    tx.utilizedImplicitRuntimeImports.Clear();

    var visited = Ꮡtx.of(JSXTransformer.ᏑTransformer).Visitor().VisitEachChild(Ꮡfile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().AddEmitHelper(visited.AsNode(), Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().ReadEmitHelpers().ꓸꓸꓸ);
    var statements = visited.Statements();
    var statementsUpdated = false;
    if (tx.filenameDeclaration != nil) {
        statements = Ꮡtx.insertStatementAfterCustomPrologue(statements, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(
            Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([tx.filenameDeclaration]),
            ast.NodeFlagsConst)));
        statementsUpdated = true;
    }

    if (Ꮡtx.ᏑutilizedImplicitRuntimeImports.Size() > 0) {
        if (ast.IsExternalModule(Ꮡfile)) {
            statementsUpdated = true;
            var newStatements = new slice<ж<ast.Node>>(0, Ꮡtx.ᏑutilizedImplicitRuntimeImports.Size());
            foreach (var (importSource, importSpecifiersMap) in range<@string, map<@string, ж<ast.Node>>>(Ꮡtx.ᏑutilizedImplicitRuntimeImports.Entries().Invoke)) {
                var s = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportDeclaration(
                    nil,
                    Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewImportClause(ast.KindUnknown, nil, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedImports(Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(getSortedSpecifiers(importSpecifiersMap)))),
                    Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(importSource, ast.TokenFlagsNone),
                    nil);
                ast.SetParentInChildren(s);
                newStatements = append(newStatements, s);
            }
            foreach (var (_, e) in range(newStatements)) {
                statements = Ꮡtx.insertStatementAfterCustomPrologue(statements, e);
            }
        } else if (ast.IsExternalOrCommonJSModule(Ꮡfile)) {
            statementsUpdated = true;
            var newStatements = new slice<ж<ast.Node>>(0, Ꮡtx.ᏑutilizedImplicitRuntimeImports.Size());
            foreach (var (importSource, importSpecifiersMap) in range<@string, map<@string, ж<ast.Node>>>(Ꮡtx.ᏑutilizedImplicitRuntimeImports.Entries().Invoke)) {
                var sorted = getSortedSpecifiers(importSpecifiersMap);
                var asBindingElems = new slice<ж<ast.Node>>(0, len(sorted));
                foreach (var (_, elem) in range(sorted)) {
                    asBindingElems = append(asBindingElems, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBindingElement(nil, elem.PropertyName(), elem.AsImportSpecifier().Name(), nil));
                }
                var s = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableStatement(nil, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclarationList(Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewVariableDeclaration(
                    Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBindingPattern(ast.KindObjectBindingPattern, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(asBindingElems)),
                    nil,
                    nil,
                    Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
                        Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("require"),
                        nil,
                        nil,
                        Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(importSource, ast.TokenFlagsNone)])), ast.NodeFlagsNone))
                ])), ast.NodeFlagsConst));
                ast.SetParentInChildren(s);
                newStatements = append(newStatements, s);
            }
            foreach (var (_, e) in range(newStatements)) {
                statements = Ꮡtx.insertStatementAfterCustomPrologue(statements, e);
            }
        } else {
        }
    }

    if (statementsUpdated) {
        visited = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).UpdateSourceFile(Ꮡfile, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(statements), @file.EndOfFileToken);
    }

    tx.currentSourceFile = default;
    tx.importSpecifier = "";
    tx.filenameDeclaration = default;
    tx.utilizedImplicitRuntimeImports.Clear();

    return visited;
}

internal static ж<ast.Node> visitJsxElement(this ж<JSXTransformer> Ꮡtx, ж<ast.JsxElement> Ꮡelement) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();

    var tagTransform = ((Func<ж<JSXTransformer>, ж<ast.Node>, ж<ast.NodeList>, core.TextRange, ж<ast.Node>>)(jsxtransformsᴛJSXTransformer.visitJsxOpeningLikeElementJSX));
    if (tx.shouldUseCreateElement(Ꮡelement.of(ast.JsxElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        tagTransform = ((Func<ж<JSXTransformer>, ж<ast.Node>, ж<ast.NodeList>, core.TextRange, ж<ast.Node>>)(jsxtransformsᴛJSXTransformer.visitJsxOpeningLikeElementCreateElement));
    }
    var location = core.NewTextRange(scanner.SkipTrivia(tx.currentSourceFile.Text(), Ꮡelement.of(ast.JsxElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos()), Ꮡelement.of(ast.JsxElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).End());
    return tagTransform(Ꮡtx, element.OpeningElement, element.Children, location);
}

internal static ж<ast.Node> visitJsxSelfClosingElement(this ж<JSXTransformer> Ꮡtx, ж<ast.JsxSelfClosingElement> element) {
    ref var tx = ref Ꮡtx.DerefOrNull();

    var tagTransform = ((Func<ж<JSXTransformer>, ж<ast.Node>, ж<ast.NodeList>, core.TextRange, ж<ast.Node>>)(jsxtransformsᴛJSXTransformer.visitJsxOpeningLikeElementJSX));
    if (tx.shouldUseCreateElement(element.of(ast.JsxSelfClosingElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
        tagTransform = ((Func<ж<JSXTransformer>, ж<ast.Node>, ж<ast.NodeList>, core.TextRange, ж<ast.Node>>)(jsxtransformsᴛJSXTransformer.visitJsxOpeningLikeElementCreateElement));
    }
    var location = core.NewTextRange(scanner.SkipTrivia(tx.currentSourceFile.Text(), element.of(ast.JsxSelfClosingElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos()), element.of(ast.JsxSelfClosingElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).End());
    return tagTransform(Ꮡtx, element.of(ast.JsxSelfClosingElement.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), nil, location);
}

internal static ж<ast.Node> visitJsxFragment(this ж<JSXTransformer> Ꮡtx, ж<ast.JsxFragment> Ꮡfragment) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var fragment = ref Ꮡfragment.DerefOrNull();

    var tagTransform = ((Func<ж<JSXTransformer>, ж<ast.JsxOpeningFragment>, ж<ast.NodeList>, core.TextRange, ж<ast.Node>>)(jsxtransformsᴛJSXTransformer.visitJsxOpeningFragmentJSX));
    if (len(tx.importSpecifier) == 0) {
        tagTransform = ((Func<ж<JSXTransformer>, ж<ast.JsxOpeningFragment>, ж<ast.NodeList>, core.TextRange, ж<ast.Node>>)(jsxtransformsᴛJSXTransformer.visitJsxOpeningFragmentCreateElement));
    }
    var location = core.NewTextRange(scanner.SkipTrivia(tx.currentSourceFile.Text(), Ꮡfragment.of(ast.JsxFragment.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).Pos()), Ꮡfragment.of(ast.JsxFragment.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).End());
    return tagTransform(Ꮡtx, fragment.OpeningFragment.AsJsxOpeningFragment(), fragment.Children, location);
}

internal static ж<ast.Node> convertJsxChildrenToChildrenPropObject(this ж<JSXTransformer> Ꮡtx, slice<ж<astꓸJsxChild>> children) {
    var prop = Ꮡtx.convertJsxChildrenToChildrenPropAssignment(children);
    if (prop == nil) {
        return default;
    }
    return Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([prop]), false);
}

internal static ж<ast.Node> transformJsxChildToExpression(this ж<JSXTransformer> Ꮡtx, ж<ast.Node> node) {
    GoFrame ᒐ = default;
    try {
        ref var tx = ref Ꮡtx.DerefOrNull();

        var prev = tx.inJsxChild;
        tx.setInChild(true);
        defer(Ꮡtx.setInChild, prev, ref ᒐ);
        return Ꮡtx.of(JSXTransformer.ᏑTransformer).Visitor().Visit(node);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static ж<ast.Node> convertJsxChildrenToChildrenPropAssignment(this ж<JSXTransformer> Ꮡtx, slice<ж<astꓸJsxChild>> children) {
    var nonWhitespceChildren = ast.GetSemanticJsxChildren(children);
    if (len(nonWhitespceChildren) == 1 && (nonWhitespceChildren[0].Kind != ast.KindJsxExpression || nonWhitespceChildren[0].AsJsxExpression().DotDotDotToken == nil)) {
        var result = Ꮡtx.transformJsxChildToExpression(nonWhitespceChildren[0]);
        if (result == nil) {
            return default;
        }
        return Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("children"), nil, nil, result);
    }
    var results = new slice<ж<ast.Node>>(0, len(nonWhitespceChildren));
    foreach (var (_, child) in range(nonWhitespceChildren)) {
        var res = Ꮡtx.transformJsxChildToExpression(child);
        if (res == nil) {
            continue;
        }
        Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().SetEmitFlags(res, (printer.EmitFlags)(Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().EmitFlags(res) & ~((printer.EmitFlags)printer.EFStartOnNewLine)));
        results = append(results, res);
    }
    if (len(results) == 0) {
        return default;
    }
    return Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("children"), nil, nil, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewArrayLiteralExpression(Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(results), false));
}

internal static ж<ast.Node> getTagName(this ref JSXTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindJsxElement) {
        return tx.getTagName(node.AsJsxElement().OpeningElement);
    } else if (ast.IsJsxOpeningLikeElement(Ꮡnode)) {
        var tagName = node.TagName();
        if (ast.IsIdentifier(tagName) && scanner.IsIntrinsicJsxName(tagName.Text())) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(tagName.Text(), ast.TokenFlagsNone);
        } else if (ast.IsJsxNamespacedName(tagName)) {
            return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(
                tagName.AsJsxNamespacedName().Namespace.Text() + ":"u8 + tagName.AsJsxNamespacedName().Name().Text(), ast.TokenFlagsNone);
        } else {
            return tx.Transformer.Factory().CreateExpressionFromEntityName(tagName);
        }
    } else {
        throw panic("unhandled node kind passed to getTagName: " + node.Kind.String());
    }
}

internal static ж<ast.Node> visitJsxOpeningLikeElementJSX(this ж<JSXTransformer> Ꮡtx, ж<ast.Node> Ꮡelement, ж<ast.NodeList> Ꮡchildren, core.TextRange location) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();
    ref var children = ref Ꮡchildren.DerefOrNull();

    var tagName = tx.getTagName(Ꮡelement);
    ж<ast.Node> childrenProp = default;
    if (Ꮡchildren != nil && len(children.Nodes) > 0) {
        childrenProp = Ꮡtx.convertJsxChildrenToChildrenPropAssignment(children.Nodes);
    }
    ж<ast.Node> keyAttr = default;
    var attrs = element.Attributes().Properties();
    foreach (var (i, p) in range(attrs)) {
        if (p.Kind == ast.KindJsxAttribute && p.AsJsxAttribute().Name() != nil && ast.IsIdentifier(p.AsJsxAttribute().Name()) && p.AsJsxAttribute().Name().Text() == "key"u8) {
            keyAttr = p;
            attrs = slices.Clone<slice<ж<ast.Node>>, ж<ast.Node>>(attrs);
            attrs = slices.Delete<slice<ж<ast.Node>>, ж<ast.Node>>(attrs, i, i + 1);
            break;
        }
    }
    ж<ast.Node> @object = default;
    if (len(attrs) > 0) {
        @object = tx.transformJsxAttributesToObjectProps(attrs, childrenProp);
    } else {
        var objectChildren = slice<ж<ast.Node>>([]);
        if (childrenProp != nil) {
            objectChildren = append(objectChildren, childrenProp);
        }
        @object = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(objectChildren), false);
    }
    return tx.visitJsxOpeningLikeElementOrFragmentJSX(
        tagName,
        @object,
        keyAttr,
        Ꮡchildren,
        location);
}

internal static ж<ast.Node> transformJsxAttributesToObjectProps(this ref JSXTransformer tx, slice<ж<ast.Node>> attrs, ж<ast.Node> childrenProp) {
    var target = tx.compilerOptions.GetEmitScriptTarget();
    if (target >= core.ScriptTargetES2018) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(tx.transformJsxAttributesToProps(attrs, childrenProp)), false);
    }
    return tx.transformJsxAttributesToExpression(attrs, childrenProp);
}

internal static ж<ast.Node> transformJsxAttributesToExpression(this ref JSXTransformer tx, slice<ж<ast.Node>> attrs, ж<ast.Node> childrenProp) {
    var expressions = new slice<ж<astꓸExpression>>(0, 2);
    var properties = new slice<ж<astꓸObjectLiteralElement>>(0, len(attrs));

    foreach (var (_, attr) in range(attrs)) {
        if (ast.IsJsxSpreadAttribute(attr)) {
            if (ast.IsObjectLiteralExpression(attr.Expression()) && !hasProto(ref (attr.Expression().AsObjectLiteralExpression()).DerefOrNull())) {
                foreach (var (_, prop) in range(attr.Expression().Properties())) {
                    if (ast.IsSpreadAssignment(prop)) {
                        (expressions, properties) = tx.combinePropertiesIntoNewExpression(expressions, properties);
                        expressions = append(expressions, tx.Transformer.Visitor().Visit(prop.Expression()));
                        continue;
                    }
                    properties = append(properties, tx.Transformer.Visitor().Visit(prop));
                }
                continue;
            }
            (expressions, properties) = tx.combinePropertiesIntoNewExpression(expressions, properties);
            expressions = append(expressions, tx.Transformer.Visitor().Visit(attr.Expression()));
            continue;
        }
        properties = append(properties, tx.transformJsxAttributeToObjectLiteralElement(attr.AsJsxAttribute()));
    }

    if (childrenProp != nil) {
        properties = append(properties, childrenProp);
    }

    (expressions, _) = tx.combinePropertiesIntoNewExpression(expressions, properties);

    if (len(expressions) > 0 && !ast.IsObjectLiteralExpression(expressions[0])) {
        expressions = appendꓸꓸꓸ(slice<ж<astꓸExpression>>([tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), false)]), expressions);
    }

    if (len(expressions) == 1) {
        return expressions[0];
    }
    return tx.Transformer.Factory().NewAssignHelper(expressions, tx.compilerOptions.GetEmitScriptTarget());
}

internal static (slice<ж<astꓸExpression>>, slice<ж<astꓸObjectLiteralElement>>) combinePropertiesIntoNewExpression(this ref JSXTransformer tx, slice<ж<astꓸExpression>> expressions, slice<ж<astꓸObjectLiteralElement>> props) {
    if (len(props) == 0) {
        return (expressions, props);
    }
    var newObj = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(props), false);
    expressions = append(expressions, newObj);
    return (expressions, default);
}

internal static slice<ж<ast.Node>> transformJsxAttributesToProps(this ref JSXTransformer tx, slice<ж<ast.Node>> attrs, ж<ast.Node> childrenProp) {
    var props = new slice<ж<ast.Node>>(0, len(attrs));
    foreach (var (_, attr) in range(attrs)) {
        if (attr.Kind == ast.KindJsxSpreadAttribute) {
            var res = tx.transformJsxSpreadAttributesToProps(attr.AsJsxSpreadAttribute());
            props = appendꓸꓸꓸ(props, res);
        } else {
            props = append(props, tx.transformJsxAttributeToObjectLiteralElement(attr.AsJsxAttribute()));
        }
    }
    if (childrenProp != nil) {
        props = append(props, childrenProp);
    }
    return props;
}

} partial class jsxtransforms_package {

internal static bool hasProto(ref ast.ObjectLiteralExpression obj) {
    foreach (var (_, p) in range(obj.Properties.Nodes)) {
        if (ast.IsPropertyAssignment(p) && (ast.IsStringLiteral(p.Name()) || ast.IsIdentifier(p.Name())) && p.Name().Text() == "__proto__"u8) {
            return true;
        }
    }
    return false;
}

} partial class jsxtransformsᴛJSXTransformer {

internal static slice<ж<ast.Node>> transformJsxSpreadAttributesToProps(this ref JSXTransformer tx, ж<ast.JsxSpreadAttribute> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsObjectLiteralExpression(node.Expression) && !hasProto(ref (node.Expression.AsObjectLiteralExpression()).DerefOrNull())) {
        var (res, _) = tx.Transformer.Visitor().VisitSlice(node.Expression.Properties());
        return res;
    }
    return slice<ж<ast.Node>>([tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSpreadAssignment(tx.Transformer.Visitor().Visit(node.Expression))]);
}

internal static ж<ast.Node> transformJsxAttributeToObjectLiteralElement(this ref JSXTransformer tx, ж<ast.JsxAttribute> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = tx.getAttributeName(Ꮡnode);
    var expression = tx.transformJsxAttributeInitializer(node.Initializer);
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, name, nil, nil, expression);
}

internal static ж<ast.Node> getAttributeName(this ref JSXTransformer tx, ж<ast.JsxAttribute> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var name = node.Name();
    if (ast.IsIdentifier(name)) {
        @string text = name.Text();
        if (scanner.IsIdentifierText(text, core.LanguageVariantStandard)) {
            return name;
        }
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(text, ast.TokenFlagsNone);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(
        name.AsJsxNamespacedName().Namespace.Text() + ":"u8 + name.AsJsxNamespacedName().Name().Text(), ast.TokenFlagsNone);
}

internal static ж<ast.Node> transformJsxAttributeInitializer(this ref JSXTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return tx.Transformer.Factory().NewTrueExpression();
    }
    if (node.Kind == ast.KindStringLiteral) {
        var res = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(decodeEntities(node.Text()), node.AsStringLiteral().TokenFlags);
        res.Loc = node.Loc;
        res.AsStringLiteral().TokenFlags = node.AsStringLiteral().TokenFlags;
        return res;
    }
    if (node.Kind == ast.KindJsxExpression) {
        if (node.Expression() == nil) {
            return tx.Transformer.Factory().NewTrueExpression();
        }
        return tx.Transformer.Visitor().Visit(node.Expression());
    }
    if (ast.IsJsxElement(Ꮡnode) || ast.IsJsxSelfClosingElement(Ꮡnode) || ast.IsJsxFragment(Ꮡnode)) {
        tx.setInChild(false);
        return tx.Transformer.Visitor().Visit(Ꮡnode);
    }
    throw panic("Unhandled node kind found in jsx initializer: " + node.Kind.String());
}

internal static ж<ast.Node> visitJsxOpeningLikeElementOrFragmentJSX(this ref JSXTransformer tx, ж<astꓸExpression> tagName, ж<astꓸExpression> @object, ж<ast.Node> ᏑkeyAttr, ж<ast.NodeList> Ꮡchildren, core.TextRange location) {
    ref var keyAttr = ref ᏑkeyAttr.DerefOrNull();
    ref var children = ref Ꮡchildren.DerefOrNull();

    slice<ж<ast.Node>> nonWhitespaceChildren = default;
    if (Ꮡchildren != nil) {
        nonWhitespaceChildren = ast.GetSemanticJsxChildren(children.Nodes);
    }
    var isStaticChildren = len(nonWhitespaceChildren) > 1 || (len(nonWhitespaceChildren) == 1 && ast.IsJsxExpression(nonWhitespaceChildren[0]) && nonWhitespaceChildren[0].AsJsxExpression().DotDotDotToken != nil);
    var args = new slice<ж<ast.Node>>(0, 3);
    args = append(args, tagName, @object);
    if (ᏑkeyAttr != nil) {
        args = append(args, tx.transformJsxAttributeInitializer(keyAttr.Initializer()));
    }

    if (tx.compilerOptions.Jsx == core.JsxEmitReactJSXDev) {
        var originalFile = tx.Transformer.EmitContext().MostOriginal(tx.currentSourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        if (originalFile != nil && ast.IsSourceFile(originalFile)) {
            if (ᏑkeyAttr == nil) {
                args = append(args, tx.Transformer.Factory().NewVoidZeroExpression());
            }
            if (isStaticChildren) {
                args = append(args, tx.Transformer.Factory().NewTrueExpression());
            } else {
                args = append(args, tx.Transformer.Factory().NewFalseExpression());
            }
            var (line, col) = scanner.GetECMALineAndUTF16CharacterOfPosition(new ast.SourceFileжSourceFileLike(originalFile.AsSourceFile()), location.Pos());
            args = append(args, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(slice<ж<ast.Node>>([
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("fileName"), nil, nil, tx.getCurrentFileNameExpression()),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("lineNumber"), nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(strconv.FormatInt((int64)(line + 1), 10), ast.TokenFlagsNone)),
                tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAssignment(nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("columnNumber"), nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(strconv.FormatInt((int64)col + 1, 10), ast.TokenFlagsNone))
            ])), false));
            args = append(args, tx.Transformer.Factory().NewThisExpression());
        }
    }

    var element = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(tx.getJsxFactoryCallee(isStaticChildren), nil, nil, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(args), ast.NodeFlagsNone);
    element.Loc = location;

    if (tx.inJsxChild) {
        tx.Transformer.EmitContext().AddEmitFlags(element, printer.EFStartOnNewLine);
    }

    return element;
}

internal static ж<ast.Node> visitJsxOpeningFragmentJSX(this ж<JSXTransformer> Ꮡtx, ж<ast.JsxOpeningFragment> fragment, ж<ast.NodeList> Ꮡchildren, core.TextRange location) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var children = ref Ꮡchildren.DerefOrNull();

    ж<astꓸExpression> childrenProps = default;
    if (Ꮡchildren != nil && len(children.Nodes) > 0) {
        var result = Ꮡtx.convertJsxChildrenToChildrenPropObject(children.Nodes);
        if (result != nil) {
            childrenProps = result;
        }
    }
    if (childrenProps == nil) {
        childrenProps = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewObjectLiteralExpression(Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList([]), false);
    }
    return tx.visitJsxOpeningLikeElementOrFragmentJSX(
        tx.getImplicitJsxFragmentReference(),
        childrenProps,
        nil,
        Ꮡchildren,
        location);
}

internal static ж<ast.Node> createReactNamespace(this ref JSXTransformer tx, @string reactNamespace, ж<ast.Node> parent) {
    if (len(reactNamespace) == 0) {
        reactNamespace = "React";
    }
    var react = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(reactNamespace);
    react.Flags &= unchecked((ast.NodeFlags)~(ast.NodeFlags)(ast.NodeFlagsSynthesized));

    react.Parent = tx.Transformer.EmitContext().ParseNode(parent);

    var container = tx.emitResolver.GetReferencedExportContainer(react, false); if (container != nil && ast.IsModuleDeclaration(container)) {
        var containerName = tx.Transformer.Factory().NewGeneratedNameForNode(container);
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(containerName, nil, react, ast.NodeFlagsNone);
    }

    return react;
}

internal static ж<ast.Node> createJsxFactoryExpressionFromEntityName(this ref JSXTransformer tx, ж<ast.Node> Ꮡe, ж<ast.Node> parent) {
    ref var e = ref Ꮡe.DerefOrNull();

    if (ast.IsQualifiedName(Ꮡe)) {
        var left = tx.createJsxFactoryExpressionFromEntityName(e.AsQualifiedName().Left, parent);
        var right = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(e.AsQualifiedName().Right.Text());
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(left, nil, right, ast.NodeFlagsNone);
    }
    return tx.createReactNamespace(e.Text(), parent);
}

internal static ж<ast.Node> createJsxPseudoFactoryExpression(this ref JSXTransformer tx, ж<ast.Node> parent, ж<ast.Node> e, @string target) {
    if (e != nil) {
        return tx.createJsxFactoryExpressionFromEntityName(e, parent);
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPropertyAccessExpression(
        tx.createReactNamespace(tx.compilerOptions.ReactNamespace, parent),
        nil,
        tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(target),
        ast.NodeFlagsNone);
}

internal static ж<ast.Node> createJsxFactoryExpression(this ref JSXTransformer tx, ж<ast.Node> parent) {
    var e = tx.emitResolver.GetJsxFactoryEntity(tx.currentSourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return tx.createJsxPseudoFactoryExpression(parent, e, "createElement");
}

internal static ж<ast.Node> createJsxFragmentFactoryExpression(this ref JSXTransformer tx, ж<ast.Node> parent) {
    var e = tx.emitResolver.GetJsxFragmentFactoryEntity(tx.currentSourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    return tx.createJsxPseudoFactoryExpression(parent, e, "Fragment");
}

internal static ж<ast.Node> visitJsxOpeningLikeElementCreateElement(this ж<JSXTransformer> Ꮡtx, ж<ast.Node> Ꮡelement, ж<ast.NodeList> Ꮡchildren, core.TextRange location) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var element = ref Ꮡelement.DerefOrNull();
    ref var children = ref Ꮡchildren.DerefOrNull();
    var newChildrenᴷ = new stackAppend<ж<ast.Node>>();

    var tagName = tx.getTagName(Ꮡelement);
    var attrs = element.Attributes().Properties();
    ж<astꓸExpression> objectProperties = default;
    if (len(attrs) > 0) {
        objectProperties = tx.transformJsxAttributesToObjectProps(attrs, nil);
    } else {
        objectProperties = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindNullKeyword);
    }

    ж<astꓸExpression> callee = default;
    if (len(tx.importSpecifier) == 0) {
        callee = tx.createJsxFactoryExpression(Ꮡelement);
    } else {
        callee = tx.getImplicitImportForName("createElement");
    }

    slice<ж<ast.Node>> newChildren = default;
    if (Ꮡchildren != nil && len(children.Nodes) > 0) {
        foreach (var (_, c) in range(children.Nodes)) {
            var res = Ꮡtx.transformJsxChildToExpression(c);
            if (res != nil) {
                newChildren = append(newChildrenᴷ, newChildren, res);
            }
        }
    }

    if (len(newChildren) > 1) {
        foreach (var (_, child) in range(newChildren)) {
            Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().AddEmitFlags(child, printer.EFStartOnNewLine);
        }
    }

    var args = new slice<ж<astꓸExpression>>(0, len(newChildren) + 2);
    args = append(args, tagName);
    args = append(args, objectProperties);
    args = appendꓸꓸꓸ(args, newChildren);

    var result = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        callee,
        nil,
        nil,
        Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(args),
        ast.NodeFlagsNone);
    result.Loc = location;

    if (tx.inJsxChild) {
        Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().AddEmitFlags(result, printer.EFStartOnNewLine);
    }
    return result;
}

internal static ж<ast.Node> visitJsxOpeningFragmentCreateElement(this ж<JSXTransformer> Ꮡtx, ж<ast.JsxOpeningFragment> fragment, ж<ast.NodeList> Ꮡchildren, core.TextRange location) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var children = ref Ꮡchildren.DerefOrNull();
    var newChildrenᴷ = new stackAppend<ж<ast.Node>>();

    var tagName = tx.createJsxFragmentFactoryExpression(fragment.of(ast.JsxOpeningFragment.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var callee = tx.createJsxFactoryExpression(fragment.of(ast.JsxOpeningFragment.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    slice<ж<ast.Node>> newChildren = default;
    if (Ꮡchildren != nil && len(children.Nodes) > 0) {
        foreach (var (_, c) in range(children.Nodes)) {
            var res = Ꮡtx.transformJsxChildToExpression(c);
            if (res != nil) {
                newChildren = append(newChildrenᴷ, newChildren, res);
            }
        }
    }

    if (len(newChildren) > 1) {
        foreach (var (_, child) in range(newChildren)) {
            Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().AddEmitFlags(child, printer.EFStartOnNewLine);
        }
    }

    var args = new slice<ж<astꓸExpression>>(0, len(newChildren) + 2);
    args = append(args, tagName);
    args = append(args, Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewKeywordExpression(ast.KindNullKeyword));
    args = appendꓸꓸꓸ(args, newChildren);

    var result = Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewCallExpression(
        callee,
        nil,
        nil,
        Ꮡtx.of(JSXTransformer.ᏑTransformer).Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(args),
        ast.NodeFlagsNone);
    result.Loc = location;

    if (tx.inJsxChild) {
        Ꮡtx.of(JSXTransformer.ᏑTransformer).EmitContext().AddEmitFlags(result, printer.EFStartOnNewLine);
    }
    return result;
}

internal static ж<ast.Node> visitJsxText(this ref JSXTransformer tx, ж<ast.JsxText> Ꮡtext) {
    ref var text = ref Ꮡtext.DerefOrNull();

    @string @fixed = fixupWhitespaceAndDecodeEntities(text.Text);
    if (len(@fixed) == 0) {
        return default;
    }
    return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(@fixed, ast.TokenFlagsNone);
}

} partial class jsxtransforms_package {

internal static void addLineOfJsxText(ж<strings.Builder> b, @string trimmedLine, bool isInitial) {
    @string decoded = decodeEntities(trimmedLine);
    if (!isInitial) {
        b.WriteString(" ");
    }
    b.WriteString(decoded);
}

internal static @string fixupWhitespaceAndDecodeEntities(@string text) {
    var acc = Ꮡ(new strings.Builder());
    var initial = true;
    nint firstNonWhitespace = 0;
    nint lastNonWhitespaceEnd = -1;
    for (nint i = 0; i < len(text); i++) {
        var (c, size) = utf8.DecodeRuneInString(text.slice(i));
        if (stringutil.IsLineBreak(c)) {
            if (firstNonWhitespace != -1 && lastNonWhitespaceEnd != -1) {
                addLineOfJsxText(acc, text.slice(firstNonWhitespace, lastNonWhitespaceEnd + 1), initial);
                initial = false;
            }

            firstNonWhitespace = -1;
        } else if (!stringutil.IsWhiteSpaceSingleLine(c)) {
            lastNonWhitespaceEnd = i + size - 1;
            if (firstNonWhitespace == -1) {
                firstNonWhitespace = i;
            }
        }

        if (size > 1) {
            i += (size - 1);
        }
    }

    if (firstNonWhitespace != -1) {
        addLineOfJsxText(acc, text.slice(firstNonWhitespace), initial);
    }
    return acc.String();
}

} partial class jsxtransformsᴛJSXTransformer {

internal static ж<ast.Node> visitJsxExpression(this ref JSXTransformer tx, ж<ast.JsxExpression> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    var e = tx.Transformer.Visitor().Visit(expression.Expression);
    if (expression.DotDotDotToken != nil) {
        return tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewSpreadElement(e);
    }
    return e;
}

} partial class jsxtransforms_package {

internal static @string decodeEntities(@string text) {
    nint i = strings.IndexByte(text, (byte)'&');
    if (i < 0) {
        return text;
    }

    ref var result = ref heap(new strings.Builder(), out var Ꮡresult);
    Ꮡresult.Grow(len(text));
    for (;;) {
        Ꮡresult.WriteString(text.slice(0, i));
        text = text.slice(i);

        nint semi = strings.IndexByte(text, (byte)';');
        if (semi < 0) {
            break;
        }

        for (;;) {
            nint nextAmp = strings.IndexByte(text.slice(1, semi), (byte)'&');
            if (nextAmp < 0) {
                break;
            }
            Ꮡresult.WriteString(text.slice(0, nextAmp + 1));
            text = text.slice(nextAmp + 1);
            semi -= nextAmp + 1;
        }

        @string entity = text.slice(1, semi);
        var (decoded, ok) = decodeEntity(entity);
        if (ok) {
            Ꮡresult.WriteString(stringutil.EncodeJSStringRune(decoded));
        } else {
            Ꮡresult.WriteString(text.slice(0, semi + 1));
        }
        text = text.slice(semi + 1);

        i = strings.IndexByte(text, (byte)'&');
        if (i < 0) {
            break;
        }
    }
    Ꮡresult.WriteString(text);
    return result.String();
}

internal static (rune, bool) decodeEntity(@string entity) {
    if (len(entity) == 0) {
        return (0, false);
    }

    if (entity[0] == '#') {
        entity = entity[1..];
        if (len(entity) == 0) {
            return (0, false);
        }

        nint @base = 10;
        if (entity[0] == 'x') {
            @base = 16;
            entity = entity[1..];
        }

        if (len(entity) == 0) {
            return (0, false);
        }

        foreach (var (_, c) in entity) {
            if (@base == 16 && !stringutil.IsHexDigit(c)) {
                return (0, false);
            }
            if (@base == 10 && !stringutil.IsDigit(c)) {
                return (0, false);
            }
        }

        var (parsed, err) = strconv.ParseInt(entity, @base, 32);
        if (err != nil) {
            return (0, false);
        }
        return ((rune)parsed, true);
    }

    var (r, ok) = entities[entity, ꟷ];
    return (r, ok);
}

[GoLazy] partial class lazyᴛentities { internal static map<@string, rune> entities = new map<@string, rune>{
    ["quot"] = 0x0022,
    ["amp"] = 0x0026,
    ["apos"] = 0x0027,
    ["lt"] = 0x003C,
    ["gt"] = 0x003E,
    ["nbsp"] = 0x00A0,
    ["iexcl"] = 0x00A1,
    ["cent"] = 0x00A2,
    ["pound"] = 0x00A3,
    ["curren"] = 0x00A4,
    ["yen"] = 0x00A5,
    ["brvbar"] = 0x00A6,
    ["sect"] = 0x00A7,
    ["uml"] = 0x00A8,
    ["copy"] = 0x00A9,
    ["ordf"] = 0x00AA,
    ["laquo"] = 0x00AB,
    ["not"] = 0x00AC,
    ["shy"] = 0x00AD,
    ["reg"] = 0x00AE,
    ["macr"] = 0x00AF,
    ["deg"] = 0x00B0,
    ["plusmn"] = 0x00B1,
    ["sup2"] = 0x00B2,
    ["sup3"] = 0x00B3,
    ["acute"] = 0x00B4,
    ["micro"] = 0x00B5,
    ["para"] = 0x00B6,
    ["middot"] = 0x00B7,
    ["cedil"] = 0x00B8,
    ["sup1"] = 0x00B9,
    ["ordm"] = 0x00BA,
    ["raquo"] = 0x00BB,
    ["frac14"] = 0x00BC,
    ["frac12"] = 0x00BD,
    ["frac34"] = 0x00BE,
    ["iquest"] = 0x00BF,
    ["Agrave"] = 0x00C0,
    ["Aacute"] = 0x00C1,
    ["Acirc"] = 0x00C2,
    ["Atilde"] = 0x00C3,
    ["Auml"] = 0x00C4,
    ["Aring"] = 0x00C5,
    ["AElig"] = 0x00C6,
    ["Ccedil"] = 0x00C7,
    ["Egrave"] = 0x00C8,
    ["Eacute"] = 0x00C9,
    ["Ecirc"] = 0x00CA,
    ["Euml"] = 0x00CB,
    ["Igrave"] = 0x00CC,
    ["Iacute"] = 0x00CD,
    ["Icirc"] = 0x00CE,
    ["Iuml"] = 0x00CF,
    ["ETH"] = 0x00D0,
    ["Ntilde"] = 0x00D1,
    ["Ograve"] = 0x00D2,
    ["Oacute"] = 0x00D3,
    ["Ocirc"] = 0x00D4,
    ["Otilde"] = 0x00D5,
    ["Ouml"] = 0x00D6,
    ["times"] = 0x00D7,
    ["Oslash"] = 0x00D8,
    ["Ugrave"] = 0x00D9,
    ["Uacute"] = 0x00DA,
    ["Ucirc"] = 0x00DB,
    ["Uuml"] = 0x00DC,
    ["Yacute"] = 0x00DD,
    ["THORN"] = 0x00DE,
    ["szlig"] = 0x00DF,
    ["agrave"] = 0x00E0,
    ["aacute"] = 0x00E1,
    ["acirc"] = 0x00E2,
    ["atilde"] = 0x00E3,
    ["auml"] = 0x00E4,
    ["aring"] = 0x00E5,
    ["aelig"] = 0x00E6,
    ["ccedil"] = 0x00E7,
    ["egrave"] = 0x00E8,
    ["eacute"] = 0x00E9,
    ["ecirc"] = 0x00EA,
    ["euml"] = 0x00EB,
    ["igrave"] = 0x00EC,
    ["iacute"] = 0x00ED,
    ["icirc"] = 0x00EE,
    ["iuml"] = 0x00EF,
    ["eth"] = 0x00F0,
    ["ntilde"] = 0x00F1,
    ["ograve"] = 0x00F2,
    ["oacute"] = 0x00F3,
    ["ocirc"] = 0x00F4,
    ["otilde"] = 0x00F5,
    ["ouml"] = 0x00F6,
    ["divide"] = 0x00F7,
    ["oslash"] = 0x00F8,
    ["ugrave"] = 0x00F9,
    ["uacute"] = 0x00FA,
    ["ucirc"] = 0x00FB,
    ["uuml"] = 0x00FC,
    ["yacute"] = 0x00FD,
    ["thorn"] = 0x00FE,
    ["yuml"] = 0x00FF,
    ["OElig"] = 0x0152,
    ["oelig"] = 0x0153,
    ["Scaron"] = 0x0160,
    ["scaron"] = 0x0161,
    ["Yuml"] = 0x0178,
    ["fnof"] = 0x0192,
    ["circ"] = 0x02C6,
    ["tilde"] = 0x02DC,
    ["Alpha"] = 0x0391,
    ["Beta"] = 0x0392,
    ["Gamma"] = 0x0393,
    ["Delta"] = 0x0394,
    ["Epsilon"] = 0x0395,
    ["Zeta"] = 0x0396,
    ["Eta"] = 0x0397,
    ["Theta"] = 0x0398,
    ["Iota"] = 0x0399,
    ["Kappa"] = 0x039A,
    ["Lambda"] = 0x039B,
    ["Mu"] = 0x039C,
    ["Nu"] = 0x039D,
    ["Xi"] = 0x039E,
    ["Omicron"] = 0x039F,
    ["Pi"] = 0x03A0,
    ["Rho"] = 0x03A1,
    ["Sigma"] = 0x03A3,
    ["Tau"] = 0x03A4,
    ["Upsilon"] = 0x03A5,
    ["Phi"] = 0x03A6,
    ["Chi"] = 0x03A7,
    ["Psi"] = 0x03A8,
    ["Omega"] = 0x03A9,
    ["alpha"] = 0x03B1,
    ["beta"] = 0x03B2,
    ["gamma"] = 0x03B3,
    ["delta"] = 0x03B4,
    ["epsilon"] = 0x03B5,
    ["zeta"] = 0x03B6,
    ["eta"] = 0x03B7,
    ["theta"] = 0x03B8,
    ["iota"] = 0x03B9,
    ["kappa"] = 0x03BA,
    ["lambda"] = 0x03BB,
    ["mu"] = 0x03BC,
    ["nu"] = 0x03BD,
    ["xi"] = 0x03BE,
    ["omicron"] = 0x03BF,
    ["pi"] = 0x03C0,
    ["rho"] = 0x03C1,
    ["sigmaf"] = 0x03C2,
    ["sigma"] = 0x03C3,
    ["tau"] = 0x03C4,
    ["upsilon"] = 0x03C5,
    ["phi"] = 0x03C6,
    ["chi"] = 0x03C7,
    ["psi"] = 0x03C8,
    ["omega"] = 0x03C9,
    ["thetasym"] = 0x03D1,
    ["upsih"] = 0x03D2,
    ["piv"] = 0x03D6,
    ["ensp"] = 0x2002,
    ["emsp"] = 0x2003,
    ["thinsp"] = 0x2009,
    ["zwnj"] = 0x200C,
    ["zwj"] = 0x200D,
    ["lrm"] = 0x200E,
    ["rlm"] = 0x200F,
    ["ndash"] = 0x2013,
    ["mdash"] = 0x2014,
    ["lsquo"] = 0x2018,
    ["rsquo"] = 0x2019,
    ["sbquo"] = 0x201A,
    ["ldquo"] = 0x201C,
    ["rdquo"] = 0x201D,
    ["bdquo"] = 0x201E,
    ["dagger"] = 0x2020,
    ["Dagger"] = 0x2021,
    ["bull"] = 0x2022,
    ["hellip"] = 0x2026,
    ["permil"] = 0x2030,
    ["prime"] = 0x2032,
    ["Prime"] = 0x2033,
    ["lsaquo"] = 0x2039,
    ["rsaquo"] = 0x203A,
    ["oline"] = 0x203E,
    ["frasl"] = 0x2044,
    ["euro"] = 0x20AC,
    ["image"] = 0x2111,
    ["weierp"] = 0x2118,
    ["real"] = 0x211C,
    ["trade"] = 0x2122,
    ["alefsym"] = 0x2135,
    ["larr"] = 0x2190,
    ["uarr"] = 0x2191,
    ["rarr"] = 0x2192,
    ["darr"] = 0x2193,
    ["harr"] = 0x2194,
    ["crarr"] = 0x21B5,
    ["lArr"] = 0x21D0,
    ["uArr"] = 0x21D1,
    ["rArr"] = 0x21D2,
    ["dArr"] = 0x21D3,
    ["hArr"] = 0x21D4,
    ["forall"] = 0x2200,
    ["part"] = 0x2202,
    ["exist"] = 0x2203,
    ["empty"] = 0x2205,
    ["nabla"] = 0x2207,
    ["isin"] = 0x2208,
    ["notin"] = 0x2209,
    ["ni"] = 0x220B,
    ["prod"] = 0x220F,
    ["sum"] = 0x2211,
    ["minus"] = 0x2212,
    ["lowast"] = 0x2217,
    ["radic"] = 0x221A,
    ["prop"] = 0x221D,
    ["infin"] = 0x221E,
    ["ang"] = 0x2220,
    ["and"] = 0x2227,
    ["or"] = 0x2228,
    ["cap"] = 0x2229,
    ["cup"] = 0x222A,
    ["int"] = 0x222B,
    ["there4"] = 0x2234,
    ["sim"] = 0x223C,
    ["cong"] = 0x2245,
    ["asymp"] = 0x2248,
    ["ne"] = 0x2260,
    ["equiv"] = 0x2261,
    ["le"] = 0x2264,
    ["ge"] = 0x2265,
    ["sub"] = 0x2282,
    ["sup"] = 0x2283,
    ["nsub"] = 0x2284,
    ["sube"] = 0x2286,
    ["supe"] = 0x2287,
    ["oplus"] = 0x2295,
    ["otimes"] = 0x2297,
    ["perp"] = 0x22A5,
    ["sdot"] = 0x22C5,
    ["lceil"] = 0x2308,
    ["rceil"] = 0x2309,
    ["lfloor"] = 0x230A,
    ["rfloor"] = 0x230B,
    ["lang"] = 0x2329,
    ["rang"] = 0x232A,
    ["loz"] = 0x25CA,
    ["spades"] = 0x2660,
    ["clubs"] = 0x2663,
    ["hearts"] = 0x2665,
    ["diams"] = 0x2666
}; }

} // end jsxtransforms_package
