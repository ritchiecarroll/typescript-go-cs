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
global using astꓸBinaryOperatorToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockOrExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCatchClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸColonToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸConciseBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEntityName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEnumDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEnumMemberList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEqualsGreaterThanToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEqualsToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸHeritageClauseList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportPhaseModifierSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸImportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸJsxAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxTagNameExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸKeywordExpressionSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸLeftHandSideExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleReference = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedExportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedImportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNodeBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTemplateLiteral = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.transformers.tstransforms_package;

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
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(RuntimeSyntaxTransformer))]
[assembly: GoFieldAccess(typeof(allDecorators))]
[assembly: GoFieldAccess(typeof(astꓸBinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(astꓸClassElementList))]
[assembly: GoFieldAccess(typeof(astꓸDeclarationName))]
[assembly: GoFieldAccess(typeof(astꓸEntityName))]
[assembly: GoFieldAccess(typeof(astꓸEnumMemberList))]
[assembly: GoFieldAccess(typeof(astꓸExportSpecifierList))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸImportSpecifierList))]
[assembly: GoFieldAccess(typeof(astꓸModuleReference))]
[assembly: GoFieldAccess(typeof(astꓸNodeBody))]
[assembly: GoFieldAccess(typeof(astꓸParameterList))]
[assembly: GoFieldAccess(typeof(astꓸStatement))]
[assembly: GoFieldAccess(typeof(astꓸStatementList))]
[assembly: GoFieldAccess(typeof(astꓸTypeList))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ArrowFunction))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Block))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CallExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HeritageClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportEqualsDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IntersectionTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.LiteralTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.MethodDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModifierList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModuleBlock))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.QualifiedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TaggedTemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TryStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeOperatorNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypePredicateNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.UnionTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclarationList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.EmitContext))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/tstransforms/importelision.go", "importelision.cs", "ABIiooKCgpSCxqKCppSCgqaCpqSGgoKClJSkgoKChJSkhJSkgoKElKSElKSElKSCgoKChKakgoKElKSElKSCgoKCpKTIgqaIpqKCpqKCpqKC")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/tstransforms/legacydecorators.go", "legacydecorators.cs", "ABUyooLGtIKmlKSmpKSkpKSkpKSkgoKCgoKCpMiEgoKmpqiCgpTGooKUgpSCgqaigpSClIKCxqKGgpSmogAHEoaCgoKClKyigoKCgoKCgqamooKUgqYAChqiAAweogAMHqIADiKiAAkWpAAIFKKCgqaClAACEriCgoKUgqYABxKClKaCpoKuAAauAYKCgoKCgoKUuIKCgoKsmoKUmIKCgsyCgoKmgoKUAAYQgpiCgpTMlIKCgoKUgoKUgoKClJSmgpTWooKCkoKYgpSUgoKmAAIQsoKUgoKmgoKUAAIQgoKCgoKUAAIQooyCgpSCgpSCpoKCrIKCgoKUgoKCxoKmoqaipoKmgoKUgoKUgoKUgpSCpgAHIIKCgoKUgpQAAhailIKUpKSkAAQUooKUgoKCgoKmgqaCgoKmgqbcooKClKaigpSCgoKUgpQAAhKigoKCgoKCgoKUgoKCgpS6xqKCgoKCgoKUvKYAAhaigoKClAAFEoIAAhaygpSCgoKmAAUWooKCgoKCpgADErKCgoIAAkSCgoKGmKbegoKmogACELKCqIKClIKCgoKmooKCgoK6grgAAxCigoKUtoKClKaCAAYQooKCgoKClIKClA==", "515-525:1", "", "152=Factory/2/2/2,Modifiers/1/1/2,elideModifiers/1/1/2,Visitor/1/2/4,Name/1/1/4,VisitNode/1/2/4,Visitor/2/2/7,VisitNode/2/2/7;196=Factory/1/1/1,Visitor/1/2/3,Modifiers/1/1/3,VisitModifiers/1/1/3,AsNode/1/2/4,visitPropertyNameOfClassElement/1/1/4,Visitor/2/2/7,VisitNode/1/1/7,UpdatePropertyDeclaration/1/1/1,AsNode/2/2/9;210=Factory/1/1/1,Visitor/1/3/3,Modifiers/1/1/3,VisitModifiers/1/1/3,AsNode/1/2/4,visitPropertyNameOfClassElement/1/1/4,Visitor/2/3/6,VisitNodes/1/1/6,Visitor/3/3/9,VisitNode/1/1/9,UpdateGetAccessorDeclaration/1/1/1,AsNode/2/2/11;226=Factory/1/1/1,Visitor/1/3/3,Modifiers/1/1/3,VisitModifiers/1/1/3,AsNode/1/2/4,visitPropertyNameOfClassElement/1/1/4,Visitor/2/3/6,VisitNodes/1/1/6,Visitor/3/3/9,VisitNode/1/1/9,UpdateSetAccessorDeclaration/1/1/1,AsNode/2/2/11;242=Factory/1/1/1,Visitor/1/3/3,Modifiers/1/1/3,VisitModifiers/1/1/3,AsNode/1/2/5,visitPropertyNameOfClassElement/1/1/5,Visitor/2/3/8,VisitNodes/1/1/8,Visitor/3/3/11,VisitNode/1/1/11,UpdateMethodDeclaration/1/1/1,AsNode/2/2/13;260=Visitor/1/3/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Visitor/2/3/4,VisitNodes/1/1/4,Visitor/3/3/7,VisitNode/1/1/7;273=Visitor/1/3/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Name/1/1/3,Visitor/2/3/5,VisitNodes/1/2/5,Visitor/3/3/6,VisitNodes/2/2/6;368=Factory/3/6/1,Factory/4/6/1,Factory/5/6/2,Factory/6/6/2,NewKeywordExpression/1/1/2,NewAssignmentExpression/1/1/2,NewExpressionStatement/1/1/1,NewNodeList/1/1/1;701=Factory/2/4/3,Factory/3/4/3,Factory/4/4/3,NewNodeList/1/2/3,NewBlock/1/1/3,NewClassStaticBlockDeclaration/1/1/3;825=Visitor/1/1/1,Expression/1/2/1,VisitNode/1/1/1,Expression/2/2/3;857=Factory/2/3/1,AsNode/1/1/1,GetDeclarationName/1/1/1,Factory/3/3/3,NewIdentifier/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/tstransforms/metadata.go", "metadata.cs", "ABgwotzG0oKmlKSkpKSkpIKCgoKCgoKkgoKCpPiCpoKm0oKClIKUggALFNKCgpSClIIACxSigqaCAAgUooKmggAMHKKCpoIAChiigqaCAAoYooKCgoKUgoKClJSCgoKCpoKCgoKCgoKClKaigpSClIKCgoKUgoKClJSCgoKCgoKCgpQAAhCEgpSClKaCgoKClIKClIKClKaCgoIADSCCAA0gggANIIKClAACFKKUpAACFKIAAhSilKSk", "", "", "28=IsTrue/1/1/1,GetEmitScriptTarget/1/1/3,GetStrictOptionValue/1/1/4;115=Visitor/1/4/3,Name/1/1/3,VisitNode/1/1/3,Visitor/2/4/4,VisitNodes/1/3/4,Visitor/3/4/5,VisitNodes/2/3/5,Visitor/4/4/6,VisitNodes/3/3/6;141=Visitor/1/4/3,Name/1/1/3,VisitNode/1/1/3,Visitor/2/4/4,VisitNodes/1/3/4,Visitor/3/4/5,VisitNodes/2/3/5,Visitor/4/4/6,VisitNodes/3/3/6;161=Visitor/1/4/3,Name/1/1/3,VisitNode/1/4/3,Visitor/2/4/4,VisitNode/2/4/4,Visitor/3/4/5,VisitNode/3/4/5,Visitor/4/4/6,VisitNode/4/4/6;178=Visitor/1/8/3,VisitNode/1/6/3,Visitor/2/8/4,Name/1/1/4,VisitNode/2/6/4,Visitor/3/8/5,VisitNode/3/6/5,Visitor/4/8/6,VisitNodes/1/2/6,Visitor/5/8/7,VisitNodes/2/2/7,Visitor/6/8/8,VisitNode/4/6/8,Visitor/7/8/9,VisitNode/5/6/9,Visitor/8/8/10,VisitNode/6/6/10;199=Visitor/1/6/3,Name/1/1/3,VisitNode/1/4/3,Visitor/2/6/4,VisitNodes/1/2/4,Visitor/3/6/5,VisitNodes/2/2/5,Visitor/4/6/6,VisitNode/2/4/6,Visitor/5/6/7,VisitNode/3/4/7,Visitor/6/6/8,VisitNode/4/4/8;218=Visitor/1/6/3,Name/1/1/3,VisitNode/1/4/3,Visitor/2/6/4,VisitNodes/1/2/4,Visitor/3/6/5,VisitNodes/2/2/5,Visitor/4/6/6,VisitNode/2/4/6,Visitor/5/6/7,VisitNode/3/4/7,Visitor/6/6/8,VisitNode/4/4/8;330=Factory/2/5/2,NewIdentifier/1/1/2,Factory/3/5/5,Factory/4/5/8,NewNodeList/1/1/8,Factory/5/5/11,NewToken/1/1/11,SerializeTypeOfNode/1/1/12,NewArrowFunction/1/1/5;345=Factory/2/5/2,NewIdentifier/1/1/2,Factory/3/5/5,Factory/4/5/8,NewNodeList/1/1/8,Factory/5/5/11,NewToken/1/1/11,SerializeParameterTypesOfNode/1/1/12,NewArrowFunction/1/1/5;360=Factory/2/5/2,NewIdentifier/1/1/2,Factory/3/5/5,Factory/4/5/8,NewNodeList/1/1/8,Factory/5/5/11,NewToken/1/1/11,SerializeReturnTypeOfNode/1/1/12,NewArrowFunction/1/1/5")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/tstransforms/runtimesyntax.go", "runtimesyntax.cs", "AB0+ooKCgsiigoKCqIKCpsKCgpSCgrSCtLSmgoSmqNKClIKUgqaetLS0tLS0tLSElMaEgoSUxrS0tNiilIKkgpSkgpSkgoKCgpSCgpSCzKKCgoKCpqaiqKKClKSEpKSkpMqCgoKogqiCqIKCgqiCgpSmooKCqIKCgoSCgpSClISWgpQAAiaCgpSmooKmmJiCgqoABRCElqiClIKCgoKCgoKCqKKClpSChN6ClIKoqoKUgoKYlJSCgpSCtMSClLy6hgAIFoKCgpSCgqaigqaYmIKCqgAFEISWqIKUgoKCgoKCgoKmooKClIKUgpSCgoKEgoKCgpiCgriCgpSCgoKCAAEugpSmooKmgoKGgoKCgoKCgpaCgrjCgoKCgoKUgt6CpoKCuIKUgoKCloKCgoKUqqKCgoKUpqKCAAkWgoKUlKaygoKCgri2ooKCgpSmgoKUgoKUgoKCgt6CpoKCgriCgoKCpqaigoKCgpSCgoKC3oKmgoKCuKaigoKCprKCgqaClIKCAAEegoKCgoKUgoKUAAcWgoK4lIKUgraCgpSCgoKCgqaigoKWpoKCloKCls6ClIKCAAUQlqaokraigoKCgoKCgpQABxKCgoKClAAJFoKClKaigoKCgpSClIKCpqaigoKCgoKUlIKCgqaCgoKCgqaCgoKCpoKmgoKElMaigpQ=", "", "", "337=AsNode/1/2/1,getExportQualifiedReferenceToDeclaration/1/2/1,Factory/2/4/2,AsNode/2/2/3,getExportQualifiedReferenceToDeclaration/2/2/3,Factory/3/4/4,Factory/4/4/4,NewNodeList/1/1/4,NewObjectLiteralExpression/1/1/4,NewAssignmentExpression/1/1/2;421=getEnumQualifiedElement/1/1/1;426=Factory/2/2/1,AsNode/1/1/2,getNamespaceContainerName/1/1/2,NewElementAccessExpression/1/1/1,getExpressionForPropertyName/1/1/7;461=AsNode/1/2/1,getExportQualifiedReferenceToDeclaration/1/2/1,Factory/2/4/2,AsNode/2/2/3,getExportQualifiedReferenceToDeclaration/2/2/3,Factory/3/4/4,Factory/4/4/4,NewNodeList/1/1/4,NewObjectLiteralExpression/1/1/4,NewAssignmentExpression/1/1/2;570=Visitor/1/1/2,VisitNode/1/1/2;618=Visitor/1/4/2,EmitContext/1/1/2,Modifiers/1/1/2,ExtractModifiers/1/1/2,VisitModifiers/1/1/2,Visitor/2/4/4,Name/1/1/4,VisitNode/1/2/4,Visitor/3/4/6,VisitNodes/1/1/6,Visitor/4/4/9,VisitNode/2/2/9;676=Name/1/1/2,Factory/2/2/2,Clone/1/1/2;716=Name/1/1/2,Factory/2/2/2,Clone/1/1/2;772=Factory/2/4/1,Factory/3/4/2,Factory/4/4/3,NewThisExpression/1/1/3,NewPropertyAccessExpression/1/1/2,NewAssignmentExpression/1/1/1;836=Factory/2/2/2,UpdateBlock/1/1/2,Visitor/1/2/3,VisitNode/1/2/3,Visitor/2/2/4,VisitNode/2/2/4;868=Visitor/1/1/5,VisitNode/1/1/5;882=Visitor/1/1/7,VisitNode/1/1/7")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/tstransforms/typeeraser.go", "typeeraser.cs", "ABMkooKCgsiigoKCqIKCpoKm0oKmgqaClAABVK6ClKiqqKaIlKaCpoSClISUgqaChJSmgoSUpoKElIKClKaChJSCgpSmgoKClKaChJSmgqaCpoKElKaCpoKmhJSEgoKWgoKCgpSmpoKmgqaCpoKCgqaCgoKCgoKCpqaCpoKmgoSUpoKGlIKClKaChJSCgoSUpoKElIKElKaChJSmgoSUgoKChKamgoSmgoSUpoKElKaClKY=", "", "", "86=ShouldPreserveConstEnums/1/1/1,IsInstantiatedModule/1/1/1,AsModuleDeclaration/1/1/2,getInnermostModuleDeclarationFromDottedModule/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/tstransforms/typeserializer.go", "typeserializer.cs", "ABs0gsaCpsKCgoLWwoKCgtbCgoKC9qKCgpSUAAIQgoKClKaigoKUgpTOspSkpKQABBCygoKCgqaCpoKCgoKUgpSmxrKCgoKmzrKCgoKUAAIq0oKmlJSkpKSClKSkpKSkpKSkpKSkgoKAkqSCyLi0pIKk1qSCooKCgpSmgoKUpoKmgqaChKqEgqi6gpSmopSkgpSk5qSkpKSCpK6igoKUgpaCpoKCggAHEqampqampqampqakgsiCgpQABxqimIKCgoKkpK6irqKEgpSEloKCggAGKILMtIKWgpSCloKWgpaCloKWgpaClA==", "220-220:1", "", "397=NewAssignmentExpression/1/1/1,NewTypeCheck/1/1/1,NewToken/1/2/2,NewToken/2/2/4,NewIdentifier/1/1/5;448=NewIdentifier/1/3/1,NewTypeCheck/1/1/1,NewToken/1/2/2,NewIdentifier/2/3/3,NewToken/2/2/4,NewIdentifier/3/3/5;493=AsBinaryExpression/1/2/2,AsBinaryExpression/2/2/3,NewAssignmentExpression/1/1/3,NewVoidZeroExpression/1/1/3,NewStrictInequalityExpression/1/1/3,NewLogicalANDExpression/1/2/1,AsQualifiedName/1/1/5,NewPropertyAccessExpression/1/1/5;501=NewTypeOfExpression/1/1/1,NewStringLiteral/1/1/1,NewStrictInequalityExpression/1/1/1", "", "257:25,258:17")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/tstransforms/utilities.go", "utilities.cs", "AAkSgpSkgoKUlIKUgpSU")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

[GoPackage("tstransforms", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/transformers/tstransforms")]
public static partial class tstransforms_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct allDecorators {}
    internal partial struct metadataSerializer {}
    internal partial struct metadataSerializerContext {}
    public partial struct ImportElisionTransformer {}
    public partial struct LegacyDecoratorsTransformer {}
    public partial struct MetadataTransformer {}
    public partial struct RuntimeSyntaxTransformer {}
    public partial struct TypeEraserTransformer {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸbinder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.binder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjsnum() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(tstransforms_package.ImportElisionTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tstransformsᴛImportElisionTransformer;
[GoMethodHost(typeof(tstransforms_package.LegacyDecoratorsTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tstransformsᴛLegacyDecoratorsTransformer;
[GoMethodHost(typeof(tstransforms_package.MetadataTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tstransformsᴛMetadataTransformer;
[GoMethodHost(typeof(tstransforms_package.RuntimeSyntaxTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tstransformsᴛRuntimeSyntaxTransformer;
[GoMethodHost(typeof(tstransforms_package.TypeEraserTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tstransformsᴛTypeEraserTransformer;
[GoMethodHost(typeof(tstransforms_package.metadataSerializer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tstransformsᴛmetadataSerializer;
// </MethodHolders>
