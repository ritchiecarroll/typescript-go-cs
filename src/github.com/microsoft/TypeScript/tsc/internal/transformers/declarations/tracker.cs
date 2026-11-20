namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using checker = go.github.com.microsoft.TypeScript.tsc.@internal.checker_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸany = Span<any>;
using static declarations_package;

partial class declarations_package {

partial struct SymbolTrackerImpl {
    internal printer.EmitResolver resolver;
    internal ж<SymbolTrackerSharedState> state;
    internal DeclarationEmitHost host;
    internal slice<ж<ast.Node>> fallbackStack;
    internal ж<ast.Symbol> watchedClassSymbol;
    internal bool classSymbolTracked;
    internal Func<ж<ast.Node>, ж<ast.Diagnostic>> getIsolatedDeclarationError;
}

} partial class declarationsᴛSymbolTrackerImpl {

public static void PopErrorFallbackNode(this ref SymbolTrackerImpl s) {
    s.fallbackStack = s.fallbackStack.slice(0, len(s.fallbackStack) - 1);
}

public static void PushErrorFallbackNode(this ref SymbolTrackerImpl s, ж<ast.Node> node) {
    s.fallbackStack = append(s.fallbackStack, node);
}

public static void ReportCyclicStructureError(this ref SymbolTrackerImpl s) {
    var location = s.errorLocation();
    if (location != nil) {
        s.state.addDiagnostic(createDiagnosticForNode(location, diagnostics.The_inferred_type_of_0_references_a_type_with_a_cyclic_structure_which_cannot_be_trivially_serialized_A_type_annotation_is_necessary, s.errorDeclarationNameWithFallback()));
    }
}

public static void ReportInaccessibleThisError(this ref SymbolTrackerImpl s) {
    var location = s.errorLocation();
    if (location != nil) {
        s.state.addDiagnostic(createDiagnosticForNode(location, diagnostics.The_inferred_type_of_0_references_an_inaccessible_1_type_A_type_annotation_is_necessary, s.errorDeclarationNameWithFallback(), "this"));
    }
}

public static void ReportInaccessibleUniqueSymbolError(this ref SymbolTrackerImpl s) {
    var location = s.errorLocation();
    if (location != nil) {
        s.state.addDiagnostic(createDiagnosticForNode(location, diagnostics.The_inferred_type_of_0_references_an_inaccessible_1_type_A_type_annotation_is_necessary, s.errorDeclarationNameWithFallback(), "unique symbol"));
    }
}

internal static bool isBoundExpando(this ref SymbolTrackerImpl s, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!(ast.IsExpandoPropertyDeclaration(Ꮡnode) && ast.IsPropertyAccessExpression(node.AsBinaryExpression().Left))) {
        return false;
    }
    var @ref = s.resolver.GetReferencedValueDeclarationUnsafe(ast.GetLeftmostExpression(node.AsBinaryExpression().Left, true));
    if (@ref == nil) {
        return false;
    }
    return s.resolver.IsExpandoFunctionDeclarationUnsafe(@ref);
}

internal static bool isChildOfBoundExpando(this ж<SymbolTrackerImpl> Ꮡs, ж<ast.Node> node) {
    return ast.FindAncestorOrQuit(node, (ж<ast.Node> n) => {
        if (ast.IsSourceFile(n) || ast.IsBlock(n)) {
            return ast.FindAncestorQuit;
        }
        return ast.ToFindAncestorResult(Ꮡs.Value.isBoundExpando(n));
    }) != nil;
}

public static void ReportInferenceFallback(this ж<SymbolTrackerImpl> Ꮡs, ж<ast.Node> node) {
    ref var s = ref Ꮡs.DerefOrNull();

    if (!s.state.isolatedDeclarations) {
        return;
    }
    if (ast.GetSourceFileOfNode(node) != s.state.currentSourceFile) {
        return;
    }
    if (s.state.resolver.IsExpandoFunctionDeclarationUnsafe(node)) {
        s.state.reportExpandoFunctionErrors(node);
    }
    if (!Ꮡs.isChildOfBoundExpando(node)) {
        s.state.addDiagnostic(s.getIsolatedDeclarationError(node));
    }
}

