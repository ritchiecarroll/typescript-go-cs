namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸany = Span<any>;
using static checker_package;

partial class checker_package {

} partial class checkerᴛChecker {

internal static bool grammarErrorOnFirstToken(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var sourceFile = ast.GetSourceFileOfNode(Ꮡnode);
    if (!c.hasParseDiagnostics(sourceFile)) {
        var span = scanner.GetRangeOfTokenAtPosition(sourceFile, node.Pos());
        Ꮡc.addDiagnostic(ast.NewDiagnostic(sourceFile, span, message, args.ꓸꓸꓸ));
        return true;
    }
    return false;
}

internal static bool grammarErrorAtPos(this ж<Checker> Ꮡc, ж<ast.Node> nodeForSourceFile, nint start, nint length, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var c = ref Ꮡc.DerefOrNull();

    var sourceFile = ast.GetSourceFileOfNode(nodeForSourceFile);
    if (!c.hasParseDiagnostics(sourceFile)) {
        Ꮡc.addDiagnostic(ast.NewDiagnostic(sourceFile, core.NewTextRange(start, start + length), message, args.ꓸꓸꓸ));
        return true;
    }
    return false;
}

internal static bool grammarErrorOnNode(this ж<Checker> Ꮡc, ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var c = ref Ꮡc.DerefOrNull();

    var sourceFile = ast.GetSourceFileOfNode(node);
    if (!c.hasParseDiagnostics(sourceFile)) {
        Ꮡc.error(node, message, args.ꓸꓸꓸ);
        return true;
    }
    return false;
}

internal static bool grammarErrorOnNodeSkippedOnNoEmit(this ж<Checker> Ꮡc, ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var c = ref Ꮡc.DerefOrNull();

    var sourceFile = ast.GetSourceFileOfNode(node);
    if (!c.hasParseDiagnostics(sourceFile)) {
        var d = NewDiagnosticForNode(node, message, args.ꓸꓸꓸ);
        d.SetSkippedOnNoEmit();
        Ꮡc.addDiagnostic(d);
        return true;
    }
    return false;
}

} partial class checker_package {

internal static ж<ast.Node> getIdentifierFromEntityNameExpression(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindIdentifier: {
        return Ꮡnode;
    }
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression: {
        return node.AsPropertyAccessExpression().Name();
    }
    default: {
        return default;
    }}

}

} partial class checkerᴛChecker {

internal static bool checkGrammarRegularExpressionLiteral(this ж<Checker> Ꮡc, ж<ast.RegularExpressionLiteral> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    var sourceFile = ast.GetSourceFileOfNode(node.of(ast.RegularExpressionLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (!c.hasParseDiagnostics(sourceFile)) {
        ref var lastError = ref heap<ж<ast.Diagnostic>>(out var ᏑlastError);
        if (c.regExpScanner == nil) {
            c.regExpScanner = scanner.NewScanner();
        }
        c.regExpScanner.SetScriptTarget(c.languageVersion);
        c.regExpScanner.SetLanguageVariant(sourceFile.LanguageVariant);
        var sourceFileʗ1 = sourceFile;
        c.regExpScanner.SetOnError((ж<diagnostics.Message> message, nint start, nint length, params ꓸꓸꓸany argsʗp) => {
            var args = argsʗp.sslice();
            if (message.Category() == diagnostics.CategoryMessage && ᏑlastError.ValueSlot != nil && start == ᏑlastError.ValueSlot.Pos() && length == ᏑlastError.ValueSlot.Len()) {
                var err = ast.NewDiagnostic(nil, core.NewTextRange(start, start + length), message, args.ꓸꓸꓸ);
                ᏑlastError.ValueSlot.AddRelatedInfo(err);
            } else if (ᏑlastError.ValueSlot == nil || start != ᏑlastError.ValueSlot.Pos()) {
                ᏑlastError.ValueSlot = ast.NewDiagnostic(sourceFileʗ1, core.NewTextRange(start, start + length), message, args.ꓸꓸꓸ);
                ᏑlastError.ValueSlot = Ꮡc.addDiagnostic(ᏑlastError.ValueSlot);
            }
        });
        c.regExpScanner.SetText(sourceFile.Text());
        c.regExpScanner.ResetTokenState(node.of(ast.RegularExpressionLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode().Pos());
        c.regExpScanner.Scan();
        var tokenIsRegularExpressionLiteral = c.regExpScanner.ReScanSlashToken(true) == ast.KindRegularExpressionLiteral;
        c.regExpScanner.SetText("");
        c.regExpScanner.SetOnError(default);
        debug.Assert(tokenIsRegularExpressionLiteral);
        return lastError != nil;
    }
    return false;
}

internal static bool checkGrammarPrivateIdentifierExpression(this ж<Checker> Ꮡc, ж<ast.PrivateIdentifier> ᏑprivId) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var privId = ref ᏑprivId.DerefOrNull();

    var privIdAsNode = ᏑprivId.of(ast.PrivateIdentifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    if (ast.GetContainingClass(ᏑprivId.of(ast.PrivateIdentifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) == nil) {
        return Ꮡc.grammarErrorOnNode(ᏑprivId.of(ast.PrivateIdentifier.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.Private_identifiers_are_not_allowed_outside_class_bodies);
    }

    if (!ast.IsForInStatement(privId.Parent)) {
        if (!ast.IsExpressionNode(privIdAsNode)) {
            return Ꮡc.grammarErrorOnNode(privIdAsNode, diagnostics.Private_identifiers_are_only_allowed_in_class_bodies_and_may_only_be_used_as_part_of_a_class_member_declaration_property_access_or_on_the_left_hand_side_of_an_in_expression);
        }

        var isInOperation = ast.IsBinaryExpression(privId.Parent) && privId.Parent.AsBinaryExpression().OperatorToken.Kind == ast.KindInKeyword;
        if (c.getSymbolForPrivateIdentifierExpression(privIdAsNode) == nil && !isInOperation) {
            return Ꮡc.grammarErrorOnNode(privIdAsNode, diagnostics.Cannot_find_name_0, privId.Text);
        }
    }

    return false;
}

internal static bool checkGrammarMappedType(this ж<Checker> Ꮡc, ж<ast.MappedTypeNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (len(node.Members.Nodes) > 0) {
        return Ꮡc.grammarErrorOnNode(node.Members.Nodes[0], diagnostics.A_mapped_type_may_not_declare_properties_or_methods);
    }
    return false;
}

internal static bool checkGrammarDecorator(this ж<Checker> Ꮡc, ж<ast.Decorator> Ꮡdecorator) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var decorator = ref Ꮡdecorator.DerefOrNull();

    var sourceFile = ast.GetSourceFileOfNode(Ꮡdecorator.of(ast.Decorator.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    if (!c.hasParseDiagnostics(sourceFile)) {
        var node = decorator.Expression;

        if (ast.IsParenthesizedExpression(node)) {
            return false;
        }

        var canHaveCallExpression = true;
        ж<ast.Node> errorNode = default;
        for (;;) {
            if (ast.IsExpressionWithTypeArguments(node) || ast.IsNonNullExpression(node)) {
                node = node.Expression();
                continue;
            }

            if (ast.IsCallExpression(node)) {
                var callExpr = node.AsCallExpression();
                if (!canHaveCallExpression) {
                    errorNode = node;
                }
                if ((~callExpr).QuestionDotToken != nil) {
                    errorNode = callExpr.Value.QuestionDotToken;
                }
                node = callExpr.Value.Expression;
                canHaveCallExpression = false;
                continue;
            }

            if (ast.IsPropertyAccessExpression(node)) {
                var propertyAccessExpr = node.AsPropertyAccessExpression();
                if ((~propertyAccessExpr).QuestionDotToken != nil) {
                    errorNode = propertyAccessExpr.Value.QuestionDotToken;
                }
                node = propertyAccessExpr.Value.Expression;
                canHaveCallExpression = false;
                continue;
            }

            if (!ast.IsIdentifier(node)) {
                errorNode = node;
            }

            break;
        }

        if (errorNode != nil) {
            var err = Ꮡc.error(decorator.Expression, diagnostics.Expression_must_be_enclosed_in_parentheses_to_be_used_as_a_decorator);
            err.AddRelatedInfo(createDiagnosticForNode(errorNode, diagnostics.Invalid_syntax_in_decorator));
            return true;
        }
    }

    return false;
}

internal static bool checkGrammarExportDeclaration(this ж<Checker> Ꮡc, ж<ast.ExportDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.IsTypeOnly && node.ExportClause != nil && node.ExportClause.Kind == ast.KindNamedExports) {
        return Ꮡc.checkGrammarTypeOnlyNamedImportsOrExports(node.ExportClause);
    }
    return false;
}

internal static bool checkGrammarModuleElementContext(this ж<Checker> Ꮡc, ж<astꓸStatement> Ꮡnode, ж<diagnostics.Message> errorMessage) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var isInAppropriateContext = node.Parent.Kind == ast.KindSourceFile || node.Parent.Kind == ast.KindModuleBlock || node.Parent.Kind == ast.KindModuleDeclaration;
    if (!isInAppropriateContext) {
        Ꮡc.grammarErrorOnFirstToken(Ꮡnode, errorMessage);
    }
    return !isInAppropriateContext;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly object overrideˢ = (@string)"override"u8;
internal static readonly object declareˢ = (@string)"declare"u8;
internal static readonly object readonlyˢ = (@string)"readonly"u8;
internal static readonly object accessorˢ = (@string)"accessor"u8;
internal static readonly object asyncˢ = (@string)"async"u8;
internal static readonly object staticˢ = (@string)"static"u8;
internal static readonly object abstractˢ = (@string)"abstract"u8;
internal static readonly object exportˢ = (@string)"export"u8;
internal static readonly object defaultˢ = (@string)"default"u8;
internal static readonly object privateˢ = (@string)"private"u8;
internal static readonly object outˢ = (@string)"out"u8;

internal static bool checkGrammarModifiers(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Modifiers() == nil) {
        return false;
    }
    if (Ꮡc.reportObviousDecoratorErrors(Ꮡnode) || Ꮡc.reportObviousModifierErrors(Ꮡnode)) {
        return true;
    }
    if (ast.IsThisParameter(Ꮡnode)) {
        return Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.Neither_decorators_nor_modifiers_may_be_applied_to_this_parameters);
    }
    var blockScopeKind = ast.NodeFlagsNone;
    if (ast.IsVariableStatement(Ꮡnode)) {
        blockScopeKind = (ast.NodeFlags)(node.AsVariableStatement().DeclarationList.Flags & ast.NodeFlagsBlockScoped);
    }
    ж<ast.Node> lastStatic = default;
    ж<ast.Node> lastDeclare = default;
    ж<ast.Node> lastAsync = default;
    ж<ast.Node> lastOverride = default;
    ж<ast.Node> firstDecorator = default;
    var flags = ast.ModifierFlagsNone;
    var sawExportBeforeDecorators = false;
    var hasLeadingDecorators = false;
    var modifiers = node.ModifierNodes();
    foreach (var (_, modifier) in range(modifiers)) {
        if (ast.IsDecorator(modifier)) {
            if (!ast.NodeCanBeDecorated(c.legacyDecorators, Ꮡnode, node.Parent, node.Parent.Parent)) {
                if (node.Kind == ast.KindMethodDeclaration && !ast.NodeIsPresent(node.Body())) {
                    return Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.A_decorator_can_only_decorate_a_method_implementation_not_an_overload);
                } else {
                    return Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.Decorators_are_not_valid_here);
                }
            } else if (c.legacyDecorators && (node.Kind == ast.KindGetAccessor || node.Kind == ast.KindSetAccessor)) {
                var accessors = ast.GetAllAccessorDeclarationsForDeclaration(Ꮡnode, Ꮡc.getSymbolOfDeclaration(Ꮡnode).Declarations);
                if (ast.HasDecorators(accessors.FirstAccessor) && Ꮡnode == accessors.SecondAccessor) {
                    return Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.Decorators_cannot_be_applied_to_multiple_get_Slashset_accessors_of_the_same_name);
                }
            }

            if ((ast.ModifierFlags)(flags & ~((ast.ModifierFlags)(ast.ModifierFlagsExportDefault | ast.ModifierFlagsDecorator))) != 0) {
                return Ꮡc.grammarErrorOnNode(modifier, diagnostics.Decorators_are_not_valid_here);
            }

            if (hasLeadingDecorators && (ast.ModifierFlags)(flags & ast.ModifierFlagsModifier) != 0) {
                if (firstDecorator == nil) {
                    throw panic("Expected firstDecorator to be set");
                }
                var sourceFile = ast.GetSourceFileOfNode(modifier);
                if (!c.hasParseDiagnostics(sourceFile)) {
                    var err = Ꮡc.error(modifier, diagnostics.Decorators_may_not_appear_after_export_or_export_default_if_they_also_appear_before_export);
                    err.AddRelatedInfo(createDiagnosticForNode(firstDecorator, diagnostics.Decorator_used_before_export_here));
                    return true;
                }
                return false;
            }

            flags |= (ast.ModifierFlags)(ast.ModifierFlagsDecorator);

            if ((ast.ModifierFlags)(flags & ast.ModifierFlagsModifier) == 0) {
                hasLeadingDecorators = true;
            } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsExport) != 0) {
                sawExportBeforeDecorators = true;
            }

            if (firstDecorator == nil) {
                firstDecorator = modifier;
            }
        } else {
            if (modifier.Kind != ast.KindReadonlyKeyword) {
                if (node.Kind == ast.KindPropertySignature || node.Kind == ast.KindMethodSignature) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_type_member, scanner.TokenToString(modifier.Kind));
                }
                if (node.Kind == ast.KindIndexSignature && (modifier.Kind != ast.KindStaticKeyword || !ast.IsClassLike(node.Parent))) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_an_index_signature, scanner.TokenToString(modifier.Kind));
                }
            }
            if (modifier.Kind != ast.KindInKeyword && modifier.Kind != ast.KindOutKeyword && modifier.Kind != ast.KindConstKeyword) {
                if (node.Kind == ast.KindTypeParameter) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_type_parameter, scanner.TokenToString(modifier.Kind));
                }
            }
            switch (modifier.Kind) {
            case var ᴛ when ᴛ == ast.KindConstKeyword: {
                if (node.Kind != ast.KindEnumDeclaration && node.Kind != ast.KindTypeParameter) {
                    return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.A_class_member_cannot_have_the_0_keyword, scanner.TokenToString(ast.KindConstKeyword));
                }
                var parent = node.Parent;
                if (node.Kind == ast.KindTypeParameter) {
                    if (!(ast.IsFunctionLikeDeclaration(parent) || ast.IsClassLike(parent) || ast.IsFunctionTypeNode(parent) || ast.IsConstructorTypeNode(parent) || ast.IsCallSignatureDeclaration(parent) || ast.IsConstructSignatureDeclaration(parent) || ast.IsMethodSignatureDeclaration(parent))) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_can_only_appear_on_a_type_parameter_of_a_function_method_or_class, scanner.TokenToString(modifier.Kind));
                    }
                }
                break;
            }
            case var ᴛ when ᴛ == ast.KindOverrideKeyword: {
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsOverride) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, overrideˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, overrideˢ, declareˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsReadonly) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, overrideˢ, readonlyˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessor) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, overrideˢ, accessorˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, overrideˢ, asyncˢ);
                }
                flags |= (ast.ModifierFlags)(ast.ModifierFlagsOverride);
                lastOverride = modifier;
                break;
            }
            case var ᴛ when ᴛ == ast.KindPublicKeyword || ᴛ == ast.KindProtectedKeyword || ᴛ == ast.KindPrivateKeyword: {
                @string text = visibilityToString(ast.ModifierToFlag(modifier.Kind));

                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessibilityModifier) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.Accessibility_modifier_already_seen);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsOverride) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, text, overrideˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsStatic) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, text, staticˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessor) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, text, accessorˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsReadonly) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, text, readonlyˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, text, asyncˢ);
                } else if (node.Parent.Kind == ast.KindModuleBlock || node.Parent.Kind == ast.KindSourceFile) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_module_or_namespace_element, text);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAbstract) != 0) {
                    if (modifier.Kind == ast.KindPrivateKeyword) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, text, abstractˢ);
                    } else if ((ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, text, abstractˢ);
                    }
                } else if (ast.IsPrivateIdentifierClassElementDeclaration(Ꮡnode)) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.An_accessibility_modifier_cannot_be_used_with_a_private_identifier);
                }
                flags |= (ast.ModifierFlags)(ast.ModifierToFlag(modifier.Kind));
                break;
            }
            case var ᴛ when ᴛ == ast.KindStaticKeyword: {
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsStatic) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, staticˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsReadonly) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, staticˢ, readonlyˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, staticˢ, asyncˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessor) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, staticˢ, accessorˢ);
                } else if (node.Parent.Kind == ast.KindModuleBlock || node.Parent.Kind == ast.KindSourceFile) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_module_or_namespace_element, staticˢ);
                } else if (node.Kind == ast.KindParameter) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_parameter, staticˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAbstract) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, staticˢ, abstractˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsOverride) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, staticˢ, overrideˢ);
                }
                flags |= (ast.ModifierFlags)(ast.ModifierFlagsStatic);
                lastStatic = modifier;
                break;
            }
            case var ᴛ when ᴛ == ast.KindAccessorKeyword: {
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessor) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, accessorˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsReadonly) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, accessorˢ, readonlyˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, accessorˢ, declareˢ);
                } else if (node.Kind != ast.KindPropertyDeclaration) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_accessor_modifier_can_only_appear_on_a_property_declaration);
                }

                flags |= (ast.ModifierFlags)(ast.ModifierFlagsAccessor);
                break;
            }
            case var ᴛ when ᴛ == ast.KindReadonlyKeyword: {
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsReadonly) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, readonlyˢ);
                } else if (node.Kind != ast.KindPropertyDeclaration && node.Kind != ast.KindPropertySignature && node.Kind != ast.KindIndexSignature && node.Kind != ast.KindParameter) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_readonly_modifier_can_only_appear_on_a_property_declaration_or_index_signature);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessor) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, readonlyˢ, accessorˢ);
                }
                flags |= (ast.ModifierFlags)(ast.ModifierFlagsReadonly);
                break;
            }
            case var ᴛ when ᴛ == ast.KindExportKeyword: {
                if (c.compilerOptions.VerbatimModuleSyntax == core.TSTrue && (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0 && node.Kind != ast.KindTypeAliasDeclaration && node.Kind != ast.KindInterfaceDeclaration && node.Kind != ast.KindModuleDeclaration && node.Parent.Kind == ast.KindSourceFile && c.program.GetEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(ast.GetSourceFileOfNode(Ꮡnode))) == core.ModuleKindCommonJS) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.A_top_level_export_modifier_cannot_be_used_on_value_declarations_in_a_CommonJS_module_when_verbatimModuleSyntax_is_enabled);
                }
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsExport) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, exportˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, exportˢ, declareˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAbstract) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, exportˢ, abstractˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, exportˢ, asyncˢ);
                } else if (ast.IsClassLike(node.Parent) && !ast.IsJSTypeAliasDeclaration(Ꮡnode)) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_class_elements_of_this_kind, exportˢ);
                } else if (node.Kind == ast.KindParameter) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_parameter, exportˢ);
                } else if (blockScopeKind == ast.NodeFlagsUsing) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_using_declaration, exportˢ);
                } else if (blockScopeKind == ast.NodeFlagsAwaitUsing) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_an_await_using_declaration, exportˢ);
                }
                flags |= (ast.ModifierFlags)(ast.ModifierFlagsExport);
                break;
            }
            case var ᴛ when ᴛ == ast.KindDefaultKeyword: {
                ж<ast.Node> container = default;
                if (node.Parent.Kind == ast.KindSourceFile) {
                    container = node.Parent;
                } else {
                    container = node.Parent.Parent;
                }
                if (container.Kind == ast.KindModuleDeclaration && !ast.IsAmbientModule(container)) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.A_default_export_can_only_be_used_in_an_ECMAScript_style_module);
                } else if (blockScopeKind == ast.NodeFlagsUsing) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_using_declaration, defaultˢ);
                } else if (blockScopeKind == ast.NodeFlagsAwaitUsing) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_an_await_using_declaration, defaultˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsExport) == 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, exportˢ, defaultˢ);
                } else if (sawExportBeforeDecorators) {
                    return Ꮡc.grammarErrorOnNode(firstDecorator, diagnostics.Decorators_are_not_valid_here);
                }

                flags |= (ast.ModifierFlags)(ast.ModifierFlagsDefault);
                break;
            }
            case var ᴛ when ᴛ == ast.KindDeclareKeyword: {
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, declareˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_in_an_ambient_context, asyncˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsOverride) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_in_an_ambient_context, overrideˢ);
                } else if (ast.IsClassLike(node.Parent) && !ast.IsPropertyDeclaration(Ꮡnode)) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_class_elements_of_this_kind, declareˢ);
                } else if (node.Kind == ast.KindParameter) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_parameter, declareˢ);
                } else if (blockScopeKind == ast.NodeFlagsUsing) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_using_declaration, declareˢ);
                } else if (blockScopeKind == ast.NodeFlagsAwaitUsing) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_an_await_using_declaration, declareˢ);
                } else if (((ast.NodeFlags)(node.Parent.Flags & ast.NodeFlagsAmbient) != 0) && node.Parent.Kind == ast.KindModuleBlock) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.A_declare_modifier_cannot_be_used_in_an_already_ambient_context);
                } else if (ast.IsPrivateIdentifierClassElementDeclaration(Ꮡnode)) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_a_private_identifier, declareˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessor) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, declareˢ, accessorˢ);
                }
                flags |= (ast.ModifierFlags)(ast.ModifierFlagsAmbient);
                lastDeclare = modifier;
                break;
            }
            case var ᴛ when ᴛ == ast.KindAbstractKeyword: {
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAbstract) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, abstractˢ);
                }
                if (node.Kind != ast.KindClassDeclaration && node.Kind != ast.KindConstructorType) {
                    if (node.Kind != ast.KindMethodDeclaration && node.Kind != ast.KindPropertyDeclaration && node.Kind != ast.KindGetAccessor && node.Kind != ast.KindSetAccessor) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_abstract_modifier_can_only_appear_on_a_class_method_or_property_declaration);
                    }
                    if (!(node.Parent.Kind == ast.KindClassDeclaration && ast.HasSyntacticModifier(node.Parent, ast.ModifierFlagsAbstract))) {
                        ж<diagnostics.Message> message = default;
                        if (node.Kind == ast.KindPropertyDeclaration) {
                            message = diagnostics.Abstract_properties_can_only_appear_within_an_abstract_class;
                        } else {
                            message = diagnostics.Abstract_methods_can_only_appear_within_an_abstract_class;
                        }
                        return Ꮡc.grammarErrorOnNode(modifier, message);
                    }
                    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsStatic) != 0) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, staticˢ, abstractˢ);
                    }
                    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsPrivate) != 0) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, privateˢ, abstractˢ);
                    }
                    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0 && lastAsync != nil) {
                        return Ꮡc.grammarErrorOnNode(lastAsync, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, asyncˢ, abstractˢ);
                    }
                    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsOverride) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, abstractˢ, overrideˢ);
                    }
                    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAccessor) != 0 && (ast.NodeFlags)(modifier.Flags & ast.NodeFlagsReparsed) == 0) {
                        return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, abstractˢ, accessorˢ);
                    }
                }
                var name = node.Name(); if (name != nil && name.Kind == ast.KindPrivateIdentifier) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_a_private_identifier, abstractˢ);
                }

                flags |= (ast.ModifierFlags)(ast.ModifierFlagsAbstract);
                break;
            }
            case var ᴛ when ᴛ == ast.KindAsyncKeyword: {
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, asyncˢ);
                } else if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0 || (ast.NodeFlags)(node.Parent.Flags & ast.NodeFlagsAmbient) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_in_an_ambient_context, asyncˢ);
                } else if (node.Kind == ast.KindParameter) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_appear_on_a_parameter, asyncˢ);
                }
                if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAbstract) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_cannot_be_used_with_1_modifier, asyncˢ, abstractˢ);
                }
                flags |= (ast.ModifierFlags)(ast.ModifierFlagsAsync);
                lastAsync = modifier;
                break;
            }
            case var ᴛ when ᴛ == ast.KindInKeyword || ᴛ == ast.KindOutKeyword: {
                ast.ModifierFlags inOutFlag = default;
                if (modifier.Kind == ast.KindInKeyword) {
                    inOutFlag = ast.ModifierFlagsIn;
                } else {
                    inOutFlag = ast.ModifierFlagsOut;
                }
                @string inOutText = default;
                if (modifier.Kind == ast.KindInKeyword) {
                    inOutText = "in";
                } else {
                    inOutText = "out";
                }
                var parent = node.Parent;
                if (node.Kind != ast.KindTypeParameter || parent != nil && !(ast.IsInterfaceDeclaration(parent) || ast.IsClassLike(parent) || ast.IsTypeOrJSTypeAliasDeclaration(parent))) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_can_only_appear_on_a_type_parameter_of_a_class_interface_or_type_alias, inOutText);
                }
                if ((ast.ModifierFlags)(flags & inOutFlag) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_already_seen, inOutText);
                }
                if ((ast.ModifierFlags)(inOutFlag & ast.ModifierFlagsIn) != 0 && (ast.ModifierFlags)(flags & ast.ModifierFlagsOut) != 0) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.X_0_modifier_must_precede_1_modifier, (@string)"in", outˢ);
                }
                flags |= (ast.ModifierFlags)(inOutFlag);
                break;
            }}

        }
    }

    if (node.Kind == ast.KindConstructor) {
        if ((ast.ModifierFlags)(flags & ast.ModifierFlagsStatic) != 0) {
            return Ꮡc.grammarErrorOnNode(lastStatic, diagnostics.X_0_modifier_cannot_appear_on_a_constructor_declaration, staticˢ);
        }
        if ((ast.ModifierFlags)(flags & ast.ModifierFlagsOverride) != 0) {
            return Ꮡc.grammarErrorOnNode(lastOverride, diagnostics.X_0_modifier_cannot_appear_on_a_constructor_declaration, overrideˢ);
        }
        if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0) {
            return Ꮡc.grammarErrorOnNode(lastAsync, diagnostics.X_0_modifier_cannot_appear_on_a_constructor_declaration, asyncˢ);
        }
        return false;
    } else if ((node.Kind == ast.KindImportDeclaration || node.Kind == ast.KindJSImportDeclaration || node.Kind == ast.KindImportEqualsDeclaration) && (ast.ModifierFlags)(flags & ast.ModifierFlagsAmbient) != 0) {
        return Ꮡc.grammarErrorOnNode(lastDeclare, diagnostics.A_0_modifier_cannot_be_used_with_an_import_declaration, declareˢ);
    } else if (node.Kind == ast.KindParameter && ((ast.ModifierFlags)(flags & ast.ModifierFlagsParameterPropertyModifier) != 0) && ast.IsBindingPattern(node.Name())) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.A_parameter_property_may_not_be_declared_using_a_binding_pattern);
    } else if (node.Kind == ast.KindParameter && ((ast.ModifierFlags)(flags & ast.ModifierFlagsParameterPropertyModifier) != 0) && node.AsParameterDeclaration().DotDotDotToken != nil) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.A_parameter_property_cannot_be_declared_using_a_rest_parameter);
    }
    if ((ast.ModifierFlags)(flags & ast.ModifierFlagsAsync) != 0) {
        return Ꮡc.checkGrammarAsyncModifier(Ꮡnode, lastAsync);
    }
    return false;
}

