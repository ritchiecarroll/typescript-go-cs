namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸany = Span<any>;
using static binder_package;

partial class binder_package {

partial struct NameResolver {
    public ж<core.CompilerOptions> CompilerOptions;
    public Func<ж<ast.Node>, ж<ast.Symbol>> GetSymbolOfDeclaration;
    public Funcꓸꓸꓸ<ж<ast.Node>, ж<diagnostics.Message>, any, ж<ast.Diagnostic>> Error;
    public ast.SymbolTable Globals;
    public ж<ast.Symbol> ArgumentsSymbol;
    public ж<ast.Symbol> RequireSymbol;
    public Func<ast.SymbolTable, @string, ast.SymbolFlags, ж<ast.Symbol>> Lookup;
    public Action<ж<ast.Symbol>, ast.SymbolFlags> SymbolReferenced;
    public Action<ж<ast.Node>, core.Tristate> SetRequiresScopeChangeCache;
    public Func<ж<ast.Node>, core.Tristate> GetRequiresScopeChangeCache;
    public Func<ж<ast.Node>, @string, ж<ast.Node>, ж<ast.Symbol>, bool> OnPropertyWithInvalidInitializer;
    public Action<ж<ast.Node>, @string, ast.SymbolFlags, ж<diagnostics.Message>> OnFailedToResolveSymbol;
    public Action<ж<ast.Node>, ж<ast.Symbol>, ast.SymbolFlags, ж<ast.Node>, ж<ast.Node>, bool> OnSuccessfullyResolvedSymbol;
}

} partial class binderᴛNameResolver {

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
internal static readonly @string verbatimModuleSyntaxˢ = "verbatimModuleSyntax"u8;
internal static readonly @string isolatedModulesˢ = "isolatedModules"u8;

public static ж<ast.Symbol> Resolve(this ж<NameResolver> Ꮡr, ж<ast.Node> Ꮡlocation, @string name, ast.SymbolFlags meaning, ж<diagnostics.Message> nameNotFoundMessage, bool isUse, bool excludeGlobals) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var location = ref Ꮡlocation.DerefOrNull();

    ж<ast.Symbol> result = default;
    ж<ast.Node> lastLocation = default;
    ж<ast.Node> lastSelfReferenceLocation = default;
    ж<ast.Node> propertyWithInvalidInitializer = default;
    ж<ast.Node> associatedDeclarationForContainingInitializerOrBindingName = default;
    bool withinDeferredContext = default;
    ж<ast.Node> grandparent = default;
    var originalLocation = Ꮡlocation;
    var nameIsConst = name == "const"u8;
loop:
    while (Ꮡlocation != nil) {
        if (nameIsConst && ast.IsConstAssertion(Ꮡlocation)) {
            return default;
        }
        if (ast.IsModuleOrEnumDeclaration(Ꮡlocation) && lastLocation != nil && location.Name() == lastLocation) {
            lastLocation = Ꮡlocation;
            Ꮡlocation = location.Parent; location = ref Ꮡlocation.DerefOrNull();
        }
        var isModuleAttributes = ast.IsModuleDeclaration(Ꮡlocation) && (~location.AsModuleDeclaration()).Attributes != nil && lastLocation == (~location.AsModuleDeclaration()).Attributes;
        var locals = location.Locals();
        if (locals != nil && !ast.IsGlobalSourceFile(Ꮡlocation)) {
            result = r.lookup(locals, name, meaning);
            if (result != nil) {
                var useResult = true;
                if (isModuleAttributes) {
                    useResult = false;
                } else if (ast.IsFunctionLike(Ꮡlocation) && lastLocation != nil && lastLocation != location.Body()) {
                    if ((ast.SymbolFlags)((ast.SymbolFlags)(meaning & result.Flags) & ast.SymbolFlagsType) != 0 && lastLocation.Kind != ast.KindJSDoc) {
                        useResult = (ast.SymbolFlags)(result.Flags & ast.SymbolFlagsTypeParameter) != 0 && ((ast.NodeFlags)(lastLocation.Flags & ast.NodeFlagsSynthesized) != 0 || lastLocation == location.Type() || lastLocation.Kind == ast.KindParameter || lastLocation.Kind == ast.KindJSDocParameterTag || lastLocation.Kind == ast.KindJSDocReturnTag || lastLocation.Kind == ast.KindTypeParameter);
                    }
                    if ((ast.SymbolFlags)((ast.SymbolFlags)(meaning & result.Flags) & ast.SymbolFlagsVariable) != 0) {
                        if (Ꮡr.useOuterVariableScopeInParameter(result, Ꮡlocation, lastLocation)) {
                            useResult = false;
                        } else if ((ast.SymbolFlags)(result.Flags & ast.SymbolFlagsFunctionScopedVariable) != 0) {
                            useResult = lastLocation.Kind == ast.KindParameter || (ast.NodeFlags)(lastLocation.Flags & ast.NodeFlagsSynthesized) != 0 || lastLocation == location.Type() && ast.FindAncestor(result.ValueDeclaration, ast.IsParameterDeclaration) != nil;
                        }
                    }
                } else if (location.Kind == ast.KindConditionalType) {
                    useResult = lastLocation == location.AsConditionalTypeNode().TrueType;
                }
                if (useResult) {
                    goto break_loop;
                }
                result = default;
            }
        }
        withinDeferredContext = withinDeferredContext || getIsDeferredContext(Ꮡlocation, lastLocation);
        var exprᴛ1 = location.Kind;
        var matchᴛ1 = false;
        if (exprᴛ1 == ast.KindSourceFile) { matchᴛ1 = true;
            do {
                if (!ast.IsExternalOrCommonJSModule(location.AsSourceFile())) {
                    break;
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1 && exprᴛ1 == ast.KindModuleDeclaration) { matchᴛ1 = true;
            do {
                if (isModuleAttributes) {
                    break;
                }
                var moduleSymbol = r.getSymbolOfDeclaration(Ꮡlocation);
                if (moduleSymbol == nil) {
                    break;
                }
                var moduleExports = moduleSymbol.Exports;
                if (ast.IsSourceFile(Ꮡlocation) || (ast.IsModuleDeclaration(Ꮡlocation) && (ast.NodeFlags)(location.Flags & ast.NodeFlagsAmbient) != 0 && !ast.IsGlobalScopeAugmentation(Ꮡlocation))) {
                    result = moduleExports[ast.InternalSymbolNameDefault];
                    if (result != nil) {
                        var localSymbol = GetLocalSymbolForExportDefault(result);
                        if (localSymbol != nil && (ast.SymbolFlags)(result.Flags & meaning) != 0 && localSymbol.Name == name) {
                            goto break_loop;
                        }
                        result = default;
                    }
                    var moduleExport = moduleExports[name];
                    if (moduleExport != nil && moduleExport.Flags == ast.SymbolFlagsAlias && (ast.GetDeclarationOfKind(moduleExport, ast.KindExportSpecifier) != nil || ast.GetDeclarationOfKind(moduleExport, ast.KindNamespaceExport) != nil)) {
                        break;
                    }
                }
                if (name != ast.InternalSymbolNameDefault) {
                    result = r.lookup(moduleExports, name, (ast.SymbolFlags)(meaning & ast.SymbolFlagsModuleMember)); if (result != nil) {
                        if (ast.IsSourceFile(Ꮡlocation) && location.AsSourceFile().CommonJSModuleIndicator != nil && (ast.SymbolFlags)(result.Flags & ast.SymbolFlagsType) == 0) {
                            result = default;
                        } else {
                            goto break_loop;
                        }
                    }
                }
            } while (false);
        }
        else if (exprᴛ1 == ast.KindEnumDeclaration) { matchᴛ1 = true;
            do {
                var enumSymbol = r.getSymbolOfDeclaration(Ꮡlocation);
                if (enumSymbol == nil) {
                    break;
                }
                result = r.lookup(enumSymbol.Exports, name, (ast.SymbolFlags)(meaning & ast.SymbolFlagsEnumMember));
                if (result != nil) {
                    if (nameNotFoundMessage != nil && r.CompilerOptions.GetIsolatedModules() && (ast.NodeFlags)(location.Flags & ast.NodeFlagsAmbient) == 0 && ast.GetSourceFileOfNode(Ꮡlocation) != ast.GetSourceFileOfNode(result.ValueDeclaration)) {
                        @string isolatedModulesLikeFlagName = core.IfElse(r.CompilerOptions.VerbatimModuleSyntax == core.TSTrue, verbatimModuleSyntaxˢ, isolatedModulesˢ);
                        r.error(originalLocation, diagnostics.Cannot_access_0_from_another_file_without_qualification_when_1_is_enabled_Use_2_instead,
                            name, isolatedModulesLikeFlagName, enumSymbol.Name + "." + name);
                    }
                    goto break_loop;
                }
            } while (false);
        }
        else if (exprᴛ1 == ast.KindPropertyDeclaration) { matchᴛ1 = true;
            if (!ast.IsStatic(Ꮡlocation)) {
                var ctor = ast.FindConstructorDeclaration(location.Parent);
                if (ctor != nil && ctor.Locals() != nil) {
                    if (r.lookup(ctor.Locals(), name, (ast.SymbolFlags)(meaning & ast.SymbolFlagsValue)) != nil) {
                        propertyWithInvalidInitializer = Ꮡlocation;
                    }
                }
            }
        }
        else if (exprᴛ1 == ast.KindClassDeclaration || exprᴛ1 == ast.KindClassExpression || exprᴛ1 == ast.KindInterfaceDeclaration) { matchᴛ1 = true;
            do {
                result = r.lookup(r.getSymbolOfDeclaration(Ꮡlocation).Members, name, (ast.SymbolFlags)(meaning & ast.SymbolFlagsType));
                if (result != nil) {
                    if (!isTypeParameterSymbolDeclaredInContainer(ref (result).DerefOrNull(), Ꮡlocation)) {
                        result = default;
                        break;
                    }
                    if (lastLocation != nil && ast.IsStatic(lastLocation)) {
                        if (nameNotFoundMessage != nil) {
                            r.error(originalLocation, diagnostics.Static_members_cannot_reference_class_type_parameters);
                        }
                        return default;
                    }
                    goto break_loop;
                }
                if (ast.IsClassExpression(Ꮡlocation) && (ast.SymbolFlags)(meaning & ast.SymbolFlagsClass) != 0) {
                    var className = location.Name();
                    if (className != nil && name == className.Text()) {
                        result = location.Symbol();
                        goto break_loop;
                    }
                }
            } while (false);
        }
        else if (exprᴛ1 == ast.KindExpressionWithTypeArguments) { matchᴛ1 = true;
            if (lastLocation == location.Expression() && ast.IsHeritageClause(location.Parent) && location.Parent.AsHeritageClause().Token == ast.KindExtendsKeyword) {
                var container = location.Parent.Parent;
                if (ast.IsClassLike(container)) {
                    result = r.lookup(r.getSymbolOfDeclaration(container).Members, name, (ast.SymbolFlags)(meaning & ast.SymbolFlagsType));
                    if (result != nil) {
                        if (nameNotFoundMessage != nil) {
                            r.error(originalLocation, diagnostics.Base_class_expressions_cannot_reference_class_type_parameters);
                        }
                        return default;
                    }
                }
            }
        }
        else if (exprᴛ1 == ast.KindComputedPropertyName) { matchᴛ1 = true;
            grandparent = location.Parent.Parent;
            if (ast.IsClassLike(grandparent) || ast.IsInterfaceDeclaration(grandparent)) {
                result = r.lookup(r.getSymbolOfDeclaration(grandparent).Members, name, (ast.SymbolFlags)(meaning & ast.SymbolFlagsType));
                if (result != nil) {
                    if (nameNotFoundMessage != nil) {
                        r.error(originalLocation, diagnostics.A_computed_property_name_cannot_reference_a_type_parameter_from_its_containing_type);
                    }
                    return default;
                }
            }
        }
        else if (exprᴛ1 == ast.KindMethodDeclaration || exprᴛ1 == ast.KindConstructor || exprᴛ1 == ast.KindGetAccessor || exprᴛ1 == ast.KindSetAccessor || exprᴛ1 == ast.KindFunctionDeclaration) { matchᴛ1 = true;
            if ((ast.SymbolFlags)(meaning & ast.SymbolFlagsVariable) != 0 && name == "arguments"u8) {
                result = r.argumentsSymbol();
                goto break_loop;
            }
        }
        else if (exprᴛ1 == ast.KindFunctionExpression) { matchᴛ1 = true;
            if ((ast.SymbolFlags)(meaning & ast.SymbolFlagsVariable) != 0 && name == "arguments"u8) {
                result = r.argumentsSymbol();
                goto break_loop;
            }
            if ((ast.SymbolFlags)(meaning & ast.SymbolFlagsFunction) != 0) {
                var functionName = location.AsFunctionExpression().Name();
                if (functionName != nil && name == functionName.Text()) {
                    result = location.Symbol();
                    goto break_loop;
                }
            }
        }
        else if (exprᴛ1 == ast.KindDecorator) { matchᴛ1 = true;
            if (location.Parent != nil && location.Parent.Kind == ast.KindParameter) {
                Ꮡlocation = location.Parent; location = ref Ꮡlocation.DerefOrNull();
            }
            if (location.Parent != nil && (ast.IsClassElement(location.Parent) || location.Parent.Kind == ast.KindClassDeclaration)) {
                Ꮡlocation = location.Parent; location = ref Ꮡlocation.DerefOrNull();
            }
        }
        else if (exprᴛ1 == ast.KindParameter) { matchᴛ1 = true;
            var parameterDeclaration = location.AsParameterDeclaration();
            if (lastLocation != nil && (lastLocation == (~parameterDeclaration).Initializer || lastLocation == parameterDeclaration.Name() && ast.IsBindingPattern(lastLocation))) {
                if (associatedDeclarationForContainingInitializerOrBindingName == nil) {
                    associatedDeclarationForContainingInitializerOrBindingName = Ꮡlocation;
                }
            }
        }
        else if (exprᴛ1 == ast.KindBindingElement) { matchᴛ1 = true;
            var bindingElement = location.AsBindingElement();
            if (lastLocation != nil && (lastLocation == (~bindingElement).Initializer || lastLocation == bindingElement.Name() && ast.IsBindingPattern(lastLocation))) {
                if (ast.IsPartOfParameterDeclaration(Ꮡlocation) && associatedDeclarationForContainingInitializerOrBindingName == nil) {
                    associatedDeclarationForContainingInitializerOrBindingName = Ꮡlocation;
                }
            }
        }
        else if (exprᴛ1 == ast.KindInferType) {
            if ((ast.SymbolFlags)(meaning & ast.SymbolFlagsTypeParameter) != 0) {
                var parameterName = location.AsInferTypeNode().TypeParameter.AsTypeParameterDeclaration().Name();
                if (parameterName != nil && name == parameterName.Text()) {
                    result = location.AsInferTypeNode().TypeParameter.Symbol();
                    goto break_loop;
                }
            }
        }
        else if (exprᴛ1 == ast.KindExportSpecifier) { matchᴛ1 = true;
            var exportSpecifier = location.AsExportSpecifier();
            if (lastLocation != nil && lastLocation == (~exportSpecifier).PropertyName && location.Parent.Parent.ModuleSpecifier() != nil) {
                Ꮡlocation = location.Parent.Parent.Parent; location = ref Ꮡlocation.DerefOrNull();
            }
        }

        if (isSelfReferenceLocation(Ꮡlocation, lastLocation)) {
            lastSelfReferenceLocation = Ꮡlocation;
        }
        lastLocation = Ꮡlocation;
        Ꮡlocation = location.Parent; location = ref Ꮡlocation.DerefOrNull();
continue_loop:;
    }
break_loop:;
    if (isUse && result != nil && (lastSelfReferenceLocation == nil || result != lastSelfReferenceLocation.Symbol())) {
        if (r.SymbolReferenced != nil) {
            r.SymbolReferenced(result, meaning);
        }
    }
    if (result == nil && !excludeGlobals) {
        result = r.lookup(r.Globals, name, (ast.SymbolFlags)(meaning | ast.SymbolFlagsGlobalLookup));
    }
    if (result == nil) {
        if (originalLocation != nil && ast.IsInJSFile(originalLocation) && originalLocation.Parent != nil) {
            if (ast.IsRequireCall(originalLocation.Parent, false)) {
                return r.RequireSymbol;
            }
        }
    }
    if (nameNotFoundMessage != nil) {
        if (propertyWithInvalidInitializer != nil && r.OnPropertyWithInvalidInitializer != nil && r.OnPropertyWithInvalidInitializer(originalLocation, name, propertyWithInvalidInitializer, result)) {
            return default;
        }
        if (result == nil) {
            if (r.OnFailedToResolveSymbol != nil) {
                r.OnFailedToResolveSymbol(originalLocation, name, meaning, nameNotFoundMessage);
            }
        } else {
            if (r.OnSuccessfullyResolvedSymbol != nil) {
                r.OnSuccessfullyResolvedSymbol(originalLocation, result, meaning, lastLocation, associatedDeclarationForContainingInitializerOrBindingName, withinDeferredContext);
            }
        }
    }
    return result;
}

internal static bool useOuterVariableScopeInParameter(this ж<NameResolver> Ꮡr, ж<ast.Symbol> Ꮡresult, ж<ast.Node> Ꮡlocation, ж<ast.Node> lastLocation) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var result = ref Ꮡresult.DerefOrNull();
    ref var location = ref Ꮡlocation.DerefOrNull();

    if (ast.IsParameterDeclaration(lastLocation)) {
        var body = location.Body();
        if (body != nil && result.ValueDeclaration != nil && result.ValueDeclaration.Pos() >= body.Pos() && result.ValueDeclaration.End() <= body.End()) {
            var functionLocation = Ꮡlocation;
            var declarationRequiresScopeChange = core.TSUnknown;
            if (r.GetRequiresScopeChangeCache != nil) {
                declarationRequiresScopeChange = r.GetRequiresScopeChangeCache(functionLocation);
            }
            if (declarationRequiresScopeChange == core.TSUnknown) {
                declarationRequiresScopeChange = core.IfElse(core.Some<ж<astꓸParameterDeclarationNode>>(functionLocation.Parameters(), Ꮡr.requiresScopeChange), core.TSTrue, core.TSFalse);
                if (r.SetRequiresScopeChangeCache != nil) {
                    r.SetRequiresScopeChangeCache(functionLocation, declarationRequiresScopeChange);
                }
            }
            return declarationRequiresScopeChange != core.TSTrue;
        }
    }
    return false;
}

internal static bool requiresScopeChange(this ж<NameResolver> Ꮡr, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var d = node.AsParameterDeclaration();
    return Ꮡr.requiresScopeChangeWorker(d.Name()) || (~d).Initializer != nil && Ꮡr.requiresScopeChangeWorker((~d).Initializer);
}

internal static bool requiresScopeChangeWorker(this ж<NameResolver> Ꮡr, ж<ast.Node> Ꮡnode) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindArrowFunction || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindConstructor: {
        return false;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor || ᴛ == ast.KindPropertyAssignment: {
        return Ꮡr.requiresScopeChangeWorker(node.Name());
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        if (ast.HasStaticModifier(Ꮡnode)) {
            return !r.CompilerOptions.GetEmitStandardClassFields();
        }
        return Ꮡr.requiresScopeChangeWorker(node.AsPropertyDeclaration().Name());
    }
    default: {
        if (ast.IsNullishCoalesce(Ꮡnode) || ast.IsOptionalChain(Ꮡnode)) {
            return r.CompilerOptions.GetEmitScriptTarget() < core.ScriptTargetES2020;
        }
        if (ast.IsBindingElement(Ꮡnode) && node.AsBindingElement().DotDotDotToken != nil && ast.IsObjectBindingPattern(node.Parent)) {
            return r.CompilerOptions.GetEmitScriptTarget() < core.ScriptTargetES2017;
        }
        if (ast.IsTypeNode(Ꮡnode)) {
            return false;
        }
        return node.ForEachChild(new Func<ж<ast.Node>, bool>(Ꮡr.requiresScopeChangeWorker));
    }}

}

