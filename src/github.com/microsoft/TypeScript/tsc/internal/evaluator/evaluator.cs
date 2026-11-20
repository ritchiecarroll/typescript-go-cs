namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using jsnum = go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class evaluator_package {

partial struct Result {
    public any Value;
    public bool IsSyntacticallyString;
    public bool ResolvedOtherFiles;
    public bool HasExternalReferences;
}

public static Result NewResult(any value, bool isSyntacticallyString, bool resolvedOtherFiles, bool hasExternalReferences) {
    return new Result(value, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
}

// type Evaluator is a methodless func type — rendered inline as its base delegate

public static Func<ж<ast.Node>, ж<ast.Node>, Result> NewEvaluator(Func<ж<ast.Node>, ж<ast.Node>, Result> evaluateEntity, ast.OuterExpressionKinds outerExpressionsToSkip) {
    ref var evaluate = ref heap<Func<ж<ast.Node>, ж<ast.Node>, Result>>(out var Ꮡevaluate);
    evaluate = (ж<ast.Node> expr, ж<ast.Node> location) => {
        var isSyntacticallyString = false;
        var resolvedOtherFiles = false;
        var hasExternalReferences = false;
        expr = ast.SkipOuterExpressions(expr, (ast.OuterExpressionKinds)(outerExpressionsToSkip | ast.OEKParentheses));
        switch (expr.Kind) {
        case var ᴛ when ᴛ == ast.KindPrefixUnaryExpression: {
            var result = Ꮡevaluate.ValueSlot(expr.AsPrefixUnaryExpression().Operand, location);
            resolvedOtherFiles = result.ResolvedOtherFiles;
            hasExternalReferences = result.HasExternalReferences;
            var (value, ok) = result.Value._<jsnum.Number>(ᐧ); if (ok) {
                switch (expr.AsPrefixUnaryExpression().Operator) {
                case var ᴛ2 when ᴛ2 == ast.KindPlusToken: {
                    return new Result(value, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindMinusToken: {
                    return new Result(-value, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindTildeToken: {
                    return new Result(value.BitwiseNOT(), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }}

            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindBinaryExpression: {
            var left = Ꮡevaluate.ValueSlot(expr.AsBinaryExpression().Left, location);
            var right = Ꮡevaluate.ValueSlot(expr.AsBinaryExpression().Right, location);
            var @operator = expr.AsBinaryExpression().OperatorToken.Kind;
            isSyntacticallyString = (left.IsSyntacticallyString || right.IsSyntacticallyString) && expr.AsBinaryExpression().OperatorToken.Kind == ast.KindPlusToken;
            resolvedOtherFiles = left.ResolvedOtherFiles || right.ResolvedOtherFiles;
            hasExternalReferences = left.HasExternalReferences || right.HasExternalReferences;
            var (leftNum, leftIsNum) = left.Value._<jsnum.Number>(ᐧ);
            var (rightNum, rightIsNum) = right.Value._<jsnum.Number>(ᐧ);
            if (leftIsNum && rightIsNum) {
                switch (@operator) {
                case var ᴛ2 when ᴛ2 == ast.KindBarToken: {
                    return new Result(leftNum.BitwiseOR(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindAmpersandToken: {
                    return new Result(leftNum.BitwiseAND(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindGreaterThanGreaterThanToken: {
                    return new Result(leftNum.SignedRightShift(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindGreaterThanGreaterThanGreaterThanToken: {
                    return new Result(leftNum.UnsignedRightShift(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindLessThanLessThanToken: {
                    return new Result(leftNum.LeftShift(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindCaretToken: {
                    return new Result(leftNum.BitwiseXOR(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindAsteriskToken: {
                    return new Result(leftNum * rightNum, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindSlashToken: {
                    return new Result(leftNum / rightNum, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindPlusToken: {
                    return new Result(leftNum + rightNum, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindMinusToken: {
                    return new Result(leftNum - rightNum, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindPercentToken: {
                    return new Result(leftNum.Remainder(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }
                case var ᴛ2 when ᴛ2 == ast.KindAsteriskAsteriskToken: {
                    return new Result(leftNum.Exponentiate(rightNum), isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
                }}

            }
            var (leftStr, leftIsStr) = left.Value._<@string>(ᐧ);
            var (rightStr, rightIsStr) = right.Value._<@string>(ᐧ);
            if ((leftIsStr || leftIsNum) && (rightIsStr || rightIsNum) && @operator == ast.KindPlusToken) {
                if (leftIsNum) {
                    leftStr = leftNum.String();
                }
                if (rightIsNum) {
                    rightStr = rightNum.String();
                }
                return new Result(leftStr + rightStr, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
            }
            break;
        }
        case var ᴛ when ᴛ == ast.KindStringLiteral || ᴛ == ast.KindNoSubstitutionTemplateLiteral: {
            return new Result(expr.Text(), true, false, false);
        }
        case var ᴛ when ᴛ == ast.KindTemplateExpression: {
            return evaluateTemplateExpression(expr, location, Ꮡevaluate.ValueSlot);
        }
        case var ᴛ when ᴛ == ast.KindNumericLiteral: {
            return new Result(jsnum.FromString(expr.Text()), false, false, false);
        }
        case var ᴛ when ᴛ == ast.KindIdentifier: {
            return evaluateEntity(expr, location);
        }
        case var ᴛ when ᴛ == ast.KindElementAccessExpression || ᴛ == ast.KindPropertyAccessExpression: {
            if (ast.IsEntityNameExpression(expr.Expression())) {
                return evaluateEntity(expr, location);
            }
            break;
        }}

        return new Result(default, isSyntacticallyString, resolvedOtherFiles, hasExternalReferences);
    };
    return evaluate;
}

internal static Result evaluateTemplateExpression(ж<ast.Node> Ꮡexpr, ж<ast.Node> location, Func<ж<ast.Node>, ж<ast.Node>, Result> evaluate) {
    ref var expr = ref Ꮡexpr.DerefOrNull();

    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    Ꮡsb.WriteString(expr.AsTemplateExpression().Head.Text());
    var resolvedOtherFiles = false;
    var hasExternalReferences = false;
    foreach (var (_, span) in range(expr.AsTemplateExpression().TemplateSpans.Nodes)) {
        var spanResult = evaluate(span.Expression(), location);
        if (spanResult.Value == nil) {
            return new Result(default, true, false, false);
        }
        Ꮡsb.WriteString(AnyToString(spanResult.Value));
        Ꮡsb.WriteString(span.AsTemplateSpan().Literal.Text());
        resolvedOtherFiles = resolvedOtherFiles || spanResult.ResolvedOtherFiles;
        hasExternalReferences = hasExternalReferences || spanResult.HasExternalReferences;
    }
    return new Result(sb.String(), true, resolvedOtherFiles, hasExternalReferences);
}

// Hoisted @string literals (single allocation; Go keeps these in RODATA)
private static readonly @string trueˢ = "true"u8;
private static readonly @string falseˢ = "false"u8;

public static @string AnyToString(any v) {
    switch (v.type()) {
    case @string vΔ1: {
        return vΔ1;
    }
    case jsnum.Number vΔ1: {
        return vΔ1.String();
    }
    case bool vΔ1: {
        return core.IfElse(vΔ1, trueˢ, falseˢ);
    }
    case jsnum.PseudoBigInt vΔ1: {
        return vΔ1.String();
    }}
    throw panic("Unhandled case in AnyToString");
}

public static bool IsTruthy(any v) {
    switch (v.type()) {
    case @string vΔ1: {
        return len(vΔ1) != 0;
    }
    case jsnum.Number vΔ1: {
        return vΔ1 != 0D && !vΔ1.IsNaN();
    }
    case bool vΔ1: {
        return vΔ1;
    }
    case jsnum.PseudoBigInt vΔ1: {
        return vΔ1 != new jsnum.PseudoBigInt();
    }}
    throw panic("Unhandled case in IsTruthy");
}

} // end evaluator_package
