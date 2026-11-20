namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

partial struct TypeMapperKind /*num:int32*/;

public static TypeMapperKind TypeMapperKindUnknown => /* iota */ 0;
public static TypeMapperKind TypeMapperKindSimple => 1;
public static TypeMapperKind TypeMapperKindArray => 2;
public static TypeMapperKind TypeMapperKindMerged => 3;

partial struct TypeMapper {
    internal TypeMapperData data;
}

} partial class checkerᴛTypeMapper {

public static ж<Type> Map(this ref TypeMapper m, ж<Type> t) { return m.data.Map(t); }
public static TypeMapperKind Kind(this ref TypeMapper m) { return m.data.Kind(); }
public static bool MapsThisOnly(this ref TypeMapper m) { return m.data.MapsThisOnly(); }

} partial class checker_package {

partial interface TypeMapperData {
    ж<Type> Map(ж<Type> t);
    TypeMapperKind Kind();
    bool MapsThisOnly();
}

internal static ж<Type> getMappedType(ж<Type> t, ж<TypeMapper> Ꮡmapper) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    return mapper.Map(getNonDistributedTypeParameter(t));
}

internal static ж<TypeMapper> newTypeMapper(slice<ж<Type>> sources, slice<ж<Type>> targets) {
    if (len(sources) == 1) {
        return newSimpleTypeMapper(sources[0], targets[0]);
    }
    return newArrayTypeMapper(sources, targets);
}

} partial class checkerᴛChecker {

internal static ж<TypeMapper> combineTypeMappers(this ж<Checker> Ꮡc, ж<TypeMapper> m1, ж<TypeMapper> m2) {
    if (m1 != nil) {
        return newCompositeTypeMapper(Ꮡc, m1, m2);
    }
    return m2;
}

internal static ж<Type> mapTypeWithCompositeMapper(this ж<Checker> Ꮡc, ж<Type> t, ж<TypeMapper> m1, ж<TypeMapper> m2) {
    if (m1 == nil) {
        return getMappedType(t, m2);
    }
    var t1 = getMappedType(t, m1);
    if (t1 != t) {
        return Ꮡc.instantiateType(t1, m2);
    }
    return getMappedType(t, m2);
}

} partial class checker_package {

internal static ж<TypeMapper> mergeTypeMappers(ж<TypeMapper> m1, ж<TypeMapper> m2) {
    if (m1 != nil) {
        return newMergedTypeMapper(m1, m2);
    }
    return m2;
}

internal static ж<TypeMapper> prependTypeMapping(ж<Type> source, ж<Type> target, ж<TypeMapper> mapper) {
    if (mapper == nil) {
        return newSimpleTypeMapper(getNonDistributedTypeParameter(source), target);
    }
    return newMergedTypeMapper(newSimpleTypeMapper(getNonDistributedTypeParameter(source), target), mapper);
}

internal static ж<TypeMapper> appendTypeMapping(ж<TypeMapper> mapper, ж<Type> source, ж<Type> target) {
    if (mapper == nil) {
        return newSimpleTypeMapper(getNonDistributedTypeParameter(source), target);
    }
    return newMergedTypeMapper(mapper, newSimpleTypeMapper(getNonDistributedTypeParameter(source), target));
}

} partial class checkerᴛChecker {

internal static ж<TypeMapper> newBackreferenceMapper(this ref Checker c, ж<InferenceContext> Ꮡcontext, nint index) {
    ref var context = ref Ꮡcontext.DerefOrNull();

    var forwardInferences = context.inferences.slice(index);
    var typeParameters = core.Map(forwardInferences, ж<Type> (ж<InferenceInfo> i) => i.typeParameter);
    return newArrayToSingleTypeMapper(typeParameters, c.unknownType);
}

} partial class checker_package {

partial struct TypeMapperBase {
    /*embed*/ public TypeMapper TypeMapper;
}

} partial class checkerᴛTypeMapperBase {

public static ж<Type> Map(this ref TypeMapperBase m, ж<Type> t) { return t; }
public static TypeMapperKind Kind(this ref TypeMapperBase m) { return TypeMapperKindUnknown; }
public static bool MapsThisOnly(this ref TypeMapperBase m) { return false; }

} partial class checker_package {

partial struct SimpleTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal ж<Type> source;
    internal ж<Type> target;
}