public static void ReportLikelyUnsafeImportRequiredError(this ref SymbolTrackerImpl s, @string specifier, @string symbolName) {
    var location = s.errorLocation();
    if (location != nil) {
        if (symbolName != ""u8) {
            s.state.addDiagnostic(createDiagnosticForNode(location, diagnostics.The_inferred_type_of_0_cannot_be_named_without_a_reference_to_2_from_1_This_is_likely_not_portable_A_type_annotation_is_necessary, s.errorDeclarationNameWithFallback(), specifier, symbolName));
        } else {
            s.state.addDiagnostic(createDiagnosticForNode(location, diagnostics.The_inferred_type_of_0_cannot_be_named_without_a_reference_to_1_This_is_likely_not_portable_A_type_annotation_is_necessary, s.errorDeclarationNameWithFallback(), specifier));
        }
    }
}

public static void ReportNonSerializableProperty(this ref SymbolTrackerImpl s, @string propertyName) {
    var location = s.errorLocation();
    if (location != nil) {
        s.state.addDiagnostic(createDiagnosticForNode(location, diagnostics.The_type_of_this_node_cannot_be_serialized_because_its_property_0_cannot_be_serialized, propertyName));
    }
}

public static void ReportNonlocalAugmentation(this ref SymbolTrackerImpl s, ж<ast.SourceFile> containingFile, ж<ast.Symbol> ᏑparentSymbol, ж<ast.Symbol> ᏑaugmentingSymbol) {
    ref var parentSymbol = ref ᏑparentSymbol.DerefOrNull();
    ref var augmentingSymbol = ref ᏑaugmentingSymbol.DerefOrNull();

    var primaryDeclaration = core.Find(parentSymbol.Declarations, (ж<ast.Node> d) => ast.GetSourceFileOfNode(d) == containingFile);
    var augmentingDeclarations = core.Filter(augmentingSymbol.Declarations, (ж<ast.Node> d) => ast.GetSourceFileOfNode(d) != containingFile);
    if (primaryDeclaration != nil && len(augmentingDeclarations) > 0) {
        foreach (var (_, augmentations) in range(augmentingDeclarations)) {
            var diag = createDiagnosticForNode(augmentations, diagnostics.Declaration_augments_declaration_in_another_file_This_cannot_be_serialized);
            var related = createDiagnosticForNode(primaryDeclaration, diagnostics.This_is_the_declaration_being_augmented_Consider_moving_the_augmenting_declaration_into_the_same_file);
            diag.AddRelatedInfo(related);
            s.state.addDiagnostic(diag);
        }
    }
}

public static void ReportPrivateInBaseOfClassExpression(this ref SymbolTrackerImpl s, @string propertyName) {
    var location = s.errorLocation();
    if (location != nil) {
        var diag = createDiagnosticForNode(location, diagnostics.Property_0_of_exported_anonymous_class_type_may_not_be_private_or_protected, propertyName);
        if (ast.IsVariableDeclaration(location.Parent)) {
            var related = createDiagnosticForNode(location, diagnostics.Add_a_type_annotation_to_the_variable_0, s.errorDeclarationNameWithFallback());
            diag.AddRelatedInfo(related);
        }
        s.state.addDiagnostic(diag);
    }
}

public static void ReportTruncationError(this ref SymbolTrackerImpl s) {
    var location = s.errorLocation();
    if (location != nil) {
        s.state.addDiagnostic(createDiagnosticForNode(location, diagnostics.The_inferred_type_of_this_node_exceeds_the_maximum_length_the_compiler_will_serialize_An_explicit_type_annotation_is_needed));
    }
}

internal static ж<ast.Node> errorFallbackNode(this ref SymbolTrackerImpl s) {
    if (len(s.fallbackStack) >= 1) {
        return s.fallbackStack[len(s.fallbackStack) - 1];
    }
    return default;
}

internal static ж<ast.Node> errorLocation(this ref SymbolTrackerImpl s) {
    var location = s.state.errorNameNode;
    if (location == nil) {
        location = s.errorFallbackNode();
    }
    return location;
}

