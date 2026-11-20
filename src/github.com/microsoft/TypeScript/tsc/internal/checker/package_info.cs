// go2cs code converter defines `global using` statements here for imported type
// aliases as package references are encountered via `import' statements. Exported
// type aliases that need a `global using` declaration will be loaded from the
// referenced package by parsing its 'package_info.cs' source file and reading its
// defined `GoTypeAlias` attributes.

// Package name separator "dot" used in imported type aliases is extended Unicode
// character '\uA4F8' which is a valid character in a C# identifier name. This is
// used to simulate Go's package level type aliases since C# does not yet support
// importing type aliases at a namespace level.

// <ImportedTypeAliases>
global using astꓸAccessorDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸAssertsKeyword = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸAsteriskToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBinaryOperatorToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBindingElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCallLikeExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCaseClausesList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸClassElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸClassExpressionNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassLikeDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸConciseBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEntityName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEnumMemberList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEqualsGreaterThanToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExportSpecifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExpressionWithTypeArgumentsNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸForInitializer = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸHeritageClauseList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸImportAttributeName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributeLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributeValue = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxChild = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxTagNameExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸKeywordExpressionSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸKeywordTypeSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸLiteralExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸLiteralLikeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleExportName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleReference = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedExportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedImportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸSignatureDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸStringLiteralLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateHeadNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateLiteralTypeSpanList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTemplateMiddleOrTail = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateSpanList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeLiteralNodeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypePredicateNodeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypePredicateParameterName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using coreꓸResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.checker_package;

// For encountered type alias declarations, e.g., `type Table = map[string]int`,
// go2cs code converter will generate a `global using` statement for the alias in
// the converted source, e.g.: `global using Table = go.map<go.@string, nint>;`.
// Although scope of `global using` is available to all files in the project, all
// converted Go code for the project targets the same package, so `global using`
// statements will effectively have package level scope.

// Additionally, `GoTypeAlias` attributes will be generated here for exported type
// aliases. This allows the type alias to be imported and used from other packages
// when referenced.

