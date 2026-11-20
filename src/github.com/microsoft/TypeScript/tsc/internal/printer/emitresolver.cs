namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using evaluator = go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class printer_package {

partial struct SymbolAccessibility /*num:int32*/;

public static SymbolAccessibility SymbolAccessibilityAccessible => /* iota */ 0;
public static SymbolAccessibility SymbolAccessibilityNotAccessible => 1;
public static SymbolAccessibility SymbolAccessibilityCannotBeNamed => 2;
public static SymbolAccessibility SymbolAccessibilityNotResolved => 3;

partial struct SymbolAccessibilityResult {
    public SymbolAccessibility Accessibility;
    public slice<ж<ast.Node>> AliasesToMakeVisible;
    public @string ErrorSymbolName;
    public ж<ast.Node> ErrorNode;
    public @string ErrorModuleName;
}

partial struct TypeReferenceSerializationKind /*num:int32*/;

public static UntypedInt TypeReferenceSerializationKindUnknown => iota;
public static UntypedInt TypeReferenceSerializationKindTypeWithConstructSignatureAndValue => 1;
public static UntypedInt TypeReferenceSerializationKindVoidNullableOrNeverType => 2;
public static UntypedInt TypeReferenceSerializationKindNumberLikeType => 3;
public static UntypedInt TypeReferenceSerializationKindBigIntLikeType => 4;
public static UntypedInt TypeReferenceSerializationKindStringLikeType => 5;
public static UntypedInt TypeReferenceSerializationKindBooleanType => 6;
public static UntypedInt TypeReferenceSerializationKindArrayLikeType => 7;
public static UntypedInt TypeReferenceSerializationKindESSymbolType => 8;
public static UntypedInt TypeReferenceSerializationKindPromise => 9;
public static UntypedInt TypeReferenceSerializationKindTypeWithCallSignature => 10;
public static UntypedInt TypeReferenceSerializationKindObjectType => 11;

partial interface EmitResolver :
    binder.ReferenceResolver
{
    bool IsReferencedAliasDeclaration(ж<ast.Node> node);
    bool IsValueAliasDeclaration(ж<ast.Node> node);
    bool IsTopLevelValueImportEqualsWithEntityName(ж<ast.Node> node);
    void MarkLinkedReferencesRecursively(ж<ast.SourceFile> @file);
    ж<ast.SourceFile> GetExternalModuleFileFromDeclaration(ж<ast.Node> node);
    ast.ModifierFlags GetEffectiveDeclarationFlags(ж<ast.Node> node, ast.ModifierFlags flags);
    TypeReferenceSerializationKind GetTypeReferenceSerializationKind(ж<astꓸEntityName> name, ж<ast.Node> serialScope);
    any GetConstantValue(ж<ast.Node> node);
    ж<ast.Node> GetJsxFactoryEntity(ж<ast.Node> location);
    ж<ast.Node> GetJsxFragmentFactoryEntity(ж<ast.Node> location);
    void SetReferencedImportDeclaration(ж<astꓸIdentifierNode> node, ж<astꓸDeclaration> @ref);
    void PrecalculateDeclarationEmitVisibility(ж<ast.SourceFile> @file);
    SymbolAccessibilityResult IsSymbolAccessible(ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, bool shouldComputeAliasToMarkVisible);
    SymbolAccessibilityResult IsEntityNameVisible(ж<ast.Node> entityName, ж<ast.Node> enclosingDeclaration);
    bool IsExpandoFunctionDeclaration(ж<ast.Node> node);
    bool IsExpandoFunctionDeclarationUnsafe(ж<ast.Node> node);
    bool IsLiteralConstDeclaration(ж<ast.Node> node);
    bool RequiresAddingImplicitUndefined(ж<ast.Node> node, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration);
    bool IsDeclarationVisible(ж<ast.Node> node);
    bool IsNameResolvable(ж<ast.Node> location, @string name);
    bool IsImportRequiredByAugmentation(ж<ast.ImportDeclaration> decl);
    bool IsDefinitelyReferenceToGlobalSymbolObject(ж<ast.Node> node);
    bool IsImplementationOfOverload(ж<astꓸSignatureDeclaration> node);
    evaluator.Result GetEnumMemberValue(ж<ast.Node> node);
    bool IsLateBound(ж<ast.Node> node);
    bool IsOptionalParameter(ж<ast.Node> node);
    bool IsThisPropertyAssignmentDeclarationRedundant(ж<ast.Node> node);
    slice<ж<ast.Symbol>> GetPropertiesOfContainerFunction(ж<ast.Node> node);
    bool RequiresAddingImplicitUndefinedUnsafe(ж<ast.Node> node, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration);
    ж<astꓸDeclaration> GetReferencedValueDeclarationUnsafe(ж<astꓸIdentifierNode> node);
    ж<ast.Node> CreateTypeOfDeclaration(ж<EmitContext> emitContext, ж<ast.Node> declaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker);
    ж<ast.Node> CreateReturnTypeOfSignatureDeclaration(ж<EmitContext> emitContext, ж<ast.Node> signatureDeclaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker);
    slice<ж<ast.Node>> CreateTypeParametersOfSignatureDeclaration(ж<EmitContext> emitContext, ж<ast.Node> signatureDeclaration, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker);
    ж<ast.Node> CreateLiteralConstValue(ж<EmitContext> emitContext, ж<ast.Node> node, nodebuilder.SymbolTracker tracker);
    ж<ast.Node> CreateTypeOfExpression(ж<EmitContext> emitContext, ж<ast.Node> expression, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker);
    slice<ж<ast.Node>> CreateLateBoundIndexSignatures(ж<EmitContext> emitContext, ж<ast.Node> container, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker);
    ж<ast.Node> TryJSTypeNodeToTypeNode(ж<EmitContext> emitContext, ж<ast.Node> typeNode, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, nodebuilder.SymbolTracker tracker);
}

} // end printer_package
