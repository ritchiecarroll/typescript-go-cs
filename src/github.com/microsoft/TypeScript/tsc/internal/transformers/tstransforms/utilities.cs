namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class tstransforms_package {

internal static ж<astꓸExpression> constantExpression(any value, ж<printer.NodeFactory> factory) {
    switch (value.type()) {
    case @string valueΔ1: {
        return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewStringLiteral(valueΔ1, ast.TokenFlagsNone);
    }
    case jsnum.Number valueΔ1: {
        if (valueΔ1.IsInf()) {
            if (valueΔ1 > 0D) {
                return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity");
            }
            return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindMinusToken, factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("Infinity"));
        }
        if (valueΔ1.IsNaN()) {
            return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewIdentifier("NaN");
        }
        if (valueΔ1 < 0D) {
            return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewPrefixUnaryExpression(ast.KindMinusToken, constantExpression(-valueΔ1, factory));
        }
        return factory.of(printer.NodeFactory.ᏑΔNodeFactory).NewNumericLiteral(valueΔ1.String(), ast.TokenFlagsNone);
    }}
    return default;
}

} // end tstransforms_package