internal static bool reportObviousModifierErrors(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    var modifier = c.findFirstIllegalModifier(node);
    if (modifier == nil) {
        return false;
    }
    return Ꮡc.grammarErrorOnFirstToken(modifier, diagnostics.Modifiers_cannot_appear_here);
}

internal static ж<ast.Node> findFirstModifierExcept(this ref Checker c, ж<ast.Node> Ꮡnode, ast.Kind allowedModifier) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var modifier = core.Find<ж<ast.Node>>(node.ModifierNodes(), ast.IsModifier);
    if (modifier != nil && modifier.Kind != allowedModifier) {
        return modifier;
    }
    return default;
}

internal static ж<ast.Node> findFirstIllegalModifier(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindConstructor || ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindPropertySignature || ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature || ᴛ == ast.KindIndexSignature || ᴛ == ast.KindModuleDeclaration || ᴛ == ast.KindImportDeclaration || ᴛ == ast.KindJSImportDeclaration || ᴛ == ast.KindImportEqualsDeclaration || ᴛ == ast.KindExportDeclaration || ᴛ == ast.KindExportAssignment || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindParameter || ᴛ == ast.KindTypeParameter || ᴛ == ast.KindJSTypeAliasDeclaration: {
        return default;
    }
    case var ᴛ when ᴛ == ast.KindClassStaticBlockDeclaration || ᴛ == ast.KindPropertyAssignment || ᴛ == ast.KindShorthandPropertyAssignment || ᴛ == ast.KindNamespaceExportDeclaration || ᴛ == ast.KindMissingDeclaration: {
        return core.Find<ж<ast.Node>>(node.ModifierNodes(), ast.IsModifier);
    }
    default: {
        if (node.Parent.Kind == ast.KindModuleBlock || node.Parent.Kind == ast.KindSourceFile) {
            return default;
        }
        switch (node.Kind) {
        case var ᴛ2 when ᴛ2 == ast.KindFunctionDeclaration: {
            return c.findFirstModifierExcept(Ꮡnode, ast.KindAsyncKeyword);
        }
        case var ᴛ2 when ᴛ2 == ast.KindClassDeclaration || ᴛ2 == ast.KindConstructorType: {
            return c.findFirstModifierExcept(Ꮡnode, ast.KindAbstractKeyword);
        }
        case var ᴛ2 when ᴛ2 == ast.KindClassExpression || ᴛ2 == ast.KindInterfaceDeclaration || ᴛ2 == ast.KindTypeAliasDeclaration: {
            return core.Find<ж<ast.Node>>(node.ModifierNodes(), ast.IsModifier);
        }
        case var ᴛ2 when ᴛ2 == ast.KindVariableStatement: {
            if ((ast.NodeFlags)(node.AsVariableStatement().DeclarationList.Flags & ast.NodeFlagsUsing) != 0) {
                return c.findFirstModifierExcept(Ꮡnode, ast.KindAwaitKeyword);
            }
            return core.Find<ж<ast.Node>>(node.ModifierNodes(), ast.IsModifier);
        }
        case var ᴛ2 when ᴛ2 == ast.KindEnumDeclaration: {
            return c.findFirstModifierExcept(Ꮡnode, ast.KindConstKeyword);
        }
        default: {
            throw panic("Unhandled case in findFirstIllegalModifier.");
            break;
        }}

        break;
    }}

}

