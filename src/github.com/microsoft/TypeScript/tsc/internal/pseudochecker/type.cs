namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static pseudochecker_package;

partial class pseudochecker_package {

partial struct PseudoTypeKind /*num:int16*/;

public static PseudoTypeKind PseudoTypeKindDirect => /* iota */ 0;
public static PseudoTypeKind PseudoTypeKindInferred => 1;
public static PseudoTypeKind PseudoTypeKindNoResult => 2;
public static PseudoTypeKind PseudoTypeKindMaybeConstLocation => 3;
public static PseudoTypeKind PseudoTypeKindUnion => 4;
public static PseudoTypeKind PseudoTypeKindUndefined => 5;
public static PseudoTypeKind PseudoTypeKindNull => 6;
public static PseudoTypeKind PseudoTypeKindAny => 7;
public static PseudoTypeKind PseudoTypeKindString => 8;
public static PseudoTypeKind PseudoTypeKindNumber => 9;
public static PseudoTypeKind PseudoTypeKindBigInt => 10;
public static PseudoTypeKind PseudoTypeKindBoolean => 11;
public static PseudoTypeKind PseudoTypeKindFalse => 12;
public static PseudoTypeKind PseudoTypeKindTrue => 13;
public static PseudoTypeKind PseudoTypeKindSingleCallSignature => 14;
public static PseudoTypeKind PseudoTypeKindTuple => 15;
public static PseudoTypeKind PseudoTypeKindObjectLiteral => 16;
public static PseudoTypeKind PseudoTypeKindStringLiteral => 17;
public static PseudoTypeKind PseudoTypeKindNumericLiteral => 18;
public static PseudoTypeKind PseudoTypeKindBigIntLiteral => 19;

partial struct PseudoType {
    public PseudoTypeKind Kind;
    internal pseudoTypeData data;
}

internal static ж<PseudoType> newPseudoType(PseudoTypeKind kind, pseudoTypeData data) {
    var n = data.AsPseudoType();
    n.Kind = kind;
    n.data = data;
    return n;
}

partial interface pseudoTypeData {
    ж<PseudoType> AsPseudoType();
}

partial struct PseudoTypeDefault {
    /*embed*/ public PseudoType PseudoType;
}

} partial class pseudocheckerᴛPseudoTypeDefault {

public static ж<PseudoType> AsPseudoType(this ж<PseudoTypeDefault> Ꮡb) { return Ꮡb.ᏑPseudoType; }

} partial class pseudochecker_package {

partial struct PseudoTypeBase {
    /*embed*/ public PseudoTypeDefault PseudoTypeDefault;
}

public static ж<PseudoType> PseudoTypeUndefined = newPseudoType(PseudoTypeKindUndefined, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeNull = newPseudoType(PseudoTypeKindNull, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeAny = newPseudoType(PseudoTypeKindAny, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeString = newPseudoType(PseudoTypeKindString, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeNumber = newPseudoType(PseudoTypeKindNumber, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeBigInt = newPseudoType(PseudoTypeKindBigInt, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeBoolean = newPseudoType(PseudoTypeKindBoolean, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeFalse = newPseudoType(PseudoTypeKindFalse, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));
public static ж<PseudoType> PseudoTypeTrue = newPseudoType(PseudoTypeKindTrue, new PseudoTypeBaseжpseudoTypeData(Ꮡ(new PseudoTypeBase())));

partial struct PseudoTypeDirect {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public ж<ast.Node> TypeNode;
}

public static ж<PseudoType> NewPseudoTypeDirect(ж<ast.Node> typeNode) {
    return newPseudoType(PseudoTypeKindDirect, new PseudoTypeDirectжpseudoTypeData(Ꮡ(new PseudoTypeDirect(TypeNode: typeNode))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeDirect> AsPseudoTypeDirect(this ref PseudoType t) { return t.data._<pseudoTypeData, ж<PseudoTypeDirect>>(); }

} partial class pseudochecker_package {

partial struct PseudoTypeInferred {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public ж<ast.Node> Expression;
    public slice<ж<ast.Node>> ErrorNodes;
    public bool IsSignatureReturn;
}

public static ж<PseudoType> NewPseudoTypeInferred(ж<ast.Node> expr, bool isSignatureReturn) {
    return newPseudoType(PseudoTypeKindInferred, new PseudoTypeInferredжpseudoTypeData(Ꮡ(new PseudoTypeInferred(Expression: expr, IsSignatureReturn: isSignatureReturn))));
}

public static ж<PseudoType> NewPseudoTypeInferredWithErrors(ж<ast.Node> expr, bool isSignatureReturn, slice<ж<ast.Node>> errorNodes) {
    return newPseudoType(PseudoTypeKindInferred, new PseudoTypeInferredжpseudoTypeData(Ꮡ(new PseudoTypeInferred(Expression: expr, ErrorNodes: errorNodes, IsSignatureReturn: isSignatureReturn))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeInferred> AsPseudoTypeInferred(this ref PseudoType t) { return t.data._<pseudoTypeData, ж<PseudoTypeInferred>>(); }

} partial class pseudochecker_package {

partial struct PseudoTypeNoResult {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public ж<ast.Node> Declaration;
}

public static ж<PseudoType> NewPseudoTypeNoResult(ж<ast.Node> decl) {
    return newPseudoType(PseudoTypeKindNoResult, new PseudoTypeNoResultжpseudoTypeData(Ꮡ(new PseudoTypeNoResult(Declaration: decl))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeNoResult> AsPseudoTypeNoResult(this ref PseudoType t) { return t.data._<pseudoTypeData, ж<PseudoTypeNoResult>>(); }

} partial class pseudochecker_package {

partial struct PseudoTypeMaybeConstLocation {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public ж<ast.Node> Node;
    public ж<PseudoType> ConstType;
    public ж<PseudoType> RegularType;
}

public static ж<PseudoType> NewPseudoTypeMaybeConstLocation(ж<ast.Node> loc, ж<PseudoType> ct, ж<PseudoType> reg) {
    return newPseudoType(PseudoTypeKindMaybeConstLocation, new PseudoTypeMaybeConstLocationжpseudoTypeData(Ꮡ(new PseudoTypeMaybeConstLocation(Node: loc, ConstType: ct, RegularType: reg))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeMaybeConstLocation> AsPseudoTypeMaybeConstLocation(this ref PseudoType t) {
    return t.data._<pseudoTypeData, ж<PseudoTypeMaybeConstLocation>>();
}

} partial class pseudochecker_package {

partial struct PseudoTypeUnion {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public slice<ж<PseudoType>> Types;
}

public static ж<PseudoType> NewPseudoTypeUnion(slice<ж<PseudoType>> types) {
    return newPseudoType(PseudoTypeKindUnion, new PseudoTypeUnionжpseudoTypeData(Ꮡ(new PseudoTypeUnion(Types: types))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeUnion> AsPseudoTypeUnion(this ref PseudoType t) {
    return t.data._<pseudoTypeData, ж<PseudoTypeUnion>>();
}

} partial class pseudochecker_package {

partial struct PseudoParameter {
    public bool Rest;
    public ж<ast.Node> Name;
    public bool Optional;
    public ж<PseudoType> Type;
}

public static ж<PseudoParameter> NewPseudoParameter(bool isRest, ж<ast.Node> name, bool isOptional, ж<PseudoType> t) {
    return Ꮡ(new PseudoParameter(Rest: isRest, Name: name, Optional: isOptional, Type: t));
}

partial struct PseudoTypeSingleCallSignature {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public ж<ast.Node> Signature;
    public slice<ж<PseudoParameter>> Parameters;
    public slice<ж<ast.TypeParameterDeclaration>> TypeParameters;
    public ж<PseudoType> ReturnType;
}

public static ж<PseudoType> NewPseudoTypeSingleCallSignature(ж<ast.Node> signature, slice<ж<PseudoParameter>> parameters, slice<ж<ast.TypeParameterDeclaration>> typeParameters, ж<PseudoType> returnType) {
    return newPseudoType(PseudoTypeKindSingleCallSignature, new PseudoTypeSingleCallSignatureжpseudoTypeData(Ꮡ(new PseudoTypeSingleCallSignature(
        Signature: signature,
        Parameters: parameters,
        TypeParameters: typeParameters,
        ReturnType: returnType
    ))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeSingleCallSignature> AsPseudoTypeSingleCallSignature(this ref PseudoType t) {
    return t.data._<pseudoTypeData, ж<PseudoTypeSingleCallSignature>>();
}

} partial class pseudochecker_package {

partial struct PseudoTypeTuple {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public slice<ж<PseudoType>> Elements;
}

public static ж<PseudoType> NewPseudoTypeTuple(slice<ж<PseudoType>> elements) {
    return newPseudoType(PseudoTypeKindTuple, new PseudoTypeTupleжpseudoTypeData(Ꮡ(new PseudoTypeTuple(
        Elements: elements
    ))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeTuple> AsPseudoTypeTuple(this ref PseudoType t) {
    return t.data._<pseudoTypeData, ж<PseudoTypeTuple>>();
}

} partial class pseudochecker_package {

partial struct PseudoObjectElement {
    public ж<ast.Node> Name;
    public bool Optional;
    public PseudoObjectElementKind Kind;
    internal pseudoObjectElementData data;
}

} partial class pseudocheckerᴛPseudoObjectElement {

public static ж<PseudoObjectElement> AsPseudoObjectElement(this ж<PseudoObjectElement> Ꮡe) { return Ꮡe; }

public static ж<ast.Node> Signature(this ref PseudoObjectElement e) {
    switch (e.Kind) {
    case var ᴛ when ᴛ == PseudoObjectElementKindMethod: {
        return (~e.AsPseudoObjectMethod()).Signature;
    }
    case var ᴛ when ᴛ == PseudoObjectElementKindSetAccessor: {
        return (~e.AsPseudoSetAccessor()).Signature;
    }
    case var ᴛ when ᴛ == PseudoObjectElementKindGetAccessor: {
        return (~e.AsPseudoGetAccessor()).Signature;
    }
    default: {
        return default;
    }}

}

} partial class pseudochecker_package {

partial struct PseudoObjectElementKind /*num:int8*/;

public static PseudoObjectElementKind PseudoObjectElementKindMethod => /* iota */ 0;
public static PseudoObjectElementKind PseudoObjectElementKindPropertyAssignment => 1;
public static PseudoObjectElementKind PseudoObjectElementKindSetAccessor => 2;
public static PseudoObjectElementKind PseudoObjectElementKindGetAccessor => 3;

partial interface pseudoObjectElementData {
    ж<PseudoObjectElement> AsPseudoObjectElement();
}

internal static ж<PseudoObjectElement> newPseudoObjectElement(PseudoObjectElementKind kind, ж<ast.Node> name, bool optional, pseudoObjectElementData data) {
    var e = data.AsPseudoObjectElement();
    e.Kind = kind;
    e.Name = name;
    e.Optional = optional;
    e.data = data;
    return e;
}

partial struct PseudoObjectMethod {
    /*embed*/ public PseudoObjectElement PseudoObjectElement;
    public ж<ast.Node> Signature;
    public slice<ж<ast.TypeParameterDeclaration>> TypeParameters;
    public slice<ж<PseudoParameter>> Parameters;
    public ж<PseudoType> ReturnType;
}

public static ж<PseudoObjectElement> NewPseudoObjectMethod(ж<ast.Node> signature, ж<ast.Node> name, bool optional, slice<ж<ast.TypeParameterDeclaration>> typeParameters, slice<ж<PseudoParameter>> parameters, ж<PseudoType> returnType) {
    return newPseudoObjectElement(PseudoObjectElementKindMethod, name, optional, new PseudoObjectMethodжpseudoObjectElementData(Ꮡ(new PseudoObjectMethod(
        Signature: signature,
        TypeParameters: typeParameters,
        Parameters: parameters,
        ReturnType: returnType
    ))));
}

} partial class pseudocheckerᴛPseudoObjectElement {

public static ж<PseudoObjectMethod> AsPseudoObjectMethod(this ref PseudoObjectElement e) {
    return e.data._<pseudoObjectElementData, ж<PseudoObjectMethod>>();
}

} partial class pseudochecker_package {

partial struct PseudoPropertyAssignment {
    /*embed*/ public PseudoObjectElement PseudoObjectElement;
    public bool Readonly;
    public ж<PseudoType> Type;
}

public static ж<PseudoObjectElement> NewPseudoPropertyAssignment(bool @readonly, ж<ast.Node> name, bool optional, ж<PseudoType> t) {
    return newPseudoObjectElement(PseudoObjectElementKindPropertyAssignment, name, optional, new PseudoPropertyAssignmentжpseudoObjectElementData(Ꮡ(new PseudoPropertyAssignment(
        Readonly: @readonly,
        Type: t
    ))));
}

} partial class pseudocheckerᴛPseudoObjectElement {

public static ж<PseudoPropertyAssignment> AsPseudoPropertyAssignment(this ref PseudoObjectElement e) {
    return e.data._<pseudoObjectElementData, ж<PseudoPropertyAssignment>>();
}

} partial class pseudochecker_package {

partial struct PseudoSetAccessor {
    /*embed*/ public PseudoObjectElement PseudoObjectElement;
    public ж<ast.Node> Signature;
    public ж<PseudoParameter> Parameter;
}

public static ж<PseudoObjectElement> NewPseudoSetAccessor(ж<ast.Node> signature, ж<ast.Node> name, bool optional, ж<PseudoParameter> p) {
    return newPseudoObjectElement(PseudoObjectElementKindSetAccessor, name, optional, new PseudoSetAccessorжpseudoObjectElementData(Ꮡ(new PseudoSetAccessor(
        Signature: signature,
        Parameter: p
    ))));
}

} partial class pseudocheckerᴛPseudoObjectElement {

public static ж<PseudoSetAccessor> AsPseudoSetAccessor(this ref PseudoObjectElement e) {
    return e.data._<pseudoObjectElementData, ж<PseudoSetAccessor>>();
}

} partial class pseudochecker_package {

partial struct PseudoGetAccessor {
    /*embed*/ public PseudoObjectElement PseudoObjectElement;
    public ж<ast.Node> Signature;
    public ж<PseudoType> Type;
}

public static ж<PseudoObjectElement> NewPseudoGetAccessor(ж<ast.Node> signature, ж<ast.Node> name, bool optional, ж<PseudoType> t) {
    return newPseudoObjectElement(PseudoObjectElementKindGetAccessor, name, optional, new PseudoGetAccessorжpseudoObjectElementData(Ꮡ(new PseudoGetAccessor(
        Signature: signature,
        Type: t
    ))));
}

} partial class pseudocheckerᴛPseudoObjectElement {

public static ж<PseudoGetAccessor> AsPseudoGetAccessor(this ref PseudoObjectElement e) {
    return e.data._<pseudoObjectElementData, ж<PseudoGetAccessor>>();
}

} partial class pseudochecker_package {

partial struct PseudoTypeObjectLiteral {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public slice<ж<PseudoObjectElement>> Elements;
}

public static ж<PseudoType> NewPseudoTypeObjectLiteral(slice<ж<PseudoObjectElement>> elements) {
    return newPseudoType(PseudoTypeKindObjectLiteral, new PseudoTypeObjectLiteralжpseudoTypeData(Ꮡ(new PseudoTypeObjectLiteral(
        Elements: elements
    ))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeObjectLiteral> AsPseudoTypeObjectLiteral(this ref PseudoType t) {
    return t.data._<pseudoTypeData, ж<PseudoTypeObjectLiteral>>();
}

} partial class pseudochecker_package {

partial struct PseudoTypeLiteral {
    /*embed*/ public PseudoTypeBase PseudoTypeBase;
    public ж<ast.Node> Node;
}

public static ж<PseudoType> NewPseudoTypeStringLiteral(ж<ast.Node> node) {
    return newPseudoType(PseudoTypeKindStringLiteral, new PseudoTypeLiteralжpseudoTypeData(Ꮡ(new PseudoTypeLiteral(
        Node: node
    ))));
}

public static ж<PseudoType> NewPseudoTypeNumericLiteral(ж<ast.Node> node) {
    return newPseudoType(PseudoTypeKindNumericLiteral, new PseudoTypeLiteralжpseudoTypeData(Ꮡ(new PseudoTypeLiteral(
        Node: node
    ))));
}

public static ж<PseudoType> NewPseudoTypeBigIntLiteral(ж<ast.Node> node) {
    return newPseudoType(PseudoTypeKindBigIntLiteral, new PseudoTypeLiteralжpseudoTypeData(Ꮡ(new PseudoTypeLiteral(
        Node: node
    ))));
}

} partial class pseudocheckerᴛPseudoType {

public static ж<PseudoTypeLiteral> AsPseudoTypeLiteral(this ref PseudoType t) {
    return t.data._<pseudoTypeData, ж<PseudoTypeLiteral>>();
}

} // end pseudocheckerᴛPseudoType
