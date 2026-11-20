global using AssignmentTarget = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using cmp = cmp_package;
using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸany = Span<any>;
using static checker_package;

partial class checker_package {

public static ж<ast.Diagnostic> NewDiagnosticForNode(ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    ж<ast.SourceFile> @file = default;
    core.TextRange loc = default;
    if (node != nil) {
        @file = ast.GetSourceFileOfNode(node);
        loc = scanner.GetErrorRangeForNode(@file, node);
    }
    return ast.NewDiagnostic(@file, loc, message, args.ꓸꓸꓸ);
}

public static ж<ast.Diagnostic> NewDiagnosticChainForNode(ж<ast.Diagnostic> chain, ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    if (chain != nil) {
        return ast.NewDiagnosticChain(chain, message, args.ꓸꓸꓸ);
    }
    return NewDiagnosticForNode(node, message, args.ꓸꓸꓸ);
}

internal static V findInMap<K, V>(map<K, V> m, Func<V, bool> predicate) {
    foreach (var (_, value) in m) {
        if (predicate(value)) {
            return value;
        }
    }
    return @new<V>().ValueSlot;
}

internal static bool tokenIsIdentifierOrKeyword(ast.Kind token) {
    return token >= ast.KindIdentifier;
}

internal static bool tokenIsIdentifierOrKeywordOrGreaterThan(ast.Kind token) {
    return token == ast.KindGreaterThanToken || tokenIsIdentifierOrKeyword(token);
}

internal static bool hasOverrideModifier(ж<ast.Node> node) {
    return ast.HasSyntacticModifier(node, ast.ModifierFlagsOverride);
}

internal static bool hasAsyncModifier(ж<ast.Node> node) {
    return ast.HasSyntacticModifier(node, ast.ModifierFlagsAsync);
}

internal static ast.ModifierFlags getSelectedModifierFlags(ж<ast.Node> Ꮡnode, ast.ModifierFlags flags) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.ModifierFlags)(node.ModifierFlags() & flags);
}

internal static bool hasReadonlyModifier(ж<ast.Node> node) {
    return ast.HasModifier(node, ast.ModifierFlagsReadonly);
}

internal static bool isStaticPrivateIdentifierProperty(ref ast.Symbol s) {
    return s.ValueDeclaration != nil && ast.IsPrivateIdentifierClassElementDeclaration(s.ValueDeclaration) && ast.IsStatic(s.ValueDeclaration);
}

internal static bool isEmptyObjectLiteral(ж<ast.Node> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    return ast.IsObjectLiteralExpression(Ꮡexpression) && len(expression.Properties()) == 0;
}

partial struct AssignmentKind /*num:int32*/;

public static AssignmentKind AssignmentKindNone => /* iota */ 0;
public static AssignmentKind AssignmentKindDefinite => 1;
public static AssignmentKind AssignmentKindCompound => 2;

internal static AssignmentKind getAssignmentTargetKind(ж<ast.Node> node) {
    var target = ast.GetAssignmentTarget(node);
    if (target == nil) {
        return AssignmentKindNone;
    }
    switch (target.Kind) {
    case var ᴛ when ᴛ == ast.KindBinaryExpression: {
        var binaryOperator = target.AsBinaryExpression().OperatorToken.Kind;
        if (binaryOperator == ast.KindEqualsToken || ast.IsLogicalOrCoalescingAssignmentOperator(binaryOperator)) {
            return AssignmentKindDefinite;
        }
        return AssignmentKindCompound;
    }
    case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression || ᴛ == ast.KindPostfixUnaryExpression: {
        return AssignmentKindCompound;
    }
    case var ᴛ when ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement: {
        return AssignmentKindDefinite;
    }}

    throw panic("Unhandled case in getAssignmentTargetKind");
}

internal static bool isDeleteTarget(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsAccessExpression(Ꮡnode)) {
        return false;
    }
    Ꮡnode = ast.WalkUpParenthesizedExpressions(node.Parent); node = ref Ꮡnode.DerefOrNull();
    return Ꮡnode != nil && node.Kind == ast.KindDeleteExpression;
}

internal static bool isInCompoundLikeAssignment(ж<ast.Node> node) {
    var target = ast.GetAssignmentTarget(node);
    return target != nil && ast.IsAssignmentExpression(target, true) && isCompoundLikeAssignment(target);
}

internal static bool isCompoundLikeAssignment(ж<ast.Node> Ꮡassignment) {
    ref var assignment = ref Ꮡassignment.DerefOrNull();

    var right = ast.SkipParentheses(assignment.AsBinaryExpression().Right);
    return right.Kind == ast.KindBinaryExpression && isShiftOperatorOrHigher(right.AsBinaryExpression().OperatorToken.Kind);
}

internal static bool isConstTypeReference(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsTypeReferenceNode(Ꮡnode) && len(node.TypeArguments()) == 0 && ast.IsIdentifier(node.AsTypeReferenceNode().TypeName) && node.AsTypeReferenceNode().TypeName.Text() == "const"u8;
}

internal static bool isConstTypeReferenceName(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && ast.IsIdentifier(Ꮡnode) && node.Parent != nil && isConstTypeReference(node.Parent) && node.Parent.Parent != nil && ast.IsAssertionExpression(node.Parent.Parent);
}

internal static bool isExportAssignmentExpressionName(ж<ast.Node> node) {
    if (node == nil) {
        return false;
    }
    var current = node;
    while (current.Parent != nil && ast.IsPropertyAccessOrQualifiedName(current.Parent)) {
        current = current.Parent;
    }
    return current.Parent != nil && ast.IsExportAssignment(current.Parent) && current.Parent.Expression() == current;
}

public static ж<ast.Node> GetSingleVariableOfVariableStatement(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!ast.IsVariableStatement(Ꮡnode)) {
        return default;
    }
    return core.FirstOrNil(node.AsVariableStatement().DeclarationList.AsVariableDeclarationList().Declarations.Nodes);
}

internal static bool isTypeReferenceIdentifier(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (node.Parent.Kind == ast.KindQualifiedName) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return ast.IsTypeReferenceNode(node.Parent);
}

public static bool IsInTypeQuery(ж<ast.Node> node) {
    return ast.FindAncestorOrQuit(node, (ж<ast.Node> n) => {
        switch (n.Kind) {
        case var ᴛ when ᴛ == ast.KindTypeQuery: {
            return ast.FindAncestorTrue;
        }
        case var ᴛ when ᴛ == ast.KindIdentifier || ᴛ == ast.KindQualifiedName: {
            return ast.FindAncestorFalse;
        }}

        return ast.FindAncestorQuit;
    }) != nil;
}

internal static bool canHaveLocals(ref ast.Node node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindArrowFunction || ᴛ == ast.KindBlock || ᴛ == ast.KindCallSignature || ᴛ == ast.KindCaseBlock || ᴛ == ast.KindCatchClause || ᴛ == ast.KindClassStaticBlockDeclaration || ᴛ == ast.KindConditionalType || ᴛ == ast.KindConstructor || ᴛ == ast.KindConstructorType || ᴛ == ast.KindConstructSignature || ᴛ == ast.KindForStatement || ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindFunctionType || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindIndexSignature || ᴛ == ast.KindJSDocSignature || ᴛ == ast.KindMappedType || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature || ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindSourceFile || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration: {
        return true;
    }}

    return false;
}

internal static bool isShorthandAmbientModuleSymbol(ref ast.Symbol moduleSymbol) {
    return isShorthandAmbientModule(moduleSymbol.ValueDeclaration);
}

internal static bool isShorthandAmbientModule(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && node.Kind == ast.KindModuleDeclaration && node.Body() == nil;
}

internal static ж<ast.Node> getAliasDeclarationFromName(ref ast.Node node) {
    switch (node.Parent.Kind) {
    case var ᴛ when ᴛ == ast.KindImportClause || ᴛ == ast.KindImportSpecifier || ᴛ == ast.KindNamespaceImport || ᴛ == ast.KindExportSpecifier || ᴛ == ast.KindExportAssignment || ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindNamespaceExport: {
        return node.Parent;
    }
    case var ᴛ when ᴛ == ast.KindQualifiedName: {
        return getAliasDeclarationFromName(ref (node.Parent).DerefOrNull());
    }}

    return default;
}

internal static @string entityNameToString(ж<ast.Node> name) {
    return ast.EntityNameToString(name, scanner.GetTextOfNode);
}

internal static ж<ast.Node> getContainingQualifiedNameNode(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (ast.IsQualifiedName(node.Parent)) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return Ꮡnode;
}

internal static bool isSideEffectImport(ж<ast.Node> node) {
    var ancestor = ast.FindAncestor(node, ast.IsImportDeclaration);
    return ancestor != nil && ancestor.ImportClause() == nil;
}

internal static ж<ast.Node> getExternalModuleRequireArgument(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsVariableDeclarationInitializedToRequire(Ꮡnode)) {
        return node.Initializer().Arguments()[0];
    }
    return default;
}

internal static bool isRightSideOfAccessExpression(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Parent != nil && (ast.IsPropertyAccessExpression(node.Parent) && node.Parent.Name() == Ꮡnode || ast.IsElementAccessExpression(node.Parent) && node.Parent.AsElementAccessExpression().ArgumentExpression == Ꮡnode);
}

internal static bool isTopLevelInExternalModuleAugmentation(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && node.Parent != nil && ast.IsModuleBlock(node.Parent) && ast.IsExternalModuleAugmentation(node.Parent.Parent);
}

internal static bool isSyntacticDefault(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.IsExportAssignment(Ꮡnode) && !node.AsExportAssignment().IsExportEquals) || ast.HasSyntacticModifier(Ꮡnode, ast.ModifierFlagsDefault) || ast.IsExportSpecifier(Ꮡnode) || ast.IsNamespaceExport(Ꮡnode);
}

