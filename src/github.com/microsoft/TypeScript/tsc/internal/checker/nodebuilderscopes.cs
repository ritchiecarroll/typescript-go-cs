namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

internal static Action cloneNodeBuilderContext(ж<NodeBuilderContext> context) {
    var restoreNames = context.ᏑtypeParameterNames.EnterScope();
    var restoreNamesByText = context.ᏑtypeParameterNamesByText.EnterScope();
    var restoreNamesByTextNextNameCount = context.ᏑtypeParameterNamesByTextNextNameCount.EnterScope();
    var restoreSymbolList = context.ᏑtypeParameterSymbolList.EnterScope();
    var restoreNamesʗ1 = restoreNames;
    var restoreNamesByTextʗ1 = restoreNamesByText;
    var restoreNamesByTextNextNameCountʗ1 = restoreNamesByTextNextNameCount;
    var restoreSymbolListʗ1 = restoreSymbolList;
    return () => {
        restoreNamesʗ1();
        restoreNamesByTextʗ1();
        restoreNamesByTextNextNameCountʗ1();
        restoreSymbolListʗ1();
    };
}

partial struct localsRecord {
    internal @string name;
    internal ж<ast.Symbol> oldSymbol;
}

} partial class checkerᴛNodeBuilderImpl {

internal static Action addSymbolTypeToContext(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Symbol> symbol, ж<Type> t) {
    ref var b = ref Ꮡb.DerefOrNull();

    var id = ast.GetSymbolId(symbol);
    var (oldType, oldTypeExists) = b.ctx.enclosingSymbolTypes[id, ꟷ];
    b.ctx.enclosingSymbolTypes[id] = t;
    var oldTypeʗ1 = oldType;
    return () => {
        if (oldTypeExists) {
            Ꮡb.Value.ctx.enclosingSymbolTypes[id] = oldTypeʗ1;
        } else {
            delete(Ꮡb.Value.ctx.enclosingSymbolTypes, id);
        }
    };
}

internal static (slice<ж<ast.Symbol>> expandedParams, Action cleanup) enterSignatureScope(this ж<NodeBuilderImpl> Ꮡb, ж<Signature> Ꮡsignature) {
    slice<ж<ast.Symbol>> expandedParams = default;
    Action cleanup = default;
    ref var b = ref Ꮡb.DerefOrNull();
    ref var signature = ref Ꮡsignature.DerefOrNull();

    expandedParams = b.ch.getExpandedParameters(Ꮡsignature, true)[0];
    cleanup = Ꮡb.enterNewScope(signature.declaration, expandedParams, signature.typeParameters, signature.parameters, signature.mapper);
    return (expandedParams, cleanup);
}

internal static Action enterNewScope(this ж<NodeBuilderImpl> Ꮡb, ж<ast.Node> declaration, slice<ж<ast.Symbol>> expandedParams, slice<ж<Type>> typeParameters, slice<ж<ast.Symbol>> originalParameters, ж<TypeMapper> Ꮡmapper) {
    ref var b = ref Ꮡb.DerefOrNull();
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    var cleanupContext = cloneNodeBuilderContext(b.ctx);
    Action cleanupParams = default;
    Action cleanupTypeParams = default;
    var oldEnclosingDecl = b.ctx.enclosingDeclaration;
    var oldMapper = b.ctx.mapper;
    if (Ꮡmapper != nil) {
        b.ctx.mapper = Ꮡmapper;
    }
    if (b.ctx.enclosingDeclaration != nil && declaration != nil) {
        Action pushFakeScope(@string kindʗp, Action<Action<@string, ж<ast.Symbol>>> addAll) {
            ref var kind = ref heap(kindʗp, out var Ꮡkind);
            debug.Assert(Ꮡb.Value.ctx.enclosingDeclaration != nil);
            ref var existingFakeScope = ref heap<ж<ast.Node>>(out var ᏑexistingFakeScope);
            if (Ꮡb.Value.links.Has(Ꮡb.Value.ctx.enclosingDeclaration)) {
                var links = Ꮡb.Value.links.Get(Ꮡb.Value.ctx.enclosingDeclaration);
                if (links.fakeScopeForSignatureDeclaration != nil && links.fakeScopeForSignatureDeclaration.Value == kind) {
                    ᏑexistingFakeScope.ValueSlot = Ꮡb.Value.ctx.enclosingDeclaration;
                }
            }
            if (ᏑexistingFakeScope.ValueSlot == nil && Ꮡb.Value.ctx.enclosingDeclaration.Parent != nil) {
                if (Ꮡb.Value.links.Has(Ꮡb.Value.ctx.enclosingDeclaration.Parent)) {
                    var links = Ꮡb.Value.links.Get(Ꮡb.Value.ctx.enclosingDeclaration.Parent);
                    if (links.fakeScopeForSignatureDeclaration != nil && links.fakeScopeForSignatureDeclaration.Value == kind) {
                        ᏑexistingFakeScope.ValueSlot = Ꮡb.Value.ctx.enclosingDeclaration.Parent;
                    }
                }
            }
            debug.Assert(ᏑexistingFakeScope.ValueSlot == nil || ast.IsBlock(ᏑexistingFakeScope.ValueSlot));

            ref var locals = ref heap<ast.SymbolTable>(out var Ꮡlocals);
            if (ᏑexistingFakeScope.ValueSlot != nil) {
                Ꮡlocals.ValueSlot = ᏑexistingFakeScope.ValueSlot.Locals();
            }
            if (Ꮡlocals.ValueSlot == nil) {
                Ꮡlocals.ValueSlot = new ast.SymbolTable(0);
            }
            ref var newLocals = ref heap<slice<@string>>(slice<@string>([]), out var ᏑnewLocals);
            ref var oldLocals = ref heap<slice<localsRecord>>(slice<localsRecord>([]), out var ᏑoldLocals);
            addAll((@string name, ж<ast.Symbol> symbol) => {
                if (ᏑexistingFakeScope.ValueSlot != nil) {
                    var (oldSymbol, ok) = Ꮡlocals.ValueSlot[name, ꟷ];
                    if (!ok || oldSymbol == nil) {
                        ᏑnewLocals.ValueSlot = append(ᏑnewLocals.ValueSlot, name);
                    } else {
                        ᏑoldLocals.ValueSlot = append(ᏑoldLocals.ValueSlot, new localsRecord(name, oldSymbol));
                    }
                }
                Ꮡlocals.ValueSlot[name] = symbol;
            });

            if (ᏑexistingFakeScope.ValueSlot == nil) {
                var fakeScope = Ꮡb.Value.f.NewBlock(Ꮡb.Value.f.NewNodeList([]), false);
                Ꮡb.Value.links.Get(fakeScope).fakeScopeForSignatureDeclaration = Ꮡkind;
                var data = fakeScope.LocalsContainerData();
                data.Locals = Ꮡlocals.ValueSlot;
                fakeScope.Parent = Ꮡb.Value.ctx.enclosingDeclaration;
                Ꮡb.Value.ctx.enclosingDeclaration = fakeScope;
                return default;
            } else {
                var undo = () => {
                    foreach (var (_, s) in range(ᏑnewLocals.ValueSlot)) {
                        delete(Ꮡlocals.ValueSlot, s);
                    }
                    foreach (var (_, s) in range(ᏑoldLocals.ValueSlot)) {
                        Ꮡlocals.ValueSlot[s.name] = s.oldSymbol;
                    }
                };
                return undo;
            }
        }

        if (expandedParams == nil || !core.Some(expandedParams, (ж<ast.Symbol> p) => p != nil)) {
            cleanupParams = default;
        } else {
            var expandedParamsʗ1 = expandedParams;
            var originalParametersʗ1 = originalParameters;
            cleanupParams = pushFakeScope("params", (Action<@string, ж<ast.Symbol>> add) => {
                if (expandedParamsʗ1 == nil) {
                    return;
                }
                foreach (var (pIndex, param) in range(expandedParamsʗ1)) {
                    ж<ast.Symbol> originalParam = default;
                    if (pIndex < len(originalParametersʗ1)) {
                        originalParam = originalParametersʗ1[pIndex];
                    }
                    if (originalParametersʗ1 != nil && originalParam != param) {
                        if (originalParam != nil) {
                            add(originalParam.Name, originalParam);
                        }
                    } else 
                    if (!core.Some(param.Declarations, (ж<ast.Node> d) => {
                        ref var bindElement = ref heap<Action<ж<ast.BindingElement>>>(out var ᏑbindElement);
                        ref var bindPattern = ref heap<Action<ж<ast.BindingPattern>>>(out var ᏑbindPattern);

                        var bindPatternWorker = (ж<ast.BindingPattern> p) => {
                            foreach (var (_, e) in range((~p).Elements.Nodes)) {
                                switch (e.Kind) {
                                case var ᴛ when ᴛ == ast.KindOmittedExpression: {
                                    return;
                                }
                                case var ᴛ when ᴛ == ast.KindBindingElement: {
                                    ᏑbindElement.ValueSlot(e.AsBindingElement());
                                    return;
                                }
                                default: {
                                    throw panic("Unhandled binding element kind");
                                    break;
                                }}

                            }
                        };

                        var bindElementWorker = (ж<ast.BindingElement> e) => {
                            if (e.Name() != nil && ast.IsBindingPattern(e.Name())) {
                                ᏑbindPattern.ValueSlot(e.Name().AsBindingPattern());
                                return;
                            }
                            var symbol = Ꮡb.Value.ch.getSymbolOfDeclaration(e.of(ast.BindingElement.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                            if (symbol != nil) {
                                add(symbol.Name, symbol);
                            }
                        };
                        ᏑbindElement.ValueSlot = bindElementWorker;
                        ᏑbindPattern.ValueSlot = bindPatternWorker;

                        if (ast.IsParameterDeclaration(d) && d.Name() != nil && ast.IsBindingPattern(d.Name())) {
                            ᏑbindPattern.ValueSlot(d.Name().AsBindingPattern());
                            return true;
                        }
                        return false;
                    })) {
                        add(param.Name, param);
                    }
                }
            });
        }

        if ((nodebuilder.Flags)(b.ctx.flags & nodebuilder.FlagsGenerateNamesForShadowedTypeParams) != 0 && typeParameters != nil && core.Some(typeParameters, (ж<Type> p) => p != nil)) {
            var typeParametersʗ1 = typeParameters;
            cleanupTypeParams = pushFakeScope("typeParams", (Action<@string, ж<ast.Symbol>> add) => {
                if (typeParametersʗ1 == nil) {
                    return;
                }
                foreach (var (_, typeParam) in range(typeParametersʗ1)) {
                    if (typeParam == nil) {
                        continue;
                    }
                    @string typeParamName = Ꮡb.typeParameterToName(typeParam).Value.Text;
                    add(typeParamName, typeParam.symbol);
                }
            });
        }
    }

    var cleanupContextʗ1 = cleanupContext;
    var cleanupParamsʗ1 = cleanupParams;
    var cleanupTypeParamsʗ1 = cleanupTypeParams;
    var oldEnclosingDeclʗ1 = oldEnclosingDecl;
    var oldMapperʗ1 = oldMapper;
    return () => {
        if (cleanupParamsʗ1 != nil) {
            cleanupParamsʗ1();
        }
        if (cleanupTypeParamsʗ1 != nil) {
            cleanupTypeParamsʗ1();
        }
        cleanupContextʗ1();
        Ꮡb.Value.ctx.enclosingDeclaration = oldEnclosingDeclʗ1;
        Ꮡb.Value.ctx.mapper = oldMapperʗ1;
    };
}

} // end checkerᴛNodeBuilderImpl
