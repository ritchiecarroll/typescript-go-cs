global using GetSymbolAccessibilityDiagnostic = System.Func<go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.SymbolAccessibilityResult, go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package.SymbolAccessibilityDiagnostic>>;

namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class declarations_package {

partial struct SymbolAccessibilityDiagnostic {
    internal ж<ast.Node> errorNode;
    internal ж<diagnostics.Message> diagnosticMessage;
    internal ж<ast.Node> typeName;
}

internal static GetSymbolAccessibilityDiagnostic wrapSimpleDiagnosticSelector(ж<ast.Node> node, Func<ж<ast.Node>, printer.SymbolAccessibilityResult, ж<diagnostics.Message>> selector) {
    return (printer.SymbolAccessibilityResult symbolAccessibilityResult) => {
        var diagnosticMessage = selector(node, symbolAccessibilityResult);
        if (diagnosticMessage == nil) {
            return default;
        }
        return Ꮡ(new SymbolAccessibilityDiagnostic(
            errorNode: node,
            diagnosticMessage: diagnosticMessage,
            typeName: ast.GetNameOfDeclaration(node)
        ));
    };
}

internal static GetSymbolAccessibilityDiagnostic wrapNamedDiagnosticSelector(ж<ast.Node> node, Func<ж<ast.Node>, printer.SymbolAccessibilityResult, ж<diagnostics.Message>> selector) {
    return (printer.SymbolAccessibilityResult symbolAccessibilityResult) => {
        var diagnosticMessage = selector(node, symbolAccessibilityResult);
        if (diagnosticMessage == nil) {
            return default;
        }
        var name = ast.GetNameOfDeclaration(node);
        return Ꮡ(new SymbolAccessibilityDiagnostic(
            errorNode: name,
            diagnosticMessage: diagnosticMessage,
            typeName: name
        ));
    };
}

internal static GetSymbolAccessibilityDiagnostic wrapFallbackErrorDiagnosticSelector(ж<ast.Node> node, Func<ж<ast.Node>, printer.SymbolAccessibilityResult, ж<diagnostics.Message>> selector) {
    return (printer.SymbolAccessibilityResult symbolAccessibilityResult) => {
        var diagnosticMessage = selector(node, symbolAccessibilityResult);
        if (diagnosticMessage == nil) {
            return default;
        }
        var errorNode = ast.GetNameOfDeclaration(node);
        if (errorNode == nil) {
            errorNode = node;
        }
        return Ꮡ(new SymbolAccessibilityDiagnostic(
            errorNode: errorNode,
            diagnosticMessage: diagnosticMessage
        ));
    };
}

internal static ж<diagnostics.Message> selectDiagnosticBasedOnModuleName(printer.SymbolAccessibilityResult symbolAccessibilityResult, ж<diagnostics.Message> moduleNotNameable, ж<diagnostics.Message> privateModule, ж<diagnostics.Message> nonModule) {
    if (len(symbolAccessibilityResult.ErrorModuleName) > 0) {
        if (symbolAccessibilityResult.Accessibility == printer.SymbolAccessibilityCannotBeNamed) {
            return moduleNotNameable;
        }
        return privateModule;
    }
    return nonModule;
}

internal static ж<diagnostics.Message> selectDiagnosticBasedOnModuleNameNoNameCheck(printer.SymbolAccessibilityResult symbolAccessibilityResult, ж<diagnostics.Message> privateModule, ж<diagnostics.Message> nonModule) {
    if (len(symbolAccessibilityResult.ErrorModuleName) > 0) {
        return privateModule;
    }
    return nonModule;
}

internal static GetSymbolAccessibilityDiagnostic createGetSymbolAccessibilityDiagnosticForNodeName(ж<ast.Node> node) {
    if (ast.IsSetAccessorDeclaration(node) || ast.IsGetAccessorDeclaration(node)) {
        return wrapSimpleDiagnosticSelector(node, getAccessorNameVisibilityDiagnosticMessage);
    } else if (ast.IsMethodDeclaration(node) || ast.IsMethodSignatureDeclaration(node)) {
        return wrapSimpleDiagnosticSelector(node, getMethodNameVisibilityDiagnosticMessage);
    } else {
        return createGetSymbolAccessibilityDiagnosticForNode(node);
    }
}

internal static ж<diagnostics.Message> getAccessorNameVisibilityDiagnosticMessage(ж<ast.Node> Ꮡnode, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsStatic(Ꮡnode)) {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Public_static_property_0_of_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Public_static_property_0_of_exported_class_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Public_static_property_0_of_exported_class_has_or_is_using_private_name_1);
    } else if (node.Parent.Kind == ast.KindClassDeclaration) {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Public_property_0_of_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Public_property_0_of_exported_class_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Public_property_0_of_exported_class_has_or_is_using_private_name_1);
    } else {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Property_0_of_exported_interface_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Property_0_of_exported_interface_has_or_is_using_private_name_1);
    }
}

