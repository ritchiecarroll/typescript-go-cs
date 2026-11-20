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
global using astꓸBindingElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸBlockOrExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCatchClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸClassElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸClassLikeDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸColonToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸConciseBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEqualsGreaterThanToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸEqualsToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExportSpecifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸForInitializer = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFunctionBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸHeritageClauseElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸHeritageClauseList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸKeywordExpressionSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleExportName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedExportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNodeBody = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸSourceFileNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸStringLiteralNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateLiteral = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTemplateSpanList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using transformersꓸTransformerFactory = System.Func<go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.TransformOptions>, go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer>>;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.transformers.estransforms_package;

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
[assembly: GoFieldAccess(typeof(astꓸBinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(astꓸBindingElementList))]
[assembly: GoFieldAccess(typeof(astꓸBlockNode))]
[assembly: GoFieldAccess(typeof(astꓸBlockOrExpression))]
[assembly: GoFieldAccess(typeof(astꓸDeclarationName))]
[assembly: GoFieldAccess(typeof(astꓸElementList))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸForInitializer))]
[assembly: GoFieldAccess(typeof(astꓸHeritageClauseElementList))]
[assembly: GoFieldAccess(typeof(astꓸNodeBody))]
[assembly: GoFieldAccess(typeof(astꓸParameterDeclarationNode))]
[assembly: GoFieldAccess(typeof(astꓸParameterList))]
[assembly: GoFieldAccess(typeof(astꓸStatement))]
[assembly: GoFieldAccess(typeof(astꓸStatementList))]
[assembly: GoFieldAccess(typeof(astꓸTemplateSpanList))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationListNode))]
[assembly: GoFieldAccess(typeof(astꓸVariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(asyncTransformer))]
[assembly: GoFieldAccess(typeof(classFieldsTransformer))]
[assembly: GoFieldAccess(typeof(classInfo))]
[assembly: GoFieldAccess(typeof(classLexicalEnv))]
[assembly: GoFieldAccess(typeof(classLexicalEnvironment))]
[assembly: GoFieldAccess(typeof(esDecoratorTransformer))]
[assembly: GoFieldAccess(typeof(forawaitTransformer))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ArrayLiteralExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ArrowFunction))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Block))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BodyBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ElementAccessExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ForInOrOfStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ForStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HeritageClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.MethodDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ModifierList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeVisitor))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ObjectLiteralExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PostfixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PropertyAccessExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ShorthandPropertyAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SyntheticReferenceExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TaggedTemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateLiteralLikeNodeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TemplateSpan))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TryStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableDeclarationList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.VariableStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]
[assembly: GoFieldAccess(typeof(lexicalEntry))]
[assembly: GoFieldAccess(typeof(memberInfo))]
[assembly: GoFieldAccess(typeof(objectRestSpreadTransformer))]
[assembly: GoFieldAccess(typeof(privateEnvironment))]
[assembly: GoFieldAccess(typeof(privateIdentifierInfo))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/async.go", "async.cs", "ACFKooKCgoKCxqKCpoKCgoKmgoKUuIKmgqaCpqKCgoKCgpSmgqaigpSCntyIgsamgqbSgoKmgpSClqSkpKSkpKSkpKSk+LKClKSkpKQAAhi2pqKCgqiCgoKClLiCgoKCgpSmsoKCgoKUlKaygoKUpgAHErKCgpSmAAcSsoKCgpSmAAcYpIKUqIKCpqKCgoIABxKCrqKCgoKUgoKCgpSCpgAKGIKmooKCggAIFIKmooKCggAIFIKuooKCgpSCgoKClIKmAAkWgq6igoKClIKCgoKUgqYACRaCrtqCgoKQtoKUgoKCgpSCpgANGLKCgpSCgoKCgtyirMKUgoKCuIKCgoKClJSUpoKSlKaigriigoKUgoKCgoLcooKClJSCgqaigoKUgpSCgoK4pqKCgoKCgoKCgoKUgpaWgoK4goKCgpSmgoKUuIKCgoKCpoLMgoKCpqKCpoKCgoaCAAYQlJgABhCUgoKmsoKCgoKCgoKCgoKCpoKCpoKCgsqCgoaCgoKClIKCgoKUlJQAAxSCgoKmlILOhIKCpoKCloKCuIKmAAgWqJSCgpS4lN6CgoKUAAYQgoKCgoKCgoKIgoSUpqKCzoKCgoKCgoLOopSkpKSClILGgsaCAAgKpKiigoKUgoKUxoKCgpTIgoKCgqaoggABIqQ=", "474-474:1", "", "113=Text/1/1/1,IsIdentifierName/1/1/2,IsLabelName/1/1/3;268=Visitor/1/1/4,VisitNode/1/1/4,VisitEmbeddedStatement/1/1/5;287=Visitor/1/2/2,VisitNode/1/2/2,Visitor/2/2/4,VisitNode/2/2/4,VisitEmbeddedStatement/1/1/5;307=Visitor/1/2/3,VisitNode/1/2/3,Visitor/2/2/4,VisitNode/2/2/4,VisitEmbeddedStatement/1/1/5;321=Visitor/1/1/2,VisitNode/1/1/2;335=Visitor/1/2/2,Modifiers/1/1/2,VisitModifiers/1/1/2,EmitContext/1/1/4,Visitor/2/2/4,VisitParameters/1/1/4,transformMethodBody/1/1/7;365=Visitor/1/1/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Name/1/1/4;386=Visitor/1/2/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Name/1/1/3,EmitContext/1/1/5,Visitor/2/2/5,VisitParameters/1/1/5,transformMethodBody/1/1/8;405=Visitor/1/2/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Name/1/1/3,EmitContext/1/1/5,Visitor/2/2/5,VisitParameters/1/1/5,transformMethodBody/1/1/8;436=Visitor/1/2/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Visitor/2/2/4,Name/1/1/4,VisitNode/1/1/4;468=Visitor/1/2/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Visitor/2/2/4,Name/1/1/4,VisitNode/1/1/4;510=Visitor/1/1/2,Modifiers/1/1/2,VisitModifiers/1/1/2;546=IsVariableDeclarationList/1/1/1,AsVariableDeclarationList/1/1/3,ContainsFunc/1/1/3;662=getOriginalIfFunctionLike/1/1/1,GetFunctionFlags/1/1/1;696=Factory/2/2/4,NewIdentifier/1/1/4;719=Factory/2/3/2,NewToken/1/1/2,Factory/3/3/3,NewUniqueNameEx/1/1/3;730=Factory/2/2/3,Name/1/1/3,NewGeneratedNameForNodeEx/1/1/3;770=Factory/1/1/1,NewUniqueName/1/1/1;807=AsBlock/1/2/1,EmitContext/1/1/2,StatementList/1/1/2,EndAndMergeVariableEnvironmentList/1/1/2,AsBlock/2/2/3;812=Size/1/1/1;832=Factory/1/2/1,Factory/2/2/2,NewAwaiterHelper/1/1/2,NewReturnStatement/1/1/1;841=EmitContext/1/1/1,Factory/2/2/1,NewNodeList/1/1/1,EndAndMergeVariableEnvironmentList/1/1/1;867=AsBlock/1/2/1,EmitContext/1/1/2,StatementList/1/1/2,createCaptureArgumentsStatement/1/1/2,MergeEnvironmentList/1/1/2,AsBlock/2/2/3;894=AsBlock/1/2/1,StatementList/1/1/2,VisitNodes/1/1/2,AsBlock/2/2/3", "", "492:25,493:17")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/classfields.go", "classfields.cs", "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", "1435-1437:1;1442-1446:2;1699-1712:1;2055-2059:1;2066-2068:2;2316-2318:1;2319-2321:2;2507-2509:1;2542-2544:2;2545-2547:3;3548-3550:1", "", "109=IsTrue/1/1/3;156=Name/1/2/1,Name/2/2/1,IsComputedPropertyName/1/1/1;382=IsSuperProperty/1/1/1,isStaticPropertyDeclarationOrClassStaticBlock/1/1/2;580=AsParenthesizedExpression/1/1/1,Factory/2/2/2,InlineExpressions/1/1/2;841=Factory/2/3/2,Factory/3/3/2,NewNodeList/1/1/2,NewBlock/1/1/2;851=Visitor/1/1/2,Modifiers/1/1/2,VisitModifiers/1/1/2,Name/1/1/3;864=Modifiers/1/1/2,VisitModifiers/1/1/2,Name/1/1/3,visitPropertyName/1/1/3,Visitor/1/1/6,VisitNode/1/1/6;888=Factory/2/3/2,Factory/3/3/2,NewNodeList/1/1/2,NewBlock/1/1/2;903=Modifiers/1/1/2,VisitModifiers/1/1/2,Name/1/1/3,visitPropertyName/1/1/3,Visitor/1/1/6,VisitNode/1/1/6;932=HasStaticModifier/1/1/1,shouldAlwaysTransformPrivateStaticElements/1/1/1;999=AsNode/1/1/1,IsSuperProperty/1/1/1,Name/1/1/1,IsIdentifier/1/1/1,isStaticPropertyDeclarationOrClassStaticBlock/1/1/2;1005=Factory/2/2/2,Name/1/1/2,NewStringLiteralFromNode/1/1/2;1033=AsNode/1/1/1,IsSuperProperty/1/1/1,isStaticPropertyDeclarationOrClassStaticBlock/1/1/2;1039=Visitor/1/1/2,VisitNode/1/1/2;1164=VisitNode/1/1/2;1190=AsPropertyAccessExpression/2/2/1,Name/2/2/1,accessPrivateIdentifier/1/1/1;1199=Factory/2/4/2,Factory/3/4/2,NewIdentifier/1/1/2,NewPropertyAccessExpression/1/1/2,Factory/4/4/5,NewNodeList/1/1/5;1207=Factory/2/4/2,Factory/3/4/2,NewIdentifier/1/1/2,NewPropertyAccessExpression/1/1/2,Factory/4/4/5,NewNodeList/1/1/5;1216=IsSuperProperty/1/1/1,isStaticPropertyDeclarationOrClassStaticBlock/1/1/2;1217=Visitor/1/2/1,VisitNode/1/1/1,Visitor/2/2/3,VisitNodes/1/1/3;1232=AsPropertyAccessExpression/2/2/1,Name/2/2/1,accessPrivateIdentifier/1/1/1;1234=Factory/2/4/1,Visitor/1/2/1,VisitNode/1/2/1,Factory/3/4/1,NewIdentifier/1/1/1,NewPropertyAccessExpression/1/1/1,Factory/4/4/4,Visitor/2/2/4,VisitNode/2/2/4,NewNodeList/1/1/4;1240=Visitor/1/1/5,VisitNode/1/1/5;1249=IsSuperProperty/1/1/1,isStaticPropertyDeclarationOrClassStaticBlock/1/1/2;1250=Visitor/1/1/1,VisitNode/1/1/1;1256=Visitor/1/1/5,VisitNode/1/1/5;1321=nodeHasTransformPrivateStaticElementsFlag/1/1/1,Some/1/1/2;1333=VisitNode/1/2/3,Visitor/1/1/6,VisitNode/2/2/6;1369=visitInvalidSuperProperty/1/1/3,Visitor/1/1/6,VisitNode/1/1/6;1399=Factory/2/2/4,GetNonAssignmentOperatorForCompoundAssignment/1/1/4,NewToken/1/1/4;1469=createPrivateIdentifierAccessHelper/1/1/2,Factory/2/2/4,GetNonAssignmentOperatorForCompoundAssignment/1/1/4,NewToken/1/1/4;1586=EmitContext/1/2/1,classHasClassThisAssignment/1/1/1,EmitContext/2/2/1,classHasExplicitlyAssignedName/1/1/1;1653=Factory/2/2/2,Visitor/1/1/2,VisitNode/1/1/2,NewAssignmentExpression/1/1/2;1692=Name/1/1/2;1729=Factory/2/2/2,GetLocalName/1/1/2;1738=Factory/2/2/2,GetLocalName/1/1/2;1850=nodeHasTransformPrivateStaticElementsFlag/1/1/1,Some/1/1/2;1852=Members/1/1/1,Some/1/1/1;1879=Name/1/1/3;1966=IsClassStaticBlockDeclaration/1/1/1,IsPropertyDeclaration/1/1/2,HasStaticModifier/1/1/2;2009=HasStaticModifier/1/1/1;2037=Factory/2/5/3,NewNodeList/1/2/3,Factory/3/5/6,NewToken/1/1/6,Factory/4/5/7,Factory/5/5/7,NewNodeList/2/2/7,NewBlock/1/1/7;2046=Factory/2/3/1,Factory/3/3/1,NewNodeList/1/1/1,NewCallExpression/1/1/1;2091=Factory/1/4/1,Factory/2/4/3,Factory/3/4/4,NewIdentifier/1/1/4,Factory/4/4/6,NewNodeList/1/1/6,NewNewExpression/1/1/3,NewAssignmentExpression/1/1/1;2181=AsTryStatement/1/1/1,Factory/2/2/2,UpdateBlock/1/1/2;2278=Factory/2/6/1,Factory/3/6/2,NewKeywordExpression/1/1/2,Factory/4/6/5,Factory/5/6/6,Factory/6/6/6,NewIdentifier/1/1/6,NewSpreadElement/1/1/6,NewNodeList/1/1/5,NewCallExpression/1/1/1;2299=AsBlock/1/1/1;2425=AsComputedPropertyName/1/1/1,Factory/2/2/2,NewGeneratedNameForNode/1/1/2;2439=Factory/1/1/1,Visitor/1/1/3,VisitNode/1/1/3;2445=Factory/1/1/1,Visitor/1/1/3,VisitNode/1/1/3;2469=Expression/1/5/1,IsCommaExpression/1/1/1,EmitContext/1/1/2,Expression/2/5/2,AsBinaryExpression/1/3/2,IsCallToHelper/1/1/2,Expression/3/5/3,AsBinaryExpression/2/3/3,IsVoidExpression/1/1/3,Expression/4/5/4,AsBinaryExpression/3/3/4,Expression/5/5/4,IsNumericLiteral/1/1/4;2497=Factory/3/13/1,Factory/4/13/1,NewIdentifier/1/4/1,Factory/5/13/1,NewTrueExpression/1/3/1,NewPropertyAssignment/1/4/1,Factory/6/13/2,Factory/7/13/2,NewIdentifier/2/4/2,Factory/8/13/2,NewTrueExpression/2/3/2,NewPropertyAssignment/2/4/2,Factory/9/13/3,Factory/10/13/3,NewIdentifier/3/4/3,Factory/11/13/3,NewTrueExpression/3/3/3,NewPropertyAssignment/3/4/3,Factory/12/13/4,Factory/13/13/4,NewIdentifier/4/4/4,NewPropertyAssignment/4/4/4;2515=Factory/1/2/2,Factory/2/2/3,createPrivateInstanceMethodInitializer/1/1/3,NewExpressionStatement/1/1/2;2525=AsPropertyAccessExpression/1/1/1,Factory/2/2/2,NewVoidZeroExpression/1/1/2,Name/1/1/4;2532=AsElementAccessExpression/1/2/1,Factory/2/2/2,NewVoidZeroExpression/1/1/2,Visitor/1/1/4,AsElementAccessExpression/2/2/4,VisitNode/1/1/4;2634=Factory/1/4/1,Factory/2/4/3,Factory/3/4/4,NewIdentifier/1/1/4,Factory/4/4/6,NewNodeList/1/1/6,NewNewExpression/1/1/3,NewAssignmentExpression/1/1/1;2894=Factory/1/1/2,Visitor/1/1/2,VisitNode/1/1/2,NewAssignmentExpression/1/1/2;2911=AsBinaryExpression/1/2/1,AsBinaryExpression/2/2/5;3002=AsArrayLiteralExpression/1/3/1,AsArrayLiteralExpression/2/3/2,VisitNodes/1/1/2,AsArrayLiteralExpression/3/3/3;3007=AsObjectLiteralExpression/1/3/1,AsObjectLiteralExpression/2/3/2,VisitNodes/1/1/2,AsObjectLiteralExpression/3/3/3;3022=NewIdentifier/1/1/4,NewPropertyAssignment/1/1/4,NewNodeList/1/1/3,NewObjectLiteralExpression/1/1/2;3058=IsPropertyDeclaration/1/1/1,HasStaticModifier/1/1/1;3069=Initializer/1/1/1,HasStaticModifier/1/1/2;3107=IsMethodOrAccessor/1/1/1,IsAutoAccessorPropertyDeclaration/1/1/1,Name/1/1/2,IsPrivateIdentifier/1/1/2;3146=Factory/2/3/1,Factory/3/3/2,NewAssignmentExpression/1/1/2,NewParenthesizedExpression/1/1/1,Name/1/1/5;3188=Factory/2/2/4,NewNodeList/1/1/4;3202=Factory/2/2/3,NewIdentifier/1/1/3;3209=Factory/2/3/1,NewPropertyAccessExpression/1/1/1,Factory/3/3/7,NewIdentifier/1/1/7;3218=Factory/2/2/4,NewNodeList/1/1/4;3242=AsBinaryExpression/2/2/1,flattenCommaListWorker/2/2/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/classthis.go", "classthis.cs", "AAgUsoKCgoKCgoKCgs4=", "", "", "22=ClassThis/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/definitions.go", "definitions.cs", "AAgSgpqSkrKiopaigpSkpKSkpKSk")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/esdecorator.go", "esdecorator.cs", "AFX4AaiElIKCgoKCgoKCgoKCgoKClJSCgpSUgoKUlMaCgoKCgpSUtIKCtIKCgoLqgtyCpoKCgoKmgoK4goKCpqaCgoKCpoKCuKaCgoKmgoKClMqCuIKCgoKUgoK4goKCgoKCgpSmooKUgoK4lKaiqqKClIKUmqSkpIKkpqSkpKSkpKSkpKSkpKSsgoKCpMiigpSmspSkpKSkpKTIopSkpKSkyIKClKaCgpSmooKUxrKCgpS0gsa0tLaClIKUgpSClMaCzIIACBaiggAGFoKKgoKUpoKCgoKmgoKUlIKCgpSUpoKmgoKClJTKgoKUgoKUuIKoirimooKUmIKmgoKCgoKCggABFIKClKamuKgABhCCgrqCgoKCgqaCgqaEvIKChrqUgoKCpoKClAACIpSYgpaKgqSClIKUgrqUloKCgoKCgoKClIKCuoLOggAEEIKCqJaWlp6CgoKClJQABhCCgpiCgoKqmIKCgoKUqIKCgoKUlKiCggACFIKCzIgAAhSCgs6CloKCgoK6hIKClKiCqIKmgoKmlIKCgoKCAAIygoKCgpSUAAQQgoKmgoKCgsqCqIKCgoKClIKClIKUgqbGgsiigoKClIKClIKClKaClIKCpoKCiIKCgoKUgoKCgpiCgoKCpoaClIKCgoKCgoKUhoKCuKiCgoKCgqaigoKCpoKCgoKCpqqClIKmgqaChIKCuIKEgoKUiJSCgpSCgpSClIKogoKmpqKCgpSCgoaCgoKClIKClIKCpoKCuIKUgqaChoKUABIesoKUgoKCgoIAAhKCgoKClIKUgoKogoKCAAEQgpS0tLS0tLiCgoKCgoKCgoKCgoKUgoKCgrgACRwAACCCgpSUgoaCgoKClKYABhCCgoKCgoKCgoKmgoKUpoKCgoKClKwABhCCgriCgoKmhqaqooKClJSCgpSmuLKCgoKClIKCgoLMsoKCgoKUgoKCgsyygoKCgpSCgoKCzIKClIKChoKUgoKCgpiCgoKChIKCgoKUgoKIgoKCgpSCgoSCyoKmsoKmmgABEoKUgoKUlJSCgoKClJSClKaCpoKCgoKCpoKCgoKCgpSUzIKCAAYQlAAAGoKYgoKCgoKCgpSCgoKCgoKCuJSCgoKClIKCgpSCgoKUpoLMgoKUpqKCgoKCgoKClKaigoKCgoKCgpSmooKCgoKCgpSmooKCgoKCgpQAAiiigoKCpgAHEoaCgoKClAACXqKClMairoKClILGooKCAAcSgqaigoKUgoKCpgAANoKCpoKCgoKClIqCgoKCgoKUgoKC3pSCgoKUgoKUgoKCgoKUyoKCgoKUlKamooKUgoKCgpSCpoKCgoKCgoKUgoKCgoKmgoIAASyCgoKmlIKClIKCpsqmooKmgoKmgpSCgoKmgoKUpqKCgoKUpqKCpoKCgoKCgoKUgoIABhKCgrimAAIQgoKCgoKUgoKUpqKCgoKClKaCgoKUgpSmAAIYgoKCgoKUgoKUpgACFIKUpqKCgoKClKaCgoKUgpSClKaigoKCgpSCgqaspqyCgoKClJSmpIKCgpSUqqKCgpSClIKCgoKClIKCgqaCgoKClKaCgoKUlIKClKiCgpSCgpSoooKWgoKCgpSmgoKCgpSClIKUgoKCgoKCgoKClJSCgoKCgoKCgoKUlKaIgpaqpMwAAhKClIKmAAgUgoKUgoKUgpSCgoKCqKKCgoKCAAYSooKCAAYSooKCggAGFKqCggAXPoKCggAMIIKCggAPKIKCggARKoKUgoKUpt66AAYQgoKmgpaU3JTegoKmgoKCgsbCgqiCgoKClIKmgoKCgpSCgoKUgpSC", "144-149:1;150-155:2;156-161:3;497-499:1", "", "89=GetEmitScriptTarget/1/1/1,GetUseDefineForClassFields/1/1/1;271=SubtreeFacts/2/3/1,SubtreeFacts/3/3/2;487=EmitContext/1/1/1,getHelperVariableName/1/1/1;493=Factory/2/4/2,Factory/3/4/3,Factory/4/4/4,NewVariableDeclaration/1/1/4,NewNodeList/1/1/3,NewVariableDeclarationList/1/1/2;506=NewUniqueNameEx/1/1/2;620=NewNodeList/1/1/1;623=createLet/1/4/2,createLet/2/4/3,NewNodeList/1/1/4,NewArrayLiteralExpression/1/1/4,createLet/3/4/4,createLet/4/4/5;656=IsFunctionExpression/1/1/1,Name/2/2/1,IsArrowFunction/1/1/2;657=NewNumericLiteral/1/1/1;695=NewThisExpression/1/1/1,createLet/1/1/1;720=NewNodeList/1/1/2,NewArrayLiteralExpression/1/1/2,createLet/1/1/2;726=NewNodeList/1/1/2,NewArrayLiteralExpression/1/1/2,createLet/1/1/2;752=NewToken/1/2/1,NewToken/2/2/5;798=NewNodeList/1/1/2,NewBlock/1/1/2;809=NewNodeList/1/1/2,NewBlock/1/1/2;864=NewVariableStatement/1/1/2,NewReturnStatement/1/1/3;911=ChildIsDecorated/1/1/1;1020=InlineExpressions/1/1/1,NewExpressionStatement/1/1/1;1155=NewNodeList/1/1/1;1212=IsStatic/1/1/4,Name/1/2/5,Name/2/2/5,IsPrivateIdentifier/1/1/5,IsPropertyDeclaration/1/2/8,IsGetAccessorDeclaration/1/1/8,IsMethodDeclaration/1/1/8,IsPropertyDeclaration/2/2/10,IsSetAccessorDeclaration/1/1/10;1240=NewThisExpression/1/1/1,NewToken/1/1/5;1336=Factory/1/1/1,UpdateMethodDeclaration/1/1/1;1355=Factory/1/1/1,UpdateGetAccessorDeclaration/1/1/1;1374=Factory/1/1/1,UpdateSetAccessorDeclaration/1/1/1;1487=NewRunInitializersHelper/1/1/2;1494=NewThisExpression/1/1/2,NewRunInitializersHelper/1/1/2;1549=UpdatePropertyDeclaration/1/1/1;1628=Visitor/1/2/4,Name/1/1/4,VisitNode/1/2/4,Visitor/2/2/7,VisitNode/2/2/7;1678=VisitNode/1/3/2,Visitor/1/2/3,VisitNode/2/3/3,VisitNode/3/3/4,EmitContext/1/1/5,Visitor/2/2/5,VisitIterationBody/1/1/5;1728=GetNonAssignmentOperatorForCompoundAssignment/1/1/4,NewToken/1/1/4;1890=NewReflectSetCall/1/1/2;2217=Name/1/1/2,createDescriptorMethod/1/1/2,NewNodeList/1/1/1;2229=Name/1/1/2,NewNodeList/1/2/2,createDescriptorMethod/1/1/2,NewNodeList/2/2/1;2242=Name/1/1/2,createDescriptorMethod/1/1/2,NewNodeList/1/1/1;2254=Name/1/2/3,NewNodeList/1/5/4,NewThisExpression/1/2/7,NewPropertyAccessExpression/1/2/7,NewReturnStatement/1/1/6,NewNodeList/2/5/5,NewBlock/1/2/5,createDescriptorMethod/1/2/2,Name/2/2/12,NewIdentifier/1/2/14,NewParameterDeclaration/1/1/14,NewNodeList/3/5/13,NewThisExpression/2/2/19,NewPropertyAccessExpression/2/2/19,NewIdentifier/2/2/20,NewAssignmentExpression/1/1/18,NewExpressionStatement/1/1/17,NewNodeList/4/5/16,NewBlock/2/2/16,createDescriptorMethod/2/2/11,NewNodeList/5/5/1;2281=NewNodeList/1/2/4,NewIdentifier/1/1/9,NewPropertyAccessExpression/1/1/9,NewReturnStatement/1/1/8,NewNodeList/2/2/7,NewBlock/1/1/7;2297=NewNodeList/1/2/4,NewIdentifier/1/1/10,NewPropertyAccessExpression/1/1/10,NewThisExpression/1/1/11,NewFunctionCallCall/1/1/9,NewReturnStatement/1/1/8,NewNodeList/2/2/7,NewBlock/1/1/7;2316=NewIdentifier/1/3/5,NewParameterDeclaration/1/1/5,NewNodeList/1/2/4,NewIdentifier/2/3/12,NewPropertyAccessExpression/1/1/12,NewThisExpression/1/1/13,NewIdentifier/3/3/14,NewFunctionCallCall/1/1/11,NewReturnStatement/1/1/10,NewNodeList/2/2/9,NewBlock/1/1/9;2344=NewIdentifier/1/2/1,NewIdentifier/2/2/1,NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/3;2350=NewIdentifier/1/3/1,NewTypeCheck/1/1/1,NewIdentifier/2/3/2,NewIdentifier/3/3/2,NewPropertyAccessExpression/1/1/2;2354=NewToken/1/2/2,NewToken/2/2/4,NewVoidZeroExpression/1/1/5;2371=NewIdentifier/1/4/1,NewTrueExpression/1/3/1,NewPropertyAssignment/1/4/1,NewIdentifier/2/4/2,NewTrueExpression/2/3/2,NewPropertyAssignment/2/4/2,NewIdentifier/3/4/3,NewTrueExpression/3/3/3,NewPropertyAssignment/3/4/3,NewIdentifier/4/4/4,NewPropertyAssignment/4/4/4;2379=NewIdentifier/1/2/1,NewIdentifier/2/2/1,NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/exponentiation.go", "exponentiation.cs", "ABAYooKUlKTIopSkpKaigoKCgoSCgoKUgoKClJSCgoSCgoKCgpSCloKmgoKCgqaigoKCgsaigg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/forawait.go", "forawait.cs", "ACdwoqaCgoKCgpSUxoKqgoKCqoKmgqaigoKCgpSmgqaigpSYpIKmgqaygpSClKSkpKSk7qTu7u7u7u7u7u7IooKogoKUpqKCgpSClIKUzJSogoKmgoKUpqiCgqamooKCgpSUzKaCgoKClJTIooKClNiCqIKCgoKoooKCgpSUgqaigoKCgoKUgoKUgoKUgoKCgoKClKaCgoKCpoKCuqaigpSCgpSmgoKUpoKCgoKCgoKCzIKClIKWgoLKqIKCypTclMyCgpa6AAcUgr4ADCC6lJbOgoKUgpSmooKCggAHEoKmooKCggAIFIKmooKCggAIFIKmooKClIKClKaCgpSmgoKCgpSCpgAKGIKmooKClIKClKaCgpSmgoKCgpSCpgAJFoKmooKCggAIFIKmooKClIKClKaCgpSmgoKCgpSCpgAJFoKmooKWgoKCgpQABhCUgoKmooKCgqaCgoKCgoKCgoKUus6CgqaCgpSmgoKmAAkWzoKCgriUzIKClLiCgoKClA==", "63-68:1", "", "220=Factory/2/2/2,Visitor/1/1/2,VisitNode/1/1/2,NewAwaitHelper/1/1/2;265=createDownlevelAwait/1/1/2;286=createDownlevelAwait/1/1/2;383=Factory/2/2/2,NewAwaitHelper/1/1/2;418=NewIdentifier/1/1/1,NewPropertyAccessExpression/1/1/1,NewNodeList/1/1/3;432=NewVoidZeroExpression/1/1/1,NewAssignmentExpression/1/1/1;442=NewKeywordExpression/1/1/1,NewVariableDeclaration/1/2/1,NewVariableDeclaration/2/2/3;449=createDownlevelAwait/1/1/1,NewAssignmentExpression/1/2/1,NewAssignmentExpression/2/2/2,NewPrefixUnaryExpression/1/1/3;457=convertForOfStatementHead/1/1/4;466=RestoreEnclosingLabel/1/1/1;470=NewIdentifier/1/1/5,NewPropertyAssignment/1/1/5,NewNodeList/1/2/4,NewObjectLiteralExpression/1/1/4,NewAssignmentExpression/1/1/2,NewExpressionStatement/1/1/1;479=NewVariableDeclaration/1/1/1;483=NewPrefixUnaryExpression/1/2/4,NewToken/1/2/6,NewPrefixUnaryExpression/2/2/7,NewBinaryExpression/1/2/2,NewToken/2/2/10,NewIdentifier/1/1/13,NewPropertyAccessExpression/1/1/13,NewAssignmentExpression/1/1/11;496=createDownlevelAwait/1/1/2,NewExpressionStatement/1/1/2;504=NewIdentifier/1/1/3,NewPropertyAccessExpression/1/1/3,NewThrowStatement/1/1/2;525=Modifiers/1/1/2,EmitContext/1/2/4,Visitor/1/2/4,VisitParameters/1/1/4,EmitContext/2/2/7,Body/1/1/7,Visitor/2/2/7,VisitFunctionBody/1/1/7;543=Modifiers/1/1/2,Visitor/1/3/3,Name/1/1/3,VisitNode/1/1/3,EmitContext/1/2/5,Visitor/2/3/5,VisitParameters/1/1/5,EmitContext/2/2/8,Body/1/1/8,Visitor/3/3/8,VisitFunctionBody/1/1/8;562=Modifiers/1/1/2,Visitor/1/3/3,Name/1/1/3,VisitNode/1/1/3,EmitContext/1/2/5,Visitor/2/3/5,VisitParameters/1/1/5,EmitContext/2/2/8,Body/1/1/8,Visitor/3/3/8,VisitFunctionBody/1/1/8;606=Visitor/1/1/4,Name/1/1/4,VisitNode/1/1/4;652=Name/1/1/4;672=Modifiers/1/1/2,EmitContext/1/2/4,Visitor/1/2/4,VisitParameters/1/1/4,EmitContext/2/2/8,Body/1/1/8,Visitor/2/2/8,VisitFunctionBody/1/1/8;716=Name/1/1/4;742=Factory/2/2/3,Name/1/1/3,NewGeneratedNameForNodeEx/1/1/3;776=Body/1/3/1,AsBlock/1/2/1,Visitor/1/1/2,Body/2/3/2,StatementList/1/1/2,VisitNodes/1/1/2,Body/3/3/3,AsBlock/2/2/3;780=AsBlock/1/2/1,EmitContext/1/1/2,StatementList/1/1/2,EndAndMergeVariableEnvironmentList/1/1/2,AsBlock/2/2/3;802=NewToken/1/1/2;812=NewAsyncGeneratorHelper/1/1/1;826=Body/1/2/1,AsBlock/1/2/1,EmitContext/1/1/2,NewNodeList/1/1/2,EndAndMergeVariableEnvironmentList/1/1/2,Body/2/2/3,AsBlock/2/2/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/logicalassignment.go", "logicalassignment.cs", "ABAYooKUlKTIooKUtLS0poKClIKCgoKCgoLMgszegoKCgoKCzMwABxQADByigg==", "", "", "65=Expression/1/1/2;71=Name/1/1/3;76=Name/1/1/3;88=AsElementAccessExpression/1/1/2;106=Factory/2/4/4,NewToken/1/1/4,Factory/3/4/5,Factory/4/4/6,NewAssignmentExpression/1/1/6,NewParenthesizedExpression/1/1/5")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/namedevaluation.go", "namedevaluation.cs", "AA0gsoKmgoKCgoKoAAIQsoKCgrgAAhCiAAIQooKUgpS0gpS0tKSClKaCpqKClJSkpKSkgsqigoSUptKCgoKmgoKmgoKCppSClIKCggACEryCpoKCgoKYgqgAAyaCpoKCgqaGgpSCgoKCgpSCggAHEgAIFJiCpqa8gqaClIKClKamAAMQgoKCpgADEoKCgpSUggAJFgADHoKCgpSUggAHEgADIoKCgpSUggAJFgADIoKCgpSUggAHEgADEIKCggAIFAADNoKCgpSUggAIFAADFIKCgoKClJSCAAcUopSkpKSkpKSkpII=", "207-209:1", "", "27=AsNode/1/1/1,AssignedName/1/1/1;120=Name/1/1/1,HasSyntacticModifier/1/1/1;205=AsClassDeclaration/1/2/1,Modifiers/1/1/2,Name/1/1/3,TypeParameterList/1/1/4,AsClassDeclaration/2/2/5;213=AsClassExpression/1/2/1,Modifiers/1/1/2,Name/1/1/3,TypeParameterList/1/1/4,AsClassExpression/2/2/5;274=Name/1/1/3;296=Name/1/1/2;316=Name/1/1/4;338=Name/1/1/4;353=Modifiers/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/nullishcoalescing.go", "nullishcoalescing.cs", "ABAYooKUlKTIopSCgoKCgpQABxDoooI=", "", "", "45=EmitContext/1/1/1,createNotNullCondition/1/1/1,Factory/2/3/2,NewToken/1/2/2,Factory/3/3/4,NewToken/2/2/4,Visitor/1/1/5,VisitNode/1/1/5")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/objectrestspread.go", "objectrestspread.cs", "ABUo0oKYgoKAkpSkpKSCpIKkpKSkpKSkpKSkpKSk+IKCgqaigoKCgpQACRaGAAgUpqKCgoKCgqYACAqCgoKmgqbSgoIADBbSgoIADRjSgoIADRjSgoIADxzSgoIADhrSgoIADRjSgoIADhq2goKCgoKCgqaClIKCgoKCgoKCgpSCuIKCgoKCpoKCpqKCgoKCiIK6goKCgpSCgoKUgoKCgoKClgAAFIKClIKUgoKUgoKUgoKCgpSCgrqCgoKClIKCgsqmooKCgrqCgoKClJSCgoKClJTMpoKCgoKCgpSmooKCgoKUpqSCzKaigoKCgoKCgoKClIKCgoKmgoKCgoKmqJSUgpSClAAHEqaygsyCgoKCgpSmooIAAjCCgpSCgoKClJSUpqKClIKCgoKCgoKUgpSCgpSUpoKUxqKC", "28-28:1", "", "125=Factory/2/2/4,AsNode/1/1/4,NewGeneratedNameForNode/1/1/4,Visitor/1/1/7,VisitNode/1/1/7;175=Modifiers/1/1/2,Visitor/1/1/4,VisitNodes/1/1/4,AsNode/1/1/7,transformFunctionBody/1/1/7;196=Modifiers/1/1/2,Visitor/1/2/3,Name/1/1/3,VisitNode/1/1/3,Visitor/2/2/5,VisitNodes/1/1/5,AsNode/1/1/8,transformFunctionBody/1/1/8;218=Modifiers/1/1/2,Visitor/1/2/3,Name/1/1/3,VisitNode/1/1/3,Visitor/2/2/5,VisitNodes/1/1/5,AsNode/1/1/8,transformFunctionBody/1/1/8;240=Modifiers/1/1/2,Visitor/1/2/4,Name/1/1/4,VisitNode/1/1/4,Visitor/2/2/7,VisitNodes/1/1/7,AsNode/1/1/10,transformFunctionBody/1/1/10;264=Modifiers/1/1/2,Visitor/1/2/4,Name/1/1/4,VisitNode/1/1/4,Visitor/2/2/6,VisitNodes/1/1/6,AsNode/1/1/9,transformFunctionBody/1/1/9;287=Modifiers/1/1/2,Visitor/1/1/4,VisitNodes/1/1/4,AsNode/1/1/8,transformFunctionBody/1/1/8;309=Modifiers/1/1/2,Visitor/1/2/4,Name/1/1/4,VisitNode/1/1/4,Visitor/2/2/6,VisitNodes/1/1/6,AsNode/1/1/9,transformFunctionBody/1/1/9;379=Factory/1/1/2,NewGeneratedNameForNode/1/1/2;426=Factory/1/1/2,NewGeneratedNameForNode/1/1/2;473=Factory/2/2/2,AsVariableDeclaration/1/1/2,UpdateVariableDeclaration/1/1/2;508=AsNode/1/1/2;545=Factory/2/3/1,Factory/3/3/1,NewVariableDeclaration/1/1/1,NewNodeList/1/1/1;574=AsNode/1/1/2", "", "33:21,34:13")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/optionalcatch.go", "optionalcatch.cs", "ABAYooKUlKTIooK6xqKC", "", "", "37=Factory/2/3/1,Factory/3/3/1,NewTempVariable/1/1/1,NewVariableDeclaration/1/1/1,Visitor/1/1/2,Invoke/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/optionalchain.go", "optionalchain.cs", "ABEcooKUlKaClKSkyKKElIKChIKCgoKCgpSmpqKChoKCgpSmooSUgpSCgoKCgpS4goKUgqaCgoKUpqKCgpSmopSkpKQACxKipqKCgoKCgpSmosaigoKCgoKCgoKUgoKCgpSClIKUgoKCgpSmgpTGgoKClIKClJQACRKmgoIABhAABhCCgpSCxqKC", "", "", "231=Visitor/1/1/4,ArgumentList/1/1/4,VisitNodes/1/1/4;246=EmitContext/1/1/1,createNotNullCondition/1/1/1,Factory/2/5/2,NewToken/1/2/2,Factory/3/5/3,NewTrueExpression/1/1/3,Factory/4/5/4,NewToken/2/2/4,Factory/5/5/5,NewDeleteExpression/1/1/5;253=EmitContext/1/1/1,createNotNullCondition/1/1/1,Factory/2/4/2,NewToken/1/2/2,Factory/3/4/3,NewVoidZeroExpression/1/1/3,Factory/4/4/4,NewToken/2/2/4")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/taggedtemplate.go", "taggedtemplate.cs", "AA0aAAcSooLGooKUlKSkyKKCgpSCggAGFIKCpoKmgqaigpSCppaCgpSCgpSCgoKCgoKCuAADEIKCqLqmgoLGooKUpqKCgoqCgoKUrJSCgqaigpSCgpSCgqY=", "", "", "59=Clip/1/1/1,Factory/1/3/2,Factory/2/3/4,Factory/3/3/5,NewNodeList/1/1/5,NewVariableDeclarationList/1/1/4,NewVariableStatement/1/1/2;110=NewNodeList/1/2/1,NewArrayLiteralExpression/1/2/1,NewNodeList/2/2/2,NewArrayLiteralExpression/2/2/2;116=NewVariableDeclaration/1/1/2;119=NewAssignmentExpression/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/usestrict.go", "usestrict.cs", "AAsSorgAChKigpSmooKmgoKYhKaCgoI=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/using.go", "using.cs", "ABUqooIAChaygqaUtLS0tLSmsoKmgoIAAE6UgpSCgqqCgoKCgqSUpoKogpaCgoKCgpQACyCCggAHFJSCAAYQlJSCgoKCgoKmsoKCgoKCkrqCgpSmogAAGgAKGqaiAAAagoKCpoKCgqiCgoKCgoLMAAYQAAsepoKUgoKmmoKkpKSmppKCgriCgoKCgoKihIKogqaCgpQAChyCgoKCgriCgoKmuKakpqKClLikhAACIIKWgoKCgqaCpqSEAAIogpaCpqSEpoKYggAALoKCgoKCgrgAADaCgoKCgrimtIKCgoKCpoKCgoKClKakgpSCgoKUpoKCgoKmpIKCgri4hIKClIKCgoKUgqaCgoKClJSCgpSClIKUlKaCpoKcyoKCggABQoKCABAugoKCAAwe3oKCxqKmgpSkpMiCpqKClKaCgoKCgpSCpg==", "309-330:1;332-337:2", "", "118=Factory/1/3/2,Factory/2/3/5,Factory/3/3/6,NewNodeList/1/1/6,NewNamedExports/1/1/5,NewExportDeclaration/1/1/2;132=Factory/2/5/1,Factory/3/5/2,NewModifier/1/1/2,NewModifierList/1/1/1,Factory/4/5/4,Factory/5/5/5,NewNodeList/1/1/5,NewVariableDeclarationList/1/1/4;174=transformUsingDeclarations/1/1/1;189=Factory/1/4/1,Factory/2/4/1,Factory/3/4/2,NewVariableStatement/1/1/2,Factory/4/4/3,UpdateForStatement/1/1/3,NewNodeList/1/1/1,NewBlock/1/1/1;216=Factory/2/2/1,NewNodeList/1/1/1,IfElse/1/1/2;225=AsBlock/1/2/1,Factory/2/2/2,NewNodeList/1/1/2,AsBlock/2/2/3;230=Factory/2/2/1,NewNodeList/1/1/1;237=Factory/1/4/1,Factory/2/4/4,Factory/3/4/5,Factory/4/4/6,NewVariableDeclaration/1/1/6,NewNodeList/1/1/5,NewVariableDeclarationList/1/1/4,UpdateForInOrOfStatement/1/1/1;308=AsVariableDeclaration/1/1/1,Name/1/1/2,Factory/2/2/5,NewAddDisposableResourceHelper/1/1/5;529=Factory/3/11/1,Factory/4/11/1,NewIdentifier/1/3/1,Factory/5/11/1,NewArrayLiteralExpression/1/1/1,NewPropertyAssignment/1/3/1,Factory/6/11/2,Factory/7/11/2,NewIdentifier/2/3/2,Factory/8/11/2,NewVoidZeroExpression/1/1/2,NewPropertyAssignment/2/3/2,Factory/9/11/3,Factory/10/11/3,NewIdentifier/3/3/3,Factory/11/11/3,NewFalseExpression/1/1/3,NewPropertyAssignment/3/3/3;541=Factory/2/13/1,NewVariableDeclaration/1/1/1,Factory/3/13/7,Factory/4/13/7,Factory/5/13/8,Factory/6/13/9,Factory/7/13/10,Factory/8/13/10,NewIdentifier/1/2/10,NewPropertyAccessExpression/1/2/10,NewAssignmentExpression/1/2/9,NewExpressionStatement/1/2/8,Factory/9/13/14,Factory/10/13/15,Factory/11/13/16,Factory/12/13/16,NewIdentifier/2/2/16,NewPropertyAccessExpression/2/2/16,Factory/13/13/17,NewTrueExpression/1/1/17,NewAssignmentExpression/2/2/15,NewExpressionStatement/2/2/14,NewNodeList/1/1/7,NewBlock/1/1/7;561=Factory/3/10/1,Factory/4/10/3,Factory/5/10/3,Factory/6/10/4,Factory/7/10/8,NewDisposeResourcesHelper/1/1/8,NewVariableDeclaration/1/1/4,NewNodeList/1/2/3,NewVariableDeclarationList/1/1/3,NewVariableStatement/1/1/1,Factory/8/10/12,Factory/9/10/12,Factory/10/10/12,NewAwaitExpression/1/1/12,NewExpressionStatement/1/1/12,NewIfStatement/1/1/12;574=Factory/3/4/1,Factory/4/4/2,NewDisposeResourcesHelper/1/1/2,NewExpressionStatement/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/estransforms/utilities.go", "utilities.cs", "AAwUst6CgqaCgoKCgqYAHUyygqyilIKClKSEqKSEqKqkyIKosoKUhKiEqKa6goKCgoK4qIKogrqCugACGIKClIKWqN6ClISCqILegqaCgqaUAAkYgoKuooKUlILGgsaCxoLGggAIDLI=", "", "", "17=Modifiers/1/1/1,ExtractModifiers/1/1/1,Name/1/1/2;36=NewToken/1/3/6,NewKeywordExpression/1/1/7,NewBinaryExpression/1/3/2,NewToken/2/3/10,NewToken/3/3/15,NewVoidZeroExpression/1/1/16,NewBinaryExpression/2/3/11;87=Name/1/1/1;94=AsElementAccessExpression/1/1/1;120=AsPropertyAccessExpression/1/1/2,Name/1/1/2;124=AsElementAccessExpression/1/1/1;130=NewIdentifier/1/1/2;143=NewNodeList/1/1/2;151=NewNodeList/1/1/2;156=NewIdentifier/1/1/2;170=NewKeywordExpression/1/1/1,NewIdentifier/1/1/2;173=NewNodeList/1/1/2,NewToken/1/1/4;184=NewKeywordExpression/1/1/1,NewIdentifier/1/1/2;188=NewNodeList/1/1/2,NewToken/1/1/4;205=NewIdentifier/1/2/2,NewIdentifier/2/2/3,NewPropertyAccessExpression/1/1/1,NewKeywordExpression/1/1/6,NewNodeList/1/1/5;266=Name/1/1/3,NewGeneratedPrivateNameForNodeEx/1/1/3")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

