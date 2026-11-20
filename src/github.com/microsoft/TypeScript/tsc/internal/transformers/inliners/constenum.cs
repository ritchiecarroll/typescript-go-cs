namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static inliners_package;

partial class inliners_package {

partial struct ConstEnumInliningTransformer {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer Transformer;
    internal ж<core.CompilerOptions> compilerOptions;
    internal ж<ast.SourceFile> currentSourceFile;
    internal printer.EmitResolver emitResolver;
}

public static ж<transformers.Transformer> NewConstEnumInliningTransformer(ж<transformers.TransformOptions> Ꮡopt) {
    ref var opt = ref Ꮡopt.DerefOrNull();

    var compilerOptions = opt.CompilerOptions;
    var emitContext = opt.Context;
    if (compilerOptions.GetIsolatedModules()) {
        debug.Fail("const enums are not inlined under isolated modules");
    }
    var tx = Ꮡ(new ConstEnumInliningTransformer(compilerOptions: compilerOptions, emitResolver: opt.EmitResolver));
    return tx.of(ConstEnumInliningTransformer.ᏑTransformer).NewTransformer(tx.visit, emitContext);
}

} partial class inlinersᴛConstEnumInliningTransformer {

internal static ж<ast.Node> visit(this ref ConstEnumInliningTransformer tx, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    switch (node.Kind) {
    case var ᴛ when ᴛ == ast.KindPropertyAccessExpression || ᴛ == ast.KindElementAccessExpression: {
        {
            var parse = tx.Transformer.EmitContext().ParseNode(Ꮡnode);
            if (parse == nil) {
                return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
            }
            var value = tx.emitResolver.GetConstantValue(parse);
            if (value != nil) {
                ж<ast.Node> replacement = default;
                switch (value.type()) {
                case jsnum.Number v: {
                    if (v.IsInf()) {
                        if (v.Abs() == v) {
                            replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity");
                        } else {
                            replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindMinusToken, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity"));
                        }
                    } else if (v.IsNaN()) {
                        replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("NaN");
                    } else if (v.Abs() == v) {
                        replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(v.String(), ast.TokenFlagsNone);
                    } else {
                        replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindMinusToken, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(v.Abs().String(), ast.TokenFlagsNone));
                    }
                    break;
                }
                case @string v: {
                    replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(v, ast.TokenFlagsNone);
                    break;
                }
                case jsnum.PseudoBigInt v: {
                    if (v == (new jsnum.PseudoBigInt())) {
                        replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBigIntLiteral("0", ast.TokenFlagsNone);
                    } else if (!v.Negative) {
                        replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBigIntLiteral(v.Base10Value, ast.TokenFlagsNone);
                    } else {
                        replacement = tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindMinusToken, tx.Transformer.Factory().of(printer.NodeFactory.ᏑΔNodeFactory).NewBigIntLiteral(v.Base10Value, ast.TokenFlagsNone));
                    }
                    break;
                }}

                if (tx.compilerOptions.RemoveComments.IsFalseOrUnknown()) {
                    var original = tx.Transformer.EmitContext().MostOriginal(Ꮡnode);
                    if (original != nil && !ast.NodeIsSynthesized(original)) {
                        @string originalText = scanner.GetTextOfNode(original);
                        @string escapedText = safeMultiLineComment(originalText);
                        tx.Transformer.EmitContext().AddSyntheticTrailingComment(replacement, ast.KindMultiLineCommentTrivia, escapedText, false);
                    }
                }
                return replacement;
            }
            return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
        }
        break;
    }}

    return tx.Transformer.Visitor().VisitEachChild(Ꮡnode);
}

} partial class inliners_package {

internal static @string safeMultiLineComment(@string text) {
    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    Ꮡb.Grow(len(text) + 2);
    Ꮡb.WriteByte((byte)' ');
    for (;;) {
        nint i = strings.Index(text, "*/");
        if (i < 0) {
            break;
        }
        Ꮡb.WriteString(text.slice(0, i));
        Ꮡb.WriteString("*_/");
        text = text.slice(i + 2);
    }
    Ꮡb.WriteString(text);
    Ꮡb.WriteByte((byte)' ');
    return b.String();
}

} // end inliners_package
