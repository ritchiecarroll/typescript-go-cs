namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static transformers_package;

partial class transformers_package {

partial struct Transformer {
    internal ж<printer.EmitContext> emitContext;
    internal ж<printer.NodeFactory> factory;
    internal ж<ast.NodeVisitor> visitor;
}

} partial class transformersᴛTransformer {

public static ж<Transformer> NewTransformer(this ж<Transformer> Ꮡtx, Func<ж<ast.Node>, ж<ast.Node>> visit, ж<printer.EmitContext> ᏑemitContext) {
    ref var tx = ref Ꮡtx.DerefOrNull();
    ref var emitContext = ref ᏑemitContext.DerefOrNull();

    if (tx.emitContext != nil) {
        throw panic("Transformer already initialized");
    }
    if (ᏑemitContext == nil) {
        ᏑemitContext = printer.NewEmitContext(); emitContext = ref ᏑemitContext.DerefOrNull();
    }
    tx.emitContext = ᏑemitContext;
    tx.factory = emitContext.Factory;
    tx.visitor = ᏑemitContext.NewNodeVisitor(visit);
    return Ꮡtx;
}

public static ж<printer.EmitContext> EmitContext(this ref Transformer tx) {
    return tx.emitContext;
}

public static ж<ast.NodeVisitor> Visitor(this ref Transformer tx) {
    return tx.visitor;
}

public static ж<printer.NodeFactory> Factory(this ref Transformer tx) {
    return tx.factory;
}

public static ж<ast.SourceFile> TransformSourceFile(this ref Transformer tx, ж<ast.SourceFile> @file) {
    return tx.visitor.VisitSourceFile(@file);
}

} // end transformersᴛTransformer