[GoPackage("estransforms", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/transformers/estransforms")]
public static partial class estransforms_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct asyncContextFlags {}
    internal partial struct asyncTransformer {}
    internal partial struct classFacts {}
    internal partial struct classFieldsTransformer {}
    internal partial struct classInfo {}
    internal partial struct classLexicalEnv {}
    internal partial struct classLexicalEnvironment {}
    internal partial struct esDecoratorTransformer {}
    internal partial struct exponentiationTransformer {}
    internal partial struct flattenResult {}
    internal partial struct forAwaitHierarchyFacts {}
    internal partial struct forawaitTransformer {}
    internal partial struct lexicalArgumentsInfo {}
    internal partial struct lexicalEntry {}
    internal partial struct lexicalEntryKind {}
    internal partial struct logicalAssignmentTransformer {}
    internal partial struct memberInfo {}
    internal partial struct nullishCoalescingTransformer {}
    internal partial struct objectRestSpreadTransformer {}
    internal partial struct oldParamScope {}
    internal partial struct optionalCatchTransformer {}
    internal partial struct optionalChainTransformer {}
    internal partial struct partialResult {}
    internal partial struct privateEnvironment {}
    internal partial struct privateEnvironmentData {}
    internal partial struct privateIdentifierInfo {}
    internal partial struct superAccessState {}
    internal partial struct taggedTemplateTransformer {}
    internal partial struct useStrictTransformer {}
    internal partial struct usingDeclarationTransformer {}
    internal partial struct usingKind {}
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
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package));
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
[GoMethodHost(typeof(estransforms_package.asyncTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛasyncTransformer;
[GoMethodHost(typeof(estransforms_package.classFieldsTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛclassFieldsTransformer;
[GoMethodHost(typeof(estransforms_package.esDecoratorTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛesDecoratorTransformer;
[GoMethodHost(typeof(estransforms_package.exponentiationTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛexponentiationTransformer;
[GoMethodHost(typeof(estransforms_package.forawaitTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛforawaitTransformer;
[GoMethodHost(typeof(estransforms_package.logicalAssignmentTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛlogicalAssignmentTransformer;
[GoMethodHost(typeof(estransforms_package.nullishCoalescingTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛnullishCoalescingTransformer;
[GoMethodHost(typeof(estransforms_package.objectRestSpreadTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛobjectRestSpreadTransformer;
[GoMethodHost(typeof(estransforms_package.optionalCatchTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛoptionalCatchTransformer;
[GoMethodHost(typeof(estransforms_package.optionalChainTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛoptionalChainTransformer;
[GoMethodHost(typeof(estransforms_package.superAccessState)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛsuperAccessState;
[GoMethodHost(typeof(estransforms_package.taggedTemplateTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛtaggedTemplateTransformer;
[GoMethodHost(typeof(estransforms_package.useStrictTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛuseStrictTransformer;
[GoMethodHost(typeof(estransforms_package.usingDeclarationTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class estransformsᴛusingDeclarationTransformer;
// </MethodHolders>