internal static bool reportObviousDecoratorErrors(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    ref var c = ref Ꮡc.DerefOrNull();

    var decorator = c.findFirstIllegalDecorator(node);
    if (decorator == nil) {
        return false;
    }
    return Ꮡc.grammarErrorOnFirstToken(decorator, diagnostics.Decorators_are_not_valid_here);
}

internal static ж<ast.Node> findFirstIllegalDecorator(this ref Checker c, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.CanHaveIllegalDecorators(Ꮡnode)) {
        var decorator = core.Find<ж<ast.Node>>(node.ModifierNodes(), ast.IsDecorator);
        return decorator;
    } else {
        return default;
    }
}

internal static bool checkGrammarAsyncModifier(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<ast.Node> asyncModifier) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction: {
        return false;
    }}

    return Ꮡc.grammarErrorOnNode(asyncModifier, diagnostics.X_0_modifier_cannot_be_used_here, asyncˢ);
}

internal static bool checkGrammarForDisallowedTrailingComma(this ж<Checker> Ꮡc, ж<ast.NodeList> Ꮡlist, ж<diagnostics.Message> diag) {
    ref var list = ref Ꮡlist.DerefOrNull();

    if (Ꮡlist != nil && list.HasTrailingComma()) {
        return Ꮡc.grammarErrorAtPos(list.Nodes[0], list.End() - len(","), len(","), diag);
    }
    return false;
}

internal static bool checkGrammarTypeParameterList(this ж<Checker> Ꮡc, ж<ast.NodeList> ᏑtypeParameters, ж<ast.SourceFile> Ꮡfile) {
    ref var typeParameters = ref ᏑtypeParameters.DerefOrNull();
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (ᏑtypeParameters != nil && len(typeParameters.Nodes) == 0) {
        nint start = typeParameters.Pos() - len("<");
        nint end = scanner.SkipTrivia(@file.Text(), typeParameters.End()) + len(">");
        return Ꮡc.grammarErrorAtPos(Ꮡfile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), start, end - start, diagnostics.Type_parameter_list_cannot_be_empty);
    }
    return false;
}

internal static bool checkGrammarParameterList(this ж<Checker> Ꮡc, ж<ast.NodeList> Ꮡparameters) {
    ref var parameters = ref Ꮡparameters.DerefOrNull();

    var seenOptionalParameter = false;
    nint parameterCount = len(parameters.Nodes);

    foreach (var i in range(parameterCount)) {
        var parameter = parameters.Nodes[i].AsParameterDeclaration();
        if (parameter.DotDotDotToken != nil) {
            if (i != parameterCount - 1) {
                return Ꮡc.grammarErrorOnNode(parameter.DotDotDotToken, diagnostics.A_rest_parameter_must_be_last_in_a_parameter_list);
            }
            if ((ast.NodeFlags)(parameter.Flags & ast.NodeFlagsAmbient) == 0) {
                Ꮡc.checkGrammarForDisallowedTrailingComma(Ꮡparameters, diagnostics.A_rest_parameter_or_binding_pattern_may_not_have_a_trailing_comma);
            }

            if ((~parameter).QuestionToken != nil) {
                return Ꮡc.grammarErrorOnNode((~parameter).QuestionToken, diagnostics.A_rest_parameter_cannot_be_optional);
            }

            if ((~parameter).Initializer != nil) {
                return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.A_rest_parameter_cannot_have_an_initializer);
            }
        } else if (isOptionalDeclaration(parameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode())) {
            seenOptionalParameter = true;
            if ((~parameter).QuestionToken != nil && (ast.NodeFlags)((~parameter).QuestionToken.Flags & ast.NodeFlagsReparsed) == 0 && (~parameter).Initializer != nil) {
                return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.Parameter_cannot_have_question_mark_and_initializer);
            }
        } else if (seenOptionalParameter && (~parameter).Initializer == nil) {
            return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.A_required_parameter_cannot_follow_an_optional_parameter);
        }
    }

    return false;
}

internal static bool checkGrammarForUseStrictSimpleParameterList(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (c.languageVersion >= core.ScriptTargetES2016) {
        var body = node.Body();
        ж<ast.Node> useStrictDirective = default;
        if (body != nil && ast.IsBlock(body)) {
            useStrictDirective = binder.FindUseStrictPrologue(ast.GetSourceFileOfNode(Ꮡnode), body.Statements());
        }
        if (useStrictDirective != nil) {
            var nonSimpleParameters = core.Filter(node.Parameters(), (ж<ast.Node> n) => {
                var parameter = n.AsParameterDeclaration();
                return (~parameter).Initializer != nil || ast.IsBindingPattern(parameter.Name()) || isRestParameter(parameter.of(ast.ParameterDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
            });
            if (len(nonSimpleParameters) != 0) {
                foreach (var (_, parameter) in range(nonSimpleParameters)) {
                    var errΔ1 = Ꮡc.error(parameter, diagnostics.This_parameter_is_not_allowed_with_use_strict_directive);
                    errΔ1.AddRelatedInfo(createDiagnosticForNode(useStrictDirective, diagnostics.X_use_strict_directive_used_here));
                }

                var err = Ꮡc.error(useStrictDirective, diagnostics.X_use_strict_directive_cannot_be_used_with_non_simple_parameter_list);
                foreach (var (index, parameter) in range(nonSimpleParameters)) {
                    ж<diagnostics.Message> relatedMessage = default;
                    if (index == 0) {
                        relatedMessage = diagnostics.Non_simple_parameter_declared_here;
                    } else {
                        relatedMessage = diagnostics.X_and_here;
                    }
                    err.AddRelatedInfo(createDiagnosticForNode(parameter, relatedMessage));
                }

                return true;
            }
        }
    }
    return false;
}

internal static bool checkGrammarFunctionLikeDeclaration(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var @file = ast.GetSourceFileOfNode(Ꮡnode);
    var funcData = node.FunctionLikeData();
    return Ꮡc.checkGrammarModifiers(Ꮡnode) || Ꮡc.checkGrammarTypeParameterList(funcData.TypeParameters, @file) || Ꮡc.checkGrammarParameterList(funcData.Parameters) || Ꮡc.checkGrammarArrowFunction(Ꮡnode, @file) || (ast.IsFunctionLikeDeclaration(Ꮡnode) && Ꮡc.checkGrammarForUseStrictSimpleParameterList(Ꮡnode));
}

internal static bool checkGrammarClassLikeDeclaration(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var @file = ast.GetSourceFileOfNode(Ꮡnode);
    return Ꮡc.checkGrammarClassDeclarationHeritageClauses(Ꮡnode, @file) || Ꮡc.checkGrammarTypeParameterList(node.TypeParameterList(), @file);
}

internal static bool checkGrammarArrowFunction(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode, ж<ast.SourceFile> Ꮡfile) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (!ast.IsArrowFunction(Ꮡnode)) {
        return false;
    }

    var arrowFunc = node.AsArrowFunction();
    var typeParameters = arrowFunc.Value.TypeParameters;
    if (typeParameters != nil) {
        var typeParamNodes = typeParameters.Nodes;
        var hasConstraint = len(typeParamNodes) > 0 && typeParamNodes[0].AsTypeParameterDeclaration().Constraint != nil;
        if (!(len(typeParamNodes) > 1 || typeParameters.HasTrailingComma() || hasConstraint)) {
            if (tspath.FileExtensionIsOneOf(@file.FileName(), [tspath.ExtensionMts, tspath.ExtensionCts])) {
                Ꮡc.grammarErrorOnNode(typeParameters.Nodes[0], diagnostics.This_syntax_is_reserved_in_files_with_the_mts_or_cts_extension_Add_a_trailing_comma_or_explicit_constraint);
            }
        }
    }

    var equalsGreaterThanToken = arrowFunc.EqualsGreaterThanToken;
    @string arrowFullText = @file.Text().slice(equalsGreaterThanToken.Pos(), equalsGreaterThanToken.End());
    return strings.ContainsFunc(arrowFullText, stringutil.IsLineBreak) && Ꮡc.grammarErrorOnNode(equalsGreaterThanToken, diagnostics.Line_terminator_not_permitted_before_arrow);
}

internal static bool checkGrammarIndexSignatureParameters(this ж<Checker> Ꮡc, ж<ast.IndexSignatureDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var paramNodes = node.Parameters.Nodes;

    if (len(paramNodes) == 0) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.An_index_signature_must_have_exactly_one_parameter);
    }

    var parameter = paramNodes[0].AsParameterDeclaration();
    if (len(paramNodes) != 1) {
        return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.An_index_signature_must_have_exactly_one_parameter);
    }

    Ꮡc.checkGrammarForDisallowedTrailingComma(node.Parameters, diagnostics.An_index_signature_cannot_have_a_trailing_comma);
    if (parameter.DotDotDotToken != nil) {
        return Ꮡc.grammarErrorOnNode(parameter.DotDotDotToken, diagnostics.An_index_signature_cannot_have_a_rest_parameter);
    }
    if (parameter.of(ast.ParameterDeclaration.ᏑModifiersBase).Modifiers() != nil) {
        return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.An_index_signature_parameter_cannot_have_an_accessibility_modifier);
    }
    if ((~parameter).QuestionToken != nil) {
        return Ꮡc.grammarErrorOnNode((~parameter).QuestionToken, diagnostics.An_index_signature_parameter_cannot_have_a_question_mark);
    }
    if ((~parameter).Initializer != nil) {
        return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.An_index_signature_parameter_cannot_have_an_initializer);
    }
    var typeNode = parameter.Value.Type;
    if (typeNode == nil) {
        return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.An_index_signature_parameter_must_have_a_type_annotation);
    }
    var t = Ꮡc.getTypeFromTypeNode(typeNode);
    if (someType(t, (ж<Type> tΔ1) => (TypeFlags)(tΔ1.flags & TypeFlagsStringOrNumberLiteralOrUnique) != 0) || Ꮡc.isGenericType(t)) {
        return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.An_index_signature_parameter_type_cannot_be_a_literal_type_or_generic_type_Consider_using_a_mapped_object_type_instead);
    }
    if (!everyType(t, Ꮡc.isValidIndexKeyType)) {
        return Ꮡc.grammarErrorOnNode(parameter.Name(), diagnostics.An_index_signature_parameter_type_must_be_string_number_symbol_or_a_template_literal_type);
    }
    if (node.Type == nil) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.An_index_signature_must_have_a_type_annotation);
    }
    return false;
}

internal static bool checkGrammarIndexSignature(this ж<Checker> Ꮡc, ж<ast.IndexSignatureDeclaration> node) {
    return Ꮡc.checkGrammarModifiers(node.of(ast.IndexSignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) || Ꮡc.checkGrammarIndexSignatureParameters(node);
}

internal static bool checkGrammarForAtLeastOneTypeArgument(this ж<Checker> Ꮡc, ж<ast.Node> node, ж<ast.NodeList> ᏑtypeArguments) {
    ref var typeArguments = ref ᏑtypeArguments.DerefOrNull();

    if (ᏑtypeArguments != nil && len(typeArguments.Nodes) == 0) {
        var sourceFile = ast.GetSourceFileOfNode(node);
        nint start = typeArguments.Pos() - len("<");
        nint end = scanner.SkipTrivia(sourceFile.Text(), typeArguments.End()) + len(">");
        return Ꮡc.grammarErrorAtPos(sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), start, end - start, diagnostics.Type_argument_list_cannot_be_empty);
    }
    return false;
}

internal static bool checkGrammarTypeArguments(this ж<Checker> Ꮡc, ж<ast.Node> node, ж<ast.NodeList> typeArguments) {
    return Ꮡc.checkGrammarForDisallowedTrailingComma(typeArguments, diagnostics.Trailing_comma_not_allowed) || Ꮡc.checkGrammarForAtLeastOneTypeArgument(node, typeArguments);
}

internal static bool checkGrammarTaggedTemplateChain(this ж<Checker> Ꮡc, ж<ast.TaggedTemplateExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.QuestionDotToken != nil || (ast.NodeFlags)(node.Flags & ast.NodeFlagsOptionalChain) != 0) {
        return Ꮡc.grammarErrorOnNode(node.Template, diagnostics.Tagged_template_expressions_are_not_permitted_in_an_optional_chain);
    }
    return false;
}

