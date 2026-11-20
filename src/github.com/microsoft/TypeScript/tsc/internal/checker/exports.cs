namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

} partial class checkerᴛChecker {

public static ж<Type> GetStringType(this ref Checker c) {
    return c.stringType;
}

public static ж<Type> GetNumberType(this ref Checker c) {
    return c.numberType;
}

public static ж<Type> GetBooleanType(this ref Checker c) {
    return c.booleanType;
}

public static ж<Type> GetVoidType(this ref Checker c) {
    return c.voidType;
}

public static ж<Type> GetUndefinedType(this ref Checker c) {
    return c.undefinedType;
}

public static ж<Type> GetNullType(this ref Checker c) {
    return c.nullType;
}

public static ж<Type> GetAnyType(this ref Checker c) {
    return c.anyType;
}

public static ж<Type> GetErrorType(this ref Checker c) {
    return c.errorType;
}

public static ж<Type> GetNeverType(this ref Checker c) {
    return c.neverType;
}

public static ж<Type> GetUnknownType(this ref Checker c) {
    return c.unknownType;
}

public static ж<Type> GetBigIntType(this ref Checker c) {
    return c.bigintType;
}

public static ж<Type> GetESSymbolType(this ref Checker c) {
    return c.esSymbolType;
}

public static ж<Type> GetNonPrimitiveType(this ref Checker c) {
    return c.nonPrimitiveType;
}

public static ж<Type> GetBaseTypeOfLiteralType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getBaseTypeOfLiteralType(t);
}

public static ж<ast.Symbol> GetUnknownSymbol(this ref Checker c) {
    return c.unknownSymbol;
}

public static ж<ast.Symbol> GetUndefinedSymbol(this ref Checker c) {
    return c.undefinedSymbol;
}

public static ж<ast.Symbol> GetArgumentsSymbol(this ref Checker c) {
    return c.argumentsSymbol;
}

public static ж<Signature> GetUnknownSignature(this ref Checker c) {
    return c.unknownSignature;
}

public static ж<Type> GetUnionType(this ж<Checker> Ꮡc, slice<ж<Type>> types) {
    return Ꮡc.getUnionType(types);
}

public static ж<Type> GetNameTypeOfSymbol(this ref Checker c, ж<ast.Symbol> symbol) {
    if (!c.valueSymbolLinks.Has(symbol)) {
        return default;
    }
    return c.valueSymbolLinks.TryGet(symbol).nameType;
}

} partial class checker_package {

public static bool IsTypeUsableAsPropertyName(ж<Type> t) {
    return isTypeUsableAsPropertyName(ref (t).DerefOrNull());
}

public static @string GetPropertyNameFromType(ж<Type> t) {
    return getPropertyNameFromType(t);
}

} partial class checkerᴛChecker {

public static ж<ast.Symbol> GetGlobalSymbol(this ref Checker c, @string name, ast.SymbolFlags meaning, ж<diagnostics.Message> diagnostic) {
    return c.getGlobalSymbol(name, meaning, diagnostic);
}

public static ж<ast.Symbol> GetMergedSymbol(this ref Checker c, ж<ast.Symbol> symbol) {
    return c.getMergedSymbol(symbol);
}

public static ж<ast.Symbol> TryFindAmbientModule(this ж<Checker> Ꮡc, @string moduleName) {
    return Ꮡc.tryFindAmbientModule(moduleName, true);
}

public static ж<ast.Symbol> GetImmediateAliasedSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getImmediateAliasedSymbol(symbol);
}

public static ж<ast.Symbol> GetTargetSymbol(this ref Checker c, ж<ast.Symbol> symbol) {
    return c.getTargetSymbol(symbol);
}

public static ж<ast.Node> GetTypeOnlyAliasDeclaration(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getTypeOnlyAliasDeclaration(symbol);
}

public static ж<ast.Symbol> ResolveExternalModuleName(this ж<Checker> Ꮡc, ж<ast.Node> moduleSpecifier, ж<Type> importAttributesType) {
    return Ꮡc.resolveExternalModuleName(moduleSpecifier, moduleSpecifier, true, importAttributesType);
}

public static ж<ast.Symbol> ResolveExternalModuleSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> moduleSymbol) {
    return Ꮡc.resolveExternalModuleSymbol(moduleSymbol, false);
}

public static ж<Type> GetTypeFromTypeNode(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    return Ꮡc.getTypeFromTypeNode(node);
}

public static bool IsArrayLikeType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.isArrayLikeType(t);
}

public static slice<ж<ast.Symbol>> GetPropertiesOfType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getPropertiesOfType(t);
}

public static ж<ast.Symbol> GetPropertyOfType(this ж<Checker> Ꮡc, ж<Type> t, @string name) {
    return Ꮡc.getPropertyOfType(t, name);
}

public static bool TypeHasCallOrConstructSignatures(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.typeHasCallOrConstructSignatures(t);
}