internal static ж<diagnostics.Message> getMethodNameVisibilityDiagnosticMessage(ж<ast.Node> Ꮡnode, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsStatic(Ꮡnode)) {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Public_static_method_0_of_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Public_static_method_0_of_exported_class_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Public_static_method_0_of_exported_class_has_or_is_using_private_name_1);
    } else if (node.Parent.Kind == ast.KindClassDeclaration) {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Public_method_0_of_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Public_method_0_of_exported_class_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Public_method_0_of_exported_class_has_or_is_using_private_name_1);
    } else {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Method_0_of_exported_interface_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Method_0_of_exported_interface_has_or_is_using_private_name_1);
    }
}

internal static GetSymbolAccessibilityDiagnostic createGetSymbolAccessibilityDiagnosticForNode(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsVariableDeclaration(Ꮡnode) || ast.IsPropertyDeclaration(Ꮡnode) || ast.IsPropertySignatureDeclaration(Ꮡnode) || ast.IsPropertyAccessExpression(Ꮡnode) || ast.IsElementAccessExpression(Ꮡnode) || ast.IsBinaryExpression(Ꮡnode) || ast.IsBindingElement(Ꮡnode) || ast.IsConstructorDeclaration(Ꮡnode)) {
        return wrapSimpleDiagnosticSelector(Ꮡnode, getVariableDeclarationTypeVisibilityDiagnosticMessage);
    } else if (ast.IsSetAccessorDeclaration(Ꮡnode) || ast.IsGetAccessorDeclaration(Ꮡnode)) {
        return wrapNamedDiagnosticSelector(Ꮡnode, getAccessorDeclarationTypeVisibilityDiagnosticMessage);
    } else if (ast.IsConstructSignatureDeclaration(Ꮡnode) || ast.IsCallSignatureDeclaration(Ꮡnode) || ast.IsMethodDeclaration(Ꮡnode) || ast.IsMethodSignatureDeclaration(Ꮡnode) || ast.IsFunctionDeclaration(Ꮡnode) || ast.IsIndexSignatureDeclaration(Ꮡnode)) {
        return wrapFallbackErrorDiagnosticSelector(Ꮡnode, getReturnTypeVisibilityDiagnosticMessage);
    } else if (ast.IsParameterDeclaration(Ꮡnode)) {
        if (ast.IsParameterPropertyDeclaration(Ꮡnode, node.Parent) && ast.HasSyntacticModifier(node.Parent, ast.ModifierFlagsPrivate)) {
            return wrapSimpleDiagnosticSelector(Ꮡnode, getVariableDeclarationTypeVisibilityDiagnosticMessage);
        }
        return wrapSimpleDiagnosticSelector(Ꮡnode, getParameterDeclarationTypeVisibilityDiagnosticMessage);
    } else if (ast.IsTypeParameterDeclaration(Ꮡnode)) {
        return wrapSimpleDiagnosticSelector(Ꮡnode, getTypeParameterConstraintVisibilityDiagnosticMessage);
    } else if (ast.IsExpressionWithTypeArguments(Ꮡnode)) {
        return (printer.SymbolAccessibilityResult symbolAccessibilityResult) => {
            ж<diagnostics.Message> diagnosticMessage = default;
            if (ast.IsClassDeclaration(Ꮡnode.Value.Parent.Parent)) {
                if (ast.IsHeritageClause(Ꮡnode.Value.Parent) && Ꮡnode.Value.Parent.AsHeritageClause().Token == ast.KindImplementsKeyword) {
                    diagnosticMessage = diagnostics.Implements_clause_of_exported_class_0_has_or_is_using_private_name_1;
                } else {
                    if (Ꮡnode.Value.Parent.Parent.Name() != nil) {
                        diagnosticMessage = diagnostics.X_extends_clause_of_exported_class_0_has_or_is_using_private_name_1;
                    } else {
                        diagnosticMessage = diagnostics.X_extends_clause_of_exported_class_has_or_is_using_private_name_0;
                    }
                }
            } else {
                diagnosticMessage = diagnostics.X_extends_clause_of_exported_interface_0_has_or_is_using_private_name_1;
            }

            return Ꮡ(new SymbolAccessibilityDiagnostic(
                diagnosticMessage: diagnosticMessage,
                errorNode: Ꮡnode,
                typeName: ast.GetNameOfDeclaration(Ꮡnode.Value.Parent.Parent)
            ));
        };
    } else if (ast.IsImportEqualsDeclaration(Ꮡnode)) {
        return wrapSimpleDiagnosticSelector(Ꮡnode, (ж<ast.Node> _Δp0, printer.SymbolAccessibilityResult _Δp1) => diagnostics.Import_declaration_0_is_using_private_name_1);
    } else if (ast.IsTypeAliasDeclaration(Ꮡnode) || ast.IsJSTypeAliasDeclaration(Ꮡnode)) {
        return (printer.SymbolAccessibilityResult symbolAccessibilityResult) => {
            var diagnosticMessage = selectDiagnosticBasedOnModuleNameNoNameCheck(
                symbolAccessibilityResult,
                diagnostics.Exported_type_alias_0_has_or_is_using_private_name_1_from_module_2,
                diagnostics.Exported_type_alias_0_has_or_is_using_private_name_1);
            var errorNode = Ꮡnode.Value.Type();
            var typeName = Ꮡnode.Value.Name();
            return Ꮡ(new SymbolAccessibilityDiagnostic(
                errorNode: errorNode,
                diagnosticMessage: diagnosticMessage,
                typeName: typeName
            ));
        };
    } else if (ast.IsCallExpression(Ꮡnode)) {
        return (printer.SymbolAccessibilityResult symbolAccessibilityResult) => {
            var diagnosticMessage = selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Exported_variable_0_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
                diagnostics.Exported_variable_0_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Exported_variable_0_has_or_is_using_private_name_1);
            var errorNode = Ꮡnode.Value.Arguments()[1];
            var typeName = Ꮡnode.Value.Arguments()[1];
            return Ꮡ(new SymbolAccessibilityDiagnostic(
                errorNode: errorNode,
                diagnosticMessage: diagnosticMessage,
                typeName: typeName
            ));
        };
    } else {
        throw panic("Attempted to set a declaration diagnostic context for unhandled node kind: " + node.Kind.String());
    }
}

