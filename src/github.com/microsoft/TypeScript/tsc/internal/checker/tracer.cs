namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using maps = maps_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

partial struct Tracer {
    internal ж<tracing.Tracing> tracing;
    internal tracing.Tracer recorder;
    internal nint checkerIndex;
}

public static ж<Tracer> NewTracer(ж<tracing.Tracing> tr, nint checkerIndex) {
    return Ꮡ(new Tracer(tracing: tr, recorder: tr.NewTypeTracer(checkerIndex), checkerIndex: checkerIndex));
}

} partial class checkerᴛTracer {

public static void RecordType(this ref Tracer t, ж<Type> typ) {
    t.recorder.RecordType(wrapType(typ));
}

public static Action Push(this ж<Tracer> Ꮡt, tracing.Phase phase, @string name, map<@string, any> args, bool separateBeginAndEnd) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (!separateBeginAndEnd) {
        return t.tracing.Push(phase, name, t.copyWithCheckerIndex(args), separateBeginAndEnd);
    }

    (args, var restore) = t.temporarilyAddCheckerIndex(args);
    var pop = t.tracing.Push(phase, name, args, separateBeginAndEnd);
    restore();

    var argsʗ1 = args;
    var popʗ1 = pop;
    return () => {
        GoFrame ᒐ = default;
        try {
            var (_, restoreEndArgs) = Ꮡt.Value.temporarilyAddCheckerIndex(argsʗ1);
            var restoreEndArgsʗ1 = restoreEndArgs;
            defer(restoreEndArgsʗ1, ref ᒐ);
            popʗ1();
        }
        catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
        finally { ᒐ.Run(); }
    };
}

public static void Instant(this ref Tracer t, tracing.Phase phase, @string name, map<@string, any> args) {
    t.tracing.Instant(phase, name, t.copyWithCheckerIndex(args));
}

internal static map<@string, any> copyWithCheckerIndex(this ref Tracer t, map<@string, any> args) {
    var withCheckerIndex = new map<@string, any>(len(args) + 1);
    maps.Copy<map<@string, any>, map<@string, any>, @string, any>(withCheckerIndex, args);
    withCheckerIndex["checkerId"] = t.checkerIndex;
    return withCheckerIndex;
}

internal static (map<@string, any>, Action) temporarilyAddCheckerIndex(this ref Tracer t, map<@string, any> args) {
    if (args == nil) {
        args = new map<@string, any>{};
    }

    var (previous, hadPrevious) = args["checkerId", ꟷ];
    args["checkerId"] = t.checkerIndex;

    var argsʗ1 = args;
    var previousʗ1 = previous;
    return (args, () => {
        if (hadPrevious) {
            argsʗ1["checkerId"] = previousʗ1;
        } else {
            delete(argsʗ1, "checkerId"u8);
        }
    });
}

} partial class checker_package {

partial struct tracedTypeAdapter {
    internal ж<Type> t;
    internal ж<Checker> checker;
}

internal static tracing.TracedType _ᴛ3ʗ = new tracedTypeAdapterжTracedType(((ж<tracedTypeAdapter>)nil));

} partial class checkerᴛtracedTypeAdapter {

internal static uint32 Id(this ref tracedTypeAdapter a) {
    return (uint32)a.t.id;
}

internal static slice<@string> FormatFlags(this ref tracedTypeAdapter a) {
    return FormatTypeFlags(a.t.flags);
}

internal static bool IsConditional(this ref tracedTypeAdapter a) {
    return (TypeFlags)(a.t.flags & TypeFlagsConditional) != 0;
}

internal static ж<ast.Symbol> Symbol(this ref tracedTypeAdapter a) {
    return a.t.symbol;
}

internal static ж<ast.Symbol> AliasSymbol(this ref tracedTypeAdapter a) {
    if (a.t.alias == nil) {
        return default;
    }
    return a.t.alias.Symbol();
}

internal static slice<tracing.TracedType> AliasTypeArguments(this ref tracedTypeAdapter a) {
    if (a.t.alias == nil) {
        return default;
    }
    return wrapTypes(a.t.alias.TypeArguments());
}

internal static @string IntrinsicName(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsIntrinsic) == 0) {
        return "";
    }
    var (data, ok) = a.t.data._<ж<IntrinsicType>>(ᐧ);
    if (!ok) {
        return "";
    }
    return data.intrinsicName;
}

internal static slice<tracing.TracedType> UnionTypes(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsUnion) == 0) {
        return default;
    }
    return wrapTypes(a.t.AsUnionType().types);
}

internal static slice<tracing.TracedType> IntersectionTypes(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsIntersection) == 0) {
        return default;
    }
    return wrapTypes(a.t.AsIntersectionType().types);
}