public static bool IsPropertyAccessible(this ж<Checker> Ꮡc, ж<ast.Node> node, bool isSuper, bool isWrite, ж<Type> containingType, ж<ast.Symbol> property) {
    return Ꮡc.isPropertyAccessible(node, isSuper, isWrite, containingType, property);
}

public static ж<Type> GetTypeOfPropertyOfContextualType(this ж<Checker> Ꮡc, ж<Type> t, @string name) {
    return Ꮡc.getTypeOfPropertyOfContextualType(t, name);
}

} partial class checker_package {

public static ast.ModifierFlags GetDeclarationModifierFlagsFromSymbol(ж<ast.Symbol> s) {
    return getDeclarationModifierFlagsFromSymbol(ref (s).DerefOrNull());
}

} partial class checkerᴛChecker {

public static bool WasCanceled(this ref Checker c) {
    return c.wasCanceled;
}

public static slice<ж<Signature>> GetSignaturesOfType(this ж<Checker> Ꮡc, ж<Type> t, SignatureKind kind) {
    return Ꮡc.getSignaturesOfType(t, kind);
}

public static ж<Type> GetDeclaredTypeOfSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getDeclaredTypeOfSymbol(symbol);
}

public static ж<Type> GetTypeOfSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getTypeOfSymbol(symbol);
}

public static ж<Type> GetNonMissingTypeOfSymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getNonMissingTypeOfSymbol(symbol);
}

public static ж<Type> GetConstraintOfTypeParameter(this ж<Checker> Ꮡc, ж<Type> typeParameter) {
    return Ꮡc.getConstraintOfTypeParameter(typeParameter);
}

public static ж<Type> GetTrueTypeOfConditionalType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getTrueTypeFromConditionalType(t);
}

public static ж<Type> GetFalseTypeOfConditionalType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getFalseTypeFromConditionalType(t);
}

public static ж<Type> GetDefaultFromTypeParameter(this ж<Checker> Ꮡc, ж<Type> typeParameter) {
    return Ꮡc.getDefaultFromTypeParameter(typeParameter);
}

public static ast.ModifierFlags GetEffectiveDeclarationFlags(this ref Checker c, ж<ast.Node> n, ast.ModifierFlags flagsToCheck) {
    return c.getEffectiveDeclarationFlags(n, flagsToCheck);
}

public static ж<Type> GetBaseConstraintOfType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getBaseConstraintOfType(t);
}

public static ж<TypePredicate> GetTypePredicateOfSignature(this ж<Checker> Ꮡc, ж<Signature> sig) {
    return Ꮡc.getTypePredicateOfSignature(sig);
}

} partial class checker_package {

public static bool IsTupleType(ж<Type> t) {
    return isTupleType(t);
}

public static bool IsTupleTypeTarget(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return isTupleType(Ꮡt) && t.Target() == Ꮡt;
}

} partial class checkerᴛChecker {

public static bool IsArrayType(this ref Checker c, ж<Type> t) {
    return c.isArrayType(t);
}

public static bool IsReadonlySymbol(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.isReadonlySymbol(symbol);
}

public static ж<Type> GetReturnTypeOfSignature(this ж<Checker> Ꮡc, ж<Signature> sig) {
    return Ꮡc.getReturnTypeOfSignature(sig);
}

public static bool HasEffectiveRestParameter(this ж<Checker> Ꮡc, ж<Signature> signature) {
    return Ꮡc.hasEffectiveRestParameter(signature);
}

public static slice<ж<Type>> GetLocalTypeParametersOfClassOrInterfaceOrTypeAlias(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getLocalTypeParametersOfClassOrInterfaceOrTypeAlias(symbol);
}

public static ж<Type> GetContextualTypeForObjectLiteralElement(this ж<Checker> Ꮡc, ж<ast.Node> element, ContextFlags contextFlags) {
    return Ꮡc.getContextualTypeForObjectLiteralElement(element, contextFlags);
}

public static @string TypePredicateToString(this ж<Checker> Ꮡc, ж<TypePredicate> t) {
    return Ꮡc.typePredicateToString(t);
}

public static slice<slice<ж<ast.Symbol>>> GetExpandedParameters(this ж<Checker> Ꮡc, ж<Signature> signature, bool skipUnionExpanding) {
    return Ꮡc.getExpandedParameters(signature, skipUnionExpanding);
}

public static ж<Signature> GetResolvedSignature(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    return Ꮡc.getResolvedSignature(node, nil, CheckModeNormal);
}

public static ж<Type> GetTypeOfPropertyOfType(this ж<Checker> Ꮡc, ж<Type> t, @string name) {
    return Ꮡc.getTypeOfPropertyOfType(t, name);
}

public static ж<Type> GetContextualTypeForArgumentAtIndex(this ж<Checker> Ꮡc, ж<ast.Node> node, nint argIndex) {
    return Ꮡc.getContextualTypeForArgumentAtIndex(node, argIndex);
}

public static ж<Type> GetAwaitedType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getAwaitedType(t);
}

public static slice<ж<ast.Node>> GetIndexSignaturesAtLocation(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    return Ꮡc.getIndexSignaturesAtLocation(node);
}

