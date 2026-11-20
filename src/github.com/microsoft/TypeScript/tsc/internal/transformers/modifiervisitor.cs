namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static transformers_package;

partial class transformers_package {

partial struct modifierVisitor {
    /*embed*/ public Transformer Transformer;
    public ast.ModifierFlags AllowedModifiers;
}

} partial class transformersᴛmodifierVisitor {

internal static ж<ast.Node> visit(this ref modifierVisitor v, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var flags = ast.ModifierToFlag(node.Kind);
    if (flags != ast.ModifierFlagsNone && (ast.ModifierFlags)(flags & v.AllowedModifiers) == 0) {
        return default;
    }
    return Ꮡnode;
}

} partial class transformers_package {

public static ж<ast.ModifierList> ExtractModifiers(ж<printer.EmitContext> emitContext, ж<ast.ModifierList> modifiers, ast.ModifierFlags allowed) {
    if (modifiers == nil) {
        return default;
    }
    ref var tx = ref heap<modifierVisitor>(new modifierVisitor(AllowedModifiers: allowed), out var Ꮡtx);
    Ꮡtx.of(modifierVisitor.ᏑTransformer).NewTransformer(Ꮡtx.visit, emitContext);
    return tx.visitor.VisitModifiers(modifiers);
}

} // end transformers_package
