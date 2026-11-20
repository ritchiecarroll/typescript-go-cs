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
global using astꓸAsteriskToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBinaryOperatorToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBindingElementNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockOrExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCaseOrDefaultClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCatchClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸColonToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸConciseBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEntityName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEnumMemberNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEqualsGreaterThanToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExportSpecifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸForInitializer = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributeName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportSpecifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributeLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributeName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributeValue = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxChild = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxTagNameExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸKeywordExpressionSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸLiteralLikeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModifierLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleExportName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleReference = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedExportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedImportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸObjectLiteralElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸPrivateIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸSignatureDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸStringLiteralNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateLiteral = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateLiteralTypeSpanNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateMiddleOrTail = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateSpanNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeArgumentList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypePredicateParameterName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;

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
[assembly: GoDynamicTypeLift("696e746572666163657b456e64282920696e747d", "tryGetEnd_node")]
[assembly: GoDynamicTypeLift("696e746572666163657b47726f77286e20696e74297d", "Write_type")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<ChangeTrackerWriter, EmitTextWriter>(Pointer = true)]
[assembly: GoImplement<NodeFactory, go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeFactoryCoercible>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModifierList, tryGetEnd_node>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node, triviaPositionKey>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node, tryGetEnd_node>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList, triviaPositionKey>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile, go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package.Source>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.core_package.TextRange, tryGetEnd_node>]
[assembly: GoImplement<singleLineStringWriter, EmitTextWriter>(Pointer = true)]
[assembly: GoImplement<textWriter, EmitTextWriter>(Pointer = true)]
[assembly: GoImplement<trailingSemicolonDeferringWriter, EmitTextWriter>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(AutoGenerateInfo))]
[assembly: GoFieldAccess(typeof(ChangeTrackerWriter))]
[assembly: GoFieldAccess(typeof(EmitContext))]
[assembly: GoFieldAccess(typeof(EmitHelper))]
[assembly: GoFieldAccess(typeof(NameGenerator))]
[assembly: GoFieldAccess(typeof(Printer))]
[assembly: GoFieldAccess(typeof(astꓸBinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸMemberName))]
[assembly: GoFieldAccess(typeof(astꓸQuestionDotToken))]
[assembly: GoFieldAccess(typeof(astꓸStatement))]
[assembly: GoFieldAccess(typeof(astꓸStatementList))]
[assembly: GoFieldAccess(typeof(astꓸTokenNode))]
[assembly: GoFieldAccess(typeof(astꓸTypeElementList))]
[assembly: GoFieldAccess(typeof(astꓸTypeNode))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(emitNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Block))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CallExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CaseBlock))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CatchClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ElementAccessExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FileReference))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.GetAccessorDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IfStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.InferTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.InterfaceDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IntersectionTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.LocalsContainerBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModifierList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NonNullExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NumericLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PostfixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PropertyAccessExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.QualifiedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SetAccessorDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.StringLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SwitchStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Symbol))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TaggedTemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateLiteralLikeNodeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateLiteralTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TryStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.UnionTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclarationList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableStatement))]
[assembly: GoFieldAccess(typeof(nameGenerationScope))]
[assembly: GoFieldAccess(typeof(singleLineStringWriter))]
[assembly: GoFieldAccess(typeof(textWriter))]
[assembly: GoFieldAccess(typeof(varScope))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/changetrackerwriter.go", "changetrackerwriter.cs", "ABkwhIKU3ILGgpSCuIK4griCuIK4gtyCpoKmgqaCpoKChIKCgoKUpriiggAHEIKGlLimuIKUioKClJKClIKmqIKClISWgoKmgqaigqaigpSCooKmooKmooKmooKmooKmooKmooKmooKUgoKCkoCAooKigqaigpSCgoKClJSC", "41-45:1;46-50:2;51-55:3;56-60:4;61-65:5;66-70:6;110-110:1;116-124:2;146-149:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/emitcontext.go", "emitcontext.cs", "ACtagoKCpsyCgpKC2ILKoqaCpoKCgoKCzKIACCSCgqyCgoKClIKCgoKUgpSosoKCpoKCgqaogoKmoqyCgoKCgoK+goKCAAISgq6CgoKCgoKClKiygoKmgoKCpqiCgqiiqIKCgoKosoKCgpSogoKmgoIAAkCWgoKWgoKCgoKmloKCqIKCqIKCqIKCgpSCgoKUgoKCgtymgqaCxqLGogACFIKCgpSogoKUqIKClKaigoKChIKClIKCppQAGTiCpoKmooKmgqaCgoKClIKCgr6CrqKCgoKCpqyigoKUpqKClIIAKmCigoKCgoKCgoKC2IKClKaCpoKmgoKUpqKoooKUqIKCgqiCqKKClKiCgoKogqiCgoKCgoKogoKCpqiCgoKUpoKmgqaCgpSmgqaigpSmooKUgpSmooKCpqKCgriCgoKUgoKmgoKCgoKCgoK4goK4goKClKaCgoKmpoKCgqaCpoKCgpSmogACFLKCprKCgoKCAAEQgpSEpqKClIKCgoKCgpSmgoKClKamgoKCgoKUpqKCggAIFpQABxIACRaygoKCqIKCgoKCugAJFoKCgpSCpqKClIKCgoKCgqa0goKCpoKmgoKC3t6CgqaCgoKmgoKCgoKClIKmprKClIKCgoKCgpSmgoKmgoKmgoKUpoKCpoKCpoKClKqiqIKClKaigoKCgg==", "59-62:1", "", "321=IsVariableStatement/1/1/1,AsVariableStatement/1/1/2,AsVariableDeclarationList/1/1/2,Every/1/1/2;732=Expression/1/3/1,IsIdentifier/1/1/1,Expression/2/3/2,EmitFlags/1/1/2,Expression/3/3/3,Text/1/1/3;803=AsNode/1/2/2,NewGeneratedNameForNode/1/2/2,NewVoidZeroExpression/1/1/3,NewStrictEqualityExpression/1/1/1,NewToken/1/2/5,NewToken/2/2/7,AsNode/2/2/8,NewGeneratedNameForNode/2/2/8;814=Name/1/1/3,NewVariableDeclaration/1/1/2,NewNodeList/1/1/2,NewVariableDeclarationList/1/1/2;822=Modifiers/1/1/2,AsNode/1/1/4,NewGeneratedNameForNode/1/1/4;847=Clone/1/1/1,NewTypeCheck/1/1/1;851=Modifiers/1/1/2,Name/1/1/4;902=AsBlock/1/2/1,StatementList/1/1/2,MergeEnvironmentList/1/1/2,AsBlock/2/2/3;908=AsBlock/1/2/1,StatementList/1/1/2,MergeEnvironmentList/1/1/2,AsBlock/2/2/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/factory.go", "factory.cs", "ABMkggALFKKUgpS0tLSmgu6ClIKsgqyCqIKogqiCqIKogqiCgqamopSClLS0tIKCpoLugpSCqIKogqiCqIKCpqqiggABGLSCgpSCroKmgqaCroKmgqaCpoKugqaCroLGooKClJSmgoKClMiCgpSClIKCgpSuooKCgoSCgoKUgoSCpqKClIKClAAFEKKCgt4ABRCClIKCgoKmgoKCgpS4pIIABhAABxKCpoKClIKmgoKClAACHoIAAhCilKSkpKSkpKTYooK6qIKCgpSmgoKogoKCpqiCgoKmABEkooKCgpS4goKClIKUgqasgqyCroKugqiCqIKmsoKUgoKClIKUroKCgpQAAhCCgoKqgpSCgoKCgoK4AAcSgpQAChiCgt6CqoKCAAcSgoIAEiiCgoKClJQABxKCgoKClJQABxKCggAHFIIACx6CAAseggALHoKCgoKoggAHEgAHFIKogoKCqoKqgoKCgoKClIKCgoKChAAGELiCggAKHoKCAAcUiIKWlIKClKYACxyCgoIABxSCggAHGIyUgoKUpgAJGJSCgpSmgoKUpgAMIILKpoiCgpSmlAAGEKaIgoKUpoKClIKUAAYQpoqCgpSmgpQABhAAAhqMlpaCqIKmpgAAFoKClKiUgoKUpoKClKYABxCmgoIABxKCgoKClJQABxaCggAHEoKCAAcSgoKCgpSUAAcYgoIABxSCggAHFIKCAAcSggALGoQABhKCgoKClJQ=", "", "", "21=NewNodeFactory/1/1/1;303=AsVariableDeclaration/1/1/1,Name/1/1/2;309=AsVariableDeclarationList/1/2/3,NewNodeList/1/1/4,AsVariableDeclarationList/2/2/5,UpdateVariableDeclarationList/1/1/2;339=NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/4;346=NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/4;375=NodeIsSynthesized/1/1/1,SourceMapRange/1/1/2,RangeIsSynthesized/1/2/2,CommentRange/1/1/3,RangeIsSynthesized/2/2/3;414=Expression/1/1/2,RestoreOuterExpressions/1/1/2;563=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;574=NewUnscopedHelperName/1/1/1,NewStringLiteral/1/1/5,NewNodeList/1/1/4;587=NewUnscopedHelperName/1/1/1,Itoa/1/1/4,NewNumericLiteral/1/1/4,NewNodeList/1/1/4;599=NewUnscopedHelperName/1/1/1,IfElse/1/1/4,NewKeywordExpression/1/1/4,NewNodeList/1/1/4;609=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;636=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;652=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;662=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;671=NewIdentifier/1/2/2,NewIdentifier/2/2/4,NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/9;684=NewIdentifier/1/2/2,NewIdentifier/2/2/4,NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/9;697=NewIdentifier/1/2/2,NewIdentifier/2/2/4,NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/9;717=NewNodeList/1/2/3,NewToken/1/1/6,NewNodeList/2/2/7,NewBlock/1/1/7;725=NewParenthesizedExpression/1/1/1,NewNodeList/1/1/4;761=NewTypeCheck/1/1/1,NewToken/1/3/2,NewToken/2/3/4,NewToken/3/3/5,NewStringLiteral/1/1/5,NewBinaryExpression/1/1/5;774=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;787=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;808=NewUnscopedHelperName/1/1/1,NewIdentifier/1/1/6,NewNodeList/1/1/4;823=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;833=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;851=NewToken/1/1/2;877=NewUnscopedHelperName/1/1/1,NewVoidZeroExpression/1/1/7,NewNodeList/1/1/4;891=NewIdentifier/1/3/1,NewStringLiteral/1/1/1,NewPropertyAssignment/1/3/1,NewIdentifier/2/3/2,NewPropertyAssignment/2/3/2,NewIdentifier/3/3/3,NewPropertyAssignment/3/3/3;909=NewNodeList/1/1/2,NewToken/1/1/4;934=NewNodeList/1/1/2,NewToken/1/1/4;955=NewNodeList/1/1/2,NewToken/1/1/4;1005=NewIdentifier/1/6/1,NewStringLiteral/1/1/1,NewPropertyAssignment/1/6/1,NewIdentifier/2/6/2,NewPropertyAssignment/2/6/2,NewIdentifier/3/6/3,NewPropertyAssignment/3/6/3,NewIdentifier/4/6/4,NewPropertyAssignment/4/6/4,NewIdentifier/5/6/5,NewPropertyAssignment/5/6/5,NewIdentifier/6/6/6,NewPropertyAssignment/6/6/6;1018=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1034=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1044=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1054=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1070=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1080=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1090=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1100=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4;1109=NewIdentifier/1/1/2,NewParameterDeclaration/1/1/5,NewNodeList/1/2/4,NewExpressionStatement/1/1/10,NewNodeList/2/2/9,NewBlock/1/1/9;1122=NewParenthesizedExpression/1/1/1,NewIdentifier/1/1/3;1137=NewUnscopedHelperName/1/1/1,NewNodeList/1/1/4")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/generatedidentifierflags.go", "generatedidentifierflags.cs", "ABUugqaCpoKmgqaCpoKmgqaCpoI=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/helpers.go", "helpers.cs", "ABIisoKUgpSClIKUqgAOGgALFAALGAAdOAAlrAEADHwADTAACxjuABQmAA0YAA4cABIiABEgAAUQAAUaACNEAA8gAAwWAAoUAAsYABMkAA0YABoyAAoSAAwW")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/namegenerator.go", "namegenerator.cs", "ACdSgoKCuIKClIK4gqaCgoKUpoKCgpSogoKCgqaogoKClIKUpqKCgpSCgoK8ooKChpaClIKUgoK4poKCgoKmgqaCgqaylKSClKSClKSClIKClKSClKSClKSkpMiyhIKUuIKCgoKUpoKmgqaigpSmsoKClKSCpAALGKyCgoKCgpaCgpSmgoKCgoKClJS4goKEgoKClJSCgoKClJQABRaCgoKCgoK6gqaCgoKCgpS4oqaCgpTYooKClKaCgoKChIKmlMaiqIKWggACFIKClJQ=", "", "", "257=Text/1/1/1,IsFileLevel/1/1/2,IfElse/1/1/2,IsOptimistic/1/1/3,IsReservedInNestedScopes/1/1/4,IsPrivateIdentifier/1/1/5;394=isReservedName/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/printer.go", "printer.cs", "AGfaAqLMgpSCkpKWgoKCgsaigoKClKS0tpSkpMqClIKUqLKCpoKCuJSYggAEEqS0ABIqgpS0tLS0tLS0tLTYgqaCgoKmgoKUuIKmgqaCpoKmgqaCpoKmgqaCpoKCuIKCgqKClIKCyoKmgqaCpoKCuIKCuKKCgoKCgpSmuIKCgoKCuKKCgoKCpqaigoKCytKCpoKCloKmgoIABhampoaCAAEQggAAEJSmwoKCpoKUAAAglISUiIIABhaUgqamwoKClISCgoIABRSMmIKClIKUpsKCgpSClIKCggAGFpSCpoKUpoKCpoKCpoKCgoKCgoKCgoKClKaEgqgAASCCgoKWloKCqLqCgoKClIKClJSCgoKCqKaoAAMQgqqCqoKmgqaCpoKmAAMSgqaCpoKmhKaCgoKmpqaCgpSmgqyKqoKmgqaCpqKCppiqgoKmgpSClKaCpqKCppQAAiSoqKSkpq6GgoKClLiCpoKCgoKmgqaigqaCgqaCpqKCpoKCpoKmooKmlLS0AAUkpIKUgqYAARyqooKCpqKCgqaigoKmooKCpqKCgq6igoKmooKCpqKCgqailLQABRCilLTYsoKuooKClIKCgqamooKCpqKClKaigoKCgoKUprKEhMyCgpSEgoK4goKmsoaCgqaCgqaigoKmooKCprKCgoKCprKCgoKCpqKUtLi02KKCppS0tLTYsoKmgpSUtLS0tLS0tLamooKmlLS02KKCppS0tNiigqaUtLTYopS0tNiigqaUtLQACxDCgqaEgoKCqJSCmr6CgoKUgoKCgoKUlIKCgpSmgoKUgpSCAAcSgoKmgrimsoKCgoKCgoKUgoKCgpSmpoKUuKKCgoKmsoKCgoKUmIKmoqaygoKCprKUtLTYgoKUpqKCpoKCpqKCpoKCgqaCgsa0gqaClAAEGoKCgpS4goKmopyWgqaynIKmmJSCgoKClIKCgpSmgpiUgriygoKmgq6ygoKCgoKCprKCgoKCgoKCprKCgoKCgoKCgoKCgqaygoKCgoKCgoKCgoKCprKCgoKCgqaygoKCgoKCgoKCgqaygoKCgoKCgoKCgoKCpoKmgqaigoKCgoKCgoKmooKCgoKCgoKCgoKmsoKCgoKCgoKCgoKmspS0tLS0tLS0tLS02LKUtLS0tLS0tLTYopS0tLS0tLQABRCCprKUtLTYsoKCgpSCgoKCgpSmgqaCgpSmsoKCgqiygpSCgoyUuLKCgoKEgoKCgoKCprKCgoKCgoKEgoKCgoKCprKCgoKCgqaygoKCgoKCgoKmsoKCgoIAAhSigoKUpoKmsoKCgoKCprKCgoKmsoSCgqaygoKCgoKCgqaCprKCgqaCprKCgqaygoKCgoKCgoKCgoKCgoKmsoKCgoKCgpSmsoKCgoKmsoKCgoKmgoKCprKCgoKCprKCgoKCgqaygoKCgoKCprKCgoKClIKUgoKClJSCgoKCgoKUgoKCgqaCgoKUgoKCgpSUpoKUgpSCprKCgqaygoKCpqKmsoKCguaygoKCgoKCgoKCprKCgoKUgoKCgoKClIKCgpSCqKKCgoKoooKCgqiCprKEpoKCgoKmAAEcuLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0uLa2tLS0tLa2gqausoKCgoKmsoKCgoKmsoKCgoKCloKClKaipoKmsoKCgqaygoKCprKCgoKmsoKCgq6CpoKmsoKCprKCgoKCgoaCgqqigoSGmqaygoKCgoKClIKCgoiClIKUlIKCgoKCgqaygoKCgoKCpoKmsoKCgoKCgoKElLiygoKCgoKmsoKCgoSUlIKCpqKUtLTYsoKCgoKCprKCgoKCgqaygoKCgoKCgoKUgqaygoKCgoKCgoKCgoKCgoKmspS6goK0tNiygoKCgoKCgoKCgoKCgoKmsoKCgoKmsoKCgoKmsoKCgoKmsoKCgoKmsoKCggABHIKChLiCprKCgoKuopSCpoKMgoKCqLimwoKCgpq2grS0urq6tLS0tIKCjqa0iJS2tLSmsoKClIKUgoKCgoKCgoKCgqaCgpS4soKCgoKCgoKCgoKCgoKCgoKCgqaygoKCprKCgoKCgpSmsoKCgqaygpSClIKCpoKUgoKCgoKCgoKUgqaCprKCgoKmsoKCgoKCgqaygoKCgoKCprKCgoKmsoKCgoIACwa0ioKCgoKUgoKUppaCgoKClIK4gqaCprKClIKCgoKCpoKCgqaClIKUgoKCgoK4lKqygpSCgoKEgoKClJSCyoIABxCCAAcQggAIEoIACBKC3IIACBKCAAgSgtyC3ILupqKCprKCgqaWtLi0tLS0uLS4tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0uLS0uLiktLa2ggADELKCgoKmoqaCgoKusqiygoKUppSCprKCgoKCpoKYgpSUprKUhIKIlJS03IamAAIYpIKEgoKCgoKCprKCgoKCgoKCgoKCgoKUpqaygoKCgqaygoKCgpSmgoKmsoKCgqaigpS4soKCgoKCgpSCgoKClIKCgoKUgoKmsoKCgoKCgoKCgoKCprKCgoKCgpSCgoKCgoKCgqaygoKCgpSCprKCgoKClIKmsoKCgoKUgqaygoKCgoKCgqaygoKCgoKCgoKmsoKCnoKUprKCgoKCgqaygoKCgoKClIKCgoKUpoKCgoKmgqaCrrKCgoKCgqaiprKClLS0tIKCtLSCgqaygoKCgoKCgpSCgoKCgoKCprKCgoKCgoKUgoKCgoKCgoKCgoKCprKCgoKCgoKCgoKCgoKCgqaygoKCgoKCgoKCgoKmsoKCgoKCgoKCguaygoKCgpSCgoKCgoKUgoKCgpSClIKUprKCgoKmgoKmsoKCgoKmsoKCgoKCgpSCgoKCgoKmopS0tLTYsoKCgoKCgoKClIKCgpSCprKCgoKUgoKCgqaCprKCgoKCgoKmsoKCgoKmooKUlLS02LKCgoKUgoKCgpSCpqKmsoKCgoKUlIKCloKClKaCprKCgoKCgoKUgpSUgoKCgpSCgpSCprKCgoKCprKCgoKCgoKClIKmoqaygoKCgoKCgoKCprKCgoKCgoKmsoKCgoKmopS0tNiygoKClIKCgoKUgqaiprKGgpSCgoKUlLiygoKmlrS0tLS0tLS0tLS0tLS0tLS0tLS4tLS0tLS0uLS0tLS2AAUQsoKCgoKCrrKCgoKCprKCgoKCgoKCprKCgoKCprKCgoKCgoKUgoKCgqaygoKCgqaCgoKCpoKCgoKmooSCprKCgqaygoKCgpSmsoKCgoKmopS0tNiygoKCgoKCgpSClKaygoKCgqaylLS0tLS02LKUtLS0tNiilLS02KKUtLS0tLQABRCynoKGgoKUpqaygoKCgoKmooKCgqailLS02LKCgoKCgqailLS02KKmsoKClIKCgoKmgq6ygoKCAAAQgoKClIKmsoKCgoKCgpSmsoKCgpSusoKCgqairoSuooKUgoKCuLKCgoKUpqaCgoKCgoKCgoiCpoKUlLimsoKClJSClIKCgoKCgpSCgoKmqAAGEIKCgqaigoL2goKCgpSCAAMQooKmpsKUgoKmgqaCpoKmgoKClIKUpoKCgriCpoSCgoKmlKaCpoKClLi4gqaChKaEqIKUtLSUtAAGGpS05pTmlOa0uIKmpoKUtLSCtIKCtILeAAISgpSCgpSCgpSCgqiCppaCgoSEgoKOgoK4loKGgoKmgoKmgqaCgrqCgpSmgpSCgqaogoKCgpQAAxSCgoKUlKiCqIKCgpSCAAMQpIKmgpSCpoKmgoKCgoKClIIACwzSgoKCgoKCgpSCgoKClIKClIKCgoK4lrS0uLi6tLi0tLi0tLS0tLS0tLS0uLS0uLS4tLS0tLS0tLS0tLS0uLi0tLS0tLS0tLS4tLS0uLS0uLi4tpS0tLS0tLTogoKCgoKCgq6CgqaCgoKCloKCgqaCgqaigqaCgoKCloKmgpaCuKKGgpSmgoKmgoKCpoKCgoKCpqaigqaCgriCgqaCgoKClIKmgqamooKmgpSCgoLKooKWhoKYgqaEpoSYgtyigoKChIKCmILKgoKUgoK4goKUgoKUuIKClIKCuIKClIKC2IKClMaCgoKClKaEgqaCgoKUAAASlKiCgriCgoK4gqimgpSkpMiEqoKCpqaCgpaCpoKCgrqmgoKUgqaCgpSCpoKCgpSClIKClKaClILGtOqCgpSCuNKCpoKUgoyCgoLMpoKCgoKCgpSIuIKmiIKCiIKCgriCgqaUgrgADBaCgoKmgqaCgoKmlIKUuIKmpoKCgqaCAAIQgoKmgoKCgqaCgqaCgoK4gqaCgqaCgoKCgoKClJSCgoKCgoKCgoKCgoK4goDeAAQwgoKCgoKCgoKClAADJIKCpoKUiKaCpoKCpqKCpoKUgqaGuIKCpoKCgpSClIKmgoKmooKmgoKCgoKUggAEEoKmgqaCpqKClIK4soKmlLS0grSCtLS0goK0grS0tLSCgoLGtLSCtLS0goKU6qKClIK4ooKUAAES2IKCgoKCzIKogoKUAAMQgpSCpoKCpoKClIK4gpSCpoKUgpSmgoKUggAOHIKCgoKmgoIATboBgpSkpKSk2IKUpKSkpA==", "184-184:1;186-188:2;466-473:1;545-552:1;575-582:1;616-623:1", "", "480=Pos/1/1/1,PositionIsSynthesized/1/1/1,NodeIsSynthesized/1/1/2;643=IsSourceFile/1/1/2;647=Text/1/2/1,isJSDocLikeText/1/1/1,Text/2/2/2,IsPinnedComment/1/1/2;682=AsNode/2/2/1,LastOrNil/1/1/1,getClosingLineTerminatorCount/1/1/1;706=IsSourceFile/1/1/2,IsInJsonFile/1/1/3;710=shouldEmitSourceMaps/1/1/1;734=IsPrologueDirective/1/1/1,NodeIsSynthesized/1/1/2;1032=AsNode/1/1/1,EmitFlags/1/1/1;1034=Clone/1/2/1,Clone/2/2/3;1059=AsNode/1/1/1,EmitFlags/1/1/1;1378=IfElse/1/1/4;1542=Modifiers/1/3/3,Modifiers/2/3/3,HasTrailingComma/1/1/4,Modifiers/3/3/5,Name/1/1/10,IsIdentifier/1/1/10;2698=IfElse/1/2/1,AsNode/2/2/2,shouldAllowTrailingComma/1/1/2,IfElse/2/2/2;2710=TokenToString/1/1/1,Contains/1/3/1,Contains/2/3/2,Contains/3/3/3;2730=mayNeedDotDotForPropertyAccess/1/1/1,HasTrailingComment/1/1/2,HasTrailingWhitespace/1/1/3;3410=parenthesizeExpressionForNoAsi/1/1/2;3416=parenthesizeExpressionForNoAsi/1/1/2,Name/1/1/4;3425=parenthesizeExpressionForNoAsi/1/1/2;3434=parenthesizeExpressionForNoAsi/1/1/2;3444=parenthesizeExpressionForNoAsi/1/1/2;3454=parenthesizeExpressionForNoAsi/1/1/2;3461=Modifiers/1/1/2,parenthesizeExpressionForNoAsi/1/1/3;3471=parenthesizeExpressionForNoAsi/1/1/2;3481=parenthesizeExpressionForNoAsi/1/1/2;3488=parenthesizeExpressionForNoAsi/1/1/2;3495=parenthesizeExpressionForNoAsi/1/1/2;3745=rangeEndIsOnSameLineAsRangeStart/1/1/1;3810=NodeIsSynthesized/1/1/2;4664=shouldEmitOnSingleLine/1/1/1,getLeadingLineTerminatorCount/1/1/2;5120=AsNode/1/1/3,NodeIsSynthesized/1/2/3,NodeIsSynthesized/2/2/4,RangeStartPositionsAreOnSameLine/1/1/5;5377=AsNode/1/1/2;6371=ECMALineMap/1/2/2,ComputeLineOfPosition/1/2/2,ECMALineMap/2/2/2,ComputeLineOfPosition/2/2/2;6578=Text/1/1/1,IsRecognizedTripleSlashComment/1/1/1;6643=GetLine/1/1/1,GetColumn/1/1/2;6677=Pos/1/1/3,PositionIsSynthesized/1/1/3;6704=End/1/1/2,PositionIsSynthesized/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/semicolon_writer.go", "semicolon_writer.cs", "AA4agsaCgoK4goKmgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCgqaCpoKCpoKCpoKmgqaCpoKmgqaCpoI=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/singlelinestringwriter.go", "singlelinestringwriter.cs", "AA4azJSCgoKSAAoSgoKmqIKmgqaCpoKmgqaCpoKClIKClKaogqaigqaigqaigqaigqaigqaigqaigqaigqaigqaigqaigqaigqaigqaigqaigg==", "24-26:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/syntheticfile.go", "syntheticfile.cs", "AAoisoIACRaCgoKssoKCgoLMgoI=", "", "", "16=GetNewLineKind/1/1/2,GetPrintHandlers/1/1/7,Write/1/1/9")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/textwriter.go", "textwriter.cs", "AA4YAA8cgqaCpoKqgoKYpoKmgqaCpoKmgqaCgpSCgpSmgqaCpqKCgoKUpoKClIKCpoKCgoKClOyCpoKClsaigoKClIKCuKKClKaigpSmgqaigoKCgoKmooK4ooK4gqaCpoKmgqaCpoKmgqaCpoLGgoKUgoKCgg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/printer/utilities.go", "utilities.cs", "ACJIygAQIoKCgoKmgoKCgpSsgoKCgpSCgoKKAAIQlILGgsa0hMaCyIKEppSCgoKUyIaCgoSCgoKCgoKGlqaClNimpoK4goKCgqaCgoKCpoKCgoKmqIKmgoSCmIIAAxCmpoKqlIKCgpSmloKWloIAAhKCgoKUgoKUqJSCtIK0grSCuJa0uJS0tLS0pqaCgoKCgoKUgoKUlKbYgqaCpoLegqaCpoKmooKUpoKmgoKUgoKCgoKCgpS4goKmooKCpqKCpqKCgqamwoKmgoKCgqaCgoKmpqKCgqaUlKS06KSkpIKCpqSClAAEGJSCxqSkpKSClKTWgpSk1oLGgsakpILGgsaCyIKCuKaCAAEQpKbCgoaClOaElILGgsaCxoLGtKSmooKCgqamooKUpqKmooKogoKUgpSmooKClIKmgqaCgpS4goKUuIKCgpSmgqaCgoKCgoKCgoKCgpSClJSClKaCgoKUpoKCgpSmgoKCgpS4goKCpoKCgoKUpoKCgoKCpoKCpqaCpoKCgpS0tKSCgoKCgqYAAhKCiIKCgoKUlIKUiJSCgpSCgsaClIKUgoKUgoLGtKSCpqaCrIKqgoKCgoKClISWlKYACyiCAAcQsoKMgoSWlIKCgoKCgg==", "", "", "338=getStartPositionOfRange/1/2/1,getStartPositionOfRange/2/2/2;676=AsBinaryExpression/1/1/1;845=Len/1/1/1,Pos/1/2/2,Pos/2/2/3;857=matchString/2/4/1,matchString/3/4/2,matchString/4/4/3;903=Len/1/1/1,Pos/1/2/2,Pos/2/2/3;907=Len/1/1/1,Pos/1/1/2;939=ECMALineMap/1/1/1,Text/1/1/2")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("printer", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/printer")]
public static partial class printer_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface Write_type {}
    internal partial interface triviaPositionKey {}
    internal partial interface tryGetEnd_node {}
    internal partial struct commentSeparator {}
    internal partial struct commentState {}
    internal partial struct detachedCommentsInfo {}
    [GoLocalName("Mode")] internal partial struct emitModifierList_Mode {}
    internal partial struct emitNode {}
    internal partial struct emitNodeFlags {}
    [GoLocalName("entry")] internal partial struct emitPartiallyEmittedExpression_entry {}
    internal partial struct environmentFlags {}
    internal partial struct getLiteralTextFlags {}
    internal partial struct lineCharacterCache {}
    internal partial struct nameGenerationScope {}
    internal partial struct printerState {}
    internal partial struct singleLineStringWriter {}
    internal partial struct sourceMapState {}
    internal partial struct tempFlags {}
    internal partial struct textWriter {}
    internal partial struct tokenEmitFlags {}
    internal partial struct trailingSemicolonDeferringWriter {}
    internal partial struct varScope {}
    public partial interface EmitHost {}
    public partial interface EmitResolver {}
    public partial interface EmitTextWriter {}
    public partial interface SourceFileMetaDataProvider {}
    public partial struct AssignedNameOptions {}
    public partial struct AutoGenerateId {}
    public partial struct AutoGenerateInfo {}
    public partial struct AutoGenerateOptions {}
    public partial struct ChangeTrackerWriter {}
    public partial struct EmitContext {}
    public partial struct EmitFlags {}
    public partial struct EmitHelper {}
    public partial struct GeneratedIdentifierFlags {}
    public partial struct ListFormat {}
    public partial struct NameGenerator {}
    public partial struct NameOptions {}
    public partial struct NodeFactory {}
    public partial struct PrintHandlers {}
    public partial struct Printer {}
    public partial struct PrinterOptions {}
    public partial struct Priority {}
    public partial struct PrivateIdentifierKind {}
    public partial struct QuoteChar {}
    public partial struct SnippetElement {}
    public partial struct SnippetKind {}
    public partial struct SymbolAccessibility {}
    public partial struct SymbolAccessibilityResult {}
    public partial struct SynthesizedComment {}
    public partial struct TypeReferenceSerializationKind {}
    public partial struct WriteKind {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸbinder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.binder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸevaluator() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.evaluator_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸnodebuilder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸsourcemap() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtsoptions() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸsyncꓸatomic() => builtin.initPackage(typeof(go.sync.atomic_package));
    [GoInit] internal static void initᴛᴛimportꓸunicodeꓸutf8() => builtin.initPackage(typeof(unicode.utf8_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(printer_package.ChangeTrackerWriter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛChangeTrackerWriter;
[GoMethodHost(typeof(printer_package.EmitContext)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛEmitContext;
[GoMethodHost(typeof(printer_package.GeneratedIdentifierFlags)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛGeneratedIdentifierFlags;
[GoMethodHost(typeof(printer_package.NameGenerator)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛNameGenerator;
[GoMethodHost(typeof(printer_package.NodeFactory)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛNodeFactory;
[GoMethodHost(typeof(printer_package.Printer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛPrinter;
[GoMethodHost(typeof(printer_package.emitNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛemitNode;
[GoMethodHost(typeof(printer_package.lineCharacterCache)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛlineCharacterCache;
[GoMethodHost(typeof(printer_package.singleLineStringWriter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛsingleLineStringWriter;
[GoMethodHost(typeof(printer_package.textWriter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛtextWriter;
[GoMethodHost(typeof(printer_package.trailingSemicolonDeferringWriter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class printerᴛtrailingSemicolonDeferringWriter;
// </MethodHolders>