internal static ж<diagnostics.Message> getVariableDeclarationTypeVisibilityDiagnosticMessage(ж<ast.Node> Ꮡnode, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindVariableDeclaration || node.Kind == ast.KindBindingElement) {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Exported_variable_0_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Exported_variable_0_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Exported_variable_0_has_or_is_using_private_name_1);
    } else if (node.Kind == ast.KindPropertyDeclaration || node.Kind == ast.KindPropertyAccessExpression || node.Kind == ast.KindElementAccessExpression || node.Kind == ast.KindBinaryExpression || node.Kind == ast.KindPropertySignature || (node.Kind == ast.KindParameter && ast.HasSyntacticModifier(node.Parent, ast.ModifierFlagsPrivate))) {
        if (ast.IsStatic(Ꮡnode)) {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Public_static_property_0_of_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
                diagnostics.Public_static_property_0_of_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Public_static_property_0_of_exported_class_has_or_is_using_private_name_1);
        } else if (node.Parent.Kind == ast.KindClassDeclaration || node.Kind == ast.KindParameter) {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Public_property_0_of_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
                diagnostics.Public_property_0_of_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Public_property_0_of_exported_class_has_or_is_using_private_name_1);
        } else {
            return selectDiagnosticBasedOnModuleNameNoNameCheck(
                symbolAccessibilityResult,
                diagnostics.Property_0_of_exported_interface_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Property_0_of_exported_interface_has_or_is_using_private_name_1);
        }
    }
    return default;
}

internal static ж<diagnostics.Message> getAccessorDeclarationTypeVisibilityDiagnosticMessage(ж<ast.Node> Ꮡnode, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindSetAccessor) {
        if (ast.IsStatic(Ꮡnode)) {
            return selectDiagnosticBasedOnModuleNameNoNameCheck(
                symbolAccessibilityResult,
                diagnostics.Parameter_type_of_public_static_setter_0_from_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Parameter_type_of_public_static_setter_0_from_exported_class_has_or_is_using_private_name_1);
        } else {
            return selectDiagnosticBasedOnModuleNameNoNameCheck(
                symbolAccessibilityResult,
                diagnostics.Parameter_type_of_public_setter_0_from_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Parameter_type_of_public_setter_0_from_exported_class_has_or_is_using_private_name_1);
        }
    } else {
        if (ast.IsStatic(Ꮡnode)) {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Return_type_of_public_static_getter_0_from_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
                diagnostics.Return_type_of_public_static_getter_0_from_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Return_type_of_public_static_getter_0_from_exported_class_has_or_is_using_private_name_1);
        } else {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Return_type_of_public_getter_0_from_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
                diagnostics.Return_type_of_public_getter_0_from_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Return_type_of_public_getter_0_from_exported_class_has_or_is_using_private_name_1);
        }
    }
}