internal static bool checkGrammarHeritageClause(this ж<Checker> Ꮡc, ж<ast.HeritageClause> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var types = node.Types;
    if (Ꮡc.checkGrammarForDisallowedTrailingComma(types, diagnostics.Trailing_comma_not_allowed)) {
        return true;
    }
    if (types != nil && len(types.Nodes) == 0) {
        @string listType = scanner.TokenToString(node.Token);
        return Ꮡc.grammarErrorAtPos(Ꮡnode.of(ast.HeritageClause.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), types.Pos(), 0, diagnostics.X_0_list_cannot_be_empty, listType);
    }

    foreach (var (_, nodeΔ1) in range(types.Nodes)) {
        if (Ꮡc.checkGrammarExpressionWithTypeArguments(nodeΔ1)) {
            return true;
        }
    }
    return false;
}

internal static bool checkGrammarExpressionWithTypeArguments(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsExpressionWithTypeArguments(Ꮡnode) && node.Expression().Kind == ast.KindImportKeyword && node.TypeArgumentList() != nil) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.This_use_of_import_is_invalid_import_calls_can_be_written_but_they_must_have_parentheses_and_cannot_have_type_arguments);
    }
    return Ꮡc.checkGrammarTypeArguments(Ꮡnode, node.TypeArgumentList());
}

internal static bool checkGrammarClassDeclarationHeritageClauses(this ж<Checker> Ꮡc, ж<astꓸClassLikeDeclaration> Ꮡnode, ж<ast.SourceFile> @file) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var seenExtendsClause = false;
    var seenImplementsClause = false;

    var classLikeData = node.ClassLikeData();

    if (!Ꮡc.checkGrammarModifiers(Ꮡnode) && classLikeData.HeritageClauses != nil) {
        foreach (var (_, heritageClauseNode) in range(classLikeData.HeritageClauses.Nodes)) {
            var heritageClause = heritageClauseNode.AsHeritageClause();
            if (heritageClause.Token == ast.KindExtendsKeyword) {
                if (seenExtendsClause) {
                    return Ꮡc.grammarErrorOnFirstToken(heritageClauseNode, diagnostics.X_extends_clause_already_seen);
                }

                if (seenImplementsClause) {
                    return Ꮡc.grammarErrorOnFirstToken(heritageClauseNode, diagnostics.X_extends_clause_must_precede_implements_clause);
                }

                var typeNodes = heritageClause.Types.Nodes;
                if (len(typeNodes) > 1) {
                    return Ꮡc.grammarErrorOnFirstToken(typeNodes[1], diagnostics.Classes_can_only_extend_a_single_class);
                }

                seenExtendsClause = true;
            } else {
                if (heritageClause.Token != ast.KindImplementsKeyword) {
                    throw panic(fmt.Sprintf("Unexpected token %q"u8, heritageClause.Token));
                }
                if (seenImplementsClause) {
                    return Ꮡc.grammarErrorOnFirstToken(heritageClauseNode, diagnostics.X_implements_clause_already_seen);
                }

                seenImplementsClause = true;
            }

            Ꮡc.checkGrammarHeritageClause(heritageClause);
        }
    }

    return false;
}

internal static bool checkGrammarInterfaceDeclaration(this ж<Checker> Ꮡc, ж<ast.InterfaceDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.HeritageClauses != nil) {
        var seenExtendsClause = false;
        foreach (var (_, heritageClauseNode) in range(node.HeritageClauses.Nodes)) {
            var heritageClause = heritageClauseNode.AsHeritageClause();

            switch (heritageClause.Token) {
            case var ᴛ when ᴛ == ast.KindExtendsKeyword: {
                if (seenExtendsClause) {
                    return Ꮡc.grammarErrorOnFirstToken(heritageClauseNode, diagnostics.X_extends_clause_already_seen);
                }
                seenExtendsClause = true;
                break;
            }
            case var ᴛ when ᴛ == ast.KindImplementsKeyword: {
                return Ꮡc.grammarErrorOnFirstToken(heritageClauseNode, diagnostics.Interface_declaration_cannot_have_implements_clause);
            }
            default: {
                throw panic(fmt.Sprintf("Unexpected token %q"u8, heritageClause.Token.String()));
                break;
            }}

            Ꮡc.checkGrammarHeritageClause(heritageClause);
        }
    }

    return false;
}

internal static bool checkGrammarComputedPropertyName(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind != ast.KindComputedPropertyName) {
        return false;
    }

    var computedPropertyName = node.AsComputedPropertyName();
    if ((~computedPropertyName).Expression.Kind == ast.KindBinaryExpression && (~computedPropertyName).Expression.AsBinaryExpression().OperatorToken.Kind == ast.KindCommaToken) {
        return Ꮡc.grammarErrorOnNode((~computedPropertyName).Expression, diagnostics.A_comma_expression_is_not_allowed_in_a_computed_property_name);
    }
    return false;
}

internal static bool checkGrammarForGenerator(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var bodyData = node.BodyData(); if (bodyData != nil && bodyData.AsteriskToken != nil) {
        if (node.Kind != ast.KindFunctionDeclaration && node.Kind != ast.KindFunctionExpression && node.Kind != ast.KindMethodDeclaration) {
            throw panic(fmt.Sprintf("Unexpected node kind %q"u8, node.Kind));
        }
        if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0) {
            return Ꮡc.grammarErrorOnNode(bodyData.AsteriskToken, diagnostics.Generators_are_not_allowed_in_an_ambient_context);
        }
        if (bodyData.Body == nil) {
            return Ꮡc.grammarErrorOnNode(bodyData.AsteriskToken, diagnostics.An_overload_signature_cannot_be_declared_as_a_generator);
        }
    }

    return false;
}

internal static bool checkGrammarForInvalidQuestionMark(this ж<Checker> Ꮡc, ж<astꓸTokenNode> ᏑpostfixToken, ж<diagnostics.Message> message) {
    ref var postfixToken = ref ᏑpostfixToken.DerefOrNull();

    return ᏑpostfixToken != nil && postfixToken.Kind == ast.KindQuestionToken && Ꮡc.grammarErrorOnNode(ᏑpostfixToken, message);
}

internal static bool checkGrammarForInvalidExclamationToken(this ж<Checker> Ꮡc, ж<astꓸTokenNode> ᏑpostfixToken, ж<diagnostics.Message> message) {
    ref var postfixToken = ref ᏑpostfixToken.DerefOrNull();

    return ᏑpostfixToken != nil && postfixToken.Kind == ast.KindExclamationToken && Ꮡc.grammarErrorOnNode(ᏑpostfixToken, message);
}

internal static bool checkGrammarObjectLiteralExpression(this ж<Checker> Ꮡc, ж<ast.ObjectLiteralExpression> Ꮡnode, bool inDestructuring) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var seen = new map<@string, DeclarationMeaning>();

    slice<ж<ast.Node>> properties = default;
    if (node.Properties != nil) {
        properties = node.Properties.Nodes;
    }
    foreach (var (_, prop) in range(properties)) {
        if (prop.Kind == ast.KindSpreadAssignment) {
            var spreadAssignment = prop.AsSpreadAssignment();
            if (inDestructuring) {
                var expression = ast.SkipParentheses((~spreadAssignment).Expression);
                if (ast.IsArrayLiteralExpression(expression) || ast.IsObjectLiteralExpression(expression)) {
                    return Ꮡc.grammarErrorOnNode((~spreadAssignment).Expression, diagnostics.A_rest_element_cannot_contain_a_binding_pattern);
                }
            }
            continue;
        }
        var name = prop.Name();
        if (name.Kind == ast.KindComputedPropertyName) {
            Ꮡc.checkGrammarComputedPropertyName(name);
        }

        if (prop.Kind == ast.KindShorthandPropertyAssignment && !inDestructuring) {
            var shorthandProp = prop.AsShorthandPropertyAssignment();
            if (shorthandProp.ObjectAssignmentInitializer != nil) {
                ref var lastNodeBeforeInitializer = ref heap<ж<ast.Node>>(out var ᏑlastNodeBeforeInitializer);
                var shorthandPropʗ1 = shorthandProp;
                shorthandProp.ForEachChild((ж<ast.Node> child) => {
                    if (child != shorthandPropʗ1.ObjectAssignmentInitializer) {
                        ᏑlastNodeBeforeInitializer.ValueSlot = child;
                        return false;
                    }
                    return true;
                });

                Ꮡc.grammarErrorOnFirstToken(lastNodeBeforeInitializer, diagnostics.Did_you_mean_to_use_a_Colon_An_can_only_follow_a_property_name_when_the_containing_object_literal_is_part_of_a_destructuring_pattern);
            }
        }

        if (name.Kind == ast.KindPrivateIdentifier) {
            Ꮡc.grammarErrorOnNode(name, diagnostics.Private_identifiers_are_not_allowed_outside_class_bodies);
        }

        var modifiers = prop.ModifierNodes(); if (len(modifiers) != 0) {
            if (ast.CanHaveModifiers(prop)) {
                foreach (var (_, mod) in range(modifiers)) {
                    if (ast.IsModifier(mod) && (mod.Kind != ast.KindAsyncKeyword || prop.Kind != ast.KindMethodDeclaration)) {
                        Ꮡc.grammarErrorOnNode(mod, diagnostics.X_0_modifier_cannot_be_used_here, scanner.GetTextOfNode(mod));
                    }
                }
            } else if (ast.CanHaveIllegalModifiers(prop)) {
                foreach (var (_, mod) in range(modifiers)) {
                    if (ast.IsModifier(mod)) {
                        Ꮡc.grammarErrorOnNode(mod, diagnostics.X_0_modifier_cannot_be_used_here, scanner.GetTextOfNode(mod));
                    }
                }
            }
        }

        DeclarationMeaning currentKind = default;
        switch (prop.Kind) {
        case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment || ᴛ == ast.KindPropertyAssignment: {
            ж<ast.NamedMemberBase> commonProp = default;
            if (prop.Kind == ast.KindShorthandPropertyAssignment) {
                prop.ClassLikeData();
                commonProp = prop.AsShorthandPropertyAssignment().ᏑNamedMemberBase;
            } else {
                commonProp = prop.AsPropertyAssignment().ᏑNamedMemberBase;
            }

            Ꮡc.checkGrammarForInvalidExclamationToken(commonProp.PostfixToken, diagnostics.A_definite_assignment_assertion_is_not_permitted_in_this_context);
            Ꮡc.checkGrammarForInvalidQuestionMark(commonProp.PostfixToken, diagnostics.An_object_member_cannot_be_declared_optional);

            if (name.Kind == ast.KindNumericLiteral) {
                Ꮡc.checkGrammarNumericLiteral(name.AsNumericLiteral());
            }

            if (name.Kind == ast.KindBigIntLiteral) {
                Ꮡc.addErrorOrSuggestion(true, createDiagnosticForNode(name, diagnostics.A_bigint_literal_cannot_be_used_as_a_property_name));
            }

            currentKind = DeclarationMeaningPropertyAssignment;
            break;
        }
        case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
            currentKind = DeclarationMeaningMethod;
            break;
        }
        case var ᴛ when ᴛ == ast.KindGetAccessor: {
            currentKind = DeclarationMeaningGetAccessor;
            break;
        }
        case var ᴛ when ᴛ == ast.KindSetAccessor: {
            currentKind = DeclarationMeaningSetAccessor;
            break;
        }
        default: {
            throw panic(fmt.Sprintf("Unexpected node kind %q"u8, prop.Kind));
            break;
        }}

        if (!inDestructuring) {
            var (effectiveName, ok) = Ꮡc.getEffectivePropertyNameForPropertyNameNode(name);
            if (!ok) {
                continue;
            }

            var existingKind = seen[effectiveName];
            if (existingKind == 0) {
                seen[effectiveName] = currentKind;
            } else {
                if (((DeclarationMeaning)(currentKind & DeclarationMeaningMethod) != 0) && ((DeclarationMeaning)(existingKind & DeclarationMeaningMethod) != 0)) {
                    Ꮡc.grammarErrorOnNode(name, diagnostics.Duplicate_identifier_0, scanner.GetTextOfNode(name));
                } else if (((DeclarationMeaning)(currentKind & DeclarationMeaningPropertyAssignment) != 0) && ((DeclarationMeaning)(existingKind & DeclarationMeaningPropertyAssignment) != 0)) {
                    Ꮡc.grammarErrorOnNode(name, diagnostics.An_object_literal_cannot_have_multiple_properties_with_the_same_name, scanner.GetTextOfNode(name));
                } else if (((DeclarationMeaning)(currentKind & DeclarationMeaningGetOrSetAccessor) != 0) && ((DeclarationMeaning)(existingKind & DeclarationMeaningGetOrSetAccessor) != 0)) {
                    if (existingKind != DeclarationMeaningGetOrSetAccessor && currentKind != existingKind) {
                        seen[effectiveName] = (DeclarationMeaning)(currentKind | existingKind);
                    } else {
                        return Ꮡc.grammarErrorOnNode(name, diagnostics.An_object_literal_cannot_have_multiple_get_Slashset_accessors_with_the_same_name);
                    }
                } else {
                    return Ꮡc.grammarErrorOnNode(name, diagnostics.An_object_literal_cannot_have_property_and_accessor_with_the_same_name);
                }
            }
        }
    }

    return false;
}

internal static bool checkGrammarJsxElement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    Ꮡc.checkGrammarJsxName(node.TagName());
    Ꮡc.checkGrammarTypeArguments(Ꮡnode, node.TypeArgumentList());
    ref var seen = ref heap(new collections.Set<@string>(), out var Ꮡseen);
    foreach (var (_, attrNode) in range(node.Attributes().Properties())) {
        if (attrNode.Kind == ast.KindJsxSpreadAttribute) {
            continue;
        }
        var attr = attrNode.AsJsxAttribute();
        var name = attr.Name();
        var initializer = attr.Value.Initializer;
        @string textOfName = name.Text();
        if (!Ꮡseen.Has(textOfName)) {
            seen.Add(textOfName);
        } else {
            return Ꮡc.grammarErrorOnNode(name, diagnostics.JSX_elements_cannot_have_multiple_attributes_with_the_same_name);
        }
        if (initializer != nil && initializer.Kind == ast.KindJsxExpression && initializer.Expression() == nil) {
            return Ꮡc.grammarErrorOnNode(initializer, diagnostics.JSX_attributes_must_only_be_assigned_a_non_empty_expression);
        }
    }
    return false;
}

