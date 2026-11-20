namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class estransforms_package {

internal static bool isClassThisAssignmentBlock(ж<printer.EmitContext> ᏑemitContext, ж<ast.Node> Ꮡnode) {
    ref var emitContext = ref ᏑemitContext.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (ast.IsClassStaticBlockDeclaration(Ꮡnode)) {
        var n = node.AsClassStaticBlockDeclaration();
        var body = (~n).Body.AsBlock();
        if (len((~body).Statements.Nodes) == 1) {
            var statement = (~body).Statements.Nodes[0];
            if (ast.IsExpressionStatement(statement)) {
                var expression = statement.Expression();
                if (ast.IsAssignmentExpression(expression, true)) {
                    var binary = expression.AsBinaryExpression();
                    return ast.IsIdentifier(binary.Left) && emitContext.ClassThis(Ꮡnode) == binary.Left && binary.Right.Kind == ast.KindThisKeyword;
                }
            }
        }
    }
    return false;
}

} // end estransforms_package
