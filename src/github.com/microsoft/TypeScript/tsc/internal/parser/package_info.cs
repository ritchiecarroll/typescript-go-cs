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
global using astꓸAssertsKeyword = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸAsteriskToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸAwaitKeyword = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBinaryOperatorToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBindingElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCaseBlockNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCaseClausesList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸCatchClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸColonToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸConciseBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEntityName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEnumMemberList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEqualsGreaterThanToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEqualsToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
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
global using astꓸImportClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportPhaseModifierSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸImportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸJSDocFullName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸJsxAttributeName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributeValue = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxChildList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸJsxClosingElementNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxClosingFragmentNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxOpeningElementNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxOpeningFragmentNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸJsxTagNameExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸKeywordExpressionSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸKeywordTypeSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸLeftHandSideExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
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
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTemplateHeadNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateLiteral = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateLiteralTypeSpanList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTemplateMiddleOrTail = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateSpanList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeLiteralNodeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypePredicateParameterName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using coreꓸResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.parser_package;

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
[assembly: GoFieldAccess(typeof(Parser))]
[assembly: GoFieldAccess(typeof(astꓸBinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(astꓸDeclarationName))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸExpressionWithTypeArgumentsNode))]
[assembly: GoFieldAccess(typeof(astꓸHeritageClauseElementList))]
[assembly: GoFieldAccess(typeof(astꓸHeritageClauseList))]
[assembly: GoFieldAccess(typeof(astꓸIdentifierNode))]
[assembly: GoFieldAccess(typeof(astꓸJsxOpeningElementNode))]
[assembly: GoFieldAccess(typeof(astꓸParameterDeclarationNode))]
[assembly: GoFieldAccess(typeof(astꓸParameterList))]
[assembly: GoFieldAccess(typeof(astꓸStatement))]
[assembly: GoFieldAccess(typeof(astꓸTemplateLiteral))]
[assembly: GoFieldAccess(typeof(astꓸTemplateMiddleOrTail))]
[assembly: GoFieldAccess(typeof(astꓸTokenNode))]
[assembly: GoFieldAccess(typeof(astꓸTypeList))]
[assembly: GoFieldAccess(typeof(astꓸTypeNode))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ArrayTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CheckJsDirective))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HeritageClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDoc))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDocParameterOrPropertyTag))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDocTemplateTag))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDocThisTag))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JSDocTypeLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxElement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModifierList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModuleDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.QualifiedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ShorthandPropertyAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.StringLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TaggedTemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateLiteralTypeSpan))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateSpan))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TypeReferenceNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclarationList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableStatement))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/parser/jsdoc.go", "jsdoc.cs", "AA4agqrSgoKCgoKUgoKCgoKCpgAUKLKCroKCgpSCuoKWgoKCgoKCgqaCgpSCgpSClIKUpqKCgoKUlIKCgoKCpqaCgoKCgpSCgqiigpaEpoKCgoKCgoKahIKEgoKUhIKUlIKCgoKCgoKUrqaCgoKCgoKCgoKCgoKClIKmgpSCgpSChoKClJS0goKUlIKUgoKUgoKUgoiCtIKCpIKEgpSCloK2gpaCgoKClIKUlKSkgoKUpqSCgpSUtIKCgpSCgoKCgoKUgoKCgpS6goKUpqSClNiCgqaCgoKmgqaCgqaoxoKCgpSmgqaCgoKCgpSCpsaEgoKClILKooKCqIK4soKCuoKCgoKCgoKCgoKUlIKUuKKClIKUgpSCpLS0uLi4uLiCuLS0tLS0tLS0tLS0tLSClKaEgpSCgpSmsoKCgoKCgoKCgoKUgoKClIKmhIKUlIKChoKClJSUhIKkgoKUgpSUpqSClISCgoKUprSCgoKUgoKCgoKCgpSUgoKCgpTGgoKUlKSCgpSotISCgpa0goKUqKSClNiUgoKCgoKUlIKmlIKUpqKCgoKClISCgoKCgpSClLS0tKaCgoKCgoKClJSUgoKClJSmooKCgoKmxoLGoqaigoKUuMSCgpaCgoKUgoSCpqbGopSkpIKClOiigoKUgpSAgJKmlIKCgpSCgoKmsoKCgoKCgoKClJS0xoKCpqaygqaCqLKCpoKCgpSmooSCgpSCpqKCpqKCpqKCgqaigoKmopSCgqaCgpSCpqKCgoKCgoKCgoKUpoKCgoKClKaipqKCgoKmgoKClIKCgt6ClJSClKaygoKCgpSClIKCgoKCgoKCgoKClIKUtIKUgoKC2MaCgoKCloKClJS6goKCgoKCgoKUuIKmgoKUpqKCgoKCgoKCgpSClKamooKCgoKCgqaClKaigoKUgoKCgpSCgpSUpqKCgoKClIKClJTGsoKCgpSmxoKmooKCgpSCgoaUlLSCtIKUtLTaooKUgpSCgoKkgsa0tKSkpIKUpqKCgoKmgoKCgoKCgoKCgqaClKaigoKCgoKUlKYAAhiCgpSCgqaCgoKUpoKCgpqCgoKUgpSmgoKCgoKUlIKCgoI=", "207-213:1;477-479:1;481-483:2;485-487:3;489-491:4;493-495:5;498-500:6;560-566:1;841-841:1", "", "372=NewTextRange/1/1/1,newNodeList/1/1/1;998=lookAhead/1/1/1;1326=Name/1/2/2,IsIdentifier/1/1/2,Name/2/2/2,AsQualifiedName/1/1/2,textsEqual/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/parser/parser.go", "parser.cs", "AGLQAYKCgqaYlKKmzIKmooKmooKCgoKClPaigoK4gqaigoKUgoKUlIKCpKSkhpTGgoKUtKiCgpSmgoLKgoCClKSCgpSCgoKUgsaigoKUxqKClJSkgpSkgpSkgqSClKTGosiigoKClIKU2KKCgoKCgvaigqaClJSCgoKClLS0tIKCpqKmoqaipqSCgoKUggARHIIACxiCgoKCgoKCgoKmooKCgqaEhJSCpoKCpoKCpoKCpoKmgqaCgpSCgpSClKaigoKUgoKCgoKCgpSCgpSCgoKCgoKCpoKClKaygoKCgoKCgoKCgoKChIKUgoKmgoKUgoKUpqKCiIKCgpSmprKClIKClIKChIKClpaCgqiUgoKClJSmhIKClIKCgoKCgpSCgoSUhIKClN6CqIKClpaCuIKClKaygoKCgoKCgoKChIKUppSClIKmgoKmooKCqLKCgoKCgoKCgoKElIKElIKYgpSegpSKlJSClIKmgqqigoKClKaCpoKCgqiigoKUgqimgoKCgrjWgpSClMa0tLS0tLS0gpTGtLS0tLS0gpTGtLS0tIKUxrS02KIAARCkpKyopJSkxqSkqIKUgpqkpKSmgpSkpKSkpKiClIKUpKSkpKaigpSWpKQAAhSmpqSmpqSkpKSmgoKClIKUgqaCgoKUgoKUgoK4goKClKaCpoKmgoKClJaClJSmgoKCgqaCgoKClKaCgpSmgoKCgpSClKaCgpSmopSkpKSCxoLGgsakpKSkpKSkpKSkpKSkpKqCxqaogoKCgoyClIKCgoKUuKKUpILGpKSkpKSkpIKUpKTWhoKUxqLGhqaCgqaigoKCgoKCgoKCgoKCgpSUgoKmooKCgoKCpqKCgoKCgoKCgoKClIKCpqKCgoKCgoKCgoqCgoKmooKCgoKCgoKCgoKmooKCgoKCgoKIlKaClIKCtIKCtIKCgpSCgoKUgrSCgqaigoKCgoKCgqaigoKCgoKCgqaCgpSmooKCgoKClIKCgqaigoKCgoKCgoKCgqaigoKCgoKCgoKmooKCgoKCgoKmooKUpqKCgoKCgoKCpqKCgoKCgoKCgoKmpoKCjIKClJSClIKCqKKCgoKCgoKYgoKClIKCpqKCgoKCgpSCgqaigoKCgoKCpqiCgoKUgoKCpoKUgoKUgqaigoKCgoKmooKClLS0tLSClIK0tAAAFIKClIKCgpSCpoKmgoKmgqaCpqKCgoKCgpSCgoKUgoKCpoKmooKUgpSmooKCgoKCgoKmooKCgoKEgoKUpqKCgoKCgoKCpqKCgoKCgoKClIKUgqaigpSmooKUprKChIKClIKCgoKUgoKCgoKCgqaCpqKmsoKChIKCiJSCgoaClJSCgoKUgpSUgoKCgoKCgoLcpqyCgoKClKaixqKmosamgpimooKCgoKClILGgsiigoKClILGooKUzKKClIKopqKCgoKUgqaigoKCgoKClIKClIKUgpSCgoKmgpiCgoKClIKCgoKUpoSCgpamooKCgoKmooKCgoKCpqKCgoKCgoKCgoKClIKmgqaigoaCgpSmooKCgoKCgoKCxqLGooKClIKCgoKCgqaCgoKUgoKClIKClJSUgpSCgpSmqIKCloKClIKClISUgqakgqSCpIKkgqaCgpSCgpaClsaCgoKmxoKClLiigoKCgoKCgoKmooKClIKCgoKClJSCgoKCpoKuooKCgoKCgqaigoKCgoKCgoKClJSCgoKCpqKChIKClIKCpqaigoKChIKWlIKClIKClJSCgoKmooKCgoKUlKaigoKClJSCgoKCgoKUlIKCgoKmooKEgoKClIKGgoKClIKCgpSUgoKCgriCgoKUgoKCgoKCgoKmgoKmgqaGpqKCgoKCgqaigpSmooKCgoKCgoKCpqKCmqaogoKClKYAAhKCgoKClIKUlIKmgoKmhoKCgoKmooqCpoKCgoKClIKClIKmAAQUgoKCggAAEISChIKGgoKCloKClIaCgpaClIaCpoKCgqaCpqaCpqKUgpSClKaigoKUlKaigoKCgoKUlIKCgoKCgoKmooKCgoKChIKCpqKCgoKCgoKCgoKClIKUiIKCpoKClJSCgoKCgoKmgoKmooqCpqKCgoKCgoKqooKCgoKUgoKEgoKCgoKCgqaCpoKmgqaygoKCgoKUlIKCgoKUgpSmgpSkpNiigpSkpKaigoKmooKCpqKCgoKmooKCgoKmgqaigoKClIK2gpSCtIKCgoKUgsa2pqKCpqKmgoSClIKmgqSmgqSkpqSClKSkgoKUpIKUpIKUpKSkpIKUpKTYgoKCgqaCgoKmsoKmgoKCpqKCgqaihIKmooKUgoKCgpSCgpSmgoKClIKClJSClKaigqaCpqKCgoKUlIKGlJSmAAIoiIKUgoKClIKUgoKUlIKCgqaCgoKClKaCpoKCgoKmgoKmgoKmgoKmgoKmooKUpqKClKaCgqaigoKCgoKCgoKCgoKCgpSUlIKCgoKCgoKCyoKCgpSCpqKCgoKCgpSClJSCpqKCgpSCgoKCgoKCgoKCgsqUpqKCgoSCgpSmgoKClIKUpqKCgoKCgoKmgoKCgpSCgoKCgqaCgoKCpqKCgoKCpqKClIKUgoKCgpSClIKUpoKCpqKCgoKUgoKCgoKClJSCgqaigpSmooKCgoKCioIAARKmgoKUgqYAAhyCgoKUpgACHIKCgoKCgoKUlIKmgqaigoiCgoKCggAGEIKUgpSCgpQABhCCpoimpoIAABKUpqKClKaCgoKCgoKEgoKUpqKCgoKCgoKUlKaGgpamooKCgoKChIKUlIKClKaigoKCpqKClIKUgpSmqIKIgoKmooKCgpSCgqamooKCgoaCgoKCpqKmAAAigoKUgoKClIKWmoKYgpiCpqKCgoKCgqaigoKChoKCgpSIgoKUlIKCpqKCgqaigoKClKaigqaigoKCgoKCgoKCgpSmgoKUpoKmooKClIKCgoKCgpSmooKCgoKmooKCgoKUlIKClKaigqaCgpSCgoKmgoKClKaigoKCgoKCpqaigqaCgoKUgqaCgoKClJSCpqiCgoKCuLiClKairKKCgoKCgoKCgoKClJSCgqaCgoKCgoKUpoKmooKGlIaKlISmpqKElIKCgpSEgoKUpoKmsoKCgoqCgoKCgoKCpoKClIKUgoKUuIKUpqKCgoKmooaCgoKClKaigoKGgpSUgoKClIKmpqKCgpSCpqKCgpSCpqKWpIKClIKUpKSCpIKkyKKUpKSkpoKmgqaCpoKmgqaCpoKCpoKmgqaCpoKCgpSmgqasgoKCgoKCgpSUgqaCpoKmAAIYggABGoKClIKCAAEegoKIggABEIKWpqKGggACIpSmooqCgoKYlKaigoKCgpSIlqaigoKClIKmgoKCioKUpAABEIKYgpqChJSagpSYpISClqamlIaCloKCgoKCgpSkgoKUlIKUlriigoScgpSCgoKUpqKCgoKCAAAQgoKCgpSUgoKClJSUgqaCgoIAARiCgpSEmIKCgoKUAAEgjoKmgoKCpoKCgoKClMiilKSkpMaigpQAAB6UgoKCgoKmpKaigoKUgoKUpqSCgoKCgpSmiIKGgpaClIKUprKCgoKCgoKCpqSCgpiCgoKCgoKClJSmooKCyoKClMaygoaCAAAsgpSClIyClIiCgpSClJaCgoK4gqamoqaipoKmroKCgoKUAAEWgoKCgoKClIKmpoKUpKSmooKCgoKChJSCgoKClKaygoKClIKCgoyCgoK8gpSCgqaCgpSCgoSWuIK0trQAAxaCgoKUgoKCgpSmsoKCgoKCgoKClIKIpoKmspiCmIKCpqSkpKSkpoKCgoKmooKClIKCgoKalIKCgpSmgoKmgoKmgoKmsoKCgoSClKamooKChIKUgoKClIKCiIKUgoKClKaUpqKMgoKUgoKUpoKCgoKCgoKUgoKUpqKCpqKClIKmooKCgoKCpoKCgoKCgpSmooKClIKUgpSUpoKCgoSClKamoqSkpKSogpqkgpSk2KKCgoKmooKCpqKCgqaigoKmooKClpSmooKCpqKCgoKCgoKmAAJAgoKCjIKChIKCgoKCpqaUgpSapoKmooKCgoKCgoKCuIKYhKaCpqqClIKCgoSCioK4gqainKqopgACYIKCprKCgoKCgpSUgoKWgoKUhIKCgpSUlIKCgoKUgoKCpriipoKCprKCgoKClIKCgoKmpqKCloKCgpSEgoKUpqaigoKUlIKCprKCgoKCgoKCgqaEgoKUgoKCgoKUhIKClJSmooKCgqaCpqKUpKSmooKCgqaygoKClJSCpqKCpqKCgoKCgoKmpqKCgoKmoqSClKSkpKSkuoKUtKSkpKSCtqSkpqKCgoKCgoKCpqKCgoKCgoKmooKCgoKCgqaigoKCgoKClIKClIKUgoKEhIKUgp6CgoKCgoKUlIKClIKCpqyCgoKCgoKCgoKCgpS0tLS0goKCgoKCgoKCpoKClKaigoKCgpSCprSClIKCyqKCgoKClIKChIKClIKUgoKUgoKmgoKCgqaCgoKCgpS0tLS0tLSCpoKClKaCpoKCgoKmgqaCpoKmgqaCpoKCgoKUlIKClIKClJSWgoKCgpSUgoKClKaCgpSmpoKCgqaCgoKmgqaigoKCgpSCpqKCgqaCpoSClISCgpaCloKCmIKUpoKCgpaCjoKUlIKYgoKWgpaEgpierpSmhqaCgpSElKaCpoKmogABEKSkqKqmyIKmooKWpKQAAi6kpoKEgpSGlLSCpIKkgoSUgoKUtIK0uKKClJ6sgpSmopqkpKaiAAESpKSopKaCgqaigqairoKmgqaClKSmgoKmgoKogoKcgpSmhKaCpoKClKaCpoIAABCClKaCpqKmooKmiKqCgqaigoKUpoKmgqaipoKmgoKU2KKCgoKCxoKmgqaCpoKmgqaCxoKmgoKCgqamooKClKaCgoKCgpSCgoKClIKCgoKClIKClIKClIKCgpTKlOyCgoKClOyCgoKCgoIAARCCgoKClIKCgoKCgoLK7pSUpoKmgoKUpoKClKaCgpSCgpSmgoKCgpSUpoKCgpSmgoKUgoKUgoKClIKUxqKCgoKEgqSCgoKCgoLIgoKUAAgO/PzoggAKEOqigoKUgoKUgqaipqKCgqaCgoKCpoKCgoKCgoaEgoSCgoKCpoK6ugAQEqKClKSClKaCgoK2graCtoK2grakgraCtqSkpKSkpKaEpoCCpKSCgsiCuICCxg==", "147-150:1;176-178:1;184-184:2;534-536:1;539-541:2;598-600:3;647-647:1;1828-1830:1;3353-3359:1;4348-4362:1;6371-6373:1;6734-6736:1;6833-6833:1;6849-6849:2", "", "414=Mark/1/1/1;1453=lookAhead/1/2/1,lookAhead/2/2/3;1953=Some/1/1/3;2076=IsOptionalChain/1/1/1,Name/1/1/2,NodeIsPresent/1/1/2,Expression/1/1/3,isValidHeritageTypeReferenceExpression/1/1/3;2088=convertEntityNameExpressionToEntityName/1/1/1,Name/1/1/2;2532=isIdentifier/1/2/1,lookAhead/1/1/1,isIdentifier/2/2/2,tokenAfterImportDefinitelyProducesImportDeclaration/1/1/2;3616=createIdentifier/1/1/3,parseTypeAnnotation/1/1/5;3633=parseNameOfParameter/1/1/3,parseOptionalToken/1/1/4,parseTypeAnnotation/1/1/5,parseInitializer/1/1/6;4048=lookAhead/1/2/1,lookAhead/2/2/3;4875=AsJsxElement/1/3/1,TagName/1/4/1,AsJsxElement/2/3/1,TagName/2/4/1,TagNamesAreEquivalent/1/2/1,TagName/3/4/2,AsJsxElement/3/3/2,TagName/4/4/2,TagNamesAreEquivalent/2/2/2;4879=AsJsxElement/1/2/1,Children/1/1/1,NewJsxElement/1/1/1,AsJsxElement/2/2/2,Pos/1/1/2;4950=AsJsxElement/1/3/1,TagName/1/4/1,AsJsxElement/2/3/1,TagName/2/4/1,TagNamesAreEquivalent/1/2/1,TagName/3/4/2,AsJsxElement/3/3/2,TagName/4/4/2,TagNamesAreEquivalent/2/2/2;4969=TagName/1/1/1,GetTextOfNodeFromSourceText/1/1/1;6279=isBindingIdentifierOrPrivateIdentifierOrPattern/1/1/1,IsModifierKind/1/1/2,isStartOfType/1/1/4;6505=Pos/1/2/3,Pos/2/2/3,NewTextRange/1/1/3;6553=Pos/1/2/3,Pos/2/2/3,NewTextRange/1/1/3;6772=Pos/1/1/2,SkipTrivia/1/1/2,End/1/1/2,NewTextRange/1/1/2;6776=Pos/1/1/2,SkipTrivia/1/1/2,End/1/1/2,NewTextRange/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/parser/references.go", "references.cs", "AAoWooKmgoKCyrKCiIKCgoSChIKEypSCjIKCgoyCgg==", "17-20:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/parser/reparser.go", "reparser.cs", "AA8asoKCpoKsooKClKaigoKCpqSClIKCgpSUqoKCgoKClIKCgtzClIKClIKCgoKUgoKClLS0tIKCgoKCtIKClIKCgoKUgoKCgoKCgrSCgpSCgsyCtoLq0oKClLS0tLS2gpSCgoKCgoKCgoKClIKGgpSClIKCgoKUgpSmgoSCgoKClJSCgpSmgoKUlJSUgoKUlIKUgoKUgqaygpSCgoKCgoKUgoKClIKUlIKClIKClIKCgpSClKbCgIKCgoKCgoLIsoKCgoKGgpSClIKClIKCgoKCgt6UlIKUgqaClLjClJSCgoKCguyCgoLGgoKCxoKCgoKC2IK6gsaAgoKCguiUgoKCuoLqgrrGgoK6xoK6xoKCgroACAqAgoKClIKCgoKUgoKCguiAgoKCgpSCgqbogIKCggAGEIKUlIKCgqaC6ICCgoLogpSogpSkgpS0tLS0tIKCgoKCgoKClIKUgtaAgpSCgIaCgraUgpSCgpSU1oCCgIaCgoKCgoKUggALEKKCgoKClMbCgoKCgoKCgriCgoSUgqamooKUpqKClILGtLS0goKUxqKCgpSUgsaigpS0tMaigoKCgqyigpSCgoKUlAACFrKCnIKCgoKClIKCgg==", "394-394:1;572-574:2;595-597:3", "", "150=Modifiers/1/1/1,DeepCloneReparseModifiers/1/1/1,addDeepCloneReparse/1/2/3,addDeepCloneReparse/2/2/4;364=Modifiers/1/1/1,DeepCloneReparseModifiers/1/1/1,Name/1/1/2,checkNonIdentifierName/1/1/2,addDeepCloneReparse/1/3/2,Type/1/1/3,addDeepCloneReparse/2/3/3,AsTypeParameterDeclaration/1/1/5,addDeepCloneReparse/3/3/5;437=TypeExpression/1/1/1,Type/1/1/1,addDeepCloneReparse/1/1/1,Expression/1/1/2;464=TypeExpression/1/1/1,Type/1/1/1,addDeepCloneReparse/1/1/1,Initializer/1/1/2;477=TypeExpression/1/1/1,Type/1/1/1,addDeepCloneReparse/1/1/1,Initializer/1/1/2;488=AsJSDocSatisfiesTag/1/1/1,Type/1/1/1,addDeepCloneReparse/1/1/1;498=TypeExpression/1/1/1,Type/1/1/1,addDeepCloneReparse/1/1/1,Expression/1/1/2;510=TypeExpression/1/1/1,Type/1/1/1,addDeepCloneReparse/1/1/1;569=NewIdentifier/1/1/3;748=Name/2/4/1,Text/1/3/1,Name/3/4/1,Text/2/3/1,Name/4/4/1,Text/3/3/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/parser/utilities.go", "utilities.cs", "AAsWgpakpoKmgqailIKUgsaCyIKCgriCpoI=", "43-47:1")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("parser", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/parser")]
public static partial class parser_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct jsdocScannerInfo {}
    internal partial struct jsdocState {}
    internal partial struct propertyLikeParse {}
    public partial struct JSDocInfo {}
    public partial struct ParseFlags {}
    public partial struct Parser {}
    public partial struct ParserState {}
    public partial struct ParsingContext {}
    public partial struct ParsingContexts {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸunicode() => builtin.initPackage(typeof(unicode_package));
    [GoInit] internal static void initᴛᴛimportꓸunicodeꓸutf8() => builtin.initPackage(typeof(go.unicode.utf8_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(parser_package.Parser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class parserᴛParser;
// </MethodHolders>