// <ExportedTypeAliases>
[assembly: GoDynamicTypeLift("7374727563747b467261676d656e7420737472696e677d", "ReactNamesᴛ1")]
[assembly: GoDynamicTypeLift("7374727563747b4a535820737472696e673b20496e7472696e736963456c656d656e747320737472696e673b20456c656d656e74436c61737320737472696e673b20456c656d656e744174747269627574657350726f70657274794e616d65436f6e7461696e657220737472696e673b20456c656d656e744368696c6472656e4174747269627574654e616d65436f6e7461696e657220737472696e673b20456c656d656e7420737472696e673b20456c656d656e745479706520737472696e673b20496e7472696e7369634174747269627574657320737472696e673b20496e7472696e736963436c6173734174747269627574657320737472696e673b204c6962726172794d616e616765644174747269627574657320737472696e677d", "JsxNamesᴛ1")]
[assembly: GoDynamicTypeLift("7374727563747b666c6167206769746875622e636f6d2f6d6963726f736f66742f547970655363726970742f7473632f696e7465726e616c2f636865636b65722e54797065466c6167733b206e616d6520737472696e677d", "typeFlagNamesᴛ1")]
[assembly: GoTypeAlias("AssignmentTarget", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExportCollisionTable", "go.map<go.@string, go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.checker_package.ExportCollision>>")]
[assembly: GoTypeAlias("StringLiteralType", "go.github.com.microsoft.TypeScript.tsc.@internal.checker_package.Type")]
[assembly: GoTypeAlias("TypeSystemEntity", "object")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<ArrayToSingleTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<ArrayTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<Checker, go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package.CheckerShape>(Pointer = true)]
[assembly: GoImplement<CompositeTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<ConditionalType, TypeData>(Pointer = true)]
[assembly: GoImplement<DeferredTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<EmitResolver, go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.EmitResolver>(Pointer = true)]
[assembly: GoImplement<EvolvingArrayType, TypeData>(Pointer = true)]
[assembly: GoImplement<FunctionTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<IndexType, TypeData>(Pointer = true)]
[assembly: GoImplement<IndexedAccessType, TypeData>(Pointer = true)]
[assembly: GoImplement<InferenceTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<InstantiationExpressionType, TypeData>(Pointer = true)]
[assembly: GoImplement<InterfaceType, TypeData>(Pointer = true)]
[assembly: GoImplement<IntersectionType, TypeData>(Pointer = true)]
[assembly: GoImplement<IntrinsicType, TypeData>(Pointer = true)]
[assembly: GoImplement<LiteralType, TypeData>(Pointer = true)]
[assembly: GoImplement<MappedType, TypeData>(Pointer = true)]
[assembly: GoImplement<MergedTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<ObjectLiteralDiscriminator, Discriminator>(Pointer = true)]
[assembly: GoImplement<ObjectType, TypeData>(Pointer = true)]
[assembly: GoImplement<ReverseMappedType, TypeData>(Pointer = true)]
[assembly: GoImplement<SimpleTypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<StringMappingType, TypeData>(Pointer = true)]
[assembly: GoImplement<SubstitutionType, TypeData>(Pointer = true)]
[assembly: GoImplement<SymbolTrackerImpl, go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package.SymbolTracker>(Pointer = true)]
[assembly: GoImplement<TemplateLiteralType, TypeData>(Pointer = true)]
[assembly: GoImplement<TupleType, TypeData>(Pointer = true)]
[assembly: GoImplement<TypeBase, TypeData>(Pointer = true)]
[assembly: GoImplement<TypeDiscriminator, Discriminator>(Pointer = true)]
[assembly: GoImplement<TypeMapper, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<TypeMapperBase, TypeMapperData>(Pointer = true)]
[assembly: GoImplement<TypeParameter, TypeData>(Pointer = true)]
[assembly: GoImplement<TypeReference, TypeData>(Pointer = true)]
[assembly: GoImplement<UnionType, TypeData>(Pointer = true)]
[assembly: GoImplement<UniqueESSymbolType, TypeData>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile, go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package.SourceFileForSpecifierGeneration>(Pointer = true)]
[assembly: GoImplement<tracedTypeAdapter, go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package.TracedType>(Pointer = true)]
[assembly: GoImplement<wrappingTracker, go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package.SymbolTracker>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
[assembly: GoImplicitConv<SymbolFormatFlags, nodebuilder.Flags>(Inverted = true, ValueType = "uint32")]
[assembly: GoImplicitConv<TypeFormatFlags, nodebuilder.Flags>(Inverted = true, ValueType = "uint32")]
[assembly: GoImplicitConv<symbolTableID, ast.NodeId>(Inverted = false, ValueType = "uint64")]
[assembly: GoImplicitConv<symbolTableID, ast.SymbolId>(Inverted = false, ValueType = "uint64")]
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(AliasSymbolLinks))]
[assembly: GoFieldAccess(typeof(ArrayLiteralLinks))]
[assembly: GoFieldAccess(typeof(ArrayToSingleTypeMapper))]
[assembly: GoFieldAccess(typeof(ArrayTypeMapper))]
[assembly: GoFieldAccess(typeof(AssertionLinks))]
[assembly: GoFieldAccess(typeof(Checker))]
[assembly: GoFieldAccess(typeof(CompositeSignature))]
[assembly: GoFieldAccess(typeof(CompositeTypeMapper))]
[assembly: GoFieldAccess(typeof(ComputedNameNodeLinks))]
[assembly: GoFieldAccess(typeof(ConditionalRoot))]
[assembly: GoFieldAccess(typeof(ConditionalType))]
[assembly: GoFieldAccess(typeof(ConstrainedType))]
[assembly: GoFieldAccess(typeof(ContainingSymbolLinks))]
[assembly: GoFieldAccess(typeof(DeclarationFileLinks))]
[assembly: GoFieldAccess(typeof(DeclarationLinks))]
[assembly: GoFieldAccess(typeof(DeclaredTypeLinks))]
[assembly: GoFieldAccess(typeof(DeferredSymbolLinks))]
[assembly: GoFieldAccess(typeof(DeferredTypeMapper))]
[assembly: GoFieldAccess(typeof(EmitResolver))]
[assembly: GoFieldAccess(typeof(EnumMemberLinks))]
[assembly: GoFieldAccess(typeof(ErrorChain))]
[assembly: GoFieldAccess(typeof(EvolvingArrayType))]
[assembly: GoFieldAccess(typeof(ExportCollision))]
[assembly: GoFieldAccess(typeof(ExportTypeLinks))]
[assembly: GoFieldAccess(typeof(FlowState))]
[assembly: GoFieldAccess(typeof(FunctionTypeMapper))]
[assembly: GoFieldAccess(typeof(IndexInfo))]
[assembly: GoFieldAccess(typeof(IndexType))]
[assembly: GoFieldAccess(typeof(IndexedAccessType))]
[assembly: GoFieldAccess(typeof(InferenceContext))]
[assembly: GoFieldAccess(typeof(InferenceInfo))]
[assembly: GoFieldAccess(typeof(InferenceState))]
[assembly: GoFieldAccess(typeof(InferenceTypeMapper))]
[assembly: GoFieldAccess(typeof(InstantiationExpressionType))]
[assembly: GoFieldAccess(typeof(InterfaceType))]
[assembly: GoFieldAccess(typeof(IntersectionType))]
[assembly: GoFieldAccess(typeof(IntrinsicType))]
[assembly: GoFieldAccess(typeof(IterationTypesResolver))]
[assembly: GoFieldAccess(typeof(JSXLinks))]
[assembly: GoFieldAccess(typeof(JsxElementLinks))]
[assembly: GoFieldAccess(typeof(LateBoundLinks))]
[assembly: GoFieldAccess(typeof(LiteralType))]
[assembly: GoFieldAccess(typeof(MappedSymbolLinks))]
[assembly: GoFieldAccess(typeof(MappedType))]
[assembly: GoFieldAccess(typeof(MarkedAssignmentSymbolLinks))]
[assembly: GoFieldAccess(typeof(MergedTypeMapper))]
[assembly: GoFieldAccess(typeof(ModuleSymbolLinks))]
[assembly: GoFieldAccess(typeof(NodeBuilder))]
[assembly: GoFieldAccess(typeof(NodeBuilderContext))]
[assembly: GoFieldAccess(typeof(NodeBuilderImpl))]
[assembly: GoFieldAccess(typeof(NodeBuilderLinks))]
[assembly: GoFieldAccess(typeof(NodeBuilderSymbolLinks))]
[assembly: GoFieldAccess(typeof(NodeLinks))]
[assembly: GoFieldAccess(typeof(ObjectType))]
[assembly: GoFieldAccess(typeof(Relater))]
[assembly: GoFieldAccess(typeof(ReverseMappedSymbolLinks))]
[assembly: GoFieldAccess(typeof(ReverseMappedType))]
[assembly: GoFieldAccess(typeof(SerializedTypeEntry))]
[assembly: GoFieldAccess(typeof(Signature))]
[assembly: GoFieldAccess(typeof(SignatureLinks))]
[assembly: GoFieldAccess(typeof(SimpleTypeMapper))]
[assembly: GoFieldAccess(typeof(SourceFileLinks))]
[assembly: GoFieldAccess(typeof(SpreadLinks))]
[assembly: GoFieldAccess(typeof(StringMappingType))]
[assembly: GoFieldAccess(typeof(StructuredType))]
[assembly: GoFieldAccess(typeof(SubstitutionType))]
[assembly: GoFieldAccess(typeof(SwitchStatementLinks))]
[assembly: GoFieldAccess(typeof(SymbolNodeLinks))]
[assembly: GoFieldAccess(typeof(SymbolReferenceLinks))]
[assembly: GoFieldAccess(typeof(SymbolTrackerImpl))]
[assembly: GoFieldAccess(typeof(TemplateLiteralType))]
[assembly: GoFieldAccess(typeof(TrackedSymbolArgs))]
[assembly: GoFieldAccess(typeof(TupleNormalizer))]
[assembly: GoFieldAccess(typeof(TupleType))]
[assembly: GoFieldAccess(typeof(TypeAlias))]
[assembly: GoFieldAccess(typeof(TypeAliasLinks))]
[assembly: GoFieldAccess(typeof(TypeBase))]
[assembly: GoFieldAccess(typeof(TypeMapper))]
[assembly: GoFieldAccess(typeof(TypeNodeLinks))]
[assembly: GoFieldAccess(typeof(TypeParameter))]
[assembly: GoFieldAccess(typeof(TypePredicate))]
[assembly: GoFieldAccess(typeof(TypeReference))]
[assembly: GoFieldAccess(typeof(TypeResolution))]
[assembly: GoFieldAccess(typeof(UnionOrIntersectionType))]
[assembly: GoFieldAccess(typeof(UnionType))]
[assembly: GoFieldAccess(typeof(UniqueESSymbolType))]
[assembly: GoFieldAccess(typeof(ValueSymbolLinks))]
[assembly: GoFieldAccess(typeof(VarianceLinks))]
[assembly: GoFieldAccess(typeof(VerbosityContext))]
[assembly: GoFieldAccess(typeof(astꓸBinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(astꓸBindingElementList))]
[assembly: GoFieldAccess(typeof(astꓸCaseClausesList))]
[assembly: GoFieldAccess(typeof(astꓸDeclarationName))]
[assembly: GoFieldAccess(typeof(astꓸElementList))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸForInitializer))]
[assembly: GoFieldAccess(typeof(astꓸHeritageClauseElementList))]
[assembly: GoFieldAccess(typeof(astꓸHeritageClauseList))]
[assembly: GoFieldAccess(typeof(astꓸIdentifierNode))]
[assembly: GoFieldAccess(typeof(astꓸImportAttributeList))]
[assembly: GoFieldAccess(typeof(astꓸJsxAttributeValue))]
[assembly: GoFieldAccess(typeof(astꓸModuleReference))]
[assembly: GoFieldAccess(typeof(astꓸNamedExportBindings))]
[assembly: GoFieldAccess(typeof(astꓸNamedImportBindings))]
[assembly: GoFieldAccess(typeof(astꓸParameterList))]
[assembly: GoFieldAccess(typeof(astꓸQuestionToken))]
[assembly: GoFieldAccess(typeof(astꓸTemplateLiteralTypeSpanList))]
[assembly: GoFieldAccess(typeof(astꓸTemplateSpanList))]
[assembly: GoFieldAccess(typeof(astꓸTokenNode))]
[assembly: GoFieldAccess(typeof(astꓸTypeElementList))]
[assembly: GoFieldAccess(typeof(astꓸTypeList))]
[assembly: GoFieldAccess(typeof(astꓸTypeNode))]
[assembly: GoFieldAccess(typeof(astꓸTypeParameterList))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationListNode))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(constructorAccessibilityError))]
[assembly: GoFieldAccess(typeof(expandModuleDecl_hoverStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ArrayTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ArrowFunction))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BindingElement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BodyBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CaseBlock))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CaseOrDefaultClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CatchClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassStaticBlockDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConstructorDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.DeclarationBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Decorator))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ElementAccessExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportSpecifier))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowNodeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowReduceLabelData))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowSwitchClauseData))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ForInOrOfStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ForStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HeritageClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IfStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportAttributes))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportEqualsDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IndexedAccessTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.InferTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IntersectionTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDoc))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDocParameterOrPropertyTag))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDocTypeLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxElement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxFragment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxNamespacedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxText))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.LiteralTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.LocalsContainerBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.MappedTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.MetaProperty))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.MethodDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModifierList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModuleDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NamedMemberBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NamedTupleMember))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PatternAmbientModule))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PostfixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Pragma))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PropertyAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.QualifiedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ShorthandPropertyAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.StringLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SwitchStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Symbol))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SyntheticExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TaggedTemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateLiteralTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateLiteralTypeSpan))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateSpan))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TryStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeOperatorNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypePredicateNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeQueryNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeReferenceNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.UnionTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclarationList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.YieldExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.checker_package.Type))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolvedModule))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.EmitContext))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoGetAccessor))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoObjectElement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoObjectMethod))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoParameter))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoPropertyAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoSetAccessor))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoType))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeDirect))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeInferred))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeMaybeConstLocation))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeNoResult))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeObjectLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeSingleCallSignature))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeTuple))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package.PseudoTypeUnion))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package.SourceOutputAndProjectReference))]
[assembly: GoFieldAccess(typeof(recoveryBoundary))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/checker.go", "checker.cs", "AJYB4gKSkpKSAI0B5gIAlATMCKKUgoKCgoKCgpKSgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCsrKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgqaCgoKUpoKCgoKmxoKClKaCgpSogqyCrIKqgqqCvqKCgpaCgoKClJSmsoKUgqaigoKCgoKUgpSCpoKUxoKClLbGhKaihoaCgoKmogAOIgAQJsSCgoKEgoKmhoKUuIKChIKCAAQWgoaCuIKCgoKEgoKCgoKCgoKCgoKElIKClIKEkpSGgoKC3LKCgoKUlKiigoKCgoKCgoKmgoKUpoKCpqaygoKIlIKYgoKWgoKWgoyWhIKUhIKCgrioyqKCgoKCgri4ogAQIqIADRyCpoKmgqzCgoKagqaClKaygpSCgoKCgpaCgoKYgoSCgsqmtoKUAAAQlIKCloKCgpaCgoKClIKClIKWprKCgoKCgoKCgoKCpoKmpqKCgpTGgsaigoKGgpSUgoKCgqamsoKCgoKCgoKCgoKCppSUgoKChoKUgoKClJS4xoLGooKCgqaCgpSmooKCgoKmpoKCgpSmggAIBoKCAAcQgoKClKaCkoKCggAIDoKCgpSCgpQAAiCigoKClIKUgoKCppSm4oIAABiCgoKogoKGqIKChIKCgqaCgoKmqoKCgoKUgoaC3LKChJaGgpSCgoKCgoKCgpSCgqaCysKCgoKEloKUhJaCgpamqoKCjJSUgpSCgoKClJSUpqakAAEShJaClIKClJSmgoKClIKUgqaCgoKCgoKUgoKCgoKCyoKC3IKCgoKClJSCgpTK2KKYgsiCroKClIKClIKmqLSCmIKUpqiU6MiygoKUqrKCgoKClJSmsoKCgoKClJSmgoKUgqaigoKCgpSChILMptKCgoKAgraCgoKCgoKUgpSCgpSEgpSUgpSipKKCgpS4ooKCgoKCgoKUprKCgoKCyoKCuJS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS02LKCpoKmgqaUlIKUgoKCigABFIKCgoKUgqaCuJSClKa0hpSkpKSElAACHIKCgoK4ooKCgpSUptKAgrSCgoKYtLS0tLS0tLS0gsa0oqSigrimlNiigoKAgqSCgoKCgoKmgpS4pqKCprKCgoKCpoKClJTKtIKClIKCgoSCgpSCgoKUgqbSgoKCgoKCgoKAgtSCgoKCgtikgqa6goKCgoKUgoKUgpSCpoKUgoKUgpSClIKqgri0gpSChIKCyqKClKa0lLS0goSElIKmgoKCgoKUgpS0xoKCgoKUlIKmggADGKKCgpSChoKUprSCgoKohIKWgpSmpIKmtISCgpSCgoSCmoKClIKCgoKcioaClIKCgoKUgqqCuIK4goKCgpSCpKSUgsaipoKCpqKUpKSU1sa0gpSCgpSCgoKCgr6ClIaCgoKCgoKCgoKUgoK4goKUgqaigoKChIKCuIKCuLKCgoKCgqaCgoLcooKCgoKCgoKClKamooKUpKSkpKSCgsamtoKChIKUgoKClIKCgoKUgoKCuIKCgoKCgqaC3KKCloLGpqKCgoKUgoKUgoK4poKmooKCgoKmooKCgoKCgoKCgpSUgpSUgpa2hNiCgoKCuIKEgpiCgoKYgoKCgpSCggAHEKKCgoKCgriCyqKmsoKCgoKCgoKCgpSCpoKCgpSCgoKClIKCgqaCpqKCpoKmooKmoqayhpSCgoKCgoKCgoKEgoLKprKCgoKUpqKCgoKClIKmgoKClIKmooKClIKUgpSCpqKCprKCgoKCgoKUgoKClIK4ooKCprKCgoiGlIiChoKUhKaCgoKCgqaGgpSIyqSCgrjCgoKCgoKCgoKCgoKCgoKCjIKClJSWgoKCgoKClIKCgoKCAAEYuLS0/JKCgoKCyoKCgpSCgoKCgpSCmIKCgoKIioKClJSCgriCgpiClLiCgoIAABCUgpSCgoKCgoKCgoKClJSClIKCppSCgriCgqaCgqaCgoKCpoKClLS0grqClIKCgoKCgoKCgoLuooaCgoSUlKaigoSCgoKUprKCgoKYgpiClIKCgoKmgpSUuLS0iIKUgoKm2KKCprSClIKCgpSUgriigoKCgoKClKaigpSmooKCgoL4soKClIKUgoKUgoKUhIKUgoKegoKClIKUtLSCgpSClISCgpTKooKCgoKCppSCgoKUlKaigoKCgoSCloKCgoKCgoKUgoKCgpTKlKaigoKmooKCprKCgqaCgoKUpoKUgpSCgriygoKMgoKClJyCgoKCgpaqgpSCgriigqaygoKCgoKUgoQAAxCClIKEiJSCioK4goK4goK4uIKCgpSClIKCgpSCgpSCgoKCgoKUgoKUgoKUhLyygoKCgoKCgoKCpoKClIKClIKmsoKCpoKCgoKCuLSCgoKCgoSCgpSCpoKChKaCgoK4griygoKCgoKCgriClKaigoKCpqaigoKCgpSCuKKCgoKCgoKClIKUgoKCgtymooKmsoKGlIKUgoKmsoKCgoKCgoKCgoKCgpaCgqaCgoKCgoKCgoKCgoKCgoK4goKWlIKClIKGuIaClqamgoKCgoKCpoKCgoKmgoKmuIKCgqaigpSCgoKUgoKUgoKUgoKC3LKClIKCgoKClAALGLKClIKCgoKClIKEgoLcoqqigoKEgoKUgoaCmIKGgpiCgoK4poKCgoK4pIKCgpSCgoKCgoKCyoS4opSCgoKCgoLGpAAMBgADHoyCgoKClIKClIKIhIiCiIKClISmgoKCgpQABxKChJSCgoKEjJSCgoKmgoKGhoKCgoK6goKElJSClJTGgpSCgpTGgoKClMaCgpT8ooKUpqKosoKCgoKCpoKCgoKCgrjcgoKCpqaigqaCgqaCgpSCgoKCuKbCgoKClKaCgoKClIKCgoKUpqaCgoKUgoKmlJSCgpSmgoKUpqaCpqKCgpSCgqaCgoSCgriCgsqygoKClIKClIKClIKClIKClIKCgpqClpSCgoKUyrKCgoKUgoKUgoKUgoKUgoKagpaUgsqkgoK4iIKClIKCgoKCgsyCgoLcsoKUgoKClIKCgoKCggAHEKKmooSCgoKCgoKCuKbCgoKUlIKCgoLWsoKUgpSCgoKChIKCgoSCgpSmgoKCgoKmlIKCAAsQooKClIKCgqaCgoKCgpSCgoKCgoLKpoKmsoKCgpSCpo6CgoKChIKCuIKEgpSCgpSCgoKU7qKClIK4soKCgqaCgoKUgoKUgqaElIKCpoKCgqaChIKClIiUgoKCgoKCuIKCpoKCgp6CgoKmgoKCgqaCmNyygoKCgoK4ooKUprKClIKCgpSUgsaCgqaCgoKmAAgGgqaipoK2toKUtKSCgoTstIKClJSEgpSClIKCgoKCgoKUgoKCgoKEgqaCgoLKiKaIlIKCgoKUpqaigoSUgoKUgoKClIaKgqaCgoKCuKaygoKCgoTKsoKUgoKCguqigsqigoKUgoKUgoKCpoKCpoKCgriCuIKClIKUprKCgoKCgoKClIKClKaigpSkpIKkprKmgoKUgoKUgoKCgoKCgoKEgoKmgqaCpoTcsqaCgpSCgpSCgoaClIKCgqqCgoKCgpSChJbKpqKClIKCgpSCuLKCgoKUgoKCloKClJSE2IKCgpTGsoiCpoKUgpSEgpSUlIKUgoKCgoKChISCppSCppSCgoimgoimyoKUgoKUgoKClIKUhISChOqCgpaClsaigoKChoKCgqiCgpiClIaGlIaCgsq4ooKCpqaigoKmxqLGgqaigoKClKaygoKUptKAgrSCoqaygoKClIKCgpyCgoKmgpSCpoqClIKWgpaCgoKCgoKCgoKChMyCloKCloKClIKChIKCgoKUqIKCgoK4loKCgpSClIKGgoKCgoKCgoKCpoKCgoLKgoa8goKUgpSCpoSCgpS4goKmgoKCuAADNoKYgoKCgpSCgoKCgoKYiIKC7raClIKClIKCgpSCgoKClIKCgpSCuIKCgsqygoKCgoKWgoKUgoKktIKClLSkpKSmooKUgoKUprKCgoKUlIKCgoKCgoKUtLS0tIK4goKUgpSmgoiChoKGgpSClIKGgoKUuIKMgoKUgoKUlJSChIKUgpSUgpSsgoKUgqaigpSCgoKClKiCgpSCAAIssoKClIaCgoKUlIKClKbCgoKCgoKCgqaUlJSCgoKCgpSUgoKCgqamgoKCgpSUgoKCgqaClKaGgoKCuKa+iIIAARKClMaCAAgOoqaipoLGgqaClKSkpMaC7oKCgpSmooSWgpQABh6ygIKCgpSChoKClIK2roIAAhKigpSCgpSmvoiCAAESgpSmgu7ihIKUgoKClKaCloKClIKCgpQAARKCgoKCgoKCgoKClKiCgoKClJiCgoKCgpSUhIKEgqiCgoKUlIKCgoKUgpSClAACEKKCmIKUgpaCgoKUgoKClIKcpoKmgqaqgqaygoKUlIqmooKClIKUgpTGgpSkxrKCgoIAARCChIKCgoKCgoKCyoKClIKAhoK2goKUlJSGgqaChoKCpoKCgoKUgoKClLS0tIKUgsyCgoKClJTYgoKKlIKCgoKCyoKCgsqChJSClILWtIKCgpSUgoKCgoSYlIKmpqTYtIKGgoSogpSCgoKCgoKCgpSmqIKCgoSCgoSCgoLcoqSkgpSkpKSCgpSWgpakgoKClKSkpKaCgoKCgoKCgoKUgoLegoKSgoKCgoLKlKaCgoKmgoKUgra4gpS2tLTqggALFKKClKbCgoKClIKCyoKUpKTYsoKUgpSCgsaCggAIDOqygoKCgoaUgpS0goKUgsaC6oKClKaCuKKCpqKCgpS4oqaigoKUuIKCgoKCgoLcsoKClIKUgpSEgoKmiJSmooKCgoKUpoKUguqipqKUpKTosoKCuLKClIKClIKCgpSCgtyipqKCgoKCgoSUyqSCqqSCgpaClIKEgoKUlKqigpSCgpSogpSkpKSkpoKCgpSmooKCgoKUpoKmgqaygoKCgoKmgpSCgoKCgpSUpoKCgoKCgoKUgoKmlOaigoKUgoKUgoKClNwABxCygoKChoKagpSCgqaihJSmgqaigpSCiIKCgoKCgpQAAhCigpSCgoKCpoKmwoCCtIKCgoKCgpSC1sqKgpqCgoKCgsqigpSCgoKClIKClIKClIKClI6CgoKCpoaChoaSlIaSmoKCggADEKaigoKCgoK4trKCgoKCgoKCgoKCgoKUpoKCgqbGgqqCgpSCgoKClNiylKSkpKSkgpSkgqSCpKSkpKSkpKSkpIKUpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpLSmsoKCgpSmooKClKaygoKWgoK4koYAAhCCgpSUpoySgpSClJSUtLS0tJSClISWgoKClIKClJSCgoKUgpSEgpSCjIKCuJSmgoKClIKUuLKCgoKCgoKCgpSClIKClIKUgpSmooKmsqaygoKClKaygoKCgoKCgoaCgpSClIIAAxyCgoKCpoK0guaCgrSCgoKChNiCgpSClIKCgoKmlJSmsoKUgoKUgoKCxoKCxqKCgoKUlKaigpSmooKCprKCgpSCgoKUgoKUgoKUgoKClJiCqrKCgoKCgoKCgpSCuKiygoKCgoKCpoKUqKKmwoKWgoSCgpSUgpaClJSCgoKCgoK4gqaCgoKCpqa0goKElIKCgoKWgpSClIKCgoKUgoKCgriWgoKCgoKClKYAAhCygoKGlIKClIKChIKUpoKUhoKUgoKCgoKmprKClIKCgriCgoKmooKUgoKUgpaCgpQAAhSiioKClIKOlIKChoyCmoKUpqailKSkpKSkpKaygoKChJSUhoKCgqaUgpSCgoKClLS0tJSUgoKUgoScgoiGgpSagoKUgoKCgoKmlAABHIKClKaygoKegoSagoKUnIKCgoKClIKUnIaClIKCgoKmnIKCgoKClIKmlIIACxCigoKUgoSClISCgoKCgqbcpqKCgpSCgoKChIKCgpSCypSClMaCgoKmxrKCgoKElIKCgpSCgoKUgpSmsoKUgoKClIKCgpSCgoKUgoKCgoKUgoKUrIKsopSkpKSkpriCgoKCgoKClIKCgpSClIKCqAATINKCgoKCgoKCgoSCpoKCpoamAAAagoKUAAEyAAAWgoKUgpSClAAAEIqIgpSUpqKCgoKCgoKCooKCgoKUgoKUgqqCgpSGgoKIlJSClJTGosaigpSCgpSCgoKmsoKCgoKCgpSCgpSUgoKUgoKCgoKCgoKmgoKCpoKClIaCgqaUhIKUiIKCgoaCgqaEgqaClKaygpSChIKClIKChoSaxrS0goKUgoK2pIKUloSCgsiCmIKUgoKCpsaixqKClKyilKSClKaClKSkpqaCgqaygoKCgoKCgoKCgpSCgoKUgoKCgoKUlLimsoKUgoiCgoKCgoKmgoKmgoKCgpSUgoKCgoiCgpSUgoKCuIKCgoKClLS0gsaCgqamooSClIKCzKKClIKUtLS0goKCpqaygpSClKSkpoK6psKCnIKCgoKCgoIAABaIggAAEIKCgpSWAAEUgoKClIKCgpTKgoKClIKSgoK4goKClIKCgoKCgriCgpSopIiClKa+goKUgoKCgpSCgpSCgpSUgqaCgoKCgoKUgoKmpqKClIKCgoKCgqaUpoKCgqaigoKClIKCgoKCgpSUgpSUlIKClJSCgoKClIKUpqKmhKaiprKClIKCgoKmpsKUgoKCooKClIKUgpSCxrS0hoKU6sKCgoKGgoKCgoKC7qKCgpSCgoKCgoKChIKClISClIKmgoKUtLS0goKClIKUgpTGtLS0loKClKaCgpSCgpSCgpS0tLSUrIKCgpSUgoKCgpSCgpSCgpTIooKUgoKUgoKUgoKUgsaigoKCpsaigoKCgoKEgoKCgoKUloKCgoKCgoKCpoKUpoKUpoLusoaUlLS0tILGpqKCAAIQxKrCgoKCgoKClIKCgoKCgoKEqIKClIS4gpSCppSEgoKCpoKClKaCgoKUgqaygpSGgoKClLiipoKGgriigpSCgqaipqKCgoKmooaCgoKCgoKUgoKCgobKprKClIKCpoKCgoKCpqaCgoLKsoKmsoKClISEgoKClIKCgoKCppaCgpSCgqaCprKEgoiCgoKClIKCgoKCgoKCpoKClKaWlIKCgqaGgoKmyqKCgoKCgoyUgp6CgoKC7rKCgoKCgoKCpoCCgoKCAAUUooKClIKUgoKCgoKElqaUpKamgoKCgoKqgoKCgpSmooKUgoKUtLS2qKKCgoKCgpSUgpSmwoKElJSCgoKClKaCgoKCgoKCgoKCgrimhIKEgsqigpSCuLKCgpSCgoKClKaChIKUvLKCgoKCgpTcooKUgoKCgoKCgoKUgrikgpSCloKWgoS4ooKWgpaChLiygpSYpISUhIKmgoSUpqKChoLcooSCyrKCgoKCyqKCloKWgoS4ooKC+rKChIKCgqaEgqaCgqaCgoLKsoK4soKmooaUpqKCgqaigoKCgoKmgoKUgoKUpqbCgoKUgoKUgoKSlpKCgpSmgpKCgoKSgoKCgoKCgoKCgoKCgpSCgoKCgqaCgoKUlIKCgoKmlIKCgoKUlIKCgpSmsoKCgoKClIKmsoKUpIKClKTWooKCgpSCgpSCprKCgoKUgpSCgoKUpoSmsoKCgoKUgpSCgoKUpqaygoKCgpSUgsqCgqaygoKCgpSmsoKCgpSUlKTWgsaUgoKUgoKUlKSCgpSkpNaChJSkprKCgoKUgoSUpqKCgpSWpqKCgoKCgqaCprKCprKGgoKUlIKClIKElIKGgqyCgpiCgpSCgoKClIKClIKUgoKCgoKCuKaigoSCgpSClMqygoKUprKClIKClIKCgpSUgpSCgoKUgoiElIKCgoKClLS0tLS0tIKUgoKUlKaGgoKClJSClJSClIiCgoKCgoKCioaahgAAFIKUtLS0tLSCgpSagoKCgpSUgoSUhJSmgoKClKiygoKUprKCgoKClIKCgpSCpqaiqqKClIKmooKCpuKCgoKClIKCgoKGgpSCpoKClIKCgpSUgoKmgpSEgpSCgqaCgriCgpSClJSUgoKCgoKUgoKClIKCgoKClJSClKaUgpSCgpSClIKmgoKUgoKCgoKClJS0tMam2IKClIKUgpSKgoKChIKClIS4goKElIKUpqKqsoKCgqaoooKCgoKmpqKClKiCgoKCgoKUgoKmpqKmtoKCgoKCgqaCgoKKgoKCkoKCgqaClKbCgoKUgoKClIKClIKCgoKCuIKCgpSCgoKUgoKCgpSCgoKCgoKmgoKClJTKpoKCgoKCgoLKpqKCgoKoAAIWogACGKSCmIKClNaixqKClILGooKCgpSsopSCxqSCxqaygoKClIKUmKbSgoKUgoKMgoKUgriipqKCgoKmAAIWgqaigoKUtLS0tMYAAhrSggAAEIqClJiCgpSohoKCgpSogpiCgoKUgoKClJSYgpiCgoKCgoKUloaCgpSCgoKUqIKUhIKUpoKClJSmgqqygpSCgoKmqKKClKiCgoKUvIKCgoKUuIKCgoKClLiCpoKCgoKClJSqgoKClLiGgoKClISCqIKCpoKUxoKCgpTGsoKUgoKCgoK4goKCiIKCgpqClJSYgoKCgoSCgoKmuKamgoKCgpSEgoKUgoKClJSCgpSUtsiCgoKCgoSCgoKCyoKUpoKmgoKClKaygpSCgoKUhoKUgqaCgoKClJSUhISUgqamooKChJSsgp6mhoKGtoKUqtqigoKCgriCpqamwoKCgsrCgoaCggAEFKKCgoKmsoKCgpSCpoKIgoKClJSCgoKmsoKCgoKCgoKClMqigqbSgoKUgoKCgoKUgoKClJSCppyClIKGgoKCgqiCgoSCgoKUtILGlIKUgoKYgoTYpIKUgoKUgoaChIKEgoaClKiClIqChpSClKSCgoKCgpSCgqaqgoaClIKYpKSkgoKCgpSUgpSkgoKUgpSkgpSCgpSClKSCpIKCgoKClJSCpqSmwoKCgoaClJSUlIKChIKmgpSClKaygoKUgpSo4oKCgpSCpoKCgoKCgqaCgoKClJSCgoKUgpSCgoKCuIKClIKmsoiCgoKCgoKUlJSmsoKCgoKChoKCgoKUgpSUgpSCgpSCgoKUlLimooKmpoKUpoKCgoKClIKCgpSClLTYgoK4goKCgoKCgpSm0oSCgqiClIKCgoKCqMqCpoKUpKSkpqKCgoKUgpSogoKUtLSCgpSmooKCgoKClIKClIKClIKCgpSCuKKCgpSmoqaygoKUgoKUAA0YooKWgpSmpKSClKSkgsamsoKCgoKUgoKClIKCgqamooKCgoKUyqKCAAEUqJqoroSEgpSkpKSkgpSkAAIWooKcpKSClKiU1qiigoKqsoKegpSChpyIgqaygpSChpiCgqiWioKopoKqgoKUqoKmhIKCgpSCgpSmtIKCgpiUgoSCgoKUgoKCgoKCgoKmgoKClIKCgoKIgoKmgpKCgoKUgpSClIKCgpSClIKUlIKCgoKUgoKUtLS0goKClIKClJSCgpSGgoaCgoKCpoKCgoKCgoKUgoSCgpSUgoKCgoKCgpSCgoKClIKClJSClJ6ClIKCgoKClJSCuJSUgoKUgoKCgoKClpKClKamsoKCgpSCyqKClIKClIK4goKCgoIABRbCgpSClIKUgpSCgoKYlIKCgpiUgpSCgpqCgoKCgoKmlIKCgoKUlIKCgoK4goKUgoKCgoKCgoqCgoKClJSCgoKCpqaGgoKmooKUpoKCqLKCgoKCkpaSpraiprKClIKCgpSUhoKUloKWgoKEgoKCgoKClJSCgoKmgoKooqiygoKUgoKCgpSUgoKCpqKmoqyygq6igpSCgpSCgpSCpqKWpKSCgqSCgoKUpKaygpSUpKSkpKSCpIKC2KaogpSCgoKClKaigoKUgpSCgoKClKaCgpSkpKSk2rKCgoIAATKChIaUgoKCgoKCgoKWgoKCgoKaAAc0goKCgoKCgpSCgoKC/KaigpSCgoKCgpSUlJSmAAMQAAIQpIiClIKmgoKUpKTIooKCgoKmlKaCpqKCgpSmoqSkyMjIyqSClKSClNiCpoKmooKCgoKUpqKCpoKmgoKUpqSClKakgpSmoqaigoKmoqaigoKUpqKClIKCuIKmgoKmgoKCgoKmprKCgoKUpqaipoKCgoKCpoKCgqaCgoKmgoKCpoKClIKUgoKCpqKCgoKClJQAACCCpgADEMKChpSCgoKUgpSCqISUgoKUgoKUgoKUgpSClIiCppSmwoKCgpS0tLSCgoKCgoKUgriygriCgoKUgoKCgpSCgqaUgpTqoqaCgoKUxqLGgoKUpoKCgpSClIKUgpSClIKUgpSClIKUgpSClIKUgpSClIKUgpSClMaihIKCgoKCgqaCgoKCpqaygpSmgqaigpSmooKUprKCgpSqsoKClKaygpSChIKClKaClKaCpoKClKa0goKClIKCgoKCpoKUgoKmooKCgoKmqK6CloKYgqaigoKCgoKmqIKClIKUgpS4goKUpqKCgpSmwoKCgoKGiJSMgpSUgpaCgoKCgoKUgoKCgpSCgpSUlISCgpSClIKmsoKUgoKClIKCgpSClILcooKCgpSUgoKmsoKCgoKmsoKCgoKClKaygoKCgoKCuIKClIKCpraCgpSCgpSCgoKClJSClIKCjIKClISClJaCgoSUgoKCgoKCuIKCgoKmgoKClKamooKCgqYAAiqygpSClIKUgoKCgoKUgoKCprKCgoKCgpSmtIKCgoKUgoKUpqaygoKUgoKElIaYhoaCgsyEgoKYhpqWgqimgoKUprKClIKCgoKUgoKCgqaClMqygoKUgoKAgoKUppaCgpSUgsi4ooKCgpS4soKCgoKCgriCgpS0tLSCpqyClIKCpqKCgqaigpSClIKClIKmooKCgpSmooKCgqamopSkgpTGpKSkpMailKSkAAQwooKWgoKCgpSCgpSmooKClIKmooKCgpSmpqKClKaigoKCppSCgpSCgpSCgpSmAAQQgoKUgqaCgqaCqoKCgoKCgoKUgoKmgoKCpoKCgqaCgoKC3IKUppSCgqaCgqaEgqaCgoKCgpTMgoKCiIKCgriClMyKgoIABhC6grqCgoKUypaCgpSCgpSUggAIFIKCgpSChIKChJaCgoKmgoKCgpSmypSCgpSClKaCgqaCpoKC3pSmhIKCgsy4gpSCgoKCgoKClJSCgpTKrLKClIKCgqaCgpSCgoKUpoKCgoKCgoKmtIKCgpSUgoKClKbGggAKBoKYgoKClLS0tqbWgpSkpKSkpKSkpKSmsoKmgoKUAAkYhIKCpoKC3KaCgoKmpqKClISClKaigoKCgoKmgoKCgrimsoKCgqaqwoKElIKCgoKUgoKClJSCgoKmgoKCiJSCgpSmgoKCgpSUuKaCxoLGsoKCgoKUgoKUpsKCgoKUgoKCgoKCgoKClKaUgpSmooKUgpaCgpaCpoKCgoKUlIKElKbCgoKCgoKCgoKUprKCgoKCgoKCgoKCgqaigpSUpKSkpKSkpKSkpKSkrLKClIKUgoKClKaCgpSCgoKChKaCpsaCtIK0tIKEloKmpsKCgpSClIyCgoKCgriCgoSUgoKCgoKCgpSCgpSCgoKUgoKCgoKCuKamooKCgpSCgoKCgpSUpqKmooKUpqKClIKUprKCgoKClLS0hIKCgoKUgpS0gpTqgoKCgoKUypQAAjqygoKGgoKClJSCgpSUgoKEgoKCloKGgoKUlIKClIKUgoKUlIKCgpSmpqSCgoKClIKUmoLYosyygoKCgoKEgoKCgpSCpoIAAxKigpSmooKCgoKUAAsUwoKGgpKIgqaClIKEgoKCgoKCgpSEgpSCppSCgpSCppSCgoKogoKCloKCgpSChriClK6igoKUgoKCgriCgsqigpSCrLKClIKCgpSCgpSChJSCgoKmpqKCgoKmpqKCgpSmsoKUgoKUgoKCgpSCgoKUgpS4pgACLoKmsoKCgpSCgpSCgpSEgpSClJSClKaipqKCgoKClKamooKCgoKClKamrLKCgpSClIKUgoKUlKbCgoqEgpSCgoKUlIK4gp6CpqaygpSClIKUgpSClIKUgpSClIKUpqKmooKClKaigoKUpqKCgoKCnoKUlKaigoKUgpSClKa0gpaClIKCgoKClJaClIKClJSClrS0tLSClMa0tLS0gpQAAhiCqLaCgpSCgpSspIKUgoSCgoSClJiClKaClIqCgpiCpoKElISCgoKCgoSClKaCloKClJSCqoKClIaCgoKUtLSClJSCgpS0tIKUpoaYgpamooKCgoKUpoKClILGgtim0oKCgoKCuIKUgoKUkoKClIKCpqKCgpSmwoKClIKCgoKCgoKUgoKCuKaigoKCgpSCgqamooKUpqKCgpSmsoKChIKCpoKCgoKUlIKUpoKClKSoAAIWsoKClIKCgpSClIKGlIKCgpSUgoKCgoKCgoKmgqaClJSClKaipqKClIKClKqigoKCgoKmxoLGooKmopSkpKSmgoKUpqKosoKmgoKCgpSCgoKGgpS4poKUlIKUpqKCpsKCgoKKgoKCgpSUgoKCgoKOkqiCggAHELS0voKCgrjqgpSmooKCgoKmprKCgoKmgqamooKClKaCgpSmooKGlIKCgoKmgoKUpoKClKaCgoKUpqKCioKClLS0xqayggAAGIKCgpSCgoKCuJSClKaygoKCgoKCjIKCgoKCgoKCgoKCpgACFKKCgoKUgoKCgoKC3AAICoIADBKCgpSqgoKmgoKUgqaCgoKCgqamgoKUgqaCgpSCpoKmgqaipoKCgriigoKClLiigoKCgoKCgoKCgoKUgoKUgoKCgoKUgqaCgoKmgqaCgtiCgoKmgoKCpqKClLSCtIK2goKCtLSCpoKCgoKUlKaCgoKUtLS0tIKmgpSmgqaCgoKCgpSmgoKCgoKCpoKCgoKClIKClKaCgoKCgpSmsoKUgoKCgpSCgpSCpoKCgoKUpqKqoqiigoKUgpSCgqbGgoKUpKSmsoKoooKCgpSqooSCgrim1IKUhIKCgpSCgpSCgoKClIKCgoKCppaCgoLMgoKIgoKUlIKYlIKCgpTGtIKUhoKUlKbSgoKUgqiCgoKCgoKUpoKIlIKUgoKUlIKSlIKCAAIcgoKClKaygoKCgoKCgoKUgoKCgoKUgqamooKUpKSkpAACFKKClIKClJSClKiygoKCgoKCgpSChIKUgoKCgpSCgpSCgoKClKiygoKCgpSEgpSUloKCgoKClJSCgoKClJSClIKCgoKUrLKIgoKUlIKUgpyClKaCgoKUpoqCgoKUAA0YsoKClIKUtIKUtLSCgoKCgpSEgriCgqaCgoK4hISClKaygoKkpIKUpKSCgpSUgpQAAhCigoKmpoKCgoKUgpSUpqKCgpSCgqaCgqasooKUgoKClJSCgoKCgpSChIKmgqaCgoKCgpSCpoKCpqKCgqbChoKmgqiClJaClJaCgqamsoSUgoKU2IKCgoKGgoKCppSClMa05oKEyIKClJSUlLSClJSktOaClKSClMamgqaygoKCpoKUtLSCgpSSgpSatLS0gpSUpsKCgqaCgpSCgoK4iISCgpSUprKGlIKCgpSCgpTGooKUgoKUgsaigoKCgoK4lKaigoKCgoKCuJSmooKUgoKCpqKCgpTWsoKCgpSCgoaCgpSClIKCpoKUgoKCgoKClJSCgoKCgoKCgpSUgqamooKCgpSCgoKCpoKCgpSWgoKUuKaigoKClIKMgoKUpoKCgpSmpoKmooKUprKCgoKUqLKClKSkpKaigpSmgqaCgpSmooKUpoSClIKCpoKCprKClKaCgpSSgoKkpqKUpIKkgqTIhIKUgoIAAhyCgoSClJSCroKCgoKCpoKCgoKUgqamopSkpKSkpKSkpKSkprKEgoKCloKUgoKCrKaigoKUpqKClKaygoKCgoKCgoKCgryCpqSmggACFrKClIKCgoiUgqaClIKUtLS0goKCpqSCgpSClKSkqIKCgpSmgqaigpSCgpSmgqaigpSqgqqCgoKUpoKClKaCpqKClKbEgoKCgoKCrJSClKSkgoKCgoKmyLaqooKUlLS0tLS05rS0xqaCpqKCgoKCgpSmsoKCgoKCgoKCgoKClIKCgoKClIKCgpSCgoKCgoKUgoKCgoKUlJiUxqKCgqbGsoKUgoKClJS0gpSCxrSCgoIAAxCClKaygoKCgoKUpqaygoKUgoKCgpSIgoKagoKClJSClIKCgoKCgpSCgpTGgoKClMaCgoKSgpS4goKqsoKCgoKCgoKCgoKmpqKCgoKCpqKCgoKClKaCpoKmoqiCpoKCgpSmooKCgoKUgrimqoKCgpaUgoKUgoKUlJSmsoKUgoKCgpSmsoKUgoKCgpSmroKClJSmsoKClIKClIaagpaCgoKosoiCgoKClKaCgpSUlJSCkqamsoKCgoKCgoKClKYABxCmgoKCgpSmgqiigoKCqqqCgoKCgpSClJTGooKUxrKCgoKCgpSUgoKCgoKUlIKUpoKCgoKClLimooKCgoKCgoKUpoKCgpSowoKCgoKCgoKCgoKCgoKCgoKClJKCgsqCgoKUlIKClJSCloKCgoKUgoKCpoKCpriCgoKogpSClIKmqLKCgoKGgoK4goKUpqKClIKClKaigpSCgpSmooKClKailKSkpoamoqqigoKmpoKmsoKUgoKCmoKChKqCgpSUtrKCgpSCgoKCgoKKgpSCgoSCgpSCgpSUgpaIgqiCgoKCpoKClIKClJSClIKUgpSCpqKClIKClKaCgoKUpqKClKyCrIKCAAIWooKUgpSmgoKmooKUgpSmosaigpS0tMaygpSClIKUtLSCgoKU2IKCgpSCgoKCgoKCgpS4lIKUpqKClKaygpSCgpSClKaigpSmsoKCgpSmpoKClKaigoKUpqKClKS2xoKCgpTGsoKClIKCgoKCgoKUgoKUisaCgoKClIKClILGgoSClpSEgoKClJSClpa0goKUgpSClIKCgsiCxrSCgoKUpoKUgpSCpoKUgoKUgoKCppqClKiygoKCgoKCgoKUhoKUhIKUgoqUgpSClIKUgpSClKaigsaClKSkxsKCgoKClIKUgoKCgpSUgpSUprSChoKUprSChoKUpqKCgqaUgrimooKCgoKClIKUlIKUpqKCgsi0tJSmooKCgpSUpsKCgoKChIKCloKUprKChILcgoKClIKClIKCgpSkgqSCpAACGLKCgoKClIKmgoKUgoKCgoKEgoKUgoLupqKUpIKCloKChJSCgpSClIKmpsKCgoKUgoKElIKCpqaygoKUgpSCgoKYgoKClKaigpSmoqbCgoiCgoKspoKCprKCgpSmsoKCgoKCgoKClIKCgoKCgoKCloKCgoKCgqaUgoKCgoKCgoKCgqaCgpSClIamioKCloKCgoKUuIKClIKClKaCgpSCgoKmpsKClIKCmIKCloKmiIKYgoKCgoKCgoKCAAQQgoKCgpSkpMiqooKClKyilqSkpKyigqyigsbCgoKCgoKClIKCgoKCgoKCgoKClJSCgoKUlKa2soKEhoKCgoKCgoSCgsaCiIKCgoKUgoKUgoKCgoKUgoKCgoKCgoKCgqaUgoKUtLSCgoKCprKCgpSSpoSUlKaygoKCgoKUgoKIlLS0goKmgpSmrLKUgoKUpIKCgoKmpKiCgpSkpqaCgpSUgpSGgqakgoKClIKCgqiGhqaygpSmsoKUrqKCgoKCgpSCgpSEgoKChIKChoKCgpSUlIKUyoqCgoKCgoKWlKiCuJSmwoKClIKElIKCgoKClIKCgoKClJSCgoKUlIKUpqKCgoKSgpSUgoKCgoKClIKClIKCgpKClIKUgpS0tLSClJSCgpSUlIKCgoKClJSmooKUgpqCpqaCgoKCgoyCgoKigoKmlIKCpqaCgpamprKCgoKCgoKUgpSCpqaCgpKCgoKUgoKUpoKClKaigoKCgoKmrqKCgoKCpqaChIKUAAIQooKClJSClIKChIKCgrimwoKCgoKEgoKCgpSCgoKCgoKCgoKCgoKUpoKCgoKIiJSClJyCpoKCgpSCgoKUlIKCgpSUgpSClIKCgpSCgoKCgoKUlJSCgpTKhJSOgpiCgoKmgoKagoKCgoKUgoKUgoKCgpSClIKCgoKCgoKCgoKClIKUgoKClIKCgpSUgpSClIKUlIKCgoKElIKCgoSCgoKCgpSClJSCgpSmpqaClM6CxoKCgoKUgoKUlIKCpoKmpqKCgoKCgoKCgqaigoKWAAIQsoKCgoKmlKSkpKSkpKSkpKSkpqKCgpSmooKCjoKCgpSUgqamwoKCgpSUgoKCgpQAAhKylIKClIKCxoKCgqaCxqaigoKUgoKUpoKmiqaigoKClIKCpqamxqTGsoKCgoKUgoKClLS0tMaCgqaClIKClLimsqiCgoKCpqaiqqKCgpSCgoKEgpSCgpSCpoKUppSmgoKUrLKClIKClKaygoKCgoKUqIKCgoKClJSCpoSmhJSmooKmsoKagoKCgoKmlIKCpoKCprKCgoK4poKmgqaiqIKmyIKUiIKUgpSCgpSCgoKCgoKUgoKCgoKUlIKmgpSChIKCpriCgpaCgqaCpoKCvrKClIKClIqCpqKCgoKUgpSkpKamwoKUpIKCgoKCgpSUgpSUpIKCgoKmgoKClIKUgpSkpIKUgqSkpKSCgpSIgpSClIKUpAACEMKCgoKUtLS0gpS0tLSCioKCgpaCroKUlIKagoKUgoKUgoKCgpSCgoKClJS0tLSChIKCgprKpqKCgpSmgsaCgoKCgoKUtLSCqKSCqpyCgoKCppSm0oKUhIKCgoK0tIKUgoKUgoKCpqKCiIKCgoKCgoKCmoLIlJSUpqKCgqaCgoKCgqamAAMYgoKCooKUgoKUgpSCppSCgoKogpSmgoKmooKClKYAAxSCgoKClIKCgoKCgoKUtLSCgsaUgsaCxpSCgpSmsoKCgpSkpOiClKSkxqKCgpSmsoKClKaigoKUgpSmsoKCgpSmpoKCpqKmgoKCgpSmsoKUgpSCgtyigoKClIKClIKClKiigpSmgqaCpoKmgsaCgoKCgoKCgpSmxqKCgpSmgqaylKSkgoKUxqSkpKSkpKSkpKSkpKSkpKSkgpSkpKSkpKSkpKSkpKSkpKSkpMiigoKUpqKCgoKCgriCprKClIKClKaygoSCgpSCgqamsoKCgoKClKaygoKClLSClMa0xqaigoKCgoKCgoKCgoKClKamsoKIgoKClKamsoKCgobKpqKCgoKClMaigpSmosaygoKCgqSCpIKkgqSCpIKkgqSCpIKkgsaCxoKCgqaUgoLqpqKChpSmooKClIKClIKUpqKClLS0tIKCgpS0tIKClJSCgoKCgpSUxoKClMaygpSClIKWgoKorLKCgoKCgoKCgoKUgoKCgqaCgqaCgoSmgpqCgpSClKaipqKCgoKUlIKUqrKClIKUpKSklKyigpakpKqilKSkpKSkpKSkpqaigpSmsoKElIKChJKClJSCAAUcgoKUgpSClKSkpoIADxiygoKCgoKCgoKChIKCgoKmgpaCgqiCgpSClKiogoKmhIKCgoKUlIKClKaCgpSClIKUgsiCgoKmqIKCgqamgsaigoKClKaipqKmosaipqKmoqaixoKmoqaigpSmtqa2pqKCpoKClKaCgpSCgIK2poKmoqaigoKUgpQAAhDCgoKCgoKCgoKUlIKCgoKCgqaCnIKCgpSCgoKChoKCgoK4goKUlIKUprKCgoKUpMiCgoKCgoKClMaCgsaigoKUpqKUpKSkpKSkxqKUqIKCxsaCgoKUpqKCgpSClKaCgpSmooKCqKKClIKClIKUgriCqKKCgoKUgpqCgoKCpoKUgpSCgoKUgpTasoKCgqa6gqaigoKmrIKClKaigoKUprKChoKUgoKCgoKGgoKUgqaCgpSClIKmpsKCgoKCgoKCgoKCgpSUgsqCgpSUgoKUlKaygqaigoKCgoKCpqKCgoKCpqaygoKCgoKCgoKCgpSUytKCgoKUgoKmgpiCnIKClIKCgoKmprKCgoKUgoLKgoKC2LS0tKaylIKClIKIpoKClJSCgoSCgpSmpIKCgoKCgoKClLikprKCgoKUgoKUgoKUpqKCgpSmooKKgpSmsoKCgoKCgoKUpoKClJSClLimgqaygoKCgpSUpqKmsoKCgpSmprKCgoKUpqKUpIKCgpSC2KSmoqaygoKClKaygoKCiIKCgoKCppSmsoKCgoKCgoKClJSmsoKCgoKChpSmsoKCgoKCgoKUlAAJFIKCgqam4oKCjIKCgpSCgoKUgpSCiIKCggAAHoKClIiagpSogoKUloqMlpiCgoKCgpSCgoKCgpSmgp6CgoKCgoKCgpSUgqiCgpSUlIKUrqKCgoKCgoKCgpSCuKaiqoKmsoKUgoKUgoKmsoKUgoKUgoyCprKClIKClIKCgqaigpSmooKUpqKCgpSmooKClKaigoKClKamsoKClKaygoKCgoKCgpSEgoKCgpSCgoKCgoKCmoKCgoKUgoaChriCgoKClIKClJSClKaCgrimooKUpqKCgoSWpqKClKaigpSmooSCgoKCgoKClKaipoKmoqailKSkgpSkpKSmgtyCgqaigpSkpKaihIKUlIKCgoKUAAIUsoKGgoKCgoKCgoKCgoKChLiCgoKUgoKUlIKCgoKCgoKCgoKCgoKCgoKCgqbCgoKCgoKCgoKUlIKUlKaipoKCgoKUgpS4gqaCpoKmsoKCgoKCppSUlIKUgpSmoqaigoKCmoKCpgACFKKosoKClKaigpSm0oKCgoaCmIKCgpSCgoKCgoKCypSClKaylKSkxqLGooKCgoKCgoKClKaCpoKCgqaygpSCgqaigpSmooKCgoKUlKaigoKCgqaigpS0tLS0tLS0tLSCgqaCgoKmooKUpoKmsoKCgpSCgoKCgqbSgoKCpoKCgoKCgqaigoKCgqaigoKCgoKClJSUgpSUlKaigoKugoKCgqamgoKCpoKCgqaygoKCgqaigoKCpoKCgoKmsoKCgoKmwoKCgoKCgqaygoKCptKCgoKCgoKCgoKCgoKmwoKCgoKCgqaigpSCgoKUlKaigoKCgoKClJTGosaigoKClKamgoKUlIKCgpSmooKCgpSqgsaipqKmoqaixrKClLSGgoKCgoKU1qSCgoKCgpTGooKUgoKUlKaipqLGpISClKaigoKCpqaylKSkpKSkpKqylKSkpKSkpqKClKaygoKUgoKmspSkpKSkpKSmspSkpKaigpSmsoKCmIiCgpQAARCcpqKClKaCprKClIKUgoKClIKCgoKClJSClIKmgoKUlAAMFoKClKaCAAIUooKUgpaCgoKClIKCgoKUlKaygoKCgpSkpJSUhIKmhJSClIKUgoKCpoKUgpSkgpSkpoKCgoKSpoKYgoKUgoKUpoSowoKUgpSCgoKCgoKClJSmgqaCgoKChIKUgoKUgpSClIKUgoKUlJSCgoKCgoKUgqaUpoSCgoKCgqaClKaCgoKCgoKCuKaCgoKCgoKMgqamgoKCgoKCgpa4prKClKaChIKCgoKUrIKEgoKCgpSCvIKCgoKCgoKCgpSCuKampIKUgoKahoKCgoKCgoqCkoKUlJSagoKCmIKUgoKKgoKClIKmgoKCgqaClIaC3IKmgpSkpAAMKoKmsoKCgoIAABSCgpSUjpSCgoKCpoKUpKSUgpSkpJSMgqaClIKUgpSCgoKUgoKGhIaClJyCrriCgoKCgoKatIKClIK0gryCAAcSgpSIgoKUxoKUlMaipqKmoqaipqKmosqCgpSmwoKClIKCgqaCgpSClIKCgpSChpSmlKaCgoKCgoyCpq6CgoKCgoKClIKEgpSCuKyygoKYgoKCgoKClKiCmoKCgoKCgoSClIKClMyCrIKCgqamsoKClIKUgpSClLS0tLSmsoKCgoKCgoKCgoKmgoKm1qKUpKSmgoKClMaCgriyqKKmspSkpKSkpqKGgoKCgoKmlKamqKLGooKCpriigpSmooKUpqKClMaygoKCgpSCgoyChoKClKqUgpSmsoKClJSAgqSCgoKWgpTGgoKmgoKUpqKUpKTGpsaCxqKCgoKUgpSmgoKClIiClLS2poKmsoKUpKSkpKSkpKSEpqKCgpSmwoKClIKClIKCgoKUgoKClLiCgqaygoKCgpSCgpSCuIKmpqKClIKUgpSCgpSClKaizKLKsoKCgoKClIaGpqakprKClKayroKCgpSClKayuIKUgoIAAhCygoKChJSCooKCmJqChpaCgoSUmIKCmJSClKaCpqKCgpSm4oKUhoKYggABEIKCloKCgoKCgpSagoKCgoKCgoKElqaClIKUlKbigoKUgoKCgpSCgoKClJSCgoKCgoKClJSUgoKCgpSClIKmgoKUlJSkpLaCgoaCgoKClJSmgoK4goKYgoKUgoKCgpSmlIKCgoKUpoiMlJSClIKUgoKCgoKChqaCgoKCgoKClIKClIKUgoKUgpS0grS0tLTKpoKUgpSCgoKCgpSCgpSUpoKUpqKCgpSmgoKClOakgoKCgpSUgoKUgoKUpqKCxqKUpKSkxqKCuKKmwoSUgoKChIK4hISCgpSCgoKCgoKCppSEgoKCgoK4poKCgoKClKamgqaigoKCpqaCgpSClKayqLKClIKUyIKCgoKCgqbYooKUprissoKUpsKCgpSCgqaCgoKUprKCgoKUlKbCgoKUgpSClI6CgpSCgoKCgoK4lIKUgpSmwpSCgpSkgoKCgoKCgpSUpoKUlKSkpIKCgqakgoKCgoKmgpSkgoKUpIaUgoKClKSClIKCgqSogoKCgoKCgoKmlKSmooKUgoKUqqKClIKCgrimooKUgqaCpqKCmKaCprKClIKUAAQYgqaCxsiClIKCgpSCgqaCpoKUxoKClMaigpSCgoKUgpSEgoKmgoKUprKCgoKCgoKClLS0goKClJSUpoKCgoKChJSCgpSChJSCgoKCuMaigqaipoKClMaygoKCgoKmlKyigoKUlKaCgoKCgoKUgoKCgpSCgpSmpqKClKaiprKCgpS0tIKUgoLYtIKUxrSkgpS4opSkpKyygoKUgoKEgpSmtoKGgoKWiIKCAAIQgoKCrIKCqqamgoaClJSmqIKGgpSUprKCgoKEgoKCgpSCgoKCpqiipoKClKaigpSOgoKCpqaigoKCgoKCpqaigoKClJSClKaygpSCgpSCgoKGgqaCgpSElIKCgpSUgpSCprKCgoiagoKCgpSClLimgqaygpSCgpSCgoKCgoKmooKigoKClJSmsoKUhpSUtLS0tLS0tISUgpSKiJSCgoKCgoKClIqCgq6CgoisgoKUgqaEgpqEhL6GgoKClJSCpoKmgoKCgpSUgpSCgpSCgpSCgoKUlIKClIKUgqaCpPiCgoKCgpSCpriigoSCloKUgoKCuKaiyKKClIKCuLKClIKClJSClIKCAAEYgoKWgoKUgoKUgoKCgpSUgoKUgoKClJSCgqamgtiCgoKUgoKmxrKCgoKCyrKCmIKCgoKUhIKCgoKUlIaEgqiCgoKCgoKClMqCgriygoKClIKWgoKUgoLcsoKUgoKUgoKUgoKCgoKUgoKCgoKClIKC3KaCgoKmpoKUpIKUpKSkpKSkpKSkpKSkpKSkpKSkpNiygoKCgoKUlKaygpSCgqaWlIKCgtiCgpSCgoKUtIKUtLSCgoKU2KKCgpSmooKCuKKClJSkpKSkpKSmgoKCgpSClIKIpoKcgrimooKUgoKIhgAFEqKCgoKCgpqChILcooKCgoSCyrKCpoKClIKIgoqCgoKCgqaC/KKUpoKUqKbKgoK4gqwABRCClIKClIKCgpaClISClIKClIKClJSCgoKCgpSmgoKClIKUlIKClIKCgpSUgoKmoqaigpTGooKCgpSClKqCgpSkpMyigoKClJSCgoKCppTGosaClKSkyKKClKaCgoKUlKaigpSmooKUpoKmgqailKSkrKSmooKCgoLKpqKGgoKUuIKUgoKCgoKCgpSCtIKClLSCgoK0tpSClIKUgoKCloKmgoKCgpSmopSkpKaylKSktKSkpOiClKSkgqSCpMailKaCgoKUgoKkpqKCgoKClAACEKKCgoKCgoKUpqiCgrqCgoKUgoKUAAIosoSYgoKUgpSkpKSkpKSClKSmpKSCgoKUgqSkpKSkpKSkpKSkAAIcooKCgoKClIKmpqKCgpSUpKSCyKiygoKUgoKCgoKUgoKCgpS0tLSClIKCgoKUlKaCpqKCgoKGgoKUlIKUgpSmgoKCgoKCgoKClJSCgpSClJSmgoKUlIKCgpSUlIKUgpSmspSkpKSmooKClIKCgoKmgpSCgoKUlIKClKaigoKmpoKCgoKCgoKCgpiCgpSWhIKClJampoKCgoKUpoaCgpiCgoKCgpiCmJSCgpSmroKCgoKmhIKUprKCgoKCgoKCgpiCgoKCgoKCgpSUpqaCgoKCgqaogoKEgpSmooKUpKS6goKUlIKUgoKUpoKCgpSmsoKAgqSYgoKCAAUSgoKUpILGprKCgoKUgoSUioKCgpSCgpSmxoLGgoKCgoKCpqaCgqaChKaEgpaCgpbqprKClIKCgoiClIKCgoKCgoLKgoSSgoKUuKyihJSmgoKUgoaCmIKClIKCmIKWgoKUgoKUmIKCgqa6ooKClKaigpSmsqiylqSCgoKUgoKCgpSkpqSClKSCgoSCgoKEgoKUpoKCgoKCgpSCpqaUyILGosaigoKCpqKCgpKCgoKUpoKUqLKCgoKCgpSUpqKClKaygoKCgpiCgrSClIKUgoaCgoKClIKUgoKCgrSCmIKCgoKCgpSCgoKClNiClKYAA6IBgoKCgpiCgrSCAAEagoKUlIKClJqClLS0tIK0gp6CgoKUmoKClJyCgpSUgsaClKaipqKmoqaipqKmoqqyuoKClIKCgoKCgqaigoKCgpSUgpS0tLS0tLSCpqKmoqaigoKqooKCgqiigqaCpqKCqIKCpqKmooKClKaygpSkgoKUpKaCpoKSgoKCgoKClIKCgpSCgoKUlIKClJKClIKUgpSUgpSClIKClLiygoKUhIKUgoKUpqKCmIKYpqKCgpSmsoKCgqaClIKClKaigoKUpqiClIKUqoKCgpSUgoKCgpSUlKSktgAMEoKmgoKUpoKCgoKCgoKUpqaUgoKmxsKCgpSCgoKCgpSClIKUlIaClIKmooKChoKCpqyilqSkpKqigoiChoKCpoqCgoKYyqyigpSCmIKYpqKmgqaCgoKmooKCpqqilKSkpKSCpKSkpoKmgqSCpKaCpqKClIKClIKUqqKmgqaCgoKmooKCpqaCpoKmsoKCgqaClIKUpIKCgpSUgpSkgpSkgoKClJSClKSClKSCgoKUlIKUpIKUpIKCgpSUgpSkgoKUlIaUlIKUpIKUpKSkpKSkgpTGgpTGpIKClKSkpraGgoKCgoKCpsaCxrKCmIKmggACELKCgpSkpIKClMimooSCloiSgoKUlLiCgpSmooKClKaClKSkAAISooKClJSClAACFoKmooKClKiCpsKCloKWgoCCpoKCgpSUgpKCgpaCgpSCgoIAAESClJiCgoKClIIAASKCgoKClJSUgqaigoKUpqSCloKGgpSUpgAAEIKCgqampIKGlKiylKSkpoKElIKmgqaigoKUqKKmooKUqqKClKayggABEoKClKaiiKqigpSmsoKUpqqEgoKmpoKCgoKClKamsoKCgqamooKUgoKCgqamigACELKCgpSUgpSEpoSCgpSCgqaCgoKCgoKUgoKEnIKWgoKCgoLupIKUpIKCgoKmgpSkpKaCgpSsiJSClKaCgoKUgqaClKSkgpSkgoKUlKSClKSClKSCgoKClJSkpIKCgpSUpNiigoKCgoKCgsqmsoKUhLqClISCgpSmgoKCgoK4gqaCgoaWgqamgoKClIK4goSUgoKCgpSCgoKUgoKChIK4goKCgoKClIKCgsyUgpSUgoKCgpSClKaCAAYQprKCgoKCgoKCuKaygqaEpoKCgqaCgoLepoKmhoKClKaEgqaCgoKUpoKCgpSmhIKClKaCgoKUpoKCgpSmgoKCgoKUuIKmgqamgoKCgoKClIKUgpSmgoKCgoKCppTKsoKUpqKm0oKCgoKUgoK4goKCkoKCgoKmlKaigpSmsoKmgqaCgoKUlKSCxqSkgpSmgoKmgqaigqamgg==", "1161-1163:1;1168-1170:1;1175-1177:1;1181-1183:1;1187-1191:1;1188-1190:1.1;1256-1258:1;1259-1261:2;1279-1281:1;1296-1298:2;1466-1468:1;1804-1812:1;1841-1856:1;1883-1885:1;1918-1920:2;1935-1937:1;2052-2109:1;2900-2902:1;2934-2943:1;3053-3053:1;3191-3217:1;3333-3335:1;3347-3347:2;3508-3519:1;3520-3561:2;3562-3571:3;3572-3626:4;3579-3586:4.1;3915-3923:1;3936-3966:1;4303-4305:1;4368-4370:1;4379-4381:2;4491-4493:1;4680-4682:1;4683-4685:2;4721-4721:3;4729-4729:4;4741-4741:1;4743-4743:2;4930-4932:1;4971-4973:1;5277-5277:1;5527-5529:1;5876-5878:1;5884-5886:2;6026-6026:1;6084-6086:2;6321-6323:1;6459-6461:1;6512-6517:2;6587-6587:1;6588-6588:2;6589-6589:3;6632-6634:1;6934-6936:1;7191-7203:1;7809-7811:1;7812-7814:2;7818-7820:3;7879-7881:1;8033-8052:1;8060-8068:2;8132-8140:1;8196-8198:1;8402-8404:1;8788-8790:1;8959-8961:1;9578-9578:1;9647-9647:2;9683-9683:1;9761-9761:1;9769-9780:2;9781-9781:3;9783-9788:4;9853-9855:1;9868-9870:1;10488-10488:1;10723-10725:1;10757-10759:1;10852-10863:1;10853-10855:1.1;10856-10862:1.2;10865-10908:2;10869-10899:2.1;11168-11170:1;11255-11255:1;11423-11425:1;11531-11534:1;11623-11625:1;11701-11701:1;11796-11798:1;11880-11880:1;11887-11889:2;12087-12087:1;12115-12120:1;12126-12132:1;12136-12143:1;12147-12147:1;12165-12170:1;12638-12640:1;12651-12658:2;12673-12675:3;12723-12728:4;12884-12884:1;13303-13305:1;13312-13314:1;13384-13408:1;13537-13542:2;13604-13606:1;13613-13615:2;13690-13692:3;13716-13716:1;13717-13719:2;13720-13720:3;13742-13744:1;13748-13750:2;13783-13783:1;13869-13869:1;13871-13871:2;13925-13938:1;14462-14464:1;14821-14827:1;15148-15150:1;15680-15683:1;15716-15716:2;15839-15854:1;16456-16500:1;17244-17246:1;17445-17449:1;17446-17448:1.1;17773-17797:1;17959-17961:1;18073-18078:1;18080-18082:2;18111-18111:1;18116-18118:2;18289-18291:1;18367-18367:1;18427-18427:1;18476-18484:1;18699-18704:1;18739-18741:1;19471-19473:1;19600-19605:1;19610-19612:1;19659-19659:1;19751-19756:1;19769-19771:1;19772-19774:2;19808-19811:1;19813-19815:2;19875-19884:1;19908-19908:1;19976-19981:1;20172-20172:1;20501-20501:1;20586-20619:1;20646-20666:1;20841-20844:1;20873-20879:1;21025-21027:1;21233-21287:1;21288-21296:2;21293-21295:2.1;21377-21382:1;21386-21388:2;21407-21409:1;21410-21412:2;21455-21457:1;21459-21464:2;21486-21488:3;21491-21493:4;21641-21645:1;21659-21661:1;22132-22132:1;22292-22294:1;22365-22370:1;22515-22515:1;22550-22558:1;22677-22679:1;22681-22685:2;22682-22684:2.1;22748-22784:1;22771-22771:1.1;22848-22850:1;22893-22909:1;23064-23066:1;23692-23697:1;23699-23701:2;23929-23929:1;24127-24136:1;24667-24667:1;24771-24773:1;24858-24860:1;25004-25006:1;25124-25124:1;25165-25167:1;25244-25253:1;25913-25915:1;26098-26098:1;26152-26178:1;26252-26254:1;26301-26301:1;26334-26336:1;26349-26354:2;26365-26367:3;26536-26538:1;26706-26706:1;26962-26964:1;26990-26990:1;27080-27080:1;27082-27082:2;27179-27181:1;27268-27276:1;27299-27301:2;27451-27453:1;27527-27529:2;27621-27628:1;27641-27641:1;27770-27779:1;27970-27970:1;28022-28024:1;28355-28357:1;28367-28369:1;28383-28385:1;28440-28440:1;28463-28468:1;28544-28544:1;28745-28756:1;29495-29495:1;29534-29536:1;29541-29543:2;29553-29575:3;29583-29583:4;29584-29584:5;29614-29614:1;29649-29649:1;29650-29650:2;29697-29699:1;30018-30020:1;30067-30069:1;30072-30074:2;30116-30118:1;30337-30343:1;30364-30406:1;30944-30992:1;31019-31021:1;31083-31088:1;31150-31162:1;31163-31165:2;31176-31178:1;31228-31230:1;31248-31250:1;31253-31255:2;31334-31336:1;31539-31541:1;31556-31561:1;31578-31586:1;31680-31680:1;31867-31869:1;31939-31944:1;32452-32464:1;32475-32475:1;32524-32544:1;32556-32558:1", "", "1201=getGlobalTypesResolver/1/1/9;1216=getGlobalTypesResolver/1/1/9;1520=checkAndReportErrorForMissingPrefix/1/1/1,checkAndReportErrorForExtendingInterface/1/1/2,checkAndReportErrorForUsingTypeAsNamespace/1/1/3,checkAndReportErrorForExportingPrimitiveType/1/1/4,checkAndReportErrorForUsingNamespaceAsTypeOrValue/1/1/5,checkAndReportErrorForUsingTypeAsValue/1/1/6,checkAndReportErrorForUsingValueAsType/1/1/7;1536=IfElse/1/2/1;1892=IsDecorator/1/1/1,IsMethodDeclaration/1/1/2,IsAccessor/1/1/3,IsPropertyDeclaration/1/1/4,IsParameterDeclaration/1/1/5;2042=IsPropertyDeclaration/1/1/1,IsParameterPropertyDeclaration/1/1/2;2960=Members/1/1/1,Some/1/2/1,Parameters/1/1/2,Some/2/2/2;3687=IsPrivateIdentifier/1/2/1,IsPrivateIdentifier/2/2/1,Text/1/4/1,Text/2/4/1,IsComputedPropertyName/1/2/2,IsComputedPropertyName/2/2/2,checkComputedPropertyName/1/2/2,checkComputedPropertyName/2/2/2,isTypeIdenticalTo/1/1/2,IsPropertyNameLiteral/1/2/3,IsPropertyNameLiteral/2/2/3,Text/3/4/3,Text/4/4/3;4000=isSymbolUsedInConditionBody/1/1/1;4473=HasSyntacticModifier/1/1/2;4561=getSymbolOfDeclaration/1/1/4,symbolToString/1/1/4;4749=IsBinaryExpression/1/1/1,arePropertiesAbstractOrInterface/1/1/2;4833=IsPropertyDeclaration/1/1/1,Initializer/1/1/1;4938=IfElse/1/3/1,IfElse/2/3/2;5558=GetSourceFileOfNode/1/1/2,GetEmitModuleFormatOfFile/1/1/2;5562=getTypeFromImportAttributes/1/1/2,isOnlyImportableAsDefault/1/1/2,hasTypeJsonImportAttribute/1/1/3;5618=GetSourceFileOfNode/1/1/1,GetEmitModuleFormatOfFile/1/1/1;6266=IsModuleBlock/1/1/1,IsModuleDeclaration/1/1/1,IsSourceFile/1/1/1;6606=Invoke/2/4/1,isReferenceToType/2/4/1,Invoke/3/4/2,isReferenceToType/3/4/2,Invoke/4/4/3,isReferenceToType/4/4/3;6619=Invoke/1/2/1,OrElse/1/2/1,Invoke/2/2/2,OrElse/2/2/2;6668=getIterationTypeUnion/1/3/1,getIterationTypeUnion/2/3/2,getIterationTypeUnion/3/3/3;6692=getAwaitedTypeEx/1/2/1,OrElse/1/2/1,getAwaitedTypeEx/2/2/2,OrElse/2/2/2;6744=Invoke/2/4/1,isReferenceToType/2/4/1,Invoke/3/4/2,isReferenceToType/3/4/2,Invoke/4/4/3,isReferenceToType/4/4/3;6755=getIterationTypesOfMethod/1/3/1,getIterationTypesOfMethod/2/3/2,getIterationTypesOfMethod/3/3/3;6909=IsForOfStatement/1/1/1,Expression/1/1/2,Invoke/1/2/3,Invoke/2/2/4,createTypeFromGenericGlobalType/1/1/4,isTypeAssignableTo/1/1/4;6986=IfElse/2/3/1,IfElse/3/3/2;7096=Name/2/2/1,Text/2/2/1;7497=IsVariableDeclaration/1/1/1,IsForInOrOfStatement/1/1/1,getCombinedNodeFlagsCached/1/1/1,IsBindingElement/1/1/2,IsObjectBindingPattern/1/1/2,PropertyName/1/1/2,isIdentifierThatStartsWithUnderscore/1/1/3;7759=IfElse/1/2/1;7765=IfElse/1/2/1;7872=IsElementAccessExpression/1/1/1,Expression/2/2/1,IsIdentifier/1/1/2,IsQualifiedName/1/1/2,isInRightSideOfImportOrExportAssignment/1/1/2,IsTypeQueryNode/1/1/3,AsTypeQueryNode/1/1/3,IsExportSpecifier/1/1/4;8277=IsPropertyDeclaration/1/1/2,IsClassStaticBlockDeclaration/1/1/2;8521=IfElse/1/2/1,isGenericObjectType/1/1/2,isThisTypeParameter/1/1/2,IfElse/2/2/2;8982=Symbol/1/1/2,getDeclaredTypeOfSymbol/1/1/2;9678=IsInJSFile/1/1/1;10205=IfElse/1/2/1;10254=getReducedType/1/1/2,isTypeAssignableTo/1/1/2;10381=ClassOrConstructorParameterIsDecorated/1/1/2;10860=GetUseDefineForClassFields/1/1/2;10871=needCollisionCheckForIdentifier/1/2/1,needCollisionCheckForIdentifier/2/2/1;11472=FindAncestor/1/1/1;11546=Initializer/1/1/1,AsVariableDeclaration/1/1/1,isMutableLocalVariableDeclaration/1/1/2,isSymbolAssignedDefinitely/1/1/2;11547=isSameScopedBindingElement/1/1/5,IsInTypeQuery/1/1/6,isInAmbientOrTypeNode/1/1/6,IsNonNullExpression/1/1/7,IsVariableDeclaration/1/1/8,AsVariableDeclaration/1/1/8;11677=GetUseDefineForClassFields/1/1/2;11823=isPropertyWithoutInitializer/1/1/1,IsStatic/1/1/1;12140=isOptionalPropertyDeclaration/1/1/1,IsAccessExpression/1/2/2,Expression/1/1/2,IsAccessExpression/2/2/2,isBlockScopedNameDeclaredBeforeUse/1/1/3,IsMethodDeclaration/1/1/4,getCombinedModifierFlagsCached/1/1/4,GetUseDefineForClassFields/1/1/5,isPropertyDeclaredInAncestorClass/1/1/5;12224=isThisInitializedObjectBindingExpression/1/1/1,IsObjectBindingPattern/1/1/2,isThisInitializedDeclaration/1/1/2;12851=IsInJSFile/1/1/2;13454=IsCallExpression/1/1/1,Expression/1/1/1,IsTaggedTemplateExpression/1/1/2,IsAccessExpression/1/1/2,IsIdentifier/1/1/2,Text/2/2/2;13484=GetUseDefineForClassFields/1/1/2;13888=Types/1/1/1,Every/1/1/1;13968=Some/2/2/1;14002=IsPropertyAssignment/1/1/1,Initializer/2/2/1,hasDefaultValue/1/1/1,IsShorthandPropertyAssignment/1/1/2,AsShorthandPropertyAssignment/1/1/2,IsBinaryExpression/1/1/3,AsBinaryExpression/1/1/3;14010=isInlineImportAttributes/1/1/1,isValidConstAssertionArgument/1/1/2,getContextualType/1/1/2,isConstTypeVariable/1/1/2,IsParenthesizedExpression/1/1/3,IsArrayLiteralExpression/1/1/3,IsSpreadElement/1/1/3,isConstContext/1/2/3,IsPropertyAssignment/1/1/4,IsShorthandPropertyAssignment/1/1/4,IsTemplateSpan/1/1/4,isConstContext/2/2/4;14164=IsNodeDescendantOf/1/1/1,getControlFlowContainer/1/2/2,getControlFlowContainer/2/2/2;14242=getDeclarationModifierFlagsFromSymbol/1/1/1,getDeclarationNodeFlagsFromSymbol/1/1/2,Some/1/1/5;14278=getCannotFindNameDiagnosticForName/1/1/1,IsWriteOnlyAccess/1/1/1;15685=getSuggestedImportSource/1/1/3;15712=FileName/1/1/1,GetCurrentDirectory/1/2/1,GetNormalizedAbsolutePath/1/1/1,UseCaseSensitiveFileNames/1/1/4,GetCurrentDirectory/2/2/5;15724=GetAnyExtensionFromPath/1/1/3;15733=UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;15766=FindAncestor/2/2/1;16010=NewDiagnosticChainForNode/1/1/2;16125=AsSourceFile/1/1/3,GetImpliedNodeFormatForEmit/1/1/3;16402=IsInJSFile/1/1/1,GetModuleResolutionKind/1/1/2,IsVariableDeclaration/1/1/3,Initializer/1/2/4,Initializer/2/2/5,isCommonJSRequire/1/1/5;16789=ModuleSpecifier/1/1/1,GetTextOfNode/1/1/1;17002=IsJsxTagName/1/1/1,IsJsxAttribute/1/1/1,IsJsxNamespacedName/1/1/1;17242=getCombinedModifierFlagsCached/1/1/1;17739=AsTypeParameterDeclaration/1/2/1,AsTypeParameterDeclaration/2/2/2,getTypeFromTypeNode/1/1/2,IfElse/1/1/1;18727=filterType/1/1/1;18833=AsElementAccessExpression/1/1/1,IsStringOrNumericLiteralLike/1/1/1;18919=Parameters/1/1/1,Contains/1/1/1,IsTypeNodeKind/1/1/2,Invoke/1/1/2;19763=isTypeAssignableTo/2/2/1,getStringLiteralValue/1/1/2,isNumericLiteralName/1/1/2;20297=Types/1/1/1,Every/1/1/1;20489=isSymbolWithNumericName/1/1/1,isSymbolWithSymbolName/2/2/2;20560=isPatternLiteralType/1/1/1,isGenericType/1/1/2,Types/1/1/2,Some/1/1/2;20592=Pos/1/1/1,End/1/1/1;20620=IsInJSFile/1/1/1,IsFunctionExpression/1/1/2,IsArrowFunction/1/1/2,IsMethodOrAccessor/1/1/2,IsFunctionDeclaration/1/1/2,IsConstructorDeclaration/1/1/2,Parameters/1/1/3,Every/1/1/3,getContextualType/1/1/4;20640=Initializer/1/1/1,isRestParameter/1/1/2,Arguments/1/1/3;21076=Symbol/2/2/1,IsFunctionExpressionOrArrowFunction/1/1/1,Expression/3/3/1,isConstantReference/1/1/1;21461=instantiateSymbol/1/1/1,instantiateSymbols/1/1/1;21667=Parameters/1/1/1,Every/1/2/1,TypeParameters/1/1/2,Every/2/2/2;22105=IfElse/2/3/1,IfElse/3/3/1;22389=Size/1/2/1,Size/2/2/3,hasCommonDeclaration/1/1/3;22555=getMappedTypeModifiers/1/1/1,AsMappedType/1/1/1;22970=getSymbolFlagsEx/1/1/1;23061=isNonGenericTopLevelType/1/2/1,AsTypeReference/1/1/1,getTypeArguments/1/1/1,Some/1/2/1,isNonGenericTopLevelType/2/2/4,Types/1/1/4,Some/2/2/4;23316=TypeArguments/1/1/1,Some/2/2/1;23323=TypeParameters/1/1/1,Some/1/2/1,Parameters/1/1/2,Some/2/2/2,Invoke/1/1/3;24377=AsConditionalTypeNode/3/4/1,mayResolveTypeAlias/3/4/1,AsConditionalTypeNode/4/4/1,mayResolveTypeAlias/4/4/1;25227=isBlockScopedNameDeclaredBeforeUse/1/1/1;25497=AsConditionalTypeNode/1/2/1,AsConditionalTypeNode/2/2/3,getTypeFromTypeNode/1/1/3,getInferTypeParameters/1/1/5;25931=getTupleElementFlags/1/1/1,IsNamedTupleMember/1/1/2,IsParameterDeclaration/1/1/2,IfElse/1/1/2;26138=isReducibleIntersection/1/1/1;26902=maybeTypeOfKind/3/8/1,maybeTypeOfKind/4/8/1,maybeTypeOfKind/5/8/2,maybeTypeOfKind/6/8/2,maybeTypeOfKind/7/8/3,maybeTypeOfKind/8/8/3,isLiteralOfContextualType/1/1/4;26904=maybeTypeOfKind/2/5/1,maybeTypeOfKind/3/5/2,maybeTypeOfKind/4/5/3,maybeTypeOfKind/5/5/4;27090=IfElse/2/2/1;27204=isFreshLiteralType/1/1/5,AsLiteralType/1/1/5,containsType/1/1/5;27344=getTargetType/2/2/1,isTypeDerivedFrom/1/1/2;27493=getIntersectionTypeEx/1/3/1,getIntersectionTypeEx/2/3/2;27613=IsEmptyAnonymousObjectType/1/1/5;27795=getMembersOfSymbol/1/1/1;27845=Target/1/1/1,isPatternLiteralPlaceholderType/1/1/1;28066=IfElse/2/2/1;28164=Types/1/1/1,Some/1/1/1;28172=Expression/1/2/1,IsBinaryExpression/1/1/1,Expression/2/2/1,AsBinaryExpression/1/1/1,IsAccessor/1/1/2;28189=isTypeAssignableToKind/1/1/1,isTypeAssignableTo/1/1/2;28216=isGenericTupleType/1/1/1,isGenericMappedType/1/1/2,getNameTypeFromMappedType/1/1/2,isGenericReducibleType/1/1/3,maybeTypeOfKind/1/1/4,Types/1/1/4,Some/1/1/4;28234=IfElse/1/1/1;28477=getParentOfSymbol/1/1/5;28755=Types/1/1/1,Every/1/1/1;28768=isGenericReducibleType/1/1/1;28801=isNoInferType/1/1/1,AsSubstitutionType/1/1/1,isNoInferTargetType/1/1/1,IsEmptyAnonymousObjectType/1/1/2,isPatternLiteralType/1/1/3;29071=isTypeAssignableTo/2/10/1,isTypeAssignableTo/3/10/2,isTypeAssignableTo/4/10/3,isTypeAssignableTo/5/10/4,isTypeAssignableTo/6/10/5,isTypeAssignableTo/7/10/6,isTypeAssignableTo/8/10/7,isTypeAssignableTo/9/10/8,isTypeAssignableTo/10/10/9;29701=AsVariableDeclarationList/1/1/1,IsNodeDescendantOf/1/1/3;29717=AsHeritageClause/1/1/1;29723=IsShorthandPropertyAssignment/1/1/1,shouldMarkIdentifierAliasReferenced/1/1/2;29827=AsImportEqualsDeclaration/1/1/1;30290=ShouldPreserveConstEnums/1/1/1,isExportOrExportExpression/1/1/1,getExportSymbolOfValueSymbolIfExported/1/1/2,isConstEnumOrConstEnumOnlyModule/1/1/2;30322=getSymbolFlagsEx/1/1/1,isConstEnumOrConstEnumOnlyModule/1/1/1;30342=symbolIsValue/1/1/1,resolveAlias/1/1/2,isConstEnumOrConstEnumOnlyModule/1/1/2,getTypeOnlyAliasDeclaration/1/1/3;30784=Map/2/2/1;31896=IfElse/1/3/2,IfElse/2/3/3;32124=getFalseTypeFromConditionalType/1/1/1,getActualTypeVariable/1/2/1,AsConditionalType/1/2/1,getActualTypeVariable/2/2/1,AsConditionalType/2/2/2,isTypeAssignableTo/1/1/2;32992=IsCallExpression/1/1/1,IsNewExpression/1/1/1,Expression/1/2/1,IsElementAccessExpression/1/1/2,Expression/2/2/2,someType/1/1/2,AsElementAccessExpression/1/1/2,getTypeOfExpression/1/1/2,isGenericIndexType/1/1/2;33017=IsJsxOpeningElement/1/1/1,IsJsxSelfClosingElement/1/1/1,TagName/1/1/1;33162=GetExternalModuleName/1/1/1,IsVariableDeclarationInitializedToRequire/1/1/2,IsImportCall/1/1/2,IsLiteralTypeNode/1/1/3,IsLiteralImportTypeNode/1/1/3,AsImportTypeNode/1/1/3;33357=Expression/1/1/1,checkExpressionCached/1/1/1,Name/1/1/2,getLiteralTypeFromPropertyName/1/1/2;33431=AsInterfaceType/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/emitresolver.go", "emitresolver.cs", "ABMiABg4goKSkoLG0oKC1tKCgtbSgoLW1oKUgpSCgoKClNbkgpSClIKClNbUgoLWhIKUgqaCgoKUpqainKQAAhSChqyClISElgACEISWAAIkrqioqKqCgpSmyNKCgoKUiKLEooSUtsailILGgsa0qqKCgoKCpoKCgoKUlIKClISCgoK42KSMlI6WxtKCgtakgqaClJSCpoKCgriC7oKCpgAIDrSylIKCgoKClKamgoKUhoKGgpSGgpSGgpSCjIKUgoKClIKClIKUgpSCurjc5IKUgoKUgoKCjoKegoKClIKCuNbUgpSChJSCgpSClIKCgoKCgoKCgoLc1vKGlIKClIKElIKUgoTW0oKUgoLWgoKWprSClJSClIKCpKTYgqakgoKUiKairKKClIKUpoKm1IKUgoKCgoKUlNaEgpaCgoKmptKCgtaCporGgsbigoKmgpSCgoKClIKGuNbSgoKmgpTWopSUqoKkgqaClKSCxqaigoKUgoKIpoKCmKrigoKUgpSEpoKU1uKClIKUgoKCgpSClIKClLSk0oKmgoLWgoIACham0oKmgpTW0oKCoqTSgoKCpoKClNbSgqaClNaCptKCpoKU2NKClILW0oKmgpTW0oKmgpTe4oKCpoKCgtbigoKmgoKC1uKCgqaCgoSC1vKCgoKCgqaCgoKCgoaCgoKUgpSClJSkgoKUlIKUhLqkpIKClJTW8oKCpoKCgtbygoKUgoKCgoKCppSCgoKClIKUgoKUgpSEjIKChKaCgoKmgoKm3pSmgoKCgoLegrjW1IKC1tSCgtbWgpSCqIKClIKmgoKCppaCgoKWlIKCgqaCgoKUuoKClKaCgoKUpoKCgoKCgoKCgoKCgoKCgoKCgoKU6IqClIKClKbygoKUggAFHNKCpoKUgoKUgoKUgoKClIKUhqY=", "389-395:1;817-827:1;1095-1100:1", "", "191=Name/2/2/1,Elements/1/1/1;261=AsSourceFile/1/1/1;336=IsPartOfTypeNode/1/1/1,AsTypePredicateNode/1/1/3;339=AsQualifiedName/1/1/2,Expression/1/2/3,Expression/2/2/4;385=Text/1/1/2;397=Text/1/1/2;435=HasSyntacticModifier/1/1/1,isDeclarationVisible/1/1/2;439=HasSyntacticModifier/1/1/1,isDeclarationVisible/1/1/2;443=HasSyntacticModifier/1/1/1,isDeclarationVisible/1/1/2;448=IsVariableDeclaration/1/1/1,IsVariableStatement/1/1/2,HasSyntacticModifier/1/1/3,isDeclarationVisible/1/1/5;476=Values/1/1/2,Collect/1/1/2;570=Name/1/1/1,IsIdentifier/1/2/1,Expression/1/2/2,IsPropertyAccessExpression/2/2/2,Expression/2/2/2,IsIdentifier/2/2/2;660=isOptionalParameter/1/1/1,Initializer/1/1/2,HasSyntacticModifier/1/1/3;768=getSymbolFlags/1/1/1,ShouldPreserveConstEnums/1/1/2,isConstEnumOrConstEnumOnlyModule/1/1/2;814=Elements/1/1/1,Some/1/1/1;849=ShouldPreserveConstEnums/1/1/1,isConstEnumOrConstEnumOnlyModule/1/1/2;866=AsImportEqualsDeclaration/1/2/1,NodeIsMissing/1/1/1,AsImportEqualsDeclaration/2/2/1;1209=String/1/1/2,NewNumericLiteral/1/1/2;1311=NewModifierList/1/1/1,IfElse/1/1/1,Name/1/1/2,QuestionToken/1/1/3,Symbol/1/1/4,getTypeOfSymbol/1/1/4,TypeToTypeNode/1/1/4;1327=AsIndexSignatureDeclaration/1/1/1,ParameterList/1/1/3,Type/1/1/4;1525=getTypeOfSymbol/1/2/2,getTypeOfSymbol/2/2/2,isTypeIdenticalTo/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/exports.go", "exports.cs", "AAsQgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCgpTGgqaCxoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoIAAhaCpoLGgsaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgsaCpqLGgqaCpoKmgqaCpoKmgqaCpoKogqaCpoKmgqaCpoKmgoKClKaCpoKmgqaCpoKqgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpqKCgpSCgpSmgqaCpoLGog==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/flow.go", "flow.cs", "ABwwgsaigpQAFyiCgoKUgqaiuMaigoKUxoKm8oKUgoKCpoKCgoKCgoKCgoqCgpSUgpSmwoaClIKClIKCgoKIgoKCppSClIKCgsaCgoLGtLSCgoKUtIKClLSCgoLGgoK2goKClri0hJSC2KKCgoKCgqbGwoaCgpSCgpSCgpSCgpSUgoKUgpScgoKYgoKmloKWprKClKaiqLKCgoKCgoKClLS0tIKUlIKmptSCgoKClIKUgoK8pqKCgpSCgpSCpqaygoIAARKCgoKClKrEgpSogoKCgoKCgoK4pKSkpKSCtqaigpSCgpimwoKUgpSCgpimwoKCgoKCpoKmgoKCgoK4ptKUpIKCgoKUgpSClIKUgoKCgqaCgpSCgpSClIKUgpSCxqSClIKCgoKCuIKCgtiqgpSkgpSkprKClIKUgoKCgpSClLS0tJSCgoKUgqaCgoKmlpSCgoKCpqim5IKUgoKClIKCmJTGAAwWooKUgoKUprKUpKSkpKSClKSClKSkpoKClIKUpKSkuKKCgoKCgoKClIKUyqqigoKUgoKCgpSCgpSClIKSgriygoKCgpSmpIKWgoKWgoKWgoKClIKWgpa6uoKWpqKCptKCgoKUlIKCgpqCgpSClIKUhoKUprKClIKClIKCprKCgpSCmIKUgqiClIKYgoKUhIKCgoIAAxCCgoKUpKSmgpSkpKSkpoKCmIKClJSCgoKCpqqUpKSkpKaigoKUgoKcpsKCgpSCgpSCgoKUlKaigoaGmIaCgqamgoKClKYAABSCgoKUlISEgpSmwoKCgoKUtLS0goKGgqqCgsam2oKClIKCgoKCgoKUgoKUlKaUgoKClIKCgqaCkqaClJKClIKClJiClKaygoKUhIaCgoKEgpqCgoKUuLKChoKChIKCgqyCgoKCppaCgpS4ooKClKbCgoKCgoKCgpSUgriqsoKCgoKCgoSClIqCgpSCmoKUgqaCiIKCgpSCgpSCuIKCrLKClIKClKbCgpSElIaAggACFIKCqoKCgoKChoKYgoKCgoKIgqaIgpqCqoKClIKmwoKCgoKUlIKCgoKCgqiCgqaUpqa4goKCgoKClILKpsKagoKCgtqCxoKCgoSCloKCgurGooKCps6ygoKCgoKUpqKClMaCgoKCgpSmyqKCgoSIrKKCgoKUpqKClKaigpSmspSkpKaCgoKCpsKUpKiUpIKUpqSkpICCgoLIgoKCgoKC6oKC2KTGvKKCgpSmwqSCgoKUlKSCgoKClIKClKSkgpSCgqSCgpSCgpSCgoKClIKCyoKCgqSmooKUgpSClIKUpqKUpKSmooKClIKClIKCgqqCgoKUgqbGopSkpMaigoKUgpSClKaCgoKUprKUpIKCyIKCgtiCgpSkpqKCgoKmpqKCgoKmpqKUpJSk1qaigoaUpqKCgqaClKaCAAIQooSClKSkpKTIxoLIooKEgoKClISUprKCgoKUhIKElpiCgpSCgpSmooKoqqKCgoKCgoKCgpSCuIKUqIKCgoKCgpSmprKCgoKCgpSClKaygpSmsoKCioKCgoKCgoKClKaCgpSClLS0gpSUgpSsgoKCgoKCgrimooKCgoKmrrKClIKkpKSCgoKCgoKmlILYtqbSgoKUgoKUgoKCgqaCgoKUgoKCgoKClJSmgrjWoqiigoKmprKmgoKCgoKmgoKClKamopSkpKaygpSClIKCgqamqIKCgqamooKCgpS0tLSmsoKUpIKCxqSkpKSkpKayhIKUpqKmooKCpoCCpKaigpSClKaipqKCgpSmoqaigoKUgoKUgpSmoqaiqIKClKzCgpSClIKCgoKUpqKIgoKcgpSmooKWgpaCgqamsoKCgqaCgqamwoKClJSCgoKCgoKWgpSmsoKClJSCgoKCgoKCloKUlKaigoKCgoKmwoKClIKCgoKUgoKUlJS0goKCpoKmtoKCpqSClriCgpS2goKCgqTagoKClIKClqqCgoKCprKCgoKCgpSClJSUtIKUtIKCpqa0goKCptyigqiigqqygoKmsoKClIKCgoLKogACFrKUgoKCgoKCgoKClKaCuKSCgoKCgoKmqqSClKyygoKYtJQ=", "329-331:1;421-423:1;437-439:1;595-597:1;607-609:2;626-628:1;686-699:1;720-722:1;744-747:1;789-791:1;865-867:1;873-875:2;889-950:3;905-913:3.1;915-927:3.2;941-949:3.3;973-975:1;1003-1005:1;1009-1011:2;1051-1051:1;1052-1052:2;1074-1076:1;1078-1080:2;1132-1132:1;1139-1150:2;1147-1149:2.1;1164-1166:1;1173-1175:2;1179-1184:3;1189-1191:1;1215-1220:2;1236-1242:1;1248-1250:2;1910-1923:1;1963-1965:1;1980-1982:1;2416-2418:1;2689-2691:1", "", "327=IsBindingElement/1/1/1,IsBinaryExpression/1/1/1,AsBinaryExpression/1/1/1,isEmptyArrayLiteral/2/2/1;1144=everyType/2/2/1;1630=IsCallExpression/1/1/1,Name/2/3/1,IsIdentifier/1/1/1,Name/3/3/1,IsPushOrUnshiftIdentifier/1/1/1;1631=IsBinaryExpression/1/1/1,AsBinaryExpression/1/2/1,AsBinaryExpression/2/2/2,IsAssignmentTarget/1/1/2,AsElementAccessExpression/1/1/3,getTypeOfExpression/1/1/3,isTypeAssignableToKind/1/1/3;1696=IsBinaryExpression/1/1/1,AsBinaryExpression/2/3/1,AsBinaryExpression/3/3/2,isMatchingReference/2/2/2;1707=IsVariableDeclaration/1/1/1,IsBindingElement/1/1/1,getResolvedSymbol/3/3/1,getExportSymbolOfValueSymbolIfExported/1/1/1,getSymbolOfDeclaration/1/1/1;2031=IsBinaryExpression/1/2/1,AsBinaryExpression/1/4/1,AsBinaryExpression/2/4/1,IsBinaryExpression/2/2/2,AsBinaryExpression/3/4/2,AsBinaryExpression/4/4/2;2058=maybeTypeOfKind/2/2/1;2376=isExpandoPropertyFunctionWithReturnTypeAnnotation/1/1/1;2526=IsPropertyAssignment/1/1/1,isDestructuringAssignmentTarget/2/2/1;2615=IsForOfStatement/1/1/1,Initializer/1/1/1;2841=isFalseExpression/3/4/1,isFalseExpression/4/4/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/grammarchecks.go", "grammarchecks.cs", "ABYmwoKCgoKUprKCgoKUprKCgoKUprKCgoKCgpTGopSkpOiigoKCgpSCgqKEgoKCgqaCgoKCgoKClKaygoKmgoKmgoK4pqKClKaygoKagqaCgoSCgqyCgoKUhJSCggACEIKChJSCgqaEpqaCgoK4pqKClKaigoKUAA8GsoKUgpSClIKClIKCgoKCgoiCgoKCgoKUlIKCgrqCqIKClIKCgoKUppaCgoKmgqaCgpSCpoKCppSClIKCiNqCgoKCgoKCgoKUgrqUgoKCgoKCgoKCgoKCgoKCgoKClIKUtIKCgoKCgoKCgoKCgoKCgpSCtIKCgoKCgoKmtIKChIKClLSClIKCgoKCgoKCgoKCgoKCgpS0goKUlIKCgoKCgoKCgqa0goKCgoKCgoKCgoKCgoKCgoKCgpSCtIKUgoKUgoKClJSUgpSClIKUgpSCpoKmtIKCgoKClIKUgraCgpSUgoKUlIKClIKUgpTqgoKUgpSClIKCgoKCgpSClKaigoKUpqKCgpSmogABKKykgpSUpqikgpSkpAAICqKCgpSmooKClLiimqamooKUprKCgoKUpqKClIKCgoKUgqaCpoKUgoSClIK4prKCgoKClIKCgpSCgoKmgoKCgpSUprimpIKCqqKCprKCpoKCgoKCgoTKgoKoopSCpoKCpoKClIKUgpSClIKClIKGlIKUgpSmhKaigoKCgpSmgqaigpSmooKClIKEpoKCpqaigpSmooKUlIKCgoKCpoKmgoKmlIKUgqaouKaigoKClJSClLSkuLimpIKmgoKUpqKCgpSClIK4pqKmoqailIKClIKCgoSCgqaUgoSmgoKMgpKCgpSmuIKogoKCgqaCgoIABRyCloKCgpSogpSCpoKmtLS0tLaCgoKmgoKUgoKCgoKClKbcpqKCgoKCgpSCgoKCgpSUgqamsoKmgqamooKmprKCgqaCgoKCgoKUtIKCgpTKgpS0yoKCgoKCgpSC3IKCpoKCggABEoKmgoKClJSmgoKCgpSUlIKCgpSUyqaygoKCpoKClIK4goKCpoKUgpSCgqaCgpSCgpSClIK4rqamooKCgpSClIKClIKUgsaCxoLGpIKCgrimooKUgoKUpoKmqIKmooKmgoSCpoKClIKUgqaCuIyCgoKUgoKCpqaigoKCgqaUhpSCpsaExoTIpoKCgpSmlIKClJS4ooKCgpSUgriEpvaygoKClKTIgoKCgoKUlKSk2oKClLS0tKaCAAIUpqKCgqaCgrimooKCpoKCgoK4pqKCgqaCpoKCgpSClIK4gqamtIKChIKClJSCgoKCgoKCgoKCgoKClJSCgpS6goKClIKClMqClLSClIKClJSCyoKCgoKClJSCgoKUgsqEgqamsoKCgpSCgpSmsoKCgoKUtLS0tLS4pqIAARCkpuaigpSUgsaCgoKCpoKU2qaigoKCgpSUpqaigoKUpqKCgpSCgpSClIKUgoKUhJSClIKClISUgriCpoKCgoKUpKTaprKCgpSCgrSCgrSCgrS2goKCgoKmuMaiqKKCpoKCpsaigqaClKqmAAIagqamooKCgrimoqayhIKCggACEIKEgoIABBCmopqCmIIAAhCCgqamsoKEgoK4prKUgpSCxoKUgpSCxqaigoKCgpSClKaigoKCgoKClIKmgrimsoKmgoKUgqaCgqaCgoSUgoK4gqqCgpSmooKClIKClIKCgoK4gpSClIKClIKmgoKm", "78-87:1;730-733:1;826-828:1;1032-1038:1", "", "343=IsFunctionTypeNode/1/1/1,IsConstructorTypeNode/1/1/1,IsCallSignatureDeclaration/1/1/2,IsConstructSignatureDeclaration/1/1/2,IsMethodSignatureDeclaration/1/1/3;818=checkGrammarParameterList/1/1/1,checkGrammarArrowFunction/1/1/1,IsFunctionLikeDeclaration/1/1/2,checkGrammarForUseStrictSimpleParameterList/1/1/2;850=grammarErrorOnNode/1/1/1;885=isGenericType/1/1/2;2109=AsPrefixUnaryExpression/1/2/1,AsPrefixUnaryExpression/2/2/1;2139=IsEntityNameExpression/1/1/1,checkExpressionCached/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/inference.go", "inference.cs", "ACJAgoKClIKmooLupsKCgoKCgoKCgqbSgpSIgoKCgpSChoKCgoKClpSGgpSUgoKClJiIgoKUgoyClIKKgoKClJaCgpSCpoKClJSGgpSCAAAigpSCgoKUgoKCgpSGgoKClIKCpoKCpoKWgoKCgoaCgoLKlrS0grSCtILGhLS0toLGtIKClIKIgoKUlILqgoKClMqigoKCpqKCgoKmooKCprKClAADItKCgoKUgpSCgoaCgoKClIKUgpSUgoKCgqaCgoKCgoKClIK4joKCgoLKgqSCpMiCgoKClK6igoKUgoKmgqamgoKClMbCgoKCgoKUlIKKgoKClIKCgoKClIK4iIKClJyCgoKCpoKCvoKClAADEIKCgvyCgoKClIKClJTGooKCgqbCgoKCgpSCuLKCjoKCgoKUmIKAgoCCgoKWgoSCloKUgpSkpKSkpKSkpKSkpKSkpKSkpKSkpJSkpNakpKSkpKS2goKUgoL8yoaCgoKCgrjChIKUgpSCgoKogpSCgoKCgoaCgpSUgoKCgoKogpSEgoKCgpSmgoKEgoSClIaCgoKCgoK4hoKCgoKCgoKCgpTKhoKCgoSCgrqClJSCgqaCgoKmgoKCgoLKgoKChoKCgoLKwoKCgoKWgoCSlIC2goKCgoKCgpSCgpSCgoKCpoKUgriigoKCgoKmgoK4xIKClIKCgoKCgoKClKaCgqaCuIKCgsrSgoKClJSKgoKCiKaUhoqCgpiCgoKUlIKUrtKCgpSCgqa2gpiCgoKUlIKGgpSCgoKClKaYgoKCgq6iqtKCgpSCgoKClIKUgoKUgoKCgqbCgoKCgqaygoKCgoKCgpSCgoiCgoKmgoKCgoKCgoiCgoKUgpSUpqKCgoKUrrKCgpSCgpSClIKSgpSmqKaGgpTIsoKCyLKClIKCpqbCgpSqsqqygoKClIKCgoKClJSCgpSmooKUpqKClKaigoKUpoLcgoKmogACIKKCgoKUlIKmprKCgoKUgoKCgoKUgoKUjsimgoKUgpSEnoKGuJSCgpSCgoKCgoKChqimhJSUlKaigoKUpqKClKqigoKClJSmpIqCgoKUtLS4goKUlKaigpSmgoKCgoKCpqaigoKClJSmoq6CgoKUpqKUpKTGooKCgoK4xqKCloKCnoKClJaClKaIgpKUpoKCgoKmqIKCgoKmpoKCgoKUpqaCgoKCgoKUgrimoqaCxoKmogALGIKCgsqipqKmgoKCgrimgoKCpsaCgoI=", "401-401:1;404-404:2;538-538:1;575-575:1;603-669:2;862-862:1;865-865:2;988-993:1;1030-1032:1;1033-1033:2;1038-1043:3;1061-1063:1;1171-1171:1;1343-1343:1;1344-1347:2;1346-1346:2.1;1388-1390:3;1475-1475:1;1495-1495:1;1538-1540:1;1539-1539:1.1;1563-1563:1", "", "1045=isTupleType/1/1/2,getElementTypes/1/1/2,Some/2/2/2;1178=getUnmatchedProperty/2/2/1;1336=Some/1/1/2,Every/1/1/3;1341=Every/1/1/1;1489=Types/1/1/1,Some/1/1/1,getTrueTypeFromConditionalType/1/1/3,isTypeParameterAtTopLevel/1/2/3,getFalseTypeFromConditionalType/1/1/4,isTypeParameterAtTopLevel/2/2/4;1630=Clone/1/2/2,Clone/2/2/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/jsdoc.go", "jsdoc.cs", "AAwSwoKCgoKClLiCpoKClIKCgpSCpoKCgoKClIKUgpSClKaSgpSCpoKC3oIACxaigoKCgpSCgrg=", "", "", "76=entityNameToString/1/2/2,AsQualifiedName/1/1/3,entityNameToString/2/2/3;83=Text/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/jsx.go", "jsx.cs", "ADRUABEw7KKCpqKChIKUlKaygoKUgoKUpqKCpoKmsoaCgqaUgoKmsoKmsoKClIKCgoKCgoKCgoKUlIKCpsqkgpSCuLKClIKCxoKExoKCgpSCtIK4soKCgqailKSkpqiCgoKUlKaygoKIpqaihIKClIKCgoKUgpSCgpS4woKClIKCgoKUgpSCgpSCgpSUgoKCprKCgoKCgriCgoKClIKCgoKUgoKCgoKCkqSClIKCkoKCgpSUgoKCgoKEgoKmgoKCgpSEgoK4AA0UgoKCgoKCgoKm3KKWpISWyoLKpKaigoSCgpSCgoKCgoKCgpSCgpSmgpSCgpSCgoKChIKClISCgoKUgoKCgoKE3KaCgoKUtLSClKakgoKUhIKCgpSCgoKCgpSUgoKUgoKUgoKmgoKClJSmsoKCgoKCgoKCgoKCgoKUlJSUgoKUgoKUhIKUlJSuqIKCgpSUgoKCloKClIKUgoKUgoKUgoKmgoKClIKEgoKCgpSCgoKUgoK4hpSCgoKCpoKUgoSCgoKUlJSClJSClIKCgpSmgoKUAAIUsoKClIKCgoKCgoKCgoKCgpSCgoKCgoKIgoKCgoKCgoKClIKCgoKClIKUgoKEgqaCgoKUgoKUgoKCpoKClMqCgriEgoKUlJaCxoLGlIKCiIKUgoKCqIKClLS0toKCgoKCpoKUgoKUlIKUprKClqaygoaCgpSElKamsoKUgoKCgpSClISChJSEmKaCgpSmooKCgoKUprKCgoKUtLSChMaClIKEloKCgoKCgoSClJSUgoKUprKMgoKCgpSCgpSUloKClKaCgoKCgoKmprKEgoKCgoKUpoKClKaigpSmpIKUAAIWgqaihJQAAhKkgoKCgoSClIKUhLimsoKUgoKClIKCgoKUgpSmqoKCgoKClIKClJamooKUgoKUgpSmooKCgqqCgqyygoKClIKCgpSCgoKCpoKusoKClIKEgoKUgoKCgoKWgoKCgpSCgoKWgoKUgpSCpqKCgpSmooKClKaCpqKCgpSCgpSCgpSmooKCgrimooKClIKUgoKCgpSCgoKClKaCqIKClKaCgoKCgoKClIKCgoKCpoKCgoKmgoKCyoKCgoKClIKmgpSmgoKClIKCgoKCpqaCgoKCpqaCgoKCgoKUgoKCuIKUpoKCgpTGooKCxqKCgoKUgoKCkoKClIKClJSClIKClIKCgoKUgqai", "258-263:1;272-279:1;280-289:2;324-324:1;325-325:2;328-328:3;332-339:4;377-390:1;408-411:1;422-422:1;490-490:1;492-492:2;603-679:1;723-728:1;798-818:2;918-920:1;1458-1468:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/links.go", "links.cs", "AA8cgqaCpoIACxSCgoKUpoKmgoKU")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/mapper.go", "mapper.cs", "ABY0goIACRiipoKClMaCgpSmgoKUgoKUxoKClKaCgpSmgoKUyqKChgAKEoKCAAkUsoKCgoLGgoKUpoKmggAKFoKCgoKCxoKCgqamgqaCAAoWooKCgoLGgoKUpoIAChaCgoKCgsaCgoKmpoIACRSCgoKCxoIAChaygoKCgsaCpoIACxjCgoKCgoLGgoKClAANGLKCgoKCgsaCgoKGgoKUpg==", "94-96:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/nodebuilder.go", "nodebuilder.cs", "ABs6gqaCgoKCgpSCAA8ggqiChIKUgsqCgoKUgrjSgoKCgpTW0oKCgoKU1oKCuqKCqKKCgoKCgqaigoKCqKKCqKKCqKKCqKKagoKUlJSWgoKUtLSCxtjGsoKCgpSUgoKCggAHEqaigoKClJSCyKKCqKKCqKKCqKKCqKKCqKKCqKKCqKKCpqKCyoKmooLGooKUgpSCpoKC", "289-291:1", "", "40=GetSourceFileOfNode/1/1/8;230=Modifiers/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/nodebuilder_hover.go", "nodebuilder_hover.cs", "AA8gggAEELKCgoKmgoKogoKmgoKmqIKCgoaCgoKCgoKClIKCgpSUgoKUpoKClIKClKaigoKUgoKUlKSUqNKClIKCgoKUkKSClIKCgoKCgoKUqJiCgoKUgoKCloaCgoKWgoKCqJaUgoKCgoKU2oKEgoKCgqaClIKClIKCgrjMooKUgrSCxsyygpSCgoKCgoKCqIKWhIKClJaCloKWlKaCgoKCgpSCuIKClIKUqqKGgoKCgoKClJSowoiCgoKUgoKCgpSCgoKCgqaCpoKCgqaCyoKUgoKClLiygoKCgoKmlLqylKSkpKSkgoIAExrygoKEgpSClJSCloKAkoKChIqCgoKCgoKCqIKCgoSCgoKCuKaCgoKClN6CuJaCgoKCgqaWgoKClKiCuoKCgpSCgrqCgpSGgoKCgsqCgpSCgIaCpNiigoKCgoKCgoKogoKWgoKWgoKCgoK4gpSqosii", "55-57:1;121-121:1;124-124:2;145-145:3;154-156:4;241-241:1;300-302:1;330-330:1;357-357:2;431-431:1;531-533:2;548-550:3;561-561:1;593-595:1", "", "317=IsInJSFile/1/1/2,getSignaturesOfType/1/1/3;348=CreateModifiersFromModifierFlags/1/1/1,NewModifierList/1/1/1,NewNodeList/1/1/2;406=NewIdentifier/1/1/4,serializeTypeForDeclaration/1/1/4,NewVariableDeclaration/1/1/4,NewNodeList/1/1/3,NewVariableDeclarationList/1/1/2;474=NewIdentifier/1/1/2,serializeTypeForDeclaration/1/1/2,NewVariableDeclaration/1/1/2,NewNodeList/1/1/1,NewVariableDeclarationList/1/1/1;487=NewIdentifier/1/1/3,NewExportSpecifier/1/1/3,NewNodeList/1/1/2,NewNamedExports/1/1/2;505=NewIdentifier/1/1/1;605=HasStaticModifier/1/1/1,IsClassLike/1/1/1", "", "125:21,126:13,449:21,450:13")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/nodebuilderimpl.go", "nodebuilderimpl.cs", "AHD6AbKClIKCxqKCgpSCgoK4goKUgoKCgpSUgqqCgoKUrKKClIKUgoaUhJSqgoKCpgACEoKClIKUgpSClIKqgq7SgpaCgpaClIKUgpSCloKCgoLYpLIAACSEgoKCgpSmgoKCgoKClJSCgpSUgoKUxqKCgoKCgpSCpqLGgoKClAALBqKCpoKCgoKUlIKC3IKUgpS4goqCppSCgoKClIKUgoKUlIKCgoKCygAAEIKCgoaCuKamooKClIKCpoKCpoKUxqKmgoKUgoKCgqamssaChLiCgpSCmIKUpoKmooKUhIKClKa0gpSCpoKCrIKClKbCgpSClIKCgqamooKUgpSGgoKUgoKCprKCgoKCpoKCgoKCgoK4pqKCgpSmhJSClIKCpoKKgrqoooKClKaygoKUgoSCgpSCgpSCgoKChIKCpoKUgpSEgoKUlISUuIaCuIKCgoKapoKCzoKClIKClJSCgpTGooKUxqKCgpSCgoKmgoKCgoKWgoKEgoKUgoKCqIKCgu6CgoKCgqaCgoK6lJSUiIKCusyClIKCgpSUpoKCpqKClIKUgoKmgoKCpoKCgoKCgoKUpoKmgoKCgoKGgpSCgoKUgsaCgpiClKaCpoKmosaigoKClIKClLSkgpSClJSCgqYAAhSigoKUjpSCgoqCgoKCuJSCgpSCgpSUpMiCgpSqgoKCgqaigoKCgpSUgoKUgoKUuKiigqa0goKCgoKClJQACxLCgoKClIaCgpSCgoKClJSCgoKCgoKChoKmgoKCgpSUgsqClIyClJSmgoKCgoKElISWlMaigpQAARikpoKCgpSmopSGgpSkpIKClKSClKSkgpSkpIKUpIKUpILosoKCgpSCgqaCgoaCuIKCgoKUlICCtoKClKaCgoKCgpSCgoKUgoKcgoKCgoKUAA0egoKClIKmooKUpoKCgpSClILmgoKCpoKClIKCgoKCgpSCgoKClIKCgpSClIKUgpSCpqKCgoKCgpSCgoKClIIABySygpSCgoKCgqaCloKClIKogoKUlKaigoKUprKCgqaCgpSCgoKmgoKClIKClIKmhqyCgqamgqaiprKCgoKClIKClIKCgpaahoKWgoKClIKClIKEloKElKiCgoKClKiCAAYQgpSKgoKUgpSUgoKmzIoABhCmsoKClIKCgpSUgoKU3qKClIKCgoKmpqKCgoKUpoKm0oKCgoKClJKCgoKUpMaigoKUgpTGspSCgoKCgoKCpoKCgpSCgoKCpgAGEIKmsoKmgpSCgoKkgoKCpMiigqaUgoIABxKCpoKmpIKmooKCgoKCgoKUlJSUgqaygoKUlIKCgpSUgoKUAAwSwpSCloKCpoK4goSqgoKUlIKUlJSCgpSCgqaCgoKUgoKUgoKUgqaClLS0goKUtLS0tLS6gpS0gpS2trS0AAMQgsaigoKCgoKGgoKCgoKClKaCgoKClIKCgoKCgpSCpoKmlJKCggABEoKCgpS0tIKCgpSUlKaCgoLKxqKClAABGqyygoKClJSCgoKCgoKmlIKCgoKCiIKCgpSUhqaUgriClIKmooKClqayhIKEiIKCgoSCgoKUgoKUgoKUlN6ClIKUgrimwoKUgoKClKaClIKCgoKUxqKCmIKUAAQU0oKCgoS4gpSCgoKUpoKCgoKClN6CgoKmgoaUgoSCgoKUgoKUlIKEpoKGgoKUnIKCgoKUpoKCgoK4gqaCgoKCgpSmgoKUAAgKAAIWgpaCloKCgoKCgr6ClIKUgoKClIKClIKCgoKCuKaEgoKCloKCAAwagoKUgpTGgpSkpMiCgoKUgoKUgoKUpoKCprSCgoKmgoKCgoKmgoKEgoKmqMKClIKClIKClIKCgpSEgoKCgqaCgpS0tIKClIKUlIKUprKCgoKUlIKCgoKCgoKCgqa4poKCgpSUgoKUgoKCgoKCgoKClKaClIKCgpSmgpSChoKmgpSCgoKmgsqCgpSUgoaCuIKUgqaCgpSClIKUlIK4goKClJSClKaygoKClJSCgpSCgpSUgqaCgqaCgoKUgoKClIKUgqaCgpSClIKUlIKUuMKCpoKCgoKCgoKCpoKCgqaCgoK4hoKIjISUpoKCgoKCgoLGgoKCgqbGtIaCgoKClIKCgpSIgoLKgoKWlqaCprKClIKClKaygoKCgoKCAAAQjIKUgoKCgqaClJSCgpQAAhCCgpSmhIKElKaouKSClIKCpoKClKaygoKCgpSUlKaygpSCgoKCgoKChIKCgoKCggABGIKCgoKUgoKCgoKCpqKCioSClOaygoKCgrqCgoKUlIKCgoKUlIKCgoKCgpSC3pS02IKCgpS4goKCgpSmgoSClIKCgoKChIKCgqqCgoKCgoKClMqCgoKCgpyCgoKCgoKClNyUgoKCgoKUyrKCgoKUtLS0uoSCgoKChIKUgpSCuIKCgoKUlIKCgoKCgoKCgpTugoKUggAGKNKClIKCgpaCppaCgoK4gqaCgpSClIKUgpSCgpSCgpSCgpSCgpSCgoKCgoKUgoKUgoSCgpSmgpSUgoKCgpSCgoKClKaCgpSCgoKUgqaCgoKClJSClIKClIKClIKClIKClIKClIKClIKCgpSUgqaEgoKCgpSClJaCgLaUgoSCgoKClIKUpoSCgoKClIKCgoKKgoKCppSCgoKWgpSCgpSUlIKUgoKClJSClIKCgpSmgpSogoSUgoKCgpSCgoKCgqKCgpSUgpSClIKClIKCgoKUgpSCgoKUgoKUuNaCpoKCgpSCyqLGgoKClKailKSk2KKClKaigoKCgqaCgqaCpoKCgpSUpqKCgoKCpoKClIKCpqaC", "139-143:1;245-247:1;428-430:1;571-573:1;1097-1102:1;1199-1201:1;1257-1259:1;1364-1364:1;1729-1729:1;1879-1881:1;1881-1883:2;1984-2022:1;1985-1987:1.1;2023-2054:2;2026-2052:2.1;2059-2061:3;2160-2163:1;2165-2196:2;2290-2292:1;2638-2640:1;2667-2669:2;2804-2806:1;2808-2810:2;2812-2814:3;2847-2849:1;3081-3087:1;3311-3313:1;3465-3465:2;3571-3580:3", "", "348=typeToTypeNode/1/2/1,typeToTypeNode/2/2/3;616=createEntityNameFromSymbolChain/1/1/1;698=NewImportTypeNode/1/1/1;787=NewTypeQueryNode/1/2/1,NewParenthesizedTypeNode/1/1/1,Expression/1/1/2,NewTypeQueryNode/2/2/2;797=getMembersOfSymbol/1/3/1,getMembersOfSymbol/2/3/1,getMembersOfSymbol/3/3/2,getSymbolIfSameReference/1/1/2;800=newStringLiteral/1/1/2,NewLiteralTypeNode/1/1/2;804=NewTypeReferenceNode/1/1/1,newStringLiteral/1/1/2,NewLiteralTypeNode/1/1/2;951=FindAncestor/1/2/6,FindAncestor/2/2/6;1068=needsQualification/1/1/1;1392=AsNode/1/1/2;1518=getModifiersTypeFromMappedType/1/1/1,getConstraintTypeFromMappedType/1/2/3,getConstraintTypeFromMappedType/2/2/3,getConstraintOfTypeParameter/1/1/3;1522=newSymbol/1/1/1;1535=newSymbol/1/1/1;1550=getMappedTypeModifiers/1/1/2;1579=getModifiersTypeFromMappedType/1/1/1,typeToTypeNode/1/1/1,AsTypeReferenceNode/1/1/2,Clone/1/1/2,NewTypeParameterDeclaration/1/1/2,NewInferTypeNode/1/1/2,NewKeywordTypeNode/1/1/4;1585=getConstraintTypeFromMappedType/1/1/1,typeToTypeNode/1/2/1,AsTypeReferenceNode/1/1/2,Clone/1/1/2,getModifiersTypeFromMappedType/1/1/2,typeToTypeNode/2/2/2,NewTypeOperatorNode/1/1/2,NewTypeParameterDeclaration/1/1/2,NewInferTypeNode/1/1/2,NewKeywordTypeNode/1/1/4;1772=Name/1/1/4;2525=IsSymbolAccessibleByFlags/1/1/1;2849=IsVariableDeclarationList/1/1/1,IsVariableStatement/1/1/1;2861=IsValueSymbolAccessible/1/1/1;3037=Target/1/2/1,IfElse/1/1/1,Target/2/2/1,newIdentifier/1/1/1,NewNodeList/1/1/2;3065=NewToken/1/2/1,IfElse/1/3/1,Target/1/1/2,AsTupleType/1/1/2,getTupleElementLabel/1/1/2,newIdentifier/1/1/2,NewToken/2/2/3,IfElse/2/3/3,NewArrayTypeNode/1/1/4,IfElse/3/3/4", "", "224:21,225:13,3270:25,3271:17,3421:25,3422:17")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/nodebuilderscopes.go", "nodebuilderscopes.cs", "AAsUAAAagoKCgsKCgoIAChKigoKCkoKUytKCgqayAAAagoKCgoKUAAAilIKCgoKCpoKCgoK4lIKClIKUgoKEgoKClKamhoKCgoKCgpaCgpSCpriClKKClIKCgpSEgqSCgpSCgpSkgqTqgoKClIKCpoKUgoKUlNyCkoKUgoKUgt7SgpSClIKC", "27-32:1;44-50:1;97-161:1;126-137:1.1;151-158:1.2;163-163:2;166-220:3;180-216:3.1;184-196:3.1.1;198-207:3.1.2;223-223:4;224-235:5;240-250:6")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/nodecopy.go", "nodecopy.cs", "AA8YgoKmpoKmooKCpoKCpoKClIKmgpS0tKSCpqKClIKIgpSUgoKssoKWgoKCgoK4goIAEh6CgoIADRSCgoKmgoKCAAsQgqaCpoKmgqaCpoKmooC2ooC2gqaigLaCpoKCxoL8goKCgoKCpqKCgoKUgpSClIKUprKCgoKCgpSCpsKCgoKUgoKCgoKUgoKUgpSCgqaCgpSCpqaigoKUgoKmgoKClMamhIKCgoKCgoKCgqaClIKClJSClJKCgpSCgoKClIKChIKCgpSUgoKMggAAEIKUgpS4hIKCpoSClJSUlIKCgoKUlIKCgoKUlJKCgt6CgpSUkoKUgoKUgoKCpoiUgoKCzIKClKbCgpSkpKSCxpTSjISWgpyCuJSCuJSElIKUgoKCgpSCgoKUlIKCgoKUlJQAAiCCgoKUjJSCggAHEoKCgpSClIKCgpSClIKCgpSClIKCgpaCgpSCgoKUgqaGgoKUgoKClJiCgoKUgoKUAAcSgoaWgoKmgoKClIKClAAIEgAIEgAJFAAJFO7u7u7uAAgSgoKCmIKmgoKCiIKUlJTegoKCgoKCggAHFISCgoKUgqaIgoSUgqaUgrSClIKCgoKCgoKCgoKCgoKUlIKCpoIAAhKUgoKGgpSmpoKEgpSUqIKCgoKm", "92-95:1;164-164:1;168-168:1;176-176:1;293-316:1;295-313:1.1;317-383:2;385-391:3;392-399:4;400-415:5;416-451:6;452-467:7;468-825:8;598-600:8.1;827-878:9;879-889:10;890-897:11", "", "392=getExportSymbolOfValueSymbolIfExported/1/1/6,getSymbolIfSameReference/1/1/6;409=IsSymbolAccessible/1/1/1;439=AsTypeQueryNode/1/2/1,AsTypeQueryNode/2/2/3,VisitNodes/1/1/3;472=AsTypeReferenceNode/1/1/1;523=AsJSDocNullableType/1/1/1,VisitNode/1/1/1,NewKeywordExpression/1/1/2,NewLiteralTypeNode/1/1/2;530=AsJSDocOptionalType/1/1/1,VisitNode/1/1/1,NewKeywordTypeNode/1/1/2;578=AsTypeParameterDeclaration/1/4/1,Modifiers/1/1/2,VisitModifiers/1/1/2,AsTypeParameterDeclaration/2/4/4,VisitNode/1/3/4,AsTypeParameterDeclaration/3/4/5,VisitNode/2/3/5,AsTypeParameterDeclaration/4/4/6,VisitNode/3/3/6;649=AsImportTypeNode/1/5/1,AsImportTypeNode/2/5/2,AsImportTypeNode/3/5/4,VisitNode/1/2/4,AsImportTypeNode/4/5/5,VisitNode/2/2/5,AsImportTypeNode/5/5/6,VisitNodes/1/1/6;675=AsPropertyDeclaration/1/1/1,Modifiers/1/1/2,Name/1/1/3,PostfixToken/1/1/4;684=AsPropertySignatureDeclaration/1/1/1,Modifiers/1/1/2,Name/1/1/3,PostfixToken/1/1/4;693=AsParameterDeclaration/1/3/1,AsParameterDeclaration/2/3/3,Name/1/1/4,AsParameterDeclaration/3/3/5;703=AsMethodSignatureDeclaration/1/4/1,Modifiers/1/1/2,Name/1/1/3,AsMethodSignatureDeclaration/2/4/4,AsMethodSignatureDeclaration/3/4/5,AsMethodSignatureDeclaration/4/4/6;713=AsCallSignatureDeclaration/1/3/1,AsCallSignatureDeclaration/2/3/2,AsCallSignatureDeclaration/3/3/3;720=AsJSDocSignature/1/3/1,AsJSDocSignature/2/3/2,AsJSDocSignature/3/3/3;727=AsConstructSignatureDeclaration/1/3/1,AsConstructSignatureDeclaration/2/3/2,AsConstructSignatureDeclaration/3/3/3;734=AsIndexSignatureDeclaration/1/2/1,Modifiers/1/1/2,AsIndexSignatureDeclaration/2/2/3;741=AsFunctionTypeNode/1/3/1,AsFunctionTypeNode/2/3/2,AsFunctionTypeNode/3/3/3;748=AsConstructorTypeNode/1/3/1,Modifiers/1/1/2,AsConstructorTypeNode/2/3/3,AsConstructorTypeNode/3/3/4;777=AsTypePredicateNode/1/3/1,AsTypePredicateNode/2/3/2,VisitNode/1/2/2,AsTypePredicateNode/3/3/4,VisitNode/2/2/4;790=AsConditionalTypeNode/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/printer.go", "printer.cs", "AA0agqaCpoLcgu6C/IKmgsaCxoKm2IKUgoKUgoKCgpSCkoKCopSCgoKCmIKClJSCgpSClIKClIKUgoKUlNaCpoKmgqbCgqSCgoKUgpSClIKUgqaCkoKClISClKaCgqSkgoLWgqaCpsKCgoKClKaClLiCkoKCopSCgoKCgpSCgoKUgpKC1oKmooKSgpKCgoKCgpSC1oKmooKCgoKCgoKCgpSUgoKCgoK4poKUgpSmgoKmooKS2MKCkoKCopSCgpSCgoKUgoKClJTYooKSgoKilIKClIKCgpTWgoKmgoI=", "80-82:1;208-210:1;305-307:1;333-335:1", "", "", "", "92:21,93:13,238:21,239:13,360:21,361:13,395:21,396:13")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/pseudotypenodebuilder.go", "pseudotypenodebuilder.cs", "AA8esoKCgoKmpoKCgoKCgoKCgpSCgoKCpqjSgpSkgoKCgpSClJSCloKCgpSUgpSCloKkgoKClKSGgoqCgoKmgpTGgoKCgoKCgoKUlIKClJSUgoKCgqaUgpSCgpSUpIKUpIKUpKSkpKSkpKSCgoKCkoKCgoKUlIKCpIKCgpiCgqSCgoKCAAEQgpqClIKCgpSCgoKClIKUgoKCgoKUlIIACBaUAAkSggAIEIIAChSCAAoUgpSCgqaCgoKUpIKkgviigoKUprKCgoKUgpQABhKClKy0gpaCgpSCgpSCgoKUhIK6gpSEgrqcgoKCppSClKSCgpSGgoKUgpSCgoKClIaCgoKmgoKUpoKCgpSUgoKUgoKChIKUgqbGgoKElIKClIKCgoKUlIKClMaCgoKUxoKCgoKU2KSCgpSGgpSCgpSCgqakgoKUgoKClJSCgpSCgoKClJSElKSClKTIooKClIKCgoKCgpSUgoKClJSCgpSUgoKCgpSUgoKClKbGopSkgqTKsoKUgoKUhIKCgpaCgoKWgoKogoKUgoKCpoKUpraClKSCgpSCpKSCgpSkgoKCgoKCgqaCgpSUgpSCgpSUpKSkpKSkpKSkpIKkpII=", "119-133:1", "", "258=reuseName/1/1/2,pseudoParametersToNodeList/1/1/6,pseudoTypeToNode/1/1/7,NewFunctionTypeNode/1/1/4;269=reuseName/1/1/2,pseudoParametersToNodeList/1/1/5,pseudoTypeToNode/1/1/6;280=reuseName/1/1/2,pseudoTypeToNode/1/1/4;290=reuseName/1/1/2,pseudoParameterToNode/1/1/4,NewNodeList/1/1/4;302=reuseName/1/1/2,pseudoTypeToNode/1/1/5;364=Symbol/1/1/4,parameterToParameterDeclarationName/1/1/4,pseudoTypeToNode/1/1/6")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/relater.go", "relater.cs", "AFS8AYIACA6CpoKClKaCxqKmooKUpqKClKaigpSmooKUpqKmoqaipqKmgqbSgpSClIKUgoKUhIKUgqaCgoKCpoKUpsKCgoKUgpSClIKUgpSClIKUgpSClIKUgpSCgpSCqoKUgpSClIKCmoKUgpaCpqbCgoKClIKUgoKUgoKCgoKClIKUgoKEgoKUggABEIKCgoKClILKgqaipqKmoqaCrAAGEIKCgoKChIKCgpSClIKEgoKCgoS4lILGooKUgpSCgpTGooKClMqipoKClIKUprKClIKUhJS0gpS0pJTGpKSkpKaipoKCgpSCgoKmgqamsoKUgoKClIKClJS0gsam0oKUgoKCgoKmgoKClIKClKbCgoSUgoSUgpaEgoKUhIKClIKCgoKCgoSmgpSCgoKCgpSCgoKCgqaCgoKUlIKUgqaCpqKCgpSCgoKmpqKCgoKmtIKUgoKUgoKUgoKCgpSClIKCgoKClIKUgpSqooKCmIKUgpSmgoKCpgACILKIiqaClIKCgrjGgqaiAAQkgoKCgoKCuIKCgoKGgoKCpsrGgoKCgoKmlKaCAAISooKUgoKEpgACEqSCiJSKlIKmhpSElMaigIKkgIKkgIKkgIKkgIKkprKCgoKCgoKUgsqmooKUprKCgoKCgoKEgoiCgpSCgtymsoKUpoKCgoKChpSUpoKmgoKCpqKChIKUgoKCgpSCgoKCgoKClNzGooKUgoKCgoKUgoKmgpQAChKCpoKmgoKCgqbIsoKClIKCgrimgoKCgqamooKCgoKCgqampoKCgpSCgpSsooKClIKUqrKCgpSmooKClIKClIKClMaixoKCgoKCyq6igoKCgoKClIKCgoKCgoKmgriClAAMErKCgoKCpoiCgoKCgoKUzIKCgpTKgoKCgqaCgqamooKCgqbGosaygoKUlIKClJSCgpSmgqaipqiClIKUgoKCpqa0gpSmogACENKCgoKAgoKCsoKClIK2goKCgpSCgoKCgpSClMa0goiCgoyClIKUgpS4gpSWgpSCgq6CgoKmgoKCgpiCuNaigoKmprKCgoKUgoKUlIKmgqaigoKCpqqCgoKmpqKmwoKUgpSClIKCgoKUpoKGlJSCgpSCgoKClIKClIKCgoKChIKClIKUgoKUlKaCgpSUgoKUlIKCgpSUgoKUlAAAEoKCgpSClISCgpSClIKogpSCgpSUpoaCgpSEgoKCgoKCgpSsgoKUgpSChoKCuLi4prKCgoKUlIKCgoKUpoKUtLS0gpSosoKCgoKUlJSuooKCgoKmpoKmooKCgoKCgoKGgoKUgriCgpSUgpSCgoKUlJSmooKClKaigoKUpqKCgpSIgoKCpqyigoKCpqaigoKCgoKUpoKClIKCgoKCgpSClJSmsoKCgoKUlIKCgoKCgoKUlIKmpqKmooKClKaygoKCgpSUgqamsoKCgoKUlIKUprKCgoKUgqaCgpSmooKUpqKClIKmsoKClIKCgoKUpqKClIKUgoKUlKaigpSCAAAYgpQAAhqClKSCgoKCgoKCgpSC6qaygpSCgsa0goKUgsaC+IKmgpSmsoKCgoKEgpSCloKClIK4gpSCprKmwoKCgpSCgpSCgoKmsoKClKaCprKClKaihoKUgoKmlIKGgoKUgoKmgpSUpoKCgoKmrMKClIKWgpiCgoKCgoSmlIKCgoKCgoKClLiCgoKCgpSUgoKCgpSmpoKCgoKCgoaCmIKUpqKClIKCgoKCloKuprKUpKSkpqKCgoKGgoKUgoKCpoKqgoaCgrqUprSCgoKCgoKmooKCgoKmlKaylKSCgoKUpqQABDKigoKCgoKCgoKUgoKCgpKClJSygoKUgoKCgpSCgpSCgoKCgoKCgoKUgoKUlIKCgAAAIoKCgpS2gqaigpSmspSopIKsgqSmopSkAAISgqSmsoKClMaCgpSClAAiQqKCgpSCprKCAAcQxoKmgqaCpsKCmIKElIKUnIKCgpSCgpSClIKegpiCgoKUtLSCgoK4hJSCgoKCgpSmiIKCgoKCgoKElKaUgoSCgpSUgqaClKbChJSCgpSCgoKCgpSUgoKChoiClIaGlIKCgpSogoKUgoaCgoKmgpS4lIKClLjGooKClKaygoKClJSClIKClMaCpoLGsqaygoKCppSClMbIgoqCgpiCgqaClJSClIKegoKCgoKUlJSCgoKUgoKClAADEKaigoKUgoKCpqaygoaCgoKUgoyCgoKmgoKUlKaygpSmwoKCgpQAABKCgpSUgpS0tLSClJSCgoKCuIKCgqaEgoKmpqKCgoKCgpSUpqKCgoKCgpSUAAIQ4oaUgoKYgoKUgpSmgoKWgpqCgoKmiJSCgoKCgoKCpoKCgqaCgoKCgpSUgIK0lIKCgpSClIKCgoaUAAMSgoKU1oKCgoKCpqaC3IKCpsKCggAAHIKCgpSUgoSmAAEWgoIABBTGgpSmoqzCgoKCgpKClIqCgpSCAAAQAAASgpqClJSWlIKClKSkgoKCgtiCgoKCgoKCgoIACA6CgoKC2IKCgoKCgqbGgsaCxoKCAAEQhsyEgoKUgoKCgoKCgqqCgoKmgoKCppaCgoKCgoK4hoCSpMaGgoKUgpSCqoKCgoKCgoKCgoSUgoKWgoLcgsaEgoKCpoaCgqqCioKUiIKCgoaEgoKUlILugpSKhIKEgpSUgoKUlILqgoKClJiUgsaCgtqCgoKGgpSGgoKUloKCgoKcgoKGgoKCggACFoKUtLSEgoK4gtiWgoKCloKClIKCgpSGgoKCgvyCgoKUgoKMgoKCgpSUgoLcgpSIgoKChIKCgpSCgoKUgryCgoKCqoKCgoKCguqCgoKCguqCgpSCgqaCgoKC7IKUgoKCgqaUgoKCgpSYgpqIgpSCgsaCmKSCgsauhIKCgoKCgoK4goKUgoKCAAMSgoKCgoLqpqKClIKCiIKClISCgoKCiIKUqIKUgoKCgoqCgqyCgriClKasooaCgoKCgoK4pgAEFIKCgp6CgoKCgpSUgqiCgoKCgpaCgoKCgoKCgpSYgoKCgoKCgoKClIKWqIKmgrSEqIKCgoKCgoKKuIKmptKClIKCgoKUgoKCgpSUgoKCgpSUgoKClJSCgpSUgoKClKaUgoKCgoKCgoKUlIKCgpSCgpSUlIKClIKClJSCgpSUgoKUloKClIKmgoKCgpSUgoKCgpSmlJSUgqaCgoKClJSCgoKClLyCgoKCgoKCgoKUuKbCgoKUgoKClKbGgoKCgpTGgpQAAhCCloKCgpSWAAAQgpSUprKChIKUgqbEioKCgoKmgoKCgoKClIKCgoKUgsoAAhCCgoKUlJSCgpSUgpSmgoi4woKUgoKClIKCgoKUgoKUlKbCgpaClIKUgoKCgoKKgpSUgqaCnoKCgpQABBCCtIKChIKCgoKCgpSUgpTmprKClIKEgpaCloKUgpSowoKUtLSCgpSGpqKCgoKUgoKCgpSUpqKClIKCgoKClLS0tIKUlKbCgoKagpSClM6igpTMwoKCgoKUlISClIKCgoKUlIKCgpSUpoKCgoKUpqbCgoKClKamooKCgpSCgoKmptKCgoKUgpSClJS0tIKCgoLGgoKCgpSCxoKCgoKCgsrCgoKGgoKWgoKUlIKClLS0tILGgpS0tLSCgoKCprSClJiqgsqCzILGprKElqqClLS0tLSCgoKamIKCgoKClIK2xoKClIKCgoKClKaCgoKUxoKCgoKUgpSCvKKCgqbGgoKClKaCAAIQooKCgpQABBrClKioqIKClKSkpKTIosaygoKUgoSUgoKC", "479-482:1;682-684:1;827-827:1;911-911:1;947-947:1;1341-1348:1;1749-1751:1;1772-1772:2;2129-2129:1;2384-2389:1;2433-2438:1;2439-2453:2;2512-2514:1;2801-2801:1;2920-2929:1;3249-3249:1;3288-3290:1;3298-3339:1;3302-3302:1.1;3329-3329:1.2;3470-3470:2;4092-4092:1;4587-4589:1;4615-4615:1;4998-5000:1;5002-5004:2;5006-5008:3", "", "469=elaborateDidYouMeanToCallOrConstruct/2/2/1;759=getApplicableIndexInfoForName/1/1/1,isLateBoundName/1/1/2,getIndexInfoOfType/1/1/2,isHyphenatedJsxName/1/1/3;785=AsSubstitutionType/1/1/2,isExcessPropertyCheckTarget/1/1/2,Types/1/2/3,Some/1/1/3,Types/2/2/4,Every/1/1/4;846=Types/1/1/1,Some/1/1/1;982=getSignaturesOfType/2/2/1;1445=isTypeAssignableTo/2/2/1,IfElse/2/2/1;1655=getTypeFacts/1/2/1,getTypeFacts/2/2/1;2311=getDefaultOrUnknownFromTypeParameter/1/2/1,instantiateType/2/2/1,getDefaultOrUnknownFromTypeParameter/2/2/1,Invoke/2/2/1;2591=isValidBigIntString/1/1/1,AsIntrinsicType/1/1/2,isMemberOfStringMapping/1/1/3,AsTemplateLiteralType/1/1/4,isTypeMatchedByTemplateLiteralType/1/1/4;2724=IfElse/1/1/1,isSimpleTypeRelatedTo/2/2/1;2770=IfElse/1/1/1,isSimpleTypeRelatedTo/2/2/1;2783=isWeakType/1/1/3,getPropertiesOfType/1/1/3,typeHasCallOrConstructSignatures/1/1/3;2791=getReturnTypeOfSignature/2/2/1,isRelatedTo/2/2/1;2800=Types/2/2/1;2863=FindAncestor/1/1/1,GetSourceFileOfNode/1/2/2,GetSourceFileOfNode/2/2/2;3328=Types/1/1/1,Some/1/1/1;3449=isMarkerType/1/2/1,isMarkerType/2/2/1;3956=getMappedTypeModifiers/1/2/1,getMappedTypeModifiers/2/2/1,getCombinedMappedTypeOptionality/1/2/2,getCombinedMappedTypeOptionality/2/2/2;4309=Name/1/2/1,Name/2/2/2,IsPrivateIdentifier/1/1/2;4362=Invoke/1/1/3;4594=typeHasCallOrConstructSignatures/1/1/1,AsReverseMappedType/1/1/3,isObjectTypeWithInferableIndex/1/1/3", "", "1406:33,1407:25")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/services.go", "services.cs", "ABcigqaihKaCmJKCiILKkoKCypKChIbcsoKCiIKCpoKmtIKUtKSkgoKYrIK2goK4gqaCgqamlIKmgqaigoK4goKmgoKogoKUgoLKgqailKSkpKaEgqaCAAIWogAHFIKCgqaCgoKCgoSCyqaCpoKmgqiCpqKmoqaCpoKmgqaCprKCgoKCgoKmgpSCgoK4pqKCgqaCgqaCgpSmgoKmoq6CgpTGooKCgoKCgoLKgoKUgvaCioKUpqKCgoKCgoKCgoKCgoKUlIKClIKUlMaigpSmgoKClIKClKaigpSmsoK+goKCgoKmgoKCpoKCpqaCgoKCgoKClJSClJSmoqaklIKUgoSUpKSmooKUAAISooKUgpSCpqq8goKCgoKUgriCgqaqqoKCgpSCgpSCgpSCgoKmgoKCuKiogoKCgpSCgpSCgoKUgoKCgqaCgoKCuKaCgoKmxqKCgoSYuIKCgpS4spyCpoKClIKmgoKGlIaUgoKUgpS4xoKClKiilKSkgpSkpKSmooKCgpSCgoKmpqKCgoKCgoLKiILMgsyClKi+gpSClKaCgoKUgpSCgoLepqKCgoKCgoKUpoKClIKClLqigoKCgoKmpoKogoKClKaCprKCpoKUgoIABhCEgoK4prKCgoKCgpSCpoSCgpSWgoKmgoKUpqiCpoKCgoKCpqqigpSSgoKCgpSCgpSoAAkSABYugoLGsoKCgqaCgoKmqLKCgpSCgpSUgoKYhoKCpoScAAIYooSCpgACGLiCgpiCgpiCgoKCloSCgqaCqKKClIKCgqaqooKClJSClA==", "32-42:1;44-50:2;52-63:3;65-124:4;329-329:1;360-363:1;426-428:1;647-652:1;818-837:1;913-916:1;920-923:2;1039-1041:1;1043-1045:2;1053-1055:3", "", "61=GetDeclarationOfKind/2/2/1;74=AsModuleDeclaration/1/2/1,AsModuleDeclaration/2/2/2;204=Expression/1/1/2;331=isArrayType/1/1/3,isTupleType/1/1/4;447=Get/1/1/1,Types/1/1/1;691=IsIdentifierPart/2/2/1;776=getUninstantiatedSignatures/1/1/1;782=getUninstantiatedSignatures/1/1/1;790=getSignaturesOfType/1/1/1;793=getSignaturesOfType/1/1/1;817=getEffectiveTypeArguments/1/1/2,newTypeMapper/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/stringer_generated.go", "stringer_generated.cs", "/oaCgsq0goKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/symbolaccessibility.go", "symbolaccessibility.cs", "AA0WgoKmgoKmgoKmgoKmgoKEgoKEgoKmgoKCipYABSaCgoKUgoK4gsqCgoKU3MaipoSClMayhoKClIKCiIKClJiCiIKCgoKCppSmgoKCgoKUgqaigpSCgoKClIKClIKEgoSUgoKUgoKUlIKCuIKWgoKClIKCgpSUgqaogpSClIKClIKUgpTGosaCgoKUpoKCgpSCgpSClK7ChIKUgoKEgoKClJaCgoKUpoKCgoKUlIKCgpSmgqaCgoKClIKClIKUptKEmIKCgqaCgoKUgoKCpoKClKaMpowAFzqCpoKugqaCpoLGooKUgpaCgoKUgoKClIKCppSSgoKClJSCrIKCgoKCpoKClJSUgq6ChIKYgoKCuIKCgqaCgpSUpr6EgoKmmoKCvIQAABSCgoLKhIKogpSmgoKCpoKCgoKUlMaipqLGjIKqgoKUgoKClIKUpryCgpSClIKClIKUgpqorKqCgoSCgpSChJaEqIKClIKClIKCqKbGgoKCmrS2lMaohIKCppSClIKCAAQUgoSCgoKUpoIAARCCgoL8rrKCgoKmgoKClIKClIIAAhiCpqKCgoKqgoKChAAIFNw=", "118-120:1;144-153:2;450-453:1;466-473:2;690-723:1", "", "79=symbolToStringEx/1/1/2;116=getAccessibleSymbolChain/1/1/2;124=getDeclaredTypeOfSymbol/1/1/3;513=isUMDExportSymbol/1/1/2,GetSourceFileOfNode/1/1/2,IsExternalModule/1/1/2,Some/1/2/4,Some/2/2/6,getDeclarationsOfKind/1/1/9;607=getMergedSymbol/1/1/1,canQualifySymbol/1/1/1;613=getQualifiedLeftMeaning/1/1/2,getAccessibleSymbolChainEx/1/1/2;760=symbolToStringEx/1/1/2,symbolToString/1/1/3,IsInJSFile/1/1/4,IfElse/1/1/4;769=symbolToStringEx/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/symboltracker.go", "symboltracker.cs", "AA8cgoKCgoKUuMaigoKCloKmpoKCgpSmgoKClKaCgoKUpoKCgpSmgoKClKaCgoKUpoKCgpSmgoKClKaCpoKClKaCgpSmgoKU")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/tracer.go", "tracer.cs", "ABAqgsaCpqKCpoKClMKCkqK2gqaCgoKCpoKCpoKUooKUAAsWtIKmgqaCpoKmgoKUpoKClKaCgpSCgpSmgoKUpoKClKaCgpSCgpSmgoKUgoKUpoKClIKClKaCgpSCgpSmgoKUgoKUpoKClIKClKaCgpSCgpSmgoKUgoKUpoKClIKClKaCgpSCgpSmgoKUpoKClKaCgpSCgpSmgoKUgoKUpoKClIKClKaCgpSCgpSmgoKUgoKUpoKmgoKUpoKmzoKUhIKUlPaigpSmgoKUgoKU", "38-42:1;64-70:1;342-344:1", "", "", "", "352:25,353:17")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/types.go", "types.cs", "AKEDkggAH0iCgoKCpoKUyILGgoKClLS0tLS0tIKCgpQAP5QBooKUpqKClAAQHoKmgqaCqoKCgoKClIKCgoKCgoKCgoKCmIKCgoKCgqailKSkqoKUpKSkpKSmgpSkpKSmgpSkpKaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoIAFCqCgoKCgoIAChIADBaCpoKmgqaCABAiAA8elIKmgqaCAA1WAAsUABQmlIKClKaCgpSmgoKUpoIAFzCCAA4WgoKCgoKUlAArZJSCACBGABIqAAwWggALEIIACg4ACxCCAB42ggAlWoKmgqaCpoKmgqaCpoKmgqaCABkwgqaCpoKmggAPHIKmgqaCpoIAJFo=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/checker/utilities.go", "utilities.cs", "ABgqooKCgoKUpqKClKaCgoKmpoKmgqaCpoKmoqaCpoKmogAIGoKCgpSUgoKUpKSkpqKClIKmgoKmooKmoqqiAAISgoKUgoKUpqKClKaigpSmiIKUpKS4ggABEKSmgqakpoKWpKSmgqaigpSmgoKmooKUpqKooqairIKmgqaClKSmopSkpKSkpqKmgqaipoLGpIKUgpSCgoKIlIKCgpjGoqaCgoKClKaipoKmgoKUgoKUxoKmsoKUgpSClIKClIKCgpSAgqimsoKUgpSClIKCgoKElsaygpSClIKUgpaAgqaAgqbMgpKClIKYgIKkgIKkgIKmgoKChIKohIKogIKogIK2goKCloCCpIKCgoCCpICCpICCtoKChoKUgqaAguqCgoKClIKCgpSC2oCC2ICC2ICC2ICC1oCC1oKCgoKUxoCC1oCCpICC1oCCpICC1oCCpICC1oCCpICC1oCCpICC1oCC2KaEgpSmgoKCgpSClIKUgpamgoKUgpSmgoKUprKClIKUgpSCgqaCgqamsoKUgpSClKaCgpSCgqamsoKUgpSClIKCgpSUgoKAgqSkgoKAgqSkgoKAgqSkpoKmgoKClLS0tIKUlIKCgpSClIKUgoKUlIKUpoKmgqaCpoKmgqaCqIKmgqiCpoKogqaCpoKmgqaCpoKmgqaiAAsYgoKUgqaChoKClIKCpsaCpqKCgpSUpoKsopSkpKSmAAAspqKmgoKUgqaCgpSCgoKSlIKCgoKUjKiigpSClKaCpqKmooKUpoKmoqaigoKUgpSUgpSClKaipqKCgpamoqqipoLGoqakgoKUpqTGoqqClqSmoqaipqKmosaCxqKmopSkpKaigqaooqaChoKClKaigoKCpoKmpqKCpqiCqqKChIKCloKUhIKWgpS4gqaigoKClKSkgpSmpoaChuyCgoKUgpSCgpTagoaUhsaUpoKqgqaipqKmgpKCgoKClIKmAAcQAM0BjASypqKUgoLGgoLGpKSmgoKCgoKCgrimooKClJSmooKUtLS0tKSsgqaClqSmgoKCgqamooKUqKLGgqaCgriCgoKCgoKCyqaCgoKmgoKCAAgGgpSkpKSUpoKWpAAEELKCgoKCgoKmiqoAAhSygpaClIKUgpSCgpQACRyClIKClNyCgsqCygAFFKKCgoKClIKCysqCytyCgoKUpqKCgoKU", "172-180:1;312-312:1;321-321:2;985-987:1;1038-1046:1;1102-1104:1;1266-1294:1;1299-1301:1;1305-1305:1", "", "151=isConstTypeReference/1/1/1,IsAssertionExpression/1/1/2;258=IsElementAccessExpression/1/1/1,AsElementAccessExpression/1/1/1;270=HasSyntacticModifier/1/1/1,IsExportSpecifier/1/1/2,IsNamespaceExport/1/1/3;350=AsParameterDeclaration/1/1/1,getEffectiveCallArguments/1/1/2;1112=TokenValue/1/1/1,ParsePseudoBigInt/1/1/1,NewPseudoBigInt/1/1/1,pseudoBigIntToString/1/1/1;1188=IsBindableStaticNameExpression/1/1/1;1196=AsBinaryExpression/1/2/1,AsBinaryExpression/2/2/2;1309=Expression/1/1/1;1350=IsNameOfHeritageClauseTypeReference/1/1/1;1910=IsClassLike/1/1/2,GetExtendsHeritageClauseElements/1/1/3,ClassOrConstructorParameterIsDecorated/1/1/4;1911=IsPropertyAccessExpression/1/1/2,Expression/1/1/2;1962=MangleScopedPackageName/1/1/2;1973=MangleScopedPackageName/1/1/2;1990=CombinePaths/1/1/2;1995=CombinePaths/1/1/2")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("checker", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/checker")]
public static partial class checker_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    [GoLocalName("result")] internal partial struct GetResolvedSignatureForSignatureHelp_result {}
    internal partial struct accessibleChainCacheKey {}
    internal partial struct accessibleSymbolChainContext {}
    [GoLocalName("MemberInfo")] internal partial struct checkKindsOfPropertyMemberOverrides_MemberInfo {}
    internal partial struct constructorAccessibilityError {}
    internal partial struct errorState {}
    [GoLocalName("hoverStatement")] internal partial struct expandModuleDecl_hoverStatement {}
    [GoValueClone("inlineBuffer")] internal partial struct keyBuilder {}
    internal partial struct localsRecord {}
    [GoLocalName("seenName")] internal partial struct mapToTypeNodes_seenName {}
    internal partial struct moduleSpecifierResult {}
    internal partial struct nodeLinkStore<V> {}
    internal partial struct orderedSet<T> {}
    internal partial struct originalRecoveryScopeState {}
    internal partial struct primitiveTypeAliasSuggestions_type {}
    internal partial struct propertyNameNodeKind {}
    internal partial struct recoveryBoundary {}
    internal partial struct sortedSymbolNamePair {}
    internal partial struct symbolArenaLinkStore<V> {}
    internal partial struct symbolTableID {}
    internal partial struct thisAssignmentDeclarationKind {}
    internal partial struct tracedTypeAdapter {}
    internal partial struct typeFlagNamesᴛ1 {}
    internal partial struct wrappingTracker {}
    public partial interface Discriminator {}
    public partial interface Host {}
    public partial interface Program {}
    public partial interface TypeData {}
    public partial interface TypeMapperData {}
    [GoLocalName("TypeSystemEntity")] public partial interface TypeSystemEntityᴅ {}
    public partial struct AccessFlags {}
    public partial struct AliasSymbolLinks {}
    public partial struct ArrayLiteralLinks {}
    public partial struct ArrayToSingleTypeMapper {}
    public partial struct ArrayTypeMapper {}
    public partial struct AssertionLinks {}
    public partial struct AssignmentKind {}
    public partial struct AssignmentReducedKey {}
    public partial struct CacheHashKey {}
    public partial struct CachedSignatureKey {}
    public partial struct CachedTypeKey {}
    public partial struct CachedTypeKind {}
    public partial struct CallState {}
    public partial struct CheckMode {}
    public partial struct Checker {}
    public partial struct CompositeSignature {}
    public partial struct CompositeSymbolIdentity {}
    public partial struct CompositeTypeCacheIdentity {}
    public partial struct CompositeTypeMapper {}
    public partial struct ComputedNameNodeLinks {}
    public partial struct ConditionalRoot {}
    public partial struct ConditionalType {}
    public partial struct ConstrainedType {}
    public partial struct ContainingSymbolLinks {}
    public partial struct ContextFlags {}
    public partial struct ContextualInfo {}
    public partial struct DeclarationFileLinks {}
    public partial struct DeclarationLinks {}
    public partial struct DeclarationMeaning {}
    public partial struct DeclarationSpaces {}
    public partial struct DeclaredTypeLinks {}
    public partial struct DeferredSymbolLinks {}
    public partial struct DeferredTypeMapper {}
    public partial struct DiagnosticAndArguments {}
    public partial struct DiagnosticDetails {}
    public partial struct DiscriminatedContextualTypeKey {}
    public partial struct ElementFlags {}
    public partial struct EmitResolver {}
    public partial struct EnumLiteralKey {}
    public partial struct EnumMemberLinks {}
    public partial struct EnumRelationKey {}
    public partial struct ErrorChain {}
    public partial struct ErrorOutputContainer {}
    public partial struct EvolvingArrayType {}
    public partial struct ExhaustiveState {}
    public partial struct ExpandingFlags {}
    public partial struct ExportCollision {}
    public partial struct ExportTypeLinks {}
    public partial struct ExternalEmitHelpers {}
    public partial struct FeatureMapEntry {}
    public partial struct FlowLoopInfo {}
    public partial struct FlowLoopKey {}
    public partial struct FlowState {}
    public partial struct FlowType {}
    public partial struct FunctionTypeMapper {}
    public partial struct IndexFlags {}
    public partial struct IndexInfo {}
    public partial struct IndexKind {}
    public partial struct IndexType {}
    public partial struct IndexedAccessType {}
    public partial struct InferenceContext {}
    public partial struct InferenceContextInfo {}
    public partial struct InferenceFlags {}
    public partial struct InferenceInfo {}
    public partial struct InferenceKey {}
    public partial struct InferencePriority {}
    public partial struct InferenceState {}
    public partial struct InferenceTypeMapper {}
    public partial struct InheritanceInfo {}
    public partial struct InstantiationExpressionKey {}
    public partial struct InstantiationExpressionType {}
    public partial struct InterfaceType {}
    public partial struct IntersectionFlags {}
    public partial struct IntersectionState {}
    public partial struct IntersectionType {}
    public partial struct IntraExpressionInferenceSite {}
    public partial struct IntrinsicType {}
    public partial struct IntrinsicTypeKind {}
    public partial struct IterationTypeKind {}
    public partial struct IterationTypes {}
    public partial struct IterationTypesKey {}
    public partial struct IterationTypesResolver {}
    public partial struct IterationUse {}
    public partial struct JSXLinks {}
    public partial struct JsxElaborationElement {}
    public partial struct JsxElementLinks {}
    public partial struct JsxFlags {}
    public partial struct JsxNamesᴛ1 {}
    public partial struct JsxReferenceKind {}
    public partial struct LanguageFeatureMinimumTargetMap {}
    public partial struct LateBoundLinks {}
    public partial struct LiteralType {}
    public partial struct MappedSymbolLinks {}
    public partial struct MappedType {}
    public partial struct MappedTypeModifiers {}
    public partial struct MappedTypeNameTypeKind {}
    public partial struct MarkedAssignmentSymbolLinks {}
    public partial struct MemberOverrideStatus {}
    public partial struct MembersAndExportsLinks {}
    public partial struct MembersOrExportsResolutionKind {}
    public partial struct MergedTypeMapper {}
    public partial struct MinArgumentCountFlags {}
    public partial struct ModuleSymbolLinks {}
    public partial struct NarrowedTypeKey {}
    public partial struct NodeBuilder {}
    public partial struct NodeBuilderContext {}
    public partial struct NodeBuilderImpl {}
    public partial struct NodeBuilderLinks {}
    public partial struct NodeBuilderSymbolLinks {}
    public partial struct NodeCheckFlags {}
    public partial struct NodeLinks {}
    public partial struct NonExistentPropertyKey {}
    public partial struct ObjectFlags {}
    public partial struct ObjectLiteralDiscriminator {}
    public partial struct ObjectType {}
    public partial struct ParseFlags {}
    public partial struct PredicateSemantics {}
    public partial struct PropertiesTypesKey {}
    public partial struct ReactNamesᴛ1 {}
    public partial struct RecursionFlags {}
    public partial struct RecursionId {}
    public partial struct ReferenceHint {}
    public partial struct Relater {}
    public partial struct Relation {}
    public partial struct RelationComparisonResult {}
    public partial struct ReverseMappedSymbolLinks {}
    public partial struct ReverseMappedType {}
    public partial struct ReverseMappedTypeKey {}
    public partial struct SerializedTypeEntry {}
    public partial struct SharedFlow {}
    public partial struct Signature {}
    public partial struct SignatureCheckMode {}
    public partial struct SignatureFlags {}
    public partial struct SignatureId {}
    public partial struct SignatureKind {}
    public partial struct SignatureLinks {}
    public partial struct SignatureToSignatureDeclarationOptions {}
    public partial struct SimpleTypeMapper {}
    public partial struct SourceFileLinks {}
    public partial struct SpreadLinks {}
    public partial struct StringMappingKey {}
    public partial struct StringMappingType {}
    public partial struct StructuredType {}
    public partial struct SubstitutionType {}
    public partial struct SubstitutionTypeKey {}
    public partial struct SwitchStatementLinks {}
    public partial struct SymbolFormatFlags {}
    public partial struct SymbolNodeLinks {}
    public partial struct SymbolReferenceLinks {}
    public partial struct SymbolTrackerImpl {}
    public partial struct TemplateLiteralType {}
    public partial struct Ternary {}
    public partial struct Tracer {}
    public partial struct TrackedSymbolArgs {}
    public partial struct TupleElementInfo {}
    public partial struct TupleNormalizer {}
    public partial struct TupleType {}
    public partial struct Type {}
    public partial struct TypeAlias {}
    public partial struct TypeAliasLinks {}
    public partial struct TypeBase {}
    public partial struct TypeDiscriminator {}
    public partial struct TypeFacts {}
    public partial struct TypeFlags {}
    public partial struct TypeFormatFlags {}
    public partial struct TypeId {}
    public partial struct TypeMapper {}
    public partial struct TypeMapperBase {}
    public partial struct TypeMapperKind {}
    public partial struct TypeNodeLinks {}
    public partial struct TypeParameter {}
    public partial struct TypePredicate {}
    public partial struct TypePredicateKind {}
    public partial struct TypeReference {}
    [GoMemberRecord("target", GoMemberFact.Descriptor, typeof(TypeSystemEntityᴅ))] public partial struct TypeResolution {}
    public partial struct TypeSystemPropertyName {}
    public partial struct UnionOfUnionKey {}
    public partial struct UnionOrIntersectionType {}
    public partial struct UnionReduction {}
    public partial struct UnionType {}
    public partial struct UniqueESSymbolType {}
    public partial struct UnusedKind {}
    public partial struct ValueSymbolLinks {}
    public partial struct VarianceFlags {}
    public partial struct VarianceLinks {}
    public partial struct VarianceStackEntry {}
    public partial struct VerbosityContext {}
    public partial struct WideningContext {}
    public partial struct WideningKind {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸcontext() => builtin.initPackage(typeof(context_package));
    [GoInit] internal static void initᴛᴛimportꓸencodingꓸbinary() => builtin.initPackage(typeof(encoding.binary_package));
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸastnav() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.astnav_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸbinder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.binder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸevaluator() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjsnum() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸmodule() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸmodulespecifiers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸnodebuilder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸparser() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.parser_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸpseudochecker() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtracing() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtsoptions() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸzeeboꓸxxh3() => builtin.initPackage(typeof(go.github.com.zeebo.xxh3_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸmath() => builtin.initPackage(typeof(math_package));
    [GoInit] internal static void initᴛᴛimportꓸmathꓸbits() => builtin.initPackage(typeof(go.math.bits_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸsyncꓸatomic() => builtin.initPackage(typeof(go.sync.atomic_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(checker_package.ArrayToSingleTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛArrayToSingleTypeMapper;
[GoMethodHost(typeof(checker_package.ArrayTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛArrayTypeMapper;
[GoMethodHost(typeof(checker_package.CacheHashKey)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛCacheHashKey;
[GoMethodHost(typeof(checker_package.Checker)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛChecker;
[GoMethodHost(typeof(checker_package.CompositeTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛCompositeTypeMapper;
[GoMethodHost(typeof(checker_package.ConditionalType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛConditionalType;
[GoMethodHost(typeof(checker_package.ConstrainedType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛConstrainedType;
[GoMethodHost(typeof(checker_package.DeferredTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛDeferredTypeMapper;
[GoMethodHost(typeof(checker_package.EmitResolver)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛEmitResolver;
[GoMethodHost(typeof(checker_package.FlowType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛFlowType;
[GoMethodHost(typeof(checker_package.FunctionTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛFunctionTypeMapper;
[GoMethodHost(typeof(checker_package.IndexInfo)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛIndexInfo;
[GoMethodHost(typeof(checker_package.IndexType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛIndexType;
[GoMethodHost(typeof(checker_package.IndexedAccessType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛIndexedAccessType;
[GoMethodHost(typeof(checker_package.InferenceTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛInferenceTypeMapper;
[GoMethodHost(typeof(checker_package.InterfaceType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛInterfaceType;
[GoMethodHost(typeof(checker_package.IntrinsicType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛIntrinsicType;
[GoMethodHost(typeof(checker_package.IterationTypes)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛIterationTypes;
[GoMethodHost(typeof(checker_package.IterationTypesResolver)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛIterationTypesResolver;
[GoMethodHost(typeof(checker_package.LiteralType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛLiteralType;
[GoMethodHost(typeof(checker_package.MergedTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛMergedTypeMapper;
[GoMethodHost(typeof(checker_package.NodeBuilder)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛNodeBuilder;
[GoMethodHost(typeof(checker_package.NodeBuilderImpl)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛNodeBuilderImpl;
[GoMethodHost(typeof(checker_package.ObjectLiteralDiscriminator)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛObjectLiteralDiscriminator;
[GoMethodHost(typeof(checker_package.ObjectType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛObjectType;
[GoMethodHost(typeof(checker_package.Relater)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛRelater;
[GoMethodHost(typeof(checker_package.Relation)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛRelation;
[GoMethodHost(typeof(checker_package.Signature)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛSignature;
[GoMethodHost(typeof(checker_package.SignatureKind)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛSignatureKind;
[GoMethodHost(typeof(checker_package.SimpleTypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛSimpleTypeMapper;
[GoMethodHost(typeof(checker_package.StringMappingType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛStringMappingType;
[GoMethodHost(typeof(checker_package.StructuredType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛStructuredType;
[GoMethodHost(typeof(checker_package.SubstitutionType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛSubstitutionType;
[GoMethodHost(typeof(checker_package.SymbolTrackerImpl)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛSymbolTrackerImpl;
[GoMethodHost(typeof(checker_package.TemplateLiteralType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTemplateLiteralType;
[GoMethodHost(typeof(checker_package.Tracer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTracer;
[GoMethodHost(typeof(checker_package.TupleElementInfo)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTupleElementInfo;
[GoMethodHost(typeof(checker_package.TupleNormalizer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTupleNormalizer;
[GoMethodHost(typeof(checker_package.TupleType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTupleType;
[GoMethodHost(typeof(checker_package.Type)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛType;
[GoMethodHost(typeof(checker_package.TypeAlias)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeAlias;
[GoMethodHost(typeof(checker_package.TypeBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeBase;
[GoMethodHost(typeof(checker_package.TypeDiscriminator)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeDiscriminator;
[GoMethodHost(typeof(checker_package.TypeFlags)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeFlags;
[GoMethodHost(typeof(checker_package.TypeMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeMapper;
[GoMethodHost(typeof(checker_package.TypeMapperBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeMapperBase;
[GoMethodHost(typeof(checker_package.TypeParameter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeParameter;
[GoMethodHost(typeof(checker_package.TypePredicate)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypePredicate;
[GoMethodHost(typeof(checker_package.TypeReference)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛTypeReference;
[GoMethodHost(typeof(checker_package.UnionOrIntersectionType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛUnionOrIntersectionType;
[GoMethodHost(typeof(checker_package.VarianceFlags)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛVarianceFlags;
[GoMethodHost(typeof(checker_package.WideningContext)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛWideningContext;
[GoMethodHost(typeof(checker_package.keyBuilder)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛkeyBuilder;
[GoMethodHost(typeof(checker_package.nodeLinkStore<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛnodeLinkStore;
[GoMethodHost(typeof(checker_package.orderedSet<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛorderedSet;
[GoMethodHost(typeof(checker_package.recoveryBoundary)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛrecoveryBoundary;
[GoMethodHost(typeof(checker_package.symbolArenaLinkStore<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛsymbolArenaLinkStore;
[GoMethodHost(typeof(checker_package.tracedTypeAdapter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛtracedTypeAdapter;
[GoMethodHost(typeof(checker_package.wrappingTracker)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class checkerᴛwrappingTracker;
// </MethodHolders>
