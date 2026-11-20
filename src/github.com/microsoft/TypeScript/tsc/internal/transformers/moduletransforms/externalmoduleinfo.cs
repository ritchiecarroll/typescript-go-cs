namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using slices = slices_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static moduletransforms_package;

partial class moduletransforms_package {

partial struct externalModuleInfo {
    internal slice<ж<astꓸDeclaration>> externalImports;
    internal collections.MultiMap<@string, ж<ast.ExportSpecifier>> exportSpecifiers;
    internal collections.MultiMap<ж<astꓸDeclaration>, ж<astꓸModuleExportName>> exportedBindings;
    internal slice<ж<astꓸModuleExportName>> exportedNames;
    internal collections.OrderedSet<ж<astꓸFunctionDeclarationNode>> exportedFunctions;
    internal ж<ast.ExportAssignment> exportEquals;
    internal bool hasExportStarsToExportValues;
}

partial struct externalModuleInfoCollector {
    internal ж<ast.SourceFile> sourceFile;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ж<printer.EmitContext> emitContext;
    internal binder.ReferenceResolver resolver;
    internal collections.Set<@string> uniqueExports;
    internal bool hasExportDefault;
    internal ж<externalModuleInfo> output;
}

internal static ж<externalModuleInfo> collectExternalModuleInfo(ж<ast.SourceFile> sourceFile, ж<core.CompilerOptions> compilerOptions, ж<printer.EmitContext> emitContext, binder.ReferenceResolver resolver) {
    ref var c = ref heap<externalModuleInfoCollector>(new externalModuleInfoCollector(
        sourceFile: sourceFile,
        compilerOptions: compilerOptions,
        emitContext: emitContext,
        resolver: resolver,
        output: Ꮡ(new externalModuleInfo())
    ), out var Ꮡc);
    return Ꮡc.collect();
}

} partial class moduletransformsᴛexternalModuleInfoCollector {

internal static ж<externalModuleInfo> collect(this ж<externalModuleInfoCollector> Ꮡc) {
    ref var c = ref Ꮡc.DerefOrNull();

    var hasImportStar = false;
    var hasImportDefault = false;
    foreach (var (_, node) in range((~c.sourceFile).Statements.Nodes)) {
        if (ast.IsNotEmittedStatement(node)) {
            var original = c.emitContext.MostOriginal(node);
            if (original != nil && ast.IsExportAssignment(original)) {
                var n = original.AsExportAssignment();
                if (n.IsExportEquals && c.output.exportEquals == nil) {
                    c.output.exportEquals = n;
                }
            }
            continue;
        }
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindImportDeclaration: {
            var n = node.AsImportDeclaration();
            c.addExternalImport(node);
            if (!hasImportStar && getImportNeedsImportStarHelper(n)) {
                hasImportStar = true;
            }
            if (!hasImportDefault && getImportNeedsImportDefaultHelper(n)) {
                hasImportDefault = true;
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindImportEqualsDeclaration: {
            var n = node.AsImportEqualsDeclaration();
            if (ast.IsExternalModuleReference(n.ModuleReference)) {
                c.addExternalImport(node);
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindExportDeclaration: {
            var n = node.AsExportDeclaration();
            if ((~n).ModuleSpecifier != nil) {
                c.addExternalImport(node);
                if (n.ExportClause == nil) {
                    c.output.hasExportStarsToExportValues = true;
                } else if (ast.IsNamedExports(n.ExportClause)) {
                    Ꮡc.addExportedNamesForExportDeclaration(n);
                    if (!hasImportDefault) {
                        hasImportDefault = containsDefaultReference(n.ExportClause);
                    }
                } else {
                    var name = n.ExportClause.AsNamespaceExport().Name();
                    @string nameText = name.Text();
                    if (Ꮡc.addUniqueExport(nameText)) {
                        c.addExportedBinding(node, name);
                        c.addExportedName(name);
                    }
                    hasImportStar = true;
                }
            } else {
                Ꮡc.addExportedNamesForExportDeclaration(node.AsExportDeclaration());
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindExportAssignment: {
            var n = node.AsExportAssignment();
            if (n.IsExportEquals && c.output.exportEquals == nil) {
                c.output.exportEquals = n;
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindVariableStatement: {
            var n = node.AsVariableStatement();
            if (ast.HasSyntacticModifier(node, ast.ModifierFlagsExport)) {
                foreach (var (_, decl) in range(n.DeclarationList.AsVariableDeclarationList().Declarations.Nodes)) {
                    Ꮡc.collectExportedVariableInfo(decl);
                }
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
            var n = node.AsFunctionDeclaration();
            if (ast.HasSyntacticModifier(node, ast.ModifierFlagsExport)) {
                Ꮡc.addExportedFunctionDeclaration(n, nil, ast.HasSyntacticModifier(node, ast.ModifierFlagsDefault));
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindClassDeclaration: {
            var n = node.AsClassDeclaration();
            if (ast.HasSyntacticModifier(node, ast.ModifierFlagsExport)) {
                if (ast.HasSyntacticModifier(node, ast.ModifierFlagsDefault)) {
                    if (!c.hasExportDefault) {
                        var name = n.Name();
                        if (name == nil) {
                            name = c.emitContext.Factory.NewGeneratedNameForNode(node);
                        }
                        c.addExportedBinding(node, name);
                        c.hasExportDefault = true;
                    }
                } else {
                    var name = n.Name();
                    if (name != nil) {
                        if (Ꮡc.addUniqueExport(name.Text())) {
                            c.addExportedBinding(node, name);
                            c.addExportedName(name);
                        }
                    }
                }
            }
            break;
        }}

    }

    return c.output;
}

internal static bool addUniqueExport(this ж<externalModuleInfoCollector> Ꮡc, @string name) {
    ref var c = ref Ꮡc.DerefOrNull();

    if (!Ꮡc.ᏑuniqueExports.Has(name)) {
        c.uniqueExports.Add(name);
        return true;
    }
    return false;
}

internal static void addExportedBinding(this ref externalModuleInfoCollector c, ж<astꓸDeclaration> decl, ж<astꓸModuleExportName> name) {
    c.output.ᏑexportedBindings.Add(c.emitContext.MostOriginal(decl), name);
}

internal static void addExternalImport(this ref externalModuleInfoCollector c, ж<ast.Node> node) {
    c.output.externalImports = append(c.output.externalImports, node);
}

internal static void addExportedName(this ref externalModuleInfoCollector c, ж<astꓸModuleExportName> name) {
    c.output.exportedNames = append(c.output.exportedNames, name);
}

internal static void addExportedNamesForExportDeclaration(this ж<externalModuleInfoCollector> Ꮡc, ж<ast.ExportDeclaration> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    foreach (var (_, specifier) in range(node.ExportClause.Elements())) {
        @string specifierNameText = specifier.Name().Text();
        if (Ꮡc.addUniqueExport(specifierNameText)) {
            var name = specifier.PropertyNameOrName();
            if (name.Kind != ast.KindStringLiteral) {
                if (node.ModuleSpecifier == nil) {
                    c.output.ᏑexportSpecifiers.Add(name.Text(), specifier.AsExportSpecifier());
                }

                var decl = c.resolver.GetReferencedImportDeclaration(c.emitContext.MostOriginal(name));
                if (decl == nil) {
                    decl = c.resolver.GetReferencedValueDeclaration(c.emitContext.MostOriginal(name));
                }
                if (decl != nil) {
                    if (decl.Kind == ast.KindFunctionDeclaration) {
                        c.uniqueExports.Delete(specifierNameText);
                        Ꮡc.addExportedFunctionDeclaration(decl.AsFunctionDeclaration(), specifier.Name(), ast.ModuleExportNameIsDefault(specifier.Name()));
                        continue;
                    }
                    c.addExportedBinding(decl, specifier.Name());
                }
            }

            c.addExportedName(specifier.Name());
        }
    }
}

internal static void addExportedFunctionDeclaration(this ж<externalModuleInfoCollector> Ꮡc, ж<ast.FunctionDeclaration> Ꮡnode, ж<astꓸModuleExportName> Ꮡname, bool isDefault) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var name = ref Ꮡname.DerefOrNull();

    c.output.ᏑexportedFunctions.Add(c.emitContext.MostOriginal(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
    if (isDefault) {
        if (!c.hasExportDefault) {
            if (Ꮡname == nil) {
                Ꮡname = c.emitContext.Factory.NewGeneratedNameForNode(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()); name = ref Ꮡname.DerefOrNull();
            }
            c.addExportedBinding(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡname);
            c.hasExportDefault = true;
        }
    } else {
        if (Ꮡname == nil) {
            Ꮡname = node.Name(); name = ref Ꮡname.DerefOrNull();
        }
        @string nameText = name.Text();
        if (Ꮡc.addUniqueExport(nameText)) {
            c.addExportedBinding(Ꮡnode.of(ast.FunctionDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), Ꮡname);
        }
    }
}

internal static void collectExportedVariableInfo(this ж<externalModuleInfoCollector> Ꮡc, ж<ast.Node> Ꮡdecl) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var decl = ref Ꮡdecl.DerefOrNull();

    if (ast.IsBindingPattern(decl.Name())) {
        foreach (var (_, element) in range(decl.Name().Elements())) {
            var e = element.AsBindingElement();
            if (e.Name() != nil) {
                Ꮡc.collectExportedVariableInfo(element);
            }
        }
    } else if (!c.emitContext.HasAutoGenerateInfo(decl.Name())) {
        @string text = decl.Name().Text();
        if (Ꮡc.addUniqueExport(text)) {
            c.addExportedName(decl.Name());
            if (transformers.IsLocalName(c.emitContext, decl.Name())) {
                c.addExportedBinding(Ꮡdecl, decl.Name());
            }
        }
    }
}

} partial class moduletransforms_package {

internal static readonly @string externalHelpersModuleNameText = "tslib"u8;

internal static ж<ast.Node> createExternalHelpersImportDeclarationIfNeeded(ж<printer.EmitContext> ᏑemitContext, ж<ast.SourceFile> sourceFile, ж<core.CompilerOptions> ᏑcompilerOptions, core.ModuleKind fileModuleKind, bool hasExportStarsToExportValues, bool hasImportStar, bool hasImportDefault) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    if (compilerOptions.ImportHelpers.IsTrue() && ast.IsEffectiveExternalModule(sourceFile, ᏑcompilerOptions)) {
        var moduleKind = compilerOptions.GetEmitModuleKind();
        var helpers = getImportedHelpers(ᏑemitContext, sourceFile);
        if (fileModuleKind == core.ModuleKindCommonJS || fileModuleKind == core.ModuleKindNone && moduleKind == core.ModuleKindCommonJS) {
            var externalHelpersModuleName = getOrCreateExternalHelpersModuleNameIfNeeded(ᏑemitContext, sourceFile, ref (ᏑcompilerOptions).DerefOrNull(), helpers, hasExportStarsToExportValues, hasImportStar || hasImportDefault, fileModuleKind);
            if (externalHelpersModuleName != nil) {
                var externalHelpersImportDeclaration = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewImportEqualsDeclaration(
                    nil,
                    false,
                    externalHelpersModuleName,
                    emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewExternalModuleReference(emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(externalHelpersModuleNameText, ast.TokenFlagsNone)));
                emitContext.AddEmitFlags(externalHelpersImportDeclaration, printer.EFCustomPrologue);
                return externalHelpersImportDeclaration;
            }
        } else {
            slice<@string> helperNames = default;
            foreach (var (_, helper) in range(helpers)) {
                @string importName = helper.ImportName;
                if (len(importName) > 0) {
                    helperNames = core.AppendIfUnique(helperNames, importName);
                }
            }
            if (len(helperNames) > 0) {
                slices.SortFunc<slice<@string>, @string>(helperNames, stringutil.CompareStringsCaseSensitive);

                var importSpecifiers = core.Map(helperNames, ж<astꓸImportSpecifierNode> (@string name) => {
                    if (ᏑemitContext.Value.IsFileLevelUniqueName(sourceFile, name, default)) {
                        return ᏑemitContext.Value.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewImportSpecifier(false, nil, ᏑemitContext.Value.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(name));
                    } else {
                        return ᏑemitContext.Value.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewImportSpecifier(false, ᏑemitContext.Value.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier(name), ᏑemitContext.Value.Factory.NewUnscopedHelperName(name));
                    }
                });
                var namedBindings = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNamedImports(emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNodeList(importSpecifiers));
                var parseNode = emitContext.MostOriginal(sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
                emitContext.AddEmitFlags(parseNode, printer.EFExternalHelpers);

                var externalHelpersImportDeclaration = emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewImportDeclaration(
                    nil,
                    emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewImportClause(ast.KindUnknown, nil, namedBindings),
                    emitContext.Factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(externalHelpersModuleNameText, ast.TokenFlagsNone),
                    nil);

                emitContext.AddEmitFlags(externalHelpersImportDeclaration, printer.EFCustomPrologue);
                return externalHelpersImportDeclaration;
            }
        }
    }
    return default;
}

internal static slice<ж<printer.EmitHelper>> getImportedHelpers(ж<printer.EmitContext> ᏑemitContext, ж<ast.SourceFile> sourceFile) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    var helpersᴷ = new stackAppend<ж<printer.EmitHelper>>(moves: true);

    slice<ж<printer.EmitHelper>> helpers = default;
    foreach (var (_, helper) in range(emitContext.GetEmitHelpers(sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()))) {
        if (!helper.Scoped) {
            helpers = append(helpersᴷ, helpers, helper);
        }
    }
    helpers = move2heap(helpersᴷ, helpers);
    return helpers;
}

internal static ж<astꓸIdentifierNode> getOrCreateExternalHelpersModuleNameIfNeeded(ж<printer.EmitContext> ᏑemitContext, ж<ast.SourceFile> node, ref core.CompilerOptions compilerOptions, slice<ж<printer.EmitHelper>> helpers, bool hasExportStarsToExportValues, bool hasImportStarOrImportDefault, core.ModuleKind fileModuleKind) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    var externalHelpersModuleName = emitContext.GetExternalHelpersModuleName(node);
    if (externalHelpersModuleName != nil) {
        return externalHelpersModuleName;
    }

    var create = len(helpers) > 0 || (hasExportStarsToExportValues || hasImportStarOrImportDefault) && fileModuleKind < core.ModuleKindSystem;

    if (create) {
        externalHelpersModuleName = emitContext.Factory.NewUniqueName(externalHelpersModuleNameText);
        emitContext.SetExternalHelpersModuleName(node, externalHelpersModuleName);
    }

    return externalHelpersModuleName;
}

internal static bool isNamedDefaultReference(ж<ast.Node> Ꮡe) {
    ref var e = ref Ꮡe.DerefOrNull();

    return ast.ModuleExportNameIsDefault(e.PropertyNameOrName());
}

internal static bool containsDefaultReference(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return Ꮡnode != nil && (ast.IsNamedImports(Ꮡnode) || ast.IsNamedExports(Ꮡnode)) && core.Some<ж<ast.Node>>(node.Elements(), isNamedDefaultReference);
}

internal static bool getExportNeedsImportStarHelper(ж<ast.ExportDeclaration> node) {
    return ast.GetNamespaceDeclarationNode(node.of(ast.ExportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) != nil;
}

internal static bool getImportNeedsImportStarHelper(ж<ast.ImportDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.GetNamespaceDeclarationNode(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) != nil) {
        return true;
    }
    if (node.ImportClause == nil) {
        return false;
    }
    var bindings = node.ImportClause.AsImportClause().NamedBindings;
    if (bindings == nil) {
        return false;
    }
    if (!ast.IsNamedImports(bindings)) {
        return false;
    }
    var namedImports = bindings.AsNamedImports();
    nint defaultRefCount = 0;
    foreach (var (_, binding) in range((~namedImports).Elements.Nodes)) {
        if (isNamedDefaultReference(binding)) {
            defaultRefCount++;
        }
    }
    return (defaultRefCount > 0 && defaultRefCount != len((~namedImports).Elements.Nodes)) || ((len((~namedImports).Elements.Nodes) - defaultRefCount) != 0 && ast.IsDefaultImport(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()));
}

internal static bool getImportNeedsImportDefaultHelper(ж<ast.ImportDeclaration> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return !getImportNeedsImportStarHelper(Ꮡnode) && (ast.IsDefaultImport(Ꮡnode.of(ast.ImportDeclaration.ᏑStatementBase).of(ast.StatementBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode()) || (node.ImportClause != nil && ast.IsNamedImports(node.ImportClause.AsImportClause().NamedBindings) && containsDefaultReference(node.ImportClause.AsImportClause().NamedBindings)));
}

} // end moduletransforms_package