internal static @string errorDeclarationNameWithFallback(this ref SymbolTrackerImpl s) {
    if (s.state.errorNameNode != nil) {
        return scanner.DeclarationNameToString(s.state.errorNameNode);
    }
    if (s.errorFallbackNode() != nil && ast.GetNameOfDeclaration(s.errorFallbackNode()) != nil) {
        return scanner.DeclarationNameToString(ast.GetNameOfDeclaration(s.errorFallbackNode()));
    }
    if (s.errorFallbackNode() != nil && ast.IsExportAssignment(s.errorFallbackNode())) {
        if (s.errorFallbackNode().AsExportAssignment().IsExportEquals) {
            return "export=";
        }
        return "default";
    }
    return "(Missing)";
}

public static bool TrackSymbol(this ref SymbolTrackerImpl s, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTypeParameter) != 0) {
        return false;
    }
    if (s.watchedClassSymbol != nil && Ꮡsymbol == s.watchedClassSymbol) {
        s.classSymbolTracked = true;
        return false;
    }
    var issuedDiagnostic = s.handleSymbolAccessibilityError(s.resolver.IsSymbolAccessible(Ꮡsymbol, enclosingDeclaration, meaning, true));
    return issuedDiagnostic;
}

internal static bool handleSymbolAccessibilityError(this ref SymbolTrackerImpl s, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    if (symbolAccessibilityResult.Accessibility == printer.SymbolAccessibilityAccessible) {
        if (len(symbolAccessibilityResult.AliasesToMakeVisible) > 0) {
            foreach (var (_, @ref) in range(symbolAccessibilityResult.AliasesToMakeVisible)) {
                s.state.lateMarkedStatements = core.AppendIfUnique(s.state.lateMarkedStatements, @ref);
            }
        }
    } else if (symbolAccessibilityResult.Accessibility != printer.SymbolAccessibilityNotResolved) {
        var errorInfo = s.state.getSymbolAccessibilityDiagnostic(symbolAccessibilityResult);
        if (errorInfo != nil) {
            var info = errorInfo.Value;
            var diagNode = symbolAccessibilityResult.ErrorNode;
            if (diagNode == nil) {
                diagNode = errorInfo.errorNode;
            }
            if (info.typeName != nil) {
                s.state.addDiagnostic(createDiagnosticForNode(diagNode, info.diagnosticMessage, scanner.GetTextOfNode(info.typeName), symbolAccessibilityResult.ErrorSymbolName, symbolAccessibilityResult.ErrorModuleName));
            } else {
                s.state.addDiagnostic(createDiagnosticForNode(diagNode, info.diagnosticMessage, symbolAccessibilityResult.ErrorSymbolName, symbolAccessibilityResult.ErrorModuleName));
            }
            return true;
        }
    }
    return false;
}

} partial class declarations_package {

internal static ж<ast.Diagnostic> createDiagnosticForNode(ж<ast.Node> node, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    return checker.NewDiagnosticForNode(node, message, args.ꓸꓸꓸ);
}

partial struct SymbolTrackerSharedState {
    internal slice<ж<ast.Node>> lateMarkedStatements;
    internal slice<ж<ast.Diagnostic>> diagnostics;
    internal GetSymbolAccessibilityDiagnostic getSymbolAccessibilityDiagnostic;
    internal ж<ast.Node> errorNameNode;
    internal bool isolatedDeclarations;
    internal bool stripInternal;
    internal ж<ast.SourceFile> currentSourceFile;
    internal printer.EmitResolver resolver;
    internal Action<ж<ast.Node>> reportExpandoFunctionErrors;
}

} partial class declarationsᴛSymbolTrackerSharedState {

internal static void addDiagnostic(this ref SymbolTrackerSharedState s, ж<ast.Diagnostic> diag) {
    s.diagnostics = append(s.diagnostics, diag);
}

} partial class declarations_package {

public static ж<SymbolTrackerImpl> NewSymbolTracker(DeclarationEmitHost host, printer.EmitResolver resolver, ж<SymbolTrackerSharedState> state) {
    var tracker = Ꮡ(new SymbolTrackerImpl(host: host, resolver: resolver, state: state, getIsolatedDeclarationError: createGetIsolatedDeclarationErrors(resolver)));
    return tracker;
}

} // end declarations_package