internal static bool checkGrammarJsxName(this ж<Checker> Ꮡc, ж<astꓸJsxTagNameExpression> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsPropertyAccessExpression(Ꮡnode) && ast.IsJsxNamespacedName(node.Expression())) {
        return Ꮡc.grammarErrorOnNode(node.Expression(), diagnostics.JSX_property_access_expressions_cannot_include_JSX_namespace_names);
    }

    if (ast.IsJsxNamespacedName(Ꮡnode) && c.compilerOptions.GetJSXTransformEnabled() && !scanner.IsIntrinsicJsxName(node.AsJsxNamespacedName().Namespace.Text())) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.React_components_cannot_include_JSX_namespace_names);
    }

    return false;
}

internal static bool checkGrammarJsxExpression(this ж<Checker> Ꮡc, ж<ast.JsxExpression> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Expression != nil && ast.IsCommaSequence(node.Expression)) {
        return Ꮡc.grammarErrorOnNode(node.Expression, diagnostics.JSX_expressions_may_not_use_the_comma_operator_Did_you_mean_to_write_an_array);
    }

    return false;
}

internal static bool checkGrammarForInOrForOfStatement(this ж<Checker> Ꮡc, ж<ast.ForInOrOfStatement> ᏑforInOrOfStatement) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var forInOrOfStatement = ref ᏑforInOrOfStatement.DerefOrNull();

    var asNode = ᏑforInOrOfStatement.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    if (Ꮡc.checkGrammarStatementInAmbientContext(asNode)) {
        return true;
    }

    if (forInOrOfStatement.Kind == ast.KindForOfStatement && forInOrOfStatement.AwaitModifier != nil) {
        if ((ast.NodeFlags)(forInOrOfStatement.Flags & ast.NodeFlagsAwaitContext) == 0) {
            var sourceFile = ast.GetSourceFileOfNode(asNode);
            if (ast.IsInTopLevelContext(asNode)) {
                if (!c.hasParseDiagnostics(sourceFile)) {
                    if (!ast.IsEffectiveExternalModule(sourceFile, c.compilerOptions)) {
                        Ꮡc.addDiagnostic(createDiagnosticForNode(forInOrOfStatement.AwaitModifier, diagnostics.X_for_await_loops_are_only_allowed_at_the_top_level_of_a_file_when_that_file_is_a_module_but_this_file_has_no_imports_or_exports_Consider_adding_an_empty_export_to_make_this_file_a_module));
                    }
                    var exprᴛ1 = c.moduleKind;
                    var matchᴛ1 = false;
                    if (exprᴛ1 == core.ModuleKindNode16 || exprᴛ1 == core.ModuleKindNode18 || exprᴛ1 == core.ModuleKindNode20 || exprᴛ1 == core.ModuleKindNodeNext) { matchᴛ1 = true;
                        do {
                            var sourceFileMetaData = c.program.GetSourceFileMetaData(sourceFile.Path());
                            if (sourceFileMetaData.ImpliedNodeFormat == core.ModuleKindCommonJS) {
                                Ꮡc.addDiagnostic(createDiagnosticForNode(forInOrOfStatement.AwaitModifier, diagnostics.The_current_file_is_a_CommonJS_module_and_cannot_use_await_at_the_top_level));
                                break;
                            }
                            fallthrough = true;
                        } while (false);
                    }
                    if (fallthrough || !matchᴛ1 && (exprᴛ1 == core.ModuleKindES2022 || exprᴛ1 == core.ModuleKindESNext || exprᴛ1 == core.ModuleKindPreserve || exprᴛ1 == core.ModuleKindSystem)) { matchᴛ1 = true;
                        do {
                            if (c.languageVersion >= core.ScriptTargetES2017) {
                                break;
                            }
                            fallthrough = true;
                        } while (false);
                    }
                    if (fallthrough || !matchᴛ1) { /* default: */
                        Ꮡc.addDiagnostic(createDiagnosticForNode(forInOrOfStatement.AwaitModifier, diagnostics.Top_level_for_await_loops_are_only_allowed_when_the_module_option_is_set_to_es2022_esnext_system_node16_node18_node20_nodenext_or_preserve_and_the_target_option_is_set_to_es2017_or_higher));
                    }

                }
            } else {
                if (!c.hasParseDiagnostics(sourceFile)) {
                    var diagnostic = createDiagnosticForNode(forInOrOfStatement.AwaitModifier, diagnostics.X_for_await_loops_are_only_allowed_within_async_functions_and_at_the_top_levels_of_modules);
                    var containingFunc = ast.GetContainingFunction(ᏑforInOrOfStatement.of(ast.ForInOrOfStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                    if (containingFunc != nil && containingFunc.Kind != ast.KindConstructor) {
                        debug.Assert(((ast.FunctionFlags)(ast.GetFunctionFlags(containingFunc) & ast.FunctionFlagsAsync)) == 0, "Enclosing function should never be an async function.");
                        var relatedInfo = createDiagnosticForNode(containingFunc, diagnostics.Did_you_mean_to_mark_this_function_as_async);
                        diagnostic.AddRelatedInfo(relatedInfo);
                    }
                    Ꮡc.addDiagnostic(diagnostic);
                    return true;
                }
            }
        }
    }

    if (ast.IsForOfStatement(asNode) && (ast.NodeFlags)(forInOrOfStatement.Flags & ast.NodeFlagsAwaitContext) == 0 && ast.IsIdentifier(forInOrOfStatement.Initializer) && forInOrOfStatement.Initializer.Text() == "async"u8) {
        Ꮡc.grammarErrorOnNode(forInOrOfStatement.Initializer, diagnostics.The_left_hand_side_of_a_for_of_statement_may_not_be_async);
        return false;
    }

    if (forInOrOfStatement.Initializer.Kind == ast.KindVariableDeclarationList) {
        var variableList = forInOrOfStatement.Initializer.AsVariableDeclarationList();
        if (!Ꮡc.checkGrammarVariableDeclarationList(variableList)) {
            var declarations = variableList.Declarations;

            if (len(declarations.Nodes) == 0) {
                return false;
            }

            if (len(declarations.Nodes) > 1) {
                ж<diagnostics.Message> diagnostic = default;
                if (forInOrOfStatement.Kind == ast.KindForInStatement) {
                    diagnostic = diagnostics.Only_a_single_variable_declaration_is_allowed_in_a_for_in_statement;
                } else {
                    diagnostic = diagnostics.Only_a_single_variable_declaration_is_allowed_in_a_for_of_statement;
                }
                return Ꮡc.grammarErrorOnFirstToken(declarations.Nodes[1], diagnostic);
            }

            var firstVariableDeclaration = declarations.Nodes[0].AsVariableDeclaration();
            if ((~firstVariableDeclaration).Initializer != nil) {
                ж<diagnostics.Message> diagnostic = default;
                if (forInOrOfStatement.Kind == ast.KindForInStatement) {
                    diagnostic = diagnostics.The_variable_declaration_of_a_for_in_statement_cannot_have_an_initializer;
                } else {
                    diagnostic = diagnostics.The_variable_declaration_of_a_for_of_statement_cannot_have_an_initializer;
                }
                return Ꮡc.grammarErrorOnNode(firstVariableDeclaration.Name(), diagnostic);
            }
            if ((~firstVariableDeclaration).Type != nil) {
                ж<diagnostics.Message> diagnostic = default;
                if (forInOrOfStatement.Kind == ast.KindForInStatement) {
                    diagnostic = diagnostics.The_left_hand_side_of_a_for_in_statement_cannot_use_a_type_annotation;
                } else {
                    diagnostic = diagnostics.The_left_hand_side_of_a_for_of_statement_cannot_use_a_type_annotation;
                }
                return Ꮡc.grammarErrorOnNode(firstVariableDeclaration.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostic);
            }
        }
    }

    return false;
}

internal static bool checkGrammarAccessor(this ж<Checker> Ꮡc, ж<astꓸAccessorDeclaration> Ꮡaccessor) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var accessor = ref Ꮡaccessor.DerefOrNull();

    var body = accessor.Body();
    if ((ast.NodeFlags)(accessor.Flags & ast.NodeFlagsAmbient) == 0 && (accessor.Parent.Kind != ast.KindTypeLiteral) && (accessor.Parent.Kind != ast.KindInterfaceDeclaration)) {
        if (body == nil && !ast.HasSyntacticModifier(Ꮡaccessor, ast.ModifierFlagsAbstract)) {
            return Ꮡc.grammarErrorAtPos(Ꮡaccessor, accessor.End() - 1, len(";"), diagnostics.X_0_expected, (@string)"{");
        }
    }
    if (body != nil) {
        if (ast.HasSyntacticModifier(Ꮡaccessor, ast.ModifierFlagsAbstract)) {
            return Ꮡc.grammarErrorOnNode(Ꮡaccessor, diagnostics.An_abstract_accessor_cannot_have_an_implementation);
        }
        if (accessor.Parent.Kind == ast.KindTypeLiteral || accessor.Parent.Kind == ast.KindInterfaceDeclaration) {
            return Ꮡc.grammarErrorOnNode(body, diagnostics.An_implementation_cannot_be_declared_in_ambient_contexts);
        }
    }

    var funcData = accessor.FunctionLikeData();
    ж<ast.NodeList> typeParameters = default;
    if (funcData != nil) {
        typeParameters = funcData.TypeParameters;
    }

    if (typeParameters != nil) {
        return Ꮡc.grammarErrorOnNode(accessor.Name(), diagnostics.An_accessor_cannot_have_type_parameters);
    }
    if (!c.doesAccessorHaveCorrectParameterCount(Ꮡaccessor)) {
        return Ꮡc.grammarErrorOnNode(accessor.Name(), core.IfElse(accessor.Kind == ast.KindGetAccessor, diagnostics.A_get_accessor_cannot_have_parameters, diagnostics.A_set_accessor_must_have_exactly_one_parameter));
    }
    if (accessor.Kind == ast.KindSetAccessor) {
        if (funcData.Type != nil) {
            return Ꮡc.grammarErrorOnNode(accessor.Name(), diagnostics.A_set_accessor_cannot_have_a_return_type_annotation);
        }

        var parameterNode = GetSetAccessorValueParameter(Ꮡaccessor);
        if (parameterNode == nil) {
            throw panic("Return value does not match parameter count assertion.");
        }
        var parameter = parameterNode.AsParameterDeclaration();
        if (parameter.DotDotDotToken != nil) {
            return Ꮡc.grammarErrorOnNode(parameter.DotDotDotToken, diagnostics.A_set_accessor_cannot_have_rest_parameter);
        }
        if ((~parameter).QuestionToken != nil) {
            return Ꮡc.grammarErrorOnNode((~parameter).QuestionToken, diagnostics.A_set_accessor_cannot_have_an_optional_parameter);
        }
        if ((~parameter).Initializer != nil) {
            return Ꮡc.grammarErrorOnNode(accessor.Name(), diagnostics.A_set_accessor_parameter_cannot_have_an_initializer);
        }
    }

    return false;
}

internal static bool doesAccessorHaveCorrectParameterCount(this ref Checker c, ж<astꓸAccessorDeclaration> Ꮡaccessor) {
    ref var accessor = ref Ꮡaccessor.DerefOrNull();

    return c.getAccessorThisParameter(Ꮡaccessor) != nil || len(accessor.Parameters()) == core.IfElse<nint>(accessor.Kind == ast.KindGetAccessor, 0, 1);
}

internal static bool checkGrammarTypeOperatorNode(this ж<Checker> Ꮡc, ж<ast.TypeOperatorNode> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Operator == ast.KindUniqueKeyword) {
        var innerType = node.Type;
        if (innerType.Kind != ast.KindSymbolKeyword) {
            return Ꮡc.grammarErrorOnNode(innerType, diagnostics.X_0_expected, scanner.TokenToString(ast.KindSymbolKeyword));
        }
        var parent = ast.WalkUpParenthesizedTypes(node.Parent);
        switch (parent.Kind) {
        case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
            var decl = parent.AsVariableDeclaration();
            if (decl.Name().Kind != ast.KindIdentifier) {
                return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_unique_symbol_types_may_not_be_used_on_a_variable_declaration_with_a_binding_name);
            }
            if (!isVariableDeclarationInVariableStatement(ref (decl.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).DerefOrNull())) {
                return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_unique_symbol_types_are_only_allowed_on_variables_in_a_variable_statement);
            }
            if ((ast.NodeFlags)(decl.Parent.Flags & ast.NodeFlagsConst) == 0) {
                return Ꮡc.grammarErrorOnNode(parent.AsVariableDeclaration().Name(), diagnostics.A_variable_whose_type_is_a_unique_symbol_type_must_be_const);
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
            if (!ast.IsStatic(parent) || !hasReadonlyModifier(parent)) {
                return Ꮡc.grammarErrorOnNode(parent.AsPropertyDeclaration().Name(), diagnostics.A_property_of_a_class_whose_type_is_a_unique_symbol_type_must_be_both_static_and_readonly);
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindPropertySignature: {
            if (!ast.HasSyntacticModifier(parent, ast.ModifierFlagsReadonly)) {
                return Ꮡc.grammarErrorOnNode(parent.AsPropertySignatureDeclaration().Name(), diagnostics.A_property_of_an_interface_or_type_literal_whose_type_is_a_unique_symbol_type_must_be_readonly);
            }
            break;
        }
        default: {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_unique_symbol_types_are_not_allowed_here);
        }}

    } else if (node.Operator == ast.KindReadonlyKeyword) {
        var innerType = node.Type;
        if (innerType.Kind != ast.KindArrayType && innerType.Kind != ast.KindTupleType) {
            return Ꮡc.grammarErrorOnFirstToken(Ꮡnode.of(ast.TypeOperatorNode.ᏑTypeNodeBase).of(ast.TypeNodeBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_readonly_type_modifier_is_only_permitted_on_array_and_tuple_literal_types, scanner.TokenToString(ast.KindSymbolKeyword));
        }
    }

    return false;
}

internal static bool checkGrammarForInvalidDynamicName(this ж<Checker> Ꮡc, ж<astꓸDeclarationName> Ꮡnode, ж<diagnostics.Message> message) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!Ꮡc.isNonBindableDynamicName(Ꮡnode)) {
        return false;
    }
    ж<ast.Node> expression = default;
    if (ast.IsElementAccessExpression(Ꮡnode)) {
        expression = ast.SkipParentheses(node.AsElementAccessExpression().ArgumentExpression);
    } else {
        expression = node.Expression();
    }

    if (!ast.IsEntityNameExpression(expression)) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, message);
    }

    return false;
}

