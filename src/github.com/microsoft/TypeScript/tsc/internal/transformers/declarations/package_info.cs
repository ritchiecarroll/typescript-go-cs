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
global using astꓸAsteriskToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBindingElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEntityName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEnumMemberList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸHeritageClauseList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportPhaseModifierSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸImportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸKeywordExpressionSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸKeywordTypeSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModifier = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleExportName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleReference = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedExportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedImportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNodeBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸSignatureDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeLiteralNodeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package;

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
[assembly: GoTypeAlias("GetSymbolAccessibilityDiagnostic", "System.Func<go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.SymbolAccessibilityResult, go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package.SymbolAccessibilityDiagnostic>>")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<SymbolTrackerImpl, go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package.SymbolTracker>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(DeclarationTransformer))]
[assembly: GoFieldAccess(typeof(SymbolAccessibilityDiagnostic))]
[assembly: GoFieldAccess(typeof(SymbolTrackerImpl))]
[assembly: GoFieldAccess(typeof(SymbolTrackerSharedState))]
[assembly: GoFieldAccess(typeof(astꓸBindingElementList))]
[assembly: GoFieldAccess(typeof(astꓸClassElementList))]
[assembly: GoFieldAccess(typeof(astꓸDeclarationName))]
[assembly: GoFieldAccess(typeof(astꓸElementList))]
[assembly: GoFieldAccess(typeof(astꓸEnumMemberList))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸHeritageClauseElementList))]
[assembly: GoFieldAccess(typeof(astꓸHeritageClauseList))]
[assembly: GoFieldAccess(typeof(astꓸMemberName))]
[assembly: GoFieldAccess(typeof(astꓸModuleReference))]
[assembly: GoFieldAccess(typeof(astꓸNamedImportBindings))]
[assembly: GoFieldAccess(typeof(astꓸNodeBody))]
[assembly: GoFieldAccess(typeof(astꓸParameterList))]
[assembly: GoFieldAccess(typeof(astꓸStatementList))]
[assembly: GoFieldAccess(typeof(astꓸTokenNode))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationListNode))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ArrowFunction))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BindingElement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.DeclarationBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FileReference))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.GetAccessorDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HeritageClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDoc))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.LiteralTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.LocalsContainerBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModifierList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeVisitor))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.QualifiedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SetAccessorDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Symbol))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeQueryNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclarationList))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/declarations/diagnostics.go", "diagnostics.cs", "ABIkgoKCgpQABxCCgoKClIIABxCCgoKClIKClO6CgoKUlKaCgpSmgoKCgpS4ooLMgt4ABhCigsyC3gAGEKKCgoKCgoKCgpSCgoKEgoSEgpSClLqm3IKGhIK6goLchoLMgoLuuKKCAAQShoLMggAFEN6mooaCzN6C3gAIFKKW3t7egsyCAAUQ/u7YopQABhIABRAABRDegsyCAAUQAAcQ7gAGENiklKSkpKSkgoKClMampqbYgpSkpKSkpKSkpKSkpKTIgpSkpKSkpKSkpKSkpKSkpKSkpMiCpoKmgoKClIKUgpSClKaCgoKmgoKClIKClKaigoKCgoKUgoKUgpSmooKCpqKCgqaigoKCpoKmooKCpoKmgqaCgpSClKaygoKClKaCgpSUgoKUgoKUgpSCgoKmgoKClIKClIKClIKCgqaSgoKUgpSClJSkpKSkpKSkpKQ=", "19-29:1;33-44:1;48-61:1;155-179:1;181-183:2;186-199:3;203-217:4;684-700:1;702-735:2", "", "25=GetNameOfDeclaration/1/1/3;171=GetNameOfDeclaration/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/declarations/supplementalreferences.go", "supplementalreferences.cs", "ABEmogAJEKKCgpSCgpQAChimgg==", "", "", "21=SupplementalSourceFiles/1/1/2;42=NewTextRange/1/1/1,GetCurrentDirectory/1/1/6,UseCaseSensitiveFileNames/1/1/7,GetRelativePathFromFile/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/declarations/tracker.go", "tracker.cs", "ABo2gqiCqIKCgrqCgoK6goKCuKKClIKClKaCgoKUuqKClIKUgpSCuoKCgoKUzIKCgrqygoKCgoKCgsyCgoKCgoKUuoKCgriCgpSmgoKClKaCgpSClIKClJSoooKagoKUgqaChIKCrISCgoKCgpSClJSmxqIAEB6CxoKC", "72-77:1;118-118:1;119-119:2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/declarations/transform.go", "transform.cs", "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", "117-131:1;160-162:1;311-311:1;345-349:1;566-570:1;743-747:1;1268-1273:1;1381-1384:1;1470-1475:2;1511-1516:3;1579-1582:1;1714-1714:1;1961-1963:1;1993-1993:1;2019-2025:2;2184-2186:1;2275-2317:1;2542-2544:1;2826-2828:1", "", "500=GetCurrentDirectory/1/1/5,UseCaseSensitiveFileNames/1/1/6;509=NewTextRange/1/1/1;527=NewTextRange/1/1/1;544=NewTextRange/1/1/1;810=Visitor/1/2/3,Invoke/1/2/3,Visitor/2/2/4,Invoke/2/2/4;825=IsExpressionWithTypeArguments/1/1/1,Expression/1/1/1;833=Visitor/1/1/3,Factory/2/2/3,NewNodeList/1/1/3,VisitNodes/1/1/3;845=Factory/2/2/3,AsLiteralTypeNode/1/2/4,AsNode/1/1/5,AsLiteralTypeNode/2/2/5,rewriteModuleSpecifier/1/1/5,UpdateLiteralTypeNode/1/1/3,Visitor/1/1/9,VisitNodes/1/1/9;859=AsNode/1/2/2,ensureModifiers/1/1/2,Visitor/1/2/3,VisitNodes/1/1/3,AsNode/2/2/4,updateParamList/1/1/4,Visitor/2/2/5,Invoke/1/1/5;870=Visitor/1/2/2,VisitNodes/1/1/2,AsNode/1/1/3,updateParamList/1/1/3,Visitor/2/2/4,Invoke/1/1/4;916=Modifiers/1/1/2,Name/1/1/3;937=Name/1/1/2,VisitNode/1/1/2,AsNode/1/2/4,ensureType/1/1/4,AsNode/2/2/5,ensureNoInitializer/1/1/5;982=Factory/2/4/2,Factory/3/4/5,Factory/4/4/5,NewNodeList/1/1/5,NewNamedImports/1/1/5,NewImportClause/1/1/2;1029=Name/1/1/1,AsNode/1/1/3,ensureType/1/1/3;1043=AsNode/1/2/2,ensureModifiers/1/1/2,AsNode/2/2/3,updateParamList/1/1/3;1053=AsNode/1/3/2,ensureTypeParams/1/1/2,AsNode/2/3/3,updateParamList/1/1/3,AsNode/3/3/4,ensureType/1/1/4;1066=AsNode/1/3/2,ensureModifiers/1/1/2,Name/1/1/3,AsNode/2/3/5,ensureType/1/1/5,AsNode/3/3/6,ensureNoInitializer/1/1/6;1087=AsNode/1/3/2,ensureModifiers/1/1/2,Name/1/1/3,AsNode/2/3/5,ensureType/1/1/5,AsNode/3/3/6,ensureNoInitializer/1/1/6;1103=AsNode/1/3/2,ensureModifiers/1/1/2,Name/1/1/3,AsNode/2/3/5,EmitContext/1/1/5,AsNode/3/3/5,ParseNode/1/1/5,GetEffectiveDeclarationFlags/1/1/5,updateAccessorParamList/1/1/5;1120=AsNode/1/4/2,ensureModifiers/1/1/2,Name/1/1/3,AsNode/2/4/5,EmitContext/1/1/5,AsNode/3/4/5,ParseNode/1/1/5,GetEffectiveDeclarationFlags/1/1/5,updateAccessorParamList/1/1/5,AsNode/4/4/6,ensureType/1/1/6;1155=Factory/2/2/3,NewIdentifier/1/1/3;1171=AsNode/1/2/2,ensureModifiers/1/1/2,AsNode/2/2/4,updateParamList/1/1/4;1184=AsNode/1/3/2,ensureTypeParams/1/1/2,AsNode/2/3/3,updateParamList/1/1/3,AsNode/3/3/4,ensureType/1/1/4;1199=ensureModifiers/1/1/1,Name/1/1/2;1206=ensureModifiers/1/1/1,Name/1/1/2;1225=AsNode/1/4/2,ensureModifiers/1/1/2,Name/1/1/3,AsNode/2/4/5,ensureTypeParams/1/1/5,AsNode/3/4/6,updateParamList/1/1/6,AsNode/4/4/7,ensureType/1/1/7;1244=AsNode/1/4/2,ensureModifiers/1/1/2,Name/1/1/4,AsNode/2/4/6,ensureTypeParams/1/1/6,AsNode/3/4/7,updateParamList/1/1/7,AsNode/4/4/8,ensureType/1/1/8;1271=AsExportDeclaration/1/3/1,Modifiers/1/1/2,IsTypeOnly/1/1/3,AsExportDeclaration/2/3/4,ModuleSpecifier/1/1/5,rewriteModuleSpecifier/1/1/5,AsExportDeclaration/3/3/6;1402=ensureTypeParams/1/1/4,updateParamList/1/1/5,ensureType/1/1/6,Visitor/1/1/7,VisitNode/1/1/7;1412=Factory/2/4/2,Factory/3/4/2,Factory/4/4/2,Visitor/1/1/2,VisitNode/1/1/2,NewVariableDeclaration/1/1/2,NewNodeList/1/1/2,NewVariableDeclarationList/1/1/2;1430=Factory/2/4/3,Factory/3/4/3,Factory/4/4/3,NewExportSpecifier/1/1/3,NewNodeList/1/1/3,NewNamedExports/1/1/3;1498=Factory/2/4/1,NewModifierList/1/1/1,Factory/3/4/5,Factory/4/4/5,NewNodeList/1/1/5,NewModuleBlock/1/1/5;1525=AsClassDeclaration/1/5/1,Factory/2/2/2,NewModifierList/1/1/2,AsClassDeclaration/2/5/3,Name/1/1/3,AsClassDeclaration/3/5/4,AsClassDeclaration/4/5/5,AsClassDeclaration/5/5/6;1636=Factory/2/4/1,NewModifierList/1/1/1,Factory/3/4/5,Factory/4/4/5,NewNodeList/1/1/5,NewModuleBlock/1/1/5;1896=AsNode/1/1/2,ensureModifiers/1/1/2,Name/1/1/3,Visitor/1/2/4,VisitNodes/1/1/4,Visitor/2/2/5,Invoke/1/1/5;1907=AsNode/1/1/2,ensureModifiers/1/1/2,Name/1/1/3,Visitor/1/3/4,VisitNodes/1/3/4,Visitor/2/3/5,VisitNodes/2/3/5,Visitor/3/3/6,VisitNodes/3/3/6;1922=AsNode/1/4/2,ensureModifiers/1/1/2,Name/1/1/4,AsNode/2/4/5,ensureTypeParams/1/1/5,AsNode/3/4/6,updateParamList/1/1/6,AsNode/4/4/7,ensureType/1/1/7;1971=Name/1/1/4;1985=Name/1/1/4;1993=Name/1/1/4;2035=ensureModifiers/1/1/1,Name/1/1/2,QuestionToken/1/1/3,ensureType/1/1/4,ensureNoInitializer/1/1/5;2055=EmitContext/1/1/1;2120=EmitContext/1/1/3,Expression/1/1/3,AsNode/1/1/3,CreateTypeOfExpression/1/1/3;2129=Factory/2/3/2,Factory/3/3/2,NewNodeList/1/1/2,NewVariableDeclarationList/1/1/2;2132=AsHeritageClause/1/2/1,AsHeritageClause/2/2/2,Factory/2/3/3,Factory/3/3/4,AsExpressionWithTypeArguments/1/2/5,Visitor/1/1/7,AsExpressionWithTypeArguments/2/2/7,VisitNodes/1/1/7,UpdateExpressionWithTypeArguments/1/1/4,NewNodeList/1/1/3;2150=Factory/2/2/2,Name/1/1/5,UpdateClassDeclaration/1/1/2;2162=Name/1/1/3,Visitor/1/1/5,VisitNodes/1/1/5;2226=ensureType/1/1/4;2306=ensureModifiers/1/1/1,Name/1/1/2,ensureType/1/1/4;2380=AsNode/1/1/2,ensureModifiers/1/1/2,Name/1/1/3,Factory/2/2/4,MapNonNil/1/1/4,NewNodeList/1/1/4;2391=Name/1/1/2,IsComputedPropertyName/1/1/2;2409=Factory/2/2/2,String/1/1/2,NewNumericLiteral/1/1/2;2534=Name/1/1/4,VisitNode/1/1/4,AsNode/1/2/6,ensureType/1/1/6,AsNode/2/2/7,ensureNoInitializer/1/1/7;2589=Modifiers/1/1/2,Name/1/1/4,Factory/2/2/5,AsExternalModuleReference/1/1/5,AsNode/1/1/5,rewriteModuleSpecifier/1/1/5,UpdateExternalModuleReference/1/1/5;2609=Modifiers/1/1/2,AsNode/1/1/4,rewriteModuleSpecifier/1/1/4;2628=Modifiers/1/1/2,Factory/2/2/3,AsImportClause/1/1/4,UpdateImportClause/1/1/3,AsNode/1/1/9,rewriteModuleSpecifier/1/1/9;2647=Modifiers/1/1/2,Factory/2/2/3,AsImportClause/1/1/4,UpdateImportClause/1/1/3,AsNode/1/1/9,rewriteModuleSpecifier/1/1/9;2658=AsImportClause/1/1/1,Elements/1/1/1;2664=AsImportClause/1/1/1,AsNamedImports/1/1/1,Factory/2/2/2,NewNodeList/1/1/2;2668=Modifiers/1/1/2,Factory/2/2/3,AsImportClause/1/1/4,UpdateImportClause/1/1/3,AsNode/1/1/9,rewriteModuleSpecifier/1/1/9;2683=Modifiers/1/1/2,AsNode/1/1/4,rewriteModuleSpecifier/1/1/4;2711=Visitor/1/2/2,Invoke/1/2/2,Visitor/2/2/4,Invoke/2/2/4;2730=Visitor/1/1/1,Invoke/1/1/1,Factory/3/4/2,Factory/4/4/2,NewKeywordExpression/1/1/2,NewLiteralTypeNode/1/1/2;2755=Visitor/1/1/1,Invoke/1/1/1,Factory/3/3/2,NewKeywordTypeNode/1/1/2;2986=Factory/1/4/1,Factory/2/4/3,Factory/3/4/4,Factory/4/4/5,AsNode/1/1/5,ensureType/1/1/5,NewVariableDeclaration/1/1/5,NewNodeList/1/1/4,NewVariableDeclarationList/1/1/3,NewVariableStatement/1/1/1;2997=Factory/3/3/2,NewExportSpecifier/1/1/2;3124=Factory/2/3/5,Factory/3/3/5,NewNodeList/1/1/5,NewModuleBlock/1/1/5", "", "351:21,352:13,1479:37,1480:29,1670:21,1671:13,2086:21,2087:13,2280:21,2281:13,2982:21,2983:13")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/declarations/util.go", "util.cs", "AAkSgqailqakpoIAAjSCgoKmprKCAAEQpqykpKaigpiClISCgqaUuIIAAhiCgpSmgoKGlIKUpqKClKaCqoKClKqCAAASpoKmooKU", "159-161:1", "", "29=IsPropertyDeclaration/1/1/1,IsPropertySignatureDeclaration/1/1/2,IsBindingElement/1/1/3,IsSetAccessorDeclaration/1/1/4,IsGetAccessorDeclaration/1/1/5,IsConstructSignatureDeclaration/1/1/6,IsCallSignatureDeclaration/1/1/7,IsMethodDeclaration/1/1/8,IsMethodSignatureDeclaration/1/1/9,IsFunctionDeclaration/1/1/10,IsParameterDeclaration/1/1/11,IsTypeParameterDeclaration/1/1/12,IsExpressionWithTypeArguments/1/1/13,IsImportEqualsDeclaration/1/1/14,IsTypeAliasDeclaration/1/1/15,IsJSTypeAliasDeclaration/1/1/16,IsConstructorDeclaration/1/1/17,IsIndexSignatureDeclaration/1/1/18,IsPropertyAccessExpression/1/1/19,IsElementAccessExpression/1/1/20,IsBinaryExpression/1/1/21,IsCallExpression/1/1/22;85=IsTypeAliasDeclaration/1/1/1,IsJSTypeAliasDeclaration/1/1/2,IsModuleDeclaration/1/1/3,IsClassDeclaration/1/1/4,IsInterfaceDeclaration/1/1/5,IsFunctionLike/1/1/6,IsIndexSignatureDeclaration/1/1/7,IsMappedTypeNode/1/1/8,IsVariableDeclaration/1/1/9")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

[GoPackage("declarations", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/transformers/declarations")]
public static partial class declarations_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct thisPropertyAssignmentKey {}
    public partial interface DeclarationEmitHost {}
    public partial interface OutputPaths {}
    public partial struct DeclarationTransformer {}
    public partial struct ReferencedFilePair {}
    public partial struct SupplementalReferencesTransformer {}
    public partial struct SymbolAccessibilityDiagnostic {}
    public partial struct SymbolTrackerImpl {}
    public partial struct SymbolTrackerSharedState {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸchecker() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.checker_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjsnum() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸmodulespecifiers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸnodebuilder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(declarations_package.DeclarationTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class declarationsᴛDeclarationTransformer;
[GoMethodHost(typeof(declarations_package.SupplementalReferencesTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class declarationsᴛSupplementalReferencesTransformer;
[GoMethodHost(typeof(declarations_package.SymbolTrackerImpl)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class declarationsᴛSymbolTrackerImpl;
[GoMethodHost(typeof(declarations_package.SymbolTrackerSharedState)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class declarationsᴛSymbolTrackerSharedState;
// </MethodHolders>
