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
global using astꓸAwaitKeyword = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBinaryOperatorToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCaseBlockNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸConciseBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEqualsGreaterThanToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEqualsToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸForInitializer = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportPhaseModifierSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸImportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸImportSpecifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸLiteralExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleExportName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleReference = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedExportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedImportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸStringLiteralNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateHeadNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateLiteral = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateMiddleOrTail = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateSpanList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTemplateSpanNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.transformers.moduletransforms_package;

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
[assembly: GoFieldAccess(typeof(CommonJSModuleTransformer))]
[assembly: GoFieldAccess(typeof(astꓸBinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(astꓸDeclaration))]
[assembly: GoFieldAccess(typeof(astꓸDeclarationName))]
[assembly: GoFieldAccess(typeof(astꓸElementList))]
[assembly: GoFieldAccess(typeof(astꓸForInitializer))]
[assembly: GoFieldAccess(typeof(astꓸImportSpecifierList))]
[assembly: GoFieldAccess(typeof(astꓸModuleExportName))]
[assembly: GoFieldAccess(typeof(astꓸNamedImportBindings))]
[assembly: GoFieldAccess(typeof(astꓸStatementList))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationListNode))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(externalModuleInfo))]
[assembly: GoFieldAccess(typeof(externalModuleInfoCollector))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Block))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportEqualsDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.StringLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclarationList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.AutoGenerateInfo))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.EmitContext))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package.EmitHelper))]
[assembly: GoFieldAccess(typeof(importRequireStatements))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/moduletransforms/commonjsmodule.go", "commonjsmodule.cs", "ACFAooKCgoKCgoKCgsiigoKCqIKCqNKClJS0tLS0tLS0tNjCgpTYspS0tLS0tLS0tLS0tLS0tLSowoKU2LSCppS0tLS0tLS0tLS0tLS0tLaowoKU1sKClNaylri0uLi4goKmtKayhqaCgoKCgqaChpSClKaCABc6gqayloKWgqaCqoKCgoKCgoKC3oKC3pSCggADEoKClIKogpaWlIKCgpSCgoKCgoKCgoKCpqyigoKCAAgYgoKmptKCgt6igqaCgqaCgqaCgpS2guquoq6igqaCpq6igqaCgoKmhKauooKmgoKCgpSmgqaCpq6ygqaCpoKCgoKCgsoAAhSygoKUlIIAAhKigoKUgoKUAAIQsoKGAClkgoYABRLelIKUqIKCgoKUAAcSgoKUpoKClIKUpqKEgoKCpoKCgoQABxwABxSCAAkYAAUQgoKCgqaihKaChAAGEoKClgAKHIKCpoKmooamgoSCAAocgoKUgoKUgoKUpoKClKaCgpSUAAUQgoKmpoaCgpSUAAcWgoKoloKCpqKCpgAHEqKCAAoYuKKCggAHEpSCprKChIKClIKCggAGEoKUgriCgoKCgpSCuJKCppKCqKKUAAAQgqaCAAscgobezJSogoKClIaCgqqCgsqCgoKUptKClIKOgoKClMyC2oKCgoKqooKCAAASgoKCgpSCgoLepgAHFqKCggAAGIKCgoKCgoKClIKUpgAHFqIABRKiAAUSooKClKqiAAUSooKCgpSCqqIABRKCqqIABRKCqqIABRKCpqIABxKiAAcUgqiCqKKCqKKCqqKCpoKmgqamroaChIKClLiosoIABhCqooKClILGgsaCxraCgoKClIKmgoKGlIKcgpSUqqKChoKGgoLMgoKUlIKUmIKCgsyCgoKUrIKCpqIACBSigoIACBSCgoKUAAYQ3oKCpqLMosyigoKCAAkWptKClJS0tNaihoKCAAAQppSCpoKCAAYQAAcSgoKUpqiigoKoAAIUhoIAABiCgoKUpqgAAhSGggAAMoKCgoKUgqaClIKCpoKCpriqooKChKaClIKUAAASggAGEIKUlKaCpqKCpoKWgoKCgoKUppSmAAASlIKCAAcSAAoagoKmgqYABxSCggAKGAAIFAAJGKaigoKCgoKCgoKUgqaCgoKCuAAIFqIAABSCAAYQgpSUqKKCgoaCgrqCgoKUAAkYgoKUqKKIgoLMgoKmgoKCzIKClIKCgoKCgt6Cgt6CgriowoKCgqqCgoKCgoKCgoK4pIKCgoKClLY=", "984-1002:1;1004-1014:2;1016-1019:3;1021-1024:4", "", "354=IsEffectiveExternalModule/1/1/1,SubtreeFacts/1/1/2;377=Factory/2/13/1,Factory/3/13/2,Factory/4/13/3,NewIdentifier/1/4/3,Factory/5/13/5,NewIdentifier/2/4/5,NewPropertyAccessExpression/1/1/2,Factory/6/13/10,Factory/7/13/11,NewIdentifier/3/4/11,Factory/8/13/12,NewStringLiteral/1/1/12,Factory/9/13/13,Factory/10/13/14,Factory/11/13/15,Factory/12/13/17,NewIdentifier/4/4/17,Factory/13/13/20,NewTrueExpression/1/1/20,NewPropertyAssignment/1/1/15,NewNodeList/1/2/14,NewObjectLiteralExpression/1/1/13,NewNodeList/2/2/10,NewCallExpression/1/1/1;430=Factory/2/3/1,NewIdentifier/1/1/1,Factory/3/3/3,NewStringLiteralFromNode/1/1/3;438=Factory/2/2/1,NewIdentifier/1/1/1;494=Factory/2/5/1,Factory/3/5/2,Factory/4/5/3,NewIdentifier/1/2/3,Factory/5/5/5,NewIdentifier/2/2/5,NewPropertyAccessExpression/1/1/2,NewAssignmentExpression/1/1/1;688=Factory/2/19/1,Factory/3/19/2,NewIdentifier/1/5/2,Factory/4/19/4,NewIdentifier/2/5/4,NewPropertyAccessExpression/1/1/1,Factory/5/19/9,Factory/6/19/10,NewIdentifier/3/5/10,Factory/7/19/11,NewStringLiteralFromNode/1/1/11,Factory/8/19/12,Factory/9/19/13,Factory/10/19/14,Factory/11/19/16,NewIdentifier/4/5/16,Factory/12/19/19,NewTrueExpression/1/1/19,NewPropertyAssignment/1/2/14,Factory/13/19/21,Factory/14/19/23,NewIdentifier/5/5/23,Factory/15/19/26,Factory/16/19/31,NewNodeList/1/4/31,Factory/17/19/34,Factory/18/19/35,Factory/19/19/36,NewReturnStatement/1/1/36,NewNodeList/2/4/35,NewBlock/1/1/34,NewFunctionExpression/1/1/26,NewPropertyAssignment/2/2/21,NewNodeList/3/4/13,NewObjectLiteralExpression/1/1/12,NewNodeList/4/4/9;732=Factory/2/3/1,NewIdentifier/1/1/1,Factory/3/3/3,NewStringLiteralFromNode/1/1/3;738=Factory/2/3/1,NewIdentifier/1/1/1,Factory/3/3/3,Clone/1/1/3;758=Factory/2/3/1,NewIdentifier/1/1/1,Factory/3/3/4,NewNodeList/1/1/4;797=Factory/1/2/2,Name/1/1/3,Factory/2/2/3,Clone/1/1/3,AsNode/1/1/6,createRequireCall/1/1/6,getHelperExpressionForImport/1/1/6,NewVariableDeclaration/1/1/2;805=Factory/1/2/2,Factory/2/2/3,AsNode/1/2/3,NewGeneratedNameForNode/1/1/3,AsNode/2/2/6,createRequireCall/1/1/6,getHelperExpressionForImport/1/1/6,NewVariableDeclaration/1/1/2;814=Factory/1/3/2,Name/1/1/3,Factory/2/3/3,Clone/1/1/3,Factory/3/3/6,AsNode/1/1/6,NewGeneratedNameForNode/1/1/6,NewVariableDeclaration/1/1/2;824=Factory/2/3/2,Factory/3/3/3,NewNodeList/1/1/3,NewVariableDeclarationList/1/1/2;846=Name/1/1/2,AsNode/1/1/3,createRequireCall/1/1/3,createExportExpression/1/1/1;857=Factory/2/5/2,Factory/3/5/3,Factory/4/5/4,Name/1/1/5,Factory/5/5/5,Clone/1/1/5,AsNode/1/1/8,createRequireCall/1/1/8,NewVariableDeclaration/1/1/4,NewNodeList/1/1/3,NewVariableDeclarationList/1/1/2;887=Factory/2/4/2,Factory/3/4/3,Factory/4/4/4,AsNode/1/1/8,createRequireCall/1/1/8,NewVariableDeclaration/1/1/4,NewNodeList/1/1/3,NewVariableDeclarationList/1/1/2;926=createExportExpression/1/1/1;947=AsNode/1/1/5,createRequireCall/1/1/5,getHelperExpressionForExport/1/1/3,createExportExpression/1/1/1;960=Visitor/1/1/1,Factory/2/3/1,AsNode/1/1/1,createRequireCall/1/1/1,Factory/3/3/1,NewIdentifier/1/1/1,NewExportStarHelper/1/1/1,VisitNode/1/1/1;974=Factory/1/1/1,NewIdentifier/1/1/1,Visitor/1/1/2,VisitNode/1/1/2;986=EmitContext/1/1/2,Modifiers/1/1/2,ExtractModifiers/1/1/2,Factory/2/2/4,AsNode/1/1/4,GetDeclarationName/1/1/4,Visitor/1/2/6,VisitNodes/1/1/6,Visitor/2/2/9,VisitNode/1/1/9;1006=Visitor/1/3/2,EmitContext/1/1/2,Modifiers/1/1/2,ExtractModifiers/1/1/2,VisitModifiers/1/1/2,Factory/2/2/3,AsNode/1/1/3,GetDeclarationName/1/1/3,Visitor/2/3/5,VisitNodes/1/2/5,Visitor/3/3/6,VisitNodes/2/2/6;1033=Factory/2/2/3,AsVariableDeclarationList/1/1/4,UpdateVariableDeclarationList/1/1/3;1083=Name/1/2/2,Name/2/2/6,Visitor/1/1/7,VisitNode/1/1/7,createExportExpression/1/1/5;1097=Name/1/1/1,Visitor/1/1/4,VisitNode/1/1/4;1103=Factory/2/2/1,NewIdentifier/1/1/1,Name/1/1/3;1110=Name/1/1/2,Factory/2/2/2,Clone/1/1/2;1155=Factory/2/2/1,NewIdentifier/1/1/1;1198=VisitNode/1/3/2,Visitor/1/1/3,VisitNode/2/3/3,VisitNode/3/3/4,EmitContext/1/1/5,VisitIterationBody/1/1/5;1228=VisitNode/1/2/3,Visitor/1/1/4,VisitNode/2/2/4,EmitContext/1/1/5,VisitIterationBody/1/1/5;1239=EmitContext/1/1/2,VisitIterationBody/1/1/2,Visitor/1/1/3,VisitNode/1/1/3;1248=Visitor/1/1/2,VisitNode/1/1/2,EmitContext/1/1/3,VisitIterationBody/1/1/3;1267=Visitor/1/1/2,VisitNode/1/1/2,VisitEmbeddedStatement/1/1/3;1288=Visitor/1/1/2,VisitNode/1/2/2,VisitNode/2/2/3;1301=Visitor/1/1/2,VisitNode/1/1/2,VisitNodes/1/1/3;1314=VisitNode/1/1/3;1327=VisitNode/1/3/2,Visitor/1/1/3,VisitNode/2/3/3,VisitNode/3/3/4,EmitContext/1/1/5,VisitIterationBody/1/1/5;1338=VisitNode/1/2/3,Visitor/1/1/4,VisitNode/2/2/4,EmitContext/1/1/5,VisitIterationBody/1/1/5;1389=EmitContext/1/3/1,IsGeneratedIdentifier/1/1/1,EmitContext/2/3/1,isFileLevelReservedGeneratedIdentifier/1/1/1,EmitContext/3/3/2,IsLocalName/1/1/2;1408=AsNode/1/1/2;1482=Factory/2/2/1,NewIdentifier/1/1/1;1501=Factory/2/2/1,NewIdentifier/1/1/1;1522=Visitor/1/1/3,Name/1/1/3,VisitNode/1/2/3,VisitNode/2/2/6;1536=Visitor/1/1/7,VisitNode/1/1/7;1550=Visitor/1/1/5,VisitNode/1/1/5;1557=Name/1/1/2;1571=visitDestructuringAssignmentTarget/1/1/2;1579=visitDestructuringAssignmentTarget/1/1/2;1590=visitDestructuringAssignmentTarget/1/1/3,Visitor/1/1/6,VisitNode/1/1/6;1631=EmitContext/1/3/1,IsGeneratedIdentifier/1/1/1,EmitContext/2/3/1,isFileLevelReservedGeneratedIdentifier/1/1/1,EmitContext/3/3/2,IsLocalName/1/1/2;1653=Factory/2/4/2,NewIdentifier/1/1/2,Factory/3/4/4,NewNodeList/1/1/4,Factory/4/4/7,NewBlock/1/1/7;1682=IsIdentifier/1/1/1,EmitContext/1/1/2,IsLocalName/1/1/2;1699=IsIdentifier/1/1/1,EmitContext/1/1/2,IsLocalName/1/1/2;1737=AsNode/2/3/1,IsInJSFile/1/1/1,AsNode/3/3/1,IsRequireCall/1/1/1;1749=Visitor/1/1/5,VisitNodes/1/1/5;1798=Factory/1/5/1,Factory/2/5/2,NewTemplateHead/1/1/2,Factory/3/5/3,Factory/4/5/4,Factory/5/5/4,NewTemplateTail/1/1/4,NewTemplateSpan/1/1/4,NewNodeList/1/1/3,NewTemplateExpression/1/1/1;1806=Factory/2/5/1,Factory/3/5/2,NewIdentifier/1/2/2,Factory/4/5/4,NewIdentifier/2/2/4,NewPropertyAccessExpression/1/1/1,Factory/5/5/9,NewNodeList/1/1/9;1819=Factory/1/1/1,NewIdentifier/1/1/1;1826=Factory/2/4/1,Factory/3/4/2,NewIdentifier/1/1/2,Factory/4/4/5,NewNodeList/1/1/5,NewCallExpression/1/1/1;1836=Factory/1/2/1,Factory/2/2/4,NewIdentifier/1/1/4,NewParameterDeclaration/1/1/1;1847=Factory/2/3/3,NewNodeList/1/1/3,Factory/3/3/6,NewToken/1/1/6;1856=Factory/2/4/1,Factory/3/4/4,NewIdentifier/1/1/4,NewPropertyAccessExpression/1/1/1,Factory/4/4/9,NewNodeList/1/1/9;1908=Visitor/1/1/5,VisitNode/1/1/5;1931=Visitor/1/1/2,VisitNode/1/1/2;1940=Visitor/1/1/7,VisitNode/1/1/7;1963=Factory/2/3/1,NewIdentifier/1/1/1,Factory/3/3/3,Clone/1/1/3;1976=Factory/2/3/1,NewGeneratedNameForNode/1/1/1,Factory/3/3/3,NewIdentifier/1/1/3;1991=Factory/2/2/3,NewStringLiteralFromNode/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/moduletransforms/esmodule.go", "esmodule.cs", "ABw4ooKCyKKUtLS0tLS0tKaihKaClIKUgoKCgoKCiJSClIKCgqaGgoKCgqaCgqaigpSCAAcSqIKmgqYAChyClIKCgqaiggAMIKaigpSElAAHFoKmooKmgoQACBSCggAHFJSCgpQACRiCpqKChKamooKmlIKClKaClJKUgoIACBSClIKCpoIABxKCggANJJSCABU2gtw=", "", "", "78=IsExternalModule/1/1/1,GetIsolatedModules/1/1/1;106=GetEmitModuleKind/1/1/1,Some/1/1/2;126=Visitor/1/2/3,VisitNode/1/2/3,Visitor/2/2/5,VisitNode/2/2/5;145=Factory/2/5/2,Factory/3/5/3,Factory/4/5/4,Name/1/1/5,Factory/5/5/5,Clone/1/1/5,AsNode/1/1/8,createRequireCall/1/1/8,NewVariableDeclaration/1/1/4,NewNodeList/1/1/3,NewVariableDeclarationList/1/1/2;169=Factory/2/5/3,Factory/3/5/4,Factory/4/5/5,Name/1/1/8,Factory/5/5/8,Clone/1/1/8,NewExportSpecifier/1/1/5,NewNodeList/1/1/4,NewNamedExports/1/1/3;194=Factory/2/5/1,Factory/3/5/2,Factory/4/5/3,NewIdentifier/1/2/3,Factory/5/5/5,NewIdentifier/2/2/5,NewPropertyAccessExpression/1/1/2,Visitor/1/1/8,VisitNode/1/1/8,NewAssignmentExpression/1/1/1;215=Visitor/1/1/6,VisitNode/1/1/6;226=Factory/2/3/2,Factory/3/3/5,NewNamespaceImport/1/1/5,NewImportClause/1/1/2,Visitor/1/1/8,VisitNode/1/1/8;240=Factory/2/4/3,Factory/3/4/4,Factory/4/4/5,NewExportSpecifier/1/1/5,NewNodeList/1/1/4,NewNamedExports/1/1/3;258=AsNode/2/3/1,IsInJSFile/1/1/1,AsNode/3/3/1,IsRequireCall/1/1/1;308=Factory/2/3/1,NewIdentifier/1/1/1,Factory/3/3/4,NewNodeList/1/1/4;318=Factory/2/7/2,Factory/3/7/5,Factory/4/7/6,Factory/5/7/7,Factory/6/7/9,NewIdentifier/1/1/9,NewImportSpecifier/1/1/7,NewNodeList/1/1/6,NewNamedImports/1/1/5,NewImportClause/1/1/2,Factory/7/7/15,NewStringLiteral/1/1/15;335=Factory/2/11/2,Factory/3/11/3,Factory/4/11/4,Factory/5/11/8,Factory/6/11/9,Clone/1/1/9,Factory/7/11/12,Factory/8/11/13,Factory/9/11/14,Factory/10/11/14,NewIdentifier/1/2/14,NewMetaProperty/1/1/14,Factory/11/11/16,NewIdentifier/2/2/16,NewPropertyAccessExpression/1/1/13,NewNodeList/1/2/12,NewCallExpression/1/1/8,NewVariableDeclaration/1/1/4,NewNodeList/2/2/3,NewVariableDeclarationList/1/1/2;364=Factory/2/3/1,Clone/1/1/1,Factory/3/3/4,NewNodeList/1/1/4")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/moduletransforms/externalmoduleinfo.go", "externalmoduleinfo.cs", "ACNGgu7GooKChoKCgoKCppScgoKClILIgoTIgoiChIKEgoKogoKCgpaoyIKEyIKCgtqCgsiCgoSCgoKUgqiCgoKCAAkQpqKCgpSmgqaCpoKmsoKCgoKCgqaCgpSCgoKClLjKwoKGgoKUgqqClIKCyrKCgoKCpoKCgoKCAAkQsoKCgoSCgsyCqoKCgoKmgpiCgpSmgoKU3oK4prKCgoKmtqKCgqaYgoKmpqKmoqaCpqKClIKUgoKUgpSCgoKCqKak", "294-300:1", "", "277=NewStringLiteral/1/1/4,NewExternalModuleReference/1/1/4;307=NewImportClause/1/1/2,NewStringLiteral/1/1/3;398=AsImportClause/1/2/1,IsNamedImports/1/1/1,AsImportClause/2/2/2,containsDefaultReference/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/moduletransforms/impliedmodule.go", "impliedmodule.cs", "ABUmooLGopS0pqKCppSCgoKUlIKUpg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/moduletransforms/utilities.go", "utilities.cs", "AAwYooKUtqbCgpSCgoKCgpSmggAHIIKCgoKClIKUlAACEoKCnKaCgpSohKqEpqKCAAISgg==", "", "", "45=NewNodeList/1/1/3,NewNamedExports/1/1/3;94=IsFileLevel/1/1/1,IsOptimistic/1/1/2,IsReservedInNestedScopes/1/1/3")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

[GoPackage("moduletransforms", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/transformers/moduletransforms")]
public static partial class moduletransforms_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct externalModuleInfo {}
    internal partial struct externalModuleInfoCollector {}
    internal partial struct importRequireStatements {}
    public partial struct CommonJSModuleTransformer {}
    public partial struct ESModuleTransformer {}
    public partial struct ImpliedModuleTransformer {}
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
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸoutputpaths() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(moduletransforms_package.CommonJSModuleTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduletransformsᴛCommonJSModuleTransformer;
[GoMethodHost(typeof(moduletransforms_package.ESModuleTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduletransformsᴛESModuleTransformer;
[GoMethodHost(typeof(moduletransforms_package.ImpliedModuleTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduletransformsᴛImpliedModuleTransformer;
[GoMethodHost(typeof(moduletransforms_package.externalModuleInfoCollector)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class moduletransformsᴛexternalModuleInfoCollector;
// </MethodHolders>