internal static bool isNonBindableDynamicName(this ж<Checker> Ꮡc, ж<astꓸDeclarationName> node) {
    return ast.IsDynamicName(node) && !Ꮡc.isLateBindableName(node);
}

internal static bool checkGrammarMethod(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡc.checkGrammarFunctionLikeDeclaration(Ꮡnode)) {
        return true;
    }

    if (node.Kind == ast.KindMethodDeclaration) {
        if (node.Parent.Kind == ast.KindObjectLiteralExpression) {
            var modifiers = node.Modifiers(); if (modifiers != nil && !(len(modifiers.Nodes) == 1 && modifiers.Nodes[0].Kind == ast.KindAsyncKeyword)) {
                return Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.Modifiers_cannot_appear_here);
            }

            var methodDecl = node.AsMethodDeclaration();
            if (Ꮡc.checkGrammarForInvalidQuestionMark((~methodDecl).PostfixToken, diagnostics.An_object_member_cannot_be_declared_optional)) {
                return true;
            }
            if (Ꮡc.checkGrammarForInvalidExclamationToken((~methodDecl).PostfixToken, diagnostics.A_definite_assignment_assertion_is_not_permitted_in_this_context)) {
                return true;
            }
            if (node.Body() == nil) {
                return Ꮡc.grammarErrorAtPos(Ꮡnode, node.End() - 1, len(";"), diagnostics.X_0_expected, (@string)"{");
            }
        }
        if (Ꮡc.checkGrammarForGenerator(Ꮡnode)) {
            return true;
        }
    }

    if (ast.IsClassLike(node.Parent)) {
        if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0) {
            return Ꮡc.checkGrammarForInvalidDynamicName(node.Name(), diagnostics.A_computed_property_name_in_an_ambient_context_must_refer_to_an_expression_whose_type_is_a_literal_type_or_a_unique_symbol_type);
        } else if (node.Kind == ast.KindMethodDeclaration && node.Body() == nil) {
            return Ꮡc.checkGrammarForInvalidDynamicName(node.Name(), diagnostics.A_computed_property_name_in_a_method_overload_must_refer_to_an_expression_whose_type_is_a_literal_type_or_a_unique_symbol_type);
        }
    } else if (node.Parent.Kind == ast.KindInterfaceDeclaration) {
        return Ꮡc.checkGrammarForInvalidDynamicName(node.Name(), diagnostics.A_computed_property_name_in_an_interface_must_refer_to_an_expression_whose_type_is_a_literal_type_or_a_unique_symbol_type);
    } else if (node.Parent.Kind == ast.KindTypeLiteral) {
        return Ꮡc.checkGrammarForInvalidDynamicName(node.Name(), diagnostics.A_computed_property_name_in_a_type_literal_must_refer_to_an_expression_whose_type_is_a_literal_type_or_a_unique_symbol_type);
    }

    return false;
}

internal static bool checkGrammarBreakOrContinueStatement(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var targetLabel = node.Label();
    ж<ast.Node> current = Ꮡnode;
    while (current != nil) {
        if (ast.IsFunctionLikeOrClassStaticBlockDeclaration(current)) {
            return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.Jump_target_cannot_cross_function_boundary);
        }

        switch (current.Kind) {
        case var ᴛ when ᴛ == ast.KindLabeledStatement: {
            if (targetLabel != nil && current.Label().Text() == targetLabel.Text()) {
                var isMisplacedContinueLabel = node.Kind == ast.KindContinueStatement && !ast.IsIterationStatement(current.Statement(), true);

                if (isMisplacedContinueLabel) {
                    return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.A_continue_statement_can_only_jump_to_a_label_of_an_enclosing_iteration_statement);
                }

                return false;
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindSwitchStatement: {
            if (node.Kind == ast.KindBreakStatement && targetLabel == nil) {
                return false;
            }
            break;
        }
        default: {
            if (ast.IsIterationStatement(current, false) && targetLabel == nil) {
                return false;
            }
            break;
        }}

        current = current.Parent;
    }

    if (targetLabel != nil) {
        ж<diagnostics.Message> message = default;
        if (node.Kind == ast.KindBreakStatement) {
            message = diagnostics.A_break_statement_can_only_jump_to_a_label_of_an_enclosing_statement;
        } else {
            message = diagnostics.A_continue_statement_can_only_jump_to_a_label_of_an_enclosing_iteration_statement;
        }

        return Ꮡc.grammarErrorOnNode(Ꮡnode, message);
    } else {
        ж<diagnostics.Message> message = default;
        if (node.Kind == ast.KindBreakStatement) {
            message = diagnostics.A_break_statement_can_only_be_used_within_an_enclosing_iteration_or_switch_statement;
        } else {
            message = diagnostics.A_continue_statement_can_only_be_used_within_an_enclosing_iteration_statement;
        }
        return Ꮡc.grammarErrorOnNode(Ꮡnode, message);
    }
}

internal static bool checkGrammarBindingElement(this ж<Checker> Ꮡc, ж<ast.BindingElement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.DotDotDotToken != nil) {
        var elements = node.Parent.ElementList();
        if (Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode() != core.LastOrNil(elements.Nodes)) {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.BindingElement.ᏑNode), diagnostics.A_rest_element_must_be_last_in_a_destructuring_pattern);
        }
        Ꮡc.checkGrammarForDisallowedTrailingComma(elements, diagnostics.A_rest_parameter_or_binding_pattern_may_not_have_a_trailing_comma);

        if (node.PropertyName != nil) {
            return Ꮡc.grammarErrorOnNode(node.Name(), diagnostics.A_rest_element_cannot_have_a_property_name);
        }
    }

    if (node.DotDotDotToken != nil && node.Initializer != nil) {
        return Ꮡc.grammarErrorAtPos(Ꮡnode.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), node.Initializer.Pos() - 1, 1, diagnostics.A_rest_element_cannot_have_an_initializer);
    }

    return false;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly object awaitUsingˢ = (@string)"await using"u8;
internal static readonly object usingˢ = (@string)"using"u8;
internal static readonly object constˢ = (@string)"const"u8;

internal static bool checkGrammarVariableDeclaration(this ж<Checker> Ꮡc, ж<ast.VariableDeclaration> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var nodeFlags = c.getCombinedNodeFlagsCached(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var blockScopeKind = (ast.NodeFlags)(nodeFlags & ast.NodeFlagsBlockScoped);
    if (ast.IsBindingPattern(node.Name())) {
        switch (blockScopeKind) {
        case var ᴛ when ᴛ == ast.NodeFlagsAwaitUsing: {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_0_declarations_may_not_have_binding_patterns, awaitUsingˢ);
        }
        case var ᴛ when ᴛ == ast.NodeFlagsUsing: {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_0_declarations_may_not_have_binding_patterns, usingˢ);
        }}

    }

    if (node.Parent.Parent.Kind != ast.KindForInStatement && node.Parent.Parent.Kind != ast.KindForOfStatement) {
        if ((ast.NodeFlags)(nodeFlags & ast.NodeFlagsAmbient) != 0) {
            Ꮡc.checkAmbientInitializer(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
        } else if (node.Initializer == nil) {
            if (ast.IsBindingPattern(node.Name()) && !ast.IsBindingPattern(node.Parent)) {
                return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.A_destructuring_declaration_must_have_an_initializer);
            }
            switch (blockScopeKind) {
            case var ᴛ when ᴛ == ast.NodeFlagsAwaitUsing: {
                return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_0_declarations_must_be_initialized, awaitUsingˢ);
            }
            case var ᴛ when ᴛ == ast.NodeFlagsUsing: {
                return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_0_declarations_must_be_initialized, usingˢ);
            }
            case var ᴛ when ᴛ == ast.NodeFlagsConst: {
                return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_0_declarations_must_be_initialized, constˢ);
            }}

        }
    }

    if (node.ExclamationToken != nil && (node.Parent.Parent.Kind != ast.KindVariableStatement || node.Type == nil || node.Initializer != nil || (ast.NodeFlags)(nodeFlags & ast.NodeFlagsAmbient) != 0)) {
        ж<diagnostics.Message> message = default;
        switch (ᐧ) {
        case {} when node.Initializer != nil: {
            message = diagnostics.Declarations_with_initializers_cannot_also_have_definite_assignment_assertions;
            break;
        }
        case {} when node.Type == nil: {
            message = diagnostics.Declarations_with_definite_assignment_assertions_must_also_have_type_annotations;
            break;
        }
        default: {
            message = diagnostics.A_definite_assignment_assertion_is_not_permitted_in_this_context;
            break;
        }}

        return Ꮡc.grammarErrorOnNode(node.ExclamationToken, message);
    }

    if (c.program.GetEmitModuleFormatOfFile(new ast.SourceFileжHasFileName(ast.GetSourceFileOfNode(Ꮡnode.of(ast.VariableDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))) < core.ModuleKindSystem && ((ast.NodeFlags)(node.Parent.Parent.Flags & ast.NodeFlagsAmbient) == 0) && ast.HasSyntacticModifier(node.Parent.Parent, ast.ModifierFlagsExport)) {
        Ꮡc.checkGrammarForEsModuleMarkerInBindingName(node.Name());
    }

    return blockScopeKind != 0 && Ꮡc.checkGrammarNameInLetOrConstDeclarations(node.Name());
}

internal static bool checkGrammarForEsModuleMarkerInBindingName(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (ast.IsIdentifier(Ꮡname)) {
        if (name.Text() == "__esModule"u8) {
            return Ꮡc.grammarErrorOnNodeSkippedOnNoEmit(Ꮡname, diagnostics.Identifier_expected_esModule_is_reserved_as_an_exported_marker_when_transforming_ECMAScript_modules);
        }
    } else {
        foreach (var (_, element) in range(name.Elements())) {
            if (element.Name() != nil) {
                return Ꮡc.checkGrammarForEsModuleMarkerInBindingName(element.Name());
            }
        }
    }
    return false;
}

internal static bool checkGrammarNameInLetOrConstDeclarations(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (name.Kind == ast.KindIdentifier) {
        if (name.Text() == "let"u8) {
            return Ꮡc.grammarErrorOnNode(Ꮡname, diagnostics.X_let_is_not_allowed_to_be_used_as_a_name_in_let_or_const_declarations);
        }
    } else {
        var elements = name.Elements();
        foreach (var (_, element) in range(elements)) {
            var bindingElement = element.AsBindingElement();
            if (bindingElement.Name() != nil) {
                Ꮡc.checkGrammarNameInLetOrConstDeclarations(bindingElement.Name());
            }
        }
    }
    return false;
}