internal static ж<diagnostics.Message> getReturnTypeVisibilityDiagnosticMessage(ж<ast.Node> Ꮡnode, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindConstructSignature: {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Return_type_of_constructor_signature_from_exported_interface_has_or_is_using_name_0_from_private_module_1,
            diagnostics.Return_type_of_constructor_signature_from_exported_interface_has_or_is_using_private_name_0);
    }
    case var ᴛ when ᴛ == ast.KindCallSignature: {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Return_type_of_call_signature_from_exported_interface_has_or_is_using_name_0_from_private_module_1,
            diagnostics.Return_type_of_call_signature_from_exported_interface_has_or_is_using_private_name_0);
    }
    case var ᴛ when ᴛ == ast.KindIndexSignature: {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Return_type_of_index_signature_from_exported_interface_has_or_is_using_name_0_from_private_module_1,
            diagnostics.Return_type_of_index_signature_from_exported_interface_has_or_is_using_private_name_0);
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature: {
        if (ast.IsStatic(Ꮡnode)) {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Return_type_of_public_static_method_from_exported_class_has_or_is_using_name_0_from_external_module_1_but_cannot_be_named,
                diagnostics.Return_type_of_public_static_method_from_exported_class_has_or_is_using_name_0_from_private_module_1,
                diagnostics.Return_type_of_public_static_method_from_exported_class_has_or_is_using_private_name_0);
        } else if (node.Parent.Kind == ast.KindClassDeclaration) {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Return_type_of_public_method_from_exported_class_has_or_is_using_name_0_from_external_module_1_but_cannot_be_named,
                diagnostics.Return_type_of_public_method_from_exported_class_has_or_is_using_name_0_from_private_module_1,
                diagnostics.Return_type_of_public_method_from_exported_class_has_or_is_using_private_name_0);
        } else {
            return selectDiagnosticBasedOnModuleNameNoNameCheck(
                symbolAccessibilityResult,
                diagnostics.Return_type_of_method_from_exported_interface_has_or_is_using_name_0_from_private_module_1,
                diagnostics.Return_type_of_method_from_exported_interface_has_or_is_using_private_name_0);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Return_type_of_exported_function_has_or_is_using_name_0_from_external_module_1_but_cannot_be_named,
            diagnostics.Return_type_of_exported_function_has_or_is_using_name_0_from_private_module_1,
            diagnostics.Return_type_of_exported_function_has_or_is_using_private_name_0);
    }
    default: {
        throw panic("This is unknown kind for signature: " + node.Kind.String());
        break;
    }}

}

internal static ж<diagnostics.Message> getParameterDeclarationTypeVisibilityDiagnosticMessage(ж<ast.Node> Ꮡnode, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Parent.Kind) {
    case var ᴛ when ᴛ == ast.KindConstructor: {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Parameter_0_of_constructor_from_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Parameter_0_of_constructor_from_exported_class_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Parameter_0_of_constructor_from_exported_class_has_or_is_using_private_name_1);
    }
    case var ᴛ when ᴛ == ast.KindConstructSignature || ᴛ == ast.KindConstructorType: {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Parameter_0_of_constructor_signature_from_exported_interface_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Parameter_0_of_constructor_signature_from_exported_interface_has_or_is_using_private_name_1);
    }
    case var ᴛ when ᴛ == ast.KindCallSignature: {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Parameter_0_of_call_signature_from_exported_interface_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Parameter_0_of_call_signature_from_exported_interface_has_or_is_using_private_name_1);
    }
    case var ᴛ when ᴛ == ast.KindIndexSignature: {
        return selectDiagnosticBasedOnModuleNameNoNameCheck(
            symbolAccessibilityResult,
            diagnostics.Parameter_0_of_index_signature_from_exported_interface_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Parameter_0_of_index_signature_from_exported_interface_has_or_is_using_private_name_1);
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature: {
        if (ast.IsStatic(node.Parent)) {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Parameter_0_of_public_static_method_from_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
                diagnostics.Parameter_0_of_public_static_method_from_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Parameter_0_of_public_static_method_from_exported_class_has_or_is_using_private_name_1);
        } else if (node.Parent.Parent.Kind == ast.KindClassDeclaration) {
            return selectDiagnosticBasedOnModuleName(
                symbolAccessibilityResult,
                diagnostics.Parameter_0_of_public_method_from_exported_class_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
                diagnostics.Parameter_0_of_public_method_from_exported_class_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Parameter_0_of_public_method_from_exported_class_has_or_is_using_private_name_1);
        } else {
            return selectDiagnosticBasedOnModuleNameNoNameCheck(
                symbolAccessibilityResult,
                diagnostics.Parameter_0_of_method_from_exported_interface_has_or_is_using_name_1_from_private_module_2,
                diagnostics.Parameter_0_of_method_from_exported_interface_has_or_is_using_private_name_1);
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration || ᴛ == ast.KindFunctionType || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindFunctionExpression: {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Parameter_0_of_exported_function_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Parameter_0_of_exported_function_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Parameter_0_of_exported_function_has_or_is_using_private_name_1);
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor || ᴛ == ast.KindGetAccessor: {
        return selectDiagnosticBasedOnModuleName(
            symbolAccessibilityResult,
            diagnostics.Parameter_0_of_accessor_has_or_is_using_name_1_from_external_module_2_but_cannot_be_named,
            diagnostics.Parameter_0_of_accessor_has_or_is_using_name_1_from_private_module_2,
            diagnostics.Parameter_0_of_accessor_has_or_is_using_private_name_1);
    }
    default: {
        throw panic("Unknown parent for parameter: " + node.Parent.Kind.String());
        break;
    }}

}