public static ж<ast.Symbol> GetResolvedSymbol(this ref Checker c, ж<ast.Node> node) {
    return c.getResolvedSymbol(node);
}

public static @string GetJsxNamespace(this ref Checker c, ж<ast.Node> location) {
    return c.getJsxNamespace(location);
}

public static @string GetJsxFragmentFactory(this ref Checker c, ж<ast.Node> location) {
    var entity = c.getJsxFragmentFactoryEntity(location);
    if (entity != nil) {
        return ast.GetFirstIdentifier(entity).Text();
    }
    return "";
}

public static ж<ast.Symbol> ResolveName(this ref Checker c, @string name, ж<ast.Node> location, ast.SymbolFlags meaning, bool excludeGlobals) {
    return c.resolveName(location, name, meaning, nil, true, excludeGlobals);
}

public static ast.SymbolFlags GetSymbolFlags(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getSymbolFlags(symbol);
}

public static slice<ж<Type>> GetBaseTypes(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getBaseTypes(t);
}

public static ж<Type> GetApparentType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getApparentType(t);
}

public static ж<Type> GetReducedType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getReducedType(t);
}

public static @string GetFullyQualifiedName(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.getFullyQualifiedName(symbol, nil);
}

public static ж<Type> GetBaseConstructorTypeOfClass(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getBaseConstructorTypeOfClass(t);
}

public static MemberOverrideStatus GetMemberOverrideModifierStatus(this ж<Checker> Ꮡc, ж<ast.Node> node, ж<ast.Node> member, ж<ast.Symbol> memberSymbol) {
    return Ꮡc.getMemberOverrideModifierStatus(node, member, memberSymbol);
}

public static ж<Type> GetRestTypeOfSignature(this ж<Checker> Ꮡc, ж<Signature> sig) {
    return Ꮡc.getRestTypeOfSignature(sig);
}

public static slice<ж<Type>> GetTypeArguments(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getTypeArguments(t);
}

public static ж<IndexInfo> GetIndexInfoOfType(this ж<Checker> Ꮡc, ж<Type> t, ж<Type> keyType) {
    return Ꮡc.getIndexInfoOfType(t, keyType);
}

public static ж<Type> GetIndexTypeOfType(this ж<Checker> Ꮡc, ж<Type> t, ж<Type> keyType) {
    return Ꮡc.getIndexTypeOfType(t, keyType);
}

public static slice<ж<IndexInfo>> GetIndexInfosOfType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getIndexInfosOfType(t);
}

public static bool IsContextSensitive(this ж<Checker> Ꮡc, ж<ast.Node> node) {
    return Ꮡc.isContextSensitive(node);
}

public static slice<ж<Type>> FillMissingTypeArguments(this ж<Checker> Ꮡc, slice<ж<Type>> typeArguments, slice<ж<Type>> typeParameters, nint minTypeArgumentCount, bool isJavaScriptImplicitAny) {
    return Ꮡc.fillMissingTypeArguments(typeArguments, typeParameters, minTypeArgumentCount, isJavaScriptImplicitAny);
}

public static nint GetMinTypeArgumentCount(this ref Checker c, slice<ж<Type>> typeParameters) {
    return c.getMinTypeArgumentCount(typeParameters);
}

public static ж<Type> GetWidenedLiteralType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getWidenedLiteralType(t);
}

public static bool IsTypeAssignableTo(this ж<Checker> Ꮡc, ж<Type> source, ж<Type> target) {
    return Ꮡc.isTypeAssignableTo(source, target);
}

public static ж<Type> GetUnionTypeEx(this ж<Checker> Ꮡc, slice<ж<Type>> types, UnionReduction unionReduction) {
    return Ꮡc.getUnionTypeEx(types, unionReduction, nil, nil);
}

public static bool RequiresAddingImplicitUndefined(this ж<Checker> Ꮡc, ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var enclosingDeclaration = ast.FindAncestor(Ꮡnode, ast.IsDeclaration);
    if (enclosingDeclaration == nil) {
        enclosingDeclaration = ast.GetSourceFileOfNode(Ꮡnode).of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    var symbol = node.Symbol();
    if (symbol == nil) {
        return false;
    }
    return Ꮡc.GetEmitResolver().RequiresAddingImplicitUndefined(Ꮡnode, symbol, enclosingDeclaration);
}

public static ж<Type> RemoveMissingOrUndefinedType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.removeMissingOrUndefinedType(t);
}

public static ж<Type> GetWidenedType(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.getWidenedType(t);
}

public static nint CompareSymbols(this ref Checker c, ж<ast.Symbol> s1, ж<ast.Symbol> s2) {
    return c.compareSymbols(s1, s2);
}

} partial class checker_package {

public static bool IsDistributedTypeParameter(ж<Type> Ꮡt) {
    ref var t = ref Ꮡt.DerefOrNull();

    return (TypeFlags)(t.flags & TypeFlagsTypeParameter) != 0 && t.AsTypeParameter().isDistributed;
}

} // end checker_package