internal static bool checkGrammarVariableDeclarationList(this ж<Checker> Ꮡc, ж<ast.VariableDeclarationList> ᏑdeclarationList) {
    ref var declarationList = ref ᏑdeclarationList.DerefOrNull();

    var declarations = declarationList.Declarations;
    if (Ꮡc.checkGrammarForDisallowedTrailingComma(declarations, diagnostics.Trailing_comma_not_allowed)) {
        return true;
    }

    if (len(declarations.Nodes) == 0) {
        return Ꮡc.grammarErrorAtPos(ᏑdeclarationList.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), declarations.Pos(), declarations.End() - declarations.Pos(), diagnostics.Variable_declaration_list_cannot_be_empty);
    }

    var blockScopeFlags = (ast.NodeFlags)(declarationList.Flags & ast.NodeFlagsBlockScoped);
    if (blockScopeFlags == ast.NodeFlagsUsing || blockScopeFlags == ast.NodeFlagsAwaitUsing) {
        if (ast.IsForInStatement(declarationList.Parent)) {
            return Ꮡc.grammarErrorOnNode(ᏑdeclarationList.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.IfElse(blockScopeFlags == ast.NodeFlagsUsing, diagnostics.The_left_hand_side_of_a_for_in_statement_cannot_be_a_using_declaration, diagnostics.The_left_hand_side_of_a_for_in_statement_cannot_be_an_await_using_declaration));
        }
        if ((ast.NodeFlags)(declarationList.Flags & ast.NodeFlagsAmbient) != 0) {
            return Ꮡc.grammarErrorOnNode(ᏑdeclarationList.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.IfElse(blockScopeFlags == ast.NodeFlagsUsing, diagnostics.X_using_declarations_are_not_allowed_in_ambient_contexts, diagnostics.X_await_using_declarations_are_not_allowed_in_ambient_contexts));
        }
        if (ast.IsVariableStatement(declarationList.Parent) && (ast.IsCaseClause(declarationList.Parent.Parent) || ast.IsDefaultClause(declarationList.Parent.Parent))) {
            return Ꮡc.grammarErrorOnNode(ᏑdeclarationList.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), core.IfElse(blockScopeFlags == ast.NodeFlagsUsing, diagnostics.X_using_declarations_are_not_allowed_in_case_or_default_clauses_unless_contained_within_a_block, diagnostics.X_await_using_declarations_are_not_allowed_in_case_or_default_clauses_unless_contained_within_a_block));
        }
    }

    if (blockScopeFlags == ast.NodeFlagsAwaitUsing) {
        return Ꮡc.checkGrammarAwaitOrAwaitUsing(ᏑdeclarationList.of(ast.VariableDeclarationList.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    }

    return false;
}

internal static bool checkGrammarAwaitOrAwaitUsing(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var hasError = false;
    var container = getContainingFunctionOrClassStaticBlock(ref (Ꮡnode).DerefOrNull());
    if (container != nil && ast.IsClassStaticBlockDeclaration(container)) {
        ж<diagnostics.Message> message = default;
        if (ast.IsAwaitExpression(Ꮡnode)) {
            message = diagnostics.X_await_expression_cannot_be_used_inside_a_class_static_block;
        } else {
            message = diagnostics.X_await_using_statements_cannot_be_used_inside_a_class_static_block;
        }
        Ꮡc.error(Ꮡnode, message);
        hasError = true;
    } else if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAwaitContext) == 0) {
        if (ast.IsInTopLevelContext(Ꮡnode)) {
            var sourceFile = ast.GetSourceFileOfNode(Ꮡnode);
            if (!c.hasParseDiagnostics(sourceFile)) {
                core.TextRange span = default;
                bool spanCalculated = default;
                if (!ast.IsEffectiveExternalModule(sourceFile, c.compilerOptions)) {
                    span = scanner.GetRangeOfTokenAtPosition(sourceFile, node.Pos());
                    spanCalculated = true;
                    ж<diagnostics.Message> message = default;
                    if (ast.IsAwaitExpression(Ꮡnode)) {
                        message = diagnostics.X_await_expressions_are_only_allowed_at_the_top_level_of_a_file_when_that_file_is_a_module_but_this_file_has_no_imports_or_exports_Consider_adding_an_empty_export_to_make_this_file_a_module;
                    } else {
                        message = diagnostics.X_await_using_statements_are_only_allowed_at_the_top_level_of_a_file_when_that_file_is_a_module_but_this_file_has_no_imports_or_exports_Consider_adding_an_empty_export_to_make_this_file_a_module;
                    }
                    var diagnostic = ast.NewDiagnostic(sourceFile, span, message);
                    Ꮡc.addDiagnostic(diagnostic);
                    hasError = true;
                }
                var exprᴛ1 = c.moduleKind;
                var matchᴛ1 = false;
                if (exprᴛ1 == core.ModuleKindNode16 || exprᴛ1 == core.ModuleKindNode18 || exprᴛ1 == core.ModuleKindNode20 || exprᴛ1 == core.ModuleKindNodeNext) { matchᴛ1 = true;
                    do {
                        var sourceFileMetaData = c.program.GetSourceFileMetaData(sourceFile.Path());
                        if (sourceFileMetaData.ImpliedNodeFormat == core.ModuleKindCommonJS) {
                            if (!spanCalculated) {
                                span = scanner.GetRangeOfTokenAtPosition(sourceFile, node.Pos());
                            }
                            Ꮡc.addDiagnostic(ast.NewDiagnostic(sourceFile, span, diagnostics.The_current_file_is_a_CommonJS_module_and_cannot_use_await_at_the_top_level));
                            hasError = true;
                            break;
                        }
                        fallthrough = true;
                    } while (false);
                }
                if (fallthrough || !matchᴛ1 && (exprᴛ1 == core.ModuleKindES2022 || exprᴛ1 == core.ModuleKindESNext || exprᴛ1 == core.ModuleKindPreserve || exprᴛ1 == core.ModuleKindSystem)) { matchᴛ1 = true;
                    do {
                        if (c.languageVersion >= core.ScriptTargetES2017) {
                            break;
                        }
                        fallthrough = true;
                    } while (false);
                }
                if (fallthrough || !matchᴛ1) { /* default: */
                    if (!spanCalculated) {
                        span = scanner.GetRangeOfTokenAtPosition(sourceFile, node.Pos());
                    }
                    ж<diagnostics.Message> message = default;
                    if (ast.IsAwaitExpression(Ꮡnode)) {
                        message = diagnostics.Top_level_await_expressions_are_only_allowed_when_the_module_option_is_set_to_es2022_esnext_system_node16_node18_node20_nodenext_or_preserve_and_the_target_option_is_set_to_es2017_or_higher;
                    } else {
                        message = diagnostics.Top_level_await_using_statements_are_only_allowed_when_the_module_option_is_set_to_es2022_esnext_system_node16_node18_node20_nodenext_or_preserve_and_the_target_option_is_set_to_es2017_or_higher;
                    }
                    Ꮡc.addDiagnostic(ast.NewDiagnostic(sourceFile, span, message));
                    hasError = true;
                }

            }
        } else {
            var sourceFile = ast.GetSourceFileOfNode(Ꮡnode);
            if (!c.hasParseDiagnostics(sourceFile)) {
                var span = scanner.GetRangeOfTokenAtPosition(sourceFile, node.Pos());
                ж<diagnostics.Message> message = default;
                if (ast.IsAwaitExpression(Ꮡnode)) {
                    message = diagnostics.X_await_expressions_are_only_allowed_within_async_functions_and_at_the_top_levels_of_modules;
                } else {
                    message = diagnostics.X_await_using_statements_are_only_allowed_within_async_functions_and_at_the_top_levels_of_modules;
                }
                var diagnostic = ast.NewDiagnostic(sourceFile, span, message);
                if (container != nil && container.Kind != ast.KindConstructor && !hasAsyncModifier(container)) {
                    var relatedInfo = NewDiagnosticForNode(container, diagnostics.Did_you_mean_to_mark_this_function_as_async);
                    diagnostic.AddRelatedInfo(relatedInfo);
                }
                Ꮡc.addDiagnostic(diagnostic);
                hasError = true;
            }
        }
    }

    if (ast.IsAwaitExpression(Ꮡnode) && c.isInParameterInitializerBeforeContainingFunction(Ꮡnode)) {
        Ꮡc.error(Ꮡnode, diagnostics.X_await_expressions_cannot_be_used_in_a_parameter_initializer);
        hasError = true;
    }

    return hasError;
}

internal static bool checkGrammarYieldExpression(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var hasError = false;
    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsYieldContext) == 0) {
        Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.A_yield_expression_is_only_allowed_in_a_generator_body);
        hasError = true;
    }
    if (c.isInParameterInitializerBeforeContainingFunction(Ꮡnode)) {
        Ꮡc.error(Ꮡnode, diagnostics.X_yield_expressions_cannot_be_used_in_a_parameter_initializer);
        hasError = true;
    }
    return hasError;
}

internal static bool checkGrammarForDisallowedBlockScopedVariableStatement(this ж<Checker> Ꮡc, ж<ast.VariableStatement> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!c.containerAllowsBlockScopedVariable(node.Parent)) {
        var blockScopeKind = (ast.NodeFlags)(c.getCombinedNodeFlagsCached(node.DeclarationList) & ast.NodeFlagsBlockScoped);
        if (blockScopeKind != 0) {
            @string keyword = default;
            switch (ᐧ) {
            case {} when blockScopeKind == ast.NodeFlagsLet: {
                keyword = "let";
                break;
            }
            case {} when blockScopeKind == ast.NodeFlagsConst: {
                keyword = "const";
                break;
            }
            case {} when blockScopeKind == ast.NodeFlagsUsing: {
                keyword = "using";
                break;
            }
            case {} when blockScopeKind == ast.NodeFlagsAwaitUsing: {
                keyword = "await using";
                break;
            }
            default: {
                throw panic("Unknown BlockScope flag");
                break;
            }}

            Ꮡc.error(Ꮡnode.of(ast.VariableStatement.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.X_0_declarations_can_only_be_declared_inside_a_block, keyword);
        }
    }

    return false;
}

internal static bool containerAllowsBlockScopedVariable(this ref Checker c, ж<ast.Node> Ꮡparent) {
    ref var parent = ref Ꮡparent.DerefOrNull();

    switch (parent.Kind) {
    case var ᴛ when ᴛ == ast.KindIfStatement || ᴛ == ast.KindDoStatement || ᴛ == ast.KindWhileStatement || ᴛ == ast.KindWithStatement || ᴛ == ast.KindForStatement || ᴛ == ast.KindForInStatement || ᴛ == ast.KindForOfStatement: {
        return false;
    }
    case var ᴛ when ᴛ == ast.KindLabeledStatement: {
        return c.containerAllowsBlockScopedVariable(parent.Parent);
    }}

    return true;
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly object targetˢ = (@string)"target"u8;
internal static readonly object metaˢ = (@string)"meta"u8;

internal static bool checkGrammarMetaProperty(this ж<Checker> Ꮡc, ж<ast.MetaProperty> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var nodeName = node.Name();
    @string nameText = nodeName.Text();

    switch (node.KeywordToken) {
    case var ᴛ when ᴛ == ast.KindNewKeyword: {
        if (nameText != "target"u8) {
            return Ꮡc.grammarErrorOnNode(nodeName, diagnostics.X_0_is_not_a_valid_meta_property_for_keyword_1_Did_you_mean_2, nameText, scanner.TokenToString(node.KeywordToken), targetˢ);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindImportKeyword: {
        if (nameText != "meta"u8) {
            var isCallee = ast.IsCallExpression(node.Parent) && node.Parent.Expression() == Ꮡnode.of(ast.MetaProperty.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
            if (nameText == "defer"u8) {
                if (!isCallee) {
                    return Ꮡc.grammarErrorAtPos(Ꮡnode.of(ast.MetaProperty.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(ast.MetaProperty.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode().End(), 0, diagnostics.X_0_expected, (@string)"(");
                }
            } else {
                if (isCallee) {
                    return Ꮡc.grammarErrorOnNode(nodeName, diagnostics.X_0_is_not_a_valid_meta_property_for_keyword_import_Did_you_mean_meta_or_defer, nameText);
                }
                return Ꮡc.grammarErrorOnNode(nodeName, diagnostics.X_0_is_not_a_valid_meta_property_for_keyword_1_Did_you_mean_2, nameText, scanner.TokenToString(node.KeywordToken), metaˢ);
            }
        }
        break;
    }}

    return false;
}

internal static bool checkGrammarConstructorTypeParameters(this ж<Checker> Ꮡc, ж<ast.ConstructorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var range_ = node.TypeParameters;
    if (range_ != nil) {
        nint pos = default;
        if (range_.Pos() == range_.End()) {
            pos = range_.Pos();
        } else {
            pos = scanner.SkipTrivia(ast.GetSourceFileOfNode(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()).Text(), range_.Pos());
        }
        return Ꮡc.grammarErrorAtPos(Ꮡnode.of(ast.ConstructorDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), pos, range_.End() - pos, diagnostics.Type_parameters_cannot_appear_on_a_constructor_declaration);
    }

    return false;
}

internal static bool checkGrammarConstructorTypeAnnotation(this ж<Checker> Ꮡc, ж<ast.ConstructorDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var t = node.Type;
    if (t != nil) {
        return Ꮡc.grammarErrorOnNode(t, diagnostics.Type_annotation_cannot_appear_on_a_constructor_declaration);
    }
    return false;
}

internal static bool checkGrammarProperty(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var propertyName = node.Name();
    if (ast.IsComputedPropertyName(propertyName) && ast.IsBinaryExpression(propertyName.Expression()) && propertyName.Expression().AsBinaryExpression().OperatorToken.Kind == ast.KindInKeyword) {
        return Ꮡc.grammarErrorOnNode(node.Parent.Members()[0], diagnostics.A_mapped_type_may_not_declare_properties_or_methods);
    }
    if (ast.IsClassLike(node.Parent)) {
        if (ast.IsStringLiteral(propertyName) && propertyName.Text() == "constructor"u8) {
            return Ꮡc.grammarErrorOnNode(propertyName, diagnostics.Classes_may_not_have_a_field_named_constructor);
        }
        if (Ꮡc.checkGrammarForInvalidDynamicName(propertyName, diagnostics.A_computed_property_name_in_a_class_property_declaration_must_have_a_simple_literal_type_or_a_unique_symbol_type)) {
            return true;
        }
        if (ast.IsAutoAccessorPropertyDeclaration(Ꮡnode) && Ꮡc.checkGrammarForInvalidQuestionMark(node.PostfixToken(), diagnostics.An_accessor_property_cannot_be_declared_optional)) {
            return true;
        }
    } else if (ast.IsInterfaceDeclaration(node.Parent)) {
        if (Ꮡc.checkGrammarForInvalidDynamicName(propertyName, diagnostics.A_computed_property_name_in_an_interface_must_refer_to_an_expression_whose_type_is_a_literal_type_or_a_unique_symbol_type)) {
            return true;
        }
        if (!ast.IsPropertySignatureDeclaration(Ꮡnode)) {
            throw panic(fmt.Sprintf("Unexpected node kind %q"u8, node.Kind));
        }
        var initializer = node.Initializer(); if (initializer != nil) {
            return Ꮡc.grammarErrorOnNode(initializer, diagnostics.An_interface_property_cannot_have_an_initializer);
        }
    } else if (ast.IsTypeLiteralNode(node.Parent)) {
        if (Ꮡc.checkGrammarForInvalidDynamicName(node.Name(), diagnostics.A_computed_property_name_in_a_type_literal_must_refer_to_an_expression_whose_type_is_a_literal_type_or_a_unique_symbol_type)) {
            return true;
        }
        if (!ast.IsPropertySignatureDeclaration(Ꮡnode)) {
            throw panic(fmt.Sprintf("Unexpected node kind %q"u8, node.Kind));
        }
        var initializer = node.Initializer(); if (initializer != nil) {
            return Ꮡc.grammarErrorOnNode(initializer, diagnostics.A_type_literal_property_cannot_have_an_initializer);
        }
    }

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0) {
        Ꮡc.checkAmbientInitializer(Ꮡnode);
    }

    if (ast.IsPropertyDeclaration(Ꮡnode)) {
        var propDecl = node.AsPropertyDeclaration();
        var postfixToken = propDecl.Value.PostfixToken;
        if (postfixToken != nil && postfixToken.Kind == ast.KindExclamationToken) {
            switch (ᐧ) {
            case {} when (~propDecl).Initializer != nil: {
                return Ꮡc.grammarErrorOnNode(postfixToken, diagnostics.Declarations_with_initializers_cannot_also_have_definite_assignment_assertions);
            }
            case {} when (~propDecl).Type == nil: {
                return Ꮡc.grammarErrorOnNode(postfixToken, diagnostics.Declarations_with_definite_assignment_assertions_must_also_have_type_annotations);
            }
            case {} when !ast.IsClassLike(node.Parent) || (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0 || ast.IsStatic(Ꮡnode) || ast.HasAbstractModifier(Ꮡnode): {
                return Ꮡc.grammarErrorOnNode(postfixToken, diagnostics.A_definite_assignment_assertion_is_not_permitted_in_this_context);
            }}

        }
    }

    return false;
}

internal static bool checkAmbientInitializer(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    ж<astꓸExpression> initializer = default;
    ж<astꓸTypeNode> typeNode = default;
    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        var varDecl = node.AsVariableDeclaration();
        initializer = varDecl.Value.Initializer;
        typeNode = varDecl.Value.Type;
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        var propDecl = node.AsPropertyDeclaration();
        initializer = propDecl.Value.Initializer;
        typeNode = propDecl.Value.Type;
        break;
    }
    case var ᴛ when ᴛ == ast.KindPropertySignature: {
        var propSig = node.AsPropertySignatureDeclaration();
        initializer = propSig.Value.Initializer;
        typeNode = propSig.Value.Type;
        break;
    }
    default: {
        throw panic(fmt.Sprintf("Unexpected node kind %q"u8, node.Kind));
        break;
    }}

    if (initializer != nil) {
        var isInvalidInitializer = !(isInitializerStringOrNumberLiteralExpression(initializer) || Ꮡc.isInitializerSimpleLiteralEnumReference(initializer) || initializer.Kind == ast.KindTrueKeyword || initializer.Kind == ast.KindFalseKeyword || isInitializerBigIntLiteralExpression(initializer));
        var isConstOrReadonly = isDeclarationReadonly(Ꮡnode) || ast.IsVariableDeclaration(Ꮡnode) && c.isVarConstLike(Ꮡnode);
        if (isConstOrReadonly && (typeNode == nil)) {
            if (isInvalidInitializer) {
                return Ꮡc.grammarErrorOnNode(initializer, diagnostics.A_const_initializer_in_an_ambient_context_must_be_a_string_or_numeric_literal_or_literal_enum_reference);
            }
        } else {
            return Ꮡc.grammarErrorOnNode(initializer, diagnostics.Initializers_are_not_allowed_in_ambient_contexts);
        }
    }

    return false;
}

} partial class checker_package {

internal static bool isInitializerStringOrNumberLiteralExpression(ж<astꓸExpression> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    return ast.IsStringOrNumericLiteralLike(Ꮡexpr) || expr.Kind == ast.KindPrefixUnaryExpression && expr.AsPrefixUnaryExpression().Operator == ast.KindMinusToken && expr.AsPrefixUnaryExpression().Operand.Kind == ast.KindNumericLiteral;
}

internal static bool isInitializerBigIntLiteralExpression(ж<astꓸExpression> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    if (expr.Kind == ast.KindBigIntLiteral) {
        return true;
    }

    if (expr.Kind == ast.KindPrefixUnaryExpression) {
        var unaryExpr = expr.AsPrefixUnaryExpression();
        return unaryExpr.Operator == ast.KindMinusToken && unaryExpr.Operand.Kind == ast.KindBigIntLiteral;
    }

    return false;
}

} partial class checkerᴛChecker {

internal static bool isInitializerSimpleLiteralEnumReference(this ж<Checker> Ꮡc, ж<astꓸExpression> Ꮡexpr) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    if (ast.IsPropertyAccessExpression(Ꮡexpr)) {
        return (TypeFlags)(Ꮡc.checkExpressionCached(Ꮡexpr).flags & TypeFlagsEnumLike) != 0;
    }

    if (ast.IsElementAccessExpression(Ꮡexpr)) {
        var elementAccess = expr.AsElementAccessExpression();

        return isInitializerStringOrNumberLiteralExpression(elementAccess.ArgumentExpression) && ast.IsEntityNameExpression((~elementAccess).Expression) && (TypeFlags)(Ꮡc.checkExpressionCached(Ꮡexpr).flags & TypeFlagsEnumLike) != 0;
    }

    return false;
}

internal static bool checkGrammarTopLevelElementForRequiredDeclareModifier(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindInterfaceDeclaration || node.Kind == ast.KindTypeAliasDeclaration || node.Kind == ast.KindImportDeclaration || node.Kind == ast.KindJSImportDeclaration || node.Kind == ast.KindImportEqualsDeclaration || node.Kind == ast.KindExportDeclaration || node.Kind == ast.KindExportAssignment || node.Kind == ast.KindNamespaceExportDeclaration || ast.HasSyntacticModifier(Ꮡnode, (ast.ModifierFlags)((ast.ModifierFlags)(ast.ModifierFlagsAmbient | ast.ModifierFlagsExport) | ast.ModifierFlagsDefault))) {
        return false;
    }

    return Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.Top_level_declarations_in_d_ts_files_must_start_with_either_a_declare_or_export_modifier);
}

internal static bool checkGrammarTopLevelElementsForRequiredDeclareModifier(this ж<Checker> Ꮡc, ж<ast.SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    foreach (var (_, decl) in range(@file.Statements.Nodes)) {
        if (ast.IsDeclarationNode(decl) || decl.Kind == ast.KindVariableStatement) {
            if (Ꮡc.checkGrammarTopLevelElementForRequiredDeclareModifier(decl)) {
                return true;
            }
        }
    }
    return false;
}

internal static bool checkGrammarSourceFile(this ж<Checker> Ꮡc, ж<ast.SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return (ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0 && Ꮡc.checkGrammarTopLevelElementsForRequiredDeclareModifier(Ꮡnode);
}

internal static bool checkGrammarStatementInAmbientContext(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) != 0) {
        var links = c.nodeLinks.Get(Ꮡnode);
        if (!links.hasReportedStatementInAmbientContext && (ast.IsFunctionLike(node.Parent) || ast.IsAccessor(node.Parent))) {
            links.hasReportedStatementInAmbientContext = Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.An_implementation_cannot_be_declared_in_ambient_contexts);
            return links.hasReportedStatementInAmbientContext;
        }

        if (node.Parent.Kind == ast.KindBlock || node.Parent.Kind == ast.KindModuleBlock || node.Parent.Kind == ast.KindSourceFile) {
            var linksΔ1 = c.nodeLinks.Get(node.Parent);
            if (!linksΔ1.hasReportedStatementInAmbientContext) {
                linksΔ1.hasReportedStatementInAmbientContext = Ꮡc.grammarErrorOnFirstToken(Ꮡnode, diagnostics.Statements_are_not_allowed_in_ambient_contexts);
                return linksΔ1.hasReportedStatementInAmbientContext;
            }
        } else {
        }
    }
    return false;
}