internal static ж<diagnostics.Message> getTypeParameterConstraintVisibilityDiagnosticMessage(ж<ast.Node> Ꮡnode, printer.SymbolAccessibilityResult symbolAccessibilityResult) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Parent.Kind) {
    case var ᴛ when ᴛ == ast.KindClassDeclaration: {
        return diagnostics.Type_parameter_0_of_exported_class_has_or_is_using_private_name_1;
    }
    case var ᴛ when ᴛ == ast.KindInterfaceDeclaration: {
        return diagnostics.Type_parameter_0_of_exported_interface_has_or_is_using_private_name_1;
    }
    case var ᴛ when ᴛ == ast.KindMappedType: {
        return diagnostics.Type_parameter_0_of_exported_mapped_object_type_is_using_private_name_1;
    }
    case var ᴛ when ᴛ == ast.KindConstructorType || ᴛ == ast.KindConstructSignature: {
        return diagnostics.Type_parameter_0_of_constructor_signature_from_exported_interface_has_or_is_using_private_name_1;
    }
    case var ᴛ when ᴛ == ast.KindCallSignature: {
        return diagnostics.Type_parameter_0_of_call_signature_from_exported_interface_has_or_is_using_private_name_1;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindMethodSignature: {
        if (ast.IsStatic(node.Parent)) {
            return diagnostics.Type_parameter_0_of_public_static_method_from_exported_class_has_or_is_using_private_name_1;
        } else if (node.Parent.Parent.Kind == ast.KindClassDeclaration) {
            return diagnostics.Type_parameter_0_of_public_method_from_exported_class_has_or_is_using_private_name_1;
        } else {
            return diagnostics.Type_parameter_0_of_method_from_exported_interface_has_or_is_using_private_name_1;
        }
        break;
    }
    case var ᴛ when ᴛ == ast.KindFunctionType || ᴛ == ast.KindFunctionDeclaration: {
        return diagnostics.Type_parameter_0_of_exported_function_has_or_is_using_private_name_1;
    }
    case var ᴛ when ᴛ == ast.KindInferType: {
        return diagnostics.Extends_clause_for_inferred_type_0_has_or_is_using_private_name_1;
    }
    case var ᴛ when ᴛ == ast.KindTypeAliasDeclaration || ᴛ == ast.KindJSTypeAliasDeclaration: {
        return diagnostics.Type_parameter_0_of_exported_type_alias_has_or_is_using_private_name_1;
    }
    default: {
        throw panic("This is unknown parent for type parameter: " + node.Parent.Kind.String());
        break;
    }}

}

internal static ж<diagnostics.Message> getRelatedSuggestionByDeclarationKind(ast.Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == ast.KindArrowFunction: {
        return diagnostics.Add_a_return_type_to_the_function_expression;
    }
    case var ᴛ when ᴛ == ast.KindFunctionExpression: {
        return diagnostics.Add_a_return_type_to_the_function_expression;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        return diagnostics.Add_a_return_type_to_the_method;
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        return diagnostics.Add_a_return_type_to_the_get_accessor_declaration;
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        return diagnostics.Add_a_type_to_parameter_of_the_set_accessor_declaration;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        return diagnostics.Add_a_return_type_to_the_function_declaration;
    }
    case var ᴛ when ᴛ == ast.KindConstructSignature: {
        return diagnostics.Add_a_return_type_to_the_function_declaration;
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        return diagnostics.Add_a_type_annotation_to_the_parameter_0;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        return diagnostics.Add_a_type_annotation_to_the_variable_0;
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return diagnostics.Add_a_type_annotation_to_the_property_0;
    }
    case var ᴛ when ᴛ == ast.KindPropertySignature: {
        return diagnostics.Add_a_type_annotation_to_the_property_0;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return diagnostics.Move_the_expression_in_default_export_to_a_variable_and_add_a_type_annotation_to_it;
    }
    default: {
        return default;
    }}

}

