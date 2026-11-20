namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

} partial class checkerᴛChecker {

internal static void checkUnmatchedJSDocParameters(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();
    var jsdocParametersᴷ = new stackAppend<ж<ast.Node>>(moves: true);

    slice<ж<ast.Node>> jsdocParameters = default;
    foreach (var (_, tag) in range(getAllJSDocTags(Ꮡnode))) {
        if (tag.Kind == ast.KindJSDocParameterTag) {
            var name = tag.AsJSDocParameterOrPropertyTag().Name();
            if (ast.IsIdentifier(name) && len(name.Text()) == 0) {
                continue;
            }
            jsdocParameters = append(jsdocParametersᴷ, jsdocParameters, tag);
        }
    }

    if (len(jsdocParameters) == 0) {
        return;
    }

    var isJs = ast.IsInJSFile(Ꮡnode);
    ref var parameters = ref heap<collections.Set<@string>>(new collections.Set<@string>(), out var Ꮡparameters);
    ref var excludedParameters = ref heap<collections.Set<nint>>(new collections.Set<nint>(), out var ᏑexcludedParameters);

    foreach (var (i, param) in range(node.Parameters())) {
        var name = param.AsParameterDeclaration().Name();
        if (ast.IsIdentifier(name)) {
            parameters.Add(name.Text());
        }
        if (ast.IsBindingPattern(name)) {
            excludedParameters.Add(i);
        }
    }
    if (Ꮡc.containsArgumentsReference(Ꮡnode)) {
        if (isJs) {
            nint lastJSDocParamIndex = len(jsdocParameters) - 1;
            var lastJSDocParam = jsdocParameters[lastJSDocParamIndex].AsJSDocParameterOrPropertyTag();
            if (lastJSDocParam == nil || !ast.IsIdentifier(lastJSDocParam.Name())) {
                return;
            }
            if (ᏑexcludedParameters.Has(lastJSDocParamIndex) || Ꮡparameters.Has(lastJSDocParam.Name().Text())) {
                return;
            }
            if ((~lastJSDocParam).TypeExpression == nil || (~lastJSDocParam).TypeExpression.Type() == nil) {
                return;
            }
            if (c.isArrayType(Ꮡc.getTypeFromTypeNode((~lastJSDocParam).TypeExpression.Type()))) {
                return;
            }
            Ꮡc.error(lastJSDocParam.Name(), diagnostics.JSDoc_param_tag_has_name_0_but_there_is_no_parameter_with_that_name_It_would_match_arguments_if_it_had_an_array_type, lastJSDocParam.Name().Text());
        }
    } else {
        jsdocParameters = move2heap(jsdocParametersᴷ, jsdocParameters);
        foreach (var (index, tag) in range(jsdocParameters)) {
            var name = tag.AsJSDocParameterOrPropertyTag().Name();
            var isNameFirst = tag.AsJSDocParameterOrPropertyTag().IsNameFirst;

            if (ᏑexcludedParameters.Has(index) || (ast.IsIdentifier(name) && Ꮡparameters.Has(name.Text()))) {
                continue;
            }

            if (ast.IsQualifiedName(name)) {
                if (isJs) {
                    Ꮡc.error(
                        name, diagnostics.Qualified_name_0_is_not_allowed_without_a_leading_param_object_1,
                        entityNameToString(name),
                        entityNameToString(name.AsQualifiedName().Left));
                }
            } else {
                if (!isNameFirst) {
                    Ꮡc.errorOrSuggestion(
                        isJs, name,
                        diagnostics.JSDoc_param_tag_has_name_0_but_there_is_no_parameter_with_that_name,
                        name.Text());
                }
            }
        }
    }
}

} partial class checker_package {

internal static slice<ж<ast.Node>> getAllJSDocTags(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsJSDoc) == 0) {
        for (var current = Ꮡnode; current != nil; current = ast.GetNextJSDocCommentLocation(current)) {
            var jsdocs = current.JSDoc(nil);
            if (len(jsdocs) == 0) {
                continue;
            }
            var lastJSDoc = jsdocs[len(jsdocs) - 1].AsJSDoc();
            if (lastJSDoc.Tags != nil) {
                return lastJSDoc.Tags.Nodes;
            }
        }
    }
    return default;
}

} // end checker_package