internal static void error(this ref NameResolver r, ж<ast.Node> location, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    if (r.Error != nil) {
        r.Error(location, message, args.ꓸꓸꓸ);
    }
}

internal static ж<ast.Symbol> getSymbolOfDeclaration(this ref NameResolver r, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (r.GetSymbolOfDeclaration != nil) {
        return r.GetSymbolOfDeclaration(Ꮡnode);
    }

    return node.Symbol();
}

internal static ж<ast.Symbol> lookup(this ref NameResolver r, ast.SymbolTable symbols, @string name, ast.SymbolFlags meaning) {
    if (r.Lookup != nil) {
        return r.Lookup(symbols, name, meaning);
    }
    if (meaning != 0) {
        var symbol = symbols[name];
        if (symbol != nil) {
            if ((ast.SymbolFlags)(symbol.Flags & meaning) != 0) {
                return symbol;
            }
        }
    }
    return default;
}

internal static ж<ast.Symbol> argumentsSymbol(this ref NameResolver r) {
    if (r.ArgumentsSymbol == nil) {
        r.ArgumentsSymbol = Ꮡ(new ast.Symbol(Name: "arguments", Flags: (ast.SymbolFlags)(ast.SymbolFlagsProperty | ast.SymbolFlagsTransient)));
    }
    return r.ArgumentsSymbol;
}

} partial class binder_package {

public static ж<ast.Symbol> GetLocalSymbolForExportDefault(ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (!isExportDefaultSymbol(Ꮡsymbol) || len(symbol.Declarations) == 0) {
        return default;
    }
    foreach (var (_, decl) in range(symbol.Declarations)) {
        var localSymbol = decl.LocalSymbol();
        if (localSymbol != nil) {
            return localSymbol;
        }
    }
    return default;
}

internal static bool isExportDefaultSymbol(ж<ast.Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    return Ꮡsymbol != nil && len(symbol.Declarations) > 0 && ast.HasSyntacticModifier(symbol.Declarations[0], ast.ModifierFlagsDefault);
}

internal static bool getIsDeferredContext(ж<ast.Node> Ꮡlocation, ж<ast.Node> lastLocation) {
    ref var location = ref Ꮡlocation.DerefOrNull();

    if (location.Kind != ast.KindArrowFunction && location.Kind != ast.KindFunctionExpression) {
        return ast.IsTypeQueryNode(Ꮡlocation) || (ast.IsFunctionLikeDeclaration(Ꮡlocation) || location.Kind == ast.KindPropertyDeclaration && !ast.IsStatic(Ꮡlocation)) && (lastLocation == nil || lastLocation != location.Name());
    }
    if (lastLocation != nil && lastLocation == location.Name()) {
        return false;
    }
    if (location.BodyData().AsteriskToken != nil || ast.HasSyntacticModifier(Ꮡlocation, ast.ModifierFlagsAsync)) {
        return true;
    }
    return ast.GetImmediatelyInvokedFunctionExpression(Ꮡlocation) == nil;
}

internal static bool isTypeParameterSymbolDeclaredInContainer(ref ast.Symbol symbol, ж<ast.Node> container) {
    foreach (var (_, decl) in range(symbol.Declarations)) {
        if (decl.Kind == ast.KindTypeParameter) {
            var parent = decl.Parent;
            if (parent == container) {
                return true;
            }
        }
    }
    return false;
}

internal static bool isSelfReferenceLocation(ж<ast.Node> Ꮡnode, ж<ast.Node> lastLocation) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindParameter: {
        return lastLocation != nil && lastLocation == node.Name();
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindClassDeclaration || ᴛ == ast.KindInterfaceDeclaration || ᴛ == ast.KindEnumDeclaration || ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration || ᴛ == ast.KindModuleDeclaration: {
        return true;
    }}

    return false;
}

} // end binder_package