internal static void checkGrammarNumericLiteral(this ж<Checker> Ꮡc, ж<ast.NumericLiteral> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    @string nodeText = scanner.GetTextOfNode(Ꮡnode.of(ast.NumericLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());

    var isFractional = strings.ContainsRune(nodeText, '.');
    var isScientific = (ast.TokenFlags)(node.TokenFlags & ast.TokenFlagsScientific) != 0;

    if (isFractional || isScientific) {
        return;
    }

    var value = jsnum.FromString(node.Text);
    if (value <= jsnum.MaxSafeInteger) {
        return;
    }

    Ꮡc.addErrorOrSuggestion(false, createDiagnosticForNode(Ꮡnode.of(ast.NumericLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.Numeric_literals_with_absolute_values_equal_to_2_53_or_greater_are_too_large_to_be_represented_accurately_as_integers));
}

internal static bool checkGrammarBigIntLiteral(this ж<Checker> Ꮡc, ж<ast.BigIntLiteral> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    var literalType = ast.IsLiteralTypeNode(node.Parent) || ast.IsPrefixUnaryExpression(node.Parent) && ast.IsLiteralTypeNode(node.Parent.Parent);
    if (!literalType) {
        if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsAmbient) == 0 && c.languageVersion < core.ScriptTargetES2020) {
            if (Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.BigIntLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.BigInt_literals_are_not_available_when_targeting_lower_than_ES2020)) {
                return true;
            }
        }
    }
    return false;
}

internal static bool checkGrammarImportClause(this ж<Checker> Ꮡc, ж<ast.ImportClause> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.PhaseModifier) {
    case var ᴛ when ᴛ == ast.KindTypeKeyword: {
        if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsJSDoc) == 0 && node.Name() != nil && node.NamedBindings != nil) {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.ImportClause.ᏑNode), diagnostics.A_type_only_import_can_specify_a_default_import_or_named_bindings_but_not_both);
        }
        if (node.NamedBindings != nil && node.NamedBindings.Kind == ast.KindNamedImports) {
            return Ꮡc.checkGrammarTypeOnlyNamedImportsOrExports(node.NamedBindings);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindDeferKeyword: {
        if (node.Name() != nil) {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.ImportClause.ᏑNode), diagnostics.Default_imports_are_not_allowed_in_a_deferred_import);
        }
        if (node.NamedBindings != nil && node.NamedBindings.Kind == ast.KindNamedImports) {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.ImportClause.ᏑNode), diagnostics.Named_imports_are_not_allowed_in_a_deferred_import);
        }
        if (c.moduleKind != core.ModuleKindESNext && c.moduleKind != core.ModuleKindPreserve) {
            return Ꮡc.grammarErrorOnNode(Ꮡnode.of(ast.ImportClause.ᏑNode), diagnostics.Deferred_imports_are_only_supported_when_the_module_flag_is_set_to_esnext_or_preserve);
        }
        break;
    }}

    return false;
}

internal static bool checkGrammarImportAttributeValues(this ж<Checker> Ꮡc, ж<ast.ImportAttributes> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var hasError = false;
    foreach (var (_, attribute) in range(node.Attributes.Nodes)) {
        var value = attribute.AsImportAttribute().Value.Value;
        if (ast.IsStringLiteral(value)) {
            continue;
        }
        hasError = true;
        Ꮡc.error(value, diagnostics.Import_attribute_values_must_be_string_literal_expressions);
    }
    return hasError;
}

internal static bool checkGrammarTypeOnlyNamedImportsOrExports(this ж<Checker> Ꮡc, ж<ast.Node> ᏑnamedBindings) {
    ref var namedBindings = ref ᏑnamedBindings.DerefOrNull();

    var nodeList = namedBindings.ElementList();
    foreach (var (_, specifier) in range(nodeList.Nodes)) {
        bool specifierIsTypeOnly = default;
        ж<diagnostics.Message> message = default;
        if (specifier.Kind == ast.KindImportSpecifier) {
            specifierIsTypeOnly = specifier.IsTypeOnly();
            message = diagnostics.The_type_modifier_cannot_be_used_on_a_named_import_when_import_type_is_used_on_its_import_statement;
        } else {
            specifierIsTypeOnly = specifier.IsTypeOnly();
            message = diagnostics.The_type_modifier_cannot_be_used_on_a_named_export_when_export_type_is_used_on_its_export_statement;
        }

        if (specifierIsTypeOnly) {
            return Ꮡc.grammarErrorOnFirstToken(specifier, message);
        }
    }

    return false;
}

internal static bool checkGrammarImportCallExpression(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (c.compilerOptions.VerbatimModuleSyntax == core.TSTrue && c.moduleKind == core.ModuleKindCommonJS) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, getVerbatimModuleSyntaxErrorMessage(Ꮡnode));
    }

    if (node.Expression().Kind == ast.KindMetaProperty) {
        if (c.moduleKind != core.ModuleKindESNext && c.moduleKind != core.ModuleKindPreserve) {
            return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.Deferred_imports_are_only_supported_when_the_module_flag_is_set_to_esnext_or_preserve);
        }
    } else if (c.moduleKind == core.ModuleKindES2015) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.Dynamic_imports_are_only_supported_when_the_module_flag_is_set_to_es2020_es2022_esnext_commonjs_amd_system_umd_node16_node18_node20_or_nodenext);
    }

    var nodeAsCall = node.AsCallExpression();
    if ((~nodeAsCall).TypeArguments != nil) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.This_use_of_import_is_invalid_import_calls_can_be_written_but_they_must_have_parentheses_and_cannot_have_type_arguments);
    }

    var nodeArguments = nodeAsCall.Value.Arguments;
    var argumentNodes = nodeArguments.Nodes;
    if (!(core.ModuleKindNode16 <= c.moduleKind && c.moduleKind <= core.ModuleKindNodeNext) && c.moduleKind != core.ModuleKindESNext && c.moduleKind != core.ModuleKindPreserve) {
        Ꮡc.checkGrammarForDisallowedTrailingComma(nodeArguments, diagnostics.Trailing_comma_not_allowed);

        if (len(argumentNodes) > 1) {
            var importAttributesArgument = argumentNodes[1];
            return Ꮡc.grammarErrorOnNode(importAttributesArgument, diagnostics.Dynamic_imports_only_support_a_second_argument_when_the_module_option_is_set_to_esnext_node16_node18_node20_nodenext_or_preserve);
        }
    }

    if (len(argumentNodes) == 0 || len(argumentNodes) > 2) {
        return Ꮡc.grammarErrorOnNode(Ꮡnode, diagnostics.Dynamic_imports_can_only_accept_a_module_specifier_and_an_optional_set_of_attributes_as_arguments);
    }

    var spreadElement = core.Find<ж<ast.Node>>(argumentNodes, ast.IsSpreadElement);
    if (spreadElement != nil) {
        return Ꮡc.grammarErrorOnNode(spreadElement, diagnostics.Argument_of_dynamic_import_cannot_be_spread_element);
    }
    return false;
}

internal static bool checkGrammarImportAttributesType(this ж<Checker> Ꮡc, ж<ast.TypeLiteralNode> Ꮡattributes) {
    ref var attributes = ref Ꮡattributes.DerefOrNull();

    var members = attributes.Members;
    if (members == nil) {
        return false;
    }
    foreach (var (_, member) in range(members.Nodes)) {
        if (member.Kind != ast.KindPropertySignature) {
            return Ꮡc.grammarErrorOnNode(member, diagnostics.An_import_attributes_type_may_only_contain_property_signatures);
        }
        var propertySignature = member.AsPropertySignatureDeclaration();
        var modifiers = propertySignature.of(ast.PropertySignatureDeclaration.ᏑNamedMemberBase).Modifiers(); if (modifiers != nil) {
            foreach (var (_, modifier) in range(modifiers.Nodes)) {
                if (modifier.Kind == ast.KindReadonlyKeyword) {
                    return Ꮡc.grammarErrorOnNode(modifier, diagnostics.An_import_attributes_property_cannot_have_a_readonly_modifier);
                }
            }
        }
        if ((~propertySignature).Type == nil) {
            return Ꮡc.grammarErrorOnNode(member, diagnostics.An_import_attributes_property_must_have_a_type_annotation);
        }
        if (propertySignature.of(ast.PropertySignatureDeclaration.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).of(ast.NodeDefault.ᏑNode).QuestionToken() != nil) {
            return Ꮡc.grammarErrorOnNode(member, diagnostics.An_import_attributes_property_cannot_be_optional);
        }
        var name = propertySignature.Name();
        if (!(ast.IsStringLiteralLike(name) || ast.IsIdentifier(name))) {
            return Ꮡc.grammarErrorOnNode(name, diagnostics.An_import_attributes_property_must_have_a_string_literal_or_identifier_name);
        }
        if (name.Text() == "resolution-mode"u8) {
            return Ꮡc.grammarErrorOnNode(name, diagnostics.X_0_is_not_a_valid_key_for_an_import_attributes_type, name.Text());
        }

        var typeNode = propertySignature.Value.Type;
        if (!ast.IsStringLiteralLikeType(typeNode)) {
            return Ꮡc.grammarErrorOnNode(typeNode, diagnostics.An_import_attributes_property_must_have_a_string_literal_type_annotation);
        }
    }
    return false;
}

} // end checkerᴛChecker