internal static ж<diagnostics.Message> getErrorByDeclarationKind(ast.Kind kind) {
    switch (kind) {
    case var ᴛ when ᴛ == ast.KindFunctionExpression: {
        return diagnostics.Function_must_have_an_explicit_return_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindFunctionDeclaration: {
        return diagnostics.Function_must_have_an_explicit_return_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindArrowFunction: {
        return diagnostics.Function_must_have_an_explicit_return_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindMethodDeclaration: {
        return diagnostics.Method_must_have_an_explicit_return_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindConstructSignature: {
        return diagnostics.Method_must_have_an_explicit_return_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindGetAccessor: {
        return diagnostics.At_least_one_accessor_must_have_an_explicit_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindSetAccessor: {
        return diagnostics.At_least_one_accessor_must_have_an_explicit_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindParameter: {
        return diagnostics.Parameter_must_have_an_explicit_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindVariableDeclaration: {
        return diagnostics.Variable_must_have_an_explicit_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindPropertyDeclaration: {
        return diagnostics.Property_must_have_an_explicit_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindPropertySignature: {
        return diagnostics.Property_must_have_an_explicit_type_annotation_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindComputedPropertyName: {
        return diagnostics.Computed_property_names_on_class_or_object_literals_cannot_be_inferred_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindSpreadAssignment: {
        return diagnostics.Objects_that_contain_spread_assignments_can_t_be_inferred_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindShorthandPropertyAssignment: {
        return diagnostics.Objects_that_contain_shorthand_properties_can_t_be_inferred_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindArrayLiteralExpression: {
        return diagnostics.Only_const_arrays_can_be_inferred_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindExportAssignment: {
        return diagnostics.Default_exports_can_t_be_inferred_with_isolatedDeclarations;
    }
    case var ᴛ when ᴛ == ast.KindSpreadElement: {
        return diagnostics.Arrays_with_spread_elements_can_t_inferred_with_isolatedDeclarations;
    }
    default: {
        return default;
    }}

}

internal static bool isDeclarationEnoughForErrors(ж<ast.Node> node) {
    return ast.IsExportAssignment(node) || ast.IsStatement(node) || ast.IsVariableDeclaration(node) || ast.IsPropertyDeclaration(node) || ast.IsParameterDeclaration(node);
}

internal static bool isFunctionLikeAndNotConstructor(ж<ast.Node> node) {
    return ast.IsFunctionLikeDeclaration(node) && !ast.IsConstructorDeclaration(node);
}

internal static ж<ast.Node> findNearestDeclaration(ж<ast.Node> node) {
    var result = ast.FindAncestor(node, isDeclarationEnoughForErrors);
    if (result == nil) {
        return default;
    }
    if (ast.IsExportAssignment(result)) {
        return result;
    }
    if (ast.IsReturnStatement(result)) {
        return ast.FindAncestor(result, isFunctionLikeAndNotConstructor);
    }
    if (ast.IsStatement(result)) {
        return default;
    }
    return result;
}

internal static ж<ast.Diagnostic> createEntityInTypeNodeError(ж<ast.Node> node) {
    var diag = createDiagnosticForNode(node, diagnostics.Type_containing_private_name_0_can_t_be_used_with_isolatedDeclarations, scanner.GetTextOfNode(node));
    addParentDeclarationRelatedInfo(node, diag);
    return diag;
}

internal static void addParentDeclarationRelatedInfo(ж<ast.Node> node, ж<ast.Diagnostic> diag) {
    var parentDeclaration = findNearestDeclaration(node);
    if (parentDeclaration == nil) {
        return;
    }
    @string targetStr = "";
    if (!ast.IsExportAssignment(parentDeclaration) && parentDeclaration.Name() != nil) {
        targetStr = scanner.GetTextOfNode(parentDeclaration.Name());
    }
    diag.AddRelatedInfo(createDiagnosticForNode(parentDeclaration, getRelatedSuggestionByDeclarationKind(parentDeclaration.Kind), targetStr));
}

internal static ж<ast.Diagnostic> createAccessorTypeError(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var allDeclarations = ast.GetAllAccessorDeclarationsForDeclaration(Ꮡnode, node.Symbol().Declarations);
    var getAccessor = allDeclarations.GetAccessor;
    var setAccessor = allDeclarations.SetAccessor;
    var targetNode = Ꮡnode;
    if (ast.IsSetAccessorDeclaration(Ꮡnode) && len(node.Parameters()) > 0) {
        targetNode = node.Parameters()[0];
    }
    var diag = createDiagnosticForNode(targetNode, getErrorByDeclarationKind(node.Kind));
    if (setAccessor != nil) {
        diag.AddRelatedInfo(createDiagnosticForNode(setAccessor.of(ast.SetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getRelatedSuggestionByDeclarationKind(setAccessor.Kind)));
    }
    if (getAccessor != nil) {
        diag.AddRelatedInfo(createDiagnosticForNode(getAccessor.of(ast.GetAccessorDeclaration.ᏑAccessorDeclarationBase).of(ast.AccessorDeclarationBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), getRelatedSuggestionByDeclarationKind(getAccessor.Kind)));
    }
    return diag;
}

internal static ж<ast.Diagnostic> createObjectLiteralError(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var diag = createDiagnosticForNode(Ꮡnode, getErrorByDeclarationKind(node.Kind));
    addParentDeclarationRelatedInfo(Ꮡnode, diag);
    return diag;
}

internal static ж<ast.Diagnostic> createArrayLiteralError(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var diag = createDiagnosticForNode(Ꮡnode, getErrorByDeclarationKind(node.Kind));
    addParentDeclarationRelatedInfo(Ꮡnode, diag);
    return diag;
}

internal static ж<ast.Diagnostic> createReturnTypeError(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var diag = createDiagnosticForNode(Ꮡnode, getErrorByDeclarationKind(node.Kind));
    addParentDeclarationRelatedInfo(Ꮡnode, diag);
    diag.AddRelatedInfo(createDiagnosticForNode(Ꮡnode, getRelatedSuggestionByDeclarationKind(node.Kind)));
    return diag;
}

internal static ж<ast.Diagnostic> createBindingElementError(ж<ast.Node> node) {
    return createDiagnosticForNode(node, diagnostics.Binding_elements_with_initializers_can_t_be_exported_directly_with_isolatedDeclarations);
}

internal static ж<ast.Diagnostic> createVariableOrPropertyError(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var diag = createDiagnosticForNode(Ꮡnode, getErrorByDeclarationKind(node.Kind));
    diag.AddRelatedInfo(createDiagnosticForNode(Ꮡnode, getRelatedSuggestionByDeclarationKind(node.Kind), scanner.GetTextOfNode(node.Name())));
    return diag;
}

internal static ж<ast.Diagnostic> createExpressionError(ж<ast.Node> node) {
    return createExpressionErrorEx(node, nil);
}

internal static ж<ast.Diagnostic> createClassExpressionError(ж<ast.Node> node) {
    return createExpressionErrorEx(node, diagnostics.Inference_from_class_expressions_is_not_supported_with_isolatedDeclarations);
}

internal static ast.FindAncestorResult isParentForIDDIagnostic(ж<ast.Node> node) {
    if (ast.IsExportAssignment(node)) {
        return ast.FindAncestorTrue;
    }
    if (ast.IsStatement(node)) {
        return ast.FindAncestorQuit;
    }
    return ast.ToFindAncestorResult(!ast.IsParenthesizedExpression(node) && !ast.IsAssertionExpression(node));
}

internal static ж<ast.Diagnostic> createExpressionErrorEx(ж<ast.Node> Ꮡnode, ж<diagnostics.Message> ᏑdiagnosticMessage) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var diagnosticMessage = ref ᏑdiagnosticMessage.DerefOrNull();

    var parentDeclaration = findNearestDeclaration(Ꮡnode);
    if (parentDeclaration == nil) {
        if (ᏑdiagnosticMessage == nil) {
            ᏑdiagnosticMessage = diagnostics.Expression_type_can_t_be_inferred_with_isolatedDeclarations; diagnosticMessage = ref ᏑdiagnosticMessage.DerefOrNull();
        }
        return createDiagnosticForNode(Ꮡnode, ᏑdiagnosticMessage);
    }

    @string targetStr = "";
    if (!ast.IsExportAssignment(parentDeclaration) && parentDeclaration.Name() != nil) {
        targetStr = scanner.GetTextOfNode(parentDeclaration.Name());
    }
    var parent = ast.FindAncestorOrQuit(node.Parent, isParentForIDDIagnostic);

    if (parentDeclaration == parent) {
        if (ᏑdiagnosticMessage == nil) {
            ᏑdiagnosticMessage = getErrorByDeclarationKind(parentDeclaration.Kind); diagnosticMessage = ref ᏑdiagnosticMessage.DerefOrNull();
        }
        var diagΔ1 = createDiagnosticForNode(Ꮡnode, ᏑdiagnosticMessage);
        diagΔ1.AddRelatedInfo(createDiagnosticForNode(parentDeclaration, getRelatedSuggestionByDeclarationKind(parentDeclaration.Kind), targetStr));
        return diagΔ1;
    }
    if (ᏑdiagnosticMessage == nil) {
        ᏑdiagnosticMessage = diagnostics.Expression_type_can_t_be_inferred_with_isolatedDeclarations; diagnosticMessage = ref ᏑdiagnosticMessage.DerefOrNull();
    }
    var diag = createDiagnosticForNode(Ꮡnode, ᏑdiagnosticMessage);
    diag.AddRelatedInfo(createDiagnosticForNode(parentDeclaration, getRelatedSuggestionByDeclarationKind(parentDeclaration.Kind), targetStr));
    diag.AddRelatedInfo(createDiagnosticForNode(Ꮡnode, diagnostics.Add_satisfies_and_a_type_assertion_to_this_expression_satisfies_T_as_T_to_make_the_type_explicit));
    return diag;
}

internal static Func<ж<ast.Node>, ж<ast.Diagnostic>> createGetIsolatedDeclarationErrors(printer.EmitResolver resolver) {
    ж<ast.Diagnostic> createParameterError(ж<ast.Node> node) {
        if (ast.IsSetAccessorDeclaration(node.Parent)) {
            return createAccessorTypeError(node.Parent);
        }
        var addUndefined = resolver.RequiresAddingImplicitUndefinedUnsafe(node, nil, nil);
        if (!addUndefined && node.Initializer() != nil) {
            return createExpressionError(node.Initializer());
        }
        var message = getErrorByDeclarationKind(node.Kind);
        if (addUndefined) {
            message = diagnostics.Declaration_emit_for_this_parameter_requires_implicitly_adding_undefined_to_its_type_This_is_not_supported_with_isolatedDeclarations;
        }
        var diag = createDiagnosticForNode(node, message);
        @string targetStr = scanner.GetTextOfNode(node.Name());
        diag.AddRelatedInfo(createDiagnosticForNode(node, getRelatedSuggestionByDeclarationKind(node.Kind), targetStr));
        return diag;
    }

    var createParameterErrorʗ1 = createParameterError;
    return (ж<ast.Node> node) => {
        var heritageClause = ast.FindAncestor(node, ast.IsHeritageClause);
        if (heritageClause != nil) {
            return createDiagnosticForNode(node, diagnostics.Extends_clause_can_t_contain_an_expression_with_isolatedDeclarations);
        }
        if (ast.IsPartOfTypeNode(node) || ast.IsTypeQueryNode(node)) {
            return createEntityInTypeNodeError(node);
        }
        if (ast.IsEntityName(node) || ast.IsEntityNameExpression(node)) {
            return createEntityInTypeNodeError(node);
        }
        switch (node.Kind) {
        case var ᴛ when ᴛ == ast.KindGetAccessor || ᴛ == ast.KindSetAccessor: {
            return createAccessorTypeError(node);
        }
        case var ᴛ when ᴛ == ast.KindComputedPropertyName || ᴛ == ast.KindShorthandPropertyAssignment || ᴛ == ast.KindSpreadAssignment: {
            return createObjectLiteralError(node);
        }
        case var ᴛ when ᴛ == ast.KindArrayLiteralExpression || ᴛ == ast.KindSpreadElement: {
            return createArrayLiteralError(node);
        }
        case var ᴛ when ᴛ == ast.KindMethodDeclaration || ᴛ == ast.KindConstructSignature || ᴛ == ast.KindFunctionExpression || ᴛ == ast.KindArrowFunction || ᴛ == ast.KindFunctionDeclaration: {
            return createReturnTypeError(node);
        }
        case var ᴛ when ᴛ == ast.KindBindingElement: {
            return createBindingElementError(node);
        }
        case var ᴛ when ᴛ == ast.KindPropertyDeclaration || ᴛ == ast.KindVariableDeclaration: {
            return createVariableOrPropertyError(node);
        }
        case var ᴛ when ᴛ == ast.KindParameter: {
            return createParameterErrorʗ1(node);
        }
        case var ᴛ when ᴛ == ast.KindPropertyAssignment: {
            return createExpressionError(node.Initializer());
        }
        case var ᴛ when ᴛ == ast.KindClassExpression: {
            return createClassExpressionError(node);
        }
        default: {
            return createExpressionError(node);
        }}

    };
}

} // end declarations_package