internal static tracing.TracedType IndexType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsIndex) == 0) {
        return default;
    }
    var t = a.t.AsIndexType().target;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType IndexedAccessObjectType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsIndexedAccess) == 0) {
        return default;
    }
    var t = a.t.AsIndexedAccessType().objectType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType IndexedAccessIndexType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsIndexedAccess) == 0) {
        return default;
    }
    var t = a.t.AsIndexedAccessType().indexType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType ConditionalCheckType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsConditional) == 0) {
        return default;
    }
    var t = a.t.AsConditionalType().checkType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType ConditionalExtendsType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsConditional) == 0) {
        return default;
    }
    var t = a.t.AsConditionalType().extendsType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType ConditionalTrueType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsConditional) == 0) {
        return default;
    }
    var t = a.t.AsConditionalType().resolvedTrueType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType ConditionalFalseType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsConditional) == 0) {
        return default;
    }
    var t = a.t.AsConditionalType().resolvedFalseType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType SubstitutionBaseType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsSubstitution) == 0) {
        return default;
    }
    var t = a.t.AsSubstitutionType().baseType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType SubstitutionConstraintType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsSubstitution) == 0) {
        return default;
    }
    var t = a.t.AsSubstitutionType().constraint;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType ReferenceTarget(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsReference) == 0) {
        return default;
    }
    var t = a.t.AsTypeReference().target;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static slice<tracing.TracedType> ReferenceTypeArguments(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsReference) == 0) {
        return default;
    }
    return wrapTypes(a.t.AsTypeReference().resolvedTypeArguments);
}

internal static ж<ast.Node> ReferenceNode(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsReference) == 0) {
        return default;
    }
    return a.t.AsTypeReference().node;
}

internal static tracing.TracedType ReverseMappedSourceType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsReverseMapped) == 0) {
        return default;
    }
    var t = a.t.AsReverseMappedType().source;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType ReverseMappedMappedType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsReverseMapped) == 0) {
        return default;
    }
    var t = a.t.AsReverseMappedType().mappedType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType ReverseMappedConstraintType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsReverseMapped) == 0) {
        return default;
    }
    var t = a.t.AsReverseMappedType().constraintType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType EvolvingArrayElementType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsEvolvingArray) == 0) {
        return default;
    }
    var t = a.t.AsEvolvingArrayType().elementType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static tracing.TracedType EvolvingArrayFinalType(this ref tracedTypeAdapter a) {
    if ((TypeFlags)(a.t.flags & TypeFlagsObject) == 0 || (ObjectFlags)(a.t.objectFlags & ObjectFlagsEvolvingArray) == 0) {
        return default;
    }
    var t = a.t.AsEvolvingArrayType().finalArrayType;
    if (t == nil) {
        return default;
    }
    return wrapType(t);
}

internal static bool IsTuple(this ref tracedTypeAdapter a) {
    return (ObjectFlags)(a.t.objectFlags & ObjectFlagsTuple) != 0;
}

internal static ж<ast.Node> Pattern(this ref tracedTypeAdapter a) {
    if (a.checker == nil) {
        return default;
    }
    return a.checker.patternForType[a.t];
}

internal static any RecursionIdentity(this ref tracedTypeAdapter a) {
    return getRecursionIdentity(a.t).value;
}

internal static @string Display(this ж<tracedTypeAdapter> Ꮡa) {
    GoFrame ᒐ = default;
    try {
        ref var a = ref Ꮡa.DerefOrNull();

        if (a.checker == nil) {
            return "";
        }
        if ((ObjectFlags)(a.t.objectFlags & ObjectFlagsAnonymous) != 0 || (TypeFlags)(a.t.flags & ((TypeFlags)((TypeFlags)((TypeFlags)(TypeFlagsLiteral | TypeFlagsTemplateLiteral) | TypeFlagsUnion) | TypeFlagsIntersection))) != 0) {
            defer(() => {
                _ = recover();
            }, ref ᒐ);
            return a.checker.TypeToString(a.t);
        }
        return "";
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class checker_package {

internal static tracing.TracedType wrapType(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    if (Ꮡt == nil) {
        return default;
    }
    return new tracedTypeAdapterжTracedType(Ꮡ(new tracedTypeAdapter(t: Ꮡt, checker: t.checker)));
}

internal static slice<tracing.TracedType> wrapTypes(slice<ж<Type>> types) {
    if (len(types) == 0) {
        return default;
    }
    var result = new slice<tracing.TracedType>(len(types));
    foreach (var (i, t) in range(types)) {
        result[i] = wrapType(t);
    }
    return result;
}

} // end checker_package
