namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ast_package {

partial struct FunctionFlags /*num:uint32*/;

public static FunctionFlags FunctionFlagsNormal => 0;
public static FunctionFlags FunctionFlagsGenerator => /* 1 << 0 */ 1;
public static FunctionFlags FunctionFlagsAsync => /* 1 << 1 */ 2;
public static FunctionFlags FunctionFlagsInvalid => /* 1 << 2 */ 4;
public static FunctionFlags FunctionFlagsAsyncGenerator => /* FunctionFlagsAsync | FunctionFlagsGenerator */ 3;

public static FunctionFlags GetFunctionFlags(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return FunctionFlagsInvalid;
    }
    var data = node.BodyData();
    if (data == nil) {
        return FunctionFlagsInvalid;
    }
    var flags = FunctionFlagsNormal;
    var exprᴛ1 = node.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == KindFunctionDeclaration || exprᴛ1 == KindFunctionExpression || exprᴛ1 == KindMethodDeclaration) {
        if (data.AsteriskToken != nil) {
            flags |= (FunctionFlags)(FunctionFlagsGenerator);
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && exprᴛ1 == KindArrowFunction) { matchᴛ1 = true;
        if (HasSyntacticModifier(Ꮡnode, ModifierFlagsAsync)) {
            flags |= (FunctionFlags)(FunctionFlagsAsync);
        }
    }

    if (data.Body == nil) {
        flags |= (FunctionFlags)(FunctionFlagsInvalid);
    }
    return flags;
}

} // end ast_package