internal static ж<TypeMapper> newSimpleTypeMapper(ж<Type> Ꮡsource, ж<Type> Ꮡtarget) {
    ref var source = ref Ꮡsource.DerefOrNull();
    ref var target = ref Ꮡtarget.DerefOrNull();

    var m = Ꮡ(new SimpleTypeMapper());
    m.data = new SimpleTypeMapperжTypeMapperData(m);
    m.source = Ꮡsource;
    m.target = Ꮡtarget;
    return m.of(SimpleTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛSimpleTypeMapper {

public static ж<Type> Map(this ref SimpleTypeMapper m, ж<Type> t) {
    if (t == m.source) {
        return m.target;
    }
    return t;
}

public static TypeMapperKind Kind(this ref SimpleTypeMapper m) {
    return TypeMapperKindSimple;
}

public static bool MapsThisOnly(this ref SimpleTypeMapper m) {
    return isThisTypeParameter(m.source);
}

} partial class checker_package {

partial struct ArrayTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal slice<ж<Type>> sources;
    internal slice<ж<Type>> targets;
}

internal static ж<TypeMapper> newArrayTypeMapper(slice<ж<Type>> sources, slice<ж<Type>> targets) {
    var m = Ꮡ(new ArrayTypeMapper());
    m.data = new ArrayTypeMapperжTypeMapperData(m);
    m.sources = sources;
    m.targets = targets;
    return m.of(ArrayTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛArrayTypeMapper {

public static ж<Type> Map(this ref ArrayTypeMapper m, ж<Type> t) {
    foreach (var (i, s) in range(m.sources)) {
        if (t == s) {
            return m.targets[i];
        }
    }
    return t;
}

public static TypeMapperKind Kind(this ref ArrayTypeMapper m) {
    return TypeMapperKindArray;
}

public static bool MapsThisOnly(this ref ArrayTypeMapper m) {
    return len(m.sources) == 1 && isThisTypeParameter(m.sources[0]);
}

} partial class checker_package {

partial struct ArrayToSingleTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal slice<ж<Type>> sources;
    internal ж<Type> target;
}

internal static ж<TypeMapper> newArrayToSingleTypeMapper(slice<ж<Type>> sources, ж<Type> Ꮡtarget) {
    ref var target = ref Ꮡtarget.DerefOrNull();

    var m = Ꮡ(new ArrayToSingleTypeMapper());
    m.data = new ArrayToSingleTypeMapperжTypeMapperData(m);
    m.sources = sources;
    m.target = Ꮡtarget;
    return m.of(ArrayToSingleTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛArrayToSingleTypeMapper {

public static ж<Type> Map(this ref ArrayToSingleTypeMapper m, ж<Type> t) {
    if (slices.Contains(m.sources, t)) {
        return m.target;
    }
    return t;
}

public static bool MapsThisOnly(this ref ArrayToSingleTypeMapper m) {
    return len(m.sources) == 1 && isThisTypeParameter(m.sources[0]);
}

} partial class checker_package {

partial struct DeferredTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal slice<ж<Type>> sources;
    internal slice<Func<ж<Type>>> targets;
}

internal static ж<TypeMapper> newDeferredTypeMapper(slice<ж<Type>> sources, slice<Func<ж<Type>>> targets) {
    var m = Ꮡ(new DeferredTypeMapper());
    m.data = new DeferredTypeMapperжTypeMapperData(m);
    m.sources = sources;
    m.targets = targets;
    return m.of(DeferredTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛDeferredTypeMapper {

public static ж<Type> Map(this ref DeferredTypeMapper m, ж<Type> t) {
    foreach (var (i, s) in range(m.sources)) {
        if (t == s) {
            return m.targets[i]();
        }
    }
    return t;
}

public static bool MapsThisOnly(this ref DeferredTypeMapper m) {
    return len(m.sources) == 1 && isThisTypeParameter(m.sources[0]);
}

} partial class checker_package {

partial struct FunctionTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal Func<ж<Type>, ж<Type>> fn;
}

internal static ж<TypeMapper> newFunctionTypeMapper(Func<ж<Type>, ж<Type>> fn) {
    var m = Ꮡ(new FunctionTypeMapper());
    m.data = new FunctionTypeMapperжTypeMapperData(m);
    m.fn = fn;
    return m.of(FunctionTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛFunctionTypeMapper {

public static ж<Type> Map(this ref FunctionTypeMapper m, ж<Type> t) {
    return m.fn(t);
}

} partial class checker_package {

partial struct MergedTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal ж<TypeMapper> m1;
    internal ж<TypeMapper> m2;
}

internal static ж<TypeMapper> newMergedTypeMapper(ж<TypeMapper> Ꮡm1, ж<TypeMapper> Ꮡm2) {
    ref var m1 = ref Ꮡm1.DerefOrNull();
    ref var m2 = ref Ꮡm2.DerefOrNull();

    var m = Ꮡ(new MergedTypeMapper());
    m.data = new MergedTypeMapperжTypeMapperData(m);
    m.m1 = Ꮡm1;
    m.m2 = Ꮡm2;
    return m.of(MergedTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛMergedTypeMapper {

public static ж<Type> Map(this ref MergedTypeMapper m, ж<Type> t) {
    return m.m2.Map(m.m1.Map(t));
}

public static TypeMapperKind Kind(this ref MergedTypeMapper m) {
    return TypeMapperKindMerged;
}

} partial class checker_package {

partial struct CompositeTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal ж<Checker> c;
    internal ж<TypeMapper> m1;
    internal ж<TypeMapper> m2;
}

internal static ж<TypeMapper> newCompositeTypeMapper(ж<Checker> Ꮡc, ж<TypeMapper> Ꮡm1, ж<TypeMapper> Ꮡm2) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var m1 = ref Ꮡm1.DerefOrNull();
    ref var m2 = ref Ꮡm2.DerefOrNull();

    var m = Ꮡ(new CompositeTypeMapper());
    m.data = new CompositeTypeMapperжTypeMapperData(m);
    m.c = Ꮡc;
    m.m1 = Ꮡm1;
    m.m2 = Ꮡm2;
    return m.of(CompositeTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛCompositeTypeMapper {

public static ж<Type> Map(this ref CompositeTypeMapper m, ж<Type> t) {
    var t1 = m.m1.Map(t);
    if (t1 != t) {
        return m.c.instantiateType(t1, m.m2);
    }
    return m.m2.Map(t);
}

} partial class checker_package {

partial struct InferenceTypeMapper {
    /*embed*/ public TypeMapperBase TypeMapperBase;
    internal ж<Checker> c;
    internal ж<InferenceContext> n;
    internal bool fixing;
}

} partial class checkerᴛChecker {

internal static ж<TypeMapper> newInferenceTypeMapper(this ж<Checker> Ꮡc, ж<InferenceContext> Ꮡn, bool fixing) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var n = ref Ꮡn.DerefOrNull();

    var m = Ꮡ(new InferenceTypeMapper());
    m.data = new InferenceTypeMapperжTypeMapperData(m);
    m.c = Ꮡc;
    m.n = Ꮡn;
    m.fixing = fixing;
    return m.of(InferenceTypeMapper.ᏑTypeMapper);
}

} partial class checkerᴛInferenceTypeMapper {

public static ж<Type> Map(this ref InferenceTypeMapper m, ж<Type> t) {
    foreach (var (i, inference) in range(m.n.inferences)) {
        if (t == inference.typeParameter) {
            if (m.fixing && !inference.isFixed) {
                m.c.inferFromIntraExpressionSites(m.n);
                clearCachedInferences(m.n.inferences);
                inference.isFixed = true;
            }
            return m.c.getInferredType(m.n, i);
        }
    }
    return t;
}

} // end checkerᴛInferenceTypeMapper