internal static bool hasExportAssignmentSymbol(ref ast.Symbol moduleSymbol) {
    return moduleSymbol.Exports[ast.InternalSymbolNameExportEquals] != nil;
}

internal static bool isTypeAlias(ж<ast.Node> node) {
    return ast.IsTypeOrJSTypeAliasDeclaration(node);
}

internal static bool hasOnlyExpressionInitializer(ref ast.Node node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableDeclaration || ᴛ == ast.KindParameter || ᴛ == ast.KindBindingElement || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindEnumMember: {
        return true;
    }}

    return false;
}

internal static bool hasDotDotDotToken(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindParameter: {
        return node.AsParameterDeclaration().DotDotDotToken != nil;
    }
    case var ᴛ when ᴛ == ast.KindBindingElement: {
        return node.AsBindingElement().DotDotDotToken != nil;
    }
    case var ᴛ when ᴛ == ast.KindNamedTupleMember: {
        return node.AsNamedTupleMember().DotDotDotToken != nil;
    }
    case var ᴛ when ᴛ == ast.KindJsxExpression: {
        return node.AsJsxExpression().DotDotDotToken != nil;
    }}

    return false;
}

public static bool IsTypeAny(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return Ꮡt != nil && (TypeFlags)(t.flags & TypeFlagsAny) != 0;
}

internal static bool isJSDocOptionalParameter(ref ast.ParameterDeclaration node) {
    return false;
}

internal static bool isExclamationToken(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && node.Kind == ast.KindExclamationToken;
}

internal static bool isOptionalDeclaration(ж<ast.Node> declaration) {
    return ast.HasQuestionToken(declaration);
}

} partial class checkerᴛChecker {

internal static bool isOptionalParameter(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsParameterDeclaration(Ꮡnode) && node.QuestionToken() != nil) {
        return true;
    }
    if (!ast.IsParameterDeclaration(Ꮡnode)) {
        return false;
    }
    if (node.Initializer() != nil) {
        var signature = Ꮡc.getSignatureFromDeclaration(node.Parent);
        nint parameterIndex = core.FindIndex(node.Parent.Parameters(), (ж<astꓸParameterDeclarationNode> p) => p == Ꮡnode);
        debug.Assert(parameterIndex >= 0);
        return parameterIndex >= Ꮡc.getMinArgumentCountEx(signature, (MinArgumentCountFlags)(MinArgumentCountFlagsStrongArityForUntypedJS | MinArgumentCountFlagsVoidIsNonOptional));
    }
    var iife = ast.GetImmediatelyInvokedFunctionExpression(node.Parent);
    if (iife != nil) {
        nint parameterIndex = core.FindIndex(node.Parent.Parameters(), (ж<astꓸParameterDeclarationNode> p) => p == Ꮡnode);
        return node.Type() == nil && node.AsParameterDeclaration().DotDotDotToken == nil && parameterIndex >= len(Ꮡc.getEffectiveCallArguments(iife));
    }
    return false;
}

} partial class checker_package {

internal static bool isEmptyArrayLiteral(ж<ast.Node> Ꮡexpression) {
    ref var expression = ref Ꮡexpression.DerefOrNull();

    return ast.IsArrayLiteralExpression(Ꮡexpression) && len(expression.Elements()) == 0;
}

internal static bool declarationBelongsToPrivateAmbientMember(ж<ast.Node> declaration) {
    var root = ast.GetRootDeclaration(declaration);
    var memberDeclaration = root;
    if (root.Kind == ast.KindParameter) {
        memberDeclaration = root.Parent;
    }
    return isPrivateWithinAmbient(memberDeclaration);
}

internal static bool isPrivateWithinAmbient(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.HasModifier(Ꮡnode, ast.ModifierFlagsPrivate) || ast.IsPrivateIdentifierClassElementDeclaration(Ꮡnode)) && (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0;
}

internal static bool isTypeAssertion(ж<ast.Node> node) {
    return ast.IsAssertionExpression(ast.SkipParentheses(node));
}

internal static ast.SymbolTable createSymbolTable(slice<ж<ast.Symbol>> symbols) {
    if (len(symbols) == 0) {
        return default;
    }
    var result = new ast.SymbolTable(0);
    foreach (var (_, symbol) in range(symbols)) {
        result[symbol.Name] = symbol;
    }
    return result;
}

} partial class checkerᴛChecker {

internal static void sortSymbols(this ref Checker c, slice<ж<ast.Symbol>> symbols) {
    slices.SortFunc(symbols, c.compareSymbols);
}

internal static nint compareSymbolsWorker(this ref Checker c, ж<ast.Symbol> Ꮡs1, ж<ast.Symbol> Ꮡs2) {
    ref var s1 = ref Ꮡs1.DerefOrNull();
    ref var s2 = ref Ꮡs2.DerefOrNull();

    if (Ꮡs1 == Ꮡs2) {
        return 0;
    }
    if (Ꮡs1 == nil) {
        return 1;
    }
    if (Ꮡs2 == nil) {
        return -1;
    }
    if (len(s1.Declarations) != 0 && len(s2.Declarations) != 0) {
        nint r = c.compareNodes(s1.Declarations[0], s2.Declarations[0]); if (r != 0) {
            return r;
        }
    } else if (len(s1.Declarations) != 0) {
        return -1;
    } else if (len(s2.Declarations) != 0) {
        return 1;
    }
    {
        nint r = strings.Compare(s1.Name, s2.Name); if (r != 0) {
            return r;
        }
    }
    return (nint)ast.GetSymbolId(Ꮡs1) - (nint)ast.GetSymbolId(Ꮡs2);
}

internal static nint compareNodes(this ref Checker c, ж<ast.Node> Ꮡn1, ж<ast.Node> Ꮡn2) {
    ref var n1 = ref Ꮡn1.DerefOrNull();
    ref var n2 = ref Ꮡn2.DerefOrNull();

    if (Ꮡn1 == Ꮡn2) {
        return 0;
    }
    if (Ꮡn1 == nil) {
        return 1;
    }
    if (Ꮡn2 == nil) {
        return -1;
    }
    var s1 = ast.GetSourceFileOfNode(Ꮡn1);
    var s2 = ast.GetSourceFileOfNode(Ꮡn2);
    if (s1 != s2) {
        nint f1 = c.fileIndexMap[s1];
        nint f2 = c.fileIndexMap[s2];
        return f1 - f2;
    }
    return n1.Pos() - n2.Pos();
}

} partial class checker_package {

public static nint CompareTypes(ж<Type> Ꮡt1, ж<Type> Ꮡt2) {
    ref var t1 = ref Ꮡt1.DerefOrNull();
    ref var t2 = ref Ꮡt2.DerefOrNull();

    if (Ꮡt1 == Ꮡt2) {
        return 0;
    }
    if (Ꮡt1 == nil) {
        return -1;
    }
    if (Ꮡt2 == nil) {
        return 1;
    }
    if (t1.checker != t2.checker) {
        throw panic("Cannot compare types from different checkers");
    }
    {
        nint c = getSortOrderFlags(ref (Ꮡt1).DerefOrNull()) - getSortOrderFlags(ref (Ꮡt2).DerefOrNull()); if (c != 0) {
            return c;
        }
    }
    {
        nint c = compareTypeNames(ref (Ꮡt1).DerefOrNull(), ref (Ꮡt2).DerefOrNull()); if (c != 0) {
            return c;
        }
    }
    switch (ᐧ) {
    case {} when (TypeFlags)(t1.flags & ((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)((TypeFlags)(TypeFlagsAny | TypeFlagsUnknown) | TypeFlagsString) | TypeFlagsNumber) | TypeFlagsBoolean) | TypeFlagsBigInt) | TypeFlagsESSymbol) | TypeFlagsVoid) | TypeFlagsUndefined) | TypeFlagsNull) | TypeFlagsNever) | TypeFlagsNonPrimitive))) != 0: {
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsObject) != 0: {
        if ((ObjectFlags)(t1.objectFlags & (uint32)ObjectFlagsInstantiationExpressionType) != 0 && (ObjectFlags)(t2.objectFlags & (uint32)ObjectFlagsInstantiationExpressionType) != 0) {
            ж<ast.Node> declaration1 = default;
            ж<ast.Node> declaration2 = default;
            if (t1.symbol != nil && len(t1.symbol.Declarations) != 0) {
                declaration1 = t1.symbol.Declarations[0];
            }
            if (t2.symbol != nil && len(t2.symbol.Declarations) != 0) {
                declaration2 = t2.symbol.Declarations[0];
            }
            {
                nint c = t1.checker.compareNodes(declaration1, declaration2); if (c != 0) {
                    return c;
                }
            }
            {
                nint c = t1.checker.compareNodes(t1.AsInstantiationExpressionType().node, t2.AsInstantiationExpressionType().node); if (c != 0) {
                    return c;
                }
            }
        } else {
            nint c = t1.checker.compareSymbols(t1.symbol, t2.symbol); if (c != 0) {
                return c;
            }
        }
        if ((ObjectFlags)(t1.objectFlags & ObjectFlagsReference) != 0 && (ObjectFlags)(t2.objectFlags & ObjectFlagsReference) != 0) {
            var r1 = t1.AsTypeReference();
            var r2 = t2.AsTypeReference();
            if ((ObjectFlags)(r1.target.objectFlags & ObjectFlagsTuple) != 0 && (ObjectFlags)(r2.target.objectFlags & ObjectFlagsTuple) != 0) {
                nint c = compareTupleTypes(r1.target.AsTupleType(), r2.target.AsTupleType()); if (c != 0) {
                    return c;
                }
            }
            if (r1.node == nil && r2.node == nil) {
                nint c = compareTypeLists(t1.AsTypeReference().resolvedTypeArguments, t2.AsTypeReference().resolvedTypeArguments); if (c != 0) {
                    return c;
                }
            } else {
                {
                    nint c = t1.checker.compareNodes(r1.node, r2.node); if (c != 0) {
                        return c;
                    }
                }
                {
                    nint c = compareTypeMappers(t1.AsObjectType().mapper, t2.AsObjectType().mapper); if (c != 0) {
                        return c;
                    }
                }
            }
        } else if ((ObjectFlags)(t1.objectFlags & ObjectFlagsReference) != 0) {
            return -1;
        } else if ((ObjectFlags)(t2.objectFlags & ObjectFlagsReference) != 0) {
            return 1;
        } else {
            {
                nint c = (nint)((ObjectFlags)(t1.objectFlags & ObjectFlagsObjectTypeKindMask)) - (nint)((ObjectFlags)(t2.objectFlags & ObjectFlagsObjectTypeKindMask)); if (c != 0) {
                    return c;
                }
            }
            if ((ObjectFlags)(t1.objectFlags & ObjectFlagsReverseMapped) != 0) {
                var r1 = t1.AsReverseMappedType();
                var r2 = t2.AsReverseMappedType();
                {
                    nint c = CompareTypes(r1.source, r2.source); if (c != 0) {
                        return c;
                    }
                }
                {
                    nint c = CompareTypes(r1.mappedType, r2.mappedType); if (c != 0) {
                        return c;
                    }
                }
                {
                    nint c = CompareTypes(r1.constraintType, r2.constraintType); if (c != 0) {
                        return c;
                    }
                }
            }
            var m1 = t1.AsObjectType().mapper;
            var m2 = t2.AsObjectType().mapper;
            if ((ObjectFlags)(t1.objectFlags & ObjectFlagsMapped) != 0) {
                if (m1 != nil) {
                    m1 = m1.data._<ж<CompositeTypeMapper>>().m2;
                }
                if (m2 != nil) {
                    m2 = m2.data._<ж<CompositeTypeMapper>>().m2;
                }
            }
            {
                nint c = compareTypeMappers(m1, m2); if (c != 0) {
                    return c;
                }
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsUnion) != 0: {
        var o1 = t1.AsUnionType().origin;
        var o2 = t2.AsUnionType().origin;
        if (o1 == nil && o2 == nil) {
            nint c = compareTypeLists(t1.Types(), t2.Types()); if (c != 0) {
                return c;
            }
        } else if (o1 == nil) {
            return 1;
        } else if (o2 == nil) {
            return -1;
        } else {
            nint c = CompareTypes(o1, o2); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsIntersection) != 0: {
        {
            nint c = compareTypeLists(t1.Types(), t2.Types()); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & ((TypeFlags)((TypeFlags)(TypeFlagsEnum | TypeFlagsEnumLiteral) | TypeFlagsUniqueESSymbol))) != 0: {
        {
            nint c = t1.checker.compareSymbols(t1.symbol, t2.symbol); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsStringLiteral) != 0: {
        {
            nint c = strings.Compare(t1.AsLiteralType().value._<@string>(), t2.AsLiteralType().value._<@string>()); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsNumberLiteral) != 0: {
        {
            nint c = cmp.Compare(t1.AsLiteralType().value._<jsnum.Number>(), t2.AsLiteralType().value._<jsnum.Number>()); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsBigIntLiteral) != 0: {
        {
            nint c = getBigIntLiteralValue(Ꮡt1).Compare(getBigIntLiteralValue(Ꮡt2)); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsBooleanLiteral) != 0: {
        var b1 = t1.AsLiteralType().value._<bool>();
        var b2 = t2.AsLiteralType().value._<bool>();
        if (b1 != b2) {
            if (b1) {
                return 1;
            }
            return -1;
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsTypeParameter) != 0: {
        {
            nint c = t1.checker.compareSymbols(t1.symbol, t2.symbol); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsIndex) != 0: {
        {
            nint c = CompareTypes(t1.AsIndexType().target, t2.AsIndexType().target); if (c != 0) {
                return c;
            }
        }
        {
            nint c = (nint)t1.AsIndexType().indexFlags - (nint)t2.AsIndexType().indexFlags; if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsIndexedAccess) != 0: {
        {
            nint c = CompareTypes(t1.AsIndexedAccessType().objectType, t2.AsIndexedAccessType().objectType); if (c != 0) {
                return c;
            }
        }
        {
            nint c = CompareTypes(t1.AsIndexedAccessType().indexType, t2.AsIndexedAccessType().indexType); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsConditional) != 0: {
        {
            nint c = t1.checker.compareNodes(t1.AsConditionalType().root.node.of(ast.ConditionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), t2.AsConditionalType().root.node.of(ast.ConditionalTypeNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); if (c != 0) {
                return c;
            }
        }
        {
            nint c = compareTypeMappers(t1.AsConditionalType().mapper, t2.AsConditionalType().mapper); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsSubstitution) != 0: {
        {
            nint c = CompareTypes(t1.AsSubstitutionType().baseType, t2.AsSubstitutionType().baseType); if (c != 0) {
                return c;
            }
        }
        {
            nint c = CompareTypes(t1.AsSubstitutionType().constraint, t2.AsSubstitutionType().constraint); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsTemplateLiteral) != 0: {
        {
            nint c = slices.Compare<slice<@string>, @string>(t1.AsTemplateLiteralType().texts, t2.AsTemplateLiteralType().texts); if (c != 0) {
                return c;
            }
        }
        {
            nint c = compareTypeLists(t1.AsTemplateLiteralType().types, t2.AsTemplateLiteralType().types); if (c != 0) {
                return c;
            }
        }
        break;
    }
    case {} when (TypeFlags)(t1.flags & TypeFlagsStringMapping) != 0: {
        {
            nint c = CompareTypes(t1.AsStringMappingType().target, t2.AsStringMappingType().target); if (c != 0) {
                return c;
            }
        }
        break;
    }}

    return (nint)t1.id - (nint)t2.id;
}

internal static nint getSortOrderFlags(ref Type t) {
    if ((TypeFlags)(t.flags & ((TypeFlags)(TypeFlagsEnumLiteral | TypeFlagsEnum))) != 0 && (TypeFlags)(t.flags & TypeFlagsUnion) == 0) {
        return (nint)TypeFlagsEnum;
    }
    return (nint)t.flags;
}

internal static nint compareTypeNames(ref Type t1, ref Type t2) {
    var s1 = getTypeNameSymbol(ref t1);
    var s2 = getTypeNameSymbol(ref t2);
    if (s1 == s2) {
        return compareTypeLists(t1.alias.TypeArguments(), t2.alias.TypeArguments());
    }
    if (s1 == nil) {
        return 1;
    }
    if (s2 == nil) {
        return -1;
    }
    nint c = strings.Compare(s1.Name, s2.Name); if (c != 0) {
        return c;
    }
    return t1.checker.compareSymbols(s1, s2);
}

internal static ж<ast.Symbol> getTypeNameSymbol(ref Type t) {
    if (t.alias != nil) {
        return t.alias.symbol;
    }
    if ((TypeFlags)(t.flags & ((TypeFlags)(TypeFlagsTypeParameter | TypeFlagsStringMapping))) != 0 || (ObjectFlags)(t.objectFlags & ((ObjectFlags)(ObjectFlagsClassOrInterface | ObjectFlagsReference))) != 0) {
        return t.symbol;
    }
    return default;
}

internal static ж<ast.Symbol> getObjectTypeName(ref Type t) {
    if ((ObjectFlags)(t.objectFlags & ((ObjectFlags)(ObjectFlagsClassOrInterface | ObjectFlagsReference))) != 0) {
        return t.symbol;
    }
    return default;
}

internal static nint compareTupleTypes(ж<TupleType> Ꮡt1, ж<TupleType> Ꮡt2) {
    ref var t1 = ref Ꮡt1.DerefOrNull();
    ref var t2 = ref Ꮡt2.DerefOrNull();

    if (Ꮡt1 == Ꮡt2) {
        return 0;
    }
    if (t1.@readonly != t2.@readonly) {
        return core.IfElse<nint>(t1.@readonly, 1, -1);
    }
    if (len(t1.elementInfos) != len(t2.elementInfos)) {
        return len(t1.elementInfos) - len(t2.elementInfos);
    }
    foreach (var i in range(len(t1.elementInfos))) {
        nint c = (nint)t1.elementInfos[i].flags - (nint)t2.elementInfos[i].flags; if (c != 0) {
            return c;
        }
    }
    foreach (var i in range(len(t1.elementInfos))) {
        nint c = compareElementLabels(t1.elementInfos[i].labeledDeclaration, t2.elementInfos[i].labeledDeclaration); if (c != 0) {
            return c;
        }
    }
    return 0;
}

internal static nint compareElementLabels(ж<ast.Node> Ꮡn1, ж<ast.Node> Ꮡn2) {
    ref var n1 = ref Ꮡn1.DerefOrNull();
    ref var n2 = ref Ꮡn2.DerefOrNull();

    if (Ꮡn1 == Ꮡn2) {
        return 0;
    }
    if (Ꮡn1 == nil) {
        return -1;
    }
    if (Ꮡn2 == nil) {
        return 1;
    }
    return strings.Compare(n1.Name().Text(), n2.Name().Text());
}

internal static nint compareTypeLists(slice<ж<Type>> s1, slice<ж<Type>> s2) {
    if (len(s1) != len(s2)) {
        return len(s1) - len(s2);
    }
    foreach (var (i, t1) in range(s1)) {
        nint c = CompareTypes(t1, s2[i]); if (c != 0) {
            return c;
        }
    }
    return 0;
}

internal static nint compareTypeMappers(ж<TypeMapper> Ꮡm1, ж<TypeMapper> Ꮡm2) {
    ref var m1 = ref Ꮡm1.DerefOrNull();
    ref var m2 = ref Ꮡm2.DerefOrNull();

    if (Ꮡm1 == Ꮡm2) {
        return 0;
    }
    if (Ꮡm1 == nil) {
        return 1;
    }
    if (Ꮡm2 == nil) {
        return -1;
    }
    var kind1 = m1.Kind();
    var kind2 = m2.Kind();
    if (kind1 != kind2) {
        return (nint)kind1 - (nint)kind2;
    }
    switch (kind1) {
    case var ᴛ when ᴛ == TypeMapperKindSimple: {
        var m1Δ2 = m1.data._<ж<SimpleTypeMapper>>();
        var m2Δ2 = m2.data._<ж<SimpleTypeMapper>>();
        {
            nint c = CompareTypes(m1Δ2.source, m2Δ2.source); if (c != 0) {
                return c;
            }
        }
        return CompareTypes(m1Δ2.target, m2Δ2.target);
    }
    case var ᴛ when ᴛ == TypeMapperKindArray: {
        var m1Δ3 = m1.data._<ж<ArrayTypeMapper>>();
        var m2Δ3 = m2.data._<ж<ArrayTypeMapper>>();
        {
            nint c = compareTypeLists(m1Δ3.sources, m2Δ3.sources); if (c != 0) {
                return c;
            }
        }
        return compareTypeLists(m1Δ3.targets, m2Δ3.targets);
    }
    case var ᴛ when ᴛ == TypeMapperKindMerged: {
        var m1Δ4 = m1.data._<ж<MergedTypeMapper>>();
        var m2Δ4 = m2.data._<ж<MergedTypeMapper>>();
        {
            nint c = compareTypeMappers(m1Δ4.m1, m2Δ4.m1); if (c != 0) {
                return c;
            }
        }
        return compareTypeMappers(m1Δ4.m2, m2Δ4.m2);
    }}

    return 0;
}

internal static ast.ModifierFlags getDeclarationModifierFlagsFromSymbol(ref ast.Symbol s) {
    return getDeclarationModifierFlagsFromSymbolEx(ref s, false);
}

internal static ast.ModifierFlags getDeclarationModifierFlagsFromSymbolEx(ref ast.Symbol s, bool isWrite) {
    if ((ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsSynthetic) != 0) {
        ast.ModifierFlags accessModifier = default;
        switch (ᐧ) {
        case {} when !isWrite && (ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsContainsPublic) != 0 || isWrite && (ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsContainsWritePublic) != 0: {
            accessModifier = ast.ModifierFlagsPublic;
            break;
        }
        case {} when !isWrite && (ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsContainsProtected) != 0 || isWrite && (ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsContainsWriteProtected) != 0: {
            accessModifier = ast.ModifierFlagsProtected;
            break;
        }
        case {} when !isWrite && (ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsContainsPrivate) != 0 || isWrite && (ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsContainsWritePrivate) != 0: {
            accessModifier = ast.ModifierFlagsPrivate;
            break;
        }}

        if ((ast.CheckFlags)(s.CheckFlags & ast.CheckFlagsContainsStatic) != 0) {
            return (ast.ModifierFlags)(accessModifier | ast.ModifierFlagsStatic);
        }
        return accessModifier;
    }
    if (s.ValueDeclaration != nil) {
        ж<ast.Node> declaration = default;
        if (isWrite) {
            declaration = core.Find<ж<ast.Node>>(s.Declarations, ast.IsSetAccessorDeclaration);
        }
        if (declaration == nil && (ast.SymbolFlags)(s.Flags & ast.SymbolFlagsGetAccessor) != 0) {
            declaration = core.Find<ж<ast.Node>>(s.Declarations, ast.IsGetAccessorDeclaration);
        }
        if (declaration == nil) {
            declaration = s.ValueDeclaration;
        }
        var flags = ast.GetCombinedModifierFlags(declaration);
        if (s.Parent != nil && (ast.SymbolFlags)(s.Parent.Flags & ast.SymbolFlagsClass) != 0) {
            return flags;
        }
        return (ast.ModifierFlags)(flags & ~((ast.ModifierFlags)ast.ModifierFlagsAccessibilityModifier));
    }
    if ((ast.SymbolFlags)(s.Flags & ast.SymbolFlagsPrototype) != 0) {
        return (ast.ModifierFlags)(ast.ModifierFlagsPublic | ast.ModifierFlagsStatic);
    }
    return ast.ModifierFlagsNone;
}

internal static bool isExponentiationOperator(ast.Kind kind) {
    return kind == ast.KindAsteriskAsteriskToken;
}

internal static bool isMultiplicativeOperator(ast.Kind kind) {
    return kind == ast.KindAsteriskToken || kind == ast.KindSlashToken || kind == ast.KindPercentToken;
}

internal static bool isMultiplicativeOperatorOrHigher(ast.Kind kind) {
    return isExponentiationOperator(kind) || isMultiplicativeOperator(kind);
}

internal static bool isAdditiveOperator(ast.Kind kind) {
    return kind == ast.KindPlusToken || kind == ast.KindMinusToken;
}

internal static bool isAdditiveOperatorOrHigher(ast.Kind kind) {
    return isAdditiveOperator(kind) || isMultiplicativeOperatorOrHigher(kind);
}

internal static bool isShiftOperator(ast.Kind kind) {
    return kind == ast.KindLessThanLessThanToken || kind == ast.KindGreaterThanGreaterThanToken || kind == ast.KindGreaterThanGreaterThanGreaterThanToken;
}

internal static bool isShiftOperatorOrHigher(ast.Kind kind) {
    return isShiftOperator(kind) || isAdditiveOperatorOrHigher(kind);
}

internal static bool isRelationalOperator(ast.Kind kind) {
    return kind == ast.KindLessThanToken || kind == ast.KindLessThanEqualsToken || kind == ast.KindGreaterThanToken || kind == ast.KindGreaterThanEqualsToken || kind == ast.KindInstanceOfKeyword || kind == ast.KindInKeyword;
}

internal static bool isRelationalOperatorOrHigher(ast.Kind kind) {
    return isRelationalOperator(kind) || isShiftOperatorOrHigher(kind);
}

internal static bool isEqualityOperator(ast.Kind kind) {
    return kind == ast.KindEqualsEqualsToken || kind == ast.KindEqualsEqualsEqualsToken || kind == ast.KindExclamationEqualsToken || kind == ast.KindExclamationEqualsEqualsToken;
}

internal static bool isEqualityOperatorOrHigher(ast.Kind kind) {
    return isEqualityOperator(kind) || isRelationalOperatorOrHigher(kind);
}

internal static bool isBitwiseOperator(ast.Kind kind) {
    return kind == ast.KindAmpersandToken || kind == ast.KindBarToken || kind == ast.KindCaretToken;
}

internal static bool isBitwiseOperatorOrHigher(ast.Kind kind) {
    return isBitwiseOperator(kind) || isEqualityOperatorOrHigher(kind);
}

internal static bool isLogicalOperatorOrHigher(ast.Kind kind) {
    return ast.IsLogicalBinaryOperator(kind) || isBitwiseOperatorOrHigher(kind);
}

internal static bool isAssignmentOperatorOrHigher(ast.Kind kind) {
    return kind == ast.KindQuestionQuestionToken || isLogicalOperatorOrHigher(kind) || ast.IsAssignmentOperator(kind);
}

internal static bool isBinaryOperator(ast.Kind kind) {
    return isAssignmentOperatorOrHigher(kind) || kind == ast.KindCommaToken;
}

internal static bool isObjectLiteralType(ref Type t) {
    return (ObjectFlags)(t.objectFlags & ObjectFlagsObjectLiteral) != 0;
}

internal static bool isDeclarationReadonly(ж<ast.Node> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    return (ast.ModifierFlags)(ast.GetCombinedModifierFlags(Ꮡdeclaration) & ast.ModifierFlagsReadonly) != 0 && !ast.IsParameterPropertyDeclaration(Ꮡdeclaration, declaration.Parent);
}

internal static UntypedInt orderedSetMapThreshold => 16;

partial struct orderedSet<T> {
    internal map<T, EmptyStruct> valuesByKey;
    internal slice<T> values;
}

} partial class checkerᴛorderedSet {

internal static bool contains<T>(this ref orderedSet<T> s, T value) {
    if (s.valuesByKey == nil) {
        return slices.Contains(s.values, value);
    }
    var (_, ok) = s.valuesByKey[value, ꟷ];
    return ok;
}

internal static void add<T>(this ref orderedSet<T> s, T value) {
    s.values = append(s.values, value);
    if (s.valuesByKey == nil) {
        if (len(s.values) <= orderedSetMapThreshold) {
            return;
        }
        s.valuesByKey = new map<T, EmptyStruct>(len(s.values));
        foreach (var (_, v) in range(s.values.slice(0, len(s.values) - 1))) {
            s.valuesByKey[v] = new EmptyStruct();
        }
    }
    s.valuesByKey[value] = new EmptyStruct();
}

} partial class checker_package {

internal static ж<ast.Node> getContainingFunctionOrClassStaticBlock(ref ast.Node node) {
    return ast.FindAncestor(node.Parent, ast.IsFunctionLikeOrClassStaticBlockDeclaration);
}

internal static bool isNodeDescendantOf(ж<ast.Node> Ꮡnode, ж<ast.Node> ancestor) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (Ꮡnode != nil) {
        if (Ꮡnode == ancestor) {
            return true;
        }
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }
    return false;
}

internal static bool isTypeUsableAsPropertyName(ref Type t) {
    return (TypeFlags)(t.flags & TypeFlagsStringOrNumberLiteralOrUnique) != 0;
}

internal static @string getPropertyNameFromType(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    switch (ᐧ) {
    case {} when (TypeFlags)(t.flags & TypeFlagsStringLiteral) != 0: {
        return t.AsLiteralType().value._<@string>();
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsNumberLiteral) != 0: {
        return t.AsLiteralType().value._<jsnum.Number>().String();
    }
    case {} when (TypeFlags)(t.flags & TypeFlagsUniqueESSymbol) != 0: {
        return t.AsUniqueESSymbolType().name;
    }}

    throw panic("Unhandled case in getPropertyNameFromType");
}

internal static bool isNumericLiteralName(@string name) {
    return jsnum.FromString(name).String() == name;
}

internal static bool isThisProperty(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.IsPropertyAccessExpression(Ꮡnode) || ast.IsElementAccessExpression(Ꮡnode)) && node.Expression().Kind == ast.KindThisKeyword;
}

internal static bool isValidNumberString(@string s, bool roundTripOnly) {
    if (s == ""u8) {
        return false;
    }
    var n = jsnum.FromString(s);
    return !n.IsNaN() && !n.IsInf() && (!roundTripOnly || n.String() == s);
}

internal static bool isValidBigIntString(@string s, bool roundTripOnly) {
    if (s == ""u8) {
        return false;
    }
    var scannerΔ1 = scanner.NewScanner();
    scannerΔ1.SetSkipTrivia(false);
    var success = true;
    scannerΔ1.SetOnError((ж<diagnostics.Message> diagnostic, nint start, nint length, params ꓸꓸꓸany argsʗp) => {
        var args = argsʗp.sslice();
        success = false;
    });
    scannerΔ1.SetText(s + "n"u8);
    var result = scannerΔ1.Scan();
    var negative = result == ast.KindMinusToken;
    if (negative) {
        result = scannerΔ1.Scan();
    }
    var flags = scannerΔ1.TokenFlags();
    return success && result == ast.KindBigIntLiteral && scannerΔ1.TokenEnd() == len(s) + 1 && (ast.TokenFlags)(flags & ast.TokenFlagsContainsSeparator) == 0 && (!roundTripOnly || s == pseudoBigIntToString(jsnum.NewPseudoBigInt(jsnum.ParsePseudoBigInt(scannerΔ1.TokenValue()), negative)));
}

internal static bool isValidESSymbolDeclaration(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsVariableDeclaration(Ꮡnode)) {
        return ast.IsVarConst(Ꮡnode) && ast.IsIdentifier(node.AsVariableDeclaration().Name()) && isVariableDeclarationInVariableStatement(ref (Ꮡnode).DerefOrNull());
    }
    if (ast.IsPropertyDeclaration(Ꮡnode)) {
        return hasReadonlyModifier(Ꮡnode) && ast.HasStaticModifier(Ꮡnode);
    }
    return ast.IsPropertySignatureDeclaration(Ꮡnode) && hasReadonlyModifier(Ꮡnode);
}

internal static bool isVariableDeclarationInVariableStatement(ref ast.Node node) {
    return ast.IsVariableDeclarationList(node.Parent) && ast.IsVariableStatement(node.Parent.Parent);
}

public static bool IsKnownSymbol(ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return isLateBoundName(symbol.Name);
}

public static bool IsPrivateIdentifierSymbol(ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (Ꮡsymbol == nil) {
        return false;
    }
    return strings.HasPrefix(symbol.Name, ast.InternalSymbolNamePrefix + "#");
}

internal static bool isLateBoundName(@string name) {
    return len(name) >= 2 && name[0] == '\xfe' && name[1] == '@';
}

internal static bool isObjectOrArrayLiteralType(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (ObjectFlags)(t.objectFlags & ((ObjectFlags)(ObjectFlagsObjectLiteral | ObjectFlagsArrayLiteral))) != 0;
}

internal static ж<astꓸClassLikeDeclaration> getContainingClassExcludingClassDecorators(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decorator = ast.FindAncestorOrQuit(node.Parent, (ж<ast.Node> n) => {
        if (ast.IsClassLike(n)) {
            return ast.FindAncestorQuit;
        }
        if (ast.IsDecorator(n)) {
            return ast.FindAncestorTrue;
        }
        return ast.FindAncestorFalse;
    });
    if (decorator != nil && ast.IsClassLike(decorator.Parent)) {
        return ast.GetContainingClass(decorator.Parent);
    }
    if (decorator != nil) {
        return ast.GetContainingClass(decorator);
    }
    return ast.GetContainingClass(Ꮡnode);
}

internal static bool isThisTypeParameter(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0 && t.AsTypeParameter().isThisType;
}

internal static bool isClassInstanceProperty(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsInJSFile(Ꮡnode) && ast.IsExpandoPropertyDeclaration(Ꮡnode)) {
        var left = node.AsBinaryExpression().Left;
        return (!ast.IsBindableStaticAccessExpression(left, false) || !ast.IsPrototypeAccess(left.Expression())) && !ast.IsBindableStaticNameExpression(left, true);
    }
    return node.Parent != nil && ast.IsClassLike(node.Parent) && ast.IsPropertyDeclaration(Ꮡnode) && !ast.HasAccessorModifier(Ꮡnode);
}

internal static bool isThisInitializedObjectBindingExpression(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && (ast.IsShorthandPropertyAssignment(Ꮡnode) || ast.IsPropertyAssignment(Ꮡnode)) && ast.IsBinaryExpression(node.Parent.Parent) && node.Parent.Parent.AsBinaryExpression().OperatorToken.Kind == ast.KindEqualsToken && node.Parent.Parent.AsBinaryExpression().Right.Kind == ast.KindThisKeyword;
}

internal static bool isThisInitializedDeclaration(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && ast.IsVariableDeclaration(Ꮡnode) && node.Initializer() != nil && node.Initializer().Kind == ast.KindThisKeyword;
}

internal static bool isInfinityOrNaNString(@string name) {
    return name == "Infinity"u8 || name == "-Infinity"u8 || name == "NaN"u8;
}

} partial class checkerᴛChecker {

internal static bool isConstantVariable(this ref Checker c, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return (ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsVariable) != 0 && ((ast.NodeFlags)(c.getDeclarationNodeFlagsFromSymbol(Ꮡsymbol) & ast.NodeFlagsConstant)) != 0;
}

internal static bool isParameterOrMutableLocalVariable(this ref Checker c, ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (symbol.ValueDeclaration != nil) {
        var declaration = ast.GetRootDeclaration(symbol.ValueDeclaration);
        return declaration != nil && (ast.IsParameterDeclaration(declaration) || ast.IsVariableDeclaration(declaration) && (ast.IsCatchClause(declaration.Parent) || c.isMutableLocalVariableDeclaration(declaration)));
    }
    return false;
}

internal static bool isMutableLocalVariableDeclaration(this ref Checker c, ж<ast.Node> Ꮡdeclaration) {
    ref var declaration = ref Ꮡdeclaration.DerefOrNull();

    return (ast.NodeFlags)(declaration.Parent.Flags & ast.NodeFlagsLet) != 0 && !((ast.ModifierFlags)(ast.GetCombinedModifierFlags(Ꮡdeclaration) & ast.ModifierFlagsExport) != 0 || declaration.Parent.Parent.Kind == ast.KindVariableStatement && ast.IsGlobalSourceFile(declaration.Parent.Parent.Parent));
}

} partial class checker_package {

internal static bool isInAmbientOrTypeNode(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0 || ast.FindAncestor(Ꮡnode, (ж<ast.Node> n) => ast.IsInterfaceDeclaration(n) || ast.IsTypeOrJSTypeAliasDeclaration(n) || ast.IsTypeLiteralNode(n)) != nil;
}

internal static bool isLiteralExpressionOfObject(ref ast.Node node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression || ᴛ == ast.KindArrayLiteralExpression || ᴛ == ast.KindRegularExpressionLiteral || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindClassExpression: {
        return true;
    }}

    return false;
}

internal static bool canHaveFlowNode(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.FlowNodeData() != nil;
}

internal static bool isNonNullAccess(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsAccessExpression(Ꮡnode) && ast.IsNonNullExpression(node.Expression());
}

internal static ж<ast.Node> getBindingElementPropertyName(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.PropertyNameOrName();
}

internal static bool isCallChain(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return ast.IsCallExpression(Ꮡnode) && (ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0;
}

} partial class checkerᴛChecker {

internal static bool callLikeExpressionMayHaveTypeArguments(this ref Checker c, ж<ast.Node> node) {
    return ast.IsCallOrNewExpression(node) || ast.IsTaggedTemplateExpression(node) || ast.IsJsxOpeningLikeElement(node);
}

} partial class checker_package {

internal static bool isSuperCall(ж<ast.Node> Ꮡn) {
    ref var n = ref Ꮡn.DerefOrNull();

    return ast.IsCallExpression(Ꮡn) && n.Expression().Kind == ast.KindSuperKeyword;
}

internal static slice<ж<ast.Node>> getMembersOfDeclaration(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindClassExpression || ᴛ == ast.KindTypeLiteral: {
        return node.Members();
    }
    case var ᴛ when ᴛ == ast.KindObjectLiteralExpression: {
        return node.Properties();
    }}

    return default;
}

internal static bool isInRightSideOfImportOrExportAssignment(ж<astꓸEntityName> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (node.Parent.Kind == ast.KindQualifiedName) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }

    return node.Parent.Kind == ast.KindImportEqualsDeclaration && node.Parent.AsImportEqualsDeclaration().ModuleReference == Ꮡnode || node.Parent.Kind == ast.KindExportAssignment && node.Parent.Expression() == Ꮡnode;
}

internal static bool isJsxIntrinsicTagName(ж<ast.Node> ᏑtagName) {
    ref var tagName = ref ᏑtagName.DerefOrNull();

    return ast.IsIdentifier(ᏑtagName) && scanner.IsIntrinsicJsxName(tagName.Text()) || ast.IsJsxNamespacedName(ᏑtagName);
}

internal static ж<ast.Node> getContainingObjectLiteral(ref astꓸSignatureDeclaration f) {
    if ((f.Kind == ast.KindMethodDeclaration || f.Kind == ast.KindGetAccessor || f.Kind == ast.KindSetAccessor) && f.Parent.Kind == ast.KindObjectLiteralExpression) {
        return f.Parent;
    } else if (f.Kind == ast.KindFunctionExpression && f.Parent.Kind == ast.KindPropertyAssignment) {
        return f.Parent.Parent;
    }
    return default;
}

internal static ж<ast.Node> isImportTypeQualifierPart(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    while (ast.IsQualifiedName(parent)) {
        Ꮡnode = parent; node = ref Ꮡnode.DerefOrNull();
        parent = parent.Parent;
    }

    if (parent != nil && parent.Kind == ast.KindImportType && parent.AsImportTypeNode().Qualifier == Ꮡnode) {
        return parent;
    }

    return default;
}

internal static bool isInNameOfExpressionWithTypeArgumentsOrHeritageTypeReference(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    while (node.Parent.Kind == ast.KindPropertyAccessExpression || node.Parent.Kind == ast.KindQualifiedName) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
    }

    return node.Parent.Kind == ast.KindExpressionWithTypeArguments || ast.IsNameOfHeritageClauseTypeReference(Ꮡnode);
}

internal static ж<ast.Symbol> getIndexSymbolFromSymbolTable(ast.SymbolTable symbolTable) {
    return symbolTable[ast.InternalSymbolNameIndex];
}

internal static bool expressionResultIsUnused(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (;;) {
        var parent = node.Parent;
        if (ast.IsParenthesizedExpression(parent)) {
            Ꮡnode = parent; node = ref Ꮡnode.DerefOrNull();
            continue;
        }
        if (ast.IsExpressionStatement(parent) || ast.IsVoidExpression(parent) || ast.IsForStatement(parent) && (parent.Initializer() == Ꮡnode || parent.AsForStatement().Incrementor == Ꮡnode)) {
            return true;
        }
        if (ast.IsBinaryExpression(parent) && parent.AsBinaryExpression().OperatorToken.Kind == ast.KindCommaToken) {
            if (Ꮡnode == parent.AsBinaryExpression().Left) {
                return true;
            }
            Ꮡnode = parent; node = ref Ꮡnode.DerefOrNull();
            continue;
        }
        return false;
    }
}

internal static @string pseudoBigIntToString(jsnum.PseudoBigInt value) {
    return value.String();
}

internal static ж<ast.Node> getSuperContainer(ж<ast.Node> Ꮡnode, bool stopOnFunctions) {
    ref var node = ref Ꮡnode.DerefOrNull();

    for (;;) {
        Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
        if (Ꮡnode == nil) {
            return default;
        }
        var exprᴛ1 = node.Kind;
        var matchᴛ1 = false;
        if (exprᴛ1 == ast.KindComputedPropertyName) { matchᴛ1 = true;
            Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
        }
        else if (exprᴛ1 == ast.KindFunctionDeclaration || exprᴛ1 == ast.KindFunctionExpression || exprᴛ1 == ast.KindArrowFunction) { matchᴛ1 = true;
            if (!stopOnFunctions) {
                continue;
            }
            fallthrough = true;
        }
        if (fallthrough || !matchᴛ1 && (exprᴛ1 == ast.KindPropertyDeclaration || exprᴛ1 == ast.KindPropertySignature || exprᴛ1 == ast.KindMethodDeclaration || exprᴛ1 == ast.KindMethodSignature || exprᴛ1 == ast.KindConstructor || exprᴛ1 == ast.KindGetAccessor || exprᴛ1 == ast.KindSetAccessor || exprᴛ1 == ast.KindClassStaticBlockDeclaration)) {
            return Ꮡnode;
        }
        else if (exprᴛ1 == ast.KindDecorator) { matchᴛ1 = true;
            if (ast.IsParameterDeclaration(node.Parent) && ast.IsClassElement(node.Parent.Parent)) {
                Ꮡnode = node.Parent.Parent; node = ref Ꮡnode.DerefOrNull();
            } else if (ast.IsClassElement(node.Parent)) {
                Ꮡnode = node.Parent; node = ref Ꮡnode.DerefOrNull();
            }
        }

    }
}

internal static bool forEachYieldExpression(ж<ast.Node> body, Func<ж<ast.Node>, bool> visitor) {
    ref var traverse = ref heap<Func<ж<ast.Node>, bool>>(out var Ꮡtraverse);
    traverse = (ж<ast.Node> node) => {
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindYieldExpression: {
            if (visitor(node)) {
                return true;
            }
            var operand = node.Expression();
            if (operand == nil) {
                return false;
            }
            return Ꮡtraverse.ValueSlot(operand);
        }
        case var ᴛ when ᴛ == ast.KindEnumDeclaration || ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindTypeAliasDeclaration: {
            break;
        }
        default: {
            if (ast.IsFunctionLike(node)) {
                if (node.Name() != nil && ast.IsComputedPropertyName(node.Name())) {
                    return Ꮡtraverse.ValueSlot(node.Name().Expression());
                }
            } else if (!ast.IsPartOfTypeNode(node)) {
                return node.ForEachChild(Ꮡtraverse.ValueSlot);
            }
            break;
        }}

        return false;
    };
    return traverse(body);
}

internal static ж<ast.Node> getEnclosingContainer(ref ast.Node node) {
    return ast.FindAncestor(node.Parent, (ж<ast.Node> n) => (binder.ContainerFlags)(binder.GetContainerFlags(n) & binder.ContainerFlagsIsContainer) != 0);
}

internal static slice<ж<ast.Node>> getDeclarationsOfKind(ref ast.Symbol symbol, ast.Kind kind) {
    return core.Filter(symbol.Declarations, (ж<ast.Node> d) => d.Kind == kind);
}

internal static bool hasType(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Type() != nil;
}

internal static nint getNonRestParameterCount(ж<Signature> Ꮡsig) {
    ref var sig = ref Ꮡsig.DerefOrNull();

    return len(sig.parameters) - core.IfElse<nint>(signatureHasRestParameter(ref (Ꮡsig).DerefOrNull()), 1, 0);
}

internal static (nint, nint) minAndMax<T>(slice<T> Δslice, Func<T, nint> getValue) {
    nint minValue = default;
    nint maxValue = default;
    foreach (var (i, element) in range(Δslice)) {
        nint value = getValue(element);
        if (i == 0) {
            minValue = value;
            maxValue = value;
        } else {
            minValue = min(minValue, value);
            maxValue = max(maxValue, value);
        }
    }
    return (minValue, maxValue);
}

partial struct FeatureMapEntry {
    internal @string lib;
    internal slice<@string> props;
}

internal static Func<map<@string, slice<FeatureMapEntry>>> getFeatureMap = sync.OnceValue(map<@string, slice<FeatureMapEntry>> () => new map<@string, slice<FeatureMapEntry>>{
        ["Array"] = [
            new(lib: "es2015", props: ["find", "findIndex", "fill", "copyWithin", "entries", "keys", "values"]),
            new(lib: "es2016", props: ["includes"]),
            new(lib: "es2019", props: ["flat", "flatMap"]),
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Iterator"] = [
            new(lib: "es2015", props: [])],
        ["AsyncIterator"] = [
            new(lib: "es2015", props: [])],
        ["ArrayBuffer"] = [
            new(lib: "es2024", props: [
                "maxByteLength",
                "resizable",
                "resize",
                "detached",
                "transfer",
                "transferToFixedLength"
            ])],
        ["Atomics"] = [
            new(lib: "es2017", props: [
                "add",
                "and",
                "compareExchange",
                "exchange",
                "isLockFree",
                "load",
                "or",
                "store",
                "sub",
                "wait",
                "notify",
                "xor"
            ]),
            new(lib: "es2024", props: [
                "waitAsync"
            ])],
        ["SharedArrayBuffer"] = [
            new(lib: "es2017", props: [
                "byteLength",
                "slice"
            ]),
            new(lib: "es2024", props: [
                "growable",
                "maxByteLength",
                "grow"
            ])],
        ["AsyncIterable"] = [
            new(lib: "es2018", props: [])],
        ["AsyncIterableIterator"] = [
            new(lib: "es2018", props: [])],
        ["AsyncGenerator"] = [
            new(lib: "es2018", props: [])],
        ["AsyncGeneratorFunction"] = [
            new(lib: "es2018", props: [])],
        ["RegExp"] = [
            new(lib: "es2015", props: ["flags", "sticky", "unicode"]),
            new(lib: "es2018", props: ["dotAll"]),
            new(lib: "es2024", props: ["unicodeSets"])],
        ["RegExpConstructor"] = [
            new(lib: "es2025", props: ["escape"])],
        ["Reflect"] = [
            new(lib: "es2015", props: ["apply", "construct", "defineProperty", "deleteProperty", "get", "getOwnPropertyDescriptor", "getPrototypeOf", "has", "isExtensible", "ownKeys", "preventExtensions", "set", "setPrototypeOf"])],
        ["ArrayConstructor"] = [
            new(lib: "es2015", props: ["from", "of"]),
            new(lib: "esnext", props: ["fromAsync"])],
        ["ObjectConstructor"] = [
            new(lib: "es2015", props: ["assign", "getOwnPropertySymbols", "keys", "is", "setPrototypeOf"]),
            new(lib: "es2017", props: ["values", "entries", "getOwnPropertyDescriptors"]),
            new(lib: "es2019", props: ["fromEntries"]),
            new(lib: "es2022", props: ["hasOwn"]),
            new(lib: "es2024", props: ["groupBy"])],
        ["NumberConstructor"] = [
            new(lib: "es2015", props: ["isFinite", "isInteger", "isNaN", "isSafeInteger", "parseFloat", "parseInt"])],
        ["Math"] = [
            new(lib: "es2015", props: ["clz32", "imul", "sign", "log10", "log2", "log1p", "expm1", "cosh", "sinh", "tanh", "acosh", "asinh", "atanh", "hypot", "trunc", "fround", "cbrt"]),
            new(lib: "es2025", props: ["f16round"])],
        ["Map"] = [
            new(lib: "es2015", props: ["entries", "keys", "values"]),
            new(lib: "esnext", props: [
                "getOrInsert",
                "getOrInsertComputed"
            ])],
        ["MapConstructor"] = [
            new(lib: "es2024", props: ["groupBy"])],
        ["Set"] = [
            new(lib: "es2015", props: ["entries", "keys", "values"]),
            new(lib: "es2025", props: [
                "union",
                "intersection",
                "difference",
                "symmetricDifference",
                "isSubsetOf",
                "isSupersetOf",
                "isDisjointFrom"
            ])],
        ["PromiseConstructor"] = [
            new(lib: "es2015", props: ["all", "race", "reject", "resolve"]),
            new(lib: "es2020", props: ["allSettled"]),
            new(lib: "es2021", props: ["any"]),
            new(lib: "es2024", props: ["withResolvers"]),
            new(lib: "es2025", props: ["try"])],
        ["Symbol"] = [
            new(lib: "es2015", props: ["for", "keyFor"]),
            new(lib: "es2019", props: ["description"])],
        ["WeakMap"] = [
            new(lib: "es2015", props: []),
            new(lib: "esnext", props: [
                "getOrInsert",
                "getOrInsertComputed"
            ])],
        ["WeakSet"] = [
            new(lib: "es2015", props: [])],
        ["String"] = [
            new(lib: "es2015", props: ["codePointAt", "includes", "endsWith", "normalize", "repeat", "startsWith", "anchor", "big", "blink", "bold", "fixed", "fontcolor", "fontsize", "italics", "link", "small", "strike", "sub", "sup"]),
            new(lib: "es2017", props: ["padStart", "padEnd"]),
            new(lib: "es2019", props: ["trimStart", "trimEnd", "trimLeft", "trimRight"]),
            new(lib: "es2020", props: ["matchAll"]),
            new(lib: "es2021", props: ["replaceAll"]),
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2024", props: ["isWellFormed", "toWellFormed"])],
        ["StringConstructor"] = [
            new(lib: "es2015", props: ["fromCodePoint", "raw"])],
        ["DateTimeFormat"] = [
            new(lib: "es2017", props: ["formatToParts"])],
        ["Promise"] = [
            new(lib: "es2015", props: []),
            new(lib: "es2018", props: ["finally"])],
        ["RegExpMatchArray"] = [
            new(lib: "es2018", props: ["groups"])],
        ["RegExpExecArray"] = [
            new(lib: "es2018", props: ["groups"])],
        ["Intl"] = [
            new(lib: "es2018", props: ["PluralRules"]),
            new(lib: "es2020", props: ["RelativeTimeFormat", "Locale", "DisplayNames"]),
            new(lib: "es2021", props: ["ListFormat", "DateTimeFormat"]),
            new(lib: "es2022", props: ["Segmenter"]),
            new(lib: "es2025", props: ["DurationFormat"])],
        ["NumberFormat"] = [
            new(lib: "es2018", props: ["formatToParts"])],
        ["SymbolConstructor"] = [
            new(lib: "es2020", props: ["matchAll"]),
            new(lib: "esnext", props: [
                "metadata",
                "dispose",
                "asyncDispose"
            ])],
        ["DataView"] = [
            new(lib: "es2020", props: ["setBigInt64", "setBigUint64", "getBigInt64", "getBigUint64"]),
            new(lib: "es2025", props: ["setFloat16", "getFloat16"])],
        ["BigInt"] = [
            new(lib: "es2020", props: [])],
        ["RelativeTimeFormat"] = [
            new(lib: "es2020", props: ["format", "formatToParts", "resolvedOptions"])],
        ["Int8Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Uint8Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Uint8ClampedArray"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Int16Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Uint16Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Int32Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Uint32Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Float16Array"] = [
            new(lib: "es2025", props: [])],
        ["Float32Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Float64Array"] = [
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["BigInt64Array"] = [
            new(lib: "es2020", props: []),
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["BigUint64Array"] = [
            new(lib: "es2020", props: []),
            new(lib: "es2022", props: ["at"]),
            new(lib: "es2023", props: ["findLastIndex", "findLast", "toReversed", "toSorted", "toSpliced", "with"])],
        ["Error"] = [
            new(lib: "es2022", props: ["cause"])],
        ["ErrorConstructor"] = [
            new(lib: "esnext", props: ["isError"])],
        ["Uint8ArrayConstructor"] = [
            new(lib: "esnext", props: ["fromBase64", "fromHex"])],
        ["DisposableStack"] = [
            new(lib: "esnext", props: [])],
        ["AsyncDisposableStack"] = [
            new(lib: "esnext", props: [])],
        ["Date"] = [
            new(lib: "esnext", props: ["toTemporalInstant"])]
    });

internal static core.TextRange rangeOfTypeParameters(ж<ast.SourceFile> ᏑsourceFile, ж<ast.NodeList> ᏑtypeParameters) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var typeParameters = ref ᏑtypeParameters.DerefOrNull();

    return core.NewTextRange(typeParameters.Pos() - 1, min(len(sourceFile.Text()), scanner.SkipTrivia(sourceFile.Text(), typeParameters.End()) + 1));
}

internal static @string tryGetPropertyAccessOrIdentifierToString(ж<ast.Node> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    switch (ᐧ) {
    case {} when ast.IsPropertyAccessExpression(Ꮡexpr): {
        @string baseStr = tryGetPropertyAccessOrIdentifierToString(expr.Expression());
        if (baseStr != ""u8) {
            return baseStr + "."u8 + entityNameToString(expr.Name());
        }
        break;
    }
    case {} when ast.IsElementAccessExpression(Ꮡexpr): {
        @string baseStr = tryGetPropertyAccessOrIdentifierToString(expr.Expression());
        if (baseStr != ""u8 && ast.IsPropertyName(expr.AsElementAccessExpression().ArgumentExpression)) {
            return baseStr + "."u8 + ast.GetPropertyNameForPropertyNameNode(expr.AsElementAccessExpression().ArgumentExpression);
        }
        break;
    }
    case {} when ast.IsIdentifier(Ꮡexpr): {
        return expr.Text();
    }
    case {} when ast.IsJsxNamespacedName(Ꮡexpr): {
        return entityNameToString(Ꮡexpr);
    }}

    return "";
}

internal static bool allDeclarationsInSameSourceFile(ref ast.Symbol symbol) {
    if (len(symbol.Declarations) > 1) {
        ж<ast.SourceFile> sourceFile = default;
        foreach (var (i, d) in range(symbol.Declarations)) {
            if (i == 0) {
                sourceFile = ast.GetSourceFileOfNode(d);
            } else if (ast.GetSourceFileOfNode(d) != sourceFile) {
                return false;
            }
        }
    }
    return true;
}

internal static bool containsNonMissingUndefinedType(ref Checker c, ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    ж<Type> candidate = default;
    if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
        candidate = t.AsUnionType().types[0];
    } else {
        candidate = Ꮡt;
    }
    return (TypeFlags)(candidate.flags & TypeFlagsUndefined) != 0 && candidate != c.missingType;
}

internal static ж<ast.Node> getAnyImportSyntax(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<ast.Node> importNode = default;
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
        importNode = Ꮡnode;
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportClause: {
        importNode = node.Parent;
        break;
    }
    case var ᴛ when ᴛ == ast.KindNamespaceImport: {
        importNode = node.Parent.Parent;
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportSpecifier: {
        importNode = node.Parent.Parent.Parent;
        break;
    }
    default: {
        return default;
    }}

    return importNode;
}

internal static bool isReservedMemberName(@string name) {
    return len(name) >= 2 && name[0] == '\xFE' && name[1] != '@' && name[1] != '#';
}

internal static bool introducesArgumentsExoticObject(ref ast.Node node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature || ᴛ == ast.KindConstructor || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression: {
        return true;
    }}

    return false;
}

internal static slice<ж<ast.Symbol>> symbolsToArray(ast.SymbolTable symbols) {
    slice<ж<ast.Symbol>> result = default;
    foreach (var (id, symbol) in symbols) {
        if (!isReservedMemberName(id)) {
            result = append(result, symbol);
        }
    }
    return result;
}

public static ж<ast.Symbol> SkipAlias(ж<ast.Symbol> Ꮡsymbol, ж<Checker> checker) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsAlias) != 0) {
        return checker.GetAliasedSymbol(Ꮡsymbol);
    }
    return Ꮡsymbol;
}

public static bool IsExternalModuleSymbol(ж<ast.Symbol> ᏑmoduleSymbol) {
    ref var moduleSymbol = ref ᏑmoduleSymbol.DerefOrNull();

    return moduleSymbol.IsExternalModule();
}

} partial class checkerᴛChecker {

internal static bool isCanceled(this ref Checker c) {
    return c.ctx != nil && c.ctx.Err() != nil;
}

internal static void checkNotCanceled(this ref Checker c) {
    if (c.wasCanceled) {
        throw panic("Checker was previously cancelled");
    }
}

internal static map<@string, bool> getPackagesMap(this ref Checker c) {
    if (c.packagesMap == nil) {
        c.packagesMap = new map<@string, bool>();
        var resolvedModules = c.program.GetResolvedModules();
        foreach (var (_, resolvedModulesInFile) in resolvedModules) {
            foreach (var (_, module) in resolvedModulesInFile) {
                if (module.PackageId.Name != ""u8) {
                    c.packagesMap[module.PackageId.Name] = c.packagesMap[module.PackageId.Name] || module.Extension == tspath.ExtensionDts;
                }
            }
        }
    }
    return c.packagesMap;
}

internal static bool typesPackageExists(this ref Checker c, @string packageName) {
    var packagesMap = c.getPackagesMap();
    var (_, ok) = packagesMap[module.GetTypesPackageName(packageName), ꟷ];
    return ok;
}

internal static bool packageBundlesTypes(this ref Checker c, @string packageName) {
    var packagesMap = c.getPackagesMap();
    var (hasTypes, _) = packagesMap[packageName, ꟷ];
    return hasTypes;
}

} partial class checker_package {

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string trueˢ = "true"u8;
internal static readonly @string falseˢ = "false"u8;

public static @string ValueToString(any value) {
    switch (value.type()) {
    case @string valueΔ1: {
        return "\""u8 + printer.EscapeString(valueΔ1, (rune)'"') + "\""u8;
    }
    case jsnum.Number valueΔ1: {
        return valueΔ1.String();
    }
    case bool valueΔ1: {
        return core.IfElse(valueΔ1, trueˢ, falseˢ);
    }
    case jsnum.PseudoBigInt valueΔ1: {
        return valueΔ1.String() + "n"u8;
    }}
    throw panic("unhandled value type in valueToString");
}

internal static bool nodeStartsNewLexicalEnvironment(ref ast.Node node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindConstructor || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindSourceFile: {
        return true;
    }}

    return false;
}

} partial class checkerᴛChecker {

internal static bool isUncheckedJSSuggestion(this ref Checker c, ж<ast.Node> Ꮡnode, ж<ast.Symbol> Ꮡsuggestion, bool excludeClasses) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var suggestion = ref Ꮡsuggestion.DerefOrNull();

    var @file = ast.GetSourceFileOfNode(Ꮡnode);
    if (@file != nil) {
        if (c.compilerOptions.CheckJs.IsUnknown() && @file.CheckJsDirective == nil && (@file.ScriptKind == core.ScriptKindJS || @file.ScriptKind == core.ScriptKindJSX)) {
            ж<ast.SourceFile> declarationFile = default;
            if (Ꮡsuggestion != nil) {
                var firstDeclaration = core.FirstOrNil(suggestion.Declarations); if (firstDeclaration != nil) {
                    declarationFile = ast.GetSourceFileOfNode(firstDeclaration);
                }
            }
            var suggestionHasNoExtendsOrDecorators = Ꮡsuggestion == nil || suggestion.ValueDeclaration == nil || !ast.IsClassLike(suggestion.ValueDeclaration) || len(ast.GetExtendsHeritageClauseElements(suggestion.ValueDeclaration)) != 0 || ast.ClassOrConstructorParameterIsDecorated(false, suggestion.ValueDeclaration);
            return !(@file != declarationFile && declarationFile != nil && ast.IsGlobalSourceFile(declarationFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) && !(excludeClasses && Ꮡsuggestion != nil && (ast.SymbolFlags)(suggestion.Flags & ast.SymbolFlagsClass) != 0 && suggestionHasNoExtendsOrDecorators) && !(Ꮡnode != nil && excludeClasses && ast.IsPropertyAccessExpression(Ꮡnode) && node.Expression().Kind == ast.KindThisKeyword && suggestionHasNoExtendsOrDecorators);
        }
    }
    return false;
}

internal static bool isJSLiteralType(this ж<Checker> Ꮡc, ж<Type> Ꮡt) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var t = ref Ꮡt.DerefOrNull();

    if (c.noImplicitAny) {
        return false;
    }
    if ((ObjectFlags)(t.objectFlags & ObjectFlagsJSLiteral) != 0) {
        return true;
    }
    if ((TypeFlags)(t.flags & TypeFlagsUnion) != 0) {
        return core.Every<ж<Type>>(t.AsUnionType().types, Ꮡc.isJSLiteralType);
    }
    if ((TypeFlags)(t.flags & TypeFlagsIntersection) != 0) {
        return core.Some<ж<Type>>(t.AsIntersectionType().types, Ꮡc.isJSLiteralType);
    }
    if ((TypeFlags)(t.flags & TypeFlagsInstantiable) != 0) {
        var constraint = Ꮡc.getResolvedBaseConstraint(Ꮡt, default);
        return constraint != Ꮡt && Ꮡc.isJSLiteralType(constraint);
    }
    return false;
}

} partial class checker_package {

partial struct DiagnosticDetails {
    public ж<diagnostics.Message> Message;
    public slice<any> Args;
}

public static DiagnosticDetails CreateModuleNotFoundChain(Program program, ж<ast.SourceFile> @file, @string moduleReference, coreꓸResolutionMode mode, @string packageName) {
    var resolvedModule = program.GetResolvedModule(new ast.SourceFileжHasFileName(@file), moduleReference, mode);

    if (resolvedModule != nil && resolvedModule.AlternateResult != ""u8) {
        if (strings.Contains(resolvedModule.AlternateResult, "/node_modules/@types/")) {
            packageName = "@types/"u8 + module.MangleScopedPackageName(packageName);
        }
        return new DiagnosticDetails(
            Message: diagnostics.There_are_types_at_0_but_this_result_could_not_be_resolved_when_respecting_package_json_exports_The_1_library_may_need_to_update_its_package_json_or_typings,
            Args: [resolvedModule.AlternateResult, packageName]
        );
    }

    var packagesMap = program.GetPackagesMap();
    var (_, ok) = packagesMap[module.GetTypesPackageName(packageName), ꟷ]; if (ok) {
        return new DiagnosticDetails(
            Message: diagnostics.If_the_0_package_actually_exposes_this_module_consider_sending_a_pull_request_to_amend_https_Colon_Slash_Slashgithub_com_SlashDefinitelyTyped_SlashDefinitelyTyped_Slashtree_Slashmaster_Slashtypes_Slash_1,
            Args: slice<any>([packageName, module.MangleScopedPackageName(packageName)])
        );
    }
    if (packagesMap[packageName]) {
        return new DiagnosticDetails(
            Message: diagnostics.If_the_0_package_actually_exposes_this_module_try_adding_a_new_declaration_d_ts_file_containing_declare_module_1,
            Args: [packageName, moduleReference]
        );
    }
    return new DiagnosticDetails(
        Message: diagnostics.Try_npm_i_save_dev_types_Slash_1_if_it_exists_or_add_a_new_declaration_d_ts_file_containing_declare_module_0,
        Args: slice<any>([moduleReference, module.MangleScopedPackageName(packageName)])
    );
}

public static DiagnosticDetails CreateModeMismatchDetails(Program program, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    @string ext = tspath.TryGetExtensionFromPath(@file.FileName());
    @string targetExt = core.IfElse(ext == tspath.ExtensionTs, tspath.ExtensionMts, core.IfElse(ext == tspath.ExtensionJs, tspath.ExtensionMjs, (@string)""));
    var meta = program.GetSourceFileMetaData(@file.Path());
    @string packageJsonType = meta.PackageJsonType;
    @string packageJsonDirectory = meta.PackageJsonDirectory;

    if (packageJsonDirectory != ""u8 && packageJsonType == ""u8) {
        if (targetExt != ""u8) {
            return new DiagnosticDetails(
                Message: diagnostics.To_convert_this_file_to_an_ECMAScript_module_change_its_file_extension_to_0_or_add_the_field_type_Colon_module_to_1,
                Args: slice<any>([targetExt, tspath.CombinePaths(packageJsonDirectory, "package.json")])
            );
        }
        return new DiagnosticDetails(
            Message: diagnostics.To_convert_this_file_to_an_ECMAScript_module_add_the_field_type_Colon_module_to_0,
            Args: slice<any>([tspath.CombinePaths(packageJsonDirectory, "package.json")])
        );
    }
    if (targetExt != ""u8) {
        return new DiagnosticDetails(
            Message: diagnostics.To_convert_this_file_to_an_ECMAScript_module_change_its_file_extension_to_0_or_create_a_local_package_json_file_with_type_Colon_module,
            Args: [targetExt]
        );
    }
    return new DiagnosticDetails(
        Message: diagnostics.To_convert_this_file_to_an_ECMAScript_module_create_a_local_package_json_file_with_type_Colon_module,
        Args: default
    );
}

internal static ж<ast.Node> walkUpOuterExpressions(ref ast.Node node) {
    var parent = node.Parent;
    while (parent != nil && ast.IsOuterExpression(parent, ast.OEKAll)) {
        parent = parent.Parent;
    }
    return parent;
}

public static ж<ast.Node> GetSetAccessorValueParameter(ж<ast.Node> Ꮡaccessor) {
    ref var accessor = ref Ꮡaccessor.DerefOrNull();

    var parameters = accessor.Parameters();
    if (len(parameters) > 0) {
        var hasThis = len(parameters) == 2 && ast.IsThisParameter(parameters[0]);
        return parameters[core.IfElse<nint>(hasThis, 1, 0)];
    }
    return default;
}

} // end checker_package
